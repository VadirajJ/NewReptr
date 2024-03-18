Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsModulePermission
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function LoadModules(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mod_ID,Mod_Parent,Mod_Description,Mod_Code,Mod_Notes From Sad_Module Where Mod_Parent=0 And Mod_DelFlag='X' and "
            sSql = sSql & "Mod_CompID =" & iACID & " order by Mod_ID"
            Return (objDBL.SQLExecuteDataTable(sAC, sSql))
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDigitalFillingModules(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mod_ID,Mod_Parent,Mod_Description,Mod_Code,Mod_Notes From Sad_Module Where Mod_Parent=0 And Mod_DelFlag='X' and "
            sSql = sSql & "Mod_CompID =" & iACID & " and Mod_Code='DGO' order by Mod_ID"
            Return (objDBL.SQLExecuteDataTable(sAC, sSql))
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
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)

            Dim dv As New DataView(ds.Tables(0))
            Dim drv As DataRowView

            For Each drv In dv
                If dtFinalTab Is Nothing = False Then
                    dr = dtFinalTab.NewRow
                    dr("ID") = drv("Mod_ID")
                    dr("Module") = drv("Mod_Description")
                    dr("Navigation") = drv("Mod_NavFunc")
                    dtFinalTab.Rows.Add(dr)
                    PopulateChildModules(sAC, iACID, drv("Mod_ID"), dtFinalTab)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub PopulateChildModules(ByVal sAC As String, ByVal iACID As Integer, ByVal iParentID As Integer, ByRef dtFinalTab As DataTable)
        Dim ds As New DataSet
        Dim dr As DataRow
        Dim sSql As String
        Try
            sSql = "Select * From Sad_Module Where Mod_CompID=" & iACID & " And  Mod_Parent = " & iParentID & " And Mod_DelFlag='X'"
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)

            Dim dv As New DataView(ds.Tables(0))
            Dim drv As DataRowView

            For Each drv In dv
                If dtFinalTab Is Nothing = False Then
                    dr = dtFinalTab.NewRow
                    dr("ID") = drv("Mod_ID")
                    dr("Module") = drv("Mod_Description")
                    dr("Navigation") = drv("Mod_NavFunc")
                    dtFinalTab.Rows.Add(dr)
                    PopulateChildModules(sAC, iACID, drv("Mod_ID"), dtFinalTab)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadUserDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from Sad_UserDetails Where usr_delflag = 'A' and "
            sSql = sSql & " Usr_GrpOrUserLvlPerm='1' and Usr_CompId=" & iACID & " order by Usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCheckPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iModId As Integer, ByVal iUsrorGrpID As Integer, ByVal sPType As String)
        Dim sSql As String
        Dim ds As New DataSet
        Try
            sSql = "Select * From SAD_UsrOrGrp_Permission Where Perm_CompID=" & iACID & " And  Perm_PType = '" & sPType & "' and "
            sSql = sSql & "Perm_UsrORGrpID = " & iUsrorGrpID & " and Perm_ModuleID = " & iModId & ""
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOperation(ByVal sAC As String, ByVal iACID As Integer, ByVal iModID As Integer) As DataSet
        Dim sSql As String
        Dim ds As New DataSet
        Try
            sSql = "Select op_PkID,Op_OperationName From SAD_Mod_Operations Where OP_CompID=" & iACID & " And OP_Status='A' And  Op_ModuleID = " & iModID & ""
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeletePermission(ByVal sAC As String, ByVal iACID As Integer, ByVal sPermissionType As String, ByVal iUsrOrGrpID As Integer, ByVal iModuleID As Integer)
        Dim blnRetValue As Boolean = False
        Dim sStr As String
        Try
            sStr = "DELETE From SAD_UsrOrGrp_Permission Where Perm_CompID=" & iACID & " And  Perm_PType='" & sPermissionType & "' AND "
            sStr = sStr & "Perm_UsrOrGrpID =" & iUsrOrGrpID & " And Perm_ModuleID=" & iModuleID & ""
            objDBL.SQLExecuteNonQuery(sAC, sStr)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveOrUpdatePermission(ByVal sAC As String, ByVal iACID As Integer, ByVal sPermType As String, ByVal UsrORGrpID As Integer,
                                                  ByVal ModuleID As Integer, ByVal iOperationIDs As String, ByVal iCrBy As Integer, ByVal sIPAddress As String) As String
        Dim blnRetValue As Boolean = False
        Dim sStr As String = ""
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(8) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            Dim sModPkIDs() As String
            If iOperationIDs.StartsWith(";") = False Then
                iOperationIDs = ";" & iOperationIDs
            End If
            If iOperationIDs.EndsWith(";") = False Then
                iOperationIDs = iOperationIDs & ";"
            End If
            sModPkIDs = Split(iOperationIDs, ";")
            For i As Integer = 1 To UBound(sModPkIDs) - 1

                iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_PType", OleDb.OleDbType.VarChar, 1)
                ObjParam(iParamCount).Value = sPermType
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_UsrORGrpID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = UsrORGrpID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_ModuleID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = ModuleID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_OpPKID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = sModPkIDs(i)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_Status", OleDb.OleDbType.VarChar, 1)
                ObjParam(iParamCount).Value = "A"
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1


                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_Crby", OleDb.OleDbType.Integer, 1)
                ObjParam(iParamCount).Value = iCrBy
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_CompID", OleDb.OleDbType.Integer, 1)
                ObjParam(iParamCount).Value = iACID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_IPAddress", OleDb.OleDbType.VarChar, 25)
                ObjParam(iParamCount).Value = sIPAddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output

                iRet = objDBL.ExecuteSPForInsert(sAC, "spSAD_UsrOrGrp_Permission", "@iOper", ObjParam)
            Next
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllModuleUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iModuleID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select  usr_Id,(Usr_FullName + ' - ' + Usr_Code) As usr_FullName,Usr_MasterModule,Usr_AuditModule,Usr_RiskModule,Usr_ComplianceModule,Usr_BCMModule from Sad_UserDetails where"
            If iModuleID = 1 Then
                sSql = sSql & " (Usr_MasterModule!='1' or Usr_MasterModule IS NULL) And "
            ElseIf iModuleID = 2 Then
                sSql = sSql & " (Usr_AuditModule!='1' or Usr_AuditModule IS NULL) And "
            ElseIf iModuleID = 3 Then
                sSql = sSql & " (Usr_RiskModule!='1' or Usr_RiskModule IS NULL) And "
            ElseIf iModuleID = 4 Then
                sSql = sSql & " (Usr_ComplianceModule!='1' or Usr_ComplianceModule IS NULL) And "
            End If
            sSql = sSql & " (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') order by usr_FullName"
            Return (objDBL.SQLExecuteDataTable(sAC, sSql))

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModuleUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iModule As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select usr_Id,(Usr_FullName + ' - ' + Usr_Code) As usr_FullName,Usr_MasterModule,Usr_AuditModule,Usr_RiskModule,Usr_ComplianceModule,Usr_BCMModule from Sad_UserDetails where"
            If iModule = 1 Then
                sSql = sSql & " (Usr_MasterModule!='1' or Usr_MasterModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            ElseIf iModule = 2 Then
                sSql = sSql & " (Usr_AuditModule!='1' or Usr_AuditModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            ElseIf iModule = 3 Then
                sSql = sSql & " (Usr_RiskModule!='1' or Usr_RiskModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            ElseIf iModule = 4 Then
                sSql = sSql & " (Usr_ComplianceModule!='1' or Usr_ComplianceModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            End If
            sSql = sSql & " (usr_DelFlag ='A' or usr_DelFlag='B' or usr_DelFlag='L') order by usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUserPermissionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iModuleID As Integer) As DataTable
        Dim sSql As String
        Dim dtDetails As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("Sr.No")
            dt.Columns.Add("usr_Id")
            dt.Columns.Add("SAP Code")
            dt.Columns.Add("Login Name")
            dt.Columns.Add("User full name")
            dt.Columns.Add("Designation")
            dt.Columns.Add("Module Role")

            sSql = "Select usr_Id,usr_Code,usr_LoginName,usr_FullName,usr_Designation,Usr_Role,Usr_MasterModule,Usr_MasterRole,Usr_AuditRole,Usr_RiskRole,Usr_ComplianceRole,Usr_BCMRole,"
            sSql = sSql & " Usr_AuditModule,Usr_RiskModule,Usr_ComplianceModule,Usr_BCMModule,b.Mas_Description As Designation,c.Mas_Description As Role from Sad_UserDetails a"
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master b on b.Mas_ID=a.usr_Designation"
            sSql = sSql & " Left Join SAD_GrpOrLvl_General_Master c on c.Mas_ID=a.Usr_Role"

            If iModuleID = 1 Then
                sSql = sSql & " where Usr_MasterModule='1' and usr_CompID=" & iACID & ""
            ElseIf iModuleID = 2 Then
                sSql = sSql & " where Usr_AuditModule='1' and usr_CompID=" & iACID & ""
            ElseIf iModuleID = 3 Then
                sSql = sSql & " where Usr_RiskModule='1' and usr_CompID=" & iACID & ""
            ElseIf iModuleID = 4 Then
                sSql = sSql & " where Usr_ComplianceModule='1' and usr_CompID=" & iACID & ""
            End If
            sSql = sSql & " order by usr_FullName"

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("Sr.No") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("usr_Id")) = False Then
                        dRow("usr_Id") = dtDetails.Rows(i)("usr_Id")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("usr_Code")) = False Then
                        dRow("SAP Code") = dtDetails.Rows(i)("usr_Code")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("usr_LoginName")) = False Then
                        dRow("Login Name") = dtDetails.Rows(i)("usr_LoginName")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("usr_FullName")) = False Then
                        dRow("User full name") = dtDetails.Rows(i)("usr_FullName")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Designation")) = False Then
                        dRow("Designation") = dtDetails.Rows(i)("Designation")
                    End If
                    If iModuleID = 1 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_MasterRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_MasterRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    ElseIf iModuleID = 2 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_AuditRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_AuditRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    ElseIf iModuleID = 3 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_RiskRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_RiskRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    ElseIf iModuleID = 4 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_ComplianceRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_ComplianceRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub RemoveUserFromModule(ByVal sAC As String, ByVal iACID As Integer, ByVal iModuleID As Integer, ByVal iUserId As Integer)
        Dim sSql As String = ""
        Try
            If iModuleID = 1 Then
                sSql = "Update Sad_UserDetails set Usr_MasterModule=0,Usr_MasterRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            ElseIf iModuleID = 2 Then
                sSql = "Update Sad_UserDetails set Usr_AuditModule=0,Usr_AuditRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            ElseIf iModuleID = 3 Then
                sSql = "Update Sad_UserDetails set Usr_RiskModule=0,Usr_RiskRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            ElseIf iModuleID = 4 Then
                sSql = "Update Sad_UserDetails set Usr_ComplianceModule=0,Usr_ComplianceRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
            If objDBL.SQLExecuteScalarInt(sAC, "Select Usr_LevelGrp From Sad_UserDetails Where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & "") = iModuleID Then
                sSql = "" : sSql = "Update Sad_UserDetails Set Usr_LevelGrp=0,Usr_Role=0 where usr_Id=" & iUserId & " And Usr_CompId=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateModuleToUser(ByVal sAC As String, ByVal iACID As Integer, ByVal iModule As Integer, ByVal iUserID As Integer, ByVal iRoleID As Integer)
        Dim sSql As String = ""
        Dim iCheckModule As Integer
        Try
            iCheckModule = objDBL.SQLExecuteScalarInt(sAC, "Select usr_Id From Sad_UserDetails Where usr_Id=" & iUserID & " And Usr_LevelGrp=0 And Usr_MasterModule=0 And Usr_AuditModule=0 And Usr_RiskModule=0 And Usr_ComplianceModule=0 And Usr_BCMModule=0 And Usr_CompId=" & iACID & "")
            If iModule = 1 Then
                sSql = "Update Sad_UserDetails set Usr_MasterModule=1,Usr_MasterRole=" & iRoleID & " where usr_Id=" & iUserID & " and Usr_CompId=" & iACID & ""
            ElseIf iModule = 2 Then
                sSql = "Update Sad_UserDetails set Usr_AuditModule=1,Usr_AuditRole=" & iRoleID & " where usr_Id=" & iUserID & "  and Usr_CompId=" & iACID & ""
            ElseIf iModule = 3 Then
                sSql = "Update Sad_UserDetails set Usr_RiskModule=1,Usr_RiskRole=" & iRoleID & " where usr_Id=" & iUserID & "  and Usr_CompId=" & iACID & ""
            ElseIf iModule = 4 Then
                sSql = "Update Sad_UserDetails set Usr_ComplianceModule=1,Usr_ComplianceRole=" & iRoleID & " where usr_Id=" & iUserID & "  and Usr_CompId=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
            If iCheckModule > 0 Then
                sSql = "Update Sad_UserDetails set Usr_LevelGrp=" & iModule & ",Usr_Role=" & iRoleID & " where usr_Id=" & iUserID & " and Usr_CompId=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
