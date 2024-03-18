Imports DatabaseLayer
Public Class clsControlLibrary
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private iMCL_PKID As Integer
    Private sMCL_ControlName As String
    Private sMCL_ControlDesc As String
    Private sMCL_Code As String
    Private iMCL_IsKey As Integer
    Private sMCL_DelFlag As String
    Private sMCL_Status As String
    Private iMCL_CrBy As Integer
    Private iMCL_UpdatedBy As Integer
    Private sMCL_IPAddress As String
    Private iMCL_CompID As Integer

    Private iCHK_ID As Integer
    Private iCHK_ControlID As Integer
    Private sCHK_CheckName As String
    Private sCHK_CheckDesc As String
    Private iCHK_CatId As Integer
    Private iCHK_IsKey As Integer
    Private sCHK_DelFlag As String
    Private sCHK_Status As String
    Private iCHK_CrBy As Integer
    Private iCHK_UpdatedBy As Integer
    Private sCHK_IPAddress As String
    Private iCHK_CompID As Integer
    Public Property iMCLPKID() As Integer
        Get
            Return (iMCL_PKID)
        End Get
        Set(ByVal Value As Integer)
            iMCL_PKID = Value
        End Set
    End Property
    Public Property sMCLControlName() As String
        Get
            Return (sMCL_ControlName)
        End Get
        Set(ByVal Value As String)
            sMCL_ControlName = Value
        End Set
    End Property
    Public Property sMCLControlDesc() As String
        Get
            Return (sMCL_ControlDesc)
        End Get
        Set(ByVal Value As String)
            sMCL_ControlDesc = Value
        End Set
    End Property
    Public Property sMCLCode() As String
        Get
            Return (sMCL_Code)
        End Get
        Set(ByVal Value As String)
            sMCL_Code = Value
        End Set
    End Property
    Public Property iMCLIsKey() As Integer
        Get
            Return (iMCL_IsKey)
        End Get
        Set(ByVal Value As Integer)
            iMCL_IsKey = Value
        End Set
    End Property
    Public Property sMCLDelFlag() As String
        Get
            Return (sMCL_DelFlag)
        End Get
        Set(ByVal Value As String)
            sMCL_DelFlag = Value
        End Set
    End Property
    Public Property sMCLStatus() As String
        Get
            Return (sMCL_Status)
        End Get
        Set(ByVal Value As String)
            sMCL_Status = Value
        End Set
    End Property
    Public Property iMCLCrBy() As Integer
        Get
            Return (iMCL_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iMCL_CrBy = Value
        End Set
    End Property
    Public Property iMCLUpdatedBy() As Integer
        Get
            Return (iMCL_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iMCL_UpdatedBy = Value
        End Set
    End Property
    Public Property sMCLIPAddress() As String
        Get
            Return (sMCL_IPAddress)
        End Get
        Set(ByVal Value As String)
            sMCL_IPAddress = Value
        End Set
    End Property
    Public Property iMCLCompID() As Integer
        Get
            Return (iMCL_CompID)
        End Get
        Set(ByVal Value As Integer)
            iMCL_CompID = Value
        End Set
    End Property

    'Checks
    Public Property iCHKID() As Integer
        Get
            Return (iCHK_ID)
        End Get
        Set(ByVal Value As Integer)
            iCHK_ID = Value
        End Set
    End Property
    Public Property iCHKControlID() As Integer
        Get
            Return (iCHK_ControlID)
        End Get
        Set(ByVal Value As Integer)
            iCHK_ControlID = Value
        End Set
    End Property
    Public Property sCHKCheckName() As String
        Get
            Return (sCHK_CheckName)
        End Get
        Set(ByVal Value As String)
            sCHK_CheckName = Value
        End Set
    End Property
    Public Property sCHKCheckDesc() As String
        Get
            Return (sCHK_CheckDesc)
        End Get
        Set(ByVal Value As String)
            sCHK_CheckDesc = Value
        End Set
    End Property
    Public Property iCHKCatId() As Integer
        Get
            Return (iCHK_CatId)
        End Get
        Set(ByVal Value As Integer)
            iCHK_CatId = Value
        End Set
    End Property
    Public Property iCHKIsKey() As Integer
        Get
            Return (iCHK_IsKey)
        End Get
        Set(ByVal Value As Integer)
            iCHK_IsKey = Value
        End Set
    End Property
    Public Property sCHKDelFlag() As String
        Get
            Return (sCHK_DelFlag)
        End Get
        Set(ByVal Value As String)
            sCHK_DelFlag = Value
        End Set
    End Property
    Public Property sCHKStatus() As String
        Get
            Return (sCHK_Status)
        End Get
        Set(ByVal Value As String)
            sCHK_Status = Value
        End Set
    End Property
    Public Property iCHKCrBy() As Integer
        Get
            Return (iCHK_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iCHK_CrBy = Value
        End Set
    End Property
    Public Property iCHKUpdatedBy() As Integer
        Get
            Return (iCHK_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCHK_UpdatedBy = Value
        End Set
    End Property
    Public Property sCHKIPAddress() As String
        Get
            Return (sCHK_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCHK_IPAddress = Value
        End Set
    End Property
    Public Property iCHKCompID() As Integer
        Get
            Return (iCHK_CompID)
        End Get
        Set(ByVal Value As Integer)
            iCHK_CompID = Value
        End Set
    End Property
    'BindControls
    Public Function LoadAllExistingControls(ByVal sAc As String, ByVal iAcID As Integer, ByVal sSearchControl As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MCL_PKID,MCL_ControlName from MST_CONTROL_Library where MCL_CompID=" & iAcID & ""
            If sSearchControl <> "" Then
                sSql = sSql & " And MCL_ControlName Like '" & sSearchControl & "%'"
            End If
            sSql = sSql & " order by MCL_ControlName"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlName(ByVal sAc As String, ByVal iAcID As Integer, ByVal iRiskID As String) As String
        Dim sSql As String
        Try
            sSql = "Select MCL_ControlName from MST_Control_Library where MCL_CompID=" & iAcID & " And MCL_PKID=" & iRiskID & " "
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BindChecks
    Public Function LoadAllChecks(ByVal sAc As String, ByVal iAcID As Integer, ByVal iSelectedControlID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CHK_ID,CHK_CheckName from MST_Checks_Master where CHK_CompID=" & iAcID & ""
            If iSelectedControlID > 0 Then
                sSql = sSql & " And CHK_ControlID=" & iSelectedControlID & ""
            End If
            sSql = sSql & "  order by CHK_CheckName"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BindChecksCategory
    Public Function LoadChecksCategory(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name from Risk_GeneralMaster where RAM_CompID=" & iAcID & " And RAM_YearID=" & iYearID & " And RAM_Category='" & sType & "' And RAM_DelFlag='A' order by RAM_PKID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BindSelectedControlDetails
    Public Function LoadSelectedControlDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iControlID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MCL_ControlName,MCL_Code,MCL_ControlDesc,MCL_IsKey,MCL_DelFlag from MST_CONTROL_Library where MCL_PKID=" & iControlID & " And MCL_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BindSelectedChecksDetails
    Public Function LoadSelectedChecksDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCheckID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CHK_CheckName,CHK_CheckDesc,CHK_CatId,CHK_IsKey,CHK_DelFlag from MST_Checks_Master where CHK_ID=" & iCheckID & " And CHK_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'CheckControlNameExist
    Public Function CheckControlNameExist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iPKID As Integer, ByVal sControlName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select MCL_PKID from MST_CONTROL_Library where MCL_ControlName='" & sControlName & "' And MCL_CompID =" & iAcID & ""
            If iPKID > 0 Then
                sSql = sSql & " And MCL_PKID <>" & iPKID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'CheckNameExist
    Public Function CheckNameExist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iControlID As Integer, ByVal iPKID As Integer, ByVal sCheckName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CHK_ID from MST_Checks_Master where CHK_CheckName='" & sCheckName & "' And Chk_ControlID=" & iControlID & " And CHK_CompID =" & iAcID & ""
            If iPKID > 0 Then
                sSql = sSql & " And CHK_ID <>" & iPKID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'ApproveControlDetails
    Public Sub ApproveControlDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iUserID As Integer, ByVal iControlID As Integer, ByVal sFlag As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update MST_CONTROL_Library set "
            If sFlag = "W" Then
                sSql = sSql & "MCL_DelFlag='A',MCL_Status='A',MCL_ApprovedBy=" & iUserID & ",MCL_ApprovedOn=Getdate(),"
            ElseIf sFlag = "D" Then
                sSql = sSql & "MCL_DelFlag='D',MCL_Status='AD',MCL_DeletedBy=" & iUserID & ",MCL_DeletedOn=Getdate(),"
            ElseIf sFlag = "A" Then
                sSql = sSql & "MCL_DelFlag='A',MCL_Status='AR',MCL_RecallBy=" & iUserID & ",MCL_RecallOn=Getdate(),"
            End If
            sSql = sSql & " MCL_IPAddress='" & sIPAddress & "' where MCL_PKID=" & iControlID & " and MCL_CompID=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'LoadGrid
    Public Function LoadControlGrid(ByVal sAc As String, ByVal iAcID As Integer) As DataTable
        Dim sSql As String
        Dim dtControl As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("Sr.No")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ControlName")
            dtTab.Columns.Add("ControlKey")
            dtTab.Columns.Add("Status")
            sSql = "Select MCL_PKID,MCL_ControlName,MCL_DelFlag,MCL_IsKey From MST_CONTROL_Library Where MCL_CompID =" & iAcID & ""
            sSql = sSql & " Order By MCL_ControlName"

            dtControl = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtControl.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Sr.No") = i + 1
                dr("ControlID") = dtControl.Rows(i)("MCL_PKID")
                dr("ControlName") = dtControl.Rows(i)("MCL_ControlName")
                If dtControl.Rows(i)("MCL_IsKey") = 1 Then
                    dr("ControlKey") = "Key"
                Else
                    dr("ControlKey") = "Non-Key"
                End If
                If IsDBNull(dtControl.Rows(i)("MCL_DelFlag")) = False Then
                    If dtControl.Rows(i)("MCL_DelFlag") = "A" Then
                        dr("Status") = "Activated"
                    ElseIf dtControl.Rows(i)("MCL_DelFlag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf dtControl.Rows(i)("MCL_DelFlag") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'LoadChecksGridItems
    Public Function LoadChecksGridItems(ByVal sAc As String, ByVal iAcID As Integer, ByVal iExistingControlID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("Sr.No")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("ControlName")
            dtTab.Columns.Add("ChecksName")
            dtTab.Columns.Add("ChecksCategory")
            dtTab.Columns.Add("ControlStatusID")
            sSql = "Select Chk_ID,Chk_ControlID,MCL_PKID,RAM_PKID,Chk_CheckName,CHK_DelFlag,RAM_Name,MCL_ControlName,RAM_Category='CC',MCL_Delflag From MST_Checks_Master "
            sSql = sSql & " left outer join Risk_GeneralMaster On RAM_PKID=Chk_CatID "
            sSql = sSql & " left outer join MST_CONTROL_Library On MCL_PKID=Chk_ControlID"
            sSql = sSql & " Where CHK_CompID=" & iAcID & " And CHK_ControlID=" & iExistingControlID & " Order By CHK_CheckName"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow()
                dr("Sr.No") = i + 1
                dr("ChecksID") = dt.Rows(i)("Chk_ID")
                dr("ControlName") = dt.Rows(i)("MCL_ControlName")
                dr("ChecksName") = dt.Rows(i)("CHK_CheckName")
                dr("ChecksCategory") = dt.Rows(i)("RAM_Name")
                dr("ControlStatusID") = dt.Rows(i)("MCL_Delflag")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'SaveControlLibrary
    Public Function SaveControlLibrary(ByVal sAc As String, ByVal objclsControlLibrary As clsControlLibrary) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iMCLPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_ControlName", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsControlLibrary.sMCLControlName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_ControlDesc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsControlLibrary.sMCLControlDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_Code", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsControlLibrary.sMCLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_IsKey", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iMCLIsKey
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iMCLCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iMCLUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsControlLibrary.sMCLIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MCL_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iMCLCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAc, "spMST_Control_Library", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'SaveChecksMaster
    Public Function SaveChecksMaster(ByVal sAc As String, ByVal objclsControlLibrary As clsControlLibrary) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iCHKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_ControlID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iCHKControlID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_CheckName", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsControlLibrary.sCHKCheckName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_CheckDesc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsControlLibrary.sCHKCheckDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_CatId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iCHKCatId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_IsKey", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iCHKIsKey
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iCHKCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iCHKUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsControlLibrary.sCHKIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CHK_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsControlLibrary.iCHKCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAc, "spMST_Checks_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadControlReport(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer) As DataTable
        Dim sSql As String
        Dim dtControl As New DataTable, dtChecks As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim i As Integer, j As Integer
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("Code")
            dtTab.Columns.Add("Key")
            dtTab.Columns.Add("Description")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("ChecksCategory")
            dtTab.Columns.Add("ChecksKey")
            dtTab.Columns.Add("ChecksDescription")
            dtTab.Columns.Add("ChecksStatus")
            dtTab.Columns.Add("Status")

            sSql = "Select * from MST_Control_Library Where MCL_CompID =" & iAcID & ""
            If iStatus = 0 Then
                sSql = sSql & " And MCL_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And MCL_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And MCL_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By MCL_ControlName"
            dtControl = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtControl.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("ControlID") = dtControl.Rows(i)("MCL_PKID")
                dr("Control") = dtControl.Rows(i)("MCL_ControlName")
                dr("Code") = dtControl.Rows(i)("MCL_Code")
                dr("Description") = dtControl.Rows(i)("MCL_ControlDesc")
                If dtControl.Rows(i)("MCL_IsKey") = 1 Then
                    dr("Key") = "Key"
                Else
                    dr("Key") = "Non-Key"
                End If
                If dtControl.Rows(i)("MCL_DelFlag") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dtControl.Rows(i)("MCL_DelFlag") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dtControl.Rows(i)("MCL_DelFlag") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If

                sSql = "Select Chk_ID,Chk_ControlID,MCL_PKID,RAM_PKID,CHK_IsKey,Chk_CheckName,CHK_DelFlag,RAM_Name,CHK_CheckDesc,MCL_ControlName,RAM_Category='CC',MCL_Delflag From MST_Checks_Master "
                sSql = sSql & " left outer join Risk_GeneralMaster On RAM_PKID=Chk_CatID "
                sSql = sSql & " left outer join MST_CONTROL_Library On MCL_PKID=Chk_ControlID"
                sSql = sSql & " Where CHK_CompID=" & iAcID & " And CHK_ControlID=" & dtControl.Rows(i)("MCL_PKID") & " Order By CHK_CheckName"
                dtChecks = objDBL.SQLExecuteDataTable(sAc, sSql)
                For j = 0 To dtChecks.Rows.Count - 1
                    If j > 0 Then
                        dr = dtTab.NewRow
                    End If
                    dr("Checks") = dtChecks.Rows(j)("Chk_CheckName")
                    dr("ChecksCategory") = dtChecks.Rows(j)("RAM_Name")

                    If dtChecks.Rows(j)("CHK_IsKey") = 1 Then
                        dr("ChecksKey") = "Key"
                    Else
                        dr("ChecksKey") = "Non-Key"
                    End If
                    dr("ChecksDescription") = dtChecks.Rows(j)("CHK_CheckDesc")
                    If dtChecks.Rows(j)("CHK_DelFlag") = "A" Then
                        dr("ChecksStatus") = "Activated"
                    ElseIf dtChecks.Rows(j)("CHK_DelFlag") = "D" Then
                        dr("ChecksStatus") = "De-Activated"
                    ElseIf dtChecks.Rows(j)("CHK_DelFlag") = "W" Then
                        dr("ChecksStatus") = "Waiting for Approval"
                    End If
                    dtTab.Rows.Add(dr)
                Next
                If dtChecks.Rows.Count = 0 Then
                    dtTab.Rows.Add(dr)
                End If
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
