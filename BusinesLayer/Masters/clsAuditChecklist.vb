Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAuditChecklist
    Private objDBL As New DatabaseLayer.DBHelper
    Public iID As Integer
    Public iAuditTypeID As Integer
    Public sCode As String
    Public sHeading As String
    Public sCheckpoint As String
    Public sStatus As String
    Public sDelflag As String
    Public iCrBy As Integer
    Public iUpdatedBy As Integer
    Public sIpAddress As String
    Public iCompId As Integer
    Public Function LoadAuditTypeIsComplainceDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & " And CMM_Delflag='A' And CMS_KeyComponent=0 Order By CMM_Desc ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTypeIsComplainceDetailsInSA(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & " And CMM_Delflag='A' And CMS_KeyComponent=0 And EXISTS (SELECT 1 FROM AuditType_Checklist_Master Where ACM_AuditTypeID=CMM_ID) Order By CMM_Desc ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadScheduledAuditTypeDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustID As Integer, ByVal iFYID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_ID in (Select SA_AuditTypeID From StandardAudit_Schedule Where SA_CustID=" & iCustID & " And SA_YearID=" & iFYID & ") Order By CMM_Desc ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTypeChecklistMasterGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, iAuditTypeID As Integer, ByVal sSearchText As String) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AuditChecklistID")
            dt.Columns.Add("Heading")
            dt.Columns.Add("Checkpoint")
            dt.Columns.Add("Status")
            sSql = "Select * From AuditType_Checklist_Master Where ACM_AuditTypeID=" & iAuditTypeID & " And ACM_CompId=" & iAcID & " "
            If iStatus = 0 Then
                sSql = sSql & " And ACM_DELFLG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And ACM_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And ACM_DELFLG='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And ACM_Checkpoint like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By ACM_ID ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("AuditChecklistID") = ds.Tables(0).Rows(i)("ACM_ID")
                dr("Heading") = ds.Tables(0).Rows(i)("ACM_Heading")
                dr("Checkpoint") = ds.Tables(0).Rows(i)("ACM_Checkpoint")
                If IsDBNull(ds.Tables(0).Rows(i)("ACM_DELFLG")) = False Then
                    If ds.Tables(0).Rows(i)("ACM_DELFLG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("ACM_DELFLG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("ACM_DELFLG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAuditTypeCheckpointStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal iUserId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update AuditType_Checklist_Master Set ACM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " ACM_DELFLG='A',ACM_STATUS='A',ACM_APPROVEDBY=" & iUserId & ",ACM_APPROVEDON=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " ACM_DELFLG='D',ACM_STATUS='AD',ACM_DELETEDBY=" & iUserId & ",ACM_DELETEDON=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " ACM_DELFLG='A',ACM_STATUS='AR',ACM_RECALLBY=" & iUserId & ",ACM_RECALLON=GetDate()"
            End If
            sSql = sSql & " Where ACM_ID=" & iMasId & " And ACM_CompId=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveAuditTypeChecklistMasterDetails(ByVal sAC As String, ByVal objclsAuditChecklist As clsAuditChecklist) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAuditChecklist.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CODE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsAuditChecklist.sCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_AuditTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAuditChecklist.iAuditTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Heading", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsAuditChecklist.sHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Checkpoint", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsAuditChecklist.sCheckpoint
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsAuditChecklist.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsAuditChecklist.sDelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAuditChecklist.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAuditChecklist.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsAuditChecklist.sIpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsAuditChecklist.iCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAuditType_Checklist_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTypeChecklistMasterReportDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, iAuditTypeID As Integer) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescID")
            dt.Columns.Add("DescName")
            dt.Columns.Add("Description")
            dt.Columns.Add("Status")
            sSql = "Select * From AuditType_Checklist_Master Where ACM_CompId=" & iAcID & " And ACM_AuditTypeID=" & iAuditTypeID & " "
            If iStatus = 0 Then
                sSql = sSql & " And ACM_DELFLG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And ACM_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And ACM_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By ACM_ID ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescID") = ds.Tables(0).Rows(i)("ACM_ID")
                dr("DescName") = ds.Tables(0).Rows(i)("ACM_Heading")
                dr("Description") = ds.Tables(0).Rows(i)("ACM_Checkpoint")
                If IsDBNull(ds.Tables(0).Rows(i)("ACM_DELFLG")) = False Then
                    If ds.Tables(0).Rows(i)("ACM_DELFLG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("ACM_DELFLG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("ACM_DELFLG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditTypeName(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditTypeID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CMM_Desc from Content_Management_Master where CMM_ID=" & iAuditTypeID & " And CMM_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadHeading(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select distinct(ACM_Heading) From AuditType_Checklist_Master Where ACM_CompId=" & iCompID & " and ACM_Heading<>'' and ACM_Heading<>'NULL'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal sHeading As String, ByVal sOldHeading As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Update AuditType_Checklist_Master set ACM_Heading='" & sHeading & "' Where ACM_Heading='" & sOldHeading & "' And ACM_CompId=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadAllAuditTypeChecklistDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditTypeID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ACM_ID,ACM_Checkpoint From AuditType_Checklist_Master Where ACM_AuditTypeID=" & iAuditTypeID & " And ACM_CompId=" & iAcID & " And ACM_DELFLG in ('A','W') Order By ACM_ID ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditTypeChecklistDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCheckPointID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From AuditType_Checklist_Master Where ACM_ID=" & iCheckPointID & " and ACM_CompId=" & iAcID & " Order By ACM_ID ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAuditTypeChecklistDeleteorNot(ByVal sAc As String, ByVal iAcID As Integer, ByVal sDesc As Object, ByVal iCheckPointID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from AuditType_Checklist_Master where ACM_CompId=" & iAcID & " And ACM_Checkpoint='" & sDesc & "' "
            If iCheckPointID > 0 Then
                sSql = sSql & " And ACM_ID=" & iCheckPointID & " and ACM_DELFLG='D'"
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAuditTypeChecklistExistingDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditTypeID As Integer, ByVal sText As Object, ByVal sCoulmnName As String, ByVal iCheckPointID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from AuditType_Checklist_Master where ACM_AuditTypeID=" & iAuditTypeID & " And ACM_CompId=" & iAcID & " And " & sCoulmnName & "='" & sText & "'"
            If iCheckPointID > 0 Then
                sSql = sSql & " And ACM_ID <> " & iCheckPointID & ""
            End If
            CheckAuditTypeChecklistExistingDetails = objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTypeChecklist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditTypeID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ACM_ID,ACM_Checkpoint From AuditType_Checklist_Master Where ACM_CompId=" & iAcID & " And ACM_AuditTypeID=" & iAuditTypeID & " And ACM_DELFLG='A' Order By ACM_Checkpoint ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class

