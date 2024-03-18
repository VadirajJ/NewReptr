Public Structure strCostBudget_Details
    Private CBD_ID As Integer
    Private CBD_YearID As Integer
    Private CBD_AuditCodeID As Integer
    Private CBD_DescID As Integer
    Private CBD_UserID As String
    Private CBD_Per_Head As Decimal
    Private CBD_Total As String
    Private CBD_CrBy As Integer
    Private CBD_UpdateBy As Integer
    Private CBD_IPAddress As String
    Private CBD_CompID As Integer
    Public Property iCBD_ID() As Integer
        Get
            Return (CBD_ID)
        End Get
        Set(ByVal Value As Integer)
            CBD_ID = Value
        End Set
    End Property
    Public Property iCBD_YearID() As Integer
        Get
            Return (CBD_YearID)
        End Get
        Set(ByVal Value As Integer)
            CBD_YearID = Value
        End Set
    End Property
    Public Property iCBD_AuditCodeID() As Integer
        Get
            Return (CBD_AuditCodeID)
        End Get
        Set(ByVal Value As Integer)
            CBD_AuditCodeID = Value
        End Set
    End Property
    Public Property iCBD_DescID() As Integer
        Get
            Return (CBD_DescID)
        End Get
        Set(ByVal Value As Integer)
            CBD_DescID = Value
        End Set
    End Property
    Public Property sCBD_UserID() As String
        Get
            Return (CBD_UserID)
        End Get
        Set(ByVal Value As String)
            CBD_UserID = Value
        End Set
    End Property
    Public Property sCBD_Per_Head() As Decimal
        Get
            Return (CBD_Per_Head)
        End Get
        Set(ByVal Value As Decimal)
            CBD_Per_Head = Value
        End Set
    End Property
    Public Property sCBD_Total() As String
        Get
            Return (CBD_Total)
        End Get
        Set(ByVal Value As String)
            CBD_Total = Value
        End Set
    End Property

    Public Property iCBD_CrBy() As Integer
        Get
            Return (CBD_CrBy)
        End Get
        Set(ByVal Value As Integer)
            CBD_CrBy = Value
        End Set
    End Property
    Public Property iCBD_UpdateBy() As Integer
        Get
            Return (CBD_UpdateBy)
        End Get
        Set(ByVal Value As Integer)
            CBD_UpdateBy = Value
        End Set
    End Property
    Public Property iCBD_CompID() As Integer
        Get
            Return (CBD_CompID)
        End Get
        Set(ByVal Value As Integer)
            CBD_CompID = Value
        End Set
    End Property
    Public Property sCBD_IPAddress() As String
        Get
            Return (CBD_IPAddress)
        End Get
        Set(ByVal Value As String)
            CBD_IPAddress = Value
        End Set
    End Property
End Structure
Public Structure strCostSheet_Details
    Private CSD_ID As Integer
    Private CSD_YearID As Integer
    Private CSD_AuditCodeID As Integer
    Private CSD_CustID As Integer
    Private CSD_FunID As Integer
    Private CSD_DescID As Integer
    Private CSD_Date As String
    Private CSD_Comments As String
    Private CSD_KmsTravelled As Integer
    Private CSD_Costs As Integer
    Private CSD_Total As String
    Private CSD_CrBy As Integer
    Private CSD_UpdateBy As Integer
    Private CSD_IPAddress As String
    Private CSD_CompID As Integer
    Private CSD_UserID As Integer
    Public Property iCSD_UserID() As Integer
        Get
            Return (CSD_UserID)
        End Get
        Set(ByVal Value As Integer)
            CSD_UserID = Value
        End Set
    End Property

    Public Property iCSD_ID() As Integer
        Get
            Return (CSD_ID)
        End Get
        Set(ByVal Value As Integer)
            CSD_ID = Value
        End Set
    End Property
    Public Property iCSD_YearID() As Integer
        Get
            Return (CSD_YearID)
        End Get
        Set(ByVal Value As Integer)
            CSD_YearID = Value
        End Set
    End Property

    Public Property iCSD_CustID() As Integer
        Get
            Return (CSD_CustID)
        End Get
        Set(ByVal Value As Integer)
            CSD_CustID = Value
        End Set
    End Property
    Public Property iCSD_FunID() As Integer
        Get
            Return (CSD_FunID)
        End Get
        Set(ByVal Value As Integer)
            CSD_FunID = Value
        End Set
    End Property
    Public Property iCSD_AuditCodeID() As Integer
        Get
            Return (CSD_AuditCodeID)
        End Get
        Set(ByVal Value As Integer)
            CSD_AuditCodeID = Value
        End Set
    End Property
    Public Property iCSD_DescID() As Integer
        Get
            Return (CSD_DescID)
        End Get
        Set(ByVal Value As Integer)
            CSD_DescID = Value
        End Set
    End Property

    Public Property sCSD_Date() As String
        Get
            Return (CSD_Date)
        End Get
        Set(ByVal Value As String)
            CSD_Date = Value
        End Set
    End Property
    Public Property sCSD_Comments() As String
        Get
            Return (CSD_Comments)
        End Get
        Set(ByVal Value As String)
            CSD_Comments = Value
        End Set
    End Property
    Public Property iCSD_Costs() As Integer
        Get
            Return (CSD_Costs)
        End Get
        Set(ByVal Value As Integer)
            CSD_Costs = Value
        End Set
    End Property
    Public Property iCSD_KmsTravelled() As Integer
        Get
            Return (CSD_KmsTravelled)
        End Get
        Set(ByVal Value As Integer)
            CSD_KmsTravelled = Value
        End Set
    End Property

    Public Property sCSD_Total() As String
        Get
            Return (CSD_Total)
        End Get
        Set(ByVal Value As String)
            CSD_Total = Value
        End Set
    End Property

    Public Property iCSD_CrBy() As Integer
        Get
            Return (CSD_CrBy)
        End Get
        Set(ByVal Value As Integer)
            CSD_CrBy = Value
        End Set
    End Property
    Public Property iCSD_UpdateBy() As Integer
        Get
            Return (CSD_UpdateBy)
        End Get
        Set(ByVal Value As Integer)
            CSD_UpdateBy = Value
        End Set
    End Property
    Public Property iCSD_CompID() As Integer
        Get
            Return (CSD_CompID)
        End Get
        Set(ByVal Value As Integer)
            CSD_CompID = Value
        End Set
    End Property
    Public Property sCSD_IPAddress() As String
        Get
            Return (CSD_IPAddress)
        End Get
        Set(ByVal Value As String)
            CSD_IPAddress = Value
        End Set
    End Property
