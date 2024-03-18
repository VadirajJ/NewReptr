Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class ClsManualEntryforInventory
    Dim objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Private CI_PKID As Integer
    Private CI_FinancialYear As Integer
    Private CI_CustId As Integer
    Private CI_Orgtype As Integer
    Private CI_Head As Integer
    Private CI_Group As Integer
    Private CI_Subgroup As Integer
    Private CI_Glid As Integer
    Private CI_SubGlid As Integer
    Private CI_Note As Integer
    Private CI_OBValues As Double
    Private CI_CBValues As Double
    Private CI_DATE As DateTime
    Private CI_Status As String
    Private CI_Delflag As String
    Private CI_CrBy As Integer
    Private CI_CrOn As DateTime
    Private CI_UpdatedBy As String
    Private CI_UpdatedOn As DateTime
    Private CI_SavedBy As Integer
    Private CI_SavedOn As DateTime
    Private CI_Approvedby As Integer
    Private CI_ApprovedOn As DateTime
    Private CI_IPAddress As String
    Private CI_CompID As Integer
    Public Property iCI_PKID() As Integer
        Get
            Return (CI_PKID)
        End Get
        Set(ByVal Value As Integer)
            CI_PKID = Value
        End Set
    End Property
    Public Property iCI_FinancialYear() As Integer
        Get
            Return (CI_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            CI_FinancialYear = Value
        End Set
    End Property
    Public Property iCI_CustId() As Integer
        Get
            Return (CI_CustId)
        End Get
        Set(ByVal Value As Integer)
            CI_CustId = Value
        End Set
    End Property
    Public Property iCI_Orgtype() As Integer
        Get
            Return (CI_Orgtype)
        End Get
        Set(ByVal Value As Integer)
            CI_Orgtype = Value
        End Set
    End Property
    Public Property iCI_Head() As Integer
        Get
            Return (CI_Head)
        End Get
        Set(ByVal Value As Integer)
            CI_Head = Value
        End Set
    End Property
    Public Property iCI_Group() As Integer
        Get
            Return (CI_Group)
        End Get
        Set(ByVal Value As Integer)
            CI_Group = Value
        End Set
    End Property

    Public Property iCI_Subgroup() As Integer
        Get
            Return (CI_Subgroup)
        End Get
        Set(ByVal Value As Integer)
            CI_Subgroup = Value
        End Set
    End Property
    Public Property iCI_Glid() As Integer
        Get
            Return (CI_Glid)
        End Get
        Set(ByVal Value As Integer)
            CI_Glid = Value
        End Set
    End Property
    Public Property iCI_SubGlid() As Integer
        Get
            Return (CI_SubGlid)
        End Get
        Set(ByVal Value As Integer)
            CI_SubGlid = Value
        End Set
    End Property
    Public Property iCI_Note() As Integer
        Get
            Return (CI_Note)
        End Get
        Set(ByVal Value As Integer)
            CI_Note = Value
        End Set
    End Property
    Public Property dCI_OBValues() As Double
        Get
            Return (CI_OBValues)
        End Get
        Set(ByVal Value As Double)
            CI_OBValues = Value
        End Set
    End Property
    Public Property dCI_CBValues() As Double
        Get
            Return (CI_CBValues)
        End Get
        Set(ByVal Value As Double)
            CI_CBValues = Value
        End Set
    End Property
    Public Property dCI_DATE() As DateTime
        Get
            Return (CI_DATE)
        End Get
        Set(ByVal Value As DateTime)
            CI_DATE = Value
        End Set
    End Property
    Public Property sCI_Status() As String
        Get
            Return (CI_Status)
        End Get
        Set(ByVal Value As String)
            CI_Status = Value
        End Set
    End Property
    Public Property sCI_Delflag() As String
        Get
            Return (CI_Delflag)
        End Get
        Set(ByVal Value As String)
            CI_Delflag = Value
        End Set
    End Property
    Public Property iCI_CrBy() As Integer
        Get
            Return (CI_CrBy)
        End Get
        Set(ByVal Value As Integer)
            CI_CrBy = Value
        End Set
    End Property
    Public Property dCI_CrOn() As DateTime
        Get
            Return (CI_CrOn)
        End Get
        Set(ByVal Value As DateTime)
            CI_CrOn = Value
        End Set
    End Property
    Public Property iCI_UpdatedBy() As Integer
        Get
            Return (CI_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            CI_UpdatedBy = Value
        End Set
    End Property
    Public Property dCI_UpdatedOn() As DateTime
        Get
            Return (CI_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            CI_UpdatedOn = Value
        End Set
    End Property
    Public Property iCI_SavedBy() As Integer
        Get
            Return (CI_SavedBy)
        End Get
        Set(ByVal Value As Integer)
            CI_SavedBy = Value
        End Set
    End Property
    Public Property dCI_SavedOn() As DateTime
        Get
            Return (CI_SavedOn)
        End Get
        Set(ByVal Value As DateTime)
            CI_SavedOn = Value
        End Set
    End Property
    Public Property iCI_Approvedby() As Integer
        Get
            Return (CI_Approvedby)
        End Get
        Set(ByVal Value As Integer)
            CI_Approvedby = Value
        End Set
    End Property
    Public Property dCI_ApprovedOn() As DateTime
        Get
            Return (CI_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            CI_ApprovedOn = Value
        End Set
    End Property
    Public Property sCI_IPAddress() As String
        Get
            Return (CI_IPAddress)
        End Get
        Set(ByVal Value As String)
            CI_IPAddress = Value
        End Set
    End Property
    Public Property iCI_CompID() As Integer
        Get
            Return (CI_CompID)
        End Get
        Set(ByVal Value As Integer)
            CI_CompID = Value
        End Set
    End Property
    Public Function SaveManualEntries(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal objclsManualEntryforInventory As ClsManualEntryforInventory)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_FinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Orgtype", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_Orgtype
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Subgroup", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_Subgroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Glid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_Glid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_SubGlid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_SubGlid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Note", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_Note
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_OBValues", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.dCI_OBValues
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_CBValues", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.dCI_CBValues
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_DATE", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.dCI_DATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.sCI_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Delflag", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.sCI_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_SavedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_SavedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_Approvedby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.iCI_Approvedby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.CI_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CI_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objclsManualEntryforInventory.CI_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "sp_Acc_Changes_Inventories", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getManualDetails(ByVal sSAC As String, ByVal iCompId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iOrgTypeId As Integer)
        Dim sSql As String = ""
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim dt As New DataTable
        Try
            dtDisplay.Columns.Add("PKID")
            dtDisplay.Columns.Add("Head")
            dtDisplay.Columns.Add("HeadID")
            dtDisplay.Columns.Add("Group")
            dtDisplay.Columns.Add("GroupID")
            dtDisplay.Columns.Add("SubGroup")
            dtDisplay.Columns.Add("SubGroupID")
            dtDisplay.Columns.Add("GeneralLedger")
            dtDisplay.Columns.Add("GeneralLedgerID")
            dtDisplay.Columns.Add("SubLedger")
            dtDisplay.Columns.Add("SubLedgerID")
            dtDisplay.Columns.Add("NoteNo")

            dtDisplay.Columns.Add("Values")
            dtDisplay.Columns.Add("OBValues")

            sSql = "select * from Acc_Changes_Inventories where CI_CustId=" & iCustID & " and CI_FinancialYear=" & iYearId & " and CI_compid=" & iCompId & " and CI_OrgType=" & iOrgTypeId & " and CI_Status<>'D'"
            dt = objDBL.SQLExecuteDataTable(sSAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("PKID") = dt.Rows(i)("CI_PKID")

                    If dt.Rows(i)("CI_Head") = 1 Then
                        dRow("Head") = "Assets"
                    ElseIf dt.Rows(i)("CI_Head") = 2 Then
                        dRow("Head") = "Income"
                    ElseIf dt.Rows(i)("CI_Head") = 4 Then
                        dRow("Head") = "Liabilities"
                    ElseIf dt.Rows(i)("CI_Head") = 3 Then
                        dRow("Head") = "Expenditure"
                    End If
                    dRow("HeadID") = dt.Rows(i)("CI_Head")
                    dRow("Group") = objDBL.SQLExecuteScalar(sSAC, "select (gl_glCode + ' - ' + gl_desc) from Chart_of_accounts where gl_id= " & dt.Rows(i)("CI_Group") & "")
                    dRow("GroupID") = dt.Rows(i)("CI_Group")
                    dRow("SubGroup") = objDBL.SQLExecuteScalar(sSAC, "select (gl_glCode + ' - ' + gl_desc) from Chart_of_accounts where gl_id= " & dt.Rows(i)("CI_SubGroup") & "")
                    dRow("SubGroupID") = dt.Rows(i)("CI_SubGroup")
                    dRow("GeneralLedger") = objDBL.SQLExecuteScalar(sSAC, "select (gl_glCode + ' - ' + gl_desc) from Chart_of_accounts where gl_id= " & dt.Rows(i)("CI_GlId") & "")
                    dRow("GeneralLedgerID") = dt.Rows(i)("CI_GlId")
                    dRow("SubLedger") = objDBL.SQLExecuteScalar(sSAC, "select (CC_glCode + ' - ' + CC_gldesc) from customer_coa where CC_gl= " & dt.Rows(i)("CI_SubGlId") & "")
                    dRow("SubLedgerID") = dt.Rows(i)("CI_SubGlId")
                    dRow("NoteNo") = dt.Rows(i)("CI_Note")
                    dRow("Values") = Convert.ToDecimal(dt.Rows(i)("CI_CBValues")).ToString("#,##0.00")
                    dRow("OBValues") = objDBL.SQLExecuteScalar(sSAC, "select sum(CC_CloseDebit-CC_CloseCredit) from customer_coa where CC_gl= " & dt.Rows(i)("CI_SubGlId") & " and CC_CustID=" & iCustID & " and CC_IndType=" & iOrgTypeId & " and CC_YearId=" & iYearId & "")
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getManualDetailExistOrNot(ByVal sSAC As String, ByVal iCompId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iOrgTypeId As Integer, ByVal iGLID As Integer, ByVal iSubGLID As Integer)
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "select Count(*) from Acc_Changes_Inventories where CI_CustId=" & iCustID & " and CI_FinancialYear=" & iYearId & " and CI_compid=" & iCompId & " and CI_OrgType=" & iOrgTypeId & " and CI_Glid=" & iGLID & " and CI_SubGLID=" & iSubGLID & " and CI_Status<>'D'"
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
    Public Sub DeleteManualEntries(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPkid As Integer)
        Dim sSql As String
        Try
            sSql = "update  Acc_Changes_Inventories set CI_Status='D' where CI_PKId=" & iPkid & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadGroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal iCustomerID As Integer, ByVal iOrgID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sStr As String = "" : Dim sLedger As String = ""
        Dim dtLedger As New DataTable
        Try
            If iCustomerID > 0 And iOrgID > 0 Then
                sSql = "Select CLM_GLLedger From CustomerGL_Linkage_Master Where CLM_SubGroupID=0 And CLM_GroupID in (Select GL_ID From Chart_Of_Accounts Where Gl_Desc<>'' And Gl_Parent=0 And gl_AccHead=" & iHead & " And GL_CompID=" & iACID & ") And CLM_CustID=" & iCustomerID & " And CLM_OrgID=" & iOrgID & ""
                dtLedger = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                If dtLedger.Rows.Count = 0 Then
                    GoTo gt
                End If

                If dtLedger.Rows.Count > 0 Then
                    For i = 0 To dtLedger.Rows.Count - 1
                        sLedger = dtLedger.Rows(i)("CLM_GLLedger")
                        If sLedger.StartsWith(",") Then
                            sLedger = sLedger.Remove(0, 1)
                        End If
                        sStr = sStr & sLedger
                    Next
                End If

                If sStr <> "" Then
                    If sStr.StartsWith(",") Then
                        sStr = sStr.Remove(0, 1)
                    End If
                    If sStr.EndsWith(",") Then
                        sStr = sStr.Remove(Len(sStr) - 1, 1)
                    End If

                    sSql = "" : sSql = "Select CC_Parent as gl_id,CC_GLDesc as Description  From Customer_COA Where CC_GL in (" & sStr & ") "
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
            Else
gt:             sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
                'sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 order by gl_id"
                sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iACID & " And gl_id =6 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustomerID & " and gl_Status<>'D' order by gl_id" 'viajaylakshmi 18-01-2020 chnaged gl_id <> 0 to gl_id =6 to set inventories
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iGroup As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sStr As String = "" : Dim sLedger As String = ""
        Dim dtLedger As New DataTable
        Try
            If iCustID > 0 And iOrgID > 0 Then
                sSql = "Select CLM_GLLedger From CustomerGL_Linkage_Master Where CLM_GL=0 And CLM_SubGL=0 And CLM_SubGroupID in (Select GL_ID From Chart_Of_Accounts Where Gl_Desc<>'' And Gl_Head=1 And gl_AccHead=" & iHead & " And GL_CompID=" & iACID & ") And CLM_GroupID =" & iGroup & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & ""
                dtLedger = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

                If dtLedger.Rows.Count = 0 Then
                    GoTo gt
                End If

                If dtLedger.Rows.Count > 0 Then
                    For i = 0 To dtLedger.Rows.Count - 1
                        sLedger = dtLedger.Rows(i)("CLM_GLLedger")
                        If sLedger.StartsWith(",") Then
                            sLedger = sLedger.Remove(0, 1)
                        End If
                        sStr = sStr & sLedger
                    Next
                End If

                If sStr <> "" Then
                    If sStr.StartsWith(",") Then
                        sStr = sStr.Remove(0, 1)
                    End If
                    If sStr.EndsWith(",") Then
                        sStr = sStr.Remove(Len(sStr) - 1, 1)
                    End If

                    sSql = "" : sSql = "Select CC_Parent as gl_id,CC_GLDesc as Description From Customer_COA Where CC_GL in (" & sStr & ") "
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
            Else
gt:             sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 1 and "
                ' sSql = sSql & "gl_Parent =" & iGroup & " And gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 order by gl_id"
                sSql = sSql & "gl_Parent =" & iGroup & " And gl_CompId =" & iACID & " And gl_id =30 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustID & " and gl_Status<>'D' order by gl_id" 'viajaylakshmi 18-01-2020 chnaged gl_id <> 0 to gl_id =30 to set inventories
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubGroup As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sStr As String = "" : Dim sLedger As String = ""
        Dim dtLedger As New DataTable
        Try
            If iCustID > 0 And iOrgID > 0 Then
                sSql = "Select CLM_GLLedger From CustomerGL_Linkage_Master Where CLM_GL=1 And CLM_SubGL=0 And CLM_SubGroupID in (" & iSubGroup & ") And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & ""
                dtLedger = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                If dtLedger.Rows.Count = 0 Then
                    GoTo gt
                End If
                If dtLedger.Rows.Count > 0 Then
                    For i = 0 To dtLedger.Rows.Count - 1
                        sLedger = dtLedger.Rows(i)("CLM_GLLedger")
                        If sLedger.StartsWith(",") Then
                            sLedger = sLedger.Remove(0, 1)
                        End If
                        sStr = sStr & sLedger
                    Next
                End If

                If sStr <> "" Then
                    If sStr.StartsWith(",") Then
                        sStr = sStr.Remove(0, 1)
                    End If
                    If sStr.EndsWith(",") Then
                        sStr = sStr.Remove(Len(sStr) - 1, 1)
                    End If

                    sSql = "" : sSql = "Select CC_GL as gl_id,CC_GLDesc as Description From Customer_COA Where CC_GL in (" & sStr & ") "
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
            Else
gt:             sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 2 and "
                ' sSql = sSql & "gl_Parent =" & iSubGroup & " and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 order by gl_id"
                sSql = sSql & "gl_Parent =" & iSubGroup & " and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustID & " and gl_Status<>'D' order by gl_id"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iGL As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sStr As String = "" : Dim sLedger As String = ""
        Dim dtLedger As New DataTable
        Try
            If iCustID > 0 And iOrgID > 0 Then
                sSql = "Select CLM_GLLedger From CustomerGL_Linkage_Master Where CLM_GL=0 And CLM_SubGL=1 And CLM_SubGroupID in (Select GL_ID From Chart_Of_Accounts Where Gl_Desc<>'' And Gl_Head=1 And gl_AccHead=" & iHead & " And GL_CompID=" & iACID & ") And CLM_GroupID in (Select GL_ID From Chart_Of_Accounts Where Gl_Desc<>'' And Gl_Parent=0 And gl_AccHead=" & iHead & " And GL_CompID=" & iACID & ") And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & ""
                dtLedger = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                If dtLedger.Rows.Count = 0 Then
                    GoTo gt
                End If
                If dtLedger.Rows.Count > 0 Then
                    For i = 0 To dtLedger.Rows.Count - 1
                        sLedger = dtLedger.Rows(i)("CLM_GLLedger")
                        If sLedger.StartsWith(",") Then
                            sLedger = sLedger.Remove(0, 1)
                        End If
                        sStr = sStr & sLedger
                    Next
                End If

                If sStr <> "" Then
                    If sStr.StartsWith(",") Then
                        sStr = sStr.Remove(0, 1)
                    End If
                    If sStr.EndsWith(",") Then
                        sStr = sStr.Remove(Len(sStr) - 1, 1)
                    End If

                    sSql = "" : sSql = "Select CC_GL as gl_id,CC_GLDesc as Description From Customer_COA Where CC_Parent in (" & iGL & ") And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_CompID=" & iACID & "  "
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                End If
            Else
gt:             sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 3 and "
                ' sSql = sSql & "gl_Parent =" & iGL & " And gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 order by gl_id"
                sSql = sSql & "gl_Parent =" & iGL & " And gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustID & " and gl_Status<>'D' order by gl_id"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
