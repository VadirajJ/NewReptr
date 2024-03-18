Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Globalization
'Imports GleamTech.DocumentUltimate.AspNet.UI  'Document Permission
Imports System.Web.UI.WebControls.Image
Imports System.Web.Services
Imports System.Web.Script.Services

Partial Class ImageView
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Seacrh ImageView"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsEDICTGeneral As New clsEDICTGeneral
    Private objclsSearch As New clsSearch
    Private objclsView As New clsView

    Private objclsCollation As New clsCollation
    Private Shared sSession As AllSession
    Private objclsFolders As New clsFolders
    Private Shared iSelectedFirstID As Integer = 0
    Private Shared iNextID As Integer = 1
    Private Shared iPageNext As Integer
    Private Shared sImgFilePath As String = ""
    Private Shared iNextPage As Integer
    Private Shared sBaseName() As String
    Private Shared sSelectedChecksIDs As String = ""
    Private Shared iDocID As Integer
    Private objIndex As New clsIndexing
    Private Shared sDetailsId As String = ""
    Private Shared sSelectedCabID As String = ""
    Private Shared sSelectedSubCabID As String = ""
    Private Shared sSelectedFolID As String = ""
    Private Shared sSelectedDocTypeID As String = ""
    Private Shared sSelectedKWID As String = ""
    Private Shared sSelectedDescID As String = ""
    Private Shared sSelectedFrmtID As String = ""
    Private Shared sSelectedCrByID As String = ""
    Private Shared iSelectedIndexID As Integer = 0
    Private Shared sFormButtons As String
    Private Shared sTitle As String = "" 'Added steffi
    Private Shared sSelId As String = ""
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsPermission As New clsAccessRights
    Private Shared iCheck As Integer = 0
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnNavDocFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNavDoc.ImageUrl = "~/Images/SearchImage/Previous16.png"
        imgbtnNextNavDoc.ImageUrl = "~/Images/SearchImage/Next16.png"
        imgbtnNavDocFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNav.ImageUrl = "~/Images/SearchImage/Preview16.png"
        imgbtnNextNav.ImageUrl = "~/Images/SearchImage/Nextt16.png"
        imgbtnFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnAnnotation.ImageUrl = "~/Images/Annotation24.png"
        imgbtnAdd.ImageUrl = "~/Images/add-file.png"
        imgbtnIndexSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtDocument As New DataTable

        Try
            sSession = Session("AllSession")
            'documentViewer.ImageUrl = String.Format("~/Images/SearchImage/NoImage.jpg")  '' 25_07_22
            Dim url As String = String.Format("~/Images/SearchImage/NoImage.jpg")
            documentImgViewer.ImageUrl = url
            If IsPostBack = False Then
                BindCabinet() : BindDocumentType()
                Dim imageDataURL As String
                imageDataURL = ConfigurationManager.AppSettings("VSPath") & "Images\SearchImage\NoImage.jpg"

                gtViewer(imageDataURL, 1)
                'Vintaviewer(imageDataURL)

                'sFormButtons = objclsPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SAVF") 'Vijeth 12/02/2019
                'If sFormButtons.Contains(",View,") = True Then

                'End If
                'If sFormButtons.Contains(",Download,") = True Then
                '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                '    imgbtnAnnotation.Visible = False
                '    lblHVersion.Visible = False
                '    ddlAnnotationVersion.Visible = False
                'Else
                '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                'End If
                'If sFormButtons.Contains(",Annotation") = True Then
                '    imgbtnAnnotation.Visible = False
                '    lblHVersion.Visible = False
                '    ddlAnnotationVersion.Visible = False
                'End If

                'If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then  'Vijeth modified on 21/01/2020

                'End If

                sSelId = String.Empty : sSelectedChecksIDs = String.Empty
                sSelectedCabID = String.Empty : sSelectedSubCabID = String.Empty : sSelectedFolID = String.Empty
                sSelectedDocTypeID = String.Empty : sSelectedKWID = String.Empty : sSelectedDescID = String.Empty
                sSelectedFrmtID = String.Empty : sSelectedCrByID = String.Empty
                iSelectedIndexID = 0 : iSelectedFirstID = 0

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                RFVCabinet.InitialValue = "Select Cabinet" : RFVCabinet.ErrorMessage = "Select Cabinet." : RFVCabinet.ControlToValidate = "ddlCabinet" : RFVCabinet.ValidationGroup = "ValidateCabinet"
                RFVSubCabinet.InitialValue = "Select Sub Cabinet" : RFVSubCabinet.ErrorMessage = "Select Sub Cabinet." : RFVSubCabinet.ControlToValidate = "ddlSubCabinet" : RFVSubCabinet.ValidationGroup = "ValidateCabinet"
                RFVFolder.InitialValue = "Select Folder" : RFVFolder.ErrorMessage = "Select Folder." : RFVFolder.ControlToValidate = "ddlFolder" : RFVFolder.ValidationGroup = "ValidateCabinet"
                RFVDocumentType.InitialValue = "Select Document Type" : RFVDocumentType.ErrorMessage = "Select Document Type." : RFVDocumentType.ControlToValidate = "ddlDocumentTypeId" : RFVDocumentType.ValidationGroup = "ValidateCabinet"

                RFVTitle.ErrorMessage = "Enter Title." : RFVTitle.InitialValue = "" : RFVTitle.ControlToValidate = "txtTitle" : RFVTitle.ValidationGroup = "MSave"
                RFVMcabinet.ErrorMessage = "Select Cabinet." : RFVMcabinet.InitialValue = "Select Cabinet" : RFVMcabinet.ControlToValidate = "ddlMCabinet" : RFVMcabinet.ValidationGroup = "MSave"
                RFVMSubCabinet.ErrorMessage = "Select Sub-Cabinet." : RFVMSubCabinet.InitialValue = "Select Sub-Cabinet" : RFVMSubCabinet.ControlToValidate = "ddlMSubcabinet" : RFVSubCabinet.ValidationGroup = "MSave"
                RFVMFolder.ErrorMessage = "Select Folder." : RFVMFolder.InitialValue = "Select Folder" : RFVMFolder.ControlToValidate = "ddlMFolder" : RFVMFolder.ValidationGroup = "MSave"
                RFVType.ErrorMessage = "Select Document Type." : RFVType.InitialValue = "Select Document Type" : RFVType.ControlToValidate = "ddlType" : RFVType.ValidationGroup = "MSave"
                REVTitle.ErrorMessage = "Title exceeded maximum size(max 2000 characters)." : REVTitle.ValidationExpression = "^[\s\S]{0,2000}$" : REVTitle.ValidationGroup = "MSave"

                imgbtnBack.Visible = False
                If Request.QueryString("FolID") IsNot Nothing Then
                    ddlCabinet.SelectedValue = HttpUtility.UrlDecode(Request.QueryString("CabID"))
                    ddlCabinet_SelectedIndexChanged(sender, e)
                    ddlSubCabinet.SelectedValue = HttpUtility.UrlDecode(Request.QueryString("SubCabID"))
                    ddlSubCabinet_SelectedIndexChanged(sender, e)
                    ddlFolder.SelectedValue = HttpUtility.UrlDecode(Request.QueryString("FolID"))
                    ddlFolder_SelectedIndexChanged(sender, e)

                    Dim iDocSelectedID As Integer = 0, iFileSelectedID As Integer = 0
                    If lstDocument.Items.Count <> 0 Then
                        If Request.QueryString("DocumentSelectedID") IsNot Nothing Then
                            Try
                                'iDocSelectedID = HttpUtility.UrlDecode(Request.QueryString("DocumentSelectedID"))
                                'iDocSelectedID = iDocSelectedID - 1
                                'txtNavDoc.Text = iDocSelectedID + 1
                                'rak
                                iDocSelectedID = HttpUtility.UrlDecode(Request.QueryString("NavDoc"))
                                iDocSelectedID = iDocSelectedID - 1
                                txtNavDoc.Text = iDocSelectedID + 1

                            Catch ex As Exception
                            End Try
                        End If
                        lstDocument.SelectedIndex = iDocSelectedID
                        lstDocument_SelectedIndexChanged(sender, e)
                    End If

                    If lstFiles.Items.Count <> 0 Then
                        If Request.QueryString("FileSelectedID") IsNot Nothing Then
                            Try
                                'iFileSelectedID = HttpUtility.UrlDecode(Request.QueryString("FileSelectedID"))
                                'iFileSelectedID = iFileSelectedID - 1
                                'txtNav.Text = iFileSelectedID + 1
                                'iPageNext = iFileSelectedID

                                'rak
                                iFileSelectedID = HttpUtility.UrlDecode(Request.QueryString("Nav"))
                                iFileSelectedID = iFileSelectedID - 1
                                txtNav.Text = iFileSelectedID + 1
                                iPageNext = iFileSelectedID
                            Catch ex As Exception
                            End Try
                        End If
                        lstFiles.SelectedIndex = iFileSelectedID
                        lstFiles_SelectedIndexChanged(sender, e)
                    End If

                End If
                If Request.QueryString("ImgFilePath") IsNot Nothing Then
                    ddlCabinet.Visible = False
                    lblCabinet.Visible = False
                    lblSubcabinet.Visible = False
                    lblFolder.Visible = False
                    lblDocumentTypeId.Visible = False
                    ddlSubCabinet.Visible = False
                    ddlFolder.Visible = False
                    ddlDocumentTypeId.Visible = False
                    OpenDocument.Visible = False
                    Checkin.Visible = False
                    imgbtnAdd.Visible = False
                    imgbtnBack.Visible = True

                    sImgFilePath = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ImgFilePath")))
                    sTitle = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("Title"))) 'Added steffi

                    If Request.QueryString("SelectedFirstID") IsNot Nothing Then
                        iSelectedFirstID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFirstID")))
                    End If
                    If Request.QueryString("SelectedChecksIDs") IsNot Nothing Then
                        sSelectedChecksIDs = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedChecksIDs")))
                    End If

                    If Request.QueryString("SelectedCabID") IsNot Nothing Then
                        sSelectedCabID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedCabID")))
                        If sSelectedCabID <> "" Then
                            ddlCabinet.Visible = True
                            lblCabinet.Visible = True
                            ' imgbtnAdd.Visible = True   Vijeth 13/02/2019
                            ddlCabinet.SelectedValue = sSelectedCabID
                            ddlCabinet_SelectedIndexChanged(sender, e)
                        End If
                    End If
                    If Request.QueryString("SelectedSubCabID") IsNot Nothing Then
                        sSelectedSubCabID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedSubCabID")))
                        If sSelectedSubCabID <> "" Then
                            lblSubcabinet.Visible = True
                            ddlSubCabinet.Visible = True
                            'imgbtnAdd.Visible = True  Vijeth 13/02/2019
                            ddlSubCabinet.SelectedValue = sSelectedSubCabID
                            ddlSubCabinet_SelectedIndexChanged(sender, e)
                        End If
                    End If
                    If Request.QueryString("SelectedFolID") IsNot Nothing Then
                        sSelectedFolID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFolID")))
                        If sSelectedFolID <> "" Then
                            lblFolder.Visible = True
                            ddlFolder.Visible = True
                            ' imgbtnAdd.Visible = True  Vijeth 13/02/2019
                            ddlFolder.SelectedValue = sSelectedFolID
                        End If
                    End If
                    If Request.QueryString("SelectedDocTypeID") IsNot Nothing Then
                        sSelectedDocTypeID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedDocTypeID")))
                    End If
                    If Request.QueryString("SelectedKWID") IsNot Nothing Then
                        sSelectedKWID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedKWID")))
                    End If
                    If Request.QueryString("SelectedDescID") IsNot Nothing Then
                        sSelectedDescID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedDescID")))
                    End If
                    If Request.QueryString("SelectedFrmtID") IsNot Nothing Then
                        sSelectedFrmtID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFrmtID")))
                    End If
                    If Request.QueryString("SelectedCrByID") IsNot Nothing Then
                        sSelectedCrByID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedCrByID")))
                    End If
                    If Request.QueryString("SelectedIndexID") IsNot Nothing Then
                        iSelectedIndexID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedIndexID")))
                    End If

                    If Request.QueryString("SelId") IsNot Nothing Then
                        sSelId = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelId")))
                    End If


                    'Files Details
                    txtID.Text = "0" : txtPreId.Text = "1" : iPageNext = 0

                    'rak
                    Dim navDoc As String = ""
                    navDoc = HttpUtility.UrlDecode(Request.QueryString("Nav"))
                    If navDoc = "" Then
                        txtID.Text = "0"
                    Else
                        txtID.Text = navDoc
                    End If


                    sBaseName = sSelectedChecksIDs.Split(",")
                    For i = 0 To sBaseName.Length - 1
                        lstDocument.Items.Add(sBaseName(i))
                    Next

                    'txtNavDoc.Text = "1 Of " & lstDocument.Items.Count
                    txtNavDoc.Text = 1
                    lblNavDoc.Text = "/" & lstDocument.Items.Count
                    If lstDocument.Items.Count = 1 Then
                        txtNavDoc.Enabled = False
                    End If

                    Dim iDocSelectedID As Integer = 0, iFileSelectedID As Integer = 0
                    If lstDocument.Items.Count <> 0 Then
                        If Request.QueryString("DocumentSelectedID") IsNot Nothing Then
                            Try
                                'iDocSelectedID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("DocumentSelectedID")))
                                iDocSelectedID = 0
                                txtNavDoc.Text = iDocSelectedID + 1

                                'rak
                                'iDocSelectedID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("NavDoc")))
                                'txtNavDoc.Text = iDocSelectedID + 1

                            Catch ex As Exception
                            End Try
                        End If
                        lstDocument.SelectedIndex = iDocSelectedID
                        lstDocument_SelectedIndexChanged(sender, e)
                    End If

                    If lstFiles.Items.Count <> 0 Then
                        If Request.QueryString("FileSelectedID") IsNot Nothing Then
                            Try
                                'iFileSelectedID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FileSelectedID")))
                                'txtNav.Text = iFileSelectedID + 1
                                'iPageNext = iFileSelectedID
                                iFileSelectedID = 0
                                'rak
                                'iFileSelectedID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("Nav")))
                                txtNav.Text = iFileSelectedID + 1 'rakchange +1
                                iPageNext = iFileSelectedID
                            Catch ex As Exception
                            End Try
                        End If
                        lstFiles.SelectedIndex = iFileSelectedID
                        lstFiles_SelectedIndexChanged(sender, e)
                    End If

                    'BindAnnotaionDetails(Val(lstDocument.SelectedItem.Text), Val(lstFiles.SelectedItem.Text))
                    'If Request.QueryString("AnnotaionVersion") IsNot Nothing Then
                    '    If objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AnnotaionVersion"))) = "YES" Then
                    '        ddlAnnotationVersion.SelectedIndex = ddlAnnotationVersion.Items.Count - 1
                    '        ddlAnnotationVersion_SelectedIndexChanged(sender, e)
                    '    End If
                    'End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                ' RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name','', 'error');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnPreviousNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNavDoc.Click
        Dim dtFiles As New DataTable
        Dim ivalue As Integer = 0
        Try
            lblError.Text = "" : iPageNext = 0 : iCheck = 0
            If (iCheck = 1 Or lstDocument.Items.Count = 1) Then
                ivalue = Val(txtID.Text) - 1
            Else
                ivalue = Val(txtID.Text)
            End If
            If Val(ivalue) >= 1 Then
                If lstDocument.Items.Count <> -1 Then
                    iCheck = 0
                    lstFiles.Items.Clear()
                    If lstDocument.Items.Count = txtID.Text Then
                        If Val(txtID.Text) = 0 Then
                            'txtNavDoc.Text = "1 Of " & lstDocument.Items.Count
                            txtNavDoc.Text = 1
                            lblNavDoc.Text = "/" & lstDocument.Items.Count
                        Else
                            txtPreId.Text = Val(txtPreId.Text) - 1
                            'txtNavDoc.Text = "" & Val(txtPreId.Text) & " Of " & lstDocument.Items.Count
                            txtNavDoc.Text = Val(txtPreId.Text)
                            lblNavDoc.Text = "/" & lstDocument.Items.Count
                        End If
                        txtID.Text = Val(txtID.Text) - 2

                        dtFiles = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            'txtNav.Text = "1 Of " & lstFiles.Items.Count
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If

                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                    Else
                        txtPreId.Text = Val(txtPreId.Text) - 1
                        'txtNavDoc.Text = "" & Val(txtPreId.Text) & " Of " & lstDocument.Items.Count
                        txtNavDoc.Text = Val(txtPreId.Text)
                        lblNavDoc.Text = "/" & lstDocument.Items.Count

                        txtID.Text = Val(txtID.Text) - 1

                        dtFiles = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            'txtNav.Text = "1 Of " & lstFiles.Items.Count
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If

                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                    End If
                End If
            Else
                txtID.Text = 0
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name','', 'error');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPreviousNavDoc_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnNextNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNavDoc.Click
        Dim dtFiles As New DataTable
        Try
            lblError.Text = "" : iPageNext = 0
            If lstDocument.Items.Count <= txtID.Text Then
                txtID.Text = lstDocument.Items.Count
                Exit Sub
            End If
            txtID.Text = Val(txtID.Text) + 1
            If lstDocument.Items.Count <> txtID.Text Then
                If lstDocument.Items.Count <> -1 Then
                    iCheck = 1
                    lstFiles.Items.Clear()
                    If lstDocument.Items.Count >= txtID.Text Then
                        txtPreId.Text = Val(txtPreId.Text) + 1
                        'txtNavDoc.Text = "" & Val(txtID.Text) + 1 & " Of " & lstDocument.Items.Count
                        If txtNavDoc.Text > Val(txtID.Text) Then
                            txtNavDoc.Text = Val(txtNavDoc.Text)
                        Else
                            txtNavDoc.Text = Val(txtID.Text) + 1
                        End If

                        lblNavDoc.Text = "/" & lstDocument.Items.Count

                        lstDocument.SelectedIndex = Val(txtID.Text)
                        dtFiles = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            'txtNav.Text = "1 Of " & lstFiles.Items.Count
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If
                        If lstFiles.Items.Count = 1 Then
                            txtNav.Enabled = False
                        End If

                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNextNavDoc_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnNavDocFastRewind_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNavDocFastRewind.Click
        Try
            lblError.Text = ""
            txtPreId.Text = 2
            txtID.Text = 1
            imgbtnPreviousNavDoc_Click(sender, e)
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNavDocFastRewind_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnNavDocFastForword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNavDocFastForword.Click
        Try
            lblError.Text = ""
            txtPreId.Text = lstDocument.Items.Count - 1
            txtID.Text = lstDocument.Items.Count - 2
            imgbtnNextNavDoc_Click(sender, e)
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNavDocFastForword_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnPreviousNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNav.Click
        Try
            lblError.Text = ""
            If lstFiles.Items.Count > 0 Then
                If lstFiles.Items.Count <> -1 And iPageNext > 0 Then
                    iPageNext = iPageNext - 1
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                    Dim iPage As Integer = iPageNext
                    'txtNav.Text = iPage + 1 & " of " & lstFiles.Items.Count
                    txtNav.Text = iPage + 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                Else
                    iPageNext = 0
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPreviousNav_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnNextNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNav.Click
        Try
            lblError.Text = ""
            If iNextPage <> 0 Then
                iPageNext = iNextPage
                iNextPage = 0
            End If
            If lstFiles.Items.Count = 0 Then
                Exit Sub
            End If
            If lstFiles.Items.Count > iPageNext Then
                iPageNext = iPageNext + 1
                If lstFiles.SelectedIndex <> -1 And lstFiles.Items.Count > iPageNext Then
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                    Dim iPage As Integer = iPageNext
                    'txtNav.Text = iPage + 1 & " of " & lstFiles.Items.Count
                    txtNav.Text = iPage + 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                Else
                    lstFiles_SelectedIndexChanged(sender, e)
                End If
            Else
                lstFiles_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                txtNavDoc.Text = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNextNav_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnFastRewind_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFastRewind.Click
        Try
            lblError.Text = ""
            iPageNext = 0
            lstFiles.SelectedIndex = iPageNext
            lstFiles_SelectedIndexChanged(sender, e)
            If lstFiles.Items.Count > 0 Then
                'txtNav.Text = "1" & " of " & lstFiles.Items.Count
                txtNav.Text = 1
                lblNav.Text = "/" & lstFiles.Items.Count
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFastRewind_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnFastForword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFastForword.Click
        Try
            lblError.Text = ""
            If lstFiles.Items.Count > 0 Then
                iPageNext = lstFiles.Items.Count
                iPageNext = iPageNext - 1
                lstFiles.SelectedIndex = iPageNext
                lstFiles_SelectedIndexChanged(sender, e)
                'txtNav.Text = iPageNext + 1 & " of " & lstFiles.Items.Count
                txtNav.Text = iPageNext + 1
                lblNav.Text = "/" & lstFiles.Items.Count
            Else
                iPageNext = 0
                lstFiles.SelectedIndex = iPageNext
                lstFiles_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFastForword_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub lstFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFiles.SelectedIndexChanged
        Dim sFile As String
        Dim sExt As String
        Dim dt As New DataTable, dtIndexType As New DataTable
        Dim sFileName As String = ""
        Try
            'BindAnnotaionDetails(Val(lstDocument.SelectedItem.Text), Val(lstFiles.SelectedItem.Text))
            If lstFiles.Items.Count = -1 Then Exit Sub
            lblDocID.Text = lstFiles.SelectedItem.Text
            sFile = objclsView.GetPageFromEdict(sSession.AccessCode, lstFiles.SelectedItem.Text, sSession.UserID)
            sImgFilePath = sFile
            If Trim(sFile.Length) = 0 Then Exit Sub
            sExt = Path.GetExtension(sFile)
            sExt = sExt.Remove(0, 1)
            sFileName = objclsSearch.GetFileNames(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
            lblFileName.Text = sFileName
            'lnkOpenDocument.Text = sFileName
            lblOpenDocument.Text = sFileName
            lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = True
            imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
            Select Case UCase(sExt)
                Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                    lblOpenDocument.Visible = False : lblFileName.Visible = True
                    'lnkOpenDocument.Visible = False : lnkOpenDocument.Visible = False
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                    'documentViewer.Document = sFile
                    gtViewer(sFile, 1)
                    'If sFormButtons.Contains(",Annotation") = True Then
                    '    imgbtnAnnotation.Visible = False
                    '    lblHVersion.Visible = False
                    '    ddlAnnotationVersion.Visible = False
                    'End If
                    'If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    '    imgbtnAnnotation.Visible = False
                    '    lblHVersion.Visible = False
                    '    ddlAnnotationVersion.Visible = False
                    'End If
                    'If sFormButtons.Contains(",Download,") = True Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    '    imgbtnAnnotation.Visible = False
                    '    lblHVersion.Visible = False
                    '    ddlAnnotationVersion.Visible = False
                    'Else
                    '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                    'End If
                    'lblHVersion.Visible = True : imgbtnAnnotation.Enabled = True : ddlAnnotationVersion.Enabled = True
                    'imgbtnAnnotation.Visible = True : ddlAnnotationVersion.Visible = False
                    lblFileType.Text = sExt
                    Dim fi As New IO.FileInfo(sFile)
                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                    dgIndex.DataBind()

                    'lblHVersion.Visible = True : imgbtnAnnotation.Enabled = True : ddlAnnotationVersion.Enabled = True
                    'imgbtnAnnotation.Visible = True : ddlAnnotationVersion.Visible = False
                    Checkin.Visible = False
                   ' LooadVersionInfo()
                Case "PDF", "TIF", "TIFF"
                    lblOpenDocument.Visible = True : lblFileName.Visible = False
                    'lnkOpenDocument.Visible = True : lnkOpenDocument.Visible = True
                    'Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                    'documentViewer.Document = sFile
                    gtViewer(sFile, 2)
                    lblFileType.Text = sExt
                    Dim fi As New IO.FileInfo(sFile)
                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                    dgIndex.DataBind()
                    'If sFormButtons.Contains(",Annotation") = True Then
                    '    imgbtnAnnotation.Visible = False
                    '    lblHVersion.Visible = False
                    '    ddlAnnotationVersion.Visible = False
                    'End If
                    'If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    '    imgbtnAnnotation.Visible = False
                    '    lblHVersion.Visible = False
                    '    ddlAnnotationVersion.Visible = False
                    'End If
                    'If sFormButtons.Contains(",Download,") = True Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    '    imgbtnAnnotation.Visible = False
                    '    lblHVersion.Visible = False
                    '    ddlAnnotationVersion.Visible = False
                    'Else
                    '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                    'End If
                    'lblHVersion.Visible = True : imgbtnAnnotation.Enabled = True : ddlAnnotationVersion.Enabled = True
                    'imgbtnAnnotation.Visible = True : ddlAnnotationVersion.Visible = False
                    Checkin.Visible = True
                   ' LooadVersionInfo()

                Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PDF", "PPS", "XLR", "XML", "TIF", "TIFF", "DWG"
                    lblOpenDocument.Visible = True : lblFileName.Visible = False
                    'lnkOpenDocument.Visible = True : lnkOpenDocument.Visible = True
                    'Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                    'documentViewer.Document = sFile
                    gtViewer(sFile, 3)
                    lblFileType.Text = sExt
                    Dim fi As New IO.FileInfo(sFile)
                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"
                    'If sFormButtons.Contains(",Download,") = True Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                    '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                    '    imgbtnAnnotation.Visible = False
                    '    lblHVersion.Visible = False
                    '    ddlAnnotationVersion.Visible = False
                    'Else
                    '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                    'End If

                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                    dgIndex.DataBind()

                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = True
                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
                    Checkin.Visible = True
                    'LooadVersionInfo()
            End Select

        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstFiles_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lstDocument_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDocument.SelectedIndexChanged
        Dim dtDocument As New DataTable
        Dim i As Integer
        Try
            If lstDocument.Items.Count <> -1 Then
                lstFiles.Items.Clear()
                dtDocument = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                If dtDocument.Rows.Count <> 0 Then
                    For i = 0 To dtDocument.Rows.Count - 1
                        lstFiles.Items.Add(dtDocument.Rows(i)("pge_basename"))
                    Next
                    'txtNav.Text = "1 Of " & lstFiles.Items.Count
                    txtNav.Text = 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                End If
                If lstFiles.Items.Count = 1 Then
                    txtNav.Enabled = False
                End If
                If lstFiles.Items.Count <> 0 Then
                    lstFiles.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDocument_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub lnkOpenDocument_Click(sender As Object, e As EventArgs) Handles lnkOpenDocument.Click
    '    Dim sFile As String, sExt As String, sTempPath As String = "", sDestPath As String = "", sFileName As String = ""
    '    Try
    '        sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")
    '        If sTempPath.EndsWith("\") = True Then
    '            sDestPath = sTempPath & "Temp\Downloads\"
    '        Else
    '            sDestPath = sTempPath & "Temp\Downloads\"
    '        End If
    '        objclsGeneralFunctions.ClearBrowseDirectory(sDestPath)
    '        sFileName = objclsSearch.GetFileNames(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
    '        sDestPath = sDestPath & sFileName
    '        sExt = objclsSearch.GetExtension(sSession.AccessCode, lblDocID.Text)
    '        'To Get Original File Location
    '        sFile = objclsSearch.GetPageFromEdict(sSession.AccessCode, lblDocID.Text, sExt)
    '        System.IO.File.Copy(sFile, sDestPath, True)
    '        DownloadMyFile(sDestPath)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkOpenDocument_Click")
    '    End Try
    'End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Dim str As String = ""
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                str = System.IO.Path.GetFileNameWithoutExtension(file.Name)
                Dim replacestr As String = Regex.Replace(str, "[^a-zA-Z0-9_]+", "")
                Response.AddHeader("Content-Disposition", "attachment; filename=" & replacestr & "." & System.IO.Path.GetExtension(file.Name))
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadMyFile" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim dtDoc As New DataTable
        Dim oImageViewID As New Object, oSelectedIndexID As New Object, oSelectedChecksIDs As New Object, oSelId As Object
        Try
            lblError.Text = ""
            If sSelectedChecksIDs.Length > 0 Then
                dtDoc = objclsSearch.SearchDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSelectedCabID, sSelectedSubCabID, sSelectedFolID, sSelectedDocTypeID, sSelectedKWID, sSelectedDescID, "", "", "", "", sSelectedFrmtID, "", sSelectedCrByID)
                If dtDoc.Rows.Count > 0 Then
                    sSession.dtDocoImageViewID = dtDoc
                    Session("AllSession") = sSession
                    oImageViewID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(2))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(iSelectedIndexID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelId = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelId))
                    Response.Redirect(String.Format("~/DigitalFilling/Search.aspx?ImageViewID={0}&SelectedIndexID={1}&SelectedChecksIDs={2}&SelId={3}&Title={4}", oImageViewID, oSelectedIndexID, oSelectedChecksIDs, oSelId, HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sTitle))), False)
                    Exit Sub
                Else
                    lblError.Text = "No documents found in this Collation."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No documents found in this Collation','', 'info');", True)
                End If
            Else
                lblError.Text = "No documents found in this Collation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No documents found in this Collation','', 'info');", True)
            End If
            Response.Redirect(String.Format("~/DigitalFilling/Search.aspx?ImageViewID={0}", oImageViewID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAnnotaionDetails(ByVal iDocumentID As Integer, ByVal iFileID As Integer)
        Try
            ddlAnnotationVersion.DataSource = objclsSearch.LoadAnnotaionSaved(sSession.AccessCode, sSession.AccessCodeID, iDocumentID, iFileID)
            ddlAnnotationVersion.DataTextField = "EAD_OriginalName"
            ddlAnnotationVersion.DataValueField = "EAD_PKID"
            ddlAnnotationVersion.DataBind()
            ddlAnnotationVersion.Items.Insert(0, "Original File")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAnnotaionDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAnnotation_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAnnotation.Click
        Dim oImgFilePath As New Object, oDocumentID As New Object, oFileID As New Object, oSelectedChecksIDs As Object, oBackToFormID As Object
        Dim oSelectedCabID As Object, oSelectedSubCabID As Object, oSelectedFolID As Object, oSelectedDocTypeID As Object, oSelectedKWID As Object, oSelectedDescID As Object
        Dim oSelectedFrmtID As Object, oSelectedCrByID As Object, oSelectedIndexID As Object, oSelId As Object, oDocumentSelectedID As Object, oFileSelectedID As Object
        Dim sImagePath As String
        Try
            sImagePath = objclsView.GetPageFromEdict(sSession.AccessCode, lstDocument.SelectedItem.Text, sSession.UserID)
            oImgFilePath = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sImgFilePath))
            oDocumentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(lstDocument.SelectedItem.Text))
            oFileID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(lstFiles.SelectedItem.Text))

            oSelectedChecksIDs = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedChecksIDs))
            oSelId = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelId))
            oSelectedCabID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedCabID))
            oSelectedSubCabID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedSubCabID))
            oSelectedFolID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedFolID))
            oSelectedDocTypeID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedDocTypeID))
            oSelectedKWID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedKWID))
            oSelectedDescID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedDescID))
            oSelectedFrmtID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedFrmtID))
            oSelectedCrByID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(sSelectedCrByID))
            oSelectedIndexID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(iSelectedIndexID))

            oDocumentSelectedID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(lstDocument.SelectedIndex))
            oFileSelectedID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(lstFiles.SelectedIndex))

            oBackToFormID = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(1))

            Response.Redirect(String.Format("~/VSAnnotation/VSSearchAnnotation.aspx?SelId={0}&SelectedChecksIDs={1}&SelectedCabID={2}&SelectedSubCabID={3}&SelectedFolID={4}&SelectedDocTypeID={5}&SelectedKWID={6}&SelectedDescID={7}&SelectedFrmtID={8}&SelectedCrByID={9}&SelectedIndexID={10}&ImgFilePath={11}&DocumentID={12}&FileID={13}&DocumentSelectedID={14}&FileSelectedID={15}&BackToFormID={16}", oSelId, oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedDocTypeID, oSelectedKWID, oSelectedDescID, oSelectedFrmtID, oSelectedCrByID, oSelectedIndexID, oImgFilePath, oDocumentID, oFileID, oDocumentSelectedID, oFileSelectedID, oBackToFormID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAnnotation_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAnnotationVersion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAnnotationVersion.SelectedIndexChanged
        Dim sFile As String
        Dim sExt As String
        Dim sFileName As String = ""
        Try
            If ddlAnnotationVersion.SelectedIndex = 0 Then
                lstFiles_SelectedIndexChanged(sender, e)
            Else
                sFile = objclsSearch.GetAnnotaionPageFromEdict(sSession.AccessCode, ddlAnnotationVersion.SelectedValue)
                sImgFilePath = sFile
                sExt = Path.GetExtension(sFile)
                sExt = sExt.Remove(0, 1)
                sFileName = objclsSearch.GetFileNames(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
                lblFileName.Text = sFileName
                'lnkOpenDocument.Text = sFileName
                lblOpenDocument.Text = sFileName
                Select Case UCase(sExt)
                    Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                        lblOpenDocument.Visible = False : lblFileName.Visible = True
                        'lnkOpenDocument.Visible = False : lnkOpenDocument.Visible = FalseddlAnnotationVersion.Visible = 
                        Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
                        Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                        Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                        'documentViewer.Document = sFile
                        gtViewer(sFile, 1)
                        'If sFormButtons.Contains(",Annotation,") = True Then
                        '    imgbtnAnnotation.Visible = False
                        '    lblHVersion.Visible = False
                        '    ddlAnnotationVersion.Visible = False
                        'End If
                        'If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then  'Vijeth modified on 21/01/2020
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        '    imgbtnAnnotation.Visible = False
                        '    lblHVersion.Visible = False
                        '    ddlAnnotationVersion.Visible = False
                        'End If
                        'If sFormButtons.Contains(",Download,") = True Then
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        '    imgbtnAnnotation.Visible = False
                        '    lblHVersion.Visible = False
                        '    ddlAnnotationVersion.Visible = False
                        'Else
                        '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                        'End If
                        'lblHVersion.Visible = True : imgbtnAnnotation.Enabled = True : ddlAnnotationVersion.Enabled = True
                        'imgbtnAnnotation.Visible = True : ddlAnnotationVersion.Visible = False
                        lblFileType.Text = sExt
                        Dim fi As New IO.FileInfo(sFile)
                        lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                        lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                        lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                        Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                        lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                        dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                        dgIndex.DataBind()

                        lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
                        imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False

                    Case "PDF", "TIF", "TIFF"
                        lblOpenDocument.Visible = True : lblFileName.Visible = False
                        'lnkOpenDocument.Visible = True : lnkOpenDocument.Visible = True
                        'Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                        'documentViewer.Document = sFile
                        gtViewer(sFile, 2)
                        lblFileType.Text = sExt
                        Dim fi As New IO.FileInfo(sFile)
                        lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                        lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                        lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                        Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                        lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                        dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                        dgIndex.DataBind()

                        'If sFormButtons.Contains(",Annotation,") = True Then
                        '    imgbtnAnnotation.Visible = False
                        '    lblHVersion.Visible = False
                        '    ddlAnnotationVersion.Visible = False
                        'End If
                        'If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        '    imgbtnAnnotation.Visible = False
                        '    lblHVersion.Visible = False
                        '    ddlAnnotationVersion.Visible = False
                        'End If
                        'If sFormButtons.Contains(",Download,") = True Then
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        '    imgbtnAnnotation.Visible = False
                        '    lblHVersion.Visible = False
                        '    ddlAnnotationVersion.Visible = False
                        'Else
                        '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                        'End If
                        'lblHVersion.Visible = True : imgbtnAnnotation.Enabled = True : ddlAnnotationVersion.Enabled = True
                        'imgbtnAnnotation.Visible = True : ddlAnnotationVersion.Visible = False

                    Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PDF", "PPS", "XLR", "XML", "TIF", "TIFF"
                        lblOpenDocument.Visible = True : lblFileName.Visible = False
                        'lnkOpenDocument.Visible = True : lnkOpenDocument.Visible = True
                        'Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                        'documentViewer.Document = sFile
                        gtViewer(sFile, 3)
                        lblFileType.Text = sExt
                        Dim fi As New IO.FileInfo(sFile)
                        lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                        'If sFormButtons.Contains(",Download,") = True Then
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                        '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                        '    imgbtnAnnotation.Visible = False
                        '    lblHVersion.Visible = False
                        '    ddlAnnotationVersion.Visible = False
                        'Else
                        '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                        'End If

                        lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                        lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                        Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                        lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                        dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                        dgIndex.DataBind()

                        lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
                        imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
                End Select
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAnnotationVersion_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Protected Sub dgIndex_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgIndex.ItemDataBound
        Dim lblHDescriptor As New Label
        Try
            If e.Item.ItemType = ListItemType.Header Then
                lblHDescriptor = e.Item.FindControl("lblHDescriptor")

                lblHDescriptor.Text = "Index Details : " & lblDoucmentType.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgIndex_ItemDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function zxa(ByVal firstNumber As Integer, ByVal secondNumber As Integer) As Integer
        Dim objclsEdictGen As New clsEDICTGeneral
        Dim sMessage As String
        Try
            objclsEdictGen.DltDecryptFile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
        Catch ex As Exception
            sMessage = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "zxa")
            'Return sMessage
        End Try
    End Function
    Public Sub BindCabinet()
        Dim ds As New DataSet
        Try
            ds = objclsView.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID)
            ddlCabinet.DataSource = ds
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_ID"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select Cabinet")

            ddlMcabinet.DataSource = ds
            ddlMcabinet.DataTextField = "CBN_NAME"
            ddlMcabinet.DataValueField = "CBN_ID"
            ddlMcabinet.DataBind()
            ddlMcabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCabinet" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            'txtNav.Text = "" : lblNav.Text = "" : lblFileName.Text = ""

            If ddlCabinet.SelectedIndex > 0 Then
                BindexistingSubCab()
                'ddlSubCabinet_SelectedIndexChanged(sender, e)
            Else
                ddlSubCabinet.Items.Clear()
            End If
            If Request.QueryString("SubCabID") IsNot Nothing Then
                BindexistingSubCab()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindexistingSubCab()
        Dim ds As New DataSet
        Try
            ds = objclsFolders.LoadSubCab(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue)
            ddlSubCabinet.DataSource = ds
            ddlSubCabinet.DataTextField = "CBN_NAME"
            ddlSubCabinet.DataValueField = "CBN_ID"
            ddlSubCabinet.DataBind()
            ddlSubCabinet.Items.Insert(0, "Select Sub Cabinet")

            ddlMSubcabinet.DataSource = ds
            ddlMSubcabinet.DataTextField = "CBN_NAME"
            ddlMSubcabinet.DataValueField = "CBN_ID"
            ddlMSubcabinet.DataBind()
            ddlMSubcabinet.Items.Insert(0, "Select Sub Cabinet")

            If Request.QueryString("Details") IsNot Nothing Then
                Dim sStr As String = HttpUtility.UrlDecode(Request.QueryString("Details"))
                Dim sArray As Array = sStr.Split("|")
                ddlSubCabinet.SelectedValue = sArray(1)
                ddlMSubcabinet.SelectedValue = sArray(1)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindexistingSubCab" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlSubCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCabinet.SelectedIndexChanged
        Try
            If ddlSubCabinet.SelectedIndex > 0 Then
                BindexistingFolder()
            Else
                ddlFolder.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubCabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindexistingFolder()
        Dim ds As New DataSet
        Try
            ds = objclsView.LoadExistingFolder(sSession.AccessCode, sSession.AccessCodeID, ddlSubCabinet.SelectedValue)
            ddlFolder.DataSource = ds
            ddlFolder.DataTextField = "FOL_Name"
            ddlFolder.DataValueField = "FOL_FolID"
            ddlFolder.DataBind()
            ddlFolder.Items.Insert(0, "Select Folder")

            ddlMFolder.DataSource = ds
            ddlMFolder.DataTextField = "FOL_Name"
            ddlMFolder.DataValueField = "FOL_FolID"
            ddlMFolder.DataBind()
            ddlMFolder.Items.Insert(0, "Select Folder")

            If Request.QueryString("Details") IsNot Nothing Then
                Dim sStr As String = HttpUtility.UrlDecode(Request.QueryString("Details"))
                Dim sArray As Array = sStr.Split("|")
                ddlFolder.SelectedValue = sArray(2)
                ddlMFolder.SelectedValue = sArray(2)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindexistingFolder" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFolder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFolder.SelectedIndexChanged
        Dim lblDetID As New Label
        Dim sImagePath As String = ""
        Dim BaseID As Integer, i As Integer = 0
        Dim FileSelectedID As String
        Dim oImagePath As Object
        Dim oSelectedDocTypeID As Object
        Dim dt As New DataTable
        Dim aSelectedChecksIDs() As String
        Dim sRFID As String = ""
        Try
            If Request.QueryString("Details") IsNot Nothing Then
                Dim sStr As String = HttpUtility.UrlDecode(Request.QueryString("Details"))
                Dim sArray As Array = sStr.Split("|")
                ddlFolder.SelectedValue = sArray(2)
                ddlMFolder.SelectedValue = sArray(2)
                sRFID = sArray(4)
            End If
            'Clear
            txtNav.Text = "" : lblNav.Text = "" : lblFileName.Text = ""
            lstDocument.Items.Clear()
            lstFiles.Items.Clear()
            sSelId = String.Empty : sSelectedChecksIDs = String.Empty
            sSelectedCabID = String.Empty : sSelectedSubCabID = String.Empty : sSelectedFolID = String.Empty
            sSelectedDocTypeID = String.Empty : sSelectedKWID = String.Empty : sSelectedDescID = String.Empty
            sSelectedFrmtID = String.Empty : sSelectedCrByID = String.Empty
            iSelectedIndexID = 0 : iSelectedFirstID = 0
            sDetailsId = ""
            If (ddlFolder.SelectedIndex > 0) Then
                lblError.Text = ""
                dt = objclsView.LoadBaseIdFromFolder(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue, ddlFolder.SelectedValue, sRFID)
                If (dt.Rows.Count > 0) Then
                    BaseID = dt.Rows(0).Item("PGE_BASENAME")
                    FileSelectedID = dt.Rows(0).Item("PGE_BASENAME")
                    sSelectedDocTypeID = dt.Rows(0).Item("PGE_DOCUMENT_TYPE")
                    For i = 0 To dt.Rows.Count - 1
                        sDetailsId = sDetailsId & "," & dt.Rows(i).Item("PGE_BASENAME")
                        If (sDetailsId.Length > 0) Then
                            If (sDetailsId.Chars(0).ToString = ",") Then
                                sDetailsId = sDetailsId.Remove(0, 1)
                            End If
                        End If
                    Next
                End If
                sSelectedChecksIDs = sDetailsId
                If Not sSelectedChecksIDs Is Nothing Then
                    If (sSelectedChecksIDs.Length > 0) Then
                        If (sSelectedChecksIDs.Chars(0).ToString = ",") Then
                            sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                        End If
                        aSelectedChecksIDs = sSelectedChecksIDs.Split(",")
                        If aSelectedChecksIDs.Length > 0 Then
                            iSelectedFirstID = aSelectedChecksIDs(0)
                        End If
                    End If
                End If
                oImagePath = objclsView.GetPageFromEdict(sSession.AccessCode, BaseID, sSession.UserID)
                oImagePath = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(oImagePath))
                sSelId = sSelId
                sSelectedCabID = ddlCabinet.SelectedValue
                sSelectedSubCabID = ddlSubCabinet.SelectedValue
                sSelectedFolID = ddlFolder.SelectedValue
                oSelectedDocTypeID = sSelectedDocTypeID
                sSelectedKWID = sSelectedKWID
                sSelectedDescID = sSelectedDescID
                sSelectedFrmtID = sSelectedFrmtID
                sSelectedCrByID = sSelectedCrByID
                If oImagePath IsNot Nothing Then
                    sImgFilePath = objclsGRACeGeneral.DecryptQueryString(oImagePath)
                End If
                If Request.QueryString("SelectedIndexID") IsNot Nothing Then
                    iSelectedIndexID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedIndexID")))
                End If
                If Request.QueryString("SelId") IsNot Nothing Then
                    sSelId = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelId")))
                End If
                If Request.QueryString("FileSelectedID") IsNot Nothing Then
                    sSelId = HttpUtility.UrlDecode(Request.QueryString("FileSelectedID"))
                End If
                'Files Details
                txtID.Text = "0" : txtPreId.Text = "1" : iPageNext = 0
                sBaseName = sSelectedChecksIDs.Split(",")
                For i = 0 To sBaseName.Length - 1
                    lstDocument.Items.Add(sBaseName(i))
                Next
                txtNavDoc.Text = 1
                lblNavDoc.Text = "/" & lstDocument.Items.Count
                If lstDocument.Items.Count = 1 Then
                    txtNavDoc.Enabled = False
                End If
                sSelectedDocTypeID = 0
                FileSelectedID = 0
                Dim iDocSelectedID As Integer = 0, iFileSelectedID As Integer = 0
                If lstDocument.Items.Count <> 0 Then
                    If sSelectedDocTypeID IsNot Nothing Then
                        Try
                            iDocSelectedID = 0
                            txtNavDoc.Text = iDocSelectedID + 1
                        Catch ex As Exception
                        End Try
                    End If
                    lstDocument.SelectedIndex = iDocSelectedID
                    lstDocument_SelectedIndexChanged(sender, e)
                End If
                If lstFiles.Items.Count <> 0 Then
                    If FileSelectedID IsNot Nothing Then
                        Try
                            iFileSelectedID = 0
                            txtNav.Text = iFileSelectedID + 1 'rakchange +1
                            iPageNext = iFileSelectedID
                        Catch ex As Exception
                        End Try
                    End If
                    lstFiles.SelectedIndex = iFileSelectedID
                    lstFiles_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("AnnotaionVersion") IsNot Nothing Then
                    If objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AnnotaionVersion"))) = "YES" Then
                        ddlAnnotationVersion.SelectedIndex = ddlAnnotationVersion.Items.Count - 1
                        ddlAnnotationVersion_SelectedIndexChanged(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFolder_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            If ddlCabinet.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Cabinet.','', 'info');", True)
                Exit Sub
            End If

            If ddlSubCabinet.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Sub Cabinet.','', 'info');", True)
                Exit Sub
            End If

            If ddlFolder.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Folder.','', 'info');", True)
                Exit Sub
            End If


            ddlMcabinet.SelectedValue = ddlCabinet.SelectedValue
            ddlMSubcabinet.SelectedValue = ddlSubCabinet.SelectedValue
            ddlMFolder.SelectedValue = ddlFolder.SelectedValue

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAddImage').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Dim sTempPath As String = "", sFileName As String = ""
        Dim dt As New DataTable
        Dim sIPath As String = ""
        Dim sOPath As String = ""
        Dim fileExtension As String
        dt.Columns.Add("ID")
        dt.Columns.Add("FilePath")
        dt.Columns.Add("FileName")
        Try
            lblError.Text = "" : iDocID = 0
            Dim hfc As HttpFileCollection = Request.Files
            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()

                        sFilesNames = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                        fileExtension = Path.GetExtension(hpf.FileName)
                        sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")
                        If sTempPath.EndsWith("\") = True Then
                            sTempPath = sTempPath & "Temp\Upload\"
                        Else
                            sTempPath = sTempPath & "Temp\Upload\"
                        End If
                        If dt.Rows.Count = 0 Then
                            objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sTempPath)


                            hpf.SaveAs(sTempPath & sFilesNames & "_ed" & fileExtension)

                            sIPath = sTempPath & sFilesNames & "_ed" & fileExtension
                            sOPath = sTempPath & sFilesNames & fileExtension

                            objclsEDICTGeneral.FileEn(sIPath, sOPath)
                            sFilesNames = sFilesNames & fileExtension

                            dRow("ID") = dt.Rows.Count + 1
                            dRow("FilePath") = sTempPath & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName) & System.IO.Path.GetExtension(hpf.FileName)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "ID Desc"
                            dt = dvAttach.ToTable
                        ElseIf dt.Rows.Count > 0 Then
                            hpf.SaveAs(sTempPath & sFilesNames & "_ed" & fileExtension)

                            sIPath = sTempPath & sFilesNames & "_ed" & fileExtension
                            sOPath = sTempPath & sFilesNames & fileExtension

                            objclsEDICTGeneral.FileEn(sIPath, sOPath)
                            sFilesNames = sFilesNames & fileExtension
                            dRow = dt.NewRow()
                            dRow("ID") = dt.Rows.Count + 1
                            dRow("FilePath") = sTempPath & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName) & System.IO.Path.GetExtension(hpf.FileName)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "ID Desc"
                            dt = dvAttach.ToTable
                        End If
                    End If
                Next
            End If
            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAddImage').modal('show');", True)
            gvattach.DataSource = dt
            gvattach.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAddImage').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAttch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Dim dt As New DataTable, dtKey As New DataTable
        Try
            gvDocumentType.DataSource = Nothing
            gvDocumentType.DataBind()
            gvKeywords.DataSource = Nothing
            gvKeywords.DataBind()
            If ddlType.SelectedIndex > 0 Then
                dt = objIndex.LoadDescriptorsForIndexing(sSession.AccessCode, sSession.AccessCodeID, ddlType.SelectedValue)
                gvDocumentType.DataSource = dt
                gvDocumentType.DataBind()
                dtKey = objIndex.LoadKeyWords()
                gvKeywords.DataSource = dtKey
                gvKeywords.DataBind()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAddImage').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlType_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnIndexSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnIndexSave.Click
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        Dim iSize As Integer = 0
        Dim lblMandatory As New Label, lblValidator As New Label, lblSize As New Label, lblDataType As New Label, lblDescriptor As New Label
        Dim sSearch As String, sSearchNumber As String, sDescriptor As String
        Dim dateVal As Date
        Try
            lblError.Text = "" : lblModelError.Text = ""

            'If ddlDocumentTypeId.SelectedIndex = 0 Then
            '    lblModelError.Text = "Select Document type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Document type.','', 'info');", True)
            '    ddlDocumentTypeId.Focus()
            '    Exit Sub
            'Else
            '    icabinetID = ddlDocumentTypeId.SelectedValue
            'End If
            If ddlMcabinet.SelectedIndex = 0 Then
                lblModelError.Text = "Select Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Cabinet.','', 'info');", True)
                ddlMcabinet.Focus()
                Exit Sub
            Else
                icabinetID = ddlMcabinet.SelectedValue
            End If
            If ddlMSubcabinet.SelectedIndex = 0 Then
                lblModelError.Text = "Select Sub Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Sub Cabinet.','', 'info');", True)
                ddlMSubcabinet.Focus()
                Exit Sub
            Else
                iSubCabinet = ddlMSubcabinet.SelectedValue
            End If

            If ddlMFolder.SelectedIndex = 0 Then
                lblModelError.Text = "Select Folder."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Folder.','', 'info');", True)
                ddlMFolder.Focus()
                Exit Sub
            Else
                iFolder = ddlMFolder.SelectedValue
            End If

            If ddlType.SelectedIndex = 0 Then
                lblModelError.Text = "Select Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Type.','', 'info');", True)
                ddlType.Focus()
                Exit Sub
            Else
                iType = ddlType.SelectedValue
            End If
            For iRowCount = 0 To gvDocumentType.Rows.Count - 1
                lblDescriptor = gvDocumentType.Rows(iRowCount).FindControl("lblDescriptor")
                lblMandatory = gvDocumentType.Rows(iRowCount).FindControl("lblMandatory")
                lblValidator = gvDocumentType.Rows(iRowCount).FindControl("lblValidator")
                lblSize = gvDocumentType.Rows(iRowCount).FindControl("lblSize")
                lblDataType = gvDocumentType.Rows(iRowCount).FindControl("lblDataType")
                txtValues = gvDocumentType.Rows(iRowCount).FindControl("txtValues")
                If lblMandatory.Text = "Y" Then
                    If txtValues.Text = "" Then
                        lblError.Text = "Enter values for " & lblDescriptor.Text & "." : lblModelError.Text = "Enter Values for " & lblDescriptor.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter values for " & lblDescriptor.Text & "','', 'info');", True)
                        txtValues.Focus()
                        Exit Sub
                    End If
                End If
                sSearch = "Date"
                sSearchNumber = "Number"
                sDescriptor = lblDataType.Text
                If txtValues.Text <> "" Then
                    If (sDescriptor.IndexOf(sSearch) <> -1) Then
                        If Date.TryParseExact(txtValues.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.None, dateVal) = False Then
                            lblModelError.Text = "Enter valid values for " & lblDescriptor.Text & "."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid values for " & lblDescriptor.Text & ".','', 'info');", True)
                            txtValues.Focus()
                            Exit Sub
                        End If
                    ElseIf (sDescriptor.IndexOf(sSearchNumber) <> -1) Then
                        If IsNumeric(txtValues.Text) = False Then
                            lblModelError.Text = "Enter valid numberic values for " & lblDescriptor.Text & "."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid numberic values for " & lblDescriptor.Text & ".','', 'info');", True)
                            txtValues.Focus()
                            Exit Sub
                        End If
                    End If
                    If lblValidator.Text = "Y" Then
                        If txtValues.Text.Length > lblSize.Text Then
                            lblModelError.Text = "Value for " & lblDescriptor.Text & " Exceeded maximum size (" & lblSize.Text & ")."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Value for " & lblDescriptor.Text & " Exceeded maximum size (" & lblSize.Text & ").','', 'error');", True)
                            txtValues.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            Next

            If gvattach.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Attach the Files','', 'warning');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAddImage').modal('show');", True)
                Exit Sub
            End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvKeywords.Rows.Count > 0 Then
                    For k = 0 To gvKeywords.Rows.Count - 1
                        txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                        If txtKeywords.Text <> "" Then
                            sKeywords = sKeywords & "," & txtKeywords.Text
                        End If
                    Next
                End If
                If sKeywords.StartsWith(",") = True Then
                    sKeywords = sKeywords.Remove(0, 1)
                End If
                If sKeywords.EndsWith(",") = True Then
                    sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                End If
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = System.IO.Path.GetExtension(lblPath.Text)
                            If sPageExt.Contains(".") = True Then
                                sPageExt = sPageExt.Remove(0, 1)
                            End If
                            sFilePath = lblPath.Text
                            sFileName = System.IO.Path.GetFileName(lblPath.Text)
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objclsGRACeGeneral.SafeSQL(txtTitle.Text.Trim)
                            objIndex.dPGEDATE = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            objIndex.sPGEKeyWORD = objclsGRACeGeneral.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            'iSize = Convert.ToInt32(sFilePath.Length)
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID
                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objclsGRACeGeneral.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                            'Delete the Files
                            If File.Exists(sFilePath) = True Then
                                System.IO.File.Delete(sFilePath)
                            End If
                        End If
                    Next
                    If Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved." : lblModelError.Text = "Successfully Saved."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved','', 'success');", True)
                        ddlFolder_SelectedIndexChanged(sender, e)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAddImage').modal('show');", True)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnIndexSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode, sSession.AccessCodeID)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sImagePath)
                sExt = objclsEDICTGeneral.ChangeExt(sExt)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "FilePageInEdict" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkSelect As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            If gvattach.Rows.Count > 0 Then
                chkAll = CType(sender, CheckBox)
                If chkAll.Checked = True Then
                    For iIndx = 0 To gvattach.Rows.Count - 1
                        chkSelect = gvattach.Rows(iIndx).FindControl("chkSelect")
                        chkSelect.Checked = True
                    Next
                Else
                    For iIndx = 0 To gvattach.Rows.Count - 1
                        chkSelect = gvattach.Rows(iIndx).FindControl("chkSelect")
                        chkSelect.Checked = False
                    Next
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAddImage').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindDocumentType()
        Dim dt As New DataTable
        Try
            dt = objIndex.LoadDocumentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlType.DataSource = dt
            ddlType.DataTextField = "DOT_DOCNAME"
            ddlType.DataValueField = "DOT_DOCTYPEID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, "Select Document Type")

            ddlDocumentTypeId.DataSource = dt
            ddlDocumentTypeId.DataTextField = "DOT_DOCNAME"
            ddlDocumentTypeId.DataValueField = "DOT_DOCTYPEID"
            ddlDocumentTypeId.DataBind()
            ddlDocumentTypeId.Items.Insert(0, "Select Document Type")

            If Request.QueryString("Details") IsNot Nothing Then
                Dim sStr As String = HttpUtility.UrlDecode(Request.QueryString("Details"))
                Dim sArray As Array = sStr.Split("|")
                ddlType.SelectedValue = sArray(3)
                ddlDocumentTypeId.SelectedValue = sArray(3)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDocumentType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub gtViewer(ByVal sOrginalFilePath As String, ByVal iType As Integer)

    '    Dim sTempFilePath As String, sUploadedImgPath As String, sDisplayPath As String
    '    Try
    '        If File.Exists(sOrginalFilePath) = True Then
    '            sTempFilePath = ConfigurationManager.AppSettings("VSPath") & "TempImage/"
    '            If Directory.Exists(sTempFilePath) = False Then
    '                Directory.CreateDirectory(sTempFilePath)
    '            End If
    '            sTempFilePath = sTempFilePath & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '            'Session("VSImagePath") = "TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '            documentiFrameViewer.Visible = False : documentImgViewer.Visible = False
    '            sDisplayPath = ConfigurationManager.AppSettings("DisplyPath") & "TempImage/"
    '            Dim url As String = ""
    '            If iType = 1 Then
    '                documentImgViewer.Visible = True : documentiFrameViewer.Visible = False
    '                url = "~/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath) 'Image Viewer 
    '                ' documentImgViewer.ImageUrl = url

    '                'documentImgViewer.ImageUrl = sDisplayPath & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '                Dim bytes As Byte() = System.IO.File.ReadAllBytes(sOrginalFilePath)
    '                Dim imageBase64Data As String = Convert.ToBase64String(bytes)
    '                Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
    '                documentImgViewer.ImageUrl = imageDataURL
    '                lblFileID.Text = sOrginalFilePath
    '                ' lblVSAnnotaionImagePath.Text = "CustomDownloadDir/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '            ElseIf iType = 2 Then
    '                documentiFrameViewer.Visible = True : documentImgViewer.Visible = False
    '                'Dim iId As Integer = 0
    '                'iId = 0
    '                'url = "~/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath) & "#embedded=true&toolbar=0&navpanes=0"
    '                'documentiFrameViewer.Attributes("src") = url
    '                'documentiFrameViewer.Attributes("src") = sDisplayPath & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath) & "#embedded=true&toolbar=0&navpanes=0"
    '                documentiFrameViewer.Attributes("src") = sOrginalFilePath & "#embedded=true&toolbar=0&navpanes=0"

    '            ElseIf iType = 3 Then
    '                documentImgViewer.Visible = True : documentiFrameViewer.Visible = False
    '                url = String.Format("~/Images/SearchImage/NoImage.jpg")
    '                documentImgViewer.ImageUrl = url
    '                documentiFrameViewer.Visible = True : documentImgViewer.Visible = True
    '                'url = "~/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '                'documentiFrameViewer.Attributes("src") = url
    '                documentiFrameViewer.Attributes("src") = sOrginalFilePath
    '                documentiFrameViewer.Attributes.CssStyle.Add("Height", "530px")
    '                documentiFrameViewer.Attributes.CssStyle.Add("width", "850px")
    '                documentiFrameViewer.Attributes.CssStyle.Add("overflow", "hidden")
    '            End If


    '            If File.Exists(sTempFilePath) = True Then
    '                Try
    '                    File.Delete(sTempFilePath)
    '                    File.Copy(sOrginalFilePath, sTempFilePath)
    '                Catch ex As Exception
    '                End Try
    '            Else
    '                File.Copy(sOrginalFilePath, sTempFilePath)
    '            End If
    '        End If
    '        If IsNothing(Session("VSsession")) = False Then
    '            'If Directory.Exists(Server.MapPath("..") & "\UploadedImageFiles\" & Session("VSsession")) = True Then
    '            '    Dim files() As String = Directory.GetFileSystemEntries(Server.MapPath("..") & "\UploadedImageFiles\" & Session("VSsession"))
    '            sUploadedImgPath = ConfigurationManager.AppSettings("VSPath") & "UploadedImageFiles\" & Session("VSsession")
    '            If Directory.Exists(sUploadedImgPath) = True Then
    '                Dim files() As String = Directory.GetFileSystemEntries(sUploadedImgPath)
    '                For Each element As String In files
    '                    If System.IO.File.Exists(element) = True Then
    '                        Try
    '                            File.Delete(element)
    '                            ' My.Computer.FileSystem.DeleteFile(element)
    '                        Catch ex As Exception
    '                        End Try
    '                    End If
    '                Next
    '            End If
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gtViewer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub

    'Public Sub gtViewer(ByVal sOrginalFilePath As String, ByVal iType As Integer)

    '    Dim sTempFilePath As String, sUploadedImgPath As String, sDisplayPath As String
    '    Dim objSearch As New clsSearch
    '    Try
    '        If File.Exists(sOrginalFilePath) = True Then
    '            Dim sPath = objSearch.GetConfigSettings(sSession.AccessCode, "ImgPath")
    '            sTempFilePath = sPath & "/TempImage/"
    '            If Directory.Exists(sTempFilePath) = False Then
    '                Directory.CreateDirectory(sTempFilePath)
    '            End If
    '            sTempFilePath = sTempFilePath & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '            documentiFrameViewer.Visible = False : documentImgViewer.Visible = False
    '            If File.Exists(sTempFilePath) = True Then
    '                Try
    '                    File.Delete(sTempFilePath)
    '                    File.Copy(sOrginalFilePath, sTempFilePath)
    '                Catch ex As Exception
    '                End Try
    '            Else
    '                File.Copy(sOrginalFilePath, sTempFilePath)
    '            End If

    '            Dim url As String = ""
    '            If iType = 1 Then
    '                documentImgViewer.Visible = True : documentiFrameViewer.Visible = False
    '                url = "~/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath) 'Image Viewer                     
    '                Dim bytes As Byte() = System.IO.File.ReadAllBytes(sOrginalFilePath)
    '                Dim imageBase64Data As String = Convert.ToBase64String(bytes)
    '                Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
    '                documentImgViewer.ImageUrl = imageDataURL
    '                lblFileID.Text = sOrginalFilePath
    '            ElseIf iType = 2 Then
    '                documentiFrameViewer.Visible = True : documentImgViewer.Visible = False
    '                sDisplayPath = objSearch.GetConfigSettings(sSession.AccessCode, "DisplayPath") & "/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '                documentiFrameViewer.Attributes("src") = ResolveUrl(sDisplayPath) & "#embedded=true&toolbar=0&navpanes=0"
    '            ElseIf iType = 3 Then
    '                documentImgViewer.Visible = True : documentiFrameViewer.Visible = False
    '                url = String.Format("~/Images/SearchImage/NoImage.jpg")
    '                documentImgViewer.ImageUrl = url
    '                documentiFrameViewer.Visible = True : documentImgViewer.Visible = True
    '                sDisplayPath = objSearch.GetConfigSettings(sSession.AccessCode, "DisplayPath") & "/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
    '                documentiFrameViewer.Attributes("src") = sDisplayPath
    '                documentiFrameViewer.Attributes.CssStyle.Add("Height", "530px")
    '                documentiFrameViewer.Attributes.CssStyle.Add("width", "850px")
    '                documentiFrameViewer.Attributes.CssStyle.Add("overflow", "hidden")
    '            End If

    '        End If
    '        If IsNothing(Session("VSsession")) = False Then
    '            'If Directory.Exists(Server.MapPath("..") & "\UploadedImageFiles\" & Session("VSsession")) = True Then
    '            '    Dim files() As String = Directory.GetFileSystemEntries(Server.MapPath("..") & "\UploadedImageFiles\" & Session("VSsession"))
    '            sUploadedImgPath = ConfigurationManager.AppSettings("VSPath") & "UploadedImageFiles\" & Session("VSsession")
    '            If Directory.Exists(sUploadedImgPath) = True Then
    '                Dim files() As String = Directory.GetFileSystemEntries(sUploadedImgPath)
    '                For Each element As String In files
    '                    If System.IO.File.Exists(element) = True Then
    '                        Try
    '                            File.Delete(element)
    '                            ' My.Computer.FileSystem.DeleteFile(element)
    '                        Catch ex As Exception
    '                        End Try
    '                    End If
    '                Next
    '            End If
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gtViewer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub

    Public Sub gtViewer(ByVal sOrginalFilePath As String, ByVal iType As Integer)

        Dim sTempFilePath As String, sUploadedImgPath As String, sDisplayPath As String
        Dim objSearch As New clsSearch
        Try
            If File.Exists(sOrginalFilePath) = True Then
                Dim sPath = objSearch.GetConfigSettings(sSession.AccessCode, "ImgPath")
                sTempFilePath = sPath & "/TempImage/"
                If Directory.Exists(sTempFilePath) = False Then
                    Directory.CreateDirectory(sTempFilePath)
                End If
                sTempFilePath = sTempFilePath & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
                documentiFrameViewer.Visible = False : documentImgViewer.Visible = False
                If File.Exists(sTempFilePath) = True Then
                    Try
                        File.Delete(sTempFilePath)
                        File.Copy(sOrginalFilePath, sTempFilePath)
                    Catch ex As Exception
                    End Try
                Else
                    File.Copy(sOrginalFilePath, sTempFilePath)
                End If

                Dim url As String = ""
                If iType = 1 Then
                    documentImgViewer.Visible = True : documentiFrameViewer.Visible = False
                    url = "~/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath) 'Image Viewer                     
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sOrginalFilePath)
                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                    documentImgViewer.ImageUrl = imageDataURL
                    lblFileID.Text = sOrginalFilePath
                ElseIf iType = 2 Then
                    documentiFrameViewer.Visible = True : documentImgViewer.Visible = False
                    sDisplayPath = objSearch.GetConfigSettings(sSession.AccessCode, "DisplayPath") & "/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
                    documentiFrameViewer.Attributes("src") = ResolveUrl(sDisplayPath) & "#embedded=true&toolbar=0&navpanes=0"
                ElseIf iType = 3 Then
                    documentImgViewer.Visible = True : documentiFrameViewer.Visible = False
                    url = String.Format("~/Images/SearchImage/NoImage.jpg")
                    documentImgViewer.ImageUrl = url
                    documentiFrameViewer.Visible = True : documentImgViewer.Visible = True
                    sDisplayPath = objSearch.GetConfigSettings(sSession.AccessCode, "DisplayPath") & "/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sOrginalFilePath)
                    documentiFrameViewer.Attributes("src") = sDisplayPath
                    documentiFrameViewer.Attributes.CssStyle.Add("Height", "530px")
                    documentiFrameViewer.Attributes.CssStyle.Add("width", "850px")
                    documentiFrameViewer.Attributes.CssStyle.Add("overflow", "hidden")
                End If

            End If
            If IsNothing(Session("VSsession")) = False Then
                'If Directory.Exists(Server.MapPath("..") & "\UploadedImageFiles\" & Session("VSsession")) = True Then
                '    Dim files() As String = Directory.GetFileSystemEntries(Server.MapPath("..") & "\UploadedImageFiles\" & Session("VSsession"))
                sUploadedImgPath = ConfigurationManager.AppSettings("VSPath") & "UploadedImageFiles\" & Session("VSsession")
                If Directory.Exists(sUploadedImgPath) = True Then
                    Dim files() As String = Directory.GetFileSystemEntries(sUploadedImgPath)
                    For Each element As String In files
                        If System.IO.File.Exists(element) = True Then
                            Try
                                File.Delete(element)
                                ' My.Computer.FileSystem.DeleteFile(element)
                            Catch ex As Exception
                            End Try
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gtViewer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Protected Sub ddlDocumentTypeId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDocumentTypeId.SelectedIndexChanged
        Dim lblDetID As New Label
        Dim sImagePath As String = ""
        Dim BaseID As Integer, i As Integer = 0
        Dim FileSelectedID As String
        Dim icabinetID As Integer, iSubCabinet As Integer, iFolder As Integer
        Dim oImagePath As Object, oSelectedDocTypeID As Object
        Dim dt As New DataTable
        Dim aSelectedChecksIDs() As String
        Dim sExt As String = ""
        Dim ObjDbGen As New DatabaseLayer.DBHelper
        Dim objSearch As New clsView
        Dim sRFID As String = ""

        sSelId = String.Empty : sSelectedChecksIDs = String.Empty
        sSelectedCabID = String.Empty : sSelectedSubCabID = String.Empty : sSelectedFolID = String.Empty
        sSelectedDocTypeID = String.Empty : sSelectedKWID = String.Empty : sSelectedDescID = String.Empty
        sSelectedFrmtID = String.Empty : sSelectedCrByID = String.Empty : sImgFilePath = String.Empty
        iSelectedIndexID = 0 : iSelectedFirstID = 0 : dgIndex.DataSource = Nothing : lblFileName.Text = String.Empty : lblFileType.Text = String.Empty
        lblSize.Text = String.Empty : lblSizeH.Text = String.Empty : lblCreatedBy.Text = String.Empty : lblCreatedOn.Text = String.Empty
        txtNav.Text = String.Empty : lblNav.Text = String.Empty : lblNavDoc.Text = String.Empty : txtNavDoc.Text = String.Empty
        sDetailsId = ""
        dgIndex.DataSource = Nothing : dgIndex.DataBind()
        Try
            If (ddlDocumentTypeId.SelectedIndex > 0) Then

                If Request.QueryString("Details") IsNot Nothing Then
                    Dim sStr As String = HttpUtility.UrlDecode(Request.QueryString("Details"))
                    Dim sArray As Array = sStr.Split("|")
                    ddlFolder.SelectedValue = sArray(2)
                    ddlMFolder.SelectedValue = sArray(2)
                    sRFID = sArray(4)
                End If

                lstDocument.Items.Clear()
                lstFiles.Items.Clear()

                If ddlCabinet.SelectedIndex = 0 Then
                    lblModelError.Text = "Select Cabinet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Cabinet.','', 'info');", True)
                    ddlMcabinet.Focus()
                    Exit Sub
                Else
                    icabinetID = ddlCabinet.SelectedValue
                End If

                If ddlSubCabinet.SelectedIndex = 0 Then
                    lblModelError.Text = "Select Sub Cabinet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Sub Cabinet.','', 'info');", True)
                    ddlMSubcabinet.Focus()
                    Exit Sub
                Else
                    iSubCabinet = ddlSubCabinet.SelectedValue
                End If

                If ddlFolder.SelectedIndex = 0 Then
                    lblModelError.Text = "Select Folder."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Folder.','', 'info');", True)
                    ddlMFolder.Focus()
                    Exit Sub
                Else
                    iFolder = ddlFolder.SelectedValue
                End If

                lblError.Text = ""

                dt = objclsView.LoadBaseIdFromFolder(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue, ddlFolder.SelectedValue, sRFID)
                If (dt.Rows.Count > 0) Then
                    BaseID = dt.Rows(0).Item("PGE_BASENAME")
                    FileSelectedID = dt.Rows(0).Item("PGE_BASENAME")
                    sSelectedDocTypeID = dt.Rows(0).Item("PGE_DOCUMENT_TYPE")
                    For i = 0 To dt.Rows.Count - 1
                        sDetailsId = sDetailsId & "," & dt.Rows(i).Item("PGE_BASENAME")
                        If (sDetailsId.Length > 0) Then
                            If (sDetailsId.Chars(0).ToString = ",") Then
                                sDetailsId = sDetailsId.Remove(0, 1)
                            End If
                        End If
                    Next

                    sSelectedChecksIDs = sDetailsId
                    If Not sSelectedChecksIDs Is Nothing Then
                        If (sSelectedChecksIDs.Length > 0) Then
                            If (sSelectedChecksIDs.Chars(0).ToString = ",") Then
                                sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                            End If
                            aSelectedChecksIDs = sSelectedChecksIDs.Split(",")
                            If aSelectedChecksIDs.Length > 0 Then
                                iSelectedFirstID = aSelectedChecksIDs(0)
                            End If
                        End If
                    End If

                    oImagePath = objclsView.GetPageFromEdict(sSession.AccessCode, BaseID, sSession.UserID)
                    oImagePath = HttpUtility.UrlDecode(objclsGRACeGeneral.EncryptQueryString(oImagePath))
                    sSelId = sSelId
                    sSelectedCabID = ddlCabinet.SelectedValue
                    sSelectedSubCabID = ddlSubCabinet.SelectedValue
                    sSelectedFolID = ddlFolder.SelectedValue
                    oSelectedDocTypeID = sSelectedDocTypeID
                    sSelectedKWID = sSelectedKWID
                    sSelectedDescID = sSelectedDescID
                    sSelectedFrmtID = sSelectedFrmtID
                    sSelectedCrByID = sSelectedCrByID

                    If oImagePath IsNot Nothing Then
                        sImgFilePath = objclsGRACeGeneral.DecryptQueryString(oImagePath)
                    End If

                    If Request.QueryString("SelectedIndexID") IsNot Nothing Then
                        iSelectedIndexID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedIndexID")))
                    End If

                    If Request.QueryString("SelId") IsNot Nothing Then
                        sSelId = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelId")))
                    End If

                    'Files Details
                    txtID.Text = "0" : txtPreId.Text = "1" : iPageNext = 0
                    sBaseName = sSelectedChecksIDs.Split(",")
                    For i = 0 To sBaseName.Length - 1
                        lstDocument.Items.Add(sBaseName(i))
                    Next

                    'txtNavDoc.Text = "1 Of " & lstDocument.Items.Count
                    txtNavDoc.Text = 1
                    lblNavDoc.Text = "/" & lstDocument.Items.Count
                    If lstDocument.Items.Count = 1 Then
                        txtNavDoc.Enabled = False
                    End If
                    sSelectedDocTypeID = 0
                    FileSelectedID = 0
                    Dim iDocSelectedID As Integer = 0, iFileSelectedID As Integer = 0
                    If lstDocument.Items.Count <> 0 Then
                        If sSelectedDocTypeID IsNot Nothing Then
                            Try
                                iDocSelectedID = 0 'sSelectedDocTypeID 'objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("DocumentSelectedID")))
                                txtNavDoc.Text = iDocSelectedID + 1
                            Catch ex As Exception
                            End Try
                        End If
                        lstDocument.SelectedIndex = iDocSelectedID
                        lstDocument_SelectedIndexChanged(sender, e)
                    End If

                    If lstFiles.Items.Count <> 0 Then
                        If FileSelectedID IsNot Nothing Then
                            Try
                                iFileSelectedID = 0 'objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FileSelectedID")))
                                txtNav.Text = iFileSelectedID 'rakchange +1
                                iPageNext = iFileSelectedID
                            Catch ex As Exception
                            End Try
                        End If
                        lstFiles.SelectedIndex = iFileSelectedID
                        lstFiles_SelectedIndexChanged(sender, e)
                    End If
                    If Request.QueryString("AnnotaionVersion") IsNot Nothing Then
                        If objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AnnotaionVersion"))) = "YES" Then
                            ddlAnnotationVersion.SelectedIndex = ddlAnnotationVersion.Items.Count - 1
                            ddlAnnotationVersion_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDocumentTypeId_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try

    End Sub
    Protected Sub Checkin_CheckedChanged(sender As Object, e As EventArgs) Handles Checkin.CheckedChanged
        Dim objVersion As New clsView
        Dim dt As New DataTable
        Dim sPath As String = "", sVersionTemp As String = ""
        Dim objSearch As New clsSearch
        Dim VrNo As Double
        Dim Vrname As Double
        Dim sTemppath As String
        Try
            If Checkin.Checked = True Then
                Checkin.Text = "Check Out"
                OpenDocument.Visible = True
            ElseIf Checkin.Checked = False Then
                CBVNewVersionInfo.Checked = False
                Checkin.Text = "Check In"
                sTemppath = objSearch.GetImageSettings(sSession.AccessCode, "VersionTemp")
                sVersionTemp = sTemppath & "Temp\"
                txtVRevisedBy.Text = sSession.UserFullName
                txtVRevisedBy.Enabled = False : txtVRevisedOn.Enabled = False : txtVFileName.Enabled = False
                txtVRevisedOn.Text = Convert.ToDateTime(Now.ToString("dd/MM/yyyy"))
                VrNo = objclsView.GetVersionInfo(lblDocID.Text)
                Vrname = objVersion.GetVersionId(lblDocID.Text, VrNo)
                txtVFileName.Text = VrNo & "." & Vrname
                OpenDocument.Visible = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalVersion').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Checkin_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSaveVersion_Click(sender As Object, e As EventArgs) Handles btnSaveVersion.ServerClick
        Dim iParent As Long
        Dim rt As String = "", wt As String = "", sPath As String = "", stpath As String = "", stpath2 As String = "", sOpenFile As String = ""
        Dim sTemppath As String = "", sTemppath2 As String = "", sExt As String = "", sAExt As String = "", ssExt As String = ""

        Dim NVersion As Integer = 0

        Dim spaths As String = ""
        Dim sOpaths As String = ""

        lblError.Text = ""
        VersionError.Text = ""
        Try

            Dim hfc As HttpFileCollection = Request.Files
            Dim hpf As HttpPostedFile = hfc(0)
            If (hfc.Count > 0) Then
                If FileUpload1.FileName <> "" Then

                    sTemppath = objclsSearch.GetImageSettings(sSession.AccessCode, "VersionTemp")
                    If System.IO.Directory.Exists(sTemppath & "Temp") = False Then
                        System.IO.Directory.CreateDirectory(sTemppath & "Temp")
                    End If
                    stpath = sTemppath & "Temp\"
                    sExt = objclsGeneralFunctions.GetFileExt(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
                    sAExt = Path.GetExtension(hpf.FileName)
                    sAExt = sAExt.Remove(0, 1)
                    If sAExt <> sExt Then
                        lblError.Text = "Invalid file."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid file','', 'error');", True)
                        Exit Sub
                    End If
                    spaths = System.IO.Path.GetFileName(hpf.FileName)
                    sOpaths = stpath & lblDocID.Text & "." & sExt
                    stpath = stpath & lblDocID.Text & "_ed" & "." & sExt
                    hpf.SaveAs(stpath)
                    objclsEDICTGeneral.FileEn(stpath, sOpaths)
                    If System.IO.File.Exists(stpath) = True Then
                        File.Delete(stpath)
                    End If
                    sPath = objclsSearch.GetImageSettings(sSession.AccessCode, "VersionTemp")
                    If (Not System.IO.Directory.Exists(sPath & "Versions\" & lblDocID.Text & "")) Then
                        System.IO.Directory.CreateDirectory(sPath & "Versions\" & lblDocID.Text & "")
                    End If
                    If (Not System.IO.Directory.Exists(sPath & "Versions\" & lblDocID.Text & "\" & txtVFileName.Text & "")) Then

                        System.IO.Directory.CreateDirectory(sPath & "Versions\" & lblDocID.Text & "\" & txtVFileName.Text & "")
                    End If
                    sOpenFile = sPath & "Versions\"
                    sTemppath2 = objclsSearch.GetImageSettings(sSession.AccessCode, "VersionTemp")
                    If System.IO.Directory.Exists(sPath & "Temp") = False Then
                        System.IO.Directory.CreateDirectory(sPath & "Temp")
                    End If
                    stpath2 = sTemppath2 & "Temp\" & lblDocID.Text & "." & sExt
                    ssExt = objclsEDICTGeneral.ChangeExt(sExt)
                    rt = sOpenFile & lblDocID.Text & "\" & txtVFileName.Text & "\" & lblDocID.Text & ssExt
                    wt = stpath2
                    System.IO.File.Copy(wt, rt, True)

                    If System.IO.File.Exists(wt) Then
                        File.Delete(wt)
                    End If
                    If CBVNewVersionInfo.Checked = True Then
                        NVersion = objclsView.GetNewVersion(lblDocID.Text)
                    Else
                        NVersion = objclsView.GetVersionInfo(lblDocID.Text)
                    End If

                    objclsView.SaveVersion(lblDocID.Text, 0, txtVRevisedOn.Text, txtVRemarks.Text, txtVFileName.Text, iParent, 0, 0, NVersion)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal(''Succusfully Created The New Version" & txtVFileName.Text & "','', 'success');", True)
                    txtVFileName.Text = "" : txtVRemarks.Text = "" : txtVRevisedBy.Text = "" : txtVRevisedOn.Text = "" : CBVNewVersionInfo.Checked = False
                    Checkin.Text = "Check In"
                    Checkin.Checked = False
                    LooadVersionInfo()
                ElseIf FileUpload1.FileName = "" Then
                    VersionError.Text = "Upload Files"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Upload Files','', 'info');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveVersion_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub OpenDocument_Click(sender As Object, e As EventArgs) Handles OpenDocument.Click
        Dim sFile As String = "", sExt As String = "", sOpenFile As String = "", sPath As String = ""
        lblError.Text = ""
        Try

            If Checkin.Checked = True Then
                sPath = objclsSearch.GetImageSettings(sSession.AccessCode, "ImagePath")
                sExt = objclsGeneralFunctions.GetFileExt(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
                sFile = objclsView.GetPageV(lblDocID.Text, sExt)
                sOpenFile = objclsEDICTGeneral.GetDecPathView(sPath, sSession.UserID, sFile, lblDocID.Text, sExt)
                DownloadVersion(sOpenFile)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "OpenDocument_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LooadVersionInfo()
        Dim dt As DataTable
        Try
            dt = objclsView.DisplayVersion(lstFiles.SelectedItem.Text)
            If dt.Rows.Count > 0 Then
                gvVersionInfo.DataSource = objclsView.DisplayVersion(lstFiles.SelectedItem.Text)
                gvVersionInfo.DataBind()
            Else
                gvVersionInfo.DataSource = Nothing
                gvVersionInfo.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LooadVersionInfo" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub DownloadVersion(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Dim str As String = ""
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            Response.Clear()
            str = System.IO.Path.GetFileNameWithoutExtension(file.Name)
            Dim replacestr As String = Regex.Replace(str, "[^a-zA-Z0-9_]+", "")
            Response.AddHeader("Content-Disposition", "attachment; filename=" & replacestr & "." & System.IO.Path.GetExtension(file.Name))
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(file.FullName)
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadVersion" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvVersionInfo_PreRender(sender As Object, e As EventArgs) Handles gvVersionInfo.PreRender
        Try
            If gvVersionInfo.Rows.Count > 0 Then
                gvVersionInfo.UseAccessibleHeader = True
                gvVersionInfo.HeaderRow.TableSection = TableRowSection.TableHeader
                gvVersionInfo.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvVersionInfo_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvVersionInfo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvVersionInfo.RowCommand
        Dim sExt As String, ssExt As String, sOpath As String, sVersionDir
        Dim sPath As String = ""
        lblError.Text = ""
        Try
            If e.CommandName.Equals("Version") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Dim sVersionDiar As LinkButton = DirectCast(clickedRow.FindControl("lnkVersionInfo"), LinkButton)
                sExt = objclsGeneralFunctions.GetFileExt(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
                sPath = objclsSearch.GetImageSettings(sSession.AccessCode, "VersionTemp")
                If System.IO.Directory.Exists(sPath & "Versions") = False Then
                    System.IO.Directory.CreateDirectory(sPath)
                End If
                ssExt = objclsEDICTGeneral.ChangeExt(sExt)
                sVersionDir = sPath & "Versions\" & lblDocID.Text & "\" & sVersionDiar.Text & "\" & lblDocID.Text & ssExt
                sOpath = objclsView.GetImageSettings(sSession.AccessCode, "ImagePath")

                Dim sTempFilePath As String
                sTempFilePath = ConfigurationManager.AppSettings("VSPath") & "VersionTempImage/"
                If Directory.Exists(sTempFilePath) = False Then
                    Directory.CreateDirectory(sTempFilePath)
                End If

                'sOpath = objclsEDICTGeneral.GetDecPathView(sOpath, sSession.UserID, sVersionDir, lblDocID.Text, sExt)
                sOpath = objclsEDICTGeneral.GetDecPathView(sTempFilePath, sSession.UserID, sVersionDir, lblDocID.Text, sExt)
                'ExtManger(sOpath, sExt)
                '   documentViewer.ImageUrl = "~/VersionTempImage/View/" & sSession.UserID & "/" & Path.GetFileName(sOpath)  ''25_07_22

                Dim url As String = "~/VersionTempImage/View/" & sSession.UserID & "/" & Path.GetFileName(sOpath)
                documentImgViewer.ImageUrl = url


                'If sFormButtons.Contains(",Download,") = True Then
                '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                'ElseIf sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '    documentViewer.AllowedPermissions = DocumentViewerPermissions.All
                '    imgbtnAnnotation.Visible = False
                '    lblHVersion.Visible = False
                '    ddlAnnotationVersion.Visible = False
                'Else
                '    documentViewer.DeniedPermissions = DocumentViewerPermissions.Print Or DocumentViewerPermissions.DownloadAsPdf Or DocumentViewerPermissions.Download
                'End If
                'gtViewer(sOpath)
                'DownloadMyFile("~/VersionTempImage/View/" & sSession.UserID & "/" & Path.GetFileName(sOpath))
            End If
            If e.CommandName.Equals("RemoveRow") Then
                lblRemoveValidationMsg.Text = "Do you want to Remove Permanently"  'Vijeth
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgLinkType').addClass('alert alert-success');$('#ModalRemoveValidation').modal('show');", True)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvVersionInfo_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub DownloadAndPrint(ByVal sender As Object, ByVal e As EventArgs)
        Dim sFile As String
        Try

            objclsGeneralFunctions.SaveViewAndDownloadLogs(sSession.AccessCode, sSession.AccessCodeID, "Download", lblDocID.Text, 0, sSession.UserID, sSession.IPAddress)  'Vijeth
            sFile = objclsView.GetPageFromEdict(sSession.AccessCode, lblDocID.Text, sSession.UserID)
            gtViewer(sFile, 1)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadAndPrint" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub btnYesMsgOk_Click(sender As Object, e As EventArgs) Handles btnYesMsgOk.Click
        Dim lblVersionID As Label
        Dim i As Integer = 0
        Dim sFile As String
        Try
            lblVersionID = gvVersionInfo.Rows(i).FindControl("lblVersionID")

            objclsView.PermanetDeleteVersion(sSession.AccessCode, sSession.AccessCodeID, Val(lblVersionID.Text))
            sFile = objclsView.GetPageFromEdict(sSession.AccessCode, lblDocID.Text, sSession.UserID)
            gtViewer(sFile, 1)
            LooadVersionInfo()
            lblError.Text = "Successfully Deleted."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Deleted','', 'success');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnYesMsgOk_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Dim sFile As String
        Try
            lblError.Text = ""
            sFile = objclsView.GetPageFromEdict(sSession.AccessCode, lblDocID.Text, sSession.UserID)
            gtViewer(sFile, 1)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgLinkType').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNo_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
