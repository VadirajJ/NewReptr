Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Partial Class DigitalFilingDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFilingDashboard"
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
    Private Shared iDocID As Integer
    Dim dt As New DataTable
    Private Shared iEDTPKId As Integer
    Dim dtColumns As New DataTable
    Private Shared sEmpCust As String

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnUploadDocuments.ImageUrl = "~/Images/Upload24.png"
        imgbtnIndexSave.ImageUrl = "~/Images/Save24.png"
        ' imgbtnIndex.ImageUrl = "~/Images/Index24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                'Email From
                RFVEmailFrom.ControlToValidate = "txtEmailFrom" : RFVEmailFrom.ErrorMessage = "Enter From."
                REVEmailFrom.ErrorMessage = "Enter valid E-Mail." : REVEmailFrom.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                'Password
                RFVPassword.ControlToValidate = "txtPassword" : RFVPassword.ErrorMessage = "Enter Password."
                'Email To
                REVEmailTo.ErrorMessage = "Enter valid E-Mail." : REVEmailTo.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                'Subject
                RFVSubject.ControlToValidate = "txtSubject" : RFVSubject.ErrorMessage = "Enter Subject."
                REVSubject.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVSubject.ValidationExpression = "^[\s\S]{0,200}$"
                'Body
                RFVBody.ControlToValidate = "txtBody" : RFVBody.ErrorMessage = "Enter Body."
                REVBody.ErrorMessage = "Body exceeded maximum size(max 200 characters)." : REVBody.ValidationExpression = "^[\s\S]{0,200}$"

                sEmpCust = objclsDigitalFilingDashboard.CheckEmpOrCust(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                If sEmpCust = "E" Then
                    ddlCustomer.Enabled = True
                Else
                    ddlCustomer.Enabled = False
                End If

                lblTab.Text = 1 : Tabs.Visible = True : iEDTPKId = 0 : imgbtnUploadDocuments.Visible = True
                liUploadedDocuments.Attributes.Add("class", "active") : divUploadedDocuments.Attributes.Add("class", "tab-pane active")

                'Session("Attachment") = Nothing
                'dtColumns.Columns.Add("AtchID")
                'dtColumns.Columns.Add("DFAttachID")
                'dtColumns.Columns.Add("FilePath")
                'dtColumns.Columns.Add("FileName")
                'dtColumns.Columns.Add("Extension")
                'dtColumns.Columns.Add("CreatedBy")
                'dtColumns.Columns.Add("CreatedOn")
                'Session("Attachment") = dtColumns

                Session("Attachment") = Nothing
                dtColumns.Columns.Add("AtchID")
                dtColumns.Columns.Add("DFAttachID")
                dtColumns.Columns.Add("FilePath")
                dtColumns.Columns.Add("FileName")
                dtColumns.Columns.Add("Extension")
                dtColumns.Columns.Add("CreatedBy")
                dtColumns.Columns.Add("CreatedOn")
                Session("Attachment") = dtColumns

                gvUploadedDocument.DataSource = dtColumns
                gvUploadedDocument.DataBind()

                'Dim chkSelect As New CheckBox, chkAll As New CheckBox
                'Dim iIndx As Integer
                'If gvattach.Rows.Count > 0 Then
                '    chkAll = gvattach.HeaderRow.FindControl("chkSelectAll")
                '    For iIndx = 0 To gvattach.Rows.Count - 1
                '        chkSelect = gvattach.Rows(iIndx).FindControl("chkSelect")
                '        chkSelect.Checked = True
                '    Next
                '    chkAll.Checked = True
                'End If

                BindCustomers() : BindCabinet() : BindAllAttachedDocuments() : BindUsers(sSession.UserID) : BindDocumentType()
                gvDigitalFilingDashboard.Visible = True
                gvDigitalFilingDashboard.DataSource = objclsDigitalFilingDashboard.GetDigitalFilingDashboardDetails(sSession.AccessCode)
                gvDigitalFilingDashboard.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub BindCabinet()
        Try
            ddlCabinet.DataSource = objIndex.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_ID"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCabinet" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindSubCabinet(ByVal iCabinetID As Integer)
        Try
            ddlSubcabinet.DataSource = objIndex.LoadSubCabinet(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue)
            ddlSubcabinet.DataTextField = "CBN_NAME"
            ddlSubcabinet.DataValueField = "CBN_ID"
            ddlSubcabinet.DataBind()
            ddlSubcabinet.Items.Insert(0, "Select Sub-Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSubCabinet" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindFolder(ByVal iSubCabinetID As Integer)
        Try
            ddlFolder.DataSource = objIndex.LoadFolder(sSession.AccessCode, sSession.AccessCodeID, ddlSubcabinet.SelectedValue)
            ddlFolder.DataTextField = "FOL_Name"
            ddlFolder.DataValueField = "Fol_FolID"
            ddlFolder.DataBind()
            ddlFolder.Items.Insert(0, "Select Folder")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFolder" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindDocumentType()
        Try
            ddlType.DataSource = objIndex.LoadDocumentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlType.DataTextField = "DOT_DOCNAME"
            ddlType.DataValueField = "DOT_DOCTYPEID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, "Select Document Type")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDocumentType" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindUsers(ByVal iUserID As Integer)
        Try
            lstUsers.DataSource = objclsDataCapture.LoadUser(sSession.AccessCode, sSession.AccessCodeID, iUserID)
            lstUsers.DataTextField = "Usr_FullName"
            lstUsers.DataValueField = "Usr_ID"
            lstUsers.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindUsers" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindAllAttachedDocuments()
        Dim dt As New DataTable
        Try
            If ddlCustomer.SelectedIndex > 0 Then
                dt = objclsDigitalFilingDashboard.LoadAllAttachedDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCustomer.SelectedValue, sEmpCust)
            Else
                dt = objclsDigitalFilingDashboard.LoadAllAttachedDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 0, sEmpCust)
            End If

            If dt.Rows.Count > 0 Then
                gvUploadedDocument.DataSource = dt
                gvUploadedDocument.DataBind()
            Else
                Dim dtEmpty As New DataTable
                gvUploadedDocument.DataSource = dtEmpty
                gvUploadedDocument.DataBind()
                lblError.Text = "No Documents found." : lblDigitalFilingDashboardValidationMsg.Text = "No Documents found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAttachedDocuments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindAllSharedDocumentsDocuments()
        Dim dt As New DataTable, dtIndex As New DataTable
        Try
            If ddlCustomer.SelectedIndex > 0 Then
                dt = objclsDigitalFilingDashboard.LoadAllSharedDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, ddlCustomer.SelectedValue, sEmpCust)
                dtIndex = objclsDigitalFilingDashboard.LoadAllSharedIndexDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, ddlCustomer.SelectedValue, sEmpCust)
            Else
                dt = objclsDigitalFilingDashboard.LoadAllSharedDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, 0, sEmpCust)
                dtIndex = objclsDigitalFilingDashboard.LoadAllSharedIndexDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, 0, sEmpCust)
            End If

            If dt.Rows.Count > 0 Then
                gvSharedDocuments.DataSource = dt
                gvSharedDocuments.DataBind()
            Else
                Dim dtEmpty As New DataTable
                gvSharedDocuments.DataSource = dtEmpty
                gvSharedDocuments.DataBind()
                lblError.Text = "Shared Documents not found." : lblDigitalFilingDashboardValidationMsg.Text = "Shared Documents not found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
            If dtIndex.Rows.Count > 0 Then
                gvSharedIndexDocuments.DataSource = dtIndex
                gvSharedIndexDocuments.DataBind()
            Else
                Dim dtEmpty As New DataTable
                gvSharedIndexDocuments.DataSource = dtEmpty
                gvSharedIndexDocuments.DataBind()
                lblError.Text = "Indexed Documents not found." : lblDigitalFilingDashboardValidationMsg.Text = "Indexed Documents not found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
            If dt.Rows.Count = 0 And dtIndex.Rows.Count = 0 Then
                lblError.Text = "Shared and Indexed Documents not found." : lblDigitalFilingDashboardValidationMsg.Text = "Shared and Indexed Documents not found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllSharedDocumentsDocuments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindAllActivitiesofDocuments()
        Dim dt As New DataTable
        Try
            If ddlCustomer.SelectedIndex > 0 Then
                dt = objclsDigitalFilingDashboard.LoadAllActivitiesofDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCustomer.SelectedValue, sEmpCust)
            Else
                dt = objclsDigitalFilingDashboard.LoadAllActivitiesofDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 0, sEmpCust)
            End If

            If dt.Rows.Count > 0 Then
                gvActivity.DataSource = dt
                gvActivity.DataBind()
            Else
                Dim dtEmpty As New DataTable
                gvActivity.DataSource = dtEmpty
                gvActivity.DataBind()
                lblError.Text = "No Documents found." : lblDigitalFilingDashboardValidationMsg.Text = "No Documents found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllActivitiesofDocuments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub BindIndexDetails(ByVal iPGEBASENAME As Integer)
        Dim dt As New DataTable, dtDocumentType As New DataTable, dtKey As New DataTable
        Try
            lblError.Text = "" : lblModelError.Text = ""
            dt = objclsDigitalFilingDashboard.LoadIndexDetails(sSession.AccessCode, sSession.AccessCodeID, iPGEBASENAME)
            If dt.Rows.Count > 0 Then
                'CABINET
                ddlCabinet.SelectedIndex = 0
                If IsDBNull(dt.Rows(0).Item("PGE_CABINET")) = False Then
                    Dim liCabinetID As ListItem = ddlCabinet.Items.FindByValue(Val(dt.Rows(0).Item("PGE_CABINET")))
                    If IsNothing(liCabinetID) = False Then
                        ddlCabinet.SelectedValue = Val(dt.Rows(0).Item("PGE_CABINET"))
                    End If
                End If

                'Sub CABINET
                If ddlCabinet.SelectedIndex > 0 Then
                    BindSubCabinet(ddlCabinet.SelectedValue)
                    ddlSubcabinet.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("PGE_SubCabinet")) = False Then
                        Dim liSubCabinetID As ListItem = ddlSubcabinet.Items.FindByValue(Val(dt.Rows(0).Item("PGE_SubCabinet")))
                        If IsNothing(liSubCabinetID) = False Then
                            ddlSubcabinet.SelectedValue = Val(dt.Rows(0).Item("PGE_SubCabinet"))
                        End If
                    End If
                Else
                    ddlSubcabinet.Items.Clear()
                End If

                'Folder
                If ddlSubcabinet.SelectedIndex > 0 Then
                    BindFolder(ddlSubcabinet.SelectedValue)
                    ddlFolder.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("PGE_FOLDER")) = False Then
                        Dim liFolderID As ListItem = ddlFolder.Items.FindByValue(Val(dt.Rows(0).Item("PGE_FOLDER")))
                        If IsNothing(liFolderID) = False Then
                            ddlFolder.SelectedValue = Val(dt.Rows(0).Item("PGE_FOLDER"))
                        End If
                    End If
                Else
                    ddlFolder.Items.Clear()
                End If

                'DOCUMENT TYPE
                If ddlFolder.SelectedIndex > 0 Then
                    BindDocumentType()
                    ddlType.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("PGE_DOCUMENT_TYPE")) = False Then
                        Dim liDocTypeID As ListItem = ddlType.Items.FindByValue(Val(dt.Rows(0).Item("PGE_DOCUMENT_TYPE")))
                        If IsNothing(liDocTypeID) = False Then
                            ddlType.SelectedValue = Val(dt.Rows(0).Item("PGE_DOCUMENT_TYPE"))
                        End If
                    End If
                Else
                    ddlType.Items.Clear()
                End If

                'Date
                lblDateDisplay.Text = ""
                If IsDBNull(dt.Rows(0)("PGE_DATE")) = False Then
                    lblDateDisplay.Text = objclsEdictGeneral.ReplaceSafeSQL(dt.Rows(0)("PGE_DATE"))
                End If

                'Title
                txtTitle.Text = ""
                If IsDBNull(dt.Rows(0)("PGE_TITLE")) = False Then
                    txtTitle.Text = objclsEdictGeneral.ReplaceSafeSQL(dt.Rows(0)("PGE_TITLE"))
                End If

                If ddlType.SelectedIndex > 0 Then
                    dtDocumentType = objclsDigitalFilingDashboard.LoadDescriptors(sSession.AccessCode, sSession.AccessCodeID, ddlType.SelectedValue, dt.Rows(0).Item("PGE_BASENAME"))
                    gvDocumentType.DataSource = dtDocumentType
                    gvDocumentType.DataBind()

                    dtKey = objclsDigitalFilingDashboard.LoadKeyWords(sSession.AccessCode, sSession.AccessCodeID, ddlType.SelectedValue, dt.Rows(0).Item("PGE_BASENAME"))
                    gvKeywords.DataSource = dtKey
                    gvKeywords.DataBind()
                Else
                    gvDocumentType.Visible = False : gvKeywords.Visible = False
                    gvDocumentType.DataSource = Nothing
                    gvDocumentType.DataBind()
                    gvKeywords.DataSource = Nothing
                    gvKeywords.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindIndexDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomer.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If lblTab.Text = 1 Then
                BindAllAttachedDocuments()
            ElseIf lblTab.Text = 2 Then
                BindAllSharedDocumentsDocuments()
            ElseIf lblTab.Text = 3 Then
                BindAllActivitiesofDocuments()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomer_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlCabinet.SelectedIndex > 0 Then
                BindSubCabinet(ddlCabinet.SelectedValue)
            Else
                ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblError.Text = "Select Cabinet." : lblDigitalFilingDashboardValidationMsg.Text = "Select Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlSubcabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubcabinet.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlCabinet.SelectedIndex > 0 And ddlSubcabinet.SelectedIndex > 0 Then
                BindFolder(ddlSubcabinet.SelectedValue)
            Else
                ddlFolder.Items.Clear() : ddlType.Items.Clear()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubcabinet_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlFolder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFolder.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlFolder.SelectedIndex > 0 Then
                BindDocumentType()
            Else
                ddlType.Items.Clear()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFolder_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Dim dt As New DataTable, dtKey As New DataTable
        Try
            lblError.Text = "" : lblModelError.Text = ""
            gvDocumentType.Visible = True : gvKeywords.Visible = True

            dt = objclsDigitalFilingDashboard.LoadDescriptors(sSession.AccessCode, sSession.AccessCodeID, ddlType.SelectedValue, 0)
            gvDocumentType.DataSource = dt
            gvDocumentType.DataBind()

            dtKey = objclsDigitalFilingDashboard.LoadKeyWords(sSession.AccessCode, sSession.AccessCodeID, ddlType.SelectedValue, 0)
            gvKeywords.DataSource = dtKey
            gvKeywords.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlType_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnUploadedDocuments_Click(sender As Object, e As EventArgs) Handles lnkbtnUploadedDocuments.Click
        Try
            lblError.Text = "" : lblTab.Text = 1
            liUploadedDocuments.Attributes.Add("class", "active")
            liSharedDocuments.Attributes.Remove("class")
            liActivity.Attributes.Remove("class")

            divUploadedDocuments.Attributes.Add("class", "tab-pane active")
            divSharedDocuments.Attributes.Add("class", "tab-pane")
            divActivity.Attributes.Add("class", "tab-pane")

            gvDigitalFilingDashboard.Visible = True : imgbtnUploadDocuments.Visible = True
            BindAllAttachedDocuments()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnUploadedDocuments_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnSharedDocuments_Click(sender As Object, e As EventArgs) Handles lnkbtnSharedDocuments.Click
        Try
            lblError.Text = "" : lblTab.Text = 2
            liUploadedDocuments.Attributes.Remove("class")
            liSharedDocuments.Attributes.Add("class", "active")
            liActivity.Attributes.Remove("class")

            divUploadedDocuments.Attributes.Add("class", "tab-pane")
            divSharedDocuments.Attributes.Add("class", "tab-pane active")
            divActivity.Attributes.Add("class", "tab-pane")

            gvDigitalFilingDashboard.Visible = False : imgbtnUploadDocuments.Visible = False
            BindAllSharedDocumentsDocuments()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSharedDocuments_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnActivity_Click(sender As Object, e As EventArgs) Handles lnkbtnActivity.Click
        Try
            lblError.Text = "" : lblTab.Text = 3
            liUploadedDocuments.Attributes.Remove("class")
            liSharedDocuments.Attributes.Remove("class")
            liActivity.Attributes.Add("class", "active")

            divUploadedDocuments.Attributes.Add("class", "tab-pane")
            divSharedDocuments.Attributes.Add("class", "tab-pane")
            divActivity.Attributes.Add("class", "tab-pane active")

            gvDigitalFilingDashboard.Visible = False : imgbtnUploadDocuments.Visible = False
            BindAllActivitiesofDocuments()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnActivity_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnUploadDocuments_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUploadDocuments.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalUploadDocuments').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUploadDocuments_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
    '    Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
    '    Dim dRow As DataRow
    '    Dim sFilesNames As String
    '    Dim i As Integer = 0
    '    Dim sTempPath As String = "", sFileName As String = ""
    '    Dim sIPath As String = ""
    '    Dim sOPath As String = ""
    '    Dim NxtExit As String = ""
    '    Try
    '        lblError.Text = "" : iDocID = 0

    '        Dim hfc As HttpFileCollection = Request.Files

    '        If hfc.Count > 0 Then
    '            For i = 0 To hfc.Count - 1
    '                Dim hpf As HttpPostedFile = hfc(i)
    '                If hpf.ContentLength > 0 Then
    '                    dRow = dt.NewRow()
    '                    sFilesNames = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
    '                    Dim fileExtension As String = Path.GetExtension(hpf.FileName)
    '                    sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")

    '                    If sTempPath.EndsWith("\") = True Then
    '                        sTempPath = sTempPath & "Temp\Upload\"
    '                    Else
    '                        sTempPath = sTempPath & "Temp\Upload\"
    '                    End If
    '                    dt = Session("Attachment")
    '                    If dt.Rows.Count = 0 Then
    '                        objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sTempPath)
    '                        ' hpf.SaveAs(sTempPath & sFilesNames & "_ed" & fileExtension)
    '                        hpf.SaveAs(sTempPath & sFilesNames & fileExtension)

    '                        'sIPath = sTempPath & sFilesNames & "_ed" & fileExtension
    '                        sIPath = sTempPath & sFilesNames & fileExtension
    '                        sOPath = sTempPath & sFilesNames & fileExtension

    '                        ' objclsEdictGeneral.FileEn(sIPath, sOPath)
    '                        sFilesNames = sFilesNames & fileExtension
    '                        dRow = dt.NewRow()

    '                        dRow("ID") = dt.Rows.Count + 1
    '                        dRow("FilePath") = sTempPath & sFilesNames
    '                        dRow("FileName") = sFilesNames
    '                        dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
    '                        dRow("CreatedBy") = sSession.UserLoginName
    '                        dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
    '                        dt.Rows.Add(dRow)
    '                        Dim dvAttach As New DataView(dt)
    '                        dvAttach.Sort = "ID Desc"
    '                        dt = dvAttach.ToTable
    '                        Session("Attachment") = dt
    '                    ElseIf dt.Rows.Count > 0 Then
    '                        hpf.SaveAs(sTempPath & sFilesNames & "_ed" & fileExtension)

    '                        sIPath = sTempPath & sFilesNames & "_ed" & fileExtension
    '                        sOPath = sTempPath & sFilesNames & fileExtension

    '                        objclsEdictGeneral.FileEn(sIPath, sOPath)
    '                        sFilesNames = sFilesNames & fileExtension

    '                        dRow = dt.NewRow()
    '                        dRow("ID") = dt.Rows.Count + 1
    '                        dRow("FilePath") = sTempPath & sFilesNames
    '                        dRow("FileName") = sFilesNames
    '                        dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
    '                        dRow("CreatedBy") = sSession.UserLoginName
    '                        dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
    '                        dt.Rows.Add(dRow)
    '                        Dim dvAttach As New DataView(dt)
    '                        dvAttach.Sort = "ID Desc"
    '                        dt = dvAttach.ToTable
    '                        Session("Attachment") = dt
    '                    End If
    '                End If
    '            Next
    '        End If

    '        If dt.Rows.Count = 0 Then
    '            lblError.Text = "No file to Attach."
    '        End If
    '        Session("Attachment") = dt
    '        gvattach.DataSource = dt
    '        gvattach.DataBind()
    '        dt.Clear()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAttch_Click")
    '    End Try
    'End Sub
    Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Dim lblPath As New Label, lblDescriptorID As New Label
        Dim ddlUpdateDocumentType As New DropDownList
        Dim sKeywords As String = "", sFilePath As String, sFileName As String, sISDB As String, sPath As String = ""
        Dim Arr() As String
        Dim iPageDetailsid As Integer = 0, iPageID As Integer = 0, fileSize As Integer, iDFAttachID As Integer = 0
        Dim objFile As FileStream
        Dim dtCheckData As New DataTable
        Try
            lblError.Text = "" : iEDTPKId = 0
            Dim hfc As HttpFileCollection = Request.Files
            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dtColumns.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        dtCheckData = Session("Attachment")
                        If dtCheckData.Rows.Count > 0 Then
                            dtCheckData.Clear()
                            Session("Attachment") = dtCheckData
                        End If

                        dtColumns = Session("Attachment")

                        If dtColumns.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            If Directory.Exists(Server.MapPath(".") & "\Images\") = False Then
                                Directory.CreateDirectory(Server.MapPath(".") & "\Images\")
                            End If
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
                For i = 0 To dtColumns.Rows.Count - 1
                    sFilePath = dtColumns.Rows(i)("FilePath")
                    sFileName = dtColumns.Rows(i)("FileName")

                    If System.IO.File.Exists(sFilePath) = True Then
                        iDFAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFilePath, sSession.UserID, 0)
                    End If

                    objFile = New FileStream(sFilePath, FileMode.Open)
                    fileSize = CType(objFile.Length, Integer)

                    objclsDigitalFilingDashboard.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                    objclsDigitalFilingDashboard.iPGEFOLDER = 0
                    objclsDigitalFilingDashboard.iPGECABINET = 0
                    objclsDigitalFilingDashboard.iPGEDOCUMENTTYPE = 0

                    objclsDigitalFilingDashboard.sPGETITLE = objclsEdictGeneral.SafeSQL(txtTitle.Text.Trim)
                    lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                    objclsDigitalFilingDashboard.dPGEDATE = lblDateDisplay.Text

                    If iPageDetailsid = 0 Then
                        iPageDetailsid = objclsDigitalFilingDashboard.iPGEBASENAME
                        objclsDigitalFilingDashboard.iPgeDETAILSID = iPageDetailsid
                    End If
                    objclsDigitalFilingDashboard.iPgeCreatedBy = sSession.UserID
                    objclsDigitalFilingDashboard.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                    objclsDigitalFilingDashboard.sPGEEXT = dtColumns.Rows(i)("Extension")
                    objclsDigitalFilingDashboard.sPGEKeyWORD = ""
                    objclsDigitalFilingDashboard.sPGEOCRText = ""
                    objclsDigitalFilingDashboard.iPGESIZE = fileSize
                    objclsDigitalFilingDashboard.iPGECURRENT_VER = 0
                    Select Case UCase(dtColumns.Rows(i)("Extension"))
                        Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                            objclsDigitalFilingDashboard.sPGEOBJECT = "IMAGE"
                        Case Else
                            objclsDigitalFilingDashboard.sPGEOBJECT = "OLE"
                    End Select
                    objclsDigitalFilingDashboard.sPGESTATUS = "U"
                    objclsDigitalFilingDashboard.iPGESubCabinet = 0
                    objclsDigitalFilingDashboard.iPgeUpdatedBy = sSession.UserID

                    objclsDigitalFilingDashboard.spgeDelflag = "A"
                    objclsDigitalFilingDashboard.iPGEQCUsrGrpId = 0
                    objclsDigitalFilingDashboard.sPGEFTPStatus = "F"
                    objclsDigitalFilingDashboard.iPGEbatchname = objclsDigitalFilingDashboard.iPGEBASENAME
                    objclsDigitalFilingDashboard.spgeOrignalFileName = objclsEdictGeneral.SafeSQL(sFileName)
                    objclsDigitalFilingDashboard.iPGEBatchID = iDFAttachID
                    objclsDigitalFilingDashboard.iPGEOCRDelFlag = 0
                    objclsDigitalFilingDashboard.iPgeCompID = sSession.AccessCodeID
                    Arr = objclsDigitalFilingDashboard.SavePage(sSession.AccessCode, sSession.AccessCodeID, objclsDigitalFilingDashboard)
                    sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                    FilePageInEdict(objclsDigitalFilingDashboard.iPGEBASENAME, sFilePath, UCase(sISDB))
                    objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objclsDigitalFilingDashboard.iPGEBASENAME, iPageID)

                    If objclsDigitalFilingDashboard.iPGEBASENAME = iPageDetailsid Then
                        objclsDigitalFilingDashboard.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, 0, 0, objclsDigitalFilingDashboard.sPGEKeyWORD, "")
                    End If
                Next

                BindAllAttachedDocuments()

                If Arr(0) = "3" Then
                    lblDigitalFilingDashboardValidationMsg.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub imgbtnIndexSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnIndexSave.Click
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        Dim ddlUpdateDocumentType As New DropDownList
        Try
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
                lblModelError.Text = "Select Document Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlType.Focus()
                Exit Sub
            Else
                iType = ddlType.SelectedValue
            End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                sPageExt = "" : sFilePath = "" : sFileName = ""
                objclsDigitalFilingDashboard.iPGEBASENAME = iEDTPKId
                objclsDigitalFilingDashboard.iPGEFOLDER = iFolder
                objclsDigitalFilingDashboard.iPGECABINET = icabinetID
                objclsDigitalFilingDashboard.iPGEDOCUMENTTYPE = iType
                objclsDigitalFilingDashboard.sPGETITLE = objclsEdictGeneral.SafeSQL(txtTitle.Text.Trim)
                dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objclsDigitalFilingDashboard.dPGEDATE = dDate
                If iPageDetailsid = 0 Then
                    iPageDetailsid = objclsDigitalFilingDashboard.iPGEBASENAME
                    objclsDigitalFilingDashboard.iPgeDETAILSID = iPageDetailsid
                End If
                objclsDigitalFilingDashboard.iPgeCreatedBy = sSession.UserID
                objclsDigitalFilingDashboard.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                objclsDigitalFilingDashboard.sPGEEXT = sPageExt
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
                objclsDigitalFilingDashboard.sPGEKeyWORD = objclsEdictGeneral.SafeSQL(sKeywords)
                objclsDigitalFilingDashboard.sPGEOCRText = ""
                objclsDigitalFilingDashboard.iPGESIZE = 0
                objclsDigitalFilingDashboard.iPGECURRENT_VER = 0
                Select Case UCase(sPageExt)
                    Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                        objclsDigitalFilingDashboard.sPGEOBJECT = "IMAGE"
                    Case Else
                        objclsDigitalFilingDashboard.sPGEOBJECT = "OLE"
                End Select
                objclsDigitalFilingDashboard.sPGESTATUS = "A"
                objclsDigitalFilingDashboard.iPGESubCabinet = iSubCabinet
                objclsDigitalFilingDashboard.iPgeUpdatedBy = sSession.UserID
                objclsDigitalFilingDashboard.spgeDelflag = "A"
                objclsDigitalFilingDashboard.iPGEQCUsrGrpId = 0
                objclsDigitalFilingDashboard.sPGEFTPStatus = "F"
                objclsDigitalFilingDashboard.iPGEbatchname = objclsDigitalFilingDashboard.iPGEBASENAME
                objclsDigitalFilingDashboard.spgeOrignalFileName = objclsEdictGeneral.SafeSQL(sFileName)
                objclsDigitalFilingDashboard.iPGEBatchID = 0
                objclsDigitalFilingDashboard.iPGEOCRDelFlag = 0
                objclsDigitalFilingDashboard.iPgeCompID = sSession.AccessCodeID
                Arr = objclsDigitalFilingDashboard.SavePage(sSession.AccessCode, sSession.AccessCodeID, objclsDigitalFilingDashboard)
                FilePageInEdict(objclsDigitalFilingDashboard.iPGEBASENAME, sFilePath, "True")
                objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objclsDigitalFilingDashboard.iPGEBASENAME, iPageID)

                If gvDocumentType.Rows.Count > 0 Then
                    For j = 0 To gvDocumentType.Rows.Count - 1
                        lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                        txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                        If objclsDigitalFilingDashboard.iPGEBASENAME = iPageDetailsid Then
                            objclsDigitalFilingDashboard.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objclsDigitalFilingDashboard.sPGEKeyWORD, txtValues.Text)
                        End If
                    Next
                End If

                BindAllAttachedDocuments()

                If Arr(0) = "2" Then
                    lblDigitalFilingDashboardValidationMsg.Text = "Successfully Indexed."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnIndexSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "FilePageInEdict" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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
    Private Sub gvSharedDocuments_PreRender(sender As Object, e As EventArgs) Handles gvSharedDocuments.PreRender
        Try
            If gvSharedDocuments.Rows.Count > 0 Then
                gvSharedDocuments.UseAccessibleHeader = True
                gvSharedDocuments.HeaderRow.TableSection = TableRowSection.TableHeader
                gvSharedDocuments.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSharedDocuments_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvSharedIndexDocuments_PreRender(sender As Object, e As EventArgs) Handles gvSharedIndexDocuments.PreRender
        Try
            If gvSharedIndexDocuments.Rows.Count > 0 Then
                gvSharedIndexDocuments.UseAccessibleHeader = True
                gvSharedIndexDocuments.HeaderRow.TableSection = TableRowSection.TableHeader
                gvSharedIndexDocuments.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSharedIndexDocuments_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvActivity_PreRender(sender As Object, e As EventArgs) Handles gvActivity.PreRender
        Try
            If gvActivity.Rows.Count > 0 Then
                gvActivity.UseAccessibleHeader = True
                gvActivity.HeaderRow.TableSection = TableRowSection.TableHeader
                gvActivity.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvActivity_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDigitalFilingDashboard_PreRender(sender As Object, e As EventArgs) Handles gvDigitalFilingDashboard.PreRender
        Try
            If gvDigitalFilingDashboard.Rows.Count > 0 Then
                gvDigitalFilingDashboard.UseAccessibleHeader = True
                gvDigitalFilingDashboard.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDigitalFilingDashboard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDigitalFilingDashboard_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDigitalFilingDashboard_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDigitalFilingDashboard.RowCommand
        Dim lblCabinetID As New Label, lblFolderID As New Label
        Dim oPGE_CABINET As New Object, oPGE_SubCABINET As New Object, oPGE_FOLDER As New Object
        Try
            lblError.Text = ""
            If e.CommandName = "Document" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCabinetID = DirectCast(clickedRow.FindControl("lblCabinetID"), Label)
                lblFolderID = DirectCast(clickedRow.FindControl("lblFolderID"), Label)

                oPGE_CABINET = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblCabinetID.Text)))
                oPGE_SubCABINET = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(0)))
                oPGE_FOLDER = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblFolderID.Text)))

                'Response.Redirect(String.Format("~/Search/Search.aspx?PGE_CABINET={0}&PGE_SUBCABINET={1}&PGE_FOLDER={2}", oPGE_CABINET, oPGE_SubCABINET, oPGE_FOLDER), False)
                'rakshan added
                Response.Redirect(String.Format("~/DigitalFilling/Search.aspx?PGE_CABINET={0}&PGE_SUBCABINET={1}&PGE_FOLDER={2}", oPGE_CABINET, oPGE_SubCABINET, oPGE_FOLDER), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDigitalFilingDashboard_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvUploadedDocument_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUploadedDocument.RowCommand
        Dim lblAtchDocID As New Label, lblDFAttachID As New Label
        Dim sPaths As String, sDestFilePath As String
        Dim oAttachID As New Object
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If e.CommandName.Equals("OPENPAGE") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsDigitalFilingDashboard.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, Val(lblAtchDocID.Text))
                DownloadMyFile(sDestFilePath)
            End If

            If e.CommandName.Equals("ShareDocument") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                lblDFAttachID = DirectCast(clickedRow.FindControl("lblDFAttachID"), Label)
                oAttachID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblDFAttachID.Text)))
                iEDTPKId = (Val(lblAtchDocID.Text))
                Response.Redirect(String.Format("~/DigitalFilling/Outward.aspx?AttachID={0}", oAttachID), False)
            End If

            If e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEDTPKId = (Val(lblAtchDocID.Text))
                BindIndexDetails(Val(lblAtchDocID.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            End If

            If e.CommandName.Equals("REMOVE") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                objclsDigitalFilingDashboard.RemoveSelectedDocument(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblAtchDocID.Text))
                BindAllAttachedDocuments()
                lblError.Text = "Successfully Removed." : lblDigitalFilingDashboardValidationMsg.Text = "Successfully Removed."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDigitalFilingDashboardValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUploadedDocuments_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadMyFile" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub gvUploadedDocument_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUploadedDocument.RowDataBound 'STEFFI
        Dim imgbtnShareDocument As New ImageButton, imgbtnAdd As New ImageButton, imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                'imgbtnShareDocument = CType(e.Row.FindControl("imgbtnShareDocument"), ImageButton)
                'imgbtnShareDocument.ImageUrl = "~/Images/Share_document24.png"

                'imgbtnAdd = CType(e.Row.FindControl("imgbtnAdd"), ImageButton)
                'If (imgbtnAdd.ToString() <> Nothing) Then
                '    imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                '    imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                '    imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                '    imgbtnAdd.Visible = True : imgbtnRemove.Visible = True
                'End If
            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUploadedDocument_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDocumentType_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDocumentType.RowDataBound
        Dim txtValues As New TextBox
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                txtValues = CType(e.Row.FindControl("txtValues"), TextBox)
                If txtValues.Text <> "" Then
                    txtValues.Text = txtValues.Text
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDocumentType_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvKeywords_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvKeywords.RowDataBound
        Dim txtKeywords As New TextBox
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                txtKeywords = CType(e.Row.FindControl("txtKeywords"), TextBox)
                If txtKeywords.Text <> "" Then
                    txtKeywords.Text = txtKeywords.Text
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvKeywords_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSendMail_Click(sender As Object, e As EventArgs) Handles btnSendMail.Click
        Dim iCount As Integer = 0
        Dim sToEmailIDs As String = "", sToEmails As String = ""
        Try
            lblError.Text = "" : lblSendMailModelError.Text = ""

            For j = 0 To lstUsers.Items.Count - 1
                If lstUsers.Items(j).Selected = True Then
                    iCount = 1
                End If
            Next
            If iCount = 0 And txtEmailTo.Text = "" Then
                lblSendMailModelError.Text = "Select Users/Enter EmailID."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('show');", True)
                Exit Sub
            End If

            For j = 0 To lstUsers.Items.Count - 1
                If lstUsers.Items(j).Selected = True Then
                    sToEmailIDs = sToEmailIDs & "," & Val(lstUsers.Items(j).Value)
                    iCount = 1
                End If
            Next

            If iCount > 0 Then
                If sToEmailIDs.StartsWith(",") Then
                    sToEmailIDs = sToEmailIDs.Remove(0, 1)
                End If
                If sToEmailIDs.EndsWith(",") Then
                    sToEmailIDs = sToEmailIDs.Remove(Len(sToEmailIDs) - 1, 1)
                End If

                sToEmails = objclsDataCapture.LoadUserEmailIDs(sSession.AccessCode, sSession.AccessCodeID, sToEmailIDs)
            End If

            If txtEmailTo.Text <> "" Then
                sToEmails = sToEmails & "," & txtEmailTo.Text
            End If

            If sToEmails.StartsWith(",") Then
                sToEmails = sToEmails.Remove(0, 1)
            End If
            If sToEmails.EndsWith(",") Then
                sToEmails = sToEmails.Remove(Len(sToEmailIDs) - 1, 1)
            End If

            SendTodaysMail(sToEmails)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSendMail_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub SendTodaysMail(ByVal sToEmailIDs As String)
        Dim sPath As String = "", sExt As String = "", sFileName As String = ""
        Dim iEmailSentUserID As Integer = 0
        Try
            Using mm As New MailMessage(txtEmailFrom.Text, sToEmailIDs)
                iEmailSentUserID = objclsDataCapture.GetEmailSentUserID(sSession.AccessCode, sSession.AccessCodeID, txtEmailFrom.Text)

                mm.Subject = txtSubject.Text
                mm.Body = txtBody.Text

                sPath = objclsSearch.GetPageFromEdict(sSession.AccessCode, iEDTPKId)

                sExt = objclsSearch.GetExtension(sSession.AccessCode, iEDTPKId)
                If sExt.StartsWith(".") Then
                    sExt = sExt.Remove(0, 1)
                End If

                sFileName = iEDTPKId & "." & sExt

                Dim sAttachment = New System.Net.Mail.Attachment(sPath)
                mm.Attachments.Add(sAttachment)

                mm.IsBodyHtml = False
                Dim smtp As New SmtpClient()
                smtp.Host = "smtp.gmail.com"
                smtp.EnableSsl = True
                Dim NetworkCred As New NetworkCredential(txtEmailFrom.Text, txtPassword.Text)
                smtp.UseDefaultCredentials = True
                smtp.Credentials = NetworkCred
                smtp.Port = 587
                Try
                    smtp.Send(mm)
                    objclsDataCapture.SaveSentEmailDetails(sSession.AccessCode, iEDTPKId, sSession.YearID, "DigitalFiling Dashboard", objclsGRACeGeneral.SafeSQL(txtEmailFrom.Text), sToEmailIDs, "", objclsGRACeGeneral.SafeSQL(txtSubject.Text), objclsGRACeGeneral.SafeSQL(txtBody.Text), "YES", "C:\GRACePA-INFO\BITMAPS\0\", sFileName, iEmailSentUserID, sSession.UserID, sSession.IPAddress, sSession.AccessCodeID)
                    objclsDataCapture.UpdateEdtPageStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iEDTPKId)
                    lblSendMailModelError.Text = "Mail sent successfully."
                Catch ex As Exception
                    objclsDataCapture.SaveSentEmailDetails(sSession.AccessCode, iEDTPKId, sSession.YearID, "DigitalFiling Dashboard", objclsGRACeGeneral.SafeSQL(txtEmailFrom.Text), sToEmailIDs, "", objclsGRACeGeneral.SafeSQL(txtSubject.Text), objclsGRACeGeneral.SafeSQL(txtBody.Text), "NO", "C:\GRACePA-INFO\BITMAPS\0\", sFileName, iEmailSentUserID, sSession.UserID, sSession.IPAddress, sSession.AccessCodeID)
                    lblSendMailModelError.Text = "Failure Sending Mail."
                End Try
            End Using
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SendTodaysMail" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub btnCancelMail_Click(sender As Object, e As EventArgs) Handles btnCancelMail.Click
        Try
            lblError.Text = "" : lblSendMailModelError.Text = ""
            txtEmailFrom.Text = "" : txtEmailTo.Text = "" : txtPassword.Text = "" : txtSubject.Text = ""
            For j = 0 To lstUsers.Items.Count - 1
                If lstUsers.Items(j).Selected = True Then
                    lstUsers.Items(j).Selected = False
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#mySendMail').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCancelMail_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim chkSelect As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        lblError.Text = ""
    '        If gvattach.Rows.Count > 0 Then
    '            chkAll = CType(sender, CheckBox)
    '            If chkAll.Checked = True Then
    '                For iIndx = 0 To gvattach.Rows.Count - 1
    '                    chkSelect = gvattach.Rows(iIndx).FindControl("chkSelect")
    '                    chkSelect.Checked = True
    '                Next
    '            Else
    '                For iIndx = 0 To gvattach.Rows.Count - 1
    '                    chkSelect = gvattach.Rows(iIndx).FindControl("chkSelect")
    '                    chkSelect.Checked = False
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
    '    End Try
    'End Sub

    'Private Sub imgbtnIndex_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnIndex.Click
    '    Dim chkSelect As CheckBox
    '    Dim iCount As Integer = 0
    '    Try
    '        If gvattach.Rows.Count > 0 Then
    '            lblError.Text = "" : lblModelError.Text = ""
    '            For i = 0 To gvattach.Rows.Count - 1
    '                chkSelect = gvattach.Rows(i).FindControl("chkSelect")
    '                If chkSelect.Checked = True Then
    '                    iCount = iCount + 1
    '                    ddlCabinet.SelectedIndex = 0 : ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : txtTitle.Text = "" : ddlType.SelectedIndex = 0 'Vijeth
    '                    ddlType_SelectedIndexChanged(sender, e)
    '                End If
    '            Next
    '            If iCount = 0 Then
    '                lblError.Text = "Select the file to Index."
    '                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select the file to Index.','', 'warning');", True) 'Vijeth
    '                Exit Sub
    '            End If
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
    '        Else
    '            lblError.Text = "Attach file to Index."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Attach file to Index','', 'warning');", True)
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnIndex_Click")
    '    End Try
    'End Sub

    'Protected Sub gvattach_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    Dim imgbtnRemove As New ImageButton
    '    Try
    '        lblError.Text = ""
    '        If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
    '            imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
    '            imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
End Class
