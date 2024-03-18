Public Structure AllSession
    Private iAccessCodeID As Integer
    Private sAccessCode As String
    Private sEncryptPassword As String
    Private iYearID As Integer
    Private sYearName As String
    Private iUserID As Integer
    Private sUserFullName As String
    Private sUserLoginName As String
    Private sUserFullNameCode As String
    Private sLastLoginDate As String
    Private sIPAddress As String
    Private iFileSize As Integer
    Private sTimeOut As String
    Private sTimeOutWarning As String
    Private iScreenWidth As Integer
    Private iScreenHeight As Integer
    Private sModules As String
    Private sMenu As String
    Private sSubMenu As String
    Private sForm As String
    Private iNoOfUnSucsfAtteptts As Integer
    Private iMinPasswordCharacter As Integer
    Private iMaxPasswordCharacter As Integer
    Private iAuditCodeID As Integer
    Private iCustomerID As Integer
    Private iUserLoginLogPKID As Integer
    Private iUserLoginCustID As Integer
    Private dPandLAmount As String
    Private iUsrDeptID As Integer
    Private iUsrCompanyID As Integer

    Public dtCab, dtSubCab, dtFol, dtDocType, dtKeyWord, dtDesc, dtFormat, dtUsers, dtDocSearchReult, dtDocoImageViewID As DataTable
    Public dtCrit, dtSrchResult As DataTable
    Public sSelId As String
    Public sSelName As String
    Public sSelectedIndex As String
    Public sSelectDoc As String
    Private iAUD_ID As Integer
    Private iMonth_ID As Integer
    Private iSec_ID As Integer

    Private dStartDate As String
    Private dEndDate As String

    Private sFileInDB As String
    Private sScanPath As String
    Private sImagePath As String
    Private sWebImgPath As String
    Private sOutlookEMail As String
    Private sErrorLog As String
    Private sTypeOfImage As String
    Private sImageFormat As String
    Private sResolution As String
    Private sBrowserName As String
    Private iScheduleYearId As Integer
    Private iScheduleBranchId As Integer
    Private iNumberOfUsers As Integer
    Private iNumberOfCustomers As Integer
    Public Property ScheduleYearId() As Integer
        Get
            Return (iScheduleYearId)
        End Get
        Set(ByVal Value As Integer)
            iScheduleYearId = Value
        End Set
    End Property
    Public Property ScheduleBranchId() As Integer
        Get
            Return (iScheduleBranchId)
        End Get
        Set(ByVal Value As Integer)
            iScheduleBranchId = Value
        End Set
    End Property

    Public Property BrowserName() As String
        Get
            Return (sBrowserName)
        End Get
        Set(ByVal Value As String)
            sBrowserName = Value
        End Set
    End Property
    Public Property FileInDB() As String
        Get
            Return (sFileInDB)
        End Get
        Set(ByVal Value As String)
            sFileInDB = Value
        End Set
    End Property

    Public Property ScanPath() As String
        Get
            Return (sScanPath)
        End Get
        Set(ByVal Value As String)
            sScanPath = Value
        End Set
    End Property
    Public Property ImagePath() As String
        Get
            Return (sImagePath)
        End Get
        Set(ByVal Value As String)
            sImagePath = Value
        End Set
    End Property
    Public Property WebImgPath() As String
        Get
            Return (sWebImgPath)
        End Get
        Set(ByVal Value As String)
            sWebImgPath = Value
        End Set
    End Property
    Public Property OutlookEMail() As String
        Get
            Return (sOutlookEMail)
        End Get
        Set(ByVal Value As String)
            sOutlookEMail = Value
        End Set
    End Property

    Public Property ErrorLog() As String
        Get
            Return (sErrorLog)
        End Get
        Set(ByVal Value As String)
            sErrorLog = Value
        End Set
    End Property
    Public Property TypeOfImage() As String
        Get
            Return (sTypeOfImage)
        End Get
        Set(ByVal Value As String)
            sTypeOfImage = Value
        End Set
    End Property
    Public Property ImageFormat() As String
        Get
            Return (sImageFormat)
        End Get
        Set(ByVal Value As String)
            sImageFormat = Value
        End Set
    End Property
    Public Property Resolution() As String
        Get
            Return (sResolution)
        End Get
        Set(ByVal Value As String)
            sResolution = Value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return (dStartDate)
        End Get
        Set(ByVal Value As String)
            dStartDate = Value
        End Set
    End Property
    Public Property PandLAmount() As String
        Get
            Return (dPandLAmount)
        End Get
        Set(ByVal Value As String)
            dPandLAmount = Value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return (dEndDate)
        End Get
        Set(ByVal Value As String)
            dEndDate = Value
        End Set
    End Property

    Public Property AccessCodeID() As Integer
        Get
            Return (iAccessCodeID)
        End Get
        Set(ByVal Value As Integer)
            iAccessCodeID = Value
        End Set
    End Property
    Public Property AccessCode() As String
        Get
            Return (sAccessCode)
        End Get
        Set(ByVal Value As String)
            sAccessCode = Value
        End Set
    End Property
    Public Property EncryptPassword() As String
        Get
            Return (sEncryptPassword)
        End Get
        Set(ByVal Value As String)
            sEncryptPassword = Value
        End Set
    End Property
    Public Property YearID() As Integer
        Get
            Return (iYearID)
        End Get
        Set(ByVal Value As Integer)
            iYearID = Value
        End Set
    End Property
    Public Property YearName() As String
        Get
            Return (sYearName)
        End Get
        Set(ByVal Value As String)
            sYearName = Value
        End Set
    End Property
    Public Property UserID() As Integer
        Get
            Return (iUserID)
        End Get
        Set(ByVal Value As Integer)
            iUserID = Value
        End Set
    End Property
    Public Property UserFullName() As String
        Get
            Return (sUserFullName)
        End Get
        Set(ByVal Value As String)
            sUserFullName = Value
        End Set
    End Property
    Public Property UserLoginName() As String
        Get
            Return (sUserLoginName)
        End Get
        Set(ByVal Value As String)
            sUserLoginName = Value
        End Set
    End Property
    Public Property UserFullNameCode() As String
        Get
            Return (sUserFullNameCode)
        End Get
        Set(ByVal Value As String)
            sUserFullNameCode = Value
        End Set
    End Property
    Public Property LastLoginDate() As String
        Get
            Return (sLastLoginDate)
        End Get
        Set(ByVal Value As String)
            sLastLoginDate = Value
        End Set
    End Property
    Public Property IPAddress() As String
        Get
            Return (sIPAddress)
        End Get
        Set(ByVal Value As String)
            sIPAddress = Value
        End Set
    End Property
    Public Property FileSize() As Integer
        Get
            Return (iFileSize)
        End Get
        Set(ByVal Value As Integer)
            iFileSize = Value
        End Set
    End Property
    Public Property TimeOut() As String
        Get
            Return (sTimeOut)
        End Get
        Set(ByVal Value As String)
            sTimeOut = Value
        End Set
    End Property
    Public Property TimeOutWarning() As String
        Get
            Return (sTimeOutWarning)
        End Get
        Set(ByVal Value As String)
            sTimeOutWarning = Value
        End Set
    End Property
    Public Property ScreenWidth() As Integer
        Get
            Return (iScreenWidth)
        End Get
        Set(ByVal Value As Integer)
            iScreenWidth = Value
        End Set
    End Property
    Public Property ScreenHeight() As Integer
        Get
            Return (iScreenHeight)
        End Get
        Set(ByVal Value As Integer)
            iScreenHeight = Value
        End Set
    End Property
    Public Property Modules() As String
        Get
            Return (sModules)
        End Get
        Set(ByVal Value As String)
            sModules = Value
        End Set
    End Property
    Public Property Menu() As String
        Get
            Return (sMenu)
        End Get
        Set(ByVal Value As String)
            sMenu = Value
        End Set
    End Property
    Public Property SubMenu() As String
        Get
            Return (sSubMenu)
        End Get
        Set(ByVal Value As String)
            sSubMenu = Value
        End Set
    End Property
    Public Property Form() As String
        Get
            Return (sForm)
        End Get
        Set(ByVal Value As String)
            sForm = Value
        End Set
    End Property
    Public Property NoOfUnSucsfAtteptts() As Integer
        Get
            Return (iNoOfUnSucsfAtteptts)
        End Get
        Set(ByVal Value As Integer)
            iNoOfUnSucsfAtteptts = Value
        End Set
    End Property
    Public Property MaxPasswordCharacter() As Integer
        Get
            Return (iMaxPasswordCharacter)
        End Get
        Set(ByVal Value As Integer)
            iMaxPasswordCharacter = Value
        End Set
    End Property
    Public Property MinPasswordCharacter() As Integer
        Get
            Return (iMinPasswordCharacter)
        End Get
        Set(ByVal Value As Integer)
            iMinPasswordCharacter = Value
        End Set
    End Property
    Public Property AuditCodeID() As Integer
        Get
            Return (iAuditCodeID)
        End Get
        Set(ByVal Value As Integer)
            iAuditCodeID = Value
        End Set
    End Property
    Public Property CustomerID() As Integer
        Get
            Return (iCustomerID)
        End Get
        Set(ByVal Value As Integer)
            iCustomerID = Value
        End Set
    End Property
    'Audit
    Public Property AUDID() As Integer
        Get
            Return (iAUD_ID)
        End Get
        Set(ByVal Value As Integer)
            iAUD_ID = Value
        End Set
    End Property
    Public Property SectionID() As Integer
        Get
            Return (iSec_ID)
        End Get
        Set(ByVal Value As Integer)
            iSec_ID = Value
        End Set
    End Property
    Public Property SelectedMonthID() As Integer
        Get
            Return (iMonth_ID)
        End Get
        Set(ByVal Value As Integer)
            iMonth_ID = Value
        End Set
    End Property

    Public Property UserLoginLogPKID() As Integer
        Get
            Return (iUserLoginLogPKID)
        End Get
        Set(ByVal Value As Integer)
            iUserLoginLogPKID = Value
        End Set
    End Property
    Public Property UserLoginCustID() As Integer
        Get
            Return (iUserLoginCustID)
        End Get
        Set(ByVal Value As Integer)
            iUserLoginCustID = Value
        End Set
    End Property
    Public Property UsrDeptID() As Integer
        Get
            Return (iUsrDeptID)
        End Get
        Set(ByVal Value As Integer)
            iUsrDeptID = Value
        End Set
    End Property
    Public Property UsrCompanyID() As Integer
        Get
            Return (iUsrCompanyID)
        End Get
        Set(ByVal Value As Integer)
            iUsrCompanyID = Value
        End Set
    End Property
    Public Property NumberOfUsers() As Integer
        Get
            Return (iNumberOfUsers)
        End Get
        Set(ByVal Value As Integer)
            iNumberOfUsers = Value
        End Set
    End Property
    Public Property NumberOfCustomers() As Integer
        Get
            Return (iNumberOfCustomers)
        End Get
        Set(ByVal Value As Integer)
            iNumberOfCustomers = Value
        End Set
    End Property
End Structure

