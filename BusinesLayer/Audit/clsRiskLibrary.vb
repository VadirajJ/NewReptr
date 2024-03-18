Imports DatabaseLayer

Public Class clsRiskLibrary
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private iMRL_PKID As Integer
    Private sMRL_RiskName As String
    Private sMRL_RiskDesc As String
    Private sMRL_Code As String
    Private iMRL_RiskTypeID As Integer
    Private iMRL_IsKey As Integer
    Private sMRL_DelFlag As String
    Private sMRL_Status As String
    Private iMRL_CrBy As Integer
    Private iMRL_UpdatedBy As Integer
    Private sMRL_IPAddress As String
    Private iMRL_CompID As Integer
    Private iMRL_InherentRiskID As Integer
    Private sMRL_Module As String
    Public Property iMRLPKID() As Integer
        Get
            Return (iMRL_PKID)
        End Get
        Set(ByVal Value As Integer)
            iMRL_PKID = Value
        End Set
    End Property
    Public Property sMRLModule() As String
        Get
            Return (sMRL_Module)
        End Get
        Set(ByVal Value As String)
            sMRL_Module = Value
        End Set
    End Property
    Public Property sMRLRiskName() As String
        Get
            Return (sMRL_RiskName)
        End Get
        Set(ByVal Value As String)
            sMRL_RiskName = Value
        End Set
    End Property
    Public Property iMRLInherentRiskID() As String
        Get
            Return (iMRL_InherentRiskID)
        End Get
        Set(ByVal Value As String)
            iMRL_InherentRiskID = Value
        End Set
    End Property
    Public Property sMRLRiskDesc() As String
        Get
            Return (sMRL_RiskDesc)
        End Get
        Set(ByVal Value As String)
            sMRL_RiskDesc = Value
        End Set
    End Property
    Public Property sMRLCode() As String
        Get
            Return (sMRL_Code)
        End Get
        Set(ByVal Value As String)
            sMRL_Code = Value
        End Set
    End Property
    Public Property iMRLRiskTypeID() As Integer
        Get
            Return (iMRL_RiskTypeID)
        End Get
        Set(ByVal Value As Integer)
            iMRL_RiskTypeID = Value
        End Set
    End Property
    Public Property iMRLIsKey() As Integer
        Get
            Return (iMRL_IsKey)
        End Get
        Set(ByVal Value As Integer)
            iMRL_IsKey = Value
        End Set
    End Property
    Public Property sMRLDelFlag() As String
        Get
            Return (sMRL_DelFlag)
        End Get
        Set(ByVal Value As String)
            sMRL_DelFlag = Value
        End Set
    End Property
    Public Property sMRLStatus() As String
        Get
            Return (sMRL_Status)
        End Get
        Set(ByVal Value As String)
            sMRL_Status = Value
        End Set
    End Property
    Public Property iMRLCrBy() As Integer
        Get
            Return (iMRL_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iMRL_CrBy = Value
        End Set
    End Property
    Public Property iMRLUpdatedBy() As Integer
        Get
            Return (iMRL_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iMRL_UpdatedBy = Value
        End Set
    End Property
    Public Property sMRLIPAddress() As String
        Get
            Return (sMRL_IPAddress)
        End Get
        Set(ByVal Value As String)
            sMRL_IPAddress = Value
        End Set
    End Property
    Public Property iMRLCompID() As Integer
        Get
            Return (iMRL_CompID)
        End Get
        Set(ByVal Value As Integer)
            iMRL_CompID = Value
        End Set
    End Property
    'BindRisks
    Public Function LoadAllExistingRisks(ByVal sAC As String, ByVal iACID As Integer, ByVal sSearchRisk As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MRL_PKID,MRL_RiskName from MST_RISK_Library where MRL_CompID=" & iACID & ""
            If sSearchRisk <> "" Then
                sSql = sSql & " And MRL_RiskName Like '" & sSearchRisk & "%'"
            End If
            sSql = sSql & " order by MRL_RiskName Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Get RiskName
    Public Function GetRiskName(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As String) As String
        Dim sSql As String
        Try
            sSql = "Select MRL_RiskName from MST_RISK_Library where MRL_CompID=" & iACID & " And MRL_PKID=" & iRiskID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskTypeName(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As String) As String
        Dim sSql As String
        Try
            sSql = "Select RAM_Name From Risk_GeneralMaster Where RAM_PKID=(Select MRL_RiskTypeID from MST_RISK_Library where MRL_PKID=" & iRiskID & " and MRL_CompID='" & iACID & "')"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInherentRiskID(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As String, ByVal sModule As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select MRL_InherentRiskID from MST_Risk_Library where MRL_PKID=" & iRiskID & " And MRL_Module='" & sModule & "' And MRL_CompID='" & iACID & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInherentRiskNameFromRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As String) As String
        Dim sSql As String
        Dim iInherentRiskID As Integer
        Try
            iInherentRiskID = GetInherentRiskID(sAC, iACID, iRiskID, "R")
            sSql = "Select MIM_Name From MST_InherentRisk_Master Where MIM_ID = " & iInherentRiskID & " And MIM_CompID='" & iACID & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BindRiskType
    Public Function LoadRiskMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name from Risk_GeneralMaster where RAM_CompID=" & iACID & " And RAM_YearID=" & iYearID & " And RAM_Category='" & sType & "' And RAM_DelFlag='A' order by RAM_Name Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BindSelectedRiskDetails
    Public Function LoadSelectedRiskDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MRL_RiskName,MRL_Code,MRL_RiskDesc,MRL_IsKey,MRL_RiskTypeID,MRL_DelFlag,MRL_InherentRiskID from MST_RISK_Library where MRL_PKID = " & iRiskID & " and MRL_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'CheckRiskNameExist
    Public Function CheckRiskNameExist(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal sRiskName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select MRL_PKID from MST_RISK_Library where MRL_RiskName='" & sRiskName & "' And MRL_CompID =" & iACID & ""
            If iPKID > 0 Then
                sSql = sSql & " And MRL_PKID <>" & iPKID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'ApproveRiskDetails
    Public Sub ApproveRiskDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iRiskID As Integer, ByVal sFlag As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update MST_RISK_Library set "
            If sFlag = "W" Then
                sSql = sSql & "MRL_Status='A',MRL_DelFlag='A',MRL_ApprovedBy=" & iUserID & ",MRL_ApprovedOn=Getdate(),"
            ElseIf sFlag = "D" Then
                sSql = sSql & "MRL_Status='AD',MRL_DelFlag='D',MRL_DeletedBy=" & iUserID & ",MRL_DeletedOn=Getdate(),"
            ElseIf sFlag = "A" Then
                sSql = sSql & "MRL_Status='AR',MRL_DelFlag='A',MRL_RecallBy=" & iUserID & ",MRL_RecallOn=Getdate(),"
            End If
            sSql = sSql & " MRL_IPAddress='" & sIPAddress & "' where MRL_PKID=" & iRiskID & " and MRL_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'LoadGrid
    Public Function LoadRiskGridItems(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("RiskID")
            dtTab.Columns.Add("RiskType")
            dtTab.Columns.Add("RiskName")
            dtTab.Columns.Add("InherentRisk")
            dtTab.Columns.Add("Status")
            sSql = "Select MRL_PKID,MRL_RiskName,MRL_InherentRiskID,MRL_DelFlag,RAM_Name,MIM_Name From MST_RISK_Library "
            sSql = sSql & " left outer join Risk_GeneralMaster On RAM_PKID=MRL_RiskTypeID"
            sSql = sSql & " left outer join MST_InherentRisk_Master On MIM_ID=MRL_InherentRiskID"
            sSql = sSql & " Where MRL_CompID =" & iACID & " Order By MRL_RiskName"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow()
                dr("RiskID") = dt.Rows(i)("MRL_PKID")
                dr("SrNo") = i + 1
                dr("RiskType") = dt.Rows(i)("RAM_Name")
                dr("RiskName") = dt.Rows(i)("MRL_RiskName")
                dr("InherentRisk") = dt.Rows(i)("MRL_InherentRiskID")
                If IsDBNull(dt.Rows(i)("MRL_InherentRiskID")) = False Then
                    If dt.Rows(i)("MRL_InherentRiskID") = "1" Then
                        dr("InherentRisk") = "High"
                    ElseIf dt.Rows(i)("MRL_InherentRiskID") = "2" Then
                        dr("InherentRisk") = "Low"
                    ElseIf dt.Rows(i)("MRL_InherentRiskID") = "3" Then
                        dr("InherentRisk") = "Medium"
                    ElseIf dt.Rows(i)("MRL_InherentRiskID") = "4" Then
                        dr("InherentRisk") = "Very High"
                    ElseIf dt.Rows(i)("MRL_InherentRiskID") = "0" Then
                        dr("InherentRisk") = ""
                    End If
                End If
                If IsDBNull(dt.Rows(i)("MRL_DelFlag")) = False Then
                    If dt.Rows(i)("MRL_DelFlag") = "A" Then
                        dr("Status") = "Activated"
                    ElseIf dt.Rows(i)("MRL_DelFlag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf dt.Rows(i)("MRL_DelFlag") = "W" Then
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

    'SaveRiskLibrary
    Public Function SaveRiskLibrary(ByVal sAC As String, ByVal objclsRiskLibrary As clsRiskLibrary) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskLibrary.iMRLPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_RiskName", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsRiskLibrary.sMRLRiskName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_RiskDesc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsRiskLibrary.sMRLRiskDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_Code", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsRiskLibrary.sMRLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_IsKey", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskLibrary.iMRLIsKey
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_RiskTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskLibrary.iMRLRiskTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_InherentRiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskLibrary.iMRL_InherentRiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_Module", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsRiskLibrary.sMRLModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskLibrary.iMRLCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskLibrary.iMRLUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsRiskLibrary.sMRLIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MRL_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskLibrary.iMRLCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_RISK_Library", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadRiskLibraryReport(ByVal sAC As String, ByVal iACID As Integer) As DataTable ', ByVal iRiskType As Integer
        Dim sSql As String
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer, iKey As Integer
        Try
            dtTab.Columns.Add("SLNo")
            dtTab.Columns.Add("Risk Type")
            dtTab.Columns.Add("Risk Name")
            dtTab.Columns.Add("Code")
            dtTab.Columns.Add("Inherent Risk")
            dtTab.Columns.Add("Key")
            dtTab.Columns.Add("Description")
            dtTab.Columns.Add("Status")
            dtTab.Columns.Add("Created by")
            dtTab.Columns.Add("Created On")
            dtTab.Columns.Add("Last updated by")
            dtTab.Columns.Add("Last updated on")
            dtTab.Columns.Add("Approved by")
            dtTab.Columns.Add("Approved on")
            sSql = "Select * From MST_RISK_Library Where MRL_CompID =" & iACID & " Order By MRL_RiskName" ' MRL_RiskTypeID = " & iRiskType & " And
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow()
                dr("SLNo") = i + 1
                dr("Risk Type") = objDBL.SQLGetDescription(sAC, "Select RAM_Name from Risk_GeneralMaster where RAM_PKID=" & dt.Rows(i)("MRL_RiskTypeID") & " And RAM_CompID=" & iACID & "") 'RAM_PKID=" & iRiskType & " And
                dr("Risk Name") = dt.Rows(i)("MRL_RiskName")
                dr("Code") = dt.Rows(i)("MRL_Code")
                dr("Inherent Risk") = objDBL.SQLGetDescription(sAC, "Select MIM_Name from MST_InherentRisk_Master Where MIM_ID=" & dt.Rows(i)("MRL_InherentRiskID") & " And MIM_CompID=" & iACID & "")
                dr("Description") = dt.Rows(i)("MRL_RiskDesc")
                iKey = dt.Rows(i)("MRL_IsKey")
                If iKey = 1 Then
                    dr("Key") = "Key"
                Else
                    dr("Key") = "Non-Key"
                End If
                If IsDBNull(dt.Rows(i)("MRL_CrBy")) = False Then
                    dr("Created by") = objclsGeneralFunctions.GetUserIDFromFullName(sAC, iACID, dt.Rows(i)("MRL_CrBy"))
                End If
                If IsDBNull(dt.Rows(i)("MRL_CrOn")) = False Then
                    dr("Created On") = dt.Rows(i)("MRL_CrOn")
                End If
                If IsDBNull(dt.Rows(i)("MRL_UpdatedBy")) = False Then
                    dr("Last updated by") = objclsGeneralFunctions.GetUserIDFromFullName(sAC, iACID, dt.Rows(i)("MRL_UpdatedBy"))
                End If
                If IsDBNull(dt.Rows(i)("MRL_UpdatedOn")) = False Then
                    dr("Last updated On") = dt.Rows(i)("MRL_UpdatedOn")
                End If
                If IsDBNull(dt.Rows(i)("MRL_ApprovedBy")) = False Then
                    dr("Approved by") = objclsGeneralFunctions.GetUserIDFromFullName(sAC, iACID, dt.Rows(i)("MRL_ApprovedBy"))
                End If
                If IsDBNull(dt.Rows(i)("MRL_ApprovedOn")) = False Then
                    dr("Approved On") = dt.Rows(i)("MRL_ApprovedOn")
                End If

                If IsDBNull(dt.Rows(i)("MRL_DelFlag")) = False Then
                    If dt.Rows(i)("MRL_DelFlag") = "A" Then
                        dr("Status") = "Activated"
                    ElseIf dt.Rows(i)("MRL_DelFlag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf dt.Rows(i)("MRL_DelFlag") = "W" Then
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
End Class
