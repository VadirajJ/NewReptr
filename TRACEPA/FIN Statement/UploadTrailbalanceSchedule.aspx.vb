Imports System
Imports System.Data
Imports System.IO
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports Microsoft.Office.Interop
Imports ExcelDataReader
Imports Microsoft.Reporting.WebForms
Imports Spire.Xls

Partial Class UploadTrailbalanceSchedule
    Inherits System.Web.UI.Page
    Private sFormName As String = "UploadTrailbalanceSchedule"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsUSEntry As New clsUploadStockEntry
    Dim objUT As New ClsUploadTailBal
    Private objCGLLink As New ClsCustomerGLLink
    Private objclsOpeningBalance As New clsOpeningBalance
    Private Shared sExcelSave As String
    Private Shared sFile As String = ""
    Private Shared TotalOpeningCredit As Decimal = 0
    Private Shared TotalOpeningDebit As Double = 0
    Private Shared TOtaltrCredit As Double = 0
    Private Shared TOtaltrDebit As Double = 0
    Private Shared TOtalClosingCredit As Double = 0
    Private Shared TOtalClosingDebit As Double = 0
    Private Shared Unmapped As Integer = 0
    Dim objTBVersion As New clsTrailBalanceVersion


    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSaveSchedTemp.ImageUrl = "~/Images/Save24.png"
        imgbtnBackSchedTemp.ImageUrl = "~/Images/Backward24.png"
        '  imgLinkageForYearSchedTemp.ImageUrl = "~/Images/Submit24.png"
        ImgbtnApproveSchedTemp.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgLinkageForYear.ImageUrl = "~/Images/Submit24.png"
        ImgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iFormID As Integer = 0
        Dim sFormButtons As String
        Dim dtSampleFormat As New DataTable
        Try
            dgGeneralSchedTemp.Enabled = True
            ddlSheetNameSchedTemp.Enabled = False
            dgGeneral.Enabled = True
            ddlSheetName.Enabled = False

            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadExistingCustomerSchedTemp() : BindYearMasterSchedTemp()
                LoadExistingCustomer() : BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    ddlFinancialYearSchedTemp.SelectedValue = ddlFinancialYear.SelectedValue
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                    ddlFinancialYearSchedTemp.SelectedValue = sSession.ScheduleYearId
                End If
                ImgbtnApproveSchedTemp.Visible = False
                If sSession.CustomerID <> 0 Then
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                    Else
                        ddlCustNameSchedTemp.SelectedValue = sSession.CustomerID
                    End If
                    'If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                    '    ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
                    'End If
                End If
                ddlUploadType.SelectedValue = 1
                ddlUploadType_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindYearMasterSchedTemp()
        Try
            ddlFinancialYearSchedTemp.DataSource = objUT.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYearSchedTemp.DataTextField = "YMS_ID"
            ddlFinancialYearSchedTemp.DataValueField = "YMS_YEARID"
            ddlFinancialYearSchedTemp.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMasterSchedTemp" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
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
    Public Sub LoadExistingCustomerSchedTemp()
        Try
            ddlCustNameSchedTemp.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustNameSchedTemp.DataTextField = "Cust_Name"
            ddlCustNameSchedTemp.DataValueField = "Cust_Id"
            ddlCustNameSchedTemp.DataBind()
            ddlCustNameSchedTemp.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub

    Private Sub ddlCustNameSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustNameSchedTemp.SelectedIndexChanged
        Dim dt As New DataTable
        Dim lblslno As New Label, lblSglDes As New Label, lblOBDebit As New Label, lblOBCredit As New Label, lblTrDebit As New Label, lblTrCredit As New Label, lblCBDebit As New Label, lblCBCredit As New Label
        Dim lblglTot As New Label, lblsgTt As New Label, lblGroupTot As New Label, lblHeadTot As New Label, lblGroup As New Label, lblHead As New Label
        Dim dtbranch As New DataTable
        Dim sOrgType As String = ""
        Try
            lblError.Text = ""
            lnkBtnFreezePrev.Visible = False
            imgbtnSaveSchedTemp.Enabled = True
            ImgbtnApproveSchedTemp.Enabled = True
            If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                If ddlFinancialYearSchedTemp.SelectedIndex > 0 Then
                    Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                    AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                    AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                    Dim AppCustomerID As Integer = ddlCustNameSchedTemp.SelectedValue
                    AppAccesscodeCookie.Value = AppCustomerID
                    AppAccesscodeCookie.Secure = True
                    AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                    Response.Cookies.Add(AppAccesscodeCookie)
                    sSession.CustomerID = ddlCustNameSchedTemp.SelectedValue
                    ddlCustName.SelectedValue = sSession.CustomerID
                    Session("AllSession") = sSession

                    DdlbranchSchedTemp.Visible = True
                    LblBranchNameSchedTemp.Visible = True
                    'lbllmtcomp.Visible = True
                    'lblNonlmtcomp.Visible = True
                    'lbllmtcomp.Visible = True
                    'lblNonlmtcomp.Visible = True
                    'chklmtcomp.Visible = True
                    'chkNonlmtcomp.Visible = True
                    sOrgType = objUT.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue)

                    If sOrgType = "Partnership firms" Then
                        btnPartner.Visible = True
                    Else
                        btnPartner.Visible = False

                    End If
                    If sOrgType = "Private Limited" Then
                        btnCashflow.Visible = True
                    Else
                        btnCashflow.Visible = False
                    End If
                    dtbranch = objUT.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue)
                    If dtbranch.Rows.Count > 0 Then
                        DdlbranchSchedTemp.DataSource = dtbranch
                        DdlbranchSchedTemp.DataTextField = "BranchName"
                        DdlbranchSchedTemp.DataValueField = "Branchid"
                        DdlbranchSchedTemp.DataBind()
                        DdlbranchSchedTemp.Items.Insert(0, "Select Branch Name")
                        dgGeneralSchedTemp.Visible = False
                        imgbtnSaveSchedTemp.Enabled = False
                        ImgbtnApproveSchedTemp.Enabled = False
                        If sSession.ScheduleBranchId <> 0 Then
                            DdlbranchSchedTemp.SelectedValue = sSession.ScheduleBranchId
                            DdlbranchSchedTemp_SelectedIndexChanged(sender, e)
                        Else
                            sSession.ScheduleBranchId = 0
                        End If
                        Session("AllSession") = sSession
                    ElseIf ddlUploadType.SelectedIndex > 0 Then
                        lblExcelValidationMsg.Text = "Customer should have atleast one Branch, Please add"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        lblError.Text = lblExcelValidationMsg.Text
                        DdlbranchSchedTemp.DataSource = dtbranch
                        DdlbranchSchedTemp.DataTextField = "BranchName"
                        DdlbranchSchedTemp.DataValueField = "Branchid"
                        DdlbranchSchedTemp.DataBind()
                        DdlbranchSchedTemp.Items.Insert(0, "Select Branch Name")
                        Exit Sub
                        dgGeneralSchedTemp.Visible = False
                        imgbtnSaveSchedTemp.Enabled = False
                        ImgbtnApproveSchedTemp.Enabled = False
                        Exit Sub
                    End If


                    'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
                    '    sSession.CustomerID = ddlCustName.SelectedValue
                    '    Session("AllSession") = sSession
                    '    ddlscheduletype.Visible = True
                    '    lblscheduletype.Visible = True
                    '    Ddlbranch.Visible = True
                    '    LblBranchName.Visible = True
                    '    'lbllmtcomp.Visible = True
                    '    'lblNonlmtcomp.Visible = True
                    '    'lbllmtcomp.Visible = True
                    '    'lblNonlmtcomp.Visible = True
                    '    'chklmtcomp.Visible = True
                    '    'chkNonlmtcomp.Visible = True
                    '    dtbranch = objUT.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue - 1)
                    '    If dtbranch.Rows.Count > 0 Then
                    '        Ddlbranch.DataSource = dtbranch
                    '        Ddlbranch.DataTextField = "BranchName"
                    '        Ddlbranch.DataValueField = "Branchid"
                    '        Ddlbranch.DataBind()
                    '        Ddlbranch.Items.Insert(0, "Select Branch Name")
                    '    Else
                    '        Ddlbranch.Visible = False
                    '        LblBranchName.Visible = False
                    '    End If
                    '    dt = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue - 1, 0, Unmapped)
                    '    If dt.Rows.Count > 0 Then
                    '        dgGeneral.Visible = True
                    '        dgGeneral.DataSource = dt
                    '        dgGeneral.DataBind()
                    '    Else
                    '        dgGeneral.DataSource = Nothing
                    '        dgGeneral.Visible = False
                    '        imgbtnSave.Enabled = False
                    '        ImgbtnApprove.Enabled = False
                    '        lblError.Text = "No Data Found. Upload Trail Balance For this Customer"
                    '    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub gvdddlItem_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlSubheading As New DropDownList
        Dim ddlheading As New DropDownList
        Dim ddlitems As New DropDownList
        Dim ddlSubitems As New DropDownList
        Dim lblitems As New Label
        Dim lblhead As New Label
        Dim lblheading As New Label
        Dim lblSubheading As New Label
        Dim i As Integer
        Dim lblslno As Label
        Dim dt As New DataTable, dtGroup As New DataTable
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Dim lblItemid As Integer
        Dim iitem As Integer = 0
        Dim isubheading As Integer = 0
        Dim iheading As Integer = 0
        Try
            lblError.Text = ""

            'ddlSubheading.Text = ""
            'ddlheading.Text = ""
            'ddlitems.Text = ""

            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = True
                Next
            Else
                Dim iitemid As Integer = 0
                Dim isheadingid As Integer = 0
                Dim iheadingid As Integer = 0
                Dim isubitemid As Integer = 0
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        ddlSubheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlSubheading")
                        ddlheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlheading")
                        ddlitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlitem")
                        ddlSubitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlsubitem")

                        If iitem = 0 Then
                            If ddlitems.SelectedIndex > 0 Then
                                iitemid = ddlitems.SelectedValue
                                iitem = iitemid
                            Else
                                iitemid = 0
                            End If
                        Else
                            iitemid = iitem
                            ddlitems.SelectedValue = iitem
                        End If

                        lblitems = dgGeneralSchedTemp.Rows(i).FindControl("lblgrdGl")
                        lblSubheading = dgGeneralSchedTemp.Rows(i).FindControl("lblSubgroup")
                        lblhead = dgGeneralSchedTemp.Rows(i).FindControl("lblHeading")
                        Dim dtTempatedetails As DataTable


                        dtTempatedetails = objUT.bindtemplate(sSession.AccessCode, sSession.AccessCodeID, isubitemid, iitemid, isheadingid, iheadingid, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)

                        If dtTempatedetails.Rows.Count <> 0 Then
                            If dtTempatedetails.Rows(0)("Ast_headingid") > 0 Then
                                ddlheading.SelectedValue = dtTempatedetails.Rows(0)("Ast_headingid")
                            End If
                            If dtTempatedetails.Rows(0)("ast_subheadingid") > 0 Then
                                ddlSubheading.SelectedValue = dtTempatedetails.Rows(0)("ast_subheadingid")
                            Else
                                ddlSubheading.SelectedIndex = 0
                            End If
                            If dtTempatedetails.Rows(0)("AST_ItemID") > 0 Then
                                ddlitems.SelectedValue = dtTempatedetails.Rows(0)("AST_ItemID")
                            Else
                                ddlitems.SelectedIndex = 0
                            End If
                            ddlSubitems.SelectedIndex = 0
                        End If
                        'dt = objUT.bindgroup(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, iSubgroup)

                        'If dt.Rows.Count > 0 Then
                        ''dtGroup = objUT.Getgroup(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, dt.Rows(0)("gl_parent"))
                        'lblheading.Text = dt.Rows(0)("gl_desc")

                        'If dt.Rows(0)("gl_accHead") = 1 Then
                        '        lblhead.Text = "Assets"
                        '    ElseIf dt.Rows(0)("gl_accHead") = 2 Then
                        '        lblhead.Text = "Income"
                        '    ElseIf dt.Rows(0)("gl_accHead") = 3 Then
                        '        lblhead.Text = "Expenditure"
                        '    ElseIf dt.Rows(0)("gl_accHead") = 4 Then
                        '        lblhead.Text = "Liabilities"
                        '    End If
                        'End If
                        'chkField.Checked = False
                        'Exit Sub
                    End If
                Next
                SaveTrailbalanceScheduleSchedTemp()
            End If
            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = False
                Next
            Else
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        chkField.Checked = False
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_SelectedIndexChanged")
        End Try
    End Sub

    Public Sub gvddlSubheading_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlSubheading As New DropDownList
        Dim ddlheading As New DropDownList
        Dim ddlitems As New DropDownList
        Dim ddlSubitems As New DropDownList
        Dim lblitems As New Label
        Dim lblhead As New Label
        Dim lblheading As New Label
        Dim lblSubheading As New Label
        Dim i As Integer
        Dim lblslno As Label
        Dim dt As New DataTable, dtGroup As New DataTable
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Dim isubheading As Integer = 0
        Dim iheading As Integer = 0
        Try
            lblError.Text = ""

            ddlSubheading.Text = ""
            ddlheading.Text = ""
            ddlitems.Text = ""

            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = True
                Next
            Else
                Dim iitemid As Integer = 0
                Dim isheadingid As Integer = 0
                Dim iheadingid As Integer = 0
                Dim isubitemid As Integer = 0
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        ddlSubheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlSubheading")
                        ddlheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlheading")
                        ddlitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlitem")
                        ddlSubitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlsubitem")
                        If isubheading = 0 Then
                            If ddlSubheading.SelectedIndex > 0 Then
                                isheadingid = ddlSubheading.SelectedValue
                                isubheading = isheadingid
                            Else
                                isheadingid = 0
                            End If
                        Else
                            isheadingid = isubheading
                            ddlSubheading.SelectedValue = isubheading
                        End If
                        Dim dtTempatedetails As DataTable
                        dtTempatedetails = objUT.bindtemplate(sSession.AccessCode, sSession.AccessCodeID, isubitemid, iitemid, isheadingid, iheadingid, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)

                        If dtTempatedetails.Rows.Count <> 0 Then
                            If dtTempatedetails.Rows(0)("Ast_headingid") > 0 Then
                                ddlheading.SelectedValue = dtTempatedetails.Rows(0)("Ast_headingid")
                            Else
                                ddlheading.SelectedIndex = 0
                            End If
                            If dtTempatedetails.Rows(0)("ast_subheadingid") > 0 Then
                                ddlSubheading.SelectedValue = dtTempatedetails.Rows(0)("ast_subheadingid")
                            Else
                                ddlSubheading.SelectedIndex = 0
                            End If
                            ddlSubitems.SelectedIndex = 0
                            ddlitems.SelectedIndex = 0
                        End If
                    End If
                Next
                SaveTrailbalanceScheduleSchedTemp()
            End If
            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = False
                Next
            Else
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        chkField.Checked = False
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_SelectedIndexChanged")
        End Try
    End Sub

    Protected Sub chkSelectSchedTemp_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim chkField2 As New CheckBox
            Dim scount As Integer = 0

            chkField2.Checked = True

            scount = dgGeneralSchedTemp.Rows.Count - 1

            chkField2 = dgGeneralSchedTemp.Rows(scount).FindControl("chkSelectSchedTemp")
            If chkField2.Checked = True Then

            Else
                chkField2.Checked = False
            End If

            'If grdddlGL.SelectedIndex > 0 Then
            '    chkSelect.Checked = False
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectSchedTemp_CheckedChanged")
        End Try
    End Sub

    Protected Sub ddlSheetNameSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetNameSchedTemp.SelectedIndexChanged
        Dim dttable As New DataTable
        Dim sStr As String = "", sYear As String
        Dim iYearID As Integer, iCheckMasterCounts As Integer = 0
        Dim ddlHeading As New DropDownList
        Dim ddlsubheading As New DropDownList
        Dim ddlItems As New DropDownList
        Dim ddlSubItems As New DropDownList
        'excel upload
        Dim Arr() As String
        Dim lbldescCode, lblOpeningDebit, lblOpeningCredit, lblTrDebit, lblTrCredit, lblClosingDebit, lblClosingCredit As New Label
        Dim lblheadingid, lblsubheadingid, lblitemid As New Label
        Dim lbldescirption As New LinkButton
        'Dim ddlHeading As New DropDownList
        'Dim ddlsubheading As New DropDownList
        'Dim ddlItems As New DropDownList
        'Dim ddlSubItems As New DropDownList
        Dim lblDescID, lblsubItemid As New Label
        Dim Masid As Integer = 0
        Dim lblDescdetails As New Label
        Dim checkdata As Integer = 0
        Dim Schdeuletype As Integer = 0
        Dim dTotalClosingCredit As Double = 0 : Dim dTotalClosingDebit As Double = 0
        Dim dClosCred, dCLosDeb As Decimal

        Try
            lblError.Text = ""
            'dgGeneral.DataSource = Nothing
            'dgGeneral.DataBind()
            dgGeneralSchedTemp.Visible = False

            If ddlSheetNameSchedTemp.SelectedIndex > 0 Then

                dttable = LoadTrialBalanceDataSchedTemp(sFile)
                'Vijayalakshmi 14-11-2019  commented because their is no master type for Upload SubGL trail balance
                'Else
                '    dttable = LoadSubTrialBalanceData(sFile)
                '    Session("SUBGL") = dttable
                If IsNothing(dttable) Then
                    ddlSheetNameSchedTemp.SelectedIndex = 0
                    lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf dttable.Rows.Count = 0 Then
                    lblError.Text = "No Data." : lblExcelValidationMsg.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'lbllmtcomp.Visible = True
                'lblNonlmtcomp.Visible = True
                'lbllmtcomp.Visible = True
                'lblNonlmtcomp.Visible = True
                'chklmtcomp.Visible = True
                'chkNonlmtcomp.Visible = True

                dgGeneralSchedTemp.DataSource = dttable
                dgGeneralSchedTemp.DataBind()
                dgGeneralSchedTemp.Visible = True

                ' 11/09/2023 for avoiding loop
                'For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                '    lblClosingDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingDebit")
                '    lblClosingCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingCredit")
                '    dTotalClosingCredit = dTotalClosingCredit + Val(lblClosingCredit.Text)
                '    dTotalClosingDebit = dTotalClosingDebit + Val(lblClosingDebit.Text)
                '    'If lblOpeningDebit.Text <> 0 Or lblTrDebit.Text <> 0 Then
                '    '    If lblOpeningDebit.Text + lblTrDebit.Text <> lblClosingDebit.Text Then
                '    '        lblError.Text = "Trial Balance is Not matching.Kindly Check line no  " & i + 1 : lblExcelValidationMsg.Text = "Trial Balance is Not matching.Kindly Check line no  " & i + 1
                '    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    '        Exit Sub
                '    '    End If
                '    'End If
                '    'If lblOpeningCredit.Text <> 0 Or lblTrCredit.Text <> 0 Then
                '    '    If lblOpeningCredit.Text + lblTrCredit.Text <> lblClosingCredit.Text Then
                '    '        lblError.Text = "Trial Balance is Not matching.Kindly Check line no  " & i + 1 : lblExcelValidationMsg.Text = "Trial Balance Is Not matching.Kindly Check line no  " & i + 1
                '    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    '        Exit Sub
                '    '    End If
                '    'End If

                'Next
                'dTotalClosingCredit = Double.Parse(dTotalClosingCredit)
                'dTotalClosingDebit = Double.Parse(dTotalClosingDebit)
                'If Val(dTotalClosingCredit) <> Val(dTotalClosingDebit) Then
                '    lblError.Text = "Closing credit Amount : " & dTotalClosingCredit & " and Closing debit amount : " & dTotalClosingDebit & " not matched,Kindly re upload matched Excel." : lblExcelValidationMsg.Text = "Closing credit Amount : " & dTotalClosingCredit & " and Closing debit amount : " & dTotalClosingDebit & " not matched,Kindly re upload matched Excel."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Sub
                'End If

                dCLosDeb = Convert.ToInt32(dttable.Compute("SUM(ClosingDebit)", String.Empty))
                dClosCred = Convert.ToInt32(dttable.Compute("SUM(ClosingCredit)", String.Empty))

                If Val(dClosCred) <> Val(dCLosDeb) Then
                    lblError.Text = "Closing credit Amount : " & dClosCred & " and Closing debit amount : " & dCLosDeb & " not matched. Kindly re-upload the Excel with Debit and Credit amount matched totals." : lblExcelValidationMsg.Text = "Closing credit Amount : " & dClosCred & " and Closing debit amount : " & dCLosDeb & " not matched,Kindly re-upload the Excel with Debit and Credit amount matched totals."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    ddlSheetNameSchedTemp.Items.Clear()
                    Exit Sub
                End If



                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    'ddlHeading = dgGeneral.Rows(i).FindControl("gvddlheading")
                    Dim Headingid As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                    'ddlsubheading = dgGeneral.Rows(i).FindControl("gvddlSubheading")
                    Dim subheading As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                    'ddlItems = dgGeneral.Rows(i).FindControl("gvddlitem")
                    Dim item As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                    'ddlSubItems = dgGeneral.Rows(i).FindControl("gvddlsubitem")
                    Dim Subitem As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblsubitem")


                    'ddlHeading.Items.Clear()
                    'ddlsubheading.Items.Clear()
                    'ddlItems.Items.Clear()
                    'ddlSubItems.Items.Clear()
                    ''ddlGrdGL.DataSource = objUT.BindGl(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlGrdSubGroup.SelectedValue)
                    'ddlHeading.DataSource = objUT.BindScheduleHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlCustName.SelectedValue)
                    'ddlHeading.DataTextField = "ASH_Name"
                    'ddlHeading.DataValueField = "ASH_ID"
                    'ddlHeading.DataBind()
                    'ddlHeading.Items.Insert(0, New ListItem("Select Heading"))


                    ''ddlGrdGL.SelectedValue = Val(Gl.Text)
                    'ddlsubheading.Enabled = True
                    'ddlsubheading.DataSource = objUT.BindScheduleSubHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlCustName.SelectedValue)
                    'ddlsubheading.DataTextField = "ASSH_Name"
                    'ddlsubheading.DataValueField = "ASSH_ID"
                    'ddlsubheading.DataBind()
                    'ddlsubheading.Items.Insert(0, New ListItem("Select Sub Heading"))

                    'ddlItems.Enabled = True
                    'ddlItems.DataSource = objUT.BindScheduleItemsHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlCustName.SelectedValue)
                    'ddlItems.DataTextField = "ASI_Name"
                    'ddlItems.DataValueField = "ASI_ID"
                    'ddlItems.DataBind()
                    'ddlItems.Items.Insert(0, New ListItem("Select Item Heading"))

                    'ddlSubItems.Enabled = True
                    'ddlSubItems.DataSource = objUT.BindScheduleSubItemsHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletype.SelectedValue, ddlCustName.SelectedValue)
                    'ddlSubItems.DataTextField = "ASSI_Name"
                    'ddlSubItems.DataValueField = "ASSI_ID"
                    'ddlSubItems.DataBind()
                    'ddlSubItems.Items.Insert(0, New ListItem("Select SubItem Heading"))
                    lbldescCode = dgGeneralSchedTemp.Rows(i).FindControl("lblDescriptionCode")
                    lblDescdetails = dgGeneralSchedTemp.Rows(i).FindControl("lblDescdetails")
                    lblDescID = dgGeneralSchedTemp.Rows(i).FindControl("lblDescID")
                    lbldescirption = dgGeneralSchedTemp.Rows(i).FindControl("lblDescription")
                    lblOpeningDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningDebit")
                    lblOpeningCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningCredit")
                    lblTrDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrDebit")
                    lblTrCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrCredit")
                    lblClosingDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingDebit")
                    lblClosingCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingCredit")
                    Headingid = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                    subheading = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                    item = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                    Subitem = dgGeneralSchedTemp.Rows(i).FindControl("lblSubitem")
                    If IsDBNull(dttable.Rows(i)("ScheduleType")) = True Then
                        Schdeuletype = 0
                    Else
                        Schdeuletype = Val(dttable.Rows(i)("ScheduleType"))
                    End If
                    'Schdeuletype = objUT.getScheduleType(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, lbldescirption.Text)
                    lblDescID.Text = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
                    lblDescdetails.Text = objUT.CheckDetaileddata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
                    If Val(lblDescID.Text) <> 0 Then
                        objUT.iATBU_ID = lblDescID.Text
                    Else
                        objUT.iATBU_ID = 0
                    End If
                    If lbldescCode.Text <> "" Then
                        objUT.sATBU_CODE = lbldescCode.Text
                    Else
                        objUT.sATBU_CODE = "SCh00" & (i + 1)
                    End If
                    If lblOpeningDebit.Text = "" Then
                        lblOpeningDebit.Text = 0
                    End If
                    If lblOpeningCredit.Text = "" Then
                        lblOpeningCredit.Text = 0
                    End If
                    If lblTrDebit.Text = "" Then
                        lblTrDebit.Text = 0
                    End If
                    If lblTrCredit.Text = "" Then
                        lblTrCredit.Text = 0
                    End If
                    If lblClosingDebit.Text = "" Then
                        lblClosingDebit.Text = 0
                    End If
                    If lblClosingCredit.Text = "" Then
                        lblClosingCredit.Text = 0
                    End If
                    objUT.sATBU_Description = lbldescirption.Text
                    objUT.iATBU_CustId = ddlCustNameSchedTemp.SelectedValue
                    objUT.iATBU_Branchname = DdlbranchSchedTemp.SelectedValue
                    objUT.dATBU_Opening_Debit_Amount = Double.Parse(lblOpeningDebit.Text)
                    objUT.dATBU_Opening_Credit_Amount = Double.Parse(lblOpeningCredit.Text)
                    objUT.dATBU_TR_Debit_Amount = Double.Parse(lblTrDebit.Text)
                    objUT.dATBU_TR_Credit_Amount = Double.Parse(lblTrCredit.Text)
                    objUT.dATBU_Closing_Debit_Amount = Double.Parse(lblClosingDebit.Text)
                    objUT.dATBU_Closing_Credit_Amount = Double.Parse(lblClosingCredit.Text)
                    objUT.sATBU_DELFLG = "A"
                    objUT.iATBU_CRBY = sSession.UserID
                    objUT.sATBU_STATUS = "C"
                    objUT.iATBU_UPDATEDBY = sSession.UserID
                    objUT.sATBU_IPAddress = sSession.IPAddress
                    objUT.iATBU_CompId = sSession.AccessCodeID
                    'If ddlFinancialYear.SelectedIndex = 0 Then
                    '    objUT.iATBU_YEARId = ddlFinancialYear.SelectedValue
                    'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
                    '    objUT.iATBU_YEARId = ddlFinancialYear.SelectedValue - 1
                    'End If
                    objUT.iATBU_YEARId = ddlFinancialYearSchedTemp.SelectedValue
                    Arr = objUT.SaveTrailBalanceExcelUpload(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)


                    If Val(lblDescdetails.Text) = 0 Then
                        '       objUT.iATBUD_ID = Val(lblDescID.Text)
                        objUT.iATBUD_ID = 0
                        objUT.iATBUD_Masid = Arr(1)
                    Else
                        objUT.iATBUD_ID = lblDescdetails.Text
                        objUT.iATBUD_Masid = Arr(1)
                    End If


                    If lbldescCode.Text <> "" Then
                        objUT.sATBUD_CODE = lbldescCode.Text
                    Else
                        objUT.sATBUD_CODE = "SCh00" & (i + 1)
                    End If

                    objUT.sATBUD_Description = lbldescirption.Text
                    objUT.iATBUD_CustId = ddlCustNameSchedTemp.SelectedValue
                    If Schdeuletype > 0 Then
                        objUT.iATBUD_SChedule_Type = Schdeuletype
                    Else
                        objUT.iATBUD_SChedule_Type = 0
                    End If
                    If DdlbranchSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_Branchname = DdlbranchSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_Branchname = 0
                    End If
                    objUT.iATBUD_Company_Type = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue)
                    If Val(Headingid.Text) > 0 Then
                        objUT.iATBUD_Headingid = Val(Headingid.Text)
                    Else
                        objUT.iATBUD_Headingid = 0
                    End If
                    If Val(subheading.Text) > 0 Then
                        objUT.iATBUD_Subheading = Val(subheading.Text)
                    Else
                        objUT.iATBUD_Subheading = 0
                    End If
                    If Val(item.Text) > 0 Then
                        objUT.iATBUD_itemid = Val(item.Text)
                    Else
                        objUT.iATBUD_itemid = 0
                    End If
                    If Val(Subitem.Text) > 0 Then
                        objUT.iATBUD_Subitemid = Val(Subitem.Text)
                    Else
                        objUT.iATBUD_Subitemid = 0
                    End If
                    objUT.sATBUD_DELFLG = "A"
                    objUT.iATBUD_CRBY = sSession.UserID
                    objUT.sATBUD_STATUS = "C"
                    objUT.sATBUD_Progress = "Uploaded"
                    objUT.iATBUD_UPDATEDBY = sSession.UserID
                    objUT.sATBUD_IPAddress = sSession.IPAddress
                    objUT.iATBUD_CompId = sSession.AccessCodeID
                    'If ddlFinancialYear.SelectedIndex = 0 Then
                    '    objUT.iATBUD_YEARId = ddlFinancialYear.SelectedValue
                    'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
                    '    objUT.iATBUD_YEARId = ddlFinancialYear.SelectedValue - 1
                    'End If
                    objUT.iATBUD_YEARId = ddlFinancialYearSchedTemp.SelectedValue
                    Arr = objUT.SaveTrailBalanceExcelUploaddetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)

                Next
                lblExcelValidationMsg.Text = "Successfully uploaded "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert success-warning');$('#ModalExcelValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
                TotalOpeningCredit = 0 : TotalOpeningDebit = 0 : TOtaltrCredit = 0 : TOtaltrDebit = 0 : TOtalClosingCredit = 0 : TOtalClosingDebit = 0
            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                ddlSheetNameSchedTemp.SelectedIndex = 0
                imgbtnSaveSchedTemp.Visible = False
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
                imgbtnSaveSchedTemp.Visible = False
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadTrialBalanceDataSchedTemp(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtDetails As New DataTable, dtdescDetails As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim dt As New DataTable
        Dim dtAccnts As New DataTable
        Dim orgTypeId As Integer = 0
        Dim dSubItemId As New Integer : Dim dItemId As Integer = 0
        Dim dSubHeadingId As Integer = 0 : Dim dHeadingId As Integer = 0
        Dim dtgroup As New DataTable
        Try
            dtTable.Columns.Add("SrNo")
            dtTable.Columns.Add("DescID")
            dtTable.Columns.Add("DescDetailsID")
            dtTable.Columns.Add("Description")
            dtTable.Columns.Add("DescriptionCode")
            dtTable.Columns.Add("OpeningDebit")
            dtTable.Columns.Add("OpeningCredit")
            dtTable.Columns.Add("TrDebit")
            dtTable.Columns.Add("TrCredit")
            dtTable.Columns.Add("TrDebittrUploaded")
            dtTable.Columns.Add("TrCredittrUploaded")
            dtTable.Columns.Add("ClosingDebit", System.Type.GetType("System.Double"))
            dtTable.Columns.Add("ClosingCredit", System.Type.GetType("System.Double"))
            dtTable.Columns.Add("Status")
            dtTable.Columns.Add("Subitemid")
            dtTable.Columns.Add("ASSI_Name")
            dtTable.Columns.Add("itemid")
            dtTable.Columns.Add("ASI_Name")
            dtTable.Columns.Add("ItemTotal")
            dtTable.Columns.Add("subheadingid")
            dtTable.Columns.Add("ASSH_name")
            dtTable.Columns.Add("subheadingTotal")
            dtTable.Columns.Add("headingid")
            dtTable.Columns.Add("ASH_Name")
            dtTable.Columns.Add("headingTotal")
            dtTable.Columns.Add("ScheduleType")
            dtDetails = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetNameSchedTemp.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtDetails) = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetNameSchedTemp.Items.Clear()
                Return dtDetails
            End If
            orgTypeId = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue)
            For i = 0 To dtDetails.Rows.Count - 1
                If Not IsDBNull(dtDetails.Rows(i).Item(0)) AndAlso dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then
                    dRow = dtTable.NewRow
                    dRow("SrNo") = i - 1
                    dRow("Description") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(0))
                    dRow("DescriptionCode") = objclsGRACeGeneral.SafeSQL("Sch00" & i)

                    For j = 1 To 6
                        If Not IsDBNull(dtDetails.Rows(i).Item(j)) AndAlso dtDetails.Rows(i).Item(j).ToString <> "&nbsp;" Then
                            Dim s As String = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(j)).Replace(",", "")
                            Select Case j
                                Case 1
                                    dRow("OpeningDebit") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                                Case 2
                                    dRow("OpeningCredit") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                                Case 3
                                    dRow("TrDebit") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                                    dRow("TrDebittrUploaded") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                                Case 4
                                    dRow("TrCredit") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                                    dRow("TrCredittrUploaded") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                                Case 5
                                    dRow("ClosingDebit") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                                Case 6
                                    dRow("ClosingCredit") = If(Double.TryParse(s, Nothing), CDbl(s), 0)
                            End Select
                        End If
                    Next j

                    If chkBxExcel.Checked = True Then
                        If String.IsNullOrEmpty(dtDetails.Rows(i).Item(7).ToString) = False Then
                            If dtDetails.Rows(i).Item(7).ToString <> "&nbsp;" Then
                                Dim s6 As String = dtDetails.Rows(i).Item(7)
                                dSubItemId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select isnull(ASSI_ID,0) as ASSI_ID  from ACC_ScheduleSubItems where Assi_Orgtype=" & orgTypeId & " and ASSI_Name='" & s6 & "' ")
                                If dSubItemId = 0 Then
                                    dtgroup = objUT.getGroupidfromAlias(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, 4, s6)
                                    If dtgroup.Rows.Count > 0 Then
                                        dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dtgroup(0)("ID"), dtgroup(0)("Level"))
                                    Else
                                        GoTo Item
                                    End If
                                Else
                                    dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dSubItemId, 4)
                                End If

                                If dtAccnts.Rows.Count > 0 Then
                                    dRow("Subitemid") = dtAccnts.Rows(0)("ASSI_ID")
                                    dRow("ASSI_Name") = dtAccnts.Rows(0)("ASSI_Name")
                                    dRow("itemid") = dtAccnts.Rows(0)("ASI_ID")
                                    dRow("ASI_Name") = dtAccnts.Rows(0)("ASI_Name")
                                    dRow("subheadingid") = dtAccnts.Rows(0)("ASSH_ID")
                                    dRow("ASSH_name") = dtAccnts.Rows(0)("ASSH_Name")
                                    dRow("headingid") = dtAccnts.Rows(0)("ASH_ID")
                                    dRow("ASH_Name") = dtAccnts.Rows(0)("ASH_Name")
                                    dRow("ScheduleType") = dtAccnts.Rows(0)("AST_Schedule_type")
                                End If
                            End If
                        ElseIf String.IsNullOrEmpty(dtDetails.Rows(i).Item(8).ToString) = False Then ' If Item Occurs
