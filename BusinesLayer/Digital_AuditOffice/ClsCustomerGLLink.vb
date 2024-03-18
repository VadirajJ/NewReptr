Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsCustomerGLLink
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral
    Public Function LoadGeneralLedger(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select gl_ID,gl_Desc From Chart_Of_Accounts Where gl_AccHead = " & iHead & " and gl_head in(1) and gl_CompID=" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 and gl_delflag ='C' order by gl_Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
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
    Public Function LoadAllCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where cust_Compid=" & iACID & " order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOrgType(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select cmm_ID,cmm_Desc from Content_Management_Master Left join SAD_CUSTOMER_MASTER On CUST_ORGTYPEID=cmm_ID And CUST_CompID=" & iACID & ""
            sSql = sSql & " And CUST_DELFLG='A' where CMM_CompID=" & iACID & " And cmm_Category='" & sType & "' And cmm_Delflag='A' And CUST_ID=" & iCustID & " order by cmm_Desc Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer)
        Dim sSql As String
        Try
            'sSql = "Select CC_GL,CC_GLDesc from Customer_COA Where CC_Head=2 And CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_YearID=" & iYearID & " And CC_CompID=" & iACID & " Order By CC_GLDesc"
            sSql = "Select CC_GL,CC_GLDesc from Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_YearID=" & iYearID & " And CC_CompID=" & iACID & " Order By CC_GLDesc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveScheduleLinkageMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal sGeneralLedger As String, ByVal iHead As Integer, ByVal iIPAddress As String, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iGL As Integer, ByVal iSubGL As Integer, ByVal iGlId As Integer)
        Dim sSql As String = ""
        Dim iMaxId As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            If iGroup > 0 And iSubGroup = 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and  "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL > 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and  "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_GL=" & iGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & " and CLM_GLID=" & iGlId & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iSubGL > 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and  "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & " and CLM_GLID=" & iGlId & ""
            End If
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                If iGroup > 0 And iSubGroup = 0 And iGL = 0 And iSubGL = 0 Then
                    sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='U',CLM_IPAddress='" & iIPAddress & "' where "
                    sSql = sSql & "CLM_Head =" & iHead & " and CLM_GroupID=" & iGroup & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & " "
                ElseIf iGroup > 0 And iSubGroup > 0 And iGL = 0 And iSubGL = 0 Then
                    sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='U',CLM_IPAddress='" & iIPAddress & "' where "
                    sSql = sSql & "CLM_Head =" & iHead & " and CLM_GroupID=" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & ""
                ElseIf iGroup > 0 And iSubGroup > 0 And iGL > 0 Then
                    sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='U',CLM_IPAddress='" & iIPAddress & "' where "
                    sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_GL=" & iGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & " and CLM_GLID=" & iGlId & ""
                ElseIf iGroup > 0 And iSubGroup > 0 And iSubGL > 0 Then
                    sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='U',CLM_IPAddress='" & iIPAddress & "' where "
                    sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_YearID =" & iYearID & " and CLM_GLID=" & iGlId & ""
                End If
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            Else
                iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CLM_ID),0)+1 from CustomerGL_Linkage_Master")
                sSql = "" : sSql = "Insert into CustomerGL_Linkage_Master(CLM_ID,CLM_Head,CLM_GroupID,CLM_SubGroupID,"
                sSql = sSql & "CLM_GLLedger,CLM_CreatedBy,"
                sSql = sSql & "CLM_CreatedOn,CLM_Status,CLM_YearID,CLM_CompID,CLM_Operation,CLM_IPAddress,CLM_CustID,CLM_OrgID,CLM_GL,CLM_SubGL,CLM_GLID)"
                sSql = sSql & "Values(" & iMaxId & "," & iHead & "," & iGroup & "," & iSubGroup & ","
                sSql = sSql & "'" & sGeneralLedger & "'," & iUserID & ","
                sSql = sSql & "GetDate(),'A'," & iYearID & "," & iACID & ",'C','" & iIPAddress & "'," & iCustID & "," & iOrgID & "," & iGL & "," & iSubGL & "," & iGlId & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
            dr.Close()
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSavedInventoryDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal ICustID As Integer, ByVal iOrgID As Integer)
        Dim sSql As String = "", sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where "
            sSql = sSql & " CLM_CompID =" & iACID & " And CLM_Status ='A' And CLM_CustID=" & ICustID & " and CLM_OrgID =" & iOrgID & "" ' and SLM_YearID =" & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sStr = sStr & "," & dt.Rows(i)("CLM_GLLedger")
                Next
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSavedGLDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iSubGL As Integer, ByVal iGLID As Integer) As DataTable
        Dim sSql As String = "", sStr As String = "", sLedger As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            If iGroup > 0 And iSubGroup = 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " And CLM_GL=" & iGL & " And CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " And CLM_GL=" & iGL & " And CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL > 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " And CLM_GL=" & iGL & " And CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_GLID=" & iGLID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iSubGL > 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " And CLM_GL=" & iGL & " And CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " and CLM_CustID=" & iCustID & " And CLM_OrgID =" & iOrgID & " and CLM_GLID=" & iGLID & "" 'and SLM_YearID =" & iYearID & ""
            End If

            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sStr = dr("CLM_GLLedger")
                dr.Close()
            End If

            If sStr <> "" Then
                sArray = sStr.Split(",")
                For i = 0 To sArray.Length - 1
                    If sArray(i) <> "" Then
                        sLedger = sLedger & "," & sArray(i)
                    End If
                Next
                sLedger = sLedger.Remove(0, 1)
                sSql = "Select CC_GL,CC_GLDesc From Customer_COA Where CC_GL In(" & sLedger & ") And CC_CompID=" & iACID & " And CC_GL <> 0 And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " order by CC_GLDesc"
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSavedGLS(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iSubGL As Integer, ByVal iGLID As Integer) As String
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim sStr As String = ""
        Try
            If iGroup > 0 And iSubGroup = 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL > 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " And CLM_GL=" & iGL & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & " and CLM_GLID=" & iGLID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iSubGL > 0 Then
                sSql = "" : sSql = "Select * from CustomerGL_Linkage_Master where CLM_Head =" & iHead & " and "
                sSql = sSql & "CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " And CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & " and CLM_GLID=" & iGLID & "" 'and SLM_YearID =" & iYearID & ""
            End If
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sStr = dr("CLM_GLLedger")
            End If
            dr.Close()
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteGeneralLedger(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iHead As Integer, ByVal sGeneralLedger As String, ByVal iIPAddress As String, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iGL As Integer, ByVal iSubGL As Integer, ByVal iGlId As Integer)
        Dim sSql As String = ""
        Try
            If iGroup > 0 And iSubGroup = 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='D',CLM_IPAddress='" & iIPAddress & "' where "
                sSql = sSql & "CLM_Head =" & iHead & " and CLM_GroupID =" & iGroup & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL = 0 And iSubGL = 0 Then
                sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='D',CLM_IPAddress='" & iIPAddress & "' where "
                sSql = sSql & "CLM_Head =" & iHead & " and CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & "" 'and SLM_YearID =" & iYearID & ""
            ElseIf iGroup > 0 And iSubGroup > 0 And iGL > 0 Then
                sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='D',CLM_IPAddress='" & iIPAddress & "' where "
                sSql = sSql & "CLM_Head =" & iHead & " and CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_GL=" & iGL & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & " and CLM_GLID=" & iGlId & "" 'and SLM_YearID =" & iYearID & ""

                'sSql = "" : sSql = "Update Customer_COA set CC_AccHead=0,CC_Head=2,CC_GLCode='',CC_Parent=0 where "
                'sSql = sSql & " CC_ID=" & iGL & " and CC_CompID =" & iACID & " And CC_CustID=" & iCustID & " and CC_IndType =" & iOrgID & "" 'and SLM_YearID =" & iYearID & ""

            ElseIf iGroup > 0 And iSubGroup > 0 And iSubGL > 0 Then
                sSql = "" : sSql = "Update CustomerGL_Linkage_Master set CLM_GLLedger='" & sGeneralLedger & "',CLM_Operation='D',CLM_IPAddress='" & iIPAddress & "' where "
                sSql = sSql & "CLM_Head =" & iHead & " and CLM_GroupID =" & iGroup & " and CLM_SubGroupID =" & iSubGroup & " and CLM_SubGL=" & iSubGL & " and CLM_CompID =" & iACID & " And CLM_CustID=" & iCustID & " and CLM_OrgID =" & iOrgID & " and CLM_GLID=" & iGlId & "" 'and SLM_YearID =" & iYearID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckRecord(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Customer_COA Where CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            CheckRecord = objDBL.DBCheckForRecord(sAC, sSql)
            Return CheckRecord
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerTrailBal(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CC_GLCode,CC_GLDesc,CC_OBDebit,CC_OBCredit,CC_TrDebit,CC_TrCredit From Customer_COA Where CC_CustID=" & iCustID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            GetCustomerTrailBal = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCOAGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iAccHead As Integer, ByVal iParent As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Select CC_GL,CC_GLDesc From Customer_COA Where CC_AccHead=" & iAccHead & " And CC_Head=2 And CC_Parent=" & iParent & " And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            BindCOAGL = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCustCoaGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iHead As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Customer_COA set cc_acchead=0,cc_parent=0,CC_GLCode='' Where CC_GL=" & iId & "  And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateCustCoaSGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iHead As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Customer_COA set cc_head=2,cc_acchead=0,cc_parent=0,CC_GLCode='' Where CC_GL=" & iId & "  And CC_CustID=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_CompID=" & iACID & " And CC_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Function CopyLinkageForThisYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim bCheck As Boolean
    '    Dim iMaxId As Integer
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID & " "
    '        bCheck = objDBL.DBCheckForRecord(sAC, sSql)
    '        If bCheck = True Then
    '            sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID - 1 & " "
    '            bCheck = objDBL.DBCheckForRecord(sAC, sSql)
    '            If bCheck = True Then
    '                sSql = "" : sSql = "Delete From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID & " "
    '                objDBL.SQLExecuteNonQuery(sAC, sSql)

    '                sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID - 1 & " "
    '                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

    '                If dt.Rows.Count > 0 Then
    '                    For i = 0 To dt.Rows.Count - 1
    '                        iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CLM_ID),0)+1 from CustomerGL_Linkage_Master")
    '                        sSql = "" : sSql = "Insert into CustomerGL_Linkage_Master(CLM_ID,CLM_Head,CLM_GroupID,CLM_SubGroupID,"
    '                        sSql = sSql & "CLM_GLLedger,CLM_CreatedBy,"
    '                        sSql = sSql & "CLM_CreatedOn,CLM_Status,CLM_YearID,CLM_CompID,CLM_Operation,CLM_IPAddress,CLM_CustID,CLM_OrgID,CLM_GL,CLM_SubGL)"
    '                        sSql = sSql & "Values(" & iMaxId & "," & dt.Rows(i)("CLM_Head") & "," & dt.Rows(i)("CLM_GroupID") & "," & dt.Rows(i)("CLM_SubGroupID") & ","
    '                        sSql = sSql & "'" & dt.Rows(i)("CLM_GLLedger") & "'," & dt.Rows(i)("CLM_CreatedBy") & ","
    '                        sSql = sSql & "GetDate(),'A'," & iYearID & "," & iACID & ",'C','" & dt.Rows(i)("CLM_IPAddress") & "'," & dt.Rows(i)("CLM_CustID") & "," & dt.Rows(i)("CLM_OrgID") & "," & dt.Rows(i)("CLM_GL") & "," & dt.Rows(i)("CLM_SubGL") & ")"
    '                        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '                    Next
    '                End If
    '            Else
    '            End If
    '            CopyLinkageForThisYear = 1
    '        Else
    '            sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID - 1 & " "
    '            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CLM_ID),0)+1 from CustomerGL_Linkage_Master")
    '                    sSql = "" : sSql = "Insert into CustomerGL_Linkage_Master(CLM_ID,CLM_Head,CLM_GroupID,CLM_SubGroupID,"
    '                    sSql = sSql & "CLM_GLLedger,CLM_CreatedBy,"
    '                    sSql = sSql & "CLM_CreatedOn,CLM_Status,CLM_YearID,CLM_CompID,CLM_Operation,CLM_IPAddress,CLM_CustID,CLM_OrgID,CLM_GL,CLM_SubGL)"
    '                    sSql = sSql & "Values(" & iMaxId & "," & dt.Rows(i)("CLM_Head") & "," & dt.Rows(i)("CLM_GroupID") & "," & dt.Rows(i)("CLM_SubGroupID") & ","
    '                    sSql = sSql & "'" & dt.Rows(i)("CLM_GLLedger") & "'," & dt.Rows(i)("CLM_CreatedBy") & ","
    '                    sSql = sSql & "GetDate(),'A'," & iYearID & "," & iACID & ",'C','" & dt.Rows(i)("CLM_IPAddress") & "'," & dt.Rows(i)("CLM_CustID") & "," & dt.Rows(i)("CLM_OrgID") & "," & dt.Rows(i)("CLM_GL") & "," & dt.Rows(i)("CLM_SubGL") & ")"
    '                    objDBL.SQLExecuteNonQuery(sAC, sSql)
    '                Next
    '            End If
    '            CopyLinkageForThisYear = 0
    '        End If
    '        Return CopyLinkageForThisYear
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Vijaylakshmi 13/01/2020 included CLM_GLID into insert Query
    Public Function CopyLinkageForThisYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer) As Integer
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim iMaxId As Integer
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID & " "
            bCheck = objDBL.DBCheckForRecord(sAC, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID - 1 & " "
                bCheck = objDBL.DBCheckForRecord(sAC, sSql)
                If bCheck = True Then
                    sSql = "" : sSql = "Delete From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID & " "
                    objDBL.SQLExecuteNonQuery(sAC, sSql)

                    sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID - 1 & " "
                    dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CLM_ID),0)+1 from CustomerGL_Linkage_Master")
                            sSql = "" : sSql = "Insert into CustomerGL_Linkage_Master(CLM_ID,CLM_Head,CLM_GroupID,CLM_SubGroupID,"
                            sSql = sSql & "CLM_GLLedger,CLM_CreatedBy,"
                            sSql = sSql & "CLM_CreatedOn,CLM_Status,CLM_YearID,CLM_CompID,CLM_Operation,CLM_IPAddress,CLM_CustID,CLM_OrgID,CLM_GL,CLM_SubGL,CLM_GLID)"
                            sSql = sSql & "Values(" & iMaxId & "," & dt.Rows(i)("CLM_Head") & "," & dt.Rows(i)("CLM_GroupID") & "," & dt.Rows(i)("CLM_SubGroupID") & ","
                            sSql = sSql & "'" & dt.Rows(i)("CLM_GLLedger") & "'," & dt.Rows(i)("CLM_CreatedBy") & ","
                            sSql = sSql & "GetDate(),'A'," & iYearID & "," & iACID & ",'C','" & dt.Rows(i)("CLM_IPAddress") & "'," & dt.Rows(i)("CLM_CustID") & "," & dt.Rows(i)("CLM_OrgID") & "," & dt.Rows(i)("CLM_GL") & "," & dt.Rows(i)("CLM_SubGL") & "," & dt.Rows(i)("CLM_GLID") & ")"
                            objDBL.SQLExecuteNonQuery(sAC, sSql)
                        Next
                    End If
                Else
                End If
                CopyLinkageForThisYear = 1
            Else
                sSql = "" : sSql = "Select * From CustomerGL_Linkage_Master Where CLM_CompID=" & iACID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iOrgID & " And CLM_YearID=" & iYearID - 1 & " "
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CLM_ID),0)+1 from CustomerGL_Linkage_Master")
                        sSql = "" : sSql = "Insert into CustomerGL_Linkage_Master(CLM_ID,CLM_Head,CLM_GroupID,CLM_SubGroupID,"
                        sSql = sSql & "CLM_GLLedger,CLM_CreatedBy,"
                        sSql = sSql & "CLM_CreatedOn,CLM_Status,CLM_YearID,CLM_CompID,CLM_Operation,CLM_IPAddress,CLM_CustID,CLM_OrgID,CLM_GL,CLM_SubGL,CLM_GLID)"
                        sSql = sSql & "Values(" & iMaxId & "," & dt.Rows(i)("CLM_Head") & "," & dt.Rows(i)("CLM_GroupID") & "," & dt.Rows(i)("CLM_SubGroupID") & ","
                        sSql = sSql & "'" & dt.Rows(i)("CLM_GLLedger") & "'," & dt.Rows(i)("CLM_CreatedBy") & ","
                        sSql = sSql & "GetDate(),'A'," & iYearID & "," & iACID & ",'C','" & dt.Rows(i)("CLM_IPAddress") & "'," & dt.Rows(i)("CLM_CustID") & "," & dt.Rows(i)("CLM_OrgID") & "," & dt.Rows(i)("CLM_GL") & "," & dt.Rows(i)("CLM_SubGL") & "," & dt.Rows(i)("CLM_GLID") & ")"
                        objDBL.SQLExecuteNonQuery(sAC, sSql)
                    Next
                End If
                CopyLinkageForThisYear = 0
            End If
            Return CopyLinkageForThisYear
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function saveglsublgl(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iOrgID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim iMaxId As Integer
        Dim dt As New DataTable, dt1 As New DataTable, dt2 As New DataTable
        Dim i As Integer, i1 As Integer
        Try
            sSql = "Select * From Customer_COA Where CC_Head=1 And CC_CustiD=" & iCustID & " And CC_IndType=" & iOrgID & " And CC_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Select * From Customer_COA Where CC_AccHead = '" & dt.Rows(i)("CC_AccHead") & "' And CC_Head = 2 And CC_Parent ='" & dt.Rows(i)("CC_Parent") & "' And CC_CustiD = " & iCustID & " And CC_IndType = " & iOrgID & " And CC_CompID = " & iACID & " and CC_GLDesc='" & dt.Rows(i)("CC_GLDesc") & "'"
                    dt1 = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                    If dt1.Rows.Count = 0 Then
                        iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CC_ID),0)+1 from Customer_COA")
                        sSql = "insert into Customer_COA(CC_ID,CC_AccHead,CC_Head,CC_GL,CC_Parent,CC_GLCode,CC_GLDesc,CC_CustID,CC_IndType,CC_OBDebit,"
                        sSql = sSql & "CC_OBCredit,CC_TrDebit,cc_TrCredit,CC_CloseDebit,CC_CloseCredit,CC_YearID,CC_CompID,CC_Status,"
                        sSql = sSql & "CC_Createdby,CC_CreatedOn,CC_Operation,CC_IPAddress)"
                        sSql = sSql & "values(" & iMaxId & "," & dt.Rows(i)("CC_AccHead") & ",2," & iMaxId & "," & dt.Rows(i)("CC_Parent") & ","
                        sSql = sSql & "'" & dt.Rows(i)("CC_GLCode") & "','" & dt.Rows(i)("CC_GLDesc") & "'," & iCustID & "," & iOrgID & ",0,0,0,0,0,0," & iYearID & ","
                        sSql = sSql & "" & iACID & ",'W'," & dt.Rows(i)("CC_Createdby") & ",GetDate(),'C','" & dt.Rows(i)("CC_IPAddress") & "')"
                        objDBL.SQLExecuteNonQuery(sAC, sSql)
                    End If
                    sSql = "Select * From Customer_COA Where CC_AccHead = '" & dt.Rows(i)("CC_AccHead") & "' And CC_Head = 2 And CC_Parent ='" & dt.Rows(i)("CC_Parent") & "' And CC_CustiD = " & iCustID & " And CC_IndType = " & iOrgID & " And CC_CompID = " & iACID & " and CC_GLDesc='" & dt.Rows(i)("CC_GLDesc") & "'"
                    dt1 = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                    For i1 = 0 To dt1.Rows.Count - 1
                        sSql = "Select * From Customer_COA Where CC_AccHead = '" & dt.Rows(i)("CC_AccHead") & "' And CC_Head = 3 And CC_Parent ='" & dt.Rows(i)("CC_Parent") & "' And CC_CustiD = " & iCustID & " And CC_IndType = " & iOrgID & " And CC_CompID = " & iACID & "  and CC_GLDesc='" & dt.Rows(i)("CC_GLDesc") & "'"
                        dt2 = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                        If dt2.Rows.Count = 0 Then
                            iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(CC_ID),0)+1 from Customer_COA")
                            sSql = "insert into Customer_COA(CC_ID,CC_AccHead,CC_Head,CC_GL,CC_Parent,CC_GLCode,CC_GLDesc,CC_CustID,CC_IndType,CC_OBDebit,"
                            sSql = sSql & "CC_OBCredit,CC_TrDebit,cc_TrCredit,CC_CloseDebit,CC_CloseCredit,CC_YearID,CC_CompID,CC_Status,"
                            sSql = sSql & "CC_Createdby,CC_CreatedOn,CC_Operation,CC_IPAddress)"
                            sSql = sSql & "values(" & iMaxId & "," & dt1.Rows(i1)("CC_AccHead") & ",3," & iMaxId & "," & dt1.Rows(i1)("CC_ID") & ","
                            sSql = sSql & "'" & dt1.Rows(i1)("CC_GLCode") & "','" & dt1.Rows(i1)("CC_GLDesc") & "'," & iCustID & "," & iOrgID & ",0,0,0,0,0,0," & iYearID & ","
                            sSql = sSql & "" & iACID & ",'W'," & dt1.Rows(i1)("CC_Createdby") & ",GetDate(),'C','" & dt1.Rows(i1)("CC_IPAddress") & "')"
                            objDBL.SQLExecuteNonQuery(sAC, sSql)
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function GetUnGroupedGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer, ByVal iYearId As Integer) As String
        Dim dt, dt1 As New DataTable
        Dim sSql As String = ""
        Dim sStr As String = ""
        Dim sDesc As String = ""
        Dim sGL As String = ""
        Dim dtGL As New DataTable : Dim sStrGL As String = "" : Dim sLedger As String = ""
        Try

            sSql = "" : sSql = "Select CLM_GLLedger From customerGL_Linkage_Master Where CLM_GL=1 And CLM_OrgID=" & iIndType & " And CLM_CustID=" & iCustID & " And CLM_YearID=" & iYearId & " And CLM_CompID=" & iCompID & " "
            dtGL = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGL.Rows.Count > 0 Then
                For k = 0 To dtGL.Rows.Count - 1
                    sStrGL = dtGL.Rows(k)("CLM_GLLedger")
                    If sStrGL <> "" Then
                        If sStrGL.StartsWith(",") Then
                            sStrGL = sStrGL.Remove(0, 1)
                        End If
                        If sStrGL.EndsWith(",") Then
                            sStrGL = sStrGL.Remove(Len(sStrGL) - 1, 1)
                        End If
                        sLedger = sLedger & "," & sStrGL
                    End If
                Next

                If sLedger.StartsWith(",") Then
                    sLedger = sLedger.Remove(0, 1)
                End If
                If sLedger.EndsWith(",") Then
                    sLedger = sLedger.Remove(Len(sLedger) - 1, 1)
                End If
            End If

            If sLedger <> "" Then
                sSql = "" : sSql = "Select * From Customer_COA Where CC_GL Not In (" & sLedger & ") And CC_Parent=0 and CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " and CC_CompID=" & iCompID & " And CC_YearID=" & iYearId & " Order By CC_GLDesc "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sStr = sStr & "," & dt.Rows(i)("CC_GLDesc")
                    Next
                    If sStr.StartsWith(",") Then
                        sStr = sStr.Remove(0, 1)
                    End If
                    If sStr.EndsWith(",") Then
                        sStr = sStr.Remove(Len(sStr) - 1, 1)
                    End If
                    GetUnGroupedGL = sStr
                Else
                    GetUnGroupedGL = ""
                End If
            Else
                GetUnGroupedGL = ""
            End If

            Return GetUnGroupedGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustLinkageGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1, dtCustCOA, dtCustCOASubGL As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = ""
        Try
            dt1.Columns.Add("GLCode")
            dt1.Columns.Add("GLDesc")
            dt1.Columns.Add("OBDebit")
            dt1.Columns.Add("OBCredit")
            dt1.Columns.Add("TrDebit")
            dt1.Columns.Add("TrCredit")
            dt1.Columns.Add("ClDebit")
            dt1.Columns.Add("ClCredit")
            dt1.Columns.Add("Total")

            sSql = "Select Distinct(CLM_ID),CLM_Head,CLM_GroupID,CLM_SubGroupID,CLM_GLLedger,a.gl_Desc As GLDesc,a.gl_GlCode As GLCode,b.gl_Desc As SubGlDesc,b.gl_GlCode As SubGLCode 
                    From CustomerGL_Linkage_Master Left Join Chart_Of_Accounts a on a.gl_ID=CLM_GroupID
                    Left Join Chart_Of_Accounts b on b.gl_ID=CLM_SubGroupID Where CLM_CompID=" & iCompID & " And CLM_CustID=" & iCustID & " And CLM_OrgID=" & iIndType & " Order By CLM_Head"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    dRow("GLCode") = dt.Rows(i)("GLCode")
                    dRow("GLDesc") = dt.Rows(i)("GLDesc")
                    dt1.Rows.Add(dRow)

                    dRow = dt1.NewRow()
                    dRow("GLCode") = dt.Rows(i)("SubGLCode")
                    dRow("GLDesc") = dt.Rows(i)("SubGlDesc")
                    dt1.Rows.Add(dRow)

                    sGL = dt.Rows(i)("CLM_GLLedger")
                    If sGL.StartsWith(",") Then
                        sGL = sGL.Remove(0, 1)
                    End If
                    If sGL.EndsWith(",") Then
                        sGL = sGL.Remove(Len(sGL) - 1, 1)
                    End If
                    dtCustCOA = objDBL.SQLExecuteDataSet(sNameSpace, "Select CC_GL,CC_GLCode,CC_GLDesc,CC_OBDebit,CC_OBCredit,CC_TrDebit,CC_TrCredit From Customer_COA Where CC_GL in (" & sGL & ") And CC_CompID=" & iCompID & " And CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " ").Tables(0)
                    If dtCustCOA.Rows.Count > 0 Then
                        For j = 0 To dtCustCOA.Rows.Count - 1
                            dRow = dt1.NewRow()
                            dRow("GLCode") = dtCustCOA.Rows(j)("CC_GLCode")
                            dRow("GLDesc") = dtCustCOA.Rows(j)("CC_GLDesc")
                            dRow("OBDebit") = dtCustCOA.Rows(j)("CC_OBDebit")
                            dRow("OBCredit") = dtCustCOA.Rows(j)("CC_OBCredit")
                            dRow("TrDebit") = dtCustCOA.Rows(j)("CC_TrDebit")
                            dRow("TrCredit") = dtCustCOA.Rows(j)("CC_TrCredit")
                            dRow("ClDebit") = Convert.ToDouble(dRow("OBDebit")) + Convert.ToDouble(dRow("TrDebit"))
                            dRow("ClCredit") = Convert.ToDouble(dRow("OBCredit")) + Convert.ToDouble(dRow("TrCredit"))
                            dt1.Rows.Add(dRow)

                            dtCustCOASubGL = objDBL.SQLExecuteDataSet(sNameSpace, "Select CC_GL,CC_GLCode,CC_GLDesc,CC_OBDebit,CC_OBCredit,CC_TrDebit,CC_TrCredit From Customer_COA Where CC_Head=3 And CC_Parent=" & dtCustCOA.Rows(j)("CC_GL") & " And CC_CompID=" & iCompID & " And CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " ").Tables(0)
                            If dtCustCOASubGL.Rows.Count > 0 Then
                                For k = 0 To dtCustCOASubGL.Rows.Count - 1
                                    dRow = dt1.NewRow()
                                    dRow("GLCode") = dtCustCOASubGL.Rows(k)("CC_GLCode")
                                    dRow("GLDesc") = dtCustCOASubGL.Rows(k)("CC_GLDesc")
                                    dRow("OBDebit") = dtCustCOASubGL.Rows(k)("CC_OBDebit")
                                    dRow("OBCredit") = dtCustCOASubGL.Rows(k)("CC_OBCredit")
                                    dRow("TrDebit") = dtCustCOASubGL.Rows(k)("CC_TrDebit")
                                    dRow("TrCredit") = dtCustCOASubGL.Rows(k)("CC_TrCredit")
                                    dRow("ClDebit") = Convert.ToDouble(dRow("OBDebit")) + Convert.ToDouble(dRow("TrDebit"))
                                    dRow("ClCredit") = Convert.ToDouble(dRow("OBCredit")) + Convert.ToDouble(dRow("TrCredit"))
                                    dt1.Rows.Add(dRow)
                                Next
                            End If

                        Next
                    End If
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
