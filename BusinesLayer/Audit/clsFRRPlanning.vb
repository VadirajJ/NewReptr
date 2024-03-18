Imports DatabaseLayer
Public Structure strFRR_Planning
    Private RPD_PKID As Integer
    Private RPD_AsgNo As String
    Private RPD_CustID As Integer
    Private RPD_FunID As Integer
    Private RPD_SubFunID As Integer
    Private RPD_Title As String
    Private RPD_Scope As String
    Private RPD_ScheduleStartDate As Date
    Private RPD_ScheduleClosure As Date
    Private RPD_ReviewerTypeID As Integer
    Private RPD_ReviewerID As Integer
    Private RPD_CrBy As Integer
    Private RPD_CrOn As Date
    Private RPD_SubmittedBy As Integer
    Private RPD_SubmittedOn As Date
    Private RPD_UpdatedBy As Integer
    Private RPD_UpdatedOn As Date
    Private RPD_IPaddress As String
    Private RPD_CompID As Integer
    Private RPD_Status As String
    Private RPD_YearID As Integer
    Private RPD_PlanningAttachID As Integer
    Public Property iRPD_PKID() As Integer
        Get
            Return (RPD_PKID)
        End Get
        Set(ByVal Value As Integer)
            RPD_PKID = Value
        End Set
    End Property
    Public Property sRPD_AsgNo() As String
        Get
            Return (RPD_AsgNo)
        End Get
        Set(ByVal Value As String)
            RPD_AsgNo = Value
        End Set
    End Property
    Public Property iRPD_CustID() As Integer
        Get
            Return (RPD_CustID)
        End Get
        Set(ByVal Value As Integer)
            RPD_CustID = Value
        End Set
    End Property
    Public Property iRPD_FunID() As Integer
        Get
            Return (RPD_FunID)
        End Get
        Set(ByVal Value As Integer)
            RPD_FunID = Value
        End Set
    End Property
    Public Property iRPD_SubFunID() As Integer
        Get
            Return (RPD_SubFunID)
        End Get
        Set(ByVal Value As Integer)
            RPD_SubFunID = Value
        End Set
    End Property
    Public Property sRPD_Title() As String
        Get
            Return (RPD_Title)
        End Get
        Set(ByVal Value As String)
            RPD_Title = Value
        End Set
    End Property
    Public Property sRPD_Scope() As String
        Get
            Return (RPD_Scope)
        End Get
        Set(ByVal Value As String)
            RPD_Scope = Value
        End Set
    End Property
    Public Property dRPD_ScheduleStartDate() As Date
        Get
            Return (RPD_ScheduleStartDate)
        End Get
        Set(ByVal Value As Date)
            RPD_ScheduleStartDate = Value
        End Set
    End Property
    Public Property dRPD_ScheduleClosure() As Date
        Get
            Return (RPD_ScheduleClosure)
        End Get
        Set(ByVal Value As Date)
            RPD_ScheduleClosure = Value
        End Set
    End Property
    Public Property iRPD_ReviewerTypeID() As Integer
        Get
            Return (RPD_ReviewerTypeID)
        End Get
        Set(ByVal Value As Integer)
            RPD_ReviewerTypeID = Value
        End Set
    End Property
    Public Property iRPD_ReviewerID() As Integer
        Get
            Return (RPD_ReviewerID)
        End Get
        Set(ByVal Value As Integer)
            RPD_ReviewerID = Value
        End Set
    End Property
    Public Property iRPD_CrBy() As Integer
        Get
            Return (RPD_CrBy)
        End Get
        Set(ByVal Value As Integer)
            RPD_CrBy = Value
        End Set
    End Property
    Public Property dRPD_CrOn() As Date
        Get
            Return (RPD_CrOn)
        End Get
        Set(ByVal Value As Date)
            RPD_CrOn = Value
        End Set
    End Property
    Public Property iRPD_UpdatedBy() As Integer
        Get
            Return (RPD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            RPD_UpdatedBy = Value
        End Set
    End Property
    Public Property dRPD_UpdatedOn() As Date
        Get
            Return (RPD_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            RPD_UpdatedOn = Value
        End Set
    End Property
    Public Property iRPD_SubmittedBy() As Integer
        Get
            Return (RPD_SubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            RPD_SubmittedBy = Value
        End Set
    End Property
    Public Property dRPD_SubmittedOn() As Date
        Get
            Return (RPD_SubmittedOn)
        End Get
        Set(ByVal Value As Date)
            RPD_SubmittedOn = Value
        End Set
    End Property
    Public Property sRPD_IPaddress() As String
        Get
            Return (RPD_IPaddress)
        End Get
        Set(ByVal Value As String)
            RPD_IPaddress = Value
        End Set
    End Property
    Public Property iRPD_CompID() As Integer
        Get
            Return (RPD_CompID)
        End Get
        Set(ByVal Value As Integer)
            RPD_CompID = Value
        End Set
    End Property
    Public Property iRPD_YearID() As Integer
        Get
            Return (RPD_YearID)
        End Get
        Set(ByVal Value As Integer)
            RPD_YearID = Value
        End Set
    End Property
    Public Property iRPD_PlanningAttachID() As Integer
        Get
            Return (RPD_PlanningAttachID)
        End Get
        Set(ByVal Value As Integer)
            RPD_PlanningAttachID = Value
        End Set
    End Property
End Structure
Public Class clsFRRPlanning
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadFRRDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Function")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("FunID")
            dt.Columns.Add("SubFunID")
            dt.Columns.Add("RiskReviewNo")
            dt.Columns.Add("RiskReviewNoPKID")
            dt.Columns.Add("RiskReviewTitle")
            dt.Columns.Add("ScopeOfReview")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("ClosureDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("Days")

            sSql = "Select ENT_ID,ENT_ENTITYNAME,SEM_ENT_ID,SEM_NAME,RPD_PKID,RPD_FunID,RPD_SubFunID,RPD_AsgNo,RPD_Title,RPD_ConductingRRStatus,RPD_ConductingStatus,"
            sSql = sSql & " RPD_ConductingActualStartDate,RPD_ConductingActualClosure,DATEDIFF(day,RPD_ConductingActualStartDate,RPD_ConductingActualClosure) as DayDiff"
            sSql = sSql & " from MST_Entity_master Left Join Risk_RRF_PlanningSchecduling_Details On RPD_CustID =" & iCustID & " And Rpd_FunID=ENT_ID And RPD_Status='Submitted' And RPD_YearID=" & iYearID & " And RPD_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER on SEM_ID=RPD_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Where ENT_CompId=" & iACID & " And Ent_Branch='F' And ENT_DELFLG='A'"
            sSql = sSql & "  Order by ENT_ENTITYNAME"

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("ENT_ID")) = False Then
                        dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("ENT_ENTITYNAME"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RPD_PKID")) = False Then
                        dRow("FunID") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_FunID"))
                        dRow("SubFunID") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_SubFunID"))
                        dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SEM_NAME"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RPD_AsgNo")) = False Then
                        dRow("RiskReviewNoPKID") = dtDetails.Rows(i)("RPD_PKID")
                        dRow("RiskReviewNo") = dtDetails.Rows(i)("RPD_AsgNo")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RPD_Title")) = False Then
                        dRow("RiskReviewTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_Title"))
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RPD_ConductingStatus")) = False Then
                        If dtDetails.Rows(i)("RPD_ConductingStatus") = "Submitted" Then
                            If IsDBNull(dtDetails.Rows(i)("RPD_ConductingActualStartDate")) = False Then
                                dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ConductingActualStartDate"), "F")
                            End If
                            If IsDBNull(dtDetails.Rows(i)("RPD_ConductingActualClosure")) = False Then
                                dRow("ClosureDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ConductingActualClosure"), "F")
                            End If
                            If IsDBNull(dtDetails.Rows(i)("RPD_ConductingActualStartDate")) = False And IsDBNull(dtDetails.Rows(i)("RPD_ConductingActualClosure")) = False Then
                                dRow("Days") = dtDetails.Rows(i)("DayDiff")
                            End If
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RPD_ConductingRRStatus")) = False Then
                        If dtDetails.Rows(i)("RPD_ConductingRRStatus") = 1 Then
                            dRow("Status") = "Open"
                        ElseIf dtDetails.Rows(i)("RPD_ConductingRRStatus") = 2 Then
                            dRow("Status") = "In Progress"
                        ElseIf dtDetails.Rows(i)("RPD_ConductingRRStatus") = 3 Then
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
    Public Function LoadFRRPSDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As New DataColumn
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("Function")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("SubFunctionID")
            dt.Columns.Add("RRAsgID")
            dt.Columns.Add("RiskReviewNo")
            dt.Columns.Add("RiskReviewTitle")
            dt.Columns.Add("ScopeOfReview")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("ClosureDate")
            dt.Columns.Add("Status")

            sSql = "Select ENT_ID,ENT_ENTITYNAME,RPD_Status,SEM_NAME,RPD_PKID,RPD_SubFunID,RPD_AsgNo,RPD_Title,RPD_Scope,RPD_ScheduleStartDate,"
            sSql = sSql & " RPD_ScheduleClosure from Risk_RRF_PlanningSchecduling_Details"
            sSql = sSql & " Left Join MST_Entity_master On ENT_ID=RPD_FunID And RPD_YearID=" & iYearID & " And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_ID=RPD_ReviewerTypeID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=RPD_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Where RPD_CustID=" & iCustID & " And ENT_CompId=" & iACID & " And Ent_Branch='F' And ENT_DELFLG='A' Order by ENT_ENTITYNAME"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("ENT_ID")) = False Then
                        dRow("FunctionID") = dtDetails.Rows(i)("ENT_ID")
                        dRow("Function") = dtDetails.Rows(i)("ENT_ENTITYNAME")
                    End If
                    dRow("RRAsgID") = 0
                    If IsDBNull(dtDetails.Rows(i)("RPD_PKID")) = False Then
                        dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SEM_NAME"))
                        dRow("RRAsgID") = dtDetails.Rows(i)("RPD_PKID")
                        dRow("RiskReviewNo") = dtDetails.Rows(i)("RPD_AsgNo")
                        dRow("RiskReviewTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_Title"))
                        dRow("ScopeOfReview") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_Scope"))
                        dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ScheduleStartDate"), "F")
                        dRow("ClosureDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ScheduleClosure"), "F")
                        dRow("Status") = dtDetails.Rows(i)("RPD_Status")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFRRPSDetails(ByVal sAC As String, ByVal objstrFRRPlanning As strFRR_Planning) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_AsgNo", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrFRRPlanning.sRPD_AsgNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_FunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_SubFunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_SubFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_Title", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objstrFRRPlanning.sRPD_Title
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_Scope ", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objstrFRRPlanning.sRPD_Scope
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ScheduleStartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrFRRPlanning.dRPD_ScheduleStartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ScheduleClosure", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrFRRPlanning.dRPD_ScheduleClosure
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ReviewerTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ReviewerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_IPaddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objstrFRRPlanning.sRPD_IPaddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_CompID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_YearID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_PlanningAttachID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrFRRPlanning.iRPD_PlanningAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_RRF_PlanningSchecduling_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitFRRPS(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPKID As Integer, ByVal iCustID As Integer, iFunctionID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_RRF_PlanningSchecduling_Details Set RPD_ConductingRRStatus=1,RPD_Status ='Submitted',RPD_SubmittedBy=" & iUserID & ",RPD_SubmittedOn=GetDate() Where RPD_PKID=" & iPKID & ""
            sSql = sSql & " And RPD_CustID=" & iCustID & " And RPD_FunID=" & iFunctionID & " And RPD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Function GetFRRNoFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer) As String
    '    Dim sSql As String
    '    Try
    '        sSql = "Select RPD_AsgNo From Risk_RRF_PlanningSchecduling_Details where RPD_YearID=" & iYearID & " And RPD_CompID=" & iACID & " and  RPD_PKID=" & iPKID & ""
    '        Return (objDBL.SQLExecuteScalar(sAC, sSql))
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetFRRPKIDFromSubFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RPD_PKID From Risk_RRF_PlanningSchecduling_Details where RPD_CustID =" & iCustID & " And RPD_YearID=" & iYearID & " And RPD_CompID=" & iACID & " and  RPD_SubFunID=" & iSubFunID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFRRDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFRRAsgID As String, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RPD_RefNo,RPD_ReviewerID,Cust_Name,RPD_PKID,RPD_YearID,RPD_AsgNo,RPD_FunID,RPD_SubFunID,SEM_Name,ENT_EntityName,RPD_PGEDetailId,RPD_ConductPGEDetailId,"
            sSql = sSql & " RPD_Title,RPD_Scope,Convert(Varchar(10),RPD_ScheduleStartDate,103)RPD_ScheduleStartDate,Convert(Varchar(10),RPD_ScheduleClosure,103)RPD_ScheduleClosure,"
            sSql = sSql & " RPD_ReviewerTypeID,RPD_ReviewerID,RPD_Status,Convert(Varchar(10),RPD_ConductingActualStartDate,103)RPD_ConductingActualStartDate,"
            sSql = sSql & " Convert(Varchar(10),RPD_ConductingActualClosure,103)RPD_ConductingActualClosure,RPD_ConductingStatus,RPD_ConductingRRStatus,"
            sSql = sSql & " RPD_PlanningAttachID,RPD_ConductAttachID,RPD_ConductingRemarks from Risk_RRF_PlanningSchecduling_Details"
            sSql = sSql & " Left Join MST_Entity_Master on ENT_ID=RPD_FunID And ENt_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_SUBENTITY_MASTER ON  SEM_ID=RPD_SubFunID And SEM_compid =" & iACID & ""
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=RPD_CustID And CUST_DelFlg = 'A' and cust_Compid=" & iACID & ""
            sSql = sSql & " Where RPD_YearID=" & iYearID & " And RPD_CompID=" & iACID & ""
            If iCustID > 0 Then
                sSql = sSql & " And RPD_CustID=" & iCustID & ""
            End If
            If sType = "PKID" Then
                sSql = sSql & " And RPD_PKID='" & iFRRAsgID & "' "
            ElseIf iFRRAsgID > 0 Then
                sSql = sSql & " And RPD_SubFunID='" & iFRRAsgID & "' "
            End If
            If iYearID > 0 Then
                sSql = sSql & " And RPD_YearID= " & iYearID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFRRPSPKIDFromFunSubFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RPD_PKID from Risk_RRF_PlanningSchecduling_Details Where RPD_CustID=" & iCustID & " And RPD_FunID=" & iFunID & " And RPD_SubFunID=" & iSubFunID & ""
            sSql = sSql & " And RPD_ConductingStatus='Submitted' And RPD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunIDFromFRRPSPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFRRCustID As Integer, ByVal iRPDPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RPD_FunID from Risk_RRF_PlanningSchecduling_Details Where RPD_CustID=" & iFRRCustID & " And RPD_PKID=" & iRPDPKID & " And RPD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunIDFromFRRPSPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFRRCustID As Integer, ByVal iRPDPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RPD_SubFunID from Risk_RRF_PlanningSchecduling_Details Where RPD_CustID=" & iFRRCustID & " And RPD_PKID=" & iRPDPKID & " And RPD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RPD_PGEDetailId From Risk_RRF_PlanningSchecduling_Details Where RPD_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " RPD_FunID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " RPD_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " RPD_CustID=" & iCustID & " And RPD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_RRF_PlanningSchecduling_Details Set RPD_PlanningAttachID=" & iAttachID & ",RPD_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " RPD_FunID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " RPD_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " RPD_YearID=" & iYearID & " And RPD_CustID=" & iCustID & " And RPD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDocIDConductDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                          ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RPD_ConductPGEDetailId From Risk_RRF_PlanningSchecduling_Details Where RPD_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " RPD_FunID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " RPD_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " RPD_CustID=" & iCustID & " And RPD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentConductID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_RRF_PlanningSchecduling_Details Set RPD_PlanningAttachID=" & iAttachID & ",RPD_ConductPGEDetailId=" & iPGDetailID & " Where"
            If iFunctionID > 0 Then
                sSql = sSql & " RPD_FunID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " RPD_PKID=" & iAuditID & " And"
            End If
            sSql = sSql & " RPD_YearID=" & iYearID & " And RPD_CustID=" & iCustID & " And RPD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
