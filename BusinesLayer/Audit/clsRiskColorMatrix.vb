Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsRiskColorMatrix
    Private objDBL As New DatabaseLayer.DBHelper
    'To Display in Color Code, Access Code, Access Key
    Public Function LoadRiskColorCodeAndAccessKey(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            If sType = "RRS" Then
                sSql = "Select RAM_Name + ' (' + RAM_Remarks + ')' As RAM_Name,RAM_Score,TC_Color_Name,TC_KeyCode,TC_AccessCode From Risk_GeneralMaster,SAD_Color_Master Where RAM_Category='" & sType & "' And RAM_YearID=" & iYearID & " And RAM_DelFlag='A' And RAM_CompID=" & iACID & " And RAM_Color=TC_Color_Name And TC_CompID=" & iACID & " Order by RAM_Score"
            ElseIf sType = "GRS" Then
                sSql = "Select RAM_Name,RAM_Score,TC_Color_Name,TC_KeyCode,TC_AccessCode From Risk_GeneralMaster,SAD_Color_Master Where RAM_Category='" & sType & "' And RAM_YearID=" & iYearID & " And RAM_DelFlag='A'  And RAM_CompID=" & iACID & " And RAM_Color=TC_Color_Name And TC_CompID=" & iACID & " Order by RAM_Score"
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'To display Y-Axis And X-Axis Vaule
    Public Function GetXAxisYAxisCount(ByVal sAC As String, ByVal iACID As Integer, ByVal sCategory As String, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*) from Risk_GeneralMaster where RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_DelFlag='A' And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetXAxisYAxisValue(ByVal sAC As String, ByVal iACID As Integer, sCategory As String, ByVal iYearID As Integer, ByVal sOrderByDesc As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_Name From Risk_GeneralMaster Where RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_DelFlag='A' And RAM_CompID=" & iACID & " Order by RAM_Score"
            If sOrderByDesc = "YES" Then
                sSql = sSql & " Desc"
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Save/Update Risk Colors
    Public Sub SaveOrUpdateRiskColors(ByVal sAC As String, ByVal iACID As Integer, ByVal iRowID As Integer, ByVal iColumnID As Integer, ByVal sColor As String, ByVal iUserID As Integer, ByVal sCategory As String, ByVal sIPaddress As String)
        Dim sSql As String
        Try
            If objDBL.SQLCheckForRecord(sAC, "Select * from MST_Risk_Color_Matrix where RCM_RowID = " & iRowID & " and RCM_ColumnID = " & iColumnID & " And RCM_Category='" & sCategory & "' and RCM_CompID = " & iACID & "") = False Then
                sSql = "Insert Into MST_Risk_Color_Matrix(RCM_RowID, RCM_ColumnID, RCM_Category, RCM_ColorsName, RCM_CreatedBy, RCM_CreatedOn, RCM_IPAddress, RCM_CompID)Values(" & iRowID & "," & iColumnID & ",'" & sCategory & "','" & sColor & "'," & iUserID & ",GetDate(),'" & sIPaddress & "'," & iACID & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            Else
                sSql = "Update MST_Risk_Color_Matrix set RCM_ColorsName='" & sColor & "',RCM_UpdatedBy=" & iUserID & ",RCM_UpdatedOn=GetDate(),RCM_IPAddress='" & sIPaddress & "' where RCM_RowID=" & iRowID & " And RCM_ColumnID=" & iColumnID & " And RCM_Category='" & sCategory & "' And RCM_CompID=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'To load selected color
    Public Function LoadExistingRiskColor(ByVal sAC As String, ByVal iACID As Integer, ByVal sCategory As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from MST_Risk_Color_Matrix where RCM_Category='" & sCategory & "' And RCM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskColorMatrixID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer, ByVal iYearID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select CM_ID from MST_Risk_Color_Matrix Where RCM_CompID=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImpactLikelihoodAssignCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iImpact As Integer, ByVal iLikelihood As Integer, ByVal iFormID As Integer) As Integer
        Dim sSql As String = ""
        Try
            If iFormID = 1 Then
                sSql = "Select Count(*) As ILValue From Risk_RCSA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=RCSAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=RCSAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where RCSAD_RCSAPKID=" & iPKID & " And RCSAD_CompID=" & iACID & ""
            ElseIf iFormID = 2 Then
                sSql = "Select Count(*) As ILValue From Risk_RA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=RAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=RAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where RAD_RAPKID=" & iPKID & " And RAD_CompID=" & iACID & ""
            ElseIf iFormID = 3 Then
                sSql = "Select Count(*) As ILValue From Audit_ARA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=ARAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=ARAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where ARAD_ARAPKID=" & iPKID & " And ARAD_CompID=" & iACID & ""
            ElseIf iFormID = 4 Then
                sSql = "Select Count(*) As ILValue From Compliance_CRCM_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=CRCMD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=CRCMD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where CRCMD_CRCMPKID=" & iPKID & " And CRCMD_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImpactLikelihoodAssign(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iImpact As Integer, ByVal iLikelihood As Integer, ByVal iFormID As Integer) As String
        Dim sSql As String = "", sID As String = ""
        Dim dt As New DataTable
        Dim i As Integer = 0
        Try
            If iFormID = 1 Then
                sSql = "Select RCSAD_PKID From Risk_RCSA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=RCSAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=RCSAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where RCSAD_RCSAPKID=" & iPKID & " And RCSAD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("RCSAD_PKID")
                Next
            ElseIf iFormID = 2 Then
                sSql = "Select RAD_PKID From Risk_RA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=RCSAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=RCSAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where RCSAD_RCSAPKID=" & iPKID & " And RCSAD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("RAD_PKID")
                Next
            ElseIf iFormID = 3 Then
                sSql = "Select ARAD_PKID From Audit_ARA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=ARAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=ARAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where ARAD_ARAPKID=" & iPKID & " And ARAD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("ARAD_PKID")
                Next
            ElseIf iFormID = 4 Then
                sSql = "Select CRCMD_PKID From Compliance_CRCM_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=CRCMD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=CRCMD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where CRCMD_CRCMPKID=" & iPKID & " And CRCMD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("CRCMD_PKID")
                Next
            End If
            If sID.StartsWith(",") = True Then
                sID = sID.Remove(0, 1)
            End If
            If sID.EndsWith(",") = True Then
                sID = sID.Remove(Len(sID) - 1, 1)
            End If
            Return sID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskControlAssignCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iRisk As Integer, ByVal iControl As Integer, ByVal iFormID As Integer) As Integer
        Dim sSql As String = ""
        Try
            If iFormID = 1 Then
                sSql = "Select Count(*) From Risk_RCSA_Details where RCSAD_RCSAPKID=" & iPKID & " And RCSAD_RiskRating=" & iRisk & " And RCSAD_ControlRating=" & iControl & " And RCSAD_CompID=" & iACID & ""
            ElseIf iFormID = 2 Then
                sSql = "Select Count(*) From Risk_RA_Details where RAD_RAPKID=" & iPKID & " And RAD_RiskRating=" & iRisk & " And RAD_ControlRating=" & iControl & " And RAD_CompID=" & iACID & ""
            ElseIf iFormID = 3 Then
                sSql = "Select Count(*) From Audit_ARA_Details where ARAD_ARAPKID=" & iPKID & " And ARAD_RiskRating=" & iRisk & " And ARAD_ControlRating=" & iControl & " And ARAD_CompID=" & iACID & ""
            ElseIf iFormID = 4 Then
                sSql = "Select Count(*) From Compliance_CRCM_Details where CRCMD_CRCMPKID=" & iPKID & " And CRCMD_RiskRating=" & iRisk & " And CRCMD_ControlRating=" & iControl & " And CRCMD_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskControlAssign(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iRisk As Integer, ByVal iControl As Integer, ByVal iFormID As Integer) As String
        Dim sSql As String = "", sID As String = ""
        Dim dt As New DataTable
        Dim i As Integer = 0
        Try
            If iFormID = 1 Then
                sSql = "Select RCSAD_PKID From Risk_RCSA_Details where RCSAD_RCSAPKID=" & iPKID & " And RCSAD_RiskRating=" & iRisk & " And RCSAD_ControlRating=" & iControl & " And RCSAD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("RCSAD_PKID")
                Next
            ElseIf iFormID = 2 Then
                sSql = "Select RAD_PKID From Risk_RA_Details where RAD_RAPKID=" & iPKID & " And RAD_RiskRating=" & iRisk & " And RAD_ControlRating=" & iControl & " And RAD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("RAD_PKID")
                Next
            ElseIf iFormID = 3 Then
                sSql = "Select ARAD_PKID From Audit_ARA_Details where ARAD_ARAPKID=" & iPKID & " And ARAD_RiskRating=" & iRisk & " And ARAD_ControlRating=" & iControl & " And ARAD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("ARAD_PKID")
                Next
            ElseIf iFormID = 4 Then
                sSql = "Select CRCMD_PKID From Compliance_CRCM_Details where CRCMD_CRCMPKID=" & iPKID & " And CRCMD_RiskRating=" & iRisk & " And CRCMD_ControlRating=" & iControl & " And CRCMD_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sID = sID & "," & dt.Rows(i).Item("CRCMD_PKID")
                Next
            End If
            If sID.StartsWith(",") = True Then
                sID = sID.Remove(0, 1)
            End If
            If sID.EndsWith(",") = True Then
                sID = sID.Remove(Len(sID) - 1, 1)
            End If
            Return sID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceImpactLikelihoodAssignCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iImpact As Integer, ByVal iLikelihood As Integer, ByVal iFormID As Integer) As Integer
        Dim sSql As String = ""
        Try
            If iFormID = 1 Then
                sSql = "Select Count(*) As ILValue From Compliance_CRSA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=CRSAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=CRSAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where CRSAD_CRSAPKID=" & iPKID & " And CRSAD_CompID=" & iACID & ""
            Else
                sSql = "Select Count(*) As ILValue From Compliance_CRA_Details "
                sSql = sSql & " Join Risk_GeneralMaster a On a.RAM_Category='RI' And a.RAM_PKID=CRAD_ImpactID And a.RAM_Score=" & iImpact & " And a.RAM_CompID=" & iACID & ""
                sSql = sSql & " Join Risk_GeneralMaster b On b.RAM_Category='RL' And b.RAM_PKID=CRAD_LikelihoodID And b.RAM_Score=" & iLikelihood & " And b.RAM_CompID=" & iACID & ""
                sSql = sSql & " Where CRAD_CRAPKID=" & iPKID & " And CRAD_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceControlAssignCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal iRisk As Integer, ByVal iControl As Integer, ByVal iFormID As Integer) As Integer
        Dim sSql As String = ""
        Try
            If iFormID = 1 Then
                sSql = "Select Count(*) From Compliance_CRSA_Details where CRSAD_CRSAPKID=" & iPKID & " And CRSAD_RiskRating=" & iRisk & " And CRSAD_ControlRating=" & iControl & " And CRSAD_CompID=" & iACID & ""
            Else
                sSql = "Select Count(*) From Compliance_CRA_Details where CRAD_CRAPKID=" & iPKID & " And CRAD_RiskRating=" & iRisk & " And CRAD_ControlRating=" & iControl & " And CRAD_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
