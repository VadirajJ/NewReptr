Imports DatabaseLayer
Public Structure strBRR_IssueTracker
    Dim iBRR_PKID As Integer
    Dim iBRR_CustID As Integer
    Dim iBRR_AsgID As Integer
    Dim iBRR_BranchId As Integer
    Dim iBRR_YearID As Integer
    Dim iBRR_CrBy As Integer
    Dim dBRR_ASDate As Date
    Dim dBRR_AEDate As Date
    Dim sBRR_Status As String
    Dim sBRR_Flag As String
    Dim sBRR_Title As String
    Dim sBRR_Remarks As String
    Dim sBRR_IPAddress As String
    Dim iBRR_CompID As Integer
    Dim iBRR_AttachID As Integer

    Dim iBBRIT_PKID As Integer
    Dim iBBRIT_BRRDPKID As Integer
    Dim iBBRIT_RCMID As Integer
    Dim iBBRIT_CustID As Integer
    Dim iBBRIT_AsgNo As Integer
    Dim iBBRIT_BranchId As Integer
    Dim iBBRIT_FunctionID As Integer
    Dim iBBRIT_AreaID As Integer
    Dim iBBRIT_CheckPointID As Integer
    Dim iBBRIT_FinancialYear As Integer
    Dim sBBRIT_IssueHeading As String
    Dim sBBRIT_IssueDesc As String
    Dim sBBRIT_ActionPlan As String
    Dim dBBRIT_TargetDate As Date
    Dim iBBRIT_OpenCloseStatus As Integer
    Dim iBBRIT_Responsible As Integer
    Dim sBBRIT_Remaks As String
    Dim iBBRIT_AttchID As Integer
    Dim iBBRIT_CrBy As Integer
    Dim iBBRIT_UpdatedBy As Integer
    Dim sBBRIT_Status As String
    Dim sBBRIT_DelFlag As String
    Dim sBBRIT_IPAddress As String
    Dim iBBRIT_CompID As Integer
    Dim iBRRITH_PKID As Integer
    Public Property iBRRPKID() As Integer
        Get
            Return (iBRR_PKID)
        End Get
        Set(ByVal Value As Integer)
            iBRR_PKID = Value
        End Set
    End Property
    Public Property iBRRCustID() As Integer
        Get
            Return (iBRR_CustID)
        End Get
        Set(ByVal Value As Integer)
            iBRR_CustID = Value
        End Set
    End Property
    Public Property iBRRAsgID() As Integer
        Get
            Return (iBRR_AsgID)
        End Get
        Set(ByVal Value As Integer)
            iBRR_AsgID = Value
        End Set
    End Property
    Public Property iBRRBranchId() As Integer
        Get
            Return (iBRR_BranchId)
        End Get
        Set(ByVal Value As Integer)
            iBRR_BranchId = Value
        End Set
    End Property
    Public Property iBRRYearID() As Integer
        Get
            Return (iBRR_YearID)
        End Get
        Set(ByVal Value As Integer)
            iBRR_YearID = Value
        End Set
    End Property
    Public Property iBRRCrBy() As Integer
        Get
            Return (iBRR_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iBRR_CrBy = Value
        End Set
    End Property
    Public Property dBRRASDate() As Date
        Get
            Return (dBRR_ASDate)
        End Get
        Set(ByVal Value As Date)
            dBRR_ASDate = Value
        End Set
    End Property
    Public Property dBRRAEDate() As Date
        Get
            Return (dBRR_AEDate)
        End Get
        Set(ByVal Value As Date)
            dBRR_AEDate = Value
        End Set
    End Property
    Public Property sBRRStatus() As String
        Get
            Return (sBRR_Status)
        End Get
        Set(ByVal Value As String)
            sBRR_Status = Value
        End Set
    End Property
    Public Property sBRRFlag() As String
        Get
            Return (sBRR_Flag)
        End Get
        Set(ByVal Value As String)
            sBRR_Flag = Value
        End Set
    End Property
    Public Property sBRRRemarks() As String
        Get
            Return (sBRR_Remarks)
        End Get
        Set(ByVal Value As String)
            sBRR_Remarks = Value
        End Set
    End Property
    Public Property sBRRTitle() As String
        Get
            Return (sBRR_Title)
        End Get
        Set(ByVal Value As String)
            sBRR_Title = Value
        End Set
    End Property
    Public Property sBRRIPAddress() As String
        Get
            Return (sBRR_IPAddress)
        End Get
        Set(ByVal Value As String)
            sBRR_IPAddress = Value
        End Set
    End Property
    Public Property iBRRCompID() As Integer
        Get
            Return (iBRR_CompID)
        End Get
        Set(ByVal Value As Integer)
            iBRR_CompID = Value
        End Set
    End Property
    Public Property iBRRAttachID() As Integer
        Get
            Return (iBRR_AttachID)
        End Get
        Set(ByVal Value As Integer)
            iBRR_AttachID = Value
        End Set
    End Property
    Public Property iBBRITCrBy() As Integer
        Get
            Return (iBBRIT_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_CrBy = Value
        End Set
    End Property
    Public Property iBBRITUpdatedBy() As Integer
        Get
            Return (iBBRIT_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_UpdatedBy = Value
        End Set
    End Property
    Public Property iBBRITPKID() As Integer
        Get
            Return (iBBRIT_PKID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_PKID = Value
        End Set
    End Property
    Public Property iBBRITBRRDPKID() As Integer
        Get
            Return (iBBRIT_BRRDPKID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_BRRDPKID = Value
        End Set
    End Property
    Public Property iBBRITRCMID() As Integer
        Get
            Return (iBBRIT_RCMID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_RCMID = Value
        End Set
    End Property
    Public Property iBBRITCustID() As Integer
        Get
            Return (iBBRIT_CustID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_CustID = Value
        End Set
    End Property
    Public Property iBBRITAsgNo() As Integer
        Get
            Return (iBBRIT_AsgNo)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_AsgNo = Value
        End Set
    End Property
    Public Property iBBRITBranchId() As Integer
        Get
            Return (iBBRIT_BranchId)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_BranchId = Value
        End Set
    End Property
    Public Property iBBRITFunctionID() As Integer
        Get
            Return (iBBRIT_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_FunctionID = Value
        End Set
    End Property
    Public Property iBBRITAreaID() As Integer
        Get
            Return (iBBRIT_AreaID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_AreaID = Value
        End Set
    End Property
    Public Property iBBRITCheckPointID() As Integer
        Get
            Return (iBBRIT_CheckPointID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_CheckPointID = Value
        End Set
    End Property
    Public Property iBBRITFinancialYear() As Integer
        Get
            Return (iBBRIT_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_FinancialYear = Value
        End Set
    End Property
    Public Property sBBRITIssueHeading() As String
        Get
            Return (sBBRIT_IssueHeading)
        End Get
        Set(ByVal Value As String)
            sBBRIT_IssueHeading = Value
        End Set
    End Property
    Public Property sBBRITIssueDesc() As String
        Get
            Return (sBBRIT_IssueDesc)
        End Get
        Set(ByVal Value As String)
            sBBRIT_IssueDesc = Value
        End Set
    End Property
    Public Property sBBRITActionPlan() As String
        Get
            Return (sBBRIT_ActionPlan)
        End Get
        Set(ByVal Value As String)
            sBBRIT_ActionPlan = Value
        End Set
    End Property
    Public Property dBBRITTargetDate() As Date
        Get
            Return (dBBRIT_TargetDate)
        End Get
        Set(ByVal Value As Date)
            dBBRIT_TargetDate = Value
        End Set
    End Property
    Public Property iBBRITOpenCloseStatus() As Integer
        Get
            Return (iBBRIT_OpenCloseStatus)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_OpenCloseStatus = Value
        End Set
    End Property
    Public Property iBBRITResponsible() As Integer
        Get
            Return (iBBRIT_Responsible)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_Responsible = Value
        End Set
    End Property
    Public Property sBBRITRemaks() As String
        Get
            Return (sBBRIT_Remaks)
        End Get
        Set(ByVal Value As String)
            sBBRIT_Remaks = Value
        End Set
    End Property
    Public Property iBBRITAttchID() As Integer
        Get
            Return (iBBRIT_AttchID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_AttchID = Value
        End Set
    End Property
    Public Property sBBRITStatus() As String
        Get
            Return (sBBRIT_Status)
        End Get
        Set(ByVal Value As String)
            sBBRIT_Status = Value
        End Set
    End Property
    Public Property sBBRITDelFlag() As String
        Get
            Return (sBBRIT_DelFlag)
        End Get
        Set(ByVal Value As String)
            sBBRIT_DelFlag = Value
        End Set
    End Property
    Public Property sBBRITIPAddress() As String
        Get
            Return (sBBRIT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sBBRIT_IPAddress = Value
        End Set
    End Property
    Public Property iBBRITCompID() As Integer
        Get
            Return (iBBRIT_CompID)
        End Get
        Set(ByVal Value As Integer)
            iBBRIT_CompID = Value
        End Set
    End Property
    Public Property iBRRITHPKID() As Integer
        Get
            Return (iBRRITH_PKID)
        End Get
        Set(ByVal Value As Integer)
            iBRRITH_PKID = Value
        End Set
    End Property
End Structure
Public Class clsBRRIssueTracker
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsBRRChecklist As New clsBRRChecklist
    Public Function LoadChecklistBRRAsgNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sSearch As String, ByVal iCheckUserAsgNo As Integer, ByVal iFUNUserID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select BRRS_PKID,BRRS_AsgNo +' ('+ Org_Name + ') 'as AsgNo from Risk_BRRSchedule Left Join Risk_BRRChecklist_Mas On BRR_AsgID=BRRS_PKID"
            If iCustID > 0 Then
                sSql = sSql & " And BRR_CustID=" & iCustID & ""
            End If
            sSql = sSql & " Left Join sad_org_structure On BRRS_BranchID=Org_Node Where BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & " And BRR_Status='S' "
            If iCustID > 0 Then
                sSql = sSql & " And BRRS_CustID=" & iCustID & ""
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (Org_Name like '" & sSearch & "%' or Org_Code like '" & sSearch & "%')"
            End If
            If iCheckUserAsgNo > 0 Then
                sSql = sSql & " And (BRRS_ZonalMgrID=" & iCheckUserAsgNo & " Or BRRS_BranchMgrID=" & iCheckUserAsgNo & " Or BRRS_EmployeeID=" & iCheckUserAsgNo & ")"
            End If
            If iFUNUserID > 0 Then
                sSql = sSql & " And BRRS_PKID in (Select BRR_AsgID from Risk_BRRChecklist_Mas where BRR_PKID in (Select BRRD_BRRPKID From Risk_BRRChecklist_Details Where BRRD_YESNONA=2 And BRRD_FunctionID in "
                sSql = sSql & " (Select ENT_ID From mst_Entity_master Where (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ") and ENT_CompID=" & iACID & "))"
                If iCustID > 0 Then
                    sSql = sSql & " And BRR_CustID=" & iCustID & ")"
                Else
                    sSql = sSql & " )"
                End If
            End If
            sSql = sSql & " order by BRRS_AsgNo"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRITStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select BBRIT_Status From Risk_BRRIssueTracker where BBRIT_CustID=" & iCustID & " And BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckIsFUNHODManagerSPOCInChecklistYESNONA2(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select BRR_AsgID from Risk_BRRChecklist_Mas where BRR_PKID in (Select BRRD_BRRPKID From Risk_BRRChecklist_Details Where BRRD_YESNONA=2 And BRRD_FunctionID in "
            sSql = sSql & " (Select ENT_ID From mst_Entity_master Where (ENT_FunownerID=" & iUserID & " Or Ent_FunManagerID= " & iUserID & " Or Ent_FunSPOCID= " & iUserID & ") and ENT_CompID=" & iACID & "))"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt.Rows.Count
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBRRAllIssueTrackerStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer, ByVal iIssueTrackerID As Integer) As DataTable
        Dim dRow As DataRow
        Dim dtTable As New DataTable, dt As New DataTable
        Dim sSql As String
        Dim i As Integer, iAreaID As Integer
        Try
            dtTable.Columns.Add("SlNo")
            dtTable.Columns.Add("BRRDPKID")
            dtTable.Columns.Add("RCMID")
            dtTable.Columns.Add("CustName")
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
            dtTable.Columns.Add("AAPlan")
            dtTable.Columns.Add("ADate")
            dtTable.Columns.Add("OpenCloseStatus")
            dtTable.Columns.Add("InherentRiskColor")
            dtTable.Columns.Add("Status")
            dtTable.Columns.Add("Remarks")
            dtTable.Columns.Add("IssueTrackerNoID")
            iAreaID = objDBL.SQLExecuteScalarInt(sAC, "Select cmm_id from content_management_master where cmm_Desc='Branch Risk Scoring Module' And CMM_CompID=" & iACID & "")
            If objDBL.SQLCheckForRecord(sAC, "Select BBRIT_PKID from Risk_BRRIssueTracker where BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_CompID=" & iACID & "") = True Then
                sSql = "Select BBRIT_BRRDPKID,BBRIT_RCMID,BBRIT_PKID,Cust_Name,BBRIT_AsgNo,BRRS_AsgNo,BBRIT_TargetDate,BRRD_FunctionID,BRRD_AreaID,BRRD_CheckPoint,"
                sSql = sSql & " BRRD_IssueDetails,BRRD_Annexure,BRRD_RiskCategory,BBRIT_ActionPlan,BBRIT_OpenCloseStatus,BBRIT_Status,BBRIT_Remaks,"
                sSql = sSql & " ENT_ENTITYNAME,cmm_Desc,RCM_ID From Risk_BRRChecklist_Details"
                sSql = sSql & " Left Join Risk_BRRIssueTracker On BRRD_PKID=BBRIT_BRRDPKID And BBRIT_CustID=" & iCustID & " And BBRIT_CompID=" & iACID & ""
                sSql = sSql & " Left Join Risk_BRRSchedule On BRRS_PKID=BBRIT_AsgNo And BRRS_CustID=" & iCustID & " And BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & ""
                sSql = sSql & " Left Join mst_entity_master On ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & ""
                sSql = sSql & " Left Join content_management_master On cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & ""
                sSql = sSql & " Left Join Risk_CheckList_Master On RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " And RCM_YearID=" & iYearID & " And RCM_CustID=" & iCustID & ""
                sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=BBRIT_CustID And Cust_Compid=" & iACID & ""
                sSql = sSql & " Where BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_CompID=" & iACID & " And BRRD_YESNONA=2 And"
                sSql = sSql & " (BRRD_RiskScore=2 Or BRRD_RiskScore=3 Or BRRD_RiskScore=0 Or BRRD_RiskScore=10 Or BRRD_RiskScore=20)"
                sSql = sSql & " And BRRD_AreaID<>" & iAreaID & " And BBRIT_CompID=" & iACID & " And RCM_YearID=" & iYearID & ""
                If iIssueTrackerID > 0 Then
                    sSql = sSql & " And BRRD_PKID=" & iIssueTrackerID & ""
                End If
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dRow = dtTable.NewRow
                        dRow("BRRDPKID") = dt.Rows(i).Item("BBRIT_BRRDPKID")
                        dRow("RCMID") = dt.Rows(i).Item("BBRIT_RCMID")
                        dRow("CustName") = dt.Rows(i).Item("Cust_Name")
                        dRow("AsgNo") = dt.Rows(i).Item("BRRS_AsgNo")
                        dRow("IssueTrackerNoID") = dt.Rows(i)("BBRIT_PKID")
                        dRow("SlNo") = i + 1
                        dRow("FunctionID") = dt.Rows(i).Item("BRRD_FunctionID")
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
                        dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
                        dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
                        dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
                        dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
                        dRow("AAPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BBRIT_ActionPlan"))
                        dRow("InherentRiskColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, dRow("Risk"))
                        dRow("Status") = dt.Rows(i).Item("BBRIT_Status")
                        If IsDBNull(dt.Rows(i)("BBRIT_Remaks")) = False Then
                            dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("BBRIT_Remaks"))
                        End If

                        If IsDBNull(dt.Rows(i)("BBRIT_OpenCloseStatus")) = False Then
                            If dt.Rows(i)("BBRIT_OpenCloseStatus") = 1 Then
                                dRow("OpenCloseStatus") = "Open"
                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 2 Then
                                dRow("OpenCloseStatus") = "Closed"
                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 3 Then
                                dRow("OpenCloseStatus") = "Open-Not Actioned"
                            End If
                        End If
                        If objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "D").Contains("1900") = False Then
                            dRow("ADate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "D")
                        End If
                        dtTable.Rows.Add(dRow)
                    Next
                End If
            End If
            If objDBL.SQLCheckForRecord(sAC, "Select BRRD_PKID from Risk_BRRChecklist_Details where BRRD_BRRPKID in (Select BRR_PKID from Risk_BRRChecklist_Mas where BRR_AsgID=" & iBRRAsgID & ")") = True Then
                sSql = "Select BRRD_PKID,BRRD_RCMID,Cust_Name,BRRS_AsgNo,BRRD_FunctionID,ENT_ENTITYNAME,BRRD_AreaID,cmm_Desc,RCM_ID,BRRD_CheckPoint,BRRD_IssueDetails,BRRD_Annexure,BRRD_RiskCategory "
                sSql = sSql & " from  Risk_BRRChecklist_Details Left Join mst_entity_master On ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & ""
                sSql = sSql & " Left Join Risk_BRRSchedule On BRRS_PKID=" & iBRRAsgID & " And BRRS_CustID=" & iCustID & " And BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & ""
                sSql = sSql & " Left Join content_management_master On cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & " Left Join Risk_CheckList_Master On RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " And RCM_YearID=" & iYearID & ""
                sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=BRRS_CustID And Cust_Compid=" & iACID & ""
                sSql = sSql & " where BRRD_PKID not in (Select BBRIT_BRRDPKID from Risk_BRRIssueTracker where BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_CustID=" & iCustID & ")"
                sSql = sSql & " and BRRD_YESNONA=2 and BRRD_BRRPKID in (Select BRR_PKID from Risk_BRRChecklist_Mas where BRR_CustID=" & iCustID & " And BRR_AsgID=" & iBRRAsgID & " And BRR_YearID=" & iYearID & ")"
                sSql = sSql & " And BRRD_AreaID<>" & iAreaID & ""
                If iIssueTrackerID > 0 Then
                    sSql = sSql & " And BRRD_PKID=" & iIssueTrackerID & ""
                End If
                sSql = sSql & " order by BRRD_PKID"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For j = 0 To dt.Rows.Count - 1
                        dRow = dtTable.NewRow
                        dRow("BRRDPKID") = dt.Rows(j).Item("BRRD_PKID")
                        dRow("RCMID") = dt.Rows(j).Item("BRRD_RCMID")
                        dRow("CustName") = dt.Rows(j).Item("Cust_Name")
                        dRow("AsgNo") = dt.Rows(j).Item("BRRS_AsgNo")
                        dRow("IssueTrackerNoID") = 0
                        dRow("SlNo") = i + j + 1
                        dRow("FunctionID") = dt.Rows(j).Item("BRRD_FunctionID")
                        If IsDBNull(dt.Rows(j)("ENT_ENTITYNAME")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("ENT_ENTITYNAME"))
                        End If
                        dRow("AreaID") = dt.Rows(j).Item("BRRD_AreaID")
                        If IsDBNull(dt.Rows(j)("cmm_Desc")) = False Then
                            dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("cmm_Desc"))
                        End If
                        If IsDBNull(dt.Rows(j)("RCM_ID")) = False Then
                            dRow("CheckPointsID") = dt.Rows(j).Item("RCM_ID")
                        End If
                        dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BRRD_CheckPoint"))
                        dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BRRD_IssueDetails"))
                        dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BRRD_Annexure"))
                        dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(j).Item("BRRD_RiskCategory"))
                        dRow("InherentRiskColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, dRow("Risk"))
                        dRow("AAPlan") = ""
                        dRow("OpenCloseStatus") = "Open"
                        dRow("ADate") = ""
                        dRow("Remarks") = ""

                        dtTable.Rows.Add(dRow)
                    Next
                End If
            End If
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBRRFollowUpDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer, ByVal iIssueTrackerID As Integer) As DataTable
        Dim dRow As DataRow
        Dim dtTable As New DataTable, dt As New DataTable
        Dim sSql As String
        Dim i As Integer, iAreaID As Integer
        Try
            dtTable.Columns.Add("SlNo")
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
            dtTable.Columns.Add("AAPlan")
            dtTable.Columns.Add("ADate")
            dtTable.Columns.Add("OpenCloseStatus")
            dtTable.Columns.Add("OpenCloseStatusID")
            dtTable.Columns.Add("InherentRiskColor")
            dtTable.Columns.Add("Status")
            dtTable.Columns.Add("Remarks")
            dtTable.Columns.Add("IssueTrackerNoID")
            iAreaID = objDBL.SQLExecuteScalarInt(sAC, "Select cmm_id from content_management_master where cmm_Desc='Branch Risk Scoring Module' And CMM_CompID=" & iACID & "")
            If objDBL.SQLCheckForRecord(sAC, "Select BBRIT_PKID from Risk_BRRIssueTracker where BBRIT_CustID=" & iCustID & " And BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_CompID=" & iACID & "") = True Then
                sSql = "Select BBRIT_BRRDPKID,BBRIT_PKID,BBRIT_RCMID,BBRIT_AsgNo,BRRS_AsgNo,BBRIT_TargetDate,BBRIT_FunctionID,BBRIT_AreaID,BBRIT_CheckPointID,BRRD_CheckPoint,BRRD_IssueDetails,"
                sSql = sSql & " BRRD_Annexure,BRRD_RiskCategory,BBRIT_ActionPlan, BBRIT_OpenCloseStatus, BBRIT_Status, BBRIT_Remaks, ENT_ENTITYNAME, cmm_Desc, RCM_ID From Risk_BRRIssueTracker"
                sSql = sSql & " Left Join Risk_BRRChecklist_Details On BRRD_PKID=BBRIT_BRRDPKID And BRRD_CompID=" & iACID & ""
                sSql = sSql & " Left Join Risk_BRRSchedule On BRRS_PKID=BBRIT_AsgNo And BRRS_CustID=" & iCustID & " And  BRRS_CompID=" & iACID & ""
                sSql = sSql & " Left Join mst_entity_master On ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & " Left Join content_management_master On cmm_ID=BRRD_AreaID And"
                sSql = sSql & " CMM_CompID=" & iACID & " Left Join Risk_CheckList_Master On RCM_CheckPoint=BRRD_CheckPoint And RCM_CompID=" & iACID & " "
                sSql = sSql & " Where BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_CustID=" & iCustID & " And BBRIT_CompID=" & iACID & ""
                sSql = sSql & " And BRRD_YESNONA = 2 And (BRRD_RiskScore = 2 Or BRRD_RiskScore = 3 Or BRRD_RiskScore = 0 Or BRRD_RiskScore = 10 Or BRRD_RiskScore = 20)"
                sSql = sSql & "  And BRRD_AreaID<>" & iAreaID & " And BBRIT_CompID =" & iACID & " And BBRIT_PKID=" & iIssueTrackerID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dRow = dtTable.NewRow
                        dRow("BRRDPKID") = dt.Rows(i).Item("BBRIT_BRRDPKID")
                        dRow("RCMID") = dt.Rows(i).Item("BBRIT_RCMID")
                        dRow("AsgNo") = dt.Rows(i).Item("BRRS_AsgNo")
                        dRow("IssueTrackerNoID") = dt.Rows(i)("BBRIT_PKID")
                        dRow("SlNo") = i + 1
                        dRow("FunctionID") = dt.Rows(i).Item("BBRIT_FunctionID")
                        If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
                        End If
                        dRow("AreaID") = dt.Rows(i).Item("BBRIT_AreaID")
                        If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
                            dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
                        End If
                        If IsDBNull(dt.Rows(i)("RCM_ID")) = False Then
                            dRow("CheckPointsID") = dt.Rows(i).Item("RCM_ID")
                        End If
                        dRow("CheckPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
                        dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
                        dRow("Annexure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
                        dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
                        dRow("AAPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BBRIT_ActionPlan"))
                        dRow("InherentRiskColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, dRow("Risk"))
                        dRow("Status") = dt.Rows(i).Item("BBRIT_Status")
                        If IsDBNull(dt.Rows(i)("BBRIT_Remaks")) = False Then
                            dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("BBRIT_Remaks"))
                        End If
                        If IsDBNull(dt.Rows(i)("BBRIT_OpenCloseStatus")) = False Then
                            dRow("OpenCloseStatusID") = dt.Rows(i)("BBRIT_OpenCloseStatus")
                            If dt.Rows(i)("BBRIT_OpenCloseStatus") = 1 Then
                                dRow("OpenCloseStatus") = "Open"
                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 2 Then
                                dRow("OpenCloseStatus") = "Closed"
                            ElseIf dt.Rows(i)("BBRIT_OpenCloseStatus") = 3 Then
                                dRow("OpenCloseStatus") = "Open-Not Actioned"
                            End If
                        End If
                        If objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "D").Contains("1900") = False Then
                            dRow("ADate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i).Item("BBRIT_TargetDate"), "D")
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
    Public Function GetBRRIssueTrackerAttachID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer, ByVal iBRRDPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BBRIT_AttchID From Risk_BRRIssueTracker Where BBRIT_CustID=" & iCustID & " And BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_BRRDPKID=" & iBRRDPKID & " And BBRIT_FinancialYear=" & iYearID & " And BBRIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRIssueTrackerOpenCloseCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer, ByVal iYearID As Integer) As Integer
        Dim ssql As String
        Try
            ssql = "Select Count(*) From Risk_BRRIssueTracker Where BBRIT_CustID=" & iCustID & " And BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_Status='S' And BBRIT_OpenCloseStatus=2 And BBRIT_FinancialYear=" & iYearID & " And BBRIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRHistoryGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iBBRITPKID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("ActionPlan")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("Remarks")
            sSql = "Select BRRITH_PKID,BRRITH_ActionPlan,BRRITH_OpenCloseStatus,BRRITH_Remarks,Convert(Varchar(10),BRRITH_TargetDate,103)BRRITH_TargetDate From Risk_BRRIssueTracker_History"
            sSql = sSql & " Where BRRITH_CompID=" & iACID & " And BRRITH_CustID=" & iCustID & " And BRRITH_BBRITPKID=" & iBBRITPKID & " Order by BRRITH_PKID Desc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BRRITH_ActionPlan"))
                    dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BRRITH_Remarks"))
                    dRow("TargetDate") = dtDetails.Rows(i)("BRRITH_TargetDate")
                    If dtDetails.Rows(i)("BRRITH_OpenCloseStatus") = 1 Then
                        dRow("Status") = "Open"
                    ElseIf dtDetails.Rows(i)("BRRITH_OpenCloseStatus") = 2 Then
                        dRow("Status") = "Closed"
                    ElseIf dtDetails.Rows(i)("BRRITH_OpenCloseStatus") = 3 Then
                        dRow("Status") = "Open - Not Actioned"
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRIssueTrachker(ByVal sAC As String, ByVal objstrBRRIssueTracker As strBRR_IssueTracker) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_BRRDPKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITBRRDPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_RCMID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITRCMID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_AsgNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITAsgNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_BranchId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITBranchId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_AreaID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITAreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_CheckPointID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITCheckPointID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITFinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_IssueHeading", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITIssueHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_IssueDesc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITIssueDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_ActionPlan", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.dBBRITTargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_OpenCloseStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITOpenCloseStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_Responsible", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITResponsible
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_Remaks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITRemaks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_AttchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITAttchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BBRIT_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_BRRIssueTracker", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRIssueTrackerHistory(ByVal sAC As String, ByVal objstrBRRIssueTracker As strBRR_IssueTracker, ByVal sSourceName As String, ByVal iBRRITPKID As Integer) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRITH_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBRRITHPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRITH_BBRITPKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iBRRITPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRITH_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRITH_AsgNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITAsgNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_ActionPlan", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.dBBRITTargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_OpenCloseStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITOpenCloseStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITRemaks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.sBBRITIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRIT_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrBRRIssueTracker.iBBRITCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_BRRIssueTracker_History", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRIssueTrackerToExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iBRRCustID As Integer, ByVal iBRRAsgID As Integer) As DataTable
        Dim dRow As DataRow
        Dim dtTable As New DataTable, dt As New DataTable
        Dim sSql As String
        Dim i As Integer, iAreaID As Integer
        Try
            dtTable.Columns.Add("Function")
            dtTable.Columns.Add("Area")
            dtTable.Columns.Add("Check Point")
            dtTable.Columns.Add("Issue Details")
            dtTable.Columns.Add("Annexure No")
            dtTable.Columns.Add("Risk Category")
            dtTable.Columns.Add("Agreed Action Plan")
            dtTable.Columns.Add("Target Date")
            dtTable.Columns.Add("Issue StatusOpen/Closed/Open - Not Actioned (1/2/3)")

            iAreaID = objDBL.SQLExecuteScalarInt(sAC, "Select cmm_id from content_management_master where cmm_Desc='Branch Risk Scoring Module' And CMM_CompID=" & iACID & "")
            If objDBL.SQLCheckForRecord(sAC, "Select BRRD_PKID from Risk_BRRChecklist_Details where BRRD_BRRPKID in (Select BRR_PKID from Risk_BRRChecklist_Mas where BRR_CustID =" & iBRRCustID & " And BRR_AsgID=" & iBRRAsgID & ")") = True Then
                sSql = "Select ENT_ENTITYNAME,cmm_Desc,BRRD_CheckPoint,BRRD_IssueDetails,BRRD_Annexure,BRRD_RiskCategory from Risk_BRRChecklist_Details "
                sSql = sSql & " Left Join mst_entity_master ON ENT_ID=BRRD_FunctionID And ENT_CompID=" & iACID & " Left Join content_management_master On cmm_ID=BRRD_AreaID And CMM_CompID=" & iACID & ""
                sSql = sSql & " Where BRRD_PKID Not In (Select BBRIT_BRRDPKID from Risk_BRRIssueTracker where BBRIT_CustID=" & iBRRCustID & " And BBRIT_AsgNo=" & iBRRAsgID & ") "
                sSql = sSql & " And BRRD_YESNONA=2 And BRRD_BRRPKID In (Select BRR_PKID from Risk_BRRChecklist_Mas where BRR_CustID =" & iBRRCustID & " And BRR_AsgID=" & iBRRAsgID & ") "
                sSql = sSql & " And BRRD_AreaID<>" & iAreaID & " order by BRRD_PKID"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dRow = dtTable.NewRow
                        If IsDBNull(dt.Rows(i)("ENT_ENTITYNAME")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("ENT_ENTITYNAME"))
                        End If
                        If IsDBNull(dt.Rows(i)("cmm_Desc")) = False Then
                            dRow("Area") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("cmm_Desc"))
                        End If
                        dRow("Check Point") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_CheckPoint"))
                        dRow("Issue Details") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_IssueDetails"))
                        dRow("Annexure No") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_Annexure"))
                        dRow("Risk Category") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("BRRD_RiskCategory"))
                        dRow("Agreed Action Plan") = ""
                        dRow("Target Date") = ""
                        dRow("Issue StatusOpen/Closed/Open - Not Actioned (1/2/3)") = ""
                        dtTable.Rows.Add(dRow)
                    Next
                End If
            End If
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChecklistDetailsToDraftFinal(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iBRRCustID As Integer, ByVal iBRRAsgID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtMaster As New DataTable
        Dim dRow As DataRow
        Dim OverAllScore As Double = 0, CScore As Double = 0, SScore As Double = 0
        Try
            dt.Columns.Add("EmployeeName")
            dt.Columns.Add("IRDA")
            dt.Columns.Add("Title")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("EndDate")
            dt.Columns.Add("CScore")
            dt.Columns.Add("CRating")
            dt.Columns.Add("SScore")
            dt.Columns.Add("SRating")
            dt.Columns.Add("OScore")
            dt.Columns.Add("ORating")

            sSql = " Select BRRS_PKID,Org_Code,usr_FullName,BRR_Title,Convert(Varchar(10),BRR_ASDate,103)BRR_ASDate,Convert(Varchar(10),BRR_AEDate,103)BRR_AEDate,Sum(BRRD_WeightedRiskScore)as OverAllcore"
            sSql = sSql & " From Sad_UserDetails Left Join Risk_BRRSchedule On BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_BRRChecklist_Mas On BRR_AsgID=BRRS_PKID And BRR_CustID =" & iBRRCustID & " And BRR_CompID=" & iACID & " And BRR_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Risk_BRRChecklist_Details On  BRRD_CompID=1 And BRRD_BRRPKID In"
            sSql = sSql & " (select BRR_PKID from Risk_BRRChecklist_Mas where BRR_AsgID=" & iBRRAsgID & " And BRR_CustID =" & iBRRCustID & " And BRR_CompID=" & iACID & " And BRR_YearID=" & iYearID & ")"
            sSql = sSql & " Left Join Sad_org_Structure On Org_Node=BRR_BranchId And org_CompID=" & iACID & ""
            sSql = sSql & " where usr_Id=BRRS_EmployeeID And BRRS_PKID=" & iBRRAsgID & " And BRRS_CustID =" & iBRRCustID & " And BRR_status='S' Group by usr_FullName,BRR_Title,BRR_ASDate,BRR_AEDate,BRRS_PKID,Org_Code"
            dtMaster = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtMaster.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dtMaster.Rows(i)("usr_FullName")) = False Then
                    dRow("EmployeeName") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("usr_FullName"))
                End If
                If IsDBNull(dtMaster.Rows(i)("Org_Code")) = False Then
                    dRow("IRDA") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("Org_Code"))
                End If
                If IsDBNull(dtMaster.Rows(i)("BRR_Title")) = False Then
                    dRow("Title") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("BRR_Title"))
                End If
                If IsDBNull(dtMaster.Rows(i)("BRR_ASDate")) = False Then
                    dRow("StartDate") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("BRR_ASDate"))
                End If
                If IsDBNull(dtMaster.Rows(i)("BRR_AEDate")) = False Then
                    dRow("EndDate") = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("BRR_AEDate"))
                End If
                If IsDBNull(dtMaster.Rows(i)("OverAllcore")) = False Then
                    OverAllScore = objclsGRACeGeneral.SafeSQL(dtMaster.Rows(i)("OverAllcore"))
                    If OverAllScore > 0 Then
                        dRow("ORating") = objclsAllActiveMaster.GetOverAllRiskRatingNameColor(sAC, iACID, iYearID, Val(OverAllScore), "Name")
                        If dRow("ORating") = "Very High" Then
                            OverAllScore = Val(OverAllScore) - 10
                        ElseIf dRow("ORating") = "High" Then
                            OverAllScore = Val(OverAllScore) - 5
                        End If
                    End If
                    dRow("OScore") = OverAllScore
                End If
                If IsDBNull(dtMaster.Rows(i)("BRRS_PKID")) = False Then
                    dRow("CScore") = objclsBRRChecklist.GetBRRCoreAndSupportProcessAuditScore(sAC, iACID, dtMaster.Rows(i)("BRRS_PKID"), "C")
                    If dRow("CScore") > 0 Then
                        dRow("CRating") = objclsAllActiveMaster.GetCoreAndSupportProcessRating(sAC, iACID, dRow("CScore"), iYearID, "C")
                    End If
                    dRow("SScore") = objclsBRRChecklist.GetBRRCoreAndSupportProcessAuditScore(sAC, iACID, dtMaster.Rows(i)("BRRS_PKID"), "S")
                    If dRow("SScore") > 0 Then
                        dRow("SRating") = objclsAllActiveMaster.GetCoreAndSupportProcessRating(sAC, iACID, dRow("SScore"), iYearID, "S")
                    End If
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChecklistBRRAsgNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sSearch As String, ByVal iCheckUserAsgNo As Integer, ByVal iFUNUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select BRRS_PKID,BRRS_AsgNo +' ('+ Org_Name + ') 'as AsgNo from Risk_BRRSchedule Left Join Risk_BRRChecklist_Mas On BRR_AsgID=BRRS_PKID Left Join sad_org_structure"
            sSql = sSql & " On BRRS_BranchID=Org_Node Where BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & " And BRR_Status='S'"
            If sSearch <> "" Then
                sSql = sSql & " And (Org_Name like '" & sSearch & "%' or Org_Code like '" & sSearch & "%')"
            End If
            If iCheckUserAsgNo > 0 Then
                sSql = sSql & " And (BRRS_ZonalMgrID=" & iCheckUserAsgNo & " Or BRRS_BranchMgrID=" & iCheckUserAsgNo & " Or BRRS_EmployeeID=" & iCheckUserAsgNo & ")"
            End If
            If iFUNUserID > 0 Then
                sSql = sSql & " And BRRS_PKID in (Select BRR_AsgID from Risk_BRRChecklist_Mas where BRR_PKID in (Select BRRD_BRRPKID From Risk_BRRChecklist_Details Where BRRD_YESNONA=2 And BRRD_FunctionID in "
                sSql = sSql & " (Select ENT_ID From mst_Entity_master Where (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ") and ENT_CompID=" & iACID & ")))"
            End If
            sSql = sSql & " order by BRRS_AsgNo"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRIssueTrackerPGEDetailsID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBRRAsgID As Integer, ByVal iBRRDPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BBRIT_PGEDetailId From Risk_BRRIssueTracker Where BBRIT_CustID=" & iCustID & " And BBRIT_AsgNo=" & iBRRAsgID & " And BBRIT_BRRDPKID=" & iBRRDPKID & " And BBRIT_FinancialYear=" & iYearID & " And BBRIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iBRRDPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select BBRIT_PGEDetailId From Risk_BRRIssueTracker Where BBRIT_FinancialYear=" & iYearID & " And"
            If iAuditID > 0 Then
                sSql = sSql & " BBRIT_AsgNo=" & iAuditID & " And"
            End If
            If iBRRDPKID > 0 Then
                sSql = sSql & " BBRIT_PKID=" & iBRRDPKID & " And"
            End If
            sSql = sSql & " BBRIT_CustID=" & iCustID & " And BBRIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer, ByVal iBRRDPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_BRRIssueTracker Set BBRIT_AttchID=" & iAttachID & ",BBRIT_PGEDetailId=" & iPGDetailID & " Where "
            If iAuditID > 0 Then
                sSql = sSql & " BBRIT_AsgNo=" & iAuditID & " And"
            End If
            If iBRRDPKID > 0 Then
                sSql = sSql & " BBRIT_PKID=" & iBRRDPKID & " And"
            End If
            sSql = sSql & " BBRIT_FinancialYear=" & iYearID & " And BBRIT_CustID=" & iCustID & " And BBRIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
