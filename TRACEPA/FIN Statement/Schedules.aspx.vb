Imports System
Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer

Partial Class Schedules
    Inherits System.Web.UI.Page
    Private sFormName As String = "Schedules"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    'Public objclsScheduleTemplate As New clsScheduleTemplate
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsGRACePermission As New clsGRACePermission
    ' Public objclsCAIQAuditUniverse As New clsCAIQAuditUniverse
    Private objclsScheduleTemplate As New clsScheduleTemplate

    Private Shared sSession As AllSession
    Private Shared iseletedvalue As Integer = 0
    Private Shared inext As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAddHeadng.ImageUrl = "~/Images/Add16.png"
        imgbtnAddSubHeadng.ImageUrl = "~/Images/Add16.png"
        imgbtnItems.ImageUrl = "~/Images/Add16.png"
        imgbtnEditHeadng.ImageUrl = "~/Images/Edit16.png"
        imgbtnEditSubHeadng.ImageUrl = "~/Images/Edit16.png"
        imgbtnEditItems.ImageUrl = "~/Images/Edit16.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnsaveSchedule.ImageUrl = "~/Images/Save24.png"
        imgbtnSubItems.ImageUrl = "~/Images/Add16.png"
        imgbtnEditSubItems.ImageUrl = "~/Images/Edit16.png"
        ImgbtnAddNew.ImageUrl = "~/Images/Add24.png"
        imgbtnGrpAlias.ImageUrl = "~/Images/Add24.png"

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")

            If IsPostBack = False Then
                BindCompanytype()
                'BindScheduleGrid()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindAcc_Head()
        Try
            If ddlscheduletype.SelectedValue = 4 Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "CAPITAL  AND LIABILITIES")
                ddlAccheadType.Items.Insert(2, "ASSETS")
                ddlAccheadType.SelectedIndex = 0
            ElseIf ddlscheduletype.SelectedValue = 3 Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "INCOME")
                ddlAccheadType.Items.Insert(2, "EXPENSES")
                ddlAccheadType.SelectedIndex = 0
            ElseIf ddlscheduletype.SelectedValue = 4 Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "CAPITAL & LIABILITIES")
                ddlAccheadType.Items.Insert(2, "ASSETS")
                ddlAccheadType.SelectedIndex = 0
            ElseIf ddlscheduletype.SelectedValue = 3 Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "REVENUE")
                ddlAccheadType.Items.Insert(2, "EXPENSES")
                ddlAccheadType.SelectedIndex = 0
            ElseIf ddlscheduletype.SelectedValue = 4 Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "EQUITY AND LIABILITIES")
                ddlAccheadType.Items.Insert(2, "Assets")
                ddlAccheadType.SelectedIndex = 0
            ElseIf ddlscheduletype.SelectedValue = 3 Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "Revenue")
                ddlAccheadType.Items.Insert(2, "Expenses")
                ddlAccheadType.SelectedIndex = 0
            ElseIf ddlscheduletype.SelectedValue = 4 And ddlComptype.SelectedItem.Text = "New Private Ltd" Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "EQUITY AND LIABILITIES")
                ddlAccheadType.Items.Insert(2, "Assets")
                ddlAccheadType.SelectedIndex = 0
            ElseIf ddlscheduletype.SelectedValue = 3 Then
                ddlAccheadType.Items.Clear()
                ddlAccheadType.Items.Insert(0, "Select Account Head")
                ddlAccheadType.Items.Insert(1, "Revenue")
                ddlAccheadType.Items.Insert(2, "Expenses")
                ddlAccheadType.SelectedIndex = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Function BindCompanytype()
        Try
            ddlComptype.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "ORG")
            ddlComptype.DataTextField = "cmm_Desc"
            ddlComptype.DataValueField = "cmm_ID"
            ddlComptype.DataBind()
            ddlComptype.Items.Insert(0, "Select Organization Type")
        Catch ex As Exception

        End Try
    End Function
    Public Function BindHeading() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlComptype.SelectedIndex = 0 Or ddlscheduletype.SelectedIndex = 0 Then
                Exit Function
            Else
                dt = objclsScheduleTemplate.LoadHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                ddlHeading.DataSource = dt
                ddlHeading.DataTextField = "HeadingName"
                ddlHeading.DataValueField = "HeadingId"
                ddlHeading.DataBind()
                ddlHeading.Items.Insert(0, "Select ")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSubHeading() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            txtNotes.Visible = True
            If ddlComptype.SelectedIndex = 0 Or ddlscheduletype.SelectedIndex = 0 Then
                Exit Function
            Else
                dt = objclsScheduleTemplate.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                ddlsubheading.DataSource = dt
                ddlsubheading.DataTextField = "SubheadingName"
                ddlsubheading.DataValueField = "SubheadingID"
                ddlsubheading.DataBind()
                ddlsubheading.Items.Insert(0, "Select ")
            End If
            If dt.Rows.Count > 0 Then
                txtNotes.Text = dt.Rows(0)("Notes").ToString
            Else
                txtNotes.Text = ""
            End If
            'txtNotes.Text = dt.Rows(0)("Notes").ToString
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindItems() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlComptype.SelectedIndex = 0 Or ddlscheduletype.SelectedIndex = 0 Then
                Exit Function
            Else
                dt = objclsScheduleTemplate.LoadItems(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                ddlItems.DataSource = dt
                ddlItems.DataTextField = "Itemsname"
                ddlItems.DataValueField = "Itemsid"
                ddlItems.DataBind()
                ddlItems.Items.Insert(0, "Select ")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSUbItems() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlComptype.SelectedIndex = 0 Or ddlscheduletype.SelectedIndex = 0 Then
                Exit Function
            Else
                dt = objclsScheduleTemplate.LoadSubItems(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                ddlSUbItems.DataSource = dt
                ddlSUbItems.DataTextField = "Subitemsname"
                ddlSUbItems.DataValueField = "SubitemsiD"
                ddlSUbItems.DataBind()
                ddlSUbItems.Items.Insert(0, "Select")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindScheduleGrid() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objclsScheduleTemplate.LoadSchedulegrid(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
            If dt.Rows.Count > 0 Then
                GvScheduleTemplate.DataSource = dt
                GvScheduleTemplate.DataBind()
                Dim dtMaster As DataTable
                dtMaster = objclsScheduleTemplate.LoadScheduleMaster(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                ddlComptype.SelectedValue = dtMaster.Rows(0)("AST_Companytype")
                'If dtMaster.Rows(0)("AST_Company_limit") = 3 Then
                '    chklmtcomp.Checked = True
                '    chkNonlmtcomp.Checked = True
                'ElseIf dtMaster.Rows(0)("AST_Company_limit") = 2 Then
                '    chklmtcomp.Checked = True
                'ElseIf dtMaster.Rows(0)("AST_Company_limit") = 1 Then
                '    chkNonlmtcomp.Checked = True
                'Else
                '    chklmtcomp.Checked = False
                '    chkNonlmtcomp.Checked = False
                'End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub imgbtnAddHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddHeadng.Click
        Try
            lblid.Text = 0
            If ddlComptype.SelectedIndex = 0 Then
                lblError.Text = "Select Organization Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
            End If
            If ddlAccheadType.SelectedIndex = 0 Then
                lblError.Text = "Select Account Head"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
            End If
            txtname.Text = ""
            txtNotes.Visible = False
            lblNotes.Visible = False
            lblheadingtext.Text = "Heading"
            lblname.Text = "Heading name"
            lblModelError.Text = ""
            iseletedvalue = 1
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnAddSubHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddSubHeadng.Click
        Try
            lblid.Text = 0
            If ddlComptype.SelectedIndex = 0 Then
                lblError.Text = "Select Organization Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
            End If
            txtname.Text = ""
            txtNotes.Text = ""
            txtNotes.Visible = True
            lblNotes.Visible = True
            lblheadingtext.Text = "Sub Heading"
            lblname.Text = "Sub Heading name"
            lblModelError.Text = ""
            iseletedvalue = 2
            If ddlHeading.SelectedIndex <> 0 Then
                lblHeading.Text = ddlHeading.SelectedItem.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnItems_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnItems.Click
        Try
            lblid.Text = 0
            If ddlComptype.SelectedIndex = 0 Then
                lblError.Text = "Select Organization Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
            End If
            txtname.Text = ""
            lblheadingtext.Text = "Items"
            lblname.Text = "Items name"
            iseletedvalue = 3
            lblModelError.Text = ""
            txtNotes.Visible = False
            lblNotes.Visible = False
            If ddlHeading.SelectedIndex <> 0 Then
                lblHeading.Text = ddlHeading.SelectedItem.Text
            End If
            If ddlsubheading.SelectedIndex <> 0 Then
                lblHeading.Text = lblHeading.Text + ddlsubheading.SelectedItem.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnSubItems_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSubItems.Click
        Try
            lblid.Text = 0
            If ddlComptype.SelectedIndex = 0 Then
                lblError.Text = "Select Organization Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
            End If
            txtname.Text = ""
            lblheadingtext.Text = "Sub Items"
            lblname.Text = "Sub Items name"
            iseletedvalue = 4
            lblModelError.Text = ""
            txtNotes.Visible = False
            lblNotes.Visible = False
            If ddlHeading.SelectedIndex <> 0 Then
                lblHeading.Text = ddlHeading.SelectedItem.Text + "/"
            End If
            If ddlsubheading.SelectedIndex <> 0 Then
                lblHeading.Text = lblHeading.Text + ddlsubheading.SelectedItem.Text + "/"
            End If
            If ddlItems.SelectedIndex <> 0 Then
                lblHeading.Text = lblHeading.Text + ddlItems.SelectedItem.Text + "/"
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnsaveSchedule_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnsaveSchedule.Click
        Dim Arr() As String
        Try
            'If ddlHeading.SelectedIndex = 0 And ddlsubheading.SelectedIndex = 0 And ddlItems.SelectedIndex = 0 And ddlsubheading.SelectedIndex = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
            '    lblExcelValidationMsg.Text = "No data"
            '    lblError.Text = "No data"
            '    Exit Sub
            'End If
            lblError.Text = ""
            If DdlScheduletype.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Please select Schedule type"
                lblError.Text = lblExcelValidationMsg.Text
                Exit Sub
            End If
            'If chklmtcomp.Checked = False And chkNonlmtcomp.Checked = False Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
            '    lblExcelValidationMsg.Text = "Please select Comapany is Limited Company or Non limited Company"
            '    lblError.Text = lblExcelValidationMsg.Text
            '    Exit Sub
            'End If
            If ddlComptype.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Please select Company type"
                lblError.Text = lblExcelValidationMsg.Text
                Exit Sub
                ddlComptype.Focus()
            End If
            If ddlAccheadType.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Please Account Head"
                lblError.Text = lblExcelValidationMsg.Text
                Exit Sub
                ddlAccheadType.Focus()
            End If
            If ddlHeading.SelectedIndex = 0 Then
                objclsScheduleTemplate.iAST_HeadingID = 0
            Else
                objclsScheduleTemplate.iAST_HeadingID = ddlHeading.SelectedValue
            End If
            If ddlsubheading.SelectedIndex = 0 Then
                objclsScheduleTemplate.iAST_SubHeadingID = 0
            Else
                objclsScheduleTemplate.iAST_SubHeadingID = ddlsubheading.SelectedValue
            End If
            If ddlItems.SelectedIndex = 0 Then
                objclsScheduleTemplate.iAST_ItemsID = 0
            Else
                objclsScheduleTemplate.iAST_ItemsID = ddlItems.SelectedValue
            End If
            If ddlSUbItems.SelectedIndex = 0 Then
                objclsScheduleTemplate.iAST_subItemsID = 0
            Else
                objclsScheduleTemplate.iAST_subItemsID = ddlSUbItems.SelectedValue
            End If
            If ddlAccheadType.SelectedIndex = 0 Then
                objclsScheduleTemplate.iAST_AccHeadId = 0
            Else
                objclsScheduleTemplate.iAST_AccHeadId = ddlAccheadType.SelectedIndex
            End If
            objclsScheduleTemplate.iAST_ID = 0
            objclsScheduleTemplate.sAST_Name = txtname.Text
            'If ddlHeading.SelectedIndex = 0 Then
            '    objclsScheduleTemplate.iAST_HeadingID = 0
            'Else
            '    objclsScheduleTemplate.iAST_HeadingID = ddlHeading.SelectedValue
            'End If
            'If ddlsubheading.SelectedIndex = 0 Then
            '    objclsScheduleTemplate.iAST_SubHeadingID = 0
            'Else
            '    objclsScheduleTemplate.iAST_SubHeadingID = ddlsubheading.SelectedValue
            'End If
            'If ddlItems.SelectedIndex = 0 Then
            '    objclsScheduleTemplate.iAST_ItemsID = 0
            'Else
            '    objclsScheduleTemplate.iAST_ItemsID = ddlItems.SelectedValue
            'End If
            objclsScheduleTemplate.sAST_DELFLG = "A"
            objclsScheduleTemplate.iAST_CRBY = sSession.UserID
            objclsScheduleTemplate.sAST_STATUS = "C"
            objclsScheduleTemplate.iAST_UPDATEDBY = 0
            objclsScheduleTemplate.sAST_IPAddress = sSession.IPAddress
            objclsScheduleTemplate.iAST_CompId = sSession.AccessCodeID
            objclsScheduleTemplate.iAST_YEARId = sSession.YearID
            objclsScheduleTemplate.iAST_Schedule_type = DdlScheduletype.SelectedValue
            'objclsScheduleTemplate.iAST_Companytype = ddlComptype.SelectedValue
            'If chklmtcomp.Checked = True And chkNonlmtcomp.Checked = True Then
            '    objclsScheduleTemplate.iAST_Companytype = 3
            'ElseIf chklmtcomp.Checked = True Then
            '    objclsScheduleTemplate.iAST_Companytype = 2
            'ElseIf chkNonlmtcomp.Checked = True Then
            '    objclsScheduleTemplate.iAST_Companytype = 1
            'End If
            objclsScheduleTemplate.iAST_Companytype = ddlComptype.SelectedValue
            Arr = objclsScheduleTemplate.SaveScheduleTemplate(sSession.AccessCode, sSession.AccessCodeID, objclsScheduleTemplate)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Saved"
            lblError.Text = lblExcelValidationMsg.Text
            BindScheduleGrid()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnSavedetails_Click(sender As Object, e As EventArgs) Handles btnSavedetails.Click
        Dim Arr() As String
        Dim checkName As Boolean = False
        Try
            lblError.Text = ""
            lblModelError.Text = ""
            'Heading
            If iseletedvalue = 1 Then
                If txtname.Text.Trim <> "" Then
                    If ddlAccheadType.SelectedIndex = 0 Then
                        lblError.Text = "Select Account Head"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                        lblExcelValidationMsg.Text = lblError.Text
                        Exit Sub
                    End If
                    checkName = objclsScheduleTemplate.CheckName_exist(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtname.Text, iseletedvalue, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                    If checkName = True And Val(lblid.Text) = 0 Then
                        lblExcelValidationMsg.Text = "Data Already exist"
                        lblModelError.Text = lblExcelValidationMsg.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If Val(lblid.Text) <> 0 Then
                        objclsScheduleTemplate.iASH_ID = lblid.Text
                    Else
                        objclsScheduleTemplate.iASH_ID = 0
                    End If
                    objclsScheduleTemplate.sASH_Name = txtname.Text
                    objclsScheduleTemplate.sASH_DELFLG = "A"
                    objclsScheduleTemplate.iASH_CRBY = sSession.UserID
                    objclsScheduleTemplate.sASH_STATUS = "C"
                    objclsScheduleTemplate.iASH_UPDATEDBY = 0
                    objclsScheduleTemplate.sASH_IPAddress = sSession.IPAddress
                    objclsScheduleTemplate.iASH_CompId = sSession.AccessCodeID
                    objclsScheduleTemplate.iASH_YEARId = sSession.YearID
                    objclsScheduleTemplate.iSch_Orgtype = ddlComptype.SelectedValue
                    objclsScheduleTemplate.iSch_scheduletype = ddlscheduletype.SelectedValue
                    objclsScheduleTemplate.iASH_Notes = ddlAccheadType.SelectedIndex
                    Arr = objclsScheduleTemplate.SaveScheduleHeadingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsScheduleTemplate)
                End If
                If Val(lblid.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                    lblExcelValidationMsg.Text = "Heading Succesfully Updated"
                    lblModelError.Text = lblExcelValidationMsg.Text
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                    lblExcelValidationMsg.Text = "Heading Succesfully Saved"
                    lblModelError.Text = lblExcelValidationMsg.Text
                End If
                BindHeading()
                'Sub Heading
            ElseIf iseletedvalue = 2 Then
                If txtname.Text.Trim <> "" Then
                    checkName = objclsScheduleTemplate.CheckName_exist(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtname.Text, iseletedvalue, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                    If checkName = True And Val(lblid.Text) = 0 Then
                        lblExcelValidationMsg.Text = "Data Already exist"
                        lblModelError.Text = lblExcelValidationMsg.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If Val(lblid.Text) <> 0 Then
                        objclsScheduleTemplate.iASSH_ID = lblid.Text
                    Else
                        objclsScheduleTemplate.iASSH_ID = 0
                    End If
                    objclsScheduleTemplate.sASSH_Name = txtname.Text
                    If ddlHeading.SelectedIndex = 0 Then
                        objclsScheduleTemplate.iASSH_HeadingID = 0
                    Else
                        objclsScheduleTemplate.iASSH_HeadingID = ddlHeading.SelectedValue
                    End If
                    objclsScheduleTemplate.sASSH_DELFLG = "A"
                    objclsScheduleTemplate.iASSH_CRBY = sSession.UserID
                    objclsScheduleTemplate.sASSH_STATUS = "C"
                    objclsScheduleTemplate.iASSH_UPDATEDBY = 0
                    objclsScheduleTemplate.sASSH_IPAddress = sSession.IPAddress
                    objclsScheduleTemplate.iASSH_CompId = sSession.AccessCodeID
                    objclsScheduleTemplate.iASSH_YEARId = sSession.YearID
                    objclsScheduleTemplate.iASSH_Notes = txtNotes.Text
                    objclsScheduleTemplate.iSch_Orgtype = ddlComptype.SelectedValue
                    objclsScheduleTemplate.iSch_scheduletype = ddlscheduletype.SelectedValue
                    Arr = objclsScheduleTemplate.SaveScheduleSubHeadingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsScheduleTemplate)
                End If
                If Val(lblid.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                    lblExcelValidationMsg.Text = "Sub Heading Succesfully Updated"
                    lblModelError.Text = lblExcelValidationMsg.Text
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                    lblExcelValidationMsg.Text = "Sub Heading Succesfully Saved"
                End If
                lblModelError.Text = lblExcelValidationMsg.Text
                BindSubHeading()
                'Items
            ElseIf iseletedvalue = 3 Then
                If txtname.Text.Trim <> "" Then
                    checkName = objclsScheduleTemplate.CheckName_exist(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtname.Text, iseletedvalue, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                    If checkName = True And Val(lblid.Text) = 0 Then
                        lblExcelValidationMsg.Text = "Data Already exist"
                        lblModelError.Text = lblExcelValidationMsg.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If Val(lblid.Text) <> 0 Then
                        objclsScheduleTemplate.iASI_ID = lblid.Text
                    Else
                        objclsScheduleTemplate.iASI_ID = 0
                    End If
                    objclsScheduleTemplate.sASI_Name = txtname.Text
                    If ddlHeading.SelectedIndex = 0 Then
                        objclsScheduleTemplate.iASI_HeadingID = 0
                    Else
                        objclsScheduleTemplate.iASI_HeadingID = ddlHeading.SelectedValue
                    End If
                    If ddlsubheading.SelectedIndex = 0 Then
                        objclsScheduleTemplate.iASI_SubHeadingID = 0
                    Else
                        objclsScheduleTemplate.iASI_SubHeadingID = ddlsubheading.SelectedValue
                    End If
                    objclsScheduleTemplate.sASI_DELFLG = "A"
                    objclsScheduleTemplate.iASI_CRBY = sSession.UserID
                    objclsScheduleTemplate.sASI_STATUS = "C"
                    objclsScheduleTemplate.iASI_UPDATEDBY = 0
                    objclsScheduleTemplate.sASI_IPAddress = sSession.IPAddress
                    objclsScheduleTemplate.iASI_CompId = sSession.AccessCodeID
                    objclsScheduleTemplate.iASI_YEARId = sSession.YearID
                    objclsScheduleTemplate.iSch_Orgtype = ddlComptype.SelectedValue
                    objclsScheduleTemplate.iSch_scheduletype = ddlscheduletype.SelectedValue
                    Arr = objclsScheduleTemplate.SaveScheduleItemDetails(sSession.AccessCode, sSession.AccessCodeID, objclsScheduleTemplate)
                    If Val(lblid.Text) <> 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                        lblExcelValidationMsg.Text = "Item Succesfully Updated"
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                        lblExcelValidationMsg.Text = "Item Succesfully Saved"
                    End If
                    lblModelError.Text = lblExcelValidationMsg.Text
                End If
                BindItems()
            ElseIf iseletedvalue = 4 Then
                If txtname.Text.Trim <> "" Then
                    checkName = objclsScheduleTemplate.CheckName_exist(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtname.Text, iseletedvalue, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                    If checkName = True And Val(lblid.Text) = 0 Then
                        lblExcelValidationMsg.Text = "Data Already exist"
                        lblModelError.Text = lblExcelValidationMsg.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If Val(lblid.Text) <> 0 Then
                        objclsScheduleTemplate.iASSI_ID = lblid.Text
                    Else
                        objclsScheduleTemplate.iASSI_ID = 0
                    End If
                    objclsScheduleTemplate.sASSI_Name = txtname.Text
                    If ddlHeading.SelectedIndex = 0 Then
                        objclsScheduleTemplate.iASSI_HeadingID = 0
                    Else
                        objclsScheduleTemplate.iASSI_HeadingID = ddlHeading.SelectedValue
                    End If
                    If ddlsubheading.SelectedIndex = 0 Then
                        objclsScheduleTemplate.iASSI_SubHeadingID = 0
                    Else
                        objclsScheduleTemplate.iASSI_SubHeadingID = ddlsubheading.SelectedValue
                    End If
                    If ddlItems.SelectedIndex = 0 Then
                        objclsScheduleTemplate.iASSI_ItemsID = 0
                    Else
                        objclsScheduleTemplate.iASSI_ItemsID = ddlItems.SelectedValue
                    End If
                    objclsScheduleTemplate.sASSI_DELFLG = "A"
                    objclsScheduleTemplate.iASSI_CRBY = sSession.UserID
                    objclsScheduleTemplate.sASSI_STATUS = "C"
                    objclsScheduleTemplate.iASSI_UPDATEDBY = 0
                    objclsScheduleTemplate.sASSI_IPAddress = sSession.IPAddress
                    objclsScheduleTemplate.iASSI_CompId = sSession.AccessCodeID
                    objclsScheduleTemplate.iASSI_YEARId = sSession.YearID
                    objclsScheduleTemplate.iAssi_Orgtype = ddlComptype.SelectedValue
                    objclsScheduleTemplate.iAssi_scheduletype = ddlscheduletype.SelectedValue
                    objclsScheduleTemplate.iSch_Orgtype = ddlComptype.SelectedValue
                    objclsScheduleTemplate.iSch_scheduletype = ddlscheduletype.SelectedValue
                    Arr = objclsScheduleTemplate.SaveScheduleSubItemDetails(sSession.AccessCode, sSession.AccessCodeID, objclsScheduleTemplate)
                End If
                If Val(lblid.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                    lblExcelValidationMsg.Text = "Sub Item Succesfully Updated"
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                    lblExcelValidationMsg.Text = "Sub Item Succesfully Saved"
                End If
                lblModelError.Text = lblExcelValidationMsg.Text
                BindSUbItems()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GvScheduleTemplate_PreRender(sender As Object, e As EventArgs) Handles GvScheduleTemplate.PreRender
        Dim dt As New DataTable
        Try
            If GvScheduleTemplate.Rows.Count > 0 Then
                GvScheduleTemplate.UseAccessibleHeader = True
                GvScheduleTemplate.HeaderRow.TableSection = TableRowSection.TableHeader
                GvScheduleTemplate.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvScheduleTemplate_PreRender")
        End Try
    End Sub
    Private Sub imgbtnEditHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditHeadng.Click
        Try
            lblError.Text = ""
            lblModelError.Text = ""
            txtname.Text = ""
            txtNotes.Visible = False
            lblNotes.Visible = False
            If ddlHeading.SelectedIndex > 0 Then
                txtname.Text = ddlHeading.SelectedItem.Text
                lblid.Text = ddlHeading.SelectedValue
                lblname.Text = "Heading name"
                iseletedvalue = 1
                BindGridView1Grid()
            Else
                lblExcelValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelError.Text = lblExcelValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnEditSubHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditSubHeadng.Click
        Dim dt As DataTable
        Try
            lblError.Text = ""
            txtname.Text = ""
            lblModelError.Text = ""
            lblheadingtext.Text = "Sub Heading"
            lblname.Text = "Sub Heading name"
            txtNotes.Visible = True
            lblNotes.Visible = True
            If ddlHeading.SelectedIndex <> 0 Then
                lblHeading.Text = ddlHeading.SelectedItem.Text
            End If
            dt = objclsScheduleTemplate.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
            If ddlsubheading.SelectedIndex > 0 Then
                txtname.Text = ddlsubheading.SelectedItem.Text
                lblid.Text = ddlsubheading.SelectedValue
                iseletedvalue = 2
                BindGridView1Grid()
                If dt.Rows.Count > 0 Then
                    txtNotes.Text = dt.Rows(0)("Notes").ToString
                Else
                    txtNotes.Text = ""
                End If
            Else
                lblExcelValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelError.Text = lblExcelValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnEditItems_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditItems.Click
        Try
            lblError.Text = ""
            lblModelError.Text = ""
            txtname.Text = ""
            lblheadingtext.Text = "Items"
            lblname.Text = "Items name"
            txtNotes.Visible = False
            lblNotes.Visible = False
            If ddlHeading.SelectedIndex <> 0 Then
                lblHeading.Text = ddlHeading.SelectedItem.Text
            End If
            If ddlsubheading.SelectedIndex <> 0 Then
                lblHeading.Text = lblHeading.Text + ddlsubheading.SelectedItem.Text
            End If
            If ddlItems.SelectedIndex > 0 Then
                txtname.Text = ddlItems.SelectedItem.Text
                lblid.Text = ddlItems.SelectedValue
                iseletedvalue = 3
                BindGridView1Grid()
            Else
                lblExcelValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelError.Text = lblExcelValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnEditSubItems_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditSubItems.Click
        Try
            lblError.Text = ""
            lblModelError.Text = ""
            txtname.Text = ""
            lblheadingtext.Text = "Sub Items"
            lblname.Text = "Sub Items name"
            txtNotes.Visible = False
            lblNotes.Visible = False
            If ddlHeading.SelectedIndex <> 0 Then
                lblHeading.Text = ddlHeading.SelectedItem.Text + "/"
            End If
            If ddlsubheading.SelectedIndex <> 0 Then
                lblHeading.Text = lblHeading.Text + ddlsubheading.SelectedItem.Text + "/"
            End If
            If ddlItems.SelectedIndex <> 0 Then
                lblHeading.Text = lblHeading.Text + ddlItems.SelectedItem.Text + "/"
            End If
            If ddlSUbItems.SelectedIndex > 0 Then
                txtname.Text = ddlSUbItems.SelectedItem.Text
                lblid.Text = ddlSUbItems.SelectedValue
                iseletedvalue = 4
                BindGridView1Grid()
            Else
                lblExcelValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelError.Text = lblExcelValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ImgbtnAddNew_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnAddNew.Click
        Try
            'objclsScheduleTemplate.Updateshcduletemplate(sSession.AccessCode, sSession.AccessCodeID)
            Response.Redirect(String.Format("Schedules.aspx?"), False)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlComptype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlComptype.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            BindAcc_Head()
            BindHeading()
            BindSubHeading()
            BindItems()
            BindSUbItems()
            ddlHeading.SelectedIndex = 0
            ddlsubheading.SelectedIndex = 0
            ddlItems.SelectedIndex = 0
            ddlSUbItems.SelectedIndex = 0
            If ddlscheduletype.SelectedIndex = 0 Then
                lblExcelValidationMsg.Text = "Select Schedule Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelError.Text = lblExcelValidationMsg.Text
                Exit Sub
            End If

            dt = objclsScheduleTemplate.LoadSchedulegrid(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
            If dt.Rows.Count > 0 Then
                GvScheduleTemplate.DataSource = dt
                GvScheduleTemplate.DataBind()
                Dim dtMaster As DataTable
                dtMaster = objclsScheduleTemplate.LoadScheduleMaster(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                ddlComptype.SelectedValue = dtMaster.Rows(0)("AST_Companytype")
                'If dtMaster.Rows(0)("AST_Company_limit") = 3 Then
                '    chklmtcomp.Checked = True
                '    chkNonlmtcomp.Checked = True
                'ElseIf dtMaster.Rows(0)("AST_Company_limit") = 2 Then
                '    chklmtcomp.Checked = True
                'ElseIf dtMaster.Rows(0)("AST_Company_limit") = 1 Then
                '    chkNonlmtcomp.Checked = True
                'Else
                '    chklmtcomp.Checked = False
                '    chkNonlmtcomp.Checked = False
                'End If
            Else
                GvScheduleTemplate.DataSource = Nothing
                GvScheduleTemplate.DataBind()
                'chklmtcomp.Checked = False
                'chkNonlmtcomp.Checked = False
                'ddlComptype.SelectedIndex = 0
            End If
            'GvScheduleTemplate.DataSource = dt
            'GvScheduleTemplate.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlscheduletype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlscheduletype.SelectedIndexChanged
        Try
            ddlComptype.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlAccheadType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccheadType.SelectedIndexChanged
        Try
            ddlHeading.SelectedIndex = 0 : ddlsubheading.SelectedIndex = 0
            ddlItems.SelectedIndex = 0 : ddlSUbItems.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Harsha 13-10-2023
    'Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
    '    Dim mimeType As String = Nothing
    '    Dim dt As New DataTable

    '    Try
    '        dt = objclsScheduleTemplate.LoadSchedulegrid(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)

    '        If (dt.Rows.Count = 0) Then
    '            lblError.Text = "No Data"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
    '            lblExcelValidationMsg.Text = lblError.Text
    '            Exit Sub
    '        End If
    '        ReportViewer1.Reset()
    '        Dim rds As New ReportDataSource("DataSet1", dt)
    '        ReportViewer1.LocalReport.DataSources.Add(rds)
    '        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/ScheduleRep.rdlc")

    '        Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
    '        Response.Buffer = True
    '        Response.Clear()
    '        Response.ContentType = mimeType
    '        Response.AddHeader("content-disposition", "attachment; filename=ScheduleRep" + ".pdf")
    '        Response.BinaryWrite(pdfViewer)
    '        Response.Flush()
    '        Response.End()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
    '    End Try
    'End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable

        Try
            dt = objclsScheduleTemplate.LoadSchedulegrid(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)

            If (dt.Rows.Count = 0) Then
                lblError.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/ScheduleRep.rdlc")
            Dim Shed_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Shed_Type", ddlscheduletype.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Shed_Type)
            Dim Comp_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Type", ddlComptype.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Comp_Type)
            Dim Acc_Head As ReportParameter() = New ReportParameter() {New ReportParameter("Acc_Head", ddlAccheadType.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Acc_Head)

            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=ScheduleRep" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub


    Private Sub imgbtnGrpAlias_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnGrpAlias.Click
        Dim Arr() As String
        Dim checkName As Boolean = False
        Try
            lblError.Text = ""
            lblModelError.Text = ""


            If txtgrpalias.Text.Trim <> "" Then
                checkName = objclsScheduleTemplate.CheckName_exist(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtgrpalias.Text.Trim, 5, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue)
                If checkName = True And Val(txtgrpalias.Text) = 0 Then
                    lblExcelValidationMsg.Text = "Data Already exist"
                    lblModelError.Text = lblExcelValidationMsg.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                    Exit Sub
                End If
                objclsScheduleTemplate.iAGA_ID = 0
                objclsScheduleTemplate.sAGA_Description = txtgrpalias.Text
                objclsScheduleTemplate.iAGA_GLID = lblid.Text
                objclsScheduleTemplate.sAGA_GLDESC = txtname.Text
                objclsScheduleTemplate.iAGA_GrpLevel = iseletedvalue
                objclsScheduleTemplate.iAGA_scheduletype = ddlscheduletype.SelectedValue
                objclsScheduleTemplate.iAGA_Orgtype = ddlComptype.SelectedValue
                objclsScheduleTemplate.iAGA_Compid = sSession.AccessCodeID
                objclsScheduleTemplate.sAGA_Status = "C"
                objclsScheduleTemplate.iAGA_Createdby = sSession.UserID
                objclsScheduleTemplate.sAGA_IPaddress = sSession.IPAddress
                Arr = objclsScheduleTemplate.SaveScheduleHeadingAliasDetails(sSession.AccessCode, sSession.AccessCodeID, objclsScheduleTemplate)
                txtgrpalias.Text = ""
            Else
                lblExcelValidationMsg.Text = "Enter Alias"
                lblModelError.Text = lblExcelValidationMsg.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                Exit Sub
            End If
            If Val(lblid.Text) <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Successfully alias Added."
                lblModelError.Text = lblExcelValidationMsg.Text
            Else
            End If
        Catch ex As Exception
            Throw
        End Try


    End Sub

    Private Sub gvAlias_PreRender(sender As Object, e As EventArgs) Handles gvAlias.PreRender
        Dim dt As New DataTable
        Try
            If gvAlias.Rows.Count > 0 Then
                gvAlias.UseAccessibleHeader = True
                gvAlias.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAlias.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAlias_PreRender")
        End Try
    End Sub
    Public Function BindGridView1Grid() As DataTable
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = objclsScheduleTemplate.LoadGridView1grid(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlComptype.SelectedValue, lblid.Text, iseletedvalue)
            If dt.Rows.Count > 0 Then
                gvAlias.DataSource = dt
                gvAlias.DataBind()
            Else
                gvAlias.DataSource = Nothing
                gvAlias.DataBind()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub gvAlias_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAlias.RowDataBound
        Dim imgbtnDelete As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnDelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtnDelete.ImageUrl = "~/Images/4delete.gif"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCustomers_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub gvAlias_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAlias.RowCommand
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblEmpID As Label = DirectCast(clickedRow.FindControl("lblheadingID"), Label)

            If e.CommandName.Equals("Deleterow") Then
                objclsScheduleTemplate.DeleteAlias(sSession.AccessCode, sSession.AccessCodeID, lblEmpID.Text)
            End If
            If Val(lblid.Text) <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Alias Succesfully Deleted"
                lblModelError.Text = lblExcelValidationMsg.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeDetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class


