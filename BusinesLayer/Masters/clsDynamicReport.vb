Imports System.IO
Public Class clsDynamicReport
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadDepartment(ByVal sAC As String, ByVal iCompId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name from Sad_Org_Structure where Org_LevelCode=3 And Org_CompID=" & iCompId & " And Org_DelFlag='A' Order By Org_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllUsers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim Sql As String = ""
        Try
            Sql = "Select Usr_ID,Usr_FullName from Sad_UserDetails where Usr_DutyStatus='A' And Usr_CompID=" & iACID & " order by Usr_FullName"
            Return objDBL.SQLExecuteDataTable(sAC, Sql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadScanDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserId As Integer, ByVal iDeptID As Integer, ByVal sPath As String, ByVal dFrom As String, ByVal dTo As String)
        Dim dt As New DataTable
        Dim sAllImages As String = ""
        Dim dRow As DataRow
        Dim dtDept As New DataTable, dtFolderNames As New DataTable, dtScan As New DataTable
        Dim dtNormal As New DataTable
        Dim m As Integer = 0
        Dim sFolPath As String = ""
        Try
            dt.Columns.Add("Folder Name")
            dt.Columns.Add("Total Pages")
            'dt.Columns.Add("Department")

            'dtDept = BindDeptNames(sAC, iACID, sPath, "\NormalScan\")
            'dtFolderNames = BindDeptFolderName(sAC, iACID, iUserId, iDeptID, sPath, "\NormalScan\", dtDept, dFrom, dTo)
            'dRow = dt.NewRow()
            'dRow("Folder Name") = "Normal Scan"
            'dt.Rows.Add(dRow)
            'If dtFolderNames.Rows.Count > 0 Then
            '    For i = 0 To dtFolderNames.Rows.Count - 1
            '        dRow = dt.NewRow()
            '        dRow("Folder Name") = dtFolderNames.Rows(i).Item("FolderName")
            '        dRow("Total Pages") = dtFolderNames.Rows(i).Item("TotalPages")
            '        dt.Rows.Add(dRow)
            '    Next
            'End If

            dRow = dt.NewRow()
            dRow("Folder Name") = "Normal Scan"
            dt.Rows.Add(dRow)

            dtNormal = BindNormalScan(sAC, iACID, iDeptID, iUserId, dFrom, dTo)
            If dtNormal.Rows.Count > 0 Then
                For m = 0 To dtNormal.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("Folder Name") = dtNormal.Rows(m).Item("END_FolderName")
                    sFolPath = sPath & "\NormalScan\" & dtNormal.Rows(m).Item("END_DeptID") & "\" & dtNormal.Rows(m).Item("END_FolderName")

                    If System.IO.Directory.Exists(sFolPath) = True Then
                        Dim icount = Directory.GetFiles(sFolPath, "*.*", SearchOption.AllDirectories).Length
                        dRow("Total Pages") = icount
                    Else
                        dRow("Total Pages") = "0"
                    End If

                    dRow("Department") = objDBL.SQLExecuteScalar(sAC, "Select Org_Name from sad_Org_Structure where Org_node=" & dtNormal.Rows(m)("END_DeptID") & "")
                    dt.Rows.Add(dRow)
                Next
            End If

            dRow = dt.NewRow()
            dt.Rows.Add(dRow)
            dRow = dt.NewRow()
            dRow("Folder Name") = "Batch Scan"
            dt.Rows.Add(dRow)
            dtScan = LoadBatchDetails(sAC, iACID, iDeptID, iUserId, sPath, "\BatchScan\", dFrom, dTo)
            If dtScan.Rows.Count > 0 Then
                For i = 0 To dtScan.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("Folder Name") = dtScan.Rows(i).Item("batch_name")
                    dRow("Total Pages") = dtScan.Rows(i).Item("batch_TotalPage")
                    dRow("Department") = dtScan.Rows(i).Item("org_name")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function BindNormalScan(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDepartment As Integer, ByVal iUserID As Integer, ByVal dFrom As String, ByVal dTo As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            Dim yForm = "yyyy/MM/dd"
            sSql = "" : sSql = "Select * from EDICT_NormalScan_Details where END_CompID =" & iCompID & ""

            If iDepartment > 0 Then
                sSql = sSql & " and END_DeptID= " & iDepartment & " "
            End If

            If iUserID > 0 Then
                sSql = sSql & " and END_CreatedBy= " & iUserID & " "
            End If

            If dFrom <> "" And dTo <> "" Then
                Dim dFromDate = Format(CDate(dFrom), yForm)
                Dim dToDate = Format(CDate(dTo), yForm)
                sSql = sSql & " And END_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            End If
            sSql = sSql & " Order by END_PKID"

            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDeptFolderName(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserId As Integer, ByVal iDeptID As Integer, ByVal sScanPath As String,
                                       ByVal sScanImages As String, ByVal dt As DataTable, ByVal dFrom As String, ByVal dTo As String) As DataTable
        Dim strImageFolderDetails() As String, strImageDetails As String, strImageFolderDet() As String
        Dim dRow As DataRow
        Dim dtDisplay As New DataTable
        Dim sAllImages As String = ""
        Try
            dtDisplay.Columns.Add("FolderName")
            dtDisplay.Columns.Add("TotalPages")
            If iDeptID > 0 Then
                Return dtDisplay
            End If
            For i = 0 To dt.Rows.Count - 1
                For Each Dir As String In Directory.GetDirectories(sScanPath & sScanImages & dt.Rows(i).Item("FolderName") & "\")
                    Dim dirImageFolder As New DirectoryInfo(Dir)
                    strImageDetails = dirImageFolder.Name
                    strImageFolderDetails = strImageDetails.Split("_")
                    dRow = dtDisplay.NewRow
                    dRow("FolderName") = strImageDetails
                    dRow("TotalPages") = Directory.GetFiles(Dir).Length
                    If dFrom <> "" And dTo <> "" Then
                        strImageFolderDet = strImageDetails.Split("@")
                        Dim yForm = "dd/MM/yyyy"
                        Dim dFromD = Format(CDate(strImageFolderDet(0)), yForm)
                        Dim dDate As DateTime = Date.ParseExact(dFromD, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Dim dFromDate As Date = Date.ParseExact(dFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Dim dToDate As Date = Date.ParseExact(dTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        If ((dDate >= dFromDate.Date) And (dDate <= dToDate.Date)) Then
                        Else
                            GoTo NextLoop
                        End If
                    End If
                    If iUserId > 0 Then
                        If strImageFolderDetails(1) <> iUserId Then
                            GoTo NextLoop
                        End If
                    End If
                    dtDisplay.Rows.Add(dRow)
NextLoop:       Next
            Next
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDeptNames(ByVal sAC As String, ByVal iACID As Integer, ByVal sScanPath As String, ByVal sScanImages As String) As DataTable
        Dim strImageFolderDetails() As String, strImageDetails As String
        Dim dRow As DataRow
        Dim dtDisplay As New DataTable
        Dim sAllImages As String = ""
        Try
            dtDisplay.Columns.Add("FolderName")
            For Each Dir As String In Directory.GetDirectories(sScanPath & sScanImages)
                Dim dirImageFolder As New DirectoryInfo(Dir)
                strImageDetails = dirImageFolder.Name
                strImageFolderDetails = strImageDetails.Split("_")
                dRow = dtDisplay.NewRow
                dRow("FolderName") = strImageDetails
                dtDisplay.Rows.Add(dRow)
            Next
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBatchDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iDeptID As Integer, ByVal iUsrID As Integer, ByVal sScanPath As String, ByVal sScanImages As String, ByVal dFrom As String, ByVal dTo As String) As DataTable
        Dim dtScan As New DataTable, dt As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Try
            dtScan.Columns.Add("batch_name")
            dtScan.Columns.Add("batch_TotalPage")
            dtScan.Columns.Add("org_name")
            Dim yForm = "yyyy/MM/dd"
            sSql = "select  a.batch_no,a.batch_name,a.batch_desc,a.batch_indexto,a.batch_TotalPage,a.batch_ix_usrgrpid,a.batch_notes,a.Batch_CreatedOn,"
            sSql = sSql & " a.Batch_IndexingOn,a.batch_status,a.Batch_DelFlag, a.Batch_Department ,a.Batch_Is_BKScan, a.Batch_IS_BKQC ,a.Batch_IS_BkIndex,c.org_Name"
            sSql = sSql & " from EDT_BATCH_MASTER a,Sad_Org_Structure c where a.Batch_Department in(select SUO_DeptID from Sad_UsersInOtherDept "
            If iUsrID > 0 Then
                sSql = sSql & " where SUO_UserId=" & iUsrID & " "
            End If
            sSql = sSql & " ) and a.Batch_Department=c.org_node  and  a.batch_status=0"
            If iDeptID > 0 Then
                sSql = sSql & " And c.org_Node =" & iDeptID & "" 'Activated
            End If
            If dFrom <> "" And dTo <> "" Then
                Dim dFromDate = Format(CDate(dFrom), yForm)
                Dim dToDate = Format(CDate(dTo), yForm)
                sSql = sSql & " And Batch_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            End If
            sSql = sSql & " Order by BATCH_NAME ASC"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtScan.NewRow
                    If IsDBNull(dt.Rows(i)("batch_name")) = False Then
                        dRow("batch_name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("batch_name"))
                    End If
                    If IsDBNull(dt.Rows(i)("batch_TotalPage")) = False Then
                        If System.IO.Directory.Exists(sScanPath & sScanImages & dRow("batch_name")) = True Then
                            dRow("batch_TotalPage") = Directory.GetFiles(sScanPath & sScanImages & dRow("batch_name")).Length
                        Else
                            dRow("batch_TotalPage") = 0
                        End If
                    End If
                    If IsDBNull(dt.Rows(i)("org_name")) = False Then
                        dRow("org_name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("org_name"))
                    End If
                    dtScan.Rows.Add(dRow)
                Next
            End If
            Return dtScan
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllInwardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String) As DataTable
        Dim dtDisplay As New DataTable, dt As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Dim dtStage As New DataTable
        Try
            dtDisplay.Columns.Add("Inward No")
            dtDisplay.Columns.Add("Reference No")
            dtDisplay.Columns.Add("Document Title")
            dtDisplay.Columns.Add("Department")
            dtDisplay.Columns.Add("Status")
            Dim yForm = "yyyy/MM/dd"
            dtStage = objDBL.SQLExecuteDataTable(sAC, "Select WIM_Stage,WIM_PKID from WF_Inward_Masters where WIM_CompID=" & iACID & " and WIM_InwardOrWorkFlow=0 order by WIM_PKID Desc")
            If dtStage.Rows.Count > 0 Then
                For j = 0 To dtStage.Rows.Count - 1
                    If IsDBNull(dtStage.Rows(j).Item("WIM_Stage")) = False Then
                        sSql = " Select *,Org_name from WF_Inward_Masters Left Join WF_Inward_Masters_history On WIMH_Stage=WIM_Stage And WIMH_InwardPKID=WIM_PKID And WIMH_CompID=" & iACID & ""
                        If iUserID > 0 Then
                            sSql = sSql & " and WIMH_SentTOID=" & iUserID & ""
                        End If
                        sSql = sSql & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & " Where"
                        If iUserID > 0 Then
                            ' sSql = sSql & " (WIMH_SentTOID=" & iUserID & " Or WIM_CreatedBy=" & iUserID & ") And "
                            sSql = sSql & " (WIMH_SentTOID=" & iUserID & ") And "
                        End If
                        sSql = sSql & " WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 "
                        sSql = sSql & " And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & ""
                        If iDeptID > 0 Then
                            sSql = sSql & " And org_node=" & iDeptID & ""
                        End If
                        If dFrom <> "" And dTo <> "" Then
                            Dim dFromDate = Format(CDate(dFrom), yForm)
                            Dim dToDate = Format(CDate(dTo), yForm)
                            sSql = sSql & " And WIM_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
                        End If
                        sSql = sSql & " Order by WIM_PKID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("Inward No") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Department") = dt.Rows(i)("Org_name")
                                dRow("Document Title") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("Reference No") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "W" Then
                                    dRow("Status") = "Waiting For Approval"
                                End If
                                dtDisplay.Rows.Add(dRow)
                            Next
                        End If
                    Else
                        sSql = "Select *,Org_name from WF_Inward_Masters Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3"
                        sSql = sSql & " And Org_CompID=" & iACID & " Where"
                        If iUserID > 0 Then
                            sSql = sSql & " WIM_CreatedBy=" & iUserID & " And"
                        End If
                        If iDeptID > 0 Then
                            sSql = sSql & " org_node=" & iDeptID & " And"
                        End If
                        If dFrom <> "" And dTo <> "" Then
                            Dim dFromDate = Format(CDate(dFrom), yForm)
                            Dim dToDate = Format(CDate(dTo), yForm)
                            sSql = sSql & " WIM_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
                        End If
                        sSql = sSql & " WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 And WIM_Stage is NULL"
                        sSql = sSql & " And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & " Order by WIM_PKID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("Inward No") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Department") = dt.Rows(i)("Org_name")
                                dRow("Document Title") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("Reference No") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "W" Then
                                    dRow("Status") = "Waiting For Approval"
                                End If
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
    Public Function LoadWorkFlowDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String, iSql As String = "", sGrpId As String = ""
        Dim sDegSql As String = ""
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim dtDeg As New DataTable
        Dim dRow As DataRow
        Dim dtCheck As New DataTable, dtLead As New DataTable

        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0

        Dim sProcess As String = "", sUsers As String = ""
        Try
            dt.Columns.Add("Inward No")
            dt.Columns.Add("Document Reference No")
            dt.Columns.Add("Document Title")
            dt.Columns.Add("Document Recieved Date")
            dt.Columns.Add("Department")
            dt.Columns.Add("Process Status")

            Dim yForm = "yyyy/MM/dd"
            sSql = "" : sSql = "Select WIM_PKID,WIM_InwardNo,WIM_DocReferenceno,WIM_Title,WIM_DocRecievedDate,WIM_WorkFlowID,Org_Name,WIM_Delflag from WF_Inward_Masters"
            sSql = sSql & " Left Join sad_org_structure On org_node=WIM_Deptartment And Org_levelcode=3 And org_CompId=" & iACID & ""
            sSql = sSql & " Where WIM_InwardOrWorkFlow=1 And WIM_WorkFLowArchiveID = 0 and WIM_Progress_Status = 1 And WIM_CompID=" & iACID & ""
            If iDeptID > 0 Then
                sSql = sSql & " And org_node=" & iDeptID & ""
            End If
            If dFrom <> "" And dTo <> "" Then
                Dim dFromDate = Format(CDate(dFrom), yForm)
                Dim dToDate = Format(CDate(dTo), yForm)
                sSql = sSql & " And WIM_WorkFlowCreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            End If
            sSql = sSql & " Order By WIM_PKID Desc"
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
                        If iUserID > 0 Then
                            If sUsers.Contains(iUserID) = True Then
                                dRow = dt.NewRow

                                Dim dtProStatus As New DataTable

                                If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                    dRow("Inward No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                    dRow("Document Reference No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                    dRow("Document Title") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                    dRow("Document Recieved Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                                End If

                                If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                    dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                                End If

                                dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                                If dtProStatus.Rows.Count > 0 Then
                                    dRow("Process Status") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                                End If
                                dt.Rows.Add(dRow)
                            End If
                        Else
                            dRow = dt.NewRow
                            Dim dtProStatus As New DataTable

                            If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                dRow("Inward No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                dRow("Document Reference No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                dRow("Document Title") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                dRow("Document Recieved Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                            End If

                            dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                            If dtProStatus.Rows.Count > 0 Then
                                dRow("Process Status") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                            End If

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
                        If iUserID > 0 Then
                            If sUsers.Contains(iUserID) = True Then
                                dRow = dt.NewRow

                                Dim dtProStatus As New DataTable

                                If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                    dRow("Inward No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                    dRow("Document Reference No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                    dRow("Document Title") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                    dRow("Document Recieved Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                                End If

                                If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                    dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                                End If

                                dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                                If dtProStatus.Rows.Count > 0 Then
                                    dRow("Process Status") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                                End If
                                dt.Rows.Add(dRow)
                            End If
                        Else
                            dRow = dt.NewRow

                            Dim dtProStatus As New DataTable

                            If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                dRow("Inward No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                dRow("Document Reference No") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                dRow("Document Title") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                dRow("Document Recieved Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                            End If

                            dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                            If dtProStatus.Rows.Count > 0 Then
                                dRow("Process Status") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                            End If
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
    Public Function BindWorkFlowArchiveDetails(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim dRow As DataRow
        Dim iValue As Integer = 0
        Try
            dtDisplay.Columns.Add("Inward No")
            dtDisplay.Columns.Add("WorkFlow Name")
            dtDisplay.Columns.Add("Reference No")
            dtDisplay.Columns.Add("Document Title")
            dtDisplay.Columns.Add("Recieved Date")
            dtDisplay.Columns.Add("Department")
            If iUserID > 0 Then
                iValue = GetLoginUserPermission(sAC, iACId, iUserID, "WFA")
                If iValue = 0 Then
                    Return dtDisplay
                End If
            End If
            Dim yForm = "yyyy/MM/dd"
            sSql = "Select WIM_PKID,WIM_InwardNo,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,Org_name,WM_WF_Name,Org_Node,WM_WF_Name,WP_ProcessPKID,WF_CreatedOn From WF_Inward_Masters"
            sSql = sSql & " Left Join WF_WORKFLOW_MASTER On WM_Id=WIM_WorkFLowID Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACId & ""
            sSql = sSql & " Left Join wf_WorkFlow_Process On WP_WorkFlow_ID=WIM_WorkFLowID And WP_Process_Type=3 And WP_Process_CompID=" & iACId & ""
            sSql = sSql & " Left Join wf_WorkFlow_Lead On WF_WorkFlowID=WP_WorkFlow_ID And WF_ProcessID=WP_ProcessPKID And WF_InwardID=WIM_PKID And WF_CompID=" & iACId & ""
            sSql = sSql & " Where WIM_WorkFLowArchiveID=1 And WIM_Delflag='A' And WM_DelFlag='A' And WIM_CompID=" & iACId & " And WM_CompID=" & iACId & ""
            If iDeptID > 0 Then
                sSql = sSql & " And org_node=" & iDeptID & ""
            End If
            If dFrom <> "" And dTo <> "" Then
                Dim dFromDate = Format(CDate(dFrom), yForm)
                Dim dToDate = Format(CDate(dTo), yForm)
                sSql = sSql & " And WF_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("Inward No") = dt.Rows(i)("WIM_InwardNo")
                    dRow("WorkFlow Name") = dt.Rows(i)("WM_WF_Name")
                    dRow("Department") = dt.Rows(i)("Org_name")
                    dRow("Document Title") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                    dRow("Reference No") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                    dRow("Recieved Date") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoginUserPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sModCode As String) As Integer
        Dim sSql As String
        Dim iModuleID As Integer
        Try
            sSql = "" : sSql = "Select Mod_ID from SAD_MODULE where Mod_Code='" & sModCode & "'"
            iModuleID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            'Check Is SuperUser
            If iModuleID = 0 Then
                Return 0
            Else
                sSql = "" : sSql = "Select SGP_ID from Sad_UsrOrGrp_permission where SGP_ModID=" & iModuleID & " And SGP_LevelGroupID=" & iUserID & ""
                Return objDBL.SQLExecuteScalarInt(sAC, sSql)
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadScanDetailsReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserId As Integer, ByVal iDeptID As Integer, ByVal sPath As String, ByVal dFrom As String, ByVal dTo As String)
        Dim dt As New DataTable
        Dim sAllImages As String = ""
        Dim dRow As DataRow
        Dim dtDept As New DataTable, dtFolderNames As New DataTable, dtScan As New DataTable
        Dim dtNormal As New DataTable
        Dim m As Integer = 0
        Dim sFolPath As String = ""
        Try
            dt.Columns.Add("FolderName")
            dt.Columns.Add("TotalPages")
            dt.Columns.Add("Department")

            'dtDept = BindDeptNames(sAC, iACID, sPath, "\NormalScan\")
            'dtFolderNames = BindDeptFolderName(sAC, iACID, iUserId, iDeptID, sPath, "\NormalScan\", dtDept, dFrom, dTo)
            'dRow = dt.NewRow()
            'dRow("Folder Name") = "Normal Scan"
            'dt.Rows.Add(dRow)
            'If dtFolderNames.Rows.Count > 0 Then
            '    For i = 0 To dtFolderNames.Rows.Count - 1
            '        dRow = dt.NewRow()
            '        dRow("Folder Name") = dtFolderNames.Rows(i).Item("FolderName")
            '        dRow("Total Pages") = dtFolderNames.Rows(i).Item("TotalPages")
            '        dt.Rows.Add(dRow)
            '    Next
            'End If

            dRow = dt.NewRow()
            dRow("FolderName") = "Normal Scan"
            dt.Rows.Add(dRow)

            dtNormal = BindNormalScan(sAC, iACID, iDeptID, iUserId, dFrom, dTo)
            If dtNormal.Rows.Count > 0 Then
                For m = 0 To dtNormal.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FolderName") = dtNormal.Rows(m).Item("END_FolderName")
                    sFolPath = sPath & "\NormalScan\" & dtNormal.Rows(m).Item("END_DeptID") & "\" & dtNormal.Rows(m).Item("END_FolderName")

                    If System.IO.Directory.Exists(sFolPath) = True Then
                        Dim icount = Directory.GetFiles(sFolPath, "*.*", SearchOption.AllDirectories).Length
                        dRow("TotalPages") = icount
                    Else
                        dRow("TotalPages") = "0"
                    End If

                    dRow("Department") = objDBL.SQLExecuteScalar(sAC, "Select Org_Name from sad_Org_Structure where Org_node=" & dtNormal.Rows(m)("END_DeptID") & "")
                    dt.Rows.Add(dRow)
                Next
            End If

            dRow = dt.NewRow()
            dt.Rows.Add(dRow)
            dRow = dt.NewRow()
            dRow("FolderName") = "Batch Scan"
            dt.Rows.Add(dRow)
            dtScan = LoadBatchDetails(sAC, iACID, iDeptID, iUserId, sPath, "\BatchScan\", dFrom, dTo)
            If dtScan.Rows.Count > 0 Then
                For i = 0 To dtScan.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FolderName") = dtScan.Rows(i).Item("batch_name")
                    dRow("TotalPages") = dtScan.Rows(i).Item("batch_TotalPage")
                    dRow("Department") = dtScan.Rows(i).Item("org_name")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAllInwardDetailsReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String) As DataTable
        Dim dtDisplay As New DataTable, dt As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Dim dtStage As New DataTable
        Try
            dtDisplay.Columns.Add("InwardNo")
            dtDisplay.Columns.Add("ReferenceNo")
            dtDisplay.Columns.Add("DocumentTitle")
            dtDisplay.Columns.Add("Department")
            dtDisplay.Columns.Add("Status")
            Dim yForm = "yyyy/MM/dd"
            dtStage = objDBL.SQLExecuteDataTable(sAC, "Select WIM_Stage,WIM_PKID from WF_Inward_Masters where WIM_CompID=" & iACID & " and WIM_InwardOrWorkFlow=0 order by WIM_PKID Desc")
            If dtStage.Rows.Count > 0 Then
                For j = 0 To dtStage.Rows.Count - 1
                    If IsDBNull(dtStage.Rows(j).Item("WIM_Stage")) = False Then
                        sSql = " Select *,Org_name from WF_Inward_Masters Left Join WF_Inward_Masters_history On WIMH_Stage=WIM_Stage And WIMH_InwardPKID=WIM_PKID And WIMH_CompID=" & iACID & ""
                        If iUserID > 0 Then
                            sSql = sSql & " and WIMH_SentTOID=" & iUserID & ""
                        End If
                        sSql = sSql & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & " Where"
                        If iUserID > 0 Then
                            ' sSql = sSql & " (WIMH_SentTOID=" & iUserID & " Or WIM_CreatedBy=" & iUserID & ") And "
                            sSql = sSql & " (WIMH_SentTOID=" & iUserID & ") And "
                        End If
                        sSql = sSql & " WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 "
                        sSql = sSql & " And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & ""
                        If iDeptID > 0 Then
                            sSql = sSql & " And org_node=" & iDeptID & ""
                        End If
                        If dFrom <> "" And dTo <> "" Then
                            Dim dFromDate = Format(CDate(dFrom), yForm)
                            Dim dToDate = Format(CDate(dTo), yForm)
                            sSql = sSql & " And WIM_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
                        End If
                        sSql = sSql & " Order by WIM_PKID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Department") = dt.Rows(i)("Org_name")
                                dRow("DocumentTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("ReferenceNo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "W" Then
                                    dRow("Status") = "Waiting For Approval"
                                End If
                                dtDisplay.Rows.Add(dRow)
                            Next
                        End If
                    Else
                        sSql = "Select *,Org_name from WF_Inward_Masters Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3"
                        sSql = sSql & " And Org_CompID=" & iACID & " Where"
                        If iUserID > 0 Then
                            sSql = sSql & " WIM_CreatedBy=" & iUserID & " And"
                        End If
                        If iDeptID > 0 Then
                            sSql = sSql & " org_node=" & iDeptID & " And"
                        End If
                        If dFrom <> "" And dTo <> "" Then
                            Dim dFromDate = Format(CDate(dFrom), yForm)
                            Dim dToDate = Format(CDate(dTo), yForm)
                            sSql = sSql & " WIM_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
                        End If
                        sSql = sSql & " WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 And WIM_Stage is NULL"
                        sSql = sSql & " And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & " Order by WIM_PKID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Department") = dt.Rows(i)("Org_name")
                                dRow("DocumentTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("ReferenceNo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "W" Then
                                    dRow("Status") = "Waiting For Approval"
                                End If
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

    Public Function LoadWorkFlowDetailsReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String, iSql As String = "", sGrpId As String = ""
        Dim sDegSql As String = ""
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim dtDeg As New DataTable
        Dim dRow As DataRow
        Dim dtCheck As New DataTable, dtLead As New DataTable

        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0

        Dim sProcess As String = "", sUsers As String = ""
        Try
            dt.Columns.Add("InwardNo")
            dt.Columns.Add("DocumentReferenceNo")
            dt.Columns.Add("DocumentTitle")
            dt.Columns.Add("DocumentRecievedDate")
            dt.Columns.Add("Department")
            dt.Columns.Add("ProcessStatus")

            Dim yForm = "yyyy/MM/dd"
            sSql = "" : sSql = "Select WIM_PKID,WIM_InwardNo,WIM_DocReferenceno,WIM_Title,WIM_DocRecievedDate,WIM_WorkFlowID,Org_Name,WIM_Delflag from WF_Inward_Masters"
            sSql = sSql & " Left Join sad_org_structure On org_node=WIM_Deptartment And Org_levelcode=3 And org_CompId=" & iACID & ""
            sSql = sSql & " Where WIM_InwardOrWorkFlow=1 And WIM_WorkFLowArchiveID = 0 and WIM_Progress_Status = 1 And WIM_CompID=" & iACID & ""
            If iDeptID > 0 Then
                sSql = sSql & " And org_node=" & iDeptID & ""
            End If
            If dFrom <> "" And dTo <> "" Then
                Dim dFromDate = Format(CDate(dFrom), yForm)
                Dim dToDate = Format(CDate(dTo), yForm)
                sSql = sSql & " And WIM_WorkFlowCreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            End If
            sSql = sSql & " Order By WIM_PKID Desc"
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
                        If iUserID > 0 Then
                            If sUsers.Contains(iUserID) = True Then
                                dRow = dt.NewRow

                                Dim dtProStatus As New DataTable

                                If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                    dRow("InwardNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                    dRow("DocumentReferenceNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                    dRow("DocumentTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                    dRow("DocumentRecievedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                                End If

                                If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                    dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                                End If

                                dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                                If dtProStatus.Rows.Count > 0 Then
                                    dRow("ProcessStatus") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                                End If
                                dt.Rows.Add(dRow)
                            End If
                        Else
                            dRow = dt.NewRow
                            Dim dtProStatus As New DataTable

                            If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                dRow("InwardNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                dRow("DocumentReferenceNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                dRow("DocumentTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                dRow("DocumentRecievedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                            End If

                            dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                            If dtProStatus.Rows.Count > 0 Then
                                dRow("ProcessStatus") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                            End If

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
                        If iUserID > 0 Then
                            If sUsers.Contains(iUserID) = True Then
                                dRow = dt.NewRow

                                Dim dtProStatus As New DataTable

                                If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                    dRow("InwardNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                    dRow("DocumentReferenceNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                    dRow("DocumentTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                                End If

                                If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                    dRow("DocumentRecievedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                                End If

                                If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                    dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                                End If

                                dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                                If dtProStatus.Rows.Count > 0 Then
                                    dRow("ProcessStatus") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                                End If
                                dt.Rows.Add(dRow)
                            End If
                        Else
                            dRow = dt.NewRow

                            Dim dtProStatus As New DataTable

                            If IsDBNull(dtCheck.Rows(i)("WIM_InwardNo")) = False Then
                                dRow("InwardNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_InwardNo"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocReferenceno")) = False Then
                                dRow("DocumentReferenceNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_DocReferenceno"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_Title")) = False Then
                                dRow("DocumentTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("WIM_Title"))
                            End If

                            If IsDBNull(dtCheck.Rows(i)("WIM_DocRecievedDate")) = False Then
                                dRow("DocumentRecievedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtCheck.Rows(i)("WIM_DocRecievedDate"), "F")
                            End If

                            If IsDBNull(dtCheck.Rows(i)("Org_Name")) = False Then
                                dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dtCheck.Rows(i)("Org_Name"))
                            End If

                            dtProStatus = GetWFStatus(sAC, iACID, dtCheck.Rows(i)("WIM_PKID"), dtCheck.Rows(i)("WIM_WorkFlowID"))
                            If dtProStatus.Rows.Count > 0 Then
                                dRow("ProcessStatus") = objclsGRACeGeneral.ReplaceSafeSQL(dtProStatus.Rows(0)("WP_Process_Name"))
                            End If
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

    Public Function BindWorkFlowArchiveDetailsReports(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim dRow As DataRow
        Dim iValue As Integer = 0
        Try
            dtDisplay.Columns.Add("InwardNo")
            dtDisplay.Columns.Add("WorkFlowName")
            dtDisplay.Columns.Add("ReferenceNo")
            dtDisplay.Columns.Add("DocumentTitle")
            dtDisplay.Columns.Add("RecievedDate")
            dtDisplay.Columns.Add("Department")
            If iUserID > 0 Then
                iValue = GetLoginUserPermission(sAC, iACId, iUserID, "WFA")
                If iValue = 0 Then
                    Return dtDisplay
                End If
            End If
            Dim yForm = "yyyy/MM/dd"
            sSql = "Select WIM_PKID,WIM_InwardNo,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,Org_name,WM_WF_Name,Org_Node,WM_WF_Name,WP_ProcessPKID,WF_CreatedOn From WF_Inward_Masters"
            sSql = sSql & " Left Join WF_WORKFLOW_MASTER On WM_Id=WIM_WorkFLowID Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACId & ""
            sSql = sSql & " Left Join wf_WorkFlow_Process On WP_WorkFlow_ID=WIM_WorkFLowID And WP_Process_Type=3 And WP_Process_CompID=" & iACId & ""
            sSql = sSql & " Left Join wf_WorkFlow_Lead On WF_WorkFlowID=WP_WorkFlow_ID And WF_ProcessID=WP_ProcessPKID And WF_InwardID=WIM_PKID And WF_CompID=" & iACId & ""
            sSql = sSql & " Where WIM_WorkFLowArchiveID=1 And WIM_Delflag='A' And WM_DelFlag='A' And WIM_CompID=" & iACId & " And WM_CompID=" & iACId & ""
            If iDeptID > 0 Then
                sSql = sSql & " And org_node=" & iDeptID & ""
            End If
            If dFrom <> "" And dTo <> "" Then
                Dim dFromDate = Format(CDate(dFrom), yForm)
                Dim dToDate = Format(CDate(dTo), yForm)
                sSql = sSql & " And WF_CreatedOn between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                    dRow("WorkFlowName") = dt.Rows(i)("WM_WF_Name")
                    dRow("Department") = dt.Rows(i)("Org_name")
                    dRow("DocumentTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                    dRow("ReferenceNo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                    dRow("RecievedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindUserDetails(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String)  'Vijeth  26/09/2019
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim dRow As DataRow
        Dim iValue As Integer = 0
        Dim slno As Integer = 0
        Try
            dtDisplay.Columns.Add("Sl No")
            dtDisplay.Columns.Add("Login Date_Time")
            dtDisplay.Columns.Add("User Name")
            dtDisplay.Columns.Add("Login Name")
            dtDisplay.Columns.Add("Designation")
            dtDisplay.Columns.Add("User EmailID")
            dtDisplay.Columns.Add("Status")


            Dim yForm = "yyyy/MM/dd"
            Dim dFromDate = Format(CDate(dFrom), yForm)
            Dim dToDate = Format(CDate(dTo), yForm)
            sSql = sSql & "Select ALP_LOGTYPE,USR_id,ALP_DATE,USR_FULLNAME,USR_LOGINNAME,USR_Email,USR_Designation FROM  Audit_Log_Operations"
            sSql = sSql & " Join sad_UserDetails On ALP_UserID = USR_ID"
            sSql = sSql & "  where USR_Compid='" & iACId & "' And ALP_DATE between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            If iDeptID > 0 Then
                sSql = sSql & " And USR_Deptid=" & iDeptID & ""
            End If
            If iUserID > 0 Then
                sSql = sSql & " And USR_id=" & iUserID & ""
            End If
            sSql = sSql & " order by alp_pkid desc"

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    slno = slno + 1
                    dRow("Sl No") = slno
                    dRow("Login Date_Time") = dt.Rows(i)("ALP_DATE")

                    If IsDBNull(dt.Rows(i)("USR_FullName")) = False Then
                        dRow("User Name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_FullName"))
                    End If
                    If IsDBNull(dt.Rows(i)("USR_LoginName")) = False Then
                        dRow("Login Name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_LoginName"))
                    End If
                    If IsDBNull(dt.Rows(i)("USR_Designation")) = False Then
                        dRow("Designation") = userdesignation(sAC, objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_Designation")))
                    End If
                    If IsDBNull(dt.Rows(i)("USR_Email")) = False Then
                        dRow("User EmailID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_Email"))
                    End If
                    If IsDBNull(dt.Rows(i)("ALP_LOGTYPE")) = False Then
                        dRow("Status") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ALP_LOGTYPE"))
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindUserDetailsReports(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim dRow As DataRow
        Dim iValue As Integer = 0
        Dim slno As Integer = 0
        Try
            dtDisplay.Columns.Add("SlNo")
            dtDisplay.Columns.Add("LoginDateTime")
            dtDisplay.Columns.Add("UserName")
            dtDisplay.Columns.Add("LoginName")
            dtDisplay.Columns.Add("Designation")
            dtDisplay.Columns.Add("UserEmailID")
            dtDisplay.Columns.Add("Status")

            Dim yForm = "yyyy/MM/dd"
            Dim dFromDate = Format(CDate(dFrom), yForm)
            Dim dToDate = Format(CDate(dTo), yForm)
            sSql = sSql & "Select ALP_LOGTYPE,USR_id,ALP_DATE,USR_FULLNAME,USR_LOGINNAME,USR_Email,USR_Designation FROM  Audit_Log_Operations"
            sSql = sSql & " Join sad_UserDetails On ALP_UserID = USR_ID"
            sSql = sSql & "  where USR_Compid='" & iACId & "' And ALP_DATE between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            If iUserID > 0 Then
                sSql = sSql & " And USR_id=" & iUserID & ""
            End If
            sSql = sSql & " order by alp_pkid desc"

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    slno = slno + 1
                    dRow("SlNo") = slno
                    dRow("LoginDateTime") = dt.Rows(i)("ALP_DATE")

                    If IsDBNull(dt.Rows(i)("USR_FullName")) = False Then
                        dRow("UserName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_FullName"))
                    End If
                    If IsDBNull(dt.Rows(i)("USR_LoginName")) = False Then
                        dRow("LoginName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_LoginName"))
                    End If
                    If IsDBNull(dt.Rows(i)("USR_Designation")) = False Then
                        dRow("Designation") = userdesignation(sAC, objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_Designation")))
                    End If
                    If IsDBNull(dt.Rows(i)("USR_Email")) = False Then
                        dRow("UserEmailID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_Email"))
                    End If
                    If IsDBNull(dt.Rows(i)("ALP_LOGTYPE")) = False Then
                        dRow("Status") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ALP_LOGTYPE"))
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function userdesignation(ByVal sAC As String, ByVal iDesigID As Integer) As String
        Dim ssql As String
        Try
            'ssql = "select Mas_DESCRIPTION from sad_designation_master where Mas_id=" & iDesigID & ""
            ssql = "select Mas_DESCRIPTION from SAD_GRPDESGN_General_Master where Mas_id=" & iDesigID & ""
            Return objDBL.SQLExecuteScalar(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDeptName(ByVal sAC As String, ByVal iDeptID As Integer) As String
        Dim ssql As String
        Try
            ssql = "select Org_Name from sad_org_structure where Org_Node=" & iDeptID & ""
            Return objDBL.SQLExecuteScalar(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSearchAndDownload(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer, ByVal iDeptID As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim dRow As DataRow
        Dim iValue As Integer = 0
        Dim slno As Integer = 0
        Try
            dtDisplay.Columns.Add("SlNo")
            dtDisplay.Columns.Add("Date_Time")
            dtDisplay.Columns.Add("Operation")
            dtDisplay.Columns.Add("Title")
            dtDisplay.Columns.Add("Ext")
            dtDisplay.Columns.Add("User")
            dtDisplay.Columns.Add("Department")
            dtDisplay.Columns.Add("Cabinet")
            dtDisplay.Columns.Add("SubCabinet")
            dtDisplay.Columns.Add("Folder")
            dtDisplay.Columns.Add("DocumetType")


            Dim yForm = "yyyy/MM/dd"
            Dim dFromDate = Format(CDate(dFrom), yForm)
            Dim dToDate = Format(CDate(dTo), yForm)
            sSql = sSql & "Select PVD_Date,PVD_LogOperation,PGE_TITLE,PGE_EXT,USR_FULLNAME,Org_Name,A.CBN_Name as Cabinet,B.CBN_Name as SubCabinet,FOL_Name,DOT_DOCNAME"
            sSql = sSql & " from EDT_PAGE_ViewAndDownloadlogs"
            sSql = sSql & " join edt_cabinet A ON  pvd_cabinet =a.cbn_id"
            sSql = sSql & " join edt_cabinet B ON b.cbn_id =PVD_SubCabinet"
            sSql = sSql & " JOIN EDT_FOLDER ON PVD_FOLDER = FOL_FOLID "
            sSql = sSql & " JOIN edt_document_type ON DOT_DOCTYPEID = PVD_DocumentType"
            sSql = sSql & " JOIN EDT_PAGE ON PVD_PageDetailsID = PGE_BASENAME"
            sSql = sSql & " JOIN SAD_USERDETAILS ON USR_ID = PVD_UserId"
            sSql = sSql & " JOIN Sad_Org_Structure ON Org_NODE = PVD_DEPID"
            sSql = sSql & "  where PVD_COMPID='" & iACId & "' And PVD_Date between '" & dFromDate & "'  and  DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "'))"
            If iDeptID > 0 Then
                sSql = sSql & " And PVD_DEPID=" & iDeptID & ""
            End If
            If iUserID > 0 Then
                sSql = sSql & " And PVD_UserId=" & iUserID & ""
            End If
            sSql = sSql & " order by PVD_PKID desc"

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    slno = slno + 1
                    dRow("SlNo") = slno
                    dRow("Date_Time") = dt.Rows(i)("PVD_Date")

                    If IsDBNull(dt.Rows(i)("PVD_LogOperation")) = False Then
                        dRow("Operation") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PVD_LogOperation"))
                    End If
                    If IsDBNull(dt.Rows(i)("PGE_TITLE")) = False Then
                        dRow("Title") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PGE_TITLE"))
                    End If
                    If IsDBNull(dt.Rows(i)("PGE_EXT")) = False Then
                        dRow("Ext") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PGE_EXT"))
                    End If
                    If IsDBNull(dt.Rows(i)("USR_FULLNAME")) = False Then
                        dRow("User") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("USR_FULLNAME"))
                    End If
                    If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                        dRow("Department") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Org_Name"))
                    End If
                    If IsDBNull(dt.Rows(i)("Cabinet")) = False Then
                        dRow("Cabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Cabinet"))
                    End If
                    If IsDBNull(dt.Rows(i)("SubCabinet")) = False Then
                        dRow("SubCabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SubCabinet"))
                    End If
                    If IsDBNull(dt.Rows(i)("FOL_Name")) = False Then
                        dRow("Folder") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("FOL_Name"))
                    End If
                    If IsDBNull(dt.Rows(i)("DOT_DOCNAME")) = False Then
                        dRow("DocumetType") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("DOT_DOCNAME"))
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
