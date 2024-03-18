Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsFexedAsst
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral
    Dim objGnrl As New clsGeneralFunctions

    Private iAFAM_ID As Integer
    Private sAFAM_AssetType As String
    Private sAFAM_AssetCode As String
    Private sAFAM_Description As String
    Private sAFAM_ItemCode As String
    Private sAFAM_ItemDescription As String
    Private iAFAM_Quantity As Integer
    Private iAFAM_Unit As Integer
    Private dAFAM_PurchaseDate As DateTime
    Private dAFAM_InstalationDate As DateTime
    Private dAFAM_CommissionDate As DateTime
    Private dAFAM_AssetAge As Double
    Private dAFAM_PurchaseAmount As Double
    Private sAFAM_PolicyNo As String
    Private dAFAM_Amount As Double
    Private dAFAM_Date As DateTime
    Private iAFAM_Location As Integer
    Private iAFAM_Division As Integer
    Private iAFAM_Department As Integer
    Private iAFAM_Bay As Integer
    Private sAFAM_EmployeeName As String
    Private sAFAM_EmployeeCode As String
    Private sAFAM_Code As String
    Private sAFAM_SuplierName As String
    Private sAFAM_ContactPerson As String
    Private sAFAM_Address As String
    Private sAFAM_Phone As String
    Private sAFAM_Fax As String
    Private sAFAM_EmailID As String
    Private sAFAM_Website As String
    Private sAFAM_BrokerName As String
    Private sAFAM_CompanyName As String
    Private sAFAM_WrntyDesc As String
    Private sAFAM_ContactPrsn As String
    Private dAFAM_AMCFrmDate As DateTime
    Private dAFAM_AMCTo As DateTime
    Private sAFAM_Contprsn As String
    Private sAFAM_PhoneNo As String
    Private sAFAM_AMCCompanyName As String
    Private dAFAM_ToDate As DateTime
    Private iAFAM_AssetDeletion As Integer
    Private dAFAM_DlnDate As DateTime
    Private sAFAM_Remark As String

    Private dAFAM_Value As Double

    Private iAFAM_CreatedBy As Integer
    Private dAFAM_CreatedOn As DateTime
    Private dAFAM_UpdatedOn As DateTime
    Private iAFAM_UpdatedBy As Integer
    Private dAFAM_ApprovedOn As DateTime
    Private iAFAM_ApprovedBy As Integer
    Private iAFAM_DeletedBy As Integer
    Private dAFAM_DeletedOn As DateTime

    Private sAFAM_DelFlag As String
    Private sAFAM_Status As String
    Private iAFAM_YearID As Integer
    Private iAFAM_CompID As Integer
    Private sAFAM_Opeartion As String
    Private sAFAM_IPAddress As String
    Private dAFAM_DateOfDeletion As DateTime
    Private iAFAM_ReasonDeletion As Integer
    Private sAFAM_EMPCode As String
    Private sAFAM_LToWhom As String
    Private dAFAM_LAmount As Double
    Private sAFAM_LAggriNo As String
    Private dAFAM_LDate As DateTime
    Private iAFAM_LCurrencyType As Integer
    Private dAFAM_LExchDate As DateTime

    Private iAFAM_CustId As Integer
    Public Property AFAM_CustId() As Integer
        Get
            Return (iAFAM_CustId)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_CustId = Value
        End Set
    End Property

    Public Property AFAM_LToWhom() As String
        Get
            Return (sAFAM_LToWhom)
        End Get
        Set(ByVal Value As String)
            sAFAM_LToWhom = Value
        End Set
    End Property
    Public Property AFAM_LAmount() As String
        Get
            Return (dAFAM_LAmount)
        End Get
        Set(ByVal Value As String)
            dAFAM_LAmount = Value
        End Set
    End Property
    Public Property AFAM_LAggriNo() As String
        Get
            Return (sAFAM_LAggriNo)
        End Get
        Set(ByVal Value As String)
            sAFAM_LAggriNo = Value
        End Set
    End Property
    Public Property AFAM_LDate() As String
        Get
            Return (dAFAM_LDate)
        End Get
        Set(ByVal Value As String)
            dAFAM_LDate = Value
        End Set
    End Property
    Public Property AFAM_LCurrencyType() As String
        Get
            Return (iAFAM_LCurrencyType)
        End Get
        Set(ByVal Value As String)
            iAFAM_LCurrencyType = Value
        End Set
    End Property
    Public Property AFAM_LExchDate() As String
        Get
            Return (dAFAM_LExchDate)
        End Get
        Set(ByVal Value As String)
            dAFAM_LExchDate = Value
        End Set
    End Property
    Public Property AFAM_EMPCode() As String
        Get
            Return (sAFAM_EMPCode)
        End Get
        Set(ByVal Value As String)
            sAFAM_EMPCode = Value
        End Set
    End Property
    Public Property AFAM_DateOfDeletion() As DateTime
        Get
            Return (dAFAM_DateOfDeletion)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_DateOfDeletion = Value
        End Set
    End Property
    Public Property AFAM_ReasonDeletion() As Integer
        Get
            Return (iAFAM_ReasonDeletion)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_ReasonDeletion = Value
        End Set
    End Property
    Public Property AFAM_ID() As Integer
        Get
            Return (iAFAM_ID)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_ID = Value
        End Set
    End Property
    Public Property AFAM_AssetType() As String
        Get
            Return (sAFAM_AssetType)
        End Get
        Set(ByVal Value As String)
            sAFAM_AssetType = Value
        End Set
    End Property
    Public Property AFAM_AssetCode() As String
        Get
            Return (sAFAM_AssetCode)
        End Get
        Set(ByVal Value As String)
            sAFAM_AssetCode = Value
        End Set
    End Property
    Public Property AFAM_Description() As String
        Get
            Return (sAFAM_Description)
        End Get
        Set(ByVal Value As String)
            sAFAM_Description = Value
        End Set
    End Property
    Public Property AFAM_ItemCode() As String
        Get
            Return (sAFAM_ItemCode)
        End Get
        Set(ByVal Value As String)
            sAFAM_ItemCode = Value
        End Set
    End Property
    Public Property AFAM_ItemDescription() As String
        Get
            Return (sAFAM_ItemDescription)
        End Get
        Set(ByVal Value As String)
            sAFAM_ItemDescription = Value
        End Set
    End Property
    Public Property AFAM_CommissionDate() As DateTime
        Get
            Return (dAFAM_CommissionDate)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_CommissionDate = Value
        End Set
    End Property
    Public Property AFAM_AssetAge() As Double
        Get
            Return (dAFAM_AssetAge)
        End Get
        Set(ByVal Value As Double)
            dAFAM_AssetAge = Value
        End Set
    End Property
    Public Property AFAM_PurchaseDate() As DateTime
        Get
            Return (dAFAM_PurchaseDate)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_PurchaseDate = Value
        End Set
    End Property

    Public Property AFAM_InstalationDate() As DateTime
        Get
            Return (dAFAM_InstalationDate)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_InstalationDate = Value
        End Set
    End Property
    Public Property AFAM_Quantity() As Integer
        Get
            Return (iAFAM_Quantity)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_Quantity = Value
        End Set
    End Property
    Public Property AFAM_Unit() As Integer
        Get
            Return (iAFAM_Unit)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_Unit = Value
        End Set
    End Property

    Public Property AFAM_PurchaseAmount() As Double
        Get
            Return (dAFAM_PurchaseAmount)
        End Get
        Set(ByVal Value As Double)
            dAFAM_PurchaseAmount = Value
        End Set
    End Property
    Public Property AFAM_Value() As Double
        Get
            Return (dAFAM_Value)
        End Get
        Set(ByVal Value As Double)
            dAFAM_Value = Value
        End Set
    End Property
    Public Property AFAM_PolicyNo() As String
        Get
            Return (sAFAM_PolicyNo)
        End Get
        Set(ByVal Value As String)
            sAFAM_PolicyNo = Value
        End Set
    End Property

    Public Property AFAM_Amount() As Double
        Get
            Return (dAFAM_Amount)
        End Get
        Set(ByVal Value As Double)
            dAFAM_Amount = Value
        End Set
    End Property

    Public Property AFAM_ToDate() As DateTime
        Get
            Return (dAFAM_ToDate)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_ToDate = Value
        End Set
    End Property
    Public Property AFAM_Date() As DateTime
        Get
            Return (dAFAM_Date)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_Date = Value
        End Set
    End Property
    Public Property AFAM_BrokerName() As String
        Get
            Return (sAFAM_BrokerName)
        End Get
        Set(ByVal Value As String)
            sAFAM_BrokerName = Value
        End Set
    End Property
    Public Property AFAM_CompanyName() As String
        Get
            Return (sAFAM_CompanyName)
        End Get
        Set(ByVal Value As String)
            sAFAM_CompanyName = Value
        End Set
    End Property
    Public Property AFAM_Location() As Integer
        Get
            Return (iAFAM_Location)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_Location = Value
        End Set
    End Property
    Public Property AFAM_Division() As Integer
        Get
            Return (iAFAM_Division)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_Division = Value
        End Set
    End Property
    Public Property AFAM_Department() As Integer
        Get
            Return (iAFAM_Department)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_Department = Value
        End Set
    End Property
    Public Property AFAM_Bay() As Integer
        Get
            Return (iAFAM_Bay)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_Bay = Value
        End Set
    End Property
    Public Property AFAM_EmployeeName() As String
        Get
            Return (sAFAM_EmployeeName)
        End Get
        Set(ByVal Value As String)
            sAFAM_EmployeeName = Value
        End Set
    End Property
    Public Property AFAM_EmployeeCode() As String
        Get
            Return (sAFAM_EmployeeCode)
        End Get
        Set(ByVal Value As String)
            sAFAM_EmployeeCode = Value
        End Set
    End Property
    Public Property AFAM_Code() As String
        Get
            Return (sAFAM_Code)
        End Get
        Set(ByVal Value As String)
            sAFAM_Code = Value
        End Set
    End Property
    Public Property AFAM_SuplierName() As String
        Get
            Return (sAFAM_SuplierName)
        End Get
        Set(ByVal Value As String)
            sAFAM_SuplierName = Value
        End Set
    End Property
    Public Property AFAM_ContactPerson() As String
        Get
            Return (sAFAM_ContactPerson)
        End Get
        Set(ByVal Value As String)
            sAFAM_ContactPerson = Value
        End Set
    End Property
    Public Property AFAM_Address() As String
        Get
            Return (sAFAM_Address)
        End Get
        Set(ByVal Value As String)
            sAFAM_Address = Value
        End Set
    End Property

    Public Property AFAM_Phone() As String
        Get
            Return (sAFAM_Phone)
        End Get
        Set(ByVal Value As String)
            sAFAM_Phone = Value
        End Set
    End Property

    Public Property AFAM_Fax() As String
        Get
            Return (sAFAM_Fax)
        End Get
        Set(ByVal Value As String)
            sAFAM_Fax = Value
        End Set
    End Property
    Public Property AFAM_EmailID() As String
        Get
            Return (sAFAM_EmailID)
        End Get
        Set(ByVal Value As String)
            sAFAM_EmailID = Value
        End Set
    End Property
    Public Property AFAM_WrntyDesc() As String
        Get
            Return (sAFAM_WrntyDesc)
        End Get
        Set(ByVal Value As String)
            sAFAM_WrntyDesc = Value
        End Set
    End Property
    Public Property AFAM_ContactPrsn() As String
        Get
            Return (sAFAM_ContactPrsn)
        End Get
        Set(ByVal Value As String)
            sAFAM_ContactPrsn = Value
        End Set
    End Property

    Public Property AFAM_AMCFrmDate() As DateTime
        Get
            Return (dAFAM_AMCFrmDate)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_AMCFrmDate = Value
        End Set
    End Property
    Public Property AFAM_AMCTo() As DateTime
        Get
            Return (dAFAM_AMCTo)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_AMCTo = Value
        End Set
    End Property
    Public Property AFAM_Contprsn() As String
        Get
            Return (sAFAM_Contprsn)
        End Get
        Set(ByVal Value As String)
            sAFAM_Contprsn = Value
        End Set
    End Property

    Public Property AFAM_PhoneNo() As String
        Get
            Return (sAFAM_PhoneNo)
        End Get
        Set(ByVal Value As String)
            sAFAM_PhoneNo = Value
        End Set
    End Property

    Public Property AFAM_AMCCompanyName() As String
        Get
            Return (sAFAM_AMCCompanyName)
        End Get
        Set(ByVal Value As String)
            sAFAM_AMCCompanyName = Value
        End Set
    End Property
    Public Property AFAM_Website() As String
        Get
            Return (sAFAM_Website)
        End Get
        Set(ByVal Value As String)
            sAFAM_Website = Value
        End Set
    End Property
    Public Property AFAM_CreatedBy() As Integer
        Get
            Return (iAFAM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_CreatedBy = Value
        End Set
    End Property
    Public Property AFAM_CreatedOn() As DateTime
        Get
            Return (dAFAM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_CreatedOn = Value
        End Set
    End Property
    Public Property AFAM_UpdatedOn() As DateTime
        Get
            Return (dAFAM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_UpdatedOn = Value
        End Set
    End Property
    Public Property AFAM_UpdatedBy() As Integer
        Get
            Return (iAFAM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_UpdatedBy = Value
        End Set
    End Property

    Public Property AFAM_ApprovedOn() As DateTime
        Get
            Return (dAFAM_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_ApprovedOn = Value
        End Set
    End Property
    Public Property AFAM_ApprovedBy() As Integer
        Get
            Return (iAFAM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_ApprovedBy = Value
        End Set
    End Property

    Public Property AFAM_DeletedBy() As Integer
        Get
            Return (iAFAM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_DeletedBy = Value
        End Set
    End Property
    Public Property AFAM_DeletedOn() As DateTime
        Get
            Return (dAFAM_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_DeletedOn = Value
        End Set
    End Property

    Public Property AFAM_DelFlag() As String
        Get
            Return (sAFAM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sAFAM_DelFlag = Value
        End Set
    End Property
    Public Property AFAM_Status() As String
        Get
            Return (sAFAM_Status)
        End Get
        Set(ByVal Value As String)
            sAFAM_Status = Value
        End Set
    End Property
    Public Property AFAM_YearID() As Integer
        Get
            Return (iAFAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_YearID = Value
        End Set
    End Property
    Public Property AFAM_CompID() As Integer
        Get
            Return (iAFAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_CompID = Value
        End Set
    End Property
    Public Property AFAM_Opeartion() As String
        Get
            Return (sAFAM_Opeartion)
        End Get
        Set(ByVal Value As String)
            sAFAM_Opeartion = Value
        End Set
    End Property
    Public Property AFAM_IPAddress() As String
        Get
            Return (sAFAM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sAFAM_IPAddress = Value
        End Set
    End Property

    Public Property AFAM_AssetDeletion() As Integer
        Get
            Return (iAFAM_AssetDeletion)
        End Get
        Set(ByVal Value As Integer)
            iAFAM_AssetDeletion = Value
        End Set
    End Property
    Public Property AFAM_DlnDate() As DateTime
        Get
            Return (dAFAM_DlnDate)
        End Get
        Set(ByVal Value As DateTime)
            dAFAM_DlnDate = Value
        End Set
    End Property
    Public Property AFAM_Remark() As String
        Get
            Return (sAFAM_Remark)
        End Get
        Set(ByVal Value As String)
            sAFAM_Remark = Value
        End Set
    End Property

    Dim iSUP_ID As Integer
    Dim sSUP_Name As String
    Dim sSUP_Code As String
    Dim sSUP_ContactPerson As String
    Dim sSUP_Address As String
    Dim sSUP_PhoneNo As String
    Dim sSUP_Fax As String
    Dim sSUP_Email As String
    Dim sSUP_Website As String
    Dim iSUP_CRBY As Integer
    Dim dSUP_CRON As DateTime
    Dim sSUP_STATUS As String
    Dim sSUP_IPAddress As String
    Dim iSUP_CompID As Integer

    Public Property SUP_ID() As Integer
        Get
            Return (iSUP_ID)
        End Get
        Set(ByVal Value As Integer)
            iSUP_ID = Value
        End Set
    End Property
    Public Property SUP_Name() As String
        Get
            Return (sSUP_Name)
        End Get
        Set(ByVal Value As String)
            sSUP_Name = Value
        End Set
    End Property
    Public Property SUP_Code() As String
        Get
            Return (sSUP_Code)
        End Get
        Set(ByVal Value As String)
            sSUP_Code = Value
        End Set
    End Property
    Public Property SUP_ContactPerson() As String
        Get
            Return (sSUP_ContactPerson)
        End Get
        Set(ByVal Value As String)
            sSUP_ContactPerson = Value
        End Set
    End Property
    Public Property SUP_Address() As String
        Get
            Return (sSUP_Address)
        End Get
        Set(ByVal Value As String)
            sSUP_Address = Value
        End Set
    End Property
    Public Property SUP_PhoneNo() As String
        Get
            Return (sSUP_PhoneNo)
        End Get
        Set(ByVal Value As String)
            sSUP_PhoneNo = Value
        End Set
    End Property
    Public Property SUP_Fax() As String
        Get
            Return (sSUP_Fax)
        End Get
        Set(ByVal Value As String)
            sSUP_Fax = Value
        End Set
    End Property
    Public Property SUP_Email() As String
        Get
            Return (sSUP_Email)
        End Get
        Set(ByVal Value As String)
            sSUP_Email = Value
        End Set
    End Property
    Public Property SUP_Website() As String
        Get
            Return (sSUP_Website)
        End Get
        Set(ByVal Value As String)
            sSUP_Website = Value
        End Set
    End Property
    Public Property SUP_CRBY() As Integer
        Get
            Return (iSUP_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iSUP_CRBY = Value
        End Set
    End Property
    Public Property SUP_CRON() As DateTime
        Get
            Return (dSUP_CRON)
        End Get
        Set(ByVal Value As DateTime)
            dSUP_CRON = Value
        End Set
    End Property
    Public Property SUP_STATUS() As String
        Get
            Return (sSUP_STATUS)
        End Get
        Set(ByVal Value As String)
            sSUP_STATUS = Value
        End Set
    End Property
    Public Property SUP_IPAddress() As String
        Get
            Return (sSUP_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSUP_IPAddress = Value
        End Set
    End Property
    Public Property SUP_CompID() As Integer
        Get
            Return (iSUP_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSUP_CompID = Value
        End Set
    End Property
    Public Function loadExistingSupplier(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iid > 0 Then
                sSql = "Select SUP_Name,SUP_ID from SAD_SUPPLIER_MASTER where SUP_CompID=" & iCompID & " and SUP_ID=" & iid & "  order by SUP_ID"
            Else
                sSql = "Select SUP_Name,SUP_ID from SAD_SUPPLIER_MASTER where SUP_CompID=" & iCompID & "  order by SUP_ID"
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
        End Try
    End Function

    Public Function SaveSupplierDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objFxdAsst As ClsFexedAsst) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Name", OleDb.OleDbType.VarChar, 150)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_ContactPerson", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Address", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_PhoneNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_PhoneNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Fax", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Email", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Website", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_Website
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_CRBY", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_CRON", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_CRON
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_STATUS", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.SUP_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSAD_SUPPLIER_MASTER", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception

            Throw
        End Try
    End Function

    Public Function LoadSupplier(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAsset As String)
        Dim sSql As String
        Dim checkitemcode As Boolean
        Try
            sSql = " Select SUP_Name from SAD_SUPPLIER_MASTER Where SUP_Name = '" & iAsset & "' And SUP_CompID=" & iCompID & ""
            checkitemcode = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If checkitemcode = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUnitsOfMeasur(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select cmm_ID,cmm_Desc from Content_Management_Master where CMM_CompID=" & iCompID & " and cmm_Category='UM' order by cmm_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
        End Try
    End Function

    Public Function LoadExistingItemCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal AFAM_AssetType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AFAM_ItemDescription,AFAM_ID from Acc_FixedAssetMaster where AFAM_AssetType=" & AFAM_AssetType & " and AFAM_CompID=" & iCompID & "  order by AFAM_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
        End Try
    End Function

    Public Function LoadFxdAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_DelFlag='A' and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadAAsetType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustId As Integer, ByVal sAsst As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_DelFlag='A' and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & " and AM_Description <> '" & sAsst & "'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Loademployee(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select usr_Id,usr_FullName From Sad_UserDetails  Where Usr_CompID='" & iCompID & "' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDepartment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "

            'sSql = "Select * From Sad_Org_Structure where Org_CompID='" & iCompID & "' and Org_Parent in( Select Org_node From Sad_Org_Structure where Org_Parent in (Select Org_Node From Sad_Org_Structure where Org_Parent in(  Select Org_Node From Sad_Org_Structure where Org_Parent in(Select Org_Node From Sad_Org_Structure where Org_Parent=0))))"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFxedAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objFxdAsst As ClsFexedAsst) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(63) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetType", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_AssetType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_AssetCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ItemCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_ItemCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ItemDescription", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_ItemDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CommissionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_CommissionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PurchaseDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_PurchaseDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Unit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Unit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetAge", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_AssetAge
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PurchaseAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_PurchaseAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PolicyNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_PolicyNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Amount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_BrokerName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_BrokerName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CompanyName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_CompanyName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Date", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Division", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Division
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Bay", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Bay
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EmployeeName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_EmployeeName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EmployeeCode ", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_EmployeeCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Code ", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_SuplierName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_SuplierName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ContactPerson", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Phone", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Phone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Fax", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Website", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Website
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_DelFlag", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_WrntyDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_WrntyDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ContactPrsn", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_ContactPrsn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AMCFrmDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_AMCFrmDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AMCTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_AMCTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Contprsn", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Contprsn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PhoneNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_PhoneNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AMCCompanyName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_AMCCompanyName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetDeletion", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_AssetDeletion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_DlnDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_DlnDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_DateOfDeletion", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_DateOfDeletion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Value", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Value
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Remark", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_Remark
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EMPCode", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_EMPCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LToWhom", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_LToWhom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LAmount", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_LAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LAggriNo", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_LAggriNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_LDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LCurrencyType", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_LCurrencyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LExchDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_LExchDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CustId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objFxdAsst.AFAM_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_FixedAssetMaster", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function showDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAFAM_ItemCode As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from Acc_FixedAssetMaster Where AFAM_ID=" & sAFAM_ItemCode & " And AFAM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function showSupDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAFAM_ItemCode As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from SAD_SUPPLIER_MASTER Where SUP_ID=" & sAFAM_ItemCode & " And SUP_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateClass(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAssetId As Integer, ByVal sRemarks As String, ByVal iPkId As Integer, ByVal iTRYear As Integer, ByVal iTRAssetType As Integer, ByVal iAssetAge As String, ByVal iTrAssetAge As String, ByVal iUserId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "UPDATE Acc_FixedAssetMaster SET AFAM_AssetType = '" & iAssetId & "',AFAM_Remark = '" & sRemarks & "',AFAM_TRStatus = 1,AFAM_TRYear =" & iTRYear & ",AFAM_TRAssetType =" & iTRAssetType & ",AFAM_AssetAge = '" & iTrAssetAge & "',AFAM_TrAssetAge = '" & iAssetAge & "',AFAM_TrUpdatedBy = " & iUserId & "  WHERE AFAM_ID=" & iPkId & " And AFAM_CompID=" & iCompID & " and AFAM_YearID ='" & iYearID & "'"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            sSql = "Update Acc_FixedAssetAdditionDetails SET FAAD_AssetType =" & iAssetId & " where FAAD_AssetType = " & iTRAssetType & " and FAAD_YearID = " & iYearID & " and FAAD_CompID =" & iCompID & " and FAAD_ItemType = " & iPkId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            sSql = "Update Acc_FixedAssetAdditionDel SET AFAA_AssetType =" & iAssetId & " where AFAA_AssetType = " & iTRAssetType & " and AFAA_YearID = " & iYearID & " and AFAA_CompID =" & iCompID & " and AFAA_ItemType = " & iPkId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub StatusCheck(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAFAM_AssetType As Integer, ByVal iAFAM_ItemId As Integer, ByVal sIPAddress As String, ByVal sStatus As String, ByVal iCustId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_FixedAssetMaster Set  AFAM_Status='A',AFAM_DelFlag='A'"
            sSql = sSql & " Where AFAM_ID=" & iAFAM_ItemId & " And AFAM_AssetType=" & iAFAM_AssetType & " and AFAM_CompID=" & iCompID & " and AFAM_CustId=" & iCustId & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAFAM_AssetType As Integer, ByVal iAFAM_ItemId As Integer, ByVal iyearId As Integer, ByVal iCustId As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select AFAM_Status from  Acc_FixedAssetMaster"
            sSql = sSql & " Where AFAM_ID=" & iAFAM_ItemId & " And AFAM_AssetType=" & iAFAM_AssetType & " and AFAM_CompID=" & iCompID & " and AFAM_CustId=" & iCustId & ""
            GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLocationZone(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_COde='" & sNameSpace & "' and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetDeletion(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from ACC_General_Master where Mas_master in(select Mas_Id from ACC_Master_Type where Mas_Type='Asset Deletion Reasons')and Mas_CompID =" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmpCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iEmpId As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try

            sSql = " select usr_Code from Sad_UserDetails where usr_Id=" & iEmpId & " and Usr_CompID=" & iCompID & ""
            GetEmpCode = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return GetEmpCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAssteType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable

        Dim dcheck As New Boolean
        Dim iID As String = ""
        Try
            'sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_COde='" & sNameSpace & "' and Org_CompID=" & iCompID & " )"

            sSql = "Select * From Chart_Of_Accounts Where GL_Desc='" & sAssettype & "' and GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Non Current Assets'))"
            dcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)

            If dcheck = True Then
                sSql = objDBL.SQLExecuteScalarInt(sNameSpace, "Select GL_ID From Chart_Of_Accounts Where GL_Desc='" & sAssettype & "' and GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Non Current Assets'))")
                iID = sSql
                Return iID
            Else
                iID = 0
                Return iID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iCustId As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try

            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(count(AFAM_ID)+1,1) from Acc_FixedAssetMaster where AFAM_CompID =" & iCompID & " and AFAM_YearID='" & iyearId & "' and AFAM_CustId='" & iCustId & "'  ")
            If iMax = 0 Then
                iMax = 1
            End If
            sPrefix = "FAR000-" & iMax
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadLevelCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iLocationId As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim Slevelcode As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            sSql = "Select LS_DescCode From Acc_AssetLocationSetup where LS_ID=" & iLocationId & " and LS_CustId=" & iCustID & " and LS_CompID=" & iCompID & ""
            Slevelcode = objDBL.SQLGetDescription(sNameSpace, sSql)

            LoadLevelCode = Slevelcode
            Return LoadLevelCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function TocheckExistitemcode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal itemcode As String, ByVal iyearId As Integer)
        Dim checkitemcode As Boolean
        Dim sSql As String
        Try
            sSql = "Select AFAM_ItemCode From Acc_FixedAssetMaster where AFAM_ItemCode='" & itemcode & "' and AFAM_AssetType=" & iAssetClassId & " and  AFAM_YearID='" & iyearId & "' and AFAM_CompID=" & iCompID & ""
            checkitemcode = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If checkitemcode = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function TocheckExistAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal itemcode As String, ByVal iyearId As Integer, ByVal iCustId As Integer)
        Dim checkitemcode As Boolean
        Dim sSql As String
        Try
            sSql = "Select AFAM_ItemDescription From Acc_FixedAssetMaster where AFAM_ItemDescription='" & itemcode & "' and AFAM_AssetType=" & iAssetClassId & " and AFAM_CustId=" & iCustId & " and AFAM_CompID=" & iCompID & ""
            checkitemcode = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If checkitemcode = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function TocheckExistAssetDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal itemcode As String, ByVal iyearId As Integer, ByVal iCustId As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "Select AFAM_ItemDescription,AFAM_DelFlag From Acc_FixedAssetMaster where AFAM_ItemDescription='" & itemcode & "' and AFAM_AssetType=" & iAssetClassId & " and AFAM_CustId=" & iCustId & " and AFAM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CUR_ID,CUR_CODE + '-' + CUR_CountryName as CUR_CountryName from Currency_Master where CUR_Status='A' order by CUR_CountryName asc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFilesCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Count(PGE_BASENAME) from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "' and FOL_CompID=" & iCompID & ") And pge_CompID=" & iCompID & " "
            BindAttachFilesCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return BindAttachFilesCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFiles(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select pge_Orignalfilename,pge_ext,pge_createdon from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "' and FOL_CompID=" & iCompID & ") And pge_CompID=" & iCompID & " "
            BindAttachFiles = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindAttachFiles
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabinetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal sCustomerName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from EDT_CABINET where CBN_NAME='" & sCustomerName & "' and CBN_CompID=" & iCompID & " And CBN_Parent=-1 "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select CBN_NODE from EDT_CABINET where CBN_NAME='" & sCustomerName & "' and CBN_CompID=" & iCompID & " And CBN_Parent=-1 "
                GetCabinetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_NODE")
                sSql = "" : sSql = "Insert Into EDT_CABINET(CBN_NODE,CBN_NAME,CBN_PARENT,CBN_Note,CBN_USERGROUP,CBN_USERID,CBN_ParGrp,CBN_PERMISSION,cbn_DelStatus,CBN_SCCount,CBN_FolCount,cbn_Operation) "
                sSql = sSql & "Values(" & iMaxID & ",'" & sCustomerName & "'," & -1 & ",'" & sCustomerName & "',0," & iUserID & ",0,0,'A',0,0,'X')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetCabinetID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubCabinetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCabinetID As Integer, ByVal sTrTypeName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from EDT_CABINET where CBN_NAME='" & sTrTypeName & "' and CBN_CompID=" & iCompID & " And CBN_Parent=" & iCabinetID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select CBN_NODE from EDT_CABINET where CBN_NAME='" & sTrTypeName & "' and CBN_CompID=" & iCompID & " And CBN_Parent=" & iCabinetID & " "
                GetSubCabinetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_NODE")
                sSql = "" : sSql = "Insert Into EDT_CABINET(CBN_NODE,CBN_NAME,CBN_PARENT,CBN_Note,CBN_USERGROUP,CBN_USERID,CBN_ParGrp,CBN_PERMISSION,cbn_DelStatus,CBN_SCCount,CBN_FolCount,cbn_Operation) "
                sSql = sSql & "Values(" & iMaxID & ",'" & sTrTypeName & "'," & iCabinetID & ",'" & sTrTypeName & "',0," & iUserID & ",0,0,'A',0,0,'X')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetSubCabinetID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFolderID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCabinetID As Integer, ByVal sFolderName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " and FOL_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select FOL_FOLID from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " and FOL_CompID=" & iCompID & ""
                GetFolderID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "edt_folder", "FOL_FOLID")
                sSql = "" : sSql = "Insert Into edt_folder(FOL_FOLID,FOL_CABINET,FOL_NAME,FOL_NOTES,FOL_CRBY,FOL_STATUS,FOL_PAGECOUNT,fol_operation,fol_operationBy) "
                sSql = sSql & "Values(" & iMaxID & "," & iCabinetID & ",'" & sFolderName & "','" & sFolderName & "'," & iUserID & ",'A',0,'I'," & iUserID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetFolderID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBaseID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From EDT_Page Where PGE_CABINET=" & iCabinet & " And PGE_SUBCABINET=" & iSubCabinet & " And PGE_Folder=" & iFolder & " And PGE_CompID=" & iCompID & " "
            GetBaseID = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetBaseID
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
