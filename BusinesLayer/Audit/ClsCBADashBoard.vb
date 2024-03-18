Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class ClsCBADashBoard
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsAuditscore As New ClsAuditScore
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function LoadDashborad(ByVal sNameSpace As String, ByVal sStatus As String, ByVal iACID As Integer, ByVal iyearID As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim DtCD As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("PKID")
        dtDisplay.Columns.Add("Customer")
        dtDisplay.Columns.Add("Auditor")
        dtDisplay.Columns.Add("Month")
        dtDisplay.Columns.Add("Date")
        dtDisplay.Columns.Add("Score")
        dtDisplay.Columns.Add("Section")
        dtDisplay.Columns.Add("SectionID")
        dtDisplay.Columns.Add("Rating")
        Try
            sSql = "select CA_PKID,b.cust_name,CA_AUDITORNAME,CA_Date,CA_NetScore,c.CAS_SECTIONNAME as Sectionname,CA_SECTIONID, d.AUD_MonthID as audmonth from CRPA_AuditAssest a, "
            sSql = sSql & "SAD_CUSTOMER_MASTER b,CRPA_Section C , Audit_Schedule d"
            sSql = sSql & " WHERE b.cust_ID=a.CA_LOCATIONID and a.CA_SECTIONID = C.CAS_ID and d.AUD_ID=a.CA_AsgNo and CA_STATUS = '" & sStatus & "' and CA_Compid ='" & iACID & "'"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("CA_PKID")) = False Then
                        dRow("PKID") = dr("CA_PKID")
                    End If
                    If IsDBNull(dr("cust_name")) = False Then
                        dRow("Customer") = dr("cust_name")
                    End If
                    If IsDBNull(dr("CA_AUDITORNAME")) = False Then
                        dRow("Auditor") = dr("CA_AUDITORNAME")
                    End If
                    If IsDBNull(dr("audmonth")) = False Then
                        dRow("Month") = objclsGeneralFunctions.GetMonthNameFromMothID(dr("audmonth"))
                    End If
                    If IsDBNull(dr("CA_Date")) = False Then
                        dRow("Date") = dr("CA_Date").ToString.Substring(0, 10)
                    End If
                    If IsDBNull(dr("CA_NetScore")) = False Then
                        dRow("Score") = dr("CA_NetScore")
                    End If
                    If IsDBNull(dr("Sectionname")) = False Then
                        dRow("Section") = dr("Sectionname")
                    End If
                    If IsDBNull(dr("CA_SECTIONID")) = False Then
                        dRow("SectionID") = dr("CA_SECTIONID")
                    End If

                    DtCD = objclsAuditscore.GetColorAndRange(sNameSpace, iACID, iyearID, dRow("Score"), dRow("SectionID"))
                    If IsDBNull(DtCD.Rows(0)("CRAT_Name")) = False Then
                        dRow("Rating") = DtCD.Rows(0)("CRAT_Name").ToString()
                    End If

                    i = i + 1
                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPOPUP(ByVal sNameSpace As String, ByVal iAUDITID As Integer, ByVal iACID As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("Section")
        dtDisplay.Columns.Add("SubSection")
        dtDisplay.Columns.Add("Process")
        dtDisplay.Columns.Add("SubProcess")
        dtDisplay.Columns.Add("Finding")
        dtDisplay.Columns.Add("Standard")
        dtDisplay.Columns.Add("Result")
        dtDisplay.Columns.Add("AuditComment")

        Try
            sSql = "select  e.CASP_SUBPROCESSNAME as SubProcess,b.CAS_SECTIONNAME as Sectionname,"
            sSql = sSql & " a.CRAD_FINDINGS,a.CRAD_SCORE_STANDARD,a.CRAD_SCORE_RESULT,a.CRAD_COMMENTS,a.CRAD_PKID,CASU_SUBSECTIONNAME as SubSectionName,d.CAP_PROCESSNAME as "
            sSql = sSql & " Processname from CRPA_ChecklistAuditAssest a,CRPA_Section b,CRPA_SubSection c, CRPA_Process d,CRPA_SubProcess "
            sSql = sSql & "e where CRAD_CAuditID ='" & iAUDITID & "' and a.CRAD_SECTIONID =b.CAS_ID and a.CRAD_SUBSECTIONID =c.CASU_ID"
            sSql = sSql & " and a.CRAD_PROCESSID = d.CAP_ID AND A.CRAD_SUBPROCESSID = E.CASP_ID  and CASP_CompId ='" & iACID & "'"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("SectionName")) = False Then
                        dRow("Section") = dr("SectionName")
                    End If
                    If IsDBNull(dr("SubSectionName")) = False Then
                        dRow("SubSection") = dr("SubSectionName")
                    End If
                    If IsDBNull(dr("Processname")) = False Then
                        dRow("Process") = dr("Processname")
                    End If
                    If IsDBNull(dr("SubProcess")) = False Then
                        dRow("SubProcess") = dr("SubProcess")
                    End If
                    If IsDBNull(dr("CRAD_SCORE_STANDARD")) = False Then
                        dRow("Standard") = dr("CRAD_SCORE_STANDARD")
                    End If
                    If IsDBNull(dr("CRAD_SCORE_RESULT")) = False Then
                        dRow("Result") = dr("CRAD_SCORE_RESULT")
                    End If
                    If IsDBNull(dr("CRAD_FINDINGS")) = False Then
                        dRow("Finding") = dr("CRAD_FINDINGS")
                        If dRow("Finding") = 0 Then
                            dRow("Finding") = "Fully Complied"
                        ElseIf dRow("Finding") = 1 Then
                            dRow("Finding") = "Complied"
                        ElseIf dRow("Finding") = 2 Then
                            dRow("Finding") = "Not Applicable"
                        ElseIf dRow("Finding") = 3 Then
                            dRow("Finding") = "Not Complied"
                        End If
                    End If
                    If IsDBNull(dr("CRAD_COMMENTS")) = False Then
                        dRow("AuditComment") = dr("CRAD_COMMENTS")
                    End If
                    i = i + 1
                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNonComplianceCount(ByVal sNameSpace As String, ByVal iAUDITID As Integer, ByVal ISubsectionID As Integer, ByVal iACID As Integer, ByVal iProcessID As Integer)
        Dim ssql As String
        Try
            ssql = "select count(CRAD_SCORE_RESULT) as CRAD_SCORE_RESULT from CRPA_ChecklistAuditAssest where CRAD_CAuditID ='" & iAUDITID & "' and crad_score_result=0 and CRAD_Subsectionid='" & ISubsectionID & "' and CRAD_PROCESSID='" & iProcessID & "' and CRAD_CompId ='" & iACID & "'"
            Return (objDBL.SQLExecuteScalar(sNameSpace, ssql))
        Catch ex As Exception
        End Try
    End Function
    Public Function LoadPOPUPProcess(ByVal sNameSpace As String, ByVal iAUDITID As Integer, ByVal ISubsectionID As Integer, ByVal iACID As Integer) As DataTable
        Dim dtDisplay As New DataTable, dtDisplay2 As New DataTable, dtDisplay3 As New DataTable
        Dim i As Integer = 1, j As Integer = 0, m As Integer = 0
        Dim dRow As DataRow
        Dim sSql As String, sSql2 As String, sSql4 As String
        Dim dr As OleDb.OleDbDataReader
        Dim dGrandTotal As Double = 0

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("Process")
        dtDisplay.Columns.Add("ProcessID")
        dtDisplay.Columns.Add("STDScore")
        dtDisplay.Columns.Add("AuditScore")
        dtDisplay.Columns.Add("AuditPercentage")
        dtDisplay.Columns.Add("AuditID")
        dtDisplay.Columns.Add("NotComplied")

        Try
            sSql = "select  distinct(CAP_PROCESSNAME),CRAD_PROCESSID, CAP_POINTS from CRPA_ChecklistAuditAssest a,CRPA_Process b"
            sSql = sSql & " where CRAD_CAuditID ='" & iAUDITID & "' and CRAD_Subsectionid='" & ISubsectionID & "' and CRAD_CompId ='" & iACID & "' and a.CRAD_PROCESSID=b.CAP_ID"
            sSql = sSql & " order by CRAD_PROCESSID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            sSql2 = "select sum(CRAD_SCORE_RESULT) as CRAD_SCORE_RESULT from CRPA_ChecklistAuditAssest where CRAD_CAuditID ='" & iAUDITID & "' and CRAD_Subsectionid='" & ISubsectionID & "' and CRAD_CompId ='" & iACID & "'"
            sSql2 = sSql2 & " group by CRAD_processID"
            dtDisplay2 = objDBL.SQLExecuteDataTable(sNameSpace, sSql2)

            'sSql4 = "select count(CRAD_SCORE_RESULT) as CRAD_SCORE_RESULT from CRPA_ChecklistAuditAssest where CRAD_CAuditID ='" & iAUDITID & "' and crad_score_result=0 and CRAD_Subsectionid='" & ISubsectionID & "' and CRAD_CompId ='" & iACID & "'"
            'sSql4 = sSql4 & " group by CRAD_processID"
            'dtDisplay3 = objDBL.SQLExecuteDataTable(sNameSpace, sSql4)  ' Not Complied

            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("CAP_PROCESSNAME")) = False Then
                        dRow("Process") = dr("CAP_PROCESSNAME")
                    End If

                    If IsDBNull(dr("CRAD_PROCESSID")) = False Then
                        dRow("ProcessID") = dr("CRAD_PROCESSID")
                    End If

                    If IsDBNull(dr("CAP_POINTS")) = False Then
                        dRow("STDScore") = dr("CAP_POINTS")
                    End If

                    'If dtDisplay3.Rows.Count <= 0 Then
                    '    dRow("NotComplied") = 0
                    'Else
                    '    If IsDBNull(dtDisplay3.Rows(m)("CRAD_SCORE_RESULT")) = False Then
                    '        dRow("NotComplied") = dtDisplay3.Rows(m)("CRAD_SCORE_RESULT")
                    '    End If
                    'End If
                    dRow("NotComplied") = GetNonComplianceCount(sNameSpace, iAUDITID, ISubsectionID, iACID, dRow("ProcessID"))


                    If IsDBNull(dtDisplay2.Rows(j)("CRAD_SCORE_RESULT")) = False Then
                        dRow("AuditScore") = dtDisplay2.Rows(j)("CRAD_SCORE_RESULT")
                    End If

                    dGrandTotal = ((dRow("AuditScore") / dRow("STDScore")) * 100)
                    dGrandTotal = Math.Round(dGrandTotal)

                    dRow("AuditPercentage") = dGrandTotal


                    dRow("AuditID") = iAUDITID


                    i = i + 1
                    j = j + 1
                    ' m = m + 1

                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPOPUPSUBProcess(ByVal sNameSpace As String, ByVal iAUDITID As Integer, ByVal iProcessID As Integer, ByVal iACID As Integer) As DataTable
        Dim dtDisplay As New DataTable, dtDisplay2 As New DataTable, dtDisplay3 As New DataTable
        Dim i As Integer = 1, j As Integer = 0
        Dim dRow As DataRow
        Dim sSql As String, sSql2 As String
        Dim dr As OleDb.OleDbDataReader
        Dim dGrandTotal As Double = 0

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("SubProcess")
        dtDisplay.Columns.Add("STDScore")
        dtDisplay.Columns.Add("AuditScore")
        dtDisplay.Columns.Add("AuditPercentage")

        Try
            sSql = "select  distinct(CASP_SUBPROCESSNAME), CRAD_SubPROCESSID, CASP_POINTS from CRPA_ChecklistAuditAssest a,CRPA_SubProcess b"
            sSql = sSql & " where CRAD_CAuditID ='" & iAUDITID & "' and CRAD_PROCESSID ='" & iProcessID & "' and CRAD_CompId ='" & iACID & "' and a.CRAD_SUBPROCESSID=b.CASP_ID"
            sSql = sSql & " order by CRAD_SubPROCESSID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            sSql2 = "select sum(CRAD_SCORE_RESULT) as CRAD_SCORE_RESULT from CRPA_ChecklistAuditAssest where CRAD_CAuditID ='" & iAUDITID & "' and CRAD_PROCESSID='" & iProcessID & "' and CRAD_CompId ='" & iACID & "'"
            sSql2 = sSql2 & " group by CRAD_SUBPROCESSID"
            dtDisplay2 = objDBL.SQLExecuteDataTable(sNameSpace, sSql2)


            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("CASP_SUBPROCESSNAME")) = False Then
                        dRow("SubProcess") = dr("CASP_SUBPROCESSNAME")
                    End If

                    If IsDBNull(dr("CASP_POINTS")) = False Then
                        dRow("STDScore") = dr("CASP_POINTS")
                    End If

                    If IsDBNull(dtDisplay2.Rows(j)("CRAD_SCORE_RESULT")) = False Then
                        dRow("AuditScore") = dtDisplay2.Rows(j)("CRAD_SCORE_RESULT")
                    End If

                    dGrandTotal = ((dRow("AuditScore") / dRow("STDScore")) * 100)
                    dGrandTotal = Math.Round(dGrandTotal)

                    dRow("AuditPercentage") = dGrandTotal

                    i = i + 1
                    j = j + 1

                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPOPUPSUBSection(ByVal sNameSpace As String, ByVal iAUDITID As Integer, ByVal iProcessID As Integer, ByVal iACID As Integer) As DataTable
        Dim dtDisplay As New DataTable, dtDisplay2 As New DataTable, dtDisplay3 As New DataTable
        Dim i As Integer = 1, j As Integer = 0
        Dim dRow As DataRow
        Dim sSql As String, sSql2 As String
        Dim dr As OleDb.OleDbDataReader
        Dim dGrandTotal As Double = 0

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("SubSection")
        dtDisplay.Columns.Add("STDScore")
        dtDisplay.Columns.Add("AuditScore")
        dtDisplay.Columns.Add("AuditPercentage")
        dtDisplay.Columns.Add("SubSectionID")
        dtDisplay.Columns.Add("AuditID")

        Try
            sSql = "select  distinct(CASU_SUBSECTIONNAME), CRAD_SECTIONID, CASu_POINTS,CRAD_SUBSECTIONID from CRPA_ChecklistAuditAssest a,CRPA_SubSection b"
            sSql = sSql & " where CRAD_CAuditID ='" & iAUDITID & "' and CRAD_SECTIONID ='" & iProcessID & "' and CRAD_CompId ='" & iACID & "' and a.CRAD_SUBSECTIONID=b.CAsu_ID"
            sSql = sSql & " order by CRAD_SUBSECTIONID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            sSql2 = "select sum(CRAD_SCORE_RESULT) as CRAD_SCORE_RESULT from CRPA_ChecklistAuditAssest where CRAD_CAuditID ='" & iAUDITID & "' and CRAD_SECTIONID='" & iProcessID & "' and CRAD_CompId ='" & iACID & "'"
            sSql2 = sSql2 & " group by CRAD_subsectionid"
            dtDisplay2 = objDBL.SQLExecuteDataTable(sNameSpace, sSql2)


            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("CASU_SUBSECTIONNAME")) = False Then
                        dRow("SubSection") = dr("CASU_SUBSECTIONNAME")
                    End If

                    If IsDBNull(dr("CASu_POINTS")) = False Then
                        dRow("STDScore") = dr("CASu_POINTS")
                    End If

                    If IsDBNull(dtDisplay2.Rows(j)("CRAD_SCORE_RESULT")) = False Then
                        dRow("AuditScore") = dtDisplay2.Rows(j)("CRAD_SCORE_RESULT")
                    End If

                    dGrandTotal = ((dRow("AuditScore") / dRow("STDScore")) * 100)
                    dGrandTotal = Math.Round(dGrandTotal)

                    dRow("AuditPercentage") = dGrandTotal

                    If IsDBNull(dr("CRAD_SUBSECTIONID")) = False Then
                        dRow("SubSectionID") = dr("CRAD_SUBSECTIONID")
                    End If

                    dRow("AuditID") = iAUDITID

                    i = i + 1
                    j = j + 1

                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadNotComplied(ByVal sNameSpace As String, ByVal iAUDITID As Integer, ByVal iProcessID As Integer, ByVal iACID As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("Processname")
        dtDisplay.Columns.Add("Subprocessname")

        Try
            sSql = "select b.CAP_PROCESSNAME as PROCESSNAME, c.CASP_SUBPROCESSNAME as SUBPROCESSNAME from CRPA_ChecklistAuditAssest a, CRPA_Process b, CRPA_SubProcess c"
            sSql = sSql & " WHERE CRAD_CAuditID ='" & iAUDITID & "' and CRAD_PROCESSID='" & iProcessID & "' and crad_score_result=0 and a.CRAD_PROCESSID=b.CAP_ID and a.CRAD_SUBPROCESSID=c.CASP_ID AND a.CRAD_CompID ='" & iACID & "'"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("PROCESSNAME")) = False Then
                        dRow("Processname") = dr("PROCESSNAME")
                    End If
                    If IsDBNull(dr("SUBPROCESSNAME")) = False Then
                        dRow("Subprocessname") = dr("SUBPROCESSNAME")
                    End If
                    i = i + 1
                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAuditorRatingNames(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal Isectionid As Integer, ByVal Imonth As Integer, ByVal Iyearid As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dt As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim iCAID As Integer = 0
        dtDisplay.Columns.Add("Id")
        dtDisplay.Columns.Add("Rating_Name")
        Try
            sSql = "Select cvr_point, cvr_name as Rating_Name from crpa_valuerating"
            sSql = sSql & " where cvr_auditid= " & Isectionid & " And CVR_YearID =" & Iyearid & "And CVR_FLAG ='A' group by cvr_point,cvr_name"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    If IsDBNull(dt.Rows(i)("cvr_point")) = False Then
                        dRow("Id") = Val(dt.Rows(i)("cvr_point"))
                    End If
                    'If IsDBNull(dt.Rows(i)("Rating_Name")) = False Then
                    '    dRow("Rating_Name") = dt.Rows(i)("Rating_Name")
                    'End If
                    If Isectionid = 6 Then
                        If IsDBNull(dt.Rows(i)("Rating_Name")) = False Then
                            If Val(dt.Rows(i)("cvr_point")) = 0 Then
                                dRow("Rating_Name") = "NA"
                            ElseIf Val(dt.Rows(i)("cvr_point")) = 1 Then
                                dRow("Rating_Name") = "Developing"
                            ElseIf Val(dt.Rows(i)("cvr_point")) = 2 Then
                                dRow("Rating_Name") = "Meeting"
                            ElseIf Val(dt.Rows(i)("cvr_point")) = 3 Then
                                dRow("Rating_Name") = "Exceeding"
                            End If
                        Else
                            dRow("Rating_Name") = ""
                        End If
                    Else
                        If IsDBNull(dt.Rows(i)("Rating_Name")) = False Then
                            dRow("Rating_Name") = dt.Rows(i)("Rating_Name")
                        End If
                    End If
                    If IsDBNull(dRow("Rating_Name")) = True Then
                        dRow("Rating_Name") = 5
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAuditorSubprocess(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal Isectionid As Integer, ByVal Imonth As Integer, ByVal Iyearid As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dt As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        dtDisplay.Columns.Add("Id")
        dtDisplay.Columns.Add("Subprocess")
        Try

            sSql = "select cvr_id,cvr_name as Subprocess from crpa_valuerating"
            sSql = sSql & " where cvr_auditid= " & Isectionid & " and CVR_YearID =" & Iyearid & " group by cvr_id,cvr_name"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    'dRow("SrNo") = i + 1
                    If IsDBNull(dt.Rows(i)("cvr_id")) = False Then
                        dRow("Id") = dt.Rows(i)("cvr_id")
                    End If
                    If IsDBNull(dt.Rows(i)("Rating_Name")) = False Then
                        dRow("Subprocess") = dt.Rows(i)("Rating_Name")
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReportChart1(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal Isectionid As Integer, ByVal Imonth As Integer, ByVal iCustId As Integer, ByVal Iyearid As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim dtDisplay2 As New DataTable
        Dim i As Integer = 0, iCAID As Integer = 0
        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim sSql As String
        Dim sSql2 As String
        Dim IAuditid As Integer
        Dim dRow As DataRow
        Dim findings_count As Integer = 0
        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("Findings")
        dtDisplay.Columns.Add("SUBPROCESSNAME")

        Try
            'sSql = "" : sSql = "select CA_PKID from CRPA_AuditAssest as a"
            'sSql = sSql & " left Join audit_schedule b on b.aud_id=a.CA_AsgNo"
            'sSql = sSql & " where CA_FinancialYear=" & Iyearid & " And CA_SECTIONID=" & Isectionid & " and CA_LOCATIONID=" & iCustId & " and AUD_MonthID=" & Imonth & ""
            'iCAID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            'If Isectionid = 5 Then
            '    sSql = "" : sSql = "select distinct(b.CRAD_SUBsectionid),b.CRAD_SECTIONID, b.CRAD_SCORE_STANDARd,b.CRAD_FINDINGS as Findings,c.CVR_name as CVR_NAME,c.cvr_point,d.CASU_SUBSECTIONNAME as SubprocessName from audit_schedule a"
            '    sSql = sSql & " left join CRPA_ChecklistAuditAssest b on b.CRAD_SECTIONID= a.aud_sectionid "
            '    sSql = sSql & " left join crpa_subsection d on d.CASU_id = b.CRAD_Sectionid"
            '    sSql = sSql & " left join crpa_valuerating c on c.CVR_Id=b.crad_findings where a.aud_sectionid = " & Isectionid & " and a.AUD_kitchenID=" & iCustId & ""
            '    sSql = sSql & " and CRAD_CAuditID= " & iCAID & " and a.AUD_MonthID =" & Imonth & " and a.Aud_yearid=" & Iyearid & ""
            '    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            'Else
            '    'sSql = "" : sSql = "select b.CRAD_SECTIONID,b.CRAD_SUBPROCESSID, b.CRAD_SCORE_STANDARd,b.CRAD_FINDINGS as Findings,c.CVR_name as CVR_NAME,c.cvr_point,d.CASP_SUBPROCESSNAME as SubprocessName from audit_schedule a "
            '    'sSql = sSql & " left join CRPA_ChecklistAuditAssest b on b.CRAD_SECTIONID= a.aud_sectionid "
            '    'sSql = sSql & " left join crpa_subprocess d on d.CASP_id = b.CRAD_SUBPROCESSID "
            '    'sSql = sSql & " left join crpa_valuerating c on c.CVR_Id=b.crad_findings where a.aud_sectionid = " & Isectionid & " and a.AUD_kitchenID=" & iCustId & ""
            '    'sSql = sSql & " and CRAD_CAuditID= " & iCAID & " and a.AUD_MonthID =" & Imonth & " and a.Aud_yearid=" & Iyearid & " order by  CRAD_PKID asc"
            '    'dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            '    sSql = "" : sSql = "select b.CRAD_SECTIONID,b.CRAD_SUBPROCESSID, b.CRAD_SCORE_STANDARd,b.CRAD_FINDINGS as Findings,c.CVR_name as CVR_NAME,c.cvr_point,d.CASU_SUBSECTIONNAME as SubSectionName  from audit_schedule a "
            '    sSql = sSql & " left join CRPA_ChecklistAuditAssest b on b.CRAD_SECTIONID= a.aud_sectionid "
            '    sSql = sSql & " left join CRPA_SubSection d on d.Casu_id = b.CRAD_SUBSectionid"
            '    sSql = sSql & " left join crpa_valuerating c on c.CVR_Id=b.crad_findings where a.aud_sectionid = " & Isectionid & " and a.AUD_kitchenID=" & iCustId & ""
            '    sSql = sSql & " and CRAD_CAuditID= " & iCAID & " and a.AUD_MonthID =" & Imonth & " and a.Aud_yearid=" & Iyearid & " order by  CRAD_PKID asc"
            '    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            'End If


            'sSql2 = "select count(distinct( b.CRAD_FINDINGS)) as findings_count, c.CVR_name from audit_schedule a"
            'sSql2 = sSql2 & " left join CRPA_ChecklistAuditAssest b on b.CRAD_SECTIONID= a.aud_sectionid "
            'sSql2 = sSql2 & " left join crpa_subprocess d on d.CASP_id = b.CRAD_SUBPROCESSID "
            'sSql2 = sSql2 & " left join crpa_valuerating c on c.CVR_Id=b.crad_findings where a.aud_sectionid = " & Isectionid & " and a.AUD_kitchenID=" & iCustId & ""
            'sSql2 = sSql2 & " and CRAD_CAuditID= " & iCAID & " and a.AUD_MonthID =" & Imonth & " and a.Aud_yearid=" & Iyearid & "  group by b.CRAD_FINDINGS, c.CVR_name"
            'dt2 = objDBL.SQLExecuteDataTable(sNameSpace, sSql2)


            'Dim strarray(dt2.Rows.Count - 1) As String


            'For i = 0 To dt2.Rows.Count - 1
            '    strarray(i) = dt2.Rows(i)("CVR_NAME").ToString
            'Next
            'Dim strarray2(dt2.Rows.Count - 1) As String

            'strarray2 = strarray


            'If dt.Rows.Count > 0 Then
            '    For i = 0 To dt.Rows.Count - 1
            '        dRow = dtDisplay.NewRow
            '        dRow("SrNo") = i + 1
            '        If IsDBNull(dt.Rows(i)("CVR_NAME")) = False Then
            '            For j = 0 To dt2.Rows.Count - 1
            '                If dt.Rows(i)("CVR_NAME").ToString = strarray(j).ToString Then
            '                    dRow("Findings") = Val(dt.Rows(i)("Findings")) * 10
            '                End If
            '            Next
            '        End If
            '        If IsDBNull(dRow("Findings")) = True Then
            '            dRow("Findings") = 5
            '        End If
            '        If IsDBNull(dt.Rows(i)("SubSectionName")) = False Then
            '            Dim stringname As String = dt.Rows(i)("SubSectionName").ToString
            '            stringname = stringname.Replace("""", "'").Trim()
            '            dRow("SubSectionName") = stringname
            '        End If

            '        dtDisplay.Rows.Add(dRow)
            '    Next
            'End If

            sSql = "" : sSql = "select CA_PKID from CBA_AuditAssest "
            sSql = sSql & " where CA_AsgNo=(select AUD_ID from CBAAudit_schedule"
            sSql = sSql & " where AUD_YearID=19 and AUD_MonthID=" & Imonth & " and AUD_KitchenID=" & iCustId & " and AUD_SectionID=" & Isectionid & " )"
            IAuditid = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            'subsection
            'sSql = "" : sSql = "select CASU_SUBSECTIONNAME,MIN(b.CRAD_FINDINGS)*10 as Result, b.CRAD_SUBSECTIONID from crpa_subsection"
            'sSql = sSql & " left join CBA_ChecklistAuditAssest b on b.CRAD_SUBSECTIONID=CASU_ID and b.CRAD_CAuditID=" & IAuditid & " and b.CRAD_Locationid=" & iCustId & ""
            'sSql = sSql & " where CASU_SECTIONID=" & Isectionid & " and CASU_CompId= " & iACID & " group by CASU_SUBSECTIONNAME,CRAD_SUBSECTIONID order by CRAD_SUBSECTIONID"
            'dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            sSql = "" : sSql = "select CASP_SUBPROCESSNAME,MIN(b.CRAD_SCORE_RESULT) as Result,b.CRAD_SUBProcessID from CRPA_SubProcess"
            sSql = sSql & " left join CBA_ChecklistAuditAssest b on b.CRAD_SUBProcessID=CASP_ID and b.CRAD_CAuditID=" & IAuditid & " and b.CRAD_Locationid=" & iCustId & ""
            sSql = sSql & " where CRAD_SECTIONID=" & Isectionid & " and CASP_CompId= " & iACID & " group by CASP_SUBPROCESSNAME,CRAD_SUBProcessID order by CRAD_SUBProcessID"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

            For i = 0 To dt.Rows.Count - 1
                dRow = dtDisplay.NewRow
                dRow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("CASP_SUBPROCESSNAME")) = False Then
                    dRow("SUBPROCESSNAME") = dt.Rows(i)("CASP_SUBPROCESSNAME")
                Else
                    dRow("SUBPROCESSNAME") = 0
                End If
                If IsDBNull(dt.Rows(i)("Result")) = False Then
                    dRow("Findings") = dt.Rows(i)("Result")
                Else
                    dRow("Findings") = 0
                End If
                dtDisplay.Rows.Add(dRow)
            Next
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReportChart2(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal Isectionid As Integer, ByVal Imonth As Integer, ByVal iCustId As Integer, ByVal Iyearid As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim dtDisplay2 As New DataTable
        Dim i As Integer = 0, iCAID As Integer = 0
        Dim dt As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        dtDisplay.Columns.Add("Ratingname")
        dtDisplay.Columns.Add("TotalCount")
        Try

            sSql = "select distinct(Aud_id) from CBAAudit_schedule a "
            sSql = sSql & " left Join CBA_ChecklistAuditAssest b On b.CRAD_CAuditID=a.aud_id "
            sSql = sSql & " where CRAD_YEARID=" & Iyearid & " And CRAD_SECTIONID=" & Isectionid & " And CRAD_LOCATIONID=" & iCustId & " And AUD_MonthID=" & Imonth & ""
            iCAID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            'sSql = "" : sSql = "Select b.cvr_id, b.cvr_name as Ratingname, count(a.crad_findings) As TotalCount from CBA_ChecklistAuditAssest a"
            'sSql = sSql & " join crpa_valuerating b On a.crad_sectionid = b.cvr_auditid  And b.cvr_id=a.crad_findings"
            'sSql = sSql & " join CBAAudit_schedule c On c.aud_sectionid = a.CRAD_SECTIONID"
            'sSql = sSql & " where crad_sectionid= " & Isectionid & " And crad_yearid =" & Iyearid & " And  c.AUD_kitchenID=" & iCustId & " And CRAD_CAuditID= " & iCAID & " And c.AUD_MonthID= " & Imonth & " group by b.cvr_id, b.cvr_name, a.crad_findings"

            sSql = "" : sSql = "Select b.cvr_point, b.cvr_name as Ratingname, count(a.CRAD_SCORE_RESULT) As TotalCount from CBA_ChecklistAuditAssest a "
            sSql = sSql & " left join crpa_valuerating b On b.cvr_point=a.CRAD_SCORE_RESULT  and b.cvr_auditid= " & Isectionid & ""
            sSql = sSql & " join CBAAudit_schedule c On c.aud_sectionid = a.CRAD_SECTIONID"
            sSql = sSql & " where crad_sectionid= " & Isectionid & " And crad_yearid =" & Iyearid & " And  c.AUD_kitchenID=" & iCustId & " And CRAD_CAuditID= " & iCAID & " And c.AUD_MonthID= " & Imonth & " group by b.cvr_id, b.cvr_name, b.cvr_point"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    If IsDBNull(dt.Rows(i)("Ratingname")) = False Then
                        dRow("Ratingname") = dt.Rows(i)("Ratingname")
                    End If
                    If IsDBNull(dt.Rows(i)("TotalCount")) = False Then
                        dRow("TotalCount") = dt.Rows(i)("TotalCount")
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
