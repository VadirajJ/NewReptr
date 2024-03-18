Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports System.Web
Public Class clsIndexing
    Private objDBL As New DBHelper
    Private Shared iUsrType As Integer
    Dim sPermlvl As String
    Dim dsMain As New DataSet
    Dim iParGrp As Integer = 0
    Dim sCabPerm As String
    Dim Permdt As DataTable
    Private Shared sMem As String = String.Empty
    Dim dtPerm As New DataTable
    Dim iUsrParGrp As Integer = 0
    Private Shared ObjConnection As OleDb.OleDbConnection
    Dim objGnrl As New clsGeneralFunctions

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
    Private sPGE_RFID As String


    Public Structure BatchScan
        Public BT_ID As Integer
        Public BT_CustomerID As Integer
        Public BT_BatchNo As String
        Public BT_TrType As Integer
        Public BT_NoOfTransaction As Integer
        Public BT_DebitTotal As Double
        Public BT_CreditTotal As Double
        Public BT_Delflag As String
        Public BT_Status As String
        Public BT_CompID As Integer
        Public BT_YearID As Integer
        Public BT_CrBy As Integer
        Public BT_CrOn As DateTime
        Public BT_Operation As String
        Public BT_IPAddress As String
    End Structure

    Public Property sPGERFID() As String
        Get
            Return (sPGE_RFID)
        End Get
        Set(ByVal Value As String)
            sPGE_RFID = Value
        End Set
    End Property
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

    Public Function LoadCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer) As DataTable
        Dim sSql As String
        Dim dtcab As New DataTable
        Try
            sSql = "Select CBN_ID,CBN_NAME from edt_cabinet where CBN_DelFlag='A' and CBN_Parent=-1 and CBN_CompID=" & iACID & " order by CBN_Name"
            dtcab = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtcab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal CabinetId As Integer) As DataTable
        Dim sSql As String
        Dim dtSubcabinet As New DataTable
        Try
            sSql = "Select CBN_ID,CBN_NAME from edt_cabinet where CBN_DelFlag='A' and CBN_Parent= " & CabinetId & " and CBN_DelFlag='A' and  CBN_CompID=" & iACID & " order by CBN_Name"
            dtSubcabinet = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtSubcabinet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFolder(ByVal sAC As String, ByVal iACID As Integer, ByVal SubCabId As Integer) As DataTable
        Dim sSql As String
        Dim dtFolder As New DataTable
        Try
            sSql = "Select Fol_FolID,FOL_Name from edt_Folder where FOL_Delflag='A' and FOL_Cabinet= " & SubCabId & " and FOL_CompID=" & iACID & " order by FOL_Name"
            dtFolder = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtFolder
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDocumentType(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtDocumentType As New DataTable
        Try

            sSql = "Select DOT_DOCTYPEID,DOT_DOCNAME from edt_document_type where DOT_Delflag='A' and DOT_CompID=" & iACID & " order by DOT_DOCNAME"
            dtDocumentType = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtDocumentType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescriptors(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocTypeID As Integer) As DataTable
        Dim sSql As String
        Dim dtDescriptors As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("DescriptorID")
            dt.Columns.Add("Descriptor")

            sSql = "select a.des_id,a.Desc_name from EDT_DESCRIPTOR a,EDT_DOCTYPE_LINK b"
            sSql = sSql & "  where a.des_id=b.edd_dptrid and b.edd_doctypeid= " & iDocTypeID & "  order by a.Desc_name"
            dtDescriptors = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDescriptors.Rows.Count > 0 Then
                For i = 0 To dtDescriptors.Rows.Count - 1
                    dr = dt.NewRow
                    dr("DescriptorID") = dtDescriptors.Rows(i)("des_id")
                    dr("Descriptor") = dtDescriptors.Rows(i)("Desc_name")
                    dt.Rows.Add(dr)
                Next
                Return dt
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadKeyWords() As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("Key")
            For i = 0 To 3
                dr = dt.NewRow
                dr("Key") = ""
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImagePath(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String
        Dim str As String
        Try

            sSql = "Select SAD_Config_Value from sad_config_settings where SAD_Config_Key = 'ImgPath'"
            str = objDBL.SQLExecuteScalar(sAC, sSql)
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ISFileinDB(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String
        Dim str As String
        Try

            sSql = "Select SAD_Config_Value from sad_config_settings where SAD_Config_Key = 'FilesInDB'"
            str = objDBL.SQLExecuteScalar(sAC, sSql)
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateImageSettings(ByVal sAC As String, ByVal iACID As Integer, ByVal iBaseId As Long, ByVal iPageID As Long)
        Dim ssql As String
        Try
            ssql = "Select * from edt_image_settings where img_Form = 'S' and img_IMGID = " & iPageID & ""
            If objDBL.DBCheckForRecord(sAC, ssql) = True Then
                objDBL.SQLExecuteNonQuery(sAC, "Update EDT_IMAGE_Settings Set img_Form = 'I' , img_IMGID = " & iBaseId & "  where img_Form = 'S' and img_IMGID = " & iPageID & "")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SavePage(ByVal sAC As String, ByVal iACID As Integer, ByVal objIndex As clsIndexing) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BASENAME", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_BASENAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CABINET", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_CABINET
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_FOLDER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DOCUMENT_TYPE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_DOCUMENT_TYPE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_TITLE", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objIndex.sPGE_TITLE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DATE", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objIndex.dPGE_DATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_DETAILS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPgeDETAILSID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPge_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OBJECT", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objIndex.sPGE_OBJECT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_PAGENO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_PAGENO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_EXT", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objIndex.sPGE_EXT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_KeyWORD", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objIndex.sPGE_KeyWORD
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRText", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objIndex.sPGE_OCRText
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SIZE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_SIZE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CURRENT_VER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_CURRENT_VER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_STATUS", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objIndex.sPGESTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SubCabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_SubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_QC_UsrGrpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_QC_UsrGrpId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FTPStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objIndex.sPGE_FTPStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_batch_name", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_batch_name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_OrignalFileName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objIndex.spge_OrignalFileName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_BatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRDelFlag", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_OCRDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPge_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objIndex.spge_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_RFID", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_PAGE", 1, Arr, ObjParam)
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

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_PAGE_DETAILS", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    '    Public Function GetMemberGroups(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrId As Integer) As String
    '        Dim sSql As String, sMemGrps As String = ""
    '        Dim dt As New DataTable
    '        Try
    '            sSql = "Select gld_grplvlid from sad_grplvl_members where gld_userid = " & iUsrId
    '            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    sMemGrps = sMemGrps & "," & dt.Rows(i)("gld_grplvlid")
    '                Next
    '            End If

    '            If sMemGrps.StartsWith(",") = True Then
    '                sMemGrps = sMemGrps.Remove(0, 1)
    '            End If

    '            If sMemGrps.EndsWith(",") = True Then
    '                sMemGrps = sMemGrps.Remove(Len(sMemGrps) - 1, 1)
    '            End If
    '            Return sMemGrps
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Public Function GetUserType(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserId As String) As Integer
    '        Dim sSql As String
    '        Try
    '            sSql = "Select usr_IsSuperuser from sad_userdetails where usr_id=" & iUserId & ""
    '            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Public Function GetUserParGrp(ByVal sAC As String, ByVal iACID As Integer, ByVal iLogUsrID As Integer) As Integer
    '        Dim sSql As String
    '        Try
    '            sSql = "Select usr_LevelGrp from sad_Userdetails where usr_id=" & iLogUsrID & ""
    '            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Public Function GetParGrpID(ByVal sAC As String, ByVal iACID As Integer, ByVal iCabId As Integer) As Integer
    '        Dim sSql As String
    '        Try
    '            sSql = "Select CBN_ParGrp from edt_cabinet where CBN_Node=" & iCabId & ""
    '            Return (objDBL.SQLExecuteScalarInt(sAC, sSql))
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Public Function CheckForGrpHead(ByVal sAC As String, ByVal iACID As Integer, ByVal iGrpId As Integer, ByVal iUsrId As Integer) As Integer
    '        Dim sSql As String

    '        Try
    '            sSql = "Select Gld_GrpLvlPosn from sad_grplvl_members where Gld_userId=" & iUsrId & " And GLD_GrpLvlId=" & iGrpId & ""
    '            Return objDBL.SQLExecuteScalarInt(sAC, sSql)

    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Public Function GetAllValues(ByVal sFieldName As String, ByVal sTableName As String) As String
    '        Dim pdr As OleDb.OleDbDataReader
    '        Dim psqlConn As OleDb.OleDbConnection
    '        Dim pstrSQL As String = ""
    '        Dim sRet As String = ""
    '        Try
    '            pstrSQL = "Select " & sFieldName & " From " & sTableName & ""
    '            psqlConn = ObjConnection
    '            Dim psqlCmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(pstrSQL, psqlConn)
    '            pdr = psqlCmd.ExecuteReader
    '            If pdr.HasRows Then
    '                Do While pdr.Read
    '                    sRet = sRet & pdr(0) & ";"
    '                Loop
    '            End If
    '            pdr.Close()
    '            'psqlConn.Close()

    '            If InStr(sRet, ";") <> 0 Then
    '                sRet = Left(sRet, Len(sRet) - 1)
    '            End If
    '            Return sRet
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Private Function GetPermCabinets(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As String, ByVal sGrpID As String) As String
    '        Dim sSql As String
    '        Dim Arr() As String
    '        Dim i As Integer
    '        Dim sCabId As String = ""
    '        Dim sRet As String
    '        Dim sFArr() As String
    '        Try
    '            Arr = Split(sGrpID, ",")
    '            For i = 0 To UBound(Arr)
    '                sSql = "edt_cabinet_permission where CBP_Grpid = " & Arr(i) & " And (CBP_UsrId=" & iUserID & " Or CBP_UsrId=0)"
    '                sRet = GetAllValues("CBP_CabId", sSql)

    '                If Len(sRet) <> 0 Then

    '                    If Right(sRet, 1) = ";" Then
    '                        sRet = Left(sRet, Len(sRet) - 1)
    '                    End If
    '                    sCabId = sCabId & ";" & sRet & ";"
    '                End If
    '            Next
    '            sSql = "Edt_cabinet_permission where cbp_ptype = 'E'"
    '            sCabId = sCabId & GetAllValues("CBP_CabId", sSql)
    '            sCabId = Replace(sCabId, ";", ",")

    '            If Len(Trim(sCabId)) = 0 Then
    '                sCabId = "0"
    '            End If

    '            sFArr = Split(sCabId, ",")
    '            For i = 0 To UBound(sFArr)
    '                If Val(sFArr(i)) <> 0 Then
    '                    GetPermCabinets = GetPermCabinets & "," & Val(sFArr(i))
    '                End If
    '            Next
    '            If Left(GetPermCabinets, 1) = "," Then
    '                GetPermCabinets = Right(GetPermCabinets, Len(GetPermCabinets) - 1)
    '            End If
    '            If Right(GetPermCabinets, 1) = "," Then
    '                GetPermCabinets = Left(GetPermCabinets, Len(GetPermCabinets) - 1)
    '            End If
    '            Return GetPermCabinets

    '        Catch ex As Exception
    '            Throw
    '        End Try

    '    End Function
    '    Private Function GetMainPermDS(ByVal sAC As String, ByVal iACID As Integer, ByVal iCabId As Integer, ByVal iUserId As Int16, ByVal iGrpId As Int16, Optional ByVal ChkType As Integer = 0) As DataTable
    '        Dim sSql, sCab As String
    '        Dim dtPerm As DataTable
    '        Dim dr As OleDb.OleDbDataReader
    '        Try
    '            sCab = ""
    '            'Check For Group Head
    '            If ChkType = 2 Then
    '                sMem = GetMemberGroups(sAC, iACID, iUserId)
    '                iUsrType = GetUserType(sAC, iACID, iUserId)
    '            End If
    '            If (iUsrType = 1) Then
    '                sPermlvl = "PU"
    '                sSql = "Select * from edt_cabinet where CBN_Node=" & iCabId & ""
    '                dsMain = objDBL.SQLExecuteDataSet(sAC, sSql)

    '            ElseIf (CheckForGrpHead(sAC, iACID, iGrpId, iUserId) = 1) Then
    '                'If 1 = 1 Then
    '                sPermlvl = "GH"
    '                sSql = "Select * from edt_cabinet where CBN_Node=" & iCabId & ""
    '                dsMain = objDBL.SQLExecuteDataSet(sAC, sSql)
    '            ElseIf ChkType <> 1 Then
    '                'If (CheckForGrpMember(iGrpId, iUserId) = True) Then
    '                'strsql = "select * from edt_cabinet left outer join edt_cabinet_permission on cbn_node=cbp_cabid where CBN_Node=" & iCabId & " and CBN_ParGrp=" & iGrpId & " and CBP_CabId not in ( select distinct(CBP_CabId)  from edt_cabinet_permission where CBP_UsrId=" & iUserId & " or CBP_Grpid in (" & sMem & ") or CBP_PType='E') "
    '                If (sCabPerm = String.Empty) Then
    '                    sCabPerm = GetPermCabinets(sAC, iACID, iUserId, sMem)
    '                End If
    '                sSql = " Select *  from edt_cabinet left outer join edt_cabinet_permission on cbn_node=cbp_cabid where  CBN_ParGrp in (" & sMem & ") and CBN_DelStatus='A' and CBN_Node= " & iCabId & " "
    '                If Val(sCabPerm) <> 0 Then
    '                    sSql = sSql & " and (CBP_CabId not in (" & sCabPerm & " ) or CBP_CabId is Null)  "
    '                End If
    '                If objDBL.DBCheckForRecord(sAC, sSql) = True Then
    '                    dsMain = objDBL.SQLExecuteDataSet(sAC, sSql)
    '                    sPermlvl = "PG"
    '                Else
    '                    GoTo LP
    '                End If
    '            Else
    'LP:             dsMain = BuildPermDataSet(iUserId, iCabId, sMem, ChkType)
    '                If dsMain.Tables.Count <> 0 Then
    '                    dtPerm = dsMain.Tables(0)
    '                    dtPerm = GetFinalPermForDS(dtPerm)
    '                    Return dtPerm
    '                Else
    '                    Dim MyDt As New DataTable
    '                    Return MyDt
    '                End If
    '            End If
    '            Return dsMain.Tables(0)

    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Private Function GetFinalPermForDS(ByVal dtCab As DataTable) As DataTable
    '        'Dim dtPerm As DataTable
    '        Dim dr As DataRow
    '        'Dim sGrp As String = ""
    '        Dim iCSC, iVCB, iDCB, iMCB, iIND, iSRH, iCFD As Byte
    '        Try
    '            For Each dr In dtCab.Rows
    '                If (UCase(sPermlvl) <> "GH" And UCase(sPermlvl) <> "PG") Then
    '                    If (dr("CBP_Create") = 1) Then
    '                        iCSC = 1
    '                    End If
    '                    If (dr("CBP_Modify") = 1) Then
    '                        iMCB = 1
    '                    End If
    '                    If (dr("CBP_Delete") = 1) Then
    '                        iDCB = 1
    '                    End If
    '                    If (dr("CBP_Create_Folder") = 1) Then
    '                        iCFD = 1
    '                    End If
    '                    If (dr("CBP_Search") = 1) Then
    '                        iSRH = 1
    '                    End If
    '                    If (dr("CBP_Index") = 1) Then
    '                        iIND = 1
    '                    End If
    '                    If (dr("CBP_View") = 1) Then
    '                        iVCB = 1
    '                    End If
    '                    'sGrp = sGrp & "," & dr("CBP_GrpId")
    '                End If
    '            Next
    '            dtCab.BeginInit()
    '            dtCab.Rows(0).Item("CBP_Create") = iCSC
    '            dtCab.Rows(0).Item("CBP_Modify") = iMCB
    '            dtCab.Rows(0).Item("CBP_View") = iVCB
    '            dtCab.Rows(0).Item("CBP_Delete") = iDCB
    '            dtCab.Rows(0).Item("CBP_Create_Folder") = iCFD
    '            dtCab.Rows(0).Item("CBP_Index") = iIND
    '            dtCab.Rows(0).Item("CBP_Search") = iSRH
    '            dtCab.EndInit()
    '            dtCab.AcceptChanges()
    '            Return dtCab
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    'Private Function BuildPermDataSet(ByVal iUserId As Integer, ByVal iCabId As Integer, ByVal sMem As String, ByVal ChkType As Integer, ByVal sConStr As String, ByVal sRDBMS As String) As Object
    '    '    Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
    '    '    Dim iCount As Integer

    '    '    Try
    '    '        objParam(iCount) = New OleDb.OleDbParameter("@p_UsrId", OleDb.OleDbType.Numeric)
    '    '        objParam(iCount).Value = iUserId
    '    '        objParam(iCount).Direction = ParameterDirection.Input
    '    '        iCount = iCount + 1

    '    '        objParam(iCount) = New OleDb.OleDbParameter("@p_CabId", OleDb.OleDbType.Numeric)
    '    '        objParam(iCount).Value = iCabId
    '    '        objParam(iCount).Direction = ParameterDirection.Input
    '    '        iCount = iCount + 1


    '    '        objParam(iCount) = New OleDb.OleDbParameter("@p_Mem", OleDb.OleDbType.VarChar)
    '    '        objParam(iCount).Value = sMem
    '    '        objParam(iCount).Direction = ParameterDirection.Input
    '    '        iCount = iCount + 1

    '    '        objParam(iCount) = New OleDb.OleDbParameter("@p_iRetLvl", OleDb.OleDbType.VarChar)
    '    '        objParam(iCount).Value = 0
    '    '        objParam(iCount).Direction = ParameterDirection.Output
    '    '        objParam(iCount).Size = 1
    '    '        If (ChkType = 2) Then
    '    '            Dim arr() As Object = objDBL.SPFrLoadingUsingDsParam("GetPerDetails", 1, "@p_iRetLvl", objParam)
    '    '            If IsDBNull(arr(1)) = False Then
    '    '                sPermlvl = arr(1)
    '    '            Else
    '    '                sPermlvl = ""
    '    '            End If
    '    '            Return arr(0)
    '    '        Else
    '    '            Return (objDBL.SPFrLoadingUsingDs("GetPerDetails", objParam))
    '    '        End If
    '    '    Catch ex As Exception
    '    '        Throw
    '    '    End Try
    '    'End Function
    '    Public Function GetFinalPermissions(ByVal sAC As String, ByVal iACID As Integer, ByVal iCabId As Integer, ByVal iUserId As Integer, Optional ByVal sPerType As String = "ALL", Optional ByVal iChkType As Integer = 0) As Object
    '        Try

    '            If (iChkType = 2) Then
    '                sCabPerm = String.Empty
    '            End If
    '            sPermlvl = String.Empty
    '            If (iChkType = 2) Then
    '                iParGrp = GetParGrpID(sAC, iACID, iCabId)
    '                iUsrParGrp = GetUserParGrp(sAC, iACID, iUserId)
    '            End If
    '            dtPerm = GetMainPermDS(sAC, iACID, iCabId, iUserId, iParGrp, iChkType)
    '            If (dtPerm.Rows.Count > 0) Then
    '                Select Case UCase(sPerType)
    '                    Case "ALL"
    '                        Dim Ht As New Hashtable
    '                        If (sPermlvl = "PG") Then
    '                            If (iUsrParGrp = iParGrp) Then
    '                                Ht.Add("CModify", 0)
    '                                Ht.Add("CView", 1)
    '                                Ht.Add("CDelete", 0)
    '                                Ht.Add("CCreate", 0)
    '                                Ht.Add("FCreate", 0)
    '                                Ht.Add("CIndex", 1)
    '                                Ht.Add("CSearch", 1)
    '                                Ht.Add("Level", sPermlvl)
    '                            Else
    '                                Ht.Add("CModify", 0)
    '                                Ht.Add("CView", 1)
    '                                Ht.Add("CDelete", 0)
    '                                Ht.Add("CCreate", 0)
    '                                Ht.Add("FCreate", 0)
    '                                Ht.Add("CIndex", 0)
    '                                Ht.Add("CSearch", 1)
    '                                Ht.Add("Level", sPermlvl)
    '                            End If
    '                            Return Ht
    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Ht.Add("CCreate", 1)
    '                            Ht.Add("CModify", 1)
    '                            Ht.Add("CView", 1)
    '                            Ht.Add("CDelete", 1)
    '                            Ht.Add("FCreate", 1)
    '                            Ht.Add("CIndex", 1)
    '                            Ht.Add("CSearch", 1)
    '                            Ht.Add("Level", sPermlvl)
    '                            Return Ht
    '                        Else
    '                            Ht.Add("CCreate", dtPerm.Rows(0).Item("CBP_Create"))
    '                            Ht.Add("CModify", dtPerm.Rows(0).Item("CBP_Modify"))
    '                            Ht.Add("CView", dtPerm.Rows(0).Item("CBP_View"))
    '                            Ht.Add("CDelete", dtPerm.Rows(0).Item("CBP_Delete"))
    '                            Ht.Add("FCreate", dtPerm.Rows(0).Item("CBP_Create_Folder"))
    '                            Ht.Add("CIndex", dtPerm.Rows(0).Item("CBP_Index"))
    '                            Ht.Add("CSearch", dtPerm.Rows(0).Item("CBP_Search"))
    '                            Ht.Add("Level", sPermlvl)
    '                            Return Ht
    '                        End If

    '                    Case "CSC"
    '                        'dsPerm = dsMain
    '                        If (sPermlvl = "PG") Then
    '                            Return 0
    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Return 1
    '                        End If
    '                        Return dtPerm.Rows(0).Item("CBP_Create")
    '                    Case "MCB"
    '                        'dsPerm = dsMain
    '                        If (sPermlvl = "PG") Then
    '                            Return 0
    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Return 1
    '                        End If
    '                        Return dtPerm.Rows(0).Item("CBP_Modify")
    '                    Case "DCB"
    '                        'dsPerm = dsMain
    '                        If (sPermlvl = "PG") Then
    '                            Return 0
    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Return 1
    '                        End If
    '                        Return dtPerm.Rows(0).Item("CBP_Delete")
    '                    Case "VCB"
    '                        'dsPerm = dsMain
    '                        If (sPermlvl = "PG") Then
    '                            Return 1
    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Return 1
    '                        End If
    '                        Return dtPerm.Rows(0).Item("CBP_View")
    '                    Case "CFD"
    '                        'dsPerm = dsMain
    '                        If (sPermlvl = "PG") Then
    '                            Return 0
    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Return 1
    '                        End If
    '                        Return dtPerm.Rows(0).Item("CBP_Create_Folder")
    '                    Case "IDX"
    '                        'dsPerm = dsMain
    '                        If (sPermlvl = "PG") Then
    '                            If (iUsrParGrp = iParGrp) Then
    '                                Return 1
    '                            Else
    '                                Return 0
    '                            End If

    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Return 1
    '                        End If
    '                        Return dtPerm.Rows(0).Item("CBP_Index")
    '                    Case "SRH"
    '                        'dsPerm = dsMain
    '                        If (sPermlvl = "PG") Then
    '                            Return 1
    '                        ElseIf (sPermlvl = "GH" Or sPermlvl = "PU") Then
    '                            Return 1
    '                        End If
    '                        Return dtPerm.Rows(0).Item("CBP_Search")
    '                End Select
    '            End If
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Private Function AddPermissions(ByVal sAC As String, ByVal iACID As Integer, ByVal dtPerm As DataTable, ByVal PLevel As String)
    '        Dim dsRow, dtRow As DataRow
    '        Try

    '            dsRow = dtPerm.Rows(0)
    '            dtRow = Permdt.NewRow
    '            dtRow("PLevel") = PLevel
    '            dtRow("CabId") = dsRow("CBN_Node")
    '            dtRow("CabName") = dsRow("CBN_Name")
    '            dtRow("CabPar") = dsRow("CBN_Parent")
    '            dtRow("CabNote") = dsRow("CBN_Note")
    '            dtRow("CabCrtUsr") = dsRow("CBN_UserID")
    '            dtRow("CabCrtGrp") = dsRow("CBN_UserGroup")
    '            dtRow("CabParGrp") = GetGroupName(sAC, iACID, dsRow("CBN_ParGrp"))
    '            dtRow("CabCrOn") = dsRow("CBN_CrOn")
    '            dtRow("SubCabNo") = dsRow("CBN_SCCount") '10 'GetSubCabCount(dsRow("CBN_Node"))
    '            dtRow("FolNo") = dsRow("CBN_FolCount") 'GetFolCount(dsRow("CBN_Node"))
    '            Permdt.Rows.Add(dtRow)

    '            dsMain.Clear()
    '            dsMain.Dispose()
    '            'Return PermDt
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    '    Private Function GetGroupName(ByVal sAC As String, ByVal iACID As Integer, ByVal GrpId As Integer) As String
    '        Dim sSql As String
    '        Try
    '            sSql = "Select Mas_description from sad_grporlvl_general_master where Mas_Id=" & GrpId & ""
    '            Return (objDBL.SQLExecuteScalar(sAC, sSql))
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function

    'Public Function LoadCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer) As String
    '    Dim dRow As DataRow
    '    Dim dsCab As DataSet
    '    Dim sSql As String
    '    Dim iRet
    '    'Dim sMem As String6
    '    Try

    '        sMem = GetMemberGroups(sAC, iACID, iUsrID)
    '        iUsrType = GetUserType(sAC, iACID, iUsrID)
    '        iUsrParGrp = GetUserParGrp(sAC, iACID, iUsrID)
    '        If (iUsrType = 1) Then
    '            'User Logged is Super User
    '            sSql = "Select * from edt_cabinet where CBN_DelStatus='A' and CBN_Parent=-1 order by CBN_Name"
    '            dsCab = objDBL.SQLExecuteDataSet(sAC, sSql)
    '            If (dsCab.Tables(0).Rows.Count > 0) Then
    '                For Each dRow In dsCab.Tables(0).Rows
    '                    iRet = GetFinalPermissions(sAC, iACID, dRow("CBN_Node"), iUsrID, sPerm)
    '                    If (iRet = 1) Then
    '                        AddPermissions(sAC, iACID, dtPerm, sPermlvl)
    '                    End If
    '                Next
    '            End If
    '            Return Permdt
    '            Exit Function
    '        End If
    '        sCabPerm = GetPermCabinets(sAC, iACID, iUsrID, sMem)
    '        If (iCabID = 0) Then
    '            sSql = " Select *  from edt_cabinet where CBN_ParGrp in (" & sMem & ") and CBN_DelStatus='A' and CBN_Parent= -1 "
    '        Else
    '            sSql = " Select *  from edt_cabinet where  CBN_ParGrp in (" & sMem & ") and CBN_DelStatus='A' and CBN_Parent = " & iCabID & "  "
    '        End If
    '        If Val(sCabPerm) <> 0 Then
    '            sSql = sSql & " and cbn_node Not in (" & sCabPerm & ") order by CBN_Name "
    '        Else
    '            sSql = sSql & " order by CBN_Name "
    '        End If

    '        dsCab = objDBL.SQLExecuteDataSet(sAC, sSql)
    '        If (dsCab.Tables(0).Rows.Count > 0) Then
    '            For Each dRow In dsCab.Tables(0).Rows
    '                iParGrp = dRow("CBN_ParGrp")
    '                iRet = GetFinalPermissions(sAC, iACID, dRow("CBN_Node"), iUsrID, sPerm)
    '                If (iRet = 1) Then
    '                    AddPermissions(sAC, iACID, dtPerm, sPermlvl)
    '                End If
    '            Next
    '        End If


    '        If (iCabID = 0) Then
    '            sSql = "Select *  from edt_cabinet where CBN_DelStatus='A' and CBN_Parent= -1"
    '            'CBP_CabId in (" & sCabPerm & ") and 
    '        Else
    '            sSql = " Select *  from edt_cabinet where CBN_DelStatus='A' and CBN_Parent = " & iCabID & ""
    '        End If

    '        If Len((sCabPerm)) <> 0 Then
    '            sSql = sSql & " and cbn_node in (" & sCabPerm & ")"
    '        Else
    '            sSql = sSql & " and cbn_node in (0)"
    '        End If
    '        sSql = sSql & " order by CBN_Name"
    '        dsCab = objDBL.SQLExecuteDataSet(sAC, sSql)
    '        If (dsCab.Tables(0).Rows.Count > 0) Then
    '            For Each dRow In dsCab.Tables(0).Rows
    '                iParGrp = dRow("cbn_parGrp")
    '                iRet = GetFinalPermissions(sAC, iACID, dRow("cbn_node"), iUsrID, sPerm, 1)
    '                If (iRet = 1) Then
    '                    AddPermissions(dtPerm, sPermlvl)
    '                End If
    '            Next
    '        End If
    '        Return Permdt

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function UpdateBatchScanIndex(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatch As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Update EDT_BATCH_MASTER set Batch_IndexStatus = 1 where Batch_No =" & iBatch & " and Batch_CompID =" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDescriptorsForIndexing(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocTypeID As Integer) As DataTable
        Dim sSql As String
        Dim dtDescriptors As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("DescriptorID")
            dt.Columns.Add("Descriptor")
            dt.Columns.Add("Values")
            dt.Columns.Add("Mandatory")
            dt.Columns.Add("Validator")
            dt.Columns.Add("Size")
            dt.Columns.Add("DataType")

            sSql = "" : sSql = "Select a.des_id,a.Desc_name,b.edd_values,c.DT_Name,b.EDD_ISREQUIRED,b.EDD_Validate,EDD_Size "
            sSql = sSql & " from EDT_DESCRIPTOR a,EDT_DOCTYPE_LINK b,EDT_DESC_TYPE c"
            sSql = sSql & "  where a.des_id=b.edd_dptrid And c.DT_ID=a.DESC_DATATYPE and b.edd_doctypeid= " & iDocTypeID & "  order by a.Desc_name"
            dtDescriptors = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDescriptors.Rows.Count > 0 Then
                For i = 0 To dtDescriptors.Rows.Count - 1
                    dr = dt.NewRow
                    dr("DescriptorID") = dtDescriptors.Rows(i)("des_id")
                    dr("Descriptor") = dtDescriptors.Rows(i)("Desc_name")
                    If IsDBNull(dtDescriptors.Rows(i)("Dt_Name")) = False Then
                        dr("DataType") = dtDescriptors.Rows(i)("Dt_Name")
                    End If
                    If IsDBNull(dtDescriptors.Rows(i)("EDD_SIZE")) = False Then
                        dr("Size") = dtDescriptors.Rows(i)("EDD_SIZE")
                    End If
                    If IsDBNull(dtDescriptors.Rows(i)("EDD_ISREQUIRED")) = False Then
                        If dtDescriptors.Rows(i)("EDD_ISREQUIRED") = "Q" Then
                            dr("Mandatory") = "N"
                        Else
                            dr("Mandatory") = "Y"
                        End If
                    End If
                    If IsDBNull(dtDescriptors.Rows(i)("EDD_VALUES")) = False Then
                        dr("Values") = dtDescriptors.Rows(i)("EDD_VALUES")
                    End If
                    If IsDBNull(dtDescriptors.Rows(i)("EDD_Validate")) = False Then
                        If dtDescriptors.Rows(i)("EDD_Validate") = "N" Then
                            dr("Validator") = "N"
                        Else
                            dr("Validator") = "Y"
                        End If
                    End If
                    dt.Rows.Add(dr)
                Next
                Return dt
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckTitle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTitle As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select PGE_BASENAME From edt_page where PGE_TITLE ='" & sTitle.Trim() & "' and Pge_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCabinetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal sCustomerName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from EDT_CABINET where CBN_NAME='" & sCustomerName & "' And CBN_Parent=-1 "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select CBN_id from EDT_CABINET where CBN_NAME='" & sCustomerName & "' And CBN_Parent=-1 "
                GetCabinetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_id")
                'sSql = "" : sSql = "Insert Into EDT_CABINET(CBN_id,CBN_NAME,CBN_PARENT,CBN_Note,CBN_USERGROUP,CBN_USERID,CBN_ParGrp,CBN_PERMISSION,cbn_DelStatus,CBN_SCCount,CBN_FolCount,cbn_Operation) "
                'sSql = sSql & "Values(" & iMaxID & ",'" & sCustomerName & "'," & -1 & ",'" & sCustomerName & "',0," & iUserID & ",0,0,'A',0,0,'X')"
                'objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                sSql = "" : sSql = "Insert Into EDT_CABINET(CBN_id,CBN_NAME,CBN_PARENT,CBN_Note,CBN_USERID) "
                sSql = sSql & "Values(" & iMaxID & ",'" & sCustomerName & "'," & -1 & ",'" & sCustomerName & "'," & iUserID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetCabinetID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSubCabinetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCabinetID As Integer, ByVal sTrTypeName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from EDT_CABINET where CBN_NAME='" & sTrTypeName & "' And CBN_Parent=" & iCabinetID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select CBN_id from EDT_CABINET where CBN_NAME='" & sTrTypeName & "' And CBN_Parent=" & iCabinetID & " "
                GetSubCabinetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_id")
                sSql = "" : sSql = "Insert Into EDT_CABINET(CBN_id,CBN_NAME,CBN_PARENT,CBN_Note,CBN_USERID) "
                sSql = sSql & "Values(" & iMaxID & ",'" & sTrTypeName & "'," & iCabinetID & ",'" & sTrTypeName & "'," & iUserID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetSubCabinetID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetFolderID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCabinetID As Integer, ByVal sFolderName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select FOL_FOLID from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " "
                GetFolderID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "edt_folder", "FOL_FOLID")
                sSql = "" : sSql = "Insert Into edt_folder(FOL_FOLID,FOL_CABINET,FOL_NAME,FOL_STATUS) "
                sSql = sSql & "Values(" & iMaxID & "," & iCabinetID & ",'" & sFolderName & "','A')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetFolderID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDOCTYPEID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select DOT_DOCTYPEID From EDT_DOCUMENT_TYPE where DOT_DOCNAME='Attachments' And DOT_CompID=" & iCompID & " "
            GetDOCTYPEID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDOCTYPEID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDescrptTYPEID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDescriptName As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select DOT_DOCTYPEID From EDT_DOCUMENT_TYPE where  DOT_CompID=" & iCompID & " "
            GetDescrptTYPEID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDescrptTYPEID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetImagePath(ByVal sAC As String) As String
        Dim sSql As String
        Dim str As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key = 'ImgPath'"
            str = objDBL.SQLExecuteScalar(sAC, sSql)
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckFoldersName(ByVal sAC As String, ByVal iCompID As Integer, ByVal sFolName As String, ByVal iCabID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select FOL_NAME from edt_folder where FOL_NAME='" & sFolName & "' and FOL_CABINET=" & iCabID & " and "
            sSql = sSql & "(FOL_Delflag='A'  or FOL_Delflag='W') and FOL_CompID = " & iCompID & " "
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CreateFolder(ByVal sAC As String, ByVal iSubCabinet As Integer, ByVal sFolderName As String, ByVal iUserID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_FolId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Name", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = sFolderName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Note", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = sFolderName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iSubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Delflag", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "SaveOrUpFolderDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdateFolderCount(ByVal sAC As String, ByVal iCBN_NODE As Integer, ByVal iSCBN_NODE As Integer)
        Dim aSql As String = ""
        Try
            'Update folder count to Cabinet
            aSql = "" : aSql = "Update edt_cabinet set CBN_FolderCount=(select count(Fol_folid) from edt_folder where fol_cabinet in (Select CBN_id from Edt_Cabinet "
            aSql = aSql & "where CBN_Parent=" & iCBN_NODE & " And (CBN_DelFlag='A' or CBN_DelFlag='W'))) where CBN_ID =" & iCBN_NODE & " and CBN_CompID = 1"
            objDBL.SQLExecuteNonQuery(sAC, aSql)

            'Update folder count to Sub-Cabinet
            aSql = "" : aSql = "Update edt_cabinet set CBN_FolderCount=(select Count(Fol_folid) from edt_folder where fol_cabinet=" & iSCBN_NODE & " and "
            aSql = aSql & "(FOL_Delflag='A' or FOL_Delflag='W')) where cbn_ID=" & iSCBN_NODE & " and CBN_CompID = 1"
            objDBL.SQLExecuteNonQuery(sAC, aSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetFolderID(ByVal sAC As String, ByVal sFoldername As String, ByVal iCabinet As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select FOL_FolID from EDT_FOLDER where FOL_Name ='" & sFoldername & "' and FOL_Cabinet ='" & iCabinet & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function

    Public Function LoadCabinetBasedOnDept(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer, ByVal iDeptID As Integer) As DataTable
        Dim sSql As String
        Dim dtcab As New DataTable
        Try
            If (iDeptID > 0) Then
                sSql = "Select CBN_ID,CBN_NAME from edt_cabinet where CBN_DelFlag='A' and CBN_Parent=-1 and CBN_CompID=" & iACID & " and CBN_Department=" & iDeptID & " order by CBN_Name"
            Else
                sSql = "Select CBN_ID,CBN_NAME from edt_cabinet where CBN_DelFlag='A' and CBN_Parent=-1 and CBN_CompID=" & iACID & "  order by CBN_Name"
            End If
            dtcab = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtcab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
