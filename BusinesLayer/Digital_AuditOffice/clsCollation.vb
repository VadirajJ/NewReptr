Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure strCollation
    Dim iCLT_COLLATENO As Integer
    Dim sCLT_COLLATEREF As String
    Dim iCLT_CREATOR As Integer
    Dim iCLT_ALLOW As Integer
    Dim sCLT_Comment As String
    Dim iclt_Group As Integer
    Dim sclt_operation As String
    Dim sclt_operationby As Integer
    Dim iCLT_CompId As Integer
    Dim sCLT_IPAddress As String

    Public Property iCLTCompId() As Integer
        Get
            Return (iCLT_CompId)
        End Get
        Set(ByVal Value As Integer)
            iCLT_CompId = Value
        End Set
    End Property
    Public Property sCLTIPAddress() As String
        Get
            Return (sCLT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCLT_IPAddress = Value
        End Set
    End Property
    Public Property iPkID() As Integer
        Get
            Return (iCLT_COLLATENO)
        End Get
        Set(ByVal Value As Integer)
            iCLT_COLLATENO = Value
        End Set
    End Property
    Public Property sCOLLATEREF() As String
        Get
            Return (sCLT_COLLATEREF)
        End Get
        Set(ByVal Value As String)
            sCLT_COLLATEREF = Value
        End Set
    End Property
    Public Property iCREATOR() As Integer
        Get
            Return (iCLT_CREATOR)
        End Get
        Set(ByVal Value As Integer)
            iCLT_CREATOR = Value
        End Set
    End Property
    Public Property iALLOW() As Integer
        Get
            Return (iCLT_ALLOW)
        End Get
        Set(ByVal Value As Integer)
            iCLT_ALLOW = Value
        End Set
    End Property
    Public Property sComment() As String
        Get
            Return (sCLT_Comment)
        End Get
        Set(ByVal Value As String)
            sCLT_Comment = Value
        End Set
    End Property
    Public Property iGroup() As Integer
        Get
            Return (iclt_Group)
        End Get
        Set(ByVal Value As Integer)
            iclt_Group = Value
        End Set
    End Property
    Public Property sOperation() As String
        Get
            Return (sclt_operation)
        End Get
        Set(ByVal Value As String)
            sclt_operation = Value
        End Set
    End Property
    Public Property iOperationby() As Integer
        Get
            Return (sclt_operationby)
        End Get
        Set(ByVal Value As Integer)
            sclt_operationby = Value
        End Set
    End Property
End Structure
Public Class clsCollation
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsEDICTGeneral As New clsEDICTGeneral
    Public Function LoadCollDetails(ByVal sAC As String, ByVal iUserId As Integer, ByVal iStatus As Integer)
        Dim sSql As String, sGrpId As String = ""
        Dim dt As New DataTable, dtCol As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow
        Try
            sSql = "Select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID=" & iUserId & " Order By SUO_PKID ASC"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    If sGrpId = String.Empty Then
                        sGrpId = 0 & "," & dt.Rows(j)("SUO_DeptID")
                    Else
                        sGrpId = sGrpId & "," & dt.Rows(j)("SUO_DeptID")
                    End If
                Next
            End If

            dtCol.Columns.Add("ColId")
            dtCol.Columns.Add("Name")
            dtCol.Columns.Add("Department")
            dtCol.Columns.Add("Note")
            dtCol.Columns.Add("CrBy")
            dtCol.Columns.Add("CrOn")
            dtCol.Columns.Add("Status")

            sSql = "Select CLT_DelFlag,CLT_COLLATENO,clt_collateref,a.usr_fullname,clt_allow,clt_createdon,clt_comment,clt_group From edt_collate,sad_userdetails a where"
            sSql = sSql & " clt_creator=" & iUserId & " And clt_allow=1 And a.usr_id=clt_Creator"
            If iStatus = 0 Then
                sSql = sSql & " And CLT_DelFlag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CLT_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CLT_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " union Select CLT_DelFlag, clt_collateno, clt_collateref, a.usr_fullname, "
            sSql = sSql & " clt_allow, clt_createdon, clt_comment, clt_group from edt_collate, sad_userdetails a where clT_group In (" & sGrpId & ") And clt_allow=0"
            If iStatus = 0 Then
                sSql = sSql & " And CLT_DelFlag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CLT_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CLT_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " And clt_collateno Not In (Select clt_collateno from edt_collate where clt_creator=" & iUserId & " And clt_allow=1) And a.usr_id=clt_Creator"
            sSql = sSql & " Order By CLT_COLLATENO ASC"
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dtCol.NewRow
                    If IsDBNull(dtdetails.Rows(i)("CLT_COLLATENO")) = False Then
                        dRow("ColId") = dtdetails.Rows(i)("CLT_COLLATENO")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_collateref")) = False Then
                        dRow("Name") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("clt_collateref"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_group")) = False Then
                        If dtdetails.Rows(i)("clt_group") <> 0 Then
                            dRow("Department") = objDBL.SQLGetDescription(sAC, "Select Org_Name from Sad_Org_Structure where Org_Node=" & (dtdetails.Rows(i)("clt_group") & " And Org_LevelCode=3"))
                        Else
                            dRow("Department") = "Non-Groups"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_comment")) = False Then
                        dRow("Note") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("clt_comment"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("usr_fullname")) = False Then
                        dRow("CrBy") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("usr_fullname"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_createdon")) = False Then
                        dRow("CrOn") = objclsEDICTGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("clt_createdon"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("CLT_DelFlag")) = False Then
                        If dtdetails.Rows(i)("CLT_DelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dtdetails.Rows(i)("CLT_DelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dtdetails.Rows(i)("CLT_DelFlag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
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
    Public Function LoadDepartment(ByVal sAC As String, ByVal iCompId As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            If (iUsrId <> 0) Then
                sSql = "Select Org_Node,Org_Name from Sad_Org_Structure Left Join Sad_UsersInOtherDept On SUO_DeptID=Org_Node"
                sSql = sSql & " where Org_DelFlag='A' And Org_CompID=" & iCompId & " And SUO_CompID=" & iCompId & " And SUO_UserID=" & iUsrId & " And Org_LevelCode=3"
            Else
                sSql = "Select Org_Node,Org_Name from Sad_Org_Structure where Org_LevelCode=3 And Org_CompID=" & iCompId & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDetails(ByVal sAC As String, ByVal iColId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CLT_COLLATENO,clt_collateref,a.usr_fullname,clt_allow,clt_group,clt_Createdon,clt_comment,CLT_DelFlag from edt_collate,sad_userdetails a where"
            sSql = sSql & " clt_collateno=" & iColId & " And a.usr_id=clt_creator"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckNameExists(ByVal sAC As String, ByVal sColName As String, Optional ByVal iColId As Integer = 0) As Boolean
        Dim sSql As String
        Dim iRet As Integer
        Try
            If iColId = 0 Then
                sSql = "Select count(*) from edt_collate where clt_collateref='" & sColName & "' "
            Else
                sSql = "Select count(*) from edt_collate where clt_collateref='" & sColName & "' and clt_collateno <>" & iColId & " "
            End If
            iRet = objDBL.SQLExecuteScalar(sAC, sSql)
            If iRet = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub CollationApproveStatus(ByVal sAC As String, ByVal iCompId As Integer, ByVal iUserID As Integer, ByVal iColID As Integer, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update EDT_COLLATE set"
            If sType = "Created" Then
                sSql = sSql & " CLT_DelFlag='A',clt_operation='A',CLT_APPROVEDBY=" & iUserID & ", CLT_APPROVEDON=Getdate()"
            ElseIf sType = "De-Activated" Then
                sSql = sSql & " CLT_DelFlag='D',clt_operation='AD',CLT_DELETEDBY=" & iUserID & ", CLT_DELETEDON=Getdate()"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CLT_DelFlag='A',clt_operation='AR',CLT_RECALLBY=" & iUserID & ", CLT_RECALLON=Getdate()"
            ElseIf sType = "Updated" Then
                sSql = sSql & " CLT_DelFlag='A',clt_operation='AR',CLT_UPDATEDBY=" & iUserID & ", CLT_UPDATEDON=Getdate()"  'Vijeth
            End If
            sSql = sSql & " Where CLT_COLLATENO=" & iColID & " And CLT_CompId=" & iCompId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDetailsID(ByVal sAC As String, ByVal iColID As Integer) As String
        Dim sSql As String, sDetId As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CLD_DocID from edt_collatedoc where CLD_CollateNo=" & iColID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If IsDBNull(dt.Rows(i)("CLD_DocID")) = False Then
                        sDetId = sDetId & "," & dt.Rows(i)("CLD_DocId")
                    End If
                Next
                If sDetId.Length > 0 Then
                    If sDetId.Chars(0).ToString = "," Then
                        sDetId = sDetId.Remove(0, 1)
                    End If
                End If
            End If
            Return sDetId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCollationDetails(ByVal sAC As String, ByVal objstrCollation As strCollation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLT_COLLATENO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollation.iPkID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLT_COLLATEREF", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objstrCollation.sCOLLATEREF
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLT_CREATOR", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollation.iCREATOR
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@clt_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollation.iGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLT_ALLOW", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollation.iALLOW
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLT_Comment", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objstrCollation.sComment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@clt_operation", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objstrCollation.sOperation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@clt_operationby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollation.iOperationby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLT_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrCollation.iCLTCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CLT_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objstrCollation.sCLTIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_COLLATE", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCollDetails(ByVal sAC As String, ByVal iUserId As Integer)
        Dim sSql As String, sGrpId As String = ""
        Dim dt As New DataTable, dtCol As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow
        Try
            sSql = "Select SUO_DeptID from Sad_UsersInOtherDept where SUO_UserID=" & iUserId & " Order By SUO_PKID ASC"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    If sGrpId = String.Empty Then
                        sGrpId = 0 & "," & dt.Rows(j)("SUO_DeptID")
                    Else
                        sGrpId = sGrpId & "," & dt.Rows(j)("SUO_DeptID")
                    End If
                Next
            End If

            dtCol.Columns.Add("ColId")
            dtCol.Columns.Add("Name")
            dtCol.Columns.Add("Department")
            dtCol.Columns.Add("Note")
            dtCol.Columns.Add("CrBy")
            dtCol.Columns.Add("CrOn")
            dtCol.Columns.Add("Status")

            sSql = "Select CLT_DelFlag,CLT_COLLATENO,clt_collateref,a.usr_fullname,clt_allow,clt_createdon,clt_comment,clt_group From edt_collate,sad_userdetails a where"
            sSql = sSql & " clt_creator=" & iUserId & " And clt_allow=1 And a.usr_id=clt_Creator"
            sSql = sSql & " union Select CLT_DelFlag, clt_collateno, clt_collateref, a.usr_fullname, "
            sSql = sSql & " clt_allow, clt_createdon, clt_comment, clt_group from edt_collate, sad_userdetails a where clT_group In (" & sGrpId & ") And clt_allow=0"
            sSql = sSql & " And clt_collateno Not In (Select clt_collateno from edt_collate where clt_creator=" & iUserId & " And clt_allow=1) And a.usr_id=clt_Creator"
            sSql = sSql & " Order By clt_collateref ASC"
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dtCol.NewRow
                    If IsDBNull(dtdetails.Rows(i)("CLT_COLLATENO")) = False Then
                        dRow("ColId") = dtdetails.Rows(i)("CLT_COLLATENO")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_collateref")) = False Then
                        dRow("Name") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("clt_collateref"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_group")) = False Then
                        If dtdetails.Rows(i)("clt_group") <> 0 Then
                            dRow("Department") = objDBL.SQLGetDescription(sAC, "Select Org_Name from Sad_Org_Structure where Org_Node=" & (dtdetails.Rows(i)("clt_group") & " And Org_LevelCode=3"))
                        Else
                            dRow("Department") = "Non-Groups"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_comment")) = False Then
                        dRow("Note") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("clt_comment"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("usr_fullname")) = False Then
                        dRow("CrBy") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("usr_fullname"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("clt_createdon")) = False Then
                        dRow("CrOn") = objclsEDICTGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("clt_createdon"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("CLT_DelFlag")) = False Then
                        If dtdetails.Rows(i)("CLT_DelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dtdetails.Rows(i)("CLT_DelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dtdetails.Rows(i)("CLT_DelFlag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
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
End Class
