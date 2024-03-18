
Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class JournalEntry
    Inherits System.Web.UI.Page
    Private sFormName As String = "JournalEntry"
    Dim objGen As New clsGRACeGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objJE As New clsJournalEntry
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private Shared dtJEDetails As DataTable
    Private Shared sCustomerAudit As String = ""
    Dim dt As New DataTable
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objUT As New ClsUploadTailBal
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"

        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtUserDetails As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                BindStatus()
                LoadExistingCustomer()
                If sSession.CustomerID <> 0 Then
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                        ddlCustName.SelectedValue = sSession.CustomerID
                        ddlCustName_SelectedIndexChanged(sender, e)
                    Else
                        ddlCustName.SelectedValue = sSession.CustomerID
                        If ddlCustName.SelectedIndex > 0 Then
                            ddlCustName_SelectedIndexChanged(sender, e)
                        End If
                    End If
                Else
                    BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
                End If

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                imgbtnAdd.Visible = True
                imgbtnReport.Visible = True
                'dtUserDetails = objJE.LoadCustomerEmpList(sSession.AccessCode, sSession.AccessCodeID)
                'If dtUserDetails.Rows.Count > 0 Then
                '    Dim DVUserDetails As New DataView(dtUserDetails)
                '    DVUserDetails.RowFilter = "usr_Id='" & sSession.UserID & "'"
                '    DVUserDetails.Sort = "usr_Id ASC"
                '    dtUserDetails = DVUserDetails.ToTable
                '    If dtUserDetails.Rows.Count = 0 Then
                '        imgbtnActivate.Visible = False
                '    Else
                If (objJE.CusormeCheck(sSession.AccessCode, sSession.AccessCodeID)) Then
                    sCustomerAudit = "Customer"
                    'End If
                End If

                dt = objJE.GetAuditEmpList(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    Dim dtview As New DataView(dt)
                    dtview.RowFilter = "usr_Id='" & sSession.UserID & "'"
                    dtview.Sort = "usr_Id ASC"
                    dt = dtview.ToTable
                    If dt.Rows.Count = 0 Then
                        imgbtnActivate.Visible = False
                    Else
                        sCustomerAudit = "Auditor"
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadExistingCustomer()
        Try
            ddlCustName.DataSource = objJE.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustName.DataTextField = "Cust_Name"
            ddlCustName.DataValueField = "Cust_Id"
            ddlCustName.DataBind()
            ddlCustName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingCustomer")
            'Throw
        End Try
    End Sub
    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objUT.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YEARID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Waiting for Approval")
            ddlStatus.Items.Insert(3, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus")
            'Throw
        End Try
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim sMainMaster As String = ""
        Try
            sMainMaster = ""
            imgbtnAdd.Visible = True
            imgbtnReport.Visible = True
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnDeActivate.Visible = True : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnActivate.Visible = True : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnWaiting.Visible = True : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If

            BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub

    Public Sub BindJEDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal iYearId As Integer)
        Dim iBranchId As Integer = 0
        Try
            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnDeActivate.Visible = True : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnActivate.Visible = True : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnWaiting.Visible = True : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            End If
            If ddlCustName.SelectedIndex > 0 Then
                If ddlBranch.SelectedIndex < 1 Then
                    iBranchId = 0
                Else
                    iBranchId = ddlBranch.SelectedValue
                End If
                dt = objJE.LoadJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, ddlCustName.SelectedValue, iYearId, iBranchId)
                dgJE.DataSource = dt
                dgJE.DataBind()
            Else
                dt = objJE.LoadJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, 0, iYearId, 0)
                dgJE.DataSource = dt
                dgJE.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindJEDetails")
            'Throw
        End Try
    End Sub



    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgJE.Rows.Count - 1
                    chkField = dgJE.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgJE.Rows.Count - 1
                    chkField = dgJE.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub

    Private Sub dgJE_PreRender(sender As Object, e As EventArgs) Handles dgJE.PreRender
        Dim dt As New DataTable
        Try
            If dgJE.Rows.Count > 0 Then
                dgJE.UseAccessibleHeader = True
                dgJE.HeaderRow.TableSection = TableRowSection.TableHeader
                dgJE.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJE_PreRender")
        End Try
    End Sub

    Private Sub dgJE_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgJE.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgJE.Columns(0).Visible = True

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    imgbtnStatus.Visible = False : imgbtnEdit.Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.Visible = False : imgbtnEdit.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJE_RowDataBound")
        End Try
    End Sub
    Private Sub dgJE_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgJE.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object, oPartyID As Object, oBranchID As Object
        Dim lblDescID As New Label, lblDescName As New Label, lblpartyID As New Label, lblbranchID As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)
            lblpartyID = DirectCast(clickedRow.FindControl("lblpartyID"), Label)
            lblbranchID = DirectCast(clickedRow.FindControl("lblbranchID"), Label)
            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                'If ddlStatus.SelectedIndex = 0 Then
                '    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                'ElseIf ddlStatus.SelectedIndex = 1 Then
                '    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                'ElseIf ddlStatus.SelectedIndex = 2 Then
                '    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                'Else
                '    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                'End If
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(ddlFinancialYear.SelectedValue))
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                oPartyID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblpartyID.Text)))
                oBranchID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblBranchID.Text)))
                Response.Redirect(String.Format("~/FIN Statement/JournalEntryDetails.aspx?StatusID={0}&MasterID={1}&MasterName={2}&PartyID={3}&BranchID={4}", oStatusID, oMasterID, oMasterName, oPartyID, oBranchID), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    objJE.UpdateAccTransactionDetailsStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    objJE.UpdateAccTransactionDetailsStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    objJE.UpdateAccTransactionDetailsStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
                End If
                ddlStatus.SelectedIndex = 0
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJE_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgJE.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                lblDescID = dgJE.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgJE.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If
            If ddlBranch.SelectedIndex <= 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                lblDescID = dgJE.Rows(i).FindControl("lblDescID")

                'If sCustomerAudit = "Customer" Then
                '    status = "W" 'status = "WC"
                'Else
                '    status = "W" 'status = "WA"
                'End If

                If chkSelect.Checked = True Then
                    'objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, status, sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    'objJE.UpdateAccTransactionDetailsStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, status, sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    'sStatus = objJE.GetJEStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue)
                    'objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, sStatus, sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    lblStatus.Text = "Approve"
                    DeactivateJE(lblDescID)

                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            If ddlCustName.SelectedIndex > 0 Then
                ddlCustName_SelectedIndexChanged(sender, e)
            Else
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim status As String
        Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If dgJE.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If
            If ddlBranch.SelectedIndex <= 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgJE.Rows.Count - 1
                chkSelect = dgJE.Rows(i).FindControl("chkSelect")
                lblDescID = dgJE.Rows(i).FindControl("lblDescID")

                'If sCustomerAudit = "Customer" Then
                '    status = "W" 'status = "WC"
                'Else
                '    status = "W" 'status = "WA"
                'End If

                If chkSelect.Checked = True Then
                    'objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, status, sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    'objJE.UpdateAccTransactionDetailsStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, status, sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    'sStatus = objJE.GetJEStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue)
                    'objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, sStatus, sSession.UserID, sSession.IPAddress, sCustomerAudit)
                    lblStatus.Text = "Approve"
                    ApproveJE(lblDescID)

                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            If ddlCustName.SelectedIndex > 0 Then
                ddlCustName_SelectedIndexChanged(sender, e)
            Else
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Function ApproveJE(ByVal lblDescID As Label)
        Dim iPaymentType As Integer = 0, iJEID As Integer = 0, IBranchid As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim dt As New DataTable
        Dim ArrJE() As String, ArrTD() As String
        Dim iTranStat As Integer = 0, dTransAMt As Double = 0.0
        Dim dtJEdetails As New DataTable
        Dim dtJEMasters As New DataTable

        Try
            lblError.Text = ""
            If ddlBranch.SelectedIndex = 0 Then
                IBranchid = 0
            Else
                IBranchid = ddlBranch.SelectedValue
            End If
            dtJEMasters = objJE.LoadMasterJournalEntryApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, IBranchid, lblDescID.Text)
            If dtJEMasters.Rows.Count > 0 Then
                For i = 0 To dtJEMasters.Rows.Count - 1
                    objJE.iAcc_JE_ID = dtJEMasters.Rows(i)("id")
                    objJE.sAJTB_TranscNo = dtJEMasters.Rows(i)("TransactionNo")
                    objJE.iAcc_JE_Location = 0
                    objJE.iAcc_JE_Party = ddlCustName.SelectedValue
                    'objJE.iAcc_JE_BillType = dtJEMasters.Rows(i)("BillType")
                    'objJE.sAcc_JE_BillNo = dtJEMasters.Rows(i)("BillNo")
                    'objJE.dAcc_JE_BillDate = dtJEMasters.Rows(i)("BillDate")


                    If (IsDBNull(dtJEMasters.Rows(i)("BillType").ToString()) = False) And (dtJEMasters.Rows(i)("BillType") <> "&nbsp;") Then
                        objJE.iAcc_JE_BillType = Val(dtJEMasters.Rows(i)("BillType"))
                    Else
                        objJE.iAcc_JE_BillType = 0
                    End If

                    If (IsDBNull(dtJEMasters.Rows(i)("BillNo").ToString()) = False) And (dtJEMasters.Rows(i)("BillNo") <> "&nbsp;") Then
                        objJE.sAcc_JE_BillNo = dtJEMasters.Rows(i)("BillNo")
                    Else
                        objJE.sAcc_JE_BillNo = ""
                    End If
                    If (IsDBNull(dtJEMasters.Rows(i)("BillDate").ToString()) = False) And (dtJEMasters.Rows(i)("BillNo").ToString <> "&nbsp;") Then
                        If dtJEMasters.Rows(i)("BillDate").ToString = "" Then
                            objJE.dAcc_JE_BillDate = "01/01/1900"
                        Else
                            objJE.dAcc_JE_BillDate = Date.ParseExact(dtJEMasters.Rows(i)("BillDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        End If
                    End If


                    'If (IsDBNull(dtJEMasters.Rows(i)("BillDate")) = False) And (dtJEMasters.Rows(i)("BillNo") <> "&nbsp;") Then
                    '    objJE.dAcc_JE_BillDate = dtJEMasters.Rows(i)("BillDate")
                    'Else
                    '    objJE.dAcc_JE_BillDate = "01/01/1900"
                    'End If
                    objJE.dAcc_JE_BillAmount = "0.00"

                    objJE.dAcc_JE_AdvanceAmount = "0.00" : objJE.sAcc_JE_AdvanceNaration = "" : objJE.dAcc_JE_BalanceAmount = "0.00" : objJE.dAcc_JE_NetAmount = "0.00"
                    objJE.sAcc_JE_PaymentNarration = "" : objJE.sAcc_JE_ChequeNo = "" : objJE.dAcc_JE_ChequeDate = "01/01/1900"
                    objJE.sAcc_JE_IFSCCode = "" : objJE.sAcc_JE_BankName = "" : objJE.sAcc_JE_BranchName = ""

                    objJE.iAcc_JE_YearID = ddlFinancialYear.SelectedValue
                    objJE.iAcc_JE_CompID = sSession.AccessCodeID

                    If lblStatus.Text = "Approve" Then
                        objJE.sAcc_JE_Status = "A"
                    End If
                    objJE.iAcc_JE_CreatedBy = sSession.UserID
                    objJE.sAcc_JE_Operation = "C"
                    objJE.sAcc_JE_IPAddress = sSession.IPAddress
                    objJE.dAcc_JE_BillCreatedDate = "01/01/1900"
                    objJE.iacc_JE_BranchId = IBranchid
                    If (IsDBNull(dtJEMasters.Rows(i)("Comments").ToString()) = False) And (dtJEMasters.Rows(i)("Comments") <> "&nbsp;") Then
                        objJE.sAcc_JE_Comments = dtJEMasters.Rows(i)("Comments")
                    Else
                        objJE.sAcc_JE_Comments = ""
                    End If
                    ArrJE = objJE.SaveJournalEntryMaster(sSession.AccessCode, objJE)
                    iJEID = ArrJE(1)
                    dtJEdetails = objJE.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustName.SelectedValue, iJEID, IBranchid)
                    For j = 0 To dtJEdetails.Rows.Count - 1
                        dTransAMt = 0
                        If (IsDBNull(dtJEdetails.Rows(j)("detID").ToString()) = False) Then
                            objJE.iAJTB_ID = Val(dtJEdetails.Rows(j)("detID"))
                        Else
                            objJE.iAJTB_ID = 0
                        End If

                        objJE.iAJTB_CustId = ddlCustName.SelectedValue
                        objJE.iAJTB_MAsID = iJEID
                        'objJE.iATD_OrgType = objCOA.GetOrgTypeID(sSession.AccessCode, sSession.AccessCodeID, "ORG", ddlParty.SelectedValue)
                        'objJE.dATD_TransactionDate = Date.Today
                        'objJE.iATD_TrType = 4
                        'objJE.iATD_BillId = iJEID
                        'objJE.iATD_PaymentType = iPaymentType

                        objJE.iAJTB_Deschead = dtJEdetails.Rows(j)("HeadID")
                        objJE.iAJTB_Desc = dtJEdetails.Rows(j)("HeadID")
                        objJE.sAJTB_DescName = dtJEdetails.Rows(j)("GLDescription")


                        Dim dTransDbAmt, dTransCrAmt As Double
                        If (IsDBNull(dtJEdetails.Rows(j)("Debit").ToString()) = False) And (dtJEdetails.Rows(j)("Debit") <> "0.00") Then
                            objJE.dAJTB_Debit = Convert.ToDouble(dtJEdetails.Rows(j)("Debit"))
                            iTranStat = 0
                            dTransAMt = Convert.ToDouble(dtJEdetails.Rows(j)("Debit").ToString())
                            dTransDbAmt = Convert.ToDouble(dtJEdetails.Rows(j)("Debit").ToString())
                        Else
                            objJE.dAJTB_Debit = 0
                        End If

                        If (IsDBNull(dtJEdetails.Rows(j)("Credit").ToString()) = False) And (dtJEdetails.Rows(j)("Credit") <> "0.00") Then
                            objJE.dAJTB_Credit = Convert.ToDouble(dtJEdetails.Rows(j)("Credit").ToString())
                            iTranStat = 1
                            dTransAMt = Convert.ToDouble(dtJEdetails.Rows(j)("Credit").ToString())
                            dTransCrAmt = Convert.ToDouble(dtJEdetails.Rows(j)("Credit").ToString())
                        Else
                            objJE.dAJTB_Credit = 0
                        End If

                        objJE.iAJTB_CreatedBy = sSession.UserID
                        objJE.iAJTB_UpdatedBy = sSession.UserID
                        If lblStatus.Text = "Approve" Then
                            objJE.sAJTB_Status = "A"
                        Else
                            objJE.sAJTB_Status = "W"
                        End If

                        objJE.iAJTB_YearID = ddlFinancialYear.SelectedValue
                        objJE.iAJTB_CompID = sSession.AccessCodeID
                        'objJE.sATD_Operation = "U"
                        objJE.sAJTB_IPAddress = sSession.IPAddress
                        objJE.iAJTB_BillType = objJE.iAcc_JE_BillType
                        objJE.iAJTB_BranchId = IBranchid
                        ArrTD = objJE.SaveTransactionDetails(sSession.AccessCode, objJE)
                        If lblStatus.Text = "Approve" Then
                            objJE.UpdateJeDet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, objJE.iAJTB_Deschead, ddlCustName.SelectedValue, iTranStat, dTransAMt, IBranchid, dTransDbAmt, dTransCrAmt)
                        End If
                    Next
                Next
            End If

            'objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iJEID, objGen.SafeSQL(txtComments.Text.Trim), "Saved(" & sCustomerAudit & ")", sSession.IPAddress)
            'objJE.DeletePaymentDetails(sSession.AccessCode, sSession.AccessCodeID, iJEID, "BILLID")


        Catch ex As Exception
            Throw
        End Try
    End Function
    Function DeactivateJE(ByVal lblDescID As Label)
        Dim iPaymentType As Integer = 0, iJEID As Integer = 0, IBranchid As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim dt As New DataTable
        Dim ArrJE() As String, ArrTD() As String
        Dim iTranStat As Integer = 0, dTransAMt As Double = 0.0
        Dim dtJEdetails As New DataTable
        Dim dtJEMasters As New DataTable

        Try
            lblError.Text = ""
            If ddlBranch.SelectedIndex = 0 Then
                IBranchid = 0
            Else
                IBranchid = ddlBranch.SelectedValue
            End If
            dtJEMasters = objJE.LoadMasterJournalEntryApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, IBranchid, lblDescID.Text)
            If dtJEMasters.Rows.Count > 0 Then
                For i = 0 To dtJEMasters.Rows.Count - 1
                    objJE.iAcc_JE_ID = dtJEMasters.Rows(i)("id")
                    objJE.sAJTB_TranscNo = dtJEMasters.Rows(i)("TransactionNo")
                    objJE.iAcc_JE_Location = 0
                    objJE.iAcc_JE_Party = ddlCustName.SelectedValue
                    'objJE.iAcc_JE_BillType = dtJEMasters.Rows(i)("BillType")
                    'objJE.sAcc_JE_BillNo = dtJEMasters.Rows(i)("BillNo")
                    'objJE.dAcc_JE_BillDate = dtJEMasters.Rows(i)("BillDate")


                    If (IsDBNull(dtJEMasters.Rows(i)("BillType").ToString()) = False) And (dtJEMasters.Rows(i)("BillType") <> "&nbsp;") Then
                        objJE.iAcc_JE_BillType = Val(dtJEMasters.Rows(i)("BillType"))
                    Else
                        objJE.iAcc_JE_BillType = 0
                    End If

                    If (IsDBNull(dtJEMasters.Rows(i)("BillNo").ToString()) = False) And (dtJEMasters.Rows(i)("BillNo") <> "&nbsp;") Then
                        objJE.sAcc_JE_BillNo = dtJEMasters.Rows(i)("BillNo")
                    Else
                        objJE.sAcc_JE_BillNo = ""
                    End If
                    If (IsDBNull(dtJEMasters.Rows(i)("BillDate").ToString()) = False) And (dtJEMasters.Rows(i)("BillNo").ToString <> "&nbsp;") Then
                        If dtJEMasters.Rows(i)("BillDate").ToString = "" Then
                            objJE.dAcc_JE_BillDate = "01/01/1900"
                        Else
                            objJE.dAcc_JE_BillDate = Date.ParseExact(dtJEMasters.Rows(i)("BillDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        End If
                    End If


                    'If (IsDBNull(dtJEMasters.Rows(i)("BillDate")) = False) And (dtJEMasters.Rows(i)("BillNo") <> "&nbsp;") Then
                    '    objJE.dAcc_JE_BillDate = dtJEMasters.Rows(i)("BillDate")
                    'Else
                    '    objJE.dAcc_JE_BillDate = "01/01/1900"
                    'End If
                    objJE.dAcc_JE_BillAmount = "0.00"

                    objJE.dAcc_JE_AdvanceAmount = "0.00" : objJE.sAcc_JE_AdvanceNaration = "" : objJE.dAcc_JE_BalanceAmount = "0.00" : objJE.dAcc_JE_NetAmount = "0.00"
                    objJE.sAcc_JE_PaymentNarration = "" : objJE.sAcc_JE_ChequeNo = "" : objJE.dAcc_JE_ChequeDate = "01/01/1900"
                    objJE.sAcc_JE_IFSCCode = "" : objJE.sAcc_JE_BankName = "" : objJE.sAcc_JE_BranchName = ""

                    objJE.iAcc_JE_YearID = ddlFinancialYear.SelectedValue
                    objJE.iAcc_JE_CompID = sSession.AccessCodeID

                    If lblStatus.Text = "Approve" Then
                        objJE.sAcc_JE_Status = "D"
                    End If
                    objJE.iAcc_JE_CreatedBy = sSession.UserID
                    objJE.sAcc_JE_Operation = "C"
                    objJE.sAcc_JE_IPAddress = sSession.IPAddress
                    objJE.dAcc_JE_BillCreatedDate = "01/01/1900"
                    objJE.iacc_JE_BranchId = IBranchid
                    If (IsDBNull(dtJEMasters.Rows(i)("Comments").ToString()) = False) And (dtJEMasters.Rows(i)("Comments") <> "&nbsp;") Then
                        objJE.sAcc_JE_Comments = dtJEMasters.Rows(i)("Comments")
                    Else
                        objJE.sAcc_JE_Comments = ""
                    End If
                    ArrJE = objJE.SaveJournalEntryMaster(sSession.AccessCode, objJE)
                    iJEID = ArrJE(1)
                    dtJEdetails = objJE.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustName.SelectedValue, iJEID, IBranchid)
                    For j = 0 To dtJEdetails.Rows.Count - 1
                        dTransAMt = 0
                        If (IsDBNull(dtJEdetails.Rows(j)("detID").ToString()) = False) Then
                            objJE.iAJTB_ID = Val(dtJEdetails.Rows(j)("detID"))
                        Else
                            objJE.iAJTB_ID = 0
                        End If

                        objJE.iAJTB_CustId = ddlCustName.SelectedValue
                        objJE.iAJTB_MAsID = iJEID
                        'objJE.iATD_OrgType = objCOA.GetOrgTypeID(sSession.AccessCode, sSession.AccessCodeID, "ORG", ddlParty.SelectedValue)
                        'objJE.dATD_TransactionDate = Date.Today
                        'objJE.iATD_TrType = 4
                        'objJE.iATD_BillId = iJEID
                        'objJE.iATD_PaymentType = iPaymentType

                        objJE.iAJTB_Deschead = dtJEdetails.Rows(j)("HeadID")
                        objJE.iAJTB_Desc = dtJEdetails.Rows(j)("HeadID")
                        objJE.sAJTB_DescName = dtJEdetails.Rows(j)("GLDescription")


                        Dim dTransDbAmt, dTransCrAmt As Double
                        If (IsDBNull(dtJEdetails.Rows(j)("Debit").ToString()) = False) And (dtJEdetails.Rows(j)("Debit") <> "0.00") Then
                            objJE.dAJTB_Debit = Convert.ToDouble(dtJEdetails.Rows(j)("Debit"))
                            iTranStat = 0
                            dTransAMt = Convert.ToDouble(dtJEdetails.Rows(j)("Debit").ToString())
                            dTransDbAmt = Convert.ToDouble(dtJEdetails.Rows(j)("Debit").ToString())
                        Else
                            objJE.dAJTB_Debit = 0
                        End If

                        If (IsDBNull(dtJEdetails.Rows(j)("Credit").ToString()) = False) And (dtJEdetails.Rows(j)("Credit") <> "0.00") Then
                            objJE.dAJTB_Credit = Convert.ToDouble(dtJEdetails.Rows(j)("Credit").ToString())
                            iTranStat = 1
                            dTransAMt = Convert.ToDouble(dtJEdetails.Rows(j)("Credit").ToString())
                            dTransCrAmt = Convert.ToDouble(dtJEdetails.Rows(j)("Credit").ToString())
                        Else
                            objJE.dAJTB_Credit = 0
                        End If

                        objJE.iAJTB_CreatedBy = sSession.UserID
                        objJE.iAJTB_UpdatedBy = sSession.UserID
                        If lblStatus.Text = "Approve" Then
                            objJE.sAJTB_Status = "D"
                        Else
                            objJE.sAJTB_Status = "W"
                        End If

                        objJE.iAJTB_YearID = ddlFinancialYear.SelectedValue
                        objJE.iAJTB_CompID = sSession.AccessCodeID
                        'objJE.sATD_Operation = "U"
                        objJE.sAJTB_IPAddress = sSession.IPAddress
                        objJE.iAJTB_BillType = objJE.iAcc_JE_BillType
                        objJE.iAJTB_BranchId = IBranchid
                        ArrTD = objJE.SaveTransactionDetails(sSession.AccessCode, objJE)
                        If lblStatus.Text = "Approve" Then
                            objJE.UpdateJeDetDeactivate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, objJE.iAJTB_Deschead, ddlCustName.SelectedValue, iTranStat, dTransAMt, IBranchid, -(dTransDbAmt), -(dTransCrAmt))
                        End If
                    Next
                Next
            End If

            'objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iJEID, objGen.SafeSQL(txtComments.Text.Trim), "Saved(" & sCustomerAudit & ")", sSession.IPAddress)
            'objJE.DeletePaymentDetails(sSession.AccessCode, sSession.AccessCodeID, iJEID, "BILLID")


        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
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
            Response.Redirect(String.Format("~/FIN Statement/JournalEntryDetails.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub dgJE_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgJE.RowEditing
    End Sub
    Private Sub ddlCustName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustName.SelectedIndexChanged
        Dim dtbranch As New DataTable
        Try
            If ddlCustName.SelectedIndex <> 0 Then
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlCustName.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)
                sSession.CustomerID = ddlCustName.SelectedValue
                If ddlBranch.SelectedIndex > 0 Then
                    sSession.ScheduleBranchId = ddlBranch.SelectedValue
                Else
                End If
                Session("AllSession") = sSession
                dtbranch = objJE.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue)
                If dtbranch.Rows.Count > 0 Then
                    ddlBranch.DataSource = dtbranch
                    ddlBranch.DataTextField = "BranchName"
                    ddlBranch.DataValueField = "Branchid"
                    ddlBranch.DataBind()
                    ddlBranch.Items.Insert(0, "Select Branch Name")
                    If sSession.ScheduleBranchId <> 0 Then
                        ddlBranch.SelectedValue = sSession.ScheduleBranchId
                        ddlBranch_SelectedIndexChanged(sender, e)
                    Else
                        BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
                        sSession.ScheduleBranchId = 0
                        'dgJE.DataSource = Nothing
                        'dgJE.DataBind()
                    End If
                    Session("AllSession") = sSession
                Else
                    lblStatus.Text = "Customer should have atleast one Branch, Please add"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    ddlBranch.DataSource = dtbranch
                    ddlBranch.DataTextField = "BranchName"
                    ddlBranch.DataValueField = "Branchid"
                    ddlBranch.DataBind()
                    ddlBranch.Items.Insert(0, "Select Branch Name")
                    Exit Sub
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Customer"
                lblPaymentMasterValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustName_SelectedIndexChanged")
            'Throw
        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            If ddlFinancialYear.SelectedIndex > 0 Then
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            End If
            Session("AllSession") = sSession
            If ddlCustName.SelectedIndex > 0 Then
                sSession.CustomerID = ddlCustName.SelectedValue
                ddlCustName_SelectedIndexChanged(sender, e)
            Else
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            If ddlCustName.SelectedIndex > 0 Then
                dt = objJE.LoadJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, ddlBranch.SelectedValue)
            Else
                dt = objJE.LoadJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, 0, ddlFinancialYear.SelectedValue, ddlBranch.SelectedValue)
            End If

            If (dt.Rows.Count = 0) Then
                lblError.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/JournalEntry.rdlc")
            Dim Customer_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Customer_Name", ddlCustName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer_Name)
            Dim Financial_Year As ReportParameter() = New ReportParameter() {New ReportParameter("Financial_Year", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Financial_Year)
            Dim Branch_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Branch_Name", ddlBranch.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Branch_Name)

            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=JournalEntry" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable

        Try
            If ddlCustName.SelectedIndex > 0 Then
                dt = objJE.LoadJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, ddlBranch.SelectedValue)
            Else
                dt = objJE.LoadJournalEntry(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlStatus.SelectedIndex, 0, ddlFinancialYear.SelectedValue, ddlBranch.SelectedValue)
            End If

            If (dt.Rows.Count = 0) Then
                lblError.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If

            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/JournalEntry.rdlc")
            Dim Customer_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Customer_Name", ddlCustName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer_Name)
            Dim Financial_Year As ReportParameter() = New ReportParameter() {New ReportParameter("Financial_Year", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Financial_Year)
            Dim Branch_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Branch_Name", ddlBranch.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Branch_Name)

            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=JournalEntry" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Try
            If ddlBranch.SelectedIndex > 0 Then
                sSession.CustomerID = ddlCustName.SelectedValue
                If ddlBranch.SelectedIndex > 0 Then
                    sSession.ScheduleBranchId = ddlBranch.SelectedValue
                Else
                End If
                Session("AllSession") = sSession
                BindJEDetails(0, ddlStatus.SelectedIndex, ddlFinancialYear.SelectedValue)
            Else
                lblError.Text = "Select Branch"
                lblPaymentMasterValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class

