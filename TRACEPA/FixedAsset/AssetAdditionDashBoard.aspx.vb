Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class AssetAdditionDashBoard
    Inherits System.Web.UI.Page
    Private sFormName As String = "AssetAdditionDashBoard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared sAssetID As String
    Private Shared sPTAP As String
    Private Shared sPTED As String
    Private objAddDsb As New ClsAssetAdditionDashBoard
    Dim objGen As New clsGRACeGeneral
    'Dim dtDetails As New DataTable
    Private Shared dtDetails As New DataTable
    Private objAsst As New ClsAssetMaster
    Private Shared dt As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        ' imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadCustomer()
                BindStatus()
                LoadFinalcialYear(sSession.AccessCode)
                If sSession.CustomerID <> 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        btnYes_Click(sender, e)
                    End If
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYears(sAC, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.ScheduleYearId = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            If sSession.CustomerID <> 0 Then
                If ddlCustomerName.SelectedIndex > 0 Then
                    lblModal.Text = "Do you wish to change Customer?Click Yes to change."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                If ddlCustomerName.SelectedIndex > 0 Then
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    btnYes_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then
                sSession.CustomerID = ddlCustomerName.SelectedValue
                Session("AllSession") = sSession
                'ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
                If ddlStatus.SelectedIndex = 0 Then
                    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "A", ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                    'ElseIf ddlStatus.SelectedIndex = 1 Then
                    '    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID,ddlFinancialYear.SelectedValue, "X", ddlCustomerName.SelectedValue)
                    '    GvAsserDetails.DataSource = dt
                    '    GvAsserDetails.DataBind()
                    imgbtnWaiting.Visible = False
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "W", ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                    imgbtnWaiting.Visible = True
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    dt = objAddDsb.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                    imgbtnWaiting.Visible = False
                ElseIf ddlStatus.SelectedIndex = 3 Then
                    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "D", ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                    imgbtnWaiting.Visible = False
                ElseIf dt.Rows.Count = 0 Then
                    lblError.Text = "No Data to Display"
                    lblOpeningBalance.Text = "Opening Balance : 0"
                    lblAddition.Text = "Addition : 0"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                End If

                Try
                    If (ddlCustomerName.SelectedIndex > 0) Then
                        dt = objAddDsb.GetCountOfOpenBalAddition(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "A")
                        If (dt.Rows.Count > 0) Then
                            lblOpeningBalance.Text = "Opening Balance : 0"
                            lblAddition.Text = "Addition : 0"
                            For iIndx = 0 To dt.Rows.Count - 1
                                If (dt.Rows(iIndx)("TransactionType").ToString() = "Opening Balance") Then
                                    lblOpeningBalance.Text = "Opening Balance : " & dt.Rows(iIndx)("Counts").ToString()
                                ElseIf (dt.Rows(iIndx)("TransactionType").ToString() = "Addition") Then
                                    lblAddition.Text = "Addition : " & dt.Rows(iIndx)("Counts").ToString()
                                End If
                            Next
                        Else
                            lblOpeningBalance.Text = "Opening Balance : 0"
                            lblAddition.Text = "Addition : 0"
                        End If
                    End If
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Try
            lblError.Text = ""
            If sSession.CustomerID <> 0 Then
                ddlCustomerName.SelectedValue = sSession.CustomerID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCustomer() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objAsst.LoadCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataSource = dt
            ddlCustomerName.DataTextField = "CUST_NAME"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Approved")
            'ddlStatus.Items.Insert(1, "Deleted")
            ddlStatus.Items.Insert(1, "Waiting for Approval")
            ddlStatus.Items.Insert(2, "All")
            ddlStatus.Items.Insert(3, "Deactivated")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                btnYes_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object
        Dim oMasterName As String = ""
        Try
            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            Session("dtDetails") = Nothing
            Response.Redirect(String.Format("~/FixedAsset/AssetTransactionAddition.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub GvAsserDetails_PreRender(sender As Object, e As EventArgs) Handles GvAsserDetails.PreRender
        Try
            If GvAsserDetails.Rows.Count > 0 Then
                GvAsserDetails.UseAccessibleHeader = True
                GvAsserDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                GvAsserDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAsserDetails_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged1(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged1" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub GvAsserDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvAsserDetails.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                GvAsserDetails.Columns(0).Visible = True
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    GvAsserDetails.Columns(0).Visible = True
                    'If sAD = "YES" Then
                    GvAsserDetails.Columns(5).Visible = True
                    'Else
                    '    gvCustomers.Columns(5).Visible = False
                    'End If
                    GvAsserDetails.Columns(6).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    GvAsserDetails.Columns(0).Visible = True
                    'If sAD = "YES" Then
                    GvAsserDetails.Columns(5).Visible = True
                    'Else
                    '    gvCustomers.Columns(5).Visible = False
                    'End If
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    GvAsserDetails.Columns(0).Visible = True
                    'If sAD = "YES" Then
                    GvAsserDetails.Columns(5).Visible = True
                    'Else
                    '    gvCustomers.Columns(5).Visible = False
                    'End If
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    GvAsserDetails.Columns(0).Visible = True
                    'If sAD = "YES" Then
                    GvAsserDetails.Columns(5).Visible = True

                    imgbtnStatus.Visible = False
                    'Else
                    '    gvCustomers.Columns(5).Visible = False
                    'End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAsserDetails_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub GvAsserDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvAsserDetails.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim lblcustid As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblDescName"), Label)
            lblcustid = DirectCast(clickedRow.FindControl("AFAA_CustId"), Label)
            If e.CommandName.Equals("Edit1") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                oMasterName = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescName.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/FixedAsset/AssetTransactionAddition.aspx?StatusID={0}&MasterID={1}", oStatusID, oMasterID), False)
            ElseIf e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objAsst.AssMasDeactivate(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblDescID.Text), "D", sSession.IPAddress, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    lblAdditionValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalasserAddnValidation').modal('show');", True)
                ElseIf ddlStatus.SelectedIndex = 3 Then
                    objAsst.AssMasDeactivate(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblDescID.Text), "A", sSession.IPAddress, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    lblAdditionValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalasserAddnValidation').modal('show');", True)
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    objAsst.AssMasDeactivate(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblDescID.Text), "A", sSession.IPAddress, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    lblAdditionValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalasserAddnValidation').modal('show');", True)
                End If
                ddlFinancialYear_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAsserDetails_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub GvAsserDetails_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GvAsserDetails.RowDeleting

    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To GvAsserDetails.Rows.Count - 1
                    chkField = GvAsserDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        'Dim dt As New DataTable
        Try
            'dt = objAddDsb.GetDetails(sSession.AccessCode, sSession.AccessCodeID,ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblAdditionValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalasserAddnValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetAddDashBoard.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AssetAdditionDashBoard" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        'Dim dt As New DataTable
        Try
            'dt = objAddDsb.GetDetails(sSession.AccessCode, sSession.AccessCodeID,ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblAdditionValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalasserAddnValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetAddDashBoard.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AssetAdditionDashBoard" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        'Dim dt As New DataTable
        Try
            lblError.Text = ""
            'ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            If ddlFinancialYear.SelectedIndex > 0 Then
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            End If
            If ddlCustomerName.SelectedIndex > 0 Then
                If ddlStatus.SelectedIndex = 0 Then
                    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "A", ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                    'ElseIf ddlStatus.SelectedIndex = 1 Then
                    '    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID,ddlFinancialYear.SelectedValue, "X", ddlCustomerName.SelectedValue)
                    '    GvAsserDetails.DataSource = dt
                    '    GvAsserDetails.DataBind()
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "W", ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    dt = objAddDsb.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                ElseIf ddlStatus.SelectedIndex = 3 Then
                    dt = objAddDsb.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "D", ddlCustomerName.SelectedValue)
                    GvAsserDetails.DataSource = dt
                    GvAsserDetails.DataBind()
                ElseIf dt.Rows.Count = 0 Then
                    lblError.Text = "No Data to Display"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            If GvAsserDetails.Rows.Count = 0 Then
                lblAdditionValidationMsg.Text = "No data to Approve." : lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalasserAddnValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To GvAsserDetails.Rows.Count - 1
                chkSelect = GvAsserDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblAdditionValidationMsg.Text = "Select Employee to Approve." : lblError.Text = "Select Employee to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalasserAddnValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To GvAsserDetails.Rows.Count - 1
                chkSelect = GvAsserDetails.Rows(i).FindControl("chkSelect")
                lblDescID = GvAsserDetails.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objAsst.AssMasDeactivate(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblDescID.Text), "A", sSession.IPAddress, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                End If
            Next
            lblAdditionValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalasserAddnValidation').modal('show');", True)
            ddlFinancialYear_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub
End Class
