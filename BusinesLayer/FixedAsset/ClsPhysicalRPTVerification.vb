Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class ClsPhysicalRPTVerification
    Dim objDB As New DBHelper
    Dim objDBL As New DBHelper
    Dim objClsFasgnrl As New clsGRACeGeneral
    Public Function LoadBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Parent,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Parent From Sad_Org_Structure Where Org_Parent=4 and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccZone(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iRegionID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Parent,Org_Name From Sad_Org_Structure Where Org_Node=" & iRegionID & " and Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccArea(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Parent,Org_Name From Sad_Org_Structure Where Org_Node=" & iBranch & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Parent,Org_Name From Sad_Org_Structure Where Org_Node=" & iAccZone & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccBrnch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccarea As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Parent,Org_Name From Sad_Org_Structure Where Org_Node=" & iAccarea & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetType1(ByVal sNameSpace As String, ByVal iCompID As Integer)
        Dim sSql As String = ""
        Dim dt2 As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_DelFlag='A' and AM_CompID=" & iCompID & ""
            dt2 = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAsseType As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim val As Integer = 0
        Try
            dt.Columns.Add("Slno")
            dt.Columns.Add("AssetTransfer")
            dt.Columns.Add("ActualLocation")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("SupplierName")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetNo")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("DateofPurchase")
            dt.Columns.Add("Amount")
            dt.Columns.Add("Depreciation")
            sSql = "Select * from Acc_FixedAssetAdditionDel where AFAA_AssetType=" & iAsseType & "  and AFAA_CompID=" & iCompID & " And AFAA_YearID=" & iyearId & ""
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                val = val + 1
                dRow("Slno") = val
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetTrType")) = False Then
                    If dtDetails.Rows(i)("AFAA_AssetTrType") = 1 Then
                        dRow("AssetTransfer") = "Local"
                    ElseIf dtDetails.Rows(i)("AFAA_AssetTrType") = 2 Then
                        dRow("AssetTransfer") = "Imported"
                    Else
                        dRow("AssetTransfer") = ""
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_ActualLocn")) = False Then
                    dRow("ActualLocation") = dtDetails.Rows(i)("AFAA_ActualLocn")
                Else
                    dRow("ActualLocation") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAge")) = False Then
                    dRow("AssetAge") = dtDetails.Rows(i)("AFAA_AssetAge")
                Else
                    dRow("AssetAge") = ""
                End If

                If dtDetails.Rows(i)("AFAA_TrType") = 1 Then
                    dRow("TransactionType") = "Addition"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 2 Then
                    dRow("TransactionType") = "Transfers"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 3 Then
                    dRow("TransactionType") = "Revaluation"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 4 Then
                    dRow("TransactionType") = "Foreign Exchange"
                Else
                    dRow("TransactionType") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_SupplierName")) = False Then
                    dRow("SupplierName") = objDB.SQLExecuteScalar(sNameSpace, "Select  CSM_Name from customerSupplierMaster where CSM_ID=" & dtDetails.Rows(i)("AFAA_SupplierName") & " And CSM_CompID=" & iCompID & "")
                Else
                    dRow("SupplierName") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("AssetType") = objDB.SQLExecuteScalar(sNameSpace, "Select  gl_desc from Chart_Of_Accounts where gl_id='" & dtDetails.Rows(i)("AFAA_AssetType") & "' and gl_CompId=" & iCompID & "")
                Else
                    dRow("AssetType") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetNo")) = False Then
                    dRow("AssetNo") = dtDetails.Rows(i)("AFAA_AssetNo")
                Else
                    dRow("AssetNo") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetRefNo")) = False Then
                    dRow("AssetRefNo") = dtDetails.Rows(i)("AFAA_AssetRefNo")
                Else
                    dRow("AssetRefNo") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("DateofPurchase") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i).Item("AFAA_PurchaseDate"), "D")
                Else
                    dRow("DateofPurchase") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAmount")) = False Then
                    dRow("Amount") = dtDetails.Rows(i)("AFAA_AssetAmount")
                Else
                    dRow("Amount") = "0.00"
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_Depreciation")) = False Then
                    dRow("Depreciation") = dtDetails.Rows(i)("AFAA_Depreciation")
                Else
                    dRow("Depreciation") = "0.00"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllReportDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAsseType As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim val As Integer = 0
        Try
            dt.Columns.Add("Slno")
            dt.Columns.Add("AssetTransfer")
            dt.Columns.Add("ActualLocation")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("SupplierName")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetNo")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("DateofPurchase")
            dt.Columns.Add("Amount")
            dt.Columns.Add("Depreciation")
            dt.Columns.Add("VerifiedBy")
            dt.Columns.Add("VerifiedOn")
            dt.Columns.Add("RemarksVrfidOn")
            dt.Columns.Add("ApprovedBy")
            dt.Columns.Add("ApprovedOn")
            dt.Columns.Add("RemarksApprvdOn")
            sSql = "Select * from Acc_FixedAssetAdditionDel where AFAA_AssetType=" & iAsseType & "  and AFAA_CompID=" & iCompID & " And AFAA_YearID=" & iyearId & ""
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                val = val + 1
                dRow("Slno") = val
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetTrType")) = False Then
                    If dtDetails.Rows(i)("AFAA_AssetTrType") = 1 Then
                        dRow("AssetTransfer") = "Local"
                    ElseIf dtDetails.Rows(i)("AFAA_AssetTrType") = 2 Then
                        dRow("AssetTransfer") = "Imported"
                    Else
                        dRow("AssetTransfer") = ""
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_ActualLocn")) = False Then
                    dRow("ActualLocation") = dtDetails.Rows(i)("AFAA_ActualLocn")
                Else
                    dRow("ActualLocation") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAge")) = False Then
                    dRow("AssetAge") = dtDetails.Rows(i)("AFAA_AssetAge")
                Else
                    dRow("AssetAge") = ""
                End If

                If dtDetails.Rows(i)("AFAA_TrType") = 1 Then
                    dRow("TransactionType") = "Addition"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 2 Then
                    dRow("TransactionType") = "Transfers"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 3 Then
                    dRow("TransactionType") = "Revaluation"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 4 Then
                    dRow("TransactionType") = "Foreign Exchange"
                Else
                    dRow("TransactionType") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_SupplierName")) = False Then
                    dRow("SupplierName") = objDB.SQLExecuteScalar(sNameSpace, "Select  CSM_Name from customerSupplierMaster where CSM_ID=" & dtDetails.Rows(i)("AFAA_SupplierName") & " And CSM_CompID=" & iCompID & "")
                Else
                    dRow("SupplierName") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("AssetType") = objDB.SQLExecuteScalar(sNameSpace, "Select  gl_desc from Chart_Of_Accounts where gl_id='" & dtDetails.Rows(i)("AFAA_AssetType") & "' and gl_CompId=" & iCompID & "")
                Else
                    dRow("AssetType") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetNo")) = False Then
                    dRow("AssetNo") = dtDetails.Rows(i)("AFAA_AssetNo")
                Else
                    dRow("AssetNo") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetRefNo")) = False Then
                    dRow("AssetRefNo") = dtDetails.Rows(i)("AFAA_AssetRefNo")
                Else
                    dRow("AssetRefNo") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("DateofPurchase") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i).Item("AFAA_PurchaseDate"), "D")
                Else
                    dRow("DateofPurchase") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAmount")) = False Then
                    dRow("Amount") = dtDetails.Rows(i)("AFAA_AssetAmount")
                Else
                    dRow("Amount") = "0.00"
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_Depreciation")) = False Then
                    dRow("Depreciation") = dtDetails.Rows(i)("AFAA_Depreciation")
                Else
                    dRow("Depreciation") = "0.00"
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PhyVerifiedby")) = False Then
                    dRow("VerifiedBy") = dtDetails.Rows(i)("AFAA_PhyVerifiedby")
                Else
                    dRow("VerifiedBy") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PhyVerifiedOn")) = False Then
                    dRow("VerifiedOn") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i).Item("AFAA_PhyVerifiedOn"), "D")
                Else
                    dRow("VerifiedOn") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PhyVerifiedRemarks")) = False Then
                    dRow("RemarksVrfidOn") = dtDetails.Rows(i)("AFAA_PhyVerifiedRemarks")
                Else
                    dRow("RemarksVrfidOn") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PhyApprovedby")) = False Then
                    dRow("ApprovedBy") = dtDetails.Rows(i)("AFAA_PhyApprovedby")
                Else
                    dRow("ApprovedBy") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PhyApprovedOn")) = False Then
                    dRow("ApprovedOn") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i).Item("AFAA_PhyApprovedOn"), "D")
                Else
                    dRow("ApprovedOn") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PhyApprovedRemarks")) = False Then
                    dRow("RemarksApprvdOn") = dtDetails.Rows(i)("AFAA_PhyApprovedRemarks")
                Else
                    dRow("RemarksApprvdOn") = ""
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
