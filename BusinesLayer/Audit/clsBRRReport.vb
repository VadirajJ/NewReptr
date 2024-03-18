Imports DatabaseLayer
Public Structure strBRR_Report
    Dim iBRRR_Pkid As Integer
    Dim iBRRR_CustID As Integer
    Dim iBRRR_AsgID As Integer
    Dim iBRRR_BBRITID As Integer
    Dim iBRRR_BRRDID As Integer
    Dim iBRRR_BranchId As Integer
    Dim iBRRR_FunctionID As Integer
    Dim iBRRR_AreaID As Integer
    Dim iBRRR_IssuAgreed As Integer
    Dim dBRRR_ActionPlanDate As Date
    Dim sBRRR_DisAgreedRsn As String
    Dim iBRRR_IssuStatus As Integer
    Dim dBRRR_ClosingDate As Date
    Dim sBRRR_Status As String
    Dim sBRRR_DelFlag As String
    Dim sBRRR_CreatedBy As String
    Dim sBRRR_UpdatedBy As String
    Dim sBRRR_IPAddress As String
    Dim sBRRR_YearID As String
    Dim iBRRR_CompID As Integer
    Dim iBRRR_AttachID As Integer
End Structure
Public Class clsBRRReport
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadITBRRAsgNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select distinct(BRRS_PKID),BRRS_AsgNo +' ('+ Org_Name + ') 'as AsgNo from Risk_BRRIssueTracker join Risk_BRRSchedule on BBRIT_AsgNo=BRRS_PKID And BRRS_CustID=" & iCustID & ""
            sSql = sSql & " Left Join sad_org_structure On BBRIT_BranchId=Org_Node"
            sSql = sSql & " where BBRIT_CustID=" & iCustID & " And BBRIT_Status='S' and BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & " order by BRRS_PKID Desc"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As DataTable
        Dim dRow As DataRow
        Dim dtTable As New DataTable, dt As New DataTable
        Dim sSql As String
        Dim i As Integer
        Try
            dtTable.Columns.Add("SlNo")
            dtTable.Columns.Add("BRRDPKID")
            dtTable.Columns.Add("BBRITPKID")
            dtTable.Columns.Add("Mngr")
            dtTable.Columns.Add("Function")
            dtTable.Columns.Add("FunctionID")
            dtTable.Columns.Add("Area")
            dtTable.Columns.Add("AreaID")
            dtTable.Columns.Add("IssueDetails")
            dtTable.Columns.Add("Annexure")
            dtTable.Columns.Add("RiskCategory")
            dtTable.Columns.Add("InherentRiskColor")
            dtTable.Columns.Add("IssueAgreedID")
            dtTable.Columns.Add("IssueAgreed")
            dtTable.Columns.Add("ActionPlanDate")
            dtTable.Columns.Add("Reason")
            dtTable.Columns.Add("IOpenCloseID")
            dtTable.Columns.Add("IOpenClose")
            dtTable.Columns.Add("ReportStatus")
            dtTable.Columns.Add("ProbableDate")

            sSql = " Select BRRR_BBRITID,BRRR_BRRDID,BRRR_IssuAgreed,convert(char(10),BRRR_ActionPlanDate,103) BRRR_ActionPlanDate,"
            sSql = sSql & " BRRR_DisAgreedRsn,BRRR_IssuStatus,convert(Char(10),BRRR_ClosingDate,103) BRRR_ClosingDate ,BRRR_Status,BRRD_FunctionID,BRRD_AreaID,BRRD_CheckPoint,"
            sSql = sSql & " BRRD_IssueDetails, BRRD_Annexure,BRRD_RiskCategory,BRRD_RiskCategory, e.BRRS_AsgNo,e.BRRS_BranchMgrID,f.usr_FullName,BRRR_Status,ENT_ENTITYNAME,cmm_Desc From Risk_BRRReport"
            sSql = sSql & " Join Risk_BRRChecklist_Details on BRRD_PKID=BRRR_BRRDID And BRRD_CompID=" & iACID & ""
            sSql = sSql & " Join Risk_BRRSchedule e on BRRR_AsgID=e.BRRS_PKID And e.BRRS_CustID=" & iCustID & " And e.BRRS_CompID=" & iACID & " And e.BRRS_FinancialYear=" & iYearID & ""
            sSql = sSql & " Left Join mst_entity_master On ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join content_management_master On cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & ""
            sSql = sSql & " Join sad_userdetails f on e.BRRS_BranchMgrID=f.usr_Id And f.Usr_CompID=" & iACID & ""
            sSql = sSql & " Where BRRR_CustID=" & iCustID & " And BRRR_AsgID=" & iBRRAsgID & " And BRRD_YESNONA=2 And"
            sSql = sSql & " (BRRD_RiskScore=2 Or BRRD_RiskScore=3 Or BRRD_RiskScore=0 Or BRRD_RiskScore=10 Or BRRD_RiskScore=20) And BRRR_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTable.NewRow
                    dRow("BRRDPKID") = dt.Rows(i).Item("BRRR_BRRDID")
                    dRow("BBRITPKID") = dt.Rows(i).Item("BRRR_BBRITID")
                    dRow("SlNo") = i + 1
                    If IsDBNull(dt.Rows(i)("usr_FullName")) = False Then
                        dRow("Mngr") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("usr_FullName"))
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_FunctionID")) = False Then
                        dRow("FunctionID") = dt.Rows(i).Item("BRRD_FunctionID")
                    End If
                    If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
                        dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
                    End If
                    dRow("AreaID") = dt.Rows(i).Item("BRRD_AreaID")
                    If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
                        dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_IssueDetails")) = False Then
                        dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_Annexure")) = False Then
                        dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_RiskCategory")) = False Then
                        dRow("RiskCategory") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
                        dRow("InherentRiskColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, dRow("RiskCategory"))
                    End If
                    dRow("ReportStatus") = dt.Rows(i).Item("BRRR_Status")
                    If dt.Rows(i).Item("BRRR_ActionPlanDate") = "01/01/1900" Then
                        dRow("IssueAgreed") = ""
                    Else
                        dRow("ActionPlanDate") = dt.Rows(i).Item("BRRR_ActionPlanDate")
                    End If
                    dRow("IssueAgreedID") = dt.Rows(i).Item("BRRR_IssuAgreed")
                    If dRow("IssueAgreedID") = 1 Then
                        dRow("IssueAgreed") = "Agreed"
                    Else
                        dRow("IssueAgreed") = "Disagreed"
                    End If
                    dRow("Reason") = dt.Rows(i).Item("BRRR_DisAgreedRsn")
                    dRow("IOpenCloseID") = dt.Rows(i).Item("BRRR_IssuStatus")
                    If dRow("IOpenCloseID") = 1 Then
                        dRow("IOpenClose") = "Closed"
                    Else
                        dRow("IOpenClose") = "Open"
                    End If

                    If dt.Rows(i).Item("BRRR_ClosingDate") = "01/01/1900" Then
                        dRow("ProbableDate") = ""
                    Else
                        dRow("ProbableDate") = dt.Rows(i).Item("BRRR_ClosingDate")
                    End If
                    dtTable.Rows.Add(dRow)
                Next
            End If
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRITReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As DataTable
        Dim dRow As DataRow
        Dim dtTable As New DataTable, dt As New DataTable
        Dim sSql As String
        Dim i As Integer
        Try
            dtTable.Columns.Add("SlNo")
            dtTable.Columns.Add("BRRDPKID")
            dtTable.Columns.Add("BBRITPKID")
            dtTable.Columns.Add("Mngr")
            dtTable.Columns.Add("Function")
            dtTable.Columns.Add("FunctionID")
            dtTable.Columns.Add("Area")
            dtTable.Columns.Add("AreaID")
            dtTable.Columns.Add("CheckPoints")
            dtTable.Columns.Add("CheckPointsID")
            dtTable.Columns.Add("IssueDetails")
            dtTable.Columns.Add("Annexure")
            dtTable.Columns.Add("AAPlan")
            dtTable.Columns.Add("ADate")
            dtTable.Columns.Add("IOpenClose")
            dtTable.Columns.Add("IOpenCloseID")
            dtTable.Columns.Add("RiskCategory")
            dtTable.Columns.Add("InherentRiskColor")
            dtTable.Columns.Add("IssueAgreed")
            dtTable.Columns.Add("IssueAgreedID")
            dtTable.Columns.Add("ActionPlanDate")
            dtTable.Columns.Add("Reason")
            dtTable.Columns.Add("Status")
            dtTable.Columns.Add("ReportStatus")
            dtTable.Columns.Add("ProbableDate")

            sSql = "Select BBRIT_PKID,BBRIT_BRRDPKID,BBRIT_RCMID,BBRIT_AsgNo,BBRIT_BranchId,BBRIT_TargetDate,BRRD_RiskCategory,BRRD_FunctionID,BRRD_AreaID,BRRD_CheckPoint,"
            sSql = sSql & " BRRD_IssueDetails,BRRD_Annexure,BRRD_RiskCategory,BBRIT_ActionPlan,BBRIT_OpenCloseStatus,e.BRRS_AsgNo,e.BRRS_BranchMgrID,f.usr_FullName,"
            sSql = sSql & " ENT_ENTITYNAME,cmm_Desc,RCM_ID From Risk_BRRIssueTracker"
            sSql = sSql & " join Risk_BRRChecklist_Details On BRRD_PKID=BBRIT_BRRDPKID And BRRD_CompID=" & iACID & ""
            sSql = sSql & " join Risk_BRRSchedule e On BBRIT_AsgNo=e.BRRS_PKID And e.BRRS_CustID=" & iCustID & " And e.BRRS_CompID=" & iACID & " And e.BRRS_FinancialYear=" & iYearID & ""
            sSql = sSql & " join sad_userdetails f On e.BRRS_BranchMgrID=f.usr_Id And f.Usr_CompID=" & iACID & ""
            sSql = sSql & " Left Join mst_entity_master On ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join content_management_master On cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_CheckList_Master On RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " And RCM_YearID=" & iYearID & " And RCM_CustID=" & iCustID & ""
            sSql = sSql & " Where BBRIT_CustID=" & iCustID & " And BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_CompID=" & iACID & " And BRRD_YESNONA=2 And"
            sSql = sSql & " (BRRD_RiskScore=2 Or BRRD_RiskScore=3 Or BRRD_RiskScore=0 Or BRRD_RiskScore=10 Or BRRD_RiskScore=20) And BRRD_PKID=BBRIT_BRRDPKID And BBRIT_CompID=" & iACID & ""

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTable.NewRow
                    dRow("BRRDPKID") = dt.Rows(i).Item("BBRIT_BRRDPKID")
                    dRow("BBRITPKID") = dt.Rows(i).Item("BBRIT_PKID")
                    dRow("SlNo") = i + 1
                    If IsDBNull(dt.Rows(i)("usr_FullName")) = False Then
                        dRow("Mngr") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("usr_FullName"))
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_FunctionID")) = False Then
                        dRow("FunctionID") = dt.Rows(i).Item("BRRD_FunctionID")
                    End If
                    If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
                        dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
                    End If
                    dRow("AreaID") = dt.Rows(i).Item("BRRD_AreaID")
                    If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
                        dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
                    End If
                    If IsDBNull(dt.Rows(i)("RCM_ID")) = False Then
                        dRow("CheckPointsID") = dt.Rows(i).Item("RCM_ID")
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_CheckPoint")) = False Then
                        dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_IssueDetails")) = False Then
                        dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
                    End If
                    If IsDBNull(dt.Rows(i)("BRRD_Annexure")) = False Then
                        dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
                    End If
                    dRow("AAPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BBRIT_ActionPlan"))
                    If IsDBNull(dt.Rows(i)("BRRD_RiskCategory")) = False Then
                        dRow("RiskCategory") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
                        dRow("InherentRiskColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, dRow("RiskCategory"))
                    End If
                    dRow("IOpenCloseID") = dt.Rows(i).Item("BBRIT_OpenCloseStatus")
                    dRow("IOpenClose") = dt.Rows(i).Item("BBRIT_OpenCloseStatus")
                    dRow("ADate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "D")
                    dRow("IssueAgreed") = "1"
                    dRow("IssueAgreedID") = "1"
                    dRow("Status") = "0"
                    dRow("ReportStatus") = ""
                    dtTable.Rows.Add(dRow)
                Next
            End If
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRReportStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer, ByVal iYearID As Integer) As String
        Dim ssql As String
        Try
            ssql = "Select Distinct(BRRR_Status) From Risk_BRRReport Where BRRR_CustID=" & iCustID & " And BRRR_AsgID=" & iBRRAsgID & " And BRRR_YearID=" & iYearID & " And BRRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRReport(ByVal sAC As String, ByVal objBRRReport As strBRR_Report) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_Pkid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_Pkid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_AsgID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_AsgID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_BBRITID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_BBRITID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_BRRDID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_BRRDID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_BranchId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_BranchId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_AreaID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_IssuAgreed", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_IssuAgreed
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_ActionPlanDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objBRRReport.dBRRR_ActionPlanDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_DisAgreedRsn", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objBRRReport.sBRRR_DisAgreedRsn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_IssuStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_IssuStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_ClosingDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objBRRReport.dBRRR_ClosingDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objBRRReport.sBRRR_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_DelFlag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objBRRReport.sBRRR_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_CreatedBy", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objBRRReport.sBRRR_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_UpdatedBy", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objBRRReport.sBRRR_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objBRRReport.sBRRR_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_YearID", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objBRRReport.sBRRR_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRR_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBRRReport.iBRRR_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_BRRReport", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadBRRAllBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCheckUserAsgNo As Integer, ByVal iFUNUserID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select distinct(BRRS_PKID),BRRS_AsgNo +' ('+ Org_Name + ') 'as AsgNo from Risk_BRRSchedule Left Join Risk_BRRIssueTracker On BBRIT_AsgNo=BRRS_PKID Left Join sad_org_structure"
    '        sSql = sSql & " On BRRS_BranchID=Org_Node Where BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & " "
    '        If iCheckUserAsgNo > 0 Then
    '            sSql = sSql & " And (BRRS_ZonalMgrID=" & iCheckUserAsgNo & " Or BRRS_BranchMgrID=" & iCheckUserAsgNo & " Or BRRS_EmployeeID=" & iCheckUserAsgNo & ")"
    '        End If
    '        If iFUNUserID > 0 Then
    '            sSql = sSql & " And BRRS_PKID in (Select BRR_AsgID from Risk_BRRChecklist_Mas where BRR_PKID in (Select BRRD_BRRPKID From Risk_BRRChecklist_Details Where BRRD_YESNONA=2 And BRRD_FunctionID in "
    '            sSql = sSql & " (Select ENT_ID From mst_Entity_master Where (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ") and ENT_CompID=" & iACID & ")))"
    '        End If
    '        sSql = sSql & " order by BRRS_PKID"
    '        dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function BindBRRAllIssueTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRRAsgID As Integer, ByVal iStatus As Integer) As DataTable
    '    Dim dRow As DataRow
    '    Dim dtTable As New DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Dim i As Integer
    '    Try
    '        dtTable.Columns.Add("SrNo")
    '        dtTable.Columns.Add("BRRDPKID")
    '        dtTable.Columns.Add("RCMID")
    '        dtTable.Columns.Add("AsgNo")
    '        dtTable.Columns.Add("Function")
    '        dtTable.Columns.Add("FunctionID")
    '        dtTable.Columns.Add("Area")
    '        dtTable.Columns.Add("AreaID")
    '        dtTable.Columns.Add("CheckPoints")
    '        dtTable.Columns.Add("CheckPointsID")
    '        dtTable.Columns.Add("IssueDetails")
    '        dtTable.Columns.Add("Annexure")
    '        dtTable.Columns.Add("Risk")
    '        dtTable.Columns.Add("RiskCategoryColor")
    '        dtTable.Columns.Add("ActionPlan")
    '        dtTable.Columns.Add("TargetDate")
    '        dtTable.Columns.Add("IOpenClose")
    '        dtTable.Columns.Add("SourceofIssue")
    '        dtTable.Columns.Add("RLICEmployeeName")
    '        dtTable.Columns.Add("BranchManager")
    '        dtTable.Columns.Add("Remarks")
    '        dtTable.Columns.Add("IssueTrackerNoID")
    '        dtTable.Columns.Add("AttachID")

    '        If objDBL.SQLCheckForRecord(sAC, "Select BBRIT_PKID from Risk_BRRIssueTracker Where BBRIT_CustID=" & iCustID & " And BBRIT_CompID=" & iACID & "") = True Then
    '            sSql = "Select BBRIT_PKID, BBRIT_BRRDPKID, BBRIT_RCMID, BBRIT_AsgNo, BBRIT_BranchId, BBRIT_TargetDate, BRRD_FunctionID,ENT_ENTITYNAME,BRRD_AreaID, cmm_Desc,RCM_ID,"
    '            sSql = sSql & " BRRD_CheckPoint, BRRD_IssueDetails, BRRD_Annexure,BBRIT_AttchID,BRRD_RiskCategory,MIM_Color,BBRIT_ActionPlan, BBRIT_OpenCloseStatus,BRRS_AsgNo,BRRS_EmployeeID,a.usr_FullName As BranchMgr,"
    '            sSql = sSql & " BRRS_BranchMgrID, b.usr_FullName As RLICEmployee From Risk_BRRIssueTracker left join Risk_BRRChecklist_Details On BRRD_YESNONA=2"
    '            sSql = sSql & " And (BRRD_RiskScore=2 Or BRRD_RiskScore=3 Or BRRD_RiskScore=0  Or BRRD_RiskScore=10 Or BRRD_RiskScore=20) And BRRD_CompID=" & iACID & " And BRRD_PKID=BBRIT_BRRDPKID"
    '            sSql = sSql & " left join mst_entity_master on ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & " left join content_management_master on cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & ""
    '            sSql = sSql & " left join Risk_CheckList_Master on RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " And RCM_YearID=" & iYearID & ""
    '            sSql = sSql & " join sad_userdetails a On BRRS_EmployeeID=a.usr_Id And a.Usr_CompID=" & iACID & " join sad_userdetails b On BRRS_BranchMgrID=b.usr_Id And b.Usr_CompID=" & iACID & ""
    '            sSql = sSql & " Left Join Risk_BRRSchedule On BBRIT_AsgNo=BRRS_PKID And BRRS_CustID=" & iCustID & " And BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & ""
    '            sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=BRRD_RiskCategory And MIM_CompID=" & iACID & ""
    '            sSql = sSql & " Where BBRIT_CustID=" & iCustID & " And BBRIT_CompID=" & iACID & " And BBRIT_FinancialYear=" & iYearID & ""
    '            If iBRRRAsgID > 0 Then
    '                sSql = sSql & " And BBRIT_AsgNo=" & iBRRRAsgID & ""
    '            End If
    '            If iStatus > 0 Then
    '                sSql = sSql & " And BBRIT_OpenCloseStatus=" & iStatus & ""
    '            End If
    '            sSql = sSql & " ORDER BY BBRIT_PKID"
    '            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    dRow = dtTable.NewRow
    '                    dRow("SrNo") = i + 1
    '                    dRow("BRRDPKID") = dt.Rows(i).Item("BBRIT_BRRDPKID")
    '                    dRow("RCMID") = dt.Rows(i).Item("BBRIT_RCMID")
    '                    dRow("AsgNo") = dt.Rows(i).Item("BBRIT_AsgNo")
    '                    dRow("FunctionID") = dt.Rows(i).Item("BRRD_FunctionID")
    '                    dRow("IssueTrackerNoID") = dt.Rows(i).Item("BBRIT_PKID")
    '                    If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
    '                        dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
    '                    End If
    '                    dRow("AreaID") = dt.Rows(i).Item("BRRD_AreaID")
    '                    If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
    '                        dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
    '                    End If
    '                    If IsDBNull(dt.Rows(i)("RCM_ID")) = False Then
    '                        dRow("CheckPointsID") = dt.Rows(i).Item("RCM_ID")
    '                    End If
    '                    If IsDBNull(dt.Rows(i)("BRRD_CheckPoint")) = False Then
    '                        dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
    '                    End If
    '                    If IsDBNull(dt.Rows(i)("BRRD_IssueDetails")) = False Then
    '                        dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
    '                    End If
    '                    If IsDBNull(dt.Rows(i)("BRRD_Annexure")) = False Then
    '                        dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
    '                    End If
    '                    If IsDBNull(dt.Rows(i)("BRRD_RiskCategory")) = False Then
    '                        dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
    '                        dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("MIM_Color"))
    '                    End If
    '                    If IsDBNull(dt.Rows(i)("BBRIT_ActionPlan")) = False Then
    '                        dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BBRIT_ActionPlan"))
    '                    End If
    '                    If IsDBNull(dt.Rows(i)("BBRIT_OpenCloseStatus")) = False Then
    '                        If dt.Rows(i)("BBRIT_OpenCloseStatus") = 1 Then
    '                            dRow("IOpenClose") = "Open"
    '                        ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 2 Then
    '                            dRow("IOpenClose") = "Closed"
    '                        ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 3 Then
    '                            dRow("IOpenClose") = "Open-Not Actioned"
    '                        End If
    '                    End If
    '                    dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "F")
    '                    dRow("SourceofIssue") = "Branch Risk Review"
    '                    dRow("RLICEmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("RLICEmployee"))
    '                    dRow("BranchManager") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BranchMgr"))
    '                    dRow("Remarks") = ""
    '                    If IsDBNull(dt.Rows(i)("BBRIT_AttchID")) = False Then
    '                        dRow("AttachID") = dt.Rows(i)("BBRIT_AttchID")
    '                    End If
    '                    dtTable.Rows.Add(dRow)
    '                Next
    '            End If
    '        End If
    '        Return dtTable
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetBRRReportAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRRAsgID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRRR_AttachID From Risk_BRRReport Where BBRIT_CustID=" & iCustID & " And BRRR_AsgID=" & iBRRRAsgID & " And BRRR_YearID=" & iYearID & " And BRRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function BindBRRAllIssueTrackerDetailsToRiskRegister(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRRAsgID As Integer, ByVal iStatus As Integer, ByVal iSourceofIssue As Integer) As DataTable
    '    Dim dRow As DataRow
    '    Dim dtTable As New DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Dim i As Integer, iAreaID As Integer, iBRRCount As Integer = 0, iBIACount As Integer = 0
    '    Try
    '        dtTable.Columns.Add("SrNo")
    '        dtTable.Columns.Add("BRRDPKID")
    '        dtTable.Columns.Add("RCMID")
    '        dtTable.Columns.Add("AsgNo")
    '        dtTable.Columns.Add("Function")
    '        dtTable.Columns.Add("FunctionID")
    '        dtTable.Columns.Add("Area")
    '        dtTable.Columns.Add("AreaID")
    '        dtTable.Columns.Add("CheckPoints")
    '        dtTable.Columns.Add("CheckPointsID")
    '        dtTable.Columns.Add("IssueDetails")
    '        dtTable.Columns.Add("Annexure")
    '        dtTable.Columns.Add("Risk")
    '        dtTable.Columns.Add("RiskCategoryColor")
    '        dtTable.Columns.Add("ActionPlan")
    '        dtTable.Columns.Add("TargetDate")
    '        dtTable.Columns.Add("IOpenClose")
    '        dtTable.Columns.Add("SourceofIssue")
    '        dtTable.Columns.Add("RLICEmployeeName")
    '        dtTable.Columns.Add("BranchManager")
    '        dtTable.Columns.Add("Remarks")
    '        dtTable.Columns.Add("IssueTrackerNoID")
    '        If iSourceofIssue = "1" Or iSourceofIssue = "0" Then 'BRR
    '            If objDBL.SQLCheckForRecord(sAC, "Select BBRIT_PKID from Risk_BRRIssueTracker Where BBRIT_CompID=" & iACID & "") = True Then
    '                sSql = "Select BBRIT_PKID, BBRIT_BRRDPKID, BBRIT_RCMID, BBRIT_AsgNo, BBRIT_BranchId, BBRIT_TargetDate, BRRD_FunctionID,ENT_ENTITYNAME,BRRD_AreaID, cmm_Desc,RCM_ID,"
    '                sSql = sSql & " BRRD_CheckPoint, BRRD_IssueDetails, BRRD_Annexure, BRRD_RiskCategory,MIM_Color,BBRIT_ActionPlan, BBRIT_OpenCloseStatus,BRRS_AsgNo,BRRS_EmployeeID,"
    '                sSql = sSql & " b.usr_FullName As BranchMgr,BRRS_BranchMgrID, a.usr_FullName As RLICEmployee From Risk_BRRIssueTracker left join Risk_BRRChecklist_Details On BRRD_YESNONA=2"
    '                sSql = sSql & " And (BRRD_RiskScore=2 Or BRRD_RiskScore=3 Or BRRD_RiskScore=0  Or BRRD_RiskScore=10 Or BRRD_RiskScore=20) And BRRD_CompID=" & iACID & " And BRRD_PKID=BBRIT_BRRDPKID"
    '                sSql = sSql & " left join mst_entity_master on ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & "  left join content_management_master on cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & ""
    '                sSql = sSql & " left join Risk_CheckList_Master on RCM_CustID=" & iCustID & " And RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " And RCM_YEARID=" & iYearID & ""
    '                sSql = sSql & " join sad_userdetails a On BRRS_EmployeeID=a.usr_Id And a.Usr_CompID=" & iACID & " join sad_userdetails b On BRRS_BranchMgrID=b.usr_Id "
    '                sSql = sSql & " And b.Usr_CompID=" & iACID & " Left Join Risk_BRRSchedule On BBRIT_AsgNo=BRRS_PKID And BRRS_CustID=" & iCustID & " And BRRS_CompID=" & iACID & ""
    '                sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=BRRD_RiskCategory And MIM_CompID=" & iACID & ""
    '                sSql = sSql & " Where BBRIT_CustID=" & iCustID & " And BBRIT_CompID=" & iACID & " And BBRIT_Status='S'"
    '                If iBRRRAsgID > 0 Then
    '                    sSql = sSql & " And BBRIT_BranchID=" & iBRRRAsgID & ""
    '                End If
    '                If iStatus > 0 Then
    '                    sSql = sSql & " And BBRIT_OpenCloseStatus=" & iStatus & ""
    '                End If
    '                sSql = sSql & " ORDER BY BBRIT_PKID"
    '                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
    '                If dt.Rows.Count > 0 Then
    '                    iBRRCount = dt.Rows.Count
    '                    For i = 0 To dt.Rows.Count - 1
    '                        dRow = dtTable.NewRow
    '                        dRow("SrNo") = i + 1
    '                        dRow("BRRDPKID") = dt.Rows(i).Item("BBRIT_BRRDPKID")
    '                        dRow("RCMID") = dt.Rows(i).Item("BBRIT_RCMID")
    '                        dRow("AsgNo") = dt.Rows(i).Item("BBRIT_AsgNo")
    '                        dRow("FunctionID") = dt.Rows(i).Item("BRRD_FunctionID")
    '                        dRow("IssueTrackerNoID") = dt.Rows(i).Item("BBRIT_PKID")
    '                        If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
    '                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
    '                        End If
    '                        dRow("AreaID") = dt.Rows(i).Item("BRRD_AreaID")
    '                        If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
    '                            dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
    '                        End If
    '                        If IsDBNull(dt.Rows(i)("RCM_ID")) = False Then
    '                            dRow("CheckPointsID") = dt.Rows(i).Item("RCM_ID")
    '                        End If
    '                        If IsDBNull(dt.Rows(i)("BRRD_CheckPoint")) = False Then
    '                            dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
    '                        End If
    '                        If IsDBNull(dt.Rows(i)("BRRD_IssueDetails")) = False Then
    '                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
    '                        End If
    '                        If IsDBNull(dt.Rows(i)("BRRD_Annexure")) = False Then
    '                            dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
    '                        End If
    '                        If IsDBNull(dt.Rows(i)("BRRD_RiskCategory")) = False Then
    '                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
    '                            dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("MIM_Color"))
    '                        End If
    '                        If IsDBNull(dt.Rows(i)("BBRIT_ActionPlan")) = False Then
    '                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BBRIT_ActionPlan"))
    '                        End If
    '                        If IsDBNull(dt.Rows(i)("BBRIT_OpenCloseStatus")) = False Then
    '                            If dt.Rows(i)("BBRIT_OpenCloseStatus") = 1 Then
    '                                dRow("IOpenClose") = "Open"
    '                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 2 Then
    '                                dRow("IOpenClose") = "Closed"
    '                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 3 Then
    '                                dRow("IOpenClose") = "Open-Not Actioned"
    '                            End If
    '                        End If
    '                        dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "F")
    '                        dRow("SourceofIssue") = "Branch Risk Review"
    '                        dRow("RLICEmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("RLICEmployee"))
    '                        dRow("BranchManager") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BranchMgr"))
    '                        dRow("Remarks") = ""
    '                        dtTable.Rows.Add(dRow)
    '                    Next
    '                End If
    '            End If
    '        End If
    '        If iSourceofIssue = "2" Or iSourceofIssue = "0" Then 'BIA
    '            iAreaID = objDBL.SQLExecuteScalar(sAC, "Select cmm_id from content_management_master where cmm_Desc='Branch Risk Scoring Module' And CMM_CompID=" & iACID & "")
    '            If objDBL.SQLCheckForRecord(sAC, "Select * from Audit_BIADraftFinalReport where BIAR_CompID=" & iACID & " And BIAR_YearID=" & iYearID & " And BIAR_FormName='BIADraftReport'") = True Then
    '                sSql = "Select BIAR_PKID,BIAR_BIACDPKID,BIAR_OpenCloseStatus,BIAR_BIACD_ACMID,BIAR_AsgID,BIAR_FunctionID,BIAR_AreaID,BIAR_IssueDesc,BIAR_AnnexureNo,BIAR_RiskCategory,BIAR_IssueAgreed,"
    '                sSql = sSql & " BIARS_DraftReportSatus,BIACD_CheckPoint,BIACD_PKID,BIARS_FinalReportSatus,BIAR_ActionPlanDate,BIAR_DisAgreedReason,BIAR_OpenCloseStatus,BIAR_ClosingDate,BIAR_ActionPlan,"
    '                sSql = sSql & " a.Usr_Fullname As BranchManager,BIAR_BranchManagerID,ENT_ENTITYNAME As FunctionName,b.usr_FullName as EmployeeName,cmm_Desc,MIM_Color From Audit_BIADraftFinalReport"
    '                sSql = sSql & " Left Join Audit_BIAChecklist_Details On BIAR_BIACDPKID=BIACD_PKID Left Join Audit_CheckList_Master On ACM_Id=BIAR_BIACD_ACMID And ACM_YearID=" & iYearID & ""
    '                sSql = sSql & " Left Join Sad_Userdetails a On usr_id=BIAR_BranchManagerID  Left Join mst_entity_master On ENT_Id=BIAR_FunctionID And ENT_CompID=" & iACID & ""
    '                sSql = sSql & " Left Join content_management_master On cmm_ID=BIAR_AreaID And CMM_CompID=" & iACID & ""
    '                sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=BIACD_RiskCategory And MIM_CompID=" & iACID & ""
    '                sSql = sSql & " Left Join Audit_BIADraftFinalReportStatus On BIARS_AsgID=BIAR_AsgID And BIARS_CompID=" & iACID & " And BIARS_YearID=" & iYearID & ""
    '                sSql = sSql & " Left Join Audit_BAnnualAuditSchedule On BAAS_FinancialYear=" & iYearID & " And BAAS_PKID=BIAR_AsgID And BAAS_CompID=" & iACID & ""
    '                sSql = sSql & " Left Join Sad_Userdetails b On b.usr_Id=BAAS_EmployeeID And b.Usr_CompID=" & iACID & ""
    '                sSql = sSql & " where BIAR_AreaID<>" & iAreaID & " And BIAR_FormName='BIADraftReport' And BIAR_Status='S'"
    '                sSql = sSql & " And BIACD_YESNONA = 2 And (BIACD_RiskScore = 2 Or BIACD_RiskScore = 3 Or BIACD_RiskScore = 0 Or BIACD_RiskScore = 10 Or BIACD_RiskScore = 20)"
    '                sSql = sSql & " And BIACD_PKID=BIAR_BIACDPKID And BIAR_CompID=" & iACID & ""
    '                If iBRRRAsgID > 0 Then
    '                    sSql = sSql & " And BIAR_BranchID=" & iBRRRAsgID & ""
    '                End If
    '                If iStatus > 0 Then
    '                    sSql = sSql & " And BIAR_OpenCloseStatus=" & iStatus & ""
    '                End If
    '                sSql = sSql & " ORDER BY BIAR_PKID"
    '                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
    '                If dt.Rows.Count > 0 Then
    '                    iBIACount = dt.Rows.Count + iBRRCount
    '                    For j = 0 To dt.Rows.Count - 1
    '                        dRow = dtTable.NewRow
    '                        dRow("SrNo") = j + 1 + iBRRCount
    '                        dRow("BRRDPKID") = dt.Rows(j).Item("BIAR_BIACDPKID")
    '                        dRow("RCMID") = dt.Rows(j).Item("BIAR_BIACD_ACMID")
    '                        dRow("AsgNo") = dt.Rows(j).Item("BIAR_AsgID")
    '                        dRow("FunctionID") = dt.Rows(j).Item("BIAR_FunctionID")
    '                        dRow("IssueTrackerNoID") = dt.Rows(j).Item("BIAR_PKID")
    '                        If IsDBNull(dt.Rows(j)("FunctionName")) = False Then
    '                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("FunctionName"))
    '                        End If
    '                        dRow("AreaID") = dt.Rows(j).Item("BIAR_AreaID")
    '                        If IsDBNull(dt.Rows(j)("cmm_Desc")) = False Then
    '                            dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("cmm_Desc"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BIACD_PKID")) = False Then
    '                            dRow("CheckPointsID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIACD_PKID"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BIACD_CheckPoint")) = False Then
    '                            dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIACD_CheckPoint"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BIAR_IssueDesc")) = False Then
    '                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_IssueDesc"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BIAR_AnnexureNo")) = False Then
    '                            dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_AnnexureNo"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BIAR_RiskCategory")) = False Then
    '                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_RiskCategory"))
    '                            dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("MIM_Color"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BIAR_ActionPlan")) = False Then
    '                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_ActionPlan"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BIAR_OpenCloseStatus")) = False Then
    '                            If dt.Rows(j)("BIAR_OpenCloseStatus") = 1 Then
    '                                dRow("IOpenClose") = "Open"
    '                            ElseIf dt.Rows(j)("BIAR_OpenCloseStatus") = 2 Then
    '                                dRow("IOpenClose") = "Closed"
    '                            ElseIf dt.Rows(j)("BIAR_OpenCloseStatus") = 3 Then
    '                                dRow("IOpenClose") = "Open-Not Actioned"
    '                            End If
    '                        End If
    '                        If objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(j).Item("BIAR_ActionPlanDate"), "D").Contains("1900") = False Then
    '                            dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(j).Item("BIAR_ActionPlanDate"), "D")
    '                        End If
    '                        dRow("SourceofIssue") = "Branch Internal Audit"
    '                        If IsDBNull(dt.Rows(j)("EmployeeName")) = False Then
    '                            dRow("RLICEmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("EmployeeName"))
    '                        End If
    '                        If IsDBNull(dt.Rows(j)("BranchManager")) = False Then
    '                            dRow("BranchManager") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BranchManager"))
    '                        End If
    '                        dRow("Remarks") = ""
    '                        dtTable.Rows.Add(dRow)
    '                    Next
    '                End If
    '            End If
    '        End If
    '        If iSourceofIssue = "3" Or iSourceofIssue = "0" Then 'BIA
    '            iAreaID = objDBL.SQLExecuteScalar(sAC, "Select cmm_id from content_management_master where cmm_Desc='Branch Risk Scoring Module' And CMM_CompID=" & iACID & "")
    '            If objDBL.SQLCheckForRecord(sAC, "Select CMAIM_ID from CMANoChecksIssue_mas where CMAIM_AsgID in(select CAD_ID from CMA_Assignment_Details where CAD_AuditYear=" & iYearID & " and CAD_CompID=" & iACID & ") And CMAIM_CompID=" & iACID & "") = True Then
    '                sSql = "Select CMAI_CMDId,CMAI_ID,CMD_Id,CMAI_AObservation,MIM_Color,CMAI_CMDId_CMId,CMD_Annexure,CMAI_IssueHeading,CMAI_AAPlan,CMAI_OpenClose,CMD_AreaId,CMAI_ActionDate,CMAI_Responsibilty,CMAI_CMAIMId,CMAI_Status,"
    '                sSql = sSql & " CM_CheckPointNo,ENT_ENTITYNAME,CMD_CheckPoints,cmm_Desc,CM_CheckPoint,CMD_RiskCategory,CMAIM_AsgID,CMD_FunctionId From CMANoChecksIssue"
    '                sSql = sSql & " Left Join CMAChecksReport On CMAI_CMDId=CMD_Id Left Join cmacheckmaster On cm_id=CMAI_CMDId_CMId"
    '                sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=CMD_RiskCategory And MIM_CompID=" & iACID & ""
    '                sSql = sSql & " Left Join mst_entity_master On ENT_ID=CMD_FunctionId And ENT_CompID=" & iACID & ""
    '                sSql = sSql & " Left Join content_management_master On cmm_ID=CMD_AreaId And CMM_CompID=" & iACID & ""
    '                sSql = sSql & " Left Join CMANoChecksIssue_mas On CMAIM_ID=CMAI_CMAIMId And CMAIM_CompID=" & iACID & ""
    '                sSql = sSql & " Where CMAI_CMAIMId In (Select CMAIM_ID from CMANoChecksIssue_mas"
    '                sSql = sSql & " where CMAIM_ID = CMAI_CMAIMID And CMAIM_AsgID In(Select CMAIM_AsgID from CMA_Assignment_Details"
    '                sSql = sSql & " Where CAD_ID=CMAIM_AsgID And CAD_AuditYear=" & iYearID & ")) And CMD_AreaId<>" & iAreaID & ""
    '                sSql = sSql & " And CMD_YESNONA = 2 And (CMD_RiskScore = 2 Or CMD_RiskScore = 3 Or CMD_RiskScore = 0 Or CMD_RiskScore = 10 Or CMD_RiskScore = 20)"
    '                sSql = sSql & " And CMD_Id = CMAI_CMDId And CMAI_Status='S'"
    '                If iBRRRAsgID > 0 Then
    '                    sSql = sSql & " And CMAIM_BranchID=" & iBRRRAsgID & ""
    '                End If
    '                If iStatus > 0 Then
    '                    sSql = sSql & " And CMAI_OpenClose = " & iStatus & ""
    '                End If
    '                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
    '                If dt.Rows.Count > 0 Then
    '                    For k = 0 To dt.Rows.Count - 1
    '                        dRow = dtTable.NewRow
    '                        dRow("SrNo") = k + 1 + iBIACount
    '                        dRow("BRRDPKID") = dt.Rows(k).Item("CMAI_CMDId")
    '                        dRow("RCMID") = dt.Rows(k).Item("CMAI_CMDId_CMId")
    '                        dRow("AsgNo") = dt.Rows(k).Item("CMAIM_AsgID")
    '                        dRow("FunctionID") = dt.Rows(k).Item("CMD_FunctionId")
    '                        dRow("IssueTrackerNoID") = dt.Rows(k).Item("CMAI_ID")
    '                        If IsDBNull(dt.Rows(k)("ENT_ENTITYNAME")) = False Then
    '                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("ENT_ENTITYNAME"))
    '                        End If
    '                        dRow("AreaID") = dt.Rows(k).Item("CMD_AreaId")
    '                        If IsDBNull(dt.Rows(k)("cmm_Desc")) = False Then
    '                            dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("cmm_Desc"))
    '                        End If
    '                        If IsDBNull(dt.Rows(k)("CMD_Id")) = False Then
    '                            dRow("CheckPointsID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_Id"))
    '                        End If
    '                        If IsDBNull(dt.Rows(k)("CMD_CheckPoints")) = False Then
    '                            dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_CheckPoints"))
    '                        End If
    '                        If IsDBNull(dt.Rows(k)("CMAI_AObservation")) = False Then
    '                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMAI_AObservation"))
    '                        End If
    '                        If IsDBNull(dt.Rows(k)("CMD_Annexure")) = False Then
    '                            dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_Annexure"))
    '                        End If
    '                        If IsDBNull(dt.Rows(k)("CMD_RiskCategory")) = False Then
    '                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_RiskCategory"))
    '                            dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("MIM_Color"))
    '                        End If
    '                        If IsDBNull(dt.Rows(k)("CMAI_AAPlan")) = False Then
    '                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMAI_AAPlan"))
    '                        End If
    '                        If IsDBNull(dt.Rows(k)("CMAI_OpenClose")) = False Then
    '                            If dt.Rows(k)("CMAI_OpenClose") = 1 Then
    '                                dRow("IOpenClose") = "Open"
    '                            ElseIf dt.Rows(k)("CMAI_OpenClose") = 2 Then
    '                                dRow("IOpenClose") = "Closed"
    '                            ElseIf dt.Rows(k)("CMAI_OpenClose") = 3 Then
    '                                dRow("IOpenClose") = "Open-Not Actioned"
    '                            End If
    '                        End If
    '                        If objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(k).Item("CMAI_ActionDate"), "D").Contains("1900") = False Then
    '                            dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(k).Item("CMAI_ActionDate"), "D")
    '                        End If
    '                        dRow("SourceofIssue") = "Branch Continuous Monitoring"
    '                        dRow("RLICEmployeeName") = ""
    '                        dRow("BranchManager") = ""
    '                        dRow("Remarks") = ""
    '                        dtTable.Rows.Add(dRow)
    '                    Next
    '                End If
    '            End If
    '        End If
    '        Return dtTable
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function LoadBRRBIABCMAllBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCheckUserAsgNo As Integer, ByVal iFUNUserID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select Distinct(Org_Node), Org_Name From sad_org_structure Left Join Risk_BRRIssueTracker On BBRIT_FinancialYear=" & iYearID & " And BBRIT_CompID=" & iACID & ""
    '        sSql = sSql & "Left Join Risk_BRRSchedule On BBRIT_AsgNo=BRRS_PKID And BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & ""
    '        sSql = sSql & " Left Join Audit_BIADraftFinalReportStatus On BIARS_YearID=" & iYearID & " And BIARS_CompID=" & iACID & " Left Join CMANoChecksIssue_mas On CMAIM_CompID=" & iACID & ""
    '        sSql = sSql & " And CMAIM_AsgID In(Select CAD_ID From CMA_Assignment_Details Where CAD_AuditYear=" & iYearID & " And CAD_CompID=" & iACID & ")"
    '        sSql = sSql & " Where (Org_Node=BBRIT_BranchId or Org_Node=BIARS_BranchID or Org_Node=CMAIM_BranchID)"
    '        If iCheckUserAsgNo > 0 Then
    '            sSql = sSql & " And (BRRS_ZonalMgrID=" & iCheckUserAsgNo & " Or BRRS_BranchMgrID=" & iCheckUserAsgNo & " Or BRRS_EmployeeID=" & iCheckUserAsgNo & ")"
    '        End If
    '        If iFUNUserID > 0 Then
    '            sSql = sSql & " And BRRS_PKID in (Select BRR_AsgID from Risk_BRRChecklist_Mas where BRR_PKID in (Select BRRD_BRRPKID From Risk_BRRChecklist_Details Where BRRD_YESNONA=2 And BRRD_FunctionID in "
    '            sSql = sSql & " (Select ENT_ID From mst_Entity_master Where (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ") and ENT_CompID=" & iACID & ")))"
    '        End If
    '        sSql = sSql & " Order by Org_Name"
    '        dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadBRRAllBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select distinct(BRRS_PKID),BRRS_AsgNo +' ('+ Org_Name + ') 'as AsgNo from Risk_BRRSchedule Left Join Risk_BRRIssueTracker On BBRIT_AsgNo=BRRS_PKID"
            sSql = sSql & " And BBRIT_CustID=" & iCustID & " Left Join sad_org_structure On BRRS_BranchID=Org_Node"
            sSql = sSql & " Where BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & " And BRRS_CustID=" & iCustID & ""
            sSql = sSql & " order by BRRS_PKID"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBRRAllIssueTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRRAsgID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dRow As DataRow
        Dim dtTable As New DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dtTable.Columns.Add("SrNo")
            dtTable.Columns.Add("BRRDPKID")
            dtTable.Columns.Add("RCMID")
            dtTable.Columns.Add("AsgNo")
            dtTable.Columns.Add("Function")
            dtTable.Columns.Add("FunctionID")
            dtTable.Columns.Add("Area")
            dtTable.Columns.Add("AreaID")
            dtTable.Columns.Add("CheckPoints")
            dtTable.Columns.Add("CheckPointsID")
            dtTable.Columns.Add("IssueDetails")
            dtTable.Columns.Add("Annexure")
            dtTable.Columns.Add("Risk")
            dtTable.Columns.Add("RiskCategoryColor")
            dtTable.Columns.Add("ActionPlan")
            dtTable.Columns.Add("TargetDate")
            dtTable.Columns.Add("IOpenClose")
            dtTable.Columns.Add("SourceofIssue")
            dtTable.Columns.Add("RLICEmployeeName")
            dtTable.Columns.Add("BranchManager")
            dtTable.Columns.Add("Remarks")
            dtTable.Columns.Add("IssueTrackerNoID")
            dtTable.Columns.Add("AttachID")

            If objDBL.SQLCheckForRecord(sAC, "Select BBRIT_PKID from Risk_BRRIssueTracker Where BBRIT_CompID=" & iACID & "") = True Then
                sSql = "Select BBRIT_PKID, BBRIT_BRRDPKID, BBRIT_RCMID, BBRIT_AsgNo, BBRIT_BranchId, BBRIT_TargetDate, BRRD_FunctionID,ENT_ENTITYNAME,BRRD_AreaID, cmm_Desc,RCM_ID,"
                sSql = sSql & " BRRD_CheckPoint, BRRD_IssueDetails, BRRD_Annexure,BBRIT_AttchID,BRRD_RiskCategory,MIM_Color,BBRIT_ActionPlan, BBRIT_OpenCloseStatus,BRRS_AsgNo,BRRS_EmployeeID,a.usr_FullName As BranchMgr,"
                sSql = sSql & " BRRS_BranchMgrID, b.usr_FullName As RLICEmployee From Risk_BRRIssueTracker left join Risk_BRRChecklist_Details On BRRD_YESNONA=2"
                sSql = sSql & " And (BRRD_RiskScore=2 Or BRRD_RiskScore=3 Or BRRD_RiskScore=0  Or BRRD_RiskScore=10 Or BRRD_RiskScore=20) And BRRD_CompID=" & iACID & " And BRRD_PKID=BBRIT_BRRDPKID"
                sSql = sSql & " left join mst_entity_master on ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & " left join content_management_master on cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & ""
                sSql = sSql & " left join Risk_CheckList_Master on RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " And RCM_YearID=" & iYearID & " And RCM_CustID=" & iCustID & ""
                sSql = sSql & " Left Join Risk_BRRSchedule On BBRIT_AsgNo=BRRS_PKID And BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & " And BRRS_CustID=" & iCustID & ""
                sSql = sSql & " join sad_userdetails a On BRRS_EmployeeID=a.usr_Id And a.Usr_CompID=" & iACID & " join sad_userdetails b On BRRS_BranchMgrID=b.usr_Id And b.Usr_CompID=" & iACID & ""
                sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=BRRD_RiskCategory And MIM_CompID=" & iACID & ""
                sSql = sSql & " Where BBRIT_CompID=" & iACID & " And BBRIT_FinancialYear=" & iYearID & " And BBRIT_CustID=" & iCustID & ""
                If iBRRRAsgID > 0 Then
                    sSql = sSql & " And BBRIT_AsgNo=" & iBRRRAsgID & ""
                End If
                If iStatus > 0 Then
                    sSql = sSql & " And BBRIT_OpenCloseStatus=" & iStatus & ""
                End If
                sSql = sSql & " ORDER BY BBRIT_PKID"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dRow = dtTable.NewRow
                        dRow("SrNo") = i + 1
                        dRow("BRRDPKID") = dt.Rows(i).Item("BBRIT_BRRDPKID")
                        dRow("RCMID") = dt.Rows(i).Item("BBRIT_RCMID")
                        dRow("AsgNo") = dt.Rows(i).Item("BBRIT_AsgNo")
                        dRow("FunctionID") = dt.Rows(i).Item("BRRD_FunctionID")
                        dRow("IssueTrackerNoID") = dt.Rows(i).Item("BBRIT_PKID")
                        If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
                        End If
                        dRow("AreaID") = dt.Rows(i).Item("BRRD_AreaID")
                        If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
                            dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
                        End If
                        If IsDBNull(dt.Rows(i)("RCM_ID")) = False Then
                            dRow("CheckPointsID") = dt.Rows(i).Item("RCM_ID")
                        End If
                        If IsDBNull(dt.Rows(i)("BRRD_CheckPoint")) = False Then
                            dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
                        End If
                        If IsDBNull(dt.Rows(i)("BRRD_IssueDetails")) = False Then
                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
                        End If
                        If IsDBNull(dt.Rows(i)("BRRD_Annexure")) = False Then
                            dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
                        End If
                        If IsDBNull(dt.Rows(i)("BRRD_RiskCategory")) = False Then
                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
                            dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("MIM_Color"))
                        End If
                        If IsDBNull(dt.Rows(i)("BBRIT_ActionPlan")) = False Then
                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BBRIT_ActionPlan"))
                        End If
                        If IsDBNull(dt.Rows(i)("BBRIT_OpenCloseStatus")) = False Then
                            If dt.Rows(i)("BBRIT_OpenCloseStatus") = 1 Then
                                dRow("IOpenClose") = "Open"
                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 2 Then
                                dRow("IOpenClose") = "Closed"
                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 3 Then
                                dRow("IOpenClose") = "Open-Not Actioned"
                            End If
                        End If
                        dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "F")
                        dRow("SourceofIssue") = "Branch Risk Review"
                        dRow("RLICEmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("RLICEmployee"))
                        dRow("BranchManager") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BranchMgr"))
                        dRow("Remarks") = ""
                        If IsDBNull(dt.Rows(i)("BBRIT_AttchID")) = False Then
                            dRow("AttachID") = dt.Rows(i)("BBRIT_AttchID")
                        End If
                        dtTable.Rows.Add(dRow)
                    Next
                End If
            End If
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRBIABCMAllBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(Org_Node), Org_Name From sad_org_structure"
            sSql = sSql & " Left Join Risk_BRRIssueTracker On BBRIT_FinancialYear=" & iYearID & " And BBRIT_CompID=" & iACID & " And BBRIT_CustID=" & iCustID & ""
            sSql = sSql & " Left Join Risk_BRRSchedule On BBRIT_AsgNo=BRRS_PKID And BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & " And BRRS_CustID=" & iCustID & ""
            'sSql = sSql & " Left Join Audit_BIADraftFinalReportStatus On BIARS_YearID=" & iYearID & " And BIARS_CompID=" & iACID & ""
            'sSql = sSql & " Left Join CMANoChecksIssue_mas On CMAIM_CompID=" & iACID & ""
            'sSql = sSql & " And CMAIM_AsgID In(Select CAD_ID From CMA_Assignment_Details Where CAD_AuditYear=" & iYearID & " And CAD_CompID=" & iACID & ")"
            sSql = sSql & " Where (Org_Node=BBRIT_BranchId)"
            sSql = sSql & " Order by Org_Name"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBRRAllIssueTrackerDetailsToRiskRegister(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRRAsgID As Integer, ByVal iStatus As Integer, ByVal iSourceofIssue As Integer) As DataTable
        Dim dRow As DataRow
        Dim dtTable As New DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim i As Integer, iAreaID As Integer, iBRRCount As Integer = 0, iBIACount As Integer = 0
        Try
            dtTable.Columns.Add("SrNo")
            dtTable.Columns.Add("BRRDPKID")
            dtTable.Columns.Add("RCMID")
            dtTable.Columns.Add("AsgNo")
            dtTable.Columns.Add("Function")
            dtTable.Columns.Add("FunctionID")
            dtTable.Columns.Add("Area")
            dtTable.Columns.Add("AreaID")
            dtTable.Columns.Add("CheckPoints")
            dtTable.Columns.Add("CheckPointsID")
            dtTable.Columns.Add("IssueDetails")
            dtTable.Columns.Add("Annexure")
            dtTable.Columns.Add("Risk")
            dtTable.Columns.Add("RiskCategoryColor")
            dtTable.Columns.Add("ActionPlan")
            dtTable.Columns.Add("TargetDate")
            dtTable.Columns.Add("IOpenClose")
            dtTable.Columns.Add("SourceofIssue")
            dtTable.Columns.Add("RLICEmployeeName")
            dtTable.Columns.Add("BranchManager")
            dtTable.Columns.Add("Remarks")
            dtTable.Columns.Add("IssueTrackerNoID")
            If iSourceofIssue = "1" Or iSourceofIssue = "0" Then 'BRR
                If objDBL.SQLCheckForRecord(sAC, "Select BBRIT_PKID from Risk_BRRIssueTracker Where BBRIT_CompID=" & iACID & "") = True Then
                    sSql = "Select BBRIT_PKID, BBRIT_BRRDPKID, BBRIT_RCMID, BBRIT_AsgNo, BBRIT_BranchId, BBRIT_TargetDate, BRRD_FunctionID,ENT_ENTITYNAME,BRRD_AreaID, cmm_Desc,RCM_ID,"
                    sSql = sSql & " BRRD_CheckPoint, BRRD_IssueDetails, BRRD_Annexure, BRRD_RiskCategory,MIM_Color,BBRIT_ActionPlan, BBRIT_OpenCloseStatus,BRRS_AsgNo,BRRS_EmployeeID,"
                    sSql = sSql & " b.usr_FullName As BranchMgr,BRRS_BranchMgrID, a.usr_FullName As RLICEmployee From Risk_BRRIssueTracker"
                    sSql = sSql & " left join Risk_BRRChecklist_Details On BRRD_YESNONA=2 And (BRRD_RiskScore=2 Or BRRD_RiskScore=3 Or BRRD_RiskScore=0"
                    sSql = sSql & " Or BRRD_RiskScore=10 Or BRRD_RiskScore=20) And BRRD_CompID=" & iACID & " And BRRD_PKID=BBRIT_BRRDPKID"
                    sSql = sSql & " left join mst_entity_master on ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & ""
                    sSql = sSql & " left join content_management_master on cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & " left join Risk_CheckList_Master"
                    sSql = sSql & " on RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " And RCM_YEARID=" & iYearID & " And RCM_CustID=" & iCustID & ""
                    sSql = sSql & " Left Join Risk_BRRSchedule On BBRIT_AsgNo=BRRS_PKID And BRRS_CompID=" & iACID & " And BRRS_CustID=" & iCustID & ""
                    sSql = sSql & " join sad_userdetails a On BRRS_EmployeeID=a.usr_Id And a.Usr_CompID=" & iACID & ""
                    sSql = sSql & " join sad_userdetails b On BRRS_BranchMgrID=b.usr_Id And b.Usr_CompID=" & iACID & ""
                    sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=BRRD_RiskCategory And MIM_CompID=" & iACID & ""
                    sSql = sSql & " Where BBRIT_CompID=" & iACID & " And BBRIT_Status='S'"
                    If iCustID > 0 Then
                        sSql = sSql & " And BBRIT_CustID=" & iCustID & ""
                    End If
                    If iBRRRAsgID > 0 Then
                        sSql = sSql & " And BBRIT_BranchID=" & iBRRRAsgID & ""
                    End If
                    If iStatus > 0 Then
                        sSql = sSql & " And BBRIT_OpenCloseStatus=" & iStatus & ""
                    End If
                    sSql = sSql & " ORDER BY BBRIT_PKID"
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If dt.Rows.Count > 0 Then
                        iBRRCount = dt.Rows.Count
                        For i = 0 To dt.Rows.Count - 1
                            dRow = dtTable.NewRow
                            dRow("SrNo") = i + 1
                            dRow("BRRDPKID") = dt.Rows(i).Item("BBRIT_BRRDPKID")
                            dRow("RCMID") = dt.Rows(i).Item("BBRIT_RCMID")
                            dRow("AsgNo") = dt.Rows(i).Item("BBRIT_AsgNo")
                            dRow("FunctionID") = dt.Rows(i).Item("BRRD_FunctionID")
                            dRow("IssueTrackerNoID") = dt.Rows(i).Item("BBRIT_PKID")
                            If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
                                dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
                            End If
                            dRow("AreaID") = dt.Rows(i).Item("BRRD_AreaID")
                            If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
                                dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
                            End If
                            If IsDBNull(dt.Rows(i)("RCM_ID")) = False Then
                                dRow("CheckPointsID") = dt.Rows(i).Item("RCM_ID")
                            End If
                            If IsDBNull(dt.Rows(i)("BRRD_CheckPoint")) = False Then
                                dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
                            End If
                            If IsDBNull(dt.Rows(i)("BRRD_IssueDetails")) = False Then
                                dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
                            End If
                            If IsDBNull(dt.Rows(i)("BRRD_Annexure")) = False Then
                                dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
                            End If
                            If IsDBNull(dt.Rows(i)("BRRD_RiskCategory")) = False Then
                                dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
                                dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("MIM_Color"))
                            End If
                            If IsDBNull(dt.Rows(i)("BBRIT_ActionPlan")) = False Then
                                dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BBRIT_ActionPlan"))
                            End If
                            If IsDBNull(dt.Rows(i)("BBRIT_OpenCloseStatus")) = False Then
                                If dt.Rows(i)("BBRIT_OpenCloseStatus") = 1 Then
                                    dRow("IOpenClose") = "Open"
                                ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 2 Then
                                    dRow("IOpenClose") = "Closed"
                                ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 3 Then
                                    dRow("IOpenClose") = "Open-Not Actioned"
                                End If
                            End If
                            dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "F")
                            dRow("SourceofIssue") = "Branch Risk Review"
                            dRow("RLICEmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("RLICEmployee"))
                            dRow("BranchManager") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BranchMgr"))
                            dRow("Remarks") = ""
                            dtTable.Rows.Add(dRow)
                        Next
                    End If
                End If
            End If
            If iSourceofIssue = "2" Or iSourceofIssue = "0" Then 'BIA
                iAreaID = objDBL.SQLExecuteScalar(sAC, "Select cmm_id from content_management_master where cmm_Desc='Branch Risk Scoring Module' And CMM_CompID=" & iACID & "")
                If objDBL.SQLCheckForRecord(sAC, "Select * from Audit_BIADraftFinalReport where BIAR_CompID=" & iACID & " And BIAR_YearID=" & iYearID & " And BIAR_FormName='BIADraftReport'") = True Then
                    sSql = "Select BIAR_PKID,BIAR_BIACDPKID,BIAR_OpenCloseStatus,BIAR_BIACD_ACMID,BIAR_AsgID,BIAR_FunctionID,BIAR_AreaID,BIAR_IssueDesc,BIAR_AnnexureNo,BIAR_RiskCategory,BIAR_IssueAgreed,"
                    sSql = sSql & " BIARS_DraftReportSatus,BIACD_CheckPoint,BIACD_PKID,BIARS_FinalReportSatus,BIAR_ActionPlanDate,BIAR_DisAgreedReason,BIAR_OpenCloseStatus,BIAR_ClosingDate,BIAR_ActionPlan,"
                    sSql = sSql & " a.Usr_Fullname As BranchManager,BIAR_BranchManagerID,ENT_ENTITYNAME As FunctionName,b.usr_FullName as EmployeeName,cmm_Desc,MIM_Color From Audit_BIADraftFinalReport"
                    sSql = sSql & " Left Join Audit_BIAChecklist_Details On BIAR_BIACDPKID=BIACD_PKID Left Join Audit_CheckList_Master On ACM_Id=BIAR_BIACD_ACMID And ACM_YearID=" & iYearID & ""
                    sSql = sSql & " Left Join Sad_Userdetails a On usr_id=BIAR_BranchManagerID  Left Join mst_entity_master On ENT_Id=BIAR_FunctionID And ENT_CompID=" & iACID & ""
                    sSql = sSql & " Left Join content_management_master On cmm_ID=BIAR_AreaID And CMM_CompID=" & iACID & ""
                    sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=BIACD_RiskCategory And MIM_CompID=" & iACID & ""
                    sSql = sSql & " Left Join Audit_BIADraftFinalReportStatus On BIARS_AsgID=BIAR_AsgID And BIARS_CompID=" & iACID & " And BIARS_YearID=" & iYearID & ""
                    sSql = sSql & " Left Join Audit_BAnnualAuditSchedule On BAAS_FinancialYear=" & iYearID & " And BAAS_PKID=BIAR_AsgID And BAAS_CompID=" & iACID & ""
                    sSql = sSql & " Left Join Sad_Userdetails b On b.usr_Id=BAAS_EmployeeID And b.Usr_CompID=" & iACID & ""
                    sSql = sSql & " where BIAR_AreaID<>" & iAreaID & " And BIAR_FormName='BIADraftReport' And BIAR_Status='S'"
                    sSql = sSql & " And BIACD_YESNONA = 2 And (BIACD_RiskScore = 2 Or BIACD_RiskScore = 3 Or BIACD_RiskScore = 0 Or BIACD_RiskScore = 10 Or BIACD_RiskScore = 20)"
                    sSql = sSql & " And BIACD_PKID=BIAR_BIACDPKID And BIAR_CompID=" & iACID & ""
                    If iBRRRAsgID > 0 Then
                        sSql = sSql & " And BIAR_BranchID=" & iBRRRAsgID & ""
                    End If
                    If iStatus > 0 Then
                        sSql = sSql & " And BIAR_OpenCloseStatus=" & iStatus & ""
                    End If
                    sSql = sSql & " ORDER BY BIAR_PKID"
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If dt.Rows.Count > 0 Then
                        iBIACount = dt.Rows.Count + iBRRCount
                        For j = 0 To dt.Rows.Count - 1
                            dRow = dtTable.NewRow
                            dRow("SrNo") = j + 1 + iBRRCount
                            dRow("BRRDPKID") = dt.Rows(j).Item("BIAR_BIACDPKID")
                            dRow("RCMID") = dt.Rows(j).Item("BIAR_BIACD_ACMID")
                            dRow("AsgNo") = dt.Rows(j).Item("BIAR_AsgID")
                            dRow("FunctionID") = dt.Rows(j).Item("BIAR_FunctionID")
                            dRow("IssueTrackerNoID") = dt.Rows(j).Item("BIAR_PKID")
                            If IsDBNull(dt.Rows(j)("FunctionName")) = False Then
                                dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("FunctionName"))
                            End If
                            dRow("AreaID") = dt.Rows(j).Item("BIAR_AreaID")
                            If IsDBNull(dt.Rows(j)("cmm_Desc")) = False Then
                                dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("cmm_Desc"))
                            End If
                            If IsDBNull(dt.Rows(j)("BIACD_PKID")) = False Then
                                dRow("CheckPointsID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIACD_PKID"))
                            End If
                            If IsDBNull(dt.Rows(j)("BIACD_CheckPoint")) = False Then
                                dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIACD_CheckPoint"))
                            End If
                            If IsDBNull(dt.Rows(j)("BIAR_IssueDesc")) = False Then
                                dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_IssueDesc"))
                            End If
                            If IsDBNull(dt.Rows(j)("BIAR_AnnexureNo")) = False Then
                                dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_AnnexureNo"))
                            End If
                            If IsDBNull(dt.Rows(j)("BIAR_RiskCategory")) = False Then
                                dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_RiskCategory"))
                                dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("MIM_Color"))
                            End If
                            If IsDBNull(dt.Rows(j)("BIAR_ActionPlan")) = False Then
                                dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BIAR_ActionPlan"))
                            End If
                            If IsDBNull(dt.Rows(j)("BIAR_OpenCloseStatus")) = False Then
                                If dt.Rows(j)("BIAR_OpenCloseStatus") = 1 Then
                                    dRow("IOpenClose") = "Open"
                                ElseIf dt.Rows(j)("BIAR_OpenCloseStatus") = 2 Then
                                    dRow("IOpenClose") = "Closed"
                                ElseIf dt.Rows(j)("BIAR_OpenCloseStatus") = 3 Then
                                    dRow("IOpenClose") = "Open-Not Actioned"
                                End If
                            End If
                            If objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(j).Item("BIAR_ActionPlanDate"), "D").Contains("1900") = False Then
                                dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(j).Item("BIAR_ActionPlanDate"), "D")
                            End If
                            dRow("SourceofIssue") = "Branch Internal Audit"
                            If IsDBNull(dt.Rows(j)("EmployeeName")) = False Then
                                dRow("RLICEmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("EmployeeName"))
                            End If
                            If IsDBNull(dt.Rows(j)("BranchManager")) = False Then
                                dRow("BranchManager") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BranchManager"))
                            End If
                            dRow("Remarks") = ""
                            dtTable.Rows.Add(dRow)
                        Next
                    End If
                End If
            End If
            If iSourceofIssue = "3" Or iSourceofIssue = "0" Then 'BIA
                iAreaID = objDBL.SQLExecuteScalar(sAC, "Select cmm_id from content_management_master where cmm_Desc='Branch Risk Scoring Module' And CMM_CompID=" & iACID & "")
                If objDBL.SQLCheckForRecord(sAC, "Select CMAIM_ID from CMANoChecksIssue_mas where CMAIM_AsgID in(select CAD_ID from CMA_Assignment_Details where CAD_AuditYear=" & iYearID & " and CAD_CompID=" & iACID & ") And CMAIM_CompID=" & iACID & "") = True Then
                    sSql = "Select CMAI_CMDId,CMAI_ID,CMD_Id,CMAI_AObservation,MIM_Color,CMAI_CMDId_CMId,CMD_Annexure,CMAI_IssueHeading,CMAI_AAPlan,CMAI_OpenClose,CMD_AreaId,CMAI_ActionDate,CMAI_Responsibilty,CMAI_CMAIMId,CMAI_Status,"
                    sSql = sSql & " CM_CheckPointNo,ENT_ENTITYNAME,CMD_CheckPoints,cmm_Desc,CM_CheckPoint,CMD_RiskCategory,CMAIM_AsgID,CMD_FunctionId From CMANoChecksIssue"
                    sSql = sSql & " Left Join CMAChecksReport On CMAI_CMDId=CMD_Id Left Join cmacheckmaster On cm_id=CMAI_CMDId_CMId"
                    sSql = sSql & " Left outer Join MST_InherentRisk_Master On MIM_Name=CMD_RiskCategory And MIM_CompID=" & iACID & ""
                    sSql = sSql & " Left Join mst_entity_master On ENT_ID=CMD_FunctionId And ENT_CompID=" & iACID & ""
                    sSql = sSql & " Left Join content_management_master On cmm_ID=CMD_AreaId And CMM_CompID=" & iACID & ""
                    sSql = sSql & " Left Join CMANoChecksIssue_mas On CMAIM_ID=CMAI_CMAIMId And CMAIM_CompID=" & iACID & ""
                    sSql = sSql & " Where CMAI_CMAIMId In (Select CMAIM_ID from CMANoChecksIssue_mas"
                    sSql = sSql & " where CMAIM_ID = CMAI_CMAIMID And CMAIM_AsgID In(Select CMAIM_AsgID from CMA_Assignment_Details"
                    sSql = sSql & " Where CAD_ID=CMAIM_AsgID And CAD_AuditYear=" & iYearID & ")) And CMD_AreaId<>" & iAreaID & ""
                    sSql = sSql & " And CMD_YESNONA = 2 And (CMD_RiskScore = 2 Or CMD_RiskScore = 3 Or CMD_RiskScore = 0 Or CMD_RiskScore = 10 Or CMD_RiskScore = 20)"
                    sSql = sSql & " And CMD_Id = CMAI_CMDId And CMAI_Status='S'"
                    If iBRRRAsgID > 0 Then
                        sSql = sSql & " And CMAIM_BranchID=" & iBRRRAsgID & ""
                    End If
                    If iStatus > 0 Then
                        sSql = sSql & " And CMAI_OpenClose = " & iStatus & ""
                    End If
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If dt.Rows.Count > 0 Then
                        For k = 0 To dt.Rows.Count - 1
                            dRow = dtTable.NewRow
                            dRow("SrNo") = k + 1 + iBIACount
                            dRow("BRRDPKID") = dt.Rows(k).Item("CMAI_CMDId")
                            dRow("RCMID") = dt.Rows(k).Item("CMAI_CMDId_CMId")
                            dRow("AsgNo") = dt.Rows(k).Item("CMAIM_AsgID")
                            dRow("FunctionID") = dt.Rows(k).Item("CMD_FunctionId")
                            dRow("IssueTrackerNoID") = dt.Rows(k).Item("CMAI_ID")
                            If IsDBNull(dt.Rows(k)("ENT_ENTITYNAME")) = False Then
                                dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("ENT_ENTITYNAME"))
                            End If
                            dRow("AreaID") = dt.Rows(k).Item("CMD_AreaId")
                            If IsDBNull(dt.Rows(k)("cmm_Desc")) = False Then
                                dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("cmm_Desc"))
                            End If
                            If IsDBNull(dt.Rows(k)("CMD_Id")) = False Then
                                dRow("CheckPointsID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_Id"))
                            End If
                            If IsDBNull(dt.Rows(k)("CMD_CheckPoints")) = False Then
                                dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_CheckPoints"))
                            End If
                            If IsDBNull(dt.Rows(k)("CMAI_AObservation")) = False Then
                                dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMAI_AObservation"))
                            End If
                            If IsDBNull(dt.Rows(k)("CMD_Annexure")) = False Then
                                dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_Annexure"))
                            End If
                            If IsDBNull(dt.Rows(k)("CMD_RiskCategory")) = False Then
                                dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMD_RiskCategory"))
                                dRow("RiskCategoryColor") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("MIM_Color"))
                            End If
                            If IsDBNull(dt.Rows(k)("CMAI_AAPlan")) = False Then
                                dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(k).Item("CMAI_AAPlan"))
                            End If
                            If IsDBNull(dt.Rows(k)("CMAI_OpenClose")) = False Then
                                If dt.Rows(k)("CMAI_OpenClose") = 1 Then
                                    dRow("IOpenClose") = "Open"
                                ElseIf dt.Rows(k)("CMAI_OpenClose") = 2 Then
                                    dRow("IOpenClose") = "Closed"
                                ElseIf dt.Rows(k)("CMAI_OpenClose") = 3 Then
                                    dRow("IOpenClose") = "Open-Not Actioned"
                                End If
                            End If
                            If objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(k).Item("CMAI_ActionDate"), "D").Contains("1900") = False Then
                                dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(k).Item("CMAI_ActionDate"), "D")
                            End If
                            dRow("SourceofIssue") = "Branch Continuous Monitoring"
                            dRow("RLICEmployeeName") = ""
                            dRow("BranchManager") = ""
                            dRow("Remarks") = ""
                            dtTable.Rows.Add(dRow)
                        Next
                    End If
                End If
            End If
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRReportPGEDetailsID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRRAsgID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRRR_PGEDetailId From Risk_BRRReport Where BBRIT_CustID=" & iCustID & " And BRRR_AsgID=" & iBRRRAsgID & " And BRRR_YearID=" & iYearID & " And BRRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BRRR_PGEDetailId From Risk_BRRReport Where BRRR_YearID=" & iYearID & " And"
            If iAuditID > 0 Then
                sSql = sSql & " BRRR_AsgID=" & iAuditID & " And"
            End If
            sSql = sSql & " BRRR_CustID=" & iCustID & " And BRRR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_BRRReport Set BRRR_AttachID=" & iAttachID & ",BRRR_PGEDetailId=" & iPGDetailID & " Where "
            If iAuditID > 0 Then
                sSql = sSql & " BRRR_AsgID=" & iAuditID & " And"
            End If
            sSql = sSql & " BRRR_YearID=" & iYearID & " And BRRR_CustID=" & iCustID & " And BRRR_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class