Item:
                            If dtDetails.Rows(i).Item(8).ToString <> "&nbsp;" Then
                                Dim s7 As String = dtDetails.Rows(i).Item(8)
                                dItemId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select isnull(ASI_ID,0) as ASI_ID  from ACC_ScheduleItems where Asi_Orgtype=" & orgTypeId & " and ASI_Name='" & s7 & "' ")
                                If dItemId = 0 Then
                                    dtgroup = objUT.getGroupidfromAlias(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, 3, s7)
                                    If dtgroup.Rows.Count > 0 Then
                                        dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dtgroup(0)("ID"), dtgroup(0)("Level"))
                                    Else
                                        GoTo SubHeading
                                    End If
                                Else
                                    dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dItemId, 3)
                                End If
                                If dtAccnts.Rows.Count > 0 Then
                                    dRow("Subitemid") = dtAccnts.Rows(0)("ASSI_ID")
                                    dRow("ASSI_Name") = dtAccnts.Rows(0)("ASSI_Name")
                                    dRow("itemid") = dtAccnts.Rows(0)("ASI_ID")
                                    dRow("ASI_Name") = dtAccnts.Rows(0)("ASI_Name")
                                    dRow("subheadingid") = dtAccnts.Rows(0)("ASSH_ID")
                                    dRow("ASSH_name") = dtAccnts.Rows(0)("ASSH_Name")
                                    dRow("headingid") = dtAccnts.Rows(0)("ASH_ID")
                                    dRow("ASH_Name") = dtAccnts.Rows(0)("ASH_Name")
                                    dRow("ScheduleType") = dtAccnts.Rows(0)("AST_Schedule_type")
                                End If
                            End If

                        ElseIf String.IsNullOrEmpty(dtDetails.Rows(i).Item(9).ToString) = False Then ' SubHeading Occurs
