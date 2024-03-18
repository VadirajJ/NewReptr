Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports DatabaseLayer
Imports System.Configuration
Imports System.Xml

Public Structure strServerDB
    Private MDA_ID As Integer
    Private MDA_DatabaseName As String
    Private MDA_AccessCode As String
    Private MDA_CompanyName As String
    Private MDA_CreatedDate As DateTime
    Private MDA_IPAddress As String
    Private MDA_Application As Integer
    Public Property iMDA_ID() As Integer
        Get
            Return (MDA_ID)
        End Get
        Set(ByVal Value As Integer)
            MDA_ID = Value
        End Set
    End Property
    Public Property sMDA_DatabaseName() As String
        Get
            Return (MDA_DatabaseName)
        End Get
        Set(ByVal Value As String)
            MDA_DatabaseName = Value
        End Set
    End Property
    Public Property sMDA_AccessCode() As String
        Get
            Return (MDA_AccessCode)
        End Get
        Set(ByVal Value As String)
            MDA_AccessCode = Value
        End Set
    End Property
    Public Property sMDA_CompanyName() As String
        Get
            Return (MDA_CompanyName)
        End Get
        Set(ByVal Value As String)
            MDA_CompanyName = Value
        End Set
    End Property
    Public Property dMDA_CreatedDate() As DateTime
        Get
            Return (MDA_CreatedDate)
        End Get
        Set(ByVal Value As DateTime)
            MDA_CreatedDate = Value
        End Set
    End Property
    Public Property sMDA_IPAddress() As String
        Get
            Return (MDA_IPAddress)
        End Get
        Set(ByVal Value As String)
            MDA_IPAddress = Value
        End Set
    End Property

    Public Property iMDA_Application() As Integer
        Get
            Return (MDA_Application)
        End Get
        Set(ByVal Value As Integer)
            MDA_Application = Value
        End Set
    End Property
