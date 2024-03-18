Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class clsUploadLedger
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
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
    Public Function LoadYears(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select YMS_YEARID,substring(YMS_ID,3,2)+ '-' +substring(YMS_ID,8,2) As YMS_ID from YEAR_MASTER where YMS_CompId=" & iACID & " order by YMS_ID asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCustTBSelectedYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer)
        Dim sSql As String
        Dim dtDesc As New DataTable
        Dim dt As New DataTable
        Dim dtPY As New DataTable
        Dim dtCY As New DataTable
        Dim sSqlDesc As String, sSqlPy As String, sSqlCy As String
        Dim iledgerCount As Integer = 0
        Dim iYear As Integer = 0
        Dim i As Integer, j As Integer
        Dim dRow As DataRow
        Dim dpyclsC, dpyclsD, dcyclsD, dcyclsC, dtotpy, dtotcy As Double
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Description")
            dt.Columns.Add("DescID")
            dt.Columns.Add("PYOpeningDebit")
            dt.Columns.Add("PYOpeningCredit")
            dt.Columns.Add("PYTrDebit")
            dt.Columns.Add("PYTrCredit")
            dt.Columns.Add("PYClosingDebit")
            dt.Columns.Add("PYClosingCredit")
            dt.Columns.Add("CYOpeningDebit")
            dt.Columns.Add("CYOpeningCredit")
            dt.Columns.Add("CYTrDebit")
            dt.Columns.Add("CYTrCredit")
            dt.Columns.Add("CYClosingDebit")
            dt.Columns.Add("CYClosingCredit")
            dt.Columns.Add("Varience")
            dt.Columns.Add("Observation")
            dt.Columns.Add("ReviewerObservation")
            dt.Columns.Add("ClientComments")
            dt.Columns.Add("ObservationCount")
            dt.Columns.Add("AttachmentID")

            iYear = iYearID - 1

            '   sSqlDesc = "select distinct(AEU_Description) from audit_excel_upload where AEU_CustId=" & iCustId & " And AEU_compid=" & iACID & " And AEU_YEARId in (" & iYearID & "," & iYear & ") and AEU_Status='A'"
            sSqlDesc = " Select a.AEU_Description, a.aeu_cron from"
            sSqlDesc = sSqlDesc & "(select AEU_Description,aeu_cron"
            sSqlDesc = sSqlDesc & " ,ROW_NUMBER() over (partition by AEU_Description "
            sSqlDesc = sSqlDesc & "order by aeu_cron asc) RowNumber  from audit_excel_upload where AEU_CustId=" & iCustId & " And AEU_AuditId=" & iAuditID & " And AEU_compid=" & iACID & " And  AEU_YEARId in (" & iYearID & "," & iYear & ") and AEU_Status='A') a "
            sSqlDesc = sSqlDesc & "where a.RowNumber <= 1 order by a.aeu_cron asc"

            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSqlDesc)
            If dtDesc.Rows.Count > 0 Then
                For i = 0 To dtDesc.Rows.Count - 1
                    sSqlPy = "select ROW_NUMBER() OVER (ORDER BY AEU_ID ASC) AS SrNo,(Select COUNT(*) From StandardAudit_ReviewLedger_Observations Where SRO_AEU_ID=AEU_ID And SRO_FinancialYearId=" & iYearID & ") As ObservationCount,AEU_ID as DescID, "
                    sSqlPy = sSqlPy & " AEU_CustId, AEU_Description as Description, CAST(AEU_ODAmount AS DECIMAL(19, 2)) as PYOpeningDebit, CAST(AEU_OCAmount AS DECIMAL(19, 2)) as PYOpeningCredit,CAST(AEU_TRDAmount AS DECIMAL(19, 2)) as PYTrDebit,"
                    sSqlPy = sSqlPy & " AEU_TRCAmount as PYTrCredit, CAST(AEU_CDAmount AS DECIMAL(19, 2)) As PYClosingDebit, CAST(AEU_CCAmount AS DECIMAL(19, 2)) As PYClosingCredit,AEU_YEARId,ISNULL(AEU_AttachmentId,0) As AttachmentID from Audit_Excel_Upload"
                    sSqlPy = sSqlPy & " where AEU_CustId=" & iCustId & " And AEU_AuditId=" & iAuditID & " And AEU_compid=" & iACID & " And AEU_YEARId in (" & iYear & "," & iYearID & ") and AEU_Description='" & dtDesc.Rows(i)("AEU_Description") & "' and AEU_Status='A'"
                    'sSqlPy = sSqlPy & " order by AEU_ID"
                    dtPY = objDBL.SQLExecuteDataTable(sAC, sSqlPy)

                    If dtPY.Rows.Count > 0 Then
                        dRow = dt.NewRow()
                        dpyclsC = "0.00" : dpyclsD = "0.00" : dcyclsD = "0.00" : dcyclsC = "0.00" : dtotpy = "0.00" : dtotcy = "0.00"
                        If dtPY.Rows.Count = 2 Then
                            dRow("SrNo") = i + 1
                            dRow("ObservationCount") = dtPY.Rows(0)("ObservationCount")
                            dRow("Description") = dtPY.Rows(0)("Description").ToString()
                            dRow("DescID") = dtPY.Rows(0)("DescID").ToString()
                            dRow("AttachmentID") = dtPY.Rows(0)("AttachmentID").ToString()
                            For j = 0 To dtPY.Rows.Count - 1

                                If dtPY.Rows(j)("AEU_YEARId") = iYear Then
                                    If IsDBNull(dtPY.Rows(j)("PYOpeningDebit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYOpeningDebit").ToString()) = 0.00 Then
                                        dRow("PYOpeningDebit") = Convert.ToDecimal(dtPY.Rows(j)("PYOpeningDebit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("PYOpeningDebit") = "0.00"
                                        ' End If
                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYOpeningCredit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYOpeningCredit").ToString()) = 0.00 Then
                                        dRow("PYOpeningCredit") = Convert.ToDecimal(dtPY.Rows(j)("PYOpeningCredit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("PYOpeningCredit") = "0.00"
                                        'End If
                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYTrDebit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYTrDebit").ToString()) = 0.00 Then
                                        dRow("PYTrDebit") = Convert.ToDecimal(dtPY.Rows(j)("PYTrDebit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("PYTrDebit") = "0.00"
                                        ' End If
                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYTrCredit").ToString()) = False Then
                                        '  If Convert.ToDouble(dtPY.Rows(j)("PYTrCredit").ToString()) = 0.00 Then
                                        dRow("PYTrCredit") = Convert.ToDecimal(dtPY.Rows(j)("PYTrCredit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("PYTrCredit") = "0.00"
                                        '  End If

                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYClosingDebit").ToString()) = False Then
                                        '  If Convert.ToDouble(dtPY.Rows(j)("PYClosingDebit").ToString()) = 0.00 Then
                                        dRow("PYClosingDebit") = Convert.ToDecimal(dtPY.Rows(j)("PYClosingDebit").ToString()).ToString("#,##0.00")
                                        dpyclsD = dRow("PYClosingDebit")
                                    Else
                                        dRow("PYClosingDebit") = "0.00"
                                        dpyclsD = 0
                                        ' End If

                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYClosingCredit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYClosingCredit").ToString()) = 0.00 Then
                                        dRow("PYClosingCredit") = Convert.ToDecimal(dtPY.Rows(j)("PYClosingCredit").ToString()).ToString("#,##0.00")
                                        dpyclsC = dRow("PYClosingCredit")
                                    Else
                                        dRow("PYClosingCredit") = "0.00"
                                        dpyclsC = 0
                                        ' End If

                                    End If
                                    dtotpy = dpyclsC - dpyclsD
                                End If
                                If dtPY.Rows(j)("AEU_YEARId") = iYearID Then
                                    If IsDBNull(dtPY.Rows(j)("PYOpeningDebit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYOpeningDebit").ToString()) = 0.00 Then
                                        dRow("CYOpeningDebit") = Convert.ToDecimal(dtPY.Rows(j)("PYOpeningDebit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("CYOpeningDebit") = "0.00"
                                        ' End If

                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYOpeningCredit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYOpeningCredit").ToString()) = 0.00 Then
                                        dRow("CYOpeningCredit") = Convert.ToDecimal(dtPY.Rows(j)("PYOpeningCredit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("CYOpeningCredit") = "0.00"
                                        '  End If
                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYTrDebit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYTrDebit").ToString()) = 0.00 Then
                                        dRow("CYTrDebit") = Convert.ToDecimal(dtPY.Rows(j)("PYTrDebit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("CYTrDebit") = "0.00"
                                        ' End If

                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYTrCredit").ToString()) = False Then
                                        'If Convert.ToDouble(dtPY.Rows(j)("PYTrCredit").ToString()) = 0.00 Then
                                        dRow("CYTrCredit") = Convert.ToDecimal(dtPY.Rows(j)("PYTrCredit").ToString()).ToString("#,##0.00")
                                    Else
                                        dRow("CYTrCredit") = "0.00"
                                        '  End If

                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYClosingDebit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYClosingDebit").ToString()) = 0.00 Then
                                        dRow("CYClosingDebit") = Convert.ToDecimal(dtPY.Rows(j)("PYClosingDebit").ToString()).ToString("#,##0.00")
                                        dcyclsD = dRow("CYClosingDebit")
                                    Else
                                        dRow("CYClosingDebit") = "0.00"
                                        dcyclsD = 0
                                        '  End If

                                    End If
                                    If IsDBNull(dtPY.Rows(j)("PYClosingCredit").ToString()) = False Then
                                        ' If Convert.ToDouble(dtPY.Rows(j)("PYClosingCredit").ToString()) = 0.00 Then
                                        dRow("CYClosingCredit") = Convert.ToDecimal(dtPY.Rows(j)("PYClosingCredit").ToString()).ToString("#,##0.00")
                                        dcyclsC = dRow("CYClosingCredit")
                                    Else
                                        dRow("CYClosingCredit") = "0.00"
                                        dcyclsC = 0
                                        ' End If
                                    End If
                                    dtotcy = dcyclsC - dcyclsD
                                End If
                                dRow("Varience") = Convert.ToDecimal(dtotpy - dtotcy).ToString("#,##0.00")
                            Next
                        ElseIf dtPY.Rows.Count = 1 Then
                            dRow("SrNo") = i + 1
                            dRow("ObservationCount") = dtPY.Rows(0)("ObservationCount")
                            dRow("Description") = dtPY.Rows(0)("Description").ToString()
                            dRow("DescID") = dtPY.Rows(0)("DescID").ToString()
                            dRow("AttachmentID") = dtPY.Rows(0)("AttachmentID").ToString()
                            If dtPY.Rows(0)("AEU_YEARId") = iYear Then
                                If IsDBNull(dtPY.Rows(0)("PYOpeningDebit").ToString()) = False Then
                                    ' If Convert.ToDouble(dtPY.Rows(0)("PYOpeningDebit").ToString()) = 0.00 Then
                                    dRow("PYOpeningDebit") = Convert.ToDecimal(dtPY.Rows(0)("PYOpeningDebit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("PYOpeningDebit") = "0.00"
                                    'End If
                                End If

                                If IsDBNull(dtPY.Rows(0)("PYOpeningCredit").ToString()) = False Then
                                    ' If Convert.ToDouble(dtPY.Rows(0)("PYOpeningCredit").ToString()) = 0.00 Then
                                    dRow("PYOpeningCredit") = Convert.ToDecimal(dtPY.Rows(0)("PYOpeningCredit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("PYOpeningCredit") = "0.00"
                                    ' End If
                                End If

                                If IsDBNull(dtPY.Rows(0)("PYTrDebit").ToString()) = False Then
                                    'If Convert.ToDouble(dtPY.Rows(0)("PYTrDebit").ToString()) = 0.00 Then
                                    dRow("PYTrDebit") = Convert.ToDecimal(dtPY.Rows(0)("PYTrDebit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("PYTrDebit") = "0.00"
                                    ' End If
                                End If

                                If IsDBNull(dtPY.Rows(0)("PYTrCredit").ToString()) = False Then
                                    'If Convert.ToDouble(dtPY.Rows(0)("PYTrCredit").ToString()) = 0.00 Then
                                    dRow("PYTrCredit") = Convert.ToDecimal(dtPY.Rows(0)("PYTrCredit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("PYTrCredit") = "0.00"
                                    ' End If
                                End If

                                If IsDBNull(dtPY.Rows(0)("PYClosingDebit").ToString()) = False Then
                                    ' If Convert.ToDouble(dtPY.Rows(0)("PYClosingDebit").ToString()) = 0.00 Then
                                    dRow("PYClosingDebit") = Convert.ToDecimal(dtPY.Rows(0)("PYClosingDebit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("PYClosingDebit") = "0.00"
                                    ' End If
                                End If

                                If IsDBNull(dtPY.Rows(0)("PYClosingCredit").ToString()) = False Then
                                    ' If Convert.ToDouble(dtPY.Rows(0)("PYClosingCredit").ToString()) = 0.00 Then
                                    dRow("PYClosingCredit") = Convert.ToDecimal(dtPY.Rows(0)("PYClosingCredit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("PYClosingCredit") = "0.00"
                                    ' End If
                                End If

                                dRow("CYOpeningDebit") = "0.00"
                                dRow("CYOpeningCredit") = "0.00"
                                dRow("CYTrDebit") = "0.00"
                                dRow("CYTrCredit") = "0.00"
                                dRow("CYClosingDebit") = "0.00"
                                dRow("CYClosingCredit") = "0.00"

                                dRow("Varience") = dRow("PYClosingCredit")
                            End If
                            If dtPY.Rows(0)("AEU_YEARId") = iYearID Then
                                dRow("PYOpeningDebit") = "0.00"
                                dRow("PYOpeningCredit") = "0.00"
                                dRow("PYTrDebit") = "0.00"
                                dRow("PYTrCredit") = "0.00"
                                dRow("PYClosingDebit") = "0.00"
                                dRow("PYClosingCredit") = "0.00"

                                If IsDBNull(dtPY.Rows(0)("PYOpeningDebit").ToString()) = False Then
                                    ' If Convert.ToDouble(dtPY.Rows(0)("PYOpeningDebit").ToString()) = 0.00 Then
                                    dRow("CYOpeningDebit") = Convert.ToDecimal(dtPY.Rows(0)("PYOpeningDebit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("CYOpeningDebit") = "0.00"
                                    ' End If
                                End If
                                If IsDBNull(dtPY.Rows(0)("PYOpeningCredit").ToString()) = False Then
                                    ' If Convert.ToDouble(dtPY.Rows(0)("PYOpeningCredit").ToString()) = 0.00 Then
                                    dRow("CYOpeningCredit") = Convert.ToDecimal(dtPY.Rows(0)("PYOpeningCredit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("CYOpeningCredit") = "0.00"
                                    'End If
                                End If
                                If IsDBNull(dtPY.Rows(0)("PYTrDebit").ToString()) = False Then
                                    'If Convert.ToDouble(dtPY.Rows(0)("PYTrDebit").ToString()) = 0.00 Then
                                    dRow("CYTrDebit") = Convert.ToDecimal(dtPY.Rows(0)("PYTrDebit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("CYTrDebit") = "0.00"
                                    ' End If
                                End If
                                If IsDBNull(dtPY.Rows(0)("PYTrCredit").ToString()) = False Then
                                    'If Convert.ToDouble(dtPY.Rows(0)("PYTrCredit").ToString()) = 0.00 Then
                                    dRow("CYTrCredit") = Convert.ToDecimal(dtPY.Rows(0)("PYTrCredit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("CYTrCredit") = "0.00"
                                    '  End If
                                End If
                                If IsDBNull(dtPY.Rows(0)("PYClosingDebit").ToString()) = False Then
                                    'If Convert.ToDouble(dtPY.Rows(0)("PYClosingDebit").ToString()) = 0.00 Then
                                    dRow("CYClosingDebit") = Convert.ToDecimal(dtPY.Rows(0)("PYClosingDebit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("CYClosingDebit") = "0.00"
                                    'End If
                                End If
                                If IsDBNull(dtPY.Rows(0)("PYClosingCredit").ToString()) = False Then
                                    ' If Convert.ToDouble(dtPY.Rows(0)("PYClosingCredit").ToString()) = 0.00 Then
                                    dRow("CYClosingCredit") = Convert.ToDecimal(dtPY.Rows(0)("PYClosingCredit").ToString()).ToString("#,##0.00")
                                Else
                                    dRow("CYClosingCredit") = "0.00"
                                    ' End If
                                End If
                                dRow("Varience") = dRow("CYClosingCredit")
                            End If
                        End If
                        dt.Rows.Add(dRow)
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCustTBCurrentYearcount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer)
        Dim sSql As String
        Dim iPYledgerCount As Integer = 0
        Try
            sSql = "select count(AEU_ID) from audit_excel_upload where AEU_CustId=" & iCustId & " And AEU_AuditId=" & iAuditID & " And AEU_compid=" & iACID & " And AEU_YEARId =" & iYearID & " and AEU_Status='A'"
            iPYledgerCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iPYledgerCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCustTBPreviousYearcount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer)
        Dim sSql As String
        Dim iYear As Integer = 0
        Dim iCYledgerCount As Integer = 0
        Try
            iYear = iYearID - 1
            sSql = "select count(AEU_ID) from audit_excel_upload where AEU_CustId=" & iCustId & " And AEU_AuditId=" & iAuditID & " And AEU_compid=" & iACID & " And AEU_YEARId =" & iYear & " and AEU_Status='A'"
            iCYledgerCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCYledgerCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveLedgerObservationsComments(ByVal sAC As String, ByVal iACID As Integer, ByVal iLedgerID As Integer, ByVal iLevelId As Integer, ByVal iUserID As Integer, ByVal sObservationComments As String, ByVal sIPAddress As String,
                                              ByVal iIsIssueRaised As Integer, ByVal sEmailIds As String, ByVal iFYId As Integer, ByVal iAuditId As Integer, ByVal iCustId As Integer, ByVal iAuditTypeId As Integer, ByVal sStatus As String)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(MAX(SRO_PKID ) + 1,1) From StandardAudit_ReviewLedger_Observations")
            sSql = "Insert Into StandardAudit_ReviewLedger_Observations(SRO_PKID,SRO_AEU_ID,SRO_Level,SRO_ObservationBy,SRO_Observations,SRO_Date,SRO_CompID,SRO_IPAddress,SRO_IsIssueRaised,SRO_EmailIds,SRO_FinancialYearId,SRO_AuditId,SRO_CustId,SRO_AuditTypeId,SRO_Status) Values "
            sSql = sSql & "(" & iMaxID & "," & iLedgerID & "," & iLevelId & "," & iUserID & ",'" & sObservationComments & "',GETDATE()," & iACID & ",'" & sIPAddress & "'," & iIsIssueRaised & ",'" & sEmailIds & "'," & iFYId & "," & iAuditId & "," & iCustId & "," & iAuditTypeId & ",'" & sStatus & "')"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateLedgerObservationsComments(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iUserID As Integer, ByVal sObservationComments As String, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_ReviewLedger_Observations Set SRO_ObservationBy=" & iUserID & ",SRO_Observations='" & sObservationComments & "',SRO_Date=GETDATE(),SRO_IPAddress='" & sIPAddress & "' Where SRO_PKID=" & iPKID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadLedgerObservationsComments(ByVal sAC As String, ByVal iACID As Integer, ByVal iLedgerID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT Convert(Varchar(10),a.SRO_Date,103) As Date,c.USr_FullName As Name,Case When a.SRO_Level=1 then 'Auditor' When a.SRO_Level=2 then 'Reviewer' When a.SRO_Level=3 then 'Client' End Role,a.SRO_Observations As Observations,"
            sSql = sSql & " ISNULL(d.USr_FullName,'') + ' - ' + ISNULL(b.SRO_Observations,'') As ClientComments, ISNULL(d.USr_FullName,'') As ClientName"
            sSql = sSql & " FROM StandardAudit_ReviewLedger_Observations a "
            sSql = sSql & " Left Join sad_userdetails c on c.Usr_ID=a.SRO_ObservationBy "
            sSql = sSql & " Left Join StandardAudit_ReviewLedger_Observations b on a.SRO_PKID=b.SRO_IsIssueRaised And b.SRO_Level=3 And b.SRO_IsIssueRaised>1"
            sSql = sSql & " Left Join sad_userdetails d on d.Usr_ID=b.SRO_ObservationBy "
            sSql = sSql & " Where a.SRO_AEU_ID=" & iLedgerID & " And a.SRO_CompID=" & iACID & " And a.SRO_IsIssueRaised <= 1"
            sSql = sSql & " Order by a.SRO_Date Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLedgerObservationsCommentsHomepage(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iFinancialYearID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataSet
        Dim Dtdetails As New DataTable
        Try
            sSql = "SELECT 'TB Review' as Notification,e.AAS_AssignmentNo as AuditNo,f.AEU_Description as Description,Convert(Varchar(10),a.SRO_Date,103) As Date,"
            sSql = sSql & " a.SRO_Observations As Observations,d.usr_LoginName + ' - ' + d.usr_FullName as Comments_by,"
            sSql = sSql & " Case When a.SRO_Level=1 then 'Auditor' When a.SRO_Level=2 then 'Reviewer' When a.SRO_Level=3 then 'Client' End Role FROM StandardAudit_ReviewLedger_Observations a"
            sSql = sSql & " Left Join sad_userdetails c on c.Usr_ID=a.SRO_ObservationBy"
            sSql = sSql & " Left Join StandardAudit_ReviewLedger_Observations b on a.SRO_PKID=b.SRO_IsIssueRaised And b.SRO_Level=3 And b.SRO_IsIssueRaised>1"
            sSql = sSql & " Left Join sad_userdetails d on d.Usr_ID=b.SRO_ObservationBy"
            sSql = sSql & " left join AuditAssignment_Schedule e on e.AAS_ID=b.SRO_AuditId"
            sSql = sSql & " left join Audit_Excel_Upload f on f.AEU_ID = a.SRO_AEU_ID"
            sSql = sSql & " where a.SRO_AuditId= " & iAuditID & " and e.AAS_CustID= " & iCustID & " And a.SRO_CompID=" & iAcID & " And a.SRO_IsIssueRaised <= 1"
            sSql = sSql & " Order by b.SRO_Date Desc"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLedgerObservationsCommentsReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim dt As New DataTable, dtLedger As New DataTable, dtComments As New DataTable
        Dim dr As DataRow, iSlNo As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Ledger")
            dt.Columns.Add("Date")
            dt.Columns.Add("Name")
            dt.Columns.Add("Role")
            dt.Columns.Add("Observations")

            sSql = "Select Distinct(SRO_AEU_ID),AEU_Description From StandardAudit_ReviewLedger_Observations Left Join audit_excel_upload on SRO_AEU_ID=AEU_ID"
            sSql = sSql & " Where SRO_AEU_ID in (Select AEU_ID From audit_excel_upload Where AEU_CustId=" & iCustId & " And AEU_YEARId in (" & iYearId & "," & iYearId - 1 & ") And AEU_CompId=" & iACID & ")"
            sSql = sSql & " Order by SRO_AEU_ID"
            dtLedger = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtLedger.Rows.Count - 1
                iSlNo = iSlNo + 1

                dr = dt.NewRow()
                dr("SrNo") = iSlNo
                dr("Ledger") = dtLedger.Rows(i)("AEU_Description")

                sSql1 = "SELECT SRO_Date,USr_FullName,Case When SRO_Level=1 then 'Auditor' When SRO_Level=2 then 'Reviewer' When SRO_Level=3 then 'Client' End Role,SRO_Observations From"
                sSql1 = sSql1 & " StandardAudit_ReviewLedger_Observations Left Join sad_userdetails on Usr_ID=SRO_ObservationBy Where SRO_AEU_ID=" & dtLedger.Rows(i)("SRO_AEU_ID") & " Order by SRO_AEU_ID,SRO_Date"
                dtComments = objDBL.SQLExecuteDataTable(sAC, sSql1)
                For j = 0 To dtComments.Rows.Count - 1
                    If j > 0 Then : dr = dt.NewRow() : End If
                    dr("Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtComments.Rows(j)("SRO_Date"), "D")
                    dr("Name") = dtComments.Rows(j)("USr_FullName")
                    dr("Role") = dtComments.Rows(j)("Role")
                    dr("Observations") = dtComments.Rows(j)("SRO_Observations")
                    If dtComments.Rows.Count > 1 Then : dt.Rows.Add(dr) : End If
                    'If j > 0 Then : dt.Rows.Add(dr) : End If
                Next
                If dtComments.Rows.Count <= 1 Then : dt.Rows.Add(dr) : End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCULedgerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iFYID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iAuditTypeID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ISNULL(AEU_ID,0) As LedgerId,AEU_Description As LedgerName From StandardAudit_ReviewLedger_Observations "
            sSql = sSql & " Left Join audit_excel_upload On SRO_AEU_ID=AEU_ID"
            sSql = sSql & " Where SRO_FinancialYearId=" & iFYID & " And SRO_CustId=" & iCustID & " And SRO_AuditId=" & iAuditID & " And SRO_AuditTypeId=" & iAuditTypeID & " And SRO_IsIssueRaised=1 And SRO_Level in (1,2) "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCULedgerCustomerIssues(ByVal sAC As String, ByVal iACID As Integer, ByVal iFYID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iAuditTypeID As Integer, ByVal iLedgerId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ISNULL(AEU_ID,0) As LedgerId,ISNULL(AEU_Description,'') As LedgerName,ISNULL(a.SRO_PKID,0) As AuditorCommentsId,ISNULL(a.SRO_Observations,'') As AuditorComments,USr_FullName As AuditorName,ISNULL(AEU_AttachmentId,0) As AttachmentID,"
            sSql = sSql & " ISNULL((Select b.SRO_PKID From StandardAudit_ReviewLedger_Observations b Where b.SRO_IsIssueRaised=a.SRO_PKID And b.SRO_Level=3 And b.SRO_AuditId=" & iAuditID & "),0) As CustomerCommentsId,"
            sSql = sSql & " ISNULL((Select c.SRO_Observations From StandardAudit_ReviewLedger_Observations c Where c.SRO_IsIssueRaised=a.SRO_PKID And c.SRO_Level=3 And c.SRO_AuditId=" & iAuditID & "),'') As CustomerComments "
            sSql = sSql & " From StandardAudit_ReviewLedger_Observations a"
            sSql = sSql & " Left Join audit_excel_upload On a.SRO_AEU_ID=AEU_ID"
            sSql = sSql & " Left Join sad_userdetails on Usr_ID=a.SRO_ObservationBy"
            sSql = sSql & " Where a.SRO_FinancialYearId=" & iFYID & " And a.SRO_CustId=" & iCustID & " And a.SRO_AuditId=" & iAuditID & " And a.SRO_AuditTypeId=" & iAuditTypeID & " And a.SRO_IsIssueRaised=1 And a.SRO_Level in (1,2)"
            If iLedgerId > 0 Then
                sSql = sSql & " And a.SRO_AEU_ID=" & iLedgerId & ""
            End If
            sSql = sSql & " Order by AEU_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCUCheckPointDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ISNULL(SCR_CheckPointID,0) As CheckPointID,ACM_Checkpoint As CheckPointName From StandardAudit_ConductAudit_RemarksHistory "
            sSql = sSql & " Left Join AuditType_Checklist_Master On ACM_ID=SCR_CheckPointID Where SCR_SA_ID=" & iAuditID & " And SCR_IsIssueRaised=1 And SCR_RemarksType in (1,2,3) "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCUCheckPointAuditorIssues(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ISNULL(SAC_CheckPointID,0) As CheckPointId,ISNULL(SCR_SAC_ID,0) As ConductAuditCheckPointId,ISNULL(ACM_Checkpoint,'') As CheckPointName,ISNULL(a.SCR_ID,0) As AuditorCommentsId,ISNULL(a.SCR_Remarks,'') As AuditorComments,USr_FullName As AuditorName,ISNULL(SAC_AttachID,0) As AttachmentID, "
            sSql = sSql & " ISNULL((Select b.SCR_ID From StandardAudit_ConductAudit_RemarksHistory b Where b.SCR_IsIssueRaised=a.SCR_ID And b.SCR_RemarksType=4 And b.SCR_SA_ID=" & iAuditID & "),0) As CustomerCommentsId,"
            sSql = sSql & " ISNULL((Select c.SCR_Remarks From StandardAudit_ConductAudit_RemarksHistory c Where c.SCR_IsIssueRaised=a.SCR_ID And c.SCR_RemarksType=4 And c.SCR_SA_ID=" & iAuditID & "),'') As CustomerComments "
            sSql = sSql & " From StandardAudit_ConductAudit_RemarksHistory a"
            sSql = sSql & " Left Join StandardAudit_ScheduleCheckPointList On a.SCR_SAC_ID=SAC_ID "
            sSql = sSql & " Left Join AuditType_Checklist_Master on ACM_ID=SAC_CheckPointID"
            sSql = sSql & " Left Join sad_userdetails on Usr_ID=a.SCR_RemarksBy Where a.SCR_SA_ID=" & iAuditID & " And a.SCR_IsIssueRaised=1 And a.SCR_RemarksType in (1,2,3)"
            If iCheckPointId > 0 Then
                sSql = sSql & " And a.SCR_CheckPointID=" & iCheckPointId & ""
            End If
            sSql = sSql & " Order by SAC_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveCUCheckPointCustomerIssues(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iConductAuditCheckPointId As Integer, ByVal iCheckPointID As Integer, ByVal iRemarksType As Integer, ByVal sRemarks As String,
                                              ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iIsIssueRaised As Integer, ByVal sEmailIds As String)
        Dim sSql As String
        Try
            Dim iMaxId As Integer = objDBL.SQLExecuteScalar(sAc, "select IsNull(Max(SCR_ID) + 1,1) from StandardAudit_ConductAudit_RemarksHistory")
            sSql = "" : sSql = "Insert into StandardAudit_ConductAudit_RemarksHistory (SCR_ID,SCR_SA_ID,SCR_SAC_ID,SCR_CheckPointID,SCR_RemarksType,SCR_Remarks,SCR_RemarksBy,SCR_Date,SCR_IPAddress,SCR_CompID,SCR_IsIssueRaised,SCR_EmailIds) Values"
            sSql = sSql & "(" & iMaxId & "," & iAuditID & "," & iConductAuditCheckPointId & "," & iCheckPointID & "," & iRemarksType & ",'" & sRemarks & "'," & iUserID & ",GetDate(),'" & sIPAddress & "'," & iAcID & "," & iIsIssueRaised & ",'" & sEmailIds & "')"
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateCUCheckPointCustomerIssues(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iPKID As Integer, ByVal sRemarks As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_ConductAudit_RemarksHistory Set SCR_RemarksBy=" & iUserID & ",SCR_Remarks='" & sRemarks & "',SCR_Date=GETDATE(),SCR_IPAddress='" & sIPAddress & "' Where SCR_ID=" & iPKID & " And SCR_SA_ID=" & iAuditID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
