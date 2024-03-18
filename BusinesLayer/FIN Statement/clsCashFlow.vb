Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsCashFlow
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral


    Private ACF_pkid As Integer
    Private ACF_Description As String
    Private ACF_Custid As Integer
    Private ACF_Branchid As Integer
    Private ACF_Current_Amount As Double
    Private ACF_Prev_Amount As Double
    Private ACF_Status As String
    Private ACF_Crby As Integer
    Private ACF_Updatedby As Integer
    Private ACF_Compid As Integer
    Private ACF_Ipaddress As String
    Private ACF_Catagary As Integer
    Private ACF_Yearid As Integer

    Public Property iACF_pkid() As Integer
        Get
            Return (ACF_pkid)
        End Get
        Set(ByVal Value As Integer)
            ACF_pkid = Value
        End Set
    End Property
    Public Property sACF_Description() As String
        Get
            Return (ACF_Description)
        End Get
        Set(ByVal Value As String)
            ACF_Description = Value
        End Set
    End Property
    Public Property iACF_Custid() As Integer
        Get
            Return (ACF_Custid)
        End Get
        Set(ByVal Value As Integer)
            ACF_Custid = Value
        End Set
    End Property
    Public Property iACF_Branchid() As Integer
        Get
            Return (ACF_Branchid)
        End Get
        Set(ByVal Value As Integer)
            ACF_Branchid = Value
        End Set
    End Property
    Public Property dACF_Current_Amount() As Double
        Get
            Return (ACF_Current_Amount)
        End Get
        Set(ByVal Value As Double)
            ACF_Current_Amount = Value
        End Set
    End Property
    Public Property dACF_Prev_Amount() As Double
        Get
            Return (ACF_Prev_Amount)
        End Get
        Set(ByVal Value As Double)
            ACF_Prev_Amount = Value
        End Set
    End Property
    Public Property sACF_Status() As String
        Get
            Return (ACF_Status)
        End Get
        Set(ByVal Value As String)
            ACF_Status = Value
        End Set
    End Property
    Public Property iACF_Crby() As Integer
        Get
            Return (ACF_Crby)
        End Get
        Set(ByVal Value As Integer)
            ACF_Crby = Value
        End Set
    End Property

    Public Property iACF_Updatedby() As Integer
        Get
            Return (ACF_Updatedby)
        End Get
        Set(ByVal Value As Integer)
            ACF_Updatedby = Value
        End Set
    End Property

    Public Property iACF_Compid() As Integer
        Get
            Return (ACF_Compid)
        End Get
        Set(ByVal Value As Integer)
            ACF_Compid = Value
        End Set
    End Property
    Public Property sACF_Ipaddress() As String
        Get
            Return (ACF_Ipaddress)
        End Get
        Set(ByVal Value As String)
            ACF_Ipaddress = Value
        End Set
    End Property
    Public Property iACF_Catagary() As Integer
        Get
            Return (ACF_Catagary)
        End Get
        Set(ByVal Value As Integer)
            ACF_Catagary = Value
        End Set
    End Property

    Public Property iACF_Yearid() As Integer
        Get
            Return (ACF_Yearid)
        End Get
        Set(ByVal Value As Integer)
            ACF_Yearid = Value
        End Set
    End Property

    Public Function SaveCashFlow(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objHeading As clsCashFlow) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_pkid", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_pkid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Description", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objHeading.sACF_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Custid", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_Custid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Branchid", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_Branchid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Current_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objHeading.dACF_Current_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Prev_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objHeading.dACF_Prev_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Status", OleDb.OleDbType.Char)
            ObjParam(iParamCount).Value = objHeading.sACF_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Crby", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_Crby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Updatedby", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_Updatedby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Compid", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_Compid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Ipaddress", OleDb.OleDbType.VarChar)
            ObjParam(iParamCount).Value = objHeading.sACF_Ipaddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Catagary", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_Catagary
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACF_Catagary", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objHeading.iACF_Yearid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Cashflow", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getCashFlowDetails(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal sBranchid As String, ByVal ACFCatagary As Integer, ByVal iYearid As Integer) As DataTable
        Dim sSql As String
        Dim Cash As DataTable
        Dim dt As New DataTable
        Dim dtDetails As New DataTable
        Dim drow As DataRow
        Dim TotalCurrent As Double = 0
        Dim Totalprev As Double = 0
        Dim CurrentAmmount As Decimal = 0
        Dim TotalprevPandl As Decimal = 0
        Dim dtIncome, dtExpenses, dtDepreciation, dtFinancialCosts As New DataTable
        Dim dCYProfiTAmt As Double = 0
        Dim dPYProfiTAmt As Double = 0
        Dim dCYFinancialCost As Double = 0
        Dim dPYFinancialCost As Double = 0
        Dim Totalcurrent5a As Double = 0
        Dim TotalPrev5a As Double = 0
        Dim Totalcurrent5b As Double = 0
        Dim TotalPrev5b As Double = 0
        Try

            dtDetails.Columns.Add("SrNo")
            dtDetails.Columns.Add("ACF_pkid")
            dtDetails.Columns.Add("Particulers")
            dtDetails.Columns.Add("CurrentAmmount1", System.Type.GetType("System.Double"))
            dtDetails.Columns.Add("PreviesAmount1", System.Type.GetType("System.Double"))
            dtDetails.Columns.Add("CurrentAmmount")
            dtDetails.Columns.Add("PreviesAmount")
            If ACFCatagary = 1 Then
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                'dtDetails.Rows.Add("0")
                dtDetails.Rows(0)("Particulers") = ("A.Cash flow from operating activities")
                dtDetails.Rows(1)("Particulers") = ("Net Profit / (Loss) before extraordinary items and tax")
                dtDetails.Rows(2)("Particulers") = ("Adjustment for:")
                dtDetails.Rows(3)("Particulers") = ("Depreciation and amortisation")
                dtDetails.Rows(4)("Particulers") = ("Provision for impairment of fixed assets and intangibles")
                dtDetails.Rows(5)("Particulers") = ("Bad Debts")
                dtDetails.Rows(6)("Particulers") = ("Expense on employee stock option scheme")
                'dtDetails.Rows(6)("Particulers") = ("Income Tax Refund Received")
                'dtDetails.Rows(7)("Particulers") = ("Preliminary Expenses written off")
                dtDetails.Rows(7)("Particulers") = ("Finance Costs")
                'dtDetails.Rows(8)("Particulers") = ("Interest income")

                For i = 0 To dtDetails.Rows.Count - 1
                    If dtDetails.Rows.Count > 0 Then
                        dtDetails.Rows(i)("SrNo") = i + 1
                        If dtDetails.Rows(i)("Particulers") = "A.Cash flow from operating activities" Then
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(0).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(0).ToString("#,##0.00")
                            dtDetails.Rows(i)("CurrentAmmount1") = 0
                            dtDetails.Rows(i)("PreviesAmount1") = 0
                        ElseIf dtDetails.Rows(i)("Particulers") = "Net Profit / (Loss) before extraordinary items and tax" Then 'V Profit before exceptional and extraordinary iteam and tax
                            dtIncome = GetHeadingAmt1(sAC, iYearid, ICustid, sBranchid, 3, 37)           'Income
                            dtExpenses = GetHeadingAmt1(sAC, iYearid, ICustid, sBranchid, 3, 38)             'Expenses
                            'Income -  Expenses
                            If dtIncome.Rows.Count > 0 Or dtExpenses.Rows.Count > 0 Then
                                dCYProfiTAmt = dtIncome(0)("Dc1") - dtExpenses(0)("Dc1")
                                dPYProfiTAmt = dtIncome(0)("DP1") - dtExpenses(0)("DP1")
                            Else
                                dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            End If
                            dtDetails.Rows(i)("CurrentAmmount1") = dCYProfiTAmt
                            dtDetails.Rows(i)("PreviesAmount1") = dPYProfiTAmt
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf dtDetails.Rows(i)("Particulers") = "Adjustment for:" Then
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(0).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(0).ToString("#,##0.00")
                            dtDetails.Rows(i)("CurrentAmmount1") = 0
                            dtDetails.Rows(i)("PreviesAmount1") = 0
                            'ElseIf dtDetails.Rows(i)("Particulers") = "Interest income" Then
                            '    dtIncome = GetItemAmt1(sAC, iCompID, ICustid, iYearid, 3, 443)  'Interest Income Received (Item)
                            '    dtExpenses = GetItemAmt1(sAC, iCompID, ICustid, iYearid - 1, 3, 443)
                            '    If dtIncome.Rows.Count > 0 Then
                            '        dCYProfiTAmt = dtIncome(0)("Dc1")
                            '        dPYProfiTAmt = dtExpenses(0)("Dc1")
                            '    Else
                            '        dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            '    End If
                            '    dtDetails.Rows(i)("CurrentAmmount1") = dCYProfiTAmt
                            '    dtDetails.Rows(i)("PreviesAmount1") = dPYProfiTAmt
                            '    dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            '    dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            'ElseIf dtDetails.Rows(i)("Particulers") = "Provision for Income Tax" Then
                            '    dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid, 3, 150)  '(a) Current Tax 
                            '    dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid - 1, 3, 150)
                            '    If dtIncome.Rows.Count > 0 Then
                            '        dCYProfiTAmt = dtIncome(0)("Dc1")
                            '        dPYProfiTAmt = dtExpenses(0)("Dc1")
                            '    Else
                            '        dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            '    End If
                            '    dtDetails.Rows(i)("CurrentAmmount") = dCYProfiTAmt
                            '    dtDetails.Rows(i)("PreviesAmount") = dPYProfiTAmt
                            '    'ElseIf dtDetails.Rows(i)("Particulers") = "Effect on exchange rate changes" Then
                            '    '    dtDetails.Rows(i)("CurrentAmmount") = 0
                            '    '    dtDetails.Rows(i)("PreviesAmount") = 0
                            '    'ElseIf dtDetails.Rows(i)("Particulers") = "Income Tax Refund Received" Then
                            '    '    dtDetails.Rows(i)("CurrentAmmount") = 0
                            '    '    dtDetails.Rows(i)("PreviesAmount") = 0
                            '    'ElseIf dtDetails.Rows(i)("Particulers") = "Preliminary Expenses written off" Then
                            '    '    dtDetails.Rows(i)("CurrentAmmount") = 0
                            '    '    dtDetails.Rows(i)("PreviesAmount") = 0
                        ElseIf dtDetails.Rows(i)("Particulers") = "Depreciation and amortisation" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 3, 123)  '(f) Depreciation and amortisation expenses
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 3, 123)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    sSql = " select ABS(" & dtIncome(0)("Dc1") & ")"
                                    dtIncome(0)("Dc1") = objDBL.SQLExecuteScalar(sAC, sSql)
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    sSql = " select ABS(" & dtExpenses(0)("Dc1") & ")"
                                    dtExpenses(0)("Dc1") = objDBL.SQLExecuteScalar(sAC, sSql)
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If

                            dtDetails.Rows(i)("CurrentAmmount1") = dCYProfiTAmt
                            dtDetails.Rows(i)("PreviesAmount1") = dPYProfiTAmt
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf dtDetails.Rows(i)("Particulers") = "Finance Costs" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 3, 122)  '(e) Finance costs
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 3, 122)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            If Val(dCYProfiTAmt) < 0 Then
                                dCYProfiTAmt = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00").Remove(0, 1)
                            End If
                            If Val(dPYProfiTAmt) < 0 Then
                                dPYProfiTAmt = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00").Remove(0, 1)
                            End If
                            dtDetails.Rows(i)("CurrentAmmount1") = dCYProfiTAmt
                            dtDetails.Rows(i)("PreviesAmount1") = dPYProfiTAmt
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        End If
                    End If
                Next
            ElseIf ACFCatagary = 2 Or ACFCatagary = 5 Then
                sSql = "select  ROW_NUMBER() OVER (ORDER BY ACF_pkid ASC) AS SrNo,ACF_pkid,ACF_Description as Particulers,CAST(ACF_Prev_Amount AS DECIMAL) as PreviesAmount,CAST(ACF_Current_Amount AS DECIMAL) as  CurrentAmmount from Acc_Cashflow where (ACF_Custid= " & ICustid & " Or ACF_Custid=0) And ACF_Catagary=" & ACFCatagary & ""
                Cash = objDBL.SQLExecuteDataTable(sAC, sSql)
                If ACFCatagary = 5 Then
                    For i = 0 To Cash.Rows.Count - 1

                        If Cash.Rows(i)("Particulers") = "Cash and cash equivalents at begining of the year" Then
                            dtIncome = getcashEquivalentPy(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 147)  ' (d) Cash and cash equivalents Begining of the Year 
                            dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 147)
                            If dtIncome.Rows.Count > 0 Then
                                dCYProfiTAmt = dtIncome(0)("Dc1")
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                dPYProfiTAmt = dtExpenses(0)("Dc1")
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            'ElseIf Cash.Rows(i)("Particulers") = "Cash and cash equivalents at Closing of the year" Then
                            'dtIncome = getcashEquivalentCY(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)  '(d) Cash and cash equivalents Closing of the Year
                            'dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)
                            'If dtIncome.Rows.Count > 0 Then
                            '    dCYProfiTAmt = dtIncome(0)("Dc1")
                            '    dPYProfiTAmt = dtExpenses(0)("Dc1")
                            'Else
                            '    dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            'End If
                            'Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            '    Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf Cash.Rows(i)("Particulers") = "Cash and cash equivalents as per Balance Sheet" Then
                            dtIncome = getcashEquivalentCY(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 147)  '(d) Cash and cash equivalents Closing of the Year
                            dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 147)
                            If dtIncome.Rows.Count > 0 Then
                                dCYProfiTAmt = dtIncome(0)("Dc1")
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                dPYProfiTAmt = dtExpenses(0)("Dc1")
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf Cash.Rows(i)("Particulers") = "(a) Cash on hand" Then 'Item
                            dtIncome = GetItemAmtCY(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 354)  'A) Cash In Hand 
                            dtExpenses = GetItemAmtpy(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 354)
                            If dtIncome.Rows.Count > 0 Then
                                sSql = " select ABS(" & dtIncome(0)("Dc1") & ")"
                                dtIncome(0)("Dc1") = objDBL.SQLExecuteScalar(sAC, sSql)
                                dCYProfiTAmt = dtIncome(0)("Dc1")
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                sSql = " select ABS(" & dtExpenses(0)("Dc1") & ")"
                                dtExpenses(0)("Dc1") = objDBL.SQLExecuteScalar(sAC, sSql)
                                dPYProfiTAmt = dtExpenses(0)("Dc1")
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            Totalcurrent5a = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            TotalPrev5a = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf Cash.Rows(i)("Particulers") = "(b) Balances with banks - in current accounts" Then
                            dtIncome = GetItemAmtCY(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 355)  'B) Bank Balance
                            dtExpenses = GetItemAmtpy(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 355)
                            If dtIncome.Rows.Count > 0 Then
                                sSql = " select ABS(" & dtIncome(0)("Dc1") & ")"
                                dtIncome(0)("Dc1") = objDBL.SQLExecuteScalar(sAC, sSql)
                                dCYProfiTAmt = dtIncome(0)("Dc1")
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                sSql = "select ABS(" & dtExpenses(0)("Dc1") & ")"
                                dtExpenses(0)("Dc1") = objDBL.SQLExecuteScalar(sAC, sSql)
                                dPYProfiTAmt = dtExpenses(0)("Dc1")
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            Totalcurrent5b = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            TotalPrev5b = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf Cash.Rows(i)("Particulers") = "Total (a+b+c)" Then
                            Cash.Rows(i)("CurrentAmmount") = Totalcurrent5a + Totalcurrent5b
                            Cash.Rows(i)("PreviesAmount") = TotalPrev5a + TotalPrev5b
                        End If
                    Next
                    Return Cash
                End If
                For i = 0 To Cash.Rows.Count - 1
                    If Cash.Rows.Count > 0 Then
                        If Cash.Rows(i)("Particulers") = "Inventories" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 145)  '(b) Inventories
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 145)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = dCYProfiTAmt
                            Cash.Rows(i)("PreviesAmount") = dPYProfiTAmt
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Trade receivables" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 146)  '(c) Trade receivables
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 146)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Short-term loans and advances" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 148)  '(e) Short-term loans and advances
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 148)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Long-term loans and advances" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 142)  '(d) Long term loans and Advances
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 142)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = dCYProfiTAmt
                            Cash.Rows(i)("PreviesAmount") = dPYProfiTAmt
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Other current Assets" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 149)  '(f) Other Current Assets
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 149)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Adjustments for increase / (decrease) in operating liabilities:" Then

                            'ElseIf Cash.Rows(i)("Particulers") = "Adjustments for decrease (increase) in other non current assets" Then
                            '    dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid, 4, 143)  '(e) Other Non Current Assets
                            '    dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid - 1, 4, 143)
                            '    If dtIncome.Rows.Count > 0 Then
                            '        dCYProfiTAmt = dtIncome(0)("Dc1")
                            '        dPYProfiTAmt = dtExpenses(0)("Dc1")
                            '    Else
                            '        dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            '    End If
                            '    Cash.Rows(i)("CurrentAmmount") = dCYProfiTAmt
                            '    Cash.Rows(i)("PreviesAmount") = dPYProfiTAmt
                            '    TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            '    Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Trade Payables" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 133)  '(b) Trade payables
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 133)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If

                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Other current liabilities" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 134)  '(c) Other current liabilities
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 134)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                            'ElseIf Cash.Rows(i)("Particulers") = "Short-term provisions" Then
                            '    dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid, 4, 132)  '(a) Short Term Borrowings
                            '    dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid - 1, 4, 132)
                            '    If dtIncome.Rows.Count > 0 Then
                            '        dCYProfiTAmt = dtIncome(0)("Dc1")
                            '        dPYProfiTAmt = dtExpenses(0)("Dc1")
                            '    Else
                            '        dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            '    End If
                            '    Cash.Rows(i)("CurrentAmmount") = dCYProfiTAmt
                            '    Cash.Rows(i)("PreviesAmount") = dPYProfiTAmt
                            '    TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            '    Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Short-term provisions" Then
                            dtIncome = GetSubHeadingAmtForMultipleheadings(sAC, iCompID, ICustid, sBranchid, iYearid, 4, "131,135")  '(d) Long term provision + (d) Short-term provisions
                            dtExpenses = GetSubHeadingAmtForMultipleheadings(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, "131,135")
                            'dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid, 4, 135)   '(d) Short-term provisions
                            'dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, iYearid - 1, 4, 135)
                            If dtIncome.Rows.Count > 0 Then
                                dCYProfiTAmt = dtIncome(0)("Dc1")
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                dPYProfiTAmt = dtExpenses(0)("Dc1")
                            Else
                                dPYProfiTAmt = 0
                            End If
                            Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            TotalCurrent = TotalCurrent + Cash.Rows(i)("CurrentAmmount")
                            Totalprev = Totalprev + Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Operating profit / (loss) after working capital changes" Then
                            Cash.Rows(i)("CurrentAmmount") = 0
                            Cash.Rows(i)("PreviesAmount") = 0
                        ElseIf Cash.Rows(i)("Particulers") = "Cash generated from operations" Then
                            Cash.Rows(i)("CurrentAmmount") = 0
                            Cash.Rows(i)("PreviesAmount") = 0
                        ElseIf Cash.Rows(i)("Particulers") = "Net income tax (paid) / refunds (net)" Then
                            Cash.Rows(i)("CurrentAmmount") = Cash.Rows(i)("CurrentAmmount")
                            Cash.Rows(i)("PreviesAmount") = Cash.Rows(i)("PreviesAmount")
                        ElseIf Cash.Rows(i)("Particulers") = "Net cash generated from/ (used in) operating activities" Then
                            Cash.Rows(i)("CurrentAmmount") = 0
                            Cash.Rows(i)("PreviesAmount") = 0
                        End If
                    End If
                Next
                Return Cash
            End If
            If ACFCatagary = 4 Then 'Finance Activities
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows.Add("0")
                dtDetails.Rows(0)("Particulers") = ("Proceeds from issue of equity shares")
                dtDetails.Rows(1)("Particulers") = ("Share application money received / (refunded)")
                dtDetails.Rows(2)("Particulers") = ("Increase / (Decrease) in Long Term Borrowings")
                dtDetails.Rows(3)("Particulers") = ("Increase / (Decrease) in Short Term Borrowings")
                dtDetails.Rows(4)("Particulers") = ("Interest Received on deposits/Income tax refund")
                dtDetails.Rows(5)("Particulers") = ("Insurance claims received  it refund")
                dtDetails.Rows(6)("Particulers") = ("Dividend Income")
                dtDetails.Rows(7)("Particulers") = ("Finance costs")
                For i = 0 To dtDetails.Rows.Count - 1
                    If dtDetails.Rows.Count > 0 Then
                        dtDetails.Rows(i)("SrNo") = i + 1
                        If dtDetails.Rows(i)("Particulers") = "Increase / (Decrease) in Long Term Borrowings" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 128)  '(a) Long-term borrowings (Financing Activities)
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 128)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            dtDetails.Rows(i)("CurrentAmmount1") = dCYProfiTAmt
                            dtDetails.Rows(i)("PreviesAmount1") = dPYProfiTAmt
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf dtDetails.Rows(i)("Particulers") = "Increase / (Decrease) in Short Term Borrowings" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 4, 132)  '(a) Short Term Borrowings
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 4, 132)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            dtDetails.Rows(i)("CurrentAmmount1") = dCYProfiTAmt
                            dtDetails.Rows(i)("PreviesAmount1") = dPYProfiTAmt
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        ElseIf dtDetails.Rows(i)("Particulers") = "Finance costs" Then
                            dtIncome = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid, 3, 122)  '(e) Finance costs
                            dtExpenses = GetSubHeadingAmt1(sAC, iCompID, ICustid, sBranchid, iYearid - 1, 3, 122)
                            If dtIncome.Rows.Count > 0 Then
                                If IsDBNull(dtIncome(0)("Dc1")) = False Then
                                    dCYProfiTAmt = dtIncome(0)("Dc1")
                                Else
                                    dCYProfiTAmt = 0
                                End If
                            Else
                                dCYProfiTAmt = 0
                            End If
                            If dtExpenses.Rows.Count > 0 Then
                                If IsDBNull(dtExpenses(0)("Dc1")) = False Then
                                    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                Else
                                    dPYProfiTAmt = 0
                                End If
                            Else
                                dPYProfiTAmt = 0
                            End If
                            dtDetails.Rows(i)("CurrentAmmount1") = dCYProfiTAmt
                            dtDetails.Rows(i)("PreviesAmount1") = dPYProfiTAmt
                            dtDetails.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            dtDetails.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            'ElseIf dtDetails.Rows(i)("Particulers") = "Increase / (Decrease) in Long Term Provisions" Then
                            '    'dtIncome = GetSubHeadingAmtForMultipleheadings(sAC, iCompID, ICustid, iYearid, 4, "131,135")  '(d) Long term provision + (d) Short-term provisions
                            '    'dtExpenses = GetSubHeadingAmtForMultipleheadings(sAC, iCompID, ICustid, iYearid - 1, 4, "131,135")
                            '    'If dtIncome.Rows.Count > 0 Then
                            '    '    dCYProfiTAmt = dtIncome(0)("Dc1")
                            '    '    dPYProfiTAmt = dtExpenses(0)("Dc1")
                            '    'Else
                            '    '    dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            '    'End If
                            '    'dtDetails.Rows(i)("CurrentAmmount") = dCYProfiTAmt
                            '    'dtDetails.Rows(i)("PreviesAmount") = dPYProfiTAmt
                        End If
                    End If
                Next
            End If
            'sSql = " select b.ACF_pkid as ACF_pkid, b.ACF_Description Particulers, sum(b.ACF_Current_Amount)  CurrentAmmount,Sum(c.ACF_Current_Amount)  PreviesAmount from  Acc_Cashflow b "
            'sSql = sSql & " left join Acc_Cashflow c on c.ACF_pkid=b.ACF_pkid and c.ACF_yearid=" & iYearid - 1 & " and c.ACF_Prev_Amount is not null"
            'sSql = sSql & " where b.ACF_Custid= " & ICustid & " And b.ACF_Catagary=" & ACFCatagary & " and b.ACF_yearid=" & iYearid & ""
            sSql = ""
            sSql = "select a.ACF_Description Particulers, sum(b.ACF_Current_Amount)  PreviesAmount,"
            sSql = sSql & " Sum(c.ACF_Current_Amount)  CurrentAmmount from Acc_Cashflow a"
            sSql = sSql & " left join Acc_Cashflow b on b.ACF_pkid=a.ACF_pkid and b.ACF_yearid=" & iYearid - 1 & ""
            sSql = sSql & " left join Acc_Cashflow c on c.ACF_pkid=a.ACF_pkid and c.ACF_yearid=" & iYearid & ""
            sSql = sSql & " where a.ACF_Custid= " & ICustid & " And a.ACF_Catagary=" & ACFCatagary & ""
            If sBranchid <> "" Then
                sSql = sSql & " And a.ACF_Branchid In (" & sBranchid & ")"
            End If
            sSql = sSql & " group by a.ACF_Description"
            Cash = objDBL.SQLExecuteDataTable(sAC, sSql)
            If Cash.Rows.Count > 0 Then
                For i = 0 To Cash.Rows.Count - 1
                    drow = dtDetails.NewRow()
                    drow("SrNo") = dtDetails.Rows.Count + 1
                    drow("ACF_pkid") = 0
                    drow("Particulers") = Cash(i)("Particulers")
                    If IsDBNull(Cash(i)("CurrentAmmount")) = False Then
                        drow("CurrentAmmount1") = Cash(i)("CurrentAmmount")
                    Else
                        drow("CurrentAmmount1") = 0
                    End If
                    If IsDBNull(Cash(i)("PreviesAmount")) = False Then
                        drow("PreviesAmount1") = Cash(i)("PreviesAmount")
                    Else
                        drow("PreviesAmount1") = 0
                    End If
                    If IsDBNull(Cash(i)("CurrentAmmount")) = False Then
                        drow("CurrentAmmount") = Convert.ToDecimal(Cash(i)("CurrentAmmount")).ToString("#,##0.00")
                        'Else
                        '    drow("CurrentAmmount") = 0
                    End If
                    If IsDBNull(Cash(i)("PreviesAmount")) = False Then
                        drow("PreviesAmount") = Convert.ToDecimal(Cash(i)("PreviesAmount")).ToString("#,##0.00")
                        'Else
                        '    drow("PreviesAmount") = 0
                    End If
                    dtDetails.Rows.Add(drow)
                Next
            End If
            Return dtDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Checkdata(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal sDesc As String, ByVal iYearID As Integer, ByVal IBranchid As Integer) As Integer
        Dim sSql As String
        Dim chkrec As Integer
        Try
            sSql = "select isnull(ACF_pkid,0) as ACF_pkid  from Acc_Cashflow Where ACF_Description='" & sDesc & "'"
            sSql = sSql & " And ACF_Custid=" & iCustid & " And ACF_Branchid=" & IBranchid & " And ACF_Compid = " & iACID & " And ACF_yearid = " & iYearID & " "
            chkrec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return chkrec
            'If chkrec = True Then
            '    Return 1
            'Else
            '    Return 0
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHeadingAmt1(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal sBranchid As String, ByVal iSchedType As Integer, ByVal iHeadingId As Integer) As DataTable
        Dim sSQL As String = ""
        Dim dTotalDieselAmount As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "Select ABS(ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0)  -  "
            sSQL = sSQL & "      ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0)) AS Dc1  , "
            sSQL = sSQL & "      ABS(ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0)  - "
            sSQL = sSQL & "     ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0))  As DP1  "
            sSQL = sSQL & "        From Acc_TrailBalance_Upload_Details "
            sSQL = sSQL & "    left join ACC_ScheduleHeading a on a.ASH_ID= ATBUD_headingid "
            sSQL = sSQL & "    Left Join Acc_TrailBalance_Upload d on d.ATBU_Description = ATBUD_Description  "
            sSQL = sSQL & " And d.ATBU_YEARId=" & iYearID & " And d.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID & "  "
            sSQL = sSQL & " Left Join Acc_TrailBalance_Upload e on e.ATBU_Description = ATBUD_Description "
            sSQL = sSQL & " And e.ATBU_YEARId=" & iYearID - 1 & " And e.ATBU_CustId= " & iCustomerID & "  And ATBUD_YEARId =" & iYearID - 1 & "   "
            sSQL = sSQL & " where ATBUD_Schedule_type = " & iSchedType & "  And ATBUD_CustId = " & iCustomerID & "  And ATBUD_Headingid = " & iHeadingId & ""
            If sBranchid <> "" Then
                sSQL = sSQL & " And Atbud_Branchnameid In (" & sBranchid & ")"
            End If
            sSQL = sSQL & " group by ATBUD_Headingid order by ATBUD_Headingid "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSQL)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubHeadingAmt1(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal sBranchid As String, ByVal iYearid As Integer, ByVal iScheduleId As Integer, ByVal iHeadingId As Integer)
        Dim sSql As String
        Dim Sum As String = ""
        Dim dSum As Double = 0
        Dim dt As DataTable
        Try
            If iScheduleId = 3 Then
                sSql = " Select ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0)  -  "
                sSql = sSql & " ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0) As Dc1  , "
                sSql = sSql & " ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0)  - "
                sSql = sSql & " ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0)  As DP1 "
                sSql = sSql & " From Acc_TrailBalance_Upload_Details "
                sSql = sSql & " left join ACC_ScheduleSubHeading a On a.ASSH_ID= ATBUD_Subheading "
                sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
                sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
                sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
                sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & "   "
                sSql = sSql & " where ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & "  And ATBUD_Subheading =" & iHeadingId & ""
                If sBranchid <> "" Then
                    sSql = sSql & " And Atbud_Branchnameid In (" & sBranchid & ")"
                End If
                sSql = sSql & " group by ATBUD_Headingid order by ATBUD_Headingid "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Else
                sSql = "Select CASE b.ASH_Notes  "
                sSql = sSql & " WHEN 1 THEN ABS(ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0) - ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0)) -"
                sSql = sSql & " ABS(ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0) - ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0))"
                sSql = sSql & " WHEN 2 THEN  ABS(ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0) - ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0)) -"
                sSql = sSql & " ABS(ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0) -  ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0)) End  As Dc1"
                sSql = sSql & " From Acc_TrailBalance_Upload_Details "
                sSql = sSql & " left join ACC_ScheduleSubHeading a On a.ASSH_ID= ATBUD_Subheading "
                sSql = sSql & " LEFT join ACC_ScheduleHeading b on b.ASH_ID=a.ASSH_HeadingID"
                sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
                sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
                sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
                sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & "   "
                sSql = sSql & " where ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & "  And ATBUD_Subheading =" & iHeadingId & ""
                If sBranchid <> "" Then
                    sSql = sSql & " And Atbud_Branchnameid In (" & sBranchid & ")"
                End If
                sSql = sSql & " group by ATBUD_Headingid,ASH_Notes order by ATBUD_Headingid"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getcashEquivalentCY(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal iBranchid As Integer, ByVal iYearid As Integer, ByVal iScheduleId As Integer, ByVal iHeadingId As Integer)
        Dim sSql As String
        Dim Sum As String = ""
        Dim dSum As Double = 0
        Dim dt As DataTable
        Try
            sSql = "Select ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0)  As Dc1 "
            sSql = sSql & " From Acc_TrailBalance_Upload_Details "
            sSql = sSql & " left join ACC_ScheduleSubHeading a On a.ASSH_ID= ATBUD_Subheading "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
            sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
            sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & " "
            sSql = sSql & " And e.ATBU_CustId= " & ICustid & " And e.ATBU_Branchid=Atbud_Branchnameid  And e.Atbu_Branchid =" & iBranchid & ""
            sSql = sSql & " where ATBUD_Subheading =" & iHeadingId & " And ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & ""
            sSql = sSql & " group by ATBUD_Headingid order by ATBUD_Headingid"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getcashEquivalentPy(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal sBranchid As String, ByVal iYearid As Integer, ByVal iScheduleId As Integer, ByVal iHeadingId As Integer)
        Dim sSql As String
        Dim Sum As String = ""
        Dim dSum As Double = 0
        Dim dt As DataTable
        Try
            sSql = "Select ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0)  As Dc1 "
            sSql = sSql & " From Acc_TrailBalance_Upload_Details "
            sSql = sSql & " left join ACC_ScheduleSubHeading a On a.ASSH_ID= ATBUD_Subheading "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
            sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
            sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & " "
            sSql = sSql & " And e.ATBU_CustId= " & ICustid & " And e.ATBU_Branchid=Atbud_Branchnameid "
            sSql = sSql & " where ATBUD_Subheading =" & iHeadingId & " And ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & ""
            If sBranchid <> "" Then
                sSql = sSql & " And Atbud_Branchnameid In (" & sBranchid & ")"
            End If
            sSql = sSql & " group by ATBUD_Headingid order by ATBUD_Headingid"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemAmtCY(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal sBranchid As String, ByVal iYearid As Integer, ByVal iScheduleId As Integer, ByVal iHeadingId As Integer)
        Dim sSql As String
        Dim Sum As String = ""
        Dim dSum As Double = 0
        Dim dt As DataTable
        Try
            sSql = "Select ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0) -  "
            sSql = sSql & " ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0)  As Dc1 "
            sSql = sSql & " From Acc_TrailBalance_Upload_Details "
            sSql = sSql & " left join ACC_ScheduleItems a On a.ASI_ID= ATBUD_itemid "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
            sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
            sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & " "
            sSql = sSql & " And e.ATBU_CustId= " & ICustid & " And e.ATBU_Branchid=Atbud_Branchnameid"
            sSql = sSql & " where ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & "  And ATBUD_itemid =" & iHeadingId & ""
            If sBranchid <> "" Then
                sSql = sSql & " And Atbud_Branchnameid In (" & sBranchid & ")"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemAmtpy(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal sBranchid As String, ByVal iYearid As Integer, ByVal iScheduleId As Integer, ByVal iHeadingId As Integer)
        Dim sSql As String
        Dim Sum As String = ""
        Dim dSum As Double = 0
        Dim dt As DataTable
        Try
            sSql = "Select ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0) -  "
            sSql = sSql & " ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0)  As Dc1 "
            sSql = sSql & " From Acc_TrailBalance_Upload_Details "
            sSql = sSql & " left join ACC_ScheduleItems a On a.ASI_ID= ATBUD_itemid "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
            sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
            sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
            sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & " "
            sSql = sSql & " And e.ATBU_CustId= " & ICustid & " And e.ATBU_Branchid=Atbud_Branchnameid "
            sSql = sSql & " where ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & "  And ATBUD_itemid =" & iHeadingId & ""
            If sBranchid <> "" Then
                sSql = sSql & " And Atbud_Branchnameid In (" & sBranchid & ")"
            End If
            sSql = sSql & " group by ATBUD_Headingid order by ATBUD_Headingid"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubHeadingAmtForMultipleheadings(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal sBranchid As String, ByVal iYearid As Integer, ByVal iScheduleId As Integer, ByVal iHeadingIds As String)
        Dim sSql As String
        Dim Sum As String = ""
        Dim dSum As Double = 0
        Dim dt As DataTable
        Try
            If iScheduleId = 3 Then
                sSql = " Select ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0)  -  "
                sSql = sSql & " ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0) As Dc1  , "
                sSql = sSql & " ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0)  - "
                sSql = sSql & " ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0)  As DP1 "
                sSql = sSql & " From Acc_TrailBalance_Upload_Details "
                sSql = sSql & " left join ACC_ScheduleSubHeading a On a.ASSH_ID= ATBUD_Subheading "
                sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
                sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
                sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
                sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & "   "
                sSql = sSql & " where ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & "  And ATBUD_Subheading  In (" & iHeadingIds & ") "
                If sBranchid <> "" Then
                    sSql = sSql & " And Atbud_Branchnameid In (" & sBranchid & ")"
                End If
                sSql = sSql & " group by ATBUD_Headingid order by ATBUD_Headingid "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Else
                sSql = "Select ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount + 0), 0) - "
                sSql = sSql & " ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount + 0), 0) - "
                sSql = sSql & " (ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount + 0), 0) -"
                sSql = sSql & " ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount + 0), 0))  As Dc1 "
                sSql = sSql & " From Acc_TrailBalance_Upload_Details "
                sSql = sSql & " left join ACC_ScheduleSubHeading a On a.ASSH_ID= ATBUD_Subheading "
                sSql = sSql & " Left Join Acc_TrailBalance_Upload d On d.ATBU_Description = ATBUD_Description  "
                sSql = sSql & " And d.ATBU_YEARId=" & iYearid & " And d.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid & "  "
                sSql = sSql & " Left Join Acc_TrailBalance_Upload e On e.ATBU_Description = ATBUD_Description "
                sSql = sSql & " And e.ATBU_YEARId=" & iYearid - 1 & " And e.ATBU_CustId= " & ICustid & "  And ATBUD_YEARId =" & iYearid - 1 & "   "
                sSql = sSql & " where ATBUD_Schedule_type = " & iScheduleId & " And ATBUD_CustId = " & ICustid & "  And ATBUD_Subheading  In (" & iHeadingIds & ")"
                If sBranchid <> "" Then
                    sSql = sSql & " And Atbud_Branchnameid In (" & sBranchid & ")"
                End If
                sSql = sSql & " group by ATBUD_Headingid order by ATBUD_Headingid "
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCategory1detailsFinance(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal iYearid As Integer)
        Dim sSql As String
        Dim Sum As String = ""
        Dim iHeadingid As Integer = 0
        Try
            sSql = "Select ASSH_ID,ASSH_Name from ACC_ScheduleSUbHeading where ASSH_Name='(e) Finance costs' and ASSH_CompId=1 and ASSh_Orgtype=28"
            iHeadingid = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If iHeadingid <> 0 Then
                sSql = "Select isnull((sum(a.ATBU_Closing_TotalCredit_Amount)-sum(a.ATBU_Closing_TotalDebit_Amount )),0) from Acc_TrailBalance_Upload_Details"
                sSql = sSql & " left join Acc_TrailBalance_Upload a on a.ATBU_Description= ATBUD_Description and ATBU_YEARId=" & iYearid & ""
                sSql = sSql & " where ATBUD_SUbHeading =" & iHeadingid & " And ATBUD_CustId=" & ICustid & " And ATBUD_SChedule_Type=3 ANd ATBUD_YEARId= " & iYearid & ""
                Sum = objDBL.SQLExecuteScalar(sAC, sSql)
                If Val(Sum) < 0 Then
                    Sum = Convert.ToDecimal(Sum).ToString("#,##0.00").Remove(0, 1)
                End If
                Sum = Convert.ToDecimal(Sum).ToString("#,##0.00")
                Return Sum
            Else
                Sum = Convert.ToDecimal(Val(Sum)).ToString("#,##0.00")
                Return Sum
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetCategory1detailsDepreciation(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal iYearid As Integer, ByVal ICategory As Integer)
    '    Dim sSql As String
    '    Dim Sum As String = ""
    '    Dim dSum As Double = 0
    '    Dim iHeadingid As Integer = 0
    '    Try
    '        sSql = "select ASSH_ID,ASSH_Name from ACC_ScheduleSUbHeading where ASSH_Name='(f) Depreciation and amortisation expenses' and ASSH_CompId=1 and ASSh_Orgtype=28"
    '        iHeadingid = objDBL.SQLExecuteScalarInt(sAC, sSql)
    '        If iHeadingid <> 0 Then
    '            sSql = "Select isnull((sum(a.ATBU_Closing_TotalCredit_Amount)-sum(a.ATBU_Closing_TotalDebit_Amount )),0) from Acc_TrailBalance_Upload_Details"
    '            sSql = sSql & " left join Acc_TrailBalance_Upload a on a.ATBU_Description= ATBUD_Description and ATBU_YEARId=" & iYearid & ""
    '            sSql = sSql & " where ATBUD_SUbHeading =" & iHeadingid & " And ATBUD_CustId=" & ICustid & " And ATBUD_SChedule_Type=3 ANd ATBUD_YEARId= " & iYearid & ""
    '            Sum = objDBL.SQLExecuteScalar(sAC, sSql)
    '            If Val(Sum) < 0 Then
    '                Sum = Convert.ToDecimal(Sum).ToString("#,##0.00").Remove(0, 1)
    '            End If
    '            Sum = Convert.ToDecimal(Sum).ToString("#,##0.00")
    '            Return Sum
    '        Else
    '            Sum = Convert.ToDecimal(Val(Sum)).ToString("#,##0.00")
    '            Return Sum
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CashflowGetCategory2details(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal iYearid As Integer, ByVal sHeading As String)
        Dim sSql As String
        Dim Sum As String = ""
        Dim iHeadingid As Integer = 0
        Try
            If sHeading <> "" Then
                sSql = "select ASSH_ID,ASSH_Name from ACC_ScheduleSUbHeading where ASSH_Name='" & sHeading & "' and ASSH_CompId=1 and ASSh_Orgtype=28"
                iHeadingid = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iHeadingid > 0 Then
                    sSql = "Select isnull((sum(a.ATBU_Closing_TotalCredit_Amount)-sum(a.ATBU_Closing_TotalDebit_Amount )),0) from Acc_TrailBalance_Upload_Details"
                    sSql = sSql & " left join Acc_TrailBalance_Upload a on a.ATBU_Description= ATBUD_Description and ATBU_YEARId=" & iYearid & ""
                    sSql = sSql & " where ATBUD_SUbHeading =" & iHeadingid & " And ATBUD_CustId=" & ICustid & " And ATBUD_SChedule_Type=3 ANd ATBUD_YEARId= " & iYearid & ""
                    Sum = objDBL.SQLExecuteScalar(sAC, sSql)
                    If Val(Sum) < 0 Then
                        Sum = Convert.ToDecimal(Sum).ToString("#,##0.00").Remove(0, 1)
                    End If
                    Sum = Convert.ToDecimal(Sum).ToString("#,##0.00")
                    Return Sum
                Else
                    Sum = Convert.ToDecimal(Val(Sum)).ToString("#,##0.00")
                    Return Sum
                End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteCashflowCategory1(ByVal sAC As String, ByVal iCompID As Integer, ByVal iPkid As Integer, ByVal ICustid As Integer)
        Dim sSql As String
        Try
            sSql = "delete Acc_Cashflow where ACF_pkid =" & iPkid & " and ACF_Custid=" & ICustid & " And ACF_Compid=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCashFlowParticularsID(ByVal sAC As String, ByVal iCompID As Integer, ByVal sDescription As String, ByVal ICustid As Integer, ByVal IBranchID As Integer)
        Dim sSql As String
        Dim iPkid As Integer = 0
        Try
            sSql = "select ACF_pkid from Acc_Cashflow where ACF_Description ='" & sDescription & "' and ACF_Custid=" & ICustid & " and acf_branchid=" & IBranchID & " And ACF_Compid=" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Getequivalents_at_begiming_Of_the_year(ByVal sAC As String, ByVal iCompID As Integer, ByVal ICustid As Integer, ByVal iYearid As Integer)
        Dim sSql As String
        Dim Sum As String = ""
        Dim iHeadingid As Integer = 0
        Try
            sSql = "select ASH_ID,ASH_Name from ACC_ScheduleHeading where ASH_Name='income' and ASH_CompId=1 and Ash_Orgtype=28"
            iHeadingid = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If iHeadingid <> 0 Then
                sSql = "Select isnull((sum(a.ATBU_Closing_TotalCredit_Amount)-sum(a.ATBU_Closing_TotalDebit_Amount )),0) from Acc_TrailBalance_Upload_Details"
                sSql = sSql & " left join Acc_TrailBalance_Upload a on a.ATBU_Description= ATBUD_Description and ATBU_YEARId=" & iYearid & ""
                sSql = sSql & " where ATBUD_Headingid =" & iHeadingid & " And ATBUD_CustId=" & ICustid & " And ATBUD_SChedule_Type=3 ANd ATBUD_YEARId= " & iYearid & ""
                Sum = objDBL.SQLExecuteScalar(sAC, sSql)
                If Val(Sum) < 0 Then
                    Sum = Convert.ToDecimal(Sum).ToString("#,##0.00").Remove(0, 1)
                End If
                Sum = Convert.ToDecimal(Sum).ToString("#,##0.00")
                Return Sum
            Else
                Sum = Convert.ToDecimal(Val(Sum)).ToString("#,##0.00")
                Return Sum
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
