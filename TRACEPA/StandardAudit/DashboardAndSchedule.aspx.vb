Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Partial Class DashboardAndSchedule
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_DashboardAndSchedule"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditChecklist As New clsAuditChecklist
    Private objclsStandardAudit As New clsStandardAudit

    Private sSession As AllSession
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iAuditID As Integer
    Private Shared iCustID As Integer
    Private Shared iAuditStatusID As Integer
    Private Shared iHeadingID As Integer
    Private Shared sCheckponitids As String
    Private Shared iCmntpkID As Integer

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAddSchedule.ImageUrl = "~/Images/Add24.png"
        imgbtnAddCust.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                LoadFinalcialYear(sSession.AccessCode) : BindCustomers()

                If Request.QueryString("CustID") IsNot Nothing Then
                    iCustID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CustID")))
                    ddlCustomerName.SelectedValue = iCustID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        ddlCustomerName_SelectedIndexChanged(sender, e)
                    End If
                End If
                If ddlCustomerName.SelectedIndex = 0 Then
                    BindAllStandardAuditScheduled(0)
                End If
                RFVCustomerName.InitialValue = "Select Customer" : RFVCustomerName.ErrorMessage = "Select Customer."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsStandardAudit.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.YearID
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCustomers()
        Try
            ddlCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "CUST_Name"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim iCustomerID As Integer
        Try
            lblError.Text = ""
            iAuditID = 0
            gvDashboard.DataSource = Nothing
            gvDashboard.DataBind()
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlCustomerName.SelectedValue
                sSession.CustomerID = ddlCustomerName.SelectedValue
                Session("AllSession") = sSession
            End If
            BindAllStandardAuditScheduled(iCustomerID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            ddlCustomerName_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAddCust_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCust.Click
        Try
            Response.Redirect(String.Format("~/Masters/CustomerDetails.aspx"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddCust_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAddSchedule_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddSchedule.Click
        Dim oAuditID As Object, oCustID As Object, oFinancialID As Object
        Try
            lblError.Text = ""
            oFinancialID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(ddlFinancialYear.SelectedValue)))
            oCustID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(ddlCustomerName.SelectedValue)))
            oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            Response.Redirect(String.Format("~/StandardAudit/DashboardAndScheduleDeatils.aspx?FinancialID={0}&CustId={1}&AuditId={2}", oFinancialID, oCustID, oAuditID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddSchedule_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllStandardAuditScheduled(ByVal iCustomerID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadDashboardStandardAudit(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustomerID, sSession.UserID, bLoginUserIsPartner)
            gvDashboard.DataSource = dt
            gvDashboard.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllStandardAuditScheduled" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDashboard_PreRender(sender As Object, e As EventArgs) Handles gvDashboard.PreRender
        Try
            If gvDashboard.Rows.Count > 0 Then
                gvDashboard.UseAccessibleHeader = True
                gvDashboard.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDashboard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDashboard_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvDashboard_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDashboard.RowDataBound
        Dim chkSelectAsgSubTask As New CheckBox
        Dim lblCustomerShortName As New Label, lblCustomerFullName As New Label, lblAuditID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblCustomerShortName = CType(e.Row.FindControl("lblCustomerShortName"), Label)
                lblCustomerFullName = CType(e.Row.FindControl("lblCustomerFullName"), Label)
                lblCustomerShortName.ToolTip = lblCustomerFullName.Text
                Dim imgbtnAuditId As ImageButton = CType(e.Row.FindControl("imgbtnAuditId"), ImageButton)
                Dim imgbtnUpdate As ImageButton = CType(e.Row.FindControl("imgbtnUpdate"), ImageButton)
                Dim imgbtnTimebooking As ImageButton = CType(e.Row.FindControl("imgbtnTimebooking"), ImageButton)
                imgbtnAuditId.ImageUrl = "~/Images/chk.jpg"
                imgbtnUpdate.ImageUrl = "~/Images/Edit16.png"
                imgbtnTimebooking.ImageUrl = "~/Images/icons8-time-limit-16.png"
                lblAuditID = CType(e.Row.FindControl("lblAuditID"), Label)
                If sSession.AuditCodeID > 0 And sSession.AuditCodeID = Val(lblAuditID.Text) Then
                    imgbtnAuditId.ToolTip = "Selected"
                    imgbtnAuditId.ImageUrl = "~/Images/chkSelect.jpg"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDashboard_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDashboard_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDashboard.RowCommand
        Dim lblAuditID As New Label, lblStatusID As New Label, lblCustID As New Label, lblAuditNo As New Label
        Dim oAuditID As Object, oCustID As Object, oFinancialID As Object
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If e.CommandName = "Conduct" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
                lblStatusID = CType(clickedRow.FindControl("lblStatusID"), Label)
                lblCustID = CType(clickedRow.FindControl("lblCustID"), Label)
                iCustID = Val(lblCustID.Text)
                oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAuditID.Text)))
                oCustID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCustID))
                Response.Redirect(String.Format("~/StandardAudit/ConductAudit.aspx?CustID={0}&AuditID={1}", oCustID, oAuditID), False)
            End If
            If e.CommandName = "SheduleUpdate" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
                lblStatusID = CType(clickedRow.FindControl("lblStatusID"), Label)
                lblCustID = CType(clickedRow.FindControl("lblCustID"), Label)
                iCustID = Val(lblCustID.Text)
                iAuditID = 0 : iAuditID = Val(lblAuditID.Text)
                oFinancialID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(ddlFinancialYear.SelectedValue)))
                oCustID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(lblCustID.Text))
                oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iAuditID))
                Response.Redirect(String.Format("~/StandardAudit/DashboardAndScheduleDeatils.aspx?FinancialID={0}&CustID={1}&AuditID={2}", oFinancialID, oCustID, oAuditID), False)
            End If
            If e.CommandName = "Timeline" Then
                iAuditID = 0 : iAuditStatusID = 0
                divtimelineEdit1.Visible = False
                divhistory.Visible = False
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
                lblStatusID = CType(clickedRow.FindControl("lblStatusID"), Label)
                lblCustID = CType(clickedRow.FindControl("lblCustID"), Label)
                lblAuditNo = CType(clickedRow.FindControl("lblAuditNo"), Label)
                iCustID = Val(lblCustID.Text)
                iAuditID = Val(lblAuditID.Text)
                txtcustname.Text = objclsStandardAudit.getcustomername(sSession.AccessCode, sSession.AccessCodeID, iCustID)
                lblUser.Text = sSession.UserFullName & "-" & sSession.UserLoginName
                txtAuditNo.Text = lblAuditNo.Text
                Dim dttimeline As New DataTable
                dttimeline = objclsStandardAudit.getTimelinedetails(sSession.AccessCode, sSession.AccessCodeID, Val(lblCustID.Text), Val(lblAuditID.Text))
                If dttimeline.Rows.Count > 0 Then
                    gvTimeleine.DataSource = dttimeline
                    gvTimeleine.DataBind()
                Else
                    gvTimeleine.DataSource = Nothing
                    gvTimeleine.DataBind()
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalTimebooking').modal('show')", True)
            End If
            If e.CommandName = "SelectAudit" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                Dim imgbtnAuditId As New ImageButton
                For Each dItem In gvDashboard.Rows
                    imgbtnAuditId = CType(dItem.FindControl("imgbtnAuditId"), ImageButton)
                    imgbtnAuditId.ImageUrl = "~/Images/chk.jpg"
                    imgbtnAuditId.ToolTip = "Select"
                Next
                imgbtnAuditId = CType(clickedRow.FindControl("imgbtnAuditId"), ImageButton)
                lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
                lblCustID = CType(clickedRow.FindControl("lblCustID"), Label)
                iCustID = Val(lblCustID.Text)
                imgbtnAuditId.ImageUrl = "~/Images/chkSelect.jpg"
                imgbtnAuditId.ToolTip = "Selected"
                sSession.AuditCodeID = Val(lblAuditID.Text)
                Session("AllSession") = sSession
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDashboard_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvTimeleine_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTimeleine.RowDataBound
        Dim imgbtnedit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnedit = CType(e.Row.FindControl("imgbtntimelineEdit"), ImageButton)
                imgbtnedit.ImageUrl = "~/Images/Edit16.png"
                imgbtnedit.ToolTip = "Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalTimebooking').modal('show')", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTimeleine_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvTimeleine_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTimeleine.RowCommand
        Dim lblAuditID As New Label, lblHeadingID As New Label, lblCustID As New Label, lblAuditNo As New Label,
            lblCheckpointids As New Label, lblHeadingnamegv As New Label
        Dim dtdetails As New DataTable
        Try
            lblError.Text = ""
            If e.CommandName = "EditTime" Then
                iCmntpkID = 0
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                Dim imgbtnedit As New ImageButton
                For Each dItem In gvTimeleine.Rows
                    imgbtnedit = CType(dItem.FindControl("imgbtntimelineEdit"), ImageButton)
                    imgbtnedit.ToolTip = "Edit"
                Next
                lblHeadingnamegv = CType(clickedRow.FindControl("lblHeading"), Label)
                lblHeadingID = CType(clickedRow.FindControl("lblheadingid"), Label)
                lblCheckpointids = CType(clickedRow.FindControl("lblCheckpointids"), Label)
                iHeadingID = Val(lblHeadingID.Text)
                sCheckponitids = lblCheckpointids.Text
                lblHeadingname.Text = lblHeadingnamegv.Text
                dtdetails = objclsStandardAudit.getTimelineotherdetails(sSession.AccessCode, sSession.AccessCodeID, iCustID, iAuditID, Val(lblHeadingID.Text), lblCheckpointids.Text)
                divtimelineEdit1.Visible = True
                divhistory.Visible = True
                If dtdetails.Rows.Count > 0 Then
                    grdcommentsHistory.DataSource = dtdetails
                    grdcommentsHistory.DataBind()
                Else
                    txtstartdate.Text = ""
                    txtEnddate.Text = ""
                    txttotalHours.Text = ""
                    txtComments.Text = ""
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalTimebooking').modal('show')", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTimeleine_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub btnTimelineupdate_Click(sender As Object, e As EventArgs) Handles btnTimelineupdate.Click
        Dim oStatusID As New Object, oAuditTypeID As New Object
        Dim sYearName As String
        Try
            Dim objSAT As New strStandardAudit_Schedule

            lblError.Text = ""

            If iCmntpkID = 0 Then
                objSAT.iAT_ID = 0
            Else
                objSAT.iAT_ID = iCmntpkID
            End If
            objSAT.iAT_CustId = iCustID
            objSAT.iAT_AuditId = iAuditID
            objSAT.iAT_Heading = iHeadingID
            objSAT.sAT_CheckpointId = sCheckponitids
            objSAT.iAT_EmpId = 0
            objSAT.iAT_WorkType = 0
            objSAT.iAT_HrPrDay = 0
            objSAT.sAT_Comments = txtComments.Text
            objSAT.dAT_StartDate = txtstartdate.Text
            objSAT.dAT_EndDate = txtEnddate.Text
            objSAT.iAT_TotalHr = Val(txttotalHours.Text)
            objSAT.sAT_Status = "A"
            objSAT.iAT_CRBY = sSession.UserID
            objSAT.iAT_UPDATEDBY = sSession.UserID
            objSAT.sAT_IPAddress = sSession.IPAddress
            objSAT.iAT_CompId = sSession.AccessCodeID
            sYearName = ddlFinancialYear.SelectedItem.Text
            objclsStandardAudit.SaveTimelineSchedule(sSession.AccessCode, objSAT, sYearName)
            lblError.Text = "Successfully Saved." : lblDashboardValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDashboardValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btntimelineupdate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Private Sub gvTimeleine_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTimeleine.RowDataBound
    '    Dim lblAuditID As New Label, lblHeadingID As New Label, lblCustID As New Label, lblAuditNo As New Label, lblCheckpointids As New Label
    '    Dim imgbtnedit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            lblHeadingID = CType(e.Row.FindControl("lblHeadingID"), Label)
    '            lblCheckpointids = CType(e.Row.FindControl("lblCheckpointids"), Label)
    '            imgbtnedit = CType(e.Row.FindControl("imgbtntimelineEdit"), ImageButton)
    '            imgbtnedit.ImageUrl = "~/Images/Edit16.png"
    '            imgbtnedit.ToolTip = "Edit"
    '            lblAuditID = CType(e.Row.FindControl("lblAuditID"), Label)
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalTimebooking').modal('show')", True)
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTimeleine_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub

    'Private Sub gvTimeleine_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTimeleine.RowCommand
    '    Dim lblAuditID As New Label, lblHeadingID As New Label, lblCustID As New Label, lblAuditNo As New Label, lblCheckpointids As New Label, lblHeadingnamegv As New Label
    '    Dim dtdetails As New DataTable
    '    Try
    '        lblError.Text = ""
    '        If e.CommandName = "EditTime" Then
    '            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
    '            Dim imgbtnedit As New ImageButton
    '            For Each dItem In gvTimeleine.Rows
    '                imgbtnedit = CType(dItem.FindControl("imgbtntimelineEdit"), ImageButton)
    '                imgbtnedit.ToolTip = "Edit"
    '            Next
    '            lblHeadingnamegv = CType(clickedRow.FindControl("lblHeading"), Label)
    '            lblHeadingID = CType(clickedRow.FindControl("lblheadingid"), Label)
    '            lblCheckpointids = CType(clickedRow.FindControl("lblCheckpointids"), Label)
    '            iHeadingID = Val(lblHeadingID.Text)
    '            sCheckponitids = lblCheckpointids.Text
    '            lblHeadingname.Text = lblHeadingnamegv.Text
    '            dtdetails = objclsStandardAudit.getTimelineotherdetails(sSession.AccessCode, sSession.AccessCodeID, iCustID, iAuditID, Val(lblHeadingID.Text), lblCheckpointids.Text)
    '            divtimelineEdit1.Visible = True
    '            divhistory.Visible = True
    '            If dtdetails.Rows.Count > 0 Then
    '                txtstartdate.Text = dtdetails.Rows(0)("Startdate")
    '                txtEnddate.Text = dtdetails.Rows(0)("EndDate")
    '                txttotalHours.Text = dtdetails.Rows(0)("totalHrs")
    '                txtComments.Text = dtdetails.Rows(0)("Comments")
    '                grdcommentsHistory.DataSource = dtdetails
    '                grdcommentsHistory.DataBind()
    '            Else
    '                txtstartdate.Text = ""
    '                txtEnddate.Text = ""
    '                txttotalHours.Text = ""
    '                txtComments.Text = ""
    '            End If
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalTimebooking').modal('show')", True)
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTimeleine_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            divtimelineEdit1.Visible = True
            divhistory.Visible = True
            Dim dttimeline As New DataTable
            iCmntpkID = 0
            dttimeline = objclsStandardAudit.getTimelineotherdetails(sSession.AccessCode, sSession.AccessCodeID, iCustID, iAuditID, iHeadingID, sCheckponitids)
            If dttimeline.Rows.Count > 0 Then
                grdcommentsHistory.DataSource = dttimeline
                grdcommentsHistory.DataBind()
            Else
                grdcommentsHistory.DataSource = Nothing
                grdcommentsHistory.DataBind()
            End If
            txtstartdate.Text = ""
            txtEnddate.Text = ""
            txttotalHours.Text = ""
            txtComments.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalTimebooking').modal('show')", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub grdcommentsHistory_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdcommentsHistory.RowCommand
        Dim lblid As New Label
        Dim dtdetails As New DataTable
        Try
            lblError.Text = ""
            If e.CommandName = "EditTime" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                Dim imgbtnedit As New ImageButton
                lblid = CType(clickedRow.FindControl("lblCommntsid"), Label)
                iCmntpkID = Val(lblid.Text)
                dtdetails = objclsStandardAudit.getTimelineCommentsrdetails(sSession.AccessCode, sSession.AccessCodeID, iCmntpkID)
                divtimelineEdit1.Visible = True
                divhistory.Visible = True
                If dtdetails.Rows.Count > 0 Then
                    txtstartdate.Text = dtdetails.Rows(0)("Startdate")
                    txtEnddate.Text = dtdetails.Rows(0)("EndDate")
                    txttotalHours.Text = dtdetails.Rows(0)("totalHrs")
                    txtComments.Text = dtdetails.Rows(0)("Comments")
                Else
                    txtstartdate.Text = ""
                    txtEnddate.Text = ""
                    txttotalHours.Text = ""
                    txtComments.Text = ""
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalTimebooking').modal('show')", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdcommentsHistory_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub grdcommentsHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdcommentsHistory.RowDataBound
        Dim imgbtnedit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnedit = CType(e.Row.FindControl("imgbtnCommentsEdit"), ImageButton)
                imgbtnedit.ImageUrl = "~/Images/Edit16.png"
                imgbtnedit.ToolTip = "Edit"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdcommentsHistory_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class