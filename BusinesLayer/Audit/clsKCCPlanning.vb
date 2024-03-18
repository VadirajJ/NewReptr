Imports DatabaseLayer
Public Structure strKCC_Planning
    Private KCC_PKID As Integer
    Private KCC_CustID As Integer
    Private KCC_AsgNo As String
    Private KCC_FunID As Integer
    Private KCC_SubFunID As Integer
    Private KCC_Title As String
    Private KCC_Scope As String
    Private KCC_ScheduleStartDate As Date
    Private KCC_ScheduleClosure As Date
    Private KCC_ReviewerTypeID As Integer
    Private KCC_ReviewerID As Integer
    Private KCC_CrBy As Integer
    Private KCC_CrOn As Date
    Private KCC_SubmittedBy As Integer
    Private KCC_SubmittedOn As Date
    Private KCC_UpdatedBy As Integer
    Private KCC_UpdatedOn As Date
    Private KCC_IPaddress As String
    Private KCC_CompID As Integer
    Private KCC_Status As String
    Private KCC_YearID As Integer
    Private KCC_PlanningAttachID As Integer
    Public Property iKCC_PKID() As Integer
        Get
            Return (KCC_PKID)
        End Get
        Set(ByVal Value As Integer)
            KCC_PKID = Value
        End Set
    End Property
    Public Property iKCC_CustID() As Integer
        Get
            Return (KCC_CustID)
        End Get
        Set(ByVal Value As Integer)
            KCC_CustID = Value
        End Set
    End Property
    Public Property sKCC_AsgNo() As String
        Get
            Return (KCC_AsgNo)
        End Get
        Set(ByVal Value As String)
            KCC_AsgNo = Value
        End Set
    End Property
    Public Property iKCC_FunID() As Integer
        Get
            Return (KCC_FunID)
        End Get
        Set(ByVal Value As Integer)
            KCC_FunID = Value
        End Set
    End Property
    Public Property iKCC_SubFunID() As Integer
        Get
            Return (KCC_SubFunID)
        End Get
        Set(ByVal Value As Integer)
            KCC_SubFunID = Value
        End Set
    End Property
    Public Property sKCC_Title() As String
        Get
            Return (KCC_Title)
        End Get
        Set(ByVal Value As String)
            KCC_Title = Value
        End Set
    End Property
    Public Property sKCC_Scope() As String
        Get
            Return (KCC_Scope)
        End Get
        Set(ByVal Value As String)
            KCC_Scope = Value
        End Set
    End Property
    Public Property dKCC_ScheduleStartDate() As Date
        Get
            Return (KCC_ScheduleStartDate)
        End Get
        Set(ByVal Value As Date)
            KCC_ScheduleStartDate = Value
        End Set
    End Property
    Public Property dKCC_ScheduleClosure() As Date
        Get
            Return (KCC_ScheduleClosure)
        End Get
        Set(ByVal Value As Date)
            KCC_ScheduleClosure = Value
        End Set
    End Property
    Public Property iKCC_ReviewerTypeID() As Integer
        Get
            Return (KCC_ReviewerTypeID)
        End Get
        Set(ByVal Value As Integer)
            KCC_ReviewerTypeID = Value
        End Set
    End Property
    Public Property iKCC_ReviewerID() As Integer
        Get
            Return (KCC_ReviewerID)
        End Get
        Set(ByVal Value As Integer)
            KCC_ReviewerID = Value
        End Set
    End Property
    Public Property iKCC_CrBy() As Integer
        Get
            Return (KCC_CrBy)
        End Get
        Set(ByVal Value As Integer)
            KCC_CrBy = Value
        End Set
    End Property
    Public Property dKCC_CrOn() As Date
        Get
            Return (KCC_CrOn)
        End Get
        Set(ByVal Value As Date)
            KCC_CrOn = Value
        End Set
    End Property
    Public Property iKCC_UpdatedBy() As Integer
        Get
            Return (KCC_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            KCC_UpdatedBy = Value
        End Set
    End Property
    Public Property dKCC_UpdatedOn() As Date
        Get
            Return (KCC_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            KCC_UpdatedOn = Value
        End Set
    End Property
    Public Property iKCC_SubmittedBy() As Integer
        Get
            Return (KCC_SubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            KCC_SubmittedBy = Value
        End Set
    End Property
    Public Property dKCC_SubmittedOn() As Date
        Get
            Return (KCC_SubmittedOn)
        End Get
        Set(ByVal Value As Date)
            KCC_SubmittedOn = Value
        End Set
    End Property
    Public Property sKCC_IPaddress() As String
        Get
            Return (KCC_IPaddress)
        End Get
        Set(ByVal Value As String)
            KCC_IPaddress = Value
        End Set
    End Property
    Public Property iKCC_CompID() As Integer
        Get
            Return (KCC_CompID)
        End Get
        Set(ByVal Value As Integer)
            KCC_CompID = Value
        End Set
    End Property
    Public Property iKCC_YearID() As Integer
        Get
            Return (KCC_YearID)
        End Get
        Set(ByVal Value As Integer)
            KCC_YearID = Value
        End Set
    End Property
    Public Property iKCC_PlanningAttachID() As Integer
        Get
            Return (KCC_PlanningAttachID)
        End Get
        Set(ByVal Value As Integer)
            KCC_PlanningAttachID = Value
        End Set
    End Property
End Structure
Public Class clsKCCPlanning
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadKCCDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer, iDays As Integer
        Dim sStartDate As Date, sClosureDate As Date
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Function")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("KCCNo")
            dt.Columns.Add("KCCTitle")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("ClosureDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("Days")

            sSql = "Select ENT_ID, ENT_ENTITYNAME,SEM_NAME,KCC_PKID, KCC_AsgNo, KCC_Title, KCC_ConductingKCCStatus, KCC_ConductingStatus,"
            sSql = sSql & " Convert(Varchar(10),KCC_ConductingActualStartDate,103)KCC_ConductingActualStartDate,Convert(Varchar(10),KCC_ConductingActualClosure,103)KCC_ConductingActualClosure"
            sSql = sSql & " from MST_Entity_master Left Join Risk_KCC_PlanningSchecduling_Details On KCC_FunID=ENT_ID And KCC_Status='Submitted' And KCC_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER on SEM_ID=KCC_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Where ENT_CompId=" & iACID & " And Ent_Branch='F' And ENT_DELFLG='A' And KCC_YearID=" & iYearID & ""
            sSql = sSql & "  Order by ENT_ENTITYNAME"

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("ENT_ID")) = False Then
                        dRow("Function") = dtDetails.Rows(i)("ENT_ENTITYNAME")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("KCC_PKID")) = False Then
                        dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SEM_NAME"))
                        dRow("KCCNo") = dtDetails.Rows(i)("KCC_AsgNo")
                        dRow("KCCTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KCC_Title"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("KCC_ConductingStatus")) = False Then
                        If dtDetails.Rows(i)("KCC_ConductingStatus") = "Submitted" Then
                            If IsDBNull(dtDetails.Rows(i)("KCC_ConductingActualStartDate")) = False Then
                                dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KCC_ConductingActualStartDate"), "F")
                            End If
                            If IsDBNull(dtDetails.Rows(i)("KCC_ConductingActualClosure")) = False Then
                                dRow("ClosureDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KCC_ConductingActualClosure"), "F")
                            End If
                            If IsDBNull(dtDetails.Rows(i)("KCC_ConductingActualStartDate")) = False And IsDBNull(dtDetails.Rows(i)("KCC_ConductingActualClosure")) = False Then
                                sStartDate = Date.ParseExact(dtDetails.Rows(i)("KCC_ConductingActualStartDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                                sClosureDate = Date.ParseExact(dtDetails.Rows(i)("KCC_ConductingActualClosure"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                                iDays = DateDiff(DateInterval.Day, sStartDate, sClosureDate)
                                If iDays >= 0 Then
                                    dRow("Days") = iDays + 1
                                End If
                            End If
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("KCC_ConductingKCCStatus")) = False Then
                        If dtDetails.Rows(i)("KCC_ConductingKCCStatus") = 1 Then
                            dRow("Status") = "Open"
                        ElseIf dtDetails.Rows(i)("KCC_ConductingKCCStatus") = 2 Then
                            dRow("Status") = "In Progress"
                        ElseIf dtDetails.Rows(i)("KCC_ConductingKCCStatus") = 3 Then
                            dRow("Status") = "Closed"
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadKCCPSDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("Function")
            dt.Columns.Add("SubFunctionID")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("KCCAsgID")
            dt.Columns.Add("KCCNo")
            dt.Columns.Add("KCCTitle")
            dt.Columns.Add("KCCScopeOfReview")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("ClosureDate")
            dt.Columns.Add("Status")

            sSql = "Select ENT_ID, ENT_ENTITYNAME,SEM_ENT_ID,SEM_NAME, KCC_PKID, KCC_AsgNo, KCC_Title, KCC_Scope, Convert(Varchar(10), KCC_ScheduleStartDate, 103)KCC_ScheduleStartDate, "
            sSql = sSql & " Convert(Varchar(10),KCC_ScheduleClosure,103)KCC_ScheduleClosure,KCC_Status from MST_Entity_master"
            sSql = sSql & " Left Join Risk_KCC_PlanningSchecduling_Details On KCC_FunID= ENT_ID And KCC_CustID=" & iCustID & " And KCC_YearID = " & iYearID & " And KCC_CompID = " & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER on SEM_ID=KCC_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Where ENT_CompId=" & iACID & " And Ent_Branch='F' And ENT_DELFLG='A' Order by ENT_ENTITYNAME"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("ENT_ID")) = False Then
                        dRow("FunctionID") = dtDetails.Rows(i)("ENT_ID")
                        dRow("Function") = dtDetails.Rows(i)("ENT_ENTITYNAME")
                    End If
                    dRow("KCCAsgID") = 0
                    If IsDBNull(dtDetails.Rows(i)("KCC_PKID")) = False Then
                        dRow("SubFunctionID") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SEM_ENT_ID"))
                        dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SEM_NAME"))
                        dRow("KCCAsgID") = dtDetails.Rows(i)("KCC_PKID")
                        dRow("KCCNo") = dtDetails.Rows(i)("KCC_AsgNo")
                        dRow("KCCTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KCC_Title"))
                        dRow("KCCScopeOfReview") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("KCC_Scope"))
                        dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KCC_ScheduleStartDate"), "F")
                        dRow("ClosureDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("KCC_ScheduleClosure"), "F")
                        dRow("Status") = dtDetails.Rows(i)("KCC_Status")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKCCPSPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String
        Dim iKCCPKID As Integer
        Try
            sSql = "Select KCC_PKID From Risk_KCC_PlanningSchecduling_Details where KCC_YearID=" & iYearID & " And KCC_CompID=" & iACID & " and  KCC_SubFunID=" & iSubFunID & ""
            iKCCPKID = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iKCCPKID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadKCCDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iRRAsgID As String, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select KCC_RiskReportReferenceNo,Cust_Name,KCC_PKID,KCC_AsgNo,KCC_FunID,KCC_SubFunID,SEM_ID,ENT_EntityName,SEM_Name,KCC_Title,KCC_Scope,"
            sSql = sSql & " Convert(Varchar(10),KCC_ScheduleStartDate,103)KCC_ScheduleStartDate,Convert(Varchar(10),KCC_ScheduleClosure,103)KCC_ScheduleClosure,"
            sSql = sSql & " KCC_ReviewerTypeID,KCC_ReviewerID,KCC_Status,Convert(Varchar(10),KCC_ConductingActualStartDate,103)KCC_ConductingActualStartDate,"
            sSql = sSql & " Convert(Varchar(10),KCC_ConductingActualClosure,103)KCC_ConductingActualClosure,KCC_ConductingStatus,KCC_ConductingKCCStatus,"
            sSql = sSql & " KCC_ConductingRemarks,KCC_PlanningAttachID,KCC_ConductAttachID,KCC_PlanningPGEDetailId,KCC_ConductPGEDetailId"
            sSql = sSql & " from Risk_KCC_PlanningSchecduling_Details Left Join MST_Entity_Master on ENT_ID=KCC_FunID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER ON SEM_ID=KCC_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_ID=KCC_CustID And CUST_DelFlg='A' and Cust_Compid=" & iACID & ""
            sSql = sSql & " Where KCC_CustID=" & iCustID & " And KCC_YearID=" & iYearID & " And KCC_CompID=" & iACID & ""
            If sType = "PKID" Then
                sSql = sSql & " And KCC_PKID ='" & iRRAsgID & "'"
            ElseIf iRRAsgID > 0 Then
                sSql = sSql & " And KCC_SubFunID  ='" & iRRAsgID & "'"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveKCCPlanningSchdedulingDetails(ByVal sAC As String, ByVal objstrKCCPS As strKCC_Planning) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_AsgNo", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrKCCPS.sKCC_AsgNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_FunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_SubFunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_SubFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_Title", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objstrKCCPS.sKCC_Title
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_Scope ", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objstrKCCPS.sKCC_Scope
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_ScheduleStartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrKCCPS.dKCC_ScheduleStartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_ScheduleClosure", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrKCCPS.dKCC_ScheduleClosure
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_ReviewerTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_ReviewerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_IPaddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objstrKCCPS.sKCC_IPaddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_CompID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_YearID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KCC_PlanningAttachID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrKCCPS.iKCC_PlanningAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_KCC_PlanningSchecduling_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitKCCPS(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPKID As Integer, iFunctionID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_KCC_PlanningSchecduling_Details Set KCC_ConductingKCCStatus=1,KCC_Status ='Submitted',KCC_SubmittedBy=" & iUserID & ",KCC_SubmittedOn=GetDate() Where KCC_PKID=" & iPKID & " "
            sSql = sSql & " And KCC_FunID=" & iFunctionID & " And KCC_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetFunIDFromKCCPSPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iRPDPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select KCC_FunID from Risk_KCC_PlanningSchecduling_Details Where KCC_CustID=" & iCustID & " And KCC_PKID=" & iRPDPKID & " And KCC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunIDFromKCCPSPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iRPDPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select KCC_SubFunID from Risk_KCC_PlanningSchecduling_Details Where KCC_CustID=" & iCustID & " And KCC_PKID=" & iRPDPKID & " And KCC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKCCPSPKIDFromFunSubFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select KCC_PKID from Risk_KCC_PlanningSchecduling_Details Where KCC_FunID=" & iFunID & " And KCC_SubFunID=" & iSubFunID & " And KCC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select KCC_PlanningPGEDetailId From Risk_KCC_PlanningSchecduling_Details Where KCC_YearID=" & iYearID & " And"
            If iAuditID > 0 Then
                sSql = sSql & " KCC_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " KCC_CustID=" & iCustID & " And KCC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_KCC_PlanningSchecduling_Details Set KCC_PlanningAttachID=" & iAttachID & ",KCC_PlanningPGEDetailId=" & iPGDetailID & " Where"
            If iAuditID > 0 Then
                sSql = sSql & " KCC_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " KCC_YearID=" & iYearID & " And KCC_CustID=" & iCustID & " And KCC_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDocIDConductDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select KCC_ConductPGEDetailId From Risk_KCC_PlanningSchecduling_Details Where KCC_YearID=" & iYearID & " And"
            If iAuditID > 0 Then
                sSql = sSql & " KCC_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " KCC_CustID=" & iCustID & " And KCC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateConductAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_KCC_PlanningSchecduling_Details Set KCC_ConductAttachID=" & iAttachID & ",KCC_ConductPGEDetailId=" & iPGDetailID & " Where"
            If iAuditID > 0 Then
                sSql = sSql & " KCC_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " KCC_YearID=" & iYearID & " And KCC_CustID=" & iCustID & " And KCC_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
