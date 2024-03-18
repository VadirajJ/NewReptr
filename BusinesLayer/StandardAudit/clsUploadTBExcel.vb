Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class clsUploadTBExcel
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    'UploadTBExcel
    Private AEU_ID As Integer
    Private AEU_Description As String
    Private AEU_CustId As Integer
    Private AEU_ODAmount As Double
    Private AEU_OCAmount As Double
    Private AEU_TRDAmount As Double
    Private AEU_TRCAmount As Double
    Private AEU_CDAmount As Double
    Private AEU_CCAmount As Double
    Private AEU_DELFLG As String
    Private AEU_CRBY As Integer
    Private AEU_STATUS As String
    Private AEU_UPDATEDBY As Integer
    Private AEU_IPAddress As String
    Private AEU_CompId As Integer
    Private AEU_YEARId As Integer
    Private AEU_AuditId As Integer
    Private AEU_AuditTypeId As Integer
    Public Property iAEU_ID() As Integer
        Get
            Return (AEU_ID)
        End Get
        Set(ByVal Value As Integer)
            AEU_ID = Value
        End Set
    End Property

    Public Property sAEU_Description() As String
        Get
            Return (AEU_Description)
        End Get
        Set(ByVal Value As String)
            AEU_Description = Value
        End Set
    End Property
    Public Property iAEU_CustId() As Integer
        Get
            Return (AEU_CustId)
        End Get
        Set(ByVal Value As Integer)
            AEU_CustId = Value
        End Set
    End Property

    Public Property dAEU_ODAmount() As Double
        Get
            Return (AEU_ODAmount)
        End Get
        Set(ByVal Value As Double)
            AEU_ODAmount = Value
        End Set
    End Property

    Public Property dAEU_OCAmount() As Double
        Get
            Return (AEU_OCAmount)
        End Get
        Set(ByVal Value As Double)
            AEU_OCAmount = Value
        End Set
    End Property
    Public Property dAEU_TRDAmount() As Double
        Get
            Return (AEU_TRDAmount)
        End Get
        Set(ByVal Value As Double)
            AEU_TRDAmount = Value
        End Set
    End Property
    Public Property dAEU_TRCAmount() As Double
        Get
            Return (AEU_TRCAmount)
        End Get
        Set(ByVal Value As Double)
            AEU_TRCAmount = Value
        End Set
    End Property
    Public Property dAEU_CDAmount() As Double
        Get
            Return (AEU_CDAmount)
        End Get
        Set(ByVal Value As Double)
            AEU_CDAmount = Value
        End Set
    End Property

    Public Property dAEU_CCAmount() As Double
        Get
            Return (AEU_CCAmount)
        End Get
        Set(ByVal Value As Double)
            AEU_CCAmount = Value
        End Set
    End Property

    Public Property sAEU_DELFLG() As String
        Get
            Return (AEU_DELFLG)
        End Get
        Set(ByVal Value As String)
            AEU_DELFLG = Value
        End Set
    End Property

    Public Property iAEU_CRBY() As Integer
        Get
            Return (AEU_CRBY)
        End Get
        Set(ByVal Value As Integer)
            AEU_CRBY = Value
        End Set
    End Property

    Public Property sAEU_STATUS() As String
        Get
            Return (AEU_STATUS)
        End Get
        Set(ByVal Value As String)
            AEU_STATUS = Value
        End Set
    End Property

    Public Property iAEU_UPDATEDBY() As Integer
        Get
            Return (AEU_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            AEU_UPDATEDBY = Value
        End Set
    End Property

    Public Property sAEU_IPAddress() As String
        Get
            Return (AEU_IPAddress)
        End Get
        Set(ByVal Value As String)
            AEU_IPAddress = Value
        End Set
    End Property

    Public Property iAEU_CompId() As Integer
        Get
            Return (AEU_CompId)
        End Get
        Set(ByVal Value As Integer)
            AEU_CompId = Value
        End Set
    End Property

    Public Property iAEU_YEARId() As Integer
        Get
            Return (AEU_YEARId)
        End Get
        Set(ByVal Value As Integer)
            AEU_YEARId = Value
        End Set
    End Property

    Public Property iAEU_AuditId() As Integer
        Get
            Return (AEU_AuditId)
        End Get
        Set(ByVal Value As Integer)
            AEU_AuditId = Value
        End Set
    End Property

    Public Property iAEU_AuditTypeId() As Integer
        Get
            Return (AEU_AuditTypeId)
        End Get
        Set(ByVal Value As Integer)
            AEU_AuditTypeId = Value
        End Set
    End Property
    Public Function SaveUploadTBExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsUTBE As clsUploadTBExcel)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.AEU_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.iAEU_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsUTBE.sAEU_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_ODAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUTBE.dAEU_ODAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_OCAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUTBE.dAEU_OCAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_TRDAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUTBE.dAEU_TRDAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_TRCAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUTBE.dAEU_TRCAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_CDAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUTBE.dAEU_CDAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_CCAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsUTBE.dAEU_CCAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsUTBE.sAEU_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.iAEU_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsUTBE.sAEU_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.iAEU_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsUTBE.sAEU_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.iAEU_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_YEARId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.iAEU_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_AuditId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.iAEU_AuditId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AEU_AuditTypeId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsUTBE.iAEU_AuditTypeId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_Excel_Upload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCustRLSelectedYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iAuditId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ROW_NUMBER() OVER (ORDER BY AEU_ID ASC) AS SrNo, AEU_ID as DescID,AEU_CustId, AEU_Description as Description, "
            sSql = sSql & " CAST(AEU_ODAmount AS DECIMAL(19, 2)) as OpeningDebit, CAST(AEU_OCAmount AS DECIMAL(19, 2)) as OpeningCredit,"
            sSql = sSql & " CAST(AEU_TRDAmount AS DECIMAL(19, 2)) as TrDebit,CAST(AEU_TRCAmount AS DECIMAL(19, 2)) as TrCredit, "
            sSql = sSql & " CAST(AEU_CDAmount AS DECIMAL(19, 2)) As ClosingDebit,CAST(AEU_CCAmount AS DECIMAL(19, 2)) As ClosingCredit from Audit_Excel_Upload"
            sSql = sSql & " where AEU_CustId=" & iCustId & " And AEU_CompID=" & iACID & " And AEU_YearID=" & iYearID & " And AEU_AuditId=" & iAuditId & ""
            sSql = sSql & " order by AEU_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCustTBtoRLSelectedYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iAuditId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ROW_NUMBER() OVER (ORDER BY ATBU_Description ASC) AS SrNo, 0 as DescID,ATBU_CustId, ATBU_Description as Description, "
            sSql = sSql & " CAST(ATBU_Opening_Debit_Amount AS DECIMAL(19, 2)) as OpeningDebit, CAST(ATBU_Opening_Credit_Amount AS DECIMAL(19, 2)) as OpeningCredit,"
            sSql = sSql & " CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebit, CAST(ATBU_TR_Credit_Amount AS DECIMAL(19, 2)) as TrCredit,"
            sSql = sSql & " CAST(ATBU_Closing_Debit_Amount AS DECIMAL(19, 2)) As ClosingDebit, CAST(ATBU_Closing_Credit_Amount AS DECIMAL(19, 2)) As ClosingCredit from Acc_TrailBalance_Upload"
            sSql = sSql & " where ATBU_CustId=" & iCustId & " And ATBU_CompId=" & iACID & " And ATBU_YEARId =" & iYearID & ""
            sSql = sSql & " order by ATBU_Description"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCustTBSelectedYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iAuditId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select ROW_NUMBER() OVER (ORDER BY AEU_ID ASC) AS SrNo, AEU_ID as DescID,AEU_CustId, AEU_Description as Description, CAST(AEU_ODAmount AS DECIMAL(19, 2))"
            sSql = sSql & " as OpeningDebit, CAST(AEU_OCAmount AS DECIMAL(19, 2)) as OpeningCredit,"
            sSql = sSql & " CAST(AEU_TRDAmount AS DECIMAL(19, 2)) as TrDebit,AEU_TRCAmount as TrCredit, CAST(AEU_CDAmount AS DECIMAL(19, 2)) As ClosingDebit,"
            sSql = sSql & " CAST(AEU_CCAmount AS DECIMAL(19, 2)) As ClosingCredit from Audit_Excel_Upload"
            sSql = sSql & " where AEU_CustId=" & iCustId & " And AEU_compid=" & iACID & " And AEU_YEARId =" & iYearID & ""
            sSql = sSql & " order by AEU_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteCustRecord(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer, ByVal iAuditId As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From Audit_Excel_Upload Where AEU_CustId=" & iCustID & " And AEU_CompID=" & iACID & " And AEU_YearID=" & iYearID & " And AEU_AuditId=" & iAuditId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function getCustYearCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer, ByVal iAuditId As Integer) As Integer
        Dim sSql As String
        Dim iCount As Integer = 0
        Try
            sSql = "select count(*) from Audit_Excel_Upload Where AEU_CustId=" & iCustID & " And AEU_CompID=" & iACID & " And AEU_YearID=" & iYearID & " And AEU_AuditId=" & iAuditId & ""
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getApproveCustYearCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer, ByVal iAuditId As Integer) As Integer
        Dim sSql As String
        Dim iCount As Integer = 0
        Try
            sSql = "select count(*) from Audit_Excel_Upload Where AEU_Status='A' and AEU_CustId=" & iCustID & " And AEU_CompID=" & iACID & " And AEU_YearID=" & iYearID & " And AEU_AuditId=" & iAuditId & ""
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveCustomerStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iAuditId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_Excel_Upload set AEU_Status='A' where AEU_CustId=" & iCustId & " and AEU_Compid=" & iACID & " and AEU_YearID=" & iYearID & " And AEU_AuditId=" & iAuditId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
