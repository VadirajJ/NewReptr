Imports System.Configuration
Imports BusinesLayer
Namespace Components
    Public Class AppException
        Inherits System.ApplicationException
        Public Sub New()
            LogError("", "An unexpected error occurred.", "", "")
        End Sub
        Public Sub New(ByVal message As String)
            LogError("", message, "", "")
        End Sub
        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            LogError("", message, "", "")
            If Not (innerException Is Nothing) Then
                LogError("", innerException.Message.ToString, "", "")
            End If
        End Sub
        Public Shared Sub LogError(ByVal sAccessCode As String, ByVal Message As String, ByVal MyClassName As String, ByVal MyFunctionName As String)
            Dim GMTOffset As Integer, iAccessCodeID As Integer
            Dim sGMTPrefix As String, sErrorDateTime As String, sErrorLogPath As String
            Dim objclsGeneralFunctions As New clsGeneralFunctions
            Try
                iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode.Trim)
                sErrorLogPath = objclsGeneralFunctions.GetGRACeSettingValue(sAccessCode, iAccessCodeID, "ErrorLog")
                'sErrorLogPath = ConfigurationManager.AppSettings("ErrorLog")
                GMTOffset = DateTime.Compare(DateTime.Now, DateTime.UtcNow)
                If GMTOffset > 0 Then
                    sGMTPrefix = "+"
                Else
                    sGMTPrefix = ""
                End If
                sErrorDateTime = DateTime.Now.Year.ToString & "." & DateTime.Now.Month.ToString & "." & DateTime.Now.Day.ToString & " @ " & DateTime.Now.Hour.ToString & ":" &
                    DateTime.Now.Minute.ToString & ":" & DateTime.Now.Second.ToString & " (GMT " & sGMTPrefix & GMTOffset.ToString & ")"

                If System.IO.File.Exists(sErrorLogPath) = False Then
                    System.IO.File.CreateText(sErrorLogPath)
                End If

                Dim MsStreamWriter As New System.IO.StreamWriter(sErrorLogPath, True)
                MsStreamWriter.WriteLine("Date And Time # " & sErrorDateTime)
                MsStreamWriter.WriteLine("Class Name    # " & MyClassName)
                MsStreamWriter.WriteLine("Function Name # " & MyFunctionName)
                MsStreamWriter.WriteLine("Error Message # " & Message)
                MsStreamWriter.WriteLine("##################################################################")
                MsStreamWriter.Close()
            Catch ex As Exception
            End Try
        End Sub
        Public Shared Sub LogError1(ByVal sAccessCode As String, ByVal Message As String, ByVal MyClassName As String, ByVal MyFunctionName As String, ByVal Lineno As String)
            Dim iAccessCodeID As Integer, GMTOffset As Integer
            Dim sErrorLogPath As String, sGMTPrefix As String, sErrorDateTime As String
            Dim objclsGeneralFunctions As New clsGeneralFunctions
            Try
                iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode.Trim)
                sErrorLogPath = objclsGeneralFunctions.GetGRACeSettingValue(sAccessCode, iAccessCodeID, "ErrorLog")
                GMTOffset = DateTime.Compare(DateTime.Now, DateTime.UtcNow)
                If GMTOffset > 0 Then
                    sGMTPrefix = "+"
                Else
                    sGMTPrefix = ""
                End If
                sErrorDateTime = DateTime.Now.Year.ToString & "." & DateTime.Now.Month.ToString & "." & DateTime.Now.Day.ToString & " @ " & DateTime.Now.Hour.ToString & ":" &
                    DateTime.Now.Minute.ToString & ":" & DateTime.Now.Second.ToString & " (GMT " & sGMTPrefix & GMTOffset.ToString & ")"

                If System.IO.File.Exists(sErrorLogPath) = False Then
                    System.IO.File.CreateText(sErrorLogPath)
                End If

                Dim MsStreamWriter As New System.IO.StreamWriter(sErrorLogPath, True)
                MsStreamWriter.WriteLine("Date And Time # " & sErrorDateTime)
                MsStreamWriter.WriteLine("Error line code     # " & Lineno)
                MsStreamWriter.WriteLine("Class Name    # " & MyClassName)
                MsStreamWriter.WriteLine("Function Name # " & MyFunctionName)
                MsStreamWriter.WriteLine("Error Message # " & Message)
                MsStreamWriter.WriteLine("##################################################################")
                MsStreamWriter.Close()
            Catch ex As Exception
            End Try
        End Sub
    End Class
End Namespace