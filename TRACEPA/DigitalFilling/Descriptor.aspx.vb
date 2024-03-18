Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports System.Diagnostics
Imports System.ComponentModel
Partial Class Descriptor
    Inherits System.Web.UI.Page
    Private sFormName As String = "Master Descriptor"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsEDICTGeneral As New clsEDICTGeneral
    Private objclsDescriptor As New clsDescriptor
    Private objclsPermission As New clsAccessRights
    Private Shared sSession As AllSession
    Private Shared iDescID As Integer = 0
    'Private Shared sDESSave As String
    'Private Shared sDESAD As String
    Private Shared dtDescrip As DataTable
    Private objclsGeneralFunctions As New clsGeneralFunctions
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
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnAdd.Visible = False : btnDescSave.Visible = False : btnDescUpdate.Visible = False : imgbtnReport.Visible = False
                'imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False

                'sDESSave = "NO" : sDESAD = "NO"
                'sFormButtons = objclsPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MDESCP")
                'If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,," Then
                '    Response.Redirect("~/Permission/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else

                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                '        sDESSave = "YES"
                '        imgbtnAdd.Visible = True : btnDescSave.Visible = True : btnDescUpdate.Visible = False
                '    End If
                '    If sFormButtons.Contains(",ActiveOrDeactive,") = True Then
                '        sDESAD = "YES"
                '        imgbtnActivate.Visible = True : imgbtnDeActivate.Visible = True : imgbtnWaiting.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Report") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                '    If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '        sDESSave = "YES" : sDESAD = "YES"
                '        imgbtnAdd.Visible = True : btnDescSave.Visible = True
                '    End If
                'End If


                BindDescType() : BindStatus()
                ddlStatus_SelectedIndexChanged(sender, e)

                RFVDescName.ControlToValidate = "txtDescName" : RFVDescName.ErrorMessage = "Enter Descriptor Name."
                REVDescName.ErrorMessage = "Descriptor Name exceeded maximum size(max 100 characters)." : REVDescName.ValidationExpression = "^[\s\S]{0,100}$"
                RFVDescNote.ControlToValidate = "txtDescNote" : RFVDescNote.ErrorMessage = "Enter Descriptor Note."
                REVDescNote.ErrorMessage = "Descriptor Note exceeded maximum size(max 200 characters)." : REVDescNote.ValidationExpression = "^[\s\S]{0,200}$"
                RFVDescDataType.InitialValue = "Select Data Type" : RFVDescDataType.ErrorMessage = "Select Data Type."
                RFVDescSize.ControlToValidate = "txtDescSize" : RFVDescSize.ErrorMessage = "Enter Descriptor Size."
                REVDescSize.ErrorMessage = "Only Integer." : REVDescSize.ValidationExpression = "^[0-9]{0,3}$"
                REVDescValue.ErrorMessage = "Descriptor Values exceeded maximum size(max 250 characters)." : REVDescValue.ValidationExpression = "^[\s\S]{0,250}$"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindDescType()
        Try
            ddlDescDataType.DataSource = objclsDescriptor.LoadDescDataType(sSession.AccessCode)
            ddlDescDataType.DataTextField = "DT_DataType"
            ddlDescDataType.DataValueField = "DT_ID"
            ddlDescDataType.DataBind()
            ddlDescDataType.Items.Insert(0, "Select Data Type")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDescType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadDescDashboard() As DataTable
        Dim dt As New DataTable
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False

            If ddlStatus.SelectedIndex = 0 Then
                'If sDESAD = "YES" Then
                imgbtnDeActivate.Visible = True 'Activate
                'End If

            ElseIf ddlStatus.SelectedIndex = 1 Then
                'If sDESAD = "YES" Then
                imgbtnActivate.Visible = True 'De-Activate
                'End If


            ElseIf ddlStatus.SelectedIndex = 2 Then
                'If sDESAD = "YES" Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
                'End If
            End If

            dt = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            dgDescDashBoard.DataSource = dt
            dgDescDashBoard.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDescDashboard" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub LoadDescDetails(ByVal iID As Integer)
        Dim dtDesc As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dtDesc = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, iID, ddlStatus.SelectedIndex)
            iDescID = dtDesc.Rows(0)("DescID")

            If IsDBNull(dtDesc.Rows(0)("Name")) = False Then
                txtDescName.Text = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Name"))
            Else
                txtDescName.Text = ""
            End If

            If IsDBNull(dtDesc.Rows(0)("Note")) = False Then
                txtDescNote.Text = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Note"))
            Else
                txtDescNote.Text = ""
            End If

            If IsDBNull(dtDesc.Rows(0)("DataType")) = False Then
                If dtDesc.Rows(0)("DataType") = "Number" Then
                    ddlDescDataType.SelectedValue = 1
                ElseIf dtDesc.Rows(0)("DataType") = "Varchar" Then
                    ddlDescDataType.SelectedValue = 2
                ElseIf dtDesc.Rows(0)("DataType") = "Date" Then
                    ddlDescDataType.SelectedValue = 3
                ElseIf dtDesc.Rows(0)("DataType") = "All" Then
                    ddlDescDataType.SelectedValue = 4
                End If
            Else
                ddlDescDataType.SelectedIndex = 0
            End If

            If IsDBNull(dtDesc.Rows(0)("Size")) = False Then
                txtDescSize.Text = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Size"))
            Else
                txtDescSize.Text = ""
            End If

            If IsDBNull(dtDesc.Rows(0)("DescValue")) = False Then
                txtDescValue.Text = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("DescValue"))
            Else
                txtDescValue.Text = ""
            End If

            If IsDBNull(dtDesc.Rows(0)("Status")) = False Then
                sStatus = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Status"))
                If sStatus = "Activated" Then
                    btnDescSave.Visible = False : btnDescUpdate.Visible = True
                ElseIf sStatus = "De-Activated" Then
                    btnDescSave.Visible = False : btnDescUpdate.Visible = False
                ElseIf sStatus = "Waiting for Approval" Then
                    btnDescSave.Visible = True : btnDescUpdate.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDescDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = LoadDescDashboard()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No data to display."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlDescDataType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDescDataType.SelectedIndexChanged
        Try
            lblError.Text = ""
            txtDescSize.Enabled = True : txtDescSize.Text = ""
            If UCase(ddlDescDataType.SelectedItem.Text) = "NUMBER" Then
                txtDescSize.Text = 100 : txtDescSize.Enabled = False
            ElseIf UCase(ddlDescDataType.SelectedItem.Text) = "VARCHAR" Then
                txtDescSize.Text = 100 : txtDescSize.Enabled = False
            ElseIf UCase(ddlDescDataType.SelectedItem.Text) = "DATE" Then
                txtDescSize.Text = 8 : txtDescSize.Enabled = False
            ElseIf UCase(ddlDescDataType.SelectedItem.Text) = "ALL" Then
                txtDescSize.Text = "" : txtDescSize.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDescDataType_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDescrip)
        Try
            lblError.Text = ""
            If dgDescDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next

            If iCount = 0 Then
                lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to Activate','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                lblDescID = dgDescDashBoard.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Activated")
                End If
            Next
            lblError.Text = "Successfully Activated."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "Activated", lblDescID.Text, sSession.YearName, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDescrip)
        Try
            lblError.Text = ""
            If dgDescDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next

            If iCount = 0 Then
                lblError.Text = "Select Name to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to De-Activate','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                lblDescID = dgDescDashBoard.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "De-Activated")
                End If
            Next
            lblError.Text = "Successfully De-Activated."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "De-Activated", lblDescID.Text, sSession.YearName, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDescrip)
        Try
            lblError.Text = ""
            If dgDescDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next

            If iCount = 0 Then
                lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to Approve','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                lblDescID = dgDescDashBoard.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Created")
                End If
            Next
            lblError.Text = "Successfully Approved."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "Approved", lblDescID.Text, sSession.YearName, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblModelError.Text = ""
            lblError.Text = "" : btnDescSave.Visible = False : btnDescUpdate.Visible = False
            'If sDESSave = "YES" Then
            btnDescSave.Visible = True : btnDescUpdate.Visible = False
            'End If
            txtDescName.Text = "" : txtDescNote.Text = "" : ddlDescDataType.SelectedIndex = 0 : txtDescSize.Text = "" : txtDescValue.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            lblModelError.Text = "" : btnDescSave.Visible = False : btnDescUpdate.Visible = False
            'If sDESSave = "YES" Then
            btnDescSave.Visible = True : btnDescUpdate.Visible = False
            'End If
            txtDescName.Text = "" : txtDescNote.Text = "" : ddlDescDataType.SelectedIndex = 0 : txtDescSize.Text = "" : txtDescValue.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim objDescriptor As New strDesc_Details
        Dim Arr() As String
        Try
            lblModelError.Text = ""
            If objclsDescriptor.CheckAvailabilityDescName(sSession.AccessCode, sSession.AccessCodeID, objclsEDICTGeneral.SafeSQL(txtDescName.Text)) = True Then
                objDescriptor.iDescId = 0
                objDescriptor.sDescName = objclsEDICTGeneral.SafeSQL(txtDescName.Text)
                objDescriptor.sDescNote = objclsEDICTGeneral.SafeSQL(txtDescNote.Text)
                objDescriptor.sDescSize = objclsEDICTGeneral.SafeSQL(txtDescSize.Text)
                objDescriptor.iDescDType = ddlDescDataType.SelectedValue
                objDescriptor.sDescStatus = "C"
                objDescriptor.sDescFlag = "W"
                objDescriptor.iDescCrBy = sSession.UserID
                objDescriptor.iDescUpdatedBy = sSession.UserID
                objDescriptor.sDescDefaultValue = txtDescValue.Text
                objDescriptor.sDescIPAddress = sSession.IPAddress
                Arr = objclsDescriptor.SaveDescriptorDetails(sSession.AccessCode, sSession.AccessCodeID, objDescriptor)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "Updated", "0", sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved & Waiting for Approval."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "Saved", "0", sSession.YearName, 0, "", sSession.IPAddress)
                    ddlStatus.SelectedIndex = 2
                    ddlStatus_SelectedIndexChanged(sender, e)
                    txtDescName.Text = "" : txtDescNote.Text = "" : txtDescSize.Text = "" : txtDescValue.Text = "" : ddlDescDataType.SelectedIndex = 0
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                End If
            Else
                lblModelError.Text = "Descriptor Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtDescName.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click
        Dim objDescriptor As New strDesc_Details
        Dim Arr() As String
        Try
            lblModelError.Text = ""
            objDescriptor.iDescId = iDescID
            objDescriptor.sDescName = objclsEDICTGeneral.SafeSQL(txtDescName.Text)
            objDescriptor.sDescNote = objclsEDICTGeneral.SafeSQL(txtDescNote.Text)
            objDescriptor.sDescSize = objclsEDICTGeneral.SafeSQL(txtDescSize.Text)
            objDescriptor.iDescDType = ddlDescDataType.SelectedValue
            objDescriptor.sDescStatus = "U"
            objDescriptor.sDescFlag = "A"
            objDescriptor.iDescCrBy = sSession.UserID
            objDescriptor.iDescUpdatedBy = sSession.UserID
            objDescriptor.sDescDefaultValue = txtDescValue.Text
            objDescriptor.iDescCompId = sSession.AccessCodeID
            objDescriptor.sDescIPAddress = sSession.IPAddress
            Arr = objclsDescriptor.SaveDescriptorDetails(sSession.AccessCode, sSession.AccessCodeID, objDescriptor)
            If Arr(0) = "2" Then
                lblModelError.Text = "Successfully Updated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "Updated", iDescID, sSession.YearName, 0, "", sSession.IPAddress)
                ddlStatus.SelectedIndex = 0
                ddlStatus_SelectedIndexChanged(sender, e)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescUpdate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDescDashBoard_PreRender(sender As Object, e As EventArgs) Handles dgDescDashBoard.PreRender
        Dim dt As New DataTable
        Try
            If dgDescDashBoard.Rows.Count > 0 Then
                dgDescDashBoard.UseAccessibleHeader = True
                dgDescDashBoard.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDescDashBoard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDescDashBoard_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDescDashBoard_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgDescDashBoard.RowCommand
        Dim lblDescID As New Label
        Dim oDescID As Object
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)

            If e.CommandName.Equals("EditRow") Then
                oDescID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblDescID.Text)))
                LoadDescDetails(Val(lblDescID.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "De-Activated")
                    lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "De-Activated", lblDescID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Activated")
                    lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "Activated", lblDescID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Waiting for Approval
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Created")
                    lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Descriptor", "Approved", lblDescID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                    ddlStatus.SelectedIndex = 0
                End If
            End If
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDescDashBoard_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDescDashBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDescDashBoard.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgDescDashBoard.Columns(0).Visible = False : dgDescDashBoard.Columns(8).Visible = False : dgDescDashBoard.Columns(9).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'If sDESAD = "YES" Then
                    dgDescDashBoard.Columns(0).Visible = True : dgDescDashBoard.Columns(8).Visible = True
                    'End If
                    'If sDESSave = "YES" Then
                    dgDescDashBoard.Columns(0).Visible = True : dgDescDashBoard.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'If sDESAD = "YES" Then
                    dgDescDashBoard.Columns(0).Visible = True : dgDescDashBoard.Columns(8).Visible = True
                    'End If
                    'If sDESSave = "YES" Then
                    dgDescDashBoard.Columns(0).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'If sDESAD = "YES" Then
                    dgDescDashBoard.Columns(0).Visible = True : dgDescDashBoard.Columns(8).Visible = True
                    'End If
                    'If sDESSave = "YES" Then
                    dgDescDashBoard.Columns(0).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgDescDashBoard.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDescDashBoard_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgDescDashBoard.Rows.Count - 1
                    chkField = dgDescDashBoard.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgDescDashBoard.Rows.Count - 1
                    chkField = dgDescDashBoard.Rows(iIndx).FindControl("chkSelect")
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
        Try
            ReportViewer1.Reset()
            dtDescrip = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            If dtDescrip.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dtDescrip)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/Descriptor.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Descriptor" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSelnkbtnExcel_Clickarch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            dtDescrip = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            If dtDescrip.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dtDescrip)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/Descriptor.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Descriptor" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
