Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Structure strCustMaster
    Dim CUST_ID As Integer
    Dim CUST_NAME As String
    Dim CUST_CODE As String
    Dim CUST_WEBSITE As String
    Dim CUST_EMAIL As String
    Dim CUST_GROUPNAME As String
    Dim CUST_GROUPINDIVIDUAL As String
    Dim CUST_ORGTYPEID As Integer
    Dim CUST_INDTYPEID As Integer
    Dim CUST_MGMTTYPEID As Integer
    Dim CUST_CommitmentDate As Date
    Dim CUSt_BranchId As String
    Dim CUST_COMM_ADDRESS As String
    Dim CUST_COMM_CITY As String
    Dim CUST_COMM_PIN As String
    Dim CUST_COMM_STATE As String
    Dim CUST_COMM_COUNTRY As String
    Dim CUST_COMM_FAX As String
    Dim CUST_COMM_TEL As String
    Dim CUST_COMM_Email As String
    Dim CUST_ADDRESS As String
    Dim CUST_CITY As String
    Dim CUST_PIN As String
    Dim CUST_STATE As String
    Dim CUST_COUNTRY As String
    Dim CUST_FAX As String
    Dim CUST_TELPHONE As String
    Dim CUST_ConEmailID As String
    Dim CUST_LOCATIONID As String
    Dim CUST_TASKS As String
    Dim CUST_ORGID As Integer
    Dim CUST_DELFLG As String
    Dim CUST_CRBY As Integer
    Dim CUST_UpdatedBy As Integer
    Dim CUST_APPROVEDBY As Integer
    Dim CUST_BOARDOFDIRECTORS As String
    Dim CUST_DEPMETHOD As Integer
    Dim CUST_STATUS As Char
    Dim CUST_IPAddress As String
    Dim CUST_CompID As Integer
    Dim CUST_Amount_Type As Integer
    Dim CUST_RoundOff As Integer
End Structure
Public Structure strCustDetails
    Dim CDET_ID As Integer
    Dim CDET_CUSTID As Integer
    Dim CDET_STANDINGININDUSTRY As String
    Dim CDET_PUBLICPERCEPTION As String
    Dim CDET_GOVTPERCEPTION As String
    Dim CDET_LITIGATIONISSUES As String
    Dim CDET_PRODUCTSMANUFACTURED As String
    Dim CDET_SERVICESOFFERED As String
    Dim CDET_TURNOVER As String
    Dim CDET_PROFITABILITY As String
    Dim CDET_FOREIGNCOLLABORATIONS As String
    Dim CDET_EMPLOYEESTRENGTH As String
    Dim CDET_PROFESSIONALSERVICES As String
    Dim CDET_GATHEREDBYAUDITFIRM As String
    Dim CDET_LEGALADVISORS As String
    Dim CDET_AUDITINCHARGE As String
    Dim CDET_FileNo As String
    Dim CDET_CRBY As Integer
    Dim CDET_UpdatedBy As Integer
    Dim CDET_STATUS As String
    Dim CDET_IPAddress As String
    Dim CDET_CompID As Integer
End Structure
Public Structure strCustLocation
    Dim Mas_Id As Integer
    Dim Mas_code As String
    Dim Mas_Description As String
    Dim Mas_DelFlag As String
    Dim Mas_CustID As Integer
    Dim Mas_Loc_Address As String
    Dim Mas_Contact_Person As String
    Dim Mas_Contact_MobileNo As String
    Dim Mas_Contact_LandLineNo As String
    Dim Mas_Contact_Email As String
    Dim mas_Designation As String
    Dim Mas_CRBY As Integer
    Dim Mas_UpdatedBy As Integer
    Dim Mas_STATUS As String
    Dim Mas_IPAddress As String
    Dim Mas_CompID As Integer
End Structure

