Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class clsServiceCharges
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Private iEMPC_ID As Integer
    Private iEMPC_CAT_ID As Integer
    Private iEMPC_DAYS As Integer
    Private iEMPC_HOURS As Integer
    Private iEMPC_CHARGES As Double
    Private iEMPC_KMCharges As Double
    Private sEMPC_DelFlag As String
    Private sEMPC_Status As String
    Private iEMPC_CompID As Integer
    Private iEMPC_YearID As Integer
    Private iEMPC_CreatedBy As Integer
    Private dEMPC_CreatedOn As DateTime
    Private iEMPC_UpdatedBy As Integer
    Private dEMPC_UpdatedOn As DateTime
    Private sEMPC_IPAddress As String
    Private sEMPC_Remarks As String
    Private sEMPC_CRemarks As String
    Private iEMPC_CCreatedBy As Integer
    Private iEMPC_CUpdatedBy As Integer
    Private sEMPC_CDelFlag As String

    Public Property EMPC_ID() As Integer
        Get
            Return (iEMPC_ID)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_ID = Value
        End Set
    End Property
    Public Property EMPC_CAT_ID() As Integer
        Get
            Return (iEMPC_CAT_ID)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_CAT_ID = Value
        End Set
    End Property
    Public Property EMPC_DAYS() As Integer
        Get
            Return (iEMPC_DAYS)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_DAYS = Value
        End Set
    End Property
    Public Property EMPC_HOURS() As Integer
        Get
            Return (iEMPC_HOURS)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_HOURS = Value
        End Set
    End Property
    Public Property EMPC_CHARGES() As Double
        Get
            Return (iEMPC_CHARGES)
        End Get
        Set(ByVal Value As Double)
            iEMPC_CHARGES = Value
        End Set
    End Property
    Public Property EMPC_KMCharges() As Double
        Get
            Return (iEMPC_KMCharges)
        End Get
        Set(ByVal Value As Double)
            iEMPC_KMCharges = Value
        End Set
    End Property
    Public Property EMPC_DelFlag() As String
        Get
            Return (sEMPC_DelFlag)
        End Get
        Set(ByVal Value As String)
            sEMPC_DelFlag = Value
        End Set
    End Property
    Public Property EMPC_IPAddress() As String
        Get
            Return (sEMPC_IPAddress)
        End Get
        Set(ByVal Value As String)
            sEMPC_IPAddress = Value
        End Set
    End Property
    Public Property EMPC_Status() As String
        Get
            Return (sEMPC_Status)
        End Get
        Set(ByVal Value As String)
            sEMPC_Status = Value
        End Set
    End Property
    Public Property EMPC_CompID() As Integer
        Get
            Return (iEMPC_CompID)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_CompID = Value
        End Set
    End Property
    Public Property EMPC_YearID() As Integer
        Get
            Return (iEMPC_YearID)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_YearID = Value
        End Set
    End Property
    Public Property EMPC_CreatedBy() As Integer
        Get
            Return (iEMPC_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_CreatedBy = Value
        End Set
    End Property
    Public Property EMPC_CreatedOn() As DateTime
        Get
            Return (dEMPC_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dEMPC_CreatedOn = Value
        End Set
    End Property
    Public Property EMPC_UpdatedBy() As Integer
        Get
            Return (iEMPC_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_UpdatedBy = Value
        End Set
    End Property
    Public Property EMPC_UpdatedOn() As DateTime
        Get
            Return (dEMPC_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dEMPC_UpdatedOn = Value
        End Set
    End Property
    Public Property EMPC_Remarks() As String
        Get
            Return (sEMPC_Remarks)
        End Get
        Set(ByVal Value As String)
            sEMPC_Remarks = Value
        End Set
    End Property
    Public Property EMPC_CRemarks() As String
        Get
            Return (sEMPC_CRemarks)
        End Get
        Set(ByVal Value As String)
            sEMPC_CRemarks = Value
        End Set
    End Property
    Public Property EMPC_CCreatedBy() As Integer
        Get
            Return (iEMPC_CCreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_CCreatedBy = Value
        End Set
    End Property
    Public Property EMPC_CUpdatedBy() As Integer
        Get
            Return (iEMPC_CUpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iEMPC_CUpdatedBy = Value
        End Set
    End Property
    Public Property EMPC_CDelFlag() As String
        Get
            Return (sEMPC_CDelFlag)
        End Get
        Set(ByVal Value As String)
            sEMPC_CDelFlag = Value
        End Set
    End Property
    Public Function LoadServiceChargesDetails(ByVal sAC As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iId As Integer, ByVal iDesgId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select * from SAD_EmpCategory_Charges where EMPC_CAT_ID=" & iDesgId & " And EMPC_CompID=" & iCompID & " And EMPC_YearID=" & iYearID & ""
            If iId > 0 Then
                sSql = sSql & " And EMPC_ID=" & iId & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveServiceCharges(ByVal sAC As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objService As clsServiceCharges) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CAT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_CAT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_HOURS", OleDb.OleDbType.Integer, 50)
            ObjParam(iParamCount).Value = objService.EMPC_HOURS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CHARGES", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objService.EMPC_CHARGES
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_DelFlag", OleDb.OleDbType.Char, 4)
            ObjParam(iParamCount).Value = objService.EMPC_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objService.EMPC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_Remarks", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objService.EMPC_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_EmpCategory_Charges", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveConveyanceCharges(ByVal sAC As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objService As clsServiceCharges) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CAT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_CAT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_KMCharges", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objService.EMPC_KMCharges
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objService.EMPC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CRemarks", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objService.EMPC_CRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CCreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_CCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CUpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objService.EMPC_CUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EMPC_CDelFlag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objService.EMPC_CDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_EmpConveyance_Charges", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAllServiceDetailsGrid(ByVal sAC As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dtRes As New DataTable
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim sSql As String
        Dim i As Integer
        Try
            dtRes.Columns.Add("SrNo")
            dtRes.Columns.Add("ID")
            dtRes.Columns.Add("DesignationID")
            dtRes.Columns.Add("Designation")
            dtRes.Columns.Add("PerDayCharges")
            dtRes.Columns.Add("NoOfHoursPerDay")
            dtRes.Columns.Add("Status")

            sSql = "Select EMPC_ID,EMPC_CAT_ID,EMPC_Charges,EMPC_Hours,EMPC_KMCharges,EMPC_DelFlag,EMPC_CAT_ID,Mas_Description From SAD_EmpCategory_Charges"
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master On mas_ID=EMPC_CAT_ID And mas_CompID = " & iCompID & ""
            sSql = sSql & " Where EMPC_CompID = " & iCompID & " And EMPC_YearID=" & iYearID & " And EMPC_Status='Saved'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtRes.NewRow
                    dRow("SrNo") = i + 1
                    dRow("ID") = dt.Rows(i)("EMPC_ID")
                        dRow("DesignationID") = dt.Rows(i)("EMPC_CAT_ID")
                        dRow("Designation") = dt.Rows(i)("Mas_Description").ToString
                        dRow("PerDayCharges") = dt.Rows(i)("EMPC_Charges").ToString
                        dRow("NoOfHoursPerDay") = dt.Rows(i)("EMPC_Hours").ToString
                    If IsDBNull(dt.Rows(i)("EMPC_DelFlag")) = False Then
                        If dt.Rows(i)("EMPC_DelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dt.Rows(i)("EMPC_DelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("EMPC_DelFlag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
                        End If
                    End If
                    dtRes.Rows.Add(dRow)
                Next
            End If
            Return dtRes
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveServiceDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iRiskID As Integer, ByVal sFlag As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update SAD_EmpCategory_Charges set "
            If sFlag = "Created" Then
                sSql = sSql & "EMPC_DelFlag='A',EMPC_AppBy=" & iUserID & ",EMPC_AppOn=Getdate(),"
            ElseIf sFlag = "DeActivated" Then
                sSql = sSql & "EMPC_DelFlag='D',EMPC_DeletedBy=" & iUserID & ",EMPC_DeletedOn=Getdate(),"
            ElseIf sFlag = "Activated" Then
                sSql = sSql & "EMPC_DelFlag='A',EMPC_RecalledBy=" & iUserID & ",EMPC_RecalledOn=Getdate(),"
            End If
            sSql = sSql & " EMPC_IPAddress='" & sIPAddress & "' where EMPC_CAT_ID=" & iRiskID & " and EMPC_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function BindAllConveyanceDetailsGrid(ByVal sAC As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dtRes As New DataTable
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim sSql As String
        Dim i As Integer
        Try
            dtRes.Columns.Add("SrNo")
            dtRes.Columns.Add("ID")
            dtRes.Columns.Add("DesignationID")
            dtRes.Columns.Add("Designation")
            dtRes.Columns.Add("PerKmCharges")
            dtRes.Columns.Add("Status")

            sSql = "Select EMPC_ID,EMPC_CAT_ID,EMPC_Charges,EMPC_Hours,EMPC_KMCharges,EMPC_CDelFlag,EMPC_CAT_ID,Mas_Description From SAD_EmpCategory_Charges"
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master On mas_ID=EMPC_CAT_ID And mas_CompID = " & iCompID & ""
            sSql = sSql & " Where EMPC_CompID = " & iCompID & " And EMPC_YearID=" & iYearID & " And EMPC_CStatus='Saved'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtRes.NewRow
                    dRow("SrNo") = i + 1
                    dRow("ID") = dt.Rows(i)("EMPC_ID")
                    dRow("DesignationID") = dt.Rows(i)("EMPC_CAT_ID")
                    dRow("Designation") = dt.Rows(i)("Mas_Description").ToString
                    dRow("PerKmCharges") = dt.Rows(i)("EMPC_KMCharges").ToString
                    If IsDBNull(dt.Rows(i)("EMPC_CDelFlag")) = False Then
                        If dt.Rows(i)("EMPC_CDelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dt.Rows(i)("EMPC_CDelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("EMPC_CDelFlag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
                        End If
                    End If
                    dtRes.Rows.Add(dRow)
                Next
            End If
            Return dtRes
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveConveyanceDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iRiskID As Integer, ByVal sFlag As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update SAD_EmpCategory_Charges set "
            If sFlag = "Created" Then
                sSql = sSql & "EMPC_CDelFlag='A',EMPC_CAppBy=" & iUserID & ",EMPC_CAppOn=Getdate(),"
            ElseIf sFlag = "DeActivated" Then
                sSql = sSql & "EMPC_CDelFlag='D',EMPC_CDeletedBy=" & iUserID & ",EMPC_CDeletedOn=Getdate(),"
            ElseIf sFlag = "Activated" Then
                sSql = sSql & "EMPC_CDelFlag='A',EMPC_CRecalledBy=" & iUserID & ",EMPC_CRecalledOn=Getdate(),"
            End If
            sSql = sSql & " EMPC_IPAddress='" & sIPAddress & "' Where EMPC_CAT_ID=" & iRiskID & " and EMPC_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
