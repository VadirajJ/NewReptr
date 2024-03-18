Imports System
Imports System.Data
Imports System.IO
Imports System.Configuration
Public Class DBExport
    Dim ConStr As String
    Dim MyRDBMS As String

    Public Sub New(ByVal sConStr As String, ByVal sRDBMS As String)
        ConStr = sConStr
        MyRDBMS = sRDBMS
    End Sub

    Public Function OpenConnection() As OleDb.OleDbConnection
        Dim ObjCon As OleDb.OleDbConnection
        Try
            ObjCon = New OleDb.OleDbConnection(ConStr)
            ObjCon.Open()
            OleDb.OleDbConnection.ReleaseObjectPool()
            OpenConnection = ObjCon
        Catch ex As Exception
            Throw

        Finally
        End Try
    End Function
    Public Function DBExecuteNoNQuery(ByVal MySql As String) As Boolean
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pSqlCmd As New OleDb.OleDbCommand
        Dim MyTrans As OleDb.OleDbTransaction
        Try
            pSqlcon = OpenConnection()
            MyTrans = pSqlcon.BeginTransaction
            With pSqlCmd
                .Connection = pSqlcon
                .Transaction = MyTrans
                .CommandText = MySql
                .ExecuteNonQuery()
                MyTrans.Commit()
                .Dispose()
                DBExecuteNoNQuery = True
            End With

        Catch ex As Exception
            MyTrans.Rollback()
            Throw
        Finally
            pSqlcon.Close()
            pSqlcon.Dispose()
        End Try
    End Function
    Public Function DBExecuteNoNQuerySP(ByVal MySql As String) As Boolean
        Dim pSqlcon As New OleDb.OleDbConnection
        Dim pSqlCmd As New OleDb.OleDbCommand
        Dim MyTrans As OleDb.OleDbTransaction

        Try
            Dim strSP() As String
            pSqlcon = OpenConnection()
            MyTrans = pSqlcon.BeginTransaction
            With pSqlCmd
                    .Connection = pSqlcon
                .Transaction = MyTrans
                strSP = MySql.Split("/")
                For i = 0 To strSP.Length - 1
                    .CommandText = strSP(i)
                    .ExecuteNonQuery()
                Next
                MyTrans.Commit()
                    .Dispose()
                    DBExecuteNoNQuerySP = True
                End With

        Catch ex As Exception
            MyTrans.Rollback()
            Throw
        Finally
            pSqlcon.Close()
            pSqlcon.Dispose()
        End Try
    End Function
End Class
