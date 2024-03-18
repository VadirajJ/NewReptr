Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.Web
Imports System.ComponentModel

Public Class clsGeneralCMaster
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Public CVR_Point As String
    Public CVR_AuditId As Integer
    Public CVR_Name As String
    Public CVR_Desc As String

    Public CRAT_AuditID As Integer
    Public CRAT_StartValue As String
    Public CRAT_EndValue As String
    Public CRAT_Name As String
    Public CRAT_Desc As String
    Public CRAT_Color As String

    Public iID As Integer
    Public iYearID As Integer
    Public sFlag As String
    Public sStatus As String
    Public iCrBy As Integer
    Public iUpdatedBy As Integer
    Public sIPAddress As String
    Public iCompID As Integer

    Public Property sAuditpoint() As String
        Get
            Return (CVR_Point)
        End Get
        Set(ByVal Value As String)
            CVR_Point = Value
        End Set
    End Property
    Public Property iAuditID() As Integer
        Get
            Return (CVR_AuditId)
        End Get
        Set(ByVal Value As Integer)
            CVR_AuditId = Value
        End Set
    End Property
    Public Property sName() As String
        Get
            Return (CVR_name)
        End Get
        Set(ByVal Value As String)
            CVR_name = Value
        End Set
    End Property
    Public Property sDesc() As String
        Get
            Return (CVR_Desc)
        End Get
        Set(ByVal Value As String)
            CVR_Desc = Value
        End Set
    End Property

    Public Property iCRAuditID() As Integer
        Get
            Return (CRAT_AuditID)
        End Get
        Set(ByVal Value As Integer)
            CRAT_AuditID = Value
        End Set
    End Property
    Public Property iCRAuditpoint() As Integer
        Get
            Return (CRAT_AuditID)
        End Get
        Set(ByVal Value As Integer)
            CRAT_AuditID = Value
        End Set
    End Property
    Public Property dStartValue() As Double
        Get
            Return (CRAT_StartValue)
        End Get
        Set(ByVal Value As Double)
            CRAT_StartValue = Value
        End Set
    End Property
    Public Property dEndValue() As Double
        Get
            Return (CRAT_EndValue)
        End Get
        Set(ByVal Value As Double)
            CRAT_EndValue = Value
        End Set
    End Property
    Public Property sCRName() As String
        Get
            Return (CRAT_Name)
        End Get
        Set(ByVal Value As String)
            CRAT_Name = Value
        End Set
    End Property
    Public Property sCRDesc() As String
        Get
            Return (CRAT_Desc)
        End Get
        Set(ByVal Value As String)
            CRAT_Desc = Value
        End Set
    End Property
    Public Property sColor() As String
        Get
            Return (CRAT_Color)
        End Get
        Set(ByVal Value As String)
            CRAT_Color = Value
        End Set
    End Property

    ' Color
    Public Function LoadColors(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Trace_Color_Master Where TC_CompID=" & iACID & " "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadAuditPoint(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select CVR_ID,CVR_Point from CRPA_ValueRating Where CVR_CompID=" & iACID & " and CVR_YearID=" & iYearID & " order by CVR_ID "
    '        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetSectionNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iSecID As String) As String
        Dim sSql As String
        Try
            sSql = "Select CAS_SECTIONNAME from CRPA_Section where CAS_ID=" & iSecID & " And CAS_CompId=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSectionPKID(ByVal sAc As String, ByVal iAcID As Integer, sSecName As String, iYearID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CAS_ID From CRPA_Section where CAS_CompID=" & iAcID & " And  CAS_sectionName='" & sSecName & "' and CAS_YearID =" & iYearID & " "
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditPoint(ByVal sAc As String, ByVal iAcID As Integer, iID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CVR_Point From CRPA_ValueRating where CVR_Compid=" & iAcID & " And  CVR_Point=" & iID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveSection(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select CAS_ID,CAS_SECTIONNAME from CRPA_Section Where   CAS_DelFlg  = 'A' and CAS_CompId=" & iACID & " order by CAS_ID"
            'CAS_Code Like 'AUD%' and
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditValuetb(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CVR_ID,SubString(CVR_Name,0,200) As CVR_Name from CRPA_ValueRating Where CVR_AuditId=" & iAuditID & "  And CVR_CompId=" & iACID & " And  CVR_YearID=" & iYearID & "  order by CVR_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditScoretb(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CRAT_PKID,SubString(CRAT_Name,0,200) As CRAT_Name from CRPA_Rating Where CRAT_AuditId=" & iAuditID & "  And CRAT_CompId=" & iACID & " And  CRAT_YearID=" & iYearID & "  order by CRAT_PKID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditValueDesc(ByVal sAC As String, ByVal iACID As Integer, ByVal iDesc As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CVR_Point,CVR_AuditId, CVR_Name, CVR_Desc, CVR_Flag from CRPA_ValueRating Where CVR_ID=" & iDesc & " And CVR_CompId=" & iACID & " And  CVR_YearID=" & iYearID & "  order by CVR_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditScoreDesc(ByVal sAC As String, ByVal iACID As Integer, ByVal iDesc As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CRAT_AuditId, CRAT_StartValue, CRAT_EndValue, CRAT_Name, CRAT_Desc, CRAT_Color, CRAT_Flag from CRPA_Rating Where CRAT_PKID=" & iDesc & " And CRAT_CompId=" & iACID & " And  CRAT_YearID=" & iYearID & "  order by CRAT_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditRating(ByVal sAC As String, ByVal iAuditID As Integer, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Points")
            dtTab.Columns.Add("Desc")
            dtTab.Columns.Add("Status")

            sSql = "Select * from CRPA_ValueRating Where CVR_AuditID=" & iAuditID & " And CVR_CompID= " & iACID & " And CVR_YearID=" & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And CVR_FLAG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CVR_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CVR_FLAG='W'" 'Waiting for approval
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (CVR_Name Like '" & sSearch & "%')"
            End If
            sSql = sSql & " order by CVR_Point"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("ID") = dt.Rows(i)("CVR_ID")
                dr("Name") = dt.Rows(i)("CVR_Name")
                dr("Points") = dt.Rows(i)("CVR_Point")
                dr("Desc") = dt.Rows(i)("CVR_Desc")

                If dt.Rows(i)("CVR_FLAG") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("CVR_FLAG") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CVR_FLAG") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditScore(ByVal sAC As String, ByVal iAuditID As Integer, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Desc")
            dtTab.Columns.Add("Color")
            dtTab.Columns.Add("Start")
            dtTab.Columns.Add("End")
            dtTab.Columns.Add("Status")

            sSql = "Select * from CRPA_Rating Where CRAT_AuditID=" & iAuditID & " And CRAT_CompID= " & iACID & " And CRAT_YearID=" & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And CRAT_FLAG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CRAT_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CRAT_FLAG='W'" 'Waiting for approval
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (CRAT_Name Like '" & sSearch & "%')"
            End If
            sSql = sSql & " order by CRAT_StartValue"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("ID") = dt.Rows(i)("CRAT_PKID")
                dr("Name") = dt.Rows(i)("CRAT_Name")
                dr("Desc") = dt.Rows(i)("CRAT_Desc")
                dr("Color") = dt.Rows(i)("CRAT_Color")
                dr("Start") = dt.Rows(i)("CRAT_StartValue")
                dr("End") = dt.Rows(i)("CRAT_EndValue")

                If dt.Rows(i)("CRAT_FLAG") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("CRAT_FLAG") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CRAT_FLAG") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckExistingDetailsMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal sName As Object, ByVal iDescID As Integer, ByVal sMasterType As String, ByVal iYearID As Integer, ByVal sSectionID As String) As Boolean
        Dim sSql As String = ""
        Try
            If sMasterType = "AR" Then
                sSql = "Select CVR_ID from CRPA_ValueRating where CVR_CompID=" & iACID & " And CVR_Name='" & sName & "'  And CVR_YearID=" & iYearID & " and CVR_AuditId='" & sSectionID & "'"
                If iDescID > 0 Then
                    sSql = sSql & " And CVR_ID<>" & iDescID & ""
                End If
            ElseIf sMasterType = "AS" Then
                sSql = "Select CRAT_PKID from CRPA_Rating where CRAT_CompID=" & iACID & " And CRAT_Name = '" & sName & "' and CRAT_YearID=" & iYearID & " and CRAT_AuditId='" & sSectionID & "'"
                If iDescID > 0 Then
                    sSql = sSql & " And CRAT_PKID<>" & iDescID & ""
                End If
            End If
            CheckExistingDetailsMaster = objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub AuditValueApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditId As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update CRPA_ValueRating set"
            If sType = "Created" Then
                sSql = sSql & " CVR_Flag='A',CVR_STATUS='A',CVR_ApprovedBy=" & iUserID & ", CVR_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CVR_FLAG='D',CVR_STATUS='AD',CVR_DeletedBy=" & iUserID & ", CVR_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CVR_FLAG='A',CVR_STATUS='AR',CVR_RecallBy=" & iUserID & ", CVR_RecallOn=Getdate(),"
            End If
            sSql = sSql & " CVR_IPAddress='" & sIPAddress & "' Where CVR_AuditId= " & iAuditId & " And CVR_CompID=" & iACID & " And CVR_ID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub AuditScoreApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditId As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String, ByVal sTableType As String)
        Dim sSql As String
        Try
            sSql = "Update CRPA_Rating set"
            If sType = "Created" Then
                sSql = sSql & " CRAT_Flag='A',CRAT_STATUS='A',CRAT_ApprovedBy=" & iUserID & ", CRAT_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CRAT_Flag='D',CRAT_STATUS='AD',CRAT_DeletedBy=" & iUserID & ", CRAT_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CRAT_Flag='A',CRAT_STATUS='AR',CRAT_RecallBy=" & iUserID & ", CRAT_RecallOn=Getdate(),"
            End If
            sSql = sSql & " CRAT_IPAddress='" & sIPAddress & "' Where CRAT_AuditId= " & iAuditId & " And CRAT_CompID=" & iACID & " And CRAT_PKID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveAuditvalueRating(ByVal sAC As String, ByVal objclsMaster As clsGeneralCMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_Point", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsMaster.sAuditpoint
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_AuditId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iAuditID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_IpAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CVR_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCRPA_ValueRating", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAuditScore(ByVal sAC As String, ByVal objclsMaster As clsGeneralCMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_AuditID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCRAuditpoint
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_StartValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dStartValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_EndValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dEndValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sCRDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sCRName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_Color", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsMaster.sColor
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_IpAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CRAT_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCRPA_Rating", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
