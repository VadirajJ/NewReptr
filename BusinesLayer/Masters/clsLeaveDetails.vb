Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Structure strLeaveDetails
    Private LPE_ID As Integer
    Private LPE_EMPID As Integer
    Private LPE_YearID As Integer
    Private LPE_FROMDATE As Date
    Private LPE_TODATE As Date
    Private LPE_DAYS As Integer
    Private LPE_PURPOSE As String
    Private LPE_DelFlag As String
    Private LPE_Status As String
    Private LPE_CrBY As Integer
    Private LPE_UpdatedBY As Integer
    Private LPE_IPAddress As String
    Private LPE_CompID As Integer
    Public Property iLPE_ID() As Integer
        Get
            Return (LPE_ID)
        End Get
        Set(ByVal Value As Integer)
            LPE_ID = Value
        End Set
    End Property
    Public Property iLPE_EMPID() As Integer
        Get
            Return (LPE_EMPID)
        End Get
        Set(ByVal Value As Integer)
            LPE_EMPID = Value
        End Set
    End Property
    Public Property iLPE_YearID() As Integer
        Get
            Return (LPE_YearID)
        End Get
        Set(ByVal Value As Integer)
            LPE_YearID = Value
        End Set
    End Property
    Public Property dLPE_FROMDATE() As Date
        Get
            Return (LPE_FROMDATE)
        End Get
        Set(ByVal Value As Date)
            LPE_FROMDATE = Value
        End Set
    End Property
    Public Property dLPE_TODATE() As Date
        Get
            Return (LPE_TODATE)
        End Get
        Set(ByVal Value As Date)
            LPE_TODATE = Value
        End Set
    End Property
    Public Property iLPE_DAYS() As Integer
        Get
            Return (LPE_DAYS)
        End Get
        Set(ByVal Value As Integer)
            LPE_DAYS = Value
        End Set
    End Property
    Public Property sLPE_PURPOSE() As String
        Get
            Return (LPE_PURPOSE)
        End Get
        Set(ByVal Value As String)
            LPE_PURPOSE = Value
        End Set
    End Property
    Public Property sLPE_DelFlag() As String
        Get
            Return (LPE_DelFlag)
        End Get
        Set(ByVal Value As String)
            LPE_DelFlag = Value
        End Set
    End Property
    Public Property sLPE_Status() As String
        Get
            Return (LPE_Status)
        End Get
        Set(ByVal Value As String)
            LPE_Status = Value
        End Set
    End Property
    Public Property iLPE_CrBY() As Integer
        Get
            Return (LPE_CrBY)
        End Get
        Set(ByVal Value As Integer)
            LPE_CrBY = Value
        End Set
    End Property
    Public Property iLPE_UpdatedBY() As Integer
        Get
            Return (LPE_UpdatedBY)
        End Get
        Set(ByVal Value As Integer)
            LPE_UpdatedBY = Value
        End Set
    End Property
    Public Property sLPE_IPAddress() As String
        Get
            Return (LPE_IPAddress)
        End Get
        Set(ByVal Value As String)
            LPE_IPAddress = Value
        End Set
    End Property
    Public Property iLPE_CompID() As Integer
        Get
            Return (LPE_CompID)
        End Get
        Set(ByVal Value As Integer)
            LPE_CompID = Value
        End Set
    End Property
End Structure
Public Class clsLeaveDetails
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadActiveUserCode(ByVal sAc As String, ByVal iAcID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_Code From Sad_UserDetails where Usr_CompID=" & iAcID & " And Usr_ID=" & iUserID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveLeaveDetails(ByVal sAC As String, ByVal objLeaveDetails As strLeaveDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLeaveDetails.iLPE_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_EMPID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLeaveDetails.iLPE_EMPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLeaveDetails.iLPE_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_FROMDATE", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objLeaveDetails.dLPE_FROMDATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_TODATE", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objLeaveDetails.dLPE_TODATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_DAYS", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objLeaveDetails.iLPE_DAYS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_PURPOSE", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objLeaveDetails.sLPE_PURPOSE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_CrBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLeaveDetails.iLPE_CrBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_UpdatedBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLeaveDetails.iLPE_UpdatedBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLeaveDetails.sLPE_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPE_CompID", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objLeaveDetails.iLPE_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spFla_LeaveDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridLeaveDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("EmpID")
            dtTab.Columns.Add("LeavePurpose")
            dtTab.Columns.Add("FromDate")
            dtTab.Columns.Add("ToDate")
            dtTab.Columns.Add("NoDays")
            dtTab.Columns.Add("Status")

            sSql = "Select * From Fla_LeaveDetails where LPE_CompID=" & iACID & " And LPE_EMPID=" & iUserID & " And LPE_YearID=" & iYearID & " Order by LPE_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("LPE_ID")) = False Then
                    drow("ID") = dt.Rows(i)("LPE_ID")
                End If
                If IsDBNull(dt.Rows(i)("LPE_EMPID")) = False Then
                    drow("EmpID") = dt.Rows(i)("LPE_EMPID")
                End If
                If IsDBNull(dt.Rows(i)("LPE_PURPOSE")) = False Then
                    drow("LeavePurpose") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("LPE_PURPOSE"))
                End If
                If IsDBNull(dt.Rows(i)("LPE_FROMDATE")) = False Then
                    drow("FromDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("LPE_FROMDATE"), "F")
                End If
                If IsDBNull(dt.Rows(i)("LPE_TODATE")) = False Then
                    drow("ToDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("LPE_TODATE"), "F")
                End If
                If IsDBNull(dt.Rows(i)("LPE_DAYS")) = False Then
                    drow("NoDays") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("LPE_DAYS"))
                End If
                If IsDBNull(dt.Rows(i)("LPE_Approve")) = False Then
                    If dt.Rows(i)("LPE_Approve") = "A" Then
                        drow("Status") = "Approved"
                    Else
                        drow("Status") = "Pending"
                    End If
                Else
                    drow("Status") = "Pending"
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLeaveDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * From Fla_LeaveDetails where LPE_CompID=" & iACID & " And LPE_EMPID=" & iUserID & " And LPE_ID=" & iID & " And LPE_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateLeaveDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal iYearID As Integer, ByVal sStatus As String, ByVal sRemarks As String)
        Dim sSql As String
        Try
            sSql = "Update Fla_LeaveDetails Set LPE_Approve='" & sStatus & "',LPE_ApprovedDetails='" & sRemarks & "' where LPE_CompID=" & iACID & " And LPE_EMPID=" & iUserID & " And LPE_ID=" & iID & " And LPE_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
