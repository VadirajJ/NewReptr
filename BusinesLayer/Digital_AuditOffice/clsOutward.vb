Imports DatabaseLayer
Public Structure strOutward
    Private WOM_ID As Integer
    Private WOM_OutwardNo As String
    Private WOM_MonthID As Integer
    Private WOM_YearID As Integer
    Private WOM_OutwardDate As Date
    Private WOM_OutwardTime As String
    Private WOM_Department As Integer
    Private WOM_Customer As Integer
    Private WOM_InwardID As Integer
    Private WOM_InwardRefNo As String
    Private WOM_InwardName As String
    Private WOM_Address As String
    Private WOM_Page As String
    Private WOM_Sensitivity As String
    Private WOM_OutwardRefNo As String
    Private WOM_DispathMode As Integer
    Private WOM_ReplyAwaited As Integer
    Private WOM_DocumentType As Integer
    Private WOM_MailingExpenses As String
    Private WOM_AttachmentDetails As String
    Private WOM_Remarks As String
    Private WOM_SendTo As String
    Private WOM_AttachID As Integer
    Private WOM_CreatedBy As Integer
    Private WOM_CreatedOn As Date
    Private WOM_UpdatedBy As Integer
    Private WOM_UpdatedOn As Date
    Private WOM_ApprovedBy As Integer
    Private WOM_ApprovedOn As Date
    Private WOM_DeletedBy As Integer
    Private WOM_DeletedOn As Date
    Private WOM_RecalledBy As Integer
    Private WOM_RecalledOn As Date
    Private WOM_Status As String
    Private WOM_Delflag As String
    Private WOM_CompID As Integer
    Private WOM_IPAddress As String
    Public Property sWOM_IPAddress() As String
        Get
            Return (WOM_IPAddress)
        End Get
        Set(ByVal Value As String)
            WOM_IPAddress = Value
        End Set
    End Property
    Public Property iWOM_ID() As Integer
        Get
            Return (WOM_ID)
        End Get
        Set(ByVal Value As Integer)
            WOM_ID = Value
        End Set
    End Property
    Public Property sWOM_OutwardNo() As String
        Get
            Return (WOM_OutwardNo)
        End Get
        Set(ByVal Value As String)
            WOM_OutwardNo = Value
        End Set
    End Property
    Public Property iWOM_MonthID() As Integer
        Get
            Return (WOM_MonthID)
        End Get
        Set(ByVal Value As Integer)
            WOM_MonthID = Value
        End Set
    End Property
    Public Property iWOM_YearID() As Integer
        Get
            Return (WOM_YearID)
        End Get
        Set(ByVal Value As Integer)
            WOM_YearID = Value
        End Set
    End Property
    Public Property dWOM_OutwardDate() As Date
        Get
            Return (WOM_OutwardDate)
        End Get
        Set(ByVal Value As Date)
            WOM_OutwardDate = Value
        End Set
    End Property
    Public Property sWOM_OutwardTime() As String
        Get
            Return (WOM_OutwardTime)
        End Get
        Set(ByVal Value As String)
            WOM_OutwardTime = Value
        End Set
    End Property
    Public Property iWOM_Department() As Integer
        Get
            Return (WOM_Department)
        End Get
        Set(ByVal Value As Integer)
            WOM_Department = Value
        End Set
    End Property
    Public Property iWOM_Customer() As Integer
        Get
            Return (WOM_Customer)
        End Get
        Set(ByVal Value As Integer)
            WOM_Customer = Value
        End Set
    End Property
    Public Property iWOM_InwardID() As Integer
        Get
            Return (WOM_InwardID)
        End Get
        Set(ByVal Value As Integer)
            WOM_InwardID = Value
        End Set
    End Property
    Public Property sWOM_InwardRefNo() As String
        Get
            Return (WOM_InwardRefNo)
        End Get
        Set(ByVal Value As String)
            WOM_InwardRefNo = Value
        End Set
    End Property
    Public Property sWOM_InwardName() As String
        Get
            Return (WOM_InwardName)
        End Get
        Set(ByVal Value As String)
            WOM_InwardName = Value
        End Set
    End Property
    Public Property sWOM_Address() As String
        Get
            Return (WOM_Address)
        End Get
        Set(ByVal Value As String)
            WOM_Address = Value
        End Set
    End Property
    Public Property sWOM_Page() As String
        Get
            Return (WOM_Page)
        End Get
        Set(ByVal Value As String)
            WOM_Page = Value
        End Set
    End Property
    Public Property iWOM_Sensitivity() As Integer
        Get
            Return (WOM_Sensitivity)
        End Get
        Set(ByVal Value As Integer)
            WOM_Sensitivity = Value
        End Set
    End Property
    Public Property sWOM_OutwardRefNo() As String
        Get
            Return (WOM_OutwardRefNo)
        End Get
        Set(ByVal Value As String)
            WOM_OutwardRefNo = Value
        End Set
    End Property
    Public Property iWOM_DispathMode() As Integer
        Get
            Return (WOM_DispathMode)
        End Get
        Set(ByVal Value As Integer)
            WOM_DispathMode = Value
        End Set
    End Property
    Public Property iWOM_ReplyAwaited() As Integer
        Get
            Return (WOM_ReplyAwaited)
        End Get
        Set(ByVal Value As Integer)
            WOM_ReplyAwaited = Value
        End Set
    End Property
    Public Property iWOM_DocumentType() As Integer
        Get
            Return (WOM_DocumentType)
        End Get
        Set(ByVal Value As Integer)
            WOM_DocumentType = Value
        End Set
    End Property
    Public Property sWOM_MailingExpenses() As String
        Get
            Return (WOM_MailingExpenses)
        End Get
        Set(ByVal Value As String)
            WOM_MailingExpenses = Value
        End Set
    End Property
    Public Property sWOM_AttachmentDetails() As String
        Get
            Return (WOM_AttachmentDetails)
        End Get
        Set(ByVal Value As String)
            WOM_AttachmentDetails = Value
        End Set
    End Property
    Public Property sWOM_Remarks() As String
        Get
            Return (WOM_Remarks)
        End Get
        Set(ByVal Value As String)
            WOM_Remarks = Value
        End Set
    End Property
    Public Property sWOM_SendTo() As String
        Get
            Return (WOM_SendTo)
        End Get
        Set(ByVal Value As String)
            WOM_SendTo = Value
        End Set
    End Property
    Public Property iWOM_AttachID() As Integer
        Get
            Return (WOM_AttachID)
        End Get
        Set(ByVal Value As Integer)
            WOM_AttachID = Value
        End Set
    End Property
    Public Property iWOM_CreatedBy() As Integer
        Get
            Return (WOM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            WOM_CreatedBy = Value
        End Set
    End Property
    Public Property dWOM_CreatedOn() As Date
        Get
            Return (WOM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            WOM_CreatedOn = Value
        End Set
    End Property
    Public Property iWOM_UpdatedBy() As Integer
        Get
            Return (WOM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            WOM_UpdatedBy = Value
        End Set
    End Property
    Public Property dWOM_UpdatedOn() As Date
        Get
            Return (WOM_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            WOM_UpdatedOn = Value
        End Set
    End Property
    Public Property iWOM_ApprovedBy() As Integer
        Get
            Return (WOM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            WOM_ApprovedBy = Value
        End Set
    End Property
    Public Property dWOM_ApprovedOn() As Date
        Get
            Return (WOM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            WOM_ApprovedOn = Value
        End Set
    End Property
    Public Property iWOM_DeletedBy() As Integer
        Get
            Return (WOM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            WOM_DeletedBy = Value
        End Set
    End Property
    Public Property dWOM_DeletedOn() As Date
        Get
            Return (WOM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            WOM_DeletedOn = Value
        End Set
    End Property
    Public Property iWOM_RecalledBy() As Integer
        Get
            Return (WOM_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            WOM_RecalledBy = Value
        End Set
    End Property
    Public Property dWOM_RecalledOn() As Date
        Get
            Return (WOM_RecalledOn)
        End Get
        Set(ByVal Value As Date)
            WOM_RecalledOn = Value
        End Set
    End Property
    Public Property sWOM_Status() As String
        Get
            Return (WOM_Status)
        End Get
        Set(ByVal Value As String)
            WOM_Status = Value
        End Set
    End Property
    Public Property sWOM_Delflag() As String
        Get
            Return (WOM_Delflag)
        End Get
        Set(ByVal Value As String)
            WOM_Delflag = Value
        End Set
    End Property
    Public Property iWOM_CompID() As Integer
        Get
            Return (WOM_CompID)
        End Get
        Set(ByVal Value As Integer)
            WOM_CompID = Value
        End Set
    End Property
End Structure
Public Class clsOutward
    Dim objDBL As New DBHelper
    Public Function LoadInwardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iInward As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from WF_Inward_Masters where WIM_PKID =" & iInward & " and WIM_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSelectedOutwardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iOutwardID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from WF_Outward_Masters where WOM_ID=" & iOutwardID & " and WOM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOutwardDashboard(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iMonthID As Integer,
                                         ByVal iDeptCustID As Integer, ByVal sDeptOrCust As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow
        Try
            dtDisplay.Columns.Add("WOM_ID")
            dtDisplay.Columns.Add("OutwardNo")
            dtDisplay.Columns.Add("OutwardReferenceNo")
            dtDisplay.Columns.Add("DepartmentCustomer")
            dtDisplay.Columns.Add("InwardNo")
            dtDisplay.Columns.Add("InwardName")
            dtDisplay.Columns.Add("Status")

            sSql = "Select WOM_ID,WOM_OutwardNo,WOM_OutwardRefNo,WOM_InwardRefNo,WOM_InwardName,WOM_Delflag,Org_Name from wf_Outward_Masters"
            sSql = sSql & " Left Join sad_Org_Structure On Org_Node=WOM_Department And Org_CompID=" & iACID & ""
            sSql = sSql & " Where (WOM_CreatedBy=" & iUserID & ") and WOM_CompID=" & iACID & "" ' or WOM_SendTo in (" & iUserID & ")
            If iMonthID > 0 Then
                sSql = sSql & " And WOM_MonthID=" & iMonthID & ""
            End If
            If sDeptOrCust = "D" Then
                If iDeptCustID > 0 Then
                    sSql = sSql & " And WOM_Department=" & iDeptCustID & ""
                End If
            ElseIf sDeptOrCust = "C" Then
                If iDeptCustID > 0 Then
                    sSql = sSql & " And WOM_Customer=" & iDeptCustID & ""
                End If
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("WOM_ID") = dt.Rows(i)("WOM_ID")
                    dRow("OutwardNo") = dt.Rows(i)("WOM_OutwardNo")
                    dRow("OutwardReferenceNo") = dt.Rows(i)("WOM_OutwardRefNo")
                    dRow("DepartmentCustomer") = dt.Rows(i)("Org_Name")
                    dRow("InwardNo") = dt.Rows(i)("WOM_InwardRefNo")
                    dRow("InwardName") = dt.Rows(i)("WOM_InwardName")
                    If dt.Rows(i)("WOM_Delflag").ToString() = "A" Then
                        dRow("Status") = "Activated"
                    ElseIf dt.Rows(i)("WOM_Delflag").ToString() = "D" Then
                        dRow("Status") = "De-Activated"
                    ElseIf dt.Rows(i)("WOM_Delflag") = "W" Then
                        dRow("Status") = "Waiting For Approval"
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveOutward(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iInwardID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update wf_Outward_Masters Set WOM_Delflag='A',WOM_Status='U',WOM_ApprovedOn=getdate(),WOM_ApprovedBy=" & iUsrID & ",WOM_IPAddress='" & sIPAddress & "' "
            sSql = sSql & " where WOM_ID=" & iInwardID & " and WOM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ActivateOutward(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iInwardID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update wf_Outward_Masters set WOM_Delflag='A',WOM_Status='U',WOM_RecalledOn=getdate(),WOM_RecalledBy=" & iUsrID & ",WOM_IPAddress='" & sIPAddress & "' "
            sSql = sSql & "where WOM_ID=" & iInwardID & " and WOM_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveOutwardMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal objOutward As strOutward)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOutward.iWOM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_OutwardNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_OutwardNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_MonthID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOutward.iWOM_MonthID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOutward.iWOM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_OutwardDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOutward.dWOM_OutwardDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_OutwardTime", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOutward.sWOM_OutwardTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Department", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Customer", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_Customer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_InwardID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_InwardID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_InwardRefNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_InwardRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_InwardName", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_InwardName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Address", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Page", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOutward.sWOM_Page
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Sensitivity", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_Sensitivity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_OutwardRefNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_OutwardRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_DispathMode", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_DispathMode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_ReplyAwaited", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_ReplyAwaited
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_DocumentType", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_DocumentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_MailingExpenses", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_MailingExpenses
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_AttachmentDetails", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_AttachmentDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Remarks", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_SendTo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_SendTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_AttachID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_CreatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objOutward.sWOM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOutward.sWOM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOutward.iWOM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@WOM_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objOutward.sWOM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spWF_Outward_Masters", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
