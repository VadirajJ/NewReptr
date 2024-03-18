Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class LeaveDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_LeaveDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsGRACePermission As New clsGRACePermission
    Private sSession As AllSession
    Private objclsLeaveDetails As New clsLeaveDetails

    Private Shared iID As Integer = 0
    Private Shared sLDAD As String
    Private Shared sLDSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : divPermLeave.Visible = False : imgbtnReport.Visible = False
                RFVRemarks.ValidationGroup = False : REVRemarks.ValidationGroup = False
                sLDSave = "NO" : sLDAD = "NO"
                sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPLD", 1)
                If sFormButtons = "False" Or sFormButtons = "" Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",Approve/Activate/De-Activate,") = True Then
                        sLDAD = "YES"
                        divPermLeave.Visible = True
                        RFVRemarks.ValidationGroup = "Validate" : REVRemarks.ValidationGroup = "Validate"
                        RFVRemarks.ControlToValidate = "txtRemarks" : RFVRemarks.ErrorMessage = "Enter Remarks."
                        REVRemarks.ErrorMessage = "Remarks exceeded maximum size(max 1000 characters)." : REVRemarks.ValidationExpression = "^[\s\S]{0,1000}$"
                        REVRemarks.ControlToValidate = "txtRemarks"
                    End If
                    If sFormButtons.Contains(",Save/Update,") = True Then
                        sLDSave = "YES"
                        imgbtnSave.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
                BindExistingEmployeeDB()
                ddlEmployee.SelectedValue = sSession.UserID
                ddlEmployee_SelectedIndexChanged(sender, e)
                RFVEmployee.InitialValue = "Select Existing Employee" : RFVEmployee.ErrorMessage = "Select Existing Employee."
                RFVFrom.ControlToValidate = "txtFrom" : RFVFrom.ErrorMessage = "Enter From Date."
                REVFrom.ErrorMessage = "Enter valid From Date." : REVFrom.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVTo.ControlToValidate = "txtTo" : RFVTo.ErrorMessage = "Enter To Date."
                REVTo.ErrorMessage = "Enter valid To Date." : REVTo.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVPurpose.ControlToValidate = "txtPurpose" : RFVPurpose.ErrorMessage = "Enter Purpose."
                REVPurpose.ErrorMessage = "Purpose exceeded maximum size(max 1000 characters)." : REVPurpose.ValidationExpression = "^[\s\S]{0,1000}$"
                RFVDays.ControlToValidate = "txtNoDays" : RFVDays.ErrorMessage = "Enter No Of Days."
                REVDays.ErrorMessage = "Only Integer." : REVDays.ValidationExpression = "^[0-9]{0,10000}$"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindExistingEmployeeDB()
        Try
            ddlEmployee.DataSource = objclsAllActiveMaster.LoadActiveEmployeesUsers(sSession.AccessCode, sSession.AccessCodeID)
            ddlEmployee.DataTextField = "FullName"
            ddlEmployee.DataValueField = "Usr_ID"
            ddlEmployee.DataBind()
            ddlEmployee.Items.Insert(0, "Select Existing Employee")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingEmployeeDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub ddlEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmployee.SelectedIndexChanged
        Try
            lblError.Text = ""
            ClearALL()
            lblSapCode.Text = ""
            gvLeaveDetails.DataSource = Nothing
            gvLeaveDetails.DataBind()
            If ddlEmployee.SelectedIndex > 0 Then
                lblSapCode.Text = objclsLeaveDetails.LoadActiveUserCode(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue)
                gvLeaveDetails.DataSource = objclsLeaveDetails.LoadGridLeaveDetails(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue, sSession.YearID)
                gvLeaveDetails.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlEmployee_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim FrDate As Date, ToDate As Date, dDate As Date, dSDate As Date
        Dim objLeaveDetails As New strLeaveDetails
        Dim Arr() As String
        Dim Status As String = ""
        Try
            lblError.Text = ""
            If ddlEmployee.SelectedIndex > 0 Then
                dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim l As Integer
                l = DateDiff(DateInterval.Day, dDate, dSDate)
                If l < 0 Then
                    lblLeaveValidationMsg.Text = "From Date (" & txtFrom.Text & ") should be greater than or equal to Current Date."
                    lblError.Text = "From Date (" & txtFrom.Text & ") should be greater than or equal to Current Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalLeaveValidation').modal('show'); $('#txtDescription').focus();", True)
                    txtFrom.Focus()
                    Exit Sub
                End If
                FrDate = DateTime.ParseExact(Trim(txtFrom.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                ToDate = DateTime.ParseExact(Trim(txtTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                If FrDate > ToDate Then
                    txtTo.Focus()
                    lblLeaveValidationMsg.Text = "To Date (" & txtTo.Text & ") should be greater than or equal From Date of Transfer (" & txtFrom.Text & ")."
                    lblError.Text = "To Date (" & txtTo.Text & ") should be greater than or equal From Date of Transfer (" & txtFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalLeaveValidation').modal('show');", True)
                    Exit Sub
                End If
                objLeaveDetails.iLPE_ID = 0
                objLeaveDetails.iLPE_EMPID = ddlEmployee.SelectedValue
                objLeaveDetails.iLPE_YearID = sSession.YearID
                objLeaveDetails.dLPE_FROMDATE = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLeaveDetails.dLPE_TODATE = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLeaveDetails.iLPE_DAYS = txtNoDays.Text
                objLeaveDetails.sLPE_PURPOSE = objclsGRACeGeneral.SafeSQL(txtPurpose.Text)
                objLeaveDetails.iLPE_CrBY = sSession.UserID
                objLeaveDetails.iLPE_UpdatedBY = sSession.UserID
                objLeaveDetails.sLPE_IPAddress = sSession.IPAddress
                objLeaveDetails.iLPE_CompID = sSession.AccessCodeID
                Arr = objclsLeaveDetails.SaveLeaveDetails(sSession.AccessCode, objLeaveDetails)
                iID = Arr(1)
                If rboApproved.Checked = True Then
                    Status = "A"
                Else
                    Status = "N"
                End If
                If sLDAD = "YES" Then
                    objclsLeaveDetails.UpdateLeaveDetails(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue, Arr(1), sSession.YearID, Status, txtRemarks.Text)
                End If
                ddlEmployee_SelectedIndexChanged(sender, e)
                LooadAllLeaveDetails(ddlEmployee.SelectedValue, Arr(1), sSession.YearID)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Leave Particulars", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                lblError.Text = "Successfully Saved." : lblLeaveValidationMsg.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLeaveValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim FrDate As Date, ToDate As Date, dDate As Date, dSDate As Date
        Dim objLeaveDetails As New strLeaveDetails
        Dim Arr() As String
        Dim Status As String = ""
        Try
            lblError.Text = ""
            If ddlEmployee.SelectedIndex > 0 Then
                dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim l As Integer
                l = DateDiff(DateInterval.Day, dDate, dSDate)
                If l < 0 Then
                    lblLeaveValidationMsg.Text = "From Date (" & txtFrom.Text & ") should be greater than or equal to Current Date."
                    lblError.Text = "From Date (" & txtFrom.Text & ") should be greater than or equal to Current Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalLeaveValidation').modal('show'); $('#txtDescription').focus();", True)
                    txtFrom.Focus()
                    Exit Sub
                End If
                FrDate = DateTime.ParseExact(Trim(txtFrom.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                ToDate = DateTime.ParseExact(Trim(txtTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                If FrDate > ToDate Then
                    txtTo.Focus()
                    lblLeaveValidationMsg.Text = "To Date (" & txtTo.Text & ") should be greater than or equal From Date of Transfer (" & txtFrom.Text & ")."
                    lblError.Text = "To Date (" & txtTo.Text & ") should be greater than or equal From Date of Transfer (" & txtFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalLeaveValidation').modal('show');", True)
                    Exit Sub
                End If
                objLeaveDetails.iLPE_ID = iID
                objLeaveDetails.iLPE_EMPID = ddlEmployee.SelectedValue
                objLeaveDetails.iLPE_YearID = sSession.YearID
                objLeaveDetails.dLPE_FROMDATE = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLeaveDetails.dLPE_TODATE = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objLeaveDetails.iLPE_DAYS = txtNoDays.Text
                objLeaveDetails.sLPE_PURPOSE = objclsGRACeGeneral.SafeSQL(txtPurpose.Text)
                objLeaveDetails.iLPE_CrBY = sSession.UserID
                objLeaveDetails.iLPE_UpdatedBY = sSession.UserID
                objLeaveDetails.sLPE_IPAddress = sSession.IPAddress
                objLeaveDetails.iLPE_CompID = sSession.AccessCodeID
                Arr = objclsLeaveDetails.SaveLeaveDetails(sSession.AccessCode, objLeaveDetails)
                If rboApproved.Checked = True Then
                    Status = "A"
                Else
                    Status = "N"
                End If
                If sLDAD = "YES" Then
                    objclsLeaveDetails.UpdateLeaveDetails(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue, Arr(1), sSession.YearID, Status, txtRemarks.Text)
                End If
                ddlEmployee_SelectedIndexChanged(sender, e)
                LooadAllLeaveDetails(ddlEmployee.SelectedValue, Arr(1), sSession.YearID)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Leave Particulars", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                lblError.Text = "Successfully Updated." : lblLeaveValidationMsg.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLeaveValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvLeaveDetails_PreRender(sender As Object, e As EventArgs) Handles gvLeaveDetails.PreRender
        Dim dt As New DataTable
        Try
            If gvLeaveDetails.Rows.Count > 0 Then
                gvLeaveDetails.UseAccessibleHeader = True
                gvLeaveDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                gvLeaveDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLeaveDetails_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvLeaveDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvLeaveDetails.RowCommand
        Dim lblID As New Label
        Try
            lblError.Text = "" : iID = 0 : ClearALL()
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                iID = lblID.Text
                If ddlEmployee.SelectedIndex > 0 Then
                    LooadAllLeaveDetails(ddlEmployee.SelectedValue, lblID.Text, sSession.YearID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLeaveDetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub LooadAllLeaveDetails(ByVal iEMpID As Integer, ByVal iID As Integer, ByVal iYearID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsLeaveDetails.LoadLeaveDetails(sSession.AccessCode, sSession.AccessCodeID, iEMpID, iID, iYearID)
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            If dt.Rows.Count > 0 Then
                If sLDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                If IsDBNull(dt.Rows(0).Item("LPE_FROMDATE")) = False Then
                    txtFrom.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("LPE_FROMDATE"), "D")
                End If
                If IsDBNull(dt.Rows(0).Item("LPE_TODATE")) = False Then
                    txtTo.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("LPE_TODATE"), "D")
                End If
                If IsDBNull(dt.Rows(0).Item("LPE_DAYS")) = False Then
                    txtNoDays.Text = dt.Rows(0).Item("LPE_DAYS")
                End If
                If IsDBNull(dt.Rows(0).Item("LPE_PURPOSE")) = False Then
                    txtPurpose.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("LPE_PURPOSE").ToString())
                End If
                rboNotApproved.Checked = False
                If IsDBNull(dt.Rows(0).Item("LPE_Approve")) = False Then
                    If dt.Rows(0).Item("LPE_Approve") = "A" Then
                        rboApproved.Checked = True
                    Else
                        rboApproved.Checked = False
                        rboNotApproved.Checked = True
                    End If
                Else
                    rboApproved.Checked = True
                End If
                If IsDBNull(dt.Rows(0).Item("LPE_ApprovedDetails")) = False Then
                    txtRemarks.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("LPE_ApprovedDetails").ToString())
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LooadAllLeaveDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            ClearALL()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub ClearALL()
        Try
            txtFrom.Text = "" : txtTo.Text = "" : txtNoDays.Text = ""
            txtPurpose.Text = "" : txtRemarks.Text = "" : rboNotApproved.Checked = False : rboApproved.Checked = True
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            If sLDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearALL" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objclsLeaveDetails.LoadGridLeaveDetails(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblLeaveValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalLeaveValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/LeaveDetails.rdlc")
            Dim FinancialYear As ReportParameter() = New ReportParameter() {New ReportParameter("FinancialYear", sSession.YearName)}
            ReportViewer1.LocalReport.SetParameters(FinancialYear)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Leave Particulars", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=LeaveParticulars" + ".pdf")
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
            dt = objclsLeaveDetails.LoadGridLeaveDetails(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblLeaveValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalLeaveValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/LeaveDetails.rdlc")
            Dim FinancialYear As ReportParameter() = New ReportParameter() {New ReportParameter("FinancialYear", sSession.YearName)}
            ReportViewer1.LocalReport.SetParameters(FinancialYear)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Leave Particulars", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=LeaveParticulars" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvLeaveDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLeaveDetails.RowDataBound
        Dim lblStatus As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblStatus = CType(e.Row.FindControl("lblStatus"), Label)
                If lblStatus.Text = "Pending" Then
                    lblStatus.BackColor = Drawing.Color.Orange
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLeaveDetails_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
End Class
