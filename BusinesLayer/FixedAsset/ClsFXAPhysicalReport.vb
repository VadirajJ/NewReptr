Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsFXAPhysicalReport
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral
    Dim objClsFasgnrl As New clsGRACeGeneral
    Dim objAsstTrn As New ClsAssetTransactionAddition
    Public Function LoadYears(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year) + ' - ' + Convert(nvarchar(50),YMS_To_Year)) as year from Acc_Year_Master where YMS_CompId=" & iCompID & " order by YMS_ID asc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function loadAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_DelFlag='A' and AM_CompID=" & iCompID & " and AM_CustID=" & iCustID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadITDetails(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sFinancialYear As String, ByVal iFYYearid As Integer, ByVal iCustId As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim ToDate As String = ""
        Dim WDVasonPY As Double
        Dim Morethan180days As Double
        Dim Lessthan180days As Double
        Dim Deletions As Double

        Dim Rate As Double

        Dim dMorethan180day As Double = 0.0
        Dim dLessthan180day As Double = 0.0
        Dim dWDVasonPY As Double = 0.0
        Dim dDeletion As Double = 0.0
        Dim dTotals As Double = 0.0
        Dim dTotalss As Double = 0.0
        Dim dDepOnOA As Double = 0.0
        Dim dDepOnOAdd As Double = 0.0
        Dim dDepOnD As Double = 0.0
        Dim dDepOnDel As Double = 0.0
        Dim DepOnCurrentRP As Double = 0.0
        Dim DepOnCurrentRPeriod As Double = 0.0
        Dim dAmount As Double = 0.0
        Dim dAmounts As Double = 0.0
        Dim dWDVasonTY As Double = 0.0
        Dim dWDVasonTYs As Double = 0.0
        Dim dCurrentreportingperiod As Double = 0.0
        Dim dCurrentreportingperiods As Double = 0.0
        Dim dWDVasonPYs As Double = 0.0

        Dim dWDVatthebegOftheyear As Double = 0.0
        Dim dMorethan180days As Double = 0.0
        Dim dLessthan180days As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotal As Double = 0.0
        Dim dOnOpeningAndAdditions As Double = 0.0
        Dim dOnDeletions As Double = 0.0
        Dim depAsatEndOfcurrperiod As Double = 0.0
        Dim dNetAsatEndOfcurrperiod As Double = 0.0
        Dim dAsatEndOfpreperiod As Double = 0.0

        Try

            dt.Columns.Add("ID")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("WDVasonPY")
            dt.Columns.Add("Morethan180days")
            dt.Columns.Add("Lessthan180days")
            dt.Columns.Add("Deletions")
            dt.Columns.Add("Total")
            dt.Columns.Add("Deprate")
            dt.Columns.Add("Amount")
            dt.Columns.Add("WDVasonTY")
            dt.Columns.Add("TotalInitialDep")
            dt.Columns.Add("DepOnOA")
            dt.Columns.Add("DepOnD")
            dt.Columns.Add("DepOnCurrentRP")
            dt.Columns.Add("Currentreportingperiod")
            dt.Columns.Add("Previousreportingperiod")
            dt.Columns.Add("Todate")
            dt.Columns.Add("FinancialYear")

            sSql = "" : sSql = "select ADITAct_ID,ADITAct_AssetClassID,ADITAct_RateofDep,isnull(sum(ADITAct_WrittenDownValue),0) as WrittenDownValue,isnull(sum(ADITAct_BfrQtrAmount),0) as Lessthan180days, "
            sSql = sSql & " isnull(sum(ADITAct_AftQtrAmount),0) as Morethan180days,isnull(sum(ADITAct_DelAmount),0) as DelAmount from Acc_AssetDepITAct"
            sSql = sSql & " where ADITAct_YearID=" & iyearId & " and ADITAct_CustId=" & iCustId & "  and ADITAct_CompID=" & iACID & "  group by ADITAct_ID,ADITAct_AssetClassID,ADITAct_RateofDep"

            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()

                If IsDBNull(dtDetails.Rows(i)("ADITAct_AssetClassID")) = False Then
                    dRow("ID") = dtDetails.Rows(i)("ADITAct_AssetClassID")
                End If

                If IsDBNull(dtDetails.Rows(i)("ADITAct_AssetClassID")) = False Then
                    dRow("AssetClass") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dtDetails.Rows(i)("ADITAct_AssetClassID") & " and AM_CompID=" & iACID & " and AM_CustId=" & iCustId & "")
                End If

                '    WDVasonPY = objDBL.SQLGetDescription(sNameSpace, "Select AM_WDVITAct From Acc_AssetMaster Where AM_ID=" & dtDetails.Rows(i)("ADITAct_AssetClassID") & " and AM_CompID=" & iACID & " and AM_CustId=" & iCustId & "")


                WDVasonPY = objDBL.SQLGetDescription(sNameSpace, "Select isnull(ADITAct_WrittenDownValue,0)as WrittenDownValue  From Acc_AssetDepITAct Where ADITAct_AssetClassID=" & dtDetails.Rows(i)("ADITAct_AssetClassID") & "  and ADITAct_CustId=" & iCustId & " and ADITAct_YearID=" & iyearId - 1 & "")
                If WDVasonPY <> 0 Then
                    dRow("WDVasonPY") = Convert.ToDecimal(Math.Round(WDVasonPY)).ToString("#,##0")
                    dWDVasonPY = Convert.ToDecimal(Math.Round(WDVasonPY)).ToString("#,##0")
                    dWDVasonPYs = Convert.ToDecimal(Math.Round(WDVasonPY)).ToString("#,##0")
                    dWDVatthebegOftheyear = dWDVatthebegOftheyear + dWDVasonPYs
                Else
                    WDVasonPY = objDBL.SQLGetDescription(sNameSpace, "Select isnull(ADITAct_OPBForYR,0)as WrittenDownValue  From Acc_AssetDepITAct Where ADITAct_AssetClassID=" & dtDetails.Rows(i)("ADITAct_AssetClassID") & "  and ADITAct_CustId=" & iCustId & " and ADITAct_YearID=" & iyearId & "")
                    If WDVasonPY = 0 Then
                        dWDVasonPY = 0
                        dRow("WDVasonPY") = 0
                    Else
                        dRow("WDVasonPY") = Convert.ToDecimal(Math.Round(WDVasonPY)).ToString("#,##0")
                        dWDVasonPY = Convert.ToDecimal(Math.Round(WDVasonPY)).ToString("#,##0")
                        dWDVasonPYs = Convert.ToDecimal(Math.Round(WDVasonPY)).ToString("#,##0")
                        dWDVatthebegOftheyear = dWDVatthebegOftheyear + dWDVasonPYs
                    End If
                End If
                Dim sBeforeInitAmount As String = ""
                If IsDBNull(dtDetails.Rows(i)("Morethan180days")) = False Then
                    dRow("Morethan180days") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("Morethan180days"))).ToString("#,##0")
                    Morethan180days = dtDetails.Rows(i)("Morethan180days")
                    dMorethan180day = dtDetails.Rows(i)("Morethan180days")
                    If dRow("Morethan180days") <> "0" Then
                        sBeforeInitAmount = objDBL.SQLGetDescription(sNameSpace, "select FAAD_InitDep from Acc_FixedAssetAdditionDetails where FAAD_AssetType =" & dtDetails.Rows(i)("ADITAct_AssetClassID") & " and FAAD_CustId =" & iCustId & "  and FAAD_YearID = " & iyearId & "")
                    End If
                    dMorethan180days = dMorethan180days + dMorethan180day
                Else
                    Morethan180days = 0
                    dRow("Morethan180days") = ""
                End If
                Dim sAfterInitAmount As String = ""
                If IsDBNull(dtDetails.Rows(i)("Lessthan180days")) = False Then
                    dRow("Lessthan180days") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("Lessthan180days"))).ToString("#,##0")
                    Lessthan180days = dtDetails.Rows(i)("Lessthan180days")
                    dLessthan180day = dtDetails.Rows(i)("Lessthan180days")
                    If dRow("Lessthan180days") <> "0" Then
                        sAfterInitAmount = objDBL.SQLGetDescription(sNameSpace, "Select ADITAct_InitAmt as After180days From Acc_AssetDepITAct Where ADITAct_ID=" & dtDetails.Rows(i)("ADITAct_ID") & "  and ADITAct_CustId=" & iCustId & " and ADITAct_YearID=" & iyearId & "")
                    End If
                    dLessthan180days = dLessthan180days + dLessthan180day
                Else
                    Lessthan180days = 0
                    dRow("Lessthan180days") = ""
                End If
                dRow("TotalInitialDep") = Val(sBeforeInitAmount) + Val(sAfterInitAmount)
                If dRow("TotalInitialDep") = "1" Then
                    If dMorethan180days <> "0" Then
                        dRow("TotalInitialDep") = Morethan180days * 20 / 100
                    Else
                        dRow("TotalInitialDep") = Morethan180days * 10 / 100
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("DelAmount")) = False Then
                    dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelAmount"))).ToString("#,##0")
                    Deletions = dtDetails.Rows(i)("DelAmount")
                    dDeletion = dtDetails.Rows(i)("DelAmount")
                    dDeletions = dDeletions + dDeletion
                Else
                    Deletions = 0
                    dRow("Deletions") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("ADITAct_RateofDep")) = False Then
                    dRow("Deprate") = dtDetails.Rows(i)("ADITAct_RateofDep")
                    Rate = dRow("Deprate")
                Else
                    dRow("Deprate") = 0
                    Rate = 0
                End If



                dRow("Total") = Convert.ToDecimal(Math.Round(Val(WDVasonPY) + Val(Morethan180days) + Val(Lessthan180days) - Val(Deletions))).ToString("#,##0")
                dTotals = dRow("Total")
                dTotalss = dTotalss + dTotals

                dRow("DepOnOA") = Convert.ToDecimal(Math.Round(Val(WDVasonPY) + Val(Morethan180days) + Val((Lessthan180days) / 2)) * Rate / 100).ToString("#,##0")
                dDepOnOA = dRow("DepOnOA")
                dDepOnOAdd = dDepOnOAdd + dDepOnOA

                dRow("DepOnD") = Convert.ToDecimal(Math.Round(Val(Deletions)) * Rate / 100).ToString("#,##0")
                dDepOnD = dRow("DepOnD")
                dDepOnDel = dDepOnDel + dDepOnD

                dRow("DepOnCurrentRP") = Convert.ToDecimal(Math.Round(dRow("DepOnOA") - dRow("DepOnD"))).ToString("#,##0")
                DepOnCurrentRP = dRow("DepOnCurrentRP")
                DepOnCurrentRPeriod = DepOnCurrentRPeriod + DepOnCurrentRP

                If IsDBNull(dtDetails.Rows(i)("ADITAct_RateofDep")) = False Then
                    dRow("Amount") = Convert.ToDecimal(Math.Round(((Val(WDVasonPY) + Val(Morethan180days) - Val(Deletions)) * dRow("Deprate") / 100) + ((Val(Lessthan180days) * dRow("Deprate") / 100) / 2))).ToString("#,##0")
                    dAmount = dRow("Amount")
                    dAmounts = dAmounts + dAmount
                Else
                    dAmounts = 0
                    dRow("Amount") = ""
                End If

                dRow("WDVasonTY") = Convert.ToDecimal(Math.Round(dRow("Total") - dRow("Amount"))).ToString("#,##0") - dRow("TotalInitialDep") 'YEAR END
                dWDVasonTY = dRow("WDVasonTY")
                dWDVasonTYs = dWDVasonTYs + dWDVasonTY

                dRow("Currentreportingperiod") = Convert.ToDecimal(Math.Round(dRow("Total") - dRow("DepOnCurrentRP"))).ToString("#,##0") - dRow("TotalInitialDep")
                dCurrentreportingperiod = dRow("Currentreportingperiod")
                dCurrentreportingperiods = dCurrentreportingperiods + dCurrentreportingperiod

                dRow("Previousreportingperiod") = 0

                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_ID From Year_Master Where YMS_YEARID=" & iyearId + 1 & "")
                dRow("Todate") = ToDate
                dRow("FinancialYear") = "AY" & " " & sFinancialYear

                dt.Rows.Add(dRow)
            Next

            dRow = dt.NewRow()
            dRow("AssetClass") = "Sub Total"
            dRow("WDVasonPY") = Convert.ToDecimal(Math.Round(dWDVatthebegOftheyear)).ToString("#,##0")
            dRow("Morethan180days") = Convert.ToDecimal(Math.Round(dMorethan180days)).ToString("#,##0")
            dRow("Lessthan180days") = Convert.ToDecimal(Math.Round(dLessthan180days)).ToString("#,##0")
            dRow("Deletions") = Convert.ToDecimal(Math.Round(dDeletions)).ToString("#,##0")
            dRow("Total") = Convert.ToDecimal(Math.Round(dTotalss)).ToString("#,##0")
            dRow("DepOnOA") = Convert.ToDecimal(Math.Round(dDepOnOAdd)).ToString("#,##0")
            dRow("DepOnD") = Convert.ToDecimal(Math.Round(dDepOnDel)).ToString("#,##0")
            dRow("DepOnCurrentRP") = Convert.ToDecimal(Math.Round(DepOnCurrentRPeriod)).ToString("#,##0")
            dRow("Currentreportingperiod") = Convert.ToDecimal(Math.Round(dCurrentreportingperiods)).ToString("#,##0")
            dRow("WDVasonTY") = Convert.ToDecimal(Math.Round(dWDVasonTYs)).ToString("#,##0")

            dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMorethan180days(ByVal sNameSpace As String, ByVal ddate As Date, ByVal assettype As Integer, ByVal iACID As Integer, ByVal iyearId As Integer) As Double
        Dim sSQL As String = ""
        Dim dAFAAAssetAmount As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select sum(AFAA_AssetAmount) As AdditionAmount  from Acc_FixedAssetAdditionDel where AFAA_AddtnDate > DATEADD(day, 180, '" & ddate & "') AND AFAA_AddtnDate > '" & ddate & "' and  AFAA_AssetType= " & assettype & " and AFAA_TrType=2 and AFAA_CompID=" & iACID & " and AFAA_YearID=" & iyearId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("AdditionAmount").ToString()) = False Then
                    dAFAAAssetAmount = dt.Rows(0)("AdditionAmount").ToString()
                Else
                    dAFAAAssetAmount = 0.0
                End If
            Else
                dAFAAAssetAmount = 0.0
            End If
            If Val(dAFAAAssetAmount) = 0 Then
                dAFAAAssetAmount = 0.0
            End If
            Return dAFAAAssetAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLessthan180days(ByVal sNameSpace As String, ByVal ddate As Date, ByVal assettype As Integer, ByVal iACID As Integer, ByVal iyearId As Integer) As Double
        Dim sSQL As String = ""
        Dim Lessthan180days As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "select sum(AFAA_AssetAmount) as AdditionAmount  from Acc_FixedAssetAdditionDel where AFAA_AddtnDate < DATEADD(day, 180, '" & ddate & "') AND AFAA_AddtnDate > '" & ddate & "' and  AFAA_AssetType= " & assettype & " and AFAA_TrType=2 and AFAA_CompID=" & iACID & " and AFAA_YearID=" & iyearId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("AdditionAmount").ToString()) = False Then
                    Lessthan180days = dt.Rows(0)("AdditionAmount").ToString()
                Else
                    Lessthan180days = 0.0
                End If
            Else
                Lessthan180days = 0.0
            End If
            If Val(Lessthan180days) = 0 Then
                Lessthan180days = 0.0
            End If
            Return Lessthan180days
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Getdeletion(ByVal sNameSpace As String, ByVal assettype As Integer, ByVal iACID As Integer, ByVal iyearId As Integer) As Double
        Dim sSQL As String = ""
        Dim ddeletion As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select sum(AFAD_AssetDeltnAmount) as Deletions From Acc_FixedAssetDeletion Where AFAD_AssetType=" & assettype & " and AFAD_YearID=" & iyearId & " and AFAD_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Deletions").ToString()) = False Then
                    ddeletion = dt.Rows(0)("Deletions").ToString()
                Else
                    ddeletion = 0.0
                End If
            Else
                ddeletion = 0.0
            End If
            If Val(ddeletion) = 0 Then
                ddeletion = 0.0
            End If
            Return ddeletion
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadComnyAct(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sFinancialYear As String, ByVal iFYYearid As Integer, ByVal iCustid As Integer, ByVal sSelectedLocation As String, ByVal iInAmt As Integer, ByVal iRoundOff As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        'Dim dAddAmt As Double = 0.0
        'Dim dDelAmt As Double = 0.0
        'Dim costasat As Double = 0.0
        Dim dtAmt As Double = 0.0
        Dim ToDate, FromDate As Date
        Dim iPreviousYearID, ihPreviousYearID As Integer
        Dim dOriginalCost As Double = 0.0
        Dim dAdditions As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotalasat As Double = 0.0
        Dim dUptoDep As Double = 0.0
        Dim dOnOpenBalDep As Double = 0.0
        Dim dOnAdditionsDep As Double = 0.0
        Dim dDeletionsDep As Double = 0.0
        Dim dTotalDep As Double = 0.0
        Dim dTotalDepasonDep As Double = 0.0
        Dim dNBWDVAson As Double = 0.0
        Dim dpNBWDVAson As Double = 0.0

        Dim Costasat As Double = 0.0
        Dim AddAmount As Double = 0.0
        Dim DelAmount As Double = 0.0
        Dim depOnOpengBal As Double = 0.0
        Dim depAdditions As Double = 0.0
        Dim depDeletions As Double = 0.0
        Dim TotalDep As Double = 0.0
        Dim DepUptoPY As Double = 0.0
        Dim WDVasOnPY As Double = 0.0
        Dim WDVasOn As Double = 0.0
        Dim TotalDepasOn As Double = 0.0
        Dim dPrevYrAmt As Double = 0.0
        Dim dPrevTotalAMt As Double = 0.0

        Dim iFLCount As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AssetClass")
            'dt.Columns.Add("Asset")
            dt.Columns.Add("Costasat")
            dt.Columns.Add("Additions")
            dt.Columns.Add("Deletions")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("DepUptoPY")
            dt.Columns.Add("DepOnOpengBal")
            dt.Columns.Add("DepOnAdditions")
            dt.Columns.Add("DepOnDeletions")
            dt.Columns.Add("TotalDepFY")
            dt.Columns.Add("TotalDepasOn")
            dt.Columns.Add("WDVasOn")
            dt.Columns.Add("WDVasOnPY")

            dt.Columns.Add("PHUpto")
            dt.Columns.Add("HTotalDep")
            dt.Columns.Add("HTotalDepreciationason")
            dt.Columns.Add("HWDVason")
            dt.Columns.Add("PHWDVason")
            dt.Columns.Add("Total")

            dt.Columns.Add("AssesmentYear")

            If iyearId > 0 Then
                iPreviousYearID = iyearId - 1
            End If

            'sSql = "" : sSql = "select sum(a.AFAA_AssetAmount) as OriginalCost,a.AFAA_AssetType as AFAA_AssetType,sum(a.AFAA_DepreAmount) as DepAmountTill,sum(b.FAAD_AssetValue) as additionAmount,sum(c.AFAD_Amount) as delAmount,sum(d.ADep_DepreciationforFY ) as DepAmount from Acc_FixedAssetAdditionDel a"
            'sSql = sSql & " left join Acc_FixedAssetAdditionDetails b on b.FAAD_AssetType =a.AFAA_AssetType"
            'sSql = sSql & " left join Acc_FixedAssetDeletion c on c.AFAD_AssetClass = a.AFAA_AssetType"
            'sSql = sSql & " left join Acc_AssetDepreciation d on d.ADep_AssetID = a.AFAA_AssetType"
            'sSql = sSql & " left join Acc_FixedAssetAdditionDetails e on e.FAAD_AssetType = a.AFAA_AssetType"
            'sSql = sSql & " where AFAA_CustId=" & iCustid & " and AFAA_YearID=" & iPreviousYearID & " group by AFAA_AssetType"

            sSql = "  select sum(OriginalCost) as OriginalCost,sum(DepAmountTill) as DepAmountTill,sum(additionAmount) as additionAmount, "
            sSql = sSql & " sum(delAmount) as delAmount,sum(DepAmountOPB) as DepAmountOPB,sum(DepAmountAdd) as DepAmountAdd,AssetClass, sum(DelDeprec) as DelDeprec from ( "
            sSql = sSql & " select sum(AFAA_AssetAmount) as OriginalCost, sum(AFAA_DepreAmount) as DepAmountTill, 0 as additionAmount, 0 as delAmount, 0 as DepAmountOPB, 0 as DepAmountAdd,"
            sSql = sSql & " AFAA_AssetType as AssetClass,0 as DelDeprec from Acc_FixedAssetAdditionDel  where AFAA_CustId=" & iCustid & " "
            If sSelectedLocation = "" Then
            Else
                sSql = sSql & " and AFAA_Location in (" & sSelectedLocation & ")"
            End If
            sSql = sSql & " group by AFAA_AssetType union all "   'Remove AFAA_YearId for fetching original cost (Dk 06_08_22)
            sSql = sSql & " Select '0' as OriginalCost, 0 as DepAmountTill,sum(FAAD_AssetValue) as additionAmount, 0 as delAmount, 0 as  DepAmountOPB, 0 as  DepAmountAdd,FAAD_AssetType as AssetClass, 0 as DelDeprec from Acc_FixedAssetAdditionDetails "
            sSql = sSql & " where FAAD_CustId=" & iCustid & ""
            If sSelectedLocation = "" Then
            Else
                sSql = sSql & " and FAAD_Location in (" & sSelectedLocation & ")"
            End If
            sSql = sSql & " And FAAD_YearID = " & iyearId & " group by FAAD_AssetType union all"
            sSql = sSql & " Select 0 As OriginalCost, 0 As DepAmountTill,0 As additionAmoun, sum(AFAD_Amount) As delAmount,0 As DepAmountOPB, 0 As DepAmountAdd, AFAD_AssetClass As AssetClass,sum(AFAD_DelDeprec) As DelDeprec "
            sSql = sSql & " from Acc_FixedAssetDeletion where AFAD_CustomerName=" & iCustid & ""
            If sSelectedLocation = "" Then
            Else
                sSql = sSql & " and AFAD_Location in (" & sSelectedLocation & ")"
            End If
            sSql = sSql & " And AFAD_YearID=" & iyearId & " group by AFAD_AssetClass union all"
            sSql = sSql & " Select 0 As OriginalCost, 0 As DepAmountTill,0 As additionAmount, 0 As delAmount,sum(Case When ADep_TransType =1  Then ADep_DepreciationforFY Else 0 End ) As DepAmountOPB,sum(Case When ADep_TransType =2  Then ADep_DepreciationforFY Else 0 End ) As DepAmountAdd, ADep_AssetID As AssetClass,0 As DelDeprec from Acc_AssetDepreciation where ADep_CustId=" & iCustid & ""
            If sSelectedLocation = "" Then
            Else
                sSql = sSql & " And ADep_Location In (" & sSelectedLocation & ")"
            End If
            sSql = sSql & " And ADep_YearID =" & iyearId & " group by ADep_AssetID) As temp group by AssetClass"

            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("ID") = 0
                If IsDBNull(dtDetails.Rows(i)("AssetClass")) = False Then
                    dRow("AssetClass") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dtDetails.Rows(i)("AssetClass") & " And AM_CompID=" & iACID & " And AM_CustId=" & iCustid & "")
                End If
                'If IsDBNull(dtDetails.Rows(i)("OriginalCost")) = False Then
                '    dRow("Costasat") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("OriginalCost"))).ToString("##,0")
                '    Costasat = dtDetails.Rows(i)("OriginalCost")
                '    dOriginalCost = dOriginalCost + Convert.ToDouble(dtDetails.Rows(i)("OriginalCost").ToString())
                'Else
                '    dOriginalCost = 0
                '    dRow("Costasat") = 0
                'End If

                'If IsDBNull(dtDetails.Rows(i)("OriginalCost")) = False Then  'Commented for original cost wrong for next year
                '    Dim dAmount As Double = 0.00
                '    dAmount = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADep_OPBForYR),0) from Acc_AssetDepreciation where ADep_CompID=" & iACID & " And  ADep_YearID=" & iyearId & " And ADep_CustId=" & iCustid & " And ADep_AssetID =" & dtDetails.Rows(i)("AssetClass") & "")
                '    If dAmount <> 0 Then
                '        'dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmount)).ToString("#,##0")

                '        If (iInAmt > 0) Then
                '            dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmount) / iInAmt).ToString("N1")
                '        Else
                '            dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmount)).ToString("#,##0")
                '        End If

                '        Costasat = dAmount
                '        dOriginalCost = dOriginalCost + Convert.ToDouble((dAmount).ToString())
                '    Else
                '        Dim dAmountT As Double = 0.00
                '        dAmountT = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(AFAA_AssetAmount),0) from Acc_FixedAssetAdditionDel where AFAA_CompID =" & iACID & " And  AFAA_YearID=" & iyearId & " And AFAA_CustId=" & iCustid & " And AFAA_AssetType =" & dtDetails.Rows(i)("AssetClass") & "")
                '        'dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT)).ToString("#,##0")

                '        If (dAmountT > 0) Then
                '            If (iInAmt > 0) Then
                '                dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT) / iInAmt).ToString("N1")
                '            Else
                '                dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT)).ToString("#,##0")
                '            End If
                '        Else
                '            dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT)).ToString("#,##0")
                '        End If

                '        Costasat = dAmountT
                '        dOriginalCost = dOriginalCost + Convert.ToDouble((dAmountT).ToString())
                '    End If
                'Else
                '    dOriginalCost = 0
                '    dRow("Costasat") = 0
                'End If



                If IsDBNull(dtDetails.Rows(i)("OriginalCost")) = False Then
                    Dim dAmount As Double = 0.00
                    Dim dAddAmt As Double = 0.00
                    Dim ddAddAmt As Double = 0.0
                    dAmount = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(AFAA_AssetAmount),0) from Acc_FixedAssetAdditionDel where AFAA_CompID =" & iACID & " And  AFAA_YearID <=" & iyearId & " And AFAA_CustId=" & iCustid & " And AFAA_AssetType =" & dtDetails.Rows(i)("AssetClass") & "")
                    If dAmount <> 0 Then
                        'dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmount)).ToString("#,##0")

                        'Dk 30_03_23
                        dAddAmt = objDBL.SQLGetDescription(sNameSpace, " Select isnull(sum(FAAD_AssetValue),0) As additionAmount from Acc_FixedAssetAdditionDetails where FAAD_CustId=" & iCustid & " And  FAAD_YearID < " & iyearId & "  And FAAD_AssetType =" & dtDetails.Rows(i)("AssetClass") & "")
                        ddAddAmt = dAddAmt + dAmount
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("Costasat") = Convert.ToDecimal(Math.Round(ddAddAmt) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("Costasat") = Convert.ToDecimal(Math.Round(ddAddAmt) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("Costasat") = Convert.ToDecimal(Math.Round(ddAddAmt) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("Costasat") = Convert.ToDecimal(Math.Round(ddAddAmt) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("Costasat") = Convert.ToDecimal(Math.Round(ddAddAmt) / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("Costasat") = Convert.ToDecimal(Math.Round(ddAddAmt)).ToString("#,##0")
                        End If

                        Costasat = ddAddAmt
                        dOriginalCost = dOriginalCost + Convert.ToDouble((ddAddAmt).ToString())
                    Else
                        Dim dAmountT As Double = 0.00
                        dAmountT = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADep_OPBForYR),0) from  Acc_AssetDepreciation where ADep_YearID <" & iyearId & " And ADep_Method=2 And  adep_custid=" & iCustid & " And ADep_CompID=" & iACID & "  And ADep_AssetID=" & dtDetails.Rows(i)("AssetClass") & " ")
                        'dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT)).ToString("#,##0")

                        If (dAmountT > 0) Then
                            If (iInAmt > 0) Then
                                If (iRoundOff = 0) Then
                                    dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT) / iInAmt).ToString("N0")
                                ElseIf (iRoundOff = 1) Then
                                    dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT) / iInAmt).ToString("N1")
                                ElseIf (iRoundOff = 2) Then
                                    dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT) / iInAmt).ToString("N2")
                                ElseIf (iRoundOff = 3) Then
                                    dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT) / iInAmt).ToString("N3")
                                ElseIf (iRoundOff = 4) Then
                                    dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT) / iInAmt).ToString("N4")
                                End If
                            Else
                                dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT)).ToString("#,##0")
                            End If
                        Else
                            dRow("Costasat") = Convert.ToDecimal(Math.Round(dAmountT)).ToString("#,##0")
                        End If

                        Costasat = dAmountT
                        dOriginalCost = dOriginalCost + Convert.ToDouble((dAmountT).ToString())
                    End If
                Else
                    dOriginalCost = 0
                    dRow("Costasat") = 0
                End If




                If IsDBNull(dtDetails.Rows(i)("additionAmount")) = False Then
                    'dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount"))).ToString("#,##0")
                    If (Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount"))) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount")) / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount"))).ToString("#,##0")
                        End If
                    Else
                        dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount"))).ToString("#,##0")
                    End If

                    AddAmount = dtDetails.Rows(i)("additionAmount")
                    dAdditions = dAdditions + Convert.ToDouble(dtDetails.Rows(i)("additionAmount").ToString())
                Else
                    dAdditions = 0
                    dRow("Additions") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("delAmount")) = False Then
                    'dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount"))).ToString("#,##0")

                    If (Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount"))) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount")) / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount"))).ToString("#,##0")
                        End If
                    Else
                        dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount"))).ToString("#,##0")
                    End If

                    DelAmount = dtDetails.Rows(i)("delAmount")
                    dDeletions = dDeletions + Convert.ToDouble(dtDetails.Rows(i)("delAmount").ToString())
                Else
                    dDeletions = 0
                    dRow("Deletions") = 0
                End If

                'dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount)).ToString("#,##0")
                If (Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount)) > 0) Then
                    If (iInAmt > 0) Then
                        If (iRoundOff = 0) Then
                            dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount) / iInAmt).ToString("N0")
                        ElseIf (iRoundOff = 1) Then
                            dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount) / iInAmt).ToString("N1")
                        ElseIf (iRoundOff = 2) Then
                            dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount) / iInAmt).ToString("N2")
                        ElseIf (iRoundOff = 3) Then
                            dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount) / iInAmt).ToString("N3")
                        ElseIf (iRoundOff = 4) Then
                            dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount) / iInAmt).ToString("N4")
                        End If
                    Else
                        dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount)).ToString("#,##0")
                    End If
                Else
                    dRow("TotalAmount") = Convert.ToDecimal(Math.Round(Costasat + AddAmount - DelAmount)).ToString("#,##0")
                End If
                dTotalasat = dTotalasat + Convert.ToDouble(dRow("TotalAmount").ToString())


                '' Commented by Dk (31/03/23) For deptill amt
                'If IsDBNull(dtDetails.Rows(i)("DepAmountTill")) = False Then
                '    DepUptoPY = dtDetails.Rows(i)("DepAmountTill")
                '    ' dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY).ToString("#,##0")

                '    If (Convert.ToDecimal(DepUptoPY) > 0) Then
                '        If (iInAmt > 0) Then
                '            dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY / iInAmt).ToString("N1")
                '        Else
                '            dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY).ToString("#,##0")
                '        End If
                '    Else
                '        dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY).ToString("#,##0")
                '    End If

                '    dUptoDep = dUptoDep + Convert.ToDouble(DepUptoPY.ToString())
                'End If
                iFLCount = objDBL.SQLGetDescription(sNameSpace, "Select  count(*) FROM Acc_AssetDepreciation where ADep_YearID =" & iyearId - 1 & " And adep_custid=" & iCustid & " And ADep_CompID=" & iACID & "  And ADep_AssetID=" & dtDetails.Rows(i)("AssetClass") & " ")

                If iFLCount <> 0 Then
                    dPrevYrAmt = objDBL.SQLGetDescription(sNameSpace, "Select  isnull(sum(ADep_DepreciationforFY),0) FROM Acc_AssetDepreciation where ADep_YearID <=" & iyearId - 1 & " And adep_custid=" & iCustid & " And ADep_CompID=" & iACID & "  And ADep_AssetID=" & dtDetails.Rows(i)("AssetClass") & " ")
                    dPrevTotalAMt = dPrevYrAmt + dtDetails.Rows(i)("DepAmountTill")
                    DepUptoPY = dPrevTotalAMt
                Else
                    DepUptoPY = dtDetails.Rows(i)("DepAmountTill")
                End If

                If IsDBNull(dtDetails.Rows(i)("DepAmountTill")) = False Then
                    ' dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY).ToString("#,##0")

                    If (Convert.ToDecimal(DepUptoPY) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY).ToString("#,##0")
                        End If
                    Else
                        dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY).ToString("#,##0")
                    End If

                    dUptoDep = dUptoDep + Convert.ToDouble(DepUptoPY.ToString())
                End If


                If IsDBNull(dtDetails.Rows(i)("DepAmountOPB")) = False Then
                    Dim opbAmount As Double = 0.00
                    Dim deletionAmount As Double = 0.00
                    Dim reuslt As Double = 0.00
                    opbAmount = dtDetails.Rows(i)("DepAmountOPB")
                    deletionAmount = dtDetails.Rows(i)("DelDeprec")
                    reuslt = opbAmount - deletionAmount
                    ' dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt)).ToString("#,##0")

                    If (Convert.ToDecimal(Math.Round(reuslt)) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt) / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt)).ToString("#,##0")
                        End If
                    Else
                        dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(reuslt)).ToString("#,##0")
                    End If

                    depOnOpengBal = reuslt
                    dOnOpenBalDep = dOnOpenBalDep + Convert.ToDouble(depOnOpengBal.ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("DepAmountAdd")) = False Then
                    'dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd"))).ToString("#,##0")

                    If (Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd"))) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd")) / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd"))).ToString("#,##0")
                        End If
                    Else
                        dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd"))).ToString("#,##0")
                    End If

                    depAdditions = dtDetails.Rows(i)("DepAmountAdd")
                    dOnAdditionsDep = dOnAdditionsDep + Convert.ToDouble(depAdditions.ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("DelDeprec")) = False Then
                    'dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec"))).ToString("#,##0")

                    If (Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec"))) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec")) / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec"))).ToString("#,##0")
                        End If
                    Else
                        dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec"))).ToString("#,##0")
                    End If

                    depDeletions = dtDetails.Rows(i)("DelDeprec")
                    dDeletionsDep = dDeletionsDep + Convert.ToDouble(depDeletions.ToString())
                End If

                TotalDep = depOnOpengBal + depAdditions + depDeletions
                'dRow("TotalDepFY") = Convert.ToDecimal(TotalDep).ToString("#,##0")

                If (Convert.ToDecimal(TotalDep) > 0) Then
                    If (iInAmt > 0) Then
                        If (iRoundOff = 0) Then
                            dRow("TotalDepFY") = Convert.ToDecimal(TotalDep / iInAmt).ToString("N0")
                        ElseIf (iRoundOff = 1) Then
                            dRow("TotalDepFY") = Convert.ToDecimal(TotalDep / iInAmt).ToString("N1")
                        ElseIf (iRoundOff = 2) Then
                            dRow("TotalDepFY") = Convert.ToDecimal(TotalDep / iInAmt).ToString("N2")
                        ElseIf (iRoundOff = 3) Then
                            dRow("TotalDepFY") = Convert.ToDecimal(TotalDep / iInAmt).ToString("N3")
                        ElseIf (iRoundOff = 4) Then
                            dRow("TotalDepFY") = Convert.ToDecimal(TotalDep / iInAmt).ToString("N4")
                        End If

                    Else
                        dRow("TotalDepFY") = Convert.ToDecimal(TotalDep).ToString("#,##0")
                    End If
                Else
                    dRow("TotalDepFY") = Convert.ToDecimal(TotalDep).ToString("#,##0")
                End If

                dTotalDep = dTotalDep + TotalDep

                TotalDepasOn = Val(DepUptoPY) + Val(TotalDep)
                'dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn).ToString("#,##0")

                If (Convert.ToDecimal(TotalDepasOn) > 0) Then
                    If (iInAmt > 0) Then
                        If (iRoundOff = 0) Then
                            dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn / iInAmt).ToString("N0")
                        ElseIf (iRoundOff = 1) Then
                            dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn / iInAmt).ToString("N1")
                        ElseIf (iRoundOff = 2) Then
                            dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn / iInAmt).ToString("N2")
                        ElseIf (iRoundOff = 3) Then
                            dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn / iInAmt).ToString("N3")
                        ElseIf (iRoundOff = 4) Then
                            dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn / iInAmt).ToString("N4")
                        End If
                    Else
                        dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn).ToString("#,##0")
                    End If
                Else
                    dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn).ToString("#,##0")
                End If

                dTotalDepasonDep = dTotalDepasonDep + TotalDepasOn

                WDVasOn = dRow("TotalAmount") - Val(TotalDepasOn)
                'dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn)).ToString("#,##0")

                If (Convert.ToDecimal(Math.Round(WDVasOn)) > 0) Then
                    If (iInAmt > 0) Then
                        If (iRoundOff = 0) Then
                            dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn) / iInAmt).ToString("N0")
                        ElseIf (iRoundOff = 1) Then
                            dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn) / iInAmt).ToString("N1")
                        ElseIf (iRoundOff = 2) Then
                            dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn) / iInAmt).ToString("N2")
                        ElseIf (iRoundOff = 3) Then
                            dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn) / iInAmt).ToString("N3")
                        ElseIf (iRoundOff = 4) Then
                            dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn) / iInAmt).ToString("N4")
                        End If
                    Else
                        dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn)).ToString("#,##0")
                    End If
                Else
                    dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn)).ToString("#,##0")
                End If

                dNBWDVAson = dNBWDVAson + WDVasOn

                WDVasOnPY = Val(Costasat) - Val(DepUptoPY)
                ' dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY)).ToString("#,##0")

                If (Convert.ToDecimal(Math.Round(WDVasOnPY)) > 0) Then
                    If (iInAmt > 0) Then
                        If (iRoundOff = 0) Then
                            dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY) / iInAmt).ToString("N0")
                        ElseIf (iRoundOff = 1) Then
                            dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY) / iInAmt).ToString("N1")
                        ElseIf (iRoundOff = 2) Then
                            dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY) / iInAmt).ToString("N2")
                        ElseIf (iRoundOff = 3) Then
                            dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY) / iInAmt).ToString("N3")
                        ElseIf (iRoundOff = 4) Then
                            dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY) / iInAmt).ToString("N4")
                        End If
                    Else
                        dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY)).ToString("#,##0")
                    End If
                Else
                    dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY)).ToString("#,##0")
                End If
                dpNBWDVAson = dpNBWDVAson + WDVasOnPY

                Dim dtYearDetails As DataTable
                dtYearDetails = LoadAssetDetails(sNameSpace, iPreviousYearID, iACID)

                If iPreviousYearID > 0 Then
                    ihPreviousYearID = iPreviousYearID - 1
                End If

                dRow("AssesmentYear") = objDBL.SQLGetDescription(sNameSpace, "Select YMS_ID From Year_Master Where YMS_YEARID=" & iyearId + 1 & " And YMS_CompID=" & iACID & "")
                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & ihPreviousYearID & " And YMS_CompID=" & iACID & "")

                dRow("FromDate") = "Cost As at" & " " & dtYearDetails.Rows(0)("YMSFROMDATE")
                dRow("ToDate") = "Total As at" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("PHUpto") = "Up To" & " " & ToDate
                dRow("HTotalDep") = "Total Dep." & " " & dtYearDetails.Rows(0)("YMSID")
                dRow("HTotalDepreciationason") = "Total Depreciation As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("HWDVason") = "WDV As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("PHWDVason") = "WDV As On" & " " & ToDate
                dt.Rows.Add(dRow)
            Next

            dRow = dt.NewRow()
            dRow("AssetClass") = "<b>" & "Sub Total" & "</b>"
            dRow("Costasat") = "<b>" & Convert.ToDecimal(Math.Round(dOriginalCost)).ToString("#,##0") & "</b>"
            dRow("Additions") = "<b>" & Convert.ToDecimal(Math.Round(dAdditions)).ToString("#,##0") & "</b>"
            dRow("Deletions") = "<b>" & Convert.ToDecimal(Math.Round(dDeletions)).ToString("#,##0") & "</b>"
            dRow("TotalAmount") = "<b>" & Convert.ToDecimal(Math.Round(dTotalasat)).ToString("#,##0") & "</b>"
            dRow("DepUptoPY") = "<b>" & Convert.ToDecimal(Math.Round(dUptoDep)).ToString("#,##0") & "</b>"
            dRow("DepOnOpengBal") = "<b>" & Convert.ToDecimal(Math.Round(dOnOpenBalDep)).ToString("#,##0") & "</b>"
            dRow("DepOnAdditions") = "<b>" & Convert.ToDecimal(Math.Round(dOnAdditionsDep)).ToString("#,##0") & "</b>"
            dRow("DepOnDeletions") = "<b>" & Convert.ToDecimal(Math.Round(dDeletionsDep)).ToString("#,##0") & "</b>"
            dRow("TotalDepFY") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDep)).ToString("#,##0") & "</b>"
            dRow("TotalDepasOn") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDepasonDep)).ToString("#,##0") & "</b>"
            dRow("WDVasOn") = "<b>" & Convert.ToDecimal(Math.Round(dNBWDVAson)).ToString("#,##0") & "</b>"
            dRow("WDVasOnPY") = "<b>" & Convert.ToDecimal(Math.Round(dpNBWDVAson)).ToString("#,##0") & "</b>"
            dt.Rows.Add(dRow)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetDetails(ByVal sNameSpace As String, ByVal iyearId As Integer, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtAsstDetails As New DataTable
        Dim dRow As DataRow
        Dim j = 0
        Try
            dt.Columns.Add("YMSID")
            dt.Columns.Add("YMSFROMDATE")
            dt.Columns.Add("YMSTODATE")
            sSql = "" : sSql = "Select YMS_ID, Convert(varchar, YMS_FROMDATE, 105) As YMS_FROMDATE, Convert(varchar, YMS_TODATE, 105) As YMS_TODATE from Year_Master  Where YMS_YEARID=" & iyearId & " And YMS_CompID=" & iACID & ""
            dtAsstDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dtAsstDetails.Rows.Count > 0 Then
                For i = 0 To dtAsstDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("YMSID") = dtAsstDetails.Rows(i)("YMS_ID")
                    dRow("YMSFROMDATE") = dtAsstDetails.Rows(i)("YMS_FROMDATE")
                    dRow("YMSTODATE") = dtAsstDetails.Rows(i)("YMS_TODATE")
                    dt.Rows.Add(dRow)
                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function DepAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAssetClassId As Integer, ByVal iAsset As Integer, ByVal iCustID As Integer) As String
    '    Dim sSql As String = ""
    '    Dim iPreviousYearID As Integer
    '    Dim dt As New DataTable
    '    Try
    '        If iYearID > 0 Then
    '            iPreviousYearID = iYearID - 1
    '        End If
    '        sSql = "Select isnull(ADep_WrittenDownValue, 0) As costasat From Acc_AssetDepreciation Where ADep_CompID=" & iCompID & " And ADep_YearID=" & iPreviousYearID & " And ADep_AssetID=" & iAssetClassId & " And ADep_Item=" & iAsset & " And ADep_CustId=" & iCustID & ""
    '        GetPreviousYrFreezeLedgerCount = objDBL.SQLGetDescription(sNameSpace, sSql)
    '        Return GetPreviousYrFreezeLedgerCount()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetPreviousYrFreezeLedgerCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAssetClassId As Integer, ByVal iAsset As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Dim dt As New DataTable
        Try
            If iYearID > 0 Then
                iPreviousYearID = iYearID - 1
            End If
            sSql = "Select isnull(ADep_WrittenDownValue,0) As costasat From Acc_AssetDepreciation Where ADep_CompID=" & iCompID & " And ADep_YearID=" & iPreviousYearID & " And ADep_AssetID=" & iAssetClassId & " And ADep_Item=" & iAsset & " And ADep_CustId=" & iCustID & ""
            GetPreviousYrFreezeLedgerCount = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetPreviousYrFreezeLedgerCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DepAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAssetClassId As Integer, ByVal iAsset As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Dim dt As New DataTable
        Try
            If iYearID > 0 Then
                iPreviousYearID = iYearID - 1
            End If
            sSql = "Select isnull(ADep_WrittenDownValue,0) As costasat From Acc_AssetDepreciation Where ADep_CompID=" & iCompID & " And ADep_YearID=" & iPreviousYearID & " And ADep_AssetID=" & iAssetClassId & " And ADep_Item=" & iAsset & " And ADep_CustId=" & iCustID & ""
            DepAmount = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return DepAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iYearId As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Try
            If iYearId > 0 Then
                iPreviousYearID = iYearId - 1
            End If
            sSql = "Select isnull(sum(FAAD_AssetValue),0) from Acc_FixedAssetAdditionDetails "
            sSql = sSql & " where FAAD_CompID=" & iCompID & " AND FAAD_AssetType=" & iAssetClassId & " And FAAD_ItemType=" & iAssetId & " And FAAD_Status<>'D' and FAAD_CustId=" & iCustID & " And FAAD_YearID=" & iPreviousYearID & ""
            GetAmount = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDelAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iYearId As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Try
            If iYearId > 0 Then
                iPreviousYearID = iYearId - 1
            End If
            sSql = "select AFAD_AssetDeletionType,isnull(AFAD_Amount,0) as Amount from Acc_FixedAssetDeletion "
            sSql = sSql & " where AFAD_CompID=" & iCompID & " and AFAD_AssetClass=" & iAssetClassId & " and AFAD_Asset=" & iAssetId & "  and AFAD_CustomerName=" & iCustID & " And AFAD_YearID=" & iPreviousYearID & ""
            GetDelAmount = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetDelAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCADetails(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sFinancialYear As String, ByVal iFYYearid As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim ToDate As Date
        Try

            dt.Columns.Add("ID")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("Asset")
            dt.Columns.Add("Dateofpurchase")
            dt.Columns.Add("OriginalCost")
            dt.Columns.Add("Lifeasper")
            dt.Columns.Add("SalvageValue")
            dt.Columns.Add("Depreciableamount")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("Deprate")
            dt.Columns.Add("WDVason")
            dt.Columns.Add("DepreciaitonforFY")
            dt.Columns.Add("OPBForYR")
            dt.Columns.Add("BalancetoClaim")

            dt.Columns.Add("YRDate")
            dt.Columns.Add("DepClaimedason")
            dt.Columns.Add("DepreciaitonforFYH")
            dt.Columns.Add("OPBForFYH")


            sSql = "" : sSql = "select AFAA_ID,AFAA_AssetType,AFAA_Description,AFAA_ItemDescription,AFAA_PurchaseDate,AFAA_AssetAmount,AFAA_AssetAge,AFAA_Depreciation,AFAA_FYAmount from Acc_FixedAssetAdditionDel"

            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                'dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("AFAA_ID")) = False Then
                    dRow("ID") = dtDetails.Rows(i)("AFAA_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_Description")) = False Then
                    dRow("AssetClass") = dtDetails.Rows(i)("AFAA_Description")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_ItemDescription")) = False Then
                    dRow("Item") = dtDetails.Rows(i)("AFAA_ItemDescription")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("Dateofpurchase") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i)("AFAA_PurchaseDate"), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAmount")) = False Then
                    dRow("OriginalCost") = Convert.ToDecimal(dtDetails.Rows(i)("AFAA_AssetAmount")).ToString("#,##0")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAge")) = False Then
                    dRow("Lifeasper") = dtDetails.Rows(i)("AFAA_AssetAge")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("AssetType") = dtDetails.Rows(i)("AFAA_AssetType")
                End If

                Dim ResidualValue As Double = objDBL.SQLGetDescription(sNameSpace, "Select AM_ResidualValue From Acc_AssetMaster Where AM_ID=" & dRow("AssetType") & " and AM_YearID=" & iyearId & "")

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAmount")) = False Then
                    dRow("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dRow("OriginalCost")) / 100))).ToString("#,##0")
                Else
                    dRow("SalvageValue") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAmount")) = False Then
                    dRow("Depreciableamount") = Convert.ToDecimal(Math.Round((dRow("OriginalCost") - dRow("SalvageValue")))).ToString("#,##0")
                Else
                    dRow("Depreciableamount") = 0
                End If

                'If IsDBNull(dtDetails.Rows(i)("AFAA_Depreciation")) = False Then
                '    dRow("Deprate") = dtDetails.Rows(i)("AFAA_Depreciation")
                'End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAmount")) = False Then
                    Dim w As Integer = dRow("Lifeasper")
                    dRow("Deprate") = String.Format("{0:0.00}", (1 - (dRow("SalvageValue") / dRow("OriginalCost")) ^ (1 / w)) * 100)
                Else
                    dRow("Deprate") = 0
                End If

                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From ACC_Year_Master Where YMS_ID=" & iFYYearid & "")

                'If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                '    Dim diff As Integer = DateDiffComputation(sNameSpace, iACID, dRow("Dateofpurchase"), ToDate)
                '    dRow("Days") = diff
                'End If

                'Dim FYAmount As Double
                If IsDBNull(dtDetails.Rows(i)("AFAA_FYAmount")) = False Then
                    dRow("WDVason") = Convert.ToDecimal(dtDetails.Rows(i)("AFAA_FYAmount")).ToString("#,##0")
                Else
                    dRow("WDVason") = 0
                End If

                If dRow("Deprate") <> "NaN" Then
                    dRow("DepreciaitonforFY") = Convert.ToDecimal(Math.Round(((dRow("WDVason") * dRow("Deprate")) / 100) * (365 / 365))).ToString("#,##0")
                Else
                    dRow("DepreciaitonforFY") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("AFAA_FYAmount")) = False Then
                    Dim Damount As Double = dRow("WDVason") - dRow("DepreciaitonforFY")
                    If Damount > dRow("SalvageValue") Then
                        dRow("OPBForYR") = Convert.ToDecimal(Math.Round(Damount)).ToString("#,##0")
                    Else
                        dRow("OPBForYR") = 0
                    End If
                End If

                '1 – [s/c]1/n
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetAmount")) = False Then
                    dRow("BalancetoClaim") = Convert.ToDecimal(Math.Round(dRow("OriginalCost") - dRow("OPBForYR"))).ToString("#,##0")
                End If

                'dRow("FYear") = sFinancialYear
                dRow("YRDate") = "W.D.V as on" & " " & ToDate
                dRow("DepClaimedason") = "Dep. Claimed as on" & " " & ToDate
                dRow("DepreciaitonforFYH") = "Depreciaiton for FY" & " " & sFinancialYear
                dRow("OPBForFYH") = "OPB For YR" & " " & sFinancialYear
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DateDiffComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dFrmDt As Date, ByVal dTodt As Date) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SELECT DATEDIFF(day, '" & objGen.FormatDtForRDBMS(dFrmDt, "CT") & "','" & objGen.FormatDtForRDBMS(dTodt, "CT") & "')"
            DateDiffComputation = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return DateDiffComputation
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Loadtodate(ByVal sNameSpace As String, ByVal iCompID As Integer, iyearid As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select YMS_TODATE From ACC_Year_Master Where YMS_ID=" & iyearid & ""
            Loadtodate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return Loadtodate
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadDynComnyAct(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sFinancialYear As String, ByVal iFYYearid As Integer, ByVal iCustid As Integer,
    '     ByVal iLocationId As Integer, ByVal iDivId As Integer, ByVal iDeptId As Integer, ByVal iBayId As Integer, ByVal iAsstCls As Integer) As DataTable
    '    Dim dt As New DataTable
    '    Dim dtDetails2 As New DataTable
    '    Dim dRow As DataRow
    '    Dim sSql As String = ""
    '    'Dim dAddAmt As Double = 0.0
    '    'Dim dDelAmt As Double = 0.0
    '    'Dim costasat As Double = 0.0
    '    Dim dtAmt As Double = 0.0
    '    Dim ToDate, FromDate As Date
    '    Dim iPreviousYearID, ihPreviousYearID As Integer
    '    Dim dOriginalCost As Double = 0.0
    '    Dim dAdditions As Double = 0.0
    '    Dim dDeletions As Double = 0.0
    '    Dim dTotalasat As Double = 0.0
    '    Dim dUptoDep As Double = 0.0
    '    Dim dOnOpenBalDep As Double = 0.0
    '    Dim dOnAdditionsDep As Double = 0.0
    '    Dim dDeletionsDep As Double = 0.0
    '    Dim dTotalDep As Double = 0.0
    '    Dim dTotalDepasonDep As Double = 0.0
    '    Dim dNBWDVAson As Double = 0.0
    '    Dim dpNBWDVAson As Double = 0.0
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("AssetClass")
    '        'dt.Columns.Add("Asset")
    '        dt.Columns.Add("Costasat")
    '        dt.Columns.Add("Additions")
    '        dt.Columns.Add("Deletions")
    '        dt.Columns.Add("TotalAmount")
    '        dt.Columns.Add("ToDate")
    '        dt.Columns.Add("FromDate")
    '        dt.Columns.Add("DepUptoPY")
    '        dt.Columns.Add("DepOnOpengBal")
    '        dt.Columns.Add("DepOnAdditions")
    '        dt.Columns.Add("DepOnDeletions")
    '        dt.Columns.Add("TotalDepFY")
    '        dt.Columns.Add("TotalDepasOn")
    '        dt.Columns.Add("WDVasOn")
    '        dt.Columns.Add("WDVasOnPY")
    '        dt.Columns.Add("PHUpto")
    '        dt.Columns.Add("HTotalDep")
    '        dt.Columns.Add("HTotalDepreciationason")
    '        dt.Columns.Add("HWDVason")
    '        dt.Columns.Add("PHWDVason")
    '        dt.Columns.Add("Total")
    '        If iyearId > 0 Then
    '            iPreviousYearID = iyearId - 1
    '        End If
    '        sSql = "  select sum(OriginalCost) as OriginalCost,sum(DepAmountTill) as DepAmountTill,sum(additionAmount) as additionAmount, "
    '        sSql = sSql & " sum(delAmount) as delAmount,sum(DepAmount) as DepAmount,AssetClass from ( "
    '        sSql = sSql & " select sum(AFAA_AssetAmount) as OriginalCost, sum(AFAA_DepreAmount) as DepAmountTill, 0 as additionAmount, 0 as delAmount, 0 as DepAmount,"
    '        sSql = sSql & " AFAA_AssetType as AssetClass from Acc_FixedAssetAdditionDel  where AFAA_CustId=" & iCustid & " and AFAA_YearID=" & iPreviousYearID & " "
    '        If iLocationId <> 0 Then
    '            sSql = sSql & " and AFAA_Location =" & iLocationId & ""
    '        End If
    '        If iDivId <> 0 Then
    '            sSql = sSql & " and AFAA_Division =" & iDivId & ""
    '        End If
    '        If iDeptId <> 0 Then
    '            sSql = sSql & " and AFAA_Department =" & iDeptId & ""
    '        End If
    '        If iBayId <> 0 Then
    '            sSql = sSql & " and AFAA_Bay =" & iBayId & ""
    '        End If
    '        If iAsstCls <> 0 Then
    '            sSql = sSql & " and AFAA_AssetType =" & iAsstCls & ""
    '        End If
    '        sSql = sSql & " group by AFAA_AssetType union all "
    '        sSql = sSql & " select '0' as OriginalCost, 0 as DepAmountTill,sum(FAAD_AssetValue) as additionAmount, 0 as delAmount, 0 as  DepAmount,FAAD_AssetType as AssetClass from Acc_FixedAssetAdditionDetails "
    '        sSql = sSql & " where FAAD_CustId=" & iCustid & " and FAAD_YearID=" & iPreviousYearID & " "
    '        If iLocationId <> 0 Then
    '            sSql = sSql & " and AFAD_Location =" & iLocationId & ""
    '        End If
    '        If iDivId <> 0 Then
    '            sSql = sSql & " and FAAD_Division =" & iDivId & ""
    '        End If
    '        If iDeptId <> 0 Then
    '            sSql = sSql & " and FAAD_Department =" & iDeptId & ""
    '        End If
    '        If iBayId <> 0 Then
    '            sSql = sSql & " and FAAD_Bay =" & iBayId & ""
    '        End If
    '        If iAsstCls <> 0 Then
    '            sSql = sSql & " and FAAD_AssetType =" & iAsstCls & ""
    '        End If
    '        sSql = sSql & " group by FAAD_AssetType union all"
    '        sSql = sSql & " select 0 as OriginalCost, 0 as DepAmountTill,0 as additionAmoun, sum(AFAD_Amount) as delAmount,0 as DepAmount, AFAD_AssetClass as AssetClass "
    '        sSql = sSql & " from Acc_FixedAssetDeletion where AFAD_CustomerName=" & iCustid & " and AFAD_YearID=" & iPreviousYearID & " "
    '        If iLocationId <> 0 Then
    '            sSql = sSql & " and AFAD_Location =" & iLocationId & ""
    '        End If
    '        If iDivId <> 0 Then
    '            sSql = sSql & " and AFAD_Division =" & iDivId & ""
    '        End If
    '        If iDeptId <> 0 Then
    '            sSql = sSql & " and AFAD_Department =" & iDeptId & ""
    '        End If
    '        If iBayId <> 0 Then
    '            sSql = sSql & " and AFAD_Bay =" & iBayId & ""
    '        End If
    '        If iAsstCls <> 0 Then
    '            sSql = sSql & " and AFAD_AssetClass =" & iAsstCls & ""
    '        End If
    '        sSql = sSql & " group by AFAD_AssetClass union all"
    '        sSql = sSql & " select 0 as OriginalCost, 0 as DepAmountTill,0 as additionAmount, 0 as delAmount,sum(ADep_DepreciationforFY) as DepAmount, ADep_AssetID as AssetClass from Acc_AssetDepreciation where ADep_CustId=" & iCustid & ""
    '        sSql = sSql & " and ADep_YearID=" & iPreviousYearID & " "
    '        If iLocationId <> 0 Then
    '            sSql = sSql & " and ADep_Location =" & iLocationId & ""
    '        End If
    '        If iDivId <> 0 Then
    '            sSql = sSql & " and ADep_Division =" & iDivId & ""
    '        End If
    '        If iDeptId <> 0 Then
    '            sSql = sSql & " and ADep_Department =" & iDeptId & ""
    '        End If
    '        If iBayId <> 0 Then
    '            sSql = sSql & " and ADep_Bay =" & iBayId & ""
    '        End If
    '        If iAsstCls <> 0 Then
    '            sSql = sSql & " and ADep_AssetID =" & iAsstCls & ""
    '        End If
    '        sSql = sSql & " group by ADep_AssetID) as temp group by AssetClass"

    '        dtDetails2 = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        For i = 0 To dtDetails2.Rows.Count - 1
    '            dRow = dt.NewRow()
    '            dRow("ID") = 0
    '            If IsDBNull(dtDetails2.Rows(i)("AssetClass")) = False Then
    '                dRow("AssetClass") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dtDetails2.Rows(i)("AssetClass") & " and AM_CompID=" & iACID & " and AM_CustId=" & iCustid & "")
    '            End If
    '            If IsDBNull(dtDetails2.Rows(i)("OriginalCost")) = False Then
    '                dRow("Costasat") = dtDetails2.Rows(i)("OriginalCost")
    '                dOriginalCost = dOriginalCost + Convert.ToDouble(dtDetails2.Rows(i)("OriginalCost").ToString())
    '            Else
    '                dOriginalCost = 0
    '                dRow("Costasat") = 0
    '            End If

    '            If IsDBNull(dtDetails2.Rows(i)("additionAmount")) = False Then
    '                dRow("Additions") = dtDetails2.Rows(i)("additionAmount")
    '                dAdditions = dAdditions + Convert.ToDouble(dtDetails2.Rows(i)("additionAmount").ToString())
    '            Else
    '                dAdditions = 0
    '                dRow("Additions") = 0
    '            End If

    '            If IsDBNull(dtDetails2.Rows(i)("delAmount")) = False Then
    '                dRow("Deletions") = dtDetails2.Rows(i)("delAmount")
    '                dDeletions = dDeletions + Convert.ToDouble(dtDetails2.Rows(i)("delAmount").ToString())
    '            Else
    '                dDeletions = 0
    '                dRow("Deletions") = 0
    '            End If

    '            dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
    '            dTotalasat = dTotalasat + Convert.ToDouble(dRow("TotalAmount").ToString())

    '            If IsDBNull(dtDetails2.Rows(i)("DepAmountTill")) = False Then
    '                dRow("DepUptoPY") = dtDetails2.Rows(i)("DepAmountTill")
    '                dUptoDep = dUptoDep + Convert.ToDouble(dRow("DepUptoPY").ToString())
    '            End If

    '            Dim Transtype As Integer = 0
    '            Transtype = objDBL.SQLGetDescription(sNameSpace, "Select AFAA_TrType From Acc_FixedAssetAdditionDel Where AFAA_AssetType=" & dtDetails2.Rows(i)("AssetClass") & " and AFAA_CompID=" & iACID & " and AFAA_CustId=" & iCustid & "")
    '            If Transtype = 1 Then
    '                dRow("DepOnOpengBal") = dtDetails2.Rows(i)("DepAmount")
    '                dRow("DepOnAdditions") = 0
    '                dOnOpenBalDep = dOnOpenBalDep + Convert.ToDouble(dRow("DepOnOpengBal").ToString())
    '            Else
    '                dRow("DepOnAdditions") = dtDetails2.Rows(i)("DepAmount")
    '                dRow("DepOnOpengBal") = 0
    '                dOnAdditionsDep = dOnAdditionsDep + Convert.ToDouble(dRow("DepOnAdditions").ToString())
    '            End If

    '            dRow("DepOnDeletions") = 0

    '            dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))
    '            dTotalDep = dTotalDep + Convert.ToDouble(dRow("TotalDepFY").ToString())

    '            dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
    '            dTotalDepasonDep = dTotalDepasonDep + Convert.ToDouble(dRow("TotalDepasOn").ToString())

    '            dRow("WDVasOn") = Val(dRow("DepUptoPY")) - Val(dRow("TotalDepFY"))
    '            dNBWDVAson = dNBWDVAson + Convert.ToDouble(dRow("WDVasOn").ToString())

    '            dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
    '            dpNBWDVAson = dpNBWDVAson + Convert.ToDouble(dRow("WDVasOnPY").ToString())

    '            Dim dtYearDetails As DataTable
    '            dtYearDetails = LoadAssetDetails(sNameSpace, iPreviousYearID, iACID)

    '            If iPreviousYearID > 0 Then
    '                ihPreviousYearID = iPreviousYearID - 1
    '            End If
    '            ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & ihPreviousYearID & "")
    '            'FromDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_FROMDATE From Year_Master Where YMS_YEARID=" & iPreviousYearID & "")

    '            dRow("FromDate") = "Cost as at" & " " & dtYearDetails.Rows(0)("YMSFROMDATE")
    '            dRow("ToDate") = "Total as at" & " " & dtYearDetails.Rows(0)("YMSTODATE")
    '            dRow("PHUpto") = "Up to" & " " & ToDate
    '            dRow("HTotalDep") = "Total Dep." & " " & dtYearDetails.Rows(0)("YMSID")
    '            dRow("HTotalDepreciationason") = "Total Depreciation as on" & " " & dtYearDetails.Rows(0)("YMSTODATE")
    '            dRow("HWDVason") = "WDV as on" & " " & dtYearDetails.Rows(0)("YMSTODATE")
    '            dRow("PHWDVason") = "WDV as on" & " " & ToDate
    '            dt.Rows.Add(dRow)
    '        Next

    '        dRow = dt.NewRow()
    '        dRow("AssetClass") = "Sub Total"
    '        dRow("Costasat") = dOriginalCost
    '        dRow("Additions") = dAdditions
    '        dRow("Deletions") = dDeletions
    '        dRow("TotalAmount") = dTotalasat
    '        dRow("DepUptoPY") = dUptoDep
    '        dRow("DepOnOpengBal") = dOnOpenBalDep
    '        dRow("DepOnAdditions") = dOnAdditionsDep
    '        dRow("TotalDepFY") = dTotalDep
    '        dRow("TotalDepasOn") = dTotalDepasonDep
    '        dRow("WDVasOn") = dNBWDVAson
    '        dRow("WDVasOnPY") = dpNBWDVAson
    '        dt.Rows.Add(dRow)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadComnyAct1(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustid As Integer, ByVal iAsstcls As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        'Dim dAddAmt As Double = 0.0
        'Dim dDelAmt As Double = 0.0
        'Dim costasat As Double = 0.0
        Dim dtAmt As Double = 0.0
        Dim ToDate, FromDate As Date
        Dim iPreviousYearID, ihPreviousYearID As Integer
        Dim dOriginalCost As Double = 0.0
        Dim dAdditions As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotalasat As Double = 0.0
        Dim dUptoDep As Double = 0.0
        Dim dOnOpenBalDep As Double = 0.0
        Dim dOnAdditionsDep As Double = 0.0
        Dim dDeletionsDep As Double = 0.0
        Dim dTotalDep As Double = 0.0
        Dim dTotalDepasonDep As Double = 0.0
        Dim dNBWDVAson As Double = 0.0
        Dim dpNBWDVAson As Double = 0.0

        Dim Costasat As Double = 0.0
        Dim AddAmount As Double = 0.0
        Dim DelAmount As Double = 0.0
        Dim depOnOpengBal As Double = 0.0
        Dim depAdditions As Double = 0.0
        Dim depDeletions As Double = 0.0
        Dim TotalDep As Double = 0.0
        Dim DepUptoPY As Double = 0.0
        Dim WDVasOnPY As Double = 0.0
        Dim WDVasOn As Double = 0.0
        Dim TotalDepasOn As Double = 0.0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AssetClass")
            'dt.Columns.Add("Asset")
            dt.Columns.Add("Costasat")
            dt.Columns.Add("Additions")
            dt.Columns.Add("Deletions")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("DepUptoPY")
            dt.Columns.Add("DepOnOpengBal")
            dt.Columns.Add("DepOnAdditions")
            dt.Columns.Add("DepOnDeletions")
            dt.Columns.Add("TotalDepFY")
            dt.Columns.Add("TotalDepasOn")
            dt.Columns.Add("WDVasOn")
            dt.Columns.Add("WDVasOnPY")

            dt.Columns.Add("PHUpto")
            dt.Columns.Add("HTotalDep")
            dt.Columns.Add("HTotalDepreciationason")
            dt.Columns.Add("HWDVason")
            dt.Columns.Add("PHWDVason")
            dt.Columns.Add("Total")

            If iyearId > 0 Then
                iPreviousYearID = iyearId
            End If

            'sSql = "" : sSql = "select sum(a.AFAA_AssetAmount) as OriginalCost,a.AFAA_AssetType as AFAA_AssetType,sum(a.AFAA_DepreAmount) as DepAmountTill,sum(b.FAAD_AssetValue) as additionAmount,sum(c.AFAD_Amount) as delAmount,sum(d.ADep_DepreciationforFY ) as DepAmount from Acc_FixedAssetAdditionDel a"
            'sSql = sSql & " left join Acc_FixedAssetAdditionDetails b on b.FAAD_AssetType =a.AFAA_AssetType"
            'sSql = sSql & " left join Acc_FixedAssetDeletion c on c.AFAD_AssetClass = a.AFAA_AssetType"
            'sSql = sSql & " left join Acc_AssetDepreciation d on d.ADep_AssetID = a.AFAA_AssetType"
            'sSql = sSql & " left join Acc_FixedAssetAdditionDetails e on e.FAAD_AssetType = a.AFAA_AssetType"
            'sSql = sSql & " where AFAA_CustId=" & iCustid & " and AFAA_YearID=" & iPreviousYearID & " group by AFAA_AssetType"

            sSql = "  select sum(OriginalCost) as OriginalCost,sum(DepAmountTill) as DepAmountTill,sum(additionAmount) as additionAmount, "
            sSql = sSql & " sum(delAmount) as delAmount,sum(DepAmountOPB) as DepAmountOPB,sum(DepAmountAdd) as DepAmountAdd,AssetClass, sum(DelDeprec) as DelDeprec from ( "
            sSql = sSql & " select sum(AFAA_AssetAmount) as OriginalCost, sum(AFAA_DepreAmount) as DepAmountTill, 0 as additionAmount, 0 as delAmount, 0 as DepAmountOPB, 0 as DepAmountAdd,"
            sSql = sSql & " AFAA_AssetType as AssetClass,0 as DelDeprec from Acc_FixedAssetAdditionDel  where AFAA_CustId=" & iCustid & " and AFAA_YearID=" & iPreviousYearID & " and AFAA_CompID=" & iACID & ""
            If iAsstcls <> 0 Then
                sSql = sSql & " and AFAA_AssetType =" & iAsstcls & ""
            End If
            sSql = sSql & " group by AFAA_AssetType union all "
            sSql = sSql & " select '0' as OriginalCost, 0 as DepAmountTill,sum(FAAD_AssetValue) as additionAmount, 0 as delAmount, 0 as  DepAmountOPB, 0 as  DepAmountAdd,FAAD_AssetType as AssetClass, 0 as DelDeprec from Acc_FixedAssetAdditionDetails "
            sSql = sSql & " where FAAD_CustId=" & iCustid & " and FAAD_YearID=" & iPreviousYearID & " "
            If iAsstcls <> 0 Then
                sSql = sSql & " and FAAD_AssetType =" & iAsstcls & ""
            End If
            sSql = sSql & " group by FAAD_AssetType union all "
            sSql = sSql & " select 0 as OriginalCost, 0 as DepAmountTill,0 as additionAmoun, sum(AFAD_Amount) as delAmount,0 as DepAmountOPB, 0 as DepAmountAdd, AFAD_AssetClass as AssetClass,sum(AFAD_DelDeprec) as DelDeprec "
            sSql = sSql & " from Acc_FixedAssetDeletion where AFAD_CustomerName=" & iCustid & " and AFAD_YearID=" & iPreviousYearID & " and AFAD_CompID=" & iACID & ""
            If iAsstcls <> 0 Then
                sSql = sSql & " and AFAD_AssetClass =" & iAsstcls & ""
            End If
            sSql = sSql & "     group by AFAD_AssetClass union all"
            sSql = sSql & " select 0 as OriginalCost, 0 as DepAmountTill,0 as additionAmount, 0 as delAmount,sum(case when ADep_TransType =1 then ADep_DepreciationforFY else 0 end ) as DepAmountOPB,sum(case when ADep_TransType =2 then ADep_DepreciationforFY else 0 end ) as DepAmountAdd, ADep_AssetID as AssetClass,0 as DelDeprec from Acc_AssetDepreciation where ADep_CustId=" & iCustid & ""
            sSql = sSql & " and ADep_YearID=" & iPreviousYearID & " and ADep_CompID=" & iACID & " "
            If iAsstcls <> 0 Then
                sSql = sSql & " and ADep_AssetID =" & iAsstcls & ""
            End If
            sSql = sSql & "  group by ADep_AssetID) as temp group by AssetClass"


            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("ID") = 0
                If IsDBNull(dtDetails.Rows(i)("AssetClass")) = False Then
                    dRow("AssetClass") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dtDetails.Rows(i)("AssetClass") & " and AM_CompID=" & iACID & " and AM_CustId=" & iCustid & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("OriginalCost")) = False Then
                    dRow("Costasat") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("OriginalCost"))).ToString("#,##0")
                    Costasat = dtDetails.Rows(i)("OriginalCost")
                    dOriginalCost = dOriginalCost + Convert.ToDouble(dtDetails.Rows(i)("OriginalCost").ToString())
                Else
                    dOriginalCost = 0
                    dRow("Costasat") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("additionAmount")) = False Then
                    dRow("Additions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("additionAmount"))).ToString("#,##0")
                    AddAmount = dtDetails.Rows(i)("additionAmount")
                    dAdditions = dAdditions + Convert.ToDouble(dtDetails.Rows(i)("additionAmount").ToString())
                Else
                    dAdditions = 0
                    dRow("Additions") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("delAmount")) = False Then
                    dRow("Deletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("delAmount"))).ToString("#,##0")
                    DelAmount = dtDetails.Rows(i)("delAmount")
                    dDeletions = dDeletions + Convert.ToDouble(dtDetails.Rows(i)("delAmount").ToString())
                Else
                    dDeletions = 0
                    dRow("Deletions") = 0
                End If

                dRow("TotalAmount") = Costasat + AddAmount - DelAmount
                dTotalasat = dTotalasat + Convert.ToDouble(dRow("TotalAmount").ToString())

                If IsDBNull(dtDetails.Rows(i)("DepAmountTill")) = False Then
                    DepUptoPY = dtDetails.Rows(i)("DepAmountTill")
                    dRow("DepUptoPY") = Convert.ToDecimal(DepUptoPY).ToString("#,##0")
                    dUptoDep = dUptoDep + Convert.ToDouble(DepUptoPY.ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("DepAmountOPB")) = False Then
                    dRow("DepOnOpengBal") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountOPB"))).ToString("#,##0")
                    depOnOpengBal = dtDetails.Rows(i)("DepAmountOPB")
                    dOnOpenBalDep = dOnOpenBalDep + Convert.ToDouble(depOnOpengBal.ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("DepAmountAdd")) = False Then
                    dRow("DepOnAdditions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DepAmountAdd"))).ToString("#,##0")
                    depAdditions = dtDetails.Rows(i)("DepAmountAdd")
                    dOnAdditionsDep = dOnAdditionsDep + Convert.ToDouble(depAdditions.ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("DelDeprec")) = False Then
                    dRow("DepOnDeletions") = Convert.ToDecimal(Math.Round(dtDetails.Rows(i)("DelDeprec"))).ToString("#,##0")
                    depDeletions = dtDetails.Rows(i)("DelDeprec")
                    dDeletionsDep = dDeletionsDep + Convert.ToDouble(depDeletions.ToString())
                End If

                TotalDep = depOnOpengBal + depAdditions + depDeletions
                dRow("TotalDepFY") = Convert.ToDecimal(TotalDep).ToString("#,##0")
                dTotalDep = dTotalDep + TotalDep

                TotalDepasOn = Val(DepUptoPY) + Val(TotalDep)
                dRow("TotalDepasOn") = Convert.ToDecimal(TotalDepasOn).ToString("#,##0")
                dTotalDepasonDep = dTotalDepasonDep + TotalDepasOn

                WDVasOn = dRow("TotalAmount") - Val(TotalDepasOn)
                dRow("WDVasOn") = Convert.ToDecimal(Math.Round(WDVasOn)).ToString("#,##0")
                dNBWDVAson = dNBWDVAson + WDVasOn

                WDVasOnPY = Val(Costasat) - Val(DepUptoPY)
                dRow("WDVasOnPY") = Convert.ToDecimal(Math.Round(WDVasOnPY)).ToString("#,##0")
                dpNBWDVAson = dpNBWDVAson + WDVasOnPY

                Dim dtYearDetails As DataTable
                dtYearDetails = LoadAssetDetails(sNameSpace, iPreviousYearID, iACID)

                If iPreviousYearID > 0 Then
                    ihPreviousYearID = iPreviousYearID - 1
                End If
                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & ihPreviousYearID & "")
                'FromDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_FROMDATE From Year_Master Where YMS_YEARID=" & iPreviousYearID & "")

                dRow("FromDate") = "Cost as at" & " " & dtYearDetails.Rows(0)("YMSFROMDATE")
                dRow("ToDate") = "Total as at" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("PHUpto") = "Up to" & " " & ToDate
                dRow("HTotalDep") = "Total Dep." & " " & dtYearDetails.Rows(0)("YMSID")
                dRow("HTotalDepreciationason") = "Total Depreciation as on" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("HWDVason") = "WDV as on" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("PHWDVason") = "WDV as on" & " " & ToDate
                dt.Rows.Add(dRow)
            Next

            dRow = dt.NewRow()
            dRow("AssetClass") = "<b>" & "Sub Total" & "</b>"
            dRow("Costasat") = "<b>" & Convert.ToDecimal(Math.Round(dOriginalCost)).ToString("#,##0") & "</b>"
            dRow("Additions") = "<b>" & Convert.ToDecimal(Math.Round(dAdditions)).ToString("#,##0") & "</b>"
            dRow("Deletions") = "<b>" & Convert.ToDecimal(Math.Round(dDeletions)).ToString("#,##0") & "</b>"
            dRow("TotalAmount") = "<b>" & Convert.ToDecimal(Math.Round(dTotalasat)).ToString("#,##0") & "</b>"
            dRow("DepUptoPY") = "<b>" & Convert.ToDecimal(Math.Round(dUptoDep)).ToString("#,##0") & "</b>"
            dRow("DepOnOpengBal") = "<b>" & Convert.ToDecimal(Math.Round(dOnOpenBalDep)).ToString("#,##0") & "</b>"
            dRow("DepOnAdditions") = "<b>" & Convert.ToDecimal(Math.Round(dOnAdditionsDep)).ToString("#,##0") & "</b>"
            dRow("DepOnDeletions") = "<b>" & Convert.ToDecimal(Math.Round(dDeletionsDep)).ToString("#,##0") & "</b>"
            dRow("TotalDepFY") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDep)).ToString("#,##0") & "</b>"
            dRow("TotalDepasOn") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDepasonDep)).ToString("#,##0") & "</b>"
            dRow("WDVasOn") = "<b>" & Convert.ToDecimal(Math.Round(dNBWDVAson)).ToString("#,##0") & "</b>"
            dRow("WDVasOnPY") = "<b>" & Convert.ToDecimal(Math.Round(dpNBWDVAson)).ToString("#,##0") & "</b>"
            dt.Rows.Add(dRow)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDynComnyDetailedAct(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustid As Integer,
        ByVal iLocationId As String, ByVal iDivId As String, ByVal iDeptId As String, ByVal iBayId As String, ByVal iAsstCls As Integer, ByVal iTransType As Integer, ByVal iInAmt As Integer, ByVal iRoundOff As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        'Dim dAddAmt As Double = 0.0
        'Dim dDelAmt As Double = 0.0
        'Dim costasat As Double = 0.0
        Dim dtAmt As Double = 0.0
        Dim ToDate, FromDate As Date
        Dim iPreviousYearID, ihPreviousYearID As Integer
        Dim dOriginalCost As Double = 0.0
        Dim dAdditions As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotalasat As Double = 0.0
        Dim dUptoDep As Double = 0.0
        Dim dOnOpenBalDep As Double = 0.0
        Dim dOnAdditionsDep As Double = 0.0
        Dim dDeletionsDep As Double = 0.0
        Dim dTotalDep As Double = 0.0
        Dim dTotalDepasonDep As Double = 0.0
        Dim dNBWDVAson As Double = 0.0
        Dim dpNBWDVAson As Double = 0.0
        Dim dtAsstDetails As New DataTable
        Dim dtDepDet As New DataTable
        Dim dtAssetItem As New DataTable
        Dim iOrgCost, iAddAmt, iDelAmt, iDepAmt, iDelDepAmt, iDelReason As Double

        Dim dOriginalCostTot As Double = 0.0
        Dim dAdditionsTot As Double = 0.0
        Dim dDeletionsTot As Double = 0.0
        Dim dTotalasatTot As Double = 0.0
        Dim dUptoDepTot As Double = 0.0
        Dim dOnOpenBalDepTot As Double = 0.0
        Dim dOnAdditionsDepTot As Double = 0.0
        Dim dDeletionsDepTot As Double = 0.0
        Dim dTotalDepTot As Double = 0.0
        Dim dTotalDepasonDepTot As Double = 0.0
        Dim dNBWDVAsonTot As Double = 0.0
        Dim dpNBWDVAsonTot As Double = 0.0
        Dim iCount As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("Asset")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("Location")
            dt.Columns.Add("Division")
            dt.Columns.Add("Department")
            dt.Columns.Add("Bay")
            dt.Columns.Add("Costasat")
            dt.Columns.Add("Additions")
            dt.Columns.Add("Deletions")
            dt.Columns.Add("DelReason")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("DepUptoPY")
            dt.Columns.Add("DepOnOpengBal")
            dt.Columns.Add("DepOnAdditions")
            dt.Columns.Add("DepOnDeletions")
            dt.Columns.Add("TotalDepFY")
            dt.Columns.Add("TotalDepasOn")
            dt.Columns.Add("WDVasOn")
            dt.Columns.Add("WDVasOnPY")
            dt.Columns.Add("PHUpto")
            dt.Columns.Add("HTotalDep")
            dt.Columns.Add("HTotalDepreciationason")
            dt.Columns.Add("HWDVason")
            dt.Columns.Add("PHWDVason")
            dt.Columns.Add("Total")
            If iyearId > 0 Then
                iPreviousYearID = iyearId
            End If
            sSql = sSql & "  Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode = 2 And AM_CustId = " & iCustid & " and AM_COmpID=" & iACID & ""
            If iAsstCls <> 0 Then
                sSql = sSql & " and AM_ID =" & iAsstCls & ""
            End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)


            For i = 0 To dtDetails.Rows.Count - 1
                dtAsstDetails = LoadAssetId(sNameSpace, dtDetails.Rows(i)("AM_ID"), iPreviousYearID, iLocationId, iDivId, iDeptId, iBayId)
                If dtAsstDetails.Rows.Count > 0 Then
                    For j = 0 To dtAsstDetails.Rows.Count - 1
                        dtAssetItem = LoadAssetItem(sNameSpace, dtAsstDetails.Rows(j)("AFAM_ID"), iPreviousYearID, iTransType)
                        If dtAssetItem.Rows.Count > 0 Then
                            iCount = 1
                            dRow = dt.NewRow()
                            dRow("ID") = 0
                            dRow("AssetClass") = dtDetails.Rows(i)("AM_Description")
                            dRow("Asset") = dtAsstDetails.Rows(j)("AFAM_ItemDescription")
                            dRow("AssetCode") = dtAsstDetails.Rows(j)("AFAM_AssetCode")

                            iOrgCost = dtAssetItem.Rows(0)("OrgCost")

                            'Dk 14-03-23 For original cost prev year added
                            Dim dAmount As Double = 0.00
                            If iOrgCost = 0 Then
                                dAmount = objDBL.SQLGetDescription(sNameSpace, "select isnull(sum(ADep_WrittenDownValue),0) from Acc_AssetDepreciation where ADep_CompID=" & iACID & " and  ADep_YearID=" & iPreviousYearID & " and ADep_CustId=" & iCustid & " and ADep_Item =" & dtAsstDetails.Rows(j)("AFAM_ID") & "")
                                If dAmount <> 0 Then
                                    iOrgCost = dAmount
                                End If
                            End If



                            'dRow("Costasat") = Val(iOrgCost)

                            If (iInAmt > 0) Then
                                If (iRoundOff = 0) Then
                                    dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N0")
                                ElseIf (iRoundOff = 1) Then
                                    dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N1")
                                ElseIf (iRoundOff = 2) Then
                                    dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N2")
                                ElseIf (iRoundOff = 3) Then
                                    dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N3")
                                ElseIf (iRoundOff = 4) Then
                                    dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N4")
                                End If
                            Else
                                dRow("Costasat") = Val(iOrgCost)
                            End If

                            dOriginalCost = dOriginalCost + Val(iOrgCost)

                            iAddAmt = dtAssetItem.Rows(0)("AddAmt")


                            If Val(iAddAmt) > 0 Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N0") 'Val(iAddAmt)
                                        dAdditions = dAdditions + Val(iAddAmt)
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N1") 'Val(iAddAmt)
                                        dAdditions = dAdditions + Val(iAddAmt)
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N2") 'Val(iAddAmt)
                                        dAdditions = dAdditions + Val(iAddAmt)
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N3") 'Val(iAddAmt)
                                        dAdditions = dAdditions + Val(iAddAmt)
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N4") 'Val(iAddAmt)
                                        dAdditions = dAdditions + Val(iAddAmt)
                                    End If
                                Else
                                    dRow("Additions") = iAddAmt 'Val(iAddAmt)
                                    dAdditions = dAdditions + Val(iAddAmt)
                                End If

                            Else
                                dRow("Additions") = 0
                            End If

                            iDelAmt = dtAssetItem.Rows(0)("DelAmt")
                            If (iDelAmt > 0) Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N0") 'Val(iDelAmt)
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N1") 'Val(iDelAmt)
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N2") 'Val(iDelAmt)
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N3") 'Val(iDelAmt)
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N4") 'Val(iDelAmt)
                                    End If
                                Else
                                    dRow("Deletions") = Val(iDelAmt)
                                End If
                            Else
                                dRow("Deletions") = Val(iDelAmt)
                            End If
                            'dRow("Deletions") = Val(iDelAmt)
                            dDeletions = dDeletions + Val(iDelAmt)

                            iDelReason = dtAssetItem.Rows(0)("AFAD_AssetDeletion")
                            If iDelReason = 0 Then
                                dRow("DelReason") = ""
                            ElseIf iDelReason = 1 Then
                                dRow("DelReason") = "Sold"
                            ElseIf iDelReason = 2 Then
                                dRow("DelReason") = "Transfer"
                            ElseIf iDelReason = 3 Then
                                dRow("DelReason") = "Stolen"
                            ElseIf iDelReason = 4 Then
                                dRow("DelReason") = "Destroyed"
                            ElseIf iDelReason = 5 Then
                                dRow("DelReason") = "Obsolete"
                            End If


                            'dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                            If (iInAmt > 0) Then
                                If (Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N0")
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N1")
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N2")
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N3")
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N4")
                                    End If
                                Else
                                    dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                                End If
                            Else
                                dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                            End If

                            dTotalasat = dTotalasat + Convert.ToDouble(dRow("TotalAmount").ToString())
                            iDepAmt = dtAssetItem.Rows(0)("AFAA_DepreAmount")

                            If (iDepAmt > 0) Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("DepUptoPY") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N0") 'iDepAmt
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("DepUptoPY") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N1") 'iDepAmt
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("DepUptoPY") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N2") 'iDepAmt
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("DepUptoPY") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N3") 'iDepAmt
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("DepUptoPY") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N4") 'iDepAmt
                                    End If
                                Else
                                    dRow("DepUptoPY") = iDepAmt
                                End If
                            Else
                                dRow("DepUptoPY") = iDepAmt
                            End If

                            'dRow("DepUptoPY") = iDepAmt
                            dUptoDep = dUptoDep + iDepAmt

                            If dtAssetItem.Rows(0)("ADep_TransType") = 1 Then
                                Dim opbAmount As Double = 0.00
                                Dim deletionAmount As Double = 0.00
                                Dim reuslt As Double = 0.00
                                opbAmount = dtAssetItem.Rows(0)("ADep_DepreciationforFY")
                                deletionAmount = dtAssetItem.Rows(0)("AFAD_DelDeprec")
                                reuslt = opbAmount - deletionAmount

                                'dRow("DepOnOpengBal") = Convert.ToDouble(reuslt).ToString()

                                If (reuslt > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(reuslt / iInAmt).ToString("N0") 'Convert.ToDouble(reuslt).ToString()
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(reuslt / iInAmt).ToString("N1") 'Convert.ToDouble(reuslt).ToString()
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(reuslt / iInAmt).ToString("N2") 'Convert.ToDouble(reuslt).ToString()
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(reuslt / iInAmt).ToString("N3") 'Convert.ToDouble(reuslt).ToString()
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(reuslt / iInAmt).ToString("N4") 'Convert.ToDouble(reuslt).ToString()
                                        End If
                                    Else
                                        dRow("DepOnOpengBal") = Convert.ToDouble(reuslt).ToString()
                                    End If
                                Else
                                    dRow("DepOnOpengBal") = Convert.ToDouble(reuslt).ToString()
                                End If

                                dRow("DepOnAdditions") = 0
                                dOnOpenBalDep = reuslt
                                dOnOpenBalDep = dOnOpenBalDep + Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                            Else
                                ' dRow("DepOnAdditions") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                If (Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString()) > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("DepOnAdditions") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N0") 'Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("DepOnAdditions") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N1") 'Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("DepOnAdditions") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N2") 'Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("DepOnAdditions") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N3") 'Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("DepOnAdditions") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N4") 'Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                        End If

                                    Else
                                        dRow("DepOnAdditions") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                    End If
                                Else
                                    dRow("DepOnAdditions") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                                End If
                                dRow("DepOnOpengBal") = 0
                                dOnAdditionsDep = dOnAdditionsDep + Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                            End If

                            iDelDepAmt = dtAssetItem.Rows(0)("AFAD_DelDeprec")
                            ' dRow("DepOnDeletions") = iDelDepAmt

                            If (iDelDepAmt > 0) Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("DepOnDeletions") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N0")  'iDelDepAmt
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("DepOnDeletions") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N1")  'iDelDepAmt
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("DepOnDeletions") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N2")  'iDelDepAmt
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("DepOnDeletions") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N3")  'iDelDepAmt
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("DepOnDeletions") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N4")  'iDelDepAmt
                                    End If
                                Else
                                    dRow("DepOnDeletions") = iDelDepAmt
                                End If
                            Else
                                dRow("DepOnDeletions") = iDelDepAmt
                            End If

                            'dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))

                            If (Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) > 0)) Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("TotalDepFY") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N0")
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("TotalDepFY") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N1")
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("TotalDepFY") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N2")
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("TotalDepFY") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N3")
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("TotalDepFY") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N4")
                                    End If

                                Else
                                    dRow("TotalDepFY") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")))
                                End If
                            Else
                                dRow("TotalDepFY") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")))
                            End If

                            dTotalDep = dTotalDep + Convert.ToDouble(dRow("TotalDepFY").ToString())


                            'dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))

                            If (Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) > 0) Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N0")  'Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N1")  'Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N2")  'Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N3")  'Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N4")  'Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                    End If

                                Else
                                    dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                End If
                            Else
                                dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                            End If

                            dTotalDepasonDep = dTotalDepasonDep + Convert.ToDouble(dRow("TotalDepasOn").ToString())
                            'dRow("WDVasOn") = Val(dRow("TotalAmount")) - dRow("TotalDepasOn")

                            If (Val(dRow("TotalAmount")) - dRow("TotalDepasOn") > 0) Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("WDVasOn") = Convert.ToDecimal(Val(dRow("TotalAmount")) - dRow("TotalDepasOn") / iInAmt).ToString("N0") 'Val(dRow("TotalAmount")) - dRow("TotalDepasOn")
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("WDVasOn") = Convert.ToDecimal(Val(dRow("TotalAmount")) - dRow("TotalDepasOn") / iInAmt).ToString("N1") 'Val(dRow("TotalAmount")) - dRow("TotalDepasOn")
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("WDVasOn") = Convert.ToDecimal(Val(dRow("TotalAmount")) - dRow("TotalDepasOn") / iInAmt).ToString("N2") 'Val(dRow("TotalAmount")) - dRow("TotalDepasOn")
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("WDVasOn") = Convert.ToDecimal(Val(dRow("TotalAmount")) - dRow("TotalDepasOn") / iInAmt).ToString("N3") 'Val(dRow("TotalAmount")) - dRow("TotalDepasOn")
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("WDVasOn") = Convert.ToDecimal(Val(dRow("TotalAmount")) - dRow("TotalDepasOn") / iInAmt).ToString("N4") 'Val(dRow("TotalAmount")) - dRow("TotalDepasOn")
                                    End If

                                Else
                                    dRow("WDVasOn") = Val(dRow("TotalAmount")) - dRow("TotalDepasOn")
                                End If
                            Else
                                dRow("WDVasOn") = Val(dRow("TotalAmount")) - dRow("TotalDepasOn")
                            End If

                            dNBWDVAson = dNBWDVAson + Convert.ToDouble(dRow("WDVasOn").ToString())
                            'dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))

                            If (Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) > 0) Then
                                If (iInAmt > 0) Then
                                    If (iRoundOff = 0) Then
                                        dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N0")  'Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                    ElseIf (iRoundOff = 1) Then
                                        dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N1")  'Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                    ElseIf (iRoundOff = 2) Then
                                        dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N2")  'Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                    ElseIf (iRoundOff = 3) Then
                                        dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N3")  'Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                    ElseIf (iRoundOff = 4) Then
                                        dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N4")  'Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                    End If

                                Else
                                    dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                End If
                            Else
                                dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                            End If

                            dpNBWDVAson = dpNBWDVAson + Convert.ToDouble(dRow("WDVasOnPY").ToString())
                            Dim dtYearDetails As DataTable
                            dtYearDetails = LoadAssetDetails(sNameSpace, iPreviousYearID, iACID)
                            If iPreviousYearID > 0 Then
                                ihPreviousYearID = iPreviousYearID - 1
                            End If
                            ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & ihPreviousYearID & "")
                            dRow("FromDate") = "Cost As at" & " " & dtYearDetails.Rows(0)("YMSFROMDATE")
                            dRow("ToDate") = "Total As at" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                            dRow("PHUpto") = "Up To" & " " & ToDate
                            dRow("HTotalDep") = "Total Dep." & " " & dtYearDetails.Rows(0)("YMSID")
                            dRow("HTotalDepreciationason") = "Total Depreciation As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                            dRow("HWDVason") = "WDV As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                            dRow("PHWDVason") = "WDV As On" & " " & ToDate
                            dRow("Location") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Location") & " And LS_CustId=" & iCustid & "")
                            dRow("Division") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Division") & " And LS_CustId=" & iCustid & "")
                            dRow("Department") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Department") & " And LS_CustId=" & iCustid & "")
                            dRow("Bay") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Bay") & " And LS_CustId=" & iCustid & "")
                            dt.Rows.Add(dRow)
                        End If
                    Next
                    If iCount > 0 Then
                        dRow = dt.NewRow()
                        dRow("AssetClass") = "<b>" & "Total" & "</b>"
                        dRow("Costasat") = "<b>" & Convert.ToDecimal(Math.Round(dOriginalCost)).ToString("#,##0") & "</b>"
                        dOriginalCostTot = dOriginalCostTot + dOriginalCost
                        dRow("Additions") = "<b>" & Convert.ToDecimal(Math.Round(dAdditions)).ToString("#,##0") & "</b>"
                        dAdditionsTot = dAdditionsTot + dAdditions
                        dRow("Deletions") = "<b>" & Convert.ToDecimal(Math.Round(dDeletions)).ToString("#,##0") & "</b>"
                        dDeletionsTot = dDeletionsTot + dDeletions
                        dRow("TotalAmount") = "<b>" & Convert.ToDecimal(Math.Round(dTotalasat)).ToString("#,##0") & "</b>"
                        dTotalasatTot = dTotalasatTot + dTotalasat
                        dRow("DepUptoPY") = "<b>" & Convert.ToDecimal(Math.Round(dUptoDep)).ToString("#,##0") & "</b>"
                        dUptoDepTot = dUptoDepTot + dUptoDep
                        dRow("DepOnOpengBal") = "<b>" & Convert.ToDecimal(Math.Round(dOnOpenBalDep)).ToString("#,##0") & "</b>"
                        dOnOpenBalDepTot = dOnOpenBalDepTot + dOnOpenBalDep
                        dRow("DepOnAdditions") = "<b>" & Convert.ToDecimal(Math.Round(dOnAdditionsDep)).ToString("#,##0") & "</b>"
                        dOnAdditionsDepTot = dOnAdditionsDepTot + dOnAdditionsDep
                        dRow("TotalDepFY") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDep)).ToString("#,##0") & "</b>"
                        dTotalDepTot = dTotalDepTot + dTotalDep
                        dRow("TotalDepasOn") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDepasonDep)).ToString("#,##0") & "</b>"
                        dTotalDepasonDepTot = dTotalDepasonDepTot + dTotalDepasonDep
                        dRow("WDVasOn") = "<b>" & Convert.ToDecimal(Math.Round(dNBWDVAson)).ToString("#,##0") & "</b>"
                        dNBWDVAsonTot = dNBWDVAsonTot + dNBWDVAson
                        dRow("WDVasOnPY") = "<b>" & Convert.ToDecimal(Math.Round(dpNBWDVAson)).ToString("#,##0") & "</b>"
                        dpNBWDVAsonTot = dpNBWDVAsonTot + dpNBWDVAson
                        dt.Rows.Add(dRow)
                        dOriginalCost = 0 : dAdditions = 0 : dDeletions = 0 : dTotalasat = 0 : dUptoDep = 0 : dOnOpenBalDep = 0 : dOnAdditionsDep = 0
                        dTotalDep = 0 : dTotalDepasonDep = 0 : dNBWDVAson = 0 : dpNBWDVAson = 0
                        iCount = 0
                    End If
                End If
            Next
            dRow = dt.NewRow()
            dRow("AssetClass") = "<b>" & "Grand Total" & "</b>"
            dRow("Costasat") = "<b>" & Convert.ToDecimal(Math.Round(dOriginalCostTot)).ToString("#,##0") & "</b>"
            dRow("Additions") = "<b>" & Convert.ToDecimal(Math.Round(dAdditionsTot)).ToString("#,##0") & "</b>"
            dRow("Deletions") = "<b>" & Convert.ToDecimal(Math.Round(dDeletionsTot)).ToString("#,##0") & "</b>"
            dRow("TotalAmount") = "<b>" & Convert.ToDecimal(Math.Round(dTotalasatTot)).ToString("#,##0") & "</b>"
            dRow("DepUptoPY") = "<b>" & Convert.ToDecimal(Math.Round(dUptoDepTot)).ToString("#,##0") & "</b>"
            dRow("DepOnOpengBal") = "<b>" & Convert.ToDecimal(Math.Round(dOnOpenBalDepTot)).ToString("#,##0") & "</b>"
            dRow("DepOnAdditions") = "<b>" & Convert.ToDecimal(Math.Round(dOnAdditionsDepTot)).ToString("#,##0") & "</b>"
            dRow("TotalDepFY") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDepTot)).ToString("#,##0") & "</b>"
            dRow("TotalDepasOn") = "<b>" & Convert.ToDecimal(Math.Round(dTotalDepasonDepTot)).ToString("#,##0") & "</b>"
            dRow("WDVasOn") = "<b>" & Convert.ToDecimal(Math.Round(dNBWDVAsonTot)).ToString("#,##0") & "</b>"
            dRow("WDVasOnPY") = "<b>" & Convert.ToDecimal(Math.Round(dpNBWDVAsonTot)).ToString("#,##0") & "</b>"
            dRow("Location") = ""
            dRow("Division") = ""
            dRow("Department") = ""
            dRow("Bay") = ""
            dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetId(ByVal sNameSpace As String, ByVal iAssetId As Integer, ByVal iYearId As Integer, ByVal iLocationId As String, ByVal iDivId As String, ByVal iDeptId As String, ByVal iBayId As String) As DataTable
        Dim sSql As String = ""
        Dim dtDetails As New DataTable
        Try
            sSql = sSql & "   Select AFAM_ID,AFAM_AssetCode,AFAM_AssetType,AFAM_ItemDescription from Acc_FixedAssetMaster  where AFAM_AssetType= '" & iAssetId & "' and AFAM_YearID= " & iYearId & " and AFAM_CompID=1"
            If iLocationId <> 0 Then
                sSql = sSql & " and AFAM_Location in (" & iLocationId & ")"
            End If
            If iDivId <> "" Then
                sSql = sSql & " and AFAM_Division in (" & iDivId & ")"
            End If
            If iDeptId <> "" Then
                sSql = sSql & " and AFAM_Department in (" & iDeptId & ")"
            End If
            If iBayId <> "" Then
                sSql = sSql & " and AFAM_Bay in (" & iBayId & ")"
            End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dtDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDepDet(ByVal sNameSpace As String, ByVal iAssetId As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtDetails As New DataTable
        Try
            sSql = sSql & "  Select ADep_TransType,ADep_DepreciationforFY From Acc_AssetDepreciation Where ADep_Item= " & iAssetId & " and   ADep_CustId=" & iCustId & " and   ADep_YearId=" & iYearId & " "
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dtDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetItem(ByVal sNameSpace As String, ByVal iAssetId As Integer, ByVal iYearId As Integer, ByVal iTransType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtDetails As New DataTable
        Try
            sSql = sSql & "     Select  isnull(sum(AFAA_AssetAmount), 0)As OrgCost,isnull(sum( FAAD_AssetValue),0)As AddAmt,isnull(sum( AFAD_Amount),0)As DelAmt,"
            sSql = sSql & "    isnull(sum(AFAA_DepreAmount), 0)As AFAA_DepreAmount,isnull(sum(ADep_DepreciationforFY),0)As ADep_DepreciationforFY,"
            sSql = sSql & "   isnull(sum(AFAD_DelDeprec), 0)As AFAD_DelDeprec,isnull(sum(ADep_TransType),0) as ADep_TransType,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay, isnull(AFAD_AssetDeletion,0) as AFAD_AssetDeletion "
            sSql = sSql & "   From Acc_FixedAssetAdditionDel  "
            sSql = sSql & "   Left Join Acc_FixedAssetAdditionDetails on FAAD_ItemType=AFAA_ItemType "
            sSql = sSql & "    Left Join Acc_FixedAssetDeletion on AFAD_Asset=AFAA_ItemType "
            sSql = sSql & "   Left Join Acc_AssetDepreciation on ADep_Item=AFAA_ItemType And ADep_YearId=" & iYearId & ""
            sSql = sSql & "   Left Join Acc_AssetLocationSetup on LS_ID=AFAA_ItemType  "
            sSql = sSql & "  Where AFAA_ItemType ='" & iAssetId & "' And AFAA_YearID=" & iYearId & ""
            If iTransType = 1 Then
                sSql = sSql & " And AFAA_TrType =1"
            End If
            If iTransType = 2 Then
                sSql = sSql & " And AFAA_TrType =2"
            End If
            If iTransType = 3 Then
                sSql = sSql & " And   AFAD_Asset In (" & iAssetId & ") "
            End If
            sSql = sSql & "  group by AFAA_AssetAmount, ADep_TransType, ADep_DepreciationforFY, AFAD_DelDeprec, AFAA_Location, AFAA_Division, AFAA_Department, AFAA_Bay,AFAD_AssetDeletionType,AFAD_AssetDeletion "
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dtDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    '' Inv Report
    Public Function LoadInvDetailed(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustid As Integer,
      ByVal iLocationId As Integer, ByVal iDivId As Integer, ByVal iDeptId As Integer, ByVal iBayId As Integer, ByVal sAsstCls As String, ByVal iAstItem As Integer, ByVal iTransType As Integer, ByVal iInAmt As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim dtAmt As Double = 0.0
        Dim ToDate, FromDate As Date
        Dim iPreviousYearID, ihPreviousYearID As Integer
        Dim dOriginalCost As Double = 0.0
        Dim dAdditions As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotalasat As Double = 0.0
        Dim dUptoDep As Double = 0.0
        Dim dOnOpenBalDep As Double = 0.0
        Dim dOnAdditionsDep As Double = 0.0
        Dim dDeletionsDep As Double = 0.0
        Dim dTotalDep As Double = 0.0
        Dim dTotalDepasonDep As Double = 0.0
        Dim dNBWDVAson As Double = 0.0
        Dim dpNBWDVAson As Double = 0.0
        Dim dtAssetItem As New DataTable
        Dim iOrgCost, iAddAmt, iDelAmt, iDepAmt, iDelDepAmt, iDelReason As Double
        Dim dOriginalCostTot As Double = 0.0
        Dim dAdditionsTot As Double = 0.0
        Dim dDeletionsTot As Double = 0.0
        Dim dTotalasatTot As Double = 0.0
        Dim dUptoDepTot As Double = 0.0
        Dim dOnOpenBalDepTot As Double = 0.0
        Dim dOnAdditionsDepTot As Double = 0.0
        Dim dDeletionsDepTot As Double = 0.0
        Dim dTotalDepTot As Double = 0.0
        Dim dTotalDepasonDepTot As Double = 0.0
        Dim dNBWDVAsonTot As Double = 0.0
        Dim dpNBWDVAsonTot As Double = 0.0
        Dim iCount As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("Asset")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("Location")
            dt.Columns.Add("Division")
            dt.Columns.Add("Department")
            dt.Columns.Add("Bay")
            dt.Columns.Add("Costasat")
            dt.Columns.Add("Additions")
            dt.Columns.Add("Deletions")
            dt.Columns.Add("DelReason")
            dt.Columns.Add("DelNo")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("DepUptoPY")
            dt.Columns.Add("DepOnOpengBal")
            dt.Columns.Add("DepOnAdditions")
            dt.Columns.Add("DepOnDeletions")
            dt.Columns.Add("DeletionDate")
            dt.Columns.Add("TotalDepFY")
            dt.Columns.Add("TotalDepasOn")
            dt.Columns.Add("WDVasOn")
            dt.Columns.Add("WDVasOnPY")
            dt.Columns.Add("PHUpto")
            dt.Columns.Add("HTotalDep")
            dt.Columns.Add("HTotalDepreciationason")
            dt.Columns.Add("HWDVason")
            dt.Columns.Add("PHWDVason")
            dt.Columns.Add("Total")
            If iyearId > 0 Then
                iPreviousYearID = iyearId
            End If
            sSql = sSql & "  Select  AFAM_ID,AFAM_AssetType,AFAM_ItemDescription,AFAM_AssetCode,AFAD_TransNo,isnull(sum(AFAA_AssetAmount), 0)As OrgCost,isnull(sum( AFAD_Amount),0)As DelAmt,"
            sSql = sSql & "  isnull(sum(AFAA_DepreAmount), 0)As AFAA_DepreAmount,isnull(sum(ADep_DepreciationforFY),0)As ADep_DepreciationforFY,"
            sSql = sSql & "  isnull(sum(AFAD_DelDeprec), 0)As AFAD_DelDeprec,isnull(sum(ADep_TransType),0) As ADep_TransType,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay,isnull(sum(AFAD_AssetDeletionType),0) As AFAD_AssetDeletionType, "
            sSql = sSql & "  AFAD_TransNo,AFAD_DeletionDate"
            sSql = sSql & "  From Acc_FixedAssetMaster "
            sSql = sSql & "  Left Join Acc_FixedAssetAdditionDel On  AFAA_ItemType=  AFAM_ID "
            'sSql = sSql & "   Left Join Acc_FixedAssetAdditionDetails On FAAD_ItemType=AFAM_ID "
            sSql = sSql & "  Left Join Acc_FixedAssetDeletion On AFAD_Asset=AFAM_ID "
            sSql = sSql & "  Left Join Acc_AssetDepreciation On ADep_Item=AFAM_ID And ADep_YearId=" & iyearId & " "
            sSql = sSql & "  Left Join Acc_AssetLocationSetup on LS_ID=AFAM_ID "
            sSql = sSql & "  Where AFAM_ID ='" & iAstItem & "' And AFAM_YearId=" & iyearId & "  "
            If iTransType = 1 Then
                sSql = sSql & " and AFAA_TrType =1"
            End If
            If iTransType = 2 Then
                sSql = sSql & " and AFAA_TrType =2"
            End If
            If iTransType = 3 Then
                sSql = sSql & " and   AFAD_Asset in (" & iAstItem & ") "
            End If
            sSql = sSql & "  group by AFAM_ID,AFAM_AssetType,AFAM_ItemDescription,AFAM_AssetCode,AFAD_TransNo,AFAA_AssetAmount, ADep_TransType, ADep_DepreciationforFY, AFAD_DelDeprec, AFAA_Location, AFAA_Division, AFAA_Department, AFAA_Bay,AFAD_AssetDeletionType,AFAD_TransNo,AFAD_DeletionDate "
            dtAssetItem = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtAssetItem.Rows.Count > 0 Then
                iCount = 1
                dRow = dt.NewRow()
                dRow("ID") = 0
                dRow("AssetClass") = sAsstCls
                dRow("Asset") = dtAssetItem.Rows(0)("AFAM_ItemDescription")
                dRow("AssetCode") = dtAssetItem.Rows(0)("AFAM_AssetCode")
                iOrgCost = dtAssetItem.Rows(0)("OrgCost")
                'dRow("Costasat") = Val(iOrgCost)

                If (iOrgCost > 0) Then
                    If (iInAmt > 0) Then
                        dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N1")
                    Else
                        dRow("Costasat") = Val(iOrgCost)
                    End If
                Else
                    dRow("Costasat") = Val(iOrgCost)
                End If

                dOriginalCost = dOriginalCost + Val(iOrgCost)

                iAddAmt = objDBL.SQLGetDescription(sNameSpace, "     select isnull(sum(FAAD_AssetValue),0) as FAAD_AssetValue  from  Acc_FixedAssetAdditionDetails where FAAD_ItemType=" & iAstItem & " and faad_CompID=" & iACID & " and faad_CustId=" & iCustid & "")
                If Val(iAddAmt) > 0 Then
                    If (iInAmt > 0) Then
                        dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N1")
                    Else
                        dRow("Additions") = Val(iAddAmt)
                    End If
                    dAdditions = dAdditions + Val(iAddAmt)
                Else
                    dRow("Additions") = 0
                End If

                iDelAmt = dtAssetItem.Rows(0)("DelAmt")
                'dRow("Deletions") = Val(iDelAmt)

                If (iDelAmt > 0) Then
                    If (iInAmt > 0) Then
                        dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N1")
                    Else
                        dRow("Deletions") = Val(iDelAmt)
                    End If
                Else
                    dRow("Deletions") = Val(iDelAmt)
                End If

                dDeletions = dDeletions + Val(iDelAmt)
                dRow("DelNo") = dtAssetItem.Rows(0)("AFAD_TransNo")
                iDelReason = dtAssetItem.Rows(0)("AFAD_AssetDeletionType")
                If iDelReason = 0 Then
                    dRow("DelReason") = ""
                ElseIf iDelReason = 1 Then
                    dRow("DelReason") = "Sold"
                ElseIf iDelReason = 2 Then
                    dRow("DelReason") = "Transfer"
                ElseIf iDelReason = 3 Then
                    dRow("DelReason") = "Stolen"
                ElseIf iDelReason = 4 Then
                    dRow("DelReason") = "Destroyed"
                ElseIf iDelReason = 5 Then
                    dRow("DelReason") = "Obsolete"
                End If
                If dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString() <> "" Then
                    If (objGen.FormatDtForRDBMS(dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString(), "D") = "01/01/1900") Or (objGen.FormatDtForRDBMS(dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString(), "D") = "01-01-1900") Then
                        dRow("DeletionDate") = ""
                    Else
                        dRow("DeletionDate") = objGen.FormatDtForRDBMS(dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString(), "D")
                    End If
                Else
                    dRow("DeletionDate") = ""
                End If
                ' dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))

                If (Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))) > 0) Then
                    If (iInAmt > 0) Then
                        dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N1")
                    Else
                        dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                    End If
                Else
                    dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                End If

                dTotalasat = dTotalasat + Convert.ToDouble(dRow("TotalAmount").ToString())
                iDepAmt = dtAssetItem.Rows(0)("AFAA_DepreAmount")
                'dRow("DepUptoPY") = iDepAmt

                If (iDepAmt > 0) Then
                    If (iInAmt > 0) Then
                        dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N1")
                    Else
                        dRow("DepUptoPY") = iDepAmt
                    End If
                Else
                    dRow("DepUptoPY") = iDepAmt
                End If

                dUptoDep = dUptoDep + iDepAmt
                If dtAssetItem.Rows(0)("ADep_TransType") = 1 Then
                    'dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())

                    If (Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString()) > 0) Then
                        If (iInAmt > 0) Then
                            dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N1")
                        Else
                            dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                        End If
                    Else
                        dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                    End If

                    dRow("DepOnAdditions") = 0
                    dOnOpenBalDep = dOnOpenBalDep + Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                Else
                    dRow("DepOnAdditions") = objDBL.SQLGetDescription(sNameSpace, "     select isnull(sum(ADep_DepreciationforFY),0) as ADep_DepreciationforFY from  Acc_AssetDepreciation where ADep_Item=" & iAstItem & " and Adep_CompID=" & iACID & " and aDep_CustId=" & iCustid & " and ADep_YearId =" & iPreviousYearID & "")
                    dRow("DepOnOpengBal") = 0
                    dOnAdditionsDep = dOnAdditionsDep + dRow("DepOnAdditions")
                End If
                iDelDepAmt = dtAssetItem.Rows(0)("AFAD_DelDeprec")
                ' dRow("DepOnDeletions") = iDelDepAmt

                If (iDelDepAmt > 0) Then
                    If (iInAmt > 0) Then
                        dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N1")
                    Else
                        dRow("DepOnDeletions") = iDelDepAmt
                    End If
                Else
                    dRow("DepOnDeletions") = iDelDepAmt
                End If

                'dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))

                If (Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) > 0) Then
                    If (iInAmt > 0) Then
                        dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N1")
                    Else
                        dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))
                    End If
                Else
                    dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))
                End If

                dTotalDep = dTotalDep + Convert.ToDouble(dRow("TotalDepFY").ToString())
                'dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))

                If (Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) > 0) Then
                    If (iInAmt > 0) Then
                        dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N1")
                    Else
                        dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                    End If
                Else
                    dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                End If

                dTotalDepasonDep = dTotalDepasonDep + Convert.ToDouble(dRow("TotalDepasOn").ToString())
                'dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))

                If (dRow("TotalAmount") - Val(dRow("TotalDepasOn")) > 0) Then
                    If (iInAmt > 0) Then
                        dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N1")
                    Else
                        dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))
                    End If
                Else
                    dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))
                End If

                dNBWDVAson = dNBWDVAson + Convert.ToDouble(dRow("WDVasOn").ToString())
                'dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))

                If (Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) > 0) Then
                    If (iInAmt > 0) Then
                        dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N1")
                    Else
                        dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                    End If
                Else
                    dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                End If

                dpNBWDVAson = dpNBWDVAson + Convert.ToDouble(dRow("WDVasOnPY").ToString())
                Dim dtYearDetails As DataTable
                dtYearDetails = LoadAssetDetails(sNameSpace, iPreviousYearID, iACID)
                If iPreviousYearID > 0 Then
                    ihPreviousYearID = iPreviousYearID - 1
                End If
                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & ihPreviousYearID & "")
                dRow("FromDate") = "Cost As at" & " " & dtYearDetails.Rows(0)("YMSFROMDATE")
                dRow("ToDate") = "Total As at" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("PHUpto") = "Up To" & " " & ToDate
                dRow("HTotalDep") = "Total Dep." & " " & dtYearDetails.Rows(0)("YMSID")
                dRow("HTotalDepreciationason") = "Total Depreciation As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("HWDVason") = "WDV As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                dRow("PHWDVason") = "WDV As On" & " " & ToDate
                dRow("Location") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Location") & " And LS_CustId=" & iCustid & "")
                dRow("Division") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Division") & " And LS_CustId=" & iCustid & "")
                dRow("Department") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Department") & " And LS_CustId=" & iCustid & "")
                dRow("Bay") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Bay") & " And LS_CustId=" & iCustid & "")
                dt.Rows.Add(dRow)
            End If

            'If iCount > 0 Then
            '    dRow = dt.NewRow()
            '    dRow("AssetClass") = "<b>" & "Total" & "</b>"
            '    dRow("Costasat") = "<b>" & dOriginalCost & "</b>"
            '    dOriginalCostTot = dOriginalCostTot + dOriginalCost
            '    dRow("Additions") = "<b>" & dAdditions & "</b>"
            '    dAdditionsTot = dAdditionsTot + dAdditions
            '    dRow("Deletions") = "<b>" & dDeletions & "</b>"
            '    dDeletionsTot = dDeletionsTot + dDeletions
            '    dRow("TotalAmount") = "<b>" & dTotalasat & "</b>"
            '    dTotalasatTot = dTotalasatTot + dTotalasat
            '    dRow("DepUptoPY") = "<b>" & dUptoDep & "</b>"
            '    dUptoDepTot = dUptoDepTot + dUptoDep
            '    dRow("DepOnOpengBal") = "<b>" & dOnOpenBalDep & "</b>"
            '    dOnOpenBalDepTot = dOnOpenBalDepTot + dOnOpenBalDep
            '    dRow("DepOnAdditions") = "<b>" & dOnAdditionsDep & "</b>"
            '    dOnAdditionsDepTot = dOnAdditionsDepTot + dOnAdditionsDep
            '    dRow("TotalDepFY") = "<b>" & dTotalDep & "</b>"
            '    dTotalDepTot = dTotalDepTot + dTotalDep
            '    dRow("TotalDepasOn") = "<b>" & dTotalDepasonDep & "</b>"
            '    dTotalDepasonDepTot = dTotalDepasonDepTot + dTotalDepasonDep
            '    dRow("WDVasOn") = "<b>" & dNBWDVAson & "</b>"
            '    dNBWDVAsonTot = dNBWDVAson + dNBWDVAson
            '    dRow("WDVasOnPY") = "<b>" & dpNBWDVAson & "</b>"
            '    dpNBWDVAsonTot = dpNBWDVAsonTot + dpNBWDVAson
            '    dt.Rows.Add(dRow)
            '    dOriginalCost = 0 : dAdditions = 0 : dDeletions = 0 : dTotalasat = 0 : dUptoDep = 0 : dOnOpenBalDep = 0 : dOnAdditionsDep = 0
            '    dTotalDep = 0 : dTotalDepasonDep = 0 : dNBWDVAson = 0 : dpNBWDVAson = 0
            '    iCount = 0
            'End If


            'dRow = dt.NewRow()
            'dRow("AssetClass") = "<b>" & "Grand Total" & "</b>"
            'dRow("Costasat") = "<b>" & dOriginalCostTot & "</b>"
            'dRow("Additions") = "<b>" & dAdditionsTot & "</b>"
            'dRow("Deletions") = "<b>" & dDeletionsTot & "</b>"
            'dRow("TotalAmount") = "<b>" & dTotalasatTot & "</b>"
            'dRow("DepUptoPY") = "<b>" & dUptoDepTot & "</b>"
            'dRow("DepOnOpengBal") = "<b>" & dOnOpenBalDepTot & "</b>"
            'dRow("DepOnAdditions") = "<b>" & dOnAdditionsDepTot & "</b>"
            'dRow("TotalDepFY") = "<b>" & dTotalDepTot & "</b>"
            'dRow("TotalDepasOn") = "<b>" & dTotalDepasonDepTot & "</b>"
            'dRow("WDVasOn") = "<b>" & dNBWDVAsonTot & "</b>"
            'dRow("WDVasOnPY") = "<b>" & dpNBWDVAsonTot & "</b>"
            'dRow("Location") = ""
            'dRow("Division") = ""
            'dRow("Department") = ""
            'dRow("Bay") = ""
            'dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvAddition(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustid As Integer,
      ByVal iLocationId As Integer, ByVal iDivId As Integer, ByVal iDeptId As Integer, ByVal iBayId As Integer, ByVal sAsstCls As String, ByVal iAstItem As Integer, ByVal iTransType As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim dtAmt As Double = 0.0
        Dim ToDate, FromDate As Date
        Dim iPreviousYearID, ihPreviousYearID As Integer
        Dim dOriginalCost As Double = 0.0
        Dim dAdditions As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotalasat As Double = 0.0
        Dim dUptoDep As Double = 0.0
        Dim dOnOpenBalDep As Double = 0.0
        Dim dOnAdditionsDep As Double = 0.0
        Dim dDeletionsDep As Double = 0.0
        Dim dTotalDep As Double = 0.0
        Dim dTotalDepasonDep As Double = 0.0
        Dim dNBWDVAson As Double = 0.0
        Dim dpNBWDVAson As Double = 0.0
        Dim dtAssetItem As New DataTable
        Dim iOrgCost, iAddAmt, iDelAmt, iDepAmt, iDelDepAmt, iDelReason As Double
        Dim dOriginalCostTot As Double = 0.0
        Dim dAdditionsTot As Double = 0.0
        Dim dDeletionsTot As Double = 0.0
        Dim dTotalasatTot As Double = 0.0
        Dim dUptoDepTot As Double = 0.0
        Dim dOnOpenBalDepTot As Double = 0.0
        Dim dOnAdditionsDepTot As Double = 0.0
        Dim dDeletionsDepTot As Double = 0.0
        Dim dTotalDepTot As Double = 0.0
        Dim dTotalDepasonDepTot As Double = 0.0
        Dim dNBWDVAsonTot As Double = 0.0
        Dim dpNBWDVAsonTot As Double = 0.0
        Dim dAddtnDate As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("VoucherNo")
            dt.Columns.Add("AdditionAmt")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("DocNo")
            dt.Columns.Add("DocDate")
            If iyearId > 0 Then
                iPreviousYearID = iyearId
            End If
            sSql = sSql & "select FAAD_MasID,FAAD_Particulars,FAAD_AssetValue,FAAD_DocNo,FAAD_DocDate from  Acc_FixedAssetAdditionDetails "
            sSql = sSql & "  Where FAAD_ItemType ='" & iAstItem & "' and FAAD_CompID=" & iACID & ""
            dtAssetItem = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtAssetItem.Rows.Count > 0 Then
                For i = 0 To dtAssetItem.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("VoucherNo") = i + 1
                    dRow("VoucherNo") = objDBL.SQLGetDescription(sNameSpace, " select AFAA_AssetNo from  Acc_FixedAssetAdditionDel where AFAA_ID=" & dtAssetItem.Rows(i)("FAAD_MasID") & " and AFAA_CompID=" & iACID & " and AFAA_CustId=" & iCustid & " and AFAA_CompID=" & iACID & "")
                    dRow("AdditionAmt") = dtAssetItem.Rows(i)("FAAD_AssetValue")
                    dRow("Particulars") = dtAssetItem.Rows(i)("FAAD_Particulars")
                    dRow("DocNo") = dtAssetItem.Rows(i)("FAAD_DocNo")
                    If (objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D") = "01/01/1900") Or (objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D") = "01-01-1900") Then
                        dRow("DocDate") = ""
                    Else
                        dRow("DocDate") = objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyNameCity(ByVal sAC As String, ByVal icompid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Company_Code,Company_Name,Company_Address,Company_City,Company_PinCode,Company_EmailID,Company_MobileNo,Company_TelephoneNo,Company_WebSite From Trace_CompanyDetails where Company_ID =" & icompid & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadInvDetailedNew(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustid As Integer,
   ByVal iLocationId As Integer, ByVal iDivId As Integer, ByVal iDeptId As Integer, ByVal iBayId As Integer, ByVal sAsstCls As String, ByVal iAsstCls As Integer, ByVal iAstItem As Integer, ByVal iTransType As Integer, ByVal iInAmt As Integer, ByVal iRoundOff As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim dtAmt As Double = 0.0
        Dim ToDate, FromDate As Date
        Dim iPreviousYearID, ihPreviousYearID As Integer
        Dim dOriginalCost As Double = 0.0
        Dim dAdditions As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotalasat As Double = 0.0
        Dim dUptoDep As Double = 0.0
        Dim dOnOpenBalDep As Double = 0.0
        Dim dOnAdditionsDep As Double = 0.0
        Dim dDeletionsDep As Double = 0.0
        Dim dTotalDep As Double = 0.0
        Dim dTotalDepasonDep As Double = 0.0
        Dim dNBWDVAson As Double = 0.0
        Dim dpNBWDVAson As Double = 0.0
        Dim dtAssetItem As New DataTable
        Dim iOrgCost, iAddAmt, iDelAmt, iDepAmt, iDelDepAmt, iDelReason As Double
        Dim dOriginalCostTot As Double = 0.0
        Dim dAdditionsTot As Double = 0.0
        Dim dDeletionsTot As Double = 0.0
        Dim dTotalasatTot As Double = 0.0
        Dim dUptoDepTot As Double = 0.0
        Dim dOnOpenBalDepTot As Double = 0.0
        Dim dOnAdditionsDepTot As Double = 0.0
        Dim dDeletionsDepTot As Double = 0.0
        Dim dTotalDepTot As Double = 0.0
        Dim dTotalDepasonDepTot As Double = 0.0
        Dim dNBWDVAsonTot As Double = 0.0
        Dim dpNBWDVAsonTot As Double = 0.0
        Dim iCount As Integer = 0
        Dim sAstItem As String = ""
        Dim dtType As New DataTable
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("Asset")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("Location")
            dt.Columns.Add("Division")
            dt.Columns.Add("Department")
            dt.Columns.Add("Bay")
            dt.Columns.Add("Costasat")
            dt.Columns.Add("Additions")
            dt.Columns.Add("Deletions")
            dt.Columns.Add("DelReason")
            dt.Columns.Add("DelNo")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("DepUptoPY")
            dt.Columns.Add("DepOnOpengBal")
            dt.Columns.Add("DepOnAdditions")
            dt.Columns.Add("DepOnDeletions")
            dt.Columns.Add("DeletionDate")
            dt.Columns.Add("TotalDepFY")
            dt.Columns.Add("TotalDepasOn")
            dt.Columns.Add("WDVasOn")
            dt.Columns.Add("WDVasOnPY")
            dt.Columns.Add("PHUpto")
            dt.Columns.Add("HTotalDep")
            dt.Columns.Add("HTotalDepreciationason")
            dt.Columns.Add("HWDVason")
            dt.Columns.Add("PHWDVason")
            dt.Columns.Add("Total")
            If iyearId > 0 Then
                iPreviousYearID = iyearId
            End If

            If (iAsstCls = 0) Then
                dtType = loadAssetType(sNameSpace, iACID, iCustid)
                If dtType.Rows.Count > 0 Then
                    For i = 0 To dtType.Rows.Count - 1
                        sAstItem = LoadAllAssetItems(sNameSpace, iACID, iyearId, iCustid, iLocationId, iDivId, iDeptId, iBayId, dtType.Rows(i)("AM_ID"))
                        sSql = ""
                        sSql = sSql & "  Select  AFAM_ID,AFAM_AssetType,AFAM_ItemDescription,AFAM_AssetCode,AFAD_TransNo,isnull(sum(AFAA_AssetAmount), 0)As OrgCost,isnull(sum( AFAD_Amount),0)As DelAmt,"
                        sSql = sSql & "  isnull(sum(AFAA_DepreAmount), 0)As AFAA_DepreAmount,isnull(sum(ADep_DepreciationforFY),0)As ADep_DepreciationforFY,"
                        sSql = sSql & "  isnull(sum(AFAD_DelDeprec), 0)As AFAD_DelDeprec,isnull(sum(ADep_TransType),0) As ADep_TransType,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay,isnull(sum(AFAD_AssetDeletionType),0) As AFAD_AssetDeletionType, "
                        sSql = sSql & "  AFAD_TransNo,AFAD_DeletionDate"
                        sSql = sSql & "  From Acc_FixedAssetMaster "
                        sSql = sSql & "  Left Join Acc_FixedAssetAdditionDel On  AFAA_ItemType=  AFAM_ID "
                        'sSql = sSql & "   Left Join Acc_FixedAssetAdditionDetails On FAAD_ItemType=AFAM_ID "
                        sSql = sSql & "  Left Join Acc_FixedAssetDeletion On AFAD_Asset=AFAM_ID "
                        sSql = sSql & "  Left Join Acc_AssetDepreciation On ADep_Item=AFAM_ID And ADep_YearId=" & iyearId & " "
                        sSql = sSql & "  Left Join Acc_AssetLocationSetup on LS_ID=AFAM_ID "
                        'sSql = sSql & "  Where AFAM_ID ='" & iAstItem & "' And AFAM_YearId=" & iyearId & "  " Steffi
                        sSql = sSql & "  Where AFAM_ID in(" & sAstItem & ") And AFAM_YearId=" & iyearId & "  "
                        If iTransType = 1 Then
                            sSql = sSql & " and AFAA_TrType =1"
                        End If
                        If iTransType = 2 Then
                            sSql = sSql & " and AFAA_TrType =2"
                        End If
                        If iTransType = 3 Then
                            sSql = sSql & " and   AFAD_Asset in (" & sAstItem & ") "
                        End If
                        sSql = sSql & "  group by AFAM_ID,AFAM_AssetType,AFAM_ItemDescription,AFAM_AssetCode,AFAD_TransNo,AFAA_AssetAmount, ADep_TransType, ADep_DepreciationforFY, AFAD_DelDeprec, AFAA_Location, AFAA_Division, AFAA_Department, AFAA_Bay,AFAD_AssetDeletionType,AFAD_TransNo,AFAD_DeletionDate "
                        dtAssetItem = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                        If dtAssetItem.Rows.Count > 0 Then
                            For j = 0 To dtAssetItem.Rows.Count - 1
                                iCount = 1
                                dRow = dt.NewRow()
                                dRow("ID") = 0
                                dRow("AssetClass") = dtType.Rows(i)("AM_Description")
                                dRow("Asset") = dtAssetItem.Rows(j)("AFAM_ItemDescription")
                                dRow("AssetCode") = dtAssetItem.Rows(j)("AFAM_AssetCode")
                                iOrgCost = dtAssetItem.Rows(j)("OrgCost")

                                If (iOrgCost > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N4")
                                        End If
                                    Else
                                        dRow("Costasat") = Val(iOrgCost)
                                    End If
                                Else
                                    dRow("Costasat") = Val(iOrgCost)
                                End If

                                dOriginalCost = dOriginalCost + Val(iOrgCost)

                                'iAddAmt = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(FAAD_AssetValue),0) as FAAD_AssetValue  from  Acc_FixedAssetAdditionDetails where FAAD_ItemType=" & iAstItem & " and faad_CompID=" & iACID & " and faad_CustId=" & iCustid & "")
                                iAddAmt = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(FAAD_AssetValue),0) as FAAD_AssetValue  from  Acc_FixedAssetAdditionDetails where FAAD_ItemType in('" & dtAssetItem.Rows(j)("AFAM_Id") & "') and faad_CompID=" & iACID & " and faad_CustId=" & iCustid & "")
                                If Val(iAddAmt) > 0 Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("Additions") = Val(iAddAmt)
                                    End If
                                    dAdditions = dAdditions + Val(iAddAmt)
                                Else
                                    dRow("Additions") = 0
                                End If

                                iDelAmt = dtAssetItem.Rows(j)("DelAmt")
                                'dRow("Deletions") = Val(iDelAmt)

                                If (iDelAmt > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("Deletions") = Val(iDelAmt)
                                    End If
                                Else
                                    dRow("Deletions") = Val(iDelAmt)
                                End If

                                dDeletions = dDeletions + Val(iDelAmt)
                                dRow("DelNo") = dtAssetItem.Rows(j)("AFAD_TransNo")
                                iDelReason = dtAssetItem.Rows(j)("AFAD_AssetDeletionType")
                                If iDelReason = 0 Then
                                    dRow("DelReason") = ""
                                ElseIf iDelReason = 1 Then
                                    dRow("DelReason") = "Sold"
                                ElseIf iDelReason = 2 Then
                                    dRow("DelReason") = "Transfer"
                                ElseIf iDelReason = 3 Then
                                    dRow("DelReason") = "Stolen"
                                ElseIf iDelReason = 4 Then
                                    dRow("DelReason") = "Destroyed"
                                ElseIf iDelReason = 5 Then
                                    dRow("DelReason") = "Obsolete"
                                End If
                                If dtAssetItem.Rows(j)("AFAD_DeletionDate").ToString() <> "" Then
                                    If (objGen.FormatDtForRDBMS(dtAssetItem.Rows(j)("AFAD_DeletionDate").ToString(), "D") = "01/01/1900") Or (objGen.FormatDtForRDBMS(dtAssetItem.Rows(j)("AFAD_DeletionDate").ToString(), "D") = "01-01-1900") Then
                                        dRow("DeletionDate") = ""
                                    Else
                                        dRow("DeletionDate") = objGen.FormatDtForRDBMS(dtAssetItem.Rows(j)("AFAD_DeletionDate").ToString(), "D")
                                    End If
                                Else
                                    dRow("DeletionDate") = ""
                                End If
                                ' dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))

                                If (Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))) > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                                    End If
                                Else
                                    dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                                End If

                                dTotalasat = dTotalasat + Convert.ToDouble(dRow("TotalAmount").ToString())
                                iDepAmt = dtAssetItem.Rows(j)("AFAA_DepreAmount")
                                'dRow("DepUptoPY") = iDepAmt

                                If (iDepAmt > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("DepUptoPY") = iDepAmt
                                    End If
                                Else
                                    dRow("DepUptoPY") = iDepAmt
                                End If

                                dUptoDep = dUptoDep + iDepAmt
                                If dtAssetItem.Rows(j)("ADep_TransType") = 1 Then
                                    'dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())

                                    If (Convert.ToDouble(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString()) > 0) Then
                                        If (iInAmt > 0) Then
                                            If (iRoundOff = 0) Then
                                                dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N0")
                                            ElseIf (iRoundOff = 1) Then
                                                dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N1")
                                            ElseIf (iRoundOff = 2) Then
                                                dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N2")
                                            ElseIf (iRoundOff = 3) Then
                                                dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N3")
                                            ElseIf (iRoundOff = 4) Then
                                                dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N4")
                                            End If

                                        Else
                                            dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString())
                                        End If
                                    Else
                                        dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString())
                                    End If

                                    dRow("DepOnAdditions") = 0
                                    dOnOpenBalDep = dOnOpenBalDep + Convert.ToDouble(dtAssetItem.Rows(j)("ADep_DepreciationforFY").ToString())
                                Else
                                    'dRow("DepOnAdditions") = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADep_DepreciationforFY),0) as ADep_DepreciationforFY from  Acc_AssetDepreciation where ADep_Item=" & iAstItem & " and Adep_CompID=" & iACID & " and aDep_CustId=" & iCustid & " and ADep_YearId =" & iPreviousYearID & "")
                                    dRow("DepOnAdditions") = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADep_DepreciationforFY),0) as ADep_DepreciationforFY from  Acc_AssetDepreciation where ADep_Item in('" & dtAssetItem.Rows(j)("AFAM_Id") & "') and Adep_CompID=" & iACID & " and aDep_CustId=" & iCustid & " and ADep_YearId =" & iPreviousYearID & "")
                                    dRow("DepOnOpengBal") = 0
                                    dOnAdditionsDep = dOnAdditionsDep + dRow("DepOnAdditions")
                                End If
                                iDelDepAmt = dtAssetItem.Rows(j)("AFAD_DelDeprec")
                                ' dRow("DepOnDeletions") = iDelDepAmt

                                If (iDelDepAmt > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N4")
                                        End If
                                    Else
                                        dRow("DepOnDeletions") = iDelDepAmt
                                    End If
                                Else
                                    dRow("DepOnDeletions") = iDelDepAmt
                                End If

                                'dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))

                                If (Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))
                                    End If
                                Else
                                    dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))
                                End If

                                dTotalDep = dTotalDep + Convert.ToDouble(dRow("TotalDepFY").ToString())
                                'dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))

                                If (Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                    End If
                                Else
                                    dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                                End If

                                dTotalDepasonDep = dTotalDepasonDep + Convert.ToDouble(dRow("TotalDepasOn").ToString())
                                'dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))

                                If (dRow("TotalAmount") - Val(dRow("TotalDepasOn")) > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))
                                    End If
                                Else
                                    dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))
                                End If

                                dNBWDVAson = dNBWDVAson + Convert.ToDouble(dRow("WDVasOn").ToString())
                                'dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))

                                If (Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) > 0) Then
                                    If (iInAmt > 0) Then
                                        If (iRoundOff = 0) Then
                                            dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N0")
                                        ElseIf (iRoundOff = 1) Then
                                            dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N1")
                                        ElseIf (iRoundOff = 2) Then
                                            dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N2")
                                        ElseIf (iRoundOff = 3) Then
                                            dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N3")
                                        ElseIf (iRoundOff = 4) Then
                                            dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N4")
                                        End If

                                    Else
                                        dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                    End If
                                Else
                                    dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                                End If

                                dpNBWDVAson = dpNBWDVAson + Convert.ToDouble(dRow("WDVasOnPY").ToString())
                                Dim dtYearDetails As DataTable
                                dtYearDetails = LoadAssetDetails(sNameSpace, iPreviousYearID, iACID)
                                If iPreviousYearID > 0 Then
                                    ihPreviousYearID = iPreviousYearID - 1
                                End If
                                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & ihPreviousYearID & "")
                                dRow("FromDate") = "Cost As at" & " " & dtYearDetails.Rows(0)("YMSFROMDATE")
                                dRow("ToDate") = "Total As at" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                                dRow("PHUpto") = "Up To" & " " & ToDate
                                dRow("HTotalDep") = "Total Dep." & " " & dtYearDetails.Rows(0)("YMSID")
                                dRow("HTotalDepreciationason") = "Total Depreciation As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                                dRow("HWDVason") = "WDV As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                                dRow("PHWDVason") = "WDV As On" & " " & ToDate
                                dRow("Location") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(j)("AFAA_Location") & " And LS_CustId=" & iCustid & "")
                                dRow("Division") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(j)("AFAA_Division") & " And LS_CustId=" & iCustid & "")
                                dRow("Department") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(j)("AFAA_Department") & " And LS_CustId=" & iCustid & "")
                                dRow("Bay") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(j)("AFAA_Bay") & " And LS_CustId=" & iCustid & "")
                                dt.Rows.Add(dRow)
                            Next

                        End If
                    Next
                End If
            Else
                If (iAstItem = 0) Then
                    sAstItem = LoadAllAssetItems(sNameSpace, iACID, iyearId, iCustid, iLocationId, iDivId, iDeptId, iBayId, iAsstCls)
                Else
                    sAstItem = iAstItem.ToString()
                End If

                sSql = sSql & "  Select  AFAM_ID,AFAM_AssetType,AFAM_ItemDescription,AFAM_AssetCode,AFAD_TransNo,isnull(sum(AFAA_AssetAmount), 0)As OrgCost,isnull(sum( AFAD_Amount),0)As DelAmt,"
                sSql = sSql & "  isnull(sum(AFAA_DepreAmount), 0)As AFAA_DepreAmount,isnull(sum(ADep_DepreciationforFY),0)As ADep_DepreciationforFY,"
                sSql = sSql & "  isnull(sum(AFAD_DelDeprec), 0)As AFAD_DelDeprec,isnull(sum(ADep_TransType),0) As ADep_TransType,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay,isnull(sum(AFAD_AssetDeletionType),0) As AFAD_AssetDeletionType, "
                sSql = sSql & "  AFAD_TransNo,AFAD_DeletionDate"
                sSql = sSql & "  From Acc_FixedAssetMaster "
                sSql = sSql & "  Left Join Acc_FixedAssetAdditionDel On  AFAA_ItemType=  AFAM_ID "
                'sSql = sSql & "   Left Join Acc_FixedAssetAdditionDetails On FAAD_ItemType=AFAM_ID "
                sSql = sSql & "  Left Join Acc_FixedAssetDeletion On AFAD_Asset=AFAM_ID "
                sSql = sSql & "  Left Join Acc_AssetDepreciation On ADep_Item=AFAM_ID And ADep_YearId=" & iyearId & " "
                sSql = sSql & "  Left Join Acc_AssetLocationSetup on LS_ID=AFAM_ID "
                'sSql = sSql & "  Where AFAM_ID ='" & iAstItem & "' And AFAM_YearId=" & iyearId & "  " Steffi
                sSql = sSql & "  Where  AFAM_YearId=" & iyearId & "  "

                If sAstItem <> "" Then
                    sSql = sSql & " And AFAM_ID in(" & sAstItem & ") "
                End If


                If iTransType = 1 Then
                    sSql = sSql & " and AFAA_TrType =1"
                End If
                If iTransType = 2 Then
                    sSql = sSql & " and AFAA_TrType =2"
                End If
                If iTransType = 3 Then
                    sSql = sSql & " and   AFAD_Asset in (" & sAstItem & ") "
                End If
                sSql = sSql & "  group by AFAM_ID,AFAM_AssetType,AFAM_ItemDescription,AFAM_AssetCode,AFAD_TransNo,AFAA_AssetAmount, ADep_TransType, ADep_DepreciationforFY, AFAD_DelDeprec, AFAA_Location, AFAA_Division, AFAA_Department, AFAA_Bay,AFAD_AssetDeletionType,AFAD_TransNo,AFAD_DeletionDate "
                dtAssetItem = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                If dtAssetItem.Rows.Count > 0 Then
                    iCount = 1
                    dRow = dt.NewRow()
                    dRow("ID") = 0
                    dRow("AssetClass") = sAsstCls
                    dRow("Asset") = dtAssetItem.Rows(0)("AFAM_ItemDescription")
                    dRow("AssetCode") = dtAssetItem.Rows(0)("AFAM_AssetCode")
                    iOrgCost = dtAssetItem.Rows(0)("OrgCost")
                    'dRow("Costasat") = Val(iOrgCost)

                    If (iOrgCost > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("Costasat") = Convert.ToDecimal(iOrgCost / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("Costasat") = Val(iOrgCost)
                        End If
                    Else
                        dRow("Costasat") = Val(iOrgCost)
                    End If

                    dOriginalCost = dOriginalCost + Val(iOrgCost)

                    'iAddAmt = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(FAAD_AssetValue),0) as FAAD_AssetValue  from  Acc_FixedAssetAdditionDetails where FAAD_ItemType=" & iAstItem & " and faad_CompID=" & iACID & " and faad_CustId=" & iCustid & "")
                    Try
                        iAddAmt = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(FAAD_AssetValue),0) as FAAD_AssetValue  from  Acc_FixedAssetAdditionDetails where FAAD_ItemType in(" & sAstItem & ") and faad_CompID=" & iACID & " and faad_CustId=" & iCustid & "")
                    Catch ex As Exception
                        iAddAmt = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(FAAD_AssetValue),0) as FAAD_AssetValue  from  Acc_FixedAssetAdditionDetails where  faad_CompID=" & iACID & " and faad_CustId=" & iCustid & "")
                    End Try

                    If Val(iAddAmt) > 0 Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("Additions") = Convert.ToDecimal(iAddAmt / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("Additions") = Val(iAddAmt)
                        End If
                        dAdditions = dAdditions + Val(iAddAmt)
                    Else
                        dRow("Additions") = 0
                    End If

                    iDelAmt = dtAssetItem.Rows(0)("DelAmt")
                    'dRow("Deletions") = Val(iDelAmt)

                    If (iDelAmt > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("Deletions") = Convert.ToDecimal(iDelAmt / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("Deletions") = Val(iDelAmt)
                        End If
                    Else
                        dRow("Deletions") = Val(iDelAmt)
                    End If

                    dDeletions = dDeletions + Val(iDelAmt)
                    dRow("DelNo") = dtAssetItem.Rows(0)("AFAD_TransNo")
                    iDelReason = dtAssetItem.Rows(0)("AFAD_AssetDeletionType")
                    If iDelReason = 0 Then
                        dRow("DelReason") = ""
                    ElseIf iDelReason = 1 Then
                        dRow("DelReason") = "Sold"
                    ElseIf iDelReason = 2 Then
                        dRow("DelReason") = "Transfer"
                    ElseIf iDelReason = 3 Then
                        dRow("DelReason") = "Stolen"
                    ElseIf iDelReason = 4 Then
                        dRow("DelReason") = "Destroyed"
                    ElseIf iDelReason = 5 Then
                        dRow("DelReason") = "Obsolete"
                    End If
                    If dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString() <> "" Then
                        If (objGen.FormatDtForRDBMS(dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString(), "D") = "01/01/1900") Or (objGen.FormatDtForRDBMS(dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString(), "D") = "01-01-1900") Then
                            dRow("DeletionDate") = ""
                        Else
                            dRow("DeletionDate") = objGen.FormatDtForRDBMS(dtAssetItem.Rows(0)("AFAD_DeletionDate").ToString(), "D")
                        End If
                    Else
                        dRow("DeletionDate") = ""
                    End If
                    ' dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))

                    If (Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("TotalAmount") = Convert.ToDecimal(Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions")) / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                        End If
                    Else
                        dRow("TotalAmount") = Val(dRow("Costasat")) + Val(dRow("Additions")) - Val(dRow("Deletions"))
                    End If

                    dTotalasat = dTotalasat + Convert.ToDouble(dRow("TotalAmount").ToString())
                    iDepAmt = dtAssetItem.Rows(0)("AFAA_DepreAmount")
                    'dRow("DepUptoPY") = iDepAmt

                    If (iDepAmt > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("TotalAmount") = Convert.ToDecimal(iDepAmt / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("DepUptoPY") = iDepAmt
                        End If
                    Else
                        dRow("DepUptoPY") = iDepAmt
                    End If

                    dUptoDep = dUptoDep + iDepAmt
                    If dtAssetItem.Rows(0)("ADep_TransType") = 1 Then
                        'dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())

                        If (Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString()) > 0) Then
                            If (iInAmt > 0) Then
                                If (iRoundOff = 0) Then
                                    dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N0")
                                ElseIf (iRoundOff = 1) Then
                                    dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N1")
                                ElseIf (iRoundOff = 2) Then
                                    dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N2")
                                ElseIf (iRoundOff = 3) Then
                                    dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N3")
                                ElseIf (iRoundOff = 4) Then
                                    dRow("DepOnOpengBal") = Convert.ToDecimal(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString() / iInAmt).ToString("N4")
                                End If

                            Else
                                dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                            End If
                        Else
                            dRow("DepOnOpengBal") = Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                        End If

                        dRow("DepOnAdditions") = 0
                        dOnOpenBalDep = dOnOpenBalDep + Convert.ToDouble(dtAssetItem.Rows(0)("ADep_DepreciationforFY").ToString())
                    Else
                        'dRow("DepOnAdditions") = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADep_DepreciationforFY),0) as ADep_DepreciationforFY from  Acc_AssetDepreciation where ADep_Item=" & iAstItem & " and Adep_CompID=" & iACID & " and aDep_CustId=" & iCustid & " and ADep_YearId =" & iPreviousYearID & "")
                        dRow("DepOnAdditions") = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADep_DepreciationforFY),0) as ADep_DepreciationforFY from  Acc_AssetDepreciation where ADep_Item in(" & sAstItem & ") and Adep_CompID=" & iACID & " and aDep_CustId=" & iCustid & " and ADep_YearId =" & iPreviousYearID & "")
                        dRow("DepOnOpengBal") = 0
                        dOnAdditionsDep = dOnAdditionsDep + dRow("DepOnAdditions")
                    End If
                    iDelDepAmt = dtAssetItem.Rows(0)("AFAD_DelDeprec")
                    ' dRow("DepOnDeletions") = iDelDepAmt

                    If (iDelDepAmt > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(iDelDepAmt / iInAmt).ToString("N4")
                            End If
                        Else
                            dRow("DepOnDeletions") = iDelDepAmt
                        End If
                    Else
                        dRow("DepOnDeletions") = iDelDepAmt
                    End If

                    'dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))

                    If (Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("DepOnOpengBal") = Convert.ToDecimal(Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions")) / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))
                        End If
                    Else
                        dRow("TotalDepFY") = Val(dRow("DepOnOpengBal")) + Val(dRow("DepOnAdditions")) + Val(dRow("DepOnDeletions"))
                    End If

                    dTotalDep = dTotalDep + Convert.ToDouble(dRow("TotalDepFY").ToString())
                    'dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))

                    If (Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("TotalDepasOn") = Convert.ToDecimal(Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY")) / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                        End If
                    Else
                        dRow("TotalDepasOn") = Val(dRow("DepUptoPY")) + Val(dRow("TotalDepFY"))
                    End If

                    dTotalDepasonDep = dTotalDepasonDep + Convert.ToDouble(dRow("TotalDepasOn").ToString())
                    'dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))

                    If (dRow("TotalAmount") - Val(dRow("TotalDepasOn")) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("WDVasOn") = Convert.ToDecimal(dRow("TotalAmount") - Val(dRow("TotalDepasOn")) / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))
                        End If
                    Else
                        dRow("WDVasOn") = dRow("TotalAmount") - Val(dRow("TotalDepasOn"))
                    End If

                    dNBWDVAson = dNBWDVAson + Convert.ToDouble(dRow("WDVasOn").ToString())
                    'dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))

                    If (Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) > 0) Then
                        If (iInAmt > 0) Then
                            If (iRoundOff = 0) Then
                                dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N0")
                            ElseIf (iRoundOff = 1) Then
                                dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N1")
                            ElseIf (iRoundOff = 2) Then
                                dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N2")
                            ElseIf (iRoundOff = 3) Then
                                dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N3")
                            ElseIf (iRoundOff = 4) Then
                                dRow("WDVasOnPY") = Convert.ToDecimal(Val(dRow("Costasat")) - Val(dRow("DepUptoPY")) / iInAmt).ToString("N4")
                            End If

                        Else
                            dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                        End If
                    Else
                        dRow("WDVasOnPY") = Val(dRow("Costasat")) - Val(dRow("DepUptoPY"))
                    End If

                    dpNBWDVAson = dpNBWDVAson + Convert.ToDouble(dRow("WDVasOnPY").ToString())
                    Dim dtYearDetails As DataTable
                    dtYearDetails = LoadAssetDetails(sNameSpace, iPreviousYearID, iACID)
                    If iPreviousYearID > 0 Then
                        ihPreviousYearID = iPreviousYearID - 1
                    End If
                    ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & ihPreviousYearID & "")
                    dRow("FromDate") = "Cost As at" & " " & dtYearDetails.Rows(0)("YMSFROMDATE")
                    dRow("ToDate") = "Total As at" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                    dRow("PHUpto") = "Up To" & " " & ToDate
                    dRow("HTotalDep") = "Total Dep." & " " & dtYearDetails.Rows(0)("YMSID")
                    dRow("HTotalDepreciationason") = "Total Depreciation As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                    dRow("HWDVason") = "WDV As On" & " " & dtYearDetails.Rows(0)("YMSTODATE")
                    dRow("PHWDVason") = "WDV As On" & " " & ToDate
                    dRow("Location") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Location") & " And LS_CustId=" & iCustid & "")
                    dRow("Division") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Division") & " And LS_CustId=" & iCustid & "")
                    dRow("Department") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Department") & " And LS_CustId=" & iCustid & "")
                    dRow("Bay") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dtAssetItem.Rows(0)("AFAA_Bay") & " And LS_CustId=" & iCustid & "")
                    dt.Rows.Add(dRow)
                End If
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadInvAdditionNew(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustid As Integer,
      ByVal iLocationId As Integer, ByVal iDivId As Integer, ByVal iDeptId As Integer, ByVal iBayId As Integer, ByVal iAsstCls As Integer, ByVal sAstItem As String, ByVal iTransType As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim dtAmt As Double = 0.0
        Dim ToDate, FromDate As Date
        Dim iPreviousYearID, ihPreviousYearID As Integer
        Dim dOriginalCost As Double = 0.0
        Dim dAdditions As Double = 0.0
        Dim dDeletions As Double = 0.0
        Dim dTotalasat As Double = 0.0
        Dim dUptoDep As Double = 0.0
        Dim dOnOpenBalDep As Double = 0.0
        Dim dOnAdditionsDep As Double = 0.0
        Dim dDeletionsDep As Double = 0.0
        Dim dTotalDep As Double = 0.0
        Dim dTotalDepasonDep As Double = 0.0
        Dim dNBWDVAson As Double = 0.0
        Dim dpNBWDVAson As Double = 0.0
        Dim dtAssetItem As New DataTable
        Dim iOrgCost, iAddAmt, iDelAmt, iDepAmt, iDelDepAmt, iDelReason As Double
        Dim dOriginalCostTot As Double = 0.0
        Dim dAdditionsTot As Double = 0.0
        Dim dDeletionsTot As Double = 0.0
        Dim dTotalasatTot As Double = 0.0
        Dim dUptoDepTot As Double = 0.0
        Dim dOnOpenBalDepTot As Double = 0.0
        Dim dOnAdditionsDepTot As Double = 0.0
        Dim dDeletionsDepTot As Double = 0.0
        Dim dTotalDepTot As Double = 0.0
        Dim dTotalDepasonDepTot As Double = 0.0
        Dim dNBWDVAsonTot As Double = 0.0
        Dim dpNBWDVAsonTot As Double = 0.0
        Dim dAddtnDate As String = ""
        Dim dtType As New DataTable
        Dim sAssetCls As String
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("VoucherNo")
            dt.Columns.Add("AdditionAmt")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("DocNo")
            dt.Columns.Add("DocDate")
            If iyearId > 0 Then
                iPreviousYearID = iyearId
            End If

            If (iAsstCls = 0) Then
                dtType = loadAssetType(sNameSpace, iACID, iCustid)
                If dtType.Rows.Count > 0 Then
                    For i = 0 To dtType.Rows.Count - 1
                        sAssetCls = sAssetCls & ",'" & dtType.Rows(i)("AM_ID") & "'"
                    Next
                    If (sAssetCls <> "") Then
                        sAssetCls = sAssetCls.Remove(0, 1)
                    End If

                    sAstItem = LoadAllAssetItems(sNameSpace, iACID, iyearId, iCustid, iLocationId, iDivId, iDeptId, iBayId, sAssetCls)

                    sSql = sSql & "select FAAD_MasID,FAAD_Particulars,FAAD_AssetValue,FAAD_DocNo,FAAD_DocDate from  Acc_FixedAssetAdditionDetails "
                    sSql = sSql & "  Where FAAD_ItemType in(" & sAstItem & ") and FAAD_CompID=" & iACID & ""
                    dtAssetItem = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                    If dtAssetItem.Rows.Count > 0 Then
                        For i = 0 To dtAssetItem.Rows.Count - 1
                            dRow = dt.NewRow()
                            dRow("VoucherNo") = i + 1
                            dRow("VoucherNo") = objDBL.SQLGetDescription(sNameSpace, " select AFAA_AssetNo from  Acc_FixedAssetAdditionDel where AFAA_ID=" & dtAssetItem.Rows(i)("FAAD_MasID") & " and AFAA_CompID=" & iACID & " and AFAA_CustId=" & iCustid & " and AFAA_CompID=" & iACID & "")
                            dRow("AdditionAmt") = dtAssetItem.Rows(i)("FAAD_AssetValue")
                            dRow("Particulars") = dtAssetItem.Rows(i)("FAAD_Particulars")
                            dRow("DocNo") = dtAssetItem.Rows(i)("FAAD_DocNo")
                            If (objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D") = "01/01/1900") Or (objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D") = "01-01-1900") Then
                                dRow("DocDate") = ""
                            Else
                                dRow("DocDate") = objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D")
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                End If

            Else
                sSql = sSql & "select FAAD_MasID,FAAD_Particulars,FAAD_AssetValue,FAAD_DocNo,FAAD_DocDate from  Acc_FixedAssetAdditionDetails "
                sSql = sSql & "  Where FAAD_ItemType in(" & sAstItem & ") and FAAD_CompID=" & iACID & ""
                dtAssetItem = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                If dtAssetItem.Rows.Count > 0 Then
                    For i = 0 To dtAssetItem.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("VoucherNo") = i + 1
                        dRow("VoucherNo") = objDBL.SQLGetDescription(sNameSpace, " select AFAA_AssetNo from  Acc_FixedAssetAdditionDel where AFAA_ID=" & dtAssetItem.Rows(i)("FAAD_MasID") & " and AFAA_CompID=" & iACID & " and AFAA_CustId=" & iCustid & " and AFAA_CompID=" & iACID & "")
                        dRow("AdditionAmt") = dtAssetItem.Rows(i)("FAAD_AssetValue")
                        dRow("Particulars") = dtAssetItem.Rows(i)("FAAD_Particulars")
                        dRow("DocNo") = dtAssetItem.Rows(i)("FAAD_DocNo")
                        If (objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D") = "01/01/1900") Or (objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D") = "01-01-1900") Then
                            dRow("DocDate") = ""
                        Else
                            dRow("DocDate") = objGen.FormatDtForRDBMS(dtAssetItem.Rows(i)("FAAD_DocDate").ToString(), "D")
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadAllAssetItems(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustid As Integer,
                                      ByVal iLocationId As Integer, ByVal iDivId As Integer, ByVal iDeptId As Integer, ByVal iBayId As Integer, ByVal sAssetType As String) As String
        Dim dt As New DataTable
        Dim sAssetCls As String = ""
        Try
            dt = objAsstTrn.ExistingItemCodeAll(sNameSpace, iACID, sAssetType, iyearId, iCustid, iLocationId, iDivId, iDeptId, iBayId)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sAssetCls = sAssetCls & ",'" & dt.Rows(i)("AFAM_ID") & "'"
                Next
                If (sAssetCls <> "") Then
                    sAssetCls = sAssetCls.Remove(0, 1)
                End If
            End If
            Return sAssetCls
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAssesmentYear(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iyearId As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Dim sYear As String
        Try
            sSql = "Select * from year_master where YMS_YearID = " & iyearId + 1 & " and YMS_CompID=" & iACID & " and  YMS_Status='A'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("YMS_ID").ToString()) = False Then
                    sYear = dt.Rows(0)("YMS_ID").ToString()
                Else
                    sYear = 0.0
                End If
            End If
            Return sYear
        Catch ex As Exception

        End Try
    End Function
End Class

