Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsOrgStructure
    Private objDBL As New DatabaseLayer.DBHelper

    Private org_node As Integer
    Private org_IRDAcode As String
    Private Org_SalesUnitCode As String
    Private Org_BranchCode As String
    Private Org_Name As String
    Private Org_Parent As Integer
    Private Org_Delflag As String
    Private Org_Notes As String
    Private Org_AppStrength As String
    Private Org_CreatedBy As Integer
    Private Org_CreatedOn As Date
    Private Org_UpdatedBy As Integer
    Private Org_UpdatedOn As Date
    Private Org_ApprovedBy As Integer
    Private Org_ApprovedOn As Date
    Private Org_DeletedBy As Integer
    Private Org_DeletedOn As Date
    Private Org_RecallBy As Integer
    Private Org_RecallOn As Date
    Private Org_Status As String
    Private Org_LevelCode As Integer
    Private Org_Cust As String
    Private Org_CompID As Integer

    Public Property iOrgCompID() As Integer
        Get
            Return (Org_CompID)
        End Get
        Set(ByVal Value As Integer)
            Org_CompID = Value
        End Set
    End Property
    Public Property sOrgCust() As String
        Get
            Return (Org_Cust)
        End Get
        Set(ByVal Value As String)
            Org_Cust = Value
        End Set
    End Property
    Public Property iOrgLevelCode() As Integer
        Get
            Return (Org_LevelCode)
        End Get
        Set(ByVal Value As Integer)
            Org_LevelCode = Value
        End Set
    End Property
    Public Property sOrgStatus() As String
        Get
            Return (Org_Status)
        End Get
        Set(ByVal Value As String)
            Org_Status = Value
        End Set
    End Property
    Public Property dOrgRecallOn() As Date
        Get
            Return (Org_RecallOn)
        End Get
        Set(ByVal Value As Date)
            Org_RecallOn = Value
        End Set
    End Property
    Public Property iOrgRecallBy() As Integer
        Get
            Return (Org_RecallBy)
        End Get
        Set(ByVal Value As Integer)
            Org_RecallBy = Value
        End Set
    End Property
    Public Property dOrgDeletedOn() As Date
        Get
            Return (Org_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Org_DeletedOn = Value
        End Set
    End Property
    Public Property iOrgDeletedBy() As Integer
        Get
            Return (Org_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Org_DeletedBy = Value
        End Set
    End Property
    Public Property dOrgApprovedOn() As Date
        Get
            Return (Org_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            Org_ApprovedOn = Value
        End Set
    End Property
    Public Property iOrgApprovedBy() As Integer
        Get
            Return (Org_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Org_ApprovedBy = Value
        End Set
    End Property
    Public Property dOrgUpdatedOn() As Date
        Get
            Return (Org_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            Org_UpdatedOn = Value
        End Set
    End Property
    Public Property iOrgUpdatedBy() As Integer
        Get
            Return (Org_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Org_UpdatedBy = Value
        End Set
    End Property
    Public Property dOrgCreatedOn() As Date
        Get
            Return (Org_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Org_CreatedOn = Value
        End Set
    End Property
    Public Property iOrgCreatedBy() As Integer
        Get
            Return (Org_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Org_CreatedBy = Value
        End Set
    End Property
    Public Property iOrgAppStrength() As Integer
        Get
            Return (Org_AppStrength)
        End Get
        Set(ByVal Value As Integer)
            Org_AppStrength = Value
        End Set
    End Property
    Public Property sOrgNote() As String
        Get
            Return (Org_Notes)
        End Get
        Set(ByVal Value As String)
            Org_Notes = Value
        End Set
    End Property
    Public Property sOrgDelflag() As String
        Get
            Return (Org_Delflag)
        End Get
        Set(ByVal Value As String)
            Org_Delflag = Value
        End Set
    End Property
    Public Property iOrgParent() As Integer
        Get
            Return (Org_Parent)
        End Get
        Set(ByVal Value As Integer)
            Org_Parent = Value
        End Set
    End Property
    Public Property sOrgName() As String
        Get
            Return (Org_Name)
        End Get
        Set(ByVal Value As String)
            Org_Name = Value
        End Set
    End Property
    Public Property sOrgIRDAcode() As String
        Get
            Return (org_IRDAcode)
        End Get
        Set(ByVal Value As String)
            org_IRDAcode = Value
        End Set
    End Property
    Public Property sOrgSalesUnitCode() As String
        Get
            Return (Org_SalesUnitCode)
        End Get
        Set(ByVal Value As String)
            Org_SalesUnitCode = Value
        End Set
    End Property
    Public Property sOrgBranchCode() As String
        Get
            Return (Org_BranchCode)
        End Get
        Set(ByVal Value As String)
            Org_BranchCode = Value
        End Set
    End Property
    Public Property iOrgnode() As Integer
        Get
            Return (org_node)
        End Get
        Set(ByVal Value As Integer)
            org_node = Value
        End Set
    End Property
    Public Function LoadOrgStructure(ByVal sAC As String, ByVal iACID As Integer, Optional ByVal iValue As Integer = 0) As DataTable
        Dim sSql As String
        Try
            sSql = "Select org_node,org_name,org_PARENT from sad_org_structure where org_PARENT='" & iValue & "' and Org_CompID=" & iACID & " order by org_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrgStructureDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iOrgNodeID As Long) As clsOrgStructure
        Dim sSql As String
        Dim myDataSet As New DataSet
        Dim objOrg As New clsOrgStructure
        Try
            sSql = "Select * from sad_org_structure where org_node=" & iOrgNodeID & " And Org_CompID =" & iACID & " order by org_NAME"
            myDataSet = objDBL.SQLExecuteDataSet(sAC, sSql)
            If myDataSet.Tables(0).Rows.Count <> 0 Then
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_Name")) = False Then
                    objOrg.sOrgName = myDataSet.Tables(0).Rows(0).Item("org_Name")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_parent")) = False Then
                    objOrg.iOrgParent = myDataSet.Tables(0).Rows(0).Item("org_parent")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_Code")) = False Then
                    objOrg.sOrgIRDAcode = myDataSet.Tables(0).Rows(0).Item("org_Code")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("Org_SalesUnitCode")) = False Then
                    objOrg.sOrgSalesUnitCode = myDataSet.Tables(0).Rows(0).Item("Org_SalesUnitCode")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("Org_BranchCode")) = False Then
                    objOrg.sOrgBranchCode = myDataSet.Tables(0).Rows(0).Item("Org_BranchCode")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_AppStrength")) = False Then
                    objOrg.iOrgAppStrength = myDataSet.Tables(0).Rows(0).Item("org_AppStrength")
                End If
                If Not IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_Note")) Then
                    objOrg.sOrgNote = myDataSet.Tables(0).Rows(0).Item("org_Note")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_DelFlag")) = False Then
                    objOrg.sOrgDelflag = "" & myDataSet.Tables(0).Rows(0).Item("org_DelFlag")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_status")) = False Then
                    objOrg.sOrgStatus = "" & myDataSet.Tables(0).Rows(0).Item("org_status")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_createdby")) = False Then
                    objOrg.iOrgCreatedBy = myDataSet.Tables(0).Rows(0).Item("org_createdby")
                End If
                If IsDBNull(myDataSet.Tables(0).Rows(0).Item("org_createdon")) = False Then
                    objOrg.dOrgCreatedOn = myDataSet.Tables(0).Rows(0).Item("org_createdon")
                End If
                If Val(myDataSet.Tables(0).Rows(0).Item("Org_levelCode")) <> 0 Then
                    objOrg.iOrgLevelCode = Val(myDataSet.Tables(0).Rows(0).Item("Org_levelCode"))
                End If
            End If
            Return objOrg
        Catch ex As Exception
            Throw
        Finally
            myDataSet = Nothing
        End Try
    End Function
    Public Function GetOrgStructureLevels(ByVal sAC As String, ByVal iACID As Integer, ByVal iSortID As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Dim sOrgStrLvlName As String = ""
        Try
            iSortID = iSortID + 1
            sSql = "Select Mas_Description From sad_Levels_General_Master where mas_compid=" & iACID & " And mas_Delflag ='X' Order by Mas_SortOrder"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dt.Rows.Count > iSortID Then
                sOrgStrLvlName = dt.Rows(iSortID)("Mas_Description").ToString()
            End If
            Return sOrgStrLvlName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOrgLevel(ByVal sAC As String, ByVal iACID As Integer, ByVal iDepth As Integer) As Boolean
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select Count(*) as Depth from sad_Levels_General_Master where mas_compid=" & iACID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If dr("Depth") = iDepth Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadParent(ByVal sAC As String, ByVal iACID As Integer, ByVal iOrgNodeID As Long) As clsOrgStructure
        Dim myDataSet As DataSet
        Dim objOrg As New clsOrgStructure
        Dim sSql As String
        Try
            sSql = "Select org_parent,org_Node,Org_Name from sad_org_structure where Org_CompID=" & iACID & " And org_node=" & iOrgNodeID & " order  by org_NAME"
            myDataSet = objDBL.SQLExecuteDataSet(sAC, sSql)  'to get parent id
            If myDataSet.Tables(0).Rows.Count <> 0 Then
                objOrg.iOrgParent = myDataSet.Tables(0).Rows(0).Item("org_parent")
                objOrg.iOrgnode = myDataSet.Tables(0).Rows(0).Item("org_Node")
                objOrg.sOrgName = myDataSet.Tables(0).Rows(0).Item("Org_Name")
            End If
            Return objOrg
        Catch ex As Exception
            Throw
        Finally
            myDataSet = Nothing
        End Try
    End Function
    Public Function SaveOrgStructure(ByVal sAC As String, ByVal objOrg As clsOrgStructure, ByVal sIPAddress As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_Node", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOrg.iOrgnode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_Code", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objOrg.sOrgIRDAcode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_SalesUnitCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOrg.sOrgSalesUnitCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_BranchCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOrg.sOrgBranchCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objOrg.sOrgName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_Parent", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOrg.iOrgParent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@org_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOrg.sOrgDelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_Note", OleDb.OleDbType.VarChar, 225)
            ObjParam(iParamCount).Value = objOrg.sOrgNote
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_AppStrength", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOrg.iOrgAppStrength
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOrg.iOrgCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOrg.iOrgCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objOrg.sOrgStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_LevelCode", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objOrg.iOrgLevelCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Org_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOrg.iOrgCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spOrgStructure", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOrgCodeExistOrNot(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String, ByVal iOrgNodeID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Org_Node from sad_org_structure where org_code='" & sCode & "' and Org_CompId =" & iACID & ""
            If iOrgNodeID > 0 Then
                sSql = sSql & " And Org_Node<>" & iOrgNodeID & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOrgNameExistOrNot(ByVal sAC As String, ByVal iACID As Integer, ByVal sName As String, ByVal iParent As Integer, iOrgNodeID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Org_Node from sad_org_structure where Upper(Org_Name)= '" & sName & "' and Org_CompId =" & iACID & " and Org_Parent=" & iParent & ""
            If iOrgNodeID > 0 Then
                sSql = sSql & " And Org_Node<>" & iOrgNodeID & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentNodeParentDeptID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String, ByVal sType As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select "
            If sType = "NODE" Then
                sSql = sSql & " org_node "
            ElseIf sType = "PARENT" Then
                sSql = sSql & " Org_Parent "
            ElseIf sType = "LEVEL" Then
                sSql = sSql & " org_LevelCode"
            End If
            sSql = sSql & " from sad_org_structure where Upper(org_code)='" & sCode & "' and Org_CompId =" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetParentName(ByVal sAC As String, ByVal iACID As Integer, ByVal iParent As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Name from sad_org_structure where Org_Node =" & iParent & " and Org_CompId =" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeActivateOrgStructureDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCurrentID As Integer, ByVal iUserId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update sad_org_Structure Set Org_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & "Org_Delflag='A',Org_Status='A',Org_AppBy=" & iUserId & ",Org_AppOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & "Org_Delflag='D',Org_Status='AD',Org_DeletedBy=" & iUserId & ",Org_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & "Org_Delflag='A',Org_Status='AR',Org_RecalledBy=" & iUserId & ",org_RecalledOn=GetDate()"
            End If
            sSql = sSql & " Where Org_Node=" & iCurrentID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckChildElementOrgStructure(ByVal sAC As String, ByVal iACID As Integer, ByVal iCurrentID As Integer, ByVal sStatus As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from sad_org_Structure where Org_Parent=" & iCurrentID & " and Org_CompID=" & iACID & " and"
            If sStatus = "W" Then
                sSql = sSql & " Org_Delflag='W'"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Org_Delflag='A'"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Org_Delflag<>'D'"
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckParentElementOrgStructure(ByVal sAC As String, ByVal iACID As Integer, ByVal iCurrentID As Integer, ByVal sStatus As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From sad_org_Structure Where Org_Node=( Select Org_Parent from sad_org_Structure where "
            sSql = sSql & " Org_Node=" & iCurrentID & " And Org_CompID=" & iACID & ") and  Org_CompID=" & iACID & " and"
            If sStatus = "W" Then
                sSql = sSql & " Org_Delflag='W'"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Org_Delflag<>'A'"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Org_Delflag='D'"
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOrgStructureReport(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable, dtZone As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Dim sZone As String = "", sZone1 As String = "", sRegion As String = "", sRegion1 As String = "", sArea As String = "", sArea1 As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Zone")
            dt.Columns.Add("Region")
            dt.Columns.Add("Area")
            dt.Columns.Add("Branch")

            sSql = "Select Z.Org_Node,Z.Org_Name as Zone,Z.Org_levelCode,R.Org_Node,R.Org_Name As Region,R.Org_levelCode,A.Org_Node,A.Org_Name As Area,A.Org_levelCode,B.Org_Node,B.Org_Name As Branch,B.Org_levelCode "
            sSql = sSql & " From sad_org_Structure Z,sad_org_Structure R,sad_org_Structure A,sad_org_Structure B"
            sSql = sSql & " Where Z.Org_Node=R.Org_Parent And R.Org_Node=A.Org_Parent And A.Org_Node=B.Org_Parent And Z.Org_levelCode>0 "
            sSql = sSql & " Order by Z.Org_Name,R.Org_Name,A.Org_Name,B.Org_Name"
            dtDetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                dRow("Zone") = ""
                sZone = dtDetails.Rows(i)("Zone")
                If sZone1 <> sZone Then
                    dRow("Zone") = sZone
                    sZone1 = sZone
                End If
                dRow("Region") = ""
                sRegion = dtDetails.Rows(i)("Region")
                If sRegion1 <> sRegion Then
                    dRow("Region") = sRegion
                    sRegion1 = sRegion
                End If
                dRow("Area") = ""
                sArea = dtDetails.Rows(i)("Area")
                If sArea1 <> sArea Then
                    dRow("Area") = sArea
                    sArea1 = sArea
                End If
                dRow("Branch") = dtDetails.Rows(i)("Branch")
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserType(ByVal sAC As String, ByVal iACID As Integer, ByVal UserID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "select USR_IsSuperUser from sad_userdetails where USR_ID=" & UserID & ""
            Return (objDBL.SQLExecuteScalar(sAC, sSql))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function InsertSadUsersInOtherDeptList(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal deptId As Integer, ByVal DHead As Integer, ByVal IPadress As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(7) {}
        Dim iParacount As Integer = 0
        Dim Arr(1) As String
        Dim i As Integer
        Try

            iParacount = 0
            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_UserID", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = iUserID
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_DeptID", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = deptId
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_IsDeptHead", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = DHead
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_CreatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = iUserID
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParacount).Value = IPadress
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = iACID
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Direction = ParameterDirection.Output
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "Sp_SadUsersInOtherDept", 1, Arr, ObjParam)
            'Next
            '  Return Arr(1)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadOrgStructurePDFExcelReport(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable, dtZone As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Dim sZone As String = "", sZone1 As String = "", sRegion As String = "", sRegion1 As String = "", sArea As String = "", sArea1 As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Zone")
            dt.Columns.Add("Region")
            dt.Columns.Add("Area")
            dt.Columns.Add("Branch")

            sSql = "Select Z.Org_Node,Z.Org_Name as Zone,Z.Org_levelCode,R.Org_Node,R.Org_Name As Region,R.Org_levelCode,A.Org_Node,A.Org_Name As Area,A.Org_levelCode,B.Org_Node,B.Org_Name As Branch,B.Org_levelCode "
            sSql = sSql & " From sad_org_Structure Z,sad_org_Structure R,sad_org_Structure A,sad_org_Structure B"
            sSql = sSql & " Where Z.Org_Node=R.Org_Parent And R.Org_Node=A.Org_Parent And A.Org_Node=B.Org_Parent And Z.Org_levelCode>=0 "
            sSql = sSql & " Order by Z.Org_Name,R.Org_Name,A.Org_Name,B.Org_Name"
            dtDetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                dRow("Zone") = ""
                sZone = dtDetails.Rows(i)("Zone")
                If sZone1 <> sZone Then
                    dRow("Zone") = sZone
                    sZone1 = sZone
                End If
                dRow("Region") = ""
                sRegion = dtDetails.Rows(i)("Region")
                If sRegion1 <> sRegion Then
                    dRow("Region") = sRegion
                    sRegion1 = sRegion
                End If
                dRow("Area") = ""
                sArea = dtDetails.Rows(i)("Area")
                If sArea1 <> sArea Then
                    dRow("Area") = sArea
                    sArea1 = sArea
                End If
                dRow("Branch") = dtDetails.Rows(i)("Branch")
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckOrgStructureData(ByVal sAC As String, ByVal iACID As Integer, ByVal sOrgName As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iOrgID As Integer = 0
        Try
            sSql = "Select * from sad_org_structure where Org_Name='" + sOrgName + "' and Org_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                iOrgID = dt.Rows(0)("org_Node").ToString()
            End If
            Return iOrgID
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