SubHeading:
                            If dtDetails.Rows(i).Item(9).ToString <> "&nbsp;" Then
                                Dim s8 As String = dtDetails.Rows(i).Item(9)
                                dSubHeadingId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select isnull(ASSH_ID,0) as ASSH_ID  from ACC_ScheduleSubHeading where Assh_Orgtype=" & orgTypeId & " and  ASSH_Name='" & s8 & "' ")
                                If dSubHeadingId = 0 Then
                                    dtgroup = objUT.getGroupidfromAlias(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, 2, s8)
                                    If dtgroup.Rows.Count = 0 Then
                                        GoTo Heading
                                    Else
                                        dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dtgroup(0)("ID"), dtgroup(0)("Level"))
                                    End If
                                Else
                                    dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dSubHeadingId, 2)
                                End If
                                If dtAccnts.Rows.Count > 0 Then
                                    dRow("Subitemid") = dtAccnts.Rows(0)("ASSI_ID")
                                    dRow("ASSI_Name") = dtAccnts.Rows(0)("ASSI_Name")
                                    dRow("itemid") = dtAccnts.Rows(0)("ASI_ID")
                                    dRow("ASI_Name") = dtAccnts.Rows(0)("ASI_Name")
                                    dRow("subheadingid") = dtAccnts.Rows(0)("ASSH_ID")
                                    dRow("ASSH_name") = dtAccnts.Rows(0)("ASSH_Name")
                                    dRow("headingid") = dtAccnts.Rows(0)("ASH_ID")
                                    dRow("ASH_Name") = dtAccnts.Rows(0)("ASH_Name")
                                    dRow("ScheduleType") = dtAccnts.Rows(0)("AST_Schedule_type")

                                End If
                            End If

                        ElseIf String.IsNullOrEmpty(dtDetails.Rows(i).Item(10).ToString) = False Then ' Heading Occurs
Heading:
                            Dim s10 As String = dtDetails.Rows(i).Item(10)
                            dHeadingId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select isnull(ASH_ID,0) as ASH_ID  from ACC_ScheduleHeading where Ash_Orgtype=" & orgTypeId & " and  ASH_Name='" & s10 & "' ")
                            If dHeadingId = 0 Then
                                dtgroup = objUT.getGroupidfromAlias(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, 1, s10)
                                If dtgroup.Rows.Count > 0 Then
                                    dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dtgroup(0)("ID"), dtgroup(0)("Level"))
                                Else
                                    GoTo Unmatch
                                End If
                            Else
                                dtAccnts = objUT.GetMappedLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, orgTypeId, dHeadingId, 1)
                            End If
                            If dtAccnts.Rows.Count > 0 Then
                                dRow("Subitemid") = dtAccnts.Rows(0)("ASSI_ID")
                                dRow("ASSI_Name") = dtAccnts.Rows(0)("ASSI_Name")
                                dRow("itemid") = dtAccnts.Rows(0)("ASI_ID")
                                dRow("ASI_Name") = dtAccnts.Rows(0)("ASI_Name")
                                dRow("subheadingid") = dtAccnts.Rows(0)("ASSH_ID")
                                dRow("ASSH_name") = dtAccnts.Rows(0)("ASSH_Name")
                                dRow("headingid") = dtAccnts.Rows(0)("ASH_ID")
                                dRow("ASH_Name") = dtAccnts.Rows(0)("ASH_Name")
                                dRow("ScheduleType") = dtAccnts.Rows(0)("AST_Schedule_type")
                            Else
                                ' Nomatch
                                dRow("Subitemid") = ""
                                dRow("ASSI_Name") = ""
                                dRow("itemid") = ""
                                dRow("ASI_Name") = ""
                                dRow("subheadingid") = ""
                                dRow("ASSH_name") = ""
                                dRow("headingid") = ""
                                dRow("ASH_Name") = ""
                                dRow("ScheduleType") = "0"
                            End If
