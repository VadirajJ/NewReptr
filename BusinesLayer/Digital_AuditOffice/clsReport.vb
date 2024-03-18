Public Class clsReport
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral
    Public Function GetCustomers(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = "", sValue As String = ""
        Try
            sSql = "Select Cust_Name from SAD_CUSTOMER_MASTER Where Cust_ID=" & iCustID & " And cust_Compid=" & iACID & " order by Cust_Name"
            sValue = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sValue
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iYear As Integer) As String
        Dim sSql As String = "", sYear As String = ""
        Dim dt As New DataTable
        Dim aYearID As Array
        Try
            sSql = "Select YMS_ID from Year_Master where YMS_YEARID=" & iYear + 1 & " and YMS_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("YMS_ID").Contains("-") = True Then
                    aYearID = dt.Rows(0)("YMS_ID").Split("-")
                    For i = 0 To aYearID.Length - 1
                        If aYearID(i) <> "" Then
                            sYear = aYearID(1)
                        End If
                    Next
                End If
            End If
            Return sYear
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Balance Sheet
    Public Function LoadBalanceSheet(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer)
        Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim dtGroup As New DataTable
        Dim dtSub As New DataTable
        Dim dtLink As New DataTable
        Dim dtArray As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0
        Dim a As Integer = 0
        Dim sArray As Array
        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00

        Dim iHead As Integer = 0
        Dim iSLNo As Integer = 0
        Dim iLastYear As Integer = 0

        Dim iFixedAssets As Integer = 0

        Dim iStatusCheck As Integer = 0

        Dim sSPresentYear As String = "" : Dim sLastYear As String = ""
        Dim sAsseCurrentYear As String = "" : Dim sAssetLastYear As String = ""
        Dim sSSValues As String = ""
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            'Liabilites

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iCOmpID & ")"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " and gl_OrgTypeID=" & iOrgID & " And gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EQUITY And LIABILITIES" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_CompID =" & iCOmpID & " And "
                    aSql = aSql & "gl_Delflag ='C' and gl_Status ='A'  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & " and SLM_custid=" & iCustID & "" ' Added SLM_custid=" & iCustID & "" vijayalakshmi 29-07-19
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If dtSub.Rows(j)("gl_ID") = 11 Then
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule Where SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " And SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =11 and SS_Status='D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count = 0 Then
                                                    iSql = "" : iSql = "Select * from Acc_Seperate_Schedule Where SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " And SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =60 and SS_Status='D' "
                                                    dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If
                                                        dDebit = 0
                                                        sSSValues = Convert.ToString(dCredit)
                                                    Next
                                                Else
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                                'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                            End If
                                                            dDebit = 0
                                                            sSSValues = Convert.ToString(dCredit)
                                                        Next

                                                    End If

                                                End If
                                                GoTo P
                                            Else
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                'Customer COA'

                                                iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If

                                            'Customer COA'

                                        End If
                                    Next

P:                                  sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    'If sSPresentYear.StartsWith("-") = True Then
                                    'sSPresentYear = sSPresentYear.Remove(0, 1)
                                    ' End If
                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    dRow("PresentYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr
                                    'If sSSValues.StartsWith("-") = True Then
                                    '    dTotalDebit = dTotalDebit - Convert.ToDouble(sSPresentYear)
                                    '    sSSValues = ""
                                    'Else
                                    dTotalDebit = dTotalDebit + Convert.ToDouble(sSPresentYear)
                                    'End If

                                    dDebit = 0 : dCredit = 0
                                    End If


                                    'Last Year
                                    sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If dtSub.Rows(j)("gl_ID") = 11 Then
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule Where SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " And SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =11 and SS_Status='D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If
                                                        dDebit = 0
                                                        sSSValues = Convert.ToString(dCredit)
                                                    Next
                                                End If
                                                GoTo l
                                            Else
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                'Customer COA'
                                                iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) ' + dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    Next

