Imports System.Data
Imports System
Imports TRACePA
Imports System.IO
Imports BusinesLayer
Imports System.Net
Imports System.Web.Security
Imports System.Web

Public Class FormControl
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objGen As New clsEDICTGeneral

    Public Function checkForAccesscode(ByVal AccessID As String) As Boolean
        Try
            Dim sRet As String
            sRet = objDBL.GetKeyValues(AccessID)
            If sRet <> String.Empty Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function checkDocumentType(ByVal sAc As String, ByVal iCompId As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As DataTable
        Try
            sSql = "Select DOT_DOCTYPEID from edt_document_type where DOT_Compid ='" & iCompId & "' and DOT_DelFlag='A' and DOT_DOCName='Assignment Attachments'"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("DOT_DOCTYPEID").ToString()
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccessCode(ByVal sAC As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select CM_AccessCode from Sad_Company_Master where CM_AccessCode ='" & sAC & "'"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUsername(ByVal sAC As String, ByVal sUser As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_Id from sad_userdetails where USR_LoginName ='" & sUser & "'"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDepartment(ByVal sAC As String, ByVal sUser As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select USR_ID,USR_DeptID from sad_userdetails where USR_LoginName ='" & sUser & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function

    Public Function GetCustomerDepartment(ByVal sAC As String, ByVal sUser As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Org_node as USR_DeptID,Org_Name from sad_Org_Structure where Org_name ='" & sUser & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function GetUserID(ByVal sAC As String, ByVal sUser As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select USR_ID from sad_userdetails where USR_LoginName ='" & sUser & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
        End Try
    End Function
    Public Function CheckCabName(ByVal sAC As String, ByVal sCabinet As String, ByVal iDepartment As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sCabinet & "' and CBN_Department=" & iDepartment & " and "
            sSql = sSql & "CBN_Parent=-1 And (CBN_DelFlag='A' or CBN_DelFlag='W')"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CreateCabinet(ByVal sAC As String, ByVal sCabName As String, ByVal iUserID As Integer, ByVal iDepartment As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_NAME", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = sCabName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Note", OleDb.OleDbType.VarChar, 7999)
            ObjParam(iParamCount).Value = sCabName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_PARENT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = "-1"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_USERID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDepartment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_SubCabCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_FolderCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_Cabinet", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabinetID(ByVal sAC As String, ByVal sCabinetname As String, ByVal iDepartment As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CBN_ID from EDT_Cabinet where CBN_name ='" & sCabinetname & "' and CBN_Department='" & iDepartment & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function

    Public Function GetSubCabinetID(ByVal sAC As String, ByVal sCabinetID As String, ByVal sSubCabinetName As String, ByVal iDepartment As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CBN_ID from EDT_Cabinet where CBN_name ='" & sSubCabinetName & "' and CBN_Parent='" & sCabinetID & "' And CBN_Department ='" & iDepartment & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function CheckSubCabName(ByVal sAC As String, ByVal sSubCabName As String, ByVal iCabnet As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sSubCabName & "'"
            sSql = sSql & " and (CBN_DelFlag ='A' or CBN_DelFlag ='W') and CBN_Parent=" & iCabnet & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CreateSubCabDetails(ByVal sAC As String, ByVal iCabinetID As Integer, ByVal sSubCabinet As String, ByVal iUserID As Integer, ByVal iDepartment As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_NAME", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = sSubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Note", OleDb.OleDbType.VarChar, 7999)
            ObjParam(iParamCount).Value = sSubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_PARENT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCabinetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_USERID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDepartment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_SubCabCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_FolderCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_Cabinet", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateSubCabDetails(ByVal sAC As String, ByVal iDepartment As Integer, ByVal iCBN_ID As Integer)
        Dim strsql As String = "", aSql As String = ""
        Try
            'Update Sub cabinet count
            strsql = "" : strsql = "Update edt_cabinet set CBN_SubCabCount=(Select count(CBN_ID) from Edt_Cabinet where "
            strsql = strsql & "CBN_Parent =" & iCBN_ID & " And (CBN_DelFlag='A' or CBN_DelFlag='W')) where CBN_ID=" & iCBN_ID & " and CBN_CompID =1"
            objDBL.SQLExecuteNonQuery(sAC, strsql)

            'Update folder count
            aSql = "" : aSql = "Update edt_cabinet set CBN_FolderCount=(select count(Fol_folid) from edt_folder where "
            aSql = aSql & "fol_cabinet in (Select CBN_ID from Edt_Cabinet where CBN_Parent=" & iCBN_ID & " And (CBN_DelFlag='A' or CBN_DelFlag='W'))) where CBN_ID=" & iCBN_ID & " and CBN_CompID =1"
            objDBL.SQLExecuteNonQuery(sAC, aSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
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
    Public Function CheckFileExistOrNot(ByVal sAc As String, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer, ByVal iDocType As Integer, ByVal sFileName As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from edt_page where Pge_Cabinet = " & iCabinet & " and Pge_SubCabinet = " & iSubCabinet & " and Pge_Folder = " & iFolder & " and "
            sSql = sSql & "PGE_DOCUMENT_TYPE =" & iDocType & " and Pge_OrignalFileName = '" & sFileName & "'"
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            Return dt
        Catch ex As Exception

        End Try
    End Function

    Public Function CreateIndex(ByVal sAC As String, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer, ByVal iDocumentType As Integer, ByVal sTitle As String, ByVal sKeyWord As String, ByVal iUserID As Integer, ByVal sPath As String) As Double
        Dim sArray As Array
        Dim sFilePath As String, sFileName As String, sTempPath As String = "", sImagePathweb As String = ""
        Dim iPageDetailsid As Integer = 0, iPageNo As Integer = 0, iBaseName As Integer = 0
        Dim sObject As String = ""
        Dim fileName As String = "", Uploades As String = "", SendFiles As String = ""
        Try
            sArray = sPath.Split(";")
            For i = 0 To sArray.Length - 1
                Dim sPageExt As String = ""
                sPageExt = Path.GetExtension(sArray(i))

                If sPageExt.Contains(".") = True Then
                    sPageExt = sPageExt.Remove(0, 1)
                End If
                sFilePath = sArray(i)
                sFileName = Path.GetFileName(sArray(i))

                iBaseName = GetMaxID(sAC, 1, "edt_page", "PGE_BASENAME", "Pge_CompID")
                If iPageDetailsid = 0 Then
                    iPageDetailsid = iBaseName
                End If

                Select Case UCase(sPageExt)
                    Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                        sObject = "IMAGE"
                    Case Else
                        sObject = "OLE"
                End Select

                iPageNo = GetMaxID(sAC, 1, "edt_page", "PGE_PAGENO", "Pge_CompID")

                SavePage(sAC, iBaseName, iCabinet, iSubCabinet, iFolder, iDocumentType, sTitle,
                                   iPageDetailsid, iUserID, sObject, iPageNo, sPageExt, sKeyWord, "", 0, 0, "A", sFileName)
                FilePageInEdict(sAC, iBaseName, sFilePath, UCase("FALSE"))
                ' Return iPageDetailsid
                'Delete the Files
                'If System.IO.File.Exists(sFilePath) = True Then   yesterday
                '    System.IO.File.Delete(sFilePath)
                'End If
                'fileName = Convert.ToString(iBaseName).ToString()
                'Uploades = Uploades & "," & fileName
                'SendFiles = Uploades.Remove(0, 1)
            Next
            Return iPageDetailsid
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal sTable As String, ByVal sColumn As String, ByVal sCompColumn As String) As Integer
        Dim sSql As String = ""
        Dim objMax As Object
        Try
            sSql = "Select ISNULL(MAX(" & sColumn & ")+1,1) FROM " & sTable & "  Where " & sCompColumn & "=" & iACID & " "
            objMax = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If Not objMax Is DBNull.Value Then
                Return Integer.Parse(objMax.ToString())
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePage(ByVal sAc As String, ByVal iBaseName As Integer, ByVal iCabnet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer, ByVal iDocumentType As Integer, ByVal sTitle As String, ByVal iPageDetailId As Integer,
                          ByVal iUserID As Integer, ByVal sObject As String, ByVal iPageNo As Integer, ByVal sExtension As String, ByVal sKeyword As String,
                          ByVal sOCRText As String, ByVal iSize As Integer, ByVal iCurrentVersion As Integer, ByVal sStatus As String,
                          ByVal sOrignalFileName As String) As Array

        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BASENAME", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iBaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CABINET", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCabnet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iFolder
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DOCUMENT_TYPE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDocumentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_TITLE", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sTitle
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DATE", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_DETAILS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iPageDetailId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OBJECT", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = sObject
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_PAGENO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iPageNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_EXT", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = sExtension
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_KeyWORD", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = sKeyword
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRText", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sOCRText
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SIZE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iSize
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CURRENT_VER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCurrentVersion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_STATUS", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SubCabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iSubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_QC_UsrGrpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FTPStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "F"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_batch_name", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iBaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_OrignalFileName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sOrignalFileName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRDelFlag", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
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

            Arr = objDBL.ExecuteSPForInsertARR(sAc, "spEDT_PAGE", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function FilePageInEdict(ByVal sAc As String, ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt, ssExt As String
        Dim objclsEdictGen As New clsEDICTGeneral
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            ssExt = objclsEdictGen.ChangeExt(sExt)
            If sFileInDB = "FALSE" Then
                sImagePath = GetImagePath(sAc)
                sImagePath = sImagePath & "BITMAPS\" & iBaseName \ 301 & "\"
                CheckAndCreateWorkingDirFromPath(sImagePath)
                sImagePath = sImagePath & iBaseName & ssExt
                If System.IO.File.Exists(sImagePath) = False Then
                    'objclsEdictGen.FileEn(sFilePath, sImagePath)
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImagePath(ByVal sAC As String) As String
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
    Public Function CheckAndCreateWorkingDirFromPath(ByVal sImagePath As String) As String
        Dim sPaths As String
        Try
            If sImagePath.EndsWith("\") = False Then
                sPaths = sImagePath & "\"
            Else
                sPaths = sImagePath
            End If
            If Not System.IO.Directory.Exists(sPaths) Then
                System.IO.Directory.CreateDirectory(sPaths)
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function
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
    Public Function LoadBaseIdFromFolder(ByVal sAC As String, ByVal iCompID As Integer, ByVal CabID As Integer, ByVal PGE_SubCabinet As Integer, ByVal FolderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select * from EDT_PAGE where pge_Basename in(Select distinct(pge_Details_ID) from EDT_PAGE where  PGE_Cabinet =" & CabID & " and "
            sSql = sSql & " PGE_SubCabinet = " & PGE_SubCabinet & " And PGE_FOLDER = " & FolderID & ") and pge_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPageFromEdict(ByVal sAC As String, ByVal iSelectedIndexID As Integer, ByVal sUser As String) As String
        Dim sImagePath As String = "", sExt As String = "", sFIleInDB As String = "", sBool As String = "", ssExt As String = ""
        Dim files() As String
        Dim objclsEdictgen As New clsEDICTGeneral
        Try
            sFIleInDB = objDBL.SQLExecuteScalar(sAC, "Select Set_Value from edt_Settings where SET_CODE = 'FileInDB'")
            If UCase(sFIleInDB) = "FALSE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                ssExt = objclsEdictgen.ChangeExt(sExt)
                sBool = GetImageSettings(sAC, "ImagePath")
                sImagePath = sBool & "BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                sImagePath = sImagePath & iSelectedIndexID & "." & ssExt

                sImagePath = objclsEdictgen.GetWebSerPathView(sBool, GetUserID(sAC, sUser), sImagePath, iSelectedIndexID, sExt)
            ElseIf UCase(sFIleInDB) = "TRUE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                Dim sPath As String = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sPath) = True Then
                    files = Directory.GetFileSystemEntries(sPath)
                    For Each element As String In files
                        If (Not Directory.Exists(element)) Then
                            File.Delete(Path.Combine(sPath, Path.GetFileName(element)))
                        End If
                    Next
                End If

                If System.IO.Directory.Exists(sPath) = False Then
                    System.IO.Directory.CreateDirectory(sPath)
                End If

                Updateimage(sAC, iSelectedIndexID, sExt)
                sImagePath = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                sImagePath = sImagePath & iSelectedIndexID & sExt
            End If
            GetPageFromEdict = sImagePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImageSettings(ByVal sAC As String, ByVal sCode As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from edt_Settings where Set_Code ='" & sCode & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("Set_Value").ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Updateimage(ByVal sAC As String, ByVal iSelectedIndexID As Integer, ByVal sExt As String)
        Dim pdr As OleDb.OleDbDataReader
        Dim sSql As String
        Dim iAtchOle As Integer
        Try
            Dim sPath As String = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
            If Not System.IO.Directory.Exists(sPath) Then
                Directory.CreateDirectory(sPath)
            End If
            sPath = sPath & iSelectedIndexID & "." & sExt
            If System.IO.File.Exists(sPath) = False Then
                sSql = "Select BDT_BIGDATA,BDT_BASENAME from EDT_BIGDATA where BDT_BASENAME=" & Val(iSelectedIndexID) & ""
                pdr = objDBL.SQLDataReader(sAC, sSql)
                If pdr.HasRows Then
                    While pdr.Read()
                        Dim BUFFER(pdr.GetBytes(iAtchOle, 0, BUFFER, 0, Integer.MaxValue)) As Byte
                        pdr.GetBytes(iAtchOle, 0, BUFFER, 0, BUFFER.Length)
                        Dim BlobData As New IO.FileStream(sPath, IO.FileMode.Create, IO.FileAccess.Write)
                        BlobData.Write(BUFFER, 0, BUFFER.Length)
                        BlobData.Close()
                    End While
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabinetID2(ByVal sAC As String, ByVal sCabinetname As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CBN_ID from EDT_Cabinet where CBN_name ='" & sCabinetname & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function LogError(ByVal sAccessCode As String, ByVal Message As String, ByVal MyClassName As String, ByVal MyFunctionName As String)
        Dim iAccessCodeID As Integer, GMTOffset As Integer
        Dim sErrorLogPath As String, sGMTPrefix As String, sErrorDateTime As String
        Dim objclsGeneralFunctions As New clsGeneralFunctions
        Try
            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode.Trim)
            sErrorLogPath = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "ErrorLog")
            GMTOffset = DateTime.Compare(DateTime.Now, DateTime.UtcNow)
            If GMTOffset > 0 Then
                sGMTPrefix = "+"
            Else
                sGMTPrefix = ""
            End If
            sErrorDateTime = DateTime.Now.Year.ToString & "." & DateTime.Now.Month.ToString & "." & DateTime.Now.Day.ToString & " @ " & DateTime.Now.Hour.ToString & ":" &
                DateTime.Now.Minute.ToString & ":" & DateTime.Now.Second.ToString & " (GMT " & sGMTPrefix & GMTOffset.ToString & ")"

            If System.IO.File.Exists(sErrorLogPath) = False Then
                System.IO.File.CreateText(sErrorLogPath)
            End If

            Dim MsStreamWriter As New System.IO.StreamWriter(sErrorLogPath, True)
            MsStreamWriter.WriteLine("Date And Time # " & sErrorDateTime)
            MsStreamWriter.WriteLine("Class Name    # " & MyClassName)
            MsStreamWriter.WriteLine("Function Name # " & MyFunctionName)
            MsStreamWriter.WriteLine("Error Message # " & Message)
            MsStreamWriter.WriteLine("##################################################################")
            MsStreamWriter.Close()
        Catch ex As Exception
        End Try
    End Function
    Public Function GetFolderID2(ByVal sAC As String, ByVal sFoldername As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select FOL_FolID from EDT_FOLDER where FOL_Name ='" & sFoldername & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function SaveCabinetPermission(ByVal sNameSpace As String, ByVal Permissiontype As String, ByVal CabinetName As String, ByVal UserID As String,
                                   ByVal Department As Integer, ByVal CreateSubCabinet As Integer, ByVal ModifyCabinet As Integer, ByVal SearchCabinet As Integer,
                                   ByVal Index As Integer, ByVal ViewCabinet As Integer, ByVal Other As Integer) As Array

        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Dim sArray As Array
        Try
            sArray = UserID.Split(",")
            For i = 0 To sArray.Length - 1

                iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PermissionType", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = Permissiontype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Cabinet", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = CabinetName
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_User", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = sArray(i)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Department", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Department
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_View", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = ViewCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = CreateSubCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_MODIFY", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = ModifyCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0 'Vijeth SArray(3)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = SearchCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Index
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Other 'Vijeth
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0  'Vijeth SArray(4)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spEDT_cabinet_Permission", 1, Arr, ObjParam)
                ' Return Arr
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubCabinetPermission(ByVal sNameSpace As String, ByVal Permissiontype As String, ByVal SubCabinetName As String, ByVal UserID As String,
                                   ByVal Department As Integer, ByVal CreateFolder As Integer, ByVal ModifySubCabinet As Integer, ByVal DeActivateSubCabinet As Integer,
                                    ByVal SearchSubCabinet As Integer, ByVal Index As Integer, ByVal ViewSubCabinet As Integer, ByVal Other As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Dim sArray As Array
        Try
            sArray = UserID.Split(",")
            For i = 0 To sArray.Length - 1

                iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_PermissionType", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = Permissiontype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Cabinet", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = SubCabinetName
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_User", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = sArray(i)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_Department", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Department
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_View", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = ViewSubCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = CreateFolder
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_MODIFY", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = ModifySubCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_DELETE", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = DeActivateSubCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_SEARCH", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = SearchSubCabinet
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_INDEX", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Index
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Other
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_CREATE_FOLDER", OleDb.OleDbType.Integer, 4)
                ' ObjParam(iParamCount).Value = SArray(4)
                ObjParam(iParamCount).Value = CreateFolder
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spEDT_cabinet_Permission", 1, Arr, ObjParam)
                ' Return Arr
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFolderPermission(ByVal sNameSpace As String, ByVal Permissiontype As String, ByVal FolderName As String,
                                         ByVal UserID As String, ByVal Department As Integer, ByVal ModifyFolder As Integer, ByVal DeActivateFolder As Integer,
                                         ByVal ViewFolder As Integer, ByVal SearchFolder As Integer, ByVal Index As Integer, ByVal Other As Integer) As Array

        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Dim sArray As Array
        Try
            sArray = UserID.Split(",")
            For i = 0 To sArray.Length - 1

                iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_Id", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_PTYPE", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = Permissiontype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_GRPID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Department
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_USRID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = sArray(i)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_FOLDER", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = ModifyFolder
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_FOLDER", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = DeActivateFolder
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_VIEW_FOL", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = ViewFolder
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_MOD_DOC", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0  'vijeth SArray(4)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_DEL_DOC", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0  'vijeth SArray(5)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_CRT_DOC", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0  'vijeth SArray(6)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_SEARCH", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = SearchFolder 'Vijeth SArray(7)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_INDEX", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Index 'Vijeth SArray(8)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_EXPORT", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = 0  'vijeth SArray(9)
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBP_OTHER", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = Other
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@EFP_FOlId", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = FolderName
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "InOrUpFolPermissions", 1, Arr, ObjParam)
                ' Return Arr
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAlluserID(ByVal sAC As String, ByVal iDept As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select distinct(usr_id) from sad_userdetails where USR_DeptID ='" & iDept & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function GetDelCabinetID(ByVal sAC As String, ByVal iBaseID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select pge_cabinet from edt_page where PGE_BASENAME ='" & iBaseID & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function NewUsertDepartment(ByVal sAC As String, ByVal sUserDept As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select org_node from Sad_Org_Structure where org_name ='" & sUserDept & "' and org_levelcode='" & 3 & "' and org_delflag='A'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function NewUsertDesignation(ByVal sAC As String, ByVal sUserDesigantion As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID from Sad_Designation_Master where Mas_DESCRIPTION ='" & sUserDesigantion & "' and Mas_DELFLAG='A'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
        End Try
    End Function
    Public Function NewUsertIDandName(ByVal sAC As String, ByVal sUserName As String) As DataTable
        Dim sSql As String, aSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select max(usr_id) as UserID from sad_UserDetails where usr_loginName ='" & sUserName & "' and usr_dutystatus ='W' and usr_DELFLAG='A'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            'Update folder count
            aSql = "" : aSql = "Update sad_UserDetails set usr_dutystatus='A' where usr_id='" & dt.Rows(0).Item("UserID") & "' "
            objDBL.SQLExecuteNonQuery(sAC, aSql)

            Return dt
        Catch ex As Exception
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


End Class