End Structure
Public Class clsCostBudget
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Public Function GetCostBudgetAuditTaskDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0, iTotCost As Integer = 0
        Try
            dt.Columns.Add("MasterPKID")
            dt.Columns.Add("AuditTaskID")
            dt.Columns.Add("AuditTaskName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")
            dt.Columns.Add("Status")

            sSql = "select CMM_Desc,AAPM_AuditTaskID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status,ATCD_UserID,usr_Designation,EMPC_CHARGES"
            sSql = sSql & " From Audit_APM_Assignment_Details Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAuditID & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetDetails On ATCD_TaskProcessID=AAPM_AuditTaskID and ATCD_AuditCodeID=" & iAuditID & " And ATCD_Type='AT' and ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_UserDetails On usr_Id=ATCD_UserID And Usr_CompId=" & iACID & ""
            sSql = sSql & " Left Join SAD_EmpCategory_Charges On EMPC_CAT_ID=usr_Designation And EMPC_DelFlag='A' And EMPC_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & "" ' and ATCB_Status= 
            If iAuditID > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAuditID & ""
            End If
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
                    iTotHrs = iTotHrs + drRow("TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                    iTotDays = iTotDays + drRow("TotalDays")
                End If
                If IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False Then
                    drRow("TotalCost") = dtAP.Rows(i)("ATCB_TotalDays") * dtAP.Rows(i)("EMPC_CHARGES")
                    iTotCost = iTotCost + drRow("TotalCost")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_Status")) = False Then
                    drRow("Status") = dtAP.Rows(i)("ATCB_Status")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("AuditTaskID") = 0 : drRow("AuditTaskName") = "Total" : drRow("MasterPKID") = 0
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays : drRow("TotalCost") = iTotCost : drRow("Status") = ""
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostBudgetProcessDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0, iTotCost As Integer = 0
        Try
            dt.Columns.Add("MasterPKID")
            dt.Columns.Add("ProcessID")
            dt.Columns.Add("ProcessName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")
            dt.Columns.Add("Status")

            sSql = "select PM_NAME,AAPM_AuditTaskID,PM_ID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status,ATCD_UserID,usr_Designation,EMPC_CHARGES"
            sSql = sSql & " From Audit_APM_Assignment_Details Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAuditID & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetDetails On ATCD_TaskProcessID=AAPM_AuditTaskID and ATCD_AuditCodeID=" & iAuditID & " And ATCD_Type='AP' and ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_UserDetails On usr_Id=ATCD_UserID And Usr_CompId=" & iACID & ""
            sSql = sSql & " Left Join SAD_EmpCategory_Charges On EMPC_CAT_ID=usr_Designation And EMPC_DelFlag='A' And EMPC_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & "And AAPM_CustID=" & iCustID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAuditID & ""
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
                If IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False Then
                    drRow("TotalCost") = dtAP.Rows(i)("ATCB_TotalDays") * dtAP.Rows(i)("EMPC_CHARGES")
                    iTotCost = iTotCost + drRow("TotalCost")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_Status")) = False Then
                    drRow("Status") = dtAP.Rows(i)("ATCB_Status")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("ProcessID") = 0 : drRow("ProcessName") = "Total" : drRow("MasterPKID") = 0
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays : drRow("TotalCost") = iTotCost : drRow("Status") = ""
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostBudgetUserDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iDescID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtuser As New DataTable
        Dim i As Integer = 0, j As Integer = 0
        Dim sResourceID As String = "", sUserIDs As String = ""
        Dim sArray As Array
        Dim drRow As DataRow
        Try
            dt.Columns.Add("Users")
            dt.Columns.Add("Charges")
            dt.Columns.Add("UserID")
            dt.Columns.Add("CBDPkID")
            dt.Columns.Add("DescID")

            sSql = "Select AAPM_Resource From Audit_APM_Assignment_Details Where AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAuditID & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtuser = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtuser.Rows.Count - 1
                If IsDBNull(dtuser.Rows(i)("AAPM_Resource")) = False Then
                    sResourceID = sResourceID & "," & dtuser.Rows(i)("AAPM_Resource")
                End If
            Next
            If sResourceID.StartsWith(",") = True Then
                sResourceID = sResourceID.Remove(0, 1)
            End If
            If sResourceID.EndsWith(",") = True Then
                sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
            End If

            If sResourceID <> "" Then
                sArray = sResourceID.Split(",")
                For j = 0 To sArray.Length - 1
                    If sArray(j) <> "" Then
                        If sUserIDs.Contains("," & sArray(j) & ",") = False Then
                            sUserIDs = sUserIDs & "," & sArray(j) & ","
                            drRow = dt.NewRow
                            drRow("Users") = objDBL.SQLGetDescription(sAC, "Select Usr_FullName from Sad_userdetails where usr_id = " & sArray(j) & "")
                            If iDescID > 0 Then
                                drRow("Charges") = objDBL.SQLGetDescription(sAC, "Select CBD_Per_Head from Audit_CostBudgetDetails where CBD_AuditCodeID=" & iAuditID & " and CBD_DescID = " & iDescID & " And  CBD_UserID = " & sArray(j) & " ")
                                If IsDBNull(drRow("Charges")) = True Then
                                    drRow("Charges") = 0
                                End If
                                drRow("CBDPkID") = objDBL.SQLGetDescription(sAC, "Select CBD_ID from Audit_CostBudgetDetails where CBD_AuditCodeID=" & iAuditID & " and CBD_DescID = " & iDescID & " And  CBD_UserID = " & sArray(j) & " ")
                                drRow("DescID") = iDescID
                            Else
                                drRow("Charges") = 0
                            End If
                            drRow("UserID") = sArray(j)
                            dt.Rows.Add(drRow)
                        End If
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDescriptionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDesc As New DataTable
        Dim drRow As DataRow
        Dim i As Integer, iTotCost As Integer = 0
        Dim dcTotal As Decimal
        Try
            dt.Columns.Add("CountID")
            dt.Columns.Add("DescID")
            dt.Columns.Add("Description")
            dt.Columns.Add("Total")
            dt.Columns.Add("Cost")
            dt.Columns.Add("Status")

            sSql = "Select Distinct(CBD_DescID) From Audit_CostBudgetDetails Where CBD_AuditCodeID=" & iAuditID & " And CBD_CompID=" & iACID & " "
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDesc.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtDesc.Rows(i)("CBD_DescID")) = False Then
                    drRow("CountID") = objDBL.SQLGetDescription(sAC, "Select Count(CBD_ID) from Audit_CostBudgetDetails where CBD_AuditCodeID=" & iAuditID & " and  CBD_DescID=" & dtDesc.Rows(i)("CBD_DescID") & " And CBD_CompID=" & iACID & "")
                    drRow("Description") = objDBL.SQLGetDescription(sAC, "Select CMM_Desc from Content_Management_Master where   CMM_ID=" & dtDesc.Rows(i)("CBD_DescID") & " And CMM_CompID=" & iACID & "")
                    dcTotal = objDBL.SQLGetDescription(sAC, "Select Sum(CBD_Per_Head) from Audit_CostBudgetDetails where CBD_AuditCodeID=" & iAuditID & " and CBD_DescID=" & dtDesc.Rows(i)("CBD_DescID") & " And CBD_CompID=" & iACID & "")
                    drRow("Total") = Math.Ceiling(dcTotal)
                    iTotCost = iTotCost + drRow("Total")
                    drRow("Cost") = (drRow("Total") / drRow("CountID"))
                    drRow("DescID") = dtDesc.Rows(i)("CBD_DescID")
                    drRow("Status") = objDBL.SQLGetDescription(sAC, "Select CBD_Status from Audit_CostBudgetDetails where CBD_AuditCodeID=" & iAuditID & " and CBD_DescID=" & dtDesc.Rows(i)("CBD_DescID") & " And CBD_CompID=" & iACID & "")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtDesc.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("CountID") = 0 : drRow("Description") = "Total" : drRow("Total") = iTotCost
                drRow("Cost") = 0 : drRow("DescID") = 0 : drRow("Status") = ""
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCostBudget_Details(ByVal sAC As String, ByVal objCostBudget As strCostBudget_Details)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.iCBD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.iCBD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.iCBD_AuditCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_DescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.iCBD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_UserID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.sCBD_UserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_Per_Head", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objCostBudget.sCBD_Per_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.iCBD_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_UpdateBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.iCBD_UpdateBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("AAPM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCostBudget.sCBD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostBudget.iCBD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_CostBudgetDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    '' Cost Sheet
    Public Function SaveCostSheet_Details(ByVal sAC As String, ByVal objCostSheet As strCostSheet_Details)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_AuditCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_FunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_DescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_UserID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_UserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_Date", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objCostSheet.sCSD_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_Comments", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objCostSheet.sCSD_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_KmsTravelled", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_KmsTravelled
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_Costs", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_Costs
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_UpdateBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_UpdateBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("AAPM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCostSheet.sCSD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCostSheet.iCSD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_CostSheetDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDesignationID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_Designation from sad_userDetails Where Usr_ID =" & iUsrID & " And Usr_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCharges(ByVal sAC As String, ByVal iACID As Integer, ByVal iDesignationID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select EMPC_KMCharges From SAD_EmpCategory_Charges Where EMPC_CAT_ID=" & iDesignationID & " And EMPC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostSheetID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iDescID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CSD_ID from Audit_CostSheetDetails Where CSD_YearID=" & iYearID & " And CSD_AuditCodeID=" & iAuditCodeID & " And CSD_CompID=" & iACID & ""
            If iDescID > 0 Then
                sSql = sSql & " And CSD_DescID=" & iDescID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostSheetStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CSD_Status from Audit_CostSheetDetails Where CSD_YearID=" & iYearID & " And CSD_AuditCodeID=" & iAuditCodeID & " And CSD_UserID=" & iUserID & " And CSD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostSheetDescriptionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDesc As New DataTable
        Dim drRow As DataRow
        Dim i As Integer, iTotCost As Integer = 0
        Try
            dt.Columns.Add("DescID")
            dt.Columns.Add("AuditCodeID")
            dt.Columns.Add("Description")
            dt.Columns.Add("Date")
            dt.Columns.Add("Comments")
            dt.Columns.Add("Cost")

            If iUserID > 0 Then
                sSql = "Select CMM_ID,CMM_Desc,CSD_Date,CSD_Comments,CSD_Costs,CSD_AuditCodeID,CSD_DescID From Content_Management_Master  "
                sSql = sSql & " Left Join Audit_CostSheetDetails On CSD_DescID=CMM_ID And CSD_AuditCodeID= " & iAuditID & " And CSD_CompID=" & iACID & " And CSD_UserID=" & iUserID & ""
                sSql = sSql & " Where CMM_Category='EC' And CMM_ID in(Select CBD_DescID From Audit_CostBudgetDetails"
                sSql = sSql & " Where CBD_AuditCodeID=" & iAuditID & " And CBD_CompID=" & iACID & ") Or CMM_ID in (Select CSD_DescID From Audit_CostSheetDetails"
                sSql = sSql & " Where CSD_AuditCodeID=" & iAuditID & " And CSD_CompID=" & iACID & ")"
                sSql = sSql & " Group by CMM_ID,CMM_Desc,CSD_Date,CSD_Comments,CSD_Costs,CSD_AuditCodeID,CSD_DescID"
            End If
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDesc.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtDesc.Rows(i)("CSD_DescID")) = False Then
                    drRow("DescID") = dtDesc.Rows(i)("CSD_DescID")
                Else
                    drRow("DescID") = dtDesc.Rows(i)("CMM_ID")
                End If
                If IsDBNull(dtDesc.Rows(i)("CMM_Desc")) = False Then
                    drRow("Description") = dtDesc.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtDesc.Rows(i)("CSD_AuditCodeID")) = False Then
                    drRow("AuditCodeID") = dtDesc.Rows(i)("CSD_AuditCodeID")
                End If
                If IsDBNull(dtDesc.Rows(i)("CSD_Date")) = False Then
                    drRow("Date") = dtDesc.Rows(i)("CSD_Date")
                End If
                If IsDBNull(dtDesc.Rows(i)("CSD_Comments")) = False Then
                    drRow("Comments") = objclsGRACeGeneral.ReplaceSafeSQL(dtDesc.Rows(i)("CSD_Comments"))
                End If
                If IsDBNull(dtDesc.Rows(i)("CSD_Costs")) = False Then
                    drRow("Cost") = dtDesc.Rows(i)("CSD_Costs")
                    iTotCost = iTotCost + drRow("Cost")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtDesc.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("DescID") = 0 : drRow("Description") = "" : drRow("AuditCodeID") = "" : drRow("Date") = "Total" : drRow("Comments") = "" : drRow("Cost") = iTotCost
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAddEnrtyDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iDescID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from Audit_CostSheetDetails Where CSD_YearID=" & iYearID & " And CSD_AuditCodeID=" & iAuditCodeID & " And CSD_UserID=" & iUserID & " And CSD_DescID=" & iDescID & " And CSD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostBudgetStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iJobID As Integer) As String
        Dim aSql As String
        Try
            aSql = "Select CBD_Status From Audit_CostBudgetDetails Where CBD_AuditCodeID=" & iJobID & "  And CBD_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitCostBudgetDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iJobID As Integer, ByVal iUserID As Integer)
        Dim aSql As String
        Try
            aSql = "Update Audit_CostBudgetDetails Set CBD_ApprovedBy=" & iUserID & " , CBD_ApprovedOn=GetDate() , CBD_Status='Submitted' Where CBD_AuditCodeID=" & iJobID & " And CBD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SubmitCostSheetDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iJobID As Integer, ByVal iUserID As Integer)
        Dim aSql As String
        Try
            aSql = "Update Audit_CostSheetDetails Set CSD_ApprovedBy=" & iUserID & " , CSD_ApprovedOn=GetDate() , CSD_Status='Submitted' Where CSD_AuditCodeID=" & iJobID & " And CSD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Cost Sheet Variance
    Public Function LoadCostSheetVarianceDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtAP As New DataTable, dtCategory As New DataTable
        Dim sTask As String = ""
        Try
            dt.Columns.Add("Task")
            dt.Columns.Add("Category")
            dt.Columns.Add("Employee")
            dt.Columns.Add("BudgetedCost")
            dt.Columns.Add("ActualCost")

            dRow = dt.NewRow()
            dRow("Task") = "Audit Check Point"
            dt.Rows.Add(dRow)

            sSql = "Select CMM_Desc,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,ATCB_TotalCost,Usr_FullName,ATCD_Days,"
            sSql = sSql & " usr_Designation,Mas_Description,usr_Designation,EMPC_CHARGES,EMPC_HOURS From Audit_TimeCostBudgetMaster Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=ATCD_TaskProcessID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master On Mas_ID=usr_Designation And Mas_DelFlag='A' And Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_EmpCategory_Charges On EMPC_CAT_ID=usr_Designation And EMPC_DelFlag='A' And EMPC_CompID=" & iACID & ""
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & " And ATCB_Type='AT' "
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("Mas_Description")) = False Then
                    dRow("Category") = dtAP.Rows(i)("Mas_Description")
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
                If IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False And IsDBNull(dtAP.Rows(i)("ATCD_Days")) = False Then
                    'dRow("BudgetedCost") = dtAP.Rows(i)("ATCD_Days") * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("BudgetedCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Days")) * Convert.ToDecimal(dtAP.Rows(i)("EMPC_CHARGES"))), 2, MidpointRounding.AwayFromZero)
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_Hours")) = False And IsDBNull(dtAP.Rows(i)("EMPC_HOURS")) = False And IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False Then
                    'dRow("ActualCost") = (dtAP.Rows(i)("ATCD_Hours") / dtAP.Rows(i)("EMPC_HOURS")) * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("ActualCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Hours")) / Convert.ToDecimal(dtAP.Rows(i)("EMPC_HOURS"))) * dtAP.Rows(i)("EMPC_CHARGES"), 2, MidpointRounding.AwayFromZero)
                End If
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
            dRow = dt.NewRow()
            dRow("Task") = "Audit Process"
            dt.Rows.Add(dRow)
            sTask = ""
            sSql = "Select PM_Name,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,ATCB_TotalCost,Usr_FullName,ATCD_Days,"
            sSql = sSql & " usr_Designation,Mas_Description,usr_Designation,EMPC_CHARGES,EMPC_HOURS From Audit_TimeCostBudgetMaster Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=ATCD_TaskProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master On Mas_ID=usr_Designation And Mas_DelFlag='A' And Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_EmpCategory_Charges On EMPC_CAT_ID=usr_Designation And EMPC_DelFlag='A' And EMPC_CompID=" & iACID & ""
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & "And ATCB_Type='AP' "
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & " "
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("Mas_Description")) = False Then
                    dRow("Category") = dtAP.Rows(i)("Mas_Description")
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
                If IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False And IsDBNull(dtAP.Rows(i)("ATCD_Days")) = False Then
                    'dRow("BudgetedCost") = dtAP.Rows(i)("ATCD_Days") * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("BudgetedCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Days")) * Convert.ToDecimal(dtAP.Rows(i)("EMPC_CHARGES"))), 2, MidpointRounding.AwayFromZero)
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_Hours")) = False And IsDBNull(dtAP.Rows(i)("EMPC_HOURS")) = False And IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False Then
                    'dRow("ActualCost") = (dtAP.Rows(i)("ATCD_Hours") / dtAP.Rows(i)("EMPC_HOURS")) * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("ActualCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Hours")) / Convert.ToDecimal(dtAP.Rows(i)("EMPC_HOURS"))) * dtAP.Rows(i)("EMPC_CHARGES"), 2, MidpointRounding.AwayFromZero)
                End If
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next

            dRow = dt.NewRow()
            dRow("Task") = "Non-Audit Task"
            dt.Rows.Add(dRow)
            sTask = ""

            sSql = "Select CMM_Desc,CBD_DescID,Sum(CSD_Costs) As CSD_Costs,Sum(CBD_Per_Head) as CBD_Per_Head From Audit_CostBudgetDetails "
            sSql = sSql & " Left Join Audit_CostSheetDetails On CSD_AuditCodeID=CBD_AuditCodeID And CSD_CompID=" & iACID & " And CSD_DescID=CBD_DescID And CSD_UserID=CBD_UserID "
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=CBD_DescID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Where CBD_AuditCodeID=" & iAsgId & " and CBD_CompID=" & iACID & " "
            If iUserID > 0 Then
                sSql = sSql & " And CBD_UserID=" & iUserID & " And CSD_UserID= " & iUserID & ""
            End If
            sSql = sSql & " Group By CBD_DescID,CMM_Desc Order by CBD_DescID"
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                dRow("Category") = ""
                'If IsDBNull(dtAP.Rows(i)("Mas_Description")) = False Then
                '    dRow("Category") = dtAP.Rows(i)("")
                'End If
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    dRow("Task") = dtAP.Rows(i)("CMM_Desc")
                    If sTask = dtAP.Rows(i)("CMM_Desc") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("CMM_Desc")
                End If
                dRow("Employee") = ""
                'If IsDBNull(dtAP.Rows(i)("Usr_FullName")) = False Then
                '    dRow("Employee") = dtAP.Rows(i)("Usr_FullName")
                'End If
                If IsDBNull(dtAP.Rows(i)("CBD_Per_Head")) = False Then
                    dRow("BudgetedCost") = objclsGRACeGeneral.ReplaceSafeSQL(dtAP.Rows(i)("CBD_Per_Head"))
                End If
                If IsDBNull(dtAP.Rows(i)("CSD_Costs")) = False Then
                    dRow("ActualCost") = dtAP.Rows(i)("CSD_Costs")
                End If
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
NextLoop:   Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCostSheetVarianceDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgId As Integer, ByVal iUserID As Integer, ByVal dBHours As Double, ByVal dAHours As Double) As DataTable
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
            dt.Columns.Add("BudgetedCost")
            dt.Columns.Add("ActualCost")
            dt.Columns.Add("iBHours")
            dt.Columns.Add("iAHours")

            dRow = dt.NewRow()
            dRow("Task") = "Audit Check Point"
            dRow("iBHours") = dBHours
            dRow("iAHours") = dAHours
            dt.Rows.Add(dRow)
            sSql = "Select CMM_Desc,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,ATCB_TotalCost,Usr_FullName,"
            sSql = sSql & " ATCD_Days,usr_Designation,Mas_Description,usr_Designation,EMPC_CHARGES,EMPC_HOURS "
            sSql = sSql & " From Audit_TimeCostBudgetMaster Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=ATCD_TaskProcessID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master On Mas_ID=usr_Designation And Mas_DelFlag='A' And Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_EmpCategory_Charges On EMPC_CAT_ID=usr_Designation And EMPC_DelFlag='A' And EMPC_CompID=" & iACID & ""
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & " And ATCB_Type='AT' "
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                j = j + 1
                dRow("SrNo") = j
                If IsDBNull(dtAP.Rows(i)("Mas_Description")) = False Then
                    dRow("Category") = dtAP.Rows(i)("Mas_Description")
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
                If IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False And IsDBNull(dtAP.Rows(i)("ATCD_Days")) = False Then
                    'dRow("BudgetedCost") = dtAP.Rows(i)("ATCD_Days") * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("BudgetedCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Days")) * Convert.ToDecimal(dtAP.Rows(i)("EMPC_CHARGES"))), 2, MidpointRounding.AwayFromZero)
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_Hours")) = False And IsDBNull(dtAP.Rows(i)("EMPC_HOURS")) = False And IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False Then
                    'dRow("ActualCost") = (dtAP.Rows(i)("ATCD_Hours") / dtAP.Rows(i)("EMPC_HOURS")) * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("ActualCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Hours")) / Convert.ToDecimal(dtAP.Rows(i)("EMPC_HOURS"))) * dtAP.Rows(i)("EMPC_CHARGES"), 2, MidpointRounding.AwayFromZero)
                End If
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
            dRow = dt.NewRow()
            dRow("Task") = "Audit Process"
            dt.Rows.Add(dRow)
            sTask = ""
            sSql = "Select PM_Name,ATCD_UserID,ATCD_Hours,ATCD_HoursPerDay,ATCD_Days,ATCD_Cost,ATCD_CostPerDay,ATCD_TaskProcessID,ATCD_Hours,ATCB_TotalCost,Usr_FullName,"
            sSql = sSql & " ATCD_Days,usr_Designation,Mas_Description,usr_Designation,EMPC_CHARGES,EMPC_HOURS"
            sSql = sSql & " From Audit_TimeCostBudgetMaster Left Join Audit_TimeCostBudgetDetails On ATCD_ATCBID=ATCB_PKID And ATCD_CompID=" & iACID & ""
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=ATCD_TaskProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ATCD_UserID And Usr_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master On Mas_ID=usr_Designation And Mas_DelFlag='A' And Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_EmpCategory_Charges On EMPC_CAT_ID=usr_Designation And EMPC_DelFlag='A' And EMPC_CompID=" & iACID & ""
            sSql = sSql & " Where ATCB_AuditCodeID=" & iAsgId & " and ATCB_CompID=" & iACID & "And ATCB_Type='AP' "
            If iUserID > 0 Then
                sSql = sSql & " And ATCD_UserID=" & iUserID & " "
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                j = j + 1
                dRow("SrNo") = j
                If IsDBNull(dtAP.Rows(i)("Mas_Description")) = False Then
                    dRow("Category") = dtAP.Rows(i)("Mas_Description")
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
                If IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False And IsDBNull(dtAP.Rows(i)("ATCD_Days")) = False Then
                    'dRow("BudgetedCost") = dtAP.Rows(i)("ATCD_Days") * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("BudgetedCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Days")) * Convert.ToDecimal(dtAP.Rows(i)("EMPC_CHARGES"))), 2, MidpointRounding.AwayFromZero)
                End If
                If IsDBNull(dtAP.Rows(i)("ATCD_Hours")) = False And IsDBNull(dtAP.Rows(i)("EMPC_HOURS")) = False And IsDBNull(dtAP.Rows(i)("EMPC_CHARGES")) = False Then
                    'dRow("ActualCost") = (dtAP.Rows(i)("ATCD_Hours") / dtAP.Rows(i)("EMPC_HOURS")) * dtAP.Rows(i)("EMPC_CHARGES")
                    dRow("ActualCost") = Decimal.Round((Convert.ToDecimal(dtAP.Rows(i)("ATCD_Hours")) / Convert.ToDecimal(dtAP.Rows(i)("EMPC_HOURS"))) * dtAP.Rows(i)("EMPC_CHARGES"), 2, MidpointRounding.AwayFromZero)
                End If
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next

            dRow = dt.NewRow()
            dRow("Task") = "Non-Audit Task"
            dt.Rows.Add(dRow)
            sTask = ""

            sSql = "Select CMM_Desc,CBD_DescID,Sum(CSD_Costs) As CSD_Costs,Sum(CBD_Per_Head) as CBD_Per_Head From Audit_CostBudgetDetails "
            sSql = sSql & " Left Join Audit_CostSheetDetails On CSD_AuditCodeID=CBD_AuditCodeID And CSD_CompID=" & iACID & " And CSD_DescID=CBD_DescID And CSD_UserID=CBD_UserID "
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=CBD_DescID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Where CBD_AuditCodeID=" & iAsgId & " and CBD_CompID=" & iACID & " "
            If iUserID > 0 Then
                sSql = sSql & " And CBD_UserID=" & iUserID & " And CSD_UserID= " & iUserID & ""
            End If
            sSql = sSql & " Group By CBD_DescID,CMM_Desc Order by CBD_DescID"
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                dRow = dt.NewRow
                j = j + 1
                dRow("SrNo") = j
                dRow("Category") = ""
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    dRow("Task") = dtAP.Rows(i)("CMM_Desc")
                    If sTask = dtAP.Rows(i)("CMM_Desc") Then
                        dRow("Task") = ""
                    End If
                    sTask = dtAP.Rows(i)("CMM_Desc")
                End If
                dRow("Employee") = ""
                If IsDBNull(dtAP.Rows(i)("CBD_Per_Head")) = False Then
                    dRow("BudgetedCost") = objclsGRACeGeneral.ReplaceSafeSQL(dtAP.Rows(i)("CBD_Per_Head"))
                End If
                If IsDBNull(dtAP.Rows(i)("CSD_Costs")) = False Then
                    dRow("ActualCost") = dtAP.Rows(i)("CSD_Costs")
                End If
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEmployee(ByVal sAC As String, ByVal iACID As Integer, ByVal iAudit As Integer, ByVal iFunID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String, sUser As String = "", sSqlUser As String
        Dim dt As New DataTable
        Try
            sSql = "Select AAPM_Resource From Audit_APM_Assignment_Details Where AAPM_CustID =" & iCustID & "  And AAPM_CompID=" & iACID & ""
            If iAudit > 0 Then
                sSql = sSql & " And AAPM_AuditCodeID=" & iAudit & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & "And AAPM_FunctionID=" & iFunID & ""
            End If
            sUser = objDBL.SQLExecuteScalar(sAC, sSql)
            If IsNothing(sUser) = False Then
                If sUser.StartsWith(",") = True Then
                    sUser = sUser.Remove(0, 1)
                End If
                If sUser.EndsWith(",") = True Then
                    sUser = sUser.Remove(Len(sUser) - 1, 1)
                End If
                sSqlUser = "Select Usr_ID,Usr_FullName From Sad_userDetails  Where Usr_ID In (" & sUser & ") And Usr_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSqlUser)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostBudgetAuditTaskDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0, iTotCost As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AuditTaskName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")


            sSql = "select CMM_Desc,AAPM_AuditTaskID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAuditID & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAuditID & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                drRow("SrNo") = i + 1
                If IsDBNull(dtAP.Rows(i)("CMM_Desc")) = False Then
                    drRow("AuditTaskName") = objclsGRACeGeneral.ReplaceSafeSQL(dtAP.Rows(i)("CMM_Desc"))
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                    iTotHrs = iTotHrs + drRow("TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                    iTotDays = iTotDays + drRow("TotalDays")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalCost")) = False Then
                    drRow("TotalCost") = dtAP.Rows(i)("ATCB_TotalCost")
                    iTotCost = iTotCost + drRow("TotalCost")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("AuditTaskName") = "Total"
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays : drRow("TotalCost") = iTotCost
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostBudgetProcessDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Dim iTotHrs As Integer = 0, iTotDays As Integer = 0, iTotCost As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ProcessName")
            dt.Columns.Add("TotalHours")
            dt.Columns.Add("TotalDays")
            dt.Columns.Add("TotalCost")

            sSql = "Select PM_NAME, AAPM_AuditTaskID, PM_ID, ATCB_PKID, ATCB_TotalDays, ATCB_TotalHours, ATCB_TotalCost, ATCB_Status From Audit_APM_Assignment_Details"
            sSql = sSql & " Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID And ATCB_AuditCodeID=" & iAuditID & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & ""
            sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & "And AAPM_CustID=" & iCustID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAuditID & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                drRow("SrNo") = i + 1
                If IsDBNull(dtAP.Rows(i)("PM_NAME")) = False Then
                    drRow("ProcessName") = objclsGRACeGeneral.ReplaceSafeSQL(dtAP.Rows(i)("PM_NAME"))
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalHours")) = False Then
                    drRow("TotalHours") = dtAP.Rows(i)("ATCB_TotalHours")
                    iTotHrs = iTotHrs + drRow("TotalHours")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalDays")) = False Then
                    drRow("TotalDays") = dtAP.Rows(i)("ATCB_TotalDays")
                    iTotDays = iTotDays + drRow("TotalDays")
                End If
                If IsDBNull(dtAP.Rows(i)("ATCB_TotalCost")) = False Then
                    drRow("TotalCost") = dtAP.Rows(i)("ATCB_TotalCost")
                    iTotCost = iTotCost + drRow("TotalCost")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtAP.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("ProcessName") = "Total"
                drRow("TotalHours") = iTotHrs : drRow("TotalDays") = iTotDays : drRow("TotalCost") = iTotCost
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDescriptionDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDesc As New DataTable
        Dim drRow As DataRow
        Dim i As Integer, iTotCost As Integer = 0
        Dim dcTotal As Decimal
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Description")
            dt.Columns.Add("Total")

            sSql = "Select Distinct(CBD_DescID) From Audit_CostBudgetDetails Where CBD_AuditCodeID=" & iAuditID & " And CBD_CompID=" & iACID & " "
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDesc.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtDesc.Rows(i)("CBD_DescID")) = False Then
                    drRow("SrNo") = i + 1
                    drRow("Description") = objDBL.SQLGetDescription(sAC, "Select CMM_Desc from Content_Management_Master where CMM_ID=" & dtDesc.Rows(i)("CBD_DescID") & " And CMM_CompID=" & iACID & "")
                    dcTotal = objDBL.SQLGetDescription(sAC, "Select Sum(CBD_Per_Head) from Audit_CostBudgetDetails where CBD_AuditCodeID=" & iAuditID & " and CBD_DescID=" & dtDesc.Rows(i)("CBD_DescID") & " And CBD_CompID=" & iACID & "")
                    drRow("Total") = Math.Ceiling(dcTotal)
                    iTotCost = iTotCost + drRow("Total")
                End If
                dt.Rows.Add(drRow)
            Next
            If dt.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("Description") = "Total" : drRow("Total") = iTotCost
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostBudgetUserDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iDescID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtuser As New DataTable
        Dim i As Integer = 0, j As Integer = 0
        Dim sResourceID As String = "", sUserIDs As String = ""
        Dim sArray As Array
        Dim drRow As DataRow
        Dim k As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Users")
            dt.Columns.Add("Charges")

            sSql = "Select AAPM_Resource From Audit_APM_Assignment_Details Where AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and AAPM_AuditCodeID=" & iAuditID & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and AAPM_FunctionID =" & iFunctionID & ""
            End If
            dtuser = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtuser.Rows.Count - 1
                If IsDBNull(dtuser.Rows(i)("AAPM_Resource")) = False Then
                    sResourceID = sResourceID & "," & dtuser.Rows(i)("AAPM_Resource")
                End If
            Next
            If sResourceID.StartsWith(",") = True Then
                sResourceID = sResourceID.Remove(0, 1)
            End If
            If sResourceID.EndsWith(",") = True Then
                sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
            End If

            If sResourceID <> "" Then
                sArray = sResourceID.Split(",")
                For j = 0 To sArray.Length - 1
                    If sArray(j) <> "" Then
                        If sUserIDs.Contains("," & sArray(j) & ",") = False Then
                            sUserIDs = sUserIDs & "," & sArray(j) & ","
                            drRow = dt.NewRow
                            k = k + 1
                            drRow("SrNo") = k
                            drRow("Users") = objDBL.SQLGetDescription(sAC, "Select Usr_FullName from Sad_userdetails where usr_id = " & sArray(j) & "")
                            If iDescID > 0 Then
                                drRow("Charges") = objDBL.SQLGetDescription(sAC, "Select CBD_Per_Head from Audit_CostBudgetDetails where CBD_AuditCodeID=" & iAuditID & " and CBD_DescID = " & iDescID & " And  CBD_UserID = " & sArray(j) & " ")
                                If IsDBNull(drRow("Charges")) = True Then
                                    drRow("Charges") = 0
                                End If
                            Else
                                drRow("Charges") = 0
                            End If
                            dt.Rows.Add(drRow)
                        End If
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCostSheetDescriptionDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDesc As New DataTable
        Dim drRow As DataRow
        Dim i As Integer, iTotCost As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Description")
            dt.Columns.Add("Date")
            dt.Columns.Add("Comments")
            dt.Columns.Add("Cost")
            sSql = "Select CMM_ID,CMM_Desc,CSD_Date,CSD_Comments,CSD_Costs,CSD_AuditCodeID,CSD_DescID From Content_Management_Master  "
            sSql = sSql & " Left Join Audit_CostSheetDetails On CSD_DescID=CMM_ID And CSD_AuditCodeID= " & iAuditID & " And CSD_CompID=" & iACID & " And CSD_UserID=" & iUserID & ""
            sSql = sSql & " Where CMM_Category='EC' And CMM_ID in(Select CBD_DescID From Audit_CostBudgetDetails"
            sSql = sSql & " Where CBD_AuditCodeID=" & iAuditID & " And CBD_CompID=" & iACID & ") Or CMM_ID in (Select CSD_DescID From Audit_CostSheetDetails"
            sSql = sSql & " Where CSD_AuditCodeID=" & iAuditID & " And CSD_CompID=" & iACID & ")"
            sSql = sSql & " Group by CMM_ID,CMM_Desc,CSD_Date,CSD_Comments,CSD_Costs,CSD_AuditCodeID,CSD_DescID"
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDesc.Rows.Count - 1
                drRow = dt.NewRow
                drRow("SrNo") = i + 1
                If IsDBNull(dtDesc.Rows(i)("CMM_Desc")) = False Then
                    drRow("Description") = dtDesc.Rows(i)("CMM_Desc")
                End If
                If IsDBNull(dtDesc.Rows(i)("CSD_Date")) = False Then
                    drRow("Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtDesc.Rows(i)("CSD_Date"), "F")
                End If
                If IsDBNull(dtDesc.Rows(i)("CSD_Comments")) = False Then
                    drRow("Comments") = objclsGRACeGeneral.ReplaceSafeSQL(dtDesc.Rows(i)("CSD_Comments"))
                End If
                If IsDBNull(dtDesc.Rows(i)("CSD_Costs")) = False Then
                    drRow("Cost") = dtDesc.Rows(i)("CSD_Costs")
                    iTotCost = iTotCost + drRow("Cost")
                End If
                dt.Rows.Add(drRow)
            Next
            If dtDesc.Rows.Count > 0 Then
                drRow = dt.NewRow
                drRow("SrNo") = "" : drRow("Description") = "" : drRow("Date") = "Total" : drRow("Comments") = "" : drRow("Cost") = iTotCost
                dt.Rows.Add(drRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
