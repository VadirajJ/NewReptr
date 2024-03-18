Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsScheduleLinkage
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral
    Public Function LoadGroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal iCustId As Integer, ByVal iOrgID As Integer) As DataSet 'Vijayalakshmi 14-11-2019 Additional inculded icustID and iOrgId
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
            'sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 order by gl_id"
            sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustId & " order by gl_id" 'Vijayalakshmi 14-11-2019
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGroup(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iCustId As Integer, ByVal iOrgID As Integer) As DataSet 'Vijayalakshmi 14-11-2019 Additional inculded icustID and iOrgId
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_AccHead = " & iHead & " and "
            'sSql = sSql & "gl_head = 1 and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0"
            sSql = sSql & "gl_head = 1 and gl_CompId =" & iACID & " And gl_id <> 0 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustId & ""
            If iParent <> 0 Then
                sSql = sSql & " and gl_Parent =" & iParent & ""
            End If
            sSql = sSql & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGeneralLedger(ByVal sAC As String, ByVal iACID As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select gl_ID,gl_Desc From Chart_Of_Accounts Where gl_AccHead = " & iHead & " and gl_head in(2,3) and gl_CompID=" & iACID & " And gl_id <> 0 And gl_OrgTypeID=0 and gl_CustID=0 and gl_delflag ='C' order by gl_ID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveScheduleLinkageMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal sGeneralLedger As String, ByVal iHead As Integer, ByVal iNote As Integer, ByVal iIPAddress As String, ByVal iOrgID As Integer, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Dim iMaxId As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & " and SLM_GroupID=" & iGroup & " and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_GLId=" & iGL & " and SLM_CompID =" & iACID & " and SLM_OrgID =" & iOrgID & " and SLM_CustID =" & iCustID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                sSql = "" : sSql = "Update Schedule_Linkage_Master set SLM_GLLedger='" & sGeneralLedger & "',SLM_NoteNo =" & iNote & ",SLM_Operation='U',SLM_IPAddress='" & iIPAddress & "' where "
                sSql = sSql & "SLM_Head =" & iHead & " and SLM_GroupID=" & iGroup & " and "
                sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_GLId=" & iGL & " and SLM_CompID =" & iACID & " and SLM_OrgID =" & iOrgID & " and SLM_CustID=" & iCustID & " " 'and SLM_YearID =" & iYearID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            Else
                iMaxId = objDBL.SQLExecuteScalar(sAC, "Select IsNull(MAX(SLM_ID),0)+1 from Schedule_Linkage_Master")
                sSql = "" : sSql = "Insert into Schedule_Linkage_Master(SLM_ID,SLM_Head,SLM_GroupID,SLM_SubGroupID,"
                sSql = sSql & "SLM_GLId,SLM_GLLedger,SLM_CreatedBy,"
                sSql = sSql & "SLM_CreatedOn,SLM_Status,SLM_CompID,SLM_NoteNo,SLM_Operation,SLM_IPAddress,SLM_OrgID,SLM_CustID)"
                sSql = sSql & "Values(" & iMaxId & "," & iHead & "," & iGroup & "," & iSubGroup & ","
                sSql = sSql & "" & iGL & ",'" & sGeneralLedger & "'," & iUserID & ","
                sSql = sSql & "GetDate(),'A'," & iACID & "," & iNote & ",'C','" & iIPAddress & "'," & iOrgID & "," & iCustID & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
            dr.Close()
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSavedInventoryDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iOrgID As Integer, ByVal iCustID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer)
        Dim sSql As String = "", sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head = " & iHead & " and SLM_GroupID=" & iGroup & " and SLM_SubGroupID=" & iSubGroup & " and"
            sSql = sSql & " SLM_CompID =" & iACID & " And SLM_Status ='A' and SLM_OrgID =" & iOrgID & " and SLM_CustID=" & iCustID & " " ' and SLM_YearID =" & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sStr = sStr & "," & dt.Rows(i)("SLM_GLLedger")
                Next
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function GetSavedGLS(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer, ByVal iOrgID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim sStr As String = ""
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & " and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_CompID =" & iACID & " and SLM_OrgID =" & iOrgID & " and SLM_CustID=" & iCustID & " " 'and SLM_YearID =" & iYearID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sStr = dr("SLM_GLLedger")
            End If
            dr.Close()
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetNoteNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer, ByVal iOrgID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = "", sStr As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & "  and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_CompID =" & iACID & " and SLM_OrgID =" & iOrgID & " and SLM_CustID=" & iCustID & " " 'and SLM_YearID =" & iYearID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("SLM_GLLedger")) = False Then
                    If dr("SLM_GLLedger") <> "" Then
                        If IsDBNull(dr("SLM_NoteNo")) = False Then
                            sStr = dr("SLM_NoteNo")
                        Else
                            sStr = ""
                        End If
                    End If
                End If
            End If
            dr.Close()
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSavedGLDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer, ByVal iOrgID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = "", sStr As String = "", sLedger As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & " and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_CompID =" & iACID & " and SLM_OrgID =" & iOrgID & " and SLM_CustID=" & iCustID & " " 'and SLM_YearID =" & iYearID & ""

            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sStr = dr("SLM_GLLedger")
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
                sSql = "Select gl_ID,gl_Desc From Chart_Of_Accounts Where gl_id In(" & sLedger & ") And gl_CompID=" & iACID & " And gl_id <> 0 And gl_OrgTypeID=" & iOrgID & " and gl_CustID=" & iCustID & " order by gl_id"
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteGeneralLedger(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer, ByVal sGeneralLedger As String, ByVal iIPAddress As String, ByVal iOrgID As Integer, ByVal iCustId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Update Schedule_Linkage_Master set SLM_GLLedger='" & sGeneralLedger & "',SLM_Operation='D',SLM_IPAddress='" & iIPAddress & "' where "
            sSql = sSql & "SLM_SubGroupId=" & iSubGroup & " and SLM_CompID =" & iACID & " and SLM_OrgID =" & iOrgID & " and SLM_CustId=" & iCustId & "" 'and SLM_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
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
    Public Function LoadCustomerGL(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer, ByVal iHead As Integer, ByVal iSubGL As Integer)
        Dim sSql As String
        Try
            'sSql = "Select CC_GL,CC_GLDesc from Customer_COA Where CC_Head=2 And CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_YearID=" & iYearID & " And CC_CompID=" & iACID & " Order By CC_GLDesc"
            ' sSql = "Select CC_GL,CC_GLDesc from Customer_COA Where CC_Head in(2) And CC_AccHead=" & iHead & " And CC_Parent=" & iSubGL & " And CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " And CC_YearID=" & iYearID & " And CC_CompID=" & iACID & " Order By CC_GLDesc" 'Vijayalakshmi 07-12-19 commented because Now Gl is Fetching From chart_of_accounts
            sSql = "Select gl_id as CC_GL , gl_desc as CC_GLDesc from Chart_Of_Accounts where gl_head in(2) and gl_acchead=" & iHead & " and gl_Parent=" & iSubGL & " And gl_CustID=" & iCustID & " And gl_OrgTypeID=" & iIndType & " And gl_CompID=" & iACID & " Order By gl_id "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Cust_Id,Cust_Name from SAD_CUSTOMER_MASTER Where cust_Compid=" & iACID & " and CUST_DelFlg = 'A' order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUnGroupedGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer, ByVal iYearId As Integer) As String
        Dim dt, dt1 As New DataTable
        Dim sSql As String = ""
        Dim sStr As String = ""
        Dim sDesc As String = ""
        Dim sGL As String = ""
        Try
            GetUnGroupedGL = ""
            sSql = "" : sSql = "Select SLM_GLLedger From Schedule_Linkage_Master Where SLM_CompID=" & iCompID & " And SLM_OrgID=" & iIndType & " And SLM_CustID=" & iCustID & " "
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then

                For j = 0 To dt1.Rows.Count - 1
                    If dt1.Rows(j)("SLM_GLLedger") <> "" Then
                        sGL = dt1.Rows(j)("SLM_GLLedger")
                        If sGL.StartsWith(",") Then
                            sGL = sGL.Remove(0, 1)
                        End If
                        If sGL.EndsWith(",") Then
                            sGL = sGL.Remove(Len(sGL) - 1, 1)
                        End If
                        sDesc = sDesc & "," & sGL
                    End If
                Next
                If sDesc.StartsWith(",,") Then
                    sDesc = sDesc.Remove(0, 2)
                End If
                If sDesc.StartsWith(",") Then
                    sDesc = sDesc.Remove(0, 1)
                End If
                If sDesc.EndsWith(",") Then
                    sDesc = sDesc.Remove(Len(sDesc) - 1, 1)
                End If
                If sDesc <> "" Then
                    sSql = "" : sSql = "Select * From Chart_of_Accounts Where gl_Parent=0 and gl_Head=2 and gl_CustID=" & iCustID & " And gl_OrgTypeID=" & iIndType & " and gl_CompID=" & iCompID & "  And gl_id Not In (" & sDesc & ") "
                Else
                    sSql = "" : sSql = "Select * From Chart_of_Accounts Where gl_Parent=0 and gl_Head=2 and gl_CustID=" & iCustID & " And gl_OrgTypeID=" & iIndType & " and gl_CompID=" & iCompID & "  And gl_id Not In (0) "
                End If
                ' sSql = "" : sSql = "Select * From Customer_COA Where CC_Parent=0 and CC_Head=2 and CC_CustID=" & iCustID & " And CC_IndType=" & iIndType & " and CC_CompID=" & iCompID & " And CC_YearID=" & iYearId & " And CC_GL Not In (" & sDesc & ") " ' Vijayalakshmi 07-12-19 Commented becuase gls are saved in COA and Fectching from COA

                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        'sStr = sStr & "," & dt.Rows(i)("CC_GLDesc")
                        sStr = sStr & "," & dt.Rows(i)("GL_Desc")
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
            End If
            Return GetUnGroupedGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLinkageGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iIndType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1, dtCustCOA, dtCustCOASubGL As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = ""
        Try
            dt1.Columns.Add("GLCode")
            dt1.Columns.Add("GLDesc")
            dt1.Columns.Add("NoteNo")
            dt1.Columns.Add("OBDebit")
            dt1.Columns.Add("OBCredit")
            dt1.Columns.Add("TrDebit")
            dt1.Columns.Add("TrCredit")
            dt1.Columns.Add("ClDebit")
            dt1.Columns.Add("ClCredit")

            sSql = "Select Distinct(SLM_ID),SLM_Head,SLM_GroupID,SLM_SubGroupID,SLM_GLLedger,SLM_NoteNo,a.gl_Desc As GLDesc,a.gl_GlCode As GLCode,b.gl_Desc As SubGlDesc,b.gl_GlCode As SubGLCode 
                    From Schedule_Linkage_Master Left Join Chart_Of_Accounts a on a.gl_ID=SLM_GroupID
                    Left Join Chart_Of_Accounts b on b.gl_ID=SLM_SubGroupID Where SLM_CompID=" & iCompID & " And SLM_CustID=" & iCustID & " And SLM_OrgID=" & iIndType & " Order By SLM_NoteNo"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    dRow("GLCode") = dt.Rows(i)("GLCode")
                    dRow("GLDesc") = dt.Rows(i)("GLDesc")
                    dRow("NoteNo") = ""
                    dt1.Rows.Add(dRow)

                    dRow = dt1.NewRow()
                    dRow("GLCode") = dt.Rows(i)("SubGLCode")
                    dRow("GLDesc") = dt.Rows(i)("SubGlDesc")
                    dRow("NoteNo") = dt.Rows(i)("SLM_NoteNo")
                    dt1.Rows.Add(dRow)

                    sGL = dt.Rows(i)("SLM_GLLedger")
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
                            dRow("NoteNo") = ""
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
                                    dRow("NoteNo") = ""
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
