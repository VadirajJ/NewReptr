Imports System
Imports DatabaseLayer
    Imports BusinesLayer
    Imports System.Data
Public Class ClsLocationSetup
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral

    Private iLS_ID As Integer
    Private sLS_Description As String
    Private sLS_DescCode As String
    Private sLS_Code As String
    Private iLS_LevelCode As Integer
    Private iLS_ParentID As Integer
    Private iLS_CreatedBy As Integer
    Private dLS_CreatedOn As DateTime
    Private iLS_UpdatedBy As Integer
    Private dLS_UpdatedOn As DateTime
    Private sLS_DelFlag As String
    Private sLS_Status As String
    Private iLS_YearID As Integer
    Private iLS_CompID As Integer
    Private sLS_Opeartion As String
    Private sLS_IPAddress As String
    Private iLS_ApprovedBy As Integer
    Private dLS_ApprovedOn As DateTime

    Private iLS_CustId As Integer
    Public Property LS_CustId() As Integer
        Get
            Return (iLS_CustId)
        End Get
        Set(ByVal Value As Integer)
            iLS_CustId = Value
        End Set
    End Property
    Public Property LS_ID() As Integer
        Get
            Return (iLS_ID)
        End Get
        Set(ByVal Value As Integer)
            iLS_ID = Value
        End Set
    End Property
    Public Property LS_Description() As String
        Get
            Return (sLS_Description)
        End Get
        Set(ByVal Value As String)
            sLS_Description = Value
        End Set
    End Property
    Public Property LS_DescCode() As String
        Get
            Return (sLS_DescCode)
        End Get
        Set(ByVal Value As String)
            sLS_DescCode = Value
        End Set
    End Property
    Public Property LS_Code() As String
        Get
            Return (sLS_Code)
        End Get
        Set(ByVal Value As String)
            sLS_Code = Value
        End Set
    End Property
    Public Property LS_LevelCode() As Integer
        Get
            Return (iLS_LevelCode)
        End Get
        Set(ByVal Value As Integer)
            iLS_LevelCode = Value
        End Set
    End Property
    Public Property LS_ParentID() As Integer
        Get
            Return (iLS_ParentID)
        End Get
        Set(ByVal Value As Integer)
            iLS_ParentID = Value
        End Set
    End Property

    Public Property LS_CreatedBy() As Integer
        Get
            Return (iLS_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLS_CreatedBy = Value
        End Set
    End Property

    Public Property LS_CreatedOn() As DateTime
        Get
            Return (dLS_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLS_CreatedOn = Value
        End Set
    End Property
    Public Property LS_UpdatedOn() As DateTime
        Get
            Return (dLS_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLS_UpdatedOn = Value
        End Set
    End Property
    Public Property LS_UpdatedBy() As Integer
        Get
            Return (iLS_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLS_UpdatedBy = Value
        End Set
    End Property
    Public Property LS_DelFlag() As String
        Get
            Return (sLS_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLS_DelFlag = Value
        End Set
    End Property
    Public Property LS_Status() As String
        Get
            Return (sLS_Status)
        End Get
        Set(ByVal Value As String)
            sLS_Status = Value
        End Set
    End Property
    Public Property LS_YearID() As Integer
        Get
            Return (iLS_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLS_YearID = Value
        End Set
    End Property
    Public Property LS_CompID() As Integer
        Get
            Return (iLS_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLS_CompID = Value
        End Set
    End Property
    Public Property LS_Opeartion() As String
        Get
            Return (sLS_Opeartion)
        End Get
        Set(ByVal Value As String)
            sLS_Opeartion = Value
        End Set
    End Property
    Public Property LS_IPAddress() As String
        Get
            Return (sLS_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLS_IPAddress = Value
        End Set
    End Property
    Public Property LS_ApprovedBy() As Integer
        Get
            Return (iLS_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLS_ApprovedBy = Value
        End Set
    End Property
    Public Property LS_ApprovedOn() As DateTime
        Get
            Return (dLS_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLS_ApprovedOn = Value
        End Set
    End Property
    'Public Sub UpdateDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal Deprate As Integer, ByVal ITRate As Integer, ByVal ResidualValue As Integer, ByVal Item As Integer, ByVal iCustId As Integer)
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "Update Acc_AssetMaster set AM_Deprate=" & Deprate & ",AM_ITRate=" & ITRate & ",AM_ResidualValue=" & ResidualValue & " where AM_ID=" & Item & " and AM_CompID=" & iACID & " and AM_CustId=" & iCustId & ""
    '        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Function LoadLocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where LS_LevelCode=0 and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select CUST_ID,CUST_NAME From SAD_CUSTOMER_MASTER Where CUST_STATUS<>'D' and CUST_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDepartment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iparentid As String, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iparentid = 0 Then
                sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where LS_LevelCode=2 and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            Else
                sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where LS_LevelCode=2 and LS_ParentID in (" & iparentid & ") and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDivision(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iparentid As String, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iparentid = 0 Then
                sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where LS_LevelCode=1 and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            Else
                sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where  LS_ParentID in (" & iparentid & ") and LS_LevelCode=1 and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBayi(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iparentid As String, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iparentid = 0 Then
                sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where LS_LevelCode=3 and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            Else
                sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where LS_ParentID in (" & iparentid & ") and LS_LevelCode=3 and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objLocationSetup As ClsLocationSetup) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objLocationSetup.LS_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_DescCode", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objLocationSetup.LS_DescCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_Code", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objLocationSetup.LS_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_LevelCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_LevelCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_ParentID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_ParentID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLocationSetup.LS_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLocationSetup.LS_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objLocationSetup.LS_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLocationSetup.LS_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.LS_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLocationSetup.iLS_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_ApprovedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLocationSetup.dLS_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLocationSetup.LS_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objLocationSetup.LS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AssetLocationSetup", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function AssetRetrieve(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAM_AsstType As Integer) As DataTable
    '    Dim dt As New DataTable
    '    Dim sSql As String = ""
    '    Try
    '        'sSql = "select * from Acc_AssetMaster Where AM_AssetID=" & iAM_AsstType & " And AM_CompID=" & iCompID & " And AM_YearID=" & iYearID & " " ' commented On 14-03-2022
    '        sSql = "select * from Acc_AssetMaster Where AM_AssetID=" & iAM_AsstType & " And AM_CompID=" & iCompID & " "
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function GetItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDescID As Integer, ByVal iCustid As Integer) As DataTable
    '    Dim dt As New DataTable
    '    Dim sSql As String = ""
    '    Dim dbcheck As New Boolean
    '    Try

    '        sSql = "Select AM_Deprate,AM_ITRate,AM_ResidualValue from Acc_AssetMaster where AM_ID =" & iDescID & " and AM_CompID =" & iCompID & " and AM_CustId =" & iCustid & ""
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function CheckDepreciationRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String, ByVal dDeprate As Double, ByVal objAsst As ClsAssetMaster) As String
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dcheck As New Boolean
    '    Dim AssetID As New Integer
    '    Dim iID As String = ""
    '    Dim iMax As Integer = 0
    '    Try

    '        sSql = "Select GL_ID From Chart_Of_Accounts Where GL_DESC='" & sAssettype & "' And GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Non Current Assets'))"

    '        AssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

    '        sSql = "select * from Acc_AssetMaster where AM_Deprate=" & dDeprate & " and AM_AssetID=" & AssetID & ""
    '        dcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
    '        If dcheck = True Then

    '        Else
    '            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(AM_ID)+1,1) from Acc_AssetMaster")
    '            sSql = "insert into Acc_AssetMaster(AM_ID,AM_AssetID,AM_CreatedBy,AM_CreatedOn,AM_DelFlag ,"
    '            sSql = sSql & " AM_Status,AM_YearID,AM_CompID,AM_Deprate,AM_Opeartion,AM_IPAddress) values (" & iMax & "," & AssetID & ","
    '            sSql = sSql & " " & objAsst.AM_CreatedBy & ",getdate(),'X','W'," & iYearID & "," & iCompID & "," & dDeprate & ",'C','" & objAsst.AM_IPAddress & "')"
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Sub UpdateStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = "", ssqlgl As String = ""
        Dim iCsmSgl As Integer = 0
        Try
            sSql = "Update Acc_AssetLocationSetup Set "
            If sStatus = "W" Then
                sSql = sSql & " LS_DelFlag='A',LS_Status='A',LS_ApprovedBy= " & iUserID & ",LS_ApprovedOn=GetDate()"
            End If
            sSql = sSql & " Where LS_ID = " & iMasId & " and LS_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iid As Integer, ByVal iCustid As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = " Select LS_DelFlag from Acc_AssetLocationSetup Where LS_ID = " & iid & " And LS_CompID=" & iCompID & " And LS_CustId=" & iCustid & ""
            GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iLocationid As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = " Select LS_DescCode from Acc_AssetLocationSetup Where LS_ID = " & iLocationid & " And LS_CompID=" & iCompID & ""
            LoadCode = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return LoadCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function TocheckExistLocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Description As String, ByVal iyearId As Integer, ByVal iCustId As Integer, ByVal iparentid As Integer)
        Dim checkitemcode As Boolean
        Dim sSql As String
        Try
            sSql = "Select LS_Description From Acc_AssetLocationSetup where LS_Description='" & Description & "'  and LS_CustId=" & iCustId & " and LS_ParentID=" & iparentid & " and LS_CompID=" & iCompID & ""
            checkitemcode = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If checkitemcode = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function
End Class

