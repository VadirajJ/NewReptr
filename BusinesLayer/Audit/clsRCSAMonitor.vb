Imports System
Imports System.Data
Imports DatabaseLayer
Public Structure strSubPrcDet
    Dim cModule As Char
    Dim cFlgFuncBrnh As Char 'Flag to indicate if Function or a branch
    Dim iZoneID As Integer
    Dim iRegionID As Integer
    Dim iStateID As Integer
    Dim iFuncID As Integer
    Dim iSubFuncID As Integer
    Dim iPID As Integer
    Dim iSubPID As Integer
End Structure
Public Class clsRCSAMonitor
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsRiskGeneral As New clsRiskGeneral
    Public Function LoadEntityBranch(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Ent_ID,Ent_EntityNAme + '-' + ENT_Branch as ent_entityname from Mst_Entity_MASter where Ent_CompID='" & iACID & "' And ENT_DELFLG='A' And"
            sSql = sSql & " Ent_ID in (Select Distinct(MMM_FunID)from MST_MAPPING_MASTER where MMM_CUSTID=" & iCustID & " And MMM_Module='R'"
            sSql = sSql & " And MMM_CompID='" & iACID & "' And MMM_DelFlag='A') or ent_branch='B' order by ent_entityname"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunctionsforAuditDefination(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer, ByVal iYearID As Integer) As ArrayList
        Dim sSql As String
        Dim drFunctions As OleDb.OleDbDataReader
        Dim alFuntionItem As New ArrayList
        Dim Item1 As New clsGRACeGeneral.DataArrayItem
        Dim NewItem As clsGRACeGeneral.DataArrayItem
        Try
            sSql = "Select SEM_ID,SEM_NAME from Mst_SubEntity_Master WHERE SEM_CompID=" & iACID & " And SEM_ENT_ID = " & iFunctionID & " And SEM_DELFLG = 'A' order by SEM_NAME"
            drFunctions = objDBL.SQLDataReader(sAC, sSql)
            Item1.DataTextField = "--- Select All ---"
            Item1.DataValueField = 0
            alFuntionItem.Add(Item1)
            Do While drFunctions.Read
                NewItem = New clsGRACeGeneral.DataArrayItem
                NewItem.DataTextField = objclsGRACeGeneral.ReplaceSafeSQL(drFunctions("SEM_NAME"))
                NewItem.DataValueField = drFunctions("SEM_ID")
                alFuntionItem.Add(NewItem)
            Loop
            Return alFuntionItem
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRCSAFunNetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDBResult As Object
        Try
            sSql = "SELECT sum(RCSA_NetScore) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & iYearID & " AND RCSA_FunID = " & iFuncID & " And RCSA_CustID=" & iCustID & " And RCSA_CompID=" & iACID & " "
            objDBResult = objDBL.SQLExecuteScalar(sAC, sSql)
            If Not IsDBNull(objDBResult) Then
                Return Math.Round(objDBResult, 2)
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRCSAUnderFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & iYearID & " AND RCSA_FunID = " & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID=" & iACID & ""
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MRL_RiskName,RCSAD_RiskRating,MRL_IsKey,RCSAD_RiskID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RCSAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsRCSAUnderFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & iYearID & " AND RCSA_FunID = " & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID=" & iACID & ""
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_PKID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MCL_ControlName,RCSAD_ControlRating,MCL_IsKey,RCSAD_ControlID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RCSAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistory_Fun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable, ds As New DataTable
        Dim drow As DataRow
        Dim i, j As Integer
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "'  And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT * FROM Risk_RCSA WHERE RCSA_FinancialYear = " & dtYear.Rows(i)("YMS_YEARID").ToString & " AND RCSA_FunID = " & iFuncID & " And RCSA_CustID=" & iCustID & " And RCSA_CompID='" & iACID & "'"
                    ds = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If ds.Rows.Count > 0 Then
                        For j = 0 To ds.Rows.Count - 1
                            drow = dtHistory.NewRow
                            If Not IsDBNull(ds.Rows(j)("RCSA_NetScore")) Then
                                iNetRiskScore = Math.Round(ds.Rows(j)("RCSA_NetScore"), 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                            drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                            drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                            drow("Netrisk") = iNetRiskScore
                            dtHistory.Rows.Add(drow)
                        Next
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer) As ArrayList
        Dim sSql As String
        Dim drFunctions As OleDb.OleDbDataReader
        Dim alFuntionItem As New ArrayList
        Dim Item1 As New clsGRACeGeneral.DataArrayItem
        Dim NewItem As clsGRACeGeneral.DataArrayItem
        Try
            sSql = "Select PM_ID,PM_NAME from Mst_Process_Master WHERE PM_CompID='" & iACID & "' And PM_SEM_ID = " & iSubFunID & " and PM_Delflg = 'A'"
            sSql = sSql & " Order by PM_NAME asc"
            drFunctions = objDBL.SQLDataReader(sAC, sSql)
            Item1.DataTextField = "--- Select Process ---"
            Item1.DataValueField = 0
            alFuntionItem.Add(Item1)
            Do While drFunctions.Read
                NewItem = New clsGRACeGeneral.DataArrayItem
                NewItem.DataTextField = objclsGRACeGeneral.ReplaceSafeSQL(drFunctions("PM_NAME"))
                NewItem.DataValueField = drFunctions("PM_ID")
                alFuntionItem.Add(NewItem)
            Loop
            Return alFuntionItem
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubRCSAFunNetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & iYearID & " AND RCSA_FunID = " & iFuncID & " And RCSA_CustID=" & iCustID & " AND  RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT Sum(RCSAD_ResidualRiskRating) FROM Risk_RCSA_Details WHERE RCSAD_CompID='" & iACID & "' And RCSAD_SEMID = " & iSubFuncID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_CompID=" & iACID & " And RCSAD_SEMID=" & iSubFuncID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iSumOfRCSA > 0 And iCount > 0 Then
                    objDB = iSumOfRCSA / iCount
                    If Not IsDBNull(objDB) Then
                        Return Math.Round(objDB, 2)
                    Else
                        Return 0.0
                    End If
                Else
                    Return 0.0
                End If
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisksRCSAUnderSubFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MRL_RiskName,RCSAD_RiskRating,MRL_IsKey,RCSAD_RiskID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RCSAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " AND  RCSAD_SEMID=" & iSubFuncID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsRCSAUnderSubFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_PKID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MCL_ControlName,RCSAD_ControlRating,MCL_IsKey,RCSAD_ControlID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RCSAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " And RCSAD_SEMID=" & iSubFuncID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistoryRCSA_SubFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "'  And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & dtYear.Rows(i)("YMS_YEARID").ToString & " AND RCSA_FunID = " & iFuncID & " And RCSA_CustID=" & iCustID & " AND  RCSA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT Sum(RCSAD_ResidualRiskRating) FROM Risk_RCSA_Details WHERE RCSAD_CompID='" & iACID & "' And RCSAD_SEMID = " & iSubFuncID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_CompID='" & iACID & "' And RCSAD_SEMID = " & iSubFuncID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If

                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iPrcID As Integer) As ArrayList
        Dim sSql As String
        Dim drFunctions As OleDb.OleDbDataReader
        Dim alFuntionItem As New ArrayList
        Dim Item1 As New clsGRACeGeneral.DataArrayItem
        Dim NewItem As New clsGRACeGeneral.DataArrayItem
        Try
            sSql = "Select SPM_ID,SPM_NAME from Mst_Subprocess_MAster WHERE SPM_CompID='" & iACID & "' And SPM_PM_ID = " & iPrcID & " and SPM_DELFLG = 'A'"
            sSql = sSql & " Order by SPM_NAME asc"
            drFunctions = objDBL.SQLDataReader(sAC, sSql)
            Item1.DataTextField = "--- Select Sub Process ---"
            Item1.DataValueField = 0
            alFuntionItem.Add(Item1)
            If IsDBNull(drFunctions) <> True Then
                Do While drFunctions.Read
                    If IsDBNull(drFunctions("SPM_NAME")) <> True Then
                        NewItem = New clsGRACeGeneral.DataArrayItem
                        NewItem.DataTextField = objclsGRACeGeneral.ReplaceSafeSQL(drFunctions("SPM_NAME"))
                        NewItem.DataValueField = drFunctions("SPM_ID")
                        alFuntionItem.Add(NewItem)
                    End If
                Loop
            End If
            Return alFuntionItem
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetProRCSANetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT Sum(RCSAD_ResidualRiskRating) FROM Risk_RCSA_Details WHERE RCSAD_CompID='" & iACID & "' And RCSAD_PMID = " & iPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_CompID='" & iACID & "' And RCSAD_PMID = " & iPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                objDB = iSumOfRCSA / iCount
                If iSumOfRCSA > 0 And iCount > 0 Then
                    objDB = iSumOfRCSA / iCount
                    If Not IsDBNull(objDB) Then
                        Return Math.Round(objDB, 2)
                    Else
                        Return 0.0
                    End If
                Else
                    Return 0.0
                End If
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisksRCSAUnderPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MRL_RiskName,RCSAD_RiskRating,MRL_IsKey,RCSAD_RiskID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RCSAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " AND  RCSAD_PMID=" & iPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsRCSAUnderPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_PKID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MCL_ControlName,RCSAD_ControlRating,MCL_IsKey,RCSAD_ControlID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RCSAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " AND RCSAD_PMID=" & iPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistoryRCSA_Pro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer, i As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "'  And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & dtYear.Rows(i)("YMS_YEARID").ToString & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND  RCSA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT Sum(RCSAD_ResidualRiskRating) FROM Risk_RCSA_Details WHERE RCSAD_CompID='" & iACID & "' And RCSAD_PMID = " & iPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_CompID='" & iACID & "' And RCSAD_PMID = " & iPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If
                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubRCSAProNetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT Sum(RCSAD_ResidualRiskRating) FROM Risk_RCSA_Details WHERE RCSAD_CompID='" & iACID & "' And RCSAD_SPMID = " & iSubPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & "" 'ra_Process_NetRisk 
                iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_CompID='" & iACID & "' And RCSAD_SPMID = " & iSubPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                objDB = iSumOfRCSA / iCount
                If iSumOfRCSA > 0 And iCount > 0 Then
                    objDB = iSumOfRCSA / iCount
                    If Not IsDBNull(objDB) Then
                        Return Math.Round(objDB, 2)
                    Else
                        Return 0.0
                    End If
                Else
                    Return 0.0
                End If
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisksRCSAUnderSubPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MRL_RiskName,MRL_IsKey,RCSAD_RiskID,RCSAD_RiskRating,RCSAD_PKID FROM Risk_RCSA_Details"
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RCSAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " AND RCSAD_SPMID=" & iSubPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsRCSAUnderSubPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_PKID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MCL_ControlName,RCSAD_ControlRating,MCL_IsKey,RCSAD_ControlID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RCSAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " AND  RCSAD_SPMID=" & iSubPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistoryRCSA_SubPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer, i As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "'  And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & dtYear.Rows(i)("YMS_YEARID").ToString & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND  RCSA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT Sum(RCSAD_ResidualRiskRating) FROM Risk_RCSA_Details WHERE RCSAD_CompID='" & iACID & "' And RCSAD_SPMID = " & iSubPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_CompID='" & iACID & "' And RCSAD_SPMID = " & iSubPID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If
                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsRCSAUnderRiskNew(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iRiskID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "Select Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear=" & iYearID & " And RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " And RCSA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RCSAD_PKID,RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,MCL_ControlName,RCSAD_ControlRating,MCL_IsKey,RCSAD_ControlID, * FROM Risk_RCSA_Details "
                sSql = sSql & " Left Join Risk_RCSA On  RCSA_PKID=RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RCSAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RCSAD_CompID=" & iACID & " And RCSAD_RCSAPKID = " & iMaxRamID & " AND  RCSAD_SPMID=" & iSubPID & " And RCSAD_RiskID=" & iRiskID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskRCSADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iRiskID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "SELECT a.RAM_Name as RiskType,b.RAM_Name as RiskImpact,c.RAM_Name as RiskLikelihood,d.RAM_Name as ResidualRiskRating,RCSA_FactorIncrease,RCSA_FactorDecrease,MRL_InherentRiskID,MIM_Name,* FROM Risk_RCSA_Details "
            sSql = sSql & " Left Join Risk_RCSA On RCSA_PKID= RCSAD_RCSAPKID And RCSA_CompID=" & iACID & " And RCSA_CustID=" & iCustID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster a On a.RAM_PKID=RCSAD_RiskTypeID And a.RAM_Category='RT' And a.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_PKID=RCSAD_ImpactID And b.RAM_Category='RI' And b.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_PKID=RCSAD_LikelihoodID And c.RAM_Category='RL' And c.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster d On d.RAM_Score=RCSAD_ResidualRiskRating And d.RAM_Category='RRS' And d.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RCSAD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_InherentRisk_Master On MIM_ID=MRL_InherentRiskID And MIM_CompID=" & iACID & ""
            sSql = sSql & " WHERE RCSA_FunID = " & iFuncID & " And RCSA_CustID=" & iCustID & " And RCSA_CompID='" & iACID & "' AND RCSA_FinancialYear = " & iYearID & " AND RCSAD_SPMID = " & iSubPID & " And RCSAD_CompID='" & iACID & "' AND RCSAD_RiskID = " & iRiskID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistoryRCSA_RiskNew(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iRiskID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer, i As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "'  And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RCSA_PKID) FROM Risk_RCSA WHERE RCSA_FinancialYear = " & dtYear.Rows(i)("YMS_YEARID").ToString & " AND RCSA_FunID=" & iFuncID & " And RCSA_CustID=" & iCustID & " AND RCSA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT  Sum(RCSAD_ResidualRiskRating) FROM   Risk_RCSA_Details WHERE RCSAD_CompID='" & iACID & "' And RCSAD_RiskID = " & iRiskID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_CompID='" & iACID & "' And RCSAD_RiskID = " & iRiskID & " AND RCSAD_RCSAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If
                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisks(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubProID As Integer, ByVal iFunID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Distinct(MMM_RiskID),MMM_Risk From MST_MAPPING_MASTER Where MMM_SPMID=" & iSubProID & " And MMM_FunID=" & iFunID & ""
            sSql = sSql & " And MMM_CUSTID=" & iCustID & " And MMM_Module='R' And MMM_DelFlag='A' And MMM_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    ' RA  Monitor
    Public Function GetFunNetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDBResult As Object
        Try
            sSql = "SELECT sum(RA_NetScore) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "' "
            objDBResult = objDBL.SQLExecuteScalar(sAC, sSql)
            If Not IsDBNull(objDBResult) Then
                Return Math.Round(objDBResult, 2)
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisksUnderFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "' "
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_SEMID,RAD_PMID,RAD_SPMID,MRL_RiskName,RAD_RiskRating,MRL_IsKey,RAD_RiskID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On  RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsUnderFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "' "
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_PKID,RAD_SEMID,RAD_PMID,RAD_SPMID,MCL_ControlName,RAD_ControlRating,MCL_IsKey,RAD_ControlID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On  RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRAHistory_Fun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable, ds As New DataTable
        Dim drow As DataRow
        Dim i, j As Integer
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "' And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT * FROM Risk_RA WHERE RA_FinancialYear=" & dtYear.Rows(i)("YMS_YEARID").ToString & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "'"
                    ds = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If ds.Rows.Count > 0 Then
                        For j = 0 To ds.Rows.Count - 1
                            drow = dtHistory.NewRow
                            If Not IsDBNull(ds.Rows(j)("RA_NetScore")) Then
                                iNetRiskScore = Math.Round(ds.Rows(j)("RA_NetScore"), 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                            drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                            drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                            drow("Netrisk") = iNetRiskScore
                            dtHistory.Rows.Add(drow)
                        Next
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunNetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer
        Dim iCount As Integer = 0, iSumOfRCSA As Integer = 0
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "' "
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT Sum(RAD_ResidualRiskRating) FROM Risk_RA_Details WHERE RAD_CompID='" & iACID & "' And RAD_SEMID = " & iSubFuncID & " AND RAD_RAPKID = " & iMaxRamID & ""
                iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select Count(*) From Risk_RA_Details Where RAD_CompID='" & iACID & "' And RAD_SEMID = " & iSubFuncID & " AND RAD_RAPKID = " & iMaxRamID & ""
                iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iSumOfRCSA > 0 And iCount > 0 Then
                    objDB = iSumOfRCSA / iCount
                    If Not IsDBNull(objDB) Then
                        Return Math.Round(objDB, 2)
                    Else
                        Return 0.0
                    End If
                Else
                    Return 0.0
                End If
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisksUnderSubFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "' "
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_SEMID,RAD_PMID,RAD_SPMID,MRL_RiskName,RAD_RiskRating,MRL_IsKey,RAD_RiskID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On  RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " And  RAD_SEMID=" & iSubFuncID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsUnderSubFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "' "
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_PKID,RAD_SEMID,RAD_PMID,RAD_SPMID,MCL_ControlName,RAD_ControlRating,MCL_IsKey,RAD_ControlID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On  RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " And  RAD_SEMID=" & iSubFuncID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistory_SubFun(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubFuncID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer, i As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim iCount As Integer = 0, iSumOfRCSA As Integer = 0
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "'  And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT Sum(RAD_ResidualRiskRating) FROM Risk_RA_Details WHERE RAD_CompID='" & iACID & "' And RAD_SEMID = " & iSubFuncID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RA_Details Where RAD_CompID='" & iACID & "' And RAD_SEMID = " & iSubFuncID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If
                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetProNetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT Sum(RAD_ResidualRiskRating) FROM Risk_RA_Details WHERE RAD_CompID='" & iACID & "' And RAD_PMID = " & iPID & " AND RAD_RAPKID = " & iMaxRamID & ""
                iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select Count(*) From Risk_RA_Details Where RAD_CompID='" & iACID & "' And RAD_PMID = " & iPID & " AND RAD_RAPKID = " & iMaxRamID & ""
                iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iSumOfRCSA > 0 And iCount > 0 Then
                    objDB = iSumOfRCSA / iCount
                    If Not IsDBNull(objDB) Then
                        Return Math.Round(objDB, 2)
                    Else
                        Return 0.0
                    End If
                Else
                    Return 0.0
                End If
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisksUnderPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_SEMID,RAD_PMID,RAD_SPMID,MRL_RiskName,RAD_RiskRating,MRL_IsKey,RAD_RiskID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " AND  RAD_PMID=" & iPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsUnderPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_PKID,RAD_SEMID,RAD_PMID,RAD_SPMID,MCL_ControlName,RAD_ControlRating,MCL_IsKey,RAD_ControlID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " AND RAD_PMID=" & iPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistory_Pro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer, i As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim iCount As Integer = 0, iSumOfRCSA As Integer = 0
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "' And YMS_YearID<=" & iYearID & " And YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT Sum(RAD_ResidualRiskRating) FROM Risk_RA_Details WHERE RAD_CompID='" & iACID & "' And RAD_PMID = " & iPID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RA_Details Where RAD_CompID='" & iACID & "' And RAD_PMID = " & iPID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If
                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubProNetRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Double
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "Select Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " And RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT Sum(RAD_ResidualRiskRating) FROM Risk_RA_Details WHERE RAD_CompID='" & iACID & "' And RAD_SPMID = " & iSubPID & " AND RAD_RAPKID = " & iMaxRamID & "" 'ra_Sub_NetRisk
                iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select Count(*) From Risk_RA_Details Where RAD_CompID='" & iACID & "' And RAD_SPMID = " & iSubPID & " AND RAD_RAPKID = " & iMaxRamID & ""
                iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iSumOfRCSA > 0 And iCount > 0 Then
                    objDB = iSumOfRCSA / iCount
                    If Not IsDBNull(objDB) Then
                        Return Math.Round(objDB, 2)
                    Else
                        Return 0.0
                    End If
                Else
                    Return 0.0
                End If
            Else
                Return 0.0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisksUnderSubPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_PKID,RAD_SEMID,RAD_PMID,RAD_SPMID,MRL_RiskName,MRL_IsKey,RAD_RiskID,RAD_RiskRating FROM Risk_RA_Details "
                sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RAD_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " AND   RAD_SPMID=" & iSubPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsUnderSubPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_PKID,RAD_SEMID,RAD_PMID,RAD_SPMID,MCL_ControlName,RAD_ControlRating,MCL_IsKey,RAD_ControlID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On  RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " AND  RAD_SPMID=" & iSubPID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistory_SubPro(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer, i As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim iCount As Integer = 0, iSumOfRCSA As Integer = 0
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "' And YMS_YearID<=" & iYearID & " And YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT Sum(RAD_ResidualRiskRating) FROM Risk_RA_Details WHERE RAD_CompID='" & iACID & "' And RAD_SPMID = " & iSubPID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RA_Details Where RAD_CompID='" & iACID & "' And RAD_SPMID = " & iSubPID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If

                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlsUnderRiskNew(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iRiskID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim iMaxRamID As Integer
        Dim dt As New DataTable
        Try
            sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND RA_CompID='" & iACID & "'"
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                sSql = "SELECT DISTINCT " & iFuncID & " AS FuncID,RAD_PKID,RAD_SEMID,RAD_PMID,RAD_SPMID,MCL_ControlName,RAD_ControlRating,MCL_IsKey,RAD_ControlID, * FROM Risk_RA_Details "
                sSql = sSql & " Left Join Risk_RA On  RA_PKID=RAD_RAPKID And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
                sSql = sSql & " Left Join MST_CONTROL_Library On  MCL_PKID=RAD_ControlID And  MCL_CompID=" & iACID & ""
                sSql = sSql & " WHERE RAD_CompID=" & iACID & " And RAD_RAPKID = " & iMaxRamID & " AND  RAD_SPMID=" & iSubPID & " And RAD_RiskID=" & iRiskID & " "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iSubPID As Integer, ByVal iRiskID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "SELECT a.RAM_Name as RiskType,b.RAM_Name as RiskImpact,c.RAM_Name as RiskLikelihood,d.RAM_Name as ResidualRiskRating,MRL_InherentRiskID,MIM_Name,* FROM Risk_RA_Details "
            sSql = sSql & " Left Join Risk_RA On RA_PKID= RAD_RAPKID  And RA_CompID=" & iACID & " And RA_CustID=" & iCustID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster a On a.RAM_PKID=RAD_RiskTypeID And a.RAM_Category='RT' And a.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_PKID=RAD_ImpactID And b.RAM_Category='RI' And b.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_PKID=RAD_LikelihoodID And c.RAM_Category='RL' And c.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster d On d.RAM_Score=RAD_ResidualRiskRating And d.RAM_Category='RRS' And d.RAM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join MST_RISK_Library On  MRL_PKID=RAD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_InherentRisk_Master On MIM_ID=MRL_InherentRiskID And MIM_CompID=" & iACID & ""
            sSql = sSql & " WHERE RA_FunID = " & iFuncID & " And RA_CustID=" & iCustID & " And RA_CompID='" & iACID & "' AND RA_FinancialYear = " & iYearID & " AND RAD_SPMID = " & iSubPID & " And RAD_CompID='" & iACID & "' AND RAD_RiskID = " & iRiskID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistory_RiskNew(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iRiskID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim objDB As Object
        Dim iMaxRamID As Integer, i As Integer
        Dim iNetRiskScore As Double
        Dim dtYear As New DataTable, dtHistory As New DataTable
        Dim drow As DataRow
        Dim iCount As Integer = 0, iSumOfRCSA As Integer = 0
        Try
            sSql = "Select * from Year_Master where yms_compid='" & iACID & "'  And YMS_YearID<=" & iYearID & " And   YMS_Delflag='A' Order by YMS_YearID DESC"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtHistory.Columns.Add("YearID")
            dtHistory.Columns.Add("Year")
            dtHistory.Columns.Add("Netrisk")
            If dtYear.Rows.Count > 0 Then
                For i = 0 To dtYear.Rows.Count - 1
                    sSql = "SELECT Max(RA_PKID) FROM Risk_RA WHERE RA_FinancialYear=" & iYearID & " AND RA_FunID=" & iFuncID & " And RA_CustID=" & iCustID & " AND  RA_CompID='" & iACID & "'"
                    If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                        iMaxRamID = objDBL.SQLExecuteScalar(sAC, sSql)
                        sSql = "SELECT DISTINCT  Sum(RAD_ResidualRiskRating) FROM  Risk_RA_Details WHERE RAD_CompID='" & iACID & "' And RAD_RiskID = " & iRiskID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "Select Count(*) From Risk_RA_Details Where RAD_CompID='" & iACID & "' And RAD_RiskID = " & iRiskID & " AND RAD_RAPKID = " & iMaxRamID & ""
                        iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        If iSumOfRCSA > 0 And iCount > 0 Then
                            objDB = iSumOfRCSA / iCount
                            If Not IsDBNull(objDB) Then
                                iNetRiskScore = Math.Round(objDB, 2)
                            Else
                                iNetRiskScore = 0.0
                            End If
                        Else
                            iNetRiskScore = 0.0
                        End If
                        drow = dtHistory.NewRow
                        drow("YearID") = dtYear.Rows(i)("YMS_YearID").ToString
                        drow("Year") = dtYear.Rows(i)("YMS_ID").ToString
                        drow("Netrisk") = iNetRiskScore
                        dtHistory.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtHistory
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
