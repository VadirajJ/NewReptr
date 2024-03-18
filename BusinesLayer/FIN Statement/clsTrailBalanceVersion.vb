Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class clsTrailBalanceVersion
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    ' For Version maintain

    Private ATBV_ID As Integer
    Private ATBV_CODE As String
    Private ATBV_Description As String
    Private ATBV_CustId As Integer
    Private ATBV_VersionNo As Integer
    Private ATBV_Branchname As Integer
    Private ATBV_Opening_Debit_Amount As Double
    Private ATBV_Opening_Credit_Amount As Double
    Private ATBV_TR_Debit_Amount As Double
    Private ATBV_TR_Credit_Amount As Double
    Private ATBV_Closing_Debit_Amount As Double
    Private ATBV_Closing_Credit_Amount As Double
    Private ATBV_DELFLG As String
    Private ATBV_CRBY As Integer
    Private ATBV_STATUS As String
    Private ATBV_UPDATEDBY As Integer
    Private ATBV_IPAddress As String
    Private ATBV_CompId As Integer
    Private ATBV_YEARId As Integer


    Private ATBVD_ID As Integer
    Private ATBVD_Masid As Integer
    Private ATBVD_CODE As String
    Private ATBVD_Description As String
    Private ATBVD_CustId As Integer
    Private ATBVD_VersionNo As Integer
    Private ATBVD_SChedule_Type As Integer
    Private ATBVD_Branchname As Integer
    Private ATBVD_Company_Type As Integer
    Private ATBVD_Headingid As Integer
    Private ATBVD_Subheading As Integer
    Private ATBVD_itemid As Integer
    Private ATBVD_Subitemid As Integer
    Private ATBVD_DELFLG As String
    Private ATBVD_CRBY As Integer
    Private ATBVD_STATUS As String
    Private ATBVD_Progress As String
    Private ATBVD_UPDATEDBY As Integer
    Private ATBVD_IPAddress As String
    Private ATBVD_CompId As Integer
    Private ATBVD_YEARId As Integer
    Private ATBVD_iFLAG As Integer

    Public Property iATBV_ID() As Integer
        Get
            Return (ATBV_ID)
        End Get
        Set(ByVal Value As Integer)
            ATBV_ID = Value
        End Set
    End Property
    Public Property sATBV_CODE() As String
        Get
            Return (ATBV_CODE)
        End Get
        Set(ByVal Value As String)
            ATBV_CODE = Value
        End Set
    End Property
    Public Property sATBV_Description() As String
        Get
            Return (ATBV_Description)
        End Get
        Set(ByVal Value As String)
            ATBV_Description = Value
        End Set
    End Property
    Public Property iATBV_CustId() As Integer
        Get
            Return (ATBV_CustId)
        End Get
        Set(ByVal Value As Integer)
            ATBV_CustId = Value
        End Set
    End Property
    Public Property iATBV_VersionNo() As Integer
        Get
            Return (ATBV_VersionNo)
        End Get
        Set(ByVal Value As Integer)
            ATBV_VersionNo = Value
        End Set
    End Property
    Public Property iATBV_Branchname() As Integer
        Get
            Return (ATBV_Branchname)
        End Get
        Set(ByVal Value As Integer)
            ATBV_Branchname = Value
        End Set
    End Property

    Public Property dATBV_Opening_Debit_Amount() As Double
        Get
            Return (ATBV_Opening_Debit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBV_Opening_Debit_Amount = Value
        End Set
    End Property

    Public Property dATBV_Opening_Credit_Amount() As Double
        Get
            Return (ATBV_Opening_Credit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBV_Opening_Credit_Amount = Value
        End Set
    End Property
    Public Property dATBV_TR_Debit_Amount() As Double
        Get
            Return (ATBV_TR_Debit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBV_TR_Debit_Amount = Value
        End Set
    End Property
    Public Property dATBV_TR_Credit_Amount() As Double
        Get
            Return (ATBV_TR_Credit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBV_TR_Credit_Amount = Value
        End Set
    End Property
    Public Property dATBV_Closing_Debit_Amount() As Double
        Get
            Return (ATBV_Closing_Debit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBV_Closing_Debit_Amount = Value
        End Set
    End Property

    Public Property dATBV_Closing_Credit_Amount() As Double
        Get
            Return (ATBV_Closing_Credit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBV_Closing_Credit_Amount = Value
        End Set
    End Property

    Public Property sATBV_DELFLG() As String
        Get
            Return (ATBV_DELFLG)
        End Get
        Set(ByVal Value As String)
            ATBV_DELFLG = Value
        End Set
    End Property

    Public Property iATBV_CRBY() As Integer
        Get
            Return (ATBV_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ATBV_CRBY = Value
        End Set
    End Property

    Public Property sATBV_STATUS() As String
        Get
            Return (ATBV_STATUS)
        End Get
        Set(ByVal Value As String)
            ATBV_STATUS = Value
        End Set
    End Property

    Public Property iATBV_UPDATEDBY() As Integer
        Get
            Return (ATBV_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ATBV_UPDATEDBY = Value
        End Set
    End Property

    Public Property sATBV_IPAddress() As String
        Get
            Return (ATBV_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATBV_IPAddress = Value
        End Set
    End Property

    Public Property iATBV_CompId() As Integer
        Get
            Return (ATBV_CompId)
        End Get
        Set(ByVal Value As Integer)
            ATBV_CompId = Value
        End Set
    End Property

    Public Property iATBV_YEARId() As Integer
        Get
            Return (ATBV_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ATBV_YEARId = Value
        End Set
    End Property


    Public Property iATBVD_ID() As Integer
        Get
            Return (ATBVD_ID)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_ID = Value
        End Set
    End Property

    Public Property iATBVD_Masid() As Integer
        Get
            Return (ATBVD_Masid)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_Masid = Value
        End Set
    End Property
    Public Property sATBVD_CODE() As String
        Get
            Return (ATBVD_CODE)
        End Get
        Set(ByVal Value As String)
            ATBVD_CODE = Value
        End Set
    End Property
    Public Property sATBVD_Description() As String
        Get
            Return (ATBVD_Description)
        End Get
        Set(ByVal Value As String)
            ATBVD_Description = Value
        End Set
    End Property
    Public Property iATBVD_CustId() As Integer
        Get
            Return (ATBVD_CustId)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_CustId = Value
        End Set
    End Property

    Public Property iATBVD_VersionNo() As Integer
        Get
            Return (ATBVD_VersionNo)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_VersionNo = Value
        End Set
    End Property

    Public Property iATBVD_Branchname() As Integer
        Get
            Return (ATBVD_Branchname)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_Branchname = Value
        End Set
    End Property
    Public Property iATBVD_SChedule_Type() As Integer
        Get
            Return (ATBVD_SChedule_Type)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_SChedule_Type = Value
        End Set
    End Property

    Public Property iATBVD_Company_Type() As Integer
        Get
            Return (ATBVD_Company_Type)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_Company_Type = Value
        End Set
    End Property

    Public Property iATBVD_Headingid() As Integer
        Get
            Return (ATBVD_Headingid)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_Headingid = Value
        End Set
    End Property

    Public Property iATBVD_Subheading() As Integer
        Get
            Return (ATBVD_Subheading)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_Subheading = Value
        End Set
    End Property
    Public Property iATBVD_itemid() As Integer
        Get
            Return (ATBVD_itemid)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_itemid = Value
        End Set
    End Property

    Public Property iATBVD_Subitemid() As Integer
        Get
            Return (ATBVD_Subitemid)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_Subitemid = Value
        End Set
    End Property

    Public Property sATBVD_DELFLG() As String
        Get
            Return (ATBVD_DELFLG)
        End Get
        Set(ByVal Value As String)
            ATBVD_DELFLG = Value
        End Set
    End Property

    Public Property iATBVD_CRBY() As Integer
        Get
            Return (ATBVD_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_CRBY = Value
        End Set
    End Property

    Public Property sATBVD_STATUS() As String
        Get
            Return (ATBVD_STATUS)
        End Get
        Set(ByVal Value As String)
            ATBVD_STATUS = Value
        End Set
    End Property

    Public Property sATBVD_Progress() As String
        Get
            Return (ATBVD_Progress)
        End Get
        Set(ByVal Value As String)
            ATBVD_Progress = Value
        End Set
    End Property

    Public Property iATBVD_UPDATEDBY() As Integer
        Get
            Return (ATBVD_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_UPDATEDBY = Value
        End Set
    End Property

    Public Property sATBVD_IPAddress() As String
        Get
            Return (ATBVD_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATBVD_IPAddress = Value
        End Set
    End Property

    Public Property iATBVD_CompId() As Integer
        Get
            Return (ATBVD_CompId)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_CompId = Value
        End Set
    End Property

    Public Property iATBVD_YEARId() As Integer
        Get
            Return (ATBVD_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_YEARId = Value
        End Set
    End Property
    Public Property iATBVD_iFLAG() As Integer
        Get
            Return (ATBVD_iFLAG)
        End Get
        Set(ByVal Value As Integer)
            ATBVD_iFLAG = Value
        End Set
    End Property

    Public Function SaveTrailBalanceVersion(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsTBVersion As clsTrailBalanceVersion)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.ATBV_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_CODE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBV_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBV_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBV_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_VersionNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBV_VersionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_Opening_Debit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTBVersion.dATBV_Opening_Debit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_Opening_Credit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTBVersion.dATBV_Opening_Credit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_TR_Debit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTBVersion.dATBV_TR_Debit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_TR_Credit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTBVersion.dATBV_TR_Credit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_Closing_Debit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTBVersion.dATBV_Closing_Debit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_Closing_Credit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTBVersion.dATBV_Closing_Credit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBV_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBV_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBV_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBV_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBV_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBV_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_YEARId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBV_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBV_Branchid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBV_Branchname
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_TrailBalance_Version", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveTrailBalanceVersiondetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsTBVersion As clsTrailBalanceVersion)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Masid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_Masid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_CODE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBVD_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBVD_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_VersionNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_VersionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_SChedule_Type", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_SChedule_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Branchid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_Branchname
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Company_Type", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_Company_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Headingid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_Headingid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Subheading", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_Subheading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_itemid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_itemid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Subitemid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_Subitemid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBVD_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBVD_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_Progress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBVD_Progress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsTBVersion.sATBVD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_YEARId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBVD_iFLAG", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTBVersion.iATBVD_iFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_TrailBalance_Version_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAllCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where cust_Compid=" & iACID & " and CUST_DelFlg = 'A' order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadYears(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select YMS_YEARID,substring(YMS_ID,3,2)+ '-' +substring(YMS_ID,8,2) As YMS_ID from YEAR_MASTER where YMS_FROMDATE < DATEADD(year,+1,GETDATE()) and YMS_CompId=" & iACID & " order by YMS_ID desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id as Branchid,Mas_Description as BranchName from SAD_CUST_LOCATION where Mas_CustID=" & iCustId & " and Mas_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTBVersion(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iBranchId As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ATBM_ID,ATBM_VersionNo from Acc_TBVersion_Master where ATBM_CustId=" & iCustID & " and ATBM_Branchid='" & iBranchId & "' and ATBM_Yearid=" & iYearID & " order by ATBM_ID "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustVersionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal IscheduleTypeid As Integer, ByVal Unmapped As Integer, ByVal Ibranchid As Integer, ByVal iVersionId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select  ROW_NUMBER() OVER (ORDER BY ATBV_ID ASC) AS SrNo, b.ATBVD_ID as DescID, ATBV_id as DescDetailsID,ATBV_code as DescriptionCode,ATBV_CustId, ATBV_Description as Description, ATBV_CustId, ATBV_Description as Description, CAST(ATBV_Opening_Debit_Amount AS DECIMAL(19, 2))"
            sSql = sSql & " as OpeningDebit, CAST(ATBV_Opening_Credit_Amount AS DECIMAL(19, 2))  as OpeningCredit,"
            sSql = sSql & " CAST(sum(ATBV_TR_Debit_Amount+ isnull(g.AJTB_Debit,0)) AS DECIMAL(19, 2)) as TrDebit,CAST (sum(ATBV_TR_Credit_Amount+ isnull(h.AJTB_Credit,0)) as DECIMAL(19,2)) as TrCredit,  CAST(ATBV_Closing_TotalDebit_Amount AS DECIMAL(19, 2))  As ClosingDebit,"
            sSql = sSql & " CAST(ATBV_Closing_TotalCredit_Amount AS DECIMAL(19, 2))   As ClosingCredit,"
            sSql = sSql & " ISNULL(b.ATBVD_SubItemId,0) as subItemID, ASSI_Name,ISNULL(b.ATBVd_itemid,0) as itemid,ASI_Name, "
            sSql = sSql & " ISNULL(b.ATBVd_subheading,0) as subheadingid ,ASSH_Name,ISNULL(b.ATBVd_headingid,0) as headingid, "
            sSql = sSql & " ASH_Name,b.ATBVd_progress as Status,b.ATBVd_Company_type as Companytype,"
            sSql = sSql & " ATBVD_SChedule_Type as ScheduleType, CAST(ATBV_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebittrUploaded,CAST (ATBV_TR_Credit_Amount as decimal(19,2)) as TrCredittrUploaded,ATBVD_iFLAG as iFLAG  From Acc_TrailBalance_Version "
            sSql = sSql & " left join Acc_TrailBalance_Version_details b on b.ATBVD_Description = ATBV_Description and b.ATBVD_CustId=" & iCustId & " and b.ATBVD_YEARId=" & iYearID & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and b.ATBVd_Branchnameid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBVD_SChedule_Type=" & IscheduleTypeid & " And b.ATBVD_Headingid=0 "
            Else
            End If
            sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBVD_Headingid"
            sSql = sSql & " left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBVD_Subheading"
            sSql = sSql & " left join ACC_ScheduleItems e on e.ASI_ID=ATBVD_itemid"
            sSql = sSql & " left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBVD_SubItemId"
            sSql = sSql & " left join Acc_JETransactions_Details g on g.AJTB_DescName= ATBV_Description and g.AJTB_Status='A' and g.AJTB_CustId=" & iCustId & " and g.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and g.ajtb_BranchId=" & Ibranchid & " and g.AJTB_Credit =0 "
            End If
            sSql = sSql & " left join Acc_JETransactions_Details h on h.AJTB_DescName= ATBV_Description and h.AJTB_Status='A' and h.AJTB_CustId=" & iCustId & " and h.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and h.ajtb_BranchId=" & Ibranchid & " and h.AJTB_Debit=0 "
            End If
            sSql = sSql & " where ATBV_CustId=" & iCustId & " And ATBV_compid=" & iACID & " And  ATBV_YEARId =" & iYearID & " and ATBV_VersionNo=" & iVersionId & "  and ATBVD_VersionNo=" & iVersionId & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and ATBV_Branchid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBVD_ID Is Not NULL"
            End If
            If Unmapped <> 0 Then
                sSql = sSql & " And ATBVD_Headingid=0 ANd ATBVD_Subheading=0 And ATBVD_itemid=0 And ATBVD_SubItemId=0"
            End If
            sSql = sSql & " group by b.ATBVD_ID , ATBV_id ,ATBV_code ,ATBV_CustId,ATBV_Description , ATBV_CustId, ATBV_Description,"
            sSql = sSql & " ATBV_Opening_Debit_Amount,ATBV_Opening_Credit_Amount,  ATBV_TR_Debit_Amount,ATBV_TR_Credit_Amount "
            sSql = sSql & " ,ATBV_Closing_TotalDebit_Amount,ATBV_Closing_TotalCredit_Amount,ATBVD_SubItemId,ATBVd_itemid,ASI_Name,"
            sSql = sSql & " ATBVd_subheading,ASSH_Name,ATBVd_headingid,ASH_Name,ATBVd_progress, "
            sSql = sSql & " ATBVd_Company_type,ATBVD_SChedule_Type,ATBV_TR_Debit_Amount,ATBV_TR_Credit_Amount,ASSI_Name,ATBVD_iFLAG "
            sSql = sSql & " order by ATBV_ID ;"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
