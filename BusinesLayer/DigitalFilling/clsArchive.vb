Imports DatabaseLayer
Public Class clsArchive
    Dim objDb As New DBHelper
    Dim objGen As New clsEDICTGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objIndex As New clsIndexing
    Public Function LoadCabinetGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iArchive As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dt As DataTable

        dtDisplay.Columns.Add("ID")
        dtDisplay.Columns.Add("Name")
        dtDisplay.Columns.Add("Org_Name")
        dtDisplay.Columns.Add("Org_node")
        dtDisplay.Columns.Add("CreatedBy")
        dtDisplay.Columns.Add("CreatedOn")

        Try
            sSql = "Select a.CBN_ID,a.CBN_NAME,a.CBN_NOTE,a.CBN_SubCabCount,a.CBN_FolderCount,a.CBN_Department,a.CBN_CreatedBy,a.CBN_CreatedOn,a.CBN_DelFlag,b.Org_Name,b.Org_node "
            sSql = sSql & " from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent=-1"
            If iArchive = 1 Then
                sSql = sSql & " and a.CBN_DelFlag='V'"
            Else
                sSql = sSql & " and a.CBN_DelFlag='A'"
            End If
            sSql = sSql & " order by a.CBN_NAME"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    If IsDBNull(dt.Rows(i)("CBN_ID")) = False Then
                        dRow("ID") = dt.Rows(i)("CBN_ID")
                    End If
                    If IsDBNull(dt.Rows(i)("CBN_NAME")) = False Then
                        dRow("Name") = dt.Rows(i)("CBN_NAME")
                    End If
                    If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                        dRow("Org_Name") = dt.Rows(i)("Org_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("Org_node")) = False Then
                        dRow("Org_node") = dt.Rows(i)("Org_node")
                    End If
                    If IsDBNull(dt.Rows(i)("CBN_CreatedBy")) = False Then
                        dRow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("CBN_CreatedBy"))
                    End If
                    If IsDBNull(dt.Rows(i)("CBN_CreatedOn")) = False Then
                        dRow("CreatedOn") = dt.Rows(i)("CBN_CreatedOn")
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubCabinetGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCabinet As Integer, ByVal iArchive As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dt As DataTable

        dtDisplay.Columns.Add("ID")
        dtDisplay.Columns.Add("Name")
        dtDisplay.Columns.Add("Org_Name")
        dtDisplay.Columns.Add("Org_node")
        dtDisplay.Columns.Add("CreatedBy")
        dtDisplay.Columns.Add("CreatedOn")

        Try
            sSql = "Select a.CBN_ID,a.CBN_NAME,a.CBN_NOTE,a.CBN_SubCabCount,a.CBN_FolderCount,a.CBN_Department,a.CBN_CreatedBy,a.CBN_CreatedOn,a.CBN_DelFlag,b.Org_Name,b.Org_node "
            sSql = sSql & " from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent='" & iCabinet & "' "
            If iArchive = 1 Then
                sSql = sSql & " And cbn_DelFlag='V'"
            Else
                sSql = sSql & " And cbn_DelFlag='A'"
            End If
            sSql = sSql & ""

            sSql = sSql & " order by a.CBN_NAME"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    If IsDBNull(dt.Rows(i)("CBN_ID")) = False Then
                        dRow("ID") = dt.Rows(i)("CBN_ID")
                    End If
                    If IsDBNull(dt.Rows(i)("CBN_NAME")) = False Then
                        dRow("Name") = dt.Rows(i)("CBN_NAME")
                    End If
                    If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                        dRow("Org_Name") = dt.Rows(i)("Org_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("Org_node")) = False Then
                        dRow("Org_node") = dt.Rows(i)("Org_node")
                    End If
                    If IsDBNull(dt.Rows(i)("CBN_CreatedBy")) = False Then
                        dRow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("CBN_CreatedBy"))
                    End If
                    If IsDBNull(dt.Rows(i)("CBN_CreatedOn")) = False Then
                        dRow("CreatedOn") = dt.Rows(i)("CBN_CreatedOn")
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadCabinetGrid1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iArchive As Integer) As DataTable
    '    Dim dtDisplay As New DataTable
    '    Dim i As Integer = 1
    '    Dim dRow As DataRow
    '    Dim sSql As String
    '    Dim dt As DataTable

    '    dtDisplay.Columns.Add("ID")
    '    dtDisplay.Columns.Add("Name")
    '    dtDisplay.Columns.Add("Org_Name")
    '    dtDisplay.Columns.Add("Org_node")
    '    dtDisplay.Columns.Add("CreatedBy")
    '    dtDisplay.Columns.Add("CreatedOn")

    '    Try
    '        sSql = "Select a.CBN_ID,a.CBN_NAME,a.CBN_NOTE,a.CBN_SubCabCount,a.CBN_FolderCount,a.CBN_Department,a.CBN_CreatedBy,a.CBN_CreatedOn,a.CBN_DelFlag,b.Org_Name,b.Org_node "
    '        sSql = sSql & " from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent=-1"

    '        sSql = sSql & " and a.CBN_DelFlag='V'"
    '        'sSql = sSql & " and a.CBN_DelFlag='A'"
    '        sSql = sSql & " order by a.CBN_NAME"
    '        dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                dRow = dtDisplay.NewRow
    '                If IsDBNull(dt.Rows(i)("CBN_ID")) = False Then
    '                    dRow("ID") = dt.Rows(i)("CBN_ID")
    '                End If
    '                If IsDBNull(dt.Rows(i)("CBN_NAME")) = False Then
    '                    dRow("Name") = dt.Rows(i)("CBN_NAME")
    '                End If
    '                If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
    '                    dRow("Org_Name") = dt.Rows(i)("Org_Name")
    '                End If
    '                If IsDBNull(dt.Rows(i)("Org_node")) = False Then
    '                    dRow("Org_node") = dt.Rows(i)("Org_node")
    '                End If
    '                If IsDBNull(dt.Rows(i)("CBN_CreatedBy")) = False Then
    '                    dRow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("CBN_CreatedBy"))
    '                End If
    '                If IsDBNull(dt.Rows(i)("CBN_CreatedOn")) = False Then
    '                    dRow("CreatedOn") = dt.Rows(i)("CBN_CreatedOn")
    '                End If
    '                dtDisplay.Rows.Add(dRow)
    '            Next
    '        End If
    '        Return dtDisplay
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadFolderGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iFolder As Integer, ByVal iArchive As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dt As DataTable

        dtDisplay.Columns.Add("ID")
        dtDisplay.Columns.Add("Name")
        dtDisplay.Columns.Add("Org_Name")
        dtDisplay.Columns.Add("Org_node")
        dtDisplay.Columns.Add("CreatedBy")
        dtDisplay.Columns.Add("CreatedOn")

        Try
            sSql = "" : sSql = "Select Distinct(a.FOL_FOLID),a.FOL_NAME,a.FOL_CABINET,a.FOL_CreatedOn,a.FOL_CreatedBy,a.FOL_Delflag,b.Org_name,b.Org_Name,c.CBN_Name, d.PGE_CABINET, d.PGE_FOLDER from EDT_FOLDER a "
            sSql = sSql & " LEFT JOIN edt_cabinet c On a.FOL_CABINET=c.CBN_ID  "
            sSql = sSql & " Left Join edt_page d On  d.PGE_FOLDER = a.FOL_FOLID "
            sSql = sSql & " Left Join Sad_Org_Structure b On b.Org_Node = c.cbn_Department "
            sSql = sSql & " where a.FOL_CABINET=" & iFolder & ""
            If iArchive = 1 Then
                sSql = sSql & " And pge_Delflag='V'"
            Else
                sSql = sSql & " And pge_Delflag='A'"
            End If
            sSql = sSql & ""

            sSql = sSql & " order by a.FOL_NAME"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    If IsDBNull(dt.Rows(i)("FOL_FOLID")) = False Then
                        dRow("ID") = dt.Rows(i)("FOL_FOLID")
                    End If
                    If IsDBNull(dt.Rows(i)("FOL_NAME")) = False Then
                        dRow("Name") = dt.Rows(i)("FOL_NAME")
                    End If
                    If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                        dRow("Org_Name") = dt.Rows(i)("Org_Name")
                    End If

                    If IsDBNull(dt.Rows(i)("FOL_CreatedBy")) = False Then
                        dRow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("FOL_CreatedBy"))
                    End If
                    If IsDBNull(dt.Rows(i)("FOL_CreatedOn")) = False Then
                        dRow("CreatedOn") = dt.Rows(i)("FOL_CreatedOn")
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPageGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iFolder As Integer, ByVal iArchive As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dt As DataTable

        dtDisplay.Columns.Add("ID")
        dtDisplay.Columns.Add("Name")
        dtDisplay.Columns.Add("Org_Name")
        dtDisplay.Columns.Add("Org_node")
        dtDisplay.Columns.Add("CreatedBy")
        dtDisplay.Columns.Add("CreatedOn")

        Try

            sSql = "" : sSql = "Select Distinct(a.FOL_FOLID),d.pge_OrignalFileName,d.PGE_BASENAME,d.PGE_TITLE,d.Pge_CreatedBy,d.Pge_CreatedOn,a.FOL_NAME,b.Org_Name,a.FOL_CABINET,a.FOL_CreatedOn,a.FOL_CreatedBy,a.FOL_Delflag,b.Org_name,c.CBN_Name, d.PGE_CABINET, d.PGE_FOLDER from EDT_FOLDER a "
            sSql = sSql & " LEFT JOIN edt_cabinet c On a.FOL_CABINET=c.CBN_ID  "
            sSql = sSql & " Left Join edt_page d On  d.PGE_FOLDER = a.FOL_FOLID "
            sSql = sSql & " Left Join Sad_Org_Structure b On b.Org_Node = c.cbn_Department "
            sSql = sSql & " where d.PGE_FOLDER=" & iFolder & ""
            If iArchive = 1 Then
                sSql = sSql & " and d.pge_Delflag='V'"
            End If
            sSql = sSql & " order by a.FOL_NAME"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    If IsDBNull(dt.Rows(i)("PGE_BASENAME")) = False Then
                        dRow("ID") = dt.Rows(i)("PGE_BASENAME")
                    End If
                    If IsDBNull(dt.Rows(i)("pge_OrignalFileName")) = False Then
                        dRow("Name") = dt.Rows(i)("pge_OrignalFileName")
                    End If
                    If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                        dRow("Org_Name") = dt.Rows(i)("Org_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("Pge_CreatedBy")) = False Then
                        dRow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("Pge_CreatedBy"))
                    End If
                    If IsDBNull(dt.Rows(i)("Pge_CreatedOn")) = False Then
                        dRow("CreatedOn") = dt.Rows(i)("Pge_CreatedOn")
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadTitleGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iFolder As Integer, ByVal iArchive As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dt As DataTable
        Dim sTitle As String = ""

        dtDisplay.Columns.Add("ID")
        dtDisplay.Columns.Add("Name")
        dtDisplay.Columns.Add("Title")
        dtDisplay.Columns.Add("Org_Name")
        dtDisplay.Columns.Add("Org_node")
        dtDisplay.Columns.Add("CreatedBy")
        dtDisplay.Columns.Add("CreatedOn")

        Try

            sSql = "" : sSql = "Select Distinct(a.FOL_FOLID),d.PGE_TITLE,d.pge_OrignalFileName,d.PGE_BASENAME,d.PGE_TITLE,d.Pge_CreatedBy,d.Pge_CreatedOn,a.FOL_NAME,b.Org_Name,a.FOL_CABINET,a.FOL_CreatedOn,a.FOL_CreatedBy,a.FOL_Delflag,b.Org_name,c.CBN_Name, d.PGE_CABINET, d.PGE_FOLDER from EDT_FOLDER a "
            sSql = sSql & " LEFT JOIN edt_cabinet c On a.FOL_CABINET=c.CBN_ID  "
            sSql = sSql & " Left Join edt_page d On  d.PGE_FOLDER = a.FOL_FOLID "
            sSql = sSql & " Left Join Sad_Org_Structure b On b.Org_Node = c.cbn_Department "
            sSql = sSql & " where "
            If iFolder <> 0 Then
                sSql = sSql & " d.PGE_FOLDER=" & iFolder & " "
            End If
            If iArchive = 1 Then
                sSql = sSql & " and d.pge_Delflag='V'"
            Else
                sSql = sSql & " and d.pge_Delflag='A'"
            End If
            sSql = sSql & " order by a.FOL_NAME"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If IsDBNull(dt.Rows(i)("PGE_TITLE")) = False Then
                        If dt.Rows(i)("PGE_TITLE") <> sTitle Then
                            dRow = dtDisplay.NewRow
                            If IsDBNull(dt.Rows(i)("PGE_BASENAME")) = False Then
                                dRow("ID") = dt.Rows(i)("PGE_BASENAME")
                            End If
                            dRow("Name") = dt.Rows(i)("PGE_TITLE")
                            sTitle = dt.Rows(i)("PGE_TITLE")
                            If IsDBNull(dt.Rows(i)("PGE_BASENAME")) = False Then
                                dRow("ID") = dt.Rows(i)("PGE_BASENAME")
                            End If
                            If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                                dRow("Org_Name") = dt.Rows(i)("Org_Name")
                            End If
                            If IsDBNull(dt.Rows(i)("Pge_CreatedBy")) = False Then
                                dRow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("Pge_CreatedBy"))
                            End If
                            If IsDBNull(dt.Rows(i)("Pge_CreatedOn")) = False Then
                                dRow("CreatedOn") = dt.Rows(i)("Pge_CreatedOn")
                            End If
                            dtDisplay.Rows.Add(dRow)
                        End If
                    End If

                Next
            End If


            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatus As String, ByVal ifol_id As String, ByVal sDStatus As String, ByVal iUsrId As Integer)
        Dim sSql As String
        Try
            sSql = "Update edt_page set "
            sSql = sSql & " pge_Delflag='" & sDStatus & "', pge_ModBy=" & iUsrId & ", pge_ModOn=Getdate(), PGE_STATUS='V'"
            sSql = sSql & " Where PGE_BASENAME = " & ifol_id & " and Pge_CompID =" & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function FilePageInEdict(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBaseName As Long, ByVal sFilePath As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sImagePath = objIndex.GetImagePath(sNameSpace, iCompID)
            sImagePath = sImagePath & "Archive\" & iBaseName \ 301 & "\"
            objGenFun.CheckAndCreateWorkingDirFromPath(sImagePath)
            sImagePath = sImagePath & iBaseName & ".dat"
            If System.IO.File.Exists(sImagePath) = False Then
                FileCopy(sFilePath, sImagePath)
                FilePageInEdict = True
                System.IO.File.Delete(sFilePath)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFileDetailGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal fileID As Integer) As String
        Dim sSql As String
        Dim fieldName As String
        Try
            sSql = "Select pge_OrignalFileName from edt_page where PGE_BASENAME = '" & fileID & "'"
            fieldName = objDb.SQLExecuteScalar(sNameSpace, sSql)
            If (fieldName = "") Then
                fieldName = "Unnamed File"
            End If
            Return fieldName
        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function GetFilesFromTitle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTitle As String) As DataTable

        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = "Select * from edt_page where PGE_TITLE = '" & sTitle & "'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function LoadUnusedFiles(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iFolder As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dt As DataTable
        Dim sTitle As String = ""
        Dim today As DateTime
        Dim modification As DateTime
        Dim span As TimeSpan
        Dim difDay As Integer
        Dim difTime As [String]

        dtDisplay.Columns.Add("fileID")
        dtDisplay.Columns.Add("fileName")
        dtDisplay.Columns.Add("Title")
        dtDisplay.Columns.Add("Org_Name")
        dtDisplay.Columns.Add("Org_node")
        dtDisplay.Columns.Add("CreatedBy")
        dtDisplay.Columns.Add("CreatedOn")
        dtDisplay.Columns.Add("modDate")
        dtDisplay.Columns.Add("difDate")


        Try

            sSql = "" : sSql = "Select Distinct(a.FOL_FOLID),d.PGE_TITLE,d.PGE_DATE,d.PGE_LastViewed,d.pge_OrignalFileName,d.PGE_BASENAME,d.PGE_TITLE,d.Pge_CreatedBy,d.Pge_CreatedOn,a.FOL_NAME,b.Org_Name,a.FOL_CABINET,a.FOL_CreatedOn,a.FOL_CreatedBy,a.FOL_Delflag,b.Org_name,c.CBN_Name, d.PGE_CABINET, d.PGE_FOLDER from EDT_FOLDER a "
            sSql = sSql & " LEFT JOIN edt_cabinet c On a.FOL_CABINET=c.CBN_ID  "
            sSql = sSql & " Left Join edt_page d On  d.PGE_FOLDER = a.FOL_FOLID "
            sSql = sSql & " Left Join Sad_Org_Structure b On b.Org_Node = c.cbn_Department "
            sSql = sSql & " where PGE_STATUS='A'"
            If iFolder <> 0 Then
                sSql = sSql & " and d.PGE_FOLDER=" & iFolder & " "
            End If
            sSql = sSql & " order by a.FOL_NAME"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If IsDBNull(dt.Rows(i)("PGE_TITLE")) = False Then
                        If dt.Rows(i)("PGE_TITLE") <> sTitle Then
                            dRow = dtDisplay.NewRow

                            dRow = dtDisplay.NewRow
                            dRow("fileName") = dt.Rows(i)("PGE_TITLE")
                            sTitle = dt.Rows(i)("PGE_TITLE")
                            If IsDBNull(dt.Rows(i)("PGE_BASENAME")) = False Then
                                dRow("fileID") = dt.Rows(i)("PGE_BASENAME")
                            End If
                            If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                                dRow("Org_Name") = dt.Rows(i)("Org_Name")
                            End If
                            If IsDBNull(dt.Rows(i)("Pge_CreatedBy")) = False Then
                                dRow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("Pge_CreatedBy"))
                            End If
                            If IsDBNull(dt.Rows(i)("Pge_CreatedOn")) = False Then
                                dRow("CreatedOn") = dt.Rows(i)("Pge_CreatedOn")
                            End If
                            If IsDBNull(dt.Rows(i)("PGE_LastViewed")) = False Then
                                dRow("modDate") = dt.Rows(i)("PGE_LastViewed")
                                modification = dt.Rows(i)("PGE_LastViewed")
                                today = DateTime.Now
                                span = (today - modification)
                                difDay = Convert.ToInt32(span.TotalDays)
                                dRow("difDate") = difDay.ToString()
                            Else
                                If IsDBNull(dt.Rows(i)("PGE_DATE")) = False Then
                                    dRow("modDate") = dt.Rows(i)("PGE_DATE")
                                    modification = dt.Rows(i)("PGE_DATE")
                                    today = DateTime.Now
                                    span = (today - modification)
                                    difDay = Convert.ToInt32(span.TotalDays)
                                    dRow("difDate") = difDay.ToString()
                                End If
                            End If
                            dtDisplay.Rows.Add(dRow)
                        End If
                    End If
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCabinet(ByVal sNameSpace As String, ByVal iCOmpID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select CBN_ID,CBN_NAME from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent=-1 and a.CBN_DelFlag='V' "

            sSql = sSql & " Order by CBN_NAME"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubCab(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParent As Integer) As DataSet
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select CBN_ID,CBN_NAME from edt_cabinet "
            sSql = sSql & " where CBN_PARENT =" & iParent & " and CBN_DelFlag ='V' and CBN_CompID =" & iCompID & " order by cbn_name"
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
