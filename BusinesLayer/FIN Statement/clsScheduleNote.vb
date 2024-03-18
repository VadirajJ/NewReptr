Public Class clsScheduleNote
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsUSEntry As New clsUploadStockEntry

    Private iASHN_ID As Integer
    Private iASHN_SubHeadingId As Integer
    Private iASHN_CustomerId As Integer
    Private sASHN_Description As String
    Private sASHN_DelFlag As String
    Private sASHN_Status As String
    Private iASHN_CreatedBy As Integer
    Private dASHN_CreatedOn As DateTime
    Private iASHN_UpdatedBy As Integer
    Private dASHN_UpdatedOn As DateTime
    Private iASHN_ApprovedBy As Integer
    Private dASHN_ApprovedOn As DateTime
    Private iASHN_CompID As Integer
    Private iASHN_YearID As Integer
    Private sASHN_IPAddress As String
    Private sASHN_Operation As String
    Public Property ASHN_ID() As Integer
        Get
            Return (iASHN_ID)
        End Get
        Set(ByVal Value As Integer)
            iASHN_ID = Value
        End Set
    End Property
    Public Property ASHN_SubHeadingId() As Integer
        Get
            Return (iASHN_SubHeadingId)
        End Get
        Set(ByVal Value As Integer)
            iASHN_SubHeadingId = Value
        End Set
    End Property
    Public Property ASHN_CustomerId() As Integer
        Get
            Return (iASHN_CustomerId)
        End Get
        Set(ByVal Value As Integer)
            iASHN_CustomerId = Value
        End Set
    End Property
    Public Property ASHN_Description() As String
        Get
            Return (sASHN_Description)
        End Get
        Set(ByVal Value As String)
            sASHN_Description = Value
        End Set
    End Property
    Public Property ASHN_DelFlag() As String
        Get
            Return (sASHN_DelFlag)
        End Get
        Set(ByVal Value As String)
            sASHN_DelFlag = Value
        End Set
    End Property
    Public Property ASHN_Status() As String
        Get
            Return (sASHN_Status)
        End Get
        Set(ByVal Value As String)
            sASHN_Status = Value
        End Set
    End Property
    Public Property ASHN_CreatedBy() As Integer
        Get
            Return (iASHN_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iASHN_CreatedBy = Value
        End Set
    End Property
    Public Property ASHN_CreatedOn() As Date
        Get
            Return (dASHN_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dASHN_CreatedOn = Value
        End Set
    End Property
    Public Property ASHN_UpdatedBy() As Integer
        Get
            Return (iASHN_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iASHN_UpdatedBy = Value
        End Set
    End Property

    Public Property ASHN_UpdatedOn() As Date
        Get
            Return (dASHN_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dASHN_UpdatedOn = Value
        End Set
    End Property
    Public Property ASHN_ApprovedBy() As Integer
        Get
            Return (iASHN_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iASHN_ApprovedBy = Value
        End Set
    End Property
    Public Property ASHN_ApprovedOn() As Date
        Get
            Return (dASHN_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dASHN_ApprovedOn = Value
        End Set
    End Property

    Public Property ASHN_CompID() As Integer
        Get
            Return (iASHN_CompID)
        End Get
        Set(ByVal Value As Integer)
            iASHN_CompID = Value
        End Set
    End Property
    Public Property ASHN_YearID() As Integer
        Get
            Return (iASHN_YearID)
        End Get
        Set(ByVal Value As Integer)
            iASHN_YearID = Value
        End Set
    End Property
    Public Property ASHN_IPAddress() As String
        Get
            Return (sASHN_IPAddress)
        End Get
        Set(ByVal Value As String)
            sASHN_IPAddress = Value
        End Set
    End Property
    Public Property ASHN_Operation() As String
        Get
            Return (sASHN_Operation)
        End Get
        Set(ByVal Value As String)
            sASHN_Operation = Value
        End Set
    End Property


    'First- Schedule

    Private iSNF_ID As Integer
    Private iSNF_CustId As Integer
    Private sSNF_Description As String
    Private sSNF_Category As String
    Private dSNF_CYear_Amount As Double
    Private dSNF_PYear_Amount As Double
    Private iSNF_YearID As Integer
    Private iSNF_CompID As Integer
    Private sSNF_Status As String
    Private sSNF_DelFlag As String
    Private dSNF_CrOn As DateTime
    Private iSNF_CrBy As Integer
    Private iSNF_UpdatedBy As Integer
    Private dSNF_UpdatedOn As DateTime
    Private sSNF_IPAddress As String
    Public Property SNF_ID() As Integer
        Get
            Return (iSNF_ID)
        End Get
        Set(ByVal Value As Integer)
            iSNF_ID = Value
        End Set
    End Property
    Public Property SNF_CustId() As Integer
        Get
            Return (iSNF_CustId)
        End Get
        Set(ByVal Value As Integer)
            iSNF_CustId = Value
        End Set
    End Property
    Public Property SNF_Description() As String
        Get
            Return (sSNF_Description)
        End Get
        Set(ByVal Value As String)
            sSNF_Description = Value
        End Set
    End Property
    Public Property SNF_Category() As String
        Get
            Return (sSNF_Category)
        End Get
        Set(ByVal Value As String)
            sSNF_Category = Value
        End Set
    End Property
    Public Property SNF_CYear_Amount() As Double
        Get
            Return (dSNF_CYear_Amount)
        End Get
        Set(ByVal Value As Double)
            dSNF_CYear_Amount = Value
        End Set
    End Property
    Public Property SNF_pYear_Amount() As Double
        Get
            Return (dSNF_PYear_Amount)
        End Get
        Set(ByVal Value As Double)
            dSNF_PYear_Amount = Value
        End Set
    End Property
    Public Property SNF_YearID() As Integer
        Get
            Return (iSNF_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSNF_YearID = Value
        End Set
    End Property
    Public Property SNF_CompID() As Integer
        Get
            Return (iSNF_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSNF_CompID = Value
        End Set
    End Property
    Public Property SNF_Status() As String
        Get
            Return (sSNF_Status)
        End Get
        Set(ByVal Value As String)
            sSNF_Status = Value
        End Set
    End Property
    Public Property SNF_DelFlag() As String
        Get
            Return (sSNF_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSNF_DelFlag = Value
        End Set
    End Property
    Public Property SNF_CRON() As Date
        Get
            Return (dSNF_CRON)
        End Get
        Set(ByVal Value As Date)
            dSNF_CRON = Value
        End Set
    End Property
    Public Property SNF_CrBy() As Integer
        Get
            Return (iSNF_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iSNF_CrBy = Value
        End Set
    End Property
    Public Property SNF_UpdatedOn() As Date
        Get
            Return (dSNF_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dSNF_UpdatedOn = Value
        End Set
    End Property
    Public Property SNF_UpdatedBy() As Integer
        Get
            Return (iSNF_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSNF_UpdatedBy = Value
        End Set
    End Property
    Public Property SNF_IPAddress() As String
        Get
            Return (sSNF_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSNF_IPAddress = Value
        End Set
    End Property

    'Second- Schedule

    Private iSNS_ID As Integer
    Private iSNS_CustId As Integer
    Private sSNS_Description As String
    Private sSNS_Category As String
    Private dSNS_CYear_BegShares As Double
    Private dSNS_CYear_BegAmount As Double
    Private dSNS_PYear_BegShares As Double
    Private dSNS_PYear_BegAmount As Double
    Private dSNS_CYear_AddShares As Double
    Private dSNS_CYear_AddAmount As Double
    Private dSNS_PYear_AddShares As Double
    Private dSNS_PYear_AddAmount As Double
    Private dSNS_CYear_EndShares As Double
    Private dSNS_CYear_EndAmount As Double
    Private dSNS_PYear_EndShares As Double
    Private dSNS_PYear_EndAmount As Double
    Private iSNS_YearID As Integer
    Private iSNS_CompID As Integer
    Private sSNS_Status As String
    Private sSNS_DelFlag As String
    Private dSNS_CrOn As DateTime
    Private iSNS_CrBy As Integer
    Private iSNS_UpdatedBy As Integer
    Private dSNS_UpdatedOn As DateTime
    Private sSNS_IPAddress As String
    Public Property SNS_ID() As Integer
        Get
            Return (iSNS_ID)
        End Get
        Set(ByVal Value As Integer)
            iSNS_ID = Value
        End Set
    End Property
    Public Property SNS_CustId() As Integer
        Get
            Return (iSNS_CustId)
        End Get
        Set(ByVal Value As Integer)
            iSNS_CustId = Value
        End Set
    End Property
    Public Property SNS_Description() As String
        Get
            Return (sSNS_Description)
        End Get
        Set(ByVal Value As String)
            sSNS_Description = Value
        End Set
    End Property
    Public Property SNS_Category() As String
        Get
            Return (sSNS_Category)
        End Get
        Set(ByVal Value As String)
            sSNS_Category = Value
        End Set
    End Property
    Public Property SNS_CYear_BegShares() As Double
        Get
            Return (dSNS_CYear_BegShares)
        End Get
        Set(ByVal Value As Double)
            dSNS_CYear_BegShares = Value
        End Set
    End Property
    Public Property SNS_CYear_BegAmount() As Double
        Get
            Return (dSNS_CYear_BegAmount)
        End Get
        Set(ByVal Value As Double)
            dSNS_CYear_BegAmount = Value
        End Set
    End Property
    Public Property SNS_PYear_BegShares() As Double
        Get
            Return (dSNS_PYear_BegShares)
        End Get
        Set(ByVal Value As Double)
            dSNS_PYear_BegShares = Value
        End Set
    End Property
    Public Property SNS_pYear_BegAmount() As Double
        Get
            Return (dSNS_PYear_BegAmount)
        End Get
        Set(ByVal Value As Double)
            dSNS_PYear_BegAmount = Value
        End Set
    End Property
    Public Property SNS_CYear_AddShares() As Double
        Get
            Return (dSNS_CYear_AddShares)
        End Get
        Set(ByVal Value As Double)
            dSNS_CYear_AddShares = Value
        End Set
    End Property
    Public Property SNS_CYear_AddAmount() As Double
        Get
            Return (dSNS_CYear_AddAmount)
        End Get
        Set(ByVal Value As Double)
            dSNS_CYear_AddAmount = Value
        End Set
    End Property
    Public Property SNS_PYear_AddShares() As Double
        Get
            Return (dSNS_PYear_AddShares)
        End Get
        Set(ByVal Value As Double)
            dSNS_PYear_AddShares = Value
        End Set
    End Property
    Public Property SNS_pYear_AddAmount() As Double
        Get
            Return (dSNS_PYear_AddAmount)
        End Get
        Set(ByVal Value As Double)
            dSNS_PYear_AddAmount = Value
        End Set
    End Property
    Public Property SNS_CYear_EndShares() As Double
        Get
            Return (dSNS_CYear_EndShares)
        End Get
        Set(ByVal Value As Double)
            dSNS_CYear_EndShares = Value
        End Set
    End Property
    Public Property SNS_CYear_EndAmount() As Double
        Get
            Return (dSNS_CYear_EndAmount)
        End Get
        Set(ByVal Value As Double)
            dSNS_CYear_EndAmount = Value
        End Set
    End Property
    Public Property SNS_PYear_EndShares() As Double
        Get
            Return (dSNS_PYear_EndShares)
        End Get
        Set(ByVal Value As Double)
            dSNS_PYear_EndShares = Value
        End Set
    End Property
    Public Property SNS_pYear_EndAmount() As Double
        Get
            Return (dSNS_PYear_EndAmount)
        End Get
        Set(ByVal Value As Double)
            dSNS_PYear_EndAmount = Value
        End Set
    End Property
    Public Property SNS_YearID() As Integer
        Get
            Return (iSNS_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSNS_YearID = Value
        End Set
    End Property
    Public Property SNS_CompID() As Integer
        Get
            Return (iSNS_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSNS_CompID = Value
        End Set
    End Property
    Public Property SNS_Status() As String
        Get
            Return (sSNS_Status)
        End Get
        Set(ByVal Value As String)
            sSNS_Status = Value
        End Set
    End Property
    Public Property SNS_DelFlag() As String
        Get
            Return (sSNS_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSNS_DelFlag = Value
        End Set
    End Property
    Public Property SNS_CRON() As Date
        Get
            Return (dSNS_CrOn)
        End Get
        Set(ByVal Value As Date)
            dSNS_CrOn = Value
        End Set
    End Property
    Public Property SNS_CrBy() As Integer
        Get
            Return (iSNS_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iSNS_CrBy = Value
        End Set
    End Property
    Public Property SNS_UpdatedOn() As Date
        Get
            Return (dSNS_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dSNS_UpdatedOn = Value
        End Set
    End Property
    Public Property SNS_UpdatedBy() As Integer
        Get
            Return (iSNS_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSNS_UpdatedBy = Value
        End Set
    End Property
    Public Property SNS_IPAddress() As String
        Get
            Return (sSNS_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSNS_IPAddress = Value
        End Set
    End Property

    'Third  module

    Private iSNT_ID As Integer
    Private iSNT_CustId As Integer
    Private sSNT_Description As String
    Private sSNT_Category As String
    Private dSNT_CYear_Shares As Double
    Private dSNT_CYear_Amount As Double
    Private dSNT_PYear_Shares As Double
    Private dSNT_PYear_Amount As Double
    Private iSNT_YearID As Integer
    Private iSNT_CompID As Integer
    Private sSNT_Status As String
    Private sSNT_DelFlag As String
    Private dSNT_CrOn As DateTime
    Private iSNT_CrBy As Integer
    Private iSNT_UpdatedBy As Integer
    Private dSNT_UpdatedOn As DateTime
    Private sSNT_IPAddress As String
    Public Property SNT_ID() As Integer
        Get
            Return (iSNT_ID)
        End Get
        Set(ByVal Value As Integer)
            iSNT_ID = Value
        End Set
    End Property
    Public Property SNT_CustId() As Integer
        Get
            Return (iSNT_CustId)
        End Get
        Set(ByVal Value As Integer)
            iSNT_CustId = Value
        End Set
    End Property
    Public Property SNT_Description() As String
        Get
            Return (sSNT_Description)
        End Get
        Set(ByVal Value As String)
            sSNT_Description = Value
        End Set
    End Property
    Public Property SNT_Category() As String
        Get
            Return (sSNT_Category)
        End Get
        Set(ByVal Value As String)
            sSNT_Category = Value
        End Set
    End Property
    Public Property SNT_CYear_Shares() As Double
        Get
            Return (dSNT_CYear_Shares)
        End Get
        Set(ByVal Value As Double)
            dSNT_CYear_Shares = Value
        End Set
    End Property
    Public Property SNT_CYear_Amount() As Double
        Get
            Return (dSNT_CYear_Amount)
        End Get
        Set(ByVal Value As Double)
            dSNT_CYear_Amount = Value
        End Set
    End Property
    Public Property SNT_pYear_Shares() As Double
        Get
            Return (dSNT_PYear_Shares)
        End Get
        Set(ByVal Value As Double)
            dSNT_PYear_Shares = Value
        End Set
    End Property
    Public Property SNT_pYear_Amount() As Double
        Get
            Return (dSNT_PYear_Amount)
        End Get
        Set(ByVal Value As Double)
            dSNT_PYear_Amount = Value
        End Set
    End Property
    Public Property SNT_YearID() As Integer
        Get
            Return (iSNT_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSNT_YearID = Value
        End Set
    End Property
    Public Property SNT_CompID() As Integer
        Get
            Return (iSNT_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSNT_CompID = Value
        End Set
    End Property
    Public Property SNT_Status() As String
        Get
            Return (sSNT_Status)
        End Get
        Set(ByVal Value As String)
            sSNT_Status = Value
        End Set
    End Property
    Public Property SNT_DelFlag() As String
        Get
            Return (sSNT_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSNT_DelFlag = Value
        End Set
    End Property
    Public Property SNT_CRON() As Date
        Get
            Return (dSNT_CrOn)
        End Get
        Set(ByVal Value As Date)
            dSNT_CrOn = Value
        End Set
    End Property
    Public Property SNT_CrBy() As Integer
        Get
            Return (iSNT_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iSNT_CrBy = Value
        End Set
    End Property
    Public Property SNT_UpdatedOn() As Date
        Get
            Return (dSNT_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dSNT_UpdatedOn = Value
        End Set
    End Property
    Public Property SNT_UpdatedBy() As Integer
        Get
            Return (iSNT_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSNT_UpdatedBy = Value
        End Set
    End Property
    Public Property SNT_IPAddress() As String
        Get
            Return (sSNT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSNT_IPAddress = Value
        End Set
    End Property

    'Schedule Description

    Private iSND_ID As Integer
    Private iSND_CustId As Integer
    Private sSND_Description As String
    Private sSND_Category As String
    Private iSND_YearID As Integer
    Private iSND_CompID As Integer
    Private sSND_Status As String
    Private sSND_DelFlag As String
    Private dSND_CrOn As DateTime
    Private iSND_CrBy As Integer
    Private iSND_UpdatedBy As Integer
    Private dSND_UpdatedOn As DateTime
    Private sSND_IPAddress As String
    Public Property SND_ID() As Integer
        Get
            Return (iSND_ID)
        End Get
        Set(ByVal Value As Integer)
            iSND_ID = Value
        End Set
    End Property
    Public Property SND_CustId() As Integer
        Get
            Return (iSND_CustId)
        End Get
        Set(ByVal Value As Integer)
            iSND_CustId = Value
        End Set
    End Property
    Public Property SND_Description() As String
        Get
            Return (sSND_Description)
        End Get
        Set(ByVal Value As String)
            sSND_Description = Value
        End Set
    End Property
    Public Property SND_Category() As String
        Get
            Return (sSND_Category)
        End Get
        Set(ByVal Value As String)
            sSND_Category = Value
        End Set
    End Property
    Public Property SND_YearID() As Integer
        Get
            Return (iSND_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSND_YearID = Value
        End Set
    End Property
    Public Property SND_CompID() As Integer
        Get
            Return (iSND_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSND_CompID = Value
        End Set
    End Property
    Public Property SND_Status() As String
        Get
            Return (sSND_Status)
        End Get
        Set(ByVal Value As String)
            sSND_Status = Value
        End Set
    End Property
    Public Property SND_DelFlag() As String
        Get
            Return (sSND_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSND_DelFlag = Value
        End Set
    End Property
    Public Property SND_CRON() As Date
        Get
            Return (dSND_CrOn)
        End Get
        Set(ByVal Value As Date)
            dSND_CrOn = Value
        End Set
    End Property
    Public Property SND_CrBy() As Integer
        Get
            Return (iSND_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iSND_CrBy = Value
        End Set
    End Property
    Public Property SND_UpdatedOn() As Date
        Get
            Return (dSND_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dSND_UpdatedOn = Value
        End Set
    End Property
    Public Property SND_UpdatedBy() As Integer
        Get
            Return (iSND_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSND_UpdatedBy = Value
        End Set
    End Property
    Public Property SND_IPAddress() As String
        Get
            Return (sSND_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSND_IPAddress = Value
        End Set
    End Property


    'Fourth Schedule

    Private iSNFT_ID As Integer
    Private iSNFT_CustId As Integer
    Private sSNFT_Description As String
    Private sSNFT_Category As String
    Private dSNFT_NumShares As Double
    Private dSNFT_TotalShares As Double
    Private dSNFT_ChangedShares As Double
    Private iSNFT_YearID As Integer
    Private iSNFT_CompID As Integer
    Private sSNFT_Status As String
    Private sSNFT_DelFlag As String
    Private dSNFT_CrOn As DateTime
    Private iSNFT_CrBy As Integer
    Private iSNFT_UpdatedBy As Integer
    Private dSNFT_UpdatedOn As DateTime
    Private sSNFT_IPAddress As String
    Public Property SNFT_ID() As Integer
        Get
            Return (iSNFT_ID)
        End Get
        Set(ByVal Value As Integer)
            iSNFT_ID = Value
        End Set
    End Property
    Public Property SNFT_CustId() As Integer
        Get
            Return (iSNFT_CustId)
        End Get
        Set(ByVal Value As Integer)
            iSNFT_CustId = Value
        End Set
    End Property
    Public Property SNFT_Description() As String
        Get
            Return (sSNFT_Description)
        End Get
        Set(ByVal Value As String)
            sSNFT_Description = Value
        End Set
    End Property
    Public Property SNFT_Category() As String
        Get
            Return (sSNFT_Category)
        End Get
        Set(ByVal Value As String)
            sSND_Category = Value
        End Set
    End Property
    Public Property SNFT_NumShares() As Double
        Get
            Return (dSNFT_NumShares)
        End Get
        Set(ByVal Value As Double)
            dSNFT_NumShares = Value
        End Set
    End Property
    Public Property SNFT_TotalShares() As Double
        Get
            Return (dSNFT_TotalShares)
        End Get
        Set(ByVal Value As Double)
            dSNFT_TotalShares = Value
        End Set
    End Property
    Public Property SNFT_ChangedShares() As Double
        Get
            Return (dSNFT_ChangedShares)
        End Get
        Set(ByVal Value As Double)
            dSNFT_ChangedShares = Value
        End Set
    End Property
    Public Property SNFT_YearID() As Integer
        Get
            Return (iSNFT_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSNFT_YearID = Value
        End Set
    End Property
    Public Property SNFT_CompID() As Integer
        Get
            Return (iSNFT_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSNFT_CompID = Value
        End Set
    End Property
    Public Property SNFT_Status() As String
        Get
            Return (sSNFT_Status)
        End Get
        Set(ByVal Value As String)
            sSNFT_Status = Value
        End Set
    End Property
    Public Property SNFT_DelFlag() As String
        Get
            Return (sSNFT_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSNFT_DelFlag = Value
        End Set
    End Property
    Public Property SNFT_CRON() As Date
        Get
            Return (dSNFT_CrOn)
        End Get
        Set(ByVal Value As Date)
            dSNFT_CrOn = Value
        End Set
    End Property
    Public Property SNFT_CrBy() As Integer
        Get
            Return (iSNFT_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iSNFT_CrBy = Value
        End Set
    End Property
    Public Property SNFT_UpdatedOn() As Date
        Get
            Return (dSNFT_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dSNFT_UpdatedOn = Value
        End Set
    End Property
    Public Property SNFT_UpdatedBy() As Integer
        Get
            Return (iSNFT_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSNFT_UpdatedBy = Value
        End Set
    End Property
    Public Property SNFT_IPAddress() As String
        Get
            Return (sSNFT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSNFT_IPAddress = Value
        End Set
    End Property
    Public Function getSubHeadingDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer) As DataTable
        Dim sSql As String
        Dim dtsubheading As DataTable
        Dim orgTypeId As Integer
        Try
            orgTypeId = getOrgtype(sAC, iACID, ICustid)
            sSql = "select distinct(assh_id) as assh_id,  CONCAT(ASSH_Name , '-' , AsSh_Notes) as AsSh_Notes "
            sSql = sSql & "  from ACC_ScheduleSubHeading  "
            sSql = sSql & "  where AsSh_Notes <>0 and  EXISTS(SELECT * FROM ACC_ScheduleTemplates WHERE AST_subHeadingID = assh_id  and AST_Companytype=" & orgTypeId & " ) "
            dtsubheading = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtsubheading
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getNoteDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer) As DataTable
        Dim sSql As String
        Dim dtNote As DataTable

        Try
            sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, ASHN_ID as Id,  ASHN_Description as Description1,CONCAT(a.ASSH_Name , '-' , a.AsSh_Notes) as SubHeading  "
            sSql = sSql & "  from ACC_SubHeadingNoteDesc  "
            sSql = sSql & "  left join ACC_ScheduleSubHeading a on  a.ASSH_ID=ASHN_SubHeadingId "
            sSql = sSql & "    where  ASHN_CustomerId=" & ICustid & "  "
            dtNote = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSubHeadingDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal iSubHeadId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select   ASHN_Description as Description,ASHN_ID  "
            sSql = sSql & "  from ACC_SubHeadingNoteDesc  "
            sSql = sSql & "  left join ACC_ScheduleSubHeading a on  a.ASSH_ID=ASHN_SubHeadingId "
            sSql = sSql & "    where  ASHN_CustomerId=" & ICustid & " and ASHN_SubHeadingId =" & iSubHeadId & " "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getOrgtype(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustId As Integer)
        Dim sSql As String
        Dim OrgtypeId As Integer
        Try
            sSql = "select CUST_ORGTYPEID from SAD_CUSTOMER_MASTER where CUST_ID=" & iCustId & " and CUST_DELFLG= 'A'"
            OrgtypeId = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return OrgtypeId
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function saveNoticeBoard(ByVal sNameSpace As String, ByVal objclsSchduleNote As clsScheduleNote) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSchduleNote.iASHN_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_SubHeadingId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSchduleNote.iASHN_SubHeadingId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_CustomerId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSchduleNote.iASHN_CustomerId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_Description", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsSchduleNote.sASHN_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_DelFlag", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsSchduleNote.sASHN_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_Status", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsSchduleNote.sASHN_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_Operation", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsSchduleNote.sASHN_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSchduleNote.iASHN_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsSchduleNote.dASHN_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSchduleNote.iASHN_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_YearID", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objclsSchduleNote.iASHN_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASHN_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsSchduleNote.sASHN_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_SubHeadingNoteDesc", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveFirstScheduleNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsSchduleNote As clsScheduleNote) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            If objclsSchduleNote.SNF_ID <> 0 Then
                sSql = "update ScheduleNote_First set SNF_Description='" & (objclsSchduleNote.sSNF_Description) & "',SNF_CYear_Amount=" & objclsSchduleNote.dSNF_CYear_Amount & ", "
                sSql = sSql & "  SNF_PYear_Amount=" & (objclsSchduleNote.dSNF_PYear_Amount) & " where SNF_ID='" & (objclsSchduleNote.iSNF_ID) & "' and  SNF_Category= '" & objGen.SafeSQL(objclsSchduleNote.sSNF_Category) & "' and  SNF_CustId ='" & (objclsSchduleNote.iSNF_CustId) & "' "
                sSql = sSql & "  and SNF_YEARId ='" & (objclsSchduleNote.iSNF_YearID) & "'"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(SNF_ID)+1,1) from ScheduleNote_First")
                sSql = " Insert into ScheduleNote_First(SNF_ID,SNF_CustId,SNF_Description,SNF_Category,SNF_CYear_Amount,SNF_PYear_Amount,"
                sSql = sSql & " SNF_YEARId,SNF_CompId,"
                sSql = sSql & " SNF_STATUS,SNF_DELFLAG,SNF_CRON,SNF_CRBY,SNF_IPAddress"
                sSql = sSql & " ) values "
                sSql = sSql & "(" & iMax & ",'" & (objclsSchduleNote.iSNF_CustId) & "','" & (objclsSchduleNote.sSNF_Description) & "',"
                sSql = sSql & " '" & objGen.SafeSQL(objclsSchduleNote.sSNF_Category) & "', " & objclsSchduleNote.dSNF_CYear_Amount & ","
                sSql = sSql & " " & (objclsSchduleNote.dSNF_PYear_Amount) & ",'" & (objclsSchduleNote.iSNF_YearID) & "',"
                sSql = sSql & " " & (objclsSchduleNote.iSNF_CompID) & ", '" & (objclsSchduleNote.sSNF_Status) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.sSNF_DelFlag) & "' ," & (objclsSchduleNote.dSNF_CrOn) & ", '" & (objclsSchduleNote.iSNF_CrBy) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.SNF_IPAddress) & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadScheduleNoteFirstDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from CustomerSupplierMaster Where CSM_ID=" & iCSMid & "  And CSM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSchedFirstNoteAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataSet
        Dim sSql As String
        Dim dtNote As DataSet
        Try
            If sType = "" Then
                sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNF_ID as Id,  SNF_Description as Description,SNF_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNF_PYear_Amount as PYear_Amount from ScheduleNote_First  "
                sSql = sSql & "    where snf_yearid=" & IyearID & " and  SNF_CustId=" & ICustid & " and SNF_Category = 'AU' and snf_status<>'D'  ;"
                sSql = sSql & " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNF_ID as Id,  SNF_Description as Description,SNF_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNF_PYear_Amount as PYear_Amount from ScheduleNote_First  "
                sSql = sSql & "    where snf_yearid=" & IyearID & " and  SNF_CustId=" & ICustid & " and SNF_Category ='IS' and snf_status<>'D' ; "
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNF_ID as Id,  SNF_Description as Description,SNF_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNF_PYear_Amount as PYear_Amount from ScheduleNote_First  "
                sSql = sSql & "    where snf_yearid=" & IyearID & " and  SNF_CustId=" & ICustid & " and SNF_Category = 'AI' and snf_status<>'D' ;"
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNF_ID as Id,  SNF_Description as Description,SNF_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNF_PYear_Amount as PYear_Amount from ScheduleNote_First  "
                sSql = sSql & "    where snf_yearid=" & IyearID & " and  SNF_CustId=" & ICustid & " and SNF_Category ='BS' and snf_status<>'D'  ;"
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNF_ID as Id,  SNF_Description as Description,SNF_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNF_PYear_Amount as PYear_Amount from ScheduleNote_First  "
                sSql = sSql & "    where snf_yearid=" & IyearID & " and  SNF_CustId=" & ICustid & " and SNF_Category ='CC' and snf_status<>'D'  ;"
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNF_ID as Id,  SNF_Description as Description,SNF_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNF_PYear_Amount as PYear_Amount from ScheduleNote_First  "
                sSql = sSql & "    where snf_yearid=" & IyearID & " and  SNF_CustId=" & ICustid & " and SNF_Category ='FD' and snf_status<>'D'  ;"

                dtNote = objDBL.SQLExecuteDataSet(sAC, sSql)
                Return dtNote
            Else
                sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNF_ID as Id,  SNF_Description as Description,SNF_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNF_PYear_Amount as PYear_Amount from ScheduleNote_First  "
                sSql = sSql & "    where snf_yearid=" & IyearID & " and  SNF_CustId=" & ICustid & " and SNF_Category ='" & sType & "' and snf_status<>'D'  "
                dtNote = objDBL.SQLExecuteDataSet(sAC, sSql)
                Return dtNote
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSchedFirstNoteDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dtNote As DataTable
        Try
            sSql = "SELECT
                ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo,
                SNF_ID AS Id,
                SNF_Description AS Description,
                CONCAT('',REPLACE(CONVERT(VARCHAR,CAST(SNF_CYear_Amount AS money), 1), 'N','en-in')) AS CYear_Amount,
                CONCAT('',REPLACE(CONVERT(VARCHAR,CAST(SNF_PYear_Amount AS money), 1), 'N','en-in')) AS PYear_Amount
             FROM
                ScheduleNote_First
             WHERE
                SNF_CustId =" & ICustid & "
                AND SNF_Category = '" & sType & "'
                AND snf_status <> 'D';  "
            dtNote = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteSchedFirstNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTrID As Integer, ByVal iCustID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update ScheduleNote_First Set SNF_DelFlag='D' , SNF_Status='D' Where SNF_ID=" & iTrID & " And SNF_CustId=" & iCustID & " And SNF_CompID=" & iCompID & " And SNF_YearID = " & iYearId & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveSecondScheduleNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsSchduleNote As clsScheduleNote) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = " delete from ScheduleNote_Second where SNS_CustId=" & objclsSchduleNote.iSNS_CustId & " And SNS_YEARId=" & objclsSchduleNote.iSNS_YearID & " And SNS_Category='" & objclsSchduleNote.sSNS_Category & "' "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            sSql = ""
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(SNS_ID)+1,1) from ScheduleNote_Second ")
            sSql = " Insert into ScheduleNote_Second(SNS_ID,SNS_CustId,SNS_Description,SNS_Category,SNS_CYear_BegShares,SNS_CYear_BegAmount,"
            sSql = sSql & " SNS_PYear_BegShares,SNS_PYear_BegAmount,SNS_CYear_AddShares,SNS_CYear_AddAmount,SNS_PYear_AddShares,SNS_PYear_AddAmount,"
            sSql = sSql & " SNS_CYear_EndShares,SNS_CYear_EndAmount,SNS_PYear_EndShares,SNS_PYear_EndAmount, "
            sSql = sSql & " SNS_YEARId,SNS_CompId,"
            sSql = sSql & " SNS_STATUS,SNS_DELFLAG,SNS_CRON,SNS_CRBY,SNS_IPAddress "
            sSql = sSql & " ) values "
            sSql = sSql & "(" & iMax & ",'" & (objclsSchduleNote.iSNS_CustId) & "','" & (objclsSchduleNote.sSNS_Description) & "',"
            sSql = sSql & " '" & objGen.SafeSQL(objclsSchduleNote.sSNS_Category) & "', " & objclsSchduleNote.dSNS_CYear_BegShares & ", " & objclsSchduleNote.dSNS_CYear_BegAmount & ","
            sSql = sSql & " " & (objclsSchduleNote.dSNS_PYear_BegShares) & ", " & objclsSchduleNote.dSNS_PYear_BegAmount & ", " & objclsSchduleNote.dSNS_CYear_AddShares & ", " & objclsSchduleNote.dSNS_CYear_AddAmount & "," & (objclsSchduleNote.dSNS_PYear_AddShares) & ", "
            sSql = sSql & " " & objclsSchduleNote.dSNS_PYear_AddAmount & ", " & objclsSchduleNote.dSNS_CYear_EndShares & ", " & objclsSchduleNote.dSNS_CYear_EndAmount & ", " & objclsSchduleNote.dSNS_PYear_EndShares & ", " & objclsSchduleNote.dSNS_PYear_EndAmount & ", '" & (objclsSchduleNote.iSNS_YearID) & "', " & (objclsSchduleNote.iSNS_CompID) & ", '" & (objclsSchduleNote.sSNS_Status) & "',"
            sSql = sSql & " '" & (objclsSchduleNote.sSNS_DelFlag) & "' ," & (objclsSchduleNote.dSNS_CrOn) & ", '" & (objclsSchduleNote.iSNS_CrBy) & "',"
            sSql = sSql & " '" & (objclsSchduleNote.SNS_IPAddress) & "')"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSchedSecondNoteDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dtNote As DataTable
        Try
            sSql = " select * from ScheduleNote_Second  "
            sSql = sSql & "    where  SNS_CustId=" & ICustid & " and SNS_Category ='" & sType & "' and SNS_YEARId =" & IyearID & " and sns_status<>'D'  "
            dtNote = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSchedSecondNoteAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataSet
        Dim sSql As String
        Dim dtNote As DataSet
        Try
            sSql = " select * from ScheduleNote_Second  "
            sSql = sSql & "    where  SNS_CustId=" & ICustid & " and SNS_Category ='SF' and SNS_YEARId =" & IyearID & " and SNS_CUSTId=" & ICustid & " and sns_status<>'D' ;"
            sSql = sSql & " select * from ScheduleNote_Second  "
            sSql = sSql & "    where  SNS_CustId=" & ICustid & " and SNS_Category ='SS' and SNS_YEARId =" & IyearID & "  and SNS_CUSTId=" & ICustid & " and sns_status<>'D' ;"
            sSql = sSql & " select * from ScheduleNote_Second  "
            sSql = sSql & "    where  SNS_CustId=" & ICustid & " and SNS_Category ='ST' and SNS_YEARId =" & IyearID & " and SNS_CUSTId=" & ICustid & " and sns_status<>'D' ;"
            sSql = sSql & " select * from ScheduleNote_Second  "
            sSql = sSql & "    where  SNS_CustId=" & ICustid & " and SNS_Category ='SV' and SNS_YEARId =" & IyearID & " and SNS_CUSTId=" & ICustid & " and sns_status<>'D' ;"
            dtNote = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveThirdScheduleNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsSchduleNote As clsScheduleNote) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            If objclsSchduleNote.SNT_ID <> 0 Then
                sSql = "update ScheduleNote_Third set SNT_Description='" & (objclsSchduleNote.sSNT_Description) & "',SNT_CYear_Shares=" & objclsSchduleNote.dSNT_CYear_Shares & ", "
                sSql = sSql & " SNT_CYear_Amount= " & objclsSchduleNote.dSNT_CYear_Amount & ",SNT_PYear_Shares=" & (objclsSchduleNote.dSNT_PYear_Shares) & ", "
                sSql = sSql & "  SNT_PYear_Amount=" & (objclsSchduleNote.dSNT_PYear_Amount) & " where SNT_ID='" & (objclsSchduleNote.iSNT_ID) & "' and  SNT_Category= '" & objGen.SafeSQL(objclsSchduleNote.sSNT_Category) & "' and  SNT_CustId ='" & (objclsSchduleNote.iSNT_CustId) & "' "
                sSql = sSql & "  and SNT_YEARId ='" & (objclsSchduleNote.iSNT_YearID) & "'"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(SNT_ID)+1,1) from ScheduleNote_Third")
                sSql = " Insert into ScheduleNote_Third(SNT_ID,SNT_CustId,SNT_Description,SNT_Category,SNT_CYear_Shares,SNT_CYear_Amount,SNT_PYear_Shares,SNT_PYear_Amount,"
                sSql = sSql & " SNT_YEARId,SNT_CompId,"
                sSql = sSql & " SNT_STATUS,SNT_DELFLAG,SNT_CRON,SNT_CRBY,SNT_IPAddress"
                sSql = sSql & " ) values "
                sSql = sSql & "(" & iMax & ",'" & (objclsSchduleNote.iSNT_CustId) & "','" & (objclsSchduleNote.sSNT_Description) & "',"
                sSql = sSql & " '" & objGen.SafeSQL(objclsSchduleNote.sSNT_Category) & "'," & objclsSchduleNote.dSNT_CYear_Shares & ", " & objclsSchduleNote.dSNT_CYear_Amount & ","
                sSql = sSql & " " & (objclsSchduleNote.dSNT_PYear_Shares) & "," & (objclsSchduleNote.dSNT_PYear_Amount) & ",'" & (objclsSchduleNote.iSNT_YearID) & "',"
                sSql = sSql & " " & (objclsSchduleNote.iSNT_CompID) & ", '" & (objclsSchduleNote.sSNT_Status) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.sSNT_DelFlag) & "' ," & (objclsSchduleNote.dSNT_CrOn) & ", '" & (objclsSchduleNote.iSNT_CrBy) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.SNT_IPAddress) & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteSchedThirdNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTrID As Integer, ByVal iCustID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update ScheduleNote_Third Set SNT_DelFlag='D' , SNT_Status='D' Where SNT_ID=" & iTrID & " And SNT_CustId=" & iCustID & " And SNT_CompID=" & iCompID & " And SNT_YearID = " & iYearId & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function getSchedThirdNoteAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataSet
        Dim sSql As String
        Dim dtNote As DataSet
        Try
            If sType = "" Then
                sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNT_ID as Id,  SNT_Description as Description,SNT_CYear_Shares as CYear_Shares,SNT_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNT_PYear_Shares as PYear_Shares,SNT_PYear_Amount as PYear_Amount from ScheduleNote_THIRD  "
                sSql = sSql & "    where snt_yearid=" & IyearID & " and   SNT_CustId=" & ICustid & " and SNT_Category = 'AU' and snt_status<>'D' ;"
                sSql = sSql & " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNT_ID as Id,  SNT_Description as Description,SNT_CYear_Shares as CYear_Shares,SNT_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNT_PYear_Shares as PYear_Shares,SNT_PYear_Amount as PYear_Amount from ScheduleNote_THIRD  "
                sSql = sSql & "    where snt_yearid=" & IyearID & " and  SNT_CustId=" & ICustid & " and SNT_Category ='IS' and snt_status<>'D'  ; "
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNT_ID as Id,  SNT_Description as Description,SNT_CYear_Shares as CYear_Shares,SNT_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNT_PYear_Shares as PYear_Shares,SNT_PYear_Amount as PYear_Amount from ScheduleNote_THIRD  "
                sSql = sSql & "    where snt_yearid=" & IyearID & " and  SNT_CustId=" & ICustid & " and SNT_Category = 'AI' and snt_status<>'D'  ;"
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNT_ID as Id,  SNT_Description as Description,SNT_CYear_Shares as CYear_Shares,SNT_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNT_PYear_Shares as PYear_Shares,SNT_PYear_Amount as PYear_Amount from ScheduleNote_THIRD  "
                sSql = sSql & "    where snt_yearid=" & IyearID & " and   SNT_CustId=" & ICustid & " and SNT_Category ='BS' and snt_status<>'D'  ;"
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNT_ID as Id,  SNT_Description as Description,SNT_CYear_Shares as CYear_Shares,SNT_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNT_PYear_Shares as PYear_Shares,SNT_PYear_Amount as PYear_Amount from ScheduleNote_THIRD  "
                sSql = sSql & "    where snt_yearid=" & IyearID & " and  SNT_CustId=" & ICustid & " and SNT_Category ='CC' and snt_status<>'D'  ;"
                sSql = sSql & "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNT_ID as Id,  SNT_Description as Description,SNT_CYear_Shares as cYear_Shares,SNT_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNT_PYear_Shares as PYear_Shares,SNT_PYear_Amount as PYear_Amount from ScheduleNote_THIRD  "
                sSql = sSql & "    where snt_yearid=" & IyearID & " and  SNT_CustId=" & ICustid & " and SNT_Category ='FD' and snt_status<>'D'  ;"

                dtNote = objDBL.SQLExecuteDataSet(sAC, sSql)
                Return dtNote
            Else
                sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNT_ID as Id,  SNT_Description as Description,SNT_cYear_Shares as cYear_Shares,SNT_CYear_Amount AS CYear_Amount,  "
                sSql = sSql & " SNT_PYear_Shares as PYear_Shares,SNT_PYear_Amount as PYear_Amount from ScheduleNote_THIRD  "
                sSql = sSql & "    where snt_yearid=" & IyearID & " and   SNT_CustId=" & ICustid & " and SNT_Category ='" & sType & "' and snt_status<>'D'   "
                dtNote = objDBL.SQLExecuteDataSet(sAC, sSql)
                Return dtNote
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSchedThirdNoteDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dtNote As DataTable
        Try

            If sType = "TBE" Then
                sSql = "SELECT
                NULL AS SrNo,
                NULL AS Id,
                '<b>Equity Share Capital</b>' AS Description,
                NULL AS CYear_Shares,
                NULL AS CYear_Amount,
                NULL AS PYear_Shares,
                NULL AS PYear_Amount
            FROM
                ScheduleNote_Third
            WHERE
                SNT_Category = 'TBE'
            UNION "
            End If

            sSql = sSql & "SELECT
        ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo,
        SNT_ID AS Id,
        SNT_Description AS Description,
        CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNT_CYear_Shares AS money), 1), '.00', '')) AS CYear_Shares,
        CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNT_CYear_Amount AS money), 1), '.00', ''), '%') AS CYear_Amount,
        CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNT_PYear_Shares AS money), 1), '.00', '')) AS PYear_Shares,
        CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNT_PYear_Amount AS money), 1), '.00', ''), '%') AS PYear_Amount


    FROM
        ScheduleNote_Third
    WHERE
        SNT_CustId =" & ICustid & "
        AND SNT_Category = '" & sType & "'
        AND SNT_YearId = " & IyearID & "
        AND snt_status <> 'D';  "


            dtNote = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveaDescScheduleNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsSchduleNote As clsScheduleNote) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = " delete from ScheduleNote_Desc where SND_CustId=" & objclsSchduleNote.iSND_CustId & " and SND_YEARId=" & objclsSchduleNote.iSND_YearID & " and SND_Category='" & objclsSchduleNote.sSND_Category & "' "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            sSql = ""
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(SND_ID)+1,1) from ScheduleNote_Desc ")
            sSql = " Insert into ScheduleNote_Desc(SND_ID,SND_CustId,SND_Description,SND_Category, "
            sSql = sSql & " SND_YEARId,SND_CompId,"
            sSql = sSql & " SND_STATUS,SND_DELFLAG,SND_CRON,SND_CRBY,SND_IPAddress "
            sSql = sSql & " ) values "
            sSql = sSql & "(" & iMax & ",'" & (objclsSchduleNote.iSND_CustId) & "','" & (objclsSchduleNote.sSND_Description) & "',"
            sSql = sSql & " '" & objGen.SafeSQL(objclsSchduleNote.sSND_Category) & "', "
            sSql = sSql & "  '" & (objclsSchduleNote.iSND_YearID) & "', " & (objclsSchduleNote.iSND_CompID) & ", '" & (objclsSchduleNote.sSND_Status) & "',"
            sSql = sSql & " '" & (objclsSchduleNote.sSND_DelFlag) & "' ," & (objclsSchduleNote.dSND_CrOn) & ", '" & (objclsSchduleNote.iSND_CrBy) & "',"
            sSql = sSql & " '" & (objclsSchduleNote.SND_IPAddress) & "')"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDesciptionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dtNote As DataTable
        Try
            sSql = " select SND_Description from ScheduleNote_Desc  "
            sSql = sSql & "    where  SND_CustId=" & ICustid & " and SND_Category ='" & sType & "' and SND_YEARId =" & IyearID & "  and snD_status<>'D'  "
            dtNote = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDescAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataSet
        Dim sSql As String
        Dim dtNote As DataSet
        Try
            sSql = "select isnull(SND_Description,'') as  SND_Description from ScheduleNote_Desc  "
            sSql = sSql & "    where  SND_CustId=" & ICustid & " and SND_Category = 'cEquity' and SND_YEARId =" & IyearID & " and snD_status<>'D'  ;"
            sSql = sSql & " select isnull(SND_Description,'') as  SND_Description from ScheduleNote_Desc  "
            sSql = sSql & "    where  SND_CustId=" & ICustid & " and SND_Category = 'dPref' and SND_YEARId =" & IyearID & " and snd_status<>'D'  ;"
            sSql = sSql & " select isnull(SND_Description,'') as  SND_Description from ScheduleNote_Desc  "
            sSql = sSql & "    where  SND_CustId=" & ICustid & " and SND_Category = 'fShares' and SND_YEARId =" & IyearID & " and snd_status<>'D'  ;"
            sSql = sSql & " select isnull(SND_Description,'') as  SND_Description from ScheduleNote_Desc  "
            sSql = sSql & "    where  SND_CustId=" & ICustid & " and SND_Category = 'footNote' and SND_YEARId =" & IyearID & " and snd_status<>'D'  ;"
            dtNote = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFourthScheduleNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsSchduleNote As clsScheduleNote) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            If objclsSchduleNote.SNFT_ID <> 0 Then
                sSql = "update ScheduleNote_Fourth set SNFT_Description='" & (objclsSchduleNote.sSNFT_Description) & "',SNFT_NumShares=" & objclsSchduleNote.dSNFT_NumShares & ", "
                sSql = sSql & " SNFT_TotalShares= " & objclsSchduleNote.dSNFT_TotalShares & ",SNFT_ChangedShares=" & (objclsSchduleNote.dSNFT_ChangedShares) & " "
                sSql = sSql & "  where SNFT_ID='" & (objclsSchduleNote.iSNFT_ID) & "' and  SNFT_Category= '" & objGen.SafeSQL(objclsSchduleNote.sSNFT_Category) & "' and  SNFT_CustId ='" & (objclsSchduleNote.iSNFT_CustId) & "' "
                sSql = sSql & "  and SNTF_YEARId ='" & (objclsSchduleNote.iSNFT_YearID) & "'"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(SNFT_ID)+1,1) from ScheduleNote_Fourth")
                sSql = " Insert into ScheduleNote_Fourth(SNFT_ID,SNFT_CustId,SNFT_Description,SNFT_Category,SNFT_NumShares ,SNFT_TotalShares,SNFT_ChangedShares,"
                sSql = sSql & " SNFT_YEARId,SNFT_CompId,"
                sSql = sSql & " SNFT_STATUS,SNFT_DELFLAG,SNFT_CRON,SNFT_CRBY,SNFT_IPAddress"
                sSql = sSql & " ) values "
                sSql = sSql & "(" & iMax & ",'" & (objclsSchduleNote.iSNFT_CustId) & "','" & (objclsSchduleNote.sSNFT_Description) & "',"
                sSql = sSql & " '" & ("FSC") & "'," & objclsSchduleNote.dSNFT_NumShares & ", " & objclsSchduleNote.dSNFT_TotalShares & ","
                sSql = sSql & " " & (objclsSchduleNote.dSNFT_ChangedShares) & ",'" & (objclsSchduleNote.iSNFT_YearID) & "',"
                sSql = sSql & " " & (objclsSchduleNote.iSNFT_CompID) & ", '" & (objclsSchduleNote.sSNFT_Status) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.sSNFT_DelFlag) & "' ," & (objclsSchduleNote.dSNFT_CrOn) & ", '" & (objclsSchduleNote.iSNFT_CrBy) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.SNT_IPAddress) & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFourthScheduleNoteDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsSchduleNote As clsScheduleNote) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            If objclsSchduleNote.SNFT_ID <> 0 Then
                sSql = "update ScheduleNote_Fourth set SNFT_Description='" & (objclsSchduleNote.sSNFT_Description) & "',SNFT_NumShares=" & objclsSchduleNote.dSNFT_NumShares & ", "
                sSql = sSql & " SNFT_TotalShares= " & objclsSchduleNote.dSNFT_TotalShares & ",SNFT_ChangedShares=" & (objclsSchduleNote.dSNFT_ChangedShares) & " "
                sSql = sSql & "  where SNFT_ID='" & (objclsSchduleNote.iSNFT_ID) & "' and  SNFT_Category= '" & objGen.SafeSQL(objclsSchduleNote.sSNFT_Category) & "' and  SNFT_CustId ='" & (objclsSchduleNote.iSNFT_CustId) & "' "
                sSql = sSql & "  and SNTF_YEARId ='" & (objclsSchduleNote.iSNFT_YearID) & "'"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(SNFT_ID)+1,1) from ScheduleNote_Fourth")
                sSql = " Insert into ScheduleNote_Fourth(SNFT_ID,SNFT_CustId,SNFT_Description,SNFT_Category,SNFT_NumShares ,SNFT_TotalShares,SNFT_ChangedShares,"
                sSql = sSql & " SNFT_YEARId,SNFT_CompId,"
                sSql = sSql & " SNFT_STATUS,SNFT_DELFLAG,SNFT_CRON,SNFT_CRBY,SNFT_IPAddress"
                sSql = sSql & " ) values "
                sSql = sSql & "(" & iMax & ",'" & (objclsSchduleNote.iSNFT_CustId) & "','" & (objclsSchduleNote.sSNFT_Description) & "',"
                sSql = sSql & " '" & ("FSP") & "'," & objclsSchduleNote.dSNFT_NumShares & ", " & objclsSchduleNote.dSNFT_TotalShares & ","
                sSql = sSql & " " & (objclsSchduleNote.dSNFT_ChangedShares) & ",'" & (objclsSchduleNote.iSNFT_YearID) & "',"
                sSql = sSql & " " & (objclsSchduleNote.iSNFT_CompID) & ", '" & (objclsSchduleNote.sSNFT_Status) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.sSNFT_DelFlag) & "' ," & (objclsSchduleNote.dSNFT_CrOn) & ", '" & (objclsSchduleNote.iSNFT_CrBy) & "',"
                sSql = sSql & " '" & (objclsSchduleNote.SNT_IPAddress) & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub DeleteSchedFourthNoteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTrID As Integer, ByVal iCustID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update ScheduleNote_Fourth Set SNFT_DelFlag='D' , SNFT_Status='D' Where SNFT_ID=" & iTrID & " And SNFT_CustId=" & iCustID & " And SNFT_CompID=" & iCompID & " And SNFT_YearID = " & iYearId & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function getSchedFourthNoteAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataSet
        Dim sSql As String
        Dim dtNote As DataSet
        Try
            sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNFT_ID as Id,  SNFT_Description as PromoterName,SNFT_NumShares as CYShares,SNFT_TotalShares AS TotShares,  "
            sSql = sSql & " SNFT_ChangedShares as ChangedShares from ScheduleNote_Fourth  "
            sSql = sSql & "    where snft_yearid=" & IyearID & " and  SNFT_CustId=" & ICustid & " and SNFT_Category = 'FSC' AND SNFT_YEARID=" & IyearID & "  and snft_status<>'D'  ;"
            sSql = sSql & " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, SNFT_ID as Id,  SNFT_Description as Description,SNFT_NumShares as CYear_Shares,SNFT_TotalShares AS CYear_Amount, "
            sSql = sSql & " SNFT_ChangedShares as PYear_Shares from ScheduleNote_Fourth "
            sSql = sSql & "   where snft_yearid=" & IyearID & " and  SNFT_CustId=" & ICustid & " and SNFT_Category = 'FSP' AND SNFT_YEARID=" & IyearID & " and snft_status<>'D'  ;"
            dtNote = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSchedFourthNoteDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dtNote As DataTable
        Try
            sSql = " SELECT 
    ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SrNo,
    SNFT_ID AS Id,
    SNFT_Description AS PromoterName,
    SNFT_NumShares AS CYShares,
    CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNFT_TotalShares AS money), 1), '.00', ''), '%') AS TotShares,
    CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNFT_ChangedShares AS money), 1), '.00', ''), '%') AS ChangedShares
