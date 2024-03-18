Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.IO
Public Class ADODashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "ADO_Dashboard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsDigitalFilingDashboard As New clsDigitalFilingDashboard
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAttachments As New clsAttachments
    Private objIndex As New clsIndexing
    Private objclsEdictGeneral As New clsEDICTGeneral
    Private objclsDataCapture As New ClsDataCapture
    Private objclsSearch As New clsSearch
    Private sSession As AllSession
    Private objclsADOBatch As New clsADOBatch

    Dim dt As New DataTable
    Private Shared iEDTPKId As Integer
    Dim dtColumns As New DataTable
    Private Shared sEmpCust As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnUploadDocuments.ImageUrl = "~/Images/Upload24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")

            If IsPostBack = False Then
                BindCabinet() : BindCustomers()
                'BindTransactiontype() :
                Session("Attachment") = Nothing
                dtColumns.Columns.Add("AtchID")
                dtColumns.Columns.Add("FilePath")
                dtColumns.Columns.Add("FileName")
                dtColumns.Columns.Add("Extension")
                dtColumns.Columns.Add("CreatedBy")
                dtColumns.Columns.Add("CreatedOn")
                Session("Attachment") = dtColumns

                sEmpCust = objclsDigitalFilingDashboard.CheckEmpOrCust(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                If sEmpCust = "E" Then
                    ddlCustomer.Enabled = True
                Else
                    ddlCustomer.Enabled = False
                End If
                BindAllAttachedDocuments()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub BindCabinet()
        Try
            ddlCabinet.DataSource = objIndex.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_ID"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCabinet" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindTransactiontype()
        Try
            ddlfolder.Items.Insert(0, "Select Folder")
            ddlfolder.Items.Insert(1, "Petty Cash")
            ddlfolder.Items.Insert(2, "Payment")
            ddlfolder.Items.Insert(3, "Reciept")
            ddlfolder.Items.Insert(4, "Purchase")
            ddlfolder.Items.Insert(5, "Sales")
            ddlfolder.Items.Insert(6, "Journal Entry")
            ddlfolder.Items.Insert(7, "Debit")
            ddlfolder.Items.Insert(8, "Credit")
            ddlfolder.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindCustomers()
        Try
            ddlCustomer.DataSource = objclsDigitalFilingDashboard.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomer.DataTextField = "CUST_NAME"
            ddlCustomer.DataValueField = "CUST_ID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub imgbtnUploadDocuments_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUploadDocuments.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            lblDateDisplay.Text = Today.ToString("dd/MM/yyyy")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalUploadDocuments').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUploadDocuments_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvUploadedDocument_PreRender(sender As Object, e As EventArgs) Handles gvUploadedDocument.PreRender
        Try
            If gvUploadedDocument.Rows.Count > 0 Then
                gvUploadedDocument.UseAccessibleHeader = True
                gvUploadedDocument.HeaderRow.TableSection = TableRowSection.TableHeader
                gvUploadedDocument.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUploadedDocument_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub BindAllAttachedDocuments()
        Dim dt As New DataTable
        Try
            If ddlCustomer.SelectedIndex > 0 Then
                dt = objclsADOBatch.GetDashboard(sSession.AccessCode, sSession.AccessCodeID, ddlCustomer.SelectedValue)
            Else
                dt = objclsADOBatch.GetDashboard(sSession.AccessCode, sSession.AccessCodeID, 0)
            End If

            If dt.Rows.Count > 0 Then
                gvUploadedDocument.DataSource = dt
                gvUploadedDocument.DataBind()
            Else
                lblError.Text = "No Documents found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Documents found.','');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAttachedDocuments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub gvUploadedDocument_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUploadedDocument.RowCommand
        Dim lblAtchDocID As New Label, lblDFAttachID As New Label
        'Dim sPaths As String, sDestFilePath As String
        Dim oAttachID As New Object
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If e.CommandName.Equals("OPENPAGE") Then
                'Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                'lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                'sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                'sDestFilePath = objclsDigitalFilingDashboard.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, Val(lblAtchDocID.Text))
                'DownloadMyFile(sDestFilePath)
            End If

            If e.CommandName.Equals("ShareDocument") Then
                'Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                'lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                'lblDFAttachID = DirectCast(clickedRow.FindControl("lblDFAttachID"), Label)
                'oAttachID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblDFAttachID.Text)))
                'iEDTPKId = (Val(lblAtchDocID.Text))
                'Response.Redirect(String.Format("~/Digital_AuditOffice/Outward.aspx?AttachID={0}", oAttachID), False)
            End If

            If e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEDTPKId = (Val(lblAtchDocID.Text))
                oAttachID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAtchDocID.Text)))

                Response.Redirect(String.Format("~/FIN Statement/ADO_Batchdetails.aspx?AttachID={0}", oAttachID), False)
                ' BindIndexDetails(Val(lblAtchDocID.Text))
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)

            End If

            If e.CommandName.Equals("REMOVE") Then
                ' Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                ' lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                ' objclsDigitalFilingDashboard.RemoveSelectedDocument(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblAtchDocID.Text))
                ' BindAllAttachedDocuments()
                ' lblError.Text = "Successfully Removed." : lblDigitalFilingDashboardValidationMsg.Text = "Successfully Removed."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUploadedDocuments_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub CreateBatch(ByVal iCabinet As Integer, ByVal iTrtype As Integer, ByVal iFolder As Integer, ByVal iAttachid As Integer, ByVal sTitle As String)
        Dim objBatch As New clsADOBatch.BatchScan
        Dim Arr() As String
        Dim sBatchno As String = ""
        Dim dDate As Date
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

            If sBatchno <> "" Then
                objBatch.BT_BatchNo = sBatchno
            Else
                sBatchno = objclsADOBatch.GetBatchno(sSession.AccessCode, sSession.AccessCodeID, iFolder)
                objBatch.BT_BatchNo = sBatchno
            End If

            objBatch.BT_AttachID = iAttachid

            objBatch.BT_BatchTitle = sTitle

            If txtNFT.Text <> "" Then
                objBatch.BT_NFT = txtNFT.Text
            Else
                objBatch.BT_NFT = dtColumns.Rows.Count
            End If

            objBatch.BT_Voucherno = ""
            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objBatch.BT_Datetime = dDate
            objBatch.BT_Comment = ""

            If txtdebit.Text <> "" Then
                objBatch.BT_DebitTotal = txtdebit.Text
            Else
                objBatch.BT_DebitTotal = 0
            End If

            If txtcredit.Text <> "" Then
                objBatch.BT_CreditTotal = txtcredit.Text
            Else
                objBatch.BT_CreditTotal = 0
            End If

            objBatch.BT_Delflag = "A"
            objBatch.BT_Status = ""
            objBatch.BT_CompID = sSession.AccessCodeID
            objBatch.BT_YearID = sSession.YearID
            objBatch.BT_CrBy = sSession.UserID
            objBatch.BT_IPAddress = sSession.IPAddress
            Arr = objclsADOBatch.SaveBatchDetails(sSession.AccessCode, objBatch)

        Catch ex As Exception

        End Try

    End Sub
    Private Sub gvUploadedDocument_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUploadedDocument.RowDataBound
        Dim imgbtnShareDocument As New ImageButton, imgbtnAdd As New ImageButton, imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                imgbtnShareDocument = CType(e.Row.FindControl("imgbtnShareDocument"), ImageButton)
                imgbtnShareDocument.ImageUrl = "~/Images/Share_document24.png"
                imgbtnAdd = CType(e.Row.FindControl("imgbtnAdd"), ImageButton)
                imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUploadedDocument_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindBatchNo(ByVal iCustomerID As Integer, ByVal iTrType As String)
        Try
            ddlfolder.DataSource = objclsADOBatch.BindBatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCustomerID, iTrType)
            ddlfolder.DataValueField = "fol_folid"
            ddlfolder.DataTextField = "fol_name"
            ddlfolder.DataBind()
            ddlfolder.Items.Insert(0, "Select BatchNo")
        Catch ex As Exception
            Throw
        End Try
    End Sub



    Private Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomer.SelectedIndexChanged
        Try
            BindAllAttachedDocuments()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            If ddlCabinet.SelectedIndex > 0 Then
                ddlSubcabinet.DataSource = objIndex.LoadSubCabinet(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue)
                ddlSubcabinet.DataTextField = "CBN_NAME"
                ddlSubcabinet.DataValueField = "CBN_ID"
                ddlSubcabinet.DataBind()
                ddlSubcabinet.Items.Insert(0, "Select Finanical Year")
            Else
                ddlSubcabinet.Items.Clear() : ddlfolder.Items.Clear()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlSubcabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubcabinet.SelectedIndexChanged
        Try
            ddlfolder.Items.Clear()
            If ddlCabinet.SelectedIndex > 0 And ddlSubcabinet.SelectedIndex > 0 Then
                ddlfolder.Items.Insert(0, "Select Transaction")
                ddlfolder.Items.Insert(1, "Petty Cash")
                ddlfolder.Items.Insert(2, "Payment")
                ddlfolder.Items.Insert(3, "Reciept")
                ddlfolder.Items.Insert(4, "Purchase")
                ddlfolder.Items.Insert(5, "Sales")
                ddlfolder.Items.Insert(6, "Journal Entry")
                ddlfolder.Items.Insert(7, "Debit")
                ddlfolder.Items.Insert(8, "Credit")
                ddlfolder.SelectedIndex = 0
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubcabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Dim lblPath As String
        Dim ddlUpdateDocumentType As New DropDownList
        Dim sKeywords As String = "", sFilePath As String, sFileName As String, sISDB As String, sPath As String = ""
        Dim Arr() As String
        Dim iPageDetailsid As Integer = 0, iPageID As Integer = 0, iDFAttachID As Integer = 0 ', fileSize As Integer
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0 ', j As Integer
        Dim sPageExt As String
        Dim dDate As Date
        Try
            lblError.Text = "" : iEDTPKId = 0
            Dim hfc As HttpFileCollection = Request.Files
            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dtColumns.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
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

                If ddlCabinet.SelectedIndex = 0 Then
                    lblMsg.Text = "Select Customer/Cabinet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalUploadDocuments').modal('show');", True)
                    ddlCustomer.Focus()
                    Exit Sub
                Else
                    'icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCabinet.SelectedItem.Text)
                    icabinetID = ddlCabinet.SelectedValue
                End If

                If ddlSubcabinet.SelectedIndex = 0 Then
                    lblMsg.Text = "Select Financial Year."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalUploadDocuments').modal('show');", True)
                    ddlSubcabinet.Focus()
                    Exit Sub
                Else
                    'iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, ddlSubcabinet.SelectedItem.Text)
                    iSubCabinet = ddlSubcabinet.SelectedValue
                End If

                If ddlfolder.SelectedIndex = 0 Then
                    lblMsg.Text = "Select Transactions."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalUploadDocuments').modal('show');", True)
                    ddlfolder.Focus()
                    Exit Sub
                Else
                    iFolder = objIndex.CheckFoldersName(sSession.AccessCode, sSession.UserID, ddlfolder.SelectedItem.Text, iSubCabinet) 'Checking for Folder Existance
                    If iFolder = 0 Then
                        objIndex.CreateFolder(sSession.AccessCode, iSubCabinet, ddlfolder.SelectedItem.Text, sSession.UserID) 'Creating new Folder
                        objIndex.UpdateFolderCount(sSession.AccessCode, ddlCabinet.SelectedValue, ddlSubcabinet.SelectedValue) 'Updating Folders
                    End If

                    Dim dt6 As DataTable = objIndex.GetFolderID(sSession.AccessCode, ddlfolder.SelectedItem.Text, iSubCabinet) 'Getting FolderID
                    iFolder = dt6.Rows(0)("FOL_FolID")
                End If


                'If ddlfolder.SelectedIndex = 0 Then
                '    Dim sNewfolder As String = objclsADOBatch.GetADOdata(sSession.AccessCode, sSession.AccessCodeID, iSubCabinet)
                '    iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, sNewfolder)
                'Else
                '    iFolder = ddlfolder.SelectedValue
                '    ' iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlfolder.SelectedValue)
                'End If

                iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

                'If ddlType.SelectedIndex = 0 Then
                '    lblModelError.Text = "Select Type."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                '    ddlType.Focus()
                '    Exit Sub
                'Else
                '    iType = ddlType.SelectedValue
                'End If

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
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            ' If iPageDetailsid = 0 Then   'Modified by Vijeth on 21/07/2020
                            'iPageDetailsid = objIndex.iPGEBASENAME
                            objIndex.iPgeDETAILSID = objIndex.iPGEBASENAME
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
                            CreateBatch(icabinetID, iSubCabinet, iFolder, objIndex.iPGEBASENAME, objIndex.spgeOrignalFileName)
                        Next

                        If Arr(0) = "3" Then
                            lblMsg.Text = "Successfully Indexed."

                        End If
                    End If
                End If

            End If

            If ddlCustomer.SelectedIndex > 0 Then
                dt = objclsADOBatch.GetDashboard(sSession.AccessCode, sSession.AccessCodeID, ddlCustomer.SelectedValue)
            Else
                dt = objclsADOBatch.GetDashboard(sSession.AccessCode, sSession.AccessCodeID, 0)
            End If
            If dt.Rows.Count > 0 Then
                gvUploadedDocument.DataSource = dt
                gvUploadedDocument.DataBind()
            Else
                Dim dtEmpty As New DataTable
                gvUploadedDocument.DataSource = dtEmpty
                gvUploadedDocument.DataBind()
            End If
            dtColumns.Rows.Clear()
            ddlCabinet.SelectedIndex = 0 : ddlSubcabinet.SelectedIndex = 0 : ddlfolder.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
End Class
