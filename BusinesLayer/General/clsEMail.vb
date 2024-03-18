Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.Net.Mail
Public Class clsEMail
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Sub SaveEmailSentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iMasterID As Integer, ByVal iYearID As Integer, ByVal sFormName As String, ByVal sFromEmails As String, ByVal sTOEmails As String, ByVal sCCEmails As String, ByVal sSubject As String, ByVal sBody As String, ByVal sIPAddress As String, ByVal iUsrID As Integer, ByVal sEmailStatus As String)
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iEmailParamCount As Integer
        Dim Arr(1) As String
        Try
            iEmailParamCount = 0

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_MstPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iEmailParamCount).Value = iMasterID
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_YearID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iEmailParamCount).Value = iYearID
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_FormName", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iEmailParamCount).Value = sFormName
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_FromEMailID", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iEmailParamCount).Value = sFromEmails
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_ToEmailIDs", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iEmailParamCount).Value = sTOEmails
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_CCEmailIDs", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iEmailParamCount).Value = sCCEmails
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_Subject", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iEmailParamCount).Value = sSubject
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1


            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_Body", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iEmailParamCount).Value = sBody
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1


            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_EMailStatus", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iEmailParamCount).Value = sEmailStatus
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_SentUsrID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iEmailParamCount).Value = iUsrID
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iEmailParamCount).Value = iUsrID
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iEmailParamCount).Value = sIPAddress
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iEmailParamCount).Value = iACID
            ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
            iEmailParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "SpGRACe_EMailSent_Details", ObjSFParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetEmailIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal susrIDs As String) As String
        Dim sSql As String
        Dim iFunOwnerID As Integer = 0, iSPOCID As Integer = 0, iManagerID As Integer = 0, i As Integer
        Dim sIDs As String = "", sEmailIDs As String = ""
        Dim dtEmails As New DataTable
        Try
            sSql = "Select Usr_Email from sad_userdetails where USr_ID in (" & susrIDs & ") And usr_Category=1 ANd Usr_Email like '%@%' And Usr_Email like '%.%' And Usr_CompID = " & iACID & ""
            dtEmails = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtEmails.Rows.Count > 0 Then
                For i = 0 To dtEmails.Rows.Count - 1
                    If IsDBNull(dtEmails.Rows(i)("Usr_Email")) = False Then
                        sEmailIDs = sEmailIDs & "," & dtEmails.Rows(i)("Usr_Email")
                    End If
                Next
                If sEmailIDs.StartsWith(",") Then
                    sEmailIDs = sEmailIDs.Remove(0, 1)
                End If
                If sEmailIDs.EndsWith(",") Then
                    sEmailIDs = sEmailIDs.Remove(Len(susrIDs) - 1, 1)
                End If
            Else
                sEmailIDs = ""
            End If
            Return sEmailIDs
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmailIDsName(ByVal sAC As String, ByVal iACID As Integer, ByVal susrIDs As String) As String
        Dim sSql As String, sTONames As String = ""
        Dim dtNames As New DataTable
        Try
            sSql = "Select Usr_FullName from sad_userdetails where USr_ID in (" & susrIDs & ") And Usr_Email like '%@%' And Usr_Email like '%.%' And Usr_CompID = " & iACID & ""
            dtNames = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dtNames.Rows.Count - 1
                sTONames = sTONames & "/" & dtNames.Rows(i)("Usr_FullName") & ""
            Next
            If sTONames.StartsWith("/") Then
                sTONames = sTONames.Remove(0, 1)
            End If
            If sTONames.EndsWith("/") Then
                sTONames = sTONames.Remove(Len(sTONames) - 1, 1)
            End If
            Return sTONames
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmailDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String)
        Dim sSql As String = ""
        Dim sEmailDetails As String = ""
        Try
            'sSql = " Select "
            'If sType = "SMTPAddress" Then
            '    sSql = sSql & " Conf_IPAddress"
            'ElseIf sType = "SenderEmailID" Then
            '    sSql = sSql & " Conf_From"
            'ElseIf sType = "PortNo" Then
            '    sSql = sSql & " conf_Port"
            'End If
            'sSql = sSql & " From ins_config Where Conf_CompID=" & iACID & ""
            'sEmailDetails = objDBL.SQLExecuteScalar(sAC, sSql)
            'Return sEmailDetails

            If sType = "SMTPAddress" Then
                sEmailDetails = "smtp.gbb.com.ng"
            ElseIf sType = "SenderEmailID" Then
                sEmailDetails = "edms@commtech.gov.ng"
            ElseIf sType = "PortNo" Then
                sEmailDetails = "25"
            ElseIf sType = "UserName" Then
                sEmailDetails = "edms@commtech.gov.ng"
            ElseIf sType = "Password" Then
                sEmailDetails = "Password1"
            End If
            Return sEmailDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFUNOwnerHODManagerSPOCIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer, ByVal sType As String) As Integer
        Dim sSql As String = ""
        Try
            If sType = "FUNHOD" Then
                sSql = "Select ENT_FunownerID From mst_Entity_master Where ENT_ID=" & iFunID & " and ENT_CompID=" & iACID & ""
            ElseIf sType = "FUNMANAGER" Then
                sSql = "Select Ent_FunManagerID From mst_Entity_master Where ENT_ID=" & iFunID & " and ENT_CompID=" & iACID & ""
            ElseIf sType = "FUNSPOC" Then
                sSql = "Select Ent_FunSPOCID From mst_Entity_master Where ENT_ID=" & iFunID & " and ENT_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerUserIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustomerID As Integer) As String
        Dim sSql As String = "", sUserID As String = ""
        Dim dt As New DataTable
        Dim i As Integer
        Try
            If iCustomerID > 0 Then
                sSql = "select Usr_ID from sad_userdetails where Usr_CompanyID=" & iCustomerID & " and Usr_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                For i = 0 To dt.Rows.Count - 1
                    sUserID = sUserID & "," & dt.Rows(i)("Usr_ID")
                Next
                If sUserID <> "" Then
                    If sUserID.StartsWith("") Then
                        sUserID = sUserID.Remove(0, 1)
                    End If
                    If sUserID.EndsWith("") Then
                        sUserID = sUserID.Remove(Len(sUserID) - 1, 1)
                    End If
                End If
            End If
            Return sUserID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SendAPMMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sFromDate As String, ByVal sAuditNO As String, ByVal iAuditID As Integer, ByVal iFunID As Integer,
                                ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String, ByVal sTOIDs As String, ByVal sCCIDs As String, ByVal sFromEmailID As String, ByVal sFromName As String) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = "", sSubFunction As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = "", sIssueHeading As String = ""

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sUsrToIDs = sTOIDs
        sUsrCCIDs = sCCIDs & "," & iUserID
        If sUsrCCIDs.StartsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(0, 1)
        End If
        If sUsrCCIDs.EndsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(Len(sUsrCCIDs) - 1, 1)
        End If

        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCEmailsID = sCCEmailsID & "," & sFromEmailID
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If

                sSubject = "Intimation of Audit Start - " & sAuditNO & "."

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = "Audit with Code " & sAuditNO & " has been initiated and Submit by " & sFromName & ". It will start from " & sFromDate & " Date."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iAuditID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iAuditID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iAuditID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function SendRCSACRSAMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal sFormName As String, ByVal sFormStatus As String,
                   ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sUsrToIDs As String, sUsrCCIDs As String, ByVal sIPAddress As String) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = "", sIssueHeading As String = ""

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")
        sFunctionName = objclsGeneralFunctions.GetFunctionNameFromPKID(sAC, iACID, iFunctionID)

        If ((sFormStatus = "Submitted(Risk Team)") Or (sFormStatus = "Submitted(Business Team)") Or (sFormStatus = "Re-Assigned(Business Team)") Or (sFormStatus = "Approved")) Then
            sSubject = "Review Of RCSA Function-" & sFunctionName & "."
        Else
            sSubject = "Review Of CRSA Function-" & sFunctionName & "."
        End If

        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                'Risk
                If sFormStatus = "Submitted(Risk Team)" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Submitted by " & sLoginFullName & " (Risk Team)."
                ElseIf sFormStatus = "Submitted(Business Team)" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Submitted by " & sLoginFullName & " (Bussiness Team)."
                ElseIf sFormStatus = "Re-Assigned(Risk Team)" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Re-Assigned by " & sLoginFullName & " (Risk Team)."
                ElseIf sFormStatus = "Approved" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Approved by " & sLoginFullName & " (Risk Team)."
                    'Compliance
                ElseIf sFormStatus = "CR Submitted(Compliance Team)" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Submitted by " & sLoginFullName & " (Compliance Team)."
                ElseIf sFormStatus = "CR Submitted(Business Team)" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Submitted by " & sLoginFullName & " (Bussiness Team)."
                ElseIf sFormStatus = "CR Re-Assigned(Compliance Team)" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Re-Assigned by " & sLoginFullName & " (Compliance Team)."
                ElseIf sFormStatus = "CR Approved" Then
                    sBody = sBody & "The Function " & sFunctionName & " Is Approved by " & sLoginFullName & " (Compliance Team)."
                End If
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & " This Is an automated message. Please Do Not reply To this." & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    'Public Function SendFRRScheduleMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal dStartDate As Date,
    '                   ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String) As String

    '    Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
    '    Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
    '    Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = ""
    '    Dim iFunID As Integer = 0

    '    sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
    '    sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
    '    iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

    '    sUsrToIDs = GetVendorUserIDs(sAC, iACID, iReviewerID)
    '    sUsrCCIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 1, 0, 0)

    '    sFunctionName = objclsGeneralFunctions.GetFunctionNameFromPKID(sAC, iACID, iFunctionID)

    '    If sUsrToIDs <> "" Then
    '        sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
    '        If sToEmailsID <> "" Then
    '            sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
    '            If sUsrCCIDs <> "" Then
    '                sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
    '                sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
    '            End If
    '            sSubject = "FRR planning For the Function-" & sFunctionName & "."

    '            sBody = "Dear " & sToNames & ""
    '            sBody = sBody & System.Environment.NewLine
    '            sBody = sBody & System.Environment.NewLine
    '            sBody = sBody & "This FRR has been scheduled To start from " & dStartDate & " For the Function " & sFunctionName & " ."
    '            sBody = sBody & System.Environment.NewLine
    '            sBody = sBody & System.Environment.NewLine
    '            sBody = sBody & System.Environment.NewLine
    '            sBody = sBody & "______________________________________________________________________________"
    '            sBody = sBody & System.Environment.NewLine
    '            sBody = sBody & System.Environment.NewLine

    '            sBody = sBody & "This Is an automated message. Please Do Not reply To this. " & System.Environment.NewLine
    '            Try
    '                If sToEmailsID <> "" Then
    '                    Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
    '                    If sCCEmailsID <> "" Then
    '                        Dim strCC() As String = Split(sCCEmailsID, ",")
    '                        Dim strThisCC As String
    '                        For Each strThisCC In strCC
    '                            sMailMsg.CC.Add(Trim(strThisCC))
    '                        Next
    '                    End If
    '                    Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
    '                    mySmtpClient.UseDefaultCredentials = True
    '                    mySmtpClient.Send(sMailMsg)
    '                    SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
    '                End If
    '            Catch ex As Exception
    '                SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
    '                Return "Failure Sending Mail."
    '            End Try
    '        Else
    '            SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
    '            Return "Failure Sending Mail."
    '        End If
    '    End If
    '    Return ""
    'End Function
    Public Function GetVendorUserIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iVendorID As Integer) As String
        Dim sSql As String = "", sUserID As String = ""
        Dim dt As New DataTable
        Dim i As Integer
        Try
            If iVendorID > 0 Then
                sSql = "Select Usr_ID from sad_userdetails where Usr_CompanyID=" & iVendorID & " And Usr_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                For i = 0 To dt.Rows.Count - 1
                    sUserID = sUserID & "," & dt.Rows(i)("Usr_ID")
                Next
                If sUserID <> "" Then
                    If sUserID.StartsWith(",") Then
                        sUserID = sUserID.Remove(0, 1)
                    End If
                    If sUserID.EndsWith(",") Then
                        sUserID = sUserID.Remove(Len(sUserID) - 1, 1)
                    End If
                End If
            End If
            Return sUserID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllRiskAuditComplanceTeamIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As Integer, ByVal iAuditID As Integer, ByVal iComplianceID As Integer) As String
        Dim sSql As String = "", sUserID As String = ""
        Dim dt As New DataTable
        Dim i As Integer
        Try
            If iRiskID > 0 Or iAuditID > 0 Or iComplianceID > 0 Then
                sSql = "select Usr_ID from sad_userdetails where Usr_CompanyID=0 And usr_category=1 And (Usr_DutyStatus='A' or Usr_DutyStatus='B' or Usr_DutyStatus='L') And ("
                If iRiskID > 0 Then
                    sSql = sSql & " (Usr_RiskModule=1 And Usr_RiskRole in (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' "
                    sSql = sSql & " And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK') "
                    sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='RISK'))))"
                End If
                If iAuditID > 0 Then
                    If iRiskID > 0 Then
                        sSql = sSql & " Or "
                    End If
                    sSql = sSql & " (usr_AuditModule=1 And Usr_AuditRole in (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' "
                    sSql = sSql & " And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='AUD') "
                    sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='AUD'))))"
                End If
                If iComplianceID > 0 Then
                    If iAuditID > 0 Or iRiskID > 0 Then
                        sSql = sSql & " Or "
                    End If
                    sSql = sSql & " (usr_complianceModule=1 And Usr_ComplianceRole in (Select Perm_UsrORGrpID From SAD_UsrOrGrp_Permission where Perm_Status='A' And Perm_PType='R' "
                    sSql = sSql & " And Perm_ModuleID In (Select Mod_ID From SAD_MODULE Where Mod_Code='COMP') "
                    sSql = sSql & " And Perm_OpPKID in (Select OP_PKID From SAD_Mod_Operations Where OP_ModuleID in (Select Mod_ID From SAD_MODULE Where Mod_Code='COMP'))))"
                End If
                sSql = sSql & ") And Usr_CompID=" & iACID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dt.Rows.Count - 1
                sUserID = sUserID & "," & dt.Rows(i)("Usr_ID")
            Next
            If sUserID <> "" Then
                If sUserID.StartsWith(",") Then
                    sUserID = sUserID.Remove(0, 1)
                End If
                If sUserID.EndsWith(",") Then
                    sUserID = sUserID.Remove(Len(sUserID) - 1, 1)
                End If
            End If
            Return sUserID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SendFRRConductMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sAssignment As String, ByVal sFunction As String,
                       ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String, ByVal iFRRASgID As Integer) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sUsrToIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 1, 0, 0)
        sUsrCCIDs = iUserID

        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If
                sSubject = "Submission Of FRR conduct For the Function " & sFunction & " With  Code -" & sAssignment & ""

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Conduct For the Function " & sFunction & " has been Completed. "
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This Is an automated message. Please Do Not reply To this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iFRRASgID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iFRRASgID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iFRRASgID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function SendRiskIssueTrackerMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAssignmentID As Integer, ByVal iIssueID As Integer, iStatus As Integer,
                       ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = "", sIssueHeading As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        iFunID = GetRiskIssueHeadingAndFunction(sAC, iACID, iAssignmentID, iIssueID, "FUNCTIONID")
        sFunctionName = GetRiskIssueHeadingAndFunction(sAC, iACID, iAssignmentID, iIssueID, "FUNCTION")
        sIssueHeading = GetRiskIssueHeadingAndFunction(sAC, iACID, iAssignmentID, iIssueID, "ISSUE")

        If sFormName = "Risk FRR Issues Tracker" Or sFormName = "Risk KCC Issues Tracker" Then
            sUsrToIDs = GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNHOD") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNSPOC") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNMANAGER")
        ElseIf sFormName = "Risk FRR Issues Follow" Or sFormName = "Risk KCC Issues Follow" Then
            sUsrToIDs = GetRiskIssueHeadingAndFunction(sAC, iACID, iAssignmentID, iIssueID, "ISSUECRBY")
        End If
        sUsrCCIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 1, 0, 0)
        If sUsrToIDs.StartsWith(",") Then
            sUsrToIDs = sUsrToIDs.Remove(0, 1)
        End If
        If sUsrCCIDs.StartsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(0, 1)
        End If
        If sUsrToIDs.EndsWith(",") Then
            sUsrToIDs = sUsrToIDs.Remove(Len(sUsrToIDs) - 1, 1)
        End If
        If sUsrCCIDs.EndsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(Len(sUsrCCIDs) - 1, 1)
        End If
        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If
                If sFormName = "Risk FRR Issues Tracker" Or sFormName = "Risk FRR Issues Follow" Then
                    sSubject = "Review Of FRR Issue For the Function " & sFunctionName & "."
                ElseIf sFormName = "Risk KCC Issues Tracker" Or sFormName = "Risk KCC Issues Follow" Then
                    sSubject = "Review Of KCC Issue For the Function " & sFunctionName & "."
                End If

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                If sFormName = "Risk FRR Issues Tracker" Or sFormName = "Risk FRR Issues Follow" Then
                    sBody = "The Risk FRR Issue For the Function " & sFunctionName & " Is Submitted by " & sLoginFullName & "."
                ElseIf sFormName = "Risk KCC Issues Tracker" Or sFormName = "Risk KCC Issues Follow" Then
                    sBody = "The Risk KCC Issue For the Function " & sFunctionName & " Is Submitted by " & sLoginFullName & "."
                End If
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Issue Heading - " & sIssueHeading & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                If iStatus = 1 Then
                    sBody = sBody & "Status - Open."
                ElseIf iStatus = 2 Then
                    sBody = sBody & "Status - Closed."
                End If
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This Is an automated message. Please Do Not reply To this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iAssignmentID, iYearID, sFormName, "", sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iAssignmentID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iAssignmentID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function GetRiskIssueHeadingAndFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iAssignmentID As Integer, ByVal iIssueID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "FUNCTION" Then
                sSql = "Select ENT_EntityName From MST_Entity_master Where ENT_ID=(Select RIT_FunID from Risk_IssueTracker Where RIT_AsgNo=" & iAssignmentID & " And RIT_PKID=" & iIssueID & ")"
            ElseIf sType = "FUNCTIONID" Then
                sSql = "Select RIT_FunID from Risk_IssueTracker Where RIT_AsgNo=" & iAssignmentID & " And RIT_PKID=" & iIssueID & ""
            ElseIf sType = "ISSUE" Then
                sSql = "Select RIT_IssueHeading from Risk_IssueTracker Where RIT_AsgNo=" & iAssignmentID & " And RIT_PKID=" & iIssueID & ""
            ElseIf sType = "ISSUECRBY" Then
                sSql = "Select RIT_CrBy from Risk_IssueTracker Where RIT_AsgNo=" & iAssignmentID & " And RIT_PKID=" & iIssueID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SendBRRIssueTrackerMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAssignmentID As Integer, ByVal iIssueID As Integer, iFunctionId As Integer,
                                           ByVal iStatus As Integer, ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String) As String
        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = "", sIssueHeading As String = ""

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sFunctionName = objclsGeneralFunctions.GetFunctionNameFromPKID(sAC, iACID, iFunctionId)
        sIssueHeading = GetRiskBRRIssueHeading(sAC, iACID, iAssignmentID, iIssueID, "ISSUE")

        If sFormName = "BRR Issues Tracker" Then
            sUsrToIDs = GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunctionId, "FUNHOD") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunctionId, "FUNSPOC") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunctionId, "FUNMANAGER")
        ElseIf sFormName = "BRR Issues Follow" Then
            sUsrToIDs = GetRiskBRRIssueHeading(sAC, iACID, iAssignmentID, iIssueID, "ISSUECRBY")
        End If
        sUsrCCIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 1, 0, 0) & "," & iUserID
        If sUsrToIDs.StartsWith(",") Then
            sUsrToIDs = sUsrToIDs.Remove(0, 1)
        End If
        If sUsrCCIDs.StartsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(0, 1)
        End If
        If sUsrToIDs.EndsWith(",") Then
            sUsrToIDs = sUsrToIDs.Remove(Len(sUsrToIDs) - 1, 1)
        End If
        If sUsrCCIDs.EndsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(Len(sUsrCCIDs) - 1, 1)
        End If
        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If
                sSubject = "Review of BRR Issue for the Function " & sFunctionName & "."

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = "The BRR Issue for the Function " & sFunctionName & " is Submitted by " & sLoginFullName & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Issue Heading - " & sIssueHeading & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                If iStatus = 1 Then
                    sBody = sBody & "Status - Open."
                ElseIf iStatus = 2 Then
                    sBody = sBody & "Status - Closed."
                ElseIf iStatus = 3 Then
                    sBody = sBody & "Status - Open -Not Actioned."
                End If
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iAssignmentID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iAssignmentID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iAssignmentID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function GetRiskBRRIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iAssignmentID As Integer, ByVal iIssueID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "ISSUE" Then
                sSql = "Select BBRIT_IssueHeading from Risk_BRRIssueTracker Where BBRIT_AsgNo=" & iAssignmentID & " And BBRIT_PKID=" & iIssueID & ""
            ElseIf sType = "ISSUECRBY" Then
                sSql = "Select BBRIT_CrBy from Risk_BRRIssueTracker Where BBRIT_AsgNo=" & iAssignmentID & " And BBRIT_PKID=" & iIssueID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SendBRRCheckListMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAssignmentID As Integer, ByVal iBranchID As Integer,
                        ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = "", sBranchName As String = ""


        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sUsrToIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 1, 0, 0)
        sUsrCCIDs = GetBRRZOMManagerEMMPFromBRRSchedule(sAC, iACID, iAssignmentID) & "," & iUserID
        sBranchName = objclsGeneralFunctions.GetBranchNameFromPKID(sAC, iACID, iBranchID)
        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
            If sUsrCCIDs <> "" Then
                sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
            End If
            sSubject = "BRR Checklist Is submitted For the Branch- " & sBranchName & ""
            sBody = "Dear " & sToNames & ""
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & System.Environment.NewLine
            sBody = "The BRR Checklist Is submitted For the Branch- " & sBranchName & " by " & sLoginFullName & "."
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & "______________________________________________________________________________"
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & System.Environment.NewLine

            sBody = sBody & "This Is an automated message. Please Do Not reply To this. " & System.Environment.NewLine
            Try
                If sToEmailsID <> "" Then
                    Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                    If sCCEmailsID <> "" Then
                        Dim strCC() As String = Split(sCCEmailsID, ",")
                        Dim strThisCC As String
                        For Each strThisCC In strCC
                            sMailMsg.CC.Add(Trim(strThisCC))
                        Next
                    End If
                    Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                    mySmtpClient.UseDefaultCredentials = True
                    mySmtpClient.Send(sMailMsg)
                    SaveEmailSentDetails(sAC, iACID, iAssignmentID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                End If
            Catch ex As Exception
                SaveEmailSentDetails(sAC, iACID, iYearID, iAssignmentID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End Try
        End If
        Return ""
    End Function
    Public Function GetBRRZOMManagerEMMPFromBRRSchedule(ByVal sAC As String, ByVal iACID As Integer, ByVal iAssignmentID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Convert(Varchar(10),BRRS_BranchMgrID) + ',' +  Convert(Varchar(10),BRRS_ZonalMgrID) + ',' +  Convert(Varchar(10),BRRS_EmployeeID) from Risk_BRRSchedule where BRRS_PKID=" & iAssignmentID & " And BRRS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SendKCCScheduleMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iReviewerID As Integer, ByVal dStartDate As Date,
                       ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sUsrToIDs = GetVendorUserIDs(sAC, iACID, iReviewerID)
        sUsrCCIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 1, 0, 0)

        sFunctionName = objclsGeneralFunctions.GetFunctionNameFromPKID(sAC, iACID, iFunctionID)

        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If
                sSubject = "KCC planning For the Function-" & sFunctionName & "."

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "This KCC has been scheduled To start from " & dStartDate & " For the Function " & sFunctionName & " ."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This Is an automated message. Please Do Not reply To this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iFunctionID, iYearID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function SendCRVendorAnnualPlanMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal iSubFunctionID As Integer, ByVal sAuditCode As String,
                   ByVal sFormName As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String, ByVal i3YPID As Integer) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = "", sSubFunction As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = "", sIssueHeading As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sFunctionName = objclsGeneralFunctions.GetFunctionNameFromPKID(sAC, iACID, iFunctionID)
        sSubFunction = objclsGeneralFunctions.GetSubFunNameFromPKID(sAC, iACID, iSubFunctionID)

        sUsrToIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 0, 0, 1)
        sUsrCCIDs = ""

        If sUsrCCIDs.StartsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(0, 1)
        End If
        If sUsrCCIDs.EndsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(Len(sUsrCCIDs) - 1, 1)
        End If

        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If

                sSubject = "Compliance Schedule Month for the Compliance Code - " & sAuditCode & "."

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = "Compliance Schedule Month to conduct the Compliance of Function - " & sFunctionName & " & Sub Function - " & sSubFunction & " for the Compliance Code - " & sAuditCode & " is filled and submitted by " & sLoginFullName & ". "
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ";")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iCustID, iFunctionID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iCustID, iFunctionID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iCustID, iFunctionID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function SendCRIssueTrackerMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal sAssignment As String, ByVal sIssueNo As String,
                          ByVal sFormName As String, ByVal sIssueHeading As String, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String,
                                           ByVal iAuditID As Integer, ByVal iIssueID As Integer) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sUsrToIDs = GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNHOD") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNSPOC") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNMANAGER")
        sUsrCCIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 0, 0, 1) & "," & iUserID
        If sUsrCCIDs.StartsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(0, 1)
        End If
        If sUsrCCIDs.EndsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(Len(sUsrCCIDs) - 1, 1)
        End If
        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If
                sSubject = "Submission of CR Issue No - " & sIssueNo & " of the Compliance Code - " & sAssignment & ""

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "This Issue with No - " & sIssueNo & " has been Submitted by " & sLoginFullName & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Issue Heading - " & sIssueHeading & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Status - " & sStatus & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iCustID, iAuditID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iCustID, iAuditID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iCustID, iAuditID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function SendCRFollowUpMail(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal sAssignment As String, ByVal sIssueNo As String,
                    ByVal sFormName As String, ByVal sIssueHeading As String, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String, ByVal iAuditID As Integer, ByVal iIssueID As Integer) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")

        sUsrToIDs = GetCRIssueHeadingCRBY(sAC, iACID, iCustID, iAuditID, iIssueID, "ISSUECRBY") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNHOD") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNSPOC") & "," & GetFUNOwnerHODManagerSPOCIDs(sAC, iACID, iFunID, "FUNMANAGER")
        sUsrCCIDs = GetAllRiskAuditComplanceTeamIDs(sAC, iACID, 0, 0, 1) & "," & iUserID
        If sUsrToIDs.StartsWith(",") Then
            sUsrToIDs = sUsrToIDs.Remove(0, 1)
        End If
        If sUsrCCIDs.StartsWith(",") Then
            sUsrCCIDs = sUsrCCIDs.Remove(0, 1)
        End If
        If sUsrToIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sUsrToIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sUsrToIDs)
                If sUsrCCIDs <> "" Then
                    sCCEmailsID = GetEmailIDs(sAC, iACID, sUsrCCIDs)
                    sCCNames = GetEmailIDsName(sAC, iACID, sUsrCCIDs)
                End If
                sSubject = "Submission of Follow Up of Issue No - " & sIssueNo & "  for the  Compliance Code - " & sAssignment & ""

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "This Issue with No - " & sIssueNo & " has been Followed by and Submitted by " & sLoginFullName & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Issue Heading - " & sIssueHeading & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Status - " & sStatus & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        mySmtpClient.UseDefaultCredentials = True
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, iCustID, iAuditID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, iCustID, iAuditID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, iCustID, iAuditID, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "To E-Mail ID Not avilable", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    Public Function GetCRIssueHeadingCRBY(ByVal sAC As String, ByVal iACID As Integer, ByVal iAssignmentID As Integer, ByVal iCustID As Integer, ByVal iIssueID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "ISSUE" Then
                sSql = "Select CIT_IssueHeading from Compliance_issuetracker_details Where CIT_CustomerID=" & iCustID & " And CIT_ComplianceCodeID=" & iAssignmentID & " And CIT_PKID=" & iIssueID & ""
            ElseIf sType = "ISSUECRBY" Then
                sSql = "Select CIT_CreatedBy from Compliance_issuetracker_details Where CIT_CustomerID=" & iCustID & " And CIT_ComplianceCodeID=" & iAssignmentID & " And CIT_PKID=" & iIssueID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SendInwardMail(ByVal sAC As String, ByVal iACID As Integer, ByVal sInwardNo As String,
                       ByVal sFormName As String, ByVal sReferenceNo As String, ByVal sTitle As String, ByVal sTOIDs As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")
        Dim sSMTPUserName As String = GetEmailDetails(sAC, iACID, "UserName")
        Dim sSMTPPassword As String = GetEmailDetails(sAC, iACID, "Password")

        If sTOIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sTOIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sTOIDs)
                sCCEmailsID = GetEmailIDs(sAC, iACID, iUserID)
                sCCNames = GetEmailIDsName(sAC, iACID, iUserID)

                sSubject = "EDICT Inward Notification-" & sInwardNo & "."

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Inward No-" & sInwardNo & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Document Reference No-" & sReferenceNo & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Document Title-" & sTitle & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Sent By-" & sLoginFullName & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        Dim SMTPUserInfo As New System.Net.NetworkCredential(sSMTPUserName, sSMTPPassword)
                        mySmtpClient.UseDefaultCredentials = False
                        mySmtpClient.Credentials = SMTPUserInfo
                        mySmtpClient.Send(sMailMsg)

                        SaveEmailSentDetails(sAC, iACID, sFormName, 0, sInwardNo, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, sFormName, 0, sInwardNo, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, sFormName, 0, sInwardNo, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "Email-IDs not Available", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
    'Public Sub SaveEmailSentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal sFormName As String, ByVal sFormStatus As String, ByVal sInwardNo As String, ByVal sTOEmails As String, ByVal sCCEmails As String, ByVal sSubject As String, ByVal sBody As String, ByVal sIPAddress As String, ByVal iUsrID As Integer, ByVal sEmailStatus As String)
    '    Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
    '    Dim iEmailParamCount As Integer
    '    Dim Arr(1) As String
    '    Try
    '        iEmailParamCount = 0

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_FormName", OleDb.OleDbType.VarChar, 50)
    '        ObjSFParam(iEmailParamCount).Value = sFormName
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_IsInwardWorkFlowStatus", OleDb.OleDbType.VarChar, 1)
    '        ObjSFParam(iEmailParamCount).Value = sFormStatus
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_InwardNo", OleDb.OleDbType.VarChar, 100)
    '        ObjSFParam(iEmailParamCount).Value = sInwardNo
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1


    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_ToEmailIDs", OleDb.OleDbType.VarChar, 8000)
    '        ObjSFParam(iEmailParamCount).Value = sTOEmails
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_CCEmailIDs", OleDb.OleDbType.VarChar, 8000)
    '        ObjSFParam(iEmailParamCount).Value = sCCEmails
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_Subject", OleDb.OleDbType.VarChar, 2000)
    '        ObjSFParam(iEmailParamCount).Value = sSubject
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1


    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_Body", OleDb.OleDbType.VarChar, 8000)
    '        ObjSFParam(iEmailParamCount).Value = sBody
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1


    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_EMailStatus", OleDb.OleDbType.VarChar, 25)
    '        ObjSFParam(iEmailParamCount).Value = sEmailStatus
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_SentUsrID", OleDb.OleDbType.Integer, 4)
    '        ObjSFParam(iEmailParamCount).Value = iUsrID
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_IPAddress", OleDb.OleDbType.VarChar, 25)
    '        ObjSFParam(iEmailParamCount).Value = sIPAddress
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        ObjSFParam(iEmailParamCount) = New OleDb.OleDbParameter("@EMD_CompID", OleDb.OleDbType.Integer, 4)
    '        ObjSFParam(iEmailParamCount).Value = iACID
    '        ObjSFParam(iEmailParamCount).Direction = ParameterDirection.Input
    '        iEmailParamCount += 1

    '        objDBL.ExecuteSPForInsertNoOutput(sAC, "SpEDICT_EMailSent_Details", ObjSFParam)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Function SendOutwardMail(ByVal sAC As String, ByVal iACID As Integer, ByVal sOutwardNo As String,
                      ByVal sFormName As String, ByVal sReferenceNo As String, ByVal sTitle As String, ByVal sTOIDs As String, ByVal iUserID As Integer, ByVal sLoginFullName As String, ByVal sIPAddress As String) As String

        Dim sSMTPAddress As String, sSenderEmailID As String, iPortNo As Integer
        Dim sBody As String, sCCNames As String, sFunctionName As String = "", sUsrToIDs As String = "", sUsrCCIDs As String = ""
        Dim sToEmailsID As String = "", sCCEmailsID As String = "", sToNames As String = "", sSubject As String = ""
        Dim iFunID As Integer = 0

        sSMTPAddress = GetEmailDetails(sAC, iACID, "SMTPAddress")
        sSenderEmailID = GetEmailDetails(sAC, iACID, "SenderEmailID")
        iPortNo = GetEmailDetails(sAC, iACID, "PortNo")
        Dim sSMTPUserName As String = GetEmailDetails(sAC, iACID, "UserName")
        Dim sSMTPPassword As String = GetEmailDetails(sAC, iACID, "Password")

        If sTOIDs <> "" Then
            sToEmailsID = GetEmailIDs(sAC, iACID, sTOIDs)
            If sToEmailsID <> "" Then
                sToNames = GetEmailIDsName(sAC, iACID, sTOIDs)
                sCCEmailsID = GetEmailIDs(sAC, iACID, iUserID)
                sCCNames = GetEmailIDsName(sAC, iACID, iUserID)

                sSubject = "EDICT Outward Notification-" & sOutwardNo & "."

                sBody = "Dear " & sToNames & ""
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Outward No-" & sOutwardNo & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Document Reference No-" & sReferenceNo & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Document Title-" & sTitle & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "Sent By-" & sLoginFullName & "."
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & "______________________________________________________________________________"
                sBody = sBody & System.Environment.NewLine
                sBody = sBody & System.Environment.NewLine

                sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
                Try
                    If sToEmailsID <> "" Then
                        Dim sMailMsg As New MailMessage(sSenderEmailID, sToEmailsID, sSubject, sBody)
                        If sCCEmailsID <> "" Then
                            Dim strCC() As String = Split(sCCEmailsID, ",")
                            Dim strThisCC As String
                            For Each strThisCC In strCC
                                sMailMsg.CC.Add(Trim(strThisCC))
                            Next
                        End If
                        Dim mySmtpClient As New SmtpClient(sSMTPAddress, iPortNo)
                        Dim SMTPUserInfo As New System.Net.NetworkCredential(sSMTPUserName, sSMTPPassword)
                        mySmtpClient.UseDefaultCredentials = False
                        mySmtpClient.Credentials = SMTPUserInfo
                        mySmtpClient.Send(sMailMsg)
                        SaveEmailSentDetails(sAC, iACID, sFormName, 0, sOutwardNo, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "YES")
                    End If
                Catch ex As Exception
                    SaveEmailSentDetails(sAC, iACID, sFormName, 0, sOutwardNo, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, sBody, sIPAddress, iUserID, "NO")
                    Return "Failure Sending Mail."
                End Try
            Else
                SaveEmailSentDetails(sAC, iACID, 0, 0, sFormName, sSenderEmailID, sToEmailsID, sCCEmailsID, sSubject, "Email-IDs not Available", sIPAddress, iUserID, "NO")
                Return "Failure Sending Mail."
            End If
        End If
        Return ""
    End Function
End Class
