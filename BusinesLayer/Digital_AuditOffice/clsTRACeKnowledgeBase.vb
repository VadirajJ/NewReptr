Imports DatabaseLayer
Public Class clsTRACeKnowledgeBase
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private TKB_ID As Integer
    Private TKB_Subject As String
    Private TKB_Content As String
    Private TKB_CrBy As Integer
    Private TKB_UpdatedBy As Integer
    Private TKB_IPAddress As String
    Private TKB_CompID As Integer
    Public Property iTKB_ID() As Integer
        Get
            Return (TKB_ID)
        End Get
        Set(ByVal Value As Integer)
            TKB_ID = Value
        End Set
    End Property
    Public Property sTKB_Subject() As String
        Get
            Return (TKB_Subject)
        End Get
        Set(ByVal Value As String)
            TKB_Subject = Value
        End Set
    End Property
    Public Property sTKB_Content() As String
        Get
            Return (TKB_Content)
        End Get
        Set(ByVal Value As String)
            TKB_Content = Value
        End Set
    End Property
    Public Property iTKB_CrBy() As Integer
        Get
            Return (TKB_CrBy)
        End Get
        Set(ByVal Value As Integer)
            TKB_CrBy = Value
        End Set
    End Property
    Public Property iTKB_UpdatedBy() As Integer
        Get
            Return (TKB_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            TKB_UpdatedBy = Value
        End Set
    End Property
    Public Property sTKB_IPAddress() As String
        Get
            Return (TKB_IPAddress)
        End Get
        Set(ByVal Value As String)
            TKB_IPAddress = Value
        End Set
    End Property
    Public Property iTKB_CompID() As Integer
        Get
            Return (TKB_CompID)
        End Get
        Set(ByVal Value As Integer)
            TKB_CompID = Value
        End Set
    End Property
    Public Function SaveTRACeKnowledgeBase(ByVal sAC As String, ByVal objclsTKB As clsTRACeKnowledgeBase) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(8) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTKB.iTKB_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_Subject", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsTKB.sTKB_Subject
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_Content", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsTKB.sTKB_Content
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTKB.iTKB_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTKB.iTKB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsTKB.sTKB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTKB.iTKB_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_Knowledge_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTRACeKnowledgeBase(ByVal sAC As String, ByVal iACID As Integer, ByVal iTKBID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from SAD_Knowledge_Master where TKB_ID=" & iTKBID & " and TKB_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTRACeKnowledgeBaseGridItems(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("lblTKBID")
            dtTab.Columns.Add("Subject")
            dtTab.Columns.Add("Content")
            dtTab.Columns.Add("Creator")
            dtTab.Columns.Add("Status")

            sSql = "Select USR_Fullname,TKB_ID,TKB_Subject,TKB_Content,TKB_CrBy,TKB_Status From SAD_Knowledge_Master TKM ,SAd_USERDETAILS UD where TKM.TKB_CrBy = UD.Usr_ID"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow()
                dr("SrNo") = i + 1
                dr("lblTKBID") = dt.Rows(i)("TKB_ID")
                dr("Subject") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("TKB_Subject"))
                dr("Content") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("TKB_Content"))
                dr("Creator") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_Fullname"))
                If IsDBNull(dt.Rows(i)("TKB_Status")) = False Then
                    If dt.Rows(i)("TKB_Status") = "A" Then
                        dr("Status") = "Activated"
                    ElseIf dt.Rows(i)("TKB_Status") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf dt.Rows(i)("TKB_Status") = "C" Then
                        dr("Status") = "Waiting for Approval"
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    'ApproveRiskDetails
    Public Sub ApproveTKBDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iRiskID As Integer, ByVal sFlag As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update SAD_Knowledge_Master set "
            If sFlag = "Created" Then
                sSql = sSql & "TKB_Status='A',TKB_ApprovedBy=" & iUserID & ",TKB_ApprovedOn=Getdate(),"
            ElseIf sFlag = "DeActivated" Then
                sSql = sSql & "TKB_Status='D',"
            ElseIf sFlag = "Activated" Then
                sSql = sSql & "TKB_Status='A',"
            End If
            sSql = sSql & " TKB_IPAddress='" & sIPAddress & "' where TKB_ID=" & iRiskID & " and TKB_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
