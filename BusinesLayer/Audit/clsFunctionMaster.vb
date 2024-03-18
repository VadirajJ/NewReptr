Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsFunctionMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Dim iENT_ID As Integer
    Dim sENT_CODE As String
    Dim sENT_ENTITYNAME As String
    Dim sENT_ENTITYDesc As String
    Dim iENT_CRBY As Integer
    Dim iENT_UPDATEDBY As Integer
    Dim sENT_IPAddress As String
    Dim iENT_COMPID As Integer
    Dim iEnt_FunOwnerID As Integer
    Dim iEnt_FunManagerID As Integer
    Dim iEnt_FunSPOCID As Integer

    'Sub Functon
    Dim iSEM_ID As Integer
    Dim sSEM_CODE As String
    Dim sSEM_NAME As String
    Dim iSEM_ORDER As Integer
    Dim iSEM_CRBY As Integer
    Dim iSEM_UPDATEDBY As Integer
    Dim sSEM_DESC As String
    Dim iSEM_COMPID As Integer
    Dim sSEM_IPAddress As String
    Public Property iENTID() As Integer
        Get
            Return (iENT_ID)
        End Get
        Set(ByVal Value As Integer)
            iENT_ID = Value
        End Set
    End Property
    Public Property sENTCODE() As String
        Get
            Return (sENT_CODE)
        End Get
        Set(ByVal Value As String)
            sENT_CODE = Value
        End Set
    End Property
    Public Property sENTNAME() As String
        Get
            Return (sENT_ENTITYNAME)
        End Get
        Set(ByVal Value As String)
            sENT_ENTITYNAME = Value
        End Set
    End Property
    Public Property sENTDesc() As String
        Get
            Return (sENT_ENTITYDesc)
        End Get
        Set(ByVal Value As String)
            sENT_ENTITYDesc = Value
        End Set
    End Property
    Public Property iENTCRBY() As Integer
        Get
            Return (iENT_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iENT_CRBY = Value
        End Set
    End Property
    Public Property iENTUPDATEDBY() As Integer
        Get
            Return (iENT_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            iENT_UPDATEDBY = Value
        End Set
    End Property
    Public Property sENTIPAddress() As String
        Get
            Return (sENT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sENT_IPAddress = Value
        End Set
    End Property
    Public Property iENTCOMPID() As Integer
        Get
            Return (iENT_COMPID)
        End Get
        Set(ByVal Value As Integer)
            iENT_COMPID = Value
        End Set
    End Property
    Public Property iEntFunOwnerID() As Integer
        Get
            Return (iEnt_FunOwnerID)
        End Get
        Set(ByVal Value As Integer)
            iEnt_FunOwnerID = Value
        End Set
    End Property
    Public Property iEntFunManagerID() As Integer
        Get
            Return (iEnt_FunManagerID)
        End Get
        Set(ByVal Value As Integer)
            iEnt_FunManagerID = Value
        End Set
    End Property
    Public Property iEntFunSPOCID() As Integer
        Get
            Return (iEnt_FunSPOCID)
        End Get
        Set(ByVal Value As Integer)
            iEnt_FunSPOCID = Value
        End Set
    End Property

    'Sub Function
    Public Property iSEMID() As Integer
        Get
            Return (iSEM_ID)
        End Get
        Set(ByVal value As Integer)
            iSEM_ID = value
        End Set
    End Property
    Public Property sSEMCODE() As String
        Get
            Return (sSEM_CODE)
        End Get
        Set(ByVal value As String)
            sSEM_CODE = value
        End Set
    End Property
    Public Property sSEMNAME() As String
        Get
            Return (sSEM_NAME)
        End Get
        Set(ByVal value As String)
            sSEM_NAME = value
        End Set
    End Property
    Public Property sSEMDecs() As String
        Get
            Return (sSEM_DESC)
        End Get
        Set(ByVal value As String)
            sSEM_DESC = value
        End Set
    End Property
    Public Property iSEMCRBY() As Integer
        Get
            Return (iSEM_CRBY)
        End Get
        Set(ByVal value As Integer)
            iSEM_CRBY = value
        End Set
    End Property
    Public Property iSEMUPDATEDBY() As Integer
        Get
            Return (iSEM_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            iSEM_UPDATEDBY = Value
        End Set
    End Property
    Public Property iSEMCOMPID() As Integer
        Get
            Return (iSEM_COMPID)
        End Get
        Set(ByVal Value As Integer)
            iSEM_COMPID = Value
        End Set
    End Property
    Public Property sSEMIPAddress() As String
        Get
            Return (sSEM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSEM_IPAddress = Value
        End Set
    End Property
    Public Function LoadAllFunctions(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("EntID")
            dtTab.Columns.Add("FunctionCode")
            dtTab.Columns.Add("FunctionName")
            dtTab.Columns.Add("FunctionOwner")
            dtTab.Columns.Add("FunctionManager")
            dtTab.Columns.Add("FunctionSPOC")
            dtTab.Columns.Add("Status")

            sSql = "Select ENT_ID,ENT_CODE,ENT_ENTITYNAME,ENT_DELFLG,ENT_FunOwnerID,Ent_FunManagerID,Ent_FunSPOCID From MST_ENTITY_MASTER"
            sSql = sSql & " Where ENT_Branch='F' And Ent_compid=" & iACID & " Order By ENT_ENTITYNAME Asc"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("EntID") = dt.Rows(i)("ENT_ID")
                dRow("FunctionCode") = dt.Rows(i)("ENT_CODE")
                dRow("FunctionName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ENT_ENTITYNAME"))
                If IsDBNull(dt.Rows(i)("ENT_FunOwnerID")) = False Then
                    dRow("FunctionOwner") = objDBL.SQLExecuteScalar(sAC, "Select USR_Fullname From SAd_USERDETAILS Where Usr_ID=" & dt.Rows(i)("ENT_FunOwnerID") & " And Usr_Compid=" & iACID & "")
                End If
                If IsDBNull(dt.Rows(i)("Ent_FunManagerID")) = False Then
                    dRow("FunctionManager") = objDBL.SQLExecuteScalar(sAC, "Select USR_Fullname From SAd_USERDETAILS Where Usr_ID=" & dt.Rows(i)("Ent_FunManagerID") & " And Usr_Compid=" & iACID & "")
                End If
                If IsDBNull(dt.Rows(i)("Ent_FunSPOCID")) = False Then
                    dRow("FunctionSPOC") = objDBL.SQLExecuteScalar(sAC, "Select USR_Fullname From SAd_USERDETAILS Where Usr_ID=" & dt.Rows(i)("Ent_FunSPOCID") & " And Usr_Compid=" & iACID & "")
                End If
                If dt.Rows(i)("ENT_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("ENT_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("ENT_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveFunctionStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iFunctionID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update mst_entity_master set"
            If sType = "Created" Then
                sSql = sSql & " ENT_Delflg='A',ENT_Status='A',ENT_ApprovedBy=" & iSessionUsrID & ", ENT_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " ENT_Delflg='D',ENT_Status='AD',ENT_DeletedBy=" & iSessionUsrID & ", ENT_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " ENT_Delflg='A',ENT_Status='AR',ENT_RecallBy=" & iSessionUsrID & ", ENT_RecallOn=Getdate(),"
            End If
            sSql = sSql & "ENT_IPAddress='" & sIPAddress & "' Where ENT_CompId=" & iACID & " And ENT_ID=" & iFunctionID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadFunctionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iEntID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from MST_Entity_Master where ENT_ID = " & iEntID & " and ENT_Compid=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSelectedUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer)
        Dim dtTab As New DataTable
        Dim sSql, sSql1 As String
        Dim dt As New DataTable
        Dim i As Integer
        Dim sStr As String
        Dim sUsers As String = ""
        Try
            sSql = "Select  MEUM_UsrID from MST_Entity_UsrMap where MEUM_EntityID=" & iFunctionID & " and MEUM_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sStr = dt.Rows(i).Item(0).ToString
                    sUsers = sUsers & sStr
                    If sUsers.EndsWith(",") Then
                        sUsers = sUsers.Remove(Len(sUsers) - 1, 1)
                    End If
                Next
                If sUsers.StartsWith(",") Then
                    sUsers = sUsers.Remove(0, 1)
                End If
            End If

            If sUsers <> "" Then
                sSql1 = "Select usr_Id,usr_FullName from Sad_userdetails where usr_Id in (" & sUsers & ")"
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSql1).Tables(0)
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFunctionDetails(ByVal sAC As String, ByVal objFunction As clsFunctionMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFunction.iENTID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_CODE", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objFunction.sENTCODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_ENTITYNAME", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objFunction.sENTNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objFunction.sENTDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFunction.iENTCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFunction.iENTUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFunction.sENTIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ENT_COMPID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFunction.iENTCOMPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Ent_FunOwnerID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFunction.iEntFunOwnerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Ent_FunManagerID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFunction.iEntFunManagerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Ent_FunSPOCID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFunction.iEntFunSPOCID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_Entity_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckFunctionNameExist(ByVal sAC As String, ByVal iACID As Integer, ByVal iID As Integer, ByVal sFunctionName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select ENT_ID from MST_ENTITY_MASTER where ENT_ENTITYNAME='" & sFunctionName & "' And ENT_compid =" & iACID & ""
            If iID > 0 Then
                sSql = sSql & " And ENT_ID <>" & iID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveFunctionUsrMap(ByVal sAC As String, ByVal iEntityID As Integer, ByVal sUserID As String, ByVal iACID As Integer)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            objDBL.SQLExecuteNonQuery(sAC, "Delete From MST_Entity_UsrMap Where MEUM_EntityID=" & iEntityID & " And MEUM_CompID=" & iACID & "")

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MEUM_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MEUM_EntityID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = iEntityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MEUM_UsrID", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MEUM_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "spMST_Entity_UsrMap", ObjParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadExistingSubFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer) As DataTable
        Dim sSql As String, sFunctionName As String = "", sFunction As String = ""
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SubEntID")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("SubFunctionCode")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Status")
            sSql = "Select SEM_ID,SEM_Ent_ID,SEM_CODE,SEM_NAME,SEM_Desc,SEM_DELFLG From MST_SUBENTITY_MASTER EM where SEM_compid=" & iACID & ""
            If iSubFunID > 0 Then
                sSql = sSql & " And SEM_ENT_ID=" & iSubFunID & ""
            End If
            sSql = sSql & " Order By SEM_NAME Asc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("SubEntID") = dt.Rows(i)("SEM_ID")
                dRow("Function") = objDBL.SQLGetDescription(sAC, "Select Ent_EntityName from MST_ENTITY_MASTER Where Ent_ID=" & dt.Rows(i)("SEM_Ent_ID") & " And Ent_CompID=" & iACID & " ")
                dRow("SubFunctionCode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_CODE"))
                dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_NAME"))
                If dt.Rows(i)("SEM_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("SEM_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("SEM_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubFunctionNameExist(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer, ByVal sSubFunctionName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from MST_SUBENTITY_MASTER where SEM_NAME='" & sSubFunctionName & "' And SEM_ENT_ID=" & iFunID & " and SEM_COMPID=" & iACID & ""
            If iSubFunID > 0 Then
                sSql = sSql & " And SEM_ID <> " & iSubFunID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubFunctionDetails(ByVal sAC As String, ByVal objSubFunction As clsFunctionMaster, ByVal iFunctionId As Integer) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iSFParamCount As Integer
        Dim Arr(1) As String
        Try
            iSFParamCount = 0
            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_ID", OleDb.OleDbType.Integer)
            ObjSFParam(iSFParamCount).Value = objSubFunction.iSEMID
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_ENT_ID", OleDb.OleDbType.Integer)
            ObjSFParam(iSFParamCount).Value = iFunctionId
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("SEM_CODE", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iSFParamCount).Value = objSubFunction.sSEMCODE
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_NAME", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iSFParamCount).Value = objSubFunction.sSEMNAME
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_CRBY", OleDb.OleDbType.Integer)
            ObjSFParam(iSFParamCount).Value = objSubFunction.iSEMCRBY
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjSFParam(iSFParamCount).Value = objSubFunction.iSEMUPDATEDBY
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_DESC", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iSFParamCount).Value = objSubFunction.sSEMDecs
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_COMPID", OleDb.OleDbType.Integer)
            ObjSFParam(iSFParamCount).Value = objSubFunction.iSEMCOMPID
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@SEM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iSFParamCount).Value = objSubFunction.sSEMIPAddress
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Input
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Output
            iSFParamCount += 1

            ObjSFParam(iSFParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iSFParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_SUBENTITY_MASTER", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveSubFunctionsStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iSubFunctionID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update MST_SUBENTITY_MASTER set"
            If sType = "Created" Then
                sSql = sSql & " SEM_Delflg='A',SEM_Status='A',SEM_ApprovedBy=" & iSessionUsrID & ", SEM_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " SEM_Delflg='D',SEM_Status='AD',SEM_DeletedBy=" & iSessionUsrID & ", SEM_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " SEM_Delflg='A',SEM_Status='AR',SEM_RecallBy=" & iSessionUsrID & ", SEM_RecallOn=Getdate(),"
            End If
            sSql = sSql & "SEM_IPAddress='" & sIPAddress & "' Where SEM_CompId=" & iACID & " And SEM_ID=" & iSubFunctionID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Load Function
    Public Function LoadAllFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And Ent_DelFlg='A'"
            If sSearch <> "" Then
                sSql = sSql & " And (ENT_ENTITYName Like '" & sSearch & "%')"
            End If
            sSql = sSql & " Order by Ent_Entityname"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load Sub Function
    Public Function LoadAllSubFunctions(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer, ByVal sSearch As String, ByVal sCheckStatus As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SEM_ID,SEM_NAME from MST_SUBENTITY_MASTER where SEM_COMPID=" & iACID & ""
            If sCheckStatus = "YES" Then
                sSql = sSql & " And SEM_DelFlg='A'"
            End If
            If iFunID > 0 Then
                sSql = sSql & " And SEM_ENT_ID = " & iFunID & ""
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (SEM_NAME Like '" & sSearch & "%')"
            End If
            sSql = sSql & "order by SEM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Sub Function Details
    Public Function LoadSubFunctionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from MST_SUBENTITY_MASTER where  SEM_COMPID=" & iACID & ""
            If iSubFunID > 0 Then
                sSql = sSql & " And SEM_ID=" & iSubFunID & " "
            End If
            If iFunID > 0 Then
                sSql = sSql & " And SEM_ENT_ID=" & iFunID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
