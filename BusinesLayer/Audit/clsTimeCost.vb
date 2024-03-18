Imports DatabaseLayer
Public Structure strTimeCostBudgetDetails
    Dim ATCD_PKID As Integer
    Dim ATCD_ATCBID As Integer
    Dim ATCD_YearID As Integer
    Dim ATCD_Type As String
    Dim ATCD_TaskProcessID As Integer
    Dim ATCD_AuditCodeID As Integer
    Dim ATCD_UserID As Integer
    Dim ATCD_Hours As Integer
    Dim ATCD_HoursPerDay As Integer
    Dim ATCD_Days As Integer
    Dim ATCD_Cost As Decimal
    Dim ATCD_CostPerDay As Decimal
    Dim ATCD_IPAddress As String
    Dim ATCD_CompID As Integer
End Structure
Public Structure strTimeCostBudgetMaster
    Dim ATCB_PKID As Integer
    Dim ATCB_YearID As Integer
    Dim ATCB_AuditCodeID As Integer
    Dim ATCB_Type As String
    Dim ATCB_TaskProcessID As Integer
    Dim ATCB_TotalDays As Integer
    Dim ATCB_TotalHours As Integer
    Dim ATCB_TotalCost As Decimal
    Dim ATCB_DelFlag As String
    Dim ATCB_Status As String
    Dim ATCB_Createdby As Integer
    Dim ATCB_Updatedby As Integer
    Dim ATCB_IPAddress As String
    Dim ATCB_CompID As Integer
End Structure
Public Structure strTimeSheet
    Dim TS_PKID As Integer
    Dim TS_AuditCodeID As Integer
    Dim TS_CustID As Integer
    Dim TS_FunID As Integer
    Dim TS_TaskID As Integer
    Dim TS_TaskType As String
    Dim TS_UserID As Integer
    Dim TS_Date As Date
    Dim TS_Comments As String
    Dim TS_Hours As Decimal
    Dim TS_DESCID As Integer
    Dim TS_DELFLG As String
    Dim TS_STATUS As String
    Dim TS_CRBY As Integer
    Dim TS_UpdatedBy As Integer
    Dim TS_IPAddress As String
    Dim TS_YearID As Integer
    Dim TS_CompID As Integer
