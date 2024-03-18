Imports System
Imports System.Data
Imports BusinesLayer
Imports System.IO
Imports System.Globalization
Imports Newtonsoft.Json
Imports System.Net
Imports System.Threading.Tasks

Partial Class Indexing
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "DigitalFiling_Indexing"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsEdictGeneral As New clsEDICTGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objIndex As New clsIndexing
    Private Shared iAttachID As Integer
    Private Shared sIndexSave As String
    Private objclsPermission As New clsAccessRights
    Private Shared iDocID As Integer
    Dim dt As New DataTable
    Private Shared iPID As Integer

    Private objDB As New clsHomeDashboard
    Private Shared sExt As String = ""
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnIndex.ImageUrl = "~/Images/Index24.png"
        imgbtnIndexSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Private Sub DigitalFiling_Indexing_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            ' documentViewer.Document = String.Format("~/Images/SearchImage/NoImage.jpg")
            If IsPostBack = False Then
                'imgbtnIndex.Visible = False : imgbtnIndexSave.Visible = False
                'sIndexSave = "NO"
                'sFormButtons = objclsPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DFI")
                'If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,," Then
                '    Response.Redirect("~/Permission/DigitalFillingPermission.aspx", False) 'Permissions/DigitalFillingPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                '        sIndexSave = "YES"
                '        imgbtnIndex.Visible = True : imgbtnIndexSave.Visible = True
                '    End If
                '    If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '        sIndexSave = "YES"
                '        imgbtnIndex.Visible = True : imgbtnIndexSave.Visible = True
                '    End If
                'End If


                If Request.QueryString("Status") IsNot Nothing Then
                    iPID = objclsEdictGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("Status")))
                End If
                If iPID <> 0 Then
                    dt = Session("Attachment")
                    gvattach.DataSource = dt
                    gvattach.DataBind()

                    Dim chkSelect As New CheckBox, chkAll As New CheckBox
                    Dim iIndx As Integer
                    If gvattach.Rows.Count > 0 Then
                        chkAll = gvattach.HeaderRow.FindControl("chkSelectAll")
                        For iIndx = 0 To gvattach.Rows.Count - 1
                            chkSelect = gvattach.Rows(iIndx).FindControl("chkSelect")
                            chkSelect.Checked = True
                        Next

                        'chkAll = gvattach.FindControl("chkSelectAll")
                        chkAll.Checked = True
                    End If
                Else
                    Session("Attachment") = Nothing
                    dt.Columns.Add("ID")
                    dt.Columns.Add("FilePath")
                    dt.Columns.Add("FileName")
                    Session("Attachment") = dt
                End If



                BindCabinet()
                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                ddlType.DataSource = objIndex.LoadDocumentType(sSession.AccessCode, sSession.AccessCodeID)
                ddlType.DataTextField = "DOT_DOCNAME"
                ddlType.DataValueField = "DOT_DOCTYPEID"
                ddlType.DataBind()
                ddlType.Items.Insert(0, "Select Document Type")
                RFVTitle.ErrorMessage = "Enter Title." : RFVTitle.InitialValue = "" : RFVTitle.ControlToValidate = "txtTitle" : RFVTitle.ValidationGroup = "Save"
                RFVcabinet.ErrorMessage = "Select Cabinet." : RFVcabinet.InitialValue = "Select Cabinet" : RFVcabinet.ControlToValidate = "ddlCabinet" : RFVcabinet.ValidationGroup = "Save"
                RFVSubCabinet.ErrorMessage = "Select Sub-Cabinet." : RFVSubCabinet.InitialValue = "Select Sub-Cabinet" : RFVSubCabinet.ControlToValidate = "ddlSubcabinet" : RFVSubCabinet.ValidationGroup = "Save"
                RFVFolder.ErrorMessage = "Select Folder." : RFVFolder.InitialValue = "Select Folder" : RFVFolder.ControlToValidate = "ddlFolder" : RFVFolder.ValidationGroup = "Save"
                RFVType.ErrorMessage = "Select Document Type." : RFVType.InitialValue = "Select Document Type" : RFVType.ControlToValidate = "ddlType" : RFVType.ValidationGroup = "Save"
                REVTitle.ErrorMessage = "Title exceeded maximum size(max 2000 characters)." : REVTitle.ValidationExpression = "^[\s\S]{0,2000}$" : REVTitle.ValidationGroup = "Save"

                If (Request.QueryString("flag") = "1") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DigitalFiling_Indexing_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Dim sTempPath As String = "", sFileName As String = ""
        Dim sIPath As String = ""
        Dim sOPath As String = ""
        Dim NxtExit As String = ""
        Try
            lblError.Text = "" : iDocID = 0

            Dim hfc As HttpFileCollection = Request.Files

            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()
                        sFilesNames = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                        Dim fileExtension As String = Path.GetExtension(hpf.FileName)
                        sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "ExcelPath")

                        If sTempPath.EndsWith("\") = True Then
                            sTempPath = sTempPath & "Temp\Upload\"
                        Else
                            sTempPath = sTempPath & "Temp\Upload\"
                        End If
                        dt = Session("Attachment")
                        If dt.Rows.Count = 0 Then
                            objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sTempPath)
                            ' hpf.SaveAs(sTempPath & sFilesNames & "_ed" & fileExtension)
                            hpf.SaveAs(sTempPath & sFilesNames & fileExtension)

                            'sIPath = sTempPath & sFilesNames & "_ed" & fileExtension
                            sIPath = sTempPath & sFilesNames & fileExtension
                            sOPath = sTempPath & sFilesNames & fileExtension

                            ' objclsEdictGeneral.FileEn(sIPath, sOPath)
                            sFilesNames = sFilesNames & fileExtension
                            dRow = dt.NewRow()

                            dRow("ID") = dt.Rows.Count + 1
                            dRow("FilePath") = sTempPath & sFilesNames
                            dRow("FileName") = sFilesNames
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "ID Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        ElseIf dt.Rows.Count > 0 Then
                            hpf.SaveAs(sTempPath & sFilesNames & "_ed" & fileExtension)

                            sIPath = sTempPath & sFilesNames & "_ed" & fileExtension
                            sOPath = sTempPath & sFilesNames & fileExtension

                            objclsEdictGeneral.FileEn(sIPath, sOPath)
                            sFilesNames = sFilesNames & fileExtension

                            dRow = dt.NewRow()
                            dRow("ID") = dt.Rows.Count + 1
                            dRow("FilePath") = sTempPath & sFilesNames
                            dRow("FileName") = sFilesNames
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "ID Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If
            Session("Attachment") = dt
            gvattach.DataSource = dt
            gvattach.DataBind()
            dt.Clear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAttch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnIndex_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnIndex.Click
        Dim chkSelect As CheckBox
        Dim iCount As Integer = 0
        Try
            If gvattach.Rows.Count > 0 Then
                lblError.Text = "" : lblModelError.Text = ""
                For i = 0 To gvattach.Rows.Count - 1
                    chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                    If chkSelect.Checked = True Then
                        iCount = iCount + 1
                        ddlCabinet.SelectedIndex = 0 : ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : txtTitle.Text = "" : ddlType.SelectedIndex = 0 'Vijeth
                        ddlType_SelectedIndexChanged(sender, e)
                    End If
                Next
                If iCount = 0 Then
                    lblError.Text = "Select the file to Index." : lblValidationMsg.Text = "Select the file to Index."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select the file to Index.','', 'warning');", True) 'Vijeth
                    Exit Sub
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            Else
                lblError.Text = "Attach file to Index."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Attach file to Index','', 'warning');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnIndex_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
        Try
            If gvattach.Rows.Count > 0 Then
                gvattach.UseAccessibleHeader = True
                gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvattach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Dim ExtraP As Integer
        Try

            ' ExtraP = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCabinet.SelectedValue, "CBP_Index") 'vijeth
            ' If ExtraP <> 0 Then
            If ddlCabinet.SelectedIndex > 0 Then
                ddlSubcabinet.DataSource = objIndex.LoadSubCabinet(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue)
                ddlSubcabinet.DataTextField = "CBN_NAME"
                ddlSubcabinet.DataValueField = "CBN_ID"
                ddlSubcabinet.DataBind()
                ddlSubcabinet.Items.Insert(0, "Select Sub-Cabinet")
            Else
                ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            ' Else
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Cabinet Permission is not Assigned','', 'info');", True)
            ' End If 'vijeth

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlSubcabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubcabinet.SelectedIndexChanged
        Dim ExtraP As Integer
        Try
            ' ExtraP = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlSubcabinet.SelectedValue, "CBP_Index") 'vijeth
            'If ExtraP <> 0 Then
            ddlFolder.Items.Clear()
            If ddlCabinet.SelectedIndex > 0 And ddlSubcabinet.SelectedIndex > 0 Then
                ddlFolder.DataSource = objIndex.LoadFolder(sSession.AccessCode, sSession.AccessCodeID, ddlSubcabinet.SelectedValue)
                ddlFolder.DataTextField = "FOL_Name"
                ddlFolder.DataValueField = "Fol_FolID"
                ddlFolder.DataBind()
                ddlFolder.Items.Insert(0, "Select Folder")
            End If
            ' Else
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('SubCabinet Permission is not Assigned','', 'info');", True)
            ' End If 'vijeth
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubcabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlFolder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFolder.SelectedIndexChanged
        Dim ExtraP As Integer
        Try
            If ddlFolder.SelectedIndex > 0 Then
                ' ExtraP = objclsPermission.ExtraPermissionsToFolder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlFolder.SelectedValue, "EFP_INDEX") 'vijeth
                ' If ExtraP <> 0 Then
                ddlType.DataSource = objIndex.LoadDocumentType(sSession.AccessCode, sSession.AccessCodeID)
                ddlType.DataTextField = "DOT_DOCNAME"
                ddlType.DataValueField = "DOT_DOCTYPEID"
                ddlType.DataBind()
                ddlType.Items.Insert(0, "Select Document Type")
                'Else
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Folder Permission is not Assigned','', 'info');", True)
                'End If 'vijeth
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFolder_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
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
        Dim iTitle As Integer = 0
        Dim ExtraP As Integer
        Try
            lblError.Text = "" : lblModelError.Text = ""

            If ddlCabinet.SelectedIndex = 0 Then
                lblModelError.Text = "Select Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlCabinet.Focus()
                Exit Sub
            Else
                icabinetID = ddlCabinet.SelectedValue
            End If

            If ddlSubcabinet.SelectedIndex = 0 Then
                lblModelError.Text = "Select Sub Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlSubcabinet.Focus()
                Exit Sub
            Else
                iSubCabinet = ddlSubcabinet.SelectedValue
            End If

            If ddlFolder.SelectedIndex = 0 Then
                lblModelError.Text = "Select Folder."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlFolder.Focus()
                Exit Sub
            Else
                iFolder = ddlFolder.SelectedValue
            End If

            If ddlType.SelectedIndex = 0 Then
                lblModelError.Text = "Select Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlType.Focus()
                Exit Sub
            Else
                iType = ddlType.SelectedValue
            End If
            If txtTitle.Text = "" Then
                lblModelError.Text = "Enter Title."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                txtTitle.Focus()
                Exit Sub
            Else
                iTitle = objIndex.CheckTitle(sSession.AccessCode, sSession.AccessCodeID, txtTitle.Text)
                If iTitle <> 0 Then
                    lblModelError.Text = "Title Already Exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                    txtTitle.Focus()
                    Exit Sub
                End If
            End If

            ExtraP = objclsPermission.ExtraPermissionsToFolder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlFolder.SelectedValue, "EFP_INDEX") 'vijeth

            If ExtraP <> 0 Then
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Folder Permission is not Assigned','', 'info');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                Exit Sub
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
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
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
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                            txtValues.Focus()
                            Exit Sub
                        End If
                    ElseIf (sDescriptor.IndexOf(sSearchNumber) <> -1) Then
                        If IsNumeric(txtValues.Text) = False Then
                            lblModelError.Text = "Enter valid numberic values for " & lblDescriptor.Text & "."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                            txtValues.Focus()
                            Exit Sub
                        End If
                    End If
                    If lblValidator.Text = "Y" Then
                        If txtValues.Text.Length > lblSize.Text Then
                            lblModelError.Text = "Value for " & lblDescriptor.Text & " exceeded maximum size (" & lblSize.Text & ")."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                            txtValues.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            Next
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
                            objIndex.sPGETITLE = objclsEdictGeneral.SafeSQL(txtTitle.Text.Trim)
                            objIndex.dPGEDATE = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If

                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt

                            objIndex.sPGEKeyWORD = objclsEdictGeneral.SafeSQL(sKeywords)
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
                            objIndex.spgeOrignalFileName = objclsEdictGeneral.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            If txtRFID.Text <> "" Then
                                objIndex.sPGERFID = txtRFID.Text.Trim
                            Else
                                objIndex.sPGERFID = ""
                            End If

                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            If iPID = 1 Then  ' Modified on 10/08/2022  - Url upload
                                sFilePath = Urlenp(objIndex.iPGEBASENAME, sFilePath)
                            End If
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
                        lblError.Text = "Successfully Indexed." : lblModelError.Text = "Successfully Indexed."
                        ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True) 'Vijeth
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Indexed','', 'success');", True)
                        lblValidationMsg.Text = "Successfully Indexed."
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Indexing", "Indexed", "0", sSession.YearName, 0, "", sSession.IPAddress)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Indexed','', 'success');", True)
                    gvattach.DataBind()
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True) 'Vijeth
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
                sExt = objclsEdictGeneral.ChangeExt(sExt)
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
    Private Function Urlenp(ByVal iBaseName As Long, ByVal sfilepath As String) As String
        'Dim sExt As String
        Dim sImagePath, sImagePath2 As String
        Try
            Dim url As String = sfilepath
            sExt = System.IO.Path.GetExtension(sfilepath)
            sImagePath = objIndex.GetImagePath(sSession.AccessCode, sSession.AccessCodeID)
            sImagePath = sImagePath & "\Temp\UrlUpload\" & sSession.UserID & "\"
            objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sImagePath)
            sImagePath = sImagePath & iBaseName & "_ed" & sExt
            Using client As Net.WebClient = New Net.WebClient()
                client.DownloadFile(New Uri(url), sImagePath)
            End Using
            sImagePath2 = sImagePath & iBaseName & sExt
            objclsEdictGeneral.FileEn(sImagePath, sImagePath2)
            Dim sfilename2 As String = System.IO.Path.GetFileName(sfilepath)
            RemoveFiles(sSession.UserID, sfilename2)

            Return sImagePath2
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Urlenp" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Async Function RemoveFiles(ByVal patnerid As Integer, ByVal filename As String) As Task
        Dim dt As New DataTable
        Try
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Dim URL As String
            URL = String.Format("https://edictcore.multimedia.interactivedns.com/api/main/removefile?patnerid={0}&filename={1}", patnerid, filename)
            Dim json As String = (New WebClient).DownloadString(URL)
            dt = JsonConvert.DeserializeObject(Of DataTable)(json)
            Session("Attachment") = Nothing

            objDB.SavedDocTracker(sSession.AccessCode, sSession.AccessCodeID, 0, sSession.UserID, sSession.IPAddress, filename, sExt, "Indexing")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "RemoveFiles" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Private Function Uncheck()
        Dim lnkFileName As New LinkButton
        Try
            For i = 0 To gvattach.Rows.Count - 1
                lnkFileName = gvattach.Rows(i).FindControl("lnkFileName")
                lnkFileName.Font.Bold = False
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Uncheck" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub gvattach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvattach.RowCommand
        Dim lblPath As New Label
        Dim sFile As String = ""
        Dim lnkFileName As New LinkButton
        Dim sTempPath As String = ""
        Try
            lblError.Text = ""
            If e.CommandName.Equals("View") Then

                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblPath = DirectCast(clickedRow.FindControl("lblPath"), Label)

                Dim clickedRow1 As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lnkFileName = DirectCast(clickedRow1.FindControl("lnkFileName"), LinkButton)

                Uncheck()
                sFile = lblPath.Text
                sExt = Path.GetExtension(sFile)
                sExt = sExt.Remove(0, 1)
                sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")
                sTempPath = objclsEdictGeneral.GetDecPathView(sTempPath, sSession.UserID, sFile, Path.GetFileNameWithoutExtension(sFile), sExt)

                Select Case UCase(sExt)
                    Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                        Dim bytes As Byte() = System.IO.File.ReadAllBytes(sTempPath)
                        Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                        Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                        lnkFileName.Font.Bold = True
                    ' documentViewer.Document = sTempPath
                    Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PDF", "PPS", "XLR", "XML", "TIF"
                        clickedRow1 = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                        lnkFileName = DirectCast(clickedRow1.FindControl("lnkFileName"), LinkButton)
                        lnkFileName.Font.Bold = True
                        '   documentViewer.Document = sTempPath
                End Select
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvDocumentType_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDocumentType.RowDataBound
        Dim lblDataType As New Label
        Dim imgValues As New ImageButton
        Dim sDescriptor As String, sSearch As String
        Try
            Dim pnlCalendar As New Panel
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblDataType = CType(e.Row.FindControl("lblDataType"), Label)
                imgValues = CType(e.Row.FindControl("imgValues"), ImageButton)
                pnlCalendar = CType(e.Row.FindControl("pnlCalendar"), Panel)
                sSearch = "Date"
                sDescriptor = lblDataType.Text
                If (sDescriptor.IndexOf(sSearch) <> -1) Then
                    pnlCalendar.Visible = True
                Else
                    pnlCalendar.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDocumentType_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvDocumentType_PreRender(sender As Object, e As EventArgs) Handles gvDocumentType.PreRender
        Try
            If gvDocumentType.Rows.Count > 0 Then
                gvDocumentType.UseAccessibleHeader = True
                gvDocumentType.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDocumentType.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDocumentType_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvKeywords_PreRender(sender As Object, e As EventArgs) Handles gvKeywords.PreRender
        Try
            If gvKeywords.Rows.Count > 0 Then
                gvKeywords.UseAccessibleHeader = True
                gvKeywords.HeaderRow.TableSection = TableRowSection.TableHeader
                gvKeywords.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvKeywords_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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

        End Try
    End Function
End Class
