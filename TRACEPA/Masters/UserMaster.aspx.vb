Imports System
Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Imports System.Web.Mail
Partial Class UserMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_UserMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    'Private Shared sUserAD As String
    'Private Shared sUserBL As String
    Private Shared dtUserDetails As DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/AddUser24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnUnLock.ImageUrl = "~/Images/Unlock24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnUnBlock.ImageUrl = "~/Images/CheckedUser24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                imgbtnUnLock.Visible = False : imgbtnUnBlock.Visible = False
                'sUserAD = "NO" : sUserBL = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPUSR", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Approve/Activate/De-Activate,") = True Then
                '        sUserAD = "YES"
                '    End If
                '    If sFormButtons.Contains(",UnLock/UnBlock,") = True Then
                '        sUserBL = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If

                BindStatus()

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                dtUserDetails = objclsEmployeeMaster.LoadAllUserDetails(sSession.AccessCode, sSession.AccessCodeID)
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Locked")
            ddlStatus.Items.Insert(3, "Blocked")
            ddlStatus.Items.Insert(4, "Waiting for Approval")
            ddlStatus.Items.Insert(5, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oUserID As Object, oStatusID As Object = 0
        Try
            lblError.Text = ""
            oUserID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 4 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
            ElseIf ddlStatus.SelectedIndex = 5 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(5))
            End If
            Response.Redirect(String.Format("~/Masters/UserMasterDetails.aspx?UserID={0}&StatusID={1}", oUserID, oStatusID), False) 'UserMasterDetails
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            LoadAllUserDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Function LoadAllUserDeatils(ByVal iPageIndex As Integer, ByVal sPageType As String, ByVal sIsReport As String) As DataTable
        Dim dt As New DataTable
        Dim sSearchText As String = "", sStatus As String = ""
        Try
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnUnLock.Visible = False : imgbtnUnBlock.Visible = False : imgbtnWaiting.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                'If sUserAD = "YES" Then
                imgbtnDeActivate.Visible = True 'Activate
                'End If
            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "De-Activated"
                'If sUserAD = "YES" Then
                imgbtnActivate.Visible = True 'De-Activate
                'End If
            ElseIf ddlStatus.SelectedIndex = 2 Then
                sStatus = "Lock"
                'If sUserBL = "YES" Then
                imgbtnUnLock.Visible = True 'Lock
                'End If
            ElseIf ddlStatus.SelectedIndex = 3 Then
                sStatus = "Block"
                'If sUserBL = "YES" Then
                imgbtnUnBlock.Visible = True 'Block
                'End If
            ElseIf ddlStatus.SelectedIndex = 4 Then
                sStatus = "Waiting for Approval"
                'If sUserAD = "YES" Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
                'End If
            End If
            If ddlStatus.SelectedIndex <= 4 Then
                dt = Nothing
                Dim DVUsrDetails As New DataView(dtUserDetails)
                DVUsrDetails.RowFilter = "Status='" & sStatus & "'"
                DVUsrDetails.Sort = "EmployeeName ASC"
                dt = DVUsrDetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            Else
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtUserDetails)
                DVZRBADetails.Sort = "EmployeeName ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            End If
            If sIsReport = "NO" Then
                gvEmployeeDetails.DataSource = dt
                gvEmployeeDetails.DataBind()
            End If
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllUserDeatils" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Function
    Private Sub gvEmployeeDetails_PreRender(sender As Object, e As EventArgs) Handles gvEmployeeDetails.PreRender
        Dim dt As New DataTable
        Try
            If gvEmployeeDetails.Rows.Count > 0 Then
                gvEmployeeDetails.UseAccessibleHeader = True
                gvEmployeeDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                gvEmployeeDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeDetails_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvEmployeeDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvEmployeeDetails.RowCommand
        Dim oUserID As Object, oStatusID As Object
        Dim DVUsrDetails As New DataView(dtUserDetails)
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblEmpID As Label = DirectCast(clickedRow.FindControl("lblEmpID"), Label)
            If e.CommandName = "EditRow" Then
                oUserID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblEmpID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 4 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
                Else
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                End If
                Response.Redirect(String.Format("~/Masters/UserMasterDetails.aspx?UserID={0}&StatusID={1}", oUserID, oStatusID), False) 'UserMasterDetails
            End If
            If e.CommandName = "Status" Then
                If ddlStatus.SelectedIndex = 0 Then
                    If objclsCheckMasterIsInUse.CheckEmployeeNameIsInUse(sSession.AccessCode, sSession.AccessCodeID, lblEmpID.Text) = True Then
                        lblError.Text = "Already tag to some User, can't be De-Activate"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Already tag to some User, can't be De-Activate','', 'erroe');", True)
                        Exit Sub
                    End If
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "DeActivated")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "De-Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                    lblError.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "De-Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Activated")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                    lblError.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Unlock
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnLock")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                    lblError.Text = "Successfully Unlocked." : lblError.Text = "Successfully Unlocked."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Unlocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Unlocked','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 3 Then 'Unblock
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnBlock")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                    lblError.Text = "Successfully Unblocked." : lblError.Text = "Successfully Unblocked."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Unblocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Unblocked','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 4 Then 'Waiting for Approval
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Created")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                    SendEmail(lblEmpID.Text)
                    lblError.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Approved", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
                LoadAllUserDeatils(0, "True", "NO")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeDetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub

    Public Function SendEmail(ByVal sEmPloyeeID As String)
        Dim sStr As String
        Dim dtDetails As New DataTable
        Dim sEmail As String = ""
        Dim sUserName As String = ""
        Dim sPassword As String = ""
        Try
            Dim myMail As New System.Web.Mail.MailMessage()
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
            'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "karthikprasad@mmcspl.com")
            'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "9738860458@Raje")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "steffi@mmcspl.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "mmcs@736")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
            myMail.From = "steffi@mmcspl.com"

            dtDetails = objclsEmployeeMaster.GetCustomerUserDetails(sSession.AccessCode, sSession.AccessCodeID, sEmPloyeeID)
            If dtDetails.Rows.Count > 0 Then
                sEmail = dtDetails.Rows(0)("Usr_Email").ToString()
                sUserName = dtDetails.Rows(0)("Usr_LoginName").ToString()
                sPassword = objclsGRACeGeneral.DecryptPassword(dtDetails.Rows(0)("Usr_Password").ToString())
            End If

            myMail.Bcc = sEmail ' To email id
            myMail.Subject = "Intimation mail for accessing TRACe PA"
            myMail.BodyFormat = MailFormat.Html

            sStr = "<p><img src='https://ckeditor.com/apps/ckfinder/userfiles/files/image(1).png' style='height:36px; width:222px' /></p>"
            sStr = sStr & "<p>&nbsp;</p>"
            sStr = sStr & "<p style='text-align:center'><span style='font-size:18px'><span style='font-family:Verdana,Geneva,sans-serif'><strong>Intimation mail</strong></span></span></p>"
            sStr = sStr & "<p style='text-align:center'><strong><span style='font-size: 11pt'><span style='font-family:Calibri, sans - serif'>Login Access to TRACe PA</span></span></strong></p>"
            sStr = sStr & "<p><span style='font-size:12px'><span style='font-family:Verdana,Geneva,sans-serif'>Greetings from TRACe PA.&nbsp;</span></span></p>"
            sStr = sStr & "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12px'>This mail is an intimation with link and credentials to access TRACe PA which is used by your Auditor.</span></span></p>"
            sStr = sStr & "<table border='1' cellpadding='1' cellspacing='1' style='width:90%'>"
            sStr = sStr & "<tbody> <tr> "
            sStr = sStr & "<td><a href='http://localhost/TracePA/Loginpage.aspx'><u><span style='font-size:12px'><span style='font-family:Verdana,Geneva,sans-serif'><strong>http://localhost/TracePA/Loginpage.aspx</strong></span></span></u></a></td>"
            sStr = sStr & "</tr> <tr>"
            sStr = sStr & "<td><p><span style='font-size:12px'><span style='font-family:Verdana, Geneva, sans - serif'>User Name : " & sUserName & "</span></span></p>"
            sStr = sStr & "<p><span style='font-size:12px'><span style='font-family:Verdana,Geneva,sans-serif'>Password : " & sPassword & "</span></span></p>"
            sStr = sStr & "</td></tr><tr><td>"
            sStr = sStr & "<p style='text-align:justify'><span style='font-size:12px'><span style='font-family:Verdana,Geneva,sans-serif'><strong>This allows you to share the documents with your auditor on receiving a request.</strong></span></span></p>"
            sStr = sStr & "<p style='text-align:justify'><span style='font-size:12px'><span style='font-family:Verdana,Geneva,sans-serif'><strong>Allows you to search and view the reports shared by auditor</strong></span></span></p>"
            sStr = sStr & "</td></tr></tbody></table>"
            sStr = sStr & "<p>&nbsp;</p>"
            sStr = sStr & "<p><span style='font-size:12px'><span style='font-family:Verdana,Geneva,sans-serif'><span style='color:black'>Thanks, </span></span></span></p>"
            sStr = sStr & "<p><span style='font-size:12px'><span style='font-family:Verdana,Geneva,sans-serif'><span style='color:black'>TRACe PA Team</span></span></span></p>"

            myMail.Body = sStr
            myMail.BodyEncoding = System.Text.Encoding.UTF8
            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com" + ":" + "465"
            System.Web.Mail.SmtpMail.Send(myMail)
        Catch ex As Exception

        End Try
    End Function
    Private Sub gvEmployeeDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvEmployeeDetails.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                'If sUserAD = "YES" Then
                gvEmployeeDetails.Columns(0).Visible = True
                'End If
                gvEmployeeDetails.Columns(9).Visible = False
                gvEmployeeDetails.Columns(10).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'If sUserAD = "YES" Then
                    gvEmployeeDetails.Columns(9).Visible = True
                    'End If
                    gvEmployeeDetails.Columns(10).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'If sUserAD = "YES" Then
                    gvEmployeeDetails.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Unlock16.png" : imgbtnStatus.ToolTip = "Unlock"
                    'If sUserAD = "YES" Then
                    gvEmployeeDetails.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.ImageUrl = "~/Images/CheckedUser16.png" : imgbtnStatus.ToolTip = "Unblock"
                    'If sUserAD = "YES" Then
                    gvEmployeeDetails.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 4 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'If sUserAD = "YES" Then
                    gvEmployeeDetails.Columns(9).Visible = True
                    'End If
                    gvEmployeeDetails.Columns(10).Visible = True
                End If

                If ddlStatus.SelectedIndex = 5 Then
                    gvEmployeeDetails.Columns(0).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeDetails_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim DVUsrDetails As New DataView(dtUserDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select User to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select User to Activate','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Activated")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                End If
            Next
            lblError.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
            LoadAllUserDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer
        Dim lblEmpID As New Label
        Dim DVUsrDetails As New DataView(dtUserDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select User to De-Activate." : lblError.Text = "Select User to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select User to De-Activate','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    If objclsCheckMasterIsInUse.CheckEmployeeNameIsInUse(sSession.AccessCode, sSession.AccessCodeID, lblEmpID.Text) = False Then
                        objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "DeActivated")
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "De-Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                        DVUsrDetails.Sort = "EmpID"
                        Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                        DVUsrDetails(iIndex)("Status") = "De-Activated"
                        dtUserDetails = DVUsrDetails.ToTable
                    Else
                        iCheck = 1
                    End If
                End If
            Next
            If iCheck = 0 Then
                lblError.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
            Else
                lblError.Text = "Already tagged to other forms, can't be De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Already tagged to other forms, can't be De-Activate','', 'error');", True)
            End If
            LoadAllUserDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnUnBlock_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUnBlock.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim DVUsrDetails As New DataView(dtUserDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to Unblock." : lblError.Text = "No data to Unblock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Unblock','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select User to Unblock." : lblError.Text = "Select User to Unblock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select User to Unblock','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnBlock")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                End If
            Next
            lblError.Text = "Successfully Unblocked."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Unblocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Unblocked','', 'success');", True)
            LoadAllUserDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUnBlock_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnUnLock_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUnLock.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim DVUsrDetails As New DataView(dtUserDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to Unlock." : lblError.Text = "No data to Unlock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Unlock','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select User to Unlock." : lblError.Text = "Select User to Unlock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select User to Unlock','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnLock")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                End If
            Next
            lblError.Text = "Successfully Unlocked." : lblError.Text = "Successfully Unlocked."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Unlocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Unlocked','', 'success');", True)
            LoadAllUserDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUnLock_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim DVUsrDetails As New DataView(dtUserDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblError.Text = "No data to Approve." : lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select User to Approve." : lblError.Text = "Select User to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select User to Approve','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Created")
                    DVUsrDetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVUsrDetails.Find(lblEmpID.Text)
                    DVUsrDetails(iIndex)("Status") = "Activated"
                    dtUserDetails = DVUsrDetails.ToTable
                End If
            Next
            lblError.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Approved", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
            LoadAllUserDeatils(0, "True", "NO")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvEmployeeDetails.Rows.Count - 1
                    chkField = gvEmployeeDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvEmployeeDetails.Rows.Count - 1
                    chkField = gvEmployeeDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = LoadAllUserDeatils(0, "True", "YES")
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/UserMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustomerUserMaster" + ".pdf")
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
            dt = LoadAllUserDeatils(0, "True", "YES")
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/UserMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustomerUserMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
End Class
