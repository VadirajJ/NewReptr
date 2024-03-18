Imports System.Data
Imports DatabaseLayer
Public Class clsSampling
    Private objDBL As New DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadDRLLOg(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Try
            dt.Columns.Add("DRLID")
            dt.Columns.Add("DRLName")
            dt.Columns.Add("RequestedDocNameID")
            dt.Columns.Add("RequestedDocName")
            dt.Columns.Add("Requestedon")
            dt.Columns.Add("Receivecdoc")
            dt.Columns.Add("Receivedon")
            dt.Columns.Add("AttachID")
            dt.Columns.Add("DocID")

            sSql = "select c.DRL_Name,ADRL_ID,ADRL_RequestedListID,ADRL_RequestedTypeID,ADRL_AttachID,b.ATCH_DocID,a.cmm_desc as DocumentName,b.ATCH_FNAME as AttachmentName,"
            sSql = sSql & " c.DRL_Name as RequestedDocument, Convert(Varchar(10),ADRL_requestedOn,103)ADRL_requestedOn,Convert(Varchar(10),ADRL_receivedOn,103)ADRL_receivedOn "
            sSql = sSql & "  from Audit_DRLLog  left join content_management_master a on ADRL_RequestedListID=a.cmm_ID "
            sSql = sSql & "  left join EDT_attachments b on ADRL_AttachID=b.ATCH_ID "
            sSql = sSql & "  Left Join Audit_Doc_Request_List c on ADRL_RequestedTypeID=c.DRL_DRLID "
            sSql = sSql & "  where  ADRL_CompID=" & iACID & " And ADRL_CustID=" & iCustID & ""
            If iAuditID > 0 Then
                sSql = sSql & " And ADRL_AuditNo=" & iAuditID & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And ADRL_FunID=" & iFunID & ""
            End If
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                drow = dt.NewRow
                If IsDBNull(dtDetails.Rows(i)("ADRL_ID")) = False Then
                    drow("DRLID") = dtDetails.Rows(i)("ADRL_ID")
                End If

                If IsDBNull(dtDetails.Rows(i)("ADRL_RequestedTypeID")) = False Then
                    drow("RequestedDocNameID") = dtDetails.Rows(i)("ADRL_RequestedTypeID")
                End If

                If IsDBNull(dtDetails.Rows(i)("DocumentName")) = False Then
                    drow("DRLName") = dtDetails.Rows(i)("DocumentName")
                End If

                If IsDBNull(dtDetails.Rows(i)("DRL_Name")) = False Then
                    drow("RequestedDocName") = dtDetails.Rows(i)("DRL_Name")
                End If

                If IsDBNull(dtDetails.Rows(i)("ADRL_requestedOn")) = False Then
                    drow("Requestedon") = dtDetails.Rows(i)("ADRL_requestedOn")
                End If

                If IsDBNull(dtDetails.Rows(i)("AttachmentName")) = False Then
                    drow("Receivecdoc") = dtDetails.Rows(i)("AttachmentName")
                End If

                If IsDBNull(dtDetails.Rows(i)("ADRL_receivedOn")) = False Then
                    drow("Receivedon") = dtDetails.Rows(i)("ADRL_receivedOn")
                End If

                If IsDBNull(dtDetails.Rows(i)("ADRL_AttachID")) = False Then
                    drow("AttachID") = dtDetails.Rows(i)("ADRL_AttachID")
                End If

                If IsDBNull(dtDetails.Rows(i)("ATCH_DocID")) = False Then
                    drow("DocID") = dtDetails.Rows(i)("ATCH_DocID")
                End If
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocumentName(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iDocID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select (ATCH_FNAME + '.' + atch_ext) as FileName from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iDocID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTaskCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iTaskCodeID As Integer) As String
        Dim sSql As String
        Try
            sSql = "select AAS_AssignmentNo from AuditAssignment_Schedule where AAS_ID=" & iTaskCodeID & " and AAS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSamplingAtchID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select SS_AttachID from Sample_selection where SS_AuditCodeID=" & iAuditID & " and SS_CheckPointID=" & iCheckPointID & " and SS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IsSampleSelectionSaved(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select SS_AttachID from Sample_selection where SS_AuditCodeID=" & iAuditID & " and SS_CheckPointID=" & iCheckPointID & " and SS_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSamplingAttachment(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer) As Integer
        Dim sSql As String
        Dim iPKID As Integer
        Try
            iPKID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(SS_PKID),0)+1 from Sample_selection Where SS_CompID=" & iACID & "")
            sSql = "Insert into Sample_selection (SS_PKID,SS_AuditCodeID,SS_CheckPointID,SS_AttachID,SS_CompID) values(" & iPKID & "," & iAuditID & "," & iCheckPointID & "," & iAttachID & "," & iACID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
            Return iPKID
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
