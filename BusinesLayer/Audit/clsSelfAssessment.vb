Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports DatabaseLayer
Imports System
Imports Microsoft.VisualBasic
Imports System.Configuration
Imports BusinesLayer
Imports System.Security.Cryptography
Public Class clsSelfAssessment
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral

    Dim SA_PKID As Integer
    Dim SA_YearID As Integer
    Dim SA_CompanyID As Integer
    Dim SA_SectionID As Integer
    Dim SA_MonthID As Integer
    Dim SA_Code As String
    Dim SA_StartDate As Date
    Dim SA_EndDate As Date
    Dim SA_CrBy As Integer
    Dim SA_UpdatedBy As Integer
    Dim SA_SubmittedBy As Integer
    Dim SA_Status As String
    Dim SA_IPAddress As String
    Dim SA_CompID As Integer

    Dim SAC_PKID As Integer
    Dim SAC_SAID As Integer
    Dim SAC_YEARID As Integer
    Dim SAC_MonthID As Integer
    Dim SAC_CompanyID As Integer
    Dim SAC_SectionID As Integer
    Dim SAC_SUBSECTIONID As Integer
    Dim SAC_PROCESSID As Integer
    Dim SAC_SUBPROCESSID As Integer
    Dim SAC_RESULT As Integer
    Dim SAC_Notes As String
    Dim SAC_IntNotes As String
    Dim SAC_CREATEDBY As Integer
    Dim SAC_UPDATEDBY As Integer
    Dim SAC_IPAddress As String
    Dim SAC_CompID As Integer

    Public Property iSA_PKID() As Integer
        Get
            Return (SA_PKID)
        End Get
        Set(ByVal Value As Integer)
            SA_PKID = Value
        End Set
    End Property
    Public Property iSA_YearID() As Integer
        Get
            Return (SA_YearID)
        End Get
        Set(ByVal Value As Integer)
            SA_YearID = Value
        End Set
    End Property
    Public Property iSA_CompanyID() As Integer
        Get
            Return (SA_CompanyID)
        End Get
        Set(ByVal Value As Integer)
            SA_CompanyID = Value
        End Set
    End Property
    Public Property iSA_SectionID() As Integer
        Get
            Return (SA_SectionID)
        End Get
        Set(ByVal Value As Integer)
            SA_SectionID = Value
        End Set
    End Property
    Public Property iSA_MonthID() As Integer
        Get
            Return (SA_MonthID)
        End Get
        Set(ByVal Value As Integer)
            SA_MonthID = Value
        End Set
    End Property
    Public Property sSA_Code() As String
        Get
            Return (SA_Code)
        End Get
        Set(ByVal Value As String)
            SA_Code = Value
        End Set
    End Property
    Public Property dSA_StartDate() As Date
        Get
            Return (SA_StartDate)
        End Get
        Set(ByVal Value As Date)
            SA_StartDate = Value
        End Set
    End Property
    Public Property dSA_EndDate() As Date
        Get
            Return (SA_EndDate)
        End Get
        Set(ByVal Value As Date)
            SA_EndDate = Value
        End Set
    End Property
    Public Property iSA_CrBy() As Integer
        Get
            Return (SA_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SA_CrBy = Value
        End Set
    End Property
    Public Property iSA_UpdatedBy() As Integer
        Get
            Return (SA_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SA_UpdatedBy = Value
        End Set
    End Property
    Public Property iSA_SubmittedBy() As Integer
        Get
            Return (SA_SubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            SA_SubmittedBy = Value
        End Set
    End Property
    Public Property sSA_Status() As String
        Get
            Return (SA_Status)
        End Get
        Set(ByVal Value As String)
            SA_Status = Value
        End Set
    End Property
    Public Property sSA_IPAddress() As String
        Get
            Return (SA_IPAddress)
        End Get
        Set(ByVal Value As String)
            SA_IPAddress = Value
        End Set
    End Property
    Public Property iSA_CompID() As Integer
        Get
            Return (SA_CompID)
        End Get
        Set(ByVal Value As Integer)
            SA_CompID = Value
        End Set
    End Property

    'Checklist
    Public Property iSAC_PKID() As Integer
        Get
            Return (SAC_PKID)
        End Get
        Set(ByVal Value As Integer)
            SAC_PKID = Value
        End Set
    End Property
    Public Property iSAC_SAID() As Integer
        Get
            Return (SAC_SAID)
        End Get
        Set(ByVal Value As Integer)
            SAC_SAID = Value
        End Set
    End Property
    Public Property iSAC_YEARID() As Integer
        Get
            Return (SAC_YEARID)
        End Get
        Set(ByVal Value As Integer)
            SAC_YEARID = Value
        End Set
    End Property
    Public Property iSAC_MonthID() As Integer
        Get
            Return (SAC_MonthID)
        End Get
        Set(ByVal Value As Integer)
            SAC_MonthID = Value
        End Set
    End Property
    Public Property iSAC_CompanyID() As Integer
        Get
            Return (SAC_CompanyID)
        End Get
        Set(ByVal Value As Integer)
            SAC_CompanyID = Value
        End Set
    End Property
    Public Property iSAC_SectionID() As Integer
        Get
            Return (SAC_SectionID)
        End Get
        Set(ByVal Value As Integer)
            SAC_SectionID = Value
        End Set
    End Property
    Public Property iSAC_SUBSECTIONID() As Integer
        Get
            Return (SAC_SUBSECTIONID)
        End Get
        Set(ByVal Value As Integer)
            SAC_SUBSECTIONID = Value
        End Set
    End Property
    Public Property iSAC_PROCESSID() As Integer
        Get
            Return (SAC_PROCESSID)
        End Get
        Set(ByVal Value As Integer)
            SAC_PROCESSID = Value
        End Set
    End Property
    Public Property iSAC_SUBPROCESSID() As Integer
        Get
            Return (SAC_SUBPROCESSID)
        End Get
        Set(ByVal Value As Integer)
            SAC_SUBPROCESSID = Value
        End Set
    End Property
    Public Property iSAC_RESULT() As Integer
        Get
            Return (SAC_RESULT)
        End Get
        Set(ByVal Value As Integer)
            SAC_RESULT = Value
        End Set
    End Property
    Public Property sSAC_Notes() As String
        Get
            Return (SAC_Notes)
        End Get
        Set(ByVal Value As String)
            SAC_Notes = Value
        End Set
    End Property
    Public Property sSAC_IntNotes() As String
        Get
            Return (SAC_IntNotes)
        End Get
        Set(ByVal Value As String)
            SAC_IntNotes = Value
        End Set
    End Property

    Public Property iSAC_CREATEDBY() As Integer
        Get
            Return (SAC_CREATEDBY)
        End Get
        Set(ByVal Value As Integer)
            SAC_CREATEDBY = Value
        End Set
    End Property
    Public Property iSAC_UPDATEDBY() As Integer
        Get
            Return (SAC_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            SAC_UPDATEDBY = Value
        End Set
    End Property
    Public Property sSAC_IPAddress() As String
        Get
            Return (SAC_IPAddress)
        End Get
        Set(ByVal Value As String)
            SAC_IPAddress = Value
        End Set
    End Property
    Public Property iSAC_CompID() As Integer
        Get
            Return (SAC_CompID)
        End Get
        Set(ByVal Value As Integer)
            SAC_CompID = Value
        End Set
    End Property
    Public Function LoadAllAudit(ByVal sAC As String, ByVal iAcID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select CAS_ID, CAS_SectionName from crpa_section where CAS_Delflg='A' and cas_id<>5 and cas_compid=" & iAcID & " order by CAS_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDetails(ByVal sAC As String, ByVal iAcID As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select * from SAssessment_Mas where SA_PKID=" & iPKID & " and SA_compid=" & iAcID & " "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRatingValueScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iAUDID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CVR_Point As IDText,concat(CVR_Point,' - ',CVR_name) As NameText from CRPA_ValueRating Where CVR_AuditId='" & iAUDID & "' and CVR_CompID=" & iACID & " and CVR_Flag='A' order by IDText"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssessmentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iMonthID As Integer, ByVal iSecID As Integer, ByVal iCompanyId As Integer) As DataTable
        Dim sSql As String
        Dim i As Integer
        Dim dtTab As New DataTable, dt As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SlNo")
            dtTab.Columns.Add("SA_PKID")
            dtTab.Columns.Add("SA_code")
            dtTab.Columns.Add("Assessment")
            dtTab.Columns.Add("Company")
            dtTab.Columns.Add("Month")
            dtTab.Columns.Add("StartDate")
            dtTab.Columns.Add("EndDate")
            dtTab.Columns.Add("Status")

            sSql = "select SA_PKID, SA_code, SA_YearID, SA_MonthID, SA_SectionID, SA_CompanyID, SA_StartDate, SA_EndDate, SA_Status,b.CUST_NAME as Company,c.CAS_SECTIONNAME as AssessName "
            sSql = sSql & " from SAssessment_Mas a "
            sSql = sSql & " Left Join sad_customer_master b on a.SA_CompanyID=b.cust_id and b.cust_compid=" & iACID & ""
            sSql = sSql & " Left Join CRPA_Section c on a.SA_SectionID=c.cas_id and c.cas_compid=" & iACID & ""
            sSql = sSql & " where SA_YearID=" & iYearID & " "
            If iSecID > 0 Then
                sSql = sSql & " and SA_SectionID=" & iSecID & ""
            End If
            If iCompanyId > 0 Then
                sSql = sSql & " and SA_CompanyID=" & iCompanyId & ""
            End If
            If iMonthID > 0 Then
                sSql = sSql & " and SA_MonthID=" & iMonthID & ""
            End If
            sSql = sSql & " Order by SA_PKID Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dt.Rows.Count <> 0 Then
                For i = 0 To dt.Rows.Count - 1
                    drow = dtTab.NewRow
                    drow("SA_PKID") = dt.Rows(i).Item("SA_PKID")
                    drow("SlNo") = i + 1
                    If IsDBNull(dt.Rows(i).Item("SA_code")) = False Then
                        drow("SA_code") = dt.Rows(i).Item("SA_code")
                    End If
                    If IsDBNull(dt.Rows(i).Item("AssessName")) = False Then
                        drow("Assessment") = dt.Rows(i).Item("AssessName")
                    End If
                    If IsDBNull(dt.Rows(i).Item("Company")) = False Then
                        drow("Company") = dt.Rows(i).Item("Company")
                    End If
                    If IsDBNull(dt.Rows(i).Item("SA_MonthID")) = False Then
                        drow("Month") = objclsGeneralFunctions.GetMonthNameFromMothID(dt.Rows(i).Item("SA_MonthID"))
                    End If
                    If IsDBNull(dt.Rows(i).Item("SA_StartDate")) = False Then
                        drow("StartDate") = dt.Rows(i).Item("SA_StartDate").ToString.Substring(0, 10)
                    End If
                    If IsDBNull(dt.Rows(i).Item("SA_EndDate")) = False Then
                        drow("EndDate") = dt.Rows(i).Item("SA_EndDate").ToString.Substring(0, 10)
                    End If
                    If IsDBNull(dt.Rows(i).Item("SA_Status")) = False Then
                        drow("Status") = dt.Rows(i).Item("SA_Status")
                    End If
                    dtTab.Rows.Add(drow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForData(ByVal sAC As String, ByVal iACID As Integer, ByVal iCompanyID As Integer, ByVal iSectionID As Integer, ByVal iYearID As Integer, ByVal iMonthId As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "select * from SAssessment_Mas  where SA_MonthID=" & iMonthId & " and SA_CompanyID='" & iCompanyID & "' "
            sSql = sSql & " and SA_SectionID='" & iSectionID & "' and SA_YearID = '" & iYearID & "' and SA_CompID='" & iACID & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssessCode(ByVal sAC As String, ByVal sYearName As String, ByVal iYearId As Integer) As String
        Dim iMaxID As Integer
        Dim sMaxID As String = "", sJobCode As String = "", sModuleCode As String = ""
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from SAssessment_Mas where sa_yearID=" & iYearId & "")
            sModuleCode = "SA"

            If iMaxID = 1 Then
                sMaxID = "001"
            ElseIf iMaxID > 0 And iMaxID < 10 Then
                sMaxID = "00" & iMaxID
            ElseIf iMaxID >= 10 And iMaxID < 100 Then
                sMaxID = "0" & iMaxID
            Else
                sMaxID = iMaxID
            End If
            sJobCode = "TRACe/" & sModuleCode & "/" & sYearName & "/" & sMaxID
            Return sJobCode

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridDetails(ByVal sNameSpace As String, ByVal iSectionID As Integer, ByVal iACID As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("PKID")
        dtDisplay.Columns.Add("AssignID")
        dtDisplay.Columns.Add("SubSection")
        dtDisplay.Columns.Add("Process")
        dtDisplay.Columns.Add("SubProcess")
        dtDisplay.Columns.Add("SubSectionID")
        dtDisplay.Columns.Add("ProcessID")
        dtDisplay.Columns.Add("SubProcessID")
        dtDisplay.Columns.Add("AssessAns")
        dtDisplay.Columns.Add("Result")
        dtDisplay.Columns.Add("SAC_Notes")
        dtDisplay.Columns.Add("SAC_IntNotes")

        Try
            sSql = "select a.CASP_ID as SubProcessID,b.CAS_ID as SectionID,c.CASU_ID  as SubSectionID,d.cap_id as processID,a.CASP_POINTS, "
            sSql = sSql & "a.CASP_SUBPROCESSNAME As SubProcess,b.CAS_SECTIONNAME As Sectionname ,c.CASU_SUBSECTIONNAME As SubSectionName, "
            sSql = sSql & " d.CAP_PROCESSNAME As Processname from CRPA_SubProcess As a"
            sSql = sSql & " Left join CRPA_Section as b on CAS_ID=CASP_SECTIONID"
            sSql = sSql & " Left join CRPA_SubSection as c on CASU_ID=CASP_SUBSECTIONID "
            sSql = sSql & " Left join CRPA_Process as d on CAP_ID=CASP_ProcessID "
            sSql = sSql & " where CASP_SECTIONID = '" & iSectionID & "'"
            sSql = sSql & " and CASP_CompId ='" & iACID & "' and CASP_DELFLG ='A'"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    dRow("PKID") = 0
                    If IsDBNull(dr("SubSectionName")) = False Then
                        dRow("SubSection") = dr("SubSectionName")
                    End If
                    If IsDBNull(dr("Processname")) = False Then
                        dRow("Process") = dr("Processname")
                    End If
                    If IsDBNull(dr("SubProcess")) = False Then
                        dRow("SubProcess") = dr("SubProcess")
                    End If
                    If IsDBNull(dr("SubSectionID")) = False Then
                        dRow("SubSectionID") = dr("SubSectionID")
                    End If
                    If IsDBNull(dr("processID")) = False Then
                        dRow("ProcessID") = dr("processID")
                    End If
                    If IsDBNull(dr("SubProcessID")) = False Then
                        dRow("SubProcessID") = dr("SubProcessID")
                    End If
                    dRow("AssessAns") = 0
                    dRow("Result") = ""
                    dRow("AssignID") = 0
                    dRow("SAC_Notes") = ""
                    dRow("SAC_IntNotes") = ""
                    i = i + 1
                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExisting(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("PKID")
        dtDisplay.Columns.Add("AssignID")
        dtDisplay.Columns.Add("SubSection")
        dtDisplay.Columns.Add("Process")
        dtDisplay.Columns.Add("SubProcess")
        dtDisplay.Columns.Add("SubSectionID")
        dtDisplay.Columns.Add("ProcessID")
        dtDisplay.Columns.Add("SubProcessID")
        dtDisplay.Columns.Add("AssessAns")
        dtDisplay.Columns.Add("Result")
        dtDisplay.Columns.Add("SAC_Notes")
        dtDisplay.Columns.Add("SAC_IntNotes")

        Try
            sSql = "select a.*, e.CASP_SUBPROCESSNAME as SubProcess,b.CAS_SECTIONNAME as Sectionname,e.CASP_ID as SubProcessID,b.CAS_ID as SectionID,"
            sSql = sSql & " c.CASU_ID as SubSectionID,d.cap_id as processID,CASU_SUBSECTIONNAME as SubSectionName, "
            sSql = sSql & " d.CAP_PROCESSNAME as  Processname, f.CVR_Name as Rating from SAssessment_Checklist a"
            sSql = sSql & " left join  CRPA_Section as b on b.CAS_ID=a.SAC_SectionID "
            sSql = sSql & " left join CRPA_SubSection as c on c.CASU_ID=a.SAC_SUBSECTIONID"
            sSql = sSql & " Left join CRPA_Process as d on d.CAP_ID=a.SAC_PROCESSID"
            sSql = sSql & " Left join CRPA_SubProcess as e on e.CASP_ID=a.SAC_SubPROCESSID"
            sSql = sSql & " Left join CRPA_ValueRating as f on f.CVR_Point=a.SAC_Result and f.CVR_AuditID=a.SAC_SectionID"
            sSql = sSql & " where SAC_SAID =" & iAuditID & " and CASP_CompId ='" & iACID & "' order by SAC_PKID asc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("SAC_PKID")) = False Then
                        dRow("PKID") = dr("SAC_PKID")
                    End If
                    If IsDBNull(dr("SAC_SAID")) = False Then
                        dRow("AssignID") = dr("SAC_SAID")
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
                    If IsDBNull(dr("SubSectionID")) = False Then
                        dRow("SubSectionID") = dr("SubSectionID")
                    End If
                    If IsDBNull(dr("processID")) = False Then
                        dRow("ProcessID") = dr("processID")
                    End If
                    If IsDBNull(dr("SubProcessID")) = False Then
                        dRow("SubProcessID") = dr("SubProcessID")
                    End If
                    'dRow("Findings") = 0

                    If IsDBNull(dr("SAC_RESULT")) = False Then
                        dRow("Result") = dr("SAC_RESULT")
                        dRow("AssessAns") = dr("SAC_RESULT")
                    End If

                    If IsDBNull(dr("Rating")) = False Then
                        dRow("Result") = dr("Rating")
                    End If

                    If IsDBNull(dr("SAC_Notes")) = False Then
                        dRow("SAC_Notes") = dr("SAC_Notes")
                    End If
                    If IsDBNull(dr("SAC_IntNotes")) = False Then
                        dRow("SAC_INTNotes") = dr("SAC_IntNotes")
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
    Public Function GetAttachCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iAudID As Integer, ByVal iSubProID As Integer) As Integer
        Dim sSql As String
        Dim iRet As New Integer
        Try
            If iSubProID = 0 Then
                sSql = "Select count(*) from SA_ATTACHMENTS where ATCH_AuditID=" & iAudID & " and ATCH_SubProID=0 And ATCH_CompID=" & iACID & " And ATCH_Status='X'"
            Else
                sSql = "select count(*) from SA_ATTACHMENTS where ATCH_AuditID=" & iAudID & " and ATCH_SubProID=" & iSubProID & " and ATCH_CompID=" & iACID & " and ATCH_Status='X'"
            End If
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveSelfAssessment(ByVal sAC As String, ByVal objclsSA As clsSelfAssessment) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iRCSAParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSAParamCount = 0
            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_PKID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_PKID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_YearID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_YearID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_CompanyID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_CompanyID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_SECTIONID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_SectionID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_MonthID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_MonthID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_Code", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.sSA_Code
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_StartDate", OleDb.OleDbType.Date)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.dSA_StartDate
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_CrBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_UpdatedBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_SubmittedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_SubmittedBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_Status", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.sSA_Status
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.sSA_IPAddress
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SA_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSA_CompID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAssessment_Mas", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAssessmentChecklist(ByVal sAC As String, ByVal objclsSA As clsSelfAssessment) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iRCSAParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSAParamCount = 0
            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_PKID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_PKID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_SAID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_SAID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_YEARID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_YEARID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_MonthID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_MonthID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_CompanyID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_CompanyID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_SectionID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_SectionID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_SUBSECTIONID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_SUBSECTIONID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_PROCESSID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_PROCESSID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_SUBPROCESSID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_SUBPROCESSID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_RESULT", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_RESULT
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_Notes", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.sSAC_Notes
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_IntNotes", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.sSAC_IntNotes
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_CREATEDBY", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_CREATEDBY
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_UPDATEDBY
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_IPAddress", OleDb.OleDbType.VarChar)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.sSAC_IPAddress
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@SAC_CompID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objclsSA.iSAC_CompID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAssessment_Checklist", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubProcessName(ByVal sAC As String, ByVal iACID As Integer, ByVal iAUDCheckID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select CASP_id, CASP_Subprocessname from CRPA_SubProcess where CASP_ID=" & iAUDCheckID & ""
            sSql = sSql & " And CASP_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateAssessStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iPkid As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "update SAssessment_Mas set SA_Status ='Submitted',SA_SubmittedBy=" & iUserID & ", SA_SubmittedOn= GetDate(),SA_EndDate=GetDate() where SA_PKID='" & iPkid & "'"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAttachments(ByVal sAC As String, ByVal iACID As Integer, ByVal sPath As String, ByVal iUserId As Integer, ByVal iAttachID As Integer,
                                 ByVal iAuditID As Integer, ByVal iSubProID As Integer) As Integer
        Dim sExt As String, sInputFilePath As String, sSql As String, sDBPath As String, sDBFilePath As String
        Dim iPosDot As Integer, iPosSlash As Integer, fileSize As Integer, iDocID As Integer
        Dim con As New OleDbConnection
        Dim objFile As FileStream
        Dim com As OleDbCommand
        Dim sPegDes As String = "", sPegExt As String = "", iPegSize As String = ""
        Try
            iPosSlash = InStrRev(sPath, "\")
            iPosDot = InStrRev(sPath, ".")
            If iPosDot <> 0 Then
                sInputFilePath = Mid(sPath, iPosSlash + 1, iPosDot - (iPosSlash + 1))
                sExt = Right(sPath, Len(sPath) - iPosDot)
            Else
                sInputFilePath = Mid(sPath, iPosSlash, Len(sPath) - (iPosSlash + 1))
                sExt = "unk"
            End If
            sInputFilePath = Replace(sInputFilePath, "&", " and")
            sInputFilePath = objclsGRACeGeneral.SafeSQL(sInputFilePath)
            If sInputFilePath.Length > 99 Then
                sInputFilePath = sInputFilePath.Substring(0, 95)
            End If

            'NextSetOfID: If iAttachID = 0 Then
            iAttachID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_ID),0)+1 from SA_ATTACHMENTS Where ATCH_CompID=" & iACID & "")
            'End If
            iDocID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_DOCID),0)+1 from SA_ATTACHMENTS where ATCH_CompID=" & iACID & "")
            'If iDocID = 0 Then
            '    sSql = "" : sSql = "Select ATCH_DOCID from SA_ATTACHMENTS where ATCH_CompID=" & iACID & " And atch_id = " & iAttachID & "" ' And ATCH_DOCID = " & Docid & ""
            '    Dim dr As OleDbDataReader
            '    dr = objDBL.SQLDataReader(sAC, sSql)
            '    If dr.HasRows = True Then
            '        iAttachID = 0
            '        dr.Close()
            '        'GoTo NextSetOfID
            '    End If
            '    dr.Close()
            'End If

            objFile = New FileStream(sPath, FileMode.Open)
            fileSize = CType(objFile.Length, Integer)
            Dim BUFFER(fileSize) As Byte
            objFile.Read(BUFFER, 0, fileSize)
            objFile.Close()
            If objDBL.SQLExecuteScalar(sAC, "Select Sad_Config_Value From Sad_Config_Settings Where Sad_Config_Key='FilesInDB' And Sad_CompID=" & iACID & "") = "True" Then
                sSql = "" : sSql = "Insert into SA_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_AuditID,ATCH_SubProID,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_OLE,ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "'," & iAuditID & "," & iSubProID & "," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "?," & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                con = objDBL.SQLOpenDBConnection(sAC)
                com = New OleDbCommand(sSql, con)
                Dim ParamBasename As New OleDbParameter("@atch_ole", OleDbType.Binary)
                ParamBasename.Value = BUFFER
                com.Parameters.Add(ParamBasename)
                Dim myTrans As OleDb.OleDbTransaction  'Start a local transaction
                myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted) 'Assign transaction object for a pending local transaction
                com.Connection = con
                com.Transaction = myTrans
                com.ExecuteNonQuery()
                myTrans.Commit()
            Else
                sSql = "" : sSql = "Insert into SA_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_AuditID,ATCH_SubProID,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "'," & iAuditID & "," & iSubProID & "," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "" & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)

                sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                If sDBPath.EndsWith("\") = False Then
                    sDBPath = sDBPath & "\Attachments\" & iDocID \ 301
                Else
                    sDBPath = sDBPath & "Attachments\" & iDocID \ 301
                End If
                If System.IO.Directory.Exists(sDBPath) = False Then
                    System.IO.Directory.CreateDirectory(sDBPath)
                End If

                sDBFilePath = sDBPath & "\" & iDocID & "." & sExt
                If System.IO.File.Exists(sDBFilePath) = True Then
                    System.IO.File.Delete(sDBFilePath)
                End If
                'File.Copy(sPath, sDBFilePath)
                Encrypt(sPath, sDBFilePath)
            End If
            Return iAttachID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Sub Encrypt(ByVal sInputFilePath As String, ByVal sOutputFilePath As String)
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using fs As New FileStream(sOutputFilePath, FileMode.Create)
                Using cs As New CryptoStream(fs, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    Using fsInput As New FileStream(sInputFilePath, FileMode.Open)
                        Dim data As Integer
                        While (Assign(data, fsInput.ReadByte())) <> -1
                            cs.WriteByte(CByte(data))
                        End While
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Shared Function Assign(Of T)(ByRef source As T, ByVal value As T) As T
        source = value
        Return value
    End Function
    Public Shared Sub Decrypt(ByVal sInputFilePath As String, ByVal sOutputFilePath As String)
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using fs As New FileStream(sInputFilePath, FileMode.Open)
                Using cs As New CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read)
                    Using fsOutput As New FileStream(sOutputFilePath, FileMode.Create)
                        Dim data As Integer
                        While (Assign(data, cs.ReadByte())) <> -1
                            fsOutput.WriteByte(CByte(data))
                        End While
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Public Function LoadAttachments(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer,
                                 ByVal iAuditID As Integer, ByVal iSubProID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From sa_attachments where ATCH_CompID=" & iACID & " And "
            sSql = sSql & "ATCH_AuditID= " & iAuditID & " And ATCH_Status <> 'D' "
            sSql = sSql & " And ATCH_SubProID= " & iSubProID & " "
            sSql = sSql & " Order by ATCH_CREATEDON"

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Sub UpdateAuditID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditId As Integer)
        Dim sSql As String
        Try
            sSql = "Update sa_attachments set ATCH_AuditID=" & iAuditId & " where  ATCH_CompID=" & iACID & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDocumentPath(ByVal sAC As String, ByVal iACID As Integer, ByVal sPaths As String, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer)
        Dim iAtchOle As Integer
        Dim con As New OleDb.OleDbConnection
        Dim Pdr As OleDb.OleDbDataReader, PdrCheck As OleDb.OleDbDataReader
        Dim sSql As String, sDBPath As String, sDBFilePath As String, sInputFilePath As String = ""
        Try
            If iAttachDocID = 0 Then
                Return ""
            End If
            If sPaths.EndsWith("\") = False Then
                sPaths = sPaths & "\"
            End If

            If System.IO.Directory.Exists(sPaths) = False Then
                System.IO.Directory.CreateDirectory(sPaths)
            End If

            If objDBL.SQLExecuteScalar(sAC, "Select Sad_Config_Value From Sad_Config_Settings Where Sad_Config_Key='FilesInDB' And Sad_CompID=" & iACID & "") = "True" Then
                sSql = "Select atch_ole,ATCH_DocId,ATCH_FNAME,atch_ext,ATCH_FLAG from sa_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                Pdr = objDBL.SQLDataReader(sAC, sSql)
                If Pdr.HasRows Then
                    While Pdr.Read()
                        sInputFilePath = sPaths & Pdr("ATCH_FNAME") & "." & Pdr("atch_ext")
                        If System.IO.File.Exists(sInputFilePath) = True Then
                            System.IO.File.Delete(sInputFilePath)
                        End If
                        Dim BUFFER(Pdr.GetBytes(iAtchOle, 0, BUFFER, 0, Integer.MaxValue)) As Byte
                        Pdr.GetBytes(iAtchOle, 0, BUFFER, 0, BUFFER.Length)
                        Dim BlobData As New IO.FileStream(sInputFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                        BlobData.Write(BUFFER, 0, BUFFER.Length)
                        BlobData.Close()
                    End While
                Else
                    sInputFilePath = String.Empty
                End If
            Else
                sSql = "Select ATCH_DocId,ATCH_FNAME,atch_ext from sa_ATTACHMENTS where atch_ole IS Not NULL And ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                PdrCheck = objDBL.SQLDataReader(sAC, sSql)
                If PdrCheck.HasRows Then
                    sSql = "Select atch_ole,ATCH_DocId,ATCH_FNAME,atch_ext,ATCH_FLAG from sa_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                    Pdr = objDBL.SQLDataReader(sAC, sSql)
                    If Pdr.HasRows Then
                        While Pdr.Read()
                            sInputFilePath = sPaths & Pdr("ATCH_FNAME") & "." & Pdr("atch_ext")
                            If System.IO.File.Exists(sInputFilePath) = True Then
                                System.IO.File.Delete(sInputFilePath)
                            End If
                            Dim BUFFER(Pdr.GetBytes(iAtchOle, 0, BUFFER, 0, Integer.MaxValue)) As Byte
                            Pdr.GetBytes(iAtchOle, 0, BUFFER, 0, BUFFER.Length)
                            Dim BlobData As New IO.FileStream(sInputFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            BlobData.Write(BUFFER, 0, BUFFER.Length)
                            BlobData.Close()
                        End While
                    Else
                        sInputFilePath = String.Empty
                    End If
                Else
                    sSql = "Select ATCH_DocId,ATCH_FNAME,atch_ext from sa_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                    Pdr = objDBL.SQLDataReader(sAC, sSql)
                    If Pdr.HasRows Then
                        While Pdr.Read()
                            sInputFilePath = sPaths & Pdr("ATCH_FNAME") & "." & Pdr("atch_ext")
                            If System.IO.File.Exists(sInputFilePath) = True Then
                                System.IO.File.Delete(sInputFilePath)
                            End If

                            sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                            If sDBPath.EndsWith("\") = False Then
                                sDBPath = sDBPath & "\Attachments\" & Pdr("ATCH_DocId") \ 301
                            Else
                                sDBPath = sDBPath & "Attachments\" & Pdr("ATCH_DocId") \ 301
                            End If
                            If System.IO.Directory.Exists(sDBPath) = True Then
                                sDBFilePath = sDBPath & "\" & Pdr("ATCH_DocId") & "." & Pdr("atch_ext")
                                If System.IO.File.Exists(sDBFilePath) = True Then
                                    'File.Copy(sDBFilePath, sFileName)
                                    Decrypt(sDBFilePath, sInputFilePath)
                                End If
                            End If
                        End While
                    Else
                        sInputFilePath = String.Empty
                    End If
                End If
                PdrCheck.Close()
            End If
            Pdr.Close()
            Return sInputFilePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub RemoveSelectedDoc(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer)
        Dim sSql As String
        Try
            If iAttachID = 0 Then
                sSql = "Update sa_attachments set ATCH_Status='D' where ATCH_CompID=" & iACID & " And atch_docid = " & iAttachDocID & ""
            Else
                sSql = "Update sa_attachments set ATCH_Status='D' where ATCH_CompID=" & iACID & " And atch_docid = " & iAttachDocID & " and atch_id=" & iAttachID & ""
            End If

            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateDescSelectedDoc(ByVal sAC As String, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer, ByVal sDesc As String)
        Dim sSql As String
        Try
            If iAttachID = 0 Then
                sSql = "Update sa_attachments set ATCH_Desc='" & sDesc & "' where atch_docid=" & iAttachDocID & " and ATCH_Status='X'"
            Else
                sSql = "Update sa_attachments set ATCH_Desc='" & sDesc & "' where atch_docid=" & iAttachDocID & " and atch_id=" & iAttachID & " and ATCH_Status='X'"
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Chart
    Public Function LoadAssessRatingNames(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal Isectionid As Integer, ByVal Imonth As Integer, ByVal Iyearid As Integer) As DataTable
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

            sSql = "" : sSql = "select SA_PKID from SAssessment_Mas "
            sSql = sSql & " where SA_YearID=19 and SA_MonthID=" & Imonth & " and SA_CompanyID=" & iCustId & " and SA_SectionID=" & Isectionid & " "
            IAuditid = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            'subsection
            'sSql = "" : sSql = "select CASU_SUBSECTIONNAME,MIN(b.CRAD_FINDINGS)*10 as Result, b.CRAD_SUBSECTIONID from crpa_subsection"
            'sSql = sSql & " left join CBA_ChecklistAuditAssest b on b.CRAD_SUBSECTIONID=CASU_ID and b.CRAD_CAuditID=" & IAuditid & " and b.CRAD_Locationid=" & iCustId & ""
            'sSql = sSql & " where CASU_SECTIONID=" & Isectionid & " and CASU_CompId= " & iACID & " group by CASU_SUBSECTIONNAME,CRAD_SUBSECTIONID order by CRAD_SUBSECTIONID"
            'dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            sSql = "" : sSql = "select CASP_SUBPROCESSNAME,b.SAC_SUBPROCESSID,b.SAC_Result from CRPA_SubProcess"
            sSql = sSql & " left join SAssessment_Checklist b on b.SAC_SUBProcessID=CASP_ID and b.SAC_SAID=" & IAuditid & " and b.SAC_CompanyID=" & iCustId & ""
            sSql = sSql & " where SAC_SECTIONID=" & Isectionid & " and CASP_CompId= " & iACID & " group by CASP_SUBPROCESSNAME,SAC_SUBProcessID,SAC_Result order by SAC_SUBProcessID"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

            For i = 0 To dt.Rows.Count - 1
                dRow = dtDisplay.NewRow
                dRow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("CASP_SUBPROCESSNAME")) = False Then
                    dRow("SUBPROCESSNAME") = dt.Rows(i)("CASP_SUBPROCESSNAME")
                Else
                    dRow("SUBPROCESSNAME") = 0
                End If
                If IsDBNull(dt.Rows(i)("SAC_Result")) = False Then
                    dRow("Findings") = dt.Rows(i)("SAC_Result")
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

            sSql = "select distinct(sa_pkid) from SAssessment_Mas a "
            sSql = sSql & " left Join SAssessment_Checklist b On b.SAC_SAID=a.sa_pkid "
            sSql = sSql & " where SAC_YEARID=" & Iyearid & " And SAC_SECTIONID=" & Isectionid & " And SAC_COMPANYID=" & iCustId & " And a.SA_MonthID=" & Imonth & ""
            iCAID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            'sSql = "" : sSql = "Select b.cvr_id, b.cvr_name as Ratingname, count(a.crad_findings) As TotalCount from CBA_ChecklistAuditAssest a"
            'sSql = sSql & " join crpa_valuerating b On a.crad_sectionid = b.cvr_auditid  And b.cvr_id=a.crad_findings"
            'sSql = sSql & " join CBAAudit_schedule c On c.aud_sectionid = a.CRAD_SECTIONID"
            'sSql = sSql & " where crad_sectionid= " & Isectionid & " And crad_yearid =" & Iyearid & " And  c.AUD_kitchenID=" & iCustId & " And CRAD_CAuditID= " & iCAID & " And c.AUD_MonthID= " & Imonth & " group by b.cvr_id, b.cvr_name, a.crad_findings"

            sSql = "" : sSql = "Select b.cvr_point, b.cvr_name as Ratingname, count(a.SAC_RESULT) As TotalCount from SAssessment_Checklist a "
            sSql = sSql & " left join crpa_valuerating b On b.cvr_point=a.SAC_RESULT  and b.cvr_auditid= " & Isectionid & ""
            sSql = sSql & " left join SAssessment_Mas c On c.sa_sectionid = a.SAC_SECTIONID"
            sSql = sSql & " where sac_sectionid= " & Isectionid & " And sac_yearid =" & Iyearid & " And  sac_companyID=" & iCustId & " And sac_SAID= " & iCAID & " And c.sa_MonthID= " & Imonth & " group by b.cvr_id, b.cvr_name, b.cvr_point"
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
