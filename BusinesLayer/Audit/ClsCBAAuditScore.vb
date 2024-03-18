Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Structure strCBA_AuditAssest
    Dim iCA_PKID As Integer
    Dim sCA_AsgNo As String
    Dim iCA_FinancialYear As Integer
    Dim iCA_CustID As Integer
    Dim iCA_SECTIONID As Integer
    Dim iCA_SUBSECTIONID As Integer
    Dim iCA_PROCESSID As Integer
    Dim iCA_SUBPROCESSID As Integer
    Dim dCA_Date As DateTime
    Dim sCA_AUDITOR As String
    Dim sCA_UnitPresident As String
    Dim dCA_NetScore As Double
    Dim iCACrBy As Integer
    Dim iCA_UpdatedBy As Integer
    Dim iCA_ASubmittedBy As Integer
    Dim iCA_BSubmittedBy As Integer
    Dim sCA_Status As String
    Dim sCA_IPAddress As String
    Dim iCA_CompID As Integer
    Dim sOpsHEAD As String
    Dim sAddress As String
    Public Property sOps_HEAD() As String
        Get
            Return (sOpsHEAD)
        End Get
        Set(ByVal Value As String)
            sOpsHEAD = Value
        End Set
    End Property
    Public Property s_Address() As String
        Get
            Return (sAddress)
        End Get
        Set(ByVal Value As String)
            sAddress = Value
        End Set
    End Property
    Public Property iCAPKID() As Integer
        Get
            Return (iCA_PKID)
        End Get
        Set(ByVal Value As Integer)
            iCA_PKID = Value
        End Set
    End Property
    Public Property sCAAsgNo() As String
        Get
            Return (sCA_AsgNo)
        End Get
        Set(ByVal Value As String)
            sCA_AsgNo = Value
        End Set
    End Property
    Public Property iCAFinancialYear() As Integer
        Get
            Return (iCA_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iCA_FinancialYear = Value
        End Set
    End Property
    Public Property iCACustID() As Integer
        Get
            Return (iCA_CustID)
        End Get
        Set(ByVal Value As Integer)
            iCA_CustID = Value
        End Set
    End Property
    Public Property iCASECTIONID() As Integer
        Get
            Return (iCA_SECTIONID)
        End Get
        Set(ByVal Value As Integer)
            iCA_SECTIONID = Value
        End Set
    End Property
    Public Property iCASUBSECTIONID() As Integer
        Get
            Return (iCA_SUBSECTIONID)
        End Get
        Set(ByVal Value As Integer)
            iCA_SUBSECTIONID = Value
        End Set
    End Property
    Public Property iCAPROCESSID() As Integer
        Get
            Return (iCA_PROCESSID)
        End Get
        Set(ByVal Value As Integer)
            iCA_PROCESSID = Value
        End Set
    End Property
    Public Property iCASUBPROCESSID() As Integer
        Get
            Return (iCA_SUBPROCESSID)
        End Get
        Set(ByVal Value As Integer)
            iCA_SUBPROCESSID = Value
        End Set
    End Property
    Public Property dCADate() As DateTime
        Get
            Return (dCA_Date)
        End Get
        Set(ByVal Value As DateTime)
            dCA_Date = Value
        End Set
    End Property
    Public Property sCAAUDITOR() As String
        Get
            Return (sCA_AUDITOR)
        End Get
        Set(ByVal Value As String)
            sCA_AUDITOR = Value
        End Set
    End Property
    Public Property sCAUNITPRESIDENT() As String
        Get
            Return (sCA_UnitPresident)
        End Get
        Set(ByVal Value As String)
            sCA_UnitPresident = Value
        End Set
    End Property
    Public Property dCANetScore() As Double
        Get
            Return (dCA_NetScore)
        End Get
        Set(ByVal Value As Double)
            dCA_NetScore = Value
        End Set
    End Property
    Public Property iCA_CrBy() As Integer
        Get
            Return (iCACrBy)
        End Get
        Set(ByVal Value As Integer)
            iCACrBy = Value
        End Set
    End Property
    Public Property iCAUpdatedBy() As Integer
        Get
            Return (iCA_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCA_UpdatedBy = Value
        End Set
    End Property
    Public Property iCAASubmittedBy() As Integer
        Get
            Return (iCA_ASubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            iCA_ASubmittedBy = Value
        End Set
    End Property
    Public Property iCABSubmittedBy() As Integer
        Get
            Return (iCA_BSubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            iCA_BSubmittedBy = Value
        End Set
    End Property
    Public Property sCAStatus() As String
        Get
            Return (sCA_Status)
        End Get
        Set(ByVal Value As String)
            sCA_Status = Value
        End Set
    End Property
    Public Property sCAIPAddress() As String
        Get
            Return (sCA_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCA_IPAddress = Value
        End Set
    End Property
    Public Property iCACompID() As Integer
        Get
            Return (iCA_CompID)
        End Get
        Set(ByVal Value As Integer)
            iCA_CompID = Value
        End Set
    End Property
End Structure
Public Class ClsCBAAuditScore
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function LoadSectiondetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select CAS_ID, CAS_SectionName from crpa_section where CAS_Delflg='A' and cas_id<>5 and cas_compid=" & iACID & "  order by CAS_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetAUDRatingPoint(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer) As Integer
        Dim sSql As String
        Dim iRet As New Integer
        Try
            sSql = "Select CVR_Point from CRPA_ValueRating Where cvr_ID=" & iPKID & " and cvr_flag = 'A' and cvr_compid=" & iACID & ""
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAUDSectionScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer) As Integer
        Dim sSql As String
        Dim iRet As New Integer
        Try
            sSql = "select CAS_Points from CRPA_section Where CAS_ID=" & iPKID & " and CAS_Delflg = 'A' and CAS_compid=" & iACID & ""
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLocation(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select org_node, org_name from sad_org_structure where org_levelCode=4 and org_delflag = 'A'"
            sSql = sSql & " Order by org_name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadScheduleDts(ByVal sAC As String, ByVal iACID As Integer, ByVal iSecID As Integer, ByVal iKitchenID As Integer, ByVal iMonthID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select * from CBAAudit_Schedule where aud_sectionid='" & iSecID & "' and AUD_KitchenID='" & iKitchenID & "' and AUD_MonthID='" & iMonthID & "' and AUD_YearID='" & iYearID & "' and AUD_CompID='" & iACID & "' "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForData(ByVal sAC As String, ByVal iACID As Integer, ByVal iCusID As Integer, ByVal iSectionID As Integer, ByVal iYearID As Integer, ByVal iMonthId As Integer, ByVal iAUDSchId As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "select ca_pkid,CA_Status,CA_NETSCORE,CA_NAME_OF_OPS_HEAD,CA_ADDRESS,CA_NAME_OF_UNIT_PRESIDENT,CA_AUDITORNAME,CA_Date,AUD_MonthId from CBA_AuditAssest  "
            sSql = sSql & " Left join CBAAudit_Schedule b on b.aud_id=ca_asgno "
            sSql = sSql & " where b.AUD_MonthID=" & iMonthId & " and  CA_LOCATIONID='" & iCusID & "' and CA_SECTIONID='" & iSectionID & "' and CA_FinancialYear = '" & iYearID & "' and CA_CompID='" & iACID & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function CheckForData(ByVal sAC As String, ByVal iACID As Integer, ByVal iCusID As Integer, ByVal iSectionID As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim sSql As String : Dim dt As New DataTable
    '    Try
    '        sSql = "select ca_pkid,CA_Status,CA_NETSCORE,CA_NAME_OF_OPS_HEAD,CA_ADDRESS,CA_NAME_OF_UNIT_PRESIDENT, "
    '        sSql = sSql & " CA_AUDITORNAME,CA_Status from CBA_AuditAssest where CA_LOCATIONID='" & iCusID & "' and "
    '        sSql = sSql & "CA_SECTIONID='" & iSectionID & "' and CA_FinancialYear = '" & iYearID & "' and CA_CompID='" & iACID & "'"

    '        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
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
    Public Function GetAttachCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iAudSchID As Integer, ByVal iAudID As Integer, ByVal iSubProID As Integer) As Integer
        Dim sSql As String
        Dim iRet As New Integer
        Try
            If iSubProID = 0 Then
                sSql = "Select count(*) from Edt_Attachments where  ATCH_AUDScheduleID=" & iAudSchID & " And ATCH_SubProcessID=0 And ATCH_CompID=" & iACID & " And ATCH_Status='X'"
            Else
                sSql = "select count(*) from Edt_Attachments where  ATCH_AUDScheduleID=" & iAudSchID & " and ATCH_SubProcessID=" & iSubProID & " and ATCH_CompID=" & iACID & " and ATCH_Status='X'"
            End If
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKitchenID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAudID As Integer, ByVal iYearID As Integer, ByVal iMonthId As Integer) As Integer
        Dim sSql As String
        Dim iRet As New Integer
        Try
            sSql = "Select AUD_KitchenID from CBAAudit_Schedule where AUD_ID=" & iAudID & " and AUD_MonthID=" & iMonthId & " and  AUD_YearID=" & iYearID & " and AUD_compID=" & iACID & ""
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditTableDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAudID As Integer, ByVal iYearID As Integer, ByVal iMonthId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select AUD_ID,AUD_Code,AUD_Title from CBAAudit_Schedule where AUD_ID=" & iAudID & " and AUD_MonthID=" & iMonthId & " and  AUD_YearID=" & iYearID & " and AUD_compID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExisting(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iSecId As Integer) As DataTable
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
        dtDisplay.Columns.Add("Findings")
        dtDisplay.Columns.Add("Standard")
        dtDisplay.Columns.Add("Result")
        dtDisplay.Columns.Add("AuditComment")
        dtDisplay.Columns.Add("FindingsValue")


        Try
            sSql = "select a.CRAD_CAuditID, e.CASP_SUBPROCESSNAME as SubProcess,b.CAS_SECTIONNAME as Sectionname,e.CASP_ID as SubProcessID,b.CAS_ID as SectionID,c.CASU_ID as"
            sSql = sSql & " SubSectionID,d.cap_id as processID, a.CRAD_FINDINGS,f.cvr_id,f.cvr_name,a.CRAD_SCORE_STANDARD,a.CRAD_SCORE_RESULT,a.CRAD_COMMENTS,a.CRAD_PKID,CASU_SUBSECTIONNAME as SubSectionName,d.CAP_PROCESSNAME as "
            sSql = sSql & " Processname from CBA_ChecklistAuditAssest a,CRPA_Section b,CRPA_SubSection c, CRPA_Process d,CRPA_SubProcess e,CRPA_ValueRating f"
            sSql = sSql & " where CRAD_CAuditID ='" & iAuditID & "' and a.CRAD_SECTIONID =b.CAS_ID and a.CRAD_SUBSECTIONID =c.CASU_ID"
            sSql = sSql & " and a.CRAD_PROCESSID = d.CAP_ID AND A.CRAD_SUBPROCESSID = E.CASP_ID and a.CRAD_FINDINGS=f.cvr_id and CASP_CompId ='" & iACID & "' order by CRAD_PKID asc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("CRAD_PKID")) = False Then
                        dRow("PKID") = dr("CRAD_PKID")
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
                    If IsDBNull(dr("CRAD_SCORE_STANDARD")) = False Then
                        dRow("Standard") = dr("CRAD_SCORE_STANDARD")
                    End If
                    If IsDBNull(dr("CRAD_SCORE_RESULT")) = False Then
                        If iSecId = 8 Then
                            If dr("CRAD_SCORE_RESULT") = 0 Then
                                dRow("Result") = "0 - Not Applicable"
                            ElseIf dr("CRAD_SCORE_RESULT") = 1 Then
                                dRow("Result") = "1 - Developing"
                            ElseIf dr("CRAD_SCORE_RESULT") = 2 Then
                                dRow("Result") = "2 - Meeting"
                            ElseIf dr("CRAD_SCORE_RESULT") = 3 Then
                                dRow("Result") = "3 - Exceeding"
                            End If
                        Else
                            dRow("Result") = dr("CRAD_SCORE_RESULT")
                        End If
                    End If
                    'If IsDBNull(dr("CRAD_FINDINGS")) = False Then
                    '    dRow("Findings") = dr("CRAD_FINDINGS")
                    'End If
                    If IsDBNull(dr("CRAD_FINDINGS")) = False Then
                        dRow("Findings") = dr("CRAD_FINDINGS")
                        'If dRow("Findings") = 0 Then
                        '    dRow("Findings") = "Select"
                        'ElseIf dRow("Findings") = 1 Then
                        '    dRow("Findings") = "Fully Complied"
                        'ElseIf dRow("Findings") = 2 Then
                        '    dRow("Findings") = "Complied"
                        'ElseIf dRow("Findings") = 3 Then
                        '    dRow("Findings") = "Not Applicable"
                        'ElseIf dRow("Findings") = 4 Then
                        '    dRow("Findings") = "Not Complied"

                    End If
                    If IsDBNull(dr("CRAD_COMMENTS")) = False Then
                        dRow("AuditComment") = dr("CRAD_COMMENTS")
                    End If
                    If IsDBNull(dr("CRAD_CAuditID")) = False Then
                        dRow("AssignID") = dr("CRAD_CAuditID")
                    End If
                    If IsDBNull(dr("cvr_name")) = False Then
                        dRow("FindingsValue") = dr("cvr_name")
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
        dtDisplay.Columns.Add("Findings")
        dtDisplay.Columns.Add("Standard")
        dtDisplay.Columns.Add("Result")
        dtDisplay.Columns.Add("AuditComment")
        dtDisplay.Columns.Add("FindingsValue")

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
                    dRow("Findings") = 0
                    If IsDBNull(dr("CASP_POINTS")) = False Then
                        dRow("Standard") = dr("CASP_POINTS")
                    End If
                    dRow("Result") = ""
                    dRow("AssignID") = 0
                    dRow("AuditComment") = ""
                    dRow("FindingsValue") = ""
                    i = i + 1
                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindValueRatings(ByVal sAC As String, ByVal iACID As Integer, ByVal iAudID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select cvr_id,cvr_name from CRPA_ValueRating Where cvr_AuditID=" & iAudID & " and  cvr_flag = 'A' and cvr_compid=" & iACID & " order by CVR_Point Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function SaceAuditDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal SArray As Array) As Array
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "insert into CRPA_Audit_Details(CRAD_PKID,CRAD_CustID,CRAD_SECTIONID,CRAD_SUBSECTIONID,CRAD_PROCESSID,CRAD_SUBPROCESSID,CRAD_AUDITTITLE,CRAD_DATE,"
    '        sSql = sSql & " CRAD_AUDITOR_TEAM,CRAD_SCORE,CRAD_AUDITORCOMMENT,CRAD_YEARID,CRAD_CREATEDBY,CRAD_CREATEDON,CRAD_UPDATEDBY,CRAD_UPDATEDON,CRAD_IPAddress,CRAD_CompID)"
    '        sSql = sSql & " values((select IsNull(Max(CRAD_PKID)+1,1) from CRPA_Audit_Details),'" & SArray(0) & "','" & SArray(1) & "','" & SArray(2) & "','" & SArray(3) & "','" & SArray(4) & "','" & SArray(5) & "','" & SArray(6) & "','" & SArray(7) & "',"
    '        sSql = sSql & " '" & SArray(8) & "','" & SArray(9) & "','" & SArray(10) & "','" & SArray(11) & "',getdate(),'" & SArray(13) & "','" & SArray(14) & "','" & SArray(15) & "','" & SArray(16) & "')"
    '        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SaveAuditAssest(ByVal sAC As String, ByVal objAuditAssest As strCBA_AuditAssest) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iRCSAParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSAParamCount = 0
            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_PKID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_PKID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_AsgNo", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.sCA_AsgNo
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_FinancialYear
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_LOCATIONID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_CustID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_SECTIONID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_SECTIONID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_Date", OleDb.OleDbType.Date)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.dCA_Date
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_NAME_OF_OPS_HEAD", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.sOpsHEAD
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_ADDRESS", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.sAddress
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_NAME_OF_UNIT_PRESIDENT", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.sCA_UnitPresident
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_AUDITORNAME", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.sCA_AUDITOR
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_NetScore", OleDb.OleDbType.Double)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.dCA_NetScore
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCACrBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_UpdatedBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_ASubmittedBy", OleDb.OleDbType.Integer, 4) 'Save
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_ASubmittedBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_BSubmittedBy", OleDb.OleDbType.Integer, 4)  'Submit
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_BSubmittedBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_Status", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.sCA_Status
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.sCA_IPAddress
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objAuditAssest.iCA_CompID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCBA_AUDITASSEST", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetASSIGNID(ByVal sAC As String, ByVal iYearID As Integer, ByVal sYearName As String) As String
    '    Dim iMaxID As Integer
    '    Dim sMaxID As String = "", sJobCode As String = "", sModuleCode As String = ""
    '    Try

    '        iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from CBA_AuditAssest where ca_financialyear=" & iYearID & "")
    '        sModuleCode = "GMP"

    '        If iMaxID = 0 Then
    '            sMaxID = "001"
    '        ElseIf iMaxID > 0 And iMaxID < 10 Then
    '            sMaxID = "00" & iMaxID
    '        ElseIf iMaxID >= 10 And iMaxID < 100 Then
    '            sMaxID = "0" & iMaxID
    '        Else
    '            sMaxID = iMaxID
    '        End If
    '        sJobCode = "TRACe/" & sModuleCode & "/" & sYearName & "/" & sMaxID
    '        Return sJobCode
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function AuditDashborad(ByVal sNameSpace As String, ByVal iACID As Integer) As DataTable
    '    Dim dtDisplay As New DataTable
    '    Dim i As Integer = 1
    '    Dim dRow As DataRow
    '    Dim sSql As String
    '    Dim dr As OleDb.OleDbDataReader

    '    dtDisplay.Columns.Add("SrNo")
    '    dtDisplay.Columns.Add("AuditTitle")
    '    dtDisplay.Columns.Add("Process")
    '    dtDisplay.Columns.Add("SubProcess")
    '    dtDisplay.Columns.Add("Score")
    '    dtDisplay.Columns.Add("AuditComment")
    '    Try
    '        sSql = "select a.CASP_SUBPROCESSNAME as SubProcess,b.CAS_SECTIONNAME as Sectionname ,c.CASU_SUBSECTIONNAME as SubSectionName,d.CAP_PROCESSNAME as Processname "
    '        sSql = sSql & " from CRPA_SubProcess a,CRPA_Section b,CRPA_SubSection c,CRPA_Process d where CASP_SECTIONID = '" & iSectionID & "'"
    '        sSql = sSql & " and a.CASP_SECTIONID =b.cas_id and a.CASP_SUBSECTIONID =c.CASU_ID and a.CASP_PROCESSID = d.CAP_ID and CASP_CompId ='" & iACID & "' and CASP_DELFLG ='A'"
    '        dr = objDBL.SQLDataReader(sNameSpace, sSql)
    '        If dr.HasRows Then
    '            While dr.Read
    '                dRow = dtDisplay.NewRow
    '                dRow("SrNo") = i
    '                dRow("SubSection") = dr("SubSectionName")
    '                If IsDBNull(dr("SubSectionName")) = False Then
    '                    dRow("SubSection") = dr("SubSectionName")
    '                End If
    '                If IsDBNull(dr("Processname")) = False Then
    '                    dRow("Process") = dr("Processname")
    '                End If
    '                If IsDBNull(dr("SubProcess")) = False Then
    '                    dRow("SubProcess") = dr("SubProcess")
    '                End If
    '                'If IsDBNull(dr("CBN_FolCount")) = False Then
    '                '    dRow("Score") = dr("CBN_FolCount")
    '                'End If
    '                dRow("Score") = 0
    '                dRow("AuditComment") = ""
    '                i = i + 1
    '                dtDisplay.Rows.Add(dRow)
    '            End While
    '        End If
    '        Return dtDisplay
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadGridDetails2(ByVal sNameSpace As String, ByVal iSectionID As Integer, ByVal iACID As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("SubSection")
        dtDisplay.Columns.Add("Process")
        dtDisplay.Columns.Add("SubProcess")
        dtDisplay.Columns.Add("Score")
        dtDisplay.Columns.Add("AuditComment")


        Try
            sSql = "select a.CASP_SUBPROCESSNAME as SubProcess,b.CAS_SECTIONNAME as Sectionname ,c.CASU_SUBSECTIONNAME as SubSectionName,d.CAP_PROCESSNAME as Processname "
            sSql = sSql & " from CRPA_SubProcess a,CRPA_Section b,CRPA_SubSection c,CRPA_Process d where CASP_SECTIONID = '" & iSectionID & "'"
            sSql = sSql & " and a.CASP_SECTIONID =b.cas_id and a.CASP_SUBSECTIONID =c.CASU_ID and a.CASP_PROCESSID = d.CAP_ID and CASP_CompId ='" & iACID & "' and CASP_DELFLG ='A'"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    dRow("SubSection") = dr("SubSectionName")
                    If IsDBNull(dr("SubSectionName")) = False Then
                        dRow("SubSection") = dr("SubSectionName")
                    End If
                    If IsDBNull(dr("Processname")) = False Then
                        dRow("Process") = dr("Processname")
                    End If
                    If IsDBNull(dr("SubProcess")) = False Then
                        dRow("SubProcess") = dr("SubProcess")
                    End If
                    'If IsDBNull(dr("CBN_FolCount")) = False Then
                    '    dRow("Score") = dr("CBN_FolCount")
                    'End If
                    dRow("Score") = "Not Applicable"
                    dRow("AuditComment") = ""
                    i = i + 1
                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCheckListAudit(ByVal sAC As String, ByVal sdate As DateTime, ByVal IPkID As Integer, ByVal iLocationID As Integer, ByVal iAssignID As Integer, ByVal iSectionId As Integer,
                                      ByVal iSubSectionId As Integer, ByVal iProcessId As Integer, ByVal iSubProcessId As Integer, ByVal iFindingsId As Integer,
                                       ByVal iScoreStandardId As Integer, ByVal iScoreResultd As Integer, ByVal scomments As String,
                                       ByVal yearid As String, ByVal createdby As String, ByVal updatedby As String, ByVal ip As String, ByVal compid As String) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iRCSAParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSAParamCount = 0
            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_PKID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = IPkID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_CAuditID", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iRCSAParamCount).Value = iAssignID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_LOCATIONID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iLocationID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_SECTIONID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iSectionId
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_SUBSECTIONID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iSubSectionId
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_PROCESSID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iProcessId
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_SUBPROCESSID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iSubProcessId
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_FINDINGS", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iFindingsId
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_SCORE_STANDARD", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iScoreStandardId
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_SCORE_RESULT", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = iScoreResultd
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_COMMENTS", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iRCSAParamCount).Value = scomments
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_DATE", OleDb.OleDbType.Date)
            ObjSFParam(iRCSAParamCount).Value = sdate
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_YEARID ", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = yearid
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_CREATEDBY", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = createdby
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = updatedby
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_IPAddress", OleDb.OleDbType.VarChar)
            ObjSFParam(iRCSAParamCount).Value = ip
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@CA_CompID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = compid
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCBA_ChecklistAuditAssest", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetColorAndRange(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal dValueID As Double, ByVal iSecID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select CRAT_Name,CRAT_Color From CRPA_Rating Where  CRAT_FLAG='A' And CRAT_STARTVALUE<=round(" & dValueID & ",1) AND CRAT_ENDVALUE>=round(" & dValueID & ",1) And CRAT_YEARID=" & iYearID & " And CRAT_COMPID=" & iACID & ""
            If iSecID > 0 Then
                sSql = sSql & " and CRAT_AuditId=" & iSecID & " "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateNetScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iNetscore As Double, ByVal iPkid As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "update CBA_AuditAssest set CA_NetScore ='" & iNetscore & "' where CA_PKID='" & iPkid & "'"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Updatestatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iPkid As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "update CBA_AuditAssest set CA_Status ='Submitted' where CA_PKID='" & iPkid & "'"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateScheduleStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSecID As Integer, ByVal iKitchenID As Integer, ByVal iMonthID As Integer, ByVal iYearID As Integer, ByVal sStatus As String) As String
        Dim sSql As String = ""
        Try
            If sStatus = "Saved" Then
                sSql = "Update CBAAudit_Schedule set AUD_Operation ='Saved' "
            ElseIf sStatus = "Submitted" Then
                sSql = "Update CBAAudit_Schedule set AUD_Operation ='Submitted' "
            End If
            sSql = sSql & " where aud_sectionid=" & iSecID & " and AUD_KitchenID=" & iKitchenID & " and AUD_MonthID=" & iMonthID & " and AUD_YearID=" & iYearID & " and AUD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
