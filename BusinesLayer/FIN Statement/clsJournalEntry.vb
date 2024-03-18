Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data

Public Class clsJournalEntry
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral

    Private Acc_JE_ID As Integer
    Private Acc_JE_TransactionNo As String
    Private Acc_JE_Party As Integer
    Private Acc_JE_Location As Integer
    Private Acc_JE_BillType As Integer
    Private Acc_JE_BillNo As String
    Private Acc_JE_BillDate As Date
    Private Acc_JE_BillAmount As Decimal
    Private Acc_JE_AdvanceAmount As Decimal
    Private Acc_JE_AdvanceNaration As String
    Private Acc_JE_BalanceAmount As Decimal
    Private Acc_JE_NetAmount As Decimal
    Private Acc_JE_PaymentNarration As String
    Private Acc_JE_ChequeNo As String
    Private Acc_JE_ChequeDate As Date
    Private Acc_JE_IFSCCode As String
    Private Acc_JE_BankName As String
    Private Acc_JE_BranchName As String
    Private Acc_JE_CreatedBy As Integer
    Private Acc_JE_YearID As Integer
    Private Acc_JE_CompID As Integer
    Private Acc_JE_Status As String
    Private Acc_JE_Operation As String
    Private Acc_JE_Comments As String
    Private Acc_JE_IPAddress As String
    Private Acc_JE_BillCreatedDate As Date
    Private acc_JE_BranchId As Integer

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

    'JE details Trialbalance

    Private AJTB_ID As Integer
    Private AJTB_MAsID As Integer
    Private AJTB_TranscNo As String

    Private AJTB_CustId As Integer
    Private AJTB_ScheduleTypeid As Integer

    Private AJTB_Deschead As Integer 
