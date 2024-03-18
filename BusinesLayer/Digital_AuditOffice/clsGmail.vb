Imports DatabaseLayer
Public Class clsGmail
    Private objDBL As New DBHelper
    Public Function GetUserEmailFromUserID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USR_Email from Sad_Userdetails where Usr_ID=" & iUserID & " And USR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWeekBackDate(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select Convert(Varchar(10),Getdate()-7,103)"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmpIDFromEmailID(ByVal sAC As String, ByVal iACID As Integer, ByVal sEmailID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where usr_FullName='" & sEmailID & "' And USR_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
