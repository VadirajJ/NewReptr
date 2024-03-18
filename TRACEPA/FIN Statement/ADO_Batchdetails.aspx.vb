
Imports System.Data
    Imports BusinesLayer
    Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports DatabaseLayer
    Imports System

Partial Class ADO_Batchdetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "ADO_Batchdetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsDigitalFilingDashboard As New clsDigitalFilingDashboard
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAttachments As New clsAttachments
    Private objIndex As New clsIndexing
    Private objclsEdictGeneral As New clsEDICTGeneral
    Private objclsDataCapture As New ClsDataCapture
    Private objclsSearch As New clsSearch
    Private objclsADOBatch As New clsADOBatch
    Private sSession As AllSession
    Private Shared sPageDetails As New DataTable
    Private Shared sAttachdetails As New DataTable
    Private Shared iAttachID As Integer
    ' Private Shared iTempAttachID As Integer
    Dim dtColumns As New DataTable
    Private objclsView As New clsView

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnNavDocFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNavDoc.ImageUrl = "~/Images/SearchImage/Previous16.png"
        imgbtnNextNavDoc.ImageUrl = "~/Images/SearchImage/Next16.png"
        imgbtnNavDocFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNav.ImageUrl = "~/Images/SearchImage/Preview16.png"
        imgbtnNextNav.ImageUrl = "~/Images/SearchImage/Nextt16.png"
        imgbtnFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                Session("Attachment") = Nothing
                dtColumns.Columns.Add("AtchID")
                dtColumns.Columns.Add("FilePath")
                dtColumns.Columns.Add("FileName")
                dtColumns.Columns.Add("Extension")
                dtColumns.Columns.Add("CreatedBy")
                dtColumns.Columns.Add("CreatedOn")
                Session("Attachment") = dtColumns
                iAttachID = 0
                sPageDetails.Clear() : sAttachdetails.Clear()
                liVoucherReciept.Attributes.Add("class", "active") : divVoucherReciept.Attributes.Add("class", "tab-pane active")
                BindCabinet()
                If Request.QueryString("AttachID") IsNot Nothing Then
                    Try
                        iAttachID = objclsEdictGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AttachID")))
                        'iTempAttachID = iAttachID
                        gtBatchfile(Val(iAttachID))
                        ddlcabinet.SelectedValue = sAttachdetails.Rows(0)("pge_cabinet")
                        BindTransactiontype()
                        ddlTransactiontype.SelectedValue = sAttachdetails.Rows(0)("pge_Subcabinet")
                        ddlTransactiontype_SelectedIndexChanged(sender, e)
                        ddlBatch.SelectedValue = sAttachdetails.Rows(0)("pge_folder")
                    Catch ex As Exception
                        iAttachID = HttpUtility.UrlDecode(Request.QueryString("AttachID"))
                    End Try
                End If

            End If
        Catch ex As Exception
            'lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            'Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub gtBatchfile(ByVal iSelectedIndexID As Integer)
        Try
            If sPageDetails.Rows.Count = 0 Then
                sPageDetails = objclsADOBatch.GetPageDetails(sSession.AccessCode, sSession.AccessCodeID, iSelectedIndexID)
                txtNavDoc.Text = 1
                lblNavDoc.Text = "/ " & sPageDetails.Rows.Count
                Try
                    gtViewer(sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))
                Catch ex As Exception
                End Try
            Else
                Try
                    gtViewer(sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))
                Catch ex As Exception
                End Try

            End If
            Try
                GetCall(sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))
            Catch ex As Exception
            End Try

            Try
                sAttachdetails = objclsADOBatch.GetAttachdetails(sSession.AccessCode, sSession.AccessCodeID, sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))
                If sAttachdetails.Rows.Count = 0 Then
                    txtNav.Text = 0
                    lblNav.Text = "/ " & 0
                Else
                    txtNav.Text = 1
                    lblNav.Text = "/ " & sAttachdetails.Rows.Count
                End If
            Catch ex As Exception
            End Try

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Vintaviewer")
        End Try

    End Sub
    Public Sub gtViewer(ByVal iSelectedIndexID As Integer)
        Dim sTempFilePath As String, sImagePath As String = "", sDisplayPath As String
        Dim objSearch As New clsSearch
        Try
            sImagePath = objclsView.GetPageFromEdict(sSession.AccessCode, iSelectedIndexID, sSession.UserID)
            If File.Exists(sImagePath) = True Then
                Dim sPath = objSearch.GetConfigSettings(sSession.AccessCode, "ImgPath")
                sTempFilePath = sPath & "/TempImage/"
                If Directory.Exists(sTempFilePath) = False Then
                    Directory.CreateDirectory(sTempFilePath)
                End If
                sTempFilePath = sTempFilePath & sSession.UserID & "_" & Path.GetFileName(sImagePath)
                documentViewer.Visible = False : documentViewer.Visible = False
                If File.Exists(sTempFilePath) = True Then
                    Try
                        File.Delete(sTempFilePath)
                        File.Copy(sImagePath, sTempFilePath)
                    Catch ex As Exception
                    End Try
                Else
                    File.Copy(sImagePath, sTempFilePath)
                End If

                documentViewer.Visible = True
                sDisplayPath = objSearch.GetConfigSettings(sSession.AccessCode, "DisplayPath") & "/TempImage/" & Path.GetFileName(sTempFilePath)
                documentViewer.Attributes("src") = ResolveUrl(sDisplayPath) & "#embedded=true&toolbar=0&navpanes=0"

                'Dim bytes As Byte() = System.IO.File.ReadAllBytes(sTempFilePath)
                'Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                'Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                'documentViewer.ImageUrl = imageDataURL1
            End If

            'If File.Exists(sImagePath) = True Then
            '    sTempFilePath = ConfigurationManager.AppSettings("VSPath") & "TempImage\"
            '    If Directory.Exists(sTempFilePath) = False Then
            '        Directory.CreateDirectory(sTempFilePath)
            '    End If
            '    sTempFilePath = sTempFilePath & sSession.UserID & "_" & Path.GetFileName(sImagePath)
            '    sDisplayPath = ConfigurationManager.AppSettings("DisplyPath") & "TempImage/"
            '    Dim url As String = ""
            '    url = "~/TempImage/" & sSession.UserID & "_" & Path.GetFileName(sImagePath) 'Image Viewer                
            '    documentViewer.ImageUrl = sDisplayPath & sSession.UserID & "_" & Path.GetFileName(sImagePath)

            '    If File.Exists(sTempFilePath) = True Then
            '        Try
            '            File.Delete(sTempFilePath)
            '            File.Copy(sImagePath, sTempFilePath)
            '        Catch ex As Exception
            '        End Try
            '    Else
            '        File.Copy(sImagePath, sTempFilePath)
            '    End If

            '    'Dim bytes As Byte() = System.IO.File.ReadAllBytes(sTempFilePath)
            '    'Dim imageBase64Data As String = Convert.ToBase64String(bytes)
            '    'Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
            '    'documentViewer.ImageUrl = imageDataURL1

            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Vintaviewer")
        End Try
    End Sub
    Private Sub lnkVoucherReciept2_Click(sender As Object, e As EventArgs) Handles lnkVoucherReciept2.Click
        Try
            lblError.Text = "" : lblTab.Text = 2
            liVoucherReciept.Attributes.Remove("class")
            liVoucherReciept2.Attributes.Add("class", "active")

            divVoucherReciept.Attributes.Add("class", "tab-pane")
            divVoucherReciept2.Attributes.Add("class", "tab-pane active")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkVoucherReciept_Click(sender As Object, e As EventArgs) Handles lnkVoucherReciept.Click
        Try
            lblError.Text = "" : lblTab.Text = 2
            liVoucherReciept2.Attributes.Remove("class")
            liVoucherReciept.Attributes.Add("class", "active")

            divVoucherReciept2.Attributes.Add("class", "tab-pane")
            divVoucherReciept.Attributes.Add("class", "tab-pane active")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindCabinet()
        Try
            ddlcabinet.DataSource = objIndex.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            ddlcabinet.DataTextField = "CBN_NAME"
            ddlcabinet.DataValueField = "CBN_ID"
            ddlcabinet.DataBind()
            ddlcabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCabinet" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub BindTransactiontype()
        Try
            ddlTransactiontype.DataSource = objIndex.LoadSubCabinet(sSession.AccessCode, sSession.AccessCodeID, ddlcabinet.SelectedValue)
            ddlTransactiontype.DataTextField = "CBN_NAME"
            ddlTransactiontype.DataValueField = "CBN_ID"
            ddlTransactiontype.DataBind()
            ddlTransactiontype.Items.Insert(0, "Transaction Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnNextNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNavDoc.Click
        Try
            If sPageDetails.Rows.Count <> txtNavDoc.Text Then
                txtNavDoc.Text = txtNavDoc.Text + 1
                gtBatchfile(txtNavDoc.Text)
            Else
                gtBatchfile(txtNavDoc.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindBatchNo(ByVal iCustomerID As Integer, ByVal iTrType As String)
        Try
            ddlBatch.DataSource = objclsADOBatch.BindBatchDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTrType)
            ddlBatch.DataValueField = "fol_folid"
            ddlBatch.DataTextField = "fol_name"
            ddlBatch.DataBind()
            ddlBatch.Items.Insert(0, "Select BatchNo")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnPreviousNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNavDoc.Click
        Try
            If txtNavDoc.Text <> 1 Then
                txtNavDoc.Text = txtNavDoc.Text - 1
                gtBatchfile(txtNavDoc.Text)
            Else
                gtBatchfile(txtNavDoc.Text)

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlcabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcabinet.SelectedIndexChanged

        Try
            If ddlcabinet.SelectedIndex = 0 Then
                ddlTransactiontype.DataSource = objIndex.LoadSubCabinet(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                ddlTransactiontype.DataSource = objIndex.LoadSubCabinet(sSession.AccessCode, sSession.AccessCodeID, ddlcabinet.SelectedValue)
            End If
            ddlTransactiontype.DataTextField = "CBN_NAME"
            ddlTransactiontype.DataValueField = "CBN_ID"
            ddlTransactiontype.DataBind()
            ddlTransactiontype.Items.Insert(0, "Transaction Type")


            If ddlTransactiontype.SelectedIndex = 0 Then
                ddlBatch.DataSource = objclsADOBatch.BindBatchDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            Else
                ddlBatch.DataSource = objclsADOBatch.BindBatchDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlTransactiontype.SelectedValue)
            End If
            ddlBatch.DataValueField = "fol_folid"
            ddlBatch.DataTextField = "fol_name"
            ddlBatch.DataBind()
            ddlBatch.Items.Insert(0, "Select BatchNo")

            ddlBatch_SelectedIndexChanged(sender, e)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlTransactiontype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransactiontype.SelectedIndexChanged
        Try
            BindBatchNo(ddlcabinet.SelectedValue, ddlTransactiontype.SelectedValue)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub gtAttachfile(ByVal iSelectedIndexID As Integer)
        Try
            If sAttachdetails.Rows.Count = 0 Then

            Else
                gtViewer(sAttachdetails.Rows(txtNav.Text - 1)("pge_basename"))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Vintaviewer")
        End Try

    End Sub

    Private Sub imgbtnPreviousNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNav.Click
        Try
            Try
                If txtNav.Text <> 1 Then
                    txtNav.Text = txtNav.Text - 1
                    gtAttachfile(txtNav.Text)
                Else
                    gtAttachfile(txtNav.Text)
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnNextNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNav.Click
        Try
            If sAttachdetails.Rows.Count <> txtNav.Text Then
                txtNav.Text = txtNav.Text + 1
                gtAttachfile(txtNav.Text)
            Else
                gtAttachfile(txtNav.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnAttachment_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAttachment.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalUploadDocuments').modal('show');", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Dim lblPath As String
        Dim ddlUpdateDocumentType As New DropDownList
        Dim sKeywords As String = "", sFilePath As String, sFileName As String, sISDB As String, sPath As String = ""
        Dim Arr() As String
        Dim iPageDetailsid As Integer = 0, iPageID As Integer = 0, iDFAttachID As Integer = 0 ', fileSize As Integer,
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0 ', j As Integer
        'Dim objFile As FileStream
        Dim sPageExt As String
        'Dim dDate As Date
        'Dim dtCheckData As New DataTable
        Try
            lblError.Text = ""
            Dim hfc As HttpFileCollection = Request.Files
            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dtColumns.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        'dtCheckData = Session("Attachment")
                        'If dtCheckData.Rows.Count > 0 Then
                        '    dtCheckData.Clear()
                        '    Session("Attachment") = dtCheckData
                        'End If

                        dtColumns = Session("Attachment")

                        If dtColumns.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "\Images\" & sFilesNames)

                            dRow = dtColumns.NewRow()
                            dRow("AtchID") = 0
                            dRow("FilePath") = Server.MapPath(".") & "\Images\" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedBy") = sSession.UserLoginName
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dtColumns.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dtColumns)
                            dvAttach.Sort = "FileName Desc"
                            dtColumns = dvAttach.ToTable
                            Session("Attachment") = dtColumns
                        ElseIf dtColumns.Rows.Count > 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "\Images\" & sFilesNames)
                            dRow = dtColumns.NewRow()
                            dRow("AtchID") = 0
                            dRow("FilePath") = Server.MapPath(".") & "\Images\" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedBy") = sSession.UserLoginName
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dtColumns.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dtColumns)
                            dvAttach.Sort = "FileName Desc"
                            dtColumns = dvAttach.ToTable
                            Session("Attachment") = dtColumns
                        End If
                    End If
                Next
            End If

            If dtColumns.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            Else


                icabinetID = sAttachdetails.Rows(0)("pge_cabinet")

                iSubCabinet = sAttachdetails.Rows(0)("pge_subcabinet")

                iFolder = sAttachdetails.Rows(0)("pge_folder")


                iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)


                If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                    If dtColumns.Rows.Count > 0 Then
                        For i = 0 To dtColumns.Rows.Count - 1
                            lblPath = dtColumns.Rows(i)("FilePath")
                            sPageExt = UCase(dtColumns.Rows(i)("Extension"))
                            sFilePath = lblPath
                            sFileName = dtColumns.Rows(i)("FileName")
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objclsGRACeGeneral.SafeSQL(sFileName)
                            objIndex.dPGEDATE = Today.ToString("dd/MM/yyyy")
                            ' If iPageDetailsid = 0 Then   'Modified by Vijeth on 21/07/2020
                            'iPageDetailsid = objIndex.iPGEBASENAME
                            objIndex.iPgeDETAILSID = sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename")
                            'End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt

                            objIndex.sPGEKeyWORD = ""
                            objIndex.sPGEOCRText = ""
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
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)
                        Next

                        If Arr(0) = "3" Then
                            lblMsg.Text = "Successfully Indexed."
                        End If
                    End If
                End If

            End If
            ddlBatch_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub btnsumbit_ServerClick(sender As Object, e As EventArgs) Handles btnsumbit.ServerClick
        Try
            CreateBatch(ddlcabinet.SelectedValue, ddlTransactiontype.SelectedValue, ddlBatch.SelectedValue, sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"), ddlBatch.SelectedItem.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreateBatch(ByVal iCabinet As Integer, ByVal iTrtype As Integer, ByVal iFolder As Integer, ByVal iAttachid As Integer, ByVal sTitle As String)
        Dim objBatch As New clsADOBatch.BatchScan
        Dim Arr() As String
        Dim sBatchno As String = ""
        'Dim dDate As Date
        Try
            'Batch Scan
            objBatch.BT_ID = 0
            objBatch.BT_CustomerID = iCabinet
            objBatch.BT_TrType = iTrtype

            '  If ddlfolder.SelectedIndex >= 0 Then
            '  objBatch.BT_BatchID = ddlfolder.SelectedValue
            ' Else
            objBatch.BT_BatchID = iFolder
            ' End If
            objBatch.BT_BatchNo = ddlBatch.SelectedItem.Text


            objBatch.BT_AttachID = iAttachid

            objBatch.BT_BatchTitle = sTitle

            objBatch.BT_NFT = txtTransactions.Text

            objBatch.BT_Voucherno = txtVocherno.Text

            objBatch.BT_Datetime = txtdate.Text
            objBatch.BT_Comment = txtComment.Text


            objBatch.BT_DebitTotal = 0

            objBatch.BT_CreditTotal = 0


            objBatch.BT_Delflag = "A"
            If rblAccept.Checked = True Then
                objBatch.BT_Status = "Accepted"
            ElseIf rblReject.Checked = True Then
                objBatch.BT_Status = "Reject"
            End If


            objBatch.BT_CompID = sSession.AccessCodeID
            objBatch.BT_YearID = sSession.YearID
            objBatch.BT_CrBy = sSession.UserID
            objBatch.BT_IPAddress = sSession.IPAddress
            Arr = objclsADOBatch.SaveBatchDetails(sSession.AccessCode, objBatch)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlBatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBatch.SelectedIndexChanged
        'Try
        '    'gtBatchfile(Val(iAttachID))
        'Catch ex As Exception
        '    Throw
        'End Try
        Dim dtGet As New DataTable
        Dim Status As String = ""
        Try
            sPageDetails.Rows.Clear()
            If sPageDetails.Rows.Count = 0 Then
                If ddlBatch.SelectedIndex = 0 Then
                    sPageDetails = objclsADOBatch.GetPageDetails(sSession.AccessCode, sSession.AccessCodeID, 0)
                    txtTransactions.Text = ""
                    txtVocherno.Text = ""
                    txtdate.Text = ""
                    txtComment.Text = ""
                    rblAccept.Checked = False
                    rblReject.Checked = False
                    documentViewer.ImageUrl = Nothing
                    txtNavDoc.Text = 0
                    lblNavDoc.Text = 0
                    txtNav.Text = 0
                    lblNav.Text = 0
                    Exit Sub
                Else
                    sPageDetails = objclsADOBatch.GetPageDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBatch.SelectedValue)
                End If

                txtNavDoc.Text = 1
                lblNavDoc.Text = "/ " & sPageDetails.Rows.Count
                gtViewer(sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))
            Else
                gtViewer(sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))
            End If


            sAttachdetails = objclsADOBatch.GetAttachdetails(sSession.AccessCode, sSession.AccessCodeID, sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))

            If sAttachdetails.Rows.Count = 0 Then
                txtNav.Text = 0
                lblNav.Text = "/ " & 0
            Else
                txtNav.Text = 1
                lblNav.Text = "/ " & sAttachdetails.Rows.Count
            End If

            dtGet = objclsADOBatch.GetDetails(sSession.AccessCode, sSession.AccessCodeID, ddlcabinet.SelectedValue, ddlTransactiontype.SelectedValue, ddlBatch.SelectedValue, sPageDetails.Rows(txtNavDoc.Text - 1)("pge_basename"))

            If dtGet.Rows.Count > 0 Then
                txtTransactions.Text = dtGet.Rows(0)("BT_NFT")
                txtVocherno.Text = dtGet.Rows(0)("BT_Vouchers")
                txtdate.Text = dtGet.Rows(0)("BT_Date")
                txtComment.Text = dtGet.Rows(0)("BT_Comments")
                Status = dtGet.Rows(0)("bt_status")
                If Status = "Accepted" Then
                    rblAccept.Checked = True
                    rblReject.Checked = False
                ElseIf Status = "Reject" Then
                    rblReject.Checked = True
                    rblAccept.Checked = False
                End If
            Else
                txtTransactions.Text = ""
                txtVocherno.Text = ""
                txtdate.Text = ""
                txtComment.Text = ""
                rblAccept.Checked = False
                rblReject.Checked = False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GetCall(ByVal iAttachiD As Integer)
        Dim dtGet As New DataTable
        Dim Status As String = ""
        Try

            dtGet = objclsADOBatch.GetBatchAttached(sSession.AccessCode, sSession.AccessCodeID, iAttachiD)
            If dtGet.Rows.Count > 0 Then
                txtTransactions.Text = dtGet.Rows(0)("BT_NFT")
                txtVocherno.Text = dtGet.Rows(0)("BT_Vouchers")
                txtdate.Text = dtGet.Rows(0)("BT_Date")
                txtComment.Text = dtGet.Rows(0)("BT_Comments")
                Status = dtGet.Rows(0)("bt_status")
                If Status = "Accepted" Then
                    rblAccept.Checked = True
                    rblReject.Checked = False
                ElseIf Status = "Reject" Then
                    rblReject.Checked = True
                    rblAccept.Checked = False
                End If
            Else
                txtTransactions.Text = ""
                txtVocherno.Text = ""
                txtdate.Text = ""
                txtComment.Text = ""
                Status = ""
                rblAccept.Checked = False
                rblReject.Checked = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click

        Try
            Response.Redirect(String.Format("~/FIN Statement/ADODashboard.aspx?"), False) 'ADO/ADO_Dashboard
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
