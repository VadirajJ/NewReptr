Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports BusinesLayer
Imports System.IO
Imports System.Net
Imports System.Web.Security
Imports System.Web
Imports System.Security.Cryptography

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class traceapi
    Inherits System.Web.Services.WebService
    Private clsfrm As New FormControl
    Private objclsSearch As New clsSearch
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Dim objclsSubCabinet As New clsSubCabinet
    Dim objclsFolders As New clsFolders
    Dim objCab As New clsCabinet
    Dim objclsArchive As New clsArchive
    Private Shared sSession As AllSession
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsUser As New clsUsers
    Private objclsLogin As New clsLogin
    Private objclsCPFP As New clsCPFP
    <WebMethod()>
        Public Function CreateCabinet(ByVal AccessCode As String, ByVal LoginID As String, ByVal CabinetName As String, ByVal Permissiontype As String, ByVal UserID As String,
                                     ByVal CreateSubCabinet As String, ByVal ModifyCabinet As String, ByVal SearchCabinet As String, ByVal Index As String,
                                      ByVal ViewCabinet As String) As String
            Dim iACC As Integer, iUsrname As Integer, iCabinet As Integer, iOther As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Dim sMessage As String
            Try
                If AccessCode = "" Then
                    sMessage = "ACCESSCODE_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If LoginID = "" Then
                    sMessage = "LOGINID_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If CabinetName = "" Then
                    sMessage = "CABINET_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Permissiontype = "" Then
                    sMessage = "Permissiontype_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Permissiontype <> "U" And Permissiontype <> "G" Then
                    sMessage = "Permissiontype_Should_Be[Userwise='U' or Groupwise='G']"
                    Return sMessage
                    Exit Function
                End If
                If UserID = "" And Permissiontype = "U" Then
                    sMessage = "UserID_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If CreateSubCabinet = "" Then
                    sMessage = "CreateSubCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf CreateSubCabinet <> "0" And CreateSubCabinet <> "1" Then
                    sMessage = "CreateSubCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If ModifyCabinet = "" Then
                    sMessage = "ModifyCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf ModifyCabinet <> "0" And ModifyCabinet <> "1" Then
                    sMessage = "ModifyCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If SearchCabinet = "" Then
                    sMessage = "SearchCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf SearchCabinet <> "0" And SearchCabinet <> "1" Then
                    sMessage = "SearchCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If Index = "" Then
                    sMessage = "Index_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf Index <> "0" And Index <> "1" Then
                    sMessage = "Index_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If ViewCabinet = "" Then
                    sMessage = "ViewCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf ViewCabinet <> "0" And ViewCabinet <> "1" Then
                    sMessage = "ViewCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If

                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    sMessage = "NOT_VALID_USERID"
                    Return sMessage
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                    'dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                    dt = clsfrm.GetCustomerDepartment(AccessCode, CabinetName) 'Getting Department ID

                    iCabinet = clsfrm.CheckCabName(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Checking for Cabinet name Existance
                    If iCabinet = 0 Then
                        clsfrm.CreateCabinet(AccessCode, sCabinet, iUsrname, dt.Rows(0).Item("USR_DeptID")) 'Creating new Cabinet
                    End If

                    If Permissiontype <> "G" Then
                        iOther = 1
                    ElseIf Permissiontype = "G" Then
                        iOther = 0
                        UserID = 0
                    End If

                    If CreateSubCabinet = 1 Or ModifyCabinet = 1 Or SearchCabinet = 1 Or Index = 1 Then   'View permission is must imp for other permissions
                        ViewCabinet = 1
                    End If


                    dt2 = clsfrm.GetCabinetID(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting cabinetID
                    clsfrm.SaveCabinetPermission(AccessCode, Permissiontype, dt2.Rows(0).Item("CBN_ID"), iUsrname, dt.Rows(0).Item("USR_DeptID"), CreateSubCabinet,  'Save permission
                                 ModifyCabinet, SearchCabinet, Index, ViewCabinet, iOther)

                    If Permissiontype = "G" Then
                        dt8 = clsfrm.GetAlluserID(AccessCode, dt.Rows(0).Item("USR_DeptID")) 'To Assisgn All UsersPermission to Group members
                        Dim sAllID As String = "", sInID As String = ""
                        For i = 0 To dt8.Rows.Count - 1
                            sInID = sInID & "," & dt8.Rows(i).Item("usr_id")
                        Next
                        sAllID = sInID.Remove(0, 1)
                        clsfrm.SaveCabinetPermission(AccessCode, Permissiontype, dt2.Rows(0).Item("CBN_ID"), iUsrname, dt.Rows(0).Item("USR_DeptID"), 0,  'Save permission
                                                   0, 0, 0, 0, 0)
                    End If

                    Return dt2.Rows(0).Item("CBN_ID")

                    ElseIf iUsrname = 0 Then
                        sMessage = "NOT_VALID_USERID"
                        Return sMessage
                    End If
                ElseIf iACC = 0 Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                End If
            Catch ex As Exception
                sMessage = "IN_VALID_PARAMETER"
                clsfrm.LogError(AccessCode, ex.Message, "FileDocumentINEdict", "EDICT_API")
                Return sMessage
            End Try
            Return ""
        End Function
        <WebMethod()>
        Public Function CreateSubCabinet(ByVal AccessCode As String, ByVal LoginID As String, ByVal CabinetName As String, ByVal SubCabinetName As String, ByVal Permissiontype As String,
                                         ByVal UserID As String, ByVal CreateFolder As String, ByVal ModifySubCabinet As String, ByVal DeActivateSubCabinet As String, ByVal SearchSubCabinet As String,
                                         ByVal Index As String, ByVal ViewSubCabinet As String) As String
            Dim iACC As Integer, iUsrname As Integer, iCabinet As Integer, iSubcabinet As Integer, iOther As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Dim sMessage As String
            Try
                If AccessCode = "" Then
                    sMessage = "ACCESSCODE_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If LoginID = "" Then
                    sMessage = "LOGINID_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If CabinetName = "" Then
                    sMessage = "CABINET_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If SubCabinetName = "" Then
                    sMessage = "SUB_CABINET_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Permissiontype = "" Then
                    sMessage = "Permissiontype_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Permissiontype <> "U" And Permissiontype <> "G" Then
                    sMessage = "Permissiontype_Should_Be[Userwise='U' or Groupwise='G']"
                    Return sMessage
                    Exit Function
                End If
                If UserID = "" And Permissiontype = "U" Then
                    sMessage = "UserID_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If CreateFolder = "" Then
                    sMessage = "CreateFolder_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf CreateFolder <> "0" And CreateFolder <> "1" Then
                    sMessage = "CreateFolder_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If ModifySubCabinet = "" Then
                    sMessage = "ModifyCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf ModifySubCabinet <> "0" And ModifySubCabinet <> "1" Then
                    sMessage = "ModifyCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If DeActivateSubCabinet = "" Then
                    sMessage = "DeActivateSubCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf DeActivateSubCabinet <> "0" And DeActivateSubCabinet <> "1" Then
                    sMessage = "DeActivateSubCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If SearchSubCabinet = "" Then
                    sMessage = "SearchSubCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf SearchSubCabinet <> "0" And SearchSubCabinet <> "1" Then
                    sMessage = "SearchSubCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If Index = "" Then
                    sMessage = "Index_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf Index <> "0" And Index <> "1" Then
                    sMessage = "Index_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If ViewSubCabinet = "" Then
                    sMessage = "ViewSubCabinet_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf ViewSubCabinet <> "0" And ViewSubCabinet <> "1" Then
                    sMessage = "ViewSubCabinet_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                    Exit Function
                End If

            iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
            iUsrname = clsfrm.GetUserID(AccessCode, LoginID) 'checking for LoginName
            If iUsrname = 0 Then
                    sMessage = "NOT_VALID_USERID"
                    Return sMessage
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                    ' dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                    dt = clsfrm.GetCustomerDepartment(AccessCode, CabinetName) 'Getting Department ID

                    Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                        Dim sSubCabinet As String = objclsEDICTGeneral.SafeSQL(SubCabinetName)
                        iCabinet = clsfrm.CheckCabName(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Checking for Cabinet name Existance
                        If iCabinet = 0 Then
                        clsfrm.CreateCabinet(AccessCode, sCabinet, iUsrname, dt.Rows(0).Item("USR_DeptID")) 'Creating new Cabinet
                    End If
                        dt2 = clsfrm.GetCabinetID(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting cabinetID
                        iSubcabinet = clsfrm.CheckSubCabName(AccessCode, sSubCabinet, dt2.Rows(0).Item("CBN_ID")) 'Checking for subcabinet name Existance
                        If iSubcabinet = 0 Then
                        clsfrm.CreateSubCabDetails(AccessCode, dt2.Rows(0).Item("CBN_ID"), sSubCabinet, iUsrname, dt.Rows(0).Item("USR_DeptID")) 'Creating new Sub-Cabinet
                        clsfrm.UpdateSubCabDetails(AccessCode, dt.Rows(0).Item("USR_DeptID"), dt2.Rows(0).Item("CBN_ID")) 'Updating Sub-cabinet 
                        End If
                    'dt4 = clsfrm.GetCabinetID(AccessCode, sSubCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting Sub-cabinetID
                    dt4 = clsfrm.GetSubCabinetID(AccessCode, dt2.Rows(0).Item("CBN_ID"), sSubCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting Sub-cabinetID

                    If Permissiontype <> "G" Then
                            iOther = 1
                        ElseIf Permissiontype = "G" Then
                            iOther = 0
                            UserID = 0
                        End If

                        If CreateFolder = 1 Or DeActivateSubCabinet = 1 Or ModifySubCabinet = 1 Or SearchSubCabinet = 1 Or Index = 1 Then   'View permission is must imp for other permissions
                            ViewSubCabinet = 1
                        End If

                    clsfrm.SaveSubCabinetPermission(AccessCode, Permissiontype, dt4.Rows(0).Item("CBN_ID"), iUsrname, dt.Rows(0).Item("USR_DeptID"), CreateFolder,'Save permission
                                                     ModifySubCabinet, DeActivateSubCabinet, SearchSubCabinet, Index, ViewSubCabinet, iOther)
                    If Permissiontype = "G" Then
                            dt8 = clsfrm.GetAlluserID(AccessCode, dt.Rows(0).Item("USR_DeptID")) 'To Assisgn All UsersPermission to Group members
                            Dim sAllID As String = "", sInID As String = ""
                            For i = 0 To dt8.Rows.Count - 1
                                sInID = sInID & "," & dt8.Rows(i).Item("usr_id")
                            Next
                            sAllID = sInID.Remove(0, 1)
                        clsfrm.SaveSubCabinetPermission(AccessCode, Permissiontype, dt4.Rows(0).Item("CBN_ID"), iUsrname, dt.Rows(0).Item("USR_DeptID"), 0,'Save permission
                                                     0, 0, 0, 0, 0, 0)
                    End If

                        ' iPageDetailsid = clsfrm.CreateIndex(AccessCode, dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID"), dt6.Rows(0).Item("FOL_FolID"), 1, Tilte, Keyword, dt.Rows(0).Item("USR_ID"), UploadedFileList) 'File index
                        Return dt4.Rows(0).Item("CBN_ID")

                    ElseIf iUsrname = 0 Then
                        sMessage = "NOT_VALID_USERID"
                        Return sMessage
                    End If
                ElseIf iACC = 0 Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                End If
            Catch ex As Exception
                sMessage = "IN_VALID_PARAMETER"
                clsfrm.LogError(AccessCode, ex.Message, "FileDocumentINEdict", "EDICT_API")
                Return sMessage
            End Try
            Return ""
        End Function

        <WebMethod()>
        Public Function CreateFolder(ByVal AccessCode As String, ByVal LoginID As String, ByVal CabinetName As String, ByVal SubCabinetName As String, ByVal FolderName As String,
                                        ByVal Permissiontype As String, ByVal UserID As String, ByVal ModifyFolder As String, ByVal DeActivateFolder As String, ByVal ViewFolder As String,
                                        ByVal SearchFolder As String, ByVal Index As String) As String
            Dim iACC As Integer, iUsrname As Integer, iCabinet As Integer, iSubcabinet As Integer, iFolder As Integer, iOther As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Dim sMessage As String
            Try
                If AccessCode = "" Then
                    sMessage = "ACCESSCODE_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If LoginID = "" Then
                    sMessage = "LOGINID_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If CabinetName = "" Then
                    sMessage = "CABINET_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If SubCabinetName = "" Then
                    sMessage = "SUB_CABINET_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function

                End If
                If FolderName = "" Then
                    sMessage = "FOLDER_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Permissiontype = "" Then
                    sMessage = "Permissiontype_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Permissiontype <> "U" And Permissiontype <> "G" Then
                    sMessage = "Permissiontype_Should_Be[Userwise='U' or Groupwise='G']"
                    Return sMessage
                    Exit Function
                End If
                If UserID = "" And Permissiontype = "U" Then
                    sMessage = "UserID_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If ModifyFolder = "" Then
                    sMessage = "ModifyFolder_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf ModifyFolder <> "0" And ModifyFolder <> "1" Then
                    sMessage = "ModifyFolder_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If DeActivateFolder = "" Then
                    sMessage = "DeActivateFolder_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf DeActivateFolder <> "0" And DeActivateFolder <> "1" Then
                    sMessage = "DeActivateFolder_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If ViewFolder = "" Then
                    sMessage = "ViewFolder_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf ViewFolder <> "0" And ViewFolder <> "1" Then
                    sMessage = "ViewFolder_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If SearchFolder = "" Then
                    sMessage = "SearchFolder_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf SearchFolder <> "0" And SearchFolder <> "1" Then
                    sMessage = "SearchFolder_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If
                If Index = "" Then
                    sMessage = "Index_Permission_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                ElseIf Index <> "0" And Index <> "1" Then
                    sMessage = "Index_Permission_Should_Be[Yes='1' or No='0']"
                    Return sMessage
                    Exit Function
                End If

                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode

            iUsrname = clsfrm.GetUserID(AccessCode, LoginID) 'checking for LoginName
            If iUsrname = 0 Then
                    sMessage = "NOT_VALID_USERID"
                    Return sMessage
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                        Dim sSubCabinet As String = objclsEDICTGeneral.SafeSQL(SubCabinetName)
                        Dim sFolderName As String = objclsEDICTGeneral.SafeSQL(FolderName)
                    'dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                    dt = clsfrm.GetCustomerDepartment(AccessCode, CabinetName) 'Getting Department ID

                    iCabinet = clsfrm.CheckCabName(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Checking for Cabinet name Existance
                        If iCabinet = 0 Then
                        clsfrm.CreateCabinet(AccessCode, sCabinet, iUsrname, dt.Rows(0).Item("USR_DeptID")) 'Creating new Cabinet
                    End If
                        dt2 = clsfrm.GetCabinetID(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting cabinetID
                        iSubcabinet = clsfrm.CheckSubCabName(AccessCode, sSubCabinet, dt2.Rows(0).Item("CBN_ID")) 'Checking for subcabinet name Existance
                        If iSubcabinet = 0 Then
                        clsfrm.CreateSubCabDetails(AccessCode, dt2.Rows(0).Item("CBN_ID"), sSubCabinet, iUsrname, dt.Rows(0).Item("USR_DeptID")) 'Creating new Sub-Cabinet
                        clsfrm.UpdateSubCabDetails(AccessCode, dt.Rows(0).Item("USR_DeptID"), dt2.Rows(0).Item("CBN_ID")) 'Updating Sub-cabinet 
                        End If
                    'dt4 = clsfrm.GetCabinetID(AccessCode, sSubCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting Sub-cabinetID
                    dt4 = clsfrm.GetSubCabinetID(AccessCode, dt2.Rows(0).Item("CBN_ID"), sSubCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting Sub-cabinetID
                    iFolder = clsfrm.CheckFoldersName(AccessCode, iUsrname, sFolderName, dt4.Rows(0).Item("CBN_ID")) 'Checking for Folder Existance
                    If iFolder = 0 Then
                        clsfrm.CreateFolder(AccessCode, dt4.Rows(0).Item("CBN_ID"), sFolderName, iUsrname) 'Creating new Folder
                        clsfrm.UpdateFolderCount(AccessCode, dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID")) 'Updating Folders
                        End If
                    dt6 = clsfrm.GetFolderID(AccessCode, sFolderName, dt4.Rows(0).Item("CBN_ID")) 'Getting FolderID


                    If Permissiontype <> "G" Then
                            iOther = 1
                        ElseIf Permissiontype = "G" Then
                            iOther = 0
                            UserID = 0
                        End If

                        If ModifyFolder = 1 Or DeActivateFolder = 1 Or Index = 1 Or SearchFolder = 1 Then   'View permission is must imp for other permissions
                            ViewFolder = 1
                        End If

                    clsfrm.SaveFolderPermission(AccessCode, Permissiontype, dt6.Rows(0).Item("FOL_FolID"), iUsrname, dt.Rows(0).Item("USR_DeptID"),
                                                    ModifyFolder, DeActivateFolder, ViewFolder, SearchFolder, Index, iOther)                               'Save permission

                    If Permissiontype = "G" Then
                            dt8 = clsfrm.GetAlluserID(AccessCode, dt.Rows(0).Item("USR_DeptID")) 'To Assisgn All UsersPermission to Group members
                            Dim sAllID As String = "", sInID As String = ""
                            For i = 0 To dt8.Rows.Count - 1
                                sInID = sInID & "," & dt8.Rows(i).Item("usr_id")
                            Next
                            sAllID = sInID.Remove(0, 1)
                        clsfrm.SaveFolderPermission(AccessCode, Permissiontype, dt6.Rows(0).Item("FOL_FolID"), iUsrname, dt.Rows(0).Item("USR_DeptID"), 'Save permission
                                                    0, 0, 0, 0, 0, 0)
                    End If
                        Return dt6.Rows(0).Item("FOL_FolID")
                    ElseIf iUsrname = 0 Then
                        sMessage = "NOT_VALID_USERID"
                        Return sMessage
                    End If
                ElseIf iACC = 0 Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                End If
            Catch ex As Exception
                sMessage = "IN_VALID_PARAMETER"
                clsfrm.LogError(AccessCode, ex.Message, "FileDocumentINEdict", "EDICT_API")
                Return sMessage
            End Try
            Return ""
        End Function
        Public Shared Function Base64DecodeString(ByVal inputStr As String) As Byte()
            Dim decodedByteArray As Byte() = Convert.FromBase64CharArray(inputStr.ToCharArray(), 0, inputStr.Length)
            Return (decodedByteArray)
        End Function
        'Public Iterator Function GetBytesFromByteString(ByVal s As String) As IEnumerable(Of Byte)
        '    For index As Integer = 0 To s.Length - 1 Step 2
        '        Yield Convert.ToByte(s.Substring(index, 2), 16)
        '    Next
        'End Function
        <WebMethod()>
        Public Function UploadFile(fileName As String, contentType As String, bytes As String) As String   'Vijeth
            Try
                'Dim s1 As String = "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H2K2/55p/3yKAG/YbUn/VJ/3yKPsNr/AM8k/wC+RRYBh0+0PWFP++RSjT7P/nin/fIosAn9m2R/5Yp/3yKadLsT1gj/AO+RRYCE6Ppx/wCXeP8A75FPGk2AGBBGP+AiiyAT+x9O/wCeEf8A3yKT+xtN/wCfeP8A75FFgGf2Jpp/5d4/++RTDoWmn/l3T8qAEGg6Z/zwT8qT+wNL/wCeC0AH9gaWf+WC0w+HtLx/qF/WgBg8OaWBjyR+ZpP+Ec0s/wDLEfmaAG/8I1peP9T+p/xpp8MaUf8All/483+NKwDf+EX0r/nmf++m/wAaZ/wiul/88z/303+NMB3/AAi2lf8APM/99N/jTf8AhFdL/wCeZ/76b/GgBg8K6YT9xv8Avo0Hwppn91v++jQA3/hE9N9G/wC+jS/8Ilpno/8A30aBjT4S030f/vqm/wDCI6d/t/8AfVAhP+ER07/b/Ok/4RDT/V/zpARnwdY/35PzH+FIfB1j2eT8x/hRr3AD4OscD55PzH+FM/4Q2z/56yfp/hTAYfBlp/z1k/T/AApv/CGWv/PaT9P8KAGnwZbf89n/AE/wo/4Qy3/57P8AkKAD/hC4P+ez/kKb/wAIXD/z3b8hQAjeC4scTtn3AqMeCl7zn/vn/wCvRqMX/hC0/wCe5/75pB4LX/nuf++aWoCf8IUP+e//AI7/APXpp8Fek/8A47/9ejUQweCnx/rx/wB8/wD164zV9KfTZlQuH3DOQMUwPoeL7i/QVLTAKKAEpaACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAKKACigBMUuKQCYpaYBRQAUtABSUALSUALSUALRQAUUAFFABRQAlFABRQAUUAJQBSAXFGKYCYoxQAYpaADFFABRQAUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFACUtABRQAUUAFFACA5paACkoAWigAooAKKAEpaACigApKAFpMCgBaKACigBKKADFLQAUUAFFABRQAUUAFeN+Mz/pcQ/6Z/wBaAPYIxhR9KkoASloASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKKACloAKSgApaACigAooAKKACmqMDGc/WgBaWgAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArxrxn/x+Rf8AXP8ArQB7Cn3R9KfQAtFACUUALRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlABS0AFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXi/jL/j9j/65/1NAHsifdH0qSgApKAFpKAFooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBaKAEpaAEpaACigAooAKKACigAooAQCloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArxbxl/wAf0f8A1zH8zQB7Kn3RUlABRQAUUAFFABSDNAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4n4xJ/tBP+uQ/maAPaV4Apc0gFopgLRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSZoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACvD/F5/wCJkP8Arkv8zQB7aKdSAKWmAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJgZzQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQBDI+zHBOSBxU1ABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABXh/i8/8TIf9c1/maAPbVp9ABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXhni0/8AEz/7Zr/WgD3ACn0gCkpgFLQAUUAFFABRQAUUAICCKWgAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKTHNAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4V4sP/ABND/uLQB7oOlFIBaKYBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXhHiv/AJCjf7i0Ae6ilpALSUwFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBaKACigAooAKKACigAooAKKACigAooASigBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACkFAC0UAFFABRQAUUAFeEeKznVG/3V/lQB7sKWgAooATvRQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlACUCkA6imAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4N4q/wCQq/8Aur/KgD3cUtAC0UAJS0AFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAJTQ6k8EGgB9FABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABTTQAtFABnmloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArwXxSc6o/8Aur/KgD3mloASloAKKACigAooAKKACigApM80AZ93f2touZZFX6nmuXm8WWMZ4V2HqBwfzpiNjStYh1MOUVl2Yzn3roaQzH1HU7fT4t8h5PQDqTV21laaBHZdpZQSPTNAi3RQMKaSACT2oA8xe7u9cu5IYZPKij6sOp7Vc0/w1Na3aytcFlU5wM5P15oEeiUUDOa1nWF0xEJQuXOMfSt2CUTRI4GNwBwfegCG4vba2A8yRVz6msc+ItLBx5wpiub8MyTRq6HKsMg+1T0hmPdanb28scROXkYAKOvPc1sUAFFABRQBx2q6vLHOttbgNO/r0Uetc62meImmVjODgg/ewPyxQI6u8123s7mOB8l2xnA4Ga6egYtFABRQAVG8iIMswA9zQBGlxDJ911P0NWKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEooAKKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAGDkn2p9ABRQAUUAFFABRQAUUAFFABRQAV4L4p/5Cj/7q/wAqAPeRRQAtFABRQAUUAFFABRQAUUAFZ9/c/ZbWSX+4pNAHG6Npq3g+2XP7x5OVB6KO3FQ+LykdnGigDL9vQCkIqeC2+WdfdTXoV9eRWVu0jnAA/M9hTGeFCabV9TTec73Ax2C+n5V9CKMAAdqAK73MMbhWdQx6AnmrOcCgBqurdCDWDrtz9m06ZgcErtH1PFAHOeD7fZaPIert+g/+vXYald/ZLOSUdVUkfXtQBy/hm9vL0SyTNkZCqOg967ygCjNb21yQHVXKnIzzisbW9QNhbqIx+8kOxB70CMmy8Nxt+8uiZZG5OTwK8+8SwwQ6gUjUKAi5A45pAevaG4fTIPZAPy4pmtaqmnWxbq7cKPemM868Lxveam80h3FAWyfU8D+te0UAJnFFAC1DNIsUbMTwATQB5X4YVrvUZ7lucdD/ALx/wr1qgDEn0m0uLlZ3XLr+X5VtUgFopgFFAHFa7rq2K+XH80zdB6Z71kWOgzXg829kZieQmePxoEV9Z0GKxtjPbsyMhBPPau40cTfYIvNYsxXJJ680gNyimMKKAEooAWigAooAKKACigCldXkFqm6Vwo96mhmSaJXX7rAEfQ0AT0UAFFABRQAVz2qaxBpyjd8zt91B1NAHNy6jr4jMwt0VAMlc5bFbuh6tJqcLu0YTadvBzmgR1FFAwooAKKAEzS0AFJQAtJQAUUALRQAUUAFMd1RSWIAHc0Ac9J4g0yPOZlOPTn+VLBr+mzEBZhk8AHj+dMm50QIIyKWkUFVridLeF5G+6gJP4UAY+lazDqauUVlKnkH3roaACigAooAKKACigAooAKKACkoAKWgCN3WNSzEADqTTYpo5l3IwYHuOaAJqKACigApu4ZxnmgB1FABRQAUUAFRSSJEpZiFA6k0AcnL4o01JNgYtzjIHH512AORmgQtFAwooAKKACigAooAKKACigAooAKKACigAooAKKACigAooA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3bf7uO+KztHsbQ310s6gurkjd6GkA+2t0+0X8UH+r2YAHTcR2roNDuYhpKnI+RSGH0oAd4XU/Ymfp5kjMB+NM19vLns3P3Vm5P1oGQ+KZUazSMYLSOoFUryAahqiW0rERxxhwBwSfrQISK3XStXjihbKTg7geSMDg5rP0TS7a6ubrzxvKSEAHpyetAG/oeYL26t0yYoyCuexPUV3dMDmPEf/ILm+g/nWBqoP2fTh1/eJ/KgBdQjbSb0XScRS/LKPT0NZ2nuraJeMO7OfzoA7fRBjTYP9wVhTv5viGEJz5cZ3fjSAi0k41m/J7bawLbUJX1Oa4+zvKpyilRkAA/1oA1fDVyUuriBlMe5jIitwQCa9LpjCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuf1XSYtSRFd2UKc8etAGTpvhu3sLkSiRmIBABx3roNSsUv7ZomYqDg5HtQBxQ8G2wI/fP79Oa9CWNUi2LwAMDHakB5y/g5XYsZySSSePWu80+yWytkiU5Cjr60AcVregNdTvcNOFAHQjoB+NcHBD5IIznJrkqysrdz6HAUuabq9I6fNkVxbmUg5xioorPYwJOcVkqloctuh6U8Hz4j2rlpzJ29C5MjOhAOM1mpZMGBJHFKE1GLXVlYjDTrVoTuuWKSt87mzQRmuY9u2hlS2QPK8e1Uvsc3oK9CNVW13PkK2Bnzt09YvX0JUsnPU4rWihSMcfnWM6nNotj0MLhPZPnn8XRdixWdPaiQ5BwayhLldz0cRRVem47PdPzMxrWVe2fpUYt5T/Ca9D2kbbnxzwldSty38y9FZHOW/KtkAAYFcVSfM9NkfU4TD+wi3L4pb+QtLXMewFFAwp6qzsABkngCgTdld9D0nSdIFuBJIMv29q62vWhHlikfnmJre2qyl0Wi9EFFbHAFFABRQAUUAFFABRQAUlAC18669/yFJ/97+goA+iqKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKACigBaKACigAooAKKACigApKAFooAKKACigAooAKKAOVvtJnvLpS837hSG8vHUiupAwKADApaAG4HpSkA0AZ5s0N0JySWC7QOw9a0KQDDGp7CnAAdqAInhjc5Kgn3FPCKDnAz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wD6Ea6+mAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVBPEs0bI3Rhik9VYuLcZKS3TT+48cvLSS1mKN+B9RVCvGas2ux+lwmqkIzW0kmFFSahRQAUUAFFABRQAUUAOAJOAMmtq30i8nwQm0ercVcYuT0OWrWp0Veb+XU6218PQJgyEufTtXVRxJGuFAA9q9KEFH1PiMRiZ132itkTUVueaFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXzprp/4mlx/v/wBKAPouigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKACloAKSgAooATFGKAFooAKWgApKAFppUEdKAFpMAmgBGVW6gH60oUKMAYoA5W70iUTGa1cRu33lIyrfUetUWTWim3yIDjvnjP0xSEW7PR5fNWa5k3uv3VXhF+grqljRCSFAJ6kDrQMVY0TO0AZ5OBUZgiKldgweSMcGmA54Y5F2soI9COOKJIo5FwygjOcH2oAZNbwzrtkRWHoRmmwWsECkRoqg9cDFKwFH+yLDzfM8lN2c5xWNqmnG51C0Pl7o13b89AMcUAb1tptnasWiiVCe4FWVtoVd3CgNJwx9cUARwWVvbxGNECoc5Hbms6PQtOjl8xYVDA5Hp+VAF26061u8eagYr0PcfjTbTTLS0yYowpPBPc/jQBYtrSG2QrGuASWI9z1pbe1ht4yiLhSSce560wFtbWK1iEcYwoycfXmrdABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQBn3dlDdptcfQ9xXmt7o9zbEkDevqP8K4qsL+8vmfSYHEqH7qb0b919jBIxSVwH14UUAFFABRQAVPHDLJ91S30FMltRV27I24NEvJcZUKPeujg8ORLzIxb2HArqjSb1Z89Xx8Y3jS1fc6W3sre3HyIBV+u9JJWR8pOcqknKTu2FFUZBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFfOeuf8hO4/36APoyigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAyr3UIbLy9+f3jhAR6n1qW9vI7O3aV/ur6dTQAllew3sCyxn5T69qhsdShvWlCZ/dttJPrQBr1lNqEK3i25zvZdw9MUAatFAFG8u4rOEySZ2jrgZNJ9ttwI8tgy42g9Tn2oAv0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACZpaACigAooAKSgBaKACigAooASloAKSgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgDNn061n+9GM+o4NYUvhy2Y5VmX261zypRlrsevRxtWkrfFHszPPhpu0o/KqZ8O3fZkP51yujLoz2Y5jTfxRa9AHh27P8SfmamHhuf8A56L+RoVKXWxbzGl0i2XU8NJj5pTn2FX08PWinncfqa2VFdWedPMaj+FJGrFptnF92Nc+p5rRWNE6AD6V0qKWyPGnVqVHeUmySirOYKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAK+dNd/5Clx/v/0oA+i6KACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooA5bxHbmbTZCPvJhx+FYWoyi/SxhzxMQzfQDNAhNOmXTlvoSeISXX6MOKrQ3Eul6NGy482d8jPHLf4CgB8t9faeEle5SZSQHTjjPpio9RFzLrkXkEK5i6kdBQI1YL3ULTUUguWWRZQdjAY5FZzalqV1dTJFLHGY2wqN1bFAF+51C+i0d5JUCyqwHIyCM9aw9W+2S6hZlGUFlGzjocc5oGdDeapeLOlrDsMuzc7N0FLYapdpeC2udhZxlWQ8fSgDt6y7JrwmTzwowx2Y7r70DKGt309jbrKgBAcBs+hrM1jXHs5LdUA/eEFs9hxQST6rqlzDNHFAqs7qWOewFRx64TpBuSo3gEEdt2cUDGJrx/sj7SQu/pjoM5xUmk6vc3E8kU6KjKqsMehpiH6XrZvLq4jIAEZ+U+ozir+k6jJfiVioVVcqpHcDvSGZ2pa1NaXqQRxeYWXIAPOaz4/Es/mGF7VvP7ICMH8aNA1NfTtZe5lkikhMUiDcQTnisuLxLLMjNHau4Qk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CigAooAKKACigAooAK+c9d/5Cdx/v8A9KAPouloAKKACigAooAKKACigAooAKKACigAooAKTHNAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJXzprn/ITuP9+gD6LpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAKKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigBK+ddd/5Cdx/v/wBKAPouigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooASvnXXf8AkJ3H+/8A0oA+i6KACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgCKIyFBvADdwORU1ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfOuujGqXH+/8A0oA+i6KACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAbzn2p1ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfO2vHOqXH+/8A0FAH0TS0AFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVmLeo1yYsc+tS3a3m7FJXv5K5p0VRIUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABXzprv/IUuP9+gD6KFLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVUW1iWQuB8x70mr28hptX8y3RTEFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJXztr3/IUuP97+goA+iaWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+dde/5Clx/v/wBBQB9E0tABRQAUUAFFABRQAUUAMZ1QZJwKfQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV876/8A8hW4/wB7+goA+h6WgAooAKKACigAooAKKAEIBpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBK+d9f/AOQpcf739BQB9E0lAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJzmgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+d9f8A+Qrcf739BQB9D0tABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABTWGR1xQAtLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFfO+v/8AIVuP97+goA+iKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkGaAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+d/EAxqtx/vD+QoA+h6WgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEooAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASvnrxD/wAhW4/3h/IUAfQtLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJRQAtJjmgBaKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACvnjxB/yFbj/eH8hQB9DUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJTeaQBnjPSkFAElFMBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+ePEH/IVuP94fyFAH0NS0AFJQAtFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUAJgClpALRTAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAr548Qf8hW4/3h/IUAfQ1LQAlFAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAnNLQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAJmvnjxB/yFbj/eH8hQB9D0tABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJXzz4g/5Ctx/vD+QoA+hqWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBa+ePEH/IVuP94fyFAH0MKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBa+ePEH/IVuP8AeH8hQB9D0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFACV88a/8A8hW4/wB7+goA+hxS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQBh32pC2dY0RpZX5CL1x6n0qtbauzXCwzwtC7fdyQQ30IoEXb/UY7Pau0vI5wiL1NRWt5eSSBZLZowR97cCB9aAN2sbU9RTT7fzCpYkgBR1JNAy1Y3aXlukq9GGceh9Kv0AFFABRQAUlAGfe3sNlEHkzgkLx6mtAHIzQBn3109tFuWNpGJwFX1rmzrN7bSxi5twiSMFDK2cE+tAje1DUEs4g2CzOQqKOpJrDl1W/tAr3ECrESASjZK59RigDorm+gtoRI54PTAyST6CsfStXe/uJozEY/LxjPXn1FAFzUdQa1aONE3ySnCgnA47ms9dTvLe5jjuYlUSnCshyM+hzQB1tFAwooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigApKAA18666c6pcf7/8ASgD6KpaACigAooAKKACigAooAKKACigAooAKKACigAooA420w+u3RI5SNFH0NN8RHY1m46i4UfgetAFKaeYa4+yIyskQCjOAM8k8+tbMOqzLdJBcQ+W0mdhB3A4oEdRXHXB+2axHH/Bbrvb/AHm4H6UDGaOxtby4tT0DeYn+63P86nFzP/bxi3Hy/I3be2c0CEvriZNXtEDEI6vkdjgVXZJNQ1CeN5XjSELtVDtJz3oATRklF7cq07yLGQFycjB/qKs6DcTTG63sW2zsoz2AoAbp80zareozEqu3aD0GRWHZBb6WUT3Ekc4cgKG24APGB3oAseKLT/REcyPkMq4zwffHrXX2FmLWMgSO+7B+c5x9KANWuK1lhezxWicncHcjso9/egYamT/a1gnb5z+IFauuqG0y4/3CfypATaZiawt2YZPlqeR3xWJpZH9r3w9Nn8qYEmvgL5MqNiZG/drjO4nqMVkx3El1qMIvF8kpzGnUM3rmgR6PRQMKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBK+dte/5Clx/vf0FAH0TS0AJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFAHFXBbT9VM7KTFMgVmUZ2sPWop5l1e7t1iUtFE/mO5BAyOgHrQIdeO2naqLhlJilQIxAztI6Zprzf2pqNuYlJjgJZnIwCSMYGetIDtmcKhbsBmuB03SVvVkuZjIryuxwGK/KOB0pjEu9PGmXlvcReYwLbHySxwfr6VYvnay1dLllYxPH5ZKjO05zzSEQSTS3er2kixMIl3gMRjOR1x2FU7qEzancfuXmwFw0bbdvHQ8igDpNKubZXNusTQuBuKt1PvnvWNZ3J0u7uY5Y3Ikk3oVUsDn6UAT6R9pOo3kkkRTcqkD25xz61R1S6tr6BlFtJ9o6L8hBDf73SgDR1a1uZNGRSN8iBGYDqSOtdHp98l3HlVdduAdykc/jTAr6xd3FraFoYy7ngYGce+K5TTL/7JGc2tw0jcu5XkmjQRtatDNILe6jQloTuKfxbSOR9ap3l9JqcH2eGGQGTAZnUqFHfk0DOlllFhbIAjuFAUBBk1xFlezQ6hcym2mKy7cfLzx60tANjU0miv4LoRtIiqVZQMsue+KqXskmrvCkULqEkDs7rtwB6ZoA78UtMYUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACGvnfXznVJ/97+goA+iKWgAooAKKACigAooAKKACigAooAKKACigAooAKKAEIB680gAHQYoAUgHrSAAdBigB1JQAUEA9aADFcl/Z9/azytbuhWVt5WTPBPoRQBbs9OnW6NxO4aQrtUKMKorosUALSUALSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACV8667/AMhO4/3/AOlAH0VS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAhr501z/kJz/7/APSgD6LpaACigAooAKKACigAooAKKACigAooAKKACigAphUFge4oAfRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAhr5z1v/kJT/79AH0ZS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACV85a3/yEp/980AfR1FABRQAUUAFFABRQAUUAFFABRQAUUAFJQAUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfOet/8hKf/foA+jBS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAIM0tABRQAUUAFFABUaqQxOSc9vSgCSigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAK+cdb/5CU/8AvmgD6NpaACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkzQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfOet/8hO4/3zQB9GUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV8565/wAhO4/36APosUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFfOmu/8hS4/3/6UAfRIp1ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACV83avIJNRnYDH7wj8uKAPpGloAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEr5x1oY1Kf/AHzQB9HUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFADHbaufSnUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfOet/8hK4/wB80AfRlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFADSwHU4pQQelACbh60Ag0AJuHqKdkCgBu9fUUu4etAAGBpu9fUUAO3LjOaWgBhkRTywH40/OaAG71B6inEgUAR+Yn94fnTg6noRQA3zY/7w/Onhgw4OaAHUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAx13KR6ilAwAKAHUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJXzlrf/ISuP980AfRtLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFQyyCKNnPRQSfwoA8os4LnxBNJJJIyQqcBVNdfZaRJp7uVmd4yh+VjnB9qBHD6JYDUVlaWeRdrYGGruBYpY6dOEkZ8qxyxyelAHG6RpYvLBpmnkVgWwQxwMVr6I0+qadLFLI3DbQ46469aAOd1LTGtL6CFZ5SJTySeRzjiu1Gkrp9rcMssjloz949MDtSGUtAZ20aQliT8/JNc9oekDULZpHmkB3EYDUAa+vWxsdISNHY7XHJPPOa27/AFBrHSEdfvlVVfqR1piMHT/Dv223E1zK5dxuHPTPSu106zextyjSmQAkgnqB6UageR3Zurlpr5GO1JQAOeg7/SvXV2alYDkgSoOR1GaAPLNZ0lLG4t40lciU4OT7gcfnXbpo0On287o7kmJh8x9qQHJaFptleWpeaRg24j7+OK9P0+0htIAkZJXOck560wNSigYUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUnegBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASvnLW/8AkJT/AO+aAPo0UtABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABWdqEbS2kqjqyMB+VAHD+D50+zSRHhlfJHfBrv5JEIK5GdpOO9AjyLw/pFtfJMZCw2vgYOK9CksorPTJY0yVCMeTnqKAPM7DSZLjSZJUlcEbvkB+U49vevQ/DdzDPYLsUIV4YD1/wDr0Ac/r/GsWX1H867rUv8Ajym/3G/lQM4zw7/yBZP+B1g+H9Je8tWYXEkfzEYQ4HQUAbXiO3NtpEcZYvtcDc3U03xBEX0aFhyF2E/likI67SLqKexiZSOFAPsQOaoa7qKQ6fKUYEn5Bj1NMDj7WS9j0s24snIZT82eue9bPhK6LQSQNw0TdD6H/wCvQBW8Uf8AH5Zf7/8AUV3l9/x5y/7jfypDPM/D2j2F7Zl5Vy24j7xHFep20EVvCscf3VGB3piLVLQMKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAENfOWt/8AISn/AN80AfRopaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooA4m88MwTTmWKRoWPXbV/TtEjs3ZzI8jsNpZj2pAY6eFUjJKXMqZOflOK37bSvItpIjK8gkzkuckZGOKeoEum6ZHYWxiViwJJyfeq+m6NFp8kjI7ESfwnoKQD73SIru5imZmBi6AdDzmtieETRMh4DAj86AMmx0qKztGgViQc8nrzUml6ZFp0JjQkgtnJoAXUtNi1CHy3JAznirgtYvs4iI3Lt289xQBxzeFIAx8uaSNT1CmtBvDlmYo48ttRt2M9T70AdWFAAA7VhW+jwW969wpYM+cjPHNAD7/SYL6SJ3LAxnIx9c1ryxLLGyHowIP40AcUvhS1QYWWUD2bFdTYWSWUPlqzMMk5Y5PNGoGlRTAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEr5z1v/AJCU/wDvmgD6MFLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAIDS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJXznrf/ITn/36APosdKWgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACmgYoAdRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAUUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAhr5x1r/kJT/wC+aAPo4UtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAJRQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfOeuDGpz/79AH0WKdQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfOetjGpT/AO/QB9FgU6gAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKSgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASvnPW/wDkJ3H++aAPowUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFACV86a4Mancf79AH0UKdQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAJXzrrv/ACE7j/f/AKUAfRI6UtAC0UAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUlAC0UAFFACV86a7/wAhO4/36APooU6gAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKKAFpKAFooAQ18667/yE7j/AH6APooUtABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AJS0AFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfOuujGqXH+/8A0oA+ihS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACV87a9/wAhS4/3v6CgD6IFLQAUtACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFAB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PawXAAkRXx6jNTLGiIFCgKOMdqAKS6fZq+4QoG65wK0qQBVSW0t5iC8asR0JGaYFhVVRgAAegqq9nbSNuaNCfUgUgLQVQMADHpQqqgwBge1MBHjSQYYAj0IpBFGF27Rt9McUAP2jGMcelCqFGAMCgDlLi1mutWiZk/dQqSD6sa6ygBaieKNyCyg49RQA8qpxwOKUgEc0AGBTFjRc4UDPoKAHgYpgjQHIUA+uKAJKaQD2oAUjNFABgUAAUABANLQAUmKADAoIBoAMUm0DtQAuKWgBMUUAGBS0AFFABRQAUUAFFABRQAUUAFJQAtFABSUAGKMUAGKWgAooAKSgArNhskiuJJslnk7nsB2FAGnRQAUUAFJQAUUAIaXFAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFNYZBHrQBmafp8VhEY0JILFufetWgArLk0+GS7S4Od6DAoA1KKACigBjKGUg9xisSx0e1spGdMl26sxycUAb1FABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAV8669/wAhS4/3/wClAH0QOlOoAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+dde/wCQpcf7/wDSgD6IFOoAKKACigAooAKKACigAooAKKACigAooAKKACigCN3WNSzEADkk1Wa8t1jVzIoVsbTng59KALlFAC0UAFFABRQAUUAFFABRQAUUAFYUmtafHOIjKN+cY96YizealaWQBlcLnp6/lVm2uYrqISRtuU9DQBbopDCigCCaaOFC7sFUdSelEM0cyB0YMp6EdKAJ6KACigAooAKKACigDmr/AF6xsZNjsS3cKM4+t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GjEEgHHIyOlNkhjlGHUMPQjNADVghUABFG3px0qxQAUUAFFABRQAUUAFFABRQAUUAFFACGvnTXP+QnP/v8A9KAPoodKdQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFNZQwIIyDwaAONbwvaByUeSMHqFbArb0/S7XT1IiXk9SeSaQGzRTAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaAErLbTbV7pbgr+8XgHNAGrRQAUUAFFABRQAUUAFJQBn2thbWpYxoFLnLY71o0AFFABRQAUUAFUILG2gkZ0jVWfqQOTQBfooAKKACigAooAbzS0ALRQAUlABS0AFFAETxpJjcoOPUVJQAtFACUUALRQAUUANCgdBinUAFFABRQAUhAPWgApaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAr5z1z/kJz/wC//SgD6KHSnUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlFAC0UAFFABRQAUlAC0UAFFABRQAUlAC0UAJRQAUUAFLQAUUAFFACUtABRQA0EE4zTqACigAooAKKACigAooAazBRknAHc0KwYZByDQA6igCN5ETG5gMnAye9SUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFfOeu/8hO4/wB/+lAH0UvSnUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAxmVFJJwB1JrmG8R6cGxuYj+8FJX86BHQ29xFcxh42DKehFWaBhRQAUUAFFABRQBBLNHCu52Cj1JwKmBBGaAFooAKKAI3dY0LMcADJNcl/wAJHE2WSCZ0B++q8UCOpgmSeJZFzhgCM8HmrFAwooAqG6hEwiLjeRkL3xVugAooAKKACigAooAK5S71pobw2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AFFABRQAUUAFFABTaAHUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABSCgBaKACigAooAKKACigAooAKKACkoAKWgAooAKKACigAooAKKACigAooAKKACigAooAKKAEooAWkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAr5013/AJCdx/v0AfRQ6UtABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVFJLHGAWYKCccnHNAEtNJAGScAUAUoL21uGKxyK5HUA5q/QAVFHLHJnawbBwcHvQBWuL22tyBJIqE+pxVxWDAEHIPIoAi86PzNm4bsZx3xU1AC0UAFFABRQAUUAFUbq9t7RQ0rhAeme9AE0E8VxGHjYMp6EVYoAKKACigCrLcwxOiswBc4Uepq1QAUUAFFABRQAUUAFFACUtABVD7bB9p8jd+827se1AF+igAqld3cNpEZJG2qKAK1jqMN6G2Bhtx95SOv1rTZlQEkgAdzQAxJUkGVYMPUc1NQBWS4hckK6kjrg9Kp/2pY5x58f/AH0KdhXBdTsmYKJkJJwAGFatAXuFFIYVk2mow3ckqJnMTbWz60AatLQBUS6heZogwLoAWXuAat0AFJQBn3OoWtqyiWRULdMmtAEEZFAFa4uYbaMvIwVR3NLBcRXEYeNgynoRQBZooAKKAM63vre5kkRGy0Rwwx0NaNABRQAViSakqaglttOXUtu7cUAbdZ0d9DJdPACd6AE8djQBo0UAFFAEKyoxIBBI6gdqpT38ME0UbZ3SkhcD0oEXnkRCNzAbjgZ7mpaBhRQBCksbkhWBKnBwehqagAooAKKAK1xMsELyN0RSx/Cud02/1C8ZZDCiwvnB3fNjscUAdVUUU0cq5RgwzjIOeRQBNRQAUUAc/pupNeTXCFQvlPtHvXQUAFFABRQBg6zqD6fZtKqhiCBg+5raRtyA+oBoAkooAKKACigAooAKKACigAooAKKACigAooAKKACigAr521//AJClx/vf0FAH0MhyM0+gAooAKKACigAooAKKACigAooAKKACigAooAKKA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GC9i2SDgHII4IPqKW0so7WIxqWYHruOTz9aAMdfD1ishYBsE52bjtz9Ola9pYwWm/yxje24/WkBnT6JaTXBmO5WON21iAceuK0ruyt7uLZIu4fqKYGRHoFojhiZG2nIDOSOPaumoAo3llb3keyVQw7eo+lZMGg2UUgfDOV6b2LAfgaAN2eCOeJo3GVYYI9qIIEt4ljQYVRgD2oAsVz6aJZJc+cqkNndgMQufXFAF28062vVAlXOOh6EfjVK20Syt5RIFJZehZicfTNAF68sLa9QLKu7HIPcfQ1Ws9Is7R96J82MbmJJx9TSAt29lb2zyOi7TIcsfU0osoBcmfb+8K7d3tQAos4BcGbb+8K7d3tRNZwTyRu65aM5U+hpgLdWkF1GUlQMp9ayrbQtPt5A6x5YdCxJx+dIDXubWG6j2SKGU84PtTpLeKWLy2UMmMYPSmBhx+H9NjcMIgSOgJJA/Cm+IIriTTnWEEk4BA6le4pAZI1HTxAIxAc4xsMeP51q6Bay21mQ427nZgv90HoKAM/R4jNqN1clNoJ2LkYJx1P411tzaw3UeyVA65zg0AWVUKABwBwKdTAKKACigAooAKKACigAooAKKACigAr5217/AJClx/vf0FAH0OvQU6gAooAKKACigAooAKKACigAooAKKACigAooAKKAOW1qwnvDbGPH7uUO2T2FdOAPSgB1N2jOcCgBaWgAooAKTAoAWqV5C09tIg4LqQPxFAFPSbN7Oyjicglc8j61sUALRQAUUAFFABRQAUUAFFABRQAUUAFFABWFqmnfb0jG/bskD/XHagDcFLQAUUAFFABRQAUUAFFABRQAUUAFJQAUtACUUALSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABUKQxozFVALHJIHWgCaigAooAKKACigAooAKKACigAooAKKACigAooATFLQAUUAFFABRQAUUAFFABRQAUUAFFAELRRuQSoODkfWpqAEpaACigAooAKKACigAooAKKACigAooAKKACvnfxB/yFbj/AHh/IUAfQy9BTqACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKACloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAaw3Aj1pqIEUAdvWkMkopiCigAooAKKACigAooAKKACigAooAKKACvnnxD/yFZ/8AeH/oIoA+hB0FOoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAr558Rf8AIWn+o/8AQRQB9CDoKdQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFfPXiL/AJC0/wDvD/0EUAfQa9BTqACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBMiloAKK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AtFABRQA0gEEHoa5aHR57XKwXLIhJO0qGAz6UAX7LTFt5Wld2llYYLN6egArcoAKKACigAooAKKACigAooAKKACigAooAKKACigAooApXVpDdxFJF3Kf51i/wBgwEYMsxX+6ZDigDfhgjgjCIoVR0AqxQAUUAFFABRQBnXdjDdhBICdjBhg9xWhQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFfPfiMEatP8AUf8AoIoA+gU+6PpT6ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAr568Rf8AIWn+o/8AQRQB9BJ90fSn0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0maAFooAQkCloAQkCigBaSgAJApNw9aAHUUAFFABRQBEZY1OCwB9zUlAEbyIn3mA+pxUgII45oAWkJAGTxQBCk0Uhwrqfoc1IzqgyxAHvQBF9ohz99fzFWKACqj3VvGcNIoPoSKALIIIyDkVFLPFCMu6qPc4oAWOWOVcowYeoOalJAGTQAxJEkGVII9RzTZJY4l3OwUepOKAIIby2nOEkVj6Ag1doAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+evEX/IWn+o/9BFAH0Ev3R9KfQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFAHNa1dTRRJHEwWSVtoJ7c84rGvNHltbVpYZ5fNQbiWYkHHJ4oESXerzHQxcR8OwAJHY5waryaMosTKLiXzNu/fvOM9elAHS6LPPPYRtKPn5B98HGa3aBnKSaEs8jPLNKxJ4AYqB9AKoaDNPHc3Nq7mRYSNrHrg9qBEMMTazdztJIwhibYqKSOR1JxSw+ZpeqxwB2aGcEqGOdpHuaANrUo7+ZisbiKILkuOWJ9B6VR8LO76aCxLHe3JOe9AGtd6RbXcm9y+ePusQOPpXG3mlQx39vDAzhid75cnCijUD06vP9V14QX8MaOQqsRKMflQM7K1vYLqIyI2VBIyRjp9axX8RaerMNzMFOCyqSo/GgR0FvcRXMYeNgynoRU7DKkZxkdaBnLHw7YMjbwzuersx3Zql4duJfss6uxcQyMqk9SBQIoaPYw6rG9zc/vGZmABPCgegq5pG+z1Ge0yTGAHjyc4B7UAd3XE6pvvNRhtNxEe3fIAeT6D6UDKWs6ZDYQC5tl8t4iCdvQjPOaf4idbjTrdj0eRCfoaBGxDouk5BWNCRg8GunoGIRkEetc6NA07aQ0Qcnqzcn86AMrw2zILmPcWjilKoT2HpUGmW8eqXE9xMN4EhSNTyoA9BQIYYl0vWYVi+WK4BBXsGHcVvalYS3RbdKViC/cXgk+5oAoeFCTpi5/vN/OqesCNtVtxcf6gqQAfu7/ftQBBq1tbJdWv2ZQJt4PycfJ3Jx2r0YUALRQMKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACvn3xJ/yFZvw/kKAPf0+6PpT6ACigAooAKKACigAooAKKACigAooAKKACigAooA4XW+NSsDnjeR/KuuuyBbSZ6bD/ACpAcHpdxDbaAplXcGLAL3YljgCqH9j6kloCW3Rg7vs+T07Dd/SmI7K31VJbASwRM+Pl8tcZB7/lVmyv57lyHt3iAGctjmgB2pajHZRZPLtwijqTVPR7CS3heSXmWY739s9vwoGZfhYjybgdxM2abrI3avYAdcscUgO1m/1T/wC6f5Vx/hP/AJBg/wB9v50wOzdwiljwAMmuM0NWu55rxv4ztjz2QUAdvXAa4q/2nYcDlznj6UAaniOVrfS5CnGcLx6E81q2FrDFZxoqjbsH48UAc14fHlXV7Ev+rSTK+2etd3QByuq6iyn7NB808nHsoPc1p2FhHZ2wiHOclj6k9aBHOxaRqFjI4tpkEbtu2uM4J9MVn6ZFcDW5y8nmskYBOMDJ6CgDfW61kuAbaMLnk7+35Vnn5fEgzxugwPzoA0/EbhdKmz3AH5mpItOhutOgimXcFVTjOOQKQzntV0a3srZp7ctE8fPBOD7Yre+1ahLaQyQxoxdQWDnGOKYjQsXvmDfaERT22HNZWqaiwb7Nb/NO/HHRR6mgDT0+wWxtBEDk8lie5PU1z3hY4t5kPVZmBpDGauDJrFio5ILMfpXZ3H+qf/dP8qYHJ+FP+QYv+8386j1NzqN59hXAUKHkYjJx6D3oAz7qzOgsLmJtyEqsivycexr0ZGDqD6jNAh9FAwooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAKKAFooAKKACigAooAKKACigAooAKKACkoAWigBKBQAtFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV8+eJP+QrP9R/6CKAPf0+6PpUlABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAc7rOnveQr5bbZI2DKfcdqxpX1i8jMDQCIEYaTdnj2FAi/faP5mnxwxHDQlWQn1FVzc600ez7OobGN27j60gNnSrD7Fb7Scsx3MfetqmM80W01gX73DQpIeiZbAUe1dXZS6o8uJ4kRMdVbJzSEZDWV/YXcktsqyRynLITjDeoq3Y2N1Jd/arnAYDaiLyFB9/WgZb1E6nvxAkbIV5LHBzWBo1prFiqxFI/L3ZJzzg9aYHS6vb3NzaNFCQGfAJPYd60LS3W2t0jXoigflQBcrlda06e6MMkJAkhbcAehFAFwW095ZPHdBVZ8jC84HasCG3162i8lDE6gYVyTkD3FAHQ6VposISC253O529WNa8gco23hsHGfWgDzyy0rWrSR3VoWaQ5ZmyTXW2C6kC32hoyP4dgP9aWvYRmMmvbmUNCVJOGOcgfStPTNNWxjb5i7udzuepNAG3XNarpbXbxyxNsmiPyt2x6GmMzzpuoXrp9rdBGhDbEz8xHrW1f296+xreUIVzlWGVagRiSaZqV9tW6lQRgglIx97HqTXZoiooUDAAwKBg4JU4ODjg1wFtomqW0julwm6Q5YlMmgDprGDUI3YzzLIuOAq45rLn0m6iunmtZQhk++rDKk+tAFuw0uSKdp55PMlIxnGAo9AKk1C01Cdz5NwI0K4IK5OfrQIydN0bULHaouQYw2Suzr681fv9Iea4W4hl8qUDGcZBHoaAKjaPeXTr9quA6Kc7EXAJHrWzqFjJdeVslMYjcMcdwO1IZs0tMAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBaKAEpaACigAooAKKACigAooAKSgBaKACigApKAFpKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAr588RkHVZvqP5CgD39Puj6U+gBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+e/EX/IVn+o/9BFAHv8f3R9KloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigApKAFooAKKAEpaACigBKKAFooAKKACigAooAKKACkoAKKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+fPEg/wCJrP8AUf8AoIoA9/T7o+lSUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFACUtABRQAUUAJRQAtFABSUALRQAUUAFFABRQAUUAFFABRQAlFAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV8/eJD/xNZvw/kKAPfU+6PpUlABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAlLQAUUAFFABSUALRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV89+I/+QtP9R/6CKAPoFPuj6U+gAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEBBpaACigAooAhM0YbbuG70zzU1ABRQAUUAFFABRQAUUAFFACUtADGYKpJOAOpNNV1YAg5B6GgCSigBaKACmk0ALS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV8+eJBjVp/qv/AKCKAPf0+4PoKkoAKKACigAooAKKACigAooAKKACigAooAKKACigDB1DUvsc8ClcrK20tnoe1bMsgjjZj0UE/lQBg6Nqw1OJ227SrYx1qa91E29zbwqu5pmPfoB1NAjdooGFFABRQBjwzXjXkiPGBEANjeprXoAx9XmkhsJnQ7WVCQR2pdJmeawgdzlmQEk9zQBsVFISEYj0NAHL+HLma4sd0rFm3sMn0rraAMm1a9NxMJQojBHlkdce9auaADNAINAC0hIFAACDS0AISB1poZT0IoAdkUgII4OaAG71BxkZ9KkoAj8xM43DP1pJHCIWPYZoAx9K1NNQt/MxsOSMZz0rdoAieRE+8wH1NPDAjOePWgBQQRkc0gYHoc4oAY8scf3mA+pxTwQRkHNAEJuIQ20uufTIzU+aAIFuIXbaHUn0BGanJAHNAECXEMhwrqx9AadJLHGMswUe5xQA5JEkGVII9RzSSBijAHBIOD6UAUNPSaO2USyiVhnLDpVxp4UUEuoB6EmgCVWVhkHI9qdQBxGizBbi+LtgCbueldlHNHKMowYexzQBkpFONRZzPlCmBF6H1rSmuYIfvuq59TigCaORJFypDD1HNQ3MyQxMzMFwDjJxzQB594ft7O6iE0uGnZy2SfmGDxivRkljfO1gcHBwehoEJ5sfmbNw3YzjPOK5ibW449SSDcmwqSzZ6EdqAOjNzCIw+9dp/izxU5ZQuc8dc0DI0mjePerAr6g8VVfULRApaVBv+7yOfpQItSzxQpudgq+pNVre/tbn/VyKx9AaANCq808UKFnYKB3JxQMq2+o2lySI5FYjsDzU73MUbqrMAzZIBPXFMRTj1Sxkl8tZkL+ma1GdUUliAB1JpActe6pZXNncJHKjHy24B9q0NGP/ABLbc/8ATNf5UALJrFhG5VplBHB56GtZZFZdwIxjOe2KAMRtd01X2mdc9OvH51pzXlvDD5juAhx83bmgCyHBGe1VIbuCdWZHBCkgnsCOtAGSdf0xW2+cv17fnW+sqNHvBBXGcj0oGRW11DdRh42DKe49qSK7gmkdEYFozhh6GgClLq1jCJN0oHlna319Kl/tG1+zCcuBG3QnigRDaataXUmxGO7GQGUgkD61tUDCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+ffEgxq0/uV/9BFAHvyfdH0FSUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQBy3iK3aXT3K/ejIcf8BqnqV6ZdFVl+9OEUfVuDQIp6dbjTNVMK8JNEGH+8vBq5CTd67I38NvGFH+83NIBL68luNRFoknlKq7pGHX6Cqy3EunajDEZjLFNkDcclW+tUI76uQsbqZNVuLeRiQQJI8/3e4pDMY6ncrrOSx+z7/JHpuxn+dbl9NLJqltCjlQA0j47gdBQBXtbmZtdnjLkoqAhewPFUFdb3UJ455njKNiNA23j196ANrUIDBo0yF2fEbfM3WsHT9KnutNjZrh1+QbAhwAO2fWgDb8OXst1ZnzDlo3KE+uK6iX/Vt9DQM8u8P6c93aNvldYw7AKhxz6k9a3tBlmjuLm1dzIIWG1m64PagQ7TJpG1e+UsSqlcAngcdqytOgnvri7R5nESzNwCQT7Z7CgCK1iul1GWxE7eUBvyeWxxwD+NW1ibS9YhjjdjHODlWOcEDrzQB6LXN6nZW87Bpp2RAPuhtoJ9fWgDk7SWK01iKO3mMkUqnK7twB616hQBwV7m71hbeVisSx7gASNxz3rOXTrePW0iRm2bN5UMcKwPFACXMU13r0kIlZEMYLYPUDHA9K2r8LomlyeRnJbjcc4LUAYZTR/suWuP323Pmbju3V0mi3L3+lDexDYKFh1470CVuhzl5Z6HHG4E371QTu3ktmtmyQaloqeflsAnIJBJXIHSgZk+FtNtpLdZyDvVzg5OOPavTaBnmUptl1OYXwO1iPKZvuY/kK6treCDTJVhPyFGI5z1HY0CIfDpJ0uHPof51maC2J77PaY0AU9Ks4tWea4uBvy5VFJOAB7VoahGujaZMYWb5iAoJztzxxQA+18O2TWy+YpaRgCz5O7PrmqOvzmzt7a3DsFdgrNk7to68+tAGVqLaSLX/RwUmTBQqpBz7nFbuovdXWjwuqsSdjSKvBK9xS0AtaadHuJEaFVWRB0xtYdjkVl6gEh1UyXSF4CgCHGVU98j+tAHXafHZrGWt9uxzn5emavXH+pf/dP8qYzitA/5AR+kn8zVbQdLt7rT0eYeYSCFDdFAPQUCL/h0GF7qAElYpcLnsD2rtqBnmulWEN1e3hk+ZRKfkP3c+pFWbSFbLXmii+WOSLcV7ZzQIlT/AJGZ/wDrh/UVVn8u11OaS7iLxuB5bkblUDqPagDtLBbRYQbcLsY5G3pzS39tDcW7LIoYAEjPqBQM4/wtZ2xsUm8tfMyw3Y564qfw1968/wCvhqAGRf8AIzSf9cP6iql1p9p/bsKeUu10ZmGOCfWgC74mhjh0goihVDLgDp1rprj/AI8H/wCuZ/lQBzukc6AP+ub/ANapeHdKtpbFJZV8xm6FudoB4AoEQajcq2tLHIjSRxJuCKC3zHuRUV7IZLu3lt7eVHVwGOzAKnqDSA9QrzbUbgPrapIjSJFHuCKM/N649qYDL93mureSC3kR0cbiVwNp65qfXIhNqdipJAbcDjj04pAP8R2dvbWSSRoEaN12kDHepvEcjNbW0ecCaVVb6GgC/q2mWh06QCNV8tCVIGCMCs6WeS38No6HB8pQD6Z4oGQWk0a2CRfY5WBTk7RznqetaOhLNDYOlwpVVJADf3MUCKcl5Zy27rDaO8e0jcqgLx6Zo0iD7boHlMc5DAe2CcU9AFtdT8vRGZj88QMRHfcOBVC8hey0OGLJVpXUOR1+Y5NAHdJp9qtsIti7duMYrkvD7sLK5iJysTuq/TFIZd8KH/iVr/vN/OotF/5Cd/8A76/1pgU9LtIZtZvXcbijDAPTmtrW9OmnihaEDMMgcJ0Bx2pATWOqRTz+VJEYZgOFYdfXBrpqYBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeAeJhjVZffaf0FAHvafdH0FSUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQBFIgkRlPRgR+deTacZJbqCycHFvI7nPcL92kI6jxFm3+z3Q6wvg/7rdaseHkL27zsMNO7P+HQfpQBzt/b28GtGS5XdFMuAT0DDHX8q27eLQjcxiLYZM5G3npQB2tcF4gdrK4guwMhco2PQ9KYyr9kc6Dvx+8/1/vuzuq14ff7dPPdkYztjXPoBz+tIQWf/Ix3P/XMf0qe+uNEvA/msodMjJ4YEenegChAbhvD03mZPyuEJ6le2a6TRP8AkFQf9cxQMxPCgIt58jH75uv0FdvL/q2+hoA43wqCLFs/89GpmlBhrN9wcHbQIXS1YazfHBwdmPyp+gAi4veDzMe1MZBAr/8ACSzHBx5PXt2qXVEY61YkAkDdmgDuK841Mxx6wj3SkwBPkyMqG96AIWeKfW7RoYisY3DdtwDwa9NoA8z1BDJrDq0JnHljaEOCn+Ga0LC6tLGYRtbvA0hwHf5tx9N2TSEEKSf8JJK207fJxnHHatvXbF72weNfvcMPqKBnP22tWaxKs0DrIBgr5ZPI9OK2Lky3WkyGFDGzqcKRg/5NMRzVndQJpogjt2M2zay7CPm7kk8V0OgQyLpCIwKthuCMHkmkBi+H7o2S/ZJIpA/mHkKcYPfNej0xnIXOr226SKaCTgkAFCwYeoxUOi2Uy6fMjgoJGcoh6qp6CgRm6XqE9lbi1e2kaRCQNo+U89c1e8PwXCPd+cu1mkz7c+lAFCzkm0SaaJ4XeN3LoyDPXtita5judX06ZWiMWSDGG68c8+nNICrba1cxQLE9rMZlAHA+Un1zUur2V1dWsEwQefCQ+0d/UUxirrhIAFnMX7jbxn61uXN7LbxRv5DOD94LyV49O9LQDmXVr/UbeSGF4xESXdhtyP7uO9blzqflSvHJbyFezKu4N+VAipoVrLEZ5ChiSV9yIeoHr+NdTMC0TAd1NMZyGiW08OjtG6FW+fg9ec1f8PQSQabGjqVYbsg9eppCINJgmivbwspCvICpPeuspjOQ0a3miurwupAaXKk9xSPbzHX1l2HYIdu7tmkAxbaf/hIGl2HZ5ON3bORVufU5I3dHtZGAJClRuDCgBNBs5baGQuuzzJC4T+6D2ro5gTE4HUqf5UwOb8O20ttp6pIpVsscH61jw/bdKu5wtu0sczl1K9ifWkBLY2t+dZaeZAA0WOOg5HFTatDdQ6hDdRRmUKpVlHXmmIu6jbTanpbLsMbthgreoPQ1li41a5tjALcxNtKs7EY6dh3zQM0tNtJ4tH8p1w+xht+uat6FbzW+nxpINrLnI/GkBn6lZXUV8l3bqHYLsdM4yPapoNQ1GeVQLQxrn5mcjp7AUCOsrjtSsbpL1Lu3AZgux0PGV9qYx8V9qc8qj7J5a5G5mYdO+AKj1GxuJtRtJEXKxltx9M0CLPiGzmu7ExxjLFlOPpRq+myXlmipgSRkMpPqKQzJmOsX1s8JhWLKkMxOc/Qe9bEWnNJpC20nB8sKfYigRkW0usWcSwm2EuwbVcMACO2c1um3ubuwdJgEd1IwvbPSgDCtI9UjtPs3kqpVdokJ+XHrjrW1oNlLZ2CRyABgWzj3NAHPXGi3T6kSD/o7yLIwz3H/ANeuq1XTxfWjRZ2ngqfQjpQMwFl14R+V5MecbfM3cfXFbmmaWlla+WTuZsl29SetAjmbKz1fTt8ESI8ZYlHY/dz6jvWno+l3NndXDyNuEhBB7nrnj8aALWnWE1vfXUjY2ysCuDzxV6/a/Qo1uqOBncrHGfTBoAx4bO8ur+O4uFWMRAhVU5OT6mtVI78ai7M4MG3Cr3zQBuUUxhRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABXgXif/AJCsv0X+VAHvMf3F+gqSgAooAKKACigAooAKKACigAooAKKACigAooAKKACq6wRLIXCKGPVsc/nQA+SNJFKsAwPY05EVFCqAAOgFADJYY5Vw6hh6EZqvDZW0BzHGqn2GKQF6opI0kXDKGHoaYDtq7cYGOmKRI0jGFAA9BQACNA27Aye/eoGtbd23GNSfUgZoAslVIxgY9KAoUYAwBQAKqr0AH0p1ADVVVHAA+lAUA5xyaAAAA5x1oAA6DrQAYGc45pcCgBaYyqw5AP1oAUADtTqAOMezv7S+lngCSrLjKscEY9DRJZ3+oTRGdUijjcPtU7iSOnPFIR2WKWmMbtXPQU6gBMCloATApaAEwDS0AJiloASloASloATApaACigAooAKKACigAooAKSgBaKACigAooAKKACigAooAKKACkoAKWgBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArwLxP8A8hWX6L/KgD3iP7i/QVLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeBeJ/+QrL9F/lQB7xF9xfoKloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArwHxP/AMhWX6L/ACoA96j+4v0FSUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4F4n/5Csv0X+VAHvMZyg+gqSgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvAvE/wDyFZfov8qAPd4v9Wv0FTUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSc5oAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvAfE4xqsv0X+VAHvEX+rX6CpqACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArwPxR/yFZPov8qAPdof9Uv0FTUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4H4o/5Csn0X+VAHusP+qX6Cp6ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8B8T/8AIVl+i/yoA94h/wBUv0FTUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFfPviQ51Wb8P5CgD3uH/AFS/7oqegAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAr588RjGqzfUfyFAHvcH+pT/dH8qsUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV8++JBjVZvqP5CgD3uD/VJ/uj+VT0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlfP/iUY1WX8P5UAe9Qf6pP90fyqegAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK+f/Eo/4mkv4fyoA93tzmFP90fyqzQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFfP/iUY1SX8P5UAe724xCn+6P5VZoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArwLxOMapJ9F/lQB7rB/qk/3R/KrFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4J4o/wCQo/8AurQB7nAMRJ/uj+VWKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigBK8F8Uf8AIUf6L/KgD3ODPlJ/uj+VWKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBK8G8U/8hR/91aAPc4P9Un+6P5VPQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACV4P4q/wCQm3+6tAHuUJzEn+6P5VPQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABTcndjHbrQA6igAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEzS0AFFABRQAUUAFFABRQAUUAFFACV4P4q/wCQo3+6tAHuFt/qI/8AdH8qs0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4T4rH/EzP8AuLQB7Zbf6iP/AHR/KrVABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQBnahdG0tJJQNxRc4rmINU1ieJZEtEKsMg7+1AjsYGkaJS67WI5A5wanoGFFABXA6l4iksr/AMrywyKAWPcA0Ad2rBlBHQjNcbruuSWDKkSB2xubPQDoKAOrtZjNBG5GNyg/mKtUAV52IicjqFP8q5zw9cyz6cJJGLNubk+xoA1NO1BL+EyKpUBiuD7Uim++3sCF8jbwf4t1Ajk9T167tNQaNUDRIFZ+OcHrXoCOHQMOhGRQM4nXtauLOQJAoLKu9yRkAZwK660laa3jc9WUE/iKBFrcM4zTqBkZcDuM+lch4cllltpizFiJWAJOaBG3pq3gib7Qys2442+natQuoOCQPxoAfRQM4n7ZfalO6WzCKKM7TIRkk+wrQtYtVt51WSRZojnLY2sv+NIR0jOqDLEAe9Ijo4ypBHtTGOyAcZqNZY2YqGBI6gHmgBDNEG2lhu9M81KSAMk4FAELzwoRudRnpk1PkEUANV1cZBBHtTRIhJG4ZHUZ6UAc2mso2pPAWQIqBt2e/pXT7lxnPHXNMCnFe2srlUlRmHYEE1aeRIxliAOnNIRELmEy+XvXf1255/KrFAzz20W71d5Xa4eJEcoqxnB47muts4prWFhNN5mDkMeML70CNRWVgCDkHoRTBIjMVDAkdRnkUDKZ1CzEmwzJu9NwzWPrerf2fAChUuWA2n0PemI3oLmKcfI6tjGcHOKjnvrW3OJJFQn1OKALaSJIoZSCD3FVmvLZFZjIoCnDHPQ0gFiu7eWPekiso7g8Uy3vba5z5Uivjrg5pgSz3MNum6Rwo9Sabb3cFyuY3VwOuDSAuUUDCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8K8Wf8hM/7i/1oA9qtv9Sn+6P5VaoAWigAooAKKACigAooAKKACigAooAKKACigAooAwNc/wCQZcf7hrmNN16KCxhQwTNtQDKpkHHoaBG5q2qPb2SPGp8yUhUB7E+tZjaPqCRGUXkhlAzg/dJ9MUAXbXUZLrR5JvuyKjA4/vLWZpltqN9HDPJcMgGMKvcDufrQB6HXnMtstzrVzGw4eACkM29AuWazMch+eBijfh0/SuUnU3FjfXbc+Z8qf7qnFAjob2R49ADIxUiNMEde1b1u7HT1Ykk+UDnvnFMZz+gTSTaQWdixO/knmqOh2v2rRAhdk+ZjlTg9aAK3hrTQ8Ql82QbZG+UN8px6itcSyf8ACRFNx2+TnGeM59KQiq1utxrV1G3R4APzrS0C5ItHikPzW7FDn0HQ/lTGczIpn069u26zcL/uKcCupkvDZ6Kso5KxLj6kACgRzcUOly22+W6zOwyX34Kn2Fadhqsp0WSVm3PEGAb1x0NPXqLToMsNEW4tknkkk89xu3hjxnnp0qXwsCLSUE5IlbmkMm8NMzQT5JP75xzVK60/RkLCacmQ55Z+RQGhf8MXDzWjhmLhJCqk9SvaukvmZLSUr1CMR+VAzn/C4H9lxnuSxP1ya6+gDzq1gGs3s7zEmKJ9iJnAyOpNNurddHv7d4SVjlbY6ZyMnvQIfravJq1miuU3BgSOuKp65p0enCGa2Jjk3hSc5zn1oAuato1vHp7zZbzkG/zM/MTV2+lafw8XJ5aJSf0oAbZaHaz2StNmV3QfMx5GR29MVJ4dnka0licljC7ICfQUAL4X/wCPN/8Arq/86r2Kg67eA8gqv8qBmVHpVk2uSRGMbBGG29s1p+IbhIfs1vkrG7fPtznaO3HNIRkanLpr2w+zxukseChWNlP54rR8RSO+jRuchiUJ7HNMDorDSLW32Sbd0mMlz1JPWuioGcNPpF3aTPNZyAbzlo2+6TV7T9QGqQywypsdflkU+9AitoVx5FtLDIebZiDn+71H6VW0uCaezuZx8slyWKn0HRaAKunPp6RLbXUISQcHeOGPqG96k8W20P2ISbRvDKN3fHpQB1cFtb2cDNHGq/Lk4GM4FcHo95atE8s0UkkkjHLbCwxngA0gNbQC6XVyiI6QEhkDKRgnrjNVNGsobi8vGkG4LMcKegJ749aYDYrCAa5LCAREUEhQfdLfSrF7BFZ61aNEAnmblYDgECkMvavFMl7DceUZoowQyjkgnuB3rc0+4srgM8GMnhsDByPUUxGzRQMKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKAErwrxX/yEz/uLQB7Zbf6iP8A3R/KrVABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAYOuf8gy4/wCuZpdEx/Zlv/1zX+VAGN4mik8iKZRnyJA5HtV6TXtPFqZBKpO3hQfmzjpigRkafby2+hTFxtZ1d8Htmt/Qf+QZB/uCgZ0FcLCceI5feEUAYutm4sb2TyRxdoF+jZxmuh1W3W20J4x0VAKQhLmJp/D+1Rk+SpA+gzVW2121OnqqktLs2iMfeyBimAvhsn+x29i9TeFw39lY77n/AJ0hmd4d1CC3V7eQ7ZfNbCkdc1c5/wCElz28j+tMRJBn/hIpv+uQrF1yO5tr4+SOLtAh9mzjP5UgOj1e3WDRHjUcKgApJrR7zQ1jH3jEpA9wM0xmNZappUduqTxCORRhlMfcenFb8iRX+lyrChQOGCgrt5HfFArmPp2uxw2aROknnIu3YFOSRwParPhhZBazb1KnzWJBoAZoKS/ZboAFWMr7c+/SsjR7i1toXSaJjcbm3DYSWJPHOKQGz4XR1huNyFCZicEYrt2UMpB6EYpgeeWdxLojvDKjNCWLI6gnAPY1v22sfbJ1WGJynO52BUD6ZoAwYpH0W9n3oxhmberKM4PfNPdpNZvoCsbLBCd5ZxjJ7YzQBZ1SKVtZsmCkqN2SBwKl8TQyS20QRSxEqngZpDNXWI2fTJlUEkp0HWsK4Rk8N4YEEQgEGmBWs9ZntrKNJLeQvsGwqMq3HHTp71t6DYy21oxlGJJWLsPTNAjn9PubjSmlge3kcF2ZCgyDn3q7pEF4NVuJZk271BGOn0zSAivWnsdY+0eU8kbxhfkGSDWjrFrPcRwXESZkiIYIepB6igCNdckb5Vspi/oQAPzp3iS3nudOCohZtynaKAOujBCL9BTn3bTjrjimM48a3dR/JJZy7/8AYAKn8al0aynSae5mXY8xHy+gHSgRz2vwTxXo8nj7Woib6g9fyrvdrWlniNNxjQBVHGcdqAOP1GeXU7fyVtJA7Y5cABffNaeuafNcaX5SfMybTj1xSA0NPupbuNlkgeLCgHdjn1xXN2b3mj74TbtLFuJjZOeD2IoGdVYXVzc7jJAYlGNu48msnRbSeC4u2ddoeXK+49aAHR2k41x5tp8sxBQ3vmm6naTy6lZyKuVQncfTNAjRvL65tpgBbtLGR95CMg/Q1n6TaTC6nuXj8rzcAJ3wO5x3NMDrqKBiUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUlABXhfiwf8TM/wC4v9aAPa7X/UR/7o/lVqgAooAKKACigAooAKKACigAooAKKACigAooAKKAI5I0kQqwDKRgg9DQiLGoVQAAMACgBxAYYIyDWUmlWCSbxAgbrnFIDUZVZSCMg8EUiIsahVAAHQCmBJUIijEhfaNxGM45xQArxRuQWUEqcjI6U50V1IYAg9jQAoUAYAwBxiqyWtujlljUMepA5oAnVEUYAAHoKVUVBgAAe1AEX2eHfv2Lu9cc1LsXdnAz60AG1c5wM+tKVBIyOlAAQCMHmlAxQBE0UbHJUE/SpelADdi5zgZ9adigAAApu1c5wM+tADqWgBCAetIAB04oAUgGgDFAC0UAFZGq2sl1ZSxJjcwwM0AWLKBobaNGxlVAP4VfoASloASloASloAKKACigDnptPkm1KOdmGyJTtXvuPeuhoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACvCfFn/ITP+4v9aAPa7X/AI94/wDdH8qt0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFeF+LP8AkJf9s1/maAParY5gj/3R/KrVABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAleGeLRjU/qi/1oA9qtv8AUJ/uj+VWqACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8M8Wn/iZf9s1/maAPZrP/j2j/wB0fyq7QAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXhni3/kJ/9s1/rQB7RaDFvH/uj+VW6ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8N8Xf8hL/tmv8zQB7Pa/8e8f+6P5VboAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArw7xd/yEh/1zX+ZoA9ks/8Aj2j/ANxf5VdoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArw7xd/yEh/1zX+ZoA9jsjm2i/3F/lV6gAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvD/F4/4mQ/65r/M0Aew2P/HrF/uL/Kr9ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeH+L/APkJD/rmv8zQB7FZf8e0X+4v8qvUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4d4v/AOQkP+ua/wAzQB7FYnNrEf8AYX+VX6ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8O8Xf8AISH/AFzX+ZoA9b0uNo7GFWOSEUH8q1qACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8O8X/8hIf9c1/maAPY7L/j2i/3B/Kr1ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABUIljLlAwLDqM8j8KAJqKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8P8AF/8AyEh/1zX+ZoA9gsP+PSL/AHF/lWhQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFAGNq141lZSSgZKjj6muatdNvbm2Sb7ZIJHAYYxsGe2KBHZ+YIIQZXAwBuY8DNWN67d2eMZz7UDK6yx3EJMbggggMpziqOlqEs1HnedjOXz15/pQIa2s6cr7TOmfTNW7q8ht4DIzqBjgk8E9qYHOaJrsd3ComkQTMxAUcH24rOtpY4dfvHdgqhBkk8dBQB2FpqVneEiKRWI6gdalur62tFBlcID0z3pAV7TVLO8YiKQMR271r0DOA1lGn1a1i3uqurZ2kjp9Krahby6O0UsM8hVpFVkc7gQfrQI9HByM0tAwooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvDvF//ACEh/wBc1/maAPX9O/484f8Armv8q0qACigAooAKKACigAooAKKACigAooAKKACigAooArXFvHcwtG4yrDBrhGg1PRULRMJ4F52N95R7GgCfXLuO80FpV6PsOPT5hXRwDGmKP+mI/wDQaAMLwz/yBx9X/maPDojbR/n+6S+7ntk0CM+afSmtJEhtmdACNyJkZ+tXNDRbnRFEgD4DYB56E4o0GReFLaBrBZCilw7fNjmqcVrDc+I7gONwVQcHpnApAWr2GO2120MYCeYGDYGM8VvancWMEsZlTzJTkIoG5vwFMDj5592sWbrA0BYkHcANw/CvVKBHnetpLJrFmsb7GKthsZx+FRSRSw6lCL1/ORj+6boof3Hr6UAek0UDCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigArxDxf/yEV/65j+ZoA9d07/jzh/65r/KtKgAooAKKACigAooAKKACigAooAKKACigAooAKKAM2+e6SHMChnBHynjI71zFxfancxNElmyMw2lmI2jNAi6+jn+x/soPzBev+1nP86zLeXWHtBbm38tgu0yMRjGMcAd6QGpolpNaaZ5ci4YbuPqTWVotsbrQDGDgv5gz+JpjFtP7USy+zC2CsqlN5I2/X1rY0OymtdOEUgw2W/U0hGPosOpWDfZ2hBj3k+ZnjB9q0bbT7hNanuCB5boAD3zx/hQMkv7CebU7SZQNkW7cc+tQ6nZ3YvorqBRIUUqyE44PpTAqS2WpXd9bTuqIsbH5M5IHr713dAHLXunzzarazrjZEG3c881Z1mwe9tdqYEisGQn1FAGxB5nlJ5gAfaN2Ome9WKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvEPF4/4mIPrGP5mgD13Tv+PKH/AK5r/KtKgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAydRtri5i2xTeUc8nGcipbCzSytkiXkKOp7nuaANGigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASvEPF5/4mK/8AXMfzNAHrWl/8eEH/AFzX+Va1ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4d4v/wCQkP8Armv8zQB61pX/AB4Qf9c1/lWvQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeG+Lv+QkP+ua/wAzQB63pTbtPgPrGv8AKtegAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBK8P8AF4/4mQ/65r/M0AesaRj+zrf/AK5r/KtmgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvEPF+f7RX/rmP5mgD1fSQBp9vjp5a/wAq2KACigAooAKKACigAooAKKACigAooAKKACigAooAgmcxxO391SfyFcRZ6lrV1biZIYipzgZIJxQI6LS9TW/jY7Sjo211PY1uUDCkzQBmag12sQ+zhS+4fe6Y71pDOBnrQAuRS0ANyM9ax77U47OWFGGfNbaD6UAbIINZmpXD21lLKv3kUkZ9aAOd0HV7i7LRzgB9odcDGVNddPMsMTO3RQSfwoA4bRtYvbq+aOYBVaPzFGOccYrvmZVGSQPrQAKysOCD9KXcPWgBiSI/3WBx6HNPLBRknAoAgFzAeki/mKnLADJOBQBFHPFJ911b6HNSF1UjJAz0oAYs0bsVVgSOoB5FTUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXifjD/j/T/rmP5mgD1HRv8AkG2//XJf5Vt0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQBUu/wDj3k/3G/lXn+h63Z22nIjFt65+UKTnmgRtaFBNm4uJFKee+5VPUAdM1zulW13qiy+bcOsccjABTyT7n0FAG7qUlw1xBZRSFd65d/4to9PrVLUbB9Lh+0QSyZQgsrMWDDv1oAva7dMdIEsbFdxQgjryal1rUHtbKIK21pSq7j2B6mgDnbz+zoLYvDdnz0G4HzCSx9CM962r7VZTpULodsk5VQfQt1oEWV8PRKqkSyiQYJfcTk/TpXP+ItPiN5bMS2ZZArcn26elAz0Czs4rSPYmcZz8xJP61Q1z/kGXH/XM0DOO5tbSwux/Aqo/+63+FdDrUn2gQ2y9Z2Bb/cHJ/wAKBGdCgTxGQOgt8fqK19Ug00uHun4xgIWOP++R1oA5vT5baLWglqxETxksvIG4emammhNz4geMswQwgsAcZHpQAXVrFpmp2rQDYJSUdR0NaOuupntY5DiF3O/sCccA+1AGPqNhpzX1qkMaElvnVem3HU4q9rVzH9ugt5Cwi2lmCgnd6DjtQBl38tmjxSWkbpKrj7qMoZe4PFafiRXkazCkqWlxkdRmkI6qz0y1s+Y0wxGC3c/WtamUFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXivjE/6en/AFzH8zQB6fo3/INt/wDrkv8AKtugAooAKKACigAooAKKACigAooAKKACigAooAKKAKl1n7PJ/uN/Kue8NxBdMjyuD83b3oA6o9K4jwspENxkEZnbr9BQAauslrfwXYUsigo4AyQD3qtqmpx6lB9ntsyNIQCccKM85oAteIIRFo4Qfw7B+RqTW7N7iyidF3NCyvt9QOopAQJrOl7B+5O/uvlnOfyq1rdo9zYRvEmGiZZFTp07UaCBPEMckahIpGlOBs2kYPuelQeI0lAtpgpYRSBmAGTijQDqLO8jvI96BgM4+YEfzqprKM+nTqoJJQgAUxlK1s/P0aOFwQTEAQexxWL4dtrppXluAcxKIkyOw6mkI0EgkHiBpNp2mHGccZ471QvQ1trInljaSIx7VKgttP0FAD0E9xrEMwhZIwjAEjBP1HarSW8w8QtJtOzycbu2cigZLrFvNJeWTIpYJISxHYVV1sO97bKiLKcN+7bpj+9QBEssmlAyNZRon8TRNkgfTAq5qlvOLmG8hTzCgwy9yp9KAJ49XlmdVS0lySMlgFAH1pmtW0081oUUkLKCcdhQI66imMKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8W8Zf8AH9H/ANc/6mgD07Rv+Qbb/wDXNf5Vt0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlIAAMAYoAdTQAOgxQApGaYsaL0UD6CgDktYtr++cQKiiHcpZyeeOoxXXKu1QPQYpAGxc5wM/Sn0wGhVB6CloAAMUtABRQAUUAFFABXNX+nTyXSXEDhZFXbhhkEUAVprDUb1dk8iLHxuEYOT7ZNdYAAAB2oAdRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeLeMf8Aj+j/AOuf9TQB6do3/INt/wDrkv8AKtugAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8X8Zf8fsf/XP+poA9O0b/AJBtt/1yX+VbVABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeK+MT/pyf9cx/M0Aen6N/wAg23/65L/KtugAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAErxLxeP+Jgv/AFzH8zQB6no3/INt/wDrkv8AKtugAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApM0ALRQAV4t4xx9tj/wBz+tAHp+jf8g23/wCuS/yraoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKYR3HagB1LQAV4t4x/4/Y/+uf8AU0AenaN/yDbf/rkv8q26ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAENLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAV4x4y/4/Iv8Arn/WgD03Rv8AkG23/XJf5VtUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXjPjL/j8i/wCuf9aAPTtJG3T7cekS/wAq2KACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8Z8ZH/TIv8Arn/WgD0vRjnTbf8A65L/ACrboAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBK8X8Y/wDH7H/1z/rSA9M0X/kHW/8A1zX+VbtMAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvGvGQ/0uL/AHD/ADoA9J0X/kG2/wD1zX+VblABRQAUUAFFABRQAUUAFFABRQAUUAFFACc5paACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBK8b8Zk/a4v9w/zoA9J0X/AJBtv/1zX+VblABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJXjXjL/j7i/3P60Ael6L/AMg23/65r/KtugAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvHfGY/0mH/cP86APRdF/5Btv/wBc1/lW7QAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeP+NP8Aj4g/3W/nQB6Hon/IMt/+ua/yrdoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArx/xp/x8Qf7h/nQB6BoR/4lkH+4K6CgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvIvGo/fW/+638xQB32h/8AIMt/9wVv0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXkfjX/W2/wDut/MUAd7of/IMt/8AcFb9ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXkfjX/W2/wDut/MUAd5oX/IMt/8AcFdBQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV5F41P763H+y38xQB3uhf8gy3/3BXQUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4/40/4+IP9xv50Ad/oP/ILg/3BXQ0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeP+NP+PiD/db+dAHf6D/yC4P9wV0NABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV4/40/wCPiD/db+dAHe6B/wAguD/dro6ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8l8ZeX58G7P3W6fUUAdvoH/ILg/wB2ujoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAryLxqP31uf9lv5igDvNB/5BcH+5XQ0AFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV5F41P763/wB1v5igDvNBH/Ert/8AcroaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8l8a58y3+j/zFAHdaCP8AiV2/+4K6CgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACvJPGv+tt/wDdf+YoA7jQDnS4P92uioAWkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArybxoQJLfIz8r/zFAHbeHx/xK4P92ujoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAryPxr/rbf/db+YoA7jw//AMguD/d/rXSUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAV5H40P723/AN1v5igDt/DxzpcH+7/WuloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACvIfGn+ug/3W/mKAO38OnOlQ/Q/zrp6ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8j8aj97b/AO638xQB2nhz/kFw/Q/zrqKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK8n8a53230f+lAHYeHP+QVD9D/M11FADSQOtOoAKKACigAooAKKACigApKACloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBaKACigAooAKKACigAooAKKACigArzLxfF5jW/GcB/wClIDpfDn/IKh+h/ma6imAmKWgAooAKTNAC0UAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFec+KiQ0H0f8ApSYG74bP/Eqh+h/ma6imAtFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXnvih1VoMjs39KQGv4a/5BUP4/wAzXV0wCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgArzHxcTvt/o/wDSpYHR+Gv+QVF+P8zXVVQC0UAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAleZeLoy7wewf8ApSA6Lwyc6VF/wL+ZrrKYBSUALSYGc0ALRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAV554ocK0H0b+lIDW8M/8AIKi/4F/6Ea62mAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFeeeKAhaDd6N/SkBq+GP+QVF/wL/0I111MAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK888UjLQfR/6UgNXwwR/ZUX/Av/QjXXUwCigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArgPE3WH6N/SgDS8M4/suL/AIF/M11tABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFcV4hiDmHJxjd/SgCbwt/yCovq3/oRrr6ACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKaWCjJOBQA6igAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK4HxN1h/4H/SkBoeF/8AkFRfVv8A0I11tMBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAprKGGCMigB1FABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXB+JTzD/AMC/pSAueFv+QVH9W/8AQjXYUwCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigBK4LxMMtDz2b+lIC/4Xx/ZUX1b/0I119MAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK4LxKMtD9G/pQBd8L/APIKi+rf+hGuvoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKAErgvEpw0P0b+lAF7wt/yCovq3/oRrsKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArhvEagtDxnhv6UgLXhcY0qL6t/wChGuvpgFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVwniRiGhx6N/SgC34W/wCQVF9W/wDQjXYUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABXD+IusPH97+lAFnwv/wAgmL6t/wChGuvoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuM8QAkw/8C/pQA/wt/wAgmP6t/wChGuwoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArj9eIBiz/tf0oAPC3/ACCY/q3/AKEa7CgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK47Xyv7rP8Atf0oAXwt/wAgmP6t/wChGuwoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKaQDQA6igAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuO18A+Vkf3v6UAHhb/AJBUf1b/ANCNdjQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFcZ4gOPJ/4F/SgBfCq40qM+pb/0I12VABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVwviQEmH/AIF/SgC14W/5BMX1b/0I12FABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFcT4hYAw/8AAv6UATeFv+QTF9X/APQjXYUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXD+IgCYf8AgX9KALHhb/kEx/V//QjXYUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVxHiEHMP/Av6UAT+Fv8AkExfVv8A0I12FABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAITgUtABRQAUUAFFABRQAUUAFFABRQAUUAFIKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuH8RZzD/AMC/pQBY8Lf8gmP6v/6Ea7CgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuM8QZzFgf3v6UAO8Lf8gqP6t/6Ea7GgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuL8QSbDD/AMC/pQBJ4W/5BMX1f/0I12FABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFcV4gjVzFk4xu/pQBZ8MjGkw/8AAv8A0I11lABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABXF+IDgxc4+9/SgDS0Bdulwf7mfzroqACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigArz/wATHDQ/Rv6UgOr0tAljCoOQEUZ9eK1aYBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJXBeJMbofo39KQGl4ZYtpUOTnG4fkxrrKYBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUtABRQAlcN4jOGh47N/SkBc8MY/sqL/gX/oRrrqYBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFcL4kzmH6N/SkBb8L/8AIJi+r/8AoRrr6YBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFcR4ixmH/AIF/SkBY8Lf8gmL6t/6Ea6+mAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXBeJiAYcnH3v6UgNHwyB/ZUWP9r+ZrrKYBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFcF4lj3mH2Df0oA0fDP/ACCov+BfzNdZQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXDeIid0OMdG/pSAveGTnSovx/ma6umAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXD+IU3NFz2b+lICz4XGNKi+rf+hGuvpgFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAJS0AJXHa64DRZGfvf0pATeGR/xKov8AgX8zXWUwCigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAaa4nxCrFosejf0pAWPC//ACCovq3/AKEa7CmAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUAFLQAUUAJXHa+xBix/tf0oAl8MkHSov+BfzNdbQAUUAFFABRQAUUAFFABRQAUlAC0UAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAIaWgAooAKKACigAooAKKACigAooAKKACigApKAFooASuS1yNXMWf8Aa/pSATwuMaVF9W/9CNddTAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigArkdc3Zix/tf0oATwuf8AiVRfVv8A0I119ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXL6yM+X/wAC/pQBV8Lf8gqP6t/6Ea7KgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuW1gE+Xzj739KAKvhY/8SqP6t/6Ea7OgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAErmdXXJj5x97+lICr4W/5BUf1b/wBCNdjTAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArmtXGfL/4F/SgCn4W/5BUf1b/0I12NABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFc3q//ACz/AOBf0oApeFf+QVH9W/8AQjXY0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVz2quF8v/AIF/SgCh4W40qP6t/wChGuwoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK5bWSP3ef9r+lAEPhfnSovq3/AKEa6+gAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK5TWiMx5/2v6UgI/C4/4lUX1b/0I119MAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuR1w8xcf3v6UgDwzj+yosf7X8zXXUwCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACuY1fZujz7/0pMCp4XP8AxKovq3/oRrsqYBRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACZpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACuT1piGj/H+lJgReF/8AkFRfVv8A0I12FMBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAbgU6gAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAK5TWVYtHj3/pSYEXhc/wDEpi+rf+hGuuFMB1FABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABXO6o2Cn4/0pAZ3hb/AJBUf1b/ANCNdlTAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASud1YZ8v/gX9KQFLwsP+JTF9X/9CNdhTAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArn9UGfL/H+lICh4XIOlRY7Fv/AEI119MAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKxNR/g/H+lAGV4WH/Eqj+rf+hGuxoAKKAEpaACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKSgAooAWigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASue1X/ln+P8ASgCl4W/5BMX1f/0I119ABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AJRQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFYmogHZn3/pQBl+Fv+QTF9X/9CNdfQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACYpaACkoAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgApKACloASloAKKACigAooAKKACigAooAKSgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACse/XOz8f6UAZHhf/AJBMX1f/ANCNdfQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAhoFAC0UAFFABRQAUUAFFABRQAUUAFFACUtABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUlABS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFZN823Z+P9KAMfwv/wAgmL6v/wChGuuoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEooAKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKKAFooAKKACigAooAKKACigAooAKxdRGdn4/0oAzPDGP7Jh/4F/6Ea62gAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaAEooAWigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigApKAFooAKKACigAooAKKACkoAWigArF1D+D8f6UAZnhjH9kxY/2v/QjXW0AFFABRQAUUAFFABRQAUUAFFABRQAUUAJS0AFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFZV6Aduff+lAGJ4X/5BUX1f/0I12FABRQAUUAFFABRQAUUAFFABRQAUUAFJmgApMc0AOooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigBKz7sZ2/jQBheGCDpUWB/e/9CNddQAUUAFFABRQAUUAFFABRQAUUAFFABTNo3Z74xQA+igAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigArPugTt/GgDB8L/8AIJh/4H/6Ea62gBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKKAFooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKqTjp+NAHO+GP+QTD/wL/wBCNdZSAWimAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAVVmGcUAc94ZBGkw5/2v1Y11dABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFJQAtFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFACUZoAWigAooAKKACq8pxigDmvDBzpMX/AAL/ANCNdbQAUUAFFABRQAUUAFJQAtFABRQA0nFOoAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigBKWgAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKSgBaSgBaKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEooAWkoAKKACloAKKACigAooAKKACigAooASloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKAEpaACigAooAKKACigAooAKKACmEenWgBRnHNOoAKKACigAqGQdKAOS8MnGlw/8C/9CNdkKQC0UwCigAooASigAooAKKACigAooAKKACloAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACkoAWkoASnUAFFABRQAUUAFFABSZoAKWgAooASigBaKACigAooAKKACigApKAFpKAFpKACkzSAWigBM0ZoAKWgApaYCUUAFLQAlFABRQAtFABRQAUlABRQAtJQAUUAGaKQBSZoAKKAFzRTAKKQBRTAKKAEzS0gDNFMBM0tIApaYBRQAlFABS0AJRQAtFACUtABRQAlLQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABRQAUUAFFABSUALRQAUUAFJQAUUAFFABRQAtFACVE56UgPHdJ8RwWVqkbxuxXPIxjqT61v/8ACZWn/PKT9P8AGmBL/wAJlZf885PyH+NJ/wAJnZ/88pP0/wAaeghP+Ezsv+eUv6f408eMbE9Y5B+A/wAaAGnxnZ/88pP0/wAab/wmdn/zyk/T/GgA/wCEzs/+eUn6f40f8JnZ/wDPKT9P8aQAfGdp/wA8pP0/xo/4TOzx/qpP0/xoAZ/wmVr/AM8pP0/xpf8AhM7X/njJ+n+NAB/wmdr/AM8pP0/xpf8AhM7X/nlJ+n+NAw/4TO0/55Sfp/jTT4ztc/6mT9P8aBC/8Jna/wDPKT9P8aX/AITO0/55Sfp/jQMQeM7T/nlJ+n+NB8ZWn/PKT9P8aBC/8Jnaf88pP0/xpf8AhM7T/nlJ+n+NAxw8ZWX/ADzk/T/Gg+MrL/nnJ+Q/xp28xC/8JlZf885PyH+NH/CZWX/POT8h/jRbzD5CjxlZf885PyH+NL/wmNjj7kn5D/Gi3mAo8YWP9yT8h/jS/wDCYWH92T8h/jRbzAcfF+n+j/lSDxhYf3X/ACpDHf8ACX6f6P8AlS/8Jfp/o/5UAL/wlunf7f8A3zSf8Jdp/wDt/lQA4eLtN/2/++aX/hLdN9X/AO+aBDv+Es0z1b/vk0h8Wab6t/3yaBh/wlum+rf98ml/4S3TPV/++TTEH/CWaZ/eb/vk03/hLdN9X/75pAJ/wlmm+r/980f8Jbpvq/8A3zQAHxdpuP4/++aZ/wAJbp3+3/3zQAo8W6d/t/lR/wAJdp3+3+VADR4u0/0f8qcfF2nDpvP4UwAeL9O9H/Kj/hLtO9H/ACoAb/wl+n+j/lSjxfp/o/5UAJ/wmFh/df8AKj/hMNP/ALsn5f8A16AGf8JhYf3JPyH+NA8Y2H9yT8h/jRoA4+MLD+7J+Q/xpf8AhL9P9H/KgBP+Ev0/0f8AKnf8Jfp/o/5UAP8A+Et031f/AL5pP+Et071f/vmkAf8ACW6d/t/9804eLNN9W/75NADx4s0zP3m/75NKPFel/wB9v++TQA7/AISrS/77f98mmjxVpf8Afb/vk0AOHirSv77f98n/AApP+Eq0v++3/fJ/woAf/wAJRpeM+Yf++T/hR/wlGlf89D/3yf8ACgBf+Eo0o/8ALU/98n/ClPifSh/y1P8A3yf8KAuKPE2ln/lr/wCOn/ClHiXSz/y1/Q0WFcP+El0v/nt+h/wpD4l0v/nr+h/wosO4n/CSaWD/AK39DS/8JJpf/PX9D/hRYB3/AAkemf8APYfkaX/hItM/57D9aAGjxFphz++H5Gl/4SLTCP8AXD8jQAf8JDpv/PYUv/CQab/z2WgBf+Eh03/nsKX/AISHTB/y2WgBf+Eh0z/nstP/ALf0zH+vWgBh8QaZ/wA91pD4g0wf8tloGH/CQ6Z/z2H60v8AwkOmf89l/WgQv/CQaZ/z3WlHiDTMf69aAE/4SHTM485f1pf+Eh0v/nutACf8JBpn/PdaP+Eg0z/nutAC/wDCQaZ/z3Wl/t/TD/y3WgAGv6Z/z3Sl/t/TP+e6UAL/AG9pn/PdPzo/t3Tf+fhPzoAT+3tM/wCe6fnQNd0z/nun50WAX+3dN/5+E/Ol/tzTf+fhPzoAX+29N/5+I/zo/tvTf+fiP86AA63pv/PxH+dNOuaaP+W6fnQMX+3NN/5+E/Oj+2tO/wCfiP8A76FAg/trTsZ8+P8A76FH9t6b/wA90/76oAQa5pv/AD3T86kXWdPIOJ0OOvNACf23pv8Az8R/99CoX1/TE/5bqfoc/wAqAHjXtNIB89PzpTrmm/8APwn50AH9u6b/AM90/Oj+3dMH/LdPzoAT+3dN/wCe6fnS/wBuaaP+W6fnQAn9uab/AM90/OlGuaZ/z3T86AF/tzTf+fiP86T+3NN/5+E/OgA/t3TR/wAt0/Oj+3dM/wCe6fnQAf25pn/Pwn50v9uab/z8R/nQAf23p3/PxH/30KP7b03/AJ+I/wDvoUAH9t6b/wA/Ef507+29N/5+I/8AvoUAH9t6b/z8R/8AfQpf7b03/n4j/wC+hQMP7a03/n4j/wC+hUh1fTwP9fH/AN9Ciwrkf9tab/z8R/8AfQpf7a03/n4j/wC+hQMP7a03/n4j/wC+hS/21pv/AD8R/wDfQoAP7a03/n4j/wC+hR/bOm/8/Ef/AH0KAHf2xpv/AD8R/wDfQpP7Z07/AJ+I/wDvoUWEOGraf/z3j/76FH9raf8A8/Ef/fQp2Yrod/atgf8AlvH/AN9CkOrWGcefH/30KLMLoP7VsP8AnvH/AN9Cg6tp4/5bx/8AfQosx3Qf2tYf894/++hThqlif+W8f/fQoswuhf7Tsv8AntH/AN9Cnf2jZ5/1yf8AfQpWYDzf2o/5ap/30Kb/AGhaf89U/wC+hQFxft9p/wA9U/76FH2+1/56p/30KLBcPt1p/wA9U/76FH2+1/56p/30KLBdC/brX/nqn/fQpfttr/z1T/voU7BdC/bbX/nqn/fQo+223/PVP++hRqF0O+12/wDz0T8xR9rt/wDnov5ikAfa7f8A56J+YpftUH/PRfzFAC/aYP8Anov5ij7TB/fX8xQAfaYf76/mKX7RD/fX8xQAfaIf76/nThNEf4h+dAB50X94fnR5qf3h+dACiVP7w/Onb19R+dAw3r6ijzF9RQAb19aTePWgQbgO9O3D1oGN8xd2MjPpT8j1oAaGozQIfkUUDCoJCBikB8u0UwCigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAKKACigAooAceg+n9abVLdCHL1rTT7wrVGUt0PP+s/AVsJ/SpKXUtjrUX8QqBlxfu0tULoS96a1ALYmX74+lXl6mkMkHQU8UwLh7VO3SpGXYO1Om7UmJH/2Q=="
                'Components.AppException.LogError("EDICT", Str, "String Check", "EDICT_API")
                'Dim mByte As Byte()
                'mByte = Encoding.ASCII.GetBytes(s1)
                Dim imageBytes As Byte() = Base64DecodeString(bytes.Replace(" ", "+"))
                'Dim imageBytes As Byte() = Convert.FromBase64String(bytes.Replace(" ", "+"))
                'Dim imageBytes As Byte() = bytes.[Select](Function(x) CByte(x)).ToArray()
                ' Dim imageBytes As Byte() = GetBytesFromByteString(s1).ToArray()

                'Dim ints As Integer() =
                'Dim bytesInt As Byte() = bytes.[Select](Function(x) CByte(x)).ToArray()

                Dim strdocPath As String
                strdocPath = "\\192.168.100.56\edictdb-info\Temp\WebUpload\" & fileName
                Dim objfilestream As FileStream = New FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite)
                objfilestream.Write(imageBytes, 0, imageBytes.Length)
                objfilestream.Close()
                Return 1
            Catch ex As Exception
                Return 0
                Components.AppException.LogError(sSession.AccessCode, ex.Message, "error", "EDICT_API")
            End Try
        End Function
        '<WebMethod()>
        'Public Function UploadFile(fileName As String, str As String) As String
        '    Try
        '        Components.AppException.LogError("EDICT", str, "String Check", "EDICT_API")
        '        Dim mByte As Byte()
        '        mByte = Encoding.ASCII.GetBytes(str)
        '        Dim strdocPath As String
        '        strdocPath = "\\192.168.5.96\edictdb-info\Temp\WebUpload\" & fileName
        '        Dim objfilestream As FileStream = New FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite)
        '        objfilestream.Write(mByte, 0, mByte.Length)
        '        objfilestream.Close()
        '        Return 1
        '    Catch ex As Exception
        '        Return 0
        '        Components.AppException.LogError(sSession.AccessCode, ex.Message, "error", "EDICT_API")
        '    End Try
        'End Function
        <WebMethod()>
        Public Function FileDocumentINEdict(ByVal AccessCode As String, ByVal LoginID As String, ByVal CabinetName As String, ByVal SubCabinetName As String,
                                           ByVal FolderName As String, ByVal Tilte As String, ByVal Keyword As String, ByVal UploadedFileList As String) As String
            Dim iACC As Integer, iUsrname As Integer, iCabinet As Integer, iSubcabinet As Integer, iFolder As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
            Dim iPageDetailsid As Integer
            Dim sMessage As String, SendFiles As String = "", sNewfilename As String = "", GetfileExtension As String = ""

            Try
                If AccessCode = "" Then
                    sMessage = "ACCESSCODE_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If LoginID = "" Then
                    sMessage = "LOGINID_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If CabinetName = "" Then
                    sMessage = "CABINET_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If SubCabinetName = "" Then
                    sMessage = "SUB_CABINET_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If FolderName = "" Then
                    sMessage = "FOLDER_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Tilte = "" Then
                    sMessage = "Tilte_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If Keyword = "" Then
                    sMessage = "Keyword_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                If UploadedFileList = "" Then
                    sMessage = "UploadedFileList_Should_Not_Be_Empty"
                    Return sMessage
                    Exit Function
                End If
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    sMessage = "NOT_VALID_USERID"
                    Return sMessage
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                        Dim sSubCabinet As String = objclsEDICTGeneral.SafeSQL(SubCabinetName)
                        Dim sFolderName As String = objclsEDICTGeneral.SafeSQL(FolderName)
                        dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                        iCabinet = clsfrm.CheckCabName(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Checking for Cabinet name Existance
                        If iCabinet = 0 Then
                            'sMessage = "NOT_VALID_CABINET"
                            'Return sMessage
                            clsfrm.CreateCabinet(AccessCode, CabinetName, dt.Rows(0).Item("USR_ID"), dt.Rows(0).Item("USR_DeptID")) 'Creating new Cabinet
                        End If
                        dt2 = clsfrm.GetCabinetID(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting cabinetID
                        iSubcabinet = clsfrm.CheckSubCabName(AccessCode, sSubCabinet, dt2.Rows(0).Item("CBN_ID")) 'Checking for subcabinet name Existance
                        If iSubcabinet = 0 Then
                            ' sMessage = "NOT_VALID_SUB-CABINET"
                            ' Return sMessage
                            clsfrm.CreateSubCabDetails(AccessCode, dt2.Rows(0).Item("CBN_ID"), SubCabinetName, dt.Rows(0).Item("USR_ID"), dt.Rows(0).Item("USR_DeptID")) 'Creating new Sub-Cabinet
                            clsfrm.UpdateSubCabDetails(AccessCode, dt.Rows(0).Item("USR_DeptID"), dt2.Rows(0).Item("CBN_ID")) 'Updating Sub-cabinet 
                        End If
                        dt4 = clsfrm.GetCabinetID(AccessCode, sSubCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting Sub-cabinetID
                        iFolder = clsfrm.CheckFoldersName(AccessCode, dt.Rows(0).Item("USR_ID"), sFolderName, dt4.Rows(0).Item("CBN_ID")) 'Checking for Folder Existance
                        If iFolder = 0 Then
                            'sMessage = "NOT_VALID_FOLDER"
                            ' Return sMessage
                            clsfrm.CreateFolder(AccessCode, dt4.Rows(0).Item("CBN_ID"), FolderName, dt.Rows(0).Item("USR_ID")) 'Creating new Folder
                            clsfrm.UpdateFolderCount(AccessCode, dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID")) 'Updating Folders
                        End If
                        dt6 = clsfrm.GetFolderID(AccessCode, sFolderName, dt4.Rows(0).Item("CBN_ID")) 'Getting FolderID

                        Try
                            Dim sTempPath As String = ""
                            sTempPath = objclsGeneralFunctions.GetTempPath(AccessCode, 1, "TempPath")                 'File stream

                            If sTempPath.EndsWith("\") = True Then
                                sTempPath = sTempPath & "Temp\WebUpload\" & LoginID & "\"
                            Else
                                sTempPath = sTempPath & "Temp\WebUpload\" & LoginID & "\"
                            End If

                            If Directory.Exists(sTempPath) = False Then
                                Directory.CreateDirectory(sTempPath)
                            End If

                        If Directory.Exists(sTempPath) Then
                            For Each filepath As String In Directory.GetFiles(sTempPath)
                                File.Delete(filepath)
                            Next
                            'For Each dir As String In Directory.GetDirectories(sTempPath)
                            '    Directory.Delete(dir)
                            'Next
                        End If
                        Dim imageBytes As Byte() = Base64DecodeString(UploadedFileList.Replace(" ", "+"))
                        sNewfilename = System.IO.Path.GetFileNameWithoutExtension(Tilte)
                            Dim fileExtension As String = Path.GetExtension(Tilte)
                            Dim strdocPath As String
                            strdocPath = sTempPath & sNewfilename & fileExtension
                            Dim objfilestream As FileStream = New FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite)
                            objfilestream.Write(imageBytes, 0, imageBytes.Length)
                            objfilestream.Close()
                            Dim fileEntries As String() = Directory.GetFiles(sTempPath)
                            Dim fileName As String, Uploades As String = ""
                            For Each fileName In fileEntries
                                Uploades = Uploades & ";" & fileName
                            Next fileName
                            SendFiles = Uploades.Remove(0, 1)
                        Catch ex As Exception
                        End Try
                        Dim length As Long = New System.IO.FileInfo(SendFiles).Length    'In Bytes
                        GetfileExtension = Path.GetExtension(SendFiles)  'For Extention
                        GetfileExtension = GetfileExtension.Remove(0, 1)
                        iPageDetailsid = clsfrm.CreateIndex(AccessCode, dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID"), dt6.Rows(0).Item("FOL_FolID"), 1, sNewfilename, Keyword, dt.Rows(0).Item("USR_ID"), SendFiles) 'File index
                        'Return iPageDetailsid
                        Dim s As String = dt2.Rows(0).Item("CBN_ID") & "," & dt4.Rows(0).Item("CBN_ID") & "," & dt6.Rows(0).Item("FOL_FolID") & "," & iPageDetailsid & "," & length & "," & GetfileExtension
                        Return s
                    ElseIf iUsrname = 0 Then
                        sMessage = "NOT_VALID_USERID"
                        Return sMessage
                    End If
                ElseIf iACC = 0 Then
                    sMessage = "NOT_VALID_ACCESS_CODE"
                    Return sMessage
                End If
            Catch ex As Exception
                'Throw
                sMessage = "ERROR_IN_FILING"
                clsfrm.LogError(AccessCode, ex.Message, "FileDocumentINEdict", "EDICT_API")
                Return sMessage
            End Try
            Return ""
        End Function
        <WebMethod()>
        Public Function GetFiles(ByVal AccessCode As String, ByVal LoginID As String, ByVal CabinetName As String, ByVal SubCabinetName As String,
                                           ByVal FolderName As String) As String
            Dim iACC As Integer, iUsrname As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Dim FileSelectedID As String, sSelectedDocTypeID As String, sDetailsId As String = "", file As String = "", filepath As String = ""
            Dim BaseID As Integer
            Try
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    Return "NOT_VALID_ACCESS_CODE"
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    Return "NOT_VALID_USERID"
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                        Dim sSubCabinet As String = objclsEDICTGeneral.SafeSQL(SubCabinetName)
                        Dim sFolderName As String = objclsEDICTGeneral.SafeSQL(FolderName)
                        dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                        dt2 = clsfrm.GetCabinetID2(AccessCode, sCabinet) 'Getting cabinetID
                        dt4 = clsfrm.GetCabinetID2(AccessCode, sSubCabinet) 'Getting Sub-cabinetID
                        dt6 = clsfrm.GetFolderID(AccessCode, sFolderName, dt4.Rows(0).Item("CBN_ID")) 'Getting FolderID
                        dt8 = clsfrm.LoadBaseIdFromFolder(AccessCode, dt.Rows(0).Item("USR_ID"), dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID"), dt6.Rows(0).Item("FOL_FolID"))
                        If (dt8.Rows.Count > 0) Then
                            BaseID = dt8.Rows(0).Item("PGE_BASENAME")
                            FileSelectedID = dt8.Rows(0).Item("PGE_BASENAME")
                            sSelectedDocTypeID = dt8.Rows(0).Item("PGE_DOCUMENT_TYPE")
                            For i = 0 To dt8.Rows.Count - 1
                                sDetailsId = dt8.Rows(i).Item("PGE_BASENAME")
                                file = String.Empty
                                file = clsfrm.GetPageFromEdict(AccessCode, sDetailsId, LoginID)
                                filepath = filepath & "," & file
                            Next
                            filepath = filepath.Remove(0, 1)
                        End If
                        Return filepath
                    ElseIf iUsrname = 0 Then
                        Return "NOT_VALID_USERID"
                    End If

                ElseIf iACC = 0 Then
                    Return "NOT_VALID_ACCESS_CODE"
                End If

            Catch ex As Exception
                Return "IN_VALID_PARAMETER"
            End Try
            Return ""
        End Function
        <WebMethod()>
        Public Function GetAllFiles(ByVal AccessCode As String, ByVal LoginID As String, ByVal CabinetName As String, ByVal SubCabinetName As String,
                                         ByVal FolderName As String, ByVal sDate As String, ByVal Title As String, ByVal Keyword As String,
                                          ByVal OCRtext As String, ByVal Format As String, ByVal CreatedBy As String, ByVal AnyDescriptor As String,
                                          ByVal DocumentTypes As String) As String
            Dim iACC As Integer, iUsrname As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Dim FileSelectedID As String, sSelectedDocTypeID As String, sDetailsId As String = "", file As String = "", filepath As String = "", base2 As String = ""
            Dim BaseID As Integer
            Try
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    Return "NOT_VALID_ACCESS_CODE"
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    Return "NOT_VALID_USERID"
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                        Dim sSubCabinet As String = objclsEDICTGeneral.SafeSQL(SubCabinetName)
                        Dim sFolderName As String = objclsEDICTGeneral.SafeSQL(FolderName)
                        dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                        If sCabinet <> "" Then
                            dt2 = clsfrm.GetCabinetID2(AccessCode, sCabinet) 'Getting cabinetID
                            sCabinet = dt2.Rows(0).Item("CBN_ID")
                        End If
                        If sSubCabinet <> "" Then
                            dt4 = clsfrm.GetCabinetID2(AccessCode, sSubCabinet) 'Getting Sub-cabinetID
                            sSubCabinet = dt4.Rows(0).Item("CBN_ID")
                        End If
                        If sFolderName <> "" Then
                            dt6 = clsfrm.GetFolderID2(AccessCode, sFolderName) 'Getting FolderID
                            sFolderName = dt6.Rows(0).Item("FOL_FolID")
                        End If
                        dt8 = objclsSearch.SearchDocuments(AccessCode, 1, dt.Rows(0).Item("USR_ID"), sCabinet, sSubCabinet, sFolderName, DocumentTypes, Keyword, AnyDescriptor, sDate, sDate, OCRtext, AnyDescriptor, Format, "", CreatedBy, , Title)
                        If (dt8.Rows.Count > 0) Then
                            For i = 0 To dt8.Rows.Count - 1
                                sDetailsId = dt8.Rows(i).Item("BASENAME")
                                file = String.Empty
                                file = clsfrm.GetPageFromEdict(AccessCode, sDetailsId, LoginID)
                                filepath = filepath & "," & file
                            Next
                            filepath = filepath.Remove(0, 1)
                            Dim file2 As Byte() = System.IO.File.ReadAllBytes(filepath)
                            base2 = Convert.ToBase64String(file2)
                        End If
                        Return base2
                    ElseIf iUsrname = 0 Then
                        Return "NOT_VALID_USERID"
                    End If
                ElseIf iACC = 0 Then
                    Return "NOT_VALID_ACCESS_CODE"
                End If
            Catch ex As Exception
                Return "IN_VALID_PARAMETER"
            End Try
            Return ""
        End Function
        <WebMethod()>
        Public Function GetFileFromEDICT(ByVal AccessCode As String, ByVal LoginID As String, ByVal FileID As Integer) As String
            Dim iACC As Integer, iUsrname As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Dim sDetailsId As String = "", filepath As String = "", base2 As String = "", file As String = "", GetfileExtension As String = ""
            Try
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    Return "NOT_VALID_ACCESS_CODE"
                    Exit Function
                End If
                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    Return "NOT_VALID_USERID"
                    Exit Function
                End If
                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        file = String.Empty
                        file = clsfrm.GetPageFromEdict(AccessCode, FileID, LoginID)
                        filepath = filepath & "," & file
                        filepath = filepath.Remove(0, 1)
                    Dim file2 As Byte() = System.IO.File.ReadAllBytes(filepath)
                    base2 = Convert.ToBase64String(file2)
                    Dim mimeType As String = MimeMapping.GetMimeMapping(filepath)
                    GetfileExtension = Path.GetExtension(filepath)  'For Extention
                    GetfileExtension = GetfileExtension.Remove(0, 1)
                    Return base2 & "," & mimeType & "," & GetfileExtension
                ElseIf iUsrname = 0 Then
                        Return "NOT_VALID_USERID"
                    End If
                ElseIf iACC = 0 Then
                    Return "NOT_VALID_ACCESS_CODE"
                End If
            Catch ex As Exception
                Return "IN_VALID_PARAMETER"
            End Try
            Return ""
        End Function

        <WebMethod()>
        Public Function ArchieveDocument(ByVal AccessCode As String, ByVal LoginID As String, ByVal CabinetName As String, ByVal SubCabinetName As String,
                                         ByVal FolderName As String) As String
            Dim iACC As Integer, iUsrname As Integer, iCabinet As Integer
            Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Dim dtFol As New DataTable
            Dim dtPage As New DataTable
            Dim dtSubCab As New DataTable
            Dim sMessage As String
            Dim sImagePath As String = ""
            Try
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    Return "NOT_VALID_ACCESS_CODE"
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    Return "NOT_VALID_USERID"
                    Exit Function
                End If


                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                        Dim sSubCabinet As String = objclsEDICTGeneral.SafeSQL(SubCabinetName)
                        Dim sFolderName As String = objclsEDICTGeneral.SafeSQL(FolderName)
                        dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                        dt2 = clsfrm.GetCabinetID2(AccessCode, sCabinet) 'Getting cabinetID
                        If sCabinet <> "" And sSubCabinet = "" And sFolderName = "" Then 'Cabinet
                            dtSubCab = objclsSubCabinet.LoadSubCabGrid(AccessCode, 1, sCabinet, 0)
                            objCab.UpdateStatus(AccessCode, 1, "AV", sCabinet, "V", dt.Rows(0).Item("USR_ID"))
                            If dtSubCab.Rows.Count > 0 Then
                                For i = 0 To dtSubCab.Rows.Count - 1
                                    dtFol = objclsFolders.LoadFolders(AccessCode, 1, dtSubCab.Rows(i)("CBN_ID"), dt.Rows(0).Item("USR_ID"))
                                    objclsSubCabinet.UpdateStatus(AccessCode, 1, "AV", dtSubCab.Rows(i)("CBN_ID"), "V", dt.Rows(0).Item("USR_ID"))
                                    If dtFol.Rows.Count > 0 Then
                                        For j = 0 To dtFol.Rows.Count - 1
                                            dtPage = objclsArchive.LoadPageGrid(AccessCode, 1, dtFol.Rows(j)("FOL_FOLID"), 0)
                                            objclsFolders.UpdateStatus(AccessCode, 1, "AV", dtFol.Rows(j)("FOL_FOLID"), "V", dt.Rows(0).Item("USR_ID"))
                                            If dtPage.Rows.Count > 0 Then
                                                For K = 0 To dtPage.Rows.Count - 1
                                                    sImagePath = objclsSearch.GetPageFromEdict(AccessCode, dtPage.Rows(K)("ID"))
                                                    objclsArchive.FilePageInEdict(AccessCode, 1, Val(dtPage.Rows(K)("ID")), sImagePath)
                                                    objclsArchive.UpdateStatus(AccessCode, 1, "AV", dtPage.Rows(K)("ID"), "V", dt.Rows(0).Item("USR_ID"))
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        ElseIf sCabinet <> "" And sSubCabinet <> "" And sFolderName = "" Then 'SubCabinet
                            '  dt4 = clsfrm.GetCabinetID2(AccessCode, SubCabinetName) 'Getting Sub-cabinetID
                            dtFol = objclsFolders.LoadFolders(AccessCode, 1, sSubCabinet, dt.Rows(0).Item("USR_ID"))
                            objclsSubCabinet.UpdateStatus(AccessCode, 1, "AV", sSubCabinet, "V", dt.Rows(0).Item("USR_ID"))
                            If dtFol.Rows.Count > 0 Then
                                For j = 0 To dtFol.Rows.Count - 1
                                    dtPage = objclsArchive.LoadPageGrid(AccessCode, 1, dtFol.Rows(j)("FOL_FOLID"), 0)
                                    objclsFolders.UpdateStatus(AccessCode, 1, "AV", dtFol.Rows(j)("FOL_FOLID"), "V", dt.Rows(0).Item("USR_ID"))
                                    If dtPage.Rows.Count > 0 Then
                                        For K = 0 To dtPage.Rows.Count - 1
                                            objclsArchive.UpdateStatus(AccessCode, 1, "AV", dtPage.Rows(K)("ID"), "V", dt.Rows(0).Item("USR_ID"))
                                        Next
                                    End If
                                Next
                            End If
                        ElseIf sCabinet <> "" And sSubCabinet <> "" And sFolderName <> "" Then 'Folder
                            '  dt6 = clsfrm.GetFolderID(AccessCode, FolderName, dt4.Rows(0).Item("CBN_ID")) 'Getting FolderID
                            dtPage = objclsArchive.LoadPageGrid(AccessCode, 1, sFolderName, 0)
                            objclsFolders.UpdateStatus(AccessCode, 1, "AV", sFolderName, "V", dt.Rows(0).Item("USR_ID"))
                            If dtPage.Rows.Count > 0 Then
                                For K = 0 To dtPage.Rows.Count - 1
                                    sImagePath = objclsSearch.GetPageFromEdict(AccessCode, Val(dtPage.Rows(K)("ID")))
                                    objclsArchive.FilePageInEdict(AccessCode, 1, Val(dtPage.Rows(K)("ID")), sImagePath)
                                    objclsArchive.UpdateStatus(AccessCode, 1, "V", dtPage.Rows(K)("ID"), "V", dt.Rows(0).Item("USR_ID"))
                                Next
                            End If
                        End If
                        'ElseIf iCab = 1 And isub = 1 And ifol = 1 Then 'Page
                        '    dtPage = objclsArchive.GetFilesFromTitle(sSession.AccessCode, sSession.AccessCodeID, lblname.Text)
                        '    For K = 0 To dtPage.Rows.Count - 1
                        '        sImagePath = objclsSearch.GetPageFromEdict(sSession.AccessCode, Val(dtPage.Rows(K)("PGE_BASENAME")))
                        '        objclsArchive.FilePageInEdict(sSession.AccessCode, sSession.AccessCodeID, Val(dtPage.Rows(K)("PGE_BASENAME")), sImagePath)
                        '        objclsArchive.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "V", Val(dtPage.Rows(K)("PGE_BASENAME")), "V", sSession.UserID)
                        '    Next
                        '    LoadPage()
                        'End If
                        ' Return filepath
                    ElseIf iUsrname = 0 Then
                        Return "NOT_VALID_USERID"
                    End If

                ElseIf iACC = 0 Then
                    Return "NOT_VALID_ACCESS_CODE"
                End If

            Catch ex As Exception
                sMessage = "IN_VALID_PARAMETER"
                clsfrm.LogError(AccessCode, ex.Message, "FileDocumentINEdict", "EDICT_API")
                Return sMessage
            End Try
            Return ""
        End Function

        <WebMethod()>
        Public Function DeactivateFile(ByVal AccessCode As String, ByVal LoginID As String, ByVal FileID As String) As String
            Dim sMessage As String
            Dim iACC As Integer, iUsrname As Integer
            Dim dtPage As New DataTable, dt As New DataTable, dt2 As New DataTable
            Dim sImagePath As String = ""
            Try
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    Return "NOT_VALID_ACCESS_CODE"
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    Return "NOT_VALID_USERID"
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        dt2 = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                        sImagePath = objclsSearch.GetPageFromEdict(AccessCode, FileID)
                        objclsArchive.FilePageInEdict(AccessCode, 1, FileID, sImagePath)
                        objclsArchive.UpdateStatus(AccessCode, 1, "X", FileID, "X", dt2.Rows(0).Item("USR_ID"))
                        dt = clsfrm.GetDelCabinetID(AccessCode, FileID) 'Getting Cabinet ID
                        clsfrm.UpdateFolderCount(AccessCode, dt.Rows(0).Item("pge_cabinet"), dt.Rows(0).Item("pge_cabinet")) 'Updating Folders
                        Return True
                    ElseIf iUsrname = 0 Then
                        Return "NOT_VALID_USERID"
                    End If
                ElseIf iACC = 0 Then
                    Return "NOT_VALID_ACCESS_CODE"
                End If
            Catch ex As Exception
                sMessage = "IN_VALID_PARAMETER"
                clsfrm.LogError(AccessCode, ex.Message, "DeactivateFile", "EDICT_API")
                Return sMessage
            End Try
            Return ""
        End Function
        <WebMethod()>
        Public Function EDICTUser(ByVal AccessCode As String, ByVal LoginID As String, ByVal Department As String, ByVal LoginName As String,
                                 ByVal FullName As String, ByVal Password As String, ByVal EmailID As String, ByVal Designation As String) As String
            Dim sMessage As String
            Dim iACC As Integer, iUsrname As Integer, iDepartment As Integer
            Dim Arr() As String
            Dim objstrUserDetails As New strUserDetails
            Dim dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
            Try
                ''check for access code
                Dim sRet = clsfrm.checkForAccesscode(AccessCode)
                If sRet = "False" Then
                    Return "NOT_VALID_ACCESS_CODE"
                    Exit Function
                End If

                iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
                iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
                If iUsrname = 0 Then
                    Return "NOT_VALID_USERID"
                    Exit Function
                End If

                If iACC <> 0 Then
                    If iUsrname <> 0 Then
                        dt2 = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                        objstrUserDetails.iUsr_ID = 0
                        objstrUserDetails.sUsr_Status = "C"
                        dt4 = clsfrm.NewUsertDepartment(AccessCode, Department) 'Get Input Department ID
                        objstrUserDetails.iUsr_DeptID = dt4.Rows(0).Item("org_node")
                        objstrUserDetails.iUsr_LevelCode = 3
                        objstrUserDetails.iUSR_SectionID = 0
                        objstrUserDetails.sUsr_LoginName = objclsEDICTGeneral.SafeSQL(Trim(LoginName))
                        objstrUserDetails.sUsr_PassWord = objclsEDICTGeneral.EncryptPassword((Password))
                        objstrUserDetails.sUsr_FullName = objclsEDICTGeneral.SafeSQL(Trim(FullName))
                        objstrUserDetails.sUsr_Email = objclsEDICTGeneral.SafeSQL(Trim(EmailID))
                        objstrUserDetails.iUsr_IsSuperUser = 2
                        objstrUserDetails.iUsr_MemberType = 0
                        dt6 = clsfrm.NewUsertDesignation(AccessCode, Designation) 'Get Input Designation ID
                        objstrUserDetails.iUsr_Designation = dt6.Rows(0).Item("Mas_ID")
                        objstrUserDetails.iUSR_usrGrpLvlPerm = 1
                        objstrUserDetails.iUsr_CrBy = dt2.Rows(0).Item("USR_ID")
                        objstrUserDetails.sUsr_IPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
                        objstrUserDetails.sUsr_DelFlag = "A"
                        objstrUserDetails.sUsr_Code = ""
                        objstrUserDetails.sOther_DeptList = ""

                        objclsUser.SaveUserDetails(AccessCode, 1, objstrUserDetails, dt4.Rows(0).Item("org_node"))
                        dt8 = clsfrm.NewUsertIDandName(AccessCode, LoginName)

                        Return dt8.Rows(0).Item("UserID") & "," & LoginName

                    End If
                ElseIf iACC = 0 Then
                    Return "NOT_VALID_ACCESS_CODE"
                End If

            Catch ex As Exception
                sMessage = "IN_VALID_PARAMETER"
                clsfrm.LogError(AccessCode, ex.Message, "EDICTUser", "EDICT_API")
                Return sMessage
            End Try
            Return ""
        End Function

    <WebMethod(EnableSession:=True)>
    Public Function EDICTSignIN(ByVal Username As String, ByVal Password As String) As String
        Dim sMessage As String
        Dim sAccessCode As String, sUserName As String, sIPAddress As String, sPassword As String
        Dim iUserID As Integer, iAccessCodeID As Integer
        Dim objstrUserDetails As New strUserDetails
        Dim iExpDay As Integer, iDays As Integer, iAlertDays As Integer, iMinPassword As Integer, iMaxPassword As Integer
        Dim objstrLogin As New strLogin
        Dim sMsg As String = ""
        Try
            If Username = "" Then
                sMessage = "Username_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If Password = "" Then
                sMessage = "Password_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If

            sAccessCode = objclsEDICTGeneral.SafeSQL("EDICT")
            sUserName = objclsEDICTGeneral.SafeSQL(Username.Trim)
            sPassword = objclsEDICTGeneral.SafeSQL(Password)
            sIPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode)
            iUserID = objclsGeneralFunctions.GetUserIDFromLoginName(sAccessCode, iAccessCodeID, sUserName)
            If iUserID = 0 Then
                Return "Invalid Login Name/Password."
            Else
                sPassword = objclsEDICTGeneral.EncryptPassword(sPassword)
                objstrLogin = objclsLogin.CheckUserIsValid(sAccessCode, iAccessCodeID, sUserName, sPassword, sIPAddress, "NO", "NO")
                If objstrLogin.Login = True Then
                    iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sAccessCode, iAccessCodeID, "Min")
                    iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sAccessCode, iAccessCodeID, "Max")
                    sSession.AccessCode = sAccessCode
                    sSession.AccessCodeID = iAccessCodeID
                    sSession.EncryptPassword = sPassword
                    sSession.IPAddress = sIPAddress
                    sSession.UserID = iUserID
                    sSession.UserLoginName = sUserName
                    sSession.UserFullName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAccessCode, iAccessCodeID, iUserID)
                    sSession.LastLoginDate = objclsLogin.GetLastLoginDate(sAccessCode, iAccessCodeID, iUserID)
                    sSession.MaxPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")
                    sSession.MinPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
                    sSession.NoOfUnSucsfAtteptts = objclsLogin.GetNoOfUnSuccssfulAttempts(sAccessCode, iAccessCodeID, iUserID)
                    sSession.TimeOut = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "TimeOut") * 60000
                    sSession.TimeOutWarning = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "TimeOutWarning") * 60000
                    sSession.Menu = "HOME"
                    ' sSession.ScreenWidth = Val(txtScreenWidth.Value)
                    ' sSession.ScreenHeight = Val(txtScreenHeight.Value)
                    sSession.FileInDB = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "FileInDB")
                    sSession.ScanPath = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "ScanPath")
                    sSession.ImagePath = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "ImagePath")
                    sSession.WebImgPath = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "WebImgPath")
                    sSession.OutlookEMail = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "OutlookEMail")
                    sSession.ErrorLog = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "ErrorLog")
                    sSession.TypeOfImage = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "TypeOfImage")
                    sSession.ImageFormat = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "ImageFormat")
                    sSession.Resolution = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "Resolution")
                    sSession.FileSize = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, iAccessCodeID, "FileSize")

                    'sSession.BrowserName = Request.Browser.Browser.ToString '& "-" & Request.Browser.Version.ToString Chrome
                    'sSession.BrowserName = "Chrome"
                    Session("AllSession") = sSession


                    '------- CheckFor Not Login ---------
                    iExpDay = objclsLogin.CheckForLastLogin(sAccessCode, iAccessCodeID, iUserID)
                    iDays = objclsLogin.GetNotLoginDays(sAccessCode, iAccessCodeID, iUserID)
                    If iExpDay >= iDays Then
                        objclsLogin.UpdateDutyStatusLock(sAccessCode, iAccessCodeID, iUserID, sIPAddress)
                        objclsGeneralFunctions.SaveUserLogOperations(sAccessCode, iAccessCodeID, iUserID, sUserName, "Account Locked. Please contact system admin.", sIPAddress, sPassword)
                        '  lblValidationMsg.Text = "Account Locked. Because you have not logged into EDICT from long time. Please contact system admin."
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('hide');$('#ModalPEAYesNo').modal('hide');$('#ModalForgotPassword').modal('hide');", True)
                        ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Account Locked. Because you have not logged into EDICT from long time. Please contact system admin.','', 'success');", True)
                        sMessage = "Account Locked. Because you have not logged into EDICT from long time. Please contact system admin"
                        Return sMessage
                        Exit Function
                    End If

                    '------- Check for Password Expire -------
                    If objclsLogin.CheckForPwdExpiry(sAccessCode, iAccessCodeID, iUserID) = False Then
                        ' lblOKtoCP.Text = "Your Password has expired. Please change it now."
                        ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divOKtoCP').addClass('alert alert-warning');$('#ModalValidation').modal('hide');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('show');$('#ModalPEAYesNo').modal('hide');$('#ModalForgotPassword').modal('hide');", True)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Your Password has expired. Please change it now.','', 'success');", True)
                        sMessage = "Your Password has expired. Please change it now"
                        Return sMessage
                        Exit Function
                    End If
                    objclsLogin.UpdateLogin(sAccessCode, iAccessCodeID, iUserID, sIPAddress)
                    objclsGeneralFunctions.SaveUserLogOperations(sAccessCode, iAccessCodeID, iUserID, sUserName, "Logged In", sIPAddress, sPassword)

                    '------- Check for Password Expire Alert -------
                    iExpDay = objclsLogin.CheckForExpireAlert(sAccessCode, iAccessCodeID, iUserID)
                    iAlertDays = objclsLogin.GetAlertDays(sAccessCode, iAccessCodeID)
                    If (iExpDay <= iAlertDays) And (iExpDay > 0) Then
                        ' lblPEAYesNoMsg.Text = "Your Password will expire in " & iExpDay & " days, Do you want to change your password now?"
                        '  ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divPEAYesNoMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('hide');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('hide');$('#ModalPEAYesNo').modal('show');$('#ModalForgotPassword').modal('hide');", True)
                        sMessage = "Your Password will expire in " & iExpDay & " days, Do you want to change your password now?"
                        Return sMessage
                        Exit Function
                    End If


                    'If objclsLogin.GetUserIsLogin(sSession.AccessCode, sSession.AccessCodeID, iUserID, sSession.IPAddress, sSession.BrowserName) = False Then
                    '    ' sMsg = "<script language=Javascript> CheckUserLoginSystem();</script>"
                    '    '  ClientScript.RegisterStartupScript(Me.GetType(), "Msg", sMsg)
                    '    Exit Function
                    'Else
                    objclsLogin.UpdateLoginWithStatus(sSession.AccessCode, sSession.AccessCodeID, iUserID, sSession.IPAddress, sSession.BrowserName, "YES")
                    objclsLogin.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, iUserID, sSession.EncryptPassword, sSession.IPAddress)

                    Dim guid__1 As String = Guid.NewGuid().ToString()
                    Session("AuthToken") = guid__1
                    HttpContext.Current.Response.Cookies.Add(New HttpCookie("AuthToken", guid__1))
                    'HttpContext.Current.Response.Redirect("http://192.168.100.56/EDICT/HomePages/HomePage.aspx", False)
                    'HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.[Public])
                    ' HttpContext.Current.Response.BufferOutput = True

                    Dim s2 As String = objclsEDICTGeneral.EncryptPassword(sSession.AccessCode & "," & sSession.AccessCodeID & "," & sSession.EncryptPassword & "," & sSession.IPAddress & "," &
                        sSession.UserID & "," & sSession.UserLoginName & "," & sSession.UserFullName & "," & sSession.LastLoginDate & "," & sSession.MaxPasswordCharacter & "," & sSession.MinPasswordCharacter & "," &
                     sSession.NoOfUnSucsfAtteptts & "," & sSession.TimeOut & "," & sSession.TimeOutWarning & "," & sSession.Menu & "," & sSession.FileInDB & "," & sSession.ScanPath & "," & sSession.ImagePath & "," &
                     sSession.WebImgPath & "," & sSession.OutlookEMail & "," & sSession.ErrorLog & "," & sSession.TypeOfImage & "," & sSession.ImageFormat & "," & sSession.Resolution & "," & sSession.FileSize)
                    'HttpContext.Current.Response.Redirect("~/HomePages/HomePage.aspx?id=" + s2 + "", False)
                    ' HttpContext.Current.Response.Flush()
                    '   Response.Redirect("~/HomePages/HomePage.aspx", False) 'HomePages/Home
                    'Dim url As String = "~/HomePages/HomePage.aspx"
                    'HttpContext.Current.Response.Write("<script>window.open('" & url & "');</script>")
                    'End If
                    ' Dim s As String = HttpContext.Current.Response.Cookies("ASP.NET_SessionId").Value
                    'Page.ClientScript.RegisterStartupScript(Me.[GetType](), "OpenWindow", "window.open('https://www.w3schools.com/','_newtab');", True)
                    Return "http://localhost/EDICT/HomePages/HomePage.aspx?id=" + s2 + ""
                    ' Return "http://192.168.100.56/EDICT/HomePages/HomePage.aspx?id=" + s2 + ""
                Else
                    sMessage = "Invalid Login Name/Password"
                End If
            End If
        Catch ex As Exception
            sMessage = "IN_VALID_PARAMETER"
            clsfrm.LogError(sAccessCode, ex.Message, "EDICTUser", "EDICT_API")
            Return sMessage
        End Try
        Return ""
    End Function


    <WebMethod()>
    Public Function FileDocumentINEdictNew(ByVal AccessCode As String, ByVal iAccCodeID As Integer, ByVal LoginID As String, ByVal CabinetName As String, ByVal SubCabinetName As String,
                                           ByVal FolderName As String, ByVal Tilte As String, ByVal Keyword As String, ByVal UploadedFileList As String) As String
        Dim iACC As Integer, iUsrname As Integer, iCabinet As Integer, iSubcabinet As Integer, iFolder As Integer
        Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dtChk As New DataTable
        Dim iPageDetailsid As Integer
        Dim sMessage As String, SendFiles As String = "", sNewfilename As String = "", GetfileExtension As String = ""

        Try
            If AccessCode = "" Then
                sMessage = "ACCESSCODE_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If LoginID = "" Then
                sMessage = "LOGINID_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If CabinetName = "" Then
                sMessage = "CABINET_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If SubCabinetName = "" Then
                sMessage = "SUB_CABINET_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If FolderName = "" Then
                sMessage = "FOLDER_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If Tilte = "" Then
                sMessage = "Tilte_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If Keyword = "" Then
                sMessage = "Keyword_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            If UploadedFileList = "" Then
                sMessage = "UploadedFileList_Should_Not_Be_Empty"
                Return sMessage
                Exit Function
            End If
            ''check for access code
            Dim sRet = clsfrm.checkForAccesscode(AccessCode)
            If sRet = "False" Then
                sMessage = "NOT_VALID_ACCESS_CODE"
                Return sMessage
                Exit Function
            End If


            Dim iDocType As Integer = clsfrm.checkDocumentType(AccessCode, iAccCodeID)
            If iDocType = 0 Then
                sMessage = "Create Document Type as Assignment Attachment"
                Return sMessage
                Exit Function
            End If

            iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
            iUsrname = clsfrm.GetUserID(AccessCode, LoginID) 'checking for LoginName
            If iUsrname = 0 Then
                sMessage = "NOT_VALID_USERID"
                Return sMessage
                Exit Function
            End If

            If iACC <> 0 Then
                If iUsrname <> 0 Then
                    Dim sCabinet As String = objclsEDICTGeneral.SafeSQL(CabinetName)
                    Dim sSubCabinet As String = objclsEDICTGeneral.SafeSQL(SubCabinetName)
                    Dim sFolderName As String = objclsEDICTGeneral.SafeSQL(FolderName)
                    'dt = clsfrm.GetDepartment(AccessCode, LoginID) 'Getting Department ID
                    dt = clsfrm.GetCustomerDepartment(AccessCode, CabinetName) 'Getting Department ID
                    iCabinet = clsfrm.CheckCabName(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Checking for Cabinet name Existance
                    If iCabinet = 0 Then
                        'sMessage = "NOT_VALID_CABINET"
                        'Return sMessage
                        clsfrm.CreateCabinet(AccessCode, CabinetName, iUsrname, dt.Rows(0).Item("USR_DeptID")) 'Creating new Cabinet
                    End If
                    dt2 = clsfrm.GetCabinetID(AccessCode, sCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting cabinetID
                    iSubcabinet = clsfrm.CheckSubCabName(AccessCode, sSubCabinet, dt2.Rows(0).Item("CBN_ID")) 'Checking for subcabinet name Existance
                    If iSubcabinet = 0 Then
                        ' sMessage = "NOT_VALID_SUB-CABINET"
                        ' Return sMessage
                        clsfrm.CreateSubCabDetails(AccessCode, dt2.Rows(0).Item("CBN_ID"), SubCabinetName, iUsrname, dt.Rows(0).Item("USR_DeptID")) 'Creating new Sub-Cabinet
                        clsfrm.UpdateSubCabDetails(AccessCode, dt.Rows(0).Item("USR_DeptID"), dt2.Rows(0).Item("CBN_ID")) 'Updating Sub-cabinet 
                    End If
                    'dt4 = clsfrm.GetCabinetID(AccessCode, sSubCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting Sub-cabinetID
                    dt4 = clsfrm.GetSubCabinetID(AccessCode, dt2.Rows(0).Item("CBN_ID"), sSubCabinet, dt.Rows(0).Item("USR_DeptID")) 'Getting Sub-cabinetID
                    iFolder = clsfrm.CheckFoldersName(AccessCode, iUsrname, sFolderName, dt4.Rows(0).Item("CBN_ID")) 'Checking for Folder Existance
                    If iFolder = 0 Then
                        'sMessage = "NOT_VALID_FOLDER"
                        ' Return sMessage
                        clsfrm.CreateFolder(AccessCode, dt4.Rows(0).Item("CBN_ID"), FolderName, iUsrname) 'Creating new Folder
                        clsfrm.UpdateFolderCount(AccessCode, dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID")) 'Updating Folders
                    End If
                    dt6 = clsfrm.GetFolderID(AccessCode, sFolderName, dt4.Rows(0).Item("CBN_ID")) 'Getting FolderID

                    Dim algSplit1 As String() = UploadedFileList.Split(";")
                    Dim sFilePathName1 As String


                    dtChk = clsfrm.CheckFileExistOrNot(AccessCode, dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID"), dt6.Rows(0).Item("FOL_FolID"), iDocType,
                    System.IO.Path.GetFileNameWithoutExtension(algSplit1(0)) & Path.GetExtension(algSplit1(0)))
                    If (dtChk.Rows.Count > 0) Then
                        Return ""
                    End If

                    Try
                        Dim sTempPath As String = ""

                        sTempPath = objclsGeneralFunctions.GetTempPath(AccessCode, 1, "TempPath")                 'File stream
                        If sTempPath = "" Then
                            sMessage = "TempPath not exists in the database."
                            Return sMessage
                            Exit Function
                        End If


                        If sTempPath.EndsWith("\") = True Then
                            sTempPath = sTempPath & "Temp\WebUpload\" & LoginID & "\"
                        Else
                            sTempPath = sTempPath & "Temp\WebUpload\" & LoginID & "\"
                        End If

                        If Directory.Exists(sTempPath) = False Then
                            Directory.CreateDirectory(sTempPath)
                        End If

                        If Directory.Exists(sTempPath) Then
                            For Each filepath As String In Directory.GetFiles(sTempPath)
                                File.Delete(filepath)
                            Next
                            'For Each dir As String In Directory.GetDirectories(sTempPath)
                            '    Directory.Delete(dir)
                            'Next
                        End If



                        Dim algSplit As String() = UploadedFileList.Split(";")

                        For i As Int32 = 0 To algSplit.Length - 1
                            Dim sFilePathName As String = sTempPath & System.IO.Path.GetFileNameWithoutExtension(algSplit(i)) & Path.GetExtension(algSplit(i))
                            FileCopy(algSplit(i), sFilePathName)
                        Next


                        ' Dim imageBytes As Byte() = Base64DecodeString(UploadedFileList.Replace(" ", "+"))
                        sNewfilename = Tilte
                        ' sNewfilename = System.IO.Path.GetFileNameWithoutExtension(Tilte)
                        'Dim fileExtension As String = Path.GetExtension(Tilte)
                        'Dim strdocPath As String
                        'strdocPath = sTempPath & sNewfilename & fileExtension
                        ' Dim objfilestream As FileStream = New FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite)
                        ' objfilestream.Write(imageBytes, 0, imageBytes.Length)
                        ' objfilestream.Close()
                        Dim fileEntries As String() = Directory.GetFiles(sTempPath)
                        Dim fileName As String, Uploades As String = ""
                        For Each fileName In fileEntries
                            Uploades = Uploades & ";" & fileName
                        Next fileName
                        SendFiles = Uploades.Remove(0, 1)
                    Catch ex As Exception
                    End Try
                    'Dim length As Long = New System.IO.FileInfo(SendFiles).Length    'In Bytes
                    'GetfileExtension = Path.GetExtension(SendFiles)  'For Extention
                    'GetfileExtension = GetfileExtension.Remove(0, 1)



                    iPageDetailsid = clsfrm.CreateIndex(AccessCode, dt2.Rows(0).Item("CBN_ID"), dt4.Rows(0).Item("CBN_ID"), dt6.Rows(0).Item("FOL_FolID"), iDocType, sNewfilename, Keyword, iUsrname, SendFiles) 'File index
                    'Return iPageDetailsid
                    Dim s As String = dt2.Rows(0).Item("CBN_ID") & "," & dt4.Rows(0).Item("CBN_ID") & "," & dt6.Rows(0).Item("FOL_FolID") & "," & iPageDetailsid & "," & SendFiles & "," & GetfileExtension
                    ' Return s
                ElseIf iUsrname = 0 Then
                    sMessage = "NOT_VALID_USERID"
                    Return sMessage
                End If
            ElseIf iACC = 0 Then
                sMessage = "NOT_VALID_ACCESS_CODE"
                Return sMessage
            End If
        Catch ex As Exception
            'Throw
            sMessage = "ERROR_IN_FILING"
            clsfrm.LogError(AccessCode, ex.Message, "FileDocumentINEdict", "EDICT_API")
            Return sMessage
        End Try
        Return ""
    End Function




    <WebMethod()>
    Public Function GetFileFromEDICTNew(ByVal AccessCode As String, ByVal LoginID As String, ByVal FileID As Integer) As String
        Dim iACC As Integer, iUsrname As Integer
        Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable, dt8 As New DataTable
        Dim sDetailsId As String = "", filepath As String = "", base2 As String = "", file As String = "", GetfileExtension As String = ""
        Try
            ''check for access code
            Dim sRet = clsfrm.checkForAccesscode(AccessCode)
            If sRet = "False" Then
                Return "NOT_VALID_ACCESS_CODE"
                Exit Function
            End If
            iACC = clsfrm.GetAccessCode(AccessCode) 'checking for Accesscode
            iUsrname = clsfrm.GetUsername(AccessCode, LoginID) 'checking for LoginName
            If iUsrname = 0 Then
                Return "NOT_VALID_USERID"
                Exit Function
            End If
            If iACC <> 0 Then
                If iUsrname <> 0 Then
                    file = String.Empty
                    file = clsfrm.GetPageFromEdict(AccessCode, FileID, LoginID)
                    filepath = filepath & "," & file
                    filepath = filepath.Remove(0, 1)
                    'Dim file2 As Byte() = System.IO.File.ReadAllBytes(filepath)
                    'base2 = Convert.ToBase64String(file2)
                    'Dim mimeType As String = MimeMapping.GetMimeMapping(filepath)
                    'GetfileExtension = Path.GetExtension(filepath)  'For Extention
                    'GetfileExtension = GetfileExtension.Remove(0, 1)
                    'Return base2 & "," & mimeType & "," & GetfileExtension
                    Return filepath
                ElseIf iUsrname = 0 Then
                    Return "NOT_VALID_USERID"
                End If
            ElseIf iACC = 0 Then
                Return "NOT_VALID_ACCESS_CODE"
            End If
        Catch ex As Exception
            Return "IN_VALID_PARAMETER"
        End Try
        Return ""
    End Function


    <WebMethod()>
    Public Function GetFileFromEDICTNew1() As String
        Return "1234"
    End Function
End Class