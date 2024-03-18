Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.Web
Imports System.ComponentModel
Public Class clsCAIQ
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function LoadActiveFactorsCAIQ(ByVal sAC As String, ByVal iACID As Integer, ByVal IAuditID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "select CF_PkID, CF_Name from CAIQ_Factors Where CF_Flag  = 'A' and CF_CompId=" & iACID & " and CF_Auditid= " & IAuditID & " order by CF_PkID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveDescriptorsCAIQ(ByVal sAC As String, ByVal iACID As Integer, ByVal IAuditID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "select CD_PkID, CD_Name, CD_Auditid from CAIQ_Descriptors Where CD_FLAG  = 'A' and CD_CompId=" & iACID & " and CD_Auditid= " & IAuditID & " order by CD_PkID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveCategoryCAIQ(ByVal sAC As String, ByVal iACID As Integer, ByVal IAuditID As Integer, ByVal iFactorID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "select CFC_PkID, CFC_Name, CFC_Auditid from CAIQ_FactorCategory Where CFC_FLAG  = 'A' and CFC_CompId=" & iACID & " and CFC_Factorid=" & iFactorID & " and CFC_Auditid= " & IAuditID & " order by CFC_PkID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveCategoryDescription(ByVal sAC As String, ByVal iACID As Integer, ByVal IAuditID As Integer, ByVal iFactor As Integer, ByVal iCategory As Integer, ByVal Idescriptor As Integer, ByVal iStatus As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Desc")
            dtTab.Columns.Add("Descvalue")
            dtTab.Columns.Add("Status")
            sSql = "select CCD_PkID, CCD_Name, CCD_Desc, CCD_DescValue,ccd_flag from CAIQ_CategoryDescription "
            sSql = sSql & " Where CCD_FactorID=" & iFactor & " and CCD_CategoryID= " & iCategory & " and CCD_FLAG='A' and CCD_CompId=" & iACID & ""
            If Val(Idescriptor) <> 0 Then
                sSql = sSql & " and ccd_descriptorid = " & Idescriptor & ""
            End If
            sSql = sSql & " and CCD_Auditid= 5 order by CCD_PkID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("ID") = dt.Rows(i)("CCD_PkID")
                dr("Name") = dt.Rows(i)("CCD_Name")
                dr("Desc") = dt.Rows(i)("CCD_Desc")
                dr("Descvalue") = dt.Rows(i)("CCD_DescValue")
                If dt.Rows(i)("ccd_flag") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("ccd_flag") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("ccd_flag") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next

            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
