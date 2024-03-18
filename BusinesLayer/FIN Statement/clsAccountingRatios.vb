Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsAccountingRatios
    Private objDBL As New DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadAccRatio(ByVal sAC As String, ByVal ICustid As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable, dtTable As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer = 0
        Dim sSql As String = ""
        Dim dPandLamt As Double = 0.0
        Dim dPrevPandLamt As Double = 0.0
        Try
            dtDetails.Columns.Add("Sr_No")
            dtDetails.Rows.Add("1.")
            dtDetails.Rows.Add("2.")
            dtDetails.Rows.Add("3.")
            dtDetails.Rows.Add("4.")
            dtDetails.Rows.Add("5.")
            dtDetails.Rows.Add("6.")
            dtDetails.Rows.Add("7.")
            dtDetails.Rows.Add("8.")
            dtDetails.Rows.Add("9.")
            dtDetails.Rows.Add("10.")
            dtDetails.Rows.Add("11.")

            dtDetails.Columns.Add("Ratios")
            dtDetails(0)("Ratios") = "Debt Equity Ratio"
            dtDetails(1)("Ratios") = "Debt Capital"
            dtDetails(2)("Ratios") = "Debt Service coverage ratio"
            dtDetails(3)("Ratios") = "Return on Equity Ratio"
            dtDetails(4)("Ratios") = "Inventory Turnover Ratio"
            dtDetails(5)("Ratios") = "Trade Receivables turnover ratio"
            dtDetails(6)("Ratios") = "Trade payables turnover ratio"
            dtDetails(7)("Ratios") = "Net capital turnover ratio"
            dtDetails(8)("Ratios") = "Net profit ratio"
            dtDetails(9)("Ratios") = "Return on Capital employed"
            dtDetails(10)("Ratios") = "Return on investment"

            dtDetails.Columns.Add("Numerator")
            dtDetails(0)("Numerator") = "Total Current Assets"
            dtDetails(1)("Numerator") = "Debt Capital"
            dtDetails(2)("Numerator") = "EBITDA-CAPEX"
            dtDetails(3)("Numerator") = "Profit for the year"
            dtDetails(4)("Numerator") = "COGS"
            dtDetails(5)("Numerator") = "Net Sales"
            dtDetails(6)("Numerator") = "Total Purchases (Fuel Cost + Other Expenses+Closing Inventory-Opening Inventory)"
            dtDetails(7)("Numerator") = "Sales"
            dtDetails(8)("Numerator") = "Net Profit"
            dtDetails(9)("Numerator") = "Earnings before interest and tax"
            dtDetails(10)("Numerator") = "Net Profit"

            dtDetails.Columns.Add("Denominator")
            dtDetails(0)("Denominator") = "Total Current Liabilities"
            dtDetails(1)("Denominator") = "Shareholder's Equity"
            dtDetails(2)("Denominator") = "Debt Service (Int+Principal)"
            dtDetails(3)("Denominator") = "Average Shareholder’s Equity"
            dtDetails(4)("Denominator") = "Average Inventory"
            dtDetails(5)("Denominator") = "Average trade receivables"
            dtDetails(6)("Denominator") = "Closing Trade Payables"
            dtDetails(7)("Denominator") = "Workimg capital (CA-CL)"
            dtDetails(8)("Denominator") = "Sales"
            dtDetails(9)("Denominator") = "Capital Employed"
            dtDetails(10)("Denominator") = "Investment"

            dtDetails.Columns.Add("Current_Reporting_Period")
            dtDetails.Columns.Add("Previous_reporting_period")
            dtDetails.Columns.Add("Change")

            ''Ratio 1
            dt = GetHeadingAmt1(sAC, iYearID, ICustid, 4, 42)      'Current Liabilities
            dt1 = GetHeadingAmt1(sAC, iYearID, ICustid, 4, 44)  'Current assets
            Dim dCValue, dPvalue, dDiffVal As Double
            If dt1.Rows.Count > 0 And dt.Rows.Count > 0 Then
                If dt(0)("DC1") > 0 Then


                    If Val(dt1(0)("Dc1")) <> 0 Or Val(dt(0)("DC1")) <> 0 Then
                        dCValue = Val(dt1(0)("Dc1") / dt(0)("DC1"))
                    Else
                        dCValue = 0
                    End If
                End If
                If Val(dt1(0)("DP1")) < 0 Or Val(dt(0)("DP1")) < 0 Then
                    dPvalue = Val(dt1(0)("DP1") / dt(0)("DP1"))
                Else
                    dPvalue = 0
                End If
                If dCValue.ToString = "NaN" Then
                    dCValue = 0
                End If
                If dPvalue.ToString = "NaN" Then
                    dPvalue = 0
                End If
                dDiffVal = dCValue - dPvalue
                dtDetails(0)("Current_Reporting_Period") = Convert.ToDecimal(dCValue).ToString("#,##0.00")
                dtDetails(0)("Previous_reporting_period") = Convert.ToDecimal(dPvalue).ToString("#,##0.00")
                dtDetails(0)("Change") = Convert.ToDecimal(dDiffVal).ToString("#,##0.00")
            Else
                dtDetails(0)("Current_Reporting_Period") = "0.00"
                dtDetails(0)("Previous_reporting_period") = "0.00"
                dtDetails(0)("Change") = "0.00"
            End If



            ''Ratio 2
            Dim dtLongTermBorrower, dtShortTermBorrower, dtShareHolderFund As New DataTable
            Dim dCTotal1, dPTotal1, dCTotal2, dPTotal2, dDiff As Double
            dtLongTermBorrower = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 128)   'Long term borrowings
            dtShortTermBorrower = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 132)    'Short term borrowings
            dtShareHolderFund = GetHeadingAmt1(sAC, iYearID, ICustid, 4, 39)   'Shareholders funds
            dPandLamt = GetPandLFinalAmt(sAC, iYearID, ICustid)
            dPrevPandLamt = GetPandLFinalAmt(sAC, iYearID - 1, ICustid)



            If dtLongTermBorrower.Rows.Count > 0 And dtShortTermBorrower.Rows.Count > 0 Then
                If Val(dtLongTermBorrower(0)("Dc1")) <> 0 Or Val(dtShortTermBorrower(0)("DC1")) <> 0 Then
                    dCTotal1 = Val(dtLongTermBorrower(0)("Dc1") + dtShortTermBorrower(0)("DC1"))
                Else
                    dCTotal1 = 0
                End If
                If Val(dtLongTermBorrower(0)("DP1")) <> 0 Or Val(dtShortTermBorrower(0)("DP1")) <> 0 Then
                    dPTotal1 = Val(dtLongTermBorrower(0)("DP1") + dtShortTermBorrower(0)("DP1"))
                Else
                    dPTotal1 = 0
                End If
            Else

                If dtShortTermBorrower.Rows.Count < 0 Then
                    dCTotal1 = Val(dtLongTermBorrower(0)("Dc1")) + 0
                    dPTotal1 = Val(dtLongTermBorrower(0)("DP1")) + 0
                End If
                If dtLongTermBorrower.Rows.Count < 0 Then
                    dCTotal1 = 0 + dtShortTermBorrower(0)("DC1")
                    dPTotal1 = 0 + dtShortTermBorrower(0)("DP1")
                End If
            End If

            If dtShareHolderFund.Rows.Count > 0 Then

                If Val(dtShareHolderFund(0)("Dc1")) <> 0 Or dCTotal1 <> 0 Then
                    dCTotal2 = (dCTotal1) / Val(dtShareHolderFund(0)("Dc1") + dPandLamt)
                Else
                    dCTotal2 = 0
                End If
                If Val(dtShareHolderFund(0)("Dp1")) <> 0 Or dPTotal1 <> 0 Then
                    dPTotal2 = (dPTotal1) / Val(dtShareHolderFund(0)("DP1") + dPrevPandLamt)
                Else
                    dPTotal2 = 0
                End If
                dDiff = dCTotal2 - dPTotal2
            Else

                If dCTotal2.ToString = "NaN" Then
                    dCTotal2 = 0
                End If
                If dPTotal2.ToString = "NaN" Then
                    dPTotal2 = 0
                End If
                If dDiff.ToString = "NaN" Then
                    dDiff = 0
                End If
                dCTotal2 = 0 : dPTotal2 = 0 : dDiff = 0
            End If
            dtDetails(1)("Current_Reporting_Period") = Convert.ToDecimal(dCTotal2).ToString("#,##0.00")
            dtDetails(1)("Previous_reporting_period") = Convert.ToDecimal(dPTotal2).ToString("#,##0.00")
            dtDetails(1)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")


            'Ratio 3 
            Dim dtIncome, dtExpenses, dtDepreciation, dtFinancialCosts As New DataTable
            Dim dCYProfiTAmt, dPYProfiTAmt, dCYFinancialCost, dPYFinancialCost As Double

            dtIncome = GetHeadingAmt1(sAC, iYearID, ICustid, 3, 37)           'Income
            dtExpenses = GetHeadingAmt1(sAC, iYearID, ICustid, 3, 38)             'Expenses
            dtDepreciation = GetSubHeadingAmt1(sAC, iYearID, ICustid, 3, 123)    '(f) Depreciation and Amortisation Expenses
            dtFinancialCosts = GetSubHeadingAmt1(sAC, iYearID, ICustid, 3, 122)   'Financial costs
            dtLongTermBorrower = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 128)   'Long term borrowings

            'Income -  Expenses
            If dtIncome.Rows.Count > 0 And dtExpenses.Rows.Count > 0 Then
                dCYProfiTAmt = dtIncome(0)("Dc1") - dtExpenses(0)("Dc1")
                dPYProfiTAmt = dtIncome(0)("DP1") - dtExpenses(0)("DP1")
            Else
                dCYProfiTAmt = 0 : dPYProfiTAmt = 0
            End If
            ' (f) Depreciation And Amortisation Expenses
            If dtDepreciation.Rows.Count > 0 Then
                dCYProfiTAmt = dCYProfiTAmt + dtDepreciation(0)("Dc1")
                dPYProfiTAmt = dPYProfiTAmt + dtDepreciation(0)("DP1")
            End If
            'Financial costs
            If dtFinancialCosts.Rows.Count > 0 Then
                dCYProfiTAmt = dCYProfiTAmt + dtFinancialCosts(0)("Dc1")
                dPYProfiTAmt = dPYProfiTAmt + dtFinancialCosts(0)("DP1")
            End If

            'Numerator
            '
            'Financial costs
            If dtFinancialCosts.Rows.Count > 0 Then
                dCYFinancialCost = dtFinancialCosts(0)("Dc1")
                dPYFinancialCost = dtFinancialCosts(0)("DP1")
            Else
                dCYFinancialCost = 0
                dPYFinancialCost = 0
            End If

            'Long term borrowings
            If dtLongTermBorrower.Rows.Count > 0 Then
                dCYFinancialCost = dCYFinancialCost + dtLongTermBorrower(0)("Dc1")
                dPYFinancialCost = dPYFinancialCost + dtLongTermBorrower(0)("DP1")
            End If

            If dCYFinancialCost = 0 Then
                dCYProfiTAmt = Val(dCYProfiTAmt / dCYFinancialCost)
            Else
                dCYProfiTAmt = 0
            End If
            If dPYFinancialCost = 0 Then

                dPYProfiTAmt = Val(dPYProfiTAmt / dPYFinancialCost)
            Else
                dPYProfiTAmt = 0
            End If
            If Double.IsInfinity(dCYProfiTAmt) Then
                dCYProfiTAmt = 0
            End If
            If Double.IsInfinity(dPYProfiTAmt) Then
                dPYProfiTAmt = 0
            End If


            If dCYProfiTAmt.ToString = "NaN" Then
                dCYProfiTAmt = 0
            End If
            If dPYProfiTAmt.ToString = "NaN" Then
                dPYProfiTAmt = 0
            End If



            dDiff = dCYProfiTAmt - dPYProfiTAmt

            dtDetails(2)("Current_Reporting_Period") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
            dtDetails(2)("Previous_reporting_period") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
            dtDetails(2)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")


            ' Ratio 4
            Dim dtCyShareFund, dtPYSHareFund As New DataTable
            Dim dCyShareAmt, dPySHareAmt As Double
            dPandLamt = GetPandLFinalAmt(sAC, iYearID, ICustid)
            dPrevPandLamt = GetPandLFinalAmt(sAC, iYearID - 1, ICustid)

            dtCyShareFund = GetHeadingAmt1(sAC, iYearID, ICustid, 4, 39)   'Current Yr Shareholders funds
            dtPYSHareFund = GetHeadingAmt1(sAC, iYearID - 1, ICustid, 4, 39)   'Previous Yr Shareholders funds

            If dtCyShareFund.Rows.Count > 0 And dtPYSHareFund.Rows.Count > 0 Then
                dCyShareAmt = Val(dtCyShareFund(0)("Dc1") + dPandLamt) + Val(dtCyShareFund(0)("Dp1") + dPrevPandLamt)
                dPySHareAmt = Val(dtPYSHareFund(0)("Dc1") + dPrevPandLamt) + Val(dtPYSHareFund(0)("DP1") + 0)
                dCyShareAmt = dCyShareAmt / 2
                dPySHareAmt = dCyShareAmt / 2
                If dCyShareAmt <> 0 Then
                    dCyShareAmt = dPandLamt / dCyShareAmt
                End If
                If dPySHareAmt <> 0 Or dPrevPandLamt <> 0 Then
                    dPySHareAmt = dPySHareAmt / dPrevPandLamt
                End If
            Else
                dCyShareAmt = 0.0 : dPySHareAmt = 0.0
            End If
            If dCyShareAmt.ToString = "NaN" Then
                dCyShareAmt = 0
            End If
            If dPySHareAmt.ToString = "NaN" Then
                dPySHareAmt = 0
            End If
            If Double.IsInfinity(dPySHareAmt) Then
                dPySHareAmt = 0
            End If

            dDiff = dCyShareAmt - dPySHareAmt
            dtDetails(3)("Current_Reporting_Period") = Convert.ToDecimal(dCyShareAmt).ToString("#,##0.00")
            dtDetails(3)("Previous_reporting_period") = Convert.ToDecimal(dPySHareAmt).ToString("#,##0.00")
            dtDetails(3)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")

            ' Ratio 5
            Dim dtCostSales, dtCYInventories, dtPYInventories As New DataTable
            Dim dCYInventories, dPYInventories As Double
            dtCostSales = GetSubHeadingAmt1(sAC, iYearID, ICustid, 3, 38) ' Cost sales
            dtCYInventories = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 145)  'Curent Yr Inventories
            dtPYInventories = GetSubHeadingAmt1(sAC, iYearID - 1, ICustid, 4, 145)   'Previous Yr Inventories

            If dtCYInventories.Rows.Count > 0 And dtPYInventories.Rows.Count > 0 Then
                dCYInventories = (Val(dtCYInventories(0)("Dc1") + dtPYInventories(0)("DC1")) / 2)
                dPYInventories = (Val(dtCYInventories(0)("Dp1") + dtPYInventories(0)("Dp1")) / 2)
                If dCYInventories < 0 Then


                    dCYInventories = (Val(dtCostSales(0)("Dc1"))) / dCYInventories
                    If dPYInventories <> 0 Then
                        dPYInventories = dPYInventories / (Val(dtCostSales(0)("Dp1")))
                    End If
                Else
                    dCYInventories = 0
                    dPYInventories = 0
                End If

            End If

            If dCYInventories.ToString = "NaN" Then
                dCYInventories = 0
            End If
            If dPYInventories.ToString = "NaN" Then
                dPYInventories = 0
            End If


            dDiff = dCYInventories - dPYInventories
            dtDetails(4)("Current_Reporting_Period") = Convert.ToDecimal(dCYInventories).ToString("#,##0.00")
            dtDetails(4)("Previous_reporting_period") = Convert.ToDecimal(dPYInventories).ToString("#,##0.00")
            dtDetails(4)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")

            'Ratio 6

            Dim dtRevenue, dtCYTradeRecivable, dtPYTradeRecivable As New DataTable
            Dim dCYTradeRecivable, dPYTradeRecivable, dCYRevenue, dPYRevenue As Double

            dtRevenue = GetSubHeadingAmt1(sAC, iYearID, ICustid, 3, 117) ' Revenue from operations
            dtCYTradeRecivable = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 146)  'Curent Yr  Trade Receivable
            dtPYTradeRecivable = GetSubHeadingAmt1(sAC, iYearID - 1, ICustid, 4, 146)   'Previous Yr Trade receivables

            If dtCYTradeRecivable.Rows.Count > 0 And dtPYTradeRecivable.Rows.Count > 0 Then
                dCYTradeRecivable = (Val(dtCYTradeRecivable(0)("Dc1") + dtPYTradeRecivable(0)("DC1")) / 2)
                dPYTradeRecivable = (Val(dtCYTradeRecivable(0)("Dp1") + dtPYTradeRecivable(0)("Dp1")) / 2)
                dCYRevenue = Val(dtRevenue(0)("Dc1"))
                dPYRevenue = Val(dtRevenue(0)("Dp1"))

                If dCYTradeRecivable <> 0 And dCYRevenue <> 0 Then
                    dCYTradeRecivable = dCYRevenue / dCYTradeRecivable
                Else
                    dCYTradeRecivable = 0
                End If
                If dPYTradeRecivable <> 0 And dPYRevenue <> 0 Then
                    dPYTradeRecivable = dPYRevenue / dPYTradeRecivable
                Else
                    dPYTradeRecivable = 0
                End If
            Else
                dCYTradeRecivable = 0
                dPYTradeRecivable = 0
            End If
            If dCYTradeRecivable.ToString = "NaN" Then
                dCYTradeRecivable = 0
            End If
            If dPYTradeRecivable.ToString = "NaN" Then
                dPYTradeRecivable = 0
            End If
            dDiff = dCYTradeRecivable - dPYTradeRecivable
            dtDetails(5)("Current_Reporting_Period") = Convert.ToDecimal(dCYTradeRecivable).ToString("#,##0.00")
            dtDetails(5)("Previous_reporting_period") = Convert.ToDecimal(dPYTradeRecivable).ToString("#,##0.00")
            dtDetails(5)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")

            'Ratio 7

            Dim dtCostOfGoods, dtTradePayable As New DataTable
            Dim dCYTradePayable, dPYTradePayable As Double

            dtCostOfGoods = GetSubHeadingAmt1(sAC, iYearID, ICustid, 3, 38) ' Cost of Goods Sold 
            dtTradePayable = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 133)  ' Trade Payables


            If dtCostOfGoods.Rows.Count > 0 And dtTradePayable.Rows.Count > 0 Then
                If Val(dtCostOfGoods(0)("Dc1")) = 0 Then

                    dCYTradePayable = 0

                Else
                    dCYTradePayable = Val(dtCostOfGoods(0)("Dc1") / dtTradePayable(0)("DC1"))
                End If
                If dtTradePayable(0)("DC1") = 0 Then
                    dCYTradePayable = Val(dtCostOfGoods(0)("Dc1"))
                End If

                If Val(dtCostOfGoods(0)("Dp1")) = 0 Then
                    dPYTradePayable = dtTradePayable(0)("Dp1")
                End If

                If Val(dtTradePayable(0)("Dp1")) = 0 Then
                    dPYTradePayable = dtCostOfGoods(0)("Dp1")
                Else
                    dPYTradePayable = Val(dtCostOfGoods(0)("Dp1") / dtTradePayable(0)("Dp1"))
                End If

            End If

            If dCYTradePayable.ToString = "NaN" Then
                dCYTradePayable = 0
            End If
            If dPYTradePayable.ToString = "NaN" Then
                dPYTradePayable = 0
            End If

            dDiff = dCYTradePayable - dPYTradePayable
            dtDetails(6)("Current_Reporting_Period") = Convert.ToDecimal(dCYTradePayable).ToString("#,##0.00")
            dtDetails(6)("Previous_reporting_period") = Convert.ToDecimal(dPYTradePayable).ToString("#,##0.00")
            dtDetails(6)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")

            'Ratio 8
            Dim dtCurrentAsst, dtCurrentLiablities As New DataTable
            Dim dCTotalValue, dTotalPvalue As Double
            dtCurrentAsst = GetHeadingAmt1(sAC, iYearID, ICustid, 4, 42)      'Current Liabilities
            dtCurrentLiablities = GetHeadingAmt1(sAC, iYearID, ICustid, 4, 44)  'Current assets

            If dtCurrentLiablities.Rows.Count > 0 And dtCurrentAsst.Rows.Count > 0 Then
                If Val(dtCurrentAsst(0)("Dc1")) <> 0 Or Val(dtCurrentLiablities(0)("DC1")) <> 0 Then
                    dCTotalValue = Val(dtCurrentLiablities(0)("Dc1") - dtCurrentAsst(0)("DC1"))
                Else
                    dCTotalValue = 0
                End If
                If Val(dtCurrentAsst(0)("DP1")) <> 0 Or Val(dtCurrentLiablities(0)("DP1")) <> 0 Then
                    dTotalPvalue = Val(dtCurrentLiablities(0)("DP1") - dtCurrentAsst(0)("DP1"))
                Else
                    dTotalPvalue = 0
                End If
            Else


            End If
            dtRevenue = GetSubHeadingAmt1(sAC, iYearID, ICustid, 3, 117) ' Revenue from operations
            If dtRevenue.Rows.Count > 0 Then
                If dCTotalValue <> 0 Then
                    dCTotalValue = (Val(dtRevenue(0)("DC1")) / dCTotalValue)
                End If
                If dTotalPvalue <> 0 Then
                    dTotalPvalue = (Val(dtRevenue(0)("DP1")) / dTotalPvalue)
                Else
                End If
            End If

            If dCTotalValue.ToString = "NaN" Then
                dCTotalValue = 0
            End If
            If dTotalPvalue.ToString = "NaN" Then
                dTotalPvalue = 0
            End If
            dDiff = dCTotalValue - dTotalPvalue
            dtDetails(7)("Current_Reporting_Period") = Convert.ToDecimal(dCTotalValue).ToString("#,##0.00")
            dtDetails(7)("Previous_reporting_period") = Convert.ToDecimal(dTotalPvalue).ToString("#,##0.00")
            dtDetails(7)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")


            'Ratio 9
            Dim dCYTotal9, dPYToal9 As Double
            dPandLamt = GetPandLFinalAmt(sAC, iYearID, ICustid)
            dPrevPandLamt = GetPandLFinalAmt(sAC, iYearID - 1, ICustid)
            dtRevenue = GetSubHeadingAmt1(sAC, iYearID, ICustid, 3, 117) ' Revenue from operations

            If dtRevenue.Rows.Count > 0 Then
                dCYTotal9 = dPandLamt / dtRevenue(0)("DC1")
                If dPrevPandLamt <> 0 Then
                    dPYToal9 = dPrevPandLamt / dtRevenue(0)("DP1")
                End If
            End If
            If Double.IsInfinity(dCYTotal9) Then
                dCYTotal9 = 0
            End If
            If dCYTotal9.ToString = "NaN" Then
                dCYTotal9 = 0
            End If
            If dPYToal9.ToString = "NaN" Then
                dPYToal9 = 0
            End If
            dDiff = dCYTotal9 - dPYToal9
            dtDetails(8)("Current_Reporting_Period") = Convert.ToDecimal(dCYTotal9).ToString("#,##0.00")
            dtDetails(8)("Previous_reporting_period") = Convert.ToDecimal(dPYToal9).ToString("#,##0.00")
            dtDetails(8)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")

            'Ratio 10
            dtIncome = GetHeadingAmt1(sAC, iYearID, ICustid, 3, 37)           'Income
            dtExpenses = GetHeadingAmt1(sAC, iYearID, ICustid, 3, 38)             'Expenses
            dtFinancialCosts = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 128)   'Financial costs

            'Income -  Expenses
            If dtIncome.Rows.Count > 0 And dtExpenses.Rows.Count > 0 Then
                dCYProfiTAmt = dtIncome(0)("Dc1") - dtExpenses(0)("Dc1")
                dPYProfiTAmt = dtIncome(0)("DP1") - dtExpenses(0)("DP1")
            Else
                dCYProfiTAmt = 0 : dPYProfiTAmt = 0
            End If
            'Financial costs
            'If dtFinancialCosts.Rows.Count > 0 Then
            '    dCYProfiTAmt = dCYProfiTAmt + dtFinancialCosts(0)("Dc1")
            '    dPYProfiTAmt = dPYProfiTAmt + dtFinancialCosts(0)("DP1")
            'End If

            Dim dtShareFund, dtLongTermBorro As New DataTable
            dPandLamt = GetPandLFinalAmt(sAC, iYearID, ICustid)
            dPrevPandLamt = GetPandLFinalAmt(sAC, iYearID - 1, ICustid)

            Dim dCYTotalAmt10, dPYTotalAmt10 As Double
            dtShareFund = GetHeadingAmt1(sAC, iYearID, ICustid, 4, 39)   ' Shareholders funds
            dtLongTermBorro = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 128)   'Long term borrowings

            If dtShareFund.Rows.Count > 0 Then
                dCYTotalAmt10 = (dtShareFund(0)("Dc1") + dPandLamt)
                dPYTotalAmt10 = (dtShareFund(0)("DP1") + dPrevPandLamt)

            Else
                dPYTotalAmt10 = 0
            End If

            If dtLongTermBorro.Rows.Count > 0 Then
                dCYTotalAmt10 = dtLongTermBorro(0)("Dc1") + dCYTotalAmt10
            Else
                dCYTotalAmt10 = dCYTotalAmt10
            End If

            If dCYTotalAmt10 > 0 Then
                dCYTotalAmt10 = dCYProfiTAmt / dCYTotalAmt10
                If dPYProfiTAmt <> 0 Then
                    dPYTotalAmt10 = dPYProfiTAmt / dPYTotalAmt10
                Else
                    dPYTotalAmt10 = 0
                End If
            Else
                dPYTotalAmt10 = 0
            End If
            If dCYTotalAmt10.ToString = "NaN" Then
                dCYTotalAmt10 = 0
            End If
            If dPYTotalAmt10.ToString = "NaN" Then
                dPYTotalAmt10 = 0
            End If

            If Double.IsInfinity(dCYTotalAmt10) Then
                dCYTotalAmt10 = 0
            End If

            dDiff = dCYTotalAmt10 - dPYTotalAmt10
            dtDetails(9)("Current_Reporting_Period") = Convert.ToDecimal(dCYTotalAmt10).ToString("#,##0.00")
            dtDetails(9)("Previous_reporting_period") = Convert.ToDecimal(dPYTotalAmt10).ToString("#,##0.00")
            dtDetails(9)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")

            'Ratio 11
            Dim dtNonInv, dtCurrInv As New DataTable
            Dim dCYTotal11, dPYTotal11 As Double
            dPandLamt = GetPandLFinalAmt(sAC, iYearID, ICustid)
            dPrevPandLamt = GetPandLFinalAmt(sAC, iYearID - 1, ICustid)
            dtNonInv = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 144)   'Non-Current Investments
            dtCurrInv = GetSubHeadingAmt1(sAC, iYearID, ICustid, 4, 140) 'Current Investments

            If dtNonInv.Rows.Count > 0 And dtCurrInv.Rows.Count > 0 Then
                dCYTotal11 = dtRevenue(0)("DC1") / dPandLamt
                dPYTotal11 = dtRevenue(0)("DP1") / dPrevPandLamt
            End If



            If dCYTotal11 <> 0 Then
                dCYTotal11 = dPandLamt / dCYTotal11
                dCYTotal11 = dPandLamt / dCYTotal11

            Else
                    dCYTotal11 = 0
                End If
                If dPYTotal11 <> 0 Then
                dPYTotal11 = dPrevPandLamt / dPYTotal11
                dPYTotal11 = dPrevPandLamt / dPYTotal11
            Else
                End If

            If dCYTotal11.ToString = "NaN" Then
                dCYTotal11 = 0
            End If
            If dPYTotal11.ToString = "NaN" Then
                dPYTotal11 = 0
            End If
            dDiff = dCYTotal11 - dPYTotal11
            dtDetails(10)("Current_Reporting_Period") = Convert.ToDecimal(dCYTotal11).ToString("#,##0.00")
            dtDetails(10)("Previous_reporting_period") = Convert.ToDecimal(dPYTotal11).ToString("#,##0.00")
            dtDetails(10)("Change") = Convert.ToDecimal(dDiff).ToString("#,##0.00")

            Return dtDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHeadingAmt1(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal iSchedType As Integer, ByVal iHeadingId As Integer) As DataTable
        Dim sSQL As String = ""
        Dim dTotalDieselAmount As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "          Select (ABS( ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0)  -  "
            sSQL = sSQL & "      ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0))) AS Dc1  , "
            sSQL = sSQL & "      (ABS (ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0)  - "
            sSQL = sSQL & "     ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0)))  As DP1  "
            sSQL = sSQL & "        From Acc_TrailBalance_Upload_Details "
            sSQL = sSQL & "    left join ACC_ScheduleHeading a on a.ASH_ID= ATBUD_headingid "
            sSQL = sSQL & "    Left Join Acc_TrailBalance_Upload d on d.ATBU_Description = ATBUD_Description  "
            sSQL = sSQL & " And d.ATBU_YEARId=" & iYearID & " And d.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID & "  "
            sSQL = sSQL & "    Left Join Acc_TrailBalance_Upload e on e.ATBU_Description = ATBUD_Description "
            sSQL = sSQL & " And e.ATBU_YEARId=" & iYearID - 1 & " And e.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID - 1 & "   "
            sSQL = sSQL & "    where ATBUD_Schedule_type = " & iSchedType & "  And ATBUD_CustId = " & iCustomerID & "  And ATBUD_Headingid = " & iHeadingId & " "
            sSQL = sSQL & "    group by ATBUD_Headingid order by ATBUD_Headingid "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSQL)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetHeadingAmt2(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal iSchedType As Integer, ByVal iHeadingId As Integer) As DataTable
    '    Dim sSQL As String = ""
    '    Dim dTotalDieselAmount As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSQL = "         select  ( ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount+0),0) -  "
    '        sSQL = sSQL & " ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount+0),0) ) as NC1,  "
    '        sSQL = sSQL & " (ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount+0),0) - "
    '        sSQL = sSQL & " ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount+0),0))  as NP1   "
    '        sSQL = sSQL & " from Acc_TrailBalance_Upload_Details  "
    '        sSQL = sSQL & " left join ACC_ScheduleSubHeading a on a.ASSH_ID= ATBUD_Subheading "
    '        sSQL = sSQL & " left join Acc_TrailBalance_Upload d on d.ATBU_Description = ATBUD_Description And d.ATBU_YEARId=" & iYearID & "  "
    '        sSQL = sSQL & " and d.ATBU_CustId= " & iCustomerID & " and ATBUD_YEARId =" & iYearID & "  "
    '        sSQL = sSQL & " left join Acc_TrailBalance_Upload e on e.ATBU_Description = ATBUD_Description And e.ATBU_YEARId=" & iYearID - 1 & " "
    '        sSQL = sSQL & " and e.ATBU_CustId= " & iCustomerID & " and ATBUD_YEARId =" & iYearID - 1 & "    "
    '        sSQL = sSQL & " where  ATBUD_Schedule_type =" & iSchedType & "  And ATBUD_CustId=" & iCustomerID & " and ATBUD_Headingid=" & iHeadingId & " "
    '        sSQL = sSQL & "  group by ATBUD_Headingid order by ATBUD_Headingid "
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSQL)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetSubHeadingAmt1(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal iSchedType As Integer, ByVal iHeadingId As Integer) As DataTable
        Dim sSQL As String = ""
        Dim dTotalDieselAmount As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "          Select (ABS( ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0)  -  "
            sSQL = sSQL & "      ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0))) AS Dc1  , "
            sSQL = sSQL & "       (ABS(ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0)  - "
            sSQL = sSQL & "     ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0)))  As DP1  "
            sSQL = sSQL & "        From Acc_TrailBalance_Upload_Details "
            sSQL = sSQL & "   left join ACC_ScheduleSubHeading a on a.ASSH_ID= ATBUD_Subheading  "
            sSQL = sSQL & "    Left Join Acc_TrailBalance_Upload d on d.ATBU_Description = ATBUD_Description  "
            sSQL = sSQL & " And d.ATBU_YEARId=" & iYearID & " And d.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID & "  "
            sSQL = sSQL & "    Left Join Acc_TrailBalance_Upload e on e.ATBU_Description = ATBUD_Description "
            sSQL = sSQL & " And e.ATBU_YEARId=" & iYearID - 1 & " And e.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID - 1 & "   "
            sSQL = sSQL & "    where ATBUD_Schedule_type = " & iSchedType & "  And ATBUD_CustId = " & iCustomerID & "  And ATBUD_Subheading = " & iHeadingId & " "
            sSQL = sSQL & "    group by ATBUD_Headingid order by ATBUD_Headingid "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSQL)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function GetSubHeadingAmt2(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal iSchedType As Integer, ByVal iHeadingId As Integer) As Double
    '    Dim sSQL As String = ""
    '    Dim dTotalDieselAmount As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSQL = "          Select ( ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0)  -  "
    '        sSQL = sSQL & "      ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0)) AS Dc1  , "
    '        sSQL = sSQL & "       (ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0)  - "
    '        sSQL = sSQL & "     ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0))  As DP1  "
    '        sSQL = sSQL & "        From Acc_TrailBalance_Upload_Details "
    '        sSQL = sSQL & "    left join ACC_ScheduleSubHeading a on a.ASSH_ID= ATBUD_Subheading "
    '        sSQL = sSQL & "    Left Join Acc_TrailBalance_Upload d on d.ATBU_Description = ATBUD_Description  "
    '        sSQL = sSQL & " And d.ATBU_YEARId=" & iYearID & " And d.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID & "  "
    '        sSQL = sSQL & "    Left Join Acc_TrailBalance_Upload e on e.ATBU_Description = ATBUD_Description "
    '        sSQL = sSQL & " And e.ATBU_YEARId=" & iYearID - 1 & " And e.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID - 1 & "   "
    '        sSQL = sSQL & "    where ATBUD_Schedule_type = " & iSchedType & "  And ATBUD_CustId = " & iCustomerID & "  And ATBUD_Headingid = " & iHeadingId & " "
    '        sSQL = sSQL & "    group by ATBUD_Headingid order by ATBUD_Headingid "
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSQL)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetPandLFinalAmt(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iCustomerID As Integer) As Double
        Dim sSQL As String = ""
        Dim dTotalDieselAmount As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "Select  abs(Acc_PnL_Amount) as Acc_PnL_Amount from Acc_ProfitAndLossAmount where Acc_PnL_Custid =" & iCustomerID & " And Acc_PnL_Yearid=" & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Acc_PnL_Amount").ToString()) = False Then
                    dTotalDieselAmount = dt.Rows(0)("Acc_PnL_Amount").ToString()
                Else
                    dTotalDieselAmount = 0.0
                End If
            Else
                dTotalDieselAmount = 0.0
            End If
            If Val(dTotalDieselAmount) = 0 Then
                dTotalDieselAmount = 0.0
            End If
            Return dTotalDieselAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRatioFormula(ByVal sAC As String, ByVal ICustid As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable, dtTable As New DataTable
        Dim i As Integer, iSlNo As Integer = 0
        Try
            dtDetails.Columns.Add("Ratios")
            dtDetails.Rows.Add("1.")
            dtDetails.Rows.Add("2.")
            dtDetails.Rows.Add("3.")
            dtDetails.Rows.Add("4.")
            dtDetails.Rows.Add("5.")
            dtDetails.Rows.Add("6.")
            dtDetails.Rows.Add("7.")
            dtDetails.Rows.Add("8.")
            dtDetails.Rows.Add("9.")
            dtDetails.Rows.Add("10.")
            dtDetails.Rows.Add("11.")

            dtDetails.Columns.Add("Formula")
            dtDetails(0)("Formula") = "(Current assets / Current Liabilities)"
            dtDetails(1)("Formula") = "(Long-Term Borrowings +  Short-Term borrowings) / Shareholder's Funds"
            dtDetails(2)("Formula") = "(Profit before exceptional and extraordinary items and tax ) +  ( Depreciation and amortization expense +  Financial costs)  / (Financial costs +  Long-Term Borrowings)"
            dtDetails(3)("Formula") = "(Profit/(Loss) for the period) /(Shareholder's Funds CY + Shareholder's Funds PY  /2)"
            dtDetails(4)("Formula") = "(Cost of Goods Sold ) / (( Inventories cy + Inventories PY)/2)"
            dtDetails(5)("Formula") = "Revenue from operations / (Trade Receivables CY + Trade Receivables PY /2 )"
            dtDetails(6)("Formula") = "(Cost of Goods Sold / Trade Payables )"
            dtDetails(7)("Formula") = "Revenue from operations/(sum(Current assets) - sum(Current Liabilities)"
            dtDetails(8)("Formula") = "Profit/(Loss) for the period / Revenue from operations"
            dtDetails(9)("Formula") = "(Profit before exceptional and extraordinary items and tax +Financial costs) / (Shareholder's Funds+Long-Term Borrowings)"
            dtDetails(10)("Formula") = "(Profit/(Loss) for the period  /  (Non-Current Investments + Current Investments)"
            Return dtDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
