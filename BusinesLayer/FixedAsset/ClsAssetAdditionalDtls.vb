Imports System
Imports System.Data
Imports DatabaseLayer
Imports BusinesLayer
Public Class ClsAssetAdditionalDtls
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral

    Public Structure DeviceDetails
        Public ADD_ID As Integer
        Public ADD_MasterID As Integer
        Public ADD_DeviceType As Integer
        Public ADD_DeviceNo As String
        Public ADD_ModelName As String
        Public ADD_ManufacturedBy As String
        Public ADD_DateofPurchase As DateTime
        Public ADD_Details As String
        Public ADD_WarrantyExpireson As DateTime
        Public ADD_Employeename As String
        Public ADD_StandAloneServer As Integer
        Public ADD_DescriptionDev As String
        Public ADD_SuplierName As String
        Public ADD_ContactPerson As String
        Public ADD_Address As String
        Public ADD_Phone As String
        Public ADD_Fax As String
        Public ADD_EmailID As String
        Public ADD_Website As String
        Public ADD_CreatedBy As Integer
        Public ADD_CreatedOn As DateTime
        Public ADD_UpdatedBy As Integer
        Public ADD_UpdatedOn As DateTime
        Public ADD_DelFlag As String
        Public ADD_Status As String
        Public ADD_YearID As Integer
        Public ADD_CompID As Integer
        Public ADD_Opeartion As String
        Public ADD_IPAddress As String
    End Structure
    Public Structure MaintainanceDetails
        Public AMD_ID As Integer
        Public AMD_MasterID As Integer
        Public AMD_MaintainedBy As String
        Public AMD_ContactPerson As String
        Public AMD_Address As String
        Public AMD_Phone As String
        Public AMD_Fax As String
        Public AMD_EmailID As String
        Public AMD_Website As String
        Public AMD_Companyname As String
        Public AMD_AmcAmount As Double
        Public AMD_AmcTermDate As DateTime
        Public AMD_AmcTo As DateTime
        Public AMD_AmcPaymentterm As Integer
        Public AMD_NoInstalment As Integer
        Public AMD_InstalmentAmnt As Double
        Public AMD_TotalPaidinstalment As Integer
        Public AMD_TotalAmnt As Double
        Public AMD_CreatedBy As Integer
        Public AMD_CreatedOn As DateTime
        Public AMD_UpdatedBy As Integer
        Public AMD_UpdatedOn As DateTime
        Public AMD_DelFlag As String
        Public AMD_Status As String
        Public AMD_YearID As Integer
        Public AMD_CompID As Integer
        Public AMD_Opeartion As String
        Public AMD_IPAddress As String
    End Structure
    Public Structure InsuranceDetails
        Public AID_ID As Integer
        Public AID_MasterID As Integer
        Public AID_InsuranceComp As String
        Public AID_ContactPerson As String
        Public AID_Address As String
        Public AID_Phone As String
        Public AID_Fax As String
        Public AID_Email As String
        Public AID_Website As String
        Public AID_PolicyType As Integer
        Public AID_PolicyNo As String
        Public AID_PolicyAmount As Double
        Public AID_Premiumpaid As Double
        Public AID_TermDate As DateTime
        Public AID_ToDate As DateTime
        Public AID_CreatedBy As Integer
        Public AID_CreatedOn As DateTime
        Public AID_UpdatedBy As Integer
        Public AID_UpdatedOn As DateTime
        Public AID_DelFlag As String
        Public AID_Status As String
        Public AID_YearID As Integer
        Public AID_CompID As Integer
        Public AID_Opeartion As String
        Public AID_IPAddress As String
    End Structure
    Public Structure InstallationDetails
        Public AIND_ID As Integer
        Public AIND_MasterID As Integer
        Public AIND_DeviceNo As Integer
        Public AIND_Software As Integer
        Public AIND_Version As String
        Public AIND_DateofInstln As DateTime
        Public AIND_UnInstlnOn As DateTime
        Public AIND_ReInstlnOn As DateTime
        Public AIND_InstlnBy As String
        Public AIND_DatabaseDtls As String
        Public AIND_Description As String
        Public AIND_InstlnPlace As String
        Public AIND_ContactPerson As String
        Public AIND_Address As String
        Public AIND_Phone As String
        Public AIND_FAX As String
        Public AIND_Email As String
        Public AIND_Website As String
        Public AIND_Maintainedby As String
        Public AIND_MaintainedContactPerson As String
        Public AIND_MaintainedAddress As String
        Public AIND_MaintainedPhone As String
        Public AIND_MaintainedFax As String
        Public AIND_MaintainedEmail As String
        Public AIND_MaintainedWebsite As String
        Public AIND_CreatedBy As Integer
        Public AIND_CreatedOn As DateTime
        Public AIND_UpdatedBy As Integer
        Public AIND_UpdatedOn As DateTime
        Public AIND_DelFlag As String
        Public AIND_Status As String
        Public AIND_YearID As Integer
        Public AIND_CompID As Integer
        Public AIND_Opeartion As String
        Public AIND_IPAddress As String
    End Structure

    Public Function AssetNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAA_ID,AFAA_AssetNo from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iCompID & " order by AFAA_ID asc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReferenceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetNo As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAA_ID,AFAA_AssetRefNo from Acc_FixedAssetAdditionDel where AFAA_ID=" & iAssetNo & " and AFAA_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearchReferanceNoList(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sSearch <> "" Then
                sSql = "Select AFAA_ID,AFAA_AssetRefNo from Acc_FixedAssetAdditionDel where AFAA_AssetRefNo '" & sSearch & "%' And AFAA_CompID=" & iCompID & " order by AFAA_AssetRefNo "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select AFAA_ID,AFAA_AssetRefNo from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iCompID & " and  order by AFAA_AssetRefNo "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReferenceNo2(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAA_ID,AFAA_AssetRefNo from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iCompID & " order by AFAA_ID"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDeviceNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select ADD_ID,ADD_DeviceNo from Acc_DeviceDetails where ADD_CompID=" & iCompID & " order by ADD_ID"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDeviceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_DeviceDetails where ADD_CompID=" & iCompID & " and ADD_MasterID=" & iMasterId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInstallationDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_InstallationDetails where AIND_CompID=" & iCompID & " and AIND_MasterID=" & iMasterId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInsuranceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_InsuranceDetails where AID_CompID=" & iCompID & " and AID_MasterID=" & iMasterId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMaintananceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_MaintenanceDetails where AMD_CompID=" & iCompID & " and AMD_MasterID=" & iMasterId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetLoanDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_AsstObtLoan where AOL_CompID=" & iCompID & " and AOL_MasterID=" & iMasterId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAsstTakenOnLoanDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_AsstTakenOnLoan where ATL_CompID=" & iCompID & " and ATL_MasterID=" & iMasterId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AssetNo2(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iRefid As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAA_ID,AFAA_AssetNo from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iCompID & " and AFAA_ID=" & iRefid & ""
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDeviceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objAsstAddnDtls1 As ClsAssetAdditionalDtls.DeviceDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(30) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_DeviceType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_DeviceType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_DeviceNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_DeviceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_ModelName", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_ModelName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_ManufacturedBy", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_ManufacturedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_DateofPurchase", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_DateofPurchase
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Details", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Details
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_WarrantyExpireson", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_WarrantyExpireson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Employeename", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Employeename
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_StandAloneServer", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_StandAloneServer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_DescriptionDev", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_DescriptionDev
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_SuplierName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_SuplierName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_ContactPerson", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Phone", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Phone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Fax", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Website", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Website
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsstAddnDtls1.ADD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_DeviceDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSupplierDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iMasterId As Integer, ByVal sSupplierName As String, ByVal sContactperson As String, ByVal sAddress As String, ByVal sPhoneNo As String, ByVal sFax As String, ByVal sEmailid As String, ByVal sWebsite As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Acc_DeviceDetails where ADD_MasterID = '" & iMasterId & "' and ADD_CompID =" & iCompID & " and ADD_YearID =" & iYearid & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Acc_DeviceDetails set ADD_SuplierName='" & sSupplierName & "',"
                sSql = sSql & "ADD_ContactPerson = '" & sContactperson & "',ADD_Address='" & sAddress & "',"
                sSql = sSql & "ADD_Phone ='" & sPhoneNo & "',ADD_Fax = '" & sFax & "',"
                sSql = sSql & "ADD_EmailID = '" & sEmailid & "',ADD_Website='" & sWebsite & "'"
                sSql = sSql & "where ADD_MasterID = '" & iMasterId & "' and ADD_CompID =" & iCompID & " and ADD_YearID =" & iYearid & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("ADD_ID")
            Else
                sSql = "" : sSql = "Insert into Acc_DeviceDetails(ADD_SuplierName,ADD_ContactPerson,ADD_Address,ADD_Phone,ADD_Fax,ADD_EmailID,ADD_Website)"
                sSql = sSql & " Values('" & sSupplierName & "','" & sContactperson & "','" & sAddress & "','" & sPhoneNo & "',"
                sSql = sSql & "'" & sFax & "','" & sEmailid & "','" & sWebsite & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("ADD_ID")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function SaveSupplierDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iMasterId As Integer, ByVal sSupplierName As String, ByVal sContactperson As String, ByVal sAddress As String, ByVal sPhoneNo As String, ByVal sFax As String, ByVal sEmailid As String, ByVal sWebsite As String) As Integer
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim iMax As Integer = 0
    '    Try
    '        sSql = "" : sSql = "Select * from Acc_DeviceDetails where ADD_MasterID = '" & iMasterId & "' and ADD_CompID =" & iCompID & " and ADD_YearID =" & iYearid & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dt.Rows.Count > 0 Then
    '            sSql = "" : sSql = "Update Acc_DeviceDetails set ADD_SuplierName='" & sSupplierName & "',"
    '            sSql = sSql & "ADD_ContactPerson = '" & sContactperson & "',ADD_Address='" & sAddress & "',"
    '            sSql = sSql & "ADD_Phone ='" & sPhoneNo & "',ADD_Fax = '" & sFax & "',"
    '            sSql = sSql & "ADD_EmailID = '" & sEmailid & "',ADD_Website='" & sWebsite & "'"
    '            sSql = sSql & "where ADD_MasterID = '" & iMasterId & "' and ADD_CompID =" & iCompID & " and ADD_YearID =" & iYearid & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return dt.Rows(0)("ADD_ID")
    '        Else
    '            sSql = "" : sSql = "Insert into Acc_DeviceDetails(ADD_SuplierName,ADD_ContactPerson,ADD_Address,ADD_Phone,ADD_Fax,ADD_EmailID,ADD_Website)"
    '            sSql = sSql & " Values('" & sSupplierName & "','" & sContactperson & "','" & sAddress & "','" & sPhoneNo & "',"
    '            sSql = sSql & "'" & sFax & "','" & sEmailid & "','" & sWebsite & "')"
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return dt.Rows(0)("ADD_ID")
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function



    Public Function SaveAssetLoanDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iMasterId As Integer, ByVal EmpId As Integer, ByVal sEmpName As String, ByVal iAsstType As Integer, ByVal sSerNo As String, ByVal dApprmateVal As Double, ByVal dIssudate As Date, ByVal dDueDate As Date, ByVal dRecvdDate As Date, ByVal RetDate As Date, ByVal sCondWhenIssued As String, ByVal sCondOnrecvd As String, ByVal sRemarks As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            dIssudate = Date.ParseExact(dIssudate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            sSql = "" : sSql = "Select * from Acc_AsstObtLoan where AOL_MasterId = '" & iMasterId & "' and AOL_CompID =" & iCompID & " and AOL_YearID =" & iYearid & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Acc_AsstObtLoan set AOL_AssetID='" & iAsstType & "',"
                sSql = sSql & "AOL_SerNo = '" & sSerNo & "',AOL_ApprxmateVal='" & dApprmateVal & "', AOL_IssueDate=" & objGen.FormatDtForRDBMS(dIssudate, "I") & ", AOL_DueDate=" & objGen.FormatDtForRDBMS(dDueDate, "I") & ","
                sSql = sSql & "AOL_RecvdDate =" & objGen.FormatDtForRDBMS(dRecvdDate, "I") & ",AOL_ReturnDate = " & objGen.FormatDtForRDBMS(RetDate, "I") & ","
                sSql = sSql & "AOL_CondWhenIssued = '" & sCondWhenIssued & "',AOL_CondOnRecvd='" & sCondOnrecvd & "', AOL_Remarks='" & sRemarks & "'"
                sSql = sSql & " where AOL_MasterId = '" & iMasterId & "' and AOL_CompID =" & iCompID & " and AOL_YearID =" & iYearid & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                sSql = "select isnull(max(AOL_ID)+1,1) from Acc_AsstObtLoan"
                iMax = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
                sSql = "" : sSql = "Insert into Acc_AsstObtLoan(AOL_ID,AOL_MasterId,AOL_EmpID,AOL_EmpCode,AOL_AssetID,AOL_SerNo,AOL_ApprxmateVal,AOL_IssueDate,AOL_DueDate,AOL_RecvdDate,AOL_ReturnDate,AOL_CondWhenIssued,AOL_CondOnRecvd,AOL_Remarks,AOL_YearID,AOL_CompID)"
                sSql = sSql & " Values('" & iMax & "','" & iMasterId & "','" & EmpId & "','" & sEmpName & "','" & iAsstType & "','" & sSerNo & "','" & dApprmateVal & "',"
                sSql = sSql & " " & objGen.FormatDtForRDBMS(dIssudate, "I") & "," & objGen.FormatDtForRDBMS(dDueDate, "I") & "," & objGen.FormatDtForRDBMS(dRecvdDate, "I") & "," & objGen.FormatDtForRDBMS(RetDate, "I") & ",'" & sCondWhenIssued & "','" & sCondOnrecvd & "','" & sRemarks & "','" & iYearid & "' ,'" & iCompID & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveTakenLoanAsstDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iMasterId As Integer, ByVal sLoanTowhm As String, ByVal sLoanToAddrss As String, ByVal sLoanAmt As String, ByVal sLoanAggrmntNo As String, ByVal dLoanDate As Date, ByVal iCrncyType As Integer, ByVal dExcgDate As Date, ByVal dExcgAmt As String, ByVal dAmtRupees As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Acc_AsstTakenOnLoan where ATL_MasterId = '" & iMasterId & "' and ATL_CompID =" & iCompID & " and ATL_YearID =" & iYearid & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Acc_AsstTakenOnLoan set ATL_LoanTowhm='" & sLoanTowhm & "',"
                sSql = sSql & "ATL_LoanToAddrss = '" & sLoanToAddrss & "',ATL_LoanAmt='" & sLoanAmt & "', ATL_LoanAggrmntNo='" & sLoanAggrmntNo & "', ATL_dLoanDate=" & objGen.FormatDtForRDBMS(dLoanDate, "I") & ","
                sSql = sSql & "ATL_ImpCrncyType ='" & iCrncyType & "',ATL_EcxhgDate = " & objGen.FormatDtForRDBMS(dExcgDate, "I") & ","
                sSql = sSql & "ATL_ExchgAmt = '" & dExcgAmt & "',ATL_AmountRpees='" & dAmtRupees & "'"
                sSql = sSql & " where ATL_MasterId = '" & iMasterId & "' and ATL_CompID =" & iCompID & " and ATL_YearID =" & iYearid & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                sSql = "select isnull(max(ATL_ID)+1,1) from Acc_AsstTakenOnLoan"
                iMax = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
                sSql = "" : sSql = "Insert into Acc_AsstTakenOnLoan(ATL_ID,ATL_MasterId,ATL_LoanTowhm,ATL_LoanToAddrss,ATL_LoanAmt,ATL_LoanAggrmntNo,ATL_dLoanDate,ATL_ImpCrncyType,ATL_EcxhgDate,ATL_ExchgAmt,ATL_AmountRpees,ATL_YearID,ATL_CompID)"
                sSql = sSql & " Values('" & iMax & "','" & iMasterId & "','" & sLoanTowhm & "','" & sLoanToAddrss & "','" & sLoanAmt & "','" & sLoanAggrmntNo & "',"
                sSql = sSql & " " & objGen.FormatDtForRDBMS(dLoanDate, "I") & ",'" & iCrncyType & "'," & objGen.FormatDtForRDBMS(dExcgDate, "I") & ",'" & dExcgAmt & "','" & dAmtRupees & "','" & iYearid & "' ,'" & iCompID & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function SaveMaintenanceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objAsstAddnDtls2 As ClsAssetAdditionalDtls.MaintainanceDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(29) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_MaintainedBy", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_MaintainedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_ContactPerson", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_Phone", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_Phone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_Fax", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_Website", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_Website
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_Companyname", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_Companyname
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_AmcAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_AmcAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_AmcTermDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_AmcTermDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_AmcTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_AmcTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_AmcPaymentterm", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_AmcPaymentterm
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_NoInstalment", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_NoInstalment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_InstalmentAmnt", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_InstalmentAmnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_TotalPaidinstalment", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_TotalPaidinstalment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_TotalAmnt", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_TotalAmnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AMD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsstAddnDtls2.AMD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_MaintenanceDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveInsuranceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objAsstAddnDtls3 As ClsAssetAdditionalDtls.InsuranceDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(26) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_InsuranceComp", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_InsuranceComp
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_ContactPerson", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Phone", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Phone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Fax", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Email", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Website", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Website
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_PolicyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_PolicyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_PolicyNo", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_PolicyNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_PolicyAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_PolicyAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Premiumpaid", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Premiumpaid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_TermDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_TermDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsstAddnDtls3.AID_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_InsuranceDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveInstallationDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objAsstAddnDtls4 As ClsAssetAdditionalDtls.InstallationDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(36) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_DeviceNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_DeviceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Software", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Software
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Version", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Version
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_DateofInstln", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_DateofInstln
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_UnInstlnOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_UnInstlnOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_ReInstlnOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_ReInstlnOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_InstlnBy", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_InstlnBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_DatabaseDtls", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_DatabaseDtls
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Description", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_InstlnPlace", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_InstlnPlace
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_ContactPerson", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Phone", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Phone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_FAX", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_FAX
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Email", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Website", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Website
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Maintainedby", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Maintainedby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_MaintainedContactPerson", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_MaintainedContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_MaintainedAddress", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_MaintainedAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_MaintainedPhone", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_MaintainedPhone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_MaintainedFax", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_MaintainedFax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_MaintainedEmail", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_MaintainedEmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_MaintainedWebsite", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_MaintainedWebsite
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AID_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIND_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsstAddnDtls4.AIND_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_InstallationDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFxdAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select GL_Desc,GL_ID From Chart_Of_Accounts Where gl_CompID=" & iCompID & "  and GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where gl_CompID=" & iCompID & " and GL_Desc='Fixed assets'))"

            'sSql = "Select * From Acc_General_Master Where Mas_CompID='" & iCompID & "' and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') "
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
