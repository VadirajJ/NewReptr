Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsProcessMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim iPM_ID As Integer
    Dim iPM_ENT_ID As Integer
    Dim iPM_SEMENT_ID As Integer
    Dim sPM_CODE As String
    Dim sPM_NAME As String
    Dim iPM_CRBY As Integer
    Dim iPM_UPDATEDBY As Integer
    Dim sPM_Desc As String
    Dim iPM_COMPID As Integer
    Dim sPM_IPAddress As String

    'Sub Process
    Dim iSPM_ID As Integer
    Dim iSPM_ENT_ID As Integer
    Dim iSPM_SEMENT_ID As Integer
    Dim iSPM_PM_ID As Integer
    Dim iSPM_IsKey As Integer
    Dim sSPM_CODE As String
    Dim sSPM_NAME As String
    Dim iSPM_CRBY As Integer
    Dim iSPM_UPDATEDBY As Integer
    Dim sSPM_Desc As String
    Dim iSPM_COMPID As Integer
    Dim sSPM_IPAddress As String
    Public Property iSPMIsKey() As Integer
        Get
            Return (iSPM_IsKey)
        End Get
        Set(ByVal value As Integer)
            iSPM_IsKey = value
        End Set
    End Property
    Public Property iPMID() As Integer
        Get
            Return (iPM_ID)
        End Get
        Set(ByVal value As Integer)
            iPM_ID = value
        End Set
    End Property
    Public Property iPMENTID() As Integer
        Get
            Return (iPM_ENT_ID)
        End Get
        Set(ByVal value As Integer)
            iPM_ENT_ID = value
        End Set
    End Property
    Public Property iPMSEMENTID() As Integer
        Get
            Return (iPM_SEMENT_ID)
        End Get
        Set(ByVal value As Integer)
            iPM_SEMENT_ID = value
        End Set
    End Property
    Public Property sPMCODE() As String
        Get
            Return (sPM_CODE)
        End Get
        Set(ByVal value As String)
            sPM_CODE = value
        End Set
    End Property
    Public Property sPMNAME() As String
        Get
            Return (sPM_NAME)
        End Get
        Set(ByVal value As String)
            sPM_NAME = value
        End Set
    End Property
    Public Property iPMCRBY() As Integer
        Get
            Return (iPM_CRBY)
        End Get
        Set(ByVal value As Integer)
            iPM_CRBY = value
        End Set
    End Property
    Public Property iPMUPDATEDBY() As Integer
        Get
            Return (iPM_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            iPM_UPDATEDBY = Value
        End Set
    End Property
    Public Property sPMDesc() As String
        Get
            Return (sPM_Desc)
        End Get
        Set(ByVal value As String)
            sPM_Desc = value
        End Set
    End Property
    Public Property iPMCOMPID() As Integer
        Get
            Return (iPM_COMPID)
        End Get
        Set(ByVal Value As Integer)
            iPM_COMPID = Value
        End Set
    End Property
    Public Property sPMIPAddress() As String
        Get
            Return (sPM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sPM_IPAddress = Value
        End Set
    End Property
    'Sub Process'
    Public Property iSPMID() As Integer
        Get
            Return (iSPM_ID)
        End Get
        Set(ByVal value As Integer)
            iSPM_ID = value
        End Set
    End Property
    Public Property iSPMENT_ID() As Integer
        Get
            Return (iSPM_ENT_ID)
        End Get
        Set(ByVal value As Integer)
            iSPM_ENT_ID = value
        End Set
    End Property
    Public Property iSPMSEMENT_ID() As Integer
        Get
            Return (iSPM_SEMENT_ID)
        End Get
        Set(ByVal value As Integer)
            iSPM_SEMENT_ID = value
        End Set
    End Property
    Public Property iSPMPM_ID() As Integer
        Get
            Return (iSPM_PM_ID)
        End Get
        Set(ByVal value As Integer)
            iSPM_PM_ID = value
        End Set
    End Property
    Public Property sSPMCODE() As String
        Get
            Return (sSPM_CODE)
        End Get
        Set(ByVal value As String)
            sSPM_CODE = value
        End Set
    End Property
    Public Property sSPMNAME() As String
        Get
            Return (sSPM_NAME)
        End Get
        Set(ByVal value As String)
            sSPM_NAME = value
        End Set
    End Property
    Public Property iSPMCRBY() As Integer
        Get
            Return (iSPM_CRBY)
        End Get
        Set(ByVal value As Integer)
            iSPM_CRBY = value
        End Set
    End Property
    Public Property iSPMUPDATEDBY() As Integer
        Get
            Return (iSPM_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            iSPM_UPDATEDBY = Value
        End Set
    End Property
    Public Property sSPMDesc() As String
        Get
            Return (sSPM_Desc)
        End Get
        Set(ByVal value As String)
            sSPM_Desc = value
        End Set
    End Property
    Public Property iSPMCOMPID() As Integer
        Get
            Return (iSPM_COMPID)
        End Get
        Set(ByVal Value As Integer)
            iSPM_COMPID = Value
        End Set
    End Property
    Public Property sSPMIPAddress() As String
        Get
            Return (sSPM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSPM_IPAddress = Value
        End Set
    End Property
    Public Function LoadProcessDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from MST_PROCESS_MASTER where PM_COMPID=" & iACID & ""
            If iProcessID > 0 Then
                sSql = sSql & " And PM_ID = " & iProcessID & "  "
            End If
            If iSubFunID > 0 Then
                sSql = sSql & " And PM_SEM_ID =" & iSubFunID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubProcessDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from MST_SUBPROCESS_MASTER where SPM_ID = " & iProcessID & " And SPM_COMPID=" & iACID & ""
            If iSubProcessID > 0 Then
                sSql = sSql & " And SPM_PM_ID =" & iSubProcessID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadProcessFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select SUBENT_Ent_ID from MST_SUBENTITY_MASTER Where SUBENT_ID IN (Select PM_SUBENT_ID from MST_PROCESS_MASTER where PM_COMPID=" & iACID & " And PM_ID=" & iProcessID & ")"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadProcessSubFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select PM_SUBENT_ID from MST_PROCESS_MASTER where PM_COMPID=" & iACID & " And PM_ID=" & iProcessID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ActivateApproveProcessMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iProcessID As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update MST_PROCESS_MASTER Set "
            If sStatus = "Created" Then
                sSql = sSql & " PM_DELFLG='A',PM_STATUS='A',PM_ApprovedBY=" & iUserID & ",PM_ApprovedON=Getdate(),"
            ElseIf sStatus = "DeActivated" Then
                sSql = sSql & " PM_DELFLG='D',PM_STATUS='AD',PM_DeletedBY=" & iUserID & ",PM_DeletedON=Getdate(),"
            ElseIf sStatus = "Activated" Then
                sSql = sSql & " PM_DELFLG='A',PM_STATUS='AR',PM_RecallBY=" & iUserID & ",PM_RecallON=Getdate(),"
            End If
            sSql = sSql & " PM_IPAddress='" & sIPAddress & "' where PM_CompID=" & iACID & " And PM_ID = " & iProcessID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadExistingProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("ProcessCode")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("Status")

            sSql = "Select PM_ID,PM_ENT_ID,PM_SEM_ID,PM_SEM_ID,PM_CODE,PM_NAME,PM_DELFLG From MST_PROCESS_MASTER EM where PM_compid=" & iACID & ""
            If iSubFunID > 0 Then
                sSql = sSql & " And PM_SEM_ID=" & iSubFunID & ""
            End If
            sSql = sSql & " Order By PM_NAME Asc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("ProcessID") = dt.Rows(i)("PM_ID")
                dRow("Function") = objDBL.SQLGetDescription(sAC, "Select Ent_EntityName from MST_ENTITY_MASTER Where Ent_ID=" & dt.Rows(i)("PM_ENT_ID") & " And Ent_CompID=" & iACID & " ")
                dRow("SubFunction") = objDBL.SQLGetDescription(sAC, "Select SEM_Name From MST_SUBENTITY_MASTER Where SEM_ID=" & dt.Rows(i)("PM_SEM_ID") & " And SEM_CompID=" & iACID & " ")
                dRow("ProcessCode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PM_CODE"))
                dRow("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PM_NAME"))
                If dt.Rows(i)("PM_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("PM_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("PM_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingSubProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcessCode")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("Status")
            sSql = "Select SPM_ID,SPM_PM_ID,SPM_ENT_ID,SPM_SEM_ID,SPM_CODE,SPM_NAME,SPM_DELFLG From MST_SUBPROCESS_MASTER EM where SPM_compid=" & iACID & ""
            If iProcessID > 0 Then
                sSql = sSql & " And SPM_PM_ID=" & iProcessID & ""
            End If
            sSql = sSql & " Order By SPM_NAME Asc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("SubProcessID") = dt.Rows(i)("SPM_ID")
                dRow("Function") = objDBL.SQLGetDescription(sAC, "Select Ent_EntityName from MST_ENTITY_MASTER Where Ent_ID=" & dt.Rows(i)("SPM_ENT_ID") & " And Ent_CompID=" & iACID & " ")
                dRow("SubFunction") = objDBL.SQLGetDescription(sAC, "Select SEM_Name From MST_SUBENTITY_MASTER Where SEM_ID=" & dt.Rows(i)("SPM_SEM_ID") & " And SEM_CompID=" & iACID & " ")
                dRow("Process") = objDBL.SQLGetDescription(sAC, "Select PM_NAME From MST_PROCESS_MASTER Where PM_ID=" & dt.Rows(i)("SPM_PM_ID") & " AND PM_CompID=" & iACID & " ")
                dRow("SubProcessCode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SPM_CODE"))
                dRow("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SPM_NAME"))
                If dt.Rows(i)("SPM_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("SPM_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("SPM_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load Process
    Public Function LoadAllProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer, ByVal sSearch As String, ByVal sCheckStatus As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select PM_ID, PM_NAME from MST_PROCESS_MASTER where PM_COMPID=" & iACID & ""
            If sCheckStatus = "YES" Then
                sSql = sSql & " And PM_DelFlg='A' "
            End If
            If iSubFunID > 0 Then
                sSql = sSql & " and PM_SEM_ID=" & iSubFunID & ""
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (PM_NAME like '" & sSearch & "%')"
            End If
            sSql = sSql & " order by PM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load Sub Process
    Public Function LoadAllSubProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessId As Integer, ByVal sSearch As String, ByVal sCheckStatus As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SPM_ID, SPM_NAME from MST_SUBPROCESS_MASTER where SPM_COMPID=" & iACID & ""
            If sCheckStatus = "YES" Then
                sSql = sSql & " And SPM_DelFlg='A'"
            End If
            If iProcessId > 0 Then
                sSql = sSql & " And SPM_PM_ID = " & iProcessId & " "
            End If
            sSql = sSql & " Order by SPM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ActivateApproveSubProcessMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iSubProcessID As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update MST_SUBPROCESS_MASTER Set "
            If sStatus = "Created" Then
                sSql = sSql & "SPM_DELFLG='A',SPM_STATUS='A',SPM_ApprovedBY=" & iUserID & ",SPM_ApprovedON=Getdate()"
            ElseIf sStatus = "DeActivated" Then
                sSql = sSql & "SPM_DELFLG='D',SPM_STATUS='AD',SPM_DeletedBY=" & iUserID & ",sPM_DeletedON=Getdate()"
            ElseIf sStatus = "Activated" Then
                sSql = sSql & "SPM_DELFLG='A',SPM_STATUS='AR',SPM_RecallBY=" & iUserID & ",SPM_RecallON=Getdate()"
            End If
            sSql = sSql & " where SPM_CompID=" & iACID & " And SPM_ID = " & iSubProcessID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'To check Process Name Exists
    Public Function CheckProcessNameExist(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer, ByVal sProcess As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from MST_PROCESS_MASTER where PM_NAME= '" & sProcess & "' and PM_SEM_ID=" & iSubFunID & " And  PM_COMPID=" & iACID & ""
            If iProcessID > 0 Then
                sSql = sSql & " And PM_ID<>" & iProcessID & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'To check SubProcess Name Exists 
    Public Function CheckSubProcessNameExist(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, ByVal sSubProcess As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from MST_SUBPROCESS_MASTER where SPM_NAME= '" & sSubProcess & "' And SPM_PM_ID=" & iProcessID & " and SPM_COMPID=" & iACID & ""
            If iSubProcessID > 0 Then
                sSql = sSql & " And SPM_ID<>" & iSubProcessID & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'To check SubProcess Code Exists
    Public Function CheckSubProcessCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select SPM_ID from MST_SUBPROCESS_MASTER where SPM_CODE = '" & sCode & "' and SPM_COMPID =" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Process
    Public Function SaveProcessDetails(ByVal sAC As String, ByVal objProcess As clsProcessMaster, ByVal iFunctionId As Integer, ByVal iSubFunctionId As Integer) As Array
        Dim ObjPParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iPParamCount As Integer
        Dim Arr(1) As String
        Try
            iPParamCount = 0
            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_ID", OleDb.OleDbType.Integer)
            ObjPParam(iPParamCount).Value = objProcess.iPMID
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_ENT_ID", OleDb.OleDbType.Integer)
            ObjPParam(iPParamCount).Value = iFunctionId
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_SEM_ID", OleDb.OleDbType.Integer)
            ObjPParam(iPParamCount).Value = iSubFunctionId
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_CODE", OleDb.OleDbType.VarChar, 20)
            ObjPParam(iPParamCount).Value = objProcess.sPMCODE
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_NAME", OleDb.OleDbType.VarChar, 500)
            ObjPParam(iPParamCount).Value = objProcess.sPMNAME
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_CRBY", OleDb.OleDbType.Integer)
            ObjPParam(iPParamCount).Value = objProcess.iPMCRBY
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjPParam(iPParamCount).Value = objProcess.iPMUPDATEDBY
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjPParam(iPParamCount).Value = objProcess.sPMDesc
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_COMPID", OleDb.OleDbType.Integer)
            ObjPParam(iPParamCount).Value = objProcess.iPMCOMPID
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@PM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjPParam(iPParamCount).Value = objProcess.sPMIPAddress
            ObjPParam(iPParamCount).Direction = ParameterDirection.Input
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjPParam(iPParamCount).Direction = ParameterDirection.Output
            iPParamCount += 1

            ObjPParam(iPParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjPParam(iPParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_PROCESS_MASTER", 1, Arr, ObjPParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    ' SubProcess
    Public Function SaveSubProcessDetails(ByVal sAC As String, ByVal objSubProcess As clsProcessMaster, ByVal iFunID As Integer, ByVal iSubID As Integer, ByVal iProcessId As Integer) As Array
        Dim ObjSPParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iSPParamCount As Integer
        Dim Arr(1) As String
        Try
            iSPParamCount = 0
            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_ID", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = objSubProcess.iSPMID
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_ENT_ID", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = iFunID
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_SEM_ID", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = iSubID
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_PM_ID", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = iProcessId
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_CODE", OleDb.OleDbType.VarChar, 20)
            ObjSPParam(iSPParamCount).Value = objSubProcess.sSPMCODE
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_NAME", OleDb.OleDbType.VarChar, 500)
            ObjSPParam(iSPParamCount).Value = objSubProcess.sSPMNAME
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_CRBY", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = objSubProcess.iSPM_CRBY
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = objSubProcess.iSPMUPDATEDBY
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjSPParam(iSPParamCount).Value = objSubProcess.sSPMDesc
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_IsKey ", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = objSubProcess.iSPMIsKey
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_COMPID", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Value = objSubProcess.iSPMCOMPID
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@SPM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSPParam(iSPParamCount).Value = objSubProcess.sSPMIPAddress
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Input
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Output
            iSPParamCount += 1

            ObjSPParam(iSPParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSPParam(iSPParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_SUBPROCESS_MASTER", 1, Arr, ObjSPParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