Public Structure strCUSTAccountingTemplate
    Private Cust_PKID As Integer
    Private Cust_ID As Integer
    Private Cust_Desc As String
    Private Cust_Value As String
    Private Cust_Delflag As String
    Private Cust_Status As String
    Private Cust_AttchID As Integer
    Private Cust_CrBy As Integer
    Private Cust_UpdatedBy As Integer
    Private Cust_IPAddress As String
    Private Cust_Compid As Integer
    Private Cust_LocationId As Integer
    Public Property iCust_PKID() As Integer
        Get
            Return (Cust_PKID)
        End Get
        Set(ByVal Value As Integer)
            Cust_PKID = Value
        End Set
    End Property
    Public Property iCust_ID() As Integer
        Get
            Return (Cust_ID)
        End Get
        Set(ByVal Value As Integer)
            Cust_ID = Value
        End Set
    End Property
    Public Property sCust_Desc() As String
        Get
            Return (Cust_Desc)
        End Get
        Set(ByVal Value As String)
            Cust_Desc = Value
        End Set
    End Property
    Public Property sCust_Value() As String
        Get
            Return (Cust_Value)
        End Get
        Set(ByVal Value As String)
            Cust_Value = Value
        End Set
    End Property
    Public Property sCust_Delflag() As String
        Get
            Return (Cust_Delflag)
        End Get
        Set(ByVal Value As String)
            Cust_Delflag = Value
        End Set
    End Property
    Public Property sCust_Status() As String
        Get
            Return (Cust_Status)
        End Get
        Set(ByVal Value As String)
            Cust_Status = Value
        End Set
    End Property
    Public Property iCust_AttchID() As Integer
        Get
            Return (Cust_AttchID)
        End Get
        Set(ByVal Value As Integer)
            Cust_AttchID = Value
        End Set
    End Property
    Public Property iCust_CrBy() As Integer
        Get
            Return (Cust_CrBy)
        End Get
        Set(ByVal Value As Integer)
            Cust_CrBy = Value
        End Set
    End Property
    Public Property iCust_UpdatedBy() As Integer
        Get
            Return (Cust_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Cust_UpdatedBy = Value
        End Set
    End Property
    Public Property sCust_IPAddress() As String
        Get
            Return (Cust_IPAddress)
        End Get
        Set(ByVal Value As String)
            Cust_IPAddress = Value
        End Set
    End Property
    Public Property iCust_Compid() As Integer
        Get
            Return (Cust_Compid)
        End Get
        Set(ByVal Value As Integer)
            Cust_Compid = Value
        End Set
    End Property
    Public Property iCust_LocationId() As Integer
        Get
            Return (Cust_LocationId)
        End Get
        Set(ByVal Value As Integer)
            Cust_LocationId = Value
        End Set
    End Property
End Structure
Public Structure strLOE
    Dim LOE_Id As Integer
    Dim LOE_YearId As Integer
    Dim LOE_CustomerId As Integer
    Dim LOE_ServiceTypeId As Integer
    Dim LOE_NatureOfService As String
    Dim LOE_LocationIds As String
    Dim LOE_Milestones As String
    Dim LOE_TimeSchedule As Date
    Dim LOE_ReportDueDate As Date
    Dim LOE_ProfessionalFees As Integer
    Dim LOE_OtherFees As Integer
    Dim LOE_ServiceTax As Integer
    Dim LOE_RembFilingFee As Integer
    Dim LOE_CrBy As Integer
    Dim LOE_Total As Integer
    Dim LOE_Name As String
    Dim LOE_Frequency As Integer
    Dim LOE_FunctionId As Integer
    Dim LOE_SubFunctionId As String
    Dim LOE_UpdatedBy As Integer
    Dim LOE_STATUS As String
    Dim LOE_IPAddress As String
    Dim LOE_CompID As Integer
End Structure
'Resource
Public Structure strLOEResources
    Dim LOER_ID As Integer
    Dim LOER_LOEID As Integer
    Dim LOER_CategoryID As Integer
    Dim LOER_NoResources As Integer
    Dim LOER_ChargesPerDay As Integer
    Dim LOER_CategoryName As String
    Dim LOER_NoDays As Integer
    Dim LOER_ResTotal As Integer
    Dim LOER_Delflag As String
    Dim LOER_STATUS As String
    Dim LOER_CrBy As Integer
    Dim LOER_UpdatedBy As Integer
    Dim LOER_IPAddress As String
    Dim LOER_CompID As Integer
    Public Property iLOER_ID() As Integer
        Get
            Return (LOER_ID)
        End Get
        Set(ByVal Value As Integer)
            LOER_ID = Value
        End Set
    End Property
    Public Property iLOER_LOEID() As Integer
        Get
            Return (LOER_LOEID)
        End Get
        Set(ByVal Value As Integer)
            LOER_LOEID = Value
        End Set
    End Property
    Public Property iLOER_CategoryID() As Integer
        Get
            Return (LOER_CategoryID)
        End Get
        Set(ByVal Value As Integer)
            LOER_CategoryID = Value
        End Set
    End Property
    Public Property iLOER_NoResources() As Integer
        Get
            Return (LOER_NoResources)
        End Get
        Set(ByVal Value As Integer)
            LOER_NoResources = Value
        End Set
    End Property
    Public Property iLOER_ChargesPerDay() As Integer
        Get
            Return (LOER_ChargesPerDay)
        End Get
        Set(ByVal Value As Integer)
            LOER_ChargesPerDay = Value
        End Set
    End Property
    Public Property sLOER_CategoryName() As String
        Get
            Return (LOER_CategoryName)
        End Get
        Set(ByVal Value As String)
            LOER_CategoryName = Value
        End Set
    End Property
    Public Property iLOER_NoDays() As Integer
        Get
            Return (LOER_NoDays)
        End Get
        Set(ByVal Value As Integer)
            LOER_NoDays = Value
        End Set
    End Property
    Public Property iLOER_ResTotal() As Integer
        Get
            Return (LOER_ResTotal)
        End Get
        Set(ByVal Value As Integer)
            LOER_ResTotal = Value
        End Set
    End Property
    Public Property sLOER_Delflag() As String
        Get
            Return (LOER_Delflag)
        End Get
        Set(ByVal Value As String)
            LOER_Delflag = Value
        End Set
    End Property
    Public Property sLOER_STATUS() As String
        Get
            Return (LOER_STATUS)
        End Get
        Set(ByVal Value As String)
            LOER_STATUS = Value
        End Set
    End Property
    Public Property iLOER_CrBy() As Integer
        Get
            Return (LOER_CrBy)
        End Get
        Set(ByVal Value As Integer)
            LOER_CrBy = Value
        End Set
    End Property
    Public Property iLOER_UpdatedBy() As Integer
        Get
            Return (LOER_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            LOER_UpdatedBy = Value
        End Set
    End Property
    Public Property sLOER_IPAddress() As String
        Get
            Return (LOER_IPAddress)
        End Get
        Set(ByVal Value As String)
            LOER_IPAddress = Value
        End Set
    End Property
    Public Property iLOER_CompID() As Integer
        Get
            Return (LOER_CompID)
        End Get
        Set(ByVal Value As Integer)
            LOER_CompID = Value
        End Set
    End Property
End Structure
'AdditionalFees
Public Structure strLOEAdditionalFees
    Private LAF_ID As Integer
    Private LAF_LOEID As Integer
    Private LAF_OtherExpensesID As Integer
    Private LAF_Charges As Integer
    Private LAF_CODE As String
    Private LAF_OtherExpensesName As String
    Private LAF_Delflag As String
    Private LAF_STATUS As String
    Private LAF_CrBy As Integer
    Private LAF_UpdatedBy As Integer
    Private LAF_IPAddress As String
    Private LAF_CompID As Integer
    Public Property iLAF_ID() As Integer
        Get
            Return (LAF_ID)
        End Get
        Set(ByVal Value As Integer)
            LAF_ID = Value
        End Set
    End Property
    Public Property iLAF_LOEID() As Integer
        Get
            Return (LAF_LOEID)
        End Get
        Set(ByVal Value As Integer)
            LAF_LOEID = Value
        End Set
    End Property
    Public Property iLAF_OtherExpensesID() As Integer
        Get
            Return (LAF_OtherExpensesID)
        End Get
        Set(ByVal Value As Integer)
            LAF_OtherExpensesID = Value
        End Set
    End Property
    Public Property iLAF_Charges() As Integer
        Get
            Return (LAF_Charges)
        End Get
        Set(ByVal Value As Integer)
            LAF_Charges = Value
        End Set
    End Property
    Public Property sLAF_CODE() As String
        Get
            Return (LAF_CODE)
        End Get
        Set(ByVal Value As String)
            LAF_CODE = Value
        End Set
    End Property
    Public Property sLAF_OtherExpensesName() As String
        Get
            Return (LAF_OtherExpensesName)
        End Get
        Set(ByVal Value As String)
            LAF_OtherExpensesName = Value
        End Set
    End Property
    Public Property sLAF_Delflag() As String
        Get
            Return (LAF_Delflag)
        End Get
        Set(ByVal Value As String)
            LAF_Delflag = Value
        End Set
    End Property
    Public Property sLAF_STATUS() As String
        Get
            Return (LAF_STATUS)
        End Get
        Set(ByVal Value As String)
            LAF_STATUS = Value
        End Set
    End Property
    Public Property iLAF_CrBy() As Integer
        Get
            Return (LAF_CrBy)
        End Get
        Set(ByVal Value As Integer)
            LAF_CrBy = Value
        End Set
    End Property
    Public Property iLAF_UpdatedBy() As Integer
        Get
            Return (LAF_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            LAF_UpdatedBy = Value
        End Set
    End Property
    Public Property sLAF_IPAddress() As String
        Get
            Return (LAF_IPAddress)
        End Get
        Set(ByVal Value As String)
            LAF_IPAddress = Value
        End Set
    End Property
    Public Property iLAF_CompID() As Integer
        Get
            Return (LAF_CompID)
        End Get
        Set(ByVal Value As Integer)
            LAF_CompID = Value
        End Set
    End Property
End Structure
'ReAmbersment
Public Structure strLOEReAmbersment
    Private LAR_ID As Integer
    Private LAR_LOEID As Integer
    Private LAR_ReambersmentID As Integer
    Private LAR_Charges As Integer
    Private LAR_ReambName As String
    Private LAR_Delflag As String
    Private LAR_STATUS As String
    Private LAR_CrBy As Integer
    Private LAR_UpdatedBy As Integer
    Private LAR_IPAddress As String
    Private LAR_CompID As Integer
    Public Property iLAR_ID() As Integer
        Get
            Return (LAR_ID)
        End Get
        Set(ByVal Value As Integer)
            LAR_ID = Value
        End Set
    End Property
    Public Property iLAR_LOEID() As Integer
        Get
            Return (LAR_LOEID)
        End Get
        Set(ByVal Value As Integer)
            LAR_LOEID = Value
        End Set
    End Property
    Public Property iLAR_ReambersmentID() As Integer
        Get
            Return (LAR_ReambersmentID)
        End Get
        Set(ByVal Value As Integer)
            LAR_ReambersmentID = Value
        End Set
    End Property
    Public Property iLAR_Charges() As Integer
        Get
            Return (LAR_Charges)
        End Get
        Set(ByVal Value As Integer)
            LAR_Charges = Value
        End Set
    End Property
    Public Property sLAR_ReambName() As String
        Get
            Return (LAR_ReambName)
        End Get
        Set(ByVal Value As String)
            LAR_ReambName = Value
        End Set
    End Property
    Public Property sLAR_Delflag() As String
        Get
            Return (LAR_Delflag)
        End Get
        Set(ByVal Value As String)
            LAR_Delflag = Value
        End Set
    End Property
    Public Property sLAR_STATUS() As String
        Get
            Return (LAR_STATUS)
        End Get
        Set(ByVal Value As String)
            LAR_STATUS = Value
        End Set
    End Property
    Public Property iLAR_CrBy() As Integer
        Get
            Return (LAR_CrBy)
        End Get
        Set(ByVal Value As Integer)
            LAR_CrBy = Value
        End Set
    End Property
    Public Property iLAR_UpdatedBy() As Integer
        Get
            Return (LAR_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            LAR_UpdatedBy = Value
        End Set
    End Property
    Public Property sLAR_IPAddress() As String
        Get
            Return (LAR_IPAddress)
        End Get
        Set(ByVal Value As String)
            LAR_IPAddress = Value
        End Set
    End Property
    Public Property iLAR_CompID() As Integer
        Get
            Return (LAR_CompID)
        End Get
        Set(ByVal Value As Integer)
            LAR_CompID = Value
        End Set
    End Property
End Structure
'LOE Template
Public Structure strLOETemplate
    Dim LOET_Id As Integer
    Dim LOET_LOEID As Integer
    Dim LOET_CustomerId As Integer
    Dim LOET_FunctionId As Integer
    Dim LOET_ScopeOfWork As String
    Dim LOET_Frequency As String
    Dim LOET_Deliverable As String
    Dim LOET_ProfessionalFees As String
    Dim LOET_StdsInternalAudit As String
    Dim LOET_Responsibilities As String
    Dim LOET_Infrastructure As String
    Dim LOET_NDA As String
    Dim LOET_General As String
    Dim LOET_Delflag As String
    Dim LOET_STATUS As String
    Dim LOE_AttachID As Integer
    Dim LOET_CrBy As Integer
    Dim LOET_UpdatedBy As Integer
    Dim LOET_IPAddress As String
    Dim LOET_CompID As Integer
    Public Property iLOET_Id() As Integer
        Get
            Return (LOET_Id)
        End Get
        Set(ByVal Value As Integer)
            LOET_Id = Value
        End Set
    End Property
    Public Property iLOET_LOEID() As Integer
        Get
            Return (LOET_LOEID)
        End Get
        Set(ByVal Value As Integer)
            LOET_LOEID = Value
        End Set
    End Property
    Public Property iLOET_CustomerId() As Integer
        Get
            Return (LOET_CustomerId)
        End Get
        Set(ByVal Value As Integer)
            LOET_CustomerId = Value
        End Set
    End Property
    Public Property iLOET_FunctionId() As Integer
        Get
            Return (LOET_FunctionId)
        End Get
        Set(ByVal Value As Integer)
            LOET_FunctionId = Value
        End Set
    End Property
    Public Property sLOET_ScopeOfWork() As String
        Get
            Return (LOET_ScopeOfWork)
        End Get
        Set(ByVal Value As String)
            LOET_ScopeOfWork = Value
        End Set
    End Property
    Public Property sLOET_Frequency() As String
        Get
            Return (LOET_Frequency)
        End Get
        Set(ByVal Value As String)
            LOET_Frequency = Value
        End Set
    End Property
    Public Property sLOET_Deliverable() As String
        Get
            Return (LOET_Deliverable)
        End Get
        Set(ByVal Value As String)
            LOET_Deliverable = Value
        End Set
    End Property
    Public Property sLOET_ProfessionalFees() As String
        Get
            Return (LOET_ProfessionalFees)
        End Get
        Set(ByVal Value As String)
            LOET_ProfessionalFees = Value
        End Set
    End Property
    Public Property sLOET_StdsInternalAudit() As String
        Get
            Return (LOET_StdsInternalAudit)
        End Get
        Set(ByVal Value As String)
            LOET_StdsInternalAudit = Value
        End Set
    End Property
    Public Property sLOET_Responsibilities() As String
        Get
            Return (LOET_Responsibilities)
        End Get
        Set(ByVal Value As String)
            LOET_Responsibilities = Value
        End Set
    End Property
    Public Property sLOET_Infrastructure() As String
        Get
            Return (LOET_Infrastructure)
        End Get
        Set(ByVal Value As String)
            LOET_Infrastructure = Value
        End Set
    End Property
    Public Property sLOET_NDA() As String
        Get
            Return (LOET_NDA)
        End Get
        Set(ByVal Value As String)
            LOET_NDA = Value
        End Set
    End Property
    Public Property sLOET_General() As String
        Get
            Return (LOET_General)
        End Get
        Set(ByVal Value As String)
            LOET_General = Value
        End Set
    End Property
    Public Property sLOET_Delflag() As String
        Get
            Return (LOET_Delflag)
        End Get
        Set(ByVal Value As String)
            LOET_Delflag = Value
        End Set
    End Property
    Public Property sLOET_STATUS() As String
        Get
            Return (LOET_STATUS)
        End Get
        Set(ByVal Value As String)
            LOET_STATUS = Value
        End Set
    End Property
    Public Property iLOE_AttachID() As Integer
        Get
            Return (LOE_AttachID)
        End Get
        Set(ByVal Value As Integer)
            LOE_AttachID = Value
        End Set
    End Property
    Public Property iLOET_CrBy() As Integer
        Get
            Return (LOET_CrBy)
        End Get
        Set(ByVal Value As Integer)
            LOET_CrBy = Value
        End Set
    End Property
    Public Property iLOET_UpdatedBy() As Integer
        Get
            Return (LOET_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            LOET_UpdatedBy = Value
        End Set
    End Property
    Public Property sLOET_IPAddress() As String
        Get
            Return (LOET_IPAddress)
        End Get
        Set(ByVal Value As String)
            LOET_IPAddress = Value
        End Set
    End Property
    Public Property iLOET_CompID() As Integer
        Get
            Return (LOET_CompID)
        End Get
        Set(ByVal Value As Integer)
            LOET_CompID = Value
        End Set
    End Property

End Structure
Public Structure strCompliance
    Dim Comp_Id As Integer
    Dim Comp_CustID As Integer
    Dim Comp_Task As Integer
    Dim Comp_Frequency As Integer
    Dim Comp_LoginName As String
    Dim Comp_Password As String
    Dim Comp_Email As String
    Dim Comp_MobileNo As String
    Dim Comp_Accountdetails As Integer
    Dim Comp_AadhaarAuthen As String
    Dim Comp_GSTIN As String
    Dim Comp_Remarks As String
    Dim Comp_CRON As DateTime
    Dim Comp_CRBY As Integer
    Dim Comp_UpdatedOn As DateTime
    Dim Comp_UpdatedBy As Integer
    Dim Comp_DelFlag As String
    Dim Comp_STATUS As String
    Dim Comp_IPAddress As String
    Dim Comp_CompID As Integer
    Public Property iComp_Id() As Integer
        Get
            Return (Comp_Id)
        End Get
        Set(ByVal Value As Integer)
            Comp_Id = Value
        End Set
    End Property
    Public Property iComp_CustID() As Integer
        Get
            Return (Comp_CustID)
        End Get
        Set(ByVal Value As Integer)
            Comp_CustID = Value
        End Set
    End Property
    Public Property iComp_Task() As Integer
        Get
            Return (Comp_Task)
        End Get
        Set(ByVal Value As Integer)
            Comp_Task = Value
        End Set
    End Property
    Public Property iComp_Frequency() As Integer
        Get
            Return (Comp_Frequency)
        End Get
        Set(ByVal Value As Integer)
            Comp_Frequency = Value
        End Set
    End Property
    Public Property sComp_LoginName() As String
        Get
            Return (Comp_LoginName)
        End Get
        Set(ByVal Value As String)
            Comp_LoginName = Value
        End Set
    End Property
    Public Property sComp_Password() As String
        Get
            Return (Comp_Password)
        End Get
        Set(ByVal Value As String)
            Comp_Password = Value
        End Set
    End Property
    Public Property sComp_Email() As String
        Get
            Return (Comp_Email)
        End Get
        Set(ByVal Value As String)
            Comp_Email = Value
        End Set
    End Property
    Public Property sComp_MobileNo() As String
        Get
            Return (Comp_MobileNo)
        End Get
        Set(ByVal Value As String)
            Comp_MobileNo = Value
        End Set
    End Property
    Public Property iComp_Accountdetails() As Integer
        Get
            Return (Comp_Accountdetails)
        End Get
        Set(ByVal Value As Integer)
            Comp_Accountdetails = Value
        End Set
    End Property
    Public Property sComp_AadhaarAuthen() As String
        Get
            Return (Comp_AadhaarAuthen)
        End Get
        Set(ByVal Value As String)
            Comp_AadhaarAuthen = Value
        End Set
    End Property
    Public Property sComp_GSTIN() As String
        Get
            Return (Comp_GSTIN)
        End Get
        Set(ByVal Value As String)
            Comp_GSTIN = Value
        End Set
    End Property
    Public Property sComp_Remarks() As String
        Get
            Return (Comp_Remarks)
        End Get
        Set(ByVal Value As String)
            Comp_Remarks = Value
        End Set
    End Property
    Public Property dComp_CRON() As Date
        Get
            Return (Comp_CRON)
        End Get
        Set(ByVal Value As Date)
            Comp_CRON = Value
        End Set
    End Property
    Public Property iComp_CRBY() As Integer
        Get
            Return (Comp_CRBY)
        End Get
        Set(ByVal Value As Integer)
            Comp_CRBY = Value
        End Set
    End Property
    Public Property dComp_UpdatedOn() As DateTime
        Get
            Return (Comp_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            Comp_UpdatedOn = Value
        End Set
    End Property
    Public Property iComp_UpdatedBy() As Integer
        Get
            Return (Comp_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Comp_UpdatedBy = Value
        End Set
    End Property
    Public Property sComp_DelFlag() As String
        Get
            Return (Comp_DelFlag)
        End Get
        Set(ByVal Value As String)
            Comp_DelFlag = Value
        End Set
    End Property
    Public Property sComp_STATUS() As String
        Get
            Return (Comp_STATUS)
        End Get
        Set(ByVal Value As String)
            Comp_STATUS = Value
        End Set
    End Property
    Public Property sComp_IPAddress() As String
        Get
            Return (Comp_IPAddress)
        End Get
        Set(ByVal Value As String)
            Comp_IPAddress = Value
        End Set
    End Property
    Public Property iComp_CompID() As String
        Get
            Return (Comp_CompID)
        End Get
        Set(ByVal Value As String)
            Comp_CompID = Value
        End Set
    End Property
End Structure

Public Structure strStatutoryDirector
    Dim SSD_Id As Integer
    Dim SSD_CustID As Integer
    Dim SSD_DirectorName As String
    Dim SSD_DOB As DateTime
    Dim SSD_DIN As String
    Dim SSD_MobileNo As String
    Dim SSD_Email As String
    Dim SSD_Remarks As String
    Dim SSD_CRON As DateTime
    Dim SSD_CRBY As Integer
    Dim SSD_UpdatedOn As DateTime
    Dim SSD_UpdatedBy As Integer
    Dim SSD_DelFlag As String
    Dim SSD_STATUS As String
    Dim SSD_IPAddress As String
    Dim SSD_CompID As Integer
    Public Property iSSD_Id() As Integer
        Get
            Return (SSD_Id)
        End Get
        Set(ByVal Value As Integer)
            SSD_Id = Value
        End Set
    End Property
    Public Property iSSD_CustID() As Integer
        Get
            Return (SSD_CustID)
        End Get
        Set(ByVal Value As Integer)
            SSD_CustID = Value
        End Set
    End Property
    Public Property sSSD_DirectorName() As String
        Get
            Return (SSD_DirectorName)
        End Get
        Set(ByVal Value As String)
            SSD_DirectorName = Value
        End Set
    End Property
    Public Property dSSD_DOB() As DateTime
        Get
            Return (SSD_DOB)
        End Get
        Set(ByVal Value As DateTime)
            SSD_DOB = Value
        End Set
    End Property
    Public Property sSSD_DIN() As String
        Get
            Return (SSD_DIN)
        End Get
        Set(ByVal Value As String)
            SSD_DIN = Value
        End Set
    End Property
    Public Property sSSD_MobileNo() As String
        Get
            Return (SSD_MobileNo)
        End Get
        Set(ByVal Value As String)
            SSD_MobileNo = Value
        End Set
    End Property
    Public Property sSSD_Email() As String
        Get
            Return (SSD_Email)
        End Get
        Set(ByVal Value As String)
            SSD_Email = Value
        End Set
    End Property
    Public Property sSSD_Remarks() As String
        Get
            Return (SSD_Remarks)
        End Get
        Set(ByVal Value As String)
            SSD_Remarks = Value
        End Set
    End Property
    Public Property dSSD_CRON() As Date
        Get
            Return (SSD_CRON)
        End Get
        Set(ByVal Value As Date)
            SSD_CRON = Value
        End Set
    End Property
    Public Property iSSD_CRBY() As Integer
        Get
            Return (SSD_CRBY)
        End Get
        Set(ByVal Value As Integer)
            SSD_CRBY = Value
        End Set
    End Property
    Public Property dSSD_UpdatedOn() As DateTime
        Get
            Return (SSD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            SSD_UpdatedOn = Value
        End Set
    End Property
    Public Property iSSD_UpdatedBy() As Integer
        Get
            Return (SSD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SSD_UpdatedBy = Value
        End Set
    End Property
    Public Property sSSD_DelFlag() As String
        Get
            Return (SSD_DelFlag)
        End Get
        Set(ByVal Value As String)
            SSD_DelFlag = Value
        End Set
    End Property
    Public Property sSSD_STATUS() As String
        Get
            Return (SSD_STATUS)
        End Get
        Set(ByVal Value As String)
            SSD_STATUS = Value
        End Set
    End Property
    Public Property sSSD_IPAddress() As String
        Get
            Return (SSD_IPAddress)
        End Get
        Set(ByVal Value As String)
            SSD_IPAddress = Value
        End Set
    End Property
    Public Property iSSD_CompID() As String
        Get
            Return (SSD_CompID)
        End Get
        Set(ByVal Value As String)
            SSD_CompID = Value
        End Set
    End Property
End Structure
Public Structure strStatutoryPartner
    Dim SSP_Id As Integer
    Dim SSP_CustID As Integer
    Dim SSP_PartnerName As String
    Dim SSP_DOJ As DateTime
    Dim SSP_PAN As String
    Dim SSP_ShareOfProfit As Decimal
    Dim SSP_CapitalAmount As Decimal
    Dim SSP_CRON As DateTime
    Dim SSP_CRBY As Integer
    Dim SSP_UpdatedOn As DateTime
    Dim SSP_UpdatedBy As Integer
    Dim SSP_DelFlag As String
    Dim SSP_STATUS As String
    Dim SSP_IPAddress As String
    Dim SSP_CompID As Integer
    Public Property iSSP_Id() As Integer
        Get
            Return (SSP_Id)
        End Get
        Set(ByVal Value As Integer)
            SSP_Id = Value
        End Set
    End Property
    Public Property iSSP_CustID() As Integer
        Get
            Return (SSP_CustID)
        End Get
        Set(ByVal Value As Integer)
            SSP_CustID = Value
        End Set
    End Property
    Public Property sSSP_PartnerName() As String
        Get
            Return (SSP_PartnerName)
        End Get
        Set(ByVal Value As String)
            SSP_PartnerName = Value
        End Set
    End Property
    Public Property dSSP_DOJ() As DateTime
        Get
            Return (SSP_DOJ)
        End Get
        Set(ByVal Value As DateTime)
            SSP_DOJ = Value
        End Set
    End Property
    Public Property sSSP_PAN() As String
        Get
            Return (SSP_PAN)
        End Get
        Set(ByVal Value As String)
            SSP_PAN = Value
        End Set
    End Property
    Public Property dSSP_ShareOfProfit() As Decimal
        Get
            Return (SSP_ShareOfProfit)
        End Get
        Set(ByVal Value As Decimal)
            SSP_ShareOfProfit = Value
        End Set
    End Property
    Public Property dSSP_CapitalAmount() As Decimal
        Get
            Return (SSP_CapitalAmount)
        End Get
        Set(ByVal Value As Decimal)
            SSP_CapitalAmount = Value
        End Set
    End Property
    Public Property dSSP_CRON() As Date
        Get
            Return (SSP_CRON)
        End Get
        Set(ByVal Value As Date)
            SSP_CRON = Value
        End Set
    End Property
    Public Property iSSP_CRBY() As Integer
        Get
            Return (SSP_CRBY)
        End Get
        Set(ByVal Value As Integer)
            SSP_CRBY = Value
        End Set
    End Property
    Public Property dSSP_UpdatedOn() As DateTime
        Get
            Return (SSP_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            SSP_UpdatedOn = Value
        End Set
    End Property
    Public Property iSSP_UpdatedBy() As Integer
        Get
            Return (SSP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SSP_UpdatedBy = Value
        End Set
    End Property
    Public Property sSSP_DelFlag() As String
        Get
            Return (SSP_DelFlag)
        End Get
        Set(ByVal Value As String)
            SSP_DelFlag = Value
        End Set
    End Property
    Public Property sSSP_STATUS() As String
        Get
            Return (SSP_STATUS)
        End Get
        Set(ByVal Value As String)
            SSP_STATUS = Value
        End Set
    End Property
    Public Property sSSP_IPAddress() As String
        Get
            Return (SSP_IPAddress)
        End Get
        Set(ByVal Value As String)
            SSP_IPAddress = Value
        End Set
    End Property
    Public Property iSSP_CompID() As String
        Get
            Return (SSP_CompID)
        End Get
        Set(ByVal Value As String)
            SSP_CompID = Value
        End Set
    End Property
End Structure
Public Class clsCustDetails
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetLatestCustomerCode(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim iMaxID As Integer
        Dim sMaxID As String = "", sCustCode As String = ""
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from SAD_CUSTOMER_MASTER where cust_Compid=" & iACID & "")
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
            sCustCode = "CUST" & sMaxID
            Return sCustCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where cust_Compid=" & iACID & " order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomersDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select * from SAD_CUSTOMER_DETAILS Where CDET_CUSTID=" & iCustID & " And CDET_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from SAD_CUSTOMER_MASTER Where CUST_ID=" & iCustId & " and cust_Compid=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranch(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Branch_Id,Branch_NAme from SAD_General_BranchDetails"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer) As String
        Dim sflag As String = ""
        Dim sSql As String
        Try
            sSql = "Select CUST_DELFLG from SAD_CUSTOMER_MASTER where CUST_ID=" & iCustId & "  and cust_Compid=" & iACID & ""
            sflag = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sflag
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerCompDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iCustCompPKId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim sCity As String = "", sScope As String = ""
        Try
            dt.Columns.Add("CompPkID")
            dt.Columns.Add("Act")
            dt.Columns.Add("ServiceTaskId")
            dt.Columns.Add("ServiceTask")
            dt.Columns.Add("FrequencyId")
            dt.Columns.Add("Frequency")
            dt.Columns.Add("LoginName")
            dt.Columns.Add("Password")
            dt.Columns.Add("Email")
            dt.Columns.Add("MobileNo")
            dt.Columns.Add("AccountDetailID")
            dt.Columns.Add("AccountDetailYesNo")
            dt.Columns.Add("AadhaarAuthentication")
            dt.Columns.Add("RegNo")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Status")

            sSql = "Select Comp_Id,Comp_Task,CMM_Act,cmm_Desc As ServiceTask,Comp_LoginName,Comp_Password,Comp_Email,Comp_MobileNo,Comp_Accountdetails,Comp_AadhaarAuthen,Comp_GSTIN,Comp_Remarks,Comp_Frequency,Comp_DelFlag,"
            sSql = sSql & " Case When Comp_Frequency=0 then '' When Comp_Frequency=1 then 'Yearly' when Comp_Frequency=2 then 'Monthly' when Comp_Frequency=3 then 'Once' when Comp_Frequency=4 then 'Quarterly' End As Frequency"
            sSql = sSql & " from SAD_Compliance_Details Join Content_Management_Master On cmm_ID=Comp_Task where Comp_CustID=" & iCustId & " and Comp_CompID=" & iACID & ""
            If iCustCompPKId > 0 Then
                sSql = sSql & " And Comp_Id=" & iCustCompPKId & ""
            End If
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows Then
                While dr.Read()
                    dRow = dt.NewRow()
                    If IsDBNull(dr("Comp_Id")) = False Then
                        dRow("CompPkID") = dr("Comp_Id")
                    End If
                    If IsDBNull(dr("CMM_Act")) = False Then
                        dRow("Act") = dr("CMM_Act")
                    End If
                    If IsDBNull(dr("Comp_Task")) = False Then
                        dRow("ServiceTaskId") = dr("Comp_Task")
                    End If
                    If IsDBNull(dr("ServiceTask")) = False Then
                        dRow("ServiceTask") = dr("ServiceTask")
                    End If
                    If IsDBNull(dr("Comp_Frequency")) = False Then
                        dRow("FrequencyId") = dr("Comp_Frequency")
                    End If
                    If IsDBNull(dr("Frequency")) = False Then
                        dRow("Frequency") = dr("Frequency")
                    End If
                    If IsDBNull(dr("Comp_LoginName")) = False Then
                        dRow("LoginName") = dr("Comp_LoginName")
                    End If
                    If IsDBNull(dr("Comp_Password")) = False Then
                        dRow("Password") = dr("Comp_Password")
                    End If
                    If IsDBNull(dr("Comp_Email")) = False Then
                        dRow("Email") = dr("Comp_Email")
                    End If
                    If IsDBNull(dr("Comp_MobileNo")) = False Then
                        If dr("Comp_MobileNo") <> 0 Then
                            dRow("MobileNo") = dr("Comp_MobileNo")
                        Else
                            dRow("MobileNo") = ""
                        End If
                    End If
                    If IsDBNull(dr("Comp_Accountdetails")) = False Then
                        dRow("AccountDetailID") = dr("Comp_Accountdetails")
                        If dr("Comp_Accountdetails") = 1 Then
                            dRow("AccountDetailYesNo") = "Yes"
                        Else
                            dRow("AccountDetailYesNo") = "No"
                        End If
                    End If
                    If IsDBNull(dr("Comp_AadhaarAuthen")) = False Then
                        dRow("AadhaarAuthentication") = dr("Comp_AadhaarAuthen")
                    End If
                    If IsDBNull(dr("Comp_GSTIN")) = False Then
                        dRow("RegNo") = dr("Comp_GSTIN")
                    End If
                    If IsDBNull(dr("Comp_Remarks")) = False Then
                        dRow("Remarks") = dr("Comp_Remarks")
                    End If
                    dRow("Status") = dr("Comp_DelFlag")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub CustComplianceApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iCustID As Integer, ByVal iCustCompPKID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update SAD_Compliance_Details set"
            If sType = "DeActivated" Then
                sSql = sSql & " Comp_DelFlag='D',Comp_UpdatedBy=" & iUsrID & ", Comp_UpdatedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " Comp_DelFlag='A',Comp_UpdatedBy=" & iUsrID & ", Comp_UpdatedOn=Getdate(),"
            End If
            sSql = sSql & "Comp_IPAddress='" & sIPAddress & "' Where Comp_Id=" & iCustCompPKID & " And Comp_CustID=" & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckCustComplianceTask(ByVal sAC As String, ByVal iACID As Integer, ByVal iCompPKId As Integer, ByVal iCustId As Integer, ByVal iTaskId As Integer, ByVal iFrequencyId As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Comp_Id from SAD_Compliance_Details where Comp_Task=" & iTaskId & " And Comp_Frequency=" & iFrequencyId & " and Comp_CustID=" & iCustId & " and Comp_CompID=" & iACID & ""
            If iCompPKId > 0 Then
                sSql = sSql & " And Comp_Id<>" & iCompPKId & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCount(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select count(CUST_ID) From SAD_CUSTOMER_MASTER Where CUST_CompID=" & iACID & " and CUST_DELFLG<>'D'"
            GetCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return GetCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateSatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer) As String
        Dim sflag As String = ""
        Dim sSql As String
        Try
            sSql = "UPDATE SAD_CUSTOMER_MASTER SET CUST_DELFLG = 'A' WHERE CUST_ID =" & iCustId & " and CUST_CompID =" & iACID & ""
            sflag = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sflag
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveCustomerMaster(ByVal sAC As String, ByVal objsCust As strCustMaster) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(40) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_ID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_ID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_NAME", OleDb.OleDbType.VarChar, 150)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_NAME
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_CODE", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_CODE
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_WEBSITE", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_WEBSITE
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_EMAIL", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_EMAIL
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_GROUPNAME", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_GROUPNAME
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_GROUPINDIVIDUAL", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_GROUPINDIVIDUAL
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_ORGTYPEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_ORGTYPEID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_INDTYPEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_INDTYPEID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_MGMTTYPEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_MGMTTYPEID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_CommitmentDate", OleDb.OleDbType.Date, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_CommitmentDate
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUSt_BranchId", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCust.CUSt_BranchId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_ADDRESS", OleDb.OleDbType.VarChar, 1000)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_ADDRESS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_CITY", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_CITY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_PIN", OleDb.OleDbType.VarChar, 10)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_PIN
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_STATE", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_STATE
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_COUNTRY", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_COUNTRY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_FAX", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_FAX
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_TEL", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_TEL
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COMM_Email", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COMM_Email
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_ADDRESS", OleDb.OleDbType.VarChar, 1000)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_ADDRESS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_CITY", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_CITY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_PIN", OleDb.OleDbType.VarChar, 10)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_PIN
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_STATE", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_STATE
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_COUNTRY", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_COUNTRY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_FAX", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_FAX
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_TELPHONE", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_TELPHONE
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_ConEmailID", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_ConEmailID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_LOCATIONID", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = 0
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_TASKS", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_TASKS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_ORGID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_ORGID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_BOARDOFDIRECTORS", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_BOARDOFDIRECTORS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_DEPMETHOD", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = 0
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_Amount_Type", OleDb.OleDbType.Integer, 10)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_Amount_Type
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CUST_RoundOff", OleDb.OleDbType.Decimal, 18)
            ObjSFParam(iARAParamCount).Value = objsCust.CUST_RoundOff
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_CUSTOMER_MASTER", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindStatutoryRef(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iLocationId As Integer)
        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = "Select Cust_Desc As StatutoryName,Cust_Value As StatutoryValue,Cust_PKID,Cust_AttchID From SAD_CUST_Accounting_Template Where Cust_ID=" & iCustID & " and Cust_LocationId=" & iLocationId & " and Cust_Status='A' and Cust_Compid =" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCustomerDetails(ByVal sAC As String, ByVal objsCustDetails As strCustDetails) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_ID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_ID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_CUSTID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_CUSTID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_STANDINGININDUSTRY", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_STANDINGININDUSTRY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_PUBLICPERCEPTION", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_PUBLICPERCEPTION
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_GOVTPERCEPTION", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_GOVTPERCEPTION
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_LITIGATIONISSUES", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_LITIGATIONISSUES
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_PRODUCTSMANUFACTURED", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_PRODUCTSMANUFACTURED
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_SERVICESOFFERED", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_SERVICESOFFERED
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_TURNOVER", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_TURNOVER
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_PROFITABILITY", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_PROFITABILITY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_FOREIGNCOLLABORATIONS", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_FOREIGNCOLLABORATIONS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_EMPLOYEESTRENGTH", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_EMPLOYEESTRENGTH
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_PROFESSIONALSERVICES", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_PROFESSIONALSERVICES
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_GATHEREDBYAUDITFIRM", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_GATHEREDBYAUDITFIRM
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_LEGALADVISORS", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_LEGALADVISORS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_AUDITINCHARGE", OleDb.OleDbType.VarChar, 255)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_AUDITINCHARGE
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_FileNo", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_FileNo
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@CDET_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustDetails.CDET_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_CUSTOMER_DETAILS", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Sub SaveStatutoryRef(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal sStatutoryName As String, ByVal sStatutoryValue As String, ByVal sStatus As String, ByVal iAttachID As Integer)
    '    Dim sSql As String
    '    Dim iId As Integer
    '    Try
    '        sSql = "Select ISNULL(max(Cust_PKID),0)+1 from SAD_CUST_Accounting_Template"
    '        iId = objDBL.SQLExecuteScalarInt(sAC, sSql)

    '        sSql = "Insert Into SAD_CUST_Accounting_Template (Cust_PKID,Cust_ID,Cust_Desc,Cust_Value,Cust_Status,Cust_Compid,Cust_AttchID) values"
    '        sSql = sSql & "(" & iId & "," & iCustID & ",'" & objclsGRACeGeneral.SafeSQL(sStatutoryName) & "','" & objclsGRACeGeneral.SafeSQL(sStatutoryValue) & "','" & sStatus & "'," & iACID & "," & iAttachID & ")"
    '        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Function SaveStatutoryRef(ByVal sAC As String, ByVal objstrCUSTAccountingTemplate As strCUSTAccountingTemplate) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.iCust_PKID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_ID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.iCust_ID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_Desc", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.sCust_Desc
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_Value", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.sCust_Value
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_Delflag", OleDb.OleDbType.VarChar, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.sCust_Delflag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_Status", OleDb.OleDbType.VarChar, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.sCust_Status
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_AttchID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.iCust_AttchID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.iCust_CrBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.iCust_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.sCust_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_Compid", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.iCust_Compid
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Cust_LocationId", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrCUSTAccountingTemplate.iCust_LocationId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_CUST_Accounting_Template", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckStatutory(ByVal sAC As String, ByVal iACID As Integer, ByVal sStatutory As String, ByVal iCustID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "select Cust_Value from SAD_CUST_Accounting_Template where Cust_Desc='" & sStatutory & "' and Cust_ID=" & iCustID & " and Cust_Compid =" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStatutory(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String
        Dim desc As String = ""
        Try
            sSql = "select Cust_Value from SAD_CUST_Accounting_Template where  Cust_ID=" & iCustID & " and Cust_Compid =" & iACID & ""
            desc = objDBL.SQLGetDescription(sAC, sSql)
            Return desc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGSTINPAN(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal sKey As String) As String
        Dim sSql As String
        Dim desc As String = ""
        Try
            sSql = "Select Cust_Value from SAD_CUST_Accounting_Template where Cust_Desc='" & sKey & "' And Cust_ID=" & iCustID & " and Cust_Compid =" & iACID & ""
            desc = objDBL.SQLGetDescription(sAC, sSql)
            Return desc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckPANWithOtherCust(ByVal sAC As String, ByVal iACID As Integer, ByVal sValue As String, ByVal iCustID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Cust_ID from SAD_CUST_Accounting_Template where Cust_Desc='PAN' And Cust_Value='" & sValue & "' and Cust_ID<>" & iCustID & " and Cust_Compid =" & iACID & ""
            Return objDBL.DBCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteStatutoryRef(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iPkid As Integer)
        Dim sSql As String
        Try
            If iPkid = 0 Then
                sSql = "Delete From SAD_CUST_Accounting_Template Where Cust_ID=" & iCustID & ""
            Else
                sSql = "Delete From SAD_CUST_Accounting_Template Where Cust_PKID=" & iPkid & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCustLocation(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mas_Id,Mas_Description from SAD_CUST_LOCATION Where Mas_CustID=" & iCustID & " and Mas_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLocationDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iLocId As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from SAD_CUST_LOCATION Where Mas_Id=" & iLocId & " and Mas_CustID=" & iCustID & " and Mas_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCustLocation(ByVal sAC As String, ByVal iACID As Integer, ByVal sLocationName As String, ByVal iCustId As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "select Mas_Id from SAD_CUST_LOCATION where Mas_CustID=" & iCustId & " And Mas_Description='" & sLocationName & "' and Mas_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCustomerLocation(ByVal sAC As String, ByVal objsCustLocation As strCustLocation) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iARAParamCount As Integer
        Dim LocArr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_Id", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_Id
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_code", OleDb.OleDbType.VarChar, 10)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_code
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_Description", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_Description
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_DelFlag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_CustID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_Loc_Address", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_Loc_Address
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_Contact_Person", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_Contact_Person
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_Contact_MobileNo", OleDb.OleDbType.VarChar, 15)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_Contact_MobileNo
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_Contact_LandLineNo", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_Contact_LandLineNo
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_Contact_Email", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_Contact_Email
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@mas_Designation", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.mas_Designation
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Mas_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCustLocation.Mas_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            LocArr(0) = "@iUpdateOrSave"
            LocArr(1) = "@iOper"

            LocArr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_CUST_LOCATION", 1, LocArr, ObjSFParam)
            Return LocArr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCustomerComplaince(ByVal sAC As String, ByVal objsCompliance As strCompliance) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_Id", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_Id
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_CustID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_Task", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_Task
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_Frequency", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_Frequency
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_LoginName", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_LoginName
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_Password", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_Password
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_Email", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_Email
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_MobileNo", OleDb.OleDbType.VarChar, 15)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_MobileNo
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_Accountdetails", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_Accountdetails
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_AadhaarAuthen", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_AadhaarAuthen
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_GSTIN", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_GSTIN
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_Remarks", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_Remarks
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_CRON", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_CRON
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_UpdatedOn", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_UpdatedOn
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_DelFlag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@Comp_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsCompliance.Comp_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_Compliance_Details", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLocIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mas_Id from SAD_CUST_LOCATION Where Mas_CustID=" & iCustID & " and Mas_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCustMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal sLocatioID As String)
        Dim sSql As String
        Try
            sSql = "Update SAD_CUSTOMER_MASTER Set Cust_LocationID='" & sLocatioID & "' Where Cust_ID=" & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadLOE(ByVal sAC As String, ByVal iACID As Integer, ByVal icustId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select LOE_ID, LOE_Name From SAD_CUST_LOE Where LOE_CustomerId=" & icustId & " and LOE_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DiplayLOE(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iLOEId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select LOE_Id,LOE_YearId,LOE_CustomerId,LOE_ServiceTypeId,LOE_NatureOfService,LOE_LocationIds,LOE_Frequency,LOE_FunctionId,LOE_SubFunctionId,LOE_Milestones,"
            sSql = sSql & " (case Convert(Varchar(10),LOE_TimeSchedule,103) when '01/01/1900' then '' else Convert(Varchar(10),LOE_TimeSchedule,103)end)LOE_TimeSchedule,(case Convert(Varchar(10),"
            sSql = sSql & " LOE_ReportDueDate,103) when '01/01/1900' then '' else Convert(Varchar(10),LOE_ReportDueDate,103)end)LOE_ReportDueDate,"
            sSql = sSql & " LOE_ProfessionalFees,LOE_OtherFees,LOE_ServiceTax,LOE_RembFilingFee,LOE_CrBy,Convert(Varchar(10),LOE_CrOn,103)LOE_CrOn,LOE_Total,LOE_Name From SAD_CUST_LOE"
            sSql = sSql & " Where LOE_Id=" & iLOEId & " and LOE_YearId=" & iYearID & " and LOE_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LodeLOEDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select LOE_Id,LOE_YearId,LOE_Id,LOE_CustomerId,LOE_ServiceTypeId,LOE_NatureOfService,LOE_LocationIds,LOE_Frequency,LOE_FunctionId,LOE_SubFunctionId,LOE_Milestones,"
            sSql = sSql & " (case Convert(Varchar(10),LOE_TimeSchedule,103) when '01/01/1900' then '' else Convert(Varchar(10),LOE_TimeSchedule,103)end)LOE_TimeSchedule,(case Convert(Varchar(10),"
            sSql = sSql & " LOE_ReportDueDate,103) when '01/01/1900' then '' else Convert(Varchar(10),LOE_ReportDueDate,103)end)LOE_ReportDueDate,"
            sSql = sSql & " LOE_ProfessionalFees,LOE_OtherFees,LOE_ServiceTax,LOE_RembFilingFee,LOE_CrBy,Convert(Varchar(10),LOE_CrOn,103)LOE_CrOn,LOE_Total,LOE_Name From SAD_CUST_LOE"
            sSql = sSql & " Where LOE_Id=" & iCustID & " and LOE_YearId=" & iYearID & " and LOE_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCustomerLOE(ByVal sAC As String, ByVal objstrLOE As strLOE) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(24) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_Id", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_Id
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_YearId", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_YearId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_CustomerId", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_CustomerId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_ServiceTypeId", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_ServiceTypeId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_NatureOfService", OleDb.OleDbType.VarChar, 200)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_NatureOfService
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_LocationIds", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_LocationIds
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_Milestones", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_Milestones
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_TimeSchedule", OleDb.OleDbType.Date, 8)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_TimeSchedule
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_ReportDueDate", OleDb.OleDbType.Date, 8)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_ReportDueDate
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_ProfessionalFees", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_ProfessionalFees
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_OtherFees", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_OtherFees
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_ServiceTax", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_ServiceTax
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_RembFilingFee", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_RembFilingFee
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_CrBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_Total", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_Total
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_Name", OleDb.OleDbType.VarChar, 200)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_Name
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_Frequency", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_Frequency
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_FunctionId", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_FunctionId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_SubFunctionId", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_SubFunctionId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOE_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOE.LOE_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_CUST_LOE", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCategoryDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iLOEID As Integer) As DataTable
        Dim dtRes As New DataTable : Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Try
            dtRes.Columns.Add("Id")
            dtRes.Columns.Add("Cat")
            dtRes.Columns.Add("Res")
            dtRes.Columns.Add("days")
            dtRes.Columns.Add("Charge")
            dtRes.Columns.Add("Total")
            sSql = "select * from LOE_Resources where LOER_LOEID=" & iLOEID & " and LOER_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtRes.NewRow
                    dRow("Id") = dt.Rows(i)("LOER_ID").ToString
                    dRow("Cat") = dt.Rows(i)("LOER_CategoryName").ToString
                    dRow("Res") = dt.Rows(i)("LOER_NoResources").ToString
                    dRow("days") = dt.Rows(i)("LOER_NoDays").ToString
                    dRow("Charge") = dt.Rows(i)("LOER_ChargesPerDay").ToString
                    dRow("Total") = dt.Rows(i)("LOER_ResTotal").ToString
                    dtRes.Rows.Add(dRow)
                Next
            End If
            Return dtRes
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLOEDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iExistingLOEId As Integer, ByVal iYearID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Try
            dtTab.Columns.Add("LOE_Id")
            dtTab.Columns.Add("LOE_ServiceTypeId")
            dtTab.Columns.Add("LOE_TimeSchedule")
            dtTab.Columns.Add("LOE_ReportDueDate")
            dtTab.Columns.Add("LOE_Total")
            dtTab.Columns.Add("LOE_Frequency")
            sSql = "Select LOE_Id,LOE_CustomerId,LOE_ServiceTypeId,LOE_TimeSchedule,LOE_ReportDueDate,LOE_Total,LOE_Frequency,a.CMM_Desc as ServiceType,b.CMM_Desc as Frequency From SAD_CUST_LOE "
            sSql = sSql & " Left Join Content_Management_Master a On a.CMM_ID=LOE_ServiceTypeId "
            sSql = sSql & " Left Join Content_Management_Master b On b.CMM_ID=LOE_Frequency "
            sSql = sSql & " Where  LOE_YearId=" & iYearID & ""
            If iCustID > 0 Then
                sSql = sSql & " And LOE_CustomerId=" & iCustID & ""
            End If
            If iExistingLOEId > 0 Then
                sSql = sSql & " And LOE_Id=" & iExistingLOEId & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("LOE_Id") = dt.Rows(i)("LOE_Id")
                    dRow("LOE_ServiceTypeId") = dt.Rows(i)("ServiceType").ToString
                    dRow("LOE_TimeSchedule") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("LOE_TimeSchedule").ToString, "D")
                    dRow("LOE_ReportDueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("LOE_ReportDueDate").ToString, "D")
                    dRow("LOE_Total") = dt.Rows(i)("LOE_Total").ToString
                    dRow("LOE_Frequency") = dt.Rows(i)("Frequency").ToString
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCategoryCodeDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iLOEID As Integer) As DataTable
        Dim dtRes As New DataTable : Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Try
            dtRes.Columns.Add("Id")
            dtRes.Columns.Add("Category")
            dtRes.Columns.Add("CatCode")
            dtRes.Columns.Add("CatRes")
            sSql = "Select * from LOE_AdditionalFees where LAF_LOEID=" & iLOEID & " And LAF_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtRes.NewRow
                    dRow("Id") = dt.Rows(i)("LAF_ID").ToString
                    dRow("Category") = dt.Rows(i)("LAF_OtherExpensesName").ToString
                    dRow("CatCode") = dt.Rows(i)("LAF_OtherExpensesID").ToString
                    dRow("CatRes") = dt.Rows(i)("LAF_Charges").ToString
                    dtRes.Rows.Add(dRow)
                Next
            End If
            Return dtRes
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReambersmentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iLOEID As Integer) As DataTable
        Dim dtRes As New DataTable : Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Try
            dtRes.Columns.Add("Id")
            dtRes.Columns.Add("Reambersment")
            dtRes.Columns.Add("ReAmount")
            sSql = "Select * from LOE_ReAmbersment where LAR_LOEID=" & iLOEID & " And LAR_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtRes.NewRow
                    dRow("Id") = dt.Rows(i)("LAR_ID").ToString
                    dRow("Reambersment") = dt.Rows(i)("LAR_ReambName").ToString
                    dRow("ReAmount") = dt.Rows(i)("LAR_Charges").ToString
                    dtRes.Rows.Add(dRow)
                Next
            End If
            Return dtRes
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLOERate(ByVal sAC As String, ByVal iACID As Integer, ByVal iCatID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select empc_charges from SAD_EmpCategory_Charges where empc_cat_id=" & iCatID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveResourceDetails(ByVal sAC As String, ByVal objLOEResources As strLOEResources) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_ID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_ID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_LOEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_LOEID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_CategoryID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_CategoryID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_NoResources", OleDb.OleDbType.SmallInt, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_NoResources
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_ChargesPerDay", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_ChargesPerDay
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_CategoryName", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objLOEResources.sLOER_CategoryName
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_NoDays", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_NoDays
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_ResTotal", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_ResTotal
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_Delflag", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objLOEResources.sLOER_Delflag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_STATUS", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objLOEResources.sLOER_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_CrBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objLOEResources.sLOER_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOER_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objLOEResources.iLOER_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spLOE_Resources", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCategoryDetails(ByVal sAC As String, ByVal objstrLOEAdditionalFees As strLOEAdditionalFees)
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_ID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.iLAF_ID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_LOEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.iLAF_LOEID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_OtherExpensesID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.iLAF_OtherExpensesID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_Charges", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.iLAF_Charges
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_CODE", OleDb.OleDbType.VarChar, 5)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.sLAF_CODE
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_OtherExpensesName", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.sLAF_OtherExpensesName
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_Delflag", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.sLAF_Delflag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_STATUS", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.sLAF_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.iLAF_CrBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.iLAF_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.sLAF_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAF_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrLOEAdditionalFees.iLAF_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spLOE_AdditionalFees", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveReambersmentDetails(ByVal sAC As String, ByVal objstrstrLOEReAmbersment As strLOEReAmbersment) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_ID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.iLAR_ID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_LOEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.iLAR_LOEID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_ReambersmentID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.iLAR_ReambersmentID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_Charges", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.iLAR_Charges
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_ReambName", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.sLAR_ReambName
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_Delflag", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.sLAR_Delflag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_STATUS", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.sLAR_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.iLAR_CrBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.iLAR_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.sLAR_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LAR_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objstrstrLOEReAmbersment.iLAR_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spLOE_ReAmbersment", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteResourceDetailsIfExists(ByVal sAC As String, ByVal iACID As Integer, ByVal LEID As Integer)
        Dim ssql As String = ""
        Try
            ssql = "delete from LOE_Resources where LOER_LOEID = " & LEID & "  and LOER_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteExpensesDetailsIfExists(ByVal sAC As String, ByVal iACID As Integer, ByVal LEID As Integer)
        Dim ssql As String = ""
        Try
            ssql = "delete from LOE_AdditionalFees where LAF_LOEID = " & LEID & "  and LAF_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteReambersementDetailsIfExists(ByVal sAC As String, ByVal iACID As Integer, ByVal LEID As Integer)
        Dim ssql As String = ""
        Try
            ssql = "delete from LOE_ReAmbersment where LAR_LOEID = " & LEID & " and LAR_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadExistingItems(ByVal sAC As String, ByVal iACID As Integer, ByVal iLOEID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim sCity As String = "", sScope As String = ""
        Try
            dt.Columns.Add("LOE_Name")
            dt.Columns.Add("LOE_Address")
            dt.Columns.Add("LOE_Partner")
            dt.Columns.Add("LOE_FuncationName")
            dt.Columns.Add("LOE_YearName")
            dt.Columns.Add("LOE_CompanyName")
            dt.Columns.Add("LOE_Scopeofwork")
            dt.Columns.Add("LOE_Frequency")
            dt.Columns.Add("LOET_Deliverable")
            dt.Columns.Add("LOE_TemplateProfessionalFee")
            dt.Columns.Add("LOET_StdsInternalAudit")
            dt.Columns.Add("LOET_Responsibilities")
            dt.Columns.Add("LOET_Infrastructure")
            dt.Columns.Add("LOET_NDA")
            dt.Columns.Add("LOE_General")
            dt.Columns.Add("LOE_SubFunctionID")
            dt.Columns.Add("LOE_City")
            dt.Columns.Add("LOE_ProfessionalFees")
            dt.Columns.Add("LOE_FunctionId")
            dt.Columns.Add("LOE_AttachID")
            sSql = "Select CUST_Name,Cust_Comm_city,AT.cmm_Desc As Ent_EntityName,YMS_ID,CM.CMM_Desc,LOET_Deliverable,LOET_ProfessionalFees,"
            sSql = sSql & " LOET_StdsInternalAudit,LOET_Responsibilities,LOET_Infrastructure,LOET_NDA,LOET_General,LOET_ScopeOfWork,"
            sSql = sSql & " LOE_SubFunctionId,LOE_ProfessionalFees,LOE_Total,LOE_FunctionId,LOE_AttachID from SAD_CUST_LOE"
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=LOE_CustomerId And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join Content_Management_Master AT On AT.cmm_ID=LOE_FunctionId And AT.CMM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Year_MAster On YMS_YearId=LOE_YearId And YMS_CompID=" & iACID & ""
            sSql = sSql & " Left Join Content_Management_Master CM ON CM.Cmm_Id=LOE_Frequency And CM.CMM_CompID=" & iACID & ""
            sSql = sSql & " Left Join LOE_Template On LOET_LOEID=LOE_Id And LOET_CompID=" & iACID & ""
            sSql = sSql & " Where LOE_Id = " & iLOEID & "and LOE_CompID=" & iACID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows Then
                While dr.Read()
                    dRow = dt.NewRow()
                    If IsDBNull(dr("CUST_Name")) = False Then
                        dRow("LOE_Name") = dr("CUST_Name")
                    End If
                    If IsDBNull(dr("Cust_Comm_city")) = False Or (dr("Cust_Comm_city") <> "") Then
                        dRow("LOE_City") = dr("Cust_Comm_city")
                    End If
                    'dRow("LOE_Address") = objDBL.SQLExecuteScalar(sAC, "Select Distinct Company_Address From Trace_CompanyDetails ")
                    dRow("LOE_Partner") = ""
                    If IsDBNull(dr("Ent_EntityName")) = False Then
                        dRow("LOE_FuncationName") = dr("Ent_EntityName")
                    End If
                    If IsDBNull(dr("YMS_ID")) = False Then
                        dRow("LOE_YearName") = dr("YMS_ID")
                    End If
                    'dRow("LOE_CompanyName") = objDBL.SQLExecuteScalar(sAC, "Select Distinct Company_Name From Trace_CompanyDetails ")
                    If IsDBNull(dr("CMM_Desc")) = False Then
                        dRow("LOE_Frequency") = dr("CMM_Desc")
                    End If
                    If IsDBNull(dr("LOET_Deliverable")) = False Then
                        dRow("LOET_Deliverable") = dr("LOET_Deliverable")
                    End If
                    'If IsDBNull(dr("LOET_ProfessionalFees")) = False Then
                    '    dRow("LOE_ProffesionalFees") = dr("LOET_ProfessionalFees")
                    'End If
                    If IsDBNull(dr("LOE_Total")) = False Then
                        dRow("LOE_TemplateProfessionalFee") = dr("LOE_Total")
                    End If
                    If IsDBNull(dr("LOET_StdsInternalAudit")) = False Then
                        dRow("LOET_StdsInternalAudit") = dr("LOET_StdsInternalAudit")
                    End If
                    If IsDBNull(dr("LOET_Responsibilities")) = False Then
                        dRow("LOET_Responsibilities") = dr("LOET_Responsibilities")
                    End If
                    If IsDBNull(dr("LOET_Infrastructure")) = False Then
                        dRow("LOET_Infrastructure") = dr("LOET_Infrastructure")
                    End If
                    If IsDBNull(dr("LOET_NDA")) = False Then
                        dRow("LOET_NDA") = dr("LOET_NDA")
                    End If
                    If IsDBNull(dr("LOET_General")) = False Then
                        dRow("LOE_General") = dr("LOET_General")
                    End If
                    If IsDBNull(dr("LOET_ScopeOfWork")) = False Then
                        dRow("LOE_SubFunctionID") = dr("LOET_ScopeOfWork")
                    Else
                        dRow("LOE_SubFunctionID") = dr("LOE_SubFunctionId")
                    End If
                    If IsDBNull(dr("LOE_ProfessionalFees")) = False Then
                        dRow("LOE_ProfessionalFees") = dr("LOE_ProfessionalFees")
                    End If
                    If IsDBNull(dr("LOE_FunctionId")) = False Then
                        dRow("LOE_FunctionId") = dr("LOE_FunctionId")
                    End If
                    If IsDBNull(dr("LOE_AttachID")) = False Then
                        dRow("LOE_AttachID") = dr("LOE_AttachID")
                    End If
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckLOEtemp(ByVal sAC As String, ByVal iACID As Integer, ByVal iLOEID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select LOET_Id from LOE_Template where LOET_LOEID=" & iLOEID & " and LOET_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveLOETemplateDetails(ByVal sAC As String, ByVal objsLOETemplate As strLOETemplate) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_Id", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.iLOET_Id
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_LOEID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.iLOET_LOEID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_CustomerId", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.iLOET_CustomerId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_FunctionId", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.iLOET_FunctionId
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_ScopeOfWork", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_ScopeOfWork
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_Frequency", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_Frequency
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_Deliverable", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_Deliverable
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_ProfessionalFees", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_ProfessionalFees
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_StdsInternalAudit", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_StdsInternalAudit
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_Responsibilities", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_Responsibilities
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_Infrastructure", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_Infrastructure
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_NDA", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_NDA
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_General", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_General
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_Delflag", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_Delflag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_STATUS", OleDb.OleDbType.VarChar, 20)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.iLOET_CrBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.iLOET_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.sLOET_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@LOET_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsLOETemplate.iLOET_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spLOE_Template", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Cabinet
    Public Function GetCabinetPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCustName As String) As Integer
        Dim sSql As String
        Dim iCBN_NODE As Integer = 0
        Try
            sSql = "Select CBN_ID from EDT_CABINET Where CBN_NAME='" & sCustName & "'"
            iCBN_NODE = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iCBN_NODE
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCabName(ByVal sAC As String, ByVal iCabID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CBN_Name from edt_cabinet where CBN_ID <>" & iCabID & " and CBN_Parent=-1 and (CBN_DelStatus='A' or CBN_DelStatus='W')"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select distinct(CUST_GROUPNAME) From SAD_CUSTOMER_MASTER Where Cust_CompID=" & iCompID & " and CUST_GROUPNAME<>'' and CUST_GROUPNAME<>'NULL'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomfontstyle(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CF_ID,CF_name  From Cust_fontstyle Where CF_CompID=" & iCompID & " And CF_DELFLAG='A'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveCustomfont(ByVal sAC As String, ByVal iACID As Integer, ByVal Iuserid As Integer, ByVal iYearID As Integer, ByVal iIpaddress As String, ByVal sFontstyle As String)
        Dim sSql As String
        Dim iId As Integer
        Try
            sSql = "Select ISNULL(max(CF_ID),0)+1 from Cust_fontstyle"
            iId = objDBL.SQLExecuteScalarInt(sAC, sSql)
            sSql = "Insert Into Cust_fontstyle ([CF_ID],[CF_CustId],[CF_name],[CF_YEARId],[CF_CompId],[CF_STATUS]
           ,[CF_DELFLAG],[CF_CRON],[CF_CRBY],[CF_IPAddress]) values"
            sSql = sSql & "(" & iId & ",0,'" & sFontstyle & "'," & iYearID & "," & iACID & ",'C','A',getdate()," & Iuserid & ",'" & iIpaddress & "')"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateCustfontstyle(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustid As Integer, ByVal iCustFontid As Integer)
        Dim sSql As String = ""
        Try
            If iCustid <> 0 Then
                sSql = " update SAD_CUSTOMER_MASTER set CUST_fontstyleid=" & iCustFontid & " where Cust_id=" & iCustid & " and Cust_CompID=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveCustomerStatutoryDirector(ByVal sAC As String, ByVal objsStatutoryDirector As strStatutoryDirector) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_Id", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.iSSD_Id
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.iSSD_CustID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_DirectorName", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_DirectorName
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_DOB", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.dSSD_DOB
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_DIN", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_DIN
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_MobileNo", OleDb.OleDbType.VarChar, 15)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_MobileNo
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_Email", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_Email
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_Remarks", OleDb.OleDbType.VarChar, 1000)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_Remarks
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_CRON", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.dSSD_CRON
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.iSSD_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_UpdatedOn", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.dSSD_UpdatedOn
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.iSSD_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_DelFlag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.sSSD_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSD_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryDirector.iSSD_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_Statutory_DirectorDetails", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub CustDirectorApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iCustID As Integer, ByVal iCustCompPKID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update SAD_Statutory_DirectorDetails set"
            If sType = "DeActivated" Then
                sSql = sSql & " SSD_DelFlag='D',SSD_UpdatedBy=" & iUsrID & ",SSD_UpdatedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " SSD_DelFlag='A',SSD_UpdatedBy=" & iUsrID & ",SSD_UpdatedOn=Getdate(),"
            End If
            sSql = sSql & "SSD_IPAddress='" & sIPAddress & "' Where SSD_Id=" & iCustCompPKID & " And SSD_CustID=" & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCustomerDirectorDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iCustDirectorPKId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim sCity As String = "", sScope As String = ""
        Try
            dt.Columns.Add("DirectorPkID")
            dt.Columns.Add("Name")
            dt.Columns.Add("DOB")
            dt.Columns.Add("DIN")
            dt.Columns.Add("MobileNo")
            dt.Columns.Add("Email")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Status")

            sSql = "Select SSD_Id,SSD_DirectorName,SSD_DOB,SSD_DIN,SSD_MobileNo,SSD_Email,SSD_Remarks,SSD_DelFlag from SAD_Statutory_DirectorDetails where SSD_CustID=" & iCustId & " and SSD_CompID=" & iACID & ""
            If iCustDirectorPKId > 0 Then
                sSql = sSql & " And SSD_Id=" & iCustDirectorPKId & ""
            End If
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows Then
                While dr.Read()
                    dRow = dt.NewRow()
                    If IsDBNull(dr("SSD_Id")) = False Then
                        dRow("DirectorPkID") = dr("SSD_Id")
                    End If
                    If IsDBNull(dr("SSD_DirectorName")) = False Then
                        dRow("Name") = dr("SSD_DirectorName")
                    End If
                    If IsDBNull(dr("SSD_DOB")) = False Then
                        dRow("DOB") = objclsGRACeGeneral.FormatDtForRDBMS(dr("SSD_DOB").ToString, "D")
                    End If
                    If IsDBNull(dr("SSD_DIN")) = False Then
                        dRow("DIN") = dr("SSD_DIN")
                    End If
                    If IsDBNull(dr("SSD_MobileNo")) = False Then
                        dRow("MobileNo") = dr("SSD_MobileNo")
                    End If
                    If IsDBNull(dr("SSD_Email")) = False Then
                        dRow("Email") = dr("SSD_Email")
                    End If
                    If IsDBNull(dr("SSD_Remarks")) = False Then
                        dRow("Remarks") = dr("SSD_Remarks")
                    End If
                    dRow("Status") = dr("SSD_DelFlag")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalShareOfProfit(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iPkID As Integer) As String
        Dim sSql As String
        Try
            sSql = "SELECT Sum(SSP_ShareOfProfit) FROM SAD_Statutory_PartnerDetails WHERE SSP_CustID=" & iCustID & " and SSP_CompID=" & iACID & ""
            If iPkID > 0 Then
                sSql = sSql & " And SSP_Id<>" & iPkID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function CheckCustPartnerName(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal sPartnerName As String, ByVal iPartnerPKId As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select SSP_Id from SAD_Statutory_PartnerDetails where SSP_PartnerName='" & sPartnerName & "' And SSP_CustID=" & iCustID & " And SSP_CompID=" & iACID & ""
            If iPartnerPKId > 0 Then
                sSql = sSql & " And SSP_Id<>" & iPartnerPKId & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCustomerStatutoryPartner(ByVal sAC As String, ByVal objsStatutoryPartner As strStatutoryPartner) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_Id", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.iSSP_Id
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.iSSP_CustID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_PartnerName", OleDb.OleDbType.VarChar, 100)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.sSSP_PartnerName
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_DOJ", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.dSSP_DOJ
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_PAN", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.sSSP_PAN
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_ShareOfProfit", OleDb.OleDbType.Decimal, 10)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.dSSP_ShareOfProfit
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_CapitalAmount", OleDb.OleDbType.Decimal, 10)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.dSSP_CapitalAmount
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_CRON", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.dSSP_CRON
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_CRBY", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.iSSP_CRBY
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_UpdatedOn", OleDb.OleDbType.Date)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.dSSP_UpdatedOn
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.iSSP_UpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.sSSP_DelFlag
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.sSSP_STATUS
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.sSSP_IPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@SSP_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objsStatutoryPartner.iSSP_CompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_Statutory_PartnerDetails", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub CustPartnerApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iCustID As Integer, ByVal iCustCompPKID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update SAD_Statutory_PartnerDetails set"
            If sType = "DeActivated" Then
                sSql = sSql & " SSP_DelFlag='D',SSP_UpdatedBy=" & iUsrID & ",SSP_UpdatedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " SSP_DelFlag='A',SSP_UpdatedBy=" & iUsrID & ",SSP_UpdatedOn=Getdate(),"
            End If
            sSql = sSql & "SSP_IPAddress='" & sIPAddress & "' Where SSP_Id=" & iCustCompPKID & " And SSP_CustID=" & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCustomerPartnerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer, ByVal iCustPartnerPKId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Try
            dt.Columns.Add("PartnerPkID")
            dt.Columns.Add("Name")
            dt.Columns.Add("DOJ")
            dt.Columns.Add("PAN")
            dt.Columns.Add("ShareOfProfit")
            dt.Columns.Add("CapitalAmount")
            dt.Columns.Add("Status")

            sSql = "Select SSP_Id,SSP_PartnerName,SSP_PAN,SSP_DOJ,IsNull(SSP_ShareOfProfit,0) As SSP_ShareOfProfit,IsNull(SSP_CapitalAmount,0) As SSP_CapitalAmount,SSP_DelFlag from SAD_Statutory_PartnerDetails where SSP_CustID=" & iCustId & " and SSP_CompID=" & iACID & ""
            If iCustPartnerPKId > 0 Then
                sSql = sSql & " And SSP_Id=" & iCustPartnerPKId & ""
            End If
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows Then
                While dr.Read()
                    dRow = dt.NewRow()
                    If IsDBNull(dr("SSP_Id")) = False Then
                        dRow("PartnerPkID") = dr("SSP_Id")
                    End If
                    If IsDBNull(dr("SSP_PartnerName")) = False Then
                        dRow("Name") = dr("SSP_PartnerName")
                    End If
                    If IsDBNull(dr("SSP_DOJ")) = False Then
                        dRow("DOJ") = objclsGRACeGeneral.FormatDtForRDBMS(dr("SSP_DOJ").ToString, "D")
                    End If
                    If IsDBNull(dr("SSP_PAN")) = False Then
                        dRow("PAN") = dr("SSP_PAN")
                    End If
                    If IsDBNull(dr("SSP_ShareOfProfit")) = False Then
                        dRow("ShareOfProfit") = dr("SSP_ShareOfProfit")
                    End If
                    If IsDBNull(dr("SSP_CapitalAmount")) = False Then
                        dRow("CapitalAmount") = dr("SSP_CapitalAmount")
                    End If
                    dRow("Status") = dr("SSP_DelFlag")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustAllLocationDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal sType As String) As String
        Dim sSql As String
        Try
            sSql = "SELECT STUFF ((SELECT DISTINCT '; '+ CAST(Mas_Description + ' : ' + " & sType & " AS VARCHAR(MAX)) FROM SAD_CUST_LOCATION WHERE Mas_CustID=" & iCustID & " and Mas_CompID=" & iACID & " FOR XMl PATH('')),1,1,'')"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetCustAllLocationStatutoryRefDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal sType As String) As String
        Dim sSql As String
        Try
            sSql = "Select STUFF ((SELECT DISTINCT '; '+ CAST(Mas_Description + ' : ' + Cust_Value AS VARCHAR(MAX)) From SAD_CUST_Accounting_Template,SAD_CUST_LOCATION Where Cust_Desc='" & sType & "' And Mas_CustID=" & iCustID & " And Cust_Id=" & iCustID & " And Mas_Id=Cust_LocationId FOR XMl PATH('')),1,1,'')"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetCustAllLocationLOEDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String
        Try
            sSql = "SELECT STUFF ((SELECT DISTINCT '; '+ CAST((CONVERT(varchar(10),LOE_TimeSchedule, 103)) + ' to ' + (CONVERT(varchar(10),LOE_ReportDueDate, 103)) AS VARCHAR(MAX)) FROM SAD_CUST_LOE  WHERE LOE_CustomerId=" & iCustID & " and LOE_CompID=" & iACID & " FOR XMl PATH('')),1,1,'')"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetAllCompanyDetails(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select STUFF ((Select DISTINCT '; '+ CAST(Company_Name AS VARCHAR(MAX)) FROM Trace_CompanyDetails  WHERE Company_CompID=1 FOR XMl PATH('')),1,1,'')"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function LoadCustInformationAuditeeDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sFY As String, ByVal iCustId As Integer, ByVal sCustName As String, ByVal sOrganizationType As String, ByVal sBusinessReltnDate As String, ByVal sProdManufactured As String) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtLocation As New DataTable
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("Details")

            dr = dt.NewRow()
            dr("SlNo") = 1
            dr("Particulars") = "Name of the auditee"
            dr("Details") = sCustName
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 2
            dr("Particulars") = "Financial year of Audit"
            dr("Details") = sFY
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 3
            dr("Particulars") = "Period of Audit (i.e. From dd/mm/yyyy to dd/mm/yyyy)"
            dr("Details") = GetCustAllLocationLOEDetails(sAc, iAcID, iCustId)
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 4
            dr("Particulars") = "Constitution: (Proprietary/ Partnership/LLP/ HUF/ AOP/ BOI/ Private/ Public Limited Company/ Trust/ Others)"
            dr("Details") = sOrganizationType
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 5
            dr("Particulars") = "Whether there was any change in constitution during the year. If so, furnish details of such change along with relevant documents evidencing such change (e.g. Retirement/ Admission of Partner/ Director, Merger/ Demerger/ Amalgamation,  Conversion of Private limited company into Public limited company or vice versa, Conversion into LLP, etc.)"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 6
            dr("Particulars") = "Nature of Audit to be conducted –
• Statutory Audit under section 143 of Companies
• Act, 2013
• Tax Audit under section 44AB of the Income Tax
• Act, 1961
• Charitable/ Religious Trust Audit under sections 11,
• 12, 12A, 12AA of the Income Tax Act, 1961
• Special Audit under section 142(2A) of the IncomeTax Act, 1961 or
• Any other specific assignment (e.g. internal audit, stock audit, debtors audit, forensic audit, etc.)"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 7
            dr("Particulars") = "Address(es) of places of Business (Specify the principal place of business and all other places of business such as registered office, corporate office, administrative offices, factories, branches, depots, godowns etc., along with date of commencement of other places of business. In case of any change in address, the date of such change.)"
            dr("Details") = GetCustAllLocationDetails(sAc, iAcID, iCustId, "Mas_Loc_Address")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 8
            dr("Particulars") = "Audit is to be conducted for [Mention whether for the whole unit or any specific unit]"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 9
            dr("Particulars") = "Phone numbers of all places of business"
            dr("Details") = GetCustAllLocationDetails(sAc, iAcID, iCustId, "Mas_Contact_LandLineNo")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 10
            dr("Particulars") = "Fax numbers of all places of business"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 11
            dr("Particulars") = "E-mail addresses of all places of business"
            dr("Details") = GetCustAllLocationDetails(sAc, iAcID, iCustId, "Mas_Contact_Email")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 12
            dr("Particulars") = "Date of Incorporation/ Formation"
            dr("Details") = sBusinessReltnDate
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 13
            dr("Particulars") = "Company (CIN)/ Firm Registration Number"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "CIN")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 14
            dr("Particulars") = "Income Tax PAN"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "PAN")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 15
            dr("Particulars") = "Tax Deduction/ Collection Account Number (TAN) of all units, if any"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "TAN")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 16
            dr("Particulars") = "Central Excise Registration Numbers of all registered units, wherever applicable. Copies of Registration Certificates (Where assessments are pending)"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "Central Excise Registration Numbers")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 17
            dr("Particulars") = "Service Tax Registration Numbers of all registered units, if any. Copies of Registration Certificates.(Where assessments are pending)"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "Service Tax Registration Numbers")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 18
            dr("Particulars") = "VAT Registration Numbers of all registered units, wherever applicable. Copies of Registration Certificates (Where assessments are pending)"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "VAT Registration Numbers")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 19
            dr("Particulars") = "GST Registration Numbers of all registered units, if any. Copies of Registration Certificates"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "GSTIN")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 20
            dr("Particulars") = "Import Export Code Number (IEC)"
            dr("Details") = GetCustAllLocationStatutoryRefDetails(sAc, iAcID, iCustId, "Import Export Code Number (IEC)")
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 21
            dr("Particulars") = "Details of all Bank Accounts (including accounts closed during the period covered by audit) Furnish details such as name of the bank, branch, Type of account (Savings, Current, OD, CC, TL etc.), Account Number, BSR code, MICR code of the branch"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 22
            dr("Particulars") = "Key persons for audit interaction and communication with the management (Proprietor/ Partner(s)/ Director(s)/ Trustee(s))/Manager(s)/ Accounts-in-charge (Whether full time or part time) / Members in the Audit Committee) along along with their PAN/Aadhar/DIN, contact numbers and / or email addresses, their relationship with owners and date of appointment. Change if any, in key persons during the audit period may be also obtained"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 23
            dr("Particulars") = "Contact person/ Coordinator for audit with Phone / Fax / Mobile numbers / E-mail Addresses"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 24
            dr("Particulars") = "Nature of Business / Core Activity Like Manufacturing / Trading / Marketing / Service Provider / Franchisee / Agency / Others (Please specify) In case of any change in the nature of business, details of such changes and date of such changes"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 25
            dr("Particulars") = "Brief note on the manufacturing process / business activities"
            dr("Details") = sProdManufactured
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 26
            dr("Particulars") = "Main products / By-products manufactured / Traded /Dealt in"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 27
            dr("Particulars") = "Main Raw materials used in manufacture"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 28
            dr("Particulars") = "Method of Accounting: Mercantile/ Cash (Wherever method of accounting is  Mercantile and certain items are accounted for on cash basis or vice versa, list out such items)"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 29
            dr("Particulars") = "Method of Book keeping: (Totally computerized/ Totally manual or Mixed (If mixed, specify the areas of computerization)"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 30
            dr("Particulars") = "If computerized, a brief note on accounting package used and list of books/ reports which could be generated from the said accounting package"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 31
            dr("Particulars") = "List of books including inventory books maintained listing out separately computerized and manual"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 32
            dr("Particulars") = "Whether the entity is covered by Internal Audit. If so, name and address of the Internal Auditors. Attach copies of the Internal Audit Reports"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 33
            dr("Particulars") = "Details of all other entities where the Partners /Proprietors / Directors are interested"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 34
            dr("Particulars") = "If yes , nature of such interest"
            dr("Details") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("SlNo") = 35
            dr("Particulars") = "DIN (Director Identification No.) of all Directors"
            dr("Details") = ""
            dt.Rows.Add(dr)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckCustomerAlreadyExists(ByVal sAC As String, ByVal iACID As Integer, ByVal sCustName As String) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SElect * from SAD_CUSTOMER_MASTER where Cust_Name='" & sCustName & "' and CUST_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function CheckCustomerExists(ByVal sAC As String, ByVal iACID As Integer, ByVal sCustName As String) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SElect * from SAD_CUSTOMER_MASTER where Cust_Name='" & sCustName & "' and CUST_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateDepartmentIdToCustomerTable(ByVal sAC As String, ByVal iACID As Integer, ByVal iDeptID As Int16, ByVal iCustID As Integer)
        Dim sSql As String
        Try
            sSql = "Update SAD_CUSTOMER_MASTER set Cust_DeptID=" & iDeptID & " where CUST_ID=" & iCustID & " and CUST_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception

        End Try
    End Function
    Public Function GetTRACeCustomerCount(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(CUST_ID) from SAD_CUSTOMER_MASTER where CUST_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class