Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class ClsFinancialAddAssign
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Private FAA_ID As Integer
    Private FAA_AccHead As Integer
    Private FAA_Head As Integer
    Private FAA_GL As Integer
    Private FAA_Parent As Integer
    Private FAA_GLCode As String
    Private FAA_GLDesc As String
    Private FAA_SGLDesc As String
    Private FAA_CustID As Integer
    Private FAA_IndType As Integer
    Private FAA_OBDebit As Double
    Private FAA_OBCredit As Double
    Private FAA_TrDebit As Double
    Private FAA_TrCredit As Double
    Private FAA_CloseDebit As Double
    Private FAA_CloseCredit As Double
    Private FAA_Comments As String
    Private FAA_Nameoftheperson As String
    Private FAA_YearID As Integer
    Private FAA_CompID As Integer
    Private FAA_Createdby As Integer
    Private FAA_CreatedOn As DateTime
    Private FAA_Operation As String
    Private FAA_IPAddress As String

    Public Property iFAA_ID() As Integer
        Get
            Return (FAA_ID)
        End Get
        Set(ByVal Value As Integer)
            FAA_ID = Value
        End Set
    End Property

    Public Property iFAA_AccHead() As Integer
        Get
            Return (FAA_AccHead)
        End Get
        Set(ByVal Value As Integer)
            FAA_AccHead = Value
        End Set
    End Property
    Public Property iFAA_Head() As Integer
        Get
            Return (FAA_Head)
        End Get
        Set(ByVal Value As Integer)
            FAA_Head = Value
        End Set
    End Property
    Public Property iFAA_GL() As Integer
        Get
            Return (FAA_GL)
        End Get
        Set(ByVal Value As Integer)
            FAA_GL = Value
        End Set
    End Property
    Public Property iFAA_Parent() As Integer
        Get
            Return (FAA_Parent)
        End Get
        Set(ByVal Value As Integer)
            FAA_Parent = Value
        End Set
    End Property
    Public Property sFAA_GLCode() As String
        Get
            Return (FAA_GLCode)
        End Get
        Set(ByVal Value As String)
            FAA_GLCode = Value
        End Set
    End Property
    Public Property sFAA_GLDesc() As String
        Get
            Return (FAA_GLDesc)
        End Get
        Set(ByVal Value As String)
            FAA_GLDesc = Value
        End Set
    End Property
    Public Property sFAA_SGLDesc() As String
        Get
            Return (FAA_SGLDesc)
        End Get
        Set(ByVal Value As String)
            FAA_SGLDesc = Value
        End Set
    End Property

    Public Property iFAA_CustID() As Integer
        Get
            Return (FAA_CustID)
        End Get
        Set(ByVal Value As Integer)
            FAA_CustID = Value
        End Set
    End Property
    Public Property iFAA_IndType() As Integer
        Get
            Return (FAA_IndType)
        End Get
        Set(ByVal Value As Integer)
            FAA_IndType = Value
        End Set
    End Property
    Public Property dFAA_OBDebit() As Double
        Get
            Return (FAA_OBDebit)
        End Get
        Set(ByVal Value As Double)
            FAA_OBDebit = Value
        End Set
    End Property
    Public Property dFAA_OBCredit() As Double
        Get
            Return (FAA_OBCredit)
        End Get
        Set(ByVal Value As Double)
            FAA_OBCredit = Value
        End Set
    End Property
    Public Property dFAA_TrDebit() As Double
        Get
            Return (FAA_TrDebit)
        End Get
        Set(ByVal Value As Double)
            FAA_TrDebit = Value
        End Set
    End Property
    Public Property dFAA_TrCredit() As Double
        Get
            Return (FAA_TrCredit)
        End Get
        Set(ByVal Value As Double)
            FAA_TrCredit = Value
        End Set
    End Property
    Public Property dFAA_CloseDebit() As Double
        Get
            Return (FAA_CloseDebit)
        End Get
        Set(ByVal Value As Double)
            FAA_CloseDebit = Value
        End Set
    End Property
    Public Property dFAA_CloseCredit() As Double
        Get
            Return (FAA_CloseCredit)
        End Get
        Set(ByVal Value As Double)
            FAA_CloseCredit = Value
        End Set
    End Property
    Public Property sFAA_Comments() As String
        Get
            Return (FAA_Comments)
        End Get
        Set(ByVal Value As String)
            FAA_Comments = Value
        End Set
    End Property
    Public Property sFAA_Nameoftheperson() As String
        Get
            Return (FAA_Nameoftheperson)
        End Get
        Set(ByVal Value As String)
            FAA_Nameoftheperson = Value
        End Set
    End Property
    Public Property iFAA_YearID() As Integer
        Get
            Return (FAA_YearID)
        End Get
        Set(ByVal Value As Integer)
            FAA_YearID = Value
        End Set
    End Property
    Public Property iFAA_CompID() As Integer
        Get
            Return (FAA_CompID)
        End Get
        Set(ByVal Value As Integer)
            FAA_CompID = Value
        End Set
    End Property
    Public Property iFAA_Createdby() As Integer
        Get
            Return (FAA_Createdby)
        End Get
        Set(ByVal Value As Integer)
            FAA_Createdby = Value
        End Set
    End Property
    Public Property dFAA_CreatedOn() As Date
        Get
            Return (FAA_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            FAA_CreatedOn = Value
        End Set
    End Property

    Public Property sFAA_Operation() As String
        Get
            Return (FAA_Operation)
        End Get
        Set(ByVal Value As String)
            FAA_Operation = Value
        End Set
    End Property
    Public Property sFAA_IPAddress() As String
        Get
            Return (FAA_IPAddress)
        End Get
        Set(ByVal Value As String)
            FAA_IPAddress = Value
        End Set
    End Property
    Public Function GetCustCOADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String, sSqlParent As String
        Dim dt As New DataTable, dtParent As New DataTable, dtMerge As New DataTable
        Try
            ' sSql = "select CC_GL from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " order by cc_gl"
            sSql = "select distinct(a.cc_gl) as slno,b.gl_desc as GLDesc,a.CC_AccHead as AccHead,a.CC_Parent as Parent,a.CC_GLCode as GLCode,a.CC_GLDesc as SGLDescription,a.CC_OBDebit as OpeningDebit,a.CC_OBCredit as OpeningCredit,a.CC_TrDebit as TrDebit,"
            sSql = sSql & "a.CC_TrCredit as TrCredit,a.CC_CloseDebit as ClosingDebit,a.CC_CloseCredit as ClosingCredit,"
            sSql = sSql & "a.cc_parent as GL,'' as GLTotal,b.gl_Parent as SubGroup,'' as  SubGroupTotal,"
            sSql = sSql & "d.gl_desc as Group1,'' as  GroupTotal,'' as HeadTotal,"
            sSql = sSql & " Case  When c.gl_accHead=1 then 'Assets' "
            sSql = sSql & " When c.gl_accHead=2 then 'Income' "
            sSql = sSql & " When c.gl_accHead=3 then 'Expenditure' "
            sSql = sSql & " When c.gl_accHead=4 then 'Liabilities' "
            sSql = sSql & " END AS Head,CC_Status  from Customer_COA  a "


            sSql = sSql & "join Chart_Of_Accounts b on a.cc_parent=b.gl_id "
            sSql = sSql & "join Chart_Of_Accounts c on b.gl_parent=c.gl_id "
            sSql = sSql & "join Chart_Of_Accounts d on c.gl_parent=d.gl_id "

            sSql = sSql & "where a.cc_custid=" & iCustId & " and a.CC_Status='A' and a.cc_compid=" & iACID & " order by cc_gl"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            ' Return dt

            sSqlParent = "select distinct(a.cc_gl) as slno,a.CC_GLDesc as SGLDescription,a.CC_OBDebit as OpeningDebit,a.CC_AccHead as AccHead,a.CC_Parent as Parent,a.CC_GLCode as GLCode,a.CC_OBCredit as OpeningCredit,"
            sSqlParent = sSqlParent & "a.CC_TrDebit as TrDebit, a.CC_TrCredit As TrCredit, a.CC_CloseDebit As ClosingDebit, a.CC_CloseCredit As ClosingCredit, a.cc_parent As GL,"
            sSqlParent = sSqlParent & "'' as GLTotal,0 as SubGroup,'' as  SubGroupTotal,'' as Group1,'' as  GroupTotal,'' as HeadTotal,'' as  Head,CC_Status"
            sSqlParent = sSqlParent & " From Customer_COA  a Where a.cc_custid = " & iCustId & " And a.cc_compid = " & iACID & " and a.cc_parent=0 and a.CC_Status='A' Order By CC_GL"
            dtParent = objDBL.SQLExecuteDataTable(sAC, sSqlParent)
            'Return dtParent
            dt.Merge(dtParent, True, MissingSchemaAction.Ignore)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerCOA(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim iCount As Integer
        Try
            sSql = "select Count(*) from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & ""
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllEmployee(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Usr_ID,Usr_FullName from Sad_UserDetails Where Usr_CompID=" & iACID & " and Usr_DelFlag = 'A' order by Usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRecord(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Financial_AddAssign Where FAA_CustID=" & iCustID & " And FAA_CompID=" & iACID & " And FAA_YearID=" & iYearID & " "
            CheckRecord = objDBL.DBCheckForRecord(sAC, sSql)
            Return CheckRecord
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAddAssign(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objFinancialAddAssign As ClsFinancialAddAssign)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_AccHead", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_AccHead
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_Parent", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_Parent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_GLCode", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFinancialAddAssign.sFAA_GLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_GLDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFinancialAddAssign.sFAA_GLDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_SGLDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFinancialAddAssign.sFAA_SGLDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_IndType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_OBDebit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objFinancialAddAssign.dFAA_OBDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_OBCredit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objFinancialAddAssign.dFAA_OBCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_TrDebit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objFinancialAddAssign.dFAA_TrDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_TrCredit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objFinancialAddAssign.dFAA_TrCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_CloseDebit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objFinancialAddAssign.dFAA_CloseDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_CloseCredit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objFinancialAddAssign.dFAA_CloseCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_Comments", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFinancialAddAssign.sFAA_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_Nameoftheperson", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFinancialAddAssign.sFAA_Nameoftheperson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_Createdby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFinancialAddAssign.iFAA_Createdby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFinancialAddAssign.dFAA_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_Operation", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFinancialAddAssign.sFAA_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAA_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFinancialAddAssign.sFAA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spFinancial_AddAssign", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select FAA_ID as ID, FAA_GLDesc as GLDesc,FAA_SGLDesc as SGLDescription,FAA_OBDebit as OpeningDebit,FAA_OBCredit as OpeningCredit,FAA_TrDebit as TrDebit,FAA_TrCredit as TrCredit,FAA_CloseDebit as ClosingDebit,FAA_CloseCredit as ClosingCredit,FAA_Comments as Comments,FAA_Nameoftheperson as Nameoftheperson,FAA_AccHead as AccHead,FAA_Parent as Parent,FAA_GLCode as GLCode  from Financial_AddAssign Where FAA_CustID = " & iCustid & " and FAA_CompID=" & iACID & " and FAA_YearID=" & iYearID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerFAA(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim iCount As Integer
        Try
            sSql = "select Count(*) from Financial_AddAssign where FAA_CustID=" & iCustId & " and FAA_CompID=" & iACID & " and FAA_YearID=" & iYearID & ""
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
