Public Structure strAuditAssignment_Schedule
    Private AAS_ID As Integer
    Private AAS_AssignmentNo As String
    Private AAS_CustID As Integer
    Private AAS_PartnerID As Integer
    Private AAS_YearID As Integer
    Private AAS_MonthID As Integer
    Private AAS_TaskID As Integer
    Private AAS_Status As Integer
    Private AAS_AdvancePartialBilling As Integer
    Private AAS_BillingType As Integer
    Private AAS_AssessmentYearID As String
    Private AAS_AttachID As Integer
    Private AAS_CrBy As Integer
    Private AAS_UpdatedBy As Integer
    Private AAS_IPAddress As String
    Private AAS_CompID As Integer
    Private AAS_IsComplianceAsg As Integer
    Public Property iAAS_ID() As Integer
        Get
            Return (AAS_ID)
        End Get
        Set(ByVal Value As Integer)
            AAS_ID = Value
        End Set
    End Property
    Public Property sAAS_AssignmentNo() As String
        Get
            Return (AAS_AssignmentNo)
        End Get
        Set(ByVal Value As String)
            AAS_AssignmentNo = Value
        End Set
    End Property
    Public Property iAAS_CustID() As Integer
        Get
            Return (AAS_CustID)
        End Get
        Set(ByVal Value As Integer)
            AAS_CustID = Value
        End Set
    End Property
    Public Property iAAS_PartnerID() As Integer
        Get
            Return (AAS_PartnerID)
        End Get
        Set(ByVal Value As Integer)
            AAS_PartnerID = Value
        End Set
    End Property
    Public Property iAAS_MonthID() As Integer
        Get
            Return (AAS_MonthID)
        End Get
        Set(ByVal Value As Integer)
            AAS_MonthID = Value
        End Set
    End Property
    Public Property iAAS_YearID() As Integer
        Get
            Return (AAS_YearID)
        End Get
        Set(ByVal Value As Integer)
            AAS_YearID = Value
        End Set
    End Property
    Public Property iAAS_TaskID() As Integer
        Get
            Return (AAS_TaskID)
        End Get
        Set(ByVal Value As Integer)
            AAS_TaskID = Value
        End Set
    End Property
    Public Property iAAS_AdvancePartialBilling() As Integer
        Get
            Return (AAS_AdvancePartialBilling)
        End Get
        Set(ByVal Value As Integer)
            AAS_AdvancePartialBilling = Value
        End Set
    End Property
    Public Property iAAS_BillingType() As Integer
        Get
            Return (AAS_BillingType)
        End Get
        Set(ByVal Value As Integer)
            AAS_BillingType = Value
        End Set
    End Property
    Public Property sAAS_AssessmentYearID() As String
        Get
            Return (AAS_AssessmentYearID)
        End Get
        Set(ByVal Value As String)
            AAS_AssessmentYearID = Value
        End Set
    End Property
    Public Property iAAS_Status() As Integer
        Get
            Return (AAS_Status)
        End Get
        Set(ByVal Value As Integer)
            AAS_Status = Value
        End Set
    End Property
    Public Property iAAS_AttachID() As Integer
        Get
            Return (AAS_AttachID)
        End Get
        Set(ByVal Value As Integer)
            AAS_AttachID = Value
        End Set
    End Property
    Public Property iAAS_CrBy() As Integer
        Get
            Return (AAS_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AAS_CrBy = Value
        End Set
    End Property
    Public Property iAAS_UpdatedBy() As Integer
        Get
            Return (AAS_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            AAS_UpdatedBy = Value
        End Set
    End Property
    Public Property sAAS_IPAddress() As String
        Get
            Return (AAS_IPAddress)
        End Get
        Set(ByVal Value As String)
            AAS_IPAddress = Value
        End Set
    End Property
    Public Property iAAS_CompID() As Integer
        Get
            Return (AAS_CompID)
        End Get
        Set(ByVal Value As Integer)
            AAS_CompID = Value
        End Set
    End Property
    Public Property iAAS_IsComplianceAsg() As Integer
        Get
            Return (AAS_IsComplianceAsg)
        End Get
        Set(ByVal Value As Integer)
            AAS_IsComplianceAsg = Value
        End Set
    End Property
End Structure

Public Structure strAuditAssignment_SubTask
    Private AAST_ID As Integer
    Private AAST_AAS_ID As Integer
    Private AAST_SubTaskID As Integer
    Private AAST_EmployeeID As Integer
    Private AAST_AssistedByEmployeesID As String
    Private AAST_Desc As String
    Private AAST_FrequencyID As Integer
    Private AAST_YearOrMonthID As Integer
    Private AAST_DueDate As DateTime
    Private AAST_ExpectedCompletionDate As DateTime
    Private AAST_WorkStatusID As Integer
    Private AAST_Closed As Integer
    Private AAST_AttachID As Integer
    Private AAST_CrBy As Integer
    Private AAST_IPAddress As String
    Private AAST_CompID As Integer
    Public Property iAAST_ID() As Integer
        Get
            Return (AAST_ID)
        End Get
        Set(ByVal Value As Integer)
            AAST_ID = Value
        End Set
    End Property
    Public Property iAAST_AAS_ID() As Integer
        Get
            Return (AAST_AAS_ID)
        End Get
        Set(ByVal Value As Integer)
            AAST_AAS_ID = Value
        End Set
    End Property
    Public Property iAAST_SubTaskID() As Integer
        Get
            Return (AAST_SubTaskID)
        End Get
        Set(ByVal Value As Integer)
            AAST_SubTaskID = Value
        End Set
    End Property
    Public Property iAAST_EmployeeID() As Integer
        Get
            Return (AAST_EmployeeID)
        End Get
        Set(ByVal Value As Integer)
            AAST_EmployeeID = Value
        End Set
    End Property
    Public Property sAAST_AssistedByEmployeesID() As String
        Get
            Return (AAST_AssistedByEmployeesID)
        End Get
        Set(ByVal Value As String)
            AAST_AssistedByEmployeesID = Value
        End Set
    End Property
    Public Property sAAST_Desc() As String
        Get
            Return (AAST_Desc)
        End Get
        Set(ByVal Value As String)
            AAST_Desc = Value
        End Set
    End Property
    Public Property iAAST_FrequencyID() As Integer
        Get
            Return (AAST_FrequencyID)
        End Get
        Set(ByVal Value As Integer)
            AAST_FrequencyID = Value
        End Set
    End Property
    Public Property iAAST_YearOrMonthID() As Integer
        Get
            Return (AAST_YearOrMonthID)
        End Get
        Set(ByVal Value As Integer)
            AAST_YearOrMonthID = Value
        End Set
    End Property
    Public Property dAAST_DueDate() As DateTime
        Get
            Return (AAST_DueDate)
        End Get
        Set(ByVal Value As DateTime)
            AAST_DueDate = Value
        End Set
    End Property
    Public Property dAAST_ExpectedCompletionDate() As DateTime
        Get
            Return (AAST_ExpectedCompletionDate)
        End Get
        Set(ByVal Value As DateTime)
            AAST_ExpectedCompletionDate = Value
        End Set
    End Property
    Public Property iAAST_WorkStatusID() As Integer
        Get
            Return (AAST_WorkStatusID)
        End Get
        Set(ByVal Value As Integer)
            AAST_WorkStatusID = Value
        End Set
    End Property
    Public Property iAAST_Closed() As Integer
        Get
            Return (AAST_Closed)
        End Get
        Set(ByVal Value As Integer)
            AAST_Closed = Value
        End Set
    End Property
    Public Property iAAST_AttachID() As Integer
        Get
            Return (AAST_AttachID)
        End Get
        Set(ByVal Value As Integer)
            AAST_AttachID = Value
        End Set
    End Property
    Public Property iAAST_CrBy() As Integer
        Get
            Return (AAST_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AAST_CrBy = Value
        End Set
    End Property
    Public Property sAAST_IPAddress() As String
        Get
            Return (AAST_IPAddress)
        End Get
        Set(ByVal Value As String)
            AAST_IPAddress = Value
        End Set
    End Property
    Public Property iAAST_CompID() As Integer
        Get
            Return (AAST_CompID)
        End Get
        Set(ByVal Value As Integer)
            AAST_CompID = Value
        End Set
    End Property
End Structure

Public Structure strAuditAssignment_EmpSubTask
    Private AAEST_ID As Integer
    Private AAEST_AAS_ID As Integer
    Private AAEST_AAST_ID As Integer
    Private AAEST_WorkStatusID As Integer
    Private AAST_Closed As Integer
    Private AAST_Review As Integer
    Private AAEST_Comments As String
    Private AAEST_AttachID As Integer
    Private AAEST_CrBy As Integer
    Private AAEST_CrOn As DateTime
    Private AAEST_IPAddress As String
    Private AAEST_CompID As Integer
    Public Property iAAEST_ID() As Integer
        Get
            Return (AAEST_ID)
        End Get
        Set(ByVal Value As Integer)
            AAEST_ID = Value
        End Set
    End Property
    Public Property iAAEST_AAS_ID() As Integer
        Get
            Return (AAEST_AAS_ID)
        End Get
        Set(ByVal Value As Integer)
            AAEST_AAS_ID = Value
        End Set
    End Property
    Public Property iAAEST_AAST_ID() As Integer
        Get
            Return (AAEST_AAST_ID)
        End Get
        Set(ByVal Value As Integer)
            AAEST_AAST_ID = Value
        End Set
    End Property
    Public Property iAAEST_WorkStatusID() As Integer
        Get
            Return (AAEST_WorkStatusID)
        End Get
        Set(ByVal Value As Integer)
            AAEST_WorkStatusID = Value
        End Set
    End Property
    Public Property iAAST_Closed() As Integer
        Get
            Return (AAST_Closed)
        End Get
        Set(ByVal Value As Integer)
            AAST_Closed = Value
        End Set
    End Property
    Public Property iAAST_Review() As Integer
        Get
            Return (AAST_Review)
        End Get
        Set(ByVal Value As Integer)
            AAST_Review = Value
        End Set
    End Property
    Public Property sAAEST_Comments() As String
        Get
            Return (AAEST_Comments)
        End Get
        Set(ByVal Value As String)
            AAEST_Comments = Value
        End Set
    End Property
    Public Property iAAEST_AttachID() As Integer
        Get
            Return (AAEST_AttachID)
        End Get
        Set(ByVal Value As Integer)
            AAEST_AttachID = Value
        End Set
    End Property
    Public Property iAAEST_CrBy() As Integer
        Get
            Return (AAEST_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AAEST_CrBy = Value
        End Set
    End Property
    Public Property dAAEST_CrOn() As DateTime
        Get
            Return (AAEST_CrOn)
        End Get
        Set(ByVal Value As DateTime)
            AAEST_CrOn = Value
        End Set
    End Property
    Public Property sAAEST_IPAddress() As String
        Get
            Return (AAEST_IPAddress)
        End Get
        Set(ByVal Value As String)
            AAEST_IPAddress = Value
        End Set
    End Property
    Public Property iAAEST_CompID() As Integer
        Get
            Return (AAEST_CompID)
        End Get
        Set(ByVal Value As Integer)
            AAEST_CompID = Value
        End Set
    End Property
End Structure

Public Structure strAuditAssignment_Invoice
    Private AAI_ID As Integer
    Private AAI_YearID As Integer
    Private AAI_Cust_ID As Integer
    Private AAI_BillingEntity_ID As Integer
    Private AAI_InvoiceNo As String
    Private AAI_InvoiceTypeID As Integer
    Private AAI_TaxType1 As Integer
    Private AAI_TaxType1Percentage As Decimal
    Private AAI_TaxType2 As Integer
    Private AAI_TaxType2Percentage As Decimal
    Private AAI_Notes As String
    Private AAI_AuthorizedSignatory As Integer
    Private AAI_CrBy As Integer
    Private AAI_IPAddress As String
    Private AAI_CompID As Integer
    Public Property iAAI_ID() As Integer
        Get
            Return (AAI_ID)
        End Get
        Set(ByVal Value As Integer)
            AAI_ID = Value
        End Set
    End Property
    Public Property iAAI_YearID() As Integer
        Get
            Return (AAI_YearID)
        End Get
        Set(ByVal Value As Integer)
            AAI_YearID = Value
        End Set
    End Property
    Public Property iAAI_Cust_ID() As Integer
        Get
            Return (AAI_Cust_ID)
        End Get
        Set(ByVal Value As Integer)
            AAI_Cust_ID = Value
        End Set
    End Property
    Public Property iAAI_BillingEntity_ID() As Integer
        Get
            Return (AAI_BillingEntity_ID)
        End Get
        Set(ByVal Value As Integer)
            AAI_BillingEntity_ID = Value
        End Set
    End Property
    Public Property sAAI_InvoiceNo() As String
        Get
            Return (AAI_InvoiceNo)
        End Get
        Set(ByVal Value As String)
            AAI_InvoiceNo = Value
        End Set
    End Property
    Public Property iAAI_InvoiceTypeID() As Integer
        Get
            Return (AAI_InvoiceTypeID)
        End Get
        Set(ByVal Value As Integer)
            AAI_InvoiceTypeID = Value
        End Set
    End Property
    Public Property iAAI_TaxType1() As Integer
        Get
            Return (AAI_TaxType1)
        End Get
        Set(ByVal Value As Integer)
            AAI_TaxType1 = Value
        End Set
    End Property
    Public Property dAAI_TaxType1Percentage() As Decimal
        Get
            Return (AAI_TaxType1Percentage)
        End Get
        Set(ByVal Value As Decimal)
            AAI_TaxType1Percentage = Value
        End Set
    End Property
    Public Property iAAI_TaxType2() As Integer
        Get
            Return (AAI_TaxType2)
        End Get
        Set(ByVal Value As Integer)
            AAI_TaxType2 = Value
        End Set
    End Property
    Public Property dAAI_TaxType2Percentage() As Decimal
        Get
            Return (AAI_TaxType2Percentage)
        End Get
        Set(ByVal Value As Decimal)
            AAI_TaxType2Percentage = Value
        End Set
    End Property
    Public Property sAAI_Notes() As String
        Get
            Return (AAI_Notes)
        End Get
        Set(ByVal Value As String)
            AAI_Notes = Value
        End Set
    End Property
    Public Property iAAI_AuthorizedSignatory() As Integer
        Get
            Return (AAI_AuthorizedSignatory)
        End Get
        Set(ByVal Value As Integer)
            AAI_AuthorizedSignatory = Value
        End Set
    End Property
    Public Property iAAI_CrBy() As Integer
        Get
            Return (AAI_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AAI_CrBy = Value
        End Set
    End Property
    Public Property sAAI_IPAddress() As String
        Get
            Return (AAI_IPAddress)
        End Get
        Set(ByVal Value As String)
            AAI_IPAddress = Value
        End Set
    End Property
    Public Property iAAI_CompID() As Integer
        Get
            Return (AAI_CompID)
        End Get
        Set(ByVal Value As Integer)
            AAI_CompID = Value
        End Set
    End Property
End Structure

Public Structure strAuditAssignment_InvoiceDetails
    Private AAID_ID As Integer
    Private AAID_AAI_ID As Integer
    Private AAID_AAS_ID As Integer
    Private AAID_IsTaxable As Integer
    Private AAID_Desc As String
    Private AAID_HSNSAC As Integer
    Private AAID_Quantity As Integer
    Private AAID_PricePerUnit As Decimal
    Private AAID_CrBy As Integer
    Private AAID_IPAddress As String
    Private AAID_CompID As Integer
    Public Property iAAID_ID() As Integer
        Get
            Return (AAID_ID)
        End Get
        Set(ByVal Value As Integer)
            AAID_ID = Value
        End Set
    End Property
    Public Property iAAID_AAI_ID() As Integer
        Get
            Return (AAID_AAI_ID)
        End Get
        Set(ByVal Value As Integer)
            AAID_AAI_ID = Value
        End Set
    End Property
    Public Property iAAID_AAS_ID() As Integer
        Get
            Return (AAID_AAS_ID)
        End Get
        Set(ByVal Value As Integer)
            AAID_AAS_ID = Value
        End Set
    End Property
    Public Property iAAID_IsTaxable() As Integer
        Get
            Return (AAID_IsTaxable)
        End Get
        Set(ByVal Value As Integer)
            AAID_IsTaxable = Value
        End Set
    End Property
    Public Property sAAID_Desc() As String
        Get
            Return (AAID_Desc)
        End Get
        Set(ByVal Value As String)
            AAID_Desc = Value
        End Set
    End Property
    Public Property iAAID_HSNSAC() As Integer
        Get
            Return (AAID_HSNSAC)
        End Get
        Set(ByVal Value As Integer)
            AAID_HSNSAC = Value
        End Set
    End Property
    Public Property iAAID_Quantity() As Integer
        Get
            Return (AAID_Quantity)
        End Get
        Set(ByVal Value As Integer)
            AAID_Quantity = Value
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
    Public Property iAAID_CrBy() As Integer
        Get
            Return (AAID_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AAID_CrBy = Value
        End Set
    End Property
    Public Property sAAID_IPAddress() As String
        Get
            Return (AAID_IPAddress)
        End Get
        Set(ByVal Value As String)
            AAID_IPAddress = Value
        End Set
    End Property
    Public Property iAAID_CompID() As Integer
        Get
            Return (AAID_CompID)
        End Get
        Set(ByVal Value As Integer)
            AAID_CompID = Value
        End Set
    End Property
End Structure

Public Class clsAuditAssignment
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function SaveScheduleAssignmentsDetails(ByVal sAC As String, ByVal objAAS As strAuditAssignment_Schedule, ByVal sYearName As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("AAS_AssignmentNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsGeneralFunctions.GetAllModuleJobCode(sAC, objAAS.iAAS_CompID, "ASG", objAAS.iAAS_YearID, sYearName, objAAS.iAAS_CustID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_PartnerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_PartnerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_MonthID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_MonthID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_TaskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_TaskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_Status", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_AdvancePartialBilling", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_AdvancePartialBilling
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_BillingType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_BillingType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_AssessmentYearID", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objAAS.sAAS_AssessmentYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("AAS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAAS.sAAS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAS_IsComplianceAsg", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAS_IsComplianceAsg
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAuditAssignment_Schedule", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAuditAssignmentEmpSubTask(ByVal sAC As String, ByVal objAAS As strAuditAssignment_SubTask)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_AAS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_AAS_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_SubTaskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_SubTaskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_EmployeeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_EmployeeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_AssistedByEmployeesID", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAAS.sAAST_AssistedByEmployeesID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAAS.sAAST_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_FrequencyID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_FrequencyID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_YearOrMonthID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_YearOrMonthID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_DueDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAAS.dAAST_DueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_ExpectedCompletionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAAS.dAAST_ExpectedCompletionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_WorkStatusID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_WorkStatusID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAAS.sAAST_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAS.iAAST_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAuditAssignment_SubTask", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmployeeSubTaskDetails(ByVal sAC As String, ByVal objAAEST As strAuditAssignment_EmpSubTask)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAEST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_AAS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAEST_AAS_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_AAST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAEST_AAST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_WorkStatusID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAEST_WorkStatusID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_Closed", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAST_Closed
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAST_Review", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAST_Review
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_Comments", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAAEST.sAAEST_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAEST_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAEST_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_CrOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAAEST.dAAEST_CrOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("AAEST_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAAEST.sAAEST_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAEST_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAEST.iAAEST_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAuditAssignment_EmpSubTask", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckLoginUserIsPartner(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from sad_userdetails where usr_compID=" & iACID & " And USR_Partner=1 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') And Usr_ID=" & iUsrID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAddYearTo2DigitFinancialYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iNo As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iDefaultYearID As Integer
        Try
            sSql = "Select YMS_YearID FROM Year_Master where YMS_default=1 And YMS_CompID=" & iACID & " And YMS_Delflag='A'"
            iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql1 = "Select YMS_ID,YMS_YearID FROM Year_Master where YMS_YearID<=" & iDefaultYearID & "+ " & iNo & " And YMS_YearID>8 And YMS_CompID=" & iACID & " And YMS_Delflag='A' ORDER BY YMS_YearID DESC"
            Return objDBL.SQLExecuteDataTable(sAC, sSql1)
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    Public Function GetPreviousFinancialYears(ByVal sAC As String, ByVal iACID As Integer, ByVal iNo As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iDefaultYearID As Integer
        Try
            sSql = "Select YMS_YearID FROM Year_Master where YMS_default=1 And YMS_CompID=" & iACID & " And YMS_Delflag='A'"
            iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql1 = "Select YMS_ID As Name,YMS_YearID As ID FROM Year_Master where YMS_YearID<=" & iDefaultYearID & "+" & iNo & " And YMS_YearID>8 And YMS_CompID=" & iACID & " And YMS_Delflag='A' ORDER BY YMS_YearID Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql1)
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    Public Function GetUpcomingFinancialYears(ByVal sAC As String, ByVal iACID As Integer, ByVal iNo As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iDefaultYearID As Integer
        Try
            sSql = "Select YMS_YearID FROM Year_Master where YMS_default=1 And YMS_CompID=" & iACID & " And YMS_Delflag='A'"
            iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql1 = "Select YMS_ID As Name,YMS_YearID As ID FROM Year_Master where YMS_YearID>=" & iDefaultYearID & "+" & iNo & " And YMS_YearID>8 And YMS_CompID=" & iACID & " And YMS_Delflag='A' ORDER BY YMS_YearID ASC"
            Return objDBL.SQLExecuteDataTable(sAC, sSql1)
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    Public Function LoadScheduledAssignment(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer, ByVal iUserID As Integer, ByVal isFromSchedule As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AAS_ID,AAS_AssignmentNo From AuditAssignment_Schedule where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & " "
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iUserID > 0 Then
                sSql = sSql & " And AAS_ID in(Select AAST_AAS_ID From AuditAssignment_SubTask Where (AAST_EmployeeID=" & iUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iUserID & ",%')) )"
            End If
            If isFromSchedule > 0 Then
                sSql = sSql & " And AAS_IsComplianceAsg=0"
            End If
            sSql = sSql & "  order by AAS_ID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerScheduledAssignment(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AAS_ID,AAS_AssignmentNo From AuditAssignment_Schedule where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & " And AAS_CustID=" & iCustID & " order by AAS_ID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetScheduledAssignmentDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AAS_CustID,AAS_PartnerID,AAS_YearID,AAS_TaskID,AAS_AdvancePartialBilling,AAS_FolderPath,AAS_AssessmentYearID,AAS_AttachID,AAS_Status From AuditAssignment_Schedule where AAS_ID=" & iScheduledAsgId & " And AAS_CompID=" & iAcID & " order by AAS_ID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetScheduledAsgAssistedByEmpDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgId As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Top 1 AAST_AssistedByEmployeesID From AuditAssignment_SubTask where AAST_AAS_ID=" & iScheduledAsgId & " And AAST_CompID=" & iAcID & " order by AAST_ID"
            If IsDBNull(objDBL.SQLExecuteScalar(sAc, sSql)) = False Then
                Return objDBL.SQLExecuteScalar(sAc, sSql)
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDashboardScheduledAssignment(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                ByVal iTaskID As Integer, ByVal iEmpID As Integer, ByVal sWorkStatusID As String, ByVal sComplianceID As String, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "SELECT AAS_ID As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_ID Desc) As SrNo,AAS_AssignmentNo As AssignmentNo,AAS_CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
            sSql = sSql & " Case When AAST_FrequencyID=1 then 'Yearly' When AAST_FrequencyID=3 then 'Once' When AAST_FrequencyID=2 then ("
            sSql = sSql & " Case When AAST_YearOrMonthID=1 then 'January' When AAST_YearOrMonthID=2 then 'February' when AAST_YearOrMonthID=3 then 'March' when AAST_YearOrMonthID=4 then 'April'"
            sSql = sSql & " When AAST_YearOrMonthID=5 then 'May' When AAST_YearOrMonthID=6 then 'June' when AAST_YearOrMonthID=7 then 'July' when AAST_YearOrMonthID=8 then 'August'"
            sSql = sSql & " When AAST_YearOrMonthID=9 then 'September' When AAST_YearOrMonthID=10 then 'October' when AAST_YearOrMonthID=11 then 'November' when AAST_YearOrMonthID=12 then 'December' End) End as 'Month',"
            sSql = sSql & " AAS_PartnerID,USr_FullName As 'Partner',AAS_YearID,YMS_ID As FinancialYear,AAS_TaskID,CMM_Desc As Task,"
            sSql = sSql & " SubTask=STUFF ((SELECT DISTINCT '; '+ CAST(AM_Name AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,AuditAssignmentSubTask_Master  WHERE AAST_AAS_ID=AAS_ID And AM_ID=AAST_SubTaskID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Employee=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID=AAS_ID And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Convert(Varchar(10),AAS_CrOn,103) As CreatedDate,"
            sSql = sSql & " DueDate=Convert(Varchar(10),(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " ExpectedCompletionDate=Convert(Varchar(10),(SELECT Max(AAST_ExpectedCompletionDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " TimeTaken=(Select CASE WHEN ((SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1) THEN"
            sSql = sSql & " (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),(SELECT Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)))"
            'sSql = sSql & " ELSE (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) END)+1,"
            sSql = sSql & " ELSE (( CASE WHEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) >= 0 THEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) ELSE -1 END)) END)+1,"
            sSql = sSql & " WorkStatus=STUFF ((SELECT DISTINCT '; '+ CAST(CMM_Desc AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,Content_Management_Master WHERE AAST_AAS_ID=AAS_ID And CMM_ID=AAST_WorkStatusID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Closed=(SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),"
            sSql = sSql & " Comments=(SELECT AAEST_Comments From AuditAssignment_EmpSubTask where AAEST_ID=(Select Max(AAEST_ID) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)),"
            sSql = sSql & " Case When AAS_BillingType=0 then '' When AAS_BillingType=1 then 'Billable' when AAS_BillingType=2 then 'Proforma' when AAS_BillingType=3 then 'Billed' End As BillingStatus"
            sSql = sSql & " FROM AuditAssignment_Schedule"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
            sSql = sSql & " Join Year_Master on YMS_YearID=AAS_YearID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            Else
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            End If
            sSql = sSql & " GROUP BY AAS_ID,AAS_AssignmentNo,AAS_CustID,Cust_Name,AAST_FrequencyID,AAST_YearOrMonthID,AAS_PartnerID,USr_FullName,AAS_YearID,YMS_ID,AAS_TaskID,CMM_Desc,AAS_CrOn,AAS_BillingType"
            sSql = sSql & " Order by AAS_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDashboardScheduledAssignmentFromId(ByVal sAc As String, ByVal iAcID As Integer, ByVal sAsgIDs As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "SELECT AAS_ID As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_ID Desc) As SrNo,AAS_AssignmentNo As AssignmentNo,AAS_CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
            sSql = sSql & " Case When AAST_FrequencyID=1 then 'Yearly' When AAST_FrequencyID=3 then 'Once' When AAST_FrequencyID=2 then ("
            sSql = sSql & " Case When AAST_YearOrMonthID=1 then 'January' When AAST_YearOrMonthID=2 then 'February' when AAST_YearOrMonthID=3 then 'March' when AAST_YearOrMonthID=4 then 'April'"
            sSql = sSql & " When AAST_YearOrMonthID=5 then 'May' When AAST_YearOrMonthID=6 then 'June' when AAST_YearOrMonthID=7 then 'July' when AAST_YearOrMonthID=8 then 'August'"
            sSql = sSql & " When AAST_YearOrMonthID=9 then 'September' When AAST_YearOrMonthID=10 then 'October' when AAST_YearOrMonthID=11 then 'November' when AAST_YearOrMonthID=12 then 'December' End) End as 'Month',"
            sSql = sSql & " AAS_PartnerID,USr_FullName As 'Partner',AAS_YearID,YMS_ID As FinancialYear,AAS_TaskID,CMM_Desc As Task,"
            sSql = sSql & " SubTask=STUFF ((SELECT DISTINCT '; '+ CAST(AM_Name AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,AuditAssignmentSubTask_Master  WHERE AAST_AAS_ID=AAS_ID And AM_ID=AAST_SubTaskID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Employee=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID=AAS_ID And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Convert(Varchar(10),AAS_CrOn,103) As CreatedDate,"
            sSql = sSql & " DueDate=Convert(Varchar(10),(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " ExpectedCompletionDate=Convert(Varchar(10),(SELECT Max(AAST_ExpectedCompletionDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " TimeTaken=(Select CASE WHEN ((SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1) THEN"
            sSql = sSql & " (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),(SELECT Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)))"
            'sSql = sSql & " ELSE (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) END)+1,"
            sSql = sSql & " ELSE (( CASE WHEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) >= 0 THEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) ELSE -1 END)) END)+1,"
            sSql = sSql & " WorkStatus=STUFF ((SELECT DISTINCT '; '+ CAST(CMM_Desc AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,Content_Management_Master WHERE AAST_AAS_ID=AAS_ID And CMM_ID=AAST_WorkStatusID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Closed=(SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),"
            sSql = sSql & " Comments=(SELECT AAEST_Comments From AuditAssignment_EmpSubTask where AAEST_ID=(Select Max(AAEST_ID) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)),"
            sSql = sSql & " Case When AAS_BillingType=0 then '' When AAS_BillingType=1 then 'Billable' when AAS_BillingType=2 then 'Proforma' when AAS_BillingType=3 then 'Billed' End As BillingStatus"
            sSql = sSql & " FROM AuditAssignment_Schedule"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
            sSql = sSql & " Join Year_Master on YMS_YearID=AAS_YearID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If sAsgIDs <> "" Then
                sSql = sSql & "  And AAS_ID in (" & sAsgIDs & ")"
            End If
            sSql = sSql & " GROUP BY AAS_ID,AAS_AssignmentNo,AAS_CustID,Cust_Name,AAST_FrequencyID,AAST_YearOrMonthID,AAS_PartnerID,USr_FullName,AAS_YearID,YMS_ID,AAS_TaskID,CMM_Desc,AAS_CrOn,AAS_BillingType"
            sSql = sSql & " Order by AAS_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerAllScheduledAssignmentDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sFinancialYearID As String, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                ByVal iTaskID As Integer, ByVal iEmpID As Integer, ByVal sWorkStatusID As String, ByVal sComplianceID As String, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "SELECT AAS_ID As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_ID Desc) As SrNo,AAS_AssignmentNo As AssignmentNo,AAS_CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
            sSql = sSql & " Case When AAST_FrequencyID=1 then 'Yearly' When AAST_FrequencyID=3 then 'Once' When AAST_FrequencyID=2 then ("
            sSql = sSql & " Case When AAST_YearOrMonthID=1 then 'January' When AAST_YearOrMonthID=2 then 'February' when AAST_YearOrMonthID=3 then 'March' when AAST_YearOrMonthID=4 then 'April'"
            sSql = sSql & " When AAST_YearOrMonthID=5 then 'May' When AAST_YearOrMonthID=6 then 'June' when AAST_YearOrMonthID=7 then 'July' when AAST_YearOrMonthID=8 then 'August'"
            sSql = sSql & " When AAST_YearOrMonthID=9 then 'September' When AAST_YearOrMonthID=10 then 'October' when AAST_YearOrMonthID=11 then 'November' when AAST_YearOrMonthID=12 then 'December' End) End as 'Month',"
            sSql = sSql & " AAS_PartnerID,USr_FullName As 'Partner',AAS_YearID,YMS_ID As FinancialYear,AAS_TaskID,CMM_Desc As Task,"
            sSql = sSql & " SubTask=STUFF ((SELECT DISTINCT '; '+ CAST(AM_Name AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,AuditAssignmentSubTask_Master  WHERE AAST_AAS_ID=AAS_ID And AM_ID=AAST_SubTaskID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Employee=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID=AAS_ID And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Convert(Varchar(10),AAS_CrOn,103) As CreatedDate,"
            sSql = sSql & " DueDate=Convert(Varchar(10),(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " ExpectedCompletionDate=Convert(Varchar(10),(SELECT Max(AAST_ExpectedCompletionDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " TimeTaken=(Select CASE WHEN ((SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1) THEN"
            sSql = sSql & " (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),(SELECT Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)))"
            'sSql = sSql & " ELSE (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) END)+1,"
            sSql = sSql & " ELSE (( CASE WHEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) >= 0 THEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) ELSE -1 END)) END)+1,"
            sSql = sSql & " WorkStatus=STUFF ((SELECT DISTINCT '; '+ CAST(CMM_Desc AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,Content_Management_Master WHERE AAST_AAS_ID=AAS_ID And CMM_ID=AAST_WorkStatusID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Closed=(SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),"
            sSql = sSql & " Comments=(SELECT AAEST_Comments From AuditAssignment_EmpSubTask where AAEST_ID=(Select Max(AAEST_ID) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)),"
            sSql = sSql & " Case When AAS_BillingType=0 then '' When AAS_BillingType=1 then 'Billable' when AAS_BillingType=2 then 'Proforma' when AAS_BillingType=3 then 'Billed' End As BillingStatus"
            sSql = sSql & " FROM AuditAssignment_Schedule"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
            sSql = sSql & " Join Year_Master on YMS_YearID=AAS_YearID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If sFinancialYearID <> "" Then
                sSql = sSql & " And AAS_YearID in (" & sFinancialYearID & ")"
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            Else
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            End If
            sSql = sSql & " GROUP BY AAS_ID,AAS_AssignmentNo,AAS_CustID,Cust_Name,AAST_FrequencyID,AAST_YearOrMonthID,AAS_PartnerID,USr_FullName,AAS_YearID,YMS_ID,AAS_TaskID,CMM_Desc,AAS_CrOn,AAS_BillingType"
            sSql = sSql & " Order by AAS_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDashboardScheduledAssignmentCounts(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                           ByVal iTaskID As Integer, ByVal iEmpID As Integer, ByVal sWorkStatusID As String, ByVal sComplianceID As String, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            sSql = "Select a.OpenTasks,a1.OpenTaskIds,b.ClosedTasks,b1.ClosedTaskIds,c.OverDueTasks,c1.OverDueTaskIds,d.MyOpenTasks,d1.MyOpenTaskIds,"
            sSql = sSql & " e.MyOverDueTasks,e1.MyOverDueTaskIds,f.TodayTasks,f1.TodayTaskIds From (Select Count(Distinct(AAS_ID)) As OpenTasks From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            'If iCustomerID > 0 Then
            '    sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            'End If
            'If iPartnerID > 0 Then
            '    sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            'End If
            'If iTaskID > 0 Then
            '    sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            'End If
            sSql = sSql & " )a,"
            sSql = sSql & " (Select STUFF((SELECT DISTINCT ', ' + CAST(AAS_ID AS VARCHAR(MAX)) From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            'If iCustomerID > 0 Then
            '    sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            'End If
            'If iPartnerID > 0 Then
            '    sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            'End If
            'If iTaskID > 0 Then
            '    sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            'End If
            sSql = sSql & " FOR XML PATH('')), 1, 2, '')As OpenTaskIds)a1,"

            sSql = sSql & "(Select Count(Distinct(AAS_ID)) As ClosedTasks From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And ((Select Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1)"
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            'If iCustomerID > 0 Then
            '    sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            'End If
            'If iPartnerID > 0 Then
            '    sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            'End If
            'If iTaskID > 0 Then
            '    sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            'End If
            sSql = sSql & " )b,"
            sSql = sSql & "(Select STUFF((SELECT DISTINCT ', ' + CAST(AAS_ID AS VARCHAR(MAX)) From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And ((Select Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1)"
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            'If iCustomerID > 0 Then
            '    sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            'End If
            'If iPartnerID > 0 Then
            '    sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            'End If
            'If iTaskID > 0 Then
            '    sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            'End If
            sSql = sSql & " FOR XML PATH('')), 1, 2, '') As ClosedTaskIds)b1,"

            sSql = sSql & "(Select Count(Distinct(AAS_ID)) As OverDueTasks From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            'If iCustomerID > 0 Then
            '    sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            'End If
            'If iPartnerID > 0 Then
            '    sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            'End If
            'If iTaskID > 0 Then
            '    sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            'End If
            sSql = sSql & " )c,"
            sSql = sSql & "(Select STUFF((SELECT DISTINCT ', ' + CAST(AAS_ID AS VARCHAR(MAX)) From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            'If iCustomerID > 0 Then
            '    sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            'End If
            'If iPartnerID > 0 Then
            '    sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            'End If
            'If iTaskID > 0 Then
            '    sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            'End If
            sSql = sSql & " FOR XML PATH('')), 1, 2, '') As OverDueTaskIds)c1,"

            sSql = sSql & "(Select Count(Distinct(AAS_ID)) As MyOpenTasks From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
            If bLoginUserIsPartner = False Then
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
            Else
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
            End If
            sSql = sSql & " ))d,"
            sSql = sSql & "(Select STUFF((SELECT DISTINCT ', ' + CAST(AAS_ID AS VARCHAR(MAX)) From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
            If bLoginUserIsPartner = False Then
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
            Else
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
            End If
            sSql = sSql & " )FOR XML PATH('')), 1, 2, '') As MyOpenTaskIds)d1,"

            sSql = sSql & " (Select Count(Distinct(AAS_ID)) As MyOverDueTasks From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
            If bLoginUserIsPartner = False Then
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
            Else
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
            End If
            sSql = sSql & " ))e,"
            sSql = sSql & " (Select STUFF((SELECT DISTINCT ', ' + CAST(AAS_ID AS VARCHAR(MAX)) From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
            If bLoginUserIsPartner = False Then
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
            Else
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
            End If
            sSql = sSql & " )FOR XML PATH('')), 1, 2, '') As MyOverDueTaskIds)e1,"

            sSql = sSql & " (Select Count(Distinct(AAS_ID)) As TodayTasks From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
            If bLoginUserIsPartner = False Then
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
            Else
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
            End If
            sSql = sSql & " ))f,"
            sSql = sSql & " (Select STUFF((SELECT DISTINCT ', ' + CAST(AAS_ID AS VARCHAR(MAX)) From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
            If bLoginUserIsPartner = False Then
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
            Else
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
            End If
            sSql = sSql & " )FOR XML PATH('')), 1, 2, '') As TodayTaskIds)f1"

            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllScheduledAssignment(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                ByVal iTaskID As Integer, ByVal iEmpID As Integer, ByVal sWorkStatusID As String, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim dt As New DataTable, dtAsg As New DataTable, dtEmp As New DataTable
        Dim dr As DataRow
        Dim i As Integer, j As Integer, k As Integer
        Dim dDate As Date, dDueDate As Date, dCompletedDate As Date
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AssignmentID")
            dt.Columns.Add("AssignmentNo")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("Partner")
            dt.Columns.Add("FinancialYear")
            dt.Columns.Add("Task")
            dt.Columns.Add("SubTask")
            dt.Columns.Add("Employee")
            dt.Columns.Add("CreatedDate")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("ExpectedCompletionDate")
            dt.Columns.Add("TimeTaken")
            dt.Columns.Add("WorkStatus")
            dt.Columns.Add("Comments")
            dt.Columns.Add("BillingStatus")

            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "Select Distinct(AAS_ID),AAS_AssignmentNo,AAS_CrOn,Cust_Name,USr_FullName,YMS_ID,CMM_Desc, "
            sSql = sSql & " Case When AAS_BillingType=0 then '' When AAS_BillingType=1 then 'Billable' when AAS_BillingType=2 then 'Proforma' when AAS_BillingType=3 then 'Billed' End As BillingStatus "
            sSql = sSql & " From AuditAssignment_Schedule "
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID "
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID "
            sSql = sSql & " Join Year_Master on YMS_YearID=AAS_YearID "
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            Else
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            End If
            sSql = sSql & " Order by AAS_ID Desc"
            dtAsg = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtAsg.Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("AssignmentID") = dtAsg.Rows(i)("AAS_ID")
                dr("AssignmentNo") = dtAsg.Rows(i)("AAS_AssignmentNo")
                dr("CustomerName") = dtAsg.Rows(i)("Cust_Name")
                dr("Partner") = dtAsg.Rows(i)("USr_FullName")
                dr("FinancialYear") = dtAsg.Rows(i)("YMS_ID")
                dr("Task") = dtAsg.Rows(i)("CMM_Desc")
                dr("CreatedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtAsg.Rows(i)("AAS_CrOn"), "D")
                sSql1 = "Select AM_Name,USr_FullName,AAST_DueDate,AAST_ExpectedCompletionDate,AAEST_CrOn,CMM_Desc,AAEST_Comments,AAST_Closed"
                sSql1 = sSql1 & " From AuditAssignment_EmpSubTask"
                sSql1 = sSql1 & " Join AuditAssignment_SubTask on AAST_ID=AAEST_AAST_ID"
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql1 = sSql1 & " Join AuditAssignmentSubTask_Master on AM_ID=AAST_SubTaskID"
                sSql1 = sSql1 & " Join sad_userdetails on Usr_ID=AAST_EmployeeID "
                sSql1 = sSql1 & " Join Content_Management_Master on CMM_ID=AAEST_WorkStatusID Where AAEST_ID=(Select Max(AAEST_ID) From AuditAssignment_EmpSubTask where AAEST_AAS_ID=" & dtAsg.Rows(i)("AAS_ID") & ")"
                dtEmp = objDBL.SQLExecuteDataTable(sAc, sSql1)
                For j = 0 To dtEmp.Rows.Count - 1
                    dr("SubTask") = dtEmp.Rows(j)("AM_Name")
                    dr("Employee") = dtEmp.Rows(j)("USr_FullName")
                    dr("DueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAST_DueDate"), "D")
                    dr("ExpectedCompletionDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAST_ExpectedCompletionDate"), "D")

                    dDueDate = Date.ParseExact(objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAST_DueDate"), "D"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dCompletedDate = Date.ParseExact(objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAEST_CrOn"), "D"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    If dtEmp.Rows(j)("AAST_Closed") = 1 Then
                        k = DateDiff(DateInterval.Day, dDueDate, dCompletedDate)
                    Else
                        k = DateDiff(DateInterval.Day, dDueDate, dDate)
                    End If
                    If k = 0 Then
                        dr("TimeTaken") = 1
                    ElseIf k > 0 Then
                        dr("TimeTaken") = k
                    Else
                        dr("TimeTaken") = 0
                    End If
                    dr("WorkStatus") = dtEmp.Rows(j)("CMM_Desc")
                    dr("Comments") = dtEmp.Rows(j)("AAEST_Comments")
                    dr("BillingStatus") = dtAsg.Rows(i)("BillingStatus")
                Next
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllScheduledAssignmentForYear(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                ByVal iTaskID As Integer, ByVal iEmpID As Integer, ByVal sWorkStatusID As String, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtAsg As New DataTable, dtEmp As New DataTable
        Try

            dt.Columns.Add("AssignmentID")
            dt.Columns.Add("AssignmentNo")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("Partner")
            dt.Columns.Add("FinancialYear")
            dt.Columns.Add("Task")
            dt.Columns.Add("SubTask")
            dt.Columns.Add("Employee")
            dt.Columns.Add("CreatedDate")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("ExpectedCompletionDate")
            dt.Columns.Add("TimeTaken")
            dt.Columns.Add("WorkStatus")
            dt.Columns.Add("Comments")
            dt.Columns.Add("BillingStatus")

            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "Select Distinct(AAS_ID) as AssignmentID,AAS_AssignmentNo as AssignmentNo,Cust_Name as CustomerName,USr_FullName as Partner,YMS_ID as FinancialYear,CMM_Desc as Task,"
            sSql = sSql & " '' as SubTask,'' as Employee,Convert(Varchar(10),AAS_CrOn,103) as CreatedDate,'' as DueDate, '' as  ExpectedCompletionDate, '' as TimeTaken, '' as WorkStatus, '' as Comments, "
            sSql = sSql & " Case When AAS_BillingType=0 then '' When AAS_BillingType=1 then 'Billable' when AAS_BillingType=2 then 'Proforma' when AAS_BillingType=3 then 'Billed' End As BillingStatus "
            sSql = sSql & " From AuditAssignment_Schedule "
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID "
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID "
            sSql = sSql & " Join Year_Master on YMS_YearID=AAS_YearID "
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%'))"
                ElseIf iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=0"
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%'))"
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            Else
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            End If
            sSql = sSql & " Order by AAS_ID Desc"
            dtAsg = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dtAsg
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssignmentSubTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAssignmentID As Integer, ByVal iFinancialYearID As Integer,
                                                 ByVal iCustomerID As Integer, ByVal iPartnerID As Integer, ByVal iTaskID As Integer) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AssignmentID")
            dt.Columns.Add("CustomerName")
            dt.Columns.Add("Partner")
            dt.Columns.Add("Task")
            dt.Columns.Add("SubTaskId")
            dt.Columns.Add("SubTask")
            dt.Columns.Add("EmployeeId")
            dt.Columns.Add("Employee")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("ExpectedCompletionDate")
            dt.Columns.Add("WorkStatusId")
            dt.Columns.Add("WorkStatus")
            dt.Columns.Add("Description")
            dt.Columns.Add("Closed")
            dt.Columns.Add("Review")
            dt.Columns.Add("AssistedBy")
            dt.Columns.Add("FrequencyId")
            dt.Columns.Add("YearOrMonthID")
            dt.Columns.Add("Frequency")
            dt.Columns.Add("CreatedBy")
            dt.Columns.Add("CreatedOn")
            dt.Columns.Add("DBpkId")

            sSql = "Select AAST_ID,AAST_AAS_ID,Cust_Name,p.USr_FullName as Partner,a.CMM_Desc As Task,AAST_SubTaskID,AM_Name As SubTask,AAST_Desc,AAST_EmployeeID,e.USr_FullName As Employee,"
            sSql = sSql & " AAST_DueDate,AAST_ExpectedCompletionDate,AAST_WorkStatusID,b.CMM_Desc As WorkStatus,AAST_Closed,AAST_Review,AAST_AssistedByEmployeesID,"
            sSql = sSql & " e1.USr_FullName As CreatedBy,AAST_CrOn,AAST_FrequencyID,AAST_YearOrMonthID,"
            sSql = sSql & " Case When AAST_FrequencyID=1 then 'Yearly' When AAST_FrequencyID=3 then 'Once' when AAST_FrequencyID=4 then 'Quarterly' When AAST_FrequencyID=2 then ("
            sSql = sSql & " Case When AAST_YearOrMonthID=1 then 'Monthly(January)' When AAST_YearOrMonthID=2 then 'Monthly(February)' when AAST_YearOrMonthID=3 then 'Monthly(March)' when AAST_YearOrMonthID=4 then 'Monthly(April)'"
            sSql = sSql & " When AAST_YearOrMonthID=5 then 'Monthly(May)' When AAST_YearOrMonthID=6 then 'Monthly(June)' when AAST_YearOrMonthID=7 then 'Monthly(July)' when AAST_YearOrMonthID=8 then 'Monthly(August)'"
            sSql = sSql & " When AAST_YearOrMonthID=9 then 'Monthly(September)' When AAST_YearOrMonthID=10 then 'Monthly(October)' when AAST_YearOrMonthID=11 then 'Monthly(November)' when AAST_YearOrMonthID=12 then 'Monthly(December)' End) End As Frequency"
            sSql = sSql & " From AuditAssignment_SubTask"
            sSql = sSql & " Join AuditAssignment_Schedule on AAST_AAS_ID=AAS_ID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join sad_userdetails p on p.Usr_ID=AAS_PartnerID"
            sSql = sSql & " Join Content_Management_Master a on a.CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignmentSubTask_Master on AM_ID=AAST_SubTaskID"
            sSql = sSql & " Join sad_userdetails e on e.Usr_ID=AAST_EmployeeID"
            sSql = sSql & " Join sad_userdetails e1 on e1.Usr_ID=AAST_CrBy"
            sSql = sSql & " Join Content_Management_Master b on b.CMM_ID=AAST_WorkStatusID"
            sSql = sSql & " Where AAST_CompID=" & iAcID & ""
            If iAssignmentID > 0 Then
                sSql = sSql & " And AAS_ID=" & iAssignmentID & ""
            End If
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            sSql = sSql & " Order by AAST_DueDate,a.CMM_Desc,AM_Name Desc"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("AssignmentID") = ds.Tables(0).Rows(i)("AAST_AAS_ID")
                dr("CustomerName") = ds.Tables(0).Rows(i)("Cust_Name")
                dr("Description") = ds.Tables(0).Rows(i)("AAST_Desc")
                dr("Partner") = ds.Tables(0).Rows(i)("Partner")
                dr("Task") = ds.Tables(0).Rows(i)("Task")
                dr("SubTaskId") = ds.Tables(0).Rows(i)("AAST_SubTaskID")
                dr("SubTask") = ds.Tables(0).Rows(i)("SubTask")
                dr("EmployeeId") = ds.Tables(0).Rows(i)("AAST_EmployeeID")
                dr("Employee") = ds.Tables(0).Rows(i)("Employee")
                dr("DueDate") = objclsGRACeGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(i)("AAST_DueDate"), "D")
                dr("ExpectedCompletionDate") = objclsGRACeGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(i)("AAST_ExpectedCompletionDate"), "D")
                dr("WorkStatusId") = ds.Tables(0).Rows(i)("AAST_WorkStatusID")
                dr("WorkStatus") = ds.Tables(0).Rows(i)("WorkStatus")
                dr("Description") = ds.Tables(0).Rows(i)("AAST_Desc")
                dr("Closed") = ds.Tables(0).Rows(i)("AAST_Closed")
                dr("Review") = ds.Tables(0).Rows(i)("AAST_Review")
                dr("AssistedBy") = ds.Tables(0).Rows(i)("AAST_AssistedByEmployeesID")
                dr("FrequencyId") = ds.Tables(0).Rows(i)("AAST_FrequencyID")
                dr("YearOrMonthID") = ds.Tables(0).Rows(i)("AAST_YearOrMonthID")
                dr("Frequency") = ds.Tables(0).Rows(i)("Frequency")
                dr("CreatedBy") = ds.Tables(0).Rows(i)("CreatedBy")
                dr("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(i)("AAST_CrOn"), "D")
                dr("DBpkId") = ds.Tables(0).Rows(i)("AAST_ID")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEmpAssignmentSubTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal sAuditAsgSubTaskId As String) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DBpkId")
            dt.Columns.Add("SubTask")
            dt.Columns.Add("Employee")
            dt.Columns.Add("WorkStatus")
            dt.Columns.Add("Comments")
            dt.Columns.Add("Date")
            dt.Columns.Add("AttachCount")
            dt.Columns.Add("AttachID")

            sSql = "Select AAEST_ID,AM_Name,USr_FullName,CMM_Desc,AAEST_Comments,AAEST_CrOn,AAEST_AttachID From AuditAssignment_EmpSubTask"
            sSql = sSql & " Join AuditAssignment_SubTask  On AAST_ID=AAEST_AAST_ID"
            sSql = sSql & " Join AuditAssignmentSubTask_Master on AM_ID=AAST_SubTaskID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAEST_WorkStatusID "
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAEST_CrBy "
            sSql = sSql & " Where AAEST_AAS_ID=" & iScheduledAsgID & " And AAEST_AAST_ID in (" & sAuditAsgSubTaskId & ") And AAEST_CompID=" & iAcID & " Order by AAEST_ID Desc"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DBpkId") = ds.Tables(0).Rows(i)("AAEST_ID")
                dr("SubTask") = ds.Tables(0).Rows(i)("AM_Name")
                dr("Employee") = ds.Tables(0).Rows(i)("USr_FullName")
                dr("WorkStatus") = ds.Tables(0).Rows(i)("CMM_Desc")
                dr("Comments") = ds.Tables(0).Rows(i)("AAEST_Comments")
                dr("Date") = objclsGRACeGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(i)("AAEST_CrOn"), "D")
                dr("AttachID") = ds.Tables(0).Rows(i)("AAEST_AttachID")
                dr("AttachCount") = objDBL.SQLExecuteScalarInt(sAc, "Select Count(*) From EDT_ATTACHMENTS Where ATCH_ID=" & ds.Tables(0).Rows(i)("AAEST_AttachID") & "")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateScheduledAsgBillingTypeDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal iBillingTypeID As Integer)
        Dim sSql As String
        Try
            sSql = "Select AAS_BillingType From AuditAssignment_Schedule Where AAS_ID=" & iScheduledAsgID & " And AAS_CompID=" & iAcID & ""
            If objDBL.SQLExecuteScalarInt(sAc, sSql) < iBillingTypeID Then
                sSql = "Update AuditAssignment_Schedule set AAS_BillingType=" & iBillingTypeID & " Where AAS_ID=" & iScheduledAsgID & " And AAS_CompID=" & iAcID & ""
                objDBL.SQLExecuteNonQuery(sAc, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateAsgSubTaskClosedDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iSTPkID As Integer)
        Dim sSql As String
        Try
            sSql = "Select AAST_Closed From AuditAssignment_SubTask Where AAST_ID=" & iSTPkID & " And AAST_CompID=" & iAcID & ""
            If objDBL.SQLExecuteScalarInt(sAc, sSql) <> 0 Then
                sSql = "Update AuditAssignment_SubTask set AAST_Closed=0 where AAST_ID=" & iSTPkID & " And AAST_CompID=" & iAcID & ""
                objDBL.SQLExecuteNonQuery(sAc, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateScheduledStatusAndFolderPath(ByVal sAc As String, ByVal iAcID As Integer, sFolderPath As String, ByVal iScheduledAsgID As Integer, ByVal iAttachID As Integer)
        Dim sSql As String
        Try
            sSql = "Update AuditAssignment_Schedule set AAS_Status=(SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 2 Else 1 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=" & iScheduledAsgID & "),"
            sSql = sSql & " AAS_FolderPath='" & sFolderPath & "',AAS_AttachID=" & iAttachID & " Where AAS_ID=" & iScheduledAsgID & " And AAS_CompID=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateScheduledAsgAdvancePartialBillingDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal iAdvancePartialBilling As Integer)
        Dim sSql As String
        Try
            sSql = "Update AuditAssignment_Schedule set AAS_AdvancePartialBilling=" & iAdvancePartialBilling & " Where AAS_ID=" & iScheduledAsgID & " And AAS_CompID=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateScheduledAsgAssistedByEmployeesDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal sAssistedByEmployeesIDs As String)
        Dim sSql As String
        Try
            sSql = "Update AuditAssignment_SubTask set AAST_AssistedByEmployeesID='" & sAssistedByEmployeesIDs & "' Where AAST_AAS_ID=" & iScheduledAsgID & " And AAST_CompID=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeletedScheduledAsgEmpSubTask(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From AuditAssignment_SubTask Where AAST_ID=" & iScheduledAsgID & " And AAST_CompID=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetWIPIdFromMaster(ByVal sAc As String, ByVal iAcID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Top 1 CMM_ID From Content_Management_Master Where CMM_Category='WS' And CMM_CompID=" & iAcID & " And (CMM_Desc='WIP' or CMM_Desc='WIP(Work In Progress)' Or CMM_Desc='Work In Progress')"
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadComplianceTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer, ByVal iType As Integer,
                                              ByVal iWIPId As Integer, ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtAsg As New DataTable, dtEmp As New DataTable
        Dim dr As DataRow, iSlNo As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Customer")
            dt.Columns.Add("Partner")
            dt.Columns.Add("AssignmentNo")
            dt.Columns.Add("SubTask")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("ExpectedCompletionDate")

            sSql = "Select Cust_Name,Usr_FullName,AAS_AssignmentNo,AM_Name,AAST_DueDate,Case When AAST_Closed=0 then AAST_ExpectedCompletionDate else AAEST_CrOn End ExpectedCompletionDate From AuditAssignment_SubTask "
            sSql = sSql & " Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID"
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join AuditAssignmentSubTask_Master on AM_ID=AAST_SubTaskID"
            sSql = sSql & " Left Join AuditAssignment_EmpSubTask On AAEST_ID=(Select Max(AAEST_ID) From AuditAssignment_EmpSubTask where AAEST_AAST_ID=AAST_ID)"
            sSql = sSql & " Where AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & " And AAST_Closed=" & iType & " And AAST_CompID=" & iAcID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAST_EmployeeID=" & iPartnerID & ""
            End If
            If iWIPId > 0 Then
                sSql = sSql & " And AAST_WorkStatusID=" & iWIPId & ""
            End If
            sSql = sSql & " Order by AAS_AssignmentNo"

            dtAsg = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtAsg.Rows.Count - 1
                dr = dt.NewRow()
                iSlNo = iSlNo + 1
                dr("SrNo") = iSlNo
                dr("Customer") = dtAsg.Rows(i)("Cust_Name")
                dr("Partner") = dtAsg.Rows(i)("Usr_FullName")
                dr("AssignmentNo") = dtAsg.Rows(i)("AAS_AssignmentNo")
                dr("SubTask") = dtAsg.Rows(i)("AM_Name")
                dr("DueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtAsg.Rows(i)("AAST_DueDate"), "D")
                dr("ExpectedCompletionDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtAsg.Rows(i)("ExpectedCompletionDate"), "D")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadComplianceTaskCounts(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer, ByVal iType As Integer,
                                              ByVal iWIPId As Integer, ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select a.AllTasks,b.YetToStartTasks,c.InProgressTasks,d.CompletedTasks,e.OverDueTasks From "
            sSql = sSql & " (Select Count(Distinct(AAS_ID)) As AllTasks From AuditAssignment_Schedule Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And"
            sSql = sSql & " AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            sSql = sSql & " )a,"

            sSql = sSql & "(Select Count(Distinct(AAS_ID)) As YetToStartTasks From AuditAssignment_Schedule Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And"
            sSql = sSql & " AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ""
            sSql = sSql & " And ((Select Count(*) From AuditAssignment_EmpSubTask Where AAEST_AAS_ID=AAS_ID)=0) Where AAS_CompID=" & iAcID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            sSql = sSql & " )b,"

            sSql = sSql & "(Select Count(Distinct(AAS_ID)) As InProgressTasks From AuditAssignment_Schedule Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And"
            sSql = sSql & " AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ""
            sSql = sSql & " And ((Select Count(*) From AuditAssignment_EmpSubTask Where AAEST_AAS_ID=AAS_ID)>0) Where AAS_CompID=" & iAcID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            sSql = sSql & " )c,"

            sSql = sSql & "(Select Count(Distinct(AAS_ID)) As CompletedTasks From AuditAssignment_Schedule Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=1 And "
            sSql = sSql & " AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ""
            sSql = sSql & " And ((SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1) Where AAS_CompID=" & iAcID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            sSql = sSql & " )d,"

            sSql = sSql & "(Select Count(Distinct(AAS_ID)) As OverDueTasks From AuditAssignment_Schedule Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And "
            sSql = sSql & " AAST_ExpectedCompletionDate>=" & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            sSql = sSql & " )e"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadAssignmentSubTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAssignmentId As String) As DataTable
    '    Dim sSql As String
    '    Try
    '        sSql = "Select AM_ID As PKID,CMM_Desc + ' - ' + AM_Name As Name From AuditAssignmentSubTask_Master"
    '        sSql = sSql & " Join AuditAssignment_SubTask On AAST_SubTaskID=AM_ID"
    '        sSql = sSql & " Join Content_Management_Master On CMM_ID=AM_AuditAssignmentID"
    '        sSql = sSql & " Where AAST_AAS_ID=" & iAssignmentId & " and AAST_CompId=" & iAcID & " Order By AM_Name ASC"
    '        Return objDBL.SQLExecuteDataTable(sAc, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadCompletedScheduledAssignment(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AAS_ID,AAS_AssignmentNo From AuditAssignment_Schedule where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & " And AAS_CustID=" & iCustID & " order by AAS_ID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadClosedSTCustomerScheduledAssignment(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustID As Integer) As DataTable
    '    Dim sSql As String
    '    Try
    '        sSql = "Select AAS_ID,AAS_AssignmentNo From AuditAssignment_Schedule where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & " And AAS_CustID=" & iCustID & " And AAS_ID in"
    '        sSql = sSql & " (Select Distinct AAST_AAS_ID from AuditAssignment_SubTask where AAST_Closed=1 And AAST_AAS_ID not in(Select Distinct AAST_AAS_ID from AuditAssignment_SubTask where AAST_Closed=0)) order by AAS_ID"
    '        Return objDBL.SQLExecuteDataTable(sAc, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SaveInvoice(ByVal sAC As String, ByVal objAAI As strAuditAssignment_Invoice)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_Cust_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_Cust_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_BillingEntity_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_BillingEntity_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_InvoiceNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAAI.sAAI_InvoiceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_InvoiceTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_InvoiceTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_TaxType1", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_TaxType1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_TaxType1Percentage", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objAAI.dAAI_TaxType1Percentage
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_TaxType2", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_TaxType2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_TaxType2Percentage", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objAAI.dAAI_TaxType2Percentage
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_Notes", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objAAI.sAAI_Notes
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_AuthorizedSignatory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_AuthorizedSignatory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAAI.sAAI_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAI_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAI.iAAI_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAuditAssignment_Invoice", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInvoiceDetailsForReport(ByVal sAc As String, ByVal iAcID As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From AuditAssignment_Invoice Where AAI_ID=" & iPKID & " And AAI_CompID=" & iAcID & ""
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInvoiceTotal(ByVal sAc As String, ByVal iAcID As Integer, ByVal iPKID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0) From AuditAssignment_InvoiceDetails Where AAID_AAI_ID=" & iPKID & " And AAID_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTaxInvoiceTotal(ByVal sAc As String, ByVal iAcID As Integer, ByVal iPKID As Integer, ByVal dTaxTypePercentage As Decimal) As String
        Dim sSql As String
        Try
            sSql = "Select (" & dTaxTypePercentage & "*ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0)/100) From AuditAssignment_InvoiceDetails Where AAID_IsTaxable=1 And"
            sSql = sSql & " AAID_AAI_ID=" & iPKID & " And AAID_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFinalInvoiceTotal(ByVal sAc As String, ByVal iAcID As Integer, ByVal iPKID As Integer, ByVal dTaxType1Percentage As Decimal, ByVal dTaxType2Percentage As Decimal) As String
        Dim sSql As String
        Dim dWithTax As Decimal, dWithOutTax As Decimal
        Try
            sSql = "Select (" & dTaxType1Percentage & "*ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0)/100) + (" & dTaxType2Percentage & "*ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0)/100) From AuditAssignment_InvoiceDetails "
            sSql = sSql & " Where AAID_IsTaxable=1 And AAID_AAI_ID=" & iPKID & " And AAID_CompID=" & iAcID & ""
            dWithTax = objDBL.SQLExecuteScalar(sAc, sSql)
            sSql = "Select ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0) From AuditAssignment_InvoiceDetails Where AAID_AAI_ID=" & iPKID & " And AAID_CompID=" & iAcID & ""
            dWithOutTax = objDBL.SQLExecuteScalar(sAc, sSql)
            Return dWithTax + dWithOutTax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyDetailsForInvoice(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCompanyID As Integer) As DataTable
        Dim sSql As String, sCompany_Conditions As String
        Dim dt As New DataTable, dtComp As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("Company_Code")
            dt.Columns.Add("Company_Name")
            dt.Columns.Add("Company_Address")
            dt.Columns.Add("Company_City_PinCode")
            dt.Columns.Add("Company_State")
            dt.Columns.Add("Company_PlaceOfSupply")
            dt.Columns.Add("Company_EmailID")
            dt.Columns.Add("Company_TelephoneNo")
            dt.Columns.Add("Company_PAN")
            dt.Columns.Add("Company_GSTIN")
            dt.Columns.Add("Company_HolderName")
            dt.Columns.Add("Company_BankName")
            dt.Columns.Add("Company_Branch")
            dt.Columns.Add("Company_BankAccountNo")
            dt.Columns.Add("Company_BankIFSCcode")
            dt.Columns.Add("Company_Paymentterms")
            dt.Columns.Add("Company_Conditions")
            dt.Columns.Add("Company_PaymenttermsAndConditions")

            sSql = "Select Company_Code,Company_Name,Company_Address,Company_City,Company_State,Company_PinCode,Company_EmailID,Company_TelephoneNo,Company_HolderName,Company_Bankname,Company_Branch,Company_AccountNo,"
            sSql = sSql & " Company_Conditions,Company_Paymentterms From Trace_CompanyDetails Where Company_ID=" & iCompanyID & ""

            dtComp = objDBL.SQLExecuteDataTable(sAc, sSql)
            dr = dt.NewRow()
            dr("Company_Code") = dtComp.Rows(0)("Company_Code")
            dr("Company_Name") = dtComp.Rows(0)("Company_Name")
            dr("Company_Address") = dtComp.Rows(0)("Company_Address")
            dr("Company_City_PinCode") = dtComp.Rows(0)("Company_City") & " " & dtComp.Rows(0)("Company_PinCode")
            dr("Company_State") = "State: " & dtComp.Rows(0)("Company_State")
            dr("Company_PlaceOfSupply") = "Place of Supply: " & dtComp.Rows(0)("Company_State")
            dr("Company_EmailID") = "Email: " & dtComp.Rows(0)("Company_EmailID")
            dr("Company_TelephoneNo") = "Phone no.: " & dtComp.Rows(0)("Company_TelephoneNo")
            dr("Company_PAN") = "PAN: "
            dr("Company_GSTIN") = "GSTIN "
            If IsDBNull(dtComp.Rows(0)("Company_HolderName")) = False Then
                dr("Company_HolderName") = "Account Holder Name : " & dtComp.Rows(0)("Company_HolderName")
            Else
                dr("Company_HolderName") = "Account Holder Name : -"
            End If
            If IsDBNull(dtComp.Rows(0)("Company_Bankname")) = False Then
                dr("Company_BankName") = "Bank Name : " & dtComp.Rows(0)("Company_Bankname")
            Else
                dr("Company_BankName") = "Bank Name : -"
            End If
            If IsDBNull(dtComp.Rows(0)("Company_Branch")) = False Then
                dr("Company_Branch") = "Branch : " & dtComp.Rows(0)("Company_Branch")
            Else
                dr("Company_Branch") = "Branch : -"
            End If
            If IsDBNull(dtComp.Rows(0)("Company_AccountNo")) = False Then
                dr("Company_BankAccountNo") = "Bank Account No. : " & dtComp.Rows(0)("Company_AccountNo")
            Else
                dr("Company_BankAccountNo") = "Bank Account No. : -"
            End If
            dr("Company_BankIFSCcode") = "Bank IFSC Code: "
            If IsDBNull(dtComp.Rows(0)("Company_Conditions")) = False Then
                dr("Company_Conditions") = dtComp.Rows(0)("Company_Conditions")
                sCompany_Conditions = dtComp.Rows(0)("Company_Conditions")
            Else
                dr("Company_Conditions") = ""
                sCompany_Conditions = ""
            End If
            If IsDBNull(dtComp.Rows(0)("Company_Paymentterms")) = False Then
                dr("Company_Paymentterms") = dtComp.Rows(0)("Company_Paymentterms") + " " + sCompany_Conditions
                dr("Company_PaymenttermsAndConditions") = dtComp.Rows(0)("Company_Paymentterms") + " " + sCompany_Conditions
            Else
                dr("Company_Paymentterms") = ""
                dr("Company_PaymenttermsAndConditions") = sCompany_Conditions
            End If
            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyLogoSignatureDetailsForInvoice(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCompanyID As Integer,
                                                         ByVal imageBase64DataLogoString As String, ByVal imageBase64DataSignatureString As String) As DataTable
        Dim sSql As String, sCompany_Conditions As String
        Dim dt As New DataTable, dtComp As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("Company_Code")
            dt.Columns.Add("Company_Name")
            dt.Columns.Add("Company_Address")
            dt.Columns.Add("Company_City_PinCode")
            dt.Columns.Add("Company_State")
            dt.Columns.Add("Company_PlaceOfSupply")
            dt.Columns.Add("Company_EmailID")
            dt.Columns.Add("Company_TelephoneNo")
            dt.Columns.Add("Company_PAN")
            dt.Columns.Add("Company_GSTIN")
            dt.Columns.Add("Company_HolderName")
            dt.Columns.Add("Company_BankName")
            dt.Columns.Add("Company_Branch")
            dt.Columns.Add("Company_BankAccountNo")
            dt.Columns.Add("Company_BankIFSCcode")
            dt.Columns.Add("Company_Paymentterms")
            dt.Columns.Add("Company_Conditions")
            dt.Columns.Add("Company_PaymenttermsAndConditions")
            dt.Columns.Add("Company_Logo")
            dt.Columns.Add("Company_Signature")

            sSql = "Select Company_Code,Company_Name,Company_Address,Company_City,Company_State,Company_PinCode,Company_EmailID,Company_TelephoneNo,Company_HolderName,Company_Bankname,Company_Branch,Company_AccountNo,"
            sSql = sSql & " Company_Conditions,Company_Paymentterms From Trace_CompanyDetails Where Company_ID=" & iCompanyID & ""

            dtComp = objDBL.SQLExecuteDataTable(sAc, sSql)
            dr = dt.NewRow()
            dr("Company_Code") = dtComp.Rows(0)("Company_Code")
            dr("Company_Name") = dtComp.Rows(0)("Company_Name")
            dr("Company_Address") = dtComp.Rows(0)("Company_Address")
            dr("Company_City_PinCode") = dtComp.Rows(0)("Company_City") & " " & dtComp.Rows(0)("Company_PinCode")
            dr("Company_State") = "State: " & dtComp.Rows(0)("Company_State")
            dr("Company_PlaceOfSupply") = "Place of Supply: " & dtComp.Rows(0)("Company_State")
            dr("Company_EmailID") = "Email: " & dtComp.Rows(0)("Company_EmailID")
            dr("Company_TelephoneNo") = "Phone no.: " & dtComp.Rows(0)("Company_TelephoneNo")
            dr("Company_PAN") = "PAN: "
            dr("Company_GSTIN") = "GSTIN "
            If IsDBNull(dtComp.Rows(0)("Company_HolderName")) = False Then
                dr("Company_HolderName") = "Account Holder Name : " & dtComp.Rows(0)("Company_HolderName")
            Else
                dr("Company_HolderName") = "Account Holder Name : -"
            End If
            If IsDBNull(dtComp.Rows(0)("Company_Bankname")) = False Then
                dr("Company_BankName") = "Bank Name : " & dtComp.Rows(0)("Company_Bankname")
            Else
                dr("Company_BankName") = "Bank Name : -"
            End If
            If IsDBNull(dtComp.Rows(0)("Company_Branch")) = False Then
                dr("Company_Branch") = "Branch : " & dtComp.Rows(0)("Company_Branch")
            Else
                dr("Company_Branch") = "Branch : -"
            End If
            If IsDBNull(dtComp.Rows(0)("Company_AccountNo")) = False Then
                dr("Company_BankAccountNo") = "Bank Account No. : " & dtComp.Rows(0)("Company_AccountNo")
            Else
                dr("Company_BankAccountNo") = "Bank Account No. : -"
            End If
            dr("Company_BankIFSCcode") = "Bank IFSC Code: "
            If IsDBNull(dtComp.Rows(0)("Company_Conditions")) = False Then
                dr("Company_Conditions") = dtComp.Rows(0)("Company_Conditions")
                sCompany_Conditions = dtComp.Rows(0)("Company_Conditions")
            Else
                dr("Company_Conditions") = ""
                sCompany_Conditions = ""
            End If
            If IsDBNull(dtComp.Rows(0)("Company_Paymentterms")) = False Then
                dr("Company_Paymentterms") = dtComp.Rows(0)("Company_Paymentterms") + " " + sCompany_Conditions
                dr("Company_PaymenttermsAndConditions") = dtComp.Rows(0)("Company_Paymentterms") + " " + sCompany_Conditions
            Else
                dr("Company_Paymentterms") = ""
                dr("Company_PaymenttermsAndConditions") = sCompany_Conditions
            End If
            If imageBase64DataLogoString IsNot Nothing Then
                dr("Company_Logo") = imageBase64DataLogoString
            Else
                dr("Company_Logo") = ""
            End If
            If imageBase64DataSignatureString IsNot Nothing Then
                dr("Company_Signature") = imageBase64DataSignatureString
            Else
                dr("Company_Signature") = ""
            End If
            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerDetailsForInvoice(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustID As Integer) As DataTable
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

            sSql = "Select CUST_ID,CUST_NAME,CUST_ADDRESS,CUST_CITY,CUST_STATE,CUST_PIN,CUST_EMAIL,CUST_TELPHONE From SAD_CUSTOMER_MASTER Where CUST_ID=" & iCustID & " And CUST_CompID=" & iAcID & ""
            dtComp = objDBL.SQLExecuteDataTable(sAc, sSql)
            dr = dt.NewRow()
            dr("CUST_NAME") = dtComp.Rows(0)("CUST_NAME")
            dr("CUST_ADDRESS") = dtComp.Rows(0)("CUST_ADDRESS")
            dr("CUST_CITY_PIN") = dtComp.Rows(0)("CUST_CITY") & " " & dtComp.Rows(0)("CUST_PIN")
            dr("CUST_STATE") = "State: " & dtComp.Rows(0)("CUST_STATE")
            dr("CUST_EMAIL") = dtComp.Rows(0)("CUST_EMAIL")
            dr("CUST_TELPHONE") = dtComp.Rows(0)("CUST_TELPHONE")
            dr("CUST_PAN") = ""
            dr("CUST_GSTIN") = "GSTIN Number: " & objDBL.SQLExecuteScalar(sAc, "Select Cust_Value From SAD_CUST_Accounting_Template Where Cust_ID=" & dtComp.Rows(0)("CUST_ID") & " And Cust_Desc='GSTIN'")
            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubTaskDetailsForInvoice(ByVal sAc As String, ByVal iAcID As Integer, ByVal iPKIdID As Integer) As DataTable
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

            sSql = "Select AAID_Desc,Case When CMM_Desc IS NULL then 'Reimbursement of expenses' else CMM_Desc End CMM_Desc,CMM_HSNSAC As AAID_HSNSAC,AAID_Quantity,AAID_PricePerUnit From AuditAssignment_InvoiceDetails"
            sSql = sSql & " Left Join AuditAssignment_Schedule On AAS_ID=AAID_AAS_ID"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAS_TaskID"
            sSql = sSql & " Where AAID_AAI_ID=" & iPKIdID & " And AAID_CompID=" & iAcID & ""

            dtInvoice = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtInvoice.Rows.Count - 1
                dr = dt.NewRow()
                iSlNo = iSlNo + 1
                dr("SrNo") = iSlNo
                dr("ItemName") = dtInvoice.Rows(i)("CMM_Desc")
                dr("Description") = dtInvoice.Rows(i)("CMM_Desc") + " (" + dtInvoice.Rows(i)("AAID_Desc") + ")"
                If IsDBNull(dtInvoice.Rows(0)("AAID_HSNSAC")) = False Then
                    dr("HSNSAC") = dtInvoice.Rows(i)("AAID_HSNSAC")
                Else
                    dr("HSNSAC") = ""
                End If
                dr("Quantity") = dtInvoice.Rows(i)("AAID_Quantity")
                dr("PricePerUnit") = "₹" & dtInvoice.Rows(i)("AAID_PricePerUnit")
                dr("Amount") = "₹" & (dtInvoice.Rows(i)("AAID_Quantity") * dtInvoice.Rows(i)("AAID_PricePerUnit"))
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function NumberToWord(ByVal val As String) As String
        Dim words, strones(100), strtens(100), aftrdecimalWord As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle, aftrDecimal1, aftrDecimal, num As Double
        Try
            If (val.Contains(".")) Then
                Dim str1 As String() = Strings.Split(val, ".")
                num = Convert.ToDouble(str1(0))
            Else
                num = Convert.ToDouble(val)
            End If
            aftrDecimal1 = num

            If num = 0 Then
                Return ""
            End If

            If num < 0 Then
                Return "Not supported"
            End If

            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If (num > 10000000) Then

                If ((Convert.ToString(num / 10000000)).Contains(".")) Then
                    crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
                    num = num - (crore * 10000000)
                Else
                    crore = num / 100
                    num = num - (crore * 10000000)
                End If
            End If

            If (num > 100000) Then

                If ((Convert.ToString(num / 100000)).Contains(".")) Then
                    lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                    num = num - (lakhs * 100000)
                Else
                    lakhs = num / 100000
                    num = num - (lakhs * 100000)
                End If
            End If

            If (num > 1000) Then

                If ((Convert.ToString(num / 1000)).Contains(".")) Then
                    thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                    num = num - (thousands * 1000)
                Else
                    thousands = num / 1000
                    num = num - (thousands * 1000)
                End If
            End If

            If (num >= 100) Then
                If ((Convert.ToString(num / 100)).Contains(".")) Then
                    hundreds = Convert.ToInt32((num / 100).ToString().Substring(0, (num / 100).ToString().IndexOf(".")))
                    num = num - (hundreds * 100)
                Else
                    hundreds = num / 100
                    num = num - (hundreds * 100)
                End If
            End If
            If num > 19 Then
                If ((Convert.ToString(num / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((num / 10).ToString().Substring(0, (num / 10).ToString().IndexOf(".")))
                    num = num - (tens * 10)
                Else
                    tens = num / 10
                    num = num - (tens * 10)
                End If
            End If

            ssingle = num

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then
                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then
                If thousands > 19 Then
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If

            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If

            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If

            If (val.Contains(".")) Then
                Dim str As String() = Strings.Split(val, ".")
                aftrDecimal = Convert.ToDouble(str(1))
                aftrdecimalWord = AfterDecimalfunction(aftrDecimal)
                If aftrdecimalWord = "zero" Then
                    words += ""
                Else
                    aftrdecimalWord += " Paise"
                    words += " And " + aftrdecimalWord

                End If
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AfterDecimalfunction(ByVal val As Decimal) As String
        Dim words, strones(100), strtens(100) As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle As Decimal
        Try
            If val = 0 Then
                Return "Zero"
            End If

            If val < 0 Then
                Return "Not supported"
            End If
            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If ((Convert.ToString(val / 10000000)).Contains(".")) Then
                crore = Convert.ToInt32((val / 10000000).ToString().Substring(0, (val / 10000000).ToString().IndexOf(".")))
                val = val - (hundreds * 10000000)
            Else
                crore = val / 10000000
                val = val - (hundreds * 10000000)
            End If

            If ((Convert.ToString(val / 100000)).Contains(".")) Then
                lakhs = Convert.ToInt32((val / 100000).ToString().Substring(0, (val / 100000).ToString().IndexOf(".")))
                val = val - (hundreds * 100000)
            Else
                lakhs = val / 100000
                val = val - (hundreds * 100000)
            End If

            If ((Convert.ToString(val / 1000)).Contains(".")) Then
                thousands = Convert.ToInt32((val / 1000).ToString().Substring(0, (val / 1000).ToString().IndexOf(".")))
                val = val - (thousands * 1000)
            Else
                thousands = val / 1000
                val = val - (thousands * 1000)
            End If

            thousands = Convert.ToInt32((val / 1000).ToString().Substring(0, (val / 1000).ToString().IndexOf(".")))
            val = val - (thousands * 1000)

            If ((Convert.ToString(val / 100)).Contains(".")) Then
                hundreds = Convert.ToInt32((val / 100).ToString().Substring(0, (val / 100).ToString().IndexOf(".")))
                val = val - (hundreds * 100)
            Else
                hundreds = val / 100
                val = val - (hundreds * 100)
            End If
            If val > 19 Then
                If ((Convert.ToString(val / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((val / 10).ToString().Substring(0, (val / 10).ToString().IndexOf(".")))
                    val = val - (tens * 10)
                Else
                    tens = val / 10
                    val = val - (tens * 10)
                End If
            End If

            ssingle = val

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then
                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then
                If thousands > 19 Then
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If

            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If

            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetComplianceMonthlyTaskDetailsForGraph(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearId As Integer, ByVal sYear As String, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer) As DataTable
        Dim dt As New DataTable, dtData As New DataTable
        Dim dr As DataRow
        Dim sSql As String
        Dim dDate As Date
        Try
            dt.Columns.Add("Month")
            dt.Columns.Add("NonCompliance")
            dt.Columns.Add("DelayedCompliance")
            dt.Columns.Add("On-TimeCompliance")

            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim iMonth As Integer = dDate.Month
            If (iMonth > 3) Then
                For i = 4 To iMonth
                    sSql = "Select Choose(" & i & ",'Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Nov','Dec') + '-" & sYear.Substring(0, 4) & "' As Month,(Select Count(*) from AuditAssignment_SubTask "
                    sSql = sSql & "Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & " "
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    If iPartnerID > 0 Then
                        sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_DueDate >" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_Closed=0) As 'NonCompliance', "
                    sSql = sSql & "(Select Count(*) from AuditAssignment_SubTask Join AuditAssignment_Schedule on AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & " "
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    If iPartnerID > 0 Then
                        sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_DueDate<" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_Closed=0 ) As 'DelayedCompliance',"
                    sSql = sSql & "(Select Count(*) from AuditAssignment_SubTask Join AuditAssignment_Schedule on AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & ""
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    If iPartnerID > 0 Then
                        sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_Closed=1) As 'On-TimeCompliance'"

                    dtData = objDBL.SQLExecuteDataTable(sAc, sSql)
                    For j = 0 To dtData.Rows.Count - 1
                        dr = dt.NewRow()
                        dr("Month") = dtData.Rows(j)("Month")
                        dr("NonCompliance") = dtData.Rows(j)("NonCompliance")
                        dr("DelayedCompliance") = dtData.Rows(j)("DelayedCompliance")
                        dr("On-TimeCompliance") = dtData.Rows(j)("On-TimeCompliance")
                        dt.Rows.Add(dr)
                    Next
                Next
            End If
            If (iMonth <= 3) Then
                For i = 4 To 12
                    sSql = "Select Choose(" & i & ",'Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Nov','Dec') + '-" & sYear.Substring(0, 4) & "' As Month,(Select Count(*) from AuditAssignment_SubTask "
                    sSql = sSql & "Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & " "
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_DueDate >" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_Closed=0) As 'NonCompliance', "
                    sSql = sSql & "(Select Count(*) from AuditAssignment_SubTask Join AuditAssignment_Schedule on AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & " "
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_DueDate<" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_Closed=0 ) As 'DelayedCompliance',"
                    sSql = sSql & "(Select Count(*) from AuditAssignment_SubTask Join AuditAssignment_Schedule on AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & ""
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_Closed=1) As 'On-TimeCompliance'"

                    dtData = objDBL.SQLExecuteDataTable(sAc, sSql)
                    For j = 0 To dtData.Rows.Count - 1
                        dr = dt.NewRow()
                        dr("Month") = dtData.Rows(j)("Month")
                        dr("NonCompliance") = dtData.Rows(j)("NonCompliance")
                        dr("DelayedCompliance") = dtData.Rows(j)("DelayedCompliance")
                        dr("On-TimeCompliance") = dtData.Rows(j)("On-TimeCompliance")
                        dt.Rows.Add(dr)
                    Next
                Next
                For i = 1 To iMonth
                    sSql = "Select Choose(" & i & ",'Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Nov','Dec') + '-" & sYear.Substring(5, 4) & "' As Month,(Select Count(*) from AuditAssignment_SubTask "
                    sSql = sSql & "Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & " "
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_DueDate >" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_Closed=0) As 'NonCompliance', "
                    sSql = sSql & "(Select Count(*) from AuditAssignment_SubTask Join AuditAssignment_Schedule on AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & " "
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_DueDate<" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_Closed=0 ) As 'DelayedCompliance',"
                    sSql = sSql & "(Select Count(*) from AuditAssignment_SubTask Join AuditAssignment_Schedule on AAS_ID=AAST_AAS_ID And AAS_YearID=" & iYearId & ""
                    If iCustomerID > 0 Then
                        sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
                    End If
                    sSql = sSql & " Where MONTH(AAST_DueDate)=" & i & " And AAST_Closed=1) As 'On-TimeCompliance'"

                    dtData = objDBL.SQLExecuteDataTable(sAc, sSql)
                    For j = 0 To dtData.Rows.Count - 1
                        dr = dt.NewRow()
                        dr("Month") = dtData.Rows(j)("Month")
                        dr("NonCompliance") = dtData.Rows(j)("NonCompliance")
                        dr("DelayedCompliance") = dtData.Rows(j)("DelayedCompliance")
                        dr("On-TimeCompliance") = dtData.Rows(j)("On-TimeCompliance")
                        dt.Rows.Add(dr)
                    Next
                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadResourceAvailability(ByVal sAc As String, ByVal iAcID As Integer, ByVal dDate As Date) As DataTable
    '    Dim sSql As String, sSql1 As String
    '    Dim dt As New DataTable, dtAsg As New DataTable, dtEmp As New DataTable
    '    Dim dr As DataRow, iSlNo As Integer = 0
    '    Try
    '        dt.Columns.Add("SrNo")
    '        dt.Columns.Add("Employee")
    '        dt.Columns.Add("TotalSubTasks")
    '        dt.Columns.Add("AssignmentNo")
    '        dt.Columns.Add("Task")
    '        dt.Columns.Add("SubTask")
    '        dt.Columns.Add("DueDate")
    '        dt.Columns.Add("ExpectedCompletionDate")

    '        sSql = "Select Usr_ID,usr_FullName,Count(AAST_ID) As Total from sad_userdetails "
    '        sSql = sSql & " Left Join AuditAssignment_SubTask on AAST_EmployeeID=Usr_id And "
    '        sSql = sSql & " AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_ExpectedCompletionDate>=" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & ""
    '        sSql = sSql & " where usr_compID=" & iAcID & " And USR_Partner <> 1 And (usr_DelFlag ='A' or usr_DelFlag='B' or usr_DelFlag='L') "
    '        sSql = sSql & " Group by Usr_ID, usr_FullName"
    '        sSql = sSql & " Order by usr_FullName"
    '        dtAsg = objDBL.SQLExecuteDataTable(sAc, sSql)
    '        For i = 0 To dtAsg.Rows.Count - 1
    '            iSlNo = iSlNo + 1

    '            dr = dt.NewRow()
    '            dr("SrNo") = iSlNo
    '            dr("Employee") = dtAsg.Rows(i)("usr_FullName")
    '            dr("TotalSubTasks") = dtAsg.Rows(i)("Total")

    '            sSql1 = "Select AAS_AssignmentNo,CMM_Desc,AM_Name,AAST_DueDate,AAST_ExpectedCompletionDate From AuditAssignment_SubTask"
    '            sSql1 = sSql1 & " Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID"
    '            sSql1 = sSql1 & " Join Content_Management_Master a on a.CMM_ID=AAS_TaskID"
    '            sSql1 = sSql1 & " Join AuditAssignmentSubTask_Master on AM_ID=AAST_SubTaskID"
    '            sSql1 = sSql1 & " Where AAST_EmployeeID=" & dtAsg.Rows(i)("Usr_ID") & " And AAST_CompID=" & iAcID & " And "
    '            sSql1 = sSql1 & " AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & " And AAST_ExpectedCompletionDate>=" & objclsGRACeGeneral.FormatDtForRDBMS(dDate, "Q") & ""
    '            dtEmp = objDBL.SQLExecuteDataTable(sAc, sSql1)
    '            For j = 0 To dtEmp.Rows.Count - 1
    '                If j > 0 Then : dr = dt.NewRow() : End If
    '                dr("AssignmentNo") = dtEmp.Rows(j)("AAS_AssignmentNo")
    '                dr("Task") = dtEmp.Rows(j)("CMM_Desc")
    '                dr("SubTask") = dtEmp.Rows(j)("AM_Name")
    '                dr("DueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAST_DueDate"), "D")
    '                dr("ExpectedCompletionDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAST_ExpectedCompletionDate"), "D")
    '                If dtEmp.Rows.Count > 1 Then : dt.Rows.Add(dr) : End If
    '                'If j > 0 Then : dt.Rows.Add(dr) : End If
    '            Next
    '            If dtEmp.Rows.Count <= 1 Then : dt.Rows.Add(dr) : End If
    '        Next
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadResourceAvailability(ByVal sAc As String, ByVal iAcID As Integer, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal iPartnerID As Integer, ByVal iEmployeeID As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim dt As New DataTable, dtAsg As New DataTable, dtEmp As New DataTable
        Dim dr As DataRow, iSlNo As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Employee")
            dt.Columns.Add("Customer")
            dt.Columns.Add("Partner")
            dt.Columns.Add("AssignmentNo")
            dt.Columns.Add("Task")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("ExpectedCompletionDate")

            sSql = "Select Distinct(Usr_ID),usr_FullName from sad_userdetails Inner Join AuditAssignment_SubTask on AAST_EmployeeID=Usr_id And "
            sSql = sSql & " ((AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And AAST_DueDate>=" & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ")"
            sSql = sSql & " Or (AAST_ExpectedCompletionDate>=" & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & "))"
            sSql = sSql & " where usr_compID=" & iAcID & " And (usr_DelFlag ='A' or usr_DelFlag='B' or usr_DelFlag='L') "
            If iEmployeeID > 0 Then
                sSql = sSql & " And Usr_ID=" & iEmployeeID & ""
            Else
                sSql = sSql & " And USR_Partner <> 1"
            End If
            sSql = sSql & " Order by usr_FullName"
            dtAsg = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtAsg.Rows.Count - 1
                iSlNo = iSlNo + 1

                dr = dt.NewRow()
                dr("SrNo") = iSlNo
                dr("Employee") = dtAsg.Rows(i)("usr_FullName")

                sSql1 = "Select CUST_Name,usr_FullName,AAS_AssignmentNo,CMM_Desc,Min(AAST_DueDate) As AAST_DueDate,Max(AAST_ExpectedCompletionDate) As AAST_ExpectedCompletionDate From AuditAssignment_SubTask "
                sSql1 = sSql1 & " Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID"
                If iPartnerID > 0 Then
                    sSql1 = sSql1 & " And AAS_PartnerID=" & iPartnerID & ""
                End If
                sSql1 = sSql1 & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
                sSql1 = sSql1 & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
                sSql1 = sSql1 & " Join Content_Management_Master a on a.CMM_ID=AAS_TaskID "
                sSql1 = sSql1 & " Where AAST_EmployeeID=" & dtAsg.Rows(i)("Usr_ID") & " And AAST_CompID=" & iAcID & " And "
                sSql1 = sSql1 & " ((AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And AAST_DueDate>=" & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ")"
                sSql1 = sSql1 & " Or (AAST_ExpectedCompletionDate>=" & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & "))"
                sSql1 = sSql1 & " group by CUST_Name,usr_FullName,AAS_AssignmentNo,CMM_Desc,AAST_DueDate,AAST_ExpectedCompletionDate Order by AAS_AssignmentNo"
                dtEmp = objDBL.SQLExecuteDataTable(sAc, sSql1)
                For j = 0 To dtEmp.Rows.Count - 1
                    If j > 0 Then : dr = dt.NewRow() : End If
                    dr("Customer") = dtEmp.Rows(j)("CUST_Name")
                    dr("Partner") = dtEmp.Rows(j)("usr_FullName")
                    dr("AssignmentNo") = dtEmp.Rows(j)("AAS_AssignmentNo")
                    dr("Task") = dtEmp.Rows(j)("CMM_Desc")
                    dr("DueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAST_DueDate"), "D")
                    dr("ExpectedCompletionDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtEmp.Rows(j)("AAST_ExpectedCompletionDate"), "D")
                    If dtEmp.Rows.Count > 1 Then : dt.Rows.Add(dr) : End If
                    'If j > 0 Then : dt.Rows.Add(dr) : End If
                Next
                If dtEmp.Rows.Count <= 1 Then : dt.Rows.Add(dr) : End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadResourceStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal iPartnerID As Integer, ByVal iWorkStatusID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtAT As New DataTable, dtEmp As New DataTable
        Dim dr As DataRow, iSlNo As Integer = 0, iCount As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Employee")

            sSql = "Select cmm_ID,cmm_Desc From Content_Management_Master where cmm_Category='AT' And cmm_Delflag='A' "
            sSql = sSql & " And cmm_ID in (Select AAS_TaskID From AuditAssignment_Schedule Where "
            If iPartnerID > 0 Then
                sSql = sSql & " AAS_PartnerID=" & iPartnerID & " And"
            End If
            sSql = sSql & " AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where "
            If iWorkStatusID > 0 Then
                sSql = sSql & " AAST_WorkStatusID=" & iWorkStatusID & " And"
            End If
            sSql = sSql & " AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & "))"
            sSql = sSql & " order by cmm_Desc"
            dtAT = objDBL.SQLExecuteDataTable(sAc, sSql)
            For x = 0 To dtAT.Rows.Count - 1
                dt.Columns.Add(dtAT.Rows(x)("cmm_Desc"))
            Next

            sSql = "Select Distinct(Usr_ID),usr_FullName from sad_userdetails Inner Join AuditAssignment_SubTask on AAST_EmployeeID=Usr_id And "
            sSql = sSql & " AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & ""
            If iPartnerID > 0 Then
                sSql = sSql & " And AAST_AAS_ID in (Select AAS_ID From AuditAssignment_Schedule Where AAS_PartnerID=" & iPartnerID & ") "
            End If
            If iWorkStatusID > 0 Then
                sSql = sSql & " And AAST_WorkStatusID=" & iWorkStatusID & ""
            End If
            sSql = sSql & " where usr_compID=" & iAcID & " And (usr_DelFlag ='A' or usr_DelFlag='B' or usr_DelFlag='L') "
            sSql = sSql & " Order by usr_FullName"
            dtEmp = objDBL.SQLExecuteDataTable(sAc, sSql)
            For y = 0 To dtEmp.Rows.Count - 1
                iSlNo = iSlNo + 1

                dr = dt.NewRow()
                dr("SrNo") = iSlNo
                dr("Employee") = dtEmp.Rows(y)("USr_FullName")
                For z = 0 To dtAT.Rows.Count - 1
                    sSql = "Select Count(*) From AuditAssignment_SubTask Where AAST_AAS_ID in (Select AAS_ID From AuditAssignment_Schedule Where AAS_TaskID=" & dtAT.Rows(z)("cmm_ID") & " "
                    If iPartnerID > 0 Then
                        sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
                    End If
                    sSql = sSql & ") "
                    sSql = sSql & " And AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & " "
                    sSql = sSql & " And AAST_EmployeeID=" & dtEmp.Rows(y)("Usr_ID") & " "
                    If iWorkStatusID > 0 Then
                        sSql = sSql & " And AAST_WorkStatusID=" & iWorkStatusID & ""
                    End If
                    iCount = objDBL.SQLExecuteScalarInt(sAc, sSql)
                    If iCount > 0 Then
                        dr(dtAT.Rows(z)("cmm_Desc")) = iCount
                    End If
                Next
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoiceReports(ByVal sAc As String, ByVal iAcID As Integer, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal sInvoiceNo As String,
                                       ByVal iTaxTypeID As Integer, ByVal iEntityID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                       ByVal iEmployeeID As Integer, ByVal iTaskID As Integer, ByVal iWorkstatusID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtInvoice As New DataTable, dtEmp As New DataTable
        Dim dr As DataRow, iSlNo As Integer = 0
        Dim dWithTaxAmount As Double, dWithOutTaxAmount As Double, dTax As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("InvoiceID")
            dt.Columns.Add("InvoiceNo")
            dt.Columns.Add("InvoiceDate")
            dt.Columns.Add("InvoiceType")
            dt.Columns.Add("BillingEntity")
            dt.Columns.Add("Customer")
            dt.Columns.Add("CustomerGSTNo")
            dt.Columns.Add("BeforeTax")
            dt.Columns.Add("TaxType1")
            dt.Columns.Add("TaxType2")
            dt.Columns.Add("TotalTax")
            dt.Columns.Add("AfterTax")
            dt.Columns.Add("Partner")
            dt.Columns.Add("Employee")

            sSql = "Select Distinct(AAI_ID),AAI_InvoiceNo,AAI_CrOn,Case When AAI_InvoiceTypeID=1 then 'Proforma Invoice' else 'Tax Invoice' End InvoiceType,Company_Name,Cust_Id,CUST_Name,"
            sSql = sSql & " Employee=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID in (Select AAID_AAS_ID From AuditAssignment_InvoiceDetails Where AAID_AAI_ID=AAI_ID) And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Partner=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_Schedule,sad_userdetails WHERE AAS_ID in (Select AAID_AAS_ID From AuditAssignment_InvoiceDetails Where AAID_AAI_ID=AAI_ID) And Usr_ID=AAS_PartnerID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " AAI_TaxType1Percentage,AAI_TaxType2Percentage From AuditAssignment_Invoice"
            sSql = sSql & " Join AuditAssignment_InvoiceDetails ON AAID_AAI_ID=AAI_ID"
            sSql = sSql & " Join AuditAssignment_Schedule On AAS_ID=AAID_AAS_ID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join Trace_CompanyDetails on Company_ID=AAI_BillingEntity_ID"
            sSql = sSql & " Where AAI_CompID=" & iAcID & ""
            sSql = sSql & " And AAI_CrOn between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & " "
            If sInvoiceNo <> "" Then
                sSql = sSql & " And AAI_InvoiceNo Like '%" & sInvoiceNo & "%'"
            End If
            If iTaxTypeID > 0 Then
                sSql = sSql & " And AAI_InvoiceTypeID=" & iTaxTypeID & ""
            End If
            If iEntityID > 0 Then
                sSql = sSql & " And AAI_BillingEntity_ID=" & iEntityID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iEmployeeID > 0 Then
                sSql = sSql & " And AAID_AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_EmployeeID=" & iEmployeeID & ")"
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If iWorkstatusID > 0 Then
                sSql = sSql & " And AAID_AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_WorkStatusID=" & iWorkstatusID & ")"
            End If
            sSql = sSql & " Order by AAI_InvoiceNo"
            dtInvoice = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtInvoice.Rows.Count - 1
                iSlNo = iSlNo + 1
                dr = dt.NewRow()
                dr("SrNo") = iSlNo
                dr("InvoiceID") = dtInvoice.Rows(i)("AAI_ID")
                dr("InvoiceNo") = dtInvoice.Rows(i)("AAI_InvoiceNo")
                dr("InvoiceDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtInvoice.Rows(i)("AAI_CrOn"), "D")
                dr("InvoiceType") = dtInvoice.Rows(i)("InvoiceType")
                If IsDBNull(dtInvoice.Rows(i)("Company_Name")) = False Then
                    dr("BillingEntity") = dtInvoice.Rows(i)("Company_Name")
                End If
                dr("Customer") = dtInvoice.Rows(i)("Cust_Name")
                dr("CustomerGSTNo") = objDBL.SQLExecuteScalar(sAc, "Select Cust_Value From SAD_CUST_Accounting_Template Where Cust_ID=" & dtInvoice.Rows(i)("Cust_Id") & " And Cust_Desc='GSTIN'")
                dWithTaxAmount = objDBL.SQLExecuteScalar(sAc, "Select ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0) From AuditAssignment_InvoiceDetails Where AAID_IsTaxable=1 And AAID_AAI_ID=" & dtInvoice.Rows(i)("AAI_ID") & "")
                dWithOutTaxAmount = objDBL.SQLExecuteScalar(sAc, "Select ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0) From AuditAssignment_InvoiceDetails Where AAID_IsTaxable=0 And AAID_AAI_ID=" & dtInvoice.Rows(i)("AAI_ID") & "")
                dr("BeforeTax") = String.Format("{0:0.00}", Convert.ToDecimal(dWithTaxAmount + dWithOutTaxAmount))
                dr("TaxType1") = String.Format("{0:0.00}", Convert.ToDecimal(dtInvoice.Rows(i)("AAI_TaxType1Percentage") * dWithTaxAmount / 100))
                dr("TaxType2") = String.Format("{0:0.00}", Convert.ToDecimal(dtInvoice.Rows(i)("AAI_TaxType2Percentage") * dWithTaxAmount / 100))
                dTax = (dtInvoice.Rows(i)("AAI_TaxType1Percentage") * dWithTaxAmount / 100) + (dtInvoice.Rows(i)("AAI_TaxType2Percentage") * dWithTaxAmount / 100)
                dr("TotalTax") = String.Format("{0:0.00}", Convert.ToDecimal(dTax))
                dr("AfterTax") = String.Format("{0:0.00}", Convert.ToDecimal(dWithTaxAmount + dTax + dWithOutTaxAmount))
                dr("Partner") = dtInvoice.Rows(i)("Partner")
                dr("Employee") = dtInvoice.Rows(i)("Employee")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssignmentDetailsForCust(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCompanyId As Integer, ByVal iCustId As Integer,
                                                 ByVal iWorkStatusID As Integer, ByVal iStatusId As Integer, ByVal iMonthID As Integer) As DataTable
        Dim dt As New DataTable, dtAsg As New DataTable
        Dim dr As DataRow
        Dim sSql As String, iSlNo As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PKID")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("Customer")
            dt.Columns.Add("AssignmentID")
            dt.Columns.Add("AssignmentNo")
            dt.Columns.Add("Task")
            dt.Columns.Add("InvoiceTypeID")
            dt.Columns.Add("Invoice")
            dt.Columns.Add("Date")
            dt.Columns.Add("Amount")

            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "Select Distinct(AAS_ID),AAS_CustID,Cust_Name,AAS_AssignmentNo,CMM_Desc,AAS_AdvancePartialBilling,AAI_ID,AAI_InvoiceTypeID,AAI_CrOn,AAID_Quantity,AAID_PricePerUnit From AuditAssignment_Schedule "
            sSql = sSql & " Left Join AuditAssignment_InvoiceDetails On AAID_AAS_ID=AAS_ID And AAID_AAI_ID in (Select AAI_ID From AuditAssignment_Invoice where AAI_BillingEntity_ID=" & iCompanyId & ")"
            sSql = sSql & " Left Join AuditAssignment_Invoice On AAI_BillingEntity_ID=" & iCompanyId & " And AAI_ID=AAID_AAI_ID"
            If iCustId > 0 Then
                sSql = sSql & " And AAI_Cust_ID=" & iCustId & ""
            End If
            sSql = sSql & " Join SAD_CUSTOMER_MASTER On Cust_Id=AAS_CustID"
            sSql = sSql & " Join Content_Management_Master On CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & ""
            If iMonthID > 0 Then
                sSql = sSql & " Join AuditAssignment_EmpSubTask on AAEST_AAS_ID=AAS_ID And AAEST_AAST_ID=AAST_ID And DATEPART(month,AAEST_CrOn)=" & iMonthID & ""
            End If
            sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & " "
            If iCustId > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustId & ""
            End If
            If iWorkStatusID > 0 And iStatusId <> 2 Then
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_WorkStatusID=" & iWorkStatusID & " And (SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=" & iStatusId & ")"
            Else
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_WorkStatusID=" & iWorkStatusID & " And ((Select Count(*) From AuditAssignment_EmpSubTask Where AAEST_AAS_ID=AAS_ID)=0))"
            End If
            sSql = sSql & " And AAS_ID Not in (Select AAID_AAS_ID From AuditAssignment_InvoiceDetails Where AAID_AAI_ID in (Select AAI_ID From AuditAssignment_Invoice where AAI_BillingEntity_ID<>" & iCompanyId & "))"
            sSql = sSql & " And AAS_ID Not in (Select AAID_AAS_ID From AuditAssignment_InvoiceDetails Where AAID_AAI_ID in (Select AAI_ID From AuditAssignment_Invoice where AAI_InvoiceTypeID=2 And AAI_BillingEntity_ID=" & iCompanyId & "))"
            sSql = sSql & " order by AAS_ID"
            dtAsg = objDBL.SQLExecuteDataTable(sAc, sSql)

            'sSql = "Select Cust_Name,Usr_FullName,AAS_ID,AAS_AssignmentNo,AAS_AdvancePartialBilling,AM_Name,AAST_DueDate,Case When AAST_Closed=0 then AAST_ExpectedCompletionDate else AAEST_CrOn End ExpectedCompletionDate From AuditAssignment_SubTask "
            'sSql = sSql & " Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID"
            'sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
            'sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            'sSql = sSql & " Join AuditAssignmentSubTask_Master on AM_ID=AAST_SubTaskID"
            'sSql = sSql & " Left Join AuditAssignment_EmpSubTask On AAEST_ID=(Select Max(AAEST_ID) From AuditAssignment_EmpSubTask where AAEST_AAST_ID=AAST_ID)"
            'sSql = sSql & " Where AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & " And AAST_Closed=" & iType & " And AAST_CompID=" & iAcID & ""
            'If iCustomerID > 0 Then
            '    sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            'End If
            'If iPartnerID > 0 Then
            '    sSql = sSql & " And AAST_EmployeeID=" & iPartnerID & ""
            'End If
            'If iWIPId > 0 Then
            '    sSql = sSql & " And AAST_WorkStatusID=" & iWIPId & ""
            'End If
            'sSql = sSql & " Order by AAS_AssignmentNo"

            For x = 0 To dtAsg.Rows.Count - 1
                dr = dt.NewRow()
                iSlNo = iSlNo + 1
                dr("SrNo") = iSlNo
                Dim iPKID As Integer = 0
                If IsDBNull(dtAsg.Rows(x)("AAI_ID")) = False Then
                    iPKID = dtAsg.Rows(x)("AAI_ID")
                End If
                dr("PKID") = iPKID
                dr("CustomerID") = dtAsg.Rows(x)("AAS_CustID")
                dr("Customer") = dtAsg.Rows(x)("Cust_Name")
                dr("AssignmentID") = dtAsg.Rows(x)("AAS_ID")
                dr("AssignmentNo") = dtAsg.Rows(x)("AAS_AssignmentNo")
                dr("Task") = dtAsg.Rows(x)("CMM_Desc")
                If IsDBNull(dtAsg.Rows(x)("AAI_InvoiceTypeID")) = False Then
                    If dtAsg.Rows(x)("AAI_InvoiceTypeID") = 2 Then
                        dr("InvoiceTypeID") = dtAsg.Rows(x)("AAI_InvoiceTypeID")
                        dr("Invoice") = "Tax Invoice"
                    ElseIf dtAsg.Rows(x)("AAI_InvoiceTypeID") = 1 Then
                        dr("InvoiceTypeID") = dtAsg.Rows(x)("AAI_InvoiceTypeID")
                        dr("Invoice") = "Proforma Invoice"
                    End If
                ElseIf IsDBNull(dtAsg.Rows(x)("AAS_AdvancePartialBilling")) = False And IsDBNull(dtAsg.Rows(x)("AAI_InvoiceTypeID")) = True Then
                    If dtAsg.Rows(x)("AAS_AdvancePartialBilling") = 1 Then
                        dr("InvoiceTypeID") = 3
                        dr("Invoice") = "Advance/Partial"
                    End If
                End If
                If IsDBNull(dtAsg.Rows(x)("AAI_CrOn")) = False Then
                    dr("Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtAsg.Rows(x)("AAI_CrOn"), "D")
                End If
                If IsDBNull(dtAsg.Rows(x)("AAID_Quantity")) = False And IsDBNull(dtAsg.Rows(x)("AAID_PricePerUnit")) = False Then
                    dr("Amount") = (dtAsg.Rows(x)("AAID_Quantity") * dtAsg.Rows(x)("AAID_PricePerUnit"))
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoiceDetailsForCust(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustId As Integer, ByVal iCompanyId As Integer,
                                              ByVal sPKIds As String, ByVal sAsgIds As String) As DataTable
        Dim dt As New DataTable, dtAsg As New DataTable, dtOID As New DataTable
        Dim dr As DataRow
        Dim sSql As String = "", sSql1 As String = "", iSlNo As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AssignmentID")
            dt.Columns.Add("CustomerID")
            dt.Columns.Add("AssignmentNo")
            dt.Columns.Add("IsTaxable")
            dt.Columns.Add("Task")
            dt.Columns.Add("Description")
            dt.Columns.Add("HSNSAC")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("PricePerUnit")
            dt.Columns.Add("Amount")

            If sPKIds <> "" Then
                sSql = "Select AAS_ID,AAS_CustID,AAS_AssignmentNo,Case When AAID_Desc IS NULL then CMM_Desc else AAID_Desc End AAID_Desc,CMM_Desc,CMM_HSNSAC,AAID_Quantity,AAID_PricePerUnit,AAID_IsTaxable From AuditAssignment_Schedule "
                sSql = sSql & " Join Content_Management_Master On CMM_ID=AAS_TaskID  "
                sSql = sSql & " Right Join AuditAssignment_InvoiceDetails On AAID_AAS_ID=AAS_ID"
                sSql = sSql & " Right Join AuditAssignment_Invoice On AAI_BillingEntity_ID=" & iCompanyId & " And AAI_ID=AAID_AAI_ID "
                If iCustId > 0 Then
                    sSql = sSql & " And AAI_Cust_ID=" & iCustId & ""
                End If
                sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & " And AAID_AAS_ID<>0 And AAI_ID In (" & sPKIds & ")"
                If iCustId > 0 Then
                    sSql = sSql & " And AAS_CustID=" & iCustId & ""
                End If

                sSql1 = " Select 0 as AAS_ID,AAI_Cust_ID As AAS_CustID,'' As AAS_AssignmentNo,'Reimbursement of expenses' As CMM_Desc,AAID_Desc,'' As CMM_HSNSAC,AAID_Quantity,"
                sSql1 = sSql1 & " AAID_PricePerUnit,AAID_IsTaxable From AuditAssignment_InvoiceDetails"
                sSql1 = sSql1 & " Right Join AuditAssignment_Invoice On AAI_BillingEntity_ID=" & iCompanyId & " And AAI_ID=AAID_AAI_ID "
                If iCustId > 0 Then
                    sSql1 = sSql1 & " And AAI_Cust_ID=" & iCustId & ""
                End If
                sSql1 = sSql1 & " where AAID_AAS_ID=0 And AAI_ID In (" & sPKIds & ")"
            End If
            If sAsgIds <> "" Then
                If sPKIds <> "" Then
                    sSql = sSql & " UNION "
                End If
                sSql = sSql & "Select AAS_ID,AAS_CustID,AAS_AssignmentNo,Case When AAID_Desc IS NULL then CMM_Desc else AAID_Desc End AAID_Desc,CMM_Desc,CMM_HSNSAC,AAID_Quantity,AAID_PricePerUnit,AAID_IsTaxable From AuditAssignment_Schedule "
                sSql = sSql & " Join Content_Management_Master On CMM_ID=AAS_TaskID "
                sSql = sSql & " Left Join AuditAssignment_InvoiceDetails On AAID_AAS_ID=AAS_ID "
                sSql = sSql & " Left Join AuditAssignment_Invoice On AAI_BillingEntity_ID=" & iCompanyId & " And AAI_ID=AAID_AAI_ID "
                If iCustId > 0 Then
                    sSql = sSql & " And AAI_Cust_ID=" & iCustId & ""
                End If
                sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
                If iCustId > 0 Then
                    sSql = sSql & " And AAS_CustID=" & iCustId & ""
                End If
                sSql = sSql & " And AAS_ID In (" & sAsgIds & ")"
                sSql = sSql & " order by AAS_ID"

                If sPKIds = "" Then
                    sSql1 = " Select '0' as AAS_ID," & iCustId & " As AAS_CustID,'' As AAS_AssignmentNo,'Reimbursement of expenses' As CMM_Desc,NULL As AAID_Desc,NULL As CMM_HSNSAC,NULL As AAID_Quantity,"
                    sSql1 = sSql1 & " NULL As AAID_PricePerUnit,NULL As AAID_IsTaxable"
                End If
            End If
            dtAsg = objDBL.SQLExecuteDataTable(sAc, sSql)
            dtOID = objDBL.SQLExecuteDataTable(sAc, sSql1)

            For x = 0 To dtAsg.Rows.Count - 1
                dr = dt.NewRow()
                iSlNo = iSlNo + 1
                dr("SrNo") = iSlNo
                dr("AssignmentID") = dtAsg.Rows(x)("AAS_ID")
                dr("CustomerID") = dtAsg.Rows(x)("AAS_CustID")
                dr("AssignmentNo") = dtAsg.Rows(x)("AAS_AssignmentNo")
                dr("IsTaxable") = dtAsg.Rows(x)("AAID_IsTaxable")
                dr("Task") = dtAsg.Rows(x)("CMM_Desc")
                dr("Description") = dtAsg.Rows(x)("AAID_Desc")
                dr("HSNSAC") = dtAsg.Rows(x)("CMM_HSNSAC")
                dr("Quantity") = dtAsg.Rows(x)("AAID_Quantity")
                dr("PricePerUnit") = dtAsg.Rows(x)("AAID_PricePerUnit")
                If IsDBNull(dtAsg.Rows(x)("AAID_Quantity")) = False And IsDBNull(dtAsg.Rows(x)("AAID_PricePerUnit")) = False Then
                    dr("Amount") = (dtAsg.Rows(x)("AAID_Quantity") * dtAsg.Rows(x)("AAID_PricePerUnit"))
                End If
                dt.Rows.Add(dr)
            Next
            For x = 0 To dtOID.Rows.Count - 1
                dr = dt.NewRow()
                iSlNo = iSlNo + 1
                dr("SrNo") = iSlNo
                dr("AssignmentID") = dtOID.Rows(x)("AAS_ID")
                dr("CustomerID") = dtOID.Rows(x)("AAS_CustID")
                dr("AssignmentNo") = dtOID.Rows(x)("AAS_AssignmentNo")
                dr("IsTaxable") = dtOID.Rows(x)("AAID_IsTaxable")
                dr("Task") = dtOID.Rows(x)("CMM_Desc")
                dr("Description") = dtOID.Rows(x)("AAID_Desc")
                dr("HSNSAC") = dtOID.Rows(x)("CMM_HSNSAC")
                dr("Quantity") = dtOID.Rows(x)("AAID_Quantity")
                dr("PricePerUnit") = dtOID.Rows(x)("AAID_PricePerUnit")
                If IsDBNull(dtOID.Rows(x)("AAID_Quantity")) = False And IsDBNull(dtOID.Rows(x)("AAID_PricePerUnit")) = False Then
                    dr("Amount") = (dtOID.Rows(x)("AAID_Quantity") * dtOID.Rows(x)("AAID_PricePerUnit"))
                End If
                dt.Rows.Add(dr)
            Next

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateNewInvoiceNo(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearID As Integer, ByVal iCompanyId As Integer, ByVal iInvoiceTypeID As Integer) As String
        Dim iMaxID As Integer
        Dim sMaxID As String = ""
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAc, "Select Count(*)+1 from AuditAssignment_Invoice where AAI_YearID=" & iYearID & " And AAI_BillingEntity_ID=" & iCompanyId & " And AAI_InvoiceTypeID=" & iInvoiceTypeID & "")
            If iMaxID = 0 Then
                sMaxID = "0001"
            ElseIf iMaxID > 0 And iMaxID < 10 Then
                sMaxID = "000" & iMaxID
            ElseIf iMaxID >= 10 And iMaxID < 100 Then
                sMaxID = "00" & iMaxID
            ElseIf iMaxID >= 100 And iMaxID < 1000 Then
                sMaxID = "0" & iMaxID
            Else
                sMaxID = iMaxID
            End If
            Return sMaxID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubTaskDetailsForInvoiceReportDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iInvoiceTypeID As Integer, ByVal iCustId As Integer, ByVal iCompanyId As Integer, ByVal sPKIds As String) As DataTable
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "Select AAI_ID From AuditAssignment_Invoice Where AAI_InvoiceTypeID=" & iInvoiceTypeID & " And AAI_BillingEntity_ID=" & iCompanyId & " And AAI_CompID=" & iAcID & ""
            If iCustId > 0 Then
                sSql = sSql & " And AAI_Cust_ID=" & iCustId & ""
            End If
            If sPKIds <> "" Then
                sSql = sSql & " And AAI_ID In (" & sPKIds & ")"
            End If
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveInvoiceDetails(ByVal sAC As String, ByVal objAAID As strAuditAssignment_InvoiceDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_AAI_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_AAI_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_AAS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_AAS_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_IsTaxable", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_IsTaxable
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAAID.sAAID_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_HSNSAC", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_HSNSAC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_PricePerUnit", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = objAAID.dAAID_PricePerUnit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAAID.sAAID_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAID_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAAID.iAAID_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAuditAssignment_InvoiceDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReviewAssignmentTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(AAS_ID) As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_AssignmentNo Desc) As SrNo,AAS_AssignmentNo,AAS_CustID As CustomerID,Cust_Name As CustomerName,AAS_TaskID As TaskID,CMM_Desc As Task,"
            sSql = sSql & " Employee=(Select USr_FullName FROM sad_userdetails Where Usr_ID In(Select Top 1 AAEST_CrBy FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID Order by AAEST_ID Desc)),"
            sSql = sSql & " DateOfRequest=Convert(Varchar(10),(Select AAEST_CrOn From AuditAssignment_EmpSubTask where AAEST_ID=(Select Max(AAEST_ID) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)),103),"
            sSql = sSql & " Remarks=(Select AAEST_Comments From AuditAssignment_EmpSubTask where AAEST_ID=(Select Max(AAEST_ID) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID))"
            sSql = sSql & " From AuditAssignment_Schedule "
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_Review=1 And AAST_AAS_ID=AAS_ID"
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAST_EmployeeID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Join Content_Management_Master On CMM_ID=AAS_TaskID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER On Cust_Id=AAS_CustID"
            sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
            If bLoginUserIsPartner = True Then
                sSql = sSql & " And AAS_PartnerID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Order by AAS_AssignmentNo Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompletedAssignmentTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer, ByVal dCompletionDate As Date, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(AAS_ID) As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_AssignmentNo Desc) As SrNo,AAS_AssignmentNo,AAS_CustID As CustomerID,Cust_Name As CustomerName,AAS_TaskID As TaskID,CMM_Desc As Task,"
            sSql = sSql & " Employee=(Select USr_FullName FROM sad_userdetails Where Usr_ID In(Select Top 1 AAEST_CrBy FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID Order by AAEST_ID Desc)),"
            sSql = sSql & " DueDate=Convert(Varchar(10),(Select Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " DateOfCompletion=Convert(Varchar(10),(Select Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID),103)"
            sSql = sSql & " From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=1"
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAST_EmployeeID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Join AuditAssignment_EmpSubTask On AAEST_AAS_ID=AAS_ID And AAEST_AAST_ID=AAST_ID And AAEST_CrOn=" & objclsGRACeGeneral.FormatDtForRDBMS(dCompletionDate, "Q") & ""
            sSql = sSql & " Join Content_Management_Master On CMM_ID=AAS_TaskID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER On Cust_Id=AAS_CustID"
            sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
            If bLoginUserIsPartner = True Then
                sSql = sSql & " And AAS_PartnerID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Order by AAS_AssignmentNo Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadPendingAssignmentTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select Distinct(AAS_ID) As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_AssignmentNo Desc) As SrNo,AAS_AssignmentNo,AAS_CustID As CustomerID,Cust_Name As CustomerName,AAS_TaskID As TaskID,CMM_Desc As Task,"
    '        sSql = sSql & " Employee=(Select USr_FullName FROM sad_userdetails Where Usr_ID In(Select Top 1 AAEST_CrBy FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID Order by AAEST_ID Desc)),"
    '        sSql = sSql & " Convert(Varchar(10), AAS_CrOn, 103) As CreatedDate,"
    '        sSql = sSql & " ExpectedCompletionDate=Convert(Varchar(10),(Select Max(AAST_ExpectedCompletionDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103)"
    '        sSql = sSql & " From AuditAssignment_Schedule "
    '        sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_ExpectedCompletionDate<=GetDate()"
    '        sSql = sSql & " Join Content_Management_Master On CMM_ID=AAS_TaskID"
    '        sSql = sSql & " Join SAD_CUSTOMER_MASTER On Cust_Id=AAS_CustID"
    '        sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & " And AAS_PartnerID=" & iLoginUserID & ""
    '        sSql = sSql & " Order by AAS_AssignmentNo Desc"
    '        dt = objDBL.SQLExecuteDataTable(sAc, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadPendingAssignmentTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(AAS_ID) As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_AssignmentNo Desc) As SrNo,AAS_AssignmentNo,AAS_CustID As CustomerID,Cust_Name As CustomerName,AAS_TaskID As TaskID,CMM_Desc As Task,"
            sSql = sSql & " Employee=STUFF ((Select DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID=AAS_ID And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Convert(Varchar(10), AAS_CrOn, 103) As CreatedDate,"
            sSql = sSql & " DueDate=Convert(Varchar(10),(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103)"
            sSql = sSql & " From AuditAssignment_Schedule "
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate<=GetDate() And ((Select Count(*) From AuditAssignment_EmpSubTask Where AAEST_AAS_ID=AAS_ID)=0)"
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAST_EmployeeID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
            If bLoginUserIsPartner = True Then
                sSql = sSql & " And AAS_PartnerID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Order by AAS_AssignmentNo Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPendingAssignmentTaskChartDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select DATENAME(month,AAST_DueDate ) 'Month Name',Count(Distinct(AAS_ID)) As PendingTasks "
            sSql = sSql & " From AuditAssignment_Schedule "
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_ExpectedCompletionDate<=GetDate()"
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAST_EmployeeID=" & iLoginUserID & ""
            End If
            sSql = sSql & "Where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & "  group by DATENAME(month,AAST_DueDate ),MONTH(AAST_DueDate ) "
            sSql = sSql & "order by MONTH(AAST_DueDate) asc "
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUnbilledAssignmentTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(AAS_ID) As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_AssignmentNo Desc) As SrNo,AAS_AssignmentNo,AAS_CustID As CustomerID,Cust_Name As CustomerName,AAS_TaskID As TaskID,CMM_Desc As Task,"
            sSql = sSql & " Employee=(SELECT USr_FullName FROM sad_userdetails Where Usr_ID in(SELECT Top 1 AAEST_CrBy FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID Order by AAEST_ID Desc)),"
            sSql = sSql & " Convert(Varchar(10), AAS_CrOn, 103) As CreatedDate,"
            sSql = sSql & " CompletionDate=Convert(Varchar(10),(SELECT Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID),103)"
            sSql = sSql & " From AuditAssignment_Schedule "
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=1 And AAST_Review=0 "
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAST_EmployeeID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Join AuditAssignment_EmpSubTask on AAEST_AAS_ID=AAS_ID And AAEST_AAST_ID=AAST_ID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & " And AAS_BillingType<>3"
            If bLoginUserIsPartner = True Then
                sSql = sSql & " And AAS_PartnerID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Order by AAS_AssignmentNo Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRevenueAssignmentTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer, ByVal iMonthID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(AAS_ID) As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_AssignmentNo Desc) As SrNo,AAS_AssignmentNo,AAS_CustID As CustomerID,Cust_Name As CustomerName,AAS_TaskID As TaskID,CMM_Desc As Task,"
            sSql = sSql & " Employee=(SELECT USr_FullName FROM sad_userdetails Where Usr_ID in(SELECT Top 1 AAEST_CrBy FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID Order by AAEST_ID Desc)),"
            sSql = sSql & " DateOfCompletion=Convert(Varchar(10),(SELECT Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " BillAmount=(Select ISNULL(Sum(AAID_Quantity * AAID_PricePerUnit), 0) From AuditAssignment_InvoiceDetails Where AAID_AAS_ID=AAS_ID And AAID_AAI_ID in (Select AAI_ID From AuditAssignment_Invoice Where AAI_InvoiceTypeID=2))"
            sSql = sSql & " From AuditAssignment_Schedule"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=1 "
            If bLoginUserIsPartner = False And iLoginUserID > 0 Then
                sSql = sSql & " And AAST_EmployeeID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Join AuditAssignment_EmpSubTask on AAEST_AAS_ID=AAS_ID And AAEST_AAST_ID=AAST_ID And DATEPART(month,AAEST_CrOn)=" & iMonthID & ""
            sSql = sSql & " Join AuditAssignment_InvoiceDetails on AAID_AAS_ID=AAS_ID And AAID_AAI_ID in (Select AAI_ID From AuditAssignment_Invoice Where AAI_InvoiceTypeID=2)"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " where AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
            If bLoginUserIsPartner = True And iLoginUserID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Order by AAS_AssignmentNo Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMonthlyPerformanceAssignmentTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iLoginUserID As Integer, ByVal iMonthID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select a.Employee,DENSE_RANK() OVER (ORDER BY Employee) As SrNo,Count(a.AAST_AAS_ID) As TasksCompleted From "
            sSql = sSql & " (Select Distinct(AAST_AAS_ID),Employee=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID=AAS_ID And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,'')"
            sSql = sSql & " From AuditAssignment_SubTask"
            sSql = sSql & " Join AuditAssignment_Schedule On AAS_ID=AAST_AAS_ID And AAS_CompID=" & iAcID & " And AAS_YearID=" & iFinancialYearID & ""
            If bLoginUserIsPartner = True And iLoginUserID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iLoginUserID & ""
            End If
            sSql = sSql & " Join AuditAssignment_EmpSubTask On DATEPART(month,AAEST_CrOn)=" & iMonthID & " And  AAEST_AAS_ID=AAS_ID And  AAEST_AAST_ID=AAST_ID"
            sSql = sSql & " Where "
            If bLoginUserIsPartner = False And iLoginUserID > 0 Then
                sSql = sSql & " AAST_EmployeeID=" & iLoginUserID & " And"
            End If
            sSql = sSql & " ((SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1)) a"
            sSql = sSql & " Group by a.Employee"
            sSql = sSql & " Order by a.Employee"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUnAssignedTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iFrequencyID As Integer, ByVal iFrequencyDetailsID As Integer, ByVal sAct As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct Comp_CustID As CustomerID,Cust_Name As Customer,ROW_NUMBER() OVER (ORDER BY Cust_Name) As SrNo,Comp_Task As TaskID,CMM_Desc As Task,CMM_Act As Act,"
            sSql = sSql & " '' AS AssignmentNo,'' As 'Partner','' As Employee, '' As DueDate,'' As ExpectedCompletionDate From SAD_Compliance_Details"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER On Cust_ID=Comp_CustID"
            sSql = sSql & " Join Content_Management_Master On CMM_Category='AT' And CMM_ID=Comp_Task"
            If sAct <> "" Then
                sSql = sSql & " And CMM_ID in (Select cmm_ID From Content_Management_Master Where CMM_CompID=" & iAcID & " and CMM_Act='" & sAct & "')"
            End If
            sSql = sSql & " Where Comp_DelFlag='A' And Comp_Frequency=" & iFrequencyID & " And "
            sSql = sSql & " (Select Count(*) From AuditAssignment_Schedule Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_FrequencyID=" & iFrequencyID & ""
            sSql = sSql & " Where AAS_IsComplianceAsg=1 And AAS_CustId=Comp_CustID And AAS_YearID=" & iFinancialYearID & ""
            If iFrequencyID > 1 And iFrequencyDetailsID > 0 Then
                sSql = sSql & " And AAST_YearOrMonthID=" & iFrequencyDetailsID & " "
            End If
            sSql = sSql & " And AAS_TaskID=Comp_Task)=0"
            If iCustomerID > 0 Then
                sSql = sSql & " And Comp_CustID=" & iCustomerID & ""
            End If
            sSql = sSql & " Order by SrNo"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssignedTaskDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                ByVal sAct As String, ByVal iEmpID As Integer, ByVal iFrequencyID As Integer, ByVal iFrequencyDetailsID As Integer, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "SELECT AAS_ID As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_ID Desc) As SrNo,AAS_AssignmentNo As AssignmentNo,AAS_CustID As CustomerID,Cust_Name As Customer,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
            sSql = sSql & " AAS_PartnerID,USr_FullName As 'Partner',AAS_YearID,YMS_ID As FinancialYear,AAS_TaskID As TaskID,CMM_Desc As Task,CMM_Act As Act,"
            sSql = sSql & " SubTask=STUFF ((SELECT DISTINCT '; '+ CAST(AM_Name AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,AuditAssignmentSubTask_Master  WHERE AAST_AAS_ID=AAS_ID And AM_ID=AAST_SubTaskID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Employee=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID=AAS_ID And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Convert(Varchar(10),AAS_CrOn,103) As CreatedDate,"
            sSql = sSql & " DueDate=Convert(Varchar(10),(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " ExpectedCompletionDate=Convert(Varchar(10),(SELECT Max(AAST_ExpectedCompletionDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " TimeTaken=(Select CASE WHEN ((SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1) THEN"
            sSql = sSql & " (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),(SELECT Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)))"
            'sSql = sSql & " ELSE (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) END)+1,"
            sSql = sSql & " ELSE (( CASE WHEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) >= 0 THEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) ELSE -1 END)) END)+1,"
            sSql = sSql & " WorkStatus=STUFF ((SELECT DISTINCT '; '+ CAST(CMM_Desc AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,Content_Management_Master WHERE AAST_AAS_ID=AAS_ID And CMM_ID=AAST_WorkStatusID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Closed=(SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),"
            sSql = sSql & " Comments=(SELECT AAEST_Comments From AuditAssignment_EmpSubTask where AAEST_ID=(Select Max(AAEST_ID) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)),"
            sSql = sSql & " Case When AAS_BillingType=0 then '' When AAS_BillingType=1 then 'Billable' when AAS_BillingType=2 then 'Proforma' when AAS_BillingType=3 then 'Billed' End As BillingStatus"
            sSql = sSql & " FROM AuditAssignment_Schedule"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
            sSql = sSql & " Join Year_Master on YMS_YearID=AAS_YearID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_FrequencyID=" & iFrequencyID & "" 'And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & "
            If iFrequencyID > 1 And iFrequencyDetailsID > 0 Then
                sSql = sSql & " And AAST_YearOrMonthID=" & iFrequencyDetailsID & " "
            End If
            sSql = sSql & " Where AAS_IsComplianceAsg=1 And AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If sAct <> "" Then
                sSql = sSql & " And AAS_TaskID in (Select cmm_ID From Content_Management_Master Where CMM_CompID=" & iAcID & " and CMM_Act='" & sAct & "')"
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
                'If sWorkStatusID <> "" Then
                '    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                'End If
                sSql = sSql & " )"
            Else
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
                'If sWorkStatusID <> "" Then
                '    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                'End If
                sSql = sSql & " )"
            End If
            sSql = sSql & " GROUP BY AAS_ID,AAS_AssignmentNo,AAS_CustID,Cust_Name,AAS_PartnerID,USr_FullName,AAS_YearID,YMS_ID,AAS_TaskID,CMM_Desc,CMM_Act,AAS_CrOn,AAS_BillingType"
            sSql = sSql & " Order by AAS_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveAuditAssignmentUserLogDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iUserLoginLogPKID As Integer, ByVal iUserID As Integer, ByVal iScheduledAsgID As Integer)
        Dim sSql As String
        Dim iStatus As Integer, iPKID As Integer
        Try
            sSql = "Select AAS_Status From AuditAssignment_Schedule Where AAS_ID=" & iScheduledAsgID & " And AAS_CompID=" & iAcID & ""
            iStatus = objDBL.SQLExecuteScalarInt(sAc, sSql)

            sSql = "Select AAUL_ID From AuditAssignment_UserLog Where AAUL_ADT_KEYID=" & iUserLoginLogPKID & " And AAUL_UserID=" & iUserID & " And AAUL_AAS_ID=" & iScheduledAsgID & " And AAUL_CompID=" & iAcID & ""
            iPKID = objDBL.SQLExecuteScalarInt(sAc, sSql)

            If iPKID = 0 Then
                sSql = "Insert Into AuditAssignment_UserLog (AAUL_ADT_KEYID,AAUL_UserID,AAUL_Date,AAUL_AAS_ID,AAUL_AAS_Status,AAUL_CompID) values (" & iUserLoginLogPKID & "," & iUserID & ",GetDate()," & iScheduledAsgID & "," & iStatus & "," & iAcID & ")"
            Else
                sSql = "Update AuditAssignment_UserLog set AAUL_AAS_Status=" & iStatus & " Where AAUL_ID=" & iPKID & " And AAUL_ADT_KEYID=" & iUserLoginLogPKID & " And AAUL_UserID=" & iUserID & " And AAUL_AAS_ID=" & iScheduledAsgID & " And AAUL_CompID=" & iAcID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadDashboardAllPartnerScheduledAssignmentCounts(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                           ByVal iTaskID As Integer, ByVal iEmpID As Integer, ByVal sWorkStatusID As String, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            sSql = "select a.usr_FullName,ISNULL(b.OpenTasks, 0 ) as OpenTasks,ISNULL(a.ClosedTasks, 0 )as ClosedTasks ,ISNULL(c.OverDueTasks , 0 )as OverDueTasks from"
            sSql = sSql & " (Select usr_Id,usr_FullName,Count(Distinct(AAS_ID)) As ClosedTasks From AuditAssignment_Schedule "
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And ((Select Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1)"
            sSql = sSql & " Join sad_userdetails on usr_Id=AAS_PartnerID"
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            sSql = sSql & " group by usr_FullName,usr_Id"
            sSql = sSql & ")a left join"
            sSql = sSql & "(Select  usr_Id,usr_FullName,Count(Distinct(AAS_ID)) As OpenTasks From AuditAssignment_Schedule "
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Join sad_userdetails on usr_Id=AAS_PartnerID"
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            sSql = sSql & " group by usr_FullName,usr_Id"
            sSql = sSql & " )b on a.usr_Id=b.usr_Id  left join"
            sSql = sSql & "(Select usr_Id,usr_FullName,Count(Distinct(AAS_ID)) As OverDueTasks From AuditAssignment_Schedule "
            sSql = sSql & " Join AuditAssignment_SubTask On AAST_AAS_ID=AAS_ID And AAST_Closed=0 And AAST_ExpectedCompletionDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentDate, "Q") & ""
            sSql = sSql & " Join sad_userdetails on usr_Id=AAS_PartnerID"
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            sSql = sSql & " group by usr_FullName,usr_Id"
            sSql = sSql & " )c on a.usr_Id=c.usr_Id"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Getdata(ByVal sAc As String, ByVal iAcID As Integer, ByVal frmdate As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, b.usr_FullName,Sum(CASE WHEN AAUL_AAS_Status=0 THEN 1 ELSE 0 END) as  "
            sSql = sSql & " Created,Sum(CASE WHEN AAUL_AAS_Status=1 THEN 1 ELSE 0 END) as InProgress, "
            sSql = sSql & " Sum(CASE WHEN AAUL_AAS_Status=2 THEN 1 ELSE 0 END) as Completed "
            sSql = sSql & " FROM AuditAssignment_UserLog left join Sad_UserDetails b on AAUL_UserID = b.usr_Id "
            sSql = sSql & " Where CONVERT(VARCHAR(10), AAUL_Date, 103)='" & frmdate & "'"
            sSql = sSql & " group by AAUL_ADT_KEYID,b.usr_FullName "
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserTimelineData(ByVal sAc As String, ByVal iAcID As Integer, ByVal frmdate As String, ByVal iUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, b.usr_FullName,Sum(CASE WHEN AAUL_AAS_Status=0 THEN 1 ELSE 0 END) as  "
            sSql = sSql & " Created,Sum(CASE WHEN AAUL_AAS_Status=1 THEN 1 ELSE 0 END) as InProgress, "
            sSql = sSql & " Sum(CASE WHEN AAUL_AAS_Status=2 THEN 1 ELSE 0 END) as Completed "
            sSql = sSql & " FROM AuditAssignment_UserLog left join Sad_UserDetails b on AAUL_UserID = b.usr_Id "
            sSql = sSql & " Where CONVERT(VARCHAR(10), AAUL_Date, 103)='" & frmdate & "'"
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAUL_UserID=" & iUserID & " "
            End If
            sSql = sSql & " group by AAUL_ADT_KEYID,b.usr_FullName "
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserSignatureID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_Signature From Sad_UserDetails Where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadScheduledAssignmentDynamicReport(ByVal sAc As String, ByVal iAcID As Integer, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal iCustomerID As Integer, ByVal iPartnerID As Integer,
                                                ByVal iTaskID As Integer, ByVal iEmpID As Integer, ByVal sWorkStatusID As String, ByVal sComplianceID As String, ByVal bLoginUserIsPartner As Boolean, ByVal iLoginUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            'Dim dCurrentDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sAc), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Dim dCurrentMonthlastDate As DateTime = New DateTime(dCurrentDate.Year, dCurrentDate.Month, 1).AddMonths(1).AddDays(-1)

            sSql = "SELECT AAS_ID As AssignmentID,DENSE_RANK() OVER (ORDER BY AAS_ID Desc) As SrNo,AAS_AssignmentNo As AssignmentNo,AAS_CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
            sSql = sSql & " Case When AAST_FrequencyID=1 then 'Yearly' When AAST_FrequencyID=3 then 'Once' When AAST_FrequencyID=2 then ("
            sSql = sSql & " Case When AAST_YearOrMonthID=1 then 'January' When AAST_YearOrMonthID=2 then 'February' when AAST_YearOrMonthID=3 then 'March' when AAST_YearOrMonthID=4 then 'April'"
            sSql = sSql & " When AAST_YearOrMonthID=5 then 'May' When AAST_YearOrMonthID=6 then 'June' when AAST_YearOrMonthID=7 then 'July' when AAST_YearOrMonthID=8 then 'August'"
            sSql = sSql & " When AAST_YearOrMonthID=9 then 'September' When AAST_YearOrMonthID=10 then 'October' when AAST_YearOrMonthID=11 then 'November' when AAST_YearOrMonthID=12 then 'December' End) End as 'Month',"
            sSql = sSql & " AAS_PartnerID,USr_FullName As 'Partner',AAS_YearID,YMS_ID As FinancialYear,AAS_TaskID,CMM_Desc As Task,"
            sSql = sSql & " SubTask=STUFF ((SELECT DISTINCT '; '+ CAST(AM_Name AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,AuditAssignmentSubTask_Master  WHERE AAST_AAS_ID=AAS_ID And AM_ID=AAST_SubTaskID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Employee=STUFF ((SELECT DISTINCT '; '+ CAST(USr_FullName AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,sad_userdetails WHERE AAST_AAS_ID=AAS_ID And Usr_ID=AAST_EmployeeID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Convert(Varchar(10),AAS_CrOn,103) As CreatedDate,"
            sSql = sSql & " DueDate=Convert(Varchar(10),(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " ExpectedCompletionDate=Convert(Varchar(10),(SELECT Max(AAST_ExpectedCompletionDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),103),"
            sSql = sSql & " TimeTaken=(Select CASE WHEN ((SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID)=1) THEN"
            sSql = sSql & " (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),(SELECT Max(AAEST_CrOn) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)))"
            'sSql = sSql & " ELSE (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) END)+1,"
            sSql = sSql & " ELSE (( CASE WHEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) >= 0 THEN (SELECT DATEDIFF(day,(SELECT Min(AAST_DueDate) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),Getdate())) ELSE -1 END)) END)+1,"
            sSql = sSql & " WorkStatus=STUFF ((SELECT DISTINCT '; '+ CAST(CMM_Desc AS VARCHAR(MAX)) FROM AuditAssignment_SubTask,Content_Management_Master WHERE AAST_AAS_ID=AAS_ID And CMM_ID=AAST_WorkStatusID FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Closed=(SELECT Convert(int, Case When Count(AAST_ID)=sum(AAST_Closed) Then 1 Else 0 End) FROM AuditAssignment_SubTask WHERE AAST_AAS_ID=AAS_ID),"
            sSql = sSql & " Comments=(SELECT AAEST_Comments From AuditAssignment_EmpSubTask where AAEST_ID=(Select Max(AAEST_ID) FROM AuditAssignment_EmpSubTask WHERE AAEST_AAS_ID=AAS_ID)),"
            sSql = sSql & " Case When AAS_BillingType=0 then '' When AAS_BillingType=1 then 'Billable' when AAS_BillingType=2 then 'Proforma' when AAS_BillingType=3 then 'Billed' End As BillingStatus"
            sSql = sSql & " FROM AuditAssignment_Schedule"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=AAS_CustID"
            sSql = sSql & " Join sad_userdetails on Usr_ID=AAS_PartnerID"
            sSql = sSql & " Join Year_Master on YMS_YearID=AAS_YearID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=AAS_TaskID"
            sSql = sSql & " Join AuditAssignment_SubTask on AAST_AAS_ID=AAS_ID And AAST_DueDate between " & objclsGRACeGeneral.FormatDtForRDBMS(dFromDate, "Q") & " And " & objclsGRACeGeneral.FormatDtForRDBMS(dToDate, "Q") & "" 'And AAST_DueDate<=" & objclsGRACeGeneral.FormatDtForRDBMS(dCurrentMonthlastDate, "Q") & ""
            sSql = sSql & " Where AAS_CompID=" & iAcID & ""
            'If iFinancialYearID > 0 Then
            '    sSql = sSql & " And AAS_YearID=" & iFinancialYearID & ""
            'End If
            If iCustomerID > 0 Then
                sSql = sSql & " And AAS_CustID=" & iCustomerID & ""
            End If
            If iPartnerID > 0 Then
                sSql = sSql & " And AAS_PartnerID=" & iPartnerID & ""
            End If
            If iTaskID > 0 Then
                sSql = sSql & " And AAS_TaskID=" & iTaskID & ""
            End If
            If sComplianceID <> "" Then
                sSql = sSql & " And AAS_IsComplianceAsg in (" & sComplianceID & ")"
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 And iEmpID = iLoginUserID Then
                    sSql = sSql & " And (AAST_EmployeeID=" & iEmpID & " Or AAST_AssistedByEmployeesID Like ('%," & iEmpID & ",%')) "
                ElseIf iEmpID > 0 And iEmpID <> iLoginUserID Then
                    sSql = sSql & " And AAST_EmployeeID=0 And "
                Else
                    sSql = sSql & " And (AAST_EmployeeID=" & iLoginUserID & " Or AAST_AssistedByEmployeesID Like ('%," & iLoginUserID & ",%')) "
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            Else
                sSql = sSql & " And AAS_ID in (Select AAST_AAS_ID From AuditAssignment_SubTask Where AAST_CompId=" & iAcID & ""
                If iEmpID > 0 Then
                    sSql = sSql & " And AAST_EmployeeID=" & iEmpID & ""
                End If
                If sWorkStatusID <> "" Then
                    sSql = sSql & " And AAST_WorkStatusID in (" & sWorkStatusID & ")"
                End If
                sSql = sSql & " )"
            End If
            sSql = sSql & " GROUP BY AAS_ID,AAS_AssignmentNo,AAS_CustID,Cust_Name,AAST_FrequencyID,AAST_YearOrMonthID,AAS_PartnerID,USr_FullName,AAS_YearID,YMS_ID,AAS_TaskID,CMM_Desc,AAS_CrOn,AAS_BillingType"
            sSql = sSql & " Order by AAS_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
