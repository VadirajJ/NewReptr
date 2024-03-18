Imports System.Data
Imports DatabaseLayer
Imports BusinesLayer
Public Class ClsAssetTransactionDeletion
    Private objDBL As New DatabaseLayer.DBHelper
    Private clsgeneral As New clsGRACeGeneral

    Private FXATD_ID As Integer
    Private FXATD_TransactionDate As Date
    Private FXATD_TrType As Integer
    Private FXATD_BillId As Integer
    Private FXATD_PaymentType As Integer
    Private FXATD_DbOrCr As Integer
    Private FXATD_Head As Integer
    Private FXATD_GL As Integer
    Private FXATD_SubGL As Integer
    Private FXATD_Debit As Decimal
    Private FXATD_Credit As Decimal
    Private FXATD_CreatedOn As Date
    Private FXATD_CreatedBy As Integer
    Private FXATD_ApprovedBy As Integer
    Private FXATD_ApprovedOn As Date
    Private FXATD_Deletedby As Integer
    Private FXATD_DeletedOn As Date
    Private FXATD_Status As String
    Private FXATD_YearID As Integer
    Private FXATD_Operation As String
    Private FXATD_CompID As Integer
    Private FXATD_IPAddress As String

    Public Property sFXATD_IPAddress() As String
        Get
            Return (FXATD_IPAddress)
        End Get
        Set(ByVal Value As String)
            FXATD_IPAddress = Value
        End Set
    End Property
    Public Property sFXATD_Operation() As String
        Get
            Return (FXATD_Operation)
        End Get
        Set(ByVal Value As String)
            FXATD_Operation = Value
        End Set
    End Property
    Public Property iFXATD_YearID() As Integer
        Get
            Return (FXATD_YearID)
        End Get
        Set(ByVal Value As Integer)
            FXATD_YearID = Value
        End Set
    End Property
    Public Property sFXATD_Status() As String
        Get
            Return (FXATD_Status)
        End Get
        Set(ByVal Value As String)
            FXATD_Status = Value
        End Set
    End Property
    Public Property dFXATD_DeletedOn() As Date
        Get
            Return (FXATD_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            FXATD_DeletedOn = Value
        End Set
    End Property
    Public Property iFXATD_Deletedby() As Integer
        Get
            Return (FXATD_Deletedby)
        End Get
        Set(ByVal Value As Integer)
            FXATD_Deletedby = Value
        End Set
    End Property
    Public Property dFXATD_ApprovedOn() As Date
        Get
            Return (FXATD_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            FXATD_ApprovedOn = Value
        End Set
    End Property
    Public Property iFXATD_ApprovedBy() As Integer
        Get
            Return (FXATD_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            FXATD_ApprovedBy = Value
        End Set
    End Property
    Public Property dFXATD_CreatedOn() As Date
        Get
            Return (FXATD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            FXATD_CreatedOn = Value
        End Set
    End Property
    Public Property iFXATD_CreatedBy() As Integer
        Get
            Return (FXATD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            FXATD_CreatedBy = Value
        End Set
    End Property
    Public Property dFXATD_Credit() As Decimal
        Get
            Return (FXATD_Credit)
        End Get
        Set(ByVal Value As Decimal)
            FXATD_Credit = Value
        End Set
    End Property
    Public Property dFXATD_Debit() As Decimal
        Get
            Return (FXATD_Debit)
        End Get
        Set(ByVal Value As Decimal)
            FXATD_Debit = Value
        End Set
    End Property
    Public Property iFXATD_SubGL() As Integer
        Get
            Return (FXATD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            FXATD_SubGL = Value
        End Set
    End Property
    Public Property iFXATD_GL() As Integer
        Get
            Return (FXATD_GL)
        End Get
        Set(ByVal Value As Integer)
            FXATD_GL = Value
        End Set
    End Property
    Public Property iFXATD_Head() As Integer
        Get
            Return (FXATD_Head)
        End Get
        Set(ByVal Value As Integer)
            FXATD_Head = Value
        End Set
    End Property
    Public Property iFXATD_CompID() As Integer
        Get
            Return (FXATD_CompID)
        End Get
        Set(ByVal Value As Integer)
            FXATD_CompID = Value
        End Set
    End Property
    Public Property iFXATD_DbOrCr() As Integer
        Get
            Return (FXATD_DbOrCr)
        End Get
        Set(ByVal Value As Integer)
            FXATD_DbOrCr = Value
        End Set
    End Property
    Public Property iFXATD_PaymentType() As Integer
        Get
            Return (FXATD_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            FXATD_PaymentType = Value
        End Set
    End Property
    Public Property iFXATD_BillId() As Integer
        Get
            Return (FXATD_BillId)
        End Get
        Set(ByVal Value As Integer)
            FXATD_BillId = Value
        End Set
    End Property
    Public Property iFXATD_TrType() As Integer
        Get
            Return (FXATD_TrType)
        End Get
        Set(ByVal Value As Integer)
            FXATD_TrType = Value
        End Set
    End Property
    Public Property dFXATD_TransactionDate() As Date
        Get
            Return (FXATD_TransactionDate)
        End Get
        Set(ByVal Value As Date)
            FXATD_TransactionDate = Value
        End Set
    End Property
    Public Property iFXATD_ID() As Integer
        Get
            Return (FXATD_ID)
        End Get
        Set(ByVal Value As Integer)
            FXATD_ID = Value
        End Set
    End Property
    Public Function LoadAssetDeletion(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from ACC_General_Master where Mas_master in(select Mas_Id from ACC_Master_Type where Mas_Type='Asset Deletion Reasons')and Mas_CompID =" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFxdAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_DelFlag='A' and AM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iAFADId As Integer, ByVal sAsstDelTrans As String, ByVal sAssetType As String, ByVal sAssetId As String, ByVal iDeletionId As Integer, ByVal sDelDate As Date, ByVal sDelnDate As Date, ByVal sSaleSrpVale As String, ByVal sDesc As String, ByVal sQuantity As String, ByVal sDelLocation As String, ByVal iPaymntType As Integer, ByVal sChqueNo As String, ByVal dChquercvdDate As Date, ByVal sAsstDesc As String, ByVal sItemDesc As String, ByVal iItemQty As Double, ByVal dPurchDate As Date, ByVal dComsinDate As Date, ByVal dAmount As String, ByVal sSts As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Acc_FixedAssetDeletion where AFAD_ID = '" & iAFADId & "' and AFAD_AssetTransNo='" & sAsstDelTrans & "' and AFAD_CompID =" & iCompID & " and AFAD_YearID =" & iYearid & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Acc_FixedAssetDeletion set AFAD_DeltnItemDescription='" & sDesc & "',"
                sSql = sSql & " AFAD_AssetDelID = '" & iDeletionId & "',AFAD_AssetDelDate=" & clsgeneral.FormatDtForRDBMS(sDelDate, "I") & ", AFAD_AssetDeletionDate=" & clsgeneral.FormatDtForRDBMS(sDelnDate, "I") & ", AFAD_DeltnItemDescription=" & sDesc & ","
                sSql = sSql & "AFAD_AssetDeltnAmount ='" & sSaleSrpVale & "',AFAD_AssetDelQuantity = " & sQuantity & ","
                sSql = sSql & "AFAD_AssetDelLocation = '" & sDelLocation & "',AFAD_AssetDelPmntType='" & iPaymntType & "',"
                sSql = sSql & "AFAD_AssetDelChqeNo = '" & sChqueNo & "',AFAD_AssetDelChqeDate='" & dChquercvdDate & "',AFAD_Delflag='D', AFAD_Description='" & sAsstDesc & "',AFAD_ItemDescription='" & sItemDesc & "', AFAD_Quantity='" & iItemQty & "',AFAD_AssetAmount='" & dAmount & "',AFAD_CommissionDate=" & clsgeneral.FormatDtForRDBMS(dComsinDate, "I") & ",AFAD_PurchaseDate=" & clsgeneral.FormatDtForRDBMS(dPurchDate, "I") & ",AFAD_Status='" & sSts & "' "
                sSql = sSql & " where AFAD_ID = '" & iAFADId & "' and AFAD_AssetTransNo='" & sAsstDelTrans & "' and AFAD_CompID =" & iCompID & " and AFAD_YearID =" & iYearid & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("AFAD_ID")
            Else
                sSql = "select isnull(max(AFAD_ID)+1,1) from Acc_FixedAssetDeletion where AFAD_YearID='" & iYearid & "'"
                iMax = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
                sSql = "" : sSql = "Insert into Acc_FixedAssetDeletion(AFAD_ID,AFAD_AssetTransNo,AFAD_AssetType,AFAD_ItemCode,AFAD_AssetDelID,AFAD_AssetDelDate,AFAD_AssetDeletionDate,AFAD_DeltnItemDescription,AFAD_AssetDeltnAmount,AFAD_AssetDelQuantity,AFAD_AssetDelLocation,AFAD_AssetDelPmntType,AFAD_AssetDelChqeNo,AFAD_AssetDelChqeDate,AFAD_CompID,AFAD_YearID,AFAD_Delflag,AFAD_Description,AFAD_ItemDescription,AFAD_Quantity,AFAD_AssetAmount,AFAD_CommissionDate,AFAD_PurchaseDate,AFAD_Status)"
                sSql = sSql & " Values('" & iMax & "','" & sAsstDelTrans & "','" & sAssetType & "','" & sAssetId & "','" & iDeletionId & "'," & clsgeneral.FormatDtForRDBMS(sDelDate, "I") & ","
                sSql = sSql & " " & clsgeneral.FormatDtForRDBMS(sDelnDate, "I") & ",'" & sDesc & "','" & sSaleSrpVale & "'," & sQuantity & ",'" & sDelLocation & "','" & iPaymntType & "','" & sChqueNo & "','" & dChquercvdDate & "' ,'" & iCompID & "','" & iYearid & "','D','" & sAsstDesc & "','" & sItemDesc & "','" & iItemQty & "','" & dAmount & "'," & clsgeneral.FormatDtForRDBMS(dComsinDate, "I") & "," & clsgeneral.FormatDtForRDBMS(dPurchDate, "I") & ",'" & sSts & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function showDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAFAA_ID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from Acc_FixedAssetDeletion Where AFAD_ID=" & iAFAA_ID & " And AFAD_CompID=" & iCompID & " And AFAD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDesc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sAssetDesc As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select GL_Desc,gl_ID from Chart_Of_Accounts Where GL_Desc ='" & sAssetDesc & "' and gl_compID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetType1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iassetId As Integer) As String
        Dim sSql As String = ""
        Dim sDesc As String
        Try
            sSql = "Select GL_Desc from Chart_Of_Accounts Where gl_ID =" & iassetId & " and gl_compID=" & iCompID & ""
            sDesc = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustId As Integer, ByVal sStatus As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try

            dt.Columns.Add("AFAD_ID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("Asset")
            dt.Columns.Add("AssetDeletionRefNo")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("Status")

            sSql = "select * from Acc_FixedAssetDeletion where AFAD_CompID=" & iACID & " and AFAD_CustomerName=" & iCustId & " and AFAD_YearID =" & iyearId & ""
            If sStatus > 0 Then
                sSql = sSql & " and AFAD_AssetDeletion=" & sStatus & ""
            End If

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("AFAD_ID")) = False Then
                    dRow("AFAD_ID") = dtDetails.Rows(i)("AFAD_ID")
                End If
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("AFAD_AssetClass")) = False Then
                    dRow("AssetClass") = objDBL.SQLExecuteScalar(sAC, "select AM_Description from Acc_AssetMaster where AM_ID= " & dtDetails.Rows(i)("AFAD_AssetClass") & " and AM_CustId =" & dtDetails.Rows(i)("AFAD_CustomerName") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_Asset")) = False Then
                    dRow("Asset") = objDBL.SQLExecuteScalar(sAC, "select AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_AssetType= " & dtDetails.Rows(i)("AFAD_AssetClass") & " and AFAM_CustId =" & dtDetails.Rows(i)("AFAD_CustomerName") & " and AFAM_ID =" & dtDetails.Rows(i)("AFAD_Asset") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAD_TransNo")) = False Then
                    dRow("AssetDeletionRefNo") = dtDetails.Rows(i)("AFAD_TransNo")
                End If

                If dtDetails.Rows(i)("AFAD_AssetDeletion") = 1 Then
                    dRow("TransactionType") = "Sold"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDeletion") = 2 Then
                    dRow("TransactionType") = "Transferred"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDeletion") = 3 Then
                    dRow("TransactionType") = "Stolen"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDeletion") = 4 Then
                    dRow("TransactionType") = "Destroyed"
                ElseIf dtDetails.Rows(i)("AFAD_AssetDeletion") = 5 Then
                    dRow("TransactionType") = "Obsolete"
                End If

                If dtDetails.Rows(i)("AFAD_Delflag") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                Else
                    dRow("Status") = "Approved"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AFAA_ID")
            dt.Columns.Add("AFAA_AssetNo")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("PurchaseDate")


            sSql = "select * from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iACID & ""
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("AFAA_ID")) = False Then
                    dRow("AFAA_ID") = dtDetails.Rows(i)("AFAA_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetNo")) = False Then
                    dRow("AFAA_AssetNo") = dtDetails.Rows(i)("AFAA_AssetNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("AssetType") = objDBL.SQLExecuteScalar(sAC, "select AM_Description from Acc_AssetMaster where AM_ID= " & dtDetails.Rows(i)("AFAA_AssetType") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetRefNo")) = False Then
                    dRow("AssetRefNo") = dtDetails.Rows(i)("AFAA_AssetRefNo")
                End If
                Try
                    If IsDBNull(dtDetails.Rows(i)("AFAA_SupplierName")) = False Then
                        dRow("Supplier") = objDBL.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("AFAA_SupplierName") & "")
                    Else
                        dRow("Supplier") = ""
                    End If
                Catch ex As Exception
                    dRow("Supplier") = ""
                End Try
                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("PurchaseDate") = clsgeneral.FormatDtForRDBMS(dtDetails.Rows(i)("AFAA_PurchaseDate"), "D")
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As Integer
        Dim sSql As String = ""
        Dim Grp As Integer
        Try
            sSql = "select IsNull(count(*),0)+1 from Acc_FixedAssetDeletion where AFAD_AssetType='" & iglID & "' and AFAD_CompID=" & iCompID & ""
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSql))
            Return Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetTypeNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As String) As String
        Dim sSql As String = ""
        Try
            sSql = "select gl_glcode from chart_of_accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            GetAssetTypeNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select gl_glcode from Chart_Of_Accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            LoadAssetNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingItemCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal assettype As Integer, ByVal iyearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAM_ID,AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_CompID=" & iCompID & " and AFAM_AssetType=" & assettype & " and AFAM_Status='A'"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAD_ID,AFAD_AssetTransNo from Acc_FixedAssetDeletion where AFAD_CompID=" & iCompID & " order by AFAD_ID asc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
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
    Public Function SaveFixedAssetTransactionDetails(ByVal sAC As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objAsstTrndel As ClsAssetTransactionDeletion) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAsstTrndel.dFXATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_TrType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstTrndel.dFXATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstTrndel.dFXATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objAsstTrndel.sFXATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_YearID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrndel.iFXATD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Operation ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstTrndel.sFXATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_IPAddress ", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAsstTrndel.sFXATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_FXDTransactions_Details", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSavedTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try

            dc = New DataColumn("ID", GetType(String))
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
            dc = New DataColumn("DebitOrCredit", GetType(String))
            dt.Columns.Add(dc)

            sSql = "" : sSql = "select A.FXATD_ID,A.FXATD_Head,A.FXATD_PaymentType,A.FXATD_Gl,A.FXATD_SubGL,A.FXATD_Debit,A.FXATD_DbOrCr,"
            sSql = sSql & "A.FXATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc,"
            sSql = sSql & "D.Opn_DebitAmt as GLDebit, D.Opn_CreditAmount as GLCredit "
            sSql = sSql & "from Acc_FXDTransactions_Details A join chart_of_Accounts B on "
            sSql = sSql & "A.FXATD_BillId = " & iPaymentID & " and A.FXATD_TRType = 11 and A.FXATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            sSql = sSql & "A.FXATD_SubGL = C.gl_ID and A.FXATD_BillId = " & iPaymentID & " left join acc_Opening_Balance D on "
            sSql = sSql & "D.Opn_GLID = A.FXATD_Gl and D.Opn_YearID = " & iYearID & "   order by a.FXAtd_id "
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = ds.Tables(0).Rows(i)("FXATD_ID")

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("FXATD_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_GL").ToString()) = False Then
                        dr("GLID") = ds.Tables(0).Rows(i)("FXATD_GL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_SubGL").ToString()) = False Then
                        dr("SubGLID") = ds.Tables(0).Rows(i)("FXATD_SubGL").ToString()
                    End If


                    If IsDBNull(ds.Tables(0).Rows(i)("GLCode").ToString()) = False Then
                        dr("GLCode") = ds.Tables(0).Rows(i)("GLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GLDescription").ToString()) = False Then
                        dr("GLDescription") = ds.Tables(0).Rows(i)("GLDescription").ToString()

                        If IsDBNull(ds.Tables(0).Rows(i)("GLDebit").ToString()) = False Then
                            If ds.Tables(0).Rows(i)("GLDebit").ToString() <> "0.00" Then
                                dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLDebit").ToString()
                            End If
                        End If

                        If IsDBNull(ds.Tables(0).Rows(i)("GLCredit").ToString()) = False Then
                            If ds.Tables(0).Rows(i)("GLCredit").ToString() <> "0.00" Then
                                dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLCredit").ToString()
                            End If
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLCode").ToString()) = False Then
                        dr("SubGL") = ds.Tables(0).Rows(i)("SubGLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLDesc").ToString()) = False Then
                        dr("SubGLDescription") = ds.Tables(0).Rows(i)("SubGLDesc").ToString()

                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_Debit").ToString()) = False Then
                        dr("Debit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("FXATD_Debit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_Credit").ToString()) = False Then
                        dr("Credit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("FXATD_Credit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_DbOrCr").ToString()) = False Then
                        dr("DebitOrCredit") = ds.Tables(0).Rows(i)("FXATD_DbOrCr")
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemDescription(ByVal sAC As String, ByVal iACID As Integer, ByVal item As String, ByVal AssetType As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_FixedAssetAdditionDel where AFAA_ItemDescription='" & item & "' and AFAA_AssetType=" & AssetType & " and AFAA_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDelQty(ByVal sAC As String, ByVal iACID As Integer, ByVal itemcode As String, ByVal itemType As Integer) As Double
        Dim sSql As String
        Dim dt As New DataTable
        Dim iCount As Integer = 0
        Dim iDelqty As Integer = 0
        Try
            sSql = "Select AFAD_Quantity from Acc_FixedAssetDeletion where AFAD_CompID=" & iACID & " and AFAD_ItemCode='" & itemcode & "' and AFAD_AssetType='" & itemType & "' and  AFAD_Status<>'RS' order by AFAD_ID desc"
            iCount = objDBL.SQLExecuteScalar(sAC, sSql)
            If iCount = 0 Then
                sSql = "Select AFAD_Quantity  from Acc_FixedAssetDeletion where AFAD_CompID=" & iACID & " and AFAD_ItemCode='" & itemcode & "'and AFAD_AssetType='" & itemType & "' and  AFAD_Status='D' order by AFAD_ID desc"
                iDelqty = objDBL.SQLExecuteScalar(sAC, sSql)
                Return iDelqty
            Else
                Return iCount
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    ''Attachments
    Public Function BindAttachFilesCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Count(PGE_BASENAME) from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "' and FOL_CompID=" & iCompID & ") And pge_CompID=" & iCompID & " "
            BindAttachFilesCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return BindAttachFilesCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFiles(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select pge_Orignalfilename,pge_ext,pge_createdon from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "' and FOL_CompID=" & iCompID & ") And pge_CompID=" & iCompID & " "
            BindAttachFiles = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindAttachFiles
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBaseID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From EDT_Page Where PGE_CABINET=" & iCabinet & " And PGE_SUBCABINET=" & iSubCabinet & " And PGE_Folder=" & iFolder & " And PGE_CompID=" & iCompID & " "
            GetBaseID = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetBaseID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateDeletionStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetId As Integer, ByVal sStatus As String, ByVal iDelQty As Integer)
        Dim sSql As String = ""
        Try
            If iDelQty > 0 Then
                sSql = "update Acc_FixedAssetDeletion set AFAD_Status='" & sStatus & "', AFAD_Delflag='Y',AFAD_Quantity=" & iDelQty & " where AFAD_ID=" & iAssetId & " and AFAD_CompID=" & iCompID & ""
            Else
                sSql = "update Acc_FixedAssetDeletion set AFAD_Status='" & sStatus & "', AFAD_Delflag='Y' where AFAD_ID=" & iAssetId & " and AFAD_CompID=" & iCompID & ""
            End If

            objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDelId(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAfadId As Integer) As DataSet
        Dim sSql As String = ""
        Try
            sSql = "Select AFAD_AssetDelID,AFAD_AssetDelQuantity,AFAD_Quantity From Acc_FixedAssetDeletion Where AFAD_ID=" & iAfadId & "  And AFAd_CompID=" & iCompID & " "
            GetDelId = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return GetDelId
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
