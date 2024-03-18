Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer

Public Class ClsUploadTailBal
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions


    Private CC_ID As Integer
    Private CC_AccHead As Integer
    Private CC_Head As Integer
    Private CC_GLCode As String
    Private CC_GLDesc As String
    Private CC_OBDebit As Double
    Private CC_OBCredit As Double
    Private CC_YearId As Integer
    Private CC_Status As String
    Private CC_CompId As Integer
    Private CC_GL As Integer
    Private CC_Operation As String
    Private CC_IPAddress As String
    Private CC_CustID As Integer
    Private CC_IndType As Integer
    Private CC_TrDebit As Double
    Private CC_TrCredit As Double
    Private CC_CreatedBy As Integer
    Private CC_CreatedOn As Date
    Private CC_CloseDebit As Double
    Private CC_CloseCredit As Double
    Private CC_Parent As Integer


    'TrialbalamceexcelUpload by karthik
    Private ATBU_ID As Integer
    Private ATBU_CODE As String
    Private ATBU_Description As String
    Private ATBU_CustId As Integer
    Private ATBU_Branchname As Integer
    Private ATBU_Opening_Debit_Amount As Double
    Private ATBU_Opening_Credit_Amount As Double
    Private ATBU_TR_Debit_Amount As Double
    Private ATBU_TR_Credit_Amount As Double
    Private ATBU_Closing_Debit_Amount As Double
    Private ATBU_Closing_Credit_Amount As Double
    Private ATBU_DELFLG As String
    Private ATBU_CRBY As Integer
    Private ATBU_STATUS As String
    Private ATBU_UPDATEDBY As Integer
    Private ATBU_IPAddress As String
    Private ATBU_CompId As Integer
    Private ATBU_YEARId As Integer

    'TrialbalamceexcelUpload details by karthik
    Private ATBUD_ID As Integer
    Private ATBUD_Masid As Integer
    Private ATBUD_CODE As String
    Private ATBUD_Description As String
    Private ATBUD_CustId As Integer
    Private ATBUD_SChedule_Type As Integer
    Private ATBUD_Branchname As Integer
    Private ATBUD_Company_Type As Integer
    Private ATBUD_Headingid As Integer
    Private ATBUD_Subheading As Integer
    Private ATBUD_itemid As Integer
    Private ATBUD_Subitemid As Integer
    Private ATBUD_DELFLG As String
    Private ATBUD_CRBY As Integer
    Private ATBUD_STATUS As String
    Private ATBUD_Progress As String
    Private ATBUD_UPDATEDBY As Integer
    Private ATBUD_IPAddress As String
    Private ATBUD_CompId As Integer
    Private ATBUD_YEARId As Integer


    Public Property iCC_Parent() As Integer
        Get
            Return (CC_Parent)
        End Get
        Set(ByVal Value As Integer)
            CC_Parent = Value
        End Set
    End Property

    Public Property iCC_CreatedBy() As Integer
        Get
            Return (CC_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            CC_CreatedBy = Value
        End Set
    End Property
    Public Property dCC_CreatedOn() As Date
        Get
            Return (CC_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            CC_CreatedOn = Value
        End Set
    End Property
    Public Property iCC_ID() As Integer
        Get
            Return (CC_ID)
        End Get
        Set(ByVal Value As Integer)
            CC_ID = Value
        End Set
    End Property
    Public Property sCC_Operation() As String
        Get
            Return (CC_Operation)
        End Get
        Set(ByVal Value As String)
            CC_Operation = Value
        End Set
    End Property
    Public Property sCC_IPAddress() As String
        Get
            Return (CC_IPAddress)
        End Get
        Set(ByVal Value As String)
            CC_IPAddress = Value
        End Set
    End Property
    Public Property iCC_GL() As Integer
        Get
            Return (CC_GL)
        End Get
        Set(ByVal Value As Integer)
            CC_GL = Value
        End Set
    End Property
    Public Property iCC_CompId() As Integer
        Get
            Return (CC_CompId)
        End Get
        Set(ByVal Value As Integer)
            CC_CompId = Value
        End Set
    End Property
    Public Property sCC_Status() As String
        Get
            Return (CC_Status)
        End Get
        Set(ByVal Value As String)
            CC_Status = Value
        End Set
    End Property
    Public Property iCC_YearId() As Integer
        Get
            Return (CC_YearId)
        End Get
        Set(ByVal Value As Integer)
            CC_YearId = Value
        End Set
    End Property
    Public Property dCC_OBDebit() As Double
        Get
            Return (CC_OBDebit)
        End Get
        Set(ByVal Value As Double)
            CC_OBDebit = Value
        End Set
    End Property
    Public Property dCC_OBCredit() As Double
        Get
            Return (CC_OBCredit)
        End Get
        Set(ByVal Value As Double)
            CC_OBCredit = Value
        End Set
    End Property
    Public Property sCC_GLCode() As String
        Get
            Return (CC_GLCode)
        End Get
        Set(ByVal Value As String)
            CC_GLCode = Value
        End Set
    End Property
    Public Property sCC_GLDesc() As String
        Get
            Return (CC_GLDesc)
        End Get
        Set(ByVal Value As String)
            CC_GLDesc = Value
        End Set
    End Property
    Public Property iCC_AccHead() As Integer
        Get
            Return (CC_AccHead)
        End Get
        Set(ByVal Value As Integer)
            CC_AccHead = Value
        End Set
    End Property
    Public Property iCC_Head() As Integer
        Get
            Return (CC_Head)
        End Get
        Set(ByVal Value As Integer)
            CC_Head = Value
        End Set
    End Property
    Public Property iCC_CustID() As Integer
        Get
            Return (CC_CustID)
        End Get
        Set(ByVal Value As Integer)
            CC_CustID = Value
        End Set
    End Property
    Public Property iCC_IndType() As Integer
        Get
            Return (CC_IndType)
        End Get
        Set(ByVal Value As Integer)
            CC_IndType = Value
        End Set
    End Property
    Public Property dCC_TrDebit() As Double
        Get
            Return (CC_TrDebit)
        End Get
        Set(ByVal Value As Double)
            CC_TrDebit = Value
        End Set
    End Property
    Public Property dCC_TrCredit() As Double
        Get
            Return (CC_TrCredit)
        End Get
        Set(ByVal Value As Double)
            CC_TrCredit = Value
        End Set
    End Property

    Public Property dCC_CloseDebit() As Double
        Get
            Return (CC_CloseDebit)
        End Get
        Set(ByVal Value As Double)
            CC_CloseDebit = Value
        End Set
    End Property
    Public Property dCC_CloseCredit() As Double
        Get
            Return (CC_CloseCredit)
        End Get
        Set(ByVal Value As Double)
            CC_CloseCredit = Value
        End Set
    End Property


    Public Property iATBU_ID() As Integer
        Get
            Return (ATBU_ID)
        End Get
        Set(ByVal Value As Integer)
            ATBU_ID = Value
        End Set
    End Property
    Public Property sATBU_CODE() As String
        Get
            Return (ATBU_CODE)
        End Get
        Set(ByVal Value As String)
            ATBU_CODE = Value
        End Set
    End Property
    Public Property sATBU_Description() As String
        Get
            Return (ATBU_Description)
        End Get
        Set(ByVal Value As String)
            ATBU_Description = Value
        End Set
    End Property
    Public Property iATBU_CustId() As Integer
        Get
            Return (ATBU_CustId)
        End Get
        Set(ByVal Value As Integer)
            ATBU_CustId = Value
        End Set
    End Property

    Public Property iATBU_Branchname() As Integer
        Get
            Return (ATBU_Branchname)
        End Get
        Set(ByVal Value As Integer)
            ATBU_Branchname = Value
        End Set
    End Property

    Public Property dATBU_Opening_Debit_Amount() As Double
        Get
            Return (ATBU_Opening_Debit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBU_Opening_Debit_Amount = Value
        End Set
    End Property

    Public Property dATBU_Opening_Credit_Amount() As Double
        Get
            Return (ATBU_Opening_Credit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBU_Opening_Credit_Amount = Value
        End Set
    End Property
    Public Property dATBU_TR_Debit_Amount() As Double
        Get
            Return (ATBU_TR_Debit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBU_TR_Debit_Amount = Value
        End Set
    End Property
    Public Property dATBU_TR_Credit_Amount() As Double
        Get
            Return (ATBU_TR_Credit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBU_TR_Credit_Amount = Value
        End Set
    End Property
    Public Property dATBU_Closing_Debit_Amount() As Double
        Get
            Return (ATBU_Closing_Debit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBU_Closing_Debit_Amount = Value
        End Set
    End Property

    Public Property dATBU_Closing_Credit_Amount() As Double
        Get
            Return (ATBU_Closing_Credit_Amount)
        End Get
        Set(ByVal Value As Double)
            ATBU_Closing_Credit_Amount = Value
        End Set
    End Property

    Public Property sATBU_DELFLG() As String
        Get
            Return (ATBU_DELFLG)
        End Get
        Set(ByVal Value As String)
            ATBU_DELFLG = Value
        End Set
    End Property

    Public Property iATBU_CRBY() As Integer
        Get
            Return (ATBU_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ATBU_CRBY = Value
        End Set
    End Property

    Public Property sATBU_STATUS() As String
        Get
            Return (ATBU_STATUS)
        End Get
        Set(ByVal Value As String)
            ATBU_STATUS = Value
        End Set
    End Property

    Public Property iATBU_UPDATEDBY() As Integer
        Get
            Return (ATBU_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ATBU_UPDATEDBY = Value
        End Set
    End Property

    Public Property sATBU_IPAddress() As String
        Get
            Return (ATBU_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATBU_IPAddress = Value
        End Set
    End Property

    Public Property iATBU_CompId() As Integer
        Get
            Return (ATBU_CompId)
        End Get
        Set(ByVal Value As Integer)
            ATBU_CompId = Value
        End Set
    End Property

    Public Property iATBU_YEARId() As Integer
        Get
            Return (ATBU_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ATBU_YEARId = Value
        End Set
    End Property

    'Trial balance details by Karthik

    Public Property iATBUD_ID() As Integer
        Get
            Return (ATBUD_ID)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_ID = Value
        End Set
    End Property

    Public Property iATBUD_Masid() As Integer
        Get
            Return (ATBUD_Masid)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_Masid = Value
        End Set
    End Property
    Public Property sATBUD_CODE() As String
        Get
            Return (ATBUD_CODE)
        End Get
        Set(ByVal Value As String)
            ATBUD_CODE = Value
        End Set
    End Property
    Public Property sATBUD_Description() As String
        Get
            Return (ATBUD_Description)
        End Get
        Set(ByVal Value As String)
            ATBUD_Description = Value
        End Set
    End Property
    Public Property iATBUD_CustId() As Integer
        Get
            Return (ATBUD_CustId)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_CustId = Value
        End Set
    End Property

    Public Property iATBUD_Branchname() As Integer
        Get
            Return (ATBUD_Branchname)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_Branchname = Value
        End Set
    End Property
    Public Property iATBUD_SChedule_Type() As Integer
        Get
            Return (ATBUD_SChedule_Type)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_SChedule_Type = Value
        End Set
    End Property

    Public Property iATBUD_Company_Type() As Integer
        Get
            Return (ATBUD_Company_Type)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_Company_Type = Value
        End Set
    End Property

    Public Property iATBUD_Headingid() As Integer
        Get
            Return (ATBUD_Headingid)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_Headingid = Value
        End Set
    End Property

    Public Property iATBUD_Subheading() As Integer
        Get
            Return (ATBUD_Subheading)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_Subheading = Value
        End Set
    End Property
    Public Property iATBUD_itemid() As Integer
        Get
            Return (ATBUD_itemid)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_itemid = Value
        End Set
    End Property

    Public Property iATBUD_Subitemid() As Integer
        Get
            Return (ATBUD_Subitemid)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_Subitemid = Value
        End Set
    End Property

    Public Property sATBUD_DELFLG() As String
        Get
            Return (ATBUD_DELFLG)
        End Get
        Set(ByVal Value As String)
            ATBUD_DELFLG = Value
        End Set
    End Property

    Public Property iATBUD_CRBY() As Integer
        Get
            Return (ATBUD_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_CRBY = Value
        End Set
    End Property

    Public Property sATBUD_STATUS() As String
        Get
            Return (ATBUD_STATUS)
        End Get
        Set(ByVal Value As String)
            ATBUD_STATUS = Value
        End Set
    End Property

    Public Property sATBUD_Progress() As String
        Get
            Return (ATBUD_Progress)
        End Get
        Set(ByVal Value As String)
            ATBUD_Progress = Value
        End Set
    End Property

    Public Property iATBUD_UPDATEDBY() As Integer
        Get
            Return (ATBUD_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_UPDATEDBY = Value
        End Set
    End Property

    Public Property sATBUD_IPAddress() As String
        Get
            Return (ATBUD_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATBUD_IPAddress = Value
        End Set
    End Property

    Public Property iATBUD_CompId() As Integer
        Get
            Return (ATBUD_CompId)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_CompId = Value
        End Set
    End Property

    Public Property iATBUD_YEARId() As Integer
        Get
            Return (ATBUD_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ATBUD_YEARId = Value
        End Set
    End Property

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
    Public Function LoadCustomerOpeningBalance(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = "", asql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0
        Try
            dt.Columns.Add("SLNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("Description")
            dt.Columns.Add("OpDebit")
            dt.Columns.Add("OpCredit")
            dt.Columns.Add("TrDebit")
            dt.Columns.Add("TrCredit")

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CustID=" & iCustomerID & " And gl_compid=" & iACID & " And gl_delflag='C' and gl_Status ='A' and (gl_head=2 or gl_head=3) and gl_status<>'D' order by gl_glcode"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                While dr.Read
                    dRow = dt.NewRow()
                    dRow("SLNo") = i + 1

                    If IsDBNull(dr("gl_glcode")) = False Then
                        dRow("GLCode") = dr("gl_glcode")
                    End If

                    If IsDBNull(dr("gl_Desc")) = False Then
                        dRow("Description") = dr("gl_Desc")
                    End If

                    dt.Rows.Add(dRow)
                    i = i + 1
                End While
            End If
            dr.Close()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccHeadID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_AccHead from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iACID & " and gl_status<>'D'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_ID from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iACID & " and gl_status<>'D'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrgTypeID(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String, ByVal iCustID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select cmm_ID from Content_Management_Master Left join SAD_CUSTOMER_MASTER On CUST_ORGTYPEID=cmm_ID And CUST_CompID=" & iACID & ""
            sSql = sSql & " And CUST_DELFLG='A' where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' And CUST_ID=" & iCustID & " order by cmm_Desc Asc"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTrailBalance(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsOpeningBalance As ClsUploadTailBal)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_AccHead", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_AccHead
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_Parent", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_Parent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_GLCode", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sCC_GLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_GLDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sCC_GLDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_IndType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_OBDebit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dCC_OBDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_OBCredit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dCC_OBCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_TrDebit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dCC_TrDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_TrCredit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dCC_TrCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_CloseDebit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dCC_CloseDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CloseCredit", OleDb.OleDbType.Double, 20)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dCC_CloseCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sCC_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iCC_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_CreatedOn", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objclsOpeningBalance.CC_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_Operation", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsOpeningBalance.CC_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsOpeningBalance.CC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCustomer_COA", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFields(ByVal sAC As String, ByVal iACID As Integer, ByVal iMaterID As Integer) As DataTable
        Dim dtExceload As New DataTable
        Dim dsField As New DataSet
        Dim dRow As DataRow
        Dim aArray As Array, aArrayval As Array
        Dim sSql As String, sStr As String, sString As String
        Dim i As Integer, j As Integer
        Try
            sSql = "Select EUS_Fields,EUS_Values from Excel_Upload_Structure where EUS_Value=" & iMaterID & " And EUS_CompID=" & iACID & ""
            dsField = objDBL.SQLExecuteDataSet(sAC, sSql)
            If dsField.Tables(0).Rows.Count > 0 Then
                sStr = dsField.Tables(0).Rows(0)(0)
                aArray = sStr.Split(",")
                For i = 0 To UBound(aArray)
                    dtExceload.Columns.Add(aArray(i))
                Next
                sString = dsField.Tables(0).Rows(0)(1)
                aArrayval = sString.Split(",")
                dRow = dtExceload.NewRow
                For j = 0 To dtExceload.Columns.Count - 1
                    dRow(j) = aArrayval(j)
                Next
                dtExceload.Rows.Add(dRow)
            End If
            Return dtExceload
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ClearCustomerCOA(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgType As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgType & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ClearCustomerGLLinkage(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From CustomerGL_Linkage_Master Where CLM_CustID=" & iCustID & " And CLM_CompID=" & iACID & " and CLM_YearId=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRecord(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Customer_COA Where CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            CheckRecord = objDBL.DBCheckForRecord(sAC, sSql)
            Return CheckRecord
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetParent(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String
        Dim bCheck As Boolean
        Dim sGLCode As String = ""
        Try
            'sSql = "Select * From Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " And CC_GLCode='" & Trim(sDesc) & "' "
            'bCheck = objDBL.DBCheckForRecord(sAC, sSql)
            'If bCheck = True Then
            '    sSql = "Select CC_GL From Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " And CC_GLCode='" & Trim(sDesc) & "' "
            '    GetParent = objDBL.SQLExecuteScalar(sAC, sSql)
            'Else
            '    '    sSql = "Select Top 1 CC_ID From Customer_COA Where CC_Parent=0 And CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " Order By CC_ID Desc "
            '    '    GetParent = objDBL.SQLExecuteScalar(sAC, sSql)
            '    GetParent = 0
            'End If
            sSql = "" : sSql = "Select LEFT('" & sDesc & "',7)"
            sGLCode = objDBL.SQLGetDescription(sAC, sSql)

            sSql = "" : sSql = "Select CC_GL From Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " And CC_GLCode='" & Trim(sGLCode) & "' "
            GetParent = objDBL.SQLExecuteScalar(sAC, sSql)
            Return GetParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLDBTotal(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal sDesc As String)
        Dim sSql As String
        Try
            sSql = "Select CC_OBDebit From Customer_COA Where CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " And CC_GLCode='" & Trim(sDesc) & "' "
            GetGLDBTotal = objDBL.SQLGetDescription(sAC, sSql)
            Return GetGLDBTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLCRTotal(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal sDesc As String)
        Dim sSql As String
        Try
            sSql = "Select CC_OBCredit From Customer_COA Where CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " And CC_GLCode='" & Trim(sDesc) & "' "
            GetGLCRTotal = objDBL.SQLGetDescription(sAC, sSql)
            Return GetGLCRTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMaxID(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Try
            GetMaxID = objclsGeneralFunctions.GetMaxID(sAC, iACID, "Customer_COA", "CC_ID", "CC_CompID")
            Return GetMaxID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iGLID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select CC_GLCode From Customer_COA Where CC_GL=" & iGLID & " And CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            GetGLCode = objDBL.SQLGetDescription(sAC, sSql)
            Return GetGLCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerTB(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgType As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * From Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iOrgType & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " order by CC_GL"
            GetCustomerTB = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return GetCustomerTB
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateGLCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal iSubGroup As Integer, ByVal GrpGl As Integer)
        Dim sSqlGrp As String = "", sSql As String = ""
        Dim Grp As String = "", prefix As String = "", sGL As String = ""
        Dim GrpLength As Integer, SubGrp As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            sSqlGrp = "" : sSqlGrp = "select IsNull(count(*),0)+1 from Customer_COA where "
            sSqlGrp = sSqlGrp & "CC_acchead ='" & iHead & "' And CC_Parent=" & iSubGroup & " and CC_compId='" & iACID & "' "
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sAC, sSqlGrp))

            sGL = objDBL.SQLExecuteScalar(sAC, "Select gl_glCode from chart_of_accounts where gl_AccHead = " & iHead & " And gl_ID=" & iSubGroup & " and gl_compId='" & iACID & "' and gl_status<>'D' ")

            sSql = "" : sSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iACID & "'"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_subgroup")) = False Then
                    SubGrp = dr("acs_subgroup")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If Grp.Length < SubGrp Then
                    Grp = Grp.PadLeft(SubGrp, "0")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateSubGLCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal iGL As Integer, ByVal GrpGl As Integer)
        Dim sSqlGrp As String = "", sSql As String = ""
        Dim Grp As String = "", prefix As String = "", sGL As String = ""
        Dim GrpLength As Integer, SubGrp As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            sSqlGrp = "" : sSqlGrp = "select IsNull(count(*),0)+1 from Customer_COA where "
            sSqlGrp = sSqlGrp & "CC_acchead ='" & iHead & "' And CC_Parent=" & iGL & " and CC_compId='" & iACID & "' "
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sAC, sSqlGrp))

            sGL = objDBL.SQLExecuteScalar(sAC, "Select CC_glCode from Customer_COA where CC_AccHead = " & iHead & " And CC_GL=" & iGL & " and CC_compId='" & iACID & "' ")

            sSql = "" : sSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iACID & "'"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_subGL")) = False Then
                    SubGrp = dr("acs_subGL")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If Grp.Length < SubGrp Then
                    Grp = Grp.PadLeft(SubGrp, "00")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustChartOfAccount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim iCount As Integer
        Try
            sSql = "select Count(*) from Chart_of_Accounts where gl_custId=" & iCustId & " and gl_status<>'D'"
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSubGroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select gl_id , gl_desc from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_Head=1 and gl_status<>'D' order by gl_desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindGl(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iParentId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select gl_id , gl_desc from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_Head=2 and gl_parent=" & iParentId & " and gl_status<>'D' order by gl_Acchead"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindHeadings(ByVal sAC As String, ByVal iACID As Integer, ByVal IsheduleTypeId As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Dim orgTypeId As Integer = 0
        Try
            orgTypeId = getOrgtype(sAC, iACID, iCustId)
            sSql = " select distinct(b.ASH_Name),ISNull(ASSH_ID,0),ASSH_Name,ISNull(ASI_ID,0),"
            sSql = sSql & " ASI_Name,ISNull(ASSI_ID,0),ASSI_Name from ACC_ScheduleTemplates"
            sSql = sSql & " left join ACC_ScheduleHeading b on b.ASH_ID = AST_HeadingID and ASH_STATUS<>'D'"
            sSql = sSql & " left join ACC_ScheduleSubHeading c on c.ASSH_ID=AST_SubHeadingID and ASSH_DELFLG<'D'"
            sSql = sSql & " left join ACC_ScheduleItems d on d.ASI_ID=AST_ItemID and ASI_DELFLG<>'D'"
            sSql = sSql & " left join ACC_ScheduleSubItems e on e.ASSI_ID=AST_SubItemID and ASSI_DELFLG<>'D'"
            sSql = sSql & " where AST_CompId =" & iACID & "and AST_Companytype=" & orgTypeId & " and AST_Schedule_type=" & IsheduleTypeId & " and b.ASH_Name IS NOT NULL and ASH_ID is NOT NULL"
            'sSql = "select distinct(b.ASH_Name),ASH_ID from ACC_ScheduleTemplates 
            '        left join ACC_ScheduleHeading b on b.ASH_ID = AST_HeadingID and ASH_STATUS<>'D'"
            'sSql = sSql & " where AST_CompId =" & iACID & "and AST_Companytype=" & orgTypeId & " and AST_Schedule_type=" & IsheduleTypeId & " and b.ASH_Name IS NOT NULL and ASH_ID is NOT NULl"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindScheduleHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal IsheduleTypeId As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Dim orgTypeId As Integer = 0
        Try
            orgTypeId = getOrgtype(sAC, iACID, iCustId)
            sSql = "select distinct(b.ASH_Name),ASH_ID from ACC_ScheduleTemplates 
                    left join ACC_ScheduleHeading b on b.ASH_ID = AST_HeadingID and ASH_STATUS<>'D'"
            sSql = sSql & " where AST_CompId =" & iACID & "and AST_Companytype=" & orgTypeId & " and AST_Schedule_type=" & IsheduleTypeId & " and b.ASH_Name IS NOT NULL and ASH_ID is NOT NULl"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getOrgtype(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim OrgtypeId As Integer
        Try
            sSql = "select CUST_ORGTYPEID from SAD_CUSTOMER_MASTER where CUST_ID=" & iCustId & " and CUST_DELFLG= 'A'"
            OrgtypeId = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return OrgtypeId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindScheduleSubHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal IsheduleTypeId As Integer, ByVal iCustId As Integer, ByVal iHeadingId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Dim orgTypeId As Integer = 0
        Try
            orgTypeId = getOrgtype(sAC, iACID, iCustId)
            sSql = "select distinct(b.AsSH_Name),ASsH_ID from ACC_ScheduleTemplates 
                    left join ACC_ScheduleSubHeading b on b.AsSH_ID = AST_SubHeadingID and ASsH_STATUS<>'D'"
            sSql = sSql & " where AST_CompId =" & iACID & "and AST_Companytype=" & orgTypeId & "and AST_Schedule_type= " & IsheduleTypeId & " and b.AsSH_Name IS NOT NULL and ASsH_ID is NOT NULl"
            If iHeadingId <> 0 Then
                sSql = sSql & " and ASt_HeadingID =" & iHeadingId & " "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindScheduleItemsHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal IsheduleTypeId As Integer, ByVal iCustId As Integer, ByVal iHeadingId As Integer, ByVal iSubHeadingId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Dim orgTypeId As Integer = 0
        Try
            orgTypeId = getOrgtype(sAC, iACID, iCustId)
            sSql = "select distinct(b.ASI_Name),ASI_ID from ACC_ScheduleTemplates 
                    left join ACC_ScheduleItems b on b.ASI_ID = AST_ItemID and ASI_STATUS<>'D'"
            sSql = sSql & " where AST_CompId =" & iACID & "and AST_Companytype=" & orgTypeId & " and AST_Schedule_type= " & IsheduleTypeId & " and b.ASI_Name IS NOT NULL and ASI_ID is NOT NULl"
            If iHeadingId <> 0 Then
                sSql = sSql & " and AST_HeadingID =" & iHeadingId & " "
            End If
            If iSubHeadingId <> 0 Then
                sSql = sSql & " and AST_SubHeadingID =" & iSubHeadingId & " "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindScheduleSubItemsHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal IsheduleTypeId As Integer, ByVal iCustId As Integer, ByVal iHeadingId As Integer, ByVal iSubHeadingId As Integer, ByVal iItemId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Dim orgTypeId As Integer = 0
        Try
            orgTypeId = getOrgtype(sAC, iACID, iCustId)
            sSql = "select distinct(b.ASSI_Name),ASSI_ID from ACC_ScheduleTemplates 
                    left join ACC_ScheduleSubItems b on b.ASSI_ID = AST_SubItemID and ASSI_STATUS<>'D'"
            sSql = sSql & " where AST_CompId =" & iACID & "and AST_Companytype=" & orgTypeId & " and AST_Schedule_type= " & IsheduleTypeId & " and b.ASSI_Name IS NOT NULL and ASSI_ID is NOT NULl"
            If iHeadingId <> 0 Then
                sSql = sSql & " and AST_HeadingID =" & iHeadingId & " "
            End If
            If iSubHeadingId <> 0 Then
                sSql = sSql & " and AST_SubHeadingID =" & iSubHeadingId & " "
            End If
            If iItemId <> 0 Then
                sSql = sSql & " and AST_ItemID =" & iItemId & " "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function bindtemplate(ByVal sAC As String, ByVal iACID As Integer, ByVal Isubitemid As Integer, ByVal iitemid As Integer, ByVal iSheadingid As Integer, ByVal iheadingid As Integer, ByVal iScheduleid As Integer, ByVal iCustId As Integer)
        Dim sSql, sSqlItem As String
        Dim dt As New DataTable
        Dim Itemid, SubHeadingID As New Integer
        Try 'for Itemid heading
            'sSql = "select AST_ItemID,ast_subheadingid,Ast_headingid from ACC_ScheduleTemplates where"
            'If iitemid <> 0 Then
            '    sSql = sSql & " Ast_itemid=" & iitemid & " And"
            '    sSql = sSql & " Ast_compid=" & iACID & " and AST_STATUS<>'D'"
            '    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            '    If dt.Rows.Count > 0 Then
            '        If dt.Rows(0)("ast_subheadingid") <> 0 Then
            '            sSql = sSql & " And ast_subheadingid=" & dt.Rows(0)("ast_subheadingid") & ""
            '            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            '        End If
            '        If dt.Rows.Count > 0 Then
            '            If dt.Rows(0)("Ast_headingid") <> 0 Then
            '                sSql = sSql & " And ast_subheadingid = " & dt.Rows(0)("ast_subheadingid") & "And Ast_headingid=" & dt.Rows(0)("Ast_headingid") & ""
            '                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            '            End If
            '        End If
            '    End If
            'ElseIf iSheadingid <> 0 Then
            '    sSql = sSql & " ASt_subheadingid=" & iSheadingid & " And"
            'ElseIf iheadingid <> 0 Then
            '    sSql = sSql & " ast_heading=" & iSheadingid & " And"
            'Else
            '    sSql = sSql & "And "
            'End If
            'sSql = sSql & " And Ast_compid=" & iACID & " And AST_STATUS<>'D'"
            'dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            'Return dt
            Dim orgTypeId As Integer = 0
            orgTypeId = getOrgtype(sAC, iACID, iCustId)
            'For Sub item heading
            sSql = "select AST_SubItemID,AST_ItemID,ast_subheadingid,Ast_headingid from ACC_ScheduleTemplates where AST_Schedule_type= " & iScheduleid & " and AST_Companytype=" & orgTypeId & ""
            If Isubitemid <> 0 Then
                sSql = sSql & " And AST_SubItemID=" & Isubitemid & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Ast_itemid") <> 0 Then
                    sSql = sSql & " And Ast_itemid=" & iitemid & dt.Rows(0)("Ast_itemid") & ""
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
                If dt.Rows(0)("ast_subheadingid") <> 0 Then
                    sSql = sSql & " And ast_subheadingid=" & dt.Rows(0)("ast_subheadingid") & ""
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("Ast_headingid") <> 0 Then
                        sSql = sSql & " And Ast_headingid=" & dt.Rows(0)("Ast_headingid") & ""
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                    End If
                End If
            ElseIf iitemid <> 0 Then
                sSql = sSql & " And Ast_itemid=" & iitemid & ""
            ElseIf iSheadingid <> 0 Then
                sSql = sSql & " And ASt_subheadingid=" & iSheadingid & ""
            ElseIf iheadingid <> 0 Then
                sSql = sSql & " And Ast_headingid=" & iheadingid & ""
            End If
            sSql = sSql & " And Ast_compid=" & iACID & " And AST_STATUS<>'D'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function bindgroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iGLId As Integer)
        Dim sSql As String, sSql1 As String
        Dim dt As New DataTable
        Dim iParent As New Integer
        Try
            sSql = "select gl_parent from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_id=" & iGLId & " and gl_status<>'D'"
            iParent = objDBL.SQLExecuteScalar(sAC, sSql)
            sSql = "select gl_desc,gl_accHead,gl_Parent from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_id=" & iParent & " and gl_status<>'D'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Getgroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iGLId As Integer)
        Dim sSql As String
        Dim iGroup As Integer
        Try
            sSql = "select gl_parent from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_Head=0  and gl_id=" & iGLId & " and gl_status<>'D'"
            iGroup = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iGroup
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function bindglCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iGLId As Integer)
        Dim sSql As String, sSql1 As String
        Dim dt As New DataTable
        Dim iGlCode As String
        Try
            sSql = "select gl_glCode from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_id=" & iGLId & " and gl_status<>'D'"
            iGlCode = objDBL.SQLExecuteScalar(sAC, sSql)

            Return iGlCode
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetglCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iGLId As Integer)
        Dim sSql As String, sSql1 As String
        Dim dt As New DataTable
        Dim iGlCode As Integer
        Try
            sSql = "select count(CC_glCode) from Customer_coa where cc_custid=" & iCustId & " and cc_compid=" & iACID & " and cc_Parent=" & iGLId & " "
            iGlCode = objDBL.SQLExecuteScalar(sAC, sSql)

            Return iGlCode
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCustglCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iGLId As Integer)
        Dim sSql As String, sSql1 As String
        Dim dt As New DataTable
        Dim iGlCount As Integer
        Try
            sSql = "select count(*) from CustomerGL_Linkage_Master where CLM_custid=" & iCustId & " and CLM_compid=" & iACID & " and CLM_GLID=" & iGLId & ""
            iGlCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iGlCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCustomerLinkageMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iGlId As Integer, ByVal sGeneralLedger As String, ByVal sIPAddress As String, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iHead As Integer)
        Dim sSql As String = ""
        Dim iMaxId As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CLM_ID),0)+1 from CustomerGL_Linkage_Master")
            sSql = "" : sSql = "Insert into CustomerGL_Linkage_Master(CLM_ID,CLM_Head,CLM_GroupID,CLM_SubGroupID,"
            sSql = sSql & "CLM_GLLedger,CLM_CreatedBy,"
            sSql = sSql & "CLM_CreatedOn,CLM_Status,CLM_YearID,CLM_CompID,CLM_Operation,CLM_IPAddress,CLM_CustID,CLM_OrgID,CLM_GL,CLM_SubGL,CLM_GLID)"
            sSql = sSql & "Values(" & iMaxId & "," & iHead & "," & iGroup & "," & iSubGroup & ","
            sSql = sSql & "'" & sGeneralLedger & "'," & iUserID & ","
            sSql = sSql & "GetDate(),'A'," & iYearID & "," & iACID & ",'C','" & sIPAddress & "'," & iCustID & "," & iOrgID & ",'0','1'," & iGlId & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function UpdateCustomerLinkageMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iGlId As Integer, ByVal sGeneralLedger As String)
        Dim sSql As String = ""
        Dim sGeneralLedgeritem As String, sGeneralLedgerValue As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "select CLM_GLLedger from CustomerGL_Linkage_Master where CLM_custid=" & iCustID & " and CLM_compid=" & iACID & " and CLM_GLID=" & iGlId & ""
            sGeneralLedgeritem = objDBL.SQLExecuteScalar(sAC, sSql)
            If sGeneralLedger <> "" Then
                sGeneralLedgerValue = sGeneralLedgeritem & sGeneralLedger
                sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedgerValue & "',CLM_Operation='U' where "
                sSql = sSql & "CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & " and CLM_GLID=" & iGlId & ""

                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
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
    Public Function GetCustomerSchedule(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim iCount As Integer
        Try
            sSql = "select Count(*) from Acc_TrailBalance_Upload where ATBU_CustId=" & iCustId & " and ATBU_Compid=" & iACID & " and ATBU_YearID=" & iYearID & ""
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCustCOAMasterDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal IscheduleTypeid As Integer, ByVal Unmapped As Integer, ByVal Ibranchid As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataSet
        Try
            'Changed qry for avoiding JE Loop (Qry Optimization) (22_06_23)
            ''sSql = "select CC_GL from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " order by cc_gl"
            'sSql = "select  ROW_NUMBER() OVER (ORDER BY ATBU_ID ASC) AS SrNo, b.ATBUD_ID as DescID, Atbu_id as DescDetailsID,ATBU_code as DescriptionCode,ATBU_CustId, ATBU_Description as Description, ATBU_CustId, ATBU_Description as Description, CAST(ATBU_Opening_Debit_Amount AS DECIMAL(19, 2))"
            'sSql = sSql & " as OpeningDebit, CAST(ATBU_Opening_Credit_Amount AS DECIMAL(19, 2))  as OpeningCredit,"
            'sSql = sSql & " CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebit,CAST (ATBU_TR_Credit_Amount as DECIMAL(19,2)) as TrCredit,  CAST(ATBU_Closing_TotalDebit_Amount AS DECIMAL(19, 2))  As ClosingDebit,"
            'sSql = sSql & " CAST(ATBU_Closing_TotalCredit_Amount AS DECIMAL(19, 2))   As ClosingCredit,"
            'sSql = sSql & " ISNULL(b.ATBUD_SubItemId,0) as subItemID, ASSI_Name,ISNULL(b.atbud_itemid,0) as itemid,ASI_Name, "
            'sSql = sSql & " ISNULL(b.atbud_subheading,0) as subheadingid ,ASSH_Name,ISNULL(b.atbud_headingid,0) as headingid, "
            'sSql = sSql & " ASH_Name,b.atbud_progress as Status,b.Atbud_Company_type as Companytype,"
            'sSql = sSql & " ATBUD_SChedule_Type as ScheduleType, CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebittrUploaded,CAST (ATBU_TR_Credit_Amount as decimal(19,2)) as TrCredittrUploaded  From Acc_TrailBalance_Upload"
            'sSql = sSql & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Description = ATBU_Description and b.ATBUD_CustId=" & iCustId & " and b.ATBUD_YEARId=" & iYearID & ""
            'If Ibranchid > 0 Then
            '    sSql = sSql & " and b.Atbud_Branchnameid=" & Ibranchid & ""
            'End If
            'If IscheduleTypeid <> 0 Then
            '    sSql = sSql & " And b.ATBUD_SChedule_Type=" & IscheduleTypeid & " And b.ATBUD_Headingid=0 "
            'Else
            'End If
            'sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid"
            'sSql = sSql & " left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading"
            'sSql = sSql & " left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid"
            'sSql = sSql & " left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
            'sSql = sSql & " where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId =" & iYearID & ""
            'If Ibranchid > 0 Then
            '    sSql = sSql & " and ATBU_Branchid=" & Ibranchid & ""
            'End If
            'If IscheduleTypeid <> 0 Then
            '    sSql = sSql & " And b.ATBUD_ID Is Not NULL"
            'End If
            'If Unmapped <> 0 Then
            '    sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
            'End If
            'sSql = sSql & " order by ATBU_ID"
            'dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            'If dt.Rows.Count > 0 Then
            '    For i = 0 To dt.Rows.Count - 1
            '        sSql = "select ISNULL(sum(AJTB_Debit),0) as AJTB_Debit,ISNULL(sum(AJTB_Credit),0) as AJTB_Credit from Acc_JETransactions_Details "
            '        sSql = sSql & " left join Acc_JE_Master a on a.Acc_JE_ID=Ajtb_Masid"
            '        sSql = sSql & " where Acc_JE_Status='A' And AJTB_DescName='" & dt.Rows(i)("Description") & "' And Acc_JE_Party=" & iCustId & " And Acc_JE_YearID=" & iYearID & " And acc_JE_BranchId=" & Ibranchid & ""
            '        dtJeDet = objDBL.SQLExecuteDataTable(sAC, sSql)
            '        If dtJeDet.Rows.Count > 0 Then
            '            dt.Rows(i)("TrDebit") = dt.Rows(i)("TrDebit") + dtJeDet.Rows(0)("AJTB_Debit")
            '            dt.Rows(i)("TrCredit") = dt.Rows(i)("TrCredit") + dtJeDet.Rows(0)("AJTB_Credit")
            '        End If
            '    Next
            'End If



            '''' Cmnted sumne kelsa illa anta

            sSql = "select  ROW_NUMBER() OVER (ORDER BY ATBU_ID ASC) AS SrNo, b.ATBUD_ID as DescID, Atbu_id as DescDetailsID,ATBU_code as DescriptionCode,ATBU_CustId, ATBU_Description as Description, ATBU_CustId, ATBU_Description as Description, CAST(ATBU_Opening_Debit_Amount AS DECIMAL(19, 2))"
            sSql = sSql & " as OpeningDebit, CAST(ATBU_Opening_Credit_Amount AS DECIMAL(19, 2))  as OpeningCredit,"
            sSql = sSql & " CAST(sum(ATBU_TR_Debit_Amount+ isnull(g.AJTB_Debit,0)) AS DECIMAL(19, 2)) as TrDebit,CAST (sum(ATBU_TR_Credit_Amount+ isnull(h.AJTB_Credit,0)) as DECIMAL(19,2)) as TrCredit,  CAST(ATBU_Closing_TotalDebit_Amount AS DECIMAL(19, 2))  As ClosingDebit,"
            sSql = sSql & " CAST(ATBU_Closing_TotalCredit_Amount AS DECIMAL(19, 2))   As ClosingCredit,"
            sSql = sSql & " ISNULL(b.ATBUD_SubItemId,0) as subItemID, ASSI_Name,ISNULL(b.atbud_itemid,0) as itemid,ASI_Name, "
            sSql = sSql & " ISNULL(b.atbud_subheading,0) as subheadingid ,ASSH_Name,ISNULL(b.atbud_headingid,0) as headingid, "
            sSql = sSql & " ASH_Name,b.atbud_progress as Status,b.Atbud_Company_type as Companytype,"
            sSql = sSql & " ATBUD_SChedule_Type as ScheduleType, CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebittrUploaded,CAST (ATBU_TR_Credit_Amount as decimal(19,2)) as TrCredittrUploaded  From Acc_TrailBalance_Upload "
            sSql = sSql & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Description = ATBU_Description and b.ATBUD_CustId=" & iCustId & " and b.ATBUD_YEARId=" & iYearID & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and b.Atbud_Branchnameid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_SChedule_Type=" & IscheduleTypeid & " And b.ATBUD_Headingid=0 "
            Else
            End If
            sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid"
            sSql = sSql & " left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading"
            sSql = sSql & " left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid"
            sSql = sSql & " left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
            sSql = sSql & " left join Acc_JETransactions_Details g on g.AJTB_DescName= ATBU_Description and g.AJTB_Status='A' and g.AJTB_CustId=" & iCustId & " and g.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and g.ajtb_BranchId=" & Ibranchid & " and g.AJTB_Credit =0 "
            End If
            sSql = sSql & " left join Acc_JETransactions_Details h on h.AJTB_DescName= ATBU_Description and h.AJTB_Status='A' and h.AJTB_CustId=" & iCustId & " and h.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and h.ajtb_BranchId=" & Ibranchid & " and h.AJTB_Debit=0 "
            End If
            sSql = sSql & " where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId =" & iYearID & "  and b.atbud_subheading not in (133,146) "
            If Ibranchid > 0 Then
                sSql = sSql & " and ATBU_Branchid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_ID Is Not NULL"
            End If
            If Unmapped <> 0 Then
                sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
            End If
            sSql = sSql & " group by b.ATBUD_ID , Atbu_id ,ATBU_code ,ATBU_CustId,ATBU_Description , ATBU_CustId, ATBU_Description,"
            sSql = sSql & " ATBU_Opening_Debit_Amount,ATBU_Opening_Credit_Amount,  ATBU_TR_Debit_Amount,ATBU_TR_Credit_Amount "
            sSql = sSql & " ,ATBU_Closing_TotalDebit_Amount,ATBU_Closing_TotalCredit_Amount,ATBUD_SubItemId,atbud_itemid,ASI_Name,"
            sSql = sSql & " atbud_subheading,ASSH_Name,atbud_headingid,ASH_Name,atbud_progress, "
            sSql = sSql & " Atbud_Company_type,ATBUD_SChedule_Type,ATBU_TR_Debit_Amount,ATBU_TR_Credit_Amount,ASSI_Name "
            sSql = sSql & " order by ATBU_ID ;"

            sSql += "  select sum(ATBU_Opening_Debit_Amount) as OpeningDebit,sum(ATBU_Opening_Credit_Amount) as OpeningCredit, "
            sSql = sSql & " sum(ATBU_TR_Debit_Amount+ ISNULL(AJTB_debit,0)) as TrDebit,sum(ATBU_TR_Credit_Amount+ ISNULL(AJTB_Credit,0)) as TrCredit , "
            sSql = sSql & " sum(ATBU_Closing_TotalCredit_Amount) as  ClosingCredit, sum(ATBU_Closing_Totaldebit_Amount) as  Closingdebit "
            sSql = sSql & " From Acc_TrailBalance_Upload  "
            sSql = sSql & " left join Acc_JETransactions_Details g on g.AJTB_DescName= ATBU_Description and g.AJTB_Status='A' and g.AJTB_CustId=" & iCustId & " and g.AJTB_YearID=" & iYearID & " "
            sSql = sSql & " and g.ajtb_BranchId=4 where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId =" & iYearID & "  "
            If Ibranchid > 0 Then
                sSql = sSql & " and ATBU_Branchid=" & Ibranchid & " ;"
            End If





            sSql += "       Select 0 As SrNo, 0 As DescID, 0 As DescDetailsID,ASSH_Name As DescriptionCode,ATBU_CustId, ASSH_Name As Description,
                            ATBU_CustId, '' as Description, CAST(sum(ATBU_Opening_Debit_Amount) AS DECIMAL(19, 2)) as OpeningDebit,
                            CAST(sum(ATBU_Opening_Credit_Amount) As Decimal(19, 2))  as OpeningCredit, 
                            CAST(sum(ATBU_TR_Debit_Amount + isnull(g.AJTB_Debit, 0)) As Decimal(19, 2)) As TrDebit,
                            CAST(sum(ATBU_TR_Credit_Amount + isnull(h.AJTB_Credit, 0)) As Decimal(19, 2)) As TrCredit, 
                            CAST(sum(ATBU_Closing_TotalDebit_Amount)As Decimal(19, 2))  As ClosingDebit,
                            CAST(Sum(ATBU_Closing_TotalCredit_Amount) As Decimal(19, 2))   As ClosingCredit,  0 As subItemID, ASSI_Name,
                            0 as itemid,ASI_Name,  ISNULL(b.atbud_subheading,0) as subheadingid,
                            ASSH_Name, ISNULL(b.atbud_headingid, 0) As headingid, ASH_Name, b.atbud_progress As Status, b.Atbud_Company_type As Companytype,
                            ATBUD_SChedule_Type as ScheduleType, CAST(SUM(ATBU_TR_Debit_Amount) As Decimal(19, 2)) as TrDebittrUploaded,
                            CAST(SUM(ATBU_TR_Credit_Amount) As Decimal(19, 2)) As TrCredittrUploaded "
            sSql = sSql & " From Acc_TrailBalance_Upload "
            sSql = sSql & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Description = ATBU_Description and b.ATBUD_CustId=" & iCustId & " and b.ATBUD_YEARId=" & iYearID & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and b.Atbud_Branchnameid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_SChedule_Type=" & IscheduleTypeid & " And b.ATBUD_Headingid=0 "
            Else
            End If
            sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid"
            sSql = sSql & " left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading"
            sSql = sSql & " left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid"
            sSql = sSql & " left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
            sSql = sSql & " left join Acc_JETransactions_Details g on g.AJTB_DescName= ATBU_Description and g.AJTB_Status='A' and g.AJTB_CustId=" & iCustId & " and g.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and g.ajtb_BranchId=" & Ibranchid & " and g.AJTB_Credit =0 "
            End If
            sSql = sSql & " left join Acc_JETransactions_Details h on h.AJTB_DescName= ATBU_Description and h.AJTB_Status='A' and h.AJTB_CustId=" & iCustId & " and h.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and h.ajtb_BranchId=" & Ibranchid & " and h.AJTB_Debit=0 "
            End If
            sSql = sSql & " where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId =" & iYearID & "  and b.atbud_subheading  in (133,146) "
            If Ibranchid > 0 Then
                sSql = sSql & " and ATBU_Branchid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_ID Is Not NULL"
            End If
            If Unmapped <> 0 Then
                sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
            End If
            sSql = sSql & " group by ATBU_CustId,ATBUD_SubItemId,atbud_itemid,ASI_Name, "
            sSql = sSql & " atbud_subheading,ASSH_Name,atbud_headingid,ASH_Name,atbud_progress,  Atbud_Company_type,ATBUD_SChedule_Type, "
            sSql = sSql & " ASSI_Name,ASI_Name "


            dt = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustCOADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal IscheduleTypeid As Integer, ByVal Unmapped As Integer, ByVal Ibranchid As Integer)
        Dim sSql, sSql1 As String, sSqlParent As String = ""
        Dim dt, dtJeDet As New DataTable, dtParent As New DataTable, dtMerge As New DataTable
        Dim dRow As DataRow
        Dim dtsum As New DataTable
        Dim sumJEDebit As New Double
        Dim sumJECredit As New Double
        Try
            'sSql = "select CC_GL from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " order by cc_gl"
            sSql = "select  ROW_NUMBER() OVER (ORDER BY ATBU_ID ASC) AS SrNo, b.ATBUD_ID as DescDetailsID, Atbu_id as  DescID,ATBU_code as DescriptionCode,ATBU_CustId, ATBU_Description as Description, ATBU_Opening_Debit_Amount as OpeningDebit,"
            sSql = sSql & " ATBU_Opening_Credit_Amount as OpeningCredit, ATBU_TR_Debit_Amount as TrDebit,ATBU_TR_Credit_Amount as TrCredit,"
            sSql = sSql & " ATBU_Closing_TotalDebit_Amount As ClosingDebit, ATBU_Closing_TotalCredit_Amount As ClosingCredit,"
            sSql = sSql & " ISNULL(b.ATBUD_SubItemId,0) as subItemID, ASSI_Name,ISNULL(b.atbud_itemid,0) as itemid,ASI_Name,"
            sSql = sSql & " ISNULL(b.atbud_subheading,0) as subheadingid ,ASSH_Name,ISNULL(b.atbud_headingid,0) as headingid,"
            sSql = sSql & " ASH_Name,b.atbud_progress as Status,b.Atbud_Company_type as Companytype,"
            sSql = sSql & " ATBUD_SChedule_Type as ScheduleType, CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebittrUploaded,cast (ATBU_TR_Credit_Amount as decimal(19,2)) as TrCredittrUploaded   From Acc_TrailBalance_Upload"
            sSql = sSql & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Description = ATBU_Description and b.ATBUD_CustId=" & iCustId & " and b.ATBUD_YEARId=" & iYearID & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and b.Atbud_Branchnameid=" & Ibranchid & ""
            End If
            sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid
            left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading
            left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid
            left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
            sSql = sSql & " where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId=" & iYearID & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and ATBU_Branchid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And (b.ATBUD_Headingid=0 Or b.ATBUD_SChedule_Type=" & IscheduleTypeid & " or b.ATBUD_SChedule_Type=0 )"
            Else
                'sSql = sSql & " And b.ATBUD_ID Is Not NULL"
            End If
            If Unmapped <> 0 Then
                sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
            End If
            sSql = sSql & " order by ATBU_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)



            'sSqlParent = "" : sSqlParent = "select ROW_NUMBER() OVER (ORDER BY Ajtb_id ASC) AS SrNo, Ajtb_id DescID,b.ATBUD_code DescriptionCode,"
            'sSqlParent = sSqlParent & "b.ATBUD_Description as Description, 0 as OpeningDebit, 0 as OpeningCredit, AJTB_CustId,ajtb_deschead ,AJTB_Debit as TrDebit,AJTB_Credit as TrCredit,"
            'sSqlParent = sSqlParent & "0 as ClosingDebit, 0 as ClosingCredit,ISNULL(b.atbud_itemid,0) as itemid,ISNULL(b.atbud_subheading,0) as subheadingid,"
            'sSqlParent = sSqlParent & "ISNULL(b.atbud_headingid,0) as headingid, 'Uploaded' as Status,"
            'sSqlParent = sSqlParent & "ISNULL(b.Atbud_Company_type,0) as Companytype "
            'sSqlParent = sSqlParent & " from Acc_JETransactions_Details"
            'sSqlParent = sSqlParent & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Custid = AJTB_Custid and b.ATBUD_ID = ajtb_deschead"
            'sSqlParent = sSqlParent & " where AJTB_CustId = " & iCustId & " And AJTB_CompID =" & iACID & " And AJTB_Status='A'"
            'dtParent = objDBL.SQLExecuteDataTable(sAC, sSqlParent)
            ' Return dt
            'dt.Merge(dtParent, True, MissingSchemaAction.Ignore)
            'sSqlParent = "select distinct(a.cc_gl) as slno,a.CC_GLDesc as SGLDescription,a.CC_OBDebit as OpeningDebit,a.CC_OBCredit as OpeningCredit,"
            'sSqlParent = sSqlParent & "a.CC_TrDebit as TrDebit, a.CC_TrCredit As TrCredit, a.CC_CloseDebit As ClosingDebit, a.CC_CloseCredit As ClosingCredit, a.cc_parent As GL,"
            'sSqlParent = sSqlParent & "'' as GLTotal,0 as SubGroup,'' as  SubGroupTotal,'' as Group1,'' as  GroupTotal,'' as HeadTotal,'' as  Head,CC_Status"
            'sSqlParent = sSqlParent & " From Customer_COA  a Where a.cc_custid = " & iCustId & " And a.cc_compid = " & iACID & " and a.cc_parent=0 Order By CC_GL"
            'dtParent = objDBL.SQLExecuteDataTable(sAC, sSqlParent)
            'Return dtParent
            'dt.Merge(dtParent, True, MissingSchemaAction.Ignore)
            If dt.Rows.Count > 0 Then
                sumJEDebit = 0
                sumJECredit = 0
                For i = 0 To dt.Rows.Count - 1
                    sSql = "select ISNULL(AJTB_Debit,0)as AJTB_Debit,ISNULL(AJTB_Credit,0)as AJTB_Credit from Acc_JETransactions_Details where AJTB_Status='A' And AJTB_Desc=" & dt.Rows(i)("DescDetailsID") & " And AJTB_CustId=" & iCustId & " And AJTB_YearID=" & iYearID & ""
                    dtJeDet = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If dtJeDet.Rows.Count > 0 Then
                        dt.Rows(i)("TrDebit") = dt.Rows(i)("TrDebit") + dtJeDet.Rows(0)("AJTB_Debit")
                        dt.Rows(i)("TrCredit") = dt.Rows(i)("TrCredit") + dtJeDet.Rows(0)("AJTB_Credit")
                        sumJEDebit = dt.Rows(i)("TrDebit") + dtJeDet.Rows(0)("AJTB_Debit") + sumJEDebit
                        sumJECredit = dt.Rows(i)("TrDebit") + dtJeDet.Rows(0)("AJTB_Credit") + sumJECredit
                    End If
                Next
                sSql = " select sum(ATBU_Opening_Debit_Amount) as OpeningDebit,"
                sSql = sSql & " sum(ATBU_Opening_Credit_Amount) as OpeningCredit, sum(ATBU_TR_Debit_Amount) as TrDebit,sum(ATBU_TR_Credit_Amount) as TrCredit,"
                sSql = sSql & " sum(ATBU_Closing_TotalDebit_Amount) As ClosingDebit, sum(ATBU_Closing_TotalCredit_Amount) As ClosingCredit  From Acc_TrailBalance_Upload 
                     left join Acc_TrailBalance_Upload_details b on b.atbud_masid = atbu_id 
                     left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid
                     left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading
                     left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid
                     left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
                sSql = sSql & " where ATBU_CustId=" & iCustId & " And b.ATBUD_compid=" & iACID & " And  b.ATBUD_YEARId=" & iYearID & ""
                If Ibranchid > 0 Then
                    sSql = sSql & "and ATBU_Branchid=" & Ibranchid & ""
                End If
                If IscheduleTypeid <> 0 Then
                    sSql = sSql & " And (b.ATBUD_Headingid=0 Or b.ATBUD_SChedule_Type=" & IscheduleTypeid & ")"
                Else
                    'sSql = sSql & " And b.ATBUD_ID Is Not NULL"
                End If
                If Unmapped <> 0 Then
                    sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
                End If
                dtsum = objDBL.SQLExecuteDataTable(sAC, sSql)

                'dRow = dt.NewRow
                'dRow("Description") = "Total"
                'dRow("OpeningCredit") = dtsum(0)("OpeningCredit")
                'dRow("OpeningDebit") = dtsum(0)("OpeningDebit")
                'dRow("TrDebit") = dtsum(0)("TrDebit") + sumJEDebit
                'dRow("TrCredit") = dtsum(0)("TrCredit") + sumJECredit
                'dRow("ClosingCredit") = dtsum(0)("ClosingCredit")
                'dRow("ClosingDebit") = dtsum(0)("ClosingDebit")
                'dt.Rows.Add(dRow)
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustCOADetailsNoLinkage(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            ' sSql = "select CC_GL from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " order by cc_gl"
            sSql = "select distinct(a.cc_gl) as slno,a.CC_GLDesc as SGLDescription,a.CC_OBDebit as OpeningDebit,a.CC_OBCredit as OpeningCredit,"
            sSql = sSql & "a.CC_TrDebit as TrDebit, a.CC_TrCredit As TrCredit, a.CC_CloseDebit As ClosingDebit, a.CC_CloseCredit As ClosingCredit, a.cc_parent As GL,"
            sSql = sSql & "'' as GLTotal,'' as SubGroup,'' as  SubGroupTotal,'' as Group1,'' as  GroupTotal,'' as HeadTotal,'' as  Head,CC_Status"
            sSql = sSql & " From Customer_COA  a Where a.cc_custid = 3 And a.cc_compid = 1 Order By CC_GL"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSubGroupId(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iGLId As Integer)
        Dim sSql As String
        Dim iSubGroup As Integer
        Try
            sSql = "select gl_parent from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_Head=1  and gl_id=" & iGLId & ""
            iSubGroup = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iSubGroup
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSubGroupGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iGlid As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select gl_id , gl_desc from Chart_Of_Accounts where gl_custid=" & iCustId & " and gl_compid=" & iACID & " and gl_Head=1 and gl_id=" & iGlid & " order by gl_desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ApproveCustomerCOA(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Customer_COA set CC_Status='A' where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSglCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iOrgId As Integer)
        Dim sSql As String
        Dim iCount As Integer = 0
        Try
            sSql = "select Count(*) from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " and CC_IndType=" & iOrgId & ""
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSglid(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal iOrgId As Integer, ByVal iSglid As Integer)
        Dim sSql As String
        Dim iCount As Integer = 0
        Try
            sSql = "select CC_ID from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " and CC_IndType=" & iOrgId & " and CC_Gl=" & iSglid & ""
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateCustomerCoaSgl(ByVal sAC As String, ByVal iACID As Integer, ByVal objUT As ClsUploadTailBal)
        Dim sSql As String = ""
        Try
            sSql = "Update Customer_COA set CC_GLdesc = '" & objclsGRACeGeneral.SafeSQL(objUT.sCC_GLDesc) & "',"
            sSql = sSql & "CC_IPAddress='" & objUT.sCC_IPAddress & "'"
            sSql = sSql & " where CC_id = " & objUT.iCC_ID & " and CC_IndType=" & objUT.iCC_IndType & " and CC_CustID=" & objUT.iCC_CustID & " and CC_CompID =" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveTrailBalanceExcelUpload(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsOpeningBalance As ClsUploadTailBal)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.ATBU_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_CODE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBU_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBU_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBU_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_Opening_Debit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dATBU_Opening_Debit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_Opening_Credit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dATBU_Opening_Credit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_TR_Debit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dATBU_TR_Debit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_TR_Credit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dATBU_TR_Credit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_Closing_Debit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dATBU_Closing_Debit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_Closing_Credit_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsOpeningBalance.dATBU_Closing_Credit_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBU_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBU_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBU_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBU_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBU_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBU_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_YEARId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBU_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBU_Branchid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBU_Branchname
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_TrailBalance_Upload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveTrailBalanceExcelUploaddetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsOpeningBalance As ClsUploadTailBal)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Masid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_Masid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_CODE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBUD_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBUD_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_SChedule_Type", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_SChedule_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Branchid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_Branchname
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Company_Type", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_Company_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Headingid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_Headingid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Subheading", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_Subheading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_itemid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_itemid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Subitemid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_Subitemid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBUD_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBUD_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_Progress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBUD_Progress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsOpeningBalance.sATBUD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATBUD_YEARId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsOpeningBalance.iATBUD_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_TrailBalance_Upload_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadItemsfromJE(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal ItemID As Integer, ByVal iYearID As Integer, ByVal iBranchID As Integer)
        Dim sSql, sSql1 As String
        Dim dt As New DataTable
        Try
            sSql = "select ROW_NUMBER() OVER (ORDER BY AJTB_ID ASC) AS SrNo,AJTB_ID as Itemid, AJTB_Credit as Credit, 
                    AJTB_Debit as Debit,convert(date, AJTB_CreatedOn) as TransactionDate, b.ATBU_Description as Description,'Journal Entry' as TrType from Acc_JETransactions_Details 
                    left join Acc_TrailBalance_Upload b on b.ATBU_ID=AJTB_Desc and b.ATBU_CustId = AJTB_CustId "
            sSql = sSql & "left join Acc_JE_Master c on c.Acc_JE_ID=Ajtb_Masid "
            sSql = sSql & " where Acc_JE_Status ='A' "
            If iBranchID >0 Then
                sSql = sSql & " And acc_JE_BranchId= " & iBranchID & ""
            End If
            sSql = sSql & "And AJTB_Desc =" & ItemID & " AND Acc_JE_Party=" & iCustId & " and Acc_JE_CompID=" & iACID & " And  Acc_JE_YearId=" & iYearID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItemsfromTB(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal ItemID As Integer, ByVal iYearID As Integer)
        Dim sSql, sSql1 As String
        Dim dt As New DataTable
        Try
            sSql1 = "select ROW_NUMBER() OVER (ORDER BY ATBU_ID ASC) AS SrNo, ATBU_ID as Itemid, ATBU_Closing_Credit_Amount as Credit, ATBU_TR_Debit_Amount as Debit,convert(date, ATBU_CRON ) as TransactionDate, ATBU_Description as Description,'Trailbalance' as TrType from Acc_TrailBalance_Upload"
            sSql1 = sSql1 & " where ATBU_ID = " & ItemID & " and ATBU_CustId = " & iCustId & " and ATBU_Compid = " & iACID & " And  ATBU_YEARId=" & iYearID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql1)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadItemsfromJECreditdebit(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal ItemID As Integer, ByVal CRORDb As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select AJTB_Credit as Credit, AJTB_Debit as Debit,convert(date,AJTB_CreatedOn) as TransactionDate from Acc_JETransactions_Details where AJTB_Desc =" & ItemID & " AND AJTB_CustId=" & iCustId & " and AJTB_compid=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count <> 0 Then
                If CRORDb = 0 Then
                    Return objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Credit"))
                ElseIf CRORDb = 1 Then
                    Return objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Debit"))
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMasid(ByVal sAC As String, ByVal iACID As Integer, ByVal IsheduleTypeId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select distinct(b.ASSI_Name),ASSI_ID from ACC_ScheduleTemplates 
                    left join ACC_ScheduleSubItems b on b.ASSI_ID = AST_SubItemID and ASSI_STATUS<>'D'"
            sSql = sSql & " where AST_CompId =" & iACID & " and AST_Schedule_type= " & IsheduleTypeId & " and b.ASSI_Name IS NOT NULL and ASSI_ID is NOT NULl"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateStatustrailbalance(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Customer_COA set CC_Status='A' where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Checkdata(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal sDesc As String, ByVal iYearID As Integer, ByVal IBranchid As Integer) As Integer
        Dim sSql As String
        Dim chkrec As Integer
        Try
            sSql = "select isnull(atbu_id,0) as atbu_id  from Acc_TrailBalance_Upload Where ATBU_Description='" & sDesc & "'"
            sSql = sSql & " And ATBU_CustId=" & iCustid & " And ATBU_Branchid=" & IBranchid & " And ATBU_CompId = " & iACID & " And ATBU_YEARId = " & iYearID & " "
            chkrec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return chkrec
            'If chkrec = True Then
            '    Return 1
            'Else
            '    Return 0
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCustdata(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal sDesc As String, ByVal iYearID As Integer, ByVal iBranchId As Integer) As Integer
        Dim sSql As String
        Dim chkrec As Integer
        Try
            sSql = "Select isnull(atbu_id,0) As atbu_id  from Acc_TrailBalance_Upload Where "
            sSql = sSql & "  ATBU_CustId=" & iCustid & " And ATBU_CompId=" & iACID & " And ATBU_YEARId=" & iYearID & " and atbu_branchid=" & iBranchId & " "
            chkrec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return chkrec
            'If chkrec = True Then
            '    Return 1
            'Else
            '    Return 0
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteCustdata(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal sDesc As String, ByVal iYearID As Integer, ByVal iBranchId As Integer)
        Dim sSql As String
        Try
            sSql = " delete  from Acc_TrailBalance_Upload Where "
            sSql = sSql & "  ATBU_CustId=" & iCustid & " And ATBU_CompId=" & iACID & " And ATBU_YEARId=" & iYearID & " and  atbu_branchid=" & iBranchId & " "
            objDBL.SQLExecuteDataSet(sAC, sSql)

            sSql = " delete  from Acc_TrailBalance_Upload_details Where "
            sSql = sSql & "  ATBUD_CustId=" & iCustid & " And ATBUD_CompId=" & iACID & " And ATBUD_YEARId=" & iYearID & "   and  atbud_branchnameid=" & iBranchId & " "
            objDBL.SQLExecuteDataSet(sAC, sSql)

            sSql = " delete  from Acc_JETransactions_Details Where "
            sSql = sSql & "  AJTB_CustId=" & iCustid & " And AJTB_CompId=" & iACID & " And AJTB_YEARId=" & iYearID & "  and  ajtb_branchid=" & iBranchId & " "
            objDBL.SQLExecuteDataSet(sAC, sSql)

            sSql = " delete  from Acc_JE_Master Where "
            sSql = sSql & "  Acc_JE_Party=" & iCustid & " And Acc_JE_CompID=" & iACID & " And Acc_JE_YearId=" & iYearID & "  and  acc_JE_BranchId=" & iBranchId & " "
            objDBL.SQLExecuteDataSet(sAC, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function UploadPrevdata(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = ""
            sSql = "Select * from Acc_TrailBalance_Upload_Details where ATBUD_CustId=" & iCustId & " And ATBUD_YEARId=" & iYearID - 1 & " And ATBUD_CompId=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                sSql = "Update Acc_TrailBalance_Upload_Details Set ATBUD_SChedule_Type=" & dt(i)("ATBUD_SChedule_Type") & ","
                sSql = sSql & " ATBUD_Headingid=" & dt.Rows(i)("ATBUD_Headingid") & ","
                sSql = sSql & " ATBUD_Subheading=" & dt.Rows(i)("ATBUD_Subheading") & ", ATBUD_itemid=" & dt.Rows(i)("ATBUD_itemid") & ","
                sSql = sSql & " ATBUD_SubItemId=" & dt.Rows(i)("ATBUD_SubItemId") & ""
                sSql = sSql & " where ATBUD_Description='" & dt.Rows(i)("ATBUD_Description") & "' And ATBUD_CustId=" & iCustId & " and ATBUD_CompId=" & iACID & " and ATBUD_YEARId=" & iYearID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            Next
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItemsfromJE(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select ROW_NUMBER() OVER (ORDER BY AJTB_ID ASC) AS SrNo,AJTB_ID as Itemid, AJTB_Credit as Credit, 
                    AJTB_Debit as Debit,convert(date, AJTB_CreatedOn) as TransactionDate, b.ATBU_Description as Description,'Journal Entry' as TrType from Acc_JETransactions_Details 
                    left join Acc_TrailBalance_Upload b on b.ATBU_ID=AJTB_Desc and b.ATBU_CustId = AJTB_CustId where AJTB_Status='A' And AJTB_CustId=" & iCustId & " and AJTB_compid=" & iACID & " And  AJTB_YearID=" & iYearID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
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
    Public Function getScheduleType(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal Sdesc As String)
        Dim sSql As String
        Dim ischeduleid As Integer = 0
        Try
            sSql = "select ATBUD_SChedule_Type from Acc_TrailBalance_Upload_Details where ATBUD_CustId = " & iCustId & " and ATBUD_Description='" & Sdesc & "' and ATBUD_YEARId=" & iYearID & " And ATBUD_CompId=" & iACID & ""
            ischeduleid = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ischeduleid
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPrevYrLinkageDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal IscheduleTypeid As Integer, ByVal Unmapped As Integer, ByVal sDesc As String, ByVal Ibranchid As Integer)
        Dim sSql, sSql1 As String, sSqlParent As String
        Dim dt, dtJeDet As New DataTable, dtParent As New DataTable, dtMerge As New DataTable
        Try
            sSql = sSql & " select ISNULL(b.ATBUD_SubItemId,0) as subItemID,b.ATBUD_SChedule_Type, ASSI_Name,ISNULL(b.atbud_itemid,0) as itemid,ASI_Name, "
            sSql = sSql & " ISNULL(b.atbud_subheading,0) as subheadingid ,ASSH_Name,ISNULL(b.atbud_headingid,0) as headingid, "
            sSql = sSql & " ASH_Name,b.atbud_progress as Status "
            sSql = sSql & "  From Acc_TrailBalance_Upload"
            sSql = sSql & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Description = ATBU_Description"
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_SChedule_Type=" & IscheduleTypeid & " And b.ATBUD_Headingid=0 "
            Else
            End If
            sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid"
            sSql = sSql & " left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading"
            sSql = sSql & " left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid"
            sSql = sSql & " left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
            sSql = sSql & " where ATBUD_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBUd_YEARId=" & iYearID & " and ATBU_Description='" & sDesc & "'"
            If Ibranchid > 0 Then
                sSql = sSql & " And Atbu_Branchid=" & Ibranchid & ""
            End If

            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_ID Is Not NULL"
            End If
            If Unmapped <> 0 Then
                sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
            End If
            sSql = sSql & " order by ATBU_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getGroupidfromAlias(ByVal sAC As String, ByVal iACID As Integer, ByVal iOrgtype As Integer, ByVal iGrplvl As Integer, ByVal Sdesc As String)
        Dim sSql As String
        Dim Dtgroup As New DataTable
        Try
            sSql = "select isnull(AGA_GLID,0) as ID,AGA_GrpLevel as Level from Acc_GroupingAlias where AGA_Description='" & Sdesc & "' and AGA_Orgtype='" & iOrgtype & "' And AGA_Compid=" & iACID & ""
            Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
            If Dtgroup.Rows.Count = 0 Then
                If iGrplvl = 4 Then
                    'Item Check
                    sSql = "select isnull(ASI_ID,0) as ID,3 as Level from ACC_ScheduleItems where ASI_Name='" & Sdesc & "' and ASI_Orgtype='" & iOrgtype & "' And ASI_Compid=" & iACID & ""
                    Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If Dtgroup.Rows.Count = 0 Then
                        'subheading check 
                        sSql = "select isnull(ASSH_ID,0) as ID,2 as Level from ACC_ScheduleSubHeading where ASSH_Name='" & Sdesc & "' and ASSH_Orgtype='" & iOrgtype & "' And ASSH_Compid=" & iACID & ""
                        Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If Dtgroup.Rows.Count = 0 Then
                            'Heading CHeck
                            sSql = "select isnull(ASH_ID,0) as ID,1 As Level from ACC_ScheduleHeading where ASH_Name='" & Sdesc & "' And ASH_Orgtype='" & iOrgtype & "' And ASH_Compid=" & iACID & ""
                            Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
                        End If
                    End If
                    'Sub heading check
                ElseIf iGrplvl = 3 Then

                    If Dtgroup.Rows.Count = 0 Then
                        'subheading check 
                        sSql = "select isnull(ASSH_ID,0) as ID,2 as Level from ACC_ScheduleSubHeading where ASSH_Name='" & Sdesc & "' and ASSH_Orgtype='" & iOrgtype & "' And ASSH_Compid=" & iACID & ""
                        Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If Dtgroup.Rows.Count = 0 Then
                            'Heading CHeck
                            sSql = "select isnull(ASH_ID,0) as ID,1 As Level from ACC_ScheduleHeading where ASH_Name='" & Sdesc & "' And ASH_Orgtype='" & iOrgtype & "' And ASH_Compid=" & iACID & ""
                            Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
                        End If
                    End If

                    ' Heading Check
                ElseIf iGrplvl = 2 Then
                    'Heading CHeck
                    If Dtgroup.Rows.Count = 0 Then
                        'Heading CHeck
                        sSql = "select isnull(ASH_ID,0) as ID,1 As Level from ACC_ScheduleHeading where ASH_Name='" & Sdesc & "' And ASH_Orgtype='" & iOrgtype & "' And ASH_Compid=" & iACID & ""
                        Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
                    End If

                ElseIf iGrplvl = 1 Then
                    ' Nothing
                    sSql = "select isnull(ASH_ID,0) as ID,1 As Level from ACC_ScheduleHeading where ASH_Name='" & Sdesc & "' And ASH_Orgtype='" & iOrgtype & "' And ASH_Compid=" & iACID & ""
                    Dtgroup = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
            End If
            Return Dtgroup
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function getGroupidfromAlias(ByVal sAC As String, ByVal iACID As Integer, ByVal iOrgtype As Integer, ByVal iGrplvl As Integer, ByVal Sdesc As String)
    '    Dim sSql As String
    '    Dim iGroupid As Integer = 0
    '    Try
    '        sSql = "select isnull(AGA_GLID,0) from Acc_GroupingAlias where AGA_Description='" & Sdesc & "'  and AGA_Orgtype='" & iOrgtype & "' And AGA_Compid=" & iACID & ""
    '        If iGrplvl <> 0 Then
    '            sSql = sSql & " And AGA_GrpLevel=" & iGrplvl & ""
    '        End If
    '        iGroupid = objDBL.SQLExecuteScalarInt(sAC, sSql)
    '        Return iGroupid
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetMappedLedgerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iOrgType As Integer, ByVal iID As Integer, ByVal iLevelId As Integer)
        Dim sSql As String = "", sSql1 As String = "", sSqlParent As String = ""
        Dim dt, dtJeDet As New DataTable, dtParent As New DataTable, dtMerge As New DataTable
        Try
            If iID <> 0 Then
                If iLevelId = 4 Then
                    sSql = sSql & " select AST_HeadingID,a.ASH_ID,a.ASH_Name,isnull(AST_Schedule_type,0) as AST_Schedule_type,
                    ISNULL(b.ASSH_ID,0) as ASSH_ID,ISNULL(b.ASSH_Name,'')as ASSH_Name,ISNULL(c.ASI_ID,0) as ASI_ID,ISNULL(c.ASI_Name,'') as ASI_Name,
                    ISNULL(d.ASSI_ID,0) as ASSi_ID,ISNULL(d.ASSI_Name,'') as ASSI_Name from ACC_ScheduleTemplates
                    left join ACC_ScheduleSubItems d on d.ASSI_ID= AST_SubItemID
                    left join ACC_ScheduleItems c on c.ASI_ID= AST_ItemID
                    left join ACC_ScheduleSubHeading b on b.ASSH_ID=AST_SubHeadingID  
                    left join ACC_ScheduleHeading a on a.ASH_ID=AST_HeadingID   
                    where AST_SubItemID= " & iID & " and AST_Companytype=" & iOrgType & " "
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                ElseIf iLevelId = 3 Then
                    sSql = sSql & " select AST_HeadingID,a.ASH_ID,a.ASH_Name,isnull(AST_Schedule_type,0) as AST_Schedule_type,
                    ISNULL(b.ASSH_ID,0) as ASSH_ID,ISNULL(b.ASSH_Name,'')as ASSH_Name,ISNULL(c.ASI_ID,0) as ASI_ID,ISNULL(c.ASI_Name,'') as ASI_Name,
                    0 as ASSi_ID,('') as ASSI_Name from ACC_ScheduleTemplates
                    left join ACC_ScheduleSubItems d on d.ASSI_ID= AST_SubItemID
                    left join ACC_ScheduleItems c on c.ASI_ID= AST_ItemID
                    left join ACC_ScheduleSubHeading b on b.ASSH_ID=AST_SubHeadingID  
                    left join ACC_ScheduleHeading a on a.ASH_ID=AST_HeadingID  
                    where AST_ItemID= " & iID & " and AST_Companytype=" & iOrgType & " and AST_SubItemID=0"
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                ElseIf iLevelId = 2 Then
                    sSql = sSql & " select AST_HeadingID,a.ASH_ID,a.ASH_Name,isnull(AST_Schedule_type,0) as AST_Schedule_type,
                    ISNULL(b.ASSH_ID,0) as ASSH_ID,ISNULL(b.ASSH_Name,'')as ASSH_Name,(0) as ASI_ID,('') as ASI_Name,
                    (0) as ASSi_ID,('') as ASSI_Name from ACC_ScheduleTemplates
                    left join ACC_ScheduleSubItems d on d.ASSI_ID= AST_SubItemID
                    left join ACC_ScheduleItems c on c.ASI_ID= AST_ItemID
                    left join ACC_ScheduleSubHeading b on b.ASSH_ID=AST_SubHeadingID  
                    left join ACC_ScheduleHeading a on a.ASH_ID=AST_HeadingID        
                      where AST_SubHeadingID= " & iID & " and AST_Companytype=" & iOrgType & ""
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                ElseIf iLevelId = 1 Then
                    sSql = sSql & " select AST_HeadingID,a.ASH_ID,a.ASH_Name,isnull(AST_Schedule_type,0) as AST_Schedule_type,
                    (0) as ASSH_ID,('')as ASSH_Name,(0) as ASI_ID,('') as ASI_Name,
                    (0) as ASSi_ID,('') as ASSI_Name from ACC_ScheduleTemplates
                    left join ACC_ScheduleSubItems d on d.ASSI_ID= AST_SubItemID
                    left join ACC_ScheduleItems c on c.ASI_ID= AST_ItemID
                    left join ACC_ScheduleSubHeading b on b.ASSH_ID=AST_SubHeadingID  
                    left join ACC_ScheduleHeading a on a.ASH_ID=AST_HeadingID      
                    where AST_HeadingID= " & iID & " and AST_Companytype=" & iOrgType & ""
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
            End If
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
    Public Function getDescmaxid(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal sDesc As String, ByVal iYearID As Integer, ByVal iBranchId As Integer) As Integer
        Dim sSql As String
        Dim iCount As Integer = 0
        Try
            sSql = "Select count(*) from Acc_TrailBalance_Upload Where"
            sSql = sSql & " ATBU_CustId=" & iCustid & " and ATBU_CompId=" & iACID & " And ATBU_YEARId=" & iYearID & " and atbu_branchId=" & iBranchId & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetaileddata(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal sDesc As String, ByVal iYearID As Integer, ByVal iBranchid As Integer) As Integer
        Dim sSql As String
        Dim chkrec As Integer
        Try
            sSql = "select isnull(atbud_id,0) as atbud_id  from Acc_TrailBalance_Upload_Details Where ATBUD_Description='" & sDesc & "'"
            sSql = sSql & " And ATBUD_CustId=" & iCustid & " and ATBUD_CompId=" & iACID & " And ATBUD_YEARId=" & iYearID & " And Atbud_Branchnameid=" & iBranchid & ""
            chkrec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return chkrec
            'If chkrec = True Then
            '    Return 1
            'Else
            '    Return 0
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOrgtype(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Dim sOrgtype As String

        Try
            sSql = "SELECT cmm_Desc FROM SAD_CUSTOMER_MASTER LEFT JOIN Content_Management_Master ON Content_Management_Master.cmm_id = SAD_CUSTOMER_MASTER .CUST_ORGTYPEID WHERE SAD_CUSTOMER_MASTER.CUST_ID =" & iCustid & ""
            sOrgtype = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sOrgtype

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckVersionId(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer, ByVal IBranchid As Integer) As Integer
        Dim sSql As String
        Dim chkrec As Integer
        Try
            sSql = "select isnull(max(ATBM_ID)+1,1) as atbm_id  from Acc_TBVersion_Master  "
            '   sSql = sSql & " where ATBM_CustId=" & iCustid & " and ATBM_Branchid='" & IBranchid & "' and ATBM_Yearid=" & iYearID & ""
            chkrec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return chkrec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckVersion(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer, ByVal IBranchid As Integer) As Integer
        Dim sSql As String
        Dim chkrec As Integer
        Try
            sSql = "select max(ATBM_ID) as atbm_id  from Acc_TBVersion_Master  "
            sSql = sSql & " where ATBM_CustId=" & iCustid & " and ATBM_Branchid='" & IBranchid & "' and ATBM_Yearid=" & iYearID & ""
            chkrec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return chkrec
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function SaveVersionMaster(ByVal sAc As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iUserid As Integer, ByVal iYearid As Integer, ByVal sBranches As Integer) As Boolean
        Dim sSql As String = ""
        Dim Imaxid As Integer
        Dim sVersionNo As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select isnull(max(ATBM_ID)+1,1) from Acc_TBVersion_Master "
            Imaxid = objDBL.SQLExecuteScalarInt(sAc, sSql)
            sSql = "select isnull(count(ATBM_ID)+1,1) from Acc_TBVersion_Master where ATBM_CustId=" & iCustid & " and ATBM_Branchid='" & sBranches & "' and ATBM_Yearid=" & iYearid & " "
            sVersionNo = objDBL.SQLExecuteScalarInt(sAc, sSql)
            If Val(sVersionNo) = "0" Then
                sVersionNo = 1
            End If
            sVersionNo = "Version-" & sVersionNo
            sSql = "" : sSql = "Insert into Acc_TBVersion_Master(ATBM_ID,ATBM_CustId,ATBM_Branchid,ATBM_Yearid,ATBM_VersionNo,ATBM_CRON,ATBM_CRBY)"
            sSql = sSql & " Values(" & Imaxid & "," & iCustid & ",'" & sBranches & "'," & iYearid & ", '" & sVersionNo & "',"
            sSql = sSql & "GETDATE()," & iUserid & ")"
            objDBL.SQLExecuteNonQuery(sAc, sSql)
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTotal(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer, ByVal sBranchid As String) As DataSet
        Dim sSql As String = ""
        Dim Imaxid As Integer = 0

        Try
            sSql = "SELECT CASE ATU_OtherType WHEN 1 THEN 'MSME' WHEN 2 THEN 'Others' WHEN 3 THEN 'Dispute dues-MSME' WHEN 4 THEN 'Dispute dues' WHEN 5 THEN 'Others' ELSE NULL END AS Name,"
            sSql &= " SUM(ATU_More_than_six_Month) AS Total_More_than_six_Month, SUM(ATU_Less_than_six_Month) AS Total_Less_than_six_Month,"
            sSql &= " SUM(ATU_One_Year) AS Total_One_Year, SUM(ATU_Two_Year) AS Total_Two_Year, SUM(ATU_Three_Year) AS Total_Three_Year,"
            sSql &= " SUM(ATU_More_than) AS Total_More_than, SUM(ATU_Total_Amount) AS Total_Amount, ATU_Category"
            sSql &= " FROM Acc_Trade_Upload"
            sSql &= " WHERE ATU_YEARId = " & iYearID & " AND ATU_Branchid in (" & sBranchid & ")"
            sSql &= " AND ATU_CustId = " & iCustid & " AND ATU_category = 1 AND ATU_OtherType IN (1,2,3,4,5)"
            sSql &= " GROUP BY ATU_Category, ATU_OtherType;"


            sSql &= "SELECT CASE ATU_OtherType WHEN 1 THEN 'MSME' WHEN 2 THEN 'Others' WHEN 3 THEN 'Dispute dues-MSME' WHEN 4 THEN 'Dispute dues' WHEN 5 THEN 'Others' ELSE NULL END AS Name,"
            sSql &= " SUM(ATU_More_than_six_Month) AS Total_More_than_six_Month, SUM(ATU_Less_than_six_Month) AS Total_Less_than_six_Month,"
            sSql &= " SUM(ATU_One_Year) AS Total_One_Year, SUM(ATU_Two_Year) AS Total_Two_Year, SUM(ATU_Three_Year) AS Total_Three_Year,"
            sSql &= " SUM(ATU_More_than) AS Total_More_than, SUM(ATU_Total_Amount) AS Total_Amount, ATU_Category "
            sSql &= " FROM Acc_Trade_Upload"
            sSql &= " WHERE ATU_YEARId = " & iYearID & " AND ATU_Branchid in (" & sBranchid & ")"
            sSql &= " AND ATU_CustId = " & iCustid & " AND ATU_category = 2 AND ATU_OtherType IN (1,2,3,4,5)"
            sSql &= " GROUP BY ATU_Category, ATU_OtherType;"
            Dim dt2 As DataSet = objDBL.SQLExecuteDataSet(sAC, sSql)

            Return dt2
        Catch ex As Exception

        End Try
    End Function

    Public Function GetCustCOAMasterDetailsDetailed(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer, ByVal IscheduleTypeid As Integer, ByVal Unmapped As Integer, ByVal Ibranchid As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataSet
        Try
            'Changed qry for avoiding JE Loop (Qry Optimization) (22_06_23)
            ''sSql = "select CC_GL from Customer_COA where CC_CustId=" & iCustId & " and CC_Compid=" & iACID & " and CC_YearID=" & iYearID & " order by cc_gl"
            'sSql = "select  ROW_NUMBER() OVER (ORDER BY ATBU_ID ASC) AS SrNo, b.ATBUD_ID as DescID, Atbu_id as DescDetailsID,ATBU_code as DescriptionCode,ATBU_CustId, ATBU_Description as Description, ATBU_CustId, ATBU_Description as Description, CAST(ATBU_Opening_Debit_Amount AS DECIMAL(19, 2))"
            'sSql = sSql & " as OpeningDebit, CAST(ATBU_Opening_Credit_Amount AS DECIMAL(19, 2))  as OpeningCredit,"
            'sSql = sSql & " CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebit,CAST (ATBU_TR_Credit_Amount as DECIMAL(19,2)) as TrCredit,  CAST(ATBU_Closing_TotalDebit_Amount AS DECIMAL(19, 2))  As ClosingDebit,"
            'sSql = sSql & " CAST(ATBU_Closing_TotalCredit_Amount AS DECIMAL(19, 2))   As ClosingCredit,"
            'sSql = sSql & " ISNULL(b.ATBUD_SubItemId,0) as subItemID, ASSI_Name,ISNULL(b.atbud_itemid,0) as itemid,ASI_Name, "
            'sSql = sSql & " ISNULL(b.atbud_subheading,0) as subheadingid ,ASSH_Name,ISNULL(b.atbud_headingid,0) as headingid, "
            'sSql = sSql & " ASH_Name,b.atbud_progress as Status,b.Atbud_Company_type as Companytype,"
            'sSql = sSql & " ATBUD_SChedule_Type as ScheduleType, CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebittrUploaded,CAST (ATBU_TR_Credit_Amount as decimal(19,2)) as TrCredittrUploaded  From Acc_TrailBalance_Upload"
            'sSql = sSql & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Description = ATBU_Description and b.ATBUD_CustId=" & iCustId & " and b.ATBUD_YEARId=" & iYearID & ""
            'If Ibranchid > 0 Then
            '    sSql = sSql & " and b.Atbud_Branchnameid=" & Ibranchid & ""
            'End If
            'If IscheduleTypeid <> 0 Then
            '    sSql = sSql & " And b.ATBUD_SChedule_Type=" & IscheduleTypeid & " And b.ATBUD_Headingid=0 "
            'Else
            'End If
            'sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid"
            'sSql = sSql & " left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading"
            'sSql = sSql & " left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid"
            'sSql = sSql & " left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
            'sSql = sSql & " where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId =" & iYearID & ""
            'If Ibranchid > 0 Then
            '    sSql = sSql & " and ATBU_Branchid=" & Ibranchid & ""
            'End If
            'If IscheduleTypeid <> 0 Then
            '    sSql = sSql & " And b.ATBUD_ID Is Not NULL"
            'End If
            'If Unmapped <> 0 Then
            '    sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
            'End If
            'sSql = sSql & " order by ATBU_ID"
            'dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            'If dt.Rows.Count > 0 Then
            '    For i = 0 To dt.Rows.Count - 1
            '        sSql = "select ISNULL(sum(AJTB_Debit),0) as AJTB_Debit,ISNULL(sum(AJTB_Credit),0) as AJTB_Credit from Acc_JETransactions_Details "
            '        sSql = sSql & " left join Acc_JE_Master a on a.Acc_JE_ID=Ajtb_Masid"
            '        sSql = sSql & " where Acc_JE_Status='A' And AJTB_DescName='" & dt.Rows(i)("Description") & "' And Acc_JE_Party=" & iCustId & " And Acc_JE_YearID=" & iYearID & " And acc_JE_BranchId=" & Ibranchid & ""
            '        dtJeDet = objDBL.SQLExecuteDataTable(sAC, sSql)
            '        If dtJeDet.Rows.Count > 0 Then
            '            dt.Rows(i)("TrDebit") = dt.Rows(i)("TrDebit") + dtJeDet.Rows(0)("AJTB_Debit")
            '            dt.Rows(i)("TrCredit") = dt.Rows(i)("TrCredit") + dtJeDet.Rows(0)("AJTB_Credit")
            '        End If
            '    Next
            'End If

            sSql = "select  ROW_NUMBER() OVER (ORDER BY ATBU_ID ASC) AS SrNo, b.ATBUD_ID as DescID, Atbu_id as DescDetailsID,ATBU_code as DescriptionCode,ATBU_CustId, ATBU_Description as Description, ATBU_CustId, ATBU_Description as Description, CAST(ATBU_Opening_Debit_Amount AS DECIMAL(19, 2))"
            sSql = sSql & " as OpeningDebit, CAST(ATBU_Opening_Credit_Amount AS DECIMAL(19, 2))  as OpeningCredit,"
            sSql = sSql & " CAST(sum(ATBU_TR_Debit_Amount+ isnull(g.AJTB_Debit,0)) AS DECIMAL(19, 2)) as TrDebit,CAST (sum(ATBU_TR_Credit_Amount+ isnull(h.AJTB_Credit,0)) as DECIMAL(19,2)) as TrCredit,  CAST(ATBU_Closing_TotalDebit_Amount AS DECIMAL(19, 2))  As ClosingDebit,"
            sSql = sSql & " CAST(ATBU_Closing_TotalCredit_Amount AS DECIMAL(19, 2))   As ClosingCredit,"
            sSql = sSql & " ISNULL(b.ATBUD_SubItemId,0) as subItemID, ASSI_Name,ISNULL(b.atbud_itemid,0) as itemid,ASI_Name, "
            sSql = sSql & " ISNULL(b.atbud_subheading,0) as subheadingid ,ASSH_Name,ISNULL(b.atbud_headingid,0) as headingid, "
            sSql = sSql & " ASH_Name,b.atbud_progress as Status,b.Atbud_Company_type as Companytype,"
            sSql = sSql & " ATBUD_SChedule_Type as ScheduleType, CAST(ATBU_TR_Debit_Amount AS DECIMAL(19, 2)) as TrDebittrUploaded,CAST (ATBU_TR_Credit_Amount as decimal(19,2)) as TrCredittrUploaded  From Acc_TrailBalance_Upload "
            sSql = sSql & " left join Acc_TrailBalance_Upload_details b on b.ATBUD_Description = ATBU_Description and b.ATBUD_CustId=" & iCustId & " and b.ATBUD_YEARId=" & iYearID & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and b.Atbud_Branchnameid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_SChedule_Type=" & IscheduleTypeid & " And b.ATBUD_Headingid=0 "
            Else
            End If
            sSql = sSql & " left join ACC_ScheduleHeading c on c.ASH_ID=ATBUD_Headingid"
            sSql = sSql & " left join ACC_ScheduleSubHeading d on d.ASSH_ID=ATBUD_Subheading"
            sSql = sSql & " left join ACC_ScheduleItems e on e.ASI_ID=ATBUD_itemid"
            sSql = sSql & " left join ACC_ScheduleSubItems f on f.ASSI_ID= ATBUD_SubItemId"
            sSql = sSql & " left join Acc_JETransactions_Details g on g.AJTB_DescName= ATBU_Description and g.AJTB_Status='A' and g.AJTB_CustId=" & iCustId & " and g.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and g.ajtb_BranchId=" & Ibranchid & " and g.AJTB_Credit =0 "
            End If
            sSql = sSql & " left join Acc_JETransactions_Details h on h.AJTB_DescName= ATBU_Description and h.AJTB_Status='A' and h.AJTB_CustId=" & iCustId & " and h.AJTB_YearID=" & iYearID & " "
            If Ibranchid > 0 Then
                sSql = sSql & " and h.ajtb_BranchId=" & Ibranchid & " and h.AJTB_Debit=0 "
            End If
            sSql = sSql & " where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId =" & iYearID & ""
            If Ibranchid > 0 Then
                sSql = sSql & " and ATBU_Branchid=" & Ibranchid & ""
            End If
            If IscheduleTypeid <> 0 Then
                sSql = sSql & " And b.ATBUD_ID Is Not NULL"
            End If
            If Unmapped <> 0 Then
                sSql = sSql & " And ATBUD_Headingid=0 ANd ATBUD_Subheading=0 And ATBUD_itemid=0 And ATBUD_SubItemId=0"
            End If
            sSql = sSql & " group by b.ATBUD_ID , Atbu_id ,ATBU_code ,ATBU_CustId,ATBU_Description , ATBU_CustId, ATBU_Description,"
            sSql = sSql & " ATBU_Opening_Debit_Amount,ATBU_Opening_Credit_Amount,  ATBU_TR_Debit_Amount,ATBU_TR_Credit_Amount "
            sSql = sSql & " ,ATBU_Closing_TotalDebit_Amount,ATBU_Closing_TotalCredit_Amount,ATBUD_SubItemId,atbud_itemid,ASI_Name,"
            sSql = sSql & " atbud_subheading,ASSH_Name,atbud_headingid,ASH_Name,atbud_progress, "
            sSql = sSql & " Atbud_Company_type,ATBUD_SChedule_Type,ATBU_TR_Debit_Amount,ATBU_TR_Credit_Amount,ASSI_Name "
            sSql = sSql & " order by ATBU_ID ;"

            sSql += "  select sum(ATBU_Opening_Debit_Amount) as OpeningDebit,sum(ATBU_Opening_Credit_Amount) as OpeningCredit, "
            sSql = sSql & " sum(ATBU_TR_Debit_Amount+ ISNULL(AJTB_debit,0)) as TrDebit,sum(ATBU_TR_Credit_Amount+ ISNULL(AJTB_Credit,0)) as TrCredit , "
            sSql = sSql & " sum(ATBU_Closing_TotalCredit_Amount) as  ClosingCredit, sum(ATBU_Closing_Totaldebit_Amount) as  Closingdebit "
            sSql = sSql & " From Acc_TrailBalance_Upload  "
            sSql = sSql & " left join Acc_JETransactions_Details g on g.AJTB_DescName= ATBU_Description and g.AJTB_Status='A' and g.AJTB_CustId=" & iCustId & " and g.AJTB_YearID=" & iYearID & " "
            sSql = sSql & " and g.ajtb_BranchId=4 where ATBU_CustId=" & iCustId & " And ATBU_compid=" & iACID & " And  ATBU_YEARId =" & iYearID & "  "
            If Ibranchid > 0 Then
                sSql = sSql & " and ATBU_Branchid=" & Ibranchid & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
