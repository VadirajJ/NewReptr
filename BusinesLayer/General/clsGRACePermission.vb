Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsGRACePermission
    Private objDBL As New DBHelper
    Public Function CheckIsFUNOwnerHODManagerSPOC(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select ENT_ID From mst_Entity_master Where (ENT_FunownerID=" & iUserID & " Or Ent_FunManagerID= " & iUserID & " Or Ent_FunSPOCID= " & iUserID & ") and ENT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndGetCustomerIDFromUserID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_CompanyId From Sad_UserDetails Where Usr_Node=0 and Usr_OrgnID=0 And Usr_ID=" & iUserID & " and Usr_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoginUserPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sModCode As String, iCheckModuleID As Integer) As String
        Dim sSql As String
        Dim iModuleID As Integer, i As Integer, iUserRoleLevel As Integer, iRoleID As Integer
        Dim ds As New DataSet
        Dim sChekALl As String = ""
        Try
            sSql = "Select Usr_GrpOrUserLvlPerm FRom Sad_UserDetails Where Usr_ID=" & iUserID & " and Usr_CompID=" & iACID & ""
            iUserRoleLevel = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "" : sSql = "Select Mod_ID from SAD_MODULE where Mod_Code='" & sModCode & "' and Mod_compid=" & iACID & ""
            iModuleID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            'Check Is Partner
            If iModuleID = 0 Then
                Return False
            Else
                sSql = "" : sSql = "Select Usr_ID from sad_userDetails where Usr_ID=" & iUserID & " And usr_Partner=1 And Usr_CompID=" & iACID & ""
                If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                    sSql = "" : sSql = "Select OP_OperationName from SAD_Mod_Operations where OP_CompID=" & iACID & " And  OP_ModuleID=" & iModuleID & ""
                    ds = objDBL.SQLExecuteDataSet(sAC, sSql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            sChekALl = sChekALl & "," & ds.Tables(0).Rows(i)("OP_OperationName") & ","
                        Next
                        Return sChekALl
                    End If
                Else
                    'Check User/Role
                    If iUserRoleLevel = 1 Then
                        sSql = "" : sSql = "Select Perm_PKID,OP_OperationName From SAD_Mod_Operations a Left Outer Join SAD_UsrOrGrp_Permission b On a.OP_PKID =b.PErm_OPPKID where b.Perm_CompID=" & iACID & " And "
                        sSql = sSql & " (a.OP_ModuleID=" & iModuleID & ") And b.perm_UsrorGrpID=" & iUserID & " And b.Perm_PType='U' and a.op_compid=" & iACID & ""
                    ElseIf iUserRoleLevel = 0 Then
                        sSql = "" : sSql = "Select "
                        If iCheckModuleID = 1 Then
                            sSql = sSql & " Usr_MasterRole"
                        ElseIf iCheckModuleID = 2 Then
                            sSql = sSql & " Usr_AuditRole"
                        ElseIf iCheckModuleID = 3 Then
                            sSql = sSql & " Usr_RiskModule"
                        ElseIf iCheckModuleID = 4 Then
                            sSql = sSql & " Usr_ComplianceModule"
                        End If
                        sSql = sSql & " From Sad_UserDetails Where Usr_ID=" & iUserID & " And Usr_CompID=" & iACID & ""
                        iRoleID = objDBL.SQLExecuteScalarInt(sAC, sSql)
                        sSql = "" : sSql = "Select Perm_PKID,OP_OperationName From SAD_Mod_Operations a Left Outer Join SAD_UsrOrGrp_Permission b On a.OP_PKID =b.PErm_OPPKID where b.Perm_CompID=" & iACID & " And "
                        sSql = sSql & " (a.OP_ModuleID=" & iModuleID & ") And b.perm_UsrorGrpID=" & iRoleID & " And b.Perm_PType='R' and a.op_compid=" & iACID & ""
                    End If
                    ds = objDBL.SQLExecuteDataSet(sAC, sSql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If IsDBNull(ds.Tables(0).Rows(i)("Perm_PKID")) = False Then
                                sChekALl = sChekALl & "," & ds.Tables(0).Rows(i)("OP_OperationName") & ","
                            Else
                                sChekALl = sChekALl & "," & ds.Tables(0).Rows(i)("OP_OperationName") & ","
                            End If
                        Next
                        Return sChekALl
                    Else
                        Return False
                    End If
                End If
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoginUserModulePermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iCheckModuleID As Integer) As String
        Dim sSql As String
        Dim iModuleID As Integer
        Try
            sSql = "Select Usr_ID from Sad_UserDetails where Usr_ID=" & iUserID & " And usr_Partner=1 And Usr_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            Else
                sSql = "" : sSql = "Select "
                If iCheckModuleID = 1 Then
                    sSql = sSql & "Usr_MasterModule"
                ElseIf iCheckModuleID = 2 Then
                    sSql = sSql & "Usr_AuditModule"
                ElseIf iCheckModuleID = 3 Then
                    sSql = sSql & "Usr_RiskModule"
                ElseIf iCheckModuleID = 4 Then
                    sSql = sSql & "Usr_ComplianceModule"
                End If
                sSql = sSql & " From Sad_UserDetails Where Usr_ID=" & iUserID & " and Usr_CompID=" & iACID & ""
                iModuleID = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iModuleID = 1 Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoginUserModulePermissionForAdmin(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iCheckModuleID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_UserDetails where Usr_ID=" & iUserID & " And usr_Partner=1 And Usr_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndGetAgencyIDFromUserID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_CompanyId From Sad_UserDetails Where Usr_Node=0 and Usr_OrgnID=0 And Usr_ID=" & iUserID & " and Usr_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
