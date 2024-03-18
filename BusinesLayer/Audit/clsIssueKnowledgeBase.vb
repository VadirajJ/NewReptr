Imports DatabaseLayer
Public Class clsIssueKnowledgeBase
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private IKB_ID As Integer
    Private IKB_IssueHeading As String
    Private IKB_IssueDetails As String
    Private IKB_IssueRatingID As Integer
    Private IKB_CrBy As Integer
    Private IKB_UpdatedBy As Integer
    Private IKB_IPAddress As String
    Private IKB_CompID As Integer
    Public Property iIKB_ID() As Integer
        Get
            Return (IKB_ID)
        End Get
        Set(ByVal Value As Integer)
            IKB_ID = Value
        End Set
    End Property
    Public Property sIKB_IssueHeading() As String
        Get
            Return (IKB_IssueHeading)
        End Get
        Set(ByVal Value As String)
            IKB_IssueHeading = Value
        End Set
    End Property
    Public Property sIKB_IssueDetails() As String
        Get
            Return (IKB_IssueDetails)
        End Get
        Set(ByVal Value As String)
            IKB_IssueDetails = Value
        End Set
    End Property
    Public Property iIKB_IssueRatingID() As Integer
        Get
            Return (IKB_IssueRatingID)
        End Get
        Set(ByVal Value As Integer)
            IKB_IssueRatingID = Value
        End Set
    End Property
    Public Property iIKB_CrBy() As Integer
        Get
            Return (IKB_CrBy)
        End Get
        Set(ByVal Value As Integer)
            IKB_CrBy = Value
        End Set
    End Property
    Public Property iIKB_UpdatedBy() As Integer
        Get
            Return (IKB_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            IKB_UpdatedBy = Value
        End Set
    End Property
    Public Property sIKB_IPAddress() As String
        Get
            Return (IKB_IPAddress)
        End Get
        Set(ByVal Value As String)
            IKB_IPAddress = Value
        End Set
    End Property
    Public Property iIKB_CompID() As Integer
        Get
            Return (IKB_CompID)
        End Get
        Set(ByVal Value As Integer)
            IKB_CompID = Value
        End Set
    End Property
    Public Function SaveIssueKnowledgeBase(ByVal sAC As String, ByVal objclsIKB As clsIssueKnowledgeBase) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TKB_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsIKB.iIKB_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IKB_IssueHeading", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsIKB.sIKB_IssueHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IKB_IssueDetails", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsIKB.sIKB_IssueDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IKB_IssueRatingID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsIKB.iIKB_IssueRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IKB_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsIKB.iIKB_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IKB_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsIKB.iIKB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IKB_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsIKB.sIKB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IKB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsIKB.iIKB_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_IssueKnowledgeBase_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueKnowledgeBase(ByVal sAC As String, ByVal iACID As Integer, ByVal iIKBID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from SAD_IssueKnowledgeBase_Master where IKB_ID=" & iIKBID & " and IKB_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueKnowledgeBaseGridItems(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("IKBID")
            dtTab.Columns.Add("IssueHeading")
            dtTab.Columns.Add("IssueDetails")
            dtTab.Columns.Add("IssueRating")
            dtTab.Columns.Add("Status")

            sSql = "Select MIM_Name,IKB_ID,IKB_IssueHeading,IKB_IssueDetails,IKB_DelFlag From SAD_IssueKnowledgeBase_Master IKM,MST_InherentRisk_Master IM"
            sSql = sSql & " Where IKM.IKB_IssueRatingID=IM.MIM_ID"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow()
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(0)("IKB_ID")) = False Then
                    dr("IKBID") = dt.Rows(i)("IKB_ID")
                End If
                If IsDBNull(dt.Rows(0)("IKB_IssueHeading")) = False Then
                    dr("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("IKB_IssueHeading"))
                End If
                If IsDBNull(dt.Rows(0)("IKB_IssueDetails")) = False Then
                    dr("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("IKB_IssueDetails"))
                End If
                If IsDBNull(dt.Rows(0)("MIM_Name")) = False Then
                    dr("IssueRating") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MIM_Name"))
                End If
                If IsDBNull(dt.Rows(i)("IKB_DelFlag")) = False Then
                    If dt.Rows(i)("IKB_DelFlag") = "A" Then
                        dr("Status") = "Activated"
                    ElseIf dt.Rows(i)("IKB_DelFlag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf dt.Rows(i)("IKB_DelFlag") = "W" Then
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
    'ApproveIssueKnowledgeBaseDetails
    Public Sub ApproveIKBDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal sFlag As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update SAD_IssueKnowledgeBase_Master set "
            If sFlag = "Created" Then
                sSql = sSql & "IKB_DelFlag='A',IKB_ApprovedBy=" & iUserID & ",IKB_ApprovedOn=Getdate(),"
            ElseIf sFlag = "DeActivated" Then
                sSql = sSql & "IKB_DelFlag='D',"
            ElseIf sFlag = "Activated" Then
                sSql = sSql & "IKB_DelFlag='A',"
            End If
            sSql = sSql & " IKB_IPAddress='" & sIPAddress & "' where IKB_ID=" & iID & " and IKB_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function ChecIssueHeadingExist(ByVal sAC As String, ByVal iACID As Integer, ByVal iID As Integer, ByVal sIssueHeading As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select IKB_ID from SAD_IssueKnowledgeBase_Master where IKB_IssueHeading='" & sIssueHeading & "' And IKB_CompID =" & iACID & ""
            If iID > 0 Then
                sSql = sSql & " And IKB_ID <>" & iID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
