Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsAssetRegister
    Private objDBL As New DatabaseLayer.DBHelper
    Public objFAS As New clsGRACeGeneral
    Public Function LoadAssetRegister(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAFAM_AssetType As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AssetID")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemDescription")
            'dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("Datecommission")
            dt.Columns.Add("Qty")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("CurrentStatus")
            dt.Columns.Add("TRStatus")

            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & " and AFAM_CustID=" & iCustId & " and AFAM_DelFlag <>'T' and AFAM_Status <>'T' and AFAM_YearID=" & iYearId & ""
            If iAFAM_AssetType <> 0 Then
                sSql = sSql & " And AFAM_AssetType=" & iAFAM_AssetType & ""
            End If
            sSql = sSql & "  order by AFAM_ID "
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow
                    If IsDBNull(dt1.Rows(i)("AFAM_ID")) = False Then
                        dr("ID") = dt1.Rows(i)("AFAM_ID")
                    Else
                        dr("ID") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_AssetType")) = False Then
                        dr("AssetID") = dt1.Rows(i)("AFAM_AssetType")
                    Else
                        dr("AssetID") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_AssetCode")) = False Then
                        dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
                    Else
                        dr("AssetCode") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_Description")) = False Then
                        dr("AssetDescription") = objDBL.SQLExecuteScalar(sNameSpace, "Select AM_Description from Acc_AssetMaster where AM_ID= " & dt1.Rows(i)("AFAM_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & "")
                    Else
                        dr("AssetDescription") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_ItemCode")) = False Then
                        dr("ItemCode") = dt1.Rows(i)("AFAM_ItemCode")
                    Else
                        dr("ItemCode") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_ItemDescription")) = False Then
                        dr("ItemDescription") = dt1.Rows(i)("AFAM_ItemDescription")
                    Else
                        dr("ItemDescription") = ""
                    End If

                    'If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
                    '    dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
                    'Else
                    '    dr("PurchaseDate") = ""
                    'End If

                    If IsDBNull(dt1.Rows(i)("AFAM_CommissionDate")) = False Then
                        If objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_CommissionDate"), "D") = "01/01/1900" Then
                            dr("Datecommission") = ""
                        Else
                            dr("Datecommission") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_CommissionDate"), "D")
                        End If
                    End If

                    If IsDBNull(dt1.Rows(i)("AFAM_Quantity")) = False Then
                        dr("Qty") = dt1.Rows(i)("AFAM_Quantity")
                    Else
                        dr("Qty") = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("AFAM_AssetAge")) = False Then
                        dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
                    Else
                        dr("AssetAge") = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("AFAM_Status")) = False Then
                        If dt1.Rows(i)("AFAM_Status") = "W" Then
                            dr("CurrentStatus") = "Waiting For Approval"
                        Else
                            dr("CurrentStatus") = "Approved"
                        End If
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_TRStatus")) = False Then
                        dr("TRStatus") = dt1.Rows(i)("AFAM_TRStatus")
                    Else
                        dr("TRStatus") = ""
                    End If

                    'Dim res As String
                    'res = IsDBNull(objDBL.SQLExecuteScalar(sNameSpace, "Select AS_DepMethod from  Application_Settings where AS_CompID=" & iCompID & "")) = False
                    'If res <> False Then
                    '    If res = 1 Then
                    '        dr("DepMethod") = "SLM"
                    '    Else
                    '        dr("DepMethod") = "WDV"
                    '    End If
                    'End If

                    'dr("DepRate") = objDBL.SQLExecuteScalar(sNameSpace, "Select Mas_DepRate from ACC_General_Master where Mas_CompID=" & iCompID & " And Mas_master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_Id=" & iAFAM_AssetType & "")

                    'dr("DepRate") = objDBL.SQLExecuteScalar(sNameSpace, "select AM_Deprate from Acc_AssetMaster where AM_CompID=" & iCompID & " and AM_ID=" & iAFAM_AssetType & "")

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_DelFlag='A' and AM_CompID=" & iCompID & " and AM_CustID=" & iCustID & ""
            'sSql = "Select * From Acc_General_Master Where Mas_CompID='" & iCompID & "' and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Try
            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_ID=" & iID & " And AFAM_CompID=" & iCompID & "  "
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer, ByVal iAssetId As Integer, ByVal iAssetClassId As Integer) As Integer
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable

        Try
            sSql = "Select AFAA_TrType From Acc_FixedAssetAdditionDel Where AFAA_ItemType=" & iAssetId & " and AFAA_AssetType=" & iAssetClassId & " and AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustId & ""
            GetTransType = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return GetTransType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTransOPB(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer, ByVal iAssetId As Integer, ByVal iAssetClassId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1, dt2, dt3 As New DataTable
        Dim iTrType As Integer
        Dim dr As DataRow
        Try

            sSql = "Select AFAA_TrType From Acc_FixedAssetAdditionDel Where AFAA_ItemType=" & iAssetId & " and AFAA_AssetType=" & iAssetClassId & " and AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustId & ""
            iTrType = objDBL.SQLExecuteScalar(sNameSpace, sSql)

            If iTrType = 1 Then
                dt.Columns.Add("ID")
                dt.Columns.Add("TransactionType")
                dt.Columns.Add("Dateofpurchase")
                dt.Columns.Add("OriginalCost")
                dt.Columns.Add("WDVOpeningValue")
                dt.Columns.Add("Depfortheperiod")

                sSql = "Select * From Acc_FixedAssetAdditionDel Where AFAA_ItemType=" & iAssetId & " and AFAA_AssetType=" & iAssetClassId & " and AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustId & ""
                dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                If dt1.Rows.Count > 0 Then
                    For i = 0 To dt1.Rows.Count - 1
                        dr = dt.NewRow
                        If IsDBNull(dt1.Rows(i)("AFAA_ID")) = False Then
                            dr("ID") = dt1.Rows(i)("AFAA_ID")
                        Else
                            dr("ID") = 0
                        End If
                        If IsDBNull(dt1.Rows(i)("AFAA_TrType")) = False Then
                            dr("TransactionType") = dt1.Rows(i)("AFAA_TrType")
                        Else
                            dr("TransactionType") = 0
                        End If
                        If IsDBNull(dt1.Rows(i)("AFAA_PurchaseDate")) = False Then
                            dr("Dateofpurchase") = dt1.Rows(i)("AFAA_PurchaseDate")
                        Else
                            dr("Dateofpurchase") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
                            dr("OriginalCost") = dt1.Rows(i)("AFAA_AssetAmount")
                        Else
                            dr("OriginalCost") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("AFAA_FYAmount")) = False Then
                            dr("WDVOpeningValue") = dt1.Rows(i)("AFAA_FYAmount")
                        Else
                            dr("WDVOpeningValue") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("AFAA_DepreAmount")) = False Then
                            dr("Depfortheperiod") = dt1.Rows(i)("AFAA_DepreAmount")
                        Else
                            dr("Depfortheperiod") = ""
                        End If

                        dt.Rows.Add(dr)
                    Next
                End If
            ElseIf iTrType = 2 Then
                dt.Columns.Add("ID")
                dt.Columns.Add("SupplierName")
                dt.Columns.Add("Particulars")
                dt.Columns.Add("DocDate")
                dt.Columns.Add("BasicCost")
                dt.Columns.Add("TaxAmount")
                dt.Columns.Add("Total")
                dt.Columns.Add("AssetValue")

                sSql = "Select * From Acc_FixedAssetAdditionDetails Where FAAD_ItemType=" & iAssetId & " and FAAD_AssetType=" & iAssetClassId & " and FAAD_CompID=" & iCompID & " and FAAD_CustId=" & iCustId & ""
                dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                If dt1.Rows.Count > 0 Then
                    For i = 0 To dt1.Rows.Count - 1
                        dr = dt.NewRow
                        If IsDBNull(dt1.Rows(i)("FAAD_PKID")) = False Then
                            dr("ID") = dt1.Rows(i)("FAAD_PKID")
                        Else
                            dr("ID") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("FAAD_SupplierName")) = False Then
                            dr("SupplierName") = dt1.Rows(i)("FAAD_SupplierName")
                        Else
                            dr("SupplierName") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("FAAD_Particulars")) = False Then
                            dr("Particulars") = dt1.Rows(i)("FAAD_Particulars")
                        Else
                            dr("Particulars") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("FAAD_DocDate")) = False Then
                            dr("DocDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("FAAD_DocDate"), "D")
                            If dr("DocDate") = "01/01/1900" Then
                                dr("DocDate") = ""
                            End If
                        End If
                        If IsDBNull(dt1.Rows(i)("FAAD_BasicCost")) = False Then
                            dr("BasicCost") = dt1.Rows(i)("FAAD_BasicCost")
                        Else
                            dr("BasicCost") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("FAAD_TaxAmount")) = False Then
                            dr("TaxAmount") = dt1.Rows(i)("FAAD_TaxAmount")
                        Else
                            dr("TaxAmount") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("FAAD_Total")) = False Then
                            dr("Total") = dt1.Rows(i)("FAAD_Total")
                        Else
                            dr("Total") = ""
                        End If
                        If IsDBNull(dt1.Rows(i)("FAAD_AssetValue")) = False Then
                            dr("AssetValue") = dt1.Rows(i)("FAAD_AssetValue")
                        Else
                            dr("AssetValue") = ""
                        End If
                        dt.Rows.Add(dr)
                    Next
                End If

            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
