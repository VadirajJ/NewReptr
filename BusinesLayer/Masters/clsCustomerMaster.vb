Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsCustomerMaster
    Private Cust_ID As Integer
    Private Cust_Name As String
    Private Cust_Code As String
    Private Cust_OrgTypeID As Integer
    Private Cust_CrBy As Integer
    Private Cust_ApprovedBy As Integer
    Private Cust_Status As String
    Private Cust_DelFlg As String
    Private Cust_IPAddress As String
    Private Cust_CompID As Integer

    Private Mas_Id As Integer
    Private Mas_Code As String
    Private Mas_Description As String
    Private Mas_DelFlag As String
    Private Mas_CustID As Integer
    Private Mas_Loc_Address As String
    Private Mas_Contact_Person As String
    Private Mas_Contact_MobileNo As String
    Private Mas_Contact_Email As String
    Private Mas_CrBy As Integer
    Private Mas_Status As String
    Private Mas_IPAddress As String
    Private Mas_CompID As Integer

    Private objDBL As New DatabaseLayer.DBHelper
    Public Property sCust_DelFlg() As String
        Get
            Return (Cust_DelFlg)
        End Get
        Set(ByVal Value As String)
            Cust_DelFlg = Value
        End Set
    End Property
    Public Property iMas_CompID() As Integer
        Get
            Return (Mas_CompID)
        End Get
        Set(ByVal Value As Integer)
            Mas_CompID = Value
        End Set
    End Property

    Public Property sMas_IPAddress() As String
        Get
            Return (Mas_IPAddress)
        End Get
        Set(ByVal Value As String)
            Mas_IPAddress = Value
        End Set
    End Property

    Public Property sMas_Status() As String
        Get
            Return (Mas_Status)
        End Get
        Set(ByVal Value As String)
            Mas_Status = Value
        End Set
    End Property
    Public Property iMas_CrBy() As Integer
        Get
            Return (Mas_CrBy)
        End Get
        Set(ByVal Value As Integer)
            Mas_CrBy = Value
        End Set
    End Property

    Public Property sMas_Contact_Email() As String
        Get
            Return (Mas_Contact_Email)
        End Get
        Set(ByVal Value As String)
            Mas_Contact_Email = Value
        End Set
    End Property

    Public Property sMas_Contact_MobileNo() As String
        Get
            Return (Mas_Contact_MobileNo)
        End Get
        Set(ByVal Value As String)
            Mas_Contact_MobileNo = Value
        End Set
    End Property

    Public Property sMas_Contact_Person() As String
        Get
            Return (Mas_Contact_Person)
        End Get
        Set(ByVal Value As String)
            Mas_Contact_Person = Value
        End Set
    End Property

    Public Property sMas_Loc_Address() As String
        Get
            Return (Mas_Loc_Address)
        End Get
        Set(ByVal Value As String)
            Mas_Loc_Address = Value
        End Set
    End Property

    Public Property iMas_CustID() As Integer
        Get
            Return (Mas_CustID)
        End Get
        Set(ByVal Value As Integer)
            Mas_CustID = Value
        End Set
    End Property

    Public Property sMas_DelFlag() As String
        Get
            Return (Mas_DelFlag)
        End Get
        Set(ByVal Value As String)
            Mas_DelFlag = Value
        End Set
    End Property
    Public Property sMas_Description() As String
        Get
            Return (Mas_Description)
        End Get
        Set(ByVal Value As String)
            Mas_Description = Value
        End Set
    End Property

    Public Property sMas_Code() As String
        Get
            Return (Mas_Code)
        End Get
        Set(ByVal Value As String)
            Mas_Code = Value
        End Set
    End Property

    Public Property iMas_Id() As Integer
        Get
            Return (Mas_Id)
        End Get
        Set(ByVal Value As Integer)
            Mas_Id = Value
        End Set
    End Property

    Public Property iCust_CompID() As Integer
        Get
            Return (Cust_CompID)
        End Get
        Set(ByVal Value As Integer)
            Cust_CompID = Value
        End Set
    End Property

    Public Property sCust_IPAddress() As String
        Get
            Return (Cust_IPAddress)
        End Get
        Set(ByVal Value As String)
            Cust_IPAddress = Value
        End Set
    End Property

    Public Property sCust_Status() As String
        Get
            Return (Cust_Status)
        End Get
        Set(ByVal Value As String)
            Cust_Status = Value
        End Set
    End Property

    Public Property iCust_ApprovedBy() As Integer
        Get
            Return (Cust_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Cust_ApprovedBy = Value
        End Set
    End Property

    Public Property iCust_CrBy() As Integer
        Get
            Return (Cust_CrBy)
        End Get
        Set(ByVal Value As Integer)
            Cust_CrBy = Value
        End Set
    End Property

    Public Property iCust_OrgTypeID() As Integer
        Get
            Return (Cust_OrgTypeID)
        End Get
        Set(ByVal Value As Integer)
            Cust_OrgTypeID = Value
        End Set
    End Property

    Public Property sCust_Code() As String
        Get
            Return (Cust_Code)
        End Get
        Set(ByVal Value As String)
            Cust_Code = Value
        End Set
    End Property

    Public Property sCust_Name() As String
        Get
            Return (Cust_Name)
        End Get
        Set(ByVal Value As String)
            Cust_Name = Value
        End Set
    End Property

    Public Property iCust_ID() As Integer
        Get
            Return (Cust_ID)
        End Get
        Set(ByVal Value As Integer)
            Cust_ID = Value
        End Set
    End Property
    Public Function CheckIndustryType(ByVal sAC As String, ByVal iACID As Integer, ByVal sOrgType As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from Content_Management_Master where cmm_Category ='IND' and Cmm_CompID=" & iACID & " and cmm_Desc = '" & sOrgType & "' and cmm_DelFlag='A'"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckOrganisationType(ByVal sAC As String, ByVal iACID As Integer, ByVal sOrgType As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from Content_Management_Master where cmm_Category ='ORG' and Cmm_CompID=" & iACID & " and cmm_Desc = '" & sOrgType & "' and cmm_DelFlag='A'"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCustType(ByVal sAC As String, ByVal iACID As Integer, ByVal sCUSTNAME As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CUST_ID from SAD_CUSTOMER_MASTER where CUST_NAME ='" & sCUSTNAME & "' and CUST_CompID=" & iACID & "  and CUST_DELFLG='A'"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckProfessionalServicesOffered(ByVal sAC As String, ByVal iACID As Integer, ByVal sOrgType As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from Content_Management_Master where cmm_Category ='AT' and Cmm_CompID=" & iACID & " and cmm_Desc = '" & sOrgType & "' and cmm_DelFlag='A'"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetCustomerCode(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select 'CUST' + Cast(COALESCE(MAX(Cust_ID), 0) + 1 as varchar ) from SAD_CUSTOMER_MASTER"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrgType(ByVal sAC As String, ByVal iACID As Integer, ByVal sOrgType As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Cmm_ID from Content_Management_Master where cmm_Category ='ORG' and Cmm_CompID=" & iACID & " and cmm_Desc = '" & sOrgType & "' and cmm_DelFlag='A'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetIndType(ByVal sAC As String, ByVal iACID As Integer, ByVal sIndType As String) As Integer
    '    Dim sSql As String
    '    Try
    '        sSql = "Select Cmm_ID from Content_Management_Master where cmm_Category ='IND' and Cmm_CompID=" & iACID & " and cmm_Desc = '" & sIndType & "' and cmm_DelFlag='A'"
    '        Return objDBL.SQLExecuteScalar(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetIndType(ByVal sAC As String, ByVal iACID As Integer, ByVal sIndType As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Cmm_ID from Content_Management_Master where cmm_Category ='IND' and Cmm_CompID=" & iACID & " and cmm_Desc = '" & sIndType & "' and cmm_DelFlag='A'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetProfServiceOffType(ByVal sAC As String, ByVal iACID As Integer, ByVal sProfServiceOffType As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select cmm_ID from Content_Management_Master where CMM_CompID=" & iACID & "  And cmm_Category ='AT' And cmm_Delflag='A' and cmm_Desc = '" & sProfServiceOffType & "'"

            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveCustomerMasters(ByVal sAC As String, ByVal objclsCustomerMaster As clsCustomerMaster)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iCust_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_Name", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sCust_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sCust_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_OrgTypeID", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iCust_OrgTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_CrBy", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iCust_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_ApprovedBy", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iCust_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sCust_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sCust_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_DelFlg", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sCust_DelFlg
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iCust_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCustomer_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function SaveCustomerLocationMasters(ByVal sAC As String, ByVal objclsCustomerMaster As clsCustomerMaster)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iMas_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_DelFlag", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CustID", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iMas_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Loc_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_Loc_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Contact_Person", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_Contact_Person
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Contact_MobileNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_Contact_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Contact_Email", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_Contact_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CrBy", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iMas_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsCustomerMaster.sMas_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsCustomerMaster.iMas_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCustomer_Loc_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
