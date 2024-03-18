Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports DatabaseLayer
Imports System
Imports Microsoft.VisualBasic
Imports System.Configuration
Imports BusinesLayer
Imports System.Security.Cryptography
Public Class clsAttachments
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Shared Sub Encrypt(ByVal sInputFilePath As String, ByVal sOutputFilePath As String)
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using fs As New FileStream(sOutputFilePath, FileMode.Create)
                Using cs As New CryptoStream(fs, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    Using fsInput As New FileStream(sInputFilePath, FileMode.Open)
                        Dim data As Integer
                        While (Assign(data, fsInput.ReadByte())) <> -1
                            cs.WriteByte(CByte(data))
                        End While
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Public Shared Sub Decrypt(ByVal sInputFilePath As String, ByVal sOutputFilePath As String)
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using fs As New FileStream(sInputFilePath, FileMode.Open)
                Using cs As New CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read)
                    Using fsOutput As New FileStream(sOutputFilePath, FileMode.Create)
                        Dim data As Integer
                        While (Assign(data, cs.ReadByte())) <> -1
                            fsOutput.WriteByte(CByte(data))
                        End While
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Shared Function Assign(Of T)(ByRef source As T, ByVal value As T) As T
        source = value
        Return value
    End Function
    Public Function LoadAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")
            dtAttach.Columns.Add("Extention")
            'dtAttach.Columns.Add("Pge_DETAILS_ID")
            'dtAttach.Columns.Add("PGE_BASENAME")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From edt_attachments"
            sSql = sSql & " Where ATCH_CompID=" & iACID & " And"
            sSql = sSql & " ATCH_ID = " & iAttachID & " AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                drow("Extention") = dt.Rows(i)("ATCH_EXT")
                'drow("Pge_DETAILS_ID") = dt.Rows(i)("Pge_DETAILS_ID")
                'drow("PGE_BASENAME") = dt.Rows(i)("PGE_BASENAME")
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function LoadAttachmentsDF(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iPGEDetailsID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable, dtPage As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")
            dtAttach.Columns.Add("Pge_DETAILS_ID")
            dtAttach.Columns.Add("PGE_BASENAME")

            sSql = "Select Distinct Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE,Pge_DETAILS_ID,PGE_BASENAME From edt_attachments Left Join edt_page On"
            sSql = sSql & " Pge_CompID=ATCH_CompID And Pge_DETAILS_ID=" & iPGEDetailsID & " And pge_Delflag <> 'D' Where ATCH_CompID=" & iACID & " And"
            sSql = sSql & " ATCH_ID = " & iAttachID & " AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            dtPage = LoadAttachments(sAC, iACID, iPGEDetailsID)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                drow("Pge_DETAILS_ID") = dt.Rows(i)("Pge_DETAILS_ID")
                If dtPage.Rows.Count > 0 Then
                    If IsDBNull(dtPage.Rows(i)("PGE_BASENAME")) = False Then
                        drow("PGE_BASENAME") = dtPage.Rows(i)("PGE_BASENAME")
                    End If
                End If
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function LoadAttachments(ByVal sAC As String, ByVal iACID As Integer, ByVal iPGEDetailsID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select PGE_BASENAME From edt_page Where Pge_DETAILS_ID=" & iPGEDetailsID & " And pge_Delflag <> 'D' And Pge_CompID=" & iACID & " Order by PGE_BASENAME"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function LoadSamplingAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAuditID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
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
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
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
    Public Function SaveAttachments(ByVal sAC As String, ByVal iACID As Integer, ByVal sPath As String, ByVal iUserId As Integer, ByVal iAttachID As Integer) As Integer
        Dim sExt As String, sInputFilePath As String, sSql As String, sDBPath As String, sDBFilePath As String
        Dim iPosDot As Integer, iPosSlash As Integer, fileSize As Integer, iDocID As Integer
        Dim con As New OleDbConnection
        Dim objFile As FileStream
        Dim com As OleDbCommand
        Dim sPegDes As String = "", sPegExt As String = "", iPegSize As String = ""
        Try
            iPosSlash = InStrRev(sPath, "\")
            iPosDot = InStrRev(sPath, ".")
            If iPosDot <> 0 Then
                sInputFilePath = Mid(sPath, iPosSlash + 1, iPosDot - (iPosSlash + 1))
                sExt = Right(sPath, Len(sPath) - iPosDot)
            Else
                sInputFilePath = Mid(sPath, iPosSlash, Len(sPath) - (iPosSlash + 1))
                sExt = "unk"
            End If
            sInputFilePath = Replace(sInputFilePath, "&", " and")
            sInputFilePath = objclsGRACeGeneral.SafeSQL(sInputFilePath)
            If sInputFilePath.Length > 99 Then
                sInputFilePath = sInputFilePath.Substring(0, 95)
            End If

NextSetOfID: If iAttachID = 0 Then
                iAttachID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_ID),0)+1 from EDT_ATTACHMENTS Where ATCH_CompID=" & iACID & "")
            End If
            iDocID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_DOCID),0)+1 from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & "")
            If iDocID = 0 Then
                sSql = "" : sSql = "Select ATCH_DOCID from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And atch_id=" & iAttachID & "" ' And ATCH_DOCID = " & Docid & ""
                Dim dr As OleDbDataReader
                dr = objDBL.SQLDataReader(sAC, sSql)
                If dr.HasRows = True Then
                    iAttachID = 0
                    dr.Close()
                    GoTo NextSetOfID
                End If
                dr.Close()
            End If

            objFile = New FileStream(sPath, FileMode.Open)
            fileSize = CType(objFile.Length, Integer)
            Dim BUFFER(fileSize) As Byte
            objFile.Read(BUFFER, 0, fileSize)
            objFile.Close()
            If objDBL.SQLExecuteScalar(sAC, "Select Sad_Config_Value From Sad_Config_Settings Where Sad_Config_Key='FilesInDB' And Sad_CompID=" & iACID & "") = "True" Then
                sSql = "" : sSql = "Insert into EDT_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_OLE,ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "'," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "?," & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                con = objDBL.SQLOpenDBConnection(sAC)
                com = New OleDbCommand(sSql, con)
                Dim ParamBasename As New OleDbParameter("@atch_ole", OleDbType.Binary)
                ParamBasename.Value = BUFFER
                com.Parameters.Add(ParamBasename)
                Dim myTrans As OleDb.OleDbTransaction  'Start a local transaction
                myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted) 'Assign transaction object for a pending local transaction
                com.Connection = con
                com.Transaction = myTrans
                com.ExecuteNonQuery()
                myTrans.Commit()
            Else
                sSql = "" : sSql = "Insert into EDT_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "'," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "" & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)

                sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                If sDBPath.EndsWith("\") = False Then
                    sDBPath = sDBPath & "\Attachments\" & iDocID \ 301
                Else
                    sDBPath = sDBPath & "Attachments\" & iDocID \ 301
                End If
                If System.IO.Directory.Exists(sDBPath) = False Then
                    System.IO.Directory.CreateDirectory(sDBPath)
                End If

                sDBFilePath = sDBPath & "\" & iDocID & "." & sExt
                If System.IO.File.Exists(sDBFilePath) = True Then
                    System.IO.File.Delete(sDBFilePath)
                End If
                File.Copy(sPath, sDBFilePath)
                'Encrypt(sPath, sDBFilePath)
            End If
            Return iAttachID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocumentPath(ByVal sAC As String, ByVal iACID As Integer, ByVal sPaths As String, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer)
        Dim iAtchOle As Integer
        Dim con As New OleDb.OleDbConnection
        Dim Pdr As OleDb.OleDbDataReader, PdrCheck As OleDb.OleDbDataReader
        Dim sSql As String, sDBPath As String, sDBFilePath As String, sInputFilePath As String = ""
        Try
            If iAttachDocID = 0 Then
                Return ""
            End If
            If sPaths.EndsWith("\") = False Then
                sPaths = sPaths & "\"
            End If

            If System.IO.Directory.Exists(sPaths) = False Then
                System.IO.Directory.CreateDirectory(sPaths)
            End If

            If objDBL.SQLExecuteScalar(sAC, "Select Sad_Config_Value From Sad_Config_Settings Where Sad_Config_Key='FilesInDB' And Sad_CompID=" & iACID & "") = "True" Then
                sSql = "Select atch_ole,ATCH_DocId,ATCH_FNAME,atch_ext,ATCH_FLAG from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                Pdr = objDBL.SQLDataReader(sAC, sSql)
                If Pdr.HasRows Then
                    While Pdr.Read()
                        sInputFilePath = sPaths & Pdr("ATCH_FNAME") & "." & Pdr("atch_ext")
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
                sSql = "Select ATCH_DocId,ATCH_FNAME,atch_ext from EDT_ATTACHMENTS where atch_ole IS Not NULL And ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                PdrCheck = objDBL.SQLDataReader(sAC, sSql)
                If PdrCheck.HasRows Then
                    sSql = "Select atch_ole,ATCH_DocId,ATCH_FNAME,atch_ext,ATCH_FLAG from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                    Pdr = objDBL.SQLDataReader(sAC, sSql)
                    If Pdr.HasRows Then
                        While Pdr.Read()
                            sInputFilePath = sPaths & Pdr("ATCH_FNAME") & "." & Pdr("atch_ext")
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
                    sSql = "Select ATCH_DocId,ATCH_FNAME,atch_ext from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
                    Pdr = objDBL.SQLDataReader(sAC, sSql)
                    If Pdr.HasRows Then
                        While Pdr.Read()
                            sInputFilePath = sPaths & Pdr("ATCH_FNAME") & "." & Pdr("atch_ext")
                            If System.IO.File.Exists(sInputFilePath) = True Then
                                System.IO.File.Delete(sInputFilePath)
                            End If

                            sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                            If sDBPath.EndsWith("\") = False Then
                                sDBPath = sDBPath & "\Attachments\" & Pdr("ATCH_DocId") \ 301
                            Else
                                sDBPath = sDBPath & "Attachments\" & Pdr("ATCH_DocId") \ 301
                            End If
                            If System.IO.Directory.Exists(sDBPath) = True Then
                                sDBFilePath = sDBPath & "\" & Pdr("ATCH_DocId") & "." & Pdr("atch_ext")
                                If System.IO.File.Exists(sDBFilePath) = True Then
                                    File.Copy(sDBFilePath, sInputFilePath)
                                    'Decrypt(sDBFilePath, sInputFilePath)
                                End If
                            End If
                        End While
                    Else
                        sInputFilePath = String.Empty
                    End If
                End If
                PdrCheck.Close()
            End If
            Pdr.Close()
            Return sInputFilePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub RemoveSelectedDoc(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer)
        Dim sSql As String
        Try
            sSql = "Update edt_attachments set ATCH_Status='D'where ATCH_CompID=" & iACID & " And atch_docid = " & iAttachDocID & " and atch_id=" & iAttachID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateDescSelectedDoc(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer, ByVal sDesc As String)
        Dim sSql As String
        Try
            sSql = "Update edt_attachments set ATCH_Desc='" & sDesc & "' where ATCH_CompID=" & iACID & " And atch_docid = " & iAttachDocID & " and atch_id=" & iAttachID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub RemoveSelectedDocFromDataFiling(ByVal sAC As String, ByVal iACID As Integer, ByVal iPGEDetailsID As Integer, ByVal iPGEBaseID As Integer)
        Dim sSql As String
        Try
            sSql = "Update edt_page set pge_Delflag='D'where Pge_CompID=" & iACID & " And Pge_DETAILS_ID = " & iPGEDetailsID & " and PGE_BASENAME=" & iPGEBaseID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    '--rakshan--
    Public Function GetOriginalDocumentPath(ByVal sAC As String, ByVal iACID As Integer, ByVal sPaths As String, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer)
        Dim con As New OleDb.OleDbConnection
        Dim Pdr As OleDb.OleDbDataReader
        Dim sSql As String, sDBPath As String, sDBFilePath As String, sInputFilePath As String = ""
        Try
            sSql = "Select ATCH_DocId,ATCH_FNAME,atch_ext from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
            Pdr = objDBL.SQLDataReader(sAC, sSql)
            If Pdr.HasRows Then
                While Pdr.Read()
                    sInputFilePath = sPaths & Pdr("ATCH_DocId") & "." & Pdr("atch_ext")
                    If System.IO.File.Exists(sInputFilePath) = True Then
                        System.IO.File.Delete(sInputFilePath)
                    End If

                    sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "TempPath")
                    If sDBPath.EndsWith("\") = False Then
                        sDBPath = sDBPath & "\Attachments\" & Pdr("ATCH_DocId") \ 301
                    Else
                        sDBPath = sDBPath & "Attachments\" & Pdr("ATCH_DocId") \ 301
                    End If
                    If System.IO.Directory.Exists(sDBPath) = True Then
                        sDBFilePath = sDBPath & "\" & Pdr("ATCH_DocId") & "." & Pdr("atch_ext")
                        If System.IO.File.Exists(sDBFilePath) = True Then
                            File.Copy(sDBFilePath, sInputFilePath)
                            'Decrypt(sDBFilePath, sInputFilePath) 'VInayak
                        End If
                    End If
                End While
            Else
                sInputFilePath = String.Empty
            End If
            Return sInputFilePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAuditID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAUDSchID As Integer, ByVal iAuditId As Integer)
        Dim sSql As String
        Try
            sSql = "Update edt_attachments set ATCH_AuditID=" & iAuditId & " where ATCH_AUDScheduleID=" & iAUDSchID & " and ATCH_CompID=" & iACID & " "
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadAttachmentsCM(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer,
                                   ByVal iAUDSchID As Integer, ByVal iAuditID As Integer, ByVal iSubProID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From edt_attachments where ATCH_CompID=" & iACID & " And "
            If iSubProID = 0 Then
                sSql = sSql & " ATCH_AUDScheduleID=" & iAUDSchID & " and ATCH_SubProcessID=0  AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            Else
                sSql = sSql & " ATCH_AUDScheduleID=" & iAUDSchID & " and ATCH_SubProcessID=" & iSubProID & "  AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            End If

            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
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
    Public Function SaveAttachmentsCM(ByVal sAC As String, ByVal iACID As Integer, ByVal sPath As String, ByVal iUserId As Integer, ByVal iAttachID As Integer,
                                  ByVal iAuDSchID As Integer, ByVal iAuditID As Integer, ByVal iSubProID As Integer) As Integer
        Dim sExt As String, sInputFilePath As String, sSql As String, sDBPath As String, sDBFilePath As String
        Dim iPosDot As Integer, iPosSlash As Integer, fileSize As Integer, iDocID As Integer
        Dim con As New OleDbConnection
        Dim objFile As FileStream
        Dim com As OleDbCommand
        Dim sPegDes As String = "", sPegExt As String = "", iPegSize As String = ""
        Try
            iPosSlash = InStrRev(sPath, "\")
            iPosDot = InStrRev(sPath, ".")
            If iPosDot <> 0 Then
                sInputFilePath = Mid(sPath, iPosSlash + 1, iPosDot - (iPosSlash + 1))
                sExt = Right(sPath, Len(sPath) - iPosDot)
            Else
                sInputFilePath = Mid(sPath, iPosSlash, Len(sPath) - (iPosSlash + 1))
                sExt = "unk"
            End If
            sInputFilePath = Replace(sInputFilePath, "&", " and")
            sInputFilePath = objclsGRACeGeneral.SafeSQL(sInputFilePath)
            If sInputFilePath.Length > 99 Then
                sInputFilePath = sInputFilePath.Substring(0, 95)
            End If

            'NextSetOfID: If iAttachID = 0 Then
            iAttachID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_ID),0)+1 from EDT_ATTACHMENTS Where ATCH_CompID=" & iACID & "")
            'End If
            iDocID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_DOCID),0)+1 from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & "")
            'If iDocID = 0 Then
            '    sSql = "" : sSql = "Select ATCH_DOCID from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And atch_id = " & iAttachID & "" ' And ATCH_DOCID = " & Docid & ""
            '    Dim dr As OleDbDataReader
            '    dr = objDBL.SQLDataReader(sAC, sSql)
            '    If dr.HasRows = True Then
            '        iAttachID = 0
            '        dr.Close()
            '        'GoTo NextSetOfID
            '    End If
            '    dr.Close()
            'End If

            objFile = New FileStream(sPath, FileMode.Open)
            fileSize = CType(objFile.Length, Integer)
            Dim BUFFER(fileSize) As Byte
            objFile.Read(BUFFER, 0, fileSize)
            objFile.Close()
            If objDBL.SQLExecuteScalar(sAC, "Select Sad_Config_Value From Sad_Config_Settings Where Sad_Config_Key='FilesInDB' And Sad_CompID=" & iACID & "") = "True" Then
                sSql = "" : sSql = "Insert into EDT_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_AUDScheduleID,ATCH_AuditID,ATCH_SubProcessID,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_OLE,ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "'," & iAuDSchID & "," & iAuditID & "," & iSubProID & "," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "?," & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                con = objDBL.SQLOpenDBConnection(sAC)
                com = New OleDbCommand(sSql, con)
                Dim ParamBasename As New OleDbParameter("@atch_ole", OleDbType.Binary)
                ParamBasename.Value = BUFFER
                com.Parameters.Add(ParamBasename)
                Dim myTrans As OleDb.OleDbTransaction  'Start a local transaction
                myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted) 'Assign transaction object for a pending local transaction
                com.Connection = con
                com.Transaction = myTrans
                com.ExecuteNonQuery()
                myTrans.Commit()
            Else
                sSql = "" : sSql = "Insert into EDT_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_AUDScheduleID,ATCH_AuditID,ATCH_SubProcessID,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "'," & iAuDSchID & "," & iAuditID & "," & iSubProID & "," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "" & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)

                sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                If sDBPath.EndsWith("\") = False Then
                    sDBPath = sDBPath & "\Attachments\" & iDocID \ 301
                Else
                    sDBPath = sDBPath & "Attachments\" & iDocID \ 301
                End If
                If System.IO.Directory.Exists(sDBPath) = False Then
                    System.IO.Directory.CreateDirectory(sDBPath)
                End If

                sDBFilePath = sDBPath & "\" & iDocID & "." & sExt
                If System.IO.File.Exists(sDBFilePath) = True Then
                    System.IO.File.Delete(sDBFilePath)
                End If
                'File.Copy(sPath, sDBFilePath)
                Encrypt(sPath, sDBFilePath)
            End If
            Return iAttachID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub RemoveSelectedDocCM(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer)
        Dim sSql As String
        Try
            If iAttachID = 0 Then
                sSql = "Update edt_attachments set ATCH_Status='D' where ATCH_CompID=" & iACID & " And atch_docid = " & iAttachDocID & ""
            Else
                sSql = "Update edt_attachments set ATCH_Status='D' where ATCH_CompID=" & iACID & " And atch_docid = " & iAttachDocID & " and atch_id=" & iAttachID & ""
            End If

            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateDescSelectedDoc(ByVal sAC As String, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer, ByVal sDesc As String)
        Dim sSql As String
        Try
            If iAttachID = 0 Then
                sSql = "Update edt_attachments set ATCH_Desc='" & sDesc & "' where atch_docid=" & iAttachDocID & " and ATCH_Status='X'"
            Else
                sSql = "Update edt_attachments set ATCH_Desc='" & sDesc & "' where atch_docid=" & iAttachDocID & " and atch_id=" & iAttachID & " and ATCH_Status='X'"
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveSamplingAttachments(ByVal sAC As String, ByVal iACID As Integer, ByVal sPath As String, ByVal iUserId As Integer, ByVal iAttachID As Integer, ByVal sDesc As String) As Integer
        Dim sExt As String, sInputFilePath As String, sSql As String, sDBPath As String, sDBFilePath As String
        Dim iPosDot As Integer, iPosSlash As Integer, fileSize As Integer, iDocID As Integer
        Dim con As New OleDbConnection
        Dim objFile As FileStream
        Dim com As OleDbCommand
        Dim sPegDes As String = "", sPegExt As String = "", iPegSize As String = ""
        Try
            iPosSlash = InStrRev(sPath, "\")
            iPosDot = InStrRev(sPath, ".")
            If iPosDot <> 0 Then
                sInputFilePath = Mid(sPath, iPosSlash + 1, iPosDot - (iPosSlash + 1))
                sExt = Right(sPath, Len(sPath) - iPosDot)
            Else
                sInputFilePath = Mid(sPath, iPosSlash, Len(sPath) - (iPosSlash + 1))
                sExt = "unk"
            End If
            sInputFilePath = Replace(sInputFilePath, "&", " and")
            sInputFilePath = objclsGRACeGeneral.SafeSQL(sInputFilePath)
            If sInputFilePath.Length > 99 Then
                sInputFilePath = sInputFilePath.Substring(0, 95)
            End If

NextSetOfID: If iAttachID = 0 Then
                iAttachID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_ID),0)+1 from EDT_ATTACHMENTS Where ATCH_CompID=" & iACID & "")
            End If
            iDocID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(Max(ATCH_DOCID),0)+1 from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & "")
            If iDocID = 0 Then
                sSql = "" : sSql = "Select ATCH_DOCID from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And atch_id=" & iAttachID & "" ' And ATCH_DOCID = " & Docid & ""
                Dim dr As OleDbDataReader
                dr = objDBL.SQLDataReader(sAC, sSql)
                If dr.HasRows = True Then
                    iAttachID = 0
                    dr.Close()
                    GoTo NextSetOfID
                End If
                dr.Close()
            End If

            objFile = New FileStream(sPath, FileMode.Open)
            fileSize = CType(objFile.Length, Integer)
            Dim BUFFER(fileSize) As Byte
            objFile.Read(BUFFER, 0, fileSize)
            objFile.Close()
            If objDBL.SQLExecuteScalar(sAC, "Select Sad_Config_Value From Sad_Config_Settings Where Sad_Config_Key='FilesInDB' And Sad_CompID=" & iACID & "") = "True" Then
                sSql = "" : sSql = "Insert into EDT_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_OLE,ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "','" & sDesc & "'," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "?," & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                con = objDBL.SQLOpenDBConnection(sAC)
                com = New OleDbCommand(sSql, con)
                Dim ParamBasename As New OleDbParameter("@atch_ole", OleDbType.Binary)
                ParamBasename.Value = BUFFER
                com.Parameters.Add(ParamBasename)
                Dim myTrans As OleDb.OleDbTransaction  'Start a local transaction
                myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted) 'Assign transaction object for a pending local transaction
                com.Connection = con
                com.Transaction = myTrans
                com.ExecuteNonQuery()
                myTrans.Commit()
            Else
                sSql = "" : sSql = "Insert into EDT_ATTACHMENTS(ATCH_ID,ATCH_DOCID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CREATEDBY,ATCH_MODIFIEDBY,ATCH_VERSION,ATCH_FLAG,"
                sSql = sSql & "ATCH_SIZE,ATCH_FROM,ATCH_Basename,ATCH_CREATEDON,ATCH_Status,ATCH_CompID) VALUES (" & iAttachID & "," & iDocID & ","
                sSql = sSql & "'" & objclsGRACeGeneral.SafeSQL(sInputFilePath) & "','" & sExt & "','" & sDesc & "'," & iUserId & "," & iUserId & ",1,0,"
                sSql = sSql & "" & CType(fileSize, Long) & ",0,0,GetDate(),'X'," & iACID & ")"
                objDBL.SQLExecuteNonQuery(sAC, sSql)

                sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                If sDBPath.EndsWith("\") = False Then
                    sDBPath = sDBPath & "\Attachments\" & iDocID \ 301
                Else
                    sDBPath = sDBPath & "Attachments\" & iDocID \ 301
                End If
                If System.IO.Directory.Exists(sDBPath) = False Then
                    System.IO.Directory.CreateDirectory(sDBPath)
                End If

                sDBFilePath = sDBPath & "\" & iDocID & "." & sExt
                If System.IO.File.Exists(sDBFilePath) = True Then
                    System.IO.File.Delete(sDBFilePath)
                End If
                File.Copy(sPath, sDBFilePath)
                'Encrypt(sPath, sDBFilePath)
            End If
            Return iAttachID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAttachmentCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*) from edt_attachments where ATCH_CompID=" & iACID & " And atch_id=" & iAttachID & " And ATCH_Status='X'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
