Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsCBAIssueFollowup
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral

    Public Property iCID_PKID As Integer
    Public Property iCID_YearID As Integer
    Public Property iCID_AuditCodeID As Integer

    Public Property iCID_ChecklistID As Integer
    Public Property sCID_IssueJobNo As String
    Public Property iCID_SectionID As Integer
    Public Property iCID_SubSectionID As Integer
    Public Property iCID_ProcessID As Integer
    Public Property iCID_SubProcessID As Integer
    Public Property iCID_FactorsID As Integer
    Public Property iCID_DescriptorID As Integer
    Public Property iCID_DescriptionID As Integer
    Public Property iCID_Results As Integer
    Public Property sCID_IssueHeading As String
    Public Property sCID_IssueDetails As String
    Public Property sCID_Impact As String
    Public Property sCID_ActionPlan As String
    Public Property iCID_IssueRatingID As Integer
    Public Property sCID_ActualLoss As String
    Public Property dCID_TargetDate As Date
    Public Property iCID_ResponsibleFunctionID As Integer
    Public Property iCID_FunctionManagerID As Integer
    Public Property iCID_FunctionHODID As Integer
    Public Property sCID_Remarks As String
    Public Property iCID_AttachID As Integer
    Public Property sCID_IssueStatus As String
    Public Property iCID_CreatedBy As Integer

    Public Property iCID_UpdatedBy As Integer
    Public Property iCID_SubmittedBy As Integer

    Public Property sCID_Status As String
    Public Property iCID_CompID As Integer
    Public Property sCID_IPAddress As String
    Public Property iCIH_PKID As Integer

    Public Function LoadAuditCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iSecID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select AUD_id, AUD_Code from CBAAudit_Schedule where AUD_operation='Submitted' and AUD_SEctionid=" & iSecID & " and AUD_compid=" & iACID & "  order by AUD_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllAuditType(ByVal sAC As String, ByVal iAcID As Integer) As DataTable
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
    Public Function GetCustomerName(ByVal sAC As String, ByVal iACID As Integer, ByVal iAUDCodeID As Integer) As Integer
        Dim sSql As String
        Dim iRet As Integer = 0
        Try
            sSql = "select AUD_KitchenID from CBAAudit_Schedule where AUD_ID=" & iAUDCodeID & " and AUD_CompID=" & iACID & " "
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingIssue(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iIssueStatus As Integer, ByVal iSecId As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String, aSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim iCAPKID As Integer = 0

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("PKID")
        dtDisplay.Columns.Add("AssignID")
        dtDisplay.Columns.Add("SubSection")
        dtDisplay.Columns.Add("Process")
        dtDisplay.Columns.Add("SubProcess")
        dtDisplay.Columns.Add("SubSectionID")
        dtDisplay.Columns.Add("ProcessID")
        dtDisplay.Columns.Add("SubProcessID")
        dtDisplay.Columns.Add("AuditComment")
        dtDisplay.Columns.Add("IssueID")
        dtDisplay.Columns.Add("IssueStatus")

        Try

            aSql = "select CA_PKID from CBA_AuditAssest where CA_FinancialYear=" & iYearID & "  and CA_AsgNo=" & iAuditID & " and CA_SECTIONID=" & iSecId & ""
            iCAPKID = objDBL.SQLExecuteScalarInt(sNameSpace, aSql)


            sSql = "select a.CRAD_PKID,a.CRAD_CAuditID,b.CAS_ID as SectionID,b.CAS_SECTIONNAME as Sectionname,c.CASU_ID as SubSectionID, c.CASU_SUBSECTIONNAME as SubSectionName, "
            sSql = sSql & " d.cap_id as processID,d.CAP_PROCESSNAME as Processname,e.CASP_ID as SubProcessID,e.CASP_SUBPROCESSNAME as SubProcess, "
            sSql = sSql & " a.CRAD_Comments, j.CID_PKID,j.CID_IssueStatus  from CBA_ChecklistAuditAssest a "
            sSql = sSql & " Left Join CRPA_Section b on b.CAS_Id=a.CRAD_SECTIONID "
            sSql = sSql & " Left Join CRPA_SubSection c on c.CASU_Id=a.CRAD_SUBSECTIONID"
            sSql = sSql & " Left Join CRPA_Process d on d.CAP_Id=a.CRAD_ProcessID"
            sSql = sSql & " Left Join CRPA_SubProcess e on e.CASP_ID=a.CRAD_SUBPROCESSID"
            sSql = sSql & " left join CBA_IssueTracker_details j on j.CID_AuditCodeID=a.CRAD_CAuditID and j.CID_ChecklistID=a.CRAD_PKID"
            sSql = sSql & " Left Join CBA_AuditAssest k on k.CA_PKID=a.CRAD_CAuditID "
            sSql = sSql & " where CASP_CompId ='" & iACID & "' "
            If iAuditID > 0 Then
                sSql = sSql & " and CRAD_CAuditID ='" & iCAPKID & "'"
                If iSecId = 1 Then
                    sSql = sSql & " and CRAD_Score_Result=1"
                ElseIf iSecId = 8 Then
                    sSql = sSql & " and CRAD_Score_Result=0"
                ElseIf iSecId = 11 Then
                    sSql = sSql & " and CRAD_Score_Result=1"
                End If
            End If
            If iIssueStatus > 0 Then
                sSql = sSql & " and CID_IssueStatus =" & iIssueStatus & ""
            End If
            sSql = sSql & " and k.CA_Status='Submitted' order by CRAD_PKID asc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("CRAD_PKID")) = False Then
                        dRow("PKID") = dr("CRAD_PKID")
                    End If
                    If IsDBNull(dr("CRAD_CAuditID")) = False Then
                        dRow("AssignID") = dr("CRAD_CAuditID")
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
                    If IsDBNull(dr("CRAD_COMMENTS")) = False Then
                        dRow("AuditComment") = dr("CRAD_COMMENTS")
                    End If
                    If IsDBNull(dr("CID_PKID")) = False Then
                        dRow("IssueID") = dr("CID_PKID")
                    End If
                    If IsDBNull(dr("CID_IssueStatus")) = False Then
                        If dr("CID_IssueStatus") = 1 Then
                            dRow("IssueStatus") = "Open"
                        ElseIf dr("CID_IssueStatus") = 2 Then
                            dRow("IssueStatus") = "Closed"
                        ElseIf dr("CID_IssueStatus") = 3 Then
                            dRow("IssueStatus") = "Ongoing"
                        End If
                    Else
                        dRow("IssueStatus") = "Not Started"
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
    'Public Function LoadIssueFromPKID(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Try

    '        sSql = "select a.CRAD_PKID,a.CRAD_CAuditID,b.CAS_ID as SectionID,b.CAS_SECTIONNAME as Sectionname,c.CASU_ID as SubSectionID, c.CASU_SUBSECTIONNAME as SubSectionName, "
    '        sSql = sSql & " d.cap_id as processID,d.CAP_PROCESSNAME as Processname,e.CASP_ID as SubProcessID,e.CASP_SUBPROCESSNAME as SubProcess, f.CF_PKID as FactorID, f.CF_Name as FactorName, "
    '        sSql = sSql & " g.CFC_PKID as FCategoryID, g.CFC_Name as FCategoryName, h.CD_PKID as DescriptorsID, h.CD_Name as DescriptorName, i.CCD_PKID as DescriptionID, i.CCD_Name as DescName, "
    '        sSql = sSql & " a.CRAD_Results, a.CRAD_COMMENTS from CBA_ChecklistAuditAssest a "
    '        sSql = sSql & " Left Join CRPA_Section b on b.CAS_Id=a.CRAD_SECTIONID "
    '        sSql = sSql & " Left Join CRPA_SubSection c on c.CASU_Id=a.CRAD_SUBSECTIONID"
    '        sSql = sSql & " Left Join CRPA_Process d on d.CAP_Id=a.CRAD_ProcessID"
    '        sSql = sSql & " Left Join CRPA_SubProcess e on e.CASP_ID=a.CRAD_SUBPROCESSID"
    '        sSql = sSql & " Left Join CAIQ_Factors f on f.CF_pkid=a.CRAD_FactorsID"
    '        sSql = sSql & " Left Join CAIQ_FactorCategory g on g.CFC_PKID=a.CRAD_FCategoryID"
    '        sSql = sSql & " Left Join CAIQ_Descriptors h on h.CD_PKID=a.CRAD_DescriptorID"
    '        sSql = sSql & " Left Join CAIQ_CategoryDescription i on i.CCD_PKID=a.CRAD_DescriptionID"
    '        sSql = sSql & " where CRAD_PKID =" & iPKID & " and  CASP_CompId ='" & iACID & "' order by CRAD_PKID asc"

    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetAuditCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select * from CBAAudit_Schedule where AUD_ID=" & iAuditID & "  and AUD_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAllIssueTrackerJobCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CID_PKID,CID_IssueJobNo From CBA_IssueTracker_details Where CID_AuditCodeID=" & iAuditID & " and  CID_YearID=" & iYearID & "  And CID_CompID=" & iACID & " Order by CID_PKID Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMaxID(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*)+1 from CBA_IssueTracker_details where CID_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Sub SubmitIssueTracker(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iITID As Integer)
        Dim sSql As String
        Try
            sSql = "Update CBA_IssueTracker_details Set CID_Status='Submitted',CID_SubmittedBy=" & iUserID & ",CID_SubmittedOn=GetDate() Where CID_PKID=" & iITID & "  And CID_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveIssueTracker(ByVal sAC As String, ByVal objFCIDDetails As clsCBAIssueFollowup)

        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_AuditCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_ChecklistID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_ChecklistID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_IssueJobNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_IssueJobNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_SectionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_SectionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_SubSectionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_SubSectionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_ProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_ProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_SubProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_SubProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_Results", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_IssueHeading", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_IssueHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_IssueDetails", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_IssueDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_Impact", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_Impact
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_ActionPlan", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_ActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_IssueRatingID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_IssueRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_ActualLoss", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_ActualLoss
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objFCIDDetails.dCID_TargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_ResponsibleFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_ResponsibleFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_FunctionManagerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_FunctionManagerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_FunctionHODID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_FunctionHODID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_IssueStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_IssueStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_CompID", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFCIDDetails.sCID_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CID_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFCIDDetails.iCID_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCBA_IssueTracker_details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadIssueTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iITPkID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select *,MIM_Name,MIM_Color from CBA_IssueTracker_details "
            sSql = sSql & " Left join MST_InherentRisk_Master On MIM_ID=CID_IssueRatingID and MIM_CompID=" & iACID & ""
            sSql = sSql & "Where CID_CompID=" & iACID & " And CID_PKID= " & iITPkID & " And  CID_AuditCodeID=" & iAuditID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerSelectedStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iITID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CID_Status From CBA_IssueTracker_details Where CID_AuditCodeID=" & iAuditID & " And CID_PKID=" & iITID & " And CID_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingFollowup(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iIssueStatus As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("AuditCode")
        dtDisplay.Columns.Add("AuditCodeID")
        dtDisplay.Columns.Add("AuditChecklistID")
        dtDisplay.Columns.Add("IssueTrackerID")
        dtDisplay.Columns.Add("IssueRatingColor")
        dtDisplay.Columns.Add("AUDTitle")
        dtDisplay.Columns.Add("Customer")
        dtDisplay.Columns.Add("Month")
        dtDisplay.Columns.Add("IssueHeading")
        dtDisplay.Columns.Add("IssueDetails")
        dtDisplay.Columns.Add("IssueRating")
        dtDisplay.Columns.Add("ActionPlan")
        dtDisplay.Columns.Add("TargetDate")
        dtDisplay.Columns.Add("IssueStatus")
        dtDisplay.Columns.Add("AttachID")

        Try

            sSql = "select a.CID_PKID,a.CID_YearID,a.CID_AuditCodeID,a.CID_ChecklistID,a.CID_IssueJobNo,a.CID_SectionID,a.CID_IssueHeading, "
            sSql = sSql & " a.CID_IssueDetails,a.CID_Impact,a.CID_ActionPlan,a.CID_TargetDate,a.CID_IssueStatus,a.CID_AttachID,b.AUD_ID,b.AUD_Code, "
            sSql = sSql & " b.AUD_Title,b.AUD_MonthID,c.CUST_ID,c.CUST_NAME,d.MIM_ID, d.MIM_Color,d.MIM_Name, e.CRAD_PKID from CBA_IssueTracker_details a "
            sSql = sSql & " left join CBAAudit_Schedule b on b.AUD_ID=a.CID_AuditCodeID and b.AUD_YearID=" & iYearID & " "
            sSql = sSql & " left join SAD_CUSTOMER_MASTER c on c.CUST_ID=b.AUD_KitchenID "
            sSql = sSql & " left join MST_InherentRisk_Master d on d.MIM_ID=a.CID_IssueRatingID"
            sSql = sSql & " left join CBA_ChecklistAuditAssest e on e.CRAD_PKID=a.CID_ChecklistID"
            sSql = sSql & " where a.CID_YearID=" & iYearID & " and a.CID_Status='Submitted' and a.CID_CompID='" & iACID & "' "
            If iAuditID > 0 Then
                sSql = sSql & " and a.CID_AuditCodeID='" & iAuditID & "'"
            End If
            If iIssueStatus > 0 Then
                sSql = sSql & " and a.CID_IssueStatus=" & iIssueStatus & ""
            End If
            sSql = sSql & " order by CID_PKID asc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    If IsDBNull(dr("AUD_Code")) = False Then
                        dRow("AuditCode") = dr("AUD_Code")
                    End If
                    If IsDBNull(dr("AUD_ID")) = False Then
                        dRow("AuditCodeID") = dr("AUD_ID")
                    End If
                    If IsDBNull(dr("CRAD_PKID")) = False Then
                        dRow("AuditChecklistID") = dr("CRAD_PKID")
                    End If
                    If IsDBNull(dr("CID_PKID")) = False Then
                        dRow("IssueTrackerID") = dr("CID_PKID")
                    End If
                    If IsDBNull(dr("MIM_Color")) = False Then
                        dRow("IssueRatingColor") = dr("MIM_Color")
                    End If
                    If IsDBNull(dr("AUD_Title")) = False Then
                        dRow("AUDTitle") = dr("AUD_Title")
                    End If
                    If IsDBNull(dr("CUST_NAME")) = False Then
                        dRow("Customer") = dr("CUST_NAME")
                    End If
                    If IsDBNull(dr("AUD_MonthID")) = False Then
                        dRow("Month") = objclsGeneralFunctions.GetMonthNameFromMothID(dr("AUD_MonthID"))
                    End If
                    If IsDBNull(dr("CID_IssueHeading")) = False Then
                        dRow("IssueHeading") = dr("CID_IssueHeading")
                    End If
                    If IsDBNull(dr("CID_IssueDetails")) = False Then
                        dRow("IssueDetails") = dr("CID_IssueDetails")
                    End If
                    If IsDBNull(dr("MIM_Name")) = False Then
                        dRow("IssueRating") = dr("MIM_Name")
                    End If
                    If IsDBNull(dr("CID_ActionPlan")) = False Then
                        dRow("ActionPlan") = dr("CID_ActionPlan")
                    End If
                    If IsDBNull(dr("CID_TargetDate")) = False Then
                        dRow("TargetDate") = dr("CID_TargetDate").ToString.Substring(0, 10)
                    End If
                    If IsDBNull(dr("CID_IssueStatus")) = False Then
                        If dr("CID_IssueStatus") = 1 Then
                            dRow("IssueStatus") = "Open"
                        ElseIf dr("CID_IssueStatus") = 2 Then
                            dRow("IssueStatus") = "Closed"
                        ElseIf dr("CID_IssueStatus") = 3 Then
                            dRow("IssueStatus") = "Ongoing"
                        End If
                    End If
                    If IsDBNull(dr("CID_AttachID")) = False Then
                        dRow("AttachID") = dr("CID_AttachID")
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
    Public Function LoadIssueFollowupFromPKID(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try

            sSql = "select a.CRAD_PKID,a.CRAD_CAuditID,b.CAS_ID as SectionID,b.CAS_SECTIONNAME as Sectionname,c.CASU_ID as SubSectionID, c.CASU_SUBSECTIONNAME as SubSectionName, "
            sSql = sSql & " d.cap_id as processID,d.CAP_PROCESSNAME as Processname,e.CASP_ID as SubProcessID,e.CASP_SUBPROCESSNAME as SubProcess, "
            sSql = sSql & " a.CRAD_SCORE_RESULT, a.CRAD_COMMENTS,k.*, l.MIM_Color, l.MIM_Name from CBA_ChecklistAuditAssest a "
            sSql = sSql & " Left Join CRPA_Section b on b.CAS_Id=a.CRAD_SECTIONID "
            sSql = sSql & " Left Join CRPA_SubSection c on c.CASU_Id=a.CRAD_SUBSECTIONID"
            sSql = sSql & " Left Join CRPA_Process d on d.CAP_Id=a.CRAD_ProcessID"
            sSql = sSql & " Left Join CRPA_SubProcess e on e.CASP_ID=a.CRAD_SUBPROCESSID"
            sSql = sSql & " Left Join CBA_IssueTracker_details k on k.CID_ChecklistID=a.CRAD_PKID"
            sSql = sSql & " Left Join MST_InherentRisk_Master l on l.MIM_ID=k.CID_IssueRatingID"
            sSql = sSql & " where CRAD_PKID =" & iPKID & " and  CRAD_CompId ='" & iACID & "' order by CRAD_PKID asc"

            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SavedIssueTracker(ByVal sAC As String, ByVal sACID As String, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iITID As Integer)
        Dim sSql As String
        Try
            sSql = "Update CBA_IssueTracker_details Set CID_Status='Saved',CID_SubmittedBy=" & iUserID & ",CID_SubmittedOn=GetDate() Where CID_PKID=" & iITID & "  And CID_CompID='" & sACID & "'"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function SaveIssueTrackerHistory(ByVal sAC As String, ByVal objclsCBAIssueFollowup As clsCBAIssueFollowup, ByVal iCIDPKID As Integer)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCIH_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_CIDPKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCIDPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCID_AuditCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_ActionPlan", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.sCID_ActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.dCID_TargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_IssueStatus", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.sCID_IssueStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_ResponsibleFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCID_ResponsibleFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_FunctionManagerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCID_FunctionManagerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_FunctionHODID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCID_FunctionHODID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.sCID_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCID_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCID_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.iCID_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIH_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCBAIssueFollowup.sCID_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCBA_IssueTracker_History", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActionPlanGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iITID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("ActionPlan")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("ResponsibleFunction")
            dt.Columns.Add("OwnerName")
            dt.Columns.Add("Remarks")
            sSql = "Select CIH_PKID,CIH_ActionPlan,CIH_IssueStatus,CIH_Remarks,Convert(Varchar(10),CIH_TargetDate,103)CIH_TargetDate,CIH_ResponsibleFunctionID,"
            sSql = sSql & " CIH_FunctionManagerID,a.usr_FullName as aUsrName,b.usr_FullName From CBA_IssueTracker_History"
            sSql = sSql & " Left Join Sad_UserDetails a On CIH_ResponsibleFunctionID=a.usr_Id And a.usr_CompId=" & iACID & ""
            sSql = sSql & " Left Join Sad_UserDetails b On CIH_FunctionManagerID=b.usr_Id And b.usr_CompId=" & iACID & ""
            sSql = sSql & " Where CIH_CompID=" & iACID & " And CIH_CIDPKID=" & iITID & " Order by CIH_PKID Desc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    If IsDBNull(dtDetails.Rows(i)("CIH_ActionPlan")) = False Then
                        dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("CIH_ActionPlan").ToString)
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CIH_Remarks")) = False Then
                        dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("CIH_Remarks").ToString)
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CIH_TargetDate")) = False Then
                        dRow("TargetDate") = dtDetails.Rows(i)("CIH_TargetDate")
                    End If
                    If dtDetails.Rows(i)("CIH_IssueStatus") = 1 Then
                        dRow("Status") = "Open"
                    ElseIf dtDetails.Rows(i)("CIH_IssueStatus") = 2 Then
                        dRow("Status") = "Closed"
                    ElseIf dtDetails.Rows(i)("CIH_IssueStatus") = 3 Then
                        dRow("Status") = "Ongoing"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CIH_ResponsibleFunctionID")) = False Then
                        dRow("ResponsibleFunction") = dtDetails.Rows(i)("aUsrName")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CIH_FunctionManagerID")) = False Then
                        dRow("OwnerName") = dtDetails.Rows(i)("usr_FullName")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