Unmatch:                Else
                            ' No MAtch
                            dRow("Subitemid") = ""
                            dRow("ASSI_Name") = ""
                            dRow("itemid") = ""
                            dRow("ASI_Name") = ""
                            dRow("subheadingid") = ""
                            dRow("ASSH_name") = ""
                            dRow("headingid") = ""
                            dRow("ASH_Name") = ""
                            dRow("ScheduleType") = "0"
                        End If
                    Else
                        dt = objUT.GetPrevYrLinkageDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue - 1, 0, Unmapped, dRow("Description"), DdlbranchSchedTemp.SelectedValue)
                        If dt.Rows.Count > 0 Then
                            dRow("Subitemid") = dt.Rows(0)("subItemID").ToString
                            dRow("ASSI_Name") = dt.Rows(0)("ASSI_Name").ToString
                            dRow("itemid") = dt.Rows(0)("itemid").ToString
                            dRow("ASI_Name") = dt.Rows(0)("ASI_Name").ToString
                            dRow("subheadingid") = dt.Rows(0)("subheadingid").ToString
                            dRow("ASSH_name") = dt.Rows(0)("ASSH_name").ToString
                            dRow("headingid") = dt.Rows(0)("headingid").ToString
                            dRow("ASH_Name") = dt.Rows(0)("ASH_Name").ToString
                            dRow("ScheduleType") = dt.Rows(0)("ATBUD_SChedule_Type").ToString
                        Else
                            dRow("Subitemid") = ""
                            dRow("ASSI_Name") = ""
                            dRow("itemid") = ""
                            dRow("ASI_Name") = ""
                            dRow("subheadingid") = ""
                            dRow("ASSH_name") = ""
                            dRow("headingid") = ""
                            dRow("ASH_Name") = ""
                            dRow("ScheduleType") = "0"
                        End If
                    End If

                    dtTable.Rows.Add(dRow)
                End If
            Next i
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Function
    Public Function ExcelSheetNamesSchedTemp(ByVal sPath As String) As DataTable
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try

            'Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
            'Dim xlApp As New Microsoft.Office.Interop.Excel.Application

            'xlApp.Workbooks.Open(sPath, 0, True)

            '' For the first sheet in an excel spreadsheet
            'xlWorkSheet = CType(xlApp.Sheets(1),
            '        Microsoft.Office.Interop.Excel.Worksheet)
            'Dim strSheetName As New List(Of String)
            'If xlApp.Sheets.Count > 0 Then
            '    strSheetName.Add(xlWorkSheet.Name)
            '    dtTab.Columns.Add("ID")
            '    dtTab.Columns.Add("Name")
            '    For Each xlWorkSheet In xlApp.Sheets
            '        drow = dtTab.NewRow
            '        drow("ID") = i + 1
            '        drow("Name") = xlWorkSheet.Name
            '        dtTab.Rows.Add(drow)
            '    Next
            'End If

            XLCon = MSAccessOpenConnectionSchedTemp(sPath)
            dt = XLCon.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            If dt.Rows.Count > 0 Then
                dtTab.Columns.Add("ID")
                dtTab.Columns.Add("Name")
                For i = 0 To dt.Rows.Count - 1
                    drow = dtTab.NewRow
                    drow("ID") = i + 1
                    drow("Name") = dt.Rows(i)(2)
                    dtTab.Rows.Add(drow)
                Next
            End If
            XLCon.Close()
            Return dtTab
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            Throw
        End Try
    End Function
    Private Function MSAccessOpenConnectionSchedTemp(ByVal sFile As String) As OleDb.OleDbConnection
        Dim con As New OleDb.OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" & sFile & ";Extended Properties=Excel 8.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
        Try
            con.ConnectionString = "Data Source=" & sFile & ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;"
            con.Open()
            Return con
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Function
    Protected Sub btnOkSchedTemp_Click(sender As Object, e As EventArgs) Handles btnOkSchedTemp.ServerClick
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            imgbtnSaveSchedTemp.Enabled = True
            'dgGeneral.DataSource = Nothing
            'dgGeneral.DataBind()
            If ddlCustNameSchedTemp.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblExcelValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            If DdlbranchSchedTemp.SelectedIndex = 0 Then
                lblError.Text = "Select Branch." : lblExcelValidationMsg.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            dgGeneralSchedTemp.Visible = False
            If FULoadSchedTemp.FileName <> String.Empty Then
                lblSheetNameSchedTemp.Visible = True : ddlSheetNameSchedTemp.Visible = True
                imgbtnSaveSchedTemp.Enabled = True : ImgbtnApproveSchedTemp.Enabled = True
                sExt = IO.Path.GetExtension(FULoadSchedTemp.PostedFile.FileName)
                Session("sExt") = sExt
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoadSchedTemp.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName
                    End If
                    FULoadSchedTemp.PostedFile.SaveAs(sFile)
                    ddlSheetNameSchedTemp.Items.Clear()

                    dt = ExcelSheetNamesSchedTemp(sFile)
                    ddlSheetNameSchedTemp.DataSource = dt
                    ddlSheetNameSchedTemp.DataTextField = "Name"
                    ddlSheetNameSchedTemp.DataValueField = "ID"
                    ddlSheetNameSchedTemp.DataBind()
                    ddlSheetNameSchedTemp.Items.Insert(0, "Select Sheet")
                    ddlSheetNameSchedTemp.SelectedValue = 1



                    lblDescID.Text = objUT.CheckCustdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, "", ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
                    If lblDescID.Text <> 0 Then
                        lblError.Text = "Select Excel file only." : lblModal.Text = "Data already existed for this customer for the year - " & ddlFinancialYearSchedTemp.SelectedItem.Text & "Click Yes to replace."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                        Exit Sub
                    End If

                    ddlSheetNameSchedTemp_SelectedIndexChanged(sender, e)
                    DdlbranchSchedTemp_SelectedIndexChanged(sender, e)

                Else
                    lblError.Text = "Select Excel file only." : lblExcelValidationMsg.Text = "Select Excel file only."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                imgbtnSaveSchedTemp.Enabled = False
                lblError.Text = "Select Excel file." : lblExcelValidationMsg.Text = "Select Excel file."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "FileUpload_Load")
        End Try
    End Sub

    Private Sub dgGeneralSchedTemp_PreRender(sender As Object, e As EventArgs) Handles dgGeneralSchedTemp.PreRender
        Try
            If dgGeneralSchedTemp.Rows.Count > 0 Then
                dgGeneralSchedTemp.UseAccessibleHeader = True
                dgGeneralSchedTemp.HeaderRow.TableSection = TableRowSection.TableHeader
                dgGeneralSchedTemp.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssessment_PreRender")
        End Try

    End Sub
    'Private Sub dgGeneralSchedTemp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgGeneralSchedTemp.RowDataBound
    '    Dim ddlGrdSubGroup As New DropDownList
    '    Dim lblslno As New Label
    '    Dim lblOpeningCredit As New Label
    '    Dim lblOpeningDebit As New Label
    '    Dim lblTrDebit As New Label
    '    Dim lblTrCredit As New Label
    '    Dim lblClosingDebit As New Label
    '    Dim lblClosingCredit As New Label
    '    Dim lblStatus As New Label
    '    Dim lblitemid As New Label
    '    Dim dDebtAmt As Double = 0.0
    '    Dim dCreAmt As Double = 0.0
    '    Dim dTotAmt As Double = 0.0
    '    Try
    '        If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
    '            'lblOpeningCredit = (TryCast(e.Row.FindControl("lblOpeningCredit"), Label))
    '            'lblOpeningDebit = (TryCast(e.Row.FindControl("lblOpeningDebit"), Label))
    '            'lblTrDebit = (TryCast(e.Row.FindControl("lblTrDebit"), Label))
    '            'lblTrCredit = (TryCast(e.Row.FindControl("lblTrCredit"), Label))
    '            'lblClosingDebit = (TryCast(e.Row.FindControl("lblClosingDebit"), Label))
    '            'lblClosingCredit = (TryCast(e.Row.FindControl("lblClosingCredit"), Label))
    '            'lblStatus = (TryCast(e.Row.FindControl("lblStatus"), Label))
    '            'lblitemid = (TryCast(e.Row.FindControl("lblDescID"), Label))
    '            'If lblTrDebit.Text = "" Then
    '            '    lblTrDebit.Text = 0
    '            'End If
    '            'If lblTrCredit.Text = "" Then
    '            '    lblTrCredit.Text = 0
    '            'End If
    '            'If lblClosingDebit.Text = "" Then
    '            '    lblClosingDebit.Text = 0
    '            'End If
    '            'If lblClosingCredit.Text = "" Then
    '            '    lblClosingCredit.Text = 0
    '            'End If
    '            'dgGeneralSchedTemp.Columns(16).Visible = False : dgGeneralSchedTemp.Columns(12).Visible = False : dgGeneralSchedTemp.Columns(14).Visible = False : dgGeneralSchedTemp.Columns(10).Visible = False
    '            'If lblOpeningCredit.Text = "" Or Nothing Or IsDBNull(lblOpeningCredit.Text) = True Then
    '            '    lblOpeningCredit.Text = 0
    '            'End If
    '            'If lblOpeningDebit.Text = "" Or Nothing Or IsDBNull(lblOpeningDebit.Text) = True Then
    '            '    lblOpeningDebit.Text = 0
    '            'End If
    '            'If lblClosingCredit.Text = "" Or Nothing Or IsDBNull(lblClosingCredit.Text) = True Then
    '            '    lblClosingCredit.Text = 0
    '            'End If
    '            'If lblClosingDebit.Text = "" Or Nothing Or IsDBNull(lblClosingDebit.Text) = True Then
    '            '    lblClosingDebit.Text = 0
    '            'End If
    '            'If Val(lblClosingDebit.Text) <> 0 Then
    '            '    If lblTrCredit.Text = "" Or Nothing Or IsDBNull(lblTrCredit.Text) = True Then
    '            '        lblTrCredit.Text = 0
    '            '    ElseIf Val(lblTrCredit.Text) <> 0 Then
    '            '        lblTrCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
    '            '        'lblClosingDebit.Text = Val(lblOpeningCredit.Text) + Val(lblTrCredit.Text)
    '            '        lblClosingDebit.Text = Math.Round(Convert.ToDouble(lblClosingDebit.Text), 2)
    '            '    ElseIf lblTrDebit.Text = "" Or Nothing Or IsDBNull(lblTrDebit.Text) = True Then
    '            '        lblTrDebit.Text = 0
    '            '    ElseIf Val(lblTrDebit.Text) <> 0 Then
    '            '        lblTrDebit.Text = Math.Round(Convert.ToDouble(lblTrDebit.Text), 2)
    '            '        'lblClosingDebit.Text = Val(lblTrDebit.Text) - Val(lblOpeningDebit.Text)
    '            '        lblClosingDebit.Text = Math.Round(Convert.ToDouble(lblClosingDebit.Text), 2)
    '            '        'lblTrCredit.Text = lblTrCredit.Text + objUT.LoadItemsfromJECreditdebit(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, Val(lblitemid.Text), 0)
    '            '    End If
    '            'ElseIf lblTrCredit.Text <> 0 And Val(lblClosingDebit.Text) = 0 And Val(lblClosingCredit.Text) = 0 Then
    '            '    ' lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
    '            'ElseIf lblTrDebit.Text <> 0 And Val(lblClosingDebit.Text) = 0 And Val(lblClosingCredit.Text) = 0 Then
    '            '    ' lblClosingDebit.Text = Math.Round(Convert.ToDouble(lblTrDebit.Text), 2)
    '            'End If
    '            'If Val(lblClosingCredit.Text) <> 0 Then
    '            '    If lblTrCredit.Text = "" Or Nothing Or IsDBNull(lblTrCredit.Text) = True Then
    '            '        lblTrCredit.Text = 0
    '            '    ElseIf Val(lblTrCredit.Text) <> 0 Then
    '            '        lblTrCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
    '            '        'lblClosingCredit.Text = Val(lblOpeningCredit.Text) + Val(lblTrCredit.Text)
    '            '        'lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblClosingCredit.Text), 2)
    '            '    ElseIf lblTrDebit.Text = "" Or Nothing Or IsDBNull(lblTrDebit.Text) = True Then
    '            '        lblTrDebit.Text = 0
    '            '    ElseIf Val(lblTrDebit.Text) <> 0 Then
    '            '        lblTrDebit.Text = Math.Round(Convert.ToDouble(lblTrDebit.Text), 2)
    '            '        'lblClosingCredit.Text = Val(lblOpeningCredit.Text) - Val(lblTrDebit.Text)
    '            '        'lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblClosingCredit.Text), 2)
    '            '    End If
    '            'ElseIf lblTrCredit.Text <> 0 Then
    '            '    'lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
    '            'End If
    '            'TotalOpeningCredit = TotalOpeningCredit + Math.Abs(Convert.ToDouble(lblOpeningCredit.Text))
    '            ''TotalOpeningCredit = Decimal.Negate(TotalOpeningCredit)
    '            'TotalOpeningDebit = TotalOpeningDebit + Math.Abs(Convert.ToDouble(lblOpeningDebit.Text))
    '            'TOtalClosingCredit = TOtalClosingCredit + Convert.ToDouble(lblClosingCredit.Text)
    '            'TOtalClosingDebit = TOtalClosingDebit + Convert.ToDouble(lblClosingDebit.Text)
    '            'TOtaltrCredit = TOtaltrCredit + Convert.ToDouble(lblTrCredit.Text)
    '            'TOtaltrDebit = TOtaltrDebit + Convert.ToDouble(lblTrDebit.Text)
    '            imgbtnSaveSchedTemp.Visible = True
    '            dgGeneralSchedTemp.Columns(16).Visible = True : dgGeneralSchedTemp.Columns(12).Visible = True : dgGeneralSchedTemp.Columns(14).Visible = True : dgGeneralSchedTemp.Columns(10).Visible = True
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Private Sub imgbtnSaveSchedTemp_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveSchedTemp.Click

        Try
            If ddlCustNameSchedTemp.SelectedIndex <> 0 Then
                SaveTrailbalanceScheduleSchedTemp()
                '       objUT.UploadPrevdata(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustName.SelectedValue)
                If ddlscheduletypeSchedTemp.SelectedIndex = 0 Then
                    DdlbranchSchedTemp_SelectedIndexChanged(sender, e)
                Else
                    ddlscheduletypeSchedTemp_SelectedIndexChanged(sender, e)
                End If
                lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
            Else
                lblError.Text = "Please Select Customer"
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function SaveTrailbalanceScheduleSchedTemp()
        Dim Arr() As String
        Dim lbldescCode, lblOpeningDebit, lblOpeningCredit, lblTrDebit, lblTrCredit, lblClosingDebit, lblClosingCredit, lblTrDebittrUploaded, lblTrCredittrUploaded As New Label
        Dim lblheadingid, lblsubheadingid, lblitemid As New Label
        Dim lbldescirption As New LinkButton
        Dim dgDdlbranchSchedTemp As New DropDownList
        'Dim ddlHeading As New DropDownList
        'Dim ddlsubheading As New DropDownList
        'Dim ddlItems As New DropDownList
        'Dim ddlSubItems As New DropDownList
        Dim lblDescID, lblsubItemid As New Label
        Dim Masid As Integer = 0
        Dim lblDescdetails As New Label
        Dim chkField As New CheckBox, chkAll As New CheckBox
        For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
            lbldescCode = dgGeneralSchedTemp.Rows(i).FindControl("lblDescriptionCode")
            lblDescdetails = dgGeneralSchedTemp.Rows(i).FindControl("lblDescdetails")
            lblDescID = dgGeneralSchedTemp.Rows(i).FindControl("lblDescID")
            lbldescirption = dgGeneralSchedTemp.Rows(i).FindControl("lblDescription")
            lblOpeningDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningDebit")
            lblOpeningCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningCredit")
            lblTrDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrDebit")
            lblTrCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrCredit")
            lblTrDebittrUploaded = dgGeneralSchedTemp.Rows(i).FindControl("lblTrDebit")
            lblTrCredittrUploaded = dgGeneralSchedTemp.Rows(i).FindControl("lblTrCredit")
            lblClosingDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingDebit")
            lblClosingCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingCredit")
            dgDdlbranchSchedTemp = dgGeneralSchedTemp.Rows(i).FindControl("DdlbranchSchedTemp")
            'ddlHeading = dgGeneral.Rows(i).FindControl("gvddlheading")
            'ddlsubheading = dgGeneral.Rows(i).FindControl("gvddlSubheading")
            'ddlItems = dgGeneral.Rows(i).FindControl("gvddlitem")
            'ddlSubItems = dgGeneral.Rows(i).FindControl("gvddlSubitem")
            '    objUT.iATBUD_ID = 0
            '    objUT.iATBUD_Masid = lblDescID.Text
            '    objUT.sATBUD_CODE = lbldescCode.Text
            '    objUT.sATBUD_Description = lbldescirption.Text
            '    objUT.iATBUD_CustId = ddlCustName.SelectedValue
            '    objUT.iATBUD_SChedule_Type = ddlscheduletype.SelectedValue
            '    If chklmtcomp.Checked = True And chkNonlmtcomp.Checked = True Then
            '        objUT.iATBUD_Company_Type = 3
            '    ElseIf chklmtcomp.Checked = True Then
            '        objUT.iATBUD_Company_Type = 2
            '    ElseIf chkNonlmtcomp.Checked = True Then
            '        objUT.iATBUD_Company_Type = 1
            '    Else
            '        objUT.iATBUD_Company_Type = 0
            '    End If
            '    If ddlHeading.SelectedIndex <> 0 Then
            '        objUT.iATBUD_Headingid = ddlHeading.SelectedValue
            '    Else
            '        objUT.iATBUD_Headingid = 0
            '    End If
            '    If ddlsubheading.SelectedIndex <> 0 Then
            '        objUT.iATBUD_Subheading = ddlItems.SelectedValue
            '    Else
            '        objUT.iATBUD_Subheading = 0
            '    End If
            '    If ddlItems.SelectedIndex <> 0 Then
            '        objUT.iATBUD_itemid = ddlItems.SelectedValue
            '    Else
            '        objUT.iATBUD_itemid = 0
            '    End If
            '    objUT.sATBUD_DELFLG = "A"
            '    objUT.iATBUD_CRBY = sSession.UserID
            '    objUT.sATBUD_STATUS = "C"
            '    objUT.sATBUD_Progress = "Uploaded"
            '    objUT.iATBUD_UPDATEDBY = sSession.UserID
            '    objUT.sATBUD_IPAddress = sSession.IPAddress
            '    objUT.iATBUD_CompId = sSession.AccessCodeID
            '    objUT.iATBUD_YEARId = ddlFinancialYear.selectedvalue
            '    Arr = objUT.SaveTrailBalanceExcelUploaddetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
            '    lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
            '    ddlCustName_SelectedIndexChanged(sender, e)

            Dim checkdata As Integer = 0


            If lblOpeningDebit.Text = "" Or Nothing Then
                lblOpeningDebit.Text = 0
            End If
            If lblOpeningCredit.Text = "" Or Nothing Then
                lblOpeningCredit.Text = 0
            End If
            If lblTrCredit.Text = "" Or Nothing Then
                lblTrCredit.Text = 0
            End If
            If lblTrDebit.Text = "" Or Nothing Then
                lblTrDebit.Text = 0
            End If
            If lblTrDebittrUploaded.Text = "" Or Nothing Then
                lblTrDebittrUploaded.Text = 0
            End If
            If lblTrCredittrUploaded.Text = "" Or Nothing Then
                lblTrCredittrUploaded.Text = 0
            End If
            If lblClosingCredit.Text = "" Or Nothing Then
                lblClosingCredit.Text = 0
            End If

            If lblClosingDebit.Text = "" Or Nothing Then
                lblClosingDebit.Text = 0
            End If

            If lblheadingid.Text = "" Or Nothing Then
                lblheadingid.Text = 0
            End If
            If lblsubheadingid.Text = "" Or Nothing Then
                lblsubheadingid.Text = 0
            End If
            If lblitemid.Text = "" Or Nothing Then
                lblitemid.Text = 0
            End If

            If lblsubItemid.Text = "" Or Nothing Then
                lblsubItemid.Text = 0
            End If
            'If ddlFinancialYear.SelectedIndex = 0 Then
            '    checkdata = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, lbldescirption.Text, ddlFinancialYear.SelectedValue)
            '    '  checkdata = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, lbldescirption.Text, ddlFinancialYear.selectedvalue - 1)
            'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
            '    checkdata = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, lbldescirption.Text, ddlFinancialYear.SelectedValue - 1)
            'End If
            checkdata = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
            lblDescdetails.Text = objUT.CheckDetaileddata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)

            If checkdata = 0 Then
                If Val(lblDescID.Text) = 0 Then
                    If Val(lblDescID.Text) <> 0 Then
                        objUT.iATBU_ID = lblDescID.Text
                    Else
                        objUT.iATBU_ID = 0
                    End If
                    If lbldescCode.Text <> "" Then
                        objUT.sATBU_CODE = lbldescCode.Text
                    Else
                        objUT.sATBU_CODE = "SCh00" & (i + 1)
                    End If
                    objUT.sATBU_Description = lbldescirption.Text
                    objUT.iATBU_CustId = ddlCustNameSchedTemp.SelectedValue
                    objUT.dATBU_Opening_Debit_Amount = Double.Parse(lblOpeningDebit.Text)
                    objUT.dATBU_Opening_Credit_Amount = Double.Parse(lblOpeningCredit.Text)
                    objUT.dATBU_TR_Debit_Amount = Double.Parse(lblTrDebittrUploaded.Text)
                    objUT.dATBU_TR_Credit_Amount = Double.Parse(lblTrCredittrUploaded.Text)
                    objUT.dATBU_Closing_Debit_Amount = Double.Parse(lblClosingDebit.Text)
                    objUT.dATBU_Closing_Credit_Amount = Double.Parse(lblClosingCredit.Text)
                    objUT.sATBU_DELFLG = "A"
                    objUT.iATBU_CRBY = sSession.UserID
                    objUT.sATBU_STATUS = "C"
                    objUT.iATBU_UPDATEDBY = sSession.UserID
                    objUT.sATBU_IPAddress = sSession.IPAddress
                    objUT.iATBU_CompId = sSession.AccessCodeID
                    'If ddlFinancialYear.SelectedIndex = 0 Then
                    '    objUT.iATBU_YEARId = ddlFinancialYear.SelectedValue
                    'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
                    '    objUT.iATBU_YEARId = ddlFinancialYear.SelectedValue - 1
                    'End If
                    objUT.iATBU_YEARId = ddlFinancialYearSchedTemp.SelectedValue
                    Arr = objUT.SaveTrailBalanceExcelUpload(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                End If
                If Val(lblDescdetails.Text) = 0 Then
                    '       objUT.iATBUD_ID = Val(lblDescID.Text)
                    objUT.iATBUD_ID = 0
                    objUT.iATBUD_Masid = Arr(1)
                Else
                    objUT.iATBUD_ID = lblDescdetails.Text
                    objUT.iATBUD_Masid = Arr(1)
                End If
                If lbldescCode.Text <> "" Then
                    objUT.sATBUD_CODE = lbldescCode.Text
                Else
                    objUT.sATBUD_CODE = "SCh00" & (i + 1)
                End If

                objUT.sATBUD_Description = lbldescirption.Text
                objUT.iATBUD_CustId = ddlCustNameSchedTemp.SelectedValue
                If ddlscheduletypeSchedTemp.SelectedIndex > 0 Then
                    objUT.iATBUD_SChedule_Type = ddlscheduletypeSchedTemp.SelectedValue
                Else
                    objUT.iATBUD_SChedule_Type = 0
                End If
                If DdlbranchSchedTemp.SelectedIndex > 0 Then
                    objUT.iATBUD_Branchname = DdlbranchSchedTemp.SelectedValue
                Else
                    objUT.iATBUD_Branchname = 0
                End If
                objUT.iATBUD_Company_Type = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue)
                If ddlHeadingSchedTemp.SelectedIndex > 0 Then
                    objUT.iATBUD_Headingid = ddlHeadingSchedTemp.SelectedValue
                Else
                    objUT.iATBUD_Headingid = 0
                End If
                If ddlsubheadingSchedTemp.SelectedIndex > 0 Then
                    objUT.iATBUD_Subheading = ddlsubheadingSchedTemp.SelectedValue
                Else
                    objUT.iATBUD_Subheading = 0
                End If
                If ddlitemsSchedTemp.SelectedIndex > 0 Then
                    objUT.iATBUD_itemid = ddlitemsSchedTemp.SelectedValue
                Else
                    objUT.iATBUD_itemid = 0
                End If
                If ddlSUbItemsSchedTemp.SelectedIndex > 0 Then
                    objUT.iATBUD_Subitemid = ddlSUbItemsSchedTemp.SelectedValue
                Else
                    objUT.iATBUD_Subitemid = 0
                End If
                objUT.sATBUD_DELFLG = "A"
                objUT.iATBUD_CRBY = sSession.UserID
                objUT.sATBUD_STATUS = "C"
                objUT.sATBUD_Progress = "Uploaded"
                objUT.iATBUD_UPDATEDBY = sSession.UserID
                objUT.sATBUD_IPAddress = sSession.IPAddress
                objUT.iATBUD_CompId = sSession.AccessCodeID
                'If ddlFinancialYear.SelectedIndex = 0 Then
                '    objUT.iATBUD_YEARId = ddlFinancialYear.SelectedValue
                'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
                '    objUT.iATBUD_YEARId = ddlFinancialYear.SelectedValue - 1
                'End If
                objUT.iATBUD_YEARId = ddlFinancialYearSchedTemp.SelectedValue
                Arr = objUT.SaveTrailBalanceExcelUploaddetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
            Else
                chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                If chkField.Checked = True Then
                    If Val(lblDescdetails.Text) = 0 Then
                        objUT.iATBUD_ID = Val(lblDescID.Text)
                        objUT.iATBUD_Masid = Arr(1)
                    Else
                        'objUT.iATBUD_ID = lblDescID.Text
                        'objUT.iATBUD_Masid = lblDescdetails.Text
                        objUT.iATBUD_ID = lblDescdetails.Text
                        objUT.iATBUD_Masid = lblDescID.Text
                    End If
                    If lbldescCode.Text <> "" Then
                        objUT.sATBUD_CODE = lbldescCode.Text
                    Else
                        objUT.sATBUD_CODE = "SCh00" & (i + 1)
                    End If
                    objUT.sATBUD_Description = lbldescirption.Text
                    objUT.iATBUD_CustId = ddlCustNameSchedTemp.SelectedValue
                    If ddlHeadingSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_SChedule_Type = ddlscheduletypeSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_SChedule_Type = 0
                    End If
                    If DdlbranchSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_Branchname = DdlbranchSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_Branchname = 0
                    End If
                    objUT.iATBUD_Company_Type = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue)
                    If ddlHeadingSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_Headingid = ddlHeadingSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_Headingid = 0
                    End If
                    If ddlsubheadingSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_Subheading = ddlsubheadingSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_Subheading = 0
                    End If
                    If ddlitemsSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_itemid = ddlitemsSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_itemid = 0
                    End If
                    If ddlSUbItemsSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_Subitemid = ddlSUbItemsSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_Subitemid = 0
                    End If
                    objUT.sATBUD_DELFLG = "A"
                    objUT.iATBUD_CRBY = sSession.UserID
                    objUT.sATBUD_STATUS = "C"
                    objUT.sATBUD_Progress = "Uploaded"
                    objUT.iATBUD_UPDATEDBY = sSession.UserID
                    objUT.sATBUD_IPAddress = sSession.IPAddress
                    objUT.iATBUD_CompId = sSession.AccessCodeID
                    '      objUT.iATBUD_YEARId = ddlFinancialYear.selectedvalue
                    'If ddlFinancialYear.SelectedIndex = 0 Then
                    '    objUT.iATBUD_YEARId = ddlFinancialYear.SelectedValue
                    'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
                    '    objUT.iATBUD_YEARId = ddlFinancialYear.SelectedValue - 1
                    'End If
                    objUT.iATBUD_YEARId = ddlFinancialYearSchedTemp.SelectedValue
                    Arr = objUT.SaveTrailBalanceExcelUploaddetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                End If
            End If
        Next
        If chkAll.Checked = True Then
            For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                chkField.Checked = False
            Next
        Else
            For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                If chkField.Checked = True Then
                    chkField.Checked = False
                End If
            Next
        End If
    End Function
    Private Sub dgGeneralSchedTemp_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgGeneralSchedTemp.RowCommand
        Dim lblItemid As New Label
        Dim dt1, dt2 As DataTable
        Try
            lblError.Text = ""
            If e.CommandName = "EditRow" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblItemid = DirectCast(clickedRow.FindControl("lblDescdetails"), Label)
                dt1 = objUT.LoadItemsfromTB(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lblItemid.Text, ddlFinancialYearSchedTemp.SelectedValue)
                If DdlbranchSchedTemp.SelectedIndex > 0 Then
                    dt2 = objUT.LoadItemsfromJE(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lblItemid.Text, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
                Else
                    dt2 = objUT.LoadItemsfromJE(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lblItemid.Text, ddlFinancialYearSchedTemp.SelectedValue, 0)
                End If
                dt2.Merge(dt1, True, MissingSchemaAction.Ignore)
                gvJeitemsSchedTemp.DataSource = dt2
                gvJeitemsSchedTemp.DataBind()
                For i = 0 To gvJeitemsSchedTemp.Rows.Count

                Next
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalJEItemsSchedTemp').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_RowCommand")
        End Try
    End Sub

    Private Sub gvJeitemsSchedTemp_PreRender(sender As Object, e As EventArgs) Handles gvJeitemsSchedTemp.PreRender
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvJeitemsSchedTemp_DataBound(sender As Object, e As EventArgs) Handles gvJeitemsSchedTemp.DataBound
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvJeitemsSchedTemp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvJeitemsSchedTemp.RowDataBound
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlscheduletypeSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlscheduletypeSchedTemp.SelectedIndexChanged
        Dim dt As New DataSet
        Dim dt1, dt2 As DataTable
        Dim lblslno As New Label, lblSglDes As New Label, lblOBDebit As New Label, lblOBCredit As New Label, lblTrDebit As New Label, lblTrCredit As New Label, lblCBDebit As New Label, lblCBCredit As New Label
        Dim lblglTot As New Label, lblsgTt As New Label, lblGroupTot As New Label, lblHeadTot As New Label, lblGroup As New Label, lblHead As New Label
        Try
            lblError.Text = ""
            'If ddlFinancialYear.SelectedIndex = 0 Then
            '    dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, ddlscheduletype.SelectedValue, Unmapped)
            'ElseIf ddlFinancialYear.SelectedIndex = 1 Then
            '    dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue - 1, ddlscheduletype.SelectedValue, Unmapped)
            'End If
            If DdlbranchSchedTemp.SelectedIndex = 0 Then
                lblExcelValidationMsg.Text = "Select Branch"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
                Exit Sub
            End If
            dt = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
            dt1 = dt.Tables(0)
            If dt1.Rows.Count > 0 Then
                dgGeneralSchedTemp.Visible = True
                dgGeneralSchedTemp.DataSource = dt
                dgGeneralSchedTemp.DataBind()
                pnlFreeze.Visible = True
                dt2 = dt.Tables(1)
                GrdviewTotalAmount.DataSource = dt2
                GrdviewTotalAmount.DataBind()
            Else
                If ddlscheduletypeSchedTemp.SelectedIndex <> 0 Then
                    'If ddlFinancialYear.SelectedIndex = 1 Then
                    '    dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, 0, Unmapped)
                    'ElseIf ddlFinancialYear.SelectedIndex = 2 Then
                    '    dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue - 1, 0, Unmapped)
                    'End If
                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
                    End If

                    If dt1.Rows.Count > 0 Then
                        dgGeneralSchedTemp.Visible = True
                        dgGeneralSchedTemp.DataSource = dt
                        dgGeneralSchedTemp.DataBind()
                        For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                            ddlCustNameSchedTemp.SelectedValue = dt1.Rows(i)("ATBU_CustId")
                            lblDescID = dgGeneralSchedTemp.Rows(i).FindControl("lblDescID")
                            lblDescID.Text = Val(0)
                            If dt1.Rows(i)("Status").ToString.Trim = "Uploaded" Then
                                ddlscheduletypeSchedTemp.Enabled = True
                                lblscheduletypeSchedTemp.Enabled = True
                                DdlbranchSchedTemp.Visible = True
                                LblBranchNameSchedTemp.Visible = True
                                If ddlscheduletypeSchedTemp.SelectedIndex = 0 Then
                                    ddlscheduletypeSchedTemp.SelectedValue = dt1.Rows(i)("ScheduleType")
                                End If
                            End If
                        Next
                    Else
                        dgGeneralSchedTemp.Visible = False
                        dgGeneralSchedTemp.DataSource = Nothing
                        dgGeneralSchedTemp.DataBind()
                    End If
                Else
                    dgGeneralSchedTemp.Visible = False

                End If
                lblExcelValidationMsg.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-info');$('#ModalExcelValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
            End If
            'dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, ddlscheduletypeSchedTemp.SelectedValue, Unmapped, DdlbranchSchedTemp.SelectedValue)
            'If dt.Rows.Count > 0 Then
            '    dgGeneralSchedTemp.Visible = True
            '    dgGeneralSchedTemp.DataSource = dt
            '    dgGeneralSchedTemp.DataBind()
            'Else
            '    If ddlscheduletypeSchedTemp.SelectedIndex <> 0 Then
            '        'If ddlFinancialYear.SelectedIndex = 1 Then
            '        '    dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, 0, Unmapped)
            '        'ElseIf ddlFinancialYear.SelectedIndex = 2 Then
            '        '    dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue - 1, 0, Unmapped)
            '        'End If
            '        dt = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
            '        If dt.Rows.Count > 0 Then
            '            dgGeneralSchedTemp.Visible = True
            '            dgGeneralSchedTemp.DataSource = dt
            '            dgGeneralSchedTemp.DataBind()
            '            For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
            '                ddlCustNameSchedTemp.SelectedValue = dt.Rows(i)("ATBU_CustId")
            '                lblDescID = dgGeneralSchedTemp.Rows(i).FindControl("lblDescID")
            '                lblDescID.Text = Val(0)
            '                If dt.Rows(i)("Status").ToString.Trim = "Uploaded" Then
            '                    ddlscheduletypeSchedTemp.Visible = True
            '                    lblscheduletypeSchedTemp.Visible = True
            '                    DdlbranchSchedTemp.Visible = True
            '                    LblBranchNameSchedTemp.Visible = True
            '                    If ddlscheduletypeSchedTemp.SelectedIndex = 0 Then
            '                        ddlscheduletypeSchedTemp.SelectedValue = dt.Rows(i)("ScheduleType")
            '                    End If
            '                End If
            '            Next
            '        Else
            '            dgGeneralSchedTemp.Visible = False
            '            dgGeneralSchedTemp.DataSource = Nothing
            '            dgGeneralSchedTemp.DataBind()
            '        End If
            '    Else
            '        dgGeneralSchedTemp.Visible = False
            '        lblExcelValidationMsg.Text = "No Data" : lblError.Text = "No Data."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            '    End If
            'End If
            ddlHeadingSchedTemp.DataSource = objUT.BindScheduleHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)
            ddlHeadingSchedTemp.DataTextField = "ASH_Name"
            ddlHeadingSchedTemp.DataValueField = "ASH_ID"
            ddlHeadingSchedTemp.DataBind()
            ddlHeadingSchedTemp.Items.Insert(0, New ListItem("Select Heading"))


            ddlsubheadingSchedTemp.DataSource = objUT.BindScheduleSubHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, 0)
            ddlsubheadingSchedTemp.DataTextField = "ASSH_Name"
            ddlsubheadingSchedTemp.DataValueField = "ASSH_ID"
            ddlsubheadingSchedTemp.DataBind()
            ddlsubheadingSchedTemp.Items.Insert(0, New ListItem("Select Sub Heading"))


            ddlitemsSchedTemp.DataSource = objUT.BindScheduleItemsHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, 0, 0)
            ddlitemsSchedTemp.DataTextField = "ASI_Name"
            ddlitemsSchedTemp.DataValueField = "ASI_ID"
            ddlitemsSchedTemp.DataBind()
            ddlitemsSchedTemp.Items.Insert(0, New ListItem("Select Item Heading"))


            ddlSUbItemsSchedTemp.DataSource = objUT.BindScheduleSubItemsHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, 0, 0, 0)
            ddlSUbItemsSchedTemp.DataTextField = "ASSI_Name"
            ddlSUbItemsSchedTemp.DataValueField = "ASSI_ID"
            ddlSUbItemsSchedTemp.DataBind()
            ddlSUbItemsSchedTemp.Items.Insert(0, New ListItem("Select SubItem Heading"))

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub gvddlSubitem_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlSubheading As New DropDownList
        Dim ddlheading As New DropDownList
        Dim ddlitems As New DropDownList
        Dim ddlSubitems As New DropDownList
        Dim lblitems As New Label
        Dim lblhead As New Label
        Dim lblheading As New Label
        Dim lblSubheading As New Label
        Dim lblSubitemid As New Label
        Dim i As Integer
        Dim lblslno As Label
        Dim dt As New DataTable, dtGroup As New DataTable
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Dim lblItemid As Integer
        Dim iSubitem As Integer = 0
        Dim iitem As Integer = 0
        Dim isubheading As Integer = 0
        Dim iheading As Integer = 0
        Try
            lblError.Text = ""

            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = True
                Next
            Else
                Dim iitemid As Integer = 0
                Dim isheadingid As Integer = 0
                Dim iheadingid As Integer = 0
                Dim isubitemid As Integer = 0
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        ddlSubheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlSubheading")
                        ddlheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlheading")
                        ddlitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlitem")
                        ddlSubitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlsubitem")
                        If iSubitem = 0 Then
                            If ddlSubitems.SelectedIndex > 0 Then
                                isubitemid = ddlSubitems.SelectedValue
                                iSubitem = isubitemid
                            Else
                                isubitemid = 0
                            End If
                        Else
                            isubitemid = iSubitem
                            ddlSubitems.SelectedValue = iSubitem
                        End If
                        Dim dtTempatedetails As DataTable
                        dtTempatedetails = objUT.bindtemplate(sSession.AccessCode, sSession.AccessCodeID, isubitemid, iitemid, isheadingid, iheadingid, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)

                        If dtTempatedetails.Rows.Count <> 0 Then
                            If dtTempatedetails.Rows(0)("Ast_headingid") > 0 Then
                                ddlheading.SelectedValue = dtTempatedetails.Rows(0)("Ast_headingid")
                            End If
                            If dtTempatedetails.Rows(0)("ast_subheadingid") > 0 Then
                                ddlSubheading.SelectedValue = dtTempatedetails.Rows(0)("ast_subheadingid")
                            Else
                                ddlSubheading.SelectedIndex = 0
                            End If
                            If dtTempatedetails.Rows(0)("AST_ItemID") > 0 Then
                                ddlitems.SelectedValue = dtTempatedetails.Rows(0)("AST_ItemID")
                            Else
                                ddlitems.SelectedIndex = 0
                            End If
                        End If
                    End If
                Next
                SaveTrailbalanceScheduleSchedTemp()
            End If
            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = False
                Next
            Else
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        chkField.Checked = False
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub gvddlheading_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlSubheading As New DropDownList
        Dim ddlheading As New DropDownList
        Dim ddlitems As New DropDownList
        Dim ddlSubitems As New DropDownList
        Dim lblitems As New Label
        Dim lblhead As New Label
        Dim lblheading As New Label
        Dim lblSubheading As New Label
        Dim lblSubitemid As New Label
        Dim i As Integer
        Dim lblslno As Label
        Dim dt As New DataTable, dtGroup As New DataTable
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Dim lblItemid As Integer
        Dim iSubitem As Integer = 0
        Dim iitem As Integer = 0
        Dim isubheading As Integer = 0
        Dim iheading As Integer = 0
        Try
            lblError.Text = ""
            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = True
                Next
            Else
                Dim iitemid As Integer = 0
                Dim isheadingid As Integer = 0
                Dim iheadingid As Integer = 0
                Dim isubitemid As Integer = 0
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        ddlSubheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlSubheading")
                        ddlheading = dgGeneralSchedTemp.Rows(i).FindControl("gvddlheading")
                        ddlitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlitem")
                        ddlSubitems = dgGeneralSchedTemp.Rows(i).FindControl("gvddlsubitem")
                        If iheadingid = 0 Then
                            If ddlheading.SelectedIndex > 0 Then
                                iheadingid = ddlheading.SelectedValue
                                iheading = iheadingid
                            Else
                                iheadingid = 0
                            End If
                        Else
                            iheadingid = iheading
                            ddlheading.SelectedValue = iheading
                        End If
                        Dim dtTempatedetails As DataTable
                        dtTempatedetails = objUT.bindtemplate(sSession.AccessCode, sSession.AccessCodeID, isubitemid, iitemid, isheadingid, iheadingid, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)

                        If dtTempatedetails.Rows.Count <> 0 Then
                            If dtTempatedetails.Rows(0)("Ast_headingid") > 0 Then
                                ddlheading.SelectedValue = dtTempatedetails.Rows(0)("Ast_headingid")
                            End If
                            ddlSubitems.SelectedIndex = 0
                            ddlitems.SelectedIndex = 0
                            ddlSubheading.SelectedIndex = 0
                        End If
                    End If
                Next
                SaveTrailbalanceScheduleSchedTemp()
            End If
            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(iIndx).FindControl("chkSelectSchedTemp")
                    chkField.Checked = False
                Next
            Else
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    chkField = dgGeneralSchedTemp.Rows(i).FindControl("chkSelectSchedTemp")
                    If chkField.Checked = True Then
                        chkField.Checked = False
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
            For i = 0 To 10000
                i = i + 1
            Next
            lblMsg.Text = "File Uploaded Successfully"
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlHeadingSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHeadingSchedTemp.SelectedIndexChanged
        Try
            If ddlHeadingSchedTemp.SelectedIndex > 0 Then
                'ddlsubheadingSchedTemp.SelectedIndex = 0
                'ddlitemsSchedTemp.SelectedIndex = 0
                'ddlSUbItemsSchedTemp.SelectedIndex = 0
                LoadSubHeadingSchedTemp(ddlHeadingSchedTemp.SelectedValue)
                ddlitemsSchedTemp.Items.Clear()
                ddlSUbItemsSchedTemp.Items.Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub LoadSubHeadingSchedTemp(ByVal iHeadSchedTemp As Integer)
        Dim dt As New DataTable
        Try
            dt = objUT.BindScheduleSubHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, iHeadSchedTemp)
            If dt.Rows.Count > 0 Then
                ddlsubheadingSchedTemp.DataSource = dt
                ddlsubheadingSchedTemp.DataTextField = "ASSH_Name"
                ddlsubheadingSchedTemp.DataValueField = "ASSH_ID"
                ddlsubheadingSchedTemp.DataBind()
                ddlsubheadingSchedTemp.Items.Insert(0, New ListItem("Select Sub Heading"))
            Else
                ddlsubheadingSchedTemp.Items.Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadItemSchedTemp(ByVal iheadingId As Integer, ByVal iSubHeadSchedTemp As Integer)
        Dim dt As New DataTable
        Try
            dt = objUT.BindScheduleItemsHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, iheadingId, iSubHeadSchedTemp)
            If dt.Rows.Count > 0 Then
                ddlitemsSchedTemp.DataSource = dt
                ddlitemsSchedTemp.DataTextField = "ASI_Name"
                ddlitemsSchedTemp.DataValueField = "ASI_ID"
                ddlitemsSchedTemp.DataBind()
                ddlitemsSchedTemp.Items.Insert(0, New ListItem("Select Item Heading"))
            Else
                ddlitemsSchedTemp.Items.Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadSubItemSchedTemp(ByVal iheadingId As Integer, ByVal iSubHeadSchedTemp As Integer, ByVal iItemIdSchedTemp As Integer)
        Dim dt As New DataTable
        Try
            dt = objUT.BindScheduleSubItemsHeading(sSession.AccessCode, sSession.AccessCodeID, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, iheadingId, iSubHeadSchedTemp, iItemIdSchedTemp)
            If dt.Rows.Count > 0 Then
                ddlSUbItemsSchedTemp.DataSource = dt
                ddlSUbItemsSchedTemp.DataTextField = "ASSI_Name"
                ddlSUbItemsSchedTemp.DataValueField = "ASSI_ID"
                ddlSUbItemsSchedTemp.DataBind()
                ddlSUbItemsSchedTemp.Items.Insert(0, New ListItem("Select SubItem Heading"))
            Else
                ddlSUbItemsSchedTemp.Items.Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlsubheadingSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsubheadingSchedTemp.SelectedIndexChanged
        Try
            'ddlitemsSchedTemp.SelectedIndex = 0
            'ddlSUbItemsSchedTemp.SelectedIndex = 0
            Dim iitemid As Integer = 0
            Dim isheadingid As Integer = 0
            Dim iheadingid As Integer = 0
            Dim isubitemid As Integer = 0
            Dim iitem As Integer = 0
            If dgGeneralSchedTemp.Rows.Count > 0 Then
                Dim dtTempatedetails As DataTable
                If iitem = 0 Then
                    If ddlsubheadingSchedTemp.SelectedIndex > 0 Then
                        isheadingid = ddlsubheadingSchedTemp.SelectedValue
                        iitem = iitemid
                    Else
                        iitemid = 0
                    End If
                Else
                    iitemid = iitem
                    ddlitemsSchedTemp.SelectedValue = iitem
                End If
                dtTempatedetails = objUT.bindtemplate(sSession.AccessCode, sSession.AccessCodeID, 0, 0, isheadingid, 0, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)

                If dtTempatedetails.Rows.Count <> 0 Then
                    If ddlHeadingSchedTemp.SelectedIndex <= 0 Then
                        If dtTempatedetails.Rows(0)("Ast_headingid") > 0 Then
                            ddlHeadingSchedTemp.SelectedValue = dtTempatedetails.Rows(0)("Ast_headingid")
                        End If
                        If dtTempatedetails.Rows(0)("ast_subheadingid") > 0 Then
                            ddlsubheadingSchedTemp.SelectedValue = dtTempatedetails.Rows(0)("ast_subheadingid")
                        Else
                            ddlsubheadingSchedTemp.SelectedIndex = 0
                        End If
                    End If
                    LoadItemSchedTemp(ddlHeadingSchedTemp.SelectedValue, ddlsubheadingSchedTemp.SelectedValue)
                    ddlSUbItemsSchedTemp.Items.Clear()
                    '   ddlitemsSchedTemp.SelectedIndex = 0
                    'If dtTempatedetails.Rows(0)("AST_ItemID") > 0 Then
                    '    ddlitems.SelectedValue = dtTempatedetails.Rows(0)("AST_ItemID")
                    'Else
                    '    ddlitems.SelectedIndex = 0
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlitemsSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlitemsSchedTemp.SelectedIndexChanged

        Try
            '     ddlSUbItemsSchedTemp.SelectedIndex = 0
            Dim iitemid As Integer = 0
            Dim isheadingid As Integer = 0
            Dim iheadingid As Integer = 0
            Dim isubitemid As Integer = 0
            Dim iitem As Integer = 0

            If dgGeneralSchedTemp.Rows.Count > 0 Then
                Dim dtTempatedetails As DataTable
                If iitem = 0 Then
                    If ddlitemsSchedTemp.SelectedIndex > 0 Then
                        iitemid = ddlitemsSchedTemp.SelectedValue
                        iitem = iitemid
                    Else
                        iitemid = 0
                    End If
                Else
                    iitemid = iitem
                    ddlitemsSchedTemp.SelectedValue = iitem
                End If
                dtTempatedetails = objUT.bindtemplate(sSession.AccessCode, sSession.AccessCodeID, 0, iitem, 0, 0, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)
                If dtTempatedetails.Rows.Count <> 0 Then
                    If ddlsubheadingSchedTemp.SelectedIndex <= 0 Then
                        If dtTempatedetails.Rows(0)("Ast_headingid") > 0 Then
                            ddlHeadingSchedTemp.SelectedValue = dtTempatedetails.Rows(0)("Ast_headingid")
                        End If
                        If dtTempatedetails.Rows(0)("ast_subheadingid") > 0 Then
                            ddlsubheadingSchedTemp.SelectedValue = dtTempatedetails.Rows(0)("ast_subheadingid")
                        Else
                            ddlsubheadingSchedTemp.SelectedIndex = 0
                        End If
                        LoadSubItemSchedTemp(ddlHeadingSchedTemp.SelectedValue, ddlsubheadingSchedTemp.SelectedValue, ddlitemsSchedTemp.SelectedValue)
                        '     ddlSUbItemsSchedTemp.SelectedIndex = 0
                        'If dtTempatedetails.Rows(0)("AST_ItemID") > 0 Then
                        '    ddlitems.SelectedValue = dtTempatedetails.Rows(0)("AST_ItemID")
                        'Else
                        '    ddlitems.SelectedIndex = 0
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlSUbItemsSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSUbItemsSchedTemp.SelectedIndexChanged
        Try
            Dim dtTempatedetails As DataTable
            dtTempatedetails = objUT.bindtemplate(sSession.AccessCode, sSession.AccessCodeID, ddlSUbItemsSchedTemp.SelectedValue, 0, 0, 0, ddlscheduletypeSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue)

            If dtTempatedetails.Rows.Count <> 0 Then
                If ddlitemsSchedTemp.SelectedIndex <= 0 Then
                    If dtTempatedetails.Rows(0)("Ast_headingid") > 0 Then
                        ddlHeadingSchedTemp.SelectedValue = dtTempatedetails.Rows(0)("Ast_headingid")
                    End If
                    If dtTempatedetails.Rows(0)("ast_subheadingid") > 0 Then
                        ddlsubheadingSchedTemp.SelectedValue = dtTempatedetails.Rows(0)("ast_subheadingid")
                    Else
                        ddlsubheadingSchedTemp.SelectedIndex = 0
                    End If
                    If dtTempatedetails.Rows(0)("AST_ItemID") > 0 Then
                        ddlitemsSchedTemp.SelectedValue = dtTempatedetails.Rows(0)("AST_ItemID")
                    Else
                        ddlitemsSchedTemp.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlFinancialYearSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYearSchedTemp.SelectedIndexChanged
        Try
            ddlFinancialYear.SelectedValue = ddlFinancialYearSchedTemp.SelectedValue
            sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
            Session("AllSession") = sSession
            If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                lnkBtnFreezePrev.Visible = False
                ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            ddlFinancialYearSchedTemp.SelectedValue = ddlFinancialYear.SelectedValue
            sSession.ScheduleYearId = ddlFinancialYearSchedTemp.SelectedValue
            Session("AllSession") = sSession
            ddlCustName_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkOnOffSchedTemp_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnOffSchedTemp.CheckedChanged
        Try
            If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                If chkOnOffSchedTemp.Checked = True Then
                    Unmapped = 1
                Else
                    Unmapped = 0
                End If
                If ddlscheduletypeSchedTemp.SelectedIndex > 0 Then
                    ddlscheduletypeSchedTemp_SelectedIndexChanged(sender, e)
                ElseIf DdlbranchSchedTemp.SelectedIndex > 0 Then
                    DdlbranchSchedTemp_SelectedIndexChanged(sender, e)
                ElseIf ddlCustName.SelectedIndex > 0 Then
                    ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
                End If
            ElseIf chkOnOffSchedTemp.Checked = True Then
                chkOnOffSchedTemp.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlUploadType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUploadType.SelectedIndexChanged

        Try
            If ddlUploadType.SelectedIndex > 0 Then
                If ddlUploadType.SelectedIndex = 1 Then
                    pnlSchedTemp.Visible = True
                    pnlClosingSTock.Visible = False
                    If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                        ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
                    End If
                ElseIf ddlUploadType.SelectedIndex = 2 Then
                    pnlClosingSTock.Visible = True
                    pnlSchedTemp.Visible = False
                    If ddlCustName.SelectedIndex > 0 Then
                        ddlCustName_SelectedIndexChanged(sender, e)
                    End If
                End If

            Else

            End If
        Catch ex As Exception

        End Try
    End Sub



    ''''' for Stock Entry

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim chkField2 As New CheckBox
            Dim scount As Integer = 0

            chkField2.Checked = True

            scount = dgGeneral.Rows.Count - 1

            chkField2 = dgGeneral.Rows(scount).FindControl("chkSelect")
            If chkField2.Checked = True Then

            Else
                chkField2.Checked = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelect_CheckedChanged")
        End Try
    End Sub
    Public Sub LoadExistingCustomer()
        Try
            ddlCustName.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustName.DataTextField = "Cust_Name"
            ddlCustName.DataValueField = "Cust_Id"
            ddlCustName.DataBind()
            ddlCustName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Private Sub ddlCustName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim lblslno As New Label, lblSglDes As New Label, lblOBDebit As New Label, lblOBCredit As New Label, lblTrDebit As New Label, lblTrCredit As New Label, lblCBDebit As New Label, lblCBCredit As New Label
        Dim lblglTot As New Label, lblsgTt As New Label, lblGroupTot As New Label, lblHeadTot As New Label, lblGroup As New Label, lblHead As New Label
        Dim ddlHeading As New DropDownList
        Dim ddlsubheading As New DropDownList
        Dim ddlItems As New DropDownList
        Dim ddlSubItems As New DropDownList
        Try
            lblError.Text = ""
            imgbtnSave.Enabled = True
            ImgbtnApprove.Enabled = True
            If ddlCustName.SelectedIndex > 0 Then
                sSession.CustomerID = ddlCustName.SelectedValue
                ddlCustNameSchedTemp.SelectedValue = sSession.CustomerID
                Session("AllSession") = sSession
                dt = objclsUSEntry.GetCustStockEntryDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblTotal.Text = objclsUSEntry.GetCustStockEntryTotal(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue)
                If dt.Rows.Count > 0 Then
                    dgGeneral.Visible = True
                    dgGeneral.DataSource = dt
                    dgGeneral.DataBind()
                Else
                    dgGeneral.Visible = False
                    imgbtnSave.Enabled = False
                    ImgbtnApprove.Enabled = False
                    lblError.Text = "No Data Found. Upload Trail Balance For this Customer"
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            dgGeneral.Visible = False
            If FULoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlSheetName.Visible = True
                imgbtnSave.Enabled = True : ImgbtnApprove.Enabled = True
                sExt = IO.Path.GetExtension(FULoad.PostedFile.FileName)
                Session("sExt") = sExt
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoad.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName
                    End If
                    FULoad.PostedFile.SaveAs(sFile)
                    ddlSheetName.Items.Clear()
                    dt = ExcelSheetNames(sFile)
                    ddlSheetName.DataSource = dt
                    ddlSheetName.DataTextField = "Name"
                    ddlSheetName.DataValueField = "ID"
                    ddlSheetName.DataBind()
                    ddlSheetName.Items.Insert(0, "Select Sheet")
                    ddlSheetName.SelectedValue = 1
                    ddlSheetName_SelectedIndexChanged(sender, e)
                Else
                    lblError.Text = "Select Excel file only." : lblExcelValidationMsg.Text = "Select Excel file only."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblExcelValidationMsg.Text = "Select Excel file."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "FileUpload_Load")
        End Try
    End Sub
    Public Function ExcelSheetNames(ByVal sPath As String) As DataTable
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            XLCon = MSAccessOpenConnection(sPath)
            dt = XLCon.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            If dt.Rows.Count > 0 Then
                dtTab.Columns.Add("ID")
                dtTab.Columns.Add("Name")
                For i = 0 To dt.Rows.Count - 1
                    drow = dtTab.NewRow
                    drow("ID") = i + 1
                    drow("Name") = dt.Rows(i)(2)
                    dtTab.Rows.Add(drow)
                Next
            End If
            XLCon.Close()
            Return dtTab
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Function
    Private Function MSAccessOpenConnection(ByVal sFile As String) As OleDb.OleDbConnection
        Dim con As New OleDb.OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" & sFile & ";Extended Properties=Excel 8.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
        Try
            con.ConnectionString = "Data Source=" & sFile & ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;"
            con.Open()
            Return con
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Function

    Protected Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Dim sStr As String = "", sYear As String
        Dim iYearID As Integer, iCheckMasterCounts As Integer = 0
        Dim ddlHeading As New DropDownList
        Dim ddlsubheading As New DropDownList
        Dim ddlItems As New DropDownList
        Dim ddlSubItems As New DropDownList
        Try
            lblError.Text = ""
            dgGeneral.Visible = False

            If ddlSheetName.SelectedIndex > 0 Then
                dttable = LoadTrialBalanceData(sFile)
                If IsNothing(dttable) Then
                    ddlSheetName.SelectedIndex = 0
                    lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf dttable.Rows.Count = 0 Then
                    lblError.Text = "No Data." : lblExcelValidationMsg.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                dgGeneral.DataSource = dttable
                dgGeneral.DataBind()
                dgGeneral.Visible = True
            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                ddlSheetName.SelectedIndex = 0
                imgbtnSave.Visible = False
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
                imgbtnSave.Visible = False
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadTrialBalanceData(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtDetails As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTable.Columns.Add("SrNo")
            dtTable.Columns.Add("DescID")
            dtTable.Columns.Add("Description")
            dtTable.Columns.Add("Itemclassification")
            dtTable.Columns.Add("UP")
            dtTable.Columns.Add("Quantity")
            dtTable.Columns.Add("UOM")
            dtTable.Columns.Add("Amount")

            dtDetails = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtDetails) = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtDetails
            End If

            For i = 0 To dtDetails.Rows.Count - 1
                If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                    dRow = dtTable.NewRow
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i).Item(1)) = False Then
                        If dtDetails.Rows(i).Item(1).ToString <> "&nbsp;" Then
                            dRow("Description") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(1))
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(2)) = False Then
                        If dtDetails.Rows(i).Item(2).ToString <> "&nbsp;" Then
                            dRow("Itemclassification") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(2))
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(3)) = False Then
                        If dtDetails.Rows(i).Item(3).ToString <> "&nbsp;" Then
                            dRow("UP") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(3))
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(4)) = False Then
                        If dtDetails.Rows(i).Item(4).ToString <> "&nbsp;" Then
                            dRow("Quantity") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(4))
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(5)) = False Then
                        If dtDetails.Rows(i).Item(5).ToString <> "&nbsp;" Then
                            dRow("UOM") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(5))
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(6)) = False Then
                        If dtDetails.Rows(i).Item(6).ToString <> "&nbsp;" Then
                            dRow("Amount") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(6))
                        End If
                    End If
                    dtTable.Rows.Add(dRow)
                End If
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Function

    Private Sub dgGeneral_PreRender(sender As Object, e As EventArgs) Handles dgGeneral.PreRender
        Try
            If dgGeneral.Rows.Count > 0 Then
                dgGeneral.UseAccessibleHeader = True
                dgGeneral.HeaderRow.TableSection = TableRowSection.TableHeader
                dgGeneral.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_PreRender")
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            If ddlCustName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblExcelValidationMsg.Text = lblError.Text
                Exit Sub
            ElseIf dgGeneral.Rows.Count > 0 Then
                SaveTrailbalanceSchedule()
                ddlCustName_SelectedIndexChanged(sender, e)
            Else
                lblError.Text = "No data"
                lblExcelValidationMsg.Text = lblError.Text
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub SaveTrailbalanceSchedule()
        Dim Arr() As String
        Dim lbldescription, lblQuantity, lblUOM, lblRate, lblAmount, lblItemclassification As New Label
        Dim lblDescID, lblsubItemid As New Label
        Dim Masid As Integer = 0
        Dim lblDescdetails As New Label
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Try
            For i = 0 To dgGeneral.Rows.Count - 1
                lblDescID = dgGeneral.Rows(i).FindControl("lblDescID")
                lbldescription = dgGeneral.Rows(i).FindControl("Description")
                lblItemclassification = dgGeneral.Rows(i).FindControl("Itemclassification")
                lblQuantity = dgGeneral.Rows(i).FindControl("Quantity")
                lblUOM = dgGeneral.Rows(i).FindControl("UOM")
                lblRate = dgGeneral.Rows(i).FindControl("UP")
                lblAmount = dgGeneral.Rows(i).FindControl("Amount")
                If lblQuantity.Text = "" Or Nothing Then
                    lblQuantity.Text = 0
                End If
                If lblUOM.Text = "" Or Nothing Then
                    lblUOM.Text = 0
                End If
                If lblAmount.Text = "" Or Nothing Then
                    lblAmount.Text = 0
                End If
                If lblRate.Text = "" Or Nothing Then
                    lblRate.Text = 0
                End If

                If Val(lblDescID.Text) <> 0 Then
                    objclsUSEntry.iACSI_id = lblDescID.Text
                    objclsUSEntry.sACSI_ItemdescCode = "SE-" & objclsUSEntry.iACSI_id
                Else
                    objclsUSEntry.iACSI_id = 0
                    objclsUSEntry.sACSI_ItemdescCode = "SE-" & objclsUSEntry.iACSI_id
                End If
                objclsUSEntry.sACSI_Itemdesc = lbldescription.Text
                objclsUSEntry.sACSI_classification = lblItemclassification.Text
                objclsUSEntry.iACSI_Custid = ddlCustName.SelectedValue
                objclsUSEntry.sACSI_Type = lblUOM.Text
                objclsUSEntry.iACSI_Qty = Double.Parse(lblQuantity.Text)
                objclsUSEntry.dACSI_Rate = Double.Parse(lblRate.Text)
                objclsUSEntry.dACSI_Total = Double.Parse(lblAmount.Text)
                objclsUSEntry.sACSI_DELFLG = "A"
                objclsUSEntry.iACSI_CRBY = sSession.UserID
                objclsUSEntry.sACSI_STATUS = "C"
                objclsUSEntry.iACSI_UPDATEDBY = sSession.UserID
                objclsUSEntry.sACSI_IPAddress = sSession.IPAddress
                objclsUSEntry.iACSI_CompId = sSession.AccessCodeID
                objclsUSEntry.iACSI_YEARId = ddlFinancialYear.SelectedValue
                Arr = objclsUSEntry.SaveTrailBalanceExcelUpload(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsUSEntry)
            Next
            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneral.Rows.Count - 1
                    chkField = dgGeneral.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            Else
                For i = 0 To dgGeneral.Rows.Count - 1
                    chkField = dgGeneral.Rows(i).FindControl("chkSelect")
                    If chkField.Checked = True Then
                        chkField.Checked = False
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnAddNote_Click(sender As Object, e As EventArgs) Handles btnAddNote.Click
        Try
            If ddlCustName.SelectedIndex < 1 Then
                lblExcelValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
                Exit Sub
            Else
                Response.Redirect(String.Format("ScheduleNote.aspx?"), False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Dim sPath As String = ""
        Try
            sPath = Server.MapPath("../") & "SampleExcels\TrialBal Format.xlsx"
            'Else
            '    sPath = Server.MapPath("../") & "Reports\ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "") & ".xls"
            'End If
            'sPath = Server.MapPath("../") & "Reports\ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "")
            DownloadFile(sPath)
            'Response.ContentType = "application/vnd.ms-excel"
            'Response.AppendHeader("Content-Disposition", "attachment; filename=TrialBal Format.xlsx")
            'Response.TransmitFile(Server.MapPath("~\SampleExcels\TrialBal Format.xlsx"))
            'Response.End()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkDownloadSchedTemp_Click(sender As Object, e As EventArgs) Handles lnkDownloadSchedTemp.Click
        Dim sPath As String = ""
        Try
            sPath = Server.MapPath("../") & "SampleExcels\TrialBal Format.xlsx"
            'Else
            '    sPath = Server.MapPath("../") & "Reports\ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "") & ".xls"
            'End If
            'sPath = Server.MapPath("../") & "Reports\ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "")
            DownloadFile(sPath)
            'Response.ContentType = "application/vnd.ms-excel"
            'Response.AppendHeader("Content-Disposition", "attachment; filename=TrialBal Format.xlsx")
            'Response.TransmitFile(Server.MapPath("~\SampleExcels\TrialBal Format.xlsx"))
            'Response.End()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub DownloadFile(ByVal pstrFileNameAndPath As String)
        Dim Extn As String, pstrContentType As String, sFileName As String, sFullName As String
        Dim myFileInfo As IO.FileInfo
        Dim StartPos As Long = 0, FileSize As Long, EndPos As Long
        Try
            If IO.File.Exists(pstrFileNameAndPath) Then
                myFileInfo = New IO.FileInfo(pstrFileNameAndPath)
                FileSize = myFileInfo.Length
                EndPos = FileSize
                Web.HttpContext.Current.Response.Clear()
                Web.HttpContext.Current.Response.ClearHeaders()
                Web.HttpContext.Current.Response.ClearContent()
                Extn = objclsGRACeGeneral.GetFileExt(pstrFileNameAndPath)
                sFileName = System.IO.Path.GetFileNameWithoutExtension(pstrFileNameAndPath)
                sFullName = sFileName & "." & Extn
                pstrContentType = "application/x-msexcel"
                Dim Range As String = Web.HttpContext.Current.Request.Headers("Range")
                If Not ((Range Is Nothing) Or (Range = "")) Then
                    Dim StartEnd As Array = Range.Substring(Range.LastIndexOf("=") + 1).Split("-")
                    If Not StartEnd(0) = "" Then
                        StartPos = CType(StartEnd(0), Long)
                    End If
                    If StartEnd.GetUpperBound(0) >= 1 And Not StartEnd(1) = "" Then
                        EndPos = CType(StartEnd(1), Long)
                    Else
                        EndPos = FileSize - StartPos
                    End If
                    If EndPos > FileSize Then
                        EndPos = FileSize - StartPos
                    End If
                    System.Web.HttpContext.Current.Response.StatusCode = 206
                    System.Web.HttpContext.Current.Response.StatusDescription = "Partial Content"
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Range", "bytes " & StartPos & "-" & EndPos & "/" & FileSize)
                End If
                System.Web.HttpContext.Current.Response.ContentType = pstrContentType
                System.Web.HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=" & sFullName & "")
                System.Web.HttpContext.Current.Response.WriteFile(Server.HtmlEncode(pstrFileNameAndPath), StartPos, EndPos)
                System.Web.HttpContext.Current.Response.Flush()
                System.Web.HttpContext.Current.Response.End()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadFile" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            objUT.DeleteCustdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, "", ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)

            '    dgGeneralSchedTemp.Visible = False
            'If FULoadSchedTemp.FileName <> String.Empty Then
            'lblSheetNameSchedTemp.Visible = True : ddlSheetNameSchedTemp.Visible = True
            '    imgbtnSaveSchedTemp.Enabled = True : ImgbtnApproveSchedTemp.Enabled = True
            '    sExt = IO.Path.GetExtension(FULoadSchedTemp.PostedFile.FileName)
            '    Session("sExt") = sExt
            '    If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
            '        sFileName = System.IO.Path.GetFileName(FULoadSchedTemp.PostedFile.FileName)
            '        Session("sFileName") = sFileName
            '        sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
            '        If sPath.EndsWith("\") = False Then
            '            sFile = sPath & "\" & sFileName
            '        Else
            '            sFile = sPath & sFileName
            '        End If
            '        FULoadSchedTemp.PostedFile.SaveAs(sFile)
            '        ddlSheetNameSchedTemp.Items.Clear()
            '        dt = ExcelSheetNamesSchedTemp(sFile)
            '        ddlSheetNameSchedTemp.DataSource = dt
            '        ddlSheetNameSchedTemp.DataTextField = "Name"
            '        ddlSheetNameSchedTemp.DataValueField = "ID"
            '        ddlSheetNameSchedTemp.DataBind()
            '        ddlSheetNameSchedTemp.Items.Insert(0, "Select Sheet")
            '        ddlSheetNameSchedTemp.SelectedValue = 1



            ddlSheetNameSchedTemp_SelectedIndexChanged(sender, e)
            DdlbranchSchedTemp_SelectedIndexChanged(sender, e)
            'lblExcelValidationMsg.Text = "Total Items in Excel:  " & dgGeneralSchedTemp.Rows.Count & ", TOtal Items Uploaded: " & dgGeneralSchedTemp.Rows.Count &
            '            ", Total opening Credit: " & TotalOpeningCredit & " = Total Opening Debit:" & TotalOpeningDebit & ", Total Tr Credit:" & TOtaltrCredit & ", Total tr Debit:" & TOtaltrDebit & ", Total Closing Credit: " & TOtalClosingCredit & ", Total CLosing Debit:" & TOtalClosingDebit
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            'lblError.Text = lblExcelValidationMsg.Text
            'Else
            '    lblError.Text = "Select Excel file only." : lblExcelValidationMsg.Text = "Select Excel file only."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            '    Exit Sub
            'End If
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            'dgGeneralSchedTemp.Visible = False
            'If FULoadSchedTemp.FileName <> String.Empty Then
            '    lblSheetNameSchedTemp.Visible = True : ddlSheetNameSchedTemp.Visible = True
            '    imgbtnSaveSchedTemp.Enabled = True : ImgbtnApproveSchedTemp.Enabled = True
            '    sExt = IO.Path.GetExtension(FULoadSchedTemp.PostedFile.FileName)
            '    Session("sExt") = sExt
            '    If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
            '        sFileName = System.IO.Path.GetFileName(FULoadSchedTemp.PostedFile.FileName)
            '        Session("sFileName") = sFileName
            '        sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
            '        If sPath.EndsWith("\") = False Then
            '            sFile = sPath & "\" & sFileName
            '        Else
            '            sFile = sPath & sFileName
            '        End If
            '        FULoadSchedTemp.PostedFile.SaveAs(sFile)
            '        ddlSheetNameSchedTemp.Items.Clear()
            '        dt = ExcelSheetNamesSchedTemp(sFile)
            '        ddlSheetNameSchedTemp.DataSource = dt
            '        ddlSheetNameSchedTemp.DataTextField = "Name"
            '        ddlSheetNameSchedTemp.DataValueField = "ID"
            '        ddlSheetNameSchedTemp.DataBind()
            '        ddlSheetNameSchedTemp.Items.Insert(0, "Select Sheet")
            '        ddlSheetNameSchedTemp.SelectedValue = 1

            ddlSheetNameSchedTemp_SelectedIndexChanged(sender, e)
            ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
            lblExcelValidationMsg.Text = "Total Items in Excel:  " & dgGeneralSchedTemp.Rows.Count & ", TOtal Items Uploaded: " & dgGeneralSchedTemp.Rows.Count &
                ", Total opening Credit: " & TotalOpeningCredit & " = Total Opening Debit:" & TotalOpeningDebit & ", Total Tr Credit:" & TOtaltrCredit & ", Total tr Debit:" & TOtaltrDebit & ", Total Closing Credit: " & TOtalClosingCredit & ", Total CLosing Debit:" & TOtalClosingDebit
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            lblError.Text = lblExcelValidationMsg.Text
            '    Else
            '        lblError.Text = "Select Excel file only." : lblExcelValidationMsg.Text = "Select Excel file only."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            '        Exit Sub
            '    End If
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DdlbranchSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlbranchSchedTemp.SelectedIndexChanged
        Dim dt As New System.Data.DataSet
        Dim dtprev As New System.Data.DataSet
        Dim dt1, dt2, dt3 As New DataTable
        Dim dClosCred, dCLosDeb As Integer
        Try
            imgbtnSaveSchedTemp.Enabled = True
            If DdlbranchSchedTemp.SelectedIndex > 0 Then
                dt = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
                dt1 = dt.Tables(0)
                dt3 = dt.Tables(2)
                dt1.Merge(dt3, False, MissingSchemaAction.Ignore)
                'dt1.Merge(dt3)
                dtprev = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue - 1, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
                If dtprev.Tables(0).Rows.Count > 0 Then
                    lnkBtnFreezePrev.Visible = False
                Else
                    lnkBtnFreezePrev.Visible = True
                End If
                If dt1.Rows.Count > 0 Then
                    dgGeneralSchedTemp.Visible = True
                    ddlscheduletypeSchedTemp.Enabled = True
                    lblscheduletypeSchedTemp.Enabled = True
                    dgGeneralSchedTemp.DataSource = dt
                    dgGeneralSchedTemp.DataBind()
                    pnlFreeze.Visible = True
                    dt2 = dt.Tables(1)
                    GrdviewTotalAmount.DataSource = dt2
                    GrdviewTotalAmount.DataBind()
                    dCLosDeb = Convert.ToInt32(dt2.Compute("SUM(ClosingDebit)", String.Empty))
                    dClosCred = Convert.ToInt32(dt2.Compute("SUM(ClosingCredit)", String.Empty))
                    If Val(dClosCred) <> Val(dCLosDeb) Then
                        lblError.Text = "Closing credit Amount : " & dClosCred & " and Closing debit amount : " & dCLosDeb & " not matched.Kindly re-upload the Excel with Debit and Credit amount matched totals" : lblExcelValidationMsg.Text = "Closing credit Amount : " & dClosCred & " and Closing debit amount : " & dCLosDeb & " not matched. Kindly re-upload the Excel with Debit and Credit amount matched totals."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                Else
                    dgGeneralSchedTemp.Visible = False
                    imgbtnSaveSchedTemp.Enabled = False
                    ImgbtnApproveSchedTemp.Enabled = False
                    pnlFreeze.Visible = False
                    dgGeneralSchedTemp.DataSource = Nothing
                    dgGeneralSchedTemp.DataBind()
                    GrdviewTotalAmount.DataSource = Nothing
                    GrdviewTotalAmount.DataBind()
                    lblError.Text = "No Data Found. Upload Trail Balance For this Customer"
                    Exit Sub
                End If
            Else
                dgGeneralSchedTemp.Visible = False
                imgbtnSaveSchedTemp.Enabled = False
                ImgbtnApproveSchedTemp.Enabled = False
                GrdviewTotalAmount.DataSource = Nothing
                GrdviewTotalAmount.DataBind()
                lblError.Text = "Select the Branch"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DdlbranchSchedTemp_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub lnkBtnFreeze_Click(sender As Object, e As EventArgs) Handles lnkBtnFreeze.Click
        Try
            If ddlCustName.SelectedIndex = 0 Then
                lblExcelValidationMsg.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            ElseIf DdlbranchSchedTemp.SelectedIndex = 0 Then
                lblExcelValidationMsg.Text = "Select Branch"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            lblfrz.Text = "Click 'Yes' to Freeze Trail Balance for next year Click, 'No' to Cancel "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgTypefrz').addClass('alert alert-warning');$('#ModalExcelValidationfrz').modal('show');", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdviewTotalAmount_PreRender(sender As Object, e As EventArgs) Handles GrdviewTotalAmount.PreRender
        Try
            If GrdviewTotalAmount.Rows.Count > 0 Then
                GrdviewTotalAmount.UseAccessibleHeader = True
                GrdviewTotalAmount.HeaderRow.TableSection = TableRowSection.TableHeader
                GrdviewTotalAmount.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GrdviewTotalAmount_PreRender")
        End Try

    End Sub
    'Harsha 13-10-2023
    'Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
    '    Dim dt As New System.Data.DataSet
    '    Dim dt1, dt2 As New DataTable
    '    Dim mimeType As String = Nothing
    '    Try
    '        dt = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
    '        dt1 = dt.Tables(0)

    '        If (dt1.Rows.Count) = 0 Then
    '            lblError.Text = "No Data"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
    '            lblExcelValidationMsg.Text = lblError.Text
    '            Exit Sub
    '        End If
    '        Dim rds As New ReportDataSource("DataSet1", dt1)
    '        ReportViewer1.LocalReport.DataSources.Add(rds)
    '        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/rptTrailBalance.rdlc")
    '        Dim Customer_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Customer_Name", ddlCustNameSchedTemp.SelectedItem.Text)}
    '        ReportViewer1.LocalReport.SetParameters(Customer_Name)
    '        Dim Finance_year As ReportParameter() = New ReportParameter() {New ReportParameter("Finance_year", ddlFinancialYear.SelectedItem.Text)}
    '        ReportViewer1.LocalReport.SetParameters(Finance_year)
    '        Dim Branch_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Branch_Name", DdlbranchSchedTemp.SelectedItem.Text)}
    '        ReportViewer1.LocalReport.SetParameters(Branch_Name)
    '        If ddlscheduletypeSchedTemp.SelectedIndex = 0 Then

    '            Dim Schedule_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Schedule_Type", "None")}
    '            ReportViewer1.LocalReport.SetParameters(Schedule_Type)
    '        Else
    '            Dim Schedule_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Schedule_Type", ddlscheduletypeSchedTemp.SelectedItem.Text)}
    '            ReportViewer1.LocalReport.SetParameters(Schedule_Type)
    '        End If
    '        Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
    '        Response.Buffer = True
    '        Response.Clear()
    '        Response.ContentType = mimeType
    '        Response.AddHeader("content-disposition", "attachment; filename=TrailBalance" + ".pdf")
    '        Response.BinaryWrite(pdfViewer)
    '        Response.Flush()
    '        Response.End()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
    '    End Try
    'End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim dt As New System.Data.DataSet
        Dim dt1, dt2 As New DataTable
        Dim mimeType As String = Nothing
        Dim dLedgerCount As Double = 0
        Dim dOpenDebit As Double = 0
        Dim dOpenCredit As Double = 0
        Dim dTRDebit As Double = 0
        Dim dTRCredit As Double = 0
        Dim dClosingDebit As Double = 0
        Dim dClosingCredit As Double = 0
        Dim objDBL As New DBHelper
        Dim dtdescDetails As New DataTable

        Try
            ReportViewer1.Reset()
            dt = objUT.GetCustCOAMasterDetailsDetailed(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
            dt1 = dt.Tables(0)
            dt2 = dt.Tables(1)
            dLedgerCount = Convert.ToDecimal(dt1.Compute("count(Description)", String.Empty))
            dOpenDebit = Convert.ToDecimal(dt1.Compute("sum(OpeningDebit)", String.Empty))
            dOpenCredit = Convert.ToDecimal(dt1.Compute("sum(OpeningCredit)", String.Empty))
            dTRDebit = Convert.ToDecimal(dt1.Compute("sum(TrDebit)", String.Empty))
            dTRCredit = Convert.ToDecimal(dt1.Compute("sum(TrCredit)", String.Empty))
            dClosingDebit = Convert.ToDecimal(dt1.Compute("sum(ClosingDebit)", String.Empty))
            dClosingCredit = Convert.ToDecimal(dt1.Compute("sum(ClosingCredit)", String.Empty))
            If (dt1.Rows.Count = 0) Then
                lblError.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
                Exit Sub
            End If

            '  dtdescDetails = objDBL.ReadExcel("Select Sub GL from [" & Trim(ddlSheetNameSchedTemp.SelectedItem.Text) & "]  Group BY Sub GL HAVING COUNT(Sub GL) > 1 ", sFile)
            Dim rds As New ReportDataSource("DataSet1", dt1)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/rptTrailBalance.rdlc")
            Dim Customer_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Customer_Name", ddlCustNameSchedTemp.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer_Name)
            Dim Finance_year As ReportParameter() = New ReportParameter() {New ReportParameter("Finance_year", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Finance_year)
            Dim Branch_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Branch_Name", DdlbranchSchedTemp.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Branch_Name)
            Dim LedgerCount As ReportParameter() = New ReportParameter() {New ReportParameter("LedgerCount", dLedgerCount)}
            ReportViewer1.LocalReport.SetParameters(LedgerCount)
            Dim OpenDebit As ReportParameter() = New ReportParameter() {New ReportParameter("OpenDebit", dOpenDebit)}
            ReportViewer1.LocalReport.SetParameters(OpenDebit)
            Dim OpenCredit As ReportParameter() = New ReportParameter() {New ReportParameter("OpenCredit", dOpenCredit)}
            ReportViewer1.LocalReport.SetParameters(OpenCredit)
            Dim TRDebit As ReportParameter() = New ReportParameter() {New ReportParameter("TRDebit", dTRDebit)}
            ReportViewer1.LocalReport.SetParameters(TRDebit)
            Dim TRCredit As ReportParameter() = New ReportParameter() {New ReportParameter("TRCredit", dTRCredit)}
            ReportViewer1.LocalReport.SetParameters(TRCredit)
            Dim ClosingDebit As ReportParameter() = New ReportParameter() {New ReportParameter("ClosingDebit", dClosingDebit)}
            ReportViewer1.LocalReport.SetParameters(ClosingDebit)
            Dim ClosingCredit As ReportParameter() = New ReportParameter() {New ReportParameter("ClosingCredit", dClosingCredit)}
            ReportViewer1.LocalReport.SetParameters(ClosingCredit)
            If ddlscheduletypeSchedTemp.SelectedIndex = 0 Then
                Dim Schedule_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Schedule_Type", "None")}
                ReportViewer1.LocalReport.SetParameters(Schedule_Type)
            Else
                Dim Schedule_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Schedule_Type", ddlscheduletypeSchedTemp.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Schedule_Type)
            End If
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename= " + ddlCustNameSchedTemp.SelectedItem.Text + "-" + DdlbranchSchedTemp.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    Private Sub btnPartner_Click(sender As Object, e As EventArgs) Handles btnPartner.Click
        Try
            Response.Redirect(String.Format("PartnersFund.aspx?"), False)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCashflow_Click(sender As Object, e As EventArgs) Handles btnCashflow.Click
        Try
            If ddlCustName.SelectedIndex < 1 Then
                lblExcelValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
                Exit Sub
            Else
                Response.Redirect(String.Format("CashFlow.aspx?"), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCashflow_Click")
        End Try
    End Sub
    Private Sub btnAccRatio_Click(sender As Object, e As EventArgs) Handles btnAccRatio.Click
        Try
            Response.Redirect(String.Format("accountingRatio.aspx?"), False)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnVersion_Click(sender As Object, e As EventArgs) Handles btnVersion.Click
        Dim lblVesrion As New Label
        Try
            If dgGeneralSchedTemp.Rows.Count > 0 Then
                SaveTrailbalanceVersion()
                objUT.SaveVersionMaster(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, sSession.UserID, ddlFinancialYear.SelectedValue, DdlbranchSchedTemp.SelectedValue)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SaveTrailbalanceVersion()
        Dim dttable As New DataTable
        Dim ddlHeading As New DropDownList
        Dim ddlsubheading As New DropDownList
        Dim ddlItems As New DropDownList
        Dim ddlSubItems As New DropDownList
        Dim Arr() As String
        Dim lbldescCode, lblOpeningDebit, lblOpeningCredit, lblTrDebit, lblTrCredit, lblClosingDebit, lblClosingCredit As New Label
        Dim lblheadingid, lblsubheadingid, lblitemid As New Label
        Dim lbldescirption As New LinkButton
        Dim lblDescID, lblsubItemid, lblVesrion, lblVerId As New Label
        Dim lblDescdetails As New Label
        Dim Schdeuletype As Integer
        Dim iFlag As Integer = 0
        Dim dt As New DataTable
        Try
            lblVerId.Text = objUT.CheckVersionId(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
            lblVesrion.Text = objUT.CheckVersion(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
            dt = objTBVersion.GetCustVersionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue, lblVesrion.Text)
            For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                Dim Headingid As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                Dim subheading As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                Dim item As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                Dim Subitem As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblsubitem")
                lbldescCode = dgGeneralSchedTemp.Rows(i).FindControl("lblDescriptionCode")
                lblDescdetails = dgGeneralSchedTemp.Rows(i).FindControl("lblDescdetails")
                lblDescID = dgGeneralSchedTemp.Rows(i).FindControl("lblDescID")
                lbldescirption = dgGeneralSchedTemp.Rows(i).FindControl("lblDescription")
                lblOpeningDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningDebit")
                lblOpeningCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningCredit")
                lblTrDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrDebit")
                lblTrCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrCredit")
                lblClosingDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingDebit")
                lblClosingCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingCredit")
                Headingid = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                subheading = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                item = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                Subitem = dgGeneralSchedTemp.Rows(i).FindControl("lblSubitem")

                Dim lblSchedType As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblScheduleType")
                Schdeuletype = lblSchedType.Text
                lblDescID.Text = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
                lblDescdetails.Text = objUT.CheckDetaileddata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue, DdlbranchSchedTemp.SelectedValue)
                If Val(lblDescID.Text) <> 0 Then
                    objTBVersion.iATBV_ID = lblDescID.Text
                Else
                    objTBVersion.iATBV_ID = 0
                End If
                If lbldescCode.Text <> "" Then
                    objTBVersion.sATBV_CODE = lbldescCode.Text
                Else
                    objTBVersion.sATBV_CODE = "SCh00" & (i + 1)
                End If
                objTBVersion.sATBV_Description = lbldescirption.Text
                objTBVersion.iATBV_CustId = ddlCustNameSchedTemp.SelectedValue
                objTBVersion.iATBV_VersionNo = lblVerId.Text
                objTBVersion.iATBV_Branchname = DdlbranchSchedTemp.SelectedValue
                objTBVersion.dATBV_Opening_Debit_Amount = Double.Parse(lblOpeningDebit.Text)
                objTBVersion.dATBV_Opening_Credit_Amount = Double.Parse(lblOpeningCredit.Text)
                objTBVersion.dATBV_TR_Debit_Amount = Double.Parse(lblTrDebit.Text)
                objTBVersion.dATBV_TR_Credit_Amount = Double.Parse(lblTrCredit.Text)
                objTBVersion.dATBV_Closing_Debit_Amount = Double.Parse(lblClosingDebit.Text)
                objTBVersion.dATBV_Closing_Credit_Amount = Double.Parse(lblClosingCredit.Text)
                objTBVersion.sATBV_DELFLG = "A"
                objTBVersion.iATBV_CRBY = sSession.UserID
                objTBVersion.sATBV_STATUS = "C"
                objTBVersion.iATBV_UPDATEDBY = sSession.UserID
                objTBVersion.sATBV_IPAddress = sSession.IPAddress
                objTBVersion.iATBV_CompId = sSession.AccessCodeID
                objTBVersion.iATBV_YEARId = ddlFinancialYearSchedTemp.SelectedValue
                Arr = objTBVersion.SaveTrailBalanceVersion(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objTBVersion)
                iFlag = 0
                If Val(lblDescdetails.Text) = 0 Then
                    objTBVersion.iATBVD_ID = 0
                    objTBVersion.iATBVD_Masid = Arr(1)
                Else
                    objTBVersion.iATBVD_ID = lblDescdetails.Text
                    objTBVersion.iATBVD_Masid = Arr(1)
                End If
                If lbldescCode.Text <> "" Then
                    objTBVersion.sATBVD_CODE = lbldescCode.Text
                Else
                    objTBVersion.sATBVD_CODE = "SCh00" & (i + 1)
                End If
                objTBVersion.sATBVD_Description = lbldescirption.Text
                objTBVersion.iATBVD_CustId = ddlCustNameSchedTemp.SelectedValue
                objTBVersion.iATBVD_VersionNo = lblVerId.Text
                If Schdeuletype > 0 Then
                    objTBVersion.iATBVD_SChedule_Type = Schdeuletype
                Else
                    objTBVersion.iATBVD_SChedule_Type = 0
                End If
                If DdlbranchSchedTemp.SelectedIndex > 0 Then
                    objTBVersion.iATBVD_Branchname = DdlbranchSchedTemp.SelectedValue
                Else
                    objTBVersion.iATBVD_Branchname = 0
                End If
                objTBVersion.iATBVD_Company_Type = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue)
                If Val(Headingid.Text) > 0 Then
                    If lblVesrion.Text > 0 Then
                        If dt.Rows(i)("headingid") <> Val(Headingid.Text) Then
                            iFlag = 1
                        End If
                    End If
                    objTBVersion.iATBVD_Headingid = Val(Headingid.Text)
                Else
                    objTBVersion.iATBVD_Headingid = 0
                End If
                If Val(subheading.Text) > 0 Then
                    If lblVesrion.Text > 0 Then
                        If dt.Rows(i)("subheadingid") <> Val(subheading.Text) Then
                            iFlag = 1
                        End If
                    End If
                    objTBVersion.iATBVD_Subheading = Val(subheading.Text)
                Else
                    objTBVersion.iATBVD_Subheading = 0
                End If
                If Val(item.Text) > 0 Then
                    If lblVesrion.Text > 0 Then
                        If dt.Rows(i)("itemid") <> Val(item.Text) Then
                            iFlag = 1
                        End If
                    End If
                    objTBVersion.iATBVD_itemid = Val(item.Text)
                Else
                    objTBVersion.iATBVD_itemid = 0
                End If
                If Val(Subitem.Text) > 0 Then
                    If lblVesrion.Text > 0 Then
                        If dt.Rows(i)("subItemID") <> Val(Subitem.Text) Then
                            iFlag = 1
                        End If
                    End If
                    objTBVersion.iATBVD_Subitemid = Val(Subitem.Text)
                Else
                    objTBVersion.iATBVD_Subitemid = 0
                End If
                objTBVersion.sATBVD_DELFLG = "A"
                objTBVersion.iATBVD_CRBY = sSession.UserID
                objTBVersion.sATBVD_STATUS = "C"
                objTBVersion.sATBVD_Progress = "Uploaded"
                objTBVersion.iATBVD_UPDATEDBY = sSession.UserID
                objTBVersion.sATBVD_IPAddress = sSession.IPAddress
                objTBVersion.iATBVD_CompId = sSession.AccessCodeID
                objTBVersion.iATBVD_YEARId = ddlFinancialYearSchedTemp.SelectedValue
                If lblVesrion.Text > 0 Then
                    If iFlag = 1 Then
                        If dt.Rows(i)("Description") = lbldescirption.Text Then
                            iFlag = 1
                        Else
                            iFlag = 0
                        End If
                    End If
                End If
                objTBVersion.iATBVD_iFLAG = iFlag
                Arr = objTBVersion.SaveTrailBalanceVersiondetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objTBVersion)
            Next
            lblExcelValidationMsg.Text = "Successfully uploaded "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert success-warning');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnViewVersion_Click(sender As Object, e As EventArgs) Handles btnViewVersion.Click
        Try
            If ddlCustName.SelectedIndex < 1 Then
                lblExcelValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
                Exit Sub
            Else
                Response.Redirect(String.Format("TrailBalanceVersion.aspx?"), False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnfrz_ServerClick(sender As Object, e As EventArgs) Handles btnfrz.ServerClick
        Dim dttable As New DataTable
        Dim sStr As String = ""
        Dim ddlHeading As New DropDownList
        Dim ddlsubheading As New DropDownList
        Dim ddlItems As New DropDownList
        Dim ddlSubItems As New DropDownList
        Dim Arr() As String
        Dim lbldescCode, lblOpeningDebit, lblOpeningCredit, lblTrDebit, lblTrCredit, lblClosingDebit, lblClosingCredit As New Label
        Dim lblheadingid, lblsubheadingid, lblitemid As New Label
        Dim lbldescirption As New LinkButton
        Dim lblDescID, lblsubItemid As New Label
        Dim Masid As Integer = 0
        Dim lblDescdetails As New Label
        Dim Schdeuletype As Integer = 0
        Try
            If dgGeneralSchedTemp.Rows.Count > 0 Then
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    Dim Headingid As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                    Dim subheading As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                    Dim item As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                    Dim Subitem As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblsubitem")
                    Dim lblScheduleType As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblScheduleType")
                    lbldescCode = dgGeneralSchedTemp.Rows(i).FindControl("lblDescriptionCode")
                    lblDescdetails = dgGeneralSchedTemp.Rows(i).FindControl("lblDescdetails")
                    lblDescID = dgGeneralSchedTemp.Rows(i).FindControl("lblDescID")
                    lbldescirption = dgGeneralSchedTemp.Rows(i).FindControl("lblDescription")
                    lblOpeningDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningDebit")
                    lblOpeningCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblOpeningCredit")
                    lblTrDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrDebit")
                    lblTrCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblTrCredit")
                    lblClosingDebit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingDebit")
                    lblClosingCredit = dgGeneralSchedTemp.Rows(i).FindControl("lblClosingCredit")
                    Headingid = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                    subheading = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                    item = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                    Subitem = dgGeneralSchedTemp.Rows(i).FindControl("lblSubitem")
                    If Val(lblScheduleType.Text) = 0 Then
                        Schdeuletype = 0
                    Else
                        Schdeuletype = Val(lblScheduleType.Text)
                    End If
                    lblDescID.Text = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue + 1, DdlbranchSchedTemp.SelectedValue)
                    lblDescdetails.Text = objUT.CheckDetaileddata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue + 1, DdlbranchSchedTemp.SelectedValue)
                    If Val(lblDescID.Text) <> 0 Then
                        objUT.iATBU_ID = lblDescID.Text
                    Else
                        objUT.iATBU_ID = 0
                    End If
                    If lbldescCode.Text <> "" Then
                        objUT.sATBU_CODE = lbldescCode.Text
                    Else
                        objUT.sATBU_CODE = "SCh00" & (i + 1)
                    End If
                    objUT.sATBU_Description = lbldescirption.Text
                    objUT.iATBU_CustId = ddlCustNameSchedTemp.SelectedValue
                    objUT.iATBU_Branchname = DdlbranchSchedTemp.SelectedValue
                    If Schdeuletype = 4 Then
                        objUT.dATBU_Opening_Debit_Amount = Double.Parse(lblClosingDebit.Text)
                        objUT.dATBU_Opening_Credit_Amount = Double.Parse(lblClosingCredit.Text)
                        objUT.dATBU_TR_Debit_Amount = 0
                        objUT.dATBU_TR_Credit_Amount = 0
                        objUT.dATBU_Closing_Debit_Amount = Double.Parse(lblClosingDebit.Text)
                        objUT.dATBU_Closing_Credit_Amount = Double.Parse(lblClosingCredit.Text)
                    Else
                        objUT.dATBU_Opening_Debit_Amount = 0
                        objUT.dATBU_Opening_Credit_Amount = 0
                        objUT.dATBU_TR_Debit_Amount = 0
                        objUT.dATBU_TR_Credit_Amount = 0
                        objUT.dATBU_Closing_Debit_Amount = 0
                        objUT.dATBU_Closing_Credit_Amount = 0
                    End If
                    objUT.sATBU_DELFLG = "A"
                    objUT.iATBU_CRBY = sSession.UserID
                    objUT.sATBU_STATUS = "C"
                    objUT.iATBU_UPDATEDBY = sSession.UserID
                    objUT.sATBU_IPAddress = sSession.IPAddress
                    objUT.iATBU_CompId = sSession.AccessCodeID
                    objUT.iATBU_YEARId = ddlFinancialYearSchedTemp.SelectedValue + 1
                    Arr = objUT.SaveTrailBalanceExcelUpload(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                    If Val(lblDescdetails.Text) = 0 Then
                        '       objUT.iATBUD_ID = Val(lblDescID.Text)
                        objUT.iATBUD_ID = 0
                        objUT.iATBUD_Masid = Arr(1)
                    Else
                        objUT.iATBUD_ID = lblDescdetails.Text
                        objUT.iATBUD_Masid = Arr(1)
                    End If
                    If lbldescCode.Text <> "" Then
                        objUT.sATBUD_CODE = lbldescCode.Text
                    Else
                        objUT.sATBUD_CODE = "SCh00" & (i + 1)
                    End If
                    objUT.sATBUD_Description = lbldescirption.Text
                    objUT.iATBUD_CustId = ddlCustNameSchedTemp.SelectedValue
                    If Schdeuletype > 0 Then
                        objUT.iATBUD_SChedule_Type = Schdeuletype
                    Else
                        objUT.iATBUD_SChedule_Type = 0
                    End If
                    If DdlbranchSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_Branchname = DdlbranchSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_Branchname = 0
                    End If
                    objUT.iATBUD_Company_Type = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue)
                    If Val(Headingid.Text) > 0 Then
                        objUT.iATBUD_Headingid = Val(Headingid.Text)
                    Else
                        objUT.iATBUD_Headingid = 0
                    End If
                    If Val(subheading.Text) > 0 Then
                        objUT.iATBUD_Subheading = Val(subheading.Text)
                    Else
                        objUT.iATBUD_Subheading = 0
                    End If
                    If Val(item.Text) > 0 Then
                        objUT.iATBUD_itemid = Val(item.Text)
                    Else
                        objUT.iATBUD_itemid = 0
                    End If
                    If Val(Subitem.Text) > 0 Then
                        objUT.iATBUD_Subitemid = Val(Subitem.Text)
                    Else
                        objUT.iATBUD_Subitemid = 0
                    End If
                    objUT.sATBUD_DELFLG = "A"
                    objUT.iATBUD_CRBY = sSession.UserID
                    objUT.sATBUD_STATUS = "C"
                    objUT.sATBUD_Progress = "Uploaded"
                    objUT.iATBUD_UPDATEDBY = sSession.UserID
                    objUT.sATBUD_IPAddress = sSession.IPAddress
                    objUT.iATBUD_CompId = sSession.AccessCodeID
                    objUT.iATBUD_YEARId = ddlFinancialYearSchedTemp.SelectedValue + 1
                    Arr = objUT.SaveTrailBalanceExcelUploaddetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                Next
                lblExcelValidationMsg.Text = " Ledgers Updated to Next Year " & 20 & ddlFinancialYearSchedTemp.SelectedValue & "-" & 20 & ddlFinancialYearSchedTemp.SelectedValue + 1
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnFreeze_Click")
        End Try
    End Sub

    Private Sub lnkBtnFreezePrev_Click(sender As Object, e As EventArgs) Handles lnkBtnFreezePrev.Click
        Dim dttable As New DataTable
        Dim sStr As String = ""
        Dim ddlHeading As New DropDownList
        Dim ddlsubheading As New DropDownList
        Dim ddlItems As New DropDownList
        Dim ddlSubItems As New DropDownList
        Dim Arr() As String
        Dim lbldescCode As New Label
        Dim lblheadingid, lblsubheadingid, lblitemid As New Label
        Dim lbldescirption As New LinkButton
        Dim lblDescID, lblsubItemid As New Label
        Dim Masid As Integer = 0
        Dim lblDescdetails As New Label
        Dim Schdeuletype As Integer = 0
        Try
            If dgGeneralSchedTemp.Rows.Count > 0 Then
                For i = 0 To dgGeneralSchedTemp.Rows.Count - 1
                    Dim Headingid As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                    Dim subheading As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                    Dim item As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                    Dim Subitem As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblsubitem")
                    Dim lblScheduleType As Label = dgGeneralSchedTemp.Rows(i).FindControl("lblScheduleType")
                    lbldescCode = dgGeneralSchedTemp.Rows(i).FindControl("lblDescriptionCode")
                    lblDescdetails = dgGeneralSchedTemp.Rows(i).FindControl("lblDescdetails")
                    lblDescID = dgGeneralSchedTemp.Rows(i).FindControl("lblDescID")
                    lbldescirption = dgGeneralSchedTemp.Rows(i).FindControl("lblDescription")
                    Headingid = dgGeneralSchedTemp.Rows(i).FindControl("lblheading")
                    subheading = dgGeneralSchedTemp.Rows(i).FindControl("lblSubheading")
                    item = dgGeneralSchedTemp.Rows(i).FindControl("lblitem")
                    Subitem = dgGeneralSchedTemp.Rows(i).FindControl("lblSubitem")
                    If Val(lblScheduleType.Text) = 0 Then
                        Schdeuletype = 0
                    Else
                        Schdeuletype = Val(lblScheduleType.Text)
                    End If
                    lblDescID.Text = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue - 1, DdlbranchSchedTemp.SelectedValue)
                    lblDescdetails.Text = objUT.CheckDetaileddata(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, lbldescirption.Text, ddlFinancialYearSchedTemp.SelectedValue - 1, DdlbranchSchedTemp.SelectedValue)
                    If Val(lblDescID.Text) <> 0 Then
                        objUT.iATBU_ID = lblDescID.Text
                    Else
                        objUT.iATBU_ID = 0
                    End If
                    If lbldescCode.Text <> "" Then
                        objUT.sATBU_CODE = lbldescCode.Text
                    Else
                        objUT.sATBU_CODE = "SCh00" & (i + 1)
                    End If
                    objUT.sATBU_Description = lbldescirption.Text
                    objUT.iATBU_CustId = ddlCustNameSchedTemp.SelectedValue
                    objUT.iATBU_Branchname = DdlbranchSchedTemp.SelectedValue
                    objUT.dATBU_Opening_Debit_Amount = 0
                    objUT.dATBU_Opening_Credit_Amount = 0
                    objUT.dATBU_TR_Debit_Amount = 0
                    objUT.dATBU_TR_Credit_Amount = 0
                    objUT.dATBU_Closing_Debit_Amount = 0
                    objUT.dATBU_Closing_Credit_Amount = 0
                    objUT.sATBU_DELFLG = "A"
                    objUT.iATBU_CRBY = sSession.UserID
                    objUT.sATBU_STATUS = "C"
                    objUT.iATBU_UPDATEDBY = sSession.UserID
                    objUT.sATBU_IPAddress = sSession.IPAddress
                    objUT.iATBU_CompId = sSession.AccessCodeID
                    objUT.iATBU_YEARId = ddlFinancialYearSchedTemp.SelectedValue - 1
                    Arr = objUT.SaveTrailBalanceExcelUpload(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                    If Val(lblDescdetails.Text) = 0 Then
                        '       objUT.iATBUD_ID = Val(lblDescID.Text)
                        objUT.iATBUD_ID = 0
                        objUT.iATBUD_Masid = Arr(1)
                    Else
                        objUT.iATBUD_ID = lblDescdetails.Text
                        objUT.iATBUD_Masid = Arr(1)
                    End If
                    If lbldescCode.Text <> "" Then
                        objUT.sATBUD_CODE = lbldescCode.Text
                    Else
                        objUT.sATBUD_CODE = "SCh00" & (i + 1)
                    End If
                    objUT.sATBUD_Description = lbldescirption.Text
                    objUT.iATBUD_CustId = ddlCustNameSchedTemp.SelectedValue
                    If Schdeuletype > 0 Then
                        objUT.iATBUD_SChedule_Type = Schdeuletype
                    Else
                        objUT.iATBUD_SChedule_Type = 0
                    End If
                    If DdlbranchSchedTemp.SelectedIndex > 0 Then
                        objUT.iATBUD_Branchname = DdlbranchSchedTemp.SelectedValue
                    Else
                        objUT.iATBUD_Branchname = 0
                    End If
                    objUT.iATBUD_Company_Type = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue)
                    If Val(Headingid.Text) > 0 Then
                        objUT.iATBUD_Headingid = Val(Headingid.Text)
                    Else
                        objUT.iATBUD_Headingid = 0
                    End If
                    If Val(subheading.Text) > 0 Then
                        objUT.iATBUD_Subheading = Val(subheading.Text)
                    Else
                        objUT.iATBUD_Subheading = 0
                    End If
                    If Val(item.Text) > 0 Then
                        objUT.iATBUD_itemid = Val(item.Text)
                    Else
                        objUT.iATBUD_itemid = 0
                    End If
                    If Val(Subitem.Text) > 0 Then
                        objUT.iATBUD_Subitemid = Val(Subitem.Text)
                    Else
                        objUT.iATBUD_Subitemid = 0
                    End If
                    objUT.sATBUD_DELFLG = "A"
                    objUT.iATBUD_CRBY = sSession.UserID
                    objUT.sATBUD_STATUS = "C"
                    objUT.sATBUD_Progress = "Uploaded"
                    objUT.iATBUD_UPDATEDBY = sSession.UserID
                    objUT.sATBUD_IPAddress = sSession.IPAddress
                    objUT.iATBUD_CompId = sSession.AccessCodeID
                    objUT.iATBUD_YEARId = ddlFinancialYearSchedTemp.SelectedValue - 1
                    Arr = objUT.SaveTrailBalanceExcelUploaddetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                Next
                ddlFinancialYearSchedTemp.SelectedValue = ddlFinancialYearSchedTemp.SelectedValue - 1
                DdlbranchSchedTemp_SelectedIndexChanged(sender, e)
                lblExcelValidationMsg.Text = " Ledgers Updated to Previous Year " & ddlFinancialYearSchedTemp.SelectedItem.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                lblError.Text = lblExcelValidationMsg.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnFreeze_Click")
        End Try
    End Sub
    Private Sub btnTrade_Click(sender As Object, e As EventArgs) Handles btnTrade.Click
        Try
            Response.Redirect(String.Format("Trade.aspx?"), False)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkbtnExcelData_Click(sender As Object, e As EventArgs) Handles lnkbtnExcelData.Click
        Dim dt As New System.Data.DataSet
        Dim dt1, dt2 As New DataTable
        Dim mimeType As String = Nothing
        Dim dLedgerCount As Double = 0
        Dim dOpenDebit As Double = 0
        Dim dOpenCredit As Double = 0
        Dim dTRDebit As Double = 0
        Dim dTRCredit As Double = 0
        Dim dClosingDebit As Double = 0
        Dim dClosingCredit As Double = 0
        Dim objDBL As New DBHelper
        Dim dtdescDetails As New DataTable

        Try
            ReportViewer1.Reset()
            dt = objUT.GetCustCOAMasterDetailsDetailed(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedValue, 0, Unmapped, DdlbranchSchedTemp.SelectedValue)
            dt1 = dt.Tables(0)
            dt2 = dt.Tables(1)
            dLedgerCount = Convert.ToDecimal(dt1.Compute("count(Description)", String.Empty))
            dOpenDebit = Convert.ToDecimal(dt1.Compute("sum(OpeningDebit)", String.Empty))
            dOpenCredit = Convert.ToDecimal(dt1.Compute("sum(OpeningCredit)", String.Empty))
            dTRDebit = Convert.ToDecimal(dt1.Compute("sum(TrDebit)", String.Empty))
            dTRCredit = Convert.ToDecimal(dt1.Compute("sum(TrCredit)", String.Empty))
            dClosingDebit = Convert.ToDecimal(dt1.Compute("sum(ClosingDebit)", String.Empty))
            dClosingCredit = Convert.ToDecimal(dt1.Compute("sum(ClosingCredit)", String.Empty))
            If (dt1.Rows.Count = 0) Then
                lblError.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = lblError.Text
                Exit Sub
            End If

            '  dtdescDetails = objDBL.ReadExcel("Select Sub GL from [" & Trim(ddlSheetNameSchedTemp.SelectedItem.Text) & "]  Group BY Sub GL HAVING COUNT(Sub GL) > 1 ", sFile)
            Dim rds As New ReportDataSource("DataSet1", dt1)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/rptTrailBalanceData.rdlc")

            'Dim Customer_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Customer_Name", ddlCustNameSchedTemp.SelectedItem.Text)}
            'ReportViewer1.LocalReport.SetParameters(Customer_Name)
            'Dim Finance_year As ReportParameter() = New ReportParameter() {New ReportParameter("Finance_year", ddlFinancialYear.SelectedItem.Text)}
            'ReportViewer1.LocalReport.SetParameters(Finance_year)
            'Dim Branch_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Branch_Name", DdlbranchSchedTemp.SelectedItem.Text)}
            'ReportViewer1.LocalReport.SetParameters(Branch_Name)

            'If ddlscheduletypeSchedTemp.SelectedIndex = 0 Then
            '    Dim Schedule_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Schedule_Type", "None")}
            '    ReportViewer1.LocalReport.SetParameters(Schedule_Type)
            'Else
            '    Dim Schedule_Type As ReportParameter() = New ReportParameter() {New ReportParameter("Schedule_Type", ddlscheduletypeSchedTemp.SelectedItem.Text)}
            '    ReportViewer1.LocalReport.SetParameters(Schedule_Type)
            'End If

            Dim workbook As Workbook = New Workbook()

            'Load a sample Excel document
            workbook.LoadFromFile("C:\Users\i5_4G_X2\Downloads\ABC Private Limited-Banglore-21-22 Data (14).xls")

            'Get the first worksheet
            Dim sheet As Worksheet = workbook.Worksheets(0)

            ' Add values to specified cells
            sheet.Range("A13").Value = "Complete"
            sheet.Range("A14").Value = "Pending"
            sheet.Range("A15").Value = "Cancelled"

            ' Create a drop-down list by referring to a specified data range as the data validation source
            sheet.Range("C2:C7").DataValidation.DataRange = sheet.Range("A13:A15")

            'Save the result document
            workbook.SaveToFile("ExcelDropdownList.xlsx", ExcelVersion.Version2010)
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
        Response.Buffer = True
        Response.Clear()
        Response.ContentType = mimeType
        Response.AddHeader("content-disposition", "attachment; filename= " + ddlCustNameSchedTemp.SelectedItem.Text + "-" + DdlbranchSchedTemp.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + " Data.xls")
        Response.BinaryWrite(pdfViewer)
        Response.Flush()
        Response.End()
        Catch ex As Exception
        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

End Class
