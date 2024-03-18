Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsChartOfAccounts
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral
    Private gl_id As Integer
    Private gl_glcode As String
    Private gl_Parent As Integer
    Private gl_Desc As String
    Private gl_head As Integer
    Private gl_Delflag As String
    Private gl_AccHead As Integer
    Private gl_reason_Creation As String
    Private gl_Crby As Integer
    Private gl_CrDate As Date
    Private gl_DelBy As Integer
    Private gl_DelDate As Date
    Private gl_CompId As Integer
    Private gl_AppBy As Integer
    Private gl_AppOn As Date
    Private gl_Status As String
    Private gl_AccType As String
    Private gl_IPAddress As String
    Private gl_UpdatedBy As Integer
    Private gl_UpdatedOn As Date
    Private gl_OrgTypeID As Integer
    Private gl_CustID As Integer

    Private gl_SortOrder As Integer

    Public Property igl_UpdatedBy() As Integer
        Get
            Return (gl_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            gl_UpdatedBy = Value
        End Set
    End Property
    Public Property dgl_UpdatedOn() As Date
        Get
            Return (gl_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            gl_UpdatedOn = Value
        End Set
    End Property
    Public Property igl_id() As Integer
        Get
            Return (gl_id)
        End Get
        Set(ByVal Value As Integer)
            gl_id = Value
        End Set
    End Property
    Public Property sgl_glcode() As String
        Get
            Return (gl_glcode)
        End Get
        Set(ByVal Value As String)
            gl_glcode = Value
        End Set
    End Property
    Public Property igl_Parent() As Integer
        Get
            Return (gl_Parent)
        End Get
        Set(ByVal Value As Integer)
            gl_Parent = Value
        End Set
    End Property
    Public Property sgl_Desc() As String
        Get
            Return (gl_Desc)
        End Get
        Set(ByVal Value As String)
            gl_Desc = Value
        End Set
    End Property
    Public Property igl_head() As Integer
        Get
            Return (gl_head)
        End Get
        Set(ByVal Value As Integer)
            gl_head = Value
        End Set
    End Property
    Public Property sgl_Delflag() As String
        Get
            Return (gl_Delflag)
        End Get
        Set(ByVal Value As String)
            gl_Delflag = Value
        End Set
    End Property
    Public Property igl_AccHead() As Integer
        Get
            Return (gl_AccHead)
        End Get
        Set(ByVal Value As Integer)
            gl_AccHead = Value
        End Set
    End Property
    Public Property sgl_reason_Creation() As String
        Get
            Return (gl_reason_Creation)
        End Get
        Set(ByVal Value As String)
            gl_reason_Creation = Value
        End Set
    End Property
    Public Property igl_Crby() As Integer
        Get
            Return (gl_Crby)
        End Get
        Set(ByVal Value As Integer)
            gl_Crby = Value
        End Set
    End Property
    Public Property dgl_CrDate() As Date
        Get
            Return (gl_CrDate)
        End Get
        Set(ByVal Value As Date)
            gl_CrDate = Value
        End Set
    End Property
    Public Property igl_DelBy() As Integer
        Get
            Return (gl_DelBy)
        End Get
        Set(ByVal Value As Integer)
            gl_DelBy = Value
        End Set
    End Property
    Public Property dgl_DelDate() As Date
        Get
            Return (gl_DelDate)
        End Get
        Set(ByVal Value As Date)
            gl_DelDate = Value
        End Set
    End Property
    Public Property igl_CompId() As Integer
        Get
            Return (gl_CompId)
        End Get
        Set(ByVal Value As Integer)
            gl_CompId = Value
        End Set
    End Property
    Public Property igl_AppBy() As Integer
        Get
            Return (gl_AppBy)
        End Get
        Set(ByVal Value As Integer)
            gl_AppBy = Value
        End Set
    End Property
    Public Property dgl_AppOn() As Date
        Get
            Return (gl_AppOn)
        End Get
        Set(ByVal Value As Date)
            gl_AppOn = Value
        End Set
    End Property
    Public Property sgl_Status() As String
        Get
            Return (gl_Status)
        End Get
        Set(ByVal Value As String)
            gl_Status = Value
        End Set
    End Property
    Public Property sGl_AccType() As String
        Get
            Return (gl_AccType)
        End Get
        Set(ByVal Value As String)
            gl_AccType = Value
        End Set
    End Property
    Public Property sgl_IPAddress() As String
        Get
            Return (gl_IPAddress)
        End Get
        Set(ByVal Value As String)
            gl_IPAddress = Value
        End Set
    End Property
    Public Property igl_OrgTypeID() As Integer
        Get
            Return (gl_OrgTypeID)
        End Get
        Set(ByVal Value As Integer)
            gl_OrgTypeID = Value
        End Set
    End Property
    Public Property igl_CustID() As Integer
        Get
            Return (gl_CustID)
        End Get
        Set(ByVal Value As Integer)
            gl_CustID = Value
        End Set
    End Property

    Public Property igl_SortOrder() As Integer
        Get
            Return (gl_SortOrder)
        End Get
        Set(ByVal Value As Integer)
            gl_SortOrder = Value
        End Set
    End Property
    Public Function LoadChartOfAccounts(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal iOrgTypeID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iHead > 0 And iOrgTypeID > 0 And iCustomerID > 0 Then
                sSql = "Select gl_id,gl_parent,gl_desc from Chart_Of_Accounts where gl_AccHead=" & iHead & " and gl_OrgTypeID=" & iOrgTypeID & " and gl_CustID=" & iCustomerID & " and gl_CompId=" & iACID & ""
                sSql = sSql & " And gl_id <> 0  order by gl_Desc Asc"
            ElseIf iHead > 0 Then
                sSql = "Select gl_id,gl_parent,gl_desc from Chart_Of_Accounts where gl_AccHead=" & iHead & " And gl_CompId=" & iACID & " And gl_id <> 0  order by gl_Desc Asc"
            ElseIf iOrgTypeID > 0 And iCustomerID > 0 Then
                sSql = "Select gl_id,gl_parent,gl_desc from Chart_Of_Accounts where gl_OrgTypeID=" & iOrgTypeID & " and gl_CustID=" & iCustomerID & " And gl_CompId=" & iACID & " And gl_id <> 0  order by gl_Desc Asc"
            Else
                ' sSql = "Select gl_id,gl_parent,gl_desc from Chart_Of_Accounts where gl_AccHead=" & iHead & " And gl_CompId=" & iACID & " And gl_id <> 0  order by gl_Desc Asc"
                sSql = "Select gl_id,gl_parent,gl_desc from Chart_Of_Accounts where gl_CustID=" & iCustomerID & " And gl_CompId=" & iACID & " And gl_id <> 0  order by gl_Desc Asc" 'Vijayalaskshmi 13-11-19
            End If

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
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
                sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustomerID & " and gl_Status<>'D' order by gl_id"
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
                sSql = sSql & "gl_Parent =" & iGroup & " And gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustID & " and gl_Status<>'D' order by gl_id"
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
    Public Function GenerateGrpCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal iCustID As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim Grp As String = "", prefix As String = ""
        Dim GrpLength As Integer = 0
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select IsNull(count(*),0)+1 from chart_of_accounts where gl_head = 0 and "
            sSql = sSql & "gl_acchead ='" & iHead & "' and gl_compId='" & iACID & "' and gl_custId=" & iCustID & ""
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sAC, sSql))

            aSql = "" : aSql = "select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iACID & "'"
            dr = objDBL.SQLDataReader(sAC, aSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If (Grp.Length < GrpLength) Then
                    Grp = Grp.PadLeft(GrpLength, "0")
                End If
            End If
            dr.Close()
            Return prefix + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateSubGrpCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal GrpGl As Integer, ByVal iCustId As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim Grp As String = "", prefix As String = "", SubGrp As String = ""
        Dim GrpLength As Integer = 0
        Dim dr As OleDb.OleDbDataReader
        Dim sGL As String = ""
        Try
            sSql = "" : sSql = "Select IsNull(count(*),0)+1 from chart_of_accounts where gl_head=1 and gl_acchead='" & iHead & "' and gl_compId='" & iACID & "' and gl_parent = " & GrpGl & " and gl_custId=" & iCustId & ""
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sAC, sSql))

            sGL = objDBL.SQLExecuteScalar(sAC, "Select gl_glCode from chart_of_accounts where gl_id = " & GrpGl & " and gl_compId='" & iACID & "' and gl_custId=" & iCustId & " ")

            aSql = "" : aSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iACID & "'"
            dr = objDBL.SQLDataReader(sAC, aSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_subgroup")) = False Then
                    SubGrp = dr("acs_subgroup")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If (Grp.Length < SubGrp) Then
                    Grp = Grp.PadLeft(SubGrp, "0")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateGLCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal GrpGl As Integer, ByVal iCustId As Integer)
        Dim sSqlGrp As String = "", sSql As String = ""
        Dim Grp As String = "", prefix As String = "", sGL As String = ""
        Dim GrpLength As Integer, SubGrp As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            sSqlGrp = "" : sSqlGrp = "select IsNull(count(*),0)+1 from chart_of_accounts where "
            sSqlGrp = sSqlGrp & "gl_acchead ='" & iHead & "' and gl_compId='" & iACID & "' and gl_parent = " & GrpGl & " and gl_custId=" & iCustId & " "
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sAC, sSqlGrp))

            sGL = objDBL.SQLExecuteScalar(sAC, "Select gl_glCode from chart_of_accounts where gl_id = " & GrpGl & " and gl_compId='" & iACID & "' and gl_custId=" & iCustId & "  ")

            sSql = "" : sSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iACID & "'"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_subgroup")) = False Then
                    SubGrp = dr("acs_subgroup")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If Grp.Length < SubGrp Then
                    Grp = Grp.PadLeft(SubGrp, "0")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateSubGLCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal GrpGl As Integer, ByVal iCustId As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim Grp As String = "", SubGrp As String = "", prefix As String = "", sGL As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim GrpLength As Integer
        Try

            'vijaylakshmi 21-01-2020 changed from  chart_of_accounts table to Customer_COA
            'sSql = "Select IsNull(count(*),0)+1 from chart_of_accounts where gl_head=3 and gl_acchead='" & iHead & "' and gl_delflag ='C' and "
            'sSql = sSql & "gl_compId ='" & iACID & "' and gl_parent = " & GrpGl & " and gl_custId=" & iCustId & " "

            sSql = "Select IsNull(count(*),0)+1 from Customer_COA where cc_head=3 and cc_acchead='" & iHead & "' and "
            sSql = sSql & "cc_compId ='" & iACID & "' and cc_parent = " & GrpGl & " and cc_custId=" & iCustId & " "

            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sAC, sSql))

            sGL = objDBL.SQLExecuteScalar(sAC, "Select gl_glCode from chart_of_accounts where gl_id = " & GrpGl & " and gl_delflag ='C' and gl_compId='" & iACID & "' and gl_custId=" & iCustId & "  ")

            aSql = "" : aSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iACID & "'"
            dr = objDBL.SQLDataReader(sAC, aSql)
            If dr.HasRows = True Then
                dr.Read()

                If IsDBNull(dr("acs_subgroup")) = False Then
                    SubGrp = dr("acs_subgroup")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If
                If Grp.Length < SubGrp Then
                    Grp = Grp.PadLeft(SubGrp, "00")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCOADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iGlID As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Chart_of_Accounts where gl_Id =" & iGlID & " and gl_CompID = " & iACID & " and gl_custid=" & iCustId & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveChartofACC(ByVal sAC As String, ByVal iACID As Integer, ByVal objCOA As clsChartOfAccounts) As String
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "Select * from chart_of_accounts where gl_Desc='" & objCOA.sgl_Desc & "' and  gl_accHead=" & objCOA.igl_AccHead & " and gl_Head =" & objCOA.igl_head & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count = 0 Then
                iMax = objDBL.SQLExecuteScalar(sAC, "Select isnull(max(gl_ID)+1,1) from Chart_of_Accounts")
                sSql = "Insert into chart_of_accounts(gl_id,gl_glcode,gl_parent,gl_desc,gl_head,"
                sSql = sSql & "gl_acchead,gl_delflag,gl_reason_Creation,"
                sSql = sSql & "gl_CrBy,gl_CrDate,gl_CompId,"
                sSql = sSql & "gl_Status,gl_IPAddress,gl_OrgTypeID,gl_CustID,gl_SortOrder) values "
                sSql = sSql & "(" & iMax & ",'" & (objCOA.sgl_glcode) & "'," & (objCOA.igl_Parent) & ","
                sSql = sSql & "'" & objGen.SafeSQL(objCOA.sgl_Desc) & "'," & objCOA.igl_head & ","
                sSql = sSql & "" & (objCOA.igl_AccHead) & ",'" & (objCOA.sgl_Delflag) & "',"
                sSql = sSql & "'" & objGen.SafeSQL(objCOA.sgl_reason_Creation) & "',"
                sSql = sSql & "" & objCOA.igl_Crby & ",GetDate(),"
                sSql = sSql & "" & iACID & ",'" & objCOA.sgl_Status & "','" & objCOA.sgl_IPAddress & "'," & objCOA.igl_OrgTypeID & "," & objCOA.igl_CustID & "," & objCOA.igl_SortOrder & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)
                Return iMax & "," & 0
            Else
                iMax = dt.Rows(0)("gl_id").ToString()
                Return iMax & "," & 1
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdateChartofAcc(ByVal sAC As String, ByVal iACID As Integer, ByVal objCOA As clsChartOfAccounts)
        Dim sSql As String = ""
        Try
            sSql = "Update Chart_of_Accounts set gl_desc = '" & objGen.SafeSQL(objCOA.sgl_Desc) & "',gl_reason_Creation='" & objGen.SafeSQL(objCOA.sgl_reason_Creation) & "',"
            sSql = sSql & "gl_IPAddress='" & objCOA.sgl_IPAddress & "',"
            sSql = sSql & "gl_UpdatedBy=" & objCOA.igl_UpdatedBy & ",gl_UpdatedOn=GetDate()"
            sSql = sSql & "where gl_id = " & objCOA.igl_id & " and gl_OrgTypeID=" & objCOA.igl_OrgTypeID & " and gl_CustID=" & objCOA.igl_CustID & " and gl_CompID =" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ApproveChartOFAccounts(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iGliD As Integer, ByVal iOrgID As Integer, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Chart_of_Accounts set gl_Status ='A', gl_AppBy =" & iUserID & ", gl_AppOn=GetDate() where gl_id=" & iGliD & " And gl_OrgTypeID=" & iOrgID & " And gl_CustID=" & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ActiveChartOFAccounts(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iGliD As Integer, ByVal iOrgID As Integer, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Chart_of_Accounts set gl_Status ='A', gl_AppBy =" & iUserID & ", gl_AppOn=GetDate() where gl_id =" & iGliD & " And gl_OrgTypeID=" & iOrgID & " And gl_CustID=" & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeActiveChartOFAccounts(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iGliD As Integer, ByVal iOrgID As Integer, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Chart_of_Accounts set gl_Status ='D', gl_AppBy =" & iUserID & ", gl_AppOn=GetDate() where gl_id =" & iGliD & " And gl_OrgTypeID=" & iOrgID & " And gl_CustID=" & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetchartofAccountPath(ByVal sAC As String, ByVal iACID As Integer, ByVal gl_id As Integer, ByVal iCustId As Integer) As String 'Added Custid throught thid function vijaylakshmi 27/11/19
        Dim sPath As String = "", sSql As String = ""
        Dim iParent As Integer
        Dim myDataTable As New DataTable
        Try
            sSql = "" : sSql = "Select gl_parent,gl_desc from Chart_Of_Accounts where gl_id = " & gl_id & " and  gl_CompId=" & iACID & " and gl_custid=" & iCustId & ""
            myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
            If myDataTable.Rows.Count > 0 Then
                iParent = Convert.ToInt16(myDataTable.Rows(0)("gl_parent").ToString())
                sPath = myDataTable.Rows(0)("gl_desc").ToString()
                If iParent <> 0 Then
                    sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iACID & " and gl_custid=" & iCustId & ""
                    myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If myDataTable.Rows.Count > 0 Then
                        sPath = myDataTable.Rows(0)("gl_desc").ToString() & "/" & sPath
                        iParent = Convert.ToInt16(myDataTable.Rows(0)("gl_parent").ToString())
                        If iParent <> 0 Then
                            sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iACID & " and gl_custid=" & iCustId & ""
                            myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
                            If myDataTable.Rows.Count > 0 Then
                                sPath = myDataTable.Rows(0)("gl_desc").ToString() & "/" & sPath
                                iParent = Convert.ToInt16(myDataTable.Rows(0)("gl_parent").ToString())
                                If iParent <> 0 Then
                                    sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iACID & " and gl_custid=" & iCustId & ""
                                    myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
                                    If myDataTable.Rows.Count > 0 Then
                                        sPath = myDataTable.Rows(0)("gl_desc").ToString() & "/" & sPath
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return sPath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChartOfAccountsDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iGlID As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Chart_Of_Accounts where gl_id =" & iGlID & " and gl_CompID =" & iACID & " and gl_Delflag ='C' and gl_CustId=" & iCustId & "" 'vijayalakshmi 13-11-2019 included cust id
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadParent(ByVal sAC As String, ByVal iACID As Integer, ByVal iGlID As Integer) As Integer
        Dim sSql As String = ""
        Dim iParent As Integer = 0
        Try
            sSql = "" : sSql = "Select gl_Parent from Chart_Of_Accounts where gl_id =" & iGlID & " and gl_CompID =" & iACID & " and gl_Delflag ='C'"
            iParent = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ImportCOA(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iOrgTypeID As Integer, ByVal iCustID As Integer)
        Dim sSql As String, sSql1 As String = ""
        Try
            sSql = "Insert Into Chart_Of_Accounts Select gl_id,gl_glcode,gl_parent,gl_desc,gl_head,NULL,NULL,NULL,"
            sSql = sSql & " gl_delflag,NULL,gl_acchead,gl_reason_Creation,NULL," & iUserID & ",GetDate(),NULL,NULL,gl_sortorder," & iACID & ",NULL,NULL,NULL,"
            sSql = sSql & " " & iUserID & ",GetDate(),gl_Status,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" & sIPAddress & "'," & iOrgTypeID & "," & iCustID & ""
            sSql = sSql & " From Chart_Of_Accounts Where gl_OrgTypeID=0 and gl_id <> 0"
            sSql = sSql & " Group BY gl_id,gl_glcode,gl_parent,gl_desc,gl_head,gl_delflag,gl_acchead,gl_reason_Creation,gl_sortorder,gl_Status"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadStandardChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "Select gl_id,gl_parent,gl_desc from Chart_Of_Accounts where gl_CompId=" & iCompID & " and gl_OrgTypeID=0 and gl_id <> 0 order by gl_Desc Asc"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrgTypeID(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String, ByVal iCustID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select cmm_ID from Content_Management_Master Left join SAD_CUSTOMER_MASTER On CUST_ORGTYPEID=cmm_ID And CUST_CompID=" & iACID & ""
            sSql = sSql & " And CUST_DELFLG='A' where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' And CUST_ID=" & iCustID & " order by cmm_Desc Asc"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCOACountOFCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "Select Count(*) from Chart_Of_Accounts where gl_CompId=" & iCompID & " and gl_custID=" & iCustId & ""
            iCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCUstChartOfAccoutList(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer, ByVal iOrgId As Integer)
        Dim sSql As String
        Dim dta, dtl, dti, dte, dtTab, dts, dtg, dth As New DataTable
        Dim dtAsset As New DataTable, dtLiablities As New DataTable, dtIncome As New DataTable, dtExpenses As New DataTable
        Dim dRow As DataRow

        Try
            dtTab.Columns.Add("SLNo")
            dtTab.Columns.Add("Head")
            dtTab.Columns.Add("Group")
            dtTab.Columns.Add("SubGroup")
            dtTab.Columns.Add("GeneralLedger")
            dtTab.Columns.Add("CustomerName")

            'sSql = "Select * from Chart_Of_Accounts where gl_CompId=" & iCompID & " and gl_custID=" & iCustId & " and gl_head=2 and gl_status <> 'D' order by gl_sortOrder,gl_id "
            ' sSql = "Select * from Chart_Of_Accounts where gl_CompId=" & iCompID & " and gl_custID=" & iCustId & " and gl_head=2 and gl_status <> 'D' order by gl_id "  'Commented 04_12_19
            Dim s1(3) As String
            s1(0) = 4
            s1(1) = 1
            s1(2) = 2
            s1(3) = 3
            For Each element As String In s1
                dta.Clear()
                sSql = "Select distinct(a.gl_desc) as GeneralLedger,a.gl_glcode , b.gl_desc as SubGroup, c.gl_AccHead,c.gl_desc as GroupName from Chart_Of_Accounts a "
                sSql = sSql & "join Chart_Of_Accounts b on b.gl_id = a.gl_parent join Chart_Of_Accounts c on c.gl_id = b.gl_parent "
                sSql = sSql & " where a.gl_CompId=" & iCompID & " and a.gl_custID=" & iCustId & " and a.gl_head=2 and c.gl_AccHead = " & element & ""
                sSql = sSql & " and a.gl_status <> 'D' order by  a.gl_glcode "
                dta = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                If dta.Rows.Count > 0 Then
                    For i = 0 To dta.Rows.Count - 1
                        dRow = dtTab.NewRow()
                        dRow("SLNo") = i + 1

                        If dta.Rows(i)("gl_AccHead") = 1 Then
                            dRow("Head") = "Assets"
                        ElseIf dta.Rows(i)("gl_AccHead") = 2 Then
                            dRow("Head") = "Income"
                        ElseIf dta.Rows(i)("gl_AccHead") = 3 Then
                            dRow("Head") = "Expenditure"
                        ElseIf dta.Rows(i)("gl_AccHead") = 4 Then
                            dRow("Head") = "Liabilities"
                        End If

                        dRow("Group") = dta.Rows(i)("GroupName")

                        dRow("SubGroup") = dta.Rows(i)("SubGroup")

                        dRow("GeneralLedger") = dta.Rows(i)("GeneralLedger")

                        dRow("CustomerName") = objDBL.SQLExecuteScalar(sNameSpace, "Select Cust_Name from SAD_CUSTOMER_MASTER Where CUST_DelFlg = 'A' and cust_Compid=" & iCompID & " and Cust_Id=" & iCustId & "")
                        dtTab.Rows.Add(dRow)
                    Next
                End If
                dtTab.Merge(dtTab)
            Next


            'dta = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            'If dta.Rows.Count > 0 Then
            '    For i = 0 To dta.Rows.Count - 1
            '        dRow = dtLiablities.NewRow()
            '        dRow("SLNo") = i + 1

            '        dRow("GeneralLedger") = dta.Rows(i)("gl_desc")
            '        dts = getCOADesc(sNameSpace, iCompID, iYearID, iCustId, iOrgId, dta.Rows(i)("gl_Parent"), dta.Rows(i)("gl_id"))
            '        dRow("SubGroup") = dts.Rows(0)("gl_desc")
            '        dtg = getCOADesc(sNameSpace, iCompID, iYearID, iCustId, iOrgId, dts.Rows(0)("gl_Parent"), dts.Rows(0)("gl_id"))
            '        dRow("Group") = dtg.Rows(0)("gl_desc")

            '        If dtg.Rows(0)("gl_AccHead") = 1 Then
            '            dRow("Head") = "Assets"
            '        ElseIf dtg.Rows(0)("gl_AccHead") = 2 Then
            '            dRow("Head") = "Income"
            '        ElseIf dtg.Rows(0)("gl_AccHead") = 3 Then
            '            dRow("Head") = "Expenditure"
            '        ElseIf dtg.Rows(0)("gl_AccHead") = 4 Then
            '            dRow("Head") = "Liabilities"
            '        End If

            '        dRow("CustomerName") = objDBL.SQLExecuteScalar(sNameSpace, "Select Cust_Name from SAD_CUSTOMER_MASTER Where CUST_DelFlg = 'A' and cust_Compid=" & iCompID & " and Cust_Id=" & iCustId & "")
            '        dtLiablities.Rows.Add(dRow)
            '    Next
            'End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCOADesc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer, ByVal iOrgId As Integer, ByVal iParent As Integer, ByVal iGlId As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Chart_Of_Accounts where gl_CompId=" & iCompID & " and gl_custID=" & iCustId & " and  gl_id=" & iParent & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNoteNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iSubGroup As Integer, ByVal iCustId As Integer, ByVal iOrgId As Integer)
        Dim sSql As String
        Dim iNoteNo As Integer
        Try
            sSql = "Select SLM_NoteNo from schedule_linkage_master where SLM_CompId=" & iCompID & " and SLM_Orgid=" & iOrgId & " and SLM_custID=" & iCustId & " and  SLM_SUbGroupid=" & iSubGroup & ""
            iNoteNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iNoteNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOADetailsSGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iGlID As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CC_GLCode as gl_glcode, CC_Gldesc as gl_desc from Customer_COA where CC_GL =" & iGlID & " and CC_CompID = " & iACID & " and CC_custid=" & iCustId & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetchartofAccountPathSGL(ByVal sAC As String, ByVal iACID As Integer, ByVal gl_id As Integer, ByVal iCustId As Integer) As String 'Added Custid throught thid function vijaylakshmi 27/11/19
        Dim sPath As String = "", sSql As String = ""
        Dim iParent As Integer
        Dim myDataTable As New DataTable
        Try
            sSql = "" : sSql = "Select CC_parent as gl_parent,CC_gldesc as gl_desc from Customer_COA where CC_GL = " & gl_id & " and  CC_CompId=" & iACID & " and CC_custid=" & iCustId & ""
            myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
            If myDataTable.Rows.Count > 0 Then
                iParent = Convert.ToInt16(myDataTable.Rows(0)("gl_parent").ToString())
                sPath = myDataTable.Rows(0)("gl_desc").ToString()
                If iParent <> 0 Then
                    sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iACID & " and gl_custid=" & iCustId & ""
                    myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
                    If myDataTable.Rows.Count > 0 Then
                        sPath = myDataTable.Rows(0)("gl_desc").ToString() & "/" & sPath
                        iParent = Convert.ToInt16(myDataTable.Rows(0)("gl_parent").ToString())
                        If iParent <> 0 Then
                            sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iACID & " and gl_custid=" & iCustId & ""
                            myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
                            If myDataTable.Rows.Count > 0 Then
                                sPath = myDataTable.Rows(0)("gl_desc").ToString() & "/" & sPath
                                iParent = Convert.ToInt16(myDataTable.Rows(0)("gl_parent").ToString())
                                If iParent <> 0 Then
                                    sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iACID & " and gl_custid=" & iCustId & ""
                                    myDataTable = objDBL.SQLExecuteDataTable(sAC, sSql)
                                    If myDataTable.Rows.Count > 0 Then
                                        sPath = myDataTable.Rows(0)("gl_desc").ToString() & "/" & sPath
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return sPath
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
