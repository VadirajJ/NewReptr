Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Imports System.IO
Public Class ReportContentMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ReportContentMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGRACePermission As New clsGRACePermission
    Private objDBHelper As New DBHelper
    Private sSession As AllSession
    'Private Shared sSave As String
    Private Shared dtDisplay2 As New DataTable
    Private objclsReportContentMaster As New clsReportContentMaster

    Private Shared ipkid As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                ' imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnReport.Visible = False

                'sSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MRTRCM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        sSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If
                ipkid = 0
                BindReportType()
                'ReportContentClientSideValidation()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Public Sub BindReportType()
        Try
            ddlReportType.Items.Add(New ListItem("Select Report Type", "0"))
            ddlReportType.Items.Add(New ListItem("Report on the standalone Financial Statements", "1"))
            ddlReportType.Items.Add(New ListItem("Independent Auditor's Report", "2"))
            ddlReportType.Items.Add(New ListItem("Annexure A to the Independent Auditor's Report", "3"))
            ddlReportType.Items.Add(New ListItem("Annexure B to the Independent Auditor's Report", "4"))
            ddlReportType.Items.Add(New ListItem("LOE and Information about the Auditee Report", "5"))
            ddlReportType.Items.Add(New ListItem("Management Representation Letter1", "6"))
            ddlReportType.Items.Add(New ListItem("Management Representation Letter2", "7"))
            ddlReportType.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindReportType" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub ReportContentClientSideValidation()
    '    Try
    '        RFVFunction.ErrorMessage = "Select Report Type." : RFVFunction.InitialValue = 0
    '        RFVHeading.ErrorMessage = "Enter Heading." : REVHeading.ValidationExpression = "^[\s\S]{0,500}$" : REVHeading.ErrorMessage = "Heading exceeded maximum size(max 500 character)."
    '        RFVDescription.ErrorMessage = "Enter Description." : REVDescription.ValidationExpression = "^[\s\S]{0,5000}$" : REVDescription.ErrorMessage = "Description exceeded maximum size(max 5000 character)."
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ReportContentClientSideValidation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub gvReportContentMaster_PreRender(sender As Object, e As EventArgs) Handles gvReportContentMaster.PreRender
        Dim dt As New DataTable
        Try
            If gvReportContentMaster.Rows.Count > 0 Then
                gvReportContentMaster.UseAccessibleHeader = True
                gvReportContentMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvReportContentMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReportContentMaster_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/Masters/ReportContentMaster.aspx?"), False)
            dtDisplay2.Clear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ImgBtnAddDetails_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAddDetails.Click
        Dim dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow

        Try
            lblError.Text = ""
            If ipkid = 0 Then
                If ddlReportType.SelectedIndex = 0 Then
                    lblError.Text = "Select Report Type."
                    lblReportValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    ddlReportType.Focus()
                    Exit Sub
                End If

                If txtEnterHeading.Text = "" Then
                    lblError.Text = "Enter Heading."
                    lblReportValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    txtEnterHeading.Focus()
                    Exit Sub
                End If

                Dim bCheck As Boolean = objclsReportContentMaster.CheckReportHeadingExisting(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedIndex, ipkid, objclsGRACeGeneral.SafeSQL(txtEnterHeading.Text.Trim))
                If bCheck = True Then
                    lblReportValidationMsg.Text = "Entered Heading already exist." : lblError.Text = "Entered Heading already exist."
                    txtEnterHeading.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    Exit Sub
                End If

                If txtEnterDescription.Text = "" Then
                    lblError.Text = "Enter Description."
                    lblReportValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    txtEnterDescription.Focus()
                    Exit Sub
                End If

                dtDisplay.Columns.Add("SrNo")
                dtDisplay.Columns.Add("ReportType")
                dtDisplay.Columns.Add("Heading")
                dtDisplay.Columns.Add("PKID")
                dtDisplay.Columns.Add("ReportID")
                dtDisplay.Columns.Add("Description")

                dRow = dtDisplay.NewRow
                dRow("SrNo") = i + 1
                dRow("ReportType") = ddlReportType.SelectedItem.Text
                dRow("Heading") = txtEnterHeading.Text
                dRow("PKID") = 0
                dRow("ReportID") = ddlReportType.SelectedIndex
                dRow("Description") = Replace(txtEnterDescription.Text, " ", "")
                dtDisplay.Rows.Add(dRow)

                dtDisplay2.Merge(dtDisplay)
                gvReportContentMaster.DataSource = dtDisplay2
                gvReportContentMaster.DataBind()
            End If
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnAddDetails_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr As Array
        Try
            If ddlReportType.SelectedIndex = 0 Then
                lblError.Text = "Select Report Type."
                lblReportValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                ddlReportType.Focus()
                Exit Sub
            End If

            If ipkid = 0 Then
                objclsReportContentMaster.RCM_Id = 0
            Else
                objclsReportContentMaster.RCM_Id = ipkid
            End If

            Dim bCheck As Boolean = objclsReportContentMaster.CheckReportHeadingExisting(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedIndex, ipkid, objclsGRACeGeneral.SafeSQL(txtEnterHeading.Text.Trim))
            If bCheck = True Then
                lblReportValidationMsg.Text = "Entered Heading already exist." : lblError.Text = "Entered Heading already exist."
                txtEnterHeading.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                Exit Sub
            End If

            objclsReportContentMaster.RCM_ReportName = ddlReportType.SelectedItem.Text

            If txtEnterHeading.Text = "" Then
                objclsReportContentMaster.RCM_Heading = ""
            Else
                objclsReportContentMaster.RCM_Heading = txtEnterHeading.Text
            End If
            If ddlReportType.SelectedValue = 0 Then
                objclsReportContentMaster.RCM_ReportId = 0
            Else
                objclsReportContentMaster.RCM_ReportId = ddlReportType.SelectedIndex
            End If
            If txtEnterDescription.Text = "" Then
                objclsReportContentMaster.RCM_Description = ""
            Else
                objclsReportContentMaster.RCM_Description = txtEnterDescription.Text
            End If
            objclsReportContentMaster.RCM_Delflag = "X"
            objclsReportContentMaster.RCM_Status = "A"
            objclsReportContentMaster.RCM_CrBy = sSession.UserID
            objclsReportContentMaster.RCM_CrOn = DateTime.Today
            objclsReportContentMaster.RCM_UpdatedBy = sSession.UserID
            objclsReportContentMaster.RCM_UpdatedOn = DateTime.Today
            objclsReportContentMaster.RCM_IPAddress = sSession.IPAddress
            objclsReportContentMaster.RCM_CompID = sSession.AccessCodeID
            objclsReportContentMaster.RCM_Yearid = sSession.YearID

            Arr = objclsReportContentMaster.SaveReportContentMaster(sSession.AccessCode, sSession.AccessCodeID, objclsReportContentMaster)
            ipkid = 0
            BindDetails(ddlReportType.SelectedIndex)

            If Arr(0) = "2" Then
                lblReportValidationMsg.Text = "Successfully Updated"
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content Master", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                lblReportValidationMsg.Text = "Successfully Saved"
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content Master", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
            End If
            txtEnterHeading.Text = "" : txtEnterDescription.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnsave_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019hrow
        End Try
    End Sub
    Private Sub BindDetails(ByVal iReportID As Integer)
        Try
            ipkid = 0
            dtDisplay2 = objclsReportContentMaster.BinALLDetails(sSession.AccessCode, sSession.AccessCodeID, iReportID)
            If dtDisplay2.Rows.Count > 0 Then
                gvReportContentMaster.DataSource = dtDisplay2
                gvReportContentMaster.DataBind()
            Else
                gvReportContentMaster.DataSource = Nothing
                gvReportContentMaster.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try

    End Sub
    Private Sub gvReportContentMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvReportContentMaster.RowDataBound

    End Sub
    Private Sub gvReportContentMaster_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvReportContentMaster.RowEditing

    End Sub
    Private Sub gvReportContentMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvReportContentMaster.RowCommand
        Dim dt As New DataTable, dt1 As New DataTable, dtHeading As New DataTable
        Dim drNew As DataRow
        Dim lblReportType As New Label
        Dim lblPKID As New Label
        Dim lblDescription As New Label
        Dim lblHeading As New LinkButton
        Dim lblReportID As New Label
        Dim sColumnName As String = ""
        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblPKID = DirectCast(clickedRow.FindControl("lblPKID"), Label)
                ipkid = lblPKID.Text
                lblReportType = DirectCast(clickedRow.FindControl("lblReportType"), Label)
                lblReportID = DirectCast(clickedRow.FindControl("lblReportID"), Label)
                lblHeading = DirectCast(clickedRow.FindControl("lblHeading"), LinkButton)
                lblDescription = DirectCast(clickedRow.FindControl("lblDescription"), Label)

                txtEnterHeading.Enabled = True
                ddlReportType.SelectedValue = lblReportID.Text
                If (ddlReportType.SelectedIndex = 5 And (lblHeading.Text = "Responsibilities of the Auditor" Or lblHeading.Text = "The objective and scope of the audit" Or lblHeading.Text = "Reporting" Or lblHeading.Text = "General" Or
                    lblHeading.Text = "The responsibilities of management and identification of the applicable financial reporting framework" Or lblHeading.Text = "Non Disclosure Of Confidential Information")) Then
                    txtEnterHeading.Enabled = False
                End If
                txtEnterHeading.Text = lblHeading.Text
                txtEnterDescription.Text = lblDescription.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReportTemplate_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportType.SelectedIndexChanged
        Try
            lblError.Text = ""
            txtEnterDescription.Text = ""
            txtEnterHeading.Text = ""
            txtEnterHeading.Enabled = True
            BindDetails(ddlReportType.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReportType_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            If ddlReportType.SelectedIndex > 0 Then
                dt = objclsReportContentMaster.LoadReportContentToGrid(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedValue)
            End If
            If dt.Rows.Count = 0 Then
                lblReportValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalReportValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/ReportContent.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ReportContent" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            If ddlReportType.SelectedIndex > 0 Then
                dt = objclsReportContentMaster.LoadReportContentToGrid(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedValue)
            End If
            If dt.Rows.Count = 0 Then
                lblReportValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalReportValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/ReportContent.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ReportContent" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
End Class