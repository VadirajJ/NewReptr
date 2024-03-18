Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure strCollationDoc
    Dim iCLD_COLLATENO As Integer
    Dim iCLD_DOCID As Integer
    Dim iCLD_PAGEID As Integer
    Public Property iCOLLATENO() As Integer
        Get
            Return (iCLD_COLLATENO)
        End Get
        Set(ByVal Value As Integer)
            iCLD_COLLATENO = Value
        End Set
    End Property
    Public Property iDOCID() As Integer
        Get
            Return (iCLD_DOCID)
        End Get
        Set(ByVal Value As Integer)
            iCLD_DOCID = Value
        End Set
    End Property
    Public Property iPAGEID() As Integer
        Get
            Return (iCLD_PAGEID)
        End Get
        Set(ByVal Value As Integer)
            iCLD_PAGEID = Value
        End Set
    End Property
End Structure
Public Class clsSearch
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsEDICTGeneral As New clsEDICTGeneral
    Dim dtPageX As New DataTable
    Dim dtPage As New DataTable
    Dim Permdt As New DataTable
    Dim dtPerm As New DataTable
    Dim dtGrp As New DataTable
    Dim dtMain As New DataTable
    Dim dsMain As New DataSet
    Public sLocation() As String
    Dim sPermlvl As String = "", sMem As String = "", sFolPerm As String = "", sCabPerm As String = "", sCabName As String = "", sDTPerm As String = ""
    Dim iParGrp As Integer, iUsrParGrp As Integer = 0, iUsrType As Integer = 0
    Public Function GetFolderNames() As DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("Id")
            dt.Columns.Add("Fields")
            dt.Columns.Add("SelectedID")
            dt.Columns.Add("SelectedName")
            dRow = dt.NewRow
            dRow("Id") = "CB"
            dRow("Fields") = "Cabinets"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "SC"
            dRow("Fields") = "SubCabinets"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "FD"
            dRow("Fields") = "Folders"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "DE"
            dRow("Fields") = "Date"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "TT"
            dRow("Fields") = "Title"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "KW"
            dRow("Fields") = "Keywords"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "OC"
            dRow("Fields") = "OCRText"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "FT"
            dRow("Fields") = "Format"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "CR"
            dRow("Fields") = "Created by"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "AD"
            dRow("Fields") = "Any Descriptor"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "DT"
            dRow("Fields") = "DocumentTypes"
            dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SetRows(ByVal dt As DataTable, ByVal i As Integer) As DataTable
        Dim iRow As Integer
        Dim dr As DataRow
        Try
            If dt.Rows.Count <> 0 Then
                If dt.Rows.Count >= i Then
                    Return dt
                Else
                    iRow = i - dt.Rows.Count
                End If
            Else
                iRow = 0
            End If

            For i = 0 To iRow - 1
                dr = dt.NewRow
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    'Cabinets
    Public Function LoadCabinets(ByVal sAC As String, ByVal LogUsrId As Int16, ByVal iCompID As Integer, Optional ByVal iCabID As Integer = 0, Optional ByVal sPerm As String = "SRH") As DataTable
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim sSql As String
        Dim iRet As Integer
        Try
            'Modified by Badari.G On 5-3-2007
            Permdt = BuildPermTable()
            sMem = GetMemberGroups(sAC, LogUsrId)
            iUsrType = GetUserType(sAC, LogUsrId)
            iUsrParGrp = GetUserParGrp(sAC, LogUsrId)
            If (iUsrType = 1) Then
                'User Logged is Super User
                If (iCabID = 0) Then
                    sSql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent=-1 order by CBN_Name"
                    UpdateFolderCount(sAC, sSql)
                Else
                    sSql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent = " & iCabID & " order by CBN_Name "
                End If
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For Each dRow In dt.Rows
                        iRet = GetFinalPermissions(sAC, dRow("CBN_ID"), LogUsrId, sPerm)
                        If iRet = 1 Then
                            AddPermissions(sAC, dtPerm, sPermlvl, iCompID)
                        End If
                    Next
                End If
                Return Permdt
                Exit Function
            End If
            sCabPerm = GetPermCabinets(sAC, LogUsrId, sMem)
            If (iCabID = 0) Then
                sSql = " Select *  from edt_cabinet where CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_Parent= -1 "
            Else
                sSql = " Select *  from edt_cabinet where  CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_Parent = " & iCabID & "  "
            End If
            If Val(sCabPerm) <> 0 Then
                sSql = sSql & " and CBN_ID Not in (" & sCabPerm & ") order by CBN_Name "
            Else
                sSql = sSql & " order by CBN_Name "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For Each dRow In dt.Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = GetFinalPermissions(sAC, dRow("CBN_ID"), LogUsrId, sPerm)
                    If (iRet = 1) Then
                        AddPermissions(sAC, dtPerm, sPermlvl, iCompID)
                    End If
                Next
            End If
            If (iCabID = 0) Then
                sSql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent= -1"
            Else
                sSql = " Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent = " & iCabID & ""
            End If
            If Len((sCabPerm)) <> 0 Then
                sSql = sSql & " and CBN_ID in (" & sCabPerm & ")"
            Else
                sSql = sSql & " and CBN_ID in (0)"
            End If
            sSql = sSql & " order by CBN_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For Each dRow In dt.Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = GetFinalPermissions(sAC, dRow("CBN_ID"), LogUsrId, sPerm, 1)
                    If (iRet = 1) Then
                        AddPermissions(sAC, dtPerm, sPermlvl, iCompID)
                    End If
                Next
            End If
            Return Permdt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildPermTable() As DataTable
        Dim PermDt As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("Name", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabPar", GetType(Int16))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabNote", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabCrtGrp", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabCrtUsr", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabParGrp", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabCrOn", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabPer", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("SubCabNo", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("FolNo", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("PLevel", GetType(String))
            PermDt.Columns.Add(dc)
            Return PermDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMemberGroups(ByVal sAC As String, ByVal iUsrId As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID=" & iUsrId
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            sSql = ""
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = sSql & "," & dt.Rows(i)("SUO_DeptID")
                Next
                If sSql.Length > 0 Then
                    sSql = sSql.Remove(0, 1)
                Else
                    sSql = 0
                End If
            End If
            Return sSql
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserType(ByVal sAC As String, ByVal iUserId As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_IsSuperuser from sad_userdetails where usr_id=" & iUserId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserParGrp(ByVal sAC As String, ByVal iLogUsrID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select USR_DeptID from sad_Userdetails where usr_id=" & iLogUsrID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateFolderCount(ByVal sAC As String, ByVal sSql As String)
        Dim dt As New DataTable, dtCab As New DataTable
        Dim iSql As String, mSql As String
        Dim i As Integer
        Try
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iSql = "Select count(*) as Count from edt_folder where fol_cabinet in (select CBN_ID from edt_cabinet where cbn_parent=" & dt.Rows(i)("CBN_ID") & ") and Fol_status='A'"
                    dtCab = objDBL.SQLExecuteDataTable(sAC, iSql)
                    If dtCab.Rows.Count > 0 Then
                        For j = 0 To dtCab.Rows.Count - 1
                            mSql = "Update edt_cabinet set CBN_FolderCount=" & dtCab.Rows(j)("Count") & " where CBN_ID=" & dt.Rows(i)("CBN_ID") & ""
                            objDBL.SQLExecuteNonQuery(sAC, mSql)
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetFinalPermissions(ByVal sAC As String, ByVal iCabId As Integer, ByVal iUserId As Int16, Optional ByVal sPerType As String = "ALL", Optional ByVal iChkType As Int16 = 0) As Object
        Try
            If iChkType = 2 Then
                sCabPerm = String.Empty
            End If
            sPermlvl = String.Empty
            If iChkType = 2 Then
                iParGrp = GetParGrpID(sAC, iCabId)
                iUsrParGrp = GetUserParGrp(sAC, iUserId)
            End If
            dtPerm = GetMainPermDSCabinet(sAC, iCabId, iUserId, iParGrp, iChkType)
            If (dtPerm.Rows.Count > 0) Then
                Select Case UCase(sPerType)
                    Case "ALL"
                        Dim Ht As New Hashtable
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Ht.Add("CModify", 0)
                                Ht.Add("CView", 1)
                                Ht.Add("CDelete", 0)
                                Ht.Add("CCreate", 0)
                                Ht.Add("FCreate", 0)
                                Ht.Add("CIndex", 1)
                                Ht.Add("CSearch", 1)
                                Ht.Add("Level", sPermlvl)
                            Else
                                Ht.Add("CModify", 0)
                                Ht.Add("CView", 1)
                                Ht.Add("CDelete", 0)
                                Ht.Add("CCreate", 0)
                                Ht.Add("FCreate", 0)
                                Ht.Add("CIndex", 0)
                                Ht.Add("CSearch", 1)
                                Ht.Add("Level", sPermlvl)
                            End If
                            Return Ht
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Ht.Add("CCreate", 1)
                            Ht.Add("CModify", 1)
                            Ht.Add("CView", 1)
                            Ht.Add("CDelete", 1)
                            Ht.Add("FCreate", 1)
                            Ht.Add("CIndex", 1)
                            Ht.Add("CSearch", 1)
                            Ht.Add("Level", sPermlvl)
                            Return Ht
                        Else
                            Ht.Add("CCreate", dtPerm.Rows(0).Item("CBP_Create"))
                            Ht.Add("CModify", dtPerm.Rows(0).Item("CBP_Modify"))
                            Ht.Add("CView", dtPerm.Rows(0).Item("CBP_View"))
                            Ht.Add("CDelete", dtPerm.Rows(0).Item("CBP_Delete"))
                            Ht.Add("FCreate", dtPerm.Rows(0).Item("CBP_Create_Folder"))
                            Ht.Add("CIndex", dtPerm.Rows(0).Item("CBP_Index"))
                            Ht.Add("CSearch", dtPerm.Rows(0).Item("CBP_Search"))
                            Ht.Add("Level", sPermlvl)
                            Return Ht
                        End If

                    Case "CSC"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        If IsDBNull(dtPerm.Rows(0).Item("CBP_Create")) = False Then
                            Return dtPerm.Rows(0).Item("CBP_Create")
                        Else
                            Return 0
                        End If
                    Case "MCB"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        If IsDBNull(dtPerm.Rows(0).Item("CBP_Modify")) = False Then
                            Return dtPerm.Rows(0).Item("CBP_Modify")
                        Else
                            Return 0
                        End If
                    Case "DCB"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        If IsDBNull(dtPerm.Rows(0).Item("CBP_Delete")) = False Then
                            Return dtPerm.Rows(0).Item("CBP_Delete")
                        Else
                            Return 0
                        End If
                    Case "VCB"
                        If (sPermlvl = "PG") Then
                            Return 1
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        If IsDBNull(dtPerm.Rows(0).Item("CBP_View")) = False Then
                            Return dtPerm.Rows(0).Item("CBP_View")
                        Else
                            Return 0
                        End If
                    Case "CFD"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        If IsDBNull(dtPerm.Rows(0).Item("CBP_Create_Folder")) = False Then
                            Return dtPerm.Rows(0).Item("CBP_Create_Folder")
                        Else
                            Return 0
                        End If
                    Case "IDX"
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Return 1
                            Else
                                Return 0
                            End If
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        If IsDBNull(dtPerm.Rows(0).Item("CBP_Index")) = False Then
                            Return dtPerm.Rows(0).Item("CBP_Index")
                        Else
                            Return 0
                        End If
                    Case "SRH"
                        If (sPermlvl = "PG") Then
                            Return 1
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        If IsDBNull(dtPerm.Rows(0).Item("CBP_Search")) = False Then
                            Return dtPerm.Rows(0).Item("CBP_Search")
                        Else
                            Return 0
                        End If
                End Select
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetParGrpID(ByVal sAC As String, ByVal iDTId As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Dot_PGroup from edt_document_type where Dot_DocTypeID=" & iDTId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetMainPermDSCabinet(ByVal sAC As String, ByVal iCabId As Integer, ByVal iUserId As Int16, ByVal iGrpId As Int16, Optional ByVal ChkType As Integer = 0) As DataTable
        Dim sSql As String
        Try
            'Check For Group Head
            sMem = GetMemberGroups(sAC, iUserId)
            If ChkType = 2 Then
                iUsrType = GetUserType(sAC, iUserId)
            End If
            If (iUsrType = 1) Then
                sPermlvl = "PU"
                sSql = "Select * from edt_cabinet where CBN_ID=" & iCabId & ""
                dsMain = objDBL.SQLExecuteDataSet(sAC, sSql)
            ElseIf (CheckForGrpHead(sAC, iGrpId, iUserId) = 1) Then
                sPermlvl = "GH"
                sSql = "Select * from edt_cabinet where CBN_ID=" & iCabId & ""
                dsMain = objDBL.SQLExecuteDataSet(sAC, sSql)
            ElseIf ChkType <> 1 Then
                'If (CheckForGrpMember(iGrpId, iUserId) = True) Then
                'strsql = "select * from edt_cabinet left outer join edt_cabinet_permission on cbn_node=cbp_cabid where CBN_Node=" & iCabId & " and CBN_ParGrp=" & iGrpId & " and CBP_CabId not in ( select distinct(CBP_CabId)  from edt_cabinet_permission where CBP_UsrId=" & iUserId & " or CBP_Grpid in (" & sMem & ") or CBP_PType='E') "
                If (sCabPerm = String.Empty) Then
                    sCabPerm = GetPermCabinets(sAC, iUserId, sMem)
                End If
                sSql = " Select *  from edt_cabinet left outer join edt_cabinet_permission on cbn_id=cbp_cabinet where  CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_id= " & iCabId & " "
                If Val(sCabPerm) <> 0 Then
                    sSql = sSql & " and (CBP_cabinet not in (" & sCabPerm & " ) or CBP_cabinet is Null)  "
                End If
                If objDBL.DBCheckForRecord(sAC, sSql) = True Then
                    dsMain = objDBL.SQLExecuteDataSet(sAC, sSql)
                    sPermlvl = "PG"
                Else
                    GoTo LP
                End If
            Else
LP:             dsMain = BuildPermDataSet(iUserId, iCabId, sMem, ChkType, sAC, iGrpId)
                If dsMain.Tables.Count >= 0 Then                                  'Vijeth 30/01/19
                    Try
                        dtPerm = dsMain.Tables(0)
                        dtPerm = GetFinalPermForDS(sAC, dtPerm)
                        Return dtPerm
                    Catch
                    End Try
                Else
                    Dim MyDt As New DataTable
                    Return MyDt
                End If
            End If
            Return dsMain.Tables(0)

        Catch ex As Exception
            Throw
        End Try
        '            ElseIf ChkType <> 1 Then
        '                If (sCabPerm = String.Empty) Then
        '                    sCabPerm = GetPermCabinets(sAC, iUserId, sMem)
        '                End If
        '                sSql = " Select *  from edt_cabinet left outer join edt_cabinet_permission on CBN_ID=cbp_Cabinet where  CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_ID= " & iCabId & " "
        '                dtMain = objDBL.SQLExecuteDataTable(sAC, sSql)
        '                GoTo LP
        '            End If
        '            Else

        'LP:         dtMain = BuildPermDataSet(sAC, iUserId, iCabId, sMem, ChkType)
        '            If dtMain.Rows.Count > 0 Then
        '                dtPerm = dtMain
        '                dtPerm = GetCabinetFinalPermForDS(dtPerm)
        '                Return dtPerm
        '            Else
        '                Dim MyDt As New DataTable
        '                Return MyDt

        '            End If
        '                Return dtMain
        '        Catch ex As Exception
        '            Throw
        '        End Try
    End Function
    Public Function CheckForGrpHead(ByVal sAC As String, ByVal iGrpId As Int16, ByVal iUsrId As Int16) As Integer
        Dim sSql As String
        Try
            sSql = "Select SUO_IsDeptHead from Sad_UsersInOtherDept where SUO_UserID=" & iUsrId & " and SUO_DeptID=" & iGrpId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetPermDocTypes(ByVal sAC As String, ByVal iUserID As String, ByVal sGrpID As String) As String
        Dim sSql As String = "", sRet As String = "", sCabId As String = ""
        Dim Arr() As String, sFArr() As String
        Dim i As Integer
        Try
            Arr = Split(sGrpID, ",")
            For i = 0 To UBound(Arr)
                sSql = "edt_docType_permission where EDP_GrpId = " & Arr(i) & " and (EDP_UsrId=" & iUserID & " or EDP_UsrId=0)"
                sRet = objDBL.GetAllValues(sAC, "EDP_DocTypeID", sSql)
                If Val(sRet) <> 0 Then
                    If Right(sRet, 1) = ";" Then
                        sRet = Left(sRet, Len(sRet) - 1)
                    End If
                    sCabId = sCabId & ";" & sRet & ";"
                End If
            Next
            sSql = "edt_docType_permission where Edp_ptype = 'E'"
            sCabId = sCabId & objDBL.GetAllValues(sAC, "EDP_DocTypeID", sSql)
            sCabId = Replace(sCabId, ";", ",")

            If Len(Trim(sCabId)) = 0 Then
                sCabId = "0"
            End If

            sFArr = Split(sCabId, ",")
            For i = 0 To UBound(sFArr)
                If Val(sFArr(i)) <> 0 Then
                    GetPermDocTypes = GetPermDocTypes & "," & Val(sFArr(i))
                End If
            Next
            If Left(GetPermDocTypes, 1) = "," Then
                GetPermDocTypes = Right(GetPermDocTypes, Len(GetPermDocTypes) - 1)
            End If
            If Right(GetPermDocTypes, 1) = "," Then
                GetPermDocTypes = Left(GetPermDocTypes, Len(GetPermDocTypes) - 1)
            End If
            Return GetPermDocTypes
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildPermDataSet(ByVal iUserId As Integer, ByVal iCabId As Integer, ByVal sMem As String, ByVal ChkType As Integer, ByVal sAC As String, ByVal iGrpId As Integer) As Object

        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iCount As Integer
        Dim ssql, ssql1, ssql2 As String
        Dim ds, ds1, ds2 As DataSet
        Dim grp As Integer
        Try
            ssql2 = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "'"
            ds2 = objDBL.SQLExecuteDataSet(sAC, ssql2)
            grp = ds2.Tables(0).Rows(0)("SUO_DeptID")

            ssql = "Select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and (cbp_user='" & iUserId & "' or cbp_user=0)"
            ds = objDBL.SQLExecuteDataSet(sAC, ssql)
            If (ds.Tables(0).Rows.Count > 0) Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ((grp <> iGrpId) And (ds.Tables(0).Rows(i)("CBP_PermissionType") <> "U")) Then
                        If ((ds.Tables(0).Rows(i)("CBP_OTHER") = 0) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "G")) = True Then
                            '  ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and (cbp_department='" & iGrpId & "' or cbp_department='" & grp & "') and  cbp_user=0 and CBP_OTHER=0 and CBP_PermissionType='G'"   'Vijeth 31/01/2019
                            ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and (cbp_department='" & grp & "') and  cbp_user=0 and CBP_OTHER=0 and CBP_PermissionType='G'"
                            ds1 = objDBL.SQLExecuteDataSet(sAC, ssql1)
                        End If
                    Else
                        If ((ds.Tables(0).Rows(i)("CBP_OTHER") = 1) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "U")) = True Then
                            ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and cbp_user='" & iUserId & "' and CBP_OTHER=1 and CBP_PermissionType='U'"
                            ds1 = objDBL.SQLExecuteDataSet(sAC, ssql1)
                            Return ds1
                            Exit Function
                        ElseIf ((ds.Tables(0).Rows(i)("CBP_OTHER") = 0) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "G")) = True Then
                            ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and cbp_department in (select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "') and  cbp_user=0 and CBP_OTHER=0 and CBP_PermissionType='G'"
                            ds1 = objDBL.SQLExecuteDataSet(sAC, ssql1)
                        ElseIf ((ds.Tables(0).Rows(i)("CBP_OTHER") = 0) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "U")) = True Then
                            If (ds.Tables(0).Rows.Count <= 1) Then
                                If (ds.Tables(0).Rows(i)("CBP_Department") <> ds.Tables(0).Rows(i)("CBN_Department")) Then
                                    Dim MyDt As New DataSet
                                    Return MyDt
                                    Exit Function
                                Else
                                    ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and cbp_department in (select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "')"
                                    ds1 = objDBL.SQLExecuteDataSet(sAC, ssql1)
                                End If
                            End If
                        End If
                    End If
                Next
            Else
                Dim MyDt As New DataSet
                Return MyDt
                Exit Function
            End If
            Return ds1

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildPermDataSetFol(ByVal sAC As String, ByVal iUserId As Integer, ByVal iFolId As Integer, ByVal sMem As String, ByVal ChkType As Integer) As Object

        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iCount As Integer
        Dim ssql, ssql1, ssql2 As String
        Dim ds, ds1, ds2 As DataSet
        Dim grp As Integer
        Dim MyDt As New DataSet
        Try
            ssql2 = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "'"
            ds2 = objDBL.SQLExecuteDataSet(sAC, ssql2)
            grp = ds2.Tables(0).Rows(0)("SUO_DeptID")

            ssql = "Select * from View_FolPermissions  where Fol_FolID='" & iFolId & "' and (EFP_USRID ='" & iUserId & "' or EFP_USRID =0)"
            ds = objDBL.SQLExecuteDataSet(sAC, ssql)
            If (ds.Tables(0).Rows.Count > 0) Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ((ds.Tables(0).Rows(i)("EFP_OTHER") = 1) And (ds.Tables(0).Rows(i)("EFP_PType") = "U")) = True Then
                        ssql1 = "select * from View_FolPermissions where Fol_FolID='" & iFolId & "' and EFP_USRID='" & iUserId & "' and EFP_OTHER=1 and EFP_PType='U'"
                        ds1 = objDBL.SQLExecuteDataSet(sAC, ssql1)
                        Return ds1
                        Exit Function
                    ElseIf ((ds.Tables(0).Rows(i)("EFP_OTHER") = 0) And (ds.Tables(0).Rows(i)("EFP_PType") = "G")) = True Then
                        ssql1 = "select * from View_FolPermissions where Fol_FolID='" & iFolId & "' and EFP_GrpId in (select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "') and  EFP_USRID=0 and EFP_OTHER=0 and EFP_PType='G'"
                        ds1 = objDBL.SQLExecuteDataSet(sAC, ssql1)
                        If (ds.Tables(0).Rows.Count > 0) Then
                        Else
                            If (ds1.Tables(0).Rows.Count = 0) Then
                                Return MyDt
                            End If
                        End If
                    ElseIf ((ds.Tables(0).Rows(i)("EFP_OTHER") = 0) And (ds.Tables(0).Rows(i)("EFP_PType") = "U")) = True Then
                        If (ds.Tables(0).Rows.Count <= 1) Then
                            If (ds.Tables(0).Rows(i)("EFP_GrpId") <> grp) Then
                                Return MyDt
                                Exit Function
                            Else
                                ssql1 = "select * from View_FolPermissions where Fol_FolID='" & iFolId & "' and EFP_GrpId in (select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "')"
                                ds1 = objDBL.SQLExecuteDataSet(sAC, ssql1)
                            End If
                        End If
                    End If
                Next
            Else
                Return MyDt
                Exit Function
            End If
            Return ds1


            'Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
            'Dim iCount As Integer
            'Try
            '    objParam(iCount) = New OleDb.OleDbParameter("@p_UsrId", OleDb.OleDbType.Numeric)
            '    objParam(iCount).Value = iUserId
            '    objParam(iCount).Direction = ParameterDirection.Input
            '    iCount = iCount + 1

            '    objParam(iCount) = New OleDb.OleDbParameter("@p_CabId", OleDb.OleDbType.Numeric)
            '    objParam(iCount).Value = iCabId
            '    objParam(iCount).Direction = ParameterDirection.Input
            '    iCount = iCount + 1

            '    objParam(iCount) = New OleDb.OleDbParameter("@p_Mem", OleDb.OleDbType.VarChar)
            '    objParam(iCount).Value = sMem
            '    objParam(iCount).Direction = ParameterDirection.Input
            '    iCount = iCount + 1

            '    objParam(iCount) = New OleDb.OleDbParameter("@p_iRetLvl", OleDb.OleDbType.VarChar)
            '    objParam(iCount).Value = 0
            '    objParam(iCount).Direction = ParameterDirection.Output
            '    objParam(iCount).Size = 1
            '    If (ChkType = 2) Then
            '        Dim arr() As Object = objDBL.SPFrLoadingUsingDsParam(sAC, "GetFolPerDetails", 1, "@p_iRetLvl", objParam)
            '        If IsDBNull(arr(1)) = False Then
            '            sPermlvl = arr(1)
            '        Else
            '            sPermlvl = ""
            '        End If
            '        Return arr(0)
            '    Else
            '        Return (objDBL.SPFrLoadingUsingDs(sAC, "GetFolPerDetails", objParam))
            '    End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetCabinetFinalPermForDS(ByVal dtCab As DataTable) As DataTable
        Dim dr As DataRow
        Dim iCSC As Byte, iVCB As Byte, iDCB As Byte, iMCB As Byte, iIND As Byte, iSRH As Byte, iCFD As Byte
        Try
            For Each dr In dtCab.Rows
                If (UCase(sPermlvl) <> "GH" And UCase(sPermlvl) <> "PG") Then
                    If (dr("CBP_Create") = 1) Then
                        iCSC = 1
                    End If
                    If (dr("CBP_Modify") = 1) Then
                        iMCB = 1
                    End If
                    If (dr("CBP_Delete") = 1) Then
                        iDCB = 1
                    End If
                    If (dr("CBP_Create_Folder") = 1) Then
                        iCFD = 1
                    End If
                    If (dr("CBP_Search") = 1) Then
                        iSRH = 1
                    End If
                    If (dr("CBP_Index") = 1) Then
                        iIND = 1
                    End If
                    If (dr("CBP_View") = 1) Then
                        iVCB = 1
                    End If
                End If
            Next
            dtCab.BeginInit()
            dtCab.Rows(0).Item("CBP_Create") = iCSC
            dtCab.Rows(0).Item("CBP_Modify") = iMCB
            dtCab.Rows(0).Item("CBP_View") = iVCB
            dtCab.Rows(0).Item("CBP_Delete") = iDCB
            dtCab.Rows(0).Item("CBP_Create_Folder") = iCFD
            dtCab.Rows(0).Item("CBP_Index") = iIND
            dtCab.Rows(0).Item("CBP_Search") = iSRH
            dtCab.EndInit()
            dtCab.AcceptChanges()
            Return dtCab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetFolderFinalPermForDS(ByVal sAC As String, ByVal dtCab As DataTable) As DataTable
        Dim dr As DataRow
        Dim iMFD As Byte, iDFD As Byte, iVFD As Byte, iDDM As Byte, iMDM As Byte, iCDM As Byte, iIDX As Byte, iSRH As Byte, iEXP As Byte
        Try
            For Each dr In dtCab.Rows
                If (UCase(sPermlvl) <> "GH" And UCase(sPermlvl) <> "PG") Then
                    If (dr("EFP_MOD_FOLDER") = 1) Then
                        iMFD = 1
                    End If
                    If (dr("EFP_DEL_FOLDER") = 1) Then
                        iDFD = 1
                    End If
                    If (dr("EFP_VIEW_Fol") = 1) Then
                        iVFD = 1
                    End If

                    If (dr("EFP_MOD_DOC") = 1) Then
                        iMDM = 1
                    End If
                    If (dr("EFP_DEL_DOC") = 1) Then
                        iDDM = 1
                    End If
                    If (dr("EFP_CRT_DOC") = 1) Then
                        iCDM = 1
                    End If

                    If (dr("EFP_SEARCH") = 1) Then
                        iSRH = 1
                    End If
                    If (dr("EFP_INDEX") = 1) Then
                        iIDX = 1
                    End If
                    If (dr("EFP_EXPORT") = 1) Then
                        iEXP = 1
                    End If
                End If
            Next
            dtCab.BeginInit()

            dtCab.Rows(0).Item("EFP_INDEX") = iIDX
            dtCab.Rows(0).Item("EFP_SEARCH") = iSRH
            dtCab.Rows(0).Item("EFP_MOD_FOLDER") = iMFD
            dtCab.Rows(0).Item("EFP_MOD_DOC") = iMDM
            dtCab.Rows(0).Item("EFP_DEL_FOLDER") = iDFD
            dtCab.Rows(0).Item("EFP_DEL_DOC") = iDDM
            dtCab.Rows(0).Item("EFP_EXPORT") = iEXP
            dtCab.Rows(0).Item("EFP_CRT_DOC") = iCDM
            dtCab.Rows(0).Item("EFP_VIEW_Fol") = iVFD

            dtCab.EndInit()
            dtCab.AcceptChanges()
            Return dtCab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetFinalPermForDS(ByVal sNameSpace As String, ByVal dtCab As DataTable) As DataTable
        Dim dr As DataRow
        Dim iCDC As Byte, iDDC As Byte, iMDC As Byte, iIND As Byte, iSRH As Byte, iMDT As Byte
        Try
            For Each dr In dtCab.Rows
                If (UCase(sPermlvl) <> "GH" And UCase(sPermlvl) <> "PG") Then
                    If (dr("EDP_INDEX") = 1) Then
                        iIND = 1
                    End If
                    If (dr("EDP_SEARCH") = 1) Then
                        iSRH = 1
                    End If
                    If (dr("EDP_MFY_TYPE") = 1) Then
                        iMDT = 1
                    End If
                    If (dr("EDP_MFY_DOCUMENT") = 1) Then
                        iMDC = 1
                    End If
                    If (dr("EDP_DEL_DOCUMENT") = 1) Then
                        iDDC = 1
                    End If
                    If (dr("EDP_OTHER") = 1) Then
                        iCDC = 1
                    End If
                End If
            Next
            dtCab.BeginInit()
            dtCab.Rows(0).Item("EDP_INDEX") = iIND
            dtCab.Rows(0).Item("EDP_SEARCH") = iSRH
            dtCab.Rows(0).Item("EDP_MFY_TYPE") = iMDT
            dtCab.Rows(0).Item("EDP_MFY_DOCUMENT") = iMDC
            dtCab.Rows(0).Item("EDP_DEL_DOCUMENT") = iDDC
            dtCab.Rows(0).Item("EDP_OTHER") = iCDC
            dtCab.EndInit()
            dtCab.AcceptChanges()
            Return dtCab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function AddPermissions(ByVal sAC As String, ByVal dtPerm As DataTable, ByVal PLevel As String, ByVal iCompID As Integer)
        Dim dsRow As DataRow, dtRow As DataRow
        Try
            dsRow = dtPerm.Rows(0)
            dtRow = Permdt.NewRow
            dtRow("PLevel") = PLevel
            dtRow("Id") = dsRow("CBN_ID")
            dtRow("Name") = dsRow("CBN_Name")
            dtRow("CabPar") = dsRow("CBN_Parent")
            dtRow("CabNote") = dsRow("CBN_Note")
            dtRow("CabCrtUsr") = dsRow("CBN_UserID")
            dtRow("CabCrtGrp") = dsRow("CBN_UserID") 'CBN_USERGROUP
            dtRow("CabParGrp") = GetGroupName(sAC, dsRow("CBN_Department"), iCompID) 'CBN_Department
            dtRow("CabCrOn") = dsRow("CBN_CreatedOn")
            dtRow("SubCabNo") = dsRow("CBN_SubCabCount")
            dtRow("FolNo") = dsRow("CBN_FolderCount")
            Permdt.Rows.Add(dtRow)
            dtMain.Clear()
            dtMain.Dispose()
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetGroupName(ByVal sAC As String, ByVal GrpId As Int16, ByVal iCompID As Integer) As String
        Dim sSql As String
        Try
            'sSql = "Select Mas_description from sad_grporlvl_general_master where Mas_Id=" & GrpId & ""
            sSql = "Select Org_Name from sad_org_structure where Org_Node=" & GrpId & " And org_levelcode=3 And Org_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetPermCabinets(ByVal sAC As String, ByVal iUserID As String, ByVal sGrpID As String) As String
        Dim sSql As String, sCabId As String = "", sRet As String = ""
        Dim Arr() As String, sFArr() As String
        Dim i As Integer
        Try
            Arr = Split(sGrpID, ",")
            For i = 0 To UBound(Arr)
                sSql = "edt_cabinet_permission where CBP_Department = " & Arr(i) & " And (CBP_User=" & iUserID & " Or CBP_User=0)"
                sRet = objDBL.GetAllValues(sAC, "CBP_Cabinet", sSql)
                If Len(sRet) <> 0 Then
                    If Right(sRet, 1) = ";" Then
                        sRet = Left(sRet, Len(sRet) - 1)
                    End If
                    sCabId = sCabId & ";" & sRet & ";"
                End If
            Next
            sSql = "Edt_cabinet_permission where cbp_permissiontype = 'E'"
            sCabId = sCabId & objDBL.GetAllValues(sAC, "CBP_Cabinet", sSql)
            sCabId = Replace(sCabId, ";", ",")

            If Len(Trim(sCabId)) = 0 Then
                sCabId = "0"
            End If

            sFArr = Split(sCabId, ",")
            For i = 0 To UBound(sFArr)
                If Val(sFArr(i)) <> 0 Then
                    GetPermCabinets = GetPermCabinets & "," & Val(sFArr(i))
                End If
            Next
            If Left(GetPermCabinets, 1) = "," Then
                GetPermCabinets = Right(GetPermCabinets, Len(GetPermCabinets) - 1)
            End If
            If Right(GetPermCabinets, 1) = "," Then
                GetPermCabinets = Left(GetPermCabinets, Len(GetPermCabinets) - 1)
            End If
            Return GetPermCabinets
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Sub Cabinet
    Public Function LoadSubCabinets(ByVal sAC As String, ByVal LogUsrId As Int16, ByVal iCompID As Integer, Optional ByVal sPerm As String = "VCB") As DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Dim iRet As Integer
        Dim dtSubCab As New DataTable
        Try
            Permdt = BuildPermTable()
            sMem = GetMemberGroups(sAC, LogUsrId)
            iUsrType = GetUserType(sAC, LogUsrId)
            iUsrParGrp = GetUserParGrp(sAC, LogUsrId)
            If (iUsrType = 1) Then
                sSql = "" : sSql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent <> -1 order by CBN_Name"
                dtSubCab = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtSubCab.Rows.Count > 0 Then
                    For Each dRow In dtSubCab.Rows
                        iRet = GetFinalPermissions(sAC, dRow("CBN_ID"), LogUsrId, sPerm)
                        If (iRet = 1) Then
                            AddPermissions(sAC, dtPerm, sPermlvl, iCompID)
                        End If
                    Next
                End If
                Return Permdt
                Exit Function
            End If

            sCabPerm = GetPermCabinets(sAC, LogUsrId, sMem)
            sSql = "" : sSql = " Select * from edt_cabinet where CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_Parent <> -1 "
            If Val(sCabPerm) <> 0 Then
                sSql = sSql & " and CBN_ID Not in (" & sCabPerm & ")"
            End If
            sSql = sSql & " order by CBN_Name"
            dtSubCab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtSubCab.Rows.Count > 0 Then
                For Each dRow In dtSubCab.Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = GetFinalPermissions(sAC, dRow("CBN_ID"), LogUsrId, sPerm)
                    If (iRet = 1) Then
                        AddPermissions(sAC, dtPerm, sPermlvl, iCompID)
                    End If
                Next
            End If
            sSql = "" : sSql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent <> -1 "
            If Len((sCabPerm)) <> 0 Then
                sSql = sSql & " and CBN_ID in (" & sCabPerm & ")"
            Else
                sSql = sSql & " and CBN_ID in (0)"
            End If
            sSql = sSql & " order by CBN_Name"
            dtSubCab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtSubCab.Rows.Count > 0 Then
                For Each dRow In dtSubCab.Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = GetFinalPermissions(sAC, dRow("CBN_ID"), LogUsrId, sPerm, 1)
                    If (iRet = 1) Then
                        AddPermissions(sAC, dtPerm, sPermlvl, iCompID)
                    End If
                Next
            End If
            Return Permdt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabOrSC(ByVal sAC As String, ByVal sLevel As String, ByVal iIndexID As Integer, ByVal iUserID As Integer, ByVal dtCab As DataTable, ByVal dtSubCab As DataTable, ByVal dtFol As DataTable) As String
        Dim iRet As Integer, iRetVal As Integer, iCabId As Integer
        Dim sStr As String = ""
        Try
            Select Case sLevel
                Case "SC"
                    iRetVal = GetTableContents(dtSubCab, "ID", "CabPar", iIndexID)
                    If (iRetVal = String.Empty) Then
                        Return 0
                    End If
                    iRet = CheckForPermissions(sAC, iRetVal, iUserID, "SRH")
                    If (iRet = 1) Then
                        Return iRetVal
                    Else
                        Return 10
                    End If
                Case "FD"
                    iCabId = GetTableContents(dtFol, "ID", "FolCabId", iIndexID)
                    iRetVal = CheckCabOrSC(iCabId, dtCab, dtSubCab, "ID")
                    If (iRetVal = "SC") Then
                        iRet = CheckForPermissions(sAC, iCabId, iUserID, "SRH")
                        If (iRet = 1) Then
                            sStr = iCabId
                            iRetVal = GetTableContents(dtSubCab, "ID", "CabPar", iCabId)
                            iRet = CheckForPermissions(sAC, iRetVal, iUserID, "SRH")
                            If (iRet = 1) Then
                                sStr = sStr & "|" & iRetVal
                            Else
                                Return "NPC"
                            End If

                        Else
                            Return "NPSC"
                        End If
                    ElseIf (iRetVal = "CB") Then
                        iRetVal = CheckForPermissions(sAC, iCabId, iUserID, "SRH")
                        If (iRetVal = 1) Then
                            sStr = iRetVal.ToString
                        Else
                            Return "NPC"
                        End If
                    End If
            End Select
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTableContents(ByVal RefDt As DataTable, ByVal sSourceCol As String, ByVal sDestCol As String, ByVal sSourceVal As String) As String
        Dim dRow As DataRow
        Try
            For Each dRow In RefDt.Rows
                If (IsDBNull(dRow(sSourceCol)) = False) Then
                    If (dRow(sSourceCol) = sSourceVal) Then
                        Return dRow(sDestCol)
                    End If
                End If
            Next
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForPermissions(ByVal sAC As String, ByVal iId As Integer, ByVal iUserID As Integer, ByVal sPERM As String) As Object
        Try
            Return (GetFinalPermissions(sAC, iId, iUserID, "SRH", 2))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckCabOrSC(ByVal iCabID As Integer, ByVal dtCab As DataTable, ByVal dtSC As DataTable, ByVal sColName As String) As String
        Dim dr As DataRow
        Try
            For Each dr In dtCab.Rows
                If (IsDBNull(dr(sColName)) = False) Then
                    If (dr(sColName) = iCabID) Then
                        Return "CB"
                    End If
                End If
            Next
            For Each dr In dtSC.Rows
                If (IsDBNull(dr(sColName)) = False) Then
                    If (dr(sColName) = iCabID) Then
                        Return "SC"
                    End If
                End If
            Next
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Folder
    Public Function LoadFolForSrh(ByVal sAC As String, ByVal LogUsrId As Int16, ByVal iCompID As Integer, Optional ByVal sPerm As String = "VFD") As DataTable
        Dim dtFol As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Dim iRet As Integer
        Dim Grpdv As DataView
        Try
            dtGrp = BuildGroupDt(sAC, iCompID)
            Grpdv = dtGrp.DefaultView
            Permdt = BuildFolderTable()
            sMem = GetMemberGroups(sAC, LogUsrId)
            sFolPerm = GetPermFolders(sAC, LogUsrId, sMem)
            iUsrType = GetUserType(sAC, LogUsrId)
            iUsrParGrp = GetUserParGrp(sAC, LogUsrId)
            If (iUsrType = 1) Then
                sSql = "Select FOL_FolID,CBN_Department,CBN_Name from EDT_FOLDER Left Join EDT_Cabinet On CBN_ID=FOL_Cabinet where Fol_DelFlag='A' And FOL_CompID=" & iCompID & " And CBN_CompID=" & iCompID & ""
                dtFol = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtFol.Rows.Count > 0 Then
                    For Each dRow In dtFol.Rows
                        iParGrp = dRow("CBN_Department")
                        sCabName = dRow("CBN_Name")

                        iRet = GetFinalFolPermissions(sAC, dRow("Fol_FolId"), LogUsrId, iCompID, sPerm)
                        If (iRet = 1) Then
                            AddFolderPermissions(dtPerm, sPermlvl, Grpdv)
                        End If
                    Next
                End If
                Return Permdt
                Exit Function
            End If
            sSql = "Select FOL_FolID,CBN_Department,CBN_Name from EDT_FOLDER Left Join EDT_Cabinet On CBN_ID=FOL_Cabinet where Fol_DelFlag='A' And CBN_Department in (" & sMem & ") And FOL_CompID=" & iCompID & " And CBN_CompID=" & iCompID & ""
            If Val(sFolPerm) <> 0 Then
                sSql = sSql & " and Fol_FolId Not in (" & sFolPerm & ")"
            End If
            dtFol = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtFol.Rows.Count > 0 Then
                For Each dRow In dtFol.Rows
                    iParGrp = dRow("CBN_Department")
                    sCabName = dRow("CBN_Name")
                    iRet = GetFinalFolPermissions(sAC, dRow("Fol_FolId"), LogUsrId, iCompID, sPerm)
                    If (iRet = 1) Then
                        AddFolderPermissions(dtPerm, sPermlvl, Grpdv)
                    End If
                Next
            End If
            sSql = "Select FOL_FolID,CBN_Department,CBN_Name from EDT_FOLDER Left Join EDT_Cabinet On CBN_ID=FOL_Cabinet where Fol_DelFlag='A' And FOL_CompID=" & iCompID & " And CBN_CompID=" & iCompID & ""

            If Len((sFolPerm)) <> 0 Then
                sSql = sSql & " and Fol_FolId in (" & sFolPerm & ")"
            Else
                sSql = sSql & " and Fol_FolId in (0)"
            End If

            dtFol = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtFol.Rows.Count > 0 Then
                For Each dRow In dtFol.Rows
                    iParGrp = dRow("CBN_Department")
                    sCabName = dRow("CBN_Name")
                    iRet = GetFinalFolPermissions(sAC, dRow("Fol_FolID"), LogUsrId, iCompID, sPerm, 1)
                    If (iRet = 1) Then
                        AddFolderPermissions(dtPerm, sPermlvl, Grpdv)
                    End If
                Next
            End If
            Return Permdt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildGroupDt(ByVal sAC As String, ByVal iCompID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String
        Try
            'sSql = "select Mas_Id,Mas_Description  from sad_grporlvl_general_master where Mas_DelFlag= 'A' "
            sSql = "Select Org_Node,Org_Name from sad_org_structure where Org_DelFlag='A' And org_levelcode=3 And Org_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildFolderTable() As DataTable
        Dim LocalDt As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Name", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolNote", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCab", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolSubCab", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCrOn", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCrBy", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolGroup", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCabId", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("PageCount", GetType(Integer))
            LocalDt.Columns.Add(dc)

            'Permissions
            dc = New DataColumn("PLevel", GetType(String))
            LocalDt.Columns.Add(dc)
            Return LocalDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AddFolderPermissions(ByVal dtperm As DataTable, ByVal PLevel As String, ByVal Grpdv As DataView)
        Dim dsRow As DataRow, dtRow As DataRow
        Try
            'Permdt = BuildFolderTable() \\ vijeth
            dsRow = dtperm.Rows(0)
            dtRow = Permdt.NewRow
            dtRow("PLevel") = PLevel
            dtRow("ID") = dsRow("Fol_FOlID")
            dtRow("Name") = dsRow("Fol_Name")
            dtRow("FolNote") = dsRow("Fol_Note")
            dtRow("FolCrOn") = dsRow("FOL_CreatedOn")
            dtRow("FolCrBy") = dsRow("FOL_CreatedBy")
            dtRow("FolCabId") = dsRow("Fol_Cabinet")
            dtRow("PageCount") = dsRow("Fol_Cabinet") ' dsRow("Fol_PageCount")
            dtRow("FolCab") = sCabName
            Grpdv.RowFilter = "Org_Node = " & iParGrp
            If Grpdv.Count > 0 Then
                dtRow("FolGroup") = Grpdv.Item(0).Item(1)
            End If
            Permdt.Rows.Add(dtRow)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Document Type
    Public Function LoadIndexedFolders(ByVal sAC As String, ByVal iCompID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(pge_folder) from edt_page where pge_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                sSql = String.Empty
                For i = 0 To dt.Rows.Count - 1
                    If IsDBNull(dt.Rows(i).Item("pge_folder")) = False Then
                        sSql = sSql & "," & dt.Rows(i).Item("pge_folder")
                    End If
                Next
                Return sSql
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDocTypes(ByVal sAC As String, ByVal iUserId As Integer, ByVal sPerm As String, ByVal sFolID As String) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtDocType As New DataTable
        Try
            dtDocType.Columns.Add("Id")
            dtDocType.Columns.Add("Name")
            dtDocType.Columns.Add("Dot_Pgroup")
            dtDocType.Columns.Add("dot_Crby")
            dtDocType.Columns.Add("dot_Note")
            dtDocType.Columns.Add("dot_CrOn")
            dtDocType.Columns.Add("DtGrpName")

            sSql = "Select a.*,Org_Name From edt_document_type a,sad_org_structure Where a.Dot_Delflag='A' And Org_Node=a.dot_pgroup And a.Dot_DocTypeID"
            sSql = sSql & " in (" & " Select distinct(pge_Document_type) From edt_page Where pge_folder in (" & sFolID & "))"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For Each dRow In dt.Rows
                    If IsDBNull(dt.Rows(0)("Dot_DocTypeID")) = False Then
                        If (GetFinalDTPermissions(sAC, dt.Rows(0)("Dot_DocTypeID"), iUserId, sPerm) = 1) Then
                            dtDocType = AddDocTypes(dtDocType, dRow)
                        End If
                    End If
                Next
            End If
            Return dtDocType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function AddDocTypes(ByVal dt As DataTable, ByVal dRow As DataRow) As DataTable
        Dim dr As DataRow
        Try
            dr = dt.NewRow
            dr("Id") = dRow("Dot_DocTypeID")
            dr("Name") = dRow("Dot_DocName")
            dr("dot_Crby") = dRow("dot_Crby")
            dr("Dot_Pgroup") = dRow("Dot_Pgroup")
            dr("dot_Note") = dRow("dot_Note")
            dr("dot_CrOn") = dRow("dot_CrOn")
            dr("DtGrpName") = dRow("Org_Name")
            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Keywords
    Public Function LoadKeyWords(ByVal sAC As String) As DataTable
        Dim sSql As String = "", sDisKw As String = ""
        Dim dtKey As New DataTable, dt As New DataTable
        Dim sArr() As String
        Dim i As Integer
        Dim dc As DataColumn
        Dim dr As DataRow
        Try
            sSql = "Select distinct(pge_keyword) from edt_page where pge_details_Id in (select distinct(pge_details_Id) from edt_page)"
            dtKey = objDBL.SQLExecuteDataTable(sAC, sSql)
            sDisKw = BuildDistinctKWs(dtKey)
            dt = New DataTable
            dc = New DataColumn("Name", GetType(String))
            dt.Columns.Add(dc)
            sArr = Split(sDisKw, ";")
            For i = 0 To UBound(sArr)
                If Convert.ToString(sArr(i)).Length <> 0 Then
                    dr = dt.NewRow
                    dr("Name") = sArr(i)
                    dt.Rows.Add(dr)
                End If
            Next
            dc = New DataColumn("ID", GetType(Int16))
            dt.Columns.Add(dc)
            dt = CreateIndexForTable("ID", dt)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildDistinctKWs(ByVal dtKw As DataTable) As String
        Dim sArr() As String
        Dim i As Integer
        Dim sStr As String = ""
        Try
            If (dtKw.Rows.Count > 0) Then
                For i = 0 To dtKw.Rows.Count - 1
                    sStr = sStr & ";" & dtKw.Rows(i).Item("pge_keyword").ToString
                Next
                sArr = Split(sStr, ";")
                sStr = String.Empty
                For i = 0 To UBound(sArr)
                    If (CheckForElmt(sArr(i), sStr) = False) Then
                        sStr = sStr & ";" & sArr(i)
                    End If
                Next
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CreateIndexForTable(ByVal sColName As String, ByVal dt As DataTable) As DataTable
        Dim i As Integer
        Dim dr As DataRow
        Try
            For i = 0 To dt.Rows.Count - 1
                dr = dt.Rows(i)
                dr.BeginEdit()
                dr(sColName) = i + 1
                dr.EndEdit()
                dr.AcceptChanges()
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForElmt(ByVal sKW As String, ByVal sStr As String) As Boolean
        Try
            If (InStr(sStr & ";", ";" & sKW & ";") > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Format
    Public Function LoadFormat(ByVal sAC As String) As DataTable
        Dim sSql As String
        Dim dtFormat As New DataTable
        Dim dc As DataColumn
        Dim dt As DataTable
        Try
            sSql = "Select distinct(PGE_EXT) as Name from edt_page where pge_status='A'"
            dtFormat = objDBL.SQLExecuteDataTable(sAC, sSql)
            dt = dtFormat
            dc = New DataColumn("ID", GetType(Int16))
            dt.Columns.Add(dc)
            dt = CreateIndexForTable("ID", dtFormat)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Users
    Public Function LoadUsers(ByVal sAC As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select usr_id as id,usr_fullname as Name from sad_userdetails where usr_id in (select distinct(Pge_CreatedBy) from edt_page)"
            Return objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'btnSearch
    Public Function SearchDocuments(ByVal sAC As String, ByVal iCompID As Integer, ByVal iUserId As Integer, ByVal sCab As String, ByVal sSubCab As String, ByVal sFol As String, ByVal sDocType As String, ByVal sKW As String, ByVal sDesc As String, ByVal sFDate As String, ByVal sTDate As String, ByVal sOCRText As String, ByVal sAnyDesc As String, ByVal sFrmt As String, Optional ByVal sDetailID As String = "", Optional ByVal sCrBY As String = "", Optional ByVal cComOrIndDesc As Char = "I", Optional ByVal sTitle As String = "") As DataTable
        Dim dtFdoc As DataTable, dtDoc As DataTable, dt As DataTable
        Dim strsql As String, sSql As String, sBaseID = "", sDetID = ""
        Dim sArr() As String
        Dim i As Integer, iRet As Integer = 0
        Dim dfDate As DateTime, dtDate As DateTime
        Try
            If (Len(sDetailID) <> 0) Then
                dtFdoc = BuildDocTable()
                sSql = "Select distinct(PGE_Details_ID) from edt_page where PGE_Details_ID in (" & sDetailID & ") and PGE_Status='A'"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sSql = "Select * from edt_page where PGE_Details_ID in (" & dt.Rows(i).Item("PGE_Details_ID") & ") and PGE_Status='A'"
                        dtDoc = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dtDoc.Rows.Count > 0 Then
                            dtFdoc = BuildFinalDocTable(sAC, dtDoc.Rows(0), dtFdoc, iUserId, iCompID)
                        End If
                    Next
                End If
                Return dtFdoc
            Else
                strsql = " Select distinct(PGE_Details_ID) from edt_page where PGE_Status='A' "
                If (sCab.Length > 0) Then
                    strsql = strsql & " and PGE_Cabinet in (" & sCab & ")"
                End If
                If (sSubCab.Length > 0) Then
                    strsql = strsql & " and PGE_SubCabinet in (" & sSubCab & ")"
                End If
                If (sFol.Length > 0) Then
                    strsql = strsql & " and PGE_Folder in (" & sFol & ")"
                End If
                If (sKW.Length > 0) Then
                    strsql = strsql & " and ( PGE_KeyWord like " & sKW & ")"
                End If
                If (sOCRText.Length > 0) Then
                    strsql = strsql & " and PGE_OCRText like  " & sOCRText & " "
                End If
                If (sDocType.Length > 0) Then
                    strsql = strsql & " and PGE_Document_Type in (" & sDocType & ")"
                End If
                If (sFrmt.Length > 0) Then
                    strsql = strsql & " and PGE_Ext in (" & sFrmt & ")"
                End If
                If (sTitle.Length > 0) Then
                    strsql = strsql & " and ( pge_Title like  '%" & sTitle & "%' or pge_Title like '" & sTitle & "%'  or pge_Title like '%" & sTitle & "' or pge_Title='" & sTitle & "')"
                End If
                If (sFDate.Length > 0) Then
                    dfDate = CDate(clsGeneralFunctions.FormatMyDate(sFDate))
                    sFDate = objclsEDICTGeneral.FormatDtForRDBMS(dfDate, "I")
                    If (sTDate.Length = 0) Then
                        dtDate = dfDate.AddDays(1)
                    Else
                        dtDate = CDate(clsGeneralFunctions.FormatMyDate(sTDate))
                        dtDate = dtDate.AddDays(1)
                    End If
                    sTDate = objclsEDICTGeneral.FormatDtForRDBMS(dtDate, "I")
                    strsql = strsql & " and Pge_CreatedOn >= " & sFDate & " and  Pge_CreatedOn <= " & sTDate & ""
                End If
                If (sDesc.Length > 0) Then
                    sDetID = GetPageDetID(sAC, sDocType, sDesc)
                    sArr = Split(sDetID, ",")
                    sDetID = String.Empty
                    For i = 0 To UBound(sArr)
                        If (sArr(i).Length > 0) Then
                            sDetID = sDetID & "," & sArr(i)
                        End If
                    Next
                    If (sDetID.Length > 0) Then
                        If (sDetID.Chars(0).ToString = ",") Then
                            sDetID = sDetID.Remove(0, 1)
                            strsql = strsql & " and PGE_Details_ID in (" & sDetID & ")"
                        End If
                    Else
                        strsql = strsql & " and PGE_Details_ID in (0)"
                    End If
                End If
                If (sAnyDesc.Length > 0) Then
                    If (cComOrIndDesc = "C") Then
                        sSql = "Select distinct(epd_baseid) from edt_page_details where epd_baseid in "
                        sArr = Split(sAnyDesc, ",")
                        For i = 0 To UBound(sArr)
                            sArr(i) = Trim(sArr(i))
                            If (i > 0) Then
                                sSql = sSql & " and epd_baseid in "
                            End If
                            sSql = sSql & "(select distinct(epd_baseid) from edt_page_Details where EPD_Value like"
                            sSql = sSql & "'%" & sArr(i) & "%' or EPD_Value like '" & sArr(i) & "%'  or EPD_Value like '%" & sArr(i) & "' or EPD_Value='" & sArr(i) & "')"
                        Next
                        strsql = strsql & " and PGE_Details_ID in (" & sSql & ")"
                    ElseIf (cComOrIndDesc = "I") Then
                        sSql = "Select distinct(epd_baseid) from edt_page_details where EPD_Value Like "
                        sArr = Split(sAnyDesc, ",")
                        For i = 0 To UBound(sArr)
                            sArr(i) = Trim(sArr(i))
                            If (i > 0) Then
                                sSql = sSql & " or EPD_Value like "
                            End If
                            sSql = sSql & "'%" & sArr(i) & "%' or EPD_Value like '" & sArr(i) & "%'  or EPD_Value like '%" & sArr(i) & "' or EPD_Value='" & sArr(i) & "'"
                        Next
                        strsql = strsql & " and PGE_Details_ID in (" & sSql & ")"
                    End If
                End If

                dt = objDBL.SQLExecuteDataTable(sAC, strsql)
                If dt.Rows.Count > 0 Then
                    dtFdoc = BuildDocTable()
                    For i = 0 To dt.Rows.Count - 1
                        sSql = " Select * from edt_page where PGE_Details_ID= " & dt.Rows(i).Item("PGE_Details_ID") & " and PGE_Status='A'"
                        If (sCrBY.Length > 0) Then
                            sSql = sSql & " and  Pge_CreatedBy in (" & sCrBY & ")"
                        End If
                        sSql = sSql & " Order by Pge_BaseName"
                        dtDoc = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dtDoc.Rows.Count > 0 Then
                            dtFdoc = BuildFinalDocTable(sAC, dtDoc.Rows(0), dtFdoc, iUserId, iCompID)
                        End If
                    Next
                    If dt.Rows.Count > 0 Then
                        Return dtFdoc
                    Else
                        dtFdoc = New DataTable
                        Return dtFdoc
                    End If
                Else
                    dtFdoc = New DataTable
                    Return dtFdoc
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildDocTable() As DataTable
        Dim dtDoc As DataTable
        Dim dc As DataColumn
        Try
            dtDoc = New DataTable
            dc = New DataColumn("BaseName", GetType(Integer))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("CabName", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("SubCabName", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("FolName", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("DocType", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("Title", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("IndexDate", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("CrBy", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("CrOn", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("PgeSize", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("Desc", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("DetailsId", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("FolID", GetType(Integer))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("DocTypeID", GetType(Integer))
            dtDoc.Columns.Add(dc)
            Return dtDoc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildFinalDocTable(ByVal sAC As String, ByVal drDoc As DataRow, ByVal dtFDoc As DataTable, ByVal iUserID As Integer, ByVal iCompID As Integer) As DataTable
        Dim sDoc As String, sDocDet() As String
        Dim drFDoc As DataRow
        Dim iRet As Integer
        Try
            iRet = CheckPermissions(sAC, drDoc("PGE_Folder"), drDoc("PGE_Document_Type"), iUserID, iCompID)
            If (iRet <> 0) Then
                drFDoc = dtFDoc.NewRow
                drFDoc("BaseName") = drDoc("PGE_BaseName")
                drFDoc("Title") = drDoc("PGE_Title")
                drFDoc("CrOn") = objclsEDICTGeneral.FormatDtForRDBMS(drDoc("PGE_Date"), "F")
                drFDoc("PgeSize") = drDoc("PGE_Size")
                drFDoc("DetailsId") = drDoc("PGE_Details_Id")
                sDoc = RetPageDetails(sAC, drDoc("PGE_BaseName"))
                sDocDet = Split(sDoc, "|")
                drFDoc("CabName") = sDocDet(0)
                drFDoc("SubCabName") = sDocDet(1)
                drFDoc("FolName") = sDocDet(2)
                drFDoc("DocType") = sDocDet(3)
                drFDoc("FolID") = sDocDet(4)
                drFDoc("DocTypeID") = sDocDet(5)
                dtFDoc.Rows.Add(drFDoc)
            End If
            Return dtFDoc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function RetPageDetails(ByVal sAC As String, ByVal PgeBaseNme As Integer) As String
        Dim objParam() As OleDb.OleDbParameter
        Dim iCount As Integer
        Dim cmd As OleDb.OleDbCommand
        Try
            objParam = New OleDb.OleDbParameter(6) {}
            objParam(iCount) = New OleDb.OleDbParameter("@PGE_BaseName", OleDb.OleDbType.Integer)
            objParam(iCount).Value = PgeBaseNme
            objParam(iCount).Direction = ParameterDirection.Input
            objParam(iCount).Size = 20
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_Cabinet", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_SubCabinet", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_Folder", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_Document_Type", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_FolID", OleDb.OleDbType.Integer)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_DocTypeID", OleDb.OleDbType.Integer)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            cmd = objDBL.SpFrInsertionUsingCmd(sAC, "GetPageDetails", objParam)
            Return cmd.Parameters("@PGE_Cabinet").Value & "|" & cmd.Parameters("@PGE_SubCabinet").Value &
                      "|" & cmd.Parameters("@PGE_Folder").Value & "|" & cmd.Parameters("@PGE_document_type").Value &
                "|" & cmd.Parameters("@PGE_FolID").Value & "|" & cmd.Parameters("@PGE_DocTypeID").Value
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPageDetID(ByVal sAC As String, ByVal sDocType As String, ByVal sDesc As String) As String
        Dim sSql As String = "", strsql As String = "", sDetID = ""
        Dim dt As New DataTable
        Dim aDoc() As String
        Dim i As Integer, j As Integer, k As Integer
        Dim descArr(), sArr() As String
        Dim htDt As Hashtable
        Dim bRet As Boolean
        Try
            If (sDocType.Length <= 0) Then
                descArr = Split(sDesc, "$")
                For i = 0 To UBound(descArr)
                    sArr = Split(descArr(i), ",")
                    If (sArr.Length > 1) Then
                        sArr(1) = Trim(sArr(1))
                        If (i > 1) Then
                            sSql = sSql & " and EPD_BaseID in "
                        End If
                        sSql = sSql & " ( Select distinct(epd_baseid) from edt_page_details where  "
                        sSql = sSql & " EPD_DescID=" & sArr(0) & " and  (EPD_Value Like  " &
                               "'% " & sArr(1) & " %' or EPD_Value like '" & sArr(1) & " %'  or EPD_Value like '% " & sArr(1) & "' or EPD_Value='" & sArr(1) & "')) "
                    End If
                Next
                sSql = "" : sSql = "Select distinct(epd_baseID) from edt_page_details where EPD_BaseID in  " & sSql
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sDetID = sDetID & "," & dt.Rows(i).Item("EPD_BaseID")
                    Next
                    Return sDetID
                Else
                    Return ""
                End If
            Else
                Dim iDesc As Integer
                Dim bFoundOrNot As Boolean 'To check value sin Each Descriptor - #11
                Dim sLocalVal As String
                htDt = GetDescriptor(sAC, sDocType)
                If (sDesc.Length > 0) Then
                    aDoc = Split(sDocType, ",")
                    descArr = Split(sDesc, "$")
                    For j = 0 To UBound(aDoc)  'For each Document types
                        sSql = String.Empty
                        iDesc = 0
                        For i = 0 To UBound(descArr) 'For Each descriptor in DocType
                            If (descArr(i).Length > 0) Then
                                bFoundOrNot = False '#11
                                sArr = Split(descArr(i), ",")
                                bRet = CheckDescForDT(htDt, aDoc(j), sArr(0))
                                If (bRet = True) Then
                                    sArr(1) = Trim(sArr(1))
                                    strsql = "  EPD_DescID=" & sArr(0) & " and  (EPD_Value Like  " &
                                              "'% " & sArr(1) & " %' or EPD_Value like '" & sArr(1) & " %'  or EPD_Value like '% " & sArr(1) & "' or EPD_Value='" & sArr(1) & "')  "

                                    strsql = strsql & " and   EPD_DocType in (" & aDoc(j) & ") "
                                    strsql = "Select epd_baseid from edt_page_details where  " & strsql
                                    dt = objDBL.SQLExecuteDataTable(sAC, strsql)
                                    sLocalVal = String.Empty
                                    If dt.Rows.Count > 0 Then
                                        For k = 0 To dt.Rows.Count - 1
                                            bRet = "," & dt.Rows(k).Item("EPD_BaseID")
                                            If iDesc = 0 Then
                                                sLocalVal = sLocalVal & bRet
                                                bFoundOrNot = True  '#11
                                            Else
                                                If (InStr(sSql & ",", bRet & ",") <> 0) Then
                                                    'ssql = ssql & "|" & bRet
                                                    sLocalVal = sLocalVal & bRet
                                                    bFoundOrNot = True  '#11
                                                End If
                                            End If
                                        Next
                                        iDesc = iDesc + 1
                                        If bFoundOrNot = False Then '#11
                                            GoTo DocTypeLP
                                        Else
                                            sSql = sLocalVal
                                        End If
                                    Else
                                        GoTo DocTypeLP
                                    End If
                                End If
                            End If
DescLP:                 Next      'End of Descriptors Loop
                        sDetID = sDetID & sSql
DocTypeLP:          Next   'End of DocumentType Loop
                End If

                If (sDetID.Length > 0) Then
                    If (sDetID.Chars(0).ToString = ",") Then
                        sDetID = sDetID.Remove(0, 1)
                    End If
                End If
                Return sDetID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckPermissions(ByVal sAC As String, ByVal iFol As Integer, ByVal iDocType As Integer, ByVal iUserID As Integer, ByVal iCompID As Integer) As Integer
        Try
            If (GetFinalDTPermissions(sAC, iDocType, iUserID, "SRH") = 1) Then
                If (GetFinalFolPermissions(sAC, iFol, iUserID, iCompID, "SRH", 2) = 1) Then
                    Return 1
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFinalDTPermissions(ByVal sAC As String, ByVal iDTId As Integer, ByVal iUserId As Int16, Optional ByVal sPerType As String = "ALL") As Object
        Try
            'First Get the Parent GroupId of the Cabinet
            iParGrp = GetParGrpID(sAC, iDTId)
            iUsrParGrp = GetUserParGrp(sAC, iUserId)
            dtPerm = GetMainPermDS(sAC, iDTId, iUserId, iParGrp)
            If (dtPerm.Rows.Count > 0) Then
                Select Case UCase(sPerType)
                    Case "ALL"
                        Dim Ht As New Hashtable
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Ht.Add("DINDEX", 1)
                                Ht.Add("DSEARCH", 1)
                                Ht.Add("MDOCTYPE", 0)
                                Ht.Add("MDOC", 0)
                                Ht.Add("DDOC", 0)
                                Ht.Add("CDOC", 0)
                                Ht.Add("Level", sPermlvl)
                            Else
                                Ht.Add("DINDEX", 0)
                                Ht.Add("DSEARCH", 1)
                                Ht.Add("MDOCTYPE", 0)
                                Ht.Add("MDOC", 0)
                                Ht.Add("DDOC", 0)
                                Ht.Add("CDOC", 0)
                                Ht.Add("Level", sPermlvl)
                            End If

                            Return Ht
                        ElseIf (sPermlvl = "GH") Then
                            Ht.Add("DINDEX", 1)
                            Ht.Add("DSEARCH", 1)
                            Ht.Add("MDOCTYPE", 1)
                            Ht.Add("MDOC", 1)
                            Ht.Add("DDOC", 1)
                            Ht.Add("CDOC", 1)
                            Ht.Add("Level", sPermlvl)
                            Return Ht
                        Else
                            Ht.Add("DINDEX", dtPerm.Rows(0).Item("EDP_INDEX"))
                            Ht.Add("DSEARCH", dtPerm.Rows(0).Item("EDP_SEARCH"))
                            Ht.Add("MDOCTYPE", dtPerm.Rows(0).Item("EDP_MFY_TYPE"))
                            Ht.Add("MDOC", dtPerm.Rows(0).Item("EDP_MFY_DOCUMENT"))
                            Ht.Add("DDOC", dtPerm.Rows(0).Item("EDP_DEL_DOCUMENT"))
                            Ht.Add("CDOC", dtPerm.Rows(0).Item("EDP_OTHER"))
                            Ht.Add("Level", sPermlvl)
                            Return Ht
                        End If

                    Case "IND"
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Return 1
                            Else
                                Return 0
                            End If
                        ElseIf (sPermlvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_INDEX")
                    Case "SRH"
                        If (sPermlvl = "PG") Then
                            Return 1
                        ElseIf (sPermlvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_SEARCH")
                    Case "MDT"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_MFY_TYPE")
                    Case "MDC"
                        If (sPermlvl = "PG") Then
                            Return 1
                        ElseIf (sPermlvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_MFY_DOCUMENT")
                    Case "DDC"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_DEL_DOCUMENT")
                    Case "CDC"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_OTHER")
                End Select
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetMainPermDS(ByVal sAC As String, ByVal iDtID As Integer, ByVal iUserId As Int16, ByVal iGrpId As Int16, Optional ByVal ChkType As Integer = 0) As DataTable
        Dim sSql As String, sCab As String, sMem As String
        Dim dtPerm As DataTable
        Try
            sCab = ""
            sPermlvl = String.Empty
            sMem = GetMemberGroups(sAC, iUserId)
            If (CheckForGrpHead(sAC, iGrpId, iUserId) = 1) Then
                sPermlvl = "GH"
                sSql = "Select * from edt_Document_Type where Dot_DocTypeID=" & iDtID & ""
                dtMain = objDBL.SQLExecuteDataTable(sAC, sSql)
            ElseIf ChkType <> 1 Then
                sDTPerm = GetPermDocTypes(sAC, iUserId, sMem)
                sSql = " Select *  from edt_Document_Type left outer join edt_doctype_permission on DOt_DocTYpeID=EDP_DocTYpeID where  Dot_PGroup in (" & sMem & ") and Dot_Status='A' and Dot_DocTypeId= " & iDtID & " "
                If Val(sDTPerm) <> 0 Then
                    sSql = sSql & " and (EDP_DocTypeID not in (" & sDTPerm & " ) or EDP_DocTypeID is Null)  "
                End If
                If objDBL.DBCheckForRecord(sAC, sSql) = True Then
                    dtMain = objDBL.SQLExecuteDataTable(sAC, sSql)
                    sPermlvl = "PG"
                Else
                    GoTo LP
                End If
            Else
LP:             dtMain = BuildPermDataSet(sAC, iUserId, iDtID, sMem)
                If dtMain.Rows.Count <> 0 Then
                    dtPerm = dtMain
                    dtPerm = GetFinalPermForDS(sAC, dtPerm)
                    Return dtPerm
                Else
                    Dim MyDt As New DataTable
                    Return MyDt
                End If
            End If
            Return dtMain
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildPermDataSet(ByVal sAC As String, ByVal iUserId As Integer, ByVal iDtId As Integer, ByVal sMem As String) As Object
        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iCount As Integer
        Try
            objParam(iCount) = New OleDb.OleDbParameter("@p_UsrId", OleDb.OleDbType.Numeric)
            objParam(iCount).Value = iUserId
            objParam(iCount).Direction = ParameterDirection.Input
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@p_DtId", OleDb.OleDbType.Numeric)
            objParam(iCount).Value = iDtId
            objParam(iCount).Direction = ParameterDirection.Input
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@p_Mem", OleDb.OleDbType.VarChar)
            objParam(iCount).Value = sMem
            objParam(iCount).Direction = ParameterDirection.Input
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@p_iRetLvl", OleDb.OleDbType.VarChar)
            objParam(iCount).Value = 0
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 1

            Dim arr() As Object = objDBL.SPFrLoadingUsingDsParam(sAC, "GetDTPerDetails", 1, "@p_iRetLvl", objParam)
            If IsDBNull(arr(1)) = False Then
                sPermlvl = arr(1)
            Else
                sPermlvl = ""
            End If
            Return arr(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetDescriptor(ByVal sAC As String, ByVal sDocType As String) As Hashtable
        Dim sDT() As String
        Dim i As Integer
        Dim sSql As String = "", sDtDesc As String = ""
        Dim dt As New DataTable
        Dim htDt As Hashtable
        Try
            sDT = Split(sDocType, ",")
            If (sDT.Length > 0) Then
                htDt = New Hashtable
                For i = 0 To UBound(sDT)
                    If sDT(i).Length > 0 Then
                        sSql = "select des_id,desc_name from edt_descriptor where des_id in (select distinct(EDD_DPTRID) & From edt_doctype_link"
                        sSql = sSql & " Where EDD_DocTypeID In (" & sDT(i) & "))"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For j = 0 To dt.Rows.Count - 1
                                sDtDesc = sDtDesc & "+" & dt.Rows(j)("des_ID")
                            Next
                            htDt.Add(sDT(i), sDtDesc)
                        End If
                    End If
                    sDtDesc = String.Empty
                Next
            End If
            Return htDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckDescForDT(ByVal dtHT As Hashtable, ByVal iDocType As Integer, ByVal iDescID As String) As Boolean
        Dim coll As Collections.IDictionaryEnumerator
        Dim sDesc As String = ""
        Dim i As Integer
        Try
            coll = dtHT.GetEnumerator()
            coll.Reset()
            coll.MoveNext()
            For i = 0 To dtHT.Count - 1
                If (iDocType = coll.Key) Then
                    sDesc = coll.Value
                Else
                    coll.MoveNext()
                End If
            Next
            If (InStr(sDesc & "+", "+" & iDescID & "+") > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFinalFolPermissions(ByVal sAC As String, ByVal iFolId As Integer, ByVal iUserId As Integer, ByVal iCompID As Integer, Optional ByVal sPerType As String = "ALL", Optional ByVal ichkType As Integer = 0) As Object
        Try
            If (ichkType = 2) Then
                iParGrp = GetParGrpID(sAC, iFolId)
                iUsrParGrp = GetUserParGrp(sAC, iUserId)
            End If
            dtPerm = GetMainPermFolder(sAC, iFolId, iUserId, iParGrp, iUsrType, iCompID, ichkType)
            If (dtPerm.Rows.Count > 0) Then
                Select Case UCase(sPerType)
                    Case "ALL"
                        Dim HT As New Hashtable
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                HT.Add("FModify", 0)
                                HT.Add("FDelete", 0)
                                HT.Add("FView", 1)
                                HT.Add("DDelete", 0)
                                HT.Add("DModify", 0)
                                HT.Add("DCreate", 0)
                                HT.Add("Index", 1)
                                HT.Add("Search", 1)
                                HT.Add("Export", 0)
                                HT.Add("Level", sPermlvl)
                            Else
                                HT.Add("FModify", 0)
                                HT.Add("FDelete", 0)
                                HT.Add("FView", 1)
                                HT.Add("DDelete", 0)
                                HT.Add("DModify", 0)
                                HT.Add("DCreate", 0)
                                HT.Add("Index", 0)
                                HT.Add("Search", 1)
                                HT.Add("Export", 0)
                                HT.Add("Level", sPermlvl)
                            End If

                            Return HT
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            HT.Add("FModify", 1)
                            HT.Add("FDelete", 1)
                            HT.Add("FView", 1)
                            HT.Add("DDelete", 1)
                            HT.Add("DModify", 1)
                            HT.Add("DCreate", 1)
                            HT.Add("Index", 1)
                            HT.Add("Search", 1)
                            HT.Add("Export", 1)
                            HT.Add("Level", sPermlvl)
                            Return HT
                        End If
                        HT.Add("FModify", dtPerm.Rows(0).Item("EFP_MOD_FOLDER"))
                        HT.Add("FDelete", dtPerm.Rows(0).Item("EFP_DEL_FOLDER"))
                        HT.Add("FView", dtPerm.Rows(0).Item("EFP_VIEW_Fol"))
                        HT.Add("DDelete", dtPerm.Rows(0).Item("EFP_DEL_DOC"))
                        HT.Add("DModify", dtPerm.Rows(0).Item("EFP_MOD_DOC"))
                        HT.Add("DCreate", dtPerm.Rows(0).Item("EFP_CRT_DOC"))
                        HT.Add("Index", dtPerm.Rows(0).Item("EFP_INDEX"))
                        HT.Add("Search", dtPerm.Rows(0).Item("EFP_SEARCH"))
                        HT.Add("Export", dtPerm.Rows(0).Item("EFP_EXPORT"))
                        HT.Add("Level", sPermlvl)
                        Return HT
                    Case "MFD"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_MOD_FOLDER")
                    Case "DFD"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_DEL_FOLDER")
                    Case "VFD"
                        If (sPermlvl = "PG") Then
                            Return 1
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_VIEW_Fol")
                    Case "DDM"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_DEL_DOC")
                    Case "MDM"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_MOD_DOC")
                    Case "IDX"
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Return 1
                            Else
                                Return 0
                            End If
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_INDEX")
                    Case "SRH"
                        If (sPermlvl = "PG") Then
                            Return 1 'Return 0 ' vijeth
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_SEARCH")
                    Case "EXP"
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_EXPORT")
                End Select
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMainPermFolder(ByVal sAC As String, ByVal iFolId As Integer, ByVal iUserId As Int16, ByVal iGrpId As Int16, ByVal iUsrType As Integer, ByVal iCompID As Integer, Optional ByVal iChkType As Int16 = 0) As DataTable
        Dim sSql As String
        Dim dtMain As New DataSet, dtPerm As New DataTable
        Dim iFlag As Integer = 0
        Try
            sPermlvl = String.Empty
            If (iChkType = 2) Then
                sMem = GetMemberGroups(sAC, iUserId)
                iUsrType = GetUserType(sAC, iUserId)
            End If
            If (iUsrType = 1) Then
                sPermlvl = "PU"
                sSql = "Select * from EDT_FOLDER where Fol_DelFlag='A' And FOL_CompID=" & iCompID & " and  Fol_FolId=" & iFolId & ""
                ' sSql = "Select * from EDT_FOLDER where Fol_DelFlag='A' And FOL_CompID=" & iCompID & ""\\Vijeth
                'sSql = "Select * from View_FolCab where Fol_Status='A' and Fol_FolId=" & iFolId
                dtMain = objDBL.SQLExecuteDataSet(sAC, sSql)
            ElseIf (CheckForGrpHead(sAC, iGrpId, iUserId) = 1) Then
                'Check For Group Head
                sPermlvl = "GH"
                sSql = "Select * from edt_folder where Fol_FolId=" & iFolId & ""
                dtMain = objDBL.SQLExecuteDataSet(sAC, sSql)
            ElseIf (iChkType <> 1) Then
                If (sFolPerm = String.Empty) Then
                    sFolPerm = GetPermFolders(sAC, iUserId, sMem)
                    'If IsDBNull(sFolPerm) = False Then
                    '    sFolPerm = "1"
                    'Else
                    '    sFolPerm = 0
                    'End If
                End If
                'sSql = "Select * from edt_folder left outer join edt_folder_permission on Fol_FolID=EFP_FolID where " & iParGrp & "  in (" & sMem & ") and Fol_DelFlag='A' and Fol_FolId= " & iFolId & " "
                sSql = "Select * from edt_folder left outer join edt_folder_permission on Fol_FolID=EFP_FolID where EFP_GRPID in (" & sMem & ") and Fol_DelFlag='A' and Fol_FolId= " & iFolId & " "
                If Val(sFolPerm) <> 0 Then
                    sSql = sSql & " and (Fol_FolID not in (" & sFolPerm & " ) or EFP_FolID is Null)  "
                End If
                If objDBL.DBCheckForRecord(sAC, sSql) = True Then
                    dtMain = objDBL.SQLExecuteDataSet(sAC, sSql)
                    sPermlvl = "PG"
                Else
                    GoTo LP
                End If
            Else
                'sSql = "Select * from edt_folder left outer join edt_folder_permission on Fol_FolID=EFP_FolID where " & iParGrp & "  in (" & sMem & ") and Fol_DelFlag='A' and Fol_FolId= " & iFolId & " "
                'sSql = sSql & " and (Fol_FolID not in (" & sFolPerm & " ) or EFP_FolID is not Null) "
                'dtMain = objDBL.SQLExecuteDataTable(sAC, sSql)
                'Return dtMain
LP:             dtMain = BuildPermDataSetFol(sAC, iUserId, iFolId, sMem, iChkType)
                If dsMain.Tables.Count >= 0 Then                                  'Vijeth 30/01/19
                    Try
                        dtPerm = dsMain.Tables(0)
                        dtPerm = GetFinalPermForDS(sAC, dtPerm)
                        Return dtPerm
                    Catch
                    End Try
                Else

                    Dim MyDt As New DataTable
                    Return MyDt
                End If
            End If
            Return dtMain.Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetPermFolders(ByVal sAC As String, ByVal iUserID As String, ByVal sGrpID As String) As String
        Dim sSql As String, sRet As String, sFolId As String = ""
        Dim Arr() As String, sFArr() As String
        Dim i As Integer
        Try
            Arr = Split(sGrpID, ",")
            For i = 0 To UBound(Arr)
                sSql = "edt_Folder_permission where EFP_Grpid in (" & Arr(i) & ") and (EFP_UsrId=" & iUserID & " or EFP_UsrId=0)"
                sRet = objDBL.GetAllValues(sAC, "EFP_FolId", sSql)
                If Val(sRet) <> 0 Then
                    If Right(sRet, 1) = ";" Then
                        sRet = Left(sRet, Len(sRet) - 1)
                    End If
                    sFolId = sFolId & ";" & sRet & ";"
                End If
            Next
            sSql = "Edt_Folder_permission where EFP_ptype = 'E'"
            sFolId = sFolId & objDBL.GetAllValues(sAC, "EFP_FolId", sSql)
            sFolId = Replace(sFolId, ";", ",")

            If Len(Trim(sFolId)) = 0 Then
                sFolId = "0"
            End If

            sFArr = Split(sFolId, ",")
            For i = 0 To UBound(sFArr)
                If Val(sFArr(i)) <> 0 Then
                    GetPermFolders = GetPermFolders & "," & Val(sFArr(i))
                End If
            Next
            If Left(GetPermFolders, 1) = "," Then
                GetPermFolders = Right(GetPermFolders, Len(GetPermFolders) - 1)
            End If
            If Right(GetPermFolders, 1) = "," Then
                GetPermFolders = Left(GetPermFolders, Len(GetPermFolders) - 1)
            End If
            Return GetPermFolders
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckFolDocTypePerm(ByVal sAC As String, ByVal sFol As String, ByVal sDocType As String, ByVal iUserID As Integer) As Boolean
        Dim sSQL As String
        Dim iRet As Integer = 0
        Try
            sSQL = "Select * from edt_folder_rights where Fer_DocTypeId In (" & sDocType & ") And Fer_Folderid In (" & sFol & ") And Fer_UserId=" & iUserID & " And Fer_search=0"
            iRet = objDBL.SQLExecuteScalar(sAC, sSQL)
            If iRet = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ChkFolDocTypePerm(ByVal sAC As String, ByVal dtSearch As DataTable, ByVal iUserID As Integer) As DataTable
        Dim iCount As Integer, j As Integer, i As Integer
        Dim dtChk As New DataTable, dt As New DataTable
        Dim sSql As String = "", sFol As String, sDocType As String
        Try
            sFol = GetFolderID(dtSearch)
            sDocType = GetDocTypeID(dtSearch)
            If CheckFolDocTypePerm(sAC, sFol, sDocType, iUserID) = True Then
                sSql = "Select * from edt_folder_rights where Fer_DocTypeId in (" & sDocType & ") and Fer_Folderid in (" & sFol & ") and Fer_UserId=" & iUserID & " and Fer_search=0"
            End If
            dtChk = objDBL.SQLExecuteDataTable(sAC, sSql)
            If (dtChk Is Nothing) Then
                dt = New DataTable
                Return dt
            Else
                If dtChk.Rows.Count > 0 Then
                    If (dtChk.Rows.Count > 0) Then
                        For iCount = 0 To dtChk.Rows.Count - 1
                            j = 0
                            For i = 0 To dtSearch.Rows.Count - 1
                                If (dtSearch.Rows(j).Item("FolID") = dtChk.Rows(iCount).Item("FER_FolderID") And dtSearch.Rows(j).Item("DocTypeID") = dtChk.Rows(iCount).Item("FER_DocTypeID")) Then
                                    dtSearch.Rows(j).Delete()
                                Else
                                    j = j + 1
                                End If
                            Next
                        Next
                    End If
                End If
            End If
            Return dtSearch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFolderID(ByVal dtSearch As DataTable) As String
        Dim dr As DataRow
        Dim sFol As String = ""
        Try
            For Each dr In dtSearch.Rows
                sFol = sFol & "," & dr("FolID")
            Next
            If (sFol.Length > 0) Then
                If (sFol.Chars(0).ToString = ",") Then
                    sFol = sFol.Remove(0, 1)
                End If
            End If
            Return sFol
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocTypeID(ByVal dtSearch As DataTable) As String
        Dim dr As DataRow
        Dim sDT As String = ""
        Try
            For Each dr In dtSearch.Rows
                sDT = sDT & "," & dr("DocTypeId")
            Next
            If (sDT.Length > 0) Then
                If (sDT.Chars(0).ToString = ",") Then
                    sDT = sDT.Remove(0, 1)
                End If
            End If
            Return sDT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDescType(ByVal sAC As String, ByVal iDoc As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select a.dt_name from EDT_DESC_TYPE a ,edt_descriptor b where b.desc_datatype=a.dt_id And b.des_id=" & iDoc)
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function

    'chkselect
    Public Function GetDescUnion(ByVal sAC As String, Optional ByVal sDocTypeID As String = "") As DataTable
        Dim sSql As String
        Dim sArr() As String
        Dim i As Integer
        Dim RefDt As New DataTable, dt As New DataTable
        Dim dc As DataColumn
        Try
            If (sDocTypeID <> "") Then
                sArr = Split(sDocTypeID, ";")
                sDocTypeID = String.Empty
                For i = 0 To UBound(sArr)
                    If (sArr(i).Length > 0) Then
                        sDocTypeID = sDocTypeID & "," & sArr(i)
                    End If
                Next
                If (sDocTypeID.Length > 0) Then
                    sDocTypeID = sDocTypeID.Remove(0, 1)
                End If
                sSql = "Select b.DES_ID As ID,b.DESC_Name As Name from edt_doctype_link a,edt_descriptor b where edd_doctypeID In ( " & sDocTypeID & " ) And a.edd_dptrid=b.des_id"
            Else
                sSql = "Select DES_ID As ID,DESC_Name As Name from edt_descriptor where des_id In (Select distinct(EDD_DPTRID) " & " from edt_doctype_link"
                sSql = sSql & " where EDD_DocTypeID In (Select Dot_doctypeID from edt_document_type ))"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Columns.Count - 1
                    dc = New DataColumn(dt.Columns(i).ColumnName, dt.Columns(i).DataType)
                    RefDt.Columns.Add(dc)
                Next
                RefDt = CopyRowsToFirstDT(dt, RefDt)
            End If
            Return RefDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CopyRowsToFirstDT(ByVal dt1 As DataTable, ByVal dt2 As DataTable) As DataTable
        Dim dr1 As DataRow, dr2 As DataRow, drRef2 As DataRow
        Dim i As Integer, j As Integer = 0
        Try
            For Each dr1 In dt1.Rows
                j = 0
                For Each drRef2 In dt2.Rows
                    If (dr1("ID") = drRef2("ID")) Then
                        j = 1
                    End If
                Next
                If (j = 0) Then
                    dr2 = dt2.NewRow
                    For i = 0 To dt1.Columns.Count - 1
                        dr2(dt1.Columns(i).ColumnName) = dr1(dt1.Columns(i).ColumnName)
                    Next
                    dt2.Rows.Add(dr2)
                End If
            Next
            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function


    'SearchView
    Public Function GetCrBy(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select usr_fullname from sad_userdetails where usr_id = (Select Pge_CreatedBy from edt_page where pge_basename=" & iDetails_id & ")")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCrOn(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sCrOn As String = ""
        Try
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, "Select Pge_CreatedOn from edt_page where pge_basename=" & iDetails_id & "")) = False Then
                sCrOn = objclsEDICTGeneral.FormatDtForRDBMS(objDBL.SQLGetDescription(sAC, "Select Pge_CreatedOn from edt_page where pge_basename=" & iDetails_id & ""), "D")
            End If
            Return sCrOn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetModOn(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sModOn As String = ""
        Try
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, "Select pge_modon from edt_page where pge_details_id=" & iDetails_id & "")) = False Then
                sModOn = objclsEDICTGeneral.FormatDtForRDBMS(objDBL.SQLGetDescription(sAC, "Select pge_modon from edt_page where pge_details_id=" & iDetails_id & ""), "D")
            End If
            Return sModOn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatus(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select pge_status from edt_page where pge_Details_id= " & iDetails_id & "")
            If sName = "A" Then
                sName = "Active"
            Else
                sName = "In-Active"
            End If
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalPage(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select count(*) from edt_page where pge_status='A'and pge_details_id= " & iDetails_id & "")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFileSize(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "select sum(pge_size) from edt_page where pge_details_id= " & iDetails_id & "")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetModBy(ByVal sAC As String, ByVal iBasename As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select a. usr_fullname from sad_userdetails a,edt_page b where a.usr_id= b.pge_modby and b.pge_details_id = " & iBasename & "")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKWords(ByVal sAC As String, ByVal ipDetails As Integer) As String
        Dim sKeyWords As String = ""
        Try
            sKeyWords = objDBL.SQLGetDescription(sAC, "Select distinct epd_keyword from edt_page_details where  epd_baseid =" & ipDetails & "")
            Return sKeyWords
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDescName(ByVal sAC As String, ByVal ipDetailsId As Integer) As String
        Dim sSql As String, sDescName As String = ""
        Dim iCount As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "Select b.desc_name +' : ' + a.epd_value as desc_name  from edt_page_details a,edt_descriptor b where a.epd_descid=b.des_id and "
            sSql = sSql & " a.epd_baseid = " & ipDetailsId & " And a.epd_doctype In (Select pge_document_type from edt_page where pge_details_id =" & ipDetailsId & ")"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iCount = iCount + 1
                    If iCount > 1 Then
                        sDescName = sDescName & "'" & dt.Rows(i)("desc_name")
                    Else
                        sDescName = dt.Rows(i)("desc_name")
                    End If
                Next
            End If
            Return sDescName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetScanDetails(ByVal sAC As String, ByVal ipDetails As Integer) As String
        Dim sSql As String, sScanDt As String = "", sLocation As String = "", sBuilding As String = "", sFloor As String = ""
        Dim sRoomNo As String = "", sRow As String = "", sColumn As String = "", sRackNo As String = "", sDescription As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from edt_ScanDoc_Details where SCAN_BatchID in(select pge_BatchID from edt_page where pge_basename=" & ipDetails & ")"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("SCAN_LOCATION") <> "" Then
                    sLocation = "Location : " & dt.Rows(0)("SCAN_LOCATION")
                End If

                If dt.Rows(0)("SCAN_BUILDING") <> "" Then
                    sBuilding = "Building : " & dt.Rows(0)("SCAN_BUILDING")
                End If

                If dt.Rows(0)("SCAN_FLOOR") <> "" Then
                    sFloor = "Floor : " & dt.Rows(0)("SCAN_FLOOR")
                End If

                If dt.Rows(0)("SCAN_ROOMNO") <> "" Then
                    sRoomNo = "RoomNo : " & dt.Rows(0)("SCAN_ROOMNO")
                End If

                If dt.Rows(0)("SCAN_ROW") <> "" Then
                    sRow = "Row : " & dt.Rows(0)("SCAN_ROW")
                End If

                If dt.Rows(0)("SCAN_COLUMN") <> "" Then
                    sColumn = "Column : " & dt.Rows(0)("SCAN_COLUMN")
                End If

                If dt.Rows(0)("SCAN_RACKNO") <> "" Then
                    sRackNo = "RackNo : " & dt.Rows(0)("SCAN_RACKNO")
                End If

                If dt.Rows(0)("SCAN_DESCRIPTION") <> "" Then
                    sDescription = "Description : " & dt.Rows(0)("SCAN_DESCRIPTION")
                End If
            End If
            If sLocation <> "" Then
                sScanDt = sScanDt & "|" & sLocation
            End If

            If sBuilding <> "" Then
                sScanDt = sScanDt & "|" & sBuilding
            End If

            If sFloor <> "" Then
                sScanDt = sScanDt & "|" & sFloor
            End If

            If sRoomNo <> "" Then
                sScanDt = sScanDt & "|" & sRoomNo
            End If

            If sRow <> "" Then
                sScanDt = sScanDt & "|" & sRow
            End If

            If sColumn <> "" Then
                sScanDt = sScanDt & "|" & sColumn
            End If

            If sRackNo <> "" Then
                sScanDt = sScanDt & "|" & sRackNo
            End If

            If sDescription <> "" Then
                sScanDt = sScanDt & "|" & sDescription
            End If
            If sScanDt.StartsWith("|") Then
                sScanDt = sScanDt.Remove(0, 1)
            End If
            Return sScanDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetPageFromEdict(ByVal sAC As String, ByVal iSelectedIndexID As Integer) As String
    '    Dim sImagePath As String = "", sExt As String = "", sFIleInDB As String = "", sBool As String = ""
    '    Dim files() As String
    '    Try
    '        sFIleInDB = objDBL.SQLExecuteScalar(sAC, "Select Set_Value from edt_Settings where SET_CODE = 'FileInDB'")
    '        If UCase(sFIleInDB) = "FALSE" Then
    '            sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
    '            sBool = GetImageSettings(sAC, "ImagePath")
    '            sImagePath = sBool & "BITMAPS\" & iSelectedIndexID \ 301 & "\"
    '            If System.IO.Directory.Exists(sImagePath) = False Then
    '                System.IO.Directory.CreateDirectory(sImagePath)
    '            End If
    '            sImagePath = sImagePath & iSelectedIndexID & "." & sExt
    '        ElseIf UCase(sFIleInDB) = "TRUE" Then
    '            sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
    '            Dim sPath As String = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
    '            If System.IO.Directory.Exists(sPath) = True Then
    '                files = Directory.GetFileSystemEntries(sPath)
    '                For Each element As String In files
    '                    If (Not Directory.Exists(element)) Then
    '                        File.Delete(Path.Combine(sPath, Path.GetFileName(element)))
    '                    End If
    '                Next
    '            End If

    '            If System.IO.Directory.Exists(sPath) = False Then
    '                System.IO.Directory.CreateDirectory(sPath)
    '            End If

    '            Updateimage(sAC, iSelectedIndexID, sExt)
    '            sImagePath = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
    '            If System.IO.Directory.Exists(sImagePath) = False Then
    '                System.IO.Directory.CreateDirectory(sImagePath)
    '            End If
    '            sImagePath = sImagePath & iSelectedIndexID & sExt
    '        End If
    '        GetPageFromEdict = sImagePath
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetImageSettings(ByVal sAC As String, ByVal sCode As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from edt_Settings where Set_Code ='" & sCode & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("Set_Value").ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Updateimage(ByVal sAC As String, ByVal iSelectedIndexID As Integer, ByVal sExt As String)
        Dim pdr As OleDb.OleDbDataReader
        Dim sSql As String
        Dim iAtchOle As Integer
        Try
            Dim sPath As String = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
            If Not System.IO.Directory.Exists(sPath) Then
                Directory.CreateDirectory(sPath)
            End If
            sPath = sPath & iSelectedIndexID & "." & sExt
            If System.IO.File.Exists(sPath) = False Then
                sSql = "Select BDT_BIGDATA,BDT_BASENAME from EDT_BIGDATA where BDT_BASENAME=" & Val(iSelectedIndexID) & ""
                pdr = objDBL.SQLDataReader(sAC, sSql)
                If pdr.HasRows Then
                    While pdr.Read()
                        Dim BUFFER(pdr.GetBytes(iAtchOle, 0, BUFFER, 0, Integer.MaxValue)) As Byte
                        pdr.GetBytes(iAtchOle, 0, BUFFER, 0, BUFFER.Length)
                        Dim BlobData As New IO.FileStream(sPath, IO.FileMode.Create, IO.FileAccess.Write)
                        BlobData.Write(BUFFER, 0, BUFFER.Length)
                        BlobData.Close()
                    End While
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    'ImageView
    Public Function LoadListFiles(ByVal sAC As String, ByVal iBaseID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select pge_basename from EDT_PAGE where pge_status <> 'X' and Pge_Details_Id=" & iBaseID & " ORDER BY PGE_PAGENO"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetName(ByVal sAC As String, ByVal iBaseId As Integer, ByVal sType As String) As String
        Dim sName As String, sSql As String = ""
        Dim dt As New DataTable
        Try
            If sType = "Size" Then
                sSql = "Select PGE_SIZE from EDT_PAGE where pge_status= 'A' and PGE_BASENAME=" & iBaseId & ""
            ElseIf sType = "FullName" Then
                sSql = "Select Usr_FullName from sad_userDetails where usr_Id in(Select Pge_CreatedBy from EDT_PAGE where PGE_BaseName=" & iBaseId & ")"
            ElseIf sType = "DocName" Then
                sSql = "select DOT_DocName from edt_page Left Join edt_document_type On Dot_DoctypeID=PGE_DOCUMENT_TYPE And DOT_Delflag='A'"
                sSql = sSql & " Where PGE_BaseName=" & iBaseId & ""
            End If

            sName = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocTypeID(ByVal sAC As String, ByVal iBaseId As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select PGE_Document_type from EDT_PAGE where PGE_BaseName=" & iBaseId & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearchCrON(ByVal sAC As String, ByVal iBaseId As Integer) As Object
        Dim sSql As String
        Try
            sSql = "Select Case When Pge_CreatedOn Is NULL then '' else Pge_CreatedOn End As Pge_CreatedOn From EDT_PAGE Where PGE_BaseName=" & iBaseId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIndexDetails(ByVal sAC As String, ByVal iDocType As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable, dtPage As New DataTable
        Dim dr As DataRow
        Dim iBaseId As Integer
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("Descriptor")

            sSql = "select a.des_id,a.Desc_name,a.Desc_Datatype,a.Desc_size ,b.edd_isrequired "
            sSql = sSql & "from edt_descriptor a,EDT_DOCTYPE_LINK b "
            sSql = sSql & "where a.des_id=b.edd_dptrid and b.edd_doctypeid= " & iDocType & " order by a.des_id"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dr = dt.NewRow
                    If IsDBNull(dtDetails.Rows(i)("DES_ID")) = False Then
                        dr("ID") = dtDetails.Rows(i)("DES_ID")
                    End If

                    If iBaseName <> 0 Then
                        iBaseId = Val(objDBL.SQLExecuteScalar(sAC, "select Pge_DETAILS_ID from edt_page where pge_basename = " & iBaseName) & "")
                        sSql = "select * from EDT_PAGE where pge_details_id = " & iBaseId & ""
                        dtPage = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dtPage.Rows.Count > 0 Then
                            For j = 0 To dtPage.Rows.Count - 1
                                dr("Descriptor") = dtDetails.Rows(i)("Desc_Name") & " : " & objDBL.SQLExecuteScalar(sAC, "select epd_value from EDT_PAGE_DETAILS where EPD_BaseID = " & Val(dtPage.Rows(j)("Pge_DETAILS_ID")) & " and EPD_Doctype = " & Val(dtPage.Rows(j)("PGE_DOCUMENT_TYPE")) & " and EPD_DescID = " & dtDetails.Rows(i)("DES_ID") & "")
                            Next
                        Else
                            dr("Descriptor") = ""
                        End If
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildMasterTable() As DataTable
        Dim dt As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("ID")
            dt.Columns.Add(dc)
            dc = New DataColumn("Descriptor")
            dt.Columns.Add(dc)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExtension(ByVal sAC As String, ByVal iBaseID As Integer) As String
        Dim sSql As String, sExtension As String
        Try
            sSql = "Select PGE_Ext from edt_page where pge_basename=" & iBaseID & ""
            sExtension = objDBL.SQLGetDescription(sAC, sSql)
            Return sExtension
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPageFromEdict(ByVal sAC As String, ByVal iBaseName As Long, ByVal sExt As String) As String
        Dim sImagePath As String = "", sFIleInDB As String, sIPath As String
        Try
            sFIleInDB = objDBL.SQLExecuteScalar(sAC, "Select Set_Value from edt_Settings where SET_CODE = 'FileInDB'")
            If UCase(sFIleInDB) = "FALSE" Then
                sIPath = GetImgPath(sAC, "ImagePath")
                sImagePath = sIPath & "BITMAPS\" & iBaseName \ 301 & "\"
                sExt = objDBL.SQLGetDescription(sAC, "Select pge_ext from EDT_PAGE where pge_basename =  " & iBaseName & "")

                If (sExt.Contains(".") = True) Then
                    sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                Else
                    sImagePath = sImagePath & iBaseName & "." & sExt   'Actual File Name
                End If

            ElseIf UCase(sFIleInDB) = "TRUE" Then
                sImagePath = Updateimage(sAC, iBaseName, sExt)
            End If
            GetPageFromEdict = sImagePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImgPath(ByVal sAC As String, ByVal sKEy As String) As String
        Dim sSql As String
        Try
            sSql = "Select SET_Value from edt_Settings where Set_code = '" & sKEy & "'"
            GetImgPath = objDBL.SQLExecuteScalar(sAC, sSql)
            Return GetImgPath
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Collation Details
    Public Function CheckCollationApproved(ByVal sAC As String, ByVal iColId As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select case when CLT_DelFlag Is Null Then '' Else CLT_DelFlag End As CLT_DelFlag from EDT_COLLATE where CLT_COLLATENO=" & iColId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCollDetails(ByVal sAC As String, ByVal iUserId As Integer, ByVal iCompId As Integer)
        Dim sSql As String
        Dim sGrpId As String = String.Empty
        Dim dtCol As New DataTable, dt As New DataTable, dtColDetails As New DataTable
        Dim dRow As DataRow
        Try
            sSql = "Select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID=" & iUserId & " "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If sGrpId = String.Empty Then
                        sGrpId = 0 & "," & dt.Rows(i)("SUO_DeptID")
                    Else
                        sGrpId = sGrpId & "," & dt.Rows(i)("SUO_DeptID")
                    End If
                Next
            End If

            dtCol.Columns.Add("clt_collateref")
            dtCol.Columns.Add("CLT_COLLATENO")
            dtCol.Columns.Add("usr_fullname")
            dtCol.Columns.Add("clt_createdon")
            dtCol.Columns.Add("clt_comment")
            dtCol.Columns.Add("clt_group")

            sSql = "Select CLT_COLLATENO,clt_collateref,a.usr_fullname,clt_allow,clt_createdon,clt_comment,clt_group from edt_collate,sad_userdetails a where clt_creator = " & iUserId & " and clt_allow = 1  and a.usr_id=clt_Creator"
            sSql = sSql & " union select clt_collateno,clt_collateref,a.usr_fullname,clt_allow,clt_createdon,clt_comment,clt_group from edt_collate,sad_userdetails a where clT_group in (" & sGrpId & ") and clt_allow = 0 and clt_collateno not in (select clt_collateno from edt_collate where clt_creator = " & iUserId & "  and clt_allow = 1)and a.usr_id=clt_Creator And CLT_DelFlag='A'"
            dtColDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtColDetails.Rows.Count > 0 Then
                For j = 0 To dtColDetails.Rows.Count - 1
                    dRow = dtCol.NewRow
                    If IsDBNull(dtColDetails.Rows(j)("CLT_COLLATENO")) = False Then
                        dRow("CLT_COLLATENO") = dtColDetails.Rows(j)("CLT_COLLATENO")
                    End If
                    If IsDBNull(dtColDetails.Rows(j)("clt_collateref")) = False Then
                        dRow("clt_collateref") = dtColDetails.Rows(j)("clt_collateref")
                    End If
                    If IsDBNull(dtColDetails.Rows(j)("usr_fullname")) = False Then
                        dRow("usr_fullname") = dtColDetails.Rows(j)("usr_fullname")
                    End If
                    If IsDBNull(dtColDetails.Rows(j)("clt_createdon")) = False Then
                        dRow("clt_createdon") = objclsEDICTGeneral.FormatDtForRDBMS(dtColDetails.Rows(j)("clt_createdon"), "F")
                    End If
                    If IsDBNull(dtColDetails.Rows(j)("clt_comment")) = False Then
                        dRow("clt_comment") = dtColDetails.Rows(j)("clt_comment")
                    End If
                    If IsDBNull(dtColDetails.Rows(j)("clt_group")) = False Then
                        If dtColDetails.Rows(j)("clt_group") <> 0 Then
                            'dRow("clt_group") = objDBL.SQLGetDescription(sAC, "Select mas_description from SAD_GrpOrLvl_General_Master where mas_id=" & dtColDetails.Rows(j)("clt_group") & "")
                            dRow("clt_group") = objDBL.SQLGetDescription(sAC, "Select Org_Name from sad_org_structure where Org_Node=" & dtColDetails.Rows(j)("clt_group") & " And org_levelcode=3 And Org_CompID=" & iCompId & "")
                        Else
                            dRow("clt_group") = "Non-Groups"
                        End If
                    End If
                    dtCol.Rows.Add(dRow)
                Next
            End If
            Return dtCol
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCollationDocDetails(ByVal sAC As String, ByVal objstrCollationDoc As strCollationDoc) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(4) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLD_COLLATENO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollationDoc.iCOLLATENO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLD_DOCID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollationDoc.iDOCID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLD_PAGEID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollationDoc.iPAGEID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_COLLATEDOC", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFolderDetails(ByVal sAC As String, ByVal iCompID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "Select Fol_FOlID As ID,Fol_Name As Name,Fol_Note As FolNote,FOL_CreatedOn As FolCrOn,FOL_CreatedBy As FolCrBy,Fol_Cabinet As FolCabId,CBN_Department"
            sSql = sSql & " From EDT_FOLDER Left Join EDT_Cabinet On CBN_ID=FOL_Cabinet Left Join Sad_Org_Structure On Org_Node=CBN_Department Where"
            sSql = sSql & " Fol_DelFlag ='A' And FOL_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFileNames(ByVal sAC As String, ByVal iBaseID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select pge_basename,pge_OrignalFileName from EDT_PAGE where pge_status <> 'X' and Pge_Details_Id=" & iBaseID & " ORDER BY PGE_PAGENO"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Annotation   
    Public Function SaveAnnotationImagesDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iDocumentID As Integer, ByVal iFileID As Integer,
                                    ByVal sOriginalName As String, ByVal sEXT As String, ByVal iSIZE As Integer, ByVal sIPAddress As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(8) {}
        Dim iParamCount As Integer
        Dim Arr(0) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_DocumentID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDocumentID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_FileID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iFileID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_OriginalName", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = sOriginalName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_EXT", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = sEXT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_SIZE", OleDb.OleDbType.BigInt, 20)
            ObjParam(iParamCount).Value = iSIZE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EAD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iPKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iPKID"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "SP_EDT_Annotation_Details", 0, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAnnotaionSavedCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocumentID As Integer, ByVal iFileID As Integer, ByVal iTypeID As Integer) As Integer
        Dim sSql As String
        Try
            If iTypeID = 1 Then
                sSql = "Select Count(*)+1 From EDT_Annotation_Details Where EAD_DocumentID=" & iDocumentID & " And EAD_FileID=" & iFileID & " And EAD_CompID=" & iACID & ""
            Else
                sSql = "Select Count(*)+1 From EDT_ATTACHMENTS Where ATCH_ID=" & iDocumentID & " And ATCH_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOriginalFilenameToAnnotaion(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocumentID As Integer, ByVal iFileID As Integer, ByVal iTypeID As Integer) As String
        Dim sSql As String
        Try
            If iTypeID = 1 Then
                sSql = "Select pge_OrignalFileName From EDT_Page Where Pge_DETAILS_ID=" & iDocumentID & " And PGE_BASENAME=" & iFileID & " And Pge_CompID=" & iACID & ""
            Else
                sSql = "Select ATCH_FNAME From EDT_ATTACHMENTS Where ATCH_ID=" & iDocumentID & " And ATCH_DOCID=" & iFileID & " And ATCH_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAnnotaionSaved(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocumentID As Integer, ByVal iFileID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select EAD_PKID,EAD_OriginalName From EDT_Annotation_Details Where EAD_DocumentID=" & iDocumentID & " And EAD_FileID=" & iFileID & " And EAD_CompID=" & iACID & " Order by EAD_PKID"
            Return objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAnnotaionPageFromEdict(ByVal sAC As String, ByVal iSelectedIndexID As Integer) As String
        Dim sImagePath As String = "", sExt As String = "", sBool As String = ""
        Try
            sExt = objDBL.SQLExecuteScalar(sAC, "Select EAD_EXT from EDT_Annotation_Details where EAD_PKID=" & iSelectedIndexID & "")
            sBool = GetImageSettings(sAC, "ImagePath")
            sImagePath = sBool & "ANNOTATIONS\" & iSelectedIndexID \ 301 & "\"
            If System.IO.Directory.Exists(sImagePath) = False Then
                System.IO.Directory.CreateDirectory(sImagePath)
            End If
            If (sExt.Contains(".") = True) Then
                sImagePath = sImagePath & iSelectedIndexID & sExt
            Else
                sImagePath = sImagePath & iSelectedIndexID & "." & sExt
            End If


            GetAnnotaionPageFromEdict = sImagePath
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetFileNames(ByVal sAC As String, ByVal iACID As Integer, ByVal iBaseID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select pge_OrignalFileName from EDT_PAGE where pge_status= 'A' and PGE_BASENAME=" & iBaseID & " And Pge_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetInwardFileNames(ByVal sAC As String, ByVal iACID As Integer, ByVal iBaseID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Edt_Attachments A Join sad_userDetails B on A.ATCH_CREATEDBY = B.USR_ID  where A.ATCH_DOCID = " & iBaseID & " and  A.ATCH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPageFromEdict(ByVal sAC As String, ByVal iSelectedIndexID As Integer) As String
        Dim sImagePath As String = "", sExt As String = "", sFIleInDB As String = "", sBool As String = "", ssExt As String = ""
        Dim files() As String
        Try
            sFIleInDB = objDBL.SQLExecuteScalar(sAC, "Select Set_Value from edt_Settings where SET_CODE = 'FileInDB'")
            If UCase(sFIleInDB) = "FALSE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                sBool = GetImageSettings(sAC, "ImagePath")
                sImagePath = sBool & "BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                ssExt = objclsEDICTGeneral.ChangeExt(sExt)
                'sImagePath = sImagePath & iSelectedIndexID & ssExt

                If (ssExt.Contains(".") = True) Then
                    sImagePath = sImagePath & iSelectedIndexID & ssExt
                Else
                    sImagePath = sImagePath & iSelectedIndexID & "." & ssExt
                End If


            ElseIf UCase(sFIleInDB) = "TRUE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                Dim sPath As String = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sPath) = True Then
                    files = Directory.GetFileSystemEntries(sPath)
                    For Each element As String In files
                        If (Not Directory.Exists(element)) Then
                            File.Delete(Path.Combine(sPath, Path.GetFileName(element)))
                        End If
                    Next
                End If

                If System.IO.Directory.Exists(sPath) = False Then
                    System.IO.Directory.CreateDirectory(sPath)
                End If

                Updateimage(sAC, iSelectedIndexID, sExt)
                sImagePath = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                If (sExt.Contains(".") = True) Then
                    sImagePath = sImagePath & iSelectedIndexID & sExt
                Else
                    sImagePath = sImagePath & iSelectedIndexID & "." & sExt
                End If

            End If
            GetPageFromEdict = sImagePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPageFromEdict1(ByVal sAC As String, ByVal iSelectedIndexID As Integer) As String
        Dim sImagePath As String = "", sExt As String = "", sFIleInDB As String = "", sBool As String = "", ssExt As String = ""
        Dim files() As String
        Try
            sFIleInDB = objDBL.SQLExecuteScalar(sAC, "Select Set_Value from edt_Settings where SET_CODE = 'FileInDB'")
            If UCase(sFIleInDB) = "FALSE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                sBool = GetImageSettings(sAC, "ImagePath")
                sImagePath = sBool & "Archive\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                ssExt = objclsEDICTGeneral.ChangeExt(sExt)
                If (ssExt.Contains(".") = True) Then
                    sImagePath = sImagePath & iSelectedIndexID & ssExt
                Else
                    sImagePath = sImagePath & iSelectedIndexID & "." & ssExt
                End If

            ElseIf UCase(sFIleInDB) = "TRUE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                Dim sPath As String = "C:\EDICTDB-INFO2\Archive" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sPath) = True Then
                    files = Directory.GetFileSystemEntries(sPath)
                    For Each element As String In files
                        If (Not Directory.Exists(element)) Then
                            File.Delete(Path.Combine(sPath, Path.GetFileName(element)))
                        End If
                    Next
                End If

                If System.IO.Directory.Exists(sPath) = False Then
                    System.IO.Directory.CreateDirectory(sPath)
                End If

                Updateimage(sAC, iSelectedIndexID, sExt)
                sImagePath = "C:\EDICTDB-INFO2\Archive" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                If (sExt.Contains(".") = True) Then
                    sImagePath = sImagePath & iSelectedIndexID & sExt
                Else
                    sImagePath = sImagePath & iSelectedIndexID & "." & sExt
                End If

            End If
            GetPageFromEdict1 = sImagePath
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetConfigSettings(ByVal sAC As String, ByVal sCode As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from sad_Config_Settings where SAD_Config_Key ='" & sCode & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("SAD_Config_Value").ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
