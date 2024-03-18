Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class ClsManualSeparateSchedule
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Private SS_PKID As Integer
    Private SS_FinancialYear As Integer
    Private SS_CustId As Integer
    Private SS_Orgtype As Integer
    Private SS_Group As Integer
    Private SS_Particulars As String
    Private SS_Values As Double
    Private SS_DATE As DateTime
    Private SS_Status As String
    Private SS_Delflag As String
    Private SS_CrBy As Integer
    Private SS_CrOn As DateTime
    Private SS_UpdatedBy As String
    Private SS_UpdatedOn As DateTime
    Private SS_Approvedby As Integer
    Private SS_ApprovedOn As DateTime
    Private SS_IPAddress As String
    Private SS_CompID As Integer
    Public Property iSS_PKID() As Integer
        Get
            Return (SS_PKID)
        End Get
        Set(ByVal Value As Integer)
            SS_PKID = Value
        End Set
    End Property
    Public Property iSS_FinancialYear() As Integer
        Get
            Return (SS_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            SS_FinancialYear = Value
        End Set
    End Property
    Public Property iSS_CustId() As Integer
        Get
            Return (SS_CustId)
        End Get
        Set(ByVal Value As Integer)
            SS_CustId = Value
        End Set
    End Property
    Public Property iSS_Orgtype() As Integer
        Get
            Return (SS_Orgtype)
        End Get
        Set(ByVal Value As Integer)
            SS_Orgtype = Value
        End Set
    End Property

    Public Property iSS_Group() As Integer
        Get
            Return (SS_Group)
        End Get
        Set(ByVal Value As Integer)
            SS_Group = Value
        End Set
    End Property

    Public Property sSS_Particulars() As String
        Get
            Return (SS_Particulars)
        End Get
        Set(ByVal Value As String)
            SS_Particulars = Value
        End Set
    End Property
    Public Property dSS_Values() As Double
        Get
            Return (SS_Values)
        End Get
        Set(ByVal Value As Double)
            SS_Values = Value
        End Set
    End Property

    Public Property dSS_DATE() As DateTime
        Get
            Return (SS_DATE)
        End Get
        Set(ByVal Value As DateTime)
            SS_DATE = Value
        End Set
    End Property
    Public Property sSS_Status() As String
        Get
            Return (SS_Status)
        End Get
        Set(ByVal Value As String)
            SS_Status = Value
        End Set
    End Property
    Public Property sSS_Delflag() As String
        Get
            Return (SS_Delflag)
        End Get
        Set(ByVal Value As String)
            SS_Delflag = Value
        End Set
    End Property
    Public Property iSS_CrBy() As Integer
        Get
            Return (SS_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SS_CrBy = Value
        End Set
    End Property
    Public Property dSS_CrOn() As DateTime
        Get
            Return (SS_CrOn)
        End Get
        Set(ByVal Value As DateTime)
            SS_CrOn = Value
        End Set
    End Property
    Public Property iSS_UpdatedBy() As Integer
        Get
            Return (SS_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SS_UpdatedBy = Value
        End Set
    End Property
    Public Property dSS_UpdatedOn() As DateTime
        Get
            Return (SS_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            SS_UpdatedOn = Value
        End Set
    End Property

    Public Property iSS_Approvedby() As Integer
        Get
            Return (SS_Approvedby)
        End Get
        Set(ByVal Value As Integer)
            SS_Approvedby = Value
        End Set
    End Property
    Public Property dSS_ApprovedOn() As DateTime
        Get
            Return (SS_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            SS_ApprovedOn = Value
        End Set
    End Property
    Public Property sSS_IPAddress() As String
        Get
            Return (SS_IPAddress)
        End Get
        Set(ByVal Value As String)
            SS_IPAddress = Value
        End Set
    End Property
    Public Property iSS_CompID() As Integer
        Get
            Return (SS_CompID)
        End Get
        Set(ByVal Value As Integer)
            SS_CompID = Value
        End Set
    End Property
    Public Function SaveManualEntriesSS(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsManualSeparateSchedule As ClsManualSeparateSchedule)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_FinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_Orgtype", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_Orgtype
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_Particulars", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.sSS_Particulars
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_Values", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.dSS_Values
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_DATE", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.dSS_DATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.sSS_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_Delflag", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.sSS_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_Approvedby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_Approvedby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.sSS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SS_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsManualSeparateSchedule.iSS_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "sp_Acc_Seperate_Schedule", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getManualDetailsSS(ByVal sSAC As String, ByVal iCompId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iOrgTypeId As Integer)
        Dim sSql As String = ""
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim dt As New DataTable
        Try
            dtDisplay.Columns.Add("PKID")
            dtDisplay.Columns.Add("Group")
            dtDisplay.Columns.Add("GroupID")
            dtDisplay.Columns.Add("Perticulars")

            dtDisplay.Columns.Add("Values")

            sSql = "select * from Acc_Seperate_Schedule where SS_CustId=" & iCustID & " and SS_FinancialYear=" & iYearId & " and SS_compid=" & iCompId & " and SS_OrgType=" & iOrgTypeId & " and SS_Status<>'D'"
            dt = objDBL.SQLExecuteDataTable(sSAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("PKID") = dt.Rows(i)("SS_PKID")
                    dRow("GroupID") = dt.Rows(i)("SS_Group")

                    If dRow("GroupID") = 1 Then
                        dRow("Group") = "Exceptional items"
                    ElseIf dRow("GroupID") = 2 Then
                        dRow("Group") = "Extraordinary items"
                    ElseIf dRow("GroupID") = 3 Then
                        dRow("Group") = "Tax expense / (benefit)"
                    ElseIf dRow("GroupID") = 4 Then
                        dRow("Group") = "Discontinuing Operations"
                    ElseIf dRow("GroupID") = 5 Then
                        dRow("Group") = "Total Operations"
                    ElseIf dRow("GroupID") = 6 Then
                        dRow("Group") = "Earnings per share (of ` ___/- each)"
                    ElseIf dRow("GroupID") = 7 Then
                        dRow("Group") = "Earnings per share (of ` ___/- each) (excluding extraordinary items)"
                    End If

                    dRow("Perticulars") = dt.Rows(i)("SS_Particulars")
                    dRow("Values") = Convert.ToDecimal(dt.Rows(i)("SS_Values")).ToString("#,##0.00")
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getManualDetailExistOrNotSS(ByVal sSAC As String, ByVal iCompId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iOrgTypeId As Integer, ByVal iGroup As Integer, ByVal sPerticulars As String)
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "Select Count(*) from Acc_Seperate_Schedule where SS_CustId=" & iCustID & " And SS_FinancialYear=" & iYearId & " And SS_compid=" & iCompId & " And SS_OrgType=" & iOrgTypeId & " And SS_Group=" & iGroup & " And SS_Particulars='" & sPerticulars & "' and SS_Status<>'D'"
            iCount = objDBL.SQLExecuteScalar(sSAC, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOBValues(ByVal sSAC As String, ByVal iCompId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iOrgTypeId As Integer, ByVal iGLID As Integer, ByVal iSubId As Integer)
        Dim sSql As String
        Dim dSum As Double = 0.0
        Try
            sSql = "select sum(CC_CloseDebit-CC_CloseCredit) from customer_coa where CC_GL= " & iSubId & " and CC_Parent=" & iGLID & " and CC_CustID=" & iCustID & " and CC_IndType=" & iOrgTypeId & " and CC_YearId=" & iYearId & ""
            dSum = objDBL.SQLExecuteScalar(sSAC, sSql)
            Return dSum
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteManualEntriesSS(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPkid As Integer)
        Dim sSql As String
        Try
            sSql = "update  Acc_Seperate_Schedule set SS_Status='D' where SS_PKId=" & iPkid & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

End Class
