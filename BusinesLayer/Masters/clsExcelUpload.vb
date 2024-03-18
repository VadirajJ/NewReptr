Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsExcelUpload
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadExcelMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal sMasterStatus As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select GEU_Pk_ID,GEU_MasterName from GRACe_ExcelUpload where GEU_CompID=" & iACID & ""
            If sMasterStatus = "A" Then
                sSql = sSql & " And GEU_Status='A'"
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetYearID(ByVal sAC As String, ByVal iACID As Integer, ByVal sYear As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select YMS_YEARID from YEAR_MASTER where YMS_ID='" & sYear & "' and YMS_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckFinancialYear(ByVal sAC As String, ByVal iACID As Integer, ByVal sYear As String) As Boolean
        Dim sSql As String
        Dim ChkRec As Boolean
        Try
            sSql = "" : sSql = "Select * from YEAR_MASTER where YMS_ID='" & sYear & "'and YMS_CompID=" & iACID & ""
            ChkRec = objDBL.SQLCheckForRecord(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRole(ByVal sAC As String, ByVal iACID As Integer, ByVal sRole As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer
        Try
            sSql = "Select * from SAD_GrpOrLvl_General_Master where Upper(Mas_Description)=Upper('" & sRole & "') and Mas_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDesignation(ByVal sAC As String, ByVal iACID As Integer, ByVal sDessignation As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select Mas_ID from SAD_GRPDESGN_General_Master where Upper(Mas_Description)=Upper('" & sDessignation & "') And Mas_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckZone(ByVal sAC As String, ByVal iACID As Integer, ByVal sZone As String) As Integer
        Dim sSql As String : Dim ChkRec As Integer = 0
        Try
            sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sZone & "') and Org_LevelCode=1 and Org_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRegion(ByVal sAC As String, ByVal iACID As Integer, ByVal iZone As String, ByVal sRegion As String) As Integer
        Dim sSql As String : Dim iChkRec As Integer = 0 : Dim iZoneID As Integer = 0
        Try
            sSql = "Select org_node from sad_org_Structure where Org_Name='" & iZone & "' and Org_LevelCode=1 and Org_CompID=" & iACID & ""
            iZoneID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sRegion & "') and Org_Parent='" & iZoneID & "' and Org_LevelCode=2 and Org_CompID=" & iACID & " order by Org_Name"
            iChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckArea(ByVal sAC As String, ByVal iACID As Integer, ByVal sZone As String, ByVal sRegion As String, ByVal sArea As String) As Integer
        Dim sSql As String : Dim iChkRec As Integer = 0 : Dim iZoneID As Integer = 0 : Dim iRegionID As Integer = 0
        Try
            sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sZone & "') and Org_LevelCode=1 and Org_CompID=" & iACID & ""
            iZoneID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            sSql = "" : sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sRegion & "') and Org_Parent='" & iZoneID & "' and "
            sSql = sSql & " Org_LevelCode=2 and Org_CompID=" & iACID & " order by Org_Name"
            iRegionID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "" : sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sArea & "') and Org_Parent='" & iRegionID & "' and "
            sSql = sSql & " Org_LevelCode=3 and Org_CompID=" & iACID & " order by Org_Name"
            iChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBranch(ByVal sAC As String, ByVal iACID As Integer, ByVal sZone As String, ByVal sRegion As String, ByVal sArea As String, ByVal sBranch As String) As Integer
        Dim sSql As String : Dim iChkRec As Integer = 0 : Dim iZoneID As Integer = 0 : Dim iRegionID As Integer = 0 : Dim iAreaID As Integer = 0
        Try
            sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sZone & "') and Org_LevelCode=1 and Org_CompID=" & iACID & ""
            iZoneID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            sSql = "" : sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sRegion & "') and Org_Parent='" & iZoneID & "' and "
            sSql = sSql & " Org_LevelCode=2 and Org_CompID=" & iACID & " order by Org_Name"
            iRegionID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            sSql = "" : sSql = "Select org_node,Org_Name from sad_org_Structure where Upper(Org_Name)=Upper('" & sArea & "') and Org_Parent='" & iRegionID & "' and "
            sSql = sSql & " Org_LevelCode=3 and Org_CompID=" & iACID & " order by Org_Name"
            iAreaID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "" : sSql = "Select org_node from sad_org_Structure where Upper(Org_Name)=Upper('" & sBranch & "') and Org_Parent='" & iAreaID & "' and "
            sSql = sSql & " Org_LevelCode=4 and Org_CompID=" & iACID & " order by Org_Name"
            iChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckVendor(ByVal sAC As String, ByVal iACID As Integer, ByVal sVendore As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select CUST_ID from SAD_CUSTOMER_MASTER where Upper(CUST_NAME)=Upper('" & sVendore & "') and CUST_CompID=" & iACID & " order by CUST_NAME"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sRiskName As String, ByVal sRiskType As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MRL_PKID from MST_RISK_Library Left Join Risk_GeneralMaster On RAM_PKID=MRL_RiskTypeID Upper(RAM_Name)=Upper('" & sRiskType & "')"
            sSql = sSql & " And RAM_CompID=" & iACID & " And RAM_YearID=" & iYearID & " And RAM_Category='RT'"
            sSql = sSql & " Where MRL_RiskName='" & sRiskName & "' And MRL_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskGeneralMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sType As String, ByVal sRiskType As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID from Risk_GeneralMaster where Upper(RAM_Name)=Upper('" & sRiskType & "') and RAM_CompID=" & iACID & " And RAM_YearID=" & iYearID & " And RAM_Category='" & sType & "' order by RAM_Name Asc"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckInherentMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal sInherentRisk As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MIM_ID from MST_InherentRisk_Master where Upper(MIM_Name)=Upper('" & sInherentRisk & "') and MIM_CompID=" & iACID & " order by MIM_Name Asc"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlID(ByVal sAC As String, ByVal iACID As Integer, ByVal sControlName As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = " select MCL_PKID from MST_CONTROL_Library where Upper(MCL_COntrolName)=Upper('" & sControlName & "') and MCL_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionID(ByVal sAC As String, ByVal iACID As Integer, ByVal sFunctionName As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select ENT_ID from MST_ENTITY_MASTER where Upper(ENT_ENTITYNAME)=Upper('" & sFunctionName & "') And ENT_compID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunctionID(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubFunctionName As String, ByVal iFunID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select SEM_ID from MST_SUBENTITY_MASTER where Upper(SEM_NAME)=Upper('" & sSubFunctionName & "') And SEM_ENT_ID=" & iFunID & " and SEM_COMPID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetProcessID(ByVal sAC As String, ByVal iACID As Integer, ByVal sProcess As String, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select PM_ID from MST_PROCESS_MASTER where Upper(PM_NAME)=Upper('" & sProcess & "') and PM_SEM_ID=" & iSubFunID & " And PM_COMPID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubProcessNameExist(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, ByVal sSubProcess As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select SPM_ID from MST_SUBPROCESS_MASTER where Upper(SPM_NAME)=Upper('" & sSubProcess & "') And SPM_PM_ID=" & iProcessID & " and SPM_COMPID=" & iACID & ""
            If iSubProcessID > 0 Then
                sSql = sSql & " And SPM_ID<>" & iSubProcessID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskNameID(ByVal sAC As String, ByVal iACID As Integer, ByVal sRiskName As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MRL_PKID from MST_RISK_Library where Upper(MRL_RiskName)=Upper('" & sRiskName & "') And MRL_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChecksID(ByVal sAc As String, ByVal iAcID As Integer, ByVal iControlID As Integer, ByVal sCheckName As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select CHK_ID from MST_Checks_Master where Upper(CHK_CheckName)=Upper('" & sCheckName & "') And Chk_ControlID=" & iControlID & " And CHK_CompID=" & iAcID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTaskIsCompliance(ByVal sAC As String, ByVal iACID As Integer, ByVal iTaskId As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CMS_KeyComponent from content_management_master Where cmm_ID=" & iTaskId & " and cmm_compID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal sMasterName As String, ByVal sCategory As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select cmm_ID from content_management_master Where Upper(cmm_desc)=Upper('" & sMasterName & "') and cmm_compID=" & iACID & " and cmm_category='" & sCategory & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunOwnerManagerSPOCId(ByVal sAC As String, ByVal iACID As Integer, ByVal sFunOwnerManagerSPOC As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select Usr_ID from sad_userdetails Where Upper(Usr_FullName)=Upper('" & sFunOwnerManagerSPOC & "') and Usr_CompID=" & iACID & " And Usr_Node>0 And Usr_OrgnID>0 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L')"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAuditCheckListYearIDExists(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sType As String) As Integer
        Dim sSql As String = ""
        Try
            If sType = "BCM" Then
                sSql = "Select CM_YearID from CMACheckMaster where CM_YearID=" & iYearID & " And CM_CompID=" & iACID & ""
            ElseIf sType = "BIA" Then
                sSql = "Select ACM_YearID from Audit_CheckList_Master where ACM_YearID=" & iYearID & " And ACM_CompID=" & iACID & ""
            ElseIf sType = "BRR" Then
                sSql = "Select RCM_YearID from Risk_CheckList_Master where RCM_YearID=" & iYearID & " And RCM_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load Field and Values required To upload sample format
    Public Function LoadAllFields(ByVal sAC As String, ByVal iACID As Integer, ByVal iMaterID As Integer) As DataTable
        Dim dtExceload As New DataTable
        Dim dsField As New DataSet
        Dim dRow As DataRow
        Dim aArray As Array, aArrayval As Array
        Dim sSql As String, sStr As String, sString As String
        Dim i As Integer, j As Integer
        Try
            sSql = "Select EUS_Fields,EUS_Values from Excel_Upload_Structure where EUS_Value=" & iMaterID & " And EUS_CompID=" & iACID & ""
            dsField = objDBL.SQLExecuteDataSet(sAC, sSql)
            If dsField.Tables(0).Rows.Count > 0 Then
                sStr = dsField.Tables(0).Rows(0)(0)
                aArray = sStr.Split(",")
                For i = 0 To UBound(aArray)
                    dtExceload.Columns.Add(aArray(i))
                Next
                sString = dsField.Tables(0).Rows(0)(1)
                aArrayval = sString.Split(",")
                dRow = dtExceload.NewRow
                For j = 0 To dtExceload.Columns.Count - 1
                    dRow(j) = aArrayval(j)
                Next
                dtExceload.Rows.Add(dRow)
            End If
            Return dtExceload
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOwnweName(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer, ByVal sUsrCode As String)
        Dim dtTab As New DataTable
        Dim sSql, sSql1 As String : Dim ChkRec As Integer = 0
        Dim dt As New DataTable
        Dim i As Integer
        Dim sStr As String
        Dim sUsers As String = ""
        Try
            sSql = "Select MEUM_UsrID from MST_Entity_UsrMap where MEUM_EntityID=" & iFunctionID & " and MEUM_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sStr = dt.Rows(i).Item(0).ToString
                    sUsers = sUsers & sStr
                    If sUsers.EndsWith(",") Then
                        sUsers = sUsers.Remove(Len(sUsers) - 1, 1)
                    End If
                Next
                If sUsers.StartsWith(",") Then
                    sUsers = sUsers.Remove(0, 1)
                End If
            End If

            sSql1 = "Select usr_ID from Sad_userdetails where Upper(usr_Code)=Upper('" & sUsrCode & "') and usr_ID in (" & sUsers & ")"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql1)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllUsersID(ByVal sAC As String, ByVal iACID As Integer, ByVal sUsrCode As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select Usr_ID from sad_userdetails Where Upper(usr_Code)=Upper('" & sUsrCode & "') and Usr_CompID=" & iACID & " order by Usr_Fullname"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckFUNOwnerFromFunIDSAPCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sUsrCode As String, ByVal iFunctionID As Integer) As Integer
        Dim sSql As String = ""
        Dim iUserID As Integer
        Try
            sSql = "Select Usr_ID from sad_userdetails Where Upper(usr_Code)=Upper('" & sUsrCode & "') and Usr_CompID=" & iACID & ""
            iUserID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            If iUserID = objclsGeneralFunctions.GetFunctionOwnerHODIDFromFunID(sAC, iACID, iFunctionID) Then
                Return iUserID
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueReviewNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal IReviewID As Integer) As String
        Dim sSql As String = "" : Dim ChkRec As String = 0
        Try
            sSql = "Select RPD_AsgNo from Risk_RRF_PlanningSchecduling_Details where RPD_PKID=" & IReviewID & " and RPD_CompID=" & iACID & " and RPD_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalar(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKCCIssueReviewNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal IReviewID As Integer) As String
        Dim sSql As String = "" : Dim ChkRec As String = 0
        Try
            sSql = "Select KCC_AsgNo from Risk_KCC_PlanningSchecduling_Details where KCC_PKID=" & IReviewID & " and KCC_CompID=" & iACID & " and KCC_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalar(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCategory(ByVal sAC As String, ByVal iACID As Integer, ByVal sCategory As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " and RAM_CompID=" & iACID & " And RAM_Category='RT' And RAM_DelFlag='A'"
            sSql = sSql & " and RAM_Name='" & sCategory & "'"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRisk(ByVal sAC As String, ByVal iACID As Integer, ByVal sRsik As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MRL_PKID From MST_RISK_Library Where MRL_CompID=" & iACID & " And MRL_RiskName='" & sRsik & "' and MRL_DelFlag='A'"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubCategory(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubCategory As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " and RAM_CompID=" & iACID & " And RAM_Category='RSC' And RAM_DelFlag='A'"
            sSql = sSql & " and RAM_Name='" & sSubCategory & "'"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckPeriod(ByVal sAC As String, ByVal iACID As Integer, ByVal sPeriod As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " and RAM_CompID=" & iACID & " And RAM_Category='RP' And RAM_DelFlag='A'"
            sSql = sSql & " and RAM_Name='" & sPeriod & "'"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckMeasure(ByVal sAC As String, ByVal iACID As Integer, ByVal sMeasure As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " and RAM_CompID=" & iACID & " And RAM_Category='RM' And RAM_DelFlag='A'"
            sSql = sSql & " and RAM_Name='" & sMeasure & "'"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckTraceNo(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTraceNo As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select KIR_TraceRefNo from Risk_KIR where KIR_TraceRefNo='" & sTraceNo & "' and KIR_CompID=" & iAcID & ""
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckExistingKRI(ByVal sAC As String, ByVal iACID As Integer, ByVal iCategoryID As Integer, ByVal iRiskID As Integer, ByVal iSubCategoryID As Integer, ByVal sDesc As String, ByVal iPeriodID As Integer, ByVal iMeasureID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select KRI_PKID From Risk_KRI where KRI_CategoryID=" & iCategoryID & " And KRI_RiskID=" & iRiskID & " And KRI_SubCategoryID=" & iSubCategoryID & ""
            sSql = sSql & " And KRI_RiskDescription='" & sDesc & "' And KRI_PeriodID=" & iPeriodID & " And KRI_MeasureID='" & iMeasureID & "' And KRI_CompID=" & iACID & ""
            sSql = sSql & " And KRI_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBRRChecklist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAsgnID As Integer, ByVal ifunID As Integer, ByVal iAreaID As Integer, ByVal sCheckPoint As String, ByVal sIssueDet As String, ByVal sAnnexure As String, ByVal sRiskCat As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select * FROM Risk_BRRChecklist_Details full join Risk_BRRChecklist_Mas on BRR_PKID=BRRD_BRRPKID and BRR_AsgID=" & iAsgnID & ""
            sSql = sSql & " where BRRD_YESNONA=2 and BRRD_CompID=" & iAcID & " "
            If ifunID > 0 Then
                sSql = sSql & " and BRRD_FunctionID=" & ifunID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and BRRD_AreaID=" & iAreaID & ""
            End If
            If sCheckPoint <> "" Then
                sSql = sSql & " and BRRD_CheckPoint='" & sCheckPoint & "' "
            End If
            If sIssueDet <> "" Then
                sSql = sSql & " and BRRD_IssueDetails='" & sIssueDet & "' "
            End If
            If sAnnexure <> "" Then
                sSql = sSql & " and BRRD_Annexure='" & sAnnexure & "' "
            End If
            If sRiskCat <> "" Then
                sSql = sSql & " and BRRD_RiskCategory='" & sRiskCat & "' "
            End If
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAreaID(ByVal sAC As String, ByVal iACID As Integer, ByVal sArea As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select cmm_ID from content_management_master where Cmm_Category='AR' and cmm_Desc='" & sArea & "' and CMM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBRRChecksID(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearID As Integer, ByVal sCheckPoints As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select RCM_ID From Risk_CheckList_Master Where RCM_CheckPoint='" & sCheckPoints & "' and RCM_YearID=" & iyearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBRRIssueTrackerRec(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAsgnID As Integer, ByVal ifunID As Integer, ByVal iAreaID As Integer, ByVal iCheckPoint As Integer, ByVal sIssueDet As String, ByVal iYearID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from Risk_BRRIssueTracker where BBRIT_AsgNo=" & iAsgnID & " and BBRIT_CompID=" & iAcID & " and BBRIT_FinancialYear=" & iYearID & ""
            If ifunID > 0 Then
                sSql = sSql & " and BBRIT_FunctionID=" & ifunID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and BBRIT_AreaID=" & iAreaID & ""
            End If
            If iCheckPoint > 0 Then
                sSql = sSql & " and BBRIT_CheckPointID='" & iCheckPoint & "' "
            End If
            If sIssueDet <> "" Then
                sSql = sSql & " and BBRIT_IssueHeading='" & sIssueDet & "' "
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMappedChecksId(ByVal sAc As String, ByVal iAcID As Integer, ByVal sCheckName As String, ByVal iFunID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MMM_ChecksID from mst_mapping_master where MMM_checks=Upper('" & sCheckName & "') And MMM_funID=" & iFunID & " and MMM_SEMID=" & iSubFunID & ""
            sSql = sSql & " and MMM_PMID=" & iProcessID & " and MMM_SPMID=" & iSubProcessID & " and MMM_RiskID=" & iRiskID & " and MMM_ControlID=" & iControlID & ""
            sSql = sSql & " And MMM_CompID=" & iAcID & " And MMM_Module='C'"
            ChkRec = objDBL.SQLExecuteScalarInt(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperID(ByVal sAC As String, ByVal iACID As Integer, ByVal sWorkPaper As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = " Select AFW_ID from Audit_FieldWork where Upper(AFW_WPNo)=Upper('" & sWorkPaper & "') And AFW_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckIsWorkPaperDone(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer, iSFID As Integer, iProcessID As Integer, iSPID As Integer, iRiskID As Integer, iControlID As Integer, iCheckID As Integer) As Boolean
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AFW_ID from Audit_FieldWork where AFW_AuditCodeID=" & iAuditID & " and AFW_FunctionID=" & iFunID & " and AFW_subFunctionID=" & iSFID & " and AFW_processID=" & iProcessID & " and AFW_SubProcessID=" & iSPID & " and AFW_RiskID=" & iRiskID & " and AFW_ControlID=" & iControlID & " and AFW_AuditChecksID=" & iCheckID & " and AFW_CompID=" & iACID & " and AFW_YearID=" & iYearID & ""
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
    Public Function CheckFAuditIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iWPID As Integer, ByVal sName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select AIT_PKID From audit_issueTracker_details Where AIT_WorkPaperID=" & iWPID & " And AIT_IssueHeading='" & sName & "' And AIT_YearID=" & iYearID & " and AIT_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSubProcessId(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubProcess As String, ByVal iProcessID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select SPM_ID from mst_SubProcess_master where Upper(SPM_Name)=Upper('" & sSubProcess & "') And SPM_CompID=" & iACID & " and SPM_PM_ID=" & iProcessID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBRRRiskChecklist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer, ByVal ifunID As Integer, ByVal iAreaID As Integer, ByVal sCheckPointNo As String, ByVal sCheckPoint As String, ByVal sRiskCat As String, ByVal iMethodologyID As Integer, ByVal iSSID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select * FROM Risk_CheckList_Master where RCM_CompID=" & iAcID & " And RCM_YearId=" & iYearID & ""
            If iPKID > 0 Then
                sSql = sSql & " and RCM_Id=" & iPKID & ""
            End If
            If ifunID > 0 Then
                sSql = sSql & " and RCM_FunctionId=" & ifunID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and RCM_AreaId=" & iAreaID & ""
            End If
            If sCheckPointNo <> "" Then
                sSql = sSql & " and RCM_CheckPointNo='" & sCheckPointNo & "'"
            End If
            If sCheckPoint <> "" Then
                sSql = sSql & " and RCM_CheckPoint='" & sCheckPoint & "' "
            End If
            If sRiskCat <> "" Then
                sSql = sSql & " and RCM_RiskCategory='" & sRiskCat & "' "
            End If
            If iMethodologyID > 0 Then
                sSql = sSql & " and RCM_MethodologyId=" & iMethodologyID & ""
            End If
            If iSSID > 0 Then
                sSql = sSql & " and RCM_SampleSize=" & iSSID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMethodologyID(ByVal sAC As String, ByVal iACID As Integer, ByVal sArea As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select cmm_ID from content_management_master where Cmm_Category='M' and cmm_Desc='" & sArea & "' and CMM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSSID(ByVal sAC As String, ByVal iACID As Integer, ByVal sArea As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select cmm_ID from content_management_master where Cmm_Category='SS' and cmm_Desc='" & sArea & "' and CMM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskBRRChecksID(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearID As Integer, ByVal sCheckPointNo As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select RCM_ID From Risk_CheckList_Master Where RCM_CheckPointNo='" & sCheckPointNo & "' and RCM_YearId=" & iyearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskImpactID(ByVal sAC As String, ByVal iACID As Integer, ByVal sImpact As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='RI' and Upper(RAM_NAME)=Upper('" & sImpact & "') and  RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskImpact(ByVal sAC As String, ByVal iACID As Integer, ByVal sImpact As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='RI' and Upper(RAM_NAME)=Upper('" & sImpact & "') and  RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRisklikelihoodID(ByVal sAC As String, ByVal iACID As Integer, ByVal sLikelihood As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='RL' and Upper(RAM_NAME)=Upper('" & sLikelihood & "') and  RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRisklikelihood(ByVal sAC As String, ByVal iACID As Integer, ByVal sLikelihood As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='RL' and Upper(RAM_NAME)=Upper('" & sLikelihood & "') and RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditFunType(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCheckPointNo As String, ByVal sCheckPoints As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select ACM_FunType From Audit_CheckList_Master Where ACM_CheckPoint='" & sCheckPoints & "' And ACM_YearId=" & iYearID & " And ACM_CompID=" & iACID & " And ACM_CheckPointNo='" & sCheckPointNo & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskFunType(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCheckPointNo As String, ByVal sCheckPoints As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select RCM_FunType From Risk_CheckList_Master Where RCM_CheckPoint='" & sCheckPoints & "' And RCM_YearId=" & iYearID & " And RCM_CompID=" & iACID & " And RCM_CheckPointNo='" & sCheckPointNo & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBCMFunType(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCheckPointNo As String, ByVal sCheckPoints As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select CM_FunType From CMACheckMaster Where CM_CheckPoint='" & sCheckPoints & "' And CM_YearId=" & iYearID & " And CM_CompID=" & iACID & " And CM_CheckPointNo='" & sCheckPointNo & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMasterCheckCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearId As Integer) As Integer
        Dim ssql As String
        Try
            ssql = "Select count(*) From Risk_CheckList_Master where RCM_YearId=" & iYearId & " and RCM_CompID=" & iACID & " And RCM_Delflag='A'"
            Return objDBL.SQLExecuteScalarInt(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRCSADetailsPKID(ByVal sAc As String, ByVal iAcID As Integer, ByVal iRCSAPKID As Integer, ByVal iSFID As Integer, ByVal iProID As Integer, ByVal iSPID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iCheckID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "select RCSAD_PKID from Risk_RCSA_Details where RCSAD_RCSAPKID=" & iRCSAPKID & " And RCSAD_SemID=" & iSFID & " and RCSAD_PMID=" & iProID & " and RCSAD_SPMID=" & iSPID & " and RCSAD_RiskID=" & iRiskID & " and RCSAD_ControlID=" & iControlID & " and RCSAD_ChecksID=" & iCheckID & " and RCSAD_CompID=" & iAcID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOperativeEfficiencyID(ByVal sAC As String, ByVal iACID As Integer, ByVal sOE As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='OES' and Upper(RAM_NAME)=Upper('" & sOE & "') and  RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOperativeEfficiency(ByVal sAC As String, ByVal iACID As Integer, ByVal sOE As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='OES' and Upper(RAM_NAME)=Upper('" & sOE & "') and RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDesignEfficiency(ByVal sAC As String, ByVal iACID As Integer, ByVal sDE As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='DES' and Upper(RAM_NAME)=Upper('" & sDE & "') and RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDesignEfficiencyID(ByVal sAC As String, ByVal iACID As Integer, ByVal sDE As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select RAM_PKID From Risk_GeneralMaster Where RAM_Delflag='A' And RAM_Category='DES' and Upper(RAM_NAME)=Upper('" & sDE & "') and  RAM_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBIAChecklist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAsgnID As Integer, ByVal ifunID As Integer, ByVal iAreaID As Integer, ByVal sCheckPoint As String, ByVal sIssueDet As String, ByVal sAnnexure As String, ByVal sRiskCat As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select * FROM  Audit_BIAChecklist_Details full join Audit_BIAChecklist_Mas on BIAC_PKID=BIACD_BIACPKID and BIAC_AsgID=" & iAsgnID & ""
            sSql = sSql & " where BIACD_YESNONA=2 and BIACD_CompID=" & iAcID & " "
            If ifunID > 0 Then
                sSql = sSql & " and BIACD_FunctionID=" & ifunID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and BIACD_AreaID=" & iAreaID & ""
            End If
            If sCheckPoint <> "" Then
                sSql = sSql & " and BIACD_CheckPoint='" & sCheckPoint & "' "
            End If
            If sIssueDet <> "" Then
                sSql = sSql & " and BIACD_IssueDetails='" & sIssueDet & "' "
            End If
            If sAnnexure <> "" Then
                sSql = sSql & " and BIACD_Annexure='" & sAnnexure & "' "
            End If
            If sRiskCat <> "" Then
                sSql = sSql & " and BIACD_RiskCategory='" & sRiskCat & "' "
            End If
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBIAIssueTrackerRec(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAsgnID As Integer, ByVal ifunID As Integer, ByVal iAreaID As Integer, ByVal iCheckPoint As Integer, ByVal sIssueDet As String, ByVal iYearID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from Audit_BIAIssueTracker where BIAIT_AsgNo=" & iAsgnID & " and BIAIT_CompID=" & iAcID & " and BIAIT_FinancialYear=" & iYearID & ""
            If ifunID > 0 Then
                sSql = sSql & " and BIAIT_FunctionID=" & ifunID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and BIAIT_AreaID=" & iAreaID & ""
            End If
            If iCheckPoint > 0 Then
                sSql = sSql & " and BIAIT_CheckPointID='" & iCheckPoint & "' "
            End If
            If sIssueDet <> "" Then
                sSql = sSql & " and BIAIT_IssueHeading='" & sIssueDet & "' "
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBIAChecksID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCheckPoints As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select ACM_Id From Audit_CheckList_Master Where ACM_CheckPoint='" & sCheckPoints & "' and ACM_YearId=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRAConductTOExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("Sub Function")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("Sub Process")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("Risk Type")
            dtTab.Columns.Add("Impact")
            dtTab.Columns.Add("Likelihood")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("Operative Efficiency")
            dtTab.Columns.Add("Design Efficiency")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("Remarks")

            sSql = "Select ENT_EntityName as Functions,a.SEM_NAME,b.PM_NAME,c.SPM_NAME,d.MRL_RiskName,e.RAM_Name, f.MCL_ControlName,h.RAM_Name As Impact,i.RAM_Name As Likelihood,"
            sSql = sSql & " g.CHK_CheckName, j.RAM_Name As OESName,k.RAM_Name As DESName,RAD_Remarks From Risk_RA_Details "
            sSql = sSql & " Left join mst_Entity_master On ENT_ID=" & iFunctionID & " And ENT_CompID = " & iACID & ""
            sSql = sSql & " Left join MST_SUBENTITY_MASTER a on a.SEM_ID=RAD_SEMID and SEM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_PROCESS_MASTER b on b.PM_ID=RAD_PMID and  PM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_SUBPROCESS_MASTER c on c.SPM_ID=RAD_SPMID and SPM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_RISK_Library d on d.MRL_PKID=RAD_RiskID and MRL_CompID=" & iACID & ""
            sSql = sSql & " Left join Risk_GeneralMaster e on e.RAM_PKID=RAD_RiskTypeID and RAM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_CONTROL_Library f on f.MCL_PKID=RAD_ControlID and MCL_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_Checks_Master g on g.CHK_ControlID=RAD_ControlID And g.CHK_ID=RAD_ChecksID And g.CHK_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster h on h.RAM_Category='RI' And h.RAM_YearID=" & iYearID & " And h.RAM_PKID=RAD_ImpactID and h.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster i On i.RAM_Category='RL' And i.RAM_YearID=" & iYearID & " And i.RAM_PKID=RAD_LikelihoodID and i.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster j on j.RAM_Category='OES' And j.RAM_YearID=" & iYearID & " And j.RAM_PKID=RAD_OES and j.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster k On k.RAM_Category='DES' And k.RAM_YearID=" & iYearID & " And k.RAM_PKID=RAD_DES and k.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where RAD_RAPKID In (Select RA_PKID from Risk_RA where RA_CustID=" & iCustID & " And RA_FinancialYear=" & iYearID & " And RA_FunID=" & iFunctionID & " And RA_CompID=" & iACID & ")"
            sSql = sSql & " And RAD_CompID=" & iACID & " Order by RAD_SEMID,RAD_PMID,RAD_SPMID,RAD_RiskID,RAD_ControlID,RAD_ChecksID"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Functions"))
                dr("Sub Function") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_NAME"))
                dr("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PM_NAME"))
                dr("Sub Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SPM_NAME"))

                dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MRL_RiskName"))
                dr("Risk Type") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAM_Name"))
                dr("Impact") = "" : dr("Likelihood") = "" : dr("Operative Efficiency") = "" : dr("Design Efficiency") = ""

                If IsDBNull(dt.Rows(i)("Impact")) = False Then
                    dr("Impact") = dt.Rows(i)("Impact")
                End If
                If IsDBNull(dt.Rows(i)("Likelihood")) = False Then
                    dr("Likelihood") = dt.Rows(i)("Likelihood")
                End If
                dr("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MCL_ControlName"))
                If IsDBNull(dt.Rows(i)("OESName")) = False Then
                    dr("Operative Efficiency") = dt.Rows(i)("OESName")
                End If

                If IsDBNull(dt.Rows(i)("DESName")) = False Then
                    dr("Design Efficiency") = dt.Rows(i)("DESName")
                End If
                dr("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CHK_CheckName"))
                dr("Remarks") = ""
                If IsDBNull(dt.Rows(i)("RAD_Remarks")) = False Then
                    dr("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAD_Remarks"))
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRADetailsPKID(ByVal sAc As String, ByVal iAcID As Integer, ByVal iRCSAPKID As Integer, ByVal iSFID As Integer, ByVal iProID As Integer, ByVal iSPID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iCheckID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "select RAD_PKID from Risk_RA_Details where RAD_RAPKID=" & iRCSAPKID & " And RAD_SemID=" & iSFID & " and RAD_PMID=" & iProID & " and RAD_SPMID=" & iSPID & " and RAD_RiskID=" & iRiskID & " and RAD_ControlID=" & iControlID & " and RAD_ChecksID=" & iCheckID & " and RAD_CompID=" & iAcID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTypeofTestID(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTestType As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select cmm_ID from Content_Management_Master Where Cmm_Category='TOT' and Upper(cmm_Desc)=Upper('" & sTestType & "') and CMM_CompID=" & iAcID & ""
            ChkRec = objDBL.SQLExecuteScalar(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetConclusionID(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTestType As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select cmm_ID from Content_Management_Master Where Cmm_Category='WPC' and Upper(cmm_Desc)=Upper('" & sTestType & "') and CMM_CompID=" & iAcID & ""
            ChkRec = objDBL.SQLExecuteScalar(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckMappingOfMasterExistForFieldWork(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer,
                                                           ByVal iSubProcessID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MMM_ID from MST_MAPPING_MASTER Where MMM_FunID=" & iFunID & " And MMM_SEMID=" & iSubFunID & " And MMM_PMID=" & iProcessID & " "
            sSql = sSql & " And MMM_SPMID=" & iSubProcessID & " And MMM_RiskID=" & iRiskID & " And MMM_ControlID=" & iControlID & " And MMM_ChecksID=" & iChecksID & ""
            sSql = sSql & " And MMM_DelFlag='A' And MMM_Module='C' And MMM_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBCMIntegratedChecklist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer, ByVal ifunID As Integer, ByVal iAreaID As Integer, ByVal sCheckPointNo As String, ByVal sCheckPoint As String, ByVal sRiskCat As String, ByVal iMethodologyID As Integer, ByVal iSSID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select * FROM cmacheckmaster where CM_CompID=" & iAcID & " And CM_YearId=" & iYearID & ""
            If iPKID > 0 Then
                sSql = sSql & " and CM_Id=" & iPKID & ""
            End If
            If ifunID > 0 Then
                sSql = sSql & " and CM_FunctionId=" & ifunID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and CM_AreaId=" & iAreaID & ""
            End If
            If sCheckPointNo <> "" Then
                sSql = sSql & " and CM_CheckPointNo='" & sCheckPointNo & "'"
            End If
            If sCheckPoint <> "" Then
                sSql = sSql & " and CM_CheckPoint='" & sCheckPoint & "' "
            End If
            If sRiskCat <> "" Then
                sSql = sSql & " and CM_RiskCategory='" & sRiskCat & "' "
            End If
            If iMethodologyID > 0 Then
                sSql = sSql & " and CM_MethodologyId=" & iMethodologyID & ""
            End If
            If iSSID > 0 Then
                sSql = sSql & " and CM_SampleSize=" & iSSID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBCMBulkUploadMasterRec(ByVal sAc As String, ByVal iAcID As Integer, ByVal sBranch As String, ByVal sBranchCode As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select org_node from sad_org_structure where Org_levelcode=4 And Org_CompID=" & iAcID & ""
            If sBranch <> "" Then
                sSql = sSql & " and org_name='" & sBranch & "'"
            End If
            If sBranchCode <> "" Then
                sSql = sSql & " and org_Code='" & sBranchCode & "'"
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBCMBulkUploadBrnachRec(ByVal sAc As String, ByVal iAcID As Integer, ByVal sBranchCode As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select org_node from sad_org_structure where Org_levelcode=4 And Org_CompID=" & iAcID & " and org_Code='" & sBranchCode & "'"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            If dt.Rows.Count = 1 Then
                Return dt.Rows(0)("org_node")
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function CheckZOMABSMRMInBranch(ByVal sAC As String, ByVal iACID As Integer, ByVal sUserCode As String, ByVal iBranchID As Integer, ByVal sUserType As String) As Integer
    '    Dim sSql As String, sSqlSub As String
    '    Dim iZoneID As Integer
    '    Dim sRegionIDs As String = "", sAreaIDs As String = "", sBranchIDs As String = ""
    '    Dim dtTab As New DataTable
    '    Try
    '        sSql = "Select org_parent From sad_org_structure Where Org_levelCode=2 And Org_CompID=" & iACID & " And Org_Node in(Select org_parent From sad_org_structure Where Org_levelCode=3 And Org_CompID=" & iACID & " And Org_Node in(Select org_parent From sad_org_structure Where Org_levelCode=4 And Org_CompID=" & iACID & " And Org_Node=" & iBranchID & "))"
    '        iZoneID = objDBL.SQLExecuteScalarInt(sAC, sSql)

    '        If iZoneID > 0 Then
    '            sSql = "Select usr_ID from sad_USERDETAILS WHERE Usr_Code ='" & sUserCode & "' And "

    '            '--------------------------------------------- Get Region IDs -----------------------------------------------------------------------
    '            sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent=" & iZoneID & " And Org_LevelCode=2 And Org_CompID=" & iACID & ""
    '            dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
    '            For i = 0 To dtTab.Rows.Count - 1
    '                sRegionIDs = sRegionIDs & "," & dtTab.Rows(i)("Org_Node")
    '            Next

    '            '--------------------------------------------- Get Areas IDs -------------------------------------------------------------------------
    '            If sRegionIDs <> "" Then
    '                dtTab = Nothing
    '                If sRegionIDs.StartsWith(",") = True Then
    '                    sRegionIDs = sRegionIDs.Remove(0, 1)
    '                End If
    '                If sRegionIDs.EndsWith(",") = True Then
    '                    sRegionIDs = sRegionIDs.Remove(Len(sRegionIDs) - 1, 1)
    '                End If
    '                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sRegionIDs & ") And Org_LevelCode=3 And Org_CompID=" & iACID & ""
    '                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
    '                For i = 0 To dtTab.Rows.Count - 1
    '                    sAreaIDs = sAreaIDs & "," & dtTab.Rows(i)("Org_Node")
    '                Next
    '            End If

    '            '--------------------------------------------- Get Branch IDs -------------------------------------------------------------------------
    '            If sAreaIDs <> "" Then
    '                dtTab = Nothing
    '                If sAreaIDs.StartsWith(",") = True Then
    '                    sAreaIDs = sAreaIDs.Remove(0, 1)
    '                End If
    '                If sAreaIDs.EndsWith(",") = True Then
    '                    sAreaIDs = sAreaIDs.Remove(Len(sAreaIDs) - 1, 1)
    '                End If
    '                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sAreaIDs & ") And Org_LevelCode=4 And Org_CompID=" & iACID & ""
    '                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
    '                For i = 0 To dtTab.Rows.Count - 1
    '                    sBranchIDs = sBranchIDs & "," & dtTab.Rows(i)("Org_Node")
    '                Next
    '            End If

    '            If sBranchIDs <> "" Then
    '                If sBranchIDs.StartsWith(",") = True Then
    '                    sBranchIDs = sBranchIDs.Remove(0, 1)
    '                End If
    '                If sBranchIDs.EndsWith(",") = True Then
    '                    sBranchIDs = sBranchIDs.Remove(Len(sBranchIDs) - 1, 1)
    '                End If
    '            End If

    '            sSql = sSql & " Usr_OrgnID In (" & iZoneID & ""
    '            If sRegionIDs <> "" Then
    '                sSql = sSql & "," & sRegionIDs & ""
    '            End If
    '            If sAreaIDs <> "" Then
    '                sSql = sSql & " ," & sAreaIDs & ""
    '            End If
    '            If sBranchIDs <> "" Then
    '                sSql = sSql & "," & sBranchIDs & ""
    '            End If
    '            sSql = sSql & ") And  Usr_CompID = " & iACID & " And Usr_designation In(Select mas_ID from SAD_GRPDESGN_General_Master where "
    '            If sUserType = "ZOM" Then
    '                sSql = sSql & " ((Mas_Code)='ZOM') and"
    '            ElseIf sUserType = "ABSMRM" Then
    '                sSql = sSql & " (Upper(Mas_Code)='ABSM' or Upper(Mas_Code)='RM') and "
    '            End If
    '            sSql = sSql & " Mas_compID=" & iACID & ")"

    '            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
    '        End If
    '        Return 0
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CheckZOMABSMRMInBranch(ByVal sAC As String, ByVal iACID As Integer, ByVal sUserCode As String, ByVal iBranchID As Integer, ByVal sUserType As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_ID from sad_USERDETAILS WHERE Usr_Code ='" & sUserCode & "' And Usr_CompID = " & iACID & " And Usr_designation In(Select mas_ID from SAD_GRPDESGN_General_Master where"
            If sUserType = "ZOM" Then
                sSql = sSql & " ((Mas_Code)='ZOM') and"
            ElseIf sUserType = "ABSMRM" Then
                sSql = sSql & " (Upper(Mas_Code)='ABSM' or Upper(Mas_Code)='RM') and "
            End If
            sSql = sSql & " Mas_compID=" & iACID & ")"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllBCMAreas(ByVal sAc As String, ByVal iAcID As Integer) As String
        Dim ds As New DataSet
        Dim sAreas As String = ""
        Dim i As Integer
        Dim sSql As String
        Try
            sSql = "Select cmm_id,cmm_desc from content_management_master where cmm_category='AR' and cmm_compid =" & iAcID & " order by cmm_id"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sAreas = sAreas & ";" & ds.Tables(0).Rows(i)("cmm_id")
                Next
                If sAreas <> "" Then
                    sAreas = sAreas & ";"
                End If
            End If
            Return sAreas
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub AddBCMBulkData(ByVal sAc As String, iAcID As Integer, iYearID As Integer, sCheckDueDate As String, ByVal dtTable As DataTable)
        Dim sSql As String
        Dim i As Integer
        Try
            objDBL.SQLExecuteNonQuery(sAc, "Truncate table CMABulkUpload")
            For i = 0 To dtTable.Rows.Count - 1
                sSql = "Select Org_node from SAD_Org_Structure where org_Code='" & dtTable.Rows(i).Item(1).ToString & "' And org_node In "
                sSql = sSql & "(Select CVA_BranchID From CMA_Vendor_Assignment Where CVA_CompID=" & iAcID & " And CVA_AsgnID In "
                sSql = sSql & "(Select CAD_ID From CMA_Assignment_Details Where CAD_AuditYear=" & iYearID & " And CAD_CompID=" & iAcID & " And substring(cad_duemonths, 4, 2) = substring('" & sCheckDueDate & "', 4, 2)))"

                If objDBL.SQLCheckForRecord(sAc, sSql) = False Then
                    sSql = "" : sSql = "Insert into CMABulkUpload values (" & dtTable.Rows(i).Item(0).ToString & ",'" & dtTable.Rows(i).Item(1).ToString & "','" & dtTable.Rows(i).Item(2).ToString & "','" & dtTable.Rows(i).Item(3).ToString & "'," & objclsGRACeGeneral.FormatDtForRDBMS(dtTable.Rows(i).Item(4).ToString, "I") & "," & objclsGRACeGeneral.FormatDtForRDBMS(dtTable.Rows(i).Item(5).ToString, "I") & ",'" & dtTable.Rows(i).Item(6).ToString & "')"
                    objDBL.SQLExecuteNonQuery(sAc, sSql)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDistinctBCMZOM(ByVal sAc As String) As DataTable
        Try
            GetDistinctBCMZOM = objDBL.SQLExecuteDataSet(sAc, "Select Distinct CMA_ZOMSAPCODE, MAX(CMA_ID) from CMABulkUpload GROUP BY CMA_ZOMSAPCODE ORDER BY MAX(CMA_ID) ASC, CMA_ZOMSAPCODE").Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUsrIdFromSapcode(ByVal sAc As String, ByVal iAcID As Integer, ByVal sSapCode As String) As Integer
        Try
            GetUsrIdFromSapcode = objDBL.SQLExecuteScalar(sAc, "Select Usr_ID from Sad_Userdetails where Usr_code ='" & sSapCode & "' And Usr_CompId=" & iAcID & "")
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBCMBranches(ByVal sAc As String, ByRef sZOMID As String) As String
        Dim sSql As String, sStr As String
        Dim i As Integer
        Dim ds As New DataSet
        Try
            sSql = "Select CMA_BranchCode from CMABulkUpload where CMA_ZOMSAPCode='" & sZOMID & "'"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                sSql = ""
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sStr = objDBL.SQLExecuteScalar(sAc, "Select Org_node from SAD_Org_Structure where org_Code='" & ds.Tables(0).Rows(i).Item(0) & "'")
                    If sStr <> Nothing Then
                        sSql = sSql & "," & sStr
                    End If
                Next
                If sSql <> "" Then
                    If sSql.EndsWith(",") Then
                        sSql = Right(sSql, sSql.Length - 1)
                    End If
                    sSql = sSql & ","
                End If
            Else
                sSql = "No"
            End If
            Return sSql
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBCMVendorDetails(ByVal sAc As String, ByRef sZOMID As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from CMABulkUpload Where CMA_ZOMSAPCode='" & sZOMID & "'"
            GetBCMVendorDetails = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            Return GetBCMVendorDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEndDate(ByVal sAc As String, ByRef sZOMID As Integer) As Date
        Dim sSql As String
        Try
            sSql = "Select CMA_EndDAte from CMABulkUpload where CMA_ID=" & sZOMID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStartDate(ByVal sAc As String, ByRef sZOMID As Integer) As Date
        Dim sSql As String
        Try
            sSql = "Select CMA_StDate from CMABulkUpload where CMA_ID=" & sZOMID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVendorBranchID(ByVal sAc As String, ByVal iAcID As Integer, ByVal sIRDAID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Org_node from sad_org_structure where Org_Code='" & sIRDAID & "' And Org_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVendorID(ByVal sAc As String, ByVal iAcID As Integer, ByVal sABSMSAPID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_Id from SAD_UserDetails where usr_Code='" & sABSMSAPID & "' And Usr_CompId=" & iAcID & ""
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUsrIdFromBranchManager(ByVal sAc As String, ByVal iAcID As Integer, ByVal sBranchManagerCode As String) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where Usr_code ='" & sBranchManagerCode & "' And Usr_CompId=" & iAcID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCRSADetailsPKID(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCRSAPKID As Integer, ByVal iSFID As Integer, ByVal iProID As Integer, ByVal iSPID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iCheckID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "select CRSAD_PKID from Compliance_CRSA_Details where CRSAD_CRSAPKID=" & iCRSAPKID & " And CRSAD_SemID=" & iSFID & " and CRSAD_PMID=" & iProID & " and CRSAD_SPMID=" & iSPID & " and CRSAD_RiskID=" & iRiskID & " and CRSAD_ControlID=" & iControlID & " and CRSAD_ChecksID=" & iCheckID & " and CRSAD_CompID=" & iAcID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCRADetailsPKID(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCRSAPKID As Integer, ByVal iSFID As Integer, ByVal iProID As Integer, ByVal iSPID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iCheckID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "select CRAD_PKID from Compliance_CRA_Details where CRAD_CRAPKID=" & iCRSAPKID & " And CRAD_SemID=" & iSFID & " and CRAD_PMID=" & iProID & " and CRAD_SPMID=" & iSPID & " and CRAD_RiskID=" & iRiskID & " and CRAD_ControlID=" & iControlID & " and CRAD_ChecksID=" & iCheckID & " and CRAD_CompID=" & iAcID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAc, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCRWorkPaperMatrixToExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, iFunID As Integer, iSubFunID As Integer, ByVal iComplianceID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable, dtWP As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Try
            dt.Columns.Add("Compliance Code")
            dt.Columns.Add("Function")
            dt.Columns.Add("Sub Function")
            dt.Columns.Add("Process")
            dt.Columns.Add("Sub Process")
            dt.Columns.Add("Risk")
            dt.Columns.Add("Controls")
            dt.Columns.Add("Compliance Checks")
            dt.Columns.Add("Type of Test")
            dt.Columns.Add("Work Paper Remarks")
            dt.Columns.Add("Observations")
            dt.Columns.Add("Deviations")
            dt.Columns.Add("Conclusion")
            dt.Columns.Add("Results")

            sSql = "Select MMM_FunID As FUNID, MMM_SEMID As SUbFUNID, MMM_PMID As PROID, MMM_SPMID As SUBPROID, MMM_Risk As Risk, MMM_RiskID As RiskID, MMM_ControlID As ControlID, "
            sSql = sSql & " MMM_Control As Control, MMM_Checks As ComplianceChecks, MMM_ChecksID As ComplianceChecksID, a.PM_Name, b.SPM_Name From MST_MAPPING_MASTER "
            sSql = sSql & " Left Join MST_PROCESS_MASTER a On a.PM_ID=MMM_PMID And a.PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER b On b.SPM_ID=MMM_SPMID And b.SPM_CompID=" & iACID & ""
            sSql = sSql & " Where MMM_FunID=" & iFunID & " And MMM_Module='C' And MMM_DelFlag='A' And MMM_SEMID=" & iSubFunID & " And MMM_YearID=" & iYearID & " And MMM_CompID=" & iACID & ""
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            For j = 0 To dtTab.Rows.Count - 1
                drow = dt.NewRow
                drow("Compliance Code") = objDBL.SQLExecuteScalar(sAC, "Select CP_ComplianceCode From Compliance_Plan  Where  CP_ID=" & iComplianceID & " And CP_CompID = " & iACID & " and CP_YEarID=" & iYearID & "")
                drow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(objDBL.SQLExecuteScalar(sAC, "Select ENT_ENTITYName From mst_Entity_master Where  ENT_ID=" & iFunID & " And ENT_CompID = " & iACID & ""))
                drow("Sub Function") = objclsGRACeGeneral.ReplaceSafeSQL(objDBL.SQLExecuteScalar(sAC, "Select SEM_NAME From MST_SUBENTITY_MASTER Where  SEM_ID=" & iSubFunID & " And SEM_CompID = " & iACID & ""))
                If IsDBNull(dtTab.Rows(j)("PROID")) = False Then
                    drow("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("PM_Name"))
                End If
                If IsDBNull(dtTab.Rows(j)("SUBPROID")) = False Then
                    drow("Sub Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SPM_Name"))
                End If
                If IsDBNull(dtTab.Rows(j)("RiskID")) = False Then
                    drow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("Risk"))
                End If
                If IsDBNull(dtTab.Rows(j)("ControlID")) = False Then
                    drow("Controls") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("Control"))
                End If
                If IsDBNull(dtTab.Rows(j)("ComplianceChecksID")) = False Then
                    drow("Compliance Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("ComplianceChecks"))
                End If
                sSql = "" : sSql = "Select CFW_ID As PKID,CFW_TypeofTestID,CFW_WorkPaperRemarks,CFW_Observations,CFW_DeviationsID,CFW_ConclusionID,CFW_ResultID,CFW_Status,"
                sSql = sSql & " e.CMM_Desc As TypeofTestID,f.CMM_Desc As ConclusionID From Compliance_FieldWork"
                sSql = sSql & " Left Join Content_Management_Master e On e.CMM_ID=CFW_TypeofTestID And e.CMM_CompID=" & iACID & ""
                sSql = sSql & " Left Join Content_Management_Master f On f.CMM_ID=CFW_ConclusionID And f.CMM_CompID=" & iACID & ""
                sSql = sSql & " Where CFW_CompID=" & iACID & " And CFW_ComplianceCodeid=" & iComplianceID & " And CFW_FunctionID=" & dtTab.Rows(j)("FUNID") & " And "
                sSql = sSql & " CFW_SubFunctionID=" & dtTab.Rows(j)("SUbFUNID") & " And CFW_ProcessID=" & dtTab.Rows(j)("PROID") & " And CFW_SubProcessID=" & dtTab.Rows(j)("SUBPROID") & " And"
                sSql = sSql & " CFW_RiskID=" & dtTab.Rows(j)("RiskID") & " And CFW_ControlID= " & dtTab.Rows(j)("ControlID") & " and CFW_ComplianceChecksID=" & dtTab.Rows(j)("ComplianceChecksID") & ""
                dtWP = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtWP.Rows.Count = 0 Then
                    drow("Type of Test") = "" : drow("Work Paper Remarks") = "" : drow("Observations") = ""
                    drow("Deviations") = "" : drow("Conclusion") = "" : drow("Results") = ""
                Else
                    If IsDBNull(dtWP.Rows(0)("CFW_Status")) = False Then
                        If dtWP.Rows(0)("CFW_Status") = "Submitted" Then
                            Continue For
                        End If
                    End If
                    drow("Type of Test") = "" : drow("Work Paper Remarks") = "" = "" : drow("Observations") = ""
                    drow("Deviations") = 0 : drow("Conclusion") = 0 : drow("Results") = 0
                    If IsDBNull(dtWP.Rows(0)("CFW_WorkPaperRemarks")) = False Then
                        drow("Work Paper Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(0)("CFW_WorkPaperRemarks"))
                    End If
                    If IsDBNull(dtWP.Rows(0)("CFW_Observations")) = False Then
                        drow("Observations") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(0)("CFW_Observations"))
                    End If
                    If IsDBNull(dtWP.Rows(0)("CFW_TypeofTestID")) = False Then
                        drow("Type of Test") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(0)("TypeofTestID"))
                    End If
                    If IsDBNull(dtWP.Rows(0)("CFW_DeviationsID")) = False Then
                        drow("Deviations") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(0)("CFW_DeviationsID"))
                        If drow("Deviations") = 1 Then
                            drow("Deviations") = "Yes"
                        ElseIf drow("Deviations") = 2 Then
                            drow("Deviations") = "No"
                        End If
                    End If
                    If IsDBNull(dtWP.Rows(0)("CFW_ConclusionID")) = False Then
                        drow("Conclusion") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(0)("ConclusionID"))
                    End If
                    If IsDBNull(dtWP.Rows(0)("CFW_ResultID")) = False Then
                        drow("Results") = dtWP.Rows(0)("CFW_ResultID")
                        If drow("Results") = 1 Then
                            drow("Results") = "Open"
                        ElseIf drow("Results") = 2 Then
                            drow("Results") = "Closed"
                        End If
                    End If
                End If
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceCodeID(ByVal sAC As String, ByVal iACID As Integer, ByVal sComplianceCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select CP_ID from Compliance_Plan where Upper(CP_ComplianceCode)=Upper('" & sComplianceCode & "') And CP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCRWorkPaperID(ByVal sAC As String, ByVal iACID As Integer, ByVal sWorkPaper As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = " Select CFW_ID from Compliance_FieldWork where Upper(CFW_WPNo)=Upper('" & sWorkPaper & "') And CFW_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCRIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iWPID As Integer, ByVal sName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CIT_PKID From Compliance_issueTracker_details Where CIT_WorkPaperID=" & iWPID & " And CIT_IssueHeading='" & sName & "' And CIT_YearID=" & iYearID & " and CIT_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckIsCRWorkPaperDone(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iComplianceID As Integer, ByVal iFunID As Integer, iSFID As Integer, iProcessID As Integer, iSPID As Integer, iRiskID As Integer, iControlID As Integer, iCheckID As Integer) As Boolean
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CFW_ID from Compliance_FieldWork where CFW_ComplianceCodeID=" & iComplianceID & " and CFW_FunctionID=" & iFunID & " and CFW_subFunctionID=" & iSFID & " and CFW_processID=" & iProcessID & " and CFW_SubProcessID=" & iSPID & " and CFW_RiskID=" & iRiskID & " and CFW_ControlID=" & iControlID & " and CFW_ComplianceChecksID=" & iCheckID & " and CFW_CompID=" & iACID & " and CFW_YearID=" & iYearID & ""
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
    Public Function GetComplianceCodeID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sComplianceCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select CP_ID from Compliance_Plan where Upper(CP_ComplianceCode)=Upper('" & sComplianceCode & "') And CP_CompID=" & iACID & " and CP_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCRComplianceIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iWPID As Integer, ByVal sName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CIT_PKID From Compliance_issueTracker_details Where CIT_WorkPaperID=" & iWPID & " And CIT_IssueHeading='" & sName & "' And CIT_YearID=" & iYearID & " and CIT_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckMappingOfMasterExistForAuditRCSA(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer,
                                                           ByVal iSubProcessID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MMM_ID from MST_MAPPING_MASTER Where MMM_FunID=" & iFunID & " And MMM_SEMID=" & iSubFunID & " And MMM_PMID=" & iProcessID & " "
            sSql = sSql & " And MMM_SPMID=" & iSubProcessID & " And MMM_RiskID=" & iRiskID & " And MMM_ControlID=" & iControlID & " And MMM_ChecksID=" & iChecksID & ""
            sSql = sSql & " And MMM_DelFlag='A' And MMM_Module='R' And MMM_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInherentIDFromName(ByVal sAC As String, ByVal iACID As Integer, ByVal sName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select MIM_ID from MST_InherentRisk_Master where MIM_CompID=" & iACID & " And Upper(MIM_Name)=Upper('" & sName & "')"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubProcessName As String, ByVal iProcessID As Integer, ByVal iSubFunID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select SPM_ID from MST_SUBPROCESS_MASTER where Upper(SPM_NAME)=Upper('" & sSubProcessName & "') And SPM_ENT_ID=" & iFunID & " And SPM_SEM_ID=" & iSubFunID & ""
            sSql = sSql & " And SPM_PM_ID=" & iProcessID & " And SPM_CompID=" & iACID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditCodeID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sAuditCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select APM_ID from Audit_APM_Details where Upper(APM_AuditCode)=Upper('" & sAuditCode & "') And APM_CompID=" & iACID & " and APM_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sAuditCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select APM_CustID from Audit_APM_Details where Upper(APM_AuditCode)=Upper('" & sAuditCode & "') And APM_CompID=" & iACID & " and APM_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOpeningBalance(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = "", asql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0
        Try
            dt.Columns.Add("SLNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("Description")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iACID & " And gl_delflag='C' and gl_Status ='A' and (gl_head=2 or gl_head=3) order by gl_glcode"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                While dr.Read
                    dRow = dt.NewRow()
                    dRow("SLNo") = i + 1

                    If IsDBNull(dr("gl_glcode")) = False Then
                        dRow("GLCode") = dr("gl_glcode")
                    End If

                    If IsDBNull(dr("gl_Desc")) = False Then
                        dRow("Description") = dr("gl_Desc")
                    End If

                    dt.Rows.Add(dRow)
                    i = i + 1
                End While
            End If
            dr.Close()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCustName As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select Cust_Id from SAD_CUSTOMER_MASTER where Upper(Cust_Name)=Upper('" & sCustName & "') And cust_Compid=" & iACID & " And CUST_DelFlg = 'A'"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskMasterCheckCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearId As Integer) As Integer
        Dim ssql As String
        Try
            ssql = "Select count(*) From Risk_CheckList_Master where RCM_YearId=" & iYearId & " and RCM_CompID=" & iACID & " And RCM_Delflag='A'"
            Return objDBL.SQLExecuteScalarInt(sAC, ssql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueComplianceNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal IReviewID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = "" : Dim ChkRec As String = 0
        Try
            sSql = "Select CP_ComplianceCode from Compliance_Plan where CP_ID=" & IReviewID & " and CP_CompID=" & iACID & " and CP_YearID=" & iYearID & " And CP_CustomerID=" & iCustID & ""
            ChkRec = objDBL.SQLExecuteScalar(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCRIChecklistID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal IComplianceID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer,
                                         ByVal iSFID As Integer, ByVal iPID As Integer, ByVal iSPID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CRCD_PKID as ChecklistID From Compliance_Checklist left Join Compliance_Checklist_Mas On CRCM_ID=CRCD_MasID And CRCM_CustID=" & iCustID & " And CRCM_CompID=" & iACID & ""
            sSql = sSql & " Where CRCD_CompID =" & iACID & " And CRCM_JobID =" & IComplianceID & " And CRCM_FunID=" & iFunID & " And CRCM_Status ='Submitted' And CRCD_SubFunID=" & iSFID & ""
            sSql = sSql & " And CRCD_PID=" & iPID & " And CRCD_SubPID=" & iSPID & " And CRCD_RiskID=" & iRiskID & " And CRCD_ControlID=" & iControlID & " And CRCD_CheckID=" & iChecksID & ""
            sSql = sSql & " And CRCD_CertID=2 And CRCD_YearID=" & iYearID & " order by CRCD_PkID"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndGetSubTaskIdByTask(ByVal sAC As String, ByVal iACID As Integer, ByVal iTaskID As Integer, ByVal sName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select AM_ID from AuditAssignmentSubTask_Master where AM_AuditAssignmentID=" & iTaskID & " And Upper(AM_Name)=Upper('" & sName & "') and AM_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndGetCustIdByCustName(ByVal sAC As String, ByVal iACID As Integer, ByVal sName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Cust_Id from SAD_CUSTOMER_MASTER where Upper(Cust_Name)=Upper('" & sName & "') and Cust_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class



