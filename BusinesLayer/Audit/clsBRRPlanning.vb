Imports DatabaseLayer
Public Structure strBRR_Planning
    Dim iBRRP_PKID As Integer
    Dim iBRRP_YearId As Integer
    Dim iBRRP_SalesUnitCode As Integer
    Dim iBRRP_CustID As Integer
    Dim iBRRP_BranchID As Integer
    Dim iBRRP_RegionID As Integer
    Dim iBRRP_ZoneID As Integer
    Dim iBRRP_RiskScore As Integer
    Dim iBRRP_BRRRatingID As Integer
    Dim iBRRP_BCMRatingID As Integer
    Dim iBRRP_IARatingID As Integer
    Dim iBRRP_GrossControlScore As Integer
    Dim iBRRP_GrossControlRatingID As Integer
    Dim iBRRP_NetScore As Integer
    Dim iBRRP_BranchRRRatingID As Integer
    Dim iBRRP_AAPlan As Integer
    Dim sBRRP_Remarks As String
    Dim sBRRP_Status As String
    Dim sBRRP_DelFlag As String
    Dim iBRRP_CrBy As Integer
    Dim iBRRP_UpdatedBy As Integer
    Dim iBRRP_SubmittedBy As Integer
    Dim sBRRP_IPAddress As String
    Dim iBRRP_CompID As Integer
    Public Property iBRRPPKID() As Integer
        Get
            Return (iBRRP_PKID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_PKID = Value
        End Set
    End Property
    Public Property iBRRPYearId() As Integer
        Get
            Return (iBRRP_YearId)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_YearId = Value
        End Set
    End Property
    Public Property iBRRPSalesUnitCode() As Integer
        Get
            Return (iBRRP_SalesUnitCode)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_SalesUnitCode = Value
        End Set
    End Property
    Public Property iBRRPCustID() As Integer
        Get
            Return (iBRRP_CustID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_CustID = Value
        End Set
    End Property
    Public Property iBRRPBranchID() As Integer
        Get
            Return (iBRRP_BranchID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_BranchID = Value
        End Set
    End Property
    Public Property iBRRPRegionID() As Integer
        Get
            Return (iBRRP_RegionID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_RegionID = Value
        End Set
    End Property
    Public Property iBRRPZoneID() As Integer
        Get
            Return (iBRRP_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_ZoneID = Value
        End Set
    End Property
    Public Property iBRRPRiskScore() As Integer
        Get
            Return (iBRRP_RiskScore)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_RiskScore = Value
        End Set
    End Property
    Public Property iBRRPBRRRatingID() As Integer
        Get
            Return (iBRRP_BRRRatingID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_BRRRatingID = Value
        End Set
    End Property
    Public Property iBRRPBCMRatingID() As Integer
        Get
            Return (iBRRP_BCMRatingID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_BCMRatingID = Value
        End Set
    End Property
    Public Property iBRRPIARatingID() As Integer
        Get
            Return (iBRRP_IARatingID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_IARatingID = Value
        End Set
    End Property
    Public Property iBRRPGrossControlScore() As Integer
        Get
            Return (iBRRP_GrossControlScore)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_GrossControlScore = Value
        End Set
    End Property
    Public Property iBRRPNetScore() As Integer
        Get
            Return (iBRRP_NetScore)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_NetScore = Value
        End Set
    End Property
    Public Property iBRRPAAPlan() As Integer
        Get
            Return (iBRRP_AAPlan)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_AAPlan = Value
        End Set
    End Property
    Public Property sBRRPRemarks() As String
        Get
            Return (sBRRP_Remarks)
        End Get
        Set(ByVal Value As String)
            sBRRP_Remarks = Value
        End Set
    End Property
    Public Property sBRRPStatus() As String
        Get
            Return (sBRRP_Status)
        End Get
        Set(ByVal Value As String)
            sBRRP_Status = Value
        End Set
    End Property
    Public Property sBRRPDelFlag() As String
        Get
            Return (sBRRP_DelFlag)
        End Get
        Set(ByVal Value As String)
            sBRRP_DelFlag = Value
        End Set
    End Property
    Public Property iBRRPCrBy() As Integer
        Get
            Return (iBRRP_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_CrBy = Value
        End Set
    End Property
    Public Property iBRRPUpdatedBy() As Integer
        Get
            Return (iBRRP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_UpdatedBy = Value
        End Set
    End Property
    Public Property iBRRPSubmittedBy() As Integer
        Get
            Return (iBRRP_SubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_SubmittedBy = Value
        End Set
    End Property
    Public Property sBRRPIPAddress() As String
        Get
            Return (sBRRP_IPAddress)
        End Get
        Set(ByVal Value As String)
            sBRRP_IPAddress = Value
        End Set
    End Property
    Public Property iBRRPCompID() As Integer
        Get
            Return (iBRRP_CompID)
        End Get
        Set(ByVal Value As Integer)
            iBRRP_CompID = Value
        End Set
    End Property
End Structure
Public Class clsBRRPlanning
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsRiskGeneral As New clsRiskGeneral
    Public Function LoadBRRDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("RiskReviewNo")
            dt.Columns.Add("RiskReviewTitle")
            dt.Columns.Add("Status")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("EndDate")
            dt.Columns.Add("Days")

            sSql = "Select ORG_NAME,BRRS_AsgNo,BRR_Title,BRR_ASDate,BRR_Status,BRR_AEDate," 'BRRR_IssuStatus,
            sSql = sSql & " DateDiff(Day, BRR_ASDate, BRR_AEDate) As DayDiff From sad_org_structure"
            sSql = sSql & " Left Join Risk_BRRSchedule On BRRS_CustID=" & iCustID & " And BRRS_BranchID=ORG_NODE And BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & ""
            sSql = sSql & " Left Join Risk_BRRChecklist_Mas On BRR_CustID=" & iCustID & " And BRRS_BranchID=BRR_BranchId And BRR_YearID=" & iYearID & " And BRR_CompID=" & iACID & ""
            'Depending on the requirement(Sujatha Mam) the status is taken from checklist than BRRReport
            'sSql = sSql & " Left Join Risk_BRRReport On BRRR_CustID=" & iCustID & " And BRRR_AsgID=BRRS_PKID And BRRR_CompID=" & iACID & " And BRRR_YearID=" & iYearID & ""
            sSql = sSql & " Order by ORG_NAME"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("ORG_NAME")) = False Then
                        dRow("BranchName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("ORG_NAME"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BRRS_AsgNo")) = False Then
                        dRow("RiskReviewNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BRRS_AsgNo"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BRR_Status")) = False Then
                        If dtDetails.Rows(i)("BRR_Status") = "W" Then
                            dRow("Status") = "In Progress"
                        ElseIf dtDetails.Rows(i)("BRR_Status") = "S" Then
                            dRow("Status") = "Completed"
                        Else
                            dRow("Status") = "In Progress"
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BRR_Title")) = False Then
                        dRow("RiskReviewTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BRR_Title"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BRR_ASDate")) = False Then
                        dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("BRR_ASDate"), "F")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BRR_AEDate")) = False Then
                        dRow("EndDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("BRR_AEDate"), "F")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BRR_ASDate")) = False And IsDBNull(dtDetails.Rows(i)("BRR_AEDate")) = False Then
                        dRow("Days") = dtDetails.Rows(i)("DayDiff")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRPlanningDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer = 0
        Dim dBRRNetScore As Double = 0, dBCMNetScore As Double = 0, dBANetScore As Double = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PKID")
            dt.Columns.Add("Status")
            dt.Columns.Add("IRDACode")
            dt.Columns.Add("SalesUnitCode")
            dt.Columns.Add("BranchCode")
            dt.Columns.Add("BranchID")
            dt.Columns.Add("AreaID")
            dt.Columns.Add("RegionID")
            dt.Columns.Add("ZoneID")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("Region")
            dt.Columns.Add("Zone")
            dt.Columns.Add("BranchRiskScore")
            dt.Columns.Add("BranchRiskScoreRating")
            dt.Columns.Add("BranchRiskScoreRatingColor")
            dt.Columns.Add("BRRResidualRiskRating")
            dt.Columns.Add("BRRResidualRiskRatingColor")
            'dt.Columns.Add("BCMResidualRiskRating")
            'dt.Columns.Add("BCMResidualRiskRatingColor")
            dt.Columns.Add("IAResidualRiskRating")
            dt.Columns.Add("IAResidualRiskRatingColor")
            dt.Columns.Add("BranchResidualRiskRating")
            dt.Columns.Add("BranchResidualRiskRatingColor")
            dt.Columns.Add("BABRRBCMHighestRiskScore")
            dt.Columns.Add("IsPlanCY")
            dt.Columns.Add("IsPlanPY")
            dt.Columns.Add("IsPlanPPY")
            dt.Columns.Add("IsAnnualPlanned")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("IsPlanPPYRating")
            dt.Columns.Add("DataPPFYColor")
            dt.Columns.Add("IsPlanPYRating")
            dt.Columns.Add("DataPFYColor")

            sSql = "Select Z.Org_Node As ZoneID,Z.Org_Name As Zone, R.Org_Node As RegionID,R.Org_Name As Region,A.Org_Node As AreaID, B.Org_Name As Branch, B.Org_Node As BranchID,"
            sSql = sSql & " Case When B.Org_Code Is Null Then '' Else B.Org_Code End IRDACode, Case When B.Org_SalesUnitCode IS Null Then '' Else B.Org_SalesUnitCode End SalesUnitCode,"
            sSql = sSql & " Case When B.Org_BranchCode Is Null Then '' Else B.Org_BranchCode End BranchCode, CY.BRRP_AAPlan As IsPlanCY,PY.BRRP_AAPlan As IsPlanPY,PPY.BRRP_AAPlan As IsPlanPPY,"
            sSql = sSql & " d.CMAR_Desc As BRRRating, d.CMAR_Color As BRRColor, h.GOD_BRRNetScore As BRRScore,"
            sSql = sSql & " e.CMAR_Desc As BARating, e.CMAR_Color As BAColor, h.GOD_BANetScore As BAScore,"
            sSql = sSql & " f.CMAR_Desc As BCMRating, f.CMAR_Color As BCMColor, h.GOD_BCMNetScore As BCMScore,"
            sSql = sSql & " g.CMAR_Desc As BRRPPYearRating, g.CMAR_Color As BRRPPYearColor, m.GOD_BRRNetScore As BRRPPYearScore,"
            sSql = sSql & " p.CMAR_Desc As BRRPYearRating, p.CMAR_Color As BRRPYearColor, k.GOD_BRRNetScore As BRRPYearScore"
            sSql = sSql & " From sad_org_Structure Z, sad_org_Structure R, sad_org_Structure A, sad_org_Structure B"
            sSql = sSql & " Left Join Risk_BRRPlanning CY On CY.BRRP_CustId=" & iCustID & " And CY.BRRP_YearId=" & iYearID & " And B.Org_Node=CY.BRRP_BranchID And CY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PY On PY.BRRP_CustId=" & iCustID & " And PY.BRRP_YearId=" & iYearID - 1 & " And B.Org_Node=PY.BRRP_BranchID And PY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PPY On PPY.BRRP_CustId=" & iCustID & " And PPY.BRRP_YearId=" & iYearID - 2 & " And B.Org_Node=PPY.BRRP_BranchID And PPY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details h On h.GOD_BranchID=B.Org_Node And h.GOD_CompID=" & iACID & " And h.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating d On d.CMAR_ID=GOD_BRRNetRatingID And d.CMAR_CompID=" & iACID & " And d.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating e On e.CMAR_ID=GOD_BANetRatingID And e.CMAR_CompID=" & iACID & " And e.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating f On f.CMAR_ID=GOD_BCMNetRatingID And f.CMAR_CompID=" & iACID & " And f.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details m On m.GOD_BranchID=B.Org_Node And m.GOD_CompID=" & iACID & " And m.GOD_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join CMARating g On g.CMAR_ID=m.GOD_BRRNetRatingID And g.CMAR_CompID=" & iACID & " And g.CMAR_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details k On k.GOD_BranchID=B.Org_Node And k.GOD_CompID=" & iACID & " And k.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating p On p.CMAR_ID=k.GOD_BRRNetRatingID And p.CMAR_CompID=" & iACID & " And p.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Where Z.Org_Node = R.Org_Parent And R.Org_Node = A.Org_Parent And A.Org_Node = B.Org_Parent And Z.Org_levelCode > 0"
            sSql = sSql & " And Z.Org_Delflag = 'A' And R.Org_Delflag = 'A' And A.Org_Delflag = 'A' And B.Org_Delflag = 'A'"
            sSql = sSql & " Order by Z.Org_Name, R.Org_Name, A.Org_Name, B.Org_Name"


            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    iSlNo = iSlNo + 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = iSlNo
                    dRow("PKID") = "0"
                    dRow("Status") = "N"
                    If IsDBNull(dtDetails.Rows(i)("IRDACode")) = False Then
                        dRow("IRDACode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("IRDACode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("SalesUnitCode")) = False Then
                        dRow("SalesUnitCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SalesUnitCode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BranchCode")) = False Then
                        dRow("BranchCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BranchCode"))
                    End If
                    dRow("BranchID") = dtDetails.Rows(i)("BranchID")
                    dRow("AreaID") = dtDetails.Rows(i)("BranchID")
                    dRow("RegionID") = dtDetails.Rows(i)("RegionID")
                    dRow("ZoneID") = dtDetails.Rows(i)("ZoneID")
                    dRow("BranchName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Branch"))
                    dRow("Region") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Region"))
                    dRow("Zone") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Zone"))
                    dRow("BranchRiskScore") = ""
                    dRow("BranchRiskScoreRating") = ""
                    dRow("BranchRiskScoreRatingColor") = ""
                    dRow("BABRRBCMHighestRiskScore") = ""
                    'BRR
                    If IsDBNull(dtDetails.Rows(i)("BRRRating")) = False Then
                        dBRRNetScore = dtDetails.Rows(i)("BRRScore")
                        dRow("BRRResidualRiskRating") = dtDetails.Rows(i)("BRRRating")
                        dRow("BRRResidualRiskRatingColor") = dtDetails.Rows(i)("BRRColor")
                    End If

                    'BCM
                    'If IsDBNull(dtDetails.Rows(i)("BCMRating")) = False Then
                    '    dBCMNetScore = dtDetails.Rows(i)("BCMScore")
                    '    dRow("BCMResidualRiskRating") = dtDetails.Rows(i)("BCMRating")
                    '    dRow("BCMResidualRiskRatingColor") = dtDetails.Rows(i)("BCMColor")
                    'End If

                    'BIA
                    If IsDBNull(dtDetails.Rows(i)("BARating")) = False Then
                        dBANetScore = dtDetails.Rows(i)("BAScore")
                        dRow("IAResidualRiskRating") = dtDetails.Rows(i)("BARating")
                        dRow("IAResidualRiskRatingColor") = dtDetails.Rows(i)("BAColor")
                    End If

                    'BRR PPY
                    If IsDBNull(dtDetails.Rows(i)("BRRPPYearRating")) = False Then
                        dRow("IsPlanPPYRating") = dtDetails.Rows(i)("BRRPPYearRating")
                        dRow("DataPPFYColor") = dtDetails.Rows(i)("BRRPPYearColor")
                    End If

                    'BRR PY
                    If IsDBNull(dtDetails.Rows(i)("BRRPYearRating")) = False Then
                        dRow("IsPlanPYRating") = dtDetails.Rows(i)("BRRPYearRating")
                        dRow("DataPFYColor") = dtDetails.Rows(i)("BRRPYearColor")
                    End If

                    dRow("BranchResidualRiskRating") = "" : dRow("BranchResidualRiskRatingColor") = ""
                    If (dBRRNetScore >= dBCMNetScore And dBRRNetScore >= dBANetScore) Then
                        If IsDBNull(dtDetails.Rows(i)("BRRRating")) = False Then
                            dRow("BABRRBCMHighestRiskScore") = dBRRNetScore
                        End If
                    ElseIf (dBCMNetScore >= dBRRNetScore And dBCMNetScore >= dBANetScore) Then
                        If IsDBNull(dtDetails.Rows(i)("BCMRating")) = False Then
                            dRow("BABRRBCMHighestRiskScore") = dBCMNetScore
                        End If
                    ElseIf (dBANetScore >= dBRRNetScore And dBANetScore >= dBCMNetScore) Then
                        If IsDBNull(dtDetails.Rows(i)("BARating")) = False Then
                            dRow("BABRRBCMHighestRiskScore") = dBANetScore
                        End If
                    End If
                    dRow("IsAnnualPlanned") = 0
                    If IsDBNull(dtDetails.Rows(i)("IsPlanCY")) = False Then
                        If dtDetails.Rows(i)("IsPlanCY") = 1 Then
                            dRow("IsPlanCY") = "YES"
                            dRow("IsAnnualPlanned") = 1
                        ElseIf dtDetails.Rows(i)("IsPlanCY") = 0 Then
                            dRow("IsPlanCY") = "NO"
                        End If
                    End If
                    dRow("Remarks") = ""
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRAsgIDFromBranchID(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRR_AsgID From Risk_BRRChecklist_Mas Where BRR_BranchId=" & iBranchID & " And BRR_YearID=" & iYearID & " And BRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBIAAsgIDFromBranchID(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BIAC_AsgID From Audit_BIAChecklist_MAs Where BIAC_BranchId=" & iBranchID & " And BIAC_YearID=" & iYearID & " And BIAC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRPlanningDashboardDetailsInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer = 0
        Dim dBRRNetScore As Double = 0, dBCMNetScore As Double = 0, dBANetScore As Double = 0
        Dim dtColor As New DataTable
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PKID")
            dt.Columns.Add("Status")
            dt.Columns.Add("IRDACode")
            dt.Columns.Add("SalesUnitCode")
            dt.Columns.Add("BranchCode")
            dt.Columns.Add("BranchID")
            dt.Columns.Add("AreaID")
            dt.Columns.Add("RegionID")
            dt.Columns.Add("ZoneID")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("Region")
            dt.Columns.Add("Zone")
            dt.Columns.Add("BranchRiskScore")
            dt.Columns.Add("BranchRiskScoreRating")
            dt.Columns.Add("BranchRiskScoreRatingColor")

            dt.Columns.Add("BRRResidualRiskRating")
            dt.Columns.Add("BRRResidualRiskRatingColor")

            'dt.Columns.Add("BCMResidualRiskRating")
            'dt.Columns.Add("BCMResidualRiskRatingColor")

            dt.Columns.Add("IAResidualRiskRating")
            dt.Columns.Add("IAResidualRiskRatingColor")

            dt.Columns.Add("BranchResidualRiskRating")
            dt.Columns.Add("BranchResidualRiskRatingColor")
            dt.Columns.Add("IsPlanCY")
            dt.Columns.Add("IsPlanPY")
            dt.Columns.Add("IsPlanPPY")
            dt.Columns.Add("IsAnnualPlanned")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("IsPlanPPYRating")
            dt.Columns.Add("DataPPFYColor")
            dt.Columns.Add("IsPlanPYRating")
            dt.Columns.Add("DataPFYColor")
            dt.Columns.Add("BABRRBCMHighestRiskScore")

            sSql = "Select BA.BRRP_PKID,BA.BRRP_Status,BA.BRRP_RiskScore,BA.BRRP_GrossControlScore,BA.BRRP_NetScore,BA.BRRP_BranchID,BA.BRRP_RegionID,BA.BRRP_ZoneID,BA.BRRP_Remarks,"
            sSql = sSql & " b.Org_Name As Region,c.Org_Name As Zone,a.Org_Name As Branch,Case When a.Org_Code Is Null Then '' Else a.Org_Code End IRDACode,"
            sSql = sSql & " Case When a.Org_SalesUnitCode IS Null Then '' Else a.Org_SalesUnitCode End SalesUnitCode,Case When a.Org_BranchCode IS Null Then '' Else a.Org_BranchCode End BranchCode,"
            sSql = sSql & " CY.BRRP_AAPlan As IsPlanCY,PY.BRRP_AAPlan As IsPlanPY,PPY.BRRP_AAPlan As IsPlanPPY,"
            sSql = sSql & " d.CMAR_Desc as BRRRating, d.CMAR_Color As BRRColor,e.CMAR_Desc As BARating, e.CMAR_Color As BAColor,f.CMAR_Desc As BCMRating, f.CMAR_Color as BCMColor,"
            sSql = sSql & " h.GOD_BRRNetScore as BRRScore,h.GOD_BANetScore as BAScore,h.GOD_BCMNetScore as BCMScore,"
            sSql = sSql & " g.CMAR_Desc As BRRPPYearRating, g.CMAR_Color As BRRPPYearColor, m.GOD_BRRNetScore As BRRPPYearScore,"
            sSql = sSql & " p.CMAR_Desc As BRRPYearRating, p.CMAR_Color As BRRPYearColor, k.GOD_BRRNetScore As BRRPYearScore,"
            sSql = sSql & " l.CMAR_Desc as BRRCYRating,l.CMAR_Color as BRRCYColor,z.CMAR_Desc,z.CMAR_Color,z.CMAR_ID as NetRatingID"
            sSql = sSql & " From Risk_BRRPlanning BA"
            sSql = sSql & " Left Join Risk_BRRPlanning CY On CY.BRRP_CustId=" & iCustID & " And CY.BRRP_YearId=" & iYearID & " And CY.BRRP_BranchID=BA.BRRP_BranchID And CY.BRRP_RegionID=BA.BRRP_RegionID And CY.BRRP_ZoneID=BA.BRRP_ZoneID And CY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PY On PY.BRRP_CustId=" & iCustID & " And PY.BRRP_YearId=" & iYearID - 1 & " And PY.BRRP_BranchID=BA.BRRP_BranchID And PY.BRRP_RegionID=BA.BRRP_RegionID And PY.BRRP_ZoneID=BA.BRRP_ZoneID And PY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PPY On PPY.BRRP_CustId=" & iCustID & " And PPY.BRRP_YearId=" & iYearID - 2 & " And PPY.BRRP_BranchID=BA.BRRP_BranchID And PPY.BRRP_RegionID=BA.BRRP_RegionID And PPY.BRRP_ZoneID=BA.BRRP_ZoneID And PPY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure a On a.org_node=BA.BRRP_BranchID And a.Org_CompID=" & iACID & " "
            sSql = sSql & " Left Join sad_org_structure b On b.org_node=BA.BRRP_RegionID And b.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure c On c.org_node=BA.BRRP_ZoneID And c.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details h On h.GOD_BranchID=BA.BRRP_BranchID And h.GOD_CompID=" & iACID & " And h.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating d On d.CMAR_ID=GOD_BRRNetRatingID And d.CMAR_CompID=" & iACID & " And d.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating e On e.CMAR_ID=GOD_BANetRatingID And e.CMAR_CompID=" & iACID & " And e.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating f On f.CMAR_ID=GOD_BCMNetRatingID And f.CMAR_CompID=" & iACID & " And f.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details m On m.GOD_BranchID=BA.BRRP_BranchID And m.GOD_CompID=" & iACID & " And m.GOD_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join CMARating g On g.CMAR_ID=m.GOD_BRRNetRatingID And g.CMAR_CompID=" & iACID & " And g.CMAR_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details k On k.GOD_BranchID=BA.BRRP_BranchID And k.GOD_CompID=" & iACID & " And k.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating p On p.CMAR_ID=k.GOD_BRRNetRatingID And p.CMAR_CompID=" & iACID & " And p.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating z On z.CMAR_ID=BA.BRRP_NetScore And z.CMAR_CompId=" & iACID & " And (z.CMAR_YearID=" & iYearID - 1 & " Or z.CMAR_YearID=" & iYearID & ")"
            sSql = sSql & " Left Join CMARating l On l.CMAR_ID=BA.BRRP_RiskScore And l.CMAR_CompId=" & iACID & " And l.CMAR_YearID=" & iYearID & ""
            sSql = sSql & " Where BA.BRRP_CustId=" & iCustID & " And BA.BRRP_YearId = " & iYearID & " Order By BA.BRRP_PKID"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    iSlNo = iSlNo + 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = iSlNo
                    dRow("PKID") = dtDetails.Rows(i)("BRRP_PKID")
                    dRow("Status") = dtDetails.Rows(i)("BRRP_Status")
                    If IsDBNull(dtDetails.Rows(i)("IRDACode")) = False Then
                        dRow("IRDACode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("IRDACode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("SalesUnitCode")) = False Then
                        dRow("SalesUnitCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SalesUnitCode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BranchCode")) = False Then
                        dRow("BranchCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BranchCode"))
                    End If
                    dRow("BranchID") = dtDetails.Rows(i)("BRRP_BranchID")
                    dRow("AreaID") = dtDetails.Rows(i)("BRRP_BranchID")
                    dRow("RegionID") = dtDetails.Rows(i)("BRRP_RegionID")
                    dRow("ZoneID") = dtDetails.Rows(i)("BRRP_ZoneID")
                    dRow("BranchName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Branch"))
                    dRow("Region") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Region"))
                    dRow("Zone") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Zone"))
                    dRow("BranchRiskScore") = dtDetails.Rows(i)("BRRP_RiskScore")
                    dRow("BranchRiskScoreRating") = "" : dRow("BranchRiskScoreRatingColor") = ""
                    If dtDetails.Rows(i)("BRRP_RiskScore") > 0 Then
                        dRow("BranchRiskScoreRating") = dtDetails.Rows(i)("BRRCYRating")
                        dRow("BranchRiskScoreRatingColor") = dtDetails.Rows(i)("BRRCYColor")
                    End If

                    'BRR
                    If IsDBNull(dtDetails.Rows(i)("BRRRating")) = False Then
                        dBRRNetScore = dtDetails.Rows(i)("BRRScore")
                        dRow("BRRResidualRiskRating") = dtDetails.Rows(i)("BRRRating")
                        dRow("BRRResidualRiskRatingColor") = dtDetails.Rows(i)("BRRColor")
                    End If

                    'BCM
                    'If IsDBNull(dtDetails.Rows(i)("BCMRating")) = False Then
                    '    dBCMNetScore = dtDetails.Rows(i)("BCMScore")
                    '    dRow("BCMResidualRiskRating") = dtDetails.Rows(i)("BCMRating")
                    '    dRow("BCMResidualRiskRatingColor") = dtDetails.Rows(i)("BCMColor")
                    'End If

                    'BIA
                    If IsDBNull(dtDetails.Rows(i)("BARating")) = False Then
                        dBANetScore = dtDetails.Rows(i)("BAScore")
                        dRow("IAResidualRiskRating") = dtDetails.Rows(i)("BARating")
                        dRow("IAResidualRiskRatingColor") = dtDetails.Rows(i)("BAColor")
                    End If

                    'BRR PPY
                    If IsDBNull(dtDetails.Rows(i)("BRRPPYearRating")) = False Then
                        dRow("IsPlanPPYRating") = dtDetails.Rows(i)("BRRPPYearRating")
                        dRow("DataPPFYColor") = dtDetails.Rows(i)("BRRPPYearColor")
                    End If

                    'BRR PY
                    If IsDBNull(dtDetails.Rows(i)("BRRPYearRating")) = False Then
                        dRow("IsPlanPYRating") = dtDetails.Rows(i)("BRRPYearRating")
                        dRow("DataPFYColor") = dtDetails.Rows(i)("BRRPYearColor")
                    End If
                    If (dBRRNetScore >= dBCMNetScore And dBRRNetScore >= dBANetScore) Then
                        If IsDBNull(dtDetails.Rows(i)("BRRRating")) = False Then
                            dRow("BABRRBCMHighestRiskScore") = dBRRNetScore
                        End If
                    ElseIf (dBCMNetScore >= dBRRNetScore And dBCMNetScore >= dBANetScore) Then
                        If IsDBNull(dtDetails.Rows(i)("BCMRating")) = False Then
                            dRow("BABRRBCMHighestRiskScore") = dBCMNetScore
                        End If
                    ElseIf (dBANetScore >= dBRRNetScore And dBANetScore >= dBCMNetScore) Then
                        If IsDBNull(dtDetails.Rows(i)("BARating")) = False Then
                            dRow("BABRRBCMHighestRiskScore") = dBANetScore
                        End If
                    End If

                    dRow("BranchResidualRiskRating") = "" : dRow("BranchResidualRiskRatingColor") = ""
                    If IsDBNull(dtDetails.Rows(i)("CMAR_Desc")) = False Then
                        dRow("BranchResidualRiskRating") = dtDetails.Rows(i)("CMAR_Desc")
                        dRow("BranchResidualRiskRatingColor") = dtDetails.Rows(i)("CMAR_Color")
                    End If
                    dRow("IsAnnualPlanned") = 0

                    If IsDBNull(dtDetails.Rows(i)("IsPlanCY")) = False Then
                        If dtDetails.Rows(i)("IsPlanCY") = 1 Then
                            dRow("IsPlanCY") = "YES"
                            dRow("IsAnnualPlanned") = 1
                        ElseIf dtDetails.Rows(i)("IsPlanCY") = 0 Then
                            dRow("IsPlanCY") = "NO"
                        End If
                    End If
                    If dtDetails.Rows(i)("BRRP_Remarks") = "&nbsp:" Then
                        dRow("Remarks") = ""
                    Else
                        dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BRRP_Remarks"))
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBRRPlanningDone(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*) From Risk_BRRPlanning Where BRRP_YearId=" & iYearID & " And BRRP_CustId=" & iCustID & " And BRRP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRPlanning(ByVal sAC As String, ByVal objstrBRRPlanning As strBRR_Planning) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_SalesUnitCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPSalesUnitCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_BranchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPBranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_RegionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPRegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_ZoneID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_RiskScore", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPRiskScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_BRRRating", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPBRRRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_BCMRatingID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPBCMRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_IARatingID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPIARatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_GrossControlScore", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPGrossControlScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_NetScore", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPNetScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_AAPlan", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPAAPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_Remarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrBRRPlanning.sBRRPRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objstrBRRPlanning.sBRRPStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objstrBRRPlanning.sBRRPDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrBRRPlanning.sBRRPIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRP_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrBRRPlanning.iBRRPCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_BRRPlanning", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitBRRPlanning(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer, ByVal iBranchID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_BRRPlanning Set BRRP_Status='S',BRRP_SubmittedBy=" & iUserID & ",BRRP_SubmittedOn=Getdate() Where BRRP_YearId=" & iYearID & " And BRRP_PKID=" & iPKID & " And BRRP_BranchID=" & iBranchID & " And BRRP_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadBRRPlanningDashboardDetailsInReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer = 0
        Dim dBRRNetScore As Double = 0, dBCMNetScore As Double = 0, dBANetScore As Double = 0
        Dim dtRiskMaster As New DataTable
        Dim sStrCurrentYear As String, sStrPreviousYear As String, sStrPPreviousYear As String
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("IRDACode")
            dt.Columns.Add("SalesUnitCode")
            dt.Columns.Add("BranchCode")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("Region")
            dt.Columns.Add("Zone")
            dt.Columns.Add("BranchRiskScore")
            dt.Columns.Add("BranchRiskScoreRating")
            dt.Columns.Add("BRRResidualRiskRating")
            'dt.Columns.Add("BCMResidualRiskRating")
            'dt.Columns.Add("IAResidualRiskRating")
            dt.Columns.Add("NetResidualScore")
            'dt.Columns.Add("BranchResidualRiskRating")
            dt.Columns.Add("IsPlanCYHeader")
            dt.Columns.Add("IsPlanPYHeader")
            dt.Columns.Add("IsPlanPPYHeader")
            dt.Columns.Add("IsPlanPPPYHeader")
            dt.Columns.Add("IsPlanPPPPYHeader")
            dt.Columns.Add("IsPlanCY")
            dt.Columns.Add("IsPlanPY")
            dt.Columns.Add("IsPlanPPY")
            dt.Columns.Add("IsAnnualPlanned")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("IsPlanPPYRating")
            dt.Columns.Add("IsPlanPYRating")

            dtRiskMaster = objclsAllActiveMaster.LoadActiveImpactLikelihoodOESDESWithOutScore(sAC, iACID, iYearID, "", "NO")

            sSql = "Select BA.BRRP_RiskScore,BA.BRRP_GrossControlScore,BA.BRRP_NetScore,BA.BRRP_Remarks,BA.BRRP_BranchID,"
            sSql = sSql & " b.Org_Name As Region,c.Org_Name As Zone,a.Org_Name As Branch,Case When a.Org_Code IS Null Then '' Else a.Org_Code End IRDACode,"
            sSql = sSql & " Case When a.Org_SalesUnitCode IS Null Then '' Else a.Org_SalesUnitCode End SalesUnitCode,Case When a.Org_BranchCode IS Null Then '' Else a.Org_BranchCode End BranchCode,"
            sSql = sSql & " CY.BRRP_AAPlan As IsPlanCY,PY.BRRP_AAPlan As IsPlanPY,PPY.BRRP_AAPlan As IsPlanPPY,"
            sSql = sSql & " d.CMAR_ID,d.CMAR_Desc as BRRRating,e.CMAR_ID,e.CMAR_Desc as BARating,f.CMAR_ID,f.CMAR_Desc as BCMRating,"
            sSql = sSql & " h.GOD_BRRNetScore as BRRScore,h.GOD_BANetScore as BAScore,h.GOD_BCMNetScore as BCMScore,"
            sSql = sSql & " g.CMAR_Desc As BRRPPYearRating, g.CMAR_Color As BRRPPYearColor, m.GOD_BRRNetScore As BRRPPYearScore,"
            sSql = sSql & " p.CMAR_Desc As BRRPYearRating, p.CMAR_Color As BRRPYearColor, k.GOD_BRRNetScore As BRRPYearScore,"
            sSql = sSql & " l.CMAR_Desc as BRRCYRating,l.CMAR_Color as BRRCYColor,z.CMAR_Desc,z.CMAR_Color,z.CMAR_ID as NetRatingID"
            sSql = sSql & " From Risk_BRRPlanning BA"
            sSql = sSql & " Left Join Risk_BRRPlanning CY On CY.BRRP_CustId=" & iCustID & " And CY.BRRP_YearId=" & iYearID & " And CY.BRRP_BranchID=BA.BRRP_BranchID And CY.BRRP_RegionID=BA.BRRP_RegionID And CY.BRRP_ZoneID=BA.BRRP_ZoneID And CY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PY On PY.BRRP_CustId=" & iCustID & " And PY.BRRP_YearId=" & iYearID - 1 & " And PY.BRRP_BranchID=BA.BRRP_BranchID And PY.BRRP_RegionID=BA.BRRP_RegionID And PY.BRRP_ZoneID=BA.BRRP_ZoneID And PY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PPY On PPY.BRRP_CustId=" & iCustID & " And PPY.BRRP_YearId=" & iYearID - 2 & " And PPY.BRRP_BranchID=BA.BRRP_BranchID And PPY.BRRP_RegionID=BA.BRRP_RegionID And PPY.BRRP_ZoneID=BA.BRRP_ZoneID And PPY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure a On a.org_node=BA.BRRP_BranchID And a.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure b On b.org_node=BA.BRRP_RegionID And b.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure c On c.org_node=BA.BRRP_ZoneID And c.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details h On h.GOD_BranchID=BA.BRRP_BranchID And h.GOD_CompID=" & iACID & " And h.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating d On d.CMAR_ID=GOD_BRRNetRatingID And d.CMAR_CompID=" & iACID & " And d.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating e On e.CMAR_ID=GOD_BANetRatingID And e.CMAR_CompID=" & iACID & " And e.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating f On f.CMAR_ID=GOD_BCMNetRatingID And f.CMAR_CompID=" & iACID & " And f.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details m On m.GOD_BranchID=BA.BRRP_BranchID And m.GOD_CompID=" & iACID & " And m.GOD_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join CMARating g On g.CMAR_ID=m.GOD_BRRNetRatingID And g.CMAR_CompID=" & iACID & " And g.CMAR_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details k On k.GOD_BranchID=BA.BRRP_BranchID And k.GOD_CompID=" & iACID & " And k.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating p On p.CMAR_ID=k.GOD_BRRNetRatingID And p.CMAR_CompID=" & iACID & " And p.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating z On z.CMAR_ID=BA.BRRP_NetScore And z.CMAR_CompId=" & iACID & " And (z.CMAR_YearID=" & iYearID - 1 & " Or z.CMAR_YearID=" & iYearID & ")"
            sSql = sSql & " Left Join CMARating l On l.CMAR_ID=BA.BRRP_RiskScore And l.CMAR_CompId=" & iACID & " And l.CMAR_YearID=" & iYearID & ""
            sSql = sSql & " Where BA.BRRP_CustId=" & iCustID & " And BA.BRRP_YearId = " & iYearID & " Order By BA.BRRP_PKID"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    iSlNo = iSlNo + 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = iSlNo
                    If IsDBNull(dtDetails.Rows(i)("IRDACode")) = False Then
                        dRow("IRDACode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("IRDACode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("SalesUnitCode")) = False Then
                        dRow("SalesUnitCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SalesUnitCode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BranchCode")) = False Then
                        dRow("BranchCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BranchCode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Branch")) = False Then
                        dRow("BranchName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Branch"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Region")) = False Then
                        dRow("Region") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Region"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Zone")) = False Then
                        dRow("Zone") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Zone"))
                    End If
                    dRow("BranchRiskScore") = dtDetails.Rows(i)("BRRP_RiskScore")
                    dRow("BranchRiskScoreRating") = ""
                    If dtDetails.Rows(i)("BRRP_RiskScore") > 0 Then
                        dRow("BranchRiskScoreRating") = dtDetails.Rows(i)("BRRCYRating")
                    End If

                    'BRR
                    If IsDBNull(dtDetails.Rows(i)("BRRRating")) = False Then
                        dBRRNetScore = dtDetails.Rows(i)("BRRScore")
                        dRow("BRRResidualRiskRating") = dtDetails.Rows(i)("BRRRating")
                    End If

                    'BCM
                    'If IsDBNull(dtDetails.Rows(i)("BCMRating")) = False Then
                    '    dBCMNetScore = dtDetails.Rows(i)("BCMScore")
                    '    dRow("BCMResidualRiskRating") = dtDetails.Rows(i)("BCMRating")
                    'End If

                    'BIA
                    'If IsDBNull(dtDetails.Rows(i)("BARating")) = False Then
                    '    dBANetScore = dtDetails.Rows(i)("BAScore")
                    '    dRow("IAResidualRiskRating") = dtDetails.Rows(i)("BARating")
                    'End If

                    'BRR PPY
                    If IsDBNull(dtDetails.Rows(i)("BRRPPYearRating")) = False Then
                        dRow("IsPlanPPYRating") = dtDetails.Rows(i)("BRRPPYearRating")
                    End If

                    'BRR PY
                    If IsDBNull(dtDetails.Rows(i)("BRRPYearRating")) = False Then
                        dRow("IsPlanPYRating") = dtDetails.Rows(i)("BRRPYearRating")
                    End If
                    If dtDetails.Rows(i)("BRRP_NetScore") > 0 Then
                        dRow("NetResidualScore") = dtDetails.Rows(i)("BRRP_NetScore")
                    End If
                    'If IsDBNull(dtDetails.Rows(i)("CMAR_Desc")) = False Then
                    '    dRow("BranchResidualRiskRating") = dtDetails.Rows(i)("CMAR_Desc")
                    'End If

                    sStrCurrentYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID))
                    sStrPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 1)
                    sStrPPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 2)
                    dRow("IsPlanCYHeader") = "FY" & sStrCurrentYear & ""
                    dRow("IsPlanPYHeader") = "FY" & sStrPreviousYear & ""
                    dRow("IsPlanPPYHeader") = "FY" & sStrPPreviousYear & ""

                    dRow("IsAnnualPlanned") = "NO"
                    If IsDBNull(dtDetails.Rows(i)("IsPlanCY")) = False Then
                        If dtDetails.Rows(i)("IsPlanCY") = 1 Then
                            dRow("IsPlanCY") = "YES"
                            dRow("IsAnnualPlanned") = "YES"
                        ElseIf dtDetails.Rows(i)("IsPlanCY") = 0 Then
                            dRow("IsPlanCY") = "NO"
                        End If
                    End If
                    dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BRRP_Remarks"))
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRPlanningDashboardInReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer = 0
        Dim dBRRNetScore As Double = 0, dBCMNetScore As Double = 0, dBANetScore As Double = 0
        Dim sStrCurrentYear As String, sStrPreviousYear As String, sStrPPreviousYear As String
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("IRDACode")
            dt.Columns.Add("SalesUnitCode")
            dt.Columns.Add("BranchCode")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("Region")
            dt.Columns.Add("Zone")
            dt.Columns.Add("BranchRiskScore")
            dt.Columns.Add("BranchRiskScoreRating")
            dt.Columns.Add("BRRResidualRiskRating")
            'dt.Columns.Add("BCMResidualRiskRating")
            'dt.Columns.Add("IAResidualRiskRating")
            dt.Columns.Add("NetResidualScore")
            'dt.Columns.Add("BranchResidualRiskRating")
            dt.Columns.Add("IsPlanCY")
            dt.Columns.Add("IsPlanPY")
            dt.Columns.Add("IsPlanPPY")
            dt.Columns.Add("IsPlanCYHeader")
            dt.Columns.Add("IsPlanPYHeader")
            dt.Columns.Add("IsPlanPPYHeader")
            dt.Columns.Add("IsAnnualPlanned")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("IsPlanPPYRating")
            dt.Columns.Add("IsPlanPYRating")

            sSql = "Select Z.Org_Name As Zone, R.Org_Name As Region,B.Org_Name As Branch, B.Org_levelCode,"
            sSql = sSql & " B.Org_Node As BranchID,Case When B.Org_Code IS Null Then '' Else B.Org_Code End IRDACode,"
            sSql = sSql & " Case When B.Org_SalesUnitCode IS Null Then '' Else B.Org_SalesUnitCode End SalesUnitCode,Case When B.Org_BranchCode IS Null Then '' Else B.Org_BranchCode End BranchCode,"
            sSql = sSql & " CY.BRRP_AAPlan As IsPlanCY,PY.BRRP_AAPlan As IsPlanPY,PPY.BRRP_AAPlan As IsPlanPPY,"
            sSql = sSql & " d.CMAR_Desc as BRRRating,e.CMAR_Desc as BARating,f.CMAR_Desc as BCMRating,"
            sSql = sSql & " h.GOD_BRRNetScore as BRRScore,h.GOD_BANetScore as BAScore,h.GOD_BCMNetScore as BCMScore,"
            sSql = sSql & " g.CMAR_Desc As BRRPPYearRating, g.CMAR_Color As BRRPPYearColor, m.GOD_BRRNetScore As BRRPPYearScore,"
            sSql = sSql & " p.CMAR_Desc As BRRPYearRating, p.CMAR_Color As BRRPYearColor, k.GOD_BRRNetScore As BRRPYearScore"
            sSql = sSql & " From sad_org_Structure Z, sad_org_Structure R, sad_org_Structure A, sad_org_Structure B"
            sSql = sSql & " Left Join Risk_BRRPlanning CY On CY.BRRP_CustId=" & iCustID & " And CY.BRRP_YearId=" & iYearID & " And CY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PY On PY.BRRP_CustId=" & iCustID & " And PY.BRRP_YearId=" & iYearID - 1 & " And PY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRPlanning PPY On PPY.BRRP_CustId=" & iCustID & " And PPY.BRRP_YearId=" & iYearID - 2 & " And PPY.BRRP_CompID=" & iACID & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details h On h.GOD_BranchID=B.Org_Node And h.GOD_CompID=" & iACID & " And h.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating d On d.CMAR_ID=GOD_BRRNetRatingID And d.CMAR_CompID=" & iACID & " And d.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating e On e.CMAR_ID=GOD_BANetRatingID And e.CMAR_CompID=" & iACID & " And e.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating f On f.CMAR_ID=GOD_BCMNetRatingID And f.CMAR_CompID=" & iACID & " And f.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details m On m.GOD_BranchID=B.Org_Node And m.GOD_CompID=" & iACID & " And m.GOD_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join CMARating g On g.CMAR_ID=m.GOD_BRRNetRatingID And g.CMAR_CompID=" & iACID & " And g.CMAR_YearID=" & iYearID - 2 & ""
            sSql = sSql & " Left Join GRACe_OverallBranchRating_Details k On k.GOD_BranchID=B.Org_Node And k.GOD_CompID=" & iACID & " And k.GOD_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Left Join CMARating p On p.CMAR_ID=k.GOD_BRRNetRatingID And p.CMAR_CompID=" & iACID & " And p.CMAR_YearID=" & iYearID - 1 & ""
            sSql = sSql & " Where Z.Org_Node=R.Org_Parent And R.Org_Node=A.Org_Parent And A.Org_Node=B.Org_Parent And Z.Org_levelCode>0 "
            sSql = sSql & " And Z.Org_Delflag = 'A' And R.Org_Delflag = 'A' And A.Org_Delflag = 'A' And B.Org_Delflag = 'A'"
            sSql = sSql & " Order by Z.Org_Name, R.Org_Name, A.Org_Name, B.Org_Name"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    iSlNo = iSlNo + 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = iSlNo
                    If IsDBNull(dtDetails.Rows(i)("IRDACode")) = False Then
                        dRow("IRDACode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("IRDACode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("SalesUnitCode")) = False Then
                        dRow("SalesUnitCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SalesUnitCode"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("BranchCode")) = False Then
                        dRow("BranchCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BranchCode"))
                    End If
                    dRow("BranchName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Branch"))
                    dRow("Region") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Region"))
                    dRow("Zone") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Zone"))
                    dRow("BranchRiskScore") = ""
                    dRow("BranchRiskScoreRating") = ""

                    'BRR
                    If IsDBNull(dtDetails.Rows(i)("BRRRating")) = False Then
                        dBRRNetScore = dtDetails.Rows(i)("BRRScore")
                        dRow("BRRResidualRiskRating") = dtDetails.Rows(i)("BRRRating")
                    End If

                    'BCM
                    'If IsDBNull(dtDetails.Rows(i)("BCMRating")) = False Then
                    '    dBCMNetScore = dtDetails.Rows(i)("BCMScore")
                    '    dRow("BCMResidualRiskRating") = dtDetails.Rows(i)("BCMRating")
                    'End If

                    'BIA
                    'If IsDBNull(dtDetails.Rows(i)("BARating")) = False Then
                    '    dBANetScore = dtDetails.Rows(i)("BAScore")
                    '    dRow("IAResidualRiskRating") = dtDetails.Rows(i)("BARating")
                    'End If

                    'BRR PPY
                    If IsDBNull(dtDetails.Rows(i)("BRRPPYearRating")) = False Then
                        dRow("IsPlanPPYRating") = dtDetails.Rows(i)("BRRPPYearRating")
                    End If

                    'BRR PY
                    If IsDBNull(dtDetails.Rows(i)("BRRPYearRating")) = False Then
                        dRow("IsPlanPYRating") = dtDetails.Rows(i)("BRRPYearRating")
                    End If

                    dRow("NetResidualScore") = ""
                    'dRow("BranchResidualRiskRating") = ""

                    sStrCurrentYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID))
                    sStrPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 1)
                    sStrPPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 2)
                    dRow("IsPlanCYHeader") = "FY" & sStrCurrentYear & ""
                    dRow("IsPlanPYHeader") = "FY" & sStrPreviousYear & ""
                    dRow("IsPlanPPYHeader") = "FY" & sStrPPreviousYear & ""
                    dRow("IsAnnualPlanned") = "NO"
                    If IsDBNull(dtDetails.Rows(i)("IsPlanCY")) = False Then
                        If dtDetails.Rows(i)("IsPlanCY") = 1 Then
                            dRow("IsPlanCY") = "YES"
                            dRow("IsAnnualPlanned") = "YES"
                        ElseIf dtDetails.Rows(i)("IsPlanCY") = 0 Then
                            dRow("IsPlanCY") = "NO"
                        End If
                    End If
                    dRow("Remarks") = ""
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
