Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Structure strUserEmp_QualificationDetails
    Private SUQ_PKID As Integer
    Private SUQ_UserEmpID As Integer
    Private SUQ_Education As String
    Private SUQ_University As String
    Private SUQ_School As String
    Private SUQ_Year As Integer
    Private SUQ_Marks As Double
    Private SUQ_Remarks As String
    Private SUQ_AttachID As Integer
    Private SUQ_CrBy As Integer
    Private SUQ_UpdatedBy As Integer
    Private SUQ_IPAddress As String
    Private SUQ_CompID As Integer
    Public Property iSUQ_PKID() As Integer
        Get
            Return (SUQ_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUQ_PKID = Value
        End Set
    End Property
    Public Property iSUQ_UserEmpID() As Integer
        Get
            Return (SUQ_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUQ_UserEmpID = Value
        End Set
    End Property
    Public Property sSUQ_Education() As String
        Get
            Return (SUQ_Education)
        End Get
        Set(ByVal Value As String)
            SUQ_Education = Value
        End Set
    End Property
    Public Property sSUQ_University() As String
        Get
            Return (SUQ_University)
        End Get
        Set(ByVal Value As String)
            SUQ_University = Value
        End Set
    End Property
    Public Property sSUQ_School() As String
        Get
            Return (SUQ_School)
        End Get
        Set(ByVal Value As String)
            SUQ_School = Value
        End Set
    End Property
    Public Property iSUQ_Year() As Integer
        Get
            Return (SUQ_Year)
        End Get
        Set(ByVal Value As Integer)
            SUQ_Year = Value
        End Set
    End Property
    Public Property dSUQ_Marks() As Double
        Get
            Return (SUQ_Marks)
        End Get
        Set(ByVal Value As Double)
            SUQ_Marks = Value
        End Set
    End Property
    Public Property sSUQ_Remarks() As String
        Get
            Return (SUQ_Remarks)
        End Get
        Set(ByVal Value As String)
            SUQ_Remarks = Value
        End Set
    End Property
    Public Property iSUQ_AttachID() As Integer
        Get
            Return (SUQ_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUQ_AttachID = Value
        End Set
    End Property
    Public Property iSUQ_CrBy() As Integer
        Get
            Return (SUQ_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUQ_CrBy = Value
        End Set
    End Property
    Public Property iSUQ_UpdatedBy() As Integer
        Get
            Return (SUQ_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUQ_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUQ_IPAddress() As String
        Get
            Return (SUQ_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUQ_IPAddress = Value
        End Set
    End Property
    Public Property iSUQ_CompID() As Integer
        Get
            Return (SUQ_CompID)
        End Get
        Set(ByVal Value As Integer)
            SUQ_CompID = Value
        End Set
    End Property
End Structure
Public Structure strUserEmp_CourseDetails
    Private SUC_PKID As Integer
    Private SUC_UserEmpID As Integer
    Private SUC_Date As Date
    Private SUC_Subject As String
    Private SUC_FeeEmployer As String
    Private SUC_FeeEmployee As String
    Private SUC_ConductedBy As String
    Private SUC_CPEPoints As String
    Private SUC_Papers As String
    Private SUC_BriefDescription As String
    Private SUC_FeedBack As String
    Private SUC_Remarks As String
    Private SUC_AttachID As Integer
    Private SUC_CrBy As Integer
    Private SUC_UpdatedBy As Integer
    Private SUC_IPAddress As String
    Private SUC_CompID As Integer
    Public Property iSUC_PKID() As Integer
        Get
            Return (SUC_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUC_PKID = Value
        End Set
    End Property
    Public Property iSUC_UserEmpID() As Integer
        Get
            Return (SUC_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUC_UserEmpID = Value
        End Set
    End Property
    Public Property dSUC_Date() As Date
        Get
            Return (SUC_Date)
        End Get
        Set(ByVal Value As Date)
            SUC_Date = Value
        End Set
    End Property
    Public Property sSUC_Subject() As String
        Get
            Return (SUC_Subject)
        End Get
        Set(ByVal Value As String)
            SUC_Subject = Value
        End Set
    End Property
    Public Property sSUC_FeeEmployer() As String
        Get
            Return (SUC_FeeEmployer)
        End Get
        Set(ByVal Value As String)
            SUC_FeeEmployer = Value
        End Set
    End Property
    Public Property sSUC_FeeEmployee() As String
        Get
            Return (SUC_FeeEmployee)
        End Get
        Set(ByVal Value As String)
            SUC_FeeEmployee = Value
        End Set
    End Property
    Public Property sSUC_ConductedBy() As String
        Get
            Return (SUC_ConductedBy)
        End Get
        Set(ByVal Value As String)
            SUC_ConductedBy = Value
        End Set
    End Property
    Public Property sSUC_CPEPoints() As String
        Get
            Return (SUC_CPEPoints)
        End Get
        Set(ByVal Value As String)
            SUC_CPEPoints = Value
        End Set
    End Property
    Public Property sSUC_Papers() As String
        Get
            Return (SUC_Papers)
        End Get
        Set(ByVal Value As String)
            SUC_Papers = Value
        End Set
    End Property
    Public Property sSUC_BriefDescription() As String
        Get
            Return (SUC_BriefDescription)
        End Get
        Set(ByVal Value As String)
            SUC_BriefDescription = Value
        End Set
    End Property
    Public Property sSUC_FeedBack() As String
        Get
            Return (SUC_FeedBack)
        End Get
        Set(ByVal Value As String)
            SUC_FeedBack = Value
        End Set
    End Property
    Public Property sSUC_Remarks() As String
        Get
            Return (SUC_Remarks)
        End Get
        Set(ByVal Value As String)
            SUC_Remarks = Value
        End Set
    End Property
    Public Property iSUC_AttachID() As Integer
        Get
            Return (SUC_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUC_AttachID = Value
        End Set
    End Property
    Public Property iSUC_CrBy() As Integer
        Get
            Return (SUC_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUC_CrBy = Value
        End Set
    End Property
    Public Property iSUC_UpdatedBy() As Integer
        Get
            Return (SUC_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUC_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUC_IPAddress() As String
        Get
            Return (SUC_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUC_IPAddress = Value
        End Set
    End Property
    Public Property iSUC_CompID() As Integer
        Get
            Return (SUC_CompID)
        End Get
        Set(ByVal Value As Integer)
            SUC_CompID = Value
        End Set
    End Property
End Structure
Public Structure strUserEmp_AssessmentDetails
    Private SUA_PKID As Integer
    Private SUA_UserEmpID As Integer
    Private SUA_IssueDate As Date
    Private SUA_Rating As String
    Private SUA_PerformanceAwardPaid As String
    Private SUA_GradesPromotedFrom As String
    Private SUA_GradesPromotedTo As String
    Private SUA_Remarks As String
    Private SUA_AttachID As Integer
    Private SUA_CrBy As Integer
    Private SUA_UpdatedBy As Integer
    Private SUA_IPAddress As String
    Private SUA_CompID As Integer
    Public Property iSUA_PKID() As Integer
        Get
            Return (SUA_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUA_PKID = Value
        End Set
    End Property
    Public Property iSUA_UserEmpID() As Integer
        Get
            Return (SUA_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUA_UserEmpID = Value
        End Set
    End Property
    Public Property dSUA_IssueDate() As Date
        Get
            Return (SUA_IssueDate)
        End Get
        Set(ByVal Value As Date)
            SUA_IssueDate = Value
        End Set
    End Property
    Public Property sSUA_Rating() As String
        Get
            Return (SUA_Rating)
        End Get
        Set(ByVal Value As String)
            SUA_Rating = Value
        End Set
    End Property
    Public Property sSUA_PerformanceAwardPaid() As String
        Get
            Return (SUA_PerformanceAwardPaid)
        End Get
        Set(ByVal Value As String)
            SUA_PerformanceAwardPaid = Value
        End Set
    End Property
    Public Property sSUA_GradesPromotedFrom() As String
        Get
            Return (SUA_GradesPromotedFrom)
        End Get
        Set(ByVal Value As String)
            SUA_GradesPromotedFrom = Value
        End Set
    End Property
    Public Property sSUA_GradesPromotedTo() As String
        Get
            Return (SUA_GradesPromotedTo)
        End Get
        Set(ByVal Value As String)
            SUA_GradesPromotedTo = Value
        End Set
    End Property
    Public Property sSUA_Remarks() As String
        Get
            Return (SUA_Remarks)
        End Get
        Set(ByVal Value As String)
            SUA_Remarks = Value
        End Set
    End Property
    Public Property iSUA_AttachID() As Integer
        Get
            Return (SUA_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUA_AttachID = Value
        End Set
    End Property
    Public Property iSUA_CrBy() As Integer
        Get
            Return (SUA_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUA_CrBy = Value
        End Set
    End Property
    Public Property iSUA_UpdatedBy() As Integer
        Get
            Return (SUA_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUA_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUA_IPAddress() As String
        Get
            Return (SUA_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUA_IPAddress = Value
        End Set
    End Property
    Public Property iSUA_CompID() As Integer
        Get
            Return (SUA_CompID)
        End Get
        Set(ByVal Value As Integer)
            SUA_CompID = Value
        End Set
    End Property
End Structure
Public Structure strUserEmp_SpecialMentionDetails
    Private SUS_PKID As Integer
    Private SUS_UserEmpID As Integer
    Private SUS_SpecialMention As String
    Private SUS_Date As Date
    Private SUS_Particulars As String
    Private SUS_DealtWith As String
    Private SUS_AttachID As Integer
    Private SUS_CrBy As Integer
    Private SUS_UpdatedBy As Integer
    Private SUS_IPAddress As String
    Private SUS_CompID As Integer
    Public Property iSUS_PKID() As Integer
        Get
            Return (SUS_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUS_PKID = Value
        End Set
    End Property
    Public Property iSUS_UserEmpID() As Integer
        Get
            Return (SUS_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUS_UserEmpID = Value
        End Set
    End Property
    Public Property sSUS_SpecialMention() As String
        Get
            Return (SUS_SpecialMention)
        End Get
        Set(ByVal Value As String)
            SUS_SpecialMention = Value
        End Set
    End Property
    Public Property dSUS_Date() As Date
        Get
            Return (SUS_Date)
        End Get
        Set(ByVal Value As Date)
            SUS_Date = Value
        End Set
    End Property
    Public Property sSUS_Particulars() As String
        Get
            Return (SUS_Particulars)
        End Get
        Set(ByVal Value As String)
            SUS_Particulars = Value
        End Set
    End Property
    Public Property sSUS_DealtWith() As String
        Get
            Return (SUS_DealtWith)
        End Get
        Set(ByVal Value As String)
            SUS_DealtWith = Value
        End Set
    End Property
    Public Property iSUS_AttachID() As Integer
        Get
            Return (SUS_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUS_AttachID = Value
        End Set
    End Property
    Public Property iSUS_CrBy() As Integer
        Get
            Return (SUS_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUS_CrBy = Value
        End Set
    End Property
    Public Property iSUS_UpdatedBy() As Integer
        Get
            Return (SUS_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUS_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUS_IPAddress() As String
        Get
            Return (SUS_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUS_IPAddress = Value
        End Set
    End Property
    Public Property iSUS_CompID() As Integer
        Get
            Return (SUS_CompID)
        End Get
        Set(ByVal Value As Integer)
            SUS_CompID = Value
        End Set
    End Property
End Structure
Public Structure strUserEmp_ParticularsofArticlesDetails
    Private SUP_PKID As Integer
    Private SUP_UserEmpID As Integer
    Private SUP_PrincipleName As String
    Private SUP_RegistrationNo As String
    Private SUP_PracticeNo As String
    Private SUP_ArticlesFrom As Date
    Private SUP_ArticlesTo As Date
    Private SUP_ExtendedTo As Date
    Private SUP_Remarks As String
    Private SUP_AttachID As Integer
    Private SUP_CrBy As Integer
    Private SUP_UpdatedBy As Integer
    Private SUP_IPAddress As String
    Private SUP_CompID As Integer
    Public Property iSUP_PKID() As Integer
        Get
            Return (SUP_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUP_PKID = Value
        End Set
    End Property
    Public Property iSUP_UserEmpID() As Integer
        Get
            Return (SUP_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUP_UserEmpID = Value
        End Set
    End Property
    Public Property sSUP_PrincipleName() As String
        Get
            Return (SUP_PrincipleName)
        End Get
        Set(ByVal Value As String)
            SUP_PrincipleName = Value
        End Set
    End Property
    Public Property sSUP_RegistrationNo() As String
        Get
            Return (SUP_RegistrationNo)
        End Get
        Set(ByVal Value As String)
            SUP_RegistrationNo = Value
        End Set
    End Property
    Public Property sSUP_PracticeNo() As String
        Get
            Return (SUP_PracticeNo)
        End Get
        Set(ByVal Value As String)
            SUP_PracticeNo = Value
        End Set
    End Property
    Public Property dSUP_ArticlesFrom() As Date
        Get
            Return (SUP_ArticlesFrom)
        End Get
        Set(ByVal Value As Date)
            SUP_ArticlesFrom = Value
        End Set
    End Property
    Public Property dSUP_ArticlesTo() As Date
        Get
            Return (SUP_ArticlesTo)
        End Get
        Set(ByVal Value As Date)
            SUP_ArticlesTo = Value
        End Set
    End Property
    Public Property dSUP_ExtendedTo() As Date
        Get
            Return (SUP_ExtendedTo)
        End Get
        Set(ByVal Value As Date)
            SUP_ExtendedTo = Value
        End Set
    End Property
    Public Property sSUP_Remarks() As String
        Get
            Return (SUP_Remarks)
        End Get
        Set(ByVal Value As String)
            SUP_Remarks = Value
        End Set
    End Property
    Public Property iSUP_AttachID() As Integer
        Get
            Return (SUP_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUP_AttachID = Value
        End Set
    End Property
    Public Property iSUP_CrBy() As Integer
        Get
            Return (SUP_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUP_CrBy = Value
        End Set
    End Property
    Public Property iSUP_UpdatedBy() As Integer
        Get
            Return (SUP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUP_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUP_IPAddress() As String
        Get
            Return (SUP_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUP_IPAddress = Value
        End Set
    End Property
    Public Property iSUP_CompID() As Integer
        Get
            Return (SUP_CompID)
        End Get
        Set(ByVal Value As Integer)
            SUP_CompID = Value
        End Set
    End Property
End Structure
Public Class clsEProfile
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private SUA_PKID As Integer
    Private SUA_UserEmpID As Integer
    Private SUA_ContactName As String
    Private SUA_Address1 As String
    Private SUA_Address2 As String
    Private SUA_Address3 As String
    Private SUA_Pincode As Integer
    Private SUA_Mobile As String
    Private SUA_Telephone As String
    Private SUA_Email As String
    Private SUA_RelationType As String
    Private SUA_IPAddress As String
    Private SUA_CompId As Integer

    'Professional Experiance
    Private SUP_PKID As Integer
    Private SUP_UserEmpID As Integer
    Private SUP_Assignment As String
    Private SUP_ReportingTo As String
    Private SUP_From As Integer
    Private SUP_To As Integer
    Private SUP_SalaryPerAnnum As Double
    Private SUP_Position As String
    Private SUP_Remarks As String
    Private SUP_AttachID As Integer
    Private SUP_CrBy As Integer
    Private SUP_UpdatedBy As Integer
    Private SUP_IPAddress As String
    Private SUP_CompId As Integer

    'Assets Obtained On Loan
    Private SUAL_PKID As Integer
    Private SUAL_UserEmpID As Integer
    Private SUAL_AssetType As String
    Private SUAL_SerialNo As String
    Private SUAL_ApproValue As Integer
    Private SUAL_IssueDate As Date
    Private SUAL_DueDate As Date
    Private SUAL_RecievedDate As Date
    Private SUAL_ConditionIssue As String
    Private SUAL_ConditionReceipt As String
    Private SUAL_Remarks As String
    Private SUAL_AttachID As Integer
    Private SUAL_CrBy As Integer
    Private SUAL_UpdatedBy As Integer
    Private SUAL_IPAddress As String
    Private SUAL_CompId As Integer

    'Academic Progress
    Private SUAP_PKID As Integer
    Private SUAP_UserEmpID As Integer
    Private SUAP_ExamTakenOn As Date
    Private SUAP_LeaveGranted As Integer
    Private SUAP_MonthofExam As Integer
    Private SUAP_Groups As String
    Private SUAP_Result As String
    Private SUAP_Remarks As String
    Private SUAP_AttachID As Integer
    Private SUAP_CrBy As Integer
    Private SUAP_UpdatedBy As Integer
    Private SUAP_IPAddress As String
    Private SUAP_CompId As Integer

    'Transfers Within The Firm
    Private SUTF_PKID As Integer
    Private SUTF_UserEmpID As Integer
    Private SUTF_EarlierPrinciple As String
    Private SUTF_NewPrinciple As String
    Private SUTF_DateofTransfer As Date
    Private SUTF_DurationWithNewPrinciple As String
    Private SUTF_CompletionDate As Date
    Private SUTF_ExtendedTo As Date
    Private SUTF_Remarks As String
    Private SUTF_AttachID As Integer
    Private SUTF_CrBy As Integer
    Private SUTF_UpdatedBy As Integer
    Private SUTF_IPAddress As String
    Private SUTF_CompId As Integer

    Public Property iSUAPKID() As Integer
        Get
            Return (SUA_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUA_PKID = Value
        End Set
    End Property
    Public Property iSUAUserEmpID() As Integer
        Get
            Return (SUA_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUA_UserEmpID = Value
        End Set
    End Property
    Public Property sSUAContactName() As String
        Get
            Return (SUA_ContactName)
        End Get
        Set(ByVal Value As String)
            SUA_ContactName = Value
        End Set
    End Property
    Public Property sSUAAddress1() As String
        Get
            Return (SUA_Address1)
        End Get
        Set(ByVal Value As String)
            SUA_Address1 = Value
        End Set
    End Property
    Public Property sSUAAddress2() As String
        Get
            Return (SUA_Address2)
        End Get
        Set(ByVal Value As String)
            SUA_Address2 = Value
        End Set
    End Property
    Public Property sSUAAddress3() As String
        Get
            Return (SUA_Address3)
        End Get
        Set(ByVal Value As String)
            SUA_Address3 = Value
        End Set
    End Property
    Public Property iSUAPincode() As Integer
        Get
            Return (SUA_Pincode)
        End Get
        Set(ByVal Value As Integer)
            SUA_Pincode = Value
        End Set
    End Property
    Public Property sSUAMobile() As String
        Get
            Return (SUA_Mobile)
        End Get
        Set(ByVal Value As String)
            SUA_Mobile = Value
        End Set
    End Property
    Public Property sSUATelephone() As String
        Get
            Return (SUA_Telephone)
        End Get
        Set(ByVal Value As String)
            SUA_Telephone = Value
        End Set
    End Property
    Public Property sSUAEmail() As String
        Get
            Return (SUA_Email)
        End Get
        Set(ByVal Value As String)
            SUA_Email = Value
        End Set
    End Property
    Public Property sSUARelationType() As String
        Get
            Return (SUA_RelationType)
        End Get
        Set(ByVal Value As String)
            SUA_RelationType = Value
        End Set
    End Property
    Public Property sSUAIPAddress() As String
        Get
            Return (SUA_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUA_IPAddress = Value
        End Set
    End Property
    Public Property iSUACompId() As Integer
        Get
            Return (SUA_CompId)
        End Get
        Set(ByVal Value As Integer)
            SUA_CompId = Value
        End Set
    End Property

    'Professional Experiance
    Public Property iSUPPKID() As Integer
        Get
            Return (SUP_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUP_PKID = Value
        End Set
    End Property
    Public Property iSUPUserEmpID() As Integer
        Get
            Return (SUP_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUP_UserEmpID = Value
        End Set
    End Property
    Public Property sSUPAssignment() As String
        Get
            Return (SUP_Assignment)
        End Get
        Set(ByVal Value As String)
            SUP_Assignment = Value
        End Set
    End Property
    Public Property sSUPReportingTo() As String
        Get
            Return (SUP_ReportingTo)
        End Get
        Set(ByVal Value As String)
            SUP_ReportingTo = Value
        End Set
    End Property
    Public Property iSUPFrom() As Integer
        Get
            Return (SUP_From)
        End Get
        Set(ByVal Value As Integer)
            SUP_From = Value
        End Set
    End Property
    Public Property iSUPTo() As Integer
        Get
            Return (SUP_To)
        End Get
        Set(ByVal Value As Integer)
            SUP_To = Value
        End Set
    End Property
    Public Property dSUPSalaryPerAnnum() As Double
        Get
            Return (SUP_SalaryPerAnnum)
        End Get
        Set(ByVal Value As Double)
            SUP_SalaryPerAnnum = Value
        End Set
    End Property
    Public Property sSUPPosition() As String
        Get
            Return (SUP_Position)
        End Get
        Set(ByVal Value As String)
            SUP_Position = Value
        End Set
    End Property
    Public Property sSUPRemarks() As String
        Get
            Return (SUP_Remarks)
        End Get
        Set(ByVal Value As String)
            SUP_Remarks = Value
        End Set
    End Property
    Public Property iSUPAttachID() As Integer
        Get
            Return (SUP_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUP_AttachID = Value
        End Set
    End Property
    Public Property iSUPCrBy() As Integer
        Get
            Return (SUP_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUP_CrBy = Value
        End Set
    End Property
    Public Property iSUPUpdatedBy() As Integer
        Get
            Return (SUP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUP_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUPIPAddress() As String
        Get
            Return (SUP_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUP_IPAddress = Value
        End Set
    End Property
    Public Property iSUPCompId() As Integer
        Get
            Return (SUP_CompId)
        End Get
        Set(ByVal Value As Integer)
            SUP_CompId = Value
        End Set
    End Property

    'Assets Obtained On Loan
    Public Property iSUALPKID() As Integer
        Get
            Return (SUAL_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUAL_PKID = Value
        End Set
    End Property
    Public Property iSUALUserEmpID() As Integer
        Get
            Return (SUAL_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUAL_UserEmpID = Value
        End Set
    End Property
    Public Property sSUALAssetType() As String
        Get
            Return (SUAL_AssetType)
        End Get
        Set(ByVal Value As String)
            SUAL_AssetType = Value
        End Set
    End Property
    Public Property sSUALSerialNo() As String
        Get
            Return (SUAL_SerialNo)
        End Get
        Set(ByVal Value As String)
            SUAL_SerialNo = Value
        End Set
    End Property
    Public Property iSUALApproValue() As Integer
        Get
            Return (SUAL_ApproValue)
        End Get
        Set(ByVal Value As Integer)
            SUAL_ApproValue = Value
        End Set
    End Property
    Public Property dSUALIssueDate() As Date
        Get
            Return (SUAL_IssueDate)
        End Get
        Set(ByVal Value As Date)
            SUAL_IssueDate = Value
        End Set
    End Property
    Public Property dSUALDueDate() As Date
        Get
            Return (SUAL_DueDate)
        End Get
        Set(ByVal Value As Date)
            SUAL_DueDate = Value
        End Set
    End Property
    Public Property dSUALRecievedDate() As Date
        Get
            Return (SUAL_RecievedDate)
        End Get
        Set(ByVal Value As Date)
            SUAL_RecievedDate = Value
        End Set
    End Property
    Public Property sSUALConditionIssue() As String
        Get
            Return (SUAL_ConditionIssue)
        End Get
        Set(ByVal Value As String)
            SUAL_ConditionIssue = Value
        End Set
    End Property
    Public Property sSUALConditionReceipt() As String
        Get
            Return (SUAL_ConditionReceipt)
        End Get
        Set(ByVal Value As String)
            SUAL_ConditionReceipt = Value
        End Set
    End Property
    Public Property sSUALRemarks() As String
        Get
            Return (SUAL_Remarks)
        End Get
        Set(ByVal Value As String)
            SUAL_Remarks = Value
        End Set
    End Property
    Public Property iSUALAttachID() As Integer
        Get
            Return (SUAL_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUAL_AttachID = Value
        End Set
    End Property
    Public Property iSUALCrBy() As Integer
        Get
            Return (SUAL_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUAL_CrBy = Value
        End Set
    End Property
    Public Property iSUALUpdatedBy() As Integer
        Get
            Return (SUAL_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUAL_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUALIPAddress() As String
        Get
            Return (SUAL_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUAL_IPAddress = Value
        End Set
    End Property
    Public Property iSUALCompId() As Integer
        Get
            Return (SUAL_CompId)
        End Get
        Set(ByVal Value As Integer)
            SUAL_CompId = Value
        End Set
    End Property

    'Academic Progress
    Public Property iSUAPPKID() As Integer
        Get
            Return (SUAP_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUAP_PKID = Value
        End Set
    End Property
    Public Property iSUAPUserEmpID() As Integer
        Get
            Return (SUAP_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUAP_UserEmpID = Value
        End Set
    End Property
    Public Property dSUAPExamTakenOn() As Date
        Get
            Return (SUAP_ExamTakenOn)
        End Get
        Set(ByVal Value As Date)
            SUAP_ExamTakenOn = Value
        End Set
    End Property
    Public Property iSUAPLeaveGranted() As Integer
        Get
            Return (SUAP_LeaveGranted)
        End Get
        Set(ByVal Value As Integer)
            SUAP_LeaveGranted = Value
        End Set
    End Property
    Public Property iSUAPMonthofExam() As Integer
        Get
            Return (SUAP_MonthofExam)
        End Get
        Set(ByVal Value As Integer)
            SUAP_MonthofExam = Value
        End Set
    End Property
    Public Property sSUAPGroups() As String
        Get
            Return (SUAP_Groups)
        End Get
        Set(ByVal Value As String)
            SUAP_Groups = Value
        End Set
    End Property
    Public Property sSUAPResult() As String
        Get
            Return (SUAP_Result)
        End Get
        Set(ByVal Value As String)
            SUAP_Result = Value
        End Set
    End Property
    Public Property sSUAPRemarks() As String
        Get
            Return (SUAP_Remarks)
        End Get
        Set(ByVal Value As String)
            SUAP_Remarks = Value
        End Set
    End Property
    Public Property iSUAPAttachID() As Integer
        Get
            Return (SUAP_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUAP_AttachID = Value
        End Set
    End Property
    Public Property iSUAPCrBy() As Integer
        Get
            Return (SUAP_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUAP_CrBy = Value
        End Set
    End Property
    Public Property iSUAPUpdatedBy() As Integer
        Get
            Return (SUAP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUAP_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUAPIPAddress() As String
        Get
            Return (SUAP_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUAP_IPAddress = Value
        End Set
    End Property
    Public Property iSUAPCompId() As Integer
        Get
            Return (SUAP_CompId)
        End Get
        Set(ByVal Value As Integer)
            SUAP_CompId = Value
        End Set
    End Property

    'Transfers Within The Firm
    Public Property iSUTFPKID() As Integer
        Get
            Return (SUTF_PKID)
        End Get
        Set(ByVal Value As Integer)
            SUTF_PKID = Value
        End Set
    End Property
    Public Property iSUTFUserEmpID() As Integer
        Get
            Return (SUTF_UserEmpID)
        End Get
        Set(ByVal Value As Integer)
            SUTF_UserEmpID = Value
        End Set
    End Property
    Public Property sSUTFEarlierPrinciple() As String
        Get
            Return (SUTF_EarlierPrinciple)
        End Get
        Set(ByVal Value As String)
            SUTF_EarlierPrinciple = Value
        End Set
    End Property
    Public Property sSUTFNewPrinciple() As String
        Get
            Return (SUTF_NewPrinciple)
        End Get
        Set(ByVal Value As String)
            SUTF_NewPrinciple = Value
        End Set
    End Property
    Public Property dSUTFDateofTransfer() As Date
        Get
            Return (SUTF_DateofTransfer)
        End Get
        Set(ByVal Value As Date)
            SUTF_DateofTransfer = Value
        End Set
    End Property
    Public Property sSUTFDurationWithNewPrinciple() As String
        Get
            Return (SUTF_DurationWithNewPrinciple)
        End Get
        Set(ByVal Value As String)
            SUTF_DurationWithNewPrinciple = Value
        End Set
    End Property
    Public Property dSUTFCompletionDate() As Date
        Get
            Return (SUTF_CompletionDate)
        End Get
        Set(ByVal Value As Date)
            SUTF_CompletionDate = Value
        End Set
    End Property
    Public Property dSUTFExtendedTo() As Date
        Get
            Return (SUTF_ExtendedTo)
        End Get
        Set(ByVal Value As Date)
            SUTF_ExtendedTo = Value
        End Set
    End Property
    Public Property sSUTFRemarks() As String
        Get
            Return (SUTF_Remarks)
        End Get
        Set(ByVal Value As String)
            SUTF_Remarks = Value
        End Set
    End Property
    Public Property iSUTFAttachID() As Integer
        Get
            Return (SUTF_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SUTF_AttachID = Value
        End Set
    End Property
    Public Property iSUTFCrBy() As Integer
        Get
            Return (SUTF_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SUTF_CrBy = Value
        End Set
    End Property
    Public Property iSUTFUpdatedBy() As Integer
        Get
            Return (SUTF_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SUTF_UpdatedBy = Value
        End Set
    End Property
    Public Property sSUTFIPAddress() As String
        Get
            Return (SUTF_IPAddress)
        End Get
        Set(ByVal Value As String)
            SUTF_IPAddress = Value
        End Set
    End Property
    Public Property iSUTFCompId() As Integer
        Get
            Return (SUTF_CompId)
        End Get
        Set(ByVal Value As Integer)
            SUTF_CompId = Value
        End Set
    End Property
    Public Function GetMaxEmployeeCode(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim iMaxID As Integer
        Dim sMaxID As String = ""
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(MAX(Usr_ID)+1,1) from Sad_UserDetails where Usr_CompId=" & iACID & "")
            If iMaxID = 0 Then
                sMaxID = "EMP001"
            ElseIf iMaxID > 0 And iMaxID < 10 Then
                sMaxID = "EMP00" & iMaxID
            ElseIf iMaxID >= 10 And iMaxID < 100 Then
                sMaxID = "EMP0" & iMaxID
            Else
                sMaxID = "EMP" & iMaxID
            End If
            Return sMaxID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingEmployeeBasicDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from Sad_UserDetails where  Usr_ID=" & iUserID & " and Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingEmployeeDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName,Usr_FullName,Usr_Delflag,Usr_Code,usr_Designation,usr_LoginName,usr_PassWord,Mas_Description,Convert(Varchar(10),usr_DOB,103)usr_DOB,"
            sSql = sSql & " usr_CurWrkAddId,usr_PermAddId,usr_ResAddId,usr_OfficialAddId,usr_Photo,usr_Signature,usr_Resume,usr_BloodGroup,usr_Gender,usr_MaritalStatus,usr_NoOfChildren,"
            sSql = sSql & " a.SUA_PKID As CPKID,b.SUA_PKID As PPKID,c.SUA_PKID As EPKID,d.SUA_PKID As MFVPKID,"
            sSql = sSql & " a.SUA_Address1 As CAddress1,a.SUA_Address2 As CAddress2,a.SUA_Address3 As CAddress3,a.SUA_Pincode As CPincode,a.SUA_Mobile As CMobile,a.SUA_Telephone As CTelephone,"
            sSql = sSql & " b.SUA_Address1 As PAddress1,b.SUA_Address2 As PAddress2,b.SUA_Address3 As PAddress3,b.SUA_Pincode As PPincode,b.SUA_Mobile As PMobile,b.SUA_Telephone As PTelephone,"
            sSql = sSql & " c.SUA_ContactName As EContactName,c.SUA_Address1 As EAddress1,c.SUA_Address2 As EAddress2,c.SUA_Address3 As EAddress3,c.SUA_Pincode As EPincode,c.SUA_Mobile As EMobile,c.SUA_Telephone As ETelephone,"
            sSql = sSql & " c.SUA_Email As EEmail, c.SUA_RelationType As ERelationType,d.SUA_ContactName As MFVContactName,d.SUA_Address1 As MFVAddress1,"
            sSql = sSql & " d.SUA_Address2 As MFVAddress2,d.SUA_Address3 As MFVAddress3,d.SUA_Pincode As MFVPincode,d.SUA_Mobile As MFVMobile,"
            sSql = sSql & " d.SUA_Telephone As MFVTelephone,d.SUA_Email As MFVEmail,d.SUA_RelationType As MFVRelationType"
            sSql = sSql & " From sad_userdetails Left Join SAD_GRPDESGN_General_Master On usr_Designation=Mas_ID And Mas_Delflag='A' And Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_UserEMP_Address a On a.SUA_UserEmpID=" & iUserID & " And a.SUA_CompId=" & iACID & " And a.SUA_PKID=usr_CurWrkAddId"
            sSql = sSql & " Left Join SAD_UserEMP_Address b On b.SUA_UserEmpID=" & iUserID & " And b.SUA_CompId=" & iACID & " And b.SUA_PKID=usr_PermAddId"
            sSql = sSql & " Left Join SAD_UserEMP_Address c On c.SUA_UserEmpID=" & iUserID & " And c.SUA_CompId=" & iACID & " And c.SUA_PKID=usr_ResAddId"
            sSql = sSql & " Left Join SAD_UserEMP_Address d On d.SUA_UserEmpID=" & iUserID & " And d.SUA_CompId=" & iACID & " And d.SUA_PKID=usr_OfficialAddId"
            sSql = sSql & " Where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From edt_attachments where ATCH_CompID=" & iACID & " And"
            sSql = sSql & " ATCH_ID = " & iAttachID & " And ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function GetFolderNames(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("FolderID")
            dt.Columns.Add("FolderName")
            dRow = dt.NewRow
            dRow("FolderID") = 1
            dRow("FolderName") = "Current Address"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 2
            dRow("FolderName") = "Permanent Address"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 3
            dRow("FolderName") = "Emergency Contact"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("FolderID") = 4
            dRow("FolderName") = "Parents Contact"
            dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPhotoDocID(ByVal sAC As String, ByVal iACID As Integer, ByVal iPhotoAttachID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Atch_DocID From edt_attachments where ATCH_CompID=" & iACID & " And ATCH_ID=" & iPhotoAttachID & " And ATCH_Status <> 'D'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEMPQualification(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("UserID")
            dtTab.Columns.Add("AttachID")
            dtTab.Columns.Add("EmpEducation")
            dtTab.Columns.Add("EmpUniversity")
            dtTab.Columns.Add("EmpCollege")
            dtTab.Columns.Add("EmpYear")
            dtTab.Columns.Add("EmpMarks")
            dtTab.Columns.Add("EmpRemarks")

            sSql = "Select SUQ_PKID,SUQ_UserEmpID,SUQ_Education,SUQ_University,SUQ_School,SUQ_Year,SUQ_Marks,SUQ_Remarks,SUQ_AttachID From Sad_UserEMP_Qualification "
            sSql = sSql & " where SUQ_UserEmpID=" & iUserID & " And SUQ_CompID=" & iACID & ""
            If iID > 0 Then
                sSql = sSql & " And SUQ_PKID=" & iID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                drow("ID") = dt.Rows(i)("SUQ_PKID")
                drow("UserID") = dt.Rows(i)("SUQ_UserEmpID")
                drow("AttachID") = dt.Rows(i)("SUQ_AttachID")
                drow("EmpEducation") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUQ_Education"))
                drow("EmpUniversity") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUQ_University"))
                drow("EmpCollege") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUQ_School"))
                drow("EmpYear") = dt.Rows(i)("SUQ_Year")
                drow("EmpMarks") = dt.Rows(i)("SUQ_Marks")
                If IsDBNull(dt.Rows(i)("SUQ_Remarks")) = False Then
                    drow("EmpRemarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUQ_Remarks"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEMPCourse(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("UserID")
            dtTab.Columns.Add("AttachID")
            dtTab.Columns.Add("ECSSubject")
            dtTab.Columns.Add("ECSDate")
            dtTab.Columns.Add("ECSDescription")
            dtTab.Columns.Add("PapersPresented")
            dtTab.Columns.Add("ConductedBy")
            dtTab.Columns.Add("FeesPaidEmployer")
            dtTab.Columns.Add("FeesPaidEmployee")
            dtTab.Columns.Add("CPEPoints")
            dtTab.Columns.Add("FeedBack")
            dtTab.Columns.Add("Remarks")

            sSql = "Select SUC_PKID,SUC_UserEmpID,SUC_Date,SUC_Subject,SUC_FeeEmployer,SUC_FeeEmployee,SUC_ConductedBy,SUC_CPEPoints,SUC_Papers,SUC_BriefDescription,"
            sSql = sSql & " SUC_FeedBack,SUC_Remarks,SUC_AttachID From Sad_UserEMP_Courses Where SUC_UserEmpID=" & iUserID & " And SUC_CompID=" & iACID & ""
            If iID > 0 Then
                sSql = sSql & " And SUC_PKID=" & iID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                drow("ID") = dt.Rows(i)("SUC_PKID")
                drow("UserID") = dt.Rows(i)("SUC_UserEmpID")
                drow("AttachID") = dt.Rows(i)("SUC_AttachID")
                If IsDBNull(dt.Rows(i)("SUC_Subject")) = False Then
                    drow("ECSSubject") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_Subject"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_Date")) = False Then
                    drow("ECSDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("SUC_Date"), "F")
                End If
                If IsDBNull(dt.Rows(i)("SUC_BriefDescription")) = False Then
                    drow("ECSDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_BriefDescription"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_Papers")) = False Then
                    drow("PapersPresented") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_Papers"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_ConductedBy")) = False Then
                    drow("ConductedBy") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_ConductedBy"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_FeeEmployer")) = False Then
                    drow("FeesPaidEmployer") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_FeeEmployer"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_FeeEmployee")) = False Then
                    drow("FeesPaidEmployee") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_FeeEmployee"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_CPEPoints")) = False Then
                    drow("CPEPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_CPEPoints"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_FeedBack")) = False Then
                    drow("FeedBack") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_FeedBack"))
                End If
                If IsDBNull(dt.Rows(i)("SUC_Remarks")) = False Then
                    drow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUC_Remarks"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEmpProfessionalExperienceDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPEPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim drow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PEPKID")
            dt.Columns.Add("AttachID")
            dt.Columns.Add("Assignment")
            dt.Columns.Add("ReportingTo")
            dt.Columns.Add("From")
            dt.Columns.Add("To")
            dt.Columns.Add("SalaryPerAnnum")
            dt.Columns.Add("PositionHeld")
            dt.Columns.Add("Remarks")

            sSql = "Select SUP_PKID,SUP_AttachID,SUP_Assignment,SUP_ReportingTo,SUP_From,SUP_To,SUP_SalaryPerAnnum,SUP_Position,SUP_Remarks"
            sSql = sSql & " from SAD_UserEMP_ProfExperiance Where SUP_UserEmpID=" & iUserID & " And SUP_CompId=" & iACID & ""
            If iPEPKID > 0 Then
                sSql = sSql & " And SUP_PKID=" & iPEPKID & ""
            End If
            sSql = sSql & " Order By SUP_PKID Asc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                drow = dt.NewRow
                drow("SrNo") = i + 1
                drow("PEPKID") = dtDetails.Rows(i)("SUP_PKID")
                drow("AttachID") = dtDetails.Rows(i)("SUP_AttachID")
                drow("Assignment") = dtDetails.Rows(i)("SUP_Assignment")
                drow("ReportingTo") = dtDetails.Rows(i)("SUP_ReportingTo")
                drow("From") = dtDetails.Rows(i)("SUP_From")
                drow("To") = dtDetails.Rows(i)("SUP_To")
                drow("SalaryPerAnnum") = dtDetails.Rows(i)("SUP_SalaryPerAnnum")
                drow("PositionHeld") = dtDetails.Rows(i)("SUP_Position")
                drow("Remarks") = dtDetails.Rows(i)("SUP_Remarks")
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEmpAsstesLoanDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iALPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim drow As DataRow
        Dim dIssueDate As Date, dDueDate As Date
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ALPKID")
            dt.Columns.Add("AttachID")
            dt.Columns.Add("TypeOfAsset")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("ApproxValue")
            dt.Columns.Add("IssueDate")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("RecievedDate")
            dt.Columns.Add("ConditionWhenIssued")
            dt.Columns.Add("ConditionOnReceipt")
            dt.Columns.Add("Remarks")

            sSql = "Select SUAL_PKID,SUAL_AssetType,SUAL_SerialNo,SUAL_ApproValue,Convert(Varchar(10),SUAL_IssueDate,103)SUAL_IssueDate,"
            sSql = sSql & " Convert(Varchar(10), SUAL_DueDate, 103)SUAL_DueDate,Convert(Varchar(10), SUAL_RecievedDate, 103)SUAL_RecievedDate,SUAL_ConditionIssue,SUAL_Remarks,"
            sSql = sSql & " SUAL_ConditionReceipt,SUAL_AttachID From SAD_UserEMP_AssetsLoan Where SUAL_UserEmpID=" & iUserID & " And SUAL_CompId=" & iACID & ""
            If iALPKID > 0 Then
                sSql = sSql & " And SUAL_PKID=" & iALPKID & ""
            End If
            sSql = sSql & " Order By SUAL_PKID Asc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                drow = dt.NewRow
                drow("SrNo") = i + 1
                drow("ALPKID") = dtDetails.Rows(i)("SUAL_PKID")
                drow("AttachID") = dtDetails.Rows(i)("SUAL_AttachID")
                drow("TypeOfAsset") = dtDetails.Rows(i)("SUAL_AssetType")
                drow("SerialNo") = dtDetails.Rows(i)("SUAL_SerialNo")
                drow("ApproxValue") = dtDetails.Rows(i)("SUAL_ApproValue")
                If IsDBNull(dtDetails.Rows(i)("SUAL_IssueDate")) = False Then
                    dIssueDate = DateTime.ParseExact(dtDetails.Rows(i)("SUAL_IssueDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    drow("IssueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dIssueDate, "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("SUAL_DueDate")) = False Then
                    dDueDate = DateTime.ParseExact(dtDetails.Rows(i)("SUAL_DueDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    drow("DueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dDueDate, "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("SUAL_RecievedDate")) = False Then
                    If dtDetails.Rows(0).Item("SUAL_RecievedDate") = "01/01/1900" Then
                        drow("RecievedDate") = ""
                    Else
                        dDueDate = DateTime.ParseExact(dtDetails.Rows(i)("SUAL_RecievedDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        drow("RecievedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dDueDate, "F")
                    End If
                End If
                drow("IssueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("SUAL_IssueDate"), "F")
                drow("DueDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("SUAL_DueDate"), "F")
                drow("ConditionWhenIssued") = dtDetails.Rows(i)("SUAL_ConditionIssue")
                drow("ConditionOnReceipt") = dtDetails.Rows(i)("SUAL_ConditionReceipt")
                drow("Remarks") = dtDetails.Rows(i)("SUAL_Remarks")
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function LoadEMPAssessment(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("UserID")
            dtTab.Columns.Add("AttachID")
            dtTab.Columns.Add("AssessmentDate")
            dtTab.Columns.Add("PARating")
            dtTab.Columns.Add("PerformanceAwardPaid")
            dtTab.Columns.Add("GradePromotedFrom")
            dtTab.Columns.Add("GradePromotedTo")
            dtTab.Columns.Add("Remarks")

            sSql = "Select SUA_PKID,SUA_UserEmpID,SUA_IssueDate,SUA_Rating,SUA_PerformanceAwardPaid,SUA_GradesPromotedFrom,SUA_GradesPromotedTo,SUA_Remarks,SUA_AttachID"
            sSql = sSql & " From Sad_UserEMP_Assessment Where SUA_UserEmpID=" & iUserID & " And SUA_CompID=" & iACID & ""
            If iID > 0 Then
                sSql = sSql & " And SUA_PKID=" & iID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                drow("ID") = dt.Rows(i)("SUA_PKID")
                drow("UserID") = dt.Rows(i)("SUA_UserEmpID")
                drow("AttachID") = dt.Rows(i)("SUA_AttachID")
                If IsDBNull(dt.Rows(i)("SUA_IssueDate")) = False Then
                    drow("AssessmentDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("SUA_IssueDate"), "F")
                End If
                If IsDBNull(dt.Rows(i)("SUA_Rating")) = False Then
                    drow("PARating") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUA_Rating"))
                End If
                If IsDBNull(dt.Rows(i)("SUA_PerformanceAwardPaid")) = False Then
                    drow("PerformanceAwardPaid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUA_PerformanceAwardPaid"))
                End If
                If IsDBNull(dt.Rows(i)("SUA_GradesPromotedFrom")) = False Then
                    drow("GradePromotedFrom") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUA_GradesPromotedFrom"))
                End If
                If IsDBNull(dt.Rows(i)("SUA_GradesPromotedTo")) = False Then
                    drow("GradePromotedTo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUA_GradesPromotedTo"))
                End If
                If IsDBNull(dt.Rows(i)("SUA_Remarks")) = False Then
                    drow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUA_Remarks"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEmpAcademicProgressDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iAPPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim drow As DataRow
        Dim dExamTakenOn As Date
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("APPKID")
            dt.Columns.Add("AttachID")
            dt.Columns.Add("ExamTaken")
            dt.Columns.Add("NoOfDaysLeave")
            dt.Columns.Add("MonthOfExam")
            dt.Columns.Add("MonthOfExamID")
            dt.Columns.Add("Groups")
            dt.Columns.Add("Result")
            dt.Columns.Add("Remarks")

            sSql = "Select SUAP_PKID,Convert(Varchar(10),SUAP_ExamTakenOn,103)SUAP_ExamTakenOn,SUAP_LeaveGranted,SUAP_MonthofExam,SUAP_Groups,SUAP_Result,SUAP_Remarks,"
            sSql = sSql & " SUAP_AttachID From SAD_UserEMP_AcademicProgress Where SUAP_UserEmpID=" & iUserID & " And SUAP_CompId=" & iACID & ""
            If iAPPKID > 0 Then
                sSql = sSql & " And SUAP_PKID=" & iAPPKID & ""
            End If
            sSql = sSql & " Order By SUAP_PKID Asc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                drow = dt.NewRow
                drow("SrNo") = i + 1
                drow("APPKID") = dtDetails.Rows(i)("SUAP_PKID")
                drow("AttachID") = dtDetails.Rows(i)("SUAP_AttachID")
                If IsDBNull(dtDetails.Rows(i)("SUAP_ExamTakenOn")) = False Then
                    dExamTakenOn = DateTime.ParseExact(dtDetails.Rows(i)("SUAP_ExamTakenOn"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    drow("ExamTaken") = objclsGRACeGeneral.FormatDtForRDBMS(dExamTakenOn, "F")
                End If
                drow("NoOfDaysLeave") = dtDetails.Rows(i)("SUAP_LeaveGranted")
                If dtDetails.Rows(i)("SUAP_MonthofExam") = 1 Then
                    drow("MonthOfExam") = "January"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 2 Then
                    drow("MonthOfExam") = "February"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 3 Then
                    drow("MonthOfExam") = "March"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 4 Then
                    drow("MonthOfExam") = "April"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 5 Then
                    drow("MonthOfExam") = "May"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 6 Then
                    drow("MonthOfExam") = "June"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 7 Then
                    drow("MonthOfExam") = "July"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 8 Then
                    drow("MonthOfExam") = "August"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 9 Then
                    drow("MonthOfExam") = "September"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 10 Then
                    drow("MonthOfExam") = "October"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 11 Then
                    drow("MonthOfExam") = "November"
                ElseIf dtDetails.Rows(i)("SUAP_MonthofExam") = 12 Then
                    drow("MonthOfExam") = "December"
                End If
                drow("MonthOfExamID") = dtDetails.Rows(i)("SUAP_MonthofExam")
                drow("Groups") = dtDetails.Rows(i)("SUAP_Groups")
                drow("Result") = dtDetails.Rows(i)("SUAP_Result")
                drow("Remarks") = dtDetails.Rows(i)("SUAP_Remarks")
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function LoadEMPSpecialMention(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("UserID")
            dtTab.Columns.Add("AttachID")
            dtTab.Columns.Add("SpecialMentions")
            dtTab.Columns.Add("SMDate")
            dtTab.Columns.Add("SMParticulars")
            dtTab.Columns.Add("SMHowDealtWith")

            sSql = "Select SUS_PKID,SUS_UserEmpID,SUS_SpecialMention,SUS_Date,SUS_Particulars,SUS_DealtWith,SUS_AttachID"
            sSql = sSql & " From Sad_UserEMP_SpecialMentions Where SUS_UserEmpID=" & iUserID & " And SUS_CompID=" & iACID & ""
            If iID > 0 Then
                sSql = sSql & " And SUS_PKID=" & iID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                drow("ID") = dt.Rows(i)("SUS_PKID")
                drow("UserID") = dt.Rows(i)("SUS_UserEmpID")
                drow("AttachID") = dt.Rows(i)("SUS_AttachID")
                If IsDBNull(dt.Rows(i)("SUS_SpecialMention")) = False Then
                    drow("SpecialMentions") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUS_SpecialMention"))
                End If
                If IsDBNull(dt.Rows(i)("SUS_Date")) = False Then
                    drow("SMDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("SUS_Date"), "F")
                End If
                If IsDBNull(dt.Rows(i)("SUS_Particulars")) = False Then
                    drow("SMParticulars") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUS_Particulars"))
                End If
                If IsDBNull(dt.Rows(i)("SUS_DealtWith")) = False Then
                    drow("SMHowDealtWith") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUS_DealtWith"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadEmpTransferFirmDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iAPPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim drow As DataRow
        Dim dDateofTransfer As Date, dCompletionDate As New Date, dExtendedTo As New Date
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("TFPKID")
            dt.Columns.Add("AttachID")
            dt.Columns.Add("EarlierPrinciple")
            dt.Columns.Add("NewPrinciple")
            dt.Columns.Add("DateofTransfer")
            dt.Columns.Add("DurationWithNewPrinciple")
            dt.Columns.Add("CompletionDate")
            dt.Columns.Add("ExtendedTo")
            dt.Columns.Add("Remarks")

            sSql = "Select SUTF_PKID,SUTF_EarlierPrinciple,SUTF_NewPrinciple,Convert(Varchar(10),SUTF_DateofTransfer,103)SUTF_DateofTransfer,SUTF_DurationWithNewPrinciple,"
            sSql = sSql & " Convert(Varchar(10),SUTF_CompletionDate,103)SUTF_CompletionDate,Convert(Varchar(10),SUTF_ExtendedTo,103)SUTF_ExtendedTo,"
            sSql = sSql & " SUTF_Remarks,SUTF_AttachID From SAD_UserEMP_TransferFirm Where SUTF_UserEmpID=" & iUserID & " And SUTF_CompId=" & iACID & ""
            If iAPPKID > 0 Then
                sSql = sSql & " And SUTF_PKID=" & iAPPKID & ""
            End If
            sSql = sSql & " Order By SUTF_PKID Asc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                drow = dt.NewRow
                drow("SrNo") = i + 1
                drow("TFPKID") = dtDetails.Rows(i)("SUTF_PKID")
                drow("AttachID") = dtDetails.Rows(i)("SUTF_AttachID")
                drow("EarlierPrinciple") = dtDetails.Rows(i)("SUTF_EarlierPrinciple")
                drow("NewPrinciple") = dtDetails.Rows(i)("SUTF_NewPrinciple")
                If IsDBNull(dtDetails.Rows(i)("SUTF_DateofTransfer")) = False Then
                    dDateofTransfer = DateTime.ParseExact(dtDetails.Rows(i)("SUTF_DateofTransfer"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    drow("DateofTransfer") = objclsGRACeGeneral.FormatDtForRDBMS(dDateofTransfer, "F")
                End If
                drow("DurationWithNewPrinciple") = dtDetails.Rows(i)("SUTF_DurationWithNewPrinciple")
                If IsDBNull(dtDetails.Rows(i)("SUTF_CompletionDate")) = False Then
                    dDateofTransfer = DateTime.ParseExact(dtDetails.Rows(i)("SUTF_CompletionDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    drow("CompletionDate") = objclsGRACeGeneral.FormatDtForRDBMS(dDateofTransfer, "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("SUTF_ExtendedTo")) = False Then
                    dExtendedTo = DateTime.ParseExact(dtDetails.Rows(i)("SUTF_ExtendedTo"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    drow("ExtendedTo") = objclsGRACeGeneral.FormatDtForRDBMS(dExtendedTo, "F")
                End If
                drow("Remarks") = dtDetails.Rows(i)("SUTF_Remarks")
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function LoadEMPParticularsofArticles(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("UserID")
            dtTab.Columns.Add("AttachID")
            dtTab.Columns.Add("NameOfThePrinciple")
            dtTab.Columns.Add("ArticleRegistrationNo")
            dtTab.Columns.Add("CertificateOfParticleNo")
            dtTab.Columns.Add("PeriodOfArticlesFrom")
            dtTab.Columns.Add("PeriodOfArticlesTo")
            dtTab.Columns.Add("PeriodOfArticlesExtendedTo")
            dtTab.Columns.Add("PeriodOfArticlesRemarks")

            sSql = "Select SUP_PKID,SUP_UserEmpID,SUP_PrincipleName,SUP_RegistrationNo,SUP_PracticeNo,SUP_ArticlesFrom,SUP_ArticlesTo,SUP_ExtendedTo,SUP_Remarks,SUP_AttachID"
            sSql = sSql & " From Sad_UserEMP_ParticularsofArticles Where SUP_UserEmpID=" & iUserID & " And SUP_CompID=" & iACID & ""
            If iID > 0 Then
                sSql = sSql & " And SUP_PKID=" & iID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                drow("ID") = dt.Rows(i)("SUP_PKID")
                drow("UserID") = dt.Rows(i)("SUP_UserEmpID")
                drow("AttachID") = dt.Rows(i)("SUP_AttachID")
                If IsDBNull(dt.Rows(i)("SUP_PrincipleName")) = False Then
                    drow("NameOfThePrinciple") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUP_PrincipleName"))
                End If
                If IsDBNull(dt.Rows(i)("SUP_RegistrationNo")) = False Then
                    drow("ArticleRegistrationNo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUP_RegistrationNo"))
                End If
                If IsDBNull(dt.Rows(i)("SUP_PracticeNo")) = False Then
                    drow("CertificateOfParticleNo") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUP_PracticeNo"))
                End If
                If IsDBNull(dt.Rows(i)("SUP_ArticlesFrom")) = False Then
                    drow("PeriodOfArticlesFrom") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("SUP_ArticlesFrom"), "F")
                End If
                If IsDBNull(dt.Rows(i)("SUP_ArticlesTo")) = False Then
                    drow("PeriodOfArticlesTo") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("SUP_ArticlesTo"), "F")
                End If
                If IsDBNull(dt.Rows(i)("SUP_ExtendedTo")) = False Then
                    drow("PeriodOfArticlesExtendedTo") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("SUP_ExtendedTo"), "F")
                End If
                If IsDBNull(dt.Rows(i)("SUP_Remarks")) = False Then
                    drow("PeriodOfArticlesRemarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SUP_Remarks"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDescSelectedDoc(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal sDesc As String)
        Dim sSql As String
        Try
            sSql = "Update edt_attachments set ATCH_Desc='" & sDesc & "' where ATCH_CompID=" & iACID & " and atch_id=" & iAttachID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateAttachID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iAttachID As Integer, ByVal sType As String)
        Dim sSql As String
        Try
            'Employee Details
            If sType = "EQ" Then
                sSql = "Update Sad_UserEMP_Qualification set SUQ_AttachID=" & iAttachID & " where SUQ_CompID=" & iACID & " and SUQ_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sType = "ECS" Then
                sSql = "Update Sad_UserEMP_Courses set SUC_AttachID=" & iAttachID & " where SUC_CompID=" & iACID & " and SUC_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sType = "PE" Then
                sSql = "Update SAD_UserEMP_ProfExperiance set SUP_AttachID=" & iAttachID & " where SUP_CompId=" & iACID & " and SUP_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sType = "AL" Then
                sSql = "Update SAD_UserEMP_AssetsLoan set SUAL_AttachID=" & iAttachID & " where SUAL_CompId=" & iACID & " and SUAL_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
                'HR Details
            ElseIf sType = "PA" Then
                sSql = "Update Sad_UserEMP_Assessment set SUA_AttachID=" & iAttachID & " where SUA_CompID=" & iACID & " and SUA_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sType = "AP" Then
                sSql = "Update SAD_UserEMP_AcademicProgress set SUAP_AttachID=" & iAttachID & " where SUAP_CompId=" & iACID & " and SUAP_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sType = "SM" Then
                sSql = "Update Sad_UserEMP_SpecialMentions set SUS_AttachID=" & iAttachID & " where SUS_CompID=" & iACID & " and SUS_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
                'Article Details
            ElseIf sType = "TF" Then
                sSql = "Update SAD_UserEMP_TransferFirm set SUTF_AttachID=" & iAttachID & " where SUTF_CompId=" & iACID & " and SUTF_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sType = "POA" Then
                sSql = "Update Sad_UserEMP_ParticularsofArticles set SUP_AttachID=" & iAttachID & " where SUP_CompID=" & iACID & " and SUP_UserEmpID=" & iUserID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadEmpDetAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal sAttachID As String) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From edt_attachments where ATCH_CompID=" & iACID & " And "
            sSql = sSql & " ATCH_ID In (" & sAttachID & ") AND ATCH_Status <> 'D' Order by ATCH_CREATEDON Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function SaveEmployeeAddressDetails(ByVal sAC As String, ByVal objclsEProfile As clsEProfile, ByVal sType As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAUserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_ContactName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAContactName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Address1", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAAddress1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Address2", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAAddress2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Address3", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAAddress3
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Pincode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Mobile", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAMobile
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Telephone", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsEProfile.sSUATelephone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Email", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAEmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_RelationType", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsEProfile.sSUARelationType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_FormName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = sType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUACompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_UserEMP_Address", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpQualificationDetails(ByVal sAC As String, ByVal objQualification As strUserEmp_QualificationDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objQualification.iSUQ_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objQualification.iSUQ_UserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_Education", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objQualification.sSUQ_Education
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_University", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objQualification.sSUQ_University
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_School", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objQualification.sSUQ_School
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_Year", OleDb.OleDbType.Integer, 50)
            ObjParam(iParamCount).Value = objQualification.iSUQ_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_Marks", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objQualification.dSUQ_Marks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objQualification.sSUQ_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objQualification.iSUQ_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objQualification.iSUQ_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objQualification.iSUQ_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objQualification.sSUQ_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUQ_CompID", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objQualification.iSUQ_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSad_UserEMP_Qualification", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpCourseDetails(ByVal sAC As String, ByVal objCourse As strUserEmp_CourseDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCourse.iSUC_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCourse.iSUC_UserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_Date", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objCourse.dSUC_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_Subject", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_Subject
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_FeeEmployer", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_FeeEmployer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_FeeEmployee", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_FeeEmployee
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_ConductedBy", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_ConductedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_CPEPoints", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_CPEPoints
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_Papers", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_Papers
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_BriefDescription", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_BriefDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_FeedBack", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCourse.sSUC_FeedBack
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objCourse.sSUC_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCourse.iSUC_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCourse.iSUC_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCourse.iSUC_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCourse.sSUC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUC_CompID", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objCourse.iSUC_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSad_UserEMP_Courses", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpProfessionalExperienceDetails(ByVal sAC As String, ByVal objclsEProfile As clsEProfile)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPUserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Assignment", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsEProfile.sSUPAssignment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_ReportingTo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEProfile.sSUPReportingTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_From", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_To", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_SalaryPerAnnum", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objclsEProfile.dSUPSalaryPerAnnum
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Position", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsEProfile.sSUPPosition
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUPRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEProfile.sSUPIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUPCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_UserEMP_ProfExperiance", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpAsstesLoanDetails(ByVal sAC As String, ByVal objclsEProfile As clsEProfile)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUALPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUALUserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_AssetType", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsEProfile.sSUALAssetType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_SerialNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsEProfile.sSUALSerialNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_ApproValue", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUALApproValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_IssueDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEProfile.dSUALIssueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_DueDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEProfile.dSUALDueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_RecievedDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEProfile.dSUALRecievedDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_ConditionIssue", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsEProfile.sSUALConditionIssue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_ConditionReceipt", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsEProfile.sSUALConditionReceipt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUALRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUALAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUALCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUALUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEProfile.sSUALIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAL_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUALCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_UserEMP_AssetsLoan", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpAssessmentDetails(ByVal sAC As String, ByVal objAssessment As strUserEmp_AssessmentDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAssessment.iSUA_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAssessment.iSUA_UserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_IssueDate", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objAssessment.dSUA_IssueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Rating", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAssessment.sSUA_Rating
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_PerformanceAwardPaid", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAssessment.sSUA_PerformanceAwardPaid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_GradesPromotedFrom", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAssessment.sSUA_GradesPromotedFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_GradesPromotedTo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objAssessment.sSUA_GradesPromotedTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAssessment.sSUA_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAssessment.iSUA_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAssessment.iSUA_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAssessment.iSUA_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAssessment.sSUA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUA_CompID", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objAssessment.iSUA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSad_UserEMP_Assessment", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpAcademicProgressDetails(ByVal sAC As String, ByVal objclsEProfile As clsEProfile)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPUserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_ExamTakenOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEProfile.dSUAPExamTakenOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_LeaveGranted", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPLeaveGranted
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_MonthofExam", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPMonthofExam
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_Groups", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAPGroups
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_Result", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAPResult
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAPRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEProfile.sSUAPIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUAP_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUAPCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_UserEMP_AcademicProgress", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpSpecialMentionDetails(ByVal sAC As String, ByVal objSpecialMention As strUserEmp_SpecialMentionDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSpecialMention.iSUS_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSpecialMention.iSUS_UserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_SpecialMention", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objSpecialMention.sSUS_SpecialMention
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_Date", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objSpecialMention.dSUS_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_Particulars", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objSpecialMention.sSUS_Particulars
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_DealtWith", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objSpecialMention.sSUS_DealtWith
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSpecialMention.iSUS_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSpecialMention.iSUS_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSpecialMention.iSUS_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objSpecialMention.sSUS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUS_CompID", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objSpecialMention.iSUS_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSad_UserEMP_SpecialMentions", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpTransferFirmDetails(ByVal sAC As String, ByVal objclsEProfile As clsEProfile)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUTFPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUTFUserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_EarlierPrinciple", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUTFEarlierPrinciple
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_NewPrinciple", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUTFNewPrinciple
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_DateofTransfer", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEProfile.dSUTFDateofTransfer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_DurationWithNewPrinciple", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUTFDurationWithNewPrinciple
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_CompletionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEProfile.dSUTFCompletionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_ExtendedTo", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEProfile.dSUTFExtendedTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsEProfile.sSUTFRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUTFAttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUTFCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUTFUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEProfile.sSUTFIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUTF_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEProfile.iSUTFCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_UserEMP_TransferFirm", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmpParticularsofArticlesDetails(ByVal sAC As String, ByVal objParticularsofArticles As strUserEmp_ParticularsofArticlesDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParticularsofArticles.iSUP_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_UserEmpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParticularsofArticles.iSUP_UserEmpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_PrincipleName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParticularsofArticles.sSUP_PrincipleName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_RegistrationNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParticularsofArticles.sSUP_RegistrationNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_PracticeNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParticularsofArticles.sSUP_PracticeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_ArticlesFrom", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objParticularsofArticles.dSUP_ArticlesFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_ArticlesTo", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objParticularsofArticles.dSUP_ArticlesTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_ExtendedTo", OleDb.OleDbType.Date, 50)
            ObjParam(iParamCount).Value = objParticularsofArticles.dSUP_ExtendedTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_Remarks", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objParticularsofArticles.sSUP_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParticularsofArticles.iSUP_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParticularsofArticles.iSUP_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParticularsofArticles.iSUP_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objParticularsofArticles.sSUP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SUP_CompID", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objParticularsofArticles.iSUP_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSad_UserEMP_ParticularsofArticles", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingEmployeeDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("EmpDOB")
            dtTab.Columns.Add("EmpGender")
            dtTab.Columns.Add("EmpBloodGroup")
            dtTab.Columns.Add("EmpMaritalStatus")
            dtTab.Columns.Add("EmpNoOfChildren")
            dtTab.Columns.Add("EmpCAAddress1")
            dtTab.Columns.Add("EmpCAddress2")
            dtTab.Columns.Add("EmpCAddress3")
            dtTab.Columns.Add("EmpCPincode")
            dtTab.Columns.Add("EmpCMobile")
            dtTab.Columns.Add("EmpCTelephone")
            dtTab.Columns.Add("EmpPAddress1")
            dtTab.Columns.Add("EmpPAddress2")
            dtTab.Columns.Add("EmpPAddress3")
            dtTab.Columns.Add("EmpPPincode")
            dtTab.Columns.Add("EmpPMobile")
            dtTab.Columns.Add("EmpPTelephone")
            dtTab.Columns.Add("EmpEContactName")
            dtTab.Columns.Add("EmpEAddress1")
            dtTab.Columns.Add("EmpEAddress2")
            dtTab.Columns.Add("EmpEAddress3")
            dtTab.Columns.Add("EmpEPincode")
            dtTab.Columns.Add("EmpEMobile")
            dtTab.Columns.Add("EmpETelephone")
            dtTab.Columns.Add("EmpEEmail")
            dtTab.Columns.Add("EmpERelationType")
            dtTab.Columns.Add("EmpMFVContactName")
            dtTab.Columns.Add("EmpMFVAddress1")
            dtTab.Columns.Add("EmpMFVAddress2")
            dtTab.Columns.Add("EmpMFVAddress3")
            dtTab.Columns.Add("EmpMFVPincode")
            dtTab.Columns.Add("EmpMFVMobile")
            dtTab.Columns.Add("EmpMFVTelephone")
            dtTab.Columns.Add("EmpMFVEmail")
            dtTab.Columns.Add("EmpMFVRelationType")

            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName,Usr_FullName,Usr_Code,usr_Designation,usr_LoginName,usr_PassWord,Mas_Description,Convert(Varchar(10),usr_DOB,103)usr_DOB,"
            sSql = sSql & " usr_CurWrkAddId,usr_PermAddId,usr_ResAddId,usr_OfficialAddId,usr_Photo,usr_Signature,usr_Resume,usr_BloodGroup,usr_Gender,usr_MaritalStatus,usr_NoOfChildren,"
            sSql = sSql & " a.SUA_PKID As CPKID,b.SUA_PKID As PPKID,c.SUA_PKID As EPKID,d.SUA_PKID As MFVPKID,"
            sSql = sSql & " a.SUA_Address1 As CAddress1,a.SUA_Address2 As CAddress2,a.SUA_Address3 As CAddress3,a.SUA_Pincode As CPincode,a.SUA_Mobile As CMobile,a.SUA_Telephone As CTelephone,"
            sSql = sSql & " b.SUA_Address1 As PAddress1,b.SUA_Address2 As PAddress2,b.SUA_Address3 As PAddress3,b.SUA_Pincode As PPincode,b.SUA_Mobile As PMobile,b.SUA_Telephone As PTelephone,"
            sSql = sSql & " c.SUA_ContactName As EContactName,c.SUA_Address1 As EAddress1,c.SUA_Address2 As EAddress2,c.SUA_Address3 As EAddress3,c.SUA_Pincode As EPincode,c.SUA_Mobile As EMobile,c.SUA_Telephone As ETelephone,"
            sSql = sSql & " c.SUA_Email As EEmail, c.SUA_RelationType As ERelationType,d.SUA_ContactName As MFVContactName,d.SUA_Address1 As MFVAddress1,"
            sSql = sSql & " d.SUA_Address2 As MFVAddress2,d.SUA_Address3 As MFVAddress3,d.SUA_Pincode As MFVPincode,d.SUA_Mobile As MFVMobile,"
            sSql = sSql & " d.SUA_Telephone As MFVTelephone,d.SUA_Email As MFVEmail,d.SUA_RelationType As MFVRelationType"
            sSql = sSql & " From sad_userdetails Left Join SAD_GRPDESGN_General_Master On usr_Designation=Mas_ID And Mas_Delflag='A' And Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_UserEMP_Address a On a.SUA_UserEmpID=" & iUserID & " And a.SUA_CompId=" & iACID & " And a.SUA_PKID=usr_CurWrkAddId"
            sSql = sSql & " Left Join SAD_UserEMP_Address b On b.SUA_UserEmpID=" & iUserID & " And b.SUA_CompId=" & iACID & " And b.SUA_PKID=usr_PermAddId"
            sSql = sSql & " Left Join SAD_UserEMP_Address c On c.SUA_UserEmpID=" & iUserID & " And c.SUA_CompId=" & iACID & " And c.SUA_PKID=usr_ResAddId"
            sSql = sSql & " Left Join SAD_UserEMP_Address d On d.SUA_UserEmpID=" & iUserID & " And d.SUA_CompId=" & iACID & " And d.SUA_PKID=usr_OfficialAddId"
            sSql = sSql & " Where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i).Item("usr_DOB")) = False Then
                    If dt.Rows(i).Item("usr_DOB") = "01/01/1900" Then
                        GoTo NextEmp
                    Else
                        drow("EmpDOB") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("usr_DOB"), "F")
                        If IsDBNull(dt.Rows(i).Item("usr_NoOfChildren")) = False Then
                            drow("EmpNoOfChildren") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("usr_NoOfChildren"))
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(i).Item("usr_Gender")) = False Then
                    If dt.Rows(0).Item("usr_Gender") = 1 Then
                        drow("EmpGender") = "Male"
                    ElseIf dt.Rows(0).Item("usr_Gender") = 2 Then
                        drow("EmpGender") = "Female"
                    End If
                End If
                drow("EmpBloodGroup") = ""
                If IsDBNull(dt.Rows(i).Item("usr_BloodGroup")) = False And dt.Rows(i).Item("usr_BloodGroup") <> "" Then
                    drow("EmpBloodGroup") = dt.Rows(i).Item("usr_BloodGroup")
                End If
                If IsDBNull(dt.Rows(i).Item("usr_MaritalStatus")) = False Then
                    If dt.Rows(i).Item("usr_MaritalStatus") = 1 Then
                        drow("EmpMaritalStatus") = "Single"
                    Else
                        drow("EmpMaritalStatus") = "Married"
                    End If
                End If

                'Contact Address
                If IsDBNull(dt.Rows(i).Item("CAddress1")) = False Then
                    drow("EmpCAAddress1") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAddress1"))
                End If
                If IsDBNull(dt.Rows(i).Item("CAddress2")) = False Then
                    drow("EmpCAddress2") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAddress2"))
                End If
                If IsDBNull(dt.Rows(i).Item("CAddress3")) = False Then
                    drow("EmpCAddress3") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAddress3"))
                End If
                If IsDBNull(dt.Rows(i).Item("CPincode")) = False Then
                    drow("EmpCPincode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CPincode"))
                End If
                If IsDBNull(dt.Rows(i).Item("CMobile")) = False Then
                    drow("EmpCMobile") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CMobile"))
                End If
                If IsDBNull(dt.Rows(i).Item("CTelephone")) = False Then
                    drow("EmpCTelephone") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CTelephone"))
                End If

                'Permanent Address
                If IsDBNull(dt.Rows(i).Item("PAddress1")) = False Then
                    drow("EmpPAddress1") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PAddress1"))
                End If
                If IsDBNull(dt.Rows(i).Item("PAddress2")) = False Then
                    drow("EmpPAddress2") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PAddress2"))
                End If
                If IsDBNull(dt.Rows(i).Item("PAddress3")) = False Then
                    drow("EmpPAddress3") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PAddress3"))
                End If
                If IsDBNull(dt.Rows(i).Item("PPincode")) = False Then
                    drow("EmpPPincode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PPincode"))
                End If
                If IsDBNull(dt.Rows(i).Item("PMobile")) = False Then
                    drow("EmpPMobile") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PMobile"))
                End If
                If IsDBNull(dt.Rows(i).Item("PTelephone")) = False Then
                    drow("EmpPTelephone") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PTelephone"))
                End If

                'Emergency Contact
                If IsDBNull(dt.Rows(i).Item("EContactName")) = False Then
                    drow("EmpEContactName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("EContactName"))
                End If
                If IsDBNull(dt.Rows(i).Item("EAddress1")) = False Then
                    drow("EmpEAddress1") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("EAddress1"))
                End If
                If IsDBNull(dt.Rows(i).Item("EAddress2")) = False Then
                    drow("EmpEAddress2") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("EAddress2"))
                End If
                If IsDBNull(dt.Rows(i).Item("EAddress3")) = False Then
                    drow("EmpEAddress3") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("EAddress3"))
                End If
                If IsDBNull(dt.Rows(i).Item("EPincode")) = False Then
                    drow("EmpEPincode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("EPincode"))
                End If
                If IsDBNull(dt.Rows(i).Item("EMobile")) = False Then
                    drow("EmpEMobile") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("EMobile"))
                End If
                If IsDBNull(dt.Rows(i).Item("ETelephone")) = False Then
                    drow("EmpETelephone") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ETelephone"))
                End If
                If IsDBNull(dt.Rows(i).Item("EEmail")) = False Then
                    drow("EmpEEmail") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("EEmail"))
                End If
                If IsDBNull(dt.Rows(i).Item("ERelationType")) = False Then
                    drow("EmpERelationType") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ERelationType"))
                End If

                'Mother/Father/Wife Contact
                If IsDBNull(dt.Rows(i).Item("MFVContactName")) = False Then
                    drow("EmpMFVContactName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVContactName"))
                End If
                If IsDBNull(dt.Rows(i).Item("MFVAddress1")) = False Then
                    drow("EmpMFVAddress1") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVAddress1"))
                End If
                If IsDBNull(dt.Rows(i).Item("MFVAddress2")) = False Then
                    drow("EmpMFVAddress2") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVAddress2"))
                End If
                If IsDBNull(dt.Rows(i).Item("MFVAddress3")) = False Then
                    drow("EmpMFVAddress3") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVAddress3"))
                End If
                If IsDBNull(dt.Rows(i).Item("MFVPincode")) = False Then
                    drow("EmpMFVPincode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVPincode"))
                End If
                If IsDBNull(dt.Rows(i).Item("MFVMobile")) = False Then
                    drow("EmpMFVMobile") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVMobile"))
                End If
                If IsDBNull(dt.Rows(i).Item("MFVTelephone")) = False Then
                    drow("EmpMFVTelephone") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVTelephone"))
                End If
                If IsDBNull(dt.Rows(0).Item("MFVEmail")) = False Then
                    drow("EmpMFVEmail") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MFVEmail"))
                End If
                If IsDBNull(dt.Rows(0).Item("MFVRelationType")) = False Then
                    If dt.Rows(i)("MFVRelationType") = 1 Then
                        drow("EmpMFVRelationType") = "Father"
                    ElseIf dt.Rows(i)("MFVRelationType") = 2 Then
                        drow("EmpMFVRelationType") = "Mother"
                    ElseIf dt.Rows(i)("MFVRelationType") = 3 Then
                        drow("EmpMFVRelationType") = "Wife"
                    End If
                End If
                dtTab.Rows.Add(drow)
            Next
NextEmp:    Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingEmployeeDetailsPhotoId(ByVal sAC As String, ByVal iACID As Integer, ByVal sUserName As String) As String
        Dim sSql As String
        Try
            sSql = "SELECT ISNULL(usr_Photo, '0') FROM sad_userdetails WHERE usr_LoginName = '" & sUserName & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllEmpBasicDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim dt As New DataTable, dtZoneRegionBranchAreaDetails As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String, sModule As String = "", sRole As String = 0
        Try
            dtZoneRegionBranchAreaDetails = GetZoneRegionAreaBranch(sAC, iACID)

            dt.Columns.Add("SrNo")
            dt.Columns.Add("SAPCode")
            dt.Columns.Add("EmployeeName")
            dt.Columns.Add("LoginName")
            dt.Columns.Add("Designation")
            dt.Columns.Add("EMail")
            dt.Columns.Add("ResNo")
            dt.Columns.Add("OffNo")
            dt.Columns.Add("MobNo")
            dt.Columns.Add("UserPermission")
            dt.Columns.Add("Module")
            dt.Columns.Add("Role")
            dt.Columns.Add("Zone")
            dt.Columns.Add("Region")
            dt.Columns.Add("Area")
            dt.Columns.Add("Branch")

            sSql = "Select a.usr_id,a.usr_node,(a.Usr_FullName + ' - ' + a.Usr_Code) as FullName,a.Usr_Role,a.usr_FullName,a.Usr_LoginName,a.usr_Code,a.usr_DutyStatus,a.usr_Node,a.Usr_OrgnID,a.usr_LevelGrp,a.usr_GrpOrUserLvlPerm,"
            sSql = sSql & " a.Usr_MasterModule,a.Usr_MasterRole,a.Usr_AuditModule,a.Usr_AuditRole,a.Usr_RiskModule,a.Usr_RiskRole,a.usr_delFlag,a.Usr_ComplianceModule,a.Usr_ComplianceRole,a.Usr_BCMModule,a.Usr_BCMRole,"
            sSql = sSql & " a.usr_DelFlag,a.USR_LastLoginDate,b.mas_Description as Designation,d.mas_Description As MasterRole,e.mas_Description As AuditRole,"
            sSql = sSql & " f.mas_Description As RiskRole,g.mas_Description As ComplianceRole,h.mas_Description As BCMRole, a.USR_LastLoginDate,a.usr_Email as EMail,"
            sSql = sSql & " a.usr_PhoneNo as ResNo,a.usr_MobileNo as MobNo,a.usr_OfficePhone as OffNo,a.Usr_GrpOrUserLvlPerm as UserPermission from sad_userdetails a"
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master b On a.usr_Designation=b.mas_ID "
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master d On a.Usr_MasterRole=d.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master e On a.Usr_AuditRole=e.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master f On a.Usr_RiskRole=f.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master g On a.Usr_ComplianceRole=g.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master h On a.Usr_BCMRole=h.mas_ID "
            sSql = sSql & " where Usr_CompID=" & iACID & " And Usr_Node>0 and Usr_OrgnID>0 And Usr_ID=" & iUserID & ""
            sSql = sSql & " order by FullName"

            dtDetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("usr_Code")) = False Then
                    dRow("SAPCode") = dtDetails.Rows(i)("usr_Code")
                End If
                If IsDBNull(dtDetails.Rows(i)("usr_FullName")) = False Then
                    dRow("EmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("usr_FullName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("Usr_LoginName")) = False Then
                    dRow("LoginName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Usr_LoginName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("Designation")) = False Then
                    dRow("Designation") = dtDetails.Rows(i)("Designation")
                End If
                If IsDBNull(dtDetails.Rows(i)("EMail")) = False Then
                    dRow("EMail") = dtDetails.Rows(i)("EMail")
                End If
                If IsDBNull(dtDetails.Rows(i)("ResNo")) = False Then
                    dRow("ResNo") = dtDetails.Rows(i)("ResNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("OffNo")) = False Then
                    dRow("OffNo") = dtDetails.Rows(i)("OffNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("MobNo")) = False Then
                    dRow("MobNo") = dtDetails.Rows(i)("MobNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("UserPermission")) = False Then
                    If dtDetails.Rows(i)("UserPermission") = 0 Then
                        dRow("UserPermission") = "Role based"
                    Else
                        dRow("UserPermission") = "User based"
                    End If
                End If
                dRow("Zone") = ""
                dRow("Region") = ""
                dRow("Area") = ""
                dRow("Branch") = ""
                If IsDBNull(dtDetails.Rows(i)("usr_Node")) = False And IsDBNull(dtDetails.Rows(i)("usr_orgnid")) = False Then
                    Dim dtGDDeatils As New DataTable, dtGD As New DataTable
                    Dim iAreaID As Integer, iRegionID As Integer, iZoneID As Integer

                    Dim DVZRBADetails As New DataView(dtZoneRegionBranchAreaDetails)
                    DVZRBADetails.RowFilter = "Org_Node=" & dtDetails.Rows(i)("usr_orgnid") & " And Org_levelCode=" & dtDetails.Rows(i)("usr_node") & ""
                    dtGDDeatils = DVZRBADetails.ToTable

                    If dtGDDeatils.Rows.Count > 0 Then
                        If dtDetails.Rows(i)("usr_Node") = 4 Then
                            dRow("Branch") = dtGDDeatils.Rows(0)("Org_Name")
                            iAreaID = dtGDDeatils.Rows(0)("Org_Parent")
                            dtGD = Nothing
                            DVZRBADetails.RowFilter = "Org_Node=" & iAreaID & " And Org_levelCode=3"
                            dtGD = DVZRBADetails.ToTable
                            If dtGD.Rows.Count > 0 Then
                                dRow("Area") = dtGD.Rows(0)("Org_Name")
                                iRegionID = dtGD.Rows(0)("Org_Parent")
                                dtGD = Nothing
                                DVZRBADetails.RowFilter = "Org_Node=" & iRegionID & " And Org_levelCode=2"
                                dtGD = DVZRBADetails.ToTable
                                If dtGD.Rows.Count > 0 Then
                                    dRow("Region") = dtGD.Rows(0)("Org_Name")
                                    iZoneID = dtGD.Rows(0)("Org_Parent")
                                    dtGD = Nothing
                                    DVZRBADetails.RowFilter = "Org_Node=" & iZoneID & " And Org_levelCode=1"
                                    dtGD = DVZRBADetails.ToTable
                                    If dtGD.Rows.Count > 0 Then
                                        dRow("Zone") = dtGD.Rows(0)("Org_Name")
                                    End If
                                End If
                            End If
                        End If

                        If dtDetails.Rows(i)("usr_Node") = 3 Then
                            If dtGDDeatils.Rows.Count > 0 Then
                                dRow("Area") = dtGDDeatils.Rows(0)("Org_Name")
                                iRegionID = dtGDDeatils.Rows(0)("Org_Parent")
                                dtGD = Nothing
                                DVZRBADetails.RowFilter = "Org_Node=" & iRegionID & " And Org_levelCode=2"
                                dtGD = DVZRBADetails.ToTable
                                If dtGD.Rows.Count > 0 Then
                                    dRow("Region") = dtGD.Rows(0)("Org_Name")
                                    iZoneID = dtGD.Rows(0)("Org_Parent")
                                    dtGD = Nothing
                                    DVZRBADetails.RowFilter = "Org_Node=" & iZoneID & " And Org_levelCode=1"
                                    dtGD = DVZRBADetails.ToTable
                                    If dtGD.Rows.Count > 0 Then
                                        dRow("Zone") = dtGD.Rows(0)("Org_Name")
                                    End If
                                End If
                            End If
                        End If

                        If dtDetails.Rows(i)("usr_Node") = 2 Then
                            If dtGDDeatils.Rows.Count > 0 Then
                                dRow("Region") = dtGDDeatils.Rows(0)("Org_Name")
                                iZoneID = dtGDDeatils.Rows(0)("Org_Parent")
                                dtGD = Nothing
                                DVZRBADetails.RowFilter = "Org_Node=" & iZoneID & " And Org_levelCode=1"
                                dtGD = DVZRBADetails.ToTable
                                If dtGD.Rows.Count > 0 Then
                                    dRow("Zone") = dtGD.Rows(0)("Org_Name")
                                End If
                            End If
                        End If

                        If dtDetails.Rows(i)("usr_Node") = 1 Then
                            dRow("Zone") = dtGDDeatils.Rows(0)("Org_Name")
                        End If
                    End If
                End If

                sModule = "" : sRole = ""
                If IsDBNull(dtDetails.Rows(i)("usr_LevelGrp")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("Usr_MasterModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_MasterModule") = 1) Then
                            sModule = "Master,"
                            sRole = dtDetails.Rows(i)("MasterRole") & ", "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_AuditModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_AuditModule") = 1) Then
                            sModule = sModule & "Audit, "
                            sRole = sRole & dtDetails.Rows(i)("AuditRole") & ", "
                        End If
                    End If
                    sModule = sModule.Trim
                    sRole = sRole.Trim
                    If sModule.EndsWith(",") Then
                        sModule = sModule.Remove(Len(sModule) - 1, 1)
                    End If
                    If sRole.EndsWith(",") Then
                        sRole = sRole.Remove(Len(sRole) - 1, 1)
                    End If
                End If
                dRow("Module") = sModule
                dRow("Role") = sRole
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneRegionAreaBranch(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Org_Node,Org_Name,Org_Parent,Org_levelCode from sad_org_Structure where Org_CompID=" & iACID & " and Org_levelCode <> ''"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class