Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Security.Cryptography
Public Class clsEDICTGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function SafeSQL(ByVal sStr As String) As String
        Try
            If IsNothing(sStr) = False Then
                sStr = sStr.Trim
                sStr = sStr.Replace("'", "`")
                sStr = sStr.Replace("--", "- -")
                sStr = sStr.Replace(";", ":")

                If sStr.Contains("INSERT") = True Then
                    sStr = sStr.Replace("INSERT", "IN SERT")
                End If
                If sStr.Contains("DELETE") = True Then
                    sStr = sStr.Replace("DELETE", "DE LETE")
                End If
                If sStr.Contains("TRUNCATE") = True Then
                    sStr = sStr.Replace("TRUNCATE", "TRUN CATE")
                End If
                If sStr.Contains("ALTER") = True Then
                    sStr = sStr.Replace("ALTER", "A L T E R")
                End If
                If sStr.Contains("DROP") = True Then
                    sStr = sStr.Replace("DROP", "D R O P")
                End If
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ReplaceSafeSQL(ByVal sStr As String) As String
        Try
            If IsNothing(sStr) = False Then
                'sStr=UCase(sStr)
                sStr = sStr.Replace("`", "'")
                sStr = sStr.Replace("- -", "--")
                sStr = sStr.Replace(":", ";")

                If sStr.Contains("IN SERT") = True Then
                    sStr = sStr.Replace("IN SERT", "INSERT")
                End If
                If sStr.Contains("DE LETE") = True Then
                    sStr = sStr.Replace("DE LETE", "DELETE")
                End If
                If sStr.Contains("TRUN CATE") = True Then
                    sStr = sStr.Replace("TRUN CATE", "TRUNCATE")
                End If
                If sStr.Contains("A L T E R") = True Then
                    sStr = sStr.Replace("A L T E R", "ALTER")
                End If
                If sStr.Contains("D R O P") = True Then
                    sStr = sStr.Replace("D R O P", "DROP")
                End If
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SafeFileName(ByVal sStr As String) As String
        Try
            If IsNothing(sStr) = False Then
                If sStr.Contains("\") = True Then
                    sStr = sStr.Replace("\", "")
                End If
                If sStr.Contains("/") = True Then
                    sStr = sStr.Replace("/", "")
                End If
                If sStr.Contains(":") = True Then
                    sStr = sStr.Replace(":", "")
                End If
                If sStr.Contains("*") = True Then
                    sStr = sStr.Replace("*", "")
                End If
                If sStr.Contains("?") = True Then
                    sStr = sStr.Replace("?", "")
                End If
                If sStr.Contains("<") = True Then
                    sStr = sStr.Replace("<", "")
                End If
                If sStr.Contains(">") = True Then
                    sStr = sStr.Replace(">", "")
                End If
                If sStr.Contains("|") = True Then
                    sStr = sStr.Replace("|", "")
                End If
                If sStr.Contains("[") = True Then
                    sStr = sStr.Replace("[", "")
                End If
                If sStr.Contains("]") = True Then
                    sStr = sStr.Replace("]", "")
                End If
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function FormatDtForRDBMS(ByVal dtDate As DateTime, ByVal sPurpose As String) As String
        Dim sTempDate As String = ""
        Try
            Select Case UCase(Trim(sPurpose))
                Case "Q" 'Query
                    sTempDate = "'" & Format(dtDate, "MM/dd/yyyy") & "'"
                Case "I" 'Insert
                    sTempDate = "'" & Format(dtDate, "dd\-MMM\-yyyy hh:mm:ss tt") & "'"
                Case "SP" 'Insert
                    sTempDate = "" & Format(dtDate, "dd\-MMM\-yyyy hh:mm:ss tt") & ""
                Case "U" 'Update
                    sTempDate = "'" & Format(dtDate, "dd\-MMM\-yyyy hh:mm:ss tt") & "'"
                Case "D"
                    sTempDate = Format(dtDate, "dd/MM/yyyy")
                Case "DD"
                    sTempDate = "'" & Format(dtDate, "MM/dd/yyyy") & "'"
                Case "DT"
                    sTempDate = Format(dtDate, "dd/MM/yyyy hh:mm:ss tt")
                Case "CT"
                    sTempDate = Format(dtDate, "yyyy-MM-dd 00:00:00.000")
                Case "T"
                    sTempDate = Format(dtDate, "MM/dd/yyyy")
                Case "F"
                    sTempDate = Format(dtDate, "dd-MMM-yy")
            End Select
            FormatDtForRDBMS = sTempDate
        Catch exp As System.Exception
            Throw
        End Try
    End Function
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
    Public Function EncryptQueryString(ByVal clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
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
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function
    Public Function DecryptQueryString(ByVal cipherText As String) As String
        Dim DecryptionKey As String = "MAKV2SPBNI99212"
        cipherText = cipherText.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
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
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function
    Public Function GetFileExt(ByVal sFileName As String) As String
        Dim i As Integer
        Dim s As String, j As String
        Try
            s = StrReverse(sFileName)
            i = InStr(s, ".")
            j = Left(s, i - 1)
            GetFileExt = StrReverse(j)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Shared Function Assign(Of T)(ByRef source As T, ByVal value As T) As T
        Try
            source = value
            Return value
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function FileEn(inputFilePath As String, outputfilePath As String)
    '    Try
    '        Dim EncryptionKey As String = "MAKV2SPBNI99212"
    '        Using encryptor As Aes = Aes.Create()
    '            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
    '         &H65, &H64, &H76, &H65, &H64, &H65,
    '         &H76})
    '            encryptor.Key = pdb.GetBytes(32)
    '            encryptor.IV = pdb.GetBytes(16)
    '            Using fs As New FileStream(outputfilePath, FileMode.Create)
    '                Using cs As New CryptoStream(fs, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
    '                    Using fsInput As New FileStream(inputFilePath, FileMode.Open)
    '                        Dim data As Integer
    '                        While (Assign(data, fsInput.ReadByte())) <> -1
    '                            cs.WriteByte(CByte(data))
    '                        End While
    '                    End Using
    '                End Using
    '            End Using
    '        End Using
    '        File.Delete(inputFilePath)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Public Function Decrypt(inputFilePath As String, outputfilePath As String)
    '    Try
    '        Dim EncryptionKey As String = "MAKV2SPBNI99212"
    '        Using encryptor As Aes = Aes.Create()
    '            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
    '         &H65, &H64, &H76, &H65, &H64, &H65,
    '         &H76})
    '            encryptor.Key = pdb.GetBytes(32)
    '            encryptor.IV = pdb.GetBytes(16)
    '            Using fs As New FileStream(inputFilePath, FileMode.Open)
    '                Using cs As New CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read)
    '                    Using fsOutput As New FileStream(outputfilePath, FileMode.Create)
    '                        Dim data As Integer
    '                        While (Assign(data, cs.ReadByte())) <> -1
    '                            fsOutput.WriteByte(CByte(data))
    '                        End While
    '                    End Using
    '                End Using
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Public Function DltDecryptFile(ByVal AccessCode As String, ByVal AccessCodeID As Integer, ByVal iUserId As Integer)
    '    Dim obj As New clsIndexing
    '    Dim sImagePath As String
    '    Try
    '        sImagePath = obj.GetImagePath(AccessCode, AccessCodeID)
    '        sImagePath = sImagePath & "View\" & iUserId & "\"

    '        If Directory.Exists(sImagePath) Then
    '            For Each filepath As String In Directory.GetFiles(sImagePath)
    '                File.Delete(filepath)
    '            Next
    '            For Each dir As String In Directory.GetDirectories(sImagePath)
    '                Directory.Delete(dir)
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function ChangeExt(ByVal sExt As String) As String
        Try

            sExt = sExt
            Return sExt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDecPathView(ByVal sTemppath As String, ByVal iUserID As Integer, ByVal sIPath As String, ByVal sFileName As String, ByVal sExt As String) As String
        Dim sOPath As String = "", sODPath As String = ""
        Try
            'If sTemppath.EndsWith("\") = True Then
            '    sDestPath = sTemppath & "View\ " & lblDocID.Text \ 301 & "\"
            'Else
            '    sDestPath = sTemppath & "View\" & lblDocID.Text \ 301 & "\"
            'End If

            sOPath = sTemppath & "View\" & iUserID & "\"
            sODPath = sTemppath & "View\" & iUserID & "\"
            If System.IO.Directory.Exists(sOPath) = False Then
                System.IO.Directory.CreateDirectory(sOPath)
            End If

            If (sExt.Contains(".") = True) Then
                sOPath = sOPath & sFileName & sExt
            Else
                sOPath = sOPath & sFileName & "." & sExt
            End If

            If (sExt.Contains(".") = True) Then
                sODPath = sODPath & sFileName & sExt
            Else
                sODPath = sODPath & sFileName & "." & sExt
            End If


            If System.IO.File.Exists(sOPath) = True Then
                System.IO.File.Delete(sOPath)
            End If

            If System.IO.File.Exists(sIPath) = True Then
                File.Copy(sIPath, sOPath)
            End If

            Return sOPath
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function GetOnlyDecPath(ByVal sTemppath As String, ByVal iUserID As Integer)
        Dim sOPath As String = ""
        Try
            sOPath = sTemppath & "View\" & iUserID & "\"
            If System.IO.Directory.Exists(sOPath) = False Then
                System.IO.Directory.CreateDirectory(sOPath)
            End If
            Return sOPath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWebSerPathView(ByVal sTemppath As String, ByVal iUserID As Integer, ByVal sIPath As String, ByVal sFileName As String, ByVal sExt As String) As String
        Dim sOPath As String = "", sODPath As String = ""
        Try

            sOPath = sTemppath & "WebSer\" & iUserID & "\"
            sODPath = sTemppath & "WebSer\" & iUserID & "\"
            If System.IO.Directory.Exists(sOPath) = False Then
                System.IO.Directory.CreateDirectory(sOPath)
            End If
            sOPath = sOPath & sFileName & "." & sExt
            sODPath = sODPath & sFileName & "." & sExt

            File.Copy(sIPath, sOPath)


            Return sOPath
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function FileEn(inputFilePath As String, outputfilePath As String)
        Try
            Dim EncryptionKey As String = "MAKV2SPBNI99212"
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using fs As New FileStream(outputfilePath, FileMode.Create)
                    Using cs As New CryptoStream(fs, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        Using fsInput As New FileStream(inputFilePath, FileMode.Open)
                            Dim data As Integer
                            While (Assign(data, fsInput.ReadByte())) <> -1
                                cs.WriteByte(CByte(data))
                            End While
                        End Using
                    End Using
                End Using
            End Using
            File.Delete(inputFilePath)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DltDecryptFile(ByVal AccessCode As String, ByVal AccessCodeID As Integer, ByVal iUserId As Integer)
        Dim obj As New clsIndexing
        Dim sImagePath As String
        Try
            sImagePath = obj.GetImagePath(AccessCode, AccessCodeID)
            sImagePath = sImagePath & "View\" & iUserId & "\"

            If Directory.Exists(sImagePath) Then
                For Each filepath As String In Directory.GetFiles(sImagePath)
                    File.Delete(filepath)
                Next
                For Each dir As String In Directory.GetDirectories(sImagePath)
                    Directory.Delete(dir)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
