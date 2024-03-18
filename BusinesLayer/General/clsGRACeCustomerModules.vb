Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports System.Web
Public Class clsGRACeCustomerModules
    Private objDBL As New DBHelper
    Public Function GetModules(ByVal sAccessCode As String) As String
        Dim sSql As String
        Dim dtTab As New DataTable
        Dim sModules As String = ""
        Try
            sSql = "Select MP_ModuleName From MMCS_Modules Where MM_MP_ID=1 And MM_ID in "
            sSql = sSql & "(Select MCM_ModuleID From MMCS_CustomerModules Where MCM_MCR_ID in "
            sSql = sSql & "(Select MCR_ID From MMCS_CustomerRegistration Where MCR_CustomerCode='" & sAccessCode & "'))"
            dtTab = objDBL.SQLExecuteDataTable("MMCSPLCR", sSql)
            For i = 0 To dtTab.Rows.Count - 1
                sModules = sModules & "," & dtTab.Rows(i)("MP_ModuleName")
            Next
            If sModules.StartsWith(",") = False Then
                sModules = "," & sModules
            End If
            If sModules.EndsWith(",") = False Then
                sModules = sModules & ","
            End If
            Return sModules
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCustomerRegAccessCode(ByVal sAccessCode As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select MCR_ID From MMCS_CustomerRegistration Where MCR_CustomerCode='" & sAccessCode & "' And MCR_MP_ID=1" '1=TRACe PA Product
            Return objDBL.SQLCheckForRecord("MMCSPLCR", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDatabaseExists(ByVal sAccessCode As String, ByVal sDatabaseName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "SELECT database_id FROM sys.databases WHERE Name='" & sDatabaseName & "'"
            Return objDBL.DBCheckForRecord(sAccessCode, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerNumberOfUsers(ByVal sAccessCode As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select MCR_NumberOfUsers From MMCS_CustomerRegistration Where MCR_CustomerCode='" & sAccessCode & "'"
            Return objDBL.SQLExecuteScalarInt("MMCSPLCR", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerNumberFromReg(ByVal sAccessCode As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select MCR_NumberOfCustomers From MMCS_CustomerRegistration Where MCR_CustomerCode='" & sAccessCode & "'"
            Return objDBL.SQLExecuteScalarInt("MMCSPLCR", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerFolderDatasize(ByVal sAccessCode As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select MCR_Datasize From MMCS_CustomerRegistration Where MCR_CustomerCode='" & sAccessCode & "'"
            Return objDBL.SQLExecuteScalarInt("MMCSPLCR", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub CheckAndAddBasicCustomerDetails(ByVal sAccessCode As String, ByVal sAccessCodeID As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Dim dt As New DataTable
        Dim objCompanyDetails As New strCompanyDetails
        Dim objclsCompanyDetails As New clsCompanyDetails
        Try
            sSql = "SELECT Count(*) FROM Trace_CompanyDetails"
            If objDBL.SQLExecuteScalarInt(sAccessCode, sSql) = 0 Then
                sSql = "Select * From MMCS_CustomerRegistration Where MCR_CustomerCode='" & sAccessCode & "'"
                dt = objDBL.SQLExecuteDataTable("MMCSPLCR", sSql)
                If dt.Rows.Count > 0 Then
                    objCompanyDetails.iCompany_ID = 0
                    objCompanyDetails.sCompany_Code = dt.Rows(0).Item("MCR_CustomerCode")
                    objCompanyDetails.sCompany_Name = dt.Rows(0).Item("MCR_CustomerName")
                    objCompanyDetails.sCompany_Address = dt.Rows(0).Item("MCR_Address")
                    objCompanyDetails.sCompany_City = dt.Rows(0).Item("MCR_City")
                    objCompanyDetails.sCompany_State = dt.Rows(0).Item("MCR_State")
                    objCompanyDetails.sCompany_Country = ""
                    objCompanyDetails.sCompany_PinCode = ""
                    objCompanyDetails.sCompany_EmailID = dt.Rows(0).Item("MCR_CustomerEmail")
                    objCompanyDetails.sCompany_Establishment_Date = ""
                    objCompanyDetails.sCompany_ContactPerson = dt.Rows(0).Item("MCR_ContactPersonName")
                    objCompanyDetails.sCompany_MobileNo = dt.Rows(0).Item("MCR_ContactPersonPhoneNo")
                    objCompanyDetails.sCompany_ContactEmailID = dt.Rows(0).Item("MCR_ContactPersonEmail")
                    objCompanyDetails.sCompany_TelephoneNo = dt.Rows(0).Item("MCR_CustomerTelephoneNo")
                    objCompanyDetails.sCompany_WebSite = ""
                    objCompanyDetails.sCompany_ContactNo1 = dt.Rows(0).Item("MCR_CustomerTelephoneNo")
                    objCompanyDetails.sCompany_ContactNo2 = ""

                    objCompanyDetails.sCompany_HolderName = ""
                    objCompanyDetails.sCompany_AccountNo = ""
                    objCompanyDetails.sCompany_Bankname = ""
                    objCompanyDetails.sCompany_Branch = ""
                    objCompanyDetails.sCompany_Conditions = ""
                    objCompanyDetails.sCompany_Paymentterms = ""

                    objCompanyDetails.iCompany_CrBy = iUserID
                    objCompanyDetails.iCompany_UpdatedBy = iUserID
                    objCompanyDetails.sCompany_IPAddress = sIPAddress
                    objCompanyDetails.iCompany_CompID = sAccessCodeID
                    objclsCompanyDetails.SaveCompanyDetails(sAccessCode, objCompanyDetails)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class