Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDataCapture
    Private objDBL As New DatabaseLayer.DBHelper
    'Dim objGen As New clsFASGeneral

    Public Function LoadAccZone(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent=0 and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccArea(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccRgn As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccBrnch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccarea As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3 "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & " order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_head=3 order by gl_AccHead"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetchartofAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CompID=" & iCompID & " and gl_DelFlag ='C'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildTable() As DataTable
        Dim dt As New DataTable
        Dim dc As New DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Balance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebitORCredit", GetType(Integer))
            dt.Columns.Add(dc)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPaymentsMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer, ByVal dAmount As Double, ByVal iDbOrCr As Integer, ByVal dtPayment As DataTable, ByVal dtCOA As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt = BuildTable()

            dr = dt.NewRow
            dr("HeadID") = iHead
            dr("GLID") = iGLID
            dr("SubGLID") = iSubGL
            dr("DebitORCredit") = iDbOrCr

            If iGLID > 0 Then
                Dim dtDGL As New DataTable
                Dim DVGLCODE As New DataView(dtCOA)
                DVGLCODE.RowFilter = "Gl_id=" & iGLID
                dtDGL = DVGLCODE.ToTable

                dr("GLCode") = dtDGL.Rows(0)("gl_glcode")
                dr("GLDescription") = dtDGL.Rows(0)("gl_desc")

            Else
                dr("GLCode") = "" : dr("GLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("GLID") = "0"
            End If


            If iSubGL > 0 Then
                Dim dtDSUBGL As New DataTable
                Dim DVSUBGLCODE As New DataView(dtCOA)
                DVSUBGLCODE.RowFilter = "Gl_id=" & iSubGL
                dtDSUBGL = DVSUBGLCODE.ToTable

                dr("SubGL") = dtDSUBGL.Rows(0)("gl_glcode")
                dr("SubGLDescription") = dtDSUBGL.Rows(0)("gl_desc")
            Else
                dr("SubGL") = "" : dr("SubGLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("SubGLID") = "0"
            End If


            Dim iCount As Integer = 0
            iCount = dtPayment.Rows.Count + 1

            If iDbOrCr = 1 Then
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iGLID)
                End If

                dr("Debit") = dAmount
                dr("Credit") = 0.00
                dr("DebitOrCredit") = 1
            Else
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iGLID)
                End If
                dr("Debit") = 0.00
                dr("Credit") = dAmount
                dr("DebitOrCredit") = 2
            End If
            dt.Rows.Add(dr)

            If dtPayment.Rows.Count > 0 Then
                dtPayment.Merge(dt, True, MissingSchemaAction.Ignore)
            Else
                dtPayment.Merge(dt)
            End If
            'dtPayment.Merge(dt)
            Return dtPayment
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sColumn As String, ByVal iGlID As Integer) As Double
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dDebitOrCredit As Double = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from acc_Opening_Balance where Opn_GLID =" & iGlID & " and Opn_YearID =" & iYearID & " and Opn_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dDebitOrCredit = dt.Rows(0)(sColumn).ToString()
            End If
            Return dDebitOrCredit
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCust As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "select CBN_ID from EDT_CABINET where CBN_NAME='" & sCust & "'"
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubCabID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSub As String, ByVal iCabId As Integer) As Integer
        Dim sSql As String = ""
        Dim iRet As Integer
        Try
            sSql = "" : sSql = "select CBN_ID from EDT_CABINET where CBN_NAME='" & sSub & "' and CBN_PARENT=" & iCabId & " "
            iRet = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFoldID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sFold As String, ByVal iSubCabId As Integer) As Integer
        Dim sSql As String = ""
        Dim iRet As Integer
        Try
            sSql = "" : sSql = "select FOL_FOLID from EDT_FOLDER where FOL_NAME='" & sFold & "' and FOL_CABINET=" & iSubCabId & " "
            iRet = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadFileNames(ByVal sAC As String, ByVal iBaseID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select pge_basename,pge_OrignalFileName from EDT_PAGE where pge_status <> 'X' and Pge_Details_Id=" & iBaseID & " ORDER BY PGE_PAGENO"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadUser(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Usr_ID,Usr_FullName from sad_userdetails where Usr_CompID=" & iACId & " and USR_DelFlag='A'"
            If iUserID > 0 Then
                sSql = sSql & " And Usr_ID !=" & iUserID & ""
            End If
            sSql = sSql & " order by Usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetEmailSentUserID(ByVal sAC As String, ByVal iACId As Integer, ByVal sUserMailID As String) As Integer
        Dim sSql As String = ""
        Dim iUserID As Integer = 0
        Try
            sSql = "Select Case When usr_Email Is Null then 0 else usr_Email End As usr_Email from sad_USERDETAILS Where Usr_CompID=" & iACId & " and USR_DelFlag='A' And usr_Email='" & sUserMailID & "'"
            iUserID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iUserID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadUserEmailIDs(ByVal sAC As String, ByVal iACId As Integer, ByVal sUserID As String) As String
        Dim sSql As String = "", sEmailIds As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Case When usr_Email Is NULL Then '' else usr_Email End As usr_Email From Sad_UserDetails Where"
            If sUserID <> "" Then
                sSql = sSql & " Usr_ID IN (" & sUserID & ") And"
            End If
            sSql = sSql & " Usr_CompID=" & iACId & " and USR_DelFlag='A'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sEmailIds = sEmailIds & "," & dt.Rows(i)("usr_Email")
                Next
            End If
            Return sEmailIds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetLoginUserEmailID(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String = "", sEmailId As String = ""
        Try
            sSql = "Select Case When usr_Email Is NULL then '' else usr_Email End As usr_Email From Sad_UserDetails where Usr_CompID=" & iACId & " and USR_DelFlag='A'"
            If iUserID > 0 Then
                sSql = sSql & " And Usr_ID=" & iUserID & ""
            End If
            sSql = sSql & " order by Usr_FullName"
            sEmailId = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sEmailId
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub SaveSentEmailDetails(ByVal sAC As String, ByVal iMstPKID As Integer, ByVal iYearID As Integer, ByVal sFormName As String, ByVal sFromEmailID As String,
                                    ByVal sToEmailIDs As String, ByVal sCCDetails As String, ByVal sSubject As String, ByVal sBody As String, ByVal sIsMailSent As String,
                                    ByVal sAttachedPath As String, ByVal sAttachedDocIDs As String, ByVal iEmailSentUserID As Integer, ByVal iUserID As Integer,
                                    ByVal sIPaddress As String, ByVal iCompID As Integer)
        Dim sSql As String = ""
        Dim iPKID As Integer = 0
        Try
            iPKID = objDBL.SQLExecuteScalarInt(sAC, "Select IsNull(Max(EMD_ID),0)+1 from GRACe_EMailSent_Details")
            sSql = "Insert Into GRACe_EMailSent_Details(EMD_ID,EMD_MstPKID,EMD_YearID,EMD_FormName,EMD_FromEmailID,EMD_ToEmailIDs,EMD_CCEmailIDs,"
            sSql = sSql & " EMD_Subject,EMD_Body,EMD_EMailStatus,EMD_SentUsrID,EMD_SentOn,EMD_CreatedBy,EMD_CreatedOn,EMD_AttachedPath,"
            sSql = sSql & " EMD_AttachedDocIDs,EMD_IPAddress,EMD_CompID) Values(" & iPKID & ",'" & iMstPKID & "',"
            sSql = sSql & " " & iYearID & ",'" & sFormName & "','" & sFromEmailID & "','" & sToEmailIDs & "','" & sCCDetails & "',"
            sSql = sSql & " '" & sSubject & "','" & sBody & "','" & sIsMailSent & "'," & iEmailSentUserID & ",GetDate()," & iUserID & ",GetDate(),'" & sAttachedPath & "',"
            sSql = sSql & " '" & sAttachedDocIDs & "','" & sIPaddress & "'," & iCompID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub UpdateEdtPageStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iEdtPagePkid As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update EDT_PAGE set PGE_STATUS='S',Pge_UpdatedBy=" & iUserID & ",Pge_UpdatedOn=Getdate() where PGE_BASENAME=" & iEdtPagePkid & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
