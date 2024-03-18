Imports System
Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Public Class ClsFXADynamicReport
    Dim objDB As New DBHelper
    Dim objClsFasgnrl As New clsGRACeGeneral
    Public Function LoadZone(ByVal sNamespace As String, ByVal iCompid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_node From Sad_Org_Structure Where Org_Parent=0 and Org_CompID=" & iCompid & ") "
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRegion(ByVal sNamespace As String, ByVal iCompid As Integer, ByVal iZoneId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iZoneId & " and Org_CompID=" & iCompid & " "
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadArea(ByVal sNamespace As String, ByVal iCompid As Integer, ByVal iRegionID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iRegionID & " and Org_CompID=" & iCompid & " "
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranch(ByVal sNamespace As String, ByVal iCompid As Integer, ByVal iAreaId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAreaId & " and Org_CompID=" & iCompid & " "
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetReferenceNo(ByVal sNamespace As String, ByVal iCompId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select AFAA_ID,AFAA_AssetRefNo from acc_fixedAssetAdditionDel where AFAA_CompID=" & iCompId & ""
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSupplier(ByVal sNamespace As String, ByVal iCompId As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompId & " and CSM_Delflag='A' order by CSM_Name"
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetType(ByVal sNamespace As String, ByVal iCompId As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_DelFlag='A' and AM_CompID=" & iCompId & ""
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetNo(ByVal sNamespace As String, ByVal iCompId As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select AFAA_AssetNo,AFAA_ID from acc_fixedAssetAdditionDel where  AFAA_CompID=" & iCompId & ""
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetails(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal iZone As Integer, ByVal iRegion As Integer, ByVal iArea As Integer, ByVal iBranch As Integer, ByVal irbtnStatus As Integer, ByVal iRefId As Integer, ByVal iSupplierId As Integer, ByVal iAssetType As Integer, ByVal iAssetNo As String, ByVal dFromDate As Date, ByVal dTo As Date, ByVal iAdditionType As Integer, ByVal iAssetTrnType As Integer, ByVal sAssetAge As String, ByVal dFromDatePurchase As Date, ByVal dPurchaseTo As Date, ByVal sDeprate As String, ByVal sItemcode As String, ByVal sItemdesc As String, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            dt = LoadAllDetails(sNamespace, iCompId, iZone, iRegion, iArea, iBranch, irbtnStatus, iRefId, iSupplierId, iAssetType, iAssetNo, dFromDate, dTo, iAdditionType, iAssetTrnType, sAssetAge, dFromDatePurchase, dPurchaseTo, sDeprate, sItemcode, sItemdesc, iYearId)
            If dt Is Nothing Then
                Return dt
            Else
                Dim dview As New DataView(dt)
                If irbtnStatus = 1 Then
                    dview = dt.DefaultView
                    dview.RowFilter = "Status='Waiting For Approval' "
                    dt = dview.ToTable
                ElseIf irbtnStatus = 2 Then
                    dview = dt.DefaultView
                    dview.RowFilter = "Status='Approved'"
                    dt = dview.ToTable
                ElseIf irbtnStatus = 3 Then
                    dview = dt.DefaultView
                    dview.RowFilter = "Status='Transaction Deleted'"
                    dt = dview.ToTable
                ElseIf irbtnStatus = 4 Then
                    dview = dt.DefaultView
                    dt = dview.ToTable
                End If
                Return dt
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDeleteDetails(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal irbtnStatus As Integer, ByVal iDelRefNo As Integer, ByVal iDelType As Integer, ByVal iAssetType As Integer, ByVal iAssetNo As String, ByVal dFromDate As Date, ByVal dTo As Date, ByVal sAssetAge As String, ByVal dFromDatePurchase As Date, ByVal dPurchaseTo As Date, ByVal sItemdesc As String, ByVal iStatus As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            dt = LoadAllDeleteDetails(sNamespace, iCompId, irbtnStatus, iDelRefNo, iDelType, iAssetType, iAssetNo, dFromDate, dTo, sAssetAge, dFromDatePurchase, dPurchaseTo, sItemdesc, iStatus, iYearId)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMasterDetails(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal iAssetType As Integer, ByVal iAssetNo As String, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            dt = LoadAllMasterDetails(sNamespace, iCompId, iAssetType, iAssetNo, iYearId)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal iZone As Integer, ByVal iRegion As Integer, ByVal iArea As Integer, ByVal iBranch As Integer, ByVal irbtnStatus As Integer, ByVal iRefId As Integer, ByVal iSupplierId As Integer, ByVal iAssetType As Integer, ByVal iAssetNo As String, ByVal dFromDate As Date, ByVal dTo As Date, ByVal iAdditionType As Integer, ByVal iAssetTrnType As Integer, ByVal sAssetAge As String, ByVal dFromDatePurchase As Date, ByVal dPurchaseTo As Date, ByVal sDeprate As String, ByVal sItemcode As String, ByVal sItemdesc As String, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Try
            dt.Columns.Add("iRefId")
            dt.Columns.Add("iSupplierID")
            dt.Columns.Add("iAssetType")
            dt.Columns.Add("iAssetNo")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("iAdditionType")
            dt.Columns.Add("iTrType")
            dt.Columns.Add("iAssetage")
            dt.Columns.Add("iDepRate")
            dt.Columns.Add("iItemcode")
            dt.Columns.Add("iItemDesc")
            dt.Columns.Add("iAddtnDate")
            dt.Columns.Add("Status")

            sSql = "select * from acc_fixedAssetAdditionDel where AFAA_CompID=" & iCompId & " and AFAA_YearID='" & iYearId & "'"
            If iZone > 0 Then
                sSql = sSql & " And AFAA_Zone =" & iZone & ""
            End If
            If iRegion > 0 Then
                sSql = sSql & " And AFAA_Region =" & iRegion & ""
            End If
            If iArea > 0 Then
                sSql = sSql & " And AFAA_Area =" & iArea & ""
            End If
            If iBranch > 0 Then
                sSql = sSql & " And AFAA_Branch =" & iBranch & ""
            End If
            If iRefId > 0 Then
                sSql = sSql & " And AFAA_ID =" & iRefId & ""
            End If
            If iSupplierId > 0 Then
                sSql = sSql & " and AFAA_SupplierName=" & iSupplierId & ""
            End If
            If iAssetType > 0 Then
                sSql = sSql & "and AFAA_AssetType=" & iAssetType & ""
            End If
            If iAssetNo <> "" Then
                sSql = sSql & " and AFAA_ItemCode='" & iAssetNo & "'"
            End If
            If dFromDate <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And AFAA_AddtnDate Between " & objClsFasgnrl.FormatDtForRDBMS(dFromDate, "Q") & " "
            End If
            If dTo <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And " & objClsFasgnrl.FormatDtForRDBMS(dTo, "Q") & " "
            End If
            If iAdditionType > 0 Then
                sSql = sSql & " and AFAA_TrType=" & iAdditionType & ""
            End If
            If iAssetTrnType > 0 Then
                sSql = sSql & " and AFAA_AssetTrType=" & iAssetTrnType & ""
            End If
            If sAssetAge <> "" Then
                sSql = sSql & " and AFAA_AssetAge=" & sAssetAge & ""
            End If
            If dFromDatePurchase <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And AFAA_PurchaseDate Between " & objClsFasgnrl.FormatDtForRDBMS(dFromDatePurchase, "Q") & " "
            End If
            If dPurchaseTo <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And  " & objClsFasgnrl.FormatDtForRDBMS(dPurchaseTo, "Q") & " "
            End If
            If sDeprate <> "" Then
                sSql = sSql & " and AFAA_Depreciation=" & sDeprate & ""
            End If
            If sItemcode <> "" Then
                sSql = sSql & " and AFAA_ItemCode='" & sItemcode & "'"
            End If
            If sItemdesc <> "" Then
                sSql = sSql & " and AFAA_ItemDescription='" & sItemdesc & "'"
            End If
            dtDetails = objDB.SQLExecuteDataTable(sNamespace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetRefNo")) = False Then
                    dRow("iRefId") = dtDetails.Rows(i)("AFAA_AssetRefNo")
                Else
                    dRow("iRefId") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_SupplierName")) = False Then
                    dRow("iSupplierID") = objDB.SQLExecuteScalar(sNamespace, "Select CSM_Name  From customerSupplierMaster Where CSM_ID='" & dtDetails.Rows(i)("AFAA_SupplierName") & "' and  CSM_CompID=" & iCompId & "")

                Else
                    dRow("iSupplierID") = "Unknown"
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("iAssetType") = objDB.SQLExecuteScalar(sNamespace, "Select AM_Description  From Acc_AssetMaster Where AM_ID='" & dtDetails.Rows(i)("AFAA_AssetType") & "' and  AM_CompID=" & iCompId & "")
                Else
                    dRow("iAssetType") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetNo")) = False Then
                    dRow("iAssetNo") = dtDetails.Rows(i)("AFAA_AssetNo")
                Else
                    dRow("iAssetNo") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("PurchaseDate") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i)("AFAA_PurchaseDate"), "D")
                Else
                    dRow("PurchaseDate") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AddtnDate")) = False Then
                    dRow("iAddtnDate") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i)("AFAA_AddtnDate"), "D")
                Else
                    dRow("iAddtnDate") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_TrType")) = False Then
                    dRow("iAdditionType") = dtDetails.Rows(i)("AFAA_TrType")
                Else
                    dRow("iAdditionType") = ""
                End If
                If dtDetails.Rows(i)("AFAA_TrType") = 1 Then
                    dRow("iAdditionType") = "Opening Balance"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 2 Then
                    dRow("iAdditionType") = "ADDITION"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 3 Then
                    dRow("iAdditionType") = "TRANSFERS"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 4 Then
                    dRow("iAdditionType") = "REVALUATION"
                ElseIf dtDetails.Rows(i)("AFAA_TrType") = 5 Then
                    dRow("iAdditionType") = "FOREIGN EXCHANGE"
                Else
                    dRow("iAdditionType") = 0
                End If

                If dtDetails.Rows(i)("AFAA_AssetTrType") = 1 Then
                    dRow("iTrType") = "Local"
                ElseIf dtDetails.Rows(i)("AFAA_AssetTrType") = 2 Then
                    dRow("iTrType") = "Imported"
                ElseIf dtDetails.Rows(i)("AFAA_AssetTrType") = 3 Then
                    dRow("iTrType") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAge")) = False Then
                    dRow("iAssetage") = dtDetails.Rows(i)("AFAA_AssetAge")
                Else
                    dRow("iAssetage") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_Depreciation")) = False Then
                    dRow("iDepRate") = dtDetails.Rows(i)("AFAA_Depreciation")
                Else
                    dRow("iDepRate") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_ItemCode")) = False Then
                    dRow("iItemcode") = dtDetails.Rows(i)("AFAA_ItemCode")
                Else
                    dRow("iItemcode") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_Description")) = False Then
                    dRow("iItemDesc") = dtDetails.Rows(i)("AFAA_Description")
                Else
                    dRow("iItemDesc") = ""
                End If
                If dtDetails.Rows(i)("AFAA_Delflag") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "A" Then
                    dRow("Status") = "Approved"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "X" Then
                    dRow("Status") = "Transaction Deleted"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "Y" Then
                    dRow("Status") = "Recalled for Approval"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "D" Then
                    dRow("Status") = "Transaction Deleted"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDeleteDetails(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal irbtnStatus As Integer, ByVal iDelRefNo As Integer, ByVal iDelType As Integer, ByVal iAssetType As Integer, ByVal iAssetNo As String, ByVal dFromDate As Date, ByVal dTo As Date, ByVal sAssetAge As String, ByVal dFromDatePurchase As Date, ByVal dPurchaseTo As Date, ByVal sItemdesc As String, ByVal iStatus As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Dim iStatdVal As String = ""
        Try
            dt.Columns.Add("AssetType")
            dt.Columns.Add("Itemcode")
            dt.Columns.Add("ItemDesc")
            dt.Columns.Add("MasterQty")
            dt.Columns.Add("AssetQuantity")
            dt.Columns.Add("RefNo")
            dt.Columns.Add("DeleteDate")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("DeleteQty")
            dt.Columns.Add("DelAmount")
            dt.Columns.Add("DelType")
            dt.Columns.Add("DelReason")
            dt.Columns.Add("Status")

            sSql = "select * from Acc_FixedAssetDeletion where AFAD_CompID=" & iCompId & " and AFAD_YearID='" & iYearId & "' "
            If iDelRefNo > 0 Then
                sSql = sSql & " And AFAD_ID =" & iDelRefNo & ""
            End If
            If iDelType > 0 Then
                sSql = sSql & " And AFAD_AssetDelID =" & iDelType & ""
            End If
            If iAssetType > 0 Then
                sSql = sSql & "and AFAD_AssetType=" & iAssetType & ""
            End If
            If dFromDate <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And AFAD_AssetDeletionDate Between " & objClsFasgnrl.FormatDtForRDBMS(dFromDate, "Q") & " "
            End If
            If dTo <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And " & objClsFasgnrl.FormatDtForRDBMS(dTo, "Q") & " "
            End If
            If iAssetNo <> "" Then
                sSql = sSql & " and AFAD_ItemCode= '" & iAssetNo & "'"
            End If

            If dFromDatePurchase <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And AFAD_PurchaseDate Between " & objClsFasgnrl.FormatDtForRDBMS(dFromDatePurchase, "Q") & " "
            End If
            If dPurchaseTo <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And  " & objClsFasgnrl.FormatDtForRDBMS(dPurchaseTo, "Q") & " "
            End If
            If sItemdesc <> "" Then
                sSql = sSql & " and AFAD_ItemDescription='" & sItemdesc & "'"
            End If
            If iStatus <> 0 Then

                If iStatus = 1 Then
                    iStatdVal = "W"
                ElseIf iStatus = 2 Then
                    iStatdVal = "D"
                ElseIf iStatus = 3 Then
                    iStatdVal = "TR"
                ElseIf iStatus = 4 Then
                    iStatdVal = "RS"
                End If
                sSql = sSql & " and AFAD_Status= '" & iStatdVal & "' "
            End If
            dtDetails = objDB.SQLExecuteDataTable(sNamespace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("AFAD_AssetType")) = False Then
                    dRow("AssetType") = objDB.SQLExecuteScalar(sNamespace, "Select AM_Description  From Acc_AssetMaster Where AM_ID='" & dtDetails.Rows(i)("AFAD_AssetType") & "' and  AM_CompID=" & iCompId & "")
                Else
                    dRow("AssetType") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_ItemCode")) = False Then
                    dRow("Itemcode") = dtDetails.Rows(i)("AFAD_ItemCode")
                Else
                    dRow("Itemcode") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_Description")) = False Then
                    dRow("ItemDesc") = dtDetails.Rows(i)("AFAD_Description")
                Else
                    dRow("ItemDesc") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_AssetType")) = False Then
                    dRow("MasterQty") = objDB.SQLExecuteScalar(sNamespace, "Select AFAM_Quantity  From Acc_FixedAssetMaster Where AFAM_AssetType='" & dtDetails.Rows(i)("AFAD_AssetType") & "' and AFAM_ItemCode='" & dtDetails.Rows(i)("AFAD_ItemCode") & "' and AFAM_CompID=" & iCompId & "")
                Else
                    dRow("MasterQty") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_AssetTransNo")) = False Then
                    dRow("RefNo") = dtDetails.Rows(i)("AFAD_AssetTransNo")
                Else
                    dRow("RefNo") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_Quantity")) = False Then
                    dRow("AssetQuantity") = dtDetails.Rows(i)("AFAD_Quantity")
                Else
                    dRow("AssetQuantity") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_AssetDeletionDate")) = False Then
                    dRow("DeleteDate") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i)("AFAD_AssetDeletionDate"), "D")
                Else
                    dRow("DeleteDate") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAd_PurchaseDate")) = False Then
                    dRow("PurchaseDate") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i)("AFAD_PurchaseDate"), "D")
                Else
                    dRow("PurchaseDate") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_AssetDelQuantity")) = False Then
                    dRow("DeleteQty") = dtDetails.Rows(i)("AFAD_AssetDelQuantity")
                Else
                    dRow("DeleteQty") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_AssetDeltnAmount")) = False Then
                    dRow("DelAmount") = dtDetails.Rows(i)("AFAD_AssetDeltnAmount")
                Else
                    dRow("DelAmount") = ""
                End If
                If dtDetails.Rows(i)("AFAD_AssetDelID") = 1 Then
                    dRow("DelType") = "Sold"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDelID") = 2 Then
                    dRow("DelType") = "Transfer"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDelID") = 3 Then
                    dRow("DelType") = "Stolen"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDelID") = 4 Then
                    dRow("DelType") = "Destroyed"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDelID") = 5 Then
                    dRow("DelType") = "Absolite"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDelID") = 6 Then
                    dRow("DelType") = "Repair"
                Else
                    dRow("DelType") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAD_DeltnItemDescription")) = False Then
                    dRow("DelReason") = dtDetails.Rows(i)("AFAD_DeltnItemDescription")
                Else
                    dRow("DelReason") = ""
                End If

                If dtDetails.Rows(i)("AFAD_Status") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("AFAD_Status") = "A" Then
                    dRow("Status") = "Approved"
                ElseIf dtDetails.Rows(i)("AFAD_Status") = "TR" Then
                    dRow("Status") = "Transfered/Repair"
                ElseIf dtDetails.Rows(i)("AFAD_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("AFAD_Status") = "RS" Then
                    dRow("Status") = "Reactivated After Transfer/Repair"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllMasterDetails(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal iAssetType As Integer, ByVal iAssetNo As String, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Try
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemDesc")
            dt.Columns.Add("AssetQuantity")
            dt.Columns.Add("Amount")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("DeleteQty")
            dt.Columns.Add("Status")

            sSql = "select * from Acc_FixedAssetMaster where AFAM_CompID=" & iCompId & " and AFAM_YearID='" & iYearId & "' "
            If iAssetType > 0 Then
                sSql = sSql & "and AFAM_AssetType=" & iAssetType & ""
            End If
            If iAssetNo <> "" Then
                sSql = sSql & " and AFAM_ItemCode= '" & iAssetNo & "'"
            End If

            dtDetails = objDB.SQLExecuteDataTable(sNamespace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("AFAM_AssetType")) = False Then
                    dRow("AssetType") = objDB.SQLExecuteScalar(sNamespace, "Select AM_Description  From Acc_AssetMaster Where AM_ID='" & dtDetails.Rows(i)("AFAM_AssetType") & "' and  AM_CompID=" & iCompId & "")
                Else
                    dRow("AssetType") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAM_AssetCode")) = False Then
                    dRow("AssetCode") = dtDetails.Rows(i)("AFAM_AssetCode")
                Else
                    dRow("AssetCode") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAM_ItemCode")) = False Then
                    dRow("ItemCode") = dtDetails.Rows(i)("AFAM_ItemCode")
                Else
                    dRow("ItemCode") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAM_Description")) = False Then
                    dRow("ItemDesc") = dtDetails.Rows(i)("AFAM_Description")
                Else
                    dRow("ItemDesc") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAM_Quantity")) = False Then
                    dRow("AssetQuantity") = dtDetails.Rows(i)("AFAM_Quantity")
                Else
                    dRow("AssetQuantity") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAM_PurchaseAmount")) = False Then
                    dRow("Amount") = dtDetails.Rows(i)("AFAM_PurchaseAmount")
                Else
                    dRow("Amount") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAM_PurchaseDate")) = False Then
                    dRow("PurchaseDate") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i)("AFAM_PurchaseDate"), "D")
                Else
                    dRow("PurchaseDate") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAM_ItemCode")) = False Then
                    dRow("DeleteQty") = objDB.SQLExecuteScalar(sNamespace, "Select sum(AFAD_AssetDelQuantity)  From Acc_FixedAssetDeletion Where AFAD_ItemCode='" & dtDetails.Rows(i)("AFAM_ItemCode") & "' and  AFAD_CompID=" & iCompId & " and AFAD_Status not in('RS','W') ")
                Else
                    dRow("DeleteQty") = ""
                End If
                If dtDetails.Rows(i)("AFAM_Status") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("AFAM_Status") = "A" Then
                    dRow("Status") = "Approved"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN "
            sSql = sSql & "From MST_Customer_master j "
            sSql = sSql & " Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT'"
            sSql = sSql & " Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX'"
            sSql = sSql & " Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN'"
            sSql = sSql & " Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN'"
            sSql = sSql & " Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN'"
            sSql = sSql & " Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' Where j.Cust_ID=" & iCompID & " "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingItemCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal assettype As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAM_ID,AFAM_Itemcode from Acc_FixedAssetMaster where AFAM_AssetType=" & assettype & " and AFAM_CompID=" & iCompID & " "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAD_ID,AFAD_AssetTransNo from Acc_FixedAssetDeletion where AFAD_CompID=" & iCompID & " order by AFAD_ID asc"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