l:                                  sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    ' If sSPresentYear.StartsWith("-") = True Then
                                    'sSPresentYear = sSPresentYear.Remove(0, 1)
                                    ' End If
                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    dRow("LastYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr

                                    'If sSSValues.StartsWith("-") = True Then
                                    '    dTotalLDebit = dTotalLDebit - Convert.ToDouble(sSPresentYear)
                                    '    sSSValues = ""
                                    'Else
                                    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sSPresentYear)
                                    'End If

                                    dDebit = 0 : dCredit = 0
                                End If


                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '            iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & ""

                                '            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '            If dtArray.Rows.Count > 0 Then
                                '                For a = 0 To dtArray.Rows.Count - 1
                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                    End If

                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                    End If
                                '                Next

                                '            End If
                                '        End If
                                '    Next

                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sLastYear.StartsWith("-") = True Then
                                '        sLastYear = sLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sLastYear

                                '    Dim dLDbCr As Double = 0.00
                                '    dLDbCr = dLDebit - dLCredit
                                '    'dTotalLDebit = dTotalLDebit + dLDbCr
                                '    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sLastYear)

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If


            'Assets
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & "  and gl_OrgTypeID=" & iOrgID & " And gl_head in(0) and gl_AccHead = 1 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "ASSETS" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & "  and gl_OrgTypeID=" & iOrgID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_Delflag ='C' and "
                    aSql = aSql & "gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            'If (dtSub.Rows(j)("gl_Desc").ToString() = "Tangible Assets") Or (dtSub.Rows(j)("gl_Desc").ToString() = "Intangible Assets") Then
                            '    iFixedAssets = 1
                            'Else
                            '    iFixedAssets = 0
                            'End If

                            'Vijayalakshmi 16/12/2019 values r not fetching for tangible and intangible

                            'If (dtSub.Rows(j)("gl_Desc").ToString() = "Fixed Assets- Tangible Assets") Or (dtSub.Rows(j)("gl_Desc").ToString() = "Fixed Assets- Intangible Assets") Then
                            '    iFixedAssets = 1
                            'Else
                            '    iFixedAssets = 0
                            'End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            'If iFixedAssets = 0 Then 'Vijayalakshmi 16/12/2019 values r not fetching for tangible and intangible

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next

                                                End If

                                            'Customer COA'
                                            If dtLink.Rows(0)("SLM_NoteNo") = 21 Then
                                                iSql = "" : iSql = "Select * from Acc_Changes_Inventories Where CI_Status<>'D' and CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " And CI_FinancialYear =" & iYearID & " and CI_CompID =" & iCOmpID & " and CI_Glid =" & sArray(k) & " "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            ' dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit")) ' Commented by Vijayalakshmi 26/11/2019

                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If
                                                        dCredit = 0
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            ' dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit")) ' Commented by Vijayalakshmi 26/11/2019

                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If

                                            'Customer COA'

                                            ' End If
                                        End If
                                    Next

                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sAsseCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    '  If sAsseCurrentYear.StartsWith("-") = True Then
                                    ' sAsseCurrentYear = sAsseCurrentYear.Remove(0, 1)
                                    '  End If
                                    dRow("PresentYear") = sAsseCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr

                                    dTotalDebit = dTotalDebit + Convert.ToDouble(sAsseCurrentYear)

                                        dDebit = 0 : dCredit = 0
                                    End If

                                    'Last Year
                                    sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            'If iFixedAssets = 0 Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next

                                                End If

                                                'Customer COA'
                                                iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                        'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                        '  dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit")) ' Commented by Vijayalakshmi 26/11/2019
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) ' + dtArray.Rows(a)("CC_TrDebit"))
                                                    End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                        'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                    End If
                                                    Next
                                                ' End If
                                                'Customer COA'

                                            End If
                                        End If
                                    Next

                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sAsseCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    ' If sAsseCurrentYear.StartsWith("-") = True Then
                                    'sAsseCurrentYear = sAsseCurrentYear.Remove(0, 1)
                                    '  End If
                                    dRow("LastYear") = sAsseCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr
                                    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sAsseCurrentYear)

                                    dDebit = 0 : dCredit = 0
                                End If

                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            If iFixedAssets = 0 Then
                                '                iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '                iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & ""

                                '                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '                If dtArray.Rows.Count > 0 Then
                                '                    For a = 0 To dtArray.Rows.Count - 1
                                '                        If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                        End If

                                '                        If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                        End If
                                '                    Next

                                '                End If
                                '            End If
                                '        End If
                                '    Next
                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sAssetLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sAssetLastYear.StartsWith("-") = True Then
                                '        sAssetLastYear = sAssetLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sAssetLastYear

                                '    Dim dLDbCr As Double = 0.00
                                '    dLDbCr = dLDebit - dLCredit
                                '    'dTotalLDebit = dTotalLDebit + dLDbCr
                                '    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sAssetLastYear)

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                Dim sStrTotalCurrentdebit As String = "" : Dim sStrTotalLastDebit As String = ""
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                sStrTotalCurrentdebit = dTotalDebit
                If sStrTotalCurrentdebit.StartsWith("-") = True Then
                    sStrTotalCurrentdebit = sStrTotalCurrentdebit.Remove(0, 1)
                End If
                'dRow("PresentYear") = dTotalDebit
                dRow("PresentYear") = sStrTotalCurrentdebit

                sStrTotalLastDebit = dTotalLDebit
                If sStrTotalLastDebit.StartsWith("-") = True Then
                    sStrTotalLastDebit.Remove(0, 1)
                End If
                'dRow("LastYear") = dTotalLDebit
                dRow("LastYear") = sStrTotalLastDebit
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadBalanceSheet(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer)
    '    Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = "", gsql As String = ""
    '    Dim dRow As DataRow
    '    Dim dt As New DataTable, dtGroup As New DataTable, dtSub As New DataTable, dtLink As New DataTable, dtArray As New DataTable, dtSub1 As New DataTable
    '    Dim dtYear As New DataTable, dtOB As New DataTable
    '    Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0
    '    Dim sArray As Array
    '    Dim dDebit As Double = 0.00, dCredit As Double = 0.00
    '    Dim dLDebit As Double = 0.00, dLCredit As Double = 0.00
    '    Dim dTotalDebit As Double = 0.00, dTotalCredit As Double = 0.00
    '    Dim dTotalLDebit As Double = 0.00, dTotalLCredit As Double = 0.00
    '    Dim iHead As Integer = 0, iSLNo As Integer = 0, iLastYear As Integer = 0, iFixedAssets As Integer = 0, iStatusCheck As Integer = 0
    '    Dim dpD As Integer = 0
    '    Dim dLD As Double = 0.00, dLC As Double = 0.00
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("SLNo")
    '        dt.Columns.Add("Particulars")
    '        dt.Columns.Add("NoteNo")
    '        dt.Columns.Add("PresentYear")
    '        dt.Columns.Add("LastYear")

    '        'Liabilites
    '        sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_YEARID=" & iYearID & " and YMS_CompID=" & iACID & ""
    '        dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtYear.Rows.Count > 0 Then
    '            For i = 0 To dtYear.Rows.Count - 1
    '                iLastYear = dtYear.Rows(i).Item("YMS_YEARID")
    '            Next
    '        Else
    '            iLastYear = 0
    '        End If


    '        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
    '        dtOB = objDBL.SQLExecuteDataTable(sAC, iSql)
    '        If dtOB.Rows.Count > 0 Then
    '            For j = 0 To dtOB.Rows.Count - 1
    '                If dtOB.Rows(j).Item("Opn_Status") = "F" Then
    '                    iStatusCheck = 0
    '                Else
    '                    iStatusCheck = 1
    '                End If
    '            Next
    '        Else
    '            iStatusCheck = 1
    '        End If

    '        sSql = "" : sSql = "Select * From Customer_COA Where CC_Parent In (Select gl_id from chart_of_Accounts where gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' And Gl_Desc <> '' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 )"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then
    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "EQUITY AND LIABILITIES" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("CC_Parent").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("CC_Parent")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("CC_GLDesc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("CC_GLDesc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * From Customer_COA Where CC_Head in (1) And CC_Parent in (Select gl_ID from chart_of_Accounts where gl_Head in (1) And gl_Parent = " & dtGroup.Rows(i)("CC_Parent") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' And gl_Desc <> '' and gl_Status ='A' And gl_OrgTypeID=0 )"
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("CC_Parent").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("CC_Parent")
    '                        End If

    '                        gsql = "select * from Customer_COA where CC_Head= '2' and CC_Parent= " & dtSub.Rows(j)("CC_Parent") & " and cc_custid=" & dtSub.Rows(j)("cc_custid") & " and CC_GLDesc='" & dtSub.Rows(j)("CC_GLDesc") & "' and CC_IndType=" & dtSub.Rows(j)("CC_IndType") & " and CC_CompID=" & dtSub.Rows(j)("CC_CompID") & ""
    '                        dtSub1 = objDBL.SQLExecuteDataTable(sAC, gsql)
    '                        dpD = dtSub1.Rows(0)("CC_GL")

    '                        If IsDBNull(dtSub.Rows(j)("CC_GLDesc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("CC_GLDesc")
    '                        End If

    '                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("CC_Parent") & " and "
    '                        mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("CC_Parent") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & ""
    '                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                        If dtLink.Rows.Count > 0 Then
    '                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                            End If
    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then

    '                                        If iStatusCheck = 0 Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next

    '                                            End If
    '                                        Else
    '                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                            iSql = iSql & "ATD_GL =" & sArray(k) & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)

    '                                            If dpD = sArray(k) Then
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    For a = 0 To dtArray.Rows.Count - 1

    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                        End If

    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                        End If
    '                                                    Next
    '                                                End If
    '                                            End If
    '                                        End If
    '                                        End If
    '                                Next
    '                                dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
    '                                Dim dDbCr As Double = 0.00
    '                                dDbCr = dDebit - dCredit
    '                                dTotalDebit = dTotalDebit + dDbCr

    '                                dDebit = 0 : dCredit = 0


    '                            End If

    '                            'Last Year

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                        If dtArray.Rows.Count > 0 Then
    '                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                End If

    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                End If

    '                                            Next

    '                                        End If
    '                                    End If
    '                                Next

    '                                dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

    '                                Dim dLDbCr As Double = 0.00
    '                                dLDbCr = dLDebit - dLCredit
    '                                dTotalLDebit = dTotalLDebit + dLDbCr

    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
    '            dRow("PresentYear") = dTotalDebit
    '            dRow("LastYear") = dTotalLDebit
    '            dt.Rows.Add(dRow)
    '        End If


    '        'Assets
    '        dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '        sSql = "" : sSql = "SElect * From Customer_COA Where CC_Parent In (Select GL_ID from chart_of_Accounts where gl_head in(0) and gl_AccHead = 1 And Gl_Desc<>'' and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID = 0 )"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "ASSETS" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("CC_Parent").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("CC_Parent")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("CC_GLDesc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("CC_GLDesc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * From Customer_COA Where CC_Head in(1) And CC_Parent in (Select GL_ID from chart_of_Accounts where gl_Head in(1) And gl_Parent = " & dtGroup.Rows(i)("CC_Parent") & " And gl_Desc<>'' And gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0) "
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("CC_Parent").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("CC_Parent")
    '                        End If

    '                        If IsDBNull(dtSub.Rows(j)("CC_GLDesc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("CC_GLDesc")
    '                        End If

    '                        gsql = "select * from Customer_COA where CC_Head= '2' and CC_Parent= " & dtSub.Rows(j)("CC_Parent") & " and cc_custid=" & dtSub.Rows(j)("cc_custid") & " and CC_GLDesc='" & dtSub.Rows(j)("CC_GLDesc") & "' and CC_IndType=" & dtSub.Rows(j)("CC_IndType") & " and CC_CompID=" & dtSub.Rows(j)("CC_CompID") & ""
    '                        dtSub1 = objDBL.SQLExecuteDataTable(sAC, gsql)
    '                        dpD = dtSub1.Rows(0)("CC_GL")

    '                        If (dtSub.Rows(j)("CC_GLDesc").ToString() = "Tangible Assets") Or (dtSub.Rows(j)("CC_GLDesc").ToString() = "Intangible Assets") Then
    '                            iFixedAssets = 1
    '                        Else
    '                            iFixedAssets = 0
    '                        End If

    '                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("CC_Parent") & " and "
    '                        mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("CC_Parent") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & ""
    '                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                        If dtLink.Rows.Count > 0 Then
    '                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                            End If

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        If iFixedAssets = 0 Then

    '                                            If iStatusCheck = 0 Then
    '                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                                iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    For a = 0 To dtArray.Rows.Count - 1
    '                                                        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                        End If

    '                                                        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                        End If
    '                                                    Next

    '                                                End If
    '                                            Else
    '                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                                iSql = iSql & "ATD_GL =" & sArray(k) & ""
    '                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                If dpD = sArray(k) Then
    '                                                    If dtArray.Rows.Count > 0 Then
    '                                                        For a = 0 To dtArray.Rows.Count - 1
    '                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                            End If

    '                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                            End If
    '                                                        Next

    '                                                    End If
    '                                                End If
    '                                            End If
    '                                            End If
    '                                    End If
    '                                Next

    '                                dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))

    '                                Dim dDbCr As Double = 0.00
    '                                dDbCr = dDebit - dCredit
    '                                dTotalDebit = dTotalDebit + dDbCr

    '                                dDebit = 0 : dCredit = 0
    '                            End If

    '                            'Last Year
    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        If iFixedAssets = 0 Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next

    '                                            End If
    '                                        End If
    '                                    End If
    '                                Next
    '                                dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

    '                                Dim dLDbCr As Double = 0.00
    '                                dLDbCr = dLDebit - dLCredit
    '                                dTotalLDebit = dTotalLDebit + dLDbCr

    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
    '            dRow("PresentYear") = dTotalDebit
    '            dRow("LastYear") = dTotalLDebit
    '            dt.Rows.Add(dRow)
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'PL Report
    Public Function LoadPLReports(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iUserid As Integer, ByVal sIpAddress As String)
        Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim dtGroup As New DataTable
        Dim dtSub As New DataTable
        Dim dtLink As New DataTable
        Dim dtArray As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0
        Dim sArray As Array
        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00

        Dim iHead As Integer = 0
        Dim iSLNo As Integer = 0
        Dim iLastYear As Integer = 0
        Dim iStatusCheck As Integer = 0

        Dim sSPresentYear As String = "" : Dim sLastYear As String = ""
        Dim sExCurrentYear As String = "" : Dim sExLastYear As String = ""
        Dim sTotalDebit As String = "" : Dim sTotalCredit As String = ""
        Dim sExTotalDebit As String = "" : Dim sExTotalCredit As String = ""

        Dim dPCIValue As Double = 0.00 : Dim dLCIValue As Double = 0.00
        Dim dPIncomeSum As Double = 0.00 : Dim dLIncomeSum As Double = 0.00
        Dim dPExpensSum As Double = 0.00 : Dim dLExpensSum As Double = 0.00
        Dim dPExceptionalSum As Double = 0.00 : Dim dLExceptionalSum As Double = 0.00
        Dim dPExp As Double = 0.00 : Dim dLExp As Double = 0.00
        Dim dPExtraSum As Double = 0.00 : Dim dLExtraSum As Double = 0.00
        Dim dPExt As Double = 0.00 : Dim dLExt As Double = 0.00
        Dim dPExpSum As Double = 0.00 : Dim dLExpSum As Double = 0.00
        Dim dPTaxExpenses As Double = 0.00 : Dim dLTaxExpenses As Double = 0.00
        Dim dPTaxExpSum As Double = 0.00 : Dim dLTaxExpSum As Double = 0.00
        Dim dPDisConOperations As Double = 0.00 : Dim dLDisConOperations As Double = 0.00
        Dim dPContOperations As Double = 0.00 : Dim dLContOperations As Double = 0.00
        Dim dPTotalOperations As Double = 0.00 : Dim dLTotalOperations As Double = 0.00

        Dim iCount As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")


            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iCOmpID & ")"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If

            'Income
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_head in(0) and gl_AccHead = 2 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & "  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "REVENUE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_CompID =" & iCOmpID & " And "
                    aSql = aSql & "gl_Delflag ='C' and gl_Status ='A'  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =2 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID=" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If

                                            'Customer COA'
                                            iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                        ' dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                        'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    If sSPresentYear.StartsWith("-") = True Then
                                        sSPresentYear = sSPresentYear.Remove(0, 1)
                                    End If
                                    dRow("PresentYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dCredit - dDebit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID=" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'
                                            iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                        'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                        ' dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    If sSPresentYear.StartsWith("-") = True Then
                                        sSPresentYear = sSPresentYear.Remove(0, 1)
                                    End If
                                    dRow("LastYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dCredit - dDebit
                                    dTotalLDebit = dTotalLDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '            iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & " "
                                '            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '            If dtArray.Rows.Count > 0 Then
                                '                For a = 0 To dtArray.Rows.Count - 1
                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                    End If

                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                    End If
                                '                Next

                                '            End If
                                '        End If
                                '    Next

                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sLastYear.StartsWith("-") = True Then
                                '        sLastYear = sLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sLastYear

                                '    Dim dLdbCr As Double = 0.00
                                '    dLdbCr = dLDebit - dLCredit
                                '    dTotalLDebit = dTotalLDebit + dLdbCr

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))

                sTotalDebit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                ' If sTotalDebit.StartsWith("-") = True Then
                ' sTotalDebit = sTotalDebit.Remove(0, 1)
                ' End If
                dRow("PresentYear") = sTotalDebit
                dPIncomeSum = Convert.ToDouble(sTotalDebit)

                sTotalCredit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                'If sTotalCredit.StartsWith("-") = True Then
                ' sTotalCredit = sTotalCredit.Remove(0, 1)
                ' End If
                dRow("LastYear") = sTotalCredit
                dLIncomeSum = Convert.ToDouble(sTotalCredit)

                dt.Rows.Add(dRow)
            End If


            'Expenditure
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_head in(0) and gl_AccHead = 3 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & "  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EXPENDITURE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_CompID =" & iCOmpID & " And "
                    aSql = aSql & "gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =3 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & "and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'
                                            If sArray(k) = 333 Then
                                                'iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " And Opn_CompID =" & iCOmpID & " And "
                                                'iSql = iSql & "Opn_GLID =198  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                'dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                'If dtArray.Rows.Count > 0 Then
                                                '    For a = 0 To dtArray.Rows.Count - 1
                                                '        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                '            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                '            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                '        End If

                                                '        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                '            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                '            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                '        End If
                                                '    Next
                                                'End If
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_oBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_oBValues")) '+ dtArray.Rows(a)("CI_CBValues"))
                                                        End If
                                                    Next
                                                End If
                                            ElseIf sArray(k) = 334 Then
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues")) '+ dtArray.Rows(a)("CI_CBValues"))
                                                        End If
                                                    Next
                                                End If
                                            Else

                                                iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If

                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sExCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    'If sExCurrentYear.StartsWith("-") = True Then
                                    '    sExCurrentYear = sExCurrentYear.Remove(0, 1)
                                    'End If
                                    dRow("PresentYear") = sExCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'
                                            If sArray(k) = 333 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "Opn_GLID =198  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            ElseIf sArray(k) = 334 Then
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues")) '+ dtArray.Rows(a)("CI_CBValues"))
                                                        End If
                                                    Next
                                                End If
                                            Else

                                                iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sExCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    'If sExCurrentYear.StartsWith("-") = True Then
                                    '    sExCurrentYear = sExCurrentYear.Remove(0, 1)
                                    'End If
                                    dRow("LastYear") = sExCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalLDebit = dTotalLDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If


                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '            iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & ""
                                '            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '            If dtArray.Rows.Count > 0 Then
                                '                For a = 0 To dtArray.Rows.Count - 1
                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                    End If

                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                    End If
                                '                Next

                                '            End If
                                '        End If
                                '    Next

                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sExLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sExLastYear.StartsWith("-") = True Then
                                '        sExLastYear = sExLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sExLastYear

                                '    Dim dLdbCr As Double = 0.00
                                '    dLdbCr = dLDebit - dLCredit
                                '    dTotalLDebit = dTotalLDebit + dLdbCr

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                            If dtLink.Rows.Count > 0 Then
                                If dtLink.Rows(0)("SLM_NoteNo") = 36 Then
                                    dRow = dt.NewRow()
                                    dRow("Particulars") = "Changes in inventories of finished goods, work-in-progress and stock-in-trade"
                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                                    dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                            End If

                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                            End If
                                        Next
                                    End If

                                    dRow("PresentYear") = dDebit - dCredit
                                    dPCIValue = dDebit - dCredit

                                    dDebit = 0 : dCredit = 0

                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                                    dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                            End If

                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                            End If
                                        Next
                                    End If
                                    dRow("LastYear") = dDebit - dCredit
                                    dLCIValue = dDebit - dCredit

                                    dDebit = 0 : dCredit = 0
                                    dt.Rows.Add(dRow)
                                End If
                            Else

                            End If

                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next
                'dRow = dt.NewRow()
                'dRow("Particulars") = "Changes in inventories of finished goods, work-in-progress and stock-in-trade"
                'iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                'dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                'If dtArray.Rows.Count > 0 Then
                '    For a = 0 To dtArray.Rows.Count - 1
                '        If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                '            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                '        End If

                '        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                '            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                '        End If
                '    Next
                'End If

                'dRow("PresentYear") = dDebit - dCredit
                'dPCIValue = dDebit - dCredit

                'dDebit = 0 : dCredit = 0

                'iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                'dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                'If dtArray.Rows.Count > 0 Then
                '    For a = 0 To dtArray.Rows.Count - 1
                '        If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                '            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                '        End If

                '        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                '            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                '        End If
                '    Next
                'End If
                'dRow("LastYear") = dDebit - dCredit
                'dLCIValue = dDebit - dCredit

                'dDebit = 0 : dCredit = 0
                'dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                sExTotalDebit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                If sExTotalDebit.StartsWith("-") = True Then
                    sExTotalDebit = sExTotalDebit.Remove(0, 1)
                End If
                dRow("PresentYear") = sExTotalDebit + dPCIValue

                dPExpensSum = Convert.ToDouble(sExTotalDebit + dPCIValue)

                'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                sExTotalCredit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                If sExTotalCredit.StartsWith("-") = True Then
                    sExTotalCredit = sExTotalCredit.Remove(0, 1)
                End If
                dRow("LastYear") = sExTotalCredit + dLCIValue
                dLExpensSum = Convert.ToDouble(sExTotalCredit + dLCIValue)
                dt.Rows.Add(dRow)


                'Profit / (Loss) before exceptional and extraordinary items and tax
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit / (Loss) before exceptional and extraordinary items and tax" & "</B>"
                dRow("PresentYear") = dPIncomeSum - dPExpensSum
                Dim dPBeforeExp As Double = 0.00
                dPBeforeExp = dPIncomeSum - dPExpensSum
                'If dRow("PresentYear").StartsWith("-") = True Then
                '    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                'End If
                Dim dLBeforeExp As Double = 0.00
                dRow("LastYear") = dLIncomeSum - dLExpensSum
                dLBeforeExp = dLIncomeSum - dLExpensSum
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Exceptional items
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "Exceptional items"

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =1 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit
                dPExceptionalSum = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =1 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dLExceptionalSum = dDebit

                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit / (Loss) before extraordinary items and tax

                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit / (Loss) before extraordinary items and tax" & "</B>"


                dPExp = Math.Round(dPBeforeExp, 2)
                dRow("PresentYear") = dPExceptionalSum
                If dRow("PresentYear").StartsWith("-") = True Then
                    dPExceptionalSum = dRow("PresentYear").Remove(0, 1)
                    dRow("PresentYear") = dPExp + dPExceptionalSum
                    dPExpSum = dPExp + dPExceptionalSum
                Else
                    dRow("PresentYear") = dPExp - dPExceptionalSum
                    dPExpSum = dPExp - dPExceptionalSum
                End If

                dLExp = Math.Round(dLBeforeExp, 2)
                dRow("LastYear") = dLExceptionalSum
                If dRow("LastYear").StartsWith("-") = True Then
                    dLExceptionalSum = dRow("LastYear").Remove(0, 1)
                    dRow("LastYear") = dLExp + dLExceptionalSum
                    dLExpSum = dLExp + dLExceptionalSum
                Else
                    dRow("LastYear") = dLExp - dLExceptionalSum
                    dLExpSum = dLExp - dLExceptionalSum
                End If
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Extraordinary items
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "Extraordinary items"

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =2 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit
                dPExtraSum = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =2 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dLExtraSum = dDebit

                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit/(Loss) before tax
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) before tax" & "</B>"

                dPExt = Math.Round(dPExpSum, 2)
                dRow("PresentYear") = dPExtraSum
                If dRow("PresentYear").StartsWith("-") = True Then
                    dPExtraSum = dRow("PresentYear").Remove(0, 1)
                    dRow("PresentYear") = dPExt - dPExtraSum
                    dPTaxExpSum = dPExt - dPExtraSum
                Else
                    dRow("PresentYear") = dPExt - dPExtraSum
                    dPTaxExpSum = dPExt - dPExtraSum
                End If

                dLExt = Math.Round(dLExpSum, 2)
                dRow("LastYear") = dLExtraSum
                If dRow("LastYear").StartsWith("-") = True Then
                    dLExtraSum = dRow("LastYear").Remove(0, 1)
                    dRow("LastYear") = dLExt + dLExtraSum
                    dLTaxExpSum = dLExt + dLExtraSum
                Else
                    dRow("LastYear") = dLExt - dLExtraSum
                    dLTaxExpSum = dLExt - dLExtraSum
                End If
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Tax expense/(benefit)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Tax expense/(benefit)" & "</B>"
                dt.Rows.Add(dRow)

                'Current tax expense
                dRow = dt.NewRow()
                dRow("Particulars") = "Current tax expense"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                '(Less): MAT credit (where applicable)
                dRow = dt.NewRow()
                dRow("Particulars") = "(Less): MAT credit (where applicable)"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='(Less): MAT credit (where applicable)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='(Less): MAT credit (where applicable)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Short / (Excess) provision for tax relating to prior years
                dRow = dt.NewRow()
                dRow("Particulars") = "Short / (Excess) provision for tax relating to prior years"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Short/(Excess) provision for tax relating to prior years' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Short/(Excess) provision for tax relating to prior years' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Net current tax expense
                dRow = dt.NewRow()
                dRow("Particulars") = "Net current tax expense"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Net current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Net current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                ' Deferred tax
                dRow = dt.NewRow()
                dRow("Particulars") = "Deferred tax"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Deferred tax' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Deferred tax' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Net tax expense/(benefit)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Net tax expense/(benefit)" & "</B>"

                dRow("PresentYear") = dPTaxExpenses

                dRow("LastYear") = dLTaxExpenses
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit / (Loss) from continuing operations
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) from continuing operations" & "</B>"

                dRow("PresentYear") = dPTaxExpSum - dPTaxExpenses
                dPContOperations = dPTaxExpSum - dPTaxExpenses

                dRow("LastYear") = dLTaxExpSum - dLTaxExpenses
                dLContOperations = dLTaxExpSum - dLTaxExpenses

                'dRow("PresentYear") = dPTaxExpSum + dPTaxExpenses
                'dPContOperations = dPTaxExpSum + dPTaxExpenses

                'dRow("LastYear") = dLTaxExpSum + dLTaxExpenses
                'dLContOperations = dLTaxExpSum + dLTaxExpenses
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Discontinuing Operations
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Discontinuing Operations" & "</B>"
                dt.Rows.Add(dRow)

                'Profit/(Loss) from discontinuing operations (before tax)
                dRow = dt.NewRow()
                dRow("Particulars") = "Profit/(Loss) from discontinuing operations (before tax)"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Profit/(Loss) from discontinuing operations (before tax)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Profit/(Loss) from discontinuing operations (before tax)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit/(Loss) from discontinuing operations
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) from discontinuing operations" & "</B>"

                dRow("PresentYear") = dPDisConOperations

                dRow("LastYear") = dLDisConOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'TOTAL OPERATIONS
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Total Operations" & "</B>"
                dt.Rows.Add(dRow)

                'Profit/(Loss) for the year
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) for the year" & "</B>"

                dRow("PresentYear") = dPContOperations - dPDisConOperations
                Dim dPResAndSur As Double = 0.00
                dPResAndSur = dPContOperations - dPDisConOperations

                dRow("LastYear") = dLContOperations - dLDisConOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dt.Rows.Add(dRow)


                Try
                    iSql = "" : iSql = "select count(SS_PKID) from Acc_Seperate_Schedule where SS_Group=60 and SS_Particulars='Add: Current year profit/(loss)' and SS_Custid=" & iCustID & ""
                    iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
                    iCount = objDBL.SQLExecuteScalar(sNameSpace, iSql)
                    If iCount = 0 Then
                        Dim iMaxid As Integer = objDBL.SQLExecuteScalar(sNameSpace, "select IsNull(Max(SS_PKID) + 1,1) from Acc_Seperate_Schedule")
                        iSql = "" : iSql = "insert into Acc_Seperate_Schedule(SS_PKID,SS_FinancialYear,SS_CustId,SS_Orgtype,SS_Group,SS_Particulars,"
                        iSql = iSql & "SS_Values,SS_DATE,SS_Status,SS_Delflag,SS_CrBy,SS_CrOn,SS_UpdatedBy,SS_UpdatedOn,SS_IPAddress,SS_CompID)"
                        iSql = iSql & "values(" & iMaxid & "," & iYearID & "," & iCustID & "," & iOrgID & ",60,'Add: Current year profit/(loss)',"
                        iSql = iSql & "" & dPResAndSur & ",getdate(),'D','A'," & iUserid & ",getdate()," & iUserid & ",getdate(),'" & sIpAddress & "'," & iCOmpID & ")"
                        objDBL.SQLExecuteNonQuery(sNameSpace, iSql)
                    Else
                        iSql = "" : iSql = "update Acc_Seperate_Schedule set SS_Values=" & dPResAndSur & " where SS_Group=60 and SS_Particulars='Add: Current year profit/(loss)' and SS_Custid=" & iCustID & ""
                        iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, iSql)
                    End If
                Catch ex As Exception
                    Throw
                End Try

                'TOTAL OPERATIONS
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Total Operations" & "</B>"
                dt.Rows.Add(dRow)

                'Profit/(Loss) after tax before share of profit/(loss) of associates and minority interest
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) after tax before share of profit/(loss) of associates and minority interest" & "</B>"

                dRow("PresentYear") = dPContOperations - dPDisConOperations

                dRow("LastYear") = dLContOperations - dLDisConOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Share in profit/(loss) of associates @
                dRow = dt.NewRow()
                dRow("Particulars") = "Share in profit/(loss) of associates @"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Share in profit/(loss) of associates @' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTotalOperations = dPTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Share in profit/(loss) of associates @' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTotalOperations = dLTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Minority interest
                dRow = dt.NewRow()
                dRow("Particulars") = "Minority interest"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Minority interest' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTotalOperations = dPTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Minority interest' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTotalOperations = dLTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit/(Loss) for the year
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) for the year" & "</B>"

                dRow("PresentYear") = dPContOperations - dPDisConOperations + dPTotalOperations

                dRow("LastYear") = dLContOperations - dLDisConOperations + dLTotalOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Earnings per share (of ` ___/- each)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Earnings per share (of ` ___/- each)" & "</B>"
                dt.Rows.Add(dRow)

                'Basic Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Basic Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Total operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)


                'Earnings per share (of ` ___/- each) (excluding extraordinary items)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Earnings per share (of ` ___/- each) (excluding extraordinary items)" & "</B>"
                dt.Rows.Add(dRow)

                'Basic Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Basic Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Total operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadPLReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer)
    '    Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = "", gsql As String = ""
    '    Dim dRow As DataRow
    '    Dim dt As New DataTable, dtGroup As New DataTable, dtSub As New DataTable, dtLink As New DataTable, dtArray As New DataTable, dtsub1 As DataTable
    '    Dim dtYear As New DataTable, dtOB As New DataTable
    '    Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0
    '    Dim sArray As Array
    '    Dim dDebit As Double = 0.00, dCredit As Double = 0.00
    '    Dim dLDebit As Double = 0.00, dLCredit As Double = 0.00
    '    Dim dTotalDebit As Double = 0.00, dTotalCredit As Double = 0.00
    '    Dim dTotalLDebit As Double = 0.00, dTotalLCredit As Double = 0.00
    '    Dim iHead As Integer = 0, iSLNo As Integer = 0, iLastYear As Integer = 0, iStatusCheck As Integer = 0
    '    Dim dpD As Integer = 0
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("SLNo")
    '        dt.Columns.Add("Particulars")
    '        dt.Columns.Add("NoteNo")
    '        dt.Columns.Add("PresentYear")
    '        dt.Columns.Add("LastYear")

    '        sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_YEARID=" & iYearID & " and YMS_CompID=" & iACID & ""
    '        dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtYear.Rows.Count > 0 Then
    '            For i = 0 To dtYear.Rows.Count - 1
    '                iLastYear = dtYear.Rows(i).Item("YMS_YEARID")
    '            Next
    '        Else
    '            iLastYear = 0
    '        End If

    '        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
    '        dtOB = objDBL.SQLExecuteDataTable(sAC, iSql)
    '        If dtOB.Rows.Count > 0 Then
    '            For j = 0 To dtOB.Rows.Count - 1
    '                If dtOB.Rows(j).Item("Opn_Status") = "F" Then
    '                    iStatusCheck = 0
    '                Else
    '                    iStatusCheck = 1
    '                End If
    '            Next
    '        Else
    '            iStatusCheck = 1
    '        End If

    '        'Income
    '        dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '        sSql = "" : sSql = "Select * From Customer_COA Where CC_Parent in (Select gl_ID from chart_of_Accounts where gl_head in(0) and gl_AccHead = 2 and gl_Desc <> '' And gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0)"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "REVENUE" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("CC_parent").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("CC_parent")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("CC_GLDesc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("CC_GLDesc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * From Customer_COA Where CC_Head in (1) And CC_Parent in (Select gl_ID from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("CC_parent") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Desc <> '' and gl_Status ='A' And gl_OrgTypeID=0 )"
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("CC_Parent").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("CC_Parent")
    '                        End If

    '                        gsql = "select * from Customer_COA where CC_Head= '2' and CC_Parent= " & dtSub.Rows(j)("CC_Parent") & " and cc_custid=" & dtSub.Rows(j)("cc_custid") & " and CC_GLDesc='" & dtSub.Rows(j)("CC_GLDesc") & "' and CC_IndType=" & dtSub.Rows(j)("CC_IndType") & " and CC_CompID=" & dtSub.Rows(j)("CC_CompID") & ""
    '                        dtsub1 = objDBL.SQLExecuteDataTable(sAC, gsql)
    '                        dpD = dtSub1.Rows(0)("CC_GL")


    '                        If IsDBNull(dtSub.Rows(j)("CC_GLDesc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("CC_GLDesc")
    '                        End If

    '                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =2 and SLM_GroupID =" & dtGroup.Rows(i)("CC_Parent") & " and "
    '                        mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("CC_Parent") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & ""
    '                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                        If dtLink.Rows.Count > 0 Then
    '                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                            End If

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        If iStatusCheck = 0 Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        Else
    '                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                            iSql = iSql & "ATD_GL =" & sArray(k) & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dpD = sArray(k) Then
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    For a = 0 To dtArray.Rows.Count - 1
    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                        End If

    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                        End If
    '                                                    Next
    '                                                End If
    '                                            End If
    '                                        End If
    '                                        End If
    '                                Next

    '                                dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))

    '                                Dim dDbCr As Double = 0.00
    '                                dDbCr = dDebit - dCredit
    '                                dTotalDebit = dTotalDebit + dDbCr

    '                                dDebit = 0 : dCredit = 0
    '                            End If

    '                            'Last Year
    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                        If dtArray.Rows.Count > 0 Then
    '                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                End If

    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                End If
    '                                            Next

    '                                        End If
    '                                    End If
    '                                Next

    '                                dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

    '                                Dim dLdbCr As Double = 0.00
    '                                dLdbCr = dLDebit - dLCredit
    '                                dTotalLDebit = dTotalLDebit + dLdbCr

    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

    '            dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
    '            dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
    '            dt.Rows.Add(dRow)
    '        End If


    '        'Expenditure
    '        dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '        sSql = "" : sSql = "Select * From Customer_COA Where CC_Parent in (Select gl_ID from chart_of_Accounts where gl_head in(0) and gl_AccHead = 3 and gl_Desc <> '' And gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 )"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "EXPENDITURE" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("CC_Parent").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("CC_Parent")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("CC_GLDesc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("CC_GLDesc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * From Customer_COA Where CC_Head in (1) And CC_Parent in (Select gl_ID from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("CC_Parent") & " and gl_CompID =" & iACID & " and gl_Desc <> '' and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=0 )"
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("CC_Parent").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("CC_Parent")
    '                        End If

    '                        gsql = "select * from Customer_COA where CC_Head= '2' and CC_Parent= " & dtSub.Rows(j)("CC_Parent") & " and cc_custid=" & dtSub.Rows(j)("cc_custid") & " and CC_GLDesc='" & dtSub.Rows(j)("CC_GLDesc") & "' and CC_IndType=" & dtSub.Rows(j)("CC_IndType") & " and CC_CompID=" & dtSub.Rows(j)("CC_CompID") & ""
    '                        dtsub1 = objDBL.SQLExecuteDataTable(sAC, gsql)
    '                        dpD = dtsub1.Rows(0)("CC_GL")

    '                        If IsDBNull(dtSub.Rows(j)("CC_GLDesc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("CC_GLDesc")
    '                        End If

    '                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =3 and SLM_GroupID =" & dtGroup.Rows(i)("CC_Parent") & " and "
    '                        mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("CC_Parent") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & ""
    '                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                        If dtLink.Rows.Count > 0 Then
    '                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                            End If

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        If iStatusCheck = 0 Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next

    '                                            End If
    '                                        Else
    '                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                            iSql = iSql & "ATD_GL =" & sArray(k) & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dpD = sArray(k) Then
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    For a = 0 To dtArray.Rows.Count - 1
    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                        End If

    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                        End If
    '                                                    Next

    '                                                End If
    '                                            End If
    '                                        End If
    '                                        End If
    '                                Next


    '                                dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))

    '                                Dim dDbCr As Double = 0.00
    '                                dDbCr = dDebit - dCredit
    '                                dTotalDebit = dTotalDebit + dDbCr

    '                                dDebit = 0 : dCredit = 0
    '                            End If

    '                            'Last Year
    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                        If dtArray.Rows.Count > 0 Then
    '                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                End If

    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                End If
    '                                            Next

    '                                        End If
    '                                    End If
    '                                Next

    '                                dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

    '                                Dim dLdbCr As Double = 0.00
    '                                dLdbCr = dLDebit - dLCredit
    '                                dTotalLDebit = dTotalLDebit + dLdbCr

    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
    '            dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
    '            dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
    '            dt.Rows.Add(dRow)
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'SubLedger Report
    Public Function LoadSubLeadger(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer)
        Dim sSql As String = "", cSql As String = "", sglSql As String = "", aSql As String = "", mSql As String = "", iSql As String = "", ciSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable, dtGroup As New DataTable, dtSub As New DataTable, dtSub1 As New DataTable, dtSub2 As New DataTable, dtLink As New DataTable, dtArray As New DataTable, dtFA As New DataTable
        Dim dtYear As New DataTable, dtOB As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0, m As Integer = 0, t As Integer = 0
        Dim sArray As Array
        'Dim sArr As String
        Dim dDebit As Double = 0.00, dCredit As Double = 0.00
        Dim dLDebit As Double = 0.00, dLCredit As Double = 0.00
        Dim dTotalDebit As Double = 0.00, dTotalCredit As Double = 0.00
        Dim dTotalLDebit As Double = 0.00, dTotalLCredit As Double = 0.00
        Dim iHead As Integer = 0, iSLNo As Integer = 0, iLastYear As Integer = 0, iStatusCheck As Integer = 0
        'Dim q As Integer
        Dim sColumnd As String, sColumnc As String ', sColumngl As String, sColumndesc As String
        Dim dbeginYearTotal As Double = 0.00, dbeginlYearTotal As Double = 0.00
        Dim dbeginYearTotal1 As Double = 0.00, dbeginlYearTotal1 As Double = 0.00
        Dim dEndYearTotal As Double = 0.00, dEndlYearTotal As Double = 0.00
        Dim dEndYearTotal1 As Double = 0.00, dEndlYearTotal1 As Double = 0.00
        Dim q1 As Integer
        'Dim z() As String
        Dim iSno As Integer = 0

        Dim sColumndATDDEb As String = "", sColumncATDCre As String = ""
        Dim iAllCount As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            'Liabilites

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iACID & ")"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtYear.Rows.Count > 0 Then
                'dr.Read()
                iLastYear = dtYear.Rows(i).Item("YMS_ID")
            Else
                iLastYear = 0
            End If
            'sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_YEARID=" & iYearID & " and YMS_CompID=" & iACID & "" 'Commented vijayalakshmi 12/12/2019 this query fetching the current year
            'dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            'If dtYear.Rows.Count > 0 Then
            '    For i = 0 To dtYear.Rows.Count - 1
            '        iLastYear = dtYear.Rows(i).Item("YMS_YEARID")
            '    Next
            'Else
            '    iLastYear = 0
            'End If

            'iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
            'dtOB = objDBL.SQLExecuteDataTable(sAC, iSql)
            'If dtOB.Rows.Count > 0 Then
            '    For j = 0 To dtOB.Rows.Count - 1
            '        If dtOB.Rows(j).Item("Opn_Status") = "F" Then
            '            iStatusCheck = 0
            '        Else
            '            iStatusCheck = 1
            '        End If
            '    Next
            'Else
            '    iStatusCheck = 1
            'End If

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EQUITY AND LIABILITIES" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If



                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                             dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            dRow("SLNo") = iSLNo + 1

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = "<B>" & dtSub.Rows(j)("gl_Desc") & "</B>"
                            End If

                            dt.Rows.Add(dRow)
                            ' dDebit = 0 : dCredit = 0
                            iSno = 0
                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                            If dtLink.Rows.Count > 0 Then
                                For t = 0 To dtLink.Rows.Count - 1
                                    dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo")
                                    sArray = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'z = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'Array.Sort(z)
                                    'sArray = z

                                    If sArray.Length > 0 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                cSql = "" : cSql = "Select * from Chart_of_accounts where gl_id = " & sArray(k) & " and gl_CompID =" & iACID & " and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " and gl_Delflag ='C' order by gl_id"
                                                dtSub1 = objDBL.SQLExecuteDataTable(sAC, cSql)
                                                If dtSub1.Rows.Count > 0 Then
                                                    For v = 0 To dtSub1.Rows.Count - 1
                                                        dRow = dt.NewRow()
                                                        If IsDBNull(dtSub1.Rows(v)("Gl_id").ToString()) = False Then
                                                            dRow("ID") = dtSub1.Rows(v)("gl_id")
                                                        End If

                                                        If IsDBNull(dtSub1.Rows(v)("gl_Desc").ToString()) = False Then
                                                            dRow("Particulars") = "<B>" & dtSub1.Rows(v)("gl_Desc") & "</B>"
                                                        End If
                                                        dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo") & "." & iSno
                                                        dt.Rows.Add(dRow)

                                                        sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub1.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl"
                                                        dtSub2 = objDBL.SQLExecuteDataTable(sAC, sglSql)
                                                        If dtSub2.Rows.Count > 0 Then
                                                            For g = 0 To dtSub2.Rows.Count - 1
                                                                dRow = dt.NewRow()
                                                                If IsDBNull(dtSub2.Rows(g)("cc_gl").ToString()) = False Then
                                                                    dRow("ID") = dtSub2.Rows(g)("cc_gl")
                                                                End If

                                                                If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                                    dRow("Particulars") = dtSub2.Rows(g)("cc_GLDesc")
                                                                End If
                                                                dt.Rows.Add(dRow)
                                                                dDebit = 0 : dCredit = 0

                                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iACID & " and "
                                                                iSql = iSql & "ATD_Subgl =" & dtSub2.Rows(g)("cc_gl") & " And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iYearID & " and cc_CompID =" & iACID & " and "
                                                                iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                'If dtArray.Rows.Count > 0 Then
                                                                '    For a = 0 To dtArray.Rows.Count - 1
                                                                '        If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                '            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                '            dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                '        End If

                                                                '        If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                '            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                '            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                '        End If
                                                                '    Next
                                                                'End If
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                If dDebit <> 0 And dCredit <> 0 Then
                                                                    dRow("PresentYear") = dDebit - dCredit
                                                                ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                                    dRow("PresentYear") = dDebit
                                                                ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                                    dRow("PresentYear") = dCredit
                                                                Else dDebit = 0 And (dCredit = 0)
                                                                    dRow("PresentYear") = "0.00"
                                                                End If
                                                                If dRow("PresentYear").StartsWith("-") = True Then
                                                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                                End If

                                                                dDebit = 0 : dCredit = 0


                                                                'Last Year

                                                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")

                                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iACID & " and "
                                                                iSql = iSql & "ATD_Subgl =" & dtSub2.Rows(g)("cc_gl") & " And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                                iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If


                                                                If dLDebit <> 0 And dLCredit <> 0 Then
                                                                    dRow("LastYear") = dLDebit - dLCredit
                                                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                                    dRow("LastYear") = dLDebit
                                                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                                    dRow("LastYear") = dLCredit
                                                                Else dLDebit = 0 And (dLCredit = 0)
                                                                    dRow("LastYear") = "0.00"
                                                                End If
                                                                If dRow("LastYear").StartsWith("-") = True Then
                                                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                                End If

                                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0



                                                                If dtSub1.Rows(v)("gl_id") = 60 Then
                                                                    dRow = dt.NewRow()

                                                                    If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                                        dRow("Particulars") = "Add: Current year profit/(loss)"
                                                                    End If
                                                                    dt.Rows.Add(dRow)
                                                                    dDebit = 0 : dCredit = 0

                                                                    iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_Financialyear =" & iYearID & " and SS_CompID =" & iACID & " and "
                                                                    iSql = iSql & "SS_Group =60 And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & ""
                                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                    If dtArray.Rows.Count > 0 Then
                                                                        For a = 0 To dtArray.Rows.Count - 1

                                                                            If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                                dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            End If
                                                                        Next
                                                                    End If

                                                                    If dDebit <> 0 And dCredit <> 0 Then
                                                                        dRow("PresentYear") = dDebit - dCredit
                                                                    ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                                        dRow("PresentYear") = dDebit
                                                                    ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                                        dRow("PresentYear") = dCredit
                                                                    Else dDebit = 0 And (dCredit = 0)
                                                                        dRow("PresentYear") = "0.00"
                                                                    End If
                                                                    If dRow("PresentYear").StartsWith("-") = True Then
                                                                        dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                                    End If

                                                                    dDebit = 0 : dCredit = 0


                                                                    'Last Year

                                                                    sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")

                                                                    iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_Financialyear =" & iLastYear & " and SS_CompID =" & iACID & " and "
                                                                    iSql = iSql & "SS_Group =60 And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & ""
                                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                    If dtArray.Rows.Count > 0 Then
                                                                        For a = 0 To dtArray.Rows.Count - 1

                                                                            If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                                                dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                                dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            End If
                                                                        Next
                                                                    End If

                                                                    If dLDebit <> 0 And dLCredit <> 0 Then
                                                                        dRow("LastYear") = dLDebit - dLCredit
                                                                    ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                                        dRow("LastYear") = dLDebit
                                                                    ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                                        dRow("LastYear") = dLCredit
                                                                    Else dLDebit = 0 And (dLCredit = 0)
                                                                        dRow("LastYear") = "0.00"
                                                                    End If
                                                                    If dRow("LastYear").StartsWith("-") = True Then
                                                                        dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                                    End If

                                                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                                                End If
                                                            Next
                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                                                            If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit - dTotalDebit
                                                            ElseIf (dTotalDebit <> 0) Then
                                                                dRow("PresentYear") = dTotalDebit
                                                            ElseIf (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit
                                                            ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                                                dRow("PresentYear") = "0.00"
                                                            End If

                                                            If dRow("PresentYear").StartsWith("-") = True Then
                                                                dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                            End If

                                                            If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit - dTotalLDebit
                                                            ElseIf (dTotalLDebit <> 0) Then
                                                                dRow("LastYear") = dTotalLDebit
                                                            ElseIf (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit
                                                            ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                                                dRow("LastYear") = "0.00"
                                                            End If

                                                            If dRow("LastYear").StartsWith("-") = True Then
                                                                dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                            End If

                                                            dt.Rows.Add(dRow)

                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = ""
                                                            dt.Rows.Add(dRow)
                                                            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                                                        Else

                                                            If dtSub1.Rows(v)("gl_id") = 60 Then
                                                                dRow = dt.NewRow()

                                                                ' If IsDBNull(dtSub2.Rows(0)("cc_GLDesc").ToString()) = False Then
                                                                dRow("Particulars") = "Add: Current year profit/(loss)"
                                                                'End If
                                                                dt.Rows.Add(dRow)
                                                                dDebit = 0 : dCredit = 0

                                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_Financialyear =" & iYearID & " and SS_CompID =" & iACID & " and "
                                                                iSql = iSql & "SS_Group =60 And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1

                                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                If dDebit <> 0 And dCredit <> 0 Then
                                                                    dRow("PresentYear") = dDebit - dCredit
                                                                ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                                    dRow("PresentYear") = dDebit
                                                                ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                                    dRow("PresentYear") = dCredit
                                                                Else dDebit = 0 And (dCredit = 0)
                                                                    dRow("PresentYear") = "0.00"
                                                                End If
                                                                If dRow("PresentYear").StartsWith("-") = True Then
                                                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                                End If

                                                                dDebit = 0 : dCredit = 0


                                                                'Last Year

                                                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")

                                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_Financialyear =" & iLastYear & " and SS_CompID =" & iACID & " and "
                                                                iSql = iSql & "SS_Group =60 And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1

                                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                If dLDebit <> 0 And dLCredit <> 0 Then
                                                                    dRow("LastYear") = dLDebit - dLCredit
                                                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                                    dRow("LastYear") = dLDebit
                                                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                                    dRow("LastYear") = dLCredit
                                                                Else dLDebit = 0 And (dLCredit = 0)
                                                                    dRow("LastYear") = "0.00"
                                                                End If
                                                                If dRow("LastYear").StartsWith("-") = True Then
                                                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                                End If

                                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                                            End If

                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                                                            If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit - dTotalDebit
                                                            ElseIf (dTotalDebit <> 0) Then
                                                                dRow("PresentYear") = dTotalDebit
                                                            ElseIf (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit
                                                            ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                                                dRow("PresentYear") = "0.00"
                                                            End If

                                                            If dRow("PresentYear").StartsWith("-") = True Then
                                                                dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                            End If

                                                            If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit - dTotalLDebit
                                                            ElseIf (dTotalLDebit <> 0) Then
                                                                dRow("LastYear") = dTotalLDebit
                                                            ElseIf (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit
                                                            ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                                                dRow("LastYear") = "0.00"
                                                            End If

                                                            If dRow("LastYear").StartsWith("-") = True Then
                                                                dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                            End If

                                                            dt.Rows.Add(dRow)

                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = ""
                                                            dt.Rows.Add(dRow)
                                                            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                                                        End If
                                                    Next

                                                End If
                                            End If
                                            iSno = iSno + 1
                                        Next
                                    End If
                                Next
                            End If
                            iSLNo = iSLNo + 1
                        Next
                    End If
                Next

            End If


            'Assets
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 1 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "ASSETS" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If



                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)


                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            dRow("SLNo") = iSLNo + 1

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = "<B>" & dtSub.Rows(j)("gl_Desc") & "</B>"
                            End If

                            dt.Rows.Add(dRow)
                            ' dDebit = 0 : dCredit = 0
                            iSno = 0
                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                            If dtLink.Rows.Count > 0 Then
                                For t = 0 To dtLink.Rows.Count - 1
                                    dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo")
                                    sArray = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'z = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'Array.Sort(z)
                                    'sArray = z.ToString().Split(",")
                                    If sArray.Length > 0 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                cSql = "" : cSql = "Select * from Chart_of_accounts where gl_id = " & sArray(k) & " and gl_CompID =" & iACID & " and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " and gl_Delflag ='C' order by gl_id"
                                                dtSub1 = objDBL.SQLExecuteDataTable(sAC, cSql)
                                                If dtSub1.Rows.Count > 0 Then
                                                    For v = 0 To dtSub1.Rows.Count - 1
                                                        dRow = dt.NewRow()
                                                        If IsDBNull(dtSub1.Rows(v)("Gl_id").ToString()) = False Then
                                                            dRow("ID") = dtSub1.Rows(v)("gl_id")
                                                        End If

                                                        If IsDBNull(dtSub1.Rows(v)("gl_Desc").ToString()) = False Then
                                                            dRow("Particulars") = "<B>" & dtSub1.Rows(v)("gl_Desc") & "</B>"
                                                        End If

                                                        dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo") & "." & iSno
                                                        dt.Rows.Add(dRow)

                                                        sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub1.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl"
                                                        dtSub2 = objDBL.SQLExecuteDataTable(sAC, sglSql)
                                                        If dtSub2.Rows.Count > 0 Then
                                                            For g = 0 To dtSub2.Rows.Count - 1

                                                                dRow = dt.NewRow()
                                                                If IsDBNull(dtSub2.Rows(g)("cc_gl").ToString()) = False Then
                                                                    dRow("ID") = dtSub2.Rows(g)("cc_gl")
                                                                End If

                                                                If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                                    dRow("Particulars") = dtSub2.Rows(g)("cc_GLDesc")
                                                                End If
                                                                dt.Rows.Add(dRow)
                                                                dDebit = 0 : dCredit = 0
                                                                If dtLink.Rows(0)("SLM_NoteNo") = 21 Then

                                                                    iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
                                                                    iSql = iSql & "CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                                    sColumnd = "CI_CBValues"
                                                                    sColumnc = ""
                                                                Else
                                                                    iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
                                                                    iSql = iSql & "ATD_SubGL = " & dtSub2.Rows(g)("cc_gl") & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                    iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                    If iAllCount > 0 Then
                                                                        iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iYearID & " And a.CC_CompID =" & iACID & " And "
                                                                        iSql = iSql & "a.CC_gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                        sColumndATDDEb = "ATD_Debit"
                                                                        sColumncATDCre = "ATD_Credit"
                                                                    Else
                                                                        iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iYearID & " and cc_CompID =" & iACID & " and "
                                                                        iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                    End If

                                                                End If


                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1

                                                                        If sColumnc <> "" Then
                                                                            If iAllCount > 0 Then
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumndATDDEb).ToString()) = False) And (dtArray.Rows(a)(sColumndATDDEb).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    Else
                                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    End If

                                                                                End If

                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumncATDCre).ToString()) = False) And (dtArray.Rows(a)(sColumncATDCre).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    Else
                                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    End If
                                                                                End If
                                                                            Else
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                End If
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
                                                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                End If
                                                                            End If

                                                                        Else
                                                                            If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                            End If
                                                                        End If
                                                                    Next
                                                                End If

                                                                If dDebit <> 0 And dCredit <> 0 Then
                                                                    dRow("PresentYear") = dDebit - dCredit
                                                                ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                                    dRow("PresentYear") = dDebit
                                                                ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                                    dRow("PresentYear") = dCredit
                                                                Else dDebit = 0 And (dCredit = 0)
                                                                    dRow("PresentYear") = "0.00"
                                                                End If
                                                                If dRow("PresentYear").StartsWith("-") = True Then
                                                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                                End If

                                                                dDebit = 0 : dCredit = 0

                                                                'Last Year

                                                                If dtLink.Rows(0)("SLM_NoteNo") = 21 Then
                                                                    iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iACID & " And "
                                                                    iSql = iSql & "CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                                    sColumnd = "CI_CBValues"
                                                                    sColumnc = ""
                                                                Else
                                                                    iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details  where ATD_YearID =" & iLastYear & " And ATD_CompID =" & iACID & " And "
                                                                    iSql = iSql & "ATD_SubGL = " & dtSub2.Rows(g)("cc_gl") & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                    iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                    If iAllCount > 0 Then
                                                                        iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iLastYear & " And a.CC_CompID =" & iACID & " And "
                                                                        iSql = iSql & "a.CC_Gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                        sColumndATDDEb = "ATD_Debit"
                                                                        sColumncATDCre = "ATD_Credit"
                                                                    Else
                                                                        iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                                        iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                    End If
                                                                End If

                                                                'iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                                'iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString())
                                                                        'End If
                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        '    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        '    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        'End If
                                                                        If sColumnc <> "" Then
                                                                            If iAllCount > 0 Then
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumndATDDEb).ToString()) = False) And (dtArray.Rows(a)(sColumndATDDEb).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    Else
                                                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    End If

                                                                                End If

                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumncATDCre).ToString()) = False) And (dtArray.Rows(a)(sColumncATDCre).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    Else
                                                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    End If
                                                                                End If
                                                                            Else
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                    dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                End If
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
                                                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                    dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                End If
                                                                            End If
                                                                        Else
                                                                            If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                            End If
                                                                        End If
                                                                    Next
                                                                End If
                                                                'End If
                                                                '         Next


                                                                If dLDebit <> 0 And dLCredit <> 0 Then
                                                                    dRow("LastYear") = dLDebit - dLCredit
                                                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                                    dRow("LastYear") = dLDebit
                                                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                                    dRow("LastYear") = dLCredit
                                                                Else dLDebit = 0 And (dLCredit = 0)
                                                                    dRow("LastYear") = "0.00"
                                                                End If
                                                                If dRow("LastYear").StartsWith("-") = True Then
                                                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                                End If

                                                                'dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                                                ' End If

                                                                'Next
                                                                'End If
                                                            Next
                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                                                            If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit - dTotalDebit
                                                            ElseIf (dTotalDebit <> 0) Then
                                                                dRow("PresentYear") = dTotalDebit
                                                            ElseIf (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit
                                                            ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                                                dRow("PresentYear") = "0.00"
                                                            End If

                                                            If dRow("PresentYear").StartsWith("-") = True Then
                                                                dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                            End If

                                                            If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit - dTotalLDebit
                                                            ElseIf (dTotalLDebit <> 0) Then
                                                                dRow("LastYear") = dTotalLDebit
                                                            ElseIf (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit
                                                            ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                                                dRow("LastYear") = "0.00"
                                                            End If

                                                            If dRow("LastYear").StartsWith("-") = True Then
                                                                dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                            End If

                                                            dt.Rows.Add(dRow)

                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = ""
                                                            dt.Rows.Add(dRow)
                                                            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                                                        End If

                                                    Next

                                                End If
                                            End If
                                            iSno = iSno + 1
                                        Next
                                    End If
                                Next
                            End If
                            iSLNo = iSLNo + 1
                            'dt.Rows.Add(dRow)
                        Next
                    End If
                    'iSLNo = iSLNo + 1
                Next
                'dRow = dt.NewRow()
                'dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                'dRow("PresentYear") = dTotalDebit 'dTotalCredit - dTotalDebit
                'dRow("LastYear") = dTotalLDebit 'dTotalLCredit - dTotalLDebit
                'dt.Rows.Add(dRow)
            End If





            'Income
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 2 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "REVENUE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If


                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)


                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            dRow("SLNo") = iSLNo + 1

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = "<B>" & dtSub.Rows(j)("gl_Desc") & "</B>"
                            End If

                            dt.Rows.Add(dRow)
                            ' dDebit = 0 : dCredit = 0
                            iSno = 0
                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 2 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                            If dtLink.Rows.Count > 0 Then
                                For t = 0 To dtLink.Rows.Count - 1
                                    dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo")
                                    sArray = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'z = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'Array.Sort(z)
                                    'sArray = z.ToString().Split(",")
                                    If sArray.Length > 0 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                cSql = "" : cSql = "Select * from Chart_of_accounts where gl_id = " & sArray(k) & " and gl_CompID =" & iACID & " and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " and gl_Delflag ='C' order by gl_id"
                                                dtSub1 = objDBL.SQLExecuteDataTable(sAC, cSql)
                                                If dtSub1.Rows.Count > 0 Then
                                                    For v = 0 To dtSub1.Rows.Count - 1
                                                        dRow = dt.NewRow()
                                                        If IsDBNull(dtSub1.Rows(v)("Gl_id").ToString()) = False Then
                                                            dRow("ID") = dtSub1.Rows(v)("gl_id")
                                                        End If

                                                        If IsDBNull(dtSub1.Rows(v)("gl_Desc").ToString()) = False Then
                                                            dRow("Particulars") = "<B>" & dtSub1.Rows(v)("gl_Desc") & "</B>"
                                                        End If
                                                        dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo") & "." & iSno
                                                        dt.Rows.Add(dRow)

                                                        sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub1.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl"
                                                        dtSub2 = objDBL.SQLExecuteDataTable(sAC, sglSql)
                                                        If dtSub2.Rows.Count > 0 Then
                                                            For g = 0 To dtSub2.Rows.Count - 1
                                                                dRow = dt.NewRow()
                                                                If IsDBNull(dtSub2.Rows(g)("cc_gl").ToString()) = False Then
                                                                    dRow("ID") = dtSub2.Rows(g)("cc_gl")
                                                                End If

                                                                If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                                    dRow("Particulars") = dtSub2.Rows(g)("cc_GLDesc")
                                                                End If
                                                                dt.Rows.Add(dRow)
                                                                dDebit = 0 : dCredit = 0

                                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iACID & " and "
                                                                iSql = iSql & "ATD_Subgl =" & dtSub2.Rows(g)("cc_gl") & " And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iYearID & " and cc_CompID =" & iACID & " and "
                                                                iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        '    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        '    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        'End If
                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                If dDebit <> 0 And dCredit <> 0 Then
                                                                    dRow("PresentYear") = dDebit - dCredit
                                                                ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                                    dRow("PresentYear") = dDebit
                                                                ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                                    dRow("PresentYear") = dCredit
                                                                Else dDebit = 0 And (dCredit = 0)
                                                                    dRow("PresentYear") = "0.00"
                                                                End If
                                                                If dRow("PresentYear").StartsWith("-") = True Then
                                                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                                End If



                                                                'dTotalDebit = dTotalDebit + dRow("PresentYear")
                                                                dDebit = 0 : dCredit = 0
                                                                ' End If


                                                                'Last Year

                                                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")

                                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iACID & " and "
                                                                iSql = iSql & "ATD_Subgl =" & dtSub2.Rows(g)("cc_gl") & " And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If

                                                                iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                                iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString())
                                                                        'End If
                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then ' And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                            dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        End If

                                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                            dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString()) ' + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        End If
                                                                    Next
                                                                End If
                                                                'End If
                                                                '         Next


                                                                If dLDebit <> 0 And dLCredit <> 0 Then
                                                                    dRow("LastYear") = dLDebit - dLCredit
                                                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                                    dRow("LastYear") = dLDebit
                                                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                                    dRow("LastYear") = dLCredit
                                                                Else dLDebit = 0 And (dLCredit = 0)
                                                                    dRow("LastYear") = "0.00"
                                                                End If
                                                                If dRow("LastYear").StartsWith("-") = True Then
                                                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                                End If

                                                                'dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                                                'End If
                                                            Next
                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                                                            If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit - dTotalDebit
                                                            ElseIf (dTotalDebit <> 0) Then
                                                                dRow("PresentYear") = dTotalDebit
                                                            ElseIf (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit
                                                            ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                                                dRow("PresentYear") = "0.00"
                                                            End If

                                                            If dRow("PresentYear").StartsWith("-") = True Then
                                                                dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                            End If

                                                            If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit - dTotalLDebit
                                                            ElseIf (dTotalLDebit <> 0) Then
                                                                dRow("LastYear") = dTotalLDebit
                                                            ElseIf (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit
                                                            ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                                                dRow("LastYear") = "0.00"
                                                            End If

                                                            If dRow("LastYear").StartsWith("-") = True Then
                                                                dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                            End If

                                                            dt.Rows.Add(dRow)

                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = ""
                                                            dt.Rows.Add(dRow)
                                                            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            iSno = iSno + 1
                                        Next
                                    End If
                                Next
                            End If
                            iSLNo = iSLNo + 1
                            'dt.Rows.Add(dRow)
                        Next
                    End If
                    'iSLNo = iSLNo + 1
                Next
                'dRow = dt.NewRow()
                'dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                'dRow("PresentYear") = dTotalDebit 'dTotalCredit - dTotalDebit
                'dRow("LastYear") = dTotalLDebit 'dTotalLCredit - dTotalLDebit
                'dt.Rows.Add(dRow)
            End If


            'Expenditure
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 3 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Expenditure" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If


                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            dRow("SLNo") = iSLNo + 1

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = "<B>" & dtSub.Rows(j)("gl_Desc") & "</B>"
                            End If

                            dt.Rows.Add(dRow)
                            ' dDebit = 0 : dCredit = 0
                            iSno = 0
                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 3 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                            If dtLink.Rows.Count > 0 Then
                                For t = 0 To dtLink.Rows.Count - 1
                                    dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo")
                                    sArray = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'z = dtLink.Rows(t)("SLM_GLLedger").ToString().Split(",")
                                    'Array.Sort(z)
                                    'sArray = z.ToString().Split(",")
                                    If sArray.Length > 0 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                cSql = "" : cSql = "Select * from Chart_of_accounts where gl_id = " & sArray(k) & " and gl_CompID =" & iACID & " and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custID=" & iCustID & " and gl_Delflag ='C' order by gl_id"
                                                dtSub1 = objDBL.SQLExecuteDataTable(sAC, cSql)
                                                If dtSub1.Rows.Count > 0 Then
                                                    For v = 0 To dtSub1.Rows.Count - 1
                                                        dRow = dt.NewRow()
                                                        If IsDBNull(dtSub1.Rows(v)("Gl_id").ToString()) = False Then
                                                            dRow("ID") = dtSub1.Rows(v)("gl_id")
                                                        End If

                                                        If IsDBNull(dtSub1.Rows(v)("gl_Desc").ToString()) = False Then
                                                            dRow("Particulars") = "<B>" & dtSub1.Rows(v)("gl_Desc") & "</B>"
                                                        End If
                                                        dRow("NoteNo") = dtLink.Rows(t)("SLM_NoteNo") & "." & iSno
                                                        dt.Rows.Add(dRow)

                                                        If dtLink.Rows(0)("SLM_NoteNo") = 35 Then
                                                            If dtSub1.Rows(v)("gl_Desc") = "Opening Stock : Raw Material" Then
                                                                sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = 198 and cc_CompID =" & iACID & "  And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl" 'and cc_Status ='W'
                                                                'sglSql = sglSql & "Opn_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & ""
                                                                'sColumngl = "Opn_DebitAmt"
                                                                'sColumndesc = "Opn_CreditAmount"
                                                            ElseIf dtSub1.Rows(v)("gl_Desc") = "Closing Stock : Raw Material" Then
                                                                sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = 198 and cc_CompID =" & iACID & "  And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl" 'and cc_Status ='W'
                                                                'sglSql = sglSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & ""
                                                                ' sColumngl = "CI_CBValues"
                                                                'sColumndesc = ""
                                                            Else
                                                                sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub1.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & "  And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl" 'and cc_Status ='W'
                                                                ' sColumngl = "cc_gl"
                                                                'sColumndesc = "cc_GLDesc"
                                                            End If
                                                        Else
                                                            sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub1.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl"
                                                        End If

                                                        ' sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub1.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl"
                                                        dtSub2 = objDBL.SQLExecuteDataTable(sAC, sglSql)
                                                        If dtSub2.Rows.Count > 0 Then
                                                            For g = 0 To dtSub2.Rows.Count - 1
                                                                dRow = dt.NewRow()
                                                                If IsDBNull(dtSub2.Rows(g)("cc_gl").ToString()) = False Then
                                                                    dRow("ID") = dtSub2.Rows(g)("cc_gl")
                                                                End If

                                                                If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                                    dRow("Particulars") = dtSub2.Rows(g)("cc_GLDesc")
                                                                End If

                                                                dt.Rows.Add(dRow)
                                                                dDebit = 0 : dCredit = 0
                                                                If dtLink.Rows(0)("SLM_NoteNo") = 35 Then
                                                                    If dtSub1.Rows(v)("gl_Desc") = "Opening Stock : Raw Material" Then

                                                                        iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
                                                                        iSql = iSql & "ATD_GL =198  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                        iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                        If iAllCount > 0 Then
                                                                            iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iYearID & " And a.CC_CompID =" & iACID & " And "
                                                                            iSql = iSql & "a.CC_Parent =198 and a.cc_gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                            sColumndATDDEb = "ATD_Debit"
                                                                            sColumncATDCre = "ATD_Credit"
                                                                        Else
                                                                            iSql = "" : iSql = "Select * from customer_coa where CC_YearID =" & iYearID & " And CC_CompID =" & iACID & " And "
                                                                            iSql = iSql & "CC_Parent =198 and cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                        End If
                                                                    ElseIf dtSub1.Rows(v)("gl_Desc") = "Closing Stock : Raw Material" Then
                                                                        iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
                                                                        iSql = iSql & "CI_GLID =198 and CI_Subglid =" & dtSub2.Rows(g)("cc_gl") & "  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_Status<>'D'"
                                                                        sColumnd = "CI_CBValues"
                                                                        sColumnc = ""
                                                                    Else
                                                                        iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
                                                                        iSql = iSql & "ATD_subGL =" & dtSub2.Rows(g)("cc_gl") & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                        iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                        If iAllCount > 0 Then
                                                                            iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iYearID & " And a.CC_CompID =" & iACID & " And "
                                                                            iSql = iSql & "a.CC_gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                            sColumndATDDEb = "ATD_Debit"
                                                                            sColumncATDCre = "ATD_Credit"
                                                                        Else
                                                                            iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iYearID & " and cc_CompID =" & iACID & " and "
                                                                            iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                        End If
                                                                    End If
                                                                Else
                                                                    iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
                                                                    iSql = iSql & "ATD_SubGL =" & dtSub2.Rows(g)("cc_gl") & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                    iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                    If iAllCount > 0 Then
                                                                        iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iYearID & " And a.CC_CompID =" & iACID & " And "
                                                                        iSql = iSql & "a.CC_gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                        sColumndATDDEb = "ATD_Debit"
                                                                        sColumncATDCre = "ATD_Credit"
                                                                    Else
                                                                        iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iYearID & " and cc_CompID =" & iACID & " and "
                                                                        iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                    End If
                                                                End If


                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        'If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        '    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        '    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        'End If
                                                                        If sColumnc <> "" Then
                                                                            If iAllCount > 0 Then
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumndATDDEb).ToString()) = False) And (dtArray.Rows(a)(sColumndATDDEb).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    Else
                                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    End If

                                                                                End If

                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumncATDCre).ToString()) = False) And (dtArray.Rows(a)(sColumncATDCre).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    Else
                                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    End If
                                                                                End If
                                                                            Else
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                End If
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
                                                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                End If
                                                                            End If
                                                                        Else
                                                                            If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                            End If
                                                                        End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        '    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        '    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        'End If
                                                                    Next
                                                                End If

                                                                If dDebit <> 0 And dCredit <> 0 Then
                                                                    dRow("PresentYear") = dDebit - dCredit
                                                                ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                                    dRow("PresentYear") = dDebit
                                                                ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                                    dRow("PresentYear") = dCredit
                                                                Else dDebit = 0 And (dCredit = 0)
                                                                    dRow("PresentYear") = "0.00"
                                                                End If
                                                                If dRow("PresentYear").StartsWith("-") = True Then
                                                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                                End If



                                                                'dTotalDebit = dTotalDebit + dRow("PresentYear")
                                                                dDebit = 0 : dCredit = 0
                                                                ' End If


                                                                'Last Year

                                                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                                                'If sArray.Length - 1 Then
                                                                '    For k = 0 To sArray.Length - 1
                                                                '        If sArray(k) <> "" Then
                                                                'iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
                                                                'iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                                'dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                'If dtArray.Rows.Count > 0 Then
                                                                '    For a = 0 To dtArray.Rows.Count - 1
                                                                '        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                                '            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                                '        End If

                                                                '        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                                '            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                                '        End If
                                                                '    Next
                                                                'End If
                                                                If dtLink.Rows(0)("SLM_NoteNo") = 35 Then
                                                                    If dtSub1.Rows(v)("gl_Desc") = "Opening Stock : Raw Material" Then

                                                                        iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details  where ATD_YearID =" & iLastYear & " And ATD_CompID =" & iACID & " And "
                                                                        iSql = iSql & "ATD_GL =198  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                        iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                        If iAllCount > 0 Then
                                                                            iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iLastYear & " And a.CC_CompID =" & iACID & " And "
                                                                            iSql = iSql & "a.CC_Parent =198 and a.cc_gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                            sColumndATDDEb = "ATD_Debit"
                                                                            sColumncATDCre = "ATD_Credit"
                                                                        Else
                                                                            iSql = "" : iSql = "Select * from customer_coa where CC_YearID =" & iLastYear & " And CC_CompID =" & iACID & " And "
                                                                            iSql = iSql & "CC_Parent =198 and cc_gl =" & dtSub2.Rows(g)("cc_gl") & "  And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                        End If
                                                                    ElseIf dtSub1.Rows(v)("gl_Desc") = "Closing Stock : Raw Material" Then
                                                                        iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iACID & " And "
                                                                        iSql = iSql & "CI_GLID =198 and CI_Subglid =" & dtSub2.Rows(g)("cc_gl") & "  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_Status<>'D'"
                                                                        sColumnd = "CI_CBValues"
                                                                        sColumnc = ""
                                                                    Else
                                                                        iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details where ATD_YearID =" & iLastYear & " And ATD_CompID =" & iACID & " And "
                                                                        iSql = iSql & "ATD_SubGL =" & dtSub2.Rows(g)("cc_gl") & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                        iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                        If iAllCount > 0 Then
                                                                            iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iLastYear & " And a.CC_CompID =" & iACID & " And "
                                                                            iSql = iSql & "a.CC_Gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                            sColumndATDDEb = "ATD_Debit"
                                                                            sColumncATDCre = "ATD_Credit"
                                                                        Else
                                                                            iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                                            iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                            sColumnd = "CC_CloseDebit"
                                                                            sColumnc = "CC_CloseCredit"
                                                                        End If
                                                                    End If
                                                                Else
                                                                    iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details where ATD_YearID =" & iLastYear & " And ATD_CompID =" & iACID & " And "
                                                                    iSql = iSql & "ATD_subGL =" & dtSub2.Rows(g)("cc_gl") & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                                    iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
                                                                    If iAllCount > 0 Then
                                                                        iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iLastYear & " And a.CC_CompID =" & iACID & " And "
                                                                        iSql = iSql & "a.CC_gl =" & dtSub2.Rows(g)("cc_gl") & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & " and b.ATD_Status='A'"
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                        sColumndATDDEb = "ATD_Debit"
                                                                        sColumncATDCre = "ATD_Credit"
                                                                    Else
                                                                        iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                                        iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                        sColumnd = "CC_CloseDebit"
                                                                        sColumnc = "CC_CloseCredit"
                                                                    End If
                                                                End If

                                                                'iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                                'iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                                If dtArray.Rows.Count > 0 Then
                                                                    For a = 0 To dtArray.Rows.Count - 1
                                                                        'If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        '    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then 'And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        '    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) '+ Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        'End If

                                                                        If sColumnc <> "" Then
                                                                            If iAllCount > 0 Then
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumndATDDEb).ToString()) = False) And (dtArray.Rows(a)(sColumndATDDEb).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    Else
                                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                        dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
                                                                                    End If

                                                                                End If

                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumncATDCre).ToString()) = False) And (dtArray.Rows(a)(sColumncATDCre).ToString() <> "") Then
                                                                                    If a = 0 Then
                                                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    Else
                                                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                        dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
                                                                                    End If
                                                                                End If
                                                                            Else
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                    dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                End If
                                                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
                                                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                    dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                                                End If
                                                                            End If
                                                                        Else
                                                                            If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                                                dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                                dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                                            End If
                                                                        End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrDebit").ToString()) = False) And (dtArray.Rows(a)("CC_TrDebit").ToString() <> "") Then
                                                                        '    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        '    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrDebit").ToString())
                                                                        'End If

                                                                        'If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") And (IsDBNull(dtArray.Rows(a)("CC_TrCredit").ToString()) = False) And (dtArray.Rows(a)("CC_TrCredit").ToString() <> "") Then
                                                                        '    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        '    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString()) + Convert.ToDouble(dtArray.Rows(a)("CC_TrCredit").ToString())
                                                                        'End If
                                                                    Next
                                                                End If
                                                                'End If
                                                                '         Next


                                                                If dLDebit <> 0 And dLCredit <> 0 Then
                                                                    dRow("LastYear") = dLDebit - dLCredit
                                                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                                    dRow("LastYear") = dLDebit
                                                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                                    dRow("LastYear") = dLCredit
                                                                Else dLDebit = 0 And (dLCredit = 0)
                                                                    dRow("LastYear") = "0.00"
                                                                End If
                                                                If dRow("LastYear").StartsWith("-") = True Then
                                                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                                End If

                                                                'dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                                                'End If
                                                            Next
                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                                                            If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit - dTotalDebit
                                                            ElseIf (dTotalDebit <> 0) Then
                                                                dRow("PresentYear") = dTotalDebit
                                                            ElseIf (dTotalCredit <> 0) Then
                                                                dRow("PresentYear") = dTotalCredit
                                                            ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                                                dRow("PresentYear") = "0.00"
                                                            End If

                                                            If dRow("PresentYear").StartsWith("-") = True Then
                                                                dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                            End If

                                                            If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit - dTotalLDebit
                                                            ElseIf (dTotalLDebit <> 0) Then
                                                                dRow("LastYear") = dTotalLDebit
                                                            ElseIf (dTotalLCredit <> 0) Then
                                                                dRow("LastYear") = dTotalLCredit
                                                            ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                                                dRow("LastYear") = "0.00"
                                                            End If
                                                            If dRow("LastYear").StartsWith("-") = True Then
                                                                dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                            End If


                                                            dt.Rows.Add(dRow)

                                                            dRow = dt.NewRow()
                                                            dRow("Particulars") = ""
                                                            dt.Rows.Add(dRow)
                                                            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            iSno = iSno + 1
                                        Next
                                    End If
                                Next
                            End If
                            iSLNo = iSLNo + 1
                        Next
                    End If
                Next
            End If
            Dim schSql As String
            Dim iCount As Integer

            schSql = "Select * from Schedule_Linkage_Master where SLM_CompID =" & iACID & " and SLM_NoteNo <> 0 And SLM_OrgID=" & iOrgID & " and SLM_CustID=" & iCustID & " and SLM_NoteNo=21"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, schSql)
            If dtGroup.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Changes in inventories of finished goods, work-in-progress and stock-in-trade " & "</B>"
                dt.Rows.Add(dRow)
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Inventories at the end of the year" & "</B>"
                dt.Rows.Add(dRow)
                sArray = dtGroup.Rows(0)("SLM_GLLedger").ToString().Split(",")
                If sArray.Length > 0 Then
                    For k = 0 To sArray.Length - 1
                        q1 = sArray.Length - 1
                        If sArray(k) <> "" Then
                            aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
                            dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                            If dtSub.Rows.Count > 0 Then
                                For v = 0 To dtSub.Rows.Count - 1
                                    If dtSub.Rows(0)("gl_id") <> 198 Then
                                        dRow = dt.NewRow()
                                        dRow("Particulars") = "<B>" & dtSub.Rows(0)("gl_Desc") & "</B>"
                                        dt.Rows.Add(dRow)
                                        sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl"
                                        dtSub2 = objDBL.SQLExecuteDataTable(sAC, sglSql)
                                        If dtSub2.Rows.Count > 0 Then
                                            For g = 0 To dtSub2.Rows.Count - 1
                                                dRow = dt.NewRow()
                                                If IsDBNull(dtSub2.Rows(g)("cc_gl").ToString()) = False Then
                                                    dRow("ID") = dtSub2.Rows(g)("cc_gl")
                                                End If

                                                If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                    dRow("Particulars") = dtSub2.Rows(g)("cc_GLDesc")
                                                End If
                                                dt.Rows.Add(dRow)
                                                dDebit = 0 : dCredit = 0
                                                'Current Year
                                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                                If iCount <> 0 Then
                                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And "
                                                    iSql = iSql & "CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"

                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                                dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    dDebit = 0
                                                    ' dTotalDebit = 0
                                                End If



                                                'Last year
                                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                                If iCount <> 0 Then
                                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " And ci_CompID =" & iACID & " And "
                                                    iSql = iSql & "CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                                dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                                dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    dDebit = 0
                                                    ' dTotalDebit = 0
                                                End If

                                                If dDebit <> 0 And dCredit <> 0 Then
                                                    dRow("PresentYear") = dDebit - dCredit
                                                ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                    dRow("PresentYear") = dDebit
                                                ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                    dRow("PresentYear") = dCredit
                                                Else dDebit = 0 And (dCredit = 0)
                                                    dRow("PresentYear") = "0.00"
                                                End If
                                                If dRow("PresentYear").StartsWith("-") = True Then
                                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                End If

                                                If dLDebit <> 0 And dLCredit <> 0 Then
                                                    dRow("LastYear") = dLDebit - dLCredit
                                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                    dRow("LastYear") = dLDebit
                                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                    dRow("LastYear") = dLCredit
                                                Else dLDebit = 0 And (dLCredit = 0)
                                                    dRow("LastYear") = "0.00"
                                                End If
                                                If dRow("LastYear").StartsWith("-") = True Then
                                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                End If

                                                ' dt.Rows.Add(dRow)
                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                            Next
                                        End If
                                        dRow = dt.NewRow()
                                        dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                                        If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                            dRow("PresentYear") = dTotalCredit - dTotalDebit
                                        ElseIf (dTotalDebit <> 0) Then
                                            dRow("PresentYear") = dTotalDebit
                                        ElseIf (dTotalCredit <> 0) Then
                                            dRow("PresentYear") = dTotalCredit
                                        ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                            dRow("PresentYear") = "0.00"
                                        End If

                                        If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                            dRow("LastYear") = dTotalLCredit - dTotalLDebit
                                        ElseIf (dTotalLDebit <> 0) Then
                                            dRow("LastYear") = dTotalLDebit
                                        ElseIf (dTotalLCredit <> 0) Then
                                            dRow("LastYear") = dTotalLCredit
                                        ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                            dRow("LastYear") = "0.00"
                                        End If

                                        dt.Rows.Add(dRow)

                                        dRow = dt.NewRow()
                                        dRow("Particulars") = ""
                                        dt.Rows.Add(dRow)
                                        dEndYearTotal1 = dEndYearTotal1 + dTotalDebit : dEndlYearTotal1 = dEndlYearTotal1 + dTotalLDebit
                                        dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                                    End If

                                Next
                            End If
                        End If

                        'If q1 = k Then
                        '    dRow = dt.NewRow()
                        '    dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                        '    If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                        '        dRow("PresentYear") = dTotalCredit - dTotalDebit
                        '    ElseIf (dTotalDebit <> 0) Then
                        '        dRow("PresentYear") = dTotalDebit
                        '    ElseIf (dTotalCredit <> 0) Then
                        '        dRow("PresentYear") = dTotalCredit
                        '    ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                        '        dRow("PresentYear") = "0.00"
                        '    End If

                        '    If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                        '        dRow("LastYear") = dTotalLCredit - dTotalLDebit
                        '    ElseIf (dTotalLDebit <> 0) Then
                        '        dRow("LastYear") = dTotalLDebit
                        '    ElseIf (dTotalLCredit <> 0) Then
                        '        dRow("LastYear") = dTotalLCredit
                        '    ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                        '        dRow("LastYear") = "0.00"
                        '    End If

                        '    dt.Rows.Add(dRow)

                        '    dRow = dt.NewRow()
                        '    dRow("Particulars") = ""
                        '    dt.Rows.Add(dRow)

                        '    dEndYearTotal = dEndYearTotal1 : dEndlYearTotal = dEndlYearTotal1
                        '    dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                        'End If
                    Next


                End If
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Inventories at the beginning of the year:" & "</B>"
                dt.Rows.Add(dRow)
                sArray = dtGroup.Rows(0)("SLM_GLLedger").ToString().Split(",")
                If sArray.Length > 0 Then
                    For k = 0 To sArray.Length - 1
                        q1 = sArray.Length - 1
                        If sArray(k) <> "" Then
                            aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
                            dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                            If dtSub.Rows.Count > 0 Then
                                For v = 0 To dtSub.Rows.Count - 1
                                    If dtSub.Rows(0)("gl_id") <> 198 Then
                                        dRow = dt.NewRow()
                                        dRow("Particulars") = "<B>" & dtSub.Rows(0)("gl_Desc") & "</B>"
                                        dt.Rows.Add(dRow)
                                        sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  and cc_custId=" & iCustID & " order by cc_gl"
                                        dtSub2 = objDBL.SQLExecuteDataTable(sAC, sglSql)
                                        If dtSub2.Rows.Count > 0 Then
                                            For g = 0 To dtSub2.Rows.Count - 1
                                                dRow = dt.NewRow()
                                                If IsDBNull(dtSub2.Rows(g)("cc_gl").ToString()) = False Then
                                                    dRow("ID") = dtSub2.Rows(g)("cc_gl")
                                                End If

                                                If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                    dRow("Particulars") = dtSub2.Rows(g)("cc_GLDesc")
                                                End If
                                                dt.Rows.Add(dRow)
                                                dDebit = 0 : dCredit = 0
                                                'Current Year
                                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                                If iCount <> 0 Then
                                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And "
                                                    iSql = iSql & "CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"

                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                                dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    dDebit = 0
                                                    ' dTotalDebit = 0
                                                End If



                                                'Last year
                                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                                If iCount <> 0 Then
                                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " And ci_CompID =" & iACID & " And "
                                                    iSql = iSql & "CI_SubGLID =" & dtSub2.Rows(g)("cc_gl") & " and CI_Status<>'D'"
                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                                dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                                dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    dDebit = 0
                                                    ' dTotalDebit = 0
                                                End If

                                                If dDebit <> 0 And dCredit <> 0 Then
                                                    dRow("PresentYear") = dDebit - dCredit
                                                ElseIf (dDebit <> 0) And dCredit = 0 Then
                                                    dRow("PresentYear") = dDebit
                                                ElseIf (dCredit <> 0) And dDebit = 0 Then
                                                    dRow("PresentYear") = dCredit
                                                Else dDebit = 0 And (dCredit = 0)
                                                    dRow("PresentYear") = "0.00"
                                                End If
                                                If dRow("PresentYear").StartsWith("-") = True Then
                                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                                End If

                                                If dLDebit <> 0 And dLCredit <> 0 Then
                                                    dRow("LastYear") = dLDebit - dLCredit
                                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                                    dRow("LastYear") = dLDebit
                                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                                    dRow("LastYear") = dLCredit
                                                Else dLDebit = 0 And (dLCredit = 0)
                                                    dRow("LastYear") = "0.00"
                                                End If
                                                If dRow("LastYear").StartsWith("-") = True Then
                                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                                End If

                                                ' dt.Rows.Add(dRow)
                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                            Next
                                        End If
                                        dRow = dt.NewRow()
                                        dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                                        If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                            dRow("PresentYear") = dTotalCredit - dTotalDebit
                                        ElseIf (dTotalDebit <> 0) Then
                                            dRow("PresentYear") = dTotalDebit
                                        ElseIf (dTotalCredit <> 0) Then
                                            dRow("PresentYear") = dTotalCredit
                                        ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                            dRow("PresentYear") = "0.00"
                                        End If

                                        If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                            dRow("LastYear") = dTotalLCredit - dTotalLDebit
                                        ElseIf (dTotalLDebit <> 0) Then
                                            dRow("LastYear") = dTotalLDebit
                                        ElseIf (dTotalLCredit <> 0) Then
                                            dRow("LastYear") = dTotalLCredit
                                        ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                            dRow("LastYear") = "0.00"
                                        End If

                                        dt.Rows.Add(dRow)

                                        dRow = dt.NewRow()
                                        dRow("Particulars") = ""
                                        dt.Rows.Add(dRow)
                                        dbeginYearTotal1 = dbeginYearTotal1 + dTotalDebit : dbeginlYearTotal1 = dbeginlYearTotal1 + dTotalLDebit
                                        dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                                    End If
                                Next
                            End If
                        End If

                        'If q1 = k Then
                        '    dRow = dt.NewRow()
                        '    dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                        '    If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                        '        dRow("PresentYear") = dTotalCredit - dTotalDebit
                        '    ElseIf (dTotalDebit <> 0) Then
                        '        dRow("PresentYear") = dTotalDebit
                        '    ElseIf (dTotalCredit <> 0) Then
                        '        dRow("PresentYear") = dTotalCredit
                        '    ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                        '        dRow("PresentYear") = "0.00"
                        '    End If

                        '    If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                        '        dRow("LastYear") = dTotalLCredit - dTotalLDebit
                        '    ElseIf (dTotalLDebit <> 0) Then
                        '        dRow("LastYear") = dTotalLDebit
                        '    ElseIf (dTotalLCredit <> 0) Then
                        '        dRow("LastYear") = dTotalLCredit
                        '    ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                        '        dRow("LastYear") = "0.00"
                        '    End If

                        '    dt.Rows.Add(dRow)

                        '    dRow = dt.NewRow()
                        '    dRow("Particulars") = ""
                        '    dt.Rows.Add(dRow)

                        '    dbeginYearTotal = dbeginYearTotal1 : dbeginlYearTotal = dbeginlYearTotal1
                        '    dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                        'End If
                    Next
                End If
                'dRow = dt.NewRow()
                'dRow("Particulars") = "<B>" & "Total  Net (increase) / decrease" & "</B>"

                'If (dbeginYearTotal >= 0) And (dEndYearTotal >= 0) Then
                '    dRow("PresentYear") = dbeginYearTotal - dEndYearTotal
                'ElseIf (dbeginYearTotal = 0) And (dEndYearTotal = 0) Then
                '    dRow("PresentYear") = "0.00"
                'End If

                'If (dbeginlYearTotal <> 0) And (dEndlYearTotal >= 0) Then
                '    dRow("LastYear") = dbeginlYearTotal - dEndlYearTotal
                'ElseIf (dbeginlYearTotal = 0) And (dEndlYearTotal = 0) Then
                '    dRow("LastYear") = "0.00"
                'End If

                'dt.Rows.Add(dRow)

                'dRow = dt.NewRow()
                'dRow("Particulars") = ""
                'dt.Rows.Add(dRow)
                'dbeginYearTotal = 0 : dbeginlYearTotal = 0 : dEndlYearTotal = 0 : dEndYearTotal = 0
                'dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Schedule Report Notes
    'Public Function LoadScheduleReportNotes(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iUserid As Integer, ByVal sIpAddress As String)
    '    Dim sSql As String = "", iSql As String = "", aSql As String = "", ciSql As String = "", cilSql As String = ""
    '    Dim dt As New DataTable, dtGroup As New DataTable, dtArray As New DataTable, dtSub As New DataTable
    '    Dim dtYear As New DataTable, dtOB As New DataTable
    '    Dim iLastYear As Integer = 0
    '    Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0
    '    Dim iStatusCheck As Integer = 0
    '    Dim dRow As DataRow
    '    Dim sArray As Array
    '    Dim dDebit As Double = 0.00, dCredit As Double = 0.00
    '    Dim dLDebit As Double = 0.00, dLCredit As Double = 0.00
    '    Dim dTotalDebit As Double = 0.00, dTotalCredit As Double = 0.00
    '    Dim dTotalLDebit As Double = 0.00, dTotalLCredit As Double = 0.00
    '    Dim dbeginYearTotal As Double = 0.00, dbeginlYearTotal As Double = 0.00
    '    Dim dEndYearTotal As Double = 0.00, dEndlYearTotal As Double = 0.00
    '    Dim q As Integer
    '    Dim sColumnd As String, sColumnc As String
    '    Dim sColumndATDDEb As String, sColumncATDCre As String
    '    Dim iAllCount As Integer = 0

    '    Dim iCont As Integer = 0
    '    Try
    '        dt.Columns.Add("Particulars")
    '        dt.Columns.Add("PresentYear")
    '        dt.Columns.Add("LastYear")

    '        sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
    '        sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iACID & ")"
    '        dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtYear.Rows.Count > 0 Then
    '            'dr.Read()
    '            iLastYear = dtYear.Rows(i).Item("YMS_ID")
    '        Else
    '            iLastYear = 0
    '        End If

    '        'sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_YEARID=" & iYearID & " and YMS_CompID=" & iACID & "" 'Commented vijayalakshmi 12/12/2019 this query fetching the current year
    '        'dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        'If dtYear.Rows.Count > 0 Then
    '        '    For i = 0 To dtYear.Rows.Count - 1
    '        '        iLastYear = dtYear.Rows(i).Item("YMS_YEARID")
    '        '    Next
    '        'Else
    '        '    iLastYear = 0
    '        'End If

    '        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
    '        dtOB = objDBL.SQLExecuteDataTable(sAC, iSql)
    '        If dtOB.Rows.Count > 0 Then
    '            For j = 0 To dtOB.Rows.Count - 1
    '                If dtOB.Rows(j).Item("Opn_Status") = "F" Then
    '                    iStatusCheck = 0
    '                Else
    '                    iStatusCheck = 1
    '                End If
    '            Next
    '        Else
    '            iStatusCheck = 1
    '        End If

    '        sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_CompID =" & iACID & " and SLM_NoteNo <> 0 And SLM_OrgID=" & iOrgID & " And SLM_CustID=" & iCustID & " order by SLM_NoteNo"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then
    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                dRow("Particulars") = "<B>" & "NOTE NO: " & dtGroup.Rows(i)("SLM_NoteNo") & " - " & objDBL.SQLExecuteScalar(sAC, "Select gl_desc from Chart_of_Accounts where gl_id = " & dtGroup.Rows(i)("SLM_SubGroupID") & "") & "</B>"
    '                dt.Rows.Add(dRow)


    '                sArray = dtGroup.Rows(i)("SLM_GLLedger").ToString().Split(",")
    '                If sArray.Length > 0 Then
    '                    For k = 0 To sArray.Length - 1
    '                        If sArray(k) <> "" Then
    '                            aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
    '                            dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                            If dtSub.Rows.Count > 0 Then
    '                                dRow = dt.NewRow()
    '                                dRow("Particulars") = dtSub.Rows(0)("gl_glCode") & " - " & dtSub.Rows(0)("gl_Desc")


    '                                'Current Year
    '                                If iStatusCheck = 0 Then
    '                                    If dtGroup.Rows(i)("SLM_NoteNo") = 21 Then
    '                                        iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
    '                                        iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D'"
    '                                        sColumnd = "CI_CBValues"
    '                                        sColumnc = ""
    '                                    Else
    '                                        If dtSub.Rows(0)("gl_Desc") = "Opening Stock : Raw Material" Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " And Opn_CompID =" & iACID & " And "
    '                                            iSql = iSql & "Opn_GLID =198  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            sColumnd = "Opn_DebitAmt"
    '                                            sColumnc = "Opn_CreditAmount"
    '                                        ElseIf dtSub.Rows(0)("gl_Desc") = "Closing Stock : Raw Material" Then
    '                                            iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
    '                                            iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
    '                                            sColumnd = "CI_CBValues"
    '                                            sColumnc = ""
    '                                        Else
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " And Opn_CompID =" & iACID & " And "
    '                                            iSql = iSql & "Opn_GLID =" & sArray(k) & "  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & " "
    '                                            sColumnd = "Opn_DebitAmt"
    '                                            sColumnc = "Opn_CreditAmount"
    '                                        End If
    '                                    End If
    '                                Else

    '                                    If dtGroup.Rows(i)("SLM_NoteNo") = 21 Then
    '                                        iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
    '                                        iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D'"
    '                                        sColumnd = "CI_CBValues"
    '                                        sColumnc = ""
    '                                    Else
    '                                        If dtSub.Rows(0)("gl_Desc") = "Opening Stock : Raw Material" Then
    '                                            iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details b where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
    '                                            iSql = iSql & "ATD_GL =198  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & ""
    '                                            iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
    '                                            If iAllCount > 0 Then
    '                                                iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iYearID & " And a.CC_CompID =" & iACID & " And "
    '                                                iSql = iSql & "a.CC_Parent =198  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & ""
    '                                                sColumnd = "CC_CloseDebit"
    '                                                sColumnc = "CC_CloseCredit"
    '                                                sColumndATDDEb = "ATD_Debit"
    '                                                sColumncATDCre = "ATD_Credit"
    '                                            Else
    '                                                iSql = "" : iSql = "Select * from customer_coa where CC_YearID =" & iYearID & " And CC_CompID =" & iACID & " And "
    '                                                iSql = iSql & "CC_Parent =198  And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
    '                                                sColumnd = "CC_CloseDebit"
    '                                                sColumnc = "CC_CloseCredit"
    '                                            End If

    '                                        ElseIf dtSub.Rows(0)("gl_Desc") = "Closing Stock : Raw Material" Then
    '                                            iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
    '                                            iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
    '                                            sColumnd = "CI_CBValues"
    '                                            sColumnc = ""
    '                                        Else
    '                                            iSql = "" : iSql = "Select count(Atd_id) from  acc_transactions_details b where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
    '                                            iSql = iSql & "ATD_GL =" & sArray(k) & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & ""
    '                                            iAllCount = objDBL.SQLExecuteScalar(sAC, iSql)
    '                                            If iAllCount > 0 Then
    '                                                iSql = "" : iSql = "Select * from customer_coa a join acc_transactions_details b on a.cc_gl=b.ATD_subgl where a.CC_YearID =" & iYearID & " And a.CC_CompID =" & iACID & " And "
    '                                                iSql = iSql & "a.CC_Parent =" & sArray(k) & "  And a.CC_CustID=" & iCustID & " And a.CC_IndType=" & iOrgID & ""
    '                                                sColumnd = "CC_CloseDebit"
    '                                                sColumnc = "CC_CloseCredit"
    '                                                sColumndATDDEb = "ATD_Debit"
    '                                                sColumncATDCre = "ATD_Credit"
    '                                            Else
    '                                                iSql = "" : iSql = "Select * from customer_coa where CC_YearID =" & iYearID & " And CC_CompID =" & iACID & " And "
    '                                                iSql = iSql & "CC_Parent =" & sArray(k) & "  And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
    '                                                sColumnd = "CC_CloseDebit"
    '                                                sColumnc = "CC_CloseCredit"
    '                                            End If
    '                                        End If

    '                                        'If dtSub.Rows(0)("gl_Desc") = "Opening Stock : Raw Material" Then
    '                                        '    iSql = "" : iSql = "Select * from acc_transactions_details where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
    '                                        '    iSql = iSql & "ATD_GL =198  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & ""
    '                                        '    sColumndATDDEb = "ATD_Debit"
    '                                        '    sColumncATDCre = "ATD_Credit"
    '                                        'ElseIf dtSub.Rows(0)("gl_Desc") = "Closing Stock : Raw Material" Then
    '                                        '    iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
    '                                        '    iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
    '                                        '    sColumnd = "CI_CBValues"
    '                                        '    sColumnc = ""
    '                                        'Else
    '                                        '    iSql = "" : iSql = "Select * from acc_transactions_details where ATD_YearID =" & iYearID & " And ATD_CompID =" & iACID & " And "
    '                                        '    iSql = iSql & "ATD_GL =" & sArray(k) & "  And ATD_CustID=" & iCustID & " And ATD_OrgType=" & iOrgID & " "
    '                                        '    sColumndATDDEb = "ATD_Debit"
    '                                        '    sColumncATDCre = "ATD_Credit"
    '                                        'End If
    '                                    End If
    '                                End If


    '                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                If dtArray.Rows.Count > 0 Then
    '                                    For a = 0 To dtArray.Rows.Count - 1
    '                                        If iStatusCheck = 0 Then
    '                                            If sColumnc <> "" Then
    '                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
    '                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                End If
    '                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
    '                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
    '                                                    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
    '                                                End If
    '                                            Else
    '                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
    '                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                End If
    '                                            End If
    '                                            If sArray(k) = 60 Then
    '                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " And SS_CompID =" & iACID & " And "
    '                                                iSql = iSql & "SS_Group = 60  And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " "
    '                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
    '                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
    '                                                    End If
    '                                                    Try
    '                                                        iSql = "" : iSql = "select count(SS_PKID) from Acc_Seperate_Schedule where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
    '                                                        iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
    '                                                        iCont = objDBL.SQLExecuteScalar(sAC, iSql)
    '                                                        If iCont = 0 Then
    '                                                            Dim iMaxid As Integer = objDBL.SQLExecuteScalar(sAC, "select IsNull(Max(SS_PKID) + 1,1) from Acc_Seperate_Schedule")
    '                                                            iSql = "" : iSql = "insert into Acc_Seperate_Schedule(SS_PKID,SS_FinancialYear,SS_CustId,SS_Orgtype,SS_Group,SS_Particulars,"
    '                                                            iSql = iSql & "SS_Values,SS_DATE,SS_Status,SS_Delflag,SS_CrBy,SS_CrOn,SS_UpdatedBy,SS_UpdatedOn,SS_IPAddress,SS_CompID)"
    '                                                            iSql = iSql & "values(" & iMaxid & "," & iYearID & "," & iCustID & "," & iOrgID & ",11,'Reserves and surplus',"
    '                                                            iSql = iSql & "" & dTotalCredit & ",getdate(),'D','A'," & iUserid & ",getdate()," & iUserid & ",getdate(),'" & sIpAddress & "'," & iACID & ")"
    '                                                            objDBL.SQLExecuteNonQuery(sAC, iSql)
    '                                                        Else
    '                                                            iSql = "" : iSql = "update Acc_Seperate_Schedule set SS_Values=" & dTotalCredit & " where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
    '                                                            iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & " and SS_Compid=" & iACID & ""
    '                                                            objDBL.SQLExecuteNonQuery(sAC, iSql)
    '                                                        End If

    '                                                    Catch ex As Exception
    '                                                        Throw
    '                                                    End Try
    '                                                End If
    '                                            End If
    '                                        Else
    '                                            If sColumnc <> "" Then
    '                                                If iAllCount > 0 Then
    '                                                    If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumndATDDEb).ToString()) = False) And (dtArray.Rows(a)(sColumndATDDEb).ToString() <> "") Then
    '                                                        If a = 0 Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
    '                                                            dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
    '                                                        Else
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
    '                                                            dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumndATDDEb).ToString())
    '                                                        End If

    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") And (IsDBNull(dtArray.Rows(a)(sColumncATDCre).ToString()) = False) And (dtArray.Rows(a)(sColumncATDCre).ToString() <> "") Then
    '                                                        If a = 0 Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
    '                                                            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString()) + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
    '                                                        Else
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
    '                                                            dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumncATDCre).ToString())
    '                                                        End If
    '                                                    End If
    '                                                Else
    '                                                    If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                        dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                    End If
    '                                                    If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
    '                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
    '                                                    End If
    '                                                End If
    '                                            Else
    '                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
    '                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                End If
    '                                            End If
    '                                            If sArray(k) = 60 Then
    '                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " And SS_CompID =" & iACID & " And "
    '                                                iSql = iSql & "SS_Group = 60  And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " "
    '                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
    '                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
    '                                                    End If
    '                                                    Try
    '                                                        iSql = "" : iSql = "select count(SS_PKID) from Acc_Seperate_Schedule where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
    '                                                        iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
    '                                                        iCont = objDBL.SQLExecuteScalar(sAC, iSql)
    '                                                        If iCont = 0 Then
    '                                                            Dim iMaxid As Integer = objDBL.SQLExecuteScalar(sAC, "select IsNull(Max(SS_PKID) + 1,1) from Acc_Seperate_Schedule")
    '                                                            iSql = "" : iSql = "insert into Acc_Seperate_Schedule(SS_PKID,SS_FinancialYear,SS_CustId,SS_Orgtype,SS_Group,SS_Particulars,"
    '                                                            iSql = iSql & "SS_Values,SS_DATE,SS_Status,SS_Delflag,SS_CrBy,SS_CrOn,SS_UpdatedBy,SS_UpdatedOn,SS_IPAddress,SS_CompID)"
    '                                                            iSql = iSql & "values(" & iMaxid & "," & iYearID & "," & iCustID & "," & iOrgID & ",11,'Reserves and surplus',"
    '                                                            iSql = iSql & "" & dTotalCredit & ",getdate(),'D','A'," & iUserid & ",getdate()," & iUserid & ",getdate(),'" & sIpAddress & "'," & iACID & ")"
    '                                                            objDBL.SQLExecuteNonQuery(sAC, iSql)
    '                                                        Else
    '                                                            iSql = "" : iSql = "update Acc_Seperate_Schedule set SS_Values=" & dTotalCredit & " where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
    '                                                            iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & " and SS_Compid=" & iACID & ""
    '                                                            objDBL.SQLExecuteNonQuery(sAC, iSql)
    '                                                        End If

    '                                                    Catch ex As Exception
    '                                                        Throw
    '                                                    End Try
    '                                                End If
    '                                            End If
    '                                        End If
    '                                    Next
    '                                End If


    '                                'Last year
    '                                If dtGroup.Rows(i)("SLM_NoteNo") = 21 Then
    '                                    iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iACID & " And "
    '                                    iSql = iSql & "CI_GLID =" & sArray(k) & "  and CI_status<>'D'"
    '                                    sColumnd = "CI_CBValues"
    '                                    sColumnc = ""
    '                                Else
    '                                    If dtSub.Rows(0)("gl_Desc") = "Opening Stock : Raw Material" Then
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iACID & " And "
    '                                        iSql = iSql & "Opn_GLID =198 And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        sColumnd = "Opn_DebitAmt"
    '                                        sColumnc = "Opn_CreditAmount"
    '                                    ElseIf dtSub.Rows(0)("gl_Desc") = "Closing Stock : Raw Material" Then
    '                                        iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iACID & " And "
    '                                        iSql = iSql & "CI_GLID =198 And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D'"
    '                                        sColumnd = "CI_CBValues"
    '                                        sColumnc = ""
    '                                    Else
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iACID & " And "
    '                                        iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        sColumnd = "Opn_DebitAmt"
    '                                        sColumnc = "Opn_CreditAmount"
    '                                    End If
    '                                End If


    '                                'iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iACID & " And "
    '                                'iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
    '                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                If dtArray.Rows.Count > 0 Then
    '                                    For a = 0 To dtArray.Rows.Count - 1
    '                                        If sColumnc <> "" Then
    '                                            If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
    '                                                dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                            End If
    '                                            If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
    '                                                dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
    '                                                dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
    '                                            End If
    '                                            If sArray(k) = 60 Then
    '                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " And SS_CompID =" & iACID & " And "
    '                                                iSql = iSql & "SS_Group =11  And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " "
    '                                                If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
    '                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
    '                                                    dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
    '                                                End If

    '                                            End If
    '                                        Else
    '                                            If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
    '                                                dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                                dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
    '                                            End If
    '                                        End If
    '                                    Next
    '                                End If

    '                                If dDebit <> 0 And dCredit <> 0 Then
    '                                    dRow("PresentYear") = dDebit - dCredit
    '                                ElseIf (dDebit <> 0) And dCredit = 0 Then
    '                                    dRow("PresentYear") = dDebit
    '                                ElseIf (dCredit <> 0) And dDebit = 0 Then
    '                                    dRow("PresentYear") = dCredit
    '                                Else dDebit = 0 And (dCredit = 0)
    '                                    dRow("PresentYear") = "0.00"
    '                                End If
    '                                If dRow("PresentYear").StartsWith("-") = True Then
    '                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
    '                                End If

    '                                If dLDebit <> 0 And dLCredit <> 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
    '                                    dRow("LastYear") = dLCredit
    '                                Else dLDebit = 0 And (dLCredit = 0)
    '                                    dRow("LastYear") = "0.00"
    '                                End If
    '                                If dRow("LastYear").StartsWith("-") = True Then
    '                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
    '                                End If

    '                                dt.Rows.Add(dRow)
    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                    Next

    '                    dRow = dt.NewRow()
    '                    dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

    '                    If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
    '                        dRow("PresentYear") = dTotalCredit - dTotalDebit
    '                    ElseIf (dTotalDebit <> 0) Then
    '                        dRow("PresentYear") = dTotalDebit
    '                    ElseIf (dTotalCredit <> 0) Then
    '                        dRow("PresentYear") = dTotalCredit
    '                    ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
    '                        dRow("PresentYear") = "0.00"
    '                    End If

    '                    If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
    '                        dRow("LastYear") = dTotalLCredit - dTotalLDebit
    '                    ElseIf (dTotalLDebit <> 0) Then
    '                        dRow("LastYear") = dTotalLDebit
    '                    ElseIf (dTotalLCredit <> 0) Then
    '                        dRow("LastYear") = dTotalLCredit
    '                    ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
    '                        dRow("LastYear") = "0.00"
    '                    End If

    '                    dt.Rows.Add(dRow)

    '                    dRow = dt.NewRow()
    '                    dRow("Particulars") = ""
    '                    dt.Rows.Add(dRow)
    '                    dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '                End If
    '            Next
    '        End If

    '        Dim schSql As String
    '        Dim iCount As Integer

    '        schSql = "Select * from Schedule_Linkage_Master where SLM_CompID =" & iACID & " and SLM_NoteNo <> 0 And SLM_OrgID=" & iOrgID & " and SLM_CustID=" & iCustID & " and SLM_NoteNo=21"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, schSql)
    '        If dtGroup.Rows.Count > 0 Then
    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "Changes in inventories of finished goods, work-in-progress and stock-in-trade " & "</B>"
    '            dt.Rows.Add(dRow)
    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "Inventories at the end of the year" & "</B>"
    '            dt.Rows.Add(dRow)
    '            sArray = dtGroup.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '            If sArray.Length > 0 Then
    '                For k = 0 To sArray.Length - 1
    '                    q = sArray.Length - 1
    '                    If sArray(k) <> "" Then
    '                        aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
    '                        dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                        If dtSub.Rows.Count > 0 Then
    '                            If dtSub.Rows(0)("gl_id") <> 198 Then
    '                                dRow = dt.NewRow()
    '                                dRow("Particulars") = dtSub.Rows(0)("gl_glCode") & " - " & dtSub.Rows(0)("gl_Desc")

    '                                'Current Year
    '                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D'"
    '                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
    '                                If iCount <> 0 Then
    '                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And "
    '                                    iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D'"

    '                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                    If dtArray.Rows.Count > 0 Then
    '                                        For a = 0 To dtArray.Rows.Count - 1
    '                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
    '                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
    '                                                dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
    '                                            End If
    '                                        Next
    '                                    End If
    '                                Else
    '                                    dDebit = 0
    '                                    ' dTotalDebit = 0
    '                                End If



    '                                'Last year
    '                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D'"
    '                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
    '                                If iCount <> 0 Then
    '                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " And ci_CompID =" & iACID & " And "
    '                                    iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D' "
    '                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                    If dtArray.Rows.Count > 0 Then
    '                                        For a = 0 To dtArray.Rows.Count - 1
    '                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
    '                                                dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
    '                                                dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
    '                                            End If
    '                                        Next
    '                                    End If
    '                                Else
    '                                    dDebit = 0
    '                                    ' dTotalDebit = 0
    '                                End If

    '                                If dDebit <> 0 And dCredit <> 0 Then
    '                                    dRow("PresentYear") = dDebit - dCredit
    '                                ElseIf (dDebit <> 0) And dCredit = 0 Then
    '                                    dRow("PresentYear") = dDebit
    '                                ElseIf (dCredit <> 0) And dDebit = 0 Then
    '                                    dRow("PresentYear") = dCredit
    '                                Else dDebit = 0 And (dCredit = 0)
    '                                    dRow("PresentYear") = "0.00"
    '                                End If
    '                                If dRow("PresentYear").StartsWith("-") = True Then
    '                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
    '                                End If

    '                                If dLDebit <> 0 And dLCredit <> 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
    '                                    dRow("LastYear") = dLCredit
    '                                Else dLDebit = 0 And (dLCredit = 0)
    '                                    dRow("LastYear") = "0.00"
    '                                End If
    '                                If dRow("LastYear").StartsWith("-") = True Then
    '                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
    '                                End If

    '                                dt.Rows.Add(dRow)
    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If

    '                        End If
    '                    End If

    '                    If q = k Then
    '                        dRow = dt.NewRow()
    '                        dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

    '                        If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
    '                            dRow("PresentYear") = dTotalCredit - dTotalDebit
    '                        ElseIf (dTotalDebit <> 0) Then
    '                            dRow("PresentYear") = dTotalDebit
    '                        ElseIf (dTotalCredit <> 0) Then
    '                            dRow("PresentYear") = dTotalCredit
    '                        ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
    '                            dRow("PresentYear") = "0.00"
    '                        End If

    '                        If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
    '                            dRow("LastYear") = dTotalLCredit - dTotalLDebit
    '                        ElseIf (dTotalLDebit <> 0) Then
    '                            dRow("LastYear") = dTotalLDebit
    '                        ElseIf (dTotalLCredit <> 0) Then
    '                            dRow("LastYear") = dTotalLCredit
    '                        ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
    '                            dRow("LastYear") = "0.00"
    '                        End If

    '                        dt.Rows.Add(dRow)

    '                        dRow = dt.NewRow()
    '                        dRow("Particulars") = ""
    '                        dt.Rows.Add(dRow)

    '                        dEndYearTotal = dTotalDebit : dEndlYearTotal = dTotalLDebit
    '                        dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '                    End If
    '                Next


    '            End If
    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "Inventories at the beginning of the year:" & "</B>"
    '            dt.Rows.Add(dRow)
    '            sArray = dtGroup.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '            If sArray.Length > 0 Then
    '                For k = 0 To sArray.Length - 1
    '                    q = sArray.Length - 1
    '                    If sArray(k) <> "" Then
    '                        aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
    '                        dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                        If dtSub.Rows.Count > 0 Then
    '                            If dtSub.Rows(0)("gl_id") <> 198 Then
    '                                dRow = dt.NewRow()
    '                                dRow("Particulars") = dtSub.Rows(0)("gl_glCode") & " - " & dtSub.Rows(0)("gl_Desc")

    '                                'Current Year
    '                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D'"
    '                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
    '                                If iCount <> 0 Then
    '                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And "
    '                                    iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_Status<>'D'"

    '                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                    If dtArray.Rows.Count > 0 Then
    '                                        For a = 0 To dtArray.Rows.Count - 1
    '                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
    '                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
    '                                                dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
    '                                            End If
    '                                        Next
    '                                    End If
    '                                Else
    '                                    dDebit = 0
    '                                    ' dTotalDebit = 0
    '                                End If



    '                                'Last year
    '                                ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D'"
    '                                iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
    '                                If iCount <> 0 Then
    '                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " And ci_CompID =" & iACID & " And "
    '                                    iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_Status<>'D'"
    '                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                    If dtArray.Rows.Count > 0 Then
    '                                        For a = 0 To dtArray.Rows.Count - 1
    '                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
    '                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
    '                                                dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
    '                                            End If
    '                                        Next
    '                                    End If
    '                                Else
    '                                    dDebit = 0
    '                                    ' dTotalDebit = 0
    '                                End If

    '                                If dDebit <> 0 And dCredit <> 0 Then
    '                                    dRow("PresentYear") = dDebit - dCredit
    '                                ElseIf (dDebit <> 0) And dCredit = 0 Then
    '                                    dRow("PresentYear") = dDebit
    '                                ElseIf (dCredit <> 0) And dDebit = 0 Then
    '                                    dRow("PresentYear") = dCredit
    '                                Else dDebit = 0 And (dCredit = 0)
    '                                    dRow("PresentYear") = "0.00"
    '                                End If
    '                                If dRow("PresentYear").StartsWith("-") = True Then
    '                                    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
    '                                End If

    '                                If dLDebit <> 0 And dLCredit <> 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf (dLDebit <> 0) And dLCredit = 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf (dLCredit <> 0) And dLDebit = 0 Then
    '                                    dRow("LastYear") = dLCredit
    '                                Else dLDebit = 0 And (dLCredit = 0)
    '                                    dRow("LastYear") = "0.00"
    '                                End If
    '                                If dRow("LastYear").StartsWith("-") = True Then
    '                                    dRow("LastYear") = dRow("LastYear").Remove(0, 1)
    '                                End If

    '                                dt.Rows.Add(dRow)
    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                    End If

    '                    If q = k Then
    '                        dRow = dt.NewRow()
    '                        dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

    '                        If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
    '                            dRow("PresentYear") = dTotalCredit - dTotalDebit
    '                        ElseIf (dTotalDebit <> 0) Then
    '                            dRow("PresentYear") = dTotalDebit
    '                        ElseIf (dTotalCredit <> 0) Then
    '                            dRow("PresentYear") = dTotalCredit
    '                        ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
    '                            dRow("PresentYear") = "0.00"
    '                        End If

    '                        If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
    '                            dRow("LastYear") = dTotalLCredit - dTotalLDebit
    '                        ElseIf (dTotalLDebit <> 0) Then
    '                            dRow("LastYear") = dTotalLDebit
    '                        ElseIf (dTotalLCredit <> 0) Then
    '                            dRow("LastYear") = dTotalLCredit
    '                        ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
    '                            dRow("LastYear") = "0.00"
    '                        End If

    '                        dt.Rows.Add(dRow)

    '                        dRow = dt.NewRow()
    '                        dRow("Particulars") = ""
    '                        dt.Rows.Add(dRow)

    '                        dbeginYearTotal = dTotalDebit : dbeginlYearTotal = dTotalLDebit
    '                        dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '                    End If
    '                Next
    '            End If
    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "Total  Net (increase) / decrease" & "</B>"

    '            If (dbeginYearTotal >= 0) And (dEndYearTotal >= 0) Then
    '                dRow("PresentYear") = dbeginYearTotal - dEndYearTotal
    '            ElseIf (dbeginYearTotal = 0) And (dEndYearTotal = 0) Then
    '                dRow("PresentYear") = "0.00"
    '            End If

    '            If (dbeginlYearTotal <> 0) And (dEndlYearTotal >= 0) Then
    '                dRow("LastYear") = dbeginlYearTotal - dEndlYearTotal
    '            ElseIf (dbeginlYearTotal = 0) And (dEndlYearTotal = 0) Then
    '                dRow("LastYear") = "0.00"
    '            End If

    '            dt.Rows.Add(dRow)

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = ""
    '            dt.Rows.Add(dRow)
    '            dbeginYearTotal = 0 : dbeginlYearTotal = 0 : dEndlYearTotal = 0 : dEndYearTotal = 0
    '            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '        End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadScheduleReportNotes(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iUserid As Integer, ByVal sIpAddress As String)
        Dim sSql As String = "", iSql As String = "", aSql As String = "", ciSql As String = "", cilSql As String = ""
        Dim dt As New DataTable, dtGroup As New DataTable, dtArray As New DataTable, dtSub As New DataTable
        Dim dtYear As New DataTable, dtOB As New DataTable
        Dim iLastYear As Integer = 0
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0
        Dim iStatusCheck As Integer = 0
        Dim dRow As DataRow
        Dim sArray As Array
        Dim dDebit As Double = 0.00, dCredit As Double = 0.00
        Dim dLDebit As Double = 0.00, dLCredit As Double = 0.00
        Dim dTotalDebit As Double = 0.00, dTotalCredit As Double = 0.00
        Dim dTotalLDebit As Double = 0.00, dTotalLCredit As Double = 0.00
        Dim dbeginYearTotal As Double = 0.00, dbeginlYearTotal As Double = 0.00
        Dim dEndYearTotal As Double = 0.00, dEndlYearTotal As Double = 0.00
        Dim q As Integer
        Dim sColumnd As String, sColumnc As String

        Dim iCont As Integer = 0
        Try
            dt.Columns.Add("Particulars")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iACID & ")"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtYear.Rows.Count > 0 Then
                'dr.Read()
                iLastYear = dtYear.Rows(i).Item("YMS_ID")
            Else
                iLastYear = 0
            End If

            'sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_YEARID=" & iYearID & " and YMS_CompID=" & iACID & "" 'Commented vijayalakshmi 12/12/2019 this query fetching the current year
            'dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            'If dtYear.Rows.Count > 0 Then
            '    For i = 0 To dtYear.Rows.Count - 1
            '        iLastYear = dtYear.Rows(i).Item("YMS_YEARID")
            '    Next
            'Else
            '    iLastYear = 0
            'End If

            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
            dtOB = objDBL.SQLExecuteDataTable(sAC, iSql)
            If dtOB.Rows.Count > 0 Then
                For j = 0 To dtOB.Rows.Count - 1
                    If dtOB.Rows(j).Item("Opn_Status") = "F" Then
                        iStatusCheck = 0
                    Else
                        iStatusCheck = 1
                    End If
                Next
            Else
                iStatusCheck = 1
            End If

            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_CompID =" & iACID & " and SLM_NoteNo <> 0 And SLM_OrgID=" & iOrgID & " And SLM_CustID=" & iCustID & " order by SLM_NoteNo"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then
                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("Particulars") = "<B>" & "NOTE NO: " & dtGroup.Rows(i)("SLM_NoteNo") & " - " & objDBL.SQLExecuteScalar(sAC, "Select gl_desc from Chart_of_Accounts where gl_id = " & dtGroup.Rows(i)("SLM_SubGroupID") & "") & "</B>"
                    dt.Rows.Add(dRow)


                    sArray = dtGroup.Rows(i)("SLM_GLLedger").ToString().Split(",")
                    If sArray.Length > 0 Then
                        For k = 0 To sArray.Length - 1
                            If sArray(k) <> "" Then
                                aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
                                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                                If dtSub.Rows.Count > 0 Then
                                    dRow = dt.NewRow()
                                    dRow("Particulars") = dtSub.Rows(0)("gl_Desc") 'dtSub.Rows(0)("gl_glCode") & " - " & vijaylakshmi 20-01-2020 removed glcode they dnt need gl code in report


                                    'Current Year
                                    If iStatusCheck = 0 Then
                                        If dtGroup.Rows(i)("SLM_NoteNo") = 21 Then
                                            iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
                                            iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D' and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                            sColumnd = "CI_CBValues"
                                            sColumnc = ""
                                        Else
                                            If dtSub.Rows(0)("gl_Desc") = "Opening Stock : Raw Material" Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " And Opn_CompID =" & iACID & " And "
                                                iSql = iSql & "Opn_GLID =198  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                sColumnd = "Opn_DebitAmt"
                                                sColumnc = "Opn_CreditAmount"
                                            ElseIf dtSub.Rows(0)("gl_Desc") = "Closing Stock : Raw Material" Then
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                sColumnd = "CI_CBValues"
                                                sColumnc = ""
                                            Else
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " And Opn_CompID =" & iACID & " And "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & "  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & " "
                                                sColumnd = "Opn_DebitAmt"
                                                sColumnc = "Opn_CreditAmount"
                                            End If
                                        End If
                                    Else
                                        If dtGroup.Rows(i)("SLM_NoteNo") = 21 Then
                                            iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
                                            iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D' and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                            sColumnd = "CI_CBValues"
                                            sColumnc = ""
                                        Else
                                            If dtSub.Rows(0)("gl_Desc") = "Opening Stock : Raw Material" Then
                                                iSql = "" : iSql = "Select * from Customer_coa where CC_YearID =" & iYearID & " And CC_CompID =" & iACID & " And "
                                                iSql = iSql & "CC_Parent =198  And CC_CustId=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                sColumnd = "CC_CloseDebit"
                                                sColumnc = "CC_CloseCredit"
                                            ElseIf dtSub.Rows(0)("gl_Desc") = "Closing Stock : Raw Material" Then
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iACID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                sColumnd = "CI_CBValues"
                                                sColumnc = ""
                                            Else
                                                iSql = "" : iSql = "Select * from Customer_coa where CC_YearID =" & iYearID & " And CC_CompID =" & iACID & " And "
                                                iSql = iSql & "CC_Parent =" & sArray(k) & "  And CC_CustId=" & iCustID & " And CC_IndType=" & iOrgID & " "
                                                sColumnd = "CC_CloseDebit"
                                                sColumnc = "CC_CloseCredit"
                                            End If
                                        End If
                                    End If


                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If sColumnc <> "" Then
                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                End If
                                                If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                End If
                                                If sArray(k) = 60 Then
                                                    iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " And SS_CompID =" & iACID & " And "
                                                    iSql = iSql & "SS_Group = 60  And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " "
                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                    If dtArray.Rows.Count > 0 Then
                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                            If dCredit > 0 Then
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                                dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                            End If

                                                        End If
                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then

                                                            If dDebit > 0 Then
                                                                dDebit = dDebit - Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                                dTotalDebit = dTotalDebit - Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                            End If
                                                        End If
                                                        Dim dtotrs As Double = "0.00"
                                                        If dCredit > 0 Then
                                                            dtotrs = dTotalCredit
                                                        Else
                                                            dtotrs = dTotalDebit
                                                        End If
                                                        Try
                                                            iSql = "" : iSql = "select count(SS_PKID) from Acc_Seperate_Schedule where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
                                                            iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
                                                            iCont = objDBL.SQLExecuteScalar(sAC, iSql)
                                                            If iCont = 0 Then
                                                                Dim iMaxid As Integer = objDBL.SQLExecuteScalar(sAC, "select IsNull(Max(SS_PKID) + 1,1) from Acc_Seperate_Schedule")
                                                                iSql = "" : iSql = "insert into Acc_Seperate_Schedule(SS_PKID,SS_FinancialYear,SS_CustId,SS_Orgtype,SS_Group,SS_Particulars,"
                                                                iSql = iSql & "SS_Values,SS_DATE,SS_Status,SS_Delflag,SS_CrBy,SS_CrOn,SS_UpdatedBy,SS_UpdatedOn,SS_IPAddress,SS_CompID)"
                                                                iSql = iSql & "values(" & iMaxid & "," & iYearID & "," & iCustID & "," & iOrgID & ",11,'Reserves and surplus',"
                                                                iSql = iSql & "" & dtotrs & ",getdate(),'D','A'," & iUserid & ",getdate()," & iUserid & ",getdate(),'" & sIpAddress & "'," & iACID & ")"
                                                                objDBL.SQLExecuteNonQuery(sAC, iSql)
                                                            Else
                                                                iSql = "" : iSql = "update Acc_Seperate_Schedule set SS_Values=" & dtotrs & " where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
                                                                iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & " and SS_Compid=" & iACID & ""
                                                                objDBL.SQLExecuteNonQuery(sAC, iSql)
                                                            End If

                                                        Catch ex As Exception
                                                            Throw
                                                        End Try
                                                    End If
                                                End If
                                            Else
                                                If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                    dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                End If
                                            End If

                                        Next
                                    Else
                                        If sArray(k) = 60 Then
                                            iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " And SS_CompID =" & iACID & " And "
                                            iSql = iSql & "SS_Group = 60  And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " "
                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                        dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                    End If
                                                    Try
                                                        iSql = "" : iSql = "select count(SS_PKID) from Acc_Seperate_Schedule where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
                                                        iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
                                                        iCont = objDBL.SQLExecuteScalar(sAC, iSql)
                                                        If iCont = 0 Then
                                                            Dim iMaxid As Integer = objDBL.SQLExecuteScalar(sAC, "select IsNull(Max(SS_PKID) + 1,1) from Acc_Seperate_Schedule")
                                                            iSql = "" : iSql = "insert into Acc_Seperate_Schedule(SS_PKID,SS_FinancialYear,SS_CustId,SS_Orgtype,SS_Group,SS_Particulars,"
                                                            iSql = iSql & "SS_Values,SS_DATE,SS_Status,SS_Delflag,SS_CrBy,SS_CrOn,SS_UpdatedBy,SS_UpdatedOn,SS_IPAddress,SS_CompID)"
                                                            iSql = iSql & "values(" & iMaxid & "," & iYearID & "," & iCustID & "," & iOrgID & ",11,'Reserves and surplus',"
                                                            iSql = iSql & "" & dTotalCredit & ",getdate(),'D','A'," & iUserid & ",getdate()," & iUserid & ",getdate(),'" & sIpAddress & "'," & iACID & ")"
                                                            objDBL.SQLExecuteNonQuery(sAC, iSql)
                                                        Else
                                                            iSql = "" : iSql = "update Acc_Seperate_Schedule set SS_Values=" & dTotalCredit & " where SS_Group=11 and SS_Particulars='Reserves and surplus' and SS_Custid=" & iCustID & ""
                                                            iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & " and SS_Compid=" & iACID & ""
                                                            objDBL.SQLExecuteNonQuery(sAC, iSql)
                                                        End If

                                                    Catch ex As Exception
                                                        Throw
                                                    End Try
                                                Next

                                            End If
                                        End If
                                    End If

                                    iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearId =" & iYearID & " And ATD_CompID =" & iACID & " And "
                                    iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_Orgtype=" & iOrgID & " and ATD_Status='A' "
                                    sColumnd = "ATD_Debit"
                                    sColumnc = "ATD_Credit"
                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                            End If
                                            If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                            End If
                                        Next
                                    End If


                                    'Last year
                                    If dtGroup.Rows(i)("SLM_NoteNo") = 21 Then
                                            iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iACID & " And "
                                            iSql = iSql & "CI_GLID =" & sArray(k) & "  and CI_status<>'D' and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                            sColumnd = "CI_CBValues"
                                            sColumnc = ""
                                        Else
                                            If dtSub.Rows(0)("gl_Desc") = "Opening Stock : Raw Material" Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iACID & " And "
                                                iSql = iSql & "Opn_GLID =198 And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                sColumnd = "Opn_DebitAmt"
                                                sColumnc = "Opn_CreditAmount"
                                            ElseIf dtSub.Rows(0)("gl_Desc") = "Closing Stock : Raw Material" Then
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iACID & " And "
                                                iSql = iSql & "CI_GLID =198 And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D'"
                                                sColumnd = "CI_CBValues"
                                                sColumnc = ""
                                            Else
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iACID & " And "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                sColumnd = "Opn_DebitAmt"
                                                sColumnc = "Opn_CreditAmount"
                                            End If
                                        End If


                                        'iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iACID & " And "
                                        'iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                        If dtArray.Rows.Count > 0 Then
                                            For a = 0 To dtArray.Rows.Count - 1
                                                If sColumnc <> "" Then
                                                    If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                        dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                    End If
                                                    If (IsDBNull(dtArray.Rows(a)(sColumnc).ToString()) = False) And (dtArray.Rows(a)(sColumnc).ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                        dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnc).ToString())
                                                    End If
                                                    If sArray(k) = 60 Then
                                                        iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " And SS_CompID =" & iACID & " And "
                                                        iSql = iSql & "SS_Group =11  And SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " "
                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                            dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                                                        End If

                                                    End If
                                                Else
                                                    If (IsDBNull(dtArray.Rows(a)(sColumnd).ToString()) = False) And (dtArray.Rows(a)(sColumnd).ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                        dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)(sColumnd).ToString())
                                                    End If
                                                End If
                                            Next
                                        End If

                                        If dDebit <> 0 And dCredit <> 0 Then
                                            dRow("PresentYear") = dDebit - dCredit
                                        ElseIf (dDebit <> 0) And dCredit = 0 Then
                                            dRow("PresentYear") = dDebit
                                        ElseIf (dCredit <> 0) And dDebit = 0 Then
                                            dRow("PresentYear") = dCredit
                                        Else dDebit = 0 And (dCredit = 0)
                                            dRow("PresentYear") = "0.00"
                                        End If
                                        If dRow("PresentYear").StartsWith("-") = True Then
                                            dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                        End If

                                        If dLDebit <> 0 And dLCredit <> 0 Then
                                        dRow("LastYear") = dLDebit - dLCredit
                                    ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                            dRow("LastYear") = dLDebit
                                        ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                            dRow("LastYear") = dLCredit
                                        Else dLDebit = 0 And (dLCredit = 0)
                                            dRow("LastYear") = "0.00"
                                        End If
                                        If dRow("LastYear").StartsWith("-") = True Then
                                            dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                        End If

                                        dt.Rows.Add(dRow)
                                        dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                    End If
                                End If
                        Next

                        dRow = dt.NewRow()
                        dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                        If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                            dRow("PresentYear") = dTotalCredit - dTotalDebit
                        ElseIf (dTotalDebit <> 0) Then
                            dRow("PresentYear") = dTotalDebit
                        ElseIf (dTotalCredit <> 0) Then
                            dRow("PresentYear") = dTotalCredit
                        ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                            dRow("PresentYear") = "0.00"
                        End If

                        If dRow("PresentYear").StartsWith("-") = True Then
                            dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                        End If

                        If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                            dRow("LastYear") = dTotalLCredit - dTotalLDebit
                        ElseIf (dTotalLDebit <> 0) Then
                            dRow("LastYear") = dTotalLDebit
                        ElseIf (dTotalLCredit <> 0) Then
                            dRow("LastYear") = dTotalLCredit
                        ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                            dRow("LastYear") = "0.00"
                        End If

                        If dRow("LastYear").StartsWith("-") = True Then
                            dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                        End If

                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow()
                        dRow("Particulars") = ""
                        dt.Rows.Add(dRow)
                        dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                    End If
                Next
            End If

            Dim schSql As String
            Dim iCount As Integer

            schSql = "Select * from Schedule_Linkage_Master where SLM_CompID =" & iACID & " and SLM_NoteNo <> 0 And SLM_OrgID=" & iOrgID & " and SLM_CustID=" & iCustID & " and SLM_NoteNo=21"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, schSql)
            If dtGroup.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Changes in inventories of finished goods, work-in-progress and stock-in-trade " & "</B>"
                dt.Rows.Add(dRow)
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Inventories at the end of the year" & "</B>"
                dt.Rows.Add(dRow)
                sArray = dtGroup.Rows(0)("SLM_GLLedger").ToString().Split(",")
                If sArray.Length > 0 Then
                    For k = 0 To sArray.Length - 1
                        q = sArray.Length - 1
                        If sArray(k) <> "" Then
                            aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
                            dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                            If dtSub.Rows.Count > 0 Then
                                If dtSub.Rows(0)("gl_id") <> 198 Then
                                    dRow = dt.NewRow()
                                    dRow("Particulars") = dtSub.Rows(0)("gl_Desc") 'dtSub.Rows(0)("gl_glCode") & " - " &

                                    'Current Year
                                    ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                    iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                    If iCount <> 0 Then
                                        iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And "
                                        iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""

                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                        If dtArray.Rows.Count > 0 Then
                                            For a = 0 To dtArray.Rows.Count - 1
                                                If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                End If
                                            Next
                                        End If
                                    Else
                                        dDebit = 0
                                        ' dTotalDebit = 0
                                    End If



                                    'Last year
                                    ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                    iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                    If iCount <> 0 Then
                                        iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " And ci_CompID =" & iACID & " And "
                                        iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                        If dtArray.Rows.Count > 0 Then
                                            For a = 0 To dtArray.Rows.Count - 1
                                                If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                    dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                                End If
                                            Next
                                        End If
                                    Else
                                        dDebit = 0
                                        ' dTotalDebit = 0
                                    End If

                                    If dDebit <> 0 And dCredit <> 0 Then
                                        dRow("PresentYear") = dDebit - dCredit
                                    ElseIf (dDebit <> 0) And dCredit = 0 Then
                                        dRow("PresentYear") = dDebit
                                    ElseIf (dCredit <> 0) And dDebit = 0 Then
                                        dRow("PresentYear") = dCredit
                                    Else dDebit = 0 And (dCredit = 0)
                                        dRow("PresentYear") = "0.00"
                                    End If
                                    If dRow("PresentYear").StartsWith("-") = True Then
                                        dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                    End If

                                    If dLDebit <> 0 And dLCredit <> 0 Then
                                        dRow("LastYear") = dLDebit - dLCredit
                                    ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                        dRow("LastYear") = dLDebit
                                    ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                        dRow("LastYear") = dLCredit
                                    Else dLDebit = 0 And (dLCredit = 0)
                                        dRow("LastYear") = "0.00"
                                    End If
                                    If dRow("LastYear").StartsWith("-") = True Then
                                        dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                    End If

                                    dt.Rows.Add(dRow)
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If

                            End If
                        End If

                        If q = k Then
                            dRow = dt.NewRow()
                            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                            If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                dRow("PresentYear") = dTotalCredit - dTotalDebit
                            ElseIf (dTotalDebit <> 0) Then
                                dRow("PresentYear") = dTotalDebit
                            ElseIf (dTotalCredit <> 0) Then
                                dRow("PresentYear") = dTotalCredit
                            ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                dRow("PresentYear") = "0.00"
                            End If

                            If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                dRow("LastYear") = dTotalLCredit - dTotalLDebit
                            ElseIf (dTotalLDebit <> 0) Then
                                dRow("LastYear") = dTotalLDebit
                            ElseIf (dTotalLCredit <> 0) Then
                                dRow("LastYear") = dTotalLCredit
                            ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                dRow("LastYear") = "0.00"
                            End If

                            dt.Rows.Add(dRow)

                            dRow = dt.NewRow()
                            dRow("Particulars") = ""
                            dt.Rows.Add(dRow)

                            dEndYearTotal = dTotalDebit : dEndlYearTotal = dTotalLDebit
                            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                        End If
                    Next


                End If
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Inventories at the beginning of the year:" & "</B>"
                dt.Rows.Add(dRow)
                sArray = dtGroup.Rows(0)("SLM_GLLedger").ToString().Split(",")
                If sArray.Length > 0 Then
                    For k = 0 To sArray.Length - 1
                        q = sArray.Length - 1
                        If sArray(k) <> "" Then
                            aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iACID & " and gl_Status ='A' and gl_Delflag='C' And gl_CustID=" & iCustID & ""
                            dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                            If dtSub.Rows.Count > 0 Then
                                If dtSub.Rows(0)("gl_id") <> 198 Then
                                    dRow = dt.NewRow()
                                    dRow("Particulars") = dtSub.Rows(0)("gl_Desc") 'dtSub.Rows(0)("gl_glCode") & " - " &

                                    'Current Year
                                    ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                    iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                    If iCount <> 0 Then
                                        iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And "
                                        iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_Status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""

                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                        If dtArray.Rows.Count > 0 Then
                                            For a = 0 To dtArray.Rows.Count - 1
                                                If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                End If
                                            Next
                                        End If
                                    Else
                                        dDebit = 0
                                        ' dTotalDebit = 0
                                    End If



                                    'Last year
                                    ciSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " And ci_CompID =" & iACID & " And CI_GLID =" & sArray(k) & " and CI_Status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                    iCount = objDBL.SQLExecuteScalar(sAC, ciSql)
                                    If iCount <> 0 Then
                                        iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " And ci_CompID =" & iACID & " And "
                                        iSql = iSql & "CI_GLID =" & sArray(k) & " and CI_Status<>'D' and CI_Custid=" & iCustID & " and CI_Orgtype=" & iOrgID & ""
                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                        If dtArray.Rows.Count > 0 Then
                                            For a = 0 To dtArray.Rows.Count - 1
                                                If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                    dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                                End If
                                            Next
                                        End If
                                    Else
                                        dDebit = 0
                                        ' dTotalDebit = 0
                                    End If

                                    If dDebit <> 0 And dCredit <> 0 Then
                                        dRow("PresentYear") = dDebit - dCredit
                                    ElseIf (dDebit <> 0) And dCredit = 0 Then
                                        dRow("PresentYear") = dDebit
                                    ElseIf (dCredit <> 0) And dDebit = 0 Then
                                        dRow("PresentYear") = dCredit
                                    Else dDebit = 0 And (dCredit = 0)
                                        dRow("PresentYear") = "0.00"
                                    End If
                                    If dRow("PresentYear").StartsWith("-") = True Then
                                        dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                                    End If

                                    If dLDebit <> 0 And dLCredit <> 0 Then
                                        dRow("LastYear") = dLDebit - -dLCredit
                                    ElseIf (dLDebit <> 0) And dLCredit = 0 Then
                                        dRow("LastYear") = dLDebit
                                    ElseIf (dLCredit <> 0) And dLDebit = 0 Then
                                        dRow("LastYear") = dLCredit
                                    Else dLDebit = 0 And (dLCredit = 0)
                                        dRow("LastYear") = "0.00"
                                    End If
                                    If dRow("LastYear").StartsWith("-") = True Then
                                        dRow("LastYear") = dRow("LastYear").Remove(0, 1)
                                    End If

                                    dt.Rows.Add(dRow)
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                        End If

                        If q = k Then
                            dRow = dt.NewRow()
                            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                            If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                                dRow("PresentYear") = dTotalCredit - dTotalDebit
                            ElseIf (dTotalDebit <> 0) Then
                                dRow("PresentYear") = dTotalDebit
                            ElseIf (dTotalCredit <> 0) Then
                                dRow("PresentYear") = dTotalCredit
                            ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                                dRow("PresentYear") = "0.00"
                            End If

                            If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                                dRow("LastYear") = dTotalLCredit - dTotalLDebit
                            ElseIf (dTotalLDebit <> 0) Then
                                dRow("LastYear") = dTotalLDebit
                            ElseIf (dTotalLCredit <> 0) Then
                                dRow("LastYear") = dTotalLCredit
                            ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                                dRow("LastYear") = "0.00"
                            End If

                            dt.Rows.Add(dRow)

                            dRow = dt.NewRow()
                            dRow("Particulars") = ""
                            dt.Rows.Add(dRow)

                            dbeginYearTotal = dTotalDebit : dbeginlYearTotal = dTotalLDebit
                            dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                        End If
                    Next
                End If
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Total  Net (increase) / decrease" & "</B>"

                If (dbeginYearTotal >= 0) And (dEndYearTotal >= 0) Then
                    dRow("PresentYear") = dbeginYearTotal - dEndYearTotal
                ElseIf (dbeginYearTotal = 0) And (dEndYearTotal = 0) Then
                    dRow("PresentYear") = "0.00"
                End If

                If (dbeginlYearTotal <> 0) And (dEndlYearTotal >= 0) Then
                    dRow("LastYear") = dbeginlYearTotal - dEndlYearTotal
                ElseIf (dbeginlYearTotal = 0) And (dEndlYearTotal = 0) Then
                    dRow("LastYear") = "0.00"
                End If

                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Particulars") = ""
                dt.Rows.Add(dRow)
                dbeginYearTotal = 0 : dbeginlYearTotal = 0 : dEndlYearTotal = 0 : dEndYearTotal = 0
                dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVarianceSheet(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer)
        Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim dtHead As New DataTable
        Dim dtLink As New DataTable
        Dim dtArray As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0
        Dim a As Integer = 0
        'Dim sArray As Array
        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00
        Dim iLastYear As Integer = 0
        Dim iFixedAssets As Integer = 0
        Dim iStatusCheck As Integer = 0

        Try
            dt.Columns.Add("GLCode")
            dt.Columns.Add("Description")
            dt.Columns.Add("CDebit")
            dt.Columns.Add("CCredit")
            dt.Columns.Add("LDebit")
            dt.Columns.Add("LCredit")
            dt.Columns.Add("VDebit")
            dt.Columns.Add("VCredit")
            dt.Columns.Add("Percentage")

            'Liabilites

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iCOmpID & ")"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If

            mSql = "" : mSql = "Select Distinct(gl_AccHead) from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_CompID =" & iCOmpID & " And "
            mSql = mSql & "gl_Delflag ='C' and gl_Status ='A' "
            dtHead = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)

            If dtHead.Rows.Count > 0 Then
                For j = 0 To dtHead.Rows.Count - 1

                    dRow = dt.NewRow()
                    dRow("GLCode") = ""
                    If dtHead.Rows(j)("gl_AccHead") = 1 Then
                        dRow("Description") = "<B>" & "Asset" & "</B>"
                    ElseIf dtHead.Rows(j)("gl_AccHead") = 2 Then
                        dRow("Description") = "<B>" & "Income" & "</B>"
                    ElseIf dtHead.Rows(j)("gl_AccHead") = 3 Then
                        dRow("Description") = "<B>" & "Expenditure" & "</B>"
                    ElseIf dtHead.Rows(j)("gl_AccHead") = 4 Then
                        dRow("Description") = "<B>" & "Liability" & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    mSql = "" : mSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_AccHead=" & dtHead.Rows(j)("gl_AccHead") & " And gl_Head = 2 And gl_CompID =" & iCOmpID & " And "
                    mSql = mSql & "gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                    If dtLink.Rows.Count > 0 Then
                        For k = 0 To dtLink.Rows.Count - 1

                            dRow = dt.NewRow()

                            'Present Year
                            dRow("GLCode") = objDBL.SQLGetDescription(sNameSpace, "Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtLink.Rows(k)("gl_id") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " ")
                            dRow("Description") = objDBL.SQLGetDescription(sNameSpace, "Select GL_Desc From Chart_Of_Accounts Where GL_ID=" & dtLink.Rows(k)("gl_id") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " ")

                            iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & dtLink.Rows(k)("gl_id") & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " "
                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                            If dtArray.Rows.Count > 0 Then
                                For a = 0 To dtArray.Rows.Count - 1
                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                    End If

                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                    End If
                                Next
                            End If

                            dRow("CDebit") = dDebit
                            dRow("CCredit") = dCredit

                            'Last Year
                            iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID - 1 & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & dtLink.Rows(k)("gl_id") & "  and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                            If dtArray.Rows.Count > 0 Then
                                For a = 0 To dtArray.Rows.Count - 1
                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                    End If

                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                    End If
                                Next
                            End If

                            dRow("LDebit") = dLDebit
                            dRow("LCredit") = dLCredit

                            dRow("VDebit") = dRow("CDebit") - dRow("LDebit")
                            dRow("VCredit") = dRow("CCredit") - dRow("LCredit")
                            If dRow("VDebit") <> 0 Then
                                If dDebit > 0 Then
                                    dRow("Percentage") = (dRow("VDebit") * 100) / dDebit
                                ElseIf dCredit > 0 Then
                                    dRow("Percentage") = (dRow("VDebit") * 100) / dCredit
                                End If
                            ElseIf dRow("VCredit") <> 0 Then
                                If dDebit > 0 Then
                                    dRow("Percentage") = (dRow("VCredit") * 100) / dDebit
                                ElseIf dCredit > 0 Then
                                    dRow("Percentage") = (dRow("VCredit") * 100) / dCredit
                                End If
                            End If

                            dt.Rows.Add(dRow)

                            dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                        Next
                    End If

                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'vijaylakshmi modified 28-01-2020 'Public Function LoadScheduleReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer)
    '    Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
    '    Dim dRow As DataRow
    '    Dim dt As New DataTable, dtGroup As New DataTable, dtSub As New DataTable, dtLink As New DataTable, dtArray As New DataTable, dtFA As New DataTable
    '    Dim dtYear As New DataTable, dtOB As New DataTable
    '    Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0, m As Integer = 0
    '    Dim sArray As Array
    '    Dim dDebit As Double = 0.00, dCredit As Double = 0.00
    '    Dim dLDebit As Double = 0.00, dLCredit As Double = 0.00
    '    Dim dTotalDebit As Double = 0.00, dTotalCredit As Double = 0.00
    '    Dim dTotalLDebit As Double = 0.00, dTotalLCredit As Double = 0.00
    '    Dim iHead As Integer = 0, iSLNo As Integer = 0, iLastYear As Integer = 0, iStatusCheck As Integer = 0
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("SLNo")
    '        dt.Columns.Add("Particulars")
    '        dt.Columns.Add("NoteNo")
    '        dt.Columns.Add("PresentYear")
    '        dt.Columns.Add("LastYear")

    '        'Liabilites

    '        sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
    '        sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iACID & ")"
    '        dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtYear.Rows.Count > 0 Then
    '            'dr.Read()
    '            iLastYear = dtYear.Rows(i).Item("YMS_ID")
    '        Else
    '            iLastYear = 0
    '        End If
    '        'sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_YEARID=" & iYearID & " and YMS_CompID=" & iACID & "" 'Commented vijayalakshmi 12/12/2019 this query fetching the current year
    '        'dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        'If dtYear.Rows.Count > 0 Then
    '        '    For i = 0 To dtYear.Rows.Count - 1
    '        '        iLastYear = dtYear.Rows(i).Item("YMS_YEARID")
    '        '    Next
    '        'Else
    '        '    iLastYear = 0
    '        'End If

    '        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
    '        dtOB = objDBL.SQLExecuteDataTable(sAC, iSql)
    '        If dtOB.Rows.Count > 0 Then
    '            For j = 0 To dtOB.Rows.Count - 1
    '                If dtOB.Rows(j).Item("Opn_Status") = "F" Then
    '                    iStatusCheck = 0
    '                Else
    '                    iStatusCheck = 1
    '                End If
    '            Next
    '        Else
    '            iStatusCheck = 1
    '        End If

    '        sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "EQUITY AND LIABILITIES" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("gl_ID")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("gl_ID")
    '                        End If

    '                        If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
    '                        End If

    '                        dDebit = 0 : dCredit = 0

    '                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
    '                        mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
    '                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                        If dtLink.Rows.Count > 0 Then
    '                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                            End If

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        If iStatusCheck = 0 Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        Else
    '                                            iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                            iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If

    '                                            iSql = "" : iSql = "Select * from Customer_coa where CC_YearID =" & iYearID & " and CC_CompID =" & iACID & " and "
    '                                            iSql = iSql & "CC_Parent =" & sArray(k) & " and CC_CustId=" & iCustID & " and CC_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        End If
    '                                    End If
    '                                Next

    '                                If (dDebit <> 0) And (dCredit <> 0) Then
    '                                    dRow("PresentYear") = dCredit - dDebit
    '                                ElseIf dDebit <> 0 Then
    '                                    dRow("PresentYear") = dDebit
    '                                ElseIf dCredit <> 0 Then
    '                                    dRow("PresentYear") = dCredit
    '                                ElseIf (dDebit = 0) And (dCredit = 0) Then
    '                                    dRow("PresentYear") = "0.00"
    '                                End If

    '                                dTotalDebit = dTotalDebit + dRow("PresentYear")
    '                                dDebit = 0 : dCredit = 0
    '                            End If


    '                            'Last Year

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                        If dtArray.Rows.Count > 0 Then
    '                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                End If

    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                End If
    '                                            Next
    '                                        End If
    '                                    End If
    '                                Next

    '                                If (dLDebit <> 0) And (dLCredit <> 0) Then
    '                                    dRow("LastYear") = dLCredit - dLDebit
    '                                ElseIf dLDebit <> 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf dLCredit <> 0 Then
    '                                    dRow("LastYear") = dLCredit
    '                                ElseIf (dLDebit = 0) And (dLCredit = 0) Then
    '                                    dRow("LastYear") = "0.00"
    '                                End If

    '                                dTotalLDebit = dTotalLDebit + dRow("LastYear")
    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
    '            dRow("PresentYear") = dTotalDebit 'dTotalCredit - dTotalDebit
    '            dRow("LastYear") = dTotalLDebit 'dTotalLCredit - dTotalLDebit
    '            dt.Rows.Add(dRow)
    '        End If


    '        'Assets
    '        dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '        sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 1 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "ASSETS" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("gl_ID")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("gl_ID")
    '                        End If

    '                        If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
    '                        End If

    '                        dDebit = 0 : dCredit = 0

    '                        If dtSub.Rows(j)("gl_Desc").ToString() = "Fixed Assets" Then
    '                            aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtSub.Rows(j)("gl_ID") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '                            dtFA = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                            If dtFA.Rows.Count > 0 Then
    '                                For m = 0 To dtFA.Rows.Count - 1
    '                                    dt.Rows.Add(dRow)
    '                                    dRow = dt.NewRow()

    '                                    If IsDBNull(dtFA.Rows(m)("gl_Desc").ToString()) = False Then
    '                                        dRow("Particulars") = dtFA.Rows(m)("gl_Desc")
    '                                    End If

    '                                    mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
    '                                    mSql = mSql & "SLM_SUbGroupID =" & dtFA.Rows(m)("gl_Parent") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
    '                                    dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                                    If dtLink.Rows.Count > 0 Then
    '                                        If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                            dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                                        End If

    '                                        sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                                        If sArray.Length - 1 Then
    '                                            For k = 0 To sArray.Length - 1
    '                                                If sArray(k) <> "" Then
    '                                                    If iStatusCheck = 0 Then
    '                                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                                        iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                        If dtArray.Rows.Count > 0 Then
    '                                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                                End If

    '                                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                                End If
    '                                                            Next
    '                                                        End If
    '                                                    Else
    '                                                        iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                                        iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
    '                                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                        If dtArray.Rows.Count > 0 Then
    '                                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                                If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                                End If

    '                                                                If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                                End If
    '                                                            Next

    '                                                        End If

    '                                                        iSql = "" : iSql = "Select * from Customer_coa where CC_YearID =" & iYearID & " and CC_CompID =" & iACID & " and "
    '                                                        iSql = iSql & "CC_Parent =" & sArray(k) & " and CC_CustId=" & iCustID & " and CC_IndType=" & iOrgID & ""
    '                                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                        If dtArray.Rows.Count > 0 Then
    '                                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                                If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
    '                                                                    dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
    '                                                                End If

    '                                                                If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
    '                                                                    dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
    '                                                                End If
    '                                                            Next
    '                                                        End If
    '                                                    End If

    '                                                End If
    '                                            Next


    '                                            If (dDebit <> 0) And (dCredit <> 0) Then
    '                                                dRow("PresentYear") = dCredit - dDebit
    '                                            ElseIf dDebit <> 0 Then
    '                                                dRow("PresentYear") = dDebit
    '                                            ElseIf dCredit <> 0 Then
    '                                                dRow("PresentYear") = dCredit
    '                                            ElseIf (dDebit = 0) And (dCredit = 0) Then
    '                                                dRow("PresentYear") = "0.00"
    '                                            End If

    '                                            dTotalDebit = dTotalDebit + dRow("PresentYear")
    '                                            dDebit = 0 : dCredit = 0
    '                                        End If

    '                                        'Last Year
    '                                        sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                                        If sArray.Length - 1 Then
    '                                            For k = 0 To sArray.Length - 1
    '                                                If sArray(k) <> "" Then
    '                                                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                                    iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                    If dtArray.Rows.Count > 0 Then
    '                                                        For a = 0 To dtArray.Rows.Count - 1
    '                                                            If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                                dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                            End If

    '                                                            If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                                dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                            End If
    '                                                        Next

    '                                                    End If
    '                                                End If
    '                                            Next

    '                                            If (dLDebit <> 0) And (dLCredit <> 0) Then
    '                                                dRow("LastYear") = dLCredit - dLDebit
    '                                            ElseIf dLDebit <> 0 Then
    '                                                dRow("LastYear") = dLDebit
    '                                            ElseIf dLCredit <> 0 Then
    '                                                dRow("LastYear") = dLCredit
    '                                            ElseIf (dLDebit = 0) And (dLCredit = 0) Then
    '                                                dRow("LastYear") = "0.00"
    '                                            End If

    '                                            dTotalLDebit = dTotalLDebit + dRow("LastYear")
    '                                            dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                                        End If
    '                                    End If
    '                                Next
    '                            End If

    '                        Else

    '                            '------------------------------------------------------------------------------------------
    '                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
    '                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
    '                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                            If dtLink.Rows.Count > 0 Then
    '                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                                End If

    '                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                                If sArray.Length - 1 Then
    '                                    For k = 0 To sArray.Length - 1
    '                                        If sArray(k) <> "" Then
    '                                            If iStatusCheck = 0 Then
    '                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                                iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    For a = 0 To dtArray.Rows.Count - 1
    '                                                        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                        End If

    '                                                        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                        End If
    '                                                    Next
    '                                                End If
    '                                            Else
    '                                                iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                                iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
    '                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    For a = 0 To dtArray.Rows.Count - 1
    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                        End If

    '                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                        End If
    '                                                    Next

    '                                                End If

    '                                                iSql = "" : iSql = "Select * from Customer_coa where CC_YearID =" & iYearID & " and CC_CompID =" & iACID & " and "
    '                                                iSql = iSql & "CC_Parent =" & sArray(k) & " and CC_CustId=" & iCustID & " and CC_IndType=" & iOrgID & ""
    '                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                                If dtArray.Rows.Count > 0 Then
    '                                                    For a = 0 To dtArray.Rows.Count - 1
    '                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
    '                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
    '                                                        End If

    '                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
    '                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
    '                                                        End If
    '                                                    Next
    '                                                End If
    '                                            End If

    '                                        End If
    '                                    Next

    '                                    If (dDebit <> 0) And (dCredit <> 0) Then
    '                                        dRow("PresentYear") = dCredit - dDebit
    '                                    ElseIf dDebit <> 0 Then
    '                                        dRow("PresentYear") = dDebit
    '                                    ElseIf dCredit <> 0 Then
    '                                        dRow("PresentYear") = dCredit
    '                                    ElseIf (dDebit = 0) And (dCredit = 0) Then
    '                                        dRow("PresentYear") = "0.00"
    '                                    End If

    '                                    dTotalDebit = dTotalDebit + dRow("PresentYear")
    '                                    dDebit = 0 : dCredit = 0
    '                                End If

    '                                'Last Year
    '                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                                If sArray.Length - 1 Then
    '                                    For k = 0 To sArray.Length - 1
    '                                        If sArray(k) <> "" Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next

    '                                            End If
    '                                        End If
    '                                    Next


    '                                    If (dLDebit <> 0) And (dLCredit <> 0) Then
    '                                        dRow("LastYear") = dLCredit - dLDebit
    '                                    ElseIf dLDebit <> 0 Then
    '                                        dRow("LastYear") = dLDebit
    '                                    ElseIf dLCredit <> 0 Then
    '                                        dRow("LastYear") = dLCredit
    '                                    ElseIf (dLDebit = 0) And (dLCredit = 0) Then
    '                                        dRow("LastYear") = "0.00"
    '                                    End If

    '                                    dTotalLDebit = dTotalLDebit + dRow("LastYear")
    '                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                                End If
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
    '            dRow("PresentYear") = dTotalDebit
    '            dRow("LastYear") = dTotalLDebit
    '            dt.Rows.Add(dRow)
    '        End If




    '        'Income
    '        dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '        sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 2 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "REVENUE" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("gl_ID")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("gl_ID")
    '                        End If

    '                        If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
    '                        End If

    '                        dDebit = 0 : dCredit = 0

    '                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =2 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
    '                        mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
    '                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                        If dtLink.Rows.Count > 0 Then
    '                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                            End If

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        If iStatusCheck = 0 Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        Else
    '                                            iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                            iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If

    '                                            iSql = "" : iSql = "Select * from Customer_coa where CC_YearID =" & iYearID & " and CC_CompID =" & iACID & " and "
    '                                            iSql = iSql & "CC_Parent =" & sArray(k) & " and CC_CustId=" & iCustID & " and CC_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        End If

    '                                    End If
    '                                Next

    '                                If (dDebit <> 0) And (dCredit <> 0) Then
    '                                    dRow("PresentYear") = dCredit - dDebit
    '                                ElseIf dDebit <> 0 Then
    '                                    dRow("PresentYear") = dDebit
    '                                ElseIf dCredit <> 0 Then
    '                                    dRow("PresentYear") = dCredit
    '                                ElseIf (dDebit = 0) And (dCredit = 0) Then
    '                                    dRow("PresentYear") = "0.00"
    '                                End If

    '                                dTotalDebit = dTotalDebit + dRow("PresentYear")
    '                                dDebit = 0 : dCredit = 0
    '                            End If

    '                            'Last Year
    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                        If dtArray.Rows.Count > 0 Then
    '                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                End If

    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                End If
    '                                            Next

    '                                        End If
    '                                    End If
    '                                Next

    '                                If (dLDebit <> 0) And (dLCredit <> 0) Then
    '                                    dRow("LastYear") = dLCredit - dLDebit
    '                                ElseIf dLDebit <> 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf dLCredit <> 0 Then
    '                                    dRow("LastYear") = dLCredit
    '                                ElseIf (dLDebit = 0) And (dLCredit = 0) Then
    '                                    dRow("LastYear") = "0.00"
    '                                End If

    '                                dTotalLDebit = dTotalLDebit + dRow("LastYear")
    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
    '            dRow("PresentYear") = dTotalDebit
    '            dRow("LastYear") = dTotalLDebit
    '            dt.Rows.Add(dRow)
    '        End If


    '        'Expenditure
    '        dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
    '        sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 3 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '        dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        If dtGroup.Rows.Count > 0 Then

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "EXPENDITURE" & "</B>"
    '            dt.Rows.Add(dRow)

    '            For i = 0 To dtGroup.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
    '                    dRow("ID") = dtGroup.Rows(i)("gl_ID")
    '                End If

    '                dRow("SLNo") = iSLNo + 1

    '                If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
    '                    dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
    '                End If
    '                dt.Rows.Add(dRow)

    '                aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=" & iOrgID & " and gl_custId=" & iCustID & " order by gl_id"
    '                dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
    '                If dtSub.Rows.Count > 0 Then
    '                    For j = 0 To dtSub.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
    '                            dRow("ID") = dtSub.Rows(j)("gl_ID")
    '                        End If

    '                        If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
    '                            dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
    '                        End If

    '                        dDebit = 0 : dCredit = 0

    '                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =3 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
    '                        mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
    '                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
    '                        If dtLink.Rows.Count > 0 Then
    '                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
    '                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
    '                            End If

    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        If iStatusCheck = 0 Then
    '                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
    '                                            iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        Else
    '                                            iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
    '                                            iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If

    '                                            iSql = "" : iSql = "Select * from Customer_coa where CC_YearID =" & iYearID & " and CC_CompID =" & iACID & " and "
    '                                            iSql = iSql & "CC_Parent =" & sArray(k) & " and CC_CustId=" & iCustID & " and CC_IndType=" & iOrgID & ""
    '                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                            If dtArray.Rows.Count > 0 Then
    '                                                For a = 0 To dtArray.Rows.Count - 1
    '                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
    '                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
    '                                                    End If

    '                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
    '                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
    '                                                    End If
    '                                                Next
    '                                            End If
    '                                        End If
    '                                    End If
    '                                Next

    '                                If (dDebit <> 0) And (dCredit <> 0) Then
    '                                    dRow("PresentYear") = dCredit - dDebit
    '                                ElseIf dDebit <> 0 Then
    '                                    dRow("PresentYear") = dDebit
    '                                ElseIf dCredit <> 0 Then
    '                                    dRow("PresentYear") = dCredit
    '                                ElseIf (dDebit = 0) And (dCredit = 0) Then
    '                                    dRow("PresentYear") = "0.00"
    '                                End If

    '                                dTotalDebit = dTotalDebit + dRow("PresentYear")
    '                                dDebit = 0 : dCredit = 0
    '                            End If

    '                            'Last Year
    '                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
    '                            If sArray.Length - 1 Then
    '                                For k = 0 To sArray.Length - 1
    '                                    If sArray(k) <> "" Then
    '                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
    '                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
    '                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
    '                                        If dtArray.Rows.Count > 0 Then
    '                                            For a = 0 To dtArray.Rows.Count - 1
    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
    '                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
    '                                                End If

    '                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
    '                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
    '                                                End If
    '                                            Next
    '                                        End If
    '                                    End If
    '                                Next

    '                                If (dLDebit <> 0) And (dLCredit <> 0) Then
    '                                    dRow("LastYear") = dLCredit - dLDebit
    '                                ElseIf dLDebit <> 0 Then
    '                                    dRow("LastYear") = dLDebit
    '                                ElseIf dLCredit <> 0 Then
    '                                    dRow("LastYear") = dLCredit
    '                                ElseIf (dLDebit = 0) And (dLCredit = 0) Then
    '                                    dRow("LastYear") = "0.00"
    '                                End If

    '                                dTotalLDebit = dTotalLDebit + dRow("LastYear")
    '                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
    '                            End If
    '                        End If
    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                iSLNo = iSLNo + 1
    '            Next

    '            dRow = dt.NewRow()
    '            dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
    '            dRow("PresentYear") = dTotalDebit
    '            dRow("LastYear") = dTotalLDebit
    '            dt.Rows.Add(dRow)
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadScheduleReport(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iUserid As Integer, ByVal sIpAddress As String)
        Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim dtGroup As New DataTable
        Dim dtSub As New DataTable
        Dim dtLink As New DataTable
        Dim dtArray As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0
        Dim a As Integer = 0
        Dim sArray As Array
        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00

        Dim iHead As Integer = 0
        Dim iSLNo As Integer = 0
        Dim iLastYear As Integer = 0

        Dim iFixedAssets As Integer = 0

        Dim iStatusCheck As Integer = 0

        Dim sSPresentYear As String = "" : Dim sLastYear As String = ""
        Dim sAsseCurrentYear As String = "" : Dim sAssetLastYear As String = ""
        Dim sSSValues As String = ""

        Dim sExCurrentYear As String = "" : Dim sExLastYear As String = ""
        Dim sTotalDebit As String = "" : Dim sTotalCredit As String = ""
        Dim sExTotalDebit As String = "" : Dim sExTotalCredit As String = ""

        Dim dPCIValue As Double = 0.00 : Dim dLCIValue As Double = 0.00
        Dim dPIncomeSum As Double = 0.00 : Dim dLIncomeSum As Double = 0.00
        Dim dPExpensSum As Double = 0.00 : Dim dLExpensSum As Double = 0.00
        Dim dPExceptionalSum As Double = 0.00 : Dim dLExceptionalSum As Double = 0.00
        Dim dPExp As Double = 0.00 : Dim dLExp As Double = 0.00
        Dim dPExtraSum As Double = 0.00 : Dim dLExtraSum As Double = 0.00
        Dim dPExt As Double = 0.00 : Dim dLExt As Double = 0.00
        Dim dPExpSum As Double = 0.00 : Dim dLExpSum As Double = 0.00
        Dim dPTaxExpenses As Double = 0.00 : Dim dLTaxExpenses As Double = 0.00
        Dim dPTaxExpSum As Double = 0.00 : Dim dLTaxExpSum As Double = 0.00
        Dim dPDisConOperations As Double = 0.00 : Dim dLDisConOperations As Double = 0.00
        Dim dPContOperations As Double = 0.00 : Dim dLContOperations As Double = 0.00
        Dim dPTotalOperations As Double = 0.00 : Dim dLTotalOperations As Double = 0.00

        Dim iCount As Integer = 0

        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            'Liabilites

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iCOmpID & ")"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " and gl_OrgTypeID=" & iOrgID & " And gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EQUITY And LIABILITIES" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_CompID =" & iCOmpID & " And "
                    aSql = aSql & "gl_Delflag ='C' and gl_Status ='A'  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & " and SLM_custid=" & iCustID & "" ' Added SLM_custid=" & iCustID & "" vijayalakshmi 29-07-19
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If dtSub.Rows(j)("gl_ID") = 11 Then
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule Where SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " And SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =11 and SS_Status='D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If
                                                        dDebit = 0
                                                        sSSValues = Convert.ToString(dCredit)
                                                    Next

                                                End If
                                                GoTo P
                                            Else
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                'Customer COA'

                                                iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If

                                            'Customer COA'

                                        End If
                                    Next

