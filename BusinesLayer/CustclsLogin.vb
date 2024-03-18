Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports DatabaseLayer
Public Class CustclsLogin
    Dim objDBL As New DBHelper
    Public Function CheckValidLoginUserName(ByVal sUserName As String)
        Dim sSql As String = ""
        Try
            sSql = "Select MUL_PKID from MMCSPL_UserLogin where MUL_LoginName='" & sUserName & "'"
            Return objDBL.SQLExecuteScalar("MMCSPL", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoginUserID(ByVal sUsername As String, ByVal sPassword As String)
        Dim sSql As String = ""
        Try
            sSql = "Select MUL_PKID from MMCSPL_UserLogin where MUL_LoginName='" & sUsername & "' and MUL_Password='" & sPassword & "'"
            Return objDBL.SQLExecuteScalar("MMCSPL", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UnSuccusfullAttemptUpdate(ByVal sUserName As String)
        Dim sSql As String = ""
        Try
            sSql = "Update MMCSPL_UserLogin set MUL_UnSuccessfullAttempts=MUL_UnSuccessfullAttempts+1 where MUL_LoginName='" & sUserName & "'"
            objDBL.SQLExecuteNonQuery("MMCSPL", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckDatabaseExists(ByVal sAccesCode As String, ByVal sDatabaseName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "SELECT database_id FROM sys.databases WHERE Name='" & sDatabaseName & "'"
            Return objDBL.DBCheckForRecord(sAccesCode, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateLoginDetails(ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update MMCSPL_UserLogin set MUL_NoOfLogins=MUL_NoOfLogins+1,MUL_LastLogindate=GetDate(),MUL_IPAddress='" & sIPAddress & "' where MUL_PKID=" & iUserID & ""
            objDBL.SQLExecuteNonQuery("MMCSPL", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub AuditLogin(ByVal MA_UserID As Integer, ByVal MA_IPAddress As String)
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "INSERT into MMCSPL_AuditLogin(MA_UserID,MA_DateTime,MA_IPAddress)values('" & MA_UserID & "',GetDate(),'" & MA_IPAddress & "')"
    '        objDBL.SQLExecuteNonQuery("MMCSPL", sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Function EncryptPassword(ByVal sValue As String) As String
        Dim EncryptionKey As String = "ML736@mmcs"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(sValue)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                sValue = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return sValue
    End Function
    Public Function DecryptPassword(ByVal sValue As String) As String
        Dim DecryptionKey As String = "ML736@mmcs"
        sValue = sValue.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(sValue)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(DecryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                sValue = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return sValue
    End Function
End Class
