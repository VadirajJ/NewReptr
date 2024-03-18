Imports DatabaseLayer
Public Structure strBRR_Checklist
    Dim iBRRDPKID As Integer
    Dim iBRRDBRRPKID As Integer
    Dim iBRRDRCMID As Integer
    Dim iBRRDFunctionID As Integer
    Dim iBRRDAreaID As Integer
    Dim iBRRDYESNONA As Integer
    Dim sBRRDIssueDetails As String
    Dim iBRRDRiskScore As Integer
    Dim dBRRDWeightedRiskScore As Double
    Dim dBRRDOWeightage As Double
    Dim sBRRDRefNo As String
    Dim sBRRDCheckPoint As String
    Dim sBRRDRiskCategory As String
    Dim iBRRDSampleSizeID As Integer
    Dim sBRRDSampleSizeName As String
    Dim iBRRDMethodologyID As Integer
    Dim sBRRDMethodology As String
    Dim sBRRDAnnexure As String
    Dim sBRRDStatus As String
    Dim iBRRDAttachID As Integer
    Dim sBRRDIPAddress As String
    Dim iBRRDCompID As Integer
    Dim sBRRDFunType As String
End Structure
Public Class clsBRRChecklist
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function GetBRRMasterCheckCount(ByVal sAC As String, iACID As Integer, ByVal iYearId As Integer) As Integer
        Dim ssql As String
        Try
            ssql = "Select count(*) From Risk_CheckList_Master where RCM_YearId=" & iYearId & " and RCM_CompID=" & iACID & " And RCM_Delflag='A'"
            Return objDBL.SQLExecuteScalarInt(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPlanningBRRAsgNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select BRRS_PKID,BRRS_AsgNo +' ('+ Org_Name + ') 'as AsgNo from Risk_BRRSchedule "
            sSql = sSql & " Left Join sad_org_structure On BRRS_BranchID=Org_Node Where BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & ""
            'If iCheckUserAsgNo > 0 Then
            '    sSql = sSql & " And (BRRS_ZonalMgrID=" & iCheckUserAsgNo & " Or BRRS_BranchMgrID=" & iCheckUserAsgNo & " Or BRRS_EmployeeID=" & iCheckUserAsgNo & ")"
            'End If
            sSql = sSql & " And BRRS_CustID=" & iCustID & " And BRRS_Status='Submitted' "
            sSql = sSql & " order by BRRS_AsgNo"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchCodeNameFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAsgNoID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "Code" Then
                sSql = "Select Org_Code"
            ElseIf sType = "Name" Then
                sSql = "Select Org_Name"
            End If
            sSql = sSql & " From Sad_Org_Structure Where Org_Node In (Select BRRS_BranchID From Risk_BRRSchedule Where BRRS_FinancialYear=" & iYearID & " And BRRS_CustID=" & iCustID & " And BRRS_PKID=" & iAsgNoID & " And BRRS_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmployeeFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAsgNoID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select usr_FullName From Sad_UserDetails Where usr_Id In (Select BRRS_EmployeeID From Risk_BRRSchedule Where BRRS_FinancialYear=" & iYearID & " And BRRS_CustID=" & iCustID & " And BRRS_PKID=" & iAsgNoID & " And BRRS_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchManagerFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAsgNoID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select usr_FullName From Sad_UserDetails Where usr_Id In (Select BRRS_BranchMgrID From Risk_BRRSchedule Where BRRS_FinancialYear=" & iYearID & " And BRRS_CustID=" & iCustID & " And BRRS_PKID=" & iAsgNoID & " And BRRS_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAsgNoID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Org_Name From Sad_Org_Structure Where Org_Node In (Select BRRS_ZoneID From Risk_BRRSchedule Where BRRS_FinancialYear=" & iYearID & " And BRRS_CustID=" & iCustID & " And BRRS_PKID=" & iAsgNoID & " And BRRS_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRCheckListID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRR_PKID From Risk_BRRChecklist_Mas Where BRR_CustID=" & iCustID & " And BRR_AsgID=" & iBRRAsgID & " And BRR_YearID=" & iYearID & " And BRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRCheckListStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As String
        Dim sSql As String, sStatusAndDates As String
        Try
            sSql = "Select BRR_Status + '|' +Convert(Varchar(10),BRR_ASDate,103) + '|' + Convert(Varchar(10),BRR_AEDate,103) From Risk_BRRChecklist_Mas Where BRR_AsgID=" & iBRRAsgID & " And BRR_YearID=" & iYearID & " And BRR_CompID=" & iACID & ""
            sStatusAndDates = objDBL.SQLExecuteScalar(sAC, sSql)
            If IsNothing(sStatusAndDates) = False Then
                Return sStatusAndDates
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOverAllAreaRiskScoreFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer, ByVal iAreaID As Integer) As Object
        Dim sSql As String
        Try
            sSql = "Select Case When Sum(RCM_RiskWeight) Is NULL Then '' else Sum(RCM_RiskWeight) End As RCM_RiskWeight From Risk_CheckList_Master Where RCM_FunctionID=" & iFunID & " And RCM_AreaID=" & iAreaID & " And RCM_yearid=" & iYearID & " and RCM_compid=" & iACID & " And RCM_DelFlag='A'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetReportTitleFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAsgNoID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select BRR_Title From Risk_BRRChecklist_Mas Where BRR_CustID=" & iCustID & " And BRR_YearID=" & iYearID & " And BRR_AsgID=" & iAsgNoID & " And BRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRCoreAndSupportProcessAuditScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iBRRID As Integer, ByVal sFunType As String) As Object
        Dim sSql As String
        Try
            If sFunType = "C" Then
                sSql = "Select Case When Sum(BRRD_WeightedRiskScore) Is NULL then '' else Sum(BRRD_WeightedRiskScore) End As Score from Risk_BRRChecklist_Details where BRRD_BRRPKID=" & iBRRID & " and BRRD_FunType='C' And BRRD_CompID=" & iACID & ""
            ElseIf sFunType = "S" Then
                sSql = "Select Case When Sum(BRRD_WeightedRiskScore) Is NULL then '' else Sum(BRRD_WeightedRiskScore) End As Score from Risk_BRRChecklist_Details where BRRD_BRRPKID=" & iBRRID & " and BRRD_FunType='S' And BRRD_CompID=" & iACID & ""
            Else
                sSql = "Select Case When Sum(BRRD_WeightedRiskScore) Is NULL then '' else Sum(BRRD_WeightedRiskScore) End As Score from Risk_BRRChecklist_Details where BRRD_BRRPKID=" & iBRRID & " And BRRD_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRChecklistAttachID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRR_AttachID From Risk_BRRChecklist_Mas Where BRR_CustID=" & iCustID & " And BRR_AsgID=" & iBRRAsgID & " And BRR_YearID=" & iYearID & " And BRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchManagerIDFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAsgNoID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRRS_BranchMgrID From Risk_BRRSchedule Where BRRS_CustID=" & iCustID & " And BRRS_FinancialYear=" & iYearID & " And BRRS_PKID=" & iAsgNoID & " And BRRS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchIDFromBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAsgNoID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRRS_BranchID From Risk_BRRSchedule Where BRRS_CustID=" & iCustID & " And BRRS_FinancialYear=" & iYearID & " And BRRS_PKID=" & iAsgNoID & " And BRRS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChecksFromBRRCheckList(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As DataTable
        Dim sFunction As String = "", sArea As String = "", sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtMaster As New DataTable
        Dim i As Integer, iSlNo As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("PKID", GetType(Integer))
            dt.Columns.Add("CheckMasterID")
            dt.Columns.Add("Function")
            dt.Columns.Add("Area")
            dt.Columns.Add("OverallWeightage")
            dt.Columns.Add("CheckPointNo")
            dt.Columns.Add("CheckPoints")
            dt.Columns.Add("RiskCategory")
            dt.Columns.Add("RiskCategoryColor")
            dt.Columns.Add("Yes")
            dt.Columns.Add("AuditObservation")
            dt.Columns.Add("Weightage")
            dt.Columns.Add("RiskScore")
            dt.Columns.Add("WeightedRiskScore")
            dt.Columns.Add("Methodology")
            dt.Columns.Add("SampleSize")
            dt.Columns.Add("Annexure")
            dt.Columns.Add("FunID")
            dt.Columns.Add("AreaID")
            dt.Columns.Add("CheckPointNoID")
            dt.Columns.Add("CheckPointsID")
            dt.Columns.Add("RiskCategoryID")
            dt.Columns.Add("WeightageID")
            dt.Columns.Add("MethodolgyID")
            dt.Columns.Add("SampleSizeID")
            dt.Columns.Add("YesNoNAID")
            dt.Columns.Add("FunType")

            sSql = "Select MIM_Color,a.BRRD_PKID,a.BRRD_RCMID,a.BRRD_FunctionID,b.ENT_EntityName As EntityName,a.BRRD_AreaID,c.Cmm_Desc As Area,a.BRRD_RiskCategory,a.BRRD_RiskScore,"
            sSql = sSql & " a.BRRD_OWeightage,a.BRRD_RefNo,a.BRRD_CheckPoint,a.BRRD_MethodologyID,a.BRRD_Methodology,a.BRRD_SampleSizeID,a.BRRD_SampleSizeName,"
            sSql = sSql & " a.BRRD_WeightedRiskScore,a.BRRD_FunType,a.BRRD_Annexure,a.BRRD_IssueDetails,a.BRRD_YESNONA From Risk_BRRChecklist_Details a "
            sSql = sSql & " LEFT outer Join MST_Entity_Master b On b.ENT_ID=a.BRRD_FunctionID And b.ENT_CompID=" & iACID & " "
            sSql = sSql & " LEFT outer Join Content_Management_Master c On c.Cmm_Category='AR' And c.Cmm_ID=a.BRRD_AreaID And c.CMM_CompID=" & iACID & ""
            sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=a.BRRD_RiskCategory And MIM_CompID=" & iACID & ""
            sSql = sSql & " Where a.BRRD_CompID=" & iACID & " And a.BRRD_BRRPKID In (Select BRR_PKID From Risk_BRRChecklist_Mas Where BRR_CustID=" & iCustID & " And BRR_AsgID=" & iBRRAsgID & ""
            sSql = sSql & " And BRR_CompID=" & iACID & ") Order by a.BRRD_RefNo,a.BRRD_FunctionID, a.BRRD_PKID"

            dtMaster = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dtMaster.Rows.Count - 1
                dRow = dt.NewRow
                dRow("Slno") = iSlNo + 1
                If IsDBNull(dtMaster.Rows(i)("BRRD_PKID")) = False Then
                    dRow("PKID") = dtMaster.Rows(i)("BRRD_PKID")
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRD_RCMID")) = False Then 'Check Master PKID
                    dRow("CheckMasterID") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_RCMID"))
                Else
                    dRow("CheckMasterID") = ""
                End If
                If IsDBNull(dtMaster.Rows(i)("EntityName")) = False Then 'Function
                    If sFunction <> dtMaster.Rows(i)("EntityName") Then
                        dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("EntityName"))
                        sFunction = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("EntityName"))
                    End If
                Else
                    dRow("Function") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("Area")) = False Then 'Area
                    If sArea <> dtMaster.Rows(i)("Area") Then
                        dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("Area"))
                        sArea = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("Area"))
                        If IsDBNull(dtMaster.Rows(i)("BRRD_FunctionID")) = False And IsDBNull(dtMaster.Rows(i)("BRRD_AreaID")) = False Then
                            dRow("OverallWeightage") = GetOverAllAreaRiskScoreFromBRR(sAC, iACID, iYearID, dtMaster.Rows(i)("BRRD_FunctionID"), dtMaster.Rows(i)("BRRD_AreaID"))
                        Else
                            dRow("OverallWeightage") = "0"
                        End If
                    End If
                Else
                    dRow("Area") = ""
                    dRow("OverallWeightage") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_RefNo")) = False Then 'Check PointNo
                    dRow("CheckPointNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_RefNo"))
                Else
                    dRow("CheckPointNo") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_CheckPoint")) = False Then 'Check Points
                    dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_CheckPoint"))
                Else
                    dRow("CheckPoints") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_RiskCategory")) = False Then 'Risk category
                    dRow("RiskCategory") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_RiskCategory"))
                    dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("MIM_Color"))
                Else
                    dRow("RiskCategory") = ""
                    dRow("RiskCategoryColor") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_OWeightage")) = False Then 'Weightage
                    dRow("Weightage") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_OWeightage"))
                Else
                    dRow("Weightage") = "0"
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_Methodology")) = False Then 'Methodology
                    dRow("Methodology") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_Methodology"))
                Else
                    dRow("Methodology") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_SampleSizeName")) = False Then 'SampleSize
                    dRow("SampleSize") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_SampleSizeName"))
                Else
                    dRow("SampleSize") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_FunctionID")) = False Then
                    dRow("FunID") = dtMaster.Rows(i)("BRRD_FunctionID")
                Else
                    dRow("FunID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRD_AreaID")) = False Then
                    dRow("AreaID") = dtMaster.Rows(i)("BRRD_AreaID")
                Else
                    dRow("AreaID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRD_MethodologyID")) = False Then
                    dRow("MethodolgyID") = dtMaster.Rows(i)("BRRD_MethodologyID")
                Else
                    dRow("MethodolgyID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRD_SampleSizeID")) = False Then
                    dRow("SampleSizeID") = dtMaster.Rows(i)("BRRD_SampleSizeID")
                Else
                    dRow("SampleSizeID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRD_FunType")) = False Then
                    dRow("FunType") = dtMaster.Rows(i)("BRRD_FunType")
                Else
                    dRow("FunType") = ""
                End If

                dRow("CheckPointNoID") = 1
                dRow("CheckPointsID") = 1
                dRow("RiskCategoryID") = 1
                dRow("WeightageID") = 1

                If IsDBNull(dtMaster.Rows(i)("BRRD_RiskScore")) = False Then
                    dRow("RiskScore") = dtMaster.Rows(i)("BRRD_RiskScore")
                Else
                    dRow("RiskScore") = "0"
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRD_WeightedRiskScore")) = False Then
                    dRow("WeightedRiskScore") = dtMaster.Rows(i)("BRRD_WeightedRiskScore")
                Else
                    dRow("WeightedRiskScore") = "0"
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_YESNONA")) = False Then
                    If dtMaster.Rows(i)("BRRD_YESNONA") = 0 Then
                        dRow("YesNoNAID") = 1
                    Else
                        dRow("YesNoNAID") = dtMaster.Rows(i)("BRRD_YESNONA")
                    End If
                Else
                    dRow("YesNoNAID") = 1
                End If

                If dRow("YesNoNAID") = 1 Then
                    dRow("Yes") = "Yes"
                ElseIf dRow("YesNoNAID") = 2 Then
                    dRow("Yes") = "No"
                ElseIf dRow("YesNoNAID") = 3 Then
                    dRow("Yes") = "NA"
                End If

                If IsDBNull(dtMaster.Rows(i)("BRRD_IssueDetails")) = False Then
                    dRow("AuditObservation") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_IssueDetails"))
                Else
                    dRow("AuditObservation") = ""
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRD_Annexure")) = False Then
                    dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("BRRD_Annexure"))
                Else
                    dRow("Annexure") = ""
                End If

                dt.Rows.Add(dRow)
                iSlNo = iSlNo + 1
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChecksFromMasterDBForBRR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sFunction As String = "", sArea As String = "", sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtMaster As New DataTable
        Dim i As Integer, iSlNo As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("PKID", GetType(Integer))
            dt.Columns.Add("CheckMasterID")
            dt.Columns.Add("Function")
            dt.Columns.Add("Area")
            dt.Columns.Add("OverallWeightage")
            dt.Columns.Add("CheckPointNo")
            dt.Columns.Add("CheckPoints")
            dt.Columns.Add("RiskCategory")
            dt.Columns.Add("RiskCategoryColor")
            dt.Columns.Add("Yes")
            dt.Columns.Add("AuditObservation")
            dt.Columns.Add("Weightage")
            dt.Columns.Add("RiskScore")
            dt.Columns.Add("WeightedRiskScore")
            dt.Columns.Add("Methodology")
            dt.Columns.Add("SampleSize")
            dt.Columns.Add("Annexure")
            dt.Columns.Add("FunID")
            dt.Columns.Add("AreaID")
            dt.Columns.Add("CheckPointNoID")
            dt.Columns.Add("CheckPointsID")
            dt.Columns.Add("RiskCategoryID")
            dt.Columns.Add("WeightageID")
            dt.Columns.Add("MethodolgyID")
            dt.Columns.Add("SampleSizeID")
            dt.Columns.Add("YesNoNAID")
            dt.Columns.Add("FunType")

            sSql = "Select a.RCM_ID,a.RCM_FunctionID As EntityID,b.ENT_EntityName As EntityName,a.RCM_AreaID As AreaID,c.Cmm_Desc As Area,"
            sSql = sSql & " a.RCM_RiskCategory As RiskCategory, a.RCM_RiskWeight As RiskWeight, a.RCM_CheckPointNo As CheckPointNo, a.RCM_CheckPoint As CheckPoints,"
            sSql = sSql & " RCM_MethodologyID As MethodologyID, d.Cmm_Desc As Methodology, RCM_SampleSize As SampleSizeID, e.Cmm_Desc As SampleSize, RCM_FunType As FunType,MIM_Color"
            sSql = sSql & " From Risk_CheckList_Master a Join MST_Entity_Master b On b.ENT_ID=a.RCM_FunctionID And b.ENT_CompID=" & iACID & ""
            sSql = sSql & " LEFT outer Join Content_Management_Master c On c.Cmm_Category='AR' And c.Cmm_ID=a.RCM_AreaID And c.CMM_CompID=" & iACID & ""
            sSql = sSql & " LEFT outer Join Content_Management_Master d On d.Cmm_Category='M' And d.Cmm_ID=a.RCM_MethodologyID And d.CMM_CompID=" & iACID & ""
            sSql = sSql & " LEFT outer Join Content_Management_Master e On e.Cmm_Category='SS' And e.Cmm_ID=a.RCM_SampleSize And e.CMM_CompID=" & iACID & ""
            sSql = sSql & " LEFT outer Join MST_InherentRisk_Master On MIM_Name=a.RCM_RiskCategory And MIM_CompID=" & iACID & ""
            sSql = sSql & " Where a.RCM_CustID=" & iCustID & " And a.RCM_YearID=" & iYearID & " And a.RCM_Delflag='A' And a.RCM_CompID=" & iACID & " Order by CheckPointNo"
            dtMaster = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dtMaster.Rows.Count - 1
                dRow = dt.NewRow
                dRow("Slno") = iSlNo + 1
                If IsDBNull(dtMaster.Rows(i)("RCM_ID")) = False Then
                    dRow("PKID") = dtMaster.Rows(i)("RCM_ID")
                End If
                If IsDBNull(dtMaster.Rows(i)("RCM_ID")) = False Then 'Check Master PKID
                    dRow("CheckMasterID") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("RCM_ID"))
                Else
                    dRow("CheckMasterID") = ""
                End If
                If IsDBNull(dtMaster.Rows(i)("EntityName")) = False Then 'Function
                    If sFunction <> dtMaster.Rows(i)("EntityName") Then
                        dRow("Function") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("EntityName"))
                        sFunction = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("EntityName"))
                    End If
                Else
                    dRow("Function") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("Area")) = False Then 'Area
                    If sArea <> dtMaster.Rows(i)("Area") Then
                        dRow("Area") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("Area"))
                        sArea = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("Area"))
                        If IsDBNull(dtMaster.Rows(i)("EntityID")) = False And IsDBNull(dtMaster.Rows(i)("AreaID")) = False Then
                            dRow("OverallWeightage") = GetOverAllAreaRiskScoreFromBRR(sAC, iACID, iYearID, dtMaster.Rows(i)("EntityID"), dtMaster.Rows(i)("AreaID"))
                        Else
                            dRow("OverallWeightage") = "0"
                        End If
                    End If
                Else
                    dRow("Area") = ""
                    dRow("OverallWeightage") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("CheckPointNo")) = False Then 'Check PointNo
                    dRow("CheckPointNo") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("CheckPointNo"))
                Else
                    dRow("CheckPointNo") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("CheckPoints")) = False Then 'Check Points
                    dRow("CheckPoints") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("CheckPoints"))
                Else
                    dRow("CheckPoints") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("RiskCategory")) = False Then 'Risk category
                    dRow("RiskCategory") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("RiskCategory"))
                    dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("MIM_Color"))
                Else
                    dRow("RiskCategory") = ""
                    dRow("RiskCategoryColor") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("RiskWeight")) = False Then 'Weightage
                    dRow("Weightage") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("RiskWeight"))
                Else
                    dRow("Weightage") = "0"
                End If

                If IsDBNull(dtMaster.Rows(i)("Methodology")) = False Then 'Methodology
                    dRow("Methodology") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("Methodology"))
                Else
                    dRow("Methodology") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("SampleSize")) = False Then 'SampleSize
                    dRow("SampleSize") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("SampleSize"))
                Else
                    dRow("SampleSize") = ""
                End If

                If IsDBNull(dtMaster.Rows(i)("EntityID")) = False Then
                    dRow("FunID") = dtMaster.Rows(i)("EntityID")
                Else
                    dRow("FunID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("AreaID")) = False Then
                    dRow("AreaID") = dtMaster.Rows(i)("AreaID")
                Else
                    dRow("AreaID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("MethodologyID")) = False Then
                    dRow("MethodolgyID") = dtMaster.Rows(i)("MethodologyID")
                Else
                    dRow("MethodolgyID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("SampleSizeID")) = False Then
                    dRow("SampleSizeID") = dtMaster.Rows(i)("SampleSizeID")
                Else
                    dRow("SampleSizeID") = 0
                End If
                If IsDBNull(dtMaster.Rows(i)("FunType")) = False Then
                    dRow("FunType") = dtMaster.Rows(i)("FunType")
                Else
                    dRow("FunType") = ""
                End If

                dRow("CheckPointNoID") = dtMaster.Rows(i)("RCM_ID")
                dRow("CheckPointsID") = dtMaster.Rows(i)("RCM_ID")
                dRow("RiskCategoryID") = dtMaster.Rows(i)("RCM_ID")
                dRow("WeightageID") = dtMaster.Rows(i)("RCM_ID")
                dRow("RiskScore") = "1"
                If IsDBNull(dtMaster.Rows(i)("RiskWeight")) = False Then
                    dRow("WeightedRiskScore") = dRow("RiskScore") * dtMaster.Rows(i)("RiskWeight")
                Else
                    dRow("WeightedRiskScore") = "0"
                End If
                dRow("Yes") = ""
                dRow("YesNoNAID") = 1
                dRow("AuditObservation") = ""
                dRow("Annexure") = ""

                dt.Rows.Add(dRow)
                iSlNo = iSlNo + 1
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRCheckListMaster(ByVal sAC As String, ByVal objstrBRRIssueTracker As strBRR_IssueTracker) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_AsgID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRAsgID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_BranchId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRBranchId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_ASDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.dBRRASDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_AEDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.dBRRAEDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBRRStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBRRFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_Remarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBRRRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_Title", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBRRTitle
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBRRIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRR_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_BRRChecklist_Mas", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRCheckListDetails(ByVal sAC As String, ByVal aArrayList As ArrayList, ByVal iBRRID As Integer) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(24) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Dim objclsBRRChecklist As strBRR_Checklist
        Try
            For i = 0 To aArrayList.Count - 1
                objclsBRRChecklist = aArrayList(i)

                iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_PKID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_BRRPKID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = iBRRID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_RCMID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDRCMID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_FunctionID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDFunctionID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_AreaID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDAreaID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_RefNo", OleDb.OleDbType.VarChar, 8000)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDRefNo
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_CheckPoint", OleDb.OleDbType.VarChar, 8000)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDCheckPoint
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_MethodologyID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDMethodologyID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_Methodology", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDMethodology
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_SampleSizeID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDSampleSizeID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_SampleSizeName", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDSampleSizeName
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_RiskCategory", OleDb.OleDbType.VarChar, 500)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDRiskCategory
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_Status", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDStatus
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_YESNONA", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDYESNONA
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_IssueDetails", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDIssueDetails
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_OWeightage", OleDb.OleDbType.Double, 8)
                ObjParam(iParamCount).Value = objclsBRRChecklist.dBRRDOWeightage
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_RiskScore", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDRiskScore
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_WeightedRiskScore", OleDb.OleDbType.Double, 8)
                ObjParam(iParamCount).Value = objclsBRRChecklist.dBRRDWeightedRiskScore
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_Annexure", OleDb.OleDbType.VarChar, 8000)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDAnnexure
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_FunType", OleDb.OleDbType.VarChar, 1)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDFunType
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_AttachID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDAttachID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_IPAddress", OleDb.OleDbType.VarChar, 20)
                ObjParam(iParamCount).Value = objclsBRRChecklist.sBRRDIPAddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRD_CompID", OleDb.OleDbType.VarChar, 50)
                ObjParam(iParamCount).Value = objclsBRRChecklist.iBRRDCompID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"
                Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_BRRChecklist_Details", 1, Arr, ObjParam)
            Next
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRCheckListToExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtMaster As New DataTable
        Dim i As Integer, iSlNo As Integer = 0
        Try
            dt.Columns.Add("Function")
            dt.Columns.Add("Area")
            dt.Columns.Add("Reference Number")
            dt.Columns.Add("Check Points")
            dt.Columns.Add("Check Procedure")
            dt.Columns.Add("Sample Size")
            dt.Columns.Add("Risk Category")
            dt.Columns.Add("Yes/No/NA")
            dt.Columns.Add("Issue Details")
            dt.Columns.Add("Weightage")
            dt.Columns.Add("Risk Score (1-3)")
            dt.Columns.Add("Net Risk Weighted Score")
            dt.Columns.Add("Annexure No.")

            sSql = "Select a.RCM_ID,a.RCM_YearID As YearID,a.RCM_FunctionID As EntityID,b.ENT_EntityName As EntityName,a.RCM_AreaID As AreaID,c.Cmm_Desc As Area,"
            sSql = sSql & " a.RCM_RiskCategory As RiskCategory, a.RCM_RiskWeight As RiskWeight, a.RCM_CheckPointNo As CheckPointNo, a.RCM_CheckPoint As CheckPoints,"
            sSql = sSql & " RCM_MethodologyID As MethodologyID, d.Cmm_Desc As Methodology, RCM_SampleSize As SampleSizeID, e.Cmm_Desc As SampleSize, RCM_FunType As FunType"
            sSql = sSql & " From Risk_CheckList_Master a Join MST_Entity_Master b On b.ENT_ID=a.RCM_FunctionID And b.ENT_CompID=" & iACID & ""
            sSql = sSql & " LEFT outer Join Content_Management_Master c On c.Cmm_Category='AR' And c.Cmm_ID=a.RCM_AreaID And c.CMM_CompID=" & iACID & ""
            sSql = sSql & " LEFT outer Join Content_Management_Master d On d.Cmm_Category='M' And d.Cmm_ID=a.RCM_MethodologyID And d.CMM_CompID=" & iACID & ""
            sSql = sSql & " LEFT outer Join Content_Management_Master e On e.Cmm_Category='SS' And e.Cmm_ID=a.RCM_SampleSize And e.CMM_CompID=" & iACID & ""
            sSql = sSql & " Where a.RCM_YearID=" & iYearID & " And a.RCM_Delflag='A' And a.RCM_CompID=" & iACID & " Order by CheckPointNo"
            dtMaster = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dtMaster.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dtMaster.Rows(i)("EntityName")) = False Then 'Function
                    dRow("Function") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("EntityName"))
                End If

                If IsDBNull(dtMaster.Rows(i)("Area")) = False Then 'Area
                    dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("Area"))
                End If

                If IsDBNull(dtMaster.Rows(i)("CheckPointNo")) = False Then 'Check PointNo
                    dRow("Reference Number") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("CheckPointNo"))
                End If

                If IsDBNull(dtMaster.Rows(i)("CheckPoints")) = False Then 'Check Points
                    dRow("Check Points") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("CheckPoints"))
                End If

                If IsDBNull(dtMaster.Rows(i)("Methodology")) = False Then 'Methodology
                    dRow("Check Procedure") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("Methodology"))
                End If

                If IsDBNull(dtMaster.Rows(i)("SampleSize")) = False Then 'SampleSize
                    dRow("Sample Size") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("SampleSize"))
                End If

                If IsDBNull(dtMaster.Rows(i)("RiskCategory")) = False Then 'Risk category
                    dRow("Risk Category") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("RiskCategory"))
                End If
                dRow("Yes/No/NA") = ""

                dRow("Issue Details") = ""

                If IsDBNull(dtMaster.Rows(i)("RiskWeight")) = False Then 'Weightage
                    dRow("Weightage") = objclsGRACeGeneral.ReplaceSafeSQL(dtMaster.Rows(i)("RiskWeight"))
                Else
                    dRow("Weightage") = "0"
                End If

                dRow("Risk Score (1-3)") = ""
                If IsDBNull(dtMaster.Rows(i)("RiskWeight")) = False Then
                    If dRow("Risk Score (1-3)") <> "" Then
                        dRow("Net Risk Weighted Score") = dRow("Risk Score (1-3)") * dtMaster.Rows(i)("RiskWeight")
                    Else
                        dRow("Net Risk Weighted Score") = "0"
                    End If
                End If
                dRow("Annexure No.") = ""
                dt.Rows.Add(dRow)
                iSlNo = iSlNo + 1
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRChecklistPGEDetailsID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRR_PGEDetailId From Risk_BRRChecklist_Mas Where BRR_CustID=" & iCustID & " And BRR_AsgID=" & iBRRAsgID & " And BRR_YearID=" & iYearID & " And BRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRR_PGEDetailId From Risk_BRRChecklist_Mas Where BRR_YearID=" & iYearID & " And"
            If iAuditID > 0 Then
                sSql = sSql & " BRR_AsgID=" & iAuditID & " And"
            End If
            sSql = sSql & " BRR_CustID=" & iCustID & " And BRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_BRRChecklist_Mas Set BRR_AttachID=" & iAttachID & ",BRR_PGEDetailId=" & iPGDetailID & " Where "
            If iAuditID > 0 Then
                sSql = sSql & " BRR_AsgID=" & iAuditID & " And"
            End If
            sSql = sSql & " BRR_YearID=" & iYearID & " And BRR_CustID=" & iCustID & " And BRR_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
