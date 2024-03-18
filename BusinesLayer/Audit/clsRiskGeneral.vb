Imports System.Data
Imports DatabaseLayer
Public Class clsRiskGeneral
    Private objDBL As New DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadImpactLikelihoodOESDES(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCategory As String, ByVal sCheckCategory As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name,RAM_Category from Risk_GeneralMaster Where RAM_DelFlag='A' And RAM_YearID=" & iYearID & " And RAM_CompID=" & iACID & " "
            If sCheckCategory = "YES" Then
                sSql = sSql & " And RAM_Category='" & sCategory & "'"
            End If
            sSql = sSql & " Order by RAM_Score"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImpactLikelihoodOESDESScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCategory As String, ByVal iPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RAM_Score from Risk_GeneralMaster Where RAM_PKID=" & iPKID & " And RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskScoreFromMatrix(ByVal sAC As String, ByVal iACID As Integer, ByVal iImpact As Integer, ByVal iLiklihood As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GG_RiskScore from GRACe_GrossRiskScore where GG_Impact=" & iImpact & " And GG_Likelihood=" & iLiklihood & " And GG_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlScoreFromMatrix(ByVal sAC As String, ByVal iACID As Integer, ByVal iDES As Integer, ByVal iOES As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GG_ControlScore from GRACe_GrossControlScore where GG_DE=" & iDES & " And GG_OE=" & iOES & " And GG_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskControlScoreFromMatrix(ByVal sAC As String, ByVal iACID As Integer, ByVal iRisk As Integer, ByVal iControl As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GG_RiskControlScore from GRACe_RiskControlMatrix where GG_Risk=" & iRisk & " And GG_Controls=" & iControl & " And GG_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNameColorFromScoreRiskMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iScore As Integer, ByVal sCategory As String, ByVal sType As String) As String
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "Select"
            If sType = "Name" Then
                sSql = sSql & " RAM_Name "
            ElseIf sType = "Color" Then
                sSql = sSql & " RAM_Color "
            ElseIf sType = "NameScore" Then
                sSql = sSql & " (RAM_Name + ' - ' + Convert(Varchar(10),RAM_Score)) As RAM_Name "
            End If
            sSql = sSql & " From Risk_GeneralMaster Where RAM_DelFlag='A' And RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_Score=" & iScore & " And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRRNameColorFromRangeRiskMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCategory As String, ByVal dValueID As Double, ByVal sType As String) As String
        Dim sSql As String
        Try
            sSql = "Select"
            If sType = "Desc" Then
                sSql = sSql & " RAm_Remarks "
            ElseIf sType = "Name" Then
                sSql = sSql & " RAM_Name "
            ElseIf sType = "Color" Then
                sSql = sSql & " RAM_Color "
            ElseIf sType = "ID" Then
                sSql = sSql & " RAM_PKID"
            End If
            sSql = sSql & " From Risk_GeneralMaster Where RAM_DelFlag='A' And RAM_StartValue<=round(" & dValueID & ",1) AND RAM_EndValue>=round(" & dValueID & ",1) And RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskTypeIDFromRiskID(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select MRL_RiskTypeID from MST_RISK_Library where MRL_PKID=" & iRiskID & " and MRL_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Get Risk DetailID
    Public Function GetRiskDetailID(ByVal sAC As String, ByVal iACID As Integer, ByVal sRiskDetails As String) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select MRL_PKID from MST_RISK_Library where MRL_CompID=" & iACID & " And MRL_RiskName='" & sRiskDetails & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Get ControlID
    Public Function GetControlID(ByVal sAC As String, ByVal iACID As Integer, ByVal sControl As String) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select MCL_PKID from MST_CONTROL_Library where MCL_CompID=" & iACID & " And MCL_ControlName='" & sControl & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Get RiskTypeID
    Public Function GetRiskTypesID(ByVal sAC As String, ByVal iACID As Integer, ByVal sRiskType As String) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select RAM_PKID from Risk_GeneralMaster where RAM_CompID=" & iACID & " And RAM_Name='" & sRiskType & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRiskLibrary(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MRL_PKID,MRL_RiskName From MST_RISK_Library Where MRL_CompID=" & iACID & " And MRL_DelFlag='A'  order by MRL_RiskName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadControlLibrary(ByVal sAC As String, ByVal iACID As Integer, ByVal sCheckIsKey As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select MCL_PKID,MCL_ControlName From MST_Control_Library Where MCl_CompID=" & iACID & " And MCL_DelFlag='A' "
            If sCheckIsKey = "YES" Then
                sSql = sSql & " And MCL_IsKey=1 "
            End If
            sSql = sSql & " order by MCL_ControlName"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskTypeNameFromRiskID(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select RAM_Name From Risk_GeneralMaster Where RAM_Category='RT' And RAM_CompID=" & iACID & " And RAM_PKID in (Select MRL_RiskTypeID From MST_RISK_Library Where MRL_CompID=" & iACID & " and MRL_PKID=" & iRiskID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskTypeName(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As String) As String
        Dim sSql As String
        Try
            sSql = "Select RAM_Name From Risk_GeneralMaster Where RAM_PKID=(Select MRL_RiskTypeID from MST_RISK_Library where MRL_PKID=" & iRiskID & " and MRL_CompID='" & iACID & "')"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetBRRModuleResidualRiskRating(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer, ByVal iYearID As Integer) As Object
    '    Dim sSql As String
    '    Try
    '        sSql = "Select Case When Sum(BRRD_WeightedRiskScore) Is NULL then '-1' else Sum(BRRD_WeightedRiskScore) End As Score from Risk_BRRChecklist_Details where BRRD_BRRPKID In"
    '        sSql = sSql & "(Select BRR_PKId From Risk_BRRChecklist_Mas Where BRR_BranchId=" & iBranchID & " And BRR_YearID=" & iYearID & " And BRR_CompID=" & iACID & ") And BRRD_CompID = " & iACID & ""
    '        Return objDBL.SQLExecuteScalar(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function GetBIAModuleResidualRiskRating(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer, ByVal iYearID As Integer) As Object
    '    Dim sSql As String
    '    Try
    '        sSql = "Select Case When Sum(BIACD_WeightedRiskScore) Is NULL then '-1' else Sum(BIACD_WeightedRiskScore) End As Score from Audit_BIAChecklist_Details where BIACD_BIACPKID In"
    '        sSql = sSql & "(Select BIAC_PKID From Audit_BIAChecklist_MAs Where BIAC_BranchId=" & iBranchID & " And BIAC_YearID=" & iYearID & " And BIAC_CompID=" & iACID & ") And BIACD_CompID = " & iACID & ""
    '        Return objDBL.SQLExecuteScalar(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function GetBCMModuleResidualRiskRating(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer, ByVal iYearID As Integer) As Object
    '    Dim sSql As String
    '    Try
    '        sSql = "Select Case When Sum(CMD_WeightedRiskScore) Is NULL Then '-1' else Sum(CMD_WeightedRiskScore) End As Score from CMAChecksReport where CMD_CMAMID in "
    '        sSql = sSql & " (Select CMAM_ID From CMAChecksReport_Mas Left Join CMA_Assignment_Details On CAD_ID=CMAM_AsgID And CAD_CompID=" & iACID & ""
    '        sSql = sSql & " Left Join CMA_vendor_Assignment On CVA_BranchID=CMAM_BranchID And CVA_AsgnID=CMAM_AsgID And CVA_CompID=" & iACID & ""
    '        sSql = sSql & " Where CMAM_BranchID=" & iBranchID & " And CVA_BranchID=" & iBranchID & " And CAD_AuditYear=" & iYearID & " And CMAM_CompID=" & iACID & ")"
    '        sSql = sSql & " And CMD_CompID=" & iACID & ""
    '        Return objDBL.SQLExecuteScalar(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
End Class
