Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsSubCabinet
    Dim objDb As New DBHelper
    Dim objGen As New clsEDICTGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objcab As New clsCabinet

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


    Dim Permdt As DataTable
    Dim sMem As String = String.Empty
    Dim iUsrType As Integer
    Dim iUsrParGrp As Integer = 0
    Dim dtPerm As New DataTable
    Dim sPermlvl As String
    Dim sCabPerm As String
    Dim iParGrp As Integer = 0
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
    Public Function LoadCabinet(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDept As String, ByVal sSearch As String, ByVal userid As Integer) As DataSet
        Dim sSql As String = ""
        Dim ssql1 As String
        Dim ds As DataSet
        Dim usertype, memtype As Integer
        Try
            'sSql = "" : sSql = "Select CBN_ID,CBN_NAME from edt_cabinet WHERE CBN_PARENT =-1 and CBN_DelFlag='A' and CBN_CompID =" & iCompID & " "
            'If sSearch <> "" Then
            '    sSql = sSql & " and CBN_NAME like '" & sSearch & "%' "
            'End If
            'sSql = sSql & " order by cbn_name"
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
    Public Function LoadDepartment(ByVal sAC As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Org_node,Org_Name from Sad_Org_Structure where Org_DelFlag='A' and Org_LevelCode = 3 Order by Org_Name"
            dt = objDb.SQLExecuteDataTable(sAC, sSql)
            Return dt
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
            Permdt = objcab.BuildPermTable()
            sMem = objcab.GetMemberGroups(sNameSpace, userid)
            iUsrType = objcab.GetUserType(sNameSpace, userid)
            iUsrParGrp = objcab.GetUserParGrp(sNameSpace, userid)
            If (iUsrType = 1) Then
                'User Logged is Super User

                strsql = "Select * from edt_cabinet where CBN_DelFlag='A' and CBN_Parent <> -1 order by CBN_Name"


                dsCab = objDb.SQLExecuteDataSet(sNameSpace, strsql)
                If (dsCab.Tables(0).Rows.Count > 0) Then
                    For Each dRow In dsCab.Tables(0).Rows
                        iRet = objcab.GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm)
                        If (iRet = 1) Then
                            objcab.AddPermissions(sNameSpace, dtPerm, sPermlvl)

                        End If
                    Next
                End If
                Return Permdt
                Exit Function
            End If
            sCabPerm = objcab.GetPermCabinets(sNameSpace, userid, sMem)

            strsql = " Select *  from edt_cabinet where CBN_Department in (" & sMem & ") and CBN_DelFlag='A' and CBN_Parent <> -1 "



            If Val(sCabPerm) <> 0 Then
                strsql = strsql & " and CBN_ID Not in (" & sCabPerm & ")"
            End If
            strsql = strsql & " order by CBN_Name"
            dsCab = objDb.SQLExecuteDataSet(sNameSpace, strsql)
            If (dsCab.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsCab.Tables(0).Rows
                    iParGrp = dRow("CBN_Department")
                    iRet = objcab.GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm)
                    If (iRet = 1) Then
                        objcab.AddPermissions(sNameSpace, dtPerm, sPermlvl)
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
                    iRet = objcab.GetFinalPermissions(iUsrType, dRow("CBN_ID"), userid, sNameSpace, sPerm, 1)
                    If (iRet = 1) Then
                        objcab.AddPermissions(sNameSpace, dtPerm, sPermlvl)
                    End If
                Next
            End If
            Return Permdt

        Catch ex As Exception
            Throw
        End Try


        'dtDisplay.Columns.Add("CBN_ID")
        'dtDisplay.Columns.Add("CBN_NAME")
        'dtDisplay.Columns.Add("CBN_NOTE")
        'dtDisplay.Columns.Add("CBN_FolderCount")
        'dtDisplay.Columns.Add("Org_Name")
        'dtDisplay.Columns.Add("Org_Node")
        'dtDisplay.Columns.Add("CBN_CreatedBy")
        'dtDisplay.Columns.Add("CBN_CreatedOn")
        'dtDisplay.Columns.Add("CBN_DelFlag")
        'Try
        '    sSql = "" : sSql = "Select a.CBN_ID ,a.CBN_NAME,a.CBN_NOTE,a.CBN_Parent,a.CBN_SubCabCount,a.CBN_FolderCount,a.CBN_Department,"
        '    sSql = sSql & " a.CBN_CreatedBy,a.CBN_CreatedOn,a.CBN_DelFlag,b.Org_Name,b.Org_Node "
        '    sSql = sSql & "from edt_cabinet a,Sad_Org_Structure b where a.CBN_Department=b.Org_Node And a.CBN_Parent='" & iCabinet & "'"
        '    If iSubCabinet > 0 Then
        '        sSql = sSql & " And a.CBN_ID=" & iSubCabinet & ""
        '    End If

        '    sSql = sSql & "order by a.CBN_NAME"
        '    dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

        '    If dt.Rows.Count > 0 Then
        '        For i = 0 To dt.Rows.Count - 1
        '            dRow = dtDisplay.NewRow

        '            dRow("CBN_ID") = dt.Rows(i)("CBN_ID").ToString()

        '            If IsDBNull(dt.Rows(i)("CBN_NAME").ToString()) = False Then
        '                dRow("CBN_NAME") = dt.Rows(i)("CBN_NAME").ToString()
        '            Else
        '                dRow("CBN_NAME") = ""
        '            End If

        '            If IsDBNull(dt.Rows(i)("CBN_NOTE").ToString()) = False Then
        '                dRow("CBN_NOTE") = dt.Rows(i)("CBN_NOTE").ToString()
        '            Else
        '                dRow("CBN_NOTE") = ""
        '            End If

        '            If IsDBNull(dt.Rows(i)("CBN_FolderCount")) = False Then
        '                dRow("CBN_FolderCount") = dt.Rows(i)("CBN_FolderCount")
        '            Else
        '                dRow("CBN_FolderCount") = 0
        '            End If

        '            If IsDBNull(dt.Rows(i)("Org_Name").ToString()) = False Then
        '                dRow("Org_Name") = dt.Rows(i)("Org_Name").ToString()
        '            Else
        '                dRow("Org_Name") = ""
        '            End If

        '            If IsDBNull(dt.Rows(i)("Org_Node").ToString()) = False Then
        '                dRow("Org_Node") = dt.Rows(i)("Org_Node").ToString()
        '            Else
        '                dRow("Org_Node") = 0
        '            End If

        '            If IsDBNull(dt.Rows(i)("CBN_CreatedBy").ToString()) = False Then
        '                dRow("CBN_CreatedBy") = objGenFun.GetUserFullNameFromUserID(sNameSpace, iCompID, dt.Rows(i)("CBN_CreatedBy").ToString())
        '            End If

        '            If IsDBNull(dt.Rows(i)("CBN_CreatedOn").ToString()) = False Then
        '                dRow("CBN_CreatedOn") = objGen.FormatDtForRDBMS(dt.Rows(i)("CBN_CreatedOn").ToString(), "D")
        '            End If

        '            If IsDBNull(dt.Rows(i)("CBN_DelFlag").ToString()) = False Then
        '                If dt.Rows(i)("CBN_DelFlag").ToString() = "A" Then
        '                    dRow("CBN_DelFlag") = "Activated"
        '                ElseIf dt.Rows(i)("CBN_DelFlag").ToString() = "D" Then
        '                    dRow("CBN_DelFlag") = "De-Activated"
        '                ElseIf dt.Rows(i)("CBN_DelFlag").ToString() = "W" Then
        '                    dRow("CBN_DelFlag") = "Waiting for Approval"
        '                End If
        '            End If
        '            dtDisplay.Rows.Add(dRow)
        '        Next
        '    End If
        '    Return dtDisplay
        'Catch ex As Exception
        '    Throw
        'End Try
    End Function
    Public Function UpdateStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatus As String, ByVal iSubCab As String, ByVal sDelFlag As String, ByVal iUsrId As Integer) As Object
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
            sSql = sSql & " Where CBN_ID=" & iSubCab & " and CBN_CompID = " & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubCabinet(ByVal sNameSpace As String, ByVal sSearch As String, ByVal iCBN_NodeId As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Select CBN_ID,CBN_NAME from edt_cabinet WHERE CBN_PARENT<>1 and cbn_DelStatus='A'"
            If sSearch <> "" Then
                sSql = sSql & " and CBN_Parent= '" & iCBN_NodeId & "' and CBN_NAME like '" & sSearch & "%'"
            End If
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
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
    Public Function LoadSubCabdetails(ByVal iCBNID As Integer, ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select  CBN_NAME, CBN_ParGrp,CBN_Note from edt_cabinet where CBN_ID=" & iCBNID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckSubCabName(ByVal sAC As String, ByVal sSubCabName As String, ByVal iSubCabID As Integer, ByVal iCabnet As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sSubCabName & "' and CBN_ID <>" & iSubCabID & " "
            sSql = sSql & " and (CBN_DelFlag ='A' or CBN_DelFlag ='W') and CBN_Parent=" & iCabnet & ""
            Return objDb.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPermission(ByVal sNameSpace As String, ByVal iDepartment As Integer, ByVal iUser As Integer, ByVal iSubCab As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from edt_cabinet_Permission where CBP_Department=" & iDepartment & " and CBP_User=" & iUser & " and CBP_Cabinet=" & iSubCab & " "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubCabDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objSubCab As clsSubCabinet) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_NAME", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSubCab.sCBN_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Note", OleDb.OleDbType.VarChar, 7999)
            ObjParam(iParamCount).Value = objSubCab.sCBN_Note
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_PARENT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_Parent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_USERID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_UserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_SubCabCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_SubCabCount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_FolderCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_FolderCount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objSubCab.sCBN_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objSubCab.sCBN_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_UpdatedBy
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
    Public Function SavePermission(ByVal sNameSpace As String, ByVal objCab As clsSubCabinet, ByVal SArray As Array) As Array
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
            ObjParam(iParamCount).Value = SArray(6)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(1)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_MODIFY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(2)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(3)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(4)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = SArray(5)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_Others 'Vijeth
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.Integer, 4)
            ' ObjParam(iParamCount).Value = SArray(4)
            ObjParam(iParamCount).Value = SArray(1) 'Vijeth
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
    Public Sub DeleteCabPermission(ByVal sNameSpace As String, ByVal iCBP_GrpID As Integer, ByVal iCBP_USRID As Integer, ByVal iCBP_CabID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Delete edt_cabinet_Permission where CBP_Department='" & iCBP_GrpID & "' and CBP_Cabinet='" & iCBP_CabID & "' "
            If iCBP_USRID > 0 Then
                sSql = sSql & "and CBP_User='" & iCBP_USRID & "'"
            End If
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateSubCabDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDepartment As Integer, ByVal iCBN_ID As Integer)
        Dim strsql As String = "", aSql As String = ""
        Try
            ''Update Sub cabinet count
            'strsql = "" : strsql = "Update edt_cabinet set CBN_SubCabCount=(Select count(CBN_ID) from Edt_Cabinet where "
            'strsql = strsql & "CBN_Parent =" & iCBN_ID & " And (CBN_DelFlag='A' or CBN_DelFlag='W')) where CBN_ID=" & iCBN_ID & " and CBN_CompID =" & iCompID & ""
            'objDb.SQLExecuteNonQuery(sNameSpace, strsql)

            'Update Sub cabinet count
            strsql = "" : strsql = "Update edt_cabinet set CBN_SubCabCount=(Select count(CBN_ID) from Edt_Cabinet where "
            strsql = strsql & "CBN_Parent =" & iCBN_ID & " And (CBN_DelFlag='A')) where CBN_ID=" & iCBN_ID & " and CBN_CompID =" & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, strsql)

            'Update folder count
            aSql = "" : aSql = "Update edt_cabinet set CBN_FolderCount=(select count(Fol_folid) from edt_folder where "
            aSql = aSql & "fol_cabinet in (Select CBN_ID from Edt_Cabinet where CBN_Parent=" & iCBN_ID & " And (CBN_DelFlag='A'))) where CBN_ID=" & iCBN_ID & " and CBN_CompID =" & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, aSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function DepartmentId(ByVal sNameSpace As String, ByVal iCBNID As Integer)
        Dim sSql As String
        Try
            sSql = "Select CBN_Department from edt_cabinet where CBN_ID='" & iCBNID & "'"
            Return objDb.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllCab(ByVal sNameSpace As String, ByVal status As String, ByVal cabid As Integer, ByVal userid As Integer)
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

            iUsrType = objcab.GetUserType(sNameSpace, userid)
            iUsrParGrp = objcab.GetUserParGrp(sNameSpace, userid)
            If (iUsrType <> 1) Then
                If (status = "De-Activated") Then
                    ssql = "select * from edt_cabinet where CBN_Department=" & iUsrParGrp & " and Cbn_DelFlag='D' and cbn_parent='" & cabid & "'"
                ElseIf (status = "Waiting for Approval") Then
                    ssql = "select * from edt_cabinet where CBN_Department=" & iUsrParGrp & " and Cbn_DelFlag='W' and cbn_parent='" & cabid & "'"
                ElseIf (status = "") Then
                    ssql = "select * from edt_cabinet where CBN_Department=" & iUsrParGrp & " and Cbn_DelFlag!='V' and cbn_parent='" & cabid & "'"
                End If
            Else
                If (status = "De-Activated") Then
                    ssql = "select * from edt_cabinet where Cbn_DelFlag='D' and cbn_parent='" & cabid & "'"
                ElseIf (status = "Waiting for Approval") Then
                    ssql = "select * from edt_cabinet where Cbn_DelFlag='W' and cbn_parent='" & cabid & "'"
                ElseIf (status = "") Then
                    ssql = "select * from edt_cabinet where Cbn_DelFlag!='V' and cbn_parent='" & cabid & "'"
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
                    dRow("CBN_Name") = dr("CBN_Name")
                    dRow("CBN_Note") = dr("CBN_Note")
                    dRow("CBN_SubCabCount") = dr("CBN_SubCabCount")
                    dRow("CBN_FolderCount") = dr("CBN_FolderCount")
                    dRow("CBN_CreatedOn") = dr("CBN_CreatedOn")
                    dRow("CBN_Department") = objcab.GetGroupName(dr("CBN_Department"), sNameSpace)
                    dRow("Cbn_CreatedBy") = objcab.getcreatedbyname(sNameSpace, dr("Cbn_CreatedBy"))
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function GetCabinetInfo(ByVal sNameSpace As String, ByVal isubcab As Integer, ByVal iparent As Integer)
        Dim ssql, ssql1 As String
        Dim icount As Integer
        Dim ds As DataSet
        Try
            ssql = "select * from edt_cabinet where CBN_ID=" & isubcab & ""
            ds = objDb.SQLExecuteDataSet(sNameSpace, ssql)
            ssql1 = "select Count(*) from edt_cabinet_Permission where CBP_Cabinet=" & ds.Tables(0).Rows(0)("CBN_Parent") & " and (CBP_PermissionType='G' or CBP_PermissionType='U') and (CBP_VIEW=1 and(CBP_Create=1 or CBP_Modify=1 or CBP_Delete=1 or CBP_Search=1 or CBP_Index=1))"
            icount = objDb.SQLExecuteScalar(sNameSpace, ssql1)
            Return icount
        Catch ex As Exception

        End Try
    End Function
    Public Function SaveDefaultPermission(ByVal sNameSpace As String, ByVal objSubCab As clsSubCabinet) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PermissionType", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = "G"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBP_Cabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_User", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_Department
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

    Public Function GetSubCabID(ByVal sAC As String, ByVal sSubCabName As String, ByVal iCabnet As Integer) As Integer
        Dim sSql As String = ""
        Dim ds As DataSet
        Dim iSubCabID As Integer = 0
        Try
            sSql = "" : sSql = "Select * from edt_cabinet where CBN_Name='" & sSubCabName & "' and CBN_Parent=" & iCabnet & " and (CBN_DelFlag ='A' or CBN_DelFlag ='W')"
            ds = objDb.SQLExecuteDataSet(sAC, sSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                iSubCabID = ds.Tables(0).Rows(0)("CBN_ID").ToString()
            End If
            Return iSubCabID
        Catch ex As Exception

        End Try
    End Function
End Class