FROM 
    ScheduleNote_Fourth
WHERE 
    SNFT_CustId = " & ICustid & "
    AND SNFT_Category = 'FSC' 
    AND SNFT_YEARID = " & IyearID & "
    AND snft_status <> 'D';"
            dtNote = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSchedFourthNoteDetails1(ByVal sAC As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal ICustid As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dtNote As DataTable
        Try
            sSql = " SELECT 
    ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SrNo,
    SNFT_ID AS Id,
    SNFT_Description AS PromoterName,
    SNFT_NumShares AS CYShares,
    CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNFT_TotalShares AS money), 1), '.00', ''), '%') AS TotShares,
    CONCAT('', REPLACE(CONVERT(VARCHAR, CAST(SNFT_ChangedShares AS money), 1), '.00', ''), '%') AS ChangedShares
FROM 
    ScheduleNote_Fourth
WHERE 
    SNFT_CustId = " & ICustid & "
    AND SNFT_Category = 'FSP' 
    AND SNFT_YEARID = " & IyearID & "
    AND snft_status <> 'D';"
            dtNote = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtNote
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getScheduleNote_First(ByVal sAC As String, ByVal iACID As Integer, ByVal Iyearid As Integer, ByVal iCustId As Integer, ByVal sType As String) As DataTable
        Dim sSql As String : Dim dt, dtdetails As New DataTable
        Dim dTotAmount As Double = 0
        Dim dr As DataRow
        Dim DCurrYearAmmount As Double = 0.0
        Dim DPrevYearAmmount As Double = 0.0
        Try
            dtdetails.Columns.Add("SNF_Description")
            dtdetails.Columns.Add("SNF_CYear_Amount")
            dtdetails.Columns.Add("SNF_PYear_Amount")

            sSql = "Select SNF_Description,SNF_CYear_Amount,SNF_PYear_Amount from ScheduleNote_First "
            sSql = sSql & "Where SNF_CustID=" & iCustId & " and SNF_CompId = " & iACID & " and SNF_YEARId=" & Iyearid & " and SNF_DELFLAG='X' "
            sSql = sSql & "and SNF_Category ='" & sType & "' order by SNF_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtdetails.NewRow
                    'dr("Srno") = i + 1

                    dr("SNF_Description") = dt(i)("SNF_Description")
                    dr("SNF_CYear_Amount") = Convert.ToDecimal(dt(i)("SNF_CYear_Amount")).ToString("#,##0.00")
                    DCurrYearAmmount = DCurrYearAmmount + Convert.ToDecimal(dt(i)("SNF_CYear_Amount")).ToString("#,##0.00")
                    dr("SNF_PYear_Amount") = Convert.ToDecimal(dt(i)("SNF_PYear_Amount")).ToString("#,##0.00")
                    DPrevYearAmmount = DPrevYearAmmount + Convert.ToDecimal(dt(i)("SNF_PYear_Amount")).ToString("#,##0.00")
                    dtdetails.Rows.Add(dr)
                Next
                dr = dtdetails.NewRow
                dr("SNF_Description") = "<b>" & "Total" & "</b>"
                dr("SNF_CYear_Amount") = "<b>" & Convert.ToDecimal(DCurrYearAmmount).ToString("#,##0.00") & "</b>"
                dr("SNF_PYear_Amount") = "<b>" & Convert.ToDecimal(DPrevYearAmmount).ToString("#,##0.00") & "</b>"
                dtdetails.Rows.Add(dr)
            End If
            Return dtdetails
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getScheduleNote_Second(ByVal sAC As String, ByVal iACID As Integer, ByVal Iyearid As Integer, ByVal iCustId As Integer, ByVal sType As String) As DataTable
        Dim sSql As String : Dim dt, dtdetails As New DataTable
        Dim dTotAmount As Double = 0
        Dim dr As DataRow
        Try
            dtdetails.Columns.Add("SNS_Description")
            dtdetails.Columns.Add("SNS_CYear_BegShares")
            dtdetails.Columns.Add("SNS_CYear_BegAmount")
            dtdetails.Columns.Add("SNS_PYear_BegShares")
            dtdetails.Columns.Add("SNS_PYear_BegAmount")
            dtdetails.Columns.Add("SNS_CYear_AddShares")
            dtdetails.Columns.Add("SNS_CYear_AddAmount")
            dtdetails.Columns.Add("SNS_PYear_AddShares")
            dtdetails.Columns.Add("SNS_PYear_AddAmount")
            dtdetails.Columns.Add("SNS_CYear_EndShares")
            dtdetails.Columns.Add("SNS_CYear_EndAmount")
            dtdetails.Columns.Add("SNS_PYear_EndShares")
            dtdetails.Columns.Add("SNS_PYear_EndAmount")

            sSql = "Select * from ScheduleNote_Second "
            sSql = sSql & "Where SNS_CustId=" & iCustId & " and SNS_CompId = " & iACID & " and SNS_YEARId=" & Iyearid & " and SNS_DELFLAG='X' "
            sSql = sSql & "and SNS_Category ='" & sType & "' order by SNS_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtdetails.NewRow
                    'dr("Srno") = i + 1
                    dr("SNS_Description") = dt(i)("SNS_Description")
                    dr("SNS_CYear_BegShares") = Convert.ToDecimal(dt(i)("SNS_CYear_BegShares")).ToString("#,##0.00")
                    dr("SNS_CYear_BegAmount") = Convert.ToDecimal(dt(i)("SNS_CYear_BegAmount")).ToString("#,##0.00")
                    dr("SNS_PYear_BegShares") = Convert.ToDecimal(dt(i)("SNS_PYear_BegShares")).ToString("#,##0.00")
                    dr("SNS_PYear_BegAmount") = Convert.ToDecimal(dt(i)("SNS_PYear_BegAmount")).ToString("#,##0.00")
                    dr("SNS_CYear_AddShares") = Convert.ToDecimal(dt(i)("SNS_CYear_AddShares")).ToString("#,##0.00")
                    dr("SNS_CYear_AddAmount") = Convert.ToDecimal(dt(i)("SNS_CYear_AddAmount")).ToString("#,##0.00")
                    dr("SNS_PYear_AddShares") = Convert.ToDecimal(dt(i)("SNS_PYear_AddShares")).ToString("#,##0.00")
                    dr("SNS_PYear_AddAmount") = Convert.ToDecimal(dt(i)("SNS_PYear_AddAmount")).ToString("#,##0.00")
                    dr("SNS_CYear_EndShares") = Convert.ToDecimal(dt(i)("SNS_CYear_EndShares")).ToString("#,##0.00")
                    dr("SNS_CYear_EndAmount") = Convert.ToDecimal(dt(i)("SNS_CYear_EndAmount")).ToString("#,##0.00")
                    dr("SNS_PYear_EndShares") = Convert.ToDecimal(dt(i)("SNS_PYear_EndShares")).ToString("#,##0.00")
                    dr("SNS_PYear_EndAmount") = Convert.ToDecimal(dt(i)("SNS_PYear_EndAmount")).ToString("#,##0.00")
                    dtdetails.Rows.Add(dr)

                Next
            Else
                dr = dtdetails.NewRow
                dr("SNS_Description") = ""
                dr("SNS_CYear_BegShares") = ""
                dr("SNS_CYear_BegAmount") = ""
                dr("SNS_PYear_BegShares") = ""
                dr("SNS_PYear_BegAmount") = ""
                dr("SNS_CYear_AddShares") = ""
                dr("SNS_CYear_AddAmount") = ""
                dr("SNS_PYear_AddShares") = ""
                dr("SNS_PYear_AddAmount") = ""
                dr("SNS_CYear_EndShares") = ""
                dr("SNS_CYear_EndAmount") = ""
                dr("SNS_PYear_EndShares") = ""
                dr("SNS_PYear_EndAmount") = ""
                dtdetails.Rows.Add(dr)
            End If
            Return dtdetails
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getScheduleNote_Third(ByVal sAC As String, ByVal iACID As Integer, ByVal Iyearid As Integer, ByVal iCustId As Integer, ByVal sType As String) As DataTable
        Dim sSql As String : Dim dt, dtdetails As New DataTable
        Dim dTotAmount As Double = 0
        Dim dr As DataRow
        Try
            dtdetails.Columns.Add("SNT_Description")
            dtdetails.Columns.Add("SNT_CYear_Shares")
            dtdetails.Columns.Add("SNT_CYear_Amount")
            dtdetails.Columns.Add("SNT_PYear_Shares")
            dtdetails.Columns.Add("SNT_PYear_Amount")

            sSql = "Select SNT_Description,SNT_CYear_Shares,SNT_CYear_Amount,SNT_PYear_Shares, SNT_PYear_Amount from ScheduleNote_Third "
            sSql = sSql & "Where SNT_CustId=" & iCustId & " and SNT_CompId = " & iACID & " and SNT_YEARId=" & Iyearid & " and SNT_DELFLAG='X' "
            sSql = sSql & "and SNT_Category ='" & sType & "' order by SNT_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtdetails.NewRow
                    'dr("Srno") = i + 1
                    dr("SNT_Description") = dt(i)("SNT_Description")
                    dr("SNT_CYear_Shares") = Convert.ToDecimal(dt(i)("SNT_CYear_Shares")).ToString("#,##0.00")
                    dr("SNT_CYear_Amount") = Convert.ToDecimal(dt(i)("SNT_CYear_Amount")).ToString("#,##0.00")
                    dr("SNT_PYear_Shares") = Convert.ToDecimal(dt(i)("SNT_PYear_Shares")).ToString("#,##0.00")
                    dr("SNT_PYear_Amount") = Convert.ToDecimal(dt(i)("SNT_PYear_Amount")).ToString("#,##0.00")
                    dtdetails.Rows.Add(dr)
                Next
            End If
            Return dtdetails
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function getScheduleNote_cNote(ByVal sAC As String, ByVal iACID As Integer, ByVal Iyearid As Integer, ByVal iCustId As Integer, ByVal sType As String) As DataTable
        Dim sSql As String : Dim dt, dtdetails As New DataTable
        Dim dTotAmount As Double = 0
        Dim dr As DataRow
        Try
            dtdetails.Columns.Add("SND_Description")

            sSql = "Select SND_Description from ScheduleNote_Desc "
            sSql = sSql & "Where SND_CustId=" & iCustId & " and SND_CompId = " & iACID & " and SND_YEARId=" & Iyearid & " and SND_DELFLAG='X' "
            sSql = sSql & "and SND_Category ='" & sType & "' order by SND_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtdetails.NewRow
                    'dr("Srno") = i + 1
                    dr("SND_Description") = dt(i)("SND_Description")

                    dtdetails.Rows.Add(dr)
                Next
            Else
                dr = dtdetails.NewRow
                'dr("Srno") = i + 1
                dr("SND_Description") = ""

                dtdetails.Rows.Add(dr)
            End If
            Return dtdetails
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getScheduleNote_Fourth(ByVal sAC As String, ByVal iACID As Integer, ByVal Iyearid As Integer, ByVal iCustId As Integer, ByVal sType As String) As DataTable
        Dim sSql As String : Dim dt, dtdetails As New DataTable
        Dim dTotAmount As Double = 0
        Dim dr As DataRow
        Try
            dtdetails.Columns.Add("SNFT_Description")
            dtdetails.Columns.Add("SNFT_NumShares")
            dtdetails.Columns.Add("SNFT_TotalShares")
            dtdetails.Columns.Add("SNFT_ChangedShares")


            sSql = "Select SNFT_Description,SNFT_NumShares,SNFT_TotalShares,SNFT_ChangedShares from ScheduleNote_Fourth "
            sSql = sSql & "Where SNFT_CustId=" & iCustId & " and SNFT_CompId = " & iACID & " and SNFT_YEARId=" & Iyearid & " and SNFT_DELFLAG='X' "
            sSql = sSql & "and SNFT_Category ='" & sType & "' order by SNFT_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtdetails.NewRow
                    dr("SNFT_Description") = dt(i)("SNFT_Description")
                    dr("SNFT_NumShares") = Convert.ToDecimal(dt(i)("SNFT_NumShares")).ToString("#,##0.00")
                    dr("SNFT_TotalShares") = Convert.ToDecimal(dt(i)("SNFT_TotalShares")).ToString("#,##0.00")
                    dr("SNFT_ChangedShares") = Convert.ToDecimal(dt(i)("SNFT_ChangedShares")).ToString("#,##0.00")
                    dtdetails.Rows.Add(dr)
                Next
            End If
            Return dtdetails
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
