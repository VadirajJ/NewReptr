Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure strAudit_AnnualPlan
    Private AAP_PKID As Integer
    Private AAP_YearID As Integer
    Private AAP_MonthID As Integer
    Private AAP_CustID As Integer
    Private AAP_FunID As Integer
    Private AAP_ResourceID As String
    Private AAP_Crby As Integer
    Private AAP_Updatedby As Integer
    Private AAP_IPAddress As String
    Private AAP_CompID As Integer
    Private AAP_Comments As String

    Public Property sAAP_Comments() As String
        Get
            Return (AAP_Comments)
        End Get
        Set(ByVal Value As String)
            AAP_Comments = Value
        End Set
    End Property

    Public Property iAAP_PKID() As Integer
        Get
            Return (AAP_PKID)
        End Get
        Set(ByVal Value As Integer)
            AAP_PKID = Value
        End Set
    End Property

    Public Property iAAP_YearID() As Integer
        Get
            Return (AAP_YearID)
        End Get
        Set(ByVal Value As Integer)
            AAP_YearID = Value
        End Set
    End Property
    Public Property iAAP_MonthID() As Integer
        Get
            Return (AAP_MonthID)
        End Get
        Set(ByVal Value As Integer)
            AAP_MonthID = Value
        End Set
    End Property

    Public Property iAAP_CustID() As Integer
        Get
            Return (AAP_CustID)
        End Get
        Set(ByVal Value As Integer)
            AAP_CustID = Value
        End Set
    End Property
    Public Property iAAP_FunID() As Integer
        Get
            Return (AAP_FunID)
        End Get
        Set(ByVal Value As Integer)
            AAP_FunID = Value
        End Set
    End Property
    Public Property SAAP_ResourceID() As String
        Get
            Return (AAP_ResourceID)
        End Get
        Set(ByVal Value As String)
            AAP_ResourceID = Value
        End Set
    End Property
    Public Property iAAP_Crby() As Integer
        Get
            Return (AAP_Crby)
        End Get
        Set(ByVal Value As Integer)
            AAP_Crby = Value
        End Set
    End Property
    Public Property iAAP_Updatedby() As Integer
        Get
            Return (AAP_Updatedby)
        End Get
        Set(ByVal Value As Integer)
            AAP_Updatedby = Value
        End Set
    End Property
    Public Property sAAP_IPAddress() As String
        Get
            Return (AAP_IPAddress)
        End Get
        Set(ByVal Value As String)
            AAP_IPAddress = Value
        End Set
    End Property
    Public Property iAAP_CompID() As Integer
        Get
            Return (AAP_CompID)
        End Get
        Set(ByVal Value As Integer)
            AAP_CompID = Value
        End Set
    End Property