End Structure
Public Class clsServerDB
    Dim objDBL As New DBHelper
    Dim ConStr As String
    Dim MyRDBMS As String
    Public Function CheckDBExists(ByVal ConnString As String, ByVal databasename As String, ByVal exc As Exception) As Boolean
        Dim conn As SqlConnection = Nothing
        Dim v As Object = Nothing
        Try
            conn = New SqlConnection(ConnString)
            Dim c As String = "SELECT COUNT (*) FROM sys.sysdatabases where name='" & databasename & "'"
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand(c, conn)
            v = cmd.ExecuteScalar()
        Catch ex As Exception
            exc = ex
            Return False
        Finally
            conn.Close()
        End Try
        Return CBool(v)
    End Function
    Public Sub createDatabase(ByVal sServerName As String, ByVal suID As String, ByVal sPassword As String, ByVal sDatabase As String)
        Try
            Dim ConnString As String = "Data Source=" & sServerName & ";Persist Security Info=True;User ID=" & suID & ";Password=" & sPassword & ""
            Dim conn As SqlConnection = New SqlConnection(ConnString)
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand("Create Database " & sDatabase & "", conn)
            cmd.ExecuteScalar()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub createTables(ByVal sServerName As String, ByVal suID As String, ByVal sPassword As String, ByVal sDatabase As String, ByVal sPath As String)
        Dim oRead As System.IO.StreamReader
        Dim LineIn As String
        Try
            Dim connString As String = ("Provider = SQLOLEDB.1;Server=" & sServerName & ";uid=" & suID & ";pwd=" & sPassword & ";DataBase=" & sDatabase & "")
            Dim ObjDb As New DBExport(connString, "SQL")
            oRead = File.OpenText(sPath)
            LineIn = oRead.ReadToEnd()
            ObjDb.DBExecuteNoNQuery(LineIn)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub InsertTraceCompanyDetailsByVal(ByVal sServerName As String, ByVal suID As String, ByVal sPassword As String, ByVal sDatabase As String)
    '    Dim sSql As String
    '    Try
    '        sSql = "Select * from Trace_CompanyDetails Where Company_CompID=" & iACID & " And Company_ID=" & iCompanyID & ""
    '        objDBL.SQLExecuteNonQuery(sDatabase, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub createTablesForSP(ByVal sServerName As String, ByVal suID As String, ByVal sPassword As String, ByVal sDatabase As String, ByVal sPath As String)
        Dim oRead As System.IO.StreamReader
        Dim LineIn As String
        Try
            Dim connString As String = ("Provider = SQLOLEDB.1;Server=" & sServerName & ";uid=" & suID & ";pwd=" & sPassword & ";DataBase=" & sDatabase & "")
            Dim ObjDb As New DBExport(connString, "SQL")
            oRead = File.OpenText(sPath)
            LineIn = oRead.ReadToEnd()
            ObjDb.DBExecuteNoNQuerySP(LineIn)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function CreatConnection(ByVal sServerName As String, ByVal suID As String, ByVal sPassword As String, ByVal sDatabase As String)
        Try
            Dim connString As String = ("Provider=SQLOLEDB.1;Data Source=" & sServerName & ";User ID=" & suID & ";pwd=" & sPassword & ";Initial Catalog=" & sDatabase & ";TRUSTED_CONNECTION=NO")
            Return connString
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Sub SaveCode(ByVal sAccesscode As String, ByVal objstrDBAccess As strServerDB)
    '    Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(8) {}
    '    Dim iParamCount As Integer
    '    Dim Arr(1) As String
    '    Try
    '        iParamCount = 0
    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@MDA_ID", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Value = objstrDBAccess.iMDA_ID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@MDA_DatabaseName", OleDb.OleDbType.VarChar)
    '        ObjParam(iParamCount).Value = objstrDBAccess.sMDA_DatabaseName
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@MDA_AccessCode", OleDb.OleDbType.VarChar)
    '        ObjParam(iParamCount).Value = objstrDBAccess.sMDA_AccessCode
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@MDA_CompanyName", OleDb.OleDbType.VarChar)
    '        ObjParam(iParamCount).Value = objstrDBAccess.sMDA_CompanyName
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@MDA_CreatedDate", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objstrDBAccess.dMDA_CreatedDate
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@MDA_IPAddress", OleDb.OleDbType.VarChar)
    '        ObjParam(iParamCount).Value = objstrDBAccess.sMDA_IPAddress
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@MDA_Application", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Value = objstrDBAccess.iMDA_Application
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        Arr(0) = "@iUpdateOrSave"
    '        Arr(1) = "@iOper"

    '        Arr = objDBL.ExecuteSPForInsertARR(sAccesscode, "spMMCSPL_DB_Access", 1, Arr, ObjParam)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    'Public Sub CreateAccessCode(ByVal App As Integer, ByVal sAccessCode As String, ByVal sCompanyName As String, ByVal sConnectionString As String)
    '    Dim sSql As String = "", sSql2 As String = "", sSql4 As String = ""
    '    Try
    '        sSql = "Insert Into MST_Customer_Master(CUST_ID, CUST_NAME, CUST_CODE, CUST_DELFLG,Cust_compID)values"
    '        sSql = sSql & "('1','" & sCompanyName & "','" & sAccessCode & "','X','1')"
    '        objDBL.SQLExecuteNonQueryWithoutAccessKey(sConnectionString, sSql)

    '        sSql2 = "Insert Into Sad_CompanyMaster_Settings(SAD_CMS_ID,SAD_CMS_AccessCode,SAD_CMS_NAME,SAD_CMS_Flag)values"
    '        sSql2 = sSql2 & "('1',	'" & sAccessCode & "',	'" & sCompanyName & "',	'A')"
    '        objDBL.SQLExecuteNonQueryWithoutAccessKey(sConnectionString, sSql2)

    '        sSql4 = "Insert Into sad_org_structure(org_node,org_Code,org_name,org_parent,org_userid,org_Type,org_DelFlag,"
    '        sSql4 = sSql4 & " org_Note,org_AppStrength,org_AppBy,org_AppOn,org_CreatedBy,org_CreatedOn,org_Status,Org_levelCode,"
    '        sSql4 = sSql4 & " org_cust,Org_Cust1,Org_CompID,Org_UpdatedBy,Org_UpdatedOn,org_DeletedBy,org_DeletedOn,org_RecalledBy,"
    '        sSql4 = sSql4 & " org_RecalledOn,Org_IPAddress,Org_SalesUnitCode,Org_BranchCode)values"
    '        sSql4 = sSql4 & "('1',	'" & sAccessCode & "',	'" & sCompanyName & "',	'0',	'0',	'R',	'A',	'" & sCompanyName & "',	'20',	'1',	GetDate(),	'1',	GetDate(),	'A',	'0',	'N',"
    '        sSql4 = sSql4 & " 'N',	'1',	'0',	GetDate(),	'0',	GetDate(),	'0',	GetDate(),	NULL,	NULL,	NULL)"
    '        objDBL.SQLExecuteNonQueryWithoutAccessKey(sConnectionString, sSql4)

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
End Class
