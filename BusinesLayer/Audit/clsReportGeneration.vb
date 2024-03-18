Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class ClsReportGeneration
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAuditGeneral As New clsAuditGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetCompanyNameCity(ByVal sAC As String, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "Name" Then
                sSql = "Select Company_Name From Trace_CompanyDetails"
            ElseIf sType = "City" Then
                sSql = "Select Company_City From Trace_CompanyDetails"
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDateId(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Sad_Config_Value from sad_config_settings Where Sad_Config_Key='DateFormat' And SAD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLOEListValuesTotalFee(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer, iFunID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "SubFun" Then
                sSql = "Select Top 1 LOE_SubFunCtionId from SAD_CUST_LOE Where LOE_CustomerId=" & iCustID & " And LOE_YearId=" & iYearID & " And LOE_FunctionId=" & iFunID & ""
            ElseIf sType = "Total" Then
                sSql = "Select Top 1 LOE_Total from SAD_CUST_LOE Where LOE_CustomerId=" & iCustID & " And LOE_YearId=" & iYearID & " And LOE_FunctionId=" & iFunID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetails(ByVal sAC As String, ByVal iAuditNo As Integer, ByVal iFunCtionId As Integer, ByVal sModule As String, ByVal sReport As String, ByVal sCustomerNAme As String, ByVal sYear As String, ByVal Total As Double, ByVal iProId As Integer)
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Dim sStatus As String = "", sContentIDs As String = ""
        Dim ds As New DataSet
        Dim aArray As Array
        Dim i As Integer
        Try
            dt.Columns.Add("Heading")
            dt.Columns.Add("Details")
            If iProId = 0 Then
                sContentIDs = objDBL.SQLGetDescription(sAC, "Select TEM_ContentId From SAD_Finalisation_Report_Template Where  TEM_FunctionId = " & iFunCtionId & " And TEM_Module = '" & sModule & "' And TEM_ReportTitle = '" & sReport & "' And TEM_Delflag  = 'A'")
            Else
                sContentIDs = objDBL.SQLGetDescription(sAC, "Select TEM_ContentId From SAD_Finalisation_Report_Template Where  TEM_FunctionId  in (Select APM_FunctionID from Audit_APM_Details Where APM_ID = " & iAuditNo & ") And TEM_Module = '" & sModule & "' And TEM_ReportTitle = '" & sReport & "' And TEM_Delflag  = 'W'")
            End If
            If sContentIDs <> "" Then
                aArray = sContentIDs.Split(",")
                For i = 0 To UBound(aArray)
                    If aArray(i) <> "" Or aArray(i) <> String.Empty Then
                        dRow = dt.NewRow()
                        dRow("Heading") = objDBL.SQLGetDescription(sAC, "Select FPT_Title from Sad_Finalisation_Report_Contents where FPT_ID = " & aArray(i) & " And FPT_DelFlag<>'D'")
                        dRow("Details") = FixCrLf(objDBL.SQLGetDescription(sAC, "select replace(replace( replace(replace(FPT_Details,'XYZ','" & sCustomerNAme & "'), ' FY ','" & sYear & "'),'XXXXXX'," & Total & "),'?','•') As FPT_Raghu from Sad_Finalisation_Report_Contents Where FPT_ID =  " & aArray(i) & " And FPT_DelFlag<>'D'"))
                        dt.Rows.Add(dRow)
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Shared Function FixCrLf(ByVal value As String) As String
        If value Is Nothing OrElse value.Length = 0 Then Return String.Empty
        Return value.Replace(Environment.NewLine, "§")
    End Function
    Public Function GenerateReport(ByVal sAC As String, ByVal iACID As Integer, ByVal sOutPutFile As String, ByVal dtReoprt As DataTable, ByVal iAuditNo As Integer,
                                   ByVal iFunctionID As Integer, ByVal iCustomerId As Integer, ByVal sCustomerNAme As String, ByVal iYearId As Integer, ByVal sYear As String,
                                   ByVal sFunNAme As String, ByVal sCompanyNAme As String, ByVal sCompanyCity As String, ByVal sSignedby As String, ByVal sDate As String,
                                   ByVal sReportNAme As String, ByVal sListValues As String, ByVal iRID As Integer, ByVal sHeadings As String,
                                   ByVal dtHeadingReport As DataTable, ByVal sTableName As String) As String
        Dim fso, myfile As Object
        Dim fsD As New ClsRTF
        Dim fsM As New ClsRTFHF
        Dim sHeading As String = "", sDetails As String = "", sStatus As String = "", sIsuFId As String = "", sStrFind As String = "", sIssueNo As String = "", sIssuename As String = "", sCreteria As String = "", sCondtion As String = ""
        Dim sRoot As String = "", sImpact As String = "", sRisk As String = "", sSeverity As String = "", sComment As String = "", sFunctionName As String = "", sBusinesssector As String = "", sPName As String = "", sSevername As String = ""
        Dim sHeading1 As String = "", sHeading2 As String = "", sReport As String = "", sSubProcess As String = "", sProcess As String = "", sIsuFIds As String = "", sCreatedOn As String = "", sDDesc As String = "", sProName As String = "", sIssuname As String = ""
        Dim i As Integer, j As Integer, m As Integer, n As Integer, p As Integer, TotalDays As Integer = 0, a As Integer, ra As Integer = 0, iSe As Integer = 0
        Dim dsFinding As New DataSet
        Dim dt As New DataTable, dtSubProcess As New DataTable, dtNew As New DataTable, dtCustID As New DataTable, dtCustomer As New DataTable, dtLOEID As New DataTable, dt2 As New DataTable, dtNew1 As New DataTable, dtI As New DataTable, dts As New DataTable
        Dim dRow2 As DataRow
        Dim sIssues As String = "Issues", sExhibit As String = "Exhibits", sSverity As String = "Severity rating"
        Dim sAuditTeamsID As String
        Try
            fso = CreateObject("Scripting.FileSystemObject")

            '1.Draft Audit Report, 2.Executive Summary, 3.Final Audit Report
            If sReportNAme = "Draft Audit Report" Or sReportNAme = "Executive Summary" Or sReportNAme = "Final Audit Report" Then
                myfile = fso.CreateTextFile(sOutPutFile, True)
                myfile.Writeline(fsM.rtf_StartSetting(sCompanyNAme))
                myfile.Writeline(fsM.rtf_Header(sCustomerNAme, sFunNAme, sYear))
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_JustifyCenter)
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))

                myfile.Writeline((fsD.table_RowStartNew(1)))
                myfile.Writeline(fsD.Table_CellsStart)
                myfile.Writeline(fsD.rtf_FontSize(56))
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsD.rtf_Spacebefore(240))
                myfile.Writeline(fsD.rtf_Spaceafter(240))
                myfile.Writeline(fsD.rtf_LineSpacing(276))
                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & sCustomerNAme & "")))
                myfile.Writeline(fsD.Table_CellsClose)
                myfile.Writeline(fsD.table_RowCloseNew(1))

                myfile.Writeline((fsD.table_RowStartNew(1)))
                myfile.Writeline(fsD.Table_CellsStart)
                myfile.Writeline(fsD.rtf_FontSize(46))
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsD.rtf_Spacebefore(240))
                myfile.Writeline(fsD.rtf_Spaceafter(240))
                myfile.Writeline(fsD.rtf_LineSpacing(276))

                If sReportNAme = "Executive Summary" Then 'Executive Summary
                    myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("Executive Summary to the Top Management " & sFunNAme & " Report for the quarter ended " & sYear & "")))
                Else
                    myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText(sReportNAme & " of the " & sFunNAme & " for the quarter ended " & sYear & "")))
                End If
                myfile.Writeline(fsD.Table_CellsClose)
                myfile.Writeline(fsD.table_RowCloseNew(1))

                myfile.Writeline((fsD.table_RowStartNew(1)))
                myfile.Writeline(fsD.Table_CellsStart)
                myfile.Writeline(fsD.rtf_FontSize(36))
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsD.rtf_Spacebefore(240))
                myfile.Writeline(fsD.rtf_Spaceafter(240))
                myfile.Writeline(fsD.rtf_LineSpacing(276))
                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & sCompanyNAme & " Chartered Accountants")))
                myfile.Writeline(fsD.Table_CellsClose)
                myfile.Writeline(fsD.table_RowCloseNew(1))

                myfile.writeline(fsD.table_CloseTag)

                myfile.Writeline(fsD.rtf_br)
                myfile.writeline(fsD.rtf_PageBreak)
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))
                myfile.writeline(fsD.rtf_JustifyCenter)
                myfile.Writeline(fsD.rtf_LineSpace("This page is intentionally left blank"))
                myfile.Writeline(fsD.rtf_br)
                myfile.writeline(fsD.rtf_PageBreak)
                myfile.Writeline(fsD.rtf_Justified)

                If sReportNAme = "Executive Summary" Or sReportNAme = "Final Audit Report" Then 'Executive Summary,Final Audit Report
                    myfile.writeline(fsD.rtf_br2)
                    myfile.Writeline(fsD.rtf_Justified)
                    myfile.Writeline(fsD.rtf_FontSize(26))
                    myfile.Writeline(fsD.rtf_BoldAndUnderLine("Executive Summary: "))
                    myfile.writeline(fsD.rtf_br2)
                    Dim dtAPM As New DataTable
                    Dim dtClosure As New DataTable
                    Dim dtExecutiveSummary As New DataTable

                    dtExecutiveSummary = objDBL.SQLExecuteDataTable(sAC, "Select * From Audit_ExecutiveSummary Where AES_AuditCode=" & iAuditNo & " And AES_CustID=" & iCustomerId & " And AES_YearID=" & iYearId & " And AES_FunctionID=" & iFunctionID & " ")
                    If dtExecutiveSummary.Rows.Count > 0 Then
                        For iEx = 0 To dtExecutiveSummary.Rows.Count - 1
                            myfile.Writeline(fsD.rtf_Justified)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Introduction: "))

                            myfile.writeline(fsD.rtf_br)
                            myfile.Writeline("" & dtExecutiveSummary.Rows(iEx)("AES_Introduction") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_Justified)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Business Overview: "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & dtExecutiveSummary.Rows(iEx)("AES_BusinessOverview") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Audit Scope: "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & dtExecutiveSummary.Rows(iEx)("AES_AuditScope") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Audit Scope Out: "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & dtExecutiveSummary.Rows(iEx)("AES_AuditScopeOut") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Audit Key Observation: "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & dtExecutiveSummary.Rows(iEx)("AES_KeyAuditObservation") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Audit Period Start Date: "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & objclsGRACeGeneral.FormatDtForRDBMS(dtExecutiveSummary.Rows(iEx)("AES_AuditPeriodStartDate"), "D") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Audit Period End Date : "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & objclsGRACeGeneral.FormatDtForRDBMS(dtExecutiveSummary.Rows(iEx)("AES_AuditPeriodEndDate"), "D") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Actual Period Start Date : "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & objclsGRACeGeneral.FormatDtForRDBMS(dtExecutiveSummary.Rows(iEx)("AES_ActualPeriodStartDate"), "D") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Actual Period End Date:"))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & objclsGRACeGeneral.FormatDtForRDBMS(dtExecutiveSummary.Rows(iEx)("AES_ActualPeriodEndDate"), "D") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Issuance Date:"))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & objclsGRACeGeneral.FormatDtForRDBMS(dtExecutiveSummary.Rows(iEx)("AES_IssuanceDate"), "D") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Audit Rating: "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & dtExecutiveSummary.Rows(iEx)("AES_AuditRating") & "")
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_bold("Audit Remarks: "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline("" & dtExecutiveSummary.Rows(iEx)("AES_AuditRemarks") & "")
                            myfile.writeline(fsD.rtf_br)
                        Next
                    End If
                    dtClosure = objDBL.SQLExecuteDataTable(sAC, "Select * From Audit_SignOff Where ASO_AuditCodeID=" & iAuditNo & " And ASO_YearID=" & iYearId & " ")
                    If dtClosure.Rows.Count > 0 Then
                        myfile.Writeline(fsD.rtf_Justified)
                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(" " & 0 + 1 & ":  Audit Rating"))
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline("" & dtClosure.Rows(0)("ASO_OverAllComments") & "")
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.rtf_bold(" Audit Conclusion"))
                        myfile.writeline(fsD.rtf_br)

                        If dtClosure.Rows(0)("ASO_AuditRatingID") = 1 Then
                            myfile.Writeline("Low")
                            myfile.writeline(fsD.rtf_br)
                        ElseIf dtClosure.Rows(0)("ASO_AuditRatingID") = 2 Then
                            myfile.Writeline("Medium")
                            myfile.writeline(fsD.rtf_br)
                        ElseIf dtClosure.Rows(0)("ASO_AuditRatingID") = 3 Then
                            myfile.Writeline("High")
                            myfile.writeline(fsD.rtf_br)
                        ElseIf dtClosure.Rows(0)("ASO_AuditRatingID") = 4 Then
                            myfile.Writeline("VeryHigh")
                            myfile.writeline(fsD.rtf_br)
                        End If
                    End If
                End If
                Dim ddtAPM
                If sHeadings.Contains("Objectives") = True Then
                    myfile.Writeline(fsD.rtf_FontSize(26))
                    myfile.Writeline(fsD.rtf_BoldAndUnderLine("Objectives:"))
                    myfile.writeline(fsD.rtf_br)

                    ddtAPM = objDBL.SQLExecuteDataTable(sAC, "Select SEM_Name,PM_Name,SPM_Name,CHK_CheckName,Mas_Description,* from Audit_APM_Details
                        Left Join Audit_APM_ChecksMatrix On APMCM_APMPKID=APM_ID  And APMCM_YearID=" & iYearId & " And APMCM_CompID=" & iACID & "
                        Left Join MST_SUBENTITY_MASTER On APMCM_SubFunctionID=SEM_ID And SEM_CompID=" & iACID & "
                        Left Join MST_PROCESS_MASTER On APMCM_ProcessID=PM_ID And APMCM_SubFunctionID=PM_SEM_ID And PM_CompID=" & iACID & "
                        Left Join MST_SUBPROCESS_MASTER On APMCM_SubProcessID=SPM_ID And APMCM_ProcessID=SPM_PM_ID And APMCM_SubFunctionID=SPM_SEM_ID And PM_CompID=" & iACID & "
                        Left Join MST_Checks_Master On APMCM_ControlID=CHK_ControlID And APMCM_ChecksID=CHK_ID And CHK_CompID=" & iACID & "
                        Left Join SAD_CUST_LOCATION  On Mas_ID=APM_BranchID And Mas_CompID=" & iACID & "
                        Where APM_CustID=" & iCustomerId & " And APM_FunctionID=" & iFunctionID & " And APM_YearID=" & iYearId & " And APM_CompID=" & iACID & "")

                    If ddtAPM.Rows.Count > 0 Then
                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold("Location/Branch :  "))
                        myfile.writeline(fsD.rtf_br)

                        myfile.writeline(fsD.rtf_br)
                        myfile.Writeline("" & ddtAPM.Rows(0)("Mas_Description") & "")
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold("Tentative Start Date :  "))
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline("" & objclsGRACeGeneral.FormatDtForRDBMS(ddtAPM.Rows(0)("APM_TStartDate"), "D") & "")
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold("Tentative End Date :  "))
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline("" & objclsGRACeGeneral.FormatDtForRDBMS(ddtAPM.Rows(0)("APM_TEndDate"), "D") & "")
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold("Estimated Effort in Days :  "))
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline("" & ddtAPM.Rows(0)("APM_EstimatedEffortDays") & "")
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold("Objectives/Scope :  "))
                        myfile.writeline(fsD.rtf_br)

                        myfile.writeline(fsD.rtf_br)
                        myfile.Writeline("" & ddtAPM.Rows(0)("APM_Objectives") & "")
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold("Audit Teams : "))
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)

                        sAuditTeamsID = objclsAuditGeneral.GetPartnersAuditorsTeam(sAC, iACID, ddtAPM.Rows(0).Item("APM_ID"), "Team")
                        myfile.Writeline("" & sAuditTeamsID & "")
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.rtf_FontSize(26))
                        myfile.Writeline(fsD.rtf_BoldAndUnderLine("Risk Control Matrix:"))
                        myfile.writeline(fsD.rtf_br)

                        myfile.Writeline(fsD.table_RowStart1(4))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Sub Function")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Process")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Sub Process")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Checks")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(4))
                        For iAPM = 0 To ddtAPM.Rows.Count - 1
                            myfile.Writeline(fsD.table_RowStart1(4))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & ddtAPM.Rows(iAPM)("SEM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & ddtAPM.Rows(iAPM)("PM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & ddtAPM.Rows(iAPM)("SPM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & ddtAPM.Rows(iAPM)("CHK_CheckName").ToString & "")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(4))
                            myfile.Writeline(fsD.table_CloseTag)
                            myfile.Writeline(fsD.rtf_br2)
                        Next
                    Else
                        Dim dtHeading As New DataTable
                        Dim DVZRBADetails As New DataView(dtReoprt)
                        DVZRBADetails.RowFilter = "Heading='Objectives'"
                        dtHeading = DVZRBADetails.ToTable
                        If dtHeading.Rows.Count > 0 Then
                            myfile.Writeline(fsD.rtf_Justified)
                            myfile.Writeline(fsD.rtf_FontSize(26))
                            myfile.Writeline(fsD.rtf_BoldAndUnderLine("" & dtHeading.Rows(i)("Heading") & "    :" & "   "))
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_br)
                            myfile.Writeline(fsD.rtf_bold("" & dtHeading.Rows(i)("Details") & ""))
                            myfile.Writeline(fsD.rtf_br)
                        End If
                    End If
                End If

                If sHeadings.Contains("Time Budget") = True Then
                    myfile.Writeline(fsD.rtf_FontSize(26))
                    myfile.Writeline(fsD.rtf_BoldAndUnderLine("Time Budget :"))
                    myfile.writeline(fsD.rtf_br2)

                    Dim dtAT As New DataTable, dtAP As New DataTable
                    Dim iAT As Integer, iAP As Integer
                    dtAT = objDBL.SQLExecuteDataTable(sAC, "select CMM_Desc,AAPM_AuditTaskID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details
                    Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " 
                    Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAuditNo & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & "
                    Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustomerId & " And AAPM_AuditCodeID=" & iAuditNo & " and AAPM_FunctionID =" & iFunctionID & "")
                    If dtAT.Rows.Count > 0 Then
                        myfile.Writeline(fsD.table_RowStart1(4))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Audit Check Point")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Hours")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Days")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(3))
                        For iAT = 0 To dtAT.Rows.Count - 1
                            myfile.Writeline(fsD.table_RowStart1(3))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAT.Rows(iAT)("CMM_Desc").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAT.Rows(iAT)("ATCB_TotalHours").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAT.Rows(iAT)("ATCB_TotalDays").ToString & "")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(3))
                            myfile.Writeline(fsD.table_CloseTag)
                            myfile.Writeline(fsD.rtf_br2)
                        Next
                    End If
                    dtAP = objDBL.SQLExecuteDataTable(sAC, "select PM_NAME,AAPM_AuditTaskID,ATCB_PKID,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost,ATCB_Status From Audit_APM_Assignment_Details
                             Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & " 
                             Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAuditNo & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & "
                             Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustomerId & " And AAPM_AuditCodeID=" & iAuditNo & " and AAPM_FunctionID =" & iFunctionID & "")
                    If dtAP.Rows.Count > 0 Then
                        myfile.Writeline(fsD.table_RowStart1(4))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Audit Process")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Hours")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Days")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(3))
                        For iAP = 0 To dtAP.Rows.Count - 1
                            myfile.Writeline(fsD.table_RowStart1(3))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAP.Rows(iAP)("PM_NAME").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAP.Rows(iAP)("ATCB_TotalHours").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAP.Rows(iAP)("ATCB_TotalDays").ToString & "")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(3))
                            myfile.Writeline(fsD.table_CloseTag)
                            myfile.Writeline(fsD.rtf_br2)
                        Next
                    End If
                    If dtAT.Rows.Count = 0 Or dtAP.Rows.Count = 0 Then
                        Dim dtHeading As New DataTable
                        Dim DVZRBADetails As New DataView(dtReoprt)
                        DVZRBADetails.RowFilter = "Heading='Time Budget'"
                        dtHeading = DVZRBADetails.ToTable
                        If dtHeading.Rows.Count > 0 Then
                            myfile.Writeline(fsD.rtf_Justified)
                            myfile.Writeline(fsD.rtf_FontSize(26))
                            myfile.Writeline(fsD.rtf_BoldAndUnderLine("" & dtHeading.Rows(i)("Heading") & "    :" & "   "))
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_br)
                            myfile.Writeline(fsD.rtf_bold("" & dtHeading.Rows(i)("Details") & ""))
                            myfile.Writeline(fsD.rtf_br)
                        End If
                    End If
                End If
                If sHeadings.Contains("Cost Budget") = True Then
                    myfile.Writeline(fsD.rtf_FontSize(26))
                    myfile.Writeline(fsD.rtf_BoldAndUnderLine("Cost Budget :"))
                    myfile.writeline(fsD.rtf_br2)

                    Dim dtAT As New DataTable, dtAP As New DataTable
                    Dim iAT As Integer, iAP As Integer
                    dtAT = objDBL.SQLExecuteDataTable(sAC, "Select CMM_Desc,ATCB_TotalDays,ATCB_TotalHours,ATCB_TotalCost From Audit_APM_Assignment_Details
                    Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & " 
                    Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAuditNo & " And ATCB_Type='AT' and ATCB_CompID=" & iACID & "
                    Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustomerId & " And AAPM_AuditCodeID=" & iAuditNo & " and AAPM_FunctionID =" & iFunctionID & "")
                    If dtAT.Rows.Count > 0 Then
                        myfile.Writeline(fsD.table_RowStart1(4))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Audit Check Point")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Hours")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Days")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Cost")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(4))
                        For iAT = 0 To dtAT.Rows.Count - 1
                            myfile.Writeline(fsD.table_RowStart1(4))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAT.Rows(iAT)("CMM_Desc").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAT.Rows(iAT)("ATCB_TotalHours").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAT.Rows(iAT)("ATCB_TotalDays").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAT.Rows(iAT)("ATCB_TotalCost").ToString & "")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(4))
                            myfile.Writeline(fsD.table_CloseTag)
                            myfile.Writeline(fsD.rtf_br2)
                        Next
                    End If
                    dtAP = objDBL.SQLExecuteDataTable(sAC, "Select PM_NAME, ATCB_TotalDays,ATCB_TotalHours, ATCB_TotalCost From Audit_APM_Assignment_Details
                             Left Join Mst_process_Master On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & " 
                             Left Join Audit_TimeCostBudgetMaster On ATCB_TaskProcessID=AAPM_AuditTaskID and ATCB_AuditCodeID=" & iAuditNo & " And ATCB_Type='AP' and ATCB_CompID=" & iACID & "
                             Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & " And AAPM_CustID=" & iCustomerId & " And AAPM_AuditCodeID=" & iAuditNo & " and AAPM_FunctionID =" & iFunctionID & "")
                    If dtAP.Rows.Count > 0 Then
                        myfile.Writeline(fsD.table_RowStart1(4))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Audit Check Point")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Hours")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Days")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total Cost")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(4))
                        For iAP = 0 To dtAP.Rows.Count - 1
                            myfile.Writeline(fsD.table_RowStart1(4))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAP.Rows(iAP)("PM_NAME").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAP.Rows(iAP)("ATCB_TotalHours").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAP.Rows(iAP)("ATCB_TotalDays").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtAP.Rows(iAP)("ATCB_TotalCost").ToString & "")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(4))
                            myfile.Writeline(fsD.table_CloseTag)
                            myfile.Writeline(fsD.rtf_br2)
                        Next
                    End If
                    If dtAT.Rows.Count = 0 And dtAP.Rows.Count = 0 Then
                        Dim dtHeading As New DataTable
                        Dim DVZRBADetails As New DataView(dtReoprt)
                        DVZRBADetails.RowFilter = "Heading='Cost Budget'"
                        dtHeading = DVZRBADetails.ToTable
                        If dtHeading.Rows.Count > 0 Then
                            myfile.Writeline(fsD.rtf_Justified)
                            myfile.Writeline(fsD.rtf_FontSize(26))
                            myfile.Writeline(fsD.rtf_BoldAndUnderLine("" & dtHeading.Rows(i)("Heading") & "    :" & "   "))
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_br)
                            myfile.Writeline(fsD.rtf_bold("" & dtHeading.Rows(i)("Details") & ""))
                            myfile.Writeline(fsD.rtf_br)
                        End If
                    End If
                End If

                If sHeadings.Contains("Workpaper") = True Then
                    myfile.Writeline(fsD.rtf_FontSize(26))
                    myfile.Writeline(fsD.rtf_BoldAndUnderLine("Workpaper:"))
                    myfile.writeline(fsD.rtf_br)

                    Dim dtWP As New DataTable
                    dtWP = objDBL.SQLExecuteDataTable(sAC, "Select Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,a.cmm_Desc as TOT ,b.cmm_Desc as WPC,* From Audit_WorkPaper 
                    Left Join MSt_Entity_Master On ENT_ID=AWP_FunctionID And ENT_CompID=" & iACID & "
                    Left Join MST_SUBENTITY_MASTER On SEM_ID=AWP_SubFunctionID And SEM_CompID=" & iACID & "
                    Left Join MST_PROCESS_MASTER On PM_ID=AWP_ProcessID And PM_CompID=" & iACID & "
                    Left Join MST_SUBPROCESS_MASTER On SPM_ID=AWP_SubProcessID And SPM_CompID=" & iACID & "
                    Left Join MST_MAPPING_MASTER On MMM_FunID=AWP_FunctionID  And MMM_SEMID=AWP_SubFunctionID And MMM_PMID=AWP_ProcessID And MMM_CompID=" & iACID & " And MMM_Module='A' 
                    And MMM_SPMID=AWP_SubProcessID  And  MMM_RiskID=AWP_RiskID And MMM_ControlID=AWP_ControlID And MMM_ChecksID=AWP_ChecksID 
                    Left Join Content_Management_Master a On a.cmm_ID=AWP_TypeofTestID 
                    Left Join Content_Management_Master b On b.cmm_ID=AWP_ConclusionID 
                    Where AWP_AuditCode = " & iAuditNo & " And AWP_CustID = " & iCustomerId & " And AWP_YearID = " & iYearId & " And AWP_FunctionID = " & iFunctionID & "")

                    If dtWP.Rows.Count > 0 Then
                        For iWP = 0 To dtWP.Rows.Count - 1
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Workpaper No:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtWP.Rows(iWP)("AWP_WorkPaperNo") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.table_RowStart1(6))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_TableCellColor(16))

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Sub Function")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Process")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Sub Process")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Risk")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Controls")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Checks")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(6))

                            myfile.Writeline(fsD.table_RowStart1(6))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtWP.Rows(iWP)("SEM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtWP.Rows(iWP)("PM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtWP.Rows(iWP)("SPM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtWP.Rows(iWP)("MMM_Risk").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtWP.Rows(iWP)("MMM_Control").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtWP.Rows(iWP)("MMM_CHECKS").ToString & "")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(6))
                            myfile.Writeline(fsD.table_CloseTag)
                            myfile.Writeline(fsD.rtf_br2)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Work Paper Done:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtWP.Rows(iWP)("AWP_WorkPaperDone") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Note:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtWP.Rows(iWP)("AWP_Note") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Response:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtWP.Rows(iWP)("AWP_Response") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Type of Test:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtWP.Rows(iWP)("TOT") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Conclusion:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.writeline(fsD.rtf_br)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtWP.Rows(iWP)("WPC") & "")
                            myfile.Writeline(fsD.rtf_br2)
                        Next
                    Else
                        Dim dtHeading As New DataTable
                        Dim DVZRBADetails As New DataView(dtReoprt)
                        DVZRBADetails.RowFilter = "Heading='Workpaper'"
                        dtHeading = DVZRBADetails.ToTable
                        If dtHeading.Rows.Count > 0 Then
                            myfile.Writeline(fsD.rtf_Justified)
                            myfile.Writeline(fsD.rtf_FontSize(26))
                            myfile.Writeline(fsD.rtf_BoldAndUnderLine("" & dtHeading.Rows(i)("Heading") & "    :" & "   "))
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_br)
                            myfile.Writeline(fsD.rtf_bold("" & dtHeading.Rows(i)("Details") & ""))
                            myfile.Writeline(fsD.rtf_br)
                        End If
                    End If
                End If
                If sHeadings.Contains("Issue Tracker") = True Then
                    myfile.Writeline(fsD.rtf_BoldAndUnderLine("Issue Tracker:"))
                    myfile.writeline(fsD.rtf_br)

                    Dim dtIT As New DataTable
                    dtIT = objDBL.SQLExecuteDataTable(sAC, "Select AWP_WorkPaperNo,Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_CHECKS,AIT_IssueJobNo,
                    AIT_IssueName,AIT_Details,AIT_Impact,AIT_RootCause,AIT_SuggestedRemedies,a.RAM_Name as RiskCategory,b.RAM_Name as Severity From Audit_IssueTracker_Details  
                    Left Join Audit_WorkPaper On AWP_PKID=AIT_WorkPaperID And AWP_YearID=" & iYearId & " And AWP_CompID=" & iACID & " And AWP_AuditCode = AIT_AuditCode
                    And AWP_CustID = AIT_CustID And AWP_YearID = " & iYearId & " And AWP_FunctionID = AIT_FunctionID
                    Left Join MSt_Entity_Master On ENT_ID=AWP_FunctionID And ENT_CompID=" & iACID & "
                    Left Join MST_SUBENTITY_MASTER On SEM_ID=AWP_SubFunctionID And SEM_CompID=" & iACID & "
                    Left Join MST_PROCESS_MASTER On PM_ID=AWP_ProcessID And PM_CompID=" & iACID & "
                    Left Join MST_SUBPROCESS_MASTER On SPM_ID=AWP_SubProcessID And SPM_CompID=" & iACID & "
                    Left Join MST_MAPPING_MASTER On MMM_FunID=AWP_FunctionID  And MMM_SEMID=AWP_SubFunctionID And MMM_PMID=AWP_ProcessID And MMM_CompID=" & iACID & " And MMM_Module='A' 
                    And MMM_SPMID=AWP_SubProcessID  And  MMM_RiskID=AWP_RiskID And MMM_ControlID=AWP_ControlID And MMM_ChecksID=AWP_ChecksID 
                    Left Join Risk_GeneralMaster a On AIT_RiskCategoryID=a.RAM_PKID And a.RAM_DelFlag ='A' 
                    and a.RAM_Category='RT' And a.RAM_YearID=" & iYearId & " and a.RAM_CompID=" & iACID & "
                    Left Join Risk_GeneralMaster b On AIT_SeverityID=b.RAM_PKID And b.RAM_DelFlag ='A' 
                    and b.RAM_Category='RRS' And b.RAM_YearID=" & iYearId & " and b.RAM_CompID=" & iACID & "
                    Where AIT_AuditCode = " & iAuditNo & " And AIT_CustID = " & iCustomerId & " And AIT_YearID = " & iYearId & " And AIT_FunctionID = " & iFunctionID & "")

                    If dtIT.Rows.Count > 0 Then
                        For iWP = 0 To dtIT.Rows.Count - 1
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Workpaper No:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("AWP_WorkPaperNo") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Issue No:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("AIT_IssueJobNo") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.table_RowStart1(6))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_TableCellColor(16))

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Sub Function")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Process")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Sub Process")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Risk")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Controls")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Checks")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(6))

                            myfile.Writeline(fsD.table_RowStart1(6))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtIT.Rows(iWP)("SEM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtIT.Rows(iWP)("PM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtIT.Rows(iWP)("SPM_Name").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtIT.Rows(iWP)("MMM_Risk").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtIT.Rows(iWP)("MMM_Control").ToString & "")))
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtIT.Rows(iWP)("MMM_CHECKS").ToString & "")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(6))
                            myfile.Writeline(fsD.table_CloseTag)
                            myfile.Writeline(fsD.rtf_br2)

                            myfile.writeline(fsD.rtf_br)
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Issue name:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("AIT_IssueName") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Details:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("AIT_Details") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Impact:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("AIT_Impact") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Root Cause:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("AIT_RootCause") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Suggested Remedies:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("AIT_SuggestedRemedies") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Risk Category:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("RiskCategory") & "")
                            myfile.writeline(fsD.rtf_br)
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold("Severity:     "))
                            myfile.writeline(fsD.rtf_br)

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline("" & dtIT.Rows(iWP)("Severity") & "")
                            myfile.Writeline(fsD.rtf_br2)
                        Next
                    Else
                        Dim dtHeading As New DataTable
                        Dim DVZRBADetails As New DataView(dtReoprt)
                        DVZRBADetails.RowFilter = "Heading='Issue Tracker'"
                        dtHeading = DVZRBADetails.ToTable
                        If dtHeading.Rows.Count > 0 Then
                            myfile.Writeline(fsD.rtf_Justified)
                            myfile.Writeline(fsD.rtf_FontSize(26))
                            myfile.Writeline(fsD.rtf_BoldAndUnderLine("" & dtHeading.Rows(i)("Heading") & "    :" & "   "))
                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_br)
                            myfile.Writeline(fsD.rtf_bold("" & dtHeading.Rows(i)("Details") & ""))
                            myfile.Writeline(fsD.rtf_br)
                        End If
                    End If
                End If

                Dim sHeadingName As String = "", sHeadingID() As String
                sHeadingID = sHeadings.Split(",")

                If sHeadingID.Length > 0 Then
                    For j = 0 To sHeadingID.Length - 1
                        sHeadingName = sHeadingID(j)
                        If sHeadingName = "Workpaper" Or sHeadingName = "Objectives" Or sHeadingName = "Issue Tracker" Or sHeadingName = "Time Budget" Or sHeadingName = "Cost Budget" Then
                        Else
                            Dim dtHeading As New DataTable
                            Dim DVZRBADetails As New DataView(dtReoprt)
                            DVZRBADetails.RowFilter = "Heading='" & sHeadingName & "'"
                            dtHeading = DVZRBADetails.ToTable
                            If dtHeading.Rows.Count > 0 Then
                                myfile.Writeline(fsD.rtf_Justified)
                                myfile.Writeline(fsD.rtf_FontSize(26))
                                myfile.Writeline(fsD.rtf_BoldAndUnderLine("" & dtHeading.Rows(i)("Heading") & "    :" & "   "))
                                myfile.Writeline(fsD.rtf_FontSize(20))
                                myfile.Writeline(fsD.rtf_br)
                                myfile.Writeline(fsD.rtf_bold("" & dtHeading.Rows(i)("Details") & ""))
                                myfile.Writeline(fsD.rtf_br)
                            End If
                        End If
                    Next
                End If

                '---------
                'dtReport - Lavanya

                If dtHeadingReport.Rows.Count > 0 Then
                    Dim dtHeading As New DataTable
                    Dim sHeadingNames As String = ""
                    dtHeading = CheckTableExists(sAC, sTableName)
                    For k = 0 To dtHeading.Rows.Count - 1
                        sHeadingNames = sHeadingNames & "," & dtHeading.Rows(k)("column_name")
                    Next

                    If sHeadingNames.StartsWith(",") Then
                        sHeadingNames = sHeadingNames.Remove(0, 1)
                    End If

                    myfile.Writeline(fsD.table_RowStart1(4))
                    myfile.Writeline(fsD.Table_CellsStart)
                    myfile.Writeline(fsD.rtf_TableCellColor(16))

                    myfile.Writeline(fsD.rtf_FontSize(20))

                    Dim sHeadingIDs() As String
                    sHeadingIDs = sHeadingNames.Split(",")

                    If sHeadingIDs.Length > 0 Then
                        For j = 0 To sHeadingIDs.Length - 1
                            sHeadingNames = sHeadingIDs(j)
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText(sHeadingNames)))
                        Next
                    End If

                    myfile.Writeline(fsD.Table_CellsClose)
                    myfile.Writeline(fsD.table_RowClose1(4))

                    myfile.Writeline(fsD.table_RowStart1(4))
                    myfile.Writeline(fsD.Table_CellsStart)
                    myfile.Writeline(fsD.rtf_FontSize(5))

                    For i = 0 To dtHeadingReport.Rows.Count - 1
                        For j = 0 To sHeadingIDs.Count - 1
                            myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dtHeadingReport.Rows(i)(j) & "")))
                        Next
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(4))
                    Next
                End If
                myfile.Writeline(fsD.table_CloseTag)
                myfile.Writeline(fsD.rtf_br2)

                '-------------------------------------------------------------------------------------------------------------------------------------------------

                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsD.rtf_bold("Signed by:"))
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("                                                                  " & sSignedby & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline("For and on behalf of:                             " & sCustomerNAme & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("                                                                  Chartered Accountants")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline("Place:                                                        " & sCompanyCity & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(("Date:                                                          " & sDate & ""))
                myfile.Writeline(fsD.Fontcolor_Close)
                myfile.close()
                fso = Nothing
                Return sOutPutFile


            ElseIf sReportNAme = "Proposal" Then '4.Proposal
                myfile = fso.CreateTextFile(sOutPutFile, True)
                myfile.Writeline(fsM.rtf_StartSetting(sCompanyNAme))
                sFunNAme = objDBL.SQLGetDescription(sAC, "Select Ent_entityNAme from MSt_Entity_MAster Where Ent_Id = " & iFunctionID & "")
                myfile.Writeline(fsM.rtf_PHeader(sCustomerNAme, sFunNAme, sYear))
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_JustifyCenter)
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))

                myfile.Writeline((fsD.table_RowStartNew(1)))
                myfile.Writeline(fsD.Table_CellsStart)
                myfile.Writeline(fsD.rtf_FontSize(56))
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsD.rtf_Spacebefore(240))
                myfile.Writeline(fsD.rtf_Spaceafter(240))
                myfile.Writeline(fsD.rtf_LineSpacing(276))
                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & sCustomerNAme & "")))
                myfile.Writeline(fsD.Table_CellsClose)
                myfile.Writeline(fsD.table_RowCloseNew(1))

                myfile.Writeline((fsD.table_RowStartNew(1)))
                myfile.Writeline(fsD.Table_CellsStart)
                myfile.Writeline(fsD.rtf_FontSize(46))
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsD.rtf_Spacebefore(240))
                myfile.Writeline(fsD.rtf_Spaceafter(240))
                myfile.Writeline(fsD.rtf_LineSpacing(276))
                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("Proposal for  " & sFunNAme & " services ")))
                myfile.Writeline(fsD.Table_CellsClose)
                myfile.Writeline(fsD.table_RowCloseNew(1))

                myfile.Writeline((fsD.table_RowStartNew(1)))
                myfile.Writeline(fsD.Table_CellsStart)
                myfile.Writeline(fsD.rtf_FontSize(36))
                myfile.Writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsD.rtf_Spacebefore(240))
                myfile.Writeline(fsD.rtf_Spaceafter(240))
                myfile.Writeline(fsD.rtf_LineSpacing(276))
                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & sCompanyNAme & " Chartered Accountants")))
                myfile.Writeline(fsD.Table_CellsClose)
                myfile.Writeline(fsD.table_RowCloseNew(1))

                myfile.Writeline(fsD.table_CloseTag)

                myfile.Writeline(fsD.rtf_br)
                myfile.writeline(fsD.rtf_PageBreak)
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))
                myfile.writeline(fsD.rtf_JustifyCenter)
                myfile.Writeline(fsD.rtf_LineSpace("This page is intentionally left blank"))
                myfile.Writeline(fsD.rtf_br)
                myfile.writeline(fsD.rtf_PageBreak)
                myfile.Writeline(fsD.rtf_Justified)


                For i = 0 To dtReoprt.Rows.Count - 1
                    myfile.Writeline(fsD.rtf_Justified)
                    myfile.Writeline(fsD.rtf_FontSize(20))
                    myfile.Writeline(fsD.rtf_Spacebefore(0))
                    myfile.Writeline(fsD.rtf_Spaceafter(120))
                    myfile.Writeline(fsD.rtf_LineSpacing(360))

                    myfile.Writeline(fsD.rtf_bold(" " & i + 1 & ".  " & dtReoprt.Rows(i)("Heading") & ""))
                    myfile.Writeline(fsD.rtf_br)

                    sHeading1 = "Industry experience and credentials"
                    sHeading2 = "Fees"
                    If String.Equals(UCase(Trim(sHeading1)), UCase(Trim(dtReoprt.Rows(i)("Heading").ToString))) Then
                        myfile.Writeline(fsD.rtf_br)
                        dt2.Columns.Add("Table_Id")
                        dt2.Columns.Add("Table_Serial")
                        dt2.Columns.Add("Table_ClientName")
                        dt2.Columns.Add("Table_Bussiness")
                        dt2.Columns.Add("Table_engagement")

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "1"
                        dRow2("Table_Serial") = "1"
                        dRow2("Table_ClientName") = "Aurigene Discovery Technologies Limited "
                        dRow2("Table_Bussiness") = "Pharmaceutical research "
                        dRow2("Table_engagement") = "Internal audit "
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "2"
                        dRow2("Table_Serial") = "2"
                        dRow2("Table_ClientName") = "e4e Business Solutions India Pvt Ltd "
                        dRow2("Table_Bussiness") = "Managed services "
                        dRow2("Table_engagement") = "Internal audit "
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "3"
                        dRow2("Table_Serial") = "3"
                        dRow2("Table_ClientName") = "IBS Software Services Private Limited"
                        dRow2("Table_Bussiness") = "Software development"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "4"
                        dRow2("Table_Serial") = "4"
                        dRow2("Table_ClientName") = "Infosys Limited"
                        dRow2("Table_Bussiness") = "Software development"
                        dRow2("Table_engagement") = "SoX and Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "5"
                        dRow2("Table_Serial") = "5"
                        dRow2("Table_ClientName") = "International Travel House"
                        dRow2("Table_Bussiness") = "Travel & logistics"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "6"
                        dRow2("Table_Serial") = "6"
                        dRow2("Table_ClientName") = "Happiest Minds Technologies Private Limited"
                        dRow2("Table_Bussiness") = "Software development"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "7"
                        dRow2("Table_Serial") = "7"
                        dRow2("Table_ClientName") = "Maini Material Movement Private Limited"
                        dRow2("Table_Bussiness") = "Manufacturer"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "8"
                        dRow2("Table_Serial") = "8"
                        dRow2("Table_ClientName") = "Maini Precisions Private Limited"
                        dRow2("Table_Bussiness") = "Precision tool manufacturer"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "9"
                        dRow2("Table_Serial") = "9"
                        dRow2("Table_ClientName") = "SanDisk India Device Design Centre Private Limited"
                        dRow2("Table_Bussiness") = "Software development"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "10"
                        dRow2("Table_Serial") = "10"
                        dRow2("Table_ClientName") = "Sutherland Global Services Pvt Ltd "
                        dRow2("Table_Bussiness") = "B P O "
                        dRow2("Table_engagement") = "SoX and Internal audit "
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "11"
                        dRow2("Table_Serial") = "11"
                        dRow2("Table_ClientName") = "Tejas Networks India Ltd "
                        dRow2("Table_Bussiness") = "Hardware manufacturer "
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "12"
                        dRow2("Table_Serial") = "12"
                        dRow2("Table_ClientName") = "Texas Instruments (India) Private Limited"
                        dRow2("Table_Bussiness") = "Software development"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "13"
                        dRow2("Table_Serial") = "13"
                        dRow2("Table_ClientName") = "Trident Powercraft Pvt Ltd "
                        dRow2("Table_Bussiness") = "Electrical motors "
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        dRow2 = dt2.NewRow
                        dRow2("Table_Id") = "14"
                        dRow2("Table_Serial") = "14"
                        dRow2("Table_ClientName") = "UKN group"
                        dRow2("Table_Bussiness") = "Construction / Hospitality"
                        dRow2("Table_engagement") = "Internal audit"
                        dt2.Rows.Add(dRow2)

                        If dt2.Rows.Count > 0 Then
                            myfile.Writeline(fsD.table_RowStart1(4))
                            myfile.Writeline(fsD.Table_CellsStart)
                            myfile.Writeline(fsD.rtf_TableCellColor(16))

                            myfile.Writeline(fsD.rtf_FontSize(20))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Serial #")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Client name")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Bussiness sector")))
                            myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Nature of engagement")))
                            myfile.Writeline(fsD.Table_CellsClose)
                            myfile.Writeline(fsD.table_RowClose1(4))
                            For a = 0 To dt2.Rows.Count - 1
                                myfile.Writeline(fsD.table_RowStart1(4))
                                myfile.Writeline(fsD.Table_CellsStart)
                                myfile.Writeline(fsD.rtf_FontSize(20))
                                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dt2.Rows(a)("Table_Serial").ToString & "")))
                                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dt2.Rows(a)("Table_ClientName").ToString & "")))
                                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dt2.Rows(a)("Table_Bussiness").ToString & "")))
                                myfile.Writeline(fsD.rtf_LineSpace(fsD.Table_CellText("" & dt2.Rows(a)("Table_engagement").ToString & "")))
                                myfile.Writeline(fsD.Table_CellsClose)
                                myfile.Writeline(fsD.table_RowClose1(4))
                            Next
                        End If
                        myfile.Writeline(fsD.table_CloseTag)
                        myfile.Writeline(fsD.rtf_br2)

                        myfile.Writeline(fsD.table_RowStart1(4))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))

                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Serial #")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Client name")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Business sector")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Nature of engagement")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(4))
                        dtCustID = objDBL.SQLExecuteDataSet(sAC, "Select Distinct LOE_CustomerID From SAD_CUST_LOE Where LOE_FunctionId=" & iFunctionID & "").Tables(0)
                        If dtCustID.Rows.Count > 0 Then
                            For n = 0 To dtCustID.Rows.Count - 1
                                dtCustomer = LoadCustomer(sAC, dtCustID.Rows(n)("LOE_CustomerID"))
                                If dtCustomer.Rows.Count > 0 Then
                                    For m = 0 To dtCustomer.Rows.Count - 1
                                        myfile.Writeline(fsD.table_RowStart1(4))
                                        myfile.Writeline(fsD.Table_CellsStart)
                                        myfile.Writeline(fsD.rtf_FontSize(20))
                                        myfile.Writeline((fsD.Table_CellText("" & n + 1 & "")))
                                        myfile.Writeline((fsD.Table_CellText("" & dtCustomer.Rows(m)("CustomerName").ToString & "")))
                                        sBusinesssector = objDBL.SQLGetDescription(sAC, "Select Mas_Description from SAD_INDUSTRYTYPE_GENERAL_MASTER Where Mas_Id = " & dtCustomer.Rows(m)("CustomerINDType") & "")
                                        myfile.Writeline((fsD.Table_CellText("" & sBusinesssector & "")))
                                        sFunctionName = "Internal Audit"
                                        myfile.Writeline((fsD.Table_CellText("" & sFunctionName & "")))
                                        myfile.Writeline(fsD.Table_CellsClose)
                                        myfile.Writeline(fsD.table_RowClose1(4))
                                    Next
                                End If
                            Next
                        End If
                        myfile.Writeline(fsD.table_CloseTag)
                        myfile.Writeline(fsD.rtf_br2)
                    End If
                    If String.Equals(UCase(Trim(sHeading2)), UCase(Trim(dtReoprt.Rows(i)("Heading").ToString))) Then
                        myfile.Writeline(fsD.table_RowStart1(2))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))
                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Personnel ")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Person days (" & sYear & ")")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(2))
                        dtLOEID = objDBL.SQLExecuteDataSet(sAC, "Select LOER_CategoryName,LOER_NoDays From LOE_Resources Where LOER_LOEID in (Select Top 1 LOE_Id From SAD_CUST_LOE Where LOE_YearId=" & iYearId & " And LOE_CustomerId=" & iCustomerId & " And LOE_FunctionId= " & iFunctionID & ")").Tables(0)
                        If dtLOEID.Rows.Count > 0 Then
                            For p = 0 To dtLOEID.Rows.Count - 1
                                myfile.Writeline(fsD.table_RowStart1(2))
                                myfile.Writeline(fsD.Table_CellsStart)
                                myfile.Writeline(fsD.rtf_FontSize(20))
                                myfile.Writeline((fsD.Table_CellText("" & dtLOEID.Rows(p)("LOER_CategoryName").ToString & "")))
                                myfile.Writeline((fsD.Table_CellText("" & dtLOEID.Rows(p)("LOER_NoDays").ToString & "")))
                                TotalDays = TotalDays + dtLOEID.Rows(p)("LOER_NoDays").ToString()
                                myfile.Writeline(fsD.Table_CellsClose)
                                myfile.Writeline(fsD.table_RowClose1(2))
                            Next
                        End If
                        myfile.Writeline(fsD.table_RowStart1(2))
                        myfile.Writeline(fsD.Table_CellsStart)
                        myfile.Writeline(fsD.rtf_TableCellColor(16))
                        myfile.Writeline(fsD.rtf_FontSize(20))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("Total ")))
                        myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("" & TotalDays & "")))
                        myfile.Writeline(fsD.Table_CellsClose)
                        myfile.Writeline(fsD.table_RowClose1(2))
                        myfile.Writeline(fsD.table_CloseTag)
                    End If
                    myfile.Writeline(fsD.rtf_Justified)
                    myfile.Writeline(fsD.rtf_FontSize(20))
                    myfile.Writeline(fsD.rtf_Spacebefore(0))
                    myfile.Writeline(fsD.rtf_Spaceafter(120))
                    myfile.Writeline(fsD.rtf_LineSpacing(360))
                    Dim sDesc As String = ""
                    Dim v As Integer
                    Dim aDesc As String()
                    sDesc = dtReoprt.Rows(i)("Details").ToString
                    If sDesc.Contains("§") Then
                        aDesc = sDesc.Split("§")
                        For v = 0 To aDesc.Length - 1
                            myfile.Writeline(fsD.rtf_LineSpace("" & aDesc(v) & ""))
                            myfile.Writeline(fsD.rtf_br)
                        Next
                    Else
                        myfile.Writeline(fsD.rtf_LineSpace("" & sDesc & ""))
                        myfile.Writeline(fsD.rtf_br)
                    End If
                Next
                myfile.Writeline(fsD.rtf_bold("Annexure 1"))
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(fsD.rtf_Justified)
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_LineSpace("The scope of internal audit and the frequency, as indicated by management is as follows:"))
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_bold("Process / Areas"))
                myfile.Writeline(fsD.rtf_br)

                dtNew = LoadProcess(sAC, sListValues)
                For i = 0 To dtNew.Rows.Count - 1
                    myfile.Writeline(fsD.table_RowStart1(1))
                    myfile.Writeline(fsD.Table_CellsStart)
                    myfile.Writeline(fsD.rtf_TableCellColor(16))
                    myfile.Writeline(fsD.rtf_FontSize(24))
                    myfile.Writeline(fsD.rtf_bold(fsD.Table_CellText("" & dtNew.Rows(i)("Process").ToString & "")))
                    myfile.Writeline(fsD.Table_CellsClose)
                    myfile.Writeline(fsD.table_RowClose1(1))
                    If IsDBNull(dtNew.Rows(i)("ProcessID")) = False Then
                        dtSubProcess = LoadSubProcess(sAC, dtNew.Rows(i)("ProcessID"))
                        If dtSubProcess.Rows.Count > 0 Then
                            For j = 0 To dtSubProcess.Rows.Count - 1
                                myfile.Writeline(fsD.table_RowStart1(1))
                                myfile.Writeline(fsD.Table_CellsStart)
                                myfile.Writeline(fsD.rtf_FontSize(20))
                                myfile.Writeline((fsD.Table_CellText("" & dtSubProcess.Rows(j)("SubProcess").ToString & "")))
                                myfile.Writeline(fsD.Table_CellsClose)
                                myfile.Writeline(fsD.table_RowClose1(1))
                            Next
                        End If
                    End If
                Next
                myfile.Writeline(fsD.table_CloseTag)
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.Fontcolor_Close)
                myfile.close()
                fso = Nothing
                Return sOutPutFile

            ElseIf sReportNAme = "Proposal For Renewal" Then '5.Proposal For Renewal
                Dim sCity As String = ""
                Dim sPin As String = ""
                Dim sState As String = ""
                Dim sCountry As String = ""
                Dim sAddress As String = ""

                sAddress = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Address,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sAddress) = False Then
                    sAddress = sAddress
                Else
                    sAddress = ""
                End If
                sCity = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_city,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sCity) = False Then
                    sCity = sCity
                Else
                    sCity = ""
                End If
                sPin = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Pin,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sPin) = False Then
                    sPin = sPin
                Else
                    sPin = ""
                End If
                sState = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_state,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & " ")
                If IsDBNull(sState) = False Then
                    sState = sState
                Else
                    sState = ""
                End If
                sCountry = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Country,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sCountry) = False Then
                    sCountry = sCountry
                Else
                    sCountry = ""
                End If

                myfile = fso.CreateTextFile(sOutPutFile, True)
                myfile.Writeline(fsD.rtf_StartSetting())
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))
                myfile.writeline(fsD.rtf_JustifyRight)
                myfile.Writeline("" & sDate & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline("To,")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCustomerNAme & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sAddress & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCity & "")
                myfile.Writeline("" & sPin & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sState & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCountry & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Dear Sir,")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Sub: 	Proposal for renewal of audit services under Karnataka Value Added Tax Act, 2003 ")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Ref:	Email dated ")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("In terms of our discussions with the concerned personnel at " & sCustomerNAme & " , the work scope is to carry out the audit of the transACtions / records of xxx India for the financial year " & sYear & " . as required under the provisions of Karnataka Value Added Tax Act, 2003 (VAT law). The detailed workscope is as under:")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(fsD.rtf_Justified)

                For i = 0 To dtReoprt.Rows.Count - 1
                    myfile.Writeline(fsD.rtf_Justified)
                    myfile.Writeline(fsD.rtf_FontSize(20))
                    myfile.Writeline(fsD.rtf_bold(" " & i + 1 & ".  " & dtReoprt.Rows(i)("Heading") & ""))
                    myfile.Writeline(fsD.rtf_br)
                    myfile.Writeline(fsD.rtf_Justified)
                    myfile.Writeline(fsD.rtf_FontSize(20))
                    myfile.Writeline(fsD.rtf_Spacebefore(0))
                    myfile.Writeline(fsD.rtf_Spaceafter(120))
                    myfile.Writeline(fsD.rtf_LineSpacing(360))
                    Dim sprDesc As String = ""
                    Dim ip As Integer
                    Dim aprDesc As String()

                    sprDesc = dtReoprt.Rows(i)("Details").ToString
                    If sprDesc.Contains("§") Then
                        aprDesc = sprDesc.Split("§")
                        For ip = 0 To aprDesc.Length - 1
                            myfile.Writeline(fsD.rtf_LineSpace("" & aprDesc(ip) & ""))
                            myfile.Writeline(fsD.rtf_br)
                        Next
                    Else
                        myfile.Writeline(fsD.rtf_LineSpace("" & sprDesc & ""))
                        myfile.Writeline(fsD.rtf_br)
                    End If
                Next
                myfile.Writeline(fsD.rtf_br2)

                myfile.Writeline("For " & sCompanyNAme & " ")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("………………")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Partner")

                myfile.Writeline(fsD.Fontcolor_Close)
                myfile.close()
                fso = Nothing
                Return sOutPutFile

            ElseIf sReportNAme = "Management Representation" Then '8.Management Representation
                Dim sCompanyNAmes As String = ""
                Dim sCompanyAddress As String = ""
                Dim sCompanyPin As String = ""
                Dim sCompanyState As String = ""
                Dim sCompanyCountry As String = ""
                Dim sStr As String = ""

                sStr = "Management Representation for the year ended March 31,"
                sCompanyNAmes = objDBL.SQLGetDescription(sAC, "Select Company_Name From Trace_CompanyDetails ")
                sCompanyAddress = objDBL.SQLGetDescription(sAC, "Select Company_Address From Trace_CompanyDetails ")
                sCompanyCity = objDBL.SQLGetDescription(sAC, "Select Company_City From Trace_CompanyDetails ")
                sCompanyPin = objDBL.SQLGetDescription(sAC, "Select Company_PinCode From Trace_CompanyDetails ")
                sCompanyState = objDBL.SQLGetDescription(sAC, "Select Company_State From Trace_CompanyDetails ")
                sCompanyCountry = objDBL.SQLGetDescription(sAC, "Select Company_Country From Trace_CompanyDetails ")

                myfile = fso.CreateTextFile(sOutPutFile, True)
                myfile.Writeline(fsD.rtf_StartSetting())
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))
                myfile.writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline(fsM.rtf_MHeader(sStr, sYear))
                myfile.Writeline("To,")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCompanyNAmes & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Chartered Accountants")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCompanyAddress & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCompanyCity & " - " & sCompanyPin & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCompanyState & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCompanyCountry & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Dear Sir,")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("This representation letter is provided in connection with your audit of the financial statements of our Company for the year ended March 31, " & sYear & ",for the purpose of expressing an opinion as to whether the financial statements give a true and fair view of the financial position of our Company as of March 31," & sYear & " and of the results of operations for the year then ended. We acknowledge our responsibility for preparation of financial statements in accordance with the requirements of the Companies Act, 1956 and recognised accounting policies and practices, including the Accounting Standards notified under the Companies Act, 1956.")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("We confirm to the best of our knowledge and belief, the following representations:")
                myfile.Writeline(fsD.rtf_br)

                For i = 0 To dtReoprt.Rows.Count - 1
                    myfile.Writeline(fsD.rtf_Justified)
                    myfile.Writeline(fsD.rtf_FontSize(20))
                    myfile.Writeline(fsD.rtf_bold(" " & i + 1 & ".  " & dtReoprt.Rows(i)("Heading") & ""))
                    myfile.Writeline(fsD.rtf_br)
                    myfile.Writeline(fsD.rtf_Justified)
                    myfile.Writeline(fsD.rtf_FontSize(20))
                    myfile.Writeline(fsD.rtf_Spacebefore(0))
                    myfile.Writeline(fsD.rtf_Spaceafter(120))
                    myfile.Writeline(fsD.rtf_LineSpacing(360))
                    Dim smrDesc As String = ""
                    Dim im As Integer
                    Dim amrDesc As String()
                    smrDesc = dtReoprt.Rows(i)("Details").ToString
                    If smrDesc.Contains("§") Then
                        amrDesc = smrDesc.Split("§")
                        For im = 0 To amrDesc.Length - 1
                            myfile.Writeline(fsD.rtf_LineSpace("" & amrDesc(im) & ""))
                            myfile.Writeline(fsD.rtf_br)
                        Next
                    Else
                        myfile.Writeline(fsD.rtf_LineSpace("" & smrDesc & ""))
                        myfile.Writeline(fsD.rtf_br)
                    End If
                Next
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Thanking You,")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("for and on behalf of the Board")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Director")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Place:")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Date:")
                myfile.Writeline(fsD.Fontcolor_Close)
                myfile.close()
                fso = Nothing
                Return sOutPutFile

            ElseIf sReportNAme = "Acceptence Letter" Then '6.Acceptence Letter
                Dim sCity As String = ""
                Dim sPin As String = ""
                Dim sState As String = ""
                Dim sCountry As String = ""
                Dim sAddress As String = ""

                sAddress = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Address,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sAddress) = False Then
                    sAddress = sAddress
                Else
                    sAddress = ""
                End If
                sCity = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_city,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sCity) = False Then
                    sCity = sCity
                Else
                    sCity = ""
                End If
                sPin = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Pin,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sPin) = False Then
                    sPin = sPin
                Else
                    sPin = ""
                End If
                sState = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_state,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & " ")
                If IsDBNull(sState) = False Then
                    sState = sState
                Else
                    sState = ""
                End If
                sCountry = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Country,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sCountry) = False Then
                    sCountry = sCountry
                Else
                    sCountry = ""
                End If
                myfile = fso.CreateTextFile(sOutPutFile, True)
                myfile.Writeline(fsD.rtf_StartSetting())
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))
                myfile.writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline("" & sDate & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("To,")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCustomerNAme & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sAddress & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCity & "")
                myfile.Writeline("" & sPin & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sState & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCountry & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Dear Sir,")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_bold("Sub:  "))
                myfile.Writeline("Acceptance of appointment as auditors under the provisions of Karnataka Value Added Tax Act, 2003 for the year ending on " & sYear & " ")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(fsD.rtf_bold("Ref:  "))
                myfile.Writeline("Your appointment letter dated " & sDate & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("With reference to the above subject we confirm the receipt of your captioned letter. We accept the appointment as auditors under section 31(4) of the Karnataka Value Added Tax Act, 2003 read with Rule 34 of the Karnataka Value Added Tax  Rules, 2005 for the year ending " & sYear & ".")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Thanking you,")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("For " & sCompanyNAme & " ")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("………………")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Partner")

                myfile.Writeline(fsD.Fontcolor_Close)
                myfile.close()
                fso = Nothing
                Return sOutPutFile

            ElseIf sReportNAme = "Covering Letter" Then '7.Covering Letter
                Dim sCity As String = ""
                Dim sPin As String = ""
                Dim sState As String = ""
                Dim sCountry As String = ""
                Dim sAddress As String = ""

                sAddress = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Address,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sAddress) = False Then
                    sAddress = sAddress
                Else
                    sAddress = ""
                End If
                sCity = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_city,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sCity) = False Then
                    sCity = sCity
                Else
                    sCity = ""
                End If
                sPin = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Pin,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sPin) = False Then
                    sPin = sPin
                Else
                    sPin = ""
                End If
                sState = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_state,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & " ")
                If IsDBNull(sState) = False Then
                    sState = sState
                Else
                    sState = ""
                End If
                sCountry = objDBL.SQLGetDescription(sAC, "Select isnull(Cust_Comm_Country,'') from SAD_CUSTOMER_MASTER Where Cust_Id =" & iCustomerId & "")
                If IsDBNull(sCountry) = False Then
                    sCountry = sCountry
                Else
                    sCountry = ""
                End If
                myfile = fso.CreateTextFile(sOutPutFile, True)
                myfile.Writeline(fsD.rtf_StartSetting())
                myfile.Writeline(fsD.rtf_FontSize(20))
                myfile.Writeline(fsD.rtf_Spacebefore(0))
                myfile.Writeline(fsD.rtf_Spaceafter(120))
                myfile.Writeline(fsD.rtf_LineSpacing(360))
                myfile.writeline(fsD.rtf_JustifyLeft)
                myfile.Writeline("" & sDate & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("To,")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(fsD.rtf_bold("Mr. " & sCustomerNAme & ""))
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline(fsD.rtf_bold("Chief Financial Officer"))
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sAddress & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCity & "")
                myfile.Writeline("" & sPin & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sState & "")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("" & sCountry & "")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Dear Sir,")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Sub: " & sFunNAme & " Report for the quarter ended -------------")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline("Please find enclosed the captioned report.")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Assuring you of our best services,")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("For " & sCompanyNAme & " ")
                myfile.Writeline(fsD.rtf_br2)
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("---------------")
                myfile.Writeline(fsD.rtf_br)
                myfile.Writeline("Partner")
                myfile.Writeline(fsD.Fontcolor_Close)
                myfile.close()
                fso = Nothing
                Return sOutPutFile
            End If
        Catch ex As Exception
            Throw
        Finally
            dt = Nothing
        End Try
    End Function
    Public Function LoadCustomer(ByVal sAC As String, ByVal CustID As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("CustomerINDType")
            sSql = "Select Cust_Name,Cust_IndTypeId From SAD_CUSTOMER_MASTER Where Cust_Id In(" & CustID & ")"
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("CustomerName") = dtTab.Rows(i)("Cust_Name").ToString
                    dRow("CustomerINDType") = dtTab.Rows(i)("Cust_IndTypeId").ToString
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadProcess(ByVal sAC As String, ByVal sListValues As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("ProcessID")
            dt.Columns.Add("Process")
            sSql = "Select PM_ID,PM_Name from Mst_Process_Master Where PM_SEM_ID in ( " & sListValues & ")"
            dtTab = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("ProcessID") = dtTab.Rows(i)("PM_ID").ToString
                    dRow("Process") = dtTab.Rows(i)("PM_Name").ToString
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubProcess(ByVal sAC As String, ByVal iProcessID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SubProcess")
            sSql = "Select SPM_Name from MST_SubProcess_Master where SPM_PM_Id in ( " & iProcessID & ")"
            dtTab = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SubProcess") = dtTab.Rows(i)("SPM_Name").ToString
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssues(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("IssuesNo")
            dt.Columns.Add("IssuesName")
            dt.Columns.Add("Criteria")
            dt.Columns.Add("Condition")
            dt.Columns.Add("RootCause")
            dt.Columns.Add("Impact")
            dt.Columns.Add("RiskCat")
            dt.Columns.Add("Severity")
            dt.Columns.Add("Status")

            sSql = "Select AIT_OpenCloseStatus,AIT_Criteria,AIT_Condition,AIT_RootCause,AIT_Impact,AIT_PKID,AIT_Status,AIT_IssueJobNo,AIT_IssueName,AIT_SeverityID,AIT_CreatedOn,AIT_RiskCategoryID,b.RAM_Name as Severity,c.RAM_Name as RiskCategory From Audit_IssueTracker_details"
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_PKID=AIT_SeverityID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_PKID=AIT_RiskCategoryID And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where AIT_YearID=" & iYearId & " And AIT_FunctionID=" & iFunctionID & " And AIT_AuditCode=" & iAuditID & " And AIT_CustID=" & iCustID & ""
            sSql = sSql & " And AIT_CompID=" & iACID & ""

            dtTab = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("IssuesNo") = dtTab.Rows(i)("AIT_IssueJobNo")
                    dRow("IssuesName") = dtTab.Rows(i)("AIT_IssueName")
                    dRow("Criteria") = dtTab.Rows(i)("AIT_Criteria")
                    dRow("Condition") = dtTab.Rows(i)("AIT_Condition")
                    dRow("RootCause") = dtTab.Rows(i)("AIT_RootCause")
                    dRow("Impact") = dtTab.Rows(i)("AIT_Impact")
                    dRow("RiskCat") = dtTab.Rows(i)("RiskCategory")
                    dRow("Severity") = dtTab.Rows(i)("Severity")
                    dRow("Status") = dtTab.Rows(i)("AIT_OpenCloseStatus")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPreviousIssues(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Dim iAsgid As Integer
        Try
            dt.Columns.Add("IssuesNo")
            dt.Columns.Add("IssuesName")
            dt.Columns.Add("Criteria")
            dt.Columns.Add("Condition")
            dt.Columns.Add("RootCause")
            dt.Columns.Add("Impact")
            dt.Columns.Add("RiskCat")
            dt.Columns.Add("Severity")
            dt.Columns.Add("CreatedOn")
            dt.Columns.Add("Status")
            iAsgid = objDBL.SQLExecuteScalarInt(sAC, "Select Top 1 APM_Id from Audit_APM_Details Where APM_CUSTID = " & iCustID & " And APM_FunctionID = " & iFunctionID & " And APM_Id = " & iAuditID & " And APM_YearID = " & iYearId & " Order by APM_Id desc")
            If iAsgid > 0 Then
                sSql = "Select AIT_Criteria,AIT_Condition,AIT_RootCause,AIT_Impact,AIT_PKID,AIT_Status,AIT_IssueJobNo,AIT_IssueName,AIT_SeverityID,AIT_CreatedOn,AIT_RiskCategoryID,b.RAM_Name as Severity,c.RAM_Name as RiskCategory From Audit_IssueTracker_details"
                sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_PKID=AIT_SeverityID And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_PKID=AIT_RiskCategoryID And c.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where AIT_YearID=" & iYearId & " And AIT_FunctionID=" & iFunctionID & " And AIT_AuditCode=" & iAuditID & " And AIT_CustID=" & iCustID & ""
                sSql = sSql & " And AIT_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                If dtTab.Rows.Count > 0 Then
                    For i = 0 To dtTab.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("IssuesNo") = dtTab.Rows(i)("AIT_IssueJobNo")
                        dRow("IssuesName") = dtTab.Rows(i)("AIT_IssueName")
                        dRow("Criteria") = dtTab.Rows(i)("AIT_Criteria")
                        dRow("Condition") = dtTab.Rows(i)("AIT_Condition")
                        dRow("RootCause") = dtTab.Rows(i)("AIT_RootCause")
                        dRow("Impact") = dtTab.Rows(i)("AIT_Impact")
                        dRow("RiskCat") = dtTab.Rows(i)("RiskCategory")
                        dRow("Severity") = dtTab.Rows(i)("Severity")
                        dRow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("AIT_CreatedOn"), "D")
                        dRow("Status") = ""
                        dt.Rows.Add(dRow)
                    Next
                End If
                Return dt
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImgPath(ByVal sAC As String, ByVal sKey As String) As String
        Dim sSql As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key = '" & sKey & "'"
            GetImgPath = objDBL.SQLGetDescription(sAC, sSql)
            Return GetImgPath
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetReportContentData(ByVal sAC As String, ByVal iCompID As Integer, ByVal iFPTID As Integer) As String
        Dim sSql As String
        Try
            sSql = "select FPT_Title from SAD_Finalisation_Report_Contents Where FPT_ID  = " & iFPTID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckTableExists(ByVal sAC As String, ByVal sTableName As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "SELECT column_name FROM INFORMATION_SCHEMA.Columns where TABLE_NAME='" & sTableName & "' And Column_name Not IN('AuditNo','YearID','CompID','IPAddress')" ''PKID',
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetReportTableDetails(ByVal sAC As String, ByVal sTableName As String) As DataTable
        Dim sSql As String
        Try
            sSql = "SELECT * FROM " & sTableName & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub SaveReportDetails(ByVal sAC As String, ByVal sTableName As String, ByVal dt As DataTable, ByVal sIPAddress As String, ByVal iAuditID As Integer, ByVal iYearID As Integer)
        Dim sSql As String, val As String = ""
        Dim dtDisplay As New DataTable
        Dim arr As New ArrayList
        Dim RemoveSlash As String
        Try
            sSql = "SELECT column_name FROM INFORMATION_SCHEMA.Columns where TABLE_NAME='" & sTableName & "' And Column_name Not IN('PKID','AuditNo','YearID','CompID','IPAddress')"
            dtDisplay = objDBL.SQLExecuteDataTable(sAC, sSql)
            For k = 0 To dtDisplay.Rows.Count - 1
                arr.Add(objclsGRACeGeneral.SafeSpaceSQL(dtDisplay.Rows(k)("column_name")))
            Next
            If dt.Columns.Contains("View") = True Then
                dt.Columns.Remove("View")
            End If
            If dt.Columns.Contains("PKID") = True Then
                dt.Columns.Remove("PKID")
            End If
            If dt.Rows.Count > 0 Then
                For i = 1 To dt.Rows.Count - 1
                    For j = 0 To arr.Count - 1
                        RemoveSlash = dt.Rows(i)(j)
                        val = val & "," & "'" & objclsGRACeGeneral.SafeSpaceSQL(RemoveSlash) & "'"
                    Next
                    val = val & "," & iAuditID & "," & iYearID & "," & "'" & sAC & "'" & "," & "'" & sIPAddress & "'"
                    val = val.Remove(0, 2)
                    val = val.Remove(Len(val) - 1, 1)
                    sSql = "Insert into " & sTableName & " Values('" & val & "')"
                    objDBL.SQLExecuteScalar(sAC, sSql)
                    val = ""
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetGridDetails(ByVal sAC As String, ByVal sTableName As String, ByVal iAuditNoID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "SELECT * FROM " & sTableName & " Where CompID='" & sAC & "' And YearID=" & iYearID & ""
            If iAuditNoID > 0 Then
                sSql = sSql & " And AuditNo=" & iAuditNoID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadHeadingDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select FPT_Id,FPT_Title from SAD_Finalisation_Report_Contents Where FPT_Delflag='A' and FPT_CompID=" & iACID & " order by FPT_Title"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