P:                                  sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    'If sSPresentYear.StartsWith("-") = True Then
                                    'sSPresentYear = sSPresentYear.Remove(0, 1)
                                    ' End If
                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    dRow("PresentYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr
                                    'If sSSValues.StartsWith("-") = True Then
                                    '    dTotalDebit = dTotalDebit - Convert.ToDouble(sSPresentYear)
                                    '    sSSValues = ""
                                    'Else
                                    dTotalDebit = dTotalDebit + Convert.ToDouble(sSPresentYear)
                                    'End If

                                    dDebit = 0 : dCredit = 0
                                End If


                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If dtSub.Rows(j)("gl_ID") = 11 Then
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule Where SS_CustID=" & iCustID & " And SS_OrgType=" & iOrgID & " And SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =11 and SS_Status='D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("SS_Values")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If
                                                        dDebit = 0
                                                        sSSValues = Convert.ToString(dCredit)
                                                    Next
                                                End If
                                                GoTo l
                                            Else
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If

                                                'Customer COA'
                                                iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) ' + dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    Next

l:                                  sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    ' If sSPresentYear.StartsWith("-") = True Then
                                    'sSPresentYear = sSPresentYear.Remove(0, 1)
                                    ' End If
                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    dRow("LastYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr

                                    'If sSSValues.StartsWith("-") = True Then
                                    '    dTotalLDebit = dTotalLDebit - Convert.ToDouble(sSPresentYear)
                                    '    sSSValues = ""
                                    'Else
                                    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sSPresentYear)
                                    'End If

                                    dDebit = 0 : dCredit = 0
                                End If


                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '            iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & ""

                                '            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '            If dtArray.Rows.Count > 0 Then
                                '                For a = 0 To dtArray.Rows.Count - 1
                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                    End If

                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                    End If
                                '                Next

                                '            End If
                                '        End If
                                '    Next

                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sLastYear.StartsWith("-") = True Then
                                '        sLastYear = sLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sLastYear

                                '    Dim dLDbCr As Double = 0.00
                                '    dLDbCr = dLDebit - dLCredit
                                '    'dTotalLDebit = dTotalLDebit + dLDbCr
                                '    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sLastYear)

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If


            'Assets
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & "  and gl_OrgTypeID=" & iOrgID & " And gl_head in(0) and gl_AccHead = 1 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "ASSETS" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & "  and gl_OrgTypeID=" & iOrgID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_Delflag ='C' and "
                    aSql = aSql & "gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            'If (dtSub.Rows(j)("gl_Desc").ToString() = "Tangible Assets") Or (dtSub.Rows(j)("gl_Desc").ToString() = "Intangible Assets") Then
                            '    iFixedAssets = 1
                            'Else
                            '    iFixedAssets = 0
                            'End If

                            'Vijayalakshmi 16/12/2019 values r not fetching for tangible and intangible

                            'If (dtSub.Rows(j)("gl_Desc").ToString() = "Fixed Assets- Tangible Assets") Or (dtSub.Rows(j)("gl_Desc").ToString() = "Fixed Assets- Intangible Assets") Then
                            '    iFixedAssets = 1
                            'Else
                            '    iFixedAssets = 0
                            'End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            'If iFixedAssets = 0 Then 'Vijayalakshmi 16/12/2019 values r not fetching for tangible and intangible

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next

                                            End If

                                            'Customer COA'
                                            If dtLink.Rows(0)("SLM_NoteNo") = 21 Then
                                                iSql = "" : iSql = "Select * from Acc_Changes_Inventories Where CI_Status<>'D' and CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " And CI_FinancialYear =" & iYearID & " and CI_CompID =" & iCOmpID & " and CI_Glid =" & sArray(k) & " "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            ' dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit")) ' Commented by Vijayalakshmi 26/11/2019

                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If
                                                        dCredit = 0
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            ' dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit")) ' Commented by Vijayalakshmi 26/11/2019

                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If

                                            'Customer COA'

                                            ' End If
                                        End If
                                    Next

                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sAsseCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    '  If sAsseCurrentYear.StartsWith("-") = True Then
                                    ' sAsseCurrentYear = sAsseCurrentYear.Remove(0, 1)
                                    '  End If
                                    dRow("PresentYear") = sAsseCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr

                                    dTotalDebit = dTotalDebit + Convert.ToDouble(sAsseCurrentYear)

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            'If iFixedAssets = 0 Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details Where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next

                                            End If

                                            'Customer COA'
                                            iSql = "" : iSql = "Select * from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID =" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & " " 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                        'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                        '  dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit")) ' Commented by Vijayalakshmi 26/11/2019
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit")) ' + dtArray.Rows(a)("CC_TrDebit"))
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                        'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                    End If
                                                Next
                                                ' End If
                                                'Customer COA'

                                            End If
                                        End If
                                    Next

                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sAsseCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    ' If sAsseCurrentYear.StartsWith("-") = True Then
                                    'sAsseCurrentYear = sAsseCurrentYear.Remove(0, 1)
                                    '  End If
                                    dRow("LastYear") = sAsseCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    'dTotalDebit = dTotalDebit + dDbCr
                                    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sAsseCurrentYear)

                                    dDebit = 0 : dCredit = 0
                                End If

                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            If iFixedAssets = 0 Then
                                '                iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '                iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & ""

                                '                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '                If dtArray.Rows.Count > 0 Then
                                '                    For a = 0 To dtArray.Rows.Count - 1
                                '                        If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                        End If

                                '                        If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                        End If
                                '                    Next

                                '                End If
                                '            End If
                                '        End If
                                '    Next
                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sAssetLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sAssetLastYear.StartsWith("-") = True Then
                                '        sAssetLastYear = sAssetLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sAssetLastYear

                                '    Dim dLDbCr As Double = 0.00
                                '    dLDbCr = dLDebit - dLCredit
                                '    'dTotalLDebit = dTotalLDebit + dLDbCr
                                '    dTotalLDebit = dTotalLDebit + Convert.ToDouble(sAssetLastYear)

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                Dim sStrTotalCurrentdebit As String = "" : Dim sStrTotalLastDebit As String = ""
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                sStrTotalCurrentdebit = dTotalDebit
                If sStrTotalCurrentdebit.StartsWith("-") = True Then
                    sStrTotalCurrentdebit = sStrTotalCurrentdebit.Remove(0, 1)
                End If
                'dRow("PresentYear") = dTotalDebit
                dRow("PresentYear") = sStrTotalCurrentdebit

                sStrTotalLastDebit = dTotalLDebit
                If sStrTotalLastDebit.StartsWith("-") = True Then
                    sStrTotalLastDebit.Remove(0, 1)
                End If
                'dRow("LastYear") = dTotalLDebit
                dRow("LastYear") = sStrTotalLastDebit
                dt.Rows.Add(dRow)
            End If
            'Income
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_head in(0) and gl_AccHead = 2 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & "  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "REVENUE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_CompID =" & iCOmpID & " And "
                    aSql = aSql & "gl_Delflag ='C' and gl_Status ='A'  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =2 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID=" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If

                                            'Customer COA'
                                            iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                        ' dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                        'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    If sSPresentYear.StartsWith("-") = True Then
                                        sSPresentYear = sSPresentYear.Remove(0, 1)
                                    End If
                                    dRow("PresentYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dCredit - dDebit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID=" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'
                                            iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                        'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                        ' dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sSPresentYear = String.Format("{0:0.00}", Convert.ToDecimal(dCredit - dDebit))
                                    If sSPresentYear.StartsWith("-") = True Then
                                        sSPresentYear = sSPresentYear.Remove(0, 1)
                                    End If
                                    dRow("LastYear") = sSPresentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dCredit - dDebit
                                    dTotalLDebit = dTotalLDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '            iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & " "
                                '            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '            If dtArray.Rows.Count > 0 Then
                                '                For a = 0 To dtArray.Rows.Count - 1
                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                    End If

                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                    End If
                                '                Next

                                '            End If
                                '        End If
                                '    Next

                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sLastYear.StartsWith("-") = True Then
                                '        sLastYear = sLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sLastYear

                                '    Dim dLdbCr As Double = 0.00
                                '    dLdbCr = dLDebit - dLCredit
                                '    dTotalLDebit = dTotalLDebit + dLdbCr

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))

                sTotalDebit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                ' If sTotalDebit.StartsWith("-") = True Then
                ' sTotalDebit = sTotalDebit.Remove(0, 1)
                ' End If
                dRow("PresentYear") = sTotalDebit
                dPIncomeSum = Convert.ToDouble(sTotalDebit)

                sTotalCredit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                'If sTotalCredit.StartsWith("-") = True Then
                ' sTotalCredit = sTotalCredit.Remove(0, 1)
                ' End If
                dRow("LastYear") = sTotalCredit
                dLIncomeSum = Convert.ToDouble(sTotalCredit)

                dt.Rows.Add(dRow)
            End If


            'Expenditure
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_head in(0) and gl_AccHead = 3 and gl_Delflag ='C' and gl_Status ='A' and "
            sSql = sSql & "gl_CompID =" & iCOmpID & "  and gl_OrgTypeID=" & iOrgID & " order by gl_id"
            dtGroup = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EXPENDITURE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustID & " And gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " And gl_CompID =" & iCOmpID & " And "
                    aSql = aSql & "gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDBL.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =3 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & "and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'
                                            If sArray(k) = 333 Then
                                                'iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " And Opn_CompID =" & iCOmpID & " And "
                                                'iSql = iSql & "Opn_GLID =198  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                'dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                'If dtArray.Rows.Count > 0 Then
                                                '    For a = 0 To dtArray.Rows.Count - 1
                                                '        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                '            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                '            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                '        End If

                                                '        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                '            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                '            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                '        End If
                                                '    Next
                                                'End If
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_oBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_oBValues")) '+ dtArray.Rows(a)("CI_CBValues"))
                                                        End If
                                                    Next
                                                End If
                                            ElseIf sArray(k) = 334 Then
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iYearID & " And CI_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues")) '+ dtArray.Rows(a)("CI_CBValues"))
                                                        End If
                                                    Next
                                                End If
                                            Else

                                                iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iYearID & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If

                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sExCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    'If sExCurrentYear.StartsWith("-") = True Then
                                    '    sExCurrentYear = sExCurrentYear.Remove(0, 1)
                                    'End If
                                    dRow("PresentYear") = sExCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iLastYear & " and ATD_CompID =" & iCOmpID & " and ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & " and ATD_Status='A'"
                                            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                    End If
                                                Next
                                            End If
                                            'Customer COA'
                                            If sArray(k) = 333 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "Opn_GLID =198  And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt")) '+ dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount")) ' + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            ElseIf sArray(k) = 334 Then
                                                iSql = "" : iSql = "Select * from acc_changes_inventories where CI_FinancialYear =" & iLastYear & " And CI_CompID =" & iCOmpID & " And "
                                                iSql = iSql & "CI_GLID =198  And CI_CustID=" & iCustID & " And CI_OrgType=" & iOrgID & " and CI_status<>'D' "
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues")) '+ dtArray.Rows(a)("CI_CBValues"))
                                                        End If
                                                    Next
                                                End If
                                            Else

                                                iSql = "" : iSql = "Select * from Customer_COA where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_YearID=" & iLastYear & " and CC_CompID =" & iCOmpID & " and CC_Parent =" & sArray(k) & "" 'CC_GL =" & sArray(k) & " vijayalakshmi 07-12-19 chnaged cc_gl To CC_parent 
                                                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseDebit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseDebit").ToString() <> "") Then
                                                            'dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseDebit").ToString())
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit") + dtArray.Rows(a)("CC_TrDebit"))
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_CloseCredit").ToString()) = False) And (dtArray.Rows(a)("CC_CloseCredit").ToString() <> "") Then
                                                            'dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_CloseCredit").ToString())
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit") + dtArray.Rows(a)("CC_TrCredit"))
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            'Customer COA'

                                        End If
                                    Next

                                    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    sExCurrentYear = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))
                                    'If sExCurrentYear.StartsWith("-") = True Then
                                    '    sExCurrentYear = sExCurrentYear.Remove(0, 1)
                                    'End If
                                    dRow("LastYear") = sExCurrentYear

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalLDebit = dTotalLDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If


                                'sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                'If sArray.Length - 1 Then
                                '    For k = 0 To sArray.Length - 1
                                '        If sArray(k) <> "" Then
                                '            iSql = "" : iSql = "Select * from Freeze_Ledger where FL_Year =" & iLastYear & " and FL_CompID =" & iCOmpID & " and "
                                '            iSql = iSql & "FL_Status ='F' and FL_GL =" & sArray(k) & ""
                                '            dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                '            If dtArray.Rows.Count > 0 Then
                                '                For a = 0 To dtArray.Rows.Count - 1
                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrDebit").ToString()) = False) And (dtArray.Rows(a)("FL_TrDebit").ToString() <> "") Then
                                '                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("FL_TrDebit").ToString())
                                '                    End If

                                '                    If (IsDBNull(dtArray.Rows(a)("FL_TrCredit").ToString()) = False) And (dtArray.Rows(a)("FL_TrCredit").ToString() <> "") Then
                                '                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("FL_TrCredit").ToString())
                                '                    End If
                                '                Next

                                '            End If
                                '        End If
                                '    Next

                                '    'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    sExLastYear = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))
                                '    If sExLastYear.StartsWith("-") = True Then
                                '        sExLastYear = sExLastYear.Remove(0, 1)
                                '    End If
                                '    dRow("LastYear") = sExLastYear

                                '    Dim dLdbCr As Double = 0.00
                                '    dLdbCr = dLDebit - dLCredit
                                '    dTotalLDebit = dTotalLDebit + dLdbCr

                                '    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                'End If
                            End If
                            dt.Rows.Add(dRow)
                            If dtLink.Rows.Count > 0 Then
                                If dtLink.Rows(0)("SLM_NoteNo") = 36 Then
                                    dRow = dt.NewRow()
                                    dRow("Particulars") = "Changes in inventories of finished goods, work-in-progress and stock-in-trade"
                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                                    dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                            End If

                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                            End If
                                        Next
                                    End If

                                    dRow("PresentYear") = dDebit - dCredit
                                    dPCIValue = dDebit - dCredit

                                    dDebit = 0 : dCredit = 0

                                    iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                                    dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                                            End If

                                            If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                                            End If
                                        Next
                                    End If
                                    dRow("LastYear") = dDebit - dCredit
                                    dLCIValue = dDebit - dCredit

                                    dDebit = 0 : dCredit = 0
                                    dt.Rows.Add(dRow)
                                End If
                            End If

                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next
                'dRow = dt.NewRow()
                'dRow("Particulars") = "Changes in inventories of finished goods, work-in-progress and stock-in-trade"
                'iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iYearID & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                'dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                'If dtArray.Rows.Count > 0 Then
                '    For a = 0 To dtArray.Rows.Count - 1
                '        If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                '            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                '        End If

                '        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                '            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                '        End If
                '    Next
                'End If

                'dRow("PresentYear") = dDebit - dCredit
                'dPCIValue = dDebit - dCredit

                'dDebit = 0 : dCredit = 0

                'iSql = "" : iSql = "Select * from Acc_Changes_Inventories where CI_FinancialYear =" & iLastYear & " and CI_CompID =" & iCOmpID & " and CI_GLID <>198 and CI_Note=21 and CI_CustId=" & iCustID & " and CI_Orgtype=" & iOrgID & " and CI_Status<>'D'"
                'dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                'If dtArray.Rows.Count > 0 Then
                '    For a = 0 To dtArray.Rows.Count - 1
                '        If (IsDBNull(dtArray.Rows(a)("CI_OBValues").ToString()) = False) And (dtArray.Rows(a)("CI_OBValues").ToString() <> "") Then
                '            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CI_OBValues").ToString())
                '        End If

                '        If (IsDBNull(dtArray.Rows(a)("CI_CBValues").ToString()) = False) And (dtArray.Rows(a)("CI_CBValues").ToString() <> "") Then
                '            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CI_CBValues").ToString())
                '        End If
                '    Next
                'End If
                'dRow("LastYear") = dDebit - dCredit
                'dLCIValue = dDebit - dCredit

                'dDebit = 0 : dCredit = 0
                'dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                'dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                sExTotalDebit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                If sExTotalDebit.StartsWith("-") = True Then
                    sExTotalDebit = sExTotalDebit.Remove(0, 1)
                End If
                dRow("PresentYear") = sExTotalDebit + dPCIValue

                dPExpensSum = Convert.ToDouble(sExTotalDebit + dPCIValue)

                'dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                sExTotalCredit = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                If sExTotalCredit.StartsWith("-") = True Then
                    sExTotalCredit = sExTotalCredit.Remove(0, 1)
                End If
                dRow("LastYear") = sExTotalCredit + dLCIValue
                dLExpensSum = Convert.ToDouble(sExTotalCredit + dLCIValue)
                dt.Rows.Add(dRow)


                'Profit / (Loss) before exceptional and extraordinary items and tax
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit / (Loss) before exceptional and extraordinary items and tax" & "</B>"
                dRow("PresentYear") = dPIncomeSum - dPExpensSum
                Dim dPBeforeExp As Double = 0.00
                dPBeforeExp = dPIncomeSum - dPExpensSum
                'If dRow("PresentYear").StartsWith("-") = True Then
                '    dRow("PresentYear") = dRow("PresentYear").Remove(0, 1)
                'End If
                Dim dLBeforeExp As Double = 0.00
                dRow("LastYear") = dLIncomeSum - dLExpensSum
                dLBeforeExp = dLIncomeSum - dLExpensSum
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Exceptional items
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "Exceptional items"

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =1 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit
                dPExceptionalSum = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =1 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dLExceptionalSum = dDebit

                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit / (Loss) before extraordinary items and tax

                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit / (Loss) before extraordinary items and tax" & "</B>"


                dPExp = Math.Round(dPBeforeExp, 2)
                dRow("PresentYear") = dPExceptionalSum
                If dRow("PresentYear").StartsWith("-") = True Then
                    dPExceptionalSum = dRow("PresentYear").Remove(0, 1)
                    dRow("PresentYear") = dPExp + dPExceptionalSum
                    dPExpSum = dPExp + dPExceptionalSum
                Else
                    dRow("PresentYear") = dPExp - dPExceptionalSum
                    dPExpSum = dPExp - dPExceptionalSum
                End If

                dLExp = Math.Round(dLBeforeExp, 2)
                dRow("LastYear") = dLExceptionalSum
                If dRow("LastYear").StartsWith("-") = True Then
                    dLExceptionalSum = dRow("LastYear").Remove(0, 1)
                    dRow("LastYear") = dLExp + dLExceptionalSum
                    dLExpSum = dLExp + dLExceptionalSum
                Else
                    dRow("LastYear") = dLExp - dLExceptionalSum
                    dLExpSum = dLExp - dLExceptionalSum
                End If
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Extraordinary items
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "Extraordinary items"

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =2 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit
                dPExtraSum = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =2 and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dLExtraSum = dDebit

                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit/(Loss) before tax
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) before tax" & "</B>"

                dPExt = Math.Round(dPExpSum, 2)
                dRow("PresentYear") = dPExtraSum
                If dRow("PresentYear").StartsWith("-") = True Then
                    dPExtraSum = dRow("PresentYear").Remove(0, 1)
                    dRow("PresentYear") = dPExt - dPExtraSum
                    dPTaxExpSum = dPExt - dPExtraSum
                Else
                    dRow("PresentYear") = dPExt - dPExtraSum
                    dPTaxExpSum = dPExt - dPExtraSum
                End If

                dLExt = Math.Round(dLExpSum, 2)
                dRow("LastYear") = dLExtraSum
                If dRow("LastYear").StartsWith("-") = True Then
                    dLExtraSum = dRow("LastYear").Remove(0, 1)
                    dRow("LastYear") = dLExt + dLExtraSum
                    dLTaxExpSum = dLExt + dLExtraSum
                Else
                    dRow("LastYear") = dLExt - dLExtraSum
                    dLTaxExpSum = dLExt - dLExtraSum
                End If
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Tax expense/(benefit)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Tax expense/(benefit)" & "</B>"
                dt.Rows.Add(dRow)

                'Current tax expense
                dRow = dt.NewRow()
                dRow("Particulars") = "Current tax expense"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                '(Less): MAT credit (where applicable)
                dRow = dt.NewRow()
                dRow("Particulars") = "(Less): MAT credit (where applicable)"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='(Less): MAT credit (where applicable)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='(Less): MAT credit (where applicable)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Short / (Excess) provision for tax relating to prior years
                dRow = dt.NewRow()
                dRow("Particulars") = "Short / (Excess) provision for tax relating to prior years"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Short/(Excess) provision for tax relating to prior years' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Short/(Excess) provision for tax relating to prior years' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Net current tax expense
                dRow = dt.NewRow()
                dRow("Particulars") = "Net current tax expense"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Net current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Net current tax expense' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                ' Deferred tax
                dRow = dt.NewRow()
                dRow("Particulars") = "Deferred tax"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Deferred tax' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTaxExpenses = dPTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =3 and SS_Particulars='Deferred tax' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTaxExpenses = dLTaxExpenses + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Net tax expense/(benefit)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Net tax expense/(benefit)" & "</B>"

                dRow("PresentYear") = dPTaxExpenses

                dRow("LastYear") = dLTaxExpenses
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit / (Loss) from continuing operations
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) from continuing operations" & "</B>"

                dRow("PresentYear") = dPTaxExpSum - dPTaxExpenses
                dPContOperations = dPTaxExpSum - dPTaxExpenses

                dRow("LastYear") = dLTaxExpSum - dLTaxExpenses
                dLContOperations = dLTaxExpSum - dLTaxExpenses

                'dRow("PresentYear") = dPTaxExpSum + dPTaxExpenses
                'dPContOperations = dPTaxExpSum + dPTaxExpenses

                'dRow("LastYear") = dLTaxExpSum + dLTaxExpenses
                'dLContOperations = dLTaxExpSum + dLTaxExpenses
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Discontinuing Operations
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Discontinuing Operations" & "</B>"
                dt.Rows.Add(dRow)

                'Profit/(Loss) from discontinuing operations (before tax)
                dRow = dt.NewRow()
                dRow("Particulars") = "Profit/(Loss) from discontinuing operations (before tax)"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Profit/(Loss) from discontinuing operations (before tax)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Profit/(Loss) from discontinuing operations (before tax)' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Gain/(Loss) on disposal of assets / settlement of liabilities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on ordinary activities attributable to the discontinuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =4 and SS_Particulars='Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit/(Loss) from discontinuing operations
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) from discontinuing operations" & "</B>"

                dRow("PresentYear") = dPDisConOperations

                dRow("LastYear") = dLDisConOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'TOTAL OPERATIONS
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Total Operations" & "</B>"
                dt.Rows.Add(dRow)

                'Profit/(Loss) for the year
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) for the year" & "</B>"

                dRow("PresentYear") = dPContOperations - dPDisConOperations
                Dim dPResAndSur As Double = 0.00
                dPResAndSur = dPContOperations - dPDisConOperations

                dRow("LastYear") = dLContOperations - dLDisConOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                dRow = dt.NewRow()
                dt.Rows.Add(dRow)


                Try
                    iSql = "" : iSql = "select count(SS_PKID) from Acc_Seperate_Schedule where SS_Group=60 and SS_Particulars='Add: Current year profit/(loss)' and SS_Custid=" & iCustID & ""
                    iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
                    iCount = objDBL.SQLExecuteScalar(sNameSpace, iSql)
                    If iCount = 0 Then
                        Dim iMaxid As Integer = objDBL.SQLExecuteScalar(sNameSpace, "select IsNull(Max(SS_PKID) + 1,1) from Acc_Seperate_Schedule")
                        iSql = "" : iSql = "insert into Acc_Seperate_Schedule(SS_PKID,SS_FinancialYear,SS_CustId,SS_Orgtype,SS_Group,SS_Particulars,"
                        iSql = iSql & "SS_Values,SS_DATE,SS_Status,SS_Delflag,SS_CrBy,SS_CrOn,SS_UpdatedBy,SS_UpdatedOn,SS_IPAddress,SS_CompID)"
                        iSql = iSql & "values(" & iMaxid & "," & iYearID & "," & iCustID & "," & iOrgID & ",60,'Add: Current year profit/(loss)',"
                        iSql = iSql & "" & dPResAndSur & ",getdate(),'D','A'," & iUserid & ",getdate()," & iUserid & ",getdate(),'" & sIpAddress & "'," & iCOmpID & ")"
                        objDBL.SQLExecuteNonQuery(sNameSpace, iSql)
                    Else
                        iSql = "" : iSql = "update Acc_Seperate_Schedule set SS_Values=" & dPResAndSur & " where SS_Group=60 and SS_Particulars='Add: Current year profit/(loss)' and SS_Custid=" & iCustID & ""
                        iSql = iSql & "and SS_Orgtype=" & iOrgID & " and SS_Status='D' and SS_FinancialYear=" & iYearID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, iSql)
                    End If
                Catch ex As Exception
                    Throw
                End Try

                'TOTAL OPERATIONS
                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "Total Operations" & "</B>"
                dt.Rows.Add(dRow)

                'Profit/(Loss) after tax before share of profit/(loss) of associates and minority interest
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) after tax before share of profit/(loss) of associates and minority interest" & "</B>"

                dRow("PresentYear") = dPContOperations - dPDisConOperations

                dRow("LastYear") = dLContOperations - dLDisConOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Share in profit/(loss) of associates @
                dRow = dt.NewRow()
                dRow("Particulars") = "Share in profit/(loss) of associates @"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Share in profit/(loss) of associates @' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTotalOperations = dPTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Share in profit/(loss) of associates @' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTotalOperations = dLTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Minority interest
                dRow = dt.NewRow()
                dRow("Particulars") = "Minority interest"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Minority interest' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPTotalOperations = dPTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =5 and SS_Particulars='Minority interest' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLTotalOperations = dLTotalOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Profit/(Loss) for the year
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Profit/(Loss) for the year" & "</B>"

                dRow("PresentYear") = dPContOperations - dPDisConOperations + dPTotalOperations

                dRow("LastYear") = dLContOperations - dLDisConOperations + dLTotalOperations
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)

                'Earnings per share (of ` ___/- each)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Earnings per share (of ` ___/- each)" & "</B>"
                dt.Rows.Add(dRow)

                'Basic Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Basic Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Total operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =6 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)


                'Earnings per share (of ` ___/- each) (excluding extraordinary items)
                dRow = dt.NewRow()
                dRow("SLNo") = iSLNo + 1
                dRow("Particulars") = "<B>" & "Earnings per share (of ` ___/- each) (excluding extraordinary items)" & "</B>"
                dt.Rows.Add(dRow)

                'Basic Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Basic Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Total operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Basic Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Continuing operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Basic Continuing operations"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Continuing operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                dt.Rows.Add(dRow)

                'Dilute Total operations
                dRow = dt.NewRow()
                dRow("Particulars") = "Tax expense of discontinuing operations on gain/(loss) on disposal of assets/settlement of liabilities"
                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iYearID & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dPDisConOperations = dPDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If

                dRow("PresentYear") = dDebit

                dDebit = 0 : dCredit = 0

                iSql = "" : iSql = "Select * from Acc_Seperate_Schedule where SS_FinancialYear =" & iLastYear & " and SS_CompID =" & iCOmpID & " and SS_Group =7 and SS_Particulars='Dilute Total operations' and  SS_CustId=" & iCustID & " and SS_Orgtype=" & iOrgID & " and SS_Status<>'D'"
                dtArray = objDBL.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                If dtArray.Rows.Count > 0 Then
                    For a = 0 To dtArray.Rows.Count - 1
                        If (IsDBNull(dtArray.Rows(a)("SS_Values").ToString()) = False) And (dtArray.Rows(a)("SS_Values").ToString() <> "") Then
                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                            dLDisConOperations = dLDisConOperations + Convert.ToDouble(dtArray.Rows(a)("SS_Values").ToString())
                        End If
                    Next
                End If
                dRow("LastYear") = dDebit
                dDebit = 0 : dCredit = 0
                iSLNo = iSLNo + 1
                dt.Rows.Add(dRow)
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubLeadger1(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer)
        Dim sSql As String = "", cSql As String = "", sglSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable, dtGroup As New DataTable, dtSub As New DataTable, dtSub1 As New DataTable, dtSub2 As New DataTable, dtLink As New DataTable, dtArray As New DataTable, dtFA As New DataTable
        Dim dtYear As New DataTable, dtOB As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0, m As Integer = 0
        Dim sArray As Array
        Dim dDebit As Double = 0.00, dCredit As Double = 0.00
        Dim dLDebit As Double = 0.00, dLCredit As Double = 0.00
        Dim dTotalDebit As Double = 0.00, dTotalCredit As Double = 0.00
        Dim dTotalLDebit As Double = 0.00, dTotalLCredit As Double = 0.00
        Dim iHead As Integer = 0, iSLNo As Integer = 0, iLastYear As Integer = 0, iStatusCheck As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            'Liabilites

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iACID & ")"
            dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtYear.Rows.Count > 0 Then
                'dr.Read()
                iLastYear = dtYear.Rows(i).Item("YMS_ID")
            Else
                iLastYear = 0
            End If
            'sSql = "Select YMS_YEARID,YMS_ID from Year_Master where YMS_YEARID=" & iYearID & " and YMS_CompID=" & iACID & "" 'Commented vijayalakshmi 12/12/2019 this query fetching the current year
            'dtYear = objDBL.SQLExecuteDataTable(sAC, sSql)
            'If dtYear.Rows.Count > 0 Then
            '    For i = 0 To dtYear.Rows.Count - 1
            '        iLastYear = dtYear.Rows(i).Item("YMS_YEARID")
            '    Next
            'Else
            '    iLastYear = 0
            'End If

            'iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
            'dtOB = objDBL.SQLExecuteDataTable(sAC, iSql)
            'If dtOB.Rows.Count > 0 Then
            '    For j = 0 To dtOB.Rows.Count - 1
            '        If dtOB.Rows(j).Item("Opn_Status") = "F" Then
            '            iStatusCheck = 0
            '        Else
            '            iStatusCheck = 1
            '        End If
            '    Next
            'Else
            '    iStatusCheck = 1
            'End If

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<b>" & "EQUITY AND LIABILITIES" & "</b>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<b>" & dtGroup.Rows(i)("gl_Desc") & "</b>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=0 order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dt.Rows.Add(dRow)
                            ' dDebit = 0 : dCredit = 0

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If
                                cSql = "" : cSql = "Select * from Chart_of_accounts where gl_Parent = " & dtSub.Rows(j)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Status ='A' And gl_OrgTypeID=0 and gl_Delflag ='C' order by gl_id"
                                dtSub1 = objDBL.SQLExecuteDataTable(sAC, cSql)
                                If dtSub1.Rows.Count > 0 Then
                                    For v = 0 To dtSub1.Rows.Count - 1
                                        dRow = dt.NewRow()
                                        If IsDBNull(dtSub1.Rows(v)("Gl_id").ToString()) = False Then
                                            dRow("ID") = dtSub1.Rows(v)("gl_id")
                                        End If

                                        If IsDBNull(dtSub1.Rows(j)("gl_Desc").ToString()) = False Then
                                            dRow("Particulars") = dtSub1.Rows(v)("gl_Desc")
                                        End If
                                        dt.Rows.Add(dRow)

                                        sglSql = "" : sglSql = "Select * from Customer_coa where cc_Parent = " & dtSub1.Rows(v)("gl_ID") & " and cc_CompID =" & iACID & " and cc_Status ='W' And cc_IndType=" & iOrgID & "  order by cc_gl"
                                        dtSub2 = objDBL.SQLExecuteDataTable(sAC, sglSql)
                                        If dtSub2.Rows.Count > 0 Then
                                            For g = 0 To dtSub2.Rows.Count - 1
                                                dRow = dt.NewRow()
                                                If IsDBNull(dtSub2.Rows(g)("cc_gl").ToString()) = False Then
                                                    dRow("ID") = dtSub2.Rows(g)("cc_gl")
                                                End If

                                                If IsDBNull(dtSub2.Rows(g)("cc_GLDesc").ToString()) = False Then
                                                    dRow("Particulars") = dtSub2.Rows(g)("cc_GLDesc")
                                                End If
                                                dt.Rows.Add(dRow)
                                                dDebit = 0 : dCredit = 0

                                                iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iYearID & " and cc_CompID =" & iACID & " and "
                                                iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString())
                                                        End If
                                                    Next
                                                End If


                                                If (dDebit <> 0) And (dCredit <> 0) Then
                                                    dRow("PresentYear") = dCredit - dDebit
                                                ElseIf dDebit <> 0 Then
                                                    dRow("PresentYear") = dDebit
                                                ElseIf dCredit <> 0 Then
                                                    dRow("PresentYear") = dCredit
                                                ElseIf (dDebit = 0) And (dCredit = 0) Then
                                                    dRow("PresentYear") = "0.00"
                                                End If

                                                dTotalDebit = dTotalDebit + dRow("PresentYear")
                                                dDebit = 0 : dCredit = 0


                                                'Last Year

                                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")

                                                iSql = "" : iSql = "Select * from Customer_coa where cc_YearID =" & iLastYear & " and cc_CompID =" & iACID & " and "
                                                iSql = iSql & "cc_gl =" & dtSub2.Rows(g)("cc_gl") & " And cc_CustID=" & iCustID & " And CC_IndType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("CC_OBDebit").ToString()) = False) And (dtArray.Rows(a)("CC_OBDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("CC_OBDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("CC_OBCredit").ToString()) = False) And (dtArray.Rows(a)("CC_OBCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("CC_OBCredit").ToString())
                                                        End If
                                                    Next
                                                End If


                                                If (dLDebit <> 0) And (dLCredit <> 0) Then
                                                    dRow("LastYear") = dLCredit - dLDebit
                                                ElseIf dLDebit <> 0 Then
                                                    dRow("LastYear") = dLDebit
                                                ElseIf dLCredit <> 0 Then
                                                    dRow("LastYear") = dLCredit
                                                ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                                    dRow("LastYear") = "0.00"
                                                End If

                                                dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                                'End If
                                            Next
                                        End If
                                    Next
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<b>" & "TOTAL" & "</b>"
                dRow("PresentYear") = dTotalDebit 'dTotalCredit - dTotalDebit
                dRow("LastYear") = dTotalLDebit 'dTotalLCredit - dTotalLDebit
                dt.Rows.Add(dRow)
            End If


            'Assets
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 1 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<b>" & "ASSETS" & "</b>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<b>" & dtGroup.Rows(i)("gl_Desc") & "</b>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dDebit = 0 : dCredit = 0

                            If dtSub.Rows(j)("gl_Desc").ToString() = "Fixed Assets" Then
                                aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtSub.Rows(j)("gl_ID") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 order by gl_id"
                                dtFA = objDBL.SQLExecuteDataTable(sAC, aSql)
                                If dtFA.Rows.Count > 0 Then
                                    For m = 0 To dtFA.Rows.Count - 1
                                        dt.Rows.Add(dRow)
                                        dRow = dt.NewRow()

                                        If IsDBNull(dtFA.Rows(m)("gl_Desc").ToString()) = False Then
                                            dRow("Particulars") = dtFA.Rows(m)("gl_Desc")
                                        End If

                                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                                        mSql = mSql & "SLM_SUbGroupID =" & dtFA.Rows(m)("gl_Parent") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                                        dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                                        If dtLink.Rows.Count > 0 Then
                                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                            End If

                                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                            If sArray.Length - 1 Then
                                                For k = 0 To sArray.Length - 1
                                                    If sArray(k) <> "" Then
                                                        If iStatusCheck = 0 Then
                                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
                                                            iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                            If dtArray.Rows.Count > 0 Then
                                                                For a = 0 To dtArray.Rows.Count - 1
                                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                                    End If

                                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                                    End If
                                                                Next
                                                            End If
                                                        Else
                                                            iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
                                                            iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
                                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                            If dtArray.Rows.Count > 0 Then
                                                                For a = 0 To dtArray.Rows.Count - 1
                                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                                    End If

                                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                                    End If
                                                                Next

                                                            End If
                                                        End If

                                                    End If
                                                Next


                                                If (dDebit <> 0) And (dCredit <> 0) Then
                                                    dRow("PresentYear") = dCredit - dDebit
                                                ElseIf dDebit <> 0 Then
                                                    dRow("PresentYear") = dDebit
                                                ElseIf dCredit <> 0 Then
                                                    dRow("PresentYear") = dCredit
                                                ElseIf (dDebit = 0) And (dCredit = 0) Then
                                                    dRow("PresentYear") = "0.00"
                                                End If

                                                dTotalDebit = dTotalDebit + dRow("PresentYear")
                                                dDebit = 0 : dCredit = 0
                                            End If

                                            'Last Year
                                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                            If sArray.Length - 1 Then
                                                For k = 0 To sArray.Length - 1
                                                    If sArray(k) <> "" Then
                                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
                                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                        dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                        If dtArray.Rows.Count > 0 Then
                                                            For a = 0 To dtArray.Rows.Count - 1
                                                                If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                                End If

                                                                If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                                End If
                                                            Next

                                                        End If
                                                    End If
                                                Next

                                                If (dLDebit <> 0) And (dLCredit <> 0) Then
                                                    dRow("LastYear") = dLCredit - dLDebit
                                                ElseIf dLDebit <> 0 Then
                                                    dRow("LastYear") = dLDebit
                                                ElseIf dLCredit <> 0 Then
                                                    dRow("LastYear") = dLCredit
                                                ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                                    dRow("LastYear") = "0.00"
                                                End If

                                                dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                            End If
                                        End If
                                    Next
                                End If

                            Else

                                '------------------------------------------------------------------------------------------
                                mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                                mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                                dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                                If dtLink.Rows.Count > 0 Then
                                    If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                        dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                    End If

                                    sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                    If sArray.Length - 1 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                If iStatusCheck = 0 Then
                                                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
                                                    iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                            End If

                                                            If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
                                                    iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
                                                    dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                            End If

                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                            End If
                                                        Next

                                                    End If
                                                End If

                                            End If
                                        Next

                                        If (dDebit <> 0) And (dCredit <> 0) Then
                                            dRow("PresentYear") = dCredit - dDebit
                                        ElseIf dDebit <> 0 Then
                                            dRow("PresentYear") = dDebit
                                        ElseIf dCredit <> 0 Then
                                            dRow("PresentYear") = dCredit
                                        ElseIf (dDebit = 0) And (dCredit = 0) Then
                                            dRow("PresentYear") = "0.00"
                                        End If

                                        dTotalDebit = dTotalDebit + dRow("PresentYear")
                                        dDebit = 0 : dCredit = 0
                                    End If

                                    'Last Year
                                    sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                    If sArray.Length - 1 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
                                                iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                        End If
                                                    Next

                                                End If
                                            End If
                                        Next


                                        If (dLDebit <> 0) And (dLCredit <> 0) Then
                                            dRow("LastYear") = dLCredit - dLDebit
                                        ElseIf dLDebit <> 0 Then
                                            dRow("LastYear") = dLDebit
                                        ElseIf dLCredit <> 0 Then
                                            dRow("LastYear") = dLCredit
                                        ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                            dRow("LastYear") = "0.00"
                                        End If

                                        dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                        dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                    End If
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<b>" & "TOTAL" & "</b>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If




            'Income
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 2 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<b>" & "REVENUE" & "</b>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<b>" & dtGroup.Rows(i)("gl_Desc") & "</b>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=0 order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dDebit = 0 : dCredit = 0

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =2 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            End If

                                        End If
                                    Next

                                    If (dDebit <> 0) And (dCredit <> 0) Then
                                        dRow("PresentYear") = dCredit - dDebit
                                    ElseIf dDebit <> 0 Then
                                        dRow("PresentYear") = dDebit
                                    ElseIf dCredit <> 0 Then
                                        dRow("PresentYear") = dCredit
                                    ElseIf (dDebit = 0) And (dCredit = 0) Then
                                        dRow("PresentYear") = "0.00"
                                    End If

                                    dTotalDebit = dTotalDebit + dRow("PresentYear")
                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                    End If
                                                Next

                                            End If
                                        End If
                                    Next

                                    If (dLDebit <> 0) And (dLCredit <> 0) Then
                                        dRow("LastYear") = dLCredit - dLDebit
                                    ElseIf dLDebit <> 0 Then
                                        dRow("LastYear") = dLDebit
                                    ElseIf dLCredit <> 0 Then
                                        dRow("LastYear") = dLCredit
                                    ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                        dRow("LastYear") = "0.00"
                                    End If

                                    dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<b>" & "TOTAL" & "</b>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If


            'Expenditure
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 3 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iACID & " And gl_OrgTypeID=0 order by gl_id"
            dtGroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EXPENDITURE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<b>" & dtGroup.Rows(i)("gl_Desc") & "</b>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_Status ='A' And gl_OrgTypeID=0 order by gl_id"
                    dtSub = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dDebit = 0 : dCredit = 0

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =3 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iACID & " And SLM_OrgID=" & iOrgID & " and slm_custid=" & iCustID & ""
                            dtLink = objDBL.SQLExecuteDataTable(sAC, mSql)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iACID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iACID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & " and ATD_CustId=" & iCustID & " and ATD_OrgType=" & iOrgID & ""
                                                dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    Next

                                    If (dDebit <> 0) And (dCredit <> 0) Then
                                        dRow("PresentYear") = dCredit - dDebit
                                    ElseIf dDebit <> 0 Then
                                        dRow("PresentYear") = dDebit
                                    ElseIf dCredit <> 0 Then
                                        dRow("PresentYear") = dCredit
                                    ElseIf (dDebit = 0) And (dCredit = 0) Then
                                        dRow("PresentYear") = "0.00"
                                    End If

                                    dTotalDebit = dTotalDebit + dRow("PresentYear")
                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iACID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & " And Opn_CustType=" & iCustID & " And Opn_IndType=" & iOrgID & ""
                                            dtArray = objDBL.SQLExecuteDataTable(sAC, iSql)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_DebitAmt").ToString()) = False) And (dtArray.Rows(a)("Opn_DebitAmt").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_DebitAmt").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_CreditAmount").ToString()) = False) And (dtArray.Rows(a)("Opn_CreditAmount").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_CreditAmount").ToString())
                                                    End If
                                                Next
                                            End If
                                        End If
                                    Next

                                    If (dLDebit <> 0) And (dLCredit <> 0) Then
                                        dRow("LastYear") = dLCredit - dLDebit
                                    ElseIf dLDebit <> 0 Then
                                        dRow("LastYear") = dLDebit
                                    ElseIf dLCredit <> 0 Then
                                        dRow("LastYear") = dLCredit
                                    ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                        dRow("LastYear") = "0.00"
                                    End If

                                    dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
