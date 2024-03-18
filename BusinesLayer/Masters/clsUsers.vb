Imports System
Imports System.Data
Imports DatabaseLayer
Public Structure strUserDetails
    Private iUsrID As Integer
    Private sUsrCode As String
    Private sUsrLoginName As String
    Private sUsrFullName As String
    Private sUsrPassWord As String
    Private sUsrEmail As String
    Private iUsrDesignation As Integer
    Private iUsrIsSuperUser As Integer
    Private iUsrMemberType As Integer
    Private iUsrDeptID As Integer
    Private iUSRSectionID As Integer
    Private iUsrLevelCode As Integer
    Private iUsrLastLogin As Integer
    Private iUsrLastLogOut As Integer
    Private iUsrCrBy As Integer
    Private sUsrStatus As String
    Private sUsrDelFlag As String
    Private sUsrIPAddress As String
    Private sOtherDeptList As String
    Private iUSRusrGrpLvlPerm As Integer
    Public Property iUsr_ID() As Integer
        Get
            Return (iUsrID)
        End Get
        Set(ByVal Value As Integer)
            iUsrID = Value
        End Set
    End Property
    Public Property iUSR_usrGrpLvlPerm() As Integer
        Get
            Return (iUSRusrGrpLvlPerm)
        End Get
        Set(ByVal Value As Integer)
            iUSRusrGrpLvlPerm = Value
        End Set
    End Property
    Public Property sUsr_Code() As String
        Get
            Return (sUsrCode)
        End Get
        Set(ByVal Value As String)
            sUsrCode = Value
        End Set
    End Property
    Public Property sUsr_LoginName() As String
        Get
            Return (sUsrLoginName)
        End Get
        Set(ByVal Value As String)
            sUsrLoginName = Value
        End Set
    End Property
    Public Property sUsr_FullName() As String
        Get
            Return (sUsrFullName)
        End Get
        Set(ByVal Value As String)
            sUsrFullName = Value
        End Set
    End Property
    Public Property sUsr_PassWord() As String
        Get
            Return (sUsrPassWord)
        End Get
        Set(ByVal Value As String)
            sUsrPassWord = Value
        End Set
    End Property
    Public Property sUsr_Email() As String
        Get
            Return (sUsrEmail)
        End Get
        Set(ByVal Value As String)
            sUsrEmail = Value
        End Set
    End Property
    Public Property iUsr_Designation() As Integer
        Get
            Return (iUsrDesignation)
        End Get
        Set(ByVal Value As Integer)
            iUsrDesignation = Value
        End Set
    End Property
    Public Property iUsr_LevelCode() As Integer
        Get
            Return (iUsrLevelCode)
        End Get
        Set(ByVal Value As Integer)
            iUsrLevelCode = Value
        End Set
    End Property
    Public Property iUsr_DeptID() As Integer
        Get
            Return (iUsrDeptID)
        End Get
        Set(ByVal Value As Integer)
            iUsrDeptID = Value
        End Set
    End Property
    Public Property iUSR_SectionID() As Integer
        Get
            Return (iUSRSectionID)
        End Get
        Set(ByVal Value As Integer)
            iUSRSectionID = Value
        End Set
    End Property
    Public Property iUsr_MemberType() As Integer
        Get
            Return (iUsrMemberType)
        End Get
        Set(ByVal Value As Integer)
            iUsrMemberType = Value
        End Set
    End Property
    Public Property iUsr_IsSuperUser() As Integer
        Get
            Return (iUsrIsSuperUser)
        End Get
        Set(ByVal Value As Integer)
            iUsrIsSuperUser = Value
        End Set
    End Property
    Public Property iUsr_LastLogin() As Integer
        Get
            Return (iUsrLastLogin)
        End Get
        Set(ByVal Value As Integer)
            iUsrLastLogin = Value
        End Set
    End Property
    Public Property iUsr_LastLogOut() As Integer
        Get
            Return (iUsrLastLogOut)
        End Get
        Set(ByVal Value As Integer)
            iUsrLastLogOut = Value
        End Set
    End Property
    Public Property iUsr_CrBy() As Integer
        Get
            Return (iUsrCrBy)
        End Get
        Set(ByVal Value As Integer)
            iUsrCrBy = Value
        End Set
    End Property
    Public Property sUsr_IPAddress() As String
        Get
            Return (sUsrIPAddress)
        End Get
        Set(ByVal Value As String)
            sUsrIPAddress = Value
        End Set
    End Property
    Public Property sUsr_Status() As String
        Get
            Return (sUsrStatus)
        End Get
        Set(ByVal Value As String)
            sUsrStatus = Value
        End Set
    End Property
    Public Property sUsr_DelFlag() As String
        Get
            Return (sUsrDelFlag)
        End Get
        Set(ByVal Value As String)
            sUsrDelFlag = Value
        End Set
    End Property
    Public Property sOther_DeptList() As String
        Get
            Return (sOtherDeptList)
        End Get
        Set(ByVal Value As String)
            sOtherDeptList = Value
        End Set
    End Property
