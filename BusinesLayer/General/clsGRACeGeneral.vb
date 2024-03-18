Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports System.Web
Imports System.Security.Cryptography
Public Class clsGRACeGeneral
    Private objDBL As New DBHelper
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
    Public Function SafeSpaceSQL(ByVal sStr As String) As String
        Try
            If IsNothing(sStr) = False Then
                sStr = sStr.Trim

                sStr = sStr.Replace(" ", "")
                sStr = sStr.Replace("/", "")
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
    Public Class DataArrayItem
        Private sDataText As String
        Private sDataValue As String
        Property DataTextField()
            Get
                Return sDataText
            End Get
            Set(ByVal value)
                sDataText = value
            End Set
        End Property
        Property DataValueField()
            Get
                Return sDataValue
            End Get
            Set(ByVal value)
                sDataValue = value
            End Set
        End Property
    End Class
    Public Function GetDecPathView(ByVal sTemppath As String, ByVal iUserID As Integer, ByVal sIPath As String, ByVal sFileName As String, ByVal sExt As String) As String
        Dim sOPath As String = "", sODPath As String = ""
        Try
            sOPath = sTemppath & "View\" & iUserID & "\"
            sODPath = sTemppath & "View\" & iUserID & "\"
            If System.IO.Directory.Exists(sOPath) = False Then
                System.IO.Directory.CreateDirectory(sOPath)
            End If
            sOPath = sOPath & sFileName & "." & sExt
            sODPath = sODPath & sFileName & "." & sExt

            If File.Exists(sOPath) = False Then
                File.Copy(sIPath, sOPath)
            End If

            Return sOPath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStartDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "SElect YMS_FROMDATE From Year_Master where YMS_YEARID=" & iYearID & " "
            GetStartDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            'sSql = "" : sSql = "Select Datepart(day,'" & objGen.FormatDtForRDBMS(dSDate, "CT") & "')"
            'GetStartDate = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            'GetStartDate = objGen.FormatDtForRDBMS(dSDate, "D")
            Return GetStartDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEndDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        'Dim dEDate As String
        Try
            sSql = "" : sSql = "SElect YMS_TODATE From Year_Master where YMS_YEARID=" & iYearID & " "
            GetEndDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            'sSql = "" : sSql = "Select Datepart(day,'" & objGen.FormatDtForRDBMS(dEDate, "CT") & "')"
            'GetEndDate = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            'GetEndDate = objGen.FormatDtForRDBMS(dEDate, "D")
            Return GetEndDate
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