Private AJTB_Desc As Integer 
Private AJTB_Debit As Double 
Private AJTB_Credit As  Double 
Private AJTB_CreatedBy As Integer 
Private AJTB_UpdatedBy As Integer 
Private AJTB_Status As String
Private AJTB_IPAddress As String
Private AJTB_CompID As Integer
    Private AJTB_YearID As Integer
    Private AJTB_BillType As Integer
    Private AJTB_DescName As String
    Private AJTB_BranchId As Integer

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
    Public Property dAcc_JE_BalanceAmount() As Decimal
        Get
            Return (Acc_JE_BalanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_BalanceAmount = Value
        End Set
    End Property
    Public Property dAcc_JE_BillCreatedDate() As Date
        Get
            Return (Acc_JE_BillCreatedDate)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_BillCreatedDate = Value
        End Set
    End Property
    Public Property sAcc_JE_IPAddress() As String
        Get
            Return (Acc_JE_IPAddress)
        End Get
        Set(ByVal Value As String)
            Acc_JE_IPAddress = Value
        End Set
    End Property
    Public Property sAcc_JE_Operation() As String
        Get
            Return (Acc_JE_Operation)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Operation = Value
        End Set
    End Property
    Public Property sAcc_JE_Comments() As String
        Get
            Return (Acc_JE_Comments)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Comments = Value
        End Set
    End Property
    Public Property sAcc_JE_Status() As String
        Get
            Return (Acc_JE_Status)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Status = Value
        End Set
    End Property
    Public Property iAcc_JE_CompID() As Integer
        Get
            Return (Acc_JE_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_CompID = Value
        End Set
    End Property
    Public Property iAcc_JE_YearID() As Integer
        Get
            Return (Acc_JE_YearID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_YearID = Value
        End Set
    End Property
    Public Property iAcc_JE_CreatedBy() As Integer
        Get
            Return (Acc_JE_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_CreatedBy = Value
        End Set
    End Property
    Public Property sAcc_JE_BranchName() As String
        Get
            Return (Acc_JE_BranchName)
        End Get
        Set(ByVal Value As String)
            Acc_JE_BranchName = Value
        End Set
    End Property
    Public Property sAcc_JE_BankName() As String
        Get
            Return (Acc_JE_BankName)
        End Get
        Set(ByVal Value As String)
            Acc_JE_BankName = Value
        End Set
    End Property
    Public Property sAcc_JE_IFSCCode() As String
        Get
            Return (Acc_JE_IFSCCode)
        End Get
        Set(ByVal Value As String)
            Acc_JE_IFSCCode = Value
        End Set
    End Property
    Public Property dAcc_JE_ChequeDate() As Date
        Get
            Return (Acc_JE_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_ChequeDate = Value
        End Set
    End Property
    Public Property sAcc_JE_ChequeNo() As String
        Get
            Return (Acc_JE_ChequeNo)
        End Get
        Set(ByVal Value As String)
            Acc_JE_ChequeNo = Value
        End Set
    End Property
    Public Property sAcc_JE_PaymentNarration() As String
        Get
            Return (Acc_JE_PaymentNarration)
        End Get
        Set(ByVal Value As String)
            Acc_JE_PaymentNarration = Value
        End Set
    End Property
    Public Property dAcc_JE_NetAmount() As Decimal
        Get
            Return (Acc_JE_NetAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_NetAmount = Value
        End Set
    End Property
    Public Property sAcc_JE_AdvanceNaration() As String
        Get
            Return (Acc_JE_AdvanceNaration)
        End Get
        Set(ByVal Value As String)
            Acc_JE_AdvanceNaration = Value
        End Set
    End Property
    Public Property dAcc_JE_AdvanceAmount() As Decimal
        Get
            Return (Acc_JE_AdvanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_AdvanceAmount = Value
        End Set
    End Property
    Public Property dAcc_JE_BillAmount() As Decimal
        Get
            Return (Acc_JE_BillAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_BillAmount = Value
        End Set
    End Property
    Public Property dAcc_JE_BillDate() As Date
        Get
            Return (Acc_JE_BillDate)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_BillDate = Value
        End Set
    End Property

    Public Property sAcc_JE_BillNo() As String
        Get
            Return (Acc_JE_BillNo)
        End Get
        Set(ByVal Value As String)
            Acc_JE_BillNo = Value
        End Set
    End Property
    Public Property iAcc_JE_BillType() As Integer
        Get
            Return (Acc_JE_BillType)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_BillType = Value
        End Set
    End Property
    Public Property iAcc_JE_Location() As Integer
        Get
            Return (Acc_JE_Location)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_Location = Value
        End Set
    End Property
    Public Property iAcc_JE_Party() As Integer
        Get
            Return (Acc_JE_Party)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_Party = Value
        End Set
    End Property
    Public Property sAcc_JE_TransactionNo() As String
        Get
            Return (Acc_JE_TransactionNo)
        End Get
        Set(ByVal Value As String)
            Acc_JE_TransactionNo = Value
        End Set
    End Property
    Public Property iAcc_JE_ID() As Integer
        Get
            Return (Acc_JE_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_ID = Value
        End Set
    End Property

    Public Property iacc_JE_BranchId() As Integer
        Get
            Return (acc_JE_BranchId)
        End Get
        Set(ByVal Value As Integer)
            acc_JE_BranchId = Value
        End Set
    End Property
    Public Property iATD_CustId() As Integer
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

    Public Property iAJTB_ID() As Integer
        Get
            Return (AJTB_ID)
        End Get
        Set(ByVal Value As Integer)
            AJTB_ID = Value
        End Set
    End Property
    Public Property iAJTB_MAsID() As Integer
        Get
            Return (AJTB_MAsID)
        End Get
        Set(ByVal Value As Integer)
            AJTB_MAsID = Value
        End Set
    End Property

    Public Property sAJTB_TranscNo() As String
        Get
            Return (AJTB_TranscNo)
        End Get
        Set(ByVal Value As String)
            AJTB_TranscNo = Value
        End Set
    End Property
    Public Property iAJTB_CustId() As Integer
        Get
            Return (AJTB_CustId)
        End Get
        Set(ByVal Value As Integer)
            AJTB_CustId = Value
        End Set
    End Property
    Public Property iAJTB_ScheduleTypeid() As Integer
        Get
            Return (AJTB_ScheduleTypeid)
        End Get
        Set(ByVal Value As Integer)
            AJTB_ScheduleTypeid = Value
        End Set
    End Property

    Public Property iAJTB_Desc() As Integer
        Get
            Return (AJTB_Desc)
        End Get
        Set(ByVal Value As Integer)
            AJTB_Desc = Value
        End Set
    End Property

    Public Property iAJTB_Deschead() As Integer
        Get
            Return (AJTB_Deschead)
        End Get
        Set(ByVal Value As Integer)
            AJTB_Deschead = Value
        End Set
    End Property

    Public Property dAJTB_Debit() As Double
        Get
            Return (AJTB_Debit)
        End Get
        Set(ByVal Value As Double)
            AJTB_Debit = Value
        End Set
    End Property

    Public Property dAJTB_Credit() As Double
        Get
            Return (AJTB_Credit)
        End Get
        Set(ByVal Value As Double)
            AJTB_Credit = Value
        End Set
    End Property
    Public Property iAJTB_CreatedBy() As Integer
        Get
            Return (AJTB_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            AJTB_CreatedBy = Value
        End Set
    End Property

    Public Property iAJTB_UpdatedBy() As Integer
        Get
            Return (AJTB_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            AJTB_UpdatedBy = Value
        End Set
    End Property

    Public Property sAJTB_Status() As String
        Get
            Return (AJTB_Status)
        End Get
        Set(ByVal Value As String)
            AJTB_Status = Value
        End Set
    End Property

    Public Property sAJTB_IPAddress() As String
        Get
            Return (AJTB_IPAddress)
        End Get
        Set(ByVal Value As String)
            AJTB_IPAddress = Value
        End Set
    End Property

    Public Property iAJTB_CompID() As Integer
        Get
            Return (AJTB_CompID)
        End Get
        Set(ByVal Value As Integer)
            AJTB_CompID = Value
        End Set
    End Property

    Public Property iAJTB_YearID() As Integer
        Get
            Return (AJTB_YearID)
        End Get
        Set(ByVal Value As Integer)
            AJTB_YearID = Value
        End Set
    End Property

    Public Property iAJTB_BranchId() As Integer
        Get
            Return (AJTB_BranchId)
        End Get
        Set(ByVal Value As Integer)
            AJTB_BranchId = Value
        End Set
    End Property
    Public Property iAJTB_BillType() As Integer
        Get
            Return (AJTB_BillType)
        End Get
        Set(ByVal Value As Integer)
            AJTB_BillType = Value
        End Set
    End Property

    Public Property sAJTB_DescName() As String
        Get
            Return (AJTB_DescName)
        End Get
        Set(ByVal Value As String)
            AJTB_DescName = Value
        End Set
    End Property

    'JE DashBoard


    Public Function LoadJournalEntry(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal IuserId As Integer, ByVal iStatus As String, ByVal iCustID As Integer, ByVal iYearId As Integer, ByVal iBranchId As Integer) As DataTable
        Dim dt, dt1, dt2 As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim dDebTot, dCredTot As Double
        Dim sCredDescription As String = "", sdebDescription As String = ""
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BranchID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Party", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PartyID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CredDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "select   Acc_JE_ID,Acc_JE_Party,Acc_JE_Status,Acc_JE_TransactionNo,acc_JE_BranchId,Acc_JE_BillNo,Acc_JE_BillDate,Acc_JE_BillType from Acc_JE_Master  where Acc_JE_Party=" & iCustID & " And Acc_JE_CompID =" & iCompID & "  And Acc_JE_YearId=" & iYearId & "  "

            If iStatus = 0 Then
                sSql = sSql & " And Acc_JE_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_JE_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_JE_Status='W'" 'Waiting for approval
            End If
            If iBranchId <> 0 Then
                sSql = sSql & " And acc_je_BranchID = " & iBranchId & ""
            End If
            sSql = sSql & " Order By Acc_JE_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("acc_JE_BranchId").ToString()) = False Then
                        dr("BranchID") = ds.Tables(0).Rows(i)("acc_JE_BranchId").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillNo").ToString()) = False Then
                        dr("BillNo") = ""
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString() = "01/01/1900 00:00:00" Then
                            dr("BillDate") = ""
                        Else
                            dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString(), "D")
                        End If
                    End If
                    'If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString()) = False Then
                    '    dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString())
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_Party").ToString()) = False Then
                        dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_Party").ToString())
                        dr("PartyID") = ds.Tables(0).Rows(i)("Acc_JE_Party").ToString()
                    End If
                    sSql = "Select sum(AJTB_Debit) as AJTB_Debit,sum(AJTB_Credit) as AJTB_Credit,AJTB_Deschead,AJTB_DescName from Acc_JETransactions_Details Where Ajtb_Masid=" & ds.Tables(0).Rows(i)("Acc_JE_ID") & "  and  AJTB_CustId=" & iCustID & "  group by AJTB_Debit,AJTB_Credit,AJTB_Deschead,AJTB_DescName "
                    dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For j = 0 To dt1.Rows.Count - 1
                            dDebTot = dDebTot + dt1.Rows(j)("AJTB_Debit").ToString()
                            dCredTot = dCredTot + dt1.Rows(j)("AJTB_Credit").ToString()
                            If Val(dt1.Rows(j)("AJTB_Debit").ToString()) <> 0 Then
                                sdebDescription = sdebDescription & "," & dt1.Rows(j)("AJTB_DescName").ToString()
                            Else
                                sCredDescription = sCredDescription & "," & dt1.Rows(j)("AJTB_DescName").ToString()
                            End If
                            'dr("Description") = ""
                            'sSql = "Select ATBU_Description from Acc_TrailBalance_Upload Where ATBU_ID=" & dt1.Rows(j)("AJTB_Deschead") & " "
                            'dt2 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                            'If dt2.Rows.Count > 0 Then
                            '    If Val(dt1.Rows(j)("AJTB_Debit").ToString()) <> 0 Then
                            '        sdebDescription = sdebDescription & "," & dt2.Rows(0)("ATBU_Description").ToString()
                            '    Else
                            '        sCredDescription = sCredDescription & "," & dt2.Rows(0)("ATBU_Description").ToString()
                            '    End If

                            'Else
                            '    dr("Description") = ""
                            'End If
                        Next
                        If sdebDescription.StartsWith(",") = True Then
                            sdebDescription = sdebDescription.Remove(0, 1)
                        End If
                        If sCredDescription.StartsWith(",") = True Then
                            sCredDescription = sCredDescription.Remove(0, 1)
                        End If
                    End If
                    dr("Debit") = dDebTot
                    dr("Credit") = dCredTot

                    dr("DebDescription") = sdebDescription
                    dr("CredDescription") = sCredDescription
                    dDebTot = 0 : dCredTot = 0
                    sdebDescription = "" : sCredDescription = ""
                    If (ds.Tables(0).Rows(i)("Acc_JE_Status") = "W") Then
                        dr("Status") = "Waiting For Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "D") Then
                        dr("Status") = "De-Activated"
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "0") Then
                        dr("BillType") = ""
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "1") Then
                        dr("BillType") = "Payment"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "2") Then
                        dr("BillType") = "Reciept"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "3") Then
                        dr("BillType") = "Pettty Cash"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "4") Then
                        dr("BillType") = "Purchase"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "5") Then
                        dr("BillType") = "Sales"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "6") Then
                        dr("BillType") = "Others"
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load Jedetails to approve LoadMasterJournalEntryApprove
    Public Function LoadMasterJournalEntryApprove(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal IuserId As Integer, ByVal iStatus As String, ByVal iCustID As Integer, ByVal iYearId As Integer, ByVal iBranchId As Integer, ByVal IjeMasterid As Integer) As DataTable
        Dim dt, dt1, dt2 As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim dDebTot, dCredTot As Double
        Dim sCredDescription, sdebDescription As String
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Party", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PartyID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CredDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Comments", GetType(String))
            dt.Columns.Add(dc)


            sSql = "select   Acc_JE_ID,Acc_JE_Party,Acc_JE_Status,Acc_JE_TransactionNo,Acc_JE_BillNo,Acc_JE_BillDate,Acc_JE_BillType,Acc_JE_Comnments from Acc_JE_Master  where Acc_JE_Party=" & iCustID & " And Acc_JE_CompID =" & iCompID & "  And Acc_JE_YearId=" & iYearId & "  and Acc_JE_ID=" & IjeMasterid & ""

            If iStatus = 0 Then
                sSql = sSql & " And Acc_JE_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_JE_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_JE_Status='W'" 'Waiting for approval
            End If
            If iBranchId <> 0 Then
                sSql = sSql & " And acc_je_BranchID = " & iBranchId & ""
            End If
            sSql = sSql & " Order By Acc_JE_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillNo").ToString()) = False Then
                        dr("BillNo") = ""
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString() = "01/01/1900 00:00:00" Then
                            dr("BillDate") = ""
                        Else
                            dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString(), "D")
                        End If
                    End If
                    'If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString()) = False Then
                    '    dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString())
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_Party").ToString()) = False Then
                        dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_Party").ToString())
                        dr("PartyID") = ds.Tables(0).Rows(i)("Acc_JE_Party").ToString()
                    End If
                    sSql = "Select sum(AJTB_Debit) as AJTB_Debit,sum(AJTB_Credit) as AJTB_Credit,AJTB_Deschead from Acc_JETransactions_Details Where Ajtb_Masid=" & ds.Tables(0).Rows(i)("Acc_JE_ID") & " group by AJTB_Debit,AJTB_Credit,AJTB_Deschead "
                    dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For j = 0 To dt1.Rows.Count - 1
                            dDebTot = dDebTot + dt1.Rows(j)("AJTB_Debit").ToString()
                            dCredTot = dCredTot + dt1.Rows(j)("AJTB_Credit").ToString()
                            sSql = "Select ATBU_Description from Acc_TrailBalance_Upload Where ATBU_ID=" & dt1.Rows(j)("AJTB_Deschead") & " "
                            dt2 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                            If dt2.Rows.Count > 0 Then
                                If Val(dt1.Rows(j)("AJTB_Debit").ToString()) <> 0 Then
                                    sdebDescription = sdebDescription & "," & dt2.Rows(0)("ATBU_Description").ToString()
                                Else
                                    sCredDescription = sCredDescription & "," & dt2.Rows(0)("ATBU_Description").ToString()
                                End If

                            Else
                                dr("Description") = ""
                            End If
                        Next
                        If sdebDescription.StartsWith(",") = True Then
                            sdebDescription = sdebDescription.Remove(0, 1)
                        End If
                        If sCredDescription.StartsWith(",") = True Then
                            sCredDescription = sCredDescription.Remove(0, 1)
                        End If
                    End If
                    dr("Debit") = dDebTot
                    dr("Credit") = dCredTot

                    dr("DebDescription") = sdebDescription
                    dr("CredDescription") = sCredDescription
                    dDebTot = 0 : dCredTot = 0
                    sdebDescription = "" : sCredDescription = ""
                    If (ds.Tables(0).Rows(i)("Acc_JE_Status") = "W") Then
                        dr("Status") = "Waiting For Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "D") Then
                        dr("Status") = "De-Activated"
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_JE_Comnments") <> "") Then
                        dr("Comments") = ds.Tables(0).Rows(i)("Acc_JE_Comnments")
                    Else
                        dr("BillType") = ""
                    End If
                    If (ds.Tables(0).Rows(i)("Acc_JE_BillType") <> "0") Then
                        dr("BillType") = ds.Tables(0).Rows(i)("Acc_JE_BillType")
                    Else
                        dr("BillType") = ""
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load Jedetails to approve LoadMasterJournalEntryApprove
    Public Function LoadJournalEntrydetailsApprove(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal IuserId As Integer, ByVal iStatus As String, ByVal iCustID As Integer, ByVal iYearId As Integer, ByVal iBranchId As Integer) As DataTable
        Dim dt, dt1, dt2 As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim dDebTot, dCredTot As Double
        Dim sCredDescription, sdebDescription As String
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Party", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PartyID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CredDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "select   Acc_JE_ID,Acc_JE_Party,Acc_JE_Status,Acc_JE_TransactionNo,Acc_JE_BillNo,Acc_JE_BillDate,Acc_JE_BillType from Acc_JE_Master  where Acc_JE_Party=" & iCustID & " And Acc_JE_CompID =" & iCompID & "  And Acc_JE_YearId=" & iYearId & "  "

            If iStatus = 0 Then
                sSql = sSql & " And Acc_JE_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_JE_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_JE_Status='W'" 'Waiting for approval
            End If
            If iBranchId <> 0 Then
                sSql = sSql & " And acc_je_BranchID = " & iBranchId & ""
            End If
            sSql = sSql & " Order By Acc_JE_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillNo").ToString()) = False Then
                        dr("BillNo") = ""
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString() = "01/01/1900 00:00:00" Then
                            dr("BillDate") = ""
                        Else
                            dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_JE_BillDate").ToString(), "D")
                        End If
                    End If
                    'If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString()) = False Then
                    '    dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString())
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_Party").ToString()) = False Then
                        dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_Party").ToString())
                        dr("PartyID") = ds.Tables(0).Rows(i)("Acc_JE_Party").ToString()
                    End If
                    sSql = "Select sum(AJTB_Debit) as AJTB_Debit,sum(AJTB_Credit) as AJTB_Credit,AJTB_Deschead from Acc_JETransactions_Details Where Ajtb_Masid=" & ds.Tables(0).Rows(i)("Acc_JE_ID") & " group by AJTB_Debit,AJTB_Credit,AJTB_Deschead "
                    dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For j = 0 To dt1.Rows.Count - 1
                            dDebTot = dDebTot + dt1.Rows(j)("AJTB_Debit").ToString()
                            dCredTot = dCredTot + dt1.Rows(j)("AJTB_Credit").ToString()
                            sSql = "Select ATBU_Description from Acc_TrailBalance_Upload Where ATBU_ID=" & dt1.Rows(j)("AJTB_Deschead") & " "
                            dt2 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                            If dt2.Rows.Count > 0 Then
                                If Val(dt1.Rows(j)("AJTB_Debit").ToString()) <> 0 Then
                                    sdebDescription = sdebDescription & "," & dt2.Rows(0)("ATBU_Description").ToString()
                                Else
                                    sCredDescription = sCredDescription & "," & dt2.Rows(0)("ATBU_Description").ToString()
                                End If

                            Else
                                dr("Description") = ""
                            End If
                        Next
                        If sdebDescription.StartsWith(",") = True Then
                            sdebDescription = sdebDescription.Remove(0, 1)
                        End If
                        If sCredDescription.StartsWith(",") = True Then
                            sCredDescription = sCredDescription.Remove(0, 1)
                        End If
                    End If
                    dr("Debit") = dDebTot
                    dr("Credit") = dCredTot

                    dr("DebDescription") = sdebDescription
                    dr("CredDescription") = sCredDescription
                    dDebTot = 0 : dCredTot = 0
                    sdebDescription = "" : sCredDescription = ""
                    If (ds.Tables(0).Rows(i)("Acc_JE_Status") = "W") Then
                        dr("Status") = "Waiting For Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "D") Then
                        dr("Status") = "De-Activated"
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "0") Then
                        dr("BillType") = ""
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "1") Then
                        dr("BillType") = "Payment"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "2") Then
                        dr("BillType") = "Reciept"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "3") Then
                        dr("BillType") = "Pettty Cash"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "4") Then
                        dr("BillType") = "Purchase"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "5") Then
                        dr("BillType") = "Sales"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_BillType") = "6") Then
                        dr("BillType") = "Others"
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    '    Public Function LoadJournalEntry(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sCustomerAudit As String) As DataTable
    '        Dim dt As New DataTable, dtDetails As New DataTable
    '        Dim sSql As String = ""
    '        Dim dr As DataRow
    '        Dim i As Integer = 0
    '        Try
    '            dt.Columns.Add("Id")
    '            dt.Columns.Add("TransactionNo")
    '            dt.Columns.Add("BillNo")
    '            dt.Columns.Add("BillDate")
    '            dt.Columns.Add("Party")
    '            dt.Columns.Add("BillType")
    '            dt.Columns.Add("Status")

    '            sSql = "Select * from Acc_JE_Master where Acc_JE_CompID=" & iACID & " Order By Acc_JE_ID ASC"
    '            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)

    '            If dtDetails.Rows.Count > 0 Then
    '                For i = 0 To dtDetails.Rows.Count - 1
    '                    dr = dt.NewRow

    '                    If IsDBNull(dtDetails.Rows(i)("Acc_JE_ID").ToString()) = False Then
    '                        dr("Id") = dtDetails.Rows(i)("Acc_JE_ID").ToString()
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(i)("Acc_JE_TransactionNo").ToString()) = False Then
    '                        dr("TransactionNo") = dtDetails.Rows(i)("Acc_JE_TransactionNo").ToString()
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(i)("Acc_JE_BillNo").ToString()) = False Then
    '                        dr("BillNo") = dtDetails.Rows(i)("Acc_JE_BillNo").ToString()
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(i)("Acc_JE_BillDate").ToString()) = False Then
    '                        If objGen.FormatDtForRDBMS(dtDetails.Rows(i)("Acc_JE_BillDate"), "D") = "01/01/1900" Then
    '                            dr("BillDate") = ""
    '                        Else
    '                            dr("BillDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("Acc_JE_BillDate"), "D")
    '                        End If
    '                    Else
    '                        dr("BillDate") = ""
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(i)("Acc_JE_BillType").ToString()) = False Then
    '                        dr("BillType") = GetBillType(sAC, iACID, dtDetails.Rows(i)("Acc_JE_BillType").ToString())
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(i)("Acc_JE_Party").ToString()) = False Then
    '                        dr("Party") = GetPartyName(sAC, iACID, dtDetails.Rows(i)("Acc_JE_Party").ToString())
    '                    End If

    '                    If IsDBNull(dtDetails.Rows(i)("Acc_JE_Status").ToString()) = False Then
    '                        If (dtDetails.Rows(i)("Acc_JE_Status") = "WC") Then
    '                            If ((dtDetails.Rows(i)("Acc_JE_Status") = "WC") And (dtDetails.Rows(i)("Acc_JE_CreatedBy") = iUserID)) Then
    '                                dr("Status") = "Waiting For Approval(Customer)"
    '                            Else
    '                                GoTo NextLoop
    '                            End If

    '                        ElseIf (dtDetails.Rows(i)("Acc_JE_Status") = "WA") Then
    '                            If ((dtDetails.Rows(i)("Acc_JE_Status") = "WA") And (dtDetails.Rows(i)("Acc_JE_CreatedBy") = iUserID)) Then
    '                                dr("Status") = "Waiting For Approval(Auditor)"
    '                            Else
    '                                GoTo NextLoop
    '                            End If

    '                        ElseIf (dtDetails.Rows(i)("Acc_JE_Status") = "S") Then
    '                            dr("Status") = "Completed"
    '                        ElseIf (dtDetails.Rows(i)("Acc_JE_Status") = "C") Then
    '                            dr("Status") = "Waiting For Approval(Auditor)"
    '                        ElseIf (dtDetails.Rows(i)("Acc_JE_Status") = "A") Then
    '                            dr("Status") = "Approved(Customer)"
    '                        End If
    '                    End If
    '                    dt.Rows.Add(dr)
    'NextLoop:       Next
    '            End If
    '            Return dt
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    Public Function GetPartyName(ByVal sAC As String, ByVal iACID As Integer, ByVal iParty As Integer) As String
        Dim sSQL As String = ""
        Dim sParty As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "Select *  from SAD_CUSTOMER_MASTER where CUST_DELFLG ='A' and CUST_ID = " & iParty & " and CUST_CompID= " & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("CUST_NAME").ToString()) = False Then
                    sParty = dt.Rows(0)("CUST_NAME").ToString() & " - " & dt.Rows(0)("CUST_Code").ToString()
                Else
                    sParty = ""
                End If
            Else
                sParty = ""
            End If
            Return sParty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBillType(ByVal sAC As String, ByVal iACID As Integer, ByVal iBillType As Integer) As String
        Dim sSQL As String = "", sBillType As String = ""
        Dim dt As New DataTable
        Try
            'sSQL = "Select * from ACC_General_Master where mas_master = 9 and mas_Delflag ='A' and Mas_ID = " & iBillType & " and mas_CompID =" & iACID & ""
            'dt = objDBL.SQLExecuteDataSet(sAC, sSQL).Tables(0)
            'If dt.Rows.Count > 0 Then
            '    If IsDBNull(dt.Rows(0)("Mas_Desc").ToString()) = False Then
            '        sBillType = dt.Rows(0)("Mas_Desc").ToString()
            '    Else
            '        sBillType = ""
            '    End If
            'Else
            '    sBillType = ""
            'End If
            'Return sBillType

            sSQL = "select * from content_management_master where cmm_Delflag='A' and cmm_id=" & iBillType & " and cmm_compid=" & iACID & " and cmm_Category='BT'"     'vijaylakshmi
            dt = objDBL.SQLExecuteDataSet(sAC, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("cmm_Desc").ToString()) = False Then
                    sBillType = dt.Rows(0)("cmm_Desc").ToString()
                Else
                    sBillType = ""
                End If
            Else
                sBillType = ""
            End If
            Return sBillType
        Catch ex As Exception
            Throw
        End Try
    End Function


    'JE Details Form
    Public Function LoadSubGLDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            'sSql = sSql & "gl_compid=" & iACID & " and gl_status='A' and gl_Delflag ='C' and gl_head=3 order by gl_AccHead"

            sSql = "Select cc_gl as gl_id,CC_GLCode + '-' + CC_Gldesc as GlDesc from Customer_coa where "
            sSql = sSql & "CC_compid=" & iACID & "  and CC_head=3 and CC_CustId=" & iCustid & " order by CC_gl"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingVoucherNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCustomerAudit As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Acc_JE_TransactionNo,Acc_JE_ID from Acc_JE_Master where Acc_JE_CompID=" & iACID & " And Acc_JE_Status<>'D' And Acc_JE_YearID=" & iYearID & " order by Acc_JE_ID Desc" '(Acc_JE_Status='W' or Acc_JE_Status='A' or Acc_JE_Status='S') And
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
            'If sCustomerAudit <> "" Then
            '    If sCustomerAudit = "Customer" Then
            '        sSql = "Select Acc_JE_TransactionNo,Acc_JE_ID from Acc_JE_Master where Acc_JE_CompID=" & iACID & " And Acc_JE_Status<>'D' And Acc_JE_YearID=" & iYearID & " order by Acc_JE_ID Desc" '(Acc_JE_Status='W' or Acc_JE_Status='A' or Acc_JE_Status='S') And
            '    ElseIf sCustomerAudit = "Auditor" Then
            '        sSql = "Select Acc_JE_TransactionNo,Acc_JE_ID from Acc_JE_Master where Acc_JE_CompID=" & iACID & " And Acc_JE_Status<>'D' And Acc_JE_YearID=" & iYearID & " order by Acc_JE_ID Desc" 'And (Acc_JE_Status='W' or Acc_JE_Status='C' or Acc_JE_Status='S')
            '    End If
            ' dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            ' Return dt
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExistingVoucherNos(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCustomerAudit As String, ByVal id As Integer, ByVal iBranchId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Acc_JE_TransactionNo,Acc_JE_ID from Acc_JE_Master where Acc_JE_CompID=" & iACID & " And Acc_JE_Status<>'D' And Acc_JE_YearID=" & iYearID & " and Acc_JE_Party=" & id & "" ' order by Acc_JE_ID Desc" 'And (Acc_JE_Status='W' or Acc_JE_Status='C' or Acc_JE_Status='S')
            If iBranchId <> 0 Then
                sSql = sSql & " And acc_je_BranchID = " & iBranchId & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadBillType(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CMM_ID,CMM_Desc From Content_Management_Master Where CMM_DelFlag='A' And CMM_Category='BT' And CMM_CompID=" & iACID & " Order By CMM_Desc ASC"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadParty(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select ACM_ID,ACM_Name + ' - ' + ACM_Code as Name  from Acc_Customer_Master where ACM_Status='A' and ACM_CompID =" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDeschead(ByVal sAC As String, ByVal iACID As Integer, ByVal IcustId As Integer, ByVal iYearId As Integer, ByVal iBranchId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select ATBU_ID,ATBU_Description from Acc_TrailBalance_Upload where ATBU_STATUS='C' And ATBU_CustId = " & IcustId & " And ATBU_CompId= " & iACID & " and ATBU_YEARId=" & iYearId & " and ATBU_Branchid=" & iBranchId & " "
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadParty(ByVal sAC As String, ByVal iACID As Integer, ByVal iType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iType = 1 Then
                sSql = "Select ACM_ID,ACM_Code + ' - ' + ACM_Name as Name  from Acc_Customer_Master where ACM_Status='A' and ACM_Type = 'C' and ACM_CompID =" & iACID & ""
            ElseIf iType = 2 Then
                sSql = "Select ACM_ID,ACM_Code + ' - ' + ACM_Name as Name  from Acc_Customer_Master where ACM_Status='A' and ACM_Type ='S' and ACM_CompID =" & iACID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomers(sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CUST_ID,CUST_NAME as Name from SAD_CUSTOMER_MASTER where CUST_DELFLG='A' and CUST_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllGLCodes(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iACID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' order by gl_glcode"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGLCodes(ByVal sAC As String, ByVal iACID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iACID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLCodes(ByVal sAC As String, ByVal iACID As Integer, ByVal iglID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iACID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3 And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChartOfAccountHead(ByVal sAC As String, ByVal iACID As Integer, ByVal iGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccHead As Integer = 0
        Try
            sSql = "Select gl_AccHead from Chart_of_Accounts where gl_id =" & iGL & " and gl_CompID =" & iACID & ""
            iAccHead = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iAccHead
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetParent(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iParent As Integer = 0
        Try
            sSql = "Select gl_Parent from Chart_of_Accounts where gl_id =" & iSubGL & " and gl_CompID =" & iACID & ""
            iParent = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPaymentTypeDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_JE_Master where Acc_JE_ID =" & iPaymentID & " And Acc_JE_CompID =" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTransactionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iJEPKID As Integer, ByVal iBranchId As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dt.Columns.Add("detID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("PaymentID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Type")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")

            'sSql = "Select Distinct(ATD_ID),ATD_Head,ATD_GL,ATD_SubGL,ATD_PaymentType,ATD_Debit,ATD_Credit,a.gl_glCode As GlCode,a.gl_Desc As GlDescription,"
            'sSql = sSql & " b.gl_glCode As SubGlCode,b.Gl_Desc As SubGlDesc From Acc_Transactions_Details Left Join Chart_of_Accounts a On a.gl_id=ATD_GL And a.gl_CompId=" & iACID & ""
            'sSql = sSql & " Left Join Chart_of_Accounts b On b.gl_id=ATD_SubGL And b.gl_CompId=" & iACID & " Where ATD_BillId=" & iJEID & " And ATD_TrType=4 And ATD_CompID=" & iACID & " order by Atd_id"

            sSql = "" : sSql = "select Ajtb_id, ajtb_deschead,ATBU_Description,AJTB_Debit,AJTB_Credit,AJTB_YearID from Acc_JETransactions_Details
                        left join Acc_TrailBalance_Upload b on b.ATBU_ID=ajtb_deschead"
            sSql = sSql & " left join Acc_JE_Master c on c.Acc_JE_ID=Ajtb_Masid"
            sSql = sSql & " Where  ajtb_custid= " & iCustID & " And AJTB_CompID= " & iACID & " And AJTB_YearID= " & iYearID & "  And c.Acc_JE_ID=" & iJEPKID & ""
            If iBranchId <> 0 Then
                sSql = sSql & " And ajtb_BranchID = " & iBranchId & ""
            End If
            sSql = sSql & " order by AJTB_ID"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dr = dt.NewRow
                    dr("SrNo") = i + 1

                    If IsDBNull(dtDetails.Rows(i)("Ajtb_id").ToString()) = False Then
                        dr("detID") = dtDetails.Rows(i)("Ajtb_id").ToString()
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ajtb_deschead").ToString()) = False Then
                        dr("HeadID") = dtDetails.Rows(i)("ajtb_deschead").ToString()
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ajtb_deschead").ToString()) = False Then
                        dr("GLID") = dtDetails.Rows(i)("ajtb_deschead").ToString()
                    End If

                    'If IsDBNull(dtDetails.Rows(i)("ATD_SubGL").ToString()) = False Then
                    '    dr("SubGLID") = dtDetails.Rows(i)("ATD_SubGL").ToString()
                    'End If

                    'If IsDBNull(dtDetails.Rows(i)("ATD_PaymentType").ToString()) = False Then
                    '    dr("PaymentID") = dtDetails.Rows(i)("ATD_PaymentType").ToString()

                    '    If dtDetails.Rows(i)("ATD_PaymentType").ToString() = "1" Then
                    '        dr("Type") = "Advance Payment"
                    '    ElseIf dtDetails.Rows(i)("ATD_PaymentType").ToString() = "2" Then
                    '        dr("Type") = "Bill Passing"
                    '    ElseIf dtDetails.Rows(i)("ATD_PaymentType").ToString() = "3" Then
                    '        dr("Type") = "Payment"
                    '    ElseIf dtDetails.Rows(i)("ATD_PaymentType").ToString() = "4" Then
                    '        dr("Type") = "Cheque"
                    '    End If
                    'End If

                    'If IsDBNull(dtDetails.Rows(i)("GLCode").ToString()) = False Then
                    '    dr("GLCode") = dtDetails.Rows(i)("GLCode").ToString()
                    'End If
                    dr("GLCode") = ""

                    If IsDBNull(dtDetails.Rows(i)("ATBU_Description").ToString()) = False Then
                        dr("GLDescription") = dtDetails.Rows(i)("ATBU_Description").ToString()
                    End If

                    'If IsDBNull(dtDetails.Rows(i)("SubGLCode").ToString()) = False Then
                    '    dr("SubGL") = dtDetails.Rows(i)("SubGLCode").ToString()
                    'End If
                    dr("SubGL") = ""

                    'If IsDBNull(dtDetails.Rows(i)("SubGLDesc").ToString()) = False Then
                    '    dr("SubGLDescription") = dtDetails.Rows(i)("SubGLDesc").ToString()
                    'End If

                    If IsDBNull(dtDetails.Rows(i)("AJTB_Debit").ToString()) = False Then
                        If dtDetails.Rows(i)("AJTB_Debit") <> "0.00" Then
                            dr("Debit") = dtDetails.Rows(i)("AJTB_Debit").ToString()
                        Else
                            dr("Debit") = "0.00"
                        End If

                    End If

                    If IsDBNull(dtDetails.Rows(i)("AJTB_Credit").ToString()) = False Then
                        If dtDetails.Rows(i)("AJTB_Credit") <> "0.00" Then
                            dr("Credit") = dtDetails.Rows(i)("AJTB_Credit").ToString()
                        Else
                            dr("Credit") = "0.00"
                        End If
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetJEHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iJEPKID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Date")
            dtTab.Columns.Add("User")
            dtTab.Columns.Add("Comments")
            dtTab.Columns.Add("Status")
            sSql = "Select AJEH_Date,AJEH_Comments,Usr_FullName,AJEH_Status From Acc_JE_Master_History,Sad_UserDetails Where"
            sSql = sSql & " AJEH_UserID=Usr_ID And AJEH_AccJEID=" & iJEPKID & " And AJEH_CompID=" & iACID & " Order by AJEH_PKID Desc"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("AJEH_Date")) = False Then
                    dr("Date") = objGen.FormatDtForRDBMS(dt.Rows(i)("AJEH_Date"), "F")
                End If
                dr("User") = objGen.ReplaceSafeSQL(dt.Rows(i)("Usr_FullName"))
                dr("Comments") = objGen.ReplaceSafeSQL(dt.Rows(i)("AJEH_Comments"))
                dr("Status") = dt.Rows(i)("AJEH_Status")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateTransactionNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearId As Integer, ByVal iParty As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim dtDetails As New DataTable
        Try
            iMax = objDBL.SQLExecuteScalar(sAC, "Select isnull(max(Acc_JE_ID)+1,1) from Acc_JE_Master where Acc_JE_YearID= " & iYearId & " and  Acc_JE_Party = " & iParty & " ")

            sPrefix = "JE00-" & iMax

            'sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 4  and AVS_CompID = " & iACID & ""
            'dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            'If dtDetails.Rows.Count > 0 Then
            '    sPrefix = dtDetails.Rows(0)("AVS_Prefix").ToString() & "00" & iMax
            'Else
            '    sPrefix = ""
            'End If
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function SaveTransactionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal objJE As clsJournalEntry)
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Try
    '        iMax = objDBL.SQLExecuteScalar(sAC, "Select isnull(max(ATD_ID)+1,1) from Acc_Transactions_Details")
    '        sSql = "" : sSql = "Insert into Acc_Transactions_Details(ATD_ID,ATD_TransactionDate,ATD_TrType,"
    '        sSql = sSql & "ATD_BillId,ATD_PaymentType,ATD_Head,"
    '        sSql = sSql & "ATD_GL,ATD_SubGL,ATD_Debit,ATD_Credit,"
    '        sSql = sSql & "ATD_CreatedOn,ATD_CreatedBy,ATD_Status,"
    '        sSql = sSql & "ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress)"
    '        sSql = sSql & "Values(" & iMax & ",GetDate()," & objJE.iATD_TrType & ","
    '        sSql = sSql & "" & objJE.iATD_BillId & "," & objJE.iATD_PaymentType & "," & objJE.iATD_Head & ","
    '        sSql = sSql & "" & objJE.iATD_GL & "," & objJE.iATD_SubGL & "," & objJE.dATD_Debit & "," & objJE.dATD_Credit & ","
    '        sSql = sSql & "GetDate()," & objJE.iATD_CreatedOn & ",'" & objJE.sATD_Status & "',"
    '        sSql = sSql & "" & objJE.iATD_YearID & "," & iACID & ",'" & objJE.sATD_Operation & "','" & objJE.sATD_IPAddress & "')"
    '        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function UpdateTransactionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPaymentType As Integer, ByVal objJE As clsJournalEntry)
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Try
    '        sSql = "" : sSql = "Update Acc_Transactions_Details set ATD_Head=" & objJE.iATD_Head & ",ATD_GL=" & objJE.iATD_GL & ","
    '        sSql = sSql & "ATD_SubGL= " & objJE.iATD_SubGL & ",ATD_DbOrCr=" & objJE.iATD_DbOrCr & ","
    '        sSql = sSql & "ATD_Debit= " & objJE.dATD_Debit & ",ATD_Credit=" & objJE.dATD_Credit & " where"
    '        sSql = sSql & "ATD_BillID= " & objJE.iATD_ID & " and ATD_TrType=" & objJE.iATD_TrType & " and ATD_CompID=" & iACID & ""
    '        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SaveChequeDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal objJE As clsJournalEntry)
        Dim sSql As String = ""
        Try
            sSql = "Update acc_JE_Master set ACC_JE_ChequeNo = " & objJE.sAcc_JE_ChequeNo & ",Acc_JE_ChequeDate=" & objGen.FormatDtForRDBMS(objJE.dAcc_JE_ChequeDate, "I") & ","
            sSql = sSql & "Acc_JE_IFSCCode = '" & objGen.SafeSQL(objJE.sAcc_JE_IFSCCode) & "',ACC_JE_BankName='" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "',"
            sSql = sSql & "Acc_JE_BranchName = '" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "' where Acc_JE_ID=" & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeletePaymentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iTransactionID As Integer, ByVal sType As String)
        Dim sSql As String = ""
        Try
            If sType = "PKID" Then
                sSql = "Delete from Acc_Transactions_Details where ATD_Id=" & iTransactionID & " and Atd_CompID=" & iACID & ""
            ElseIf sType = "BILLID" Then
                sSql = "Delete from Acc_Transactions_Details where ATD_BillId=" & iTransactionID & " and Atd_CompID=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetJEStatus(sAC As String, ByVal iACID As Integer, ByVal iJEID As Integer) As String
        Dim sSql As String = "", sStatus As String
        Try
            sSql = "Select Acc_JE_Status from Acc_JE_Master Where Acc_JE_ID=" & iJEID & " And Acc_JE_CompID=" & iACID & ""
            sStatus = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditEmpList(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select usr_Id,Usr_FullName From Sad_UserDetails Left Join SAD_GRPORLVL_GENERAL_MASTER On Mas_ID=Usr_Role And Mas_DelFlag='A' And Mas_CompID=" & iACID & ""
            sSql = sSql & " Where Mas_Code='AU' And Usr_CompId=" & iACID & " And usr_DelFlag='A' Order by Usr_FullName Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerEmpList(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select a.usr_id,a.Usr_FullName,a.usr_Designation,a.Usr_MasterRole,a.Usr_AuditRole,a.Usr_RiskRole,a.Usr_ComplianceRole,a.Usr_BCMRole"
            sSql = sSql & " from sad_userdetails a"
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master b On a.usr_Designation=b.mas_ID "
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master d On a.Usr_MasterRole=d.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master e On a.Usr_AuditRole=e.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master f On a.Usr_RiskRole=f.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master g On a.Usr_ComplianceRole=g.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master h On a.Usr_BCMRole=h.mas_ID "
            sSql = sSql & " where Usr_CompID=" & iACID & " and Usr_Node=0 and Usr_OrgnID=0 And a.usr_DelFlag='A'"
            sSql = sSql & " order by Usr_FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CusormeCheck(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String = ""
        Try
            'sSql = "Select a.usr_id,a.Usr_FullName,a.usr_Designation,a.Usr_MasterRole,a.Usr_AuditRole,a.Usr_RiskRole,a.Usr_ComplianceRole,a.Usr_BCMRole"
            'sSql = sSql & " from sad_userdetails a"
            'sSql = sSql & " Left Join SAD_GRPDESGN_General_Master b On a.usr_Designation=b.mas_ID "
            'sSql = sSql & " left join SAD_GrpOrLvl_General_Master d On a.Usr_MasterRole=d.mas_ID"
            'sSql = sSql & " left join SAD_GrpOrLvl_General_Master e On a.Usr_AuditRole=e.mas_ID"
            'sSql = sSql & " left join SAD_GrpOrLvl_General_Master f On a.Usr_RiskRole=f.mas_ID"
            'sSql = sSql & " left join SAD_GrpOrLvl_General_Master g On a.Usr_ComplianceRole=g.mas_ID"
            'sSql = sSql & " left join SAD_GrpOrLvl_General_Master h On a.Usr_BCMRole=h.mas_ID "
            'sSql = sSql & " where Usr_CompID=" & iACID & " and Usr_Node=0 and Usr_OrgnID=0 And a.usr_DelFlag='A'"
            'sSql = sSql & " order by Usr_FullName"
            sSql = "select * from sad_userdetails where usr_id=" & iACID & " "
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdateJEMasterStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal sCustomerAudit As String)
        Dim sSql As String = ""
        Dim iSequenceNum As Integer
        Try
            sSql = "Update Acc_JE_Master Set Acc_JE_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_JE_Status='A',Acc_JE_ApprovedBy=" & iUserID & ",Acc_JE_ApprovedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_JE_Status='A',Acc_JE_ApprovedBy=" & iUserID & ",Acc_JE_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_JE_Status='D',Acc_JE_ApprovedBy=" & iUserID & ",Acc_JE_ApprovedOn=GetDate()"
            End If
            sSql = sSql & " Where Acc_JE_ID=" & iMasId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)




        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub UpdateAccTransactionDetailsStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal sCustomerAudit As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_JETransactions_Details Set AJTB_IPAddress='" & sIPAddress & "',"
            If sCustomerAudit = "Customer" Then
                If sStatus = "W" Then
                    sSql = sSql & " AJTB_Status='A',AJTB_ApprovedBy=" & iUserID & ",AJTB_ApprovedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " AJTB_Status='A',AJTB_ApprovedBy=" & iUserID & ",AJTB_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " AJTB_Status='D',AJTB_ApprovedBy=" & iUserID & ",AJTB_ApprovedOn=GetDate()"
                End If
            End If

            If sCustomerAudit = "Auditor" Then
                If sStatus = "W" Then
                    sSql = sSql & " AJTB_Status='A',AJTB_ApprovedBy=" & iUserID & ",AJTB_ApprovedOn=GetDate()"
                ElseIf sStatus = "C" Then
                    sSql = sSql & " AJTB_Status='S',AJTB_ApprovedBy=" & iUserID & ",AJTB_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " AJTB_Status='D',AJTB_ApprovedBy=" & iUserID & ",AJTB_ApprovedOn=GetDate()"
                End If
            End If
            sSql = sSql & " Where Ajtb_Masid=" & iMasId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveJournalEntryMaster(ByVal sAC As String, ByVal objJE As clsJournalEntry) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAJTB_TranscNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_BillType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillDate", OleDb.OleDbType.Date, 4)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_AdvanceAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_AdvanceNaration", OleDb.OleDbType.VarChar, 10000)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_AdvanceNaration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_BalanceAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_NetAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_PaymentNarration", OleDb.OleDbType.VarChar, 10000)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_PaymentNarration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_ChequeNo", OleDb.OleDbType.VarChar, 10000)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_ChequeDate", OleDb.OleDbType.Date, 4)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_IFSCCode", OleDb.OleDbType.VarChar, 10000)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BankName", OleDb.OleDbType.VarChar, 10000)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BranchName", OleDb.OleDbType.VarChar, 10000)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Status", OleDb.OleDbType.VarChar, 40)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillCreatedDate", OleDb.OleDbType.Date, 4)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_BillCreatedDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@acc_JE_BranchId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iacc_JE_BranchId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Comnments", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdatePaymentMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iPaymentType As Integer, ByVal objJE As clsJournalEntry)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from acc_JE_Master where Acc_JE_ID =" & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update acc_JE_Master set Acc_JE_Party = " & objJE.iAcc_JE_Party & ",Acc_JE_Location=" & objJE.iAcc_JE_Location & ","
                sSql = sSql & "Acc_JE_BillType = " & objJE.iAcc_JE_BillType & ",Acc_JE_BillNo = '" & objGen.SafeSQL(objJE.sAcc_JE_BillNo) & "',"
                sSql = sSql & "Acc_JE_BillDate = " & objGen.FormatDtForRDBMS(objJE.dAcc_JE_BillDate, "I") & ",Acc_JE_BillAmount = " & objJE.dAcc_JE_BillAmount & " "

                If iPaymentType = 1 Then
                    sSql = sSql & ",Acc_JE_AdvanceAmount = " & objJE.dAcc_JE_AdvanceAmount & ",Acc_JE_AdvanceNaration = '" & objGen.SafeSQL(objJE.sAcc_JE_AdvanceNaration) & "',Acc_JE_BalanceAmount = " & objJE.dAcc_JE_BalanceAmount & " "
                ElseIf iPaymentType = 3 Then
                    sSql = sSql & ",Acc_JE_NetAmount = " & objJE.dAcc_JE_NetAmount & ",Acc_JE_PaymentNarration = '" & objJE.sAcc_JE_PaymentNarration & "' "
                ElseIf iPaymentType = 4 Then
                    sSql = sSql & ",Acc_JE_ChequeNo = " & objJE.sAcc_JE_ChequeNo & ","
                    sSql = sSql & "Acc_JE_ChequeDate = " & objGen.FormatDtForRDBMS(objJE.Acc_JE_ChequeDate, "I") & ",Acc_JE_IFSCCode = '" & objJE.sAcc_JE_IFSCCode & "',"
                    sSql = sSql & "Acc_JE_BankName = '" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "',Acc_JE_BranchName = '" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "' "
                End If
                sSql = sSql & "Where Acc_JE_ID = " & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
                Return objJE.iAcc_JE_ID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveJEHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iJEPKID As Integer, ByVal sComments As String, ByVal sStatus As String, ByVal sIPAddress As String)
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(6) {}
        Dim iJEHParamCount As Integer
        Dim Arr(1) As String
        Try
            iJEHParamCount = 0
            ObjSFParam(iJEHParamCount) = New OleDb.OleDbParameter("@AJEH_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iJEHParamCount).Value = 0
            ObjSFParam(iJEHParamCount).Direction = ParameterDirection.Input
            iJEHParamCount += 1

            ObjSFParam(iJEHParamCount) = New OleDb.OleDbParameter("@AJEH_AccJEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iJEHParamCount).Value = iJEPKID
            ObjSFParam(iJEHParamCount).Direction = ParameterDirection.Input
            iJEHParamCount += 1

            ObjSFParam(iJEHParamCount) = New OleDb.OleDbParameter("@AJEH_Comments", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iJEHParamCount).Value = sComments
            ObjSFParam(iJEHParamCount).Direction = ParameterDirection.Input
            iJEHParamCount += 1

            ObjSFParam(iJEHParamCount) = New OleDb.OleDbParameter("@AJEH_UserID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iJEHParamCount).Value = iUserID
            ObjSFParam(iJEHParamCount).Direction = ParameterDirection.Input
            iJEHParamCount += 1

            ObjSFParam(iJEHParamCount) = New OleDb.OleDbParameter("@AJEH_Status", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iJEHParamCount).Value = sStatus
            ObjSFParam(iJEHParamCount).Direction = ParameterDirection.Input
            iJEHParamCount += 1

            ObjSFParam(iJEHParamCount) = New OleDb.OleDbParameter("@AJEH_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iJEHParamCount).Value = sIPAddress
            ObjSFParam(iJEHParamCount).Direction = ParameterDirection.Input
            iJEHParamCount += 1

            ObjSFParam(iJEHParamCount) = New OleDb.OleDbParameter("@AJEH_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iJEHParamCount).Value = iACID
            ObjSFParam(iJEHParamCount).Direction = ParameterDirection.Input
            iJEHParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "SpAcc_JE_Master_History", ObjSFParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveTransactionDetails(ByVal sAC As String, ByVal objJE As clsJournalEntry) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAJTB_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_MasID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAJTB_MAsID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_TranscNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objJE.sAJTB_TranscNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 4)
            'ObjParam(iParamCount).Value = objJE.dATD_TransactionDate
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            'ObjParam(iParamCount).Value = objJE.iATD_TrType
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            'ObjParam(iParamCount).Value = objJE.iATD_BillId
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            'ObjParam(iParamCount).Value = objJE.iATD_PaymentType
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_ScheduleTypeid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_ScheduleTypeid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_Deschead", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_Deschead
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_Desc", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dAJTB_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objJE.dAJTB_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objJE.sAJTB_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objJE.sAJTB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_BillType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_DescName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objJE.sAJTB_DescName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AJTB_BranchId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAJTB_BranchId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_JETransactions_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAccHead As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sGL As String = ""
        Dim bCheck As Boolean
        Dim accHead As String = ""
        Try
            'sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_Head=" & iAccHead & " And CLM_CUSTID=" & iCustID & " And CLM_CompID=" & iACID & " "
            'bCheck = objDBL.DBCheckForRecord(sAC, sSql)
            'If bCheck = True Then
            '    sSql = "" : sSql = "Select CLM_GLLedger From CustomerGL_Linkage_Master Where CLM_Head=" & iAccHead & " And CLM_CUSTID=" & iCustID & " And CLM_CompID=" & iACID & " "
            '    sGL = objDBL.SQLGetDescription(sAC, sSql)

            '    If sGL.StartsWith(",") Then
            '        sGL = sGL.Remove(0, 1)
            '    End If
            '    If sGL.EndsWith(",") Then
            '        sGL = sGL.Remove(Len(sGL) - 1, 1)
            '    End If
            '    sSql = "" : sSql = "Select CC_GL,CC_GLDesc From Customer_COA Where CC_GL In (" & sGL & ") And CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " "
            '    dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            'End If

            'sSql = "" : sSql = "Select CC_GL,CC_GLDesc From Customer_COA Where CC_Head=2 And CC_AccHead=" & iAccHead & " And CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " " 'Commented by vijayalakshmi 28/11/19 because now the gls are going to save in Chart_of_accounts
            'dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)


            sSql = "" : sSql = "Select Gl_id as CC_GL,GL_Desc as CC_GLDesc From chart_of_accounts Where Gl_Head=2 And gl_ACCHead =" & iAccHead & " And Gl_CustID=" & iCustID & " And Gl_CompID=" & iACID & " order by gl_desc"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAccHead As Integer, ByVal iGL As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sGL As String = ""
        Dim bCheck As Boolean
        Try
            'sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_Head=" & iAccHead & " And CLM_CUSTID=" & iCustID & " And CLM_CompID=" & iACID & " "
            'bCheck = objDBL.DBCheckForRecord(sAC, sSql)
            'If bCheck = True Then
            '    sSql = "" : sSql = "Select CLM_GLLedger From CustomerGL_Linkage_Master Where CLM_Head=" & iAccHead & " And CLM_CUSTID=" & iCustID & " And CLM_CompID=" & iACID & " "
            '    sGL = objDBL.SQLGetDescription(sAC, sSql)

            '    If sGL.StartsWith(",") Then
            '        sGL = sGL.Remove(0, 1)
            '    End If
            '    If sGL.EndsWith(",") Then
            '        sGL = sGL.Remove(Len(sGL) - 1, 1)
            '    End If
            '    sSql = "" : sSql = "Select CC_GL,CC_GLDesc From Customer_COA Where CC_Parent In (" & sGL & ") And CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " "
            '    dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            'End If

            sSql = "" : sSql = "Select CC_GL,CC_GLDesc From Customer_COA Where CC_AccHead=" & iAccHead & " And CC_Head=3 And CC_Parent=" & iGL & " And CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccountHead(ByVal sAC As String, ByVal iACID As Integer, ByVal iGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccHead As Integer = 0
        Try
            sSql = "Select CC_AccHead from Customer_COA where CC_GL =" & iGL & " and CC_CompID =" & iACID & ""
            iAccHead = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iAccHead
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAParent(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iParent As Integer = 0
        Try
            sSql = "Select CC_Parent from Customer_COA where CC_GL =" & iSubGL & " and CC_CompID =" & iACID & ""
            iParent = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where cust_Compid=" & iACID & " and CUST_DelFlg = 'A' order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLedgerdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer, ByVal iSgl As Integer)
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select CC_AccHead,CC_Parent from Customer_COA Where CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " and CC_Custid=" & iCustId & " and CC_GL=" & iSgl & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateJeDet(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iID As Integer, ByVal iCustID As Integer, ByVal iTransId As Integer, ByVal dTransAmt As Double, ByVal iBranchId As Integer, ByVal dTransDbAmt As Double, ByVal dTransCrAmt As Double)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sTrAMt As String = ""
        Try
            sSql = "Select * From Acc_TrailBalance_Upload Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            If iBranchId <> 0 Then
                sSql = sSql & "and ATBU_Branchid=" & iBranchId & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt.Rows.Count > 0 Then
                If iTransId = 0 Then 'Debit
                    If dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") <> 0 Then
                        dTransDbAmt = dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") + dTransDbAmt
                        If dTransDbAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    ElseIf dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") <> 0 Then
                        dTransDbAmt = dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") - dTransDbAmt
                        If dTransDbAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sTrAMt = dTransDbAmt
                            sTrAMt = sTrAMt.Remove(0, 1)
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & sTrAMt & " ,ATBU_Closing_TotalCredit_Amount=0.00  Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                            sTrAMt = 0
                        End If
                    Else
                        dTransDbAmt = dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") + dTransDbAmt
                        If dTransDbAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    End If
                ElseIf iTransId = 1 Then 'Credit
                    If dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") <> 0 Then
                        dTransCrAmt = dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") + dTransCrAmt
                        If dTransCrAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    ElseIf dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") <> 0 Then
                        dTransCrAmt = dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") - dTransCrAmt
                        If dTransCrAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sTrAMt = dTransCrAmt
                            sTrAMt = sTrAMt.Remove(0, 1)
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & sTrAMt & ",ATBU_Closing_TotalDebit_Amount=0.00  Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    Else
                        dTransCrAmt = dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") + dTransCrAmt
                        If dTransCrAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    End If
                End If
            End If
            'If dt.Rows.Count > 0 Then
            '    If iTransId = 0 Then 'Debit
            '        If dt.Rows(0)("ATBU_Closing_Debit_Amount") = 0 Then
            '            If dt.Rows(0)("ATBU_Closing_Credit_Amount") <> 0 Then
            '                dTransAmt = dt.Rows(0)("ATBU_Closing_Credit_Amount") - dTransAmt
            '                If dTransAmt > 0 Then
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                Else
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                End If
            '            ElseIf dt.Rows(0)("ATBU_Closing_Credit_Amount") = 0 Then
            '                dTransAmt = dt.Rows(0)("ATBU_Closing_Debit_Amount") + dTransAmt
            '                If dTransAmt > 0 Then
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                Else
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                End If
            '            End If
            '        ElseIf dt.Rows(0)("ATBU_Closing_Debit_Amount") <> 0 Then
            '            dTransAmt = dt.Rows(0)("ATBU_Closing_Debit_Amount") + dTransAmt
            '            If dTransAmt > 0 Then
            '                sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '            Else
            '                sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '            End If
            '        End If
            '    ElseIf iTransId = 1 Then 'Credit
            '        If dt.Rows(0)("ATBU_Closing_Credit_Amount") = 0 Then
            '            If dt.Rows(0)("ATBU_Closing_Debit_Amount") <> 0 Then
            '                dTransAmt = dt.Rows(0)("ATBU_Closing_Debit_Amount") - dTransAmt
            '                If dTransAmt > 0 Then
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                Else
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                End If
            '            ElseIf dt.Rows(0)("ATBU_Closing_Debit_Amount") = 0 Then
            '                dTransAmt = dt.Rows(0)("ATBU_Closing_Debit_Amount") + dTransAmt
            '                If dTransAmt > 0 Then
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                Else
            '                    sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '                End If
            '            End If
            '        ElseIf dt.Rows(0)("ATBU_Closing_Credit_Amount") <> 0 Then
            '            dTransAmt = dt.Rows(0)("ATBU_Closing_Credit_Amount") + dTransAmt
            '            If dTransAmt > 0 Then
            '                sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '            Else
            '                sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            '                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            '            End If
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateJeDetDeactivate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iID As Integer, ByVal iCustID As Integer, ByVal iTransId As Integer, ByVal dTransAmt As Double, ByVal iBranchId As Integer, ByVal dTransDbAmt As Double, ByVal dTransCrAmt As Double)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sTrAMt As String = ""
        Try
            sSql = "Select * From Acc_TrailBalance_Upload Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
            If iBranchId <> 0 Then
                sSql = sSql & "and ATBU_Branchid=" & iBranchId & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt.Rows.Count > 0 Then
                If iTransId = 0 Then 'Debit
                    If dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") <> 0 Then
                        dTransDbAmt = dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") + dTransDbAmt
                        If dTransDbAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sTrAMt = dTransDbAmt
                            sTrAMt = sTrAMt.Remove(0, 1)
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & sTrAMt & ",ATBU_Closing_Totaldebit_Amount=0.00  Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    ElseIf dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") <> 0 Then
                        dTransDbAmt = dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") - dTransDbAmt
                        If dTransDbAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sTrAMt = dTransDbAmt
                            sTrAMt = sTrAMt.Remove(0, 1)
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & sTrAMt & " ,ATBU_Closing_TotalCredit_Amount=0.00  Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                            sTrAMt = 0
                        End If
                    Else
                        dTransDbAmt = dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") + dTransDbAmt
                        If dTransDbAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransDbAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    End If
                ElseIf iTransId = 1 Then 'Credit
                    If dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") <> 0 Then
                        dTransCrAmt = dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") + dTransCrAmt
                        If dTransCrAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sTrAMt = dTransCrAmt
                            sTrAMt = sTrAMt.Remove(0, 1)
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & sTrAMt & ",ATBU_Closing_TotalCredit_Amount=0.00 Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    ElseIf dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") <> 0 Then
                        dTransCrAmt = dt.Rows(0)("ATBU_Closing_TotalDebit_Amount") - dTransCrAmt
                        If dTransCrAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sTrAMt = dTransCrAmt
                            sTrAMt = sTrAMt.Remove(0, 1)
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & sTrAMt & ",ATBU_Closing_TotalDebit_Amount=0.00  Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    Else
                        dTransCrAmt = dt.Rows(0)("ATBU_Closing_TotalCredit_Amount") + dTransCrAmt
                        If dTransCrAmt >= 0 Then
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalCredit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        Else
                            sSql = "Update Acc_TrailBalance_Upload Set ATBU_Closing_TotalDebit_Amount=" & dTransCrAmt & " Where ATBU_ID=" & iID & " And ATBU_CustId =" & iCustID & " and ATBU_CompID=" & iCompID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id as Branchid,Mas_Description as BranchName from SAD_CUST_LOCATION where Mas_CustID=" & iCustId & " and Mas_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
