Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Class clsIntacct
    Private Shared objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadFunctionID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select APM_FunctionID From Audit_APM_Details Where  APM_CompID=" & iACID & " And APM_YearID=" & iYearID & "  And APM_ID=" & iAuditID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LOadTranscation(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSQl As String

        Try
            sSQl = "select INTAC_ID,intac_objname,intac_objval from IntacctDDlItemText"
            Return objDBL.SQLExecuteDataTable(sAC, sSQl)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDRLLog(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAuditID As Integer, ByVal iADRLID As Integer,
                                  ByVal iListID As Integer, ByVal iTypeID As Integer, ByVal iUsrID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_DRLLog set  ADRL_ReceivedOn=getdate(),ADRL_ReceivedComments='Attached by Intacct', "
            sSql = sSql & " ADRL_Status='Updated',ADRL_UpdatedBy=" & iUsrID & ",ADRL_UpdatedOn =GetDate(),ADRL_AttachID=" & iAttachID & ""
            sSql = sSql & " Where ADRL_CompID=" & iACID & " And ADRL_ID=" & iADRLID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
