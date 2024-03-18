Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsOpeningBalance
    Private objDBL As New DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral

    Private Opn_Id As Integer
    Private Opn_Date As Date
    Private Opn_AccHead As Integer
    Private Opn_GLCode As String
    Private Opn_DebitAmt As Double
    Private Opn_CreditAmount As Double
    Private Opn_YearId As Integer
    Private Opn_Status As String
    Private Opn_CompId As Integer
    Private Opn_GlId As Integer
    Private Opn_IPAddress As String
    Private Opn_CustType As Integer
    Private Opn_IndType As Integer
    Private Opn_ClosingBalanceDebit As Double
    Private Opn_ClosingBalanceCredit As Double
    Private Opn_GLDesc As String

    Private ATD_ID As Integer
    Private ATD_TransactionDate As Date
    Private ATD_TrType As Integer
    Private ATD_BillId As Integer
    Private ATD_PaymentType As Integer
    Private ATD_Head As Integer
    Private ATD_DbOrCr As Integer
    Private ATD_GL As Integer
    Private ATD_SubGL As Integer
    Private ATD_Debit As Decimal
    Private ATD_Credit As Decimal
    Private ATD_CreatedBy As Integer
    Private ATD_UpdatedBy As Integer
    Private ATD_Status As String
    Private ATD_YearID As Integer
    Private ATD_CompID As Integer
    Private ATD_Operation As String
    Private ATD_IPAddress As String
    Private ATD_CustID As Integer
    Private ATD_OrgType As Integer

    Public Property sOpn_GLDesc() As String
        Get
            Return (Opn_GLDesc)
        End Get
        Set(ByVal Value As String)
            Opn_GLDesc = Value
        End Set
    End Property

    Public Property sATD_IPAddress() As String
        Get
            Return (ATD_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATD_IPAddress = Value
        End Set
    End Property
    Public Property sATD_Operation() As String
        Get
            Return (ATD_Operation)
        End Get
        Set(ByVal Value As String)
            ATD_Operation = Value
        End Set
    End Property
    Public Property iATD_YearID() As Integer
        Get
            Return (ATD_YearID)
        End Get
        Set(ByVal Value As Integer)
            ATD_YearID = Value
        End Set
    End Property
    Public Property iATD_CompID() As Integer
        Get
            Return (ATD_CompID)
        End Get
        Set(ByVal Value As Integer)
            ATD_CompID = Value
        End Set
    End Property
    Public Property sATD_Status() As String
        Get
            Return (ATD_Status)
        End Get
        Set(ByVal Value As String)
            ATD_Status = Value
        End Set
    End Property
    Public Property iATD_CreatedBy() As Integer
        Get
            Return (ATD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_CreatedBy = Value
        End Set
    End Property
    Public Property iATD_UpdatedBy() As Integer
        Get
            Return (ATD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_UpdatedBy = Value
        End Set
    End Property
    Public Property dATD_Credit() As Decimal
        Get
            Return (ATD_Credit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_Credit = Value
        End Set
    End Property
    Public Property dATD_Debit() As Decimal
        Get
            Return (ATD_Debit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_Debit = Value
        End Set
    End Property
    Public Property iATD_SubGL() As Integer
        Get
            Return (ATD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            ATD_SubGL = Value
        End Set
    End Property
    Public Property iATD_GL() As Integer
        Get
            Return (ATD_GL)
        End Get
        Set(ByVal Value As Integer)
            ATD_GL = Value
        End Set
    End Property
    Public Property iATD_Head() As Integer
        Get
            Return (ATD_Head)
        End Get
        Set(ByVal Value As Integer)
            ATD_Head = Value
        End Set
    End Property
    Public Property iATD_DbOrCr() As Integer
        Get
            Return (ATD_DbOrCr)
        End Get
        Set(ByVal Value As Integer)
            ATD_DbOrCr = Value
        End Set
    End Property
    Public Property iATD_PaymentType() As Integer
        Get
            Return (ATD_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            ATD_PaymentType = Value
        End Set
    End Property
    Public Property iATD_BillId() As Integer
        Get
            Return (ATD_BillId)
        End Get
        Set(ByVal Value As Integer)
            ATD_BillId = Value
        End Set
    End Property
    Public Property iATD_TrType() As Integer
        Get
            Return (ATD_TrType)
        End Get
        Set(ByVal Value As Integer)
            ATD_TrType = Value
        End Set
    End Property
    Public Property dATD_TransactionDate() As Date
        Get
            Return (ATD_TransactionDate)
        End Get
        Set(ByVal Value As Date)
            ATD_TransactionDate = Value
        End Set
    End Property
    Public Property iATD_ID() As Integer
        Get
            Return (ATD_ID)
        End Get
        Set(ByVal Value As Integer)
            ATD_ID = Value
        End Set
    End Property

    Public Property sOpn_IPAddress() As String
        Get
            Return (Opn_IPAddress)
        End Get
        Set(ByVal Value As String)
            Opn_IPAddress = Value
        End Set
    End Property
    Public Property iOpn_GlId() As Integer
        Get
            Return (Opn_GlId)
        End Get
        Set(ByVal Value As Integer)
            Opn_GlId = Value
        End Set
    End Property
    Public Property iOpn_CompId() As Integer
        Get
            Return (Opn_CompId)
        End Get
        Set(ByVal Value As Integer)
            Opn_CompId = Value
        End Set
    End Property
    Public Property sOpn_Status() As String
        Get
            Return (Opn_Status)
        End Get
        Set(ByVal Value As String)
            Opn_Status = Value
        End Set
    End Property
    Public Property iOpn_YearId() As Integer
        Get
            Return (Opn_YearId)
        End Get
        Set(ByVal Value As Integer)
            Opn_YearId = Value
        End Set
    End Property
    Public Property dOpn_CreditAmount() As Double
        Get
            Return (Opn_CreditAmount)
        End Get
        Set(ByVal Value As Double)
            Opn_CreditAmount = Value
        End Set
    End Property
    Public Property dOpn_DebitAmt() As Double
        Get
            Return (Opn_DebitAmt)
        End Get
        Set(ByVal Value As Double)
            Opn_DebitAmt = Value
        End Set
    End Property
    Public Property sOpn_GLCode() As String
        Get
            Return (Opn_GLCode)
        End Get
        Set(ByVal Value As String)
            Opn_GLCode = Value
        End Set
    End Property
    Public Property iOpn_AccHead() As Integer
        Get
            Return (Opn_AccHead)
        End Get
        Set(ByVal Value As Integer)
            Opn_AccHead = Value
        End Set
    End Property
    Public Property dOpn_Date() As Date
        Get
            Return (Opn_Date)
        End Get
        Set(ByVal Value As Date)
            Opn_Date = Value
        End Set
    End Property
    Public Property iOpn_Id() As Integer
        Get
            Return (Opn_Id)
        End Get
        Set(ByVal Value As Integer)
            Opn_Id = Value
        End Set
    End Property
    Public Property iOpn_CustType() As Integer
        Get
            Return (Opn_CustType)
        End Get
        Set(ByVal Value As Integer)
            Opn_CustType = Value
        End Set
    End Property
    Public Property iOpn_IndType() As Integer
        Get
            Return (Opn_IndType)
        End Get
        Set(ByVal Value As Integer)
            Opn_IndType = Value
        End Set
    End Property
    Public Property dOpn_ClosingBalanceCredit() As Double
        Get
            Return (Opn_ClosingBalanceCredit)
        End Get
        Set(ByVal Value As Double)
            Opn_ClosingBalanceCredit = Value
        End Set
    End Property
    Public Property dOpn_ClosingBalanceDebit() As Double
        Get
            Return (Opn_ClosingBalanceDebit)
        End Get
        Set(ByVal Value As Double)
            Opn_ClosingBalanceDebit = Value
        End Set
    End Property
    Public Property iATD_CustID() As Integer
        Get
            Return (ATD_CustID)
        End Get
        Set(ByVal Value As Integer)
            ATD_CustID = Value
        End Set
    End Property
    Public Property iATD_OrgType() As Integer
        Get
            Return (ATD_OrgType)
        End Get
        Set(ByVal Value As Integer)
            ATD_OrgType = Value
        End Set
    End Property
    Public Function GetAccHeadID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_AccHead from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_ID from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetParentID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_Parent from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOrgType(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select cmm_ID,cmm_Desc from Content_Management_Master Left join SAD_CUSTOMER_MASTER On CUST_ORGTYPEID=cmm_ID And CUST_CompID=" & iACID & ""
            sSql = sSql & " And CUST_DELFLG='A' where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' And CUST_ID=" & iCustID & " order by cmm_Desc Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadGrdGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGlID As Integer, ByVal iSubGL As Integer, ByVal iCustomerID As Integer, ByVal iOrgTypeID As Integer) As DataTable
    '    Dim sSql As String = "", asql As String = ""
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim dtDetails As New DataTable, dtGridDetails As New DataTable
    '    Dim dCYATDDeTotal As Double, dCYATDCrTotal As Double, dCYOBDeTotal As Double, dCYOBDCrTotal As Double, dCYOBDeTotalAmount As Double, dCYOBDCrTotalAmount As Double
    '    Dim dPYATDDeTotal As Double, dPYATDCrTotal As Double, dPYOBDeTotal As Double, dPYOBDCrTotal As Double, dPYOBDeTotalAmount As Double, dPYOBDCrTotalAmount As Double
    '    Try
    '        dt.Columns.Add("GLID")
    '        dt.Columns.Add("AccHead")
    '        dt.Columns.Add("SLNo")
    '        dt.Columns.Add("GLCode")
    '        dt.Columns.Add("GLDescription")
    '        dt.Columns.Add("Debit")
    '        dt.Columns.Add("Credit")
    '        dt.Columns.Add("Balance")
    '        dt.Columns.Add("StartDate")

    '        dt.Columns.Add("PYDebit")
    '        dt.Columns.Add("PYCredit")
    '        dt.Columns.Add("PYBalance")
    '        dt.Columns.Add("Status")

    '        If iCustomerID > 0 And iOrgTypeID > 0 Then
    '            If (iHead = 0) And (iGlID = 0) And (iSubGL = 0) Then
    '                sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' and gl_Status ='A' and (gl_head=2 or gl_head=3) order by gl_glcode"
    '            ElseIf (iHead > 0) And (iGlID = 0) And (iSubGL = 0) Then
    '                sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' And gl_Status ='A' and (gl_head=2 or gl_head=3) and gl_AccHead = " & iHead & " order by gl_glcode"
    '            ElseIf (iHead > 0) And (iGlID > 0) And (iSubGL = 0) Then
    '                sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' And gl_Status ='A' and (gl_head=2 or gl_head=3) and gl_AccHead = " & iHead & " and gl_Parent in(Select gl_id from chart_of_Accounts where gl_parent = " & iGlID & ")  order by gl_glcode"
    '            ElseIf (iHead > 0) And (iGlID > 0) And (iSubGL > 0) Then
    '                sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' And gl_Status ='A' and (gl_head=2 or gl_head=3) and gl_AccHead = " & iHead & " and gl_id in(Select gl_id from chart_of_Accounts where gl_parent = " & iSubGL & ") order by gl_glcode"
    '            End If

    '            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
    '            If dtDetails.Rows.Count > 0 Then
    '                For j = 0 To dtDetails.Rows.Count - 1
    '                    dRow = dt.NewRow()
    '                    dRow("SLNo") = j + 1
    '                    If IsDBNull(dtDetails.Rows(j)("gl_Id")) = False Then
    '                        dRow("GLID") = dtDetails.Rows(j)("gl_Id")
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(j)("gl_AccHead")) = False Then
    '                        dRow("AccHead") = dtDetails.Rows(j)("gl_AccHead")
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(j)("gl_glcode")) = False Then
    '                        dRow("GLCode") = dtDetails.Rows(j)("gl_glcode")
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(j)("gl_Desc")) = False Then
    '                        dRow("GLDescription") = dtDetails.Rows(j)("gl_Desc")
    '                    End If

    '                    asql = "" : asql = "Select a.Opn_DebitAmt As CYDebitAmt,a.Opn_CreditAmount As CYCreditAmount,a.Opn_Date As CYDate,a.Opn_Status As Status,"
    '                    asql = asql & " b.Opn_DebitAmt As PYDebitAmt,b.Opn_CreditAmount As PYCreditAmount,b.Opn_Date As PYDate,"
    '                    asql = asql & " c.ATD_Debit As CYDebit,c.ATD_Credit As CYCredit,d.ATD_Debit As PYDebit,d.ATD_Credit As PYCredit From Acc_opening_balance a"
    '                    asql = asql & " Left Join Acc_opening_balance b On b.opn_compid=" & iACID & " And b.Opn_YearID=" & iYearID - 1 & " And b.Opn_glid=" & dtDetails.Rows(j)("gl_Id") & " And b.Opn_CustType=" & iCustomerID & " And b.Opn_IndType=" & iOrgTypeID & ""
    '                    asql = asql & " Left Join Acc_Transactions_Details c On c.ATD_GL=a.Opn_GlId And c.ATD_CompID=" & iACID & ""
    '                    asql = asql & " Left Join Acc_Transactions_Details d On d.ATD_GL=b.Opn_GlId And d.ATD_CompID=" & iACID & ""
    '                    asql = asql & " Where a.opn_compid=" & iACID & " And a.Opn_YearID=" & iYearID & " And a.Opn_glid=" & dtDetails.Rows(j)("gl_Id") & " And a.Opn_CustType=" & iCustomerID & " And a.Opn_IndType=" & iOrgTypeID & ""

    '                    dtGridDetails = objDBL.SQLExecuteDataTable(sAC, asql)
    '                    If dtGridDetails.Rows.Count > 0 Then
    '                        For i = 0 To dtGridDetails.Rows.Count - 1
    '                            'Current Year
    '                            If IsDBNull(dtGridDetails.Rows(i)("CYDate")) = False Then
    '                                If dtGridDetails.Rows(i)("CYDate").ToString() <> "" Then
    '                                    dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtGridDetails.Rows(i)("CYDate"), "D")
    '                                End If
    '                            End If

    '                            If IsDBNull(dtGridDetails.Rows(i)("CYDebitAmt")) = False Then
    '                                dCYOBDeTotal = dtGridDetails.Rows(i)("CYDebitAmt")
    '                            Else
    '                                dCYOBDeTotal = 0.00
    '                            End If
    '                            If IsDBNull(dtGridDetails.Rows(i)("CYCreditAmount")) = False Then
    '                                dCYOBDCrTotal = dtGridDetails.Rows(i)("CYCreditAmount")
    '                            Else
    '                                dCYOBDCrTotal = 0.00
    '                            End If
    '                            If IsDBNull(dtGridDetails.Rows(i)("CYDebit")) = False Then
    '                                dCYATDDeTotal = dtGridDetails.Rows(i)("CYDebit")
    '                            Else
    '                                dCYATDDeTotal = 0.00
    '                            End If
    '                            If IsDBNull(dtGridDetails.Rows(i)("CYCredit")) = False Then
    '                                dCYATDCrTotal = dtGridDetails.Rows(i)("CYCredit")
    '                            Else
    '                                dCYATDCrTotal = 0.00
    '                            End If

    '                            dCYOBDeTotalAmount = dCYOBDeTotal + dCYATDDeTotal
    '                            dCYOBDCrTotalAmount = dCYOBDCrTotal + dCYATDCrTotal

    '                            If Convert.ToDouble(dCYOBDeTotalAmount) = 0.00 Then
    '                                dRow("Debit") = ""
    '                            Else
    '                                dRow("Debit") = Convert.ToDecimal(dCYOBDeTotalAmount).ToString("#,##0.00")
    '                            End If

    '                            If Convert.ToDouble(dCYOBDCrTotalAmount) = 0.00 Then
    '                                dRow("Credit") = ""
    '                            Else
    '                                dRow("Credit") = Convert.ToDecimal(dCYOBDCrTotalAmount).ToString("#,##0.00")
    '                            End If

    '                            'If IsDBNull(dtGridDetails.Rows(i)("CYDebitAmt")) = False Then
    '                            '    If Convert.ToDouble(dtGridDetails.Rows(i)("CYDebitAmt")) = 0.00 Then
    '                            '        dRow("Debit") = ""
    '                            '    Else
    '                            '        dRow("Debit") = Convert.ToDecimal(dtGridDetails.Rows(i)("CYDebitAmt")).ToString("#,##0.00")
    '                            '    End If
    '                            'End If

    '                            'If IsDBNull(dtGridDetails.Rows(i)("CYCreditAmount")) = False Then
    '                            '    If Convert.ToDouble(dtGridDetails.Rows(i)("CYCreditAmount")) = 0.00 Then
    '                            '        dRow("Credit") = ""
    '                            '    Else
    '                            '        dRow("Credit") = Convert.ToDecimal(dtGridDetails.Rows(i)("CYCreditAmount")).ToString("#,##0.00")
    '                            '    End If
    '                            'End If

    '                            If IsDBNull(dtGridDetails.Rows(i)("CYDebitAmt")) = False And IsDBNull(dtGridDetails.Rows(i)("CYCreditAmount")) = False Then
    '                                Dim dBalance As Double = Convert.ToDecimal(dtGridDetails.Rows(i)("CYDebitAmt") - dtGridDetails.Rows(i)("CYCreditAmount")).ToString("#,##0.00")
    '                                If dBalance < 0 Then
    '                                    dRow("Balance") = dBalance & " Cr"
    '                                Else
    '                                    dRow("Balance") = dBalance & " Dr"
    '                                End If
    '                            End If


    '                            'Previous Year
    '                            If IsDBNull(dtGridDetails.Rows(i)("PYDebitAmt")) = False Then
    '                                dPYOBDeTotal = dtGridDetails.Rows(i)("PYDebitAmt")
    '                            Else
    '                                dPYOBDeTotal = 0.00
    '                            End If
    '                            If IsDBNull(dtGridDetails.Rows(i)("PYCreditAmount")) = False Then
    '                                dPYOBDCrTotal = dtGridDetails.Rows(i)("PYCreditAmount")
    '                            Else
    '                                dPYOBDCrTotal = 0.00
    '                            End If
    '                            If IsDBNull(dtGridDetails.Rows(i)("PYDebit")) = False Then
    '                                dPYATDDeTotal = dtGridDetails.Rows(i)("PYDebit")
    '                            Else
    '                                dPYATDDeTotal = 0.00
    '                            End If
    '                            If IsDBNull(dtGridDetails.Rows(i)("PYCredit")) = False Then
    '                                dPYATDCrTotal = dtGridDetails.Rows(i)("PYCredit")
    '                            Else
    '                                dPYATDCrTotal = 0.00
    '                            End If

    '                            dPYOBDeTotalAmount = dPYOBDeTotal + dPYATDDeTotal
    '                            dPYOBDCrTotalAmount = dPYOBDCrTotal + dPYATDCrTotal

    '                            If Convert.ToDouble(dPYOBDeTotalAmount) = 0.00 Then
    '                                dRow("PYDebit") = ""
    '                            Else
    '                                dRow("PYDebit") = Convert.ToDecimal(dPYOBDeTotalAmount).ToString("#,##0.00")
    '                            End If

    '                            If Convert.ToDouble(dPYOBDCrTotalAmount) = 0.00 Then
    '                                dRow("PYCredit") = ""
    '                            Else
    '                                dRow("PYCredit") = Convert.ToDecimal(dPYOBDCrTotalAmount).ToString("#,##0.00")
    '                            End If

    '                            'If IsDBNull(dtGridDetails.Rows(i)("PYDebitAmt")) = False Then
    '                            '    If Convert.ToDouble(dtGridDetails.Rows(i)("PYDebitAmt")) = 0.00 Then
    '                            '        dRow("PYDebit") = ""
    '                            '    Else
    '                            '        dRow("PYDebit") = Convert.ToDecimal(dtGridDetails.Rows(i)("PYDebitAmt")).ToString("#,##0.00")
    '                            '    End If
    '                            'End If

    '                            'If IsDBNull(dtGridDetails.Rows(i)("PYCreditAmount")) = False Then
    '                            '    If Convert.ToDouble(dtGridDetails.Rows(i)("PYCreditAmount")) = 0.00 Then
    '                            '        dRow("PYCredit") = ""
    '                            '    Else
    '                            '        dRow("PYCredit") = Convert.ToDecimal(dtGridDetails.Rows(i)("PYCreditAmount")).ToString("#,##0.00")
    '                            '    End If
    '                            'End If

    '                            If IsDBNull(dtGridDetails.Rows(i)("PYDebitAmt")) = False And IsDBNull(dtGridDetails.Rows(i)("PYCreditAmount")) = False Then
    '                                Dim dPYBalance As Double = Convert.ToDecimal(dtGridDetails.Rows(i)("PYDebitAmt") - dtGridDetails.Rows(i)("PYCreditAmount")).ToString("#,##0.00")
    '                                If dPYBalance < 0 Then
    '                                    dRow("PYBalance") = dPYBalance & " Cr"
    '                                Else
    '                                    dRow("PYBalance") = dPYBalance & " Dr"
    '                                End If
    '                            End If

    '                            If dtGridDetails.Rows(i)("Status") = "A" Then
    '                                dRow("Status") = "Activated"
    '                            ElseIf dtGridDetails.Rows(i)("Status") = "F" Then
    '                                dRow("Status") = "Freezed"
    '                            End If
    '                        Next
    '                    Else
    '                        dRow("Debit") = "" : dRow("Credit") = "" : dRow("PYDebit") = "" : dRow("PYCredit") = ""
    '                    End If
    '                    dt.Rows.Add(dRow)
    '                Next
    '            End If
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadGrdGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGlID As Integer, ByVal iSubGL As Integer, ByVal iCustomerID As Integer, ByVal iOrgTypeID As Integer) As DataTable
        Dim sSql As String = "", asql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable, dtGridDetails As New DataTable
        Dim dCYATDDeTotal As Double, dCYATDCrTotal As Double, dCYOBDeTotal As Double, dCYOBDCrTotal As Double, dCYOBDeTotalAmount As Double, dCYOBDCrTotalAmount As Double
        'Dim dPYATDDeTotal As Double, dPYATDCrTotal As Double, dPYOBDeTotal As Double, dPYOBDCrTotal As Double, dPYOBDeTotalAmount As Double, dPYOBDCrTotalAmount As Double
        Dim dDebitTotal, dCreditTotal, dGLDebitTotal, dGLCreditTotal As Double

        Dim dSumATDDebit As Double = 0.00 : Dim dSumATDCredit As Double = 0.00
        Dim iAtdCount As Integer = 0
        Try
            dt.Columns.Add("GLID")
            dt.Columns.Add("AccHead")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")
            dt.Columns.Add("StartDate")

            dt.Columns.Add("PYDebit")
            dt.Columns.Add("PYCredit")
            dt.Columns.Add("PYBalance")
            dt.Columns.Add("Status")
            dt.Columns.Add("SGL")

            If iCustomerID > 0 And iOrgTypeID > 0 Then
                'If (iHead = 0) And (iGlID = 0) And (iSubGL = 0) Then
                '    sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' and gl_Status ='A' and (gl_head=2 or gl_head=3) order by gl_glcode"
                'ElseIf (iHead > 0) And (iGlID = 0) And (iSubGL = 0) Then
                '    sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' And gl_Status ='A' and (gl_head=2 or gl_head=3) and gl_AccHead = " & iHead & " order by gl_glcode"
                'ElseIf (iHead > 0) And (iGlID > 0) And (iSubGL = 0) Then
                '    sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' And gl_Status ='A' and (gl_head=2 or gl_head=3) and gl_AccHead = " & iHead & " and gl_Parent in(Select gl_id from chart_of_Accounts where gl_parent = " & iGlID & ")  order by gl_glcode"
                'ElseIf (iHead > 0) And (iGlID > 0) And (iSubGL > 0) Then
                '    sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " and gl_OrgTypeID=0 and gl_delflag='C' And gl_Status ='A' and (gl_head=2 or gl_head=3) and gl_AccHead = " & iHead & " and gl_id in(Select gl_id from chart_of_Accounts where gl_parent = " & iSubGL & ") order by gl_glcode"
                'End If

                'sSql = "" : sSql = "Select * From Customer_COA Where CC_CustID=" & iCustomerID & " And CC_IndType=" & iOrgTypeID & " And CC_YearID=" & iYearID & " And CC_CompID=" & iACID & " "

                ' Commented by Vijayalakshmi 13/01/2020
                'sSql = "" : sSql = "Select CC_GL,CC_AccHead,CC_glcode,cc_parent,ATD_Subgl,CC_GLDesc,CC_CreatedOn,CC_OBDebit,CC_OBCredit,CC_TrDebit,CC_TrCredit,ATD_Debit,ATD_Credit From Customer_COA a
                '                    Left Join Acc_Transactions_details b On b.ATD_SubGL=a.CC_GL And b.ATD_TrType=4
                '                    Where a.CC_CustID=" & iCustomerID & " And a.CC_IndType=" & iOrgTypeID & " And a.CC_YearID=" & iYearID & " And a.CC_CompID=" & iACID & " order by cc_gl"


                sSql = "" : sSql = "Select CC_GL,CC_AccHead,CC_glcode,cc_parent,CC_GLDesc,CC_CreatedOn,CC_OBDebit,CC_OBCredit,CC_TrDebit,CC_TrCredit From Customer_COA 
                                   Where CC_CustID=" & iCustomerID & " And CC_IndType=" & iOrgTypeID & " And CC_YearID=" & iYearID & " And CC_CompID=" & iACID & " order by cc_gl"

                dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtDetails.Rows.Count > 0 Then
                    For j = 0 To dtDetails.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("SLNo") = j + 1

                        'Vijayalakshmi 17/12/2019 Changed cc_gl to cc_parent
                        'If IsDBNull(dtDetails.Rows(j)("CC_GL")) = False Then
                        '    dRow("GLID") = dtDetails.Rows(j)("CC_GL")
                        'End If

                        If IsDBNull(dtDetails.Rows(j)("CC_Parent")) = False Then
                            dRow("GLID") = dtDetails.Rows(j)("CC_Parent")
                        End If
                        If IsDBNull(dtDetails.Rows(j)("CC_GL")) = False Then
                            dRow("SGL") = dtDetails.Rows(j)("CC_GL")
                        End If

                        If IsDBNull(dtDetails.Rows(j)("CC_AccHead")) = False Then
                            dRow("AccHead") = dtDetails.Rows(j)("CC_AccHead")
                        End If

                        If IsDBNull(dtDetails.Rows(j)("CC_glcode")) = False Then
                            dRow("GLCode") = dtDetails.Rows(j)("CC_glcode")
                        End If

                        If IsDBNull(dtDetails.Rows(j)("CC_GLDesc")) = False Then
                            dRow("GLDescription") = dtDetails.Rows(j)("CC_GLDesc")
                        End If

                        If IsDBNull(dtDetails.Rows(j)("CC_GL")) = False Then
                            iAtdCount = objDBL.SQLExecuteScalar(sAC, "select Count(ATD_ID) From Acc_Transactions_details where atd_CustID=" & iCustomerID & " And ATD_OrgType=" & iOrgTypeID & " And Atd_YearID=" & iYearID & " And Atd_CompID=" & iACID & " and ATD_SubGL=" & dtDetails.Rows(j)("CC_GL") & "")
                            If iAtdCount > 0 Then
                                dSumATDDebit = objDBL.SQLExecuteScalar(sAC, "select sum(ATD_Debit) From Acc_Transactions_details where atd_CustID=" & iCustomerID & " And ATD_OrgType=" & iOrgTypeID & " And Atd_YearID=" & iYearID & " And Atd_CompID=" & iACID & " and ATD_SubGL=" & dtDetails.Rows(j)("CC_GL") & "")
                                dSumATDCredit = objDBL.SQLExecuteScalar(sAC, "select sum(ATD_Credit) From Acc_Transactions_details where atd_CustID=" & iCustomerID & " And ATD_OrgType=" & iOrgTypeID & " And Atd_YearID=" & iYearID & " And Atd_CompID=" & iACID & " and ATD_SubGL=" & dtDetails.Rows(j)("CC_GL") & "")
                            Else
                                dSumATDDebit = 0.00
                                dSumATDCredit = 0.00
                            End If
                        Else
                            dSumATDDebit = 0.00
                            dSumATDCredit = 0.00
                        End If


                        'Current Year
                        If IsDBNull(dtDetails.Rows(j)("CC_CreatedOn")) = False Then
                            If dtDetails.Rows(j)("CC_CreatedOn").ToString() <> "" Then
                                dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(j)("CC_CreatedOn"), "D")
                            End If
                        End If

                        If IsDBNull(dtDetails.Rows(j)("CC_OBDebit")) = False Then
                            dCYOBDeTotal = dtDetails.Rows(j)("CC_OBDebit")
                        Else
                            dCYOBDeTotal = 0.00
                        End If
                        If IsDBNull(dtDetails.Rows(j)("CC_OBCredit")) = False Then
                            dCYOBDCrTotal = dtDetails.Rows(j)("CC_OBCredit")
                        Else
                            dCYOBDCrTotal = 0.00
                        End If
                        If IsDBNull(dtDetails.Rows(j)("CC_TrDebit")) = False Then
                            ' If IsDBNull(dtDetails.Rows(j)("ATD_Debit")) = False Then
                            dCYATDDeTotal = dtDetails.Rows(j)("CC_TrDebit") + dSumATDDebit '+ dtDetails.Rows(j)("ATD_Debit")
                            '  Else
                            'dCYATDDeTotal = dtDetails.Rows(j)("CC_TrDebit")
                            'End If
                        Else
                            'If IsDBNull(dtDetails.Rows(j)("ATD_Debit")) = False Then
                            dCYATDDeTotal = 0.00 + dSumATDDebit 'dtDetails.Rows(j)("ATD_Debit")
                            ' Else
                            'dCYATDDeTotal = 0.00
                            'End If

                        End If
                        If IsDBNull(dtDetails.Rows(j)("CC_TrCredit")) = False Then
                            'If IsDBNull(dtDetails.Rows(j)("ATD_Credit")) = False Then
                            dCYATDCrTotal = dtDetails.Rows(j)("CC_TrCredit") + dSumATDCredit 'dtDetails.Rows(j)("ATD_Credit")
                            'Else
                            'dCYATDCrTotal = dtDetails.Rows(j)("CC_TrCredit")
                            'End If
                        Else
                            ' If IsDBNull(dtDetails.Rows(j)("ATD_Credit")) = False Then
                            dCYATDCrTotal = 0.00 + dSumATDCredit 'dtDetails.Rows(j)("ATD_Credit")
                            'Else
                            'dCYATDCrTotal = 0.00
                            'End If
                        End If

                        dDebitTotal = dCYOBDeTotal + dCYATDDeTotal
                        dCreditTotal = dCYOBDCrTotal + dCYATDCrTotal

                        dGLDebitTotal = dGLDebitTotal + dDebitTotal
                        dGLCreditTotal = dGLCreditTotal + dCreditTotal

                        dCYOBDeTotalAmount = dCYOBDeTotalAmount + dDebitTotal
                        dCYOBDCrTotalAmount = dCYOBDCrTotalAmount + dCreditTotal

                        'If dtDetails.Rows(j)("CC_Head") = 2 Then    'GL
                        '    If Convert.ToDouble(dGLDebitTotal) = 0.00 Then
                        '        dRow("Debit") = ""
                        '    Else
                        '        dRow("Debit") = Convert.ToDecimal(dGLDebitTotal).ToString("#,##0.00")
                        '    End If

                        '    If Convert.ToDouble(dGLCreditTotal) = 0.00 Then
                        '        dRow("Credit") = ""
                        '    Else
                        '        dRow("Credit") = Convert.ToDecimal(dGLCreditTotal).ToString("#,##0.00")
                        '    End If

                        '    Dim dBalance As Double = Convert.ToDecimal(dGLDebitTotal - dGLCreditTotal).ToString("#,##0.00")
                        '    If dBalance < 0 Then
                        '        dRow("Balance") = dBalance & " Cr"
                        '    Else
                        '        dRow("Balance") = dBalance & " Dr"
                        '    End If
                        'ElseIf dtDetails.Rows(j)("CC_Head") = 3 Then    'SubGL
                        If Convert.ToDouble(dDebitTotal) = 0.00 Then
                            dRow("Debit") = ""
                        Else
                            dRow("Debit") = Convert.ToDecimal(dDebitTotal).ToString("#,##0.00")
                        End If

                        If Convert.ToDouble(dCreditTotal) = 0.00 Then
                            dRow("Credit") = ""
                        Else
                            dRow("Credit") = Convert.ToDecimal(dCreditTotal).ToString("#,##0.00")
                        End If

                        Dim dBalance As Double = Convert.ToDecimal(dDebitTotal - dCreditTotal).ToString("#,##0.00")
                        If dBalance < 0 Then
                            dRow("Balance") = dBalance & " Cr"
                        Else
                            dRow("Balance") = dBalance & " Dr"
                        End If
                        'End If


                        dRow("PYDebit") = ""
                        dRow("PYCredit") = ""
                        dRow("PYBalance") = ""
                        dRow("Status") = ""

                        dt.Rows.Add(dRow)

                        dGLDebitTotal = 0
                        dGLCreditTotal = 0
                    Next
                Else
                    ' dRow("Debit") = "" : dRow("Credit") = "" : dRow("PYDebit") = "" : dRow("PYCredit") = ""
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
            sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iACID & " and gl_Status ='A' order by gl_id"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iGroup As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 1 and "
            sSql = sSql & "gl_Parent =" & iGroup & " And gl_CompId =" & iACID & " and gl_Status ='A' order by gl_id"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveOpeningBalance(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsOpeningBalance As clsOpeningBalance)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iOpn_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_SerialNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Date", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_AccHead", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iOpn_AccHead
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GLCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sOpn_GLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_DebitAmt", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dOpn_DebitAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreditAmount", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dOpn_CreditAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iOpn_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iOpn_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GlId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iOpn_GlId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsOpeningBalance.Opn_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CustType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iOpn_CustType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iOpn_IndType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ClosingBalanceDebit", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dOpn_ClosingBalanceDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ClosingBalanceCredit", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dOpn_ClosingBalanceCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GLDesc", OleDb.OleDbType.VarChar, 500)  ' vijayalakshmi 30-07-2019
            'ObjParam(iParamCount).Value = objclsOpeningBalance.sOpn_GLDesc
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spACC_Opening_Balance", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateOBCrDeAmount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustomerID As Integer) As Integer
        Dim sSqlOBGLID As String = "", sSqlATDGLID As String = "", sSqlOBDeCr As String = ""
        Dim dtOBGLIDs As New DataTable, dtATDGLIDs As New DataTable
        Dim dATDDeTotal As Double, dATDCrTotal As Double, dOBDeTotal As Double, dOBDCrTotal As Double
        Dim iCount As Integer = 0
        Try
            sSqlOBGLID = "Select Opn_Glid,Opn_DebitAmt,Opn_CreditAmount From ACC_Opening_Balance Where Opn_CompId=" & iACID & " And Opn_Status='A' And Opn_CustType=" & iCustomerID & ""
            dtOBGLIDs = objDBL.SQLExecuteDataTable(sAC, sSqlOBGLID)

            For i = 0 To dtOBGLIDs.Rows.Count - 1
                iCount = 1
                sSqlATDGLID = "Select ATD_Debit,ATD_Credit From Acc_Transactions_Details Where ATD_GL=" & dtOBGLIDs.Rows(i).Item("Opn_Glid") & " And ATD_Status<>'D' And ATD_CompID=" & iACID & ""
                dtATDGLIDs = objDBL.SQLExecuteDataTable(sAC, sSqlATDGLID)

                dATDDeTotal = 0.00 : dATDCrTotal = 0.00
                For j = 0 To dtATDGLIDs.Rows.Count - 1
                    dATDDeTotal += dtATDGLIDs.Rows(j).Item("ATD_Debit")
                    dATDCrTotal += dtATDGLIDs.Rows(j).Item("ATD_Credit")
                Next

                dOBDeTotal = ((dATDDeTotal) - (dtOBGLIDs.Rows(i).Item("Opn_DebitAmt")))
                dOBDCrTotal = ((dATDCrTotal) - (dtOBGLIDs.Rows(i).Item("Opn_CreditAmount")))

                sSqlOBDeCr = "Update ACC_Opening_Balance set Opn_ClosingBalanceDebit=" & dOBDeTotal & ",Opn_ClosingBalanceCredit=" & dOBDCrTotal & ",Opn_Status='F'"
                sSqlOBDeCr = sSqlOBDeCr & " Where Opn_Glid=" & dtOBGLIDs.Rows(i).Item("Opn_Glid") & " And Opn_CompId=" & iACID & " And Opn_CustType=" & iCustomerID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSqlOBDeCr)
            Next
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOBExists(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustomerID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer = 0
        Try
            sSql = "Select Count(Opn_ID) From ACC_Opening_Balance Where Opn_CompId=" & iACID & " And Opn_Status='A' And Opn_CustType=" & iCustomerID & ""
            iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrgTypeID(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String, ByVal iCustID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select cmm_ID from Content_Management_Master Left join SAD_CUSTOMER_MASTER On CUST_ORGTYPEID=cmm_ID And CUST_CompID=" & iACID & ""
            sSql = sSql & " And CUST_DELFLG='A' where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' And CUST_ID=" & iCustID & " order by cmm_Desc Asc"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionDetails(ByVal sAC As String, ByVal objJE As clsOpeningBalance) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 4)
            ObjParam(iParamCount).Value = objJE.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objJE.sATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objJE.sATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objJE.sATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OrgType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_OrgType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_Transactions_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
