Imports DatabaseLayer
Public Structure str_KIRTracker
    Dim iPKID As Integer
    Dim sTraceRefNo As String
    Dim sRiskActionable As String
    Dim dAssignmentDate As Date
    Dim sMonth As String
    Dim sEmail As String
    Dim sTrigger As String
    Dim sCaseSummary As String
    Dim sEntityInv As String
    Dim sAdvisorCode As String
    Dim sAdvisorName As String
    Dim iEmpCode As Integer
    Dim sEmpName As String
    Dim sChannel As String
    Dim sCaseClassification As String
    Dim iRiskType As Integer
    Dim sFraudReptdStage As String
    Dim sContractNo As String
    Dim sActualLoss As String
    Dim sNotionalLoss As String
    Dim sLossAmtRecvd As String
    Dim dAsgnDate As Date
    Dim sInvOutcome As String
    Dim sInvSummary As String
    Dim dClosureDate As Date
    Dim sClosureDays As String
    Dim dCauseInitiationDate As Date
    Dim sPreDispAction As String
    Dim sActionAgainstInter As String
    Dim sActionAgainstEmp As String
    Dim sNoActionRsn As String
    Dim sMatrixAction As String
    Dim sDeviationRsn As String
    Dim dZEDCDate As Date
    Dim dCEDCDate As Date
    Dim sKIRStatus As String
    Dim dFIRfrwdDate As Date
    Dim sLawName As String
    Dim sPreventiveStep As String
    Dim sRCAstatus As String
    Dim sRCAName As String
    Dim sCustName As String
    Dim iZone As Integer
    Dim sSMCode As String
    Dim sSMName As String
    Dim iRegion As Integer
    Dim sLocation As String
    Dim sPlan As String
    Dim sTerm As String
    Dim dLoginDate As Date
    Dim dIssuanceDate As Date
    Dim sPremium As String
    Dim sSumAssured As String
    Dim sBusinessSegment As String
    Dim sZCAR As String
    Dim sDelFlag As String
    Dim sSTATUS As String
    Dim sCrBy As String
    Dim dCrOn As Date
    Dim sIPAddress As String
    Dim iYearID As Integer
    Dim iCompID As Integer
