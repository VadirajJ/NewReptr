Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Structure strCompanyDetails
    Private Company_ID As Integer
    Private Company_Code As String
    Private Company_Name As String
    Private Company_Address As String
    Private Company_City As String
    Private Company_State As String
    Private Company_Country As String
    Private Company_PinCode As String
    Private Company_EmailID As String
    Private Company_Establishment_Date As String
    Private Company_ContactPerson As String
    Private Company_MobileNo As String
    Private Company_ContactEmailID As String
    Private Company_TelephoneNo As String
    Private Company_Status As String
    Private Company_CrBy As Integer
    Private Company_UpdatedBy As Integer
    Private Company_IPAddress As String
    Private Company_CompID As Integer
    Private Company_WebSite As String
    Private Company_ContactNo1 As String
    Private Company_ContactNo2 As String
    Private Company_HolderName As String
    Private Company_AccountNo As String
    Private Company_Bankname As String
    Private Company_Branch As String
    Private Company_Conditions As String
    Private Company_Paymentterms As String
    Public Property iCompany_ID() As Integer
        Get
            Return (Company_ID)
        End Get
        Set(ByVal Value As Integer)
            Company_ID = Value
        End Set
    End Property
    Public Property sCompany_WebSite() As String
        Get
            Return (Company_WebSite)
        End Get
        Set(ByVal Value As String)
            Company_WebSite = Value
        End Set
    End Property
    Public Property sCompany_ContactNo1() As String
        Get
            Return (Company_ContactNo1)
        End Get
        Set(ByVal Value As String)
            Company_ContactNo1 = Value
        End Set
    End Property
    Public Property sCompany_ContactNo2() As String
        Get
            Return (Company_ContactNo2)
        End Get
        Set(ByVal Value As String)
            Company_ContactNo2 = Value
        End Set
    End Property
    Public Property sCompany_Code() As String
        Get
            Return (Company_Code)
        End Get
        Set(ByVal Value As String)
            Company_Code = Value
        End Set
    End Property
    Public Property sCompany_Name() As String
        Get
            Return (Company_Name)
        End Get
        Set(ByVal Value As String)
            Company_Name = Value
        End Set
    End Property
    Public Property sCompany_Address() As String
        Get
            Return (Company_Address)
        End Get
        Set(ByVal Value As String)
            Company_Address = Value
        End Set
    End Property
    Public Property sCompany_City() As String
        Get
            Return (Company_City)
        End Get
        Set(ByVal Value As String)
            Company_City = Value
        End Set
    End Property
    Public Property sCompany_State() As String
        Get
            Return (Company_State)
        End Get
        Set(ByVal Value As String)
            Company_State = Value
        End Set
    End Property
    Public Property sCompany_Country() As String
        Get
            Return (Company_Country)
        End Get
        Set(ByVal Value As String)
            Company_Country = Value
        End Set
    End Property
    Public Property sCompany_PinCode() As String
        Get
            Return (Company_PinCode)
        End Get
        Set(ByVal Value As String)
            Company_PinCode = Value
        End Set
    End Property
    Public Property sCompany_EmailID() As String
        Get
            Return (Company_EmailID)
        End Get
        Set(ByVal Value As String)
            Company_EmailID = Value
        End Set
    End Property
    Public Property sCompany_Establishment_Date() As String
        Get
            Return (Company_Establishment_Date)
        End Get
        Set(ByVal Value As String)
            Company_Establishment_Date = Value
        End Set
    End Property
    Public Property sCompany_ContactPerson() As String
        Get
            Return (Company_ContactPerson)
        End Get
        Set(ByVal Value As String)
            Company_ContactPerson = Value
        End Set
    End Property
    Public Property sCompany_MobileNo() As String
        Get
            Return (Company_MobileNo)
        End Get
        Set(ByVal Value As String)
            Company_MobileNo = Value
        End Set
    End Property
    Public Property sCompany_ContactEmailID() As String
        Get
            Return (Company_ContactEmailID)
        End Get
        Set(ByVal Value As String)
            Company_ContactEmailID = Value
        End Set
    End Property
    Public Property sCompany_TelephoneNo() As String
        Get
            Return (Company_TelephoneNo)
        End Get
        Set(ByVal Value As String)
            Company_TelephoneNo = Value
        End Set
    End Property
    Public Property sCompany_Status() As String
        Get
            Return (Company_Status)
        End Get
        Set(ByVal Value As String)
            Company_Status = Value
        End Set
    End Property
    Public Property iCompany_CrBy() As Integer
        Get
            Return (Company_CrBy)
        End Get
        Set(ByVal Value As Integer)
            Company_CrBy = Value
        End Set
    End Property
    Public Property iCompany_UpdatedBy() As Integer
        Get
            Return (Company_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Company_UpdatedBy = Value
        End Set
    End Property
    Public Property sCompany_IPAddress() As String
        Get
            Return (Company_IPAddress)
        End Get
        Set(ByVal Value As String)
            Company_IPAddress = Value
        End Set
    End Property
    Public Property iCompany_CompID() As Integer
        Get
            Return (Company_CompID)
        End Get
        Set(ByVal Value As Integer)
            Company_CompID = Value
        End Set
    End Property
    Public Property sCompany_HolderName() As String
        Get
            Return (Company_HolderName)
        End Get
        Set(ByVal Value As String)
            Company_HolderName = Value
        End Set
    End Property
    Public Property sCompany_AccountNo() As String
        Get
            Return (Company_AccountNo)
        End Get
        Set(ByVal Value As String)
            Company_AccountNo = Value
        End Set
    End Property
    Public Property sCompany_Bankname() As String
        Get
            Return (Company_Bankname)
        End Get
        Set(ByVal Value As String)
            Company_Bankname = Value
        End Set
    End Property
    Public Property sCompany_Branch() As String
        Get
            Return (Company_Branch)
        End Get
        Set(ByVal Value As String)
            Company_Branch = Value
        End Set
    End Property
    Public Property sCompany_Conditions() As String
        Get
            Return (Company_Conditions)
        End Get
        Set(ByVal Value As String)
            Company_Conditions = Value
        End Set
    End Property
    Public Property sCompany_Paymentterms() As String
        Get
            Return (Company_Paymentterms)
        End Get
        Set(ByVal Value As String)
            Company_Paymentterms = Value
        End Set
    End Property
End Structure

Public Structure strCompanyBranch
    Private Company_Branch_Id As Integer
    Private Company_Branch_CompanyID As Integer
    Private Company_Branch_Name As String
    Private Company_Branch_Address As String
    Private Company_Branch_Contact_Person As String
    Private Company_Branch_Contact_MobileNo As String
    Private Company_Branch_Contact_LandLineNo As String
    Private Company_Branch_Contact_Email As String
    Private Company_Branch_Designation As String
    Private Company_Branch_DelFlag As String
    Private Company_Branch_STATUS As String
    Private Company_Branch_CRBY As Integer
    Private Company_Branch_UpdatedBy As Integer
    Private Company_Branch_IPAddress As String
    Private Company_Branch_CompID As Integer
    Public Property iCompany_Branch_Id() As Integer
        Get
            Return (Company_Branch_Id)
        End Get
        Set(ByVal Value As Integer)
            Company_Branch_Id = Value
        End Set
    End Property
    Public Property iCompany_Branch_CompanyID() As Integer
        Get
            Return (Company_Branch_CompanyID)
        End Get
        Set(ByVal Value As Integer)
            Company_Branch_CompanyID = Value
        End Set
    End Property
    Public Property sCompany_Branch_Name() As String
        Get
            Return (Company_Branch_Name)
        End Get
        Set(ByVal Value As String)
            Company_Branch_Name = Value
        End Set
    End Property
    Public Property sCompany_Branch_Address() As String
        Get
            Return (Company_Branch_Address)
        End Get
        Set(ByVal Value As String)
            Company_Branch_Address = Value
        End Set
    End Property
    Public Property sCompany_Branch_Contact_Person() As String
        Get
            Return (Company_Branch_Contact_Person)
        End Get
        Set(ByVal Value As String)
            Company_Branch_Contact_Person = Value
        End Set
    End Property
    Public Property sCompany_Branch_Contact_MobileNo() As String
        Get
            Return (Company_Branch_Contact_MobileNo)
        End Get
        Set(ByVal Value As String)
            Company_Branch_Contact_MobileNo = Value
        End Set
    End Property
    Public Property sCompany_Branch_Contact_LandLineNo() As String
        Get
            Return (Company_Branch_Contact_LandLineNo)
        End Get
        Set(ByVal Value As String)
            Company_Branch_Contact_LandLineNo = Value
        End Set
    End Property
    Public Property sCompany_Branch_Contact_Email() As String
        Get
            Return (Company_Branch_Contact_Email)
        End Get
        Set(ByVal Value As String)
            Company_Branch_Contact_Email = Value
        End Set
    End Property
    Public Property sCompany_Branch_Designation() As String
        Get
            Return (Company_Branch_Designation)
        End Get
        Set(ByVal Value As String)
            Company_Branch_Designation = Value
        End Set
    End Property
    Public Property sCompany_Branch_DelFlag() As String
        Get
            Return (Company_Branch_DelFlag)
        End Get
        Set(ByVal Value As String)
            Company_Branch_DelFlag = Value
        End Set
    End Property
    Public Property iCompany_Branch_CRBY() As Integer
        Get
            Return (Company_Branch_CRBY)
        End Get
        Set(ByVal Value As Integer)
            Company_Branch_CRBY = Value
        End Set
    End Property
    Public Property iCompany_Branch_UpdatedBy() As Integer
        Get
            Return (Company_Branch_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Company_Branch_UpdatedBy = Value
        End Set
    End Property
    Public Property sCompany_Branch_STATUS() As String
        Get
            Return (Company_Branch_STATUS)
        End Get
        Set(ByVal Value As String)
            Company_Branch_STATUS = Value
        End Set
    End Property
    Public Property sCompany_Branch_IPAddress() As String
        Get
            Return (Company_Branch_IPAddress)
        End Get
        Set(ByVal Value As String)
            Company_Branch_IPAddress = Value
        End Set
    End Property
    Public Property iCompany_Branch_CompID() As Integer
        Get
            Return (Company_Branch_CompID)
        End Get
        Set(ByVal Value As Integer)
            Company_Branch_CompID = Value
        End Set
    End Property
End Structure
Public Class clsCompanyDetails
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetCompanyDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCompanyID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from TRACe_CompanyDetails Where Company_CompID=" & iACID & " And Company_ID=" & iCompanyID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Company_ID,Company_Name from TRACe_CompanyDetails Where Company_CompID=" & iACID & " Order by Company_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCompanyNameCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iCompPKId As Integer, ByVal sColumnName As String, ByVal sName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Company_ID from TRACe_CompanyDetails where Company_CompID=" & iACID & " And " & sColumnName & "='" & sName & "'"
            If iCompPKId > 0 Then
                sSql = sSql & " And Company_ID<>" & iCompPKId & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyDetailsReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iCompanyID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("Company_Code")
            dtTab.Columns.Add("Company_Name")
            dtTab.Columns.Add("Company_Address")
            dtTab.Columns.Add("Company_City")
            dtTab.Columns.Add("Company_State")
            dtTab.Columns.Add("Company_Country")
            dtTab.Columns.Add("Company_PinCode")
            dtTab.Columns.Add("Company_EmailID")
            dtTab.Columns.Add("Company_Establishment_Date")
            dtTab.Columns.Add("Company_ContactPerson")
            dtTab.Columns.Add("Company_MobileNo")
            dtTab.Columns.Add("Company_ContactEmailID")
            dtTab.Columns.Add("Company_TelephoneNo")
            dtTab.Columns.Add("Company_WebSite")
            dtTab.Columns.Add("Company_ContactNo1")
            dtTab.Columns.Add("Company_ContactNo2")

            sSql = "Select * From TRACe_CompanyDetails where Company_CompID=" & iACID & " And Company_ID=" & iCompanyID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("Company_Code")) = False Then
                    drow("Company_Code") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_Code"))
                End If
                If IsDBNull(dt.Rows(i)("Company_Name")) = False Then
                    drow("Company_Name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_Name"))
                End If
                If IsDBNull(dt.Rows(i)("Company_Address")) = False Then
                    drow("Company_Address") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_Address"))
                End If
                If IsDBNull(dt.Rows(i)("Company_City")) = False Then
                    drow("Company_City") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_City"))
                End If
                If IsDBNull(dt.Rows(i)("Company_State")) = False Then
                    drow("Company_State") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_State"))
                End If
                If IsDBNull(dt.Rows(i)("Company_Country")) = False Then
                    drow("Company_Country") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_Country"))
                End If
                If IsDBNull(dt.Rows(i)("Company_PinCode")) = False Then
                    drow("Company_PinCode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_PinCode"))
                End If
                If IsDBNull(dt.Rows(i)("Company_EmailID")) = False Then
                    drow("Company_EmailID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_EmailID"))
                End If
                If IsDBNull(dt.Rows(i)("Company_Establishment_Date")) = False Then
                    If dt.Rows(i)("Company_Establishment_Date") <> "" Then
                        drow("Company_Establishment_Date") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_Establishment_Date"))
                    End If
                End If
                If IsDBNull(dt.Rows(i)("Company_ContactPerson")) = False Then
                    drow("Company_ContactPerson") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_ContactPerson"))
                End If
                If IsDBNull(dt.Rows(i)("Company_MobileNo")) = False Then
                    drow("Company_MobileNo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_MobileNo"))
                End If
                If IsDBNull(dt.Rows(i)("Company_ContactEmailID")) = False Then
                    drow("Company_ContactEmailID") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_ContactEmailID"))
                End If
                If IsDBNull(dt.Rows(i)("Company_TelephoneNo")) = False Then
                    drow("Company_TelephoneNo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_TelephoneNo"))
                End If
                If IsDBNull(dt.Rows(i)("Company_WebSite")) = False Then
                    drow("Company_WebSite") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_WebSite"))
                End If
                If IsDBNull(dt.Rows(i)("Company_ContactNo1")) = False Then
                    drow("Company_ContactNo1") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_ContactNo1"))
                End If
                If IsDBNull(dt.Rows(i)("Company_ContactNo2")) = False Then
                    drow("Company_ContactNo2") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Company_ContactNo2"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCompanyDetails(ByVal sAC As String, ByVal objCompanyDetails As strCompanyDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCompanyDetails.iCompany_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Code", OleDb.OleDbType.VarChar, 30)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Name", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Address", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_City", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_State", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Country", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Country
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_PinCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_PinCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Establishment_Date", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Establishment_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_ContactPerson", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_MobileNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_ContactEmailID", OleDb.OleDbType.VarChar, 30)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_ContactEmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_TelephoneNo", OleDb.OleDbType.VarChar, 30)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_TelephoneNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_WebSite", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_WebSite
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_ContactNo1", OleDb.OleDbType.VarChar, 30)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_ContactNo1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_ContactNo2", OleDb.OleDbType.VarChar, 30)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_ContactNo2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_HolderName", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_HolderName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_AccountNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_AccountNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Bankname", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Bankname
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Branch", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Conditions", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Conditions
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_Paymentterms", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_Paymentterms
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCompanyDetails.iCompany_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCompanyDetails.iCompany_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCompanyDetails.sCompany_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Company_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCompanyDetails.iCompany_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spTRACe_CompanyDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal sTable As String, ByVal sColumn As String, ByVal sCompColumn As String) As Integer
        Dim sSql As String
        Dim objMax As Object
        Try
            sSql = "Select ISNULL(MAX(" & sColumn & ")+1,1) FROM " & sTable & "  Where " & sCompColumn & "=" & iACID & " "
            objMax = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If Not objMax Is DBNull.Value Then
                Return Integer.Parse(objMax.ToString())
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCompanyImageName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As String
        Dim sSql As String = ""
        Dim sImageName As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT (CLS_FileName + '.' + CLS_Extn) As CLS_FileName FROM Company_Logo_Settings WHERE CLS_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("CLS_FileName")) = False Then
                    sImageName = dt.Rows(0)("CLS_FileName")
                    If sImageName = "NULL.NULL" Then
                        sImageName = ""
                    End If
                Else
                    sImageName = ""
                End If
            End If
            Return sImageName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyBranchDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCompanyID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Company_Branch_Id,Company_Branch_Name from TRACe_CompanyBranchDetails Where Company_Branch_CompanyID=" & iCompanyID & " and Company_Branch_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyBranchDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchId As Integer, ByVal iCompanyID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from TRACe_CompanyBranchDetails Where Company_Branch_Id=" & iBranchId & " and Company_Branch_CompanyID=" & iCompanyID & " and Company_Branch_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCompanyBranch(ByVal sAC As String, ByVal iACID As Integer, ByVal sBranchName As String, ByVal iCompanyID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Company_Branch_Id from TRACe_CompanyBranchDetails where Company_Branch_CompanyID=" & iCompanyID & " And Company_Branch_Name='" & sBranchName & "' and Company_Branch_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCompanyBranch(ByVal sAC As String, ByVal objsCompanyBranch As strCompanyBranch) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Id", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.iCompany_Branch_Id
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_CompanyID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.iCompany_Branch_CompanyID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Name", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_Name
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Address", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_Address
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Contact_Person", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_Contact_Person
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Contact_MobileNo", OleDb.OleDbType.VarChar, 15)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_Contact_MobileNo
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Contact_LandLineNo", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_Contact_LandLineNo
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Contact_Email", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_Contact_Email
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_Designation", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_Designation
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_DelFlag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.iCompany_Branch_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.iCompany_Branch_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.sCompany_Branch_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Company_Branch_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompanyBranch.iCompany_Branch_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spTRACe_CompanyBranchDetails", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
