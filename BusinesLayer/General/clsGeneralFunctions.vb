Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports System.Web
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Configuration

Public Class clsGeneralFunctions
    Private objDBL As New DBHelper
    Public Function GetLineNumber(ByVal ex As Exception)
        Dim lineNumber As Int32 = 0
        Const lineSearch As String = ":line "
        Dim index = ex.StackTrace.LastIndexOf(lineSearch)
        If index <> -1 Then
            Dim lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length)
            If Int32.TryParse(lineNumberText, lineNumber) Then
            End If
        End If
        Return lineNumber
    End Function
    Public Function CreateWorkingDir(ByVal sAC As String, ByVal iACID As Integer, ByVal sUserName As String) As String
        Dim objFileInfo As System.IO.FileInfo()
        Dim objDirInfo As DirectoryInfo
        Dim iIndxFiles As Integer
        Dim sSql As String, sGetImgPath As String, sPaths As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where SAD_CompID=" & iACID & " And sad_Config_Key='ExcelPath'"
            sGetImgPath = objDBL.SQLExecuteScalar(sAC, sSql)
            sPaths = sGetImgPath & sUserName
            If Not Directory.Exists(sPaths) Then
                Directory.CreateDirectory(sPaths)
            Else
                objDirInfo = New IO.DirectoryInfo(sPaths)
                objFileInfo = objDirInfo.GetFiles()
                For iIndxFiles = 0 To objFileInfo.Length - 1
                    Try
                        objFileInfo(iIndxFiles).Delete()
                    Catch ex As Exception
                    End Try
                Next
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CreateWorkingDirImg(ByVal sAC As String, ByVal iACID As Integer, ByVal sUserName As String) As String
        Dim objFileInfo As System.IO.FileInfo()
        Dim objDirInfo As DirectoryInfo
        Dim iIndxFiles As Integer
        Dim sSql As String, sGetImgPath As String, sPaths As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where SAD_CompID=" & iACID & " And sad_Config_Key='ImgPath'"
            sGetImgPath = objDBL.SQLExecuteScalar(sAC, sSql)
            sPaths = sGetImgPath & sUserName
            If Not Directory.Exists(sPaths) Then
                Directory.CreateDirectory(sPaths)
            Else
                objDirInfo = New IO.DirectoryInfo(sPaths)
                objFileInfo = objDirInfo.GetFiles()
                For iIndxFiles = 0 To objFileInfo.Length - 1
                    Try
                        objFileInfo(iIndxFiles).Delete()
                    Catch ex As Exception
                    End Try
                Next
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadYear(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Try

            sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_FROMDATE < DATEADD(year,+1,GETDATE()) and YMS_CompId=" & iCompID & " order by YMS_ID desc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadYears(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select YMS_YEARID,substring(YMS_ID,3,2)+ '-' +substring(YMS_ID,8,2) As YMS_ID from YEAR_MASTER where YMS_FROMDATE < DATEADD(year,+1,GETDATE()) and YMS_CompId=" & iCompID & " order by YMS_ID desc"
            '  sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_CompId=" & iCompID & " order by YMS_ID asc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CreateWorkingDirEDCIT(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim objFileInfo As System.IO.FileInfo()
        Dim objDirInfo As DirectoryInfo
        Dim iIndxFiles As Integer
        Dim sSql As String, sGetImgPath As String, sPaths As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where SAD_CompID=" & iACID & " "
            sGetImgPath = objDBL.SQLExecuteScalar(sAC, sSql)
            sPaths = sGetImgPath
            If Not Directory.Exists(sPaths) Then
                Directory.CreateDirectory(sPaths)
            Else
                objDirInfo = New IO.DirectoryInfo(sPaths)
                objFileInfo = objDirInfo.GetFiles()
                For iIndxFiles = 0 To objFileInfo.Length - 1
                    Try
                        objFileInfo(iIndxFiles).Delete()
                    Catch ex As Exception
                    End Try
                Next
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionName(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As String
        Dim sSql As String
        Try
            sSql = "Select ENT_ENTITYName from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndCreateWorkingDirFromPath(ByVal sAC As String, ByVal sGetImgPath As String) As String
        Dim sPaths As String
        Try
            If sGetImgPath.EndsWith("\") = False Then
                sPaths = sGetImgPath & "\"
            Else
                sPaths = sGetImgPath
            End If
            If Not Directory.Exists(sPaths) Then
                Directory.CreateDirectory(sPaths)
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ClearBrowseDirectory(ByVal sBrowse As String)
        Try
            If System.IO.Directory.Exists(sBrowse) = True Then
                Dim files() As String
                files = Directory.GetFileSystemEntries(sBrowse)
                For Each element As String In files
                    If System.IO.File.Exists(element) = True Then
                        Try
                            My.Computer.FileSystem.DeleteFile(System.IO.Path.Combine(sBrowse, System.IO.Path.GetFileName(element)))
                        Catch ex As Exception
                        End Try
                    End If
                Next
            Else
                System.IO.Directory.CreateDirectory(sBrowse)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal sTable As String, ByVal sColumn As String, ByVal sCompColumn As String) As Integer
        Dim sSql As String
        Dim objMax As Object
        Try
            sSql = "Select ISNULL(MAX(" & sColumn & ")+1,1) FROM " & sTable & "  Where " & sCompColumn & "=" & iACID & " "
            objMax = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If Not objMax Is DBNull.Value Then
                Return Integer.Parse(objMax.ToString())
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccessCodeID(ByVal sAC As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select CM_ID from Sad_Company_Master where CM_AccessCode ='" & sAC & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentMonthName(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select DateName(Month,Getdate())"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentMonthID(ByVal sAC As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Datepart(Month,Getdate())"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentDate(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select Convert(Varchar(10),Getdate(),103)"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDefaultYear(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Dim sSql As String
        Dim iYearid As Integer = 0
        Try

            sSql = "Select YMS_YearID from Year_Master where YMS_Default=1 and YMS_CompID=" & iACID & ""
                iYearid = objDBL.SQLExecuteScalarInt(sAC, sSql)

            Return iYearid
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFinancialYearName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            sSql = "Select YMS_ID from Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAddYearTo2DigitFinancialYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iNo As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iDefaultYearID As Integer
        Dim dt As New DataTable
        Try
            sSql = "Select YMS_YearID FROM Year_Master where YMS_default=1 And YMS_CompID=" & iACID & " And YMS_Delflag='A'"
            iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, sSql)

                sSql1 = "Select YMS_ID,YMS_YearID FROM Year_Master where YMS_YearID<=" & iDefaultYearID & "+ " & iNo & " And  YMS_CompID=" & iACID & " And YMS_Delflag='A' ORDER BY YMS_YearID DESC"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql1)

            Return dt
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    Public Function Get4DigitCurrentFinancialYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearid As Integer) As String
        Dim sSql As String, sSql1 As String
        Dim iDefaultYearID As String
        Try
            sSql = "Select '20' +substring(YMS_ID,8,2) from Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearid & ""
            iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iDefaultYearID
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    Public Function Get2DigitFinancialYearName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            '  sSql = "Select YMS_ID from Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearID & " "
            sSql = "Select substring(YMS_ID, 3, 2)+ '-' +substring(YMS_ID,8,2) from Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMonthNameFromMothID(ByVal iMonthID As String) As String
        Dim sMonth As String = ""
        Try
            If iMonthID = 1 Then
                sMonth = "January"
            ElseIf iMonthID = 2 Then
                sMonth = "February"
            ElseIf iMonthID = 3 Then
                sMonth = "March"
            ElseIf iMonthID = 4 Then
                sMonth = "April"
            ElseIf iMonthID = 5 Then
                sMonth = "May"
            ElseIf iMonthID = 6 Then
                sMonth = "June"
            ElseIf iMonthID = 7 Then
                sMonth = "July"
            ElseIf iMonthID = 8 Then
                sMonth = "August"
            ElseIf iMonthID = 9 Then
                sMonth = "September"
            ElseIf iMonthID = 10 Then
                sMonth = "October"
            ElseIf iMonthID = 11 Then
                sMonth = "November"
            ElseIf iMonthID = 12 Then
                sMonth = "December"
            End If
            Return sMonth
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetUserIDFromFullName(ByVal sAC As String, ByVal iACID As Integer, ByVal sUser As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where Usr_FullName='" & sUser & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserIDFromLoginName(ByVal sAC As String, ByVal iACID As Integer, ByVal sUser As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where Usr_LoginName='" & sUser & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserIDFromUserCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where Usr_Code='" & sCode & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserFullNameFromUserID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_FullName from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserLoginNameFromUserID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_LoginName from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserNameAndCodeFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_FullName + ' - ' + Usr_Code From Sad_UserDetails Where Usr_ID=" & iUserID & " And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function GetAllModuleJobCode(ByVal sAC As String, ByVal sModule As String, ByVal iYearID As Integer, ByVal sYearName As String, ByVal iNameID As Integer, ByVal iCustID As Integer) As String
    '    Dim iMaxID As Integer
    '    Dim sMaxID As String = "", sJobCode As String = "", sModuleCode As String = ""
    '    Try
    '        Select Case sModule
    '            Case "AUDIT"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Audit_APM_Details where APM_YearID=" & iYearID & " And APM_APMCRStatus='Submitted' And APM_CustID=" & iCustID & "")
    '                sModuleCode = "AUD"
    '            Case "WORKPAPER"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Audit_WorkPaper where AWP_YearID=" & iYearID & " And AWP_CustID=" & iCustID & "")
    '                sModuleCode = "WP"
    '            Case "LOE"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from SAD_CUST_LOE where LOE_YearId=" & iYearID & "")
    '                sModuleCode = "LOE"
    '            Case "RCSA"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RCSA where RCSA_FinancialYear=" & iYearID & " And RCSA_CustID=" & iCustID & "")
    '                sModuleCode = "RCSA"
    '            Case "RA"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RA where RA_FinancialYear=" & iYearID & " And RA_CustID=" & iCustID & "")
    '                sModuleCode = "RA"
    '            Case "FRR"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RRF_PlanningSchecduling_Details where RPD_YearID=" & iYearID & " And RPD_CustID=" & iCustID & "")
    '                sModuleCode = "FRR"
    '            Case "KCC"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_KCC_PlanningSchecduling_Details where KCC_YearID=" & iYearID & " And KCC_CustID=" & iCustID & "")
    '                sModuleCode = "KCC"
    '            Case "BRR"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_BRRSchedule where BRRS_FinancialYear=" & iYearID & " And BRRS_CustID=" & iCustID & "")
    '                sModuleCode = "BRR"
    '            Case "COMPLIANCE"
    '                iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select CP_ID from Compliance_Plan where CP_YearID=" & iYearID & " and CP_ID=" & iNameID & " And CP_CustomerID=" & iCustID & "")
    '                sModuleCode = "Compliance"
    '        End Select
    '        If iMaxID = 0 Then
    '            sMaxID = "001"
    '        ElseIf iMaxID > 0 And iMaxID < 10 Then
    '            sMaxID = "00" & iMaxID
    '        ElseIf iMaxID >= 10 And iMaxID < 100 Then
    '            sMaxID = "0" & iMaxID
    '        Else
    '            sMaxID = iMaxID
    '        End If
    '        sJobCode = "TRACe/" & sModuleCode & "/" & sYearName & "/" & sMaxID
    '        Return sJobCode
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetAllModuleJobCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sModule As String, ByVal iYearID As Integer, ByVal sYearName As String, ByVal iCustID As Integer) As String
        Dim iMaxID As Integer
        Dim sMaxID As String = "", sJobCode As String = "", sModuleCode As String = ""
        Try
            Select Case sModule
                Case "AUDIT"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from StandardAudit_Schedule where SA_YearID=" & iYearID & "")
                    sModuleCode = "AUD"
                Case "WORKPAPER"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Audit_WorkPaper where AWP_YearID=" & iYearID & "")
                    sModuleCode = "WP"
                Case "LOE"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from SAD_CUST_LOE where LOE_YearId=" & iYearID & "")
                    sModuleCode = "LOE"
                Case "RCSA"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RCSA where RCSA_FinancialYear=" & iYearID & "")
                    sModuleCode = "RCSA"
                Case "RA"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RA where RA_FinancialYear=" & iYearID & "")
                    sModuleCode = "RA"
                Case "FRR"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RRF_PlanningSchecduling_Details where RPD_YearID=" & iYearID & "")
                    sModuleCode = "FRR"
                Case "KCC"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_KCC_PlanningSchecduling_Details where KCC_YearID=" & iYearID & "")
                    sModuleCode = "KCC"
                Case "BRR"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_BRRSchedule where BRRS_FinancialYear=" & iYearID & "")
                    sModuleCode = "BRR"
                Case "COMPLIANCE"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Compliance_Plan where CP_YearID=" & iYearID & " And CP_IsCurrentYear=1 And CP_PlanStatus='Submitted' And (CP_ScheduleStatus='Submitted' Or CP_ScheduleStatus='Saved')")
                    sModuleCode = "Compliance"
                Case "ASG"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from AuditAssignment_Schedule where AAS_YearID=" & iYearID & "")
                    sModuleCode = "ASG"
                Case "ITR"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from ITReturnsFiling_Details where ITRFD_FinancialYearID=" & iYearID & "")
            End Select
            If iMaxID = 0 Then
                sMaxID = "00001"
            ElseIf iMaxID > 0 And iMaxID < 10 Then
                sMaxID = "0000" & iMaxID
            ElseIf iMaxID >= 10 And iMaxID < 100 Then
                sMaxID = "000" & iMaxID
            ElseIf iMaxID >= 100 And iMaxID < 1000 Then
                sMaxID = "00" & iMaxID
            ElseIf iMaxID >= 1000 And iMaxID < 10000 Then
                sMaxID = "0" & iMaxID
            Else
                sMaxID = iMaxID
            End If
            If sModule = "ITR" Then
                sJobCode = "ITR/" & sYearName & "/" & sMaxID
            Else
                Dim sPrefix As String = GetCustomerCode(sAC, iACID, iCustID)
                sJobCode = sPrefix & "/" & sModuleCode & "/" & sYearName & "/" & sMaxID
            End If
            Return sJobCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CUST_CODE from sad_Customer_master where Cust_ID=" & iCustID & " And Cust_CompID=" & iACID & " "
            Return Regex.Replace(objDBL.SQLExecuteScalar(sAC, sSql), "\s", "").Trim()
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyName(ByVal sAC As String, ByVal iACID As Integer, ByVal iCompanyID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Company_Name from Trace_CompanyDetails where Company_ID=" & iCompanyID & " And Company_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerName(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CUst_name from sad_Customer_master where Cust_ID=" & iCustID & " And Cust_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditNO(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select APM_AuditCode from Audit_APM_Details where APM_ID=" & iAuditID & " And APM_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveGRACeFormOperations(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sModule As String, ByVal sForm As String, ByVal sEvent As String,
                                       ByVal iMasterID As Integer, ByVal sMasterName As String, ByVal iSubMasterID As Integer, ByVal sSubMasterName As String, ByVal sIPAddress As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_UserID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iUserID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Module", OleDb.OleDbType.VarChar, 50)
            ObjParam(iRCSADParamCount).Value = sModule
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Form", OleDb.OleDbType.VarChar, 500)
            ObjParam(iRCSADParamCount).Value = sForm
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Event", OleDb.OleDbType.VarChar, 500)
            ObjParam(iRCSADParamCount).Value = sEvent
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iMasterID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_MasterName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iRCSADParamCount).Value = sMasterName
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_SubMasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iSubMasterID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_SubMasterName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iRCSADParamCount).Value = sSubMasterName
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iRCSADParamCount).Value = sIPAddress
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iACID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "spAudit_Log_Form_Operations", ObjParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveUserLogOperations(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sLoginUserName As String, ByVal sLogType As String, ByVal sIPAddress As String, ByVal sPassword As String)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(MAX(ALP_PKID ) + 1,1) From audit_log_operations")
            sSql = "Insert Into Audit_Log_Operations (ALP_PKID,ALP_UserName,ALP_UserID,ALP_Password,ALP_Date,ALP_LogType,ALP_IPAddress,ALP_CompID )"
            sSql = sSql & "Values(" & iMaxID & ",'" & sLoginUserName & "'," & iUserID & ",'" & sPassword & "',GetDate(),'" & sLogType & "','" & sIPAddress & "'," & iACID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadColors(ByVal sAc As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select TC_Color_Name,TC_KeyCode from SAD_Color_Master Where TC_CompID=" & iACID & " "
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetColorNameFromPKID(ByVal sAc As String, ByVal iAcID As Integer, iColor As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select TC_Color_Name From SAD_Color_Master where TC_CompID=" & iAcID & " And  TC_KeyCode=" & iColor & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetColorIDFromName(ByVal sAc As String, ByVal iAcID As Integer, ByVal sColors As String) As String
        Dim sSql As String
        Try
            sSql = "Select TC_KeyCode from SAD_Color_Master Where Upper(TC_Color_Name)=Upper('" & sColors & "') and TC_CompID=" & iAcID & " "
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDesignatedUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrDesgination As Integer, ByVal sSearchUser As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select usr_id,usr_LoginName,usr_FullName from sad_userdetails Where Usr_Designation=" & iUsrDesgination & " and (Usr_DutyStatus='A' Or Usr_DutyStatus='L' And Usr_DutyStatus='B')"
            If sSearchUser <> "" Then
                sSql = sSql & " And usr_FullName like '" & sSearchUser & "%'"
            End If
            sSql = sSql & " order by usr_FullName Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As String
        Dim sSql As String
        Try
            sSql = "Select ENT_ENTITYName from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As String) As String
        Dim sSql As String
        Try
            sSql = "Select SEM_NAME from MST_SUBENTITY_MASTER where SEM_compid=" & iACID & " And SEM_ID=" & iSubFunID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetProcessNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iProID As String) As String
        Dim sSql As String
        Try
            sSql = "Select PM_NAME from MST_PROCESS_MASTER where PM_COMPID=" & iACID & " And PM_ID=" & iProID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionOwnerHODIDFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Ent_FunOwnerID from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFunctionOwnerHODFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (Select ENT_FunownerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionOwnerHODNameFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USr_FullName from sad_userdetails  where usr_compID=" & iACID & "  and usr_ID in (Select ENT_FunownerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionManagerIDFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Ent_FunManagerID from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionManagerNameFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USr_FullName from sad_userdetails  where usr_compID=" & iACID & "  and usr_ID in (Select Ent_FunManagerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFunctionManagersFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (Select Ent_FunManagerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionSPOCIDFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Ent_FunSPOCID from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionSPOCNameFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USr_FullName from sad_userdetails where usr_compID=" & iACID & "  and usr_ID in (Select Ent_FunSPOCID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFunctionSPOCsFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (Select Ent_FunSPOCID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFUNOwnerHODManagerSPOCIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String = "", sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Convert(Varchar(10),ENT_FunownerID) + ',' +  Convert(Varchar(10),Ent_FunManagerID) + ',' +  Convert(Varchar(10),Ent_FunSPOCID) from mst_Entity_master where ENT_ID=" & iFunID & " And ENT_CompID=" & iACID & ""
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                sStr = objDBL.SQLExecuteScalar(sAC, sSql)
                If sStr <> "" Then
                    sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (" & sStr & ")"
                    dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                End If
            Else
                sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID=0"
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllUsers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_Id,(Usr_Fullname +' - ' + Usr_Code) as Usr_Fullname from sad_userdetails Where Usr_CompId=" & iACID & " order by Usr_Fullname"
            Return objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserEMailFromID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select usr_Email from sad_userdetails where usr_category=1 and usr_ID=" & iUserID & " and Usr_CompID =" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFunctionSPOCHODManagerFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iFunOwnerID As Integer = 0, iSPOCID As Integer = 0, iManagerID As Integer = 0
        Dim sIDs As String
        Dim dtIDs As New DataTable, dtUSers As New DataTable
        Try
            sSql = "select ENT_FunOwnerID,ENT_FunManagerID,ENT_FunSPOCID from mst_entity_master where ENT_ID=" & iFunctionID & " and ENT_CompID=" & iACID & ""
            dtIDs = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtIDs.Rows.Count > 0 Then
                If IsDBNull(dtIDs.Rows(0)("ENT_FunOwnerID")) = False Then
                    iFunOwnerID = dtIDs.Rows(0)("ENT_FunOwnerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunManagerID")) = False Then
                    iManagerID = dtIDs.Rows(0)("ENT_FunManagerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunSPOCID")) = False Then
                    iSPOCID = dtIDs.Rows(0)("ENT_FunSPOCID")
                End If
                sIDs = iFunOwnerID & "," & iManagerID & "," & iSPOCID

                sSql1 = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as Name from sad_userdetails where USr_ID In (" & sIDs & ") And Usr_CompID = " & iACID & " order by Usr_FullName"
                dtUSers = objDBL.SQLExecuteDataTable(sAC, sSql1)
            End If
            Return dtUSers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFunctionSPOCHODManagerWithEmailFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iFunOwnerID As Integer = 0, iSPOCID As Integer = 0, iManagerID As Integer = 0
        Dim sIDs As String
        Dim dtIDs As New DataTable, dtUSers As New DataTable
        Try
            sSql = "select ENT_FunOwnerID,ENT_FunManagerID,ENT_FunSPOCID from mst_entity_master where ENT_ID=" & iFunctionID & " and ENT_CompID=" & iACID & ""
            dtIDs = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtIDs.Rows.Count > 0 Then
                If IsDBNull(dtIDs.Rows(0)("ENT_FunOwnerID")) = False Then
                    iFunOwnerID = dtIDs.Rows(0)("ENT_FunOwnerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunManagerID")) = False Then
                    iManagerID = dtIDs.Rows(0)("ENT_FunManagerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunSPOCID")) = False Then
                    iSPOCID = dtIDs.Rows(0)("ENT_FunSPOCID")
                End If
                sIDs = iFunOwnerID & "," & iManagerID & "," & iSPOCID

                sSql1 = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as Name from sad_userdetails where USr_ID In (" & sIDs & ") And usr_category=1 and Usr_Email like '%@%' And Usr_Email like '%.%' And Usr_CompID = " & iACID & " order by Usr_FullName  "
                dtUSers = objDBL.SQLExecuteDataTable(sAC, sSql1)
            End If
            Return dtUSers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllRiskAuditTeamMembers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID, USr_fullName from Sad_UserDetails Where Usr_CompID=" & iACID & " And usr_category=1 and Usr_Email like '%@%' And Usr_Email like '%.%' "
            sSql = sSql & " And ((Usr_RiskModule=1 And Usr_RiskRole in (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' "
            sSql = sSql & " And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK') "
            sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK'))))"
            sSql = sSql & " or (Usr_AuditModule=1 And Usr_AuditRole in (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' "
            sSql = sSql & " And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='AUD') "
            sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='AUD')))))"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllRiskComplianceTeamMembers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID, USr_fullName from Sad_UserDetails Where Usr_CompID=" & iACID & " And usr_category=1 and Usr_Email like '%@%' And Usr_Email like '%.%' "
            sSql = sSql & " And ((Usr_RiskModule=1 And Usr_RiskRole in (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' "
            sSql = sSql & " And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK') "
            sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK'))))"
            sSql = sSql & " or (usr_complianceModule=1 And Usr_ComplianceRole in (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' "
            sSql = sSql & " And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='COMP') "
            sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='COMP')))))"

            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllRiskTeamWithEmail(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID, (Usr_FullName + ' - ' + Usr_Code) as Name from Sad_UserDetails Where Usr_CompID=" & iACID & " And Usr_CompanyID=0 And usr_category=1 and Usr_Email like '%@%' And Usr_Email like '%.%' And Usr_RiskModule=1 And Usr_RiskRole in "
            sSql = sSql & " (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK') "
            sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK'))) order by Usr_FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlLibraryID(ByVal sAC As String, ByVal iACID As Integer, ByVal sControlName As String, ByVal sCheckIsKey As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MCL_PKID From MST_Control_Library Where MCl_CompID=" & iACID & " And Upper(MCL_ControlName)=Upper('" & sControlName & "')"
            If sCheckIsKey = "YES" Then
                sSql = sSql & " And MCL_IsKey=1 "
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskLibraryID(ByVal sAC As String, ByVal iACID As Integer, ByVal sRiskLibrary As String, ByVal sCheckIsKey As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select MRL_PKID From MST_RISK_Library Where MRL_CompID=" & iACID & " And Upper(MRL_RiskName)=Upper('" & sRiskLibrary & "')"
            If sCheckIsKey = "YES" Then
                sSql = sSql & " And MRL_IsKey=1 "
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUsersFromDesignationID(ByVal sAC As String, ByVal iACID As Integer, ByVal iDesgination As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select Usr_id,Usr_FullName from Sad_Userdetails  Where Usr_LevelGrp=" & iDesgination & " And Usr_DelFlag= 'A' And Usr_COmpID=" & iACID & " Order By Usr_FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchesNameFromCustID(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Mas_Description from SAD_CUST_LOCATION where Mas_ID =" & iBranchID & " and Mas_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRoleNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iMasID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Mas_Description from SAD_GrpOrLvl_General_Master where Mas_ID=" & iMasID & " and Mas_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGeneralMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try

            sSql = "Select cmm_ID,cmm_Desc from Content_Management_Master where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' order by cmm_Desc Asc"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadComplianceTask(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select cmm_ID,cmm_Desc from Content_Management_Master where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' and cms_KeyComponent=1 order by cmm_Desc Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadNonComplianceOrAssignmentTask(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select cmm_ID,cmm_Desc from Content_Management_Master where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' and cms_KeyComponent=0 order by cmm_Desc Asc"
            'sSql = "Select cmm_ID,cmm_Desc from Content_Management_Master where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' order by cmm_Desc Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRiskGeneralMasters(ByVal sAc As String, ByVal iAcID As Integer, ByVal SMasterID As String, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name From Risk_GeneralMaster Where RAM_DelFlag ='A' and RAM_Category='" & SMasterID & "' And RAM_YearID=" & iYearID & " and RAM_CompID=" & iAcID & " Order By RAM_Name"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGRACeOverAllFunctionRatingDetails(ByVal sAC As String, ByVal iPKID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer,
                                                  ByVal dRANetScore As Double, ByVal iRANetRatingID As Integer, ByVal dIANetScore As Double, ByVal iIANetRatingID As Integer,
                                                  ByVal sType As String, ByVal iCrBy As Integer, ByVal sIPAddress As String, ByVal iACID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_YearID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_CustID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_FunID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_SubFunID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iSubFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_RANetScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dRANetScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_RANetRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iRANetRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_IANetScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dIANetScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_IAMNetRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iIANetRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_FormName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = sType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spGRACe_OverallFunctionRating_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function FormatMyDate(ByVal sDate As String)
        Dim sFromDte As String = ""
        Dim sArry() As String
        Dim i As Int16
        Dim iValue As String
        Try
            If Len(Trim(sDate)) <> 0 Then
                sArry = Split(sDate, "/")
                If sArry.Length = 1 Then
                    FormatMyDate = String.Empty
                    Exit Try
                End If
                For i = 0 To UBound(sArry)
                    If Len(Trim(sArry(i))) = 1 Then
                        iValue = "0" & sArry(i)
                    Else
                        iValue = sArry(i)
                    End If

                    Select Case i
                        Case 0
                            sFromDte = iValue
                        Case 1
                            sFromDte = iValue & "/" & sFromDte
                        Case 2
                            sFromDte = sFromDte & "/" & iValue
                    End Select
                Next
            End If
            Return sFromDte
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTempPath(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As String
        Dim sSql As String = "", sValue As String = ""
        Dim dt As New DataTable
        Try
            '05/01/2023
            ' sSql = "" : sSql = "Select SET_Value from edt_Settings where SET_CompID =" & iCompID & " and Set_Code = '" & sCode & "'"
            sSql = "" : sSql = "Select SAD_Config_Value from sad_config_settings where SAD_CompID =" & iCompID & " And SAD_Config_Key= '" & sCode & "'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sValue = dt.Rows(0)(0).ToString()
            End If
            Return sValue
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer) As String
        Dim sSql As String
        Try
            sSql = "select org_Name from sad_org_structure where org_node=" & iBranchID & " and org_CompID=" & iACID & " and Org_LevelCode=4 And Org_DelFlag='A'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGRACeOverAllBranchRatingDetails(ByVal sAC As String, ByVal iPKID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iBranchID As Integer, ByVal iBRRCoreProcessRatingID As Integer, ByVal dBRRCoreProcessScore As Double,
                                                    ByVal iBRRSupportProcessRatingID As Integer, ByVal dBRRSupportProcessScore As Double, ByVal iBRRNetRatingID As Integer, ByVal dBRRNetScore As Double,
                                                    ByVal iBACoreProcessRatingID As Integer, ByVal dBACoreProcessScore As Double, ByVal iBASupportProcessRatingID As Integer, ByVal dBASupportProcessScore As Double,
                                                    ByVal iBANetRatingID As Integer, ByVal dBANetScore As Double, ByVal iBCMCoreProcessRatingID As Integer, ByVal dBCMCoreProcessScore As Double,
                                                    ByVal iBCMSupportProcessRatingID As Integer, ByVal dBCMSupportProcessScore As Double, ByVal iBCMNetRatingID As Integer, ByVal dBCMNetScore As Double,
                                                    ByVal sType As String, ByVal iCrBy As Integer, ByVal sIPAddress As String, ByVal iACID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_YearID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_CustID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BranchID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BRRCoreProcessRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBRRCoreProcessRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BRRCoreProcessScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBRRCoreProcessScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BRRSupportProcessRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBRRSupportProcessRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BRRSupportProcessScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBRRSupportProcessScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BRRNetRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBRRNetRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BRRNetScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBRRNetScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BACoreProcessRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBACoreProcessRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BACoreProcessScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBACoreProcessScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BASupportProcessRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBASupportProcessRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BASupportProcessScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBASupportProcessScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BANetRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBANetRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BANetScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBANetScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BCMCoreProcessRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBCMCoreProcessRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BCMCoreProcessScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBCMCoreProcessScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BCMSupportProcessRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBCMSupportProcessRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BCMSupportProcessScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBCMSupportProcessScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BCMNetRatingID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iBCMNetRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_BCMNetScore", OleDb.OleDbType.Double, 10)
            ObjParam(iParamCount).Value = dBCMNetScore
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_FormName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = sType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GOD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spGRACe_OverallBranchRating_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iZoneID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Name from sad_org_Structure where org_node=" & iZoneID & " And Org_LevelCode=1 And Org_DelFlag='A' and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRegionNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iRegionID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Name from sad_org_Structure where org_node=" & iRegionID & " And Org_LevelCode=2 And Org_DelFlag='A' and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllZOMs(ByVal sAC As String, ByVal iACID As Integer, ByVal iZoneID As Integer) As DataTable
        Dim sSql As String, sSqlSub As String
        Dim dt As New DataTable
        Dim sRegionIDs As String = "", sAreaIDs As String = "", sBranchIDs As String = ""
        Dim dtTab As New DataTable
        Try
            sSql = "Select usr_ID,usr_fullname from sad_USERDETAILS "
            If iZoneID > 0 Then
                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent=" & iZoneID & " And Org_LevelCode=2 And Org_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                For i = 0 To dtTab.Rows.Count - 1
                    sRegionIDs = sRegionIDs & "," & dtTab.Rows(i)("Org_Node")
                Next
                If sRegionIDs.StartsWith(",") = True Then
                    sRegionIDs = sRegionIDs.Remove(0, 1)
                End If
                If sRegionIDs.EndsWith(",") = True Then
                    sRegionIDs = sRegionIDs.Remove(Len(sRegionIDs) - 1, 1)
                End If
                If sRegionIDs <> "" Then
                    dtTab = Nothing
                    sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sRegionIDs & ") And Org_LevelCode=3 And Org_CompID=" & iACID & ""
                    dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                    For i = 0 To dtTab.Rows.Count - 1
                        sAreaIDs = sAreaIDs & "," & dtTab.Rows(i)("Org_Node")
                    Next
                    If sAreaIDs.StartsWith(",") = True Then
                        sAreaIDs = sAreaIDs.Remove(0, 1)
                    End If
                    If sAreaIDs.EndsWith(",") = True Then
                        sAreaIDs = sAreaIDs.Remove(Len(sAreaIDs) - 1, 1)
                    End If
                End If

                If sAreaIDs <> "" Then
                    dtTab = Nothing
                    sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sAreaIDs & ") And Org_LevelCode=4 And Org_CompID=" & iACID & ""
                    dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                    For i = 0 To dtTab.Rows.Count - 1
                        sBranchIDs = sBranchIDs & "," & dtTab.Rows(i)("Org_Node")
                    Next
                    If sBranchIDs <> "" Then
                        If sBranchIDs.StartsWith(",") = True Then
                            sBranchIDs = sBranchIDs.Remove(0, 1)
                        End If
                        If sBranchIDs.EndsWith(",") = True Then
                            sBranchIDs = sBranchIDs.Remove(Len(sBranchIDs) - 1, 1)
                        End If
                    End If
                End If

                sSql = sSql & " WHERE Usr_OrgnID In (" & iZoneID & ""
                If sRegionIDs <> "" Then
                    sSql = sSql & "," & sRegionIDs & ""
                End If
                If sAreaIDs <> "" Then
                    sSql = sSql & " ," & sAreaIDs & ""
                End If
                If sBranchIDs <> "" Then
                    sSql = sSql & "," & sBranchIDs & ""
                End If
                sSql = sSql & ") And "
            End If
            sSql = sSql & " Usr_CompID=" & iACID & " And Usr_designation In(Select mas_ID from SAD_GRPDESGN_General_Master  where (mas_Code=Lower('ZOM') or mas_Code=Upper('ZOM')) and mas_compID=" & iACID & ") order by usr_fullname"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iRegionID As Integer) As DataTable
        Dim sSql As String = "", sSqlSub As String
        Dim sAreaIDs As String = "", sBranchIDs As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dt.Columns.Add("Org_Node")
            dt.Columns.Add("org_name")

            If iRegionID > 0 Then
                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & iRegionID & ") And Org_LevelCode=3 And Org_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                For i = 0 To dtTab.Rows.Count - 1
                    sAreaIDs = sAreaIDs & "," & dtTab.Rows(i)("Org_Node")
                Next
                If sAreaIDs.StartsWith(",") = True Then
                    sAreaIDs = sAreaIDs.Remove(0, 1)
                End If
                If sAreaIDs.EndsWith(",") = True Then
                    sAreaIDs = sAreaIDs.Remove(Len(sAreaIDs) - 1, 1)
                End If
            End If

            If sAreaIDs <> "" Then
                dtTab = Nothing
                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sAreaIDs & ") And Org_LevelCode=4 And Org_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                For i = 0 To dtTab.Rows.Count - 1
                    sBranchIDs = sBranchIDs & "," & dtTab.Rows(i)("Org_Node")
                Next
                If sBranchIDs <> "" Then
                    If sBranchIDs.StartsWith(",") = True Then
                        sBranchIDs = sBranchIDs.Remove(0, 1)
                    End If
                    If sBranchIDs.EndsWith(",") = True Then
                        sBranchIDs = sBranchIDs.Remove(Len(sBranchIDs) - 1, 1)
                    End If
                    sSql = "Select Org_Code +' - '+ Org_Name As Org_Name,Org_Node from Sad_Org_Structure Left Join Risk_BRRPlanning On BRRP_BranchID=Org_Node"
                    sSql = sSql & " where Org_Node in (" & sBranchIDs & ") And BRRP_Status='S' And Org_CompID=" & iACID & ""
                End If
            End If

            If iRegionID = 0 Or sAreaIDs = "" Or sBranchIDs = "" Then
                sSql = "Select Org_Code +' - '+ Org_Name As Org_Name,Org_Node from Sad_Org_Structure Left Join Risk_BRRPlanning On BRRP_BranchID=Org_Node"
                sSql = sSql & " where Org_LevelCode=4 And BRRP_Status='S' And Org_CompID=" & iACID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranchManager(ByVal sAC As String, ByVal iACID As Integer, ByVal sBranchID As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select usr_ID, usr_fullname from sad_USERDETAILS WHERE "
            If sBranchID > 0 Then
                sSql = sSql & " Usr_OrgnID In('" & sBranchID & "') And "
            End If
            sSql = sSql & "usr_compID=" & iACID & " And usr_designation In(Select mas_ID from SAD_GRPDESGN_General_Master  where mas_Code=Lower('BM') or mas_Code=Upper('BM') and mas_compID=" & iACID & ") order by usr_fullname"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Function GetEdictMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal sTable As String, ByVal sColumn As String) As Integer
        Dim sSql As String
        Dim objMax As Object
        Try
            sSql = "Select ISNULL(MAX(" & sColumn & ")+1,1) FROM " & sTable & " "
            objMax = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If Not objMax Is DBNull.Value Then
                Return Integer.Parse(objMax.ToString())
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocId As Integer, ByVal sMandatory As String) As DataTable
        Dim sSql As String
        Dim dtDecs As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow

        dtDecs.Columns.Add("DescId")
        dtDecs.Columns.Add("Descriptor")
        dtDecs.Columns.Add("DataType")
        dtDecs.Columns.Add("Size")
        dtDecs.Columns.Add("Mandatory")
        dtDecs.Columns.Add("Values")
        dtDecs.Columns.Add("Validator")
        Try
            sSql = "Select EDD_DPTRID,EDD_SIZE,EDD_ISREQUIRED,EDD_VALUES,EDD_Validate,DESC_Name,Dt_Name From EDT_DOCTYPE_LINK"
            sSql = sSql & " Left Join EDT_DESCRIPTIOS On DES_ID=EDD_DPTRID Left Join EDT_DESC_TYPE On DT_ID=DESC_DATATYPE"
            sSql = sSql & " Where EDD_DOCTYPEID=" & iDocId & ""
            If sMandatory = "Y" Then
                sSql = sSql & " And EDD_ISREQUIRED='Y'"
            End If
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dtDecs.NewRow
                    If IsDBNull(dtdetails.Rows(i)("EDD_DPTRID")) = False Then
                        dRow("DescId") = dtdetails.Rows(i)("EDD_DPTRID")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_Name")) = False Then
                        dRow("Descriptor") = dtdetails.Rows(i)("DESC_Name")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("Dt_Name")) = False Then
                        dRow("DataType") = dtdetails.Rows(i)("Dt_Name")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_SIZE")) = False Then
                        dRow("Size") = dtdetails.Rows(i)("EDD_SIZE")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_ISREQUIRED")) = False Then
                        If dtdetails.Rows(i)("EDD_ISREQUIRED") = "Q" Then
                            dRow("Mandatory") = "N"
                        Else
                            dRow("Mandatory") = "Y"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_VALUES")) = False Then
                        dRow("Values") = dtdetails.Rows(i)("EDD_VALUES")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_Validate")) = False Then
                        If dtdetails.Rows(i)("EDD_Validate") = "N" Then
                            dRow("Validator") = "N"
                        Else
                            dRow("Validator") = "Y"
                        End If
                    End If
                    dtDecs.Rows.Add(dRow)
                Next
            End If
            Return dtDecs
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentTime(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select Convert(Varchar(10),Getdate(),108)"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndCreateWorkingDirFromPath(ByVal sImagePath As String) As String
        Dim sPaths As String
        Try
            If sImagePath.EndsWith("\") = False Then
                sPaths = sImagePath & "\"
            Else
                sPaths = sImagePath
            End If
            If Not Directory.Exists(sPaths) Then
                Directory.CreateDirectory(sPaths)
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetFileExt(ByVal sAC As String, ByVal iACID As Integer, ByVal iFile As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select PGE_Ext from edt_page where pge_basename = " & iFile & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Sub SaveViewAndDownloadLogs(ByVal sAC As String, ByVal iACID As Integer, ByVal sOperation As String, ByVal iPageBaseId As Integer,
                                  ByVal iVersion As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)

        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(5) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@PVD_LogOperation", OleDb.OleDbType.VarChar, 50)
            ObjParam(iRCSADParamCount).Value = sOperation
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@PVD_PageBaseID", OleDb.OleDbType.Integer)
            ObjParam(iRCSADParamCount).Value = iPageBaseId
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@PVD_Version", OleDb.OleDbType.Integer)
            ObjParam(iRCSADParamCount).Value = iVersion
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@PVD_UserId", OleDb.OleDbType.Integer)
            ObjParam(iRCSADParamCount).Value = iUserID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@PVD_Ipaddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iRCSADParamCount).Value = sIPAddress
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@PVD_CompId", OleDb.OleDbType.Integer)
            ObjParam(iRCSADParamCount).Value = iACID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "spEDT_PAGE_ViewAndDownloadlogs", ObjParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetEDICTSettingValue(ByVal sAC As String, ByVal iACID As Integer, ByVal sKey As String) As String
        Dim sSql As String
        Try
            sSql = "Select SET_Value from EDT_Settings where SET_CODE='" & sKey & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGRACeSettingValue(ByVal sAC As String, ByVal iACID As Integer, ByVal sKey As String) As String
        Dim sSql As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key='" & sKey & "' and sad_compid=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAddGetAccessCodeID(ByVal sAccessCode As String, ByVal sServerPath As String) As Integer
        Dim sSql As String
        Dim sPath As String
        Dim iMaxID As Integer = 0
        Try
            If objDBL.SQLExecuteScalarInt(sAccessCode, "SELECT Count(*) FROM Sad_Company_Master") = 0 Then
                iMaxID = objDBL.SQLExecuteScalarInt(sAccessCode, "Select ISNULL(MAX(CM_ID ) + 1,1) From Sad_Company_Master")
                sSql = "Insert Into Sad_Company_Master(CM_ID,CM_AccessCode,CM_CompanyName,CM_DelFlag,CM_CreatedBy,CM_CreatedOn) Values "
                sSql = sSql & "(1,'" & sAccessCode & "','" & sAccessCode & "','X',1,GETDATE())"
                objDBL.SQLExecuteNonQuery(sAccessCode, sSql)

                sPath = sServerPath & sAccessCode & "\"
                sSql = "" : sSql = "Update sad_Config_Settings set SAD_Config_Value='" & sPath & "' Where SAD_Config_Key='ImgPath' And SAD_CompID=" & iMaxID & ""
                objDBL.SQLExecuteNonQuery(sAccessCode, sSql)

                sPath = sServerPath & sAccessCode & "\TRACePA Doc"
                sSql = "" : sSql = "Update sad_Config_Settings set SAD_Config_Value='" & sPath & "' Where SAD_Config_Key='FileInDBPath' And SAD_CompID=" & iMaxID & ""
                objDBL.SQLExecuteNonQuery(sAccessCode, sSql)

                sPath = sServerPath & sAccessCode & "\Tempfolder\"
                sSql = "" : sSql = "Update sad_Config_Settings set SAD_Config_Value='" & sPath & "' Where SAD_Config_Key='ExcelPath' And SAD_CompID=" & iMaxID & ""
                objDBL.SQLExecuteNonQuery(sAccessCode, sSql)
            Else
                sSql = "Select CM_ID from Sad_Company_Master where CM_AccessCode ='" & sAccessCode & "'"
                iMaxID = objDBL.SQLExecuteScalarInt(sAccessCode, sSql)
            End If

            If objDBL.SQLExecuteScalarInt(sAccessCode, "SELECT Count(*) FROM sad_org_structure") = 1 Then
                sSql = "" : sSql = "Insert Into sad_org_structure(org_node,org_Code,org_name,org_parent,org_DelFlag,org_Note,org_AppStrength,org_AppBy,org_AppOn,org_CreatedBy,org_CreatedOn,org_Status,Org_levelCode,Org_CompID,Org_IPAddress) "
                sSql = sSql & " VALUES (2, 'ZONE', '" & sAccessCode & "', 1, 'A', '" & sAccessCode & "', 0, 1, GETDATE(), 1, GETDATE(), 'A', 1, 1, '192.168.0.111')"
                objDBL.SQLExecuteNonQuery(sAccessCode, sSql)

                sSql = "" : sSql = "Insert Into sad_org_structure(org_node,org_Code,org_name,org_parent,org_DelFlag,org_Note,org_AppStrength,org_AppBy,org_AppOn,org_CreatedBy,org_CreatedOn,org_Status,Org_levelCode,Org_CompID,Org_IPAddress) "
                sSql = sSql & " VALUES (3, 'REGION', '" & sAccessCode & "', 2, 'A', '" & sAccessCode & "', 0, 1, GETDATE(), 1, GETDATE(), 'A', 2, 1, '192.168.0.111')"
                objDBL.SQLExecuteNonQuery(sAccessCode, sSql)

                sSql = "" : sSql = "Insert Into sad_org_structure(org_node,org_Code,org_name,org_parent,org_DelFlag,org_Note,org_AppStrength,org_AppBy,org_AppOn,org_CreatedBy,org_CreatedOn,org_Status,Org_levelCode,Org_CompID,Org_IPAddress) "
                sSql = sSql & " VALUES (4, 'AREA', '" & sAccessCode & "', 3, 'A', '" & sAccessCode & "', 0, 1, GETDATE(), 1, GETDATE(), 'A', 3, 1, '192.168.0.111')"
                objDBL.SQLExecuteNonQuery(sAccessCode, sSql)
            End If
            'If objDBL.SQLExecuteScalarInt(sAccessCode, "SELECT Count(*) FROM edt_document_type") = 0 Then
            '    sSql = "" : sSql = "Insert Into edt_document_type(DOT_DOCTYPEID,DOT_DOCNAME,DOT_NOTE,DOT_PGROUP,DOT_CRBY,DOT_CRON,DOT_STATUS,dot_operation,dot_operationby,DOT_isGlobal,DOT_DelFlag,DOT_APPROVEDBY,DOT_APPROVEDON,DOT_CompId,DOT_IPAddress) "
            '    sSql = sSql & " VALUES (1, 'Assignment Attachments', 'Assignment Attachments', 4, 1, GETDATE(), 'A', 'I', 1, 0, 'A', 1, GETDATE(), 1, '192.168.0.111')"
            '    objDBL.SQLExecuteNonQuery(sAccessCode, sSql)

            '    sSql = "" : sSql = "Insert Into edt_document_type(DOT_DOCTYPEID,DOT_DOCNAME,DOT_NOTE,DOT_PGROUP,DOT_CRBY,DOT_CRON,DOT_STATUS,dot_operation,dot_operationby,DOT_isGlobal,DOT_DelFlag,DOT_APPROVEDBY,DOT_APPROVEDON,DOT_CompId,DOT_IPAddress) "
            '    sSql = sSql & " VALUES (2, 'Attachments', 'Attachments', 4, 1, GETDATE(), 'A', 'I', 1, 0, 'A', 1, GETDATE(), 1, '192.168.0.111')"
            '    objDBL.SQLExecuteNonQuery(sAccessCode, sSql)
            'End If

            Return iMaxID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetUserType(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_Type from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCompanyID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Dim iCompanyId As Integer = 0
        Try
            sSql = "Select ISNULL(usr_CompanyId,0) as usr_CompanyId from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If (dt.Rows.Count > 0) Then
                iCompanyId = dt.Rows.Item(0)("usr_CompanyId")
            Else
                iCompanyId = 0
            End If
            Return iCompanyId
        Catch ex As Exception
            iCompanyId = 0
        End Try
    End Function

    Public Function GetUserDeptID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Dim iDeptID As Integer = 0
        Try
            sSql = "Select ISNULL(usr_deptid,0) as usr_deptid from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If (dt.Rows.Count > 0) Then
                iDeptID = dt.Rows.Item(0)("usr_deptid")
            Else
                iDeptID = 0
            End If
            Return iDeptID
        Catch ex As Exception
            iDeptID = 0
        End Try
    End Function
End Class

