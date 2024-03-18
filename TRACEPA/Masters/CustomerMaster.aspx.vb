Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class CustomerMaster
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Masters_CustomerMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsGRACePermission As New clsGRACePermission
    Private sSession As AllSession
    Private Shared dtCust As DataTable
    Private objclsCustDashbord As New clsCustDashbord

    Private Shared Status As String
    'Private Shared sAD As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        Me.Form.DefaultButton = Me.imgbtnAdd.UniqueID
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnReport.Visible = False
                'sAD = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPCM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Approve/Activate/De-Activate,") = True Then
                '        sAD = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If
                BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                dtCust = objclsCustDashbord.BindCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Add(New ListItem("Activated", "0"))
            ddlStatus.Items.Add(New ListItem("De-Activated", "DC"))
            ddlStatus.Items.Add(New ListItem("Waiting for Approval", "W"))
            ddlStatus.Items.Add(New ListItem("All", "All"))
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            LoadAllCustomerToGrid(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Public Function LoadAllCustomerToGrid(ByVal iPageIndex As Integer, ByVal sPageType As String, ByVal sIsReport As String) As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                'If sAD = "YES" Then
                imgbtnDeActivate.Visible = True 'Activate
                'End If
            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "De-Activated"
                'If sAD = "YES" Then
                imgbtnActivate.Visible = True 'De-Activate
                'End If
            ElseIf ddlStatus.SelectedIndex = 2 Then
                sStatus = "Waiting for Approval"
                'If sAD = "YES" Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
                'End If
            End If
            If ddlStatus.SelectedIndex <= 2 Then
                dt = Nothing
                Dim DVFunctionStatus As New DataView(dtCust)
                DVFunctionStatus.RowFilter = "Status='" & sStatus & "'"
                DVFunctionStatus.Sort = "CustomerName Asc"
                dt = DVFunctionStatus.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            Else
                dt = Nothing
                Dim DVFunctionStatus As New DataView(dtCust)
                DVFunctionStatus.Sort = "CustomerName Asc"
                dt = DVFunctionStatus.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            End If
            gvCustomers.DataSource = dt
            gvCustomers.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllCustomerToGrid" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            Throw
        End Try
    End Function
    'Protected Sub DisplayMessage(ByVal sMsg As String, Optional ByVal iMsgType As Integer = 64)
    '    Try
    '        sMsg = "<script type= text/vbscript> MsgBox "" " & sMsg & " "", " & iMsgType & ", ""Customer Creation"" </script>"
    '        ClientScript.RegisterStartupScript(Me.GetType(), "Msg", sMsg)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub gvCustomers_PreRender(sender As Object, e As EventArgs) Handles gvCustomers.PreRender
        Dim dt As New DataTable
        Try
            If gvCustomers.Rows.Count > 0 Then
                gvCustomers.UseAccessibleHeader = True
                gvCustomers.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCustomers.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCustomers_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub gvCustomers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCustomers.RowCommand
        Dim lblCustID As New Label
        Dim txtCustomerName As New TextBox
        Dim txtAbbreviation As New TextBox
        Dim txtCity As New TextBox
        Dim sCustName As String = ""
        Dim sAbbreviation As String = ""
        Dim sCity As String = ""
        Dim oCustID As New Object, oStatusID As Object
        Dim DVCust As New DataView(dtCust)
        Dim Ccount As Integer = 0
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCustID = DirectCast(clickedRow.FindControl("lblCustID"), Label)
            If e.CommandName = "EditRow" Then
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                End If
                oCustID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(lblCustID.Text))
                Response.Redirect(String.Format("~/Masters/CustomerDetails.aspx?CustomerID={0}&StatusID={1}", oCustID, oStatusID), False)
            End If
            If e.CommandName = "Status" Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsCustDashbord.ApproveCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblCustID.Text), "D", sSession.IPAddress)
                    DVCust.Sort = "CustID"
                    Dim iIndex As Integer = DVCust.Find(Val(lblCustID.Text))
                    DVCust(iIndex)("Status") = "De-Activated"
                    DVCust.Sort = "CustomerName"
                    dtCust = DVCust.ToTable
                    lblCustomerValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then

                    'Ccount = objclsCustDashbord.GetCount(sSession.AccessCode, sSession.AccessCodeID)
                    'If Ccount >= 5 Then
                    '    lblCustomerValidationMsg.Text = "Please Contact System Admin to Add more Customers." : lblError.Text = "Please Contact System Admin to Add more Customers."
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustName').focus();", True)
                    '    Exit Sub
                    'End If

                    objclsCustDashbord.ApproveCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblCustID.Text), "A", sSession.IPAddress)
                    DVCust.Sort = "CustID"
                    Dim iIndex As Integer = DVCust.Find(Val(lblCustID.Text))
                    DVCust(iIndex)("Status") = "Activated"
                    DVCust.Sort = "CustomerName"
                    dtCust = DVCust.ToTable
                    lblCustomerValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsCustDashbord.ApproveCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblCustID.Text), "W", sSession.IPAddress)
                    DVCust.Sort = "CustID"
                    Dim iIndex As Integer = DVCust.Find(Val(lblCustID.Text))
                    DVCust(iIndex)("Status") = "Activated"
                    DVCust.Sort = "CustomerName"
                    dtCust = DVCust.ToTable
                    lblCustomerValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
            End If
            LoadAllCustomerToGrid(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCustomers_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub gvCustomers_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCustomers.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                gvCustomers.Columns(0).Visible = True
                gvCustomers.Columns(5).Visible = False
                gvCustomers.Columns(6).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    gvCustomers.Columns(0).Visible = True
                    'If sAD = "YES" Then
                    gvCustomers.Columns(5).Visible = True
                    'Else
                    '    gvCustomers.Columns(5).Visible = False
                    'End If
                    gvCustomers.Columns(6).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    gvCustomers.Columns(0).Visible = True
                    'If sAD = "YES" Then
                    gvCustomers.Columns(5).Visible = True
                    'Else
                    '    gvCustomers.Columns(5).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    gvCustomers.Columns(0).Visible = True
                    'If sAD = "YES" Then
                    gvCustomers.Columns(5).Visible = True
                    'Else
                    '    gvCustomers.Columns(5).Visible = False
                    'End If
                    gvCustomers.Columns(6).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    gvCustomers.Columns(0).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCustomers_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCustID As New Label
        Dim DVCust As New DataView(dtCust)
        Dim Ccount As Integer = 0
        Try
            lblError.Text = ""
            If gvCustomers.Rows.Count = 0 Then
                lblCustomerValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvCustomers.Rows.Count - 1
                chkSelect = gvCustomers.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblCustomerValidationMsg.Text = "Select Customer Name to Activate." : lblError.Text = "Select Customer Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To gvCustomers.Rows.Count - 1
                chkSelect = gvCustomers.Rows(i).FindControl("chkSelect")
                lblCustID = gvCustomers.Rows(i).FindControl("lblCustID")
                If chkSelect.Checked = True Then
                    'Ccount = objclsCustDashbord.GetCount(sSession.AccessCode, sSession.AccessCodeID)
                    'If Ccount >= 5 Then
                    '    lblCustomerValidationMsg.Text = "Please Contact System Admin to Add more Customers." : lblError.Text = "Please Contact System Admin to Add more Customers."
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustName').focus();", True)
                    '    Exit Sub
                    'End If
                    objclsCustDashbord.ApproveCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblCustID.Text), "A", sSession.IPAddress)
                    DVCust.Sort = "CustID"
                    Dim iIndex As Integer = DVCust.Find(Val(lblCustID.Text))
                    DVCust(iIndex)("Status") = "Activated"
                    DVCust.Sort = "CustomerName"
                    dtCust = DVCust.ToTable
                    lblCustomerValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
            Next
            LoadAllCustomerToGrid(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCustID As New Label
        Dim DVCust As New DataView(dtCust)
        Try
            lblError.Text = ""
            If gvCustomers.Rows.Count = 0 Then
                lblCustomerValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvCustomers.Rows.Count - 1
                chkSelect = gvCustomers.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblCustomerValidationMsg.Text = "Select Customer Name to Activate." : lblError.Text = "Select Customer Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvCustomers.Rows.Count - 1
                chkSelect = gvCustomers.Rows(i).FindControl("chkSelect")
                lblCustID = gvCustomers.Rows(i).FindControl("lblCustID")
                If chkSelect.Checked = True Then
                    objclsCustDashbord.ApproveCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblCustID.Text), "W", sSession.IPAddress)
                    DVCust.Sort = "CustID"
                    Dim iIndex As Integer = DVCust.Find(Val(lblCustID.Text))
                    DVCust(iIndex)("Status") = "Activated"
                    DVCust.Sort = "CustomerName"
                    dtCust = DVCust.ToTable
                    lblCustomerValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
            Next
            LoadAllCustomerToGrid(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCustID As New Label
        Dim DVCust As New DataView(dtCust)
        Try
            lblError.Text = ""
            If gvCustomers.Rows.Count = 0 Then
                lblCustomerValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvCustomers.Rows.Count - 1
                chkSelect = gvCustomers.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblCustomerValidationMsg.Text = "Select Customer Name to Activate." : lblError.Text = "Select Customer Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvCustomers.Rows.Count - 1
                chkSelect = gvCustomers.Rows(i).FindControl("chkSelect")
                lblCustID = gvCustomers.Rows(i).FindControl("lblCustID")
                If chkSelect.Checked = True Then
                    objclsCustDashbord.ApproveCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Val(lblCustID.Text), "D", sSession.IPAddress)
                    DVCust.Sort = "CustID"
                    Dim iIndex As Integer = DVCust.Find(Val(lblCustID.Text))
                    DVCust(iIndex)("Status") = "De-Activated"
                    DVCust.Sort = "CustomerName"
                    dtCust = DVCust.ToTable
                    lblCustomerValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
            Next
            LoadAllCustomerToGrid(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As New Object
        Try
            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(3))
            End If
            Response.Redirect(String.Format("CustomerDetails.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvCustomers.Rows.Count - 1
                    chkField = gvCustomers.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvCustomers.Rows.Count - 1
                    chkField = gvCustomers.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = LoadAllCustomerToGrid(0, "No", "NO")
            If dt.Rows.Count = 0 Then
                lblCustomerValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/CustomerMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustomerMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = LoadAllCustomerToGrid(0, "No", "NO")
            If dt.Rows.Count = 0 Then
                lblCustomerValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/CustomerMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustomerMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
End Class



