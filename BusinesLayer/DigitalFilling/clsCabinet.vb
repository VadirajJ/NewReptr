Imports System
Imports System.Data
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Web
Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports BusinesLayer.clsFolders
Imports System.Web.UI.WebControls

Public Class clsCabinet
    Dim objDb As New DBHelper
    Dim objGen As New clsEDICTGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared clsfol As New clsFolders

    Private CBN_ID As Integer
    Private CBN_Name As String
    Private CBN_Parent As Integer
    Private CBN_Note As String
    Private CBN_UserID As Integer
    Private CBN_Department As Integer
    Private CBN_SubCabCount As Integer
    Private CBN_FolderCount As Integer
    Private CBN_CreatedBy As Integer
    Private CBN_CreatedOn As Date
    Private CBN_UpdatedBy As Integer
    Private CBN_UpdatedOn As Date
    Private CBN_ApprovedBy As Integer
    Private CBN_ApprovedOn As Date
    Private CBN_DeletedBy As Integer
    Private CBN_DeletedOn As Date
    Private CBN_RecalledBy As Integer
    Private CBN_RecalledOn As Date
    Private CBN_Status As String
    Private CBN_DelFlag As String
    Private CBN_CompID As Integer
    Private CBPArray As Array

    Private CBP_ID As Integer
    Private CBP_PermissionType As String
    Private CBP_Cabinet As Integer
    Private CBP_User As Integer
    Private CBP_Department As Integer
    Private CBP_Others As Integer 'Vijeth

    Private CBP_View As Integer
    Private CBP_Create As Integer
    Private CBP_Modify As Integer
    Private CBP_Delete As Integer
    Private CBP_Search As Integer
    Private CBP_Index As Integer
    Private CBP_CreateFolder As Integer

    Public Structure SrtCabPer
        Dim cLvlType As Char
        Dim iCabId As Integer
        Dim iUsrId As Integer
        Dim iGrpId As Int16
        Dim iCrSubCab As Int16
        Dim iModCab As Int16
        Dim iDelCab As Int16
        Dim iCrFol As Int16
        Dim iIndex As Integer
        Dim iSearch As Integer
        Dim iOther As Integer
        Dim iView As Int16
    End Structure
    Public Property iCBP_Department() As Integer
        Get
            Return (CBP_Department)
        End Get
        Set(ByVal Value As Integer)
            CBP_Department = Value
        End Set
    End Property
    Public Property iCBP_User() As Integer
        Get
            Return (CBP_User)
        End Get
        Set(ByVal Value As Integer)
            CBP_User = Value
        End Set
    End Property
    Public Property iCBP_Cabinet() As Integer
        Get
            Return (CBP_Cabinet)
        End Get
        Set(ByVal Value As Integer)
            CBP_Cabinet = Value
        End Set
    End Property
    Public Property sCBP_PermissionType() As String
        Get
            Return (CBP_PermissionType)
        End Get
        Set(ByVal Value As String)
            CBP_PermissionType = Value
        End Set
    End Property
    Public Property iCBP_ID() As Integer
        Get
            Return (CBP_ID)
        End Get
        Set(ByVal Value As Integer)
            CBP_ID = Value
        End Set
    End Property
    Public Property iCBN_CompID() As Integer
        Get
            Return (CBN_CompID)
        End Get
        Set(ByVal Value As Integer)
            CBN_CompID = Value
        End Set
    End Property
    Public Property sCBN_DelFlag() As String
        Get
            Return (CBN_DelFlag)
        End Get
        Set(ByVal Value As String)
            CBN_DelFlag = Value
        End Set
    End Property
    Public Property sCBN_Status() As String
        Get
            Return (CBN_Status)
        End Get
        Set(ByVal Value As String)
            CBN_Status = Value
        End Set
    End Property
    Public Property iCBN_RecalledBy() As Integer
        Get
            Return (CBN_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            CBN_RecalledBy = Value
        End Set
    End Property
    Public Property iCBN_DeletedBy() As Integer
        Get
            Return (CBN_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            CBN_DeletedBy = Value
        End Set
    End Property

    Public Property iCBN_ApprovedBy() As Integer
        Get
            Return (CBN_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            CBN_ApprovedBy = Value
        End Set
    End Property

    Public Property iCBN_UpdatedBy() As Integer
        Get
            Return (CBN_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            CBN_UpdatedBy = Value
        End Set
    End Property
    Public Property iCBN_CreatedBy() As Integer
        Get
            Return (CBN_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            CBN_CreatedBy = Value
        End Set
    End Property

    Public Property iCBN_FolderCount() As Integer
        Get
            Return (CBN_FolderCount)
        End Get
        Set(ByVal Value As Integer)
            CBN_FolderCount = Value
        End Set
    End Property

    Public Property iCBN_SubCabCount() As Integer
        Get
            Return (CBN_SubCabCount)
        End Get
        Set(ByVal Value As Integer)
            CBN_SubCabCount = Value
        End Set
    End Property
    Public Property iCBN_Department() As Integer
        Get
            Return (CBN_Department)
        End Get
        Set(ByVal Value As Integer)
            CBN_Department = Value
        End Set
    End Property
    Public Property iCBN_UserID() As Integer
        Get
            Return (CBN_UserID)
        End Get
        Set(ByVal Value As Integer)
            CBN_UserID = Value
        End Set
    End Property
    Public Property sCBN_Note() As String
        Get
            Return (CBN_Note)
        End Get
        Set(ByVal Value As String)
            CBN_Note = Value
        End Set
    End Property
    Public Property iCBN_Parent() As Integer
        Get
            Return (CBN_Parent)
        End Get
        Set(ByVal Value As Integer)
            CBN_Parent = Value
        End Set
    End Property
    Public Property sCBN_Name() As String
        Get
            Return (CBN_Name)
        End Get
        Set(ByVal Value As String)
            CBN_Name = Value
        End Set
    End Property
    Public Property iCBN_ID() As Integer
        Get
            Return (CBN_ID)
        End Get
        Set(ByVal Value As Integer)
            CBN_ID = Value
        End Set
    End Property
    Public Property iCBP_Others() As Integer 'Vijeth
        Get
            Return (CBP_Others)
        End Get
        Set(ByVal Value As Integer)
            CBP_Others = Value
        End Set
    End Property
    Public Property iCBP_View() As Integer
        Get
            Return (CBP_View)
        End Get
        Set(ByVal Value As Integer)
            CBP_View = Value
        End Set
    End Property
    Public Property iCBP_Modify() As Integer
        Get
            Return (CBP_Modify)
        End Get
        Set(ByVal Value As Integer)
            CBP_Modify = Value
        End Set
    End Property
    Public Property iCBP_Create() As Integer
        Get
            Return (CBP_Create)
        End Get
        Set(ByVal Value As Integer)
            CBP_Create = Value
        End Set
    End Property
    Public Property iCBP_Search() As Integer
        Get
            Return (CBP_Search)
        End Get
        Set(ByVal Value As Integer)
            CBP_Search = Value
        End Set
    End Property
    Public Property iCBP_Index() As Integer
        Get
            Return (CBP_Index)
        End Get
        Set(ByVal Value As Integer)
            CBP_Index = Value
        End Set
    End Property
    Public Property iCBP_Delete() As Integer
        Get
            Return (CBP_Delete)
        End Get
        Set(ByVal Value As Integer)
            CBP_Delete = Value
        End Set
    End Property
    Public Property iCBP_CreateFolder() As Integer
        Get
            Return (CBP_CreateFolder)
        End Get
        Set(ByVal Value As Integer)
            CBP_CreateFolder = Value
        End Set
    End Property
    Public Function LoadDepartment(ByVal sAC As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Org_node,Org_Name from Sad_Org_Structure where Org_DelFlag='A' and Org_LevelCode = 3 Order By Org_Name"
            dt = objDb.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Dim sMem As String
    Dim iParGrp As Integer = 0
    Dim Permdt As DataTable
    Dim dtPerm As New DataTable

    Public Function CheckCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCustomerName As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from SAD_CUSTOMER_MASTER where CUST_Name like '%" & sCustomerName & "%' "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCabinetGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDescID As Integer, ByVal sDept As String, ByVal userid As Integer, Optional ByVal iCabID As Integer = 0, Optional ByVal sPerm As String = "VCB") As DataTable

        Dim i As Integer = 1, deptid As Integer, iUsrType As Integer, iUsrParGrp As Integer
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dsCab As DataSet
        Dim sPermlvl As String
        Dim sCabPerm As String
        Dim iRet

        Try

            Permdt = BuildPermTable()
            sMem = GetMemberGroups(sNameSpace, userid)
            iUsrType = GetUserType(sNameSpace, userid)
            iUsrParGrp = GetUserParGrp(sNameSpace, userid)

            If (iUsrType = 1) Then
                'User Logged is Super User
                If (iCabID = 0) Then
                    sSql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent=-1 order by CBN_Name"
                    UpdateFolderCount(sNameSpace, sSql)
                Else
                    sSql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent = " & iCabID & " order by CBN_Name "
                End If

                dsCab = objDb.SQLExecuteDataSet(sNameSpace, sSql)
                If (dsCab.Tables(0).Rows.Count > 0) Then
                    For Each dRow In dsCab.Tables(0).Rows
                        iRet = GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm)
                        If (iRet = 1) Then
                            AddPermissions(sNameSpace, dtPerm, sPermlvl)
                        End If
                    Next
                End If
                Return Permdt
                Exit Function
            End If
            sCabPerm = GetPermCabinets(sNameSpace, userid, sMem)
            If (iCabID = 0) Then
                sSql = " Select *  from edt_cabinet where CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_Parent= -1 "
            Else
                sSql = " Select *  from edt_cabinet where  CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_Parent = " & iCabID & "  "
            End If
            If Val(sCabPerm) <> 0 Then
                sSql = sSql & " and cbn_id Not in (" & sCabPerm & ") order by CBN_Name "
            Else
                sSql = sSql & " order by CBN_Name "
            End If

            dsCab = objDb.SQLExecuteDataSet(sNameSpace, sSql)
            If (dsCab.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsCab.Tables(0).Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm)
                    If (iRet = 1) Then
                        AddPermissions(sNameSpace, dtPerm, sPermlvl)
                    End If
                Next

            End If


            If (iCabID = 0) Then
                sSql = "Select *  from edt_cabinet where CBN_DelFlag='A' and CBN_Parent= -1"
                'CBP_CabId in (" & sCabPerm & ") and 
            Else
                sSql = " Select *  from edt_cabinet where CBN_DelFlag='A' and CBN_Parent = " & iCabID & ""
            End If

            If Len((sCabPerm)) <> 0 Then
                sSql = sSql & " and cbn_id in (" & sCabPerm & ")"
            Else
                sSql = sSql & " and cbn_id in (0)"
            End If
            sSql = sSql & " order by CBN_Name"
            dsCab = objDb.SQLExecuteDataSet(sNameSpace, sSql)
            If (dsCab.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsCab.Tables(0).Rows
                    iParGrp = dRow("cbn_Department")
                    iRet = GetFinalPermissions(iUsrType, dRow("cbn_id"), userid, sNameSpace, sPerm, 1)
                    If (iRet = 1) Then
                        AddPermissions(sNameSpace, dtPerm, sPermlvl)
                    End If
                Next
            End If

            Return Permdt

        Catch ex As Exception

        End Try

        'dtDisplay.Columns.Add("CBN_ID")
        'dtDisplay.Columns.Add("CBN_NAME")
        'dtDisplay.Columns.Add("CBN_NOTE")
        'dtDisplay.Columns.Add("CBN_SubCabCount")
        'dtDisplay.Columns.Add("CBN_FolderCount")
        'dtDisplay.Columns.Add("Org_Name")
        'dtDisplay.Columns.Add("Org_node")
        'dtDisplay.Columns.Add("CBN_CreatedBy")
        'dtDisplay.Columns.Add("CBN_CreatedOn")
        'dtDisplay.Columns.Add("CBN_DelFlag")

        'Try
        '    sSql = "Select a.CBN_ID,a.CBN_NAME,a.CBN_NOTE,a.CBN_SubCabCount,a.CBN_FolderCount,a.CBN_Department,a.CBN_CreatedBy,a.CBN_CreatedOn,a.CBN_DelFlag,b.Org_Name,b.Org_node "
        '    sSql = sSql & "from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent=-1 and CBN_DelFlag<>'V' "
        '    If iDescID > 0 Then
        '        sSql = sSql & " And a.CBN_ID=" & iDescID & ""
        '    End If

        '    If sDept <> "" Then
        '        sSql = sSql & " And b.Org_Node In (" & sDept & ")"
        '    End If

        '    sSql = sSql & "order by a.CBN_NAME"
        '    dr = objDb.SQLDataReader(sNameSpace, sSql)

        '    If dr.HasRows Then
        '        While dr.Read
        '            dRow = dtDisplay.NewRow

        '            dRow("CBN_ID") = dr("CBN_ID")


        '            If IsDBNull(dr("CBN_NAME")) = False Then
        '                dRow("CBN_NAME") = dr("CBN_NAME")
        '            Else
        '                dRow("CBN_NAME") = ""
        '            End If

        '            If IsDBNull(dr("CBN_NOTE")) = False Then
        '                dRow("CBN_NOTE") = dr("CBN_NOTE")
        '            Else
        '                dRow("CBN_NOTE") = ""
        '            End If

        '            If IsDBNull(dr("CBN_SubCabCount")) = False Then
        '                dRow("CBN_SubCabCount") = dr("CBN_SubCabCount")
        '            Else
        '                dRow("CBN_SubCabCount") = 0
        '            End If

        '            If IsDBNull(dr("CBN_FolderCount")) = False Then
        '                dRow("CBN_FolderCount") = dr("CBN_FolderCount")
        '            Else
        '                dRow("CBN_FolderCount") = 0
        '            End If

        '            If IsDBNull(dr("Org_Node")) = False Then
        '                dRow("Org_Node") = dr("Org_Node")
        '            Else
        '                dRow("Org_Node") = ""
        '            End If

        '            If IsDBNull(dr("Org_Name")) = False Then
        '                dRow("Org_Name") = dr("Org_Name")
        '            Else
        '                dRow("Org_Name") = ""
        '            End If

        '            If IsDBNull(dr("CBN_CreatedBy")) = False Then
        '                dRow("CBN_CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dr("CBN_CreatedBy"))
        '            End If

        '            If IsDBNull(dr("CBN_CreatedOn")) = False Then
        '                dRow("CBN_CreatedOn") = objGen.FormatDtForRDBMS(dr("CBN_CreatedOn"), "D")
        '            End If

        '            If IsDBNull(dr("cbn_DelFlag")) = False Then
        '                If dr("cbn_DelFlag") = "A" Then
        '                    dRow("cbn_DelFlag") = "Activated"
        '                ElseIf dr("cbn_DelFlag") = "D" Then
        '                    dRow("cbn_DelFlag") = "De-Activated"
        '                ElseIf dr("cbn_DelFlag") = "W" Then
        '                    dRow("cbn_DelFlag") = "Waiting for Approval"
        '                End If
        '            End If

        '            dtDisplay.Rows.Add(dRow)
        '        End While
        '    End If
        '    Return dtDisplay
        'Catch ex As Exception
        '    Throw
        'End Try
    End Function
    'Public Function LoadCabinetGridWithPermission(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUseriD As String) As DataTable
    '    Dim dtDisplay As New DataTable
    '    Dim i As Integer = 1
    '    Dim dRow As DataRow
    '    Dim sSql As String
    '    Dim dr As OleDb.OleDbDataReader

    '    dtDisplay.Columns.Add("CBN_ID")
    '    dtDisplay.Columns.Add("CBN_NAME")
    '    dtDisplay.Columns.Add("CBN_NOTE")
    '    dtDisplay.Columns.Add("CBN_SubCabCount")
    '    dtDisplay.Columns.Add("CBN_FolderCount")
    '    dtDisplay.Columns.Add("Org_Name")
    '    dtDisplay.Columns.Add("Org_node")
    '    dtDisplay.Columns.Add("CBN_CreatedBy")
    '    dtDisplay.Columns.Add("CBN_CreatedOn")
    '    dtDisplay.Columns.Add("CBN_DelFlag")

    '    Try
    '        sSql = "  Select b.CBN_ID,b.CBN_NAME,b.CBN_NOTE,b.CBN_SubCabCount,b.CBN_FolderCount,b.CBN_Department,b.CBN_CreatedBy,"
    '        sSql = sSql & "b.CBN_CreatedOn,b.CBN_DelFlag,c.Org_Name,c.Org_node from edt_cabinet_Permission a join edt_cabinet b on a.CBP_Cabinet=b.CBN_ID"
    '        sSql = sSql & "join Sad_Org_Structure c on CBN_Department=Org_Node  where CBN_Parent=-1 and CBN_DelFlag<>'V' and  "
    '        sSql = sSql & "((a.CBP_View=1 and a.CBP_Other=1 and a.CBP_User=2 and a.cbp_permissiontype='U') or (a.CBP_View=1 and a.CBP_Other=0 and a.cbp_permissiontype='G'))"
    '        sSql = sSql & "order by a.CBN_NAME"
    '        dr = objDb.SQLDataReader(sNameSpace, sSql)


    '        If dr.HasRows Then
    '            While dr.Read
    '                dRow = dtDisplay.NewRow

    '                dRow("CBN_ID") = dr("CBN_ID")

    '                If IsDBNull(dr("CBN_NAME")) = False Then
    '                    dRow("CBN_NAME") = dr("CBN_NAME")
    '                Else
    '                    dRow("CBN_NAME") = ""
    '                End If

    '                If IsDBNull(dr("CBN_NOTE")) = False Then
    '                    dRow("CBN_NOTE") = dr("CBN_NOTE")
    '                Else
    '                    dRow("CBN_NOTE") = ""
    '                End If

    '                If IsDBNull(dr("CBN_SubCabCount")) = False Then
    '                    dRow("CBN_SubCabCount") = dr("CBN_SubCabCount")
    '                Else
    '                    dRow("CBN_SubCabCount") = 0
    '                End If

    '                If IsDBNull(dr("CBN_FolderCount")) = False Then
    '                    dRow("CBN_FolderCount") = dr("CBN_FolderCount")
    '                Else
    '                    dRow("CBN_FolderCount") = 0
    '                End If

    '                If IsDBNull(dr("Org_Node")) = False Then
    '                    dRow("Org_Node") = dr("Org_Node")
    '                Else
    '                    dRow("Org_Node") = ""
    '                End If

    '                If IsDBNull(dr("Org_Name")) = False Then
    '                    dRow("Org_Name") = dr("Org_Name")
    '                Else
    '                    dRow("Org_Name") = ""
    '                End If

    '                If IsDBNull(dr("CBN_CreatedBy")) = False Then
    '                    dRow("CBN_CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dr("CBN_CreatedBy"))
    '                End If

    '                If IsDBNull(dr("CBN_CreatedOn")) = False Then
    '                    dRow("CBN_CreatedOn") = objGen.FormatDtForRDBMS(dr("CBN_CreatedOn"), "D")
    '                End If

    '                If IsDBNull(dr("cbn_DelFlag")) = False Then
    '                    If dr("cbn_DelFlag") = "A" Then
    '                        dRow("cbn_DelFlag") = "Activated"
    '                    ElseIf dr("cbn_DelFlag") = "D" Then
    '                        dRow("cbn_DelFlag") = "De-Activated"
    '                    ElseIf dr("cbn_DelFlag") = "W" Then
    '                        dRow("cbn_DelFlag") = "Waiting for Approval"
    '                    End If
    '                End If

    '                dtDisplay.Rows.Add(dRow)
    '            End While
    '        End If
    '        Return dtDisplay
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Sub UpdateStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatus As String, ByVal iCBN_ID As String, ByVal sDelFlag As String, ByVal iUsrId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update edt_cabinet set "
            If sStatus = "D" Then
                sSql = sSql & " CBN_DelFlag='" & sDelFlag & "', CBN_DeletedBy=" & iUsrId & ", CBN_DeletedOn=Getdate(), CBN_Status='AD'"
            ElseIf sStatus = "A" Then
                sSql = sSql & " CBN_DelFlag='" & sDelFlag & "', CBN_RecalledBy=" & iUsrId & ", CBN_RecalledOn=Getdate(), CBN_Status='AR'"
            ElseIf sStatus = "W" Then
                sSql = sSql & " CBN_DelFlag='" & sDelFlag & "', CBN_ApprovedBy=" & iUsrId & ", CBN_ApprovedOn=Getdate(), CBN_Status='A'"
            ElseIf sStatus = "AV" Then
                sSql = sSql & " CBN_DelFlag='" & sDelFlag & "', CBN_UpdatedBy=" & iUsrId & ", CBN_UpdatedOn=Getdate(), CBN_Status='AV'" 'manish 
            End If
            sSql = sSql & " Where CBN_ID=" & iCBN_ID & " and CBN_CompID = " & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCabinet(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal sDept As String, ByVal userid As Integer) As DataSet
        Dim sSql As String = ""
        Dim ssql1 As String
        Dim ds As DataSet
        Dim usertype, memtype As Integer
        Try
            'sSql = "" : sSql = "Select CBN_ID,CBN_NAME from edt_cabinet WHERE CBN_PARENT=-1 and CBN_DelFlag='A' and CBN_CompID = " & iCOmpID & ""
            'sSql = "" : sSql = "Select CBN_ID,CBN_NAME from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent=-1 and a.CBN_DelFlag='A' "
            'If sDept <> "" Then
            '    sSql = sSql & " And b.Org_Node In (" & sDept & ")"
            'End If

            'sSql = sSql & " Order by CBN_NAME"
            'Return objDb.SQLExecuteDataSet(sNameSpace, sSql)

            ssql1 = "select * from Sad_userdetails where usr_id='" & userid & "'"
            ds = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
            usertype = ds.Tables(0).Rows(0)("USR_IsSuperUser").ToString()
            memtype = ds.Tables(0).Rows(0)("USR_MemberType").ToString()

            If (usertype = 1 Or memtype = 1) Then
                sSql = "" : sSql = "Select CBN_ID,CBN_NAME from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent=-1 and a.CBN_DelFlag='A' "
                If sDept <> "" Then
                    sSql = sSql & " And b.Org_Node In (" & sDept & ")"
                End If
                sSql = sSql & " Order by CBN_NAME"
            Else
                sSql = "" : sSql = "Select distinct CBN_ID,CBN_NAME from view_cabpermissions where  CBN_Parent=-1 and CBN_DelFlag='A' "
                If sDept <> "" Then
                    sSql = sSql & " And (CBN_Department in (" & sDept & ") or CBP_Department in (" & sDept & ")) and (CBP_User='" & userid & "' or CBP_User=0)"
                End If
                sSql = sSql & " Order by CBN_NAME"
            End If
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCabdetails(ByVal iCBNID As Integer, ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CBN_NAME,CBN_Department,CBN_Note from edt_cabinet where CBN_ID=" & iCBNID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCabUserPer(ByVal sNameSpace As String, ByVal iDeptID As Integer) As DataSet
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,Usr_LoginName from Sad_UserDetails Left join Sad_Org_Structure on Org_node=Usr_DeptId"
            sSql = sSql & " where Usr_DeptId =" & iDeptID & " And USR_DutyStatus='A'"
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCabName(ByVal sAC As String, ByVal iCompID As Integer, ByVal sCabName As String, ByVal iCabID As Integer, ByVal iDepartment As Integer) As Boolean
        Dim sSql As String = ""
        Try
            If (iDepartment = 0) Then
                'Assume cabinet name need to be checked for all groups
                sSql = "" : sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sCabName & "' and  CBN_ID <>" & iCabID & " and "
                sSql = sSql & "CBN_Parent =-1 And (CBN_DelFlag='A' or CBN_DelFlag='W')"
            Else
                'Check cabinet name only for that group
                sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sCabName & "' and CBN_Department=" & iDepartment & " and "
                sSql = sSql & "CBN_ID <> " & iCabID & "  And CBN_Parent=-1 And (CBN_DelFlag='A' or CBN_DelFlag='W')"
            End If
            Return objDb.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPermission(ByVal sNameSpace As String, ByVal iDepartment As Integer, ByVal iUsrId As Integer, ByVal iCabID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from edt_cabinet_Permission where CBP_Department=" & iDepartment & " and CBP_User=" & iUsrId & " and CBP_Cabinet=" & iCabID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCabDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objCab As clsCabinet) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_NAME", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCab.sCBN_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Note", OleDb.OleDbType.VarChar, 7999)
            ObjParam(iParamCount).Value = objCab.sCBN_Note
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_PARENT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_Parent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_USERID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_UserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_SubCabCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_SubCabCount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_FolderCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_FolderCount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objCab.sCBN_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objCab.sCBN_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spEDT_Cabinet", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePermission(ByVal sNameSpace As String, ByVal objCab As clsCabinet, ByVal SArray As Array) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            If SArray(1) = 1 Or SArray(5) = 1 Or SArray(3) = 1 Or SArray(4) = 1 Then          'Vijeth  
                SArray(2) = 1 ' vijaylakshmi interchanged SArray(5) = 1 and SArray(2) = 1 
            End If

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PermissionType", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objCab.sCBP_PermissionType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_Cabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_User", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_User
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_View", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(2) ' vijayalakshmi SArray(5)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(1)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_MODIFY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(3) 'Vijaylakshmi SArray(2)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0 'Vijeth SArray(3)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(5) ' Vijaylakshmi SArray(3)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(4)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_Others 'Vijeth
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0  'Vijeth SArray(4)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spEDT_cabinet_Permission", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception

            Throw
        End Try
    End Function
    Public Sub UpdateCabDetails(ByVal sNameSpace As String, ByVal iDepartment As Integer, ByVal iCBN_ID As Integer)
        Dim strsql As String = "", aSql As String = ""
        Try
            'Update Sub cabinet count
            strsql = "Update edt_cabinet set CBN_SubCabCount=(Select count(CBN_ID) from Edt_Cabinet where CBN_Parent=" & iCBN_ID & " and CBN_DelFlag='A') where CBN_ID=" & iCBN_ID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, strsql)

            'Update folder count
            aSql = "Update edt_cabinet set CBN_FolderCount=(select count(Fol_folid) from edt_folder where fol_cabinet in (Select CBN_ID from Edt_Cabinet where CBN_Parent=" & iCBN_ID & " and CBN_DelFlag='A')) where CBN_ID=" & iCBN_ID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, aSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteCabPermission(ByVal sNameSpace As String, ByVal iDepartment As Integer, ByVal iUser As Integer, ByVal iCab As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Delete edt_cabinet_Permission where CBP_Department=" & iDepartment & " and CBP_Cabinet = " & iCab & " "
            If iUser > 0 Then
                sSql = sSql & "and CBP_User =" & iUser & ""
            End If
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadUserPermission(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CBP_PID,USr_FullName from sad_userdetails where usr_compID=" & iACID & " And USR_Partner=1 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') order by USr_FullName"
            Return objDb.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadUserOtherDepartment(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Try
            'sSql = "Select distinct Org_Node,Org_Name from Sad_Org_Structure Left Join Sad_UsersInOtherDept On SUO_DeptID=Org_Node"
            'sSql = sSql & " where Org_DelFlag='A' And Org_CompID=" & iACID & " And SUO_CompID=" & iACID & " And SUO_UserID=" & iUserID & " And Org_LevelCode=3 order by Org_Name"
            'Return objDb.SQLExecuteDataTable(sAC, sSql)

            sSql = "Select distinct Org_Node,Org_Name from Sad_Org_Structure Left Join Sad_UsersInOtherDept On SUO_DeptID=Org_Node"
            sSql = sSql & " where Org_DelFlag='A' And Org_CompID=" & iACID & " And Org_LevelCode=3 order by Org_Name"
            Return objDb.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveDefaultPermission(ByVal sNameSpace As String, ByVal objCab As clsCabinet) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PermissionType", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = "G"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_Cabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_User", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_View", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_MODIFY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0 'Vijeth SArray(3)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0  'Vijeth SArray(4)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spEDT_cabinet_Permission", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception

            Throw
        End Try
    End Function
    Public Function GetCabID(ByVal sNameSpace As String) As Integer
        Dim sSql As String
        Try
            sSql = "select max(CBN_ID) from EDT_Cabinet"
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserCabID(ByVal sNameSpace As String) As Integer
        Dim sSql As String
        Try
            sSql = "select ISNULL(max(CBN_Id)+1,1) from edt_cabinet"
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserParGrp(ByVal sNameSpace As String, ByVal iLogUsrID As Integer) As Integer
        Dim strsql As String
        Try
            strsql = "Select isnull(usr_deptid,0) from sad_Userdetails where usr_id=" & iLogUsrID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, strsql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserType(ByVal sNameSpace As String, ByVal iUserId As Integer) As Integer
        Dim strsql As String
        Try
            strsql = "Select isnull(usr_IsSuperuser,0) from sad_userdetails where usr_id=" & iUserId & ""
            Return objDb.SQLExecuteScalar(sNameSpace, strsql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMemberType(ByVal sNameSpace As String, ByVal iUserId As String) As Integer
        Dim strsql As String
        Try
            strsql = "Select USR_MemberType from sad_userdetails where usr_id=" & iUserId & ""
            Return objDb.SQLExecuteScalar(sNameSpace, strsql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMemberGroups(ByVal sNameSpace As String, ByVal iUsrId As Integer) As String
        Dim strsql As String
        'Dim objDB As New DBGeneral(sConStr, sRDBMS)
        Dim dr As OleDb.OleDbDataReader
        Try
            strsql = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_Userid = " & iUsrId & ""
            dr = objDb.SQLDataReader(sNameSpace, strsql)
            strsql = ""
            While (dr.Read)
                strsql = strsql & "," & dr("SUO_DeptID")
            End While
            If (strsql.Length > 0) Then
                strsql = strsql.Remove(0, 1)
            Else
                strsql = 0
            End If
            dr.Close()
            Return strsql
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateFolderCount(ByVal sNameSpace As String, ByVal sSql As String)
        Dim ds As DataSet
        Dim dr1 As OleDb.OleDbDataReader
        Dim iSql, mSql As String
        Dim i As Integer
        Try
            ds = objDb.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    iSql = "select count(*) as Count from edt_folder where fol_cabinet in(select CBN_ID from edt_cabinet where cbn_parent = " & ds.Tables(0).Rows(i)("CBN_ID") & ") and FOL_DelFlag = 'A'"
                    dr1 = objDb.SQLDataReader(sNameSpace, iSql)
                    If dr1.HasRows = True Then
                        dr1.Read()
                        mSql = "Update edt_cabinet set CBN_FolderCount = " & dr1("Count") & " where CBN_ID = " & ds.Tables(0).Rows(i)("CBN_ID") & ""
                        objDb.SQLExecuteNonQuery(sNameSpace, mSql)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Dim sCabPerm As String
    Dim sPermlvl As String
    Dim iUsrParGrp As Integer = 0
    Public Function GetFinalPermissions(ByVal useretype As Integer, ByVal iCabId As Integer, ByVal iUserId As Int16, ByVal sNameSpace As String, Optional ByVal sPerType As String = "ALL", Optional ByVal iChkType As Int16 = 0) As Object
        Dim ssql As String
        Dim dt, dt2 As DataTable
        Dim depid As Integer
        Try
            'First Get the Parent GroupId of the Cabinet
            'sCabPerm = String.Empty
            If (iChkType = 2) Then
                sCabPerm = String.Empty
            End If
            sPermlvl = String.Empty
            If (iChkType = 2) Then
                iParGrp = GetParGrpID(iCabId, sNameSpace)
            End If

            dtPerm = GetMainPermDS(useretype, iCabId, iUserId, iParGrp, sNameSpace, iChkType)

            If (dtPerm.Rows.Count > 0) Then
                Select Case UCase(sPerType)
                    Case "ALL"
                        Dim Ht As New Hashtable
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Ht.Add("CCreate", 0)
                                Ht.Add("CView", 1)
                                Ht.Add("CModify", 0)
                                ' Ht.Add("CDelete", 0)
                                ' Ht.Add("FCreate", 0)
                                Ht.Add("CIndex", 1)
                                Ht.Add("CSearch", 1)
                                Ht.Add("Level", sPermlvl)
                            Else
                                Ht.Add("CCreate", 0)
                                Ht.Add("CView", 1)
                                Ht.Add("CModify", 0)
                                ' Ht.Add("CDelete", 0)
                                'Ht.Add("FCreate", 0)
                                Ht.Add("CIndex", 0)
                                Ht.Add("CSearch", 1)
                                Ht.Add("Level", sPermlvl)
                            End If
                            Return Ht
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Ht.Add("CCreate", 1)
                            Ht.Add("CView", 1)
                            Ht.Add("CModify", 1)
                            ' Ht.Add("CDelete", 1)
                            'Ht.Add("FCreate", 1)
                            Ht.Add("CIndex", 1)
                            Ht.Add("CSearch", 1)
                            Ht.Add("Level", sPermlvl)
                            Return Ht
                        Else
                            Ht.Add("CCreate", dtPerm.Rows(0).Item("CBP_Create"))
                            Ht.Add("CView", dtPerm.Rows(0).Item("CBP_View"))
                            Ht.Add("CModify", dtPerm.Rows(0).Item("CBP_Modify"))
                            'Ht.Add("CDelete", dtPerm.Rows(0).Item("CBP_Delete"))
                            'Ht.Add("FCreate", dtDisplay.Rows(0).Item("CBP_Create_Folder"))
                            Ht.Add("CIndex", dtPerm.Rows(0).Item("CBP_Index"))
                            Ht.Add("CSearch", dtPerm.Rows(0).Item("CBP_Search"))
                            Ht.Add("Level", sPermlvl)
                            Return Ht
                        End If

                    Case "CSC"
                        'dsPerm = dsMain
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("CBP_Create")
                    Case "MCB"
                        'dsPerm = dsMain
                        If (sPermlvl = "PG") Then
                            Return 0
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("CBP_Modify")
                    Case "SRH"
                        'dsPerm = dsMain
                        If (sPermlvl = "PG") Then
                            Return 1
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("CBP_Search")
                    'Case "DCB"
                    '    'dsPerm = dsMain
                    '    If (sPermlvl = "PG") Then
                    '        Return 0
                    '    ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                    '        Return 1
                    '    End If
                    '    Return dtPerm.Rows(0).Item("CBP_Delete")

                    'Case "CFD"
                    '    'dsPerm = dsMain
                    '    If (sPermlvl = "PG") Then
                    '        Return 0
                    '    ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                    '        Return 1
                    '    End If
                    '    Return dtDisplay.Rows(0).Item("CBP_Create_Folder")
                    Case "IDX"
                        'dsPerm = dsMain
                        If (sPermlvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Return 1
                            Else
                                Return 0
                            End If

                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("CBP_Index")

                    Case "VCB"
                        'dsPerm = dsMain
                        If (sPermlvl = "PG") Then
                            Return 1
                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("CBP_View")
                End Select
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Dim dtDisplay As New DataTable
    Dim dsMain As New DataSet
    Public Function AddPermissions(ByVal sNameSpace As String, ByVal dtPerm As DataTable, ByVal PLevel As String)
        Dim dsRow, dtRow As DataRow
        Dim depname As String
        Try
            'For Each dsRow In dsMain.Tables(0).Rows
            dsRow = dtPerm.Rows(0)
            dtRow = Permdt.NewRow

            'dtRow("PLevel") = PLevel
            dtRow("CBN_ID") = dsRow("CBN_ID")
            dtRow("CBN_Name") = dsRow("CBN_Name")
            'dtRow("CBN_Parent") = dsRow("CBN_Parent")
            dtRow("CBN_Note") = dsRow("CBN_Note")
            'dtRow("CBN_UserId") = dsRow("CBN_UserId")
            dtRow("CBN_CreatedBy") = getcreatedbyname(sNameSpace, dsRow("Cbn_CreatedBy"))
            dtRow("CBN_Department") = dsRow("CBN_Department")
            depname = GetGroupName(dsRow("CBN_Department"), sNameSpace)
            dtRow("CBN_Department") = depname
            dtRow("CBN_CreatedOn") = dsRow("CBN_CreatedOn")
            dtRow("CBN_SubCabCount") = dsRow("CBN_SubCabCount")
            dtRow("CBN_FolderCount") = dsRow("CBN_FolderCount")
            dtRow("CBN_DelFlag") = dsRow("CBN_DelFlag")
            If dtRow("cbn_DelFlag") = "A" Then
                dsRow("cbn_DelFlag") = "Activated"
            ElseIf dtRow("cbn_DelFlag") = "D" Then
                dsRow("cbn_DelFlag") = "De-Activated"
            ElseIf dtRow("cbn_DelFlag") = "W" Then
                dsRow("cbn_DelFlag") = "Waiting for Approval"
            End If
            dtRow("CBN_DelFlag") = dsRow("cbn_DelFlag")

            Permdt.Rows.Add(dtRow)
            'Next
            dsMain.Clear()
            dsMain.Dispose()

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGroupName(ByVal GrpId As Int16, ByVal sNameSpace As String) As String
        Dim strsql As String
        'Dim objDB As New DBGeneral(sConstr, sRDBMS)
        Try
            strsql = "Select Org_Name from Sad_Org_Structure where Org_Node=" & GrpId & ""
            Return (objDb.SQLExecuteScalar(sNameSpace, strsql))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPermCabinets(ByVal sNameSpace As String, ByVal iUserID As String, ByVal sGrpID As String) As String
        Dim strsql As String
        Dim Arr() As String
        Dim i As Integer
        Dim sCabId As String = ""
        Dim sRet As String
        Dim sFArr() As String
        Try
            Arr = Split(sGrpID, ",")
            For i = 0 To UBound(Arr)
                strsql = "edt_cabinet_permission where CBP_Department = " & Arr(i) & " and (CBP_User=" & iUserID & " or CBP_User=0)"
                sRet = objDb.GetAllValues(sNameSpace, "CBP_Cabinet", strsql)
                'sCabId = sCabId & objDB.GetAllValues("CBP_CabId", strsql)
                'If Val(sRet) <> 0 Then
                If Len(sRet) <> 0 Then
                    'changed by badari on 09-05-2007
                    'If InStr(sRet, ";") <> 0 Then
                    If Right(sRet, 1) = ";" Then
                        sRet = Left(sRet, Len(sRet) - 1)
                    End If
                    sCabId = sCabId & ";" & sRet & ";"
                End If
            Next
            strsql = "Edt_cabinet_permission where cbp_permissiontype = 'E'"
            sCabId = sCabId & objDb.GetAllValues(sNameSpace, "CBP_Cabinet", strsql)
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
    Public Function GetParGrpID(ByVal iCabId As Integer, ByVal sNameSpace As String) As Integer
        'Dim objDB As New DBGeneral(sConStr, sRDBMS)
        Dim strsql As String
        Try
            strsql = "Select CBN_department from edt_cabinet where CBN_ID=" & iCabId & ""
            Return (objDb.SQLExecuteScalar(sNameSpace, strsql))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Dim iUsrType As Integer
    Private Function GetMainPermDS(ByVal iUsrType As Integer, ByVal iCabId As Integer, ByVal iUserId As Int16, ByVal iGrpId As Int16, ByVal sNameSpace As String, Optional ByVal ChkType As Integer = 0) As DataTable
        Dim strsql, ssql, sCab As String
        Dim dtPerm, dt As DataTable
        Dim dr As OleDb.OleDbDataReader
        Try
            sCab = ""
            'Check For Group Head
            If ChkType = 2 Then
                sMem = GetMemberGroups(sNameSpace, iUserId)
                iUsrType = GetUserType(sNameSpace, iUserId)
            End If
            If (iUsrType = 1) Then
                sPermlvl = "PU"
                strsql = "Select * from edt_cabinet where CBN_id=" & iCabId & ""
                dsMain = objDb.SQLExecuteDataSet(sNameSpace, strsql)

            ElseIf (CheckForGrpHead(sNameSpace, iGrpId, iUserId) = 1) Then
                'If 1 = 1 Then
                sPermlvl = "GH"
                strsql = "Select * from edt_cabinet where CBN_id=" & iCabId & ""
                dsMain = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            ElseIf ChkType <> 1 Then
                'If (CheckForGrpMember(iGrpId, iUserId) = True) Then
                'strsql = "select * from edt_cabinet left outer join edt_cabinet_permission on CBN_ID=cbp_cabid where CBN_ID=" & iCabId & " and CBN_ParGrp=" & iGrpId & " and CBP_CabId not in ( select distinct(CBP_CabId)  from edt_cabinet_permission where CBP_UsrId=" & iUserId & " or CBP_Grpid in (" & sMem & ") or CBP_PType='E') "
                If (sCabPerm = String.Empty) Then
                    sCabPerm = GetPermCabinets(sNameSpace, iUserId, sMem)
                End If
                strsql = " Select *  from edt_cabinet left outer join edt_cabinet_permission on cbn_id=cbp_cabinet where  CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_id= " & iCabId & " "
                If Val(sCabPerm) <> 0 Then
                    strsql = strsql & " and (CBP_cabinet not in (" & sCabPerm & " ) or CBP_cabinet is Null)  "
                End If
                If objDb.DBCheckForRecord(sNameSpace, strsql) = True Then
                    dsMain = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                    sPermlvl = "PG"
                Else
                    GoTo LP
                End If
            Else
LP:             dsMain = BuildPermDataSet(iUserId, iCabId, sMem, ChkType, sNameSpace, iGrpId)
                If dsMain.Tables.Count >= 0 Then                                  'Vijeth 30/01/19
                    Try
                        dtPerm = dsMain.Tables(0)
                        dtPerm = GetFinalPermForDS(sNameSpace, dtPerm)
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
    End Function
    Public Function CheckForGrpHead(ByVal sNameSpace As String, ByVal iGrpId As Int16, ByVal iUsrId As Int16) As Integer
        Dim strsql As String

        Try
            'Dim objDB As New DBGeneral(GetKeyValues("ConnectionString"), GetKeyValues("sRDBMS"))
            strsql = "Select Suo_isDeptHead from Sad_UsersInOtherDept where suo_userId=" & iUsrId & " and suo_deptId=" & iGrpId & ""
            Return objDb.SQLExecuteScalar(sNameSpace, strsql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildPermDataSet(ByVal iUserId As Integer, ByVal iCabId As Integer, ByVal sMem As String, ByVal ChkType As Integer, ByVal sNameSpace As String, ByVal iGrpId As Integer) As Object
        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iCount As Integer
        Dim ssql, ssql1, ssql2 As String
        Dim ds, ds1, ds2 As DataSet
        Dim grp As Integer
        Try
            ssql2 = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "'"
            ds2 = objDb.SQLExecuteDataSet(sNameSpace, ssql2)
            grp = ds2.Tables(0).Rows(0)("SUO_DeptID")
            'sMem = GetMemberGroups(sNameSpace, iUserId)
            'ssql2 = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_UsersID = '" & iUserId & "'"
            'ds2 = objDb.SQLExecuteDataSet(sNameSpace, ssql2)

            'grp = ds2.Tables(0).Rows(0)("SUO_DeptID")

            ssql = "Select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and (cbp_user='" & iUserId & "' or cbp_user=0)"
            ds = objDb.SQLExecuteDataSet(sNameSpace, ssql)
            If (ds.Tables(0).Rows.Count > 0) Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ((grp <> iGrpId) And (ds.Tables(0).Rows(i)("CBP_PermissionType") <> "U")) Then
                        If ((ds.Tables(0).Rows(i)("CBP_OTHER") = 0) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "G")) = True Then
                            ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and (cbp_department='" & iGrpId & "' or cbn_department in (" & grp & ")) and  cbp_user=0 and CBP_OTHER=0 and CBP_PermissionType='G'"   'Vijeth 31/01/2019
                            'ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and (cbp_department='" & grp & "') and  cbp_user=0 and CBP_OTHER=0 and CBP_PermissionType='G'"
                            ds1 = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
                        End If
                    Else
                        If ((ds.Tables(0).Rows(i)("CBP_OTHER") = 1) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "U")) = True Then
                            ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and cbp_user='" & iUserId & "' and CBP_OTHER=1 and CBP_PermissionType='U'"
                            ds1 = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
                            Return ds1
                            Exit Function
                        ElseIf ((ds.Tables(0).Rows(i)("CBP_OTHER") = 0) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "G")) = True Then
                            ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and cbp_department in (select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "') and  cbp_user=0 and CBP_OTHER=0 and CBP_PermissionType='G'"
                            ds1 = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
                        ElseIf ((ds.Tables(0).Rows(i)("CBP_OTHER") = 0) And (ds.Tables(0).Rows(i)("CBP_PermissionType") = "U")) = True Then
                            If (ds.Tables(0).Rows.Count <= 1) Then
                                If (ds.Tables(0).Rows(i)("CBP_Department") <> ds.Tables(0).Rows(i)("CBN_Department")) Then
                                    Dim MyDt As New DataSet
                                    Return MyDt
                                    Exit Function
                                Else
                                    ssql1 = "select * from view_cabpermissions where cbp_cabinet='" & iCabId & "' and cbp_department in (select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID = '" & iUserId & "')"
                                    ds1 = objDb.SQLExecuteDataSet(sNameSpace, ssql1)
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
            'objParam(iCount) = New OleDb.OleDbParameter("@p_UsrId", OleDb.OleDbType.Numeric)
            'objParam(iCount).Value = iUserId
            'objParam(iCount).Direction = ParameterDirection.Input
            'iCount = iCount + 1

            'objParam(iCount) = New OleDb.OleDbParameter("@p_CabId", OleDb.OleDbType.Numeric)
            'objParam(iCount).Value = iCabId
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
            '    Dim arr() As Object = objDb.SPFrLoadingUsingDsParam(sNameSpace, "GetPerDetails", 1, "@p_iRetLvl", objParam)
            '    If IsDBNull(arr(1)) = False Then
            '        sPermlvl = arr(1)
            '    Else
            '        sPermlvl = ""
            '    End If
            '    Return arr(0)
            'Else
            '    Return (objDb.SPFrLoadingUsingDs(sNameSpace, "GetPerDetails", objParam))
            'End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetFinalPermForDS(ByVal sNameSpace As String, ByVal dtCab As DataTable) As DataTable
        'Dim dtPerm As DataTable
        Dim dr As DataRow
        'Dim sGrp As String = ""
        Dim iCSC, iVCB, iDCB, iMCB, iIND, iSRH, iCFD As Byte
        Try
            For Each dr In dtCab.Rows
                If (UCase(sPermlvl) <> "GH" And UCase(sPermlvl) <> "PG") Then
                    If (dr("CBP_Create") = 1) Then
                        iCSC = 1
                    End If
                    If (dr("CBP_Modify") = 1) Then
                        iMCB = 1
                    End If
                    'If (dr("CBP_Delete") = 1) Then   'vijeth
                    '    iDCB = 1
                    'End If
                    'If (dr("CBP_Create_Folder") = 1) Then
                    '    iCFD = 1
                    'End If
                    If (dr("CBP_Search") = 1) Then
                        iSRH = 1
                    End If
                    If (dr("CBP_Index") = 1) Then
                        iIND = 1
                    End If
                    If (dr("CBP_View") = 1) Then
                        iVCB = 1
                    End If
                    'sGrp = sGrp & "," & dr("CBP_GrpId")
                End If
            Next
            dtCab.BeginInit()
            dtCab.Rows(0).Item("CBP_Create") = iCSC
            dtCab.Rows(0).Item("CBP_Modify") = iMCB
            dtCab.Rows(0).Item("CBP_View") = iVCB
            ' dtCab.Rows(0).Item("CBP_Delete") = iDCB     'Vijeth
            ' dtCab.Rows(0).Item("CBP_Create_Folder") = iCFD
            dtCab.Rows(0).Item("CBP_Index") = iIND
            dtCab.Rows(0).Item("CBP_Search") = iSRH
            dtCab.EndInit()
            dtCab.AcceptChanges()
            Return dtCab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildPermTable() As DataTable
        Dim PermDt As New DataTable
        Dim dc As DataColumn
        Try
            ' PermDt.Columns.Add("CBN_UserId")
            PermDt.Columns.Add("CBN_ID")
            PermDt.Columns.Add("CBN_Name")
            PermDt.Columns.Add("CBN_Note")
            ' PermDt.Columns.Add("CBN_Parent")
            PermDt.Columns.Add("CBN_SubCabCount")
            PermDt.Columns.Add("CBN_FolderCount")
            PermDt.Columns.Add("Cbn_department")
            'PermDt.Columns.Add("org_name")
            'PermDt.Columns.Add("Org_Node")
            PermDt.Columns.Add("CBN_CreatedOn")
            PermDt.Columns.Add("CBN_CreatedBy")
            PermDt.Columns.Add("CBN_DelFlag")



            Return PermDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabBindDetails(ByVal sNameSpace As String, ByVal iCabinet As Integer)
        Dim dt As New DataTable
        Dim ssql As String
        'dt = objcab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, iCabinet, sDeptID, sSession.UserID)
        Try


            'ssql = "Select a.CBN_ID,a.CBN_NAME,a.CBN_NOTE,a.CBN_SubCabCount,a.CBN_FolderCount,a.CBN_Department,a.CBN_CreatedBy,a.CBN_CreatedOn,a.CBN_DelFlag,b.Org_Name,b.Org_node "
            '    sSql = sSql & "from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_node and CBN_Parent=-1 and CBN_DelFlag<>'V' "

            'ssql = ssql & " And a.CBN_ID=" & iCabinet & ""


            'ssql = sSql & "order by a.CBN_NAME"
            'dt = objDb.SQLExecuteDataTable(sNameSpace, ssql)



            ssql = "Select * from edt_cabinet where cbn_id='" & iCabinet & "'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, ssql)
            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function GetAllCab(ByVal sNameSpace As String, ByVal status As String, ByVal userid As Integer)
        Dim dt As New DataTable, dt2 As DataTable
        Dim dRow As DataRow
        Dim ssql As String
        Dim depname As String
        Dim dr As OleDb.OleDbDataReader
        Try

            dt.Columns.Add("CBN_ID")
            dt.Columns.Add("CBN_Name")
            dt.Columns.Add("CBN_Note")
            dt.Columns.Add("CBN_SubCabCount")
            dt.Columns.Add("CBN_FolderCount")
            dt.Columns.Add("Cbn_department")
            dt.Columns.Add("CBN_CreatedOn")
            dt.Columns.Add("CBN_CreatedBy")
            dt.Columns.Add("CBN_DelFlag")

            iUsrParGrp = GetUserParGrp(sNameSpace, userid)
            iUsrType = GetUserType(sNameSpace, userid)
            If (iUsrType <> 1) Then
                If (status = "De-Activated") Then
                    ssql = "select * from edt_cabinet where CBN_Department=" & iUsrParGrp & " and Cbn_DelFlag='D' and CBN_Parent=-1"
                ElseIf (status = "Waiting for Approval") Then
                    ssql = "select * from edt_cabinet where CBN_Department=" & iUsrParGrp & " and Cbn_DelFlag='W' and CBN_Parent=-1"
                ElseIf (status = "") Then
                    ssql = "select * from edt_cabinet where CBN_Department=" & iUsrParGrp & " and Cbn_DelFlag!='V' and CBN_Parent=-1"
                End If
            Else
                If (status = "De-Activated") Then
                    ssql = "select * from edt_cabinet where Cbn_DelFlag='D' and CBN_Parent=-1"
                ElseIf (status = "Waiting for Approval") Then
                    ssql = "select * from edt_cabinet where Cbn_DelFlag='W' and CBN_Parent=-1"
                ElseIf (status = "") Then
                    ssql = "select * from edt_cabinet where Cbn_DelFlag!='V' and CBN_Parent=-1"
                End If
            End If
            dr = objDb.SQLDataReader(sNameSpace, ssql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    If dr("cbn_DelFlag") = "A" Then
                        dRow("cbn_DelFlag") = "Activated"
                    ElseIf dr("cbn_DelFlag") = "D" Then
                        dRow("cbn_DelFlag") = "De-Activated"
                    ElseIf dr("cbn_DelFlag") = "W" Then
                        dRow("cbn_DelFlag") = "Waiting for Approval"
                    End If
                    dRow("CBN_ID") = dr("CBN_ID")
                    dRow("CBN_Name") = objGen.ReplaceSafeSQL(Trim(dr("CBN_Name")))
                    dRow("CBN_Note") = objGen.ReplaceSafeSQL(Trim(dr("CBN_Note")))
                    dRow("CBN_SubCabCount") = dr("CBN_SubCabCount")
                    dRow("CBN_FolderCount") = dr("CBN_FolderCount")
                    dRow("CBN_CreatedOn") = dr("CBN_CreatedOn")
                    dRow("CBN_Department") = objGen.ReplaceSafeSQL(Trim(GetGroupName(dr("CBN_Department"), sNameSpace)))
                    dRow("Cbn_CreatedBy") = objGen.ReplaceSafeSQL(Trim(getcreatedbyname(sNameSpace, dr("Cbn_CreatedBy"))))
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function RetrievePermissions(ByVal sNameSpace As String, ByVal iCabId As Integer, ByVal iGrpId As Integer, ByVal iUsrId As Integer) As DataTable
        Dim strsql As String

        Dim ds As DataSet
        Dim PerDt As DataTable
        Try
            If (iUsrId = 0 And iGrpId <> -1) Then
                strsql = "Select * from edt_cabinet_Permission where CBP_PermissionType='G' and CBP_Department=" & iGrpId &
                          " and CBP_Cabinet=" & iCabId & " "

                ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                PerDt = BuildPermDt(ds)
                Return PerDt
            ElseIf iGrpId <> -1 Then
                strsql = "Select * from edt_cabinet_Permission where CBP_PermissionType='U' and CBP_Department=" & iGrpId &
                          " and CBP_Cabinet=" & iCabId & " and CBP_User=" & iUsrId & ""   'vijeth
                ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                If (ds.Tables(0).Rows.Count = 0) Then
                    strsql = "Select * from edt_cabinet_Permission where CBP_PermissionType='G' and CBP_Department=" & iGrpId &
                                             " and CBP_Cabinet=" & iCabId & ""

                    ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                    PerDt = BuildPermDt(ds)
                    Return PerDt
                Else
                    PerDt = BuildPermDt(ds)
                    Return PerDt
                End If
            ElseIf iGrpId = -1 Then
                strsql = "Select * from edt_cabinet_Permission where CBP_PermissionType ='E' and CBP_Department=0" &
                                          " and CBP_Cabinet=" & iCabId & " and CBP_User=0"
                ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                PerDt = BuildPermDt(ds)
                Return PerDt
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
                        Case "CBP_CREATE"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "CSC"
                            'drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_Create")
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_VIEW")
                            LocalDt.Rows.Add(drPerm)
                        Case "CBP_VIEW"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "VCB"
                            'drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_VIEW")
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_Create")
                            LocalDt.Rows.Add(drPerm)
                        Case "CBP_MODIFY"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "MCB"
                            'drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_MODIFY")
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_MODIFY")
                            LocalDt.Rows.Add(drPerm)
                        Case "CBP_INDEX"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "IDX"
                            ' drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_INDEX")
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_SEARCH")
                            LocalDt.Rows.Add(drPerm)

                        'Case "CBP_DELETE"                                    'vijeth
                        '    drPerm = LocalDt.NewRow
                        '    drPerm("PerName") = "DCB"
                        '    drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_DELETE")
                        '    LocalDt.Rows.Add(drPerm)
                        'Case "CBP_CREATE_FOLDER"
                        '    drPerm = LocalDt.NewRow
                        '    drPerm("PerName") = "CFD"
                        '    drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_CREATE_FOLDER")
                        '    LocalDt.Rows.Add(drPerm)
                        Case "CBP_SEARCH"
                            drPerm = LocalDt.NewRow
                            drPerm("PerName") = "SRH"
                            'drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_SEARCH")
                            drPerm("PerValue") = ds.Tables(0).Rows(0).Item("CBP_INDEX")
                            LocalDt.Rows.Add(drPerm)
                    End Select
                Next
            End If
            Return LocalDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getcreatedbyname(ByVal sNameSpace As String, ByVal icreatedid As Integer)
        Dim ssql As String
        Try
            ssql = "select usr_fullname from sad_userdetails where usr_id='" & icreatedid & "'"
            Return (objDb.SQLExecuteScalar(sNameSpace, ssql))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function chkRemFlagCabPerm(ByVal sUserGrp As String, ByVal iCabId As Integer, ByVal iUserid As Integer, ByVal iGrpID As Integer, ByVal sNameSpace As String) As Boolean

        Dim strsql As String
        Try
            If (sUserGrp = "G") Then
                sUserGrp = "Group"
            Else
                sUserGrp = "User"
            End If
            If (UCase(sUserGrp) = "USER") Then
                strsql = "Select * from edt_cabinet_permission where CBP_Cabinet=" & iCabId & " and CBP_User=" & iUserid & "  and CBP_PermissionType='U'"
                Return objDb.SQLCheckForRecord(sNameSpace, strsql)
            ElseIf (UCase(sUserGrp) = "GROUP") Then
                strsql = "Select * from edt_cabinet_permission where CBP_Cabinet=" & iCabId & " and  CBP_Department=" & iGrpID & " and CBP_PermissionType='G'"
                Return objDb.SQLCheckForRecord(sNameSpace, strsql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ExtendPermissions(ByVal objSrtPer As SrtCabPer, ByVal sCabSub As Char, ByVal sNameSpace As String, ByVal cLvlType As Char, Optional ByVal sLevel As String = "B")
        Dim dsCab, dsFol As DataSet
        Dim i, j As Integer
        Try
            If (sCabSub = "C") Then
                'Check For SubCabinet
                If (CheckForChild("S", objSrtPer.iCabId, sNameSpace) = True) Then
                    dsCab = GetChild("C", objSrtPer.iCabId, sNameSpace)
                    If (dsCab.Tables(0).Rows.Count > 0) Then
                        For i = 0 To dsCab.Tables(0).Rows.Count - 1
                            'If (CheckForUserGrp("S", objSrtPer.cLvlType, ds.Tables(0).Rows(i).Item("CBN_ID"), objSrtPer.iGrpId, objSrtPer.iUsrId, sConStr, sRDBMS) = False) Then
                            InsertIntoSubCab(objSrtPer, sNameSpace, dsCab.Tables(0).Rows(i).Item("CBN_ID"), cLvlType)
                            'End If

                            'Check for Folders within SubCabinets
                            'If the slevel=B it will add the permissions for folder
                            If (sLevel = "B") Then
                                If (CheckForChild("F", dsCab.Tables(0).Rows(i).Item("CBN_ID"), sNameSpace) = True) Then
                                    dsFol = GetChild("S", dsCab.Tables(0).Rows(i).Item("CBN_ID"), sNameSpace)
                                    If (dsFol.Tables(0).Rows.Count > 0) Then
                                        For j = 0 To dsFol.Tables(0).Rows.Count - 1
                                            'If (CheckForUserGrp("F", objSrtPer.cLvlType, ds.Tables(0).Rows(j).Item("Fol_FolID"), objSrtPer.iGrpId, objSrtPer.iUsrId, sConStr, sRDBMS) = False) Then
                                            InsertIntoFolder(objSrtPer, sNameSpace, dsFol.Tables(0).Rows(j).Item("Fol_FolID"), cLvlType)
                                            'End If
                                        Next
                                    End If
                                End If
                            End If

                        Next
                    End If
                End If

                'Check for Folders within cabinets
                'If the slevel=B it will add the permissions for folder
                If (sLevel = "B") Then
                    If (CheckForChild("F", objSrtPer.iCabId, sNameSpace) = True) Then
                        dsFol = GetChild("S", objSrtPer.iCabId, sNameSpace)
                        If (dsFol.Tables(0).Rows.Count > 0) Then
                            For i = 0 To dsFol.Tables(0).Rows.Count - 1
                                'If (CheckForUserGrp("F", objSrtPer.cLvlType, ds.Tables(0).Rows(i).Item("Fol_FolID"), objSrtPer.iGrpId, objSrtPer.iUsrId, sConStr, sRDBMS) = False) Then
                                InsertIntoFolder(objSrtPer, sNameSpace, dsFol.Tables(0).Rows(i).Item("Fol_FolID"), cLvlType)
                                'End If
                            Next
                        End If
                    End If
                End If

            ElseIf (sCabSub = "S") Then
                If (CheckForChild("F", objSrtPer.iCabId, sNameSpace) = True) Then
                    dsFol = GetChild("S", objSrtPer.iCabId, sNameSpace)
                    If (dsFol.Tables(0).Rows.Count > 0) Then
                        For i = 0 To dsFol.Tables(0).Rows.Count - 1
                            If (CheckForUserGrp("F", cLvlType, dsFol.Tables(0).Rows(i).Item("Fol_FolID"), iCBP_Department, iCBP_User, sNameSpace) = False) Then
                                InsertIntoFolder(objSrtPer, sNameSpace, dsFol.Tables(0).Rows(i).Item("Fol_FolID"), cLvlType)
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckForChild(ByVal iScOrFol As Char, ByVal iCabOrScID As Integer, ByVal sNameSpace As String) As Boolean
        Dim strsql As String

        Try
            If (iScOrFol = "S") Then
                strsql = "Select CBN_id from edt_cabinet where CBN_Parent =" & iCabOrScID & ""
                Return objDb.SQLCheckForRecord(sNameSpace, strsql)
            ElseIf (iScOrFol = "F") Then
                strsql = "Select Fol_FolID from edt_Folder where Fol_Cabinet =" & iCabOrScID & ""
                Return objDb.SQLCheckForRecord(sNameSpace, strsql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetChild(ByVal cCabOrSC As Char, ByVal iCabOrSCID As Integer, ByVal sNameSpace As String) As DataSet
        Dim strsql As String

        Dim ds As New DataSet
        Try
            If (cCabOrSC = "C") Then
                strsql = "Select CBN_id from edt_cabinet where CBN_Parent=" & iCabOrSCID & ""
                ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            ElseIf (cCabOrSC = "S") Then
                strsql = "Select Fol_FolID from edt_Folder where Fol_Cabinet=" & iCabOrSCID & ""
                ds = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            End If
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckForUserGrp(ByVal sCabSub As Char, ByVal cLevel As Char, ByVal iCabOrFolId As Integer, ByVal iGrpId As Integer, ByVal iUsrId As Integer, ByVal sNameSpace As String) As Boolean
        Dim strsql As String
        Try
            If (sCabSub = "S") Then
                strsql = " Select * from edt_cabinet_permission where CBP_CabID=" & iCabOrFolId & " and CBP_GrpId=" & iGrpId & "" &
                                       " and CBP_UsrId=" & iUsrId & " and CBP_PType='" & cLevel & "'"
                Return objDb.SQLCheckForRecord(sNameSpace, strsql)
            ElseIf (sCabSub = "F") Then
                strsql = " Select * from edt_Folder_permission where EFP_FOlID=" & iCabOrFolId & " and EFP_GrpId=" & iGrpId & "" &
                                                          " and EFP_UsrId=" & iUsrId & " and EFP_PType='" & cLevel & "'"
                Return objDb.SQLCheckForRecord(sNameSpace, strsql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveCabPermissions(ByVal objSrt As SrtCabPer, ByVal sNameSpace As String, ByVal cLvlType As Char) As String
        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParCount As Int16
        Try
            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_PTYPE", OleDb.OleDbType.VarChar)
            objParam(iParCount).Value = objSrt.cLvlType
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_GRPID", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iGrpId
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_USRID", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iUsrId
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_CREATE", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iCrSubCab
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_MODIFY", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iModCab
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iDelCab
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iCrFol
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iSearch
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iIndex
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iOther
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_VIEW", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Value = objSrt.iView
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@CBP_CabId", OleDb.OleDbType.Numeric)
            objParam(iParCount).Value = objSrt.iCabId
            objParam(iParCount).Direction = ParameterDirection.Input
            iParCount = iParCount + 1

            objParam(iParCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.SmallInt)
            objParam(iParCount).Direction = ParameterDirection.Output

            ' iParCount = objDb.ExecuteStoredProcFrInsert("InOrUpCabPermissions", "@iOper", objParam)
            iParCount = objDb.ExecuteSPForInsert(sNameSpace, "InOrUpCabPermissionsPtype", "@iOper", objParam)
            Return iParCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function InsertIntoSubCab(ByVal objSrtPer As SrtCabPer, ByVal sNameSpace As String, ByVal iScID As Integer, ByVal cleveltype As Char)
        Try
            objSrtPer.iCabId = iScID
            SaveCabPermissions(objSrtPer, sNameSpace, cleveltype)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function InsertIntoFolder(ByVal objSrtPer As SrtCabPer, ByVal sNameSpace As String, ByVal iFolID As Integer, ByVal cleveltype As Char)

        Dim objsrtfol As New SrtFolPer
        Try
            objsrtfol.cLvlType = objSrtPer.cLvlType
            objsrtfol.iCabId = objSrtPer.iCabId
            objsrtfol.iFolId = iFolID
            objsrtfol.iIndex = objSrtPer.iIndex
            objsrtfol.iSearch = objSrtPer.iSearch
            objsrtfol.iGrpId = objSrtPer.iGrpId
            objsrtfol.iUsrId = objSrtPer.iUsrId
            objsrtfol.iViewFol = objSrtPer.iView
            objsrtfol.iModFol = objSrtPer.iModCab
            objsrtfol.iOther = objSrtPer.iOther
            objsrtfol.iDelFol = objSrtPer.iDelCab
            clsfol.SaveFolPermissions(objsrtfol, sNameSpace)
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function LoadSubCabGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal userid As Integer, ByVal icabid As Integer, Optional ByVal sPerm As String = "VCB") As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim dt As New DataTable


        ' Dim dRow As DataRow
        Dim dsCab As DataSet
        Dim strsql As String
        Dim iRet
        'Dim sMem As String6
        Try
            'Modified by Badari.G On 5-3-2007
            Permdt = BuildPermTable()
            sMem = GetMemberGroups(sNameSpace, userid)
            iUsrType = GetUserType(sNameSpace, userid)
            iUsrParGrp = GetUserParGrp(sNameSpace, userid)
            If (iUsrType = 1) Then
                'User Logged is Super User

                strsql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent <> -1 order by CBN_Name"


                dsCab = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                If (dsCab.Tables(0).Rows.Count > 0) Then
                    For Each dRow In dsCab.Tables(0).Rows
                        iRet = GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm)
                        If (iRet = 1) Then
                            AddPermissions(sNameSpace, dtPerm, sPermlvl)

                        End If
                    Next
                End If
                Return Permdt
                Exit Function
            End If
            sCabPerm = GetPermCabinets(sNameSpace, userid, sMem)

            strsql = " Select *  from edt_cabinet where CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_Parent <> -1 "



            If Val(sCabPerm) <> 0 Then
                strsql = strsql & " and CBN_ID Not in (" & sCabPerm & ")"
            End If
            strsql = strsql & " order by CBN_Name"
            dsCab = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            If (dsCab.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsCab.Tables(0).Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm)
                    If (iRet = 1) Then
                        AddPermissions(sNameSpace, dtPerm, sPermlvl)
                    End If
                Next
            End If



            strsql = "Select *  from edt_cabinet where CBN_DelFlag='A' and CBN_Parent <> -1 "
            'CBP_CabId in (" & sCabPerm & ") and 



            If Len((sCabPerm)) <> 0 Then
                strsql = strsql & " and CBN_ID in (" & sCabPerm & ")"
            Else
                strsql = strsql & " and CBN_ID in (0)"
            End If
            strsql = strsql & " order by CBN_Name"
            dsCab = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            If (dsCab.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsCab.Tables(0).Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm, 1)
                    If (iRet = 1) Then
                        AddPermissions(sNameSpace, dtPerm, sPermlvl)
                    End If
                Next
            End If
            Return Permdt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCheckPermissionDep(ByVal cabid As Integer, ByVal depid As Integer, ByVal sNameSpace As String)
        Dim ssql As String
        Dim dt As DataTable
        Try
            ssql = "select * from view_cabpermissions where cbn_id='" & cabid & "' and cbp_department='" & depid & "' and CBP_PermissionType='G'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, ssql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function BindCheckPermissionUser(ByVal cabid As Integer, ByVal depid As Integer, ByVal sNameSpace As String, ByVal userid As Integer)
        Dim ssql As String
        Dim dt As DataTable
        Try
            ssql = "select * from view_cabpermissions where cbn_id='" & cabid & "' and cbp_department='" & depid & "' and CBP_User='" & userid & "' and CBP_PermissionType='U'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, ssql)
            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function getcabinetdept(ByVal nodeid As Integer, ByVal sNameSpace As String)
        Dim ssql, deptname As String
        Dim ds As DataSet

        Try
            ssql = "select * from Sad_Org_Structure where Org_Node='" & nodeid & "'"
            ds = objDb.SQLExecuteDataSet(sNameSpace, ssql)
            'deptname = ds.Tables(0).Rows(0)("Org_Name")
            Return ds
        Catch ex As Exception

        End Try
    End Function

    Public Function CheckCabName(ByVal sAC As String, ByVal sCabName As String, ByVal iCabID As Integer, Optional ByVal iGrpId As Integer = 0) As Boolean
        Dim sSql As String
        Try
            If (iGrpId = 0) Then
                'Assume cabinet name need to be checked for all groups
                sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sCabName & "' and  CBN_ID <>" & iCabID & " and CBN_Parent=-1 and (CBN_DelStatus='A' or CBN_DelStatus='W')"
            Else
                'Check cabinet name only for that group
                sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sCabName & "' and CBN_ParGrp=" & iGrpId & " and CBN_ID <> " & iCabID & "  and CBN_Parent=-1 and (CBN_DelStatus='A' or CBN_DelStatus='W')"
            End If
            Return objDb.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class