End Structure
Public Class clsTimeCost
    Private objDBL As New DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function SaveTimeBudgetMaster(ByVal sAC As String, ByVal objstrTimeCostBudgetMaster As strTimeCostBudgetMaster) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_PKID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_YearID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_YearID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_AuditCodeID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_Type", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_Type
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_TaskProcessID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_TaskProcessID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_Createdby", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_Createdby
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCB_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetMaster.ATCB_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_TimeCostBudgetMaster", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTimeBudgetDetails(ByVal sAC As String, ByVal objstrTimeCostBudgetDetails As strTimeCostBudgetDetails) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try

            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_PKID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_ATCBID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_ATCBID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_YearID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_YearID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_Type", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_Type
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_TaskProcessID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_TaskProcessID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_AuditCodeID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_UserID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_UserID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_Hours", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_Hours
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_HoursPerDay", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_HoursPerDay
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_CostPerDay", OleDb.OleDbType.Decimal, 8)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_CostPerDay
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_Days", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_Days
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_Cost", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_Cost
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ATCD_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeCostBudgetDetails.ATCD_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_TimeCostBudgetDetails", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrId As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0
        Try
            dt.Columns.Add("MasterPKID")
            dt.Columns.Add("AuditTaskID")
            dt.Columns.Add("AuditTaskName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")
            dt.Columns.Add("Status")

            sSql = "Select CMM_Desc,AAPM_AuditTaskID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & ""
            If iAsgId > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID=" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("AAPM_AuditTaskID")) = False Then
                    drRow("AuditTaskID") = dtAP.Rows(i)("AAPM_AuditTaskID")
                End If
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    drRow("AuditTaskName") = dtAP.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_PKID")) = False Then
                    drRow("MasterPKID") = dtAP.Rows(i)("ATCB_PKID")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                    iTotHrs = iTotHrs + drRow("TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                    iTotDays = iTotDays + drRow("TotalDays")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_Status")) = False Then
                    drRow("Status") = dtAP.Rows(i)("ATCB_Status")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("AuditTaskID") = 0 : drRow("AuditTaskName") = "Total" : drRow("MasterPKID") = 0
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays : drRow("Status") = ""
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0
        Try
            dt.Columns.Add("MasterPKID")
            dt.Columns.Add("ProcessID")
            dt.Columns.Add("ProcessName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")
            dt.Columns.Add("Status")

            sSql = "select PM_NAME,AAPM_AuditTaskID,PM_ID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & ""
            If iAsgId > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("PM_NAME")) = False Then
                    drRow("ProcessName") = (dtAP.Rows(i)("PM_NAME"))
                End If
                If IsDBNull(dtAP.Rows(i)("PM_ID")) = False Then
                    drRow("ProcessID") = dtAP.Rows(i)("PM_ID")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_PKID")) = False Then
                    drRow("MasterPKID") = dtAP.Rows(i)("ATCB_PKID")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                    iTotHrs = iTotHrs + drRow("TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                    iTotDays = iTotDays + drRow("TotalDays")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_Status")) = False Then
                    drRow("Status") = dtAP.Rows(i)("ATCB_Status")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("ProcessID") = 0 : drRow("ProcessName") = "Total" : drRow("MasterPKID") = 0
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays : drRow("Status") = ""
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadResource(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iMasID As Integer, ByVal sType As String)
        Dim sSql As String, sSql1 As String
        Dim dRow As DataRow
        Dim aArray As String(), aResourceDesg As String(), aHoursCharges As String()
        Dim dtTab As New DataTable, dt As New DataTable
        Dim sResourceDesg As String = "", sAPMResource As String = "", sHoursCharges As String = ""
        Try
            dt.Columns.Add("UserID")
            dt.Columns.Add("ResourceName")
            dt.Columns.Add("DesginationName")
            dt.Columns.Add("DesginationID")
            dt.Columns.Add("Hours")
            dt.Columns.Add("HoursPerDay")
            dt.Columns.Add("ChargesPerDay")
            dt.Columns.Add("TimeCostBudgetDetailsPKID")

            sSql = "SELECT AAPM_Resource FROM Audit_APM_Assignment_Details WHERE AAPM_AuditTaskType='" & sType & "' And AAPM_AuditTaskID = " & iMasID & " and AAPM_AuditCodeID = " & iAsgId & " and AAPM_CompID = " & iACID & ""
            sAPMResource = objDBL.SQLExecuteScalar(sAC, sSql)
            If sAPMResource <> "" Then
                aArray = sAPMResource.Split(",")
                For i = 0 To aArray.Length - 1
                    If aArray(i) <> "" Then
                        dRow = dt.NewRow
                        dRow("UserID") = aArray(i)
                        sResourceDesg = GetAsgMemberDesgDetails(sAC, iACID, aArray(i))

                        If sResourceDesg <> Nothing Then
                            If sResourceDesg.Contains("|") Then
                                aResourceDesg = sResourceDesg.Split("|")
                                If aResourceDesg.Length > 1 Then
                                    dRow("ResourceName") = aResourceDesg(0)
                                    dRow("DesginationID") = aResourceDesg(1)
                                    dRow("DesginationName") = aResourceDesg(2)
                                End If
                            End If
                            sSql1 = "Select ATCD_PKID,ATCD_Hours,ATCD_HoursPerDay,ATCD_CostPerDay From Audit_TimeCostBudgetDetails WHERE ATCD_UserID = " & aArray(i) & " And "
                            sSql1 = sSql1 & " ATCD_TaskProcessID = " & iMasID & " And ATCD_AuditCodeID=" & iAsgId & " And ATCD_Type='" & sType & "' And ATCD_CompID=" & iACID & ""

                            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql1)
                            If dtTab.Rows.Count > 0 Then
                                dRow("TimeCostBudgetDetailsPKID") = dtTab.Rows(0)("ATCD_PKID")
                                dRow("Hours") = dtTab.Rows(0)("ATCD_Hours")
                                dRow("HoursPerDay") = dtTab.Rows(0)("ATCD_HoursPerDay")
                                dRow("ChargesPerDay") = dtTab.Rows(0)("ATCD_CostPerDay")
                            Else
                                dRow("Hours") = ""
                                If dRow("DesginationID") > 0 Then
                                    sHoursCharges = GetCategoryHoursAndCharges(sAC, iACID, dRow("DesginationID"))
                                    If sHoursCharges <> Nothing Then
                                        If sHoursCharges.Contains("|") Then
                                            aHoursCharges = sHoursCharges.Split("|")
                                            If aHoursCharges.Length > 1 Then
                                                dRow("HoursPerDay") = aHoursCharges(0)
                                                dRow("ChargesPerDay") = (aHoursCharges(1))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        End If
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAsgMemberDesgDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_FullName + '|' +  Convert(Varchar(500),usr_Designation) + '|' +  Convert(Varchar(500),Mas_Description) From Sad_userdetails "
            sSql = sSql & " Join SAD_GRPDESGN_General_Master On Mas_ID=usr_Designation And Mas_CompID=" & iACID & " Where usr_id=" & iUserID & " and Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCategoryHoursAndCharges(ByVal sAC As String, ByVal iACID As Integer, ByVal iDesgID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Convert(Varchar(500),EMPC_Hours) + '|' +  Convert(Varchar(500),EMPC_CHARGES)  from SAD_EmpCategory_Charges  Where EMPC_Cat_ID=" & iDesgID & " and EMPC_CompID= " & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateTotalDetailsToMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iJobID As Integer, ByVal iTaskId As Integer, ByVal sType As String)
        Dim aSql As String
        Try
            aSql = "Update Audit_TimeCostBudgetmaster set ATCB_TotalDays=(Select Sum(ATCD_Days) From  Audit_TimeCostBudgetDetails  Where ATCD_TaskProcessID = " & iTaskId & " And ATCD_AuditCodeID=" & iJobID & " And ATCD_Type='" & sType & "' and ATCD_CompID=" & iACID & "),"
            aSql = aSql & " ATCB_TotalHours=(Select Sum(ATCD_Hours) From  Audit_TimeCostBudgetDetails  Where ATCD_TaskProcessID = " & iTaskId & " And ATCD_AuditCodeID=" & iJobID & " And ATCD_Type='" & sType & "' and ATCD_CompID=" & iACID & "),"
            aSql = aSql & " ATCB_TotalCost=(Select Sum(ATCD_Cost) From  Audit_TimeCostBudgetDetails  Where ATCD_TaskProcessID = " & iTaskId & " And ATCD_AuditCodeID=" & iJobID & " And ATCD_Type='" & sType & "' and ATCD_CompID=" & iACID & ")"
            aSql = aSql & " where ATCB_CompID=" & iACID & " And ATCB_TaskProcessID = " & iTaskId & " And ATCB_AuditCodeID = " & iJobID & " And ATCB_Type ='" & sType & "'"
            objDBL.SQLExecuteNonQuery(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetTimeBudgetStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iJobID As Integer) As String
        Dim aSql As String
        Try
            aSql = "Select ATCB_Status From Audit_TimeCostBudgetMaster Where ATCB_AuditCodeID=" & iJobID & " And ATCB_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitTimeBuugetDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iJobID As Integer)
        Dim aSql As String
        Try
            aSql = " Update Audit_TimeCostBudgetMaster Set ATCB_Status='Submitted' Where ATCB_AuditCodeID=" & iJobID & " And ATCB_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadNonAudit(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iFunID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dr As New DataSet
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0
        Try
            dt.Columns.Add("ProcessId")
            dt.Columns.Add("Process")
            dt.Columns.Add("Total")
            dt.Columns.Add("Days")
            dt.Columns.Add("Hours")

            sSql = "select TS_TaskID,TS_Hours from Audit_TimeSheet where TS_CompID=" & iACID & " and TS_TaskType='NA'"
            If iAsgId > 0 Then
                sSql = sSql & " And TS_AuditCodeID=" & iAsgId & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And TS_FunID=" & iFunID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("TS_TaskID")) = False Then
                    If dtAP.Rows(i)("TS_TaskID") <> 0 Then
                        drRow("Process") = objDBL.SQLExecuteScalar(sAC, "Select cmm_Desc from Content_Management_Master where cmm_ID =" & dtAP.Rows(i)("TS_TaskID") & "")
                        drRow("ProcessId") = dtAP.Rows(i)("TS_TaskID")
                        If iUserID > 0 Then
                            drRow("Hours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("TS_TaskID") & " And TS_UserID = " & iUserID & " And TS_TaskType='NA'")
                            iTotHrs = iTotHrs + drRow("Hours")
                        Else
                            drRow("Hours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("TS_TaskID") & " And TS_TaskType='NA'")
                            iTotHrs = iTotHrs + drRow("Hours")
                        End If
                        dt.Rows.Add(drRow)
                    End If
                End If
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("ProcessId") = 0 : drRow("Process") = "Total" : drRow("Hours") = iTotHrs
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadHours(ByVal sAC As String, ByVal iTaskID As Integer, ByVal iJobID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = "SELECT FT_PKID,FT_TaskID,FT_TaskType,Convert(Varchar(10),ft_date,103) ft_date,FT_Hours,FT_Comments FROM fla_Timesheet where FT_TaskID = " & iTaskID & "  And FT_JobID = " & iJobID & " And FT_UserID=" & iUserID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingHours(ByVal sAC As String, ByVal iACID As Integer, ByVal iTaskID As Integer, ByVal sTaskType As String, ByVal iAuditCodeID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = "SELECT TS_PKID,TS_Hours,TS_TaskID,TS_TaskType,Convert(Varchar(10),TS_Date,103) TS_Date,TS_Comments FROM Audit_TimeSheet where TS_TaskID = " & iTaskID & " "
            sSql = sSql & " And TS_TaskType='" & sTaskType & "' And TS_AuditCodeID = " & iAuditCodeID & " and TS_CompID=" & iACID & ""
            If iUserID > 0 Then
                sSql = sSql & " And TS_UserID=" & iUserID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNonAuditHours(ByVal sAC As String, ByVal iACID As Integer, ByVal iTaskID As Integer, ByVal iAuditCodeID As Integer) As DataTable
        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = "SELECT TS_Hours,TS_TaskID,TS_TaskType,Convert(Varchar(10),TS_Date,103) TS_Date,TS_Hours,TS_Comments FROM Audit_TimeSheet"
            sSql = sSql & " where TS_TaskType='NA' And TS_AuditCodeID = " & iAuditCodeID & " and TS_CompID=" & iACID & " "
            If iTaskID > 0 Then
                sSql = sSql & " And TS_TaskID = " & iTaskID & " "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadNonAuditTask(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = "Select Mas_ID,Mas_Description from  where Mas_Delflag = 'A' Order by Mas_Description"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadHoursDetails(ByVal sAC As String, ByVal iPKID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "SELECT TS_Hours,TS_TaskID,TS_TaskType,Convert(Varchar(10),TS_Date,103) TS_Date,TS_Hours,TS_Comments,TS_STATUS FROM Audit_TimeSheet where TS_PKID=" & iPKID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTimeSheetStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditCodeID As Integer) As String
        Dim sSql As String
        Try
            sSql = "SELECT TS_STATUS FROM Audit_TimeSheet where TS_AuditCodeID=" & iAuditCodeID & " And TS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTimeSheet(ByVal sAC As String, ByVal objstrTimeSheet As strTimeSheet) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_PKID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_AuditCodeID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_FunID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_FunID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_CustID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_TaskID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_TaskID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_TaskType", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_TaskType
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_UserID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_UserID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_Date", OleDb.OleDbType.Date, 8)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_Date
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_Comments", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_Comments
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_Hours", OleDb.OleDbType.Decimal)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_Hours
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_YearID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_YearID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@TS_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrTimeSheet.TS_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_TimeSheet", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAuditTaskDetailsTimeSheetEntry(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrId As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0
        Try
            dt.Columns.Add("MasterPKID")
            dt.Columns.Add("AuditTaskID")
            dt.Columns.Add("AuditTaskName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")
            dt.Columns.Add("Status")
            dt.Columns.Add("ActualHours")

            sSql = "select CMM_Desc,AAPM_AuditTaskID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & ""
            'If iAsgId > 0 Then
            sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            'End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("AAPM_AuditTaskID")) = False Then
                    drRow("AuditTaskID") = dtAP.Rows(i)("AAPM_AuditTaskID")
                End If
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    drRow("AuditTaskName") = dtAP.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_PKID")) = False Then
                    drRow("MasterPKID") = dtAP.Rows(i)("ATCB_PKID")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_Status")) = False Then
                    drRow("Status") = dtAP.Rows(i)("ATCB_Status")
                End If
                If iUsrId > 0 Then
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_UserID = " & iUsrId & " And TS_TaskType='AT'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                Else
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_TaskType='AT'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("AuditTaskID") = 0 : drRow("AuditTaskName") = "Total" : drRow("MasterPKID") = 0
                drRow("TotalHours") = 0 : drRow("TotalDays") = 0 : drRow("ActualHours") = iTotHrs : drRow("Status") = ""
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadProcessTimeSheetEntry(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0
        Try
            dt.Columns.Add("MasterPKID")
            dt.Columns.Add("ProcessID")
            dt.Columns.Add("ProcessName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")
            dt.Columns.Add("Status")
            dt.Columns.Add("ActualHours")
            sSql = "select PM_NAME,AAPM_AuditTaskID,PM_ID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & " "
            If iAsgId > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("PM_NAME")) = False Then
                    drRow("ProcessName") = (dtAP.Rows(i)("PM_NAME"))
                End If
                If IsDBNull(dtAP.Rows(i)("PM_ID")) = False Then
                    drRow("ProcessID") = dtAP.Rows(i)("PM_ID")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_PKID")) = False Then
                    drRow("MasterPKID") = dtAP.Rows(i)("ATCB_PKID")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_Status")) = False Then
                    drRow("Status") = dtAP.Rows(i)("ATCB_Status")
                End If
                If iUsrID > 0 Then
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_UserID = " & iUsrID & " And TS_TaskType='AP'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                Else
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_TaskType='AP'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("ProcessID") = 0 : drRow("ProcessName") = "Total" : drRow("MasterPKID") = 0
                drRow("TotalHours") = 0 : drRow("TotalDays") = 0 : drRow("ActualHours") = iTotHrs : drRow("Status") = ""
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitTimeSheetDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditCodeID As Integer, ByVal iUserID As Integer)
        Dim aSql As String
        Try
            aSql = " Update Audit_TimeSheet Set TS_STATUS='A',TS_APPROVEDBY=" & iUserID & ",TS_APPROVEDON=GetDAte() Where TS_AuditCodeID=" & iAuditCodeID & " And TS_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Time Sheet variance
    Public Function LoadTimeSheetVarianceDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim sTask As String = ""
        Try
            dt.Columns.Add("Task")
            dt.Columns.Add("Category")
            dt.Columns.Add("Employee")
            dt.Columns.Add("BudgetedHours")
            dt.Columns.Add("ActualHours")

            dRow = dt.NewRow()
            dRow("Task") = "Audit Check Point"
            dt.Rows.Add(dRow)

            sSql = "select CMM_Desc,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,Usr_FullName From Audit_TimeCostBudgetMaster "
            sSql = sSql & " Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=ATCD_TaskProcessID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & ""
            End If
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & " And ATCB_Type='AT' "
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("Category") = objDBL.SQLGetDescription(sAC, "Select Mas_Description from SAD_GRPDESGN_General_Master where Mas_ID = (Select usr_Designation from sad_userDetails where usr_ID = " & dtAP.Rows(i)("ATCD_UserID") & ")")
                End If
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    dRow("Task") = dtAP.Rows(i)("CMM_Desc")
                    If sTask = dtAP.Rows(i)("CMM_Desc") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("CMM_Desc")
                End If
                    If IsDBNull(dtAP.Rows(i)("Usr_FullName")) = False Then
                    dRow("Employee") = dtAP.Rows(i)("Usr_FullName")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("BudgetedHours") = dtAP.Rows(i)("ATCD_Hours")
                End If

                If IsDBNull(dtAP.Rows(i)("ATCD_TaskProcessID")) = False And IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    If dtAP.Rows(i)("ATCD_TaskProcessID") > 0 And dtAP.Rows(i)("ATCD_UserID") > 0 Then
                        If iUserID > 0 Then
                            dRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("ATCD_TaskProcessID") & " And TS_UserID = " & dtAP.Rows(i)("ATCD_UserID") & " And TS_TaskType='AT'")
                        Else
                            dRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("ATCD_TaskProcessID") & " And TS_TaskType='AT'")
                        End If
                    Else
                        dRow("ActualHours") = ""
                    End If
                Else
                    dRow("ActualHours") = ""
                End If

                ' dRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("ATCD_TaskProcessID") & " And TS_UserID = " & dtAP.Rows(i)("ATCD_UserID") & " And TS_TaskType='AT'")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
            dRow = dt.NewRow()
            dRow("Task") = "Audit Process"
            dt.Rows.Add(dRow)
            sTask = ""
            sSql = "select PM_Name,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,Usr_FullName From Audit_TimeCostBudgetMaster "
            sSql = sSql & " Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=ATCD_TaskProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & "And ATCB_Type='AP' "
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("Category") = objDBL.SQLGetDescription(sAC, "Select Mas_Description from SAD_GRPDESGN_General_Master where Mas_ID = (Select usr_Designation from sad_userDetails where usr_ID = " & dtAP.Rows(i)("ATCD_UserID") & ")")
                End If
                If IsDBNull(dtAP.Rows(i)("PM_Name")) = False Then
                    dRow("Task") = dtAP.Rows(i)("PM_Name")
                    If sTask = dtAP.Rows(i)("PM_Name") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("PM_Name")
                End If
                If IsDBNull(dtAP.Rows(i)("Usr_FullName")) = False Then
                    dRow("Employee") = dtAP.Rows(i)("Usr_FullName")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("BudgetedHours") = dtAP.Rows(i)("ATCD_Hours")
                End If

                If IsDBNull(dtAP.Rows(i)("ATCD_TaskProcessID")) = False And IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    If dtAP.Rows(i)("ATCD_TaskProcessID") > 0 And dtAP.Rows(i)("ATCD_UserID") > 0 Then
                        If iUserID > 0 Then
                            dRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("ATCD_TaskProcessID") & " And TS_UserID = " & dtAP.Rows(i)("ATCD_UserID") & " And TS_TaskType='AP'")
                        Else
                            dRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("ATCD_TaskProcessID") & " And TS_TaskType='AP'")
                        End If
                    Else
                        dRow("ActualHours") = ""
                    End If
                Else
                    dRow("ActualHours") = ""
                End If

                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next

            dRow = dt.NewRow()
            dRow("Task") = "Non-Audit Task"
            dt.Rows.Add(dRow)
            sTask = ""
            sSql = "select CMM_Desc,TS_Hours,Usr_FullName,TS_UserID From Audit_TimeSheet "
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=TS_TaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=TS_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Where TS_AuditCodeID=" & iAsgId & " and TS_CompID=" & iACID & "And TS_TaskType='NA' "
            If iUserID > 0 Then
                sSql = sSql & " And TS_UserID=" & iUserID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("TS_UserID")) = False Then
                    dRow("Category") = objDBL.SQLGetDescription(sAC, "Select Mas_Description from SAD_GRPDESGN_General_Master where Mas_ID = (Select usr_Designation from sad_userDetails where usr_ID = " & dtAP.Rows(i)("TS_UserID") & ")")
                End If
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    dRow("Task") = dtAP.Rows(i)("CMM_Desc")
                    If sTask = dtAP.Rows(i)("CMM_Desc") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtAP.Rows(i)("Usr_FullName")) = False Then
                    dRow("Employee") = dtAP.Rows(i)("Usr_FullName")
                End If
                dRow("BudgetedHours") = ""
                dRow("ActualHours") = dtAP.Rows(i)("TS_Hours")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEmployee(ByVal sAC As String, ByVal iACID As Integer, ByVal iAudit As Integer, ByVal iFunID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String, sUser As String = "", sSqlUser As String, sUserID As String = ""
        Dim dt As New DataTable, dtUser As New DataTable
        Try
        sSql = "Select AAPM_Resource From Audit_APM_Assignment_Details Where AAPM_CustID =" & iCustID & "  And AAPM_CompID=" & iACID & ""
            If iAudit > 0 Then
                sSql = sSql & " And AAPM_AuditCodeID=" & iAudit & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & "And AAPM_FunctionID=" & iFunID & ""
            End If
            dtUser = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtUser.Rows.Count - 1
                If IsDBNull(dtUser.Rows(i)("AAPM_Resource")) = False Then
                    sUserID = dtUser.Rows(i)("AAPM_Resource")
                    If sUserID.StartsWith(",") = True Then
                        sUserID = sUserID.Remove(0, 1)
                    End If
                    sUser = sUser & sUserID
                End If
            Next
            If IsNothing(sUser) = False Then
                If sUser.StartsWith(",") = True Then
                    sUser = sUser.Remove(0, 1)
                End If
                If sUser.EndsWith(",") = True Then
                    sUser = sUser.Remove(Len(sUser) - 1, 1)
                End If
                sSqlUser = "Select Usr_ID,Usr_FullName From Sad_userDetails  Where Usr_ID In (" & sUser & ") And Usr_CompID=" & iACID & " Order by Usr_FullName"
                dt = objDBL.SQLExecuteDataTable(sAC, sSqlUser)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrId As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AuditTaskName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")

            sSql = "select CMM_Desc,AAPM_AuditTaskID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & ""
            If iAsgId > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                drRow("SrNo") = i + 1
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    drRow("AuditTaskName") = dtAP.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                    iTotHrs = iTotHrs + drRow("TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                    iTotDays = iTotDays + drRow("TotalDays")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("AuditTaskName") = "Total"
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadProcessToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ProcessName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")

            sSql = "Select PM_NAME, AAPM_AuditTaskID, PM_ID, ATCB_PKID, ATCB_TotalDays, ATCB_TotalHours, ATCB_TotalCost, ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID And ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & ""
            If iAsgId > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                drRow("SrNo") = i + 1
                If IsDBNull(dtAP.Rows(i)("PM_NAME")) = False Then
                    drRow("ProcessName") = (dtAP.Rows(i)("PM_NAME"))
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                    iTotHrs = iTotHrs + drRow("TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                    iTotDays = iTotDays + drRow("TotalDays")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("ProcessName") = "Total"
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadResourceToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iMasID As Integer, ByVal sType As String)
        Dim sSql As String, sSql1 As String
        Dim dRow As DataRow
        Dim aArray As String(), aResourceDesg As String(), aHoursCharges As String()
        Dim dtTab As New DataTable, dt As New DataTable
        Dim sResourceDesg As String = "", sAPMResource As String = "", sHoursCharges As String = ""
        Dim j As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ResourceName")
            dt.Columns.Add("DesginationName")
            dt.Columns.Add("DesginationID")
            dt.Columns.Add("Hours")
            dt.Columns.Add("HoursPerDay")

            sSql = "SELECT AAPM_Resource FROM Audit_APM_Assignment_Details WHERE AAPM_AuditTaskType='" & sType & "' And AAPM_AuditTaskID = " & iMasID & " and AAPM_AuditCodeID = " & iAsgId & " and AAPM_CompID = " & iACID & ""
            sAPMResource = objDBL.SQLExecuteScalar(sAC, sSql)
            If sAPMResource <> "" Then
                aArray = sAPMResource.Split(",")
                For i = 0 To aArray.Length - 1
                    If aArray(i) <> "" Then
                        dRow = dt.NewRow
                        sResourceDesg = GetAsgMemberDesgDetails(sAC, iACID, aArray(i))

                        If sResourceDesg <> Nothing Then
                            If sResourceDesg.Contains("|") Then
                                aResourceDesg = sResourceDesg.Split("|")
                                If aResourceDesg.Length > 1 Then
                                    j = j + 1
                                    dRow("SrNo") = j
                                    dRow("ResourceName") = aResourceDesg(0)
                                    dRow("DesginationID") = aResourceDesg(1)
                                    dRow("DesginationName") = aResourceDesg(2)
                                End If
                            End If
                            sSql1 = "Select ATCD_PKID,ATCD_Hours,ATCD_HoursPerDay,ATCD_CostPerDay From Audit_TimeCostBudgetDetails WHERE ATCD_UserID = " & aArray(i) & " And "
                            sSql1 = sSql1 & " ATCD_TaskProcessID = " & iMasID & " And ATCD_AuditCodeID=" & iAsgId & " And ATCD_Type='" & sType & "' And ATCD_CompID=" & iACID & ""

                            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql1)
                            If dtTab.Rows.Count > 0 Then
                                dRow("Hours") = dtTab.Rows(0)("ATCD_Hours")
                                dRow("HoursPerDay") = dtTab.Rows(0)("ATCD_HoursPerDay")
                            Else
                                dRow("Hours") = ""
                                If dRow("DesginationID") > 0 Then
                                    sHoursCharges = GetCategoryHoursAndCharges(sAC, iACID, dRow("DesginationID"))
                                    If sHoursCharges <> Nothing Then
                                        If sHoursCharges.Contains("|") Then
                                            aHoursCharges = sHoursCharges.Split("|")
                                            If aHoursCharges.Length > 1 Then
                                                dRow("HoursPerDay") = aHoursCharges(0)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        End If
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTaskDetailsTimeSheetEntryToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrId As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AuditTaskName")
            dt.Columns.Add("ActualHours")

            sSql = "select CMM_Desc,AAPM_AuditTaskID From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & ""
            'If iUsrId > 0 Then
            '    sSql = sSql & " And AAPM_Resource Like '%" & "," & iUsrId & "," & "%'"
            'End If
            If iAsgId > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                drRow("SrNo") = i + 1
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    drRow("AuditTaskName") = dtAP.Rows(i)("CMM_Desc")
                End If
                If iUsrId > 0 Then
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_UserID = " & iUsrId & " And TS_TaskType='AT'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                Else
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_TaskType='AT'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("AuditTaskName") = "Total" : drRow("ActualHours") = iTotHrs
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadProcessTimeSheetEntryToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUsrID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ProcessName")
            dt.Columns.Add("ActualHours")

            sSql = "select PM_NAME,AAPM_AuditTaskID From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAsgId & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & " "
            'If iUsrID > 0 Then
            '    sSql = sSql & " And AAPM_Resource Like '%" & "," & iUsrID & "," & "%'"
            'End If
            If iAsgId > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAsgId & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                drRow("SrNo") = i + 1
                If IsDBNull(dtAP.Rows(i)("PM_NAME")) = False Then
                    drRow("ProcessName") = (dtAP.Rows(i)("PM_NAME"))
                End If
                If iUsrID > 0 Then
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_UserID = " & iUsrID & " And TS_TaskType='AP'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                Else
                    drRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("AAPM_AuditTaskID") & " And TS_TaskType='AP'")
                    iTotHrs = iTotHrs + drRow("ActualHours")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("ProcessName") = "Total" : drRow("ActualHours") = iTotHrs
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadNonAuditToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iFunID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dr As New DataSet
        Dim dt, dtAP As New DataTable
        Dim j As Integer = 0, iTotHrs As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Process")
            dt.Columns.Add("Hours")

            sSql = "select TS_TaskID,TS_Hours from Audit_TimeSheet where TS_CompID=" & iACID & " and TS_TaskType='NA'"
            'If iAsgId > 0 Then
            sSql = sSql & " And TS_AuditCodeID=" & iAsgId & ""
            'End If
            If iFunID > 0 Then
                sSql = sSql & " And TS_FunID=" & iFunID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("TS_TaskID")) = False Then
                    If dtAP.Rows(i)("TS_TaskID") <> 0 Then
                        j = j + 1
                        drRow("SrNo") = j
                        drRow("Process") = objDBL.SQLExecuteScalar(sAC, "Select cmm_Desc from Content_Management_Master where cmm_ID =" & dtAP.Rows(i)("TS_TaskID") & "")
                        If iUserID > 0 Then
                            drRow("Hours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("TS_TaskID") & " And TS_UserID = " & iUserID & " And TS_TaskType='NA'")
                            iTotHrs = iTotHrs + drRow("Hours")
                        Else
                            drRow("Hours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("TS_TaskID") & " And TS_TaskType='NA'")
                            iTotHrs = iTotHrs + drRow("Hours")
                        End If

                        dt.Rows.Add(drRow)
                    End If
                End If
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("Process") = "Total" : drRow("Hours") = iTotHrs
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTimeSheetVarianceDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUserID As Integer, ByVal iBHours As Integer, ByVal iAHours As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim sTask As String = ""
        Dim j As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Task")
            dt.Columns.Add("Category")
            dt.Columns.Add("Employee")
            dt.Columns.Add("BudgetedHours")
            dt.Columns.Add("ActualHours")
            dt.Columns.Add("iBHours")
            dt.Columns.Add("iAHours")

            dRow = dt.NewRow()
            dRow("Task") = "Audit Check Point"
            dRow("iBHours") = iBHours
            dRow("iAHours") = iAHours
            dt.Rows.Add(dRow)

            sSql = "select CMM_Desc,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,Usr_FullName From Audit_TimeCostBudgetMaster "
            sSql = sSql & " Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=ATCD_TaskProcessID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & " And ATCB_Type='AT' "
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                j = j + 1
                dRow("SrNo") = j
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("Category") = objDBL.SQLGetDescription(sAC, "Select Mas_Description from SAD_GRPDESGN_General_Master where Mas_ID = (Select usr_Designation from sad_userDetails where usr_ID = " & dtAP.Rows(i)("ATCD_UserID") & ")")
                End If
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    dRow("Task") = dtAP.Rows(i)("CMM_Desc")
                    If sTask = dtAP.Rows(i)("CMM_Desc") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtAP.Rows(i)("Usr_FullName")) = False Then
                    dRow("Employee") = dtAP.Rows(i)("Usr_FullName")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("BudgetedHours") = dtAP.Rows(i)("ATCD_Hours")
                End If
                dRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("ATCD_TaskProcessID") & " And TS_UserID = " & dtAP.Rows(i)("ATCD_UserID") & " And TS_TaskType='AT'")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
            dRow = dt.NewRow()
            dRow("Task") = "Audit Process"
            dt.Rows.Add(dRow)
            sTask = ""
            sSql = "select PM_Name,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,Usr_FullName From Audit_TimeCostBudgetMaster "
            sSql = sSql & " Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=ATCD_TaskProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & "And ATCB_Type='AP' "
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                j = j + 1
                dRow("SrNo") = j
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("Category") = objDBL.SQLGetDescription(sAC, "Select Mas_Description from SAD_GRPDESGN_General_Master where Mas_ID = (Select usr_Designation from sad_userDetails where usr_ID = " & dtAP.Rows(i)("ATCD_UserID") & ")")
                End If
                If IsDBNull(dtAP.Rows(i)("PM_Name")) = False Then
                    dRow("Task") = dtAP.Rows(i)("PM_Name")
                    If sTask = dtAP.Rows(i)("PM_Name") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("PM_Name")
                End If
                If IsDBNull(dtAP.Rows(i)("Usr_FullName")) = False Then
                    dRow("Employee") = dtAP.Rows(i)("Usr_FullName")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_UserID")) = False Then
                    dRow("BudgetedHours") = dtAP.Rows(i)("ATCD_Hours")
                End If
                dRow("ActualHours") = objDBL.SQLExecuteScalar(sAC, "Select SUM(TS_Hours) from Audit_TimeSheet where TS_AuditCodeID = " & iAsgId & " And TS_TaskID = " & dtAP.Rows(i)("ATCD_TaskProcessID") & " And TS_UserID = " & dtAP.Rows(i)("ATCD_UserID") & " And TS_TaskType='AP'")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next

            dRow = dt.NewRow()
            dRow("Task") = "Non-Audit Task"
            dt.Rows.Add(dRow)
            sTask = ""
            sSql = "select CMM_Desc,TS_Hours,Usr_FullName,TS_UserID From Audit_TimeSheet "
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=TS_TaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=TS_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Where TS_AuditCodeID=" & iAsgId & " and TS_CompID=" & iACID & "And TS_TaskType='NA' "
            If iUserID > 0 Then
                sSql = sSql & " And TS_UserID=" & iUserID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                j = j + 1
                dRow("SrNo") = j
                If IsDBNull(dtAP.Rows(i)("TS_UserID")) = False Then
                    dRow("Category") = objDBL.SQLGetDescription(sAC, "Select Mas_Description from SAD_GRPDESGN_General_Master where Mas_ID = (Select usr_Designation from sad_userDetails where usr_ID = " & dtAP.Rows(i)("TS_UserID") & ")")
                End If
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    dRow("Task") = dtAP.Rows(i)("CMM_Desc")
                    If sTask = dtAP.Rows(i)("CMM_Desc") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtAP.Rows(i)("Usr_FullName")) = False Then
                    dRow("Employee") = dtAP.Rows(i)("Usr_FullName")
                End If
                dRow("BudgetedHours") = ""
                dRow("ActualHours") = dtAP.Rows(i)("TS_Hours")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingHoursToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iTaskID As Integer, ByVal sTaskType As String, ByVal iAuditCodeID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtDate As New DataTable
        Try
            dtDate.Columns.Add("TS_Date")
            dtDate.Columns.Add("TS_Comments")
            dtDate.Columns.Add("TS_Hours")
            sSql = "SELECT TS_PKID,TS_Hours,TS_TaskID,TS_TaskType,Convert(Varchar(10),TS_Date,103) TS_Date,TS_Comments FROM Audit_TimeSheet where TS_TaskID = " & iTaskID & " "
            sSql = sSql & " And TS_TaskType='" & sTaskType & "' And TS_AuditCodeID = " & iAuditCodeID & "  and TS_CompID=" & iACID & ""
            If iUserID > 0 Then
                sSql = sSql & " And TS_UserID=" & iUserID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dt.Rows(i)("TS_Date")) = False Then
                    dRow("TS_Date") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("TS_Date"), "F")
                End If
                If IsDBNull(dt.Rows(i)("TS_Comments")) = False Then
                    dRow("TS_Comments") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("TS_Comments"))
                End If
                If IsDBNull(dt.Rows(i)("TS_Hours")) = False Then
                    dRow("TS_Hours") = dt.Rows(i)("TS_Hours")
                End If
                dt.Rows.Add(dRow)
            Next
            Return dtDate
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
