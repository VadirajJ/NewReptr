Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Public Class ReportTemplateMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ReportTemplateMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsReportTemplate As New clsReportTemplate
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsGRACePermission As New clsGRACePermission

    Private sSession As AllSession
    Private Shared dtReports As New DataTable
    'Private Shared sSave As String
    'Private Shared sReport As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"

        btnUpArrow.ImageUrl = "~/Images/Upload24.png"
        btnDownArrow.ImageUrl = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False : imgbtnReport.Visible = False
                'sSave = "NO" : sReport = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MRTRTM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '        sSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        sReport = "YES"
                '    End If
                'End If
                BindReportType()
                RFVModules.ErrorMessage = "Select Module." : RFVModules.InitialValue = 0
                RFVFunction.ErrorMessage = "Select Report Type." : RFVFunction.InitialValue = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFunction" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub ClearAll()
        Dim x As Integer
        Dim chkReoprt As New CheckBox
        Try
            ddlModules.SelectedIndex = 0
            If ddlReportType.SelectedIndex > 0 Then
                ddlReportType.SelectedIndex = 0
            End If
            lstDes.Items.Clear()
            gvReport.DataSource = Nothing
            gvReport.DataBind()
            btnAdd.Visible = False
            btnUpArrow.Visible = False
            btnDownArrow.Visible = False
            For x = 0 To gvReport.Rows.Count - 1
                chkReoprt = gvReport.Rows(x).FindControl("chkReoprt")
                chkReoprt.Checked = False
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sContenctId As String = "", sSortId As String = ""
        Dim i As Integer = 0, r As Integer
        Try
            lblError.Text = ""
            If lstDes.Items.Count > 0 Then
                For i = 0 To lstDes.Items.Count - 1
                    sContenctId = sContenctId & "," & lstDes.Items(i).Value
                Next
                If sContenctId.StartsWith(",") Then
                    sContenctId = sContenctId.Remove(0, 1)
                End If
                If sContenctId.EndsWith(",") Then
                    sContenctId = sContenctId.Remove(Len(sContenctId) - 1, 1)
                End If
                For r = 1 To lstDes.Items.Count
                    sSortId = sSortId & "," & r
                Next
                If sSortId.StartsWith(",") Then
                    sSortId = sSortId.Remove(0, 1)
                End If
                If sSortId.EndsWith(",") Then
                    sSortId = sContenctId.Remove(Len(sSortId) - 1, 1)
                End If
            End If
            If ddlModules.SelectedIndex = 0 Then
                ddlModules.Focus()
                lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-waring');$('#ModelReportTemplateValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlReportType.SelectedIndex = 0 Then
                ddlReportType.Focus()
                lblError.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-waring');$('#ModelReportTemplateValidation').modal('show');", True)
                Exit Sub
            End If
            objclsReportTemplate.TEM_Module = ddlModules.SelectedItem.Text
            objclsReportTemplate.TEM_FunctionId = ddlReportType.SelectedValue
            objclsReportTemplate.TEM_ReportTitle = 0
            objclsReportTemplate.TEM_ContentId = sContenctId
            objclsReportTemplate.TEM_SortOrder = sSortId
            objclsReportTemplate.TEM_CompID = sSession.AccessCodeID
            objclsReportTemplate.TEM_Yearid = sSession.YearID
            objclsReportTemplate.TEM_CrBy = sSession.UserID
            objclsReportTemplate.TEM_UpdatedBy = sSession.UserID
            objclsReportTemplate.TEM_IPAddress = sSession.IPAddress
            Arr = objclsReportTemplate.SaveReportTemplate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objclsReportTemplate)
            ddlReportType_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated." : lblModelReportTemplateValidationMsg.Text = "Successfully Updated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Template Master", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved." : lblModelReportTemplateValidationMsg.Text = "Successfully Saved."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Template Master", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModelReportTemplateValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvReport.Rows.Count - 1
                    chkField = gvReport.Rows(iIndx).FindControl("chkReoprt")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvReport.Rows.Count - 1
                    chkField = gvReport.Rows(iIndx).FindControl("chkReoprt")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportType.SelectedIndexChanged
        Dim dtReport As New DataTable
        Dim sContentIDs As String = "", sSql As String = ""
        Dim aArray As Array = Nothing
        Dim dt1 As New DataTable, dt As New DataTable, dtRep As New DataTable
        Dim i As Integer, j As Integer, r As Integer, k As Integer, x As Integer
        Dim lblID As New Label
        Dim dRow As DataRow
        Dim dc As DataColumn, dcReport As DataColumn
        Dim chkReoprt As New CheckBox
        Try
            lblError.Text = ""
            lstDes.Items.Clear()
            gvReport.DataSource = Nothing
            gvReport.DataBind()
            lstDes.Visible = False : btnAdd.Visible = False : btnUpArrow.Visible = False : btnDownArrow.Visible = False : imgbtnReport.Visible = False
            If ddlReportType.SelectedIndex > 0 Then
                gvReport.Visible = True
                dtReport = objclsReportTemplate.LoadReortTemplateToGrid(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedValue)
                gvReport.DataSource = dtReport
                gvReport.DataBind()
                If dtReport.Rows.Count > 0 Then
                    btnAdd.Visible = True : lstDes.Visible = True : btnUpArrow.Visible = True : btnDownArrow.Visible = True
                Else
                    lblError.Text = "Enter Heading & Description in Report Content."
                    lblModelReportTemplateValidationMsg.Text = "Enter Heading & Description in Report Content."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModelReportTemplateValidation').modal('show');", True)
                    Exit Sub
                End If
                imgbtnReport.Visible = True

                For x = 0 To gvReport.Rows.Count - 1
                    chkReoprt = gvReport.Rows(x).FindControl("chkReoprt")
                    chkReoprt.Checked = False
                Next

                dc = New DataColumn("Id", GetType(String))
                dt.Columns.Add(dc)
                dc = New DataColumn("Heading", GetType(String))
                dt.Columns.Add(dc)
                dc = New DataColumn("Details", GetType(String))
                dt.Columns.Add(dc)
                dcReport = New DataColumn("SlNo", GetType(String))
                dtRep.Columns.Add(dcReport)
                dcReport = New DataColumn("Id", GetType(String))
                dtRep.Columns.Add(dcReport)
                dcReport = New DataColumn("Heading", GetType(String))
                dtRep.Columns.Add(dcReport)
                dcReport = New DataColumn("Details", GetType(String))
                dtRep.Columns.Add(dcReport)

                sContentIDs = objclsReportTemplate.GetReportTemplateID(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedValue, ddlModules.SelectedItem.Text, 0)
                If sContentIDs <> "" Then
                    aArray = sContentIDs.Split(",")
                    For k = 0 To gvReport.Rows.Count - 1
                        For r = 0 To UBound(aArray)
                            If aArray(r) <> "" Or aArray(r) <> String.Empty Then
                                chkReoprt = gvReport.Rows(k).FindControl("chkReoprt")
                                lblID = gvReport.Rows(k).FindControl("lblDRLID")
                                If aArray(r) = lblID.Text Then
                                    chkReoprt.Checked = True
                                End If
                            End If
                        Next
                    Next
                End If
                If sContentIDs <> "" Then
                    For i = 0 To UBound(aArray)
                        If aArray(i) <> "" Or aArray(i) <> String.Empty Then
                            dt1 = objclsReportTemplate.GetReportContentData(sSession.AccessCode, sSession.AccessCodeID, aArray(i))
                            If dt1.Rows.Count > 0 Then
                                For j = 0 To dt1.Rows.Count - 1
                                    dRow = dt.NewRow()
                                    dRow("Id") = aArray(i)
                                    dRow("Heading") = dt1.Rows(j)("RCM_Heading")
                                    dRow("Details") = dt1.Rows(j)("RCM_Description")
                                    dt.Rows.Add(dRow)

                                    dRow = dtRep.NewRow()
                                    dRow("SlNo") = i + 1
                                    dRow("Id") = aArray(i)
                                    dRow("Heading") = dt1.Rows(j)("RCM_Heading")
                                    dRow("Details") = dt1.Rows(j)("RCM_Description")
                                    dtRep.Rows.Add(dRow)
                                Next
                            End If
                        End If
                    Next

                    dtReports = dtRep
                    lstDes.DataSource = dt
                    lstDes.DataTextField = "Heading"
                    lstDes.DataValueField = "Id"
                    lstDes.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReportType_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim i As Integer
        Dim chkReoprt As CheckBox
        Dim bret As Boolean
        Dim dtReoprt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim lblID As New Label, lblList As New Label
        Try
            lblError.Text = ""
            dc = New DataColumn("Id", GetType(String))
            dtReoprt.Columns.Add(dc)
            dc = New DataColumn("Names", GetType(String))
            dtReoprt.Columns.Add(dc)
            If gvReport.Rows.Count > 0 Then
                For i = 0 To gvReport.Rows.Count - 1
                    chkReoprt = gvReport.Rows(i).FindControl("chkReoprt")
                    lblID = gvReport.Rows(i).FindControl("lblDRLID")
                    lblList = gvReport.Rows(i).FindControl("lblList")
                    If chkReoprt.Checked = True Then
                        bret = CheckForListID(dtReoprt, lblID.Text)
                        If bret = False Then
                            dr = dtReoprt.NewRow
                            dr("Id") = lblID.Text
                            dr("Names") = lblList.Text
                            dtReoprt.Rows.Add(dr)
                        End If
                    End If
                Next
                dtReports = dtReoprt
                lstDes.DataSource = dtReoprt
                lstDes.DataTextField = "Names"
                lstDes.DataValueField = "Id"
                lstDes.DataBind()
            Else
                lblError.Text = "No items to Add"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Function CheckForListID(ByVal dt As DataTable, ByVal iDocTypeID As Integer) As Boolean
        Dim i As Integer
        Try
            For i = 0 To dt.Rows.Count - 1
                If Val(dt.Rows(i).Item("Id")) = Val(iDocTypeID) Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckForListID" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Function
    Private Sub ddlModules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModules.SelectedIndexChanged
        Try
            lblError.Text = ""
            gvReport.DataSource = Nothing
            gvReport.DataBind()
            lstDes.Visible = False : btnAdd.Visible = False : imgbtnSave.Visible = False : btnUpArrow.Visible = False : btnDownArrow.Visible = False
            ddlReportType.Items.Clear()
            If ddlModules.SelectedIndex > 0 Then
                'If sSave = "YES" Then
                imgbtnSave.Visible = True
                'End If
                BindReportType()
                imgbtnReport.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlModules_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            lblError.Text = ""
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            ddlModules.SelectedIndex = 0 : ddlReportType.SelectedIndex = 0
            btnAdd.Visible = False : lstDes.Visible = False : gvReport.Visible = False : btnUpArrow.Visible = False : btnDownArrow.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvReport_PreRender(sender As Object, e As EventArgs) Handles gvReport.PreRender
        Dim dt As New DataTable
        Try
            If gvReport.Rows.Count > 0 Then
                gvReport.UseAccessibleHeader = True
                gvReport.HeaderRow.TableSection = TableRowSection.TableHeader
                gvReport.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReport_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            If dtReports.Rows.Count = 0 Then
                lblModelReportTemplateValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModelReportTemplateValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtReports)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/ReportTemplate.rdlc")
            Dim Modules As ReportParameter() = New ReportParameter() {New ReportParameter("Module", ddlModules.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Modules)

            Dim Functions As ReportParameter() = New ReportParameter() {New ReportParameter("Function", ddlReportType.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Functions)

            Dim Reports As ReportParameter() = New ReportParameter() {New ReportParameter("Report", "Report")}
            ReportViewer1.LocalReport.SetParameters(Reports)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Template", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ReportTemplate" + ".pdf")
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
        Try
            If dtReports.Rows.Count = 0 Then
                lblModelReportTemplateValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModelReportTemplateValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtReports)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/ReportTemplate.rdlc")
            Dim Modules As ReportParameter() = New ReportParameter() {New ReportParameter("Module", ddlModules.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Modules)

            Dim Functions As ReportParameter() = New ReportParameter() {New ReportParameter("Function", ddlReportType.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Functions)

            Dim Reports As ReportParameter() = New ReportParameter() {New ReportParameter("Report", "Report")}
            ReportViewer1.LocalReport.SetParameters(Reports)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Template", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ReportTemplate" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub

    Private Sub gvReport_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvReport.RowDataBound
    End Sub

    Private Sub gvReport_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvReport.RowEditing

    End Sub

    Private Sub gvReport_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvReport.RowCommand

    End Sub
    Private Sub btnUpArrow_Click(sender As Object, e As ImageClickEventArgs) Handles btnUpArrow.Click
        lblError.Text = ""
        If lstDes.SelectedIndex = -1 Then
            lblModelReportTemplateValidationMsg.Text = "Select Heading Which you want to Move." : lblError.Text = "Select Heading Which you want to Move."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModelReportTemplateValidation').modal('show');", True)
            Exit Sub
        End If
        If lstDes.Items.Count > 0 Then
            Dim index As Integer = lstDes.SelectedIndex - 1
            If Index >= 0 Then
                lstDes.Items.Insert(lstDes.SelectedIndex + 1, lstDes.Items(lstDes.SelectedIndex - 1))
                lstDes.Items.RemoveAt(lstDes.SelectedIndex - 1)
            End If
        End If
    End Sub
    Private Sub btnDownArrow_Click(sender As Object, e As ImageClickEventArgs) Handles btnDownArrow.Click
        lblError.Text = ""
        If lstDes.SelectedIndex = -1 Then
            lblModelReportTemplateValidationMsg.Text = "Select Heading Which you want to Move." : lblError.Text = "Select Heading Which you want to Move."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModelReportTemplateValidation').modal('show');", True)
            Exit Sub
        End If
        If lstDes.Items.Count > 0 Then
            Dim index As Integer = lstDes.SelectedIndex + 1
            If Index <= lstDes.Items.Count - 1 Then
                lstDes.Items.Insert(lstDes.SelectedIndex, lstDes.Items(lstDes.SelectedIndex + 1))
                lstDes.Items.RemoveAt(lstDes.SelectedIndex + 1)
            End If
        End If
    End Sub
End Class