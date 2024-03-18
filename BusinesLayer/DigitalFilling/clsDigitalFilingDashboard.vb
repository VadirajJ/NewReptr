Imports System.Data.OleDb
Imports System.IO
Imports System.Security.Cryptography

Public Class clsDigitalFilingDashboard
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Private iPGE_BASENAME As Integer
    Private iPGE_CABINET As Integer
    Private iPGE_FOLDER As Integer
    Private iPGE_DOCUMENT_TYPE As Integer
    Private sPGE_TITLE As String
    Private dPGE_DATE As DateTime
    Private iPge_DETAILS_ID As Integer
    Private iPge_CreatedBy As Integer
    Private sPGE_OBJECT As String
    Private iPGE_PAGENO As Integer
    Private sPGE_EXT As String
    Private sPGE_KeyWORD As String
    Private sPGE_OCRText As String
    Private iPGE_SIZE As Integer
    Private iPGE_CURRENT_VER As Integer
    Private sPGE_STATUS As String
    Private iPGE_SubCabinet As Integer
    Private iPge_UpdatedBy As Integer
    Private iPGE_QC_UsrGrpId As Integer
    Private sPGE_FTPStatus As String
    Private iPGE_batch_name As Integer
    Private spge_OrignalFileName As String
    Private iPGE_BatchID As Integer
    Private iPGE_OCRDelFlag As Integer
    Private iPge_CompID As Integer
    Private spge_Delflag As String


    Public Property iPGECABINET() As Integer
        Get
            Return (iPGE_CABINET)
        End Get
        Set(ByVal Value As Integer)
            iPGE_CABINET = Value
        End Set
    End Property

    Public Property iPGEBASENAME() As Integer
        Get
            Return (iPGE_BASENAME)
        End Get
        Set(ByVal Value As Integer)
            iPGE_BASENAME = Value
        End Set
    End Property
    Public Property iPGEFOLDER() As Integer
        Get
            Return (iPGE_FOLDER)
        End Get
        Set(ByVal Value As Integer)
            iPGE_FOLDER = Value
        End Set
    End Property

    Public Property iPGEDOCUMENTTYPE() As Integer
        Get
            Return (iPGE_DOCUMENT_TYPE)
        End Get
        Set(ByVal Value As Integer)
            iPGE_DOCUMENT_TYPE = Value
        End Set
    End Property
    Public Property sPGETITLE() As String
        Get
            Return (sPGE_TITLE)
        End Get
        Set(ByVal Value As String)
            sPGE_TITLE = Value
        End Set
    End Property
    Public Property spgeDelflag() As String
        Get
            Return (spge_Delflag)
        End Get
        Set(ByVal Value As String)
            spge_Delflag = Value
        End Set
    End Property
    Public Property dPGEDATE() As Date
        Get
            Return (dPGE_DATE)
        End Get
        Set(ByVal Value As Date)
            dPGE_DATE = Value
        End Set
    End Property
    Public Property iPgeDETAILSID() As Integer
        Get
            Return (iPge_DETAILS_ID)
        End Get
        Set(ByVal Value As Integer)
            iPge_DETAILS_ID = Value
        End Set
    End Property
    Public Property sPGEOBJECT() As String
        Get
            Return (sPGE_OBJECT)
        End Get
        Set(ByVal Value As String)
            sPGE_OBJECT = Value
        End Set
    End Property
    Public Property iPgeCreatedBy() As Integer
        Get
            Return (iPge_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPge_CreatedBy = Value
        End Set
    End Property
    Public Property iPGEPAGENO() As Integer
        Get
            Return (iPGE_PAGENO)
        End Get
        Set(ByVal Value As Integer)
            iPGE_PAGENO = Value
        End Set
    End Property
    Public Property sPGEEXT() As String
        Get
            Return (sPGE_EXT)
        End Get
        Set(ByVal Value As String)
            sPGE_EXT = Value
        End Set
    End Property
    Public Property sPGEKeyWORD() As String
        Get
            Return (sPGE_KeyWORD)
        End Get
        Set(ByVal Value As String)
            sPGE_KeyWORD = Value
        End Set
    End Property
    Public Property sPGEOCRText() As String
        Get
            Return (sPGE_OCRText)
        End Get
        Set(ByVal Value As String)
            sPGE_OCRText = Value
        End Set
    End Property
    Public Property iPGESIZE() As Integer
        Get
            Return (iPGE_SIZE)
        End Get
        Set(ByVal Value As Integer)
            iPGE_SIZE = Value
        End Set
    End Property
    Public Property iPGECURRENT_VER() As Integer
        Get
            Return (iPGE_CURRENT_VER)
        End Get
        Set(ByVal Value As Integer)
            iPGE_CURRENT_VER = Value
        End Set
    End Property
    Public Property sPGESTATUS() As String
        Get
            Return (sPGE_STATUS)
        End Get
        Set(ByVal Value As String)
            sPGE_STATUS = Value
        End Set
    End Property
    Public Property iPGESubCabinet() As Integer
        Get
            Return (iPGE_SubCabinet)
        End Get
        Set(ByVal Value As Integer)
            iPGE_SubCabinet = Value
        End Set
    End Property
    Public Property iPgeUpdatedBy() As Integer
        Get
            Return (iPge_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPge_UpdatedBy = Value
        End Set
    End Property
    Public Property iPGEQCUsrGrpId() As Integer
        Get
            Return (iPGE_QC_UsrGrpId)
        End Get
        Set(ByVal Value As Integer)
            iPGE_QC_UsrGrpId = Value
        End Set
    End Property
    Public Property sPGEFTPStatus() As String
        Get
            Return (sPGE_FTPStatus)
        End Get
        Set(ByVal Value As String)
            sPGE_FTPStatus = Value
        End Set
    End Property
    Public Property iPGEbatchname() As Integer
        Get
            Return (iPGE_batch_name)
        End Get
        Set(ByVal Value As Integer)
            iPGE_batch_name = Value
        End Set
    End Property
    Public Property spgeOrignalFileName() As String
        Get
            Return (spge_OrignalFileName)
        End Get
        Set(ByVal Value As String)
            spge_OrignalFileName = Value
        End Set
    End Property
    Public Property iPGEBatchID() As Integer
        Get
            Return (iPGE_BatchID)
        End Get
        Set(ByVal Value As Integer)
            iPGE_BatchID = Value
        End Set
    End Property
    Public Property iPGEOCRDelFlag() As Integer
        Get
            Return (iPGE_OCRDelFlag)
        End Get
        Set(ByVal Value As Integer)
            iPGE_OCRDelFlag = Value
        End Set
    End Property
    Public Property iPgeCompID() As Integer
        Get
            Return (iPge_CompID)
        End Get
        Set(ByVal Value As Integer)
            iPge_CompID = Value
        End Set
    End Property

    Public Function LoadCustomers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtCustomers As New DataTable
        Try
            sSql = "Select CUST_ID,CUST_NAME From SAD_CUSTOMER_MASTER where CUST_CompID=" & iACID & " And CUST_DELFLG='A' Order by Cust_Name"
            dtCustomers = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtCustomers
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckEmpOrCust(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String, sEmpCust As String
        Dim iEmpUserID As Integer = 0, iCustomerID As Integer = 0
        Try
            If iUserID > 0 Then
                sSql = "Select usr_Id From Sad_UserDetails Where usr_Id=" & iUserID & " And Usr_CompId=" & iACID & " And usr_Node<>0 And usr_CompanyId=0"
                iEmpUserID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            End If
            If iEmpUserID > 0 Then
                sEmpCust = "E"
            Else
                sEmpCust = "C"
            End If
            Return sEmpCust
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAllAttachedDocuments(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iSelectedCompID As Integer, ByVal sEmpCust As String) As DataTable
        Dim sSql As String, sSql1 As String, sCustomers As String = ""
        Dim dtColumn As New DataTable, dt As New DataTable, dtCustomers As New DataTable
        Dim drow As DataRow
        Dim iEmpUserID As Integer = 0, iCustomerID As Integer = 0
        Try
            dtColumn.Columns.Add("AtchID")
            dtColumn.Columns.Add("DFAttachID")
            dtColumn.Columns.Add("FilePath")
            dtColumn.Columns.Add("FileName")
            dtColumn.Columns.Add("Size")
            dtColumn.Columns.Add("Extension")
            dtColumn.Columns.Add("CreatedBy")
            dtColumn.Columns.Add("CreatedOn")

            sSql = "Select * From edt_page where Pge_CompID=" & iACID & " And (PGE_STATUS='U')" ' Or PGE_STATUS='S'
            If sEmpCust = "C" Then
                If iUserID > 0 Then
                    sSql = sSql & " And Pge_CreatedBy=" & iUserID & ""
                End If
            End If
            If sEmpCust = "E" Then
                If iSelectedCompID > 0 Then
                    sSql1 = "" : sSql1 = "Select usr_Id From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_CompanyId=" & iSelectedCompID & ""
                    dtCustomers = objDBL.SQLExecuteDataTable(sAC, sSql1)
                    If dtCustomers.Rows.Count > 0 Then
                        For j = 0 To dtCustomers.Rows.Count - 1
                            sCustomers = sCustomers & "," & dtCustomers.Rows(j)("usr_Id")
                        Next
                    End If

                    If sCustomers.StartsWith(",") Then
                        sCustomers = sCustomers.Remove(0, 1)
                    End If

                    If iSelectedCompID > 0 Then
                        If sCustomers <> "" Then
                            sSql = sSql & " And Pge_CreatedBy In (" & sCustomers & ")"
                        Else
                            sSql = sSql & " And Pge_CreatedBy=0"
                        End If
                    End If
                End If
            End If

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtColumn.NewRow
                drow("AtchID") = dt.Rows(i)("PGE_BASENAME")
                drow("DFAttachID") = dt.Rows(i)("PGE_BatchID")
                drow("FilePath") = ""
                drow("FileName") = dt.Rows(i)("pge_OrignalFileName") & dt.Rows(i)("PGE_EXT")
                drow("Size") = String.Format("{0:0.00}", (dt.Rows(i)("PGE_SIZE") / 1024)) & " KB"
                drow("Extension") = dt.Rows(i)("PGE_EXT")
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("Pge_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("Pge_CreatedOn"), "F")
                dtColumn.Rows.Add(drow)
            Next
            Return dtColumn
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function

    Public Function LoadAllSharedDocuments(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iSelectedCompID As Integer, ByVal sEmpCust As String) As DataTable
        Dim sSql As String, sSql1 As String, sCustomers As String = ""
        Dim dtColumn As New DataTable, dt As New DataTable, dtCustomers As New DataTable
        Dim drow As DataRow
        Dim iEmpUserID As Integer = 0, iCustomerID As Integer = 0
        Try
            dtColumn.Columns.Add("DocumentType")
            dtColumn.Columns.Add("FileName")
            dtColumn.Columns.Add("SharedBy")
            dtColumn.Columns.Add("SharedOn")

            sSql = "Select Distinct(PGE_BASENAME),PGE_DOCUMENT_TYPE,pge_OrignalFileName,PGE_EXT,Pge_UpdatedBy,Pge_UpdatedOn,Pge_CreatedBy," 'EMD_ToEmailIDs,
            sSql = sSql & " PGE_STATUS,PGE_TITLE,usr_FullName From edt_page" ',DOT_DOCNAME
            sSql = sSql & " Left Join Sad_UserDetails On usr_Id=Pge_UpdatedBy And Usr_CompId=" & iACID & ""
            'sSql = sSql & " Left Join GRACe_EMailSent_Details On EMD_MstPKID=PGE_BASENAME And EMD_CompID=" & iACID & ""
            'sSql = sSql & " Left Join edt_document_type On DOT_DOCTYPEID=PGE_DOCUMENT_TYPE And DOT_DelFlag='A' And DOT_CompId=" & iACID & ""
            sSql = sSql & " where Pge_CompID=" & iACID & " And PGE_STATUS='S'" ' And EMD_YearID=" & iYearID & ""

            If sEmpCust = "C" Then
                If iUserID > 0 Then
                    sSql = sSql & " And Pge_CreatedBy=" & iUserID & ""
                End If
            End If
            If sEmpCust = "E" Then
                If iSelectedCompID > 0 Then
                    sSql1 = "" : sSql1 = "Select usr_Id From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_CompanyId=" & iSelectedCompID & ""
                    dtCustomers = objDBL.SQLExecuteDataTable(sAC, sSql1)
                    If dtCustomers.Rows.Count > 0 Then
                        For j = 0 To dtCustomers.Rows.Count - 1
                            sCustomers = sCustomers & "," & dtCustomers.Rows(j)("usr_Id")
                        Next
                    End If

                    If sCustomers.StartsWith(",") Then
                        sCustomers = sCustomers.Remove(0, 1)
                    End If

                    If iSelectedCompID > 0 Then
                        If sCustomers <> "" Then
                            sSql = sSql & " And Pge_CreatedBy In (" & sCustomers & ")"
                        Else
                            sSql = sSql & " And Pge_CreatedBy=0"
                        End If
                    End If
                End If
            End If

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtColumn.NewRow
                If IsDBNull(dt.Rows(i)("PGE_DOCUMENT_TYPE")) = False Then
                    If dt.Rows(i)("PGE_DOCUMENT_TYPE") > 0 Then
                        drow("DocumentType") = GetDocumentTypeName(sAC, iACID, dt.Rows(i)("PGE_DOCUMENT_TYPE"))
                    Else
                        drow("DocumentType") = ""
                    End If
                End If
                drow("FileName") = dt.Rows(i)("pge_OrignalFileName") & dt.Rows(i)("PGE_EXT")

                If dt.Rows(i)("PGE_STATUS") = "S" Then
                    'If IsDBNull(dt.Rows(i)("EMD_ToEmailIDs")) = False Then
                    '    drow("SharedWith") = dt.Rows(i)("EMD_ToEmailIDs")
                    'Else
                    '    drow("SharedWith") = ""
                    'End If
                    If IsDBNull(dt.Rows(i)("usr_FullName")) = False Then
                        drow("SharedBy") = dt.Rows(i)("usr_FullName")
                    Else
                        drow("SharedBy") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("Pge_UpdatedOn")) = False Then
                        drow("SharedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("Pge_UpdatedOn"), "F")
                    Else
                        drow("SharedOn") = ""
                    End If
                Else
                    drow("SharedBy") = ""
                    drow("SharedOn") = ""
                End If
                dtColumn.Rows.Add(drow)
            Next
            Return dtColumn
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function LoadAllSharedIndexDocuments(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iSelectedCompID As Integer, ByVal sEmpCust As String) As DataTable
        Dim sSql As String, sSql1 As String, sCustomers As String = ""
        Dim dtColumn As New DataTable, dt As New DataTable, dtCustomers As New DataTable
        Dim drow As DataRow
        Dim iEmpUserID As Integer = 0, iCustomerID As Integer = 0
        Try
            dtColumn.Columns.Add("FileName")
            dtColumn.Columns.Add("Cabinet")
            dtColumn.Columns.Add("SubCabinet")
            dtColumn.Columns.Add("Folder")
            dtColumn.Columns.Add("IndexedBy")
            dtColumn.Columns.Add("IndexedOn")

            sSql = "Select Distinct(PGE_BASENAME),PGE_DOCUMENT_TYPE,pge_OrignalFileName,PGE_EXT,Pge_UpdatedBy,Pge_UpdatedOn,Pge_CreatedBy,"
            sSql = sSql & " PGE_STATUS,PGE_TITLE,usr_FullName,a.CBN_NAME As Cabinet,b.CBN_NAME As SubCabinet,FOL_NAME From edt_page"
            sSql = sSql & " Left Join Sad_UserDetails On usr_Id=Pge_UpdatedBy And Usr_CompId=" & iACID & ""
            sSql = sSql & " Left Join EDT_CABINET a On a.CBN_ID=PGE_CABINET And a.CBN_PARENT=-1"
            sSql = sSql & " Left Join EDT_CABINET b On b.CBN_PARENT=a.CBN_ID And b.CBN_ID=PGE_SubCabinet And b.cbn_DelFlag='A' And b.CBN_PARENT <> -1"
            sSql = sSql & " Left Join EDT_FOLDER On FOL_CABINET=b.CBN_ID And FOL_FOLID=PGE_FOLDER And FOL_STATUS='A'"
            sSql = sSql & " Where PGE_STATUS='I'"
            If sEmpCust = "C" Then
                If iUserID > 0 Then
                    sSql = sSql & " And Pge_CreatedBy=" & iUserID & ""
                End If
            End If
            If sEmpCust = "E" Then
                If iSelectedCompID > 0 Then
                    sSql1 = "" : sSql1 = "Select usr_Id From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_CompanyId=" & iSelectedCompID & ""
                    dtCustomers = objDBL.SQLExecuteDataTable(sAC, sSql1)
                    If dtCustomers.Rows.Count > 0 Then
                        For j = 0 To dtCustomers.Rows.Count - 1
                            sCustomers = sCustomers & "," & dtCustomers.Rows(j)("usr_Id")
                        Next
                    End If

                    If sCustomers.StartsWith(",") Then
                        sCustomers = sCustomers.Remove(0, 1)
                    End If

                    If iSelectedCompID > 0 Then
                        If sCustomers <> "" Then
                            sSql = sSql & " And Pge_CreatedBy In (" & sCustomers & ")"
                        Else
                            sSql = sSql & " And Pge_CreatedBy=0"
                        End If
                    End If
                End If
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtColumn.NewRow
                drow("FileName") = dt.Rows(i)("pge_OrignalFileName") & dt.Rows(i)("PGE_EXT")
                If IsDBNull(dt.Rows(i)("Cabinet")) = False Then 'Cabinet
                    drow("Cabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Cabinet"))
                Else
                    drow("Cabinet") = ""
                End If
                If IsDBNull(dt.Rows(i)("SubCabinet")) = False Then 'SubCabinet
                    drow("SubCabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SubCabinet"))
                Else
                    drow("SubCabinet") = ""
                End If
                If IsDBNull(dt.Rows(i)("FOL_NAME")) = False Then 'Folder
                    drow("Folder") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("FOL_NAME"))
                Else
                    drow("Folder") = ""
                End If
                If dt.Rows(i)("PGE_STATUS") = "I" Then
                    If IsDBNull(dt.Rows(i)("usr_FullName")) = False Then
                        drow("IndexedBy") = dt.Rows(i)("usr_FullName")
                    Else
                        drow("IndexedBy") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("Pge_UpdatedOn")) = False Then
                        drow("IndexedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("Pge_UpdatedOn"), "F")
                    Else
                        drow("IndexedOn") = ""
                    End If
                Else
                    drow("IndexedBy") = ""
                    drow("IndexedOn") = ""
                End If
                dtColumn.Rows.Add(drow)
            Next
            Return dtColumn
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function

    Public Function LoadAllActivitiesofDocuments(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iSelectedCompID As Integer, ByVal sEmpCust As String) As DataTable
        Dim sSql As String, sSql1 As String, sCustomers As String = ""
        Dim dtColumn As New DataTable, dt As New DataTable, dtCustomers As New DataTable
        Dim drow As DataRow
        Dim iEmpUserID As Integer = 0, iCustomerID As Integer = 0
        Try
            dtColumn.Columns.Add("FileName")
            dtColumn.Columns.Add("DocumentType")
            dtColumn.Columns.Add("CreatedBy")
            dtColumn.Columns.Add("CreatedOn")
            dtColumn.Columns.Add("SharedBy")
            dtColumn.Columns.Add("SharedOn")

            sSql = "Select PGE_BASENAME,PGE_DOCUMENT_TYPE,pge_OrignalFileName,PGE_EXT," 'EMD_MstPKID,EMD_ToEmailIDs,
            sSql = sSql & " Pge_CreatedBy,Pge_CreatedOn,Pge_UpdatedBy,Pge_UpdatedOn,PGE_STATUS From edt_page_log" ' Left Join GRACe_EMailSent_Details On EMD_MstPKID=PGE_BASENAME And EMD_CompID=" & iACID & "
            sSql = sSql & " where Pge_CompID=" & iACID & " And (PGE_STATUS='U' Or PGE_STATUS='S' Or PGE_STATUS='I')"

            If sEmpCust = "C" Then
                If iUserID > 0 Then
                    sSql = sSql & " And Pge_CreatedBy=" & iUserID & ""
                End If
            End If
            If sEmpCust = "E" Then
                If iSelectedCompID > 0 Then
                    sSql1 = "" : sSql1 = "Select usr_Id From Sad_UserDetails Where Usr_CompId=" & iACID & " And usr_Node=0 And usr_CompanyId=" & iSelectedCompID & ""
                    dtCustomers = objDBL.SQLExecuteDataTable(sAC, sSql1)
                    If dtCustomers.Rows.Count > 0 Then
                        For j = 0 To dtCustomers.Rows.Count - 1
                            sCustomers = sCustomers & "," & dtCustomers.Rows(j)("usr_Id")
                        Next
                    End If
                End If

                If sCustomers.StartsWith(",") Then
                    sCustomers = sCustomers.Remove(0, 1)
                End If

                If iSelectedCompID > 0 Then
                    If sCustomers <> "" Then
                        sSql = sSql & " And Pge_CreatedBy In (" & sCustomers & ")"
                    Else
                        sSql = sSql & " And Pge_CreatedBy=0"
                    End If
                End If
            End If

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtColumn.NewRow
                drow("FileName") = dt.Rows(i)("pge_OrignalFileName") & dt.Rows(i)("PGE_EXT")
                If IsDBNull(dt.Rows(i)("PGE_DOCUMENT_TYPE")) = False Then
                    If dt.Rows(i)("PGE_DOCUMENT_TYPE") > 0 Then
                        drow("DocumentType") = GetDocumentTypeName(sAC, iACID, dt.Rows(i)("PGE_DOCUMENT_TYPE"))
                    Else
                        drow("DocumentType") = ""
                    End If
                End If

                If dt.Rows(i)("PGE_STATUS") = "U" Then
                    drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("Pge_CreatedBy"))
                    drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("Pge_CreatedOn"), "F")
                    drow("SharedBy") = ""
                    drow("SharedOn") = ""
                ElseIf (dt.Rows(i)("PGE_STATUS") = "S") Or (dt.Rows(i)("PGE_STATUS") = "I") Then
                    drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("Pge_CreatedBy"))
                    drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("Pge_CreatedOn"), "F")
                    drow("SharedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("Pge_UpdatedBy"))
                    drow("SharedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("Pge_UpdatedOn"), "F")
                Else
                    drow("CreatedBy") = ""
                    drow("CreatedOn") = ""
                    drow("SharedBy") = ""
                    drow("SharedOn") = ""
                End If
                dtColumn.Rows.Add(drow)
            Next
            Return dtColumn
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDocumentTypeName(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocID As Integer) As String
        Dim sSql As String, sDocType As String = ""
        Try
            sSql = "Select DOT_DOCNAME From edt_document_type where DOT_DOCTYPEID=" & iDocID & " And DOT_DelFlag='A' order by DOT_DOCNAME"
            sDocType = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sDocType
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDigitalFilingDashboardDetails(ByVal sAC As String) As DataTable
        Dim dtColumn As New DataTable, dtDetails As New DataTable
        Dim sSql As String = "", sSqlDocCount As String = "", sCabinet As String = "", sSubCabinet As String = "", sFolder As String = ""
        Dim dRow As DataRow
        Dim iSlNo As Integer = 0, iDocID As Integer = 0
        Try
            dtColumn.Columns.Add("SlNo")
            dtColumn.Columns.Add("Cabinet")
            dtColumn.Columns.Add("CabinetID")
            dtColumn.Columns.Add("SubCabinet")
            dtColumn.Columns.Add("SubCabinetID")
            dtColumn.Columns.Add("Folder")
            dtColumn.Columns.Add("FolderID")
            dtColumn.Columns.Add("DocumentType")

            sSql = "Select Distinct(FOL_NAME),a.CBN_ID As CabinetID,a.CBN_NAME As Cabinet,b.CBN_ID As SubCabinetID,b.CBN_NAME As SubCabinet,FOL_FOLID"
            sSql = sSql & " From EDT_CABINET a Left Join EDT_CABINET b On b.CBN_PARENT=a.CBN_ID And b.cbn_DelFlag='A' And b.CBN_PARENT <> -1"
            sSql = sSql & " Left Join EDT_FOLDER On FOL_CABINET=b.CBN_ID And FOL_STATUS='A' where a.CBN_PARENT=-1 Order by a.CBN_ID"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dtColumn.NewRow
                dRow("SlNo") = iSlNo + 1

                If IsDBNull(dtDetails.Rows(i)("Cabinet")) = False Then 'Cabinet
                    dRow("Cabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Cabinet"))
                    dRow("CabinetID") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("CabinetID"))
                Else
                    dRow("Cabinet") = ""
                    dRow("CabinetID") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("SubCabinet")) = False Then 'SubCabinet
                    dRow("SubCabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SubCabinet"))
                    dRow("SubCabinetID") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SubCabinetID"))
                Else
                    dRow("SubCabinet") = ""
                    dRow("SubCabinetID") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("FOL_NAME")) = False Then 'Folder
                    dRow("Folder") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("FOL_NAME"))
                    dRow("FolderID") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("FOL_FOLID"))
                Else
                    dRow("Folder") = ""
                    dRow("FolderID") = ""
                End If
                If IsDBNull(dRow("Folder")) = False Then
                    sSqlDocCount = "Select Count(distinct(PGE_Details_ID)) from edt_page where PGE_Folder='" & dRow("FolderID") & "'"
                    iDocID = objDBL.SQLExecuteScalarInt(sAC, sSqlDocCount)
                    If iDocID > 0 Then
                        dRow("DocumentType") = iDocID
                    Else
                        dRow("DocumentType") = "0"
                    End If
                End If
                dtColumn.Rows.Add(dRow)
                iSlNo = iSlNo + 1
            Next
            Return dtColumn
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadIndexDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPGEBASENAME As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select PGE_CABINET,PGE_SubCabinet,PGE_FOLDER,PGE_DOCUMENT_TYPE,PGE_DATE,PGE_TITLE,PGE_BASENAME From edt_page Where Pge_CompID=" & iACID & ""
            sSql = sSql & " And PGE_BASENAME=" & iPGEBASENAME & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDescriptors(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocTypeID As Integer, iEPDBASEID As Integer) As DataTable
        Dim sSql As String, sSql1 As String, sVALUE As String
        Dim dtDescriptors As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("DescriptorID")
            dt.Columns.Add("Descriptor")
            dt.Columns.Add("Value")

            sSql = "select a.des_id,a.Desc_name from EDT_DESCRIPTIOS a,EDT_DOCTYPE_LINK b"
            sSql = sSql & "  where a.des_id=b.edd_dptrid And b.edd_doctypeid= " & iDocTypeID & "  order by a.Desc_name"
            dtDescriptors = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDescriptors.Rows.Count > 0 Then
                For i = 0 To dtDescriptors.Rows.Count - 1
                    dr = dt.NewRow
                    dr("DescriptorID") = dtDescriptors.Rows(i)("des_id")
                    dr("Descriptor") = dtDescriptors.Rows(i)("Desc_name")

                    sSql1 = "Select EPD_VALUE From EDT_PAGE_DETAILS where EPD_DESCID=" & dtDescriptors.Rows(i)("des_id") & ""
                    If iEPDBASEID > 0 Then
                        sSql1 = sSql1 & "And EPD_BASEID=" & iEPDBASEID & ""
                    End If
                    sVALUE = objDBL.SQLExecuteScalar(sAC, sSql1)
                    dr("Value") = sVALUE
                    dt.Rows.Add(dr)
                Next
                Return dt
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadKeyWords(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocTypeID As Integer, iEPDBASEID As Integer) As DataTable
        Dim dt As New DataTable, dtDescriptors As New DataTable
        Dim dr As DataRow
        Dim i As Integer, iCount As Integer = 0, iKeyEndCount As Integer = 0
        Dim sSql As String, sKeyWords As String
        Try
            dt.Columns.Add("Key")

            sSql = "select a.des_id,a.Desc_name from EDT_DESCRIPTIOS a,EDT_DOCTYPE_LINK b"
            sSql = sSql & "  where a.des_id=b.edd_dptrid And b.edd_doctypeid= " & iDocTypeID & "  order by a.Desc_name"
            dtDescriptors = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDescriptors.Rows.Count > 0 Then
                For i = 0 To dtDescriptors.Rows.Count - 1
                    dr = dt.NewRow
                    sSql = "Select EPD_KEYWORD From EDT_PAGE_DETAILS where EPD_DESCID=" & dtDescriptors.Rows(i)("des_id") & ""
                    If iEPDBASEID > 0 Then
                        sSql = sSql & "And EPD_BASEID=" & iEPDBASEID & ""
                    End If
                    sKeyWords = objDBL.SQLExecuteScalar(sAC, sSql)
                    If sKeyWords <> "" Then
                        dr("Key") = sKeyWords
                        iCount = i + 1
                    Else
                        dr("Key") = ""
                    End If
                    dt.Rows.Add(dr)
                Next
                If iCount = 0 Then
                    iKeyEndCount = 3
                ElseIf iCount = 1 Then
                    iKeyEndCount = 2
                ElseIf iCount = 2 Then
                    iKeyEndCount = 1
                ElseIf iCount = 3 Then
                    iKeyEndCount = 0
                End If
                If iKeyEndCount <> 0 Then
                    For j = 0 To iKeyEndCount
                        dr = dt.NewRow
                        dr("Key") = ""
                        dt.Rows.Add(dr)
                    Next
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub RemoveSelectedDocument(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iAttachID As Integer)
        Dim sSql As String
        Try
            sSql = "Update edt_page set PGE_STATUS='D',Pge_DeletedBy=" & iUserID & ",Pge_DeletedOn=Getdate()"
            sSql = sSql & " where Pge_CompID=" & iACID & " And PGE_BASENAME=" & iAttachID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetDocumentPath(ByVal sAC As String, ByVal iACID As Integer, ByVal sPaths As String, ByVal iAttachID As Integer)
        Dim iAtchOle As Integer
        Dim con As New OleDb.OleDbConnection
        Dim Pdr As OleDb.OleDbDataReader
        Dim sSql As String, sInputFilePath As String = ""
        Try
            If iAttachID = 0 Then
                Return ""
            End If
            If sPaths.EndsWith("\") = False Then
                sPaths = sPaths & "\"
            End If

            If System.IO.Directory.Exists(sPaths) = False Then
                System.IO.Directory.CreateDirectory(sPaths)
            End If

            If objDBL.SQLExecuteScalar(sAC, "Select Sad_Config_Value From Sad_Config_Settings Where Sad_Config_Key='FilesInDB' And Sad_CompID=" & iACID & "") = "True" Then
                sSql = "Select USD_PKID,USD_FName,USD_Ext,USD_Status From Uploaded_Shared_Documents where USD_CompID=" & iACID & " And USD_PKID=" & iAttachID & ""
                Pdr = objDBL.SQLDataReader(sAC, sSql)
                If Pdr.HasRows Then
                    While Pdr.Read()
                        sInputFilePath = sPaths & Pdr("USD_FName") & "." & Pdr("USD_Ext")
                        If System.IO.File.Exists(sInputFilePath) = True Then
                            System.IO.File.Delete(sInputFilePath)
                        End If
                        Dim BUFFER(Pdr.GetBytes(iAtchOle, 0, BUFFER, 0, Integer.MaxValue)) As Byte
                        Pdr.GetBytes(iAtchOle, 0, BUFFER, 0, BUFFER.Length)
                        Dim BlobData As New IO.FileStream(sInputFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                        BlobData.Write(BUFFER, 0, BUFFER.Length)
                        BlobData.Close()
                    End While
                Else
                    sInputFilePath = String.Empty
                End If
            Else
                sSql = "Select USD_PKID,USD_FName,USD_Ext From Uploaded_Shared_Documents where USD_CompID=" & iACID & " And USD_PKID=" & iAttachID & ""
                Pdr = objDBL.SQLDataReader(sAC, sSql)
                If Pdr.HasRows Then
                    While Pdr.Read()
                        sInputFilePath = sPaths & "Uploads\" & Pdr("USD_FName") & "." & Pdr("USD_Ext")
                    End While
                Else
                    sInputFilePath = String.Empty
                End If
            End If
            Pdr.Close()
            Return sInputFilePath
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckCabinetFolderDocTypeID(ByVal sAC As String, ByVal iACID As Integer, ByVal iEDTPKID As Integer) As Boolean
        Dim sSql As String = ""
        Dim iDoctype As Integer = 0
        Try
            sSql = "Select PGE_DOCUMENT_TYPE From edt_page Where PGE_BASENAME=" & iEDTPKID & ""
            iDoctype = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If iDoctype > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateEDTPageStatus(ByVal sAC As String, ByVal iUserID As Integer, ByVal iEDTPKID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update edt_page set PGE_STATUS='S',PGE_DOCUMENT_TYPE=1,Pge_UpdatedBy=" & iUserID & ",Pge_UpdatedOn=GetDate() Where PGE_BASENAME=" & iEDTPKID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SavePage(ByVal sAC As String, ByVal iACID As Integer, ByVal objDigitalFilingDashboard As clsDigitalFilingDashboard) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BASENAME", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_BASENAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CABINET", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_CABINET
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_FOLDER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DOCUMENT_TYPE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_DOCUMENT_TYPE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_TITLE", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.sPGE_TITLE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DATE", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.dPGE_DATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_DETAILS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPgeDETAILSID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPge_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPge_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OBJECT", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.sPGE_OBJECT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_PAGENO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_PAGENO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_EXT", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.sPGE_EXT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_KeyWORD", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.sPGE_KeyWORD
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRText", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.sPGE_OCRText
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SIZE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_SIZE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CURRENT_VER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_CURRENT_VER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_STATUS", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.sPGESTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SubCabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_SubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_QC_UsrGrpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_QC_UsrGrpId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FTPStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.sPGE_FTPStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_batch_name", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_batch_name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_OrignalFileName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.spge_OrignalFileName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_BatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRDelFlag", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPGE_OCRDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.iPge_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDigitalFilingDashboard.spge_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_PAGEDigitalFiling", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePageDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPagedetailsID As Integer, iDocumenttypeID As Integer, iDescriptorID As Integer, ByVal sKeyWords As String, ByVal sDescValues As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(7) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_BASEID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iPagedetailsID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_DOCTYPE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDocumenttypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_DESCID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDescriptorID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_KEYWORD ", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = sKeyWords
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_VALUE", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sDescValues
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_PAGE_DETAILSDigitalFiling", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDocumenttype(ByVal sAC As String) As DataTable
        Dim sSql As String
        Dim dtDesc As New DataTable
        Try
            sSql = "Select DOT_DOCTYPEID,DOT_DOCNAME from edt_document_type where DOT_DelFlag='A' order by DOT_DOCNAME"
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtDesc
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCustomerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer) As DataTable

        Dim sSql As String
        Dim dtCustomers As New DataTable
        Try
            sSql = "Select CUST_ID,CUST_NAME From SAD_CUSTOMER_MASTER where CUST_CompID=" & iACID & " and CUST_ID=" & iCustID & " And CUST_DELFLG ='A' Order by Cust_Name"
            dtCustomers = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtCustomers
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
