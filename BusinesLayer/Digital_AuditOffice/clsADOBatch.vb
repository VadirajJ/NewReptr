
Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsADOBatch
    Private objDBL As New DatabaseLayer.DBHelper
    Public Structure BatchScan
        Public BT_ID As Integer
        Public BT_CustomerID As Integer
        Public BT_TrType As Integer
        Public BT_BatchID As Integer
        Public BT_BatchNo As String
        Public BT_AttachID As Integer
        Public BT_BatchTitle As String
        Public BT_NFT As Integer
        Public BT_Voucherno As String
        Public BT_Datetime As DateTime
        Public BT_Comment As String
        Public BT_DebitTotal As Double
        Public BT_CreditTotal As Double
        Public BT_Delflag As String
        Public BT_Status As String
        Public BT_CompID As Integer
        Public BT_YearID As Integer
        Public BT_CrBy As Integer
        Public BT_CrOn As DateTime
        Public BT_IPAddress As String
    End Structure
    Public Function GetPageDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPageDetailsID As Integer) As DataTable
        Dim sSql As String
        Dim dtPageDetails As New DataTable
        Try
            sSql = " select Pge_DETAILS_ID as pge_basename from edt_page where pge_folder = " & iPageDetailsID & " and Pge_CompID=" & iACID & " group by Pge_DETAILS_ID"
            dtPageDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtPageDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAttachdetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPageDetailsID As Integer) As DataTable
        Dim sSql As String
        Dim dtPageDetails As New DataTable
        Try
            sSql = " select pge_basename,pge_cabinet,pge_subcabinet,pge_folder from edt_page where pge_details_id = " & iPageDetailsID & " and Pge_CompID=" & iACID & " order by pge_basename"
            dtPageDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtPageDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBatchDetails(ByVal sAC As String, ByVal objBatch As clsADOBatch.BatchScan)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CustomerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CustomerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_TransactionType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_BatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_BatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_BatchNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objBatch.BT_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Title", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objBatch.BT_BatchTitle
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_NFT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_NFT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Vouchers", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objBatch.BT_Voucherno
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Date", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBatch.BT_Datetime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Comments", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objBatch.BT_Comment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_DebitTotal", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objBatch.BT_DebitTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CreditTotal", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CreditTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Delflag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objBatch.BT_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objBatch.BT_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objBatch.BT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spADO_BATCH", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBatchNo(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal iTrType As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select fol_folid,fol_name From edt_folder Where fol_cabinet in (select CBN_ID from edt_cabinet where CBN_parent =" & iCustomerID & " and CBN_NAME='" & iTrType & "') And fol_Status='A'"
            BindBatchNo = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetADOdata(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubcabinet As Integer) As String
        Dim sSql As String = "" : Dim sStr As String = ""
        Dim sMaximumID As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sAC, "Select IsNull(count(FOL_FOLID)+1,1) From edt_FOLder where fol_cabinet=" & iSubcabinet & "")
            sStr = "BS-" & sMaximumID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBatchno(ByVal sAC As String, ByVal iACID As Integer, ByVal iFolderID As Integer) As String
        Dim sSql As String = "" : Dim sStr As String = ""
        Dim sBatchID As String = ""
        Try
            sBatchID = objDBL.SQLGetDescription(sAC, "Select FOL_NAME From edt_FOLder where fol_folid=" & iFolderID & "")
            Return sBatchID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDashboard(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustomerid As Integer) As DataTable
        Dim sSql As String
        Dim dtPageDetails As New DataTable, dtColumn As New DataTable
        'Dim drow As DataRow
        Try
            'dtColumn.Columns.Add("SrNo")
            'dtColumn.Columns.Add("AtchID")
            'dtColumn.Columns.Add("Cabinet")
            'dtColumn.Columns.Add("Trtype")
            'dtColumn.Columns.Add("Batch")
            'dtColumn.Columns.Add("NFT")

            'sSql = "select bt_customerid,bt_transactiontype,bt_batchid,bt_batchno,BT_NFT"
            'sSql = sSql & " from ado_batch where bt_compid = " & iACID & ""
            'If iCustomerid >= 1 Then
            '    sSql = sSql & " and bt_customerid = " & iCustomerid & ""
            'End If
            'sSql = sSql & " group by bt_customerid, bt_transactiontype, bt_batchid, bt_batchno, BT_NFT"
            'dtPageDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            'For i = 0 To dtPageDetails.Rows.Count - 1
            '    drow = dtColumn.NewRow
            '    drow("SrNo") = i + 1
            '    drow("AtchID") = dtPageDetails.Rows(i)("bt_batchid")
            '    drow("Cabinet") = GetCabinet(sAC, iACID, dtPageDetails.Rows(i)("bt_customerid"))
            '    drow("Trtype") = GetCabinet(sAC, iACID, dtPageDetails.Rows(i)("bt_transactiontype"))
            '    drow("Batch") = GetBatchno(sAC, iACID, dtPageDetails.Rows(i)("bt_batchid"))
            '    drow("NFT") = dtPageDetails.Rows(i)("BT_NFT")
            '    dtColumn.Rows.Add(drow)
            'Next

            sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, count(a.BT_NFT) as NFT, b.cbn_name as Cabinet,"
            sSql = sSql & " c.cbn_name as Trtype, d.fol_name as Batch,d.fol_folid as AtchID from ado_batch a"
            sSql = sSql & " left join edt_cabinet b on a.bt_customerid = b.CBN_ID"
            sSql = sSql & " left join edt_cabinet c on  a.bt_transactiontype = c.CBN_ID"
            sSql = sSql & " left join edt_folder d on a.bt_batchid = d.fol_folid where a.bt_compid = " & iACID & ""
            If iCustomerid >= 1 Then
                sSql = sSql & " and a.bt_customerid = " & iCustomerid & ""
            End If
            sSql = sSql & " group by b.cbn_name,c.cbn_name, d.fol_name,d.fol_folid "
            dtColumn = objDBL.SQLExecuteDataTable(sAC, sSql)

            Return dtColumn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal iCabinetID As Integer) As String
        Dim sSql As String = "" : Dim sStr As String = ""
        Dim sBatchID As String = ""
        Try
            sBatchID = objDBL.SQLGetDescription(sAC, "Select cbn_name From edt_cabinet where CBN_ID=" & iCabinetID & "")
            Return sBatchID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBatchDetails(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTrType As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select fol_folid, fol_name From edt_folder Where fol_cabinet =" & iTrType & " And fol_Status='A'"
            BindBatchDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBatchAttached(sNameSpace As String, ByVal iCompID As Integer, ByVal iAttachid As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select BT_NFT,BT_Vouchers,BT_Date,BT_Comments,bt_status from ado_batch Where bt_attachid =" & iAttachid & ""
            GetBatchAttached = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDetails(sNameSpace As String, ByVal iCompID As Integer, ByVal iCabinetid As Integer, ByVal iTransid As Integer, ByVal iBatchid As Integer, ByVal iAttachid As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select BT_NFT,BT_Vouchers,BT_Date,BT_Comments,bt_status from ado_batch Where BT_CustomerID =" & iCabinetid & " and BT_TransactionType =" & iTransid & " and BT_BatchID =" & iBatchid & " and BT_AttachID =" & iAttachid & " "
            GetDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDigitalVouchingDashboard(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustomerid As Integer) As DataTable
        Dim sSql As String
        Dim dtPageDetails As New DataTable, dtColumn As New DataTable
        Try

            sSql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SrNo, count(a.BT_NFT) as NFT, b.cbn_id, b.cbn_name as Cabinet,"
            sSql = sSql & " c.cbn_name as Trtype, d.fol_name as Batch,d.fol_folid as AtchID, e.usr_FullName as Cr_By, CONVERT(varchar,a.BT_Date,103) as BT_Date from ado_batch a"
            sSql = sSql & " left join edt_cabinet b on a.bt_customerid = b.CBN_ID"
            sSql = sSql & " left join edt_cabinet c on  a.bt_transactiontype = c.CBN_ID"
            sSql = sSql & " left join edt_folder d on a.bt_batchid = d.fol_folid "
            sSql = sSql & " Inner join Sad_UserDetails e on e.usr_Id = a.BT_CrBy where a.bt_compid = " & iACID & ""
            If iCustomerid >= 1 Then
                'sSql = sSql & " and a.bt_customerid = " & iCustomerid & ""
                sSql = sSql & " and a.bt_customerid in(Select isnull(Cbn_id,0) from edt_Cabinet where CBN_Department=" & iCustomerid & " and CBN_Parent=-1 and CBN_status ='A' and CBN_DelFlag = 'A' and CBN_CompID= " & iACID & ")"
            End If
            sSql = sSql & " group by b.cbn_name,c.cbn_name, d.fol_name,d.fol_folid,b.cbn_id,e.usr_FullName, BT_Date order by BT_Date desc"
            dtColumn = objDBL.SQLExecuteDataTable(sAC, sSql)

            Return dtColumn
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
