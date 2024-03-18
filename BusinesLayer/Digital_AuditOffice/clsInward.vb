Imports DatabaseLayer
Public Structure strInward
    Private WID_PKID As Integer
    Private WID_InwardNo As String
    Private WID_MonthID As Integer
    Private WID_YearID As Integer
    Private WID_InwardDate As Date
    Private WID_InwardTime As String
    Private WID_Title As String
    Private WID_DocReferenceno As String
    Private WID_DocRecievedDate As Date
    Private WID_DateOnDocument As Date
    Private WID_ReceiptMode As Integer
    Private WID_Deptartment As Integer
    Private WID_Customer As Integer
    Private WID_ContactPerson As String
    Private WID_ContactEmailID As String
    Private WID_ContactPhNO As String
    Private WID_Remarks As String
    Private WID_AttachID As Integer
    Private WID_Status As String
    Private WID_Delflag As String
    Private WID_CreatedOn As Date
    Private WID_CreatedBy As Integer
    Private WID_UpdatedOn As Date
    Private WID_UpdatedBy As Integer
    Private WID_DeletedOn As Date
    Private WID_DeletedBy As Integer
    Private WID_RecalledOn As Date
    Private WID_RecalledBy As Integer
    Private WIM_InwardOrWorkFlow As Integer
    Private WID_sIPAdress As String
    Private WID_CompID As Integer
    Private WIM_Address As String
    Private WIM_Stage As Integer
    Private WIM_WorkFLowID As Integer
    Private WIM_WorkFLowArchiveID As Integer
    Private WIM_Progress_Status As Integer

    Public Property iWIM_Progress_Status() As Integer
        Get
            Return (WIM_Progress_Status)
        End Get
        Set(ByVal Value As Integer)
            WIM_Progress_Status = Value
        End Set
    End Property
    Public Property iWIMWorkFLowArchiveID() As Integer
        Get
            Return (WIM_WorkFLowArchiveID)
        End Get
        Set(ByVal Value As Integer)
            WIM_WorkFLowArchiveID = Value
        End Set
    End Property
    Public Property iWIMWorkFLowID() As Integer
        Get
            Return (WIM_WorkFLowID)
        End Get
        Set(ByVal Value As Integer)
            WIM_WorkFLowID = Value
        End Set
    End Property
    Public Property iWIMStage() As Integer
        Get
            Return (WIM_Stage)
        End Get
        Set(ByVal Value As Integer)
            WIM_Stage = Value
        End Set
    End Property
    Public Property iWIMInwardOrWorkFlow() As Integer
        Get
            Return (WIM_InwardOrWorkFlow)
        End Get
        Set(ByVal Value As Integer)
            WIM_InwardOrWorkFlow = Value
        End Set
    End Property
    Public Property iWIDPKID() As Integer
        Get
            Return (WID_PKID)
        End Get
        Set(ByVal Value As Integer)
            WID_PKID = Value
        End Set
    End Property

    Public Property sWIDInwardNo() As String
        Get
            Return (WID_InwardNo)
        End Get
        Set(ByVal Value As String)
            WID_InwardNo = Value
        End Set
    End Property
    Public Property sWIDsIPAdress() As String
        Get
            Return (WID_sIPAdress)
        End Get
        Set(ByVal Value As String)
            WID_sIPAdress = Value
        End Set
    End Property
    Public Property iWIDMonthID() As Integer
        Get
            Return (WID_MonthID)
        End Get
        Set(ByVal Value As Integer)
            WID_MonthID = Value
        End Set
    End Property
    Public Property iWIDYearID() As Integer
        Get
            Return (WID_YearID)
        End Get
        Set(ByVal Value As Integer)
            WID_YearID = Value
        End Set
    End Property
    Public Property dWIDInwardDate() As Date
        Get
            Return (WID_InwardDate)
        End Get
        Set(ByVal Value As Date)
            WID_InwardDate = Value
        End Set
    End Property
    Public Property sWIDInwardTime() As String
        Get
            Return (WID_InwardTime)
        End Get
        Set(ByVal Value As String)
            WID_InwardTime = Value
        End Set
    End Property
    Public Property sWIDTitle() As String
        Get
            Return (WID_Title)
        End Get
        Set(ByVal Value As String)
            WID_Title = Value
        End Set
    End Property
    Public Property sWIDDocReferenceno() As String
        Get
            Return (WID_DocReferenceno)
        End Get
        Set(ByVal Value As String)
            WID_DocReferenceno = Value
        End Set
    End Property
    Public Property dWIDDocRecievedDate() As Date
        Get
            Return (WID_DocRecievedDate)
        End Get
        Set(ByVal Value As Date)
            WID_DocRecievedDate = Value
        End Set
    End Property
    Public Property dWIDDateOnDocument() As Date
        Get
            Return (WID_DateOnDocument)
        End Get
        Set(ByVal Value As Date)
            WID_DateOnDocument = Value
        End Set
    End Property
    Public Property iWIDReceiptMode() As Integer
        Get
            Return (WID_ReceiptMode)
        End Get
        Set(ByVal Value As Integer)
            WID_ReceiptMode = Value
        End Set
    End Property
    Public Property iWIDDeptartment() As Integer
        Get
            Return (WID_Deptartment)
        End Get
        Set(ByVal Value As Integer)
            WID_Deptartment = Value
        End Set
    End Property
    Public Property iWIDCustomer() As Integer
        Get
            Return (WID_Customer)
        End Get
        Set(ByVal Value As Integer)
            WID_Customer = Value
        End Set
    End Property
    Public Property sWIDContactPerson() As String
        Get
            Return (WID_ContactPerson)
        End Get
        Set(ByVal Value As String)
            WID_ContactPerson = Value
        End Set
    End Property
    Public Property sWIDContactEmailID() As String
        Get
            Return (WID_ContactEmailID)
        End Get
        Set(ByVal Value As String)
            WID_ContactEmailID = Value
        End Set
    End Property
    Public Property sWIDContactPhNO() As String
        Get
            Return (WID_ContactPhNO)
        End Get
        Set(ByVal Value As String)
            WID_ContactPhNO = Value
        End Set
    End Property
    Public Property sWID_Remarks() As String
        Get
            Return (WID_Remarks)
        End Get
        Set(ByVal Value As String)
            WID_Remarks = Value
        End Set
    End Property
    Public Property iWID_AttachID() As Integer
        Get
            Return (WID_AttachID)
        End Get
        Set(ByVal Value As Integer)
            WID_AttachID = Value
        End Set
    End Property
    Public Property sWID_Status() As String
        Get
            Return (WID_Status)
        End Get
        Set(ByVal Value As String)
            WID_Status = Value
        End Set
    End Property
    Public Property sWID_Delflag() As String
        Get
            Return (WID_Delflag)
        End Get
        Set(ByVal Value As String)
            WID_Delflag = Value
        End Set
    End Property
    Public Property dWID_CreatedOn() As Date
        Get
            Return (WID_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            WID_CreatedOn = Value
        End Set
    End Property
    Public Property iWID_CreatedBy() As Integer
        Get
            Return (WID_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            WID_CreatedBy = Value
        End Set
    End Property
    Public Property dWID_UpdatedOn() As Date
        Get
            Return (WID_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            WID_UpdatedOn = Value
        End Set
    End Property
    Public Property iWID_UpdatedBy() As Integer
        Get
            Return (WID_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            WID_UpdatedBy = Value
        End Set
    End Property
    Public Property dWID_DeletedOn() As Date
        Get
            Return (WID_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            WID_DeletedOn = Value
        End Set
    End Property
    Public Property iWID_DeletedBy() As Integer
        Get
            Return (WID_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            WID_DeletedBy = Value
        End Set
    End Property
    Public Property iWID_CompID() As Integer
        Get
            Return (WID_CompID)
        End Get
        Set(ByVal Value As Integer)
            WID_CompID = Value
        End Set
    End Property
    Public Property sWIMAddress() As String
        Get
            Return (WIM_Address)
        End Get
        Set(ByVal Value As String)
            WIM_Address = Value
        End Set
    End Property
End Structure
Public Structure strInwardHistory
    Private WIMH_PKID As Integer
    Private WIMH_InwardPKID As Integer
    Private WIMH_User As Integer
    Private WIMH_SentTOID As Integer
    Private WIMH_Remarks As String
    Private WIMH_LineNo As Integer
    Private WIMH_Stage As Integer
    Private WIMH_CompID As Integer
    Private WIMH_ReplyOrForward As Integer
    Public Property iWIMH_PKID() As Integer
        Get
            Return (WIMH_PKID)
        End Get
        Set(ByVal Value As Integer)
            WIMH_PKID = Value
        End Set
    End Property
    Public Property iWIMH_InwardPKID() As Integer
        Get
            Return (WIMH_InwardPKID)
        End Get
        Set(ByVal Value As Integer)
            WIMH_InwardPKID = Value
        End Set
    End Property
    Public Property iWIMH_User() As Integer
        Get
            Return (WIMH_User)
        End Get
        Set(ByVal Value As Integer)
            WIMH_User = Value
        End Set
    End Property
    Public Property iWIMH_SentTOID() As Integer
        Get
            Return (WIMH_SentTOID)
        End Get
        Set(ByVal Value As Integer)
            WIMH_SentTOID = Value
        End Set
    End Property
    Public Property sWIMH_Remarks() As String
        Get
            Return (WIMH_Remarks)
        End Get
        Set(ByVal Value As String)
            WIMH_Remarks = Value
        End Set
    End Property
    Public Property iWIMH_LineNo() As Integer
        Get
            Return (WIMH_LineNo)
        End Get
        Set(ByVal Value As Integer)
            WIMH_LineNo = Value
        End Set
    End Property
    Public Property iWIMH_Stage() As Integer
        Get
            Return (WIMH_Stage)
        End Get
        Set(ByVal Value As Integer)
            WIMH_Stage = Value
        End Set
    End Property
    Public Property iWIMH_CompID() As Integer
        Get
            Return (WIMH_CompID)
        End Get
        Set(ByVal Value As Integer)
            WIMH_CompID = Value
        End Set
    End Property
    Public Property iWIMH_ReplyOrForward() As Integer
        Get
            Return (WIMH_ReplyOrForward)
        End Get
        Set(ByVal Value As Integer)
            WIMH_ReplyOrForward = Value
        End Set
    End Property
End Structure
Public Class clsInward
    Dim objDBL As New DBHelper
    Dim objGen As New clsGRACeGeneral
    Dim objGenFun As New clsGeneralFunctions
    Public Function GetCompanyCode(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Company_Code From Trace_CompanyDetails Where Company_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckInwardCreator(ByVal sAC As String, ByVal iACID As Integer, ByVal iUser As Integer, ByVal iInward As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from wf_inward_Masters where WIM_PkID=" & iInward & " And WIM_CreatedBy=" & iUser & " And WIM_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckInwardPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iInward As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = " Select *,Org_name from WF_Inward_Masters Left Join WF_Inward_Masters_history On WIMH_Stage=WIM_Stage And WIMH_InwardPKID=WIM_PKID And WIMH_CompID=" & iACID & ""
            sSql = sSql & " and WIMH_SentTOID=" & iUserID & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & ""
            sSql = sSql & " Where (WIMH_SentTOID=" & iUserID & " Or WIM_CreatedBy=" & iUserID & ") And WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 "
            sSql = sSql & " And WIM_PKID=" & iInward & " Order by WIM_PKID Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUser(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Usr_ID,Usr_FullName from sad_userdetails where Usr_CompID=" & iACId & " and USR_DelFlag='A'"
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
    Public Function LoadCustomerUser(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Usr_ID,Usr_FullName from sad_userdetails where Usr_CompID=" & iACId & " and USR_DelFlag='A' And usr_Node=0"
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
    Public Function LoadSelectedInwardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iInwardID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select * from WF_Inward_Masters where WIM_PKID=" & iInwardID & " and WIM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSelectedUserDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iInwardID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select WIMH_SentTOID from WF_Inward_Masters_history where WIMH_InwardPKID=" & iInwardID & " and WIMH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDeptUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iDeptID As Integer, ByVal sUserID As String, ByVal iInwardID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Usr_ID, Usr_FullName, USR_LoginName from sad_userdetails Left Join WF_Inward_Masters On WIM_CreatedBy<>Usr_ID And WIM_CompID=" & iACID & ""
            sSql = sSql & " Where Usr_CompID=" & iACID & " And USR_DelFlag='A' And usr_OrgnId=" & iDeptID & ""
            sSql = sSql & " And usr_id not in(" & sUserID & ") And WIM_PKID=" & iInwardID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("Ext")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From edt_attachments where ATCH_CompID=" & iACID & " And "
            sSql = sSql & " ATCH_ID = " & iAttachID & " AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("Ext") = dt.Rows(i)("ATCH_EXT")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objGen.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objGen.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function GetStageFromInwardHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iInwardID As Integer, ByVal sType As String) As Integer
        Dim sSql As String = "", sSql1 As String = ""
        Dim iSent As Integer = 0, iUser As Integer = 0
        Try
            If sType = "Sent" Then
                sSql = "Select Max(WIMH_Stage) from WF_Inward_Masters_history where WIMH_CompID=" & iACID & " and WIMH_InwardPKID=" & iInwardID & " And WIMH_SentTOID=" & iUserID & ""
                iSent = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iSent = 0 Then
                    sSql1 = "Select Max(WIMH_Stage) from WF_Inward_Masters_history where WIMH_CompID=" & iACID & " and WIMH_InwardPKID=" & iInwardID & " And WIMH_User=" & iUserID & ""
                    iUser = objDBL.SQLExecuteScalarInt(sAC, sSql1)
                    iSent = iUser
                    If iUser = 0 Then
                        iSent = iSent + 1
                    End If
                Else
                    iSent = iSent + 1
                End If
            Else
                sSql = "Select Max(WIMH_Stage) from WF_Inward_Masters_history where WIMH_CompID=" & iACID & " and WIMH_InwardPKID=" & iInwardID & ""
                iSent = objDBL.SQLExecuteScalarInt(sAC, sSql)
            End If
            Return iSent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentMonthID(ByVal sAC As String)
        Dim sSql As String = ""
        Try
            sSql = "Select Month(GETDATE())"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateInwardStage(ByVal sAC As String, ByVal iACID As Integer, ByVal iStage As Integer, ByVal iInwardID As Integer, ByVal iAttachID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update WF_Inward_Masters Set WIM_AttachID=" & iAttachID & ", WIM_Stage=" & iStage & " where WIM_CompID=" & iACID & " and WIM_PKID=" & iInwardID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetSentToIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iInwardID As Integer, ByVal inWardStage As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select WIMH_SentTOID from WF_Inward_Masters_history where WIMH_InwardPKID=" & iInwardID & " and WIMH_CompID=" & iACID & ""
            If inWardStage = 0 Then
                sSql = sSql & " and WIMH_Stage=1"
            Else
                sSql = sSql & " and WIMH_Stage=" & inWardStage & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInwardStage(ByVal sAC As String, ByVal iACID As Integer, ByVal iInwardID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select WIM_Stage from WF_Inward_Masters where WIM_PKID=" & iInwardID & " And WIM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllInwardHistoryDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iInwardID As Integer) As DataTable
        Dim dtDisplay As New DataTable, dt As New DataTable
        Dim dtStage As New DataTable
        Dim sSql As String = "", aSql As String = ""
        Dim dRow As DataRow
        Dim sUser As String = "", sSentToUser As String = ""
        Try
            dtDisplay.Columns.Add("UserName")
            dtDisplay.Columns.Add("SentTO")
            dtDisplay.Columns.Add("Datetime")
            dtDisplay.Columns.Add("Remarks")
            dtDisplay.Columns.Add("Flag")


            sSql = "" : sSql = "Select distinct(wimh_stage) from WF_Inward_Masters_history where WIMH_InwardPKID=" & iInwardID & " And WIMH_CompID=" & iACID & ""
            dtStage = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtStage.Rows.Count > 0 Then
                For i = 0 To dtStage.Rows.Count - 1
                    aSql = "" : aSql = "Select * from WF_Inward_Masters_history where WIMH_InwardPKID=" & iInwardID & " And "
                    aSql = aSql & "WIMH_CompID = " & iACID & " and wimh_stage =" & dtStage.Rows(i)("wimh_stage") & " Order by WIMH_PKID"
                    dt = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dt.Rows.Count > 0 Then
                        sUser = "" : sSentToUser = ""
                        For j = 0 To dt.Rows.Count - 1
                            sUser = objGen.ReplaceSafeSQL(objGenFun.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(j)("WIMH_User")))
                            sSentToUser = sSentToUser & "," & objGen.ReplaceSafeSQL(objGenFun.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(j)("WIMH_SentToID")))
                        Next
                        dRow = dtDisplay.NewRow

                        dRow("UserName") = sUser

                        If sSentToUser <> "" Then
                            dRow("SentTO") = sSentToUser.Remove(0, 1)
                        End If


                        If IsDBNull(dt.Rows(0)("WIMH_Datetime")) = False Then
                            dRow("Datetime") = objGen.FormatDtForRDBMS(dt.Rows(0)("WIMH_Datetime"), "DT")
                        End If
                        If IsDBNull(dt.Rows(0)("WIMH_Remarks")) = False Then
                            dRow("Remarks") = objGen.ReplaceSafeSQL(dt.Rows(0)("WIMH_Remarks"))
                        End If

                        If IsDBNull(dt.Rows(0)("WIMH_ReplyOrForward")) = False Then
                            dRow("Flag") = objGen.ReplaceSafeSQL(dt.Rows(0)("WIMH_ReplyOrForward"))
                        End If
                        dtDisplay.Rows.Add(dRow)
                    End If
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub MarkInwardToWorkFlow(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iWorkFlow As Integer, ByVal iInward As Integer, ByVal iAtchID As Integer, ByVal sWorkFlowCommnets As String)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Update WF_Inward_Masters set WIM_InwardOrWorkFLow=1,WIM_Progress_Status=1,WIM_WorkFlowCreatedBy=" & iUserID & ","
            sSql = sSql & "WIM_WorkFlowComments='" & objGen.SafeSQL(sWorkFlowCommnets) & "',WIM_WorkFlowCreatedOn=GetDate(),"
            sSql = sSql & "WIM_WorkFLowID=" & iWorkFlow & ",WIM_AttachID=" & iAtchID & " where WIM_PkID=" & iInward & " and WIM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DocumentArchive(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserId As Integer, ByVal iInward As Integer, ByVal iAtchID As Integer)
        Dim sSql As String = ""
        Dim iWorkflow As Integer = 0
        Try
            iWorkflow = objDBL.SQLExecuteScalar(sAC, "Select WM_Id from wf_Workflow_Master where wm_wf_name='Correspondence Archive' and WM_CompID=" & iACID & "")

            sSql = "" : sSql = "Update WF_Inward_Masters set WIM_InwardOrWorkFLow=1,WIM_Progress_Status=2,WIM_WorkFlowCreatedBy=" & iUserId & ","
            sSql = sSql & "WIM_WorkFlowComments='',WIM_WorkFlowCreatedOn=GetDate(),WIM_WorkFLowID=" & iWorkflow & ",WIM_AttachID=" & iAtchID & ","
            sSql = sSql & "WIM_WorkFLowArchiveID=1 Where WIM_PkID=" & iInward & " and WIM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadReplyUserDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iInwardID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select WIMH_User from WF_Inward_Masters_history where WIMH_Stage in(select WIM_Stage from WF_Inward_Masters where wim_pkID=" & iInwardID & " and WIM_CompID=" & iACID & ") "
            sSql = sSql & " and WIMH_InwardPKID=" & iInwardID & " and WIMH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDepartment(ByVal sAC As String, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Org_node,Org_Name from Sad_Org_Structure where Org_DelFlag='A' and Org_LevelCode=3 Order By Org_Name Asc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomer(ByVal sAC As String, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select CUST_ID,CUST_NAME from SAD_CUSTOMER_MASTER where CUST_DELFLG='A' Order By CUST_NAME Asc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllInwardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iMonthID As Integer,
                                         ByVal iDeptCustID As Integer, ByVal sDeptOrCust As String) As DataTable
        Dim dtDisplay As New DataTable, dt As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Dim dtStage As New DataTable, dtPerm As New DataTable, dtUsers As New DataTable
        Dim sArchUsers As String = ""
        Dim p As Integer = 0, m As Integer = 0
        Try
            dtDisplay.Columns.Add("WIM_PKID")
            dtDisplay.Columns.Add("InwardNo")
            dtDisplay.Columns.Add("Month")
            dtDisplay.Columns.Add("DepartmentCustomer")
            dtDisplay.Columns.Add("DocumentTitle")
            dtDisplay.Columns.Add("DocumentReferenceNo")
            dtDisplay.Columns.Add("DocumentRecievedDate")
            dtDisplay.Columns.Add("Status")

            ''Check the Permission
            'sSql = "" : sSql = "Select * from Sad_UsrOrGrp_permission where Perm_ModuleID=40"
            'dtPerm = objDBL.SQLExecuteDataTable(sAC, sSql)
            'If dtPerm.Rows.Count > 0 Then
            '    For p = 0 To dtPerm.Rows.Count - 1
            '        If dtPerm.Rows(p)("Perm_PType").ToString() = "U" Then
            '            sArchUsers = sArchUsers & "," & dtPerm.Rows(p)("SGP_LevelGroupID").ToString()

            '        ElseIf dtPerm.Rows(p)("SGP_LevelGroup").ToString() = "R" Then
            '            sSql = "" : sSql = "Select * from sad_USerdetails where Usr_Designation =" & dtPerm.Rows(p)("SGP_LevelGroupID").ToString() & " and Usr_Delflag='A' and Usr_CompID=" & iACID & ""
            '            dtUsers = objDBL.SQLExecuteDataTable(sAC, sSql)
            '            If dtUsers.Rows.Count > 0 Then
            '                For m = 0 To dtUsers.Rows.Count - 1
            '                    sArchUsers = sArchUsers & "," & dtUsers.Rows(m)("Usr_ID").ToString()
            '                Next
            '            End If
            '        End If
            '    Next
            'End If

            dtStage = objDBL.SQLExecuteDataTable(sAC, "Select WIM_Stage,WIM_PKID from WF_Inward_Masters where WIM_CompID=" & iACID & " and WIM_InwardOrWorkFlow=0 order by WIM_PKID Desc")
            If dtStage.Rows.Count > 0 Then
                For j = 0 To dtStage.Rows.Count - 1
                    If IsDBNull(dtStage.Rows(j).Item("WIM_Stage")) = False Then
                        sSql = " Select WIM_PKID,WIM_InwardNo,WIM_Deptartment,WIM_Customer,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,WIM_DelFlag,WIMH_SentTOID,"
                        sSql = sSql & " Org_name,CUST_NAME,WIM_MonthID from WF_Inward_Masters"
                        sSql = sSql & " Left Join WF_Inward_Masters_history On WIMH_Stage=WIM_Stage And WIMH_InwardPKID=WIM_PKID And WIMH_SentTOID=" & iUserID & " And WIMH_CompID=" & iACID & ""
                        sSql = sSql & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & ""
                        sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On CUST_ID=WIM_Customer And CUST_CompID=" & iACID & ""
                        sSql = sSql & " Where (WIMH_SentTOID=" & iUserID & ") And WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 "
                        sSql = sSql & " And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & ""
                        If iMonthID > 0 Then
                            sSql = sSql & " And WIM_MonthID=" & iMonthID & ""
                        End If
                        If sDeptOrCust = "D" Then
                            If iDeptCustID > 0 Then
                                sSql = sSql & " And WIM_Deptartment=" & iDeptCustID & ""
                            End If
                        ElseIf sDeptOrCust = "C" Then
                            If iDeptCustID > 0 Then
                                sSql = sSql & " And WIM_Customer=" & iDeptCustID & ""
                            End If
                        End If
                        sSql = sSql & " Order by WIM_PKID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("WIM_PKID") = dt.Rows(i)("WIM_PKID")
                                dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Month") = dt.Rows(i)("WIM_MonthID")
                                If dt.Rows(i)("WIM_Deptartment") > 0 Then
                                    dRow("DepartmentCustomer") = dt.Rows(i)("Org_name")
                                ElseIf dt.Rows(i)("WIM_Customer") > 0 Then
                                    dRow("DepartmentCustomer") = dt.Rows(i)("CUST_NAME")
                                End If
                                dRow("DocumentTitle") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("DocumentReferenceNo") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                dRow("DocumentRecievedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                End If
                                If IsDBNull(dt.Rows(i)("WIMH_SentTOID")) = False Then
                                    If iUserID = dt.Rows(i)("WIMH_SentTOID") Then
                                        If dt.Rows(i)("WIM_DelFlag") = "W" Then
                                            GoTo NextLoop
                                        End If
                                    End If
                                Else
                                    If dt.Rows(i)("WIM_DelFlag") = "W" Then
                                        dRow("Status") = "Waiting For Approval"
                                    End If
                                End If
                                dtDisplay.Rows.Add(dRow)
NextLoop:                   Next
                        Else
                            'Check Permission
                            If sArchUsers.Contains(iUserID) = True Then
                                sSql = "" : sSql = "Select Distinct(WIM_pkid),WIM_InwardNo,WIM_Deptartment,WIM_Customer,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,"
                                sSql = sSql & " WIM_DelFlag,Org_Name,CUST_NAME,WIM_MonthID from WF_Inward_Masters"
                                sSql = sSql & " Left Join WF_Inward_Masters_history On WIMH_Stage=WIM_Stage And WIMH_InwardPKID=WIM_PKID And WIMH_CompID=" & iACID & ""
                                sSql = sSql & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & ""
                                sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On CUST_ID=WIM_Customer And CUST_CompID=" & iACID & ""
                                sSql = sSql & " Where WIM_CompID=" & iACID & " And WIM_InwardOrWorkFlow=0 And WIM_PKID=" & dtStage.Rows(j).Item("WIM_PKID") & ""
                                If iMonthID > 0 Then
                                    sSql = sSql & " And WIM_MonthID=" & iMonthID & ""
                                End If
                                If sDeptOrCust = "D" Then
                                    If iDeptCustID > 0 Then
                                        sSql = sSql & " And WIM_Deptartment=" & iDeptCustID & ""
                                    End If
                                ElseIf sDeptOrCust = "C" Then
                                    If iDeptCustID > 0 Then
                                        sSql = sSql & " And WIM_Customer=" & iDeptCustID & ""
                                    End If
                                End If
                                sSql = sSql & " Group By WIM_pkid,WIM_InwardNo,Org_name,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,WIM_DelFlag,Org_Name,CUST_NAME,WIM_MonthID"
                                sSql = sSql & " Order By WIM_PKID Desc"
                                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                                If dt.Rows.Count > 0 Then
                                    For i = 0 To dt.Rows.Count - 1
                                        dRow = dtDisplay.NewRow
                                        dRow("WIM_PKID") = dt.Rows(i)("WIM_PKID")
                                        dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                        dRow("Month") = dt.Rows(i)("WIM_MonthID")
                                        If dt.Rows(i)("WIM_Deptartment") = 0 Then
                                            dRow("DepartmentCustomer") = dt.Rows(i)("Org_name")
                                        ElseIf dt.Rows(i)("WIM_Customer") = 0 Then
                                            dRow("DepartmentCustomer") = dt.Rows(i)("CUST_NAME")
                                        End If
                                        dRow("DocumentTitle") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                        dRow("DocumentReferenceNo") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                        dRow("DocumentRecievedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                                        If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                            dRow("Status") = "Activated"
                                        ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                            dRow("Status") = "De-Activated"
                                        End If
                                        dtDisplay.Rows.Add(dRow)
NextPermLoop:                       Next
                                End If
                            End If
                        End If
                    Else
                        sSql = "Select WIM_PKID,WIM_InwardNo,WIM_Title,WIM_DocReferenceno,WIM_DocRecievedDate,WIM_DelFlag,Org_name,CUST_NAME,WIM_MonthID,"
                        sSql = sSql & " WIM_Deptartment,WIM_Customer From WF_Inward_Masters"
                        sSql = sSql & " Left Join sad_org_structure On org_node=WIM_Deptartment And org_LevelCode=3 And Org_CompID=" & iACID & ""
                        sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On CUST_ID=WIM_Customer And CUST_CompID=" & iACID & ""
                        sSql = sSql & " Where WIM_CreatedBy = " & iUserID & " And WIM_CompID = " & iACID & " And WIM_InwardOrWorkFlow = 0 And WIM_Stage Is NULL"
                        sSql = sSql & " And WIM_PKID= " & dtStage.Rows(j).Item("WIM_PKID") & ""
                        If iMonthID > 0 Then
                            sSql = sSql & " And WIM_MonthID=" & iMonthID & ""
                        End If
                        If sDeptOrCust = "D" Then
                            If iDeptCustID > 0 Then
                                sSql = sSql & " And WIM_Deptartment=" & iDeptCustID & ""
                            End If
                        ElseIf sDeptOrCust = "C" Then
                            If iDeptCustID > 0 Then
                                sSql = sSql & " And WIM_Customer=" & iDeptCustID & ""
                            End If
                        End If
                        sSql = sSql & " Order by WIM_PKID Desc"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For i = 0 To dt.Rows.Count - 1
                                dRow = dtDisplay.NewRow
                                dRow("WIM_PKID") = dt.Rows(i)("WIM_PKID")
                                dRow("InwardNo") = dt.Rows(i)("WIM_InwardNo")
                                dRow("Month") = dt.Rows(i)("WIM_MonthID")
                                If dt.Rows(i)("WIM_Deptartment") = 0 Then
                                    dRow("DepartmentCustomer") = dt.Rows(i)("Org_name")
                                ElseIf dt.Rows(i)("WIM_Customer") = 0 Then
                                    dRow("DepartmentCustomer") = dt.Rows(i)("CUST_NAME")
                                End If
                                dRow("DocumentTitle") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_Title"))
                                dRow("DocumentReferenceNo") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                                dRow("DocumentRecievedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                                If dt.Rows(i)("WIM_DelFlag") = "A" Then
                                    dRow("Status") = "Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "D" Then
                                    dRow("Status") = "De-Activated"
                                ElseIf dt.Rows(i)("WIM_DelFlag") = "W" Then
                                    dRow("Status") = "Waiting For Approval"
                                End If
                                dtDisplay.Rows.Add(dRow)
                            Next
                        End If
                    End If
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveInward(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iInwardID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update WF_Inward_Masters Set WIM_Delflag='A',WIM_Status='U',WIM_ApprovedOn=getdate(),WIM_ApprovedBy=" & iUsrID & ",WIM_IPAdress='" & sIPAddress & "'"
                        sSql = sSql & " where WIM_PKID=" & iInwardID & " And WIM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ActivateInward(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iInwardID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update WF_Inward_Masters set WIM_Delflag='A',WIM_Status='U',WIM_RecalledOn=getdate(),WIM_RecalledBy=" & iUsrID & ",WIM_IPAdress='" & sIPAddress & "'"
            sSql = sSql & " where WIM_PKID=" & iInwardID & " and WIM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveInwardMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal objInward As strInward)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(30) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIDPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_InwardNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objInward.sWIDInwardNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_MonthID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIDMonthID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIDYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_InwardDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objInward.dWIDInwardDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_InwardTime", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objInward.sWIDInwardTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Title", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objInward.sWIDTitle
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_DocReferenceno", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objInward.sWIDDocReferenceno
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_DocRecievedDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objInward.dWIDDocRecievedDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_DateOnDocument", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objInward.dWIDDateOnDocument
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_ReceiptMode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIDReceiptMode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Deptartment", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIDDeptartment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Customer", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIDCustomer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_ContactPerson", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objInward.sWIDContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_ContactEmailID", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objInward.sWIDContactEmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_ContactPhNO", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objInward.sWIDContactPhNO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Remarks", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objInward.sWID_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWID_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objInward.sWID_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Delflag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objInward.sWID_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWID_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_IPAdress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInward.sWIDsIPAdress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWID_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_InwardOrWorkFlow", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMInwardOrWorkFlow
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Address", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objInward.sWIMAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Stage", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMStage
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_WorkFLowID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMWorkFLowID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_WorkFLowArchiveID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMWorkFLowArchiveID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIM_Progress_Status", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIM_Progress_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spWF_Inward_Masters", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveInwardMasterHistory(ByVal sAC As String, ByVal objInward As strInwardHistory) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_InwardPKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_InwardPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_User", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_User
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_SentTOID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_SentTOID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_Remarks", OleDb.OleDbType.VarChar, 100000)
            ObjParam(iParamCount).Value = objInward.sWIMH_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_LineNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_LineNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_Stage", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_Stage
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WIMH_ReplyOrForward", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInward.iWIMH_ReplyOrForward
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spWF_Inward_Masters_history", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function



    '--rakshan--
    Public Function LoadUsers(ByVal sAC As String, ByVal iACId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Usr_ID,Usr_FullName from sad_userdetails where Usr_CompID=" & iACId & " and USR_DelFlag='A'"
            sSql = sSql & " order by Usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    '--rakshan--
    Public Function LoadInwardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal sInwardNO As String, ByVal sReferenceNO As String, ByVal sTitle As String,
                                      ByVal iMode As Integer, ByVal dRecievedDate As String, ByVal dDocumentDate As String, ByVal dInwardFromDate As String, ByVal dInwardToDate As String,
                                      ByVal iUser As Integer, ByVal sPersonName As String, ByVal sEmailID As String, ByVal sContactNo As String, ByVal iDepartment As Integer) As DataTable
        Dim dtDisplay As New DataTable, dt As New DataTable, dt1 As New DataTable
        Dim sSql As String = "" : Dim aSql As String = ""
        Dim sSentToUser As String = ""
        Dim dRow As DataRow
        Try
            dtDisplay.Columns.Add("WIM_PKID")
            dtDisplay.Columns.Add("WIM_InwardNo")
            dtDisplay.Columns.Add("WIM_InwardDate")
            dtDisplay.Columns.Add("WIMH_SentTOID")
            dtDisplay.Columns.Add("WIM_DocRecievedDate")
            dtDisplay.Columns.Add("WIM_DocReferenceno")
            dtDisplay.Columns.Add("WIM_Deptartment")

            sSql = "Select Distinct(WIM_InwardNo),WIM_PKID,WIM_DocReferenceno,WIM_Title,WIM_ReceiptMode,WIM_DocRecievedDate,WIM_DateOnDocument,WIM_InwardDate,"
            sSql = sSql & " WIM_ContactPerson, WIM_ContactEmailID,WIM_ContactPhNO, WIM_Deptartment,Org_Name From WF_Inward_Masters"
            sSql = sSql & " Left Join Sad_Org_Structure On Org_Node=WIM_Deptartment"
            If (iUser > 0) Then
                sSql = sSql & " Left Join WF_Inward_Masters_history On WIM_PKID=WIMH_InwardPKID"
            End If
            sSql = sSql & " Where Org_LevelCode=3 And Org_CompID=" & iACID & " And WIM_CompID=" & iACID & ""
            If (iUser > 0) Then
                sSql = sSql & " And (WIMH_User=" & iUser & ")"
            End If
            If (sInwardNO.Length > 0) Then
                sSql = sSql & " And (WIM_InwardNo Like '%" & sInwardNO & "%')"
            End If
            If (sReferenceNO.Length > 0) Then
                sSql = sSql & " And (WIM_DocReferenceno like '%" & sReferenceNO & "%')"
            End If
            If (sTitle.Length > 0) Then
                sSql = sSql & " And (WIM_Title like '%" & sTitle & "%')"
            End If
            If (iMode > 0) Then
                sSql = sSql & " And (WIM_ReceiptMode like '%" & iMode & "%')"
            End If
            If (dRecievedDate <> "") Then
                sSql = sSql & " And WIM_DocRecievedDate=" & objGen.FormatDtForRDBMS(dRecievedDate, "Q") & ""
            End If
            If (dDocumentDate <> "") Then
                sSql = sSql & " And WIM_DateOnDocument=" & objGen.FormatDtForRDBMS(dDocumentDate, "Q") & ""
            End If
            If (dInwardFromDate <> "") And (dInwardToDate <> "") Then
                sSql = sSql & " And WIM_InwardDate between " & objGen.FormatDtForRDBMS(dInwardFromDate, "Q") & " And " & objGen.FormatDtForRDBMS(dInwardToDate, "Q") & ""
            ElseIf (dInwardFromDate <> "") Then
                sSql = sSql & " And WIM_InwardDate=" & objGen.FormatDtForRDBMS(dInwardFromDate, "Q") & ""
            ElseIf (dInwardToDate <> "") Then
                sSql = sSql & " And WIM_InwardDate=" & objGen.FormatDtForRDBMS(dInwardToDate, "Q") & ""
            End If
            If (sPersonName.Length > 0) Then
                sSql = sSql & " And (WIM_ContactPerson like '%" & sPersonName & "%')"
            End If
            If (sEmailID.Length > 0) Then
                sSql = sSql & " And (WIM_ContactEmailID like '%" & sEmailID & "%')"
            End If
            If (sContactNo.Length > 0) Then
                sSql = sSql & " And (WIM_ContactPhNO like '%" & sContactNo & "%')"
            End If
            If (iDepartment > 0) Then
                sSql = sSql & " And WIM_Deptartment=" & iDepartment & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    If IsDBNull(dt.Rows(i)("WIM_PKID")) = False Then
                        dRow("WIM_PKID") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_PKID"))
                    End If
                    If IsDBNull(dt.Rows(i)("WIM_InwardNo")) = False Then
                        dRow("WIM_InwardNo") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_InwardNo"))
                    End If
                    If IsDBNull(dt.Rows(i)("WIM_InwardDate")) = False Then
                        dRow("WIM_InwardDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_InwardDate"), "D")
                    End If

                    aSql = "" : aSql = "Select * from WF_Inward_Masters_history where WIMH_InwardPKID=" & objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_PKID")) & " And "
                    aSql = aSql & "WIMH_CompID = " & iACID & " Order by WIMH_PKID"
                    dt1 = objDBL.SQLExecuteDataTable(sAC, aSql)
                    If dt1.Rows.Count > 0 Then
                        sSentToUser = ""
                        For j = 0 To dt1.Rows.Count - 1
                            sSentToUser = sSentToUser & "," & objGen.ReplaceSafeSQL(objGenFun.GetUserFullNameFromUserID(sAC, iACID, dt1.Rows(j)("WIMH_SentToID")))
                        Next
                        If sSentToUser <> "" Then
                            dRow("WIMH_SentTOID") = sSentToUser.Remove(0, 1)
                        End If
                    End If

                    If IsDBNull(dt.Rows(i)("WIM_DocRecievedDate")) = False Then
                        dRow("WIM_DocRecievedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("WIM_DocRecievedDate"), "D")
                    End If
                    If IsDBNull(dt.Rows(i)("WIM_DocReferenceno")) = False Then
                        dRow("WIM_DocReferenceno") = objGen.ReplaceSafeSQL(dt.Rows(i)("WIM_DocReferenceno"))
                    End If
                    If IsDBNull(dt.Rows(i)("Org_Name")) = False Then
                        dRow("WIM_Deptartment") = objGen.ReplaceSafeSQL(dt.Rows(i)("Org_Name"))
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If



            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    '--rakshan--
    Public Function GetInwardDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInwardID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from WF_Inward_Masters where WIM_PKID=" & iInwardID & " and WIM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
