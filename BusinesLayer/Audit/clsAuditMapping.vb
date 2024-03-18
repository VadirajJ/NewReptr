Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAuditMapping
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private iMMM_ID As Integer
    Private iMMM_YearID As Integer
    Private iMMM_CustID As Integer
    Private iMMM_FunID As Integer
    Private iMMM_SEMID As Integer
    Private iMMM_PMID As Integer
    Private iMMM_SPMID As Integer
    Private iMMM_RISKID As Integer
    Private iMMM_CONTROLID As Integer
    Private sMMM_CHECKS As String
    Private iMMM_KEY As Integer
    Private iMMM_CRBY As Integer
    Private iMMM_UpdatedBY As Integer
    Private sMMM_IPaddress As String
    Private iMMM_CompID As Integer
    Private sMMM_RISK As String
    Private sMMM_CONTROL As String
    Private iMMM_CHECKSID As Integer
    Private iMMM_InherentRiskID As Integer
    Private sMMM_InherentRisk As String
    Private sMMM_Module As String
    Private iMMM_SPMKey As Integer
    Private iMMM_RiskKey As Integer
    Private iMMM_ControlKey As Integer
    Private iMMM_ChecksKey As Integer
    Public Property iMMMChecksKey() As Integer
        Get
            Return (iMMM_ChecksKey)
        End Get
        Set(ByVal Value As Integer)
            iMMM_ChecksKey = Value
        End Set
    End Property
    Public Property iMMMSPMKey() As Integer
        Get
            Return (iMMM_SPMKey)
        End Get
        Set(ByVal Value As Integer)
            iMMM_SPMKey = Value
        End Set
    End Property
    Public Property iMMMRiskKey() As Integer
        Get
            Return (iMMM_RiskKey)
        End Get
        Set(ByVal Value As Integer)
            iMMM_RiskKey = Value
        End Set
    End Property
    Public Property iMMMControlKey() As Integer
        Get
            Return (iMMM_ControlKey)
        End Get
        Set(ByVal Value As Integer)
            iMMM_ControlKey = Value
        End Set
    End Property
    Public Property iMMMUpdatedBY() As Integer
        Get
            Return (iMMM_UpdatedBY)
        End Get
        Set(ByVal Value As Integer)
            iMMM_UpdatedBY = Value
        End Set
    End Property
    Public Property iMMMID() As Integer
        Get
            Return (iMMM_ID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_ID = Value
        End Set
    End Property
    Public Property iMMMYearID() As Integer
        Get
            Return (iMMM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_YearID = Value
        End Set
    End Property
    Public Property iMMMCustID() As Integer
        Get
            Return (iMMM_CustID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_CustID = Value
        End Set
    End Property
    Public Property iMMMFunID() As Integer
        Get
            Return (iMMM_FunID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_FunID = Value
        End Set
    End Property
    Public Property iMMMSEMID() As Integer
        Get
            Return (iMMM_SEMID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_SEMID = Value
        End Set
    End Property
    Public Property iMMMPMID() As Integer
        Get
            Return (iMMM_PMID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_PMID = Value
        End Set
    End Property
    Public Property iMMMSPMID() As Integer
        Get
            Return (iMMM_SPMID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_SPMID = Value
        End Set
    End Property
    Public Property iMMMRISKID() As Integer
        Get
            Return (iMMM_RISKID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_RISKID = Value
        End Set
    End Property
    Public Property iMMMCONTROLID() As Integer
        Get
            Return (iMMM_CONTROLID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_CONTROLID = Value
        End Set
    End Property
    Public Property sMMMCHECKS() As String
        Get
            Return (sMMM_CHECKS)
        End Get
        Set(ByVal Value As String)
            sMMM_CHECKS = Value
        End Set
    End Property
    Public Property iMMMKEY() As Integer
        Get
            Return (iMMM_KEY)
        End Get
        Set(ByVal Value As Integer)
            iMMM_KEY = Value
        End Set
    End Property
    Public Property iMMMCRBY() As Integer
        Get
            Return (iMMM_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iMMM_CRBY = Value
        End Set
    End Property
    Public Property sMMMIPaddress() As String
        Get
            Return (sMMM_IPaddress)
        End Get
        Set(ByVal Value As String)
            sMMM_IPaddress = Value
        End Set
    End Property
    Public Property iMMMCompID() As Integer
        Get
            Return (iMMM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_CompID = Value
        End Set
    End Property
    Public Property sMMMRISK() As String
        Get
            Return (sMMM_RISK)
        End Get
        Set(ByVal Value As String)
            sMMM_RISK = Value
        End Set
    End Property
    Public Property sMMMCONTROL() As String
        Get
            Return (sMMM_CONTROL)
        End Get
        Set(ByVal Value As String)
            sMMM_CONTROL = Value
        End Set
    End Property
    Public Property iMMMCHECKSID() As Integer
        Get
            Return (iMMM_CHECKSID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_CHECKSID = Value
        End Set
    End Property
    Public Property iMMMInherentRiskID() As Integer
        Get
            Return (iMMM_InherentRiskID)
        End Get
        Set(ByVal Value As Integer)
            iMMM_InherentRiskID = Value
        End Set
    End Property
    Public Property sMMMModule() As String
        Get
            Return (sMMM_Module)
        End Get
        Set(ByVal Value As String)
            sMMM_Module = Value
        End Set
    End Property

    Public Property sMMMInherentRisk() As String
        Get
            Return (sMMM_InherentRisk)
        End Get
        Set(ByVal Value As String)
            sMMM_InherentRisk = Value
        End Set
    End Property
    Public Function GetMapID(ByVal AC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iSEMID As Integer, ByVal iProcessID As Integer,
                             ByVal iSubPID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iCheckID As Integer, ByVal sModule As String) As Boolean
        Dim sSql As String
        Dim iMapID As Integer
        Try
            sSql = "Select MMM_ID FROM MST_MAPPING_MASTER WHERE MMM_CUSTID=" & iCustID & " And MMM_FunID=" & iFunID & " And MMM_SEMID=" & iSEMID & ""
            sSql = sSql & " AND MMM_PMID=" & iProcessID & " AND MMM_SPMID = " & iSubPID & ""
            sSql = sSql & " AND MMM_RISKID = " & iRiskID & " AND MMM_CONTROLID = " & iControlID & " And MMM_ChecksID=" & iCheckID & " And MMM_CompID=" & iACID & ""
            sSql = sSql & " And MMM_Module='" & sModule & "' And MMM_YearID=" & iYearID & ""
            iMapID = objDBL.SQLCheckForRecord(AC, sSql)
            Return iMapID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckMapIDDeleted(ByVal sAC As String, ByVal iACID As Integer, ByVal iMapID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select MMM_ID From MST_MAPPING_MASTER where MMM_ID=" & iMapID & " And MMM_DELFLAG='D' And MMM_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChecksMasterGrid(ByVal AC As String, ByVal iACID As Integer, ByVal iControlID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("Category")
            dtTab.Columns.Add("Key")

            sSql = "Select Chk_ID,Chk_CheckName,Chk_IsKey,Chk_CatID from MST_Checks_Master Where CHK_DelFlag='A' And Chk_CompID= " & iACID & " And Chk_ControlID=" & iControlID & " order by Chk_CheckName"
            dt = objDBL.SQLExecuteDataTable(AC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("ChecksID") = dt.Rows(i)("Chk_ID")
                dr("Checks") = dt.Rows(i)("Chk_CheckName")
                dr("Category") = objDBL.SQLGetDescription(AC, "Select  RAM_Name from Risk_GeneralMaster  Where RAM_PKID=" & dt.Rows(i)("Chk_CatID") & " And RAM_CompID=" & iACID & "")
                If dt.Rows(i)("Chk_IsKey") = "0" Then
                    dr("Key") = "NON-KEY"
                ElseIf dt.Rows(i)("Chk_IsKey") = "1" Then
                    dr("Key") = "KEY"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappingGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sModule As String, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer, ByVal iSubprocessID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("MappingID")
            dtTab.Columns.Add("CustID")
            dtTab.Columns.Add("FunctionID")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("Status")

            sSql = "Select MMM_ID,MMM_CUSTID,MMM_FunID,MMM_SEMID,MMM_PMID,MMM_SPMID,MMM_RISKID,MMM_Risk,MMM_CONTROLID,MMM_Control,MMM_ChecksID,MMM_CHECKS,MMM_Status,"
            sSql = sSql & " MMM_DelFlag from MST_MAPPING_MASTER Where MMM_YearID =" & iYearID & " And MMM_CompID= " & iACID & " And MMM_Module='" & sModule & "'"
            If iCustID > 0 Then
                sSql = sSql & " and MMM_CUSTID=" & iCustID & " "
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and MMM_FunID=" & iFunctionID & " "
            End If
            If iSubFunID > 0 Then
                sSql = sSql & " and MMM_SEMID=" & iSubFunID & " "
            End If
            If iProcessID > 0 Then
                sSql = sSql & " and MMM_PMID=" & iProcessID & " "
            End If
            If iSubprocessID > 0 Then
                sSql = sSql & " and MMM_SPMID=" & iSubprocessID & " "
            End If

            sSql = sSql & "Order by MMM_SPMID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("MappingID") = dt.Rows(i)("MMM_ID")
                dr("CustID") = dt.Rows(i)("MMM_CUSTID")
                dr("FunctionID") = dt.Rows(i)("MMM_FunID")
                dr("SubFunctionID") = dt.Rows(i)("MMM_SEMID")
                dr("ProcessID") = dt.Rows(i)("MMM_PMID")
                dr("SubProcessID") = dt.Rows(i)("MMM_SPMID")
                dr("Function") = objDBL.SQLGetDescription(sAC, "Select Ent_EntityName from MST_ENTITY_MASTER Where Ent_ID=" & dt.Rows(i)("MMM_FunID") & " And Ent_CompID=" & iACID & " ")
                dr("SubFunction") = objDBL.SQLGetDescription(sAC, "Select SEM_Name From MST_SUBENTITY_MASTER Where SEM_ID=" & dt.Rows(i)("MMM_SEMID") & " And SEM_CompID=" & iACID & " ")
                dr("Process") = objDBL.SQLGetDescription(sAC, "Select PM_NAME From MST_PROCESS_MASTER Where PM_ID=" & dt.Rows(i)("MMM_PMID") & " AND PM_CompID=" & iACID & " ")
                dr("SubProcess") = objDBL.SQLGetDescription(sAC, "Select SPM_Name from MST_SUBPROCESS_MASTER  Where SPM_ID=" & dt.Rows(i)("MMM_SPMID") & " And SPM_CompID=" & iACID & "")
                dr("RisKID") = dt.Rows(i)("MMM_RISKID")
                dr("Risk") = dt.Rows(i)("MMM_Risk")
                dr("ControlID") = dt.Rows(i)("MMM_CONTROLID")
                dr("Control") = dt.Rows(i)("MMM_Control")
                dr("ChecksID") = dt.Rows(i)("MMM_ChecksID")
                dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                If dt.Rows(i)("MMM_DelFlag") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("MMM_DelFlag") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("MMM_DelFlag") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSPRISKCONTROLCHECKKey(ByVal AC As String, ByVal iACID As Integer, iID As Integer, ByVal sType As String) As Integer
        Dim sSql As String = ""
        Dim iKey As Integer
        Try
            If sType = "SUBPPROCESS" Then
                sSql = "Select SPM_IsKey from MST_SUBPROCESS_MASTER where SPM_SEM_ID = " & iID & " And SPM_CompID = " & iACID & ""
            ElseIf sType = "RISK" Then
                sSql = "Select MRL_IsKey from MST_RISK_Library where MRL_PKID = " & iID & " And MRL_CompID = " & iACID & ""
            ElseIf sType = "CONTROL" Then
                sSql = "Select MCL_IsKey from MST_Control_Library where MCL_PKID = " & iID & " And MCL_CompID = " & iACID & ""
            ElseIf sType = "CHECK" Then
                sSql = "Select Chk_IsKey from MST_Checks_Master where chk_ID = " & iID & " And Chk_CompID = " & iACID & ""
            End If
            iKey = objDBL.SQLExecuteScalarInt(AC, sSql)
            Return iKey
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAuditMapping(ByVal AC As String, ByVal iACID As Integer, ByVal objclsAduitMap As clsAuditMapping) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_SEMID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMSEMID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_PMID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMPMID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_SPMID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMSPMID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_SPMKey", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMM_SPMKey
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_RISKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMRISKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_Risk", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsAduitMap.sMMMRISK
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_RiskKey", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMRiskKey
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_CONTROLID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMCONTROLID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_Control", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsAduitMap.sMMMCONTROL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_ControlKey", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMControlKey
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_ChecksID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMCHECKSID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_CHECKS", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsAduitMap.sMMMCHECKS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_ChecksKey", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMChecksKey
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_InherentRiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMInherentRiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_InherentRisk", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsAduitMap.sMMMInherentRisk
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_Module", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objclsAduitMap.sMMMModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_UpdatedBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMUpdatedBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_IPaddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsAduitMap.sMMMIPaddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MMM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAduitMap.iMMMCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(AC, "spMST_MAPPING_MASTER", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ActivatedMapped(ByVal AC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal iSubFunctionID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, ByVal iChecksID As Integer, ByVal iMappingID As Integer, ByVal sModule As String, ByVal iUserID As Integer, ByVal sIPaddress As String, ByVal sStatus As String)
        Dim sSql As String
        Try
            sSql = "UPDATE MST_MAPPING_MASTER SET"
            If sStatus = "Created" Then
                sSql = sSql & " MMM_DelFlag='A',MMM_Status='A',MMM_ApprovedBy=" & iUserID & ",MMM_ApprovedOn=Getdate(),MMM_IPaddress='" & sIPaddress & "'"
            ElseIf sStatus = "DeActivated" Then
                sSql = sSql & " MMM_DelFlag='D',MMM_Status='AD',MMM_DeletedBy=" & iUserID & ",MMM_DeletedOn=Getdate(),MMM_IPaddress='" & sIPaddress & "'"
            ElseIf sStatus = "Activated" Then
                sSql = sSql & " MMM_DelFlag='A',MMM_Status='AR',MMM_RecallBy=" & iUserID & ",MMM_RecallOn=Getdate(),MMM_IPaddress='" & sIPaddress & "'"
            End If
            sSql = sSql & " WHERE MMM_CompID=" & iACID & " And MMM_Module='" & sModule & "' And MMM_CUSTID=" & iCustID & " And MMM_FunID = " & iFunctionID & ""
            sSql = sSql & " And MMM_ID=" & iMappingID & ""
            If iSubFunctionID > 0 Then
                sSql = sSql & " And MMM_SEMID=" & iSubFunctionID & ""
            End If
            If iProcessID > 0 Then
                sSql = sSql & " And MMM_PMID=" & iProcessID & ""
            End If
            If iSubProcessID > 0 Then
                sSql = sSql & " And MMM_SPMID=" & iSubProcessID & ""
            End If
            If iChecksID > 0 Then
                sSql = sSql & " And MMM_ChecksID=" & iChecksID & ""
            End If
            objDBL.SQLExecuteNonQuery(AC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
