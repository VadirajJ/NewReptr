Imports DatabaseLayer
Public Structure strBRR_Schedule
    Private iBRRS_PKID As Integer
    Private iBRRS_CustID As Integer
    Private sBRRS_AsgNo As String
    Private iBRRS_FinancialYear As Integer
    Private iBRRS_ScheduleMonth As Integer
    Private iBRRS_ZoneID As Integer
    Private iBRRS_RegionID As Integer
    Private iBRRS_BranchID As Integer
    Private iBRRS_ZonalMgrID As Integer
    Private iBRRS_BranchMgrID As Integer
    Private iBRRS_ReviewerTypeID As Integer
    Private iBRRS_EmployeeID As Integer
    Private sBRRS_Remarks As String
    Private iBRRS_AttchID As Integer
    Private sBRRS_Status As String
    Private iBRRS_CrBy As Integer
    Private iBRRS_UpdatedBy As Integer
    Private sBRRS_IPAddress As String
    Private iBRRS_CompID As Integer
    Public Property iBRRSPKID() As Integer
        Get
            Return (iBRRS_PKID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_PKID = Value
        End Set
    End Property
    Public Property iBRRSCustID() As Integer
        Get
            Return (iBRRS_CustID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_CustID = Value
        End Set
    End Property
    Public Property sBRRSAsgNo() As String
        Get
            Return (sBRRS_AsgNo)
        End Get
        Set(ByVal Value As String)
            sBRRS_AsgNo = Value
        End Set
    End Property
    Public Property iBRRSFinancialYear() As Integer
        Get
            Return (iBRRS_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_FinancialYear = Value
        End Set
    End Property
    Public Property iBRRSScheduleMonth() As Integer
        Get
            Return (iBRRS_ScheduleMonth)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_ScheduleMonth = Value
        End Set
    End Property
    Public Property iBRRSZoneID() As Integer
        Get
            Return (iBRRS_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_ZoneID = Value
        End Set
    End Property
    Public Property iBRRSRegionID() As Integer
        Get
            Return (iBRRS_RegionID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_RegionID = Value
        End Set
    End Property
    Public Property iBRRSBranchID() As Integer
        Get
            Return (iBRRS_BranchID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_BranchID = Value
        End Set
    End Property
    Public Property iBRRSZonalMgrID() As Integer
        Get
            Return (iBRRS_ZonalMgrID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_ZonalMgrID = Value
        End Set
    End Property
    Public Property iBRRSBranchMgrID() As Integer
        Get
            Return (iBRRS_BranchMgrID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_BranchMgrID = Value
        End Set
    End Property
    Public Property iBRRSReviewerTypeID() As Integer
        Get
            Return (iBRRS_ReviewerTypeID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_ReviewerTypeID = Value
        End Set
    End Property
    Public Property iBRRSEmployeeID() As Integer
        Get
            Return (iBRRS_EmployeeID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_EmployeeID = Value
        End Set
    End Property
    Public Property iBRRSAttchID() As Integer
        Get
            Return (iBRRS_AttchID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_AttchID = Value
        End Set
    End Property
    Public Property sBRRSRemarks() As String
        Get
            Return (sBRRS_Remarks)
        End Get
        Set(ByVal Value As String)
            sBRRS_Remarks = Value
        End Set
    End Property
    Public Property sBRRSStatus() As String
        Get
            Return (sBRRS_Status)
        End Get
        Set(ByVal Value As String)
            sBRRS_Status = Value
        End Set
    End Property
    Public Property iBRRSCrBy() As Integer
        Get
            Return (iBRRS_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_CrBy = Value
        End Set
    End Property
    Public Property iBRRSUpdatedBy() As String
        Get
            Return (iBRRS_UpdatedBy)
        End Get
        Set(ByVal Value As String)
            iBRRS_UpdatedBy = Value
        End Set
    End Property
    Public Property sBRRSIPAddress() As String
        Get
            Return (sBRRS_IPAddress)
        End Get
        Set(ByVal Value As String)
            sBRRS_IPAddress = Value
        End Set
    End Property
    Public Property iBRRSCompID() As Integer
        Get
            Return (iBRRS_CompID)
        End Get
        Set(ByVal Value As Integer)
            iBRRS_CompID = Value
        End Set
    End Property
End Structure
Public Class clsBRRSchedule
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadBRRPSDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("BranchRiskReviewCode")
            dt.Columns.Add("IRDACode")
            dt.Columns.Add("SalesUnitCode")
            dt.Columns.Add("BranchCode")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("Region")
            dt.Columns.Add("Zone")
            dt.Columns.Add("BranchManager")
            dt.Columns.Add("ZonalManager")
            dt.Columns.Add("RLICEmployeeCode")
            dt.Columns.Add("RLICEmployeeName")
            dt.Columns.Add("ScheduleMonth")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Status")
            dt.Columns.Add("AsgnID")
            dt.Columns.Add("BranchID")
            dt.Columns.Add("RegionID")
            dt.Columns.Add("ZoneID")
            dt.Columns.Add("ZonalMgrID")
            dt.Columns.Add("BranchMgrID")
            dt.Columns.Add("EmpID")

            sSql = "Select BRRP_PKID,BRRS_PKID,BRRS_AsgNo,BRRP_BranchID,BRRP_RegionID,BRRP_ZoneID,BRRP_Remarks,BRRS_Remarks,"
            sSql = sSql & " d.USR_FULLNAME As BranchManager,e.USR_FULLNAME As ZonalManager,f.usr_Code As RLICEmployeeCode,f.USR_FULLNAME As RLICEmployeeName,"
            sSql = sSql & " b.Org_Name As Region, c.Org_Name As Zone, a.Org_Name As Branch,Case When a.Org_Code Is Null Then '' Else a.Org_Code End IRDACode,"
            sSql = sSql & " Case When a.Org_SalesUnitCode IS Null Then '' Else a.Org_SalesUnitCode End SalesUnitCode,Case When a.Org_BranchCode IS Null Then '' Else a.Org_BranchCode End BranchCode,"
            sSql = sSql & " BRRS_Status,BRRS_BranchMgrID,BRRS_ZonalMgrID,BRRS_EmployeeID,BRRS_ScheduleMonth From Risk_BRRPlanning"
            sSql = sSql & " Left Join Risk_BRRSchedule On BRRS_CustID=" & iCustID & " And BRRS_BranchID=BRRP_BranchID And BRRP_RegionID=BRRS_RegionID"
            sSql = sSql & " And BRRP_ZoneID=BRRS_ZoneID And BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & ""
            sSql = sSql & " Left Join sad_org_structure a On a.org_node=BRRP_BranchID And a.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure b On b.org_node=BRRP_RegionID And b.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure c On c.org_node=BRRP_ZoneID And c.Org_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_userdetails d on d.Usr_ID=BRRS_BranchMgrID And d.Usr_CompId=" & iACID & ""
            sSql = sSql & " Left Join sad_userdetails e on e.Usr_ID=BRRS_ZonalMgrID And e.Usr_CompId=" & iACID & ""
            sSql = sSql & " Left Join sad_userdetails f on f.Usr_ID=BRRS_EmployeeID And f.Usr_CompId=" & iACID & ""
            sSql = sSql & " Where BRRP_CustId=" & iCustID & " And BRRP_YearId=" & iYearID & " And BRRP_AAPlan=1 And BRRP_Status='S' order by BRRP_PKID"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("BRRP_PKID")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("BRRS_AsgNo")) = False Then
                            dRow("BranchRiskReviewCode") = dtDetails.Rows(i)("BRRS_AsgNo")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("IRDACode")) = False Then
                            dRow("IRDACode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("IRDACode"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("SalesUnitCode")) = False Then
                            'objDBL.SQLGetDescription(sAC, "Select Case When Org_SalesUnitCode Is NULL Then '' else Org_SalesUnitCode End As SalesUnitCode from sad_org_structure where Org_CompID=" & iACID & " And org_node=" & dtDetails.Rows(i)("BRRP_BranchID") & "")
                            dRow("SalesUnitCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SalesUnitCode"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("BranchCode")) = False Then
                            'objDBL.SQLGetDescription(sAC, "Select Case When Org_BranchCode Is NULL then '' else Org_BranchCode End As BranchCode from sad_org_structure where Org_CompID=" & iACID & " And org_node=" & dtDetails.Rows(i)("BRRP_BranchID") & "")
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
                        If IsDBNull(dtDetails.Rows(i)("BranchManager")) = False Then
                            'objDBL.SQLGetDescription(sAC, "Select usr_FullName from Sad_Userdetails where Usr_CompId=" & iACID & " and usr_Id=" & dtDetails.Rows(i)("BRRS_BranchMgrID") & "")
                            dRow("BranchManager") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BranchManager"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("ZonalManager")) = False Then
                            'objDBL.SQLGetDescription(sAC, "Select usr_FullName from Sad_Userdetails where Usr_CompId=" & iACID & " and usr_Id=" & dtDetails.Rows(i)("BRRS_ZonalMgrID") & "")
                            dRow("ZonalManager") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("ZonalManager"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("RLICEmployeeName")) = False Then
                            'objDBL.SQLGetDescription(sAC, "Select usr_Code from Sad_Userdetails where Usr_CompId=" & iACID & " and usr_Id=" & dtDetails.Rows(i)("BRRS_EmployeeID") & "")
                            'objDBL.SQLGetDescription(sAC, "Select usr_FullName from Sad_Userdetails where Usr_CompId=" & iACID & " and usr_Id=" & dtDetails.Rows(i)("BRRS_EmployeeID") & "")
                            dRow("RLICEmployeeCode") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RLICEmployeeCode"))
                            dRow("RLICEmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RLICEmployeeName"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("BRRS_ScheduleMonth")) = False Then
                            dRow("ScheduleMonth") = objclsGeneralFunctions.GetMonthNameFromMothID(dtDetails.Rows(i)("BRRS_ScheduleMonth"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("BRRS_Remarks")) = False Then
                            dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("BRRS_Remarks"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("BRRS_Status")) = False Then
                            dRow("Status") = dtDetails.Rows(i)("BRRS_Status")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("BRRS_PKID")) = False Then
                            dRow("AsgnID") = dtDetails.Rows(i)("BRRS_PKID")
                        End If
                        dRow("BranchID") = dtDetails.Rows(i)("BRRP_BranchID")
                        dRow("RegionID") = dtDetails.Rows(i)("BRRP_RegionID")
                        dRow("ZoneID") = dtDetails.Rows(i)("BRRP_ZoneID")
                        If IsDBNull(dtDetails.Rows(i)("BRRS_ZonalMgrID")) = False Then
                            dRow("ZonalMgrID") = dtDetails.Rows(i)("BRRS_ZonalMgrID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("BRRS_BranchMgrID")) = False Then
                            dRow("BranchMgrID") = dtDetails.Rows(i)("BRRS_BranchMgrID")
                        End If
                        If IsDBNull(dtDetails.Rows(i)("BRRS_EmployeeID")) = False Then
                            dRow("EmpID") = dtDetails.Rows(i)("BRRS_EmployeeID")
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
    Public Function LoadBRRPlannedAsgNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select BRRS_PKID,BRRS_AsgNo from Risk_BRRSchedule where BRRS_CompID=" & iACID & " And BRRS_FinancialYear=" & iYearID & " order by BRRS_AsgNo"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBRRScheduledDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From Risk_BRRSchedule Where BRRS_CustID=" & iCustID & " And BRRS_FinancialYear=" & iYearID & " And BRRS_PKID=" & iPKID & " And  BRRS_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRScheduleDetails(ByVal sAC As String, ByVal objstrBRRSchedule As strBRR_Schedule) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_AsgNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objstrBRRSchedule.sBRRSAsgNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSFinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_ScheduleMonth", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSScheduleMonth
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_ZoneID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_RegionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSRegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_BranchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSBranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_ZonalMgrID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSZonalMgrID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_BranchMgrID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSBranchMgrID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_ReviewerTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSReviewerTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_EmployeeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSEmployeeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrBRRSchedule.sBRRSRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_AttchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSAttchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrBRRSchedule.sBRRSIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BRRS_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrBRRSchedule.iBRRSCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_BRRSchedule", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitBRRScheduledDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_BRRSchedule Set BRRS_Status='Submitted',BRRS_SubmittedBy=" & iUserID & ",BRRS_SubmittedOn=GetDate() Where BRRS_PKID=" & iPKID & " And BRRS_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadBRRPSDashboardToExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("IRDA Code")
            dt.Columns.Add("Branch Name")
            dt.Columns.Add("Branch Manager")
            dt.Columns.Add("Zonal Manager")
            dt.Columns.Add("RLIC Employee")
            dt.Columns.Add("Schedule Month")
            dt.Columns.Add("Remarks")

            sSql = "Select BRRP_PKID,a.Org_Name As Branch,Case When a.Org_Code Is Null Then '' Else a.Org_Code End IRDACode From Risk_BRRPlanning"
            sSql = sSql & " Left Join Risk_BRRSchedule On BRRS_BranchID=BRRP_BranchID And BRRP_RegionID=BRRS_RegionID And BRRP_ZoneID=BRRS_ZoneID And BRRS_CompID=" & iACID & ""
            sSql = sSql & " Left Join sad_org_structure a On a.org_node=BRRP_BranchID And a.Org_CompID=" & iACID & ""
            sSql = sSql & " Where BRRP_YearId=" & iYearID & " And BRRP_AAPlan=1 And BRRP_Status='S' And BRRP_branchID Not In"
            sSql = sSql & " (Select BRRS_BranchID from Risk_BRRSchedule where BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & ")"
            sSql = sSql & " order by BRRP_PKID"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtDetails.Rows(i)("BRRP_PKID")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("IRDACode")) = False Then
                            dRow("IRDA Code") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("IRDACode"))
                        End If
                        If IsDBNull(dtDetails.Rows(i)("Branch")) = False Then
                            dRow("Branch Name") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Branch"))
                        End If
                        dRow("Branch Manager") = ""
                        dRow("Zonal Manager") = ""
                        dRow("RLIC Employee") = ""
                        dRow("Schedule Month") = ""
                        dRow("Remarks") = ""
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(BRRP_PKID) from Risk_BRRPlanning where BRRP_CustId=" & iCustID & " And BRRP_CompID=" & iACID & " And BRRP_YearId=" & iYearID & " "
            sSql = sSql & " And BRRP_BranchID Not In(select BRRS_BranchID from Risk_BRRSchedule Where BRRS_CustID =" & iCustID & " And"
            sSql = sSql & " BRRS_FinancialYear=" & iYearID & " And BRRS_CompID=" & iACID & ") And BRRP_Status='S'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
