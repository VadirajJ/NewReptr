Public Structure strStandardAudit_AuditSummary_IFC
    Private SAIFC_PKID As Integer
    Private SAIFC_SA_ID As Integer
    Private SAIFC_CustID As Integer
    Private SAIFC_YearID As Integer
    Private SAIFC_ReportDate As Date
    Private SAIFC_ReportBy As String
    Private SAIFC_Comments As String
    Private SAIFC_ColumnCount As Integer
    Private SAIFC_AttachID As Integer
    Private SAIFC_CrBy As Integer
    Private SAIFC_IPAddress As String
    Private SAIFC_CompID As Integer
    Public Property iSAIFC_PKID() As Integer
        Get
            Return (SAIFC_PKID)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_PKID = Value
        End Set
    End Property
    Public Property iSAIFC_SA_ID() As Integer
        Get
            Return (SAIFC_SA_ID)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_SA_ID = Value
        End Set
    End Property
    Public Property iSAIFC_CustID() As Integer
        Get
            Return (SAIFC_CustID)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_CustID = Value
        End Set
    End Property
    Public Property iSAIFC_YearID() As Integer
        Get
            Return (SAIFC_YearID)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_YearID = Value
        End Set
    End Property
    Public Property dSAIFC_ReportDate() As Date
        Get
            Return (SAIFC_ReportDate)
        End Get
        Set(ByVal Value As Date)
            SAIFC_ReportDate = Value
        End Set
    End Property
    Public Property sSAIFC_ReportBy() As String
        Get
            Return (SAIFC_ReportBy)
        End Get
        Set(ByVal Value As String)
            SAIFC_ReportBy = Value
        End Set
    End Property
    Public Property sSAIFC_Comments() As String
        Get
            Return (SAIFC_Comments)
        End Get
        Set(ByVal Value As String)
            SAIFC_Comments = Value
        End Set
    End Property
    Public Property iSAIFC_ColumnCount() As Integer
        Get
            Return (SAIFC_ColumnCount)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_ColumnCount = Value
        End Set
    End Property
    Public Property iSAIFC_AttachID() As Integer
        Get
            Return (SAIFC_AttachID)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_AttachID = Value
        End Set
    End Property
    Public Property iSAIFC_CrBy() As Integer
        Get
            Return (SAIFC_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_CrBy = Value
        End Set
    End Property
    Public Property sSAIFC_IPAddress() As String
        Get
            Return (SAIFC_IPAddress)
        End Get
        Set(ByVal Value As String)
            SAIFC_IPAddress = Value
        End Set
    End Property
    Public Property iSAIFC_CompID() As Integer
        Get
            Return (SAIFC_CompID)
        End Get
        Set(ByVal Value As Integer)
            SAIFC_CompID = Value
        End Set
    End Property
End Structure

Public Structure strStandardAudit_AuditSummary_IFCDetails
    Private SAIFCD_PKID As Integer
    Private SAIFCD_SAIFC_PKID As Integer
    Private SAIFCD_ColumnRowType As Integer
    Private SAIFCD_Column1 As String
    Private SAIFCD_Column2 As String
    Private SAIFCD_Column3 As String
    Private SAIFCD_Column4 As String
    Private SAIFCD_Column5 As String
    Private SAIFCD_Column6 As String
    Public Property iSAIFCD_PKID() As Integer
        Get
            Return (SAIFCD_PKID)
        End Get
        Set(ByVal Value As Integer)
            SAIFCD_PKID = Value
        End Set
    End Property
    Public Property iSAIFCD_SAIFC_PKID() As Integer
        Get
            Return (SAIFCD_SAIFC_PKID)
        End Get
        Set(ByVal Value As Integer)
            SAIFCD_SAIFC_PKID = Value
        End Set
    End Property
    Public Property iSAIFCD_ColumnRowType() As Integer
        Get
            Return (SAIFCD_ColumnRowType)
        End Get
        Set(ByVal Value As Integer)
            SAIFCD_ColumnRowType = Value
        End Set
    End Property
    Public Property sSAIFCD_Column1() As String
        Get
            Return (SAIFCD_Column1)
        End Get
        Set(ByVal Value As String)
            SAIFCD_Column1 = Value
        End Set
    End Property
    Public Property sSAIFCD_Column2() As String
        Get
            Return (SAIFCD_Column2)
        End Get
        Set(ByVal Value As String)
            SAIFCD_Column2 = Value
        End Set
    End Property
    Public Property sSAIFCD_Column3() As String
        Get
            Return (SAIFCD_Column3)
        End Get
        Set(ByVal Value As String)
            SAIFCD_Column3 = Value
        End Set
    End Property
    Public Property sSAIFCD_Column4() As String
        Get
            Return (SAIFCD_Column4)
        End Get
        Set(ByVal Value As String)
            SAIFCD_Column4 = Value
        End Set
    End Property
    Public Property sSAIFCD_Column5() As String
        Get
            Return (SAIFCD_Column5)
        End Get
        Set(ByVal Value As String)
            SAIFCD_Column5 = Value
        End Set
    End Property
    Public Property sSAIFCD_Column6() As String
        Get
            Return (SAIFCD_Column6)
        End Get
        Set(ByVal Value As String)
            SAIFCD_Column6 = Value
        End Set
    End Property
End Structure
Public Structure strStandardAudit_AuditSummary_MRDetails
    Private SAMR_PKID As Integer
    Private SAMR_SA_PKID As Integer
    Private SAMR_CustID As Integer
    Private SAMR_YearID As Integer
    Private SAMR_MRID As Integer
    Private SAMR_RequestedDate As Date
    Private SAMR_RequestedByPerson As String
    Private SAMR_RequestedRemarks As String
    Private SAMR_DueDateReceiveDocs As Date
    Private SAMR_EmailIds As String
    Private SAMR_CrBy As Integer
    Private SAMR_IPAddress As String
    Private SAMR_CompID As Integer
    Public Property iSAMR_PKID() As Integer
        Get
            Return (SAMR_PKID)
        End Get
        Set(ByVal Value As Integer)
            SAMR_PKID = Value
        End Set
    End Property
    Public Property iSAMR_SA_PKID() As Integer
        Get
            Return (SAMR_SA_PKID)
        End Get
        Set(ByVal Value As Integer)
            SAMR_SA_PKID = Value
        End Set
    End Property
    Public Property iSAMR_CustID() As Integer
        Get
            Return (SAMR_CustID)
        End Get
        Set(ByVal Value As Integer)
            SAMR_CustID = Value
        End Set
    End Property
    Public Property iSAMR_YearID() As Integer
        Get
            Return (SAMR_YearID)
        End Get
        Set(ByVal Value As Integer)
            SAMR_YearID = Value
        End Set
    End Property
    Public Property iSAMR_MRID() As Integer
        Get
            Return (SAMR_MRID)
        End Get
        Set(ByVal Value As Integer)
            SAMR_MRID = Value
        End Set
    End Property
    Public Property dSAMR_RequestedDate() As Date
        Get
            Return (SAMR_RequestedDate)
        End Get
        Set(ByVal Value As Date)
            SAMR_RequestedDate = Value
        End Set
    End Property
    Public Property sSAMR_RequestedByPerson() As String
        Get
            Return (SAMR_RequestedByPerson)
        End Get
        Set(ByVal Value As String)
            SAMR_RequestedByPerson = Value
        End Set
    End Property
    Public Property sSAMR_RequestedRemarks() As String
        Get
            Return (SAMR_RequestedRemarks)
        End Get
        Set(ByVal Value As String)
            SAMR_RequestedRemarks = Value
        End Set
    End Property
    Public Property dSAMR_DueDateReceiveDocs() As Date
        Get
            Return (SAMR_DueDateReceiveDocs)
        End Get
        Set(ByVal Value As Date)
            SAMR_DueDateReceiveDocs = Value
        End Set
    End Property
    Public Property sSAMR_EmailIds() As String
        Get
            Return (SAMR_EmailIds)
        End Get
        Set(ByVal Value As String)
            SAMR_EmailIds = Value
        End Set
    End Property
    Public Property iSAMR_CrBy() As Integer
        Get
            Return (SAMR_CrBy)
        End Get
        Set(ByVal Value As Integer)
            SAMR_CrBy = Value
        End Set
    End Property
    Public Property sSAMR_IPAddress() As String
        Get
            Return (SAMR_IPAddress)
        End Get
        Set(ByVal Value As String)
            SAMR_IPAddress = Value
        End Set
    End Property
    Public Property iSAMR_CompID() As Integer
        Get
            Return (SAMR_CompID)
        End Get
        Set(ByVal Value As Integer)
            SAMR_CompID = Value
        End Set
    End Property
End Structure
Public Class clsSAAuditSummary
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Dim obclsUL As New clsUploadLedger

    Public Function SaveUpdateStandardAuditASIFC(ByVal sAC As String, ByVal objSAIFC As strStandardAudit_AuditSummary_IFC)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_SA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_SA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_ReportDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSAIFC.dSAIFC_ReportDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_ReportBy", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSAIFC.sSAIFC_ReportBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_Comments", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objSAIFC.sSAIFC_Comments
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_ColumnCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_ColumnCount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("SAIFC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSAIFC.sSAIFC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFC_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFC.iSAIFC_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spStandardAudit_AuditSummary_IFC", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveStandardAuditASIFCdetails(ByVal sAC As String, ByVal objSAIFCD As strStandardAudit_AuditSummary_IFCDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFCD.iSAIFCD_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_SAIFC_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFCD.iSAIFCD_SAIFC_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_ColumnRowType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAIFCD.iSAIFCD_ColumnRowType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_Column1", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSAIFCD.sSAIFCD_Column1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_Column2", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSAIFCD.sSAIFCD_Column2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_Column3", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSAIFCD.sSAIFCD_Column3
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_Column4", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSAIFCD.sSAIFCD_Column4
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_Column5", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSAIFCD.sSAIFCD_Column5
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAIFCD_Column6", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSAIFCD.sSAIFCD_Column6
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spStandardAudit_AuditSummary_IFCDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStandardAuditASIFCdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iIFCPKID As Integer, ByVal dDateOfTesting As Date, ByVal sTestingDetails As String, ByVal sSampleSizeUsed As String, ByVal iConclusionId As Integer, ByVal iUserId As Integer)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_AuditSummary_IFCDetails set SAIFCD_DateOfTesting=" & objclsGRACeGeneral.FormatDtForRDBMS(dDateOfTesting, "Q") & ",SAIFCD_TypeOfTestingDetails='" & sTestingDetails & "',SAIFCD_SampleSizeUsed='" & sSampleSizeUsed & "', "
            sSql = sSql & " SAIFCD_Conclusion=" & iConclusionId & ",SAIFCD_CrBy=" & iUserId & ",SAIFCD_CrOn=Getdate() Where SAIFCD_PKID=" & iIFCPKID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetStandardAuditASIFCcolumnCount(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select SAIFC_ColumnCount From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & " And SAIFC_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalarInt(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStandardAuditASIFCcolumnHeaderName(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal sColumnName As String) As String
        Dim sSql As String
        Try
            sSql = "Select " & sColumnName & " From StandardAudit_AuditSummary_IFCDetails Where SAIFCD_SAIFC_PKID=(Select SAIFC_PKID From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & " And SAIFC_CompID=" & iAcID & ") And SAIFCD_ColumnRowType=0"
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStandardAuditASIFCbasicDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ISNULL(Convert(Varchar(10),SAIFC_ReportDate,103),'') As SAIFC_ReportDate,SAIFC_ReportBy,SAIFC_Comments,SAIFC_ColumnCount From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & " And SAIFC_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStandardAuditASIFCselectedDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal iIFCDPKID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ISNULL(Convert(Varchar(10),SAIFCD_DateOfTesting,103),'') As SAIFCD_DateOfTesting,SAIFCD_TypeOfTestingDetails,SAIFCD_SampleSizeUsed,SAIFCD_Conclusion From StandardAudit_AuditSummary_IFCDetails"
            sSql = sSql & " Where SAIFCD_SAIFC_PKID=(Select SAIFC_PKID From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & " And SAIFC_CompID=" & iAcID & ") And SAIFCD_PKID=" & iIFCDPKID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadStandardAuditASIFCdetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer) As DataTable
    '    Dim sSql As String, sSql1 As String
    '    Dim dr As DataRow
    '    Dim dt As New DataTable, dtSlectedHeader As New DataTable, dtTab As New DataTable
    '    Dim iColumnCount As Integer
    '    Try
    '        iColumnCount = GetStandardAuditASIFCcolumnCount(sAc, iAcID, iScheduledAsgID)
    '        If iColumnCount > 0 Then
    '            sSql = "Select SAIFCD_Column1,SAIFCD_Column2,SAIFCD_Column3,SAIFCD_Column4,SAIFCD_Column5,SAIFCD_Column6 From StandardAudit_AuditSummary_IFCDetails Where SAIFCD_ColumnRowType=0"
    '            sSql = sSql & " And SAIFCD_SAIFC_PKID=(Select SAIFC_PKID From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & ")"
    '            dtSlectedHeader = objDBL.SQLExecuteDataTable(sAc, sSql)

    '            dt.Columns.Add("SrNo")
    '            dt.Columns.Add("DBpkId")
    '            dt.Columns.Add("AttachmentID")
    '            Dim row As DataRow = dtSlectedHeader.Rows(0)
    '            For columnIndex As Integer = 0 To iColumnCount - 1
    '                If IsDBNull(row(columnIndex)) = False And row(columnIndex) <> "" Then
    '                    dt.Columns.Add(row(columnIndex)).ToString()
    '                End If
    '            Next

    '            sSql1 = "Select SAIFCD_PKID,SAIFCD_Column1,SAIFCD_Column2,SAIFCD_Column3,SAIFCD_Column4,SAIFCD_Column5,SAIFCD_Column6,ISNULL(SAIFCD_AttachID,0) As SAIFCD_AttachID From StandardAudit_AuditSummary_IFCDetails Where SAIFCD_ColumnRowType=1"
    '            sSql1 = sSql1 & " And SAIFCD_SAIFC_PKID=(Select SAIFC_PKID From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & ")"
    '            dtTab = objDBL.SQLExecuteDataTable(sAc, sSql1)
    '            For i = 0 To dtTab.Rows.Count - 1
    '                dr = dt.NewRow()
    '                dr("SrNo") = i + 1
    '                dr("DBpkId") = dtTab.Rows(i)("SAIFCD_PKID")
    '                dr("AttachmentID") = dtTab.Rows(i)("SAIFCD_AttachID")
    '                For columnIndex As Integer = 0 To iColumnCount - 1
    '                    If IsDBNull(row(columnIndex)) = False And row(columnIndex) <> "" Then
    '                        Dim sColumnId As String = "SAIFCD_Column" & columnIndex + 1
    '                        dr((row(columnIndex)).ToString()) = dtTab.Rows(i)(sColumnId)
    '                    End If
    '                Next
    '                dt.Rows.Add(dr)
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadStandardAuditASIFCdetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer) As DataTable
        Dim sSql As String
        Dim dr As DataRow
        Dim dt As New DataTable, dtSlectedHeader As New DataTable, dtTab As New DataTable
        Dim iColumnCount As Integer
        Try
            iColumnCount = GetStandardAuditASIFCcolumnCount(sAc, iAcID, iScheduledAsgID)
            If iColumnCount > 0 Then
                dt.Columns.Add("SrNo")
                dt.Columns.Add("DBpkId")
                dt.Columns.Add("SAIFCD_Column1")
                dt.Columns.Add("SAIFCD_Column2")
                dt.Columns.Add("SAIFCD_Column3")
                dt.Columns.Add("SAIFCD_Column4")
                dt.Columns.Add("SAIFCD_Column5")
                dt.Columns.Add("SAIFCD_Column6")
                dt.Columns.Add("TestingDetails")
                dt.Columns.Add("Conclusion")
                dt.Columns.Add("AttachmentID")

                sSql = "Select SAIFCD_PKID,ISNULL(SAIFCD_Column1,'') As SAIFCD_Column1,ISNULL(SAIFCD_Column2,'') As SAIFCD_Column2,ISNULL(SAIFCD_Column3,'') As SAIFCD_Column3,ISNULL(SAIFCD_Column4,'') As SAIFCD_Column4,"
                sSql = sSql & " ISNULL(SAIFCD_Column5,'') As SAIFCD_Column5,ISNULL(SAIFCD_Column6,'') As SAIFCD_Column6,ISNULL(SAIFCD_TypeOfTestingDetails,'') As SAIFCD_TypeOfTestingDetails,"
                sSql = sSql & " Case When SAIFCD_Conclusion=1 then 'KAM' When SAIFCD_Conclusion=2 then 'Audit Observation' Else '' End SAIFCD_Conclusion,"
                sSql = sSql & " ISNULL(SAIFCD_AttachID,0) As SAIFCD_AttachID From StandardAudit_AuditSummary_IFCDetails Where SAIFCD_ColumnRowType=1"
                sSql = sSql & " And SAIFCD_SAIFC_PKID=(Select SAIFC_PKID From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & ")"
                dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)
                For i = 0 To dtTab.Rows.Count - 1
                    dr = dt.NewRow()
                    dr("SrNo") = i + 1
                    dr("DBpkId") = dtTab.Rows(i)("SAIFCD_PKID")
                    dr("SAIFCD_Column1") = dtTab.Rows(i)("SAIFCD_Column1")
                    dr("SAIFCD_Column2") = dtTab.Rows(i)("SAIFCD_Column2")
                    dr("SAIFCD_Column3") = dtTab.Rows(i)("SAIFCD_Column3")
                    dr("SAIFCD_Column4") = dtTab.Rows(i)("SAIFCD_Column4")
                    dr("SAIFCD_Column5") = dtTab.Rows(i)("SAIFCD_Column5")
                    dr("SAIFCD_Column6") = dtTab.Rows(i)("SAIFCD_Column6")
                    dr("TestingDetails") = dtTab.Rows(i)("SAIFCD_TypeOfTestingDetails")
                    dr("Conclusion") = dtTab.Rows(i)("SAIFCD_Conclusion")
                    dr("AttachmentID") = dtTab.Rows(i)("SAIFCD_AttachID")
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStandardAuditASKAMdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iIFCPKID As Integer, ByVal iKAMDPKID As Integer, ByVal sDescriptionOrReasonForSelectionAsKAM As String, ByVal sAuditProcedureUndertakenToAddressTheKAM As String, ByVal iUserId As Integer)
        Dim sSql As String
        Try
            If iKAMDPKID = 0 Then
                sSql = "Select ISNULL(max(SAKAMD_PKID),0)+1 from StandardAudit_AuditSummary_KAMDetails"
                iKAMDPKID = objDBL.SQLExecuteScalarInt(sAC, sSql)

                sSql = "Insert into StandardAudit_AuditSummary_KAMDetails (SAKAMD_PKID,SAKAM_SAIFCD_PKID,SAKAM_DescriptionOrReasonForSelectionAsKAM,SAKAM_AuditProcedureUndertakenToAddressTheKAM,SAKAM_AttachID,SAKAM_CrBy,SAKAM_CrOn) "
                sSql = sSql & " values(" & iKAMDPKID & "," & iIFCPKID & ",'" & sDescriptionOrReasonForSelectionAsKAM & "','" & sAuditProcedureUndertakenToAddressTheKAM & "',0," & iUserId & ",Getdate())"
            Else
                sSql = "Update StandardAudit_AuditSummary_KAMDetails set SAKAM_DescriptionOrReasonForSelectionAsKAM='" & sDescriptionOrReasonForSelectionAsKAM & "',SAKAM_AuditProcedureUndertakenToAddressTheKAM='" & sAuditProcedureUndertakenToAddressTheKAM & "' "
                sSql = sSql & " Where SAKAMD_PKID=" & iKAMDPKID & " And SAKAM_SAIFCD_PKID=" & iIFCPKID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateStandardAuditASIFCAttachmentdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iIFCPKID As Integer, ByVal iAttachmentID As Integer)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_AuditSummary_IFCDetails set SAIFCD_AttachID=" & iAttachmentID & " Where SAIFCD_PKID=" & iIFCPKID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateStandardAuditASKAMAttachmentdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iKAMDPKID As Integer, ByVal iAttachmentID As Integer)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_AuditSummary_KAMDetails set SAKAM_AttachID=" & iAttachmentID & " Where SAKAMD_PKID=" & iKAMDPKID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadStandardAuditIFCtoKAM(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer) As DataTable
        Dim sSql As String
        Dim dr As DataRow
        Dim dt As New DataTable, dtTab As New DataTable
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DBpkId")
            dt.Columns.Add("IFCDpkId")
            dt.Columns.Add("AttachmentID")
            dt.Columns.Add("Source")
            dt.Columns.Add("KAM")
            dt.Columns.Add("DescriptionOrReasonForSelectionAsKAM")
            dt.Columns.Add("AuditProcedureUndertakenToAddressTheKAM")

            sSql = "Select SAIFCD_PKID,SAIFCD_SAIFC_PKID,SAIFCD_TypeOfTestingDetails,ISNULL(SAKAMD_PKID,0) As SAKAMD_PKID,ISNULL(SAKAM_DescriptionOrReasonForSelectionAsKAM,'') As SAKAM_DescriptionOrReasonForSelectionAsKAM,"
            sSql = sSql & " ISNULL(SAKAM_AuditProcedureUndertakenToAddressTheKAM,'') As SAKAM_AuditProcedureUndertakenToAddressTheKAM,ISNULL(SAKAM_AttachID,0) As SAKAM_AttachID From StandardAudit_AuditSummary_IFCDetails"
            sSql = sSql & " Left Join StandardAudit_AuditSummary_KAMDetails On SAIFCD_PKID=SAKAM_SAIFCD_PKID"
            sSql = sSql & " Where SAIFCD_ColumnRowType=1 And SAIFCD_Conclusion=1 And SAIFCD_TypeOfTestingDetails IS NOT NULL"
            sSql = sSql & " And SAIFCD_SAIFC_PKID=(Select SAIFC_PKID From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & ")"
            dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtTab.Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DBpkId") = dtTab.Rows(i)("SAKAMD_PKID")
                dr("IFCDpkId") = dtTab.Rows(i)("SAIFCD_PKID")
                dr("AttachmentID") = dtTab.Rows(i)("SAKAM_AttachID")
                dr("Source") = "IFC"
                dr("KAM") = dtTab.Rows(i)("SAIFCD_TypeOfTestingDetails")
                dr("DescriptionOrReasonForSelectionAsKAM") = dtTab.Rows(i)("SAKAM_DescriptionOrReasonForSelectionAsKAM")
                dr("AuditProcedureUndertakenToAddressTheKAM") = dtTab.Rows(i)("SAKAM_AuditProcedureUndertakenToAddressTheKAM")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStandardAuditASKAMselectedDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal iIFCDPKID As Integer, ByVal iKAMDPKID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SAIFCD_TypeOfTestingDetails,ISNULL(SAKAM_DescriptionOrReasonForSelectionAsKAM,'') As SAKAM_DescriptionOrReasonForSelectionAsKAM,"
            sSql = sSql & " ISNULL(SAKAM_AuditProcedureUndertakenToAddressTheKAM,'') As SAKAM_AuditProcedureUndertakenToAddressTheKAM,ISNULL(SAKAM_AttachID,0) As SAKAM_AttachID From StandardAudit_AuditSummary_IFCDetails"
            sSql = sSql & " Left Join StandardAudit_AuditSummary_KAMDetails On SAIFCD_PKID=SAKAM_SAIFCD_PKID"
            sSql = sSql & " Where SAIFCD_ColumnRowType=1 And SAIFCD_Conclusion=1 And SAIFCD_TypeOfTestingDetails IS NOT NULL"
            sSql = sSql & " And SAIFCD_SAIFC_PKID=(Select SAIFC_PKID From StandardAudit_AuditSummary_IFC Where SAIFC_SA_ID=" & iScheduledAsgID & ") And SAIFCD_PKID=" & iIFCDPKID & ""
            If iKAMDPKID > 0 Then
                sSql = sSql & " And SAKAMD_PKID=" & iKAMDPKID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustAllUserEmails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iCustomerID As Integer) As String
        Dim sSql As String
        Try
            sSql = "SELECT ISNULL(STUFF((SELECT DISTINCT ';' + Usr_Email FROM Sad_UserDetails WHERE Usr_Companyid=" & iCustomerID & " And Usr_Email<>'' And Usr_CompId=" & iAcID & " And Usr_Email IS NOT NULL FOR XML PATH('')), 1, 2, ''),'')"
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadManagementRepresentations(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal iFYId As Integer, ByVal iAsgId As Integer, ByVal iMRPKID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & " And CMM_Delflag='A' And CMS_KeyComponent=0 And "
            sSql = sSql & " (CMM_ID Not in (Select SAMR_MRID From StandardAudit_AuditSummary_MRDetails Where SAMR_YearID=" & iFYId & " And SAMR_SA_PKID=" & iAsgId & " And SAMR_CompID=" & iAcID & ")"
            If iMRPKID > 0 Then
                sSql = sSql & " Or CMM_ID=" & iMRPKID & ""
            End If
            sSql = sSql & " ) Order By CMM_Desc ASC"

            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetManagementRepresentationsDesc(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMRId As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CMS_Remarks From Content_Management_Master Where CMM_Category='MR' And CMM_ID=" & iMRId & " And CMM_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveStandardAuditASMRdetails(ByVal sAC As String, ByVal objSAMRD As strStandardAudit_AuditSummary_MRDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAMRD.iSAMR_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_SA_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAMRD.iSAMR_SA_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAMRD.iSAMR_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAMRD.iSAMR_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_MRID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAMRD.iSAMR_MRID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_RequestedDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSAMRD.dSAMR_RequestedDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_RequestedByPerson", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objSAMRD.sSAMR_RequestedByPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_RequestedRemarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objSAMRD.sSAMR_RequestedRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_DueDateReceiveDocs", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objSAMRD.dSAMR_DueDateReceiveDocs
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_EmailIds", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objSAMRD.sSAMR_EmailIds
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAMRD.iSAMR_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSAMRD.sSAMR_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAMR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSAMRD.iSAMR_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spStandardAudit_AuditSummary_MRDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStandardAuditASMRdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iScheduledAsgID As Integer, ByVal iMRPKID As Integer, ByVal dMRDueDateReceiveDocs As Date, ByVal sResponsesDetails As String, ByVal sResponsesRemarks As String)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_AuditSummary_MRDetails set SAMR_ResponsesReceivedDate=" & objclsGRACeGeneral.FormatDtForRDBMS(dMRDueDateReceiveDocs, "Q") & ",SAMR_ResponsesDetails='" & sResponsesDetails & "',SAMR_ResponsesRemarks='" & sResponsesRemarks & "' "
            sSql = sSql & " Where SAMR_SA_PKID=" & iScheduledAsgID & " And SAMR_PKID=" & iMRPKID & " And SAMR_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadStandardAuditMR(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer) As DataTable
        Dim sSql As String
        Dim dr As DataRow
        Dim dt As New DataTable, dtTab As New DataTable
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DBpkId")
            dt.Columns.Add("Heading")
            dt.Columns.Add("Description")
            dt.Columns.Add("RequestedDat")
            dt.Columns.Add("RequestedByPerson")
            dt.Columns.Add("DueDateReceiveDocs")
            dt.Columns.Add("ResponsesReceivedDate")
            dt.Columns.Add("ResponsesDetails")
            dt.Columns.Add("ResponsesRemarks")
            dt.Columns.Add("AttachmentID")

            sSql = "Select SAMR_PKID,ISNULL(CMM_Desc,'') As Heading,ISNULL(CMS_Remarks,'') As Description,SAMR_MRID,ISNULL(Convert(Varchar(10),SAMR_RequestedDate,103),'') As SAMR_RequestedDate,SAMR_RequestedByPerson,SAMR_RequestedRemarks,"
            sSql = sSql & " ISNULL(Convert(Varchar(10),SAMR_DueDateReceiveDocs,103),'') As SAMR_DueDateReceiveDocs,ISNULL(SAMR_EmailIds,'') As SAMR_EmailIds,ISNULL(Convert(Varchar(10),SAMR_ResponsesReceivedDate,103),'') As SAMR_ResponsesReceivedDate,"
            sSql = sSql & " ISNULL(SAMR_ResponsesDetails,'') As SAMR_ResponsesDetails,ISNULL(SAMR_ResponsesRemarks,'') As SAMR_ResponsesRemarks,ISNULL(SAMR_AttachID,0) As SAMR_AttachID From StandardAudit_AuditSummary_MRDetails"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=SAMR_MRID"
            sSql = sSql & " Where SAMR_SA_PKID=" & iScheduledAsgID & " And SAMR_CompID=" & iAcID & ""
            dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtTab.Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DBpkId") = dtTab.Rows(i)("SAMR_PKID")
                dr("Heading") = dtTab.Rows(i)("Heading")
                dr("Description") = dtTab.Rows(i)("Description")
                dr("RequestedDat") = dtTab.Rows(i)("SAMR_RequestedDate")
                dr("RequestedByPerson") = dtTab.Rows(i)("SAMR_RequestedByPerson")
                dr("DueDateReceiveDocs") = dtTab.Rows(i)("SAMR_DueDateReceiveDocs")
                dr("ResponsesReceivedDate") = dtTab.Rows(i)("SAMR_ResponsesReceivedDate")
                dr("ResponsesDetails") = dtTab.Rows(i)("SAMR_ResponsesDetails")
                dr("ResponsesRemarks") = dtTab.Rows(i)("SAMR_ResponsesRemarks")
                dr("AttachmentID") = dtTab.Rows(i)("SAMR_AttachID")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStandardAuditASMRselectedDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iScheduledAsgID As Integer, ByVal iMRPKID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SAMR_PKID,ISNULL(CMM_Desc,'') As Heading,ISNULL(CMS_Remarks,'') As Description,SAMR_MRID,ISNULL(Convert(Varchar(10),SAMR_RequestedDate,103),'') As SAMR_RequestedDate,SAMR_RequestedByPerson,SAMR_RequestedRemarks,"
            sSql = sSql & " ISNULL(Convert(Varchar(10),SAMR_DueDateReceiveDocs,103),'') As SAMR_DueDateReceiveDocs,ISNULL(SAMR_EmailIds,'') As SAMR_EmailIds,SAMR_RequestedRemarks,ISNULL(Convert(Varchar(10),SAMR_ResponsesReceivedDate,103),'') As SAMR_ResponsesReceivedDate,"
            sSql = sSql & " ISNULL(SAMR_ResponsesDetails,'') As SAMR_ResponsesDetails,ISNULL(SAMR_ResponsesRemarks,'') As SAMR_ResponsesRemarks,ISNULL(SAMR_AttachID,0) As SAMR_AttachID From StandardAudit_AuditSummary_MRDetails"
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=SAMR_MRID"
            sSql = sSql & " Where SAMR_SA_PKID=" & iScheduledAsgID & " And SAMR_CompID=" & iAcID & " And SAMR_PKID=" & iMRPKID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStandardAuditASMRAttachmentdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iMRPKID As Integer, ByVal iAttachmentID As Integer)
        Dim sSql As String
        Try
            sSql = "Update StandardAudit_AuditSummary_MRDetails set SAMR_AttachID=" & iAttachmentID & " Where SAMR_PKID=" & iMRPKID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
