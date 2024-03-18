Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class clsUploadStockEntry
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions


    Private ACSI_id As Integer
    Private ACSI_ItemdescCode As String
    Private ACSI_Itemdesc As String
    Private ACSI_classification As String
    Private ACSI_Type As String
    Private ACSI_Custid As Double
    Private ACSI_Qty As Double
    Private ACSI_Rate As Double
    Private ACSI_Total As Double
    Private ACSI_DELFLG As String
    Private ACSI_CRBY As Integer
    Private ACSI_STATUS As String
    Private ACSI_UPDATEDBY As Integer
    Private ACSI_IPAddress As String
    Private ACSI_CompId As Integer
    Private ACSI_YEARId As Integer



    Public Property iACSI_id() As Integer
        Get
            Return (ACSI_id)
        End Get
        Set(ByVal Value As Integer)
            ACSI_id = Value
        End Set
    End Property
    Public Property sACSI_ItemdescCode() As String
        Get
            Return (ACSI_ItemdescCode)
        End Get
        Set(ByVal Value As String)
            ACSI_ItemdescCode = Value
        End Set
    End Property
    Public Property sACSI_Itemdesc() As String
        Get
            Return (ACSI_Itemdesc)
        End Get
        Set(ByVal Value As String)
            ACSI_Itemdesc = Value
        End Set
    End Property

    Public Property sACSI_classification() As String
        Get
            Return (ACSI_classification)
        End Get
        Set(ByVal Value As String)
            ACSI_classification = Value
        End Set
    End Property

    Public Property sACSI_Type() As String
        Get
            Return (ACSI_Type)
        End Get
        Set(ByVal Value As String)
            ACSI_Type = Value
        End Set
    End Property
    Public Property iACSI_Custid() As Integer
        Get
            Return (ACSI_Custid)
        End Get
        Set(ByVal Value As Integer)
            ACSI_Custid = Value
        End Set
    End Property


    Public Property iACSI_Qty() As Integer
        Get
            Return (ACSI_Qty)
        End Get
        Set(ByVal Value As Integer)
            ACSI_Qty = Value
        End Set
    End Property
    Public Property dACSI_Rate() As Double
        Get
            Return (ACSI_Rate)
        End Get
        Set(ByVal Value As Double)
            ACSI_Rate = Value
        End Set
    End Property

    Public Property dACSI_Total() As Double
        Get
            Return (ACSI_Total)
        End Get
        Set(ByVal Value As Double)
            ACSI_Total = Value
        End Set
    End Property

    Public Property sACSI_DELFLG() As String
        Get
            Return (ACSI_DELFLG)
        End Get
        Set(ByVal Value As String)
            ACSI_DELFLG = Value
        End Set
    End Property

    Public Property iACSI_CRBY() As Integer
        Get
            Return (ACSI_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ACSI_CRBY = Value
        End Set
    End Property

    Public Property sACSI_STATUS() As String
        Get
            Return (ACSI_STATUS)
        End Get
        Set(ByVal Value As String)
            ACSI_STATUS = Value
        End Set
    End Property

    Public Property iACSI_UPDATEDBY() As Integer
        Get
            Return (ACSI_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ACSI_UPDATEDBY = Value
        End Set
    End Property

    Public Property sACSI_IPAddress() As String
        Get
            Return (ACSI_IPAddress)
        End Get
        Set(ByVal Value As String)
            ACSI_IPAddress = Value
        End Set
    End Property

    Public Property iACSI_CompId() As Integer
        Get
            Return (ACSI_CompId)
        End Get
        Set(ByVal Value As Integer)
            ACSI_CompId = Value
        End Set
    End Property

    Public Property iACSI_YEARId() As Integer
        Get
            Return (ACSI_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ACSI_YEARId = Value
        End Set
    End Property

    Public Function SaveTrailBalanceExcelUpload(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsUSEntry As clsUploadStockEntry)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUSEntry.iACSI_id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_ItemdescCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsUSEntry.ACSI_ItemdescCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_Itemdesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsUSEntry.sACSI_Itemdesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_classification", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsUSEntry.sACSI_classification
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_Type", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsUSEntry.sACSI_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_Custid", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsUSEntry.ACSI_Custid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_Qty", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsUSEntry.iACSI_Qty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_Rate", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUSEntry.dACSI_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_Total", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUSEntry.dACSI_Total
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsUSEntry.sACSI_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUSEntry.iACSI_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsUSEntry.sACSI_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUSEntry.iACSI_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsUSEntry.sACSI_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUSEntry.iACSI_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACSI_YEARId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUSEntry.iACSI_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spACC_Closingstock_Items", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustStockEntryDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim dt, dtJeDet As New DataTable, dtParent As New DataTable, dtMerge As New DataTable
        Try
            sSql = "select  ROW_NUMBER() OVER (ORDER BY ACSI_id ASC) AS SrNo, ACSI_id as DescID,ACSI_Itemdesc as Description,ACSI_classification as Itemclassification,"
            sSql = sSql & "ACSI_Qty as Quantity,ACSI_Type as UOM,ACSI_Rate as UP,ACSI_Total as Amount from ACC_Closingstock_Items"
            sSql = sSql & " where ACSI_Custid=" & iCustId & " And ACSI_CompId=" & iACID & " And  ACSI_YEARId=" & iYearID & ""
            sSql = sSql & " order by ACSI_id"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustStockEntryTotal(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim lblbTotal As New Double
        Try
            sSql = "select  isnull(SUm(ACSI_Total),0) as ACSI_Total  from ACC_Closingstock_Items"
            sSql = sSql & " where ACSI_Custid=" & iCustId & " And ACSI_CompId=" & iACID & " And  ACSI_YEARId=" & iYearID & ""
            lblbTotal = objDBL.SQLExecuteScalar(sAC, sSql)
            Return lblbTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
