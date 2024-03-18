Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.Web
Imports System.ComponentModel
Public Class clsCAIQMaster
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    'section
    Private CAS_ID As Integer
    Private CAS_CODE As String
    Private CAS_SECTIONNAME As String
    Private CAS_POINTS As Integer
    Private CAS_Desc As String
    Private CAS_DELFLG As String
    Private CAS_CRBY As Integer
    Private CAS_STATUS As String
    Private CAS_UPDATEDBY As Integer
    Private CAS_IPAddress As String
    Private CAS_CompId As String
    Private CAS_YEARId As String

    'SubSection
    Private CASU_ID As Integer
    Private CASU_CODE As String
    Private CASU_SUBSECTIONNAME As String
    Private CASU_SECTIONID As Integer
    Private CASU_Points As Integer
    Private CASU_Desc As String
    Private CASU_DELFLG As String
    Private CASU_CRBY As Integer
    Private CASU_STATUS As String
    Private CASU_UPDATEDBY As String
    Private CASU_IPAddress As String
    Private CASU_CompId As Integer
    Private CASU_YEARId As Integer

    'Process
    Private CAP_ID As Integer
    Private CAP_CODE As String
    Private CAP_PROCESSNAME As String
    Private CAP_POINTS As Integer
    Private CAP_SECTIONID As Integer
    Private CAP_SUBSECTIONID As Integer
    Private CAP_Desc As String
    Private CAP_DELFLG As String
    Private CAP_CRBY As Integer
    Private CAP_STATUS As String
    Private CAP_UPDATEDBY As Integer
    Private CAP_IPAddress As String
    Private CAP_CompId As Integer
    Private CAP_YEARId As Integer

    'SubProcess
    Private CASP_ID As Integer
    Private CASP_CODE As String
    Private CASP_SUBPROCESSNAME As String
    Private CASP_POINTS As Integer
    Private CASP_SECTIONID As Integer
    Private CASP_SUBSECTIONID As Integer
    Private CASP_PROCESSID As Integer
    Private CASP_Desc As String
    Private CASP_DELFLG As String
    Private CASP_CRBY As Integer
    Private CASP_STATUS As String
    Private CASP_UPDATEDBY As Integer
    Private CASP_IPAddress As String
    Private CASP_CompId As Integer
    Private CASP_YEARId As Integer

    Public Property iId() As Integer
        Get
            Return (CAS_ID)
        End Get
        Set(ByVal Value As Integer)
            CAS_ID = Value
        End Set
    End Property

    Public Property SECTCODE() As String
        Get
            Return (CAS_CODE)
        End Get
        Set(ByVal Value As String)
            CAS_CODE = Value
        End Set
    End Property
    Public Property SECTNAME() As String
        Get
            Return (CAS_SECTIONNAME)
        End Get
        Set(ByVal Value As String)
            CAS_SECTIONNAME = Value
        End Set
    End Property
    Public Property SECTPOINTS() As Integer
        Get
            Return (CAS_POINTS)
        End Get
        Set(ByVal Value As Integer)
            CAS_POINTS = Value
        End Set
    End Property
    Public Property SECTDESC() As String
        Get
            Return (CAS_Desc)
        End Get
        Set(ByVal Value As String)
            CAS_Desc = Value
        End Set
    End Property
    Public Property SECTDELFLAG() As String
        Get
            Return (CAS_DELFLG)
        End Get
        Set(ByVal Value As String)
            CAS_DELFLG = Value
        End Set
    End Property
    Public Property SECTCRBY() As Integer
        Get
            Return (CAS_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CAS_CRBY = Value
        End Set
    End Property
    Public Property SECTSTATUS() As String
        Get
            Return (CAS_STATUS)
        End Get
        Set(ByVal Value As String)
            CAS_STATUS = Value
        End Set
    End Property
    Public Property SECTUPDATEDBY() As Integer
        Get
            Return (CAS_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CAS_UPDATEDBY = Value
        End Set
    End Property

    Public Property SECTIPADDRESS() As String
        Get
            Return (CAS_IPAddress)
        End Get
        Set(ByVal Value As String)
            CAS_IPAddress = Value
        End Set
    End Property
    Public Property SECTCOMPID As Integer
        Get
            Return (CAS_CompId)
        End Get
        Set(ByVal Value As Integer)
            CAS_CompId = Value
        End Set
    End Property
    Public Property SECTYEARID() As Integer
        Get
            Return (CAS_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CAS_YEARId = Value
        End Set
    End Property

    'subsection
    Public Property sSectID() As Integer
        Get
            Return (CASU_ID)
        End Get
        Set(ByVal Value As Integer)
            CASU_ID = Value
        End Set
    End Property
    Public Property sSECTCODE() As String
        Get
            Return (CASU_CODE)
        End Get
        Set(ByVal Value As String)
            CASU_CODE = Value
        End Set
    End Property
    Public Property sSECTNAME() As String
        Get
            Return (CASU_SUBSECTIONNAME)
        End Get
        Set(ByVal Value As String)
            CASU_SUBSECTIONNAME = Value
        End Set
    End Property
    Public Property sSECTPOINTS() As Integer
        Get
            Return (CASU_Points)
        End Get
        Set(ByVal Value As Integer)
            CASU_Points = Value
        End Set
    End Property
    Public Property iSecID() As Integer
        Get
            Return (CASU_SECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CASU_SECTIONID = Value
        End Set
    End Property
    Public Property sSECTDESC() As String
        Get
            Return (CASU_Desc)
        End Get
        Set(ByVal Value As String)
            CASU_Desc = Value
        End Set
    End Property
    Public Property sSECTDELFLAG() As String
        Get
            Return (CASU_DELFLG)
        End Get
        Set(ByVal Value As String)
            CASU_DELFLG = Value
        End Set
    End Property
    Public Property sSECTCRBY() As Integer
        Get
            Return (CASU_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CASU_CRBY = Value
        End Set
    End Property
    Public Property sSECTSTATUS() As String
        Get
            Return (CASU_STATUS)
        End Get
        Set(ByVal Value As String)
            CASU_STATUS = Value
        End Set
    End Property
    Public Property sSECTUPDATEDBY() As Integer
        Get
            Return (CASU_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CASU_UPDATEDBY = Value
        End Set
    End Property
    Public Property sSECTIPADDRESS() As String
        Get
            Return (CASU_IPAddress)
        End Get
        Set(ByVal Value As String)
            CASU_IPAddress = Value
        End Set
    End Property
    Public Property sSECTCOMPID() As Integer
        Get
            Return (CASU_CompId)
        End Get
        Set(ByVal Value As Integer)
            CASU_CompId = Value
        End Set
    End Property
    Public Property sSECTYEARID() As Integer
        Get
            Return (CASU_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CASU_YEARId = Value
        End Set
    End Property
    'Process
    Public Property iCAP_ID() As Integer
        Get
            Return (CAP_ID)
        End Get
        Set(ByVal Value As Integer)
            CAP_ID = Value
        End Set
    End Property
    Public Property sCAP_CODE() As String
        Get
            Return (CAP_CODE)
        End Get
        Set(ByVal Value As String)
            CAP_CODE = Value
        End Set
    End Property
    Public Property iCAP_PNAME() As String
        Get
            Return (CAP_PROCESSNAME)
        End Get
        Set(ByVal Value As String)
            CAP_PROCESSNAME = Value
        End Set
    End Property
    Public Property iCAP_Points() As Integer
        Get
            Return (CAP_POINTS)
        End Get
        Set(ByVal Value As Integer)
            CAP_POINTS = Value
        End Set
    End Property
    Public Property iCAP_SECID() As Integer
        Get
            Return (CAP_SECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CAP_SECTIONID = Value
        End Set
    End Property
    Public Property iCAP_SubSECID() As Integer
        Get
            Return (CAP_SUBSECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CAP_SUBSECTIONID = Value
        End Set
    End Property
    Public Property sCAP_Desc() As String
        Get
            Return (CAP_Desc)
        End Get
        Set(ByVal Value As String)
            CAP_Desc = Value
        End Set
    End Property
    Public Property sCAP_Delflag() As String
        Get
            Return (CAP_DELFLG)
        End Get
        Set(ByVal Value As String)
            CAP_DELFLG = Value
        End Set
    End Property
    Public Property iCAP_CRBY() As Integer
        Get
            Return (CAP_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CAP_CRBY = Value
        End Set
    End Property
    Public Property sCAP_STATUS() As String
        Get
            Return (CAP_STATUS)
        End Get
        Set(ByVal Value As String)
            CAP_STATUS = Value
        End Set
    End Property
    Public Property iCAP_UPDATEDBY() As Integer
        Get
            Return (CAP_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CAP_UPDATEDBY = Value
        End Set
    End Property
    Public Property iCAP_IPAddress() As String
        Get
            Return (CAP_IPAddress)
        End Get
        Set(ByVal Value As String)
            CAP_IPAddress = Value
        End Set
    End Property
    Public Property iCAP_CompId() As Integer
        Get
            Return (CAP_CompId)
        End Get
        Set(ByVal Value As Integer)
            CAP_CompId = Value
        End Set
    End Property
    Public Property iCAP_YEARId() As Integer
        Get
            Return (CAP_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CAP_YEARId = Value
        End Set
    End Property

    'Sub Process
    Public Property iCASP_ID() As Integer
        Get
            Return (CASP_ID)
        End Get
        Set(ByVal Value As Integer)
            CASP_ID = Value
        End Set
    End Property
    Public Property sCASP_CODE() As String
        Get
            Return (CASP_CODE)
        End Get
        Set(ByVal Value As String)
            CASP_CODE = Value
        End Set
    End Property
    Public Property sCASP_SUBPNAME() As String
        Get
            Return (CASP_SUBPROCESSNAME)
        End Get
        Set(ByVal Value As String)
            CASP_SUBPROCESSNAME = Value
        End Set
    End Property
    Public Property iCASP_POINTS() As Integer
        Get
            Return (CASP_POINTS)
        End Get
        Set(ByVal Value As Integer)
            CASP_POINTS = Value
        End Set
    End Property
    Public Property iCASP_SECID() As Integer
        Get
            Return (CASP_SECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CASP_SECTIONID = Value
        End Set
    End Property
    Public Property iCASP_SubSECID() As Integer
        Get
            Return (CASP_SUBSECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CASP_SUBSECTIONID = Value
        End Set
    End Property
    Public Property iCASP_ProID() As Integer
        Get
            Return (CASP_PROCESSID)
        End Get
        Set(ByVal Value As Integer)
            CASP_PROCESSID = Value
        End Set
    End Property
    Public Property sCASP_Desc() As String
        Get
            Return (CASP_Desc)
        End Get
        Set(ByVal Value As String)
            CASP_Desc = Value
        End Set
    End Property
    Public Property sCASP_DELFLG() As String
        Get
            Return (CASP_DELFLG)
        End Get
        Set(ByVal Value As String)
            CASP_DELFLG = Value
        End Set
    End Property
    Public Property iCASP_CRBY() As Integer
        Get
            Return (CASP_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CASP_CRBY = Value
        End Set
    End Property
    Public Property sCASP_STATUS() As String
        Get
            Return (CASP_STATUS)
        End Get
        Set(ByVal Value As String)
            CASP_STATUS = Value
        End Set
    End Property
    Public Property iCASP_UPDATEDBY() As Integer
        Get
            Return (CASP_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CASP_UPDATEDBY = Value
        End Set
    End Property
    Public Property sCASP_IPAddress() As String
        Get
            Return (CASP_IPAddress)
        End Get
        Set(ByVal Value As String)
            CASP_IPAddress = Value
        End Set
    End Property
    Public Property iCASP_CompId() As Integer
        Get
            Return (CASP_CompId)
        End Get
        Set(ByVal Value As Integer)
            CASP_CompId = Value
        End Set
    End Property
    Public Property iCASP_YearId() As Integer
        Get
            Return (CASP_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CASP_YEARId = Value
        End Set
    End Property

    'Factor
    Private CF_PKID As Integer
    Private CF_YearID As Integer
    Private CF_AuditID As Integer
    Private CF_Name As String
    Private CF_Desc As String
    Private CF_FLAG As String
    Private CF_STATUS As String
    Private CF_CrBy As Integer
    Private CF_UpdatedBy As Integer
    Private CF_IPAddress As String
    Private CF_CompId As Integer
    Public Property iFactorId() As Integer
        Get
            Return (CF_PKID)
        End Get
        Set(ByVal Value As Integer)
            CF_PKID = Value
        End Set
    End Property
    Public Property iFactorYearId() As Integer
        Get
            Return (CF_YearID)
        End Get
        Set(ByVal Value As Integer)
            CF_YearID = Value
        End Set
    End Property
    Public Property iFactorAuditId() As Integer
        Get
            Return (CF_AuditID)
        End Get
        Set(ByVal Value As Integer)
            CF_AuditID = Value
        End Set
    End Property
    Public Property sFactorDesc() As String
        Get
            Return (CF_Desc)
        End Get
        Set(ByVal Value As String)
            CF_Desc = Value
        End Set
    End Property
    Public Property sFactorName() As String
        Get
            Return (CF_Name)
        End Get
        Set(ByVal Value As String)
            CF_Name = Value
        End Set
    End Property
    Public Property sFactorFlag() As String
        Get
            Return (CF_FLAG)
        End Get
        Set(ByVal Value As String)
            CF_FLAG = Value
        End Set
    End Property
    Public Property sFactorStatus() As String
        Get
            Return (CF_STATUS)
        End Get
        Set(ByVal Value As String)
            CF_STATUS = Value
        End Set
    End Property
    Public Property iFactorCrById() As Integer
        Get
            Return (CF_CrBy)
        End Get
        Set(ByVal Value As Integer)
            CF_CrBy = Value
        End Set
    End Property
    Public Property iFactorUpdatedById() As Integer
        Get
            Return (CF_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            CF_UpdatedBy = Value
        End Set
    End Property
    Public Property sFactorIPAddress() As String
        Get
            Return (CF_IPAddress)
        End Get
        Set(ByVal Value As String)
            CF_IPAddress = Value
        End Set
    End Property
    Public Property iFactorCompId() As Integer
        Get
            Return (CF_CompId)
        End Get
        Set(ByVal Value As Integer)
            CF_CompId = Value
        End Set
    End Property

    'FactorCategory
    Private CFC_PKID As Integer
    Private CFC_FactorID As Integer
    Private CFC_YearID As Integer
    Private CFC_AuditID As Integer
    Private CFC_Name As String
    Private CFC_Desc As String
    Private CFC_FLAG As String
    Private CFC_STATUS As String
    Private CFC_CrBy As Integer
    Private CFC_UpdatedBy As Integer
    Private CFC_IPAddress As String
    Private CFC_CompId As Integer
    Public Property iFactorCategoryId() As Integer
        Get
            Return (CFC_PKID)
        End Get
        Set(ByVal Value As Integer)
            CFC_PKID = Value
        End Set
    End Property
    Public Property iFactorCategoryFactorId() As Integer
        Get
            Return (CFC_FactorID)
        End Get
        Set(ByVal Value As Integer)
            CFC_FactorID = Value
        End Set
    End Property
    Public Property iFactorCategoryYearId() As Integer
        Get
            Return (CFC_YearID)
        End Get
        Set(ByVal Value As Integer)
            CFC_YearID = Value
        End Set
    End Property
    Public Property iFactorCategoryAuditId() As Integer
        Get
            Return (CFC_AuditID)
        End Get
        Set(ByVal Value As Integer)
            CFC_AuditID = Value
        End Set
    End Property
    Public Property sFactorCategoryDesc() As String
        Get
            Return (CFC_Desc)
        End Get
        Set(ByVal Value As String)
            CFC_Desc = Value
        End Set
    End Property
    Public Property sFactorCategoryName() As String
        Get
            Return (CFC_Name)
        End Get
        Set(ByVal Value As String)
            CFC_Name = Value
        End Set
    End Property
    Public Property sFactorCategoryFlag() As String
        Get
            Return (CFC_FLAG)
        End Get
        Set(ByVal Value As String)
            CFC_FLAG = Value
        End Set
    End Property
    Public Property sFactorCategoryStatus() As String
        Get
            Return (CFC_STATUS)
        End Get
        Set(ByVal Value As String)
            CFC_STATUS = Value
        End Set
    End Property
    Public Property iFactorCategoryCrById() As Integer
        Get
            Return (CFC_CrBy)
        End Get
        Set(ByVal Value As Integer)
            CFC_CrBy = Value
        End Set
    End Property
    Public Property iFactorCategoryUpdatedById() As Integer
        Get
            Return (CFC_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            CFC_UpdatedBy = Value
        End Set
    End Property
    Public Property sFactorCategoryIPAddress() As String
        Get
            Return (CFC_IPAddress)
        End Get
        Set(ByVal Value As String)
            CFC_IPAddress = Value
        End Set
    End Property
    Public Property iFactorCategoryCompId() As Integer
        Get
            Return (CFC_CompId)
        End Get
        Set(ByVal Value As Integer)
            CFC_CompId = Value
        End Set
    End Property

    'FactorDescriptor
    Private CD_PKID As Integer
    Private CD_YearID As Integer
    Private CD_AuditID As Integer
    Private CD_Name As String
    Private CD_Desc As String
    Private CD_Range As String
    Private CD_FLAG As String
    Private CD_STATUS As String
    Private CD_CrBy As Integer
    Private CD_UpdatedBy As Integer
    Private CD_IPAddress As String
    Private CD_CompId As Integer
    Public Property iFactorDescriptorId() As Integer
        Get
            Return (CD_PKID)
        End Get
        Set(ByVal Value As Integer)
            CD_PKID = Value
        End Set
    End Property
    Public Property iFactorDescriptorYearId() As Integer
        Get
            Return (CD_YearID)
        End Get
        Set(ByVal Value As Integer)
            CD_YearID = Value
        End Set
    End Property
    Public Property iFactorDescriptorAuditId() As Integer
        Get
            Return (CD_AuditID)
        End Get
        Set(ByVal Value As Integer)
            CD_AuditID = Value
        End Set
    End Property
    Public Property sFactorDescriptorName() As String
        Get
            Return (CD_Name)
        End Get
        Set(ByVal Value As String)
            CD_Name = Value
        End Set
    End Property
    Public Property sFactorDescriptorDesc() As String
        Get
            Return (CD_Desc)
        End Get
        Set(ByVal Value As String)
            CD_Desc = Value
        End Set
    End Property
    Public Property sFactorDescriptorRange() As String
        Get
            Return (CD_Range)
        End Get
        Set(ByVal Value As String)
            CD_Range = Value
        End Set
    End Property
    Public Property sFactorDescriptorFlag() As String
        Get
            Return (CD_FLAG)
        End Get
        Set(ByVal Value As String)
            CD_FLAG = Value
        End Set
    End Property
    Public Property sFactorDescriptorStatus() As String
        Get
            Return (CD_STATUS)
        End Get
        Set(ByVal Value As String)
            CD_STATUS = Value
        End Set
    End Property
    Public Property iFactorDescriptorCrById() As Integer
        Get
            Return (CD_CrBy)
        End Get
        Set(ByVal Value As Integer)
            CD_CrBy = Value
        End Set
    End Property
    Public Property iFactorDescriptorUpdatedById() As Integer
        Get
            Return (CD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            CD_UpdatedBy = Value
        End Set
    End Property
    Public Property sFactorDescriptorIPAddress() As String
        Get
            Return (CD_IPAddress)
        End Get
        Set(ByVal Value As String)
            CD_IPAddress = Value
        End Set
    End Property
    Public Property iFactorDescriptorCompId() As Integer
        Get
            Return (CD_CompId)
        End Get
        Set(ByVal Value As Integer)
            CD_CompId = Value
        End Set
    End Property
    'Issue Range
    Private CIR_PKID As Integer
    Private CIR_YearID As Integer
    Private CIR_AuditID As Integer
    Private CIR_StartRange As Integer
    Private CIR_EndRange As Integer
    Private CIR_FLAG As String
    Private CIR_STATUS As String
    Private CIR_CrBy As Integer
    Private CIR_UpdatedBy As Integer
    Private CIR_IPAddress As String
    Private CIR_CompId As Integer
    Public Property iCIR_PKID() As Integer
        Get
            Return (CIR_PKID)
        End Get
        Set(ByVal Value As Integer)
            CIR_PKID = Value
        End Set
    End Property
    Public Property iCIR_YearID() As Integer
        Get
            Return (CIR_YearID)
        End Get
        Set(ByVal Value As Integer)
            CIR_YearID = Value
        End Set
    End Property
    Public Property iCIR_AuditID() As Integer
        Get
            Return (CIR_AuditID)
        End Get
        Set(ByVal Value As Integer)
            CIR_AuditID = Value
        End Set
    End Property
    Public Property iCIR_StartRange() As Integer
        Get
            Return (CIR_StartRange)
        End Get
        Set(ByVal Value As Integer)
            CIR_StartRange = Value
        End Set
    End Property
    Public Property iCIR_EndRange() As Integer
        Get
            Return (CIR_EndRange)
        End Get
        Set(ByVal Value As Integer)
            CIR_EndRange = Value
        End Set
    End Property
    Public Property sCIR_FLAG() As String
        Get
            Return (CIR_FLAG)
        End Get
        Set(ByVal Value As String)
            CIR_FLAG = Value
        End Set
    End Property
    Public Property sCIR_STATUS() As String
        Get
            Return (CIR_STATUS)
        End Get
        Set(ByVal Value As String)
            CIR_STATUS = Value
        End Set
    End Property
    Public Property iCIR_CrBy() As Integer
        Get
            Return (CIR_CrBy)
        End Get
        Set(ByVal Value As Integer)
            CIR_CrBy = Value
        End Set
    End Property
    Public Property iCIR_UpdatedBy() As Integer
        Get
            Return (CIR_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            CIR_UpdatedBy = Value
        End Set
    End Property
    Public Property sCIR_IPAddress() As String
        Get
            Return (CIR_IPAddress)
        End Get
        Set(ByVal Value As String)
            CIR_IPAddress = Value
        End Set
    End Property
    Public Property iCIR_CompId() As Integer
        Get
            Return (CIR_CompId)
        End Get
        Set(ByVal Value As Integer)
            CIR_CompId = Value
        End Set
    End Property

    '-------------Excel Upload---------------------
    Public Function CheckSection(ByVal sAC As String, ByVal iACID As Integer, ByVal sSectionName As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select CAS_ID from CRPA_Section where Upper(CAS_SECTIONNAME)=Upper('" & sSectionName & "') and (CAS_DELFLG='A' or CAS_STATUS = 'W')"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubSection(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubSectionName As String, ByVal iSecID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iSecID = 0 Then
                sSql = "Select CASU_ID from CRPA_SubSection where Upper(CASU_SUBSECTIONNAME)=Upper('" & sSubSectionName & "') And  (CASU_DELFLG ='A' or CASU_DELFLG ='W') AND CASU_CompId=" & iACID & ""
            Else
                sSql = "Select CASU_ID from CRPA_SubSection where Upper(CASU_SUBSECTIONNAME)=Upper('" & sSubSectionName & "') And CASU_SECTIONID=" & iSecID & " And  (CASU_DELFLG ='A' or CASU_DELFLG ='W')  AND CASU_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal sProcessName As String, ByVal iSecID As Integer, ByVal iSSecID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iSSecID = 0 Then
                sSql = "Select CAP_ID from CRPA_Process where Upper(CAP_PROCESSNAME)=Upper('" & sProcessName & "') And (CAP_DELFLG ='A' or CAP_DELFLG ='W') and  CAP_CompId=" & iACID & ""
            Else
                sSql = "Select CAP_ID from CRPA_Process where Upper(CAP_PROCESSNAME)=Upper('" & sProcessName & "')  And CAP_SECTIONID=" & iSecID & " And CAP_SubSECTIONID=" & iSSecID & " And (CAP_DELFLG ='A' or CAP_DELFLG ='W') and  CAP_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal sProcessName As String, ByVal iSecID As Integer, ByVal iSSecID As Integer, ByVal iProID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iProID = 0 Then
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_SUBPROCESSNAME)=Upper('" & sProcessName & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')  And CASP_CompId=" & iACID & ""
            Else
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_SUBPROCESSNAME)=Upper('" & sProcessName & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')  And CASP_SECTIONID=" & iSecID & " and CASP_SUBSECTIONID=" & iSSecID & " AND CASP_PROCESSID=" & iProID & " AND CASP_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubProcessCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubProCode As String, ByVal iSecID As Integer, ByVal iSSecID As Integer, ByVal iProID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iProID = 0 Then
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_CODE)=Upper('" & sSubProCode & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')  And CASP_CompId=" & iACID & ""
            Else
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_CODE)=Upper('" & sSubProCode & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')"
                sSql = sSql & " And CASP_SECTIONID=" & iSecID & " and CASP_SUBSECTIONID=" & iSSecID & " AND CASP_PROCESSID=" & iProID & " AND CASP_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSectionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTCODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_SECTIONNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_POINTS", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTPOINTS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTDESC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTDELFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTSTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTIPADDRESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTCOMPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.SECTYEARID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_Section_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavesSectionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSectID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTCODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_SUBSECTIONNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_SECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iSecID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_Points", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTPOINTS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTDESC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTDELFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTSTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@@CASU_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTIPADDRESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTCOMPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sSECTYEARID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_SubSection", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveProcessDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCAP_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_PROCESSNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.CAP_PROCESSNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_POINTS", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_Points
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_SECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_SECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_SUBSECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_SubSECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCAP_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCAP_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCAP_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCAP_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_Process", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubProcessSectionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCASP_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_SUBPROCESSNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCASP_SUBPNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_POINTS", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_POINTS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_SECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_SECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_SUBSECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_SubSECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_PROCESSID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_ProID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCASP_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCASP_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCASP_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCASP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCASP_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_SubProcess", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCAIQFactorGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FactorID")
            dt.Columns.Add("FactorName")
            dt.Columns.Add("FactorDesc")
            dt.Columns.Add("Status")
            sSql = "Select * From CAIQ_Factors Where CF_CompID=" & iAcID & " "
            If iID > 0 Then
                sSql = sSql & " And CF_PKID =" & iID & " "
            End If
            If iStatus = 0 Then
                sSql = sSql & " And CF_FLAG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CF_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CF_FLAG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CF_PKID ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("FactorID") = ds.Tables(0).Rows(i)("CF_PKID")
                dr("FactorName") = ds.Tables(0).Rows(i)("CF_Name")
                dr("FactorDesc") = ds.Tables(0).Rows(i)("CF_Desc")
                If IsDBNull(ds.Tables(0).Rows(i)("CF_FLAG")) = False Then
                    If ds.Tables(0).Rows(i)("CF_FLAG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("CF_FLAG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("CF_FLAG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCAIQFactorStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal iUserId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update CAIQ_Factors Set CF_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " CF_FLAG='A',CF_STATUS='A',CF_ApprovedBy= " & iUserId & ",CF_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " CF_FLAG='D',CF_STATUS='AD',CF_DeletedBy= " & iUserId & ",CF_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " CF_FLAG='A',CF_STATUS='AR',CF_RecallBy= " & iUserId & ",CF_RecallOn=GetDate()"
            End If
            sSql = sSql & " Where CF_PKID= " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckCAIQFactorNameExist(ByVal sAC As String, ByVal sFactorName As String, ByVal iFactorID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CF_Name from CAIQ_Factors where CF_Name='" & sFactorName & "' And CF_PKID <>" & iFactorID & " And (CF_FLAG='A' or CF_STATUS = 'W')"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFactorDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_YearID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_AuditID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorAuditId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_FLAG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCrById
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorUpdatedById
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CF_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCAIQ_Factors", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCAIQFactorCategoryGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FactorID")
            dt.Columns.Add("FactorCategoryID")
            dt.Columns.Add("FactorCategoryName")
            dt.Columns.Add("FactorCategoryDesc")
            dt.Columns.Add("FactorName")
            dt.Columns.Add("Status")
            sSql = "Select CF_PKID,CF_Name,* From CAIQ_FactorCategory,CAIQ_Factors Where CFC_FactorID=CF_PKID And CFC_CompID=" & iAcID & " "
            If iID > 0 Then
                sSql = sSql & " And CFC_PKID =" & iID & " "
            End If
            If iStatus = 0 Then
                sSql = sSql & " And CFC_FLAG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CFC_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CFC_FLAG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CFC_PKID ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("FactorID") = ds.Tables(0).Rows(i)("CF_PKID")
                dr("FactorCategoryID") = ds.Tables(0).Rows(i)("CFC_PKID")
                dr("FactorCategoryName") = ds.Tables(0).Rows(i)("CFC_Name")
                dr("FactorCategoryDesc") = ds.Tables(0).Rows(i)("CFC_Desc")
                dr("FactorName") = ds.Tables(0).Rows(i)("CF_Name")
                If IsDBNull(ds.Tables(0).Rows(i)("CFC_FLAG")) = False Then
                    If ds.Tables(0).Rows(i)("CFC_FLAG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("CFC_FLAG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("CFC_FLAG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCAIQFactorCategoryStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal iUserId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update CAIQ_FactorCategory Set CFC_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " CFC_FLAG='A',CFC_STATUS='A',CFC_ApprovedBy= " & iUserId & ",CFC_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " CFC_FLAG='D',CFC_STATUS='AD',CFC_DeletedBy= " & iUserId & ",CFC_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " CFC_FLAG='A',CFC_STATUS='AR',CFC_RecallBy= " & iUserId & ",CFC_RecallOn=GetDate()"
            End If
            sSql = sSql & " Where CFC_PKID= " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckCAIQFactorCategoryNameExist(ByVal sAC As String, ByVal sFactorCategoryName As String, ByVal iFactorID As Integer, ByVal iFactorCategoryID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CFC_Name from CAIQ_FactorCategory where CFC_Name='" & sFactorCategoryName & "' And CFC_FactorID=" & iFactorID & " And CFC_PKID <>" & iFactorCategoryID & " And (CFC_FLAG='A' or CFC_STATUS = 'W')"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFactorCategoryDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCategoryId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_FactorID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCategoryFactorId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_YearID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCategoryYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_AuditID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCategoryAuditId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorCategoryName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorCategoryDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_FLAG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorCategoryFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorCategoryStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCategoryCrById
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCategoryUpdatedById
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorCategoryIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CFC_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorCategoryCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCAIQ_FactorCategory", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCAIQDescriptorsGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescriptorID")
            dt.Columns.Add("DescriptorName")
            dt.Columns.Add("DescriptorDesc")
            dt.Columns.Add("DescriptorRange")
            dt.Columns.Add("Status")
            sSql = "Select * From CAIQ_Descriptors Where CD_CompID=" & iAcID & " "
            If iID > 0 Then
                sSql = sSql & " And CD_PKID =" & iID & " "
            End If
            If iStatus = 0 Then
                sSql = sSql & " And CD_FLAG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CD_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CD_FLAG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CD_PKID ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescriptorID") = ds.Tables(0).Rows(i)("CD_PKID")
                dr("DescriptorName") = ds.Tables(0).Rows(i)("CD_Name")
                dr("DescriptorDesc") = ds.Tables(0).Rows(i)("CD_Desc")
                dr("DescriptorRange") = ds.Tables(0).Rows(i)("CD_Range")
                If IsDBNull(ds.Tables(0).Rows(i)("CD_FLAG")) = False Then
                    If ds.Tables(0).Rows(i)("CD_FLAG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("CD_FLAG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("CD_FLAG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCAIQIssueRangeGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FinancialYr")
            dt.Columns.Add("IssueID")
            dt.Columns.Add("IssueStartRange")
            dt.Columns.Add("IssueEndRange")
            dt.Columns.Add("Status")
            sSql = "Select * From CAIQ_IssueRange Where CIR_CompID=" & iAcID & " "
            If iID > 0 Then
                sSql = sSql & " And CIR_PKID =" & iID & " "
            End If
            If iStatus = 0 Then
                sSql = sSql & " And CIR_FLAG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CIR_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CIR_FLAG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CIR_PKID ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("FinancialYr") = objclsGeneralFunctions.Get2DigitFinancialYearName(sAc, iAcID, ds.Tables(0).Rows(i)("CIR_YearID"))
                dr("IssueID") = ds.Tables(0).Rows(i)("CIR_PKID")
                dr("IssueStartRange") = ds.Tables(0).Rows(i)("CIR_StartRange")
                dr("IssueEndRange") = ds.Tables(0).Rows(i)("CIR_EndRange")
                If IsDBNull(ds.Tables(0).Rows(i)("CIR_FLAG")) = False Then
                    If ds.Tables(0).Rows(i)("CIR_FLAG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("CIR_FLAG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("CIR_FLAG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCAIQDescriptorsStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal iUserId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update CAIQ_Descriptors Set CD_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " CD_FLAG='A',CD_STATUS='A',CD_ApprovedBy= " & iUserId & ",CD_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " CD_FLAG='D',CD_STATUS='AD',CD_ApprovedBy= " & iUserId & ",CD_ApprovedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " CD_FLAG='A',CD_STATUS='AR',CD_ApprovedBy= " & iUserId & ",CD_ApprovedOn=GetDate()"
            End If
            sSql = sSql & " Where CD_PKID= " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateCAIQIssueRangeStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal iUserId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update CAIQ_IssueRange Set CIR_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " CIR_FLAG='A',CIR_STATUS='A',CIR_ApprovedBy= " & iUserId & ",CIR_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " CIR_FLAG='D',CIR_STATUS='AD',CIR_ApprovedBy= " & iUserId & ",CIR_ApprovedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " CIR_FLAG='A',CIR_STATUS='AR',CIR_ApprovedBy= " & iUserId & ",CIR_ApprovedOn=GetDate()"
            End If
            sSql = sSql & " Where CIR_PKID= " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckCAIQDescriptorsNameExist(ByVal sAC As String, ByVal sFactorCategoryName As String, ByVal iFactorCategoryID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CD_Name from CAIQ_Descriptors where CD_Name='" & sFactorCategoryName & "' And CD_PKID <>" & iFactorCategoryID & " And (CD_FLAG='A' or CD_STATUS = 'W')"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCAIQIssueRangeExistFORFY(ByVal sAC As String, ByVal iFYID As Integer, ByVal iPKID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "select CIR_PKID from CAIQ_IssueRange where CIR_YearID=" & iFYID & " And CIR_PKID <>" & iPKID & " And (CIR_FLAG='A' or CIR_STATUS = 'W')"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCAIQDescriptorDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorDescriptorId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_YearID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorDescriptorYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_AuditID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorDescriptorAuditId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorDescriptorName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorDescriptorDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_Range", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorDescriptorRange
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_FLAG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorDescriptorFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorDescriptorStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorDescriptorCrById
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorDescriptorUpdatedById
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sFactorDescriptorIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CD_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iFactorDescriptorCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCAIQ_Descriptors", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCAIQIssueRangeDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsCAIQMaster As clsCAIQMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_YearID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_AuditID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_AuditID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_StartRange", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_StartRange
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_EndRange", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_EndRange
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_FLAG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCIR_FLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCIR_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsCAIQMaster.sCIR_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIR_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsCAIQMaster.iCIR_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCAIQ_IssueRange", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class