
Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure strAUDClouser
    Private ASO_PKID As Integer
    Private ASO_YearID As Integer
    Private ASO_AuditCodeID As Integer
    Private ASO_FunctionID As Integer
    Private ASO_CustID As Integer
    Private ASO_MasterID As Integer
    Private ASO_SignOffStatus As String
    Private ASO_Comments As String
    Private ASO_OverAllComments As String
    Private ASO_Status As String
    Private ASO_CrBy As Integer
    Private ASO_UpdatedBy As Integer
    Private ASO_AttachID As Integer
    Private ASO_IPAddress As String
    Private ASO_CompID As Integer
    Private ASO_AuditRatingID As Integer
    Private ASO_KeyObservation As String
    Public Property iASO_AuditRatingID() As Integer
        Get
            Return (ASO_AuditRatingID)
        End Get
        Set(ByVal Value As Integer)
            ASO_AuditRatingID = Value
        End Set
    End Property
    Public Property iASO_PKID() As Integer
        Get
            Return (ASO_PKID)
        End Get
        Set(ByVal Value As Integer)
            ASO_PKID = Value
        End Set
    End Property
    Public Property iASO_FunctionID() As Integer
        Get
            Return (ASO_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            ASO_FunctionID = Value
        End Set
    End Property
    Public Property iASO_CustID() As Integer
        Get
            Return (ASO_CustID)
        End Get
        Set(ByVal Value As Integer)
            ASO_CustID = Value
        End Set
    End Property
    Public Property iASO_YearID() As Integer
        Get
            Return (ASO_YearID)
        End Get
        Set(ByVal Value As Integer)
            ASO_YearID = Value
        End Set
    End Property
    Public Property iASO_AuditCodeID() As Integer
        Get
            Return (ASO_AuditCodeID)
        End Get
        Set(ByVal Value As Integer)
            ASO_AuditCodeID = Value
        End Set
    End Property
    Public Property iASO_MasterID() As Integer
        Get
            Return (ASO_MasterID)
        End Get
        Set(ByVal Value As Integer)
            ASO_MasterID = Value
        End Set
    End Property
    Public Property sASO_SignOffStatus() As String
        Get
            Return (ASO_SignOffStatus)
        End Get
        Set(ByVal Value As String)
            ASO_SignOffStatus = Value
        End Set
    End Property
    Public Property sASO_KeyObservation() As String
        Get
            Return (ASO_KeyObservation)
        End Get
        Set(ByVal Value As String)
            ASO_KeyObservation = Value
        End Set
    End Property
    Public Property sASO_Comments() As String
        Get
            Return (ASO_Comments)
        End Get
        Set(ByVal Value As String)
            ASO_Comments = Value
        End Set
    End Property

    Public Property sASO_OverAllComments() As String
        Get
            Return (ASO_OverAllComments)
        End Get
        Set(ByVal Value As String)
            ASO_OverAllComments = Value
        End Set
    End Property
    Public Property sASO_Status() As String
        Get
            Return (ASO_Status)
        End Get
        Set(ByVal Value As String)
            ASO_Status = Value
        End Set
    End Property
    Public Property iASO_CrBy() As Integer
        Get
            Return (ASO_CrBy)
        End Get
        Set(ByVal Value As Integer)
            ASO_CrBy = Value
        End Set
    End Property
    Public Property iASO_UpdatedBy() As Integer
        Get
            Return (ASO_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ASO_UpdatedBy = Value
        End Set
    End Property
    Public Property iASO_AttachID() As String
        Get
            Return (ASO_AttachID)
        End Get
        Set(ByVal Value As String)
            ASO_AttachID = Value
        End Set
    End Property
    Public Property sASO_IPAddress() As String
        Get
            Return (ASO_IPAddress)
        End Get
        Set(ByVal Value As String)
            ASO_IPAddress = Value
        End Set
    End Property
    Public Property iASO_CompID() As Integer
        Get
            Return (ASO_CompID)
        End Get
        Set(ByVal Value As Integer)
            ASO_CompID = Value
        End Set
    End Property
End Structure
Public Class clsQAAuditClosure
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Public Function LoadClouseDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable, dtAuditSignOff
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("PKID")
            dt.Columns.Add("cmm_Id")
            dt.Columns.Add("cmm_Desc")
            dt.Columns.Add("Status")
            dt.Columns.Add("Comments")

            sSql = "Select * from QAAudit_SignOff where ASO_CompID=" & iACID & ""
            If iAuditNo > 0 Then
                sSql = sSql & " And ASO_AuditCodeID=" & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And ASO_FunctionID=" & iFunID & ""
            End If
            dtAuditSignOff = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtAuditSignOff.Rows.Count > 0 Then
                sSql = "Select ASO_PKID,ASO_MasterID,ASO_Comments,ASO_SignOffStatus,ASO_OverAllComments,cmm_Id,cmm_Desc from QAAudit_SignOff"
                sSql = sSql & " Left Join  Content_Management_Master On cmm_Id=ASO_MasterID And Cmm_Category='ASF' and CMM_CompID=" & iACID & " "
                sSql = sSql & " Where ASO_YearID=" & iYearID & " And CMM_Delflag='A' "
                If iAuditNo > 0 Then
                    sSql = sSql & " And ASO_AuditCodeID=" & iAuditNo & ""
                End If
                If iFunID > 0 Then
                    sSql = sSql & " And ASO_FunctionID=" & iFunID & ""
                End If
                dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtTab.Rows.Count > 0 Then
                    For i = 0 To dtTab.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("PKID") = dtTab.Rows(i)("ASO_PKID")
                        dRow("cmm_Id") = dtTab.Rows(i)("ASO_MasterID")
                        dRow("cmm_Desc") = dtTab.Rows(i)("cmm_Desc")
                        dRow("Status") = dtTab.Rows(i)("ASO_SignOffStatus")
                        dRow("Comments") = dtTab.Rows(i)("ASO_Comments")
                        dt.Rows.Add(dRow)
                    Next
                End If
            Else
                sSql = "Select cmm_Id,cmm_Desc from Content_Management_Master where Cmm_Category='ASF' and CMM_CompID=" & iACID & " and CMM_Delflag='A' "
                dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtTab.Rows.Count > 0 Then
                    For i = 0 To dtTab.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("PKID") = "0"
                        dRow("cmm_Id") = dtTab.Rows(i)("cmm_Id")
                        dRow("cmm_Desc") = dtTab.Rows(i)("cmm_Desc")
                        dRow("Status") = "Y"
                        dRow("Comments") = String.Empty
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFieldWorkGridDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Dim dRow As DataRow
        Try
            dt.Columns.Add("WPID")
            dt.Columns.Add("WorkPaper")
            dt.Columns.Add("AuditCodeID")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("SubFunctionID")
            dt.Columns.Add("ProcessID")
            dt.Columns.Add("SubProcessID")
            dt.Columns.Add("RiskID")
            dt.Columns.Add("ControlID")
            dt.Columns.Add("AuditChecksID")
            dt.Columns.Add("CustID")
            sSql = "Select AWP_PKID,AWP_CustID,AWP_WorkPaperNo,AWP_AuditCode,AWP_FunctionID,AWP_SubFunctionID,AWP_ProcessID,AWP_SubProcessID,AWP_RiskID,AWP_ControlID,AWP_ChecksID"
            sSql = sSql & " From Audit_WorkPaper WHere  AWP_CompID=" & iACID & " And AWP_Status<>'Submitted'"
            If iAuditNo > 0 Then
                sSql = sSql & " And AWP_AuditCode=" & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And AWP_FunctionID=" & iFunID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("WPID") = dtTab.Rows(i)("AWP_PKID")
                    dRow("CustID") = dtTab.Rows(i)("AWP_CustID")
                    dRow("WorkPaper") = dtTab.Rows(i)("AWP_WorkPaperNo")
                    dRow("AuditCodeID") = dtTab.Rows(i)("AWP_AuditCode")
                    dRow("FunctionID") = dtTab.Rows(i)("AWP_FunctionID")
                    dRow("SubFunctionID") = dtTab.Rows(i)("AWP_SubFunctionID")
                    dRow("ProcessID") = dtTab.Rows(i)("AWP_ProcessID")
                    dRow("SubProcessID") = dtTab.Rows(i)("AWP_SubProcessID")
                    dRow("RiskID") = dtTab.Rows(i)("AWP_RiskID")
                    dRow("ControlID") = dtTab.Rows(i)("AWP_ControlID")
                    dRow("AuditChecksID") = dtTab.Rows(i)("AWP_ChecksID")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueGridDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer)
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Dim dRow As DataRow
        Try
            dt.Columns.Add("IssueID")
            dt.Columns.Add("AuditID")
            dt.Columns.Add("WPID")
            dt.Columns.Add("IssueJobNo")
            dt.Columns.Add("CustID")
            dt.Columns.Add("FunctionID")
            sSql = "Select AIT_PKID,AIT_IssueJobNo,AIT_AuditCode,AIT_FunctionID,AIT_CustID,AIT_WorkPaperID From  Audit_IssueTracker_details Where AIT_CompID=" & iACID & " And AIT_Status <> 'Submitted'"
            If iAuditNo > 0 Then
                sSql = sSql & " And AIT_AuditCode=" & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And AIT_FunctionID=" & iFunID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("CustID") = dtTab.Rows(i)("AIT_CustID")
                    dRow("FunctionID") = dtTab.Rows(i)("AIT_FunctionID")
                    dRow("IssueID") = dtTab.Rows(i)("AIT_PKID")
                    dRow("AuditID") = dtTab.Rows(i)("AIT_AuditCode")
                    dRow("WPID") = dtTab.Rows(i)("AIT_WorkPaperID")
                    dRow("IssueJobNo") = dtTab.Rows(i)("AIT_IssueJobNo")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveClouserDetails(ByVal sAC As String, ByVal objClouser As strClouser)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_AuditCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_AuditRatingID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_AuditRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_SignOffStatus", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objClouser.sASO_SignOffStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_Comments", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objClouser.sASO_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_OverAllComments", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objClouser.sASO_OverAllComments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_KeyObservation", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objClouser.sASO_KeyObservation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objClouser.sASO_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASO_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objClouser.iASO_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spQAAudit_SignOff", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComment(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ASO_OverAllComments,ASO_AttachID,ASO_AuditRatingID,ASO_KeyObservation from QAAudit_SignOff where ASO_CompID=" & iACID & " And  ASO_YearID=" & iYearID & " "
            If iAuditID > 0 Then
                sSql = sSql & " And ASO_AuditCodeID=" & iAuditID & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And ASO_FunctionID=" & iFunID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFieldWorkSubmittedDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select QAW_PKID From  QA_Workpaper WHere QAW_AuditCode=" & iAuditID & " and QAW_CompID=" & iACID & "  And QAW_Status = 'Submitted'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueSubmittedDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select AIT_PKID from Audit_IssueTracker_details Where AIT_AuditCode=" & iAuditID & " And AIT_CompID=" & iACID & " And AIT_Status = 'Submitted'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitAuditSignOff(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Try
            sSql = "Update QAAudit_SignOff set ASO_SubmittedBy=" & iUserID & ",ASO_SubmittedOn =Getdate(),ASO_Status='Submitted' where ASO_AuditCodeID=" & iAuditID & " and ASO_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetFolderNames(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("FolderID")
            dt.Columns.Add("FolderName")
            dRow = dt.NewRow
            dRow("FolderID") = 1
            dRow("FolderName") = "2.1 Audit Planning Memorandum"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 2
            dRow("FolderName") = "2.4 Audit Plan Sign Off"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 3
            dRow("FolderName") = "3.1 Work Paper"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 4
            dRow("FolderName") = "3.2 Issue Tracker"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 5
            dRow("FolderName") = "3.3 Risk Assessment Checklist"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 6
            dRow("FolderName") = "4.3 Report"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 7
            dRow("FolderName") = "4.4 Executive Summary"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 8
            dRow("FolderName") = "5.1 Closure"
            dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAttachmentNames(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iCustID As Integer, ByVal iFolderID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim sAttachment As String = ""
        Try
            If iFolderID = 1 Then
                sSql = "Select ATCH_ID, ATCH_DOCID,(ATCH_FName + '.' + ATCH_EXT) as ATCH_FName,ATCH_CreatedBy,ATCH_CreatedOn from Audit_APM_Details Left Join EDT_Attachments On APM_AttachID=ATCH_ID And ATCH_CompID=" & iACID & ""
                sSql = sSql & " Where APM_CompID=" & iACID & " And APM_ID=" & iAuditCodeID & " And APM_CustID=" & iCustID & " And APM_YearID=" & iYearID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            ElseIf iFolderID = 2 Then
                sSql = "Select ATCH_ID, ATCH_DOCID,(ATCH_FName + '.' + ATCH_EXT) as ATCH_FName,ATCH_CreatedBy,ATCH_CreatedOn from Audit_PlanSignOff Left Join EDT_Attachments On APSO_AttachID=ATCH_ID And ATCH_CompID=" & iACID & ""
                sSql = sSql & " Where APSO_CompID=" & iACID & " And APSO_AuditCode=" & iAuditCodeID & " And APSO_CustID=" & iCustID & " And APSO_YearID=" & iYearID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            ElseIf iFolderID = 3 Then
                sSql = "Select ATCH_ID, ATCH_DOCID,(ATCH_FName + '.' + ATCH_EXT) as ATCH_FName,ATCH_CreatedBy,ATCH_CreatedOn,AWP_WorkPaperNo from Audit_WorkPaper Left Join EDT_Attachments On AWP_AttachID=ATCH_ID And ATCH_CompID=" & iACID & ""
                sSql = sSql & " Where AWP_CompID=" & iACID & " And AWP_AuditCode=" & iAuditCodeID & " And AWP_CustID=" & iCustID & " And AWP_YearID=" & iYearID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            ElseIf iFolderID = 4 Then
                sSql = "Select ATCH_ID, ATCH_DOCID,(ATCH_FName + '.' + ATCH_EXT) as ATCH_FName,ATCH_CreatedBy,ATCH_CreatedOn,AIT_IssueJobNo from Audit_IssueTracker_Details Left Join EDT_Attachments On AIT_AttachID=ATCH_ID And ATCH_CompID=" & iACID & ""
                sSql = sSql & " Where AIT_CompID=" & iACID & " And AIT_AuditCode=" & iAuditCodeID & " And AIT_CustID=" & iCustID & " And AIT_YearID=" & iYearID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            ElseIf iFolderID = 5 Then
            ElseIf iFolderID = 6 Then
            ElseIf iFolderID = 7 Then
            ElseIf iFolderID = 8 Then
                sSql = "Select ATCH_ID, ATCH_DOCID,(ATCH_FName + '.' + ATCH_EXT) as ATCH_FName,ATCH_CreatedBy,ATCH_CreatedOn from QAAudit_SignOff Left Join EDT_Attachments On ASO_AttachID=ATCH_ID And ATCH_CompID=" & iACID & ""
                sSql = sSql & " Where ASO_CompID=" & iACID & " And ASO_AuditCodeID=" & iAuditCodeID & " And ASO_CustID=" & iCustID & " And ASO_YearID=" & iYearID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAttachmentPath(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer) As String
        Dim con As New OleDb.OleDbConnection
        Dim Pdr As OleDb.OleDbDataReader
        Dim sSql As String, sDBPath As String, sDBFilePath As String
        Try
            sSql = "Select ATCH_DocId,ATCH_FNAME,atch_ext from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
            Pdr = objDBL.SQLDataReader(sAC, sSql)
            If Pdr.HasRows Then
                While Pdr.Read()
                    sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                    If sDBPath.EndsWith("\") = False Then
                        sDBPath = sDBPath & "\Attachments\" & Pdr("ATCH_DocId") \ 301
                    Else
                        sDBPath = sDBPath & "Attachments\" & Pdr("ATCH_DocId") \ 301
                    End If
                    If System.IO.Directory.Exists(sDBPath) = True Then
                        sDBFilePath = sDBPath & "\" & Pdr("ATCH_DocId") & "." & Pdr("atch_ext")
                        If System.IO.File.Exists(sDBFilePath) = True Then
                            Return sDBFilePath
                        Else
                            Return ""
                        End If
                    End If
                End While
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappedRiskControlMatrixinWorkPaperNotStarted(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("MMMID")
            dtTab.Columns.Add("FunctionId")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("RisK")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("Checks")

            sSql = "Select APMCM_PKID,APMCM_APMPKID,APMCM_YearID,APMCM_CustID,APMCM_FunctionID,APMCM_SubFunctionID,APMCM_ProcessID,"
            sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,AWP_WorkPaperNo,AWP_Status,AWP_PKID,"
            sSql = sSql & " APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APMCM_MMMID From Audit_APM_ChecksMatrix"
            sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=APMCM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=APMCM_MMMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_WorkPaper On AWP_AuditCode=APMCM_APMPKID And AWP_CompID=" & iACID & "And AWP_FunctionID=APMCM_FunctionID And AWP_SubFunctionID=APMCM_SubFunctionID And"
            sSql = sSql & " AWP_ProcessID=APMCM_ProcessID And AWP_SubProcessID=APMCM_SubProcessID And AWP_RiskID=APMCM_RiskID And AWP_ControlID=APMCM_ControlID And AWP_ChecksID=APMCM_ChecksID "
            sSql = sSql & " Where APMCM_YearID=" & iYearID & " And APMCM_FunctionID=" & iFunctionID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_CustID=" & iCustID & "AND AWP_Status IS NUll"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("FunctionId") = 0 : dr("SubFunctionID") = 0 : dr("ProcessID") = 0 : dr("SubProcessID") = 0 : dr("RisKID") = 0 : dr("ControlID") = 0 : dr("ChecksID") = 0
                dr("SubFunction") = "" : dr("Process") = "" : dr("SubProcess") = "" : dr("Checks") = ""
                If IsDBNull(dt.Rows(i)("MMM_ID")) = False Then
                    dr("MMMID") = dt.Rows(i)("MMM_ID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_FunctionID")) = False Then
                    dr("FunctionId") = dt.Rows(i)("APMCM_FunctionID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_SubFunctionID")) = False Then
                    dr("SubFunctionID") = dt.Rows(i)("APMCM_SubFunctionID")
                End If
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ProcessID")) = False Then
                    dr("ProcessID") = dt.Rows(i)("APMCM_ProcessID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_SubProcessID")) = False Then
                    dr("SubProcessID") = dt.Rows(i)("APMCM_SubProcessID")
                End If
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_RiskID")) = False Then
                    dr("RisKID") = dt.Rows(i)("APMCM_RiskID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_RiskID")) = False Then
                    dr("RisK") = dt.Rows(i)("MMM_Risk")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ControlID")) = False Then
                    dr("ControlID") = dt.Rows(i)("APMCM_ControlID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ControlID")) = False Then
                    dr("Control") = dt.Rows(i)("MMM_Control")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ChecksID")) = False Then
                    dr("ChecksID") = dt.Rows(i)("APMCM_ChecksID")
                End If
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadClouseDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable, dtAuditSignOff As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AuditSignOff")
            dt.Columns.Add("Status")
            dt.Columns.Add("Comments")

            sSql = "Select * from QAAudit_SignOff where ASO_CompID=" & iACID & ""
            If iAuditNo > 0 Then
                sSql = sSql & " And ASO_AuditCodeID=" & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And ASO_FunctionID=" & iFunID & ""
            End If
            dtAuditSignOff = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtAuditSignOff.Rows.Count > 0 Then
                sSql = "Select ASO_PKID,ASO_MasterID,ASO_Comments,ASO_SignOffStatus,ASO_OverAllComments,cmm_Id,cmm_Desc from QAAudit_SignOff"
                sSql = sSql & " Left Join  Content_Management_Master On cmm_Id=ASO_MasterID And Cmm_Category='ASF' and CMM_CompID=" & iACID & ""
                sSql = sSql & "Where ASO_YearID=" & iYearID & " "
                If iAuditNo > 0 Then
                    sSql = sSql & " And ASO_AuditCodeID=" & iAuditNo & ""
                End If
                If iFunID > 0 Then
                    sSql = sSql & " And ASO_FunctionID=" & iFunID & ""
                End If
                dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtTab.Rows.Count > 0 Then
                    For i = 0 To dtTab.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("SrNo") = i + 1
                        dRow("AuditSignOff") = dtTab.Rows(i)("cmm_Desc")
                        If dtTab.Rows(i)("ASO_SignOffStatus") = "Y" Then
                            dRow("Status") = "Yes"
                        Else
                            dRow("Status") = "No"
                        End If
                        dRow("Comments") = dtTab.Rows(i)("ASO_Comments")
                        dt.Rows.Add(dRow)
                    Next
                End If
            Else
                sSql = "Select cmm_Id,cmm_Desc from Content_Management_Master where Cmm_Category='ASF' and CMM_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtTab.Rows.Count > 0 Then
                    For i = 0 To dtTab.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("SrNo") = i + 1
                        dRow("AuditSignOff") = dtTab.Rows(i)("cmm_Desc")
                        dRow("Status") = "Yes"
                        dRow("Comments") = String.Empty
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCommentToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("Conclusion")
            dt.Columns.Add("Observation")
            sSql = "Select ASO_OverAllComments,ASO_AuditRatingID,ASO_KeyObservation from QAAudit_SignOff where ASO_CompID=" & iACID & " And  ASO_YearID=" & iYearID & " "
            If iAuditID > 0 Then
                sSql = sSql & " And ASO_AuditCodeID=" & iAuditID & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And ASO_FunctionID=" & iFunID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            dRow = dt.NewRow
            If IsDBNull(dtTab.Rows(0)("ASO_OverAllComments")) = False Then
                dRow("Conclusion") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(0)("ASO_OverAllComments"))
            End If
            If IsDBNull(dtTab.Rows(0)("ASO_KeyObservation")) = False Then
                dRow("Observation") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(0)("ASO_KeyObservation"))
            End If
            dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFieldWorkGridDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("WorkPaper")
            sSql = "Select AWP_PKID,AWP_CustID,AWP_WorkPaperNo,AWP_AuditCode,AWP_FunctionID,AWP_SubFunctionID,AWP_ProcessID,AWP_SubProcessID,AWP_RiskID,AWP_ControlID,AWP_ChecksID"
            sSql = sSql & " From Audit_WorkPaper WHere  AWP_CompID=" & iACID & " And AWP_Status<>'Submitted'"
            If iAuditNo > 0 Then
                sSql = sSql & " And AWP_AuditCode=" & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And AWP_FunctionID=" & iFunID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    dRow("WorkPaper") = dtTab.Rows(i)("AWP_WorkPaperNo")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueGridDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditNo As Integer, ByVal iFunID As Integer)
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("IssueJobNo")
            sSql = "Select AIT_PKID,AIT_IssueJobNo,AIT_AuditCode,AIT_FunctionID,AIT_CustID,AIT_WorkPaperID From  Audit_IssueTracker_details Where AIT_CompID=" & iACID & " And AIT_Status <> 'Submitted'"
            If iAuditNo > 0 Then
                sSql = sSql & " And AIT_AuditCode=" & iAuditNo & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And AIT_FunctionID=" & iFunID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    dRow("IssueJobNo") = dtTab.Rows(i)("AIT_IssueJobNo")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappedRiskControlMatrixinWorkPaperNotStartedToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("Risks")
            dtTab.Columns.Add("Controls")
            dtTab.Columns.Add("Checks")

            sSql = "Select APMCM_PKID,APMCM_APMPKID,APMCM_YearID,APMCM_CustID,APMCM_FunctionID,APMCM_SubFunctionID,APMCM_ProcessID,"
            sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,AWP_WorkPaperNo,AWP_Status,AWP_PKID,"
            sSql = sSql & " APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APMCM_MMMID From Audit_APM_ChecksMatrix"
            sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=APMCM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=APMCM_MMMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_WorkPaper On AWP_AuditCode=APMCM_APMPKID And AWP_CompID=" & iACID & "And AWP_FunctionID=APMCM_FunctionID And AWP_SubFunctionID=APMCM_SubFunctionID And"
            sSql = sSql & " AWP_ProcessID=APMCM_ProcessID And AWP_SubProcessID=APMCM_SubProcessID And AWP_RiskID=APMCM_RiskID And AWP_ControlID=APMCM_ControlID And AWP_ChecksID=APMCM_ChecksID "
            sSql = sSql & " Where APMCM_YearID=" & iYearID & " And APMCM_FunctionID=" & iFunctionID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_CustID=" & iCustID & "AND AWP_Status IS NUll"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SubFunction") = "" : dr("Process") = "" : dr("SubProcess") = "" : dr("Risks") = "" : dr("Controls") = "" : dr("Checks") = ""
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_RiskID")) = False Then
                    dr("Risks") = dt.Rows(i)("MMM_Risk")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ControlID")) = False Then
                    dr("Controls") = dt.Rows(i)("MMM_Control")
                End If
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
