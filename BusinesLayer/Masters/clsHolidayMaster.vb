Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsHolidayMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private iHol_YearId As Integer
    Private dHol_Date As Date
    Private sHol_Remarks As String
    Private iHol_Createdby As Integer
    Private dHol_CreatedOn As Date
    Private iHol_UpdatedBy As Integer
    Private dHol_UpdatedOn As Date
    Private sHol_Delflag As String
    Private sHol_Status As String
    Private sHol_IPAddress As String
    Private iHol_CompID As Integer
    Public Property iHolYearId() As Integer
        Get
            Return (iHol_YearId)
        End Get
        Set(ByVal Value As Integer)
            iHol_YearId = Value
        End Set
    End Property
    Public Property dHoldate() As Date
        Get
            Return (dHol_Date)
        End Get
        Set(ByVal Value As Date)
            dHol_Date = Value
        End Set
    End Property
    Public Property sHolRemarks() As String
        Get
            Return (sHol_Remarks)
        End Get
        Set(ByVal Value As String)
            sHol_Remarks = Value
        End Set
    End Property
    Public Property iHolCreatedby() As Integer
        Get
            Return (iHol_Createdby)
        End Get
        Set(ByVal Value As Integer)
            iHol_Createdby = Value
        End Set
    End Property
    Public Property dHolCreatedOn() As Date
        Get
            Return (dHol_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dHol_CreatedOn = Value
        End Set
    End Property
    Public Property iHolUpdatedBy() As Integer
        Get
            Return (iHol_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iHol_UpdatedBy = Value
        End Set
    End Property
    Public Property dHolUpdatedOn() As Date
        Get
            Return (dHol_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dHol_UpdatedOn = Value
        End Set
    End Property
    Public Property sHolDelflag() As String
        Get
            Return (sHol_Delflag)
        End Get
        Set(ByVal Value As String)
            sHol_Delflag = Value
        End Set
    End Property
    Public Property sHolStatus() As String
        Get
            Return (sHol_Status)
        End Get
        Set(ByVal Value As String)
            sHol_Status = Value
        End Set
    End Property
    Public Property sHolIPAddress() As String
        Get
            Return (sHol_IPAddress)
        End Get
        Set(ByVal Value As String)
            sHol_IPAddress = Value
        End Set
    End Property
    Public Property iHolCompID() As Integer
        Get
            Return (iHol_CompID)
        End Get
        Set(ByVal Value As Integer)
            iHol_CompID = Value
        End Set
    End Property
    Public Function LoadYears(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select YMS_YEARID,substring(YMS_ID,3,2)+ '-' +substring(YMS_ID,8,2) As YMS_ID from YEAR_MASTER where YMS_CompId=" & iACID & " order by YMS_ID asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindYearsDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iDateFormatCode As Integer, ByVal iYearID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Select YMS_ID,YMS_FROMDATE,YMS_TODATE,YMS_Default,Yms_Status,"
            sSql = sSql & "YMS_Delflag from YEAR_MASTER where YMS_CompId=" & iACID & " And YMS_YEARID=" & iYearID & ""
            Return objDBL.SQLExecuteDataSet(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCurrentYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select YMS_YEARID From YEAR_MASTER where YMS_CompId=" & iACID & " "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                sSql = "Update YEAR_MASTER set YMS_Default=0 where YMS_YEARID=" & dt.Rows(i)("YMS_YEARID") & " And YMS_CompId=" & iACID & " "
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            Next
            sSql = "Update YEAR_MASTER set YMS_Default=1, yms_Status ='U' where YMS_CompId=" & iACID & " And YMS_YEARID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDateFormat(ByVal sAC As String, ByVal iACID As Integer, ByVal iDateCode As Integer, ByVal iday As String) As String
        Dim sSql As String
        Try
            If iday = 0 Then
                sSql = "Select Convert(varchar(10),Getdate()," & iDateCode & ")"
            Else
                If iday.StartsWith("-") Then
                    sSql = "Select Convert(varchar(10),Getdate()" & iday & "," & iDateCode & ")"
                Else
                    sSql = "Select Convert(varchar(10),Getdate() + " & iday & "," & iDateCode & ")"
                End If
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch exp As System.Exception
            Throw
        End Try
    End Function
    Public Function GetAllHolidays(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String, sHolidays As String = ""
        Dim dt As New DataTable
        Dim i As Integer
        Try
            sSql = " Select Convert(VarChar(10),HOL_Date,103) +' (' + HOL_Remarks +')' As Date From Holiday_MASTER Where HOL_YearID=" & iYearID & " and Hol_Status ='A'"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                sHolidays = sHolidays & ";" & dt.Rows(i)("Date")
            Next
            If sHolidays.StartsWith(";") = False Then
                sHolidays = ";" & sHolidays
            End If
            If sHolidays.EndsWith(";") = False Then
                sHolidays = sHolidays & ";"
            End If
            Return sHolidays
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteHoliday(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sHolidayDeleteDate As String, ByVal iYearId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Holiday_Master set  Hol_status='D',Hol_Delflag='D',Hol_Updatedby=" & iUserID & ",Hol_UpdatedOn=GetDate() WHERE Hol_YearID=" & iYearId & " And "
            sSql = sSql & "Convert(Varchar(10),Hol_Date,103) ='" & sHolidayDeleteDate & "' And Hol_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetToDate(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearId As Integer) As Object
        Dim sSql As String
        Try
            sSql = "Select YMS_TODATE FROM YEAR_MASTER WHERE YMS_CompId=" & iACID & " And YMS_YEARID=" & iYearId - 1 & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function IsDefaultYearChecked(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As Boolean
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select YMS_YEARID FROM YEAR_MASTER WHERE YMS_CompId=" & iACID & " And YMS_Default=1 And YMS_YEARID <> " & iYearID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateYearDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal sYear As String, ByVal sFD As Date, ByVal sED As Date, ByVal iYearID As Integer, ByVal iFlag As Integer, ByVal iUserId As Integer, ByVal iDefault As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Dim iDefaultYearID As Integer, iMax As Integer
        Try
            If iDefault = 1 Then
                iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, "Select YMS_YEARID FROM YEAR_MASTER WHERE YMS_CompId=" & iACID & " And YMS_Default=1")
                If iDefaultYearID > 0 Then
                    sSql = "Update YEAR_MASTER set yms_Status ='U',YMS_IPAddress='" & sIPAddress & "', YMS_Default=0 where YMS_CompId=" & iACID & " And YMS_YEARID=" & iDefaultYearID & ""
                    objDBL.SQLExecuteNonQuery(sAC, sSql)
                End If
            End If
            If iFlag = 1 Then
                iMax = objclsGeneralFunctions.GetMaxID(sAC, iACID, "YEAR_MASTER", "YMS_YEARID", "YMS_CompId")
                sSql = "" : sSql = "Insert into YEAR_MASTER(YMS_ID,YMS_FROMDATE,YMS_TODATE,YMS_YEARID,YMS_CompId,yms_Createdby,"
                sSql = sSql & "yms_Createdon,yms_Status,YMS_Default,Yms_Delflag,YMS_IPAddress)VALUES('" & sYear & "',"
                sSql = sSql & "" & objclsGRACeGeneral.FormatDtForRDBMS(sFD, "I") & "," & objclsGRACeGeneral.FormatDtForRDBMS(sED, "I") & ","
                sSql = sSql & "" & iMax & "," & iACID & "," & iUserId & ",GetDate(),'C'," & iDefault & ",'W','" & sIPAddress & "')"
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf iFlag = 2 Then
                iMax = iYearID
                sSql = "" : sSql = "Update YEAR_MASTER set YMS_IPAddress='" & sIPAddress & "',YMS_ID='" & sYear & "',YMS_FROMDATE=" & objclsGRACeGeneral.FormatDtForRDBMS(sFD, "I") & ","
                sSql = sSql & "yms_Updatedby=" & iUserId & ",yms_Updatedon=GetDate(),yms_Status ='U',"
                sSql = sSql & "YMS_TODATE=" & objclsGRACeGeneral.FormatDtForRDBMS(sED, "I") & ",YMS_Default=" & iDefault & " Where YMS_CompId =" & iACID & " And YMS_YEARID=" & iYearID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveHolidayDetails(ByVal sAC As String, ByVal objHoliday As clsHolidayMaster)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objHoliday.iHolYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_Date", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objHoliday.dHoldate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_Remarks", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objHoliday.sHolRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_Createdby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objHoliday.iHolCreatedby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objHoliday.iHolUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_Delflag", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objHoliday.sHol_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objHoliday.sHolStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objHoliday.sHol_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Hol_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objHoliday.iHolCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1
            Arr(0) = "@iUpdateOrSave"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spHoliday_master", 0, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function HolidayMasterDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iDateFormatCode As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("HolidayDate")
            dt.Columns.Add("HDFormat")
            dt.Columns.Add("Occasion")
            dt.Columns.Add("Status")
            sSql = "Select Hol_Date,Hol_Remarks Remarks,Hol_Delflag from Holiday_Master where"
            sSql = sSql & "  HOL_CompID=" & iACID & " And Hol_Yearid =" & iYearID & " And Hol_Delflag='A' Order by Hol_Date"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    dRow("HolidayDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("Hol_Date"), "F")
                    dRow("HDFormat") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("Hol_Date"), "D")
                    dRow("Occasion") = dtDetails.Rows(i)("Remarks")
                    If (dtDetails.Rows(i)("Hol_Delflag") = "W") Then
                        dRow("Status") = "Waiting for Approval"
                    ElseIf (dtDetails.Rows(i)("Hol_Delflag") = "A") Then
                        dRow("Status") = "Activated"
                    ElseIf (dtDetails.Rows(i)("Hol_Delflag") = "D") Then
                        dRow("Status") = "De-Activated"
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class