Imports System
Imports System.Data
Imports DatabaseLayer
Imports BusinesLayer
Public Class ClsAssetOpeningBalExcelUpload
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsFASGeneral As New clsGRACeGeneral

    Private AFAA_ID As Integer
    Private AFAA_AssetTrType As Integer
    Private AFAA_CurrencyType As Integer
    Private AFAA_CurrencyAmnt As Double
    Private AFAA_Zone As Integer
    Private AFAA_Region As Integer
    Private AFAA_Area As Integer
    Private AFAA_Branch As Integer
    Private AFAA_ActualLocn As String
    Private AFAA_SupplierName As Integer
    Private AFAA_SupplierCode As Integer
    Private AFAA_TrType As Integer
    Private AFAA_AssetType As String
    Private AFAA_AssetNo As String
    Private AFAA_AssetRefNo As String
    Private AFAA_Description As String
    Private AFAA_ItemCode As String
    Private AFAA_ItemDescription As String
    Private AFAA_Quantity As Integer
    Private AFAA_CommissionDate As Date
    Private AFAA_PurchaseDate As Date
    Private AFAA_AssetAge As Double
    Private AFAA_AssetAmount As Double
    Private AFAA_AssetDelID As Integer
    Private AFAA_AssetDelDate As Date
    Private AFAA_AssetDeletionDate As Date
    Private AFAA_Assetvalue As Double
    Private AFAA_AssetDesc As String
    Private AFAA_CreatedBy As Integer
    Private AFAA_CreatedOn As Date
    Private AFAA_UpdatedBy As Integer
    Private AFAA_UpdatedOn As Date
    Private AFAA_ApprovedBy As Integer
    Private AFAA_ApprovedOn As Date
    Private AFAA_Deletedby As Integer
    Private AFAA_DeletedOn As Date
    Private AFAA_Status As String
    Private AFAA_Delflag As String
    Private AFAA_YearID As Integer
    Private AFAA_CompID As Integer
    Private AFAA_Operation As String
    Private AFAA_IPAddress As String


    Private AFAA_AddnType As String
    Private AFAA_DelnType As String
    Private AFAA_Depreciation As Double
    Private AFAA_AddtnDate As DateTime
    Private AFAA_ItemType As Integer

    Private AFAA_FYAmount As Double
    Private AFAA_DepreAmount As Double

    Private AFAM_Unit As Integer
    Private AFAA_CustId As Integer


    Private AFAA_Location As Integer
    Private AFAA_Division As Integer
    Private AFAA_Department As Integer
    Private AFAA_Bay As Integer
    Private AFAA_EmployeeName As String
    Private AFAA_EmployeeCode As String
    Private AFAA_Code As String

    Public Property iAFAA_CustId() As Integer
        Get
            Return (AFAA_CustId)
        End Get
        Set(ByVal Value As Integer)
            AFAA_CustId = Value
        End Set
    End Property
    Public Property iAFAA_Location() As Integer
        Get
            Return (AFAA_Location)
        End Get
        Set(ByVal Value As Integer)
            AFAA_Location = Value
        End Set
    End Property
    Public Property iAFAA_Division() As Integer
        Get
            Return (AFAA_Division)
        End Get
        Set(ByVal Value As Integer)
            AFAA_Division = Value
        End Set
    End Property
    Public Property iAFAA_Department() As Integer
        Get
            Return (AFAA_Department)
        End Get
        Set(ByVal Value As Integer)
            AFAA_Department = Value
        End Set
    End Property
    Public Property iAFAA_Bay() As Integer
        Get
            Return (AFAA_Bay)
        End Get
        Set(ByVal Value As Integer)
            AFAA_Bay = Value
        End Set
    End Property
    Public Property sAFAA_EmployeeName() As String
        Get
            Return (AFAA_EmployeeName)
        End Get
        Set(ByVal Value As String)
            AFAA_EmployeeName = Value
        End Set
    End Property
    Public Property sAFAA_EmployeeCode() As String
        Get
            Return (AFAA_EmployeeCode)
        End Get
        Set(ByVal Value As String)
            AFAA_EmployeeCode = Value
        End Set
    End Property
    Public Property sAFAA_Code() As String
        Get
            Return (AFAA_Code)
        End Get
        Set(ByVal Value As String)
            AFAA_Code = Value
        End Set
    End Property
    Public Property iAFAM_Unit() As Integer
        Get
            Return (AFAM_Unit)
        End Get
        Set(ByVal Value As Integer)
            AFAM_Unit = Value
        End Set
    End Property
    Public Property dAFAA_Depreciation() As Double
        Get
            Return (AFAA_Depreciation)
        End Get
        Set(ByVal Value As Double)
            AFAA_Depreciation = Value
        End Set
    End Property
    Public Property sAFAA_DelnType() As String
        Get
            Return (AFAA_DelnType)
        End Get
        Set(ByVal Value As String)
            AFAA_DelnType = Value
        End Set
    End Property
    Public Property sAFAA_AddnType() As String
        Get
            Return (AFAA_AddnType)
        End Get
        Set(ByVal Value As String)
            AFAA_AddnType = Value
        End Set
    End Property
    Public Property iAFAA_ID() As Integer
        Get
            Return AFAA_ID
        End Get
        Set(ByVal value As Integer)
            AFAA_ID = value
        End Set
    End Property
    Public Property iAFAA_AssetTrType() As Integer
        Get
            Return AFAA_AssetTrType
        End Get
        Set(value As Integer)
            AFAA_AssetTrType = value
        End Set
    End Property
    Public Property dAFAA_CurrencyAmnt() As Double
        Get
            Return AFAA_CurrencyAmnt
        End Get
        Set(value As Double)
            AFAA_CurrencyAmnt = value
        End Set
    End Property
    Public Property iAFAA_Zone() As Integer
        Get
            Return AFAA_Zone
        End Get
        Set(value As Integer)
            AFAA_Zone = value
        End Set
    End Property
    Public Property iAFAA_Region() As Integer
        Get
            Return AFAA_Region
        End Get
        Set(value As Integer)
            AFAA_Region = value
        End Set
    End Property
    Public Property iAFAA_Area() As Integer
        Get
            Return AFAA_Area
        End Get
        Set(value As Integer)
            AFAA_Area = value
        End Set
    End Property
    Public Property iAFAA_Branch() As Integer
        Get
            Return AFAA_Branch
        End Get
        Set(value As Integer)
            AFAA_Branch = value
        End Set
    End Property
    Public Property sAFAA_ActualLocn() As String
        Get
            Return AFAA_ActualLocn
        End Get
        Set(value As String)
            AFAA_ActualLocn = value
        End Set
    End Property
    Public Property iAFAA_SupplierName() As Integer
        Get
            Return AFAA_SupplierName
        End Get
        Set(value As Integer)
            AFAA_SupplierName = value
        End Set
    End Property
    Public Property iAFAA_SupplierCode() As Integer
        Get
            Return AFAA_SupplierCode
        End Get
        Set(value As Integer)
            AFAA_SupplierCode = value
        End Set
    End Property
    Public Property iAFAA_CurrencyType() As Integer
        Get
            Return AFAA_CurrencyType
        End Get
        Set(value As Integer)
            AFAA_CurrencyType = value
        End Set
    End Property
    Public Property iAFAA_TrType() As Integer
        Get
            Return AFAA_TrType
        End Get
        Set(ByVal value As Integer)
            AFAA_TrType = value
        End Set
    End Property
    Public Property sAFAA_AssetType() As String
        Get
            Return AFAA_AssetType
        End Get
        Set(ByVal value As String)
            AFAA_AssetType = value
        End Set
    End Property
    Public Property sAFAA_AssetNo() As String
        Get
            Return AFAA_AssetNo
        End Get
        Set(ByVal value As String)
            AFAA_AssetNo = value
        End Set
    End Property
    Public Property sAFAA_AssetRefNo() As String
        Get
            Return AFAA_AssetRefNo
        End Get
        Set(ByVal value As String)
            AFAA_AssetRefNo = value
        End Set
    End Property
    Public Property sAFAA_Description() As String
        Get
            Return AFAA_Description
        End Get
        Set(ByVal value As String)
            AFAA_Description = value
        End Set
    End Property
    Public Property sAFAA_ItemCode() As String
        Get
            Return AFAA_ItemCode
        End Get
        Set(ByVal value As String)
            AFAA_ItemCode = value
        End Set
    End Property
    Public Property sAFAA_ItemDescription() As String
        Get
            Return AFAA_ItemDescription
        End Get
        Set(ByVal value As String)
            AFAA_ItemDescription = value
        End Set
    End Property
    Public Property iAFAA_Quantity() As Integer
        Get
            Return AFAA_Quantity
        End Get
        Set(ByVal value As Integer)
            AFAA_Quantity = value
        End Set
    End Property
    Public Property dAFAA_CommissionDate() As Date
        Get
            Return AFAA_CommissionDate
        End Get
        Set(ByVal value As Date)
            AFAA_CommissionDate = value
        End Set
    End Property
    Public Property dAFAA_PurchaseDate() As Date
        Get
            Return AFAA_PurchaseDate
        End Get
        Set(ByVal value As Date)
            AFAA_PurchaseDate = value
        End Set
    End Property
    Public Property dAFAA_AssetAge() As Double
        Get
            Return AFAA_AssetAge
        End Get
        Set(ByVal value As Double)
            AFAA_AssetAge = value
        End Set
    End Property
    Public Property dAFAA_AssetAmount() As Double
        Get
            Return AFAA_AssetAmount
        End Get
        Set(ByVal value As Double)
            AFAA_AssetAmount = value
        End Set
    End Property
    Public Property iAFAA_AssetDelID() As Integer
        Get
            Return AFAA_AssetDelID
        End Get
        Set(ByVal value As Integer)
            AFAA_AssetDelID = value
        End Set
    End Property
    Public Property dAFAA_AssetDelDate() As Date
        Get
            Return AFAA_AssetDelDate
        End Get
        Set(ByVal value As Date)
            AFAA_AssetDelDate = value
        End Set
    End Property
    Public Property dAFAA_AssetDeletionDate() As Date
        Get
            Return AFAA_AssetDeletionDate
        End Get
        Set(ByVal value As Date)
            AFAA_AssetDeletionDate = value
        End Set
    End Property

    Public Property dAFAA_Assetvalue() As Double
        Get
            Return AFAA_Assetvalue
        End Get
        Set(ByVal value As Double)
            AFAA_Assetvalue = value
        End Set
    End Property

    Public Property sAFAA_AssetDesc() As String
        Get
            Return AFAA_AssetDesc
        End Get
        Set(ByVal value As String)
            AFAA_AssetDesc = value
        End Set
    End Property
    Public Property iAFAA_CreatedBy() As Integer
        Get
            Return AFAA_CreatedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_CreatedBy = value
        End Set
    End Property
    Public Property dAFAA_CreatedOn() As Date
        Get
            Return AFAA_CreatedOn
        End Get
        Set(ByVal value As Date)
            AFAA_CreatedOn = value
        End Set
    End Property
    Public Property iAFAA_UpdatedBy() As Integer
        Get
            Return AFAA_UpdatedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_UpdatedBy = value
        End Set
    End Property
    Public Property dAFAA_UpdatedOn() As Date
        Get
            Return AFAA_UpdatedOn
        End Get
        Set(ByVal value As Date)
            AFAA_UpdatedOn = value
        End Set
    End Property
    Public Property iAFAA_ApprovedBy() As Integer
        Get
            Return AFAA_ApprovedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_ApprovedBy = value
        End Set
    End Property
    Public Property dAFAA_ApprovedOn() As Date
        Get
            Return AFAA_ApprovedOn
        End Get
        Set(ByVal value As Date)
            AFAA_ApprovedOn = value
        End Set
    End Property
    Public Property dAFAA_Deletedby() As Integer
        Get
            Return AFAA_Deletedby
        End Get
        Set(ByVal value As Integer)
            AFAA_Deletedby = value
        End Set
    End Property
    Public Property dAFAA_DeletedOn() As Date
        Get
            Return AFAA_DeletedOn
        End Get
        Set(ByVal value As Date)
            AFAA_DeletedOn = value
        End Set
    End Property
    Public Property sAFAA_Status() As String
        Get
            Return AFAA_Status
        End Get
        Set(ByVal value As String)
            AFAA_Status = value
        End Set
    End Property
    Public Property sAFAA_Delflag() As String
        Get
            Return AFAA_Delflag
        End Get
        Set(ByVal value As String)
            AFAA_Delflag = value
        End Set
    End Property
    Public Property iAFAA_YearID() As Integer
        Get
            Return AFAA_YearID
        End Get
        Set(ByVal value As Integer)
            AFAA_YearID = value
        End Set
    End Property
    Public Property iAFAA_CompID() As Integer
        Get
            Return AFAA_CompID
        End Get
        Set(ByVal value As Integer)
            AFAA_CompID = value
        End Set
    End Property
    Public Property sAFAA_Operation() As String
        Get
            Return AFAA_Operation
        End Get
        Set(ByVal value As String)
            AFAA_Operation = value
        End Set
    End Property
    Public Property sAFAA_IPAddress() As String
        Get
            Return AFAA_IPAddress
        End Get
        Set(ByVal value As String)
            AFAA_IPAddress = value
        End Set
    End Property
    Public Property dAFAA_AddtnDate() As Date
        Get
            Return AFAA_AddtnDate
        End Get
        Set(ByVal value As Date)
            AFAA_AddtnDate = value
        End Set
    End Property
    Public Property iAFAA_ItemType() As Integer
        Get
            Return AFAA_ItemType
        End Get
        Set(ByVal value As Integer)
            AFAA_ItemType = value
        End Set
    End Property
    Public Property dAFAA_FYAmount() As Double
        Get
            Return AFAA_FYAmount
        End Get
        Set(ByVal value As Double)
            AFAA_FYAmount = value
        End Set
    End Property
    Public Property dAFAA_DepreAmount() As Double
        Get
            Return AFAA_DepreAmount
        End Get
        Set(ByVal value As Double)
            AFAA_DepreAmount = value
        End Set
    End Property


    Private sAFAA_AssetCode As String
    Private dAFAA_PurchaseAmount As Double
    Private sAFAA_PolicyNo As String
    Private dAFAA_Amount As Double
    Private dAFAA_Date As DateTime
    ' Private iAFAA_Department As Integer
    Private iAFAA_Employee As Integer

    Private sAFAA_ContactPerson As String
    Private sAFAA_Address As String
    Private sAFAA_Phone As String
    Private sAFAA_Fax As String
    Private sAFAA_EmailID As String
    Private sAFAA_Website As String

    Private sAFAA_BrokerName As String
    Private sAFAA_CompanyName As String

    Private sAFAA_WrntyDesc As String
    Private sAFAA_ContactPrsn As String
    Private dAFAA_AMCFrmDate As DateTime
    Private dAFAA_AMCTo As DateTime
    Private sAFAA_Contprsn As String
    Private sAFAA_PhoneNo As String
    Private sAFAA_AMCCompanyName As String
    Private dAFAA_ToDate As DateTime

    Private iAFAA_AssetDeletion As Integer
    Private sAFAA_Remark As String

    Private sAFAA_EMPCode As String
    Private sAFAA_LToWhom As String
    Private dAFAA_LAmount As Double
    Private sAFAA_LAggriNo As String
    Private dAFAA_LDate As DateTime
    Private iAFAA_LCurrencyType As Integer
    Private dAFAA_LExchDate As DateTime
    Public Property AFAA_AssetCode() As String
        Get
            Return sAFAA_AssetCode
        End Get
        Set(ByVal value As String)
            sAFAA_AssetCode = value
        End Set
    End Property
    Public Property AFAA_PurchaseAmount() As Double
        Get
            Return dAFAA_PurchaseAmount
        End Get
        Set(ByVal value As Double)
            dAFAA_PurchaseAmount = value
        End Set
    End Property
    Public Property AFAA_PolicyNo() As String
        Get
            Return sAFAA_PolicyNo
        End Get
        Set(ByVal value As String)
            sAFAA_PolicyNo = value
        End Set
    End Property
    Public Property AFAA_Amount() As Double
        Get
            Return dAFAA_Amount
        End Get
        Set(ByVal value As Double)
            dAFAA_Amount = value
        End Set
    End Property
    Public Property AFAA_Date() As Date
        Get
            Return dAFAA_Date
        End Get
        Set(ByVal value As Date)
            dAFAA_Date = value
        End Set
    End Property
    'Public Property AFAA_Department() As Integer
    '    Get
    '        Return iAFAA_Department
    '    End Get
    '    Set(ByVal value As Integer)
    '        iAFAA_Department = value
    '    End Set
    'End Property
    Public Property AFAA_Employee() As Integer
        Get
            Return iAFAA_Employee
        End Get
        Set(ByVal value As Integer)
            iAFAA_Employee = value
        End Set
    End Property
    Public Property AFAA_ContactPerson() As String
        Get
            Return sAFAA_ContactPerson
        End Get
        Set(ByVal value As String)
            sAFAA_ContactPerson = value
        End Set
    End Property
    Public Property AFAA_Address() As String
        Get
            Return sAFAA_Address
        End Get
        Set(ByVal value As String)
            sAFAA_Address = value
        End Set
    End Property
    Public Property AFAA_Phone() As String
        Get
            Return sAFAA_Phone
        End Get
        Set(ByVal value As String)
            sAFAA_Phone = value
        End Set
    End Property
    Public Property AFAA_Fax() As String
        Get
            Return sAFAA_Fax
        End Get
        Set(ByVal value As String)
            sAFAA_Fax = value
        End Set
    End Property
    Public Property AFAA_EmailID() As String
        Get
            Return sAFAA_EmailID
        End Get
        Set(ByVal value As String)
            sAFAA_EmailID = value
        End Set
    End Property
    Public Property AFAA_Website() As String
        Get
            Return sAFAA_Website
        End Get
        Set(ByVal value As String)
            sAFAA_Website = value
        End Set
    End Property
    Public Property AFAA_BrokerName() As String
        Get
            Return sAFAA_BrokerName
        End Get
        Set(ByVal value As String)
            sAFAA_BrokerName = value
        End Set
    End Property
    Public Property AFAA_CompanyName() As String
        Get
            Return sAFAA_CompanyName
        End Get
        Set(ByVal value As String)
            sAFAA_CompanyName = value
        End Set
    End Property
    Public Property AFAA_ContactPrsn() As String
        Get
            Return sAFAA_ContactPrsn
        End Get
        Set(ByVal value As String)
            sAFAA_ContactPrsn = value
        End Set
    End Property
    Public Property AFAA_AMCFrmDate() As Date
        Get
            Return dAFAA_AMCFrmDate
        End Get
        Set(ByVal value As Date)
            dAFAA_AMCFrmDate = value
        End Set
    End Property
    Public Property AFAA_AMCTo() As Date
        Get
            Return dAFAA_AMCTo
        End Get
        Set(ByVal value As Date)
            dAFAA_AMCTo = value
        End Set
    End Property
    Public Property AFAA_Contprsn() As String
        Get
            Return sAFAA_Contprsn
        End Get
        Set(ByVal value As String)
            sAFAA_Contprsn = value
        End Set
    End Property
    Public Property AFAA_PhoneNo() As String
        Get
            Return sAFAA_PhoneNo
        End Get
        Set(ByVal value As String)
            sAFAA_PhoneNo = value
        End Set
    End Property
    Public Property AFAA_AMCCompanyName() As String
        Get
            Return sAFAA_AMCCompanyName
        End Get
        Set(ByVal value As String)
            sAFAA_AMCCompanyName = value
        End Set
    End Property
    Public Property AFAA_ToDate() As Date
        Get
            Return dAFAA_ToDate
        End Get
        Set(ByVal value As Date)
            dAFAA_ToDate = value
        End Set
    End Property
    Public Property AFAA_AssetDeletion() As Integer
        Get
            Return iAFAA_AssetDeletion
        End Get
        Set(ByVal value As Integer)
            iAFAA_AssetDeletion = value
        End Set
    End Property
    Public Property AFAA_Remark() As String
        Get
            Return sAFAA_Remark
        End Get
        Set(ByVal value As String)
            sAFAA_Remark = value
        End Set
    End Property
    Public Property AFAA_EMPCode() As String
        Get
            Return sAFAA_EMPCode
        End Get
        Set(ByVal value As String)
            sAFAA_EMPCode = value
        End Set
    End Property
    Public Property AFAA_LToWhom() As String
        Get
            Return sAFAA_LToWhom
        End Get
        Set(ByVal value As String)
            sAFAA_LToWhom = value
        End Set
    End Property
    Public Property AFAA_LAmount() As Double
        Get
            Return dAFAA_LAmount
        End Get
        Set(ByVal value As Double)
            dAFAA_LAmount = value
        End Set
    End Property
    Public Property AFAA_LAggriNo() As String
        Get
            Return sAFAA_LAggriNo
        End Get
        Set(ByVal value As String)
            sAFAA_LAggriNo = value
        End Set
    End Property
    Public Property AFAA_LDate() As Date
        Get
            Return dAFAA_LDate
        End Get
        Set(ByVal value As Date)
            dAFAA_LDate = value
        End Set
    End Property
    Public Property AFAA_LCurrencyType() As Integer
        Get
            Return iAFAA_LCurrencyType
        End Get
        Set(ByVal value As Integer)
            iAFAA_LCurrencyType = value
        End Set
    End Property
    Public Property AFAA_LExchDate() As Date
        Get
            Return dAFAA_LExchDate
        End Get
        Set(ByVal value As Date)
            dAFAA_LExchDate = value
        End Set
    End Property
    Public Property AFAA_WrntyDesc() As String
        Get
            Return sAFAA_WrntyDesc
        End Get
        Set(ByVal value As String)
            sAFAA_WrntyDesc = value
        End Set
    End Property
    Dim iFAAD_PKID As Integer
    Dim iFAAD_MasID As Integer
    Dim sFAAD_Particulars As String
    Dim sFAAD_DocNo As String
    Dim dFAAD_DocDate As DateTime
    Dim iFAAD_chkCost As Integer
    Dim dFAAD_BasicCost As Double
    Dim dFAAD_TaxAmount As Double
    Dim dFAAD_Total As Double
    Dim dFAAD_AssetValue As Double
    Dim iFAAD_CreatedBy As Integer
    Dim dFAAD_CreatedOn As DateTime
    Dim idFAAD_UpdatedBy As Integer
    Dim dFAAD_UpdatedOn As DateTime
    Dim sFAAD_IPAddress As String
    Dim iFAAD_CompID As Integer
    Dim sFAAD_Status As String
    Dim iFAAD_AssetType As Integer
    Dim iFAAD_ItemType As Integer
    Dim sFAAD_SupplierName As String
    Public Property FAAD_PKID() As Integer
        Get
            Return iFAAD_PKID
        End Get
        Set(ByVal value As Integer)
            iFAAD_PKID = value
        End Set
    End Property
    Public Property FAAD_MasID() As Integer
        Get
            Return iFAAD_MasID
        End Get
        Set(ByVal value As Integer)
            iFAAD_MasID = value
        End Set
    End Property
    Public Property FAAD_Particulars() As String
        Get
            Return sFAAD_Particulars
        End Get
        Set(ByVal value As String)
            sFAAD_Particulars = value
        End Set
    End Property
    Public Property FAAD_DocNo() As String
        Get
            Return sFAAD_DocNo
        End Get
        Set(ByVal value As String)
            sFAAD_DocNo = value
        End Set
    End Property
    Public Property FAAD_DocDate() As DateTime
        Get
            Return dFAAD_DocDate
        End Get
        Set(ByVal value As DateTime)
            dFAAD_DocDate = value
        End Set
    End Property
    Public Property FAAD_BasicCost() As Double
        Get
            Return dFAAD_BasicCost
        End Get
        Set(ByVal value As Double)
            dFAAD_BasicCost = value
        End Set
    End Property
    Public Property FAAD_TaxAmount() As Double
        Get
            Return dFAAD_TaxAmount
        End Get
        Set(ByVal value As Double)
            dFAAD_TaxAmount = value
        End Set
    End Property
    Public Property FAAD_Total() As Double
        Get
            Return dFAAD_Total
        End Get
        Set(ByVal value As Double)
            dFAAD_Total = value
        End Set
    End Property
    Public Property FAAD_AssetValue() As Double
        Get
            Return dFAAD_AssetValue
        End Get
        Set(ByVal value As Double)
            dFAAD_AssetValue = value
        End Set
    End Property
    Public Property FAAD_CreatedBy() As Integer
        Get
            Return iFAAD_CreatedBy
        End Get
        Set(ByVal value As Integer)
            iFAAD_CreatedBy = value
        End Set
    End Property
    Public Property FAAD_CreatedOn() As DateTime
        Get
            Return dFAAD_CreatedOn
        End Get
        Set(ByVal value As DateTime)
            dFAAD_CreatedOn = value
        End Set
    End Property
    Public Property dFAAD_UpdatedBy() As Integer
        Get
            Return idFAAD_UpdatedBy
        End Get
        Set(ByVal value As Integer)
            idFAAD_UpdatedBy = value
        End Set
    End Property
    Public Property FAAD_UpdatedOn() As DateTime
        Get
            Return dFAAD_UpdatedOn
        End Get
        Set(ByVal value As DateTime)
            dFAAD_UpdatedOn = value
        End Set
    End Property
    Public Property FAAD_IPAddress() As String
        Get
            Return sFAAD_IPAddress
        End Get
        Set(ByVal value As String)
            sFAAD_IPAddress = value
        End Set
    End Property
    Public Property FAAD_CompID() As Integer
        Get
            Return iFAAD_CompID
        End Get
        Set(ByVal value As Integer)
            iFAAD_CompID = value
        End Set
    End Property
    Public Property FAAD_Status() As String
        Get
            Return sFAAD_Status
        End Get
        Set(ByVal value As String)
            sFAAD_Status = value
        End Set
    End Property
    Public Property FAAD_AssetType() As Integer
        Get
            Return iFAAD_AssetType
        End Get
        Set(ByVal value As Integer)
            iFAAD_AssetType = value
        End Set
    End Property
    Public Property FAAD_ItemType() As Integer
        Get
            Return iFAAD_ItemType
        End Get
        Set(ByVal value As Integer)
            iFAAD_ItemType = value
        End Set
    End Property
    Public Property FAAD_SupplierName() As String
        Get
            Return sFAAD_SupplierName
        End Get
        Set(ByVal value As String)
            sFAAD_SupplierName = value
        End Set
    End Property

    Public Function LoadAccZone(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent=0 and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccArea(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccRgn As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccBrnch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccarea As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCurrencyID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCurrencyname As String) As Integer
        Dim sSql As String = ""
        Dim iId As Integer
        Try
            sSql = "Select CUR_ID from Currency_Master where  CUR_CountryName ='" & sCurrencyname & "' and CUR_Status='A'"
            iId = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iId > 0 Then
                Return iId
            Else
                iId = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCurrencyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCurrencyname As String) As String
        Dim sSql As String = ""
        Dim sCode As String
        Try
            sSql = "Select CUR_CODE from Currency_Master where  CUR_CountryName ='" & sCurrencyname & "' and CUR_Status='A'"
            sCode = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If sCode <> "" Then
                Return sCode
            Else
                sCode = ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String) As Integer
        Dim sSql As String = ""
        Dim iId As Integer
        Try
            sSql = "Select CSM_ID From customerSupplierMaster Where CSM_Name='" & sSupplierName & "'  and  CSM_CompID=" & iCompID & ""
            iId = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iId > 0 Then
                Return iId
            Else
                iId = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierID1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String, ByVal sSuplierCode As String) As Integer
        Dim sSql As String = ""
        ' Dim bCheck As Boolean
        Dim iSupId As Integer
        Try
            sSql = "Select CSM_ID From customerSupplierMaster Where CSM_Name='" & sSupplierName & "' and CSM_Code='" & sSuplierCode & "' and  CSM_CompID=" & iCompID & ""
            iSupId = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iSupId > 0 Then
                Return iSupId
            Else
                iSupId = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String) As Integer
        Dim sSql As String = ""
        Dim iID As Integer
        Try
            sSql = "Select CSM_ID From customerSupplierMaster Where CSM_Name='" & sSupplierName & "' and CSM_CompID=" & iCompID & ""
            iID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iID > 0 Then
                Return iID
            Else
                iID = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String, ByVal iCustID As Integer) As Integer
        Dim sSql As String = ""
        Dim iAssetType As Integer
        Try
            sSql = "Select AM_ID From Acc_AssetMaster where AM_Description='" & sSupplierName & "' and AM_CompID=" & iCompID & " and AM_CustId=" & iCustID & " and AM_LevelCode=2"
            iAssetType = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iAssetType > 0 Then
                Return iAssetType
            Else
                iAssetType = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sAsset As String, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim iAssetType As Integer
    '    Try
    '        sSql = "Select AFAM_ID From Acc_FixedAssetMaster where AFAM_ItemDescription='" & sAsset & "' and AFAM_CompID=" & iCompID & " and AFAM_CustId=" & iCustID & ""
    '        iAssetType = objDBL.SQLExecuteScalar(sNameSpace, sSql)
    '        If iAssetType > 0 Then
    '            Return iAssetType
    '        Else
    '            iAssetType = 0
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetAsset1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sAsset As String, ByVal iCustID As Integer, ByVal iAssetClass As Integer) As Integer
        Dim sSql As String = ""
        Dim iAssetType As Integer
        Try
            sSql = "Select AFAM_ID From Acc_FixedAssetMaster where AFAM_ItemDescription='" & sAsset & "' and AFAM_CompID=" & iCompID & " and AFAM_CustId=" & iCustID & " and AFAM_AssetType=" & iAssetClass & ""
            iAssetType = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iAssetType > 0 Then
                Return iAssetType
            Else
                iAssetType = 0
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

            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(AFAM_ID)+1,1) from Acc_FixedAssetMaster where AFAM_CompID=" & iCompID & " and AFAM_YearID='" & iyearId & "'")
            sPrefix = "FAR000-" & iMax
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckExistorNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetType As Integer, ByVal sRefNo As String)
        Dim sSql As String = ""
        Dim dBcheck As Boolean
        Try
            sSql = "Select AFAA_ID From Acc_FixedAssetAdditionDel where AFAA_AssetType=" & iAssetType & " and AFAA_AssetRefNo='" & sRefNo & "' and AFAA_CompID=" & iCompID & ""
            dBcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dBcheck = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetType1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Dim dBcheck As Boolean
        Try
            sSql = "Select AM_Description From Acc_AssetMaster where AM_Description='" & sSupplierName & "' and AM_CompID=" & iCompID & " and AM_CustId=" & iCustID & ""
            dBcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dBcheck = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Dim dBcheck As Boolean
        Try
            sSql = "Select AM_Description From Acc_AssetMaster where AM_Description='" & sSupplierName & "' and AM_CompID=" & iCompID & " and AM_CustId=" & iCustID & ""
            dBcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dBcheck = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Dim dBcheck As Boolean
        Try
            sSql = "Select LS_Description From Acc_AssetLocationSetup where LS_Description='" & sDesc & "' and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
            dBcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dBcheck = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sglDesc As String) As Integer
        Dim sSql As String = ""
        Dim Grp As Integer
        Dim iglID As Integer
        Try
            sSql = "select gl_ID from Chart_Of_Accounts where gl_desc='" & sglDesc & "' and gl_CompId=" & iCompID & ""
            iglID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select IsNull(count(*),0)+1 from acc_fixedAssetAdditionDel where AFAA_AssetType='" & iglID & "' and AFAA_CompID=" & iCompID & ""
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSql))
            Return Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetTypeNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sglDesc As String) As String
        Dim sSql As String = ""
        Dim iglID As Integer
        Try
            sSql = "select gl_ID from Chart_Of_Accounts where gl_desc='" & sglDesc & "' and gl_CompId=" & iCompID & ""
            iglID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select gl_glcode from chart_of_accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            GetAssetTypeNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sglDesc As String) As String
        Dim sSql As String = ""
        Dim iglID As Integer
        Try
            sSql = "select gl_ID from Chart_Of_Accounts where gl_desc='" & sglDesc & "' and gl_CompId=" & iCompID & ""
            iglID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select gl_glcode from Chart_Of_Accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            LoadAssetNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFixedAssetAddition(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objOPExcel As ClsAssetOpeningBalExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(49) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetTrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CurrencyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CurrencyAmnt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = AFAA_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Division", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = AFAA_Division
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = AFAA_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Bay", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = AFAA_Bay
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ActualLocn", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_SupplierName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_SupplierCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetType", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetRefNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ItemCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ItemType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ItemDescription", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CommissionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_PurchaseDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_PurchaseDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetAge", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = "0.0"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_FYAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_FYAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_DepreAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_DepreAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDelID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDelDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDeletionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Assetvalue", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Assetvalue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AddnType", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_DelnType", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Depreciation", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = "0.0"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AddtnDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ApprovedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ItemType", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ItemType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CustId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_FixedAssetAdditionDel", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUnitsOfMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Sunits As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select cmm_ID from Content_Management_Master where cmm_Desc='" & Sunits & "' and CMM_CompID=" & iCompID & " and cmm_Category='UM' order by cmm_ID"
            LoadUnitsOfMeasurement = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return LoadUnitsOfMeasurement
        Catch ex As Exception
        End Try
    End Function
    Public Function LoadLocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As String, ByVal iCustID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select LS_ID from Acc_AssetLocationSetup where LS_Description='" & sLocation & "' and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
            LoadLocation = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return LoadLocation
        Catch ex As Exception
        End Try
    End Function
    Public Function LoadLocation1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As Integer, ByVal iCustID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select LS_ID from Acc_AssetLocationSetup where LS_ID=" & sLocation & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
            LoadLocation1 = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return LoadLocation1
        Catch ex As Exception
        End Try
    End Function
    'Public Function LoadDivision(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As String, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "select LS_ID from Acc_AssetLocationSetup where LS_Description='" & sLocation & "' and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
    '        LoadDivision = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return LoadDivision
    '    Catch ex As Exception
    '    End Try
    'End Function
    'Public Function LoadDepartment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As String, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "select LS_ID from Acc_AssetLocationSetup where LS_Description='" & sLocation & "' and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
    '        LoadDepartment = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return LoadDepartment
    '    Catch ex As Exception
    '    End Try
    'End Function
    'Public Function LoadBay(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As String, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "select LS_ID from Acc_AssetLocationSetup where LS_Description='" & sLocation & "' and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
    '        LoadBay = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return LoadBay
    '    Catch ex As Exception
    '    End Try
    'End Function
    Public Function SaveFixedAssetMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objOPExcel As ClsAssetOpeningBalExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(63) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetType", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ItemCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ItemCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ItemDescription", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ItemDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CommissionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.dAFAA_CommissionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PurchaseDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Unit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAM_Unit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetAge", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetAge
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PurchaseAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PolicyNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Amount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = "0.00"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_BrokerName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CompanyName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Date", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAFAA_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Division", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAFAA_Division
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAFAA_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Bay", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAFAA_Bay
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EmployeeName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EmployeeCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Code", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = AFAA_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_SuplierName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ContactPerson", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Phone", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Fax", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Website", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_DelFlag", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_WrntyDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_ContactPrsn", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AMCFrmDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AMCTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Contprsn", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_PhoneNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AMCCompanyName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_AssetDeletion", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_DlnDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_DateOfDeletion", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Value", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_Remark", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_EMPCode", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LToWhom", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LAmount", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = "0.00"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LAggriNo", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LCurrencyType", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAM_LExchDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = "01/01/1900"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CustId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = AFAA_CustId
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


    'Public Sub SaveFixedAssetMaster(ByVal sAC As String, ByVal iCompID As Integer, ByVal ID As Integer, ByVal AssetType As String,
    '                                ByVal AssetCode As String, ByVal Description As String, ByVal ItemCode As String,
    '                                ByVal ItemDescription As String, ByVal Quantity As Integer, ByVal PurchaseDate As DateTime,
    '                                ByVal CommissionDate As Date, ByVal AssetAge As Double, ByVal AssetAmount As Double,
    '                                ByVal CreatedBy As Integer, ByVal UpdatedBy As Integer, ByVal Delflag As String, ByVal Status As String,
    '                                ByVal YearID As Integer, ByVal CompID As Integer, ByVal Operation As String, ByVal IPAddress As String)

    '    Dim sSql As String = ""
    '    Dim iPKID As Integer = 0

    '    Try
    '        iPKID = objDBL.SQLExecuteScalarInt(sAC, "Select IsNull(Max(AFAM_ID),0)+1 from Acc_FixedAssetMaster")
    '        sSql = "Insert Into Acc_FixedAssetMaster(AFAM_ID,AFAM_AssetType,AFAM_AssetCode,AFAM_Description,AFAM_ItemCode,AFAM_ItemDescription,"
    '        sSql = sSql & " AFAM_Quantity,AFAM_PurchaseDate,AFAM_CommissionDate,AFAM_AssetAge,AFAM_PurchaseAmount,"
    '        sSql = sSql & " AFAM_CreatedBy,AFAM_UpdatedBy,AFAM_DelFlag,AFAM_Status,AFAM_YearID,AFAM_CompID,AFAM_Opeartion,AFAM_IPAddress) Values(" & iPKID & ",'" & AssetType & "','" & AssetCode & "','" & Description & "',"
    '        sSql = sSql & " '" & ItemCode & "','" & ItemDescription & "'," & Quantity & "," & objclsFASGeneral.FormatDtForRDBMS(PurchaseDate, "I") & "," & objclsFASGeneral.FormatDtForRDBMS(PurchaseDate, "I") & "," & AssetAge & "," & AssetAmount & "," & CreatedBy & "," & UpdatedBy & ","
    '        sSql = sSql & " '" & Delflag & "','" & Status & "'," & YearID & "," & CompID & ",'" & Operation & "','" & IPAddress & "')"
    '        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
End Class
