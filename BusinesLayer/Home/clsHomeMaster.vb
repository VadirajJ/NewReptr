Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Class clsHomeMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As clsGRACeGeneral
    Dim objclsGeneralFunctions As clsGeneralFunctions
    Public Function LoadAuditNoDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As DataTable
        Dim sSql As String, sSql1 As String = "", sPartnerID As String = "", sAPMPartnerID As String(), sPartners As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim iCompanyID As Integer = 0
        Try
            dtTab.Columns.Add("AuditNo")
            dtTab.Columns.Add("CustomerName")
            dtTab.Columns.Add("PartnerName")
            dtTab.Columns.Add("Function")

            sSql = "Select APM_ID,APM_CustID,APM_AuditCode,APM_CustID,APM_FunctionID,APM_APMTAStatus,Cust_Name,ENT_EntityName,APM_PartnersID from Audit_APM_Details"
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Entity_Master On ENT_ID=APM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""

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
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("APM_AuditCode")) = False Then
                    dr("AuditNo") = dt.Rows(i)("APM_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("Cust_Name")) = False Then
                    dr("CustomerName") = dt.Rows(i)("Cust_Name")
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
                    dr("PartnerName") = sPartners
                End If
                If IsDBNull(dt.Rows(i)("ENT_EntityName")) = False Then
                    dr("Function") = dt.Rows(i)("ENT_EntityName")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadWorkPaperNoReviewerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As DataTable
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim iCompanyID As Integer = 0
        Try
            dtTab.Columns.Add("AuditNo")
            dtTab.Columns.Add("FunctionID")
            dtTab.Columns.Add("CustID")
            dtTab.Columns.Add("AuditID")
            dtTab.Columns.Add("WPPKID")
            dtTab.Columns.Add("WorkPaperNo")
            dtTab.Columns.Add("WorkPaperDone")
            dtTab.Columns.Add("Attachment")

            sSql = "Select APM_ID,APM_AuditCode,AWP_WorkPaperNo,AWP_AttachID,AWP_PKID,AWP_AuditCode,AWP_CustID,AWP_FunctionID,AWP_WorkPaperDone From Audit_WorkPaper"
            sSql = sSql & " Left Join Audit_APM_Details On APM_ID=AWP_AuditCode And APM_CompID=" & iACID & ""
            sSql = sSql & " where AWP_Status='Auditor Submitted' And AWP_compID=" & iACID & " and AWP_YearID=" & iYearID & ""

            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("APM_AuditCode")) = False Then
                    dr("AuditNo") = dt.Rows(i)("APM_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("AWP_PKID")) = False Then
                    dr("WPPKID") = dt.Rows(i)("AWP_PKID")
                End If
                If IsDBNull(dt.Rows(i)("AWP_AuditCode")) = False Then
                    dr("AuditID") = dt.Rows(i)("AWP_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("AWP_CustID")) = False Then
                    dr("CustID") = dt.Rows(i)("AWP_CustID")
                End If
                If IsDBNull(dt.Rows(i)("AWP_FunctionID")) = False Then
                    dr("FunctionID") = dt.Rows(i)("AWP_FunctionID")
                End If
                If IsDBNull(dt.Rows(i)("AWP_WorkPaperNo")) = False Then
                    dr("WorkPaperNo") = dt.Rows(i)("AWP_WorkPaperNo")
                End If
                If IsDBNull(dt.Rows(i)("AWP_WorkPaperDone")) = False Then
                    dr("WorkPaperDone") = "Workpaper : " & dt.Rows(i)("AWP_WorkPaperDone")
                End If
                If IsDBNull(dt.Rows(i)("AWP_AttachID")) = False Then
                    dr("Attachment") = objDBL.SQLGetDescription(sAC, "Select Count(ATCH_ID) From Edt_Attachments Where ATCH_ID=" & dt.Rows(i)("AWP_AttachID") & " And ATCH_CompID=" & iACID & "")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssuesDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As DataTable
        Dim sSql As String, sWorkPaperNo As String = "", sMaxID As String = "", sSql1 As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim iIssuePKID As Integer, iCompanyID As Integer = 0
        Try
            dtTab.Columns.Add("AuditNo")
            dtTab.Columns.Add("IssueNo")
            dtTab.Columns.Add("FunctionID")
            dtTab.Columns.Add("CustID")
            dtTab.Columns.Add("AuditID")
            dtTab.Columns.Add("WPPKID")
            dtTab.Columns.Add("IssuePKID")
            dtTab.Columns.Add("IssueName")
            dtTab.Columns.Add("Attachment")

            sSql = "Select AIT_PKID,AIT_IssueJobNo,APM_AuditCode,AIT_PKID,AIT_CustID,AIT_AuditCode,AIT_WorkPaperID,AIT_FunctionID,AWP_WorkPaperNo,AIT_IssueName,AIT_AttachID From Audit_IssueTracker_Details"
            sSql = sSql & " Left Join Audit_APM_Details On APM_ID=AIT_AuditCode And APM_CompID=" & iACID & " "
            sSql = sSql & " Left Join Audit_WorkPaper on AWP_AuditCode=AIT_AuditCode  And AIT_FunctionID=AWP_FunctionID  And AWP_PKID=AIT_WorkPaperID "
            sSql = sSql & " where AIT_compID=" & iACID & " and AIT_YearID=" & iYearID & ""

            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AIT_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AIT_CustID=0"
                End If
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("APM_AuditCode")) = False Then
                    dr("AuditNo") = dt.Rows(i)("APM_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("AIT_PKID")) = False Then
                    dr("IssuePKID") = dt.Rows(i)("AIT_PKID")
                End If
                If IsDBNull(dt.Rows(i)("AIT_CustID")) = False Then
                    dr("CustID") = dt.Rows(i)("AIT_CustID")
                End If
                If IsDBNull(dt.Rows(i)("AIT_AuditCode")) = False Then
                    dr("AuditID") = dt.Rows(i)("AIT_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("AIT_WorkPaperID")) = False Then
                    dr("WPPKID") = dt.Rows(i)("AIT_WorkPaperID")
                End If
                If IsDBNull(dt.Rows(i)("AIT_FunctionID")) = False Then
                    dr("FunctionID") = dt.Rows(i)("AIT_FunctionID")
                End If
                If IsDBNull(dt.Rows(i)("AIT_IssueName")) = False Then
                    dr("IssueName") = "Issue Name : " & dt.Rows(i)("AIT_IssueName")
                End If
                If IsDBNull(dt.Rows(i)("AIT_AttachID")) = False Then
                    dr("Attachment") = objDBL.SQLGetDescription(sAC, "Select Count(ATCH_ID) From Edt_Attachments Where ATCH_ID=" & dt.Rows(i)("AIT_AttachID") & " And ATCH_CompID=" & iACID & "")
                End If
                If IsDBNull(dt.Rows(i)("AIT_IssueJobNo")) = False Then
                    iIssuePKID = dt.Rows(i)("AIT_PKID")
                    sWorkPaperNo = dt.Rows(i)("AWP_WorkPaperNo")
                    If iIssuePKID = 0 Then
                        sMaxID = "001"
                    ElseIf iIssuePKID > 0 And iIssuePKID < 10 Then
                        sMaxID = "00" & iIssuePKID
                    ElseIf iIssuePKID >= 10 And iIssuePKID < 100 Then
                        sMaxID = "0" & iIssuePKID
                    Else
                        sMaxID = iIssuePKID
                    End If
                    dr("IssueNo") = sWorkPaperNo & "/IT-" & sMaxID
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadWorkPaperOpenDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As DataTable
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim iCompanyID As Integer = 0
        Try
            dtTab.Columns.Add("AuditNo")
            dtTab.Columns.Add("FunctionID")
            dtTab.Columns.Add("CustID")
            dtTab.Columns.Add("AuditID")
            dtTab.Columns.Add("WPPKID")
            dtTab.Columns.Add("WorkPaperNo")
            dtTab.Columns.Add("Attachment")
            dtTab.Columns.Add("WorkPaperDone")

            sSql = "Select APM_ID,APM_AuditCode,AWP_WorkPaperNo,AWP_AttachID,AWP_PKID,AWP_AuditCode,AWP_CustID,AWP_FunctionID,AWP_WorkPaperDone From Audit_WorkPaper"
            sSql = sSql & " Left Join Audit_APM_Details On APM_ID=AWP_AuditCode And APM_CompID=1"
            sSql = sSql & " where AWP_Status='Rejected' And AWP_OpenCloseStatus=1 And AWP_compID=" & iACID & " and AWP_YearID=" & iYearID & ""

            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("AWP_PKID")) = False Then
                    dr("WPPKID") = dt.Rows(i)("AWP_PKID")
                End If
                If IsDBNull(dt.Rows(i)("AWP_AuditCode")) = False Then
                    dr("AuditID") = dt.Rows(i)("AWP_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("AWP_CustID")) = False Then
                    dr("CustID") = dt.Rows(i)("AWP_CustID")
                End If
                If IsDBNull(dt.Rows(i)("AWP_FunctionID")) = False Then
                    dr("FunctionID") = dt.Rows(i)("AWP_FunctionID")
                End If
                If IsDBNull(dt.Rows(i)("APM_AuditCode")) = False Then
                    dr("AuditNo") = dt.Rows(i)("APM_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("AWP_WorkPaperNo")) = False Then
                    dr("WorkPaperNo") = dt.Rows(i)("AWP_WorkPaperNo")
                End If
                If IsDBNull(dt.Rows(i)("AWP_WorkPaperDone")) = False Then
                    dr("WorkPaperDone") = "Workpaper : " & dt.Rows(i)("AWP_WorkPaperDone")
                End If
                If IsDBNull(dt.Rows(i)("AWP_AttachID")) = False Then
                    dr("Attachment") = objDBL.SQLGetDescription(sAC, "Select Count(ATCH_ID) From Edt_Attachments Where ATCH_ID=" & dt.Rows(i)("AWP_AttachID") & " And ATCH_CompID=" & iACID & "")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Count Audit
    Public Function GetTotalAuditChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql1 As String = "", sSql As String = ""
        Dim iCompanyID As Integer = 0
        Try
            sSql1 = "Select Count(APM_CustID) from Audit_APM_Details Where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " And APM_YearID=" & iYearID & ""
            If sEmpCust = "C" Then
                sSql = "" : sSql = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iCompanyID > 0 Then
                    sSql1 = sSql1 & " And APM_CustID=" & iCompanyID & ""
                Else
                    sSql1 = sSql1 & " And APM_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql1)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerCountChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As DataTable
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Distinct(APM_CustID),Cust_Name from Audit_APM_Details "
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " And APM_YearID=" & iYearID & ""
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
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerNameChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql1 As String = "", sSql As String = ""
        Dim iCompanyID As Integer = 0
        Try
            sSql1 = "Select Count(APM_CustID) from Audit_APM_Details Where APM_CustID=" & iCustId & " And APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " And APM_YearID=" & iYearID & ""
            If sEmpCust = "C" Then
                sSql = "" : sSql = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iCompanyID > 0 Then
                    sSql1 = sSql1 & " And APM_CustID=" & iCompanyID & ""
                Else
                    sSql1 = sSql1 & " And APM_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql1)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Charts Work Paper
    Public Function GetWorkPaperTotalChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_YearID=" & iYearID & ""
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperStartedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_YearID=" & iYearID & " And AWP_Status<>'Submitted'"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperCompletedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_YearID=" & iYearID & " and AWP_Status='Submitted'"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperOpenChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_YearID=" & iYearID & " And AWP_OpenCloseStatus=1"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperClosedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_YearID=" & iYearID & " And AWP_OpenCloseStatus=2"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Chart Issues
    Public Function GetWPTotalChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_YearID=" & iYearID & " and (AWP_Status='Saved' Or AWP_Status='Updated' Or AWP_Status='Submitted')"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AWP_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AWP_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerTotalChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim iCompanyID As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_YearID=" & iYearID & ""
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AIT_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AIT_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerStartedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim iCompanyID As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_YearID=" & iYearID & " and AIT_Status<>'Submitted'"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AIT_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AIT_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerCompletedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_YearID=" & iYearID & " and AIT_Status='Submitted'"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AIT_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AIT_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerOpenChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_YearID=" & iYearID & " And AIT_OpenCloseStatus=1"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AIT_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AIT_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerClosedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sEmpCust As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String, sSql1 As String = ""
        Dim dt As New DataTable
        Dim iCompanyID As Integer = 0
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_YearID=" & iYearID & " And AIT_OpenCloseStatus=2"
            If sEmpCust = "C" Then
                sSql1 = "" : sSql1 = "Select usr_CompanyId From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_Id=" & iUserID & ""
                iCompanyID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                If iCompanyID > 0 Then
                    sSql = sSql & " And AIT_CustID=" & iCompanyID & ""
                Else
                    sSql = sSql & " And AIT_CustID=0"
                End If
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
