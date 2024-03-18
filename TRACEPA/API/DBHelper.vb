Imports System
Imports System.Data
Imports System.IO
Imports System.Configuration
Public Class DBHelper
    Public Function SQLOpenDBConnection(ByVal sAccessCode As String) As OleDb.OleDbConnection
        Dim pSqlcon As OleDb.OleDbConnection
        Dim sConn As String
        Try
            sConn = GetKeyValues(sAccessCode)
            pSqlcon = New OleDb.OleDbConnection(sConn)
            pSqlcon.Open()
            OleDb.OleDbConnection.ReleaseObjectPool()
            Return pSqlcon
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKeyValues(ByVal sKeyName As String) As String
        Dim sConn() As String
        Try
            sConn = System.Configuration.ConfigurationManager.AppSettings.GetValues(sKeyName)
            If IsNothing(sConn) = False Then
                Return sConn(0)
            End If
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetColumnDescription(ByVal sAC As String, ByVal sFieldName As String, ByVal sColumnName As String, ByVal sValue As String, ByVal sTableName As String) As String
        Dim sSql As String
        Dim obj As Object
        Try
            sSql = "Select " & sFieldName & " From " & sTableName & " Where " & sColumnName & "=" & sValue & ""
            obj = SQLExecuteScalar(sAC, sSql)
            If obj Is Nothing Then
                Return String.Empty
            End If
            Return (obj.ToString)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SQLExecuteNonQuery(ByVal sAccessCode As String, ByVal MySql As String)
        Dim pSqlcon As New OleDb.OleDbConnection
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlCmd As New OleDb.OleDbCommand(MySql, pSqlcon)
                pSqlCmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Sub
    Public Function SQLExecuteScalar(ByVal sAccessCode As String, ByVal MySql As String) As Object
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim oValue As Object
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlCmd As New OleDb.OleDbCommand(MySql, pSqlcon)
                oValue = pSqlCmd.ExecuteScalar()
                Return oValue
            End Using
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function DBCheckForRecord(ByVal sAccessCode As String, ByVal ssql As String) As Boolean
        Dim objOleConn As New OleDb.OleDbConnection
        Dim objOleCmd As OleDb.OleDbCommand
        Dim pOleDBReader As OleDb.OleDbDataReader
        Try
            objOleConn = SQLOpenDBConnection(sAccessCode)
            objOleCmd = New OleDb.OleDbCommand(ssql, objOleConn)
            pOleDBReader = objOleCmd.ExecuteReader()
            If pOleDBReader.HasRows = True Then
                Do While pOleDBReader.Read
                    DBCheckForRecord = True
                    Exit Try
                Loop
                DBCheckForRecord = False
            Else
                DBCheckForRecord = False
            End If
        Catch ex As Exception
            Throw
        Finally
            pOleDBReader.Close()
        End Try
    End Function
    Public Function SQLExecuteScalarInt(ByVal sAccessCode As String, ByVal MySql As String) As Integer
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim iValue As Integer
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlCmd As New OleDb.OleDbCommand(MySql, pSqlcon)
                iValue = pSqlCmd.ExecuteScalar()
                Return iValue
            End Using
        Catch ex As Exception
            Return 0
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function SQLExecuteDataSet(ByVal sAccessCode As String, ByVal MySql As String) As DataSet
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pDSReturn = New DataSet
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlAdapter As New OleDb.OleDbDataAdapter(MySql, pSqlcon)
                pSqlAdapter.Fill(pDSReturn, "ReturnTable")
                Return pDSReturn
            End Using
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function SQLExecuteDataTable(ByVal sAccessCode As String, ByVal MySql As String) As DataTable
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pDSReturn = New DataSet
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlAdapter As New OleDb.OleDbDataAdapter(MySql, pSqlcon)
                pSqlAdapter.Fill(pDSReturn, "ReturnTable")
                Return pDSReturn.Tables(0)
            End Using
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function SQLDataReader(ByVal sAccessCode As String, ByVal MySql As String) As OleDb.OleDbDataReader
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pdr As OleDb.OleDbDataReader
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlCmd As New OleDb.OleDbCommand(MySql, pSqlcon)
                pdr = pSqlCmd.ExecuteReader
                Return pdr
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SQLCheckForRecord(ByVal sAccessCode As String, ByVal MySql As String) As Boolean
        Dim dr As OleDb.OleDbDataReader
        Dim pSqlcon As New OleDb.OleDbConnection
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlCmd As New OleDb.OleDbCommand(MySql, pSqlcon)
                dr = pSqlCmd.ExecuteReader()
                If dr.HasRows = True Then
                    SQLCheckForRecord = True
                Else
                    SQLCheckForRecord = False
                End If
                dr.Close()
            End Using
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function SQLGetDescription(ByVal sAccessCode As String, ByVal MySql As String) As String
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim strDescription As String
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            Using pSqlCmd As New OleDb.OleDbCommand(MySql, pSqlcon)
                strDescription = pSqlCmd.ExecuteScalar
                Return strDescription
            End Using
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function ExecuteSPForMultipleInsert(ByVal sAccessCode As String, ByVal sQry As String) As Boolean
        Dim iQueries As Integer
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pSqlCmd As New OleDb.OleDbCommand
        Dim strQry() As String
        pSqlcon = SQLOpenDBConnection(sAccessCode)
        pSqlCmd.Connection = pSqlcon
        Dim trans As OleDb.OleDbTransaction = pSqlcon.BeginTransaction
        Try
            strQry = sQry.Split(";")
            iQueries = strQry.GetUpperBound(0)
            pSqlCmd.Transaction = trans
            While (iQueries > 0)
                pSqlCmd.CommandText = strQry(iQueries - 1)
                pSqlCmd.ExecuteNonQuery()
                iQueries = iQueries - 1
            End While
            trans.Commit()
            pSqlCmd.Dispose()
            pSqlcon.Close()
            pSqlcon.Dispose()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Sub ExecuteSPForInsertNoOutput(ByVal sAccessCode As String, ByVal sSPName As String, ByVal ParamArray CmdParam() As OleDb.OleDbParameter)
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pSqlCmd As New OleDb.OleDbCommand
        Dim MyParam As OleDb.OleDbParameter
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            pSqlCmd.Connection = pSqlcon
            pSqlCmd.CommandType = CommandType.StoredProcedure
            pSqlCmd.CommandText = sSPName
            For Each MyParam In CmdParam
                If MyParam.Direction = ParameterDirection.Input And MyParam.Value Is Nothing Then
                    MyParam.Value = Nothing
                End If
                pSqlCmd.Parameters.Add(MyParam)
            Next
            pSqlCmd.ExecuteNonQuery()
            pSqlCmd.Connection.Close()
            pSqlCmd.Dispose()
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Sub
    Public Function ExecuteSPForInsert(ByVal sAccessCode As String, ByVal sSPName As String, ByVal sOutput As String, ByVal ParamArray CmdParam() As OleDb.OleDbParameter) As Integer
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pSqlCmd As New OleDb.OleDbCommand
        Dim MyParam As OleDb.OleDbParameter
        Dim iRet As Integer
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            pSqlCmd.Connection = pSqlcon
            pSqlCmd.CommandType = CommandType.StoredProcedure
            pSqlCmd.CommandText = sSPName
            For Each MyParam In CmdParam
                If MyParam.Direction = ParameterDirection.Input And MyParam.Value Is Nothing Then
                    MyParam.Value = Nothing
                End If
                pSqlCmd.Parameters.Add(MyParam)
            Next
            pSqlCmd.ExecuteNonQuery()
            iRet = pSqlCmd.Parameters(sOutput).Value
            pSqlCmd.Connection.Close()
            pSqlCmd.Dispose()
            Return iRet
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function ExecuteSPForInsertARR(ByVal sAccessCode As String, ByVal sSPName As String, ByVal outSize As Integer, ByVal outArr() As String, ByVal ParamArray CmdParam() As OleDb.OleDbParameter) As Array
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pSqlCmd As New OleDb.OleDbCommand
        Dim MyParam As OleDb.OleDbParameter
        Dim iRet As Integer
        Dim Arr(outSize) As String
        Try
            pSqlcon = SQLOpenDBConnection(sAccessCode)
            pSqlCmd.Connection = pSqlcon
            pSqlCmd.CommandType = CommandType.StoredProcedure
            pSqlCmd.CommandText = sSPName
            For Each MyParam In CmdParam
                If MyParam.Direction = ParameterDirection.Input And MyParam.Value Is Nothing Then
                    MyParam.Value = Nothing
                End If
                pSqlCmd.Parameters.Add(MyParam)
            Next
            pSqlCmd.ExecuteNonQuery()
            For iRet = 0 To outSize
                Arr(iRet) = pSqlCmd.Parameters(outArr(iRet)).Value
            Next
            pSqlCmd.Connection.Close()
            pSqlCmd.Dispose()
            Return Arr
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Private Function MSAccessOpenConnection(ByVal sFile As String) As OleDb.OleDbConnection
        Dim pSqlcon As New OleDb.OleDbConnection
        Try
            pSqlcon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" & sFile & ";Extended Properties=Excel 8.0;"
            pSqlcon.Open()
            Return pSqlcon
        Catch ex As Exception
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
            End If
        End Try
        Try
            pSqlcon.ConnectionString = "Data Source=" & sFile & ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0;"
            pSqlcon.Open()
            Return pSqlcon
        Catch ex As Exception
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
            End If
        End Try
        Try
            pSqlcon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';Data Source=" & sFile & ""
            pSqlcon.Open()
            Return pSqlcon
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function ReadExcel(ByVal sSql As String, ByVal sFilePath As String) As DataTable
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim da As New OleDb.OleDbDataAdapter
        Dim ds As New DataSet
        Try
            pSqlcon = MSAccessOpenConnection(sFilePath)
            If IsNothing(pSqlcon) = False Then
                da = New OleDb.OleDbDataAdapter(sSql, pSqlcon)
                da.Fill(ds)
            End If
            Return ds.Tables(0)
        Catch ex As Exception
            Throw
        Finally
            If pSqlcon.State = ConnectionState.Open Then
                pSqlcon.Close()
                OleDb.OleDbConnection.ReleaseObjectPool()
                pSqlcon.Dispose()
            End If
        End Try
    End Function
    Public Function SPFrLoadingUsingDsParam(ByVal sNameSpace As String, ByVal sSPName As String, ByVal ArrSize As Integer, ByVal PName As String, ByVal ParamArray CmdParam() As OleDb.OleDbParameter) As Array
        Dim psqlConn As OleDb.OleDbConnection
        Dim myCmd As New OleDb.OleDbCommand
        Dim MyParam As OleDb.OleDbParameter
        Dim dt As New DataTable
        Dim Arr(ArrSize) As Object
        Try
            psqlConn = SQLOpenDBConnection(sNameSpace)
            myCmd.Connection = psqlConn
            myCmd.CommandType = CommandType.StoredProcedure
            myCmd.CommandText = sSPName
            For Each MyParam In CmdParam
                If MyParam.Direction = ParameterDirection.InputOutput And MyParam.Value Is Nothing Then
                    MyParam.Value = Nothing
                End If
                myCmd.Parameters.Add(MyParam)
            Next
            Dim ada As New OleDb.OleDbDataAdapter
            ada.SelectCommand = myCmd
            ada.Fill(dt)
            Arr(0) = dt
            Arr(1) = myCmd.Parameters.Item(PName).Value
            Return Arr
        Catch ex As Exception
            Throw
        Finally
            'psqlConn.Close()
        End Try
    End Function
    Public Function SPFrLoadingUsingDs(ByVal sNameSpace As String, ByVal sSPName As String, ByVal ParamArray CmdParam() As OleDb.OleDbParameter) As DataSet
        Dim psqlConn As OleDb.OleDbConnection
        Dim myCmd As New OleDb.OleDbCommand
        Dim MyParam As New OleDb.OleDbParameter
        Dim ds As New DataSet
        Try
            psqlConn = SQLOpenDBConnection(sNameSpace)
            myCmd.Connection = psqlConn
            myCmd.CommandType = CommandType.StoredProcedure
            myCmd.CommandText = sSPName
            For Each MyParam In CmdParam
                If MyParam.Direction = ParameterDirection.InputOutput And MyParam.Value Is Nothing Then
                    MyParam.Value = Nothing
                End If
                myCmd.Parameters.Add(MyParam)
            Next
            Dim ada As New OleDb.OleDbDataAdapter
            ada.SelectCommand = myCmd
            ada.Fill(ds)
            Return ds
        Catch ex As Exception
            Throw
        Finally
            'psqlConn.Close()
        End Try
    End Function

    Public Function GetAllValues(ByVal sNameSpace As String, ByVal sFieldName As String, ByVal sTableName As String) As String
        Dim pdr As OleDb.OleDbDataReader
        Dim psqlConn As OleDb.OleDbConnection
        Dim pstrSQL As String = ""
        Dim sRet As String = ""
        Try
            pstrSQL = "select " & sFieldName & " From " & sTableName & ""
            psqlConn = SQLOpenDBConnection(sNameSpace)
            Dim psqlCmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(pstrSQL, psqlConn)
            pdr = psqlCmd.ExecuteReader
            If pdr.HasRows Then
                Do While pdr.Read
                    sRet = sRet & pdr(0) & ";"
                Loop
            End If
            pdr.Close()
            'psqlConn.Close()

            If InStr(sRet, ";") <> 0 Then
                sRet = Left(sRet, Len(sRet) - 1)
            End If
            Return sRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SpFrInsertionUsingCmd(ByVal sNameSpace As String, ByVal sSPName As String, ByVal ParamArray CmdParam() As OleDb.OleDbParameter) As OleDb.OleDbCommand
        Dim psqlConn As OleDb.OleDbConnection
        Dim myCmd As New OleDb.OleDbCommand
        Dim MyParam As OleDb.OleDbParameter
        Try
            If Not (CmdParam Is Nothing) AndAlso CmdParam.Length > 0 Then
                psqlConn = SQLOpenDBConnection(sNameSpace)
                myCmd.Connection = psqlConn
                myCmd.CommandType = CommandType.StoredProcedure
                myCmd.CommandText = sSPName
                For Each MyParam In CmdParam
                    If MyParam.Direction = ParameterDirection.InputOutput And MyParam.Value Is Nothing Then
                        MyParam.Value = Nothing
                    End If
                    myCmd.Parameters.Add(MyParam)
                Next
                myCmd.ExecuteNonQuery()
                Return myCmd
            End If
        Catch ex As Exception
            Throw
        Finally
            ' psqlConn.Close()
        End Try
    End Function
End Class
