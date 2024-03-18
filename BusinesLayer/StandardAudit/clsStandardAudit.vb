Public Structure strStandardAudit_Schedule
    Private SA_ID As Integer
    Private SA_AuditNo As String
    Private SA_CustID As Integer
    Private SA_YearID As Integer
    Private SA_AuditTypeID As Integer
    Private SA_PartnerID As String
    Private SA_ReviewPartnerID As String
    Private SA_AdditionalSupportEmployeeID As String
    Private SA_ScopeOfAudit As String
    Private SA_Status As Integer
    Private SA_AttachID As Integer
    Private SA_StartDate As String
    Private SA_ExpCompDate As String
    Private SA_RptRvDate As String
    Private SA_RptFilDate As String
    Private SA_MRSDate As String
    Private SA_CrBy As Integer
    Private SA_UpdatedBy As Integer
    Private SA_IPAddress As String
    Private SA_CompID As Integer
    Public Property iSA_ID() As Integer
        Get
            Return (SA_ID)
        End Get
        Set(ByVal Value As Integer)
            SA_ID = Value
        End Set
    End Property
    Public Property sSA_AuditNo() As String
        Get
            Return (SA_AuditNo)
        End Get
        Set(ByVal Value As String)
            SA_AuditNo = Value
        End Set
    End Property
    Public Property iSA_CustID() As Integer
        Get
            Return (SA_CustID)
        End Get
        Set(ByVal Value As Integer)
            SA_CustID = Value
        End Set
    End Property
    Public Property iSA_YearID() As Integer
        Get
            Return (SA_YearID)
        End Get
        Set(ByVal Value As Integer)
            SA_YearID = Value
        End Set
    End Property
    Public Property iSA_AuditTypeID() As Integer
        Get
            Return (SA_AuditTypeID)
        End Get
        Set(ByVal Value As Integer)
            SA_AuditTypeID = Value
        End Set
    End Property
    Public Property sSA_PartnerID() As String
        Get
            Return (SA_PartnerID)
        End Get
        Set(ByVal Value As String)
            SA_PartnerID = Value
        End Set
    End Property
    Public Property sSA_ReviewPartnerID() As String
        Get
            Return (SA_ReviewPartnerID)
        End Get
        Set(ByVal Value As String)
            SA_ReviewPartnerID = Value
        End Set
    End Property
    Public Property sSA_AdditionalSupportEmployeeID() As String
        Get
            Return (SA_AdditionalSupportEmployeeID)
        End Get
        Set(ByVal Value As String)
            SA_AdditionalSupportEmployeeID = Value
        End Set
    End Property
    Public Property sSA_ScopeOfAudit() As String
        Get
            Return (SA_ScopeOfAudit)
        End Get
        Set(ByVal Value As String)
            SA_ScopeOfAudit = Value
        End Set
    End Property
    Public Property iSA_Status() As Integer
        Get
            Return (SA_Status)
        End Get
        Set(ByVal Value As Integer)
            SA_Status = Value
        End Set
    End Property
    Public Property iSA_AttachID() As Integer
        Get
            Return (SA_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SA_AttachID = Value
        End Set
    End Property
    Public Property dSA_StartDate() As Date
        Get
            Return (SA_StartDate)
        End Get
        Set(ByVal Value As Date)
            SA_StartDate = Value
        End Set
    End Property
    Public Property dSA_ExpCompDate() As Date
        Get
            Return (SA_ExpCompDate)
        End Get
        Set(ByVal Value As Date)
            SA_ExpCompDate = Value
        End Set
    End Property
    Public Property dSA_RptRvDate() As Date
        Get
            Return (SA_RptRvDate)
        End Get
        Set(ByVal Value As Date)
            SA_RptRvDate = Value
        End Set
    End Property
    Public Property dSA_RptFilDate() As Date
        Get
            Return (SA_RptFilDate)
        End Get
        Set(ByVal Value As Date)
            SA_RptFilDate = Value
        End Set
    End Property
    Public Property dSA_MRSDate() As Date
        Get
            Return (SA_MRSDate)
        End Get
        Set(ByVal Value As Date)
            SA_MRSDate = Value
        End Set
    End Property
    Public Property iSA_CrBy() As Integer
        Get
            Return (SA_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SA_CrBy = Value
        End Set
    End Property
    Public Property iSA_UpdatedBy() As Integer
        Get
            Return (SA_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SA_UpdatedBy = Value
        End Set
    End Property
    Public Property sSA_IPAddress() As String
        Get
            Return (SA_IPAddress)
        End Get
        Set(ByVal Value As String)
            SA_IPAddress = Value
        End Set
    End Property
    Public Property iSA_CompID() As Integer
        Get
            Return (SA_CompID)
        End Get
        Set(ByVal Value As Integer)
            SA_CompID = Value
        End Set
    End Property
    Private AT_ID As Integer
    Private AT_CustId As Integer
    Private AT_AuditId As Integer
    Private AT_Heading As Integer
    Private AT_CheckpointId As String
    Private AT_EmpId As Integer
    Private AT_WorkType As Integer
    Private AT_HrPrDay As Integer
    Private AT_StartDate As DateTime
    Private AT_EndDate As DateTime
    Private AT_TotalHr As Integer
    Private AT_Comments As String
    Private AT_Status As String
    Private AT_CRBY As Integer
    Private AT_UPDATEDBY As Integer
    Private AT_IPAddress As String
    Private AT_CompId As Integer
    Public Property iAT_ID() As Integer
        Get
            Return (AT_ID)
        End Get
        Set(ByVal Value As Integer)
            AT_ID = Value
        End Set
    End Property
    Public Property iAT_CustId() As Integer
        Get
            Return (AT_CustId)
        End Get
        Set(ByVal Value As Integer)
            AT_CustId = Value
        End Set
    End Property
    Public Property iAT_AuditId() As Integer
        Get
            Return (AT_AuditId)
        End Get
        Set(ByVal Value As Integer)
            AT_AuditId = Value
        End Set
    End Property
    Public Property iAT_Heading() As Integer
        Get
            Return (AT_Heading)
        End Get
        Set(ByVal Value As Integer)
            AT_Heading = Value
        End Set
    End Property
    Public Property sAT_CheckpointId() As String
        Get
            Return (AT_CheckpointId)
        End Get
        Set(ByVal Value As String)
            AT_CheckpointId = Value
        End Set
    End Property
    Public Property iAT_EmpId() As Integer
        Get
            Return (AT_EmpId)
        End Get
        Set(ByVal Value As Integer)
            AT_EmpId = Value
        End Set
    End Property
    Public Property iAT_WorkType() As Integer
        Get
            Return (AT_WorkType)
        End Get
        Set(ByVal Value As Integer)
            AT_WorkType = Value
        End Set
    End Property
    Public Property iAT_HrPrDay() As Integer
        Get
            Return (AT_HrPrDay)
        End Get
        Set(ByVal Value As Integer)
            AT_HrPrDay = Value
        End Set
    End Property
    Public Property dAT_StartDate() As DateTime
        Get
            Return (AT_StartDate)
        End Get
        Set(ByVal Value As DateTime)
            AT_StartDate = Value
        End Set
    End Property
    Public Property dAT_EndDate() As DateTime
        Get
            Return (AT_EndDate)
        End Get
        Set(ByVal Value As DateTime)
            AT_EndDate = Value
        End Set
    End Property
    Public Property iAT_TotalHr() As Integer
        Get
            Return (AT_TotalHr)
        End Get
        Set(ByVal Value As Integer)
            AT_TotalHr = Value
        End Set
    End Property
    Public Property sAT_Comments() As String
        Get
            Return (AT_Comments)
        End Get
        Set(ByVal Value As String)
            AT_Comments = Value
        End Set
    End Property
    Public Property sAT_Status() As String
        Get
            Return (AT_Status)
        End Get
        Set(ByVal Value As String)
            AT_Status = Value
        End Set
    End Property
    Public Property iAT_CRBY() As Integer
        Get
            Return (AT_CRBY)
        End Get
        Set(ByVal Value As Integer)
            AT_CRBY = Value
        End Set
    End Property
    Public Property iAT_UPDATEDBY() As Integer
        Get
            Return (AT_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            AT_UPDATEDBY = Value
        End Set
    End Property
    Public Property sAT_IPAddress() As String
        Get
            Return (AT_IPAddress)
        End Get
        Set(ByVal Value As String)
            AT_IPAddress = Value
        End Set
    End Property
    Public Property iAT_CompId() As Integer
        Get
            Return (AT_CompId)
        End Get
        Set(ByVal Value As Integer)
            AT_CompId = Value
        End Set
    End Property
End Structure
Public Structure strStandardAudit_Checklist_Details
    Private SACD_ID As Integer
    Private SACD_CustId As Integer
    Private SACD_AuditId As Integer
    Private SACD_AuditType As Integer
    Private SACD_Heading As String
    Private SACD_CheckpointId As String
    Private SACD_EmpId As Integer
    Private SACD_WorkType As Integer
    Private SACD_HrPrDay As String
    Private SACD_StartDate As DateTime
    Private SACD_EndDate As DateTime
    Private SACD_TotalHr As String
    Private SACD_CRBY As Integer
    Private SACD_UPDATEDBY As Integer
    Private SACD_IPAddress As String
    Private SACD_CompID As Integer
    Public Property iSACD_ID() As Integer
        Get
            Return (SACD_ID)
        End Get
        Set(ByVal Value As Integer)
            SACD_ID = Value
        End Set
    End Property
    Public Property iSACD_CustId() As Integer
        Get
            Return (SACD_CustId)
        End Get
        Set(ByVal Value As Integer)
            SACD_CustId = Value
        End Set
    End Property
    Public Property iSACD_AuditId() As Integer
        Get
            Return (SACD_AuditId)
        End Get
        Set(ByVal Value As Integer)
            SACD_AuditId = Value
        End Set
    End Property
    Public Property iSACD_AuditType() As Integer
        Get
            Return (SACD_AuditType)
        End Get
        Set(ByVal Value As Integer)
            SACD_AuditType = Value
        End Set
    End Property
    Public Property sSACD_Heading() As String
        Get
            Return (SACD_Heading)
        End Get
        Set(ByVal Value As String)
            SACD_Heading = Value
        End Set
    End Property
    Public Property sSACD_CheckpointId() As String
        Get
            Return (SACD_CheckpointId)
        End Get
        Set(ByVal Value As String)
            SACD_CheckpointId = Value
        End Set
    End Property
    Public Property iSACD_EmpId() As Integer
        Get
            Return (SACD_EmpId)
        End Get
        Set(ByVal Value As Integer)
            SACD_EmpId = Value
        End Set
    End Property
    Public Property iSACD_WorkType() As Integer
        Get
            Return (SACD_WorkType)
        End Get
        Set(ByVal Value As Integer)
            SACD_WorkType = Value
        End Set
    End Property
    Public Property sSACD_HrPrDay() As String
        Get
            Return (SACD_HrPrDay)
        End Get
        Set(ByVal Value As String)
            SACD_HrPrDay = Value
        End Set
    End Property
    Public Property dSACD_StartDate() As DateTime
        Get
            Return (SACD_StartDate)
        End Get
        Set(ByVal Value As DateTime)
            SACD_StartDate = Value
        End Set
    End Property
    Public Property dSACD_EndDate() As DateTime
        Get
            Return (SACD_EndDate)
        End Get
        Set(ByVal Value As DateTime)
            SACD_EndDate = Value
        End Set
    End Property
    Public Property sSACD_TotalHr() As String
        Get
            Return (SACD_TotalHr)
        End Get
        Set(ByVal Value As String)
            SACD_TotalHr = Value
        End Set
    End Property
    Public Property iSACD_CRBY() As Integer
        Get
            Return (SACD_CRBY)
        End Get
        Set(ByVal Value As Integer)
            SACD_CRBY = Value
        End Set
    End Property
    Public Property iSACD_UPDATEDBY() As Integer
        Get
            Return (SACD_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            SACD_UPDATEDBY = Value
        End Set
    End Property
    Public Property sSACD_IPAddress() As String
        Get
            Return (SACD_IPAddress)
        End Get
        Set(ByVal Value As String)
            SACD_IPAddress = Value
        End Set
    End Property
    Public Property iSACD_CompId() As Integer
        Get
            Return (SACD_CompID)
        End Get
        Set(ByVal Value As Integer)
            SACD_CompID = Value
        End Set
    End Property

End Structure
Public Structure strStandardAudit_ScheduleCheckPointList
    Private SAC_ID As Integer
    Private SAC_SA_ID As Integer
    Private SAC_CheckPointID As Integer
    Private SAC_Mandatory As Integer
    Private SAC_Status As Integer
    Private SAC_AttachID As Integer
    Private SAC_CrBy As Integer
    Private SAC_IPAddress As String
    Private SAC_CompID As Integer
    Public Property iSAC_ID() As Integer
        Get
            Return (SAC_ID)
        End Get
        Set(ByVal Value As Integer)
            SAC_ID = Value
        End Set
    End Property
    Public Property iSAC_SA_ID() As Integer
        Get
            Return (SAC_SA_ID)
        End Get
        Set(ByVal Value As Integer)
            SAC_SA_ID = Value
        End Set
    End Property
    Public Property iSAC_CheckPointID() As Integer
        Get
            Return (SAC_CheckPointID)
        End Get
        Set(ByVal Value As Integer)
            SAC_CheckPointID = Value
        End Set
    End Property
    Public Property iSAC_Mandatory() As Integer
        Get
            Return (SAC_Mandatory)
        End Get
        Set(ByVal Value As Integer)
            SAC_Mandatory = Value
        End Set
    End Property
    Public Property iSAC_Status() As Integer
        Get
            Return (SAC_Status)
        End Get
        Set(ByVal Value As Integer)
            SAC_Status = Value
        End Set
    End Property
    Public Property iSAC_AttachID() As Integer
        Get
            Return (SAC_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SAC_AttachID = Value
        End Set
    End Property
    Public Property iSAC_CrBy() As Integer
        Get
            Return (SAC_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SAC_CrBy = Value
        End Set
    End Property
    Public Property sSAC_IPAddress() As String
        Get
            Return (SAC_IPAddress)
        End Get
        Set(ByVal Value As String)
            SAC_IPAddress = Value
        End Set
    End Property
    Public Property iSAC_CompID() As Integer
        Get
            Return (SAC_CompID)
        End Get
        Set(ByVal Value As Integer)
            SAC_CompID = Value
        End Set
    End Property
End Structure
Public Class clsStandardAudit
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Dim obclsUL As New clsUploadLedger
    Public Function LoadAuditTypeIsComplainceDetailsInSA(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal iFYId As Integer, ByVal iCustID As Integer, ByVal iAuditTypeId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & " And CMM_Delflag='A' And CMS_KeyComponent=0 And "
            sSql = sSql & " CMM_ID in (SELECT LOE_ServiceTypeId FROM SAD_CUST_LOE Where LOE_CustomerId=" & iCustID & " And LOE_YearId=" & iFYId & ") And EXISTS (SELECT 1 FROM AuditType_Checklist_Master Where ACM_AuditTypeID=CMM_ID) "
            sSql = sSql & " And (CMM_ID Not in (Select SA_AuditTypeID From StandardAudit_Schedule Where SA_YearID=" & iFYId & " And SA_CustID=" & iCustID & " And SA_CompID=" & iAcID & ")"
            If iAuditTypeId > 0 Then
                sSql = sSql & " Or CMM_ID=" & iAuditTypeId & ""
            End If
            sSql = sSql & " ) Order By CMM_Desc ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadScheduledAuditNos(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SA_ID,SA_AuditNo + ' - ' + CMM_Desc As SA_AuditNo From StandardAudit_Schedule "
            sSql = sSql & " Left Join Content_Management_Master on CMM_ID=SA_AuditTypeID Where SA_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And SA_YearID = " & iFinancialYearID & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " And SA_CustID=" & iCustID & " "
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And CONCAT(',', SA_AdditionalSupportEmployeeID, ',') Like ('%," & iLoginUserID & ",%')"
            End If
            sSql = sSql & " Order by SA_ID desc"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateStandardAuditScheduleDetails(ByVal sAC As String, ByVal objSA As strStandardAudit_Schedule, ByVal sYearName As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_AuditNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsGeneralFunctions.GetAllModuleJobCode(sAC, objSA.iSA_CompID, "AUDIT", objSA.iSA_YearID, sYearName, objSA.iSA_CustID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_AuditTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_AuditTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_PartnerID", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSA.sSA_PartnerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_ReviewPartnerID", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSA.sSA_ReviewPartnerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_AdditionalSupportEmployeeID", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSA.sSA_AdditionalSupportEmployeeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_ScopeOfAudit", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objSA.sSA_ScopeOfAudit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_Status", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_StartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSA.dSA_StartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_ExpCompDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSA.dSA_ExpCompDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_RptRvDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSA.dSA_RptRvDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_RptFilDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSA.dSA_RptFilDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_MRSDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSA.dSA_MRSDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("SA_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSA.sSA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iSA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spStandardAudit_Schedule", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateStandardAuditChecklistDetails(ByVal sAC As String, ByVal objSACLD As strStandardAudit_Checklist_Details)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_AuditId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_AuditId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_AuditType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_AuditType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_Heading", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objSACLD.sSACD_Heading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_CheckpointId", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSACLD.sSACD_CheckpointId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_EmpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_EmpId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_WorkType", OleDb.OleDbType.Integer, 5000)
            ObjParam(iParamCount).Value = objSACLD.iSACD_WorkType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_HrPrDay", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objSACLD.sSACD_HrPrDay
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_StartDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSACLD.dSACD_StartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_EndDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSACLD.dSACD_EndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_TotalHr", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objSACLD.sSACD_TotalHr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("SACD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSACLD.sSACD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SACD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSACLD.iSACD_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spStandardAudit_Checklist_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveStandardAuditScheduleCheckPointListDetails(ByVal sAC As String, ByVal objSAC As strStandardAudit_ScheduleCheckPointList)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_SA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_SA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_CheckPointID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_CheckPointID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_Mandatory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_Mandatory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_Status", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("SAC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSAC.sSAC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAC_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAC.iSAC_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spStandardAudit_ScheduleCheckPointList", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTimelineSchedule(ByVal sAC As String, ByVal objSA As strStandardAudit_Schedule, ByVal sYearName As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SA_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_AuditId ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_AuditId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_Heading", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_Heading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_CheckpointId", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSA.sAT_CheckpointId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_EmpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_EmpId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_WorkType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_WorkType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_HrPrDay", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_HrPrDay
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_Comments", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSA.sAT_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_StartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSA.dAT_StartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_EndDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSA.dAT_EndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_TotalHr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_TotalHr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objSA.sAT_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSA.sAT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AT_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSA.iAT_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_Timeline", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTypeCheckList(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iAuditTypeID As Integer, ByVal sHeading As String, ByVal sCheckPointIds As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select DENSE_RANK() OVER (ORDER BY ACM_ID) as SlNo,ACM_Heading,ACM_ID,ACM_Checkpoint From AuditType_Checklist_Master Where ACM_AuditTypeID=" & iAuditTypeID & " And ACM_CompId=" & iAcID & " And ACM_DELFLG='A'"
            If sHeading <> "" Then
                sSql = sSql & " And ACM_Heading='" & sHeading & "'"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " And (ACM_ID Not In (Select SAC_CheckPointID From StandardAudit_ScheduleCheckPointList Where SAC_SA_ID=" & iAuditID & " And SAC_CompID=" & iAcID & ")"
            End If
            If sCheckPointIds <> "" Then
                sSql = sSql & " Or ACM_ID  in (" & sCheckPointIds & ")"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " )"
            End If
            sSql = sSql & " Order by ACM_Heading,ACM_ID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedAuditTypeCheckPointHeadings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select distinct(ACM_Heading) From AuditType_Checklist_Master Where ACM_ID in (Select SAC_CheckPointID From StandardAudit_ScheduleCheckPointList Where SAC_SA_ID=" & iAuditID & " And SAC_CompID=" & iCompID & ") And ACM_CompId=" & iCompID & " and ACM_Heading<>'' and ACM_Heading<>'NULL'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
    Public Function CheckLoginUserIsPartner(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from sad_userdetails where usr_compID=" & iACID & " And USR_Partner=1 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') And Usr_ID=" & iUsrID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedScheduleDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SA_ID,SA_YearID,YMS_ID As FY,SA_ID,SA_AuditNo,SA_CustID,Cust_Name As CustomerName,SA_AuditTypeID,CMM_Desc As AuditType,SA_PartnerID,SA_ReviewPartnerID,SA_AdditionalSupportEmployeeID,ISNULL(SA_ScopeOfAudit,'') As SA_ScopeOfAudit, "
            sSql = sSql & " ISNULL(Convert(Varchar(10),SA_StartDate,103),'') As SA_StartDate,ISNULL(Convert(Varchar(10),SA_ExpCompDate,103),'') As SA_ExpCompDate,ISNULL(Convert(Varchar(10),SA_RptRvDate,103),'') As SA_RptRvDate,"
            sSql = sSql & " ISNULL(Convert(Varchar(10),SA_RptFilDate,103),'') As SA_RptFilDate,ISNULL(Convert(Varchar(10),SA_MRSDate,103),'') As SA_MRSDate,"
            sSql = sSql & " ISNULL(SA_SignedBy,0) As SA_SignedBy,ISNULL(SA_UDIN,'') As SA_UDIN,ISNULL(Convert(Varchar(10),SA_UDINdate,103),'') As SA_UDINdate,SA_Status From StandardAudit_Schedule "
            sSql = sSql & " Join Year_Master on SA_YearID=YMS_YearID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=SA_AuditTypeID"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=SA_CustID"
            sSql = sSql & " Where SA_ID=" & iAuditID & " And SA_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedScheduleCheckPointDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCheckPointID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ACM_Checkpoint From AuditType_Checklist_Master Where ACM_ID=" & iCheckPointID & " And ACM_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedScheduleHeadingDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCheckPointID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ISNULL(ACM_Heading, ' ') as ACM_Heading  From AuditType_Checklist_Master Where ACM_ID=" & iCheckPointID & " And ACM_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedScheduleCheckPointListDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SAC_CheckPointID,SAC_Mandatory From StandardAudit_ScheduleCheckPointList Where SAC_SA_ID=" & iAuditID & " And SAC_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDashboardStandardAudit(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT b.SA_ID As AuditID,DENSE_RANK() OVER (ORDER BY b.SA_ID Desc) As SrNo,b.SA_AuditNo As AuditNo,b.SA_CustID As CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
            sSql = sSql & " CMM_Desc As AuditType, b.SA_Status As StatusID,ISNULL(Convert(Varchar(10),SA_StartDate,103),'') + ' - ' +ISNULL(Convert(Varchar(10),SA_ExpCompDate,103),'') As AuditDate ,"
            sSql = sSql & " Case When b.SA_Status=1 then 'Scheduled' When b.SA_Status=2 then 'Collection of Data' When b.SA_Status=3 then 'TBR' When b.SA_Status=4 then 'Conduct Audit' When b.SA_Status=5 then 'Report' End AuditStatus,"
            sSql = sSql & " Partner=STUFF ((SELECT DISTINCT '; '+ CAST(usr_FullName AS VARCHAR(MAX)) FROM Sad_UserDetails WHERE usr_id in (SELECT value FROM STRING_SPLIT((Select STUFF(LEFT(a.SA_PartnerID, LEN(a.SA_PartnerID) - PATINDEX('%[^,]%', REVERSE(a.SA_PartnerID)) + 1), 1, PATINDEX('%[^,]%', a.SA_PartnerID) - 1, '') from StandardAudit_Schedule a Where SA_ID=b.SA_ID),',')) FOR XMl PATH('')),1,1,'')"
            sSql = sSql & " FROM StandardAudit_Schedule b"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=b.SA_CustID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=b.SA_AuditTypeID"
            sSql = sSql & " Where b.SA_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And b.SA_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And b.SA_CustID=" & iCustomerID & ""
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And CONCAT(',', b.SA_AdditionalSupportEmployeeID, ',') Like ('%," & iLoginUserID & ",%')"
            End If
            sSql = sSql & " Order by b.SA_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            If ex.Message.ToString().Contains("STRING_SPLIT") = True Then
                sSql = "SELECT b.SA_ID As AuditID,DENSE_RANK() OVER (ORDER BY b.SA_ID Desc) As SrNo,b.SA_AuditNo As AuditNo,b.SA_CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
                sSql = sSql & " CMM_Desc As AuditType, b.SA_Status As StatusID,Convert(Varchar(10),b.SA_CrOn,103) As AuditDate,"
                sSql = sSql & " Case When b.SA_Status=1 then 'Scheduled' When b.SA_Status=2 then 'Collection of Data' When b.SA_Status=3 then 'TBR' When b.SA_Status=4 then 'Conduct Audit' When b.SA_Status=5 then 'Report' End AuditStatus,"
                sSql = sSql & " '' As Partner"
                sSql = sSql & " FROM StandardAudit_Schedule b"
                sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=b.SA_CustID"
                sSql = sSql & " Join Content_Management_Master on CMM_ID=b.SA_AuditTypeID"
                sSql = sSql & " Where b.SA_CompID=" & iAcID & ""
                If iFinancialYearID > 0 Then
                    sSql = sSql & " And b.SA_YearID=" & iFinancialYearID & ""
                End If
                If iCustomerID > 0 Then
                    sSql = sSql & " And b.SA_CustID=" & iCustomerID & ""
                End If
                If bLoginUserIsPartner = False Then
                    sSql = sSql & " And CONCAT(',', b.SA_AdditionalSupportEmployeeID, ',') Like ('%," & iLoginUserID & ",%')"
                End If
                sSql = sSql & " Order by b.SA_ID Desc"
                dt = objDBL.SQLExecuteDataTable(sAc, sSql)
                Return dt
            End If
        End Try
    End Function
    Public Function LoadSelectedStandardAuditCheckPointDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iEmpId As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim sCheckpointIds As String = ""
        Try
            If bLoginUserIsPartner = False Then
                sSql = "SELECT ISNULL(STUFF((SELECT ',' + CAST(SACD_CheckpointId AS VARCHAR(10)) FROM StandardAudit_Checklist_Details WHERE SACD_EmpId=" & iEmpId & " AND SACD_AuditId=" & iAuditID & " FOR XML PATH('')), 1, 1, ''),'') AS CheckpointIds"
                sCheckpointIds = objDBL.SQLExecuteScalar(sAc, sSql)
            End If
            sSql = "Select '" & iAuditID & "' As AuditID,DENSE_RANK() OVER (ORDER BY SAC_CheckPointID) As SrNo,SAC_ID As ConductAuditCheckPointPKId,SAC_CheckPointID as CheckPointID,ACM_Heading As Heading,ACM_Checkpoint As 'CheckPoint', SAC_Remarks As Remarks,"
            sSql = sSql & " Case When SAC_Mandatory=1 then 'Yes' When SAC_Mandatory=0 then 'No' End Mandatory,SAC_TestResult As TestResult,SAC_Remarks As Remarks,SAC_ReviewerRemarks As ReviewerRemarks,COALESCE(SAC_AttachID,0) As AttachmentID,"
            sSql = sSql & " Case When SAC_Annexure=1 then 'TRUE' Else 'FALSE' End Annexure,USr_FullName As ConductedBy,Convert(Varchar(10),SAC_LastUpdatedOn,103) as LastUpdatedOn From StandardAudit_ScheduleCheckPointList"
            sSql = sSql & " Join AuditType_Checklist_Master on ACM_ID=SAC_CheckPointID"
            sSql = sSql & " Left Join sad_userdetails on Usr_ID=SAC_ConductedBy "
            'sSql = sSql & " Left Join Sample_selection on SS_AuditCodeID=" & iAuditID & " And SS_CheckPointID=SAC_CheckPointID"
            sSql = sSql & " Where SAC_SA_ID=" & iAuditID & " And SAC_CompID=" & iAcID & " "
            If bLoginUserIsPartner = False And sCheckpointIds <> "" Then
                sSql = sSql & " And SAC_CheckPointID in (" & sCheckpointIds & ")"
            End If
            sSql = sSql & " Order by SAC_CheckPointID"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSelectedStandardAuditCheckPointRemarksHistoryDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer, ByVal iRemarksType As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = " select b.Mas_Description as Role,
                      Usr_FullName as RemarksBy,
                      SAR_Remarks as Remarks,
                      SAR_Date as Date,
	                  SAR_TimlinetoResOn as Timeline,
                      Case when SAR_RemarksType='C' then 'Auditor comments' when SAR_RemarksType='RC' then 'Recieved comments' end as Comments  From StandardAudit_Audit_DRLLog_RemarksHistory Left Join sad_userdetails on Usr_ID=SAR_RemarksBy left join SAD_GrpOrLvl_General_Master b on b.Mas_ID=Usr_Role "
            sSql = sSql & " Where SAR_SA_ID=" & iAuditID & " And SAR_CheckPointIDs='" & iCheckPointID & "' And SAR_CompID=" & iAcID & ""
            'If iRemarksType = 1 Then
            '    sSql = sSql & " And SAR_RemarksType in (1,2)"
            'End If
            'If iRemarksType = 2 Then
            '    sSql = sSql & " And SCR_RemarksType=3"
            'End If
            sSql = sSql & " Order by SAR_ID"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttch(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As String)
        Dim sSql As String
        Try
            sSql = "Update Audit_DRLLog Set ADRL_AttachID= " & iAttachID & " Where ADRL_AuditNo=" & iAuditID & " And ADRL_CompID=" & iACID & " and ADRL_FunID ='" & iCheckPointID & "'"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadSelectedStandardAuditCheckPointRemarksHistoryUserDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer, ByVal iRemarksType As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = " select b.Mas_Description as Role,
                      Usr_FullName as RemarksBy,
                      SAR_Remarks as Remarks,
                      SAR_Date as Date,
	                  SAR_TimlinetoResOn as Timeline,
                      Case when SAR_RemarksType='C' then 'Auditor comments' when SAR_RemarksType='RC' then 'Recieved comments' end as Comments From StandardAudit_Audit_DRLLog_RemarksHistory Left Join sad_userdetails on Usr_ID=SAR_RemarksBy left join SAD_GrpOrLvl_General_Master b on b.Mas_ID=Usr_Role "
            sSql = sSql & " Where SAR_SA_ID=" & iAuditID & " And SAR_CheckPointIDs='" & iCheckPointID & "' And SAR_CompID=" & iAcID & ""
            'If iRemarksType = 1 Then
            '    sSql = sSql & " And SAR_RemarksType in (1,2)"
            'End If
            'If iRemarksType = 2 Then
            '    sSql = sSql & " And SCR_RemarksType=3"
            'End If
            sSql = sSql & " Order by SAR_ID"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStandardAuditConductAuditReport(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select  DENSE_RANK() OVER (ORDER BY SAC_CheckPointID) As SrNo,ACM_Heading As Heading,ACM_Heading As Header,ACM_Checkpoint As 'CheckPoint', SAC_Remarks As Comments,"
            sSql = sSql & " Case When SAC_Annexure=1 then 'Yes' When SAC_Mandatory=0 then 'No' End Annexures From StandardAudit_ScheduleCheckPointList"
            sSql = sSql & " Join AuditType_Checklist_Master on ACM_ID=SAC_CheckPointID"
            sSql = sSql & " Where SAC_SA_ID=" & iAuditID & " And SAC_CompID=" & iAcID & " Order by SAC_CheckPointID"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStandardAuditConductAuditObservationsReport(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim iSrNo As Integer = 0
        Dim iCheckPointID As Integer
        Dim iObservation As Integer = 0
        Try
            sSql = "Select SSO_SAC_CheckPointID,ACM_Checkpoint,SSO_Observations From StandardAudit_ScheduleObservations Left Join AuditType_Checklist_Master on ACM_ID=SSO_SAC_CheckPointID"
            sSql = sSql & " Where SSO_SA_ID=" & iAuditID & " And SSO_CompID=" & iAcID & " Order by SSO_SAC_CheckPointID,SSO_Observations desc"
            dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)

            dt.Columns.Add("SrNo")
            dt.Columns.Add("CheckPoint")
            dt.Columns.Add("Observations")
            For i = 0 To dtTab.Rows.Count - 1
                dr = dt.NewRow()
                If iCheckPointID <> dtTab.Rows(i)("SSO_SAC_CheckPointID") Then
                    iObservation = 0
                    iSrNo = iSrNo + 1
                    dr("SrNo") = iSrNo
                    dr("CheckPoint") = dtTab.Rows(i)("ACM_Checkpoint")
                    iCheckPointID = dtTab.Rows(i)("SSO_SAC_CheckPointID")
                End If
                iObservation = iObservation + 1
                dr("Observations") = "Observation " + iObservation.ToString() + " : " + dtTab.Rows(i)("SSO_Observations")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateScheduleCheckPointRemarksAnnexure(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iConductAuditCheckPointId As Integer, ByVal iCheckPointID As Integer, ByVal iRemarksType As Integer, ByVal sRemarks As String, ByVal iAnnexure As Integer,
                                                       ByVal iTestResult As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iIsIssueRaised As Integer, ByVal sEmailIds As String)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_ScheduleCheckPointList Set SAC_TestResult=" & iTestResult & ",SAC_ConductedBy=" & iUserID & ",SAC_LastUpdatedOn=GetDate(),"
            If iRemarksType = 1 Or iRemarksType = 2 Then
                sSql = sSql & "SAC_Remarks='" & sRemarks & "',"
            End If
            If iRemarksType = 3 Then
                sSql = sSql & "SAC_ReviewerRemarks='" & sRemarks & "',"
            End If
            sSql = sSql & "SAC_Annexure= " & iAnnexure & " Where SAC_SA_ID=" & iAuditID & " And SAC_ID=" & iConductAuditCheckPointId & " And SAC_CheckPointID=" & iCheckPointID & " And SAC_CompID=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)

            Dim iMaxId As Integer = objDBL.SQLExecuteScalar(sAc, "select IsNull(Max(SCR_ID) + 1,1) from StandardAudit_ConductAudit_RemarksHistory")
            sSql = "" : sSql = "Insert into StandardAudit_ConductAudit_RemarksHistory (SCR_ID,SCR_SA_ID,SCR_SAC_ID,SCR_CheckPointID,SCR_RemarksType,SCR_Remarks,SCR_RemarksBy,SCR_Date,SCR_IPAddress,SCR_CompID,SCR_IsIssueRaised,SCR_EmailIds) Values"
            sSql = sSql & "(" & iMaxId & "," & iAuditID & "," & iConductAuditCheckPointId & "," & iCheckPointID & "," & iRemarksType & ",'" & sRemarks & "'," & iUserID & ",GetDate(),'" & sIPAddress & "'," & iAcID & "," & iIsIssueRaised & ",'" & sEmailIds & "')"
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadScheduledAuditAllCheckPoints(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iEmpId As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim sCheckpointIds As String = ""
        Try
            If bLoginUserIsPartner = False Then
                sSql = "SELECT ISNULL(STUFF((SELECT ',' + CAST(SACD_CheckpointId AS VARCHAR(10)) FROM StandardAudit_Checklist_Details WHERE SACD_EmpId=" & iEmpId & " AND SACD_AuditId=" & iAuditID & " FOR XML PATH('')), 1, 1, ''),'') AS CheckpointIds"
                sCheckpointIds = objDBL.SQLExecuteScalar(sAc, sSql)
            End If
            sSql = "Select ACM_ID,ACM_Checkpoint From AuditType_Checklist_Master Where  "
            If bLoginUserIsPartner = False And sCheckpointIds <> "" Then
                sSql = sSql & " ACM_ID in (" & sCheckpointIds & ") And"
            ElseIf bLoginUserIsPartner = True Then
                sSql = sSql & " ACM_ID in (Select SAC_CheckPointID From StandardAudit_ScheduleCheckPointList Where SAC_SA_ID=" & iAuditID & " And SAC_CompID=" & iAcID & ") And"
            End If
            sSql = sSql & " ACM_CompID=" & iAcID & " Order by ACM_ID"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeletecheduleCheckPointObservations(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From StandardAudit_ScheduleObservations Where SSO_SA_ID=" & iAuditID & " And SSO_SAC_CheckPointID=" & iCheckPointID & " And SSO_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveScheduleCheckPointObservations(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer, ByVal sObservations As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            Dim iMaxid As Integer = objDBL.SQLExecuteScalar(sAC, "select IsNull(Max(SSO_PKID) + 1,1) from StandardAudit_ScheduleObservations")
            sSql = "Insert into StandardAudit_ScheduleObservations(SSO_PKID,SSO_SA_ID,SSO_SAC_CheckPointID,SSO_Observations,SSO_CrBy,SSO_CrOn,SSO_CompID,SSO_IPAddress) "
            sSql = sSql & "Values(" & iMaxid & "," & iAuditID & "," & iCheckPointID & ",'" & sObservations & "'," & iUserID & ",Getdate()," & iACID & ",'" & sIPAddress & "')"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadPanAndSheduleAudit(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Cust_ID As CustomerID,Cust_Name As CustomerName,ISNULL(CMM_ID,0) As CheckPointID,CMM_Desc As AuditType,ISNULL(SA_ID,0) As AuditID,SA_AuditNo As AuditNo,"
            sSql = sSql & " Case When SA_Status=1 then 'Scheduled' When SA_Status=2 then 'Collection of Data' When SA_Status=3 then 'TBR' When SA_Status=4 then 'Conduct Audit' When SA_Status=5 then 'Report' End AuditStatus,"
            sSql = sSql & " Convert(Varchar(10),SA_CrOn,103) As AuditDate From SAD_CUST_LOE"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=LOE_CustomerId"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=LOE_ServiceTypeId"
            sSql = sSql & " Left Join StandardAudit_Schedule On CMM_ID=SA_AuditTypeID And Cust_Id=SA_CustID And SA_YearID=" & iFinancialYearID & ""
            sSql = sSql & " where LOE_YearId=" & iFinancialYearID & " Order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveTrialBalanceReviewAttachmentInAudit(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAuditID As Integer, ByVal iLedgerID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_Excel_Upload Set AEU_AttachmentId= " & iAttachID & " Where AEU_AuditId=" & iAuditID & " And AEU_ID=" & iLedgerID & " And AEU_CompId=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateStandardAuditStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iStatusId As Integer)
        Try
            Dim iCurrentStatusId As Integer = objDBL.SQLExecuteScalar(sAc, "Select SA_Status From StandardAudit_Schedule Where SA_ID=" & iAuditID & " And SA_CompID=" & iAcID & "")
            If iCurrentStatusId < iStatusId Then
                Dim sSql As String = "Update StandardAudit_Schedule Set SA_Status= " & iStatusId & " Where SA_ID=" & iAuditID & " And SA_CompID=" & iAcID & ""
                objDBL.SQLExecuteNonQuery(sAc, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateSignedByUDINInAudit(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iSignedby As Integer, ByVal sUDIN As String, ByVal dUDINdate As Date)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_Schedule Set SA_SignedBy=" & iSignedby & ",SA_UDIN='" & sUDIN & "',SA_UDINdate= " & objclsGRACeGeneral.FormatDtForRDBMS(dUDINdate, "Q") & " Where SA_ID=" & iAuditID & " And SA_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetAuditUDINdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select ISNULL(usr_FullName,'') As usr_FullName,ISNULL(SA_UDIN,'') As SA_UDIN,ISNULL(Convert(Varchar(10),SA_UDINdate,103),'') As SA_UDINdate From StandardAudit_Schedule Left Join Sad_UserDetails on usr_Id=SA_SignedBy Where SA_ID=" & iAuditId & " And SA_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSAAuditSummaryDashboard(ByVal sAc As String, ByVal iAcID As Integer, ByVal iFinancialYearID As Integer, ByVal iCustomerID As Integer, ByVal iAuditID As Integer, ByVal iLoginUserID As Integer, ByVal bLoginUserIsPartner As Boolean) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT b.SA_ID As AuditID,DENSE_RANK() OVER (ORDER BY b.SA_ID Desc) As SrNo,b.SA_AuditNo As AuditNo,b.SA_CustID As CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
            sSql = sSql & " CMM_Desc As AuditType, b.SA_Status As StatusID,Convert(Varchar(10),b.SA_CrOn,103) As AuditDate,"
            sSql = sSql & " Case When b.SA_Status=1 then 'Scheduled' When b.SA_Status=2 then 'Collection of Data' When b.SA_Status=3 then 'TBR' When b.SA_Status=4 then 'Conduct Audit' When b.SA_Status=5 then 'Report' End AuditStatus,"
            sSql = sSql & " Partner=STUFF ((SELECT DISTINCT '; '+ CAST(usr_FullName AS VARCHAR(MAX)) FROM Sad_UserDetails WHERE usr_id in (SELECT value FROM STRING_SPLIT((Select STUFF(LEFT(a.SA_PartnerID, LEN(a.SA_PartnerID) - PATINDEX('%[^,]%', REVERSE(a.SA_PartnerID)) + 1), 1, PATINDEX('%[^,]%', a.SA_PartnerID) - 1, '') from StandardAudit_Schedule a Where SA_ID=b.SA_ID),',')) FOR XMl PATH('')),1,1,''),"
            sSql = sSql & " Team=STUFF ((SELECT DISTINCT '; '+ CAST(usr_FullName AS VARCHAR(MAX)) FROM Sad_UserDetails WHERE usr_id in (SELECT value FROM STRING_SPLIT((Select STUFF(LEFT(a.SA_AdditionalSupportEmployeeID, LEN(a.SA_AdditionalSupportEmployeeID) - PATINDEX('%[^,]%', REVERSE(a.SA_AdditionalSupportEmployeeID)) + 1), 1, PATINDEX('%[^,]%', a.SA_AdditionalSupportEmployeeID) - 1, '') from StandardAudit_Schedule a Where SA_ID=b.SA_ID),',')) FOR XMl PATH('')),1,1,'')"
            sSql = sSql & " FROM StandardAudit_Schedule b"
            sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=b.SA_CustID"
            sSql = sSql & " Join Content_Management_Master on CMM_ID=b.SA_AuditTypeID"
            sSql = sSql & " Where b.SA_CompID=" & iAcID & ""
            If iFinancialYearID > 0 Then
                sSql = sSql & " And b.SA_YearID=" & iFinancialYearID & ""
            End If
            If iCustomerID > 0 Then
                sSql = sSql & " And b.SA_CustID=" & iCustomerID & ""
            End If
            If iAuditID > 0 Then
                sSql = sSql & " And b.SA_ID=" & iAuditID & ""
            End If
            If bLoginUserIsPartner = False Then
                sSql = sSql & " And CONCAT(',', b.SA_AdditionalSupportEmployeeID, ',') Like ('%," & iLoginUserID & ",%')"
            End If
            sSql = sSql & " Order by b.SA_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            If ex.Message.ToString().Contains("STRING_SPLIT") = True Then
                sSql = "SELECT b.SA_ID As AuditID,DENSE_RANK() OVER (ORDER BY b.SA_ID Desc) As SrNo,b.SA_AuditNo As AuditNo,b.SA_CustID,Cust_Name As CustomerName,CONCAT(SUBSTRING(Cust_Name, 0, 25),'....') As CustomerShortName, "
                sSql = sSql & " CMM_Desc As AuditType, b.SA_Status As StatusID,Convert(Varchar(10),b.SA_CrOn,103) As AuditDate,"
                sSql = sSql & " Case When b.SA_Status=1 then 'Scheduled' When b.SA_Status=2 then 'Collection of Data' When b.SA_Status=3 then 'TBR' When b.SA_Status=4 then 'Conduct Audit' When b.SA_Status=5 then 'Report' End AuditStatus,"
                sSql = sSql & " '' As Partner,"
                sSql = sSql & " '' As Team"
                sSql = sSql & " FROM StandardAudit_Schedule b"
                sSql = sSql & " Join SAD_CUSTOMER_MASTER on Cust_Id=b.SA_CustID"
                sSql = sSql & " Join Content_Management_Master on CMM_ID=b.SA_AuditTypeID"
                sSql = sSql & " Where b.SA_CompID=" & iAcID & ""
                If iFinancialYearID > 0 Then
                    sSql = sSql & " And b.SA_YearID=" & iFinancialYearID & ""
                End If
                If iCustomerID > 0 Then
                    sSql = sSql & " And b.SA_CustID=" & iCustomerID & ""
                End If
                If iAuditID > 0 Then
                    sSql = sSql & " And b.SA_ID=" & iAuditID & ""
                End If
                If bLoginUserIsPartner = False Then
                    sSql = sSql & " And CONCAT(',', b.SA_AdditionalSupportEmployeeID, ',') Like ('%," & iLoginUserID & ",%')"
                End If
                sSql = sSql & " Order by b.SA_ID Desc"
                dt = objDBL.SQLExecuteDataTable(sAc, sSql)
                Return dt
            End If
        End Try
    End Function
    Public Function LoadSADRLSummary(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT COUNT(ADRL_ID) As DocumentsRequested,COUNT(CASE WHEN ADRL_AuditNo = " & iAuditID & " AND ADRL_ReceivedComments IS NOT NULL THEN 1 END) As Received,"
            sSql = sSql & " ((SELECT COUNT(SAC_ID) FROM StandardAudit_ScheduleCheckPointList WHERE SAC_SA_ID = " & iAuditID & " And SAC_CompID=" & iAcID & ") + COUNT(CASE WHEN ADRL_AuditNo = " & iAuditID & " AND ADRL_FunID=0 THEN 1 END)) As TotalCheckpoints"
            sSql = sSql & " FROM Audit_DRLLog Where ADRL_AuditNo=" & iAuditID & ""
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSAObservationsQuerySummary(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer, ByVal iCustomerId As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dtCount As New DataTable
        Dim dt As New DataTable
        Try
            dtCount = obclsUL.getCustTBSelectedYear(sAc, iAcID, iCustomerId, iYearID, iAuditID)
            sSql = "SELECT " & dtCount.Rows.Count & " AS TotalLedger,COUNT(DISTINCT CASE WHEN SRO_AuditId = " & iAuditID & " AND SRO_CompID = 1 THEN SRO_AEU_ID END) AS TotalObservations,"
            sSql = sSql & " COUNT(DISTINCT CASE WHEN SRO_IsIssueRaised = 1 AND SRO_AuditId = " & iAuditID & " AND SRO_CompID = 1 THEN SRO_AEU_ID END) AS TotalIssueRaised,COUNT(DISTINCT CASE WHEN SRO_IsIssueRaised > 1 AND SRO_AuditId = " & iAuditID & " AND SRO_CompID = 1 THEN SRO_AEU_ID END) AS ClientResponse"
            sSql = sSql & " FROM Audit_Excel_Upload LEFT JOIN StandardAudit_ReviewLedger_Observations ON Audit_Excel_Upload.AEU_ID = StandardAudit_ReviewLedger_Observations.SRO_AEU_ID Where AEU_YearID=" & iYearID & ""
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSACheckpointSummary(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT COUNT(SAC_ID) AS TotalCheckpoints, COUNT(CASE WHEN SAC_Mandatory = 1 THEN 1 END) AS Mandatory, COUNT(CASE WHEN SAC_SA_ID = " & iAuditID & " AND SAC_TestResult IS NOT NULL THEN 1 END) AS Tested,"
            sSql = sSql & " (SELECT COUNT(Distinct(SCR_CheckPointID)) FROM StandardAudit_ConductAudit_RemarksHistory WHERE SCR_SA_ID = " & iAuditID & " And SCR_IsIssueRaised in (0,1)) As TotalObservations"
            sSql = sSql & " FROM StandardAudit_ScheduleCheckPointList WHERE SAC_SA_ID = " & iAuditID & " And SAC_CompID=" & iAcID & ""
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckIsAuditReportCompleted(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iFinancialYear As Integer, ByVal iCustomerId As Integer) As Integer
        Dim sSql As String
        Dim iCount As Integer
        Try
            sSql = "Select COUNT(RG_Id) From SAD_ReportGeneration Where RG_AuditId=" & iAuditID & " And RG_FinancialYear=" & iFinancialYear & " And RG_CustomerId=" & iCustomerId & ""
            iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSamplingAuditAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select SS_AttachID from Sample_selection where SS_AuditCodeID=" & iAuditID & " and SS_CheckPointID=" & iCheckPointID & " and SS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetConductAuditAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select SAC_AttachID from StandardAudit_ScheduleCheckPointList where SAC_SA_ID=" & iAuditID & " and SAC_CheckPointID=" & iCheckPointID & " and SAC_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDRLAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select ADRL_AttachID from Audit_DRLLog where ADRL_AuditNo=" & iAuditID & " and ADRL_FunID=" & iCheckPointID & " and ADRL_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveDRLConductAuditAttachmentInAudit(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointID As Integer, ByVal sSource As String)
        Dim sSql As String
        Try
            If sSource = "CA" Then
                sSql = "Update StandardAudit_ScheduleCheckPointList Set SAC_AttachID= " & iAttachID & " Where SAC_SA_ID=" & iAuditID & " And SAC_CheckPointID=" & iCheckPointID & " And SAC_CompID=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
                If GetDRLAttachmentID(sAC, iACID, iAuditID, iCheckPointID) = 0 Then
                    sSql = "Update Audit_DRLLog Set ADRL_AttachID= " & iAttachID & " Where ADRL_AuditNo=" & iAuditID & " And ADRL_CompID=" & iACID & " and ADRL_FunID ='" & iCheckPointID & "'"
                    objDBL.SQLExecuteNonQuery(sAC, sSql)
                End If
            End If
            If sSource = "DRL" Then
                sSql = "Update Audit_DRLLog Set ADRL_AttachID= " & iAttachID & " Where ADRL_AuditNo=" & iAuditID & " And ADRL_CompID=" & iACID & " and ADRL_FunID ='" & iCheckPointID & "'"
                objDBL.SQLExecuteNonQuery(sAC, sSql)
                If GetConductAuditAttachmentID(sAC, iACID, iAuditID, iCheckPointID) = 0 Then
                    sSql = "Update StandardAudit_ScheduleCheckPointList Set SAC_AttachID= " & iAttachID & " Where SAC_SA_ID=" & iAuditID & " And SAC_CheckPointID=" & iCheckPointID & " And SAC_CompID=" & iACID & ""
                    objDBL.SQLExecuteNonQuery(sAC, sSql)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetSelectedCheckPoints(ByVal sAC As String, ByVal iACID As Integer, ByVal sCheckpointIDs As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ACM_Checkpoint As 'Check Point' from AuditType_Checklist_Master where ACM_ID in (" & sCheckpointIDs & ")"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteSelectedCheckPointsAndTeamMembers(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCustId As Integer, ByVal iPKId As String, ByVal sCheckpointIDs As String)
        Dim sSql As String
        Try
            sSql = "Delete From StandardAudit_Checklist_Details where SACD_AuditId=" & iAuditID & " And SACD_CustID=" & iCustId & " And SACD_ID=" & iPKId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)

            DeleteFinalCheckPointsDetails(sAC, iACID, iAuditID, sCheckpointIDs)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteFinalCheckPointsDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal sCheckpointIDs As String)
        Dim sSql As String
        Try
            sSql = "Delete From StandardAudit_ScheduleCheckPointList where SAC_SA_ID=" & iAuditID & " And SAC_CheckPointID in (" & sCheckpointIDs & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadSAAsignedCheckPointsAndTeamMembers(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAuditId As Integer, ByVal iCustId As Integer, ByVal sHeading As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT SACD_ID,SACD_CheckpointId,SACD_Heading,ISNULL(usr_FullName,'') As Employee,SUM(len(SACD_CheckpointId) - len(replace(SACD_CheckpointId, ',', '')) +1) as NoCheckpoints,CASE WHEN SACD_EmpId > 0 THEN 1 ELSE 0 END as NoEmployee,SACD_TotalHr as Working_Hours,"
            sSql = sSql & " CASE WHEN Convert(Varchar(10),SACD_EndDate,103) = '01/01/1900' THEN '' ELSE Convert(Varchar(10),SACD_EndDate,103) END As Timeline FROM StandardAudit_Checklist_Details left join sad_userdetails a on SACD_EmpId=usr_Id where SACD_AuditId=" & iAuditId & " and SACD_CustID=" & iCustId & ""
            If sHeading <> "" Then
                sSql = sSql & " and SACD_Heading='" & sHeading & "'"
            End If
            sSql = sSql & " Group by SACD_ID,SACD_Heading,SACD_EndDate,SACD_TotalHr,SACD_CheckpointId,usr_FullName,SACD_EmpId"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFinalAuditTypeHeadingCheckPoints(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT ACM_Heading, ACM_ID,ACM_Checkpoint,CASE WHEN SAC_Mandatory=1 THEN 'Yes' ELSE 'No' END AS SAC_Mandatory FROM AuditType_Checklist_Master "
            sSql = sSql & " JOIN StandardAudit_ScheduleCheckPointList ON ACM_ID=SAC_CheckPointID WHERE SAC_SA_ID=" & iAuditID & " AND SAC_CompID=" & iCompID & " And ACM_CompId=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSASelectedEmployees(ByVal sAC As String, ByVal iACID As Integer, ByVal sUserId As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails where Usr_CompID=" & iACID & " And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L')"
            If sUserId <> "" Then
                sSql = sSql & " and Usr_ID in (" & sUserId & ")"
            End If
            sSql = sSql & " order by FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSelectedSAAsignedCheckPointsAndTeamMembers(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAuditId As Integer, ByVal iCustId As Integer, ByVal iPKId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT SACD_ID,SACD_CheckpointId,SACD_Heading,SACD_EmpId,SACD_WorkType,SACD_TotalHr,SACD_HrPrDay,"
            sSql = sSql & " CASE WHEN Convert(Varchar(10),SACD_StartDate,103) = '01/01/1900' THEN '' ELSE Convert(Varchar(10),SACD_StartDate,103) END As SACD_StartDate,"
            sSql = sSql & " CASE WHEN Convert(Varchar(10),SACD_EndDate,103) = '01/01/1900' THEN '' ELSE Convert(Varchar(10),SACD_EndDate,103) END As SACD_EndDate"
            sSql = sSql & " From StandardAudit_Checklist_Details Where SACD_ID=" & iPKId & " And SACD_AuditId=" & iAuditId & " and SACD_CustID=" & iCustId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSelectedScheduleCheckPointListDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditId As Integer, ByVal iCustId As Integer, ByVal sCheckPointsPKID As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SAC_CheckPointID,SAC_Mandatory From StandardAudit_ScheduleCheckPointList Where SAC_SA_ID=" & iAuditId & " And SAC_CompID=" & iAcID & " And SAC_CheckPointID in (" & sCheckPointsPKID & ")"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetResourceAvailability(ByVal sAc As String, ByVal iAcID As Integer) As DataTable
        Dim sSql As String
        Dim dt As DataTable
        Try
            sSql = "WITH table1 AS (SELECT b.usr_FullName,AAST_ExpectedCompletionDate,a.AAS_AssignmentNo,ROW_NUMBER() OVER (PARTITION BY AAST_EmployeeID ORDER BY AAST_ExpectedCompletionDate DESC) AS RowNo FROM AuditAssignment_SubTask"
            sSql = sSql & " left join AuditAssignment_Schedule a on a.AAS_ID=AAST_AAS_ID"
            sSql = sSql & " left join sad_userdetails b on b.usr_id = AAST_EmployeeID)"
            sSql = sSql & " SELECT  * FROM table1 WHERE RowNo = 1"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getcustomername(ByVal sAC As String, ByVal iACID As Integer, ByVal Icustid As Integer) As String
        Dim sSql As String
        Try
            sSql = "select CUST_NAME from SAD_CUSTOMER_MASTER where CUST_ID=" & Icustid & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getTimelinedetails(ByVal sAC As String, ByVal iACID As Integer, ByVal Icustid As Integer, ByVal iAudit As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "SELECT  SACD_Heading as Heading,SUM(len(SACD_CheckpointId) - len(replace(SACD_CheckpointId, ',', '')) +1) AS NoCheckpoints,"
            sSql = sSql & " SUM(len(SACD_EmpId) - len(replace(SACD_EmpId, ',', '')) +1)  AS NoEmployee,Convert(Varchar(10),SACD_EndDate,103) as Timeline,"
            sSql = sSql & " SACD_TotalHr as Working_Hours,SACD_CustId as CustID, SACD_ID as Headingid,SACD_AuditId as AuditID,"
            sSql = sSql & " SACD_CheckpointId as Checkpointids,SUM(a.AT_TotalHr+0) as TotalHoursBooked FROM StandardAudit_Checklist_Details"
            sSql = sSql & " left join Audit_Timeline a on a.AT_Heading = SACD_ID"
            sSql = sSql & " where SACD_CustId=" & Icustid & " and SACD_AuditId=" & iAudit & ""
            sSql = sSql & " group by SACD_Heading,SACD_EndDate,SACD_TotalHr,SACD_CustId,SACD_ID,SACD_AuditId,SACD_CheckpointId"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getTimelineotherdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal Icustid As Integer, ByVal iAudit As Integer, ByVal iHeadingid As Integer, ByVal sCheckpointids As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select AT_ID as pkid ,at_startdate as Startdate, AT_EndDate as EndDate,AT_TotalHr totalHrs,at_comments as Comments,at_custid as CustID,at_auditid as AuditID from Audit_Timeline"
            sSql = sSql & " where"
            If Icustid > 0 Then
                sSql = sSql & " at_custid = " & Icustid & " And"
            End If
            If iAudit > 0 Then
                sSql = sSql & " at_auditid=" & iAudit & " And"
            End If
            sSql = sSql & " at_heading = " & iHeadingid & " And AT_CheckpointId = '" & sCheckpointids & "'"
            sSql = sSql & " order by at_id desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getTimelineCommentsrdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPkid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select AT_ID as pkid , at_startdate as Startdate, AT_EndDate as EndDate,AT_TotalHr totalHrs,at_comments as Comments from Audit_Timeline"
            sSql = sSql & " where AT_ID = " & iPkid & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
