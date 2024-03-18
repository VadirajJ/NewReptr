Imports System
Imports System.Data
Imports DatabaseLayer
Imports BusinesLayer
Public Class clsFolders

    Dim objGen As New clsEDICTGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objcab As New clsCabinet

    Private iFolId As Integer
    Private iFolCab As Integer
    Private sFolName As String
    Private sFolNotes As String
    Private iFolCrby As Integer
    Private sFolStatus As String
    Private sFolDelflag As String
    Private iFolUpdatedBy As Integer
    Private iFolDeletedBy As Integer
    Private iFOLCompID As Integer
    Private iEFPId As Integer
    Private sEFPPtype As String
    Private iEFPGPID As Integer
    Private iEFPUSRID As Integer
    Private iEFPOther As Integer
    Private iEFPFolId As Integer
    Private EFPArray As Array

    Private EFP_INDEX As Integer
    Private EFP_SEARCH As Integer
    Private EFP_MOD_FOLDER As Integer
    Private EFP_MOD_DOC As Integer
    Private EFP_DEL_FOLDER As Integer
    Private EFP_DEL_DOC As Integer
    Private EFP_EXPORT As Integer
    Private EFP_CRT_DOC As Integer
    Private EFP_VIEW_FOL As Integer
    Private cLvlType As Char


    Dim PermDt As DataTable
    Dim dtGrp As DataTable
    Dim sMem As String = String.Empty
    Dim sFolPerm As String
    Dim iUsrType As Integer
    Dim iUsrParGrp As Integer = 0
    Dim iParGrp As Int32
    Dim sCabName As String
    Dim dtPerm As New DataTable
    Dim sPermLvl As String


    Dim objDb As New DBHelper

    Public Structure SrtFolPer
        Dim cLvlType As Char
        Dim iFolId As Integer
        Dim iUsrId As Integer
        Dim iGrpId As Int16
        Dim iModFol As Int16
        Dim iDelFol As Int16
        Dim iViewFol As Int16
        Dim iCrtDoc As Int16
        Dim iDelDoc As Int16
        Dim iModDoc As Int16
        Dim iIndex As Integer
        Dim iSearch As Integer
        Dim iExport As Int16
        Dim iOther As Int16
        Dim iCabId As Integer
    End Structure

    Public Property iEFP_Id() As Integer
        Get
            Return (iEFPId)
        End Get
        Set(ByVal Value As Integer)
            iEFPId = Value
        End Set
    End Property
    Public Property sEFP_Ptype() As String
        Get
            Return (sEFPPtype)
        End Get
        Set(ByVal Value As String)
            sEFPPtype = Value
        End Set
    End Property
    Public Property iEFP_GPID() As Integer
        Get
            Return (iEFPGPID)
        End Get
        Set(ByVal Value As Integer)
            iEFPGPID = Value
        End Set
    End Property
    Public Property iEFP_USRID() As Integer
        Get
            Return (iEFPUSRID)
        End Get
        Set(ByVal Value As Integer)
            iEFPUSRID = Value
        End Set
    End Property
    Public Property iEFP_Other() As Integer
        Get
            Return (iEFPOther)
        End Get
        Set(ByVal Value As Integer)
            iEFPOther = Value
        End Set
    End Property
    Public Property iEFP_FolId() As Integer
        Get
            Return (iEFPFolId)
        End Get
        Set(ByVal Value As Integer)
            iEFPFolId = Value
        End Set
    End Property
    Public Property EFP_Array() As Array
        Get
            Return (EFPArray)
        End Get
        Set(ByVal Value As Array)
            EFPArray = Value
        End Set
    End Property
    Public Property iFol_Id() As Integer
        Get
            Return (iFolId)
        End Get
        Set(ByVal Value As Integer)
            iFolId = Value
        End Set
    End Property
    Public Property iFol_Cab() As Integer
        Get
            Return (iFolCab)
        End Get
        Set(ByVal Value As Integer)
            iFolCab = Value
        End Set
    End Property
    Public Property sFol_Name() As String
        Get
            Return (sFolName)
        End Get
        Set(ByVal Value As String)
            sFolName = Value
        End Set
    End Property
    Public Property sFol_Notes() As String
        Get
            Return (sFolNotes)
        End Get
        Set(ByVal Value As String)
            sFolNotes = Value
        End Set
    End Property
    Public Property iFol_Crby() As Integer
        Get
            Return (iFolCrby)
        End Get
        Set(ByVal Value As Integer)
            iFolCrby = Value
        End Set
    End Property
    Public Property sFol_Status() As String
        Get
            Return (sFolStatus)
        End Get
        Set(ByVal Value As String)
            sFolStatus = Value
        End Set
    End Property
    Public Property sFol_Delflag() As String
        Get
            Return (sFolDelflag)
        End Get
        Set(ByVal Value As String)
            sFolDelflag = Value
        End Set
    End Property
    Public Property iFol_UpdatedBy() As Integer
        Get
            Return (iFolUpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iFolUpdatedBy = Value
        End Set
    End Property
    Public Property iFol_DeletedBy() As Integer
        Get
            Return (iFolDeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iFolDeletedBy = Value
        End Set
    End Property
    Public Property iFol_CompId() As Integer
        Get
            Return (iFOLCompID)
        End Get
        Set(ByVal Value As Integer)
            iFOLCompID = Value
        End Set
    End Property
    Public Property iEFP_Index() As Integer
        Get
            Return (EFP_INDEX)
        End Get
        Set(ByVal Value As Integer)
            EFP_INDEX = Value
        End Set
    End Property
    Public Property iEFP_Search() As Integer
        Get
            Return (EFP_SEARCH)
        End Get
        Set(ByVal Value As Integer)
            EFP_SEARCH = Value
        End Set
    End Property
    Public Property iEFP_Mod_Doc() As Integer
        Get
            Return (EFP_MOD_DOC)
        End Get
        Set(ByVal Value As Integer)
            EFP_MOD_DOC = Value
        End Set
    End Property
    Public Property iEFP_Mod_Folder() As Integer
        Get
            Return (EFP_MOD_FOLDER)
        End Get
        Set(ByVal Value As Integer)
            EFP_MOD_FOLDER = Value
        End Set
    End Property
    Public Property iEFP_Del_Folder() As Integer
        Get
            Return (EFP_DEL_FOLDER)
        End Get
        Set(ByVal Value As Integer)
            EFP_DEL_FOLDER = Value
        End Set
    End Property
    Public Property iEFP_Del_Doc() As Integer
        Get
            Return (EFP_DEL_DOC)
        End Get
        Set(ByVal Value As Integer)
            EFP_DEL_DOC = Value
        End Set
    End Property
    Public Property iEFP_Export() As Integer
        Get
            Return (EFP_EXPORT)
        End Get
        Set(ByVal Value As Integer)
            EFP_EXPORT = Value
        End Set
    End Property
    Public Property iEFP_Crt_Doc() As Integer
        Get
            Return (EFP_CRT_DOC)
        End Get
        Set(ByVal Value As Integer)
            EFP_CRT_DOC = Value
        End Set
    End Property
    Public Property iEFP_View_Fol() As Integer
        Get
            Return (EFP_VIEW_FOL)
        End Get
        Set(ByVal Value As Integer)
            EFP_VIEW_FOL = Value
        End Set
    End Property

    Public Function LoadSubCab(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParent As Integer) As DataSet
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select CBN_ID,CBN_NAME from edt_cabinet "
            sSql = sSql & " where CBN_PARENT =" & iParent & " and CBN_DelFlag ='A' and CBN_CompID =" & iCompID & " order by cbn_name"
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFolderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iFolId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select fol_folid, FOL_NAME, FOL_NOTE from edt_FOLDER where FOL_FOLID=" & iFolId & " and FOL_CompID =" & iCompID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadFolders(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCabId As Integer, ByVal userid As Integer, Optional ByVal sPerm As String = "VFD") As DataTable
        Dim dsFol As DataSet
        Dim dRow As DataRow
        Dim strsql As String
        Dim iRet
        Dim Grpdv As DataView
        Try
            'Modified by Badari.G On 18-4-2007
            'MyDt.Select  
            dtGrp = BuildGroupDt(sNameSpace)
            Grpdv = dtGrp.DefaultView

            PermDt = BuildFolderTable()
            sMem = objcab.GetMemberGroups(sNameSpace, userid)
            sFolPerm = GetPermFolders(sNameSpace, userid, sMem)
            iUsrType = objcab.GetUserType(sNameSpace, userid)
            iUsrParGrp = objcab.GetUserParGrp(sNameSpace, userid)
            ' GeneralInfo.UserId = LogUsrId
            If (iUsrType = 1) Then
                'strsql = "Select * from View_FolCab where (Fol_Status like 'A%' or Fol_Status='U') and Fol_Cabinet=" & iCabId & ""
                strsql = "Select * from View_FolCab where Fol_DelFlag='A' and Fol_Cabinet=" & iCabId & ""
                dsFol = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                If (dsFol.Tables(0).Rows.Count > 0) Then
                    For Each dRow In dsFol.Tables(0).Rows
                        iParGrp = dRow("CBN_Department")
                        sCabName = dRow("CBN_Name")
                        iRet = GetFinalFolPermissions(dRow("Fol_FolId"), userid, sNameSpace, sPerm)
                        If (iRet = 1) Then
                            AddNewPermissions(dtPerm, sPermLvl, sNameSpace, Grpdv, userid)
                        End If
                    Next
                End If
                Return PermDt
                Exit Function
            End If
            strsql = "Select * from View_FolCab where Fol_DelFlag='A' and CBN_Department in (" & sMem & ") and  Fol_cabinet=" & iCabId & ""
            If Val(sFolPerm) <> 0 Then
                strsql = strsql & " and Fol_FolId Not in (" & sFolPerm & ")"
            End If
            dsFol = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            If (dsFol.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsFol.Tables(0).Rows
                    'iParGrp = GetParGrpID(dRow("Fol_FolID"), sConStr, sRDBMS)
                    iParGrp = dRow("CBN_Department")
                    sCabName = dRow("CBN_Name")
                    'GeneralInfo.UserId = LogUsrId
                    iRet = GetFinalFolPermissions(dRow("Fol_FolId"), userid, sNameSpace, sPerm)
                    If (iRet = 1) Then
                        AddPermissions(dtPerm, sPermLvl, sNameSpace, Grpdv, userid)
                    End If
                Next
            End If
            strsql = "Select *  from View_FolCab where Fol_DelFlag='A' and Fol_Cabinet=" & iCabId & ""

            If Len((sFolPerm)) <> 0 Then
                strsql = strsql & " and Fol_FolId in (" & sFolPerm & ")"
            Else
                strsql = strsql & " and Fol_FolId in (0)"
            End If

            dsFol = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            If (dsFol.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsFol.Tables(0).Rows
                    iParGrp = dRow("CBN_Department")
                    sCabName = dRow("CBN_Name")
                    iRet = GetFinalFolPermissions(dRow("Fol_FolID"), userid, sNameSpace, sPerm, 1)
                    If (iRet = 1) Then
                        AddPermissions(dtPerm, sPermLvl, sNameSpace, Grpdv, userid)
                    End If
                Next
            End If
            Return PermDt

        Catch ex As Exception
            Throw
        End Try

        'Dim dtDisplay As New DataTable
        'Dim iID As Integer
        'Dim dRow As DataRow
        'Dim sSql As String = "", aSql As String = "", pSql As String = ""
        'Dim dr As OleDb.OleDbDataReader
        'Dim dsFol As DataSet

        'dtDisplay.Columns.Add("FOL_FOLID")
        'dtDisplay.Columns.Add("FOL_NAME")
        'dtDisplay.Columns.Add("CBN_Name")
        'dtDisplay.Columns.Add("PGE_DETAILS_ID")
        'dtDisplay.Columns.Add("Org_Name")
        'dtDisplay.Columns.Add("FOL_CreatedBy")
        'dtDisplay.Columns.Add("FOL_CreatedOn")
        'dtDisplay.Columns.Add("FOL_Delflag")
        'dtDisplay.Columns.Add("PGE_CABINET")
        'dtDisplay.Columns.Add("PGE_FOLDER")

        'Try
        'sSql = "" : sSql = "Select Distinct(a.FOL_FOLID),a.FOL_NAME,a.FOL_CABINET,a.FOL_CreatedOn,a.FOL_CreatedBy,a.FOL_Delflag,b.Org_name,c.CBN_Name, d.PGE_CABINET, d.PGE_FOLDER from EDT_FOLDER a "
        'sSql = sSql & " LEFT JOIN edt_cabinet c On a.FOL_CABINET=c.CBN_ID  "
        'sSql = sSql & " Left Join edt_page d On  d.PGE_SubCabinet = c.CBN_ID " Vijeth
        'sSql = sSql & " Left Join edt_page d On  d.PGE_FOLDER = a.FOL_FOLID "
        'sSql = sSql & " Left Join Sad_Org_Structure b On b.Org_Node = c.cbn_Department "
        'sSql = sSql & " where a.FOL_CABINET=" & iSubCBN_Id & ""

        '    sSql = sSql & " order by a.FOL_NAME"
        '    dr = objDb.SQLDataReader(sNameSpace, sSql)
        '    dsFol = objDb.SQLExecuteDataSet(sNameSpace, sSql)
        '    If (dsFol.Tables(0).Rows.Count > 0) Then
        '        For Each dRow In dsFol.Tables(0).Rows
        '            If dr.HasRows Then
        '                While dr.Read
        '                    aSql = "Select Count(PGE_Details_ID) from edt_page  where PGE_Folder='" & dr("FOL_FOLID") & "'"
        '                    iID = objDb.SQLExecuteScalarInt(sNameSpace, aSql)
        '                    dRow = dtDisplay.NewRow
        '                    dRow("FOL_FOLID") = dr("FOL_FOLID")
        '                    If IsDBNull(dr("FOL_NAME")) = False Then
        '                        dRow("FOL_NAME") = dr("FOL_NAME")
        '                    End If
        '                    If IsDBNull(dr("CBN_Name")) = False Then
        '                        dRow("CBN_Name") = dr("CBN_Name")
        '                    End If
        '                    If iID > 0 Then
        '                        dRow("PGE_DETAILS_ID") = iID
        '                    Else
        '                        dRow("PGE_DETAILS_ID") = 0
        '                    End If
        '                    If IsDBNull(dr("Org_Name")) = False Then
        '                        dRow("Org_Name") = dr("Org_Name")
        '                    End If
        '                    If IsDBNull(dr("FOL_CreatedBy")) = False Then
        '                        dRow("FOL_CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dr("FOL_CreatedBy").ToString())
        '                    End If

        '                    If IsDBNull(dr("FOL_CreatedOn")) = False Then
        '                        dRow("FOL_CreatedOn") = objGen.FormatDtForRDBMS(dr("FOL_CreatedOn"), "D")
        '                    End If
        '                    If IsDBNull(dr("FOL_Delflag")) = False Then
        '                        If dr("FOL_Delflag") = "A" Then
        '                            dRow("FOL_Delflag") = "Activated"
        '                        ElseIf dr("FOL_Delflag") = "D" Then
        '                            dRow("FOL_Delflag") = "De-Activated"
        '                        ElseIf dr("FOL_Delflag") = "W" Then
        '                            dRow("FOL_Delflag") = "Waiting for Approval"
        '                        End If
        '                    End If
        '                    If IsDBNull(dr("PGE_CABINET")) = False Then
        '                        dRow("PGE_CABINET") = dr("PGE_CABINET")
        '                    End If
        '                    If IsDBNull(dr("PGE_FOLDER")) = False Then
        '                        dRow("PGE_FOLDER") = dr("PGE_FOLDER")
        '                    End If
        '                    dtDisplay.Rows.Add(dRow)
        '                End While
        '            End If
        '        Next
        '    End If
        '    Return dtDisplay
        'Catch ex As Exception
        '    Throw
        'End Try
    End Function
    Public Sub UpdateStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatus As String, ByVal ifol_id As String, ByVal sDStatus As String, ByVal iUsrId As Integer)
        Dim sSql As String
        Try
            sSql = "Update edt_Folder set "
            If sStatus = "D" Then
                sSql = sSql & " FOL_Delflag='" & sDStatus & "', Fol_DeletedBy=" & iUsrId & ", Fol_DeletedOn=Getdate(), Fol_Status='AD'"
            ElseIf sStatus = "A" Then
                sSql = sSql & " FOL_Delflag='" & sDStatus & "', Fol_RecalledBy=" & iUsrId & ", Fol_RecalledOn=Getdate(), Fol_Status='AR'"
            ElseIf sStatus = "W" Then
                sSql = sSql & " FOL_Delflag='" & sDStatus & "', Fol_ApprovedBy=" & iUsrId & ", Fol_ApprovedOn=Getdate(), Fol_Status='A'"
            ElseIf sStatus = "AV" Then
                sSql = sSql & " FOL_Delflag='" & sDStatus & "', FOL_UpdatedBy=" & iUsrId & ", FOL_UpdatedOn=Getdate(), Fol_Status='AV'" 'manish
            End If
            sSql = sSql & " Where FOL_folid = " & ifol_id & " and FOL_CompID =" & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function CheckFoldersName(ByVal sAC As String, ByVal iCompID As Integer, ByVal sFolName As String, ByVal iCabID As Integer, ByVal iFolID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select FOL_NAME from edt_folder where FOL_NAME='" & sFolName & "' and FOL_CABINET=" & iCabID & " and "
            sSql = sSql & "FOL_FOLID <>" & iFolID & " and (FOL_Delflag='A'  or FOL_Delflag='W') and FOL_CompID = " & iCompID & " "
            Return objDb.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFolderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objFoldr As clsFolders) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_FolId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Name", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFoldr.sFol_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Note", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFoldr.sFol_Notes
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Cab
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Crby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objFoldr.sFol_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Delflag", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objFoldr.sFol_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "SaveOrUpFolderDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteFolPermission(ByVal sNameSpace As String, ByVal iEFP_GrpID As Integer, ByVal iEFP_USRID As Integer, ByVal iEFP_FolID As Integer)
        Dim sSql As String = ""
        Try

            sSql = "" : sSql = "Delete edt_Folder_Permission where  EFP_GrpID=" & iEFP_GrpID & "  and EFP_FolID=" & iEFP_FolID & " "
            If iEFP_USRID > 0 Then
                sSql = sSql & "and EFP_USRID=" & iEFP_USRID & ""
            End If
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Function LoadPermission(ByVal sNameSpace As String, ByVal iEFP_GrpID As Integer, ByVal iEFP_USRID As Integer, ByVal iEFP_FolID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim PermDt As DataTable
    '    Dim i As Int16
    '    Dim ht As Hashtable
    '    Dim objFolDis As New clsFolders
    '    Dim sLevel As String
    '    Dim sPermission As String = ""
    '    Dim sArray() As String
    '    Dim objColl As System.Collections.IDictionaryEnumerator
    '    Dim Keys As System.Collections.ICollection
    '    Try
    '        If (iEFP_USRID <> 0) Then
    '            If (iEFP_FolID = 0) Then
    '                ht = New Hashtable
    '                ht = GetFinalFolPermissions(iEFP_FolID, iEFP_USRID, sNameSpace, "ALL", 2)
    '                'PermDt = objClsCab.RetrievePermissions(lblMsg.Text, iGrpId, iUsrId)
    '            Else
    '                'PermDt = objClsCab.RetrievePermissions(iCabId, iGrpId, iUsrId)
    '                ht = New Hashtable
    '                ht = GetFinalFolPermissions(iEFP_FolID, iEFP_USRID, sNameSpace, "ALL", 2)
    '            End If
    '            If ht Is Nothing Then
    '                Exit Function
    '            End If
    '            'Assign the Level of Permission to the Label
    '            sLevel = ht("Level")
    '            Select Case UCase(sLevel)
    '                'Case "PG"
    '                '    lblPerm.Text = "Group Level Permissions"
    '                'Case "GH"
    '                '    lblPerm.Text = "Group Level Permissions"
    '                'Case "PU"
    '                '    lblPerm.Text = "POWER USER"
    '                'Case "G"
    '                '    lblPerm.Text = "Group Level Permissions"
    '                'Case "U"
    '                '    lblPerm.Text = "User Level Permissions"
    '                'Case "E"
    '                '    lblPerm.Text = "Permissions given to EveryOne"
    '            End Select
    '            objColl = ht.GetEnumerator()
    '            objColl.Reset()

    '            Keys = ht.Keys
    '            For i = 0 To Keys.Count - 1
    '                objColl.MoveNext()
    '                Select Case UCase(objColl.Key.ToString)

    '                    Case "FMODIFY"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If
    '                    Case "FDELETE"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If
    '                    Case "FVIEW"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If

    '                    Case "DMODIFY"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If
    '                    Case "DDELETE"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If
    '                    Case "DCREATE"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If


    '                    Case "INDEX"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If
    '                    Case "SEARCH"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If
    '                    Case "EXPORT"
    '                        If (objColl.Value = 1) Then
    '                            sPermission = sPermission & "1"
    '                        Else
    '                            sPermission = sPermission & "0"
    '                        End If


    '                End Select
    '            Next
    '        Else
    '            If (iEFP_FolID = 0) Then
    '                PermDt = RetrievePermissions(sNameSpace, iEFP_FolID, iEFP_GrpID, iEFP_USRID)
    '            Else
    '                PermDt = RetrievePermissions(sNameSpace, iEFP_FolID, iEFP_GrpID, iEFP_USRID)
    '            End If
    '            If PermDt.Rows.Count > 0 Then
    '                For i = 0 To PermDt.Rows.Count - 1
    '                    Select Case PermDt.Rows(i).Item("PerName")
    '                        Case "MFD"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If

    '                        Case "DFD"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If
    '                        Case "VFD"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If


    '                        Case "MDC"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If
    '                        Case "DDC"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If
    '                        Case "CDC"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If


    '                        Case "SRH"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If
    '                        Case "IND"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If
    '                        Case "EXP"
    '                            If (PermDt.Rows(i).Item("PerValue") = 1) Then
    '                                sPermission = sPermission & "," & "1"
    '                            Else
    '                                sPermission = sPermission & "," & "0"
    '                            End If
    '                    End Select
    '                Next
    '                sArray = sPermission.Split(",")
    '                For i = 0 To chkPermission.Items.Count - 1
    '                    If sArray(i) = 1 Then
    '                        chkPermission.Items(i).Selected = True
    '                    Else
    '                        chkPermission.Items(i).Selected = False
    '                    End If
    '                Next
    '            Else
    '                lblPrmError.Text = "No Permissions Assigned"
    '            For i = 0 To chkPermission.Items.Count - 1
    '                chkPermission.Items(i).Selected = False
    '            Next
    '        End If
    '        End If
    '        'sSql = "Select * from edt_Folder_Permission where EFP_GrpID='" & iEFP_GrpID & "' and EFP_USRID='" & iEFP_USRID & "' and EFP_FolID='" & iEFP_FolID & "' "
    '        'Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SavePermission(ByVal sNameSpace As String, ByVal objFoldr As clsFolders, ByVal SArray As Array) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_PTYPE", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objFoldr.sEFP_Ptype
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_GRPID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_GPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_USRID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_USRID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(3) 'vijaylakshmi SArray(1)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(4) 'vijaylakshmi SArray(2)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_VIEW_FOL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(5)  'vijaylakshmi SArray(3)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0  'vijeth SArray(4)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0  'vijeth SArray(5)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_CRT_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0  'vijeth SArray(6)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(2) 'Vijeth SArray(7) 'vijaylakshmi SArray(4)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(1) 'Vijeth SArray(8) 'vijaylakshmi SArray(5)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_EXPORT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0  'vijeth SArray(9)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_Other
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_FOlId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_FolId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "InOrUpFolPermissions", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateFolderCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCBN_NODE As Integer, ByVal iSCBN_NODE As Integer)
        Dim aSql As String = ""
        Try
            'Update folder count to Cabinet
            aSql = "" : aSql = "Update edt_cabinet set CBN_FolderCount=(select count(Fol_folid) from edt_folder where fol_cabinet in (Select CBN_id from Edt_Cabinet "
            aSql = aSql & "where CBN_Parent=" & iCBN_NODE & " And (CBN_DelFlag='A'))) where CBN_ID =" & iCBN_NODE & " and CBN_CompID = " & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, aSql)

            'Update folder count to Sub-Cabinet
            aSql = "" : aSql = "Update edt_cabinet set CBN_FolderCount=(select Count(Fol_folid) from edt_folder where fol_cabinet=" & iSCBN_NODE & " and "
            aSql = aSql & "(FOL_Delflag='A')) where cbn_ID=" & iSCBN_NODE & " and CBN_CompID = " & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, aSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveFolPermissions(ByVal objSrt As SrtFolPer, ByVal sNameSpace As String) As String

        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParCount As Int16
        Try

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_PTYPE", OleDb.OleDbType.VarChar)
            objParam(iParCount).Value = objSrt.cLvlType
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_FolId", OleDb.OleDbType.Numeric)
            objParam(iParCount).Value = objSrt.iFolId
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_GRPID", OleDb.OleDbType.Numeric)
            objParam(iParCount).Value = objSrt.iGrpId
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_USRID", OleDb.OleDbType.Numeric)
            objParam(iParCount).Value = objSrt.iUsrId
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_INDEX", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iIndex
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_Search", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iSearch
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_Mod_Folder", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iModFol
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_MOD_Doc", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iModDoc
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_DEL_FOLDER", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iDelFol
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_DEL_Doc", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iDelDoc
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_Export", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iExport
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_OTHER", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iOther
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_CRT_Doc", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iCrtDoc
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@EFP_View_Fol", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iViewFol
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1


            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_PTYPE", OleDb.OleDbType.VarChar)
            'objParam(iParCount).Value = cLvlType
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_FolId", OleDb.OleDbType.Numeric)
            'objParam(iParCount).Value = iFolId
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_GRPID", OleDb.OleDbType.Numeric)
            'objParam(iParCount).Value = iEFP_GPID
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_USRID", OleDb.OleDbType.Numeric)
            'objParam(iParCount).Value = userid
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_INDEX", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Index
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_Search", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Search
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_Mod_Folder", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Mod_Folder
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_MOD_Doc", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Mod_Doc
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_DEL_FOLDER", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Del_Folder
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_DEL_Doc", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Del_Doc
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_Export", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Export
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_OTHER", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Other
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_CRT_Doc", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_Crt_Doc
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            'objParam(iParCount) = New OleDb.OleDbParameter("@EFP_View_Fol", OleDb.OleDbType.SmallInt)
            'objParam(iParCount).Value = iEFP_View_Fol
            'objParam(iParCount).Direction = ParameterDirection.Input
            'iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Direction = ParameterDirection.Output

            iParCount = objDb.ExecuteSPForInsert(sNameSpace, "InOrUpFolPermissionsPtype", "@iOper", objParam)
            Return iParCount
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function BuildFolderTable() As DataTable
        Dim LocalDt As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("Fol_FolID", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_Name", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_Note", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_Cabinet", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_SubCab", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_CreatedOn", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_CreatedBy", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_Group", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_CabId", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("PageCount", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Fol_DelFlag", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("CBN_NAme", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("CBN_Department", GetType(String))
            LocalDt.Columns.Add(dc)

            dc = New DataColumn("PGE_CABINET", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("PGE_FOLDER", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("PGE_DETAILS_ID", GetType(String))
            LocalDt.Columns.Add(dc)


            'Permissions
            dc = New DataColumn("PLevel", GetType(String))
            LocalDt.Columns.Add(dc)

            Return LocalDt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetPermFolders(ByVal sNameSpace As String, ByVal iUserID As String, ByVal sGrpID As String) As String
        Dim strsql As String
        'Dim objDB As New DBGeneral(GetKeyValues("ConnectionString"), GetKeyValues("sRDBMS"))
        Dim Arr() As String
        Dim i As Integer
        Dim sFolId As String = ""
        Dim sRet As String
        Dim sFArr() As String
        Try
            Arr = Split(sGrpID, ",")
            For i = 0 To UBound(Arr)
                strsql = "edt_Folder_permission where EFP_Grpid = " & Arr(i) & " and (EFP_UsrId=" & iUserID & " or EFP_UsrId=0)"
                sRet = objDb.GetAllValues(sNameSpace, "EFP_FolId", strsql)
                'sCabId = sCabId & objDB.GetAllValues("CBP_CabId", strsql)
                If Val(sRet) <> 0 Then
                    'If InStr(sRet, ";") <> 0 Then
                    If Right(sRet, 1) = ";" Then
                        sRet = Left(sRet, Len(sRet) - 1)
                    End If
                    sFolId = sFolId & ";" & sRet & ";"
                End If
            Next
            strsql = "Edt_Folder_permission where EFP_ptype = 'E'"
            sFolId = sFolId & objDb.GetAllValues(sNameSpace, "EFP_FolId", strsql)
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
    Private Function BuildGroupDt(ByVal sNameSpace As String) As DataTable
        Dim ds As DataSet
        Dim strsql As String
        Try
            strsql = "select Org_node,Org_Name from sad_org_structure where Org_DelFlag= 'A' "
            ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            Return ds.Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetFinalFolPermissions(ByVal iFolId As Integer, ByVal iUserId As Int16, ByVal sNameSpace As String, Optional ByVal sPerType As String = "ALL", Optional ByVal ichkType As Int16 = 0) As Object
        'Dim objDB As New DBGeneral(sConstr, sRDBMS)
        'Dim dsMain As DataSet
        'Dim Ht As New Hashtable
        'Dim iParGrp As Integer
        Try
            'First Get the Parent GroupId of the Cabinet
            If (ichkType = 2) Then
                iParGrp = GetParGrpID(iFolId, sNameSpace)
                iUsrParGrp = GetUserParGrp(sNameSpace, iUserId)
            End If
            dtPerm = GetMainPermDS(iFolId, iUserId, iParGrp, sNameSpace, ichkType)
            If (dtPerm.Rows.Count > 0) Then
                Select Case UCase(sPerType)
                    Case "ALL"
                        Dim HT As New Hashtable
                        ' dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                HT.Add("FModify", 0)
                                HT.Add("FView", 1)
                                HT.Add("FDelete", 0)
                                'HT.Add("DDelete", 0)
                                'HT.Add("DModify", 0)
                                'HT.Add("DCreate", 0)
                                HT.Add("Index", 1)
                                HT.Add("Search", 1)
                                'HT.Add("Export", 0)
                                HT.Add("Level", sPermLvl)
                            Else
                                HT.Add("FModify", 0)
                                HT.Add("FView", 1)
                                HT.Add("FDelete", 0)
                                'HT.Add("DDelete", 0)
                                'HT.Add("DModify", 0)
                                'HT.Add("DCreate", 0)
                                HT.Add("Index", 0)
                                HT.Add("Search", 1)
                                'HT.Add("Export", 0)
                                HT.Add("Level", sPermLvl)
                            End If

                            Return HT
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            HT.Add("FModify", 1)
                            HT.Add("FView", 1)
                            HT.Add("FDelete", 1)
                            'HT.Add("DDelete", 1)
                            'HT.Add("DModify", 1)
                            'HT.Add("DCreate", 1)
                            HT.Add("Index", 1)
                            HT.Add("Search", 1)
                            'HT.Add("Export", 1)
                            HT.Add("Level", sPermLvl)
                            Return HT
                        End If
                        HT.Add("FModify", dtPerm.Rows(0).Item("EFP_MOD_FOLDER"))
                        HT.Add("FView", dtPerm.Rows(0).Item("EFP_VIEW_Fol"))
                        HT.Add("FDelete", dtPerm.Rows(0).Item("EFP_DEL_FOLDER"))
                        'HT.Add("DDelete", dtPerm.Rows(0).Item("EFP_DEL_DOC"))
                        'HT.Add("DModify", dtPerm.Rows(0).Item("EFP_MOD_DOC"))
                        'HT.Add("DCreate", dtPerm.Rows(0).Item("EFP_CRT_DOC"))
                        HT.Add("Index", dtPerm.Rows(0).Item("EFP_INDEX"))
                        HT.Add("Search", dtPerm.Rows(0).Item("EFP_SEARCH"))
                        'HT.Add("Export", dtPerm.Rows(0).Item("EFP_EXPORT"))
                        HT.Add("Level", sPermLvl)
                        Return HT
                    Case "MFD"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_MOD_FOLDER")
                    Case "DFD"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_DEL_FOLDER")
                    Case "VFD"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 1
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_VIEW_Fol")
                    Case "DDM"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_DEL_DOC")
                    Case "MDM"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_MOD_DOC")
                    Case "IDX"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Return 1
                            Else
                                Return 0
                            End If
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_INDEX")
                    Case "SRH"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 1
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_SEARCH")
                    Case "EXP"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH" Or sPermLvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EFP_EXPORT")
                End Select
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function AddNewPermissions(ByVal dtperm As DataTable, ByVal PLevel As String, ByVal sNameSpace As String, ByVal Grpdv As DataView, ByVal iUsrId As Integer)
        Dim dsRow, dtRow As DataRow
        Dim aSql As String, asql1 As String
        Dim ds1 As DataSet
        Dim iID As Integer
        'Dim iRet
        'Dim dv As DataView
        'Dim dr() As DataRow
        Dim depname As String
        Try
            dsRow = dtperm.Rows(0)
            dtRow = PermDt.NewRow
            dtRow("PLevel") = PLevel
            dtRow("Fol_FolID") = dsRow("Fol_FOlID")
            dtRow("Fol_Name") = dsRow("Fol_Name")
            dtRow("Fol_Note") = dsRow("Fol_Note")
            dtRow("Fol_CreatedOn") = dsRow("Fol_CreatedOn")
            dtRow("Fol_CreatedBy") = objcab.getcreatedbyname(sNameSpace, dsRow("Fol_Createdby"))
            'dtRow("Fol_CreatedBy") = dsRow("Fol_Createdby")
            dtRow("Fol_CabId") = dsRow("Fol_Cabinet")
            ' dtRow("PageCount") = dsRow("Fol_PageCount")
            dtRow("CBN_Name") = dsRow("CBN_Name")
            dtRow("PageCount") = checkFolderPermission(dsRow("Fol_FOlID"), iUsrId, sNameSpace)
            dtRow("Fol_Cabinet") = sCabName 'GetCabName(dsRow("Fol_Cabinet"))
            'iRet = GetParGrpID(dsRow("Fol_FolID"), GetKeyValues("ConnectionString"), GetKeyValues("sRDBMS"))
            Grpdv.RowFilter = "Org_Node = " & iParGrp
            'dr = dtGrp.Select("Mas_ID =" & iParGrp)
            dtRow("Fol_Group") = Grpdv.Item(0).Item(1) 'GetGroupName(iParGrp, sConstr, sRDBMS)

            aSql = "Select Count(PGE_Details_ID) from edt_page  where PGE_Folder='" & dtRow("FOL_FOLID") & "'"
            iID = objDb.SQLExecuteScalarInt(sNameSpace, aSql)
            If iID > 0 Then
                dtRow("PGE_DETAILS_ID") = iID
            Else
                dtRow("PGE_DETAILS_ID") = 0
            End If
            'asql1 = "Select * from edt_page  where PGE_Folder='" & dtRow("FOL_FOLID") & "'"
            'ds1 = objDb.SQLExecuteDataSet(sNameSpace, asql1)
            'If ds1.Tables(0).Rows.Count <> 0 Then
            '    dtRow("PGE_CABINET") = ds1.Tables(0).Rows("PGE_CABINET")
            'Else
            '    dtRow("PGE_CABINET") = 0
            'End If
            'If ds1.Tables(0).Rows.Count <> 0 Then
            '    dtRow("PGE_FOLDER") = ds1.Tables(0).Rows("PGE_FOLDER")
            'Else
            '    dtRow("PGE_FOLDER") = 0
            'End If
            'dtRow("FolCab") = ""
            'dtRow("FolGroup") = ""
            'dtRow("FolGroup") = dr(0).Item("Mas_Description")
            dtRow("Fol_DelFlag") = dsRow("Fol_DelFlag")
            If dtRow("Fol_DelFlag") = "A" Then
                dsRow("Fol_DelFlag") = "Activated"
            ElseIf dtRow("Fol_DelFlag") = "D" Then
                dsRow("Fol_DelFlag") = "De-Activated"
            ElseIf dtRow("Fol_DelFlag") = "W" Then
                dsRow("Fol_DelFlag") = "Waiting for Approval"
            End If
            dtRow("PGE_CABINET") = getCabinetID(sNameSpace, dsRow("Fol_Cabinet"))
            dtRow("Fol_DelFlag") = dsRow("Fol_DelFlag")
            depname = objcab.GetGroupName(dsRow("CBN_Department"), sNameSpace)
            dtRow("CBN_Department") = depname
            PermDt.Rows.Add(dtRow)
            'Return PermDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function AddPermissions(ByVal dtperm As DataTable, ByVal PLevel As String, ByVal sNameSpace As String, ByVal Grpdv As DataView, ByVal userid As Integer)
        Dim dsRow, dtRow As DataRow
        'Dim iRet
        'Dim dv As DataView
        'Dim dr() As DataRow
        Dim depname As String
        Try
            dsRow = dtperm.Rows(0)
            dtRow = PermDt.NewRow
            dtRow("PLevel") = PLevel
            dtRow("Fol_FolID") = dsRow("Fol_FOlID")
            dtRow("Fol_Name") = dsRow("Fol_Name")
            dtRow("Fol_Note") = dsRow("Fol_Note")
            dtRow("Fol_CreatedOn") = dsRow("Fol_CreatedOn")
            dtRow("Fol_CreatedBy") = objcab.getcreatedbyname(sNameSpace, dsRow("Fol_Createdby"))
            dtRow("Fol_CabId") = dsRow("Fol_Cabinet")
            ' dtRow("PageCount") = dsRow("Fol_PageCount")

            dtRow("PageCount") = checkFolderPermission(dsRow("Fol_FOlID"), userid, sNameSpace)
            dtRow("CBN_Name") = sCabName
            dtRow("Fol_Cabinet") = sCabName 'GetCabName(dsRow("Fol_Cabinet"))
            'iRet = GetParGrpID(dsRow("Fol_FolID"), GetKeyValues("ConnectionString"), GetKeyValues("sRDBMS"))
            Grpdv.RowFilter = "Org_Node = " & iParGrp
            'dr = dtGrp.Select("Mas_ID =" & iParGrp)
            dtRow("Fol_Group") = Grpdv.Item(0).Item(1) 'GetGroupName(iParGrp, sConstr, sRDBMS)
            'dtRow("FolCab") = ""
            'dtRow("FolGroup") = ""
            'dtRow("FolGroup") = dr(0).Item("Mas_Description")
            dtRow("PGE_CABINET") = getCabinetID(sNameSpace, dsRow("Fol_Cabinet"))
            dtRow("Fol_DelFlag") = dsRow("Fol_DelFlag")
            If dtRow("Fol_DelFlag") = "A" Then
                dsRow("Fol_DelFlag") = "Activated"
            ElseIf dtRow("Fol_DelFlag") = "U" Then
                dsRow("Fol_DelFlag") = "Activated"
            ElseIf dtRow("Fol_DelFlag") = "D" Then
                dsRow("Fol_DelFlag") = "De-Activated"
            ElseIf dtRow("Fol_DelFlag") = "W" Then
                dsRow("Fol_DelFlag") = "Waiting for Approval"
            End If
            dtRow("PGE_CABINET") = getCabinetID(sNameSpace, dsRow("Fol_Cabinet"))
            dtRow("Fol_DelFlag") = dsRow("Fol_DelFlag")
            Dim EDep As Integer = GetDepID(sNameSpace, dsRow("Fol_Cabinet"))
            depname = objcab.GetGroupName(EDep, sNameSpace)
            dtRow("CBN_Department") = depname
            PermDt.Rows.Add(dtRow)

            'Return PermDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function checkFolderPermission(ByVal iFolID As Integer, ByVal iUserID As Integer, ByVal sNameSpace As String) As String
        Dim i As Integer
        Dim sSql As String
        Dim dtFdoc As New DataTable
        Dim dsDoc, dsFDoc As DataSet
        Dim iCount As Integer = 0
        Try
            dtFdoc = BuildDocTable()
            dsDoc = objDb.SQLExecuteDataSet(sNameSpace, "Select distinct(PGE_Details_ID) from edt_page where PGE_Folder = " & iFolID & "")
            If dsDoc.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsDoc.Tables(0).Rows.Count - 1
                    sSql = " Select * from edt_page where PGE_Details_ID= " & dsDoc.Tables(0).Rows(i).Item("PGE_Details_ID") & " and PGE_Status='A'"
                    dsFDoc = objDb.SQLExecuteDataSet(sNameSpace, sSql)
                    If (dsFDoc.Tables(0).Rows.Count > 0) Then

                        dtFdoc = BuildFinalDocTable(dsFDoc.Tables(0).Rows(0), dtFdoc, iUserID, sNameSpace)
                    End If
                Next

                If dtFdoc.Rows.Count > 0 Then
                    iCount = dtFdoc.Rows.Count
                Else
                    iCount = 0
                End If
            End If
            Return iCount
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
    Public Function BuildFinalDocTable(ByVal drDoc As DataRow, ByVal dtFDoc As DataTable, ByVal iUserID As Integer, ByVal sNameSpace As String) As DataTable
        Dim sDoc, sDocDet() As String
        Dim drFDoc As DataRow
        Dim iRet
        Try

            iRet = CheckPermissions(drDoc("PGE_Folder"), drDoc("PGE_Document_Type"), iUserID, sNameSpace)
            If (iRet <> 0) Then
                drFDoc = dtFDoc.NewRow
                drFDoc("BaseName") = drDoc("PGE_BaseName")
                drFDoc("Title") = drDoc("PGE_Title") & "." & drDoc("PGE_Ext")
                drFDoc("CrOn") = drDoc("PGE_Date")
                drFDoc("PgeSize") = drDoc("PGE_Size")
                drFDoc("DetailsId") = drDoc("PGE_Details_Id")
                sDoc = RetPageDetails(sNameSpace, drDoc("PGE_BaseName"))
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
    Private Function CheckPermissions(ByVal iFol As Integer, ByVal iDocType As Integer, ByVal iUserID As Integer, ByVal sNameSpace As String) As Integer
        Dim objFol As New clsFolders
        Dim objDoc As New clsDocumentType
        Try
            If (objDoc.GetFinalDTPermissions(iDocType, iUserID, sNameSpace, "SRH") = 1) Then
                If (objFol.GetFinalFolPermissions(iFol, iUserID, sNameSpace, "SRH", 2) = 1) Then
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
    Public Function GetParGrpID(ByVal iFolId As Integer, ByVal sNameSpace As String) As Integer
        ' Dim objDB As New DBGeneral(sConStr, sRDBMS)
        Dim strsql As String
        Try
            strsql = "Select CBN_Department from View_FolCab where Fol_FolID=" & iFolId & ""
            Return objDb.SQLExecuteScalar(sNameSpace, strsql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserParGrp(ByVal sNameSpace As String, ByVal iLogUsrID As Integer) As Integer
        Dim strsql As String
        Try
            strsql = "Select USR_Levelcode from sad_Userdetails where usr_id=" & iLogUsrID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, strsql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetMainPermDS(ByVal iFolId As Integer, ByVal iUserId As Int16, ByVal iGrpId As Int16, ByVal sNameSpace As String, Optional ByVal iChkType As Int16 = 0) As DataTable
        'Dim objDB As New DBGeneral(sConstr, sRDBMS)
        Dim strsql As String
        Dim dsMain As DataSet
        Dim dtPerm As DataTable
        Dim iFlag As Integer = 0
        Try
            sPermLvl = String.Empty
            If (iChkType = 2) Then
                sMem = objcab.GetMemberGroups(sNameSpace, iUserId)
                iUsrType = objcab.GetUserType(sNameSpace, iUserId)
            End If
            If (iUsrType = 1) Then
                sPermLvl = "PU"
                strsql = "Select * from View_FolCab where Fol_DelFlag='A' and Fol_FolId=" & iFolId & ""
                dsMain = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            ElseIf (CheckForGrpHead(sNameSpace, iGrpId, iUserId) = 1) Then
                'Check For Group Head
                sPermLvl = "GH"
                strsql = "Select * from edt_folder where Fol_FolId=" & iFolId & ""
                dsMain = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            ElseIf (iChkType <> 1) Then
                'Check Cabinet Permissions from User Level
                'If (sFolPerm = String.Empty Or Val(sFolPerm) <> 0) Then
                'If (Val(sFolPerm) <> 0) Then
                '    sFolPerm = GetPermFolders(iUserId, sMem)
                'End If
                'Changed by badari on 9-05

                If (sFolPerm = String.Empty) Then
                    sFolPerm = GetPermFolders(sNameSpace, iUserId, sMem)
                End If
                'strsql = " Select *  from View_FolCab left outer join edt_folder_permission on Fol_FolId=EFP_FolId where  CBN_ParGrp in (" & sMem & ") and Fol_Status='A' and Fol_FolId= " & iFolId & " "
                'If Val(sFolPerm) <> 0 Then
                '    strsql = strsql & " and (Fol_FolID not in (" & sFolPerm & " ) or EFP_FolID is Null)  "
                'End If

                'strsql = "select CBN_ID,F.*,P.* from edt_cabinet C,edt_folder F, edt_folder_permission P where C.CBN_ParGrp in (" & sMem & ") and F.Fol_Status='A' and F.Fol_FolId= " & iFolId & " and C.CBN_ID = F.FOL_CABINET AND P.EFP_FOLID = F.FOL_FOLID"
                strsql = "Select * from edt_folder left outer join edt_folder_permission on Fol_FolID=EFP_FolID where " & iParGrp & "  in (" & sMem & ") and Fol_DelFlag='A' and Fol_FolId= " & iFolId & " "
                If Val(sFolPerm) <> 0 Then
                    strsql = strsql & " and (Fol_FolID not in (" & sFolPerm & " ) or EFP_FolID is Null)  "
                End If
                If objDb.DBCheckForRecord(sNameSpace, strsql) = True Then
                    dsMain = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                    sPermLvl = "PG"
                Else
                    GoTo LP
                End If
            Else
LP:             dsMain = BuildPermDataSet(iUserId, iFolId, sMem, iChkType, sNameSpace)
                If dsMain.Tables.Count <> 0 Then
                    dtPerm = dsMain.Tables(0)
                    dtPerm = GetFinalPermForDS(dtPerm, sNameSpace)
                    Return dtPerm
                Else
                    Dim MyDt As New DataTable
                    Return MyDt
                End If
            End If
            Return dsMain.Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForGrpHead(ByVal sNameSpace As String, ByVal iGrpId As Int16, ByVal iUsrId As Int16, Optional ByVal iCabID As Integer = 0) As Integer
        Dim strsql As String
        Try
            If (iGrpId <> 0) Then
                strsql = "Select Suo_isDeptHead from Sad_UsersInOtherDept where suo_userId=" & iUsrId & " and suo_deptId=" & iGrpId & ""
                Return (objDb.SQLExecuteScalar(sNameSpace, strsql))
            Else
                strsql = "Select Suo_isDeptHead from Sad_UsersInOtherDept where suo_userId=" & iUsrId & " and suo_deptId=(Select CBN_Department from edt_cabinet where CBN_id=" & iCabID & ")"
                Return (objDb.SQLExecuteScalar(sNameSpace, strsql))
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildPermDataSet(ByVal iUserId As Integer, ByVal iFolId As Integer, ByVal sMem As String, ByVal ChkType As Integer, ByVal sNameSpace As String) As Object
        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iCount As Integer
        Dim ssql, ssql1, ssql2 As String
        Dim ds, ds1, ds2 As DataSet
        Dim grp As Integer
        Dim MyDt As New DataSet
        Try
            ssql2 = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "'"
            ds2 = objDb.SQLExecuteDataSet(sNameSpace, ssql2)
            grp = ds2.Tables(0).Rows(0)("SUO_DeptID")

            ssql = "Select * from View_FolPermissions  where Fol_FolID='" & iFolId & "' and (EFP_USRID ='" & iUserId & "' or EFP_USRID =0)"
            ds = objDb.SQLExecuteDataSet(sNameSpace, ssql)
            If (ds.Tables(0).Rows.Count > 0) Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ((ds.Tables(0).Rows(i)("EFP_OTHER") = 1) And (ds.Tables(0).Rows(i)("EFP_PType") = "U")) = True Then
                        ssql1 = "select * from View_FolPermissions where Fol_FolID='" & iFolId & "' and EFP_USRID='" & iUserId & "' and EFP_OTHER=1 and EFP_PType='U'"
                        ds1 = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
                        Return ds1
                        Exit Function
                    ElseIf ((ds.Tables(0).Rows(i)("EFP_OTHER") = 0) And (ds.Tables(0).Rows(i)("EFP_PType") = "G")) = True Then
                        ssql1 = "select * from View_FolPermissions where Fol_FolID='" & iFolId & "' and EFP_GrpId in (select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "') and  EFP_USRID=0 and EFP_OTHER=0 and EFP_PType='G'"
                        ds1 = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
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
                                ds1 = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
                            End If
                        End If
                    End If
                Next
            Else
                Return MyDt
                Exit Function
            End If
            Return ds1

            'objParam(iCount) = New OleDb.OleDbParameter("@p_UsrId", OleDb.OleDbType.Numeric)
            'objParam(iCount).Value = iUserId
            'objParam(iCount).Direction = ParameterDirection.Input
            'iCount = iCount + 1

            'objParam(iCount) = New OleDb.OleDbParameter("@p_FolId", OleDb.OleDbType.Numeric)
            'objParam(iCount).Value = iFolId
            'objParam(iCount).Direction = ParameterDirection.Input
            'iCount = iCount + 1


            'objParam(iCount) = New OleDb.OleDbParameter("@p_Mem", OleDb.OleDbType.VarChar)
            'objParam(iCount).Value = sMem
            'objParam(iCount).Direction = ParameterDirection.Input
            'iCount = iCount + 1

            'objParam(iCount) = New OleDb.OleDbParameter("@p_iRetLvl", OleDb.OleDbType.VarChar)
            'objParam(iCount).Value = 0
            'objParam(iCount).Direction = ParameterDirection.Output
            'objParam(iCount).Size = 1
            'If (ChkType = 2) Then
            '    Dim arr() As Object = objDb.SPFrLoadingUsingDsParam(sNameSpace, "GetFolPerDetails", 1, "@p_iRetLvl", objParam)
            '    If (IsDBNull(arr(1)) = False) Then
            '        sPermLvl = arr(1)
            '    Else
            '        sPermLvl = ""
            '    End If
            '    Return arr(0)
            'Else
            '    Return (objDb.SPFrLoadingUsingDs(sNameSpace, "GetFolPerDetails", objParam))
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetFinalPermForDS(ByVal dtCab As DataTable, ByVal sNameSpace As String) As DataTable

        Dim dr As DataRow
        'Dim sGrp As String = ""
        Dim iMFD, iDFD, iVFD, iDDM, iMDM, iCDM, iIDX, iSRH, iEXP As Byte
        Try
            For Each dr In dtCab.Rows
                If (UCase(sPermLvl) <> "GH" And UCase(sPermLvl) <> "PG") Then
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

                    'sGrp = sGrp & "," & dr("CBP_GrpId")
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
    Private Function RetPageDetails(ByVal sNameSpace As String, ByVal PgeBaseNme As Integer) As String
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

            'objParam(iCount) = New OleDb.OleDbParameter("PGE_Desc", OleDb.OleDbType.Integer)
            'objParam(iCount).Direction = ParameterDirection.Output
            'iCount += 1
            cmd = objDb.SpFrInsertionUsingCmd(sNameSpace, "GetPageDetails", objParam)
            Return cmd.Parameters("@PGE_Cabinet").Value & "|" & cmd.Parameters("@PGE_SubCabinet").Value &
                  "|" & cmd.Parameters("@PGE_Folder").Value & "|" & cmd.Parameters("@PGE_document_type").Value &
            "|" & cmd.Parameters("@PGE_FolID").Value & "|" & cmd.Parameters("@PGE_DocTypeID").Value


        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllCab(ByVal sNameSpace As String, ByVal status As String, ByVal cabid As Integer, ByVal subcabid As Integer, ByVal userid As Integer)
        Dim dt As New DataTable, dt2 As DataTable
        Dim dRow As DataRow
        Dim ssql As String, ssql1 As String, aSql As String = ""
        Dim depname As String
        Dim dr As OleDb.OleDbDataReader, dr1 As OleDb.OleDbDataReader
        Dim iID As Integer

        Try

            dt.Columns.Add("Fol_FolID")
            dt.Columns.Add("Fol_Name")
            dt.Columns.Add("Fol_Note")
            ' dt.Columns.Add("Fol_Cabinett")
            dt.Columns.Add("CBN_Department")
            dt.Columns.Add("Fol_CreatedOn")
            dt.Columns.Add("Fol_CreatedBy")
            dt.Columns.Add("Fol_DelFlag")
            dt.Columns.Add("CBN_Name")
            dt.Columns.Add("PGE_CABINET")
            dt.Columns.Add("PageCount")

            If (status = "De-Activated") Then
                ssql = "select distinct(fol_cabinet),Fol_DelFlag,Fol_FolID,Fol_Name,Fol_Note,Fol_CreatedOn,Fol_CreatedBy  from edt_folder where Fol_DelFlag='D' and Fol_cabinet='" & subcabid & "'"
            ElseIf (status = "Waiting for Approval") Then
                ssql = "select distinct(fol_cabinet),Fol_DelFlag,Fol_FolID,Fol_Name,Fol_Note,Fol_CreatedOn,Fol_CreatedBy  from edt_folder where Fol_DelFlag='W' and Fol_cabinet='" & subcabid & "'"
            ElseIf (status = "") Then
                'ssql = "select * from edt_folder where Fol_DelFlag!='V' and Fol_cabinet='" & subcabid & "'"
                ssql = "Select distinct(fol_cabinet),Fol_DelFlag,Fol_FolID,Fol_Name,Fol_Note,Fol_CreatedOn,Fol_CreatedBy  from Edt_folder where Fol_DelFlag!='V' and Fol_cabinet='" & subcabid & "'"
            End If
            'End If
            dr = objDb.SQLDataReader(sNameSpace, ssql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    If dr("Fol_DelFlag") = "A" Then
                        dRow("Fol_DelFlag") = "Activated"
                    ElseIf dr("Fol_DelFlag") = "D" Then
                        dRow("Fol_DelFlag") = "De-Activated"
                    ElseIf dr("Fol_DelFlag") = "W" Then
                        dRow("Fol_DelFlag") = "Waiting for Approval"
                    End If
                    dRow("Fol_FolID") = dr("Fol_FolID")
                    dRow("Fol_Name") = dr("Fol_Name")
                    dRow("Fol_Note") = dr("Fol_Note")
                    'dRow("CBN_SubCabCount") = dr("CBN_SubCabCount")
                    ' dRow("CBN_FolderCount") = dr("CBN_FolderCount")
                    dRow("Fol_CreatedOn") = dr("Fol_CreatedOn")
                    ssql1 = "select CBN_Name, CBN_Department from edt_cabinet where CBN_ID='" & dr("Fol_Cabinet") & "'"
                    dr1 = objDb.SQLDataReader(sNameSpace, ssql1)
                    If dr1.HasRows Then
                        While dr1.Read
                            dRow("CBN_Department") = objcab.GetGroupName(dr1("CBN_Department"), sNameSpace)
                            dRow("CBN_Name") = dr1("CBN_Name")
                        End While
                    End If
                    dRow("Fol_CreatedBy") = objcab.getcreatedbyname(sNameSpace, dr("Fol_CreatedBy"))
                    dRow("PGE_CABINET") = getCabinetID(sNameSpace, dr("Fol_Cabinet"))
                    aSql = "Select Count(PGE_Details_ID) from edt_page  where PGE_Folder='" & dr("FOL_FOLID") & "'"
                    iID = objDb.SQLExecuteScalarInt(sNameSpace, aSql)
                    If iID > 0 Then
                        dRow("PageCount") = iID
                    Else
                        dRow("PageCount") = 0
                    End If
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function CheckForCabPerm(ByVal objSrtPer As SrtFolPer, ByVal sNameSpace As String, ByVal sLevel As String, ByVal iCabID As Integer) As Boolean
        ' Dim objDB As New DBGeneral(sConStr, sRDBMS)
        Dim ssql As String
        Dim strsql As String = ""
        Dim bRet, bVal
        Dim iFolParGrp, iUsrParGrp As Integer
        Dim sMemGrp As String
        Dim oClsFolDis As New clsFolders
        Try
            If (sLevel = "F") Then
                If (objSrtPer.cLvlType = "U") Then
                    strsql = "Select * from edt_cabinet_permission where CBP_Cabinet=  " & iCabID &
                              "   and CBP_User=" & objSrtPer.iUsrId & " and CBP_Department=" & objSrtPer.iGrpId & " and CBP_PermissionType='U'"
                ElseIf (objSrtPer.cLvlType = "G") Then
                    strsql = "Select * from edt_cabinet_permission where CBP_Cabinet=  " & iCabID &
                                                       "  and CBP_Department=" & objSrtPer.iGrpId & " and CBP_PermissionType='G'"
                ElseIf (objSrtPer.cLvlType = "E") Then
                    strsql = "Select * from edt_cabinet_permission where CBP_Cabinet=  " & iCabID &
                                                       "   and CBP_User=0 and CBP_Department=0  and CBP_PermissionType='E'"
                End If
                ssql = strsql
                If (objSrtPer.iIndex = 1) Then
                    strsql = strsql & " and CBP_Index= " & objSrtPer.iIndex & ""
                End If
                If (objSrtPer.iSearch = 1) Then
                    strsql = strsql & " and CBP_Search= " & objSrtPer.iSearch & ""
                End If
                If (objSrtPer.iViewFol = 1) Then
                    strsql = strsql & " and CBP_View= " & objSrtPer.iViewFol & ""
                End If
                bRet = objDb.DBCheckForRecord(sNameSpace, strsql)
                bVal = objDb.DBCheckForRecord(sNameSpace, ssql)
                If (bRet = False) Then
                    iFolParGrp = GetParGrpID(objSrtPer.iCabId, sNameSpace)
                    iUsrParGrp = GetUserParGrp(sNameSpace, objSrtPer.iUsrId)
                    If (iFolParGrp = iUsrParGrp) Then
                        'The User Selected to give Permissions belongs to parent group

                        If (bVal = False) Then
                            bRet = True
                        Else
                            bRet = False
                        End If

                    Else
                        sMemGrp = objcab.GetMemberGroups(sNameSpace, objSrtPer.iUsrId)
                        If (sMemGrp.Length > 0) Then
                            If (InStr("," & sMemGrp & ",", "," & iFolParGrp & ",") > 0) Then
                                'The User Selected to give Permissions belongs to Member group
                                If (bVal = False) Then
                                    If (objSrtPer.iIndex = 1) Then
                                        bRet = False
                                    Else
                                        bRet = True
                                    End If
                                Else
                                    bRet = False
                                End If

                            Else
                                'The User do not belong to neither parent group or member group
                                bRet = False
                            End If
                        End If
                    End If
                End If
            End If
            Return bRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RetrievePermissions(ByVal sNameSpace As String, ByVal iFolId As Integer, ByVal iGrpId As Integer, ByVal iUsrId As Integer) As DataTable
        Dim strsql As String
        ' Dim objDB As New DBGeneral(GetKeyValues("ConnectionString"), GetKeyValues("sRDBMS"))
        Dim ds As DataSet
        Dim PerDt As DataTable
        Try
            If (iUsrId = 0 And iGrpId <> -1) Then
                strsql = "Select * from edt_Folder_Permission where EFP_PType='G' and EFP_GrpId=" & iGrpId &
                          " and EFP_FolId=" & iFolId & " "

                ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                PerDt = BuildPermDt(ds)
                Return PerDt
            ElseIf iGrpId <> -1 Then
                strsql = "Select * from edt_Folder_Permission where EFP_PType='U' and EFP_GrpId=" & iGrpId &
                          " and EFP_FolId=" & iFolId & "  And EFP_UsrId = " & iUsrId & ""
                ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                If (ds.Tables(0).Rows.Count = 0) Then
                    strsql = "Select * from edt_Folder_Permission where EFP_PType='G' and EFP_GrpId=" & iGrpId &
                                             " and EFP_FolId=" & iFolId & ""

                    ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                    PerDt = BuildPermDt(ds)
                    Return PerDt
                Else
                    PerDt = BuildPermDt(ds)
                    Return PerDt
                End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildPermDt(ByVal ds As DataSet) As DataTable
        Dim LocalDt As New DataTable
        Dim dc As DataColumn
        Dim i As Integer
        Dim drPerm As DataRow
        Try
            dc = New DataColumn("PerName", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("PerValue", GetType(Integer))
            LocalDt.Columns.Add(dc)
            If (ds.Tables(0).Rows.Count > 0) Then
                For i = 0 To ds.Tables(0).Columns.Count - 1
                    Select Case UCase(ds.Tables(0).Columns(i).ColumnName)

                        Case "EFP_INDEX"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "IND"
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_INDEX")
                            LocalDt.Rows.Add(drPerm)
                        Case "EFP_SEARCH"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "SRH"
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_Search")
                            LocalDt.Rows.Add(drPerm)
                        Case "EFP_MOD_FOLDER"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "MFD"
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_Mod_Folder")
                            LocalDt.Rows.Add(drPerm)
                        'Case "EFP_MOD_DOC"
                        '    drPerm = LocalDt.NewRow
                        '    drPerm("PerName") = "MDC"
                        '    drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_MOD_Doc")
                        '    LocalDt.Rows.Add(drPerm)
                        Case "EFP_DEL_FOLDER"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "DFD"
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_DEL_FOLDER")
                            LocalDt.Rows.Add(drPerm)
                        'Case "EFP_DEL_DOC"
                        '    drPerm = LocalDt.NewRow
                        '    drPerm("PerName") = "DDC"
                        '    drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_DEL_Doc")
                        '    LocalDt.Rows.Add(drPerm)
                        'Case "EFP_EXPORT"
                        '    drPerm = LocalDt.NewRow
                        '    drPerm("PerName") = "EXP"
                        '    drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_Export")
                        '    LocalDt.Rows.Add(drPerm)
                        'Case "EFP_CRT_DOC"
                        '    drPerm = LocalDt.NewRow
                        '    drPerm("PerName") = "CDC"
                        '    drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_CRT_Doc")
                        '    LocalDt.Rows.Add(drPerm)
                        Case "EFP_VIEW_FOL"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "VFD"
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("EFP_View_Fol")
                            LocalDt.Rows.Add(drPerm)
                    End Select
                Next
            End If
            Return LocalDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCheckPermissionFolDep(ByVal Folid As Integer, ByVal depid As Integer, ByVal sNameSpace As String)
        Dim ssql As String
        Dim dt As DataTable
        Try
            ssql = "select * from View_FolPermissions  where Fol_FolID='" & Folid & "' and EFP_GrpId ='" & depid & "' and EFP_PType='G'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, ssql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function BindCheckPermissionFolUser(ByVal cabid As Integer, ByVal depid As Integer, ByVal sNameSpace As String, ByVal userid As Integer)
        Dim ssql As String
        Dim dt As DataTable
        Try
            ssql = "select * from View_FolPermissions  where Fol_FolID='" & cabid & "' and EFP_GrpId ='" & depid & "' and EFP_USRID ='" & userid & "' and EFP_PType='U'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, ssql)
            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function getCabinetID(ByVal sNameSpace As String, ByVal Scabid As Integer)
        Dim ssql As String
        Try
            ssql = "select cbn_parent from edt_cabinet where Cbn_id='" & Scabid & "'"
            Return objDb.SQLExecuteScalar(sNameSpace, ssql)
        Catch ex As Exception
        End Try
    End Function
    Public Function SaveDefaultPermission(ByVal sNameSpace As String, ByVal objFoldr As clsFolders) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_PTYPE", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = "G"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_GRPID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_GPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_USRID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_VIEW_FOL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_CRT_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_EXPORT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_FOlId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_FolId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "InOrUpFolPermissions", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFolID(ByVal sNameSpace As String) As Integer
        Dim sSql As String
        Try
            sSql = "select max(fol_folid) from EDT_FOLDER"
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserFolID(ByVal sNameSpace As String) As Integer
        Dim sSql As String
        Try
            sSql = "select ISNULL(max(fol_folid)+1,1) from EDT_FOLDER"
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDepID(ByVal sNameSpace As String, ByVal sSubcab As String) As Integer
        Dim sSql As String
        Try
            sSql = "select cbn_department from EDT_Cabinet where cbn_id='" & sSubcab & "'"
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
