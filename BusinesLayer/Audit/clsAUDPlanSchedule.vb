Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsAUDPlanSchedule
    Private objDBL As New DatabaseLayer.DBHelper

    Dim AUD_ID As Integer
    Dim AUD_Code As String
    Dim AUD_YearID As Integer
    Dim AUD_MonthID As Integer
    Dim AUD_SectionID As Integer
    Dim AUD_KitchenID As Integer
    Dim AUD_AuditorID As String
    Dim AUD_FromDate As Date
    Dim AUD_ToDate As Date
    Dim AUD_Title As String
    Dim AUD_Intmail As Integer
    Dim AUD_Firstmail As Integer
    Dim AUD_SecondMail As Integer
    Dim AUD_CrBy As Integer
    Dim AUD_UpdatedBy As Integer
    Dim AUD_ApprovedBy As Integer
    Dim AUD_Status As String
    Dim AUD_CompID As Integer
    Dim AUD_Operation As String
    Dim AUD_IPAddress As String

    Public Property iAUD_ID() As Integer
        Get
            Return (AUD_ID)
        End Get
        Set(ByVal Value As Integer)
            AUD_ID = Value
        End Set
    End Property
    Public Property sAUD_Code() As String
        Get
            Return (AUD_Code)
        End Get
        Set(ByVal Value As String)
            AUD_Code = Value
        End Set
    End Property
    Public Property iAUD_YearID() As Integer
        Get
            Return (AUD_YearID)
        End Get
        Set(ByVal Value As Integer)
            AUD_YearID = Value
        End Set
    End Property
    Public Property iAUD_MonthID() As Integer
        Get
            Return (AUD_MonthID)
        End Get
        Set(ByVal Value As Integer)
            AUD_MonthID = Value
        End Set
    End Property
    Public Property iAUD_SectionID() As Integer
        Get
            Return (AUD_SectionID)
        End Get
        Set(ByVal Value As Integer)
            AUD_SectionID = Value
        End Set
    End Property
    Public Property iAUD_KitchenID() As Integer
        Get
            Return (AUD_KitchenID)
        End Get
        Set(ByVal Value As Integer)
            AUD_KitchenID = Value
        End Set
    End Property
    Public Property sAUD_AuditorID() As String
        Get
            Return (AUD_AuditorID)
        End Get
        Set(ByVal Value As String)
            AUD_AuditorID = Value
        End Set
    End Property
    Public Property sAUD_Title() As String
        Get
            Return (AUD_Title)
        End Get
        Set(ByVal Value As String)
            AUD_Title = Value
        End Set
    End Property
    Public Property dAUD_FromDate() As Date
        Get
            Return (AUD_FromDate)
        End Get
        Set(ByVal Value As Date)
            AUD_FromDate = Value
        End Set
    End Property
    Public Property dAUD_ToDate() As Date
        Get
            Return (AUD_ToDate)
        End Get
        Set(ByVal Value As Date)
            AUD_ToDate = Value
        End Set
    End Property
    Public Property iAUD_Intmail() As Integer
        Get
            Return (AUD_Intmail)
        End Get
        Set(ByVal Value As Integer)
            AUD_Intmail = Value
        End Set
    End Property
    Public Property iAUD_Firstmail() As Integer
        Get
            Return (AUD_Firstmail)
        End Get
        Set(ByVal Value As Integer)
            AUD_Firstmail = Value
        End Set
    End Property
    Public Property iAUD_SecondMail() As Integer
        Get
            Return (AUD_SecondMail)
        End Get
        Set(ByVal Value As Integer)
            AUD_SecondMail = Value
        End Set
    End Property
    Public Property iAUD_CrBy() As Integer
        Get
            Return (AUD_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AUD_CrBy = Value
        End Set
    End Property
    Public Property iAUD_UpdatedBy() As Integer
        Get
            Return (AUD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            AUD_UpdatedBy = Value
        End Set
    End Property
    Public Property iAUD_ApprovedBy() As Integer
        Get
            Return (AUD_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            AUD_ApprovedBy = Value
        End Set
    End Property
    Public Property sAUD_Status() As String
        Get
            Return (AUD_Status)
        End Get
        Set(ByVal Value As String)
            AUD_Status = Value
        End Set
    End Property
    Public Property iAUD_CompID() As Integer
        Get
            Return (AUD_CompID)
        End Get
        Set(ByVal Value As Integer)
            AUD_CompID = Value
        End Set
    End Property
    Public Property sAUD_Operation() As String
        Get
            Return (AUD_Operation)
        End Get
        Set(ByVal Value As String)
            AUD_Operation = Value
        End Set
    End Property
    Public Property sAUD_IPAddress() As String
        Get
            Return (AUD_IPAddress)
        End Get
        Set(ByVal Value As String)
            AUD_IPAddress = Value
        End Set
    End Property
    Public Function LoadAllAudit(ByVal sAC As String, ByVal iAcID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select CAS_ID, CAS_SectionName from crpa_section where CAS_Delflg='A' and CAS_ID=5 and cas_compid=" & iAcID & " order by CAS_ID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForScheduledData(ByVal sAC As String, ByVal iAcID As Integer, ByVal iAuditId As Integer, ByVal iCustID As Integer, ByVal iMonthid As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Dim iRet As New Integer
        Try
            sSql = "select AUD_ID from audit_schedule where AUD_SectionID=" & iAuditId & " and AUD_KitchenID=" & iCustID & " and AUD_MonthID=" & iMonthid & "  and AUD_YearID=" & iYearID & " and AUD_CompID=" & iAcID & ""
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllKitchens(ByVal sAC As String, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select org_node, org_name from sad_org_structure where org_levelCode=4 and org_delflag = 'A'"
            If sSearch <> "" Then
                sSql = sSql & " And (org_name Like '%" & sSearch & "%')"
            End If
            sSql = sSql & " Order by org_name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllAuditors(ByVal sAC As String, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select usr_id, usr_fullname from Sad_UserDetails where usr_designation=21"
            If sSearch <> "" Then
                sSql = sSql & " And (usr_fullname Like '%" & sSearch & "%')"
            End If
            sSql = sSql & " Order by usr_fullname"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditCode(ByVal sAC As String, ByVal iAudID As Integer, ByVal sYearName As String, ByVal iMonthID As Integer, ByVal iYearId As Integer) As String
        Dim iMaxID As Integer
        Dim sMaxID As String = "", sJobCode As String = "", sModuleCode As String = ""
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Audit_schedule where AUD_yearID=" & iYearId & "")
            sModuleCode = "CCM"

            If iMaxID = 1 Then
                sMaxID = "001"
            ElseIf iMaxID > 0 And iMaxID < 10 Then
                sMaxID = "00" & iMaxID
            ElseIf iMaxID >= 10 And iMaxID < 100 Then
                sMaxID = "0" & iMaxID
            Else
                sMaxID = iMaxID
            End If
            sJobCode = "TRACe/" & sModuleCode & "/" & sYearName & "/" & sMaxID
            Return sJobCode

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditSchedule(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iMonthID As Integer, ByVal iSecID As Integer) As DataTable
        Dim sSql As String
        Dim i As Integer
        Dim dtTab As New DataTable, dt As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SlNo")
            dtTab.Columns.Add("AUD_ID")
            dtTab.Columns.Add("aud_code")
            dtTab.Columns.Add("Customer")
            dtTab.Columns.Add("Auditor")
            dtTab.Columns.Add("FromDate")
            dtTab.Columns.Add("ToDate")
            dtTab.Columns.Add("Status")

            sSql = "select Aud_id, aud_code, AUD_YearID, aud_monthid, AUD_SectionID, AUD_KitchenID, AUD_AuditorIDs,AUD_FromDate, AUD_ToDate, AUD_Operation, b.CUST_NAME as Customer "
            sSql = sSql & " from audit_schedule a "
            sSql = sSql & " Left Join sad_customer_master b on a.AUD_KitchenID=b.cust_id and b.cust_compid=" & iACID & ""
            sSql = sSql & " where aud_yearid=" & iYearID & " and aud_monthid=" & iMonthID & " and AUD_SectionID=" & iSecID & ""
            sSql = sSql & " Order by AUD_id Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dt.Rows.Count <> 0 Then
                For i = 0 To dt.Rows.Count - 1
                    drow = dtTab.NewRow
                    drow("AUD_ID") = dt.Rows(i).Item("AUD_ID")
                    drow("SlNo") = i + 1
                    If IsDBNull(dt.Rows(i).Item("aud_code")) = False Then
                        drow("aud_code") = dt.Rows(i).Item("aud_code")
                    End If
                    If IsDBNull(dt.Rows(i).Item("Customer")) = False Then
                        drow("Customer") = dt.Rows(i).Item("Customer")
                    End If
                    If IsDBNull(dt.Rows(i).Item("AUD_AuditorIDs")) = False Then
                        drow("Auditor") = GetAuditorsUserName(sAC, dt.Rows(i).Item("AUD_AuditorIDs"))
                    End If
                    If IsDBNull(dt.Rows(i).Item("AUD_FromDate")) = False Then
                        drow("FromDate") = dt.Rows(i).Item("AUD_FromDate").ToString.Substring(0, 10)
                    End If
                    If IsDBNull(dt.Rows(i).Item("AUD_ToDate")) = False Then
                        drow("ToDate") = dt.Rows(i).Item("AUD_ToDate").ToString.Substring(0, 10)
                    End If
                    If IsDBNull(dt.Rows(i).Item("aud_operation")) = False Then
                        drow("Status") = dt.Rows(i).Item("aud_operation")
                    End If
                    dtTab.Rows.Add(drow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditorsUserName(ByVal sAC As String, ByVal sAuditorIDs As String)
        Dim sSql As String
        Dim dt As New DataTable
        Dim sUsrID As String = "", sAllUserName As String = ""
        Try

            If sAuditorIDs <> "" Then
                sSql = "select USR_FullName from sad_userdetails where USR_id in (" & sAuditorIDs & ") and USR_dutyStatus='A'"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            For i = 0 To dt.Rows.Count - 1
                sAllUserName = sAllUserName & "," & dt.Rows(i)("Usr_FullName")
            Next
            If sAllUserName.StartsWith(",") Then
                sAllUserName = sAllUserName.Remove(0, 1)
            End If
            If sAllUserName.EndsWith(",") Then
                sAllUserName = sAllUserName.Remove(Len(sAllUserName) - 1, 1)
            End If
            If sAllUserName <> "" Then
                Return sAllUserName
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSelectedScheduleDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iMonthId As Integer, ByVal iSecID As Integer, ByVal ICustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From audit_schedule Where AUD_Monthid=" & iMonthId & " And AUD_SectionID=" & iSecID & " and AUD_Yearid=" & iYearID & " and AUD_CompID= " & iACID & ""
            If ICustID > 0 Then
                sSql = sSql & " and AUD_KitchenID= " & ICustID & " "
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveScheduleDetails(ByVal sAC As String, ByVal objSchedule As clsAUDPlanSchedule) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objSchedule.sAUD_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_YearID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_MonthID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_MonthID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_SectionID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_SectionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_KitchenID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_KitchenID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_AuditorIDs", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSchedule.sAUD_AuditorID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_FromDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSchedule.dAUD_FromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSchedule.dAUD_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_Intmail", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_Intmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_Firstmail", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_Firstmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_SecondMail", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_SecondMail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_ApprovedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objSchedule.sAUD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSchedule.iAUD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_Operation", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSchedule.sAUD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objSchedule.sAUD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AUD_Title", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSchedule.sAUD_Title
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_Schedule", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
