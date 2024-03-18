Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Structure strExecutiveSummary
    Private AES_PKID As Integer
    Private AES_YearID As Integer
    Private AES_AuditCode As Integer
    Private AES_CustID As Integer
    Private AES_FunctionID As Integer
    Private AES_IssuanceDate As DateTime
    Private AES_AuditRatingID As Integer
    Private AES_Introduction As String
    Private AES_BusinessOverview As String
    Private AES_AuditScope As String
    Private AES_AuditScopeOut As String
    Private AES_KeyAuditObservation As String
    Private AES_AuditPeriodStartDate As DateTime
    Private AES_AuditPeriodEndDate As DateTime
    Private AES_ActualPeriodStartDate As DateTime
    Private AES_ActualPeriodEndDate As DateTime
    Private AES_AuditRating As String
    Private AES_AuditRemarks As String
    Private AES_CrBy As Integer
    Private AES_UpdatedBy As Integer
    Private AES_IPAddress As String
    Private AES_CompID As Integer
    Private AES_AttchID As Integer
    Public Property iAES_AttchID() As Integer
        Get
            Return (AES_AttchID)
        End Get
        Set(ByVal Value As Integer)
            AES_AttchID = Value
        End Set
    End Property
    Public Property iAES_PKID() As Integer
        Get
            Return (AES_PKID)
        End Get
        Set(ByVal Value As Integer)
            AES_PKID = Value
        End Set
    End Property
    Public Property iAES_YearID() As Integer
        Get
            Return (AES_YearID)
        End Get
        Set(ByVal Value As Integer)
            AES_YearID = Value
        End Set
    End Property
    Public Property iAES_AuditCode() As Integer
        Get
            Return (AES_AuditCode)
        End Get
        Set(ByVal Value As Integer)
            AES_AuditCode = Value
        End Set
    End Property
    Public Property iAES_CustID() As Integer
        Get
            Return (AES_CustID)
        End Get
        Set(ByVal Value As Integer)
            AES_CustID = Value
        End Set
    End Property
    Public Property iAES_FunctionID() As Integer
        Get
            Return (AES_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            AES_FunctionID = Value
        End Set
    End Property
    Public Property dAES_IssuanceDate() As DateTime
        Get
            Return (AES_IssuanceDate)
        End Get
        Set(ByVal Value As DateTime)
            AES_IssuanceDate = Value
        End Set
    End Property
    Public Property iAES_AuditRatingID() As Integer
        Get
            Return (AES_AuditRatingID)
        End Get
        Set(ByVal Value As Integer)
            AES_AuditRatingID = Value
        End Set
    End Property

    Public Property sAES_Introduction() As String
        Get
            Return (AES_Introduction)
        End Get
        Set(ByVal Value As String)
            AES_Introduction = Value
        End Set
    End Property
    Public Property sAES_BusinessOverview() As String
        Get
            Return (AES_BusinessOverview)
        End Get
        Set(ByVal Value As String)
            AES_BusinessOverview = Value
        End Set
    End Property
    Public Property sAES_AuditScope() As String
        Get
            Return (AES_AuditScope)
        End Get
        Set(ByVal Value As String)
            AES_AuditScope = Value
        End Set
    End Property
    Public Property sAES_AuditScopeOut() As String
        Get
            Return (AES_AuditScopeOut)
        End Get
        Set(ByVal Value As String)
            AES_AuditScopeOut = Value
        End Set
    End Property
    Public Property sAES_KeyAuditObservation() As String
        Get
            Return (AES_KeyAuditObservation)
        End Get
        Set(ByVal Value As String)
            AES_KeyAuditObservation = Value
        End Set
    End Property
    Public Property dAES_AuditPeriodStartDate() As DateTime
        Get
            Return (AES_AuditPeriodStartDate)
        End Get
        Set(ByVal Value As DateTime)
            AES_AuditPeriodStartDate = Value
        End Set
    End Property
    Public Property dAES_AuditPeriodEndDate() As DateTime
        Get
            Return (AES_AuditPeriodEndDate)
        End Get
        Set(ByVal Value As DateTime)
            AES_AuditPeriodEndDate = Value
        End Set
    End Property
    Public Property dAES_ActualPeriodStartDate() As DateTime
        Get
            Return (AES_ActualPeriodStartDate)
        End Get
        Set(ByVal Value As DateTime)
            AES_ActualPeriodStartDate = Value
        End Set
    End Property
    Public Property dAES_ActualPeriodEndDate() As DateTime
        Get
            Return (AES_ActualPeriodEndDate)
        End Get
        Set(ByVal Value As DateTime)
            AES_ActualPeriodEndDate = Value
        End Set
    End Property
    Public Property sAES_AuditRating() As String
        Get
            Return (AES_AuditRating)
        End Get
        Set(ByVal Value As String)
            AES_AuditRating = Value
        End Set
    End Property
    Public Property sAES_AuditRemarks() As String
        Get
            Return (AES_AuditRemarks)
        End Get
        Set(ByVal Value As String)
            AES_AuditRemarks = Value
        End Set
    End Property
    Public Property iAES_CrBy() As Integer
        Get
            Return (AES_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AES_CrBy = Value
        End Set
    End Property
    Public Property iAES_UpdatedBy() As Integer
        Get
            Return (AES_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            AES_UpdatedBy = Value
        End Set
    End Property
    Public Property sAES_IPAddress() As String
        Get
            Return (AES_IPAddress)
        End Get
        Set(ByVal Value As String)
            AES_IPAddress = Value
        End Set
    End Property
    Public Property iAES_CompID() As Integer
        Get
            Return (AES_CompID)
        End Get
        Set(ByVal Value As Integer)
            AES_CompID = Value
        End Set
    End Property


End Structure
Public Class ClsExecutiveSummary
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetAuditStartEndDate(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal sType As String) As String
        Dim sSql As String
        Try
            sSql = "Select"
            If sType = "StartDate" Then
                sSql = sSql & " APM_TStartDate"
            ElseIf sType = "EndDate" Then
                sSql = sSql & " APM_TEndDate"
            End If
            sSql = sSql & " From Audit_APM_Details  Where APM_CompID=" & iACID & " And APM_YearID=" & iYearID & "  And APM_ID = " & iAuditID & " And APM_FunctionID = " & iFunctionID & " And APM_CustID=" & iCustID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExecutiveSummaryPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select AES_PKID From Audit_ExecutiveSummary where AES_CompID=" & iACID & " And  AES_YearID=" & iYearID & " "
            If iAuditID > 0 Then
                sSql = sSql & " And AES_AuditCode=" & iAuditID & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And AES_FunctionID=" & iFunID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExecutiveSummaryDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select AES_PKID,AES_YearID,AES_AuditCode,AES_CustID,AES_FunctionID,AES_IssuanceDate,AES_AuditRatingID,AES_AttchID,AES_PGEDetailId,"
            sSql = sSql & " AES_Introduction,AES_BusinessOverview,AES_AuditScope,AES_AuditScopeOut,AES_KeyAuditObservation,AES_Status,"
            sSql = sSql & " AES_AuditPeriodStartDate,AES_AuditPeriodEndDate,AES_ActualPeriodStartDate,AES_ActualPeriodEndDate,AES_AuditRating,"
            sSql = sSql & " AES_AuditRemarks,AES_CrBy,AES_CrOn,AES_IPAddress, AES_CompID from Audit_ExecutiveSummary where AES_CompID=" & iACID & " And AES_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " And AES_AuditCode=" & iAuditID & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And AES_FunctionID=" & iFunID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveExecutiveSummary(ByVal sAC As String, ByVal objExecutiveSummary As strExecutiveSummary) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(24) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_AuditCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_IssuanceDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objExecutiveSummary.dAES_IssuanceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditRatingID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_AuditRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_Introduction", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_Introduction
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_BusinessOverview", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_BusinessOverview
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditScope", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_AuditScope
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditScopeOut", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_AuditScopeOut
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_KeyAuditObservation", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_KeyAuditObservation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditPeriodStartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objExecutiveSummary.dAES_AuditPeriodStartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditPeriodEndDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objExecutiveSummary.dAES_AuditPeriodEndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_ActualPeriodStartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objExecutiveSummary.dAES_ActualPeriodStartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_ActualPeriodEndDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objExecutiveSummary.dAES_ActualPeriodEndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditRating", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_AuditRating
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AuditRemarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_AuditRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objExecutiveSummary.sAES_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AES_AttchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objExecutiveSummary.iAES_AttchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_ExecutiveSummary", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitExecutiveSummary(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iUserID As Integer, ByVal iYearID As Int64)
        Dim sSql As String
        Try
            sSql = "Update Audit_ExecutiveSummary set AES_SubmittedBy=" & iUserID & ",AES_SubmittedOn =Getdate(),AES_Status='Submitted' where AES_AuditCode=" & iAuditID & " and AES_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadExecutiveSummaryDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Dim iMaxId As Integer = 0
        Try
            dt.Columns.Add("IssueNo")
            dt.Columns.Add("ActionDate")
            dt.Columns.Add("IssueHeading")
            dt.Columns.Add("IssueDetails")
            dt.Columns.Add("Condition")
            dt.Columns.Add("Criteria")
            dt.Columns.Add("SuggestedRemedies")
            dt.Columns.Add("Impact")
            dt.Columns.Add("RootCause")
            dt.Columns.Add("Severity")
            dt.Columns.Add("SeverityID")
            dt.Columns.Add("SeverityColor")
            dt.Columns.Add("RiskCategory")
            dt.Columns.Add("Status")

            sSql = "Select AIT_PKID,AIT_Status,AIT_IssueJobNo,AIT_IssueName,AIT_SeverityID,AIT_RiskCategoryID,b.RAM_Name as Severity,c.RAM_Name as RiskCategory,b.RAM_Color,"
            sSql = sSql & " AIT_Criteria,AIT_Condition,AIT_Details,AIT_Impact,AIT_RootCause,AIT_SuggestedRemedies,AIT_OpenCloseStatus,"
            sSql = sSql & " AIT_CreatedOn,AIT_OpenCloseStatus From Audit_IssueTracker_Details"
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_PKID=AIT_SeverityID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_PKID=AIT_RiskCategoryID And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where AIT_YearID=" & iYearID & " And AIT_FunctionID=" & iFunID & " And AIT_AuditCode=" & iAuditID & " And AIT_CompID=" & iACID & ""
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            For j = 0 To dtTab.Rows.Count - 1
                If dtTab.Rows.Count = 0 Then
                    drow = dt.NewRow
                    drow("IssueNo") = 0 : drow("ActionDate") = "" : drow("IssueHeading") = "" : drow("IssueDetails") = "" : drow("Condition") = "" : drow("Criteria") = "" : drow("SuggestedRemedies") = ""
                    drow("Impact") = "" : drow("RootCause") = "" : drow("RiskRatingID") = 0 : drow("RiskRating") = 0 : drow("RiskRatingColor") = ""
                Else
                    drow = dt.NewRow
                    drow("IssueNo") = j + 1
                    If IsDBNull(dtTab.Rows(j)("AIT_CreatedOn")) = False Then
                        drow("ActionDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(j)("AIT_CreatedOn"), "D")
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_Details")) = False Then
                        drow("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("AIT_Details"))
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_Details")) = False Then
                        drow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("AIT_Details"))
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_Condition")) = False Then
                        drow("Condition") = dtTab.Rows(j)("AIT_Condition")
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_Criteria")) = False Then
                        drow("Criteria") = dtTab.Rows(j)("AIT_Criteria")
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_SeverityID")) = False Then
                        drow("SeverityID") = dtTab.Rows(0)("AIT_SeverityID")
                        drow("Severity") = "Severity - " & dtTab.Rows(j)("Severity") & ""
                        drow("SeverityColor") = dtTab.Rows(j)("RAM_Color")
                    End If
                    If IsDBNull(dtTab.Rows(j)("RiskCategory")) = False Then
                        drow("RiskCategory") = dtTab.Rows(j)("RiskCategory")
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_SuggestedRemedies")) = False Then
                        drow("SuggestedRemedies") = dtTab.Rows(j)("AIT_SuggestedRemedies")
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_Impact")) = False Then
                        drow("Impact") = dtTab.Rows(j)("AIT_Impact")
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_RootCause")) = False Then
                        drow("RootCause") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("AIT_RootCause"))
                    End If
                    If IsDBNull(dtTab.Rows(j)("AIT_OpenCloseStatus")) = False Then
                        If dtTab.Rows(j)("AIT_OpenCloseStatus") = 1 Then
                            drow("Status") = "Open"
                        ElseIf dtTab.Rows(j)("AIT_OpenCloseStatus") = 2 Then
                            drow("Status") = "Closed"
                        ElseIf dtTab.Rows(j)("AIT_OpenCloseStatus") = 3 Then
                            drow("Status") = "Deferred"
                        End If
                    End If
                End If
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExecutiveSummaryDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Try
            dt.Columns.Add("IssuanceDate")
            dt.Columns.Add("AuditRating")
            dt.Columns.Add("Introduction")
            dt.Columns.Add("BusinessOverview")
            dt.Columns.Add("AuditScope")
            dt.Columns.Add("AuditScopeOut")
            dt.Columns.Add("KeyAuditObservation")
            dt.Columns.Add("AuditPeriodStartDate")
            dt.Columns.Add("AuditPeriodEndDate")
            dt.Columns.Add("ActualPeriodStartDate")
            dt.Columns.Add("ActualPeriodEndDate")
            dt.Columns.Add("AuditRatings")
            dt.Columns.Add("AuditRemarks")
            sSql = "Select AES_PKID,AES_YearID,MIM_Name,AES_AuditCode,AES_CustID,AES_FunctionID,AES_IssuanceDate,AES_AuditRatingID ,AES_AttchID,"
            sSql = sSql & " AES_Introduction,AES_BusinessOverview, AES_AuditScope, AES_AuditScopeOut,AES_KeyAuditObservation,AES_Status, "
            sSql = sSql & " AES_AuditPeriodStartDate , AES_AuditPeriodEndDate, AES_ActualPeriodStartDate, AES_ActualPeriodEndDate, "
            sSql = sSql & " AES_AuditRating ,AES_AuditRemarks ,AES_CrBy,AES_CrOn,AES_IPAddress, AES_CompID  from Audit_ExecutiveSummary"
            sSql = sSql & " Left Join MST_InherentRisk_Master On AES_AuditRatingID=MIM_ID And MIM_CompID=" & iACID & ""
            sSql = sSql & " Where AES_CompID=" & iACID & " And  AES_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " And AES_AuditCode=" & iAuditID & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And AES_FunctionID=" & iFunID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTab.Rows.Count - 1
                drow = dt.NewRow
                If IsDBNull(dtTab.Rows(i)("AES_IssuanceDate")) = False Then
                    drow("IssuanceDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("AES_IssuanceDate"), "F")
                End If
                If IsDBNull(dtTab.Rows(i)("MIM_Name")) = False Then
                    drow("AuditRating") = dtTab.Rows(i)("MIM_Name")
                End If
                If IsDBNull(dtTab.Rows(i)("AES_Introduction")) = False Then
                    drow("Introduction") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("AES_Introduction"))
                End If
                If IsDBNull(dtTab.Rows(i)("AES_BusinessOverview")) = False Then
                    drow("BusinessOverview") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("AES_BusinessOverview"))
                End If
                If IsDBNull(dtTab.Rows(i)("AES_AuditScope")) = False Then
                    drow("AuditScope") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("AES_AuditScope"))
                End If
                If IsDBNull(dtTab.Rows(i)("AES_AuditScopeOut")) = False Then
                    drow("AuditScopeOut") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("AES_AuditScopeOut"))
                End If
                If IsDBNull(dtTab.Rows(i)("AES_KeyAuditObservation")) = False Then
                    drow("KeyAuditObservation") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("AES_KeyAuditObservation"))
                End If
                If IsDBNull(dtTab.Rows(i)("AES_AuditPeriodStartDate")) = False Then
                    drow("AuditPeriodStartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("AES_AuditPeriodStartDate"), "F")
                End If
                If IsDBNull(dtTab.Rows(i)("AES_AuditPeriodEndDate")) = False Then
                    drow("AuditPeriodEndDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("AES_AuditPeriodEndDate"), "F")
                End If
                If IsDBNull(dtTab.Rows(i)("AES_ActualPeriodStartDate")) = False Then
                    drow("ActualPeriodStartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("AES_ActualPeriodStartDate"), "F")
                End If
                If IsDBNull(dtTab.Rows(i)("AES_ActualPeriodEndDate")) = False Then
                    drow("ActualPeriodEndDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("AES_ActualPeriodEndDate"), "F")
                End If
                If IsDBNull(dtTab.Rows(i)("AES_AuditRating")) = False Then
                    drow("AuditRatings") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("AES_AuditRating"))
                End If
                If IsDBNull(dtTab.Rows(i)("AES_AuditRemarks")) = False Then
                    drow("AuditRemarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("AES_AuditRemarks"))
                End If
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select AES_PGEDetailId From Audit_ExecutiveSummary Where AES_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " AES_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " AES_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " AES_CustID=" & iCustID & " And AES_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_ExecutiveSummary Set AES_AttchID=" & iAttachID & ",AES_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " AES_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " AES_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " AES_YearID=" & iYearID & " And AES_CustID=" & iCustID & " And AES_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