End Structure
Public Class clsUsers
    Dim objDBL As New DBHelper
    Dim objclsEDICTGeneral As New clsEDICTGeneral

    Public Function LoadDesignation(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID,MAS_DESCRIPTION from Sad_Designation_Master"
            sSql = sSql & " Left Join sad_userdetails On USR_ID=Mas_CREATEDBY  Where Mas_CompId=" & iCompID & " And USR_CompId=" & iCompID & " and Mas_DelFlag ='A'"
            sSql = sSql & " order by MAS_DESCRIPTION"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCountryGovtDeptSectionMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iLevelCode As Integer, ByVal iParent As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Node,Org_Name from sad_Org_Structure where Org_LevelCode=" & iLevelCode & " And Org_DelFlag='A' And Org_CompID=" & iACID & ""
            If iLevelCode > 1 Then
                sSql = sSql & " And Org_Parent=" & iParent & ""
            End If
            sSql = sSql & " order by Org_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCountryGovtDeptSection(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Org_Node,Org_Name,Org_Parent,Org_levelCode from sad_Org_Structure where Org_CompID=" & iACID & " and Org_levelCode <> ''"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrgParentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iNode As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Org_Parent from sad_org_Structure where org_node=" & iNode & " and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUesrPassword(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As String) As String
        Dim sSql As String
        Try
            sSql = "Select usr_password from Sad_Userdetails where Usr_ID=" & iUserID & " and Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdatePasswordReset(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPasswordReset As Integer)
        Dim sSql As String
        Try
            sSql = "Update Sad_UserDetails set Usr_Status='N',Usr_IsPasswordReset=" & iPasswordReset & " where Usr_ID=" & iUserID & " and Usr_CompId=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadAllUsersToGrID(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim i As Integer = 0
        Dim StrSql As String = ""
        Dim dRow As DataRow
        Dim dtDisplay As New DataTable, dtCountryGovtDeptSectionDetails As New DataTable, dt As New DataTable
        Try
            dtDisplay.Columns.Add("UsrID")
            dtDisplay.Columns.Add("LoginName")
            dtDisplay.Columns.Add("FullName")
            dtDisplay.Columns.Add("Department")
            dtDisplay.Columns.Add("Section")
            dtDisplay.Columns.Add("Designation")
            dtDisplay.Columns.Add("UserType")
            dtDisplay.Columns.Add("LastLogin")
            dtDisplay.Columns.Add("NoOfLogin")
            dtDisplay.Columns.Add("Delflag")

            dtCountryGovtDeptSectionDetails = GetCountryGovtDeptSection(sAC, iACID)

            StrSql = " select a.Usr_LoginName,a.Usr_ID,a.Usr_DeptID,a.Usr_FullName,a.Usr_Email,a.Usr_IsSuperuser,a.Usr_DelFlag,a.Usr_LastLoginDate,"
            StrSql = StrSql & "a.Usr_LevelCode,a.Usr_SectionID,a.Usr_NoOfLogin,Usr_DutyStatus,Usr_DelFlag,b.Mas_Description as Designation from Sad_UserDetails a "
            StrSql = StrSql & " Left Join Sad_Designation_MASTER b ON b.Mas_ID=a.Usr_Designation "
            StrSql = StrSql & " where a.Usr_CompID=" & iACID & " order by a.Usr_LoginName"
            dt = objDBL.SQLExecuteDataTable(sAC, StrSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow

                    dRow("UsrID") = 0 : dRow("LoginName") = "" : dRow("FullName") = "" : dRow("Department") = ""
                    dRow("Department") = "" : dRow("Section") = "" : dRow("Designation") = ""
                    dRow("UserType") = "" : dRow("Delflag") = "" : dRow("LastLogin") = "" : dRow("NoOfLogin") = 0

                    If IsDBNull(dt.Rows(i)("Usr_ID").ToString()) = False Then
                        dRow("UsrID") = dt.Rows(i)("Usr_ID").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_LoginName").ToString()) = False Then
                        dRow("LoginName") = dt.Rows(i)("Usr_LoginName").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_FullName").ToString()) = False Then
                        dRow("FullName") = dt.Rows(i)("Usr_FullName").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_DeptID")) = False And IsDBNull(dt.Rows(i)("Usr_LevelCode")) = False Then
                        Dim dtDSDeatils As New DataTable
                        Dim DVCGDSDetails As New DataView(dtCountryGovtDeptSectionDetails)
                        DVCGDSDetails.RowFilter = "Org_Node=" & dt.Rows(i)("Usr_DeptID") & " And Org_levelCode=3"
                        dtDSDeatils = DVCGDSDetails.ToTable
                        If dtDSDeatils.Rows.Count > 0 Then
                            dRow("Department") = dtDSDeatils.Rows(0)("Org_Name")
                        End If
                        If IsDBNull(dt.Rows(i)("Usr_SectionID")) = False And dt.Rows(i)("Usr_LevelCode") = 4 Then
                            Dim dtDS As New DataTable
                            DVCGDSDetails.RowFilter = "Org_Node=" & dt.Rows(i)("Usr_SectionID") & " And Org_levelCode=4"
                            dtDS = DVCGDSDetails.ToTable
                            If dtDS.Rows.Count > 0 Then
                                dRow("Section") = dtDS.Rows(0)("Org_Name")
                            End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(i)("Designation").ToString()) = False Then
                        dRow("Designation") = dt.Rows(i)("Designation").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_LastLoginDate").ToString()) = False Then
                        If dt.Rows(i)("Usr_LastLoginDate").ToString() <> "" Then
                            dRow("LastLogin") = objclsEDICTGeneral.FormatDtForRDBMS(dt.Rows(i)("Usr_LastLoginDate").ToString(), "D")
                        End If
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_NoOfLogin").ToString()) = False Then
                        dRow("NoOfLogin") = dt.Rows(i)("Usr_NoOfLogin").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_IsSuperuser").ToString()) = False Then
                        If dt.Rows(i)("Usr_IsSuperuser").ToString() = 1 Then
                            dRow("UserType") = "Power User"
                            'ElseIf dt.Rows(i)("Usr_IsSuperuser").ToString() = 2 Then  'Vijeth
                            '    dRow("UserType") = "Super User" 
                        ElseIf dt.Rows(i)("Usr_IsSuperuser").ToString() = 2 Then
                            dRow("UserType") = "Normal User"
                        End If
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_DelFlag").ToString()) = False Then
                        If dt.Rows(i)("Usr_DutyStatus") = "W" Then
                            dRow("Delflag") = "Waiting for Approval"
                        ElseIf dt.Rows(i)("Usr_DutyStatus") = "D" Then
                            dRow("Delflag") = "De-Activated"
                        ElseIf (dt.Rows(i)("Usr_DutyStatus") = "A") Then
                            dRow("Delflag") = "Activated"
                        ElseIf dt.Rows(i)("Usr_DutyStatus") = "L" Then
                            dRow("Delflag") = "Locked"
                        ElseIf dt.Rows(i)("Usr_DutyStatus") = "B" Then
                            dRow("Delflag") = "Blocked"
                        End If
                    End If

                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUserOtherDeptDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iParentID As Integer, ByVal IDept As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtDept As New DataTable
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim i As Integer = 0
        Try
            dtDept.Columns.Add("DeptID")
            dtDept.Columns.Add("Department")
            dtDept.Columns.Add("IsDeptSelected")
            dtDept.Columns.Add("IsDeptHeadSelected")

            sSql = "Select Org_Node,Org_Name from sad_Org_Structure where Org_LevelCode=3 And Org_DelFlag='A' And Org_CompID=" & iACID & ""
            'If iParentID > 1 Then
            '    sSql = sSql & " And Org_Parent=" & iParentID & ""   'Vijeth 08/0/2019
            'End If
            If IDept > 0 Then
                sSql = sSql & " And Org_Node<>" & IDept & ""
            End If
            sSql = sSql & " order by Org_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDept.NewRow
                    If IsDBNull(dt.Rows(i)("Org_Node").ToString()) = False Then
                        dRow("DeptID") = dt.Rows(i)("Org_Node").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Org_Name").ToString()) = False Then
                        dRow("Department") = dt.Rows(i)("Org_Name").ToString()
                    End If

                    dRow("IsDeptSelected") = 0
                    dRow("IsDeptHeadSelected") = 0
                    dtDept.Rows.Add(dRow)
                Next
            End If
            Return dtDept
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSelectedUserDetails(ByVal sAC As String, ByVal iUserID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Sad_UserDetails where Usr_ID=" & iUserID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckLoginNameEists(ByVal sAC As String, ByVal iACID As Integer, ByVal sLoginName As String, ByVal iUserID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID from Sad_UserDetails where Upper(Usr_LoginName)='" & sLoginName & "' And Usr_CompID=" & iACID & ""
            If iUserID > 0 Then
                sSql = sSql & " And Usr_ID<>" & iUserID & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUserInOtherDeptDetails(ByVal sAC As String, ByVal iUserID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select SUO_DeptID,SUO_IsDeptHead from Sad_UsersInOtherDept where SUO_UserID=" & iUserID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUserDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal objstrUserDetails As strUserDetails, ByVal iDeptID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUsr_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Code", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_LoginName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_LoginName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_FullName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_FullName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PassWord", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_PassWord
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Email", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_DeptID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUsr_DeptID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_SectionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUSR_SectionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_LevelCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUsr_LevelCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_Designation", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUsr_Designation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_IsSuperUser", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUsr_IsSuperUser
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_MemberType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUsr_MemberType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_DelFlag", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_CrBy", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objstrUserDetails.iUsr_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objstrUserDetails.sUsr_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@USR_usrGrpLvlPerm", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrUserDetails.iUSR_usrGrpLvlPerm
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSad_userdetails", 1, Arr, ObjParam)

            objDBL.SQLExecuteNonQuery(sAC, "Delete From Sad_UsersInOtherDept where SUO_UserID=" & Arr(1) & "")
            If iDeptID > 0 Then
                InsertSadUsersInOtherDept(sAC, iACID, objstrUserDetails, Arr(1), iDeptID)
            End If
            If objstrUserDetails.sOther_DeptList <> "" Then
                InsertSadUsersInOtherDeptList(sAC, iACID, objstrUserDetails, Arr(1))
            End If
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function InsertSadUsersInOtherDept(ByVal sAC As String, ByVal iACID As Integer, ByVal objstrUserDetails As strUserDetails, ByVal iUserID As Integer, ByVal iDeptID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(7) {}
        Dim iParacount As Integer = 0
        Dim Arr(1) As String
        Try
            iParacount = 0
            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_UserID", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = iUserID
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_DeptID", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = iDeptID
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_IsDeptHead", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = objstrUserDetails.iUsr_MemberType
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_CreatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParacount).Value = objstrUserDetails.iUsr_CrBy
            ObjParam(iParacount).Direction = ParameterDirection.Input
            iParacount += 1

            ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParacount).Value = objstrUserDetails.sUsr_IPAddress
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
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function InsertSadUsersInOtherDeptList(ByVal sAC As String, ByVal iACID As Integer, ByVal objstrUserDetails As strUserDetails, ByVal iUserID As Integer) As String
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(7) {}
        Dim iParacount As Integer = 0
        Dim Arr(1) As String
        Dim sStr As String
        Dim MyGrpList() As String
        Dim MyPosList() As String
        Dim i As Integer
        Try
            MyGrpList = Split(objstrUserDetails.sOther_DeptList, "|")
            For i = 0 To UBound(MyGrpList)
                sStr = MyGrpList(i)
                MyPosList = sStr.Split(";")
                iParacount = 0
                ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_UserID", OleDb.OleDbType.Integer)
                ObjParam(iParacount).Value = iUserID
                ObjParam(iParacount).Direction = ParameterDirection.Input
                iParacount += 1

                ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_DeptID", OleDb.OleDbType.Integer)
                ObjParam(iParacount).Value = MyPosList(0)
                ObjParam(iParacount).Direction = ParameterDirection.Input
                iParacount += 1

                ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_IsDeptHead", OleDb.OleDbType.Integer)
                ObjParam(iParacount).Value = MyPosList(1)
                ObjParam(iParacount).Direction = ParameterDirection.Input
                iParacount += 1

                ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_CreatedBy", OleDb.OleDbType.Integer)
                ObjParam(iParacount).Value = objstrUserDetails.iUsr_CrBy
                ObjParam(iParacount).Direction = ParameterDirection.Input
                iParacount += 1

                ObjParam(iParacount) = New OleDb.OleDbParameter("@SUO_IPAddress", OleDb.OleDbType.VarChar, 50)
                ObjParam(iParacount).Value = objstrUserDetails.sUsr_IPAddress
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
            Next
            Return Arr(1)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UserApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iEmployeeID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update Sad_Userdetails set"
            If sType = "Created" Then
                sSql = sSql & " Usr_DelFlag='A',Usr_Status='A',Usr_DutyStatus='A',Usr_AppBy=" & iSessionUsrID & ",Usr_AppOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " Usr_DelFlag='D',Usr_Status='AD',Usr_DutyStatus='D',Usr_DeActivatedBy=" & iSessionUsrID & ",Usr_DeActivatedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " Usr_DelFlag='A',Usr_Status='AR',Usr_DutyStatus='A',Usr_ActivatedBy=" & iSessionUsrID & ",Usr_ActivatedOn=Getdate(),"
            ElseIf sType = "UnBlock" Then
                sSql = sSql & " Usr_DelFlag='A',Usr_Status='UB',Usr_DutyStatus='A',Usr_NoOfUnSucsfAtteptts=0,Usr_LastLoginDate=GetDate(),Usr_UnBlockBy=" & iSessionUsrID & ",Usr_UnBlockOn=GetDate(),"
            ElseIf sType = "UnLock" Then
                sSql = sSql & " Usr_DelFlag='A',Usr_Status='UL',Usr_DutyStatus='A',Usr_NoOfUnSucsfAtteptts=0,Usr_LastLoginDate=GetDate(),Usr_UnLockBy=" & iSessionUsrID & ",Usr_UnLockOn=GetDate(),"
            End If
            sSql = sSql & " Usr_IPAddress='" & sIPAddress & "' Where Usr_CompID=" & iACID & " And Usr_ID=" & iEmployeeID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadAllUsersToReport(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim i As Integer = 0
        Dim StrSql As String = ""
        Dim dRow As DataRow
        Dim dtDisplay As New DataTable, dtCountryGovtDeptSectionDetails As New DataTable, dt As New DataTable
        Try
            dtDisplay.Columns.Add("SrNo")
            dtDisplay.Columns.Add("LoginName")
            dtDisplay.Columns.Add("FullName")
            dtDisplay.Columns.Add("Department")
            dtDisplay.Columns.Add("Section")
            dtDisplay.Columns.Add("Designation")
            dtDisplay.Columns.Add("UserType")
            dtDisplay.Columns.Add("Delflag")

            dtCountryGovtDeptSectionDetails = GetCountryGovtDeptSection(sAC, iACID)

            StrSql = " select a.Usr_LoginName,a.Usr_ID,a.Usr_DeptID,a.Usr_FullName,a.Usr_Email,a.Usr_IsSuperuser,a.Usr_DelFlag,a.Usr_LastLoginDate,"
            StrSql = StrSql & "a.Usr_LevelCode,a.Usr_SectionID,a.Usr_NoOfLogin,Usr_DutyStatus,Usr_DelFlag,b.Mas_Description as Designation from Sad_UserDetails a "
            StrSql = StrSql & " Left Join Sad_Designation_MASTER b ON b.Mas_ID=a.Usr_Designation "
            StrSql = StrSql & " where a.Usr_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, StrSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow

                    dRow("LoginName") = "" : dRow("FullName") = "" : dRow("Department") = ""
                    dRow("Department") = "" : dRow("Section") = "" : dRow("Designation") = ""
                    dRow("UserType") = "" : dRow("Delflag") = ""

                    dRow("SrNo") = i + 1
                    If IsDBNull(dt.Rows(i)("Usr_LoginName").ToString()) = False Then
                        dRow("LoginName") = dt.Rows(i)("Usr_LoginName").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_FullName").ToString()) = False Then
                        dRow("FullName") = dt.Rows(i)("Usr_FullName").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_DeptID")) = False And IsDBNull(dt.Rows(i)("Usr_LevelCode")) = False Then
                        Dim dtDSDeatils As New DataTable
                        Dim DVCGDSDetails As New DataView(dtCountryGovtDeptSectionDetails)
                        DVCGDSDetails.RowFilter = "Org_Node=" & dt.Rows(i)("Usr_DeptID") & " And Org_levelCode=3"
                        dtDSDeatils = DVCGDSDetails.ToTable
                        If dtDSDeatils.Rows.Count > 0 Then
                            dRow("Department") = dtDSDeatils.Rows(0)("Org_Name")
                        End If
                        If IsDBNull(dt.Rows(i)("Usr_SectionID")) = False And dt.Rows(i)("Usr_LevelCode") = 4 Then
                            Dim dtDS As New DataTable
                            DVCGDSDetails.RowFilter = "Org_Node=" & dt.Rows(i)("Usr_SectionID") & " And Org_levelCode=4"
                            dtDS = DVCGDSDetails.ToTable
                            If dtDS.Rows.Count > 0 Then
                                dRow("Section") = dtDS.Rows(0)("Org_Name")
                            End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(i)("Designation").ToString()) = False Then
                        dRow("Designation") = dt.Rows(i)("Designation").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Usr_IsSuperuser").ToString()) = False Then
                        If dt.Rows(i)("Usr_IsSuperuser").ToString() = 1 Then
                            dRow("UserType") = "Power User"
                            'ElseIf dt.Rows(i)("Usr_IsSuperuser").ToString() = 2 Then 'Vijeth
                            '    dRow("UserType") = "Super User"
                        ElseIf dt.Rows(i)("Usr_IsSuperuser").ToString() = 2 Then
                            dRow("UserType") = "Normal User"
                        End If
                    End If
                    If IsDBNull(dt.Rows(i)("Usr_DelFlag").ToString()) = False Then
                        If dt.Rows(i)("Usr_DutyStatus") = "W" Then
                            dRow("Delflag") = "Waiting for Approval"
                        ElseIf dt.Rows(i)("Usr_DutyStatus") = "D" Then
                            dRow("Delflag") = "De-Activated"
                        ElseIf (dt.Rows(i)("Usr_DutyStatus") = "A") Then
                            dRow("Delflag") = "Activated"
                        ElseIf dt.Rows(i)("Usr_DutyStatus") = "L" Then
                            dRow("Delflag") = "Locked"
                        ElseIf dt.Rows(i)("Usr_DutyStatus") = "B" Then
                            dRow("Delflag") = "Blocked"
                        End If
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUserGridDetails(ByVal sNameSpace As String, ByVal iUserID As Integer, ByVal iGrpID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtUsr As DataTable
        Dim dRow As DataRow
        Dim drLoad As OleDb.OleDbDataReader
        Try
            dtUsr = BuildUsrGrpTable()
            sSql = "" : sSql = "select a.Gld_GrpLvlId,b.Mas_Description, a.Gld_GrpLvlPosn from sad_grplvl_members a,SAD_GrpOrLvl_General_Master b where a.Gld_GrpLvlId = b.mas_id and a.gld_userId =" & iUserID & " and Gld_GrpLvlId not in (" & iGrpID & ")  "
            drLoad = objDBL.SQLDataReader(sNameSpace, sSql)
            If drLoad.HasRows Then
                While drLoad.Read
                    dRow = dtUsr.NewRow
                    dRow("Mas_ID") = drLoad("Gld_GrpLvlId")
                    dRow("Mas_Description") = drLoad("Mas_Description")
                    dRow("Mas_Flag") = True
                    If drLoad("Gld_GrpLvlPosn") = 0 Then
                        dRow("Mas_Post") = "Q"
                    Else
                        dRow("Mas_Post") = "P"
                    End If
                    dtUsr.Rows.Add(dRow)
                End While
            End If
            drLoad.Close()
            sSql = "" : sSql = "select MAs_ID,MAs_Description from SAD_GrpOrLvl_General_Master where mas_delflag ='A' and mas_id not in(select Gld_GrpLvlid from Sad_GrpLvl_Members where gld_userId =" & iUserID & ")and Mas_Id Not In(" & iGrpID & ")"
            drLoad = objDBL.SQLDataReader(sNameSpace, sSql)
            If drLoad.HasRows Then
                While drLoad.Read
                    dRow = dtUsr.NewRow
                    dRow("Mas_ID") = drLoad("Mas_ID")
                    dRow("Mas_Description") = drLoad("Mas_Description")
                    dRow("Mas_Flag") = False
                    dtUsr.Rows.Add(dRow)
                End While
            End If
            Return dtUsr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildUsrGrpTable()
        Dim dtUsr As DataTable
        Dim dc As DataColumn
        Try
            dtUsr = New DataTable("UsrTable")
            dc = New DataColumn("Mas_ID")
            dtUsr.Columns.Add(dc)
            dc = New DataColumn("Mas_Flag", System.Type.GetType("System.Boolean"))
            dtUsr.Columns.Add(dc)
            dc = New DataColumn("Mas_Post")
            dtUsr.Columns.Add(dc)
            dc = New DataColumn("Mas_Description")
            dtUsr.Columns.Add(dc)
            Return dtUsr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCabUserPermission(ByVal sNameSpace As String, ByVal objCab As clsCabinet) As Array
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
            ObjParam(iParamCount).Value = "U"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBP_Cabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_User", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_UserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_Department
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
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spEDT_cabinet_Permission", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception

            Throw
        End Try
    End Function
    Public Function SaveSubUserPermission(ByVal sNameSpace As String, ByVal objSubCab As clsSubCabinet) As Array
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
            ObjParam(iParamCount).Value = "U"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBP_Cabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_User", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSubCab.iCBN_UserID
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
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spEDT_cabinet_Permission", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception

            Throw
        End Try
    End Function
    Public Function SaveFolUserPermission(ByVal sNameSpace As String, ByVal objFoldr As clsFolders) As Array
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
            ObjParam(iParamCount).Value = "U"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_GRPID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iEFP_GPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_USRID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Crby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_VIEW_FOL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_CRT_DOC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_SEARCH", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_INDEX", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_EXPORT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
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

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "InOrUpFolPermissions", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub PermanetDeleteUserDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "delete from sad_userdetails Where Usr_Id=" & iUsrID & " and USR_compid='" & iACID & "'"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class

