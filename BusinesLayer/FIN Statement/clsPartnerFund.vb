Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Structure strPartnership_Firms
    Private APF_ID As Integer
    Private APF_YearID As Integer
    Private APF_Cust_ID As Integer
    Private APF_Branch_ID As Integer
    Private APF_Partner_ID As Integer
    Private AAID_PricePerUnit As Decimal
    Private APF_OpeningBalance As Decimal
    Private APF_UnsecuredLoanTreatedAsCapital As Decimal
    Private APF_InterestOnCapital As Decimal
    Private APF_PartnersSalary As Decimal
    Private APF_ShareOfprofit As Decimal
    Private APF_TransferToFixedCapital As Decimal
    Private APF_Drawings As Decimal
    Private APF_AddOthers As Decimal
    Private APF_LessOthers As Decimal
    Private APF_CapitalAmount As String
    Private APF_CrBy As Integer
    Private APF_UpdateBy As Integer
    Private APF_IPAddress As String
    Private APF_CompID As Integer
    Public Property iAPF_ID() As Integer
        Get
            Return (APF_ID)
        End Get
        Set(ByVal Value As Integer)
            APF_ID = Value
        End Set
    End Property
    Public Property iAPF_YearID() As Integer
        Get
            Return (APF_YearID)
        End Get
        Set(ByVal Value As Integer)
            APF_YearID = Value
        End Set
    End Property
    Public Property iAPF_Cust_ID() As Integer
        Get
            Return (APF_Cust_ID)
        End Get
        Set(ByVal Value As Integer)
            APF_Cust_ID = Value
        End Set
    End Property
    Public Property iAPF_Branch_ID() As Integer
        Get
            Return (APF_Branch_ID)
        End Get
        Set(ByVal Value As Integer)
            APF_Branch_ID = Value
        End Set
    End Property
    Public Property iAPF_Partner_ID() As Integer
        Get
            Return (APF_Partner_ID)
        End Get
        Set(ByVal Value As Integer)
            APF_Partner_ID = Value
        End Set
    End Property
    Public Property dAAID_PricePerUnit() As Decimal
        Get
            Return (AAID_PricePerUnit)
        End Get
        Set(ByVal Value As Decimal)
            AAID_PricePerUnit = Value
        End Set
    End Property
    Public Property dAPF_OpeningBalance() As Decimal
        Get
            Return (APF_OpeningBalance)
        End Get
        Set(ByVal Value As Decimal)
            APF_OpeningBalance = Value
        End Set
    End Property
    Public Property dAPF_UnsecuredLoanTreatedAsCapital() As Decimal
        Get
            Return (APF_UnsecuredLoanTreatedAsCapital)
        End Get
        Set(ByVal Value As Decimal)
            APF_UnsecuredLoanTreatedAsCapital = Value
        End Set
    End Property
    Public Property dAPF_InterestOnCapital() As Decimal
        Get
            Return (APF_InterestOnCapital)
        End Get
        Set(ByVal Value As Decimal)
            APF_InterestOnCapital = Value
        End Set
    End Property
    Public Property dAPF_PartnersSalary() As Decimal
        Get
            Return (APF_PartnersSalary)
        End Get
        Set(ByVal Value As Decimal)
            APF_PartnersSalary = Value
        End Set
    End Property
    Public Property dAPF_ShareOfprofit() As Decimal
        Get
            Return (APF_ShareOfprofit)
        End Get
        Set(ByVal Value As Decimal)
            APF_ShareOfprofit = Value
        End Set
    End Property
    Public Property dAPF_TransferToFixedCapital() As Decimal
        Get
            Return (APF_TransferToFixedCapital)
        End Get
        Set(ByVal Value As Decimal)
            APF_TransferToFixedCapital = Value
        End Set
    End Property
    Public Property dAPF_Drawings() As Decimal
        Get
            Return (APF_Drawings)
        End Get
        Set(ByVal Value As Decimal)
            APF_Drawings = Value
        End Set
    End Property
    Public Property dAPF_AddOthers() As Decimal
        Get
            Return (APF_AddOthers)
        End Get
        Set(ByVal Value As Decimal)
            APF_AddOthers = Value
        End Set
    End Property
    Public Property dAPF_LessOthers() As Decimal
        Get
            Return (APF_LessOthers)
        End Get
        Set(ByVal Value As Decimal)
            APF_LessOthers = Value
        End Set
    End Property
    Public Property sAPF_CapitalAmount() As String
        Get
            Return (APF_CapitalAmount)
        End Get
        Set(ByVal Value As String)
            APF_CapitalAmount = Value
        End Set
    End Property
    Public Property iAPF_CrBy() As Integer
        Get
            Return (APF_CrBy)
        End Get
        Set(ByVal Value As Integer)
            APF_CrBy = Value
        End Set
    End Property
    Public Property iAPF_UpdateBy() As Integer
        Get
            Return (APF_UpdateBy)
        End Get
        Set(ByVal Value As Integer)
            APF_UpdateBy = Value
        End Set
    End Property
    Public Property sAPF_IPAddress() As String
        Get
            Return (APF_IPAddress)
        End Get
        Set(ByVal Value As String)
            APF_IPAddress = Value
        End Set
    End Property
    Public Property iAPF_CompID() As Integer
        Get
            Return (APF_CompID)
        End Get
        Set(ByVal Value As Integer)
            APF_CompID = Value
        End Set
    End Property
