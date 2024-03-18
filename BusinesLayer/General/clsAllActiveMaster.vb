Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsAllActiveMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function LoadActiveEmployeesUsers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails where Usr_CompID=" & iACID & " And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') order by FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveEmployees(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails where Usr_CompID=" & iACID & " And Usr_Node>0 And Usr_OrgnID>0 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') order by FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVendorOrAgencyWiseUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iAgencyID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_Id,Usr_FullName from sad_userdetails where Usr_CompanyID=" & iAgencyID & " and usr_CompID=" & iACID & " And Usr_Node=0 And usr_OrgnId=0"
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
    Public Function LoadActiveUsers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails where Usr_CompID=" & iACID & " And Usr_Node=0 And Usr_OrgnID=0 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') order by FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where CUST_DelFlg = 'A' and cust_Compid=" & iACID & " order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActivePartners(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,USr_FullName from sad_userdetails where usr_compID=" & iACID & " And USR_Partner=1 And (usr_DelFlag='A' or usr_DelFlag='B' or usr_DelFlag='L') order by USr_FullName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mas_ID,Mas_Description from SAD_CUST_LOCATION where Mas_Delflag='A' And Mas_CustID =" & iCustID & " order by Mas_Description"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerWiseUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iAgencyID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_Id,Usr_FullName from sad_userdetails where Usr_CompanyID=" & iAgencyID & " and usr_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerWiseUsersWithEmail(ByVal sAC As String, ByVal iACID As Integer, ByVal iAgencyID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_Id,Usr_FullName from sad_userdetails where Usr_CompanyID=" & iAgencyID & " and usr_CompID=" & iACID & " And  Usr_Email like '%@%' And Usr_Email like '%.%' "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveRole(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mas_ID,Mas_Description from SAD_GrpOrLvl_General_Master where Mas_Delflag='A' and Mas_CompID=" & iACID & " order by Mas_Description"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveDesignation(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID,Mas_Description from SAD_GRPDESGN_General_Master where Mas_Delflag='A' and Mas_CompID=" & iACID & " order by Mas_Description"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveInherentMasters(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MIM_ID,MIM_Name,MIM_FromScore,MIM_ToScore,MIM_Frequency from MST_InherentRisk_Master where MIM_CompID=" & iACID & " order by MIM_ID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iFUNUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And Ent_DelFlg='A'"
            If iFUNUserID > 0 Then
                sSql = sSql & " And (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ")"
            End If
            sSql = sSql & " Order by Ent_Entityname"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveSubFunctions(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SEM_ID,SEM_NAME,SEM_Ent_ID from MST_SUBENTITY_MASTER where SEM_COMPID=" & iACID & " And SEM_DelFlg='A'"
            If iFunID > 0 Then
                sSql = sSql & " And SEM_ENT_ID=" & iFunID & ""
            End If
            sSql = sSql & "order by SEM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select PM_ID, PM_NAME from MST_PROCESS_MASTER where PM_COMPID=" & iACID & " And PM_DelFlg='A'"
            If iSubFunID > 0 Then
                sSql = sSql & " and PM_SEM_ID=" & iSubFunID & ""
            End If
            sSql = sSql & " order by PM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveSubProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iProcessId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SPM_ID, SPM_NAME from MST_SUBPROCESS_MASTER where SPM_COMPID=" & iACID & " And SPM_DelFlg='A'"
            If iProcessId > 0 Then
                sSql = sSql & " And SPM_PM_ID=" & iProcessId & " "
            End If
            sSql = sSql & " Order by SPM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveRisks(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MRL_PKID,MRL_RiskName from MST_RISK_Library where MRL_CompID=" & iACID & " And MRL_DelFlag='A'"
            sSql = sSql & " order by MRL_RiskName Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveControls(ByVal sAc As String, ByVal iAcID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MCL_PKID,MCL_ControlName from MST_CONTROL_Library where MCL_CompID=" & iAcID & " And MCL_DelFlag='A'"
            sSql = sSql & " order by MCL_ControlName"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveImpactLikelihoodOESDESWithOutScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCategory As String, ByVal sCheckCategory As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_Name,RAM_Remarks,RAM_PKID,RAM_Score,RAM_Category,RAM_Color from Risk_GeneralMaster Where RAM_DelFlag='A' And RAM_YearID=" & iYearID & " And RAM_CompID=" & iACID & " "
            If sCheckCategory = "YES" Then
                sSql = sSql & " And RAM_Category='" & sCategory & "'"
            End If
            sSql = sSql & " Order by RAM_Score"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveVendorOrAgency(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Cust_ID,Cust_Name from SAD_CUSTOMER_MASTER where Cust_CompID=" & iACID & " and Cust_Status='A' order by Cust_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOverAllRiskRatingNameColor(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal dValue As Double, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "Name" Then
                sSql = "Select  Case When CMAR_Desc Is NULL Then '' else CMAR_Desc End As CMAR_Desc from CMARating where "
            ElseIf sType = "Color" Then
                sSql = "Select  Case When CMAR_Color Is NULL Then '' else CMAR_Color End As CMAR_Color from CMARating where "
            ElseIf sType = "ID" Then
                sSql = "Select  Case When CMAR_ID Is NULL Then '0' else CMAR_ID End As CMAR_ID from CMARating where "
            End If
            If dValue > 0 Then
                sSql = sSql & " (cmar_startvalue <= '" & dValue & "' And cmar_endvalue > '" & dValue & "') And "
            End If
            sSql = sSql & " CMAR_Flag='A' and CMAR_Yearid=" & iYearID & " And CMAR_Compid=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCoreAndSupportProcessRating(ByVal sAC As String, ByVal iACID As Integer, ByVal dValue As Double, iYearID As Integer, ByVal sFunType As String) As String
        Dim sSql As String = ""
        Try
            If sFunType = "C" Then
                sSql = "Select Case When cmacr_desc Is NULL then '' else cmacr_desc End As cmacr_desc from CMARating_CoreProcess where (cmacr_startvalue <= '" & dValue & "' And cmacr_endvalue > '" & dValue & "') And"
                sSql = sSql & " CMACR_Flag='A' And CMACR_YearId=" & iYearID & " And CMACR_CompId=" & iACID & ""
            ElseIf sFunType = "CPID" Then
                sSql = "Select Case When CMACR_ID Is NULL then '0' else CMACR_ID End As CMACR_ID from CMARating_CoreProcess where (cmacr_startvalue <= '" & dValue & "' And cmacr_endvalue > '" & dValue & "') And"
                sSql = sSql & " CMACR_Flag='A' And CMACR_YearId=" & iYearID & " And CMACR_CompId=" & iACID & ""
            ElseIf sFunType = "S" Then
                sSql = "Select Case When cmasr_desc Is NULL Then '' else cmasr_desc End As cmasr_desc from CMARating_SupportProcess where (cmasr_startvalue <= '" & dValue & "' And cmasr_endvalue > '" & dValue & "') And"
                sSql = sSql & " CMASR_Flag='A' And CMASR_YearId=" & iYearID & " And CMASR_CompId=" & iACID & ""
            ElseIf sFunType = "SID" Then
                sSql = "Select Case When CMASR_ID Is NULL Then '0' else CMASR_ID End As CMASR_ID from CMARating_SupportProcess where (cmasr_startvalue <= '" & dValue & "' And cmasr_endvalue > '" & dValue & "') And"
                sSql = sSql & " CMASR_Flag='A' And CMASR_YearId=" & iYearID & " And CMASR_CompId=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInherentColor(ByVal sAC As String, ByVal iACID As Integer, ByVal sRiskName As String) As String
        Dim sSql As String
        Try
            sSql = "Select MIM_Color from MST_InherentRisk_Master where MIM_CompID=" & iACID & " And Upper(MIM_Name)='" & sRiskName & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCoreAndSupportProcessRatingColor(ByVal sAC As String, ByVal iACID As Integer, ByVal dValue As Double, iYearID As Integer, ByVal sFunType As String) As String
        Dim sSql As String = ""
        Try
            If sFunType = "C" Then
                sSql = "Select Case When CMACR_Color Is NULL then '' else CMACR_Color End As CMACR_Color from CMARating_CoreProcess where (cmacr_startvalue <= '" & dValue & "' And cmacr_endvalue > '" & dValue & "') And"
                sSql = sSql & " CMACR_Flag='A' And CMACR_YearId=" & iYearID & " And CMACR_CompId=" & iACID & ""
            ElseIf sFunType = "S" Then
                sSql = "Select Case When CMASR_Color Is NULL then '' else CMASR_Color End As CMASR_Color from CMARating_SupportProcess where (cmasr_startvalue <= '" & dValue & "' And cmasr_endvalue > '" & dValue & "') And"
                sSql = sSql & " CMASR_Flag='A' And CMASR_YearId=" & iYearID & " And CMASR_CompId=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveCMARatingScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMAR_DESC,CMAR_ID,CMAR_Name,CMAR_Color,CMAR_StartValue,CMAR_EndValue from CMARating Where CMAR_Flag='A' And CMAR_YearID=" & iYearID & " And CMAR_CompID=" & iACID & " "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOverAllRiskRatingColor(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select  Case When CMAR_Color Is NULL Then '' else CMAR_Color End As CMAR_Color,CMAR_Desc,CMAR_StartValue,CMAR_EndValue,CMAR_ID from CMARating where "
            sSql = sSql & " CMAR_Flag ='A' and CMAR_Yearid=" & iYearID & " And CMAR_Compid=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerName(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Cust_Name from SAD_CUSTOMER_MASTER where Cust_ID=" & iCustID & " And Cust_CompID=" & iACID & " and Cust_Status='A' order by Cust_Name"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllAuditTypeHeadings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAuditTypeID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(ACM_Heading),DENSE_RANK() OVER (ORDER BY ACM_Heading DESC) AS ACM_ID From AuditType_Checklist_Master Where ACM_AuditTypeID=" & iAuditTypeID & " And ACM_CompId=" & iCompID & " and ACM_Heading<>'' and ACM_Heading<>'NULL'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