End Structure
Public Class clsAnnualPlan
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetAnnualPlanPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer, ByVal iCustID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select AAP_PKID FRom Audit_AnnualPlan Where AAP_YearID=" & iYearID & " And AAP_CustID =" & iCustID & " And AAP_FunID =" & iFunID & " And AAP_CompID =" & iACID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAnnualPlanDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * FRom Audit_AnnualPlan Where AAP_YearID=" & iYearID & " And AAP_CustID =" & iCustID & " And AAP_FunID =" & iFunID & " And AAP_CompID =" & iACID & " "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPreviousYearUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String = ""
        Dim dtuser As New DataTable
        Dim sResourceID As String = "", sResource As String = ""
        Try
            sSql = "Select APM_AuditTeamsID FRom Audit_APM_Details Where APM_CompID=" & iACID & " And APM_FunctionID=" & iFunID & ""
            dtuser = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtuser.Rows.Count - 1
                If IsDBNull(dtuser.Rows(i)("APM_AuditTeamsID")) = False Then
                    sResourceID = dtuser.Rows(i)("APM_AuditTeamsID")
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

    Public Function LoadResource(ByVal sAC As String, ByVal iACID As Integer, ByVal sResourceID As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sResourceID <> "" Then
                sSql = "Select Usr_ID from sad_userdetails where Usr_CompID=" & iACID & " And Usr_ID In(" & sResourceID & ")"
                Return objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuunalPlanGridDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String, sResourceID As String = "", sUserIDs As String = "", sSql1 As String, sAuditors As String = ""
        Dim dt As New DataTable, dtTab As New DataTable, dt1 As New DataTable
        Dim i As Integer
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("CustName")
            dt.Columns.Add("Function")
            dt.Columns.Add("Month")
            dt.Columns.Add("Resource")
            dt.Columns.Add("Status")

            sSql = "Select Distinct(ASO_CustID),AAP_PKID,AAP_YearID,AAP_MonthID,AAP_CustID,AAP_FunID,AAP_ResourceID,Ent_EntityName,Cust_Name,APM_CustID,ASO_Status FRom Audit_AnnualPlan"
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=AAP_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " left Join MST_Entity_Master On ENT_ID=AAP_FunID and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_APM_Details On APM_CustID=AAP_CustID And APM_FunctionID=AAP_FunID And APM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_SignOff On ASO_CustID=AAP_CustID And ASO_FunctionID=AAP_FunID And APM_CompID=" & iACID & ""
            sSql = sSql & " Where AAP_CompID=" & iACID & " And AAP_YearID=" & iYearID & " "
            If iFunID > 0 Then
                sSql = sSql & " and AAP_FunID=" & iFunID & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " and AAP_CustID=" & iCustID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtTab.Rows(i)("Cust_Name")) = False Then
                        dRow("CustName") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("Cust_Name"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("Ent_EntityName")) = False Then
                        dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("Ent_EntityName"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("AAP_MonthID")) = False Then
                        dRow("Month") = objclsGeneralFunctions.GetMonthNameFromMothID(dtTab.Rows(i)("AAP_MonthID"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("APM_CustID")) = False Then
                        If dtTab.Rows(i)("APM_CustID") <> Nothing Then
                            dRow("Status") = "In Progess"
                        ElseIf dtTab.Rows(i)("ASO_Status") = "Submitted" Then
                            dRow("Status") = "Completed"
                        Else
                            dRow("Status") = "Not Started"
                        End If
                    Else
                        dRow("Status") = "Not Started"
                    End If
                    If IsDBNull(dtTab.Rows(i)("AAP_ResourceID")) = False Then
                        sResourceID = dtTab.Rows(i)("AAP_ResourceID")
                        If sResourceID.StartsWith(",") = True Then
                            sResourceID = sResourceID.Remove(0, 1)
                        End If
                        If sResourceID.EndsWith(",") = True Then
                            sResourceID = sResourceID.Remove(Len(sResourceID) - 1, 1)
                        End If
                        If sResourceID <> "" Then
                            sSql1 = "Select Usr_FullName From Sad_UserDetails Where Usr_ID IN (" & sResourceID & ") And Usr_CompID=" & iACID & ""
                            dt1 = objDBL.SQLExecuteDataTable(sAC, sSql1)
                        End If
                        sAuditors = ""
                        For j = 0 To dt1.Rows.Count - 1
                            sAuditors = sAuditors & ", " & dt1.Rows(j).Item("Usr_FullName")
                        Next
                        If sAuditors.StartsWith(", ") Then
                            sAuditors = sAuditors.Remove(0, 2)
                        End If
                        If sAuditors.EndsWith(", ") Then
                            sAuditors = sAuditors.Remove(Len(sAuditors) - 2, 1)
                        End If
                        dRow("Resource") = sAuditors
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetResourceAvailabiltyGridDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql1 As String = "", sUserID As String = "", sSql As String
        Dim sJanUser As String = "", sFebUser As String = "", sMarUser As String = "", sAprUser As String = "", sMayUser As String = "", sJunUser As String = ""
        Dim sJulyUser As String = "", sAugUser As String = "", sSeptUser As String = "", sOctUser As String = "", sNovUser As String = "", sDecUser As String = ""
        Dim dt As New DataTable, dtUser As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("EmpName")
            dt.Columns.Add("MonthID")
            dt.Columns.Add("CustFun")
            dt.Columns.Add("January")
            dt.Columns.Add("February")
            dt.Columns.Add("March")
            dt.Columns.Add("April")
            dt.Columns.Add("May")
            dt.Columns.Add("June")
            dt.Columns.Add("July")
            dt.Columns.Add("August")
            dt.Columns.Add("September")
            dt.Columns.Add("October")
            dt.Columns.Add("November")
            dt.Columns.Add("December")

            sSql1 = "Select Usr_ID,Usr_FullName From Sad_userDetails Where Usr_CompID=" & iACID & ""
            dtUser = objDBL.SQLExecuteDataTable(sAC, sSql1)
            For j = 0 To dtUser.Rows.Count - 1
                dRow = dt.NewRow
                dRow("SrNo") = j + 1
                If IsDBNull(dtUser.Rows(j)("Usr_ID")) = False Then
                    sUserID = dtUser.Rows(j)("Usr_ID")
                End If
                If sUserID.StartsWith(",") = True Then
                    sUserID = sUserID.Remove(0, 1)
                End If
                If sUserID.EndsWith(",") = True Then
                    sUserID = sUserID.Remove(Len(sUserID) - 1, 1)
                End If
                If IsDBNull(dtUser.Rows(j)("Usr_FullName")) = False Then
                    dRow("EmpName") = dtUser.Rows(j)("Usr_FullName")
                End If
                sJanUser = "" : sFebUser = "" : sMarUser = "" : sAprUser = "" : sMayUser = "" : sJunUser = ""
                sJulyUser = "" : sAugUser = "" : sSeptUser = "" : sOctUser = "" : sNovUser = "" : sDecUser = ""

                sSql = "Select AAP_MonthID,Ent_EntityName,Cust_Name FRom Audit_AnnualPlan "
                sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=AAP_CustID And Cust_CompID=" & iACID & ""
                sSql = sSql & " left Join MST_Entity_Master On ENT_ID=AAP_FunID and ENT_CompID=" & iACID & ""
                sSql = sSql & " Where AAP_CompID=" & iACID & " And AAP_YearID=" & iYearID & "And AAP_ResourceID Like '%" & "," & sUserID & "," & "%'"
                dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtTab.Rows.Count - 1
                    If IsDBNull(dtTab.Rows(i)("AAP_MonthID")) = False Then
                        dRow("MonthID") = dtTab.Rows(i)("AAP_MonthID")
                    End If
                    dRow("CustFun") = ""
                    If IsDBNull(dRow("MonthID")) = False Then
                        If dRow("MonthID") = 1 Then
                            dRow("January") = 1
                            If IsDBNull(dtTab.Rows(i)("Cust_Name")) = False Then
                                dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                                sJanUser = sJanUser & "," & dRow("CustFun")
                                If sJanUser.StartsWith(",") Then
                                    sJanUser = sJanUser.Remove(0, 2)
                                End If
                                dRow("CustFun") = sJanUser
                            End If
                        ElseIf dRow("MonthID") = 2 Then
                            dRow("February") = 2
                            dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                            sFebUser = sFebUser & "," & dRow("CustFun")
                            If sFebUser.StartsWith(",") Then
                                sFebUser = sFebUser.Remove(0, 2)
                            End If
                            dRow("CustFun") = sFebUser
                        ElseIf dRow("MonthID") = 3 Then
                            dRow("March") = 3
                            dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                            sMarUser = sMarUser & "," & dRow("CustFun")
                            If sMarUser.StartsWith(",") Then
                                sMarUser = sMarUser.Remove(0, 2)
                            End If
                            dRow("CustFun") = sMarUser
                        ElseIf dRow("MonthID") = 4 Then
                            dRow("April") = 4
                            If IsDBNull(dtTab.Rows(i)("Cust_Name")) = False Then
                                dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                                sAprUser = sAprUser & "," & dRow("CustFun")
                                If sAprUser.StartsWith(",") Then
                                    sAprUser = sAprUser.Remove(0, 2)
                                End If
                                dRow("CustFun") = sAprUser
                            End If
                        ElseIf dRow("MonthID") = 5 Then
                            dRow("May") = 5
                            If IsDBNull(dtTab.Rows(i)("Cust_Name")) = False Then
                                dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                                sMayUser = sMayUser & "," & dRow("CustFun")
                                If sMayUser.StartsWith(",") Then
                                    sMayUser = sMayUser.Remove(0, 2)
                                End If
                                dRow("CustFun") = sMayUser
                            End If
                        ElseIf dRow("MonthID") = 6 Then
                            dRow("June") = 6
                            If IsDBNull(dtTab.Rows(i)("Cust_Name")) = False Then
                                sJunUser = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                                sJunUser = sJunUser & "," & dRow("CustFun")
                                If sJunUser.StartsWith(",") Then
                                    sJunUser = sJunUser.Remove(0, 2)
                                End If
                                dRow("CustFun") = sJunUser
                            End If
                        ElseIf dRow("MonthID") = 7 Then
                            dRow("July") = 7
                            dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                            sJulyUser = sJulyUser & "," & dRow("CustFun")
                            If sJulyUser.StartsWith(",") Then
                                sJulyUser = sJulyUser.Remove(0, 2)
                            End If
                            dRow("CustFun") = sJulyUser
                        ElseIf dRow("MonthID") = 8 Then
                            dRow("August") = 8
                            dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                            sAugUser = sAugUser & "," & dRow("CustFun")
                            If sAugUser.StartsWith(",") Then
                                sAugUser = sAugUser.Remove(0, 2)
                            End If
                            dRow("CustFun") = sAugUser
                        ElseIf dRow("MonthID") = 9 Then
                            dRow("September") = 9
                            dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                            sSeptUser = sSeptUser & "," & dRow("CustFun")
                            If sAugUser.StartsWith(",") Then
                                sAugUser = sAugUser.Remove(0, 2)
                            End If
                            dRow("CustFun") = sAugUser
                        ElseIf dRow("MonthID") = 10 Then
                            dRow("October") = 10
                            If IsDBNull(dtTab.Rows(i)("Cust_Name")) = False Then
                                dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                                sOctUser = sOctUser & "," & dRow("CustFun")
                                If sOctUser.StartsWith(",") Then
                                    sOctUser = sOctUser.Remove(0, 2)
                                End If
                                dRow("CustFun") = sOctUser
                            End If
                        ElseIf dRow("MonthID") = 11 Then
                            dRow("November") = 11
                            dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                            sNovUser = sNovUser & "," & dRow("CustFun")
                            If sNovUser.StartsWith(",") Then
                                sNovUser = sNovUser.Remove(0, 2)
                            End If
                            dRow("CustFun") = sNovUser
                        ElseIf dRow("MonthID") = 12 Then
                            dRow("December") = 12
                            dRow("CustFun") = " Customer Name : " & dtTab.Rows(i)("Cust_Name") & vbNewLine & "Function : " & dtTab.Rows(i)("Ent_EntityName") & vbNewLine
                            sDecUser = sDecUser & "," & dRow("CustFun")
                            If sDecUser.StartsWith(",") Then
                                sDecUser = sDecUser.Remove(0, 2)
                            End If
                            dRow("CustFun") = sDecUser
                        End If
                    End If
                Next
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAnnualPlanDetails(ByVal sAC As String, ByVal objAnnualPlan As strAudit_AnnualPlan)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_MonthID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_MonthID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_FunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_ResourceID", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAnnualPlan.SAAP_ResourceID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_Comments", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objAnnualPlan.sAAP_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_Crby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_Crby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_Updatedby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_Updatedby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAnnualPlan.sAAP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AAP_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAnnualPlan.iAAP_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_AnnualPlan", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