End Structure
Public Class clsPartnerFund
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objDBL As New DatabaseLayer.DBHelper
    Public Function LoadExistingCustomerName(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where cust_Compid=" & iACID & " and CUST_DelFlg = 'A' order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingFinancialYear(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select YMS_YEARID,YMS_ID from YEAR_MASTER where YMS_FROMDATE < DATEADD(year,+1,GETDATE()) and YMS_CompId=" & iACID & " order by YMS_ID desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingBranchName(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
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
    Public Function GetCustShareOfProfitPercentage(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustPartnerId As Integer) As String
        Dim sSql As String
        Dim sShareOfProfitPercentage As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SSP_ShareOfProfit from SAD_Statutory_PartnerDetails where SSP_Id=" & iCustPartnerId & " and SSP_CompID=" & iACID & ""
            sShareOfProfitPercentage = objDBL.SQLExecuteScalar(sAC, sSql)
            If sShareOfProfitPercentage = "" Then
                Return String.Empty
            Else
                Return "(" & sShareOfProfitPercentage & "%)"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustCapitalAmount(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustPartnerId As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select IsNull(SSP_CapitalAmount,0) As SSP_CapitalAmount from SAD_Statutory_PartnerDetails where SSP_Id=" & iCustPartnerId & " and SSP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustPartner(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select SSP_Id as ID,SSP_PartnerName as Name from SAD_Statutory_PartnerDetails where SSP_CustID=" & iCustId & " and SSP_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePartnershipFirms(ByVal sAC As String, ByVal objPF As strPartnership_Firms)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_Cust_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_Cust_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_Branch_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_Branch_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_Partner_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_Partner_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_OpeningBalance", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_OpeningBalance
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_UnsecuredLoanTreatedAsCapital", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_UnsecuredLoanTreatedAsCapital
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_InterestOnCapital", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_InterestOnCapital
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_PartnersSalary", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_PartnersSalary
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_ShareOfprofit", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_ShareOfprofit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_TransferToFixedCapital", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_TransferToFixedCapital
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_Drawings", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_Drawings
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_AddOthers", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_AddOthers
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_LessOthers", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objPF.dAPF_LessOthers
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_CapitalAmount", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPF.sAPF_CapitalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_UpdateBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_UpdateBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objPF.sAPF_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APF_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPF.iAPF_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spACC_Partnership_Firms", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedPartnershipFirms(ByVal sAc As String, ByVal iAcID As Integer, ByVal iPartnershipFirmId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From ACC_Partnership_Firms Where APF_ID=" & iPartnershipFirmId & " And APF_CompID=" & iAcID & ""
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedPartnershipFirmsIdFromPartnerFY(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select APF_ID From ACC_Partnership_Firms Where APF_YearID=" & iFinancialYearID & " And APF_Cust_ID=" & iCustomerID & " And APF_Partner_ID=" & iPartnerID & ""
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadAllPartnershipFirms(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable, dtTab As New DataTable
    '    Dim dr As DataRow
    '    Try
    '        dt.Columns.Add("PartnershipFirmID")
    '        dt.Columns.Add("PartnerName")
    '        dt.Columns.Add("OpeningBalance")
    '        dt.Columns.Add("CapitalIntroducedUnsecuredLoanTreatedAsCapital")
    '        dt.Columns.Add("InterestOnCapital")
    '        dt.Columns.Add("PartnerSalary")
    '        dt.Columns.Add("ShareOfProfit")
    '        dt.Columns.Add("TransferToFixedCapital")
    '        dt.Columns.Add("Drawings")
    '        dt.Columns.Add("Total")

    '        sSql = "Select APF_ID,SSP_PartnerName,APF_OpeningBalance,APF_UnsecuredLoanTreatedAsCapital,APF_InterestOnCapital,APF_PartnersSalary,APF_ShareOfprofit,APF_TransferToFixedCapital,APF_Drawings,"
    '        sSql = sSql & " APF_OpeningBalance+APF_UnsecuredLoanTreatedAsCapital+APF_InterestOnCapital+APF_PartnersSalary+APF_ShareOfprofit-APF_TransferToFixedCapital-APF_Drawings As Total From ACC_Partnership_Firms"
    '        sSql = sSql & " Join SAD_Statutory_PartnerDetails On SSP_Id=APF_Partner_ID "
    '        sSql = sSql & " Where APF_YearID=" & iFinancialYearID & " And APF_Cust_ID=" & iCustomerID & " And APF_CompID=" & iAcID & ""
    '        dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)
    '        For i = 0 To dtTab.Rows.Count - 1
    '            dr = dt.NewRow()
    '            dr("PartnershipFirmID") = dtTab.Rows(i)("APF_ID")
    '            dr("PartnerName") = dtTab.Rows(i)("SSP_PartnerName")
    '            dr("OpeningBalance") = dtTab.Rows(i)("APF_OpeningBalance")
    '            dr("CapitalIntroducedUnsecuredLoanTreatedAsCapital") = dtTab.Rows(i)("APF_UnsecuredLoanTreatedAsCapital")
    '            dr("InterestOnCapital") = dtTab.Rows(i)("APF_InterestOnCapital")
    '            dr("PartnerSalary") = dtTab.Rows(i)("APF_PartnersSalary")
    '            dr("ShareOfProfit") = dtTab.Rows(i)("APF_ShareOfprofit")
    '            dr("TransferToFixedCapital") = "-" & dtTab.Rows(i)("APF_TransferToFixedCapital")
    '            dr("Drawings") = "-" & dtTab.Rows(i)("APF_Drawings")
    '            dr("Total") = dtTab.Rows(i)("Total")
    '            dt.Rows.Add(dr)
    '        Next
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadAllPartnershipFirms(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal sFY1 As Integer, ByVal sFY2 As String, ByVal sIsReport As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable, dtTotalAmt As New DataTable
        Dim dr As DataRow
        Dim iSlNo As Integer = 0
        Dim dFYCAdd As Decimal = 0, dFYPAdd As Decimal = 0, dFYCLess As Decimal = 0, dFYPLess As Decimal = 0
        Dim dFYCTotal As Decimal = 0, dFYPTotal As Decimal = 0
        Dim dCOthers As Decimal = 0, dPOthers As Decimal = 0
        Dim dCLessOthers As Decimal = 0, dPLessOthers As Decimal = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("PARTICULARS")
            dt.Columns.Add("FYCData")
            dt.Columns.Add("FYPData")

            'sSql = "Select FYC.APF_ID,SSP_PartnerName,SSP_ShareOfProfit,IsNull(FYC.APF_OpeningBalance,0) AS FYC_APF_OpeningBalance,IsNull(FYP.APF_OpeningBalance,0) AS FYP_APF_OpeningBalance,"
            'sSql = sSql & " IsNull(FYC.APF_UnsecuredLoanTreatedAsCapital,0) AS FYC_APF_UnsecuredLoanTreatedAsCapital,IsNull(FYP.APF_UnsecuredLoanTreatedAsCapital,0) AS FYP_APF_UnsecuredLoanTreatedAsCapital,"
            'sSql = sSql & " IsNull(FYC.APF_InterestOnCapital,0) AS FYC_APF_InterestOnCapital,IsNull(FYP.APF_InterestOnCapital,0) AS FYP_APF_InterestOnCapital,"
            'sSql = sSql & " IsNull(FYC.APF_PartnersSalary,0) AS FYC_APF_PartnersSalary,IsNull(FYP.APF_PartnersSalary,0) AS FYP_APF_PartnersSalary,"
            'sSql = sSql & " IsNull(FYC.APF_ShareOfprofit,0) AS FYC_APF_ShareOfprofit,IsNull(FYP.APF_ShareOfprofit,0) AS FYP_APF_ShareOfprofit,"
            'sSql = sSql & " IsNull(FYC.APF_AddOthers,0) AS FYC_APF_AddOthers,IsNull(FYP.APF_AddOthers,0) AS FYP_APF_AddOthers,"
            'sSql = sSql & " IsNull(FYC.APF_TransferToFixedCapital,0) AS FYC_APF_TransferToFixedCapital,IsNull(FYP.APF_TransferToFixedCapital,0) AS FYP_APF_TransferToFixedCapital,"
            'sSql = sSql & " IsNull(FYC.APF_Drawings,0) AS FYC_APF_Drawings,IsNull(FYP.APF_Drawings,0) AS FYP_APF_Drawings,"
            'sSql = sSql & " IsNull(FYC.APF_LessOthers,0) AS FYC_APF_LessOthers,IsNull(FYP.APF_LessOthers,0) AS FYP_APF_LessOthers"
            'sSql = sSql & " From ACC_Partnership_Firms FYC"
            'sSql = sSql & " Left Join ACC_Partnership_Firms FYP on FYP.APF_YearID=" & iFinancialYearID - 1 & " And FYP.APF_Cust_ID=" & iCustomerID & " And FYC.APF_Partner_ID=FYP.APF_Partner_ID And FYP.APF_CompID=" & iAcID & ""
            'sSql = sSql & " Join SAD_Statutory_PartnerDetails On SSP_Id=FYC.APF_Partner_ID "
            'sSql = sSql & " Where FYC.APF_YearID=" & iFinancialYearID & " And FYC.APF_Cust_ID=" & iCustomerID & " And FYC.APF_CompID=" & iAcID & ""
            'dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)

            sSql = "Select SSP_PartnerName,SSP_ShareOfProfit,IsNull(SSP_CapitalAmount,0) as SSP_CapitalAmount,IsNull(FYC.APF_OpeningBalance,0) AS FYC_APF_OpeningBalance,IsNull(FYP.APF_OpeningBalance,0) AS FYP_APF_OpeningBalance,"
            sSql = sSql & " IsNull(FYC.APF_UnsecuredLoanTreatedAsCapital,0) AS FYC_APF_UnsecuredLoanTreatedAsCapital,IsNull(FYP.APF_UnsecuredLoanTreatedAsCapital,0) AS FYP_APF_UnsecuredLoanTreatedAsCapital,"
            sSql = sSql & " IsNull(FYC.APF_InterestOnCapital,0) AS FYC_APF_InterestOnCapital,IsNull(FYP.APF_InterestOnCapital,0) AS FYP_APF_InterestOnCapital,"
            sSql = sSql & " IsNull(FYC.APF_PartnersSalary,0) AS FYC_APF_PartnersSalary,IsNull(FYP.APF_PartnersSalary,0) AS FYP_APF_PartnersSalary,"
            sSql = sSql & " IsNull(FYC.APF_ShareOfprofit,0) AS FYC_APF_ShareOfprofit,IsNull(FYP.APF_ShareOfprofit,0) AS FYP_APF_ShareOfprofit,IsNull(FYC.APF_CapitalAmount,0) as FYC_APF_CapitalAmount,IsNull(FYP.APF_CapitalAmount,0) as FYP_APF_CapitalAmount,"
            sSql = sSql & " IsNull(FYC.APF_AddOthers,0) AS FYC_APF_AddOthers,IsNull(FYP.APF_AddOthers,0) AS FYP_APF_AddOthers,"
            sSql = sSql & " IsNull(FYC.APF_TransferToFixedCapital,0) AS FYC_APF_TransferToFixedCapital,IsNull(FYP.APF_TransferToFixedCapital,0) AS FYP_APF_TransferToFixedCapital,"
            sSql = sSql & " IsNull(FYC.APF_Drawings,0) AS FYC_APF_Drawings,IsNull(FYP.APF_Drawings,0) AS FYP_APF_Drawings,"
            sSql = sSql & " IsNull(FYC.APF_LessOthers,0) AS FYC_APF_LessOthers,IsNull(FYP.APF_LessOthers,0) AS FYP_APF_LessOthers, IsNull(a.Acc_PnL_Amount,0) as CYPandLAmt,isnull(b.Acc_PnL_Amount,0) as PYPandLAMt "
            sSql = sSql & " From SAD_Statutory_PartnerDetails"
            sSql = sSql & " Left Join ACC_Partnership_Firms FYC on FYC.APF_YearID=" & iFinancialYearID & " And FYC.APF_Cust_ID=" & iCustomerID & " And FYC.APF_Partner_ID=SSP_Id And FYC.APF_CompID=1 "
            sSql = sSql & " Left Join ACC_Partnership_Firms FYP on FYP.APF_YearID=" & iFinancialYearID - 1 & " And FYP.APF_Cust_ID=" & iCustomerID & " And FYP.APF_Partner_ID=SSP_Id And FYP.APF_CompID=" & iAcID & ""
            sSql = sSql & " Left Join Acc_ProfitAndLossAmount a on a.Acc_PnL_Yearid=" & iFinancialYearID & " And a.Acc_PnL_Custid=" & iCustomerID & " "
            sSql = sSql & " Left Join Acc_ProfitAndLossAmount b on b.Acc_PnL_Yearid=" & iFinancialYearID - 1 & " And b.Acc_PnL_Custid=" & iCustomerID & " "
            sSql = sSql & " Where SSP_CustID=" & iCustomerID & " And SSP_Id in (Select Distinct(APF_Partner_ID) From ACC_Partnership_Firms Where APF_YearID=" & iFinancialYearID & " Or APF_YearID=" & iFinancialYearID - 1 & ")"
            dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)

            If dtTab.Rows.Count > 0 Then
                If sIsReport = "No" Then
                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "PARTICULARS"
                    dr("FYCData") = "As at 31st March " & sFY1
                    dr("FYPData") = "As at 31st March " & sFY2
                    dt.Rows.Add(dr)
                End If

                For i = 0 To dtTab.Rows.Count - 1
                    dr = dt.NewRow()
                    iSlNo = iSlNo + 1
                    dr("SlNo") = iSlNo
                    dr("PARTICULARS") = dtTab.Rows(i)("SSP_PartnerName").ToString().ToUpper()
                    dr("FYCData") = ""
                    dr("FYPData") = ""
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Opening balance"
                    dr("FYCData") = dtTab.Rows(i)("FYC_APF_OpeningBalance")
                    dr("FYPData") = dtTab.Rows(i)("FYP_APF_OpeningBalance")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Add: Capital Introduced - Unsecured Loan treated as Capital"
                    dr("FYCData") = dtTab.Rows(i)("FYC_APF_UnsecuredLoanTreatedAsCapital")
                    dr("FYPData") = dtTab.Rows(i)("FYP_APF_UnsecuredLoanTreatedAsCapital")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Add: Interest on Capital"
                    dr("FYCData") = dtTab.Rows(i)("FYC_APF_InterestOnCapital")
                    dr("FYPData") = dtTab.Rows(i)("FYP_APF_InterestOnCapital")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Add: Partner's salary"
                    dr("FYCData") = dtTab.Rows(i)("FYC_APF_PartnersSalary")
                    dr("FYPData") = dtTab.Rows(i)("FYP_APF_PartnersSalary")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    If IsDBNull(dtTab.Rows(i)("SSP_ShareOfProfit")) = False Then
                        dr("PARTICULARS") = "Add: Share of profit(" & dtTab.Rows(i)("SSP_ShareOfProfit") & "%)"
                    Else
                        dr("PARTICULARS") = "Add: Share of profit"
                    End If
                    '20_07_23
                    'dr("FYCData") = dtTab.Rows(i)("FYC_APF_ShareOfprofit")
                    'dr("FYPData") = dtTab.Rows(i)("FYP_APF_ShareOfprofit")
                    Dim dFYCData As Double = 0.0
                    Dim dFYPData As Double = 0.0
                    'dFYCData = Convert.ToDecimal(Val((dtTab.Rows(i)("SSP_ShareOfProfit") * dtTab.Rows(i)("CYPandLAmt")) / 100)).ToString("#,##0.00")
                    'dFYPData = Convert.ToDecimal(Val((dtTab.Rows(i)("SSP_ShareOfProfit") * dtTab.Rows(i)("PYPandLAMt")) / 100)).ToString("#,##0.00")
                    dFYCData = Convert.ToDecimal(Val(dtTab.Rows(i)("FYC_APF_ShareOfprofit")))
                    dFYPData = Convert.ToDecimal(Val(dtTab.Rows(i)("FYP_APF_ShareOfprofit")))
                    dr("FYCData") = dFYCData.ToString("#,##0.00")
                    dr("FYPData") = dFYPData.ToString("#,##0.00")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Add: Others"
                    dCOthers = dtTab.Rows(i)("FYC_APF_AddOthers")
                    dPOthers = dtTab.Rows(i)("FYP_APF_AddOthers")
                    dr("FYCData") = dCOthers.ToString("#,##0.00")
                    dr("FYPData") = dPOthers.ToString("#,##0.00")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Add Total"
                    dFYCAdd = dtTab.Rows(i)("FYC_APF_OpeningBalance") + dtTab.Rows(i)("FYC_APF_UnsecuredLoanTreatedAsCapital") + dtTab.Rows(i)("FYC_APF_InterestOnCapital") + dtTab.Rows(i)("FYC_APF_PartnersSalary") + dFYCData + dtTab.Rows(i)("FYC_APF_AddOthers")
                    dFYPAdd = dtTab.Rows(i)("FYP_APF_OpeningBalance") + dtTab.Rows(i)("FYP_APF_UnsecuredLoanTreatedAsCapital") + dtTab.Rows(i)("FYP_APF_InterestOnCapital") + dtTab.Rows(i)("FYP_APF_PartnersSalary") + dFYPData + dtTab.Rows(i)("FYP_APF_AddOthers")
                    dr("FYCData") = dFYCAdd
                    dr("FYPData") = dFYPAdd
                    dt.Rows.Add(dr)

                    'dr = dt.NewRow()
                    'dr("SlNo") = ""
                    'dr("PARTICULARS") = ""
                    'dr("FYCData") = ""
                    'dr("FYPData") = ""
                    'dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Less: Transfer to Fixed Capital"
                    dr("FYCData") = dtTab.Rows(i)("FYC_APF_TransferToFixedCapital")
                    dr("FYPData") = dtTab.Rows(i)("FYP_APF_TransferToFixedCapital")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Less: Drawings"
                    dr("FYCData") = dtTab.Rows(i)("FYC_APF_Drawings")
                    dr("FYPData") = dtTab.Rows(i)("FYP_APF_Drawings")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Less: Others"
                    dCLessOthers = dtTab.Rows(i)("FYC_APF_LessOthers")
                    dPLessOthers = dtTab.Rows(i)("FYP_APF_LessOthers")
                    dr("FYCData") = dCLessOthers.ToString("#,##0.00")
                    dr("FYPData") = dPLessOthers.ToString("#,##0.00")
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Less Total"
                    dFYCLess = dtTab.Rows(i)("FYC_APF_TransferToFixedCapital") + dtTab.Rows(i)("FYC_APF_Drawings") + dtTab.Rows(i)("FYC_APF_LessOthers")
                    dFYPLess = dtTab.Rows(i)("FYP_APF_TransferToFixedCapital") + dtTab.Rows(i)("FYP_APF_Drawings") + dtTab.Rows(i)("FYP_APF_LessOthers")
                    dr("FYCData") = dFYCLess
                    dr("FYPData") = dFYPLess
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = ""
                    dr("FYCData") = ""
                    dr("FYPData") = ""
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "Total"
                    dr("FYCData") = dFYCAdd - dFYCLess
                    dr("FYPData") = dFYPAdd - dFYPLess
                    dt.Rows.Add(dr)

                    dFYCTotal = dFYCTotal + (dFYCAdd - dFYCLess)
                    dFYPTotal = dFYPTotal + (dFYPAdd - dFYPLess)
                    dFYCAdd = 0 : dFYCLess = 0
                    dFYPAdd = 0 : dFYPLess = 0

                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = ""
                    dr("FYCData") = ""
                    dr("FYPData") = ""
                    dt.Rows.Add(dr)
                Next

                dr = dt.NewRow()
                dr("SlNo") = ""
                dr("PARTICULARS") = "TOTAL - CURRENT A/C CAPITAL"
                dr("FYCData") = dFYCTotal
                dr("FYPData") = dFYPTotal
                dt.Rows.Add(dr)

                'Darshan 
                dtTotalAmt = GetHeadingAmt1(sAc, iFinancialYearID, iCustomerID, 4, 42)

                dr = dt.NewRow()
                dr("PARTICULARS") = "PARTNER'S FIXED CAPITAL"
                dt.Rows.Add(dr)

                If sIsReport = "No" Then
                    dr = dt.NewRow()
                    dr("SlNo") = ""
                    dr("PARTICULARS") = "PARTICULARS"
                    dr("FYCData") = "As at 31st March " & sFY1
                    dr("FYPData") = "As at 31st March " & sFY2
                    dt.Rows.Add(dr)
                End If
                iSlNo = 0
                Dim dCapitalAmountC As Decimal = 0
                Dim dCapitalAmountP As Decimal = 0
                For i = 0 To dtTab.Rows.Count - 1
                    dr = dt.NewRow()
                    iSlNo = iSlNo + 1
                    dr("SlNo") = iSlNo
                    dr("PARTICULARS") = dtTab.Rows(i)("SSP_PartnerName").ToString().ToUpper()
                    'dr("FYCData") = Convert.ToDecimal(Val(dtTotalAmt(0)("DbTotal")) * dtTab.Rows(i)("SSP_ShareOfProfit") / 100).ToString("#,##0.00")
                    'dr("FYPData") = Convert.ToDecimal(Val(dtTotalAmt(0)("PrevDbTotal")) * dtTab.Rows(i)("SSP_ShareOfProfit") / 100).ToString("#,##0.00")

                    '09-02-24 CustCapitalAmount Amt
                    dr("FYCData") = Convert.ToDecimal(dtTab.Rows(i)("FYC_APF_CapitalAmount")).ToString("#,##0.00")
                    dr("FYPData") = Convert.ToDecimal(dtTab.Rows(i)("FYP_APF_CapitalAmount")).ToString("#,##0.00")
                    dt.Rows.Add(dr)

                    dCapitalAmountC = dCapitalAmountC + dr("FYCData")
                    dCapitalAmountP = dCapitalAmountP + dr("FYPData")
                Next
                Convert.ToDecimal(Val(dtTotalAmt(0)("PrevDbTotal") + dFYPTotal)).ToString("#,##0.00")

                dr = dt.NewRow()
                dr("SlNo") = ""
                dr("PARTICULARS") = "Total"
                dr("FYCData") = Convert.ToDecimal(Val(dCapitalAmountC)).ToString("#,##0.00")
                dr("FYPData") = Convert.ToDecimal(Val(dCapitalAmountP)).ToString("#,##0.00")
                dt.Rows.Add(dr)

                dr = dt.NewRow()
                dr("SlNo") = ""
                dr("PARTICULARS") = "Total Capital"
                dr("FYCData") = Convert.ToDecimal(Val(dFYCTotal + dCapitalAmountC)).ToString("#,##0.00")
                dr("FYPData") = Convert.ToDecimal(Val(dFYPTotal + dCapitalAmountP)).ToString("#,##0.00")
                dt.Rows.Add(dr)

            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetHeadingAmt1(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal iSchedType As Integer, ByVal iHeadingId As Integer) As DataTable
        Dim sSQL As String = ""
        Dim dTotalDieselAmount As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "          Select ISNULL(Sum(d.ATBU_Closing_TotalCredit_Amount+0),0)-    "
            sSQL = sSQL & "      ISNULL(Sum(d.ATBU_Closing_TotalDebit_Amount+0),0)  as DbTotal, "
            sSQL = sSQL & "    ISNULL(Sum(e.ATBU_Closing_TotalCredit_Amount+0),0)  - "
            sSQL = sSQL & "  ISNULL(Sum(e.ATBU_Closing_TotalDebit_Amount+0),0)  as PrevDbTotal  "
            sSQL = sSQL & "    from Acc_TrailBalance_Upload_Details  "
            sSQL = sSQL & "  left join ACC_ScheduleSubHeading a on a.ASSH_ID= ATBUD_Subheading "
            sSQL = sSQL & "  left join Acc_TrailBalance_Upload d on d.ATBU_Description = ATBUD_Description "
            sSQL = sSQL & " And d.ATBU_YEARId=" & iYearID & "  and d.ATBU_CustId= " & iCustomerID & "  and ATBUD_YEARId =" & iYearID & "   "
            sSQL = sSQL & "  left join Acc_TrailBalance_Upload e on e.ATBU_Description = ATBUD_Description "
            sSQL = sSQL & " And e.ATBU_YEARId=" & iYearID - 1 & "  and e.ATBU_CustId= " & iCustomerID & "  and ATBUD_YEARId =" & iYearID - 1 & "     "
            sSQL = sSQL & "  where ATBUD_Schedule_type =4 and  ATBUD_Subheading=  160     "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSQL)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPandLAmt(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal iPartnerID As Integer, ByVal iSchedType As Integer, ByVal iHeadingId As Integer) As DataTable
        Dim sSQL As String = ""
        Dim dTotalDieselAmount As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "select sum((SSP_ShareOfProfit * a.Acc_PnL_Amount)/100 ) as PandLAmt   "
            sSQL = sSQL & "    from SAD_Statutory_PartnerDetails  "
            sSQL = sSQL & "  left join Acc_ProfitAndLossAmount a on a.Acc_PnL_Custid=SSP_CustID "
            sSQL = sSQL & "  where ssp_id =" & iPartnerID & " and  a.Acc_PnL_Yearid=  " & iYearID & "     "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSQL)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class