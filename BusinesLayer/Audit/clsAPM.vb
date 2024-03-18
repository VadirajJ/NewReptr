Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure strAPM_Details
    Private APM_ID As Integer
    Private APM_YearID As Integer
    Private APM_AuditCode As String
    Private APM_CustID As Integer
    Private APM_FunctionID As Integer
    Private APM_AuditorsRoleID As Integer
    Private APM_AuditTeamsID As String
    Private APM_BranchID As Integer
    Private APM_PartnersID As String
    Private APM_TStartDate As Date
    Private APM_TEndDate As Date
    Private APM_EstimatedEffortDays As Integer
    Private APM_Objectives As String
    Private APM_CustomerRemarks As String
    Private APM_AuditorsRemarks As String
    Private APM_Audit_Confirm As Integer
    Private APM_Audit_Confirm_Yes As Date
    Private APM_AuditTaskID As Integer
    Private APM_PStartDate As Date
    Private APM_PEndDate As Date
    Private APM_Resource As String
    Private APM_CrBy As Integer
    Private APM_CrOn As Date
    Private APM_UpdatedBy As Integer
    Private APM_UpdatedOn As Date
    Private APM_Subject As String
    Private APM_Body As String
    Private APM_TOEmail As String
    Private APM_CCEmail As String
    Private APM_IPAddress As String
    Private APM_AttachID As Integer
    Private APM_CompID As Integer
    Public Property iAPM_ID() As Integer
        Get
            Return (APM_ID)
        End Get
        Set(ByVal Value As Integer)
            APM_ID = Value
        End Set
    End Property
    Public Property iAPM_YearID() As Integer
        Get
            Return (APM_YearID)
        End Get
        Set(ByVal Value As Integer)
            APM_YearID = Value
        End Set
    End Property
    Public Property sAPM_AuditCode() As String
        Get
            Return (APM_AuditCode)
        End Get
        Set(ByVal Value As String)
            APM_AuditCode = Value
        End Set
    End Property
    Public Property iAPM_CustID() As Integer
        Get
            Return (APM_CustID)
        End Get
        Set(ByVal Value As Integer)
            APM_CustID = Value
        End Set
    End Property
    Public Property iAPM_FunctionID() As Integer
        Get
            Return (APM_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            APM_FunctionID = Value
        End Set
    End Property
    Public Property iAPM_AuditorsRoleID() As Integer
        Get
            Return (APM_AuditorsRoleID)
        End Get
        Set(ByVal Value As Integer)
            APM_AuditorsRoleID = Value
        End Set
    End Property

    Public Property sAPM_AuditTeamsID() As String
        Get
            Return (APM_AuditTeamsID)
        End Get
        Set(ByVal Value As String)
            APM_AuditTeamsID = Value
        End Set
    End Property

    Public Property iAPM_BranchID() As Integer
        Get
            Return (APM_BranchID)
        End Get
        Set(ByVal Value As Integer)
            APM_BranchID = Value
        End Set
    End Property

    Public Property sAPM_PartnersID() As String
        Get
            Return (APM_PartnersID)
        End Get
        Set(ByVal Value As String)
            APM_PartnersID = Value
        End Set
    End Property
    Public Property dAPM_TStartDate() As Date
        Get
            Return (APM_TStartDate)
        End Get
        Set(ByVal Value As Date)
            APM_TStartDate = Value
        End Set
    End Property
    Public Property dAPM_TEndDate() As Date
        Get
            Return (APM_TEndDate)
        End Get
        Set(ByVal Value As Date)
            APM_TEndDate = Value
        End Set
    End Property
    Public Property iAPM_EstimatedEffortDays() As Integer
        Get
            Return (APM_EstimatedEffortDays)
        End Get
        Set(ByVal Value As Integer)
            APM_EstimatedEffortDays = Value
        End Set
    End Property
    Public Property sAPM_Objectives() As String
        Get
            Return (APM_Objectives)
        End Get
        Set(ByVal Value As String)
            APM_Objectives = Value
        End Set
    End Property
    Public Property sAPM_CustomerRemarks() As String
        Get
            Return (APM_CustomerRemarks)
        End Get
        Set(ByVal Value As String)
            APM_CustomerRemarks = Value
        End Set
    End Property
    Public Property sAPM_AuditorsRemarks() As String
        Get
            Return (APM_AuditorsRemarks)
        End Get
        Set(ByVal Value As String)
            APM_AuditorsRemarks = Value
        End Set
    End Property
    Public Property iAPM_Audit_Confirm() As Integer
        Get
            Return (APM_Audit_Confirm)
        End Get
        Set(ByVal Value As Integer)
            APM_Audit_Confirm = Value
        End Set
    End Property
    Public Property dAPM_Audit_Confirm_Yes() As Date
        Get
            Return (APM_Audit_Confirm_Yes)
        End Get
        Set(ByVal Value As Date)
            APM_Audit_Confirm_Yes = Value
        End Set
    End Property
    Public Property iAPM_AttachID() As Integer
        Get
            Return (APM_AttachID)
        End Get
        Set(ByVal Value As Integer)
            APM_AttachID = Value
        End Set
    End Property
    Public Property iAPM_AuditTaskID() As Integer
        Get
            Return (APM_AuditTaskID)
        End Get
        Set(ByVal Value As Integer)
            APM_AuditTaskID = Value
        End Set
    End Property
    Public Property dAPM_PStartDate() As Date
        Get
            Return (APM_PStartDate)
        End Get
        Set(ByVal Value As Date)
            APM_PStartDate = Value
        End Set
    End Property
    Public Property sAPM_Resource() As String
        Get
            Return (APM_Resource)
        End Get
        Set(ByVal Value As String)
            APM_Resource = Value
        End Set
    End Property
    Public Property dAPM_PEndDate() As Date
        Get
            Return (APM_PEndDate)
        End Get
        Set(ByVal Value As Date)
            APM_PEndDate = Value
        End Set
    End Property
    Public Property iAPM_CrBy() As Integer
        Get
            Return (APM_CrBy)
        End Get
        Set(ByVal Value As Integer)
            APM_CrBy = Value
        End Set
    End Property
    Public Property iAPM_UpdatedBy() As Integer
        Get
            Return (APM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            APM_UpdatedBy = Value
        End Set
    End Property
    Public Property sAPM_Subject() As String
        Get
            Return (APM_Subject)
        End Get
        Set(ByVal Value As String)
            APM_Subject = Value
        End Set
    End Property

    Public Property sAPM_Body() As String
        Get
            Return (APM_Body)
        End Get
        Set(ByVal Value As String)
            APM_Body = Value
        End Set
    End Property
    Public Property sAPM_TOEmail() As String
        Get
            Return (APM_TOEmail)
        End Get
        Set(ByVal Value As String)
            APM_TOEmail = Value
        End Set
    End Property
    Public Property sAPM_CCEmail() As String
        Get
            Return (APM_CCEmail)
        End Get
        Set(ByVal Value As String)
            APM_CCEmail = Value
        End Set
    End Property
    Public Property iAPM_CompID() As Integer
        Get
            Return (APM_CompID)
        End Get
        Set(ByVal Value As Integer)
            APM_CompID = Value
        End Set
    End Property
    Public Property sAPM_IPAddress() As String
        Get
            Return (APM_IPAddress)
        End Get
        Set(ByVal Value As String)
            APM_IPAddress = Value
        End Set
    End Property
End Structure
Public Structure strAPM_Assignment_Details
    Private AAPM_ID As Integer
    Private AAPM_AuditCodeID As Integer
    Private AAPM_CustID As Integer
    Private AAPM_AuditTaskID As Integer
    Private AAPM_AuditTaskType As String
    Private AAPM_PStartDate As Date
    Private AAPM_PEndDate As Date
    Private AAPM_Resource As String
    Private AAPM_CrBy As Integer
    Private AAPM_UpdateBy As Integer
    Private AAPM_IPAddress As String
    Private AAPM_CompID As Integer
    Private AAPM_YearID As Integer
    Private AAPM_FunctionID As Integer
    Public Property iAAPM_ID() As Integer
        Get
            Return (AAPM_ID)
        End Get
        Set(ByVal Value As Integer)
            AAPM_ID = Value
        End Set
    End Property

    Public Property iAAPM_YearID() As Integer
        Get
            Return (AAPM_YearID)
        End Get
        Set(ByVal Value As Integer)
            AAPM_YearID = Value
        End Set
    End Property
    Public Property iAAPM_FunctionID() As Integer
        Get
            Return (AAPM_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            AAPM_FunctionID = Value
        End Set
    End Property

    Public Property iAAPM_AuditCodeID() As Integer
        Get
            Return (AAPM_AuditCodeID)
        End Get
        Set(ByVal Value As Integer)
            AAPM_AuditCodeID = Value
        End Set
    End Property
    Public Property iAAPM_CustID() As Integer
        Get
            Return (AAPM_CustID)
        End Get
        Set(ByVal Value As Integer)
            AAPM_CustID = Value
        End Set
    End Property
    Public Property iAAPM_AuditTaskID() As Integer
        Get
            Return (AAPM_AuditTaskID)
        End Get
        Set(ByVal Value As Integer)
            AAPM_AuditTaskID = Value
        End Set
    End Property
    Public Property dAAPM_PStartDate() As Date
        Get
            Return (AAPM_PStartDate)
        End Get
        Set(ByVal Value As Date)
            AAPM_PStartDate = Value
        End Set
    End Property
    Public Property dAAPM_PEndDate() As Date
        Get
            Return (AAPM_PEndDate)
        End Get
        Set(ByVal Value As Date)
            AAPM_PEndDate = Value
        End Set
    End Property
    Public Property sAAPM_Resource() As String
        Get
            Return (AAPM_Resource)
        End Get
        Set(ByVal Value As String)
            AAPM_Resource = Value
        End Set
    End Property
    Public Property sAAPM_AuditTaskType() As String
        Get
            Return (AAPM_AuditTaskType)
        End Get
        Set(ByVal Value As String)
            AAPM_AuditTaskType = Value
        End Set
    End Property
    Public Property iAAPM_CrBy() As Integer
        Get
            Return (AAPM_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AAPM_CrBy = Value
        End Set
    End Property
    Public Property iAAPM_Updateby() As Integer
        Get
            Return (AAPM_UpdateBy)
        End Get
        Set(ByVal Value As Integer)
            AAPM_UpdateBy = Value
        End Set
    End Property
    Public Property iAAPM_CompID() As Integer
        Get
            Return (AAPM_CompID)
        End Get
        Set(ByVal Value As Integer)
            AAPM_CompID = Value
        End Set
    End Property
    Public Property sAAPM_IPAddress() As String
        Get
            Return (AAPM_IPAddress)
        End Get
        Set(ByVal Value As String)
            AAPM_IPAddress = Value
        End Set
    End Property
End Structure
Public Structure strAudit_APMCM_ChecksMatrix
    Private APMCM_PKID As Integer
    Private APMCM_APMPKID As Integer
    Private APMCM_YearID As Integer
    Private APMCM_AuditCode As String
    Private APMCM_CustID As Integer
    Private APMCM_FunctionID As Integer
    Private APMCM_SubFunctionID As Integer
    Private APMCM_ProcessID As Integer
    Private APMCM_SubProcessID As Integer
    Private APMCM_RiskID As Integer
    Private APMCM_ControlID As Integer
    Private APMCM_ChecksID As Integer
    Private APMCM_MMMID As Integer
    Private APMCM_IPAddress As String
    Private APMCM_CompID As Integer
    Public Property iAPMCM_PKID() As Integer
        Get
            Return (APMCM_PKID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_PKID = Value
        End Set
    End Property
    Public Property iAPMCM_MMMID() As Integer
        Get
            Return (APMCM_MMMID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_MMMID = Value
        End Set
    End Property
    Public Property iAPMCM_APMPKID() As Integer
        Get
            Return (APMCM_APMPKID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_APMPKID = Value
        End Set
    End Property
    Public Property iAPMCM_YearID() As Integer
        Get
            Return (APMCM_YearID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_YearID = Value
        End Set
    End Property
    Public Property iAPMCM_CustID() As Integer
        Get
            Return (APMCM_CustID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_CustID = Value
        End Set
    End Property
    Public Property iAPMCM_FunctionID() As Integer
        Get
            Return (APMCM_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_FunctionID = Value
        End Set
    End Property
    Public Property iAPMCM_SubFunctionID() As Integer
        Get
            Return (APMCM_SubFunctionID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_SubFunctionID = Value
        End Set
    End Property

    Public Property iAPMCM_ProcessID() As Integer
        Get
            Return (APMCM_ProcessID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_ProcessID = Value
        End Set
    End Property

    Public Property iAPMCM_SubProcessID() As Integer
        Get
            Return (APMCM_SubProcessID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_SubProcessID = Value
        End Set
    End Property
    Public Property iAPMCM_RiskID() As Integer
        Get
            Return (APMCM_RiskID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_RiskID = Value
        End Set
    End Property
    Public Property iAPMCM_ControlID() As Integer
        Get
            Return (APMCM_ControlID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_ControlID = Value
        End Set
    End Property
    Public Property iAPMCM_ChecksID() As Integer
        Get
            Return (APMCM_ChecksID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_ChecksID = Value
        End Set
    End Property
    Public Property iAPMCM_CompID() As Integer
        Get
            Return (APMCM_CompID)
        End Get
        Set(ByVal Value As Integer)
            APMCM_CompID = Value
        End Set
    End Property
    Public Property sAPMCM_IPAddress() As String
        Get
            Return (APMCM_IPAddress)
        End Get
        Set(ByVal Value As String)
            APMCM_IPAddress = Value
        End Set
    End Property
End Structure
Public Class clsAPM
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetAnnualPlanCheck(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select AAP_PKID From Audit_AnnualPlan Where AAP_CustID=" & iCustID & " And AAP_CompID=" & iACID & " And AAP_YearID=" & iYearID & " And AAP_FunID=" & iFunctionID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APM_ID,APM_AuditCode From Audit_APM_Details Where APM_AuditCode <>'' and APM_APMCRStatus='Submitted' And APM_CompID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iCustID > 0 Then
                sSql = sSql & " And APM_CustID=" & iCustID & " "
            End If
            If iFunID > 0 Then
                sSql = sSql & " And APM_FunctionID=" & iFunID & " "
            End If
            sSql = sSql & " Order by APM_ID Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer,
                                   ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APM_ID,APM_CustID,APM_AuditorsRoleID,APM_BranchID,APM_Objectives,APM_TStartDate,APM_TEndDate,APM_EstimatedEffortDays,APM_AuditTeamsID,"
            sSql = sSql & " APM_PartnersID,APM_AttachID,APM_APMStatus from Audit_APM_Details where APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APM_ID=" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APM_FunctionID=" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APM_CustID=" & iCustID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMCRDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer,
                                     ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APM_AuditTeamsID,APM_ID,APM_CustID,APM_AuditorsRoleID,APM_BranchID,APM_Objectives,APM_TStartDate,APM_TEndDate,APM_EstimatedEffortDays,APM_AuditTeamsID,APM_Audit_Confirm_Yes,APM_Audit_Confirm,"
            sSql = sSql & " APM_PartnersID,APM_AttachID,APM_APMStatus,c.Mas_Description as BranchName,APM_CustomerRemarks,APM_AuditorsRemarks,APM_APMCRStatus from Audit_APM_Details"
            sSql = sSql & " Left Join SAD_CUST_LOCATION c On c.Mas_ID=APM_BranchID And c.Mas_CompID=" & iACID & ""
            sSql = sSql & " where APM_APMStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APM_ID=" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APM_FunctionID=" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APM_CustID=" & iCustID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMTADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APM_AuditTeamsID,APM_ID,APM_CustID,APM_AuditorsRoleID,APM_BranchID,APM_Objectives,APM_TStartDate,APM_TEndDate,APM_EstimatedEffortDays,APM_AuditTeamsID,APM_Audit_Confirm_Yes,APM_Audit_Confirm,"
            sSql = sSql & " APM_PartnersID,APM_AttachID,APM_APMStatus,c.Mas_Description as BranchName,APM_CustomerRemarks,APM_AuditorsRemarks,APM_APMCRStatus,APM_APMTAStatus,APM_PGEDetailId from Audit_APM_Details"
            sSql = sSql & " Left Join SAD_CUST_LOCATION c On c.Mas_ID=APM_BranchID And c.Mas_CompID=" & iACID & ""
            sSql = sSql & " where APM_APMCRStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APM_ID=" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APM_FunctionID=" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APM_CustID=" & iCustID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAPMDetails(ByVal sAC As String, ByVal objAPM As strAPM_Details)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_AuditorsRoleID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_AuditorsRoleID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_AuditTeamsID", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAPM.sAPM_AuditTeamsID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_BranchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_PartnersID", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAPM.sAPM_PartnersID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_TStartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAPM.dAPM_TStartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_TEndDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAPM.dAPM_TEndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_EstimatedEffortDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_EstimatedEffortDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_Objectives", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAPM.sAPM_Objectives
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("APM_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAPM.sAPM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPM_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_APM_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitAPM(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iUserID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_APM_Details set APM_StatusID=2,APM_AppBy=" & iUserID & ",APM_AppOn=Getdate(),APM_APMStatus='Submitted' where APM_CustID=" & iCustID & " And "
            sSql = sSql & "APM_FunctionID=" & iFunID & " And APM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadMappedRiskControlMatrix(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal sSubFunctionID As String, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("MMMID")
            dtTab.Columns.Add("FunctionId")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("Checks")

            sSql = "Select MMM_ID,MMM_FunID,MMM_SEMID,MMM_PMID,PM_Name,MMM_SPMID,SPM_Name,SEM_Name,MMM_RISKID,MMM_Risk,MMM_CONTROLID,MMM_Control,MMM_ChecksID,MMM_CHECKS From MST_MAPPING_MASTER"
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=MMM_SEMID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=MMM_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=MMM_SPMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Where MMM_DelFlag='A' And MMM_YearID=" & iYearID & " And MMM_Module='A' And MMM_FunID=" & iFunctionID & " And MMM_CustID=" & iCustID & ""
            If sSubFunctionID <> "" Then
                sSql = sSql & " And MMM_SEMID In (" & sSubFunctionID & ")"
            End If
            sSql = sSql & " And MMM_CompID=" & iACID & " Order by PM_Name,SPM_Name,MMM_Risk,MMM_Control,MMM_CHECKS"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("MMMID") = 0
                If IsDBNull(dt.Rows(i)("MMM_ID")) = False Then
                    dr("MMMID") = dt.Rows(i)("MMM_ID")
                End If
                dr("FunctionId") = 0
                If IsDBNull(dt.Rows(i)("MMM_FunID")) = False Then
                    dr("FunctionId") = dt.Rows(i)("MMM_FunID")
                End If
                dr("SubFunctionID") = 0
                If IsDBNull(dt.Rows(i)("MMM_SEMID")) = False Then
                    dr("SubFunctionID") = dt.Rows(i)("MMM_SEMID")
                End If
                dr("SubFunction") = ""
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                dr("ProcessID") = 0
                If IsDBNull(dt.Rows(i)("MMM_PMID")) = False Then
                    dr("ProcessID") = dt.Rows(i)("MMM_PMID")
                End If
                dr("SubProcessID") = 0
                If IsDBNull(dt.Rows(i)("MMM_SPMID")) = False Then
                    dr("SubProcessID") = dt.Rows(i)("MMM_SPMID")
                End If
                dr("Process") = ""
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                dr("SubProcess") = ""
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                dr("RisKID") = 0
                If IsDBNull(dt.Rows(i)("MMM_RISKID")) = False Then
                    dr("RisKID") = dt.Rows(i)("MMM_RISKID")
                End If
                dr("ControlID") = 0
                If IsDBNull(dt.Rows(i)("MMM_CONTROLID")) = False Then
                    dr("ControlID") = dt.Rows(i)("MMM_CONTROLID")
                End If
                dr("ChecksID") = 0
                If IsDBNull(dt.Rows(i)("MMM_ChecksID")) = False Then
                    dr("ChecksID") = dt.Rows(i)("MMM_ChecksID")
                End If
                dr("Checks") = ""
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappedRiskControlMatrixAPMCR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal sSubFunctionID As String, ByVal sProcessID As String, ByVal sSubProID As String, ByVal sRiskID As String, ByVal sControlID As String, ByVal sChecksID As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("MMMID")
            dtTab.Columns.Add("FunctionId")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("Checks")

            sSql = "Select MMM_ID,MMM_FunID,MMM_SEMID,MMM_PMID,PM_Name,MMM_SPMID,SPM_Name,SEM_Name,MMM_RISKID,MMM_Risk,MMM_CONTROLID,MMM_Control,MMM_ChecksID,MMM_CHECKS From MST_MAPPING_MASTER"
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=MMM_SEMID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=MMM_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=MMM_SPMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Where MMM_DelFlag='A' And MMM_YearID=" & iYearID & " And MMM_Module='A' And MMM_FunID=" & iFunctionID & ""
            sSql = sSql & " And MMM_SEMID In (" & sSubFunctionID & ") And MMM_PMID In (" & sProcessID & ") And MMM_SPMID In (" & sSubProID & ") And MMM_RISKID In (" & sRiskID & ") And MMM_CONTROLID In (" & sControlID & ") And MMM_ChecksID In (" & sChecksID & ")"
            sSql = sSql & " And MMM_CompID=" & iACID & " Order by PM_Name,SPM_Name,MMM_Risk,MMM_Control,MMM_CHECKS"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("MMMID") = 0
                If IsDBNull(dt.Rows(i)("MMM_ID")) = False Then
                    dr("MMMID") = dt.Rows(i)("MMM_ID")
                End If
                dr("FunctionId") = 0
                If IsDBNull(dt.Rows(i)("MMM_FunID")) = False Then
                    dr("FunctionId") = dt.Rows(i)("MMM_FunID")
                End If
                dr("SubFunctionID") = 0
                If IsDBNull(dt.Rows(i)("MMM_SEMID")) = False Then
                    dr("SubFunctionID") = dt.Rows(i)("MMM_SEMID")
                End If
                dr("SubFunction") = ""
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                dr("ProcessID") = 0
                If IsDBNull(dt.Rows(i)("MMM_PMID")) = False Then
                    dr("ProcessID") = dt.Rows(i)("MMM_PMID")
                End If
                dr("SubProcessID") = 0
                If IsDBNull(dt.Rows(i)("MMM_SPMID")) = False Then
                    dr("SubProcessID") = dt.Rows(i)("MMM_SPMID")
                End If
                dr("Process") = ""
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                dr("SubProcess") = ""
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                dr("RisKID") = 0
                If IsDBNull(dt.Rows(i)("MMM_RISKID")) = False Then
                    dr("RisKID") = dt.Rows(i)("MMM_RISKID")
                End If
                dr("ControlID") = 0
                If IsDBNull(dt.Rows(i)("MMM_CONTROLID")) = False Then
                    dr("ControlID") = dt.Rows(i)("MMM_CONTROLID")
                End If
                dr("ChecksID") = 0
                If IsDBNull(dt.Rows(i)("MMM_ChecksID")) = False Then
                    dr("ChecksID") = dt.Rows(i)("MMM_ChecksID")
                End If
                dr("Checks") = ""
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappedRiskControlMatrixFromAPM(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                                       ByVal iAuditID As Integer, ByVal sSubFunctionID As String, ByVal sProcessID As String, ByVal sSubProID As String, ByVal sRiskID As String, ByVal sControlID As String, ByVal sChecksID As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("MMMID")
            dtTab.Columns.Add("APMMatrixID")
            dtTab.Columns.Add("APMAssignID")
            dtTab.Columns.Add("FunctionId")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("Checks")

            sSql = "Select MMM_ID,MMM_FunID,MMM_SEMID,MMM_PMID,PM_Name,MMM_SPMID,SPM_Name,SEM_Name,MMM_RISKID,MMM_Risk, MMM_CONTROLID,MMM_Control,MMM_ChecksID,MMM_CHECKS,APMCM_PKID,AAPM_ID From MST_MAPPING_MASTER"
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=MMM_SEMID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=MMM_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=MMM_SPMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_APM_ChecksMatrix On APMCM_YearID=" & iYearID & " And APMCM_CustID=" & iCustID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_FunctionID=MMM_FunID And APMCM_SubFunctionID=MMM_SEMID And "
            sSql = sSql & " APMCM_ProcessID=MMM_PMID And APMCM_SubProcessID=MMM_SPMID And APMCM_RiskID=MMM_RISKID And APMCM_ControlID=MMM_CONTROLID And APMCM_ChecksID=MMM_ChecksID"
            sSql = sSql & " Left Join Audit_APM_Assignment_Details On AAPM_YearID=" & iYearID & " And AAPM_CustID=" & iCustID & " And AAPM_AuditCodeID=" & iAuditID & " And AAPM_FunctionID=MMM_FunID And AAPM_AuditTaskID=MMM_PMID And AAPM_AuditTaskType='AP'"
            sSql = sSql & " Where MMM_DelFlag='A' And MMM_YearID=" & iYearID & " And MMM_Module='A' And MMM_FunID=" & iFunctionID & ""
            If sSubFunctionID <> "" Then
                sSql = sSql & " And MMM_SEMID In (" & sSubFunctionID & ") And MMM_PMID In (" & sProcessID & ") And MMM_SPMID In (" & sSubProID & ") And MMM_RISKID In (" & sRiskID & ") And MMM_CONTROLID In (" & sControlID & ") And MMM_ChecksID In (" & sChecksID & ")"
            End If
            sSql = sSql & " And MMM_CompID=" & iACID & " Order by PM_Name,SPM_Name,MMM_Risk,MMM_Control,MMM_CHECKS"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("MMMID") = 0
                If IsDBNull(dt.Rows(i)("MMM_ID")) = False Then
                    dr("MMMID") = dt.Rows(i)("MMM_ID")
                End If
                dr("FunctionId") = 0
                If IsDBNull(dt.Rows(i)("MMM_FunID")) = False Then
                    dr("FunctionId") = dt.Rows(i)("MMM_FunID")
                End If
                dr("SubFunctionID") = 0
                If IsDBNull(dt.Rows(i)("MMM_SEMID")) = False Then
                    dr("SubFunctionID") = dt.Rows(i)("MMM_SEMID")
                End If
                dr("SubFunction") = ""
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                dr("ProcessID") = 0
                If IsDBNull(dt.Rows(i)("MMM_PMID")) = False Then
                    dr("ProcessID") = dt.Rows(i)("MMM_PMID")
                End If
                dr("SubProcessID") = 0
                If IsDBNull(dt.Rows(i)("MMM_SPMID")) = False Then
                    dr("SubProcessID") = dt.Rows(i)("MMM_SPMID")
                End If
                dr("Process") = ""
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                dr("SubProcess") = ""
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                dr("RisKID") = 0
                If IsDBNull(dt.Rows(i)("MMM_RISKID")) = False Then
                    dr("RisKID") = dt.Rows(i)("MMM_RISKID")
                End If
                dr("ControlID") = 0
                If IsDBNull(dt.Rows(i)("MMM_CONTROLID")) = False Then
                    dr("ControlID") = dt.Rows(i)("MMM_CONTROLID")
                End If
                dr("ChecksID") = 0
                If IsDBNull(dt.Rows(i)("MMM_ChecksID")) = False Then
                    dr("ChecksID") = dt.Rows(i)("MMM_ChecksID")
                End If
                dr("Checks") = ""
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                dr("APMMatrixID") = 0
                If IsDBNull(dt.Rows(i)("APMCM_PKID")) = False Then
                    dr("APMMatrixID") = dt.Rows(i)("APMCM_PKID")
                End If
                dr("APMAssignID") = 0
                If IsDBNull(dt.Rows(i)("AAPM_ID")) = False Then
                    dr("APMAssignID") = dt.Rows(i)("AAPM_ID")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTaskAndProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal sProcessIDs As String, ByVal sCode As String) As DataTable
        Dim sSql As String = ""
        Try
            If sCode = "AT" Then
                sSql = "SELECT CMM_ID As DataValueField,CMM_Desc As DataTextField FROM Content_Management_Master WHERE CMM_Category='AP' And CMM_DelFlag='A' Order by CMM_Desc"
            ElseIf sCode = "AP" Then
                If sProcessIDs <> "" Then
                    sSql = "Select PM_ID As DataValueField,PM_NAME As DataTextField from Mst_process_MAster where PM_ID in(" & sProcessIDs & ") Order by PM_Name"
                End If
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCustomersAuditRemarks(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal sCustomerRemarks As String, ByVal sAuditorsRemarks As String, ByVal iConfirm As Integer,
                                           ByVal dConfirmDate As String, ByVal iAttachID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_APM_details Set APM_APMCRStatus='Updated',APM_AttachID=" & iAttachID & " ,APM_YearID=" & iYearID & ",APM_CustomerRemarks='" & sCustomerRemarks & "',APM_AuditorsRemarks='" & sAuditorsRemarks & "',APM_Audit_Confirm=" & iConfirm & ","
            If dConfirmDate <> "" Then
                sSql = sSql & "APM_Audit_Confirm_Yes='" & dConfirmDate & "',"
            End If
            sSql = sSql & "APM_UpdatedBy=" & iUserID & ",APM_UpdatedOn=GetDate() where APM_CustID=" & iCustID & " And APM_FunctionID=" & iFunctionID & " and APM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SubmittedCustomersAuditRemarks(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                              ByVal iFunctionID As Integer, ByVal sCustomerRemarks As String, ByVal sAuditorsRemarks As String, ByVal iConfirm As Integer,
                                              ByVal dConfirmDate As String, ByVal sAuditCode As String, ByVal iAttachID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_APM_details Set APM_StatusID=3,APM_AttachID=" & iAttachID & " ,APM_APMCRStatus='Submitted' ,APM_YearID=" & iYearID & ",APM_CustomerRemarks='" & sCustomerRemarks & "',APM_AuditorsRemarks='" & sAuditorsRemarks & "',APM_Audit_Confirm=" & iConfirm & ","
            If dConfirmDate <> "" Then
                sSql = sSql & "APM_Audit_Confirm_Yes='" & dConfirmDate & "',"
            End If
            If sAuditCode <> "" Then
                sSql = sSql & "APM_AuditCode='" & sAuditCode & "',"
            End If
            sSql = sSql & "APM_AppBy=" & iUserID & ",APM_AppOn=GetDate() where APM_CustID=" & iCustID & " And APM_FunctionID=" & iFunctionID & " and APM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateTaskAssignmentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iAttachID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_APM_details Set APM_APMTAStatus='Updated',APM_AttachID=" & iAttachID & " ,APM_UpdatedBy=" & iUserID & " ,APM_UpdatedOn=GetDate() Where "
            If iFunctionID > 0 Then
                sSql = sSql & " APM_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " APM_ID=" & iAuditID & " And"
            End If
            sSql = sSql & " APM_YearID=" & iYearID & " And APM_CustID=" & iCustID & " And APM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                            ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_APM_details Set APM_AttachID=" & iAttachID & ",APM_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " APM_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " APM_ID=" & iAuditID & " And"
            End If
            sSql = sSql & " APM_YearID=" & iYearID & " And APM_CustID=" & iCustID & " And APM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SubmittedTaskAssignmentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                              ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iAttachID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_APM_details Set APM_StatusID=3,APM_APMTAStatus='Submitted',APM_AttachID=" & iAttachID & " ,APM_AppBy=" & iUserID & ",APM_AppOn=GetDate(),APM_YearID=" & iYearID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " APM_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " APM_ID=" & iAuditID & " And"
            End If
            sSql = sSql & " APM_CustID=" & iCustID & " and APM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GerAuditTeamsID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select APM_AuditTeamsID From Audit_APM_Details Where APM_CompID=" & iACID & " And APM_YearID=" & iYearID & " "
            If iAuditID > 0 Then
                sSql = sSql & " And APM_ID=" & iAuditID & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " And APM_FunctionID=" & iFunctionID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GerProcessID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APMCM_ProcessID From Audit_APM_ChecksMatrix Where APMCM_CompID=" & iACID & " And APMCM_YearID=" & iYearID & " "
            If iAuditID > 0 Then
                sSql = sSql & " And APMCM_APMPKID=" & iAuditID & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " And APMCM_FunctionID=" & iFunctionID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubmittedSubFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable
        Dim sSql As String, sSql1 As String, sSubFunction As String = ""
        Try
            sSql = "select APMCM_SubFunctionID from Audit_APM_ChecksMatrix Where APMCM_CompID=" & iACID & " And APMCM_CustID=" & iCustID & " and APMCM_FunctionID=" & iFunID & " And APMCM_YearID=" & iYearID & " "
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dtTab.Rows.Count - 1
                sSubFunction = sSubFunction & "," & dtTab.Rows(i)("APMCM_SubFunctionID")
            Next
            If IsNothing(sSubFunction) = False Then
                If sSubFunction.StartsWith(",") Then
                    sSubFunction = sSubFunction.Remove(0, 1)
                End If
                If sSubFunction.EndsWith(",") Then
                    sSubFunction = sSubFunction.Remove(Len(sSubFunction) - 1, 1)
                End If
                If sSubFunction <> "" Then
                    sSql1 = "select SEM_ID,SEM_NAME from MST_SUBENTITY_MASTER Where SEM_ID In (" & sSubFunction & ") And SEM_CompID=" & iACID & " order by SEM_NAME "
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql1)
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadResource(ByVal sAC As String, ByVal iACID As Integer, ByVal sAuditTeamsID As String, ByVal iFunOwnerID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            If sAuditTeamsID.StartsWith(",") = True Then
                sAuditTeamsID = sAuditTeamsID.Remove(0, 1)
            End If
            If sAuditTeamsID.EndsWith(",") = True Then
                sAuditTeamsID = sAuditTeamsID.Remove(Len(sAuditTeamsID) - 1, 1)
            End If
            If sAuditTeamsID > 0 Then
                sSql = "Select Usr_id,Usr_FullName from Sad_Userdetails Where Usr_COmpID=" & iACID & " And Usr_Id In (" & sAuditTeamsID & "," & iFunOwnerID & ") Order By Usr_FullName "
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditTaskProcessDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As String, ByVal iFunID As Integer, ByVal iCustID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtAP As New DataTable, dtMilestone As New DataTable
        Dim drRow As DataRow
        Dim i As Integer
        Dim sResource As String = "", sResourceID As String = ""
        Dim sArray As Array
        Try
            dt.Columns.Add("AuditPhaseTaskID")
            dt.Columns.Add("AuditPhaseTask")
            dt.Columns.Add("PlannedStartDate")
            dt.Columns.Add("PlannedEndDate")
            dt.Columns.Add("Resource")
            dt.Columns.Add("AuditStatus")
            dt.Columns.Add("FuntionID")
            dt.Columns.Add("SubFuntionID")
            If sType = "AT" Then
                sSql = "Select CMM_Desc,APM_APMTAStatus,Convert(Varchar(10),AAPM_PStartDate,103)AAPM_PStartDate,Convert(Varchar(10),AAPM_PEndDate,103)AAPM_PEndDate,AAPM_Resource,AAPM_AuditTaskID From Audit_APM_Assignment_Details"
                sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & ""
                sSql = sSql & " left Join Audit_APM_details On APM_ID=AAPM_AuditCodeID And APM_CompID=" & iACID & ""
                sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " "
                If iAuditID > 0 Then
                    sSql = sSql & "and AAPM_AuditCodeID=" & iAuditID & ""
                End If
                If iCustID > 0 Then
                    sSql = sSql & "and AAPM_CustID=" & iCustID & ""
                End If
                dtMilestone = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtMilestone.Rows.Count - 1
                    drRow = dt.NewRow
                    drRow("AuditPhaseTaskID") = dtMilestone.Rows(i)("AAPM_AuditTaskID")
                    If IsDBNull(dtMilestone.Rows(i)("APM_APMTAStatus")) = False Then
                        drRow("AuditStatus") = dtMilestone.Rows(i)("APM_APMTAStatus")
                    End If
                    If IsDBNull(dtMilestone.Rows(i)("CMM_Desc")) = False Then
                        drRow("AuditPhaseTask") = dtMilestone.Rows(i)("CMM_Desc")
                    End If
                    drRow("PlannedStartDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PStartDate")) = False Then
                        drRow("PlannedStartDate") = dtMilestone.Rows(i).Item("AAPM_PStartDate")
                    End If
                    drRow("PlannedEndDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PEndDate")) = False Then
                        drRow("PlannedEndDate") = dtMilestone.Rows(i).Item("AAPM_PEndDate")
                    End If
                    sResourceID = "" : sResource = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_Resource")) = False Then
                        sResourceID = dtMilestone.Rows(i)("AAPM_Resource")
                        If sResourceID.StartsWith(",") = True Then
                            sResourceID = sResourceID.Remove(0, 1)
                        End If
                        If sResourceID.EndsWith(",") = True Then
                            sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
                        End If
                        If sResourceID <> "" Then
                            sArray = sResourceID.Split(",")
                            For k = 0 To sArray.Length - 1
                                If sArray(k) <> "" Then
                                    sResource = sResource & "," & objDBL.SQLExecuteScalar(sAC, "Select Usr_FullName from Sad_UserDetails where Usr_ID=" & sArray(k) & "")
                                End If
                            Next
                            If sResource.StartsWith(",") = True Then
                                sResource = sResource.Remove(0, 1)
                            End If
                            If sResource.EndsWith(",") = True Then
                                sResource = sResource.Remove(Len(sResourceID) - 1, 1)
                            End If
                        End If
                        drRow("Resource") = sResource
                    End If
                    dt.Rows.Add(drRow)
                Next
            ElseIf sType = "AP" Then
                sSql = ""
                sSql = "select PM_ENT_ID,PM_SEM_ID,PM_NAME,APM_APMTAStatus,Convert(Varchar(10),AAPM_PStartDate,103)AAPM_PStartDate,Convert(Varchar(10),AAPM_PEndDate,103)AAPM_PEndDate,AAPM_Resource,AAPM_AuditTaskID From Audit_APM_Assignment_Details"
                sSql = sSql & " Left Join Mst_process_MAster On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & " "
                sSql = sSql & " left Join Audit_APM_details On APM_ID=AAPM_AuditCodeID And APM_CompID=" & iACID & ""
                sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & ""
                If iAuditID > 0 Then
                    sSql = sSql & "and AAPM_AuditCodeID=" & iAuditID & ""
                End If
                If iCustID > 0 Then
                    sSql = sSql & "and AAPM_CustID=" & iCustID & ""
                End If
                dtMilestone = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtMilestone.Rows.Count - 1
                    drRow = dt.NewRow
                    drRow("AuditPhaseTaskID") = dtMilestone.Rows(i)("AAPM_AuditTaskID")
                    drRow("FuntionID") = dtMilestone.Rows(i)("PM_ENT_ID")
                    drRow("SubFuntionID") = dtMilestone.Rows(i)("PM_SEM_ID")
                    If IsDBNull(dtMilestone.Rows(i)("APM_APMTAStatus")) = False Then
                        drRow("AuditStatus") = dtMilestone.Rows(i)("APM_APMTAStatus")
                    End If
                    If IsDBNull(dtMilestone.Rows(i)("PM_NAME")) = False Then
                        drRow("AuditPhaseTask") = dtMilestone.Rows(i)("PM_NAME")
                    End If
                    drRow("PlannedStartDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PStartDate")) = False Then
                        drRow("PlannedStartDate") = dtMilestone.Rows(i)("AAPM_PStartDate")
                    End If
                    drRow("PlannedEndDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PEndDate")) = False Then
                        drRow("PlannedEndDate") = dtMilestone.Rows(i)("AAPM_PEndDate")
                    End If
                    sResourceID = "" : sResource = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_Resource")) = False Then
                        sResourceID = dtMilestone.Rows(i)("AAPM_Resource")
                        If sResourceID.StartsWith(",") = True Then
                            sResourceID = sResourceID.Remove(0, 1)
                        End If
                        If sResourceID.EndsWith(",") = True Then
                            sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
                        End If
                        If sResourceID <> "" Then
                            sArray = sResourceID.Split(",")
                            For k = 0 To sArray.Length - 1
                                If sArray(k) <> "" Then
                                    sResource = sResource & "," & objDBL.SQLExecuteScalar(sAC, "Select Usr_FullName from Sad_UserDetails where Usr_ID=" & sArray(k) & "")
                                End If
                            Next
                            If sResource.StartsWith(",") = True Then
                                sResource = sResource.Remove(0, 1)
                            End If
                            If sResource.EndsWith(",") = True Then
                                sResource = sResource.Remove(Len(sResourceID) - 1, 1)
                            End If
                        End If
                        drRow("Resource") = sResource
                    End If
                    dt.Rows.Add(drRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAPMAssignment_Details(ByVal sAC As String, ByVal objAPM As strAPM_Assignment_Details)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_AuditCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_AuditTaskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_AuditTaskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_AuditTaskType", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAPM.sAAPM_AuditTaskType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_PStartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAPM.dAAPM_PStartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_PEndDate", OleDb.OleDbType.Date, 4)
            ObjParam(iParamCount).Value = objAPM.dAAPM_PEndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_Resource", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAPM.sAAPM_Resource
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_UpdateBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_Updateby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("AAPM_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAPM.sAAPM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAPM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAAPM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_APM_Assignment_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteAPMAssignmentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAAPMID As Integer, ByVal iAudit As Integer)
        Dim sSql As String
        Try
            sSql = "Delete from Audit_APM_Assignment_Details Where AAPM_AuditTaskID=" & iAAPMID & " And AAPM_AuditCodeID=" & iAudit & " And AAPM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadAPMAssignmentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAAPMID As Integer, ByVal iAudit As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from Audit_APM_Assignment_Details Where AAPM_AuditTaskID=" & iAAPMID & " And AAPM_AuditCodeID=" & iAudit & " And AAPM_CompID=" & iACID & " order by AAPM_ID "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveEmployeesWithSearch(ByVal sAC As String, ByVal iACID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails where Usr_CompID=" & iACID & " And Usr_Node>0 And Usr_OrgnID>0 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L')"
            If sSearch <> "" Then
                sSql = sSql & " And (usr_FullName like '%" & sSearch & "%' OR usr_code like '%" & sSearch & "%')"
            End If
            sSql = sSql & " order by FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAPMPKIDStatusID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal sType As String) As String
        Dim sSql As String
        Try
            sSql = "Select "
            If sType = "PKID" Then
                sSql = sSql & " APM_ID "
            ElseIf sType = "StatusID" Then
                sSql = sSql & " APM_StatusID "
            End If
            sSql = sSql & " APM_StatusID From Audit_APM_Details Where APM_YearID=" & iYearID & " And APM_CustID=" & iCustID & " And APM_FunctionID=" & iFunID & " And APM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAPMPKIDStatusIDFromAuditID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal sType As String) As String
        Dim sSql As String
        Try
            sSql = "Select "
            If sType = "PKID" Then
                sSql = sSql & " APM_ID "
            ElseIf sType = "StatusID" Then
                sSql = sSql & " APM_StatusID "
            ElseIf sType = "FUNID" Then
                sSql = sSql & " APM_FunctionID "
            End If
            sSql = sSql & " APM_StatusID From Audit_APM_Details Where APM_YearID=" & iYearID & " And APM_CustID=" & iCustID & " And APM_ID=" & iAuditID & " And APM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAuditAPMCMChecksMatrix(ByVal sAC As String, ByVal objAPM As strAudit_APMCM_ChecksMatrix)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_APMPKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_APMPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_SubFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_SubFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_ProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_ProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_SubProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_SubProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_RiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_RiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_ControlID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_ControlID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_ChecksID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_ChecksID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_MMMID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_MMMID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("APMCM_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAPM.sAPMCM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APMCM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAPM.iAPMCM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_APM_ChecksMatrix", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCheckMasterDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAPMPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_APM_ChecksMatrix Set APMCM_Status='D' Where APMCM_APMPKID=" & iAPMPKID & " And APMCM_CustID=" & iCustID & " And APMCM_CompID=" & iACID & " And APMCM_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetSelectedSubFunctionInAPM(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAPMPKID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sType As String, ByVal sSubFunID As String) As String
        Dim sSql As String = "", sSFIsD As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Try
            If sType = "SubFun" Then
                sSql = "Select Distinct(APMCM_SubFunctionID) as APMCM_SubFunctionID From Audit_APM_ChecksMatrix Where APMCM_CompID=" & iACID & " And APMCM_YearID=" & iYearID & ""
                If iFunction > 0 Then
                    sSql = sSql & " And APMCM_FunctionID=" & iFunction & " "
                End If
                If iAPMPKID > 0 Then
                    sSql = sSql & " And APMCM_APMPKID=" & iAPMPKID & " "
                End If
                If iAPMPKID > 0 Then
                    sSql = sSql & " And APMCM_CustID=" & iCustID & " "
                End If
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sSFIsD = sSFIsD & "," & dt.Rows(i)("APMCM_SubFunctionID")
                Next
            End If
            Return sSFIsD
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedSubFunctionInAPMCR(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAPMPKID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sType As String, ByVal sSubFunID As String) As DataTable
        Dim sSql As String = "", sSFIsD As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RiskID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")

            sSql = "Select * From Audit_APM_ChecksMatrix Where APMCM_CompID=" & iACID & " And APMCM_YearID=" & iYearID & " And APMCM_Status<>'D' "
            If iFunction > 0 Then
                sSql = sSql & " And APMCM_FunctionID=" & iFunction & " "
            End If
            If iAPMPKID > 0 Then
                sSql = sSql & " And APMCM_APMPKID=" & iAPMPKID & " "
            End If
            If iAPMPKID > 0 Then
                sSql = sSql & " And APMCM_CustID=" & iCustID & " "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SubFunctionID") = dt.Rows(i)("APMCM_SubFunctionID")
                dr("ProcessID") = dt.Rows(i)("APMCM_ProcessID")
                dr("SubProcessID") = dt.Rows(i)("APMCM_SubProcessID")
                dr("RiskID") = dt.Rows(i)("APMCM_RiskID")
                dr("ControlID") = dt.Rows(i)("APMCM_ControlID")
                dr("ChecksID") = dt.Rows(i)("APMCM_ChecksID")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedChecksInAPM(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAPMPKID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sType As String, ByVal sSubFunID As String) As DataTable
        Dim sSql As String = "", sSFIsD As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RiskID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")

            sSql = "Select * From Audit_APM_ChecksMatrix Where APMCM_CompID=" & iACID & " And APMCM_YearID=" & iYearID & " And APMCM_Status<>'D' "
            If iFunction > 0 Then
                sSql = sSql & " And APMCM_FunctionID=" & iFunction & " "
            End If
            If iAPMPKID > 0 Then
                sSql = sSql & " And APMCM_APMPKID=" & iAPMPKID & " "
            End If
            If iAPMPKID > 0 Then
                sSql = sSql & " And APMCM_CustID=" & iCustID & " "
            End If
            If sSubFunID <> "" Then
                sSql = sSql & " And APMCM_SubFunctionID In(" & sSubFunID & ") "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("SubFunctionID") = dt.Rows(i)("APMCM_SubFunctionID")
                dr("ProcessID") = dt.Rows(i)("APMCM_ProcessID")
                dr("SubProcessID") = dt.Rows(i)("APMCM_SubProcessID")
                dr("RiskID") = dt.Rows(i)("APMCM_RiskID")
                dr("ControlID") = dt.Rows(i)("APMCM_ControlID")
                dr("ChecksID") = dt.Rows(i)("APMCM_ChecksID")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAPMAuditScope(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select APM_Objectives From Audit_APM_Details Where APM_ID=" & iAuditID & " And APM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer, ByVal sBranch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Dim sPartner As String(), sPartners As String, sPartnerName As String
        Dim sAuditor As String(), sAuditors As String, sAuditorName As String
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("CustomerName")
            dtTab.Columns.Add("Functions")
            dtTab.Columns.Add("AuditCode")
            dtTab.Columns.Add("Location")
            dtTab.Columns.Add("TentativeStartDate")
            dtTab.Columns.Add("TentativeEndDate")
            dtTab.Columns.Add("EstimatedDays")
            dtTab.Columns.Add("Auditors")
            dtTab.Columns.Add("Partners")
            dtTab.Columns.Add("AuditTeam")
            dtTab.Columns.Add("Objectives")

            sSql = "Select Ent_EntityName,Cust_Name,b.Mas_CustID,b.Mas_Description as BranchName,APM_Objectives,APM_TStartDate,APM_TEndDate,APM_EstimatedEffortDays,a.Mas_Description as Auditors,"
            sSql = sSql & " APM_PartnersID,APM_AuditTeamsID from Audit_APM_Details Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Entity_Master On ENT_ID=APM_FunctionID and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUST_LOCATION b On b.Mas_CustID=Cust_ID And b.Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_GrpOrLvl_General_Master a On APM_AuditorsRoleID=a.Mas_ID And a.Mas_Delflag='A' and a.Mas_CompID=" & iACID & ""
            sSql = sSql & " Where APM_CompID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APM_ID=" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APM_FunctionID=" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APM_CustID=" & iCustID & ""
            End If
            If sBranch <> "" Then
                sSql = sSql & " and b.Mas_Description='" & sBranch & "'"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i).Item("Cust_Name")) = False Then
                    drow("CustomerName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Cust_Name"))
                End If
                If IsDBNull(dt.Rows(i).Item("Ent_EntityName")) = False Then
                    drow("Functions") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Ent_EntityName"))
                End If
                drow("AuditCode") = ""
                If IsDBNull(dt.Rows(i).Item("BranchName")) = False Then
                    drow("Location") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("BranchName"))
                End If
                If IsDBNull(dt.Rows(i).Item("APM_TStartDate")) = False Then
                    drow("TentativeStartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TStartDate"), "F")
                End If
                If IsDBNull(dt.Rows(i).Item("APM_TEndDate")) = False Then
                    drow("TentativeEndDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TEndDate"), "F")
                End If
                If IsDBNull(dt.Rows(i).Item("APM_EstimatedEffortDays")) = False Then
                    drow("EstimatedDays") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_EstimatedEffortDays"))
                End If
                If IsDBNull(dt.Rows(i).Item("Auditors")) = False Then
                    drow("Auditors") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Auditors"))
                End If
                If IsDBNull(dt.Rows(i)("APM_PartnersID")) = False Then
                    If dt.Rows(i)("APM_PartnersID") <> "" Then
                        sPartner = dt.Rows(i)("APM_PartnersID").Split(",")
                        If sPartner.Length > 0 Then
                            sPartners = ""
                            For j = 1 To sPartner.Length - 2
                                sPartnerName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, sPartner(j))
                                sPartners = sPartners & ", " & sPartnerName
                            Next
                            sPartners = sPartners.Remove(0, 2)
                            drow("Partners") = sPartners
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(i)("APM_AuditTeamsID")) = False Then
                    If dt.Rows(i)("APM_AuditTeamsID") <> "" Then
                        sAuditor = dt.Rows(i)("APM_AuditTeamsID").Split(",")
                        If sAuditor.Length > 0 Then
                            sAuditors = ""
                            For j = 1 To sAuditor.Length - 2
                                sAuditorName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, sAuditor(j))
                                sAuditors = sAuditors & ", " & sAuditorName
                            Next
                            sAuditors = sAuditors.Remove(0, 2)
                            drow("AuditTeam") = sAuditors
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(i).Item("APM_Objectives")) = False Then
                    drow("Objectives") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_Objectives"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMSubFunDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SEM_Name")

            sSql = "Select SEM_Name from Audit_APM_Details Left Join Audit_APM_ChecksMatrix On APMCM_APMPKID=APM_ID And APMCM_YearID=" & iYearID & ""
            sSql = sSql & " And APMCM_CompID=" & iACID & " Left Join MST_SUBENTITY_MASTER On APMCM_SubFunctionID=SEM_ID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Where APM_CustID=" & iCustID & " And APM_FunctionID=" & iFunction & " And APM_YearID=" & iYearID & " And APM_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i).Item("SEM_Name")) = False Then
                    drow("SEM_Name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_Name"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMMappingDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SEM_Name")
            dtTab.Columns.Add("PM_Name")
            dtTab.Columns.Add("SPM_Name")
            dtTab.Columns.Add("CHK_CheckName")

            sSql = "Select SEM_Name,PM_Name,SPM_Name,CHK_CheckName from Audit_APM_Details Left Join Audit_APM_ChecksMatrix On APMCM_APMPKID=APM_ID "
            sSql = sSql & " And APMCM_YearID=" & iYearID & " And APMCM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On APMCM_SubFunctionID=SEM_ID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On APMCM_ProcessID=PM_ID And APMCM_SubFunctionID=PM_SEM_ID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On APMCM_SubProcessID=SPM_ID And APMCM_ProcessID=SPM_PM_ID And APMCM_SubFunctionID=SPM_SEM_ID"
            sSql = sSql & " And PM_CompID=" & iACID & " Left Join MST_Checks_Master On APMCM_ControlID=CHK_ControlID And APMCM_ChecksID=CHK_ID And CHK_CompID=" & iACID & ""
            sSql = sSql & " Where APM_CustID=" & iCustID & " And APM_FunctionID=" & iFunction & " And APM_YearID=" & iYearID & " And APM_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i).Item("SEM_Name")) = False Then
                    drow("SEM_Name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_Name"))
                End If
                If IsDBNull(dt.Rows(i).Item("PM_Name")) = False Then
                    drow("PM_Name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PM_Name"))
                End If
                If IsDBNull(dt.Rows(i).Item("SPM_Name")) = False Then
                    drow("SPM_Name") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SPM_Name"))
                End If
                If IsDBNull(dt.Rows(i).Item("CHK_CheckName")) = False Then
                    drow("CHK_CheckName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CHK_CheckName"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMCRDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Dim sPartner As String(), sPartners As String, sPartnerName As String
        Dim sAuditor As String(), sAuditors As String, sAuditorName As String
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("CustomerName")
            dtTab.Columns.Add("Functions")
            dtTab.Columns.Add("Location")
            dtTab.Columns.Add("TentativeStartDate")
            dtTab.Columns.Add("TentativeEndDate")
            dtTab.Columns.Add("EstimatedDays")
            dtTab.Columns.Add("Auditors")
            dtTab.Columns.Add("Partners")
            dtTab.Columns.Add("AuditTeam")
            dtTab.Columns.Add("Objectives")
            dtTab.Columns.Add("CustomerRemarks")
            dtTab.Columns.Add("AuditorRemarks")
            dtTab.Columns.Add("AuditConfirmed")
            dtTab.Columns.Add("HConfirmedDate")
            dtTab.Columns.Add("ConfirmedDate")

            sSql = "Select Ent_EntityName,Cust_Name,c.Mas_Description as BranchName,APM_Objectives,APM_TStartDate,APM_TEndDate,APM_EstimatedEffortDays,APM_AuditTeamsID,"
            sSql = sSql & " APM_Audit_Confirm_Yes,APM_Audit_Confirm,APM_PartnersID,b.Mas_Description as AuditorName,APM_CustomerRemarks,APM_AuditorsRemarks"
            sSql = sSql & " from Audit_APM_Details Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Entity_Master On ENT_ID=APM_FunctionID and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_GrpOrLvl_General_Master b On b.Mas_ID=APM_AuditorsRoleID And b.Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUST_LOCATION c On c.Mas_ID=APM_BranchID And c.Mas_CompID=" & iACID & ""
            sSql = sSql & " Where APM_APMStatus='Submitted' And APM_CompID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APM_ID=" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APM_FunctionID=" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APM_CustID=" & iCustID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i).Item("Cust_Name")) = False Then
                    drow("CustomerName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Cust_Name"))
                End If
                If IsDBNull(dt.Rows(i).Item("Ent_EntityName")) = False Then
                    drow("Functions") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Ent_EntityName"))
                End If
                If IsDBNull(dt.Rows(i).Item("BranchName")) = False Then
                    drow("Location") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("BranchName"))
                End If
                If IsDBNull(dt.Rows(i).Item("APM_TStartDate")) = False Then
                    drow("TentativeStartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TStartDate"), "F")
                End If
                If IsDBNull(dt.Rows(i).Item("APM_TEndDate")) = False Then
                    drow("TentativeEndDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TEndDate"), "F")
                End If
                If IsDBNull(dt.Rows(i).Item("APM_EstimatedEffortDays")) = False Then
                    drow("EstimatedDays") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_EstimatedEffortDays"))
                End If
                If IsDBNull(dt.Rows(i).Item("AuditorName")) = False Then
                    drow("Auditors") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("AuditorName"))
                End If
                If IsDBNull(dt.Rows(i)("APM_PartnersID")) = False Then
                    If dt.Rows(i)("APM_PartnersID") <> "" Then
                        sPartner = dt.Rows(i)("APM_PartnersID").Split(",")
                        If sPartner.Length > 0 Then
                            sPartners = ""
                            For j = 1 To sPartner.Length - 2
                                sPartnerName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, sPartner(j))
                                sPartners = sPartners & ", " & sPartnerName
                            Next
                            sPartners = sPartners.Remove(0, 2)
                            drow("Partners") = sPartners
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(i)("APM_AuditTeamsID")) = False Then
                    If dt.Rows(i)("APM_AuditTeamsID") <> "" Then
                        sAuditor = dt.Rows(i)("APM_AuditTeamsID").Split(",")
                        If sAuditor.Length > 0 Then
                            sAuditors = ""
                            For j = 1 To sAuditor.Length - 2
                                sAuditorName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, sAuditor(j))
                                sAuditors = sAuditors & ", " & sAuditorName
                            Next
                            sAuditors = sAuditors.Remove(0, 2)
                            drow("AuditTeam") = sAuditors
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(i).Item("APM_Objectives")) = False Then
                    drow("Objectives") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_Objectives"))
                End If
                If IsDBNull(dt.Rows(i).Item("APM_CustomerRemarks")) = False Then
                    drow("CustomerRemarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_CustomerRemarks"))
                End If
                If IsDBNull(dt.Rows(i).Item("APM_AuditorsRemarks")) = False Then
                    drow("AuditorRemarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_AuditorsRemarks"))
                End If
                drow("HConfirmedDate") = "" : drow("ConfirmedDate") = ""
                If IsDBNull(dt.Rows(i).Item("APM_Audit_Confirm")) = False Then
                    If (dt.Rows(i)("APM_Audit_Confirm") = 1) Then
                        drow("AuditConfirmed") = "Yes"
                        If IsDBNull(dt.Rows(i).Item("APM_Audit_Confirm_Yes")) = False Then
                            drow("HConfirmedDate") = "If 'Yes',Then Confirm Date : "
                            drow("ConfirmedDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_Audit_Confirm_Yes"), "F")
                        End If
                    Else
                        drow("AuditConfirmed") = "No"
                    End If
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAPMTADetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim drow As DataRow
        Dim sPartner As String(), sPartners As String, sPartnerName As String
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("CustomerName")
            dtTab.Columns.Add("Functions")
            dtTab.Columns.Add("AuditCode")
            dtTab.Columns.Add("Location")
            dtTab.Columns.Add("TentativeStartDate")
            dtTab.Columns.Add("TentativeEndDate")
            dtTab.Columns.Add("EstimatedDays")
            dtTab.Columns.Add("Auditors")
            dtTab.Columns.Add("Partners")
            dtTab.Columns.Add("Objectives")

            sSql = "Select Ent_EntityName,Cust_Name,APM_AuditCode,c.Mas_Description as BranchName,APM_Objectives,APM_TStartDate,APM_TEndDate,APM_EstimatedEffortDays,APM_AuditTeamsID,"
            sSql = sSql & " APM_Audit_Confirm_Yes,APM_Audit_Confirm,APM_PartnersID,b.Mas_Description as AuditorName,APM_CustomerRemarks,APM_AuditorsRemarks"
            sSql = sSql & " from Audit_APM_Details Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Entity_Master On ENT_ID=APM_FunctionID and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_GrpOrLvl_General_Master b On b.Mas_ID=APM_AuditorsRoleID And b.Mas_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUST_LOCATION c On c.Mas_ID=APM_BranchID And c.Mas_CompID=" & iACID & ""
            sSql = sSql & " Where APM_APMCRStatus='Submitted' And APM_CompID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APM_ID=" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APM_FunctionID=" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APM_CustID=" & iCustID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow
                drow("SrNo") = i + 1
                If IsDBNull(dt.Rows(i).Item("Cust_Name")) = False Then
                    drow("CustomerName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Cust_Name"))
                End If
                If IsDBNull(dt.Rows(i).Item("Ent_EntityName")) = False Then
                    drow("Functions") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Ent_EntityName"))
                End If
                If IsDBNull(dt.Rows(i).Item("APM_AuditCode")) = False Then
                    drow("AuditCode") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_AuditCode"))
                End If
                If IsDBNull(dt.Rows(i).Item("BranchName")) = False Then
                    drow("Location") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("BranchName"))
                End If
                If IsDBNull(dt.Rows(i).Item("APM_TStartDate")) = False Then
                    drow("TentativeStartDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TStartDate"), "F")
                End If
                If IsDBNull(dt.Rows(i).Item("APM_TEndDate")) = False Then
                    drow("TentativeEndDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("APM_TEndDate"), "F")
                End If
                If IsDBNull(dt.Rows(i).Item("APM_EstimatedEffortDays")) = False Then
                    drow("EstimatedDays") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_EstimatedEffortDays"))
                End If
                If IsDBNull(dt.Rows(i).Item("AuditorName")) = False Then
                    drow("Auditors") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("AuditorName"))
                End If
                If IsDBNull(dt.Rows(i)("APM_PartnersID")) = False Then
                    If dt.Rows(i)("APM_PartnersID") <> "" Then
                        sPartner = dt.Rows(i)("APM_PartnersID").Split(",")
                        If sPartner.Length > 0 Then
                            sPartners = ""
                            For j = 1 To sPartner.Length - 2
                                sPartnerName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, sPartner(j))
                                sPartners = sPartners & ", " & sPartnerName
                            Next
                            sPartners = sPartners.Remove(0, 2)
                            drow("Partners") = sPartners
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(i).Item("APM_Objectives")) = False Then
                    drow("Objectives") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("APM_Objectives"))
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditTaskProcessDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As String, ByVal iFunID As Integer, ByVal iCustID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtAP As New DataTable, dtMilestone As New DataTable
        Dim drRow As DataRow
        Dim i As Integer
        Dim sResource As String = "", sResourceID As String = ""
        Dim sArray As Array
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AuditPhaseTaskID")
            dt.Columns.Add("AuditPhaseTask")
            dt.Columns.Add("PlannedStartDate")
            dt.Columns.Add("PlannedEndDate")
            dt.Columns.Add("Resource")
            dt.Columns.Add("AuditStatus")
            dt.Columns.Add("FuntionID")
            dt.Columns.Add("SubFuntionID")

            If sType = "AT" Then
                sSql = "Select CMM_Desc,APM_APMTAStatus,Convert(Varchar(10),AAPM_PStartDate,103)AAPM_PStartDate,Convert(Varchar(10),AAPM_PEndDate,103)AAPM_PEndDate,AAPM_Resource,AAPM_AuditTaskID From Audit_APM_Assignment_Details"
                sSql = sSql & " Left Join Content_Management_Master On CMM_ID=AAPM_AuditTaskID And CMM_CompID=" & iACID & ""
                sSql = sSql & " left Join Audit_APM_details On APM_ID=AAPM_AuditCodeID And APM_CompID=" & iACID & ""
                sSql = sSql & " Where AAPM_AuditTaskType='AT' and AAPM_CompID=" & iACID & " "
                If iAuditID > 0 Then
                    sSql = sSql & "and AAPM_AuditCodeID=" & iAuditID & ""
                End If
                If iCustID > 0 Then
                    sSql = sSql & "and AAPM_CustID=" & iCustID & ""
                End If
                dtMilestone = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtMilestone.Rows.Count - 1
                    drRow = dt.NewRow
                    drRow("SrNo") = i + 1
                    drRow("AuditPhaseTaskID") = dtMilestone.Rows(i)("AAPM_AuditTaskID")
                    If IsDBNull(dtMilestone.Rows(i)("APM_APMTAStatus")) = False Then
                        drRow("AuditStatus") = dtMilestone.Rows(i)("APM_APMTAStatus")
                    End If
                    If IsDBNull(dtMilestone.Rows(i)("CMM_Desc")) = False Then
                        drRow("AuditPhaseTask") = dtMilestone.Rows(i)("CMM_Desc")
                    End If
                    drRow("PlannedStartDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PStartDate")) = False Then
                        drRow("PlannedStartDate") = dtMilestone.Rows(i).Item("AAPM_PStartDate")
                    End If
                    drRow("PlannedEndDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PEndDate")) = False Then
                        drRow("PlannedEndDate") = dtMilestone.Rows(i).Item("AAPM_PEndDate")
                    End If
                    sResourceID = "" : sResource = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_Resource")) = False Then
                        sResourceID = dtMilestone.Rows(i)("AAPM_Resource")
                        If sResourceID.StartsWith(",") = True Then
                            sResourceID = sResourceID.Remove(0, 1)
                        End If
                        If sResourceID.EndsWith(",") = True Then
                            sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
                        End If
                        If sResourceID <> "" Then
                            sArray = sResourceID.Split(",")
                            For k = 0 To sArray.Length - 1
                                If sArray(k) <> "" Then
                                    sResource = sResource & "," & objDBL.SQLExecuteScalar(sAC, "Select Usr_FullName from Sad_UserDetails where Usr_ID=" & sArray(k) & "")
                                End If
                            Next
                            If sResource.StartsWith(",") = True Then
                                sResource = sResource.Remove(0, 1)
                            End If
                            If sResource.EndsWith(",") = True Then
                                sResource = sResource.Remove(Len(sResourceID) - 1, 1)
                            End If
                        End If
                        drRow("Resource") = sResource
                    End If
                    dt.Rows.Add(drRow)
                Next
            ElseIf sType = "AP" Then
                sSql = ""
                sSql = "select PM_ENT_ID,PM_SEM_ID,PM_NAME,APM_APMTAStatus,Convert(Varchar(10),AAPM_PStartDate,103)AAPM_PStartDate,Convert(Varchar(10),AAPM_PEndDate,103)AAPM_PEndDate,AAPM_Resource,AAPM_AuditTaskID From Audit_APM_Assignment_Details"
                sSql = sSql & " Left Join Mst_process_MAster On PM_ID=AAPM_AuditTaskID And AAPM_CompID=" & iACID & " "
                sSql = sSql & " left Join Audit_APM_details On APM_ID=AAPM_AuditCodeID And APM_CompID=" & iACID & ""
                sSql = sSql & " Where AAPM_AuditTaskType='AP' and AAPM_CompID=" & iACID & ""
                If iAuditID > 0 Then
                    sSql = sSql & "and AAPM_AuditCodeID=" & iAuditID & ""
                End If
                If iCustID > 0 Then
                    sSql = sSql & "and AAPM_CustID=" & iCustID & ""
                End If
                dtMilestone = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtMilestone.Rows.Count - 1
                    drRow = dt.NewRow
                    drRow("SrNo") = i + 1
                    drRow("AuditPhaseTaskID") = dtMilestone.Rows(i)("AAPM_AuditTaskID")
                    drRow("FuntionID") = dtMilestone.Rows(i)("PM_ENT_ID")
                    drRow("SubFuntionID") = dtMilestone.Rows(i)("PM_SEM_ID")
                    If IsDBNull(dtMilestone.Rows(i)("APM_APMTAStatus")) = False Then
                        drRow("AuditStatus") = dtMilestone.Rows(i)("APM_APMTAStatus")
                    End If
                    If IsDBNull(dtMilestone.Rows(i)("PM_NAME")) = False Then
                        drRow("AuditPhaseTask") = dtMilestone.Rows(i)("PM_NAME")
                    End If
                    drRow("PlannedStartDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PStartDate")) = False Then
                        drRow("PlannedStartDate") = dtMilestone.Rows(i)("AAPM_PStartDate")
                    End If
                    drRow("PlannedEndDate") = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_PEndDate")) = False Then
                        drRow("PlannedEndDate") = dtMilestone.Rows(i)("AAPM_PEndDate")
                    End If
                    sResourceID = "" : sResource = ""
                    If IsDBNull(dtMilestone.Rows(i)("AAPM_Resource")) = False Then
                        sResourceID = dtMilestone.Rows(i)("AAPM_Resource")
                        If sResourceID.StartsWith(",") = True Then
                            sResourceID = sResourceID.Remove(0, 1)
                        End If
                        If sResourceID.EndsWith(",") = True Then
                            sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
                        End If
                        If sResourceID <> "" Then
                            sArray = sResourceID.Split(",")
                            For k = 0 To sArray.Length - 1
                                If sArray(k) <> "" Then
                                    sResource = sResource & "," & objDBL.SQLExecuteScalar(sAC, "Select Usr_FullName from Sad_UserDetails where Usr_ID=" & sArray(k) & "")
                                End If
                            Next
                            If sResource.StartsWith(",") = True Then
                                sResource = sResource.Remove(0, 1)
                            End If
                            If sResource.EndsWith(",") = True Then
                                sResource = sResource.Remove(Len(sResourceID) - 1, 1)
                            End If
                        End If
                        drRow("Resource") = sResource
                    End If
                    dt.Rows.Add(drRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadResourceAnnualPlan(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String = ""
        Dim dtuser As New DataTable
        Dim sResourceID As String = "", sResource As String = ""
        Try
            sSql = "Select AAP_ResourceID FRom Audit_AnnualPlan Where AAP_CompID=" & iACID & " And AAP_FunID=" & iFunID & ""
            dtuser = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtuser.Rows.Count - 1
                If IsDBNull(dtuser.Rows(i)("AAP_ResourceID")) = False Then
                    sResourceID = dtuser.Rows(i)("AAP_ResourceID")
                End If
                If sResourceID.StartsWith(",") = True Then
                    sResourceID = sResourceID.Remove(0, 1)
                End If
                If sResourceID.EndsWith(",") = True Then
                    sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
                End If
                sResource = sResource & "," & sResourceID
            Next
            Return sResource & ","
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocId As Integer, ByVal sMandatory As String) As DataTable
        Dim sSql As String
        Dim dtDecs As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow

        dtDecs.Columns.Add("DescId")
        dtDecs.Columns.Add("Descriptor")
        dtDecs.Columns.Add("DataType")
        dtDecs.Columns.Add("Size")
        dtDecs.Columns.Add("Mandatory")
        dtDecs.Columns.Add("Values")
        dtDecs.Columns.Add("Validator")
        Try
            sSql = "Select EDD_DPTRID,EDD_SIZE,EDD_ISREQUIRED,EDD_VALUES,EDD_Validate,DESC_Name,Dt_Name From EDT_DOCTYPE_LINK"
            sSql = sSql & " Left Join EDT_DESCRIPTIOS On DES_ID=EDD_DPTRID Left Join EDT_DESC_TYPE On DT_ID=DESC_DATATYPE "
            sSql = sSql & " Where EDD_DOCTYPEID=" & iDocId & ""
            If sMandatory = "Y" Then
                sSql = sSql & " And EDD_ISREQUIRED='Y'"
            End If
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dtDecs.NewRow
                    If IsDBNull(dtdetails.Rows(i)("EDD_DPTRID")) = False Then
                        dRow("DescId") = dtdetails.Rows(i)("EDD_DPTRID")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_Name")) = False Then
                        dRow("Descriptor") = dtdetails.Rows(i)("DESC_Name")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("Dt_Name")) = False Then
                        dRow("DataType") = dtdetails.Rows(i)("Dt_Name")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_SIZE")) = False Then
                        dRow("Size") = dtdetails.Rows(i)("EDD_SIZE")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_ISREQUIRED")) = False Then
                        If dtdetails.Rows(i)("EDD_ISREQUIRED") = "Q" Then
                            dRow("Mandatory") = "N"
                        Else
                            dRow("Mandatory") = "Y"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_VALUES")) = False Then
                        dRow("Values") = dtdetails.Rows(i)("EDD_VALUES")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_Validate")) = False Then
                        If dtdetails.Rows(i)("EDD_Validate") = "N" Then
                            dRow("Validator") = "N"
                        Else
                            dRow("Validator") = "Y"
                        End If
                    End If
                    dtDecs.Rows.Add(dRow)
                Next
            End If
            Return dtDecs
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select APM_PGEDetailId From Audit_APM_details Where APM_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " APM_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " APM_ID=" & iAuditID & " And"
            End If
            sSql = sSql & " APM_CustID=" & iCustID & " And APM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
