Public Structure strITReturns_Client
    Private ITR_ID As Integer
    Private ITR_ClientName As String
    Private ITR_PAN As String
    Private ITR_Aadhaar As String
    Private ITR_DOB As DateTime
    Private ITR_Phone As String
    Private ITR_Email As String
    Private ITR_ITLoginId As String
    Private ITR_ITPassword As String
    Private ITR_CrBy As Integer
    Private ITR_UpdatedBy As Integer
    Private ITR_IPAddress As String
    Private ITR_CompID As Integer
    Public Property iITR_ID() As Integer
        Get
            Return (ITR_ID)
        End Get
        Set(ByVal Value As Integer)
            ITR_ID = Value
        End Set
    End Property
    Public Property sITR_ClientName() As String
        Get
            Return (ITR_ClientName)
        End Get
        Set(ByVal Value As String)
            ITR_ClientName = Value
        End Set
    End Property
    Public Property sITR_PAN() As String
        Get
            Return (ITR_PAN)
        End Get
        Set(ByVal Value As String)
            ITR_PAN = Value
        End Set
    End Property
    Public Property sITR_Aadhaar() As String
        Get
            Return (ITR_Aadhaar)
        End Get
        Set(ByVal Value As String)
            ITR_Aadhaar = Value
        End Set
    End Property
    Public Property dITR_DOB() As DateTime
        Get
            Return (ITR_DOB)
        End Get
        Set(ByVal Value As DateTime)
            ITR_DOB = Value
        End Set
    End Property
    Public Property sITR_Phone() As String
        Get
            Return (ITR_Phone)
        End Get
        Set(ByVal Value As String)
            ITR_Phone = Value
        End Set
    End Property
    Public Property sITR_Email() As String
        Get
            Return (ITR_Email)
        End Get
        Set(ByVal Value As String)
            ITR_Email = Value
        End Set
    End Property
    Public Property sITR_ITLoginId() As String
        Get
            Return (ITR_ITLoginId)
        End Get
        Set(ByVal Value As String)
            ITR_ITLoginId = Value
        End Set
    End Property
    Public Property sITR_ITPassword() As String
        Get
            Return (ITR_ITPassword)
        End Get
        Set(ByVal Value As String)
            ITR_ITPassword = Value
        End Set
    End Property
    Public Property iITR_CrBy() As Integer
        Get
            Return (ITR_CrBy)
        End Get
        Set(ByVal Value As Integer)
            ITR_CrBy = Value
        End Set
    End Property
    Public Property iITR_UpdatedBy() As Integer
        Get
            Return (ITR_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ITR_UpdatedBy = Value
        End Set
    End Property
    Public Property sITR_IPAddress() As String
        Get
            Return (ITR_IPAddress)
        End Get
        Set(ByVal Value As String)
            ITR_IPAddress = Value
        End Set
    End Property
    Public Property iITR_CompID() As Integer
        Get
            Return (ITR_CompID)
        End Get
        Set(ByVal Value As Integer)
            ITR_CompID = Value
        End Set
    End Property
End Structure

Public Structure strITReturnsFiling_Details
    Private ITRFD_ID As Integer
    Private ITRFD_ITR_ID As Integer
    Private ITRFD_ITRNo As Integer
    Private ITRFD_FinancialYearID As Integer
    Private ITRFD_AssessmentYearID As Integer
    Private ITRFD_ServiceChargeInINR As Decimal
    Private ITRFD_Status As Integer
    Private ITRFD_InvoiceMail As Integer
    Private ITRFD_AssignTo As Integer
    Private ITRFD_BillingEntityId As Integer
    Private ITRFD_CrBy As Integer
    Private ITRFD_UpdatedBy As Integer
    Private ITRFD_IPAddress As String
    Private ITRFD_CompID As Integer
    Public Property iITRFD_ID() As Integer
        Get
            Return (ITRFD_ID)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_ID = Value
        End Set
    End Property
    Public Property iITRFD_ITR_ID() As Integer
        Get
            Return (ITRFD_ITR_ID)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_ITR_ID = Value
        End Set
    End Property
    Public Property sITRFD_ITRNo() As String
        Get
            Return (ITRFD_ITRNo)
        End Get
        Set(ByVal Value As String)
            ITRFD_ITRNo = Value
        End Set
    End Property
    Public Property iITRFD_FinancialYearID() As Integer
        Get
            Return (ITRFD_FinancialYearID)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_FinancialYearID = Value
        End Set
    End Property
    Public Property iITRFD_AssessmentYearID() As Integer
        Get
            Return (ITRFD_AssessmentYearID)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_AssessmentYearID = Value
        End Set
    End Property
    Public Property dITRFD_ServiceChargeInINR() As Decimal
        Get
            Return (ITRFD_ServiceChargeInINR)
        End Get
        Set(ByVal Value As Decimal)
            ITRFD_ServiceChargeInINR = Value
        End Set
    End Property
    Public Property iITRFD_Status() As Integer
        Get
            Return (ITRFD_Status)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_Status = Value
        End Set
    End Property
    Public Property iITRFD_InvoiceMail() As Integer
        Get
            Return (ITRFD_InvoiceMail)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_InvoiceMail = Value
        End Set
    End Property
    Public Property iITRFD_AssignTo() As Integer
        Get
            Return (ITRFD_AssignTo)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_AssignTo = Value
        End Set
    End Property
    Public Property iITRFD_BillingEntityId() As Integer
        Get
            Return (ITRFD_BillingEntityId)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_BillingEntityId = Value
        End Set
    End Property

    Public Property iITRFD_CrBy() As Integer
        Get
            Return (ITRFD_CrBy)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_CrBy = Value
        End Set
    End Property
    Public Property iITRFD_UpdatedBy() As Integer
        Get
            Return (ITRFD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_UpdatedBy = Value
        End Set
    End Property
    Public Property sITRFD_IPAddress() As String
        Get
            Return (ITRFD_IPAddress)
        End Get
        Set(ByVal Value As String)
            ITRFD_IPAddress = Value
        End Set
    End Property
    Public Property iITRFD_CompID() As Integer
        Get
            Return (ITRFD_CompID)
        End Get
        Set(ByVal Value As Integer)
            ITRFD_CompID = Value
        End Set
    End Property
End Structure
Public Class clsITReturnsFiling
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function LoadITRClients(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select ITR_ID,ITR_ClientName from ITReturns_Client Where ITR_CompID=" & iACID & " order by ITR_ClientName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedITRFilingClientDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iITRClientID As Integer, ByVal iFinancialYearID As Integer, ByVal iAssessmentYearID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select ITR_ID,ITR_ClientName,ITR_PAN,ITR_Aadhaar,ITR_DOB,ITR_Phone,ITR_Email,ITRFD_AssignTo,ITRFD_BillingEntityId,ITR_ITLoginId,ITR_ITPassword,ITRFD_ID,ITRFD_ITRNo,ITRFD_FinancialYearID,ITRFD_AssessmentYearID,ITRFD_ServiceChargeInINR,ITRFD_Status,ITRFD_InvoiceMail"
            sSql = sSql & " From ITReturns_Client Left Join ITReturnsFiling_Details On ITRFD_ITR_ID=ITR_ID And ITRFD_CompID=" & iACID & ""
            sSql = sSql & " Where ITR_ID=" & iITRClientID & " And ITR_CompID=" & iACID & " And ITRFD_FinancialYearID=" & iFinancialYearID & " And ITRFD_AssessmentYearID=" & iAssessmentYearID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllITRFilingClientDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iITRClientID As Integer, ByVal iFinancialYearID As Integer, ByVal iAssessmentYearID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select ITR_ID As ClientID,ITRFD_ID As ITRFID,ITRFD_ITRNo As ITRNo,ITR_ClientName + ' (' + ITR_PAN +   ')' As ClientNamePAN,DATEDIFF(year, ITR_DOB,GETDATE()) As Age,ITR_Aadhaar As Aadhaar,ITRFD_Status As StatusID,"
            sSql = sSql & " Case When ITRFD_Status=0 then '' When ITRFD_Status=1 then 'Assigned' when ITRFD_Status=2 then 'Completed' when ITRFD_Status=3 then 'Invoice Raised' when ITRFD_Status=4 then 'Paid' End As Status"
            sSql = sSql & " From ITReturns_Client Left Join ITReturnsFiling_Details On ITRFD_ITR_ID=ITR_ID And ITRFD_CompID=" & iACID & ""
            sSql = sSql & " Where ITR_CompID=" & iACID & " And ITRFD_FinancialYearID=" & iFinancialYearID & " And ITRFD_AssessmentYearID=" & iAssessmentYearID & ""
            If iITRClientID > 0 Then
                sSql = sSql & " And ITR_ID=" & iITRClientID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckITRClientName(ByVal sAC As String, ByVal iACID As Integer, ByVal sClientName As String, ByVal iClientPKId As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select ITR_ID from ITReturns_Client where ITR_ClientName='" & sClientName & "' And ITR_CompID=" & iACID & ""
            If iClientPKId > 0 Then
                sSql = sSql & " And ITR_ID<>" & iClientPKId & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveITReturnsClientDetails(ByVal sAC As String, ByVal objITR As strITReturns_Client)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITR.iITR_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_ClientName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objITR.sITR_ClientName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_PAN", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objITR.sITR_PAN
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_Aadhaar", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objITR.sITR_Aadhaar
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_DOB", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objITR.dITR_DOB
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_Phone", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objITR.sITR_Phone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_Email", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objITR.sITR_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_ITLoginId", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objITR.sITR_ITLoginId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_ITPassword", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objITR.sITR_ITPassword
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITR.iITR_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITR.iITR_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("ITR_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objITR.sITR_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITR.iITR_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spITReturns_Client", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveITReturnsClientFilingDetails(ByVal sAC As String, ByVal objITRFD As strITReturnsFiling_Details, ByVal sYearName As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_ITR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_ITR_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_ITRNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsGeneralFunctions.GetAllModuleJobCode(sAC, objITRFD.iITRFD_CompID, "ITR", objITRFD.iITRFD_FinancialYearID, sYearName, 0)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_FinancialYearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_FinancialYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_AssessmentYearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_AssessmentYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_ServiceChargeInINR", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objITRFD.dITRFD_ServiceChargeInINR
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_Status", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_InvoiceMail", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_InvoiceMail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_AssignTo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_AssignTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_BillingEntityId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_BillingEntityId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objITRFD.sITRFD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ITRFD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objITRFD.iITRFD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spITReturnsFiling_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadClientDetailsForInvoice(ByVal sAc As String, ByVal iAcID As Integer, ByVal iClientID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtComp As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("CUST_NAME")
            dt.Columns.Add("CUST_ADDRESS")
            dt.Columns.Add("CUST_CITY_PIN")
            dt.Columns.Add("CUST_STATE")
            dt.Columns.Add("CUST_EMAIL")
            dt.Columns.Add("CUST_TELPHONE")
            dt.Columns.Add("CUST_PAN")
            dt.Columns.Add("CUST_GSTIN")

            sSql = "Select ITR_ClientName,ITR_PAN,ITR_Phone From ITReturns_Client Where ITR_ID=" & iClientID & " And ITR_CompID=" & iAcID & ""
            dtComp = objDBL.SQLExecuteDataTable(sAc, sSql)
            dr = dt.NewRow()
            dr("CUST_NAME") = dtComp.Rows(0)("ITR_ClientName")
            dr("CUST_ADDRESS") = ""
            dr("CUST_CITY_PIN") = ""
            dr("CUST_STATE") = ""
            dr("CUST_EMAIL") = ""
            dr("CUST_TELPHONE") = "Phone no.: " & dtComp.Rows(0)("ITR_Phone")
            dr("CUST_PAN") = "PAN: " & dtComp.Rows(0)("ITR_PAN")
            dr("CUST_GSTIN") = ""
            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEntityIDFormClientDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iITRFDPkID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select ITRFD_BillingEntityId From ITReturnsFiling_Details Where ITRFD_ID=" & iITRFDPkID & " And ITRFD_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAmountFormClientDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iITRFDPkID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ITRFD_ServiceChargeInINR From ITReturnsFiling_Details Where ITRFD_ID=" & iITRFDPkID & " And ITRFD_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadClientITRFDDetailsForInvoice(ByVal sAc As String, ByVal iAcID As Integer, ByVal iITRFDPkID As Integer) As DataTable
        Dim dt As New DataTable, dtInvoice As New DataTable
        Dim dr As DataRow
        Dim sSql As String, iSlNo As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ItemName")
            dt.Columns.Add("Description")
            dt.Columns.Add("HSNSAC")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("PricePerUnit")
            dt.Columns.Add("Amount")

            sSql = "Select ITRFD_ITRNo,ITR_ClientName + ' (' + ITR_PAN +   ')' As ClientNamePAN,DATEDIFF(year, ITR_DOB,GETDATE()) As Age,ITR_Aadhaar As Aadhaar,ITRFD_ServiceChargeInINR,"
            sSql = sSql & " FY.YMS_ID As FinancialYear,AY.YMS_ID As AssessmentYear,ITRFD_AssignTo From ITReturnsFiling_Details"
            sSql = sSql & " Left Join ITReturns_Client On ITR_ID=ITRFD_ITR_ID"
            sSql = sSql & " Left Join Year_Master FY on FY.YMS_YearID=ITRFD_FinancialYearID"
            sSql = sSql & " Left Join Year_Master AY on AY.YMS_YearID=ITRFD_AssessmentYearID"
            sSql = sSql & " Where ITR_ID=" & iITRFDPkID & " And ITR_CompID=" & iAcID & ""

            dtInvoice = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtInvoice.Rows.Count - 1
                dr = dt.NewRow()
                iSlNo = iSlNo + 1
                dr("SrNo") = iSlNo
                dr("ItemName") = dtInvoice.Rows(i)("ITRFD_ITRNo")
                dr("Description") = dtInvoice.Rows(i)("ClientNamePAN")
                dr("HSNSAC") = dtInvoice.Rows(i)("FinancialYear")
                dr("Quantity") = dtInvoice.Rows(i)("AssessmentYear")
                dr("PricePerUnit") = dtInvoice.Rows(i)("ITRFD_AssignTo")
                dr("Amount") = "₹" & dtInvoice.Rows(i)("ITRFD_ServiceChargeInINR")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class