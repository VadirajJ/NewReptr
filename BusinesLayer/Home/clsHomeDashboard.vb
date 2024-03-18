Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsHomeDashboard
    Dim objDBL As New DBHelper
    Dim objGen As New clsEDICTGeneral
    Dim objGenFun As New clsGeneralFunctions

    'Dim DT_Pkid As Integer
    'Dim DT_Userid As Integer
    'Dim DT_filename As String
    'Dim DT_fileformat As String
    'Dim DT_filepath As String
    'Dim DT_Createdby As Integer
    'Dim DT_CreatedOn As DateTime
    'Dim DT_Movedby As Integer
    'Dim DT_Movedon As DateTime
    'Dim DT_Movedto As String
    'Dim DT_IPAddress As String
    'Dim DT_CompID As Integer

    'Public Property iDT_Pkid() As Integer
    '    Get
    '        Return (DT_Pkid)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        DT_Pkid = Value
    '    End Set
    'End Property
    'Public Property iDT_Userid() As Integer
    '    Get
    '        Return (DT_Userid)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        DT_Userid = Value
    '    End Set
    'End Property
    'Public Property sDT_filename() As String
    '    Get
    '        Return (DT_filename)
    '    End Get
    '    Set(ByVal Value As String)
    '        DT_filename = Value
    '    End Set
    'End Property
    'Public Property sDT_fileformat() As String
    '    Get
    '        Return (DT_fileformat)
    '    End Get
    '    Set(ByVal Value As String)
    '        DT_fileformat = Value
    '    End Set
    'End Property
    'Public Property sDT_filepath() As String
    '    Get
    '        Return (DT_filepath)
    '    End Get
    '    Set(ByVal Value As String)
    '        DT_filepath = Value
    '    End Set
    'End Property
    'Public Property iDT_Createdby() As Integer
    '    Get
    '        Return (DT_Createdby)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        DT_Createdby = Value
    '    End Set
    'End Property
    'Public Property dDT_CreatedOn() As DateTime
    '    Get
    '        Return (DT_CreatedOn)
    '    End Get
    '    Set(ByVal Value As DateTime)
    '        DT_CreatedOn = Value
    '    End Set
    'End Property
    'Public Property iDT_Movedby() As Integer
    '    Get
    '        Return (DT_Movedby)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        DT_Movedby = Value
    '    End Set
    'End Property
    'Public Property dDT_Movedon() As DateTime
    '    Get
    '        Return (DT_Movedon)
    '    End Get
    '    Set(ByVal Value As DateTime)
    '        DT_Movedon = Value
    '    End Set
    'End Property
    'Public Property sDT_Movedto() As String
    '    Get
    '        Return (DT_Movedto)
    '    End Get
    '    Set(ByVal Value As String)
    '        DT_Movedto = Value
    '    End Set
    'End Property
    'Public Property sDT_IPAddress() As String
    '    Get
    '        Return (DT_IPAddress)
    '    End Get
    '    Set(ByVal Value As String)
    '        DT_IPAddress = Value
    '    End Set
    'End Property
    'Public Property iDT_CompID() As Integer
    '    Get
    '        Return (DT_CompID)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        DT_CompID = Value
    '    End Set
    'End Property
    Public Sub SavedDocTracker(ByVal sAC As String, ByVal iACID As Integer, ByVal ipkid As Integer, ByVal iuserid As Integer, ByVal IpAddress As String, ByVal Filename As String, ByVal sExtension As String, ByVal SMovedto As String)
        Dim ssql As String
        Dim strsql As String
        Dim dtCheck As New DataTable
        Dim id As Integer

        Try

            strsql = "Select isnull(max(DT_Pkid)+1,1) from DocsTracker"
            id = objDBL.SQLExecuteScalar(sAC, strsql)

            ssql = "" : ssql = "insert into DocsTracker(DT_Pkid,DT_Userid,DT_filename,DT_fileformat,DT_Createdby,DT_CreatedOn,DT_Movedby,DT_Movedon,DT_Movedto,DT_IPAddress,DT_CompID,DT_Status)"
            ssql = ssql & "Values(" & id & "," & iuserid & ",'" & Filename & "','" & sExtension & "'," & iuserid & ",getdate() ," & iuserid & ",getdate(),'" & SMovedto & "','" & IpAddress & "'," & iACID & ",'Deleted')"
            objDBL.SQLExecuteNonQuery(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Function GetMaxNo(ByVal sfield As String, ByVal stable As String) As Long
    '    Dim ssql As String
    '    Dim rsMax As OleDb.OleDbDataReader
    '    Try
    '        ssql = "Select Max(" & sfield & ") from " & stable
    '        rsMax = objDBL.DBExecuteReader(ssql)
    '        If rsMax.Read <> 0 Then
    '            GetMaxNo = Val(rsMax(0) & "") + 1
    '        Else
    '            GetMaxNo = 1
    '        End If
    '        Exit Function
    '        'MsgBox(Err.Description, 48, "Patient Registration")
    '        GetMaxNo = False
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Public Function SaveDocsTracker(ByVal sAC As String, ByVal iACID As Integer, ByVal objDocsTracker As clsHomeDashboard) As Array
    '    Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
    '    Dim iParamCount As Integer
    '    Dim Arr(1) As String
    '    Try
    '        iParamCount = 0
    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_Pkid", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objDocsTracker.iDT_Pkid
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_Userid", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objDocsTracker.iDT_Userid
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_filename", OleDb.OleDbType.VarChar, 500)
    '        ObjParam(iParamCount).Value = objDocsTracker.sDT_filename
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_fileformat", OleDb.OleDbType.VarChar, 50)
    '        ObjParam(iParamCount).Value = objDocsTracker.sDT_fileformat
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_filepath", OleDb.OleDbType.VarChar, 500)
    '        ObjParam(iParamCount).Value = objDocsTracker.sDT_filepath
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_Createdby", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objDocsTracker.iDT_Createdby
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_CreatedOn", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objDocsTracker.dDT_CreatedOn
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_Movedby", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objDocsTracker.iDT_Movedby
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_Movedon", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objDocsTracker.dDT_Movedon
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_Movedto", OleDb.OleDbType.VarChar, 50)
    '        ObjParam(iParamCount).Value = objDocsTracker.sDT_Movedto
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_IPAddress", OleDb.OleDbType.VarChar, 50)
    '        ObjParam(iParamCount).Value = objDocsTracker.sDT_IPAddress
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@DT_CompID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objDocsTracker.iDT_CompID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        Arr(0) = "@iUpdateOrSave"
    '        Arr(1) = "@iOper"

    '        Arr = objDBL.ExecuteSPForInsertARR(sAC, "spDocsTracker", 1, Arr, ObjParam)
    '        Return Arr
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function LoadWorkFlowDetailsToGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal iUserID As Integer)
        Dim sSql As String, iSql As String = "", sGrpId As String = ""
        Dim sDegSql As String = ""
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim dtDeg As New DataTable
        Dim dRow As DataRow
        Dim dtCheck As New DataTable, dtLead As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, iAttachCount As Integer = 0
        Dim sProcess As String = "", sUsers As String = ""
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("WorkFlowID")
            dt.Columns.Add("InwardNo")
            dt.Columns.Add("RefNo")
            dt.Columns.Add("Title")
            dt.Columns.Add("RecDate")
            dt.Columns.Add("Department")
            dt.Columns.Add("ProcessStatus")
            dt.Columns.Add("Status")
            dt.Columns.Add("NoofAttachments")

            sSql = "" : sSql = "Select WIM_PKID,WIM_InwardNo,WIM_DocReferenceno,WIM_Title,WIM_DocRecievedDate,WIM_WorkFlowID,Org_Name,WIM_Delflag,count(ATCH_ID) As AttachCount"
            sSql = sSql & " From WF_Inward_Masters Left Join sad_org_structure On org_node=WIM_Deptartment And Org_levelcode=3 And org_CompId=" & iACID & ""
            sSql = sSql & " Left Join Edt_Attachments On ATCH_ID=WIM_AttachID And ATCH_CompID=" & iACID & " And ATCH_Status<>'D'"
            sSql = sSql & " Where WIM_InwardOrWorkFlow=1 And WIM_WorkFLowArchiveID = 0 and WIM_Progress_Status = 1 And WIM_CompID=" & iACID & ""
            If iStatus = 0 Then
                sSql = sSql & " And WIM_Delflag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And WIM_Delflag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And WIM_Delflag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Group by WIM_PKID,WIM_InwardNo,WIM_DocReferenceno,WIM_Title,WIM_DocRecievedDate,WIM_WorkFlowID,WIM_Delflag,Org_name Order By WIM_PKID Desc"
            dtCheck = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtCheck.Rows.Count > 0 Then
                For i = 0 To dtCheck.Rows.Count - 1
                    iSql = "" : iSql = "Select * from WF_WorkFLow_Lead where WF_WorkFlowID =" & dtCheck.Rows(i)("WIM_WorkFlowID").ToString() & " and "
                    iSql = iSql & "WF_InwardID = " & dtCheck.Rows(i)("WIM_PkID").ToString() & " and  WF_CompID =" & iACID & " order by WF_Level Desc"
                    dtLead = objDBL.SQLExecuteDataTable(sAC, iSql)
                    If dtLead.Rows.Count > 0 Then

                        sProcess = "" : sUsers = ""
                        iSql = "" : iSql = "Select B.WPU_ProcessPKID, B.WPU_UserID from WF_WorkFlow_Process A join WF_WorkFlow_Process_Users B On "
                        iSql = iSql & "A.WP_WorkFLow_ID = " & dtLead.Rows(0)("WF_WorkFLowID").ToString() & " and  A.WP_Process_DelFlag='A' "
                        iSql = iSql & "and A.WP_Process_CompID = " & iACID & " and A.WP_ProcessPkID = " & dtLead.Rows(0)("WF_LeadToProcessID").ToString() & " and "
                        iSql = iSql & " B.WPU_ProcessPKID=" & dtLead.Rows(0)("WF_LeadToProcessID").ToString() & ""
                        dtLead = objDBL.SQLExecuteDataTable(sAC, iSql)

                        If dtLead.Rows.Count > 0 Then
                            For j = 0 To dtLead.Rows.Count - 1
                                If sProcess.Contains(dtLead.Rows(j)("WPU_ProcessPKID").ToString()) = False Then
                                    sProcess = sProcess & "," & dtLead.Rows(j)("WPU_ProcessPKID").ToString()
                                End If

                                If sUsers.Contains(dtLead.Rows(j)("WPU_UserID").ToString()) = False Then
                                    sUsers = sUsers & "," & dtLead.Rows(j)("WPU_UserID").ToString()
                                End If
                            Next
                        End If

                        If sProcess <> "" Then
                            sDegSql = "" : sDegSql = "Select B.Usr_ID from WF_WorkFlow_Process_Designation A join sad_UserDetails B on "
                            sDegSql = sDegSql & "B.Usr_Designation =  A.WPD_DesignationID  and A.WPD_ProcessPKID in(" & sProcess.Remove(0, 1) & ") and B.Usr_DelFlag='A'"
                            dtDeg = objDBL.SQLExecuteDataTable(sAC, sDegSql)
                            If dtDeg.Rows.Count > 0 Then
                                For l = 0 To dtDeg.Rows.Count - 1
                                    If sUsers.Contains(dtDeg.Rows(l)("Usr_ID").ToString()) = False Then
                                        sUsers = sUsers & "," & dtDeg.Rows(l)("Usr_ID").ToString()
                                    End If
                                Next
                            End If
                        End If


                        If sUsers.Contains(iUserID) = True Then
                            dRow = dt.NewRow

                            Dim dtProStatus As New DataTable

                            If IsDBNull(dtCheck.Rows(i)("WIM_PKID")) = False Then
                                dRow("ID") = dtCheck.Rows(i)("WIM_PKID")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_WorkFlowID")) = False Then
                                dRow("WorkFlowID") = dtCheck.Rows(i)("WIM_WorkFlowID")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                dRow("InwardNo") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                dRow("RefNo") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                dRow("Title") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                dRow("RecDate") = objGen.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                dRow("Department") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                            End If

                            dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                            If dtProStatus.Rows.Count > 0 Then
                                dRow("ProcessStatus") = objGen.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Delflag")) = False Then
                                If dtCheck.Rows(i)("WIM_Delflag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dtCheck.Rows(i)("WIM_Delflag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dtCheck.Rows(i)("WIM_Delflag") = "W" Then
                                    dRow("Status") = "Waiting for Approval"
                                End If
                            End If

                            dRow("NoofAttachments") = dtCheck.Rows(i)("AttachCount")

                            dt.Rows.Add(dRow)
                        End If
                    Else

                        sProcess = "" : sUsers = ""
                        iSql = "" : iSql = "Select B.WPU_ProcessPKID, B.WPU_UserID from WF_WorkFlow_Process A join WF_WorkFlow_Process_Users B On "
                        iSql = iSql & "A.WP_WorkFLow_ID = " & dtCheck.Rows(i)("WIM_WorkFlowID").ToString() & " and A.WP_Process_Type=1 and A.WP_Process_DelFlag='A' "
                        iSql = iSql & "and A.WP_Process_CompID = " & iACID & " and A.WP_ProcessPkID = B.WPU_ProcessPKID"
                        dtLead = objDBL.SQLExecuteDataTable(sAC, iSql)
                        If dtLead.Rows.Count > 0 Then
                            For j = 0 To dtLead.Rows.Count - 1
                                If sProcess.Contains(dtLead.Rows(j)("WPU_ProcessPKID").ToString()) = False Then
                                    sProcess = sProcess & "," & dtLead.Rows(j)("WPU_ProcessPKID").ToString()
                                End If

                                If sUsers.Contains(dtLead.Rows(j)("WPU_UserID").ToString()) = False Then
                                    sUsers = sUsers & "," & dtLead.Rows(j)("WPU_UserID").ToString()
                                End If
                            Next
                        End If


                        If sProcess <> "" Then
                            sDegSql = "" : sDegSql = "Select B.Usr_ID from WF_WorkFlow_Process_Designation A join sad_UserDetails B on "
                            sDegSql = sDegSql & "B.Usr_Designation =  A.WPD_DesignationID  and A.WPD_ProcessPKID in(" & sProcess.Remove(0, 1) & ") and B.Usr_DelFlag='A'"
                            dtDeg = objDBL.SQLExecuteDataTable(sAC, sDegSql)
                            If dtDeg.Rows.Count > 0 Then
                                For l = 0 To dtDeg.Rows.Count - 1
                                    If sUsers.Contains(dtDeg.Rows(l)("Usr_ID").ToString()) = False Then
                                        sUsers = sUsers & "," & dtDeg.Rows(l)("Usr_ID").ToString()
                                    End If
                                Next
                            End If
                        End If


                        If sUsers.Contains(iUserID) = True Then
                            dRow = dt.NewRow

                            Dim dtProStatus As New DataTable

                            If IsDBNull(dtCheck.Rows(i)("WIM_PKID")) = False Then
                                dRow("ID") = dtCheck.Rows(i)("WIM_PKID")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_WorkFlowID")) = False Then
                                dRow("WorkFlowID") = dtCheck.Rows(i)("WIM_WorkFlowID")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                dRow("InwardNo") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                dRow("RefNo") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                dRow("Title") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                dRow("RecDate") = objGen.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                dRow("Department") = objGen.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                            End If

                            dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                            If dtProStatus.Rows.Count > 0 Then
                                dRow("ProcessStatus") = objGen.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Delflag")) = False Then
                                If dtCheck.Rows(i)("WIM_Delflag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dtCheck.Rows(i)("WIM_Delflag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dtCheck.Rows(i)("WIM_Delflag") = "W" Then
                                    dRow("Status") = "Waiting for Approval"
                                End If
                            End If

                            dRow("NoofAttachments") = dtCheck.Rows(i)("AttachCount")
                            dt.Rows.Add(dRow)
                        End If

                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllInwardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim dtDisplay As New DataTable, dt As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Dim dtStage As New DataTable
        Dim iAttachCount As Integer = 0
        Dim dtPerm As New DataTable, dtUsers As New DataTable
        Dim sArchUsers As String = ""
        Dim p As Integer = 0, m As Integer = 0
        Try
            dtDisplay.Columns.Add("WIM_PKID")
            dtDisplay.Columns.Add("InwardNo")
            dtDisplay.Columns.Add("Department")
            dtDisplay.Columns.Add("DocumentTitle")
            dtDisplay.Columns.Add("DocumentReferenceNo")
            dtDisplay.Columns.Add("DocumentRecievedDate")
            dtDisplay.Columns.Add("Status")
            dtDisplay.Columns.Add("NoofAttachments")

            'Check the Permission
            sSql = "" : sSql = "Select * from Sad_UsrOrGrp_permission where sgp_modid = 40"
            dtPerm = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtPerm.Rows.Count > 0 Then
                For p = 0 To dtPerm.Rows.Count - 1
                    If dtPerm.Rows(p)("SGP_LevelGroup").ToString() = "U" Then
                        sArchUsers = sArchUsers & "," & dtPerm.Rows(p)("SGP_LevelGroupID").ToString()

                    ElseIf dtPerm.Rows(p)("SGP_LevelGroup").ToString() = "R" Then
                        sSql = "" : sSql = "Select * from sad_USerdetails where Usr_Designation =" & dtPerm.Rows(p)("SGP_LevelGroupID").ToString() & " and Usr_Delflag='A' and Usr_CompID=" & iACID & ""
                        dtUsers = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dtUsers.Rows.Count > 0 Then
                            For m = 0 To dtUsers.Rows.Count - 1
                                sArchUsers = sArchUsers & "," & dtUsers.Rows(m)("Usr_ID").ToString()
                            Next
                        End If
                    End If
                Next
            End If

            dtStage = objDBL.SQLExecuteDataTable(sAC, "Select WIM_Stage,WIM_PKID from WF_Inward_Masters where WIM_CompID=" & iACID & " and WIM_InwardOrWorkFlow=0 order by WIM_PkID Desc")
            If dtStage.Rows.Count > 0 Then
                For j = 0 To dtStage.Rows.Count - 1
                    If IsDBNull(dtStage.Rows(j).Item("WIM_Stage")) = False Then
                        sSql = " Select WIM_PKID,WIM_InwardNo,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,WIM_DelFlag,Org_name,count(ATCH_ID) As AttachCount,WIMH_SentTOID"
                        sSql = sSql & " from WF_Inward_Masters Left Join WF_Inward_Masters_history On WIMH_Stage=WIM_Stage And WIMH_InwardPKID=WIM_PKID And WIMH_CompID=" & iACID & ""
                        sSql = sSql & " And WIMH_SentTOID=" & iUserID & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & ""
                        sSql = sSql & " Left Join Edt_Attachments On ATCH_ID=WIM_AttachID And ATCH_CompID=" & iACID & " And ATCH_Status<>'D'"
                        ' sSql = sSql & " Where (WIMH_SentTOID=" & iUserID & " Or WIM_CreatedBy=" & iUserID & ") And WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 "
                        sSql = sSql & " Where (WIMH_SentTOID=" & iUserID & ") And WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 "
                        sSql = sSql & " And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & " Group by WIM_PKID,WIM_InwardNo,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,"
                        sSql = sSql & " WIMH_SentTOID,WIM_DelFlag,Org_name order by WIM_PkID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("WIM_PKID") = dt.Rows(i)("WIM_PKID")
                                dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Department") = dt.Rows(i)("Org_name")
                                dRow("DocumentTitle") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("DocumentReferenceNo") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                dRow("DocumentRecievedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                End If
                                If IsDBNull(dt.Rows(i)("WIMH_SentTOID")) = False Then
                                    If iUserID = dt.Rows(i)("WIMH_SentTOID") Then
                                        If dt.Rows(i)("WIM_DelFlag") = "W" Then
                                            GoTo NextLoop
                                        End If
                                    End If
                                Else
                                    If dt.Rows(i)("WIM_DelFlag") = "W" Then
                                        dRow("Status") = "Waiting For Approval"
                                    End If
                                End If
                                dRow("NoofAttachments") = dt.Rows(i)("AttachCount")
                                dtDisplay.Rows.Add(dRow)
NextLoop:                   Next
                        Else
                            'Check Permission
                            If sArchUsers.Contains(iUserID) = True Then
                                sSql = "" : sSql = " Select Distinct(WIM_pkid),WIM_InwardNo,Org_name,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,WIM_DelFlag,"
                                sSql = sSql & "org_name,WIM_AttachID from WF_Inward_Masters Left Join WF_Inward_Masters_history On WIMH_Stage=WIM_Stage And WIMH_InwardPKID=WIM_PKID And "
                                sSql = sSql & "WIMH_CompID= " & iACID & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And "
                                sSql = sSql & "Org_CompID =" & iACID & " Where WIM_CompID= " & iACID & " And WIM_InwardOrWorkFlow=0 "
                                sSql = sSql & "And WIM_PKID= " & dtStage.Rows(j).Item("WIM_PKID") & " group by WIM_pkid,WIM_InwardNo,Org_name,WIM_Title,"
                                sSql = sSql & "WIM_DocReferenceno,WIM_DocRecievedDate,WIM_DelFlag,WIM_AttachID Order by WIM_PKID Desc"
                                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                                If dt.Rows.Count > 0 Then
                                    For i = 0 To dt.Rows.Count - 1
                                        dRow = dtDisplay.NewRow
                                        dRow("WIM_PKID") = dt.Rows(i)("WIM_PKID")
                                        dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                        dRow("Department") = dt.Rows(i)("Org_name")
                                        dRow("DocumentTitle") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                        dRow("DocumentReferenceNo") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                        dRow("DocumentRecievedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                                        If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                            dRow("Status") = "Activated"
                                        ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                            dRow("Status") = "De-Activated"
                                        End If
                                        'If IsDBNull(dt.Rows(i)("WIMH_SentTOID")) = False Then
                                        '    If iUserID = dt.Rows(i)("WIMH_SentTOID") Then
                                        '        If dt.Rows(i)("WIM_DelFlag") = "W" Then
                                        '            GoTo NextPermLoop
                                        '        End If
                                        '    End If
                                        'Else
                                        '    If dt.Rows(i)("WIM_DelFlag") = "W" Then
                                        '        dRow("Status") = "Waiting For Approval"
                                        '    End If
                                        'End If
                                        dRow("NoofAttachments") = GetAttachmentCount(sAC, dt.Rows(i)("WIM_AttachID"))
                                        dtDisplay.Rows.Add(dRow)
NextPermLoop:                       Next
                                End If
                            End If
                        End If
                    Else
                        sSql = "Select WIM_PKID,WIM_InwardNo,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,WIM_DelFlag,Org_name,count(ATCH_ID) As AttachCount"
                        sSql = sSql & " From WF_Inward_Masters Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & ""
                        sSql = sSql & " Left Join Edt_Attachments On ATCH_ID=WIM_AttachID And ATCH_CompID=" & iACID & " And ATCH_Status<>'D'"
                        sSql = sSql & " Where WIM_CreatedBy=" & iUserID & " And WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 And WIM_Stage Is NULL"
                        sSql = sSql & " And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & " Group by WIM_PKID,WIM_InwardNo,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,"
                        sSql = sSql & " WIM_DelFlag,Org_name order by WIM_PkID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("WIM_PKID") = dt.Rows(i)("WIM_PKID")
                                dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Department") = dt.Rows(i)("Org_name")
                                dRow("DocumentTitle") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("DocumentReferenceNo") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                dRow("DocumentRecievedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "W" Then
                                    dRow("Status") = "Waiting For Approval"
                                End If
                                dRow("NoofAttachments") = dt.Rows(i)("AttachCount")
                                dtDisplay.Rows.Add(dRow)
                            Next
                        End If
                    End If
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetAttachmentCount(ByVal sAC As String, ByVal iInward As Integer) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer = 0
        Try
            sSql = "Select Count(*) from Edt_Attachments where atch_id =" & iInward & ""
            iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetWFStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iInward As Integer, ByVal iWorkFlowID As Integer)
        Dim sSql As String = "", sStr As String = ""
        Dim iWorkFlow As Integer = 0
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim dtLead As New DataTable
        Try
            sSql = "" : sSql = "Select * from WF_WorkFLow_Lead where WF_WorkFLowID =" & iWorkFlowID & " and WF_InwardID=" & iInward & " and WF_CompID=" & iACID & " order by wf_Level desc"
            dtLead = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtLead.Rows.Count > 0 Then
                sSql = "" : sSql = "Select A.WP_Process_Name,B.WM_WF_Name,A.WP_ProcessPKID,B.WM_ID from WF_WorkFlow_Process A join WF_Workflow_Master B On "
                sSql = sSql & "A.WP_WorkFLow_id = " & iWorkFlowID & " and A.WP_ProcessPKID = " & dtLead.Rows(0)("WF_LeadToProcessID").ToString() & " and "
                sSql = sSql & "A.WP_Process_CompID =" & iACID & " And  B.WM_ID = " & iWorkFlowID & ""
                dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            Else
                sSql = "" : sSql = "Select A.WP_Process_Name,B.WM_WF_Name,A.WP_ProcessPKID,B.WM_ID from WF_WorkFlow_Process A join WF_Workflow_Master B On "
                sSql = sSql & "A.WP_WorkFLow_id = " & iWorkFlowID & " And WP_Process_type = 1 And WP_Process_CompID =" & iACID & " And  B.WM_ID = " & iWorkFlowID & ""
                dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dtdetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadScheduledAuditNos(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SA_ID,SA_AuditNo + ' - ' + CMM_Desc As SA_AuditNo From StandardAudit_Schedule "
            sSql = sSql & " Left Join Content_Management_Master on CMM_ID=SA_AuditTypeID Where SA_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And SA_YearID = " & iFinancialYearID & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " And SA_CustID=" & iCustID & " "
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And SA_AdditionalSupportEmployeeID Like ('%," & iLoginUserID & ",%')"
            End If
            sSql = sSql & " Order by SA_ID desc"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
