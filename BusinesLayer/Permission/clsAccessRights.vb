Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsAccessRights
    Dim objDb As New DBHelper
    Dim objGen As New clsEDICTGeneral

    Dim SGP_ID As Integer
    Dim SGP_ModID As Integer
    Dim SGP_LevelGroup As String
    Dim SGP_LevelGroupID As Integer
    Dim SGP_View As Integer
    Dim SGP_SaveOrUpdate As Integer
    Dim SGP_ActiveOrDeactive As Integer
    Dim SGP_Report As Integer
    Dim SGP_Download As Integer
    Dim SGP_Annotation As Integer
    Dim SGP_CreatedBy As Integer
    Dim SGP_ApprovedBy As Integer
    Dim SGP_UpdatedBy As Integer
    Dim SGP_Status As String
    Dim SGP_DelFlag As String
    Dim SGP_CompID As Integer
    Public Property iSGP_CompID() As Integer
        Get
            Return (SGP_CompID)
        End Get
        Set(ByVal Value As Integer)
            SGP_CompID = Value
        End Set
    End Property
    Public Property sSGP_DelFlag() As String
        Get
            Return (SGP_DelFlag)
        End Get
        Set(ByVal Value As String)
            SGP_DelFlag = Value
        End Set
    End Property
    Public Property sSGP_Status() As String
        Get
            Return (SGP_Status)
        End Get
        Set(ByVal Value As String)
            SGP_Status = Value
        End Set
    End Property
    Public Property iSGP_UpdatedBy() As Integer
        Get
            Return (SGP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SGP_UpdatedBy = Value
        End Set
    End Property
    Public Property iSGP_ApprovedBy() As Integer
        Get
            Return (SGP_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            SGP_ApprovedBy = Value
        End Set
    End Property
    Public Property iSGP_CreatedBy() As Integer
        Get
            Return (SGP_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            SGP_CreatedBy = Value
        End Set
    End Property
    Public Property iSGP_Report() As Integer
        Get
            Return (SGP_Report)
        End Get
        Set(ByVal Value As Integer)
            SGP_Report = Value
        End Set
    End Property
    Public Property iSGP_Download() As Integer
        Get
            Return (SGP_Download)
        End Get
        Set(ByVal Value As Integer)
            SGP_Download = Value
        End Set
    End Property
    Public Property iSGP_Annotation() As Integer
        Get
            Return (SGP_Annotation)
        End Get
        Set(ByVal Value As Integer)
            SGP_Annotation = Value
        End Set
    End Property
    Public Property iSGP_ActiveOrDeactive() As Integer
        Get
            Return (SGP_ActiveOrDeactive)
        End Get
        Set(ByVal Value As Integer)
            SGP_ActiveOrDeactive = Value
        End Set
    End Property
    Public Property iSGP_SaveOrUpdate() As Integer
        Get
            Return (SGP_SaveOrUpdate)
        End Get
        Set(ByVal Value As Integer)
            SGP_SaveOrUpdate = Value
        End Set
    End Property
    Public Property iSGP_View() As Integer
        Get
            Return (SGP_View)
        End Get
        Set(ByVal Value As Integer)
            SGP_View = Value
        End Set
    End Property
    Public Property iSGP_LevelGroupID() As Integer
        Get
            Return (SGP_LevelGroupID)
        End Get
        Set(ByVal Value As Integer)
            SGP_LevelGroupID = Value
        End Set
    End Property
    Public Property sSGP_LevelGroup() As String
        Get
            Return (SGP_LevelGroup)
        End Get
        Set(ByVal Value As String)
            SGP_LevelGroup = Value
        End Set
    End Property
    Public Property iSGP_ModID() As Integer
        Get
            Return (SGP_ModID)
        End Get
        Set(ByVal Value As Integer)
            SGP_ModID = Value
        End Set
    End Property
    Public Property iSGP_ID() As Integer
        Get
            Return (SGP_ID)
        End Get
        Set(ByVal Value As Integer)
            SGP_ID = Value
        End Set
    End Property
    Public Function LoadAcsRgtDetails(ByVal sNameSpace As String, ByVal imodId As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Sgp_Permit,Sgp_Save,Sgp_Delete,Sgp_Modify,Sgp_Print,Sgp_Approve from Sad_UsrOrGrp_permissionDGO where Sgp_Modid=" & imodId & " And Sgp_LvlGrpId =" & iUsrId & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModules(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mod_ID,Mod_Parent,Mod_Description,Mod_Code,Mod_Notes From Sad_Module Where Mod_Parent=0 And Mod_DelFlag='X' and "
            sSql = sSql & "Mod_CompID =" & iACID & " order by Mod_ID"
            Return (objDb.SQLExecuteDataTable(sAC, sSql))
        Catch ex As Exception
            Throw
        End Try
    End Function


    'Access Rights- loading User names for ddlsearch ----
    Public Function LoadAllUserToAccessRights(ByVal sAC As String, ByVal iUserId As Integer) As DataSet
        Dim sSql As String
        Dim strUser As String = ""
        Dim drUsr As OleDb.OleDbDataReader
        Try
            ' If iUserId <> 0 Then
            If iUserId <> 1 Then  'Vijeth
                sSql = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID=" & iUserId & ""
                drUsr = objDb.SQLDataReader(sAC, sSql)
                If drUsr.HasRows Then
                    While drUsr.Read
                        strUser = strUser & "," & drUsr("SUO_DeptID")
                    End While
                    strUser = Right(strUser, Len(strUser) - 1)
                Else
                    strUser = "Null"
                End If

                sSql = "Select USR_ID,USR_FULLNAME + '['+ Org_name +']'as USR_FULLNAME FROM SAD_USERDETAILS,Sad_Org_Structure where usr_Delflag= 'A' and usr_DeptId in (" & strUser & ") and org_node=usr_DeptId and Usr_id <>" & iUserId & " and usr_usrgrpLvlPerm=1 order by USR_FULLNAME"
            Else
                sSql = "Select USR_ID,USR_FULLNAME + '  ['+ Org_name +']'as USR_FULLNAME FROM SAD_USERDETAILS,Sad_Org_Structure where usr_Delflag= 'A' and org_node=usr_DeptId and usr_usrgrpLvlPerm=1 order by USR_FULLNAME"
            End If
            Return objDb.SQLExecuteDataSet(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDepartment(ByVal sNameSpace As String) As DataSet
        Dim sSql As String
        Try
            sSql = "Select Org_Node, Org_Name from Sad_Org_Structure where Org_LevelCode=3 and Org_Delflag='A'"
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Loading Access rights grid
    Public Function LoadGrdPermission(ByVal sNameSpace As String, ByVal iModuleID As Integer) As DataTable
        Dim sSql As String
        Dim i As Integer = 0
        Dim dtPerm As New DataTable
        Dim dRow As DataRow
        Dim drPerm As OleDb.OleDbDataReader

        dtPerm.Columns.Add("Sgp_id")
        dtPerm.Columns.Add("Mod_ID")
        dtPerm.Columns.Add("Mod_Description")
        dtPerm.Columns.Add("View")
        dtPerm.Columns.Add("Save")
        dtPerm.Columns.Add("Active")
        dtPerm.Columns.Add("Report")
        dtPerm.Columns.Add("mod_Function")
        Try
            sSql = "select Mod_ID,Mod_Description,mod_NavFunc from Sad_Module where Mod_Delflag = 'X'"
            If iModuleID > 0 Then
                sSql = sSql & " and Mod_parent=" & iModuleID & ""
            End If
            drPerm = objDb.SQLDataReader(sNameSpace, sSql)
            If drPerm.HasRows Then
                While drPerm.Read
                    dRow = dtPerm.NewRow

                    dRow("Mod_ID") = drPerm(0)
                    dRow("Mod_Description") = drPerm(1)
                    dRow("Sgp_Id") = 0
                    dRow("View") = 0
                    dRow("Save") = 0
                    dRow("Active") = 0
                    dRow("Report") = 0
                    dRow("mod_Function") = drPerm(2)
                    dtPerm.Rows.Add(dRow)
                End While
            End If

            i = dtPerm.Rows.Count
            Return dtPerm
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub GetAllModule(ByVal sAC As String, ByVal iACID As Integer, ByVal iParentID As Integer, ByRef dtFinalTab As DataTable)
        Dim ds As New DataSet
        Dim dr As DataRow
        Dim sSql As String
        Try
            sSql = "Select * From Sad_Module Where Mod_CompID=" & iACID & " And  Mod_Parent = " & iParentID & " And Mod_DelFlag='X'"
            ds = objDb.SQLExecuteDataSet(sAC, sSql)

            Dim dv As New DataView(ds.Tables(0))
            Dim drv As DataRowView

            For Each drv In dv
                If dtFinalTab Is Nothing = False Then
                    dr = dtFinalTab.NewRow
                    dr("Mod_Id") = drv("Mod_ID")
                    dr("Mod_Description") = drv("Mod_Description")
                    dr("mod_Function") = drv("Mod_NavFunc")
                    dr("Mod_Buttons") = drv("Mod_Buttons")
                    dtFinalTab.Rows.Add(dr)
                    PopulateChildModules(sAC, iACID, drv("Mod_ID"), dtFinalTab)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCheckPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iModId As Integer, ByVal iUsrorGrpID As Integer, ByVal sPType As String)
        Dim sSql As String
        Try
            sSql = "Select SGP_View,SGP_SaveOrUpdate,SGP_ActiveOrDeactive,SGP_Report,SGP_Annotaion,SGP_Download From Sad_UsrOrGrp_permissionDGO Where SGP_CompID=" & iACID & " And  SGP_levelGroup = '" & sPType & "' and "
            sSql = sSql & "SGP_levelGroupID = " & iUsrorGrpID & " and SGP_modID = " & iModId & ""
            Return objDb.SQLExecuteDataTable(sAC, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub PopulateChildModules(ByVal sAC As String, ByVal iACID As Integer, ByVal iParentID As Integer, ByRef dtFinalTab As DataTable)
        Dim ds As New DataSet
        Dim dr As DataRow
        Dim sSql As String
        Try
            sSql = "Select * From Sad_Module Where Mod_CompID=" & iACID & " And  Mod_Parent = " & iParentID & " And Mod_DelFlag='X'"
            ds = objDb.SQLExecuteDataSet(sAC, sSql)

            Dim dv As New DataView(ds.Tables(0))
            Dim drv As DataRowView

            For Each drv In dv
                If dtFinalTab Is Nothing = False Then
                    dr = dtFinalTab.NewRow
                    dr("Mod_Id") = drv("Mod_ID")
                    dr("Mod_Description") = drv("Mod_Description")
                    dr("mod_Function") = drv("Mod_NavFunc")
                    dr("Mod_Buttons") = drv("Mod_Buttons")
                    dtFinalTab.Rows.Add(dr)
                    PopulateChildModules(sAC, iACID, drv("Mod_ID"), dtFinalTab)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Function LoadgrpOrUsrGrdPermission(ByVal sNameSpace As String, ByVal iUserOrGrpId As Integer, ByVal sGroup As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim i As Int64
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim drPerm As OleDb.OleDbDataReader
    '    dt.Columns.Add("SGP_ID")
    '    dt.Columns.Add("Mod_Id")
    '    dt.Columns.Add("Mod_Description")
    '    dt.Columns.Add("View")
    '    dt.Columns.Add("Save")
    '    dt.Columns.Add("Active")
    '    dt.Columns.Add("Report")
    '    dt.Columns.Add("mod_Function")
    '    Try
    '        sSql = "" : sSql = "Select Sgp_Id,Sgp_ModId,SM.Mod_Description,SGP_View,SGP_SaveOrUpdate,SGP_ActiveOrDeactive,SGP_Report,SM.mod_NavFunc "
    '        sSql = sSql & "from Sad_UsrOrGrp_permission,Sad_Module SM where SGP_LevelGroup='" & sGroup & "' and "
    '        sSql = sSql & "Mod_Delflag = 'X' And  SGP_LevelGroupID=" & iUserOrGrpId & " And SM.Mod_id =SGP_ModID and SM.Mod_Delflag = 'X' ORDER BY mod_ID"
    '        drPerm = objDb.SQLDataReader(sNameSpace, sSql)
    '        If drPerm.HasRows Then
    '            While drPerm.Read
    '                dRow = dt.NewRow
    '                dRow("SGP_ID") = drPerm(0)
    '                dRow("Mod_Id") = drPerm(1)
    '                dRow("Mod_Description") = drPerm(2)

    '                If drPerm(3) = 1 Then
    '                    dRow("View") = 1
    '                Else
    '                    dRow("View") = 0
    '                End If
    '                If drPerm(4) = 1 Then
    '                    dRow("Save") = 1
    '                Else
    '                    dRow("Save") = 0
    '                End If
    '                If drPerm(5) = 1 Then
    '                    dRow("Active") = 1
    '                Else
    '                    dRow("Active") = 0
    '                End If
    '                If drPerm(6) = 1 Then
    '                    dRow("Report") = 1
    '                Else
    '                    dRow("Report") = 0
    '                End If
    '                dRow("mod_Function") = drPerm(4)
    '                dt.Rows.Add(dRow)
    '            End While
    '            drPerm.Close()
    '        End If
    '        If dt.Rows.Count <> 0 Then
    '            sSql = "" : sSql = "select Mod_id,Mod_Description,mod_NavFunc from sad_module where mod_id not in (select sgp_modid from Sad_UsrOrGrp_permission "
    '            sSql = sSql & " where SGP_LevelGroup ='" & sGroup & "' And SGP_LevelGroupID=" & iUserOrGrpId & " ) and Mod_Delflag = 'X'"
    '            drPerm = objDb.SQLDataReader(sNameSpace, sSql)
    '            If drPerm.HasRows Then
    '                While drPerm.Read
    '                    dRow = dt.NewRow
    '                    dRow("Mod_Id") = drPerm(0)
    '                    dRow("Mod_Description") = drPerm(1)
    '                    dRow("Sgp_Id") = 0
    '                    dRow("View") = ""
    '                    dRow("Save") = ""
    '                    dRow("Active") = ""
    '                    dRow("Report") = ""
    '                    dRow("mod_Function") = drPerm(2)
    '                    dt.Rows.Add(dRow)
    '                End While
    '            End If
    '            i = dt.Rows.Count
    '            i = 13 - i
    '            For irowCount = 0 To i
    '                dRow = dt.NewRow
    '                dRow("Sgp_Id") = -1
    '                dt.Rows.Add(dRow)
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CheckAvailability(ByVal sNameSpace As String, ByVal sGroup As String, ByVal iUserOrGrpId As Integer) As Integer
        Dim StrSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iRet As Integer
        Try
            StrSql = "" : StrSql = "Select Sgp_Id,Sgp_ModId,SGP_LevelGroup, SGP_LevelGroupID from Sad_UsrOrGrp_permissionDGO where "
            StrSql = StrSql & "SGP_LevelGroup = '" & sGroup & "' and SGP_LevelGroupID =" & iUserOrGrpId & ""
            dr = objDb.SQLDataReader(sNameSpace, StrSql)
            If dr.HasRows = True Then
                iRet = 1
            Else
                iRet = 0
            End If
            dr.Close()
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCategoryId(ByVal sNameSpace As String, ByVal iUserId As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select usr_Designation from sad_userdetails where Usr_id=" & iUserId & ""
            Return objDb.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveOrUpdatePermission(ByVal sNameSpace As String, ByVal objPerm As clsAccessRights) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSgp_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ModID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_ModID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_LevelGroup", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objPerm.sSGP_LevelGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_LevelGroupID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_LevelGroupID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_View", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSgp_View
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_SaveOrUpdate", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_SaveOrUpdate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ActiveOrDeactive", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_ActiveOrDeactive
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Report ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Report
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Download ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Download
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Annotaion ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Annotation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objPerm.sSGP_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_DelFlag", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objPerm.sSGP_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spSad_UsrOrGrp_permissionDGO", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckArchivePermission(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal sModCode As String) As String
        Dim sSql As String = "", sArchUsers As String = ""
        Dim dt As New DataTable, dtPerm As New DataTable, dtUsers As New DataTable
        Dim iModuleID As Integer = 0
        Try
            sSql = "" : sSql = "Select Mod_ID from SAD_MODULE where Mod_Code='" & sModCode & "'"
            iModuleID = objDb.SQLExecuteScalarInt(sNameSpace, sSql)

            sSql = "" : sSql = "Select * from Sad_UsrOrGrp_permissionDGO where sgp_modid = " & iModuleID & ""
            dtPerm = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dtPerm.Rows.Count > 0 Then
                For p = 0 To dtPerm.Rows.Count - 1
                    If dtPerm.Rows(p)("SGP_LevelGroup").ToString() = "U" Then
                        sArchUsers = sArchUsers & "," & dtPerm.Rows(p)("SGP_LevelGroupID").ToString()

                    ElseIf dtPerm.Rows(p)("SGP_LevelGroup").ToString() = "R" Then
                        sSql = "" : sSql = "Select * from sad_USerdetails where Usr_Designation =" & dtPerm.Rows(p)("SGP_LevelGroupID").ToString() & " and Usr_Delflag='A' and Usr_CompID=" & iCompID & ""
                        dtUsers = objDb.SQLExecuteDataTable(sNameSpace, sSql)
                        If dtUsers.Rows.Count > 0 Then
                            For m = 0 To dtUsers.Rows.Count - 1
                                sArchUsers = sArchUsers & "," & dtUsers.Rows(m)("Usr_ID").ToString()
                            Next
                        End If
                    End If
                Next
            End If
            Return sArchUsers
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetLoginUserPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sModCode As String) As String
        Dim sSql As String
        Dim iModuleID As Integer, iUserRoleLevel As Integer
        Try
            sSql = "" : sSql = "Select Mod_ID from SAD_MODULE where Mod_Code='" & sModCode & "'"
            iModuleID = objDb.SQLExecuteScalarInt(sAC, sSql)
            'Check Is SuperUser
            If iModuleID = 0 Then
                Return ""
            Else
                sSql = "" : sSql = "Select Usr_ID from sad_userDetails where Usr_ID=" & iUserID & " and Usr_ISSuperUser=1 And Usr_CompID=" & iACID & ""
                If objDb.SQLCheckForRecord(sAC, sSql) = True Then
                    sSql = ",View,SaveOrUpdate,ActiveOrDeactive,Report"
                    Return sSql
                Else
                    sSql = "Select usr_usrgrpLvlPerm FRom Sad_UserDetails Where Usr_ID=" & iUserID & " and Usr_CompID=" & iACID & ""
                    iUserRoleLevel = objDb.SQLExecuteScalarInt(sAC, sSql)

                    'Check User/Dept
                    If iUserRoleLevel = 1 Then
                        sSql = "" : sSql = "Select ',' + Case SGP_View When 1 then 'View' else '' end + ',' + Case SGP_SaveOrUpdate When 1 then 'SaveOrUpdate' else '' end + ',' + Case SGP_ActiveOrDeactive When 1 then 'ActiveOrDeactive' else '' end"
                        sSql = sSql & " + ',' + Case SGP_Report When 1 then 'Report,' else '' end + ',' + Case SGP_Download When 1 then 'Download' else '' end + ',' + Case SGP_Annotaion When 1 then 'Annotation' else '' end "
                        sSql = sSql & "  from Sad_UsrOrGrp_permissionDGO where sgp_Modid=" & iModuleID & " And SGP_LevelGroupID=" & iUserID & " And SGP_LevelGroup='U' and sgp_DelFlag='A'"
                    ElseIf iUserRoleLevel = 0 Then
                        sSql = "" : sSql = "Select ',' + Case SGP_View When 1 then 'View' else '' end + ',' + Case SGP_SaveOrUpdate When 1 then 'SaveOrUpdate' else '' end + ',' + Case SGP_ActiveOrDeactive When 1 then 'ActiveOrDeactive' else '' end"
                        sSql = sSql & " + ',' + Case SGP_Report When 1 then 'Report,' else '' end + ',' + Case SGP_Download When 1 then 'Download' else '' end + ',' + Case SGP_Annotaion When 1 then 'Annotation' else '' end "
                        sSql = sSql & " from Sad_UsrOrGrp_permissionDGO where sgp_Modid=" & iModuleID & " And SGP_LevelGroup ='R' and sgp_DelFlag='A' And"
                        sSql = sSql & "  SGP_LevelGroupID in (Select Usr_Designation From Sad_UserDetails Where Usr_ID=" & iUserID & " And Usr_CompID=" & iACID & ") "
                    End If
                    Return objDb.SQLExecuteScalar(sAC, sSql)
                End If
            End If
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeletePermission(ByVal sAC As String, ByVal iACID As Integer, ByVal sPermissionType As String, ByVal iUsrOrGrpID As Integer, ByVal iModuleID As Integer)
        Dim blnRetValue As Boolean = False
        Dim sStr As String
        Try
            sStr = "DELETE From Sad_UsrOrGrp_permissionDGO Where SGP_CompID=" & iACID & " And  SGP_levelGroup='" & sPermissionType & "' AND "
            sStr = sStr & "SGP_levelGroupID =" & iUsrOrGrpID & " And SGP_modID=" & iModuleID & ""
            objDb.SQLExecuteNonQuery(sAC, sStr)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function IsPermissionSet(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal sLvlGrp As String, ByVal iLvlGrpID As Integer, ByVal iModID As Integer) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select * from Sad_UsrOrGrp_permissionDGO where SGP_modID=" & iModID & " and SGP_levelGroup='" & sLvlGrp & "' and SGP_levelGroupID=" & iLvlGrpID & " and SGP_CompID=" & iACID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CopyDataNewCol(ByVal RefDt As DataTable) As DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("Mod_View", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_Save", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_Active", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_Report", GetType(String))
            RefDt.Columns.Add(dc)
            For Each dr As DataRow In RefDt.Rows
                dr.BeginEdit()
                dr("Mod_View") = ""
                dr("Mod_Save") = ""
                dr("Mod_Active") = ""
                dr("Mod_Report") = ""
                dr.EndEdit()
                dr.AcceptChanges()
            Next
            Return RefDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iModId As String, ByVal iUsrorGrpID As Integer, ByVal sPType As String)
        Dim sSql As String
        Dim dt As New DataTable, dtCol As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow
        Try
            dtCol.Columns.Add("SGP_modID")
            dtCol.Columns.Add("SGP_View")
            dtCol.Columns.Add("SGP_SaveOrUpdate")
            dtCol.Columns.Add("SGP_ActiveOrDeactive")
            dtCol.Columns.Add("SGP_Report")

            sSql = "Select SGP_View,SGP_SaveOrUpdate,SGP_ActiveOrDeactive,SGP_Report,SGP_modID From Sad_UsrOrGrp_permissionDGO Where SGP_CompID=" & iACID & " And  SGP_levelGroup = '" & sPType & "' and "
            sSql = sSql & "SGP_levelGroupID = " & iUsrorGrpID & " and SGP_modID IN(" & iModId & ")"
            dtdetails = objDb.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dtCol.NewRow
                    If IsDBNull(dtdetails.Rows(i)("SGP_modID")) = False Then
                        dRow("SGP_modID") = dtdetails.Rows(i)("SGP_modID")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_View")) = False Then
                        If dtdetails.Rows(i)("SGP_View") = 1 Then
                            dRow("SGP_View") = "Yes"
                        Else
                            dRow("SGP_View") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_SaveOrUpdate")) = False Then
                        If dtdetails.Rows(i)("SGP_SaveOrUpdate") = 1 Then
                            dRow("SGP_SaveOrUpdate") = "Yes"
                        Else
                            dRow("SGP_SaveOrUpdate") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_ActiveOrDeactive")) = False Then
                        If dtdetails.Rows(i)("SGP_ActiveOrDeactive") = 1 Then
                            dRow("SGP_ActiveOrDeactive") = "Yes"
                        Else
                            dRow("SGP_ActiveOrDeactive") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_Report")) = False Then
                        If dtdetails.Rows(i)("SGP_Report") = 1 Then
                            dRow("SGP_Report") = "Yes"
                        Else
                            dRow("SGP_Report") = "No"
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
    Public Function GetAccessRightsDetails(ByVal RefDt As DataTable, ByVal dtTable As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sView As String = "", sSave As String = "", sActive As String = "", sReport As String = ""
        Try
            dt.Columns.Add("Mod_Id")
            dt.Columns.Add("Mod_Description")
            dt.Columns.Add("mod_Function")
            dt.Columns.Add("Mod_View")
            dt.Columns.Add("Mod_Save")
            dt.Columns.Add("Mod_Active")
            dt.Columns.Add("Mod_Report")
            For i = 0 To RefDt.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(RefDt.Rows(i)("Mod_Id")) = False Then
                    dRow("Mod_Id") = RefDt.Rows(i)("Mod_Id")
                End If
                If IsDBNull(RefDt.Rows(i)("Mod_Description")) = False Then
                    dRow("Mod_Description") = RefDt.Rows(i)("Mod_Description")
                End If
                If IsDBNull(RefDt.Rows(i)("mod_Function")) = False Then
                    dRow("mod_Function") = RefDt.Rows(i)("mod_Function")
                End If
                Dim DVdtMaster As New DataView(dtTable)
                DVdtMaster.Sort = "SGP_modID"
                Dim sAppName As String = DVdtMaster.Find(dRow("Mod_Id"))
                If sAppName <> "-1" Then
                    sView = DVdtMaster(sAppName)("SGP_View")
                    dRow("Mod_View") = sView
                    sSave = DVdtMaster(sAppName)("SGP_SaveOrUpdate")
                    dRow("Mod_Save") = sSave
                    sActive = DVdtMaster(sAppName)("SGP_ActiveOrDeactive")
                    dRow("Mod_Active") = sActive
                    sReport = DVdtMaster(sAppName)("SGP_Report")
                    dRow("Mod_Report") = sReport
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function ExtraPermissionsToCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iCabinet As Integer, ByVal sPertypes As String) As String 'vijeth   31/01/19
    '    Dim sSql As String, sSql2 As String, sSql3 As String
    '    Dim iForexist As Integer, iDepatment As String, iUsrType As String, iReturn As Integer
    '    Dim dt As New DataTable, dt2 As New DataTable
    '    Try
    '        sSql3 = "Select USR_DeptID,usr_IsSuperuser from sad_userdetails where usr_id=" & iUserID & ""
    '        dt = objDb.SQLExecuteDataTable(sAC, sSql3)
    '        iDepatment = dt.Rows(0)("USR_DeptID")
    '        iUsrType = dt.Rows(0)("usr_IsSuperuser")
    '        If iUsrType <> 1 Then
    '            sSql2 = "select count(*) from edt_cabinet_Permission where CBP_cabinet=" & iCabinet & ""
    '            iForexist = objDb.SQLExecuteScalar(sAC, sSql2)
    '            If iForexist <> 0 Then
    '                sSql = "select " & sPertypes & " as ELevel,CBP_Other as permission from edt_cabinet_Permission where CBP_user=" & iUserID & " and CBP_PermissionType='U' and CBP_cabinet=" & iCabinet & " and CBP_Department=" & iDepatment & ""
    '                dt2 = objDb.SQLExecuteDataTable(sAC, sSql)
    '                If dt2.Rows(0)("permission") <> 0 Then
    '                    iReturn = dt2.Rows(0)("ELevel")
    '                Else
    '                    sSql = "select " & sPertypes & " from edt_cabinet_Permission where CBP_user= 0 and CBP_PermissionType='G' and CBP_cabinet=" & iCabinet & " and CBP_Department=" & iDepatment & ""
    '                    iReturn = objDb.SQLExecuteScalar(sAC, sSql)
    '                End If
    '                Return iReturn
    '            End If
    '            Return 0
    '        End If
    '        Return 1
    '    Catch ex As Exception
    '    End Try
    'End Function
    Public Function ExtraPermissionsToFolder(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iFolderID As Integer, ByVal sPertypes As String) As String 'vijeth
        Dim sSql As String, sSql2 As String, sSql3 As String, ssql4 As String
        Dim iForexist As Integer, iDepatment As String, iUsrType As String, iReturn As Integer, count As Integer
        Dim dt As New DataTable, dt2 As New DataTable
        Try
            sSql3 = "Select USR_DeptID,usr_IsSuperuser from sad_userdetails where usr_id=" & iUserID & ""
            dt = objDb.SQLExecuteDataTable(sAC, sSql3)
            iDepatment = dt.Rows(0)("USR_DeptID")
            iUsrType = dt.Rows(0)("usr_IsSuperuser")
            If iUsrType <> 1 Then
                sSql2 = "select count(*) from edt_Folder_Permission where EFP_Folid=" & iFolderID & ""
                iForexist = objDb.SQLExecuteScalar(sAC, sSql2)
                If iForexist <> 0 Then
                    ssql4 = "select count(*) from edt_Folder_Permission where EFP_Folid=" & iFolderID & " and EFP_GRPID=" & iDepatment & " and  EFP_USRID=" & iUserID & " and EFP_Other=1"
                    count = objDb.SQLExecuteScalar(sAC, ssql4)
                    If (count <> 0) Then
                        sSql = "select " & sPertypes & " as ELevel,EFP_Other as permission  from edt_Folder_Permission where EFP_USRID=" & iUserID & " and EFP_PTYPE='U' and EFP_Folid=" & iFolderID & " and EFP_GRPID=" & iDepatment & ""
                        dt2 = objDb.SQLExecuteDataTable(sAC, sSql)
                        If dt2.Rows(0)("permission") <> 0 Then
                            iReturn = dt2.Rows(0)("ELevel")
                        Else
                        End If
                    Else
                        sSql = "select " & sPertypes & " from edt_Folder_Permission where EFP_USRID= 0 and EFP_PTYPE='G' and EFP_Folid=" & iFolderID & " and EFP_GRPID=" & iDepatment & ""
                        iReturn = objDb.SQLExecuteScalar(sAC, sSql)

                    End If
                    Return iReturn
                End If
                Return 0
            End If
            Return 1
        Catch ex As Exception
        End Try
    End Function

    'Public Function ExtraViewPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iCabinet As Integer, ByVal sPertypes As String) As String 'vijeth
    '    Dim sSql As String, sSql2 As String, sSql3 As String
    '    Dim iForexist As Integer, iDepatment As String, iUsrType As String, iReturn As Integer
    '    Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable
    '    Try
    '        sSql3 = "Select USR_DeptID,usr_IsSuperuser from sad_userdetails where usr_id=" & iUserID & ""
    '        dt = objDb.SQLExecuteDataTable(sAC, sSql3)
    '        iDepatment = dt.Rows(0)("USR_DeptID")
    '        iUsrType = dt.Rows(0)("usr_IsSuperuser")
    '        If iUsrType <> 1 Then
    '            sSql2 = "select count(*) from edt_cabinet_Permission where CBP_cabinet=" & iCabinet & ""
    '            iForexist = objDb.SQLExecuteScalar(sAC, sSql2)
    '            If iForexist <> 0 Then
    '                sSql = "select " & sPertypes & " as ELevel,CBP_Other as permission,cbp_department from edt_cabinet_Permission where CBP_user=" & iUserID & " and CBP_PermissionType='U' and CBP_cabinet=" & iCabinet & " and CBP_Department=" & iDepatment & ""
    '                dt2 = objDb.SQLExecuteDataTable(sAC, sSql)
    '                If dt2.Rows(0)("permission") <> 0 Then
    '                    iReturn = dt2.Rows(0)("cbp_department")
    '                Else
    '                    sSql = "select " & sPertypes & ", cbp_department from edt_cabinet_Permission where CBP_user= 0 and CBP_PermissionType='G' and CBP_cabinet=" & iCabinet & " and CBP_Department=" & iDepatment & ""
    '                    dt4 = objDb.SQLExecuteDataTable(sAC, sSql)
    '                    iReturn = dt4.Rows(0)("cbp_department")
    '                End If
    '                Return iReturn
    '            End If
    '            Return 0
    '        End If
    '        Return 1
    '    Catch ex As Exception
    '    End Try
    'End Function
    Public Function ExtraPermissionsToCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iCabinet As Integer, ByVal sPertypes As String) As String 'vijeth
        Dim sSql As String, sSql2 As String, sSql3 As String, ssql4 As String
        Dim iForexist As Integer, iDepatment As String, iUsrType As String, iReturn As Integer
        Dim dt As New DataTable, dt2 As New DataTable
        Dim count As Integer
        Try
            sSql3 = "Select USR_DeptID,usr_IsSuperuser from sad_userdetails where usr_id=" & iUserID & ""
            dt = objDb.SQLExecuteDataTable(sAC, sSql3)
            iDepatment = dt.Rows(0)("USR_DeptID")
            iUsrType = dt.Rows(0)("usr_IsSuperuser")
            If iUsrType <> 1 Then
                sSql2 = "select count(*) from edt_cabinet_Permission where CBP_cabinet=" & iCabinet & ""
                iForexist = objDb.SQLExecuteScalar(sAC, sSql2)
                If iForexist <> 0 Then
                    ssql4 = " select count(*) from edt_cabinet_Permission where CBP_cabinet=" & iCabinet & " and CBP_Department=" & iDepatment & " and  CBP_user=" & iUserID & " and cbp_other=1"
                    count = objDb.SQLExecuteScalar(sAC, ssql4)
                    If (count <> 0) Then
                        sSql = "select " & sPertypes & " as ELevel,CBP_Other as permission from edt_cabinet_Permission where CBP_user=" & iUserID & " and CBP_PermissionType='U' and CBP_cabinet=" & iCabinet & " and CBP_Department=" & iDepatment & ""
                        dt2 = objDb.SQLExecuteDataTable(sAC, sSql)
                        If dt2.Rows(0)("permission") <> 0 Then
                            iReturn = dt2.Rows(0)("ELevel")
                        Else
                        End If
                    Else
                        sSql = "select " & sPertypes & " from edt_cabinet_Permission where CBP_user= 0 and CBP_PermissionType='G' and CBP_cabinet=" & iCabinet & " and CBP_Department=" & iDepatment & ""
                        iReturn = objDb.SQLExecuteScalar(sAC, sSql)
                    End If
                    Return iReturn
                End If
                Return 0
            End If
            Return 1
        Catch ex As Exception
        End Try
    End Function
End Class