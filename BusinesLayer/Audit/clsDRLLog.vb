Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure str_DRLLog
    Private ADRL_ID As Integer
    Private ADRL_YearID As Integer
    Private ADRL_AuditNo As Integer
    Private ADRL_FunID As Integer
    Private ADRL_CustID As Integer
    Private ADRL_RequestedListID As Integer
    Private ADRL_RequestedTypeID As Integer
    Private ADRL_RequestedOn As String
    Private ADRL_TimlinetoResOn As String
    Private ADRL_EmailID As String
    Private ADRL_Comments As String
    Private ADRL_CrBy As Integer
    Private ADRL_UpdatedBy As Integer
    Private ADRL_IPAddress As String
    Private ADRL_CompID As Integer

    Public Property iADRL_ID() As Integer
        Get
            Return (ADRL_ID)
        End Get
        Set(ByVal Value As Integer)
            ADRL_ID = Value
        End Set
    End Property
    Public Property iADRL_YearID() As Integer
        Get
            Return (ADRL_YearID)
        End Get
        Set(ByVal Value As Integer)
            ADRL_YearID = Value
        End Set
    End Property
    Public Property iADRL_AuditNo() As Integer
        Get
            Return (ADRL_AuditNo)
        End Get
        Set(ByVal Value As Integer)
            ADRL_AuditNo = Value
        End Set
    End Property
    Public Property iADRL_FunID() As Integer
        Get
            Return (ADRL_FunID)
        End Get
        Set(ByVal Value As Integer)
            ADRL_FunID = Value
        End Set
    End Property
    Public Property iADRL_CustID() As Integer
        Get
            Return (ADRL_CustID)
        End Get
        Set(ByVal Value As Integer)
            ADRL_CustID = Value
        End Set
    End Property
    Public Property iADRL_RequestedListID() As Integer
        Get
            Return (ADRL_RequestedListID)
        End Get
        Set(ByVal Value As Integer)
            ADRL_RequestedListID = Value
        End Set
    End Property
    Public Property iADRL_RequestedTypeID() As Integer
        Get
            Return (ADRL_RequestedTypeID)
        End Get
        Set(ByVal Value As Integer)
            ADRL_RequestedTypeID = Value
        End Set
    End Property

    Public Property sADRL_RequestedOn() As String
        Get
            Return (ADRL_RequestedOn)
        End Get
        Set(ByVal Value As String)
            ADRL_RequestedOn = Value
        End Set
    End Property

    Public Property sADRL_TimlinetoResOn() As String
        Get
            Return (ADRL_TimlinetoResOn)
        End Get
        Set(ByVal Value As String)
            ADRL_TimlinetoResOn = Value
        End Set
    End Property


    Public Property sADRL_EmailID() As String
        Get
            Return (ADRL_EmailID)
        End Get
        Set(ByVal Value As String)
            ADRL_EmailID = Value
        End Set
    End Property
    Public Property sADRL_Comments() As String
        Get
            Return (ADRL_Comments)
        End Get
        Set(ByVal Value As String)
            ADRL_Comments = Value
        End Set
    End Property

    Public Property iADRL_CrBy() As Integer
        Get
            Return (ADRL_CrBy)
        End Get
        Set(ByVal Value As Integer)
            ADRL_CrBy = Value
        End Set
    End Property

    Public Property iADRL_UpdatedBy() As Integer
        Get
            Return (ADRL_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ADRL_UpdatedBy = Value
        End Set
    End Property
    Public Property sADRL_IPAddress() As String
        Get
            Return (ADRL_IPAddress)
        End Get
        Set(ByVal Value As String)
            ADRL_IPAddress = Value
        End Set
    End Property
    Public Property iADRL_CompID() As Integer
        Get
            Return (ADRL_CompID)
        End Get
        Set(ByVal Value As Integer)
            ADRL_CompID = Value
        End Set
    End Property
End Structure

Public Class clsDRLLog
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function LoadDocRequestTypeDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iDRLId As Integer) As DataTable
        Dim sSQl As String
        Try
            sSQl = "Select DRL_DRLID,DRL_Name from Audit_Doc_Request_List where DRL_DocTypeID= " & iDRLId & " And DRL_CompID=" & iACID & " Order by DRL_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSQl)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCustomerID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSQl As String
        Try
            sSQl = "Select usr_CompanyId from Sad_Userdetails Where Usr_COmpID=" & iACID & " And Usr_Id=" & iUserID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSQl)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSeletedDocDetails(ByVal sAC As String, ByVal iDRLId As Integer) As String
        Dim sSQl As String
        Try
            sSQl = "Select ADRL_Name from Audit_Doc_Request_List where ADRL_DRLID= " & iDRLId & " Order by ADRL_Name"
            Return objDBL.SQLExecuteScalar(sAC, sSQl)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDRLLogReceivedList_Details(ByVal sAC As String, ByVal objDRLLog As str_DRLLog)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_AuditNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_AuditNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_FunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_RequestedListID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_RequestedListID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_RequestedTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_RequestedTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_RequestedOn", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDRLLog.sADRL_RequestedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_TimlinetoResOn", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDRLLog.sADRL_TimlinetoResOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("ADRL_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDRLLog.sADRL_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("ADRL_Comments", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objDRLLog.sADRL_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("ADRL_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDRLLog.sADRL_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADRL_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDRLLog.iADRL_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_DRLLog", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDRLdg(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal sCheckPointIDs As String, ByVal iCustID As Integer, ByVal iYearID As Integer, ByVal iDocumentRequestedList As Integer, ByVal IsCustLogin As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Dim sdate As String
        Dim sLbl As String
        Try
            dt.Columns.Add("DRLID")
            dt.Columns.Add("CheckPointID")
            dt.Columns.Add("CheckPoint")
            dt.Columns.Add("DocumentRequestedList")
            dt.Columns.Add("DocumentRequestedType")
            dt.Columns.Add("DocumentRequestedListID")
            dt.Columns.Add("DocumentRequestedTypeID")
            dt.Columns.Add("EmailID")
            dt.Columns.Add("RequestedOn")
            dt.Columns.Add("TimlinetoResOn")
            dt.Columns.Add("Comments")
            dt.Columns.Add("Status")
            dt.Columns.Add("ReceivedComments")
            dt.Columns.Add("ReceivedOn")
            dt.Columns.Add("AttachID")
            dt.Columns.Add("DocID")

            sSql = "select CMM_ID,CMM_Desc,DRL_DRLID,DRL_Name,ADRL_ID,ADRL_YearID,ADRL_AuditNo,ADRL_FunID,ACM_Checkpoint,ADRL_CustID,ADRL_RequestedListID,ADRL_RequestedTypeID,ADRL_RequestedOn, ADRL_TimlinetoResOn,"
            sSql = sSql & " ADRL_EmailID,ADRL_Comments,ADRL_Status,ADRL_AttachID,ADRL_CompID,ADRL_ReceivedComments,ADRL_LogStatus,ADRL_ReceivedOn,b.ATCH_DocID From Audit_DRLLog"
            sSql = sSql & " Left Join AuditType_Checklist_Master On ACM_ID=ADRL_FunID"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=ADRL_RequestedListID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_Doc_Request_List On DRL_DRLID=ADRL_RequestedTypeID And DRL_CompID=" & iACID & " "
            sSql = sSql & " Left join EDT_attachments b on ADRL_AttachID=b.ATCH_ID "
            sSql = sSql & " Where ADRL_CompID = " & iACID & " And ADRL_YearID=" & iYearID & ""
            If iAuditNo > 0 Then
                sSql = sSql & " And ADRL_AuditNo = " & iAuditNo & ""
            End If
            If IsCustLogin = 0 Then
                If sCheckPointIDs <> "" Then
                    sSql = sSql & " And ADRL_FunID in (" & sCheckPointIDs & ")"
                End If
                If sCheckPointIDs = "" Then
                    sSql = sSql & " And ADRL_FunID<>0"
                End If
            End If
            If iDocumentRequestedList > 0 Then
                sSql = sSql & " And ADRL_RequestedListID=" & iDocumentRequestedList & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("DRLID") = dtTab.Rows(i)("ADRL_ID")
                    If IsDBNull(dtTab.Rows(i)("ADRL_FunID")) = False Then
                        dRow("CheckPointID") = dtTab.Rows(i)("ADRL_FunID")
                    Else
                        dRow("CheckPointID") = 0
                    End If
                    If IsDBNull(dtTab.Rows(i)("ACM_Checkpoint")) = False Then
                        dRow("CheckPoint") = dtTab.Rows(i)("ACM_Checkpoint")
                    Else
                        dRow("CheckPoint") = "Others"
                    End If
                    If IsDBNull(dtTab.Rows(i)("CMM_Desc")) = False Then
                        dRow("DocumentRequestedListID") = dtTab.Rows(i)("CMM_ID")
                        dRow("DocumentRequestedList") = dtTab.Rows(i)("CMM_Desc")
                    Else
                        dRow("DocumentRequestedListID") = 0
                        dRow("DocumentRequestedList") = "NA"
                    End If
                    If IsDBNull(dtTab.Rows(i)("DRL_Name")) = False Then
                        dRow("DocumentRequestedTypeID") = dtTab.Rows(i)("DRL_DRLID")
                        dRow("DocumentRequestedType") = dtTab.Rows(i)("DRL_Name")
                    Else
                        dRow("DocumentRequestedTypeID") = 0
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_RequestedOn")) = False Then
                        dRow("RequestedOn") = dtTab.Rows(i)("ADRL_RequestedOn")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_TimlinetoResOn")) = False Then
                        dRow("TimlinetoResOn") = dtTab.Rows(i)("ADRL_TimlinetoResOn")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_EmailID")) = False Then
                        Dim modifiedEmails As String = dtTab.Rows(i)("ADRL_EmailID").ToString().Replace(".com", ".com ")
                        dRow("EmailID") = modifiedEmails      'Varun
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_Comments")) = False Then
                        dRow("Comments") = dtTab.Rows(i)("ADRL_Comments")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_ReceivedComments")) = False Then
                        dRow("ReceivedComments") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("ADRL_ReceivedComments"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_ReceivedOn")) = False Then
                        dRow("ReceivedOn") = dtTab.Rows(i)("ADRL_ReceivedOn")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_AttachID")) = False Then
                        dRow("AttachID") = dtTab.Rows(i)("ADRL_AttachID")
                    Else
                        dRow("AttachID") = 0
                    End If

                    If IsDBNull(dtTab.Rows(i)("ATCH_DocID")) = False Then
                        dRow("DocID") = dtTab.Rows(i)("ATCH_DocID")
                    Else
                        dRow("DocID") = 0
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_LogStatus")) = False Then
                        If dtTab.Rows(i)("ADRL_LogStatus") = 1 Then
                            dRow("Status") = "Outstanding"
                        ElseIf dtTab.Rows(i)("ADRL_LogStatus") = 2 Then
                            dRow("Status") = "Acceptable"
                        ElseIf dtTab.Rows(i)("ADRL_LogStatus") = 3 Then
                            dRow("Status") = "Partially"
                        ElseIf dtTab.Rows(i)("ADRL_LogStatus") = 4 Then
                            dRow("Status") = "No"
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDRLAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("PKID")
            dtAttach.Columns.Add("AttachID")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select ATCH_ID, Atch_DocID, ATCH_FNAME, ATCH_EXT, ATCH_Desc, ATCH_CreatedBy, ATCH_CREATEDON, ATCH_SIZE, ADRL_AttachID, ADRL_ID "
            sSql = sSql & " From Audit_DRLLog left join edt_attachments on ATCH_ID=ADRL_AttachID where ATCH_CompID=" & iACID & " And ADRL_ID = " & iPKID & " And ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("PKID") = dt.Rows(i)("ADRL_ID")
                drow("AttachID") = dt.Rows(i)("ATCH_ID")
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function CheckAttachID(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select ADRL_AttachID from Audit_DRLLog Where ADRL_CompID=" & iACID & " And ADRL_ID=" & iPKID & " and ADRL_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveDRLLogAttachment(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iReceivedID As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_DRLLog set ADRL_AttachID=" & iAttachID & " Where ADRL_CompID=" & iACID & " And ADRL_ID=" & iReceivedID & " and ADRL_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDRLPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer, ByVal iRequestedID As Integer, ByVal iReceivedID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select ADRL_ID  From Audit_DRLLog Where ADRL_AuditNo=" & iAuditNo & " And ADRL_FunID = " & iFunID & " And ADRL_RequestedListID = " & iRequestedID & " And ADRL_RequestedTypeID =" & iReceivedID & " And ADRL_CompID =" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCheckPointDRL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer, ByVal iRequestedID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select ADRL_ID from Audit_DRLLog Where ADRL_AuditNo=" & iAuditNo & " And ADRL_FunID=" & iFunID & " And ADRL_CustID=" & iCustID & " And ADRL_RequestedListID=" & iRequestedID & " And ADRL_CompID=" & iACID & " And ADRL_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComment(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ADRL_Comments  From Audit_DRLLog Where ADRL_ID=" & iPKID & " And ADRL_CompID =" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDRLLogDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iADRLID As Integer, ByVal iYearID As Integer, ByVal iAttachID As Integer,
                                   ByVal sReceivedOn As String, ByVal iLogStatus As Integer, ByVal sComments As String, ByVal iUserID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_DRLLog set  ADRL_ReceivedOn='" & sReceivedOn & "',ADRL_LogStatus=" & iLogStatus & ",ADRL_ReceivedComments='" & sComments & "', "
            sSql = sSql & " ADRL_Status='Updated',ADRL_UpdatedBy=" & iUserID & ",ADRL_UpdatedOn =GetDate(),ADRL_AttachID=" & iAttachID & ""
            sSql = sSql & " Where ADRL_CompID=" & iACID & " And ADRL_ID=" & iADRLID & " and ADRL_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadSelectListDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditNo As Integer, ByVal iRequestedID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ADRL_ReceivedID,ADRL_Name,ADRL_OverALLComments From Audit_DRLLog  Left Join Audit_Doc_Request_List On ADRL_DRLID=ADRL_ReceivedID "
            sSql = sSql & "Where ADRL_RequestedID=" & iRequestedID & " And ADRL_AuditNo=" & iAuditNo & " And ADRL_YearID=" & iYearID & " And ADRL_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDRLDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer, ByVal iCustID As Integer, ByVal iDRListID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_Desc,ACM_Checkpoint,ADRL_FunID,ADRL_RequestedListID,ADRL_RequestedTypeID,ADRL_RequestedOn,ADRL_EmailID,ADRL_Comments,ADRL_Status,ADRL_ReceivedComments,ADRL_LogStatus,ADRL_ReceivedOn,ADRL_TimlinetoResOn,"
            sSql = sSql & " ADRL_AttachID From Audit_DRLLog "
            sSql = sSql & " Left Join AuditType_Checklist_Master On ACM_ID=ADRL_FunID "
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=ADRL_RequestedListID"
            sSql = sSql & " Where ADRL_CustID=" & iCustID & " And ADRL_YearID=" & iYearID & " And ADRL_CompID=" & iACID & " "
            sSql = sSql & " And ADRL_RequestedListID=" & iDRListID & " "
            If iAuditNo > 0 Then
                sSql = sSql & " And ADRL_AuditNo=" & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And ADRL_FunID=" & iFunID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDRLdgToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DocumentRequestedList")
            dt.Columns.Add("DocumentRequestedType")
            dt.Columns.Add("EmailID")
            dt.Columns.Add("RequestedOn")
            dt.Columns.Add("Comments")
            dt.Columns.Add("Status")
            dt.Columns.Add("ReceivedComments")
            dt.Columns.Add("ReceivedOn")

            sSql = "select CMM_ID,CMM_Desc,DRL_DRLID,DRL_Name,ADRL_ID,ADRL_YearID,ADRL_AuditNo,ADRL_FunID,ADRL_CustID,ADRL_RequestedListID,ADRL_RequestedTypeID,ADRL_RequestedOn, "
            sSql = sSql & " ADRL_EmailID,ADRL_Comments ,ADRL_Status ,ADRL_AttachID,ADRL_CompID,ADRL_ReceivedComments,ADRL_LogStatus,ADRL_ReceivedOn From Audit_DRLLog"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=ADRL_RequestedListID And CMM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_Doc_Request_List On DRL_DRLID=ADRL_RequestedTypeID And DRL_CompID=" & iACID & " Where ADRL_CompID = " & iACID & " "
            If iAuditNo > 0 Then
                sSql = sSql & " And ADRL_AuditNo = " & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And ADRL_FunID = " & iFunID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtTab.Rows(i)("CMM_Desc")) = False Then
                        dRow("DocumentRequestedList") = dtTab.Rows(i)("CMM_Desc")
                    End If
                    If IsDBNull(dtTab.Rows(i)("DRL_Name")) = False Then
                        dRow("DocumentRequestedType") = dtTab.Rows(i)("DRL_Name")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_RequestedOn")) = False Then
                        dRow("RequestedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("ADRL_RequestedOn"), "F")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_EmailID")) = False Then
                        dRow("EmailID") = dtTab.Rows(i)("ADRL_EmailID")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_Comments")) = False Then
                        dRow("Comments") = dtTab.Rows(i)("ADRL_Comments")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_ReceivedComments")) = False Then
                        dRow("ReceivedComments") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("ADRL_ReceivedComments"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_ReceivedOn")) = False Then
                        dRow("ReceivedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("ADRL_ReceivedOn"), "F")
                    End If
                    If IsDBNull(dtTab.Rows(i)("ADRL_LogStatus")) = False Then
                        If dtTab.Rows(0)("ADRL_LogStatus") = 1 Then
                            dRow("Status") = "Outstanding"
                        ElseIf dtTab.Rows(0)("ADRL_LogStatus") = 2 Then
                            dRow("Status") = "Acceptable"
                        ElseIf dtTab.Rows(0)("ADRL_LogStatus") = 3 Then
                            dRow("Status") = "Partially"
                        ElseIf dtTab.Rows(0)("ADRL_LogStatus") = 4 Then
                            dRow("Status") = "No"
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTaskCode(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AAS_ID,AAS_AssignmentNo From AuditAssignment_Schedule where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & " "
            End If
            sSql = sSql & "  order by AAS_ID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustAllUserEmails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustomerID As Integer) As String
        Dim sSql As String
        Try
            sSql = "SELECT ISNULL(STUFF((SELECT DISTINCT ';' + Usr_Email FROM Sad_UserDetails WHERE Usr_Companyid=" & iCustomerID & " And Usr_Email<>'' And Usr_CompId=" & iAcID & " And Usr_Email IS NOT NULL FOR XML PATH('')), 1, 2, ''),'')"
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub updateStandardAudit_Audit_DRLLog_RemarksHistory(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iCheckPointID As String, ByVal sRemarks As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal sEmailIds As String, ByVal sRespondTime As String, ByVal iFinancialId As String)
        Dim sSql As String
        Try
            Dim iMaxId As Integer = objDBL.SQLExecuteScalar(sAc, "select IsNull(Max(SAR_ID) + 1,1) from StandardAudit_Audit_DRLLog_RemarksHistory")
            sSql = "" : sSql = "Insert into StandardAudit_Audit_DRLLog_RemarksHistory (SAR_ID,SAR_SA_ID,SAR_SAC_ID,SAR_CheckPointIDs,SAR_RemarksType,SAR_Remarks,SAR_RemarksBy,SAR_Date,SAR_IPAddress,SAR_CompID,SAR_EmailIds,SAR_TimlinetoResOn,sar_Yearid) Values"
            sSql = sSql & "(" & iMaxId & "," & iAuditID & "," & iCustID & ",'" & iCheckPointID & "','C','" & sRemarks & "'," & iUserID & ",GetDate(),'" & sIPAddress & "'," & iAcID & ",'" & sEmailIds & "','" & sRespondTime & "','" & iFinancialId & "')"
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub updateStandardAudit_Audit_DRLLog_RemarksUserHistory(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iCheckPointID As String, ByVal sRemarks As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal sEmailIds As String, ByVal iFinancialId As String)
        Dim sSql As String
        Try
            Dim iMaxId As Integer = objDBL.SQLExecuteScalar(sAc, "select IsNull(Max(SAR_ID) + 1,1) from StandardAudit_Audit_DRLLog_RemarksHistory")
            sSql = "" : sSql = "Insert into StandardAudit_Audit_DRLLog_RemarksHistory (SAR_ID,SAR_SA_ID,SAR_SAC_ID,SAR_CheckPointIDs,SAR_RemarksType,SAR_Remarks,SAR_RemarksBy,SAR_Date,SAR_IPAddress,SAR_CompID,SAR_EmailIds,sar_Yearid) Values"
            sSql = sSql & "(" & iMaxId & "," & iAuditID & "," & iCustID & ",'" & iCheckPointID & "','RC','" & sRemarks & "'," & iUserID & ",GetDate(),'" & sIPAddress & "'," & iAcID & ",'" & sEmailIds & "','" & iFinancialId & "')"
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub updateStandardAudit_ConductAudit_RemarksHistory(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iCheckPointID As String, ByVal sRemarks As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal sEmailIds As String)
        Dim sSql As String
        Try
            Dim iMaxId As Integer = objDBL.SQLExecuteScalar(sAc, "select IsNull(Max(SCR_ID) + 1,1) from StandardAudit_ConductAudit_RemarksHistory")
            sSql = "" : sSql = "Insert into StandardAudit_ConductAudit_RemarksHistory (SCR_ID,SCR_SA_ID,SCR_SAC_ID,SCR_CheckPointID,SCR_Remarks,SCR_RemarksBy,SCR_Date,SCR_IPAddress,SCR_CompID,SCR_EmailIds) Values"
            sSql = sSql & "(" & iMaxId & "," & iAuditID & "," & iCustID & ",'" & iCheckPointID & "','" & sRemarks & "'," & iUserID & ",GetDate(),'" & sIPAddress & "'," & iAcID & ",'" & sEmailIds & "')"
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub updateStandardAudit_Update_RemarksHistory(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iCheckPointID As String, ByVal sRemarks As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal sEmailIds As String, ByVal sRespondTime As String, ByVal iFinancialId As String)
        Dim sSql As String
        Try
            Dim iMaxId As Integer = objDBL.SQLExecuteScalar(sAc, "select IsNull(Max(SAR_ID) + 1,1) from StandardAudit_Audit_DRLLog_RemarksHistory")
            sSql = "" : sSql = "Insert into StandardAudit_Audit_DRLLog_RemarksHistory (SAR_ID,SAR_SA_ID,SAR_SAC_ID,SAR_CheckPointIDs,SAR_RemarksType,SAR_Remarks,SAR_RemarksBy,SAR_Date,SAR_IPAddress,SAR_CompID,SAR_EmailIds,SAR_TimlinetoResOn,sar_Yearid) Values"
            sSql = sSql & "(" & iMaxId & "," & iAuditID & "," & iCustID & ",'" & iCheckPointID & "','RC','" & sRemarks & "'," & iUserID & ",GetDate(),'" & sIPAddress & "'," & iAcID & ",'" & sEmailIds & "','" & sRespondTime & "','" & iFinancialId & "')"
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCustrecievedremarksHistory(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iFinancialYearID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataSet
        Try
            sSql = " SELECT 'Collection of Data' as Notification,c.SA_AuditNo as AuditNo,d.ACM_Checkpoint as Description,Convert(Varchar(10),SAR_Date,103) As Date,SAR_Remarks as Observations,"
            sSql = sSql & " a.usr_LoginName + ' - ' + a.Usr_FullName  as Comments_By,Case when SAR_RemarksType='C' then 'Auditor' when SAR_RemarksType='RC' then 'Client' end as Role"
            sSql = sSql & " FROM StandardAudit_Audit_DRLLog_RemarksHistory"
            sSql = sSql & " Left Join sad_userdetails a on a.Usr_ID=SAR_RemarksBy"
            sSql = sSql & " left join SAD_CUSTOMER_MASTER b on b.CUST_ID=SAR_SAC_ID"
            sSql = sSql & " left join StandardAudit_Schedule c on c.sa_id=sar_sa_id"
            sSql = sSql & " left join AuditType_Checklist_Master d on d.ACM_ID = SAR_CheckPointIDs"
            sSql = sSql & " where SAR_SAC_ID=" & iCustID & " and sar_sa_id =" & iAuditID & " and sar_Yearid=" & iFinancialYearID & " and SAR_CompID=" & iAcID & ""
            sSql = sSql & " ORDER BY SAR_ID DESC"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
