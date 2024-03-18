Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Class clsTRACe360
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As clsGeneralFunctions
    Public Function LoadTRACe360Details(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer,
                                        ByVal iCustID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer, ByVal iPartnerID As Integer) As DataTable
        Dim sSql As String, sStr As String = "", sSignOffStatus As String, sAuditTeamsID As String = "", sResource As String = "", sSql1 As String = ""
        Dim sCustomers As String = "", sPartner As String = "", sPartnerID As String = "", sAPMPartnerID As String(), sPartners As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim iClosedStatus As Integer = 0, iAuditClouserStatus As Integer = 0, iIssueTrackerStatus As Integer = 0, iWorkPaperStatus As Integer = 0
        Dim iPlannedSignOffStatus As Integer = 0, iPlannedStatus As Integer = 0
        Dim iCompanyID As Integer = 0
        Dim sArray As Array
        Try
            dtTab.Columns.Add("FunctionID")
            dtTab.Columns.Add("CustID")
            dtTab.Columns.Add("AuditID")
            dtTab.Columns.Add("AuditNo")
            dtTab.Columns.Add("CustomerName")
            dtTab.Columns.Add("LoactionBranch")
            dtTab.Columns.Add("Partners")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("Status")
            dtTab.Columns.Add("ClosedStatus")
            dtTab.Columns.Add("AuditClouserStatus")
            dtTab.Columns.Add("IssueTrackerStatus")
            dtTab.Columns.Add("WorkPaperStatus")
            dtTab.Columns.Add("PlannedSignOffStatus")
            dtTab.Columns.Add("PlannedStatus")
            dtTab.Columns.Add("TeamMember")
            dtTab.Columns.Add("StartDate")
            dtTab.Columns.Add("EndDate")

            sSql = "Select APM_ID,APM_CustID,APM_AuditCode,APM_CustID,APM_FunctionID,APM_APMTAStatus,APM_AuditTeamsID,APM_TStartDate,APM_TEndDate,APM_PartnersID,"
            sSql = sSql & " ENT_ID,Ent_EntityName,Cust_Name,MAS_Description from Audit_APM_Details"
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " left Join MST_Entity_Master On ENT_ID=APM_FunctionID and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUST_LOCATION  On Mas_ID=APM_BranchID And Mas_CompID=" & iACID & ""
            sSql = sSql & " where APM_APMTAStatus='Submitted'  And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iFunction > 0 Then
                sSql = sSql & " And APM_FunctionID = " & iFunction & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " And APM_CustID = " & iCustID & ""
            End If
            If iPartnerID > 0 Then
                sPartner = LoadTRACe360DetailsPartner(sAC, iACID, iYearID, iAuditID, iFunction, iCustID, sEmpCust, iUserID, iPartnerID)
                If sPartner <> "" Then
                    sSql = sSql & " And APM_ID IN(" & sPartner & ")"
                Else
                    GoTo Skipdt
                End If
            Else
                If sEmpCust = "C" Then
                    sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                    iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                    If iCompanyID > 0 Then
                        sSql = sSql & " And APM_CustID=" & iCompanyID & ""
                    Else
                        sSql = sSql & " And APM_CustID=0"
                    End If
                End If
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("ENT_ID")) = False Then
                    dr("FunctionID") = dt.Rows(i)("ENT_ID")
                End If
                If IsDBNull(dt.Rows(i)("APM_CustID")) = False Then
                    dr("CustID") = dt.Rows(i)("APM_CustID")
                End If
                If IsDBNull(dt.Rows(i)("APM_ID")) = False Then
                    dr("AuditID") = dt.Rows(i)("APM_ID")
                End If
                If IsDBNull(dt.Rows(i)("APM_AuditCode")) = False Then
                    dr("AuditNo") = dt.Rows(i)("APM_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("Cust_Name")) = False Then
                    dr("CustomerName") = dt.Rows(i)("Cust_Name")
                End If
                If IsDBNull(dt.Rows(i)("MAS_Description")) = False Then
                    dr("LoactionBranch") = dt.Rows(i)("MAS_Description")
                End If
                sPartners = ""
                If IsDBNull(dt.Rows(i)("APM_PartnersID")) = False Then
                    sPartnerID = dt.Rows(i)("APM_PartnersID")
                    If sPartnerID.Contains(",") Then
                        sAPMPartnerID = sPartnerID.Split(",")
                        If sAPMPartnerID.Length > 1 Then
                            For j = 1 To sAPMPartnerID.Length - 2
                                sPartners = sPartners & "," & objDBL.SQLExecuteScalar(sAC, "Select USr_FullName from sad_userdetails where usr_compID=" & iACID & " And USR_Partner=1 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') And Usr_ID=" & sAPMPartnerID(j) & "")
                            Next
                        End If
                    End If
                End If
                If sPartners.StartsWith(",") Then
                    sPartners = sPartners.Remove(0, 1)
                End If
                If sPartners.EndsWith(",") Then
                    sPartners = sPartners.Remove(Len(sPartners) - 1, 1)
                End If
                If sPartners <> "" Then
                    dr("Partners") = sPartners
                End If
                If IsDBNull(dt.Rows(i)("Ent_EntityName")) = False Then
                    dr("Function") = dt.Rows(i)("Ent_EntityName")
                End If
                If IsDBNull(dt.Rows(i)("APM_TStartDate")) = False Then
                    dr("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TStartDate"), "D")
                End If
                If IsDBNull(dt.Rows(i)("APM_TStartDate")) = False Then
                    dr("EndDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TStartDate"), "D")
                End If
                sAuditTeamsID = "" : dr("StartDate") = "" : dr("EndDate") = "" : dr("TeamMember") = "" : sResource = ""
                If IsDBNull(dt.Rows(i)("APM_AuditTeamsID")) = False Then
                    sAuditTeamsID = dt.Rows(i)("APM_AuditTeamsID")
                    If sAuditTeamsID.StartsWith(",") = True Then
                        sAuditTeamsID = sAuditTeamsID.Remove(0, 1)
                    End If
                    If sAuditTeamsID.EndsWith(",") = True Then
                        sAuditTeamsID = sAuditTeamsID.Remove(Len(sAuditTeamsID) - 1, 1)
                    End If
                    If sAuditTeamsID <> "" Then
                        sArray = sAuditTeamsID.Split(",")
                        For k = 0 To sArray.Length - 1
                            If sArray(k) <> "" Then
                                sResource = sResource & "," & objDBL.SQLExecuteScalar(sAC, "Select Usr_FullName from Sad_UserDetails where Usr_ID=" & sArray(k) & "")
                            End If
                        Next
                        If sResource.StartsWith(",") = True Then
                            sResource = sResource.Remove(0, 1)
                        End If
                        If sResource.EndsWith(",") = True Then
                            sResource = sResource.Remove(Len(sAuditTeamsID) - 1, 1)
                        End If
                    End If
                    dr("TeamMember") = "Team Members : " & sResource & vbNewLine & "Start Date : " & objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TStartDate"), "D") & vbNewLine & "End Date : " & objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TStartDate"), "D") & vbNewLine
                End If

                If CheckAuditSignOff(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Audit Closure"
                    iAuditClouserStatus = iAuditClouserStatus + 1
                    dr("AuditClouserStatus") = iAuditClouserStatus
                    sSignOffStatus = GetAuditSignOffClosed(sAC, iACID, dt.Rows(i)("APM_ID"))
                    If sSignOffStatus = "Submitted" Then
                        dr("Status") = "Completed"
                        iClosedStatus = iClosedStatus + 1
                        dr("ClosedStatus") = iClosedStatus
                        dr("AuditClouserStatus") = iAuditClouserStatus - iClosedStatus
                    End If
                    GoTo Skip
                ElseIf CheckIssueTracker(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Issue Tracker"
                    iIssueTrackerStatus = iIssueTrackerStatus + 1
                    dr("IssueTrackerStatus") = iIssueTrackerStatus
                    GoTo Skip
                ElseIf CheckWorkPaper(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Work Paper"
                    iWorkPaperStatus = iWorkPaperStatus + 1
                    dr("WorkPaperStatus") = iWorkPaperStatus
                    GoTo Skip
                ElseIf ChecPlanSignOff(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Planning Sign Off"
                    iPlannedSignOffStatus = iPlannedSignOffStatus + 1
                    dr("PlannedSignOffStatus") = iPlannedSignOffStatus
                    GoTo Skip
                Else
                    dr("Status") = "Planned"
                    iPlannedStatus = iPlannedStatus + 1
                    dr("PlannedStatus") = iPlannedStatus
                    GoTo Skip
                End If
Skip:           dtTab.Rows.Add(dr)
            Next
Skipdt:     Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTRACe360DetailsPartner(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer,
                                               ByVal iCustID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer, ByVal iPartnerID As Integer) As String
        Dim sSql As String, sSql1 As String = "", sPartnerID As String = "", sAPMPartnerID As String(), sAuditID As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select APM_PartnersID,APM_ID from Audit_APM_Details where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iFunction > 0 Then
                sSql = sSql & " And APM_FunctionID = " & iFunction & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " And APM_CustID = " & iCustID & ""
            End If
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And APM_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And APM_CustID=0"
                End If
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                If IsDBNull(dt.Rows(i)("APM_PartnersID")) = False Then
                    sPartnerID = dt.Rows(i)("APM_PartnersID")
                    If sPartnerID.Contains(",") Then
                        sAPMPartnerID = sPartnerID.Split(",")
                        If sAPMPartnerID.Length > 1 Then
                            For j = 1 To sAPMPartnerID.Length - 2
                                If sAPMPartnerID(j) = iPartnerID Then
                                    sAuditID = sAuditID & "," & dt.Rows(i)("APM_ID")
                                End If
                            Next
                        End If
                    End If
                End If
            Next
            If sAuditID.StartsWith(",") Then
                sAuditID = sAuditID.Remove(0, 1)
            End If
            If sAuditID.EndsWith(",") Then
                sAuditID = sAuditID.Remove(Len(sAuditID) - 1, 1)
            End If
            Return sAuditID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTeamsID(ByVal sAC As String, ByVal iACID As Integer, ByVal sAuditTeamsID As String) As String
        Dim sSql As String = ""
        Try
            If sAuditTeamsID.StartsWith(",") = True Then
                sAuditTeamsID = sAuditTeamsID.Remove(0, 1)
            End If
            If sAuditTeamsID.EndsWith(",") = True Then
                sAuditTeamsID = sAuditTeamsID.Remove(Len(sAuditTeamsID) - 1, 1)
            End If
            If sAuditTeamsID > 0 Then
                sSql = "Select Usr_FullName from Sad_Userdetails Where Usr_COmpID=" & iACID & " And Usr_Id In (" & sAuditTeamsID & ") Order By Usr_FullName "
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAuditSignOff(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select ASO_PKID from Audit_SignOff where ASO_AuditCodeID =" & iAuditID & " And ASO_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditSignOffClosed(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ASO_Status from Audit_SignOff where ASO_AuditCodeID =" & iAuditID & " And ASO_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckIssueTracker(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select AIT_PKID from Audit_IssueTracker_Details where AIT_AuditCode  =" & iAuditID & " And AIT_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select AWP_PKID from Audit_WorkPaper where AWP_AuditCode  =" & iAuditID & " And AWP_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ChecPlanSignOff(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select APSO_ID from Audit_PlanSignOff where APSO_AuditCode =" & iAuditID & " And APSO_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTRACe360DetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer,
                                       ByVal iCustID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer, ByVal iPartnerID As Integer) As DataTable
        Dim sSql As String, sPartner As String = "", sSql1 As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim sSignOffStatus As String
        Dim iCompanyID As Integer = 0
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("AuditNo")
            dtTab.Columns.Add("CustomerName")
            dtTab.Columns.Add("LoactionBranch")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("Status")

            sSql = "Select APM_ID,APM_CustID,APM_AuditCode,APM_CustID,APM_FunctionID,APM_APMTAStatus,"
            sSql = sSql & " ENT_ID,Ent_EntityName,Cust_Name,Mas_Loc_Address from Audit_APM_Details"
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " left Join MST_Entity_Master On ENT_ID=APM_FunctionID and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUST_LOCATION  On Mas_ID=Cust_ID And Mas_CompID=" & iACID & ""
            sSql = sSql & " where APM_APMTAStatus='Submitted'  And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iFunction > 0 Then
                sSql = sSql & " And APM_FunctionID = " & iFunction & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " And APM_CustID = " & iCustID & ""
            End If
            If iPartnerID > 0 Then
                sPartner = LoadTRACe360DetailsPartner(sAC, iACID, iYearID, iAuditID, iFunction, iCustID, sEmpCust, iUserID, iPartnerID)
                If sPartner <> "" Then
                    sSql = sSql & " And APM_ID IN(" & sPartner & ")"
                Else
                    GoTo Skipdt
                End If
            Else
                If sEmpCust = "C" Then
                    sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                    iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                    If iCompanyID > 0 Then
                        sSql = sSql & " And APM_CustID=" & iCompanyID & ""
                    Else
                        sSql = sSql & " And APM_CustID=0"
                    End If
                End If
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("APM_AuditCode")) = False Then
                    dr("AuditNo") = dt.Rows(i)("APM_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("Cust_Name")) = False Then
                    dr("CustomerName") = dt.Rows(i)("Cust_Name")
                End If
                If IsDBNull(dt.Rows(i)("Mas_Loc_Address")) = False Then
                    dr("LoactionBranch") = dt.Rows(i)("Mas_Loc_Address")
                End If
                If IsDBNull(dt.Rows(i)("Ent_EntityName")) = False Then
                    dr("Function") = dt.Rows(i)("Ent_EntityName")
                End If
                If CheckAuditSignOff(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    sSignOffStatus = GetAuditSignOffClosed(sAC, iACID, dt.Rows(i)("APM_ID"))
                    If sSignOffStatus = "Submitted" Then
                        dr("Status") = "Completed"
                        GoTo Skip
                    Else
                        dr("Status") = "Audit Closure"
                        GoTo Skip
                    End If
                ElseIf CheckIssueTracker(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Issue Tracker"
                    GoTo Skip
                ElseIf CheckIssueTracker(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Issue Tracker"
                    GoTo Skip
                ElseIf CheckWorkPaper(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Work Paper"
                    GoTo Skip
                ElseIf ChecPlanSignOff(sAC, iACID, dt.Rows(i)("APM_ID")) = True Then
                    dr("Status") = "Planning Sign Off"
                    GoTo Skip
                Else
                    dr("Status") = "Planned"
                    GoTo Skip
                End If
Skip:           dtTab.Rows.Add(dr)
            Next
Skipdt:     Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalAuditCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer,
                                       ByVal iCustID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer, ByVal iPartnerID As Integer) As Integer
        Dim sSql As String, sSql1 As String = "", sPartner As String = ""
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(*) from Audit_APM_Details"
            sSql = sSql & " Where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iFunction > 0 Then
                sSql = sSql & " And APM_FunctionID = " & iFunction & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " And APM_CustID = " & iCustID & ""
            End If
            If iPartnerID > 0 Then
                sPartner = LoadTRACe360DetailsPartner(sAC, iACID, iYearID, iAuditID, iFunction, iCustID, sEmpCust, iUserID, iPartnerID)
                If sPartner <> "" Then
                    sSql = sSql & " And APM_ID IN(" & sPartner & ")"
                Else
                    Return 0
                End If
            Else
                If sEmpCust = "C" Then
                    sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                    iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                    If iCompanyID > 0 Then
                        sSql = sSql & " And APM_CustID=" & iCompanyID & ""
                    Else
                        sSql = sSql & " And APM_CustID=0"
                    End If
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
