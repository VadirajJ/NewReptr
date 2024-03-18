Imports System
Imports System.Data
Imports System.Web.Mail
Imports BusinesLayer
Partial Class DRLSampling
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_DRLSampling"
    Private sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAttachments As New clsAttachments
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsDRLLog As New clsDRLLog
    Private objAdminMaster As New clsAdminMaster
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsStandardAudit As New clsStandardAudit

    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared iCustID As Integer = 0
    Private Shared iAuditTypeID As Integer = 0
    Private Shared iSelectedCheckPointID As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnSendMail.ImageUrl = "~/Images/EMail24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If
                chkSendMail.Visible = True : chkSendMail.Checked = False
                imgbtnSave.Visible = True : imgbtnUpdate.Visible = False : imgbtnBack.Visible = False
                imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False : btnAddAttch.Visible = True : txtfile.Visible = True : lblBrowse.Visible = True : lblSize.Visible = True
                iAttachID = 0 : ClearAll("NO")
                LoadFinalcialYear(sSession.AccessCode)
                lblHComment.Text = "Comments"
                BindCustomerName() : BindAllAuditNo(0)
                BindDocumentRequestedList() : BindStatus()
                DRLClientSideValidation()
                txtRequestedOn.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                txtRequestedOn.Enabled = False
                If Request.QueryString("AuditID") IsNot Nothing Then
                    ddlAuditNo.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditID")))
                ElseIf sSession.AuditCodeID > 0 Then
                    ddlAuditNo.SelectedValue = sSession.AuditCodeID
                End If
                If ddlAuditNo.SelectedIndex > 0 Then
                    BindScheduledDetails(ddlAuditNo.SelectedValue)
                    BindAuditCheckPoints(ddlAuditNo.SelectedValue)
                    rboCheckPoint_CheckedChanged(sender, e)
                End If
                If Request.QueryString("CheckPointID") IsNot Nothing Then
                        imgbtnBack.Visible = True
                        Dim iCheckPointID As Integer = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CheckPointID")))
                        lstCheckPoint.Enabled = iCheckPointID > 0 : rboCheckPoint.Checked = iCheckPointID > 0 : rboOthers.Checked = iCheckPointID <= 0
                        Dim sCheckPointIDs As String = iCheckPointID
                        If sCheckPointIDs <> "" Then
                            sCheckPointIDs = If(sCheckPointIDs.StartsWith(","), sCheckPointIDs, "," & sCheckPointIDs) & If(sCheckPointIDs.EndsWith(","), "", ",")
                            For Each item In lstCheckPoint.Items
                                If sCheckPointIDs.Contains("," & item.Value & ",") Then
                                    item.Selected = True
                                End If
                            Next
                            lstCheckPoint_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindScheduledDetails(ByVal iAuditID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            If dt.Rows.Count = 1 Then
                ddlFinancialYear.SelectedValue = dt.Rows(0)("SA_YearID")
                iCustID = dt.Rows(0)("SA_CustID")
                ddlCustomerName.SelectedValue = iCustID
                BindAllAuditNo(ddlCustomerName.SelectedValue)
                ddlAuditNo.SelectedValue = iAuditID
                iAuditTypeID = dt.Rows(0)("SA_AuditTypeID")
                lblAuditType.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("AuditType").ToString())
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/StandardAudit/ConductAudit.aspx?AuditID={0}&CustID={1}", HttpUtility.UrlDecode(Request.QueryString("AuditID")), HttpUtility.UrlDecode(Request.QueryString("CustID"))))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Try
            ddlFinancialYear.DataSource = objclsStandardAudit.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Public Sub DRLClientSideValidation()
        Try
            RFVAuditNo.InitialValue = "Select Audit No" : RFVAuditNo.ErrorMessage = "Select Audit No."
            'RFVDocumentRequestedList.InitialValue = "Select Document Requested List" : RFVDocumentRequestedList.ErrorMessage = "Select Document Requested List."
            RFVRequestedOn.ControlToValidate = "txtRequestedOn" : RFVRequestedOn.ErrorMessage = "Enter Requested On"
            REVRequestedOn.ErrorMessage = "Enter valid Date." : REVRequestedOn.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            If lblHComment.Text = "Comments" Then
                'RFVComment.ControlToValidate = "txtComment" : RFVComment.ErrorMessage = "Enter Comments."
                REVComment.ErrorMessage = "Comment exceeded maximum size(max 5000 characters)." : REVComment.ValidationExpression = "^[\s\S]{0,5000}$"
            ElseIf lblHComment.Text = "Received Comments" Then
                'RFVComment.ControlToValidate = "txtComment" : RFVComment.ErrorMessage = "Enter Received Comments."
                REVComment.ErrorMessage = "Received Comment exceeded maximum size(max 5000 characters)." : REVComment.ValidationExpression = "^[\s\S]{0,5000}$"
            End If
            'RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
            RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-\.]+)+([;]([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-\.]+))*$"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DRLClientSideValidation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindAllAuditNo(ByVal iCustID As Integer)
        Try
            BindAuditCheckPoints(0)
            If iCustID = 0 And ddlCustomerName.SelectedIndex > 0 Then
                iCustID = ddlCustomerName.SelectedValue
            End If
            ddlAuditNo.DataSource = objclsStandardAudit.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, sSession.UserID, bLoginUserIsPartner)
            ddlAuditNo.DataTextField = "SA_AuditNo"
            ddlAuditNo.DataValueField = "SA_ID"
            ddlAuditNo.DataBind()
            ddlAuditNo.Items.Insert(0, "Select Audit No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAuditNo" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindAuditCheckPoints(ByVal iAuditID As Integer)
        Try
            lstCheckPoint.DataSource = objclsStandardAudit.LoadScheduledAuditAllCheckPoints(sSession.AccessCode, sSession.AccessCodeID, iAuditID, sSession.UserID, bLoginUserIsPartner)
            lstCheckPoint.DataTextField = "ACM_Checkpoint"
            lstCheckPoint.DataValueField = "ACM_ID"
            lstCheckPoint.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditCheckPoints" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindCustomerName()
        Try
            ddlCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "CUST_Name"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomerName" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindDocumentRequestedList()
        Try
            ddlDocumentRequestedList.DataSource = objAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "DRL")
            ddlDocumentRequestedList.DataTextField = "Name"
            ddlDocumentRequestedList.DataValueField = "PKID"
            ddlDocumentRequestedList.DataBind()
            ddlDocumentRequestedList.Items.Insert(0, "Select Document Requested List")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDocumentRequestedList" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Add(New ListItem("Select Status", "0"))
            ddlStatus.Items.Add(New ListItem("Outstanding", "1"))
            ddlStatus.Items.Add(New ListItem("Acceptable", "2"))
            ddlStatus.Items.Add(New ListItem("Partially", "3"))
            ddlStatus.Items.Add(New ListItem("No", "4"))
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            lblError.Text = "" : ClearAll("NO")
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            lblAuditType.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then
                txtEmailID.Text = objclsDRLLog.GetCustAllUserEmails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                BindAllAuditNo(ddlCustomerName.SelectedValue)
            Else
                BindAllAuditNo(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAuditNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNo.SelectedIndexChanged
        Try
            lblError.Text = "" : ClearAll("NO")
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            lblAuditType.Text = ""
            If ddlAuditNo.SelectedIndex > 0 Then
                lstCheckPoint.Enabled = rboCheckPoint.Checked
                BindScheduledDetails(ddlAuditNo.SelectedValue)
                If rboCheckPoint.Checked Then rboCheckPoint_CheckedChanged(sender, e) Else rboOthers_CheckedChanged(sender, e)
            Else
                BindAuditCheckPoints(0)
                lstCheckPoint.Enabled = rboCheckPoint.Checked
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadDRLDetails(ByVal iCustID As Integer, ByVal iTaskID As Integer, ByVal iChkPointID As Integer, ByVal iDRListID As Integer)
        Dim dt As New DataTable
        Try
            txtRequestedOn.Text = "" ': txtEmailID.Text = ""
            txtRequestedOn.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
            txtRequestedOn.Enabled = False
            lblHComment.Text = "Comments"
            DRLClientSideValidation()
            dt = objclsDRLLog.GetDRLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iTaskID, iChkPointID, iCustID, iDRListID)
            chkSendMail.Visible = True : chkSendMail.Checked = False
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False

            lstCheckPoint.Enabled = iChkPointID > 0 : rboCheckPoint.Checked = iChkPointID > 0 : rboOthers.Checked = iChkPointID <= 0

            If dt.Rows.Count > 0 Then
                lblHComment.Text = "Received Comments"
                DRLClientSideValidation()
                chkSendMail.Visible = False : chkSendMail.Checked = False
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                If IsDBNull(dt.Rows(0)("ADRL_RequestedListID")) = False AndAlso dt.Rows(0)("ADRL_RequestedListID") > 0 Then
                    ddlDocumentRequestedList.SelectedValue = dt.Rows(0)("ADRL_RequestedListID")
                End If
                If IsDBNull(dt.Rows(0)("ADRL_RequestedOn")) = False Then
                    txtRequestedOn.Text = dt.Rows(0)("ADRL_RequestedOn")
                End If

                If IsDBNull(dt.Rows(0)("ADRL_TimlinetoResOn")) = False Then
                    txtRespndate.Text = dt.Rows(0)("ADRL_TimlinetoResOn")
                End If
                If IsDBNull(dt.Rows(0)("ADRL_EmailID")) = False Then
                    txtEmailID.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ADRL_EmailID"))
                End If
                If IsDBNull(dt.Rows(0)("ADRL_Comments")) = False Then
                    txtComment.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ADRL_Comments"))
                End If
                If IsDBNull(dt.Rows(0)("ADRL_ReceivedComments")) = False Then
                    txtComment.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ADRL_ReceivedComments"))
                End If
                If IsDBNull(dt.Rows(0)("ADRL_ReceivedOn")) = False Then
                    txtRequestedOn.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ADRL_ReceivedOn"))
                End If
                If IsDBNull(dt.Rows(0)("ADRL_LogStatus")) = False Then
                    ddlStatus.SelectedValue = dt.Rows(0)("ADRL_LogStatus")
                End If
                iAttachID = 0 : lblBadgeCount.Text = 0
                dgAttach.DataSource = Nothing
                dgAttach.DataBind()
                If IsDBNull(dt.Rows(0).Item("ADRL_AttachID")) = False Then
                    iAttachID = dt.Rows(0).Item("ADRL_AttachID")
                    BindAllAttachments(sSession.AccessCode, iAttachID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDRLDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindDRLLogDetails(ByVal iCustID As Integer, ByVal iAuditNo As Integer, ByVal sCheckPointIDs As String)
        Try
            gvDRLLog.DataSource = objclsDRLLog.LoadDRLdg(sSession.AccessCode, sSession.AccessCodeID, iAuditNo, sCheckPointIDs, iCustID, ddlFinancialYear.SelectedValue, 0, 0)
            gvDRLLog.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDRLLogDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub ClearAll(ByVal sType As String)
        Try
            ddlStatus.Visible = False : lblHStatus.Visible = False : imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
            If sType = "YES" Then
                lblHDocumentRequestedList.Visible = True : ddlDocumentRequestedList.Visible = True
                ddlDocumentRequestedList.SelectedIndex = 0
                lblHEmailID.Visible = True : txtEmailID.Visible = True
                If ddlCustomerName.SelectedIndex = 0 Then
                    txtEmailID.Text = ""
                End If
                lblRequestedOn.Visible = True : txtRequestedOn.Visible = True
                lblHComment.Visible = True : txtComment.Visible = True : txtComment.Text = ""
            ElseIf sType = "NO" Then
                lblHDocumentRequestedList.Visible = False : ddlDocumentRequestedList.Visible = False
                lblHEmailID.Visible = False : txtEmailID.Visible = False
                lblRequestedOn.Visible = False : txtRequestedOn.Visible = False
                lblHComment.Visible = False : txtComment.Visible = False
                gvDRLLog.DataSource = Nothing
                gvDRLLog.DataBind()
            End If
            If ddlCustomerName.SelectedIndex > 0 Then
                txtEmailID.Text = objclsDRLLog.GetCustAllUserEmails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Protected Sub rboCheckPoint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboCheckPoint.CheckedChanged
        Try
            lblError.Text = ""
            lstCheckPoint.Enabled = True
            gvDRLLog.DataSource = Nothing
            gvDRLLog.DataBind()
            If ddlAuditNo.SelectedIndex > 0 Then
                ClearAll("YES") : lblRequestedOn.Text = "* Requested On"
                BindAuditCheckPoints(ddlAuditNo.SelectedValue)
                lstCheckPoint_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboCheckPoint_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboOthers_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboOthers.CheckedChanged
        Try
            lblError.Text = ""
            BindAuditCheckPoints(0)
            lstCheckPoint.Enabled = False
            lstCheckPoint_SelectedIndexChanged(sender, e)
            imgbtnSave.Visible = False
            If ddlAuditNo.SelectedIndex > 0 And gvDRLLog.Rows.Count = 0 Then
                imgbtnSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboOthers_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lstCheckPoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCheckPoint.SelectedIndexChanged
        Dim dt As New DataTable
        Dim iChkPointID As Integer = 0
        Dim sCheckPointIDs As String = ""
        Try
            lblError.Text = "" : ClearAll("NO")
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            lblHComment.Text = "Comments"
            DRLClientSideValidation()

            If rboCheckPoint.Checked Then
                For j = 0 To lstCheckPoint.Items.Count - 1
                    If lstCheckPoint.Items(j).Selected = True Then
                        iChkPointID = lstCheckPoint.Items(j).Value
                        sCheckPointIDs = sCheckPointIDs & "," & lstCheckPoint.Items(j).Value
                    End If
                Next
            End If

            If ddlAuditNo.SelectedIndex > 0 Then
                ClearAll("YES") : lblRequestedOn.Text = "* Requested On"
                txtComment.Visible = True : lblHComment.Visible = True
                If rboCheckPoint.Checked Then sCheckPointIDs = "" Else sCheckPointIDs = "0"
                dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count > 0 And ddlCustomerName.SelectedIndex = 0 Then
                    ddlCustomerName.SelectedValue = dt.Rows(0).Item("SA_CustID")
                    BindAllAuditNo(ddlCustomerName.SelectedValue)
                    ddlAuditNo.SelectedValue = dt.Rows(0).Item("SA_ID")
                    If rboCheckPoint.Checked Then
                        BindAuditCheckPoints(ddlAuditNo.SelectedValue)
                        sCheckPointIDs = If(sCheckPointIDs.StartsWith(","), sCheckPointIDs, "," & sCheckPointIDs) & If(sCheckPointIDs.EndsWith(","), "", ",")
                        For k = 0 To lstCheckPoint.Items.Count - 1
                            If sCheckPointIDs.Contains("," & lstCheckPoint.Items(k).Value & ",") = True Then
                                lstCheckPoint.Items(k).Selected = True
                            End If
                        Next
                        sCheckPointIDs = sCheckPointIDs.TrimStart(","c).TrimEnd(","c)
                    Else
                        BindAuditCheckPoints(0)
                    End If
                End If
                txtEmailID.Text = objclsDRLLog.GetCustAllUserEmails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                BindDRLLogDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, sCheckPointIDs)
            End If
            txtRespndate.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstCheckPoint_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlDocumentRequestedList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDocumentRequestedList.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sSelectedlstReceivedListID As String = ""
        Try
            lblError.Text = ""
            DRLClientSideValidation()
            txtRequestedOn.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
            txtRequestedOn.Enabled = False
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False : chkSendMail.Visible = True : chkSendMail.Checked = False
            ddlStatus.Visible = False : lblHStatus.Visible = False : imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
            txtComment.Text = "" : lblHComment.Text = "Comments" : lblRequestedOn.Text = "* Requested On"
            'txtEmailID.Text = ""

            Dim iChkPointID As Integer = 0
            Dim sCheckPointIDs As String = ""
            For j = 0 To lstCheckPoint.Items.Count - 1
                If lstCheckPoint.Items(j).Selected = True Then
                    iChkPointID = lstCheckPoint.Items(j).Value
                    sCheckPointIDs = sCheckPointIDs & "," & lstCheckPoint.Items(j).Value
                End If
            Next
            sCheckPointIDs = sCheckPointIDs.TrimStart(","c).TrimEnd(","c)
            Dim iDocumentRequestedList As Integer = If(ddlDocumentRequestedList.SelectedIndex > 0, ddlDocumentRequestedList.SelectedValue, 0)
            If sCheckPointIDs.Contains(",") = False Then
                If ddlCustomerName.SelectedIndex > 0 And ddlAuditNo.SelectedIndex > 0 And iChkPointID > 0 Then
                    'txtEmailID.Text = objclsDRLLog.GetCustAllUserEmails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                    LoadDRLDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, iChkPointID, iDocumentRequestedList)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDocumentRequestedList_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            'ddlCheckPoint_SelectedIndexChanged(sender, e)
            ddlDocumentRequestedList_SelectedIndexChanged(sender, e)
            If rboOthers.Checked = True And ddlCustomerName.SelectedIndex > 0 And ddlAuditNo.SelectedIndex > 0 And gvDRLLog.Rows.Count > 0 Then
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objDRLLog As New str_DRLLog
        Dim Array() As String
        Dim iDRLPKID As Integer = 0
        Try
            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name." : lblDRLLogDetailsValidationMsg.Text = "Select Customer Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                ddlCustomerName.Focus()
                Exit Try
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblDRLLogDetailsValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Try
            End If

            If rboCheckPoint.Checked Then
                Dim iCheckPoint As Integer = 0
                For x = 0 To lstCheckPoint.Items.Count - 1
                    If lstCheckPoint.Items(x).Selected = True Then
                        iCheckPoint = 1
                    End If
                Next
                If iCheckPoint = 0 Then
                    lblError.Text = "Select Check Point." : lblDRLLogDetailsValidationMsg.Text = "Select Check Point."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                    Exit Try
                End If
            End If
            If ddlDocumentRequestedList.SelectedIndex = 0 And txtComment.Text = "" Then
                If lblHComment.Text = "Comments" Then
                    lblError.Text = "Select Document Requested List or Enter Comments." : lblDRLLogDetailsValidationMsg.Text = "Select Document Requested List or Enter Comments."
                ElseIf lblHComment.Text = "Received Comments" Then
                    lblError.Text = "Select Document Requested List or Enter Received Comments." : lblDRLLogDetailsValidationMsg.Text = "Select Document Requested List or Enter Received Comments."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            Dim iDocumentRequestedList As Integer = If(ddlDocumentRequestedList.SelectedIndex > 0, ddlDocumentRequestedList.SelectedValue, 0)
            Dim iFinancialYearID As Integer = ddlFinancialYear.SelectedValue
            objDRLLog.iADRL_YearID = iFinancialYearID
            objDRLLog.iADRL_AuditNo = ddlAuditNo.SelectedValue
            objDRLLog.iADRL_CustID = ddlCustomerName.SelectedValue
            objDRLLog.iADRL_RequestedListID = iDocumentRequestedList
            objDRLLog.iADRL_RequestedTypeID = 0
            objDRLLog.sADRL_RequestedOn = txtRequestedOn.Text
            objDRLLog.sADRL_TimlinetoResOn = txtRespndate.Text
            objDRLLog.sADRL_EmailID = objclsGRACeGeneral.SafeSQL(txtEmailID.Text.Trim)
            objDRLLog.sADRL_Comments = objclsGRACeGeneral.SafeSQL(txtComment.Text.Trim)
            objDRLLog.iADRL_CrBy = sSession.UserID
            objDRLLog.iADRL_UpdatedBy = sSession.UserID
            objDRLLog.sADRL_IPAddress = sSession.IPAddress
            objDRLLog.iADRL_CompID = sSession.AccessCodeID

            Dim sCheckPointIDs As String = ""
            If rboCheckPoint.Checked Then
                For j = 0 To lstCheckPoint.Items.Count - 1
                    If lstCheckPoint.Items(j).Selected = True Then
                        sCheckPointIDs = sCheckPointIDs & "," & lstCheckPoint.Items(j).Value
                        objDRLLog.iADRL_FunID = lstCheckPoint.Items(j).Value

                        iDRLPKID = objclsDRLLog.GetDRLPKID(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, ddlAuditNo.SelectedValue, lstCheckPoint.Items(j).Value, iDocumentRequestedList, 0)
                        If iDRLPKID = 0 Then objDRLLog.iADRL_ID = 0 Else objDRLLog.iADRL_ID = iDRLPKID
                        Array = objclsDRLLog.SaveDRLLogReceivedList_Details(sSession.AccessCode, objDRLLog)
                        If Array(0) = "3" Then
                            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Audit", "DRL Log", "Saved", ddlAuditNo.SelectedValue, ddlAuditNo.SelectedItem.Text, 0, "", sSession.IPAddress)
                            lblError.Text = "Successfully Saved." : lblDRLLogDetailsValidationMsg.Text = "Successfully Saved."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                        End If
                    End If
                Next
                sCheckPointIDs = sCheckPointIDs.TrimStart(","c).TrimEnd(","c)
            ElseIf rboOthers.Checked Then
                objDRLLog.iADRL_FunID = 0
                iDRLPKID = objclsDRLLog.GetDRLPKID(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, ddlAuditNo.SelectedValue, 0, iDocumentRequestedList, 0)
                If iDRLPKID = 0 Then objDRLLog.iADRL_ID = 0 Else objDRLLog.iADRL_ID = iDRLPKID
                Array = objclsDRLLog.SaveDRLLogReceivedList_Details(sSession.AccessCode, objDRLLog)
                If Array(0) = "3" Then
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Audit", "DRL Log", "Saved", ddlAuditNo.SelectedValue, ddlAuditNo.SelectedItem.Text, 0, "", sSession.IPAddress)
                    lblError.Text = "Successfully Saved." : lblDRLLogDetailsValidationMsg.Text = "Successfully Saved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                End If
            End If
            objclsStandardAudit.UpdateStandardAuditStatus(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, 2)
            If chkSendMail.Checked = True Then
                SendMail()
            End If
            objclsDRLLog.updateStandardAudit_Audit_DRLLog_RemarksHistory(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, objDRLLog.iADRL_FunID, txtComment.Text, sSession.UserID, sSession.IPAddress, objDRLLog.sADRL_EmailID, objDRLLog.sADRL_TimlinetoResOn, ddlFinancialYear.SelectedValue)
            BindDRLLogDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, sCheckPointIDs)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim objDRLLog As New str_DRLLog
        Dim Array() As String
        Dim iDRLPKID As Integer = 0
        Try
            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name." : lblDRLLogDetailsValidationMsg.Text = "Select Customer Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                ddlCustomerName.Focus()
                Exit Try
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblDRLLogDetailsValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Try
            End If

            If rboCheckPoint.Checked Then
                Dim iCheckPoint As Integer = 0
                For x = 0 To lstCheckPoint.Items.Count - 1
                    If lstCheckPoint.Items(x).Selected = True Then
                        iCheckPoint = 1
                    End If
                Next
                If iCheckPoint = 0 Then
                    lblError.Text = "Select Check Point." : lblDRLLogDetailsValidationMsg.Text = "Select Check Point."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                    Exit Try
                End If
            End If

            If ddlDocumentRequestedList.SelectedIndex = 0 And txtComment.Text = "" Then
                If lblHComment.Text = "Comments" Then
                    lblError.Text = "Select Document Requested List or Enter Comments." : lblDRLLogDetailsValidationMsg.Text = "Select Document Requested List or Enter Comments."
                ElseIf lblHComment.Text = "Received Comments" Then
                    lblError.Text = "Select Document Requested List or Enter Received Comments." : lblDRLLogDetailsValidationMsg.Text = "Select Document Requested List or Enter Received Comments."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            Dim iDocumentRequestedList As Integer = If(ddlDocumentRequestedList.SelectedIndex > 0, ddlDocumentRequestedList.SelectedValue, 0)
            Dim iFinancialYearID As Integer = ddlFinancialYear.SelectedValue
            objDRLLog.iADRL_YearID = iFinancialYearID
            objDRLLog.iADRL_AuditNo = ddlAuditNo.SelectedValue
            objDRLLog.iADRL_CustID = ddlCustomerName.SelectedValue
            objDRLLog.iADRL_RequestedListID = iDocumentRequestedList
            objDRLLog.iADRL_RequestedTypeID = 0
            objDRLLog.sADRL_RequestedOn = txtRequestedOn.Text
            objDRLLog.sADRL_TimlinetoResOn = txtRespndate.Text
            objDRLLog.sADRL_EmailID = objclsGRACeGeneral.SafeSQL(txtEmailID.Text.Trim)
            objDRLLog.iADRL_CrBy = sSession.UserID
            objDRLLog.iADRL_UpdatedBy = sSession.UserID
            objDRLLog.sADRL_IPAddress = sSession.IPAddress
            objDRLLog.iADRL_CompID = sSession.AccessCodeID

            Dim sCheckPointIDs As String
            If rboCheckPoint.Checked Then
                sCheckPointIDs = ""
                For j = 0 To lstCheckPoint.Items.Count - 1
                    If lstCheckPoint.Items(j).Selected = True Then
                        sCheckPointIDs = sCheckPointIDs & "," & lstCheckPoint.Items(j).Value
                        If objclsDRLLog.CheckCheckPointDRL(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, lstCheckPoint.Items(j).Value, iDocumentRequestedList) > 0 Then
                            iDRLPKID = objclsDRLLog.GetDRLPKID(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, ddlAuditNo.SelectedValue, lstCheckPoint.Items(j).Value, iDocumentRequestedList, 0)
                            If iDRLPKID = 0 Then
                                objDRLLog.iADRL_ID = 0
                                objDRLLog.sADRL_Comments = objclsGRACeGeneral.SafeSQL(txtComment.Text.Trim)
                            Else
                                objDRLLog.iADRL_ID = iDRLPKID
                                objDRLLog.sADRL_Comments = objclsDRLLog.GetComment(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iDRLPKID)
                            End If

                            objDRLLog.iADRL_FunID = lstCheckPoint.Items(j).Value
                            Array = objclsDRLLog.SaveDRLLogReceivedList_Details(sSession.AccessCode, objDRLLog)
                            objclsDRLLog.UpdateDRLLogDetails(sSession.AccessCode, sSession.AccessCodeID, iDRLPKID, iFinancialYearID, iAttachID, txtRequestedOn.Text, ddlStatus.SelectedValue, objclsGRACeGeneral.SafeSQL(txtComment.Text.Trim), sSession.UserID)
                            If Array(0) = 2 Then
                                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Audit", "DRL Log", "Updated", ddlAuditNo.SelectedValue, ddlAuditNo.SelectedItem.Text, 0, "", sSession.IPAddress)
                                lblError.Text = "Successfully Updated." : lblDRLLogDetailsValidationMsg.Text = "Successfully Updated."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                            End If
                        End If
                    End If
                Next
                objclsDRLLog.updateStandardAudit_Update_RemarksHistory(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, objDRLLog.iADRL_FunID, txtComment.Text, sSession.UserID, sSession.IPAddress, objDRLLog.sADRL_EmailID, objDRLLog.sADRL_TimlinetoResOn, ddlFinancialYear.SelectedValue)
                sCheckPointIDs = sCheckPointIDs.TrimStart(","c).TrimEnd(","c)
            ElseIf rboOthers.Checked Then
                sCheckPointIDs = "0"
                If objclsDRLLog.CheckCheckPointDRL(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, 0, iDocumentRequestedList) > 0 Then
                    iDRLPKID = objclsDRLLog.GetDRLPKID(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, ddlAuditNo.SelectedValue, 0, iDocumentRequestedList, 0)
                    If iDRLPKID = 0 Then
                        objDRLLog.iADRL_ID = 0
                        objDRLLog.sADRL_Comments = objclsGRACeGeneral.SafeSQL(txtComment.Text.Trim)
                    Else
                        objDRLLog.iADRL_ID = iDRLPKID
                        objDRLLog.sADRL_Comments = objclsDRLLog.GetComment(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iDRLPKID)
                    End If

                    objDRLLog.iADRL_FunID = 0
                    Array = objclsDRLLog.SaveDRLLogReceivedList_Details(sSession.AccessCode, objDRLLog)
                    objclsDRLLog.UpdateDRLLogDetails(sSession.AccessCode, sSession.AccessCodeID, iDRLPKID, iFinancialYearID, iAttachID, txtRequestedOn.Text, ddlStatus.SelectedValue, objclsGRACeGeneral.SafeSQL(txtComment.Text.Trim), sSession.UserID)
                    If Array(0) = 2 Then
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Audit", "DRL Log", "Updated", ddlAuditNo.SelectedValue, ddlAuditNo.SelectedItem.Text, 0, "", sSession.IPAddress)
                        lblError.Text = "Successfully Updated." : lblDRLLogDetailsValidationMsg.Text = "Successfully Updated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                    End If
                End If
            End If
            BindDRLLogDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, sCheckPointIDs)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDRLLog_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDRLLog.RowDataBound
        Dim imgbtnAttachment As New ImageButton
        Dim lblAttachID As New Label, lblCheckPoint As New Label, lblCheckPointID As New Label
        Dim lnkCheckPoint As New LinkButton
        Dim lblBadgeCountgv As New Label
        Dim lbldrid As New Label
        Dim ds As New DataTable
        Dim lblAtchDocID1 As Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnAttachment = CType(e.Row.FindControl("imgbtnAttachment"), ImageButton)
                lblCheckPointID = CType(e.Row.FindControl("lblCheckPointID"), Label)
                lblAttachID = CType(e.Row.FindControl("lblAttachID"), Label)
                lblAtchDocID1 = CType(e.Row.FindControl("lblAtchDocID"), Label)
                lblCheckPoint = CType(e.Row.FindControl("lblCheckPoint"), Label)
                lnkCheckPoint = CType(e.Row.FindControl("lnkCheckPoint"), LinkButton)
                lblBadgeCountgv = CType(e.Row.FindControl("lblBadgeCountgv"), Label)
                lblBadgeCountgv.Text = 0
                If Val(lblAttachID.Text) > 0 Then
                    Dim iCount As Integer = objclsAttachments.GetAttachmentCount(sSession.AccessCode, sSession.AccessCodeID, lblAttachID.Text)
                    lblBadgeCountgv.Text = iCount
                Else
                    Dim iRowAttachID As Integer = objclsStandardAudit.GetDRLAttachmentID(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text))
                    Dim iCount As Integer = objclsAttachments.GetAttachmentCount(sSession.AccessCode, sSession.AccessCodeID, iRowAttachID)
                    lblBadgeCount.Text = iCount
                End If
                If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                    imgbtnAttachment.ImageUrl = "~/Images/Attachment16.png"
                    imgbtnAttachment.Visible = True
                    If lblAttachID.Text > 0 Then
                        lnkCheckPoint.Visible = False
                        lblCheckPoint.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDRLLog_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnAttachment_Click(sender As Object, e As EventArgs)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAttachment_Click")
        End Try
    End Sub
    Private Sub gvDRLLog_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDRLLog.RowCommand
        Dim lblDRLID As New Label, lblAttachID As New Label, lblCheckPointID As New Label, lblDocumentRequestedListID As New Label, lblDocID As New Label, lblStatus As New Label
        Dim lblBadgeCountgv As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                RFVStatus.InitialValue = "0" : RFVStatus.ErrorMessage = "Select Status."
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                lblDocumentRequestedListID = CType(clickedRow.FindControl("lblDocumentRequestedListID"), Label)
                If ddlAuditNo.SelectedIndex > 0 And ddlCustomerName.SelectedIndex > 0 Then
                    For k = 0 To lstCheckPoint.Items.Count - 1
                        If lstCheckPoint.Items(k).Value = Val(lblCheckPointID.Text) Then
                            lstCheckPoint.Items(k).Selected = True
                        Else
                            lstCheckPoint.Items(k).Selected = False
                        End If
                    Next
                    txtComment.Text = "" : txtRequestedOn.Text = ""
                    LoadDRLDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text), Val(lblDocumentRequestedListID.Text))
                    txtRequestedOn.Enabled = True : lblRequestedOn.Text = "* Received On" : lblHStatus.Visible = True : ddlStatus.Visible = True
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True : btnAddAttch.Visible = True : txtfile.Visible = True : lblBrowse.Visible = True : lblSize.Visible = True
                    iDocID = 0 : lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                End If
            ElseIf e.CommandName = "Attachment" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAttachID = CType(clickedRow.FindControl("lblAttachID"), Label) : iAttachID = Val(lblAttachID.Text)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                iSelectedCheckPointID = 0 : iSelectedCheckPointID = Val(lblCheckPointID.Text)
                lblDocumentRequestedListID = CType(clickedRow.FindControl("lblDocumentRequestedListID"), Label)
                If iAttachID = 0 Then
                    iAttachID = objclsStandardAudit.GetConductAuditAttachmentID(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iSelectedCheckPointID)
                End If
                If ddlCustomerName.SelectedIndex > 0 And ddlAuditNo.SelectedIndex > 0 And Val(lblCheckPointID.Text) > 0 Then
                    LoadDRLDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text), Val(lblDocumentRequestedListID.Text))
                    txtComment.Text = ""
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            ElseIf e.CommandName = "HistoryAR" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                Dim dt As New DataTable
                dt = objclsStandardAudit.LoadSelectedStandardAuditCheckPointRemarksHistoryDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text), 1)
                If dt.Rows.Count > 0 Then
                    gvHistory.DataSource = dt
                    gvHistory.DataBind()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myHistoryModal').modal('show')", True)
                Else
                    lblError.Text = "History not available." : lblDRLLogDetailsValidationMsg.Text = "History not available."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDRLLog_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgAttach.CurrentPageIndex = 0
            dgAttach.PageSize = 1000
            ds = objclsAttachments.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            If ds.Tables(0).Rows.Count > dgAttach.PageSize Then
                dgAttach.AllowPaging = True
            Else
                dgAttach.AllowPaging = False
            End If
            dgAttach.DataSource = ds
            dgAttach.DataBind()
            lblBadgeCount.Text = dgAttach.Items.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAttachments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub btnaddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblMsg.Text = "" : iDocID = 0
            If Not (txtfile.PostedFile Is Nothing) And txtfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                    Exit Sub
                End If
                lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                    If iAttachID > 0 Then
                        BindAllAttachments(sSession.AccessCode, iAttachID)
                    End If
                    objclsStandardAudit.SaveDRLConductAuditAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iAttachID, ddlAuditNo.SelectedValue, iSelectedCheckPointID, "DRL")
                Else
                    lblMsg.Text = "No file to Attach."
                End If
            Else
                lblMsg.Text = "No file to Attach."
            End If
            BindDRLLogDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, iSelectedCheckPointID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnaddAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgAttach_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAttach.ItemDataBound
        Dim lblStatus As New Label, lblExtention As New Label, imgbtnSampling As New ImageButton
        Dim imgbtnAdd As New ImageButton, imgbtnRemove As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnAdd = CType(e.Item.FindControl("imgbtnAdd"), ImageButton)
                imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                imgbtnRemove = CType(e.Item.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                imgbtnSampling = CType(e.Item.FindControl("imgbtnSampling"), ImageButton)
                imgbtnSampling.ImageUrl = "~/Images/ExcelSA.jpg"
                lblExtention = CType(e.Item.FindControl("lblExtention"), Label)
                imgbtnRemove.Visible = False : imgbtnSampling.Visible = False
                If bLoginUserIsPartner = True Then
                    imgbtnRemove.Visible = True
                End If
                If UCase(lblExtention.Text) = "XLS" Or UCase(lblExtention.Text) = "XLSX" Then
                    imgbtnSampling.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub dgAttach_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAttach.ItemCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                'DownloadMyFile(sDestFilePath)
                sDestFilePath = "https://" & sDestFilePath.Substring(48)
                iframeview.Src = sDestFilePath
            End If
            If e.CommandName = "REMOVE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(sSession.AccessCode, iAttachID)
            End If
            If e.CommandName = "ADDDESC" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                lblFDescription = e.Item.FindControl("lblFDescription")
                lblHeadingDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
                txtDescription.Text = lblFDescription.Text
                txtDescription.Focus()
            End If
            If e.CommandName = "SAMPLING" Then
                Dim oAttachID As Object, oAuditID As Object, oCheckPointID As Object, oDocID As Object
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                oAttachID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iAttachID))
                oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditNo.SelectedValue))
                oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iSelectedCheckPointID))
                oDocID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAtchDocID.Text)))
                Response.Redirect(String.Format("~/StandardAudit/SampleSelection.aspx?AuditID={0}&CheckPointID={1}&AttachID={2}&DocID={3}", oAuditID, oCheckPointID, oAttachID, oDocID), False)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Protected Sub btnAddDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDesc.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If txtDescription.Text.Trim.Length > 1000 Then
                lblMsg.Text = "Description exceeded maximum size(max 1000 characters)."
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                Exit Try
            End If
            objclsAttachments.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID, Replace(txtDescription.Text.Trim, "'", "`"))
            lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
            iDocID = 0
            BindAllAttachments(sSession.AccessCode, iAttachID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDRLLog_PreRender(sender As Object, e As EventArgs) Handles gvDRLLog.PreRender
        Dim dt As New DataTable
        Try
            If gvDRLLog.Rows.Count > 0 Then
                gvDRLLog.UseAccessibleHeader = True
                gvDRLLog.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDRLLog.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDRLLog_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub SendMail()
        Dim sBody As String
        Dim sSubject As String
        Try
            sSubject = "Intimation mail for sharing the Documents requested by the Auditor"

            sBody = "<!DOCTYPE html><html><head><style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style></head><body>"
            'sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>"
            'sBody += "<img width='142' src='data:image/png;base64,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' alt='image'>"
            'sBody += "</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'>Intimation mail</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'>Document Request</p>"
            'sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>&nbsp;</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>Greetings from TRACe PA. &nbsp;&nbsp;</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>This mail is an intimation for sharing the documents requested by the Auditor’s office.</p>"

            sBody += "<table style=‘border-collapse:collapse;border:none;’>"
            sBody += "<tbody>"
            sBody += "<tr>"
            sBody += "<td style=‘width: 550pt;border: 1pt solid windowtext;padding: 0cm 5.4pt;vertical-align: top;’>"
            'sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'><strong>&nbsp;</strong></p>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'><strong>Audit No. : " & ddlAuditNo.SelectedItem.Text & " and Date : " & txtRequestedOn.Text & "(Requested On)</strong></p>"
            'sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'><strong>&nbsp;</strong></p>"
            sBody += "</td>"
            sBody += "</tr>"
            sBody += "<tr>"
            sBody += "<td style=‘width: 550pt;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;’>"
            'sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>&nbsp;</strong></p>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>Checkpoints selected,</strong></p>"
            sBody += "<div style='margin-top:0cm;margin-right:0.5cm;margin-bottom:8.0pt;margin-left:0.5cm;line-height:107%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>"
            sBody += "<ul style=‘margin-bottom:0cm;list-style-type: disc;’>"
            For i = 0 To lstCheckPoint.Items.Count - 1
                If lstCheckPoint.Items(i).Selected = True Then
                    sBody += "<li style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:107%;font-size:15px;font-family:’Calibri’,’sans-serif’;'><strong>" & lstCheckPoint.Items(i).Text.ToString() & "</strong></li>"
                End If
            Next
            sBody += "</ul>"
            sBody += "</div>"
            'sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:.0001pt;margin-left:0cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'><strong>&nbsp;</strong></p>"
            sBody += "</td>"
            sBody += "</tr>"
            sBody += "<tr>"
            sBody += "<td style=‘width: 550pt;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;’>"
            'sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>&nbsp;</strong></p>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>" & txtComment.Text.Trim & "</strong></p>"
            'sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>&nbsp;</strong></p>"
            sBody += "</td>"
            sBody += "</tr>"
            sBody += "</tbody>"
            sBody += "</table>"

            'sBody += "<p style='margin:0cm;margin-bottom:.0001pt;font-size:16px;font-family:’Calibri’,’sans-serif’;color:black;'>&nbsp;</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>Please login to TRACe PA website using the link and credentials shared with you.</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>Home page of the application will show you the list of documents requested by the auditor. Upload all the requested documents using links provided.</p>"
            sBody += "<p style='margin:0cm;margin-bottom:.0001pt;font-size:16px;font-family:’Calibri’,’sans-serif’;color:black;'>&nbsp;</p>"
            sBody += "<p style='margin:0cm;margin-bottom:.0001pt;font-size:16px;font-family:’Calibri’,’sans-serif’;color:black;'>Thanks,</p>"
            sBody += "<p style='margin:0cm;margin-bottom:.0001pt;font-size:16px;font-family:’Calibri’,’sans-serif’;color:black;'>TRACe PA Team</p>"
            sBody += "</body></html>"

            Dim myMail As New System.Web.Mail.MailMessage()
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "Trace@mmcspl.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "Trjune@23")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
            myMail.From = "Trace@mmcspl.com"

            myMail.Bcc = txtEmailID.Text.Trim ' To email id
            myMail.Subject = sSubject
            myMail.BodyFormat = MailFormat.Html
            myMail.Body = sBody
            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com" + ":" + "465"
            System.Web.Mail.SmtpMail.Send(myMail)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnSendMail_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSendMail.Click
        Try
            If Not ValidateDropDownListSelectedIndex(ddlAuditNo, "Select Audit No.") Then ddlAuditNo.Focus() : Exit Try
            If Not ValidateGridViewNotEmpty(gvDRLLog, "No Check Point for selected Audit No.") Then Exit Try
            If Not ValidateGridViewCheckBox(gvDRLLog, "chkSelectCheckList", "Select checkpoint to send mail.") Then Exit Try
            SendAllMails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSendMail_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function ValidateGridViewNotEmpty(gridView As GridView, errorMessage As String) As Boolean
        If gridView.Rows.Count = 0 Then
            lblError.Text = errorMessage : lblDRLLogDetailsValidationMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateDropDownListSelectedIndex(dropDownList As DropDownList, errorMessage As String) As Boolean
        If dropDownList.SelectedIndex = 0 Then
            lblError.Text = errorMessage : lblDRLLogDetailsValidationMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateGridViewCheckBox(gridView As GridView, checkBoxID As String, errorMessage As String) As Boolean
        If Not gridView.Rows.Cast(Of GridViewRow)().Any(Function(row) DirectCast(row.FindControl(checkBoxID), CheckBox).Checked) Then
            lblError.Text = errorMessage : lblDRLLogDetailsValidationMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Protected Sub chkSelectAllCheckList_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblError.Text = "" : lblDRLLogDetailsValidationMsg.Text = ""
            Dim chkAll As CheckBox = CType(sender, CheckBox)
            For Each row As GridViewRow In gvDRLLog.Rows
                Dim chkSelectCheckList As CheckBox = TryCast(row.FindControl("chkSelectCheckList"), CheckBox)
                If chkSelectCheckList IsNot Nothing Then
                    chkSelectCheckList.Checked = chkAll.Checked
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllCheckList_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub SendAllMails()
        Dim sBody As String
        Dim sSubject As String
        Try
            sSubject = "Intimation mail for sharing the Documents requested by the Auditor"

            sBody = "<!DOCTYPE html><html><head><style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style></head><body>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'>Intimation mail</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'>Document Request</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>Greetings from TRACe PA. &nbsp;&nbsp;</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>This mail is an intimation for sharing the documents requested by the Auditor’s office.</p>"

            sBody += "<table style='border-collapse:collapse;border:none;'>"
            sBody += "<tbody>"
            sBody += "<tr>"
            sBody += "<td colspan='4' style='border: 1pt solid windowtext;padding: 0cm 5.4pt;vertical-align: top;'>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:center;'><strong>Audit No. : " & ddlAuditNo.SelectedItem.Text & "</strong></p>"
            sBody += "</td>"
            sBody += "</tr>"
            sBody += "<tr>"
            sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>Checkpoints</strong></p>"
            sBody += "</td>"
            sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>Document Requested List</strong></p>"
            sBody += "</td>"
            sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>Requested On</strong></p>"
            sBody += "</td>"
            sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
            sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'><strong>Comments</strong></p>"
            sBody += "</td>"
            sBody += "</tr>"
            Dim chkSelectCheckList As New CheckBox, lblCheckPoint As New Label
            Dim lblRequestedOn As New Label, lblremarks As New Label, lblDocumentRequestedList As New Label
            For i = 0 To gvDRLLog.Rows.Count - 1
                chkSelectCheckList = gvDRLLog.Rows(i).FindControl("chkSelectCheckList")
                lblCheckPoint = gvDRLLog.Rows(i).FindControl("lblCheckPoint")
                lblDocumentRequestedList = gvDRLLog.Rows(i).FindControl("lblDocumentRequestedList")
                lblRequestedOn = gvDRLLog.Rows(i).FindControl("lblRequestedOn")
                lblremarks = gvDRLLog.Rows(i).FindControl("lblremarks")
                If chkSelectCheckList.Checked = True Then
                    sBody += "<tr>"
                    sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
                    sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'>" & lblCheckPoint.Text.ToString() & "</p>"
                    sBody += "</td>"
                    sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
                    sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'>" & lblDocumentRequestedList.Text.ToString() & "</p>"
                    sBody += "</td>"
                    sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
                    sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'>" & lblRequestedOn.Text.ToString() & "</p>"
                    sBody += "</td>"
                    sBody += "<td style='border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0cm 5.4pt;vertical-align: top;'>"
                    sBody += "<p style='margin-top:0cm;margin-right:0.5cm;margin-bottom:.0001pt;margin-left:0.5cm;line-height:normal;font-size:15px;font-family:’Calibri’,’sans-serif’;text-align:left;'>" & lblremarks.Text.ToString() & "</p>"
                    sBody += "</td>"
                    sBody += "</tr>"
                End If
            Next
            sBody += "</tbody>"
            sBody += "</table>"
            sBody += "<br/>"
            sBody += "</table>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>Please login to TRACe PA website using the link and credentials shared with you.</p>"
            sBody += "<p style='margin-top:0cm;margin-right:0cm;margin-bottom:8.0pt;margin-left:0cm;line-height:100%;font-size:15px;font-family:’Calibri’,’sans-serif’;'>Home page of the application will show you the list of documents requested by the auditor. Upload all the requested documents using links provided.</p>"
            sBody += "<p style='margin:0cm;margin-bottom:.0001pt;font-size:16px;font-family:’Calibri’,’sans-serif’;color:black;'>&nbsp;</p>"
            sBody += "<p style='margin:0cm;margin-bottom:.0001pt;font-size:16px;font-family:’Calibri’,’sans-serif’;color:black;'>Thanks,</p>"
            sBody += "<p style='margin:0cm;margin-bottom:.0001pt;font-size:16px;font-family:’Calibri’,’sans-serif’;color:black;'>TRACe PA Team</p>"
            sBody += "</body></html>"

            Dim myMail As New System.Web.Mail.MailMessage()
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "Trace@mmcspl.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "Trjune@23")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
            myMail.From = "Trace@mmcspl.com"

            myMail.Bcc = txtEmailID.Text.Trim ' To email id
            myMail.Subject = sSubject
            myMail.BodyFormat = MailFormat.Html
            myMail.Body = sBody
            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com" + ":" + "465"
            System.Web.Mail.SmtpMail.Send(myMail)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnAddAttch_Command(sender As Object, e As CommandEventArgs) Handles btnAddAttch.Command

    End Sub
End Class