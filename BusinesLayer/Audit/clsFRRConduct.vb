Imports DatabaseLayer
Public Structure strFRR_Conduct
    Private RPD_PKID As Integer
    Private RPD_AsgNo As String
    Private RPD_CustID As Integer
    Private RPD_FunID As Integer
    Private RPD_SubFunID As Integer
    Private RPD_ConductingActualStartDate As Date
    Private RPD_ConductingActualClosure As Date
    Private RPD_ConductingRemarks As String
    Private RPD_ConductingCrBy As Integer
    Private RPD_ConductingCrOn As Date
    Private RPD_ConductingUpdatedBy As Integer
    Private RPD_ConductingUpdatedOn As Date
    Private RPD_ConductingIPaddress As String
    Private RPD_CompID As Integer
    Private RPD_ConductingRRStatus As String
    Private RPD_YearID As Integer
    Private RPD_ConductAttachID As Integer
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
    Public Property iRPD_SubFunID() As Integer
        Get
            Return (RPD_SubFunID)
        End Get
        Set(ByVal Value As Integer)
            RPD_SubFunID = Value
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
    Public Property dRPD_ConductingActualStartDate() As Date
        Get
            Return (RPD_ConductingActualStartDate)
        End Get
        Set(ByVal Value As Date)
            RPD_ConductingActualStartDate = Value
        End Set
    End Property
    Public Property dRPD_ConductingActualClosure() As Date
        Get
            Return (RPD_ConductingActualClosure)
        End Get
        Set(ByVal Value As Date)
            RPD_ConductingActualClosure = Value
        End Set
    End Property
    Public Property sRPD_ConductingRemarks() As String
        Get
            Return (RPD_ConductingRemarks)
        End Get
        Set(ByVal Value As String)
            RPD_ConductingRemarks = Value
        End Set
    End Property
    Public Property iRPD_ConductingCrBy() As Integer
        Get
            Return (RPD_ConductingCrBy)
        End Get
        Set(ByVal Value As Integer)
            RPD_ConductingCrBy = Value
        End Set
    End Property
    Public Property dRPD_ConductingCrOn() As Date
        Get
            Return (RPD_ConductingCrOn)
        End Get
        Set(ByVal Value As Date)
            RPD_ConductingCrOn = Value
        End Set
    End Property
    Public Property iRPD_ConductingUpdatedBy() As Integer
        Get
            Return (RPD_ConductingUpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            RPD_ConductingUpdatedBy = Value
        End Set
    End Property
    Public Property dRPD_ConductingUpdatedOn() As Date
        Get
            Return (RPD_ConductingUpdatedOn)
        End Get
        Set(ByVal Value As Date)
            RPD_ConductingUpdatedOn = Value
        End Set
    End Property
    Public Property sRPD_ConductingIPaddress() As String
        Get
            Return (RPD_ConductingIPaddress)
        End Get
        Set(ByVal Value As String)
            RPD_ConductingIPaddress = Value
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
    Public Property sRPD_ConductingRRStatus() As String
        Get
            Return (RPD_ConductingRRStatus)
        End Get
        Set(ByVal Value As String)
            RPD_ConductingRRStatus = Value
        End Set
    End Property
    Public Property iRPD_ConductAttachID() As Integer
        Get
            Return (RPD_ConductAttachID)
        End Get
        Set(ByVal Value As Integer)
            RPD_ConductAttachID = Value
        End Set
    End Property
End Structure
Public Class clsFRRConduct
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadFRRConductDashboardInGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
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
            dt.Columns.Add("RRAsgID")
            dt.Columns.Add("RiskReviewNo")
            dt.Columns.Add("RiskReviewTitle")
            dt.Columns.Add("ScopeOfReview")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("ClosureDate")
            dt.Columns.Add("ActualStartDate")
            dt.Columns.Add("ActualClosureDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("ConductStatus")
            dt.Columns.Add("Remarks")

            sSql = "Select ENT_ID,ENT_ENTITYNAME,SEM_ID,SEM_NAME,RPD_SubFunID,RPD_PKID,RPD_AsgNo,RPD_Title,RPD_Scope,RPD_ScheduleStartDate,RPD_ConductingRemarks,"
            sSql = sSql & " RPD_ScheduleClosure,RPD_ConductingStatus,RPD_ConductingRRStatus,RPD_ConductingStatus,RPD_ConductingActualStartDate,"
            sSql = sSql & " RPD_ConductingActualClosure from Risk_RRF_PlanningSchecduling_Details "
            sSql = sSql & " Left Join MST_Entity_master On ENT_ID = Rpd_FunID And RPD_Status='Submitted' And RPD_YearID=" & iYearID & " And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=RPD_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Where RPD_CustID=" & iCustID & " And ENT_CompId=" & iACID & " And Ent_Branch='F' And ENT_DELFLG='A'"
            sSql = sSql & " Order by ENT_ENTITYNAME"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("ENT_ID")) = False Then
                        dRow("FunctionID") = dtDetails.Rows(i)("ENT_ID")
                        dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("ENT_ENTITYNAME"))
                    End If
                    dRow("RRAsgID") = 0
                    If IsDBNull(dtDetails.Rows(i)("RPD_PKID")) = False Then
                        dRow("RRAsgID") = dtDetails.Rows(i)("RPD_PKID")
                        If IsDBNull(dtDetails.Rows(i)("SEM_ID")) = False Then
                            dRow("SubFunctionID") = dtDetails.Rows(i)("SEM_ID")
                            dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SEM_NAME"))
                        End If
                        dRow("RRAsgID") = dtDetails.Rows(i)("RPD_PKID")
                        dRow("RiskReviewNo") = dtDetails.Rows(i)("RPD_AsgNo")
                        dRow("RiskReviewTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_Title"))
                        dRow("ScopeOfReview") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_Scope"))
                        dRow("StartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ScheduleStartDate"), "F")
                        dRow("ClosureDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ScheduleClosure"), "F")
                        If IsDBNull(dtDetails.Rows(i)("RPD_ConductingRemarks")) = False Then
                            dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RPD_ConductingRemarks"))
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RPD_ConductingActualStartDate")) = False Then
                        dRow("ActualStartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ConductingActualStartDate"), "F")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RPD_ConductingActualClosure")) = False Then
                        If objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ConductingActualClosure"), "F").Contains("1900") = False Then
                            dRow("ActualClosureDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RPD_ConductingActualClosure"), "F")
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
                    Else
                        dRow("Status") = "Open"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RPD_ConductingStatus")) = False Then
                        dRow("ConductStatus") = dtDetails.Rows(i)("RPD_ConductingStatus")
                    Else
                        dRow("ConductStatus") = ""
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFRRConductDetails(ByVal sAC As String, ByVal objstrFRRConduct As strFRR_Conduct) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_FunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_SubFunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_SubFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductingActualStartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrFRRConduct.dRPD_ConductingActualStartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductingActualClosure", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrFRRConduct.dRPD_ConductingActualClosure
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductingRemarks", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objstrFRRConduct.sRPD_ConductingRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductingCrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_ConductingCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductingUpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_ConductingUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductingIPaddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objstrFRRConduct.sRPD_ConductingIPaddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_CompID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_YearID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductingRRStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrFRRConduct.sRPD_ConductingRRStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RPD_ConductAttachID", OleDb.OleDbType.Integer, 500)
            ObjParam(iParamCount).Value = objstrFRRConduct.iRPD_ConductAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_RRF_PlanningSchecduling_Conducting", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitFRRConduct(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_RRF_PlanningSchecduling_Details Set RPD_ConductingStatus ='Submitted',RPD_ConductingSubmittedBy=" & iUserID & ",RPD_ConductingSubmittedOn=GetDate() Where RPD_PKID=" & iPKID & " "
            sSql = sSql & " And RPD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
