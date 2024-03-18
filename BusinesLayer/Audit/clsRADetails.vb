Imports DatabaseLayer
Public Structure strRA_Assess
    Dim iRAH_PKID As Integer
    Dim iRAH_RAPKID As Integer
    Dim iRAH_CUSTID As Integer
    Dim iRAH_FUNID As Integer
    Dim iRAH_FinancialYear As Integer
    Dim sRAH_FactorIncrease As String
    Dim sRAH_FactorDecrease As String
    Dim sRAH_ActionPlan As String
    Dim dRAH_TargetDate As Date
    Dim iRAH_CrBy As Integer
    Dim iRAH_CompID As Integer
    Public Property iRAHPKID() As Integer
        Get
            Return (iRAH_PKID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_PKID = Value
        End Set
    End Property
    Public Property iRAHRAPKID() As Integer
        Get
            Return (iRAH_RAPKID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_RAPKID = Value
        End Set
    End Property
    Public Property iRAHCustID() As Integer
        Get
            Return (iRAH_CUSTID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_CUSTID = Value
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
Public Class clsRADetails
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsRiskGeneral As New clsRiskGeneral
    Public Function LoadRADashboardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("Function")
            dt.Columns.Add("NetResidualRiskScoreCY")
            dt.Columns.Add("NetResidualRiskRatingCY")
            dt.Columns.Add("NetResidualRiskColorCY")
            dt.Columns.Add("NetResidualRiskScorePY")
            dt.Columns.Add("NetResidualRiskRatingPY")
            dt.Columns.Add("NetResidualRiskColorPY")

            sSql = "Select Ent_ID,Ent_EntityName,a.RA_Status,a.RA_NetScore As CurrentYearNetScore,b.RA_NetScore As PreviousYearNetScore  From MST_Entity_Master "
            sSql = sSql & " Left join Risk_RA a On a.RA_CustID=" & iCustID & " And a.RA_FunID=Ent_ID And a.RA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And a.RA_CompID=" & iACID & " And a.RA_Status<>'Saved'"
            sSql = sSql & " Left join Risk_RA b On b.RA_CustID=" & iCustID & " And b.RA_FunID=Ent_ID And b.RA_FinancialYear=" & iYearID - 1 & ""
            sSql = sSql & " And b.RA_CompID=" & iACID & " And b.RA_Status<>'Saved'"
            sSql = sSql & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & " order by Ent_EntityName"
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    dRow("FunctionID") = dtdetails.Rows(i)("Ent_ID")
                    dRow("Function") = dtdetails.Rows(i)("Ent_EntityName")
                    If IsDBNull(dtdetails.Rows(i)("CurrentYearNetScore")) = False Then
                        dRow("NetResidualRiskScoreCY") = dtdetails.Rows(i)("CurrentYearNetScore")
                        If dtdetails.Rows(i)("CurrentYearNetScore") > 0 Then
                            dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Desc")
                            dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Color")
                        Else
                            dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                            dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
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
    Public Function LoadRADashboardReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim sSql As String, sStrCurrentYear As String, sStrPreviousYear As String
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("Function")
            dt.Columns.Add("NetResidualRiskScoreCY")
            dt.Columns.Add("NetResidualRiskRatingCY")
            dt.Columns.Add("NetResidualRiskColorCY")
            dt.Columns.Add("NetResidualRiskScorePY")
            dt.Columns.Add("NetResidualRiskRatingPY")
            dt.Columns.Add("NetResidualRiskColorPY")
            dt.Columns.Add("NetRiskCYScoreYear")
            dt.Columns.Add("NetRiskPYScoreYear")
            dt.Columns.Add("NetRiskCYRatingYear")

            sSql = "Select Ent_ID,Ent_EntityName,a.RA_Status,a.RA_NetScore As CurrentYearNetScore,b.RA_NetScore As PreviousYearNetScore  From MST_Entity_Master "
            sSql = sSql & " Left join Risk_RA a On a.RA_CustID=" & iCustID & " And a.RA_FunID=Ent_ID And a.RA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And a.RA_CompID=" & iACID & " And a.RA_Status<>'Saved'"
            sSql = sSql & " Left join Risk_RA b On b.RA_CustID=" & iCustID & " And b.RA_FunID=Ent_ID And b.RA_FinancialYear=" & iYearID - 1 & ""
            sSql = sSql & " And b.RA_CompID=" & iACID & " And b.RA_Status<>'Saved'"
            sSql = sSql & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & " order by Ent_EntityName"
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            sStrCurrentYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID))
            sStrPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 1)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    dRow("NetRiskCYScoreYear") = "FY " & sStrCurrentYear & ""
                    dRow("NetRiskCYRatingYear") = "Net Residual Risk Rating FY " & sStrCurrentYear & ""
                    dRow("NetRiskPYScoreYear") = "FY " & sStrPreviousYear & ""
                    dRow("FunctionID") = dtdetails.Rows(i)("Ent_ID")
                    dRow("Function") = dtdetails.Rows(i)("Ent_EntityName")
                    If IsDBNull(dtdetails.Rows(i)("CurrentYearNetScore")) = False Then
                        dRow("NetResidualRiskScoreCY") = dtdetails.Rows(i)("CurrentYearNetScore")
                        If dtdetails.Rows(i)("CurrentYearNetScore") > 0 Then
                            dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Desc")
                            dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Color")
                        Else
                            dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                            dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
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
    Public Function LoadRASummarySheet(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("RCSANetResidualRiskScoreCY")
            dtTab.Columns.Add("RCSANetResidualRiskRatingCY")
            dtTab.Columns.Add("RCSANetResidualRiskColorCY")
            dtTab.Columns.Add("RCSAStatus")
            dtTab.Columns.Add("RCSANetResidualRiskScorePY")
            dtTab.Columns.Add("RCSANetResidualRiskRatingPY")
            dtTab.Columns.Add("RCSANetResidualRiskColorPY")
            dtTab.Columns.Add("RANetResidualRiskScoreCY")
            dtTab.Columns.Add("RANetResidualRiskRatingCY")
            dtTab.Columns.Add("RANetResidualRiskColorCY")
            dtTab.Columns.Add("RAStatus")
            dtTab.Columns.Add("RANetResidualRiskScorePY")
            dtTab.Columns.Add("RANetResidualRiskRatingPY")
            dtTab.Columns.Add("RANetResidualRiskColorPY")

            sSql = "Select Ent_ID,Ent_EntityName,a.RCSA_Status,a.RCSA_NetScore As RCSACurrentYearNetScore,b.RCSA_NetScore As RCSAPreviousYearNetScore,"
            sSql = sSql & " c.RA_Status,c.RA_NetScore As RACurrentYearNetScore,d.RA_NetScore As RAPreviousYearNetScore From MST_Entity_Master "
            sSql = sSql & " Left join Risk_RCSA a On a.RCSA_CustID = " & iCustID & " And a.RCSA_FunID=Ent_ID And a.RCSA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And a.RCSA_CompID=" & iACID & " And a.RCSA_Status<>'Saved'"
            sSql = sSql & " Left join Risk_RCSA b On b.RCSA_CustID = " & iCustID & " And b.RCSA_FunID=Ent_ID And b.RCSA_FinancialYear=" & iYearID - 1 & ""
            sSql = sSql & " And b.RCSA_CompID=" & iACID & " And b.RCSA_Status<>'Saved'"
            sSql = sSql & " Left join Risk_RA c On c.RA_CustID = " & iCustID & " And c.RA_FunID=Ent_ID And c.RA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And c.RA_CompID=" & iACID & ""
            sSql = sSql & " Left join Risk_RA d On d.RA_CustID = " & iCustID & " And d.RA_FunID=Ent_ID And d.RA_FinancialYear=" & iYearID - 1 & ""
            sSql = sSql & " And d.RA_CompID=" & iACID & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & ""
            sSql = sSql & " order by Ent_EntityName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("Function") = dt.Rows(i)("Ent_EntityName")
                If IsDBNull(dt.Rows(i)("RCSACurrentYearNetScore")) = False Then
                    dr("RCSANetResidualRiskScoreCY") = dt.Rows(i)("RCSACurrentYearNetScore")
                    If dt.Rows(i)("RCSACurrentYearNetScore") > 0 Then
                        dr("RCSANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RCSACurrentYearNetScore"), "Desc")
                        dr("RCSANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RCSACurrentYearNetScore"), "Color")
                    Else
                        dr("RCSANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                        dr("RCSANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                    End If
                End If

                If IsDBNull(dt.Rows(i)("RCSA_Status")) = False Then
                    If (dt.Rows(i)("RCSA_Status") = "Submitted(Risk Team)") Then
                        dr("RCSAStatus") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Saved(Business Team)" Then
                        dr("RCSAStatus") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Submitted(Business Team)" Then
                        dr("RCSAStatus") = "Pending at Risk Team"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Re-Assigned" Then
                        dr("RCSAStatus") = "Re-Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Approved" Then
                        dr("RCSAStatus") = "Completed"
                    End If
                End If

                If IsDBNull(dt.Rows(i)("RCSAPreviousYearNetScore")) = False Then
                    dr("RCSANetResidualRiskScorePY") = dt.Rows(i)("RCSAPreviousYearNetScore")
                    If dt.Rows(i)("RCSAPreviousYearNetScore") > 0 Then
                        dr("RCSANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RCSAPreviousYearNetScore"), "Desc")
                        dr("RCSANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RCSAPreviousYearNetScore"), "Color")
                    Else
                        dr("RCSANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                        dr("RCSANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                    End If
                End If
                If IsDBNull(dt.Rows(i)("RACurrentYearNetScore")) = False Then
                    dr("RANetResidualRiskScoreCY") = dt.Rows(i)("RACurrentYearNetScore")
                    If dt.Rows(i)("RACurrentYearNetScore") > 0 Then
                        dr("RANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RACurrentYearNetScore"), "Desc")
                        dr("RANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RACurrentYearNetScore"), "Color")
                    Else
                        dr("RANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                        dr("RANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                    End If
                End If
                If IsDBNull(dt.Rows(i)("RACurrentYearNetScore")) = False Then
                    If IsDBNull(dt.Rows(i)("RA_Status")) = False Then
                        If dt.Rows(i)("RA_Status") = "Saved" Then
                            dr("RAStatus") = "Assigned"
                        ElseIf dt.Rows(i)("RA_Status") = "Submitted" Then
                            dr("RAStatus") = "Submitted"
                        End If
                    End If
                End If

                If IsDBNull(dt.Rows(i)("RAPreviousYearNetScore")) = False Then
                    dr("RANetResidualRiskScorePY") = dt.Rows(i)("RAPreviousYearNetScore")
                    If dt.Rows(i)("RAPreviousYearNetScore") > 0 Then
                        dr("RANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RAPreviousYearNetScore"), "Desc")
                        dr("RANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RAPreviousYearNetScore"), "Color")
                    Else
                        dr("RANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                        dr("RANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRASummarySheetReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String, sStrCurrentYear As String, sStrPreviousYear As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("RCSANetResidualRiskScoreCY")
            dtTab.Columns.Add("RCSANetResidualRiskRatingCY")
            dtTab.Columns.Add("RCSANetResidualRiskColorCY")
            dtTab.Columns.Add("RCSAStatus")
            dtTab.Columns.Add("RCSANetResidualRiskScorePY")
            dtTab.Columns.Add("RCSANetResidualRiskRatingPY")
            dtTab.Columns.Add("RCSANetResidualRiskColorPY")
            dtTab.Columns.Add("RANetResidualRiskScoreCY")
            dtTab.Columns.Add("RANetResidualRiskRatingCY")
            dtTab.Columns.Add("RANetResidualRiskColorCY")
            dtTab.Columns.Add("RAStatus")
            dtTab.Columns.Add("RANetResidualRiskScorePY")
            dtTab.Columns.Add("RANetResidualRiskRatingPY")
            dtTab.Columns.Add("RANetResidualRiskColorPY")
            dtTab.Columns.Add("NetRiskCY")
            dtTab.Columns.Add("NetRiskPY")
            sSql = "Select Ent_ID,Ent_EntityName,a.RCSA_Status,a.RCSA_NetScore As RCSACurrentYearNetScore,b.RCSA_NetScore As RCSAPreviousYearNetScore,"
            sSql = sSql & " c.RA_Status,c.RA_NetScore As RACurrentYearNetScore,d.RA_NetScore As RAPreviousYearNetScore From MST_Entity_Master "
            sSql = sSql & " Left join Risk_RCSA a On a.RCSA_CustID = " & iCustID & " And a.RCSA_FunID=Ent_ID And a.RCSA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And a.RCSA_CompID=" & iACID & " And a.RCSA_Status<>'Saved'"
            sSql = sSql & " Left join Risk_RCSA b On b.RCSA_CustID = " & iCustID & " And b.RCSA_FunID=Ent_ID And b.RCSA_FinancialYear=" & iYearID - 1 & ""
            sSql = sSql & " And b.RCSA_CompID=" & iACID & " And b.RCSA_Status<>'Saved'"
            sSql = sSql & " Left join Risk_RA c On c.RA_CustID = " & iCustID & " And c.RA_FunID=Ent_ID And c.RA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And c.RA_CompID=" & iACID & "" 'And c.RA_Status='Submitted'"
            sSql = sSql & " Left join Risk_RA d On d.RA_CustID = " & iCustID & " And d.RA_FunID=Ent_ID And d.RA_FinancialYear=" & iYearID - 1 & ""
            sSql = sSql & " And d.RA_CompID=" & iACID & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & ""  'And d.RCSA_Status='Submitted'"
            sSql = sSql & " order by Ent_EntityName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            sStrCurrentYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID))
            sStrPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 1)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("NetRiskCY") = "FY " & sStrCurrentYear & ""
                dr("NetRiskPY") = "FY " & sStrPreviousYear & ""
                dr("Function") = dt.Rows(i)("Ent_EntityName")
                If IsDBNull(dt.Rows(i)("RCSACurrentYearNetScore")) = False Then
                    dr("RCSANetResidualRiskScoreCY") = dt.Rows(i)("RCSACurrentYearNetScore")
                    If dt.Rows(i)("RCSACurrentYearNetScore") > 0 Then
                        dr("RCSANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RCSACurrentYearNetScore"), "Desc")
                        dr("RCSANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RCSACurrentYearNetScore"), "Color")
                    Else
                        dr("RCSANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                        dr("RCSANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                    End If
                End If
                If IsDBNull(dt.Rows(i)("RCSA_Status")) = False Then
                    If (dt.Rows(i)("RCSA_Status") = "Submitted(Risk Team)") Then
                        dr("RCSAStatus") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Saved(Business Team)" Then
                        dr("RCSAStatus") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Submitted(Business Team)" Then
                        dr("RCSAStatus") = "Pending at Risk Team"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Re-Assigned" Then
                        dr("RCSAStatus") = "Re-Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Approved" Then
                        dr("RCSAStatus") = "Completed"
                    End If
                End If
                If IsDBNull(dt.Rows(i)("RCSAPreviousYearNetScore")) = False Then
                    dr("RCSANetResidualRiskScorePY") = dt.Rows(i)("RCSAPreviousYearNetScore")
                    If dt.Rows(i)("RCSAPreviousYearNetScore") > 0 Then
                        dr("RCSANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RCSAPreviousYearNetScore"), "Desc")
                        dr("RCSANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RCSAPreviousYearNetScore"), "Color")
                    Else
                        dr("RCSANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                        dr("RCSANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                    End If
                End If
                If IsDBNull(dt.Rows(i)("RACurrentYearNetScore")) = False Then
                    dr("RANetResidualRiskScoreCY") = dt.Rows(i)("RACurrentYearNetScore")
                    If dt.Rows(i)("RACurrentYearNetScore") > 0 Then
                        dr("RANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RACurrentYearNetScore"), "Desc")
                        dr("RANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("RACurrentYearNetScore"), "Color")
                    Else
                        dr("RANetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                        dr("RANetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                    End If
                End If

                If IsDBNull(dt.Rows(i)("RACurrentYearNetScore")) = False Then
                    If IsDBNull(dt.Rows(i)("RA_Status")) = False Then
                        If dt.Rows(i)("RA_Status") = "Saved" Then
                            dr("RAStatus") = "Assigned"
                        ElseIf dt.Rows(i)("RA_Status") = "Submitted" Then
                            dr("RAStatus") = "Submitted"
                        End If
                    End If
                End If

                If IsDBNull(dt.Rows(i)("RAPreviousYearNetScore")) = False Then
                    dr("RANetResidualRiskScorePY") = dt.Rows(i)("RAPreviousYearNetScore")
                    If dt.Rows(i)("RAPreviousYearNetScore") > 0 Then
                        dr("RANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RAPreviousYearNetScore"), "Desc")
                        dr("RANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("RAPreviousYearNetScore"), "Color")
                    Else
                        dr("RANetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                        dr("RANetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubmittedSubFunctionRA(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable
        Dim sSql As String, sSubFunID As String, sSubFunIDs As String = ""
        Try
            sSubFunID = "Select Distinct(RAD_SEMID) from Risk_RA_Details where RAD_RAPKID in (Select RA_PKID from Risk_RA where RA_CustID =" & iCustID & " And RA_FunID=" & iFunID & " and RA_MasterStatus='Submitted' and RA_CompID=" & iACID & " )"
            dtTab = objDBL.SQLExecuteDataSet(sAC, sSubFunID).Tables(0)
            For i = 0 To dtTab.Rows.Count - 1
                sSubFunIDs = sSubFunIDs & "," & dtTab.Rows(i)("RAD_SEMID")
            Next
            If sSubFunIDs.StartsWith(",") = True Then
                sSubFunIDs = sSubFunIDs.Remove(0, 1)
            End If
            If sSubFunIDs.EndsWith(",") = True Then
                sSubFunIDs = sSubFunIDs.Remove(Len(sSubFunIDs) - 1, 1)
            End If
            If dtTab.Rows.Count > 0 Then
                sSql = "Select SEM_ID As SubFunID,SEM_NAME As SubFunction from MST_SUBENTITY_MASTER Where SEM_Ent_ID=" & iFunID & " AND SEM_DELFLG='A'"
                sSql = sSql & " And SEM_CompID=" & iACID & " And SEM_ID in (" & sSubFunIDs & ") order by SEM_NAME"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndGetRAID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RA_PKID from Risk_RA where RA_FinancialYear=" & iYearID & " And RA_CustID=" & iCustID & " And RA_FunID=" & iFunctionID & " And RA_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckStatusRAID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select case when RA_MasterStatus Is Null Then '' Else  RA_MasterStatus End As RA_MasterStatus from Risk_RA where RA_FinancialYear=" & iYearID & " And RA_CustID=" & iCustID & " And RA_FunID=" & iFunctionID & " And RA_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRAAssessDetails(ByVal sAC As String, ByVal iYearID As Integer, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from Risk_RA where RA_CustID=" & iCustID & " And RA_FunID=" & iFunID & " And RA_FinancialYear=" & iYearID & " And RA_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateRASubmittedStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iRCSAID As Integer, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            If sStatus = "Submitted(Risk Team)" Then
                sSql = "Update Risk_RA Set RA_MasterStatus='Submitted' Where RA_PKID=" & iRCSAID & " And RA_FinancialYear=" & iYearID & " And RA_CompID=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveRAMaster(ByVal sAC As String, ByVal objRCSA As strRCSA_Assess, ByVal iCustomerID As Integer, ByVal iFunctionId As Integer, ByVal sFormType As String, ByVal sYearName As String) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iRCSAParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSAParamCount = 0
            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_PKID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSAPKID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_AsgNo", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iRCSAParamCount).Value = objclsGeneralFunctions.GetAllModuleJobCode(sAC, objRCSA.iRCSACompID, sFormType, objRCSA.iRCSAFinancialYear, sYearName, iCustomerID)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSAFinancialYear
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSACustID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_FunID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSAFunID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_Comments", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.sRCSA_Comments
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_CrBy", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSACrBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_CompID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSACompID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RA_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.sRCSAIPAddress
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_RA", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteRADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iRCSCID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From Risk_RA_Details Where RAD_RAPKID=" & iRCSCID & " And RAD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveRADetails(ByVal sAC As String, ByVal objRCSAD As strRCSA_AssessDetails) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSADParamCount = 0
            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADPKID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_RAPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRCSAPKID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_SEMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADSEMID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_PMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADPMID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_SPMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADSPMID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_RiskID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRiskID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_RiskTypeID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRiskTypeID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_ImpactID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADImpactID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_LikelihoodID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADLikelihoodID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_RiskRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRiskRating
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_ControlID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADControlID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_OES", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADOES
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_DES", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADDES
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_ControlRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADControlRating
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_ChecksID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADChecksID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_ResidualRiskRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADResidualRiskRating
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_Remarks", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.sRCSADRemarks
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.sRCSADIPAddress
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAD_CompID", OleDb.OleDbType.Integer)
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

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_RA_Details", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRAConductGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim iSubProcessKey As Integer, iRiskKey As Integer, iChecksKey As Integer
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("RAPKID")
            dtTab.Columns.Add("RCSAPKID")
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

            sSql = "Select m.RAD_PKID,m.RAD_RAPKID,m.RAD_SEMID,a.SEM_NAME,m.RAD_PMID,b.PM_NAME,m.RAD_SPMID,c.SPM_NAME,c.SPM_Iskey,m.RAD_RiskID,d.MRL_IsKey,d.MRL_RiskName,m.RAD_RiskTypeID,"
            sSql = sSql & " e.RAM_Name As RiskType,m.RAD_ImpactID,m.RAD_LikelihoodID,m.RAD_OES,m.RAD_DES,h.RAM_Name As Impact,i.RAM_Name As Likelihood,"
            sSql = sSql & " j.RAM_Name As OESName,k.RAM_Name As DESName,m.RAD_RiskRating,m.RAD_ControlID,f.MCL_IsKey,f.MCL_ControlName,m.RAD_ControlRating,"
            sSql = sSql & " m.RAD_ChecksID,g.CHK_IsKey,g.CHK_CheckName,m.RAD_ResidualRiskRating,m.RAD_Remarks,"
            sSql = sSql & " RRPYD.RAD_ImpactID As PYImpactID, RRPYD.RAD_LikelihoodID As PYLikelihoodID, RRPYD.RAD_OES As PYOESID, RRPYD.RAD_DES As PYDESID,"
            sSql = sSql & " p.RAM_Name As PYImpact, q.RAM_Name As PYLikelihood, r.RAM_Name As PYOESName, s.RAM_Name As PYDESName,"
            sSql = sSql & " RRPYD.RAD_RiskRating As PYRiskRating, RRPYD.RAD_ControlRating As PYControlRating, RRPYD.RAD_ResidualRiskRating As PYResidualRiskRating"
            sSql = sSql & " From Risk_RA_Details m Left Join Risk_RA RRPY on RRPY.RA_CustID=" & iCustID & " And RRPY.RA_FunID=" & iFunctionID & " And RRPY.RA_FinancialYear=" & iYearID - 1 & " And RRPY.RA_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_RA_Details RRPYD On RRPYD.RAD_RAPKID=RRPY.RA_PKID And m.RAD_SEMID=RRPYD.RAD_SEMID"
            sSql = sSql & " And m.RAD_PMID=RRPYD.RAD_PMID And m.RAD_SPMID=RRPYD.RAD_SPMID And m.RAD_RiskID=RRPYD.RAD_RiskID And m.RAD_ControlID=RRPYD.RAD_ControlID And RRPYD.RAD_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER a On a.SEM_ID=m.RAD_SEMID And SEM_CompID=" & iACID & " Left Join MST_PROCESS_MASTER b on b.PM_ID=m.RAD_PMID And  PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER c On c.SPM_ID=m.RAD_SPMID And SPM_CompID=" & iACID & " Left Join MST_RISK_Library d On d.MRL_PKID=m.RAD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster e On e.RAM_PKID=m.RAD_RiskTypeID And RAM_CompID=" & iACID & " Left Join MST_CONTROL_Library f on f.MCL_PKID=m.RAD_ControlID And MCL_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Checks_Master g On g.CHK_ControlID=m.RAD_ControlID And g.CHK_ID=m.RAD_ChecksID And g.CHK_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster h on h.RAM_Category='RI' And h.RAM_YearID=" & iYearID & " And h.RAM_PKID=m.RAD_ImpactID and h.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster i On i.RAM_Category='RL' And i.RAM_YearID=" & iYearID & " And i.RAM_PKID=m.RAD_LikelihoodID and i.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster j on j.RAM_Category='OES' And j.RAM_YearID=" & iYearID & " And j.RAM_PKID=m.RAD_OES and j.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster k On k.RAM_Category='DES' And k.RAM_YearID=" & iYearID & " And k.RAM_PKID=m.RAD_DES and k.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster p on p.RAM_Category='RI' And p.RAM_YearID=" & iYearID - 1 & " And p.RAM_PKID=RRPYD.RAD_ImpactID and p.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster q On q.RAM_Category='RL' And q.RAM_YearID=" & iYearID - 1 & " And q.RAM_PKID=RRPYD.RAD_LikelihoodID and q.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster r on r.RAM_Category='OES' And r.RAM_YearID=" & iYearID - 1 & " And r.RAM_PKID=RRPYD.RAD_OES and r.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster s On s.RAM_Category='DES' And s.RAM_YearID=" & iYearID - 1 & " And s.RAM_PKID=RRPYD.RAD_DES and s.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where m.RAD_RAPKID In (Select RA_PKID from Risk_RA where RA_FinancialYear=" & iYearID & " And RA_CustID=" & iCustID & " And RA_FunID=" & iFunctionID & " And RA_CompID=" & iACID & ") And m.RAD_CompID=" & iACID & ""
            sSql = sSql & " Order by m.RAD_SEMID, m.RAD_PMID, m.RAD_SPMID, m.RAD_RiskID, m.RAD_ControlID, m.RAD_ChecksID"

            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("RAPKID") = dt.Rows(i)("RAD_PKID")
                dr("RCSAPKID") = dt.Rows(i)("RAD_RAPKID")
                dr("SubFunctionID") = dt.Rows(i)("RAD_SEMID")
                dr("ProcessID") = dt.Rows(i)("RAD_PMID")
                dr("SubProcessID") = dt.Rows(i)("RAD_SPMID")
                dr("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_NAME"))
                dr("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PM_NAME"))
                dr("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SPM_NAME"))
                iSubProcessKey = dt.Rows(i)("SPM_Iskey")
                If iSubProcessKey = 1 Then
                    dr("SubProcessKey") = "KEY"
                Else
                    dr("SubProcessKey") = "NON-KEY"
                End If
                dr("RisKID") = dt.Rows(i)("RAD_RiskID")
                dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MRL_RiskName"))
                dr("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RiskType"))
                iRiskKey = dt.Rows(i)("MRL_IsKey")
                If iRiskKey = 1 Then
                    dr("RiskKey") = "KEY"
                Else
                    dr("RiskKey") = "NON-KEY"
                End If
                dr("ControlID") = dt.Rows(i)("RAD_ControlID")
                dr("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MCL_ControlName"))
                iRiskKey = dt.Rows(i)("MCL_IsKey")
                If iRiskKey = 1 Then
                    dr("ControlKey") = "KEY"
                Else
                    dr("ControlKey") = "NON-KEY"
                End If
                dr("ChecksID") = dt.Rows(i)("RAD_ChecksID")
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
                If IsDBNull(dt.Rows(i)("RAD_ImpactID")) = False Then
                    dr("ImpactID") = dt.Rows(i)("RAD_ImpactID")
                End If
                If IsDBNull(dt.Rows(i)("Impact")) = False Then
                    dr("Impact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Impact"))
                End If
                'Previous Year Impact
                If IsDBNull(dt.Rows(i)("PYImpact")) = False Then
                    dr("PYImpact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYImpact"))
                End If

                If IsDBNull(dt.Rows(i)("RAD_LikelihoodID")) = False Then
                    dr("LikelihoodID") = dt.Rows(i)("RAD_LikelihoodID")
                End If
                If IsDBNull(dt.Rows(i)("Likelihood")) = False Then
                    dr("Likelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Likelihood"))
                End If
                'Previous Year Likelihood
                If IsDBNull(dt.Rows(i)("PYLikelihood")) = False Then
                    dr("PYLikelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYLikelihood"))
                End If

                dr("RiskRating") = "" : dr("RiskRatingColor") = ""
                If IsDBNull(dt.Rows(i)("RAD_RiskRating")) = False Then
                    If dt.Rows(i)("RAD_RiskRating") > 0 Then
                        dr("RiskRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RAD_RiskRating"), "GRS", "Name")
                    Else
                        dr("RiskRating") = ""
                    End If
                    dr("RiskRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RAD_RiskRating"), "GRS", "Color")
                End If
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

                If IsDBNull(dt.Rows(i)("RAD_OES")) = False Then
                    dr("OEID") = dt.Rows(i)("RAD_OES")
                End If
                If IsDBNull(dt.Rows(i)("OESName")) = False Then
                    dr("OE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("OESName"))
                End If
                'Previous Year OESName
                If IsDBNull(dt.Rows(i)("PYOESName")) = False Then
                    dr("PYOESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYOESName"))
                End If

                If IsDBNull(dt.Rows(i)("RAD_DES")) = False Then
                    dr("DEID") = dt.Rows(i)("RAD_DES")
                End If
                If IsDBNull(dt.Rows(i)("DESName")) = False Then
                    dr("DE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("DESName"))
                End If
                'Previous Year DESName
                If IsDBNull(dt.Rows(i)("PYDESName")) = False Then
                    dr("PYDESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYDESName"))
                End If

                If IsDBNull(dt.Rows(i)("RAD_ControlRating")) = False Then
                    If dt.Rows(i)("RAD_ControlRating") > 0 Then
                        dr("ControlRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RAD_ControlRating"), "GCS", "Name")
                    Else
                        dr("ControlRating") = ""
                    End If
                    dr("ControlRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RAD_ControlRating"), "GCS", "Color")
                End If
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
                If IsDBNull(dt.Rows(i)("RAD_ResidualRiskRating")) = False Then
                    If dt.Rows(i)("RAD_RiskRating") > 0 And dt.Rows(i)("RAD_ControlRating") > 0 Then
                        If dt.Rows(i)("RAD_ResidualRiskRating") >= 0 Then
                            dr("ResidualRiskRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RAD_ResidualRiskRating"), "RRS", "Name")
                            dr("ResidualRiskRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RAD_ResidualRiskRating"), "RRS", "Color")
                        Else
                            dr("ResidualRiskRating") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Name")
                            dr("ResidualRiskRatingColor") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                        End If
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
                If IsDBNull(dt.Rows(i)("RAD_Remarks")) = False Then
                    dr("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAD_Remarks"))
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRAOverAllScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iRAPKID As Integer) As Object
        Dim sSql As String, dOverAllScore As Double
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "Select Sum(RAD_ResidualRiskRating) From Risk_RA_Details Where RAD_RAPKID=" & iRAPKID & " And RAD_CompID=" & iACID & ""
            iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "Select Count(*) From Risk_RA_Details Where RAD_RAPKID=" & iRAPKID & " And RAD_CompID=" & iACID & ""
            iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)

            dOverAllScore = iSumOfRCSA / iCount
            Return dOverAllScore
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStatusRAAssgin(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iRAID As Integer, ByVal sComments As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Dim dNetScore As Double
        Try
            dNetScore = Math.Round(GetRAOverAllScore(sAC, iACID, iRAID), 2)
            If sStatus = "Saved" Then
                sSql = "Update Risk_RA Set RA_Comments='" & sComments & "',RA_NetScore=0,RA_Status='Saved',RA_UpdatedBy=" & iUserID & ",RA_UpdatedOn=GetDate() Where RA_PKID=" & iRAID & " And RA_CompID=" & iACID & ""
            ElseIf sStatus = "Submitted" Then
                sSql = "Update Risk_RA Set RA_Comments='" & sComments & "',RA_NetScore=" & dNetScore & ",RA_Status='Submitted',RA_SubmittedBy=" & iUserID & ",RA_SubmittedOn=GetDate() Where RA_PKID=" & iRAID & " And RA_CompID=" & iACID & ""
            ElseIf sStatus = "Approved" Then
                sSql = "Update Risk_RA Set RA_Comments='" & sComments & "',RA_NetScore=" & dNetScore & ",RA_Status='Approved',RA_ApprovedBy=" & iUserID & ",RA_ApprovedOn=GetDate() Where RA_PKID=" & iRAID & " And RA_CompID=" & iACID & ""
            ElseIf sStatus = "Re-Assigned" Then
                sSql = "Update Risk_RA Set RA_Comments='" & sComments & "',RA_NetScore=" & dNetScore & ",RA_Status='Re-Assigned',RA_ReAssignBy=" & iUserID & ",RA_ReAssignOn=GetDate() Where RA_PKID=" & iRAID & " And RA_CompID=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveRAAssginHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iRAAPKID As Integer, ByVal sComments As String, ByVal Status As String, ByVal sIPAddress As String)
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(6) {}
        Dim iRADParamCount As Integer
        Dim Arr(1) As String
        Try
            iRADParamCount = 0
            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAAH_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = 0
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAAH_RAAPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = iRAAPKID
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@AAH_Comments", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iRADParamCount).Value = sComments
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAAH_UserID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = iUserID
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAAH_Status", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRADParamCount).Value = Status
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAAH_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRADParamCount).Value = sIPAddress
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAAH_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = iACID
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "SpRisk_RA_Conduct_History", ObjSFParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetRAAssignHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iRAID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Date")
            dtTab.Columns.Add("User")
            dtTab.Columns.Add("Comments")
            dtTab.Columns.Add("Status")

            sSql = "Select RAAH_Date,RAAH_Comments,Usr_FullName,RAAH_Status from Risk_RA_Conduct_History,Sad_UserDetails Where "
            sSql = sSql & " RAAH_UserID=Usr_ID And RAAH_RAAPKID=" & iRAID & " And RAAH_CompID=" & iACID & " Order by RAAH_PKID Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("RAAH_Date")) = False Then
                    dr("Date") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("RAAH_Date"), "F")
                End If
                dr("User") = dt.Rows(i)("Usr_FullName")
                dr("Comments") = dt.Rows(i)("RAAH_Comments")
                dr("Status") = dt.Rows(i)("RAAH_Status")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRatingSFCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "Select Case when SUM(RAD_ResidualRiskRating)Is null then '' else SUM(RAD_ResidualRiskRating)End As Rating,Count(RAD_SEMID) As Count"
            sSql = sSql & " From Risk_RA_Details Where Rad_RAPKID In (Select RA_PKID From Risk_RA Where RA_CustID=" & iCustID & " And RA_FunID=" & iFunID & ""
            sSql = sSql & " And RA_FinancialYear=" & iYearID & ") And RAD_SEMID=" & iSubFunID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSEMIDFromRADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iRAPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(RAD_SEMID) As RAD_SEMID From Risk_RA_Details Where RAD_RAPKID=" & iRAPKID & " And RAD_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRAActionPlanDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("RAH_PKID")
            dtTab.Columns.Add("RAH_RAPKID")
            dtTab.Columns.Add("FactorsIncreasing")
            dtTab.Columns.Add("FactorsDecreasing")
            dtTab.Columns.Add("ActionPlan")
            dtTab.Columns.Add("TargetDate")
            sSql = "Select RAH_PKID,RAH_RAPKID,RAH_FactorIncrease,RAH_FactorDecrease,RAH_ActionPlan,RAH_TargetDate from Risk_RA_ActionPlan_History "
            sSql = sSql & " where RAH_CustID=" & iCustID & " And RAH_FUNID=" & iFunctionID & " And RAH_FinancialYear=" & iYearID & " And RAH_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("RAH_PKID") = dt.Rows(i)("RAH_PKID")
                    dRow("RAH_RAPKID") = dt.Rows(i)("RAH_RAPKID")
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
    Public Sub SaveRAActionPlanHistory(ByVal sAC As String, ByVal objRA As strRA_Assess)
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iRADParamCount As Integer
        Dim Arr(1) As String
        Try
            iRADParamCount = 0
            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = 0
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_RAPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = objRA.iRAHRAPKID
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_CUSTID", OleDb.OleDbType.Integer, 15)
            ObjSFParam(iRADParamCount).Value = objRA.iRAHCustID
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_FUNID", OleDb.OleDbType.Integer, 15)
            ObjSFParam(iRADParamCount).Value = objRA.iRAHFunID
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_FinancialYear", OleDb.OleDbType.Integer, 15)
            ObjSFParam(iRADParamCount).Value = objRA.iRAHFinancialYear
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_FactorIncrease", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iRADParamCount).Value = objRA.sRAHFactorIncrease
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_FactorDecrease", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iRADParamCount).Value = objRA.sRAHFactorDecrease
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_ActionPlan", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iRADParamCount).Value = objRA.sRAHActionPlan
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_TargetDate", OleDb.OleDbType.Date, 50)
            ObjSFParam(iRADParamCount).Value = objRA.dRAHTargetDate
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = objRA.iRAHCrBy
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            ObjSFParam(iRADParamCount) = New OleDb.OleDbParameter("@RAH_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRADParamCount).Value = objRA.iRAHCompID
            ObjSFParam(iRADParamCount).Direction = ParameterDirection.Input
            iRADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "spRisk_RA_ActionPlan_History", ObjSFParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadRAHeatMap(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sPKID As String) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("Control")

            sSql = "Select RAD_PKID,RAD_RiskID,MRL_RiskName,RAD_ControlID,MCL_ControlName From Risk_RA_Details"
            sSql = sSql & " Left join MST_RISK_Library on MRL_PKID=RAD_RiskID and MRL_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_CONTROL_Library on MCL_PKID=RAD_ControlID and MCL_CompID=" & iACID & ""
            sSql = sSql & " Where RAD_PKID In (" & sPKID & ")"
            sSql = sSql & " And RAD_CompID=" & iACID & " Order by RAD_RiskID,RAD_ControlID"
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
