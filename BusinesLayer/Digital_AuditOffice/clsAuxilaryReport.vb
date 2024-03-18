Public Class clsAuxilaryReport
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function LoadReports(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CMM_ID,CMM_Desc from Content_Management_Master where cmm_Category='TOR' and cmm_Delflag ='A' And CMM_CompID=" & iACID & " order by CMM_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetReportDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iRptId As Integer, Optional ByVal iCustID As Integer = 0) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("MasterGLID")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Additions")
            dt.Columns.Add("Transfer")
            dt.Columns.Add("Reduction")
            dt.Columns.Add("Sold")
            dt.Columns.Add("RTransfer")
            dt.Columns.Add("RReduction")
            dt.Columns.Add("RRateOff")
            dt.Columns.Add("ROpnBal")
            dt.Columns.Add("DFortheYear")
            dt.Columns.Add("DDeduction")
            dt.Columns.Add("DClsBal")
            dt.Columns.Add("MOpnBal")
            dt.Columns.Add("MClsBal")

            If iCustID > 0 Then
                sSql = "Select TRM_ID,TRM_HeaderName,Acc_FAT_Additon,Acc_FAT_Transfer,Acc_FAT_Reduction,Acc_FAT_Sold,Acc_FAT_RTransfer,Acc_FAT_RReduction,Acc_FAT_RRateoff,"
                sSql = sSql & " Acc_FAT_ROpnBal,Acc_FAT_DFortheYear,Acc_FAT_DDeduction,Acc_FAT_DClsBal,Acc_FAT_MOpnBal,Acc_FAT_MClsBal"
                sSql = sSql & " From Trace_Report_Master Left Join acc_FixedAssets_Transaction On Acc_FAT_FixedAssetsID=TRM_ID where TRM_RptID=" & iRptId & " And TRM_CustID=" & iCustID & " order by TRM_Id"
            Else
                sSql = "Select TRM_ID,TRM_HeaderName,Acc_FAT_Additon,Acc_FAT_Transfer,Acc_FAT_Reduction,Acc_FAT_Sold,Acc_FAT_RTransfer,Acc_FAT_RReduction,Acc_FAT_RRateoff,"
                sSql = sSql & " Acc_FAT_ROpnBal,Acc_FAT_DFortheYear,Acc_FAT_DDeduction,Acc_FAT_DClsBal,Acc_FAT_MOpnBal,Acc_FAT_MClsBal"
                sSql = sSql & " From Trace_Report_Master Left Join acc_FixedAssets_Transaction On Acc_FAT_FixedAssetsID=TRM_ID where TRM_RptID=" & iRptId & " order by TRM_Id"
            End If

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("ID") = dtDetails.Rows(i)("TRM_ID")
                dRow("Particulars") = dtDetails.Rows(i)("TRM_HeaderName")
                dRow("OpeningBalance") = ""                                     'Opening Balance
                dRow("Additions") = dtDetails.Rows(i)("Acc_FAT_Additon")        'Additon
                dRow("Transfer") = dtDetails.Rows(i)("Acc_FAT_Transfer")        'Transfer
                dRow("Reduction") = dtDetails.Rows(i)("Acc_FAT_Reduction")      'Reduction
                dRow("Sold") = dtDetails.Rows(i)("Acc_FAT_Sold")                'Sold
                dRow("RTransfer") = dtDetails.Rows(i)("Acc_FAT_RTransfer")      'Transfer
                dRow("RReduction") = dtDetails.Rows(i)("Acc_FAT_RReduction")    'Reduction
                dRow("RRateOff") = dtDetails.Rows(i)("Acc_FAT_RRateoff")        'Rate off
                dRow("ROpnBal") = dtDetails.Rows(i)("Acc_FAT_ROpnBal")          'Opening Balance
                dRow("DFortheYear") = dtDetails.Rows(i)("Acc_FAT_DFortheYear")  'For the Year
                dRow("DDeduction") = dtDetails.Rows(i)("Acc_FAT_DDeduction")    'Deduction
                dRow("DClsBal") = dtDetails.Rows(i)("Acc_FAT_DClsBal")          'Close Balance
                dRow("MOpnBal") = dtDetails.Rows(i)("Acc_FAT_MOpnBal")          'Opening Main Balance
                dRow("MClsBal") = dtDetails.Rows(i)("Acc_FAT_MClsBal")          'Closing Main Balance

                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where cust_Compid=" & iACID & " order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
