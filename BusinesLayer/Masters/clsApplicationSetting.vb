Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsApplicationSettings
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private MPS_MinimumChar As Integer
    Private MPS_MaximumChar As Integer
    Private MPS_RecoveryAttempts As Integer
    Private MPS_UnsuccessfulAttempts As Integer
    Private MPS_PasswordExpiryDays As Integer
    Private MPS_NotLoginDays As Integer
    Private MPS_Password_Contains As String
    Private MPS_PasswordExpiryAlertDays As Integer
    Private MPS_UpdatedBy As Integer
    Private MPS_UpdatedOn As Date
    Private MPS_Operation As String
    Private MPS_IPAddress As String
    Private MPS_CompID As Integer

    Private Conf_IPAddress As String
    Private conf_Port As Integer
    Private Conf_From As String
    Private conf_SenderID As String
    Private conf_UpdatedBy As Integer
    Private conf_CompID As Integer
    Private Conf_Status As String
    Private Conf_INS_IPAddress As String
    Public Property iMPS_CompID() As Integer
        Get
            Return (MPS_CompID)
        End Get
        Set(ByVal Value As Integer)
            MPS_CompID = Value
        End Set
    End Property
    Public Property sMPS_IPAddress() As String
        Get
            Return (MPS_IPAddress)
        End Get
        Set(ByVal Value As String)
            MPS_IPAddress = Value
        End Set
    End Property
    Public Property sMPS_Operation() As String
        Get
            Return (MPS_Operation)
        End Get
        Set(ByVal Value As String)
            MPS_Operation = Value
        End Set
    End Property
    Public Property dMPS_UpdatedOn() As Date
        Get
            Return (MPS_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            MPS_UpdatedOn = Value
        End Set
    End Property
    Public Property iMPS_UpdatedBy() As Integer
        Get
            Return (MPS_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            MPS_UpdatedBy = Value
        End Set
    End Property
    Public Property iMPS_PasswordExpiryAlertDays() As Integer
        Get
            Return (MPS_PasswordExpiryAlertDays)
        End Get
        Set(ByVal Value As Integer)
            MPS_PasswordExpiryAlertDays = Value
        End Set
    End Property
    Public Property sMPS_Password_Contains() As String
        Get
            Return (MPS_Password_Contains)
        End Get
        Set(ByVal Value As String)
            MPS_Password_Contains = Value
        End Set
    End Property
    Public Property iMPS_NotLoginDays() As Integer
        Get
            Return (MPS_NotLoginDays)
        End Get
        Set(ByVal Value As Integer)
            MPS_NotLoginDays = Value
        End Set
    End Property
    Public Property iMPS_PasswordExpiryDays() As Integer
        Get
            Return (MPS_PasswordExpiryDays)
        End Get
        Set(ByVal Value As Integer)
            MPS_PasswordExpiryDays = Value
        End Set
    End Property
    Public Property iMPS_UnsuccessfulAttempts() As Integer
        Get
            Return (MPS_UnsuccessfulAttempts)
        End Get
        Set(ByVal Value As Integer)
            MPS_UnsuccessfulAttempts = Value
        End Set
    End Property
    Public Property iMPS_RecoveryAttempts() As Integer
        Get
            Return (MPS_RecoveryAttempts)
        End Get
        Set(ByVal Value As Integer)
            MPS_RecoveryAttempts = Value
        End Set
    End Property
    Public Property iMPS_MaximumChar() As Integer
        Get
            Return (MPS_MaximumChar)
        End Get
        Set(ByVal Value As Integer)
            MPS_MaximumChar = Value
        End Set
    End Property
    Public Property iMPS_MinimumChar() As Integer
        Get
            Return (MPS_MinimumChar)
        End Get
        Set(ByVal Value As Integer)
            MPS_MinimumChar = Value
        End Set
    End Property
    Public Property sConf_IPAddress() As String
        Get
            Return (Conf_IPAddress)
        End Get
        Set(ByVal Value As String)
            Conf_IPAddress = Value
        End Set
    End Property
    Public Property iconf_Port() As Integer
        Get
            Return (conf_Port)
        End Get
        Set(ByVal Value As Integer)
            conf_Port = Value
        End Set
    End Property
    Public Property sConf_From() As String
        Get
            Return (Conf_From)
        End Get
        Set(ByVal Value As String)
            Conf_From = Value
        End Set
    End Property
    Public Property sconf_SenderID() As String
        Get
            Return (conf_SenderID)
        End Get
        Set(ByVal Value As String)
            conf_SenderID = Value
        End Set
    End Property
    Public Property iconf_UpdatedBy() As Integer
        Get
            Return (conf_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            conf_UpdatedBy = Value
        End Set
    End Property
    Public Property iconf_CompID() As Integer
        Get
            Return (conf_CompID)
        End Get
        Set(ByVal Value As Integer)
            conf_CompID = Value
        End Set
    End Property
    Public Property sConf_Status() As String
        Get
            Return (Conf_Status)
        End Get
        Set(ByVal Value As String)
            Conf_Status = Value
        End Set
    End Property
    Public Property sConf_INS_IPAddress() As String
        Get
            Return (Conf_INS_IPAddress)
        End Get
        Set(ByVal Value As String)
            Conf_INS_IPAddress = Value
        End Set
    End Property
    Public Function GetApplicationSettingDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select sad_Config_Key,sad_Config_Value from sad_config_settings Where SAD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCurrencyType(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CUR_ID,CUR_CODE + ' [' + CUR_CountryName + ']' as CUR_Code From SAD_Currency_Master where CUR_CompID=" & iACID & " And CUR_Status = 'A' Order by CUR_CountryName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveApplicationSettings(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String, ByVal sValue As String, ByVal sIPAddress As String, ByVal iUserID As Integer, ByVal sOperation As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(6) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Config_Key", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Config_Value", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_UpdatedBy", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Config_Operation", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = sOperation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Config_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_CompID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iRet = objDBL.ExecuteSPForInsert(sAC, "spApplicationSettings", "@iOper", ObjParam)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGRACeSettingValue(ByVal sAC As String, ByVal iACID As Integer, ByVal sKey As String) As String
        Dim sSql As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key = '" & sKey & "' and sad_compid='" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPasswordSettings(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from MST_Password_Setting Where MPS_CompID=" & iACID & " "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmailSettings(ByVal sAC As String, ByVal objEmail As clsApplicationSettings)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(8) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objEmail.sConf_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@conf_Port", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmail.iconf_Port
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@conf_From", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objEmail.sConf_From
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@conf_SenderID", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objEmail.sconf_SenderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_INS_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objEmail.sConf_INS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objEmail.sConf_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmail.iconf_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_CompID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmail.iconf_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iRet = objDBL.ExecuteSPForInsert(sAC, "spEmailSettings", "@iOper", ObjParam)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmailsettings(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select conf_IPAddress,Conf_port,Conf_From,Conf_SenderID,conf_CompID from INS_COnfig Where conf_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePasswordDetails(ByVal sAC As String, ByVal objPassword As clsApplicationSettings)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_MinimumChar", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_MinimumChar
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_MaximumChar", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_MaximumChar
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_RecoveryAttempts", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_RecoveryAttempts
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_UnsuccessfulAttempts", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_UnsuccessfulAttempts
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_PasswordExpiryDays", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = objPassword.iMPS_PasswordExpiryDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_NotLoginDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_NotLoginDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_Password_Contains", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objPassword.sMPS_Password_Contains
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_PasswordExpiryAlertDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_PasswordExpiryAlertDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPassword.sMPS_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPassword.sMPS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_CompID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPassword.iMPS_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iRet = objDBL.ExecuteSPForInsert(sAC, "spPasswordManagement", "@iOper", ObjParam)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SettingsReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iCurrency As Integer) As DataTable
        Dim sSql As String, sSplitAry() As String
        Dim dt As New DataTable, dtDetails As New DataTable, dtPM As New DataTable, dtES As New DataTable
        Dim dRow As DataRow
        Dim i As Integer = 0
        Try
            dtPM = GetPasswordSettings(sAC, iACID)
            dtES = GetEmailsettings(sAC, iACID)
            dt.Columns.Add("Name")
            dt.Columns.Add("Value")
            sSql = "Select sad_Config_Key,sad_Config_Value,CUR_CODE + ' [' + CUR_CountryName + ']' as CUR_Code from sad_config_settings left join SAD_Currency_Master on CUR_Status ='A' and CUR_ID=" & iCurrency & ""
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("Name") = <b>Application Settings</b>
                dRow("Value") = ""
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                Dim DVAppADetails As New DataView(dtDetails)
                dRow("Name") = "Image path"
                DVAppADetails.Sort = "sad_Config_Key"
                Dim sImgPath As String = DVAppADetails.Find("ImgPath")
                dRow("Value") = DVAppADetails(sImgPath)("sad_Config_Value")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Error log path"
                Dim sErrorLog As String = DVAppADetails.Find("ErrorLog")
                dRow("Value") = DVAppADetails(sErrorLog)("sad_Config_Value")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Application temp directory"
                Dim sExcelPath As String = DVAppADetails.Find("ExcelPath")
                dRow("Value") = DVAppADetails(sExcelPath)("sad_Config_Value")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "FTP server"
                Dim sFtpServer As String = DVAppADetails.Find("FtpServer")
                dRow("Value") = DVAppADetails(sFtpServer)("sad_Config_Value")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Max file size"
                Dim iFileSize As Integer = DVAppADetails.Find("FileSize")
                dRow("Value") = DVAppADetails(iFileSize)("sad_Config_Value") & " MB"
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Currency type"
                dRow("Value") = DVAppADetails(0)("CUR_Code")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Session time out"
                Dim iTimeOut As Integer = DVAppADetails.Find("TimeOut")
                dRow("Value") = DVAppADetails(iTimeOut)("sad_Config_Value") & " min"
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Time out warning before"
                Dim iTimeOutWarning As Integer = DVAppADetails.Find("TimeOutWarning")
                dRow("Value") = DVAppADetails(iTimeOutWarning)("sad_Config_Value") & " min"
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "HTTP"
                Dim sHTP As String = DVAppADetails.Find("HTP")
                dRow("Value") = DVAppADetails(sHTP)("sad_Config_Value")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Date format"
                Dim dDateFormat As Integer = DVAppADetails.Find("DateFormat")
                Dim idateformat As Integer = DVAppADetails(dDateFormat)("sad_Config_Value")
                If idateformat = 1 Then
                    dRow("Value") = "dd-mmm-yy"
                ElseIf idateformat = 2 Then
                    dRow("Value") = "dd/mm/yyyy"
                ElseIf idateformat = 3 Then
                    dRow("Value") = "mm/dd/yyyy"
                ElseIf idateformat = 4 Then
                    dRow("Value") = "yyyy/mm/dd"
                End If
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "File in DB"
                Dim sFilesInDB As String = DVAppADetails.Find("FilesInDB")
                dRow("Value") = DVAppADetails(sFilesInDB)("sad_Config_Value")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Attachment file path"
                Dim sFileInDBPath As String = DVAppADetails.Find("FileInDBPath")
                dRow("Value") = DVAppADetails(sFileInDBPath)("sad_Config_Value")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Out Look"
                Dim sFileInDBOutlookEMail As String = DVAppADetails.Find("OutlookEMail")
                dRow("Value") = DVAppADetails(sFileInDBOutlookEMail)("sad_Config_Value")
                dt.Rows.Add(dRow)


                dRow = dt.NewRow()
                dRow("Name") = ""
                dRow("Value") = ""
                dt.Rows.Add(dRow)


                dRow = dt.NewRow()
                dRow("Name") = <b>Password Management</b>
                dRow("Value") = ""
                dt.Rows.Add(dRow)

                Dim DVPMDetails As New DataView(dtPM)
                dRow = dt.NewRow()
                dRow("Name") = "Min password character"
                dRow("Value") = DVPMDetails(0)("MPS_MinimumChar")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Max password character"
                dRow("Value") = DVPMDetails(0)("MPS_MaximumChar")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "No of recovery attempts"
                dRow("Value") = DVPMDetails(0)("MPS_RecoveryAttempts")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Unsuccessful attempts"
                dRow("Value") = DVPMDetails(0)("MPS_UnSuccessfulAttempts")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Password expiry days"
                dRow("Value") = DVPMDetails(0)("MPS_PasswordExpiryDays")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Password expiry alert days"
                dRow("Value") = DVPMDetails(0)("MPS_PasswordExpiryAlertDays")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Dormant (not login) days"
                dRow("Value") = DVPMDetails(0)("MPS_NotLoginDays")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Password contains"
                sSplitAry = DVPMDetails(0)("MPS_Password_Contains").Split(",")
                For iIndxAry = 0 To sSplitAry.Length - 1
                    If iIndxAry = 0 Then
                        dRow("Value") = ""
                    ElseIf iIndxAry = 1 Then
                        dRow("Value") = "Capital Letter(A - Z)"
                    ElseIf iIndxAry = 2 Then
                        dRow("Value") = "Capital Letter(A - Z),Small Letter(a-z)"
                    ElseIf iIndxAry = 3 Then
                        dRow("Value") = "Capital Letter(A - Z),Small Letter(a-z),Special Symbol"
                    ElseIf iIndxAry = 4 Then
                        dRow("Value") = "Capital Letter(A - Z),Small Letter(a-z),Special Symbol,Integer(0-9)"
                    End If
                Next
                dt.Rows.Add(dRow)


                dRow = dt.NewRow()
                dRow("Name") = ""
                dRow("Value") = ""
                dt.Rows.Add(dRow)


                dRow = dt.NewRow()
                dRow("Name") = <b>E-Mail Settings</b>
                dRow("Value") = ""
                dt.Rows.Add(dRow)

                Dim DVESDetails As New DataView(dtES)
                dRow = dt.NewRow()
                dRow("Name") = "SMTP address"
                dRow("Value") = DVESDetails(0)("conf_IPAddress")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Sender E-Mail ID"
                dRow("Value") = DVESDetails(0)("Conf_From")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "Port number"
                dRow("Value") = DVESDetails(0)("Conf_port")
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Name") = "SMS sender ID"
                dRow("Value") = DVESDetails(0)("Conf_SenderID")
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
