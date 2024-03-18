
Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer

Public Class ClsTrade
    Dim objDBL As New DatabaseLayer.DBHelper
    'Trade Trail Balance
    Private ATU_ID As Integer
    Private ATU_Name As String
    Private ATU_CustId As Integer
    Private ATU_Category As Integer
    Private ATU_OtherType As Integer
    Private ATU_Less_than_six_Month As Double
    Private ATU_More_than_six_Month As Double
    Private ATU_One_Year As Double
    Private ATU_Two_Year As Double
    Private ATU_Three_Year As Double
    Private ATU_More_than As Double
    Private ATU_Total_Amount As Double
    Private ATU_CRBY As Integer
    Private ATU_UPDATEDBY As Integer
    Private ATU_IPAddress As String
    Private ATU_YEARId As Integer
    Private ATU_Branchname As Integer


    Public Property iATU_ID() As Integer
        Get
            Return (ATU_ID)
        End Get
        Set(ByVal Value As Integer)
            ATU_ID = Value
        End Set
    End Property
    Public Property sATU_Name() As String
        Get
            Return (ATU_Name)
        End Get
        Set(ByVal Value As String)
            ATU_Name = Value
        End Set
    End Property
    Public Property iATU_CustId() As Integer
        Get
            Return (ATU_CustId)
        End Get
        Set(ByVal Value As Integer)
            ATU_CustId = Value
        End Set
    End Property
    Public Property iATU_Category() As Integer
        Get
            Return (ATU_Category)
        End Get
        Set(ByVal Value As Integer)
            ATU_Category = Value
        End Set
    End Property
    Public Property iATU_OtherType() As Integer
        Get
            Return (ATU_OtherType)
        End Get
        Set(ByVal Value As Integer)
            ATU_OtherType = Value
        End Set
    End Property
    Public Property dATU_Less_than_six_Month() As Double
        Get
            Return (ATU_Less_than_six_Month)
        End Get
        Set(ByVal Value As Double)
            ATU_Less_than_six_Month = Value
        End Set
    End Property
    Public Property dATU_More_than_six_Month() As Double
        Get
            Return (ATU_More_than_six_Month)
        End Get
        Set(ByVal Value As Double)
            ATU_More_than_six_Month = Value
        End Set
    End Property
    Public Property dATU_One_Year() As Double
        Get
            Return (ATU_One_Year)
        End Get
        Set(ByVal Value As Double)
            ATU_One_Year = Value
        End Set
    End Property
    Public Property dATU_Two_Year() As Double
        Get
            Return (ATU_Two_Year)
        End Get
        Set(ByVal Value As Double)
            ATU_Two_Year = Value
        End Set
    End Property
    Public Property dATU_Three_Year() As Double
        Get
            Return (ATU_Three_Year)
        End Get
        Set(ByVal Value As Double)
            ATU_Three_Year = Value
        End Set
    End Property
    Public Property dATU_More_than() As Double
        Get
            Return (ATU_More_than)
        End Get
        Set(ByVal Value As Double)
            ATU_More_than = Value
        End Set
    End Property
    Public Property dATU_Total_Amount() As Double
        Get
            Return (ATU_Total_Amount)
        End Get
        Set(ByVal Value As Double)
            ATU_Total_Amount = Value
        End Set
    End Property
    Public Property iATU_CRBY() As Integer
        Get
            Return (ATU_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ATU_CRBY = Value
        End Set
    End Property
    Public Property iATU_UPDATEDBY() As Integer
        Get
            Return (ATU_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ATU_UPDATEDBY = Value
        End Set
    End Property
    Public Property sATU_IPAddress() As String
        Get
            Return (ATU_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATU_IPAddress = Value
        End Set
    End Property
    Public Property iATU_YEARId() As Integer
        Get
            Return (ATU_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ATU_YEARId = Value
        End Set
    End Property
    Public Property iATU_Branchname() As Integer
        Get
            Return (ATU_Branchname)
        End Get
        Set(ByVal Value As Integer)
            ATU_Branchname = Value
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

    Public Function SaveTradeExcelUpload(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsTrade As ClsTrade)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.ATU_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_Name", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsTrade.ATU_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.iATU_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_Category", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.iATU_Category
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_OtherType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.iATU_OtherType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_Less_than_six_Month", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTrade.dATU_Less_than_six_Month
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_More_than_six_Month", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTrade.dATU_More_than_six_Month
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_One_Year", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTrade.dATU_One_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_Two_Year", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTrade.dATU_Two_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_Three_Year", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTrade.dATU_Three_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_More_than", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTrade.dATU_More_than
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_Total_Amount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsTrade.dATU_Total_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.iATU_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.iATU_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsTrade.sATU_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_YEARId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.iATU_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATU_Branchid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTrade.iATU_Branchname
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_Trade_Upload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function TRChecksdata(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer, ByVal IBranchid As Integer, ByVal iATU_Category As Integer, ByVal iOtherType As Integer) As Integer
        Dim sSql As String
        Dim chkrec As Integer
        Try
            sSql = "SELECT isnull(ATU_ID,0) as ATU_ID FROM Acc_Trade_Upload WHERE "
            sSql = sSql & "ATU_CustId=" & iCustid & " AND ATU_Branchid=" & IBranchid & " AND ATU_YEARId=" & iYearID & " AND ATU_Category=" & iATU_Category
            chkrec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If chkrec > 0 Then
                sSql = " delete  from Acc_Trade_Upload Where "
                sSql = sSql & "ATU_CustId=" & iCustid & " And ATU_Category=" & iATU_Category & " and ATU_OtherType = " & iOtherType & " And ATU_YEARId=" & iYearID & " And  ATU_Branchid=" & IBranchid & " "
                objDBL.SQLExecuteDataSet(sAC, sSql)
            End If
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
    Public Function GetTrDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer, ByVal IBranchid As Integer) As DataSet
        Dim sSql As String
        Dim Dataset As New DataSet
        Try
            ' Assuming dATU_Less_than_six_Month, ATU_More_than_six_Month, ATU_One_Year, ATU_Two_Year,
            ' ATU_Three_Year, ATU_More_than, and ATU_Total_Amount are variables or constants that should be declared and assigned before using them in the query.
            sSql = "Select  ROW_NUMBER() OVER (ORDER BY ATU_ID ASC) As SrNo, ATU_Name,ATU_More_than_six_Month,ATU_Less_than_six_Month, ATU_One_Year,ATU_Two_Year,ATU_Three_Year,ATU_More_than ,ATU_Total_Amount FROM Acc_Trade_Upload WHERE ATU_CustId = " & iCustid & " And ATU_Category = " & iATU_Category & " And ATU_YEARId = " & iYearID & " And  ATU_Branchid=" & IBranchid & ""

            ' Assuming objDBL is an instance of your database access class.
            Dataset = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return Dataset
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCTrDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer, ByVal iATU_Category As Integer, ByVal iBranchid As Integer, ByVal iOtherType As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            ' Assuming dATU_Less_than_six_Month, ATU_More_than_six_Month, ATU_One_Year, ATU_Two_Year,
            ' ATU_Three_Year, ATU_More_than, and ATU_Total_Amount are variables or constants that should be declared and assigned before using them in the query.
            sSql = "Select  ROW_NUMBER() OVER (ORDER BY ATU_ID ASC) As SrNo, ATU_Name,ATU_More_than_six_Month,ATU_Less_than_six_Month, ATU_One_Year,ATU_Two_Year,ATU_Three_Year,ATU_More_than ,ATU_Total_Amount FROM Acc_Trade_Upload WHERE ATU_CustId = " & iCustid & " And ATU_Category = " & iATU_Category & " And ATU_YEARId = " & iYearID & " And  ATU_Branchid=" & iBranchid & " and ATU_OtherType = " & iOtherType & ""


            ' Assuming objDBL is an instance of your database access class.
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotal(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iYearID As Integer, ByVal iBranchid As Integer) As DataSet
        Dim sSql As String = ""
        Dim Imaxid As Integer = 0

        Try
            sSql = "SELECT CASE ATU_OtherType WHEN 1 THEN 'MSME' WHEN 2 THEN 'Others' WHEN 3 THEN 'Dispute dues-MSME' WHEN 4 THEN 'Dispute dues' WHEN 5 THEN 'Others' ELSE NULL END AS Name,"
            sSql &= " SUM(ATU_More_than_six_Month) AS Total_More_than_six_Month, SUM(ATU_Less_than_six_Month) AS Total_Less_than_six_Month,"
            sSql &= " SUM(ATU_One_Year) AS Total_One_Year, SUM(ATU_Two_Year) AS Total_Two_Year, SUM(ATU_Three_Year) AS Total_Three_Year,"
            sSql &= " SUM(ATU_More_than) AS Total_More_than, SUM(ATU_Total_Amount) AS Total_Amount, ATU_Category"
            sSql &= " FROM Acc_Trade_Upload"
            sSql &= " WHERE ATU_YEARId = " & iYearID & " AND ATU_Branchid = " & iBranchid & ""
            sSql &= " AND ATU_CustId = " & iCustid & " AND ATU_category = 1 AND ATU_OtherType IN (1,2,3,4,5)"
            sSql &= " GROUP BY ATU_Category, ATU_OtherType;"


            sSql &= "SELECT CASE ATU_OtherType WHEN 1 THEN 'MSME' WHEN 2 THEN 'Others' WHEN 3 THEN 'Dispute dues-MSME' WHEN 4 THEN 'Dispute dues' WHEN 5 THEN 'Others' ELSE NULL END AS Name,"
            sSql &= " SUM(ATU_More_than_six_Month) AS Total_More_than_six_Month, SUM(ATU_Less_than_six_Month) AS Total_Less_than_six_Month,"
            sSql &= " SUM(ATU_One_Year) AS Total_One_Year, SUM(ATU_Two_Year) AS Total_Two_Year, SUM(ATU_Three_Year) AS Total_Three_Year,"
            sSql &= " SUM(ATU_More_than) AS Total_More_than, SUM(ATU_Total_Amount) AS Total_Amount, ATU_Category"
            sSql &= " FROM Acc_Trade_Upload"
            sSql &= " WHERE ATU_YEARId = " & iYearID & " AND ATU_Branchid = " & iBranchid & ""
            sSql &= " AND ATU_CustId = " & iCustid & " AND ATU_category = 2 AND ATU_OtherType IN (1,2,3,4,5)"
            sSql &= " GROUP BY ATU_Category, ATU_OtherType;"
            Dim dt2 As DataSet = objDBL.SQLExecuteDataSet(sAC, sSql)

            Return dt2
        Catch ex As Exception

        End Try
    End Function
    Public Function LoadBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iYearID As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_Id As Branchid,Mas_Description As BranchName from SAD_CUST_LOCATION where Mas_CustID=" & iCustId & " And Mas_CompID=" & iACID & ""
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
End Class