End Structure
Public Class clsKIR
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadKIRTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Try
            dt.Columns.Add("Slno")
            dt.Columns.Add("TraceRefNo")
            dt.Columns.Add("RiskActionable")
            dt.Columns.Add("DateRiskAsgnmnt")
            dt.Columns.Add("Month")
            dt.Columns.Add("Email")
            dt.Columns.Add("Trigger")
            dt.Columns.Add("BreifSumury")
            dt.Columns.Add("InvolmentofEntity")
            dt.Columns.Add("AdCode")
            dt.Columns.Add("AdName")
            dt.Columns.Add("EmpCode")
            dt.Columns.Add("EmpName")
            dt.Columns.Add("Channel")
            dt.Columns.Add("ClasOfCases")
            dt.Columns.Add("RiskType")
            dt.Columns.Add("FraudRptd")
            dt.Columns.Add("ContractNumber")
            dt.Columns.Add("ActualLoss")
            dt.Columns.Add("NationalLoss")
            dt.Columns.Add("LossAmtRecvrd")
            dt.Columns.Add("DateOfAsgnmnt")
            dt.Columns.Add("InvOutcome")
            dt.Columns.Add("Summary")
            dt.Columns.Add("ClosureDate")
            dt.Columns.Add("DaysClouser")
            dt.Columns.Add("DateCauseIniation")
            dt.Columns.Add("PreDesAction")
            dt.Columns.Add("ActiontakenInterm")
            dt.Columns.Add("ActionTakenEmp")
            dt.Columns.Add("ReasonNoAction")
            dt.Columns.Add("ActionMatrix")
            dt.Columns.Add("Deviation")
            dt.Columns.Add("ZEDCdate")
            dt.Columns.Add("CEDCforDate")
            dt.Columns.Add("status")
            dt.Columns.Add("DateofFIR")
            dt.Columns.Add("NameofLaw")
            dt.Columns.Add("PreventiveStep")
            dt.Columns.Add("RCAStatus")
            dt.Columns.Add("RCARName")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("Zone")
            dt.Columns.Add("SMCode")
            dt.Columns.Add("SMName")
            dt.Columns.Add("Region")
            dt.Columns.Add("Location")
            dt.Columns.Add("Plan")
            dt.Columns.Add("Term")
            dt.Columns.Add("LoginDate")
            dt.Columns.Add("PolicyDate")
            dt.Columns.Add("Premiun")
            dt.Columns.Add("SumAssured")
            dt.Columns.Add("BusinessSegmnt")
            dt.Columns.Add("ZCAR")

            sSql = "Select KIR_Pkid,KIR_TraceRefNo,KIR_RiskActionable,KIR_AssignmentDate,KIR_Month,KIR_Email,KIR_Trigger,KIR_CaseSummary,KIR_EntityInv,KIR_AdvisorCode,"
            sSql = sSql & " KIR_AdvisorName,KIR_EmpName,KIR_Channel,KIR_CaseClassification,KIR_FraudReptdStage,KIR_ContractNo,KIR_ActualLoss,KIR_NotionalLoss,KIR_LossAmtRecvd,"
            sSql = sSql & " KIR_AsgnDate,KIR_InvOutcome,KIR_InvSummary,KIR_ClosureDate,KIR_ClosureDays,KIR_CauseInitiationDate ,KIR_PreDispAction,KIR_ActionAgainstInter,"
            sSql = sSql & " KIR_ActionAgainstEmp,KIR_NoActionRsn,KIR_MatrixAction,KIR_DeviationRsn,KIR_ZEDCDate,KIR_CEDCDate,KIR_KIRStatus,KIR_FIRfrwdDate,KIR_LawName,KIR_PreventiveStep,"
            sSql = sSql & " KIR_RCAstatus,KIR_RCAName,KIR_CustName,KIR_SMCode,KIR_SMName,KIR_Location,KIR_Plan,KIR_Term,KIR_LoginDate, KIR_Term,KIR_IssuanceDate,KIR_Premium,KIR_SumAssured,"
            sSql = sSql & " KIR_BusinessSegment,KIR_ZCAR,KIR_YearID,KIR_CompID,KIR_EmpCode,b.usr_Code,KIR_RiskType,RAM_Name,KIR_Zone,d.org_name Zone,KIR_Region,e.org_name from Risk_KIR"
            sSql = sSql & " left Join sad_userdetails b on KIR_EmpCode=b.usr_Id And b.Usr_CompID=" & iACID & ""
            sSql = sSql & " left Join Risk_GeneralMaster c On KIR_RiskType=c.RAM_PKID And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " left Join sad_org_Structure d on KIR_Zone=d.org_node And d.Org_CompID=" & iACID & ""
            sSql = sSql & " left Join sad_org_Structure e On KIR_Region=e.org_node And e.Org_CompID=" & iACID & " where KIR_YearID=" & iYearId & " And KIR_CompID=" & iACID & ""
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("KIR_TraceRefNo")) = False Then
                    dRow("Slno") = i + 1
                    dRow("TraceRefNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_TraceRefNo"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_RiskActionable")) = False Then
                    dRow("RiskActionable") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_RiskActionable"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_AssignmentDate")) = False Then
                    dRow("DateRiskAsgnmnt") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_AssignmentDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Month")) = False Then
                    dRow("Month") = dtDetails.Rows(i)("KIR_Month")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Email")) = False Then
                    dRow("Email") = dtDetails.Rows(i)("KIR_Email")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Trigger")) = False Then
                    dRow("Trigger") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_Trigger"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_CaseSummary")) = False Then
                    dRow("BreifSumury") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_CaseSummary"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_EntityInv")) = False Then
                    dRow("InvolmentofEntity") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_EntityInv"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_AdvisorCode")) = False Then
                    dRow("AdCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_AdvisorCode"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_AdvisorName")) = False Then
                    dRow("AdName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_AdvisorName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_EmpCode")) = False Then
                    dRow("EmpCode") = dtDetails.Rows(i)("usr_Code")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_EmpName")) = False Then
                    dRow("EmpName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_EmpName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Channel")) = False Then
                    dRow("Channel") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_Channel"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_CaseClassification")) = False Then
                    dRow("ClasOfCases") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_CaseClassification"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_RiskType")) = False Then
                    dRow("RiskType") = dtDetails.Rows(i)("RAM_Name")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_FraudReptdStage")) = False Then
                    dRow("FraudRptd") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_FraudReptdStage"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ContractNo")) = False Then
                    dRow("ContractNumber") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_ContractNo"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ActualLoss")) = False Then
                    dRow("ActualLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_ActualLoss"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_NotionalLoss")) = False Then
                    dRow("NationalLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_NotionalLoss"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_LossAmtRecvd")) = False Then
                    dRow("LossAmtRecvrd") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_LossAmtRecvd"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_AsgnDate")) = False Then
                    dRow("DateOfAsgnmnt") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_AsgnDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_InvOutcome")) = False Then
                    dRow("InvOutcome") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_InvOutcome"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_InvSummary")) = False Then
                    dRow("Summary") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_InvSummary"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ClosureDate")) = False Then
                    dRow("ClosureDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_ClosureDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ClosureDays")) = False Then
                    dRow("DaysClouser") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_ClosureDays"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_CauseInitiationDate")) = False Then
                    dRow("DateCauseIniation") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_CauseInitiationDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_PreDispAction")) = False Then
                    dRow("PreDesAction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_PreDispAction"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ActionAgainstInter")) = False Then
                    dRow("ActiontakenInterm") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_ActionAgainstInter"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ActionAgainstEmp")) = False Then
                    dRow("ActionTakenEmp") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_ActionAgainstEmp"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_NoActionRsn")) = False Then
                    dRow("ReasonNoAction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_NoActionRsn"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_MatrixAction")) = False Then
                    dRow("ActionMatrix") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_MatrixAction"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_DeviationRsn")) = False Then
                    dRow("Deviation") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_DeviationRsn"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ZEDCDate")) = False Then
                    dRow("ZEDCdate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_ZEDCDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_CEDCDate")) = False Then
                    dRow("CEDCforDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_CEDCDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_KIRStatus")) = False Then
                    dRow("status") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_KIRStatus"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_FIRfrwdDate")) = False Then
                    dRow("DateofFIR") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_FIRfrwdDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_LawName")) = False Then
                    dRow("NameofLaw") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_LawName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_PreventiveStep")) = False Then
                    dRow("PreventiveStep") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_PreventiveStep"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_RCAstatus")) = False Then
                    dRow("RCAStatus") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_RCAStatus"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_RCAName")) = False Then
                    dRow("RCARName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_RCAName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_CustName")) = False Then
                    dRow("CustomerName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_CustName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Zone")) = False Then
                    dRow("Zone") = dtDetails.Rows(i)("Zone")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_SMCode")) = False Then
                    dRow("SMCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_SMCode"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_SMName")) = False Then
                    dRow("SMName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_SMName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Region")) = False Then
                    dRow("Region") = dtDetails.Rows(i)("org_name")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Location")) = False Then
                    dRow("Location") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_Location"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Plan")) = False Then
                    dRow("Plan") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_Plan"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Term")) = False Then
                    dRow("Term") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_Term"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_LoginDate")) = False Then
                    dRow("LoginDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_LoginDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_IssuanceDate")) = False Then
                    dRow("PolicyDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KIR_IssuanceDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_Premium")) = False Then
                    dRow("Premiun") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_Premium"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_SumAssured")) = False Then
                    dRow("SumAssured") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_SumAssured"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_BusinessSegment")) = False Then
                    dRow("BusinessSegmnt") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_BusinessSegment"))
                End If
                If IsDBNull(dtDetails.Rows(i)("KIR_ZCAR")) = False Then
                    dRow("ZCAR") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KIR_ZCAR"))
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveKIR(ByVal sAC As String, ByVal objKIR As str_KIRTracker) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(62) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Pkid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKIR.iPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_TraceRefNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sTraceRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_RiskActionable", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sRiskActionable
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_AssignmentDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dAssignmentDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Month", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sMonth
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Email", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sEmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Trigger", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sTrigger
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_CaseSummary", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objKIR.sCaseSummary
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_EntityInv", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sEntityInv
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_AdvisorCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sAdvisorCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_AdvisorName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sAdvisorName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_EmpCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKIR.iEmpCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_EmpName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sEmpName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Channel", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sChannel
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_CaseClassification", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sCaseClassification
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_RiskType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKIR.iRiskType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_FraudReptdStage", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sFraudReptdStage
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ContractNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sContractNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ActualLoss", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sActualLoss
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_NotionalLoss", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sNotionalLoss
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_LossAmtRecvd", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sLossAmtRecvd
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_AsgnDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dAsgnDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_InvOutcome", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sInvOutcome
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_InvSummary", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objKIR.sInvSummary
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ClosureDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dClosureDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ClosureDays", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sClosureDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_CauseInitiationDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dCauseInitiationDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_PreDispAction", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sPreDispAction
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ActionAgainstInter", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sActionAgainstInter
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ActionAgainstEmp", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sActionAgainstEmp
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_NoActionRsn", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objKIR.sNoActionRsn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_MatrixAction", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objKIR.sMatrixAction
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_DeviationRsn", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objKIR.sDeviationRsn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ZEDCDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dZEDCDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_CEDCDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dCEDCDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_KIRStatus", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sKIRStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_FIRfrwdDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dFIRfrwdDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_LawName", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sLawName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_PreventiveStep", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sPreventiveStep
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_RCAstatus", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sRCAstatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_RCAName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sRCAName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_CustName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sCustName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Zone", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKIR.iZone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_SMCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sSMCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_SMName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sSMName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Region", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKIR.iRegion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Location", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sLocation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Plan", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objKIR.sPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Term", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sTerm
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_LoginDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dLoginDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_IssuanceDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objKIR.dIssuanceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_Premium", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sPremium
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_SumAssured", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sSumAssured
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_BusinessSegment", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sBusinessSegment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_ZCAR", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKIR.sZCAR
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_DelFlag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objKIR.sDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_STATUS", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objKIR.sSTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_CrBy", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objKIR.sCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_IPAddress", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objKIR.sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKIR.iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KIR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKIR.iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_KIR", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